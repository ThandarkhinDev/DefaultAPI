using System.Collections.Generic;
using System.Linq;
using System;
using Repository;
using Microsoft.AspNetCore.Http;

namespace MFI.Repository
{
    public enum EventLogType
    {
        Info = 1,
        Error = 2,
        Warning = 3,
        Insert = 4,
        Update = 5,
        Delete = 6
    }
    public class EventLogRepository : RepositoryBase<EventLog>, IEventLogRepository
    {
        public static IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public EventLogRepository(AppDb repositoryContext)
            : base(repositoryContext)
        {
            _httpContextAccessor = CustomTokenAuthProvider.TokenProviderMiddleware._httpContextAccessor;
        }
        public IEnumerable<EventLog> GetEventLogByUser(int userID, string userType) {
            return FindByCondition(x => x.UserID == userID && x.UserType == userType);
        }

        public string getLogType(EventLogType LogTypeEnum)
        {
            string logtype = "";
            switch (LogTypeEnum)
            {
                case EventLogType.Info:
                    logtype = "Info";
                    break;
                case EventLogType.Error:
                    logtype = "Error";
                    break;
                case EventLogType.Warning:
                    logtype = "Warning";
                    break;
                case EventLogType.Insert:
                    logtype = "Insert";
                    break;
                case EventLogType.Update:
                    logtype = "Update";
                    break;
                case EventLogType.Delete:
                    logtype = "Delete";
                    break;
                default:
                    logtype = "Info";
                    break;
            }
            return logtype;
        }

        public void AddEventLog(EventLogType LogTypeEnum, string Source, string LogMessage)
        {
            string LogType = getLogType(LogTypeEnum);
            string LoginUserID = _session.GetString("LoginUserID");
            string LoginRemoteIpAddress = _session.GetString("LoginRemoteIpAddress");
            string LoginTypeParam = _session.GetString("LoginTypeParam");
            if (LogMessage != "")
            {
                if (LoginTypeParam == "") LoginTypeParam = "0";
                string LoginTypestr = "public";
                int LoginType = int.Parse(LoginTypeParam);
                if (LoginType == 1)
                    LoginTypestr = "admin";

                try
                {
                    var newobj = new EventLog();
                    newobj.LogType = LogType;
                    newobj.LogDateTime = DateTime.Now;
                    newobj.Source = Source;
                    newobj.LogMessage = LogMessage;
                    newobj.UserID = int.Parse(LoginUserID);
                    newobj.UserType = LoginTypestr;
                    newobj.ipAddress = LoginRemoteIpAddress;

                    Create(newobj);
                    Save();

                }
                catch (Exception ex)
                {
                    Globalfunction.WriteSystemLog("SQL Exception :" + ex.Message);
                }
            }
        }

        public void InsertEventLog(string Source, dynamic obj)
        {
            obj.SetToString();
            string LogMessage = "";
            LogMessage += "Created new data are as follow:\r\n";
            LogMessage += obj.ToString();

            AddEventLog(EventLogType.Insert, Source, LogMessage);
        }

        public void UpdateEventLog(string Source, dynamic obj)
        {
            obj.SetToString();
            string LogMessage = "";
            LogMessage += "Updated data are as follow:\r\n";
            LogMessage += obj.GetUpdateString();
            AddEventLog(EventLogType.Update, Source, LogMessage);
        }

        public void DeleteEventLog(string Source, dynamic obj)
        {
            obj.SetToString();
            string LogMessage = "";
            LogMessage += "Deleted old data are as follow:\r\n";
            LogMessage += obj.ToString();
            AddEventLog(EventLogType.Delete, Source, LogMessage);
        }

        public void ErrorEventLog(string Source, string LogMessage)
        {
            AddEventLog(EventLogType.Error, Source, LogMessage);
        }

        public void InfoEventLog(string Source, string LogMessage)
        {
            AddEventLog(EventLogType.Info, Source, LogMessage);
        }
   
        public void WarningEventLog(string Source, string LogMessage)
        {
            AddEventLog(EventLogType.Warning, Source, LogMessage);
        } 
    }
}
