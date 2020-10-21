using System.ComponentModel.DataAnnotations;

namespace ISPH.Core.Models
{
    public class Resume
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Path { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
