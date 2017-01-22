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

            ViewBag.ArtistID = new SelectList(_db.Artists, "ArtistID", "ArtistName");
            ViewBag.Title = "Home Page";

            return View(artists);
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
    }
}