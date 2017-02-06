using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web;
using System.Web.Mvc;
using AquavitBEAT.Models;
using System.Data.Entity;

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

            //ItemsListOfAllArtists = getLists.GetListOfArtists();
            //ItemsListOfAllFormats = getLists.GetListOfAllFormats();
            ListOfAllArtists = _db.Artists.ToList();
            //ArtistCheckBoxes = new List<CheckBoxViewModel>(); // Slett?
            SongCheckBoxes = new List<CheckBoxViewModel>();

            //ListOfHasSongs = getLists.GetListOfHasSongs(Release);
            //ListOfHasArtwork = getLists.GetListOfHasArtwork(Release);
        }
        public Release Release { get; set; }

        public List<SelectListItem> ItemsListOfAllReleaseTypes { get { return getLists.GetListOfAllReleaseTypes(); } }
        public List<SelectListItem> ItemsListOfAllSongs { get { return getLists.GetListOfAllSongs(); } }
        public List<SelectListItem> ItemsListOfAllArtists { get { return getLists.GetListOfAllArtists(); } }
        public List<SelectListItem> ItemsListOfAllFormats { get { return getLists.GetListOfAllFormats(); } }
        public List<Artist> ListOfAllArtists { get; set; }

        //public List<CheckBoxViewModel> ArtistCheckBoxes { get; set; } // Slett?
        public List<CheckBoxViewModel> SongCheckBoxes { get; set; }
        //public List<SelectListItem> ListOfHasSongs { get; set; }
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
                    if (Release.FormatTypes.Select(f => f.FormatTypeId).Contains(item.FormatTypeId))
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
            ArtistCheckBoxes = new List<CheckBoxViewModel>();
        }
        public Song Song { get; set; }
        public List<CheckBoxViewModel> ArtistCheckBoxes { get; set; }
        //public virtual SongToArtist SongsToArtists { get; set; }
    }

    public class ArtistViewModel
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        public ArtistViewModel()
        {
            SocialMediaList = new List<SocialMedia>();
            SongToArtists = new List<SongToArtist>();
            ReleaseToArtists = new List<ReleaseToArtist>();
        }
        public ArtistViewModel(List<SocialMedia> socialMediaList)
        {
            SocialMediaList = socialMediaList;
        }
        public Artist Artist { get; set; }
        public List<SocialMedia> SocialMediaList { get; set; }
        public List<SongToArtist> SongToArtists { get; set; }
        public List<ReleaseToArtist> ReleaseToArtists { get; set; }

    }

    public class FrontPageReleaseBox
    {
        public FrontPageReleaseBox(Release release)
        {
            _release = release;
        }

        private Release _release { get; set; }

        public string audioPlayerId { get { return "audio_" + _release.ReleaseId; } }
        public int ReleaseId { get { return _release.ReleaseId; } }
        public string Title { get { return _release.Title; } }
        public string ReleaseDate { get { return _release.ReleaseDate.ToShortDateString(); } }
        public string frontImageUrl { get { return _release.frontImageUrl; } }

        public List<string> FormatTypes
        { get { return _release.FormatTypes.Select(f => f.Format.FormatTypeName).ToList(); } }

        public string ArtistNames
        {
            get
            {
                return string.Join(" // ", _release.Artists.Select(a => a.ArtistName));

            }

        }
        public string FeaturedSongUrl
        {
            get { return _release.HasSongs[0].AudioUrl; }

        }
        public string FeaturedSongTitle
        {
            get { return _release.HasSongs[0].Title; }

        }

        //public ReleaseToArtist ReleaseToArtist { get; set; }
        //public SongToRelease SongToRelease { get; set; }
    }

    public class FrontPageViewModel
    {
        public List<FrontPageReleaseBox> FrontPageReleaseBox { get; set; } = new List<Models.FrontPageReleaseBox>();
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

    public class CheckBoxViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
