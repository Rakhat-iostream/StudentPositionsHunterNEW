using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class NewsDTO
    {
        public int NewsId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime? PublishDate { get; set; }
        [Required]
        public string Description { get; set; }

        public IFormFile File { get; set; }
    }
}
