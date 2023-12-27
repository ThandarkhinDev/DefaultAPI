namespace MFI
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tbl_adminlevelmenu")]
    public class Adminlevelmenu: BaseModel
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Adminlevel")]
        public int AdminLevelID { get; set; }
        public virtual Adminlevel Adminlevel { get; set; }
        public int AdminMenuID { get; set; }
    }
}