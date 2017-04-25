using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{
    public class Release
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        public Release()
        {
            FormatTypes = new List<ReleaseFormat>();
            //FormatTypes2 = FillFormatsList2();
            Artists = new List<Artist>();
            HasSongs = new List<Song>();
            HasArtworks = new List<Artwork>();
            //ReleasesToArtists = new List<ReleaseToArtist>();
            Images = new List<UploadedImage>();
            BuyOrStreamLinks = new List<BuyOrStreamLink>();
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

        public bool ShowOnFrontpage { get; set; }

        [Required, Display(Name = "Release type")]
        public virtual ReleaseType ReleaseType { get; set; }

        public string FrontImageUrl
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

        public string BackImageUrl
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


        [Required, Display(Name = "Format type(s)")]
        public virtual List<ReleaseFormat> FormatTypes { get; set; }
        
        [Display(Name = "Artist(s)")]
        public virtual List<Artist> Artists { get; set; }
        public virtual List<UploadedImage> Images { get; set; }

        [Display(Name = "Contains song(s)")]
        public virtual List<Song> HasSongs { get; set; }

        [Display(Name = "Artwork")]
        public virtual List<Artwork> HasArtworks { get; set; }
        public virtual List<BuyOrStreamLink> BuyOrStreamLinks { get; set; }   
        
        [Required, Display(Name = "Song(s)")]
        public virtual ICollection<SongToRelease> SongToReleases { get; set; }

        // Kanskje fjernes? Bedre å regne ut i ViewModel:
        [Required, Display(Name = "Releases(s)")]
        public virtual ICollection<ReleaseToArtist> ReleaseToArtist { get; set; }

        private IEnumerable<ReleaseFormat> FillFormatsList()
        {

            foreach (var format in _db.FormatsTypes)
            {
                var newReleaseFormat = new ReleaseFormat
                {
                    Format = format
                };

                yield return newReleaseFormat;
            }
        }

        public IEnumerable<Artist> GetArtists()
        {
            return SongToReleases
                .SelectMany(s => s.Song.SongToArtists.Select(a => a.Artist))
                .Distinct().ToList();
        }

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



}