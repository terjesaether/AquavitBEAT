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
            ArtistViewModel vm = new ArtistViewModel(_db.SocialMedias.ToList());

            ViewBag.SocialMedia = new SelectList(_db.SocialMedias, "SocialMediaId", "Name");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Artist/AddArtist/")]
        public ActionResult AddArtist(ArtistViewModel vm)
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
        [Route("Artist/Edit/{id}")]
        public ActionResult EditArtist(int id)
        {
            var artist = _db.Artists.Find(id);
            ArtistViewModel vm = new ArtistViewModel(_db.SocialMedias.ToList());
            vm.Artist = artist;

            ViewBag.SocialMedia = new SelectList(_db.SocialMedias, "SocialMediaId", "Name");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Artist/Edit/")]
        public ActionResult EditArtist(ArtistViewModel vm)
        {
            //var artist = _db.Artists.Find(id);

            var context = System.Web.HttpContext.Current;

            var isSaved = addAndEditOps.AddOrUpdateArtist(vm, context, true, false);

            if (isSaved)
            {
                return RedirectToAction("Index", "Artist");
            }

            ViewBag.SocialMedia = new SelectList(_db.SocialMedias, "SocialMediaId", "Name");

            vm.SocialMediaList = _db.SocialMedias.ToList();

            return View(vm);
        }

        public ActionResult AddSocialMedia(Artist artist)
        {
            return View();
        }

        [Route("Artist/{id}", Name = "ArtistDetails")]
        public ActionResult Details(int? id)
        {
            var vm = new ArtistViewModel(_db.SocialMedias.ToList());
            var artist = _db.Artists.Find(id);
            vm.Artist = artist;
            vm.SongToArtists = _db.SongToArtists.Where(s => s.ArtistId == id.Value).ToList();
            vm.ReleaseToArtists = _db.ReleaseToArtist.Where(r => r.ArtistId == id.Value).ToList();

            if (id == null)
            {
                return HttpNotFound();
            }

            return View(vm);
        }
    }
}