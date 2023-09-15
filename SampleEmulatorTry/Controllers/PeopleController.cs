using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleEmulatorTry.Model;
using SampleEmulatorTry.Services;

namespace SampleEmulatorTry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonRepository _repository;

        public PeopleController(PersonRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonEntity entity)
        {
            await _repository.InsertAsync(entity);
            return Ok("Person inserted successfully.");
        }
    }
}
