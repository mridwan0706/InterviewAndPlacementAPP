using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;
        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var get = await repository.GetAsync();
            get = get.Where(e => e.IsDeleted.Equals(false)).ToList();
            return Ok(get);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var get = await repository.GetAsync(id);
            if (get == null)
            {
                return NotFound();
            }
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult>Post(TEntity entity)
        {
            var data = await repository.PostAsync(entity);
            if (data > 0)
            {
                return Ok("Data for Id=" + data + " Create.");
            }
            return BadRequest("Create Failed");
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity entity)
        {
            //var edit = await repository.GetAsync(id);
            
            entity.Update();
            var isSuccess = await repository.PullAsync(entity);
            if (isSuccess)
            {
                return Ok("Update Success.");
            }
            return BadRequest("Update Failed.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> HardDelete(int id)
        {
            var isSuccess = await repository.DeleteAsync(id);
            if (isSuccess)
            {
                return Ok("Delete Success.");
            }
            return BadRequest("Delete Failed");
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> SoftDelete (int id)
        {
            var entity = await repository.GetAsync(id);
            entity.Delete();
            var isSuccess = await repository.PullAsync(entity);
            if (isSuccess)
            {
                return Ok("Delete Success.");
            }

            return BadRequest("Delete Failed.");
        }

    }
}