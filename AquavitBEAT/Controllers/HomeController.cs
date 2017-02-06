using AquavitBEAT.Models;
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
            var release = _db.Releases.Find(id);

            ViewBag.Title2 = release.Title;
            ViewBag.Bodyclass = "front-page";

            return View();
        }
    }
}