using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{
    public class BuyLink
    {
        public Guid Id { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }
        public virtual Release Release { get; set; }
    }
}