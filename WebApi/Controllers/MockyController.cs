using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockyController : ControllerBase
    {
        private readonly IMockyService mocky;

        public MockyController(IMockyService mocky)
        {
            this.mocky = mocky;
        }

        /// <summary>
        /// Requests http://www.mocky.io/v2/5c127054330000e133998f85 and returns its response content.
        /// </summary>
        /// <returns>Content of the response.</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Failure.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> RequestMocky()
        {
            try
            {
                return await mocky.GetAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
