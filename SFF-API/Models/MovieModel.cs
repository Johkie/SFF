using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFF_API.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public int MaxNrOfRentals { get; set; }
    }
}
