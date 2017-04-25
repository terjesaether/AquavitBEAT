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

        // ARTISTS

        public Artist GetArtistById(int id)
        {
            return _db.Artists.Find(id);
        }

        public List<Artist> GetAllArtists()
        {
            return _db.Artists.ToList();
        }

        public MyJson DeleteArtist(int id)
        {
            var artist = _db.Artists.Find(id);
            var socMedias = _db.ArtistSocialMedias.Where(d => d.ArtistId == id).ToList();

            if (artist.SongsToArtists.Count() == 0 && artist.HasReleases.Count() == 0)
            {

                try
                {
                    foreach (var socMedia in socMedias)
                    {                       
                        _db.ArtistSocialMedias.Remove(socMedia);
                    }
                    
                    _db.Artists.Remove(artist);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    var error = e.Message;
                    //return error;
                }
                return new MyJson { Status = "success", Message = "Deleted" };
            }

            var songs = "";
            foreach (var song in artist.SongsToArtists)
            {
                songs += song.Song.Title + " ";
            }
            return new MyJson{ Status = "error", Message = "Can't delete artist. Artist has these songs: " + songs };
            
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
        public List<Release> OrderReleasesByTitleDecending()
        {
            return _db.Releases.OrderByDescending(r => r.Title).ToList();
        }

        public List<Release> OrderByReleaseDateDecending()
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