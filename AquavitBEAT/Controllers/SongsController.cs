using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AquavitBEAT.Models;

namespace AquavitBEAT.Controllers
{
    public class SongsController : Controller
    {
        private AquavitBeatContext _db = new AquavitBeatContext();

        // GET: Songs
        public ActionResult Index()
        {
            return View(_db.Songs.ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
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
        //[Route("Songs/Create")]
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
        //[Route("Songs/AddSong")]
        public ActionResult AddSong(Song song, int[] ArtistID)
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            // lagrer på releasedate:
            var storagePath = "/audio/" + song.ReleaseDate.ToString("yyyy/MM/dd");
            List<string> formattedFilenames = new List<string>();
            bool isSavedSuccessfully = true;

            if (httpRequest.Files.Count > 0)
            {
                formattedFilenames.Add(httpRequest.Files[0].FileName.ToString().Replace(" ", "_"));

                var fileOps = new FileOperations();

                isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);
            }
            if (isSavedSuccessfully)
            {
                foreach (var artistId in ArtistID)
                {
                    song.Artist.Add(_db.Artists.Find(artistId));
                }

                song.AudioUrl = storagePath + "/" + formattedFilenames[0];

                _db.Songs.Add(song);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("AddSong");
        }

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
        public ActionResult Edit(int? id)
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

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SongId,Title,RemixName,ReleaseDate,Comment,AudioUrl")] Song song)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(song).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(song);
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
