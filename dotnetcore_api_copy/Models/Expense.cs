using System;

namespace MFI
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tbl_expense")]
    public class Expense: BaseModel
    {
        public int ExpenseID { get; set; }
        public string ParticularName { get; set; }
        public decimal Amount { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public DateTime transaction_date { get; set; }
     
    }
}