using Admin.Service.REST.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Admin.Service.REST.Controllers
{
    [ApiController]
    [RequireHttps]
    //[EnableCors]
    public class PingController : ControllerBase
    {
        /// <summary>
        /// API Testing endpoint
        /// </summary>
        /// <remarks>Testing if the API is working properly</remarks>
        /// <response code="200">Ok</response>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Index()
        {
            return Redirect($"{Request.Scheme}://{Request.Host.ToUriComponent()}/swagger");
        }

        /// <summary>
        /// API Testing endpoint
        /// </summary>
        /// <remarks>Testing if the API is working properly</remarks>
        /// <response code="200">Ok</response>
        [HttpGet("[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Send()
        {
            dynamic expando = new ExpandoObject();

            expando.Description = $"Please go to {Request.Scheme}://{Request.Host.ToUriComponent()}/swagger for API Documentation";

            var apiResponse = new ApiResponse
            {
                Success = true,
                Message = "Service is UP!",
                Data = expando
            };
            return Ok(apiResponse);
        }
    }
}
