using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{
    public class Artist
    {
        public Artist()
        {
            //SocialMediaInfo = new Dictionary<string, string>();
            //HasArtworks = new List<Artwork>();
            //HasReleases = new List<Release>();
            //HasSongs = new List<Song>();
            //SocialMediaTitle = new List<string>();
            //SocialMediaAddress = new List<string>();
            SocialMedia = new List<ArtistSocialMedia>();
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

        public virtual List<ArtistSocialMedia> SocialMedia { get; set; }


        //[NotMapped]
        //public Dictionary<string, string> SocialMediaInfo { get; set; } = new Dictionary<string, string>();
        public virtual List<Song> HasSongs { get; set; } = new List<Song>();
        public virtual List<Release> HasReleases { get; set; } = new List<Release>();
        public virtual List<Artwork> HasArtworks { get; set; } = new List<Artwork>();

        public virtual ICollection<ReleaseToArtist> ReleasesToArtists { get; set; }
        public virtual ICollection<SongToArtist> SongsToArtists { get; set; }
    }
}