using AquavitBEAT.Models;
using System.Linq;
using System.Web.Mvc;

namespace AquavitBEAT.Controllers
{
    public class ArtistController : Controller
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        ArtistRepository artistRepo = new ArtistRepository();

        // GET: Artist
        public ActionResult Index()
        {
            var artists = artistRepo.GetAllArtists();
            return View(artists);
        }

        [HttpGet]
        [Route("Artist/AddArtist/", Name = "AddArtist")]
        public ActionResult AddArtist()
        {
            AddArtistViewModel vm = new AddArtistViewModel(_db.SocialMedias.ToList());

            ViewBag.SocialMedia = new SelectList(_db.SocialMedias, "SocialMediaId", "Name");

            return View(vm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddArtist(AddArtistViewModel vm)
        {
            return View();
        }

        public ActionResult AddSocialMedia(Artist artist)
        {
            return View();
        }

        [Route("Artist/{id}", Name = "ArtistDetails")]
        public ActionResult Details(int? id)
        {
            //var vm = new AddArtistViewModel(_db.SocialMedias.ToList());
            var artist = _db.Artists.Find(id);

            if (id == null)
            {
                return HttpNotFound();
            }

            return View(_db.Artists.Find(id));
        }
    }
}