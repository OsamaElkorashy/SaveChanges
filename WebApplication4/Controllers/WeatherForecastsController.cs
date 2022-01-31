#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication4;
using WebApplication4.Data;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly WebApplication4Context _context;

        public WeatherForecastsController(WebApplication4Context context)
        {
            _context = context;
        }

        // GET: api/WeatherForecasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecast()
        {
            return await _context.WeatherForecast.ToListAsync();
        }

        // GET: api/WeatherForecasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecast.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return weatherForecast;
        }

        // PUT: api/WeatherForecasts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherForecast(int id, WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id)
            {
                return BadRequest();
            }

            _context.Entry(weatherForecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WeatherForecasts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> PostWeatherForecast(int counter)
        {
            var clockSaveChanges = new Stopwatch();
            clockSaveChanges.Start();
            for (int i = 0; i < counter; i++)
            {
                _context.WeatherForecast.Add(new WeatherForecast
                {
                    Date = DateTime.Now,
                    IsMale = ((i + 1) % 2) == 0
                    ,
                    Name = "sdfsdgfsdfsdfsdfsdsdfsfdf",
                    Occupation = "adfafasdaddasdasdasdasddadsdasasasdasd"
                    ,
                    Salary = 20000,
                    TemperatureC = i,
                    Type = "dfgdfgdfgdfgdfgdfgdfgfgfdggdfg"
                });
            }
            await _context.SaveChangesAsync();
            //using var transaction = _context.Database.BeginTransaction();

            //try
            //{
            //    for (int i = 0; i < counter; i++)
            //    {
            //        _context.WeatherForecast.Add(new WeatherForecast
            //        {
            //            Date = DateTime.Now,
            //            IsMale = ((i + 1) % 2) == 0
            //            ,
            //            Name = "sdfsdgfsdfsdfsdfsdsdfsfdf",
            //            Occupation = "adfafasdaddasdasdasdasddadsdasasasdasd"
            //            ,
            //            Salary = 20000,
            //            TemperatureC = i,
            //            Type = "dfgdfgdfgdfgdfgdfgdfgfgfdggdfg"
            //        });
            //    }
            //    _context.SaveChanges();

            //    // Commit transaction if all commands succeed, transaction will auto-rollback
            //    // when disposed if either commands fails
            //    transaction.Commit();
            //}
            //catch (Exception)
            //{
            //    // TODO: Handle failure
            //}

            clockSaveChanges.Stop();
            return CreatedAtAction("GetWeatherForecast", new { time = clockSaveChanges.ElapsedMilliseconds});
        }

        // DELETE: api/WeatherForecasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecast.FindAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecast.Remove(weatherForecast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherForecastExists(int id)
        {
            return _context.WeatherForecast.Any(e => e.Id == id);
        }
    }
}
