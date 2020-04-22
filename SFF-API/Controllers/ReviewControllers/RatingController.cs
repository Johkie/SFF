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
    [Route("api/reviews/ratings")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public RatingController(IReviewService reviewService)
        {
            this._reviewService = reviewService;
        }

        [HttpPost("rental/{rentalId}")]
        public async Task<ActionResult<RatingDTO>> AddRating(int rentalId, RatingModel rating)
        {
           try
            {
                var result = await _reviewService.AddRatingToRentalByIdAsync(rentalId, rating);
                return CreatedAtAction(nameof(GetRating), new { ratingId = result.Id }, result.ToDto());
            }
            catch(Exception e)
            {
                return BadRequest(new { Title = e.Message, BadRequest().StatusCode });
            }
        }

        [HttpGet("{ratingId}")]
        public async Task<ActionResult<RatingDTO>> GetRating(int ratingId)
        {
            try
            {
                var rating = await _reviewService.GetRatingByIdAsync(ratingId);
                return Ok(rating.ToDto());
            }
            catch (Exception e)
            {
                return NotFound(new { Title = e.Message, NotFound().StatusCode });
            }
        }

        [HttpDelete("{ratingId}")]
        public async Task<ActionResult<RatingDTO>> DeleteRating(int ratingId)
        {
            try
            {
                var result = await _reviewService.DeleteRatingByIdAsync(ratingId);
                return Ok(new { trivia = result.ToDto(), status = "Succesfully deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Title = e.Message, BadRequest().StatusCode });
            }
        }
    }
}
