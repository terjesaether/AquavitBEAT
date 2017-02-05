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
            ReleasesToArtists = new List<ReleaseToArtist>();
            Images = new List<UploadedImage>();
        }

        [Key]
        public int ReleaseId { get; set; }

        [Required, Display(Name = "Release Title")]
        public string Title { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [DataType(DataType.Currency), Display(Name = "Price")]
        public decimal Price { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Release date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Release type")]
        public virtual ReleaseType ReleaseType { get; set; }
        //public virtual int ReleaseTypeId { get; set; }
        public string frontImageUrl
        {
            get
            {
                if (Images.Count > 0 && Images[0] != null)
                {
                    return Images[0].ImgUrl;
                };
                return "";
            }

        }

        public string backImageUrl
        {
            get
            {
                if (Images.Count > 1 && Images[1] != null)
                {
                    return Images[1].ImgUrl;
                };
                return "";
            }

        }




        [Display(Name = "Format type(s)")]
        public virtual List<ReleaseFormat> FormatTypes { get; set; }

        [Display(Name = "Artist(s)")]
        public virtual List<Artist> Artists { get; set; }
        public virtual List<UploadedImage> Images { get; set; }

        [Display(Name = "Contains song(s)")]
        public virtual List<Song> HasSongs { get; set; }

        [Display(Name = "Artwork")]
        public virtual List<Artwork> HasArtworks { get; set; }

        public virtual ICollection<ReleaseToArtist> ReleasesToArtists { get; set; } // Fjernes?
        public virtual ICollection<SongToRelease> SongToReleases { get; set; }
    }






    public class Artwork
    {
        [Key]
        public int ArtworkId { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
        //public List<Release> InReleases { get; set; }
        public string Comment { get; set; }

        [Display(Name = "Type of artwork (EP, Single, Comp)")]
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

    public class UploadedImage
    {
        public int UploadedImageId { get; set; }
        public string ImgUrl { get; set; }
        public string Title { get; set; }
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