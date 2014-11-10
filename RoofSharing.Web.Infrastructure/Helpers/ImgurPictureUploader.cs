using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace RoofSharing.Web.Infrastructure.Helpers
{
    public class ImgurPictureUploader
    {
        private const string ClientId = "a16ad2676cd456c";

        public static string HandlePostedPicture(HttpPostedFileBase picture)
        {
            var indexOfFileExtension = picture.FileName.LastIndexOf(".");
            if (indexOfFileExtension == -1)
            {
                throw new ArgumentException("File must include a file extension!");
            }

            var fileExtension = picture.FileName.Substring(indexOfFileExtension).ToLower();
            if (!(fileExtension == ".jpg" || fileExtension == ".jpeg"))
            {
                throw new ArgumentException("Invalid file type! Only jpg and jpeg are supported!");
            }

            if (picture.ContentLength == 0)
            {
                throw new ArgumentException("Invalid file! Content is empty!");
            }

            if (picture.ContentLength > 1000 * 1000) //1 MB
            {
                throw new ArgumentException("File is too big! Max file length is 1 Mb!");
            }

            using (var memory = new MemoryStream())
            {
                picture.InputStream.CopyTo(memory);
                var imageBytes = memory.GetBuffer();
                var pictureUrl = UploadImageToImgur(imageBytes);
                return pictureUrl;
            }
        }

        public static string UploadImageToImgur(byte[] image)
        {
            WebClient w = new WebClient();
            w.Headers.Add("Authorization", "Client-ID " + ClientId);

            NameValueCollection Keys = new NameValueCollection();
            Keys.Add("image", Convert.ToBase64String(image));

            byte[] responseArray = w.UploadValues("https://api.imgur.com/3/image", Keys);
            var result = Encoding.ASCII.GetString(responseArray);

            Regex reg = new Regex("link\":\"(.*?)\"");
            Match match = reg.Match(result);

            string url = match.ToString().Replace("link\":\"", "").Replace("\"", "").Replace("\\/", "/");
                
            return url;
        }
    }
}