using City.Api.Core;
using City.Api.Core.Dtos.City;
using City.Api.Services.CityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace City.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCity>>>> Get()
        {
            return Ok(await _cityService.GetAllCities());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCity>>> GetSingle(int id)
        {
            return Ok(await _cityService.GetByIdCity(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCity>>>> AddCity(AddCity newCity)
        {
            return Ok(await _cityService.AddCity(newCity));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCity>>> Update(UpdateCity updateCity)
        {
            var response = await _cityService.UpdateCity(updateCity);
            if (response.Data == null) 
                return NotFound();
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCity>>>> DeleteCity(int id)
        {
            var response = await _cityService.DeleteCity(id);
            if (response.Data == null)
                return NotFound(response);
            return Ok(response);
        }
    }
}
