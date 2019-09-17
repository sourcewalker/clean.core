using Admin.Service.REST.Models;
using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;

namespace Admin.Service.REST.Controllers
{
    [ApiController]
    [RequireHttps]
    [Route("[controller]")]
    //[EnableCors]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;
        private readonly ILoggingProvider _logger;

        public SiteController(
            ISiteService siteService,
            ILoggingProvider logger)
        {
            _siteService = siteService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve site list organised by page
        /// </summary>
        /// <param name="pageNumber">Current page to retrieve</param>
        /// <param name="pageSize">Size of page</param>
        /// <remarks>
        /// Get Site list paged
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult GetPaged(int pageNumber, int pageSize)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request",
                Data = expando
            };

            try
            {
                expando.Sites = _siteService.GetSitesPaged(pageNumber, pageSize);

                apiResponse.Success = true;
                apiResponse.Message = "Site list returned successfully";
                apiResponse.Data = expando;

                _logger.LogWarn("Sites Requested", $"On {DateTimeOffset.UtcNow.ToString()}");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando = new ExpandoObject();
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return BadRequest(apiResponse);
            }
        }

        /// <summary>
        /// Retrieve site by Id
        /// </summary>
        /// <param name="id">Id of the site to retrieve</param>
        /// <remarks>
        /// Get Site by Id
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Get(string id)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request",
                Data = expando
            };

            try
            {
                var idGuid = Guid.Empty;
                var isGuid = string.IsNullOrEmpty(id) ? 
                    false : Guid.TryParse(id, out idGuid);
                if (!isGuid)
                {
                    expando.Description = "id format is not correct.";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Parameter error", "id parameter is in bad format");

                    return BadRequest(apiResponse);
                }

                expando.Site = _siteService.GetSite(idGuid);

                apiResponse.Success = true;
                apiResponse.Message = "Site list returned successfully";
                apiResponse.Data = expando;

                _logger.LogWarn($"Site Id Requested: {idGuid}", $"On {DateTimeOffset.UtcNow.ToString()}");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando = new ExpandoObject();
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return BadRequest(apiResponse);
            }
        }

        /// <summary>
        /// Create site
        /// </summary>
        /// <param name="site">Site to create</param>
        /// <remarks>
        /// Create
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody]SiteDto site)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request",
                Data = expando
            };

            try
            {
                site.Id = site.Id == default ? Guid.NewGuid() : site.Id;
                var status = _siteService.CreateSite(site);
                if (!status)
                {
                    expando.Description = "Creation has failed. Please retry later.";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Site Creation error", "Creation failure");

                    return BadRequest(apiResponse);
                }

                apiResponse.Success = true;
                apiResponse.Message = "Site creation successfully";
                apiResponse.Data = expando;

                _logger.LogWarn($"Site Id Created: {site.Id}", $"On {DateTimeOffset.UtcNow.ToString()}");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando = new ExpandoObject();
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return BadRequest(apiResponse);
            }
        }

        /// <summary>
        /// Edit site by Id
        /// </summary>
        /// <param name="id">Id of the site to update</param>
        /// <param name="site">Updated site</param>
        /// <remarks>
        /// Update Site by Id
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Put(string id, [FromBody]SiteDto site)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request",
                Data = expando
            };

            try
            {
                var idGuid = Guid.Empty;
                var isGuid = string.IsNullOrEmpty(id) ?
                    false : Guid.TryParse(id, out idGuid);
                if (!isGuid)
                {
                    expando.Description = "id format is not correct.";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Parameter error", "id parameter is in bad format");

                    return BadRequest(apiResponse);
                }

                site.Id = site.Id == idGuid ? site.Id : idGuid;
                var status = _siteService.UpdateSite(site);
                if (!status)
                {
                    expando.Description = "Update has failed. Please retry later.";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Site Update error", "Update failure");

                    return BadRequest(apiResponse);
                }

                apiResponse.Success = true;
                apiResponse.Message = "Site updated successfully";
                apiResponse.Data = expando;

                _logger.LogWarn($"Site Id Updated: {idGuid}", $"On {DateTimeOffset.UtcNow.ToString()}");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando = new ExpandoObject();
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return BadRequest(apiResponse);
            }
        }

        /// <summary>
        /// Remove site by Id
        /// </summary>
        /// <param name="id">Id of the site to delete</param>
        /// <remarks>
        /// Delete Site by Id
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(string id)
        {
            dynamic expando = new ExpandoObject();

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Bad Request",
                Data = expando
            };

            try
            {
                var idGuid = Guid.Empty;
                var isGuid = string.IsNullOrEmpty(id) ?
                    false : Guid.TryParse(id, out idGuid);
                if (!isGuid)
                {
                    expando.Description = "id format is not correct.";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Parameter error", "id parameter is in bad format");

                    return BadRequest(apiResponse);
                }

                var status = _siteService.DeleteSite(idGuid);
                if (!status)
                {
                    expando.Description = "Deletion has failed. Please retry later.";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Site Deletion error", "Deletion failure");

                    return BadRequest(apiResponse);
                }

                apiResponse.Success = true;
                apiResponse.Message = "Site removed successfully";
                apiResponse.Data = expando;

                _logger.LogWarn($"Site Id Removed: {idGuid}", $"On {DateTimeOffset.UtcNow.ToString()}");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando = new ExpandoObject();
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return BadRequest(apiResponse);
            }
        }
    }
}
