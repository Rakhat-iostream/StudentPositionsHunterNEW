using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ISPH.Core.Models
{
    public class PriceListItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PriceListId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
    }
}
