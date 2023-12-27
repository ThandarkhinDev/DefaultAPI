using System;

namespace MFI
{
     [System.ComponentModel.DataAnnotations.Schema.Table("tbl_admin")]
    public class Admin: BaseModel
    {
        public int AdminID { get; set; }
        public sbyte access_status { get; set; }
        public int AdminLevelID { get; set; }
        public string AdminName { get; set; }
        public DateTime created_date { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public int login_fail_count { get; set; }
        public string LoginName { get; set; }
        public DateTime modified_date { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string nrc { get; set; }
        public long state { get; set; }
    }
}