using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FizzBuzzController : ControllerBase
    {
        /// <summary>
        /// Prints Fizz and/or Buzz if the value is divisible by 2 and/or 3.
        /// </summary>
        /// <param name="value">Value to be tested. It must be in the range of 0-1000.</param>
        /// <returns>Fizz, Buzz, FizzBuzz or empty string.</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Value out of range.</response>
        [HttpGet("{value}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<string> FizzBuzz(int value)
        {
            try
            {
                return Methods.FizzBuzz(value);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
