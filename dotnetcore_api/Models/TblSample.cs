using System;

namespace MFI
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tbl_sample")]
    public class Sample: BaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
     
    }
}