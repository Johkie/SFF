using Microsoft.EntityFrameworkCore;
using SFF_API.Models;

namespace SFF_API.Context
{
    public class SFFEntitiesContext : DbContext
    {
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<FilmClubModel> FilmClubs { get; set; }
        public DbSet<RatingModel> MovieRatings { get; set; }
        public DbSet<TriviaModel> MovieTrivias { get; set; }
        public DbSet<RentalModel> RentalLog { get; set; }

        public SFFEntitiesContext(DbContextOptions<SFFEntitiesContext> options) : base(options)
        {
        }
    }
}
