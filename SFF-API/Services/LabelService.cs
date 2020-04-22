using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SFF_API.Models;
using SFF_API.Context;

namespace SFF_API.Services
{
    public interface ILabelService
    {        
        /// <summary>
        /// Generates an XML label based on the id of the rental object.
        /// </summary>
        /// <param name="rentalId">The Id of the rental object.</param>
        /// <returns>Rental XML Label</returns>
        public Task<LabelModel> GetXmlLabelFor(int rentalId);
    }

    public class LabelService : ILabelService
    {
        private readonly SFFEntitiesContext _context;

        public LabelService(SFFEntitiesContext context)
        {
            _context = context;
        }

        public async Task<LabelModel> GetXmlLabelFor(int rentalId)
        {
            var rentalOrder = await _context.RentalLog
                    .Include(m => m.Movie)
                    .Where(r => r.Id == rentalId)
                    .Join(
                        _context.FilmClubs,
                        rental => rental.FilmClubModelId,
                        filmclub => filmclub.Id,
                        (rental, filmclub) => new LabelModel
                        {
                            MovieName = rental.Movie.Title,
                            Location = filmclub.Location,
                            Date = rental.RentalDate
                        })
                    .SingleAsync();

            return rentalOrder;
        }
    }
}
