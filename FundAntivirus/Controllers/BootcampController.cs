using Microsoft.AspNetCore.Mvc;
using BootcampApi.Models;
using BootcampApi.Services;

namespace BootcampApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BootcampsController : ControllerBase
    {
        private readonly IBootcampService _bootcampService;
        
        public BootcampsController(IBootcampService bootcampService)
        { 
            _bootcampService = bootcampService; 
        }

        // GET: api/bootcamps 
        [HttpGet]
        public ActionResult<IEnumerable<Bootcamp>> GetAll()
        { 
            return Ok(_bootcampService.GetAll()); 
        }

        // GET: api/bootcamps/5 
        [HttpGet("{id}")]
        public ActionResult<Bootcamp> Get(int id)
        {
            var bootcamp = _bootcampService.GetById(id);
            if (bootcamp == null)
                return NotFound();
            return Ok(bootcamp);
        }

        // POST: api/bootcamps 
        [HttpPost]
        public ActionResult Create([FromBody] Bootcamp bootcamp)
        {
            _bootcampService.Create(bootcamp);
            return CreatedAtAction(nameof(Get), new { id = bootcamp.Id }, bootcamp);
        }

        // PUT: api/bootcamps/5 
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Bootcamp bootcamp)
        {
            _bootcampService.Update(id, bootcamp);
            return NoContent();
        }

        // DELETE: api/bootcamps/5 
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        { 
            _bootcampService.Delete(id); 
            return NoContent(); 
        }
    }
}
