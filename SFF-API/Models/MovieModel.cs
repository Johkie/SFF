using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SFF_API.Models
{
    public interface IMovieModel
    {

    }
    public class MovieModel : IMovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int RentalLimit { get; set; }
        public ICollection<RatingModel> Ratings { get; set; } = new List<RatingModel>();
        public ICollection<TriviaModel> Trivias { get; set; } = new List<TriviaModel>();

        public class ErrorCode : IMovieModel
        {
            public string Title { get; set; }
            public int StatusCode { get; set; }
        }

        public class StatusCode : IMovieModel
        {
            public MovieModel Movie { get; set; }
            public string Status { get; set; }
        }
    }

    public class GenreModel
    {
        public const string Action = "Action";
        public const string Adventure = "Adventure";
        public const string Thriller = "Thriller";
        public const string Drama = "Drama";
    }
}
