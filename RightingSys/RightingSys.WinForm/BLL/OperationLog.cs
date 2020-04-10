using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.BLL
{
    public class OperationLog
    {
        DAL.OperationLog dal = new DAL.OperationLog();
        public System.Data.DataTable Query()
        {
            return dal.Query("");
        }
    }
}
