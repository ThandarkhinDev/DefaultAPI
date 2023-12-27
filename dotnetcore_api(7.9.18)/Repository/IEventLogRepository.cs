//using Entities.ExtendedModels;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IEventLogRepository
    {
        IEnumerable<EventLog> GetEventLogByUser(int userID, string userType);
        void Insert(string Source, dynamic obj);
        void Update(string Source, dynamic obj);
        void Delete(string Source, dynamic obj);
        void Info(string Source, string LogMessage);
        void Error(string Source, string LogMessage);
        void Warning(string Source, string LogMessage);
    }
}
