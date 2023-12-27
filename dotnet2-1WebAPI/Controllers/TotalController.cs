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
    public class TotalController : BaseController //Base on the BaseController.
    {
        private AppDb _objdb;

        public TotalController(AppDb DB) //DB is parameter, 
        {
            _objdb = DB;
        } 

        [HttpGet("GetTotal/{fromdate}/{todate}", Name = "GetTotal")]
        public dynamic GetTotal(DateTime FromDate, DateTime ToDate)
        {
            
            dynamic objresponse = new { TotalIncome = 0 ,TotalExpense = 0 ,NetAmount = 0 , msg = "Invalid Token Data" };


            var mainQuery1 = (from main in _objdb.Income
                             where main.TransactionDate >= FromDate && main.TransactionDate < ToDate.AddDays(1)
                             select main.Amount);
            
            var list1 = mainQuery1.ToList();
            var TotalIncome = list1.Sum();


            var mainQuery2 = (from main in _objdb.Expense
                             where main.TransactionDate >= FromDate && main.TransactionDate < ToDate.AddDays(1)
                             select main.Amount);
            
            var list2 = mainQuery2.ToList();
            var TotalExpense = list2.Sum();

            var NetAmount =TotalIncome-TotalExpense;

            objresponse = new { TotalIncome , TotalExpense, NetAmount };   
            
            return objresponse;
        }

        [HttpGet("GetTotalIncomeWithoutDateRange", Name = "GetTotalIncomeWithoutDateRange")]
        public dynamic GetTotalIncomeWithoutDateRange()
        {
            
            dynamic objresponse = new { TotalIncome = 0 , msg = "Invalid Token Data" };


            var mainQuery1 = (from main in _objdb.Income
                             select main.Amount);
            
            var list1 = mainQuery1.ToList();
            var TotalIncome = list1.Sum();
            objresponse = new { data=TotalIncome, msg=(TotalIncome !=0)? "Success":"No Income"};   
            
            return objresponse;
        }

        [HttpGet("GetTotalExpenseWithoutDateRange", Name = "GetTotalExpenseWithoutDateRange")]
        public dynamic GetTotalExpenseWithoutDateRange()
        {
            
            dynamic objresponse = new { TotalExpense = 0 , msg = "Invalid Token Data" };


            var mainQuery2 = (from main in _objdb.Expense
                             select main.Amount);
            
            var list1 = mainQuery2.ToList();
            var TotalExpense = list1.Sum();
            objresponse = new { data=TotalExpense, msg=(TotalExpense !=0)? "Success":"No Income"};   
            
            return objresponse;
        }

        [HttpGet("GetTotalWithoutDate", Name = "GetTotalWithoutDate")]
        public dynamic GetTotalWithoutDate()
        {
            
            dynamic objresponse = new {NetAmount = 0 , msg = "Invalid Token Data" };

            var mainQuery1 = (from main in _objdb.Income
                             select main.Amount);
            var list1 = mainQuery1.ToList();
            var TotalIncome = list1.Sum();

            var mainQuery2 = (from main in _objdb.Expense
                             select main.Amount);
            var list2 = mainQuery2.ToList();
            var TotalExpense = list2.Sum();

            var NetAmount =TotalIncome-TotalExpense;

            objresponse = new {NetAmount};   
            
            return objresponse;
        }    
     
    }
}
