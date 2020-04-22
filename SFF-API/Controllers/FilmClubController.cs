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
    [Route("api/filmclubs")]
    [ApiController]
    public class FilmClubController : ControllerBase
    {
        private readonly IFilmClubService _filmClubService;

        public FilmClubController(IFilmClubService filmClubService)
        {
            this._filmClubService = filmClubService;
        }

        [HttpPost]
        public async Task<ActionResult<FilmClubDTO>> AddFilmClub(FilmClubModel filmCLubToAdd)
        {
            try
            {
                var result = await _filmClubService.AddFilmClubToDatabase(filmCLubToAdd);
                return CreatedAtAction(nameof(GetFilmClub), new { filmClubId = result.Id }, result.ToDto());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmClubDTO>>> GetFilmClubs()
        {
            try
            {
                var filmClubs = await _filmClubService.GetAllFilmClubsInDatabase();
                return Ok(filmClubs.ToDtoList());
            }
            catch(Exception e)
            {
                return NotFound(new { Title = e.Message, NotFound().StatusCode });
            }
        }

        [HttpGet("{filmClubId}")]
        public async Task<ActionResult<FilmClubDTO>> GetFilmClub(int filmClubId)
        {
            try
            {
                var filmClub = await _filmClubService.GetFilmClubById(filmClubId);
                return Ok(filmClub.ToDto());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{filmClubId}")]
        public async Task<ActionResult<FilmClubDTO>> DeleteFilmClub(int filmClubId)
        {
            try
            {
                var result = await _filmClubService.DeleteFilmClubFromDatabaseById(filmClubId);
                return Ok(new { filmClub = result.ToDto(), status = "Sucessfully deleted" });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{filmClubId}")]
        public async Task<ActionResult<FilmClubDTO>> ChangeFilmClubDetails(int filmClubId, FilmClubModel filmClub)
        {
            try
            {
                var result = await _filmClubService.ModifyDetailsForFilmClub(filmClubId, filmClub);
                return Ok(new { filmClub = result.ToDto(), status = "Sucessfully modified" });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{filmClubId}/rentals")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMoviesRentedByStudio(int filmClubId)
        {
            try
            {
                var movies = await _filmClubService.GetMoviesRentedByFilmclubId(filmClubId);
                return Ok(movies.ToDtoList(false));
            }
            catch
            {
                return NotFound();
            }
        }

        //public string ToUrl(this string text)
        //{
        //filmClub.FilmClubUrl = Regex.Replace(filmClub.Name, @"[,;.:_'*^¨~`´ ]", "-").ToLower();
        //    string s = Regex.Replace(text, @"[,;.:-_'*^¨~`´ ]", "-");
        //    return s;
        //}
    }
}
