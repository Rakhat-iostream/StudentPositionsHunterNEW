using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISPH.Core.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Role { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] SaltPassword { get; set; }
        public string Email { get; set; }
        public Resume Resume { get; set; }
    }
}
