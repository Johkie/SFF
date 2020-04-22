using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFF_API.Models.DTO
{
    public static class DTOExtensions
    {
        public static MovieDTO ToDto(this MovieModel movie, bool includeReviews = true)
        {
            if (movie == null) return null;

            var dto = new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
            };

            if (includeReviews)
            {
                dto.Ratings = movie.Ratings.ToDtoList();
                dto.Trivias = movie.Trivias.ToDtoList();
            }

            return dto;
        }

        public static FilmClubDTO ToDto(this FilmClubModel filmClub)
        {
            if (filmClub == null) return null;
            
            return new FilmClubDTO
            {
                Id = filmClub.Id,
                Name = filmClub.Name,
                Location = filmClub.Location
            };
        }

        public static RatingDTO ToDto(this RatingModel rating)
        {
            if (rating == null) return null;
            
            return new RatingDTO
            {
                Id = rating.Id,
                Rating = rating.Rating,
                Comment = rating.Comment,
                FilmClub = rating.FilmClub.ToDto()
            };
        }
        public static TriviaDTO ToDto(this TriviaModel trivia)
        {
            if (trivia == null) return null; 

            return new TriviaDTO
            {
                Id = trivia.Id,
                Trivia = trivia.Trivia,
                FilmClub = trivia.FilmClub.ToDto()
            };
        }
        public static RentalDTO ToDto(this RentalModel rental)
        {
            if (rental == null) return null;

            return new RentalDTO
            {
                Id = rental.Id,
                Movie = rental.Movie.ToDto(),
                FilmClub = rental.FilmClub.ToDto(),
                RentalActive = rental.RentalActive,
                RentalDate = rental.RentalDate,
                Rating = rental.Rating.ToDto(),
                Trivia = rental.Trivia.ToDto()
            };
        }

        //**************************
        //******* TO DTO LISTS *****
        //**************************
        public static ICollection<MovieDTO> ToDtoList(this IEnumerable<MovieModel> movie, bool includeReviews = true)
        {
            var dtos = new List<MovieDTO>();

            foreach (var m in movie)
            {
                dtos.Add(m.ToDto(includeReviews));
            }

            return dtos;
        }
        public static ICollection<FilmClubDTO> ToDtoList(this IEnumerable<FilmClubModel> filmClubs)
        {
            var dtos = new List<FilmClubDTO>();

            foreach (var f in filmClubs)
            {
                dtos.Add(f.ToDto());
            }

            return dtos;
        }
        public static ICollection<RatingDTO> ToDtoList(this IEnumerable<RatingModel> ratings)
        {
            var dtos = new List<RatingDTO>();

            foreach (var r in ratings)
            {
                dtos.Add(r.ToDto());
            }

            return dtos;
        }
        public static ICollection<TriviaDTO> ToDtoList(this IEnumerable<TriviaModel> trivias)
        {
            var dtos = new List<TriviaDTO>();

            foreach (var t in trivias)
            {
                dtos.Add(t.ToDto());
            }

            return dtos;
        }
        public static ICollection<RentalDTO> ToDtoList(this IEnumerable<RentalModel> rentals)
        {
            var dtos = new List<RentalDTO>();

            foreach (var r in rentals)
            {
                dtos.Add(r.ToDto());
            }

            return dtos;
        }
    }
}
