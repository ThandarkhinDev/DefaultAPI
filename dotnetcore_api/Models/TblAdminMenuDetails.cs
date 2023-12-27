namespace MFI
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tbl_adminmenudetails")]
    public class AdminMenuDetails: BaseModel   
    {
        public int MenuID { get; set; }
        public string ControllerName { get; set; }        
    }
}