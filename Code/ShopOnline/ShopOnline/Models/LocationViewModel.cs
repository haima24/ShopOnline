using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class LocationViewModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int LocationLevel { get; set; }
        public Nullable<int> ParentId { get; set; }
    }
}