using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;


namespace dotnet2_1WebAPI
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReportController : BaseController
  {
    private AppDb _objdb;

    public ReportController(AppDb DB)
    {
      _objdb = DB;
    }

    [HttpGet("GetReportByDateRange/{FromMonth}", Name = "GetReportByDateRange")]
    public dynamic GetReportByDateRange(DateTime FromMonth)
    {
      /*DateTime FromDate = new DateTime();
      DateTime ToDate = new DateTime();

      FromDate = fromDate;
      ToDate = toDate;*/


      //DateTime janFirst = new DateTime();
      // janFirst.AddDays(1);
      DateTime nowDate = DateTime.Now;
      DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);

      //String Jandate = firstDayCurrentYear.ToLongDateString();
      DateTime EndDate = FromMonth.AddMonths(1);

     
      dynamic objResult = new { 
        incomeList = 0,expenseList=0,openingAmount=0 ,msg = "Invalid Token Data" };

      decimal amountIncome, amountExpense, openingAmount = 0;

      // income amount (opening)
      var countIncomeAmount = (from a in _objdb.Income                 
                               where a.TransactionDate >= firstDayCurrentYear && a.TransactionDate < FromMonth
                               select a.Amount);
                            


      amountIncome = countIncomeAmount.ToList().Sum();
     

      // expense amount (opening)
      var countExpenseAmount = (from a in _objdb.Expense
                                where a.TransactionDate >= firstDayCurrentYear && a.TransactionDate < FromMonth
                                select a.Amount);


      amountExpense = countExpenseAmount.ToList().Sum();

      // opening amount

      openingAmount = amountIncome - amountExpense;

      //Income

      var mainQueryIncome = (from i in _objdb.Income
                             where i.TransactionDate >= FromMonth && i.TransactionDate < EndDate
                             select i);

      var incomeList = mainQueryIncome.ToList();

      //  //Expense
      var mainQueryExpense = (from e in _objdb.Expense
                              where e.TransactionDate >= FromMonth && e.TransactionDate < EndDate
                              select e);


      var expenseList = mainQueryExpense.ToList();

      objResult = new {
          incomeList,
          expenseList, 
         openingAmount,
          msg = "Success!"
           };   

      return objResult;
      


    }

  }
}
