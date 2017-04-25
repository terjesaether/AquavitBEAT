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

        public ArtistDetailsViewModel(int id)
        {
            _artist = _db.Artists.Find(id);
            Releases = GetReleases(id);
        }

        private Artist _artist;

        public Artist Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }

        public IEnumerable<Release> Releases { get; set; }

        public IEnumerable<Release> GetReleases(int id)
        {
           var artistSongs = _db.SongToArtists.Where(s => s.ArtistId == id).Select(s => s.Song).ToList();
            
            var list = new List<Release>();

            foreach (var release in _db.Releases.ToList())
            {
                foreach (var song in release.HasSongs)
                {
                    if (artistSongs.Contains(song) && !list.Contains(release))
                    {
                        list.Add(release);
                    }
                }
            }
            return list;
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