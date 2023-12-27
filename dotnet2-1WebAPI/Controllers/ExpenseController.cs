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
    public class ExpenseController : BaseController //Base on the BaseController.
    {
        private AppDb _objdb;

        public ExpenseController(AppDb DB) //DB is parameter, 
        {
            _objdb = DB;
        } 

        [HttpGet("GetExpense/{ID}", Name = "GetExpense")]
        public dynamic GetExpense(int ID)
        {
           // return ID;
            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" };
            
            var mainQuery = (from main in _objdb.Expense
                             select main);
            if (ID != 0)
            {
                mainQuery = (from main in mainQuery
                            where main.ID == ID
                            select main);
            }
            
            var list = mainQuery.ToList();
            
            objresponse = new { data = list, msg = (list != null && list.Count > 0) ? "Success" : "No Record Found" };   
        
            return objresponse;
        }

        [HttpGet("GetExpenseByName/{name}", Name = "GetExpenseByName")]
        public dynamic GetExpenseByName(string name)
        {
            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" };
            
            var mainQuery = (from main in _objdb.Expense
                             where main.Particular == name
                            select main);
            
            var list = mainQuery.ToList();
            
            objresponse = new { data = list, msg = (list != null && list.Count > 0) ? "Success" : "No Record Found" };   
        
            return objresponse;
        }

        [HttpGet("GetExpenseByDateRange/{fromdate}/{todate}", Name = "GetExpenseByDateRange")]
        public dynamic GetExpenseByDateRange(DateTime FromDate, DateTime ToDate)
        {
            /*DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();

            FromDate = fromDate;
            ToDate = toDate;*/

            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" };
            
            var mainQuery = (from main in _objdb.Expense
                             where main.TransactionDate >= FromDate && main.TransactionDate < ToDate.AddDays(1)
                             select main);
            
            var list = mainQuery.ToList();
            
            objresponse = new { data = list, msg = (list != null && list.Count > 0) ? "Success" : "No Record Found" };   
        
            return objresponse;
        }
        
        [HttpPost("SaveExpense", Name = "SaveExpense")]
        public dynamic SaveExpense()
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

                int ID = obj.ID;
                var objExpense = _objdb.Expense.Find(ID);
                if (objExpense != null)
                {                    
                    //objExpense.TransactionDate = obj.TransactionDate;
                    objExpense.Particular = obj.Particular;
                    objExpense.Amount = obj.Amount;
                                 
                    _objdb.Update(objExpense);
                    _objdb.SaveChanges();
                }
                else
                {
                    if(ID > 0)
                    {
                        objresponse = new { data = 0 , msg = "Expense ID is invalid" };
                    }
                    else
                    {
                        var newobj = new Expense();
                        newobj.TransactionDate = System.DateTime.Now;
                        newobj.Particular = obj.Particular;
                        newobj.Amount = obj.Amount;                       
                        newobj.created_date = System.DateTime.Now;
                        newobj.modified_date = System.DateTime.Now;
                       
                        _objdb.Add(newobj);
                        _objdb.SaveChanges();
                        ID = newobj.ID;
                    }
                }

                objresponse = new { data = ID , msg = "Save Successfully"};
            }

           return objresponse;
        } 

        [HttpDelete("DeleteExpense/{ID}", Name = "DeleteExpense")]
        public dynamic DeleteExpense(int ID)
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
                var objExpense = _objdb.Expense.Find(ID);
                if (objExpense != null)
                {
                    _objdb.Remove(objExpense);
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


        [HttpGet("GetExpenseReportByDateRange/{fromdate}/{todate}", Name = "GetExpenseReportByDateRange")]
        public dynamic GetExpenseReportByDateRange(DateTime FromDate, DateTime ToDate)
        {
            /*DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();

            FromDate = fromDate;
            ToDate = toDate;*/

            dynamic objresponse = new { data = 0 , msg = "Invalid Token Data" };
            
            var mainQuery = (from main in _objdb.Expense
                             where main.TransactionDate >= FromDate && main.TransactionDate < ToDate.AddDays(1)
                             select main.Amount);
            
            var list = mainQuery.ToList();
            
            // var total= list.Sum();

            objresponse = new { data = list.Sum(), msg = (list != null && list.Count > 0) ? "Total Expense" : "No Expense" };   
            
            return objresponse;
    
        }

    }
}
