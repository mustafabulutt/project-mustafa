using Business.Aspects.Secured;
using Core.Aspects;
using Core.Aspects.Caching;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.MovieRepository;
using Entities.Concrete;
using Entities.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Utilities.ApiRequest
{
    public class ApiRequestManager : IApiRequest
    {
        private readonly RestClient _client;
        private readonly IMovieDal _moviedal;
        private const int maxMovieCount = 1000;
        public ApiRequestManager(IMovieDal movieDal)
        {
            _client = new RestClient("https://api.themoviedb.org/3/");
            _moviedal = movieDal;
        }

        [TransactionAspect]
        [RemoveCacheAspect("IMovieService.GetList")]
        [assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
        public void GetMovie()
        {


            #region download movie
           
            try
            {
                string downloadDate = DateTime.Now.ToString("MM_dd_yyyy");
                //string downloadDate = "09_10_2022";


                string downloadUrl = $"http://files.tmdb.org/p/exports/movie_ids_{downloadDate}.json.gz";

                WebClient webClient = new WebClient();
                {
                    var fileName = $"movie_ids_{downloadDate}.json.gz";
                    webClient.DownloadFile(downloadUrl, string.Format("./" + fileName, Guid.NewGuid().ToString()));
                    DirectoryInfo directorySelected = new DirectoryInfo("./");
                    foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
                    {
                         Decompress(fileToDecompress);
                    }


                    string jsonFilePath = $"./movie_ids_{downloadDate}.json";
                    string json =   File.ReadAllText(jsonFilePath);                  
                    string newJson = $"[{json.Replace("}", "} ,")}]";
                    TextWriter writer;
                    using (writer = new StreamWriter(jsonFilePath, append: false))
                    {
                        writer.WriteLine(newJson.Substring(0, newJson.Length - 3) + "]");
                    }
                    List<Movie> movieList = new List<Movie>();


                    using (StreamReader r = new StreamReader(jsonFilePath))
                    {
                        string jsonReaded = r.ReadToEnd();
                        movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonReaded);
                    }


                    foreach (var item in _moviedal.GetAll())
                    {
                        _moviedal.Delete(item);
                    }

                    try
                    {
                        int sayac = 0;
                        foreach (var item in movieList)
                        {
                            if (sayac < maxMovieCount)   //maksimum 1000 adet film ile sınırlıdır
                            {
                                _moviedal.Add(item);
                                sayac++;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                   
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            #endregion




           

            
        }


        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
        }


    }
}
