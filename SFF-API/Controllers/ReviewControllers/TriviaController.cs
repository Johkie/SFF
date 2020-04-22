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
    [Route("api/reviews/trivias")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public TriviaController(IReviewService context)
        {
            this._reviewService = context;
        }

        [HttpPost("{rentalId}")]
        public async Task<ActionResult<TriviaDTO>> AddTrivia(int rentalId, TriviaModel trivia)
        {
            try
            {
                var result = await _reviewService.AddTriviaToRentalByIdAsync(rentalId, trivia);
                return CreatedAtAction(nameof(GetTrivia), new { triviaId = result.Id }, result.ToDto());
            }
            catch (Exception e)
            {
                return BadRequest(new { Title = e.Message, BadRequest().StatusCode });
            }
        }

        // Get a trivia by id ".../api/reviews/trivias/1"
        [HttpGet("{triviaId}")]
        public async Task<ActionResult<TriviaDTO>> GetTrivia(int triviaId)
        {
            try
            {
                var trivia = await _reviewService.GetTriviaByIdAsync(triviaId);
                return Ok(trivia.ToDto());
            }
            catch (Exception e)
            {
                return NotFound(new { Title = e.Message, NotFound().StatusCode });
            }
        }

        // Delete a trivia by id ".../api/reviews/trivias/1"
        [HttpDelete("{triviaId}")]
        public async Task<ActionResult<TriviaDTO>> DeleteTrivia(int triviaId)
        {
            try
            {
                var result = await _reviewService.DeleteTriviaByIdAsync(triviaId);
                return Ok(new { trivia = result.ToDto(), status = "Succesfully deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Title = e.Message, BadRequest().StatusCode });
            }
        }
    }
}
