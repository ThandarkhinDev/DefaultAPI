namespace MFI
{
     [System.ComponentModel.DataAnnotations.Schema.Table("tbl_adminmenu")]
    public class Adminmenu: BaseModel
    {
        public int AdminMenuID { get; set; }
        public string AdminMenuName { get; set; }
        public string ControllerName { get; set; }
        public string Icon { get; set; }
        public int ParentID { get; set; }
        public int SrNo { get; set; }

    }
}
