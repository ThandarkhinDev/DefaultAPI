namespace MFI
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tbl_state")]
    public class State: BaseModel
    {
        public long stateid { get; set; }
        public string statename { get; set; }
        public bool isactive { get; set; }
    }
}