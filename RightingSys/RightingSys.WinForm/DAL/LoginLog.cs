using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.DAL
{
    public class LoginLog
    {
        public System.Data.DataTable Query(string where)
        {
            string sqlText = " select *from ACL_LoginLog  ";
            if (where != "")
                sqlText = sqlText + where;
            //AppPublic.appLogs.Add_OperationLog("登录记录查询", DateTime.Now, "ACL_LoginLog", "查询", sqlText);
            return AppPublic.appSQL.Query(sqlText+" order by OpTime desc ").Tables[0];
        }

        public int Delete(string where)
        {
            string sqlText = "delete ACL_LoginLog ";
            if (where != "")
                sqlText = sqlText + where;
            AppPublic.appLogs.Add_OperationLog("登录记录删除", DateTime.Now, "ACL_LoginLog", "删除", sqlText);
            return  AppPublic.appSQL.ExecuteSql(sqlText);
        }
    }
}
