using System;
using System.Collections.Generic;
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
}