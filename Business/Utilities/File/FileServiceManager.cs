using Business.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FileServiceManager : IFileService
    {
        public string FileSaveToServer(string filePath, IFormFile file)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;

            string path = filePath + fileName;
            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public string FileSaveToFtp(IFormFile file)
        {

            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("FTP ip "+ fileName);
            request.Credentials = new NetworkCredential("kıd", "pass");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            using (Stream ftpStream = request.GetRequestStream())
            {
                file.CopyTo(ftpStream);
            }
            return fileName;
        }
    }
}
