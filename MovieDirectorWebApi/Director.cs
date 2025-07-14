using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MovieDirectorWebApi
{
    [Table("Director")]
    public class Director
    {
        [Key]
        public int DirId {  get; set; }
        [Required]
        public string Name {  get; set; }
        public ICollection<Movie> Movies {  get; set; } = new List<Movie>();

    }
}
