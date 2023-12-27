using System;

namespace MFI
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tbl_eventlog")]
    public class EventLog: BaseModel
    {
        public int ID { get; set; }
        public string LogType { get; set; }
        public DateTime LogDateTime { get; set; }
        public string Source { get; set; }
        public string LogMessage { get; set; }
        public int UserID { get; set; }
        public string UserType { get; set; }
        public string ipAddress { get; set; }
    }
}