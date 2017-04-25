using AquavitBEAT.Models;
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

        public bool AddOrUpdateRelease(ReleaseViewModel vm, HttpContext context, int[] SongId, int[] FormatTypeId, string ReleaseTypeId, string deleteCovers,string showOnFrontpage, bool update, bool create)
        {
            var httpRequest = context.Request;
            Release release;
            if (vm.Release.ReleaseId > 0)
            {
                release = _db.Releases.Find(vm.Release.ReleaseId);
                release.Title = vm.Release.Title;
                release.Price = vm.Release.Price;
                release.ReleaseDate = vm.Release.ReleaseDate;
                release.Comment = vm.Release.Comment;
                if (showOnFrontpage == "on")
                {
                    release.ShowOnFrontpage = true;
                }
                else
                {
                    release.ShowOnFrontpage = false;
                }
                release.FormatTypes.Clear();
                release.HasSongs.Clear();
                release.Artists.Clear();
                //release.SongToReleases.Clear(); OBS, sjekk om dette ødelegger
                release.BuyOrStreamLinks.Clear();
                //release.ReleasesToArtists.Clear(); // Tror ikke denne blir brukt

                if (deleteCovers == "on")
                {
                    release.Images.Clear();
                }
            }
            else
            {
                release = vm.Release;
            }

            // FORMATER:
            //foreach (var format in vm.Release.FormatTypes)
            //{
            //    var newFormat = new ReleaseFormat
            //    {
            //        Format = format,
            //        FormatTypeId = format.FormatTypeId,
            //        BuyUrl = httpRequest.Form[format.FormatTypeName]
            //    };
            //    release.FormatTypes.Add(newFormat);
            //}
            //var counter = 0;
            //foreach (var format in release.FormatTypes2)
            //{
            //    for (int i = 0; i < format.BuyUrls.Count; i++)
            //    {
            //        //var test = httpRequest.Form[format.FormatName.ToString() + "_0".ToString()];

            //        format.BuyUrls.Add(new BuyUrl
            //        {
            //            BuyLink = httpRequest.Form[(format.FormatName.ToString() + "_" + i).ToString()],
            //            UrlName = "Test"
            //        });
            //    }

            //    //counter++;
            //}

            // BUY OR STREAM
            foreach (var b in vm.ListOfAllBuyOrStreamSites)
            {
                var newB = new BuyOrStreamLink
                {
                    LinkUrl = httpRequest.Form[b.Name],
                    FormatName = b.Format,
                    LinkTitle = httpRequest.Form[b.Name + "_title"],
                    Release = release
                };
                release.BuyOrStreamLinks.Add(newB);
            }

            string storagePath = "/images/releases/" + release.Title.ToString().Replace(" ", "_") + "/";
            List<string> formattedFilenames = new List<string>();
            bool isSavedSuccessfully = true;
            var fileOps = new FileOperations();

            foreach (var item in _db.SongToReleases)
            {
                if (item.ReleaseId == release.ReleaseId)
                {
                    _db.Entry(item).State = EntityState.Deleted;
                }
            }

            //// OBS NY!
            //foreach (var item in _db.ReleaseToArtist)
            //{
            //    if (item.ReleaseId == release.ReleaseId)
            //    {
            //        _db.Entry(item).State = EntityState.Deleted;
            //    }
            //}

            //foreach (var item in _db.SongToReleases)
            //{
            //    if (item.ReleaseId == release.ReleaseId)
            //    {
            //        _db.ReleaseToArtist.Add(item.Song.Artists)
            //    }
            //}

            // SANGER
            foreach (var songId in SongId)
            {
                if (songId > 0)
                {
                    var song = _db.Songs.Find(songId);
                    release.HasSongs.Add(song);
                    release.SongToReleases.Add(new SongToRelease
                    {
                        Release = release,
                        ReleaseId = release.ReleaseId,
                        Song = _db.Songs.Find(songId),
                        SongId = songId
                    });
                    foreach (var artist in song.Artists)
                    {
                        if (!release.Artists.Contains(artist))
                        {
                            release.Artists.Add(artist);
                        }
                    }
                }
            }

            // FORMATER: (Bare som navn)
            if (FormatTypeId != null)
            {
                var formats = _db.FormatsTypes.ToList();
                foreach (var id in FormatTypeId)
                {
                    if (formats.Select(f => f.FormatTypeId).Contains(id))
                    {
                        var newReleaseFormat = new ReleaseFormat
                        {
                            FormatTypeId = id,
                            Format = formats.Where(f => f.FormatTypeId == id).SingleOrDefault()
                        };
                        release.FormatTypes.Add(newReleaseFormat);
                    }
                }
            }

            release.ReleaseType = _db.ReleaseTypes.Find(Convert.ToInt32(ReleaseTypeId));

            // IMAGES:
            var tempFileName = "";
            var imgIndex = release.Images.Count;
            if (httpRequest.Files.Count > 0 && httpRequest.Files[0].ContentLength > 0)
            {
                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    if (httpRequest.Files[i].FileName.ToString() != "")
                    {
                        string extension = Path.GetExtension(httpRequest.Files[i].FileName.ToString());

                        tempFileName = release.Title.ToString().Replace(" ", "_") + "_" + imgIndex + extension;

                        //tempFileName = Path.GetFileNameWithoutExtension(httpRequest.Files[i].FileName.ToString().Replace(" ", "_")) + "_" + i + extension;

                        formattedFilenames.Add(tempFileName);
                    }
                    else
                    {
                        formattedFilenames.Add("");
                    }

                    release.Images.Add(new UploadedImage
                    {
                        ImgUrl = storagePath + formattedFilenames[i],
                        Title = release.Title + "_" + i
                    });
                    imgIndex++;
                }

                isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);
            }

            if (isSavedSuccessfully)
            {
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
                catch (Exception /*e*/)
                {
                    throw;
                }
            }
            return false;
        }

        // ARTIST
        public bool AddOrUpdateArtist(ArtistViewModel vm, HttpContext context, bool update, bool create)
        {
            Artist artist;
            if (vm.Artist.ArtistId > 0)
            {
                //artist = _db.Artists.Find(vm.Artist.ArtistId);
                artist = _db.Artists
                .Where(a => a.ArtistId == vm.Artist.ArtistId)
                .Include("ArtistSocialMedias")
                .Single();


                artist.FirstName = vm.Artist.FirstName;
                artist.LastName = vm.Artist.LastName;
                artist.ArtistName = vm.Artist.ArtistName;
                artist.About = vm.Artist.About;
                artist.Address = vm.Artist.Address;
                artist.Country = vm.Artist.Country;
                artist.Mail = vm.Artist.Mail;
                artist.ArtistSocialMedias.Clear();
            }
            else
            {
                artist = vm.Artist;
            }


            const string storagePath = "/images/profiles/";
            bool isSavedSuccessfully = true;
            var httpRequest = context.Request;
            var fileOps = new FileOperations();

            //artist.ProfileImgUrl = storagePath + httpRequest.Files[0].FileName;

            // SOCIAL MEDIA
            // Noe med at listen cleares og prøves å opprettes igjen. Listen bør ikke cleares og addes, men oppdateres
            // VIKTIG!?
            //foreach (var item in _db.ArtistSocialMedias)
            //{
            //    if (item.Artist.ArtistId == artist.ArtistId)
            //    {
            //        _db.Entry(item).State = EntityState.Deleted;
            //    }
            //}

            // Sletter eksisterende og fyller opp liste på nytt:
            var some = _db.ArtistSocialMedias.Where(a => a.ArtistId == artist.ArtistId);
            foreach (var item in some)
            {
                _db.ArtistSocialMedias.Remove(item);
            }

            foreach (var item in vm.SocialMediaList)
            {
                var newSocMedia = new ArtistSocialMedia
                {
                    Name = item.Name.ToString(),
                    Url = httpRequest.Form[item.Name].ToString(),
                    Artist = artist,
                    ArtistId = artist.ArtistId
                };

                artist.ArtistSocialMedias.Add(newSocMedia);
                _db.ArtistSocialMedias.Add(newSocMedia);
            }


            //for (int i = 0; i < artist.SocialMedia.Count; i++)
            //{
            //    string name = artist.SocialMedia[i].Name.ToString();
            //    artist.SocialMedia[i].Url = httpRequest.Form[name].ToString();
            //    if (update)
            //    {
            //        artist.SocialMedia[i].ArtistSocialMediaId = int.Parse(httpRequest.Form[name + "_id"]);
            //    }

            //}

            // Flere filenames siden savefile-funksjon tar i mot liste
            var formattedFilenames = new List<string>();

            string formattedFilename = artist.ArtistName.Replace(" ", "_") + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".jpg";

            formattedFilenames.Add(formattedFilename);

            if (httpRequest.Files.Count > 0 && httpRequest.Files[0].ContentLength > 0)
            {
                isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);

                artist.ProfileImgUrl = storagePath + formattedFilename;
            }

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
            catch (Exception /* e */)
            {
                return false;
            }

            return false;
        }

        // SONG
        public bool AddOrUpdateSong(SongViewModel vm, HttpContext context, int[] ArtistID, int[] RemixerID, bool update, bool create)
        {
            Song song;
            if (vm.Song.SongId != 0)
            {
                song = _db.Songs.Find(vm.Song.SongId);
                song = _db.Songs
                    .Where(s => s.SongId == vm.Song.SongId)
                    .Include("Artists")
                    .Include("Remixers")
                    .Single();
                song.Title = vm.Song.Title;
                song.RemixName = vm.Song.RemixName;
                song.ReleaseDate = vm.Song.ReleaseDate;
                song.Comment = vm.Song.Comment;
                song.Artists.Clear();
                song.Remixers.Clear();

                //song.InReleases = vm.Song.InReleases;
            }
            else
            {
                song = vm.Song;
            }

            var httpRequest = context.Request;
            string storagePath = "/audio/" + song.ReleaseDate.ToString("yyyy/MM/dd");
            bool isSavedSuccessfully = true;
            List<string> formattedFilenames = new List<string>();
            string extension = Path.GetExtension(httpRequest.Files[0].FileName);

            Artist addedArtist;
            if (ArtistID == null)
            {
                // Sletter eksisterende artister:


                // Og legger til de som er huket av: FJERNES?
                //foreach (var chbx in vm.ArtistCheckBoxes)
                //{
                //    if (chbx.Checked)
                //    {
                //        addedArtist = _db.Artists.Find(chbx.Id);

                //        song.Artist.Add(addedArtist);
                //        song.SongToArtists.Add(new SongToArtist
                //        {
                //            Artist = addedArtist,
                //            Song = song,
                //            SongId = song.SongId,
                //            ArtistId = chbx.Id,
                //        });
                //    }
                //}
                // Lager en ny array med artister for navngiving:
                ArtistID = new int[song.Artists.Count()];
                for (int i = 0; i < ArtistID.Length; i++)
                {
                    ArtistID[i] = song.Artists[i].ArtistId;
                }

            }

            // Hvis det er create, dvs man henter fra dropdown
            else if (ArtistID != null)
            {
                // VIKTIG!
                foreach (var item in _db.SongToArtists)
                {
                    if (item.SongId == song.SongId)
                    {
                        _db.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var artistId in ArtistID)
                {
                    addedArtist = _db.Artists.Find(artistId);
                    song.Artists.Add(addedArtist);
                    song.SongToArtists.Add(new SongToArtist
                    {
                        Artist = addedArtist,
                        Song = song,
                        SongId = song.SongId,
                        ArtistId = artistId,
                    });
                }
            }

            if (RemixerID == null)
            {


                RemixerID = new int[song.Artists.Count()];
                for (int i = 0; i < RemixerID.Length; i++)
                {
                    RemixerID[i] = song.Artists[i].ArtistId;
                }
            }
            else if (RemixerID != null)
            {

                // Sletter eksisterende artister: VIKTIG!
                foreach (var item in _db.SongToRemixers)
                {
                    if (item.SongId == song.SongId)
                    {
                        _db.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var remixerId in RemixerID)
                {
                    addedArtist = _db.Artists.Find(remixerId);
                    song.Remixers.Add(addedArtist);
                    song.SongToRemixers.Add(new SongToRemixer
                    {
                        Artist = addedArtist,
                        Song = song,
                        SongId = song.SongId,
                        ArtistId = remixerId,
                        RemixName = httpRequest.Form["Song.RemixName"]
                    });
                }
            }

            string audioFileName = "";

            foreach (var artist in ArtistID)
            {
                audioFileName += _db.Artists.Find(artist).ArtistName.ToString().Replace(" ", "_") + "_";
            }
            audioFileName += song.Title.ToString().Replace(" ", "_") + "_" + song.RemixName.ToString().Replace(" ", "_") + extension;

            if (httpRequest.Files.Count > 0 && httpRequest.Files[0].ContentLength > 0)
            {
                //formattedFilenames.Add(httpRequest.Files[0].FileName.ToString().Replace(" ", "_"));
                formattedFilenames.Add(audioFileName);

                var fileOps = new FileOperations();

                isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);

                song.AudioUrl = storagePath + "/" + formattedFilenames[0];
            }
            if (isSavedSuccessfully)
            {
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
                catch (Exception /*e*/)
                {
                    return false;
                }
            }
            return false;
        }

        // Slettes:
        //public void AddArtists(Artist addedArtist, Song song, int artistId)
        //{
        //    song.Artist.Add(addedArtist);
        //    song.SongToArtists.Add(new SongToArtist
        //    {
        //        Artist = addedArtist,
        //        Song = song,
        //        SongId = song.SongId,
        //        ArtistId = artistId,
        //    });
        //}
    }
}