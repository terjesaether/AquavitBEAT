using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{
    public class Song
    {
        public Song()
        {
            Remixers = new List<Artist>();
            InReleases = new List<Release>();
            SongToArtists = new List<SongToArtist>();
            SongToRemixers = new List<SongToRemixer>();
        }

        [Required]
        public int SongId { get; set; }

        [Required, Display(Name = "Song name")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Artist(s)")]
        public virtual List<Artist> Artists { get; set; } = new List<Artist>();

        [Required, Display(Name = "Remix name")]
        public string RemixName { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Release date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Comment")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Display(Name = "Audiofile:")]
        public string AudioUrl { get; set; }

        [Display(Name = "Remixers(s)")]
        public virtual List<Artist> Remixers { get; set; }
        public virtual ICollection<Release> InReleases { get; set; }
        public virtual ICollection<SongToArtist> SongToArtists { get; set; }
        public virtual ICollection<SongToRemixer> SongToRemixers { get; set; }
        public virtual ICollection<SongToRelease> SongToReleases { get; set; }

        public string GetFullSongName()
        {
            return Title + " (" + RemixName + ")";
        }
        public string GetFormattedArtistNames()
        {
            string artists = "";
            foreach (var a in this.Artists)
            {
                artists += a.ArtistName + " // ";
            }
            artists = artists.TrimEnd('/');
            return artists;
        }

        public string GetFormattedRemixNames()
        {
            string artists = "";
            foreach (var a in this.Remixers)
            {
                artists += a.ArtistName + " // ";
            }
            artists = artists.TrimEnd('/');
            return artists;
        }

    }
}