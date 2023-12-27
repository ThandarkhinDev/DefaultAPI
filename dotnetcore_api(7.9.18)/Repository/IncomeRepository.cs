using System;
using System.Collections.Generic;
using System.Linq;
using MFI;
using Repository;

namespace MFI.Repository
{
    public class IncomeRepository: RepositoryBase<Income>, IIncomeRepository
    {
        public IncomeRepository(AppDb repositoryContext)
            :base(repositoryContext)
        {
        }

      
        public Income GetIncomeById(int id) {
            return RepositoryContext.Income.Find(id); //FindByCondition(x => x.ID.Equals(id)).FirstOrDefault();
        }
    
        // public IEnumerable<Income> GetAllIncome()
        // {
        //     return FindAll();
        // }
        // public void AddIncome(Income income)
        // {
        //     Create(income);
        //     Save();
        // }

        // public void UpdateIncome(Income income)
        // {
        //    Update(income);
        //    Save();
        // }
        // public void DeleteIncome(Income income)
        // {
        //    Delete(income);
        //    Save();
        // }
        public IEnumerable<Income> GetIncomeByDateRange(DateTime date) {
            return FindByCondition(x => x.transaction_date<=date);
        }
        public IEnumerable<Income> GetIncOpening(int year,int month) {
            
            return FindByCondition(x => x.transaction_date.Month==month && x.transaction_date.Year==year);
        }
      

        public int CheckDuplicateIncomeDonarName(int incomeID, string donarName) {
            return FindByCondition(x => x.IncomeID != incomeID && x.DonarName == donarName).Count();
        }
        public int CheckDuplicateIncomeAmount(int incomeID, decimal amount) {
            return FindByCondition(x => x.IncomeID != incomeID && x.Amount == amount).Count();
        }
        public dynamic GetIncomes() {
        var mainQuery = (from main in RepositoryContext.Income
                            // join s in RepositoryContext.Income on main.IncomeID equals s.IncomeID into tmp
                            // from tmps in tmp.DefaultIfEmpty()
                        select new {
                            main.IncomeID,
                            main.DonarName,
                            main.Amount,
                            main.transaction_date
                           
                            // state = tmps != null ? tmps.statename : ""
                        });
        return mainQuery;
    }
        
    }
}
