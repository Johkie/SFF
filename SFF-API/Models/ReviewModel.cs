using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFF_API.Models
{
    public interface IReviewModel
    {

    }
    public abstract class ReviewModel : IReviewModel
    {
        public int Id { get; set; }
        public int FilmClubModelId { get; set; }
        public FilmClubModel FilmClub { get; set; }
        public int MovieModelId { get; set; }
    }
    public class RatingModel : ReviewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

    public class TriviaModel : ReviewModel
    {
        public string Trivia { get; set; }
    }
    

}
