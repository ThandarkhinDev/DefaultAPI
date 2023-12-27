using System;
using System.Collections.Generic;
using MFI; 

namespace MFI.Repository
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> GetAllExpense();

        IEnumerable<Expense> GetExpenseByDateRange(DateTime date);

        IEnumerable<Expense> GetExpOpening(int year,int month);
        
        Expense GetExpenseById(int id);

        void AddExpense(Expense expense);

        void UpdateExpense(Expense expense);

        void DeleteExpense(Expense expense);
    }
}
