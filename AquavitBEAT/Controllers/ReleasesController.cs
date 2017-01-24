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
    public class ReleasesController : Controller
    {
        private AquavitBeatContext _db = new AquavitBeatContext();

        // GET: Releases
        public ActionResult Index()
        {
            return View(_db.Releases.ToList());
        }

        // GET: Releases/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult AddRelease(AddReleaseViewModel vm, int[] ArtistId, int[] SongId, int[] FormatTypeId)
        {

            //var httpRequest = System.Web.HttpContext.Current.Request;
            var httpRequest = System.Web.HttpContext.Current;
            // lagrer på releasedate:
            //var storagePath = "/images/releases/" + vm.Release.Title.ToString();


            var success = AddOrUpdateRelease(vm.Release, httpRequest, ArtistId, SongId, FormatTypeId, false, true);

            //List<string> formattedFilenames = new List<string>();
            //bool isSavedSuccessfully = true;

            //if (httpRequest.Files.Count > 0)
            //{

            //    for (int i = 0; i < httpRequest.Files.Count; i++)
            //    {
            //        if (httpRequest.Files[i].FileName.ToString() != "")
            //        {
            //            formattedFilenames.Add(httpRequest.Files[i].FileName.ToString().Replace(" ", "_"));
            //        }
            //        else
            //        {
            //            formattedFilenames.Add("");
            //        }

            //    }


            //    var fileOps = new FileOperations();

            //    isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);
            //}

            //if (isSavedSuccessfully)
            //{

            //    vm.Release.frontImageUrl = formattedFilenames[0];
            //    vm.Release.backImageUrl = formattedFilenames[1];

            //    foreach (var artistId in ArtistId)
            //    {
            //        vm.Release.Artists.Add(_db.Artists.Find(artistId));
            //    }

            //    foreach (var songId in SongId)
            //    {
            //        vm.Release.HasSongs.Add(_db.Songs.Find(songId));
            //    }

            //    foreach (var formatId in FormatTypeId)
            //    {
            //        var newFormat = _db.FormatsTypes.Find(formatId);
            //        var newReleaseFormat = new ReleaseFormat
            //        {
            //            Format = newFormat,
            //            FormatTypeId = newFormat.FormatTypeId,
            //            ReleaseFormatId = newFormat.FormatTypeId
            //        };
            //        vm.Release.FormatTypes.Add(newReleaseFormat);
            //    }
            //    try
            //    {
            //        var release = vm.Release;
            //        _db.Releases.Add(release);
            //        _db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //    catch (Exception e)
            //    {

            //        throw;
            //    }

            //}

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
            return View(vm);
        }

        // POST: Releases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRelease(ReleaseViewModel vm, int[] ArtistId, int[] SongId, int[] FormatTypeId)
        {
            //var httpRequest = System.Web.HttpContext.Current.Request;
            var httpRequest = System.Web.HttpContext.Current;
            //var release = vm.Release;
            var storagePath = "/images/releases/" + vm.Release.Title.ToString();

            // Kjører UpdateAndCreate-metode:
            var success = AddOrUpdateRelease(vm.Release, httpRequest, ArtistId, SongId, FormatTypeId, true, false);

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

        private bool AddOrUpdateRelease(Release release, HttpContext context, int[] ArtistId, int[] SongId, int[] FormatTypeId, bool update, bool create)
        {
            var httpRequest = context.Request;
            var storagePath = "/images/releases/" + release.Title.ToString();
            List<string> formattedFilenames = new List<string>();
            bool isSavedSuccessfully = true;


            if (httpRequest.Files.Count > 0)
            {

                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    if (httpRequest.Files[i].FileName.ToString() != "")
                    {
                        formattedFilenames.Add(httpRequest.Files[i].FileName.ToString().Replace(" ", "_"));
                    }
                    else
                    {
                        formattedFilenames.Add("");
                    }

                }

                var fileOps = new FileOperations();

                isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);
            }

            if (isSavedSuccessfully)
            {

                release.frontImageUrl = formattedFilenames[0];
                release.backImageUrl = formattedFilenames[1];

                foreach (var artistId in ArtistId)
                {
                    release.Artists.Add(_db.Artists.Find(artistId));
                }

                foreach (var songId in SongId)
                {
                    release.HasSongs.Add(_db.Songs.Find(songId));
                }

                foreach (var formatId in FormatTypeId)
                {
                    var newFormat = _db.FormatsTypes.Find(formatId);
                    var newReleaseFormat = new ReleaseFormat
                    {
                        Format = newFormat,
                        FormatTypeId = newFormat.FormatTypeId,
                        ReleaseFormatId = newFormat.FormatTypeId
                    };
                    release.FormatTypes.Add(newReleaseFormat);
                }
                try
                {
                    if (create)
                    {
                        _db.Releases.Add(release);
                        _db.SaveChanges();
                        return true;
                    }
                    else if (update)
                    {
                        _db.Entry(release).State = EntityState.Modified;
                        _db.SaveChanges();
                        return true;
                    }

                }
                catch (Exception e)
                {
                    throw;
                }

            }
            return false;
        }
    }
}
