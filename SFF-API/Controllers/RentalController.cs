using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFF_API.Models;
using SFF_API.Context;
using Microsoft.EntityFrameworkCore;

namespace SFF_API.Controllers
{
    [Route("api/{filmClubId}/rentals")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly SFFEntitiesContext _context;

        public RentalController(SFFEntitiesContext context)
        {
            this._context = context;
        }

        [HttpPost("order")]
        public async Task<ActionResult<RentalModel>> RentalOrder(int filmClubId, RentalModel rental)
        {
            var filmClub = await _context.FilmClubs.FindAsync(filmClubId);

            rental.Movie = await _context.Movies.FindAsync(rental.MovieModelId);
            rental.RentalActive = true;

            filmClub.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRental), new { id = rental.Id }, rental); ;
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetAvailableMoviesForRent(int filmClubId)
        {
            var filmCLubRentals = await _context.FilmClubs
                .Include(r => r.Rentals)
                .Where(f => f.Id == filmClubId)
                .SelectMany(r => r.Rentals)
                .Where(m => m.RentalActive == true)
                .Select(m => m.Movie)
                .ToListAsync();
            
            var rentalList = await _context.RentalLog
                .Where(r => r.RentalActive == true)
                .GroupBy(r => r.MovieModelId )
                .Select(m => new { Id = m.Key, Count = m.Count() })
                .ToListAsync();

            var allMovies = await _context.Movies.ToListAsync();

            var unAvailableMovies = allMovies
                .Where(m =>
                    rentalList.Any(r =>
                        r.Id == m.Id && r.Count >= m.RentalLimit))
                .Union(filmCLubRentals)
                .ToList();

            var availableMovies = allMovies.Except(unAvailableMovies).Except(filmCLubRentals).ToList();

            return Ok(availableMovies);
        }

        // Get all trivia for movie ".../api/reviews/trivias/1"
        [HttpGet("order/{id}")]
        public async Task<ActionResult<IEnumerable<RatingModel>>> GetRental(int id)
        {
            var rental = await _context.RentalLog.FindAsync(id);

            return Ok(rental);
        }

        // Delete a trivia by id ".../api/reviews/trivias/1"
        [HttpDelete("trivias/{id}")]
        public async Task<ActionResult<RatingModel>> DeleteTrivia(int id)
        {
            var review = await _context.MovieTrivias.FindAsync(id);

            _context.MovieTrivias.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }


    }
}
