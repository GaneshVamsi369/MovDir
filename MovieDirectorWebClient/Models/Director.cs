namespace MovieDirectorWebClient.Models
{
    public class Director
    {
        public int DirId { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
