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
    [Route("api/reviews")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly SFFEntitiesContext _context;

        public RatingController(SFFEntitiesContext context)
        {
            this._context = context;
        }

        [HttpPost("ratings/{movieId}/{filmClubId}")]
        public async Task<ActionResult<RatingModel>> PostRating(int movieId, int filmClubId, RatingModel rating)
        {
            if (!(_context.FilmClubs.Any(f => f.Id == filmClubId)) || !(_context.Movies.Any(f => f.Id == movieId)))
            {
                return BadRequest(new MovieModel.ErrorCode { Title = "Filmclub not found", StatusCode = BadRequest().StatusCode });
            }

            var movie = _context.Movies.Find(movieId);
            rating.FilmClub = _context.FilmClubs.Find(filmClubId);

            movie.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating);
        }

        // Get a rating by id ".../api/reviews/trivias/1"
        [HttpGet("ratings/{id}")]
        public async Task<ActionResult<RatingModel>> GetRating(int id)
        {
            var rating = await _context.MovieRatings.FindAsync(id);

            return Ok(rating);
        }

        // Delete a rating by id ".../api/reviews/trivias/1"
        [HttpDelete("ratings/{id}")]
        public async Task<ActionResult<RatingModel>> DeleteRating(int id)
        {
            var review = await _context.MovieRatings.FindAsync(id);

            _context.MovieRatings.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }


    }
}
