using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AquavitBEAT.Models
{
    public class ArtistRepository : IArtistRepository
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        public void Delete(Artist artist)
        {
            _db.Artists.Remove(artist);
            _db.SaveChanges();
        }

        public Artist GetArtistById(int id)
        {
            var artist = _db.Artists.Find(id);
            return artist;
        }

        public IEnumerable<Artist> GetAllArtists()
        {
            var artists = _db.Artists.ToList();
            return artists;
        }

        public void Update(Artist artist)
        {
            _db.Entry(artist).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}