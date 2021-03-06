﻿using AquavitBEAT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AquavitBEAT.ViewModels;
using AquavitBEAT.DbServices;
using AquavitBEAT.Operations;

namespace AquavitBEAT.Controllers
{
    public class HomeController : Controller
    {
        //private AquavitBeatContext _db = new AquavitBeatContext();
        private AquavitDbService _dbService = new AquavitDbService();
        private SortOperations _sortOperations = new SortOperations();

        public ActionResult Index()
        {
            //var artists = _db.Artists.ToList();
            var artists = _dbService.GetAllArtists();

            var vm = new FrontPageViewModel();
            ViewBag.Bodyclass = "front-page";
            
            var releases = _dbService.GetAllReleases()
                .Where(r => r.ShowOnFrontpage == true)
                .Take(8)
                .OrderByDescending(d => d.ReleaseDate)
                .ToList();

            foreach (var release in releases)
            {
                var newBox = new FrontPageReleaseBox(release);
                vm.FrontPageReleaseBox.Add(newBox);
            }

            return View(vm);
        }

        //[Route("Releases/All")]
        [HttpGet]
        public ActionResult AllReleases()
        {
            var vm = new AllReleasesPublicViewmodel()
            {

                //vm.AllReleases = _db.Releases.ToList();
                AllReleases = _dbService.GetAllReleases()
            };
            ViewBag.Bodyclass = "front-page";
            return View(vm);
        }

        //[Route("Releases/All")]
        [HttpPost]
        public ActionResult AllReleases(string AllReleases)
        {
            var vm = new AllReleasesPublicViewmodel();
           
            vm.AllReleases = _sortOperations.SortReleases(AllReleases);

            ViewBag.Bodyclass = "front-page";
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Bodyclass = "front-page";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Release(int id = 3)
        {
            //var release = _db.Releases.Find(id);
            var vm = new ReleaseDetailsViewModel(_dbService.GetReleaseById(id));

            //ViewBag.Title2 = release.Title;
            ViewBag.Bodyclass = "release-page";

            return View(vm);
        }
        [HttpGet]
        public ActionResult Artist(int id)
        {
            //var vm = new ArtistDetailsViewModel(_dbService.GetArtistById(id));
            var vm = new ArtistDetailsViewModel(id);

            ViewBag.Bodyclass = "artist-page";

            return View(vm);
        }

        [Authorize]
        public ActionResult Admin()
        {
            return View();
        }
    }
}