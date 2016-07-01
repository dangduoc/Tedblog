using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlickrNet;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using MyBlog.Common;
namespace MyBlog.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/
        public ActionResult AuthorizeFlickr()
        {
            return View();
        }
        [HttpPost]
        public string Index()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                HttpPostedFileBase filebase = new HttpPostedFileWrapper(pic);
                var filename = Path.GetFileName(filebase.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/Upload"), filename);
                filebase.SaveAs(path);
                Flickr flickr = FlickrManager.GetAuthInstance();
                string FileuploadedID = flickr.UploadPicture(path, "Ảnh bạn bè upload", "", null, true, false, false);
                PhotoInfo oPhotoInfo = flickr.PhotosGetInfo(FileuploadedID);
                System.IO.File.Delete(path);
                return oPhotoInfo.LargeUrl;
            }
            else return "InvalidFile";
        }
        [HttpPost]
        public JsonResult checkToken()
        {
            return Json(this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OAuthToken"), JsonRequestBehavior.AllowGet);
        }
        public void RemoveToken()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OAuthToken"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OAuthToken"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }
        private static OAuthRequestToken requestToken;
        [HttpPost]
        public string Authenticate()
        {
            Flickr f = FlickrManager.GetInstance();
            requestToken = f.OAuthGetRequestToken("oob");

            string url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Write);

            return url;
        }
        [HttpPost]
        public JsonResult CompleteAuth(string verifycode)
        {
            //if (String.IsNullOrEmpty(VerifierTextBox.Text))
            //{
            //    MessageBox.Show("You must paste the verifier code into the textbox above.");
            //    return;
            //}
            Flickr f = FlickrManager.GetInstance();
            var accessToken = f.OAuthGetAccessToken(requestToken, verifycode);
            FlickrManager.OAuthToken = accessToken;
            return Json((accessToken != null), JsonRequestBehavior.AllowGet);
        }
    }
}
