using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProiectSoftbinator.EN.Models.Cause;
using ProiectSoftbinator.Services.CauseServices;

namespace ProiectSoftbinator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauseController : ControllerBase
    {
        private readonly ICauseService _causeService;

        public CauseController(ICauseService causeService)
        {
            _causeService = causeService;
        }

        [HttpGet("getAllCauses")]
        public async Task<ActionResult> GetAllCauses()
        {
            var causes = await _causeService.GetAll();

            return Ok(causes);
        }

        [HttpGet("{id}")]
       public async Task<ActionResult> GetById(int id)
        {
            var causes = await _causeService.GetById(id);

            return Ok(causes);
        }

        [HttpPost("add-cause")]
        public async Task<ActionResult> AddCause([FromBody] CausePostModel model)
        {
            await _causeService.AddCause(model);

            return Ok();
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateCause([FromBody] CausePutModel model, int id)
        {
            await _causeService.UpdateCause(model, id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCause(int id)
        {
            await _causeService.DeleteCause(id);

            return Ok();
        }

        [HttpGet("getMaxCauses")]

        public async Task<ActionResult> GetMaxCauseDet()
        {
            var cause = await _causeService.GetMaxCause();

            return Ok(cause);
            
        }
    }
}
