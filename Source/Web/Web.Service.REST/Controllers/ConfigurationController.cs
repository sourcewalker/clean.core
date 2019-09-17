using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;
using Web.Service.REST.Models;

namespace Web.Service.REST.Controllers
{
    [ApiController]
    [RequireHttps]
    [Route("[controller]")]
    [EnableCors]
    public class ConfigurationController : ControllerBase
    {
        private readonly ISiteService _siteService;
        private readonly ILoggingProvider _logger;

        public ConfigurationController(
            ISiteService siteService,
            ILoggingProvider logger)
        {
            _siteService = siteService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve site configuration culture related
        /// </summary>
        /// <param name="culture">Culture associated to the site</param>
        /// <remarks>
        /// Get Site configuration by culture
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("getsitebyculture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult GetSiteByCulture(string culture)
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
                expando.Site = _siteService.GetSiteByCulture(culture);

                apiResponse.Success = true;
                apiResponse.Message = "Operation success";
                apiResponse.Data = expando;

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
