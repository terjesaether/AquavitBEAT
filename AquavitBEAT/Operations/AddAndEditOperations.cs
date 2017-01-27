﻿using AquavitBEAT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace AquavitBEAT.Operations
{
    public class AddAndEditOperations
    {
        private AquavitBeatContext _db = new AquavitBeatContext();


        public bool AddOrUpdateRelease(ReleaseViewModel vm, HttpContext context, int[] ArtistId, int[] SongId, int[] FormatTypeId, string ReleaseTypeId, bool update, bool create)
        {
            Release release;
            if (vm.Release.ReleaseId != 0)
            {
                release = _db.Releases.Find(vm.Release.ReleaseId);
                release.Title = vm.Release.Title;
                release.Price = vm.Release.Price;
                release.ReleaseDate = vm.Release.ReleaseDate;
                release.Comment = vm.Release.Comment;
                //song.InReleases = vm.Song.InReleases;
            }
            else
            {
                release = new Release();
            }

            var httpRequest = context.Request;
            var storagePath = "/images/releases/" + release.Title.ToString();
            List<string> formattedFilenames = new List<string>();
            bool isSavedSuccessfully = true;

            if (httpRequest.Files.Count > 0)
            {

                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    if (httpRequest.Files[i].FileName.ToString() != "")
                    {
                        formattedFilenames.Add(httpRequest.Files[i].FileName.ToString().Replace(" ", "_"));
                    }
                    else
                    {
                        formattedFilenames.Add("");
                    }

                    release.Images.Add(new UploadedImage
                    {
                        ImgUrl = formattedFilenames[i]
                    });
                }

                var fileOps = new FileOperations();

                isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);
            }

            if (isSavedSuccessfully)
            {

                foreach (var songId in SongId)
                {
                    if (songId > 0)
                    {
                        var song = _db.Songs.Find(songId);
                        release.HasSongs.Add(song);
                        foreach (var artist in song.Artist)
                        {
                            if (!release.Artists.Contains(artist))
                            {
                                release.Artists.Add(artist);
                                release.ReleasesToArtists.Add(new ReleaseToArtist
                                {
                                    Artist = artist,
                                    Release = release,
                                    ArtistId = artist.ArtistId,
                                    ReleaseId = release.ReleaseId
                                });
                            }

                        }
                    }

                }

                //Ops!Må laste inn artistene som allerede finnes i Sangene
                //var newArtists = new List<Artist>();
                //foreach (var songs in release.HasSongs)
                //{
                //    foreach (var artist in songs.Artist)
                //    {
                //        if (!newArtists.Contains(artist))
                //        {
                //            newArtists.Add(artist);
                //        }
                //    }

                //    //release.Artists.Add(_db.Artists.Find(artist));
                //}
                ////release.Artists = release.HasSongs
                ////    .SelectMany(s => s.Artist)
                ////    .ToList();

                //release.ReleasesToArtists = release.HasSongs
                //    .Select(s => new ReleaseToArtist
                //    {
                //        Artist = s.Artist.Single(),
                //        Release = release,
                //        ArtistId = s.Artist.Select(a => a.ArtistId).Single(),
                //        ReleaseId = release.ReleaseId
                //    }).
                //    ToList();




                foreach (var formatId in FormatTypeId)
                {
                    var newFormat = _db.FormatsTypes.Find(formatId);
                    var newReleaseFormat = new ReleaseFormat
                    {
                        Format = newFormat,
                        FormatTypeId = newFormat.FormatTypeId,
                        ReleaseFormatId = newFormat.FormatTypeId
                    };
                    release.FormatTypes.Add(newReleaseFormat);
                }

                release.ReleaseType = _db.ReleaseTypes.Find(Convert.ToInt32(ReleaseTypeId));


                try
                {
                    if (create)
                    {
                        _db.Releases.Add(release);
                        _db.SaveChanges();
                        return true;
                    }
                    else if (update)
                    {
                        _db.Entry(release).State = EntityState.Modified;
                        _db.SaveChanges();
                        return true;
                    }

                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return false;
        }

        public bool AddOrUpdateArtist(AddArtistViewModel vm, HttpContext context, bool update, bool create)
        {
            var storagePath = "/images/profiles/";
            bool isSavedSuccessfully = true;
            Artist artist = vm.Artist;
            var httpRequest = context.Request;
            var fileOps = new FileOperations();

            //artist.ProfileImgUrl = storagePath + httpRequest.Files[0].FileName;

            var SocialMediaList = _db.SocialMedias.ToList();

            foreach (var item in SocialMediaList)
            {
                artist.SocialMedia.Add(new ArtistSocialMedia
                {
                    Name = item.Name.ToString(),
                    Address = HttpContext.Current.Request.Form[item.Name].ToString()
                });
            }

            // Flere filenames siden savefile-funksjon tar i mot liste
            var formattedFilenames = new List<string>();

            string formattedFilename = artist.ArtistName.Replace(" ", "_") + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".jpg";

            formattedFilenames.Add(formattedFilename);

            isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);

            artist.ProfileImgUrl = storagePath + formattedFilename;

            try
            {
                if (create)
                {
                    _db.Artists.Add(artist);
                    _db.SaveChanges();
                    return true;
                }
                else if (update)
                {
                    _db.Entry(artist).State = EntityState.Modified;
                    _db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }

        public bool AddOrUpdateSong(SongViewModel vm, HttpContext context, int[] ArtistID, bool update, bool create)
        {
            Song song;
            if (vm.Song.SongId != 0)
            {
                song = _db.Songs.Find(vm.Song.SongId);
                song.Title = vm.Song.Title;
                song.RemixName = vm.Song.RemixName;
                song.ReleaseDate = vm.Song.ReleaseDate;
                song.Comment = vm.Song.Comment;
                //song.InReleases = vm.Song.InReleases;
            }
            else
            {
                song = new Song();
            }

            var httpRequest = context.Request;
            var storagePath = "/audio/" + song.ReleaseDate.ToString("yyyy/MM/dd");
            bool isSavedSuccessfully = true;
            List<string> formattedFilenames = new List<string>();

            string ext = Path.GetExtension(httpRequest.Files[0].FileName);

            Artist addedArtist;
            if (ArtistID == null)
            {
                // Sletter eksisterende artister:
                foreach (var item in _db.SongToArtists)
                {
                    if (item.SongId == song.SongId)
                    {
                        _db.Entry(item).State = EntityState.Deleted;
                    }
                }
                // Og legger til de som er huket av:
                foreach (var chbx in vm.ArtistCheckBoxes)
                {
                    if (chbx.Checked)
                    {
                        addedArtist = _db.Artists.Find(chbx.Id);

                        song.Artist.Add(addedArtist);
                        song.SongToArtists.Add(new SongToArtist
                        {
                            Artist = addedArtist,
                            Song = song,
                            SongId = song.SongId,
                            ArtistId = chbx.Id,
                        });


                    }
                }
                // Lager en ny array med artister for navngiving:
                ArtistID = new int[song.Artist.Count()];
                for (int i = 0; i < ArtistID.Length; i++)
                {
                    ArtistID[i] = song.Artist[i].ArtistId;
                }
            }

            // Hvis det er create, dvs man henter fra drop down
            else if (ArtistID != null)
            {
                foreach (var artistId in ArtistID)
                {
                    addedArtist = _db.Artists.Find(artistId);
                    song.Artist.Add(addedArtist);
                    song.SongToArtists.Add(new SongToArtist
                    {
                        Artist = addedArtist,
                        Song = song,
                        SongId = song.SongId,
                        ArtistId = artistId,
                    });
                }
            }

            string audioFileName = "";

            foreach (var artist in ArtistID)
            {
                audioFileName += _db.Artists.Find(artist).ArtistName.ToString().Replace(" ", "_") + "_";
            }
            audioFileName += song.Title.ToString().Replace(" ", "_") + "_" + song.RemixName.ToString().Replace(" ", "_") + ext;

            if (httpRequest.Files.Count > 0)
            {
                //formattedFilenames.Add(httpRequest.Files[0].FileName.ToString().Replace(" ", "_"));
                formattedFilenames.Add(audioFileName);

                var fileOps = new FileOperations();

                isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);
            }
            if (isSavedSuccessfully)
            {

                song.AudioUrl = storagePath + "/" + formattedFilenames[0];

                try
                {
                    if (create)
                    {
                        _db.Songs.Add(song);
                        _db.SaveChanges();
                        return true;
                    }
                    else if (update)
                    {
                        _db.Entry(song).State = EntityState.Modified;
                        _db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception e)
                {

                    return false;
                }

            }
            return false;
        }

        public void AddArtists(Artist addedArtist, Song song, int artistId)
        {
            song.Artist.Add(addedArtist);
            song.SongToArtists.Add(new SongToArtist
            {
                Artist = addedArtist,
                Song = song,
                SongId = song.SongId,
                ArtistId = artistId,
            });
        }
    }
}