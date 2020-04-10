using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.DAL
{
    public class OperationLog
    {
        public System.Data.DataTable Query(string where)
        {
            string sql = "select * from ACL_OperationLog";
            if (where == "")
            {
                sql = sql + where;
            }
            //AppPublic.appLogs.Add_OperationLog("操作记录查询",DateTime.Now,"ACL_OperationLog","查询",sql);
            return AppPublic.appSQL.Query(sql).Tables[0];
        }

    }
}
