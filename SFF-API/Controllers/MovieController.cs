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
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly SFFEntitiesContext _context;

        public MovieController(SFFEntitiesContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task<ActionResult<IMovieModel>> AddMovie(MovieModel movie)
        {
            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetMovies()
        {
            return await _context.Movies.Include(r => r.Ratings)
                .Include(r => r.Trivias).ThenInclude(f => f.FilmClub).
                ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IMovieModel>> GetMovie(int id)
        {
            var movie = await _context.Movies
                .Include(r => r.Ratings)
                .Include(r => r.Trivias).ThenInclude(f => f.FilmClub)
                .FirstAsync(m => m.Id == id);

            // If movie not found
            if (movie == null) return NotFound(new MovieModel.ErrorCode { Title = "Movie not found", StatusCode = NotFound().StatusCode });

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IMovieModel>> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null) return NotFound(new MovieModel.ErrorCode { Title = "Movie not found", StatusCode = NotFound().StatusCode });

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(new MovieModel.StatusCode { Movie = movie, Status = "Deleted" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IMovieModel>> ChangeMovieDetails(int id, MovieModel movie)
        {
            if (id != movie.Id) return BadRequest(new MovieModel.ErrorCode { Title = "Id's doesn't match", StatusCode = BadRequest().StatusCode });
         
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new MovieModel.StatusCode { Movie = movie, Status = "Information changed" });
        }


        /************************************************************************************************************************/
        /************************************************************************************************************************/

        // Get all trivia for movie ".../api/reviews/trivias/1"
        [HttpGet("{id}/reviews/trivia")]
        public async Task<ActionResult<IEnumerable<TriviaModel>>> GetTriviaForMovie(int id)
        {
            var review = await _context.Movies
                .Include(t => t.Trivias)
                .FirstAsync(m => m.Id == id);
                
            return Ok(review.Trivias);
        }
    }
}

//[HttpDelete("{ids}")]
//public async Task<ActionResult<IMovieModel>> DeleteMovie(string ids)
//{
//    var separatedIds = ids.Split(new char[] { ',' });
//    List<int> idList = separatedIds.Select(s => int.Parse(s)).ToList();

//    var movies = await _context.Movies.Where(m => idList.Contains(m.Id)).ToListAsync();

//    if (movies.Count == 0)
//    {
//        return BadRequest(new MovieModel.ErrorCode { Title = "Movies not found", StatusCode = "400" });
//    }

//    // Remove movies
//    _context.Movies.RemoveRange(movies);
//    await _context.SaveChangesAsync();

//    // Create deleted movies status codes
//    var deletedMovies = new List<MovieModel.DeletedCode>();
//    foreach (var movie in movies)
//    {
//        deletedMovies.Add(new MovieModel.DeletedCode { Movie= movie, StatusCode = "Deleted" });
//    }

//    return Ok(deletedMovies);  
//}