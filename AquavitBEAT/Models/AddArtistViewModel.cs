using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{
    public class AddArtistViewModel
    {
        public AddArtistViewModel()
        {
            SocialMediaList = new List<SocialMedia>();
        }
        public AddArtistViewModel(List<SocialMedia> socialMediaList)
        {
            SocialMediaList = socialMediaList;
        }
        public Artist Artist { get; set; }
        public List<SocialMedia> SocialMediaList { get; set; }
    }
}