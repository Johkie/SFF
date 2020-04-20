using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFF_API.Models;
using SFF_API.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace SFF_API.Controllers
{
    [Route("api/filmclubs")]
    [ApiController]
    public class FilmClubController : ControllerBase
    {
        private readonly SFFEntitiesContext _context;

        public FilmClubController(SFFEntitiesContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task<ActionResult<IFilmClubModel>> PostFilmClub(FilmClubModel filmClub)
        {
            _context.FilmClubs.Add(filmClub);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFilmClub), new { id = filmClub.Id }, filmClub);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IFilmClubModel>>> GetFilmClubs()
        {
            return await _context.FilmClubs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IFilmClubModel>> GetFilmClub(int id)
        {
            var filmClub = await _context.FilmClubs.FindAsync(id);

            //If filmClub not found
            if (filmClub == null) return NotFound(new FilmClubModel.ErrorCode { Title = "Filmclub not found", StatusCode = NotFound().StatusCode });

            return Ok(filmClub);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IFilmClubModel>> DeleteFilmClub(int id)
        {
            var filmClub = await _context.FilmClubs.FindAsync(id);

            if (filmClub == null) return NotFound(new FilmClubModel.ErrorCode { Title = "Filmclub not found", StatusCode = NotFound().StatusCode });

            _context.FilmClubs.Remove(filmClub);
            await _context.SaveChangesAsync();

            return Ok(new FilmClubModel.StatusCode { FilmClub = filmClub, Status = "Deleted" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IFilmClubModel>> ChangeFilmClubDetails(int id, FilmClubModel filmClub)
        {
            if (id != filmClub.Id)
            {
                return BadRequest(new FilmClubModel.ErrorCode { Title = "Id's doesn't match", StatusCode = BadRequest().StatusCode });
            }

            _context.Entry(filmClub).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new FilmClubModel.StatusCode { FilmClub = filmClub, Status = "Information changed" });
        }

        // ###################################################################################################
        // ###################################################################################################
        // ###################################################################################################

        [HttpGet("{id}/rentals")]
        public async Task<ActionResult<IEnumerable<IMovieModel>>> GetMoviesRentedByStudio(int filmClubId)
        {
            var movies = await _context.RentalLog
                .Where(m => m.FilmClubModelId == filmClubId)
                .Where(m => m.RentalActive == true)
                .Select(m => m.Movie)
                .ToListAsync();

            return movies;
        }

            

        //public string ToUrl(this string text)
        //{
        //filmClub.FilmClubUrl = Regex.Replace(filmClub.Name, @"[,;.:_'*^¨~`´ ]", "-").ToLower();
        //    string s = Regex.Replace(text, @"[,;.:-_'*^¨~`´ ]", "-");
        //    return s;
        //}
    }
}
