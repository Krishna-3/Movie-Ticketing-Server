﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Location>))]
        [ProducesResponseType(400)]
        public IActionResult GetLocaitons()
        {
            var locations = _locationRepository.GetLocations();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(locations);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateLocation([FromBody] Location location)
        {
            if (location == null)
                return BadRequest();

            if (_locationRepository.LocationExists(location.City))
            {
                ModelState.AddModelError("message", "Location already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_locationRepository.CreateLocation(location))
            {
                ModelState.AddModelError("message", "Location can not be created");
                return StatusCode(500, ModelState);
            }

            return Ok(new Response()
            {
                Success= "Successfully Location created"
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLocation( [FromBody] Location newLocation)
        {
            var locationId = newLocation.Id;
            var city= newLocation.City;

            if (locationId < 0)
                return BadRequest();

            if (city.IsNullOrEmpty())
                return BadRequest();

            if (!_locationRepository.LocationExists(locationId))
            {
                ModelState.AddModelError("message", "Location doesn't exists");
                return BadRequest(ModelState);
            }

            if (_locationRepository.LocationExists(city))
            {
                ModelState.AddModelError("message", "City already Exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var location = _locationRepository.GetLocation(locationId);
            location.City= city;

            if (!_locationRepository.UpdateLocation(location))
            {
                ModelState.AddModelError("message", "Location can not be updated");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{locationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLocation(int locationId)
        {
            if (locationId < 0)
                return BadRequest();

            if (!_locationRepository.LocationExists(locationId))
            {
                ModelState.AddModelError("message", "Location doesn't exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var location = _locationRepository.GetLocation(locationId);

            if (!_locationRepository.DeleteLocation(location))
            {
                ModelState.AddModelError("message", "Location can not be deleted");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
