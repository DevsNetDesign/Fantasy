using Fantasy.BackEnd.Data;
using Fantasy.Shared.Entites.GeneralParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.BackEnd.Controllers.GeneralParameters
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContex _contex;

        public CountriesController(DataContex contex)
        {
            _contex = contex;
        }

        [HttpGet]
        public async Task<ActionResult> GetCountryAsync()
        {
            return Ok(await _contex.Countries.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCountryAsyncId(int id)
        {
            var country = await _contex.Countries.FindAsync(id);
            if (country == null)
            { return NotFound(); }

            return Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult> PostCountryAsync(Country country)
        {
            _contex.Add(country);
            await _contex.SaveChangesAsync();
            return Ok(country);
        }

        [HttpPut]
        public async Task<ActionResult> PuttCountryAsync(Country country)
        {
            var currentcountry = await _contex.Countries.FindAsync(country.Id);
            if (currentcountry == null)
            { return NotFound(); }

            currentcountry.Name = country.Name;
            _contex.Update(currentcountry);
            await _contex.SaveChangesAsync();
            return NoContent(); ;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountryAsync(int id)
        {
            var country = await _contex.Countries.FindAsync(id);
            if (country == null)
            { return NotFound(); }

            _contex.Remove(country);
            await _contex.SaveChangesAsync();
            return NoContent(); ;
        }
    }
}