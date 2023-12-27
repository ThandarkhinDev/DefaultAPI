using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using MFI.Repository;
using System;

namespace MFI

{

    [Route("api/[controller]")]
    public class TestaController : BaseController
    {
        private IRepositoryWrapper _repositoryWrapper;

        public TestaController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        [HttpGet("GetIncomeList", Name = "GetIncomeList")]
        [Authorize]
        public dynamic GetIncomeList([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
            var mainQuery  = Enumerable.Repeat(new
            {
                IncomeID = default(int),
                DonarName = default(string),
                Amount = default(decimal),
                // created_date = default(DateTime),
                // modified_date = default(DateTime),
                // transaction_date = default(DateTime),
            }, 0).AsQueryable();

          /*   var objstr = HttpContext.Request.Form["formDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            dynamic obj = param;   
            int currentPage = obj.skip;
            int rowsPerPage = obj.take;
            string sortField = null;
            string sortBy = "";
            if( obj.sort.Count > 0)
            {
                var sort =  obj.sort[0];
                sortBy = sort.dir == null ? sortBy : sort.dir.Value;
                sortField = sort.field.Value;
            }
            if(sortField == null || sortField == "")
                sortField = "IncomeID";
            if(sortBy == null || sortBy == "")
                sortBy = "desc";
          
            mainQuery = _repositoryWrapper.Income.GetIncomes(); // admin table + state 

            for(int i = 0 ; i < obj.filter.filters.Count;i++)
            {
                string filterName = obj.filter.filters[i].field;
                var filterValue = obj.filter.filters[i].value;
                if(filterName == "IncomeID")
                {   int IncomeID = filterValue;
                    mainQuery = mainQuery.Where(x=> x.IncomeID == IncomeID);
                }
                else if(filterName == "DonarName")
                {   string DonarName = filterValue;
                  mainQuery = mainQuery.Where(x => x.DonarName.Contains(DonarName));
                }
                else if(filterName == "Amount")
                {
                    decimal Amount = filterValue;
                   mainQuery = mainQuery.Where(x => x.Amount == Amount);
                }
                // else if(filterName == "Email")
                // {
                //     string Email = filterValue;
                //     mainQuery = mainQuery.Where(x => x.Email.Contains(Email));
                // } 


                // else if(filterName == "transaction_date")
                // {
                //     DateTime TransactionDate = filterValue;
                //     mainQuery = mainQuery.Where(x => x.transaction_date == TransactionDate);
                // } 
            }

            var objSort = new SortModel();
            objSort.ColId = sortField;
            objSort.Sort = sortBy;
            var sortList = new System.Collections.Generic.List<SortModel>();
            sortList.Add(objSort);
            mainQuery = mainQuery.OrderBy(sortList); 
            if(mainQuery.Count() < currentPage + 1 ) currentPage = 0; // reset paging if total found count is less than current page number
            var tmpList = PaginatedList<dynamic>.Create(mainQuery,currentPage,rowsPerPage);
            var objtotal = tmpList.TotalRecords;
            
            var objresult = tmpList.Select(x=>
                new{
                    IncomeID = x.IncomeID,
                    DonarName = x.DonarName,
                    Amount = x.Amount,
                    TransactionDate = x.transaction_date,
                    
                }
            ).ToList();

            dynamic objresponse = new { data = objresult, dataFoundRowsCount = objtotal };
            return objresponse;
        }

        [HttpPost("GetIncome", Name = "GetIncome")]
        [Authorize]
        public dynamic GetIncome(
            [FromBody] Newtonsoft.Json.Linq.JObject param
            )
        {
            var mainQuery  = Enumerable.Repeat(new
            {
                IncomeID = default(int),
                DonarName = default(string),
                Amount = default(decimal),
                // created_date = default(DateTime),
                // modified_date = default(DateTime),
                 transaction_date = default(DateTime),
            }, 0).AsQueryable();

          /*   var objstr = HttpContext.Request.Form["formDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            
            dynamic obj = param;   
            int currentPage = obj.skip;
            int rowsPerPage = obj.take;
            string sortField = null;
            string sortBy = "";
            if( obj.sort.Count > 0)
            {
                var sort =  obj.sort[0];
                sortBy = sort.dir == null ? sortBy : sort.dir.Value;
                sortField = sort.field.Value;
            }
            if(sortField == null || sortField == "")
                sortField = "IncomeID";
            if(sortBy == null || sortBy == "")
                sortBy = "desc";
          
            mainQuery = _repositoryWrapper.Income.GetIncomes(); // admin table + state 

            for(int i = 0 ; i < obj.filter.filters.Count;i++)
            {
                string filterName = obj.filter.filters[i].field;
                var filterValue = obj.filter.filters[i].value;
                if(filterName == "IncomeID")
                {   int IncomeID = filterValue;
                    mainQuery = mainQuery.Where(x=> x.IncomeID == IncomeID);
                }
                else if(filterName == "DonarName")
                {   string DonarName = filterValue;
                    mainQuery = mainQuery.Where(x => x.DonarName.Contains(DonarName));
                }
                else if(filterName == "Amount")
                {
                    decimal Amount = filterValue;
                    mainQuery = mainQuery.Where(x => x.Amount == Amount);
                }
            //     // else if(filterName == "Email")
            //     // {
            //     //     string Email = filterValue;
            //     //     mainQuery = mainQuery.Where(x => x.Email.Contains(Email));
            //     // } 

                
                else if(filterName == "transaction_date")
                {
                    DateTime transaction_date = filterValue;
                    mainQuery = mainQuery.Where(x => x.transaction_date == transaction_date);
                } 
            }

            var objSort = new SortModel();
            objSort.ColId = sortField;
            objSort.Sort = sortBy;
            var sortList = new System.Collections.Generic.List<SortModel>();
            sortList.Add(objSort);
            mainQuery = mainQuery.OrderBy(sortList); 
            if(mainQuery.Count() < currentPage + 1 ) currentPage = 0; // reset paging if total found count is less than current page number
            var tmpList = PaginatedList<dynamic>.Create(mainQuery,currentPage,rowsPerPage);
            var objtotal = tmpList.TotalRecords;
            
            var objresult = tmpList.Select(x=>
                new{
                    IncomeID = x.IncomeID,
                    DonarName = x.DonarName,
                    Amount = x.Amount,
                    transaction_date = x.transaction_date,
                    
                }
            ).ToList();


            dynamic objresponse = new { data = objresult, dataFoundRowsCount = objtotal };
            // dynamic objresponse = new { data = mainQuery};
            return objresponse;
        }
      
    }
}