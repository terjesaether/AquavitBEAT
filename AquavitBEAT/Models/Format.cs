using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        [Required, Display(Name = "Format type (Streaming, Vinyl, CD")]
        public virtual FormatType Format { get; set; }
        public virtual int FormatTypeId { get; set; }
    }
}