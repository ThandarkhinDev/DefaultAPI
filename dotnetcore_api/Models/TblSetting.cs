namespace MFI
{
     [System.ComponentModel.DataAnnotations.Schema.Table("tbl_setting")]
    public class Setting: BaseModel
    {
        public int SettingID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}