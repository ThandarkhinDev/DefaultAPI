namespace MFI
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tbl_adminmenuurl")]
    public class AdminMenuUrl: BaseModel
    {
        public int AdminMenuID { get; set; }
        public string ServiceUrl { get; set; }        
    }
}