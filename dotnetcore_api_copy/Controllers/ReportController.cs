
using MFI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MFI
{
    [Route("api/[controller]")]
    public class ReportController: BaseController
    {
      
        private IRepositoryWrapper _repositoryWrapper;
        public ReportController(IRepositoryWrapper RW) {
            _repositoryWrapper = RW;
        }

        [HttpGet("GetOpenings/{year}/{month}", Name="GetOpenings")]
        // [Authorize]
        public dynamic GetOpenings(int year, int month) {
            dynamic objresponse = null;

            decimal openingforInc=0;
            decimal openingforExp=0;
            decimal opening=0;

            var _incOpening =  _repositoryWrapper.Income.GetIncOpening(year, month);

            foreach(var inc1 in _incOpening){
                openingforInc+=inc1.Amount;
            }

           var _expOpening =   _repositoryWrapper.Expense.GetExpOpening(year, month);
           
            foreach(var inc2 in _expOpening){
                openingforExp+=inc2.Amount;
            }
            opening=openingforInc-openingforExp;

            return objresponse = new {Total = opening, Income=openingforInc, Expnese=openingforExp};

        }
    }
}