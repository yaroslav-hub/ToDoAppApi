using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        ToDoRepository repository = new();
        // GET: api/<ValuesController>
        [HttpGet]
        public List<ToDoDto> Get()
        {
            return repository.GetAll();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public int Post([FromBody] ToDoDto toDoDto)
        {
            return repository.Create(toDoDto);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ToDoDto toDoDto)
        {
            repository.Update(id, toDoDto);
        }
    }
}
