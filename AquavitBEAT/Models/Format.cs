using System.ComponentModel.DataAnnotations;

namespace AquavitBEAT.Models
{
    // Vinyl, Streaming, Download
    public class FormatType
    {
        public int FormatTypeId { get; set; }

        [Required, Display(Name = "Format type (Streaming, Vinyl, CD")]
        public string FormatTypeName { get; set; }
    }

    public class ReleaseFormat
    {
        [Key]
        public int ReleaseFormatId { get; set; }

        [DataType(DataType.Url)]
        //public string BuyUrl { get; set; }

        [Required, Display(Name = "Format type (Streaming, Vinyl, CD")]
        public virtual FormatType Format { get; set; }
        public virtual int FormatTypeId { get; set; } // Tydeligvis viktig med referanser! 
        public virtual Release Release { get; set; } // Tydeligvis viktig med referanser!
    }

    public class BuyOrStreamLink
    {
        [Key]
        public int BuyOrStreamLinkId { get; set; }
        public string LinkUrl { get; set; }
        public string LinkTitle { get; set; }
        public string FormatName { get; set; }
        public virtual Release Release { get; set; }
    }

    public class BuyOrStreamSite
    {
        [Key]
        public int BuyOrStreamSiteId { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
    }

    // Slette:
    //public enum Formats
    //{
    //    Vinyl,
    //    Download,
    //    Streaming,
    //    Cassette,
    //    CD
    //}

    // Slette:
    //public class ReleaseFormat2
    //{
    //    [Key]
    //    public int ReleaseFormatId { get; set; }
    //    public virtual Formats FormatName { get; set; }
    //    public virtual List<BuyUrl> BuyUrls { get; set; }

    //}

    // Slette:
    //public class BuyUrl
    //{
    //    [Key]
    //    public int BuyUrlId { get; set; }

    //    //[DataType(DataType.Url)]
    //    public string BuyLink { get; set; }
    //    public string UrlName { get; set; }
    //}
}