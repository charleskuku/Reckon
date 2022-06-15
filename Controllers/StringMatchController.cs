using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ReckonStringMatching.Models;
using ReckonStringMatching.Contract;
using Microsoft.AspNetCore.Http;

namespace ReckonStringMatching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringMatchController : ControllerBase
    {
        private readonly IService _reckonService;
        public StringMatchController(IService reckonService)
        {
            _reckonService = reckonService;
        }


        [HttpGet]
        public async Task<ActionResult<SearchResultModel>> Get()
        {
            try
            {
                var result = await _reckonService.RunSearchTask();
                if (result is null) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
            

        }
    }
}
