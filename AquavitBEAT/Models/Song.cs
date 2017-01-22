﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{
    public class Song
    {
        [Required]
        public int SongId { get; set; }

        [Required, Display(Name = "Song name")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Artist(s)")]
        public virtual List<Artist> Artist { get; set; } = new List<Artist>();

        [Required, Display(Name = "Remix name")]
        public string RemixName { get; set; }

        [Required, DataType(DataType.DateTime), Display(Name = "Release date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Comment")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Required, Display(Name = "Audiofile:")]
        public string AudioUrl { get; set; }
        public virtual List<Artist> Remixers { get; set; } = new List<Artist>();
        public virtual List<Release> InReleases { get; set; } = new List<Release>();

    }
}