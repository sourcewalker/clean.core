using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using Core.Shared.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Service.REST.Models;

namespace Web.Service.REST.Controllers
{
    [ApiController]
    [RequireHttps]
    [Route("[controller]")]
    //[EnableCors]
    public class ParticipateController : ControllerBase
    {
        private readonly IJourneyService _journeyService;
        private readonly ILoggingProvider _logger;
        private readonly ISiteService _siteService;

        public ParticipateController(
            IJourneyService journeyService,
            ILoggingProvider logger,
            ISiteService siteService)
        {
            _journeyService = journeyService;
            _logger = logger;
            _siteService = siteService;
        }

        /// <summary>
        /// Retrieve All model configuration
        /// </summary>
        /// <param name="culture">Culture of the current request</param>
        /// <remarks>
        /// This endpoint will return :
        ///  * View Model data
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("model")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult GetModel(string culture)
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
                if (culture == null)
                {
                    expando.Description = "Please provide culture parameter";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Participate model error", "Culture parameter is missing");
                    return BadRequest(apiResponse);
                }

                var site = _siteService.GetSiteByCulture(culture);

                apiResponse.Success = true;
                apiResponse.Message = "Participate successfull";
                apiResponse.Data = expando;

                _logger.LogTrace("Participate Model retrieval", "Ok");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return BadRequest(apiResponse);
            }
        }

        /// <summary>
        /// Retrieve Participate status and retailer related prize
        /// </summary>
        /// <param name="culture">Culture of the current request</param>
        /// <remarks>
        /// This endpoint will return :
        ///  * ChocolateBar List
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult GetParticipateStatus(string culture)
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
                if (culture == null)
                {
                    expando.Description = "Please provide culture parameter";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Participate model error", "Culture parameter is missing");
                    return BadRequest(apiResponse);
                }

                var site = _siteService.GetSiteByCulture(culture);

                apiResponse.Success = true;
                apiResponse.Message = "Participate successfull";
                apiResponse.Data = expando;

                _logger.LogTrace("Participate and prize retrieval", "Ok");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                expando.Error = e.Message;

                apiResponse.Success = false;
                apiResponse.Message = $"Error occured in {e.Source}";
                apiResponse.Data = expando;

                _logger.LogError(e.Message, e);

                return BadRequest(apiResponse);
            }
        }

        /// <summary>
        /// Create a participate entry
        /// </summary>
        /// <param name="participate">Participation related data needed</param>
        /// <remarks>
        /// Participation to inventor participate
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("participate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Participate([FromBody]ParticipationViewModel participate)
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
                if (participate == null)
                {
                    expando.Error = new List<string>() { "Body request data should be as documented" };

                    apiResponse.Message = "Missing or unknown request body";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Participate validation Error", "Body request not found or unknown");
                    return BadRequest(apiResponse);
                }

                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    var errorList = allErrors.Select(error => error.ErrorMessage);
                    expando.Error = errorList;

                    apiResponse.Message = "Validation error occured";
                    apiResponse.Data = expando;

                    _logger.LogWarn("Participate validation Error", string.Join(", ", errorList));

                    return BadRequest(apiResponse);
                }

                var dto = new ParticipationDto
                {
                    Id = Guid.NewGuid(),
                    EmailHash = StringUtility.Md5HashEncode(participate.Email.ToLower()),
                    Email = participate.Email,
                    SiteId = _siteService.GetSiteByCulture(participate.Culture)?.Id,
                    RetailerConsent = participate.RetailerConsent,
                    NewsletterOptin = participate.NewsletterOptin
                };

                var crmResponse = await _journeyService.ParticipateAsync(dto, participate.Culture);

                var site = _siteService.GetSiteByCulture(participate.Culture);

                expando.Description = "Your participate has been taken into account.";
                expando.ConsumerId = crmResponse.Item1 ? crmResponse.Item2 : string.Empty;
                expando.ParticipationId = dto.Id;

                apiResponse.Success = true;
                apiResponse.Message = crmResponse.Item1 ? "Participate successfull" :
                                            "Your participate has been considered but CRM sync has failed";
                apiResponse.Data = expando;

                _logger.LogTrace(apiResponse.Message, $"Ok: {dto.Id}");

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
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