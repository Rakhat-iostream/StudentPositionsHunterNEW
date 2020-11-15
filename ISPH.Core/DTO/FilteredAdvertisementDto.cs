using System;
using System.Collections.Generic;
using System.Text;

namespace ISPH.Core.DTO
{
   public class FilteredAdvertisementDto
    {
        public int? Salary { get; set; }
        public string CompanyName { get; set; }
        public string PositionName { get; set; }

        public bool HasValue(string com, string pos, int? sal)
        {
            return !string.IsNullOrEmpty(com) || !string.IsNullOrEmpty(pos) || sal.HasValue;
        }
    }
}
