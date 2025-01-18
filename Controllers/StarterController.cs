using Microsoft.AspNetCore.Mvc;

namespace starter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StarterController : ControllerBase
    {
        private static List<string> items = new List<string>();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id >= 0 && id < items.Count)
            {
                return Ok(items[id]);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            items.Add(value);
            return Ok(items);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            if (id >= 0 && id < items.Count)
            {
                items[id] = value;
                return Ok(items);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id >= 0 && id < items.Count)
            {
                items.RemoveAt(id);
                return Ok(items);
            }
            return NotFound();
        }
    }
}
