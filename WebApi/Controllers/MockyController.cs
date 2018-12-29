using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockyController : ControllerBase
    {
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.mocky.io");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                try
                {
                    var response = await client.GetAsync("v2/5c127054330000e133998f85");
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }
    }
}
