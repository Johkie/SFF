using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SFF_API.Models;
using SFF_API.Context;

namespace SFF_API.Services
{
    public interface IRentalService
    {
        public Task<RentalModel> AddRentalOrderToDatabase(int filmClubId, int movieId);
        public Task<RentalModel> MarkRentalAsReturnedFromId(int rentalId);
        public Task<IEnumerable<MovieModel>> GetMoviesAvailableForRentalFromFilmclubId(int filmClubId);

        // Extra
        public Task<RentalModel> GetRentalFromId(int rentalId);
        public Task<RentalModel> DeleteRentalFromDatabaseById(int rentalId);
    }

    public class RentalService : IRentalService
    {
        private readonly SFFEntitiesContext _context;
        private readonly IFilmClubService _filmClubService;
        private readonly IMovieService _movieService;

        public RentalService(SFFEntitiesContext context, IFilmClubService filmClubService, IMovieService movieService)
        {
            _context = context;
            _filmClubService = filmClubService;
            _movieService = movieService;
        }

        private async Task<bool> IsRentalValid(int filmClubId, int movieId)
        {
            // Check if an active rental of the movie exists from the filmclub 
            var rentalExist =  _context.RentalLog
                .Any(r => 
                    r.FilmClubModelId == filmClubId &&
                    r.MovieModelId == movieId &&
                    r.RentalActive == true
                    );

            // Check if movie has reached its rental limit
            var nrOfActiveRentals = await _movieService.GetNrOfActiveRentalsForMovieId(movieId);
            var movieAvailable = _context.Movies
                .Any(m =>
                    m.Id == movieId &&
                    m.RentalLimit > nrOfActiveRentals
                    );
            
            return (!rentalExist && movieAvailable) ? true : false;
        }
        private async Task<RentalModel> GenerateRentalOrderFrom(int movieId)
        {
            try
            {
                var rental = new RentalModel
                {
                    Movie = await _movieService.GetMovieById(movieId),
                    RentalActive = true,
                    RentalDate = DateTime.Now
                };
                
                return rental;
            }
            catch(Exception e)
            {
                throw e;
            }

        }
        public async Task<RentalModel> AddRentalOrderToDatabase(int filmClubId, int movieId)
        {
            try
            {
                if (!await IsRentalValid(filmClubId, movieId))
                {
                    throw new Exception("Rental is not valid");
                }

                var filmClub = await _filmClubService.GetFilmClubById(filmClubId);
                var rentalOrder = await GenerateRentalOrderFrom(movieId);

                filmClub.Rentals.Add(rentalOrder);
                await _context.SaveChangesAsync();

                return rentalOrder;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<MovieModel>> GetMoviesAvailableForRentalFromFilmclubId(int filmClubId)
        {
            var filmCLubActiveRentals = await _filmClubService.GetMoviesRentedByFilmclubId(filmClubId, true);
            var allMovies = await _movieService.GetAllMoviesInDatabase();

            // Get movies who has reached rentallimit
            var unAvailableMovies = await _context.RentalLog
                .Where(r => r.RentalActive == true)
                .GroupBy(r => r.MovieModelId)
                .Select(r => new { Id = r.Key, ActiveRentals = r.Count() })
                .Join(
                    _context.Movies,
                    g => g.Id,
                    m => m.Id,
                    (g, m) => new
                    {
                        Movie = m,
                        g.ActiveRentals,
                    })
                .Where(m => m.ActiveRentals >= m.Movie.RentalLimit)
                .Select(m => m.Movie)
                .ToListAsync();

            // Remove movies that the studio currently is renting as well as the unavailable ones
            var availableMovies = allMovies
                .Except(unAvailableMovies)
                .Except(filmCLubActiveRentals)
                .ToList();

            return availableMovies;
        }

        public async Task<RentalModel> GetRentalFromId(int rentalId)
        {
            try
            {
                var rentalOrder = await _context.RentalLog
                    .Include(r => r.FilmClub)
                    .Include(r => r.Movie)
                    .Include(r => r.Rating)
                    .Include(r => r.Trivia)
                    .FirstAsync(r => r.Id == rentalId);
                return rentalOrder;
            }
            catch
            {
                throw new Exception($"Rental with id \"{rentalId}\" not found.");
            }
        }

        public async Task<RentalModel> MarkRentalAsReturnedFromId(int rentalId)
        {
            var rentalOrder = await GetRentalFromId(rentalId);

            if (rentalOrder.RentalActive == false)
            {
                throw new Exception("Rental is already marked as returned");
            }

            rentalOrder.RentalActive = false;
            await _context.SaveChangesAsync();

            return rentalOrder;
        }

        public async Task<RentalModel> DeleteRentalFromDatabaseById(int rentalId)
        {
            var rentalToDelete = await GetRentalFromId(rentalId);

            _context.RentalLog.Remove(rentalToDelete);
            await _context.SaveChangesAsync();

            return rentalToDelete;
        }
    }
}
