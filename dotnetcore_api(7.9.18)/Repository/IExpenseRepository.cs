using System;
using System.Collections.Generic;
using MFI; 

namespace MFI.Repository
{
    public interface IExpenseRepository:IRepositoryBase<Expense>
    {
        // IEnumerable<Expense> GetAllExpense();
        // void AddExpense(Expense expense);
        // void UpdateExpense(Expense expense);
        // void DeleteExpense(Expense expense);

        IEnumerable<Expense> GetExpenseByDateRange(DateTime date);

        IEnumerable<Expense> GetExpOpening(int year,int month);
        
        Expense GetExpenseById(int id);
        int CheckDuplicateExpenseParticularName(int expenseID, string particularName);
        int CheckDuplicateExpenseAmount(int expenseID, decimal amount);

    }
}
