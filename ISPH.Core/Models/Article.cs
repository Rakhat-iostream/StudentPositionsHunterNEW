using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace ISPH.Core.Models
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public  string PublishDateString
        {
            get {
                return PublishDate.ToString("D", CultureInfo.CreateSpecificCulture("ru-Ru"));
            }
        }
        public string Description { get; set; }

        public string ImagePath { get; set; }
    }
}
