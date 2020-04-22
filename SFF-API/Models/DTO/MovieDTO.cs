using System;
using System.Collections.Generic;

namespace SFF_API.Models.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public ICollection<RatingDTO> Ratings { get; set; } = new List<RatingDTO>();
        public ICollection<TriviaDTO> Trivias { get; set; } = new List<TriviaDTO>();
    }
}
