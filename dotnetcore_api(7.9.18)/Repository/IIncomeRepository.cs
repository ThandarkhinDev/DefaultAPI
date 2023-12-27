using System;
using System.Collections.Generic;
using MFI; 

namespace MFI.Repository
{
    public interface IIncomeRepository:IRepositoryBase<Income>
    {
        // IEnumerable<Income> GetAllIncome();
        // void AddIncome(Income income);
        // void UpdateIncome(Income income);
        // void DeleteIncome(Income income);

        IEnumerable<Income> GetIncomeByDateRange(DateTime date);
        IEnumerable<Income> GetIncOpening(int year,int month);
        Income GetIncomeById(int id);
        int CheckDuplicateIncomeDonarName(int incomeID, string donarName);
        int CheckDuplicateIncomeAmount(int incomeID, decimal amount);
        dynamic GetIncomes();

    }
}
