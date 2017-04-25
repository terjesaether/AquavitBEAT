using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using AquavitBEAT.Migrations;

namespace AquavitBEAT.Models
{
    public class Artist
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        public Artist()
        {
            HasArtworks = new List<Artwork>();
            HasReleases = new List<Release>();
            HasSongs = new List<Song>();
            ArtistSocialMedias = new List<ArtistSocialMedia>();
        }
        [Required]
        public int ArtistId { get; set; }

        [Required, Display(Name = "Artist name")]
        public string ArtistName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string About { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        [Display(Name = "Profile image")]
        public string ProfileImgUrl { get; set; }
        public virtual List<ArtistSocialMedia> ArtistSocialMedias { get; set; }
        public virtual List<Song> HasSongs { get; set; }
        public virtual List<Release> HasReleases { get; set; }
        public virtual ICollection<Artwork> HasArtworks { get; set; }

        public virtual ICollection<ReleaseToArtist> ReleasesToArtists { get; set; }
        public virtual ICollection<SongToArtist> SongsToArtists { get; set; }
        //public virtual ICollection<ArtistToSocialMedia> ArtistToSocialMedias { get; set; } Fjernes

        private List<ArtistSocialMedia> InitSocialMedias()
        {
            var list = new List<ArtistSocialMedia>();
            foreach (var s in _db.SocialMedias)
            {
                var newSos = new ArtistSocialMedia
                {
                    Url = "",
                    Name = s.Name,
                    Prefix = "http://www." + s.Name + ".com/"
                };
                //yield return newSos;
                list.Add(newSos);
            }
            return list;
        }


    }
}