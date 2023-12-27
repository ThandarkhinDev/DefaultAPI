using System;
using System.Collections.Generic;
using System.Linq;
using MFI;
using Repository;

namespace MFI.Repository
{
    public class ExpenseRepository: RepositoryBase<Expense>, IExpenseRepository
    {
        public ExpenseRepository(AppDb repositoryContext)
            :base(repositoryContext)
        {
        }

        public Expense GetExpenseById(int id) {
            return RepositoryContext.Expense.Find(id); //FindByCondition(x => x.ID.Equals(id)).FirstOrDefault();
        }

         // public IEnumerable<Expense> GetAllExpense()
        // {
        //     return FindAll();
        // }
    
        // public void AddExpense(Expense expense)
        // {
        //     Create(expense);
        //     Save();
        // }

        // public void UpdateExpense(Expense expense)
        // {
        //    Update(expense);
        //    Save();
        // }
        // public void DeleteExpense(Expense expense)
        // {
        //    Delete(expense);
        //    Save();
        // }
        
        public IEnumerable<Expense> GetExpenseByDateRange(DateTime date) {
            return FindByCondition(x => x.transaction_date<=date);
        }
         public IEnumerable<Expense> GetExpOpening(int year,int month) {
            return FindByCondition(x => x.transaction_date.Month==month && x.transaction_date.Year==year);
        }
         public int CheckDuplicateExpenseParticularName(int expenseID, string particularName) {
            return FindByCondition(x => x.ExpenseID != expenseID && x.ParticularName == particularName).Count();
        }
        public int CheckDuplicateExpenseAmount(int expenseID, decimal amount) {
            return FindByCondition(x => x.ExpenseID != expenseID && x.Amount == amount).Count();
        }
    }
}
