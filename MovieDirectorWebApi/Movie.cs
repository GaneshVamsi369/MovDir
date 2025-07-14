using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDirectorWebApi
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        public int MovieId {  get; set; }
        [Required]
        public string Title {  get; set; }
        //public int DirId {  get; set; }

        public ICollection<Director> Director { get; set; } = new List<Director>();
    }
}
