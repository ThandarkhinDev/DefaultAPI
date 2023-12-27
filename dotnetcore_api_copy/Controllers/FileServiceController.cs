using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MFI
{
    [Route("api/[controller]")]
    public class FileServiceController : BaseController
    {
        [HttpGet("Download/{functionname}/{param}", Name = "Download")]
        //[Authorize]
        public string DownLoadFile(string functionname, string param)
        {
            string path = "";
            string ext = "";
            byte[] data = Convert.FromBase64String(param);
            string decodedString = Encoding.UTF8.GetString(data);
            Char delimiter = '/';
            String[] substrings = decodedString.Split(delimiter);
            if (substrings.Length >= 2)
            {
                path = substrings[0];
                ext = substrings[1];
            }

            var appsettingbuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var Configuration = appsettingbuilder.Build();
            string folderPath = "";
            if (functionname == "GetAdminPhoto")
            {
                folderPath = Configuration.GetSection("appSettings:uploadAdminPath").Value;
            }
            else if (functionname == "GetCVImage")
            {
                folderPath = Configuration.GetSection("appSettings:uploadCVImagePath").Value;
            }
             else if (functionname == "GetEducationImage")
            {
                folderPath = Configuration.GetSection("appSettings:uploadEducationImagePath").Value;
            }
            else if (functionname == "GetExperienceImage")
            {
                folderPath = Configuration.GetSection("appSettings:uploadExpereinceImagePath").Value;
            }
            else if (functionname == "GetCVVideo")
            {
                folderPath = Configuration.GetSection("appSettings:uploadVideoPath").Value;
            }
            else if (functionname == "GetLogo")
            {
                folderPath = Configuration.GetSection("appSettings:uploadLogoImage").Value;
            }

            string fullPath = folderPath + path + "." + ext;

            if (!System.IO.File.Exists(fullPath))
            {
                int lstindex = fullPath.LastIndexOf(".");
                string subString = fullPath.Substring(lstindex + 1, fullPath.Length - (lstindex + 1));
                string concatStr = "0.jpg";
                lstindex = fullPath.LastIndexOf(@"\");
                subString = fullPath.Substring(0, lstindex + 1);
                fullPath = subString + concatStr;
            }
            try
            {
                if(ext == "mp4" || ext == "flv")
                {
                    string vdoname = path + "." + ext;
                    return vdoname;
                }else{
                //fullPath = "dfd";
                    var fformat = "png";
                    if(ext != "png")
                        fformat = "jpeg";
                    byte[] m_Bytes = ReadToEnd(new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read));
                    Response.ContentType = "image/html";
                    string imageBase64Data = Convert.ToBase64String(m_Bytes);
                    string imageDataURL = string.Format("data:image/" + fformat + ";base64,{0}", imageBase64Data);
                    return imageDataURL;
                }
            }
            catch (Exception ex)
            {
                Globalfunction.WriteSystemLog(ex.Message);
                Response.StatusCode = 500;
                return null;
            }

        }

        [HttpPost("Upload/{functionname}/{fileparam}", Name = "Upload")]
        [Authorize]
        public string FileUpload(String functionname, string fileparam)
        {
            string filename = "";
            string ext = "";
            byte[] data = Convert.FromBase64String(fileparam);
            string decodedString = Encoding.UTF8.GetString(data);
            Char delimiter = '/';
            String[] substrings = decodedString.Split(delimiter);
            if (substrings.Length >= 1)
                filename = substrings[0];
            if (substrings.Length >= 2)
                ext = substrings[1];

            string returnStr = "Fail to Upload";
            try
            {
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    var appsettingbuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                    var Configuration = appsettingbuilder.Build();
                    string folderPath = "";
                    string fullPath = "";
                    if (functionname == "uploadAdminPhoto")
                    {
                        folderPath = Configuration.GetSection("appSettings:uploadAdminPath").Value;
                        fullPath = folderPath + filename + "." + ext;
                    }
                    else if (functionname == "uploadVideoFile")
                    {
                        folderPath = Configuration.GetSection("appSettings:uploadVideoPath").Value;
                        fullPath = folderPath + filename + "." + ext;
                    }
                    else if (functionname == "uploadCVImage")
                    {
                        folderPath = Configuration.GetSection("appSettings:uploadCVImagePath").Value;
                        fullPath = folderPath + filename + "." + ext;
                    }
                     else if (functionname == "uploadEducationImage")
                    {
                        folderPath = Configuration.GetSection("appSettings:uploadEducationImagePath").Value;
                        fullPath = folderPath + filename + "." + ext;
                    }
                      else if (functionname == "uploadExperienceImage")
                    {
                        folderPath = Configuration.GetSection("appSettings:uploadExpereinceImagePath").Value;
                        fullPath = folderPath + filename + "." + ext;
                    }
                    else if (functionname == "uploadLogoImage")
                    {
                        folderPath = Configuration.GetSection("appSettings:uploadLogoImage").Value;
                        fullPath = folderPath + filename + "." + ext;
                    }

                    // Save the file
                    var file = files[0];
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                    returnStr = "Succesfully Upload";
                }

            }
            catch (Exception ex)
            {
                Globalfunction.WriteSystemLog(ex.Message);

            }
            return returnStr;
        }
        public static byte[] ReadToEnd(System.IO.Stream inputStream)
        {
            if (!inputStream.CanRead)
            {
                throw new ArgumentException();
            }

            // This is optional
            if (inputStream.CanSeek)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }

            byte[] output = new byte[inputStream.Length];
            int bytesRead = inputStream.Read(output, 0, output.Length);
            inputStream.Dispose();//.Close();
            //Debug.Assert(bytesRead == output.Length, "Bytes read from stream matches stream length");
            return output;
        }
    }
}