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

        public IEnumerable<Income> GetAllIncome()
        {
            return FindAll();
        }
        public Income GetIncomeById(int id) {
            return RepositoryContext.Income.Find(id); //FindByCondition(x => x.ID.Equals(id)).FirstOrDefault();
        }
    
        public void AddIncome(Income income)
        {
            Create(income);
            Save();
        }

        public void UpdateIncome(Income income)
        {
           Update(income);
           Save();
        }
        public void DeleteIncome(Income income)
        {
           Delete(income);
           Save();
        }
        public IEnumerable<Income> GetIncomeByDateRange(DateTime date) {
            return FindByCondition(x => x.transaction_date<=date);
        }
        public IEnumerable<Income> GetIncOpening(int year,int month) {
            return FindByCondition(x => x.transaction_date.Month==month && x.transaction_date.Year==year);
        }

    }
}
