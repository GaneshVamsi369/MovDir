namespace MovieDirectorWebApi.DTO
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public List<int> DirectorIds { get; set; }    
    }
}
