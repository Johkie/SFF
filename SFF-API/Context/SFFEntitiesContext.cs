using Microsoft.EntityFrameworkCore;
using SFF_API.Models;

namespace SFF_API.Context
{
    public class SFFEntitiesContext : DbContext
    {
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<StudioModel> Studios { get; set; }

        public SFFEntitiesContext(DbContextOptions<SFFEntitiesContext> options) : base(options)
        {
        }
    }
}
