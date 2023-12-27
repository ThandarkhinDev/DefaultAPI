using System;

namespace dotnet2_1WebAPI
{
     [System.ComponentModel.DataAnnotations.Schema.Table("tbl_income")]
    public class Income
    {
        public int IncomeID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string DonarName { get; set; }        
        public decimal Amount { get; set; }
        public DateTime modified_date { get; set; }
        public DateTime created_date { get; set; }
    }
}