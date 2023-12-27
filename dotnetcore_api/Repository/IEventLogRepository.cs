//using Entities.ExtendedModels;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IEventLogRepository
    {
        IEnumerable<EventLog> GetEventLogByUser(int userID, string userType);
        void InsertEventLog(string Source, dynamic obj);
        void UpdateEventLog(string Source, dynamic obj);
        void DeleteEventLog(string Source, dynamic obj);
        void InfoEventLog(string Source, string LogMessage);
        void ErrorEventLog(string Source, string LogMessage);
        void WarningEventLog(string Source, string LogMessage);
    }
}
