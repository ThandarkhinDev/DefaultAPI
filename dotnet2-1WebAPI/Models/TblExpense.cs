using System;

namespace dotnet2_1WebAPI
{
     [System.ComponentModel.DataAnnotations.Schema.Table("tbl_expense")]
    public class Expense
    {
        public int ID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Particular { get; set; }        
        public decimal Amount { get; set; }
        public DateTime modified_date { get; set; }
        public DateTime created_date { get; set; }
    }
}