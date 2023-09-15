using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleEmulatorTry.Model;
using SampleEmulatorTry.Services;

namespace SampleEmulatorTry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyEntitiesController : ControllerBase
    {
        private readonly MyEntityRepository _repository;

        public MyEntitiesController(MyEntityRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MyEntity entity)
        {
            var createdEntity = await _repository.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { partitionKey = createdEntity.PartitionKey, rowKey = createdEntity.RowKey }, createdEntity);
        }

        [HttpGet("{partitionKey}/{rowKey}")]
        public async Task<IActionResult> GetById(string partitionKey, string rowKey)
        {
            var entity = await _repository.GetByIdAsync(partitionKey, rowKey);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{partitionKey}/{rowKey}")]
        public async Task<IActionResult> Update(string partitionKey, string rowKey, [FromBody] MyEntity entity)
        {
            if (partitionKey != entity.PartitionKey || rowKey != entity.RowKey)
            {
                return BadRequest();
            }

            var updatedEntity = await _repository.UpdateAsync(entity);
            return Ok(updatedEntity);
        }

        [HttpDelete("{partitionKey}/{rowKey}")]
        public async Task<IActionResult> Delete(string partitionKey, string rowKey)
        {
            await _repository.DeleteAsync(partitionKey, rowKey);
            return NoContent();
        }
    }
}
