using AquavitBEAT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AquavitBEAT.Controllers
{
    public class HomeController : Controller
    {
        private AquavitBeatContext _db = new AquavitBeatContext();

        public ActionResult Index()
        {
            var artists = _db.Artists.ToList();

            var vm = new FrontPageViewModel();
            ViewBag.Bodyclass = "front-page";


            var releases = _db.Releases
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

            vm.AllReleases = _db.Releases.ToList();

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
                    sortedReleases = _db.Releases.OrderByDescending(r => r.Title).ToList();
                    break;
                case "2":
                    sortedReleases = _db.Releases.OrderBy(r => r.ReleaseDate).ToList();
                    break;
                case "3":
                    sortedReleases = _db.Releases.OrderByDescending(r => r.ReleaseDate).ToList();
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
        public ActionResult Release(int id)
        {
            //var release = _db.Releases.Find(id);
            var vm = new ReleaseDetailsViewModel(_db.Releases.Find(id));

            //ViewBag.Title2 = release.Title;
            ViewBag.Bodyclass = "front-page";

            return View(vm);
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}