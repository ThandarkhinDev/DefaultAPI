
using MFI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("GetAllIncomes", Name="GetAllIncomes")]
        [Authorize]
        public dynamic GetAllIncomes() {
            dynamic objresponse = null;
            var _allIncomeByDateRange =   _repositoryWrapper.Income.GetAllIncome();
            return objresponse = new {data = _allIncomeByDateRange};
        }

        // [HttpGet("GetAllIncomesByRanges/{year}/{month}", Name="GetAllIncomesByRanges")]
        // [Authorize]
        // public dynamic GetAllIncomesByRanges(int year, int month) {
        //     dynamic objresponse = null;
        //     var _allIncome =   _repositoryWrapper.Income.GetAllIncomesByRange(year,month);
        //     return objresponse = new {data = _allIncome};
        // }
        [HttpPost("AddIncome", Name="AddIncome")]
        [Authorize]
        public dynamic AddIncome() {
            dynamic objresponse = null;
            var objstr = HttpContext.Request.Form["SampleDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr);
            int id = obj.IncomeID;
            var _objSample = _repositoryWrapper.Income.GetIncomeById(id);
            if(_objSample != null) {
                _objSample.DonarName = obj.DonarName;
                _objSample.Amount = obj.Amount;
                _repositoryWrapper.Income.UpdateIncome(_objSample);
            }else {
                var _newObj = new Income();
                _newObj.DonarName = obj.DonarName;
                _newObj.Amount = obj.Amount;
                _newObj.transaction_date = DateTime.Now;
                _newObj.created_date = DateTime.Now;
                _newObj.modified_date = DateTime.Now;
                _repositoryWrapper.Income.AddIncome(_newObj);
                id = _newObj.IncomeID;
            }
            return objresponse = new { data = id };
        }

        [HttpPost("DeleteIncome", Name="DeleteIncome")]
        [Authorize]
        public dynamic DeleteIncome() {
            dynamic objresponse = null;
            var objstr = HttpContext.Request.Form["SampleDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr);
            int id = obj.IncomeID;
            var _objSample = _repositoryWrapper.Income.GetIncomeById(id);
            _objSample.IncomeID = obj.IncomeID;
            _repositoryWrapper.Income.DeleteIncome(_objSample);
            return objresponse = new { data = id+"Deleted" };
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
    }
}