using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFF_API.Models
{
    public class RentalModel
    {
        public int Id { get; set; }
        public int FilmClubModelId { get; set; }
        public FilmClubModel FilmClub { get; set; }
        public int MovieModelId { get; set; }
        public MovieModel Movie { get; set; }
        public DateTime RentalDate { get; set; }
        public bool RentalActive { get; set; }
        public RatingModel Rating { get; set; }
        public TriviaModel Trivia { get; set; }
    }
}
