using AquavitBEAT.Models;
using AquavitBEAT.Operations;
using System.Linq;
using System.Web.Mvc;

namespace AquavitBEAT.Controllers
{
    public class ArtistController : Controller
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        ArtistRepository artistRepo = new ArtistRepository();
        AddAndEditOperations addAndEditOps = new AddAndEditOperations();

        // GET: Artist
        public ActionResult Index()
        {
            var artists = artistRepo.GetAllArtists();
            return View(artists);
        }

        [HttpGet]
        [Route("Artist/AddArtist/")]
        public ActionResult AddArtist()
        {
            AddArtistViewModel vm = new AddArtistViewModel(_db.SocialMedias.ToList());

            ViewBag.SocialMedia = new SelectList(_db.SocialMedias, "SocialMediaId", "Name");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Artist/AddArtist/")]
        public ActionResult AddArtist(AddArtistViewModel vm)
        {

            var context = System.Web.HttpContext.Current;

            var isSaved = addAndEditOps.AddOrUpdateArtist(vm, context, false, true);

            if (isSaved)
            {
                return RedirectToAction("Index", "Artist");
            }

            return View();
        }

        [HttpGet]
        [Route("Artist/EditArtist/{id}")]
        public ActionResult EditArtist(int id)
        {
            var artist = _db.Artists.Find(id);

            ViewBag.SocialMedia = new SelectList(_db.SocialMedias, "SocialMediaId", "Name");

            return View(artist);
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