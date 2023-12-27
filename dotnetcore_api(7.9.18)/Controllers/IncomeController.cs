
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using MFI.Repository;

using System;

namespace MFI
{
    [Route("api/[controller]")]
    public class IncomeController: BaseController
    {
        private IRepositoryWrapper _repositoryWrapper;
        public IncomeController(IRepositoryWrapper RW) {
            _repositoryWrapper = RW;
        }

        

        [HttpPost("GetAllIncomes", Name="GetAllIncomes")]
        // [Authorize]
        public dynamic GetAllIncomes() {
            dynamic objresponse = null;


            // dynamic obj = param;   
            // int currentPage = obj.skip;
            // int rowsPerPage = obj.take;
            // string sortField = null;
            // string sortBy = "";
            // if( obj.sort.Count > 0)
            // {
            //     var sort =  obj.sort[0];
            //     sortBy = sort.dir == null ? sortBy : sort.dir.Value;
            //     sortField = sort.field.Value;
            // }
            // if(sortField == null || sortField == "")
            //     sortField = "AdminID";
            // if(sortBy == null || sortBy == "")
            //     sortBy = "desc";
          
            // mainQuery = _repositoryWrapper.Admin.GetAdmins(); // admin table + state 

            // for(int i = 0 ; i < obj.filter.filters.Count;i++)
            // {
            //     string filterName = obj.filter.filters[i].field;
            //     var filterValue = obj.filter.filters[i].value;
            //     if(filterName == "AdminID")
            //     {   int AdminID = filterValue;
            //         mainQuery = mainQuery.Where(x=> x.AdminID == AdminID);
            //     }
            //     else if(filterName == "AdminName")
            //     {   string AdminName = filterValue;
            //       mainQuery = mainQuery.Where(x => x.AdminName.Contains(AdminName));
            //     }
            //     else if(filterName == "phone")
            //     {
            //         string phone = filterValue;
            //        mainQuery = mainQuery.Where(x => x.phone == phone);
            //     }
            //     else if(filterName == "Email")
            //     {
            //         string Email = filterValue;
            //         mainQuery = mainQuery.Where(x => x.Email.Contains(Email));
            //     } 
            //     else if(filterName == "state")
            //     {
            //         int stateid = filterValue;
            //         mainQuery = mainQuery.Where(x => x.stateId == stateid);
            //     } 
            // }

            // var objSort = new SortModel();
            // objSort.ColId = sortField;
            // objSort.Sort = sortBy;
            // var sortList = new System.Collections.Generic.List<SortModel>();
            // sortList.Add(objSort);
            // mainQuery = mainQuery.OrderBy(sortList); 
            // if(mainQuery.Count() < currentPage + 1 ) currentPage = 0; // reset paging if total found count is less than current page number
            // var tmpList = PaginatedList<dynamic>.Create(mainQuery,currentPage,rowsPerPage);
            // var objtotal = tmpList.TotalRecords;
            



            var _allIncomeByDateRange =   _repositoryWrapper.Income.FindAll();

            return objresponse = new {data = _allIncomeByDateRange};
        }

