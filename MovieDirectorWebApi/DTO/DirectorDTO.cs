namespace MovieDirectorWebApi.DTO
{
    public class DirectorDTO
    {
        public int DirId { get; set; }
        public string Name { get; set; }
        public List<MovieDTO> Movies { get; set; } = new List<MovieDTO>();
    }
}
