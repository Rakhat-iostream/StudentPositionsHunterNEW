using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ISPH.Core.Models
{
   public class PriceList
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PriceListItem> Items { get; set; }
    }
}
