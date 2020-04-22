namespace SFF_API.Models.DTO
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public FilmClubDTO FilmClub { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
