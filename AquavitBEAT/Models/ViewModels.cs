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
        public ReleaseViewModel()
        {
            var getLists = new GetSelectLists();

            ListOfArtists = getLists.GetListOfArtists();
            ListOfFormats = getLists.GetListOfArtists();
            ListOfAllArtists = _db.Artists.ToList();
            ArtistCheckBoxes = new List<CheckBoxViewModel>();
            SongCheckBoxes = new List<CheckBoxViewModel>();

            //ListOfHasSongs = getLists.GetListOfHasSongs(Release);
            //ListOfHasArtwork = getLists.GetListOfHasArtwork(Release);
        }
        public Release Release { get; set; }

        public List<SelectListItem> ListOfArtists { get; set; }
        public List<SelectListItem> ListOfFormats { get; set; }
        public List<Artist> ListOfAllArtists { get; set; }

        public List<CheckBoxViewModel> ArtistCheckBoxes { get; set; }
        public List<CheckBoxViewModel> SongCheckBoxes { get; set; }
        //public List<SelectListItem> ListOfHasSongs { get; set; }
        //public List<SelectListItem> ListOfHasArtwork { get; set; }
    }

    public class AddReleaseViewModel
    {
        //private AquavitBeatContext _db = new AquavitBeatContext();
        public AddReleaseViewModel()
        {
            var getLists = new GetSelectLists();

            ListOfArtists = getLists.GetListOfArtists();
            ListOfFormats = getLists.GetListOfArtists();
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



    class GetSelectLists
    {
        private AquavitBeatContext _db = new AquavitBeatContext();

        public List<SelectListItem> GetListOfArtists()
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
        public List<SelectListItem> GetListOfFormats()
        {
            var listOfAll = _db.FormatsTypes.ToList();
            var list = new List<SelectListItem>();

            foreach (var item in listOfAll)
            {
                list.Add(new SelectListItem
                {
                    Text = item.FormatTypeName,
                    Value = item.FormatTypeName.ToString()
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
