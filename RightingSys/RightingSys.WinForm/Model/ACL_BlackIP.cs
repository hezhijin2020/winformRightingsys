using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.Model
{
    public class ACL_BlackIP
    {
        private Guid _ID = Guid.Empty;
        private string _Name = "";
        private int _AuthorizeType=0;
        private int _IsEnabled = 0;
        private string _IPStart = "";
        private string _IPEnd = "";
        private string _Note = "";
        private string _Creator = "";
        private Guid _Creator_ID=Guid.Empty;
        private DateTime _CreateTime=DateTime.Now;
        private Guid _SysID=Guid.Empty;

        public Guid ID { get => _ID; set => _ID = value; }
        public string Name { get => _Name; set => _Name = value; }
        public int AuthorizeType { get => _AuthorizeType; set => _AuthorizeType = value; }
        public int IsEnabled { get => _IsEnabled; set => _IsEnabled = value; }
        public string IPStart { get => _IPStart; set => _IPStart = value; }
        public string IPEnd { get => _IPEnd; set => _IPEnd = value; }
        public string Note { get => _Note; set => _Note = value; }
        public string Creator { get => _Creator; set => _Creator = value; }
        public Guid Creator_ID { get => _Creator_ID; set => _Creator_ID = value; }
        public DateTime CreateTime { get => _CreateTime; set => _CreateTime = value; }
        public Guid SysID { get => _SysID; set => _SysID = value; }
    }
}
