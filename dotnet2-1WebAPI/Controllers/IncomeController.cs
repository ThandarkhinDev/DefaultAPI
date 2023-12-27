using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;


namespace dotnet2_1WebAPI
{
    [Route("api/[controller]")]//Declaring restful Api or web api, and now is webAPI.
    [ApiController]
    public class IncomeController : BaseController //Base on the BaseController.
    {
        private AppDb _objdb;

        public IncomeController(AppDb DB) //DB is parameter, 
        {
            _objdb = DB;
        } 

        [HttpGet("GetIncome/{IncomeID}", Name = "GetIncome")]
        /* Declaring GetIncome fx is HttpGet and 
        "GetIncome/{IncomeID}" is routing. 
        */
        public dynamic GetIncome(int IncomeID)
        {
            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" }; //using data and msg means it's json.
            

            var mainQuery = (from main in _objdb.Income
                             select main);// means select * from tblIncome and place in main Query
            if (IncomeID != 0)
            {
                mainQuery = (from main in mainQuery
                            where main.IncomeID == IncomeID
                            select main);

            } 
            
            var list = mainQuery.ToList();// ToList() means giving select comd and give json in list variable.
            
            objresponse = new { data = list, msg = (list != null && list.Count > 0) ? "Success" : "No Record Found" };   
        
            return objresponse;
        }

        [HttpGet("GetIncomeByName/{name}", Name = "GetIncomeByName")]
        public dynamic GetIncomeByName(string name)
        {
            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" };
            
            var mainQuery = (from main in _objdb.Income
                             where main.DonarName == name
                            select main);
            
            var list = mainQuery.ToList();
            
            objresponse = new { data = list, msg = (list != null && list.Count > 0) ? "Success" : "No Record Found" };   
        
            return objresponse;
        }

        [HttpGet("GetIncomeByDateRange/{fromdate}/{todate}", Name = "GetIncomeByDateRange")]
        public dynamic GetIncomeByDateRange(DateTime FromDate, DateTime ToDate)
        {

            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" };
            
            var mainQuery = (from main in _objdb.Income
                             where main.TransactionDate >= FromDate && main.TransactionDate < ToDate.AddDays(1)
                             select main);
            
            var list = mainQuery.ToList();
            
            objresponse = new { data = list, msg = (list != null && list.Count > 0) ? "Success" : "No Record Found" };   
        
            return objresponse;
        }
        
        [HttpPost("SaveIncome", Name = "SaveIncome")]
        public dynamic SaveIncome()
        {
            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" };
            string decodedString = "";
    
            //ZG90bmV0QVBJ
            string token = token = HttpContext.Request.Form["token"].ToString(); 
            try
            {           
                byte[] data = Convert.FromBase64String(token);
                decodedString = Encoding.UTF8.GetString(data);
            }catch
            {
              objresponse = new { data = 0 , msg = "Invalid Token"};
            }
            
            if(decodedString == "dotnetAPI")
            {
                var objstr = HttpContext.Request.Form["objInfo"];
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr);

                int IncomeID = obj.IncomeID;
                var objIncome = _objdb.Income.Find(IncomeID);
                if (objIncome != null)
                {                    
                    //objIncome.TransactionDate = obj.TransactionDate;
                    objIncome.DonarName = obj.DonarName;
                    objIncome.Amount = obj.Amount;
                    objIncome.TransactionDate=DateTime.Now;
                                 
                    _objdb.Update(objIncome);
                    _objdb.SaveChanges();
                }
                else
                {
                    if(IncomeID > 0)
                    {
                        objresponse = new { data = 0 , msg = "IncomeID ID is invalid" };
                    }
                    else
                    {
                        var newobj = new Income();
                        newobj.TransactionDate = System.DateTime.Now;
                        newobj.DonarName = obj.DonarName;
                        newobj.Amount = obj.Amount;                       
                        newobj.created_date = System.DateTime.Now;
                        newobj.modified_date = System.DateTime.Now;
                       
                        _objdb.Add(newobj);
                        _objdb.SaveChanges();
                        IncomeID = newobj.IncomeID;
                    }
                }

                objresponse = new { data = IncomeID , msg = "Save Successfully"};
            }

           return objresponse;
        } 

        [HttpPost("DeleteIncome/{IncomeID}", Name = "DeleteIncome")]
        public dynamic DeleteIncome(int IncomeID)
        {
           bool retBool = false;
           string message = "";
           string decodedString = "";
            //ZG90bmV0QVBJ
            string token = token = HttpContext.Request.Form["token"].ToString(); 
            try
            {           
                byte[] data = Convert.FromBase64String(token);
                decodedString = Encoding.UTF8.GetString(data);
            }catch
            {
                message =  "Invalid Token";
            }

            if(decodedString == "dotnetAPI")
            {
                var objIncome = _objdb.Income.Find(IncomeID);
                if (objIncome != null)
                {
                    _objdb.Remove(objIncome);
                    _objdb.SaveChanges();
                    retBool = true;
                    message = "Deleted Successfully";
                }
                else
                    message = "Invalid ID";
            }
            dynamic objresponse = new { data = retBool, msg = message };
            return objresponse;
        }  

        [HttpGet("GetIncomeReportByDateRange/{fromdate}/{todate}", Name = "GetIncomeReportByDateRange")]
        public dynamic GetIncomeReportByDateRange(DateTime FromDate, DateTime ToDate)
        {
            /*DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();

            FromDate = fromDate;
            ToDate = toDate;*/

            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" };
            
            var mainQuery = (from main in _objdb.Income
                             where main.TransactionDate >= FromDate && main.TransactionDate < ToDate.AddDays(1)
                             select main.Amount);
            
            var list = mainQuery.ToList();
            
            var total= list.Sum();

            objresponse = new { data = total, msg = (total != 0) ? "Total Income" : "No Income" };   
            
            return objresponse;
        }

        [HttpGet("GetCurrentIncome/{month}", Name = "GetCurrentIncome")]
        public dynamic GetCurrentIncome(DateTime month)
        {
            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" };
            
            var mainQuery = (from main in _objdb.Income
                             where main.TransactionDate <= month 
                                select main);
            var list = mainQuery.ToList();

            objresponse = new { data = list, msg = (list != null) ? "Total Income" : "No Income" };   
            
            return objresponse;
    
        }


    
 

    }
}
