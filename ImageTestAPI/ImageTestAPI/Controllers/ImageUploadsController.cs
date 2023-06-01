using ImageTestAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ImageTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadsController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public ImageUploadsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public IActionResult Post([FromForm] FileUploadAPI files)
        {
            try
            {
                //List<Customer> list = JsonConvert.DeserializeObject<List<Customer>>(objFile.Customers);
                //obj.LstCustomer = list;
               
                if (files.file.Length > 0)
                {
                    if (!Directory.Exists(_environment.ContentRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.ContentRootPath + "\\Upload");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.ContentRootPath + "\\Upload\\" + files.file.FileName))
                    {
                        files.file.CopyTo(filestream);
                        filestream.Flush();
                        //  return "\\Upload\\" + objFile.files.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return StatusCode(200, _environment.WebRootPath + "\\Upload\\" + files.file.FileName);
        }
    }
}

