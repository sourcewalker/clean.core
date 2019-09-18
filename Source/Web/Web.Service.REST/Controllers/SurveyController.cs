using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using Web.Service.REST.Models;

namespace Web.Service.REST.Controllers
{
    [ApiController]
    [RequireHttps]
    [Route("[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly ILoggingProvider _logger;

        public SurveyController(
            ISurveyService surveyService,
            ILoggingProvider logger)
        {
            _logger = logger;
            _surveyService = surveyService;
        }

        /// <summary>
        /// Retrieve votes between a time frame to json
        /// </summary>
        /// <param name="from">Start date of the export(string should be datetime convertible: 
        /// "yyyy-MM-ddTHH:mm:ss" or
        /// "yyyy-MM-ddTHH:mm" or
        /// "yyyy-MM-dd" or
        /// "yyyy/MM/dd" or
        /// "YYYY/MM")
        /// </param>
        /// <param name="to">End date of the export(string should be datetime convertible:
        /// "yyyy-MM-ddTHH:mm:ss" or
        /// "yyyy-MM-ddTHH:mm" or
        /// "yyyy-MM-dd" or
        /// "yyyy/MM/dd" or
        /// "YYYY/MM")
        /// </param>
        /// <remarks>
        /// Extract votes between start and end
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad request</response>
        [HttpGet("extract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Extract(string from = "", string to = "")
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
                var startDate = DateTimeOffset.MinValue;

                if (!string.IsNullOrEmpty(from))
                {
                    if (DateTimeOffset.TryParse(from, out var startOut))
                    {
                        startDate = startOut;
                    }
                    else
                    {
                        _logger.LogWarn("Extract Error", $"On {DateTimeOffset.UtcNow.ToString()}");

                        expando.Error = new List<string>() { "Start date invalid: Please enter a valid date" };
                        apiResponse.Message = "Validation error occured";
                        apiResponse.Data = expando;

                        return BadRequest(apiResponse);
                    }
                }

                var endDate = DateTimeOffset.MaxValue;

                if (!string.IsNullOrEmpty(to))
                {
                    if (DateTimeOffset.TryParse(to, out var endOut))
                    {
                        endDate = endOut;
                    }
                    else
                    {
                        _logger.LogWarn("Extract Error", $"On {DateTimeOffset.UtcNow.ToString()}");

                        expando.Error = "End date invalid: Please enter a valid date";
                        apiResponse.Message = "Validation error occured";
                        apiResponse.Data = expando;

                        return BadRequest(apiResponse);
                    }
                }

                if (endDate.TimeOfDay.TotalSeconds == 0)
                    endDate = new DateTimeOffset(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, TimeSpan.Zero);

                expando.Export = _surveyService.ExtractParticipation(startDate, endDate);

                apiResponse.Success = true;
                apiResponse.Message = "Extracted successfully";
                apiResponse.Data = expando;

                _logger.LogWarn("Extract Success", $"On {DateTime.UtcNow.ToString()}");

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
