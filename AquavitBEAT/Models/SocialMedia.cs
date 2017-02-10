using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{

    public class SocialMedia
    {
        public int SocialMediaId { get; set; }

        [Display(Name = "Social media name")]
        public string Name { get; set; }
        public string Prefix { get; set; }
    }
    public class ArtistSocialMedia
    {
        public int ArtistSocialMediaId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Prefix { get; set; }
        public int ArtistId { get; set; }
        //public int SocialMediaId { get; set; }
        public virtual Artist Artist { get; set; } // Tydeligvis viktig med referanser!

    }
}