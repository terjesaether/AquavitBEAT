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
            return View(_db.Releases.ToList());
        }

        // GET: Releases/Details/5
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
        //[Route("Releases/AddRelease")]
        [HttpGet]
        // GET: Releases/Create
        public ActionResult AddRelease()
        {

            ViewBag.ArtistId = new SelectList(_db.Artists, "ArtistId", "ArtistName");
            ViewBag.FormatTypeId = new SelectList(_db.FormatsTypes, "FormatTypeId", "FormatTypeName");
            ViewBag.ReleaseTypeId = new SelectList(_db.ReleaseTypes, "ReleaseTypeId", "ReleaseTypeName");
            ViewBag.SongId = new SelectList(_db.Songs, "SongId", "Title");
            return View();
        }

        // POST: Releases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRelease(ReleaseViewModel vm, int[] ArtistId, int[] SongId, int[] FormatTypeId, string ReleaseTypeId)
        {

            //var httpRequest = System.Web.HttpContext.Current.Request;
            var httpRequest = System.Web.HttpContext.Current;

            var success = addAndEdit.AddOrUpdateRelease(vm, httpRequest, ArtistId, SongId, FormatTypeId, ReleaseTypeId, false, true);

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

            ViewBag.ArtistId = new SelectList(_db.Artists, "ArtistId", "ArtistName");
            ViewBag.FormatTypeId = new SelectList(_db.FormatsTypes, "FormatTypeId", "FormatTypeName");
            ViewBag.ReleaseTypeId = new SelectList(_db.ReleaseTypes, "ReleaseTypeId", "ReleaseTypeName");
            ViewBag.SongId = new SelectList(_db.Songs, "SongId", "Title");

            var results = _db.Artists
                .Select(r => new
                {
                    r.ArtistId,
                    r.ArtistName,
                    Checked = _db.ReleaseToArtist.Where(s => s.ReleaseId == id.Value && s.ArtistId == r.ArtistId)
                    .Count() > 0

                });

            var CheckBoxes = new List<CheckBoxViewModel>();
            foreach (var item in results)
            {
                CheckBoxes.Add(new CheckBoxViewModel
                {
                    Name = item.ArtistName,
                    Id = item.ArtistId,
                    Checked = item.Checked
                });
            }

            var allSongs = _db.Songs
                .Select(r => new
                {
                    r.SongId,
                    r.Title,
                    Checked = _db.SongToReleases.Where(s => s.ReleaseId == id.Value && s.SongId == r.SongId)
                    .Count() > 0

                });



            var songsDropDown = new List<SelectListItem>();
            var songsCheckBoxes = new List<CheckBoxViewModel>();
            foreach (var item in allSongs)
            {
                songsCheckBoxes.Add(new CheckBoxViewModel
                {
                    Name = item.Title,
                    Id = item.SongId,
                    Checked = item.Checked
                });
            }

            foreach (var item in allSongs)
            {
                songsDropDown.Add(new SelectListItem
                {
                    Text = item.Title,
                    Value = item.SongId.ToString(),
                    Selected = item.Checked
                });
            }

            var songTo = _db.SongToReleases.ToList();

            ViewBag.SongId = songsDropDown;

            vm.ArtistCheckBoxes = CheckBoxes;
            vm.SongCheckBoxes = songsCheckBoxes;

            return View(vm);
        }

        // POST: Releases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Releases/Edit")]
        public ActionResult EditRelease(ReleaseViewModel vm, int[] ArtistId, int[] SongId, int[] FormatTypeId, string ReleaseTypeId)
        {

            //var httpRequest = System.Web.HttpContext.Current.Request;
            var httpRequest = System.Web.HttpContext.Current;

            var storagePath = "/images/releases/" + vm.Release.Title.ToString();

            //var release = vm.Release;

            //foreach (var chbx in vm.ArtistCheckBoxes)
            //{
            //    if (chbx.Checked)
            //    {
            //        release.Artists.Add(_db.Artists.Find(chbx.Id));
            //    }

            //}


            // Kjører UpdateAndCreate-metode:
            var success = addAndEdit.AddOrUpdateRelease(vm, httpRequest, ArtistId, SongId, FormatTypeId, ReleaseTypeId, true, false);

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
