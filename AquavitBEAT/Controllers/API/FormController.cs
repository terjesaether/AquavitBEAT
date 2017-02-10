using AquavitBEAT.Models;
using AquavitBEAT.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AquavitBEAT.Controllers.API
{
    public class FormController : ApiController
    {
        private AquavitBeatContext _db = new AquavitBeatContext();
        // GET: api/Image
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Image/5
        public string Get(int id)
        {
            return "value";
        }


        [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("api/form")]
        [ValidateAntiForgeryToken]
        public IHttpActionResult Post(ArtistViewModel vm)
        {
            var storagePath = "/images/profiles/";
            bool isSavedSuccessfully = true;
            Artist artist = new Artist();
            var httpRequest = HttpContext.Current.Request;
            var fileOps = new FileOperations();

            artist.ArtistName = httpRequest.Form["Artist.ArtistName"];
            artist.FirstName = httpRequest.Form["Artist.FirstName"];
            artist.LastName = httpRequest.Form["Artist.LastName"];
            artist.Address = httpRequest.Form["Artist.Address"];
            artist.Mail = httpRequest.Form["Artist.Mail"];
            artist.Country = httpRequest.Form["Artist.Country"];
            artist.About = httpRequest.Form["Artist.About"];
            artist.ProfileImgUrl = storagePath + httpRequest.Files[0].FileName;

            var SocialMediaList = _db.SocialMedias.ToList();

            foreach (var item in SocialMediaList)
            {
                artist.ArtistSocialMedias.Add(new ArtistSocialMedia
                {
                    Name = item.Name.ToString(),
                    Url = HttpContext.Current.Request.Form[item.Name].ToString()
                });
            }
            var formattedFilenames = new List<string>();

            string formattedFilename = artist.ArtistName.Replace(" ", "_") + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".jpg";

            formattedFilenames.Add(formattedFilename);

            isSavedSuccessfully = fileOps.SaveUploadedFile(httpRequest, storagePath, formattedFilenames);

            artist.ProfileImgUrl = storagePath + formattedFilename;
            _db.Artists.Add(artist);
            _db.SaveChanges();



            if (isSavedSuccessfully)
            {
                //return Json(new { Message = "Ok" });
                return RedirectToRoute("ArtistDetails", new
                {
                    id = int.Parse(_db.Artists
                    .OrderByDescending(a => a.ArtistId)
                    .Select(a => a.ArtistId)
                    .First()
                    .ToString())
                });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }


        }


        // PUT: api/Image/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Image/5
        public void Delete(int id)
        {
        }
    }
}
