using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.Model
{
    public class OperationLog
    {
        private int _ID;
        private string _LogDesc;
        private string _LoginName;
        private string _StaffName;
        private string _OuName;
        private string _TableName;
        private string _OperationType;
        private string _SQLText;
        private string _IPAddress;
        private string _MACAddress;

        public OperationLog( string LogDesc, string LoginName, string StaffName, string OuName, string TableName, string OperationType, string SQLText, string IPAddress, string MACAddress)
        {
            _LogDesc = LogDesc;
            _LoginName = LoginName;
            _StaffName = StaffName;
            _OuName = OuName;
            _TableName = TableName;
            _OperationType = OperationType;
            _SQLText = SQLText;
            _IPAddress = IPAddress;
            _MACAddress = MACAddress;
        }

        public int ID { get => _ID; set => _ID = value; }
        public string LogDesc { get => _LogDesc; set => _LogDesc = value; }
        public string LoginName { get => _LoginName; set => _LoginName = value; }
        public string StaffName { get => _StaffName; set => _StaffName = value; }
        public string OuName { get => _OuName; set => _OuName = value; }
        public string TableName { get => _TableName; set => _TableName = value; }
        public string OperationType { get => _OperationType; set => _OperationType = value; }
        public string SQLText { get => _SQLText; set => _SQLText = value; }
        public string IPAddress { get => _IPAddress; set => _IPAddress = value; }
        public string MACAddress { get => _MACAddress; set => _MACAddress = value; }
    }
}
