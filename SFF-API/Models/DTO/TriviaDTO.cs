namespace SFF_API.Models.DTO
{
    public class TriviaDTO
    {
        public int Id { get; set; }

        public string Trivia { get; set; }
        public FilmClubDTO FilmClub { get; set; }
    }
}
