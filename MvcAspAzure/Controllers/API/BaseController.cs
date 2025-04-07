using Microsoft.AspNetCore.Mvc;

using MvcAspAzure.Infrastructure.Repository;

namespace MvcAspAzure.Controllers.API {
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : class {
        protected readonly IRepositoryAsync<T> _repository;

        protected BaseController(IRepositoryAsync<T> repository) {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAll() {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetById(int id) {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<ActionResult<T>> Create(T entity) {
            var created = await _repository.InsertAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created }, created);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(int id, T entity) {
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id) {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
