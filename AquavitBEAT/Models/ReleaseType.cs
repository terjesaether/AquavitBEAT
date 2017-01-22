using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{
    // EP, Single, Comp, Album, T-shirt, Poster
    public class ReleaseType
    {
        [Key]
        public int ReleaseTypeId { get; set; }

        [Required, Display(Name = "Release type (EP, Single, Album, Comp)")]
        public string ReleaseTypeName { get; set; }
    }
}