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
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> AddMovie(MovieModel movieToAdd)
        {
            try
            {
                var result = await _movieService.AddMovieToDatabase(movieToAdd);
                return CreatedAtAction(nameof(GetMovie), new { movieId = result.Id }, result.ToDto());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            try
            {
                var movies = await _movieService.GetAllMoviesInDatabase();
                return Ok(movies.ToDtoList());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int movieId)
        {
            try
            {
                var movie = await _movieService.GetMovieById(movieId);
                return Ok(movie.ToDto());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult<MovieDTO>> DeleteMovie(int movieId)
        {
            try
            {
                var result = await _movieService.DeleteMovieFromDatabaseById(movieId);
                return Ok(new { movie = result.ToDto(), status = "Sucessfully deleted" });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{movieId}")]
        public async Task<ActionResult<IMovieModel>> ChangeAllowedMovieRentals(int movieId, MovieModel modifiedMovie)
        {
            try
            {
                var result = await _movieService.ModifyAllowedNumberOfRentalsFor(movieId, modifiedMovie);
                return Ok(new { movie = result.ToDto(false), status = "Number of allowed rentals sucessfully changed." });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}