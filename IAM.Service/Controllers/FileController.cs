using IAM.API.Handlers;
using IAM.API.Services;
using IAM.Authenticator;
using IAM.Data.Models;
using IAM.Data.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace IAM.API.Controllers
{
    [ApiController]
    public class FileController : BaseController
    {
        readonly FileHandler Handler;
        public FileController(IConfiguration configuration, IFileService service, IAuthenticator authenticator, IAnalysisService analysisService) : base(configuration)
        {
            Handler = new FileHandler(authenticator, service, configuration, analysisService);
        }
        //[HttpGet]
        //[Route("TestRedis")]
        //public IActionResult TestRedis()
        //{
        //    return Ok(Redis.Ping());
        //}
        [HttpGet]
        [Route("Download")]
        public async Task<IActionResult> DownloadAsync(string token, int id)
        {
            var res = await Handler.GetFileByIdAsync(token, id);
            var env = GetResult(res);

            // add analysis
            if(env.Success)
            {
                var countryName = HttpContext.Request.Headers.ContainsKey("countryName") ? HttpContext.Request.Headers["countryName"].ToString() : "";
                var cityName = HttpContext.Request.Headers.ContainsKey("cityName") ? HttpContext.Request.Headers["cityName"].ToString() : "";
                var continentName = HttpContext.Request.Headers.ContainsKey("continentName") ? HttpContext.Request.Headers["continentName"].ToString() : ""; 
                var IPAddress = HttpContext.Request.Headers.ContainsKey("IPAddress") ? HttpContext.Request.Headers["IPAddress"].ToString() : ""; 

                var analysisReq = new FileAnalysisRequest()
                {
                    CityName = cityName,
                    Continent = continentName,
                    CountryName = countryName,
                    IPAddress = IPAddress
                };

                Handler.AddFileAnalysis(analysisReq, id, token);
            }

            return StatusCodeResult(env);


            //try
            //{
            //    File file = null;
            //    if (env.Success)
            //    {
            //        file = (File)env.Data;
            //    }
            //    if (file == null) return BadRequest("File Not Found!");

            //    var fileName = file.Name;
            //    var fileBytes = Convert.FromBase64String(file.Data);
            //    var fileExt = file.Ext;

            //    Response.ContentType = "application/octet-stream";
            //    Response.Headers.Add("Content-Disposition", string.Format("attachment; filename={0}", fileName));
            //    //Response.Headers.Add("Content-Length", fileBytes.Length.ToString());
            //    await Response.Body.WriteAsync(fileBytes);
            //    return new EmptyResult();
            //}
            //catch (System.Exception ex)
            //{
            //    var err = GetResult(ex);
            //    return StatusCodeResult(err);
            //}
        }


        [HttpPost]
        [Route("SaveFile")]
        public IActionResult SaveFile([FromBody] RequestSaveFile file)
        {
            var res = Handler.SaveFile(file);
            var env = GetResult(res);
            return StatusCodeResult(env);
        }

        [HttpGet]
        [Route("GetFilesMeta")]
        public IActionResult GetFilesMeta(string token)
        {
            var res = Handler.GetAllFilesMeta(token);
            var env = GetResult(res);
            return StatusCodeResult(env);
        }
        [HttpGet]
        [Route("GetFileAnalysis")]
        public IActionResult GetFileAnalysis(string token, int fileId)
        {
            var res = Handler.GetFileAnalysis(token, fileId);
            var env = GetResult(res);
            return StatusCodeResult(env);
        }
    }
}
