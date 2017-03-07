using AquavitBEAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace AquavitBEAT.DbServices
{
    public class AquavitDbService
    {
        private AquavitBeatContext _db = new AquavitBeatContext();


        public Artist GetArtistById(int id)
        {
            return _db.Artists.Find(id);
        }

        public List<Artist> GetAllArtists()
        {
            return _db.Artists.ToList();
        }

        // SONGS

        public Song GetSongById(int id)
        {
            return _db.Songs.Find(id);
        }
        public List<Song> GetAllSongs()
        {
            return _db.Songs.ToList();
        }

        public void AddSong(Song song)
        {
            _db.Songs.Add(song);
            _db.SaveChanges();
        }

        public void DeleteSong(int id)
        {
            Song song = _db.Songs.Find(id);
            if (song != null) _db.Songs.Remove(song);
            _db.SaveChanges();
        }

        // RELEASES

        public Release GetReleaseById(int id)
        {
            return _db.Releases.Find(id);
        }

        public List<Release> GetAllReleases()
        {
            return _db.Releases.ToList();
        }

        public List<Release> OrderReleasesByTitle()
        {
            return _db.Releases.OrderBy(r => r.Title).ToList();
        }

        public List<Release> OrderByDescendingReleaseDate()
        {
            return _db.Releases.OrderByDescending(r => r.ReleaseDate).ToList();
        }

        public List<Release> OrderByReleaseDate()
        {
            return _db.Releases.OrderBy(r => r.ReleaseDate).ToList();
        }

        // DIV

        public List<SocialMedia> GetAllSocialMedia()
        {
            return _db.SocialMedias.ToList();
        }

        public List<SongToRelease> GetAllSongToReleases()
        {
            return _db.SongToReleases.ToList();
        }
        public List<SongToArtist> GetAllSongToArtists()
        {
            return _db.SongToArtists.ToList();
        }
        public List<SongToRemixer> GetAllSongToRemixers()
        {
            return _db.SongToRemixers.ToList();
        }


    }
}