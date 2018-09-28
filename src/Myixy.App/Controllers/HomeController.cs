using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Myixy.App.Data;
using Myixy.App.Models;

namespace Myixy.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _appEnvironment;

        public HomeController(AppDbContext context, ILogger<HomeController> logger, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        //[Authorize]
        [HttpGet] //1.Load 
        public IActionResult Upload_User_Profil_Image()
        {
            //--< Upload Form >-- 
            return View();
            //--</ Upload Form >-- 
        }

        //[Authorize]
        [HttpPost] //Postback 
        public async Task<IActionResult> Upload_User_Profil_Image(IFormFile uploaded_File)
        {
            //https://codedocu.com/Net-Framework/ASP_dot_Net-Core/Files-Folders/Asp_dot_Net-Core_colon_-Sample-Code-Photo-Upload-and-Resize-Photo?2210
            //--------< Upload_ImageFile() >-------- 
            string sResult = "";

            //< check > 
            if (uploaded_File == null || uploaded_File.Length == 0)
            {
                ViewData["Error"] = "Error:file not selected";
                return View(ViewData);
            }

            if (uploaded_File.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) < 0)
            {
                ViewData["Error"] = "Error:This file is not an image";
                return View(ViewData);
            }
            //</ check > 

            //--< Get User ID >-- 
            //internal referenz-Number for tracking in tables 
            long IDUser = 10;// await Common.ExtensionMethods.getIDUser_Number(this.User, _context);
            //--</ Get User ID >-- 

            //< init > 
            string sImage_Folder = "User_Images";
            string sTarget_Filename = "User_Image_" + IDUser + ".jpg";
            //</ init > 

            //< get Path > 
            string sPath_WebRoot = _appEnvironment.WebRootPath;
            string sPath_of_Target_Folder = sPath_WebRoot + "\\User_Files\\" + sImage_Folder + "\\";
            if (!Directory.Exists(sPath_of_Target_Folder))
            {
                Directory.CreateDirectory(sPath_of_Target_Folder);
            }

            string sPath_of_Original_Folder = sPath_of_Target_Folder + "\\Original\\";
            if (!Directory.Exists(sPath_of_Original_Folder))
            {
                Directory.CreateDirectory(sPath_of_Target_Folder);
            }

            string sFile_Target_Original = sPath_of_Original_Folder + sTarget_Filename;
            //string sImage_Filename_Original = sPath_of_Target_Folder + uploaded_File.FileName; 
            //</ get Path > 

            //< Copy File to Target > 
            using (var stream = new FileStream(sFile_Target_Original, FileMode.Create))
            {
                await uploaded_File.CopyToAsync(stream);
            }
            //</ Copy File to Target > 

            //< resize > 
            Image_resize(sFile_Target_Original, sPath_of_Target_Folder + "\\40\\" + sTarget_Filename, 40);
            Image_resize(sFile_Target_Original, sPath_of_Target_Folder + "\\80\\" + sTarget_Filename, 80);
            //Image_resize(sFile_Target_Original, sPath_of_Target_Folder + "\\120\\" + sTarget_Filename, 120);
            //Image_resize(sFile_Target_Original, sPath_of_Target_Folder + "\\240\\" + sTarget_Filename, 240);
            //Image_resize(sFile_Target_Original, sPath_of_Target_Folder + "\\400\\" + sTarget_Filename, 400);
            //</ resize > 

            //string sResult= await Safe_Uploaded_Image(ref uploaded_File, "User_Images"); 


            //< output > 
            ViewData["FileUploaded"] = "/User_Files/User_Images/Original/" + sTarget_Filename;
            ViewData["FileResized_40"] = "/User_Files/User_Images/40/" + sTarget_Filename;
            ViewData["FileResized_80"] = "/User_Files/User_Images/80/" + sTarget_Filename;
            //ViewData["FileResized_120"] = "/User_Files/User_Images/120/" + sTarget_Filename;
            //ViewData["FileResized_240"] = "/User_Files/User_Images/240/" + sTarget_Filename;
            //ViewData["FileResized_400"] = "/User_Files/User_Images/400/" + sTarget_Filename;
            return View();
            //</ output > 
            //--------</ Upload_ImageFile() >-------- 
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void Image_resize(string input_Image_Path, string output_Image_Path, int new_Width)
        {
            //---------------< Image_resize() >--------------- 
            //*Resizes an Image in Asp.Net MVC Core 2 
            //*Using Nuget CoreCompat.System.Drawing 
            //using System.IO 
            //using System.Drawing;             //CoreCompat 
            //using System.Drawing.Drawing2D;   //CoreCompat 
            //using System.Drawing.Imaging;     //CoreCompat 

            const long quality = 50L;
            Bitmap source_Bitmap = new Bitmap(input_Image_Path);

            double dblWidth_origial = source_Bitmap.Width;
            double dblHeigth_origial = source_Bitmap.Height;
            double relation_heigth_width = dblHeigth_origial / dblWidth_origial;
            int new_Height = (int)(new_Width * relation_heigth_width);

            //< create Empty Drawarea > 
            var new_DrawArea = new Bitmap(new_Width, new_Height);
            //</ create Empty Drawarea > 

            using (var graphic_of_DrawArea = Graphics.FromImage(new_DrawArea))
            {
                //< setup > 
                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;
                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;
                //</ setup > 

                //< draw into placeholder > 
                //*imports the image into the drawarea 
                graphic_of_DrawArea.DrawImage(source_Bitmap, 0, 0, new_Width, new_Height);
                //</ draw into placeholder > 

                //--< Output as .Jpg >-- 
                using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))
                {
                    //< setup jpg > 
                    var qualityParamId = Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                    //</ setup jpg > 

                    //< save Bitmap as Jpg > 
                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    new_DrawArea.Save(output, codec, encoderParameters);
                    //resized_Bitmap.Dispose(); 
                    output.Close();
                    //</ save Bitmap as Jpg > 
                }
                //--</ Output as .Jpg >-- 
                graphic_of_DrawArea.Dispose();
            }
            source_Bitmap.Dispose();
            //---------------</ Image_resize() >--------------- 
        }

    }
}

