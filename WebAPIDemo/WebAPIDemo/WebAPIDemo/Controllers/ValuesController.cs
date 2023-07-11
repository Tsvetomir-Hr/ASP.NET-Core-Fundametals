namespace WebAPIDemo.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Dictionary<int,string> values = new Dictionary<int, string>()
        {
            {1, "value1" },
            {2, "value2" },
            {3, "value3" },
            {4, "value4" },
            {5, "value5" },

        };
        [HttpGet]
        [Route("GetValue")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get(int id)
        {
            if (values.ContainsKey(id))
            {
                return Ok(values[id]);
            }

            return NotFound();
            
        }
    }
}
