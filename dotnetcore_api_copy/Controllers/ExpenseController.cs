
using MFI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MFI
{
    [Route("api/[controller]")]
    public class ExpenseController: BaseController
    {
        private IRepositoryWrapper _repositoryWrapper;
        public ExpenseController(IRepositoryWrapper RW) {
            _repositoryWrapper = RW;
        }

        [HttpGet("GetAllExpenses", Name="GetAllExpenses")]
        [Authorize]
        public dynamic GetAllExpenses() {
            dynamic objresponse = null;
            var _allExpense =   _repositoryWrapper.Expense.GetAllExpense();
            return objresponse = new {data = _allExpense};
        }

        [HttpPost("AddExpense", Name="AddExpense")]
        [Authorize]
        public dynamic AddExpense() {
            dynamic objresponse = null;
            var objstr = HttpContext.Request.Form["SampleDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr);
            int id = obj.ExpenseID;
            var _objSample = _repositoryWrapper.Expense.GetExpenseById(id);
            if(_objSample != null) {
                _objSample.ParticularName = obj.ParticularName;
                _objSample.Amount = obj.Amount;
                _repositoryWrapper.Expense.UpdateExpense(_objSample);
            }else {
                var _newObj = new Expense();
                _newObj.ParticularName = obj.ParticularName;
                _newObj.Amount = obj.Amount;
                _newObj.transaction_date = DateTime.Now;
                _newObj.created_date = DateTime.Now;
                _newObj.modified_date = DateTime.Now;
                _repositoryWrapper.Expense.AddExpense(_newObj);
                id = _newObj.ExpenseID;
            }
            return objresponse = new { data = id };
        }

        [HttpPost("DeleteExpense", Name="DeleteExpense")]
        [Authorize]
        public dynamic DeleteExpense() {
            dynamic objresponse = null;
            var objstr = HttpContext.Request.Form["SampleDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr);
            int id = obj.ExpenseID;
            var _objSample = _repositoryWrapper.Expense.GetExpenseById(id);
            _objSample.ExpenseID = obj.ExpenseID;
            _repositoryWrapper.Expense.DeleteExpense(_objSample);
            return objresponse = new { data = id+"Deleted" };
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
    }
}
