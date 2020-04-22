using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFF_API.Models.DTO
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public FilmClubDTO FilmClub { get; set; }
        public MovieDTO Movie { get; set; }
        public DateTime RentalDate { get; set; }
        public bool RentalActive { get; set; }
        public RatingDTO Rating { get; set; }
        public TriviaDTO Trivia { get; set; }
    }
}
