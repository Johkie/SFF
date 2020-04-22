using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SFF_API.Models;
using SFF_API.Models.DTO;
using SFF_API.Services;

namespace SFF_API.Controllers
{
    [Route("api/rentals/")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            this._rentalService = rentalService;
        }

        [HttpPost("order/{filmClubId}/{movieId}")]
        public async Task<ActionResult<RentalDTO>> RentalMovieOrder(int filmClubId, int movieId)
        {
            try
            {
                var rentalOrder = await _rentalService.AddRentalOrderToDatabase(filmClubId, movieId);
                return CreatedAtAction(nameof(GetRental), new { rentalId = rentalOrder.Id }, rentalOrder.ToDto());
            }
            catch(Exception e)
            {
                return BadRequest(new { title = e.Message, BadRequest().StatusCode });
            }
        }

        [HttpPut("{rentalId}/return")]
        public async Task<ActionResult<RentalDTO>> ReturnMovieOrder(int rentalId)
        {
            try
            {
                var result = await _rentalService.MarkRentalAsReturnedFromId(rentalId);
                return Ok( new { rental = result.ToDto(), status = "Rental has been succesfully marked as returned" });
            }
            catch(Exception e)
            {
                return BadRequest(new { title = e.Message, BadRequest().StatusCode });
            }
        }

        [HttpGet("{filmClubId}/available")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetAvailableMoviesForRent(int filmClubId)
        {
            try
            {
                var movies = await _rentalService.GetMoviesAvailableForRentalFromFilmclubId(filmClubId);
                return Ok(movies.ToDtoList(false));
            }
            catch(Exception e)
            {
                return BadRequest(new { title = e.Message, BadRequest().StatusCode });
            }
        }

        [HttpGet("{rentalId}")]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetRental(int rentalId)
        {
            try
            {
                var rentalOrder = await _rentalService.GetRentalFromId(rentalId);
                return Ok(rentalOrder.ToDto());
            }
            catch (Exception e)
            {
                return NotFound(new { title = e.Message, NotFound().StatusCode });
            }
        }

        [HttpDelete("{rentalId}")]
        public async Task<ActionResult<RentalDTO>> DeleteRental(int rentalId)
        {
            try
            {
                var result = await _rentalService.DeleteRentalFromDatabaseById(rentalId);
                return Ok(new { rental = result.ToDto(), status = "Succesfully deleted" });
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
