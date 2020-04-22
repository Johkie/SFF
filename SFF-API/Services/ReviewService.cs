using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SFF_API.Models;
using SFF_API.Context;

namespace SFF_API.Services
{
    public interface IReviewService
    {
        // Rating
        public Task<RatingModel> AddRatingToRentalByIdAsync(int rentalId, RatingModel rating);
        public Task<RatingModel> GetRatingByIdAsync(int ratingId);
        public Task<RatingModel> DeleteRatingByIdAsync(int ratingId);

        // Trivia
        public Task<TriviaModel> AddTriviaToRentalByIdAsync(int rentalId, TriviaModel trivia);
        public Task<TriviaModel> GetTriviaByIdAsync(int triviaId);
        public Task<TriviaModel> DeleteTriviaByIdAsync(int triviaId);
    }

    public class ReviewService : IReviewService
    {
        private readonly SFFEntitiesContext _context;

        public ReviewService(SFFEntitiesContext context)
        {
            _context = context;
        }

        private async Task<RentalModel> GetRentalById(int rentalId)
        {
            if (!(_context.RentalLog.Any(r => r.Id == rentalId)))
            {
                throw new Exception($"No rental with id \"{rentalId}\" could be found.");
            }

            var rental = await _context.RentalLog
                .Include(m => m.Movie)
                .Include(r => r.Rating)
                .Include(r => r.Trivia)
                .FirstAsync(r => r.Id == rentalId);

            return rental;
        }

        public async Task<RatingModel> AddRatingToRentalByIdAsync(int rentalId, RatingModel rating)
        {
            var rental = await GetRentalById(rentalId);
            
            // Check wheater the rental already has a rating connected. Only one rating is allowed per rental.
            if (rental.Rating != null)
            {
                throw new Exception("A rating already exists on this object.");
            }

            rating.FilmClub = _context.FilmClubs.Find(rental.FilmClubModelId);
            rental.Rating = rating;

            rental.Movie.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return rating;
        }

        public async Task<RatingModel> DeleteRatingByIdAsync(int ratingId)
        {
            var rating = await GetRatingByIdAsync(ratingId);

            _context.MovieRatings.Remove(rating);
            await _context.SaveChangesAsync();

            return rating;
        }

        public async Task<RatingModel> GetRatingByIdAsync(int ratingId)
        {
            try
            {
                var rating = await _context.MovieRatings.FirstAsync(r => r.Id == ratingId);
                return rating;
            }
            catch
            {
                throw new Exception($"A rating with id \"{ratingId}\" was not found.");
            }
        }

        public async Task<TriviaModel> AddTriviaToRentalByIdAsync(int rentalId, TriviaModel trivia)
        {
            var rental = await GetRentalById(rentalId);

            // Check wheater the rental already has a trivia connected. Only one trivia is allowed per rental.
            if (rental.Trivia != null)
            {
                throw new Exception("A trivia already exists on this object.");
            }

            trivia.FilmClub = _context.FilmClubs.Find(rental.FilmClubModelId);
            rental.Trivia = trivia;

            rental.Movie.Trivias.Add(trivia);
            await _context.SaveChangesAsync();

            return trivia;
        }

        public async Task<TriviaModel> GetTriviaByIdAsync(int triviaId)
        {
            try
            {
                var trivia = await _context.MovieTrivias.FirstAsync(t => t.Id == triviaId);
                return trivia;
            }
            catch
            {
                throw new Exception($"A trivia with id \"{triviaId}\" was not found.");
            }
        }

        public async Task<TriviaModel> DeleteTriviaByIdAsync(int triviaId)
        {
            var trivia = await GetTriviaByIdAsync(triviaId);

            _context.MovieTrivias.Remove(trivia);
            await _context.SaveChangesAsync();

            return trivia;
        }
    }
}
