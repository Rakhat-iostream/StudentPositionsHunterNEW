using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class CompanyDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
