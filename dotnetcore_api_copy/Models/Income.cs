using System;

namespace MFI
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tbl_income")]
    public class Income: BaseModel
    {
        public int IncomeID { get; set; }
        public string DonarName { get; set; }
        public decimal Amount { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public DateTime transaction_date { get; set; }
     
    }
}