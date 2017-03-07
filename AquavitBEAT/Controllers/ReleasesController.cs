using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AquavitBEAT.Models;
using AquavitBEAT.Operations;
using AquavitBEAT.DbServices;

namespace AquavitBEAT.Controllers
{
    public class ReleasesController : Controller
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        private AddAndEditOperations _addAndEdit = new AddAndEditOperations();
        private AquavitDbService _dbService = new AquavitDbService();

        // GET: Releases
        public ActionResult Index()
        {
            //var vm = new List<ReleaseIndexViewModel>();
            //foreach (var item in _db.Releases.ToList())
            //{
            //    var newRelease = new ReleaseIndexViewModel(item);
            //    vm.Add(newRelease);
            //}

            return View(_dbService.GetAllReleases());
        }



        // GET: Releases/Details/5
        [HttpGet]
        [Route("Releases/{id}")]
        public ActionResult ReleaseDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Release release = _dbService.GetReleaseById(id.Value);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }
        [Route("Releases/Add")]
        [HttpGet]
        // GET: Releases/Create
        public ActionResult AddRelease()
        {

            ViewBag.FormatTypeId = new SelectList(_db.FormatsTypes, "FormatTypeId", "FormatTypeName");
            ViewBag.ReleaseTypeId = new SelectList(_db.ReleaseTypes, "ReleaseTypeId", "ReleaseTypeName");
            ViewBag.SongId = new SelectList(_dbService.GetAllSongs(), "SongId", "Title");

            var vm = new ReleaseViewModel();

            vm.Release.ReleaseDate = DateTime.Now;
            vm.Release.Price = 10;

            return View(vm);
        }

        // POST: Releases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Releases/Add")]

        public ActionResult AddRelease(ReleaseViewModel vm, int[] SongId, int[] FormatTypeId, string ReleaseTypeId)
        {
            try
            {

                string[] formats;
                if (!string.IsNullOrEmpty(Request.Form["Release.FormatTypes"]))
                {
                    formats = Request.Form["Release.FormatTypes"].Split(',');
                    FormatTypeId = Array.ConvertAll(formats, int.Parse);
                }
                else
                {
                    FormatTypeId[0] = 1;
                }

                ReleaseTypeId = Request.Form["Release.ReleaseType"];
                var songs = Request.Form["Release.SongToReleases"].Split(',');

                SongId = Array.ConvertAll(songs, int.Parse);

                var httpRequest = System.Web.HttpContext.Current;

                var success = _addAndEdit.AddOrUpdateRelease(vm, httpRequest, SongId, FormatTypeId, ReleaseTypeId, null, false, true);

                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vm);
                }

            }
            catch (Exception)
            {

                return View();
            }


        }

        // GET: Releases/Edit/5
        [HttpGet]
        [Route("Releases/Edit/{id}/{from?}")]
        public ActionResult EditRelease(int? id, string from)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vm = new ReleaseViewModel
            {
                Release = _dbService.GetReleaseById(id.Value)
            };


            if (vm.Release == null)
            {
                return HttpNotFound();
            }
            if (from != null)
            {
                vm.RequestFrom = from;
            }


            var allSongs = _dbService.GetAllSongs()
                .Select(r => new
                {
                    r.SongId,
                    r.Title,
                    r.RemixName,
                    Checked = _db.SongToReleases
                    .Count(s => s.ReleaseId == id.Value && s.SongId == r.SongId) > 0
                });



            //var songsCheckBoxes = new List<CheckBoxViewModel>();
            //foreach (var item in allSongs)
            //{
            //    songsCheckBoxes.Add(new CheckBoxViewModel
            //    {
            //        Name = item.Title,
            //        Id = item.SongId,
            //        Checked = item.Checked
            //    });
            //}

            // Lager dropdown med valgt verdi:
            var songsDropDown = new List<SelectListItem>();
            foreach (var item in allSongs)
            {
                songsDropDown.Add(new SelectListItem
                {
                    Text = item.Title + " (" + item.RemixName + ")",
                    Value = item.SongId.ToString(),
                    Selected = item.Checked
                });
            }

            //var songTo = _dbService.GetAllSongToReleases();

            ViewBag.SongId = songsDropDown;
            vm.ItemListOfHasSongs = songsDropDown; // Sender ikke med selected...
            ViewBag.ReleaseTypeId = vm.GetSelectedReleaseType();
            ViewBag.FormatTypeId = vm.GetReleasesAndSelectedFormats();

            //vm.ArtistCheckBoxes = CheckBoxes;
            //vm.SongCheckBoxes = songsCheckBoxes;

            return View(vm);
        }

        // POST: Releases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Releases/Edit")]
        public ActionResult EditRelease(ReleaseViewModel vm, int[] SongId, int[] FormatTypeId, string ReleaseTypeId)
        {

            HttpContext httpRequest = System.Web.HttpContext.Current;

            string storagePath = "/images/releases/" + vm.Release.Title.ToString();

            string deleteCovers = Request.Form["deleteCovers"];

            // Kjører UpdateAndCreate-metode:
            var success = _addAndEdit.AddOrUpdateRelease(vm, httpRequest, SongId, FormatTypeId, ReleaseTypeId, deleteCovers, true, false);

            if (success)
            {
                if (vm.RequestFrom == "main")
                {
                    return RedirectToAction("Release", "Home", new { id = vm.Release.ReleaseId });
                }
                return RedirectToAction("Index");
            }
            else
            {

                return View(vm);
            }
        }

        // GET: Releases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Release release = _dbService.GetReleaseById(id.Value);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }

        // POST: Releases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Release release = _dbService.GetReleaseById(id);
            _db.Releases.Remove(release);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
