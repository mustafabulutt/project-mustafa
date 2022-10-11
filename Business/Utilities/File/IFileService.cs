using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFileService
    {
        string FileSaveToServer(string filePath,IFormFile file);
        string FileSaveToFtp(IFormFile file);
    }
}
