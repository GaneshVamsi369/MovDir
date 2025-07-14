namespace MovieDirectorWebClient.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public List<int> DirectorIds { get; set; }
    }
}
