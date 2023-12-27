using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFI
{
    public class BaseModel
    { 
        private string _OldDataString = "";
        private JObject _OldObj = null;

        public static string ObjectToString(dynamic OldObj)
        {
            string _OldObjString = "";
            try
            {
                if (OldObj == null) return "";
                JObject _duplicateObj = JObject.FromObject(OldObj);
                var _List = _duplicateObj.ToObject<Dictionary<string, object>>();
                foreach (var item in _List)
                {
                    var name = item.Key;
                    var val = item.Value;
                    string msg = name + " : " + val + "\r\n";
                    _OldObjString += msg;
                }
            }
            catch (Exception ex)
            {
                Globalfunction.WriteSystemLog("Exception :" + ex.Message);
            }
            return _OldObjString;
        }

        public void SetToString()
        {
            _OldDataString = ObjectToString(this);
        }

        public void CopyOldObj()
        {
            try
            {
                _OldDataString = ObjectToString(this);

                _OldObj = null;
                JObject _duplicateObj = JObject.FromObject(this);
                _OldObj = _duplicateObj;

            }
            catch (Exception ex)
            {
                Globalfunction.WriteSystemLog("Exception :" + ex.Message);
            }
        }

        public String GetUpdateString()
        {
            string _OldObjString = "";
            try
            {
                if (_OldObj == null) return "";
                JObject _newObj = JObject.FromObject(this);
                var _newList = _newObj.ToObject<Dictionary<string, object>>();

                JObject _duplicateObj = JObject.FromObject(_OldObj);
                var _List = _duplicateObj.ToObject<Dictionary<string, object>>();
                foreach (var item in _List)
                {
                    var name = item.Key;
                    var val = item.Value != null ? item.Value.ToString().Trim() : "";
                    var newval = _newList.GetValueOrDefault(item.Key) != null ? _newList.GetValueOrDefault(item.Key).ToString().Trim() : "";
                    string msg = "";
                    if(val != newval) msg = name + " : " + val + " >>> " + newval + "\r\n";
                    _OldObjString += msg;
                }
            }
            catch (Exception ex)
            {
                Globalfunction.WriteSystemLog("Exception :" + ex.Message);
            }
            return _OldObjString;
        }

        public override String ToString()
        {
            return _OldDataString;
        }
    }
}
