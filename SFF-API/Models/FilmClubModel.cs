using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFF_API.Models
{
    public interface IFilmClubModel
    {

    }
    public class FilmClubModel : IFilmClubModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<RentalModel> Rentals { get; set; } = new List<RentalModel>();

        public class ErrorCode : IFilmClubModel
        {
            public string Title { get; set; }
            public int StatusCode { get; set; }
        }

        public class StatusCode : IFilmClubModel
        {
            public FilmClubModel FilmClub { get; set; }
            public string Status { get; set; }
        }
    }
}
