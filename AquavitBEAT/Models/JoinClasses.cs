﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Models
{
    public class SongToArtist
    {
        public int SongToArtistId { get; set; }
        public int SongId { get; set; }
        public int ArtistId { get; set; }
        public virtual Song Song { get; set; }
        public virtual Artist Artist { get; set; }
    }
    public class SongToRemixer
    {
        public int SongToRemixerId { get; set; }
        public int SongId { get; set; }
        public int ArtistId { get; set; }
        public string RemixName { get; set; }
        public virtual Song Song { get; set; }
        public virtual Artist Artist { get; set; }
    }

    public class SongToRelease
    {
        public int SongToReleaseId { get; set; }
        public int SongId { get; set; }
        public int ReleaseId { get; set; }
        public virtual Song Song { get; set; }
        public virtual Release Release { get; set; }
    }

    public class ReleaseToArtist
    {
        public int ReleaseToArtistId { get; set; }
        public int ArtistId { get; set; }
        public int ReleaseId { get; set; }
        public virtual Release Release { get; set; }
        public virtual Artist Artist { get; set; }
    }

    public class ArtistToSocialMedia
    {
        [Key]
        public int ArtistToSocialMediaId { get; set; }
        public int ArtistId { get; set; }
        public int ArtistSocialMediaId { get; set; }
        public virtual Artist Artist { get; set; } // Tydeligvis viktig med referanser!
        public virtual ArtistSocialMedia ArtistSocialMedia { get; set; } // Tydeligvis viktig med referanser!
    }
}