        [HttpGet("GetTotalIncWoDates", Name="GetTotalIncWoDates")]
        // [Authorize]
        public dynamic GetTotalIncWoDates() {
            dynamic objresponse = null;

            decimal openingforInc=0;

            var _incOpening =  _repositoryWrapper.Income.FindAll();

            foreach(var inc1 in _incOpening){
                openingforInc+=inc1.Amount;
            }
            return objresponse = new {data = openingforInc};
        }

        
        [HttpPost("checkDuplicate", Name = "checkDuplicate")]
        [Authorize]
          public dynamic checkDuplicate([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
          /*   var objstr = HttpContext.Request.Form["formDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            dynamic obj = param;
            dynamic objresult = null;
            int IncomeID = obj.IncomeID != null ? obj.IncomeID : 0;
            string DonarName = obj.DonarName;
            decimal Amount = obj.Amount;
           
            var duplicateDonarName = _repositoryWrapper.Income.CheckDuplicateIncomeDonarName(IncomeID, DonarName);
            var duplicateAmount = _repositoryWrapper.Income.CheckDuplicateIncomeAmount(IncomeID, Amount);
   

            objresult = new {   
                                donarName = duplicateDonarName > 0 ? 1 : 0, 
                                amount = duplicateAmount > 0 ? 1 : 0, 
                             
                            };
                             

            List<dynamic> dd = new List<dynamic>();
            dd.Add(objresult);
            dynamic objresponse = new { data = dd };
            return objresponse;
        }

        //[HttpPost("AddIncome", Name="AddIncome")]
       //[Authorize]
        // public dynamic AddIncome() {
        //     dynamic objresponse = null;
        //     var objstr = HttpContext.Request.Form["SampleDataObj"];
        //     dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr);
        //     int id = obj.IncomeID;
        //     var _objSample = _repositoryWrapper.Income.GetIncomeById(id);
        //     if(_objSample != null) {
        //         _objSample.DonarName = obj.DonarName;
        //         _objSample.Amount = obj.Amount;
        //         _repositoryWrapper.Income.Update(_objSample);
        //     }else {
        //         var _newObj = new Income();
        //         _newObj.DonarName = obj.DonarName;
        //         _newObj.Amount = obj.Amount;
        //         _newObj.transaction_date = DateTime.Now;
        //         _newObj.created_date = DateTime.Now;
        //         _newObj.modified_date = DateTime.Now;
        //         _repositoryWrapper.Income.Create(_newObj);
        //         id = _newObj.IncomeID;
        //     }
        //     return objresponse = new { data = id };
        // }


        // [HttpPost("AddIncome", Name = "AddIncome")]
        // //[Authorize]
        // public dynamic AddIncome([FromBody] Newtonsoft.Json.Linq.JObject param)
        // {
        //     dynamic obj = param;

        //     int IncomeID = obj.IncomeID != null ? obj.IncomeID : 0;
        //     string Income = obj.Income;
          
        //     Income objIncome = _repositoryWrapper.Income.GetIncomeById(IncomeID);
        //     if (objIncome != null)
        //     {
        //         objIncome.DonarName = obj.DonarName.Value;
        //         objIncome.Amount = obj.Amount.Value;
        //         // objIncome.transaction_date = obj.transaction_date.Value;
        //         // objIncome.modified_date = DateTime.Now;
               
                              
        //         _repositoryWrapper.Income.Update(objIncome);
        //         _repositoryWrapper.EventLog.Update("Update Income", objIncome);
        //     }
        //     else
        //     {
        //         Income newobj = new Income();
        //         newobj.DonarName = obj.DonarName;
        //         newobj.Amount = obj.Amount;
        //         // newobj.transaction_date = obj.transaction_date;
        //         // newobj.modified_date = DateTime.Now;
             
        //         _repositoryWrapper.Income.Create(newobj);
        //             IncomeID = newobj.IncomeID;
        //         _repositoryWrapper.EventLog.Insert("Add Income", newobj);
        //     }

        //     dynamic objresponse = new { data = IncomeID };
        //     return objresponse;
        // }

        
        [HttpPost("AddIncomeSetup", Name = "AddIncomeSetup")]
        [Authorize]
        public dynamic AddIncomeSetup([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
            /* var objstr = HttpContext.Request.Form["formDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            dynamic obj = param;

            int IncomeID = obj.IncomeID != null ? obj.IncomeID : 0;
            string Income = obj.Income;
          
            Income objIncome = _repositoryWrapper.Income.GetIncomeById(IncomeID);
            if (objIncome != null)
            {
                objIncome.DonarName = obj.DonarName.Value;
                objIncome.Amount = (int)obj.Amount;
                objIncome.modified_date = DateTime.Now;


                _repositoryWrapper.Income.Update(objIncome);
                // _repositoryWrapper.EventLog.Update("Update Income", objIncome);
                 IncomeID = objIncome.IncomeID;
            }
           
            else
            {
                Income newobj = new Income();
                newobj.DonarName=obj.DonarName;
                newobj.Amount=obj.Amount;
                newobj.transaction_date=DateTime.Now;
                newobj.created_date=DateTime.Now;
                newobj.modified_date=DateTime.Now;
                _repositoryWrapper.Income.Create(newobj);
                
            }

            dynamic objresponse = new { data = IncomeID };
            return objresponse;
        }

        [HttpPost("DeleteIncome/", Name="DeleteIncome")]
        [Authorize]
        public dynamic DeleteIncome() {
            dynamic objresponse = null;
            var objstr = HttpContext.Request.Form["SampleDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr);
            int id = obj.IncomeID;
            var _objSample = _repositoryWrapper.Income.GetIncomeById(id);
            _objSample.IncomeID = obj.IncomeID;
            _repositoryWrapper.Income.Delete(_objSample);
            return objresponse = new { data = id+"Deleted" };
        }

        
        [HttpDelete("DeleteIncome1/{IncomeID}", Name = "DeleteIncome1")]
        [Authorize]
        public dynamic DeleteIncome1(int IncomeID)
        {
            bool retBool = false;

            //validateDelete
            var userresult = (_repositoryWrapper.EventLog.GetEventLogByUser(IncomeID, "income")).ToList();
            if (userresult.Count > 0)
            {
                retBool = false;
            }
            else
            {
                Income objIncome =_repositoryWrapper.Income.GetIncomeById(IncomeID);
                if (objIncome != null)
                {
                    _repositoryWrapper.Income.Delete(objIncome);
                    retBool = true;
                    _repositoryWrapper.EventLog.Delete("Delete Income", objIncome);
                }
            }
            dynamic objresponse = new { data = retBool };
            return objresponse;
        }

        [HttpPost("GetIncc/{date}", Name="GetIncc")]
        [Authorize]
        public dynamic GetIncc(DateTime date) {
            dynamic objresponse = null;
            var _objSample = _repositoryWrapper.Income.GetIncomeByDateRange(date);
            return objresponse = new { data = _objSample };
        } 

        [HttpGet("GetIncOpenings/{year}/{month}", Name="GetIncOpenings")]
        [Authorize]
        public dynamic GetIncOpenings(int year, int month) {
            dynamic objresponse = null;
            var _opening =  _repositoryWrapper.Income.GetIncOpening(year,month);
            return objresponse = new {data = _opening};
        }

        [HttpPost("GetIncomeList1", Name = "GetIncomeList1")]
        [Authorize]
        public dynamic GetIncomeList1([FromBody] Newtonsoft.Json.Linq.JObject param)
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

       

    }
}