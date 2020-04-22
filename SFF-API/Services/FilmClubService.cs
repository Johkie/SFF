using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SFF_API.Models;
using SFF_API.Context;

namespace SFF_API.Services
{
    public interface IFilmClubService
    {
        public Task<FilmClubModel> AddFilmClubToDatabase(FilmClubModel filmClubToAdd);
        public Task<FilmClubModel> ModifyDetailsForFilmClub(int filmClubId, FilmClubModel filmClub);
        public Task<IEnumerable<MovieModel>> GetMoviesRentedByFilmclubId(int filmClubId, bool filterOnlyActiveRentals = false);
        public Task<FilmClubModel> DeleteFilmClubFromDatabaseById(int filmClubId);

        // Extra
        public Task<FilmClubModel> GetFilmClubById(int filmClubId);
        public Task<IEnumerable<FilmClubModel>> GetAllFilmClubsInDatabase();
    }

    public class FilmClubService : IFilmClubService
    {
        private readonly SFFEntitiesContext _context;

        public FilmClubService(SFFEntitiesContext context)
        {
            _context = context;
        }

        public async Task<FilmClubModel> AddFilmClubToDatabase(FilmClubModel filmClubToAdd)
        {
            _context.FilmClubs.Add(filmClubToAdd);
            await _context.SaveChangesAsync();

            return filmClubToAdd;
        }

        public async Task<FilmClubModel> ModifyDetailsForFilmClub(int filmClubId, FilmClubModel filmClub)
        {
            if (filmClubId != filmClub.Id)
            {
                throw new Exception("Id's doesnt match");
            }

            _context.Entry(filmClub).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return filmClub;
        }

        public async Task<IEnumerable<MovieModel>> GetMoviesRentedByFilmclubId(int filmClubId, bool filterOnlyActiveRentals = false)
        {
            if (!(_context.FilmClubs.Any(f => f.Id == filmClubId)))
            {
                throw new Exception($"Filmclub with id \"{filmClubId}\" was not found");
            }

            var movies = await _context.RentalLog
                .Where(m => m.FilmClubModelId == filmClubId)
                .Where(m => m.RentalActive == true || m.RentalActive == filterOnlyActiveRentals)
                .Select(m => m.Movie)
                .ToListAsync();

            return movies;
        }

        public async Task<FilmClubModel> DeleteFilmClubFromDatabaseById(int filmClubId)
        {
            var filmClub = await GetFilmClubById(filmClubId);

            _context.FilmClubs.Remove(filmClub);
            await _context.SaveChangesAsync();

            return filmClub;
        }

        public async Task<FilmClubModel> GetFilmClubById(int filmClubId)
        {
           try
            {
                var filmClub = await _context.FilmClubs.FirstAsync(f => f.Id == filmClubId);
                return filmClub;
            }
            catch
            {
                throw new Exception($"Filmclub with id \"{filmClubId}\" was not found");
            }
        }

        public async Task<IEnumerable<FilmClubModel>> GetAllFilmClubsInDatabase()
        {
            var filmClubs = await _context.FilmClubs.ToListAsync();

            if (filmClubs.Count == 0)
            {
                throw new Exception("No filmClubs found");
            }

            return filmClubs;
        }
    }
}
