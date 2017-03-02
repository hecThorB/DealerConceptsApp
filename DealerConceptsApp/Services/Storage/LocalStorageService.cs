using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Services.Storage
{
    public class LocalStorageService : BaseService
    {
        public string Upload(HttpPostedFile file)
        {
            string uploadFileName = Guid.NewGuid().ToString() + file.FileName;
            string path = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
            string filePath = path + uploadFileName;
            file.SaveAs(filePath);

            //saving file to virtual path
            // string encodedUrl = HttpUtility.UrlEncode(uploadFileName);
            string encodedUrl = HttpUtility.UrlPathEncode(uploadFileName);
            string virtualPath = System.Configuration.ConfigurationManager.AppSettings["VirtualUploadFilePath"].ToString();
            string virtualFilePath = virtualPath + encodedUrl;

            return virtualFilePath;
        }
    }
}