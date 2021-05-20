using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoTaskApi.Contracts;
using TodoTaskApi.Models;
using TodoTaskApi.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoTaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskToDoController : ControllerBase
    {
        private readonly TaskToDoRepository taskRepo;
        public TaskToDoController()
        {
            taskRepo = new TaskToDoRepository();
        }

        // GET: api/<TaskToDoController>
        [HttpGet]
        public IActionResult Get()
        {
            var taskList = taskRepo.Listar();
            return Ok( taskList );
            
        }

        // GET api/<TaskToDoController>/5
        [HttpGet("{id}")]
        public IActionResult GetOne(Guid id)
        {
            var task = taskRepo.Consultar(id, null);
            if (task == null)
                return BadRequest();

            return Ok(task);
        }

        // POST api/<TaskToDoController>
        [HttpPost]
        public IActionResult Post(AddTaskRequest requestObj)
        {
            var taskModel = new TaskToDoModel() { Id = Guid.NewGuid(), Active = true, Nome = requestObj.Nome };
            taskRepo.Adicionar(taskModel);

            return Ok(taskModel);
        }

        // PUT api/<TaskToDoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TaskToDoModel taskModel)
        {
            var response = taskRepo.Alterar(taskModel, id);
            if (!response)
                return BadRequest();
         
            return Ok();
        }

        // DELETE api/<TaskToDoController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            taskRepo.Excluir(id);
        }
    }
}
