using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;

namespace MFI
{
    public class Globalfunction
    {
        public static byte[] ExportExcel(dynamic dataList, List<string> columnsHeader, List<string> fieldNames, string heading)
        {
            byte[] result = null;

            using (ExcelPackage package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add(heading);
                using (var cells = worksheet.Cells[1, 1, 1, 13])
                {
                    cells.Style.Font.Bold = true;
                    //cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //cells.Style.Fill.BackgroundColor.SetColor(Color.Green);
                }
                //First add the headers
                for (int i = 0; i < columnsHeader.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = columnsHeader[i];
                }

                //Add values
                var j = 2;
                var count = 1;
                foreach (var item in dataList)
                {
                    string jstr = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                    dynamic jobj = Newtonsoft.Json.JsonConvert.DeserializeObject(jstr);

                    int unicode = 65;
                    foreach (var field in fieldNames)
                    {
                        if (jobj.GetValue(field) != null)
                        {
                            string colindex = ((char)unicode).ToString();
                            worksheet.Cells[colindex + j].Value = jobj.GetValue(field).ToString();
                        }
                        unicode++;
                    }

                    j++;
                    count++;
                }
                result = package.GetAsByteArray();
            }

            return result;
        }

        public static byte[] ExportPDF(dynamic dataList, List<string> columnsHeader, List<string> fieldNames, float[] widths, string heading)
        {

            var document = new Document();
            var outputMS = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, outputMS);
            document.Open();
            var font5 = FontFactory.GetFont(FontFactory.HELVETICA, 11);
            if (heading == "VideoCV")
            {
                font5 = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            }

            document.Add(new Phrase(Environment.NewLine));

            //var count = typeof(UserListVM).GetProperties().Count();
            var count = columnsHeader.Count;
            var table = new PdfPTable(count);

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            var cell = new PdfPCell(new Phrase(heading));
            cell.Colspan = count;

            for (int i = 0; i < count; i++)
            {
                var headerCell = new PdfPCell(new Phrase(columnsHeader[i], font5));
                headerCell.BackgroundColor = BaseColor.Gray;
                table.AddCell(headerCell);
            }

            var sn = 1;
            foreach (var item in dataList)
            {
                string jstr = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                dynamic jobj = Newtonsoft.Json.JsonConvert.DeserializeObject(jstr);
                foreach (var field in fieldNames)
                {
                    table.AddCell(new Phrase(jobj.GetValue(field).ToString(), font5));
                }

                sn++;
            }

            document.Add(table);
            document.Close();
            var result = outputMS.ToArray();

            return result;
        }
        public static byte[] DownloadPDF(dynamic dataList, List<string> columnsHeader, List<string> fieldNames, float[] widths, string heading)
        {
            var document = new Document();
            var outputMS = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, outputMS);
            document.Open();
            var font5 = FontFactory.GetFont(FontFactory.HELVETICA, 11);
            if (heading == "Applicant Data")
            {
                font5 = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            }
            document.Add(new Phrase(Environment.NewLine));
            var count = 2;
            var table = new PdfPTable(count);
            table.SetWidths(widths);

            table.WidthPercentage = 100;
            var cell = new PdfPCell(new Phrase(heading));
            cell.Colspan = count;
            var sn = 0;
            foreach (var item in fieldNames)
            {
                table.AddCell(new Phrase(columnsHeader[sn].ToString(), font5));
                if (dataList.GetValue(item) == null)
                {
                    table.AddCell(new Phrase("", font5));
                }
                else
                {
                    table.AddCell(new Phrase(dataList.GetValue(item).ToString(), font5));
                }

                sn++;
            }
            document.Add(table);
            document.Close();
            var result = outputMS.ToArray();

            return result;
        }
        public static dynamic SendEmailAsync(List<Setting> settingresult,string email, string FromEmail, string subject, string message, Boolean ishtml, string replytoname = "", string replytoemail = "")
        {
            
            string SMTP = "";
            string UserName = "";
            string Password = "";
            int Port = 0;//Convert.ToInt32(settingresult[3].Value);

            foreach (var s in settingresult)
            {
                if (s.Name == "SMTP")
                    SMTP = s.Value;

                if (s.Name == "Email")
                    UserName = s.Value;

                if (s.Name == "Email Password")
                    Password = s.Value;

                if (s.Name == "Server Port")
                    Port = Convert.ToInt32(s.Value);
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(FromEmail, FromEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            if (replytoemail != "")
                emailMessage.ReplyTo.Add(new MailboxAddress(replytoname, replytoemail));
            emailMessage.Subject = subject;
            if (ishtml)
                emailMessage.Body = new TextPart("html") { Text = message };
            else
                emailMessage.Body = new TextPart("html ") { Text = message };

            using (var client = new SmtpClient())//SmtpClient(new MailKit.ProtocolLogger ("smtp.log"))
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(SMTP, Port, SecureSocketOptions.Auto);
                    client.Authenticate(UserName, Password);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    //this.AddEventLog("Error", "Send Mail", ex.Message, "0", "0", "");
                    return false;
                }
            }
            return true;
        }

        public static string ObjectToString(dynamic OldObj)
        {
            string _OldObjString = "";
            try
            {
                if (OldObj == null) return "";
                JObject _duplicateObj = JObject.FromObject(OldObj);
                var _List = _duplicateObj.ToObject<Dictionary<string, object>>();
                foreach (var item in _List)
                {
                    var name = item.Key;
                    var val = item.Value;
                    string msg = name + " : " + val + "\r\n";
                    _OldObjString += msg;
                }
            }
            catch (Exception ex)
            {
                Globalfunction.WriteSystemLog("Exception :" + ex.Message);
            }
            return _OldObjString;
        }

        public static Claim[] GetClaims(TokenData obj)
        {
            var claims = new Claim[]
            {
                new Claim("UserID",obj.UserID),
                new Claim("LoginType",obj.LoginType),
                new Claim("Userlevelid", obj.Userlevelid),
                new Claim("TicketExpireDate", obj.TicketExpireDate.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, obj.Sub),
                new Claim(JwtRegisteredClaimNames.Jti, obj.Jti),
                new Claim(JwtRegisteredClaimNames.Iat, obj.Iat, ClaimValueTypes.Integer64)
            };
            return claims;
        }

        public static TokenData GetTokenData(JwtSecurityToken tokenS)
        {
            var obj = new TokenData();
            try
            {
                obj.UserID = tokenS.Claims.First(claim => claim.Type == "UserID").Value;
                obj.LoginType = tokenS.Claims.First(claim => claim.Type == "LoginType").Value;
                obj.Userlevelid = tokenS.Claims.First(claim => claim.Type == "Userlevelid").Value;
                obj.Sub = tokenS.Claims.First(claim => claim.Type == "sub").Value;
                string TicketExpire = tokenS.Claims.First(claim => claim.Type == "TicketExpireDate").Value;
                DateTime TicketExpireDate = DateTime.Parse(TicketExpire);
                obj.TicketExpireDate = TicketExpireDate;
            }
            catch (Exception ex)
            {
                WriteSystemLog(ex.Message);
            }
            return obj;
        }

        public static void WriteSystemLog(string message)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - " + message);
        }
    }
}