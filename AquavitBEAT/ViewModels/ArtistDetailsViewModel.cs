using AquavitBEAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquavitBEAT.ViewModels
{
    public class ArtistDetailsViewModel
    {
        AquavitBeatContext _db = new AquavitBeatContext();
        public ArtistDetailsViewModel(Artist artist)
        {
            _artist = artist;
        }

        private Artist _artist;

        public Artist Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }

        public List<string> GetSocialMediaIcons()
        {
            var list = new List<string>();
            foreach (var item in _db.SocialMedias)
            {
                list.Add(item.IconUri);
            }
            return list;
        }

    }
}