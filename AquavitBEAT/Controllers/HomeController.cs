using AquavitBEAT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AquavitBEAT.ViewModels;
using AquavitBEAT.DbServices;

namespace AquavitBEAT.Controllers
{
    public class HomeController : Controller
    {
        //private AquavitBeatContext _db = new AquavitBeatContext();
        private AquavitDbService _dbService = new AquavitDbService();

        public ActionResult Index()
        {
            //var artists = _db.Artists.ToList();
            var artists = _dbService.GetAllArtists();

            var vm = new FrontPageViewModel();
            ViewBag.Bodyclass = "front-page";


            var releases = _dbService.GetAllReleases()
                .Take(6)
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
            var vm = new AllReleasesPublicViewmodel();

            //vm.AllReleases = _db.Releases.ToList();
            vm.AllReleases = _dbService.GetAllReleases();

            ViewBag.Bodyclass = "front-page";
            return View(vm);
        }

        //[Route("Releases/All")]
        [HttpPost]
        public ActionResult AllReleases(string AllReleases)
        {
            var vm = new AllReleasesPublicViewmodel();
            var sortedReleases = new List<Release>();

            switch (AllReleases)
            {
                case "1":
                    //sortedReleases = _db.Releases.OrderBy(r => r.Title).ToList();
                    sortedReleases = _dbService.OrderReleasesByTitle();
                    break;
                case "2":
                    //sortedReleases = _db.Releases.OrderByDescending(r => r.ReleaseDate).ToList();
                    sortedReleases = _dbService.OrderByDescendingReleaseDate();
                    break;
                case "3":
                    //sortedReleases = _db.Releases.OrderBy(r => r.ReleaseDate).ToList();
                    sortedReleases = _dbService.OrderByReleaseDate();
                    break;
                case "4":
                    sortedReleases = null;
                    break;
                default:
                    break;
            }
            vm.AllReleases = sortedReleases;

            ViewBag.Bodyclass = "front-page";
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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
        public ActionResult Artist(int id = 3)
        {
            var vm = new ArtistDetailsViewModel(_dbService.GetArtistById(id));

            ViewBag.Bodyclass = "artist-page";

            return View(vm);
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}