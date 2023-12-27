
using MFI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MFI
{
    [Route("api/[controller]")]
    public class ExpenseController: BaseController
    {
        private IRepositoryWrapper _repositoryWrapper;
        public ExpenseController(IRepositoryWrapper RW) {
            _repositoryWrapper = RW;
        }

        

        [HttpPost("GetAllExpenses", Name="GetAllExpenses")]
        [Authorize]
        public dynamic GetAllExpenses() {
            dynamic objresponse = null;
            var _allExpenseByDateRange =   _repositoryWrapper.Expense.FindAll();
            return objresponse = new {data = _allExpenseByDateRange};
        }

        
        [HttpPost("checkDuplicateExpense", Name = "checkDuplicateExpense")]
        [Authorize]
          public dynamic checkDuplicateExpense([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
          /*   var objstr = HttpContext.Request.Form["formDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            dynamic obj = param;
            dynamic objresult = null;
            int ExpenseID = obj.ExpenseID != null ? obj.ExpenseID : 0;
            string ParticularName = obj.ParticularName;
            decimal Amount = obj.Amount;
           
            var duplicateParticularName = _repositoryWrapper.Expense.CheckDuplicateExpenseParticularName(ExpenseID, ParticularName);
            var duplicateAmount = _repositoryWrapper.Expense.CheckDuplicateExpenseAmount(ExpenseID, Amount);
   

            objresult = new {   
                                particularName = duplicateParticularName > 0 ? 1 : 0, 
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

        
        [HttpPost("AddExpenseSetup", Name = "AddExpenseSetup")]
        [Authorize]
        public dynamic AddExpenseSetup([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
            /* var objstr = HttpContext.Request.Form["formDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            dynamic obj = param;

            int ExpenseID = obj.ExpenseID != null ? obj.ExpenseID : 0;
            string Expense = obj.Expense;
          
            Expense objExpense = _repositoryWrapper.Expense.GetExpenseById(ExpenseID);
            if (objExpense != null)
            {
                objExpense.ParticularName = obj.ParticularName.Value;
                objExpense.Amount = (int)obj.Amount;
                objExpense.transaction_date = DateTime.Now;
                objExpense.modified_date = DateTime.Now;

                _repositoryWrapper.Expense.Update(objExpense);
                // _repositoryWrapper.EventLog.Update("Update Income", objIncome);
                 ExpenseID = objExpense.ExpenseID;
            }
           
            else
            {
                Expense newobj = new Expense();
                newobj.ParticularName=obj.ParticularName;
                newobj.Amount=obj.Amount;
                newobj.transaction_date=DateTime.Now;
                newobj.created_date=DateTime.Now;
                newobj.modified_date=DateTime.Now;
                _repositoryWrapper.Expense.Create(newobj);
                
            }

            dynamic objresponse = new { data = ExpenseID };
            return objresponse;
        }

        [HttpPost("DeleteExpense/", Name="DeleteExpense")]
        [Authorize]
        public dynamic DeleteExpense() {
            dynamic objresponse = null;
            var objstr = HttpContext.Request.Form["SampleDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr);
            int id = obj.ExpenseID;
            var _objSample = _repositoryWrapper.Expense.GetExpenseById(id);
            _objSample.ExpenseID = obj.ExpenseID;
            _repositoryWrapper.Expense.Delete(_objSample);
            return objresponse = new { data = id+"Deleted" };
        }

        
        [HttpDelete("DeleteExpense1/{ExpenseID}", Name = "DeleteExpense1")]
        [Authorize]
        public dynamic DeleteExpense1(int ExpenseID)
        {
            bool retBool = false;

            //validateDelete
            var userresult = (_repositoryWrapper.EventLog.GetEventLogByUser(ExpenseID, "expense")).ToList();
            if (userresult.Count > 0)
            {
                retBool = false;
            }
            else
            {
                Expense objExpense =_repositoryWrapper.Expense.GetExpenseById(ExpenseID);
                if (objExpense != null)
                {
                    _repositoryWrapper.Expense.Delete(objExpense);
                    retBool = true;
                    _repositoryWrapper.EventLog.Delete("Delete Expense", objExpense);
                }
            }
            dynamic objresponse = new { data = retBool };
            return objresponse;
        }

        [HttpPost("GetExpp/{date}", Name="GetExpp")]
        [Authorize]
        public dynamic GetExpp(DateTime date) {
            dynamic objresponse = null;
            var _objSample = _repositoryWrapper.Expense.GetExpenseByDateRange(date);
            return objresponse = new { data = _objSample };
        } 

        [HttpGet("GetExpOpenings/{year}/{month}", Name="GetExpOpenings")]
        [Authorize]
        public dynamic GetExpOpenings(int year, int month) {
            dynamic objresponse = null;
            var _opening =  _repositoryWrapper.Expense.GetExpOpening(year,month);
            return objresponse = new {data = _opening};
        }

        [HttpGet("GetTotalExpWoDates", Name="GetTotalExpWoDates")]
        // [Authorize]
        public dynamic GetTotalExpWoDates() {
            dynamic objresponse = null;

            decimal openingforExp=0;

            var _expOpening =  _repositoryWrapper.Expense.FindAll();

            foreach(var exp1 in _expOpening){
                openingforExp+=exp1.Amount;
            }
            return objresponse = new {data = openingforExp};
        }

    }   
}