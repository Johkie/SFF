using System;
using System.ComponentModel.DataAnnotations;

namespace SFF_API.Models
{
    public class RatingModel : ReviewModel
    {
        [Required, Range(1, 5)]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
