using Library.LeoTumbas.Contracts.DTOs;
using Library.LeoTumbas.Contracts.Entities;
using Library.LeoTumbas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.LeoTumbas.API.Controllers
{
    [ApiController]
    [Route("host/api")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet]
        [Route("people/all")]
        public async Task<IActionResult> GetAll()
        {
            IList<Person>? people = await _peopleService.GetAll();
            return people is null || people.Count.Equals(0) ? NotFound() : Ok(people);
        }

        [HttpGet]
        [Route("people")]
        public async Task<IActionResult> GetByCityName([FromQuery] string city)
        {
            IList<Person>? people = await _peopleService.GetByCityName(city);
            return people is null || people.Count.Equals(0) ? NotFound() : Ok(people);
        }

        [HttpGet]
        [Route("people/personId/{id}", Name = "GetById")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Person? person = await _peopleService.GetById(id);
            return person is null ? NotFound() : Ok(person);
        }

        [Authorize]
        [HttpPost("people")]
        public async Task<IActionResult> CreatePerson([FromQuery] PersonValidator personValidator)
        {
            ModelState.ClearValidationState(nameof(personValidator));
            if (TryValidateModel(personValidator, nameof(personValidator)))
            {
                Person? person = await _peopleService.CreatePerson(personValidator);
                return person is null ? NotFound() : CreatedAtRoute(routeName: "GetById", routeValues: new { person.Id }, value: person);
            }

            return ValidationProblem();
        }
    }
}
