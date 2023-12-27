using System;
using System.Collections.Generic;
using MFI; 

namespace MFI.Repository
{
    public interface IIncomeRepository
    {
        IEnumerable<Income> GetAllIncome();

        IEnumerable<Income> GetIncomeByDateRange(DateTime date);
        
        IEnumerable<Income> GetIncOpening(int year,int month);
        Income GetIncomeById(int id);

        void AddIncome(Income income);

        void UpdateIncome(Income income);

        void DeleteIncome(Income income);
    }
}
