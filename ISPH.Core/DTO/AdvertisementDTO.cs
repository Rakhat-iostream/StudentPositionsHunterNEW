﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class AdvertisementDTO
    {   
        
        public int AdvertisementId { get; set; }
        
        public int EmployerId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public uint? Salary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PositionName { get; set; }
    }
}
