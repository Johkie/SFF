using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SFF_API.Models;
using SFF_API.Context;

namespace SFF_API.Services
{
    public interface IMovieService
    {
        public Task<MovieModel> AddMovieToDatabase(MovieModel movieToAdd);
        public Task<MovieModel> DeleteMovieFromDatabaseById(int movieId);
        public Task<MovieModel> ModifyAllowedNumberOfRentalsFor(int movieId, MovieModel movie);
        public Task<MovieModel> GetMovieById(int movieId);
        public Task<IEnumerable<MovieModel>> GetAllMoviesInDatabase();
        public Task<int> GetNrOfActiveRentalsForMovieId(int movieId);
    }

    public class MovieService : IMovieService
    {
        private readonly SFFEntitiesContext _context;

        public MovieService(SFFEntitiesContext context)
        {
            _context = context;
        }

        public async Task<MovieModel> AddMovieToDatabase(MovieModel movieToAdd)
        {
            _context.Movies.Add(movieToAdd);
            await _context.SaveChangesAsync();

            return movieToAdd;
        }

        public async Task<MovieModel> DeleteMovieFromDatabaseById(int movieId)
        {
            var movieToDelete = await GetMovieById(movieId);
            
            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();

            return movieToDelete;
        }

        public async Task<IEnumerable<MovieModel>> GetAllMoviesInDatabase()
        {
            var movies = await _context.Movies.Include(r => r.Ratings)
                .Include(r => r.Trivias).ThenInclude(f => f.FilmClub).
                ToListAsync();
           
            return movies;
        }

        public async Task<MovieModel> GetMovieById(int movieId)
        {
            try 
            { 
                var movie = await _context.Movies
                    .Include(r => r.Ratings)
                    .Include(r => r.Trivias).ThenInclude(f => f.FilmClub)
                    .FirstAsync(m => m.Id == movieId);

                return movie;
            }
            catch
            {
                throw new Exception($"Movie with id \"{movieId}\" not found.");
            }
        }

        public async Task<int> GetNrOfActiveRentalsForMovieId(int movieId)
        {
            var activeRentals = await _context.RentalLog
                .Where(r =>
                    r.RentalActive == true &&
                    r.MovieModelId == movieId)
                .ToListAsync();

            return activeRentals.Count();
        }

        public async Task<MovieModel> ModifyAllowedNumberOfRentalsFor(int movieId, MovieModel modifiedMovie)
        {
            if (movieId != modifiedMovie.Id)
            {
                throw new Exception("Id's doesnt match");
            }
            
            var movie = await GetMovieById(movieId);

            movie.RentalLimit = modifiedMovie.RentalLimit;
            await _context.SaveChangesAsync();

            return movie;
        }
    }
}
