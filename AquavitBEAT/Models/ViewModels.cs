using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web;
using System.Web.Mvc;
using AquavitBEAT.Models;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AquavitBEAT.Models
{

    public class ViewModels
    {
    }

    public class ReleaseViewModel
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        private GetSelectLists getLists = new GetSelectLists();
        public ReleaseViewModel()
        {

            ItemsListOfAllArtists = getLists.GetListOfAllArtists();
            ItemsListOfAllFormats = getLists.GetListOfAllFormats();
            ItemsListOfAllSongs = getLists.GetListOfAllSongs();
            ItemsListOfAllReleaseTypes = getLists.GetListOfAllReleaseTypes();
            ListOfAllArtists = _db.Artists.ToList();
            ListOfAllBuyOrStreamSites = _db.BuyOrStreamSites.ToList();
            //AllCurrentFormats = _db.FormatsTypes.ToList();
            //ArtistCheckBoxes = new List<CheckBoxViewModel>(); // Slett?
            SongCheckBoxes = new List<CheckBoxViewModel>();

            ItemListOfHasSongs = new List<SelectListItem>();
            //ListOfHasArtwork = getLists.GetListOfHasArtwork(Release);
            Release = new Release();
        }
        public Release Release { get; set; }

        public List<SelectListItem> ItemsListOfAllReleaseTypes { get; private set; }
        public List<SelectListItem> ItemsListOfAllSongs { get; private set; }
        public List<SelectListItem> ItemsListOfAllArtists { get; private set; }
        public List<SelectListItem> ItemsListOfAllFormats { get; private set; }
        public List<Artist> ListOfAllArtists { get; set; }
        public List<BuyOrStreamSite> ListOfAllBuyOrStreamSites { get; set; }
        //public List<FormatType> AllCurrentFormats { get; set; }

        //public List<CheckBoxViewModel> ArtistCheckBoxes { get; set; } // Slett?
        public List<CheckBoxViewModel> SongCheckBoxes { get; set; }
        [Required]
        public List<SelectListItem> ItemListOfHasSongs { get; set; }
        //public List<SelectListItem> ListOfHasArtwork { get; set; }

        public List<SelectListItem> GetReleasesAndSelectedFormats()
        {
            if (Release != null)
            {
                var allFormats = _db.FormatsTypes.ToList();
                var sortedFormats = new List<SelectListItem>();
                bool selected = false;
                foreach (var item in allFormats)
                {
                    if (Release.FormatTypes.Select(f => f.Format.FormatTypeName).Contains(item.FormatTypeName))
                    {
                        selected = true;
                    }
                    else
                    {
                        selected = false;
                    }
                    sortedFormats.Add(new SelectListItem
                    {
                        Text = item.FormatTypeName,
                        Value = item.FormatTypeId.ToString(),
                        Selected = selected
                    }
                        );

                }
                return sortedFormats;
            }
            return null;
        }
        public List<SelectListItem> GetSelectedReleaseType()
        {
            if (Release.ReleaseType == null)
            {
                Release.ReleaseType = new ReleaseType
                {
                    ReleaseTypeId = 1,
                    ReleaseTypeName = _db.ReleaseTypes.Where(r => r.ReleaseTypeId == 1).ToString()
                };
            }
            if (Release != null)
            {
                var allTypes = _db.ReleaseTypes.ToList();
                var sortedTypes = new List<SelectListItem>();
                bool selected = false;
                foreach (var item in allTypes)
                {
                    if (Release.ReleaseType.ReleaseTypeId == item.ReleaseTypeId)
                    {
                        selected = true;
                    }
                    else
                    {
                        selected = false;
                    }
                    sortedTypes.Add(new SelectListItem
                    {
                        Text = item.ReleaseTypeName,
                        Value = item.ReleaseTypeId.ToString(),
                        Selected = selected
                    }
                        );

                }
                return sortedTypes;
            }
            return null;
        }

    }

    public struct BuyOrStreamAndReleaseLinks
    {
        public BuyOrStreamSite BuyOrStreamSite { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class AddReleaseViewModel
    {
        //private AquavitBeatContext _db = new AquavitBeatContext();
        public AddReleaseViewModel()
        {
            var getLists = new GetSelectLists();

            ListOfArtists = getLists.GetListOfAllArtists();
            ListOfFormats = getLists.GetListOfAllArtists();
            //ListOfHasSongs = getLists.GetListOfHasSongs(Release);
            //ListOfHasArtwork = getLists.GetListOfHasArtwork(Release);
        }
        public Release Release { get; set; }

        public List<SelectListItem> ListOfArtists { get; set; }
        public List<SelectListItem> ListOfFormats { get; set; }
        //public List<SelectListItem> ListOfHasSongs { get; set; }
        //public List<SelectListItem> ListOfHasArtwork { get; set; }
    }

    public class SongViewModel
    {
        public SongViewModel()
        {
            //ArtistCheckBoxes = new List<CheckBoxViewModel>(); 
            //Song = new Song();
        }
        public Song Song { get; set; }

        //public DateTime SetReleaseDate { get; set; }
        //public List<CheckBoxViewModel> ArtistCheckBoxes { get; set; }
        //public virtual SongToArtist SongsToArtists { get; set; }
    }

    public class ArtistViewModel
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        public ArtistViewModel() // BARE NY Artist!
        {
            SocialMediaList = _db.SocialMedias.ToList();
            //SocialMediaList = new List<SocialMedia>();
            SongToArtists = new List<SongToArtist>();
            ReleaseToArtists = new List<ReleaseToArtist>();
            Artist = new Artist();
        }
        public ArtistViewModel(Artist artist) // Eksisterende artist!
        {
            Artist = artist;
            SocialMediaList = _db.SocialMedias.ToList();
        }
        public Artist Artist { get; set; }
        public List<SocialMedia> SocialMediaList { get; private set; }
        public List<SongToArtist> SongToArtists { get; set; }
        public List<ReleaseToArtist> ReleaseToArtists { get; set; }

    }

    public class FrontPageReleaseBox
    {
        private Random _rnd;
        private int _randomSong;
        public FrontPageReleaseBox(Release release)
        {
            _release = release;
            _rnd = new Random();
            _randomSong = _rnd.Next(0, _release.SongToReleases.Count());
        }
        private Release _release { get; set; }
        public string audioPlayerId { get { return "audio_" + _release.ReleaseId; } }
        public int ReleaseId { get { return _release.ReleaseId; } }
        public string Title { get { return _release.Title; } }
        public string ReleaseDate { get { return _release.ReleaseDate.ToShortDateString(); } }
        public string frontImageUrl { get { return _release.frontImageUrl; } }

        public IEnumerable<string> FormatTypes
        {
            get
            {
                foreach (var r in _release.FormatTypes)
                {
                    yield return r.Format.FormatTypeName;
                }
            }
        }

        public string ArtistNames
        {
            get
            {
                //var an = "";
                //an = string.Join(" // ", _release.GetArtists().Select(a => a.ArtistName).Distinct());
                //return an;

                return HelperMethods.GetDistinctArtistNames(_release);
            }
        }
        public string FeaturedSongUrl
        {
            get { return _release.HasSongs[_randomSong].AudioUrl; }

        }
        public string FeaturedSongTitle
        {
            get { return _release.HasSongs[_randomSong].Title; }

        }

        //public ReleaseToArtist ReleaseToArtist { get; set; }
        //public SongToRelease SongToRelease { get; set; }
    }

    public class FrontPageViewModel
    {
        public List<FrontPageReleaseBox> FrontPageReleaseBox { get; set; } = new List<FrontPageReleaseBox>();
    }

    public class ReleaseDetailsViewModel
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        public ReleaseDetailsViewModel(Release release)
        {
            _release = release;
        }
        private Release _release;
        public string ArtistNames
        {
            get
            {
                //return string.Join(" // ", _release.GetArtists().Select(a => a.ArtistName).Distinct());
                return HelperMethods.GetDistinctArtistNames(_release);
            }

        }
        public string Title { get { return _release.Title; } }
        public string About { get { return _release.Comment; } }
        public string FrontImg { get { return _release.frontImageUrl; } }
        public string Backimg { get { return _release.backImageUrl; } }

        public IEnumerable<BuyOrStreamLink> BuyOrStreamLinks
        {
            get
            {
                //var list = new List<BuyOrStreamLink>();
                foreach (var b in _release.BuyOrStreamLinks)
                {
                    if (!string.IsNullOrEmpty(b.LinkUrl))
                    {
                        //list.Add(b);
                        yield return b;
                    }
                }
                //return list;
            }
        }
        public IEnumerable<Song> Songs
        {
            get
            {
                return _release.HasSongs;
            }

        }
        public List<string> AudioUrls
        {
            get
            {
                return _release.HasSongs.Select(h => h.AudioUrl).ToList();
            }

        }

        public List<string> FullSongTitles
        {
            get
            {
                var list = new List<string>();

                for (int i = 0; i < _release.HasSongs.Count; i++)
                {
                    list.Add((i + 1).ToString("D2") + " // " + _release.HasSongs[i].GetFullSongName());
                }
                return list;
            }

        }

        //public string SongsFormatted
        //{
        //    get
        //    {
        //        string songs = "";

        //        for (int i = 0; i < _release.HasSongs.Count; i++)
        //        {
        //            songs += "<p>" + (i + 1).ToString("D2") + " // " + _release.HasSongs[i].GetFullSongName() + "</p>";
        //        }
        //        return songs;
        //    }

        //}
        public List<string> FormatTypes
        {
            get
            {
                return _release.FormatTypes.Select(f => f.Format.FormatTypeName).ToList();
            }
        }

        public IEnumerable<Artist> GetAllArtists()
        {
            return _db.Artists.ToList();
        }

    }




    class GetSelectLists
    {
        private AquavitBeatContext _db = new AquavitBeatContext();

        public List<SelectListItem> GetListOfAllArtists()
        {

            var listOfAll = _db.Artists.ToList();
            var list = new List<SelectListItem>();

            foreach (var item in listOfAll)
            {
                list.Add(new SelectListItem
                {
                    Text = item.ArtistName,
                    Value = item.ArtistId.ToString()
                });

            }
            return list;

        }
        public List<SelectListItem> GetListOfAllFormats()
        {
            var listOfAll = _db.FormatsTypes.ToList();
            var list = new List<SelectListItem>();

            foreach (var item in listOfAll)
            {

                list.Add(new SelectListItem
                {
                    Text = item.FormatTypeName,
                    Value = item.FormatTypeId.ToString()
                });

            }
            return list;
        }
        public List<SelectListItem> GetListOfAllSongs()
        {
            var listOfAll = _db.Songs.ToList();
            var list = new List<SelectListItem>();

            foreach (var item in listOfAll)
            {

                list.Add(new SelectListItem
                {
                    Text = item.Title + " (" + item.RemixName + ")",
                    Value = item.SongId.ToString()
                });

            }
            return list;
        }
        public List<SelectListItem> GetListOfAllReleaseTypes()
        {
            var listOfAll = _db.ReleaseTypes.ToList();
            var list = new List<SelectListItem>();

            foreach (var item in listOfAll)
            {

                list.Add(new SelectListItem
                {
                    Text = item.ReleaseTypeName,
                    Value = item.ReleaseTypeId.ToString()
                });

            }
            return list;
        }

        public List<SelectListItem> GetListOfHasSongs(Release release)
        {

            var listOfAll = _db.Songs.Where(s => s.SongId == release.ReleaseId).ToList();

            var list = new List<SelectListItem>();

            foreach (var item in listOfAll)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Title,
                    Value = item.SongId.ToString()
                });
            }
            return list;
        }
        public List<SelectListItem> GetListOfHasArtwork(Release release)
        {
            var listOfAll = _db.Artworks.Where(s => s.ArtworkId == release.ReleaseId).ToList();

            var list = new List<SelectListItem>();

            foreach (var item in listOfAll)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Title,
                    Value = item.ArtworkId.ToString()
                });
            }
            return list;
        }
    }

    public class AllReleasesPublicViewmodel
    {
        public AllReleasesPublicViewmodel()
        {
            SortBySelecList = GetSortBySelectList();
            AllReleases = new List<Release>();
        }
        public List<Release> AllReleases { get; set; }
        public List<SelectListItem> SortBySelecList { get; set; }

        public List<SelectListItem> GetSortBySelectList()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Value = "1",
                Text = "Release Name"
            });
            list.Add(new SelectListItem
            {
                Value = "2",
                Text = "Date - New first"
            });
            list.Add(new SelectListItem
            {
                Value = "3",
                Text = "Date - Old first"
            });
            list.Add(new SelectListItem
            {
                Value = "4",
                Text = "Artist"
            });
            return list;
        }
    }

    static class HelperMethods
    {
        public static string GetDistinctArtistNames(Release release)
        {
            return string.Join(" // ", release.GetArtists().Select(a => a.ArtistName).Distinct());
        }
    }



    public class CheckBoxViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
