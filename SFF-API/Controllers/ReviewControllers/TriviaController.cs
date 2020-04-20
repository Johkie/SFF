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
    public class TriviaController : ControllerBase
    {
        private readonly SFFEntitiesContext _context;

        public TriviaController(SFFEntitiesContext context)
        {
            this._context = context;
        }

        [HttpPost("trivias/{movieId}/{filmClubId}")]
        public async Task<ActionResult<TriviaModel>> PostTrivia(int movieId, int filmClubId, TriviaModel trivia)
        {
            if (!(_context.FilmClubs.Any(f => f.Id == filmClubId)) || !(_context.Movies.Any(f => f.Id == movieId)))
            {
                return BadRequest( new MovieModel.ErrorCode { Title = "Filmclub not found", StatusCode = BadRequest().StatusCode });
            }

            var movie = _context.Movies.Find(movieId);
            trivia.FilmClub = _context.FilmClubs.Find(filmClubId);

            movie.Trivias.Add(trivia);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetTrivia), new { id = trivia.Id }, trivia);
        }

        // Get a trivia by id ".../api/reviews/trivias/1"
        [HttpGet("trivias/{id}")]
        public async Task<ActionResult<TriviaModel>> GetTrivia(int id)
        {
            var review = await _context.MovieTrivias.FindAsync(id);

            return Ok(review);
        }

        // Delete a trivia by id ".../api/reviews/trivias/1"
        [HttpDelete("trivias/{id}")]
        public async Task<ActionResult<TriviaModel>> DeleteTrivia(int id)
        {
            var review = await _context.MovieTrivias.FindAsync(id);
            
            _context.MovieTrivias.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }

        
    }
}
