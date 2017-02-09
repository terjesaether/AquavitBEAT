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

namespace AquavitBEAT.Controllers
{
    public class ReleasesController : Controller
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        AddAndEditOperations addAndEdit = new AddAndEditOperations();

        // GET: Releases
        public ActionResult Index()
        {
            //var vm = new List<ReleaseIndexViewModel>();
            //foreach (var item in _db.Releases.ToList())
            //{
            //    var newRelease = new ReleaseIndexViewModel(item);
            //    vm.Add(newRelease);
            //}

            return View(_db.Releases.ToList());
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
            Release release = _db.Releases.Find(id);
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
            ViewBag.SongId = new SelectList(_db.Songs, "SongId", "Title");

            var vm = new ReleaseViewModel();
            //vm.Release = new Release();

            vm.Release.ReleaseDate = DateTime.Now;
            vm.Release.Price = 10;

            //foreach (var f in vm.AllCurrentFormats)
            //{
            //    var newReleaseFormat = new ReleaseFormat
            //    {
            //        Format = f,
            //        FormatTypeId = f.FormatTypeId,
            //        BuyUrl = ""
            //    };
            //    vm.Release.FormatTypes.Add(newReleaseFormat);
            //}

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

            var formats = Request.Form["Release.FormatTypes"].Split(',');
            ReleaseTypeId = Request.Form["Release.ReleaseType"];
            var songs = Request.Form["Release.SongToReleases"].Split(',');


            FormatTypeId = Array.ConvertAll(formats, int.Parse);
            SongId = Array.ConvertAll(songs, int.Parse);

            var httpRequest = System.Web.HttpContext.Current;

            var success = addAndEdit.AddOrUpdateRelease(vm, httpRequest, SongId, FormatTypeId, ReleaseTypeId, null, false, true);

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }

        }

        // GET: Releases/Edit/5
        [HttpGet]
        [Route("Releases/Edit/{id}")]
        public ActionResult EditRelease(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vm = new ReleaseViewModel
            {
                Release = _db.Releases.Find(id)
            };


            if (vm.Release == null)
            {
                return HttpNotFound();
            }


            var allSongs = _db.Songs
                .Select(r => new
                {
                    r.SongId,
                    r.Title,
                    r.RemixName,
                    Checked = _db.SongToReleases.Where(s => s.ReleaseId == id.Value && s.SongId == r.SongId)
                    .Count() > 0
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

            var songTo = _db.SongToReleases.ToList();

            ViewBag.SongId = songsDropDown;
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
            var success = addAndEdit.AddOrUpdateRelease(vm, httpRequest, SongId, FormatTypeId, ReleaseTypeId, deleteCovers, true, false);

            if (success)
            {
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
            Release release = _db.Releases.Find(id);
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
            Release release = _db.Releases.Find(id);
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
