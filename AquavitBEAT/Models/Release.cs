using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{
    public class Release
    {
        public Release()
        {
            FormatTypes = new List<ReleaseFormat>();
            Artists = new List<Artist>();
            HasSongs = new List<Song>();
            HasArtworks = new List<Artwork>();
        }

        [Required]
        public int ReleaseId { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Currency), Display(Name = "Price")]

        public decimal Price { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Release date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Release type")]
        public virtual ReleaseType ReleaseType { get; set; }
        //public virtual int ReleaseTypeId { get; set; }
        public string frontImageUrl { get; set; }
        public string backImageUrl { get; set; }

        [Display(Name = "Format type(s)")]
        public virtual List<ReleaseFormat> FormatTypes { get; set; }

        [Display(Name = "Artist(s)")]
        public virtual List<Artist> Artists { get; set; }

        [Display(Name = "Contains song(s)")]
        public virtual List<Song> HasSongs { get; set; }

        [Display(Name = "Artwork")]
        public virtual List<Artwork> HasArtworks { get; set; }
    }






    public class Artwork
    {
        [Required]
        public int ArtworkId { get; set; }

        [Required, Display(Name = "Title")]
        public string Title { get; set; }
        //public List<Release> InReleases { get; set; }
        public string Comment { get; set; }

        [Required, Display(Name = "Type of artwork (EP, Single, Comp)")]
        public ReleaseType TypeOfArtwork { get; set; }
        public virtual Image Image { get; set; }

    }
    // Selve bildet:
    public class Image
    {
        [Required]
        public int ImageId { get; set; }

        [Required, Display(Name = "Title")]
        public string Title { get; set; }
        public virtual List<Artwork> InArtworks { get; set; } = new List<Artwork>();

        [Required, Display(Name = "Author")]
        public virtual Artist Author { get; set; }
        public string ImgUrl { get; set; }
    }

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

        public virtual List<ArtistSocialMedia> SocialMedia { get; set; } = new List<ArtistSocialMedia>();


        //[NotMapped]
        //public Dictionary<string, string> SocialMediaInfo { get; set; } = new Dictionary<string, string>();
        public virtual List<Song> HasSongs { get; set; } = new List<Song>();
        public virtual List<Release> HasReleases { get; set; } = new List<Release>();
        public virtual List<Artwork> HasArtworks { get; set; } = new List<Artwork>();
    }

    public class Group // Band lissom
    {
        public int GroupId { get; set; }
        public virtual List<Artist> Artists { get; set; } = new List<Artist>();
    }

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
        public string Address { get; set; }
    }

}