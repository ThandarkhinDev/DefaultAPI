using System;

namespace MFI
{
     [System.ComponentModel.DataAnnotations.Schema.Table("tbl_adminlevel")]
    public class Adminlevel: BaseModel
    {
        public int AdminLevelID { get; set; }
        public string AdminLevel { get; set; }
        public DateTime created_date { get; set; }
        public string Description { get; set; }
        public bool IsAdministrator { get; set; }
        public DateTime modified_date { get; set; }
        public string Remark { get; set; }
        public string restricted_iplist { get; set; }
    }
}
