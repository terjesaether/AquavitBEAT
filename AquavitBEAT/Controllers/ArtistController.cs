using AquavitBEAT.Models;
using AquavitBEAT.Operations;
using System.Linq;
using System.Web.Mvc;
using System;
using AquavitBEAT.DbServices;

namespace AquavitBEAT.Controllers
{
    public class ArtistController : Controller
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        private AddAndEditOperations addAndEditOps = new AddAndEditOperations();
        private AquavitDbService _dbService = new AquavitDbService();

        // GET: Artist
        public ActionResult Index()
        {
            var artists = _dbService.GetAllArtists();
            return View(artists);
        }

        [HttpGet]
        [Route("Artist/Add")]
        public ActionResult AddArtist()
        {
            ArtistViewModel vm = new ArtistViewModel();

            //ViewBag.SocialMedia = new SelectList(_db.SocialMedias, "SocialMediaId", "Name");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Artist/Add")]
        public ActionResult AddArtist(ArtistViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var context = System.Web.HttpContext.Current;

                    var isSaved = addAndEditOps.AddOrUpdateArtist(vm, context, false, true);

                    if (isSaved)
                    {
                        return RedirectToAction("Index", "Artist");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }

            return View();
        }

        [HttpGet]
        [Route("Artist/Edit/{id}")]
        public ActionResult EditArtist(int id)
        {
            var artist = _dbService.GetArtistById(id);
            ArtistViewModel vm = new ArtistViewModel(artist);

            //ViewBag.SocialMedia = new SelectList(_db.SocialMedias, "SocialMediaId", "Name");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Artist/Edit/")]
        public ActionResult EditArtist(ArtistViewModel vm)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var context = System.Web.HttpContext.Current;

                    var isSaved = addAndEditOps.AddOrUpdateArtist(vm, context, true, false);

                    if (isSaved)
                    {
                        return RedirectToAction("Index", "Artist");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.SocialMedia = new SelectList(_dbService.GetAllSocialMedia(), "SocialMediaId", "Name");

            //vm.SocialMediaList = _db.SocialMedias.ToList();

            return View(vm);
        }

        public ActionResult AddSocialMedia(Artist artist)
        {
            return View();
        }

        [Route("Artist/{id}")]
        public ActionResult ArtistDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var vm = new ArtistViewModel();
            var artist = _db.Artists.Find(id);
            vm.Artist = artist;
            vm.SongToArtists = _db.SongToArtists.Where(s => s.ArtistId == id.Value).ToList();
            vm.ReleaseToArtists = _db.ReleaseToArtist.Where(r => r.ArtistId == id.Value).ToList();

            return View(vm);
        }
    }
}