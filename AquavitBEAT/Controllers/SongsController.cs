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
    public class SongsController : Controller
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        AddAndEditOperations addAndEdit = new AddAndEditOperations();

        // GET: Songs
        public ActionResult Index()
        {
            var songs = _db.Songs.ToList();

            return View(songs);
        }

        // GET: Songs/Details/5
        [Route("Song/{id}")]
        public ActionResult SongDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = _db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        [Route("Songs/Add")]
        [HttpGet]
        public ActionResult AddSong()
        {
            ViewBag.ArtistID = new SelectList(_db.Artists, "ArtistID", "ArtistName");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Songs/Add")]
        public ActionResult AddSong(SongViewModel vm, int[] ArtistID)
        {
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction("AddSong");
            //}
            var httpRequest = System.Web.HttpContext.Current;

            var success = addAndEdit.AddOrUpdateSong(vm, httpRequest, ArtistID, false, true);

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // For å laste opp musikk til en evt pool
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAudio([Bind(Include = "SongId,Title,RemixName,ReleaseDate,Comment,AudioUrl")] Song song)
        {
            if (ModelState.IsValid)
            {
                _db.Songs.Add(song);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(song);
        }

        // GET: Songs/Edit/5
        [HttpGet]
        [Route("Songs/Edit/{id}")]
        public ActionResult EditSong(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = _db.Songs.Find(id);
            var vm = new SongViewModel
            {
                Song = song
            };

            // TO DO: hente inn flere artister!

            if (song == null)
            {
                return HttpNotFound();
            }

            var results = _db.Artists
                .Select(r => new
                {
                    r.ArtistId,
                    r.ArtistName,
                    Checked = _db.SongToArtists.Where(s => s.SongId == id.Value && s.ArtistId == r.ArtistId)
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

            vm.ArtistCheckBoxes = CheckBoxes;

            return View(vm);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Songs/Edit/{id}")]
        public ActionResult EditSong(SongViewModel vm, int[] ArtistId)
        {
            var httpRequest = System.Web.HttpContext.Current;

            var success = addAndEdit.AddOrUpdateSong(vm, httpRequest, ArtistId, true, false);

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }

            //if (ModelState.IsValid)
            //{
            //    _db.Entry(vm.Song).State = EntityState.Modified;
            //    _db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(vm);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = _db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = _db.Songs.Find(id);
            _db.Songs.Remove(song);
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
