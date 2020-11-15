using ISPH.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISPH.Core.DTO
{
    public class PositionDto
    {
        public int PositionId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Amount { get; set; }
        public IList<Advertisement> Advertisements { get; set; }
    }
}
