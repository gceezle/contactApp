using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using contactApp.Contracts;
using contactApp.Data;
using contactApp.Models;

namespace contactApp.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var persons = _personRepository.Query().ToList();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            try
            {
                var person = await _personRepository.GetAsync(id);
                if (person == null)
                {
                    return NotFound();
                }
                return Ok(person);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonDTO data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    return BadRequest(new { message = errors });
                }
                var person = Mapper.Map<Person>(data);
                await _personRepository.InsertAsync(person);
                await _personRepository.Commit();

                return Created($"api/persons/{person.Id}", person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] PersonDTO data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    return BadRequest(new { message = errors });
                }
                var person = await _personRepository.GetAsync(id);
                if (person == null)
                {
                    return NotFound();
                }
                person.first_name = data.first_name;
                person.last_name = data.last_name;
                person.phone = data.phone;
                _personRepository.Update(person);
                await _personRepository.Commit();
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                var person = await _personRepository.GetAsync(id);
                if (person == null)
                {
                    return NotFound();
                }
                _personRepository.Delete(person);
                await _personRepository.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
