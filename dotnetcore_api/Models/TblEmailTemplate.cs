using System;

namespace MFI
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tbl_email_template")]
    public class EmailTemplate: BaseModel
    {
        public int EmailTemplateID { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }
        public string subject { get; set; }
        public string from_email { get; set; }
        public string variable { get; set; }
        public DateTime modified_date { get; set; }
    }
}