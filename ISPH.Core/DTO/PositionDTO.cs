using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class PositionDTO
    {
        public int PositionId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
