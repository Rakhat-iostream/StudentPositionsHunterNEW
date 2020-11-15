using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ISPH.Core.DTO
{
    public class ArticleDto
    {
        public int ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        public string PublishDateString { get { return PublishDate.ToString("D", CultureInfo.CreateSpecificCulture("ru-RU")); } }
        [Required]
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string ImagePath { get; set; }
    }
}
