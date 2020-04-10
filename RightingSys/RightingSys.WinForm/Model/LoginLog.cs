using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.Model
{
    public class LoginLog
    {
        private int _ID;
        private string _UserID;
        private string _LoginName;
        private string _StaffName;
        private string _OUID;
        private string _OuName;
        private string _IPAddress;
        private string _MacAddress;
        private string _SystemCategoryID;
        private string _SysName;
        private string _LogDesc;
        private DateTime _LoginTime;

        public int ID { get => _ID; set => _ID = value; }
        public string UserID { get => _UserID; set => _UserID = value; }
        public string LoginName { get => _LoginName; set => _LoginName = value; }
        public string StaffName { get => _StaffName; set => _StaffName = value; }
        public string OUID { get => _OUID; set => _OUID = value; }
        public string OuName { get => _OuName; set => _OuName = value; }
        public string IPAddress { get => _IPAddress; set => _IPAddress = value; }
        public string MacAddress { get => _MacAddress; set => _MacAddress = value; }
        public string SystemCategoryID { get => _SystemCategoryID; set => _SystemCategoryID = value; }
        public string SysName { get => _SysName; set => _SysName = value; }
        public string LogDesc { get => _LogDesc; set => _LogDesc = value; }
        public DateTime LoginTime { get => _LoginTime; set => _LoginTime = value; }
    }
}
