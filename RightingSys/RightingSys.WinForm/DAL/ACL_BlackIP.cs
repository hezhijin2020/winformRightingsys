using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.DAL
{
    public class ACL_BlackIP
    {
        public bool Add(Model.ACL_BlackIP model,List<string> userlist)
        {
            List<string> sqlList = new List<string>();
            string sqlText = string.Format(@"INSERT INTO ACL_BlackIP ([ID],[Name],[AuthorizeType],[IsEnabled],[IPStart],[IPEnd],[Note],[Creator],[Creator_ID],[CreateTime],[SysID])
            VALUES('{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
            model.ID,model.Name,model.AuthorizeType,model.IsEnabled,model.IPStart,model.IPEnd,model.Note,model.Creator,model.Creator_ID,model.CreateTime,model.SysID); 
            sqlList.Add(sqlText);
            if (userlist != null && userlist.Count > 0)
            {
                foreach (string s in userlist)
                {
                    string sql = string.Format("INSERT INTO ACL_BlackIP_User([BlackIP_ID],[User_ID]) VALUES('{0}','{1}')", model.ID,s);
                    sqlList.Add(sql);
                }
            }

            //AppPublic.appLogs.Add_OperationLog("新增黑白名单列表", DateTime.Now, "ACL_BlackIP,ACL_BlackIP_User", "新增", string.Join("|", sqlList));
            int  i= AppPublic.appSQL.ExecuteSqlTran(sqlList);
            if (i > 0)
                return true;
            else
                return false;

        }
        public bool Modify(Model.ACL_BlackIP model)
        {
            string sqlText = string.Format(@"Update ACL_BlackIP set [Name]='{0}',[AuthorizeType]={1},[IsEnabled]={2},[IPStart]='{3}',[IPEnd]='{4}',[Note]='{5}',[Creator]='{6}',[Creator_ID]='{7}',[CreateTime]='{8}',[SysID]='{9}'
            Where [ID]='{10}' ",
            model.Name, model.AuthorizeType, model.IsEnabled, model.IPStart, model.IPEnd, model.Note, model.Creator, model.Creator_ID, model.CreateTime, model.SysID,model.ID);
            
            //AppPublic.appLogs.Add_OperationLog("修改黑白名单列表", DateTime.Now, "ACL_BlackIP", "修改", sqlText);
            int i = AppPublic.appSQL.ExecuteSql(sqlText);
            if (i > 0)
                return true;
            else
                return false;

        }

        public bool Delete(Guid BlackIP_ID)
        {
            List<string> sqlList = new List<string>();
            string sqlTest = string.Format("Delete ACL_BlackIP Where [ID]='{0}'", BlackIP_ID);
            string sql = string.Format("Delete ACL_BlackIP_User where BlackIP_ID='{0}'", BlackIP_ID);
            sqlList.Add(sqlTest);
            sqlList.Add(sql);
            
           // AppPublic.appLogs.Add_OperationLog("删除黑白名单", DateTime.Now, "ACL_BlackIP,ACL_BlackIP_User", "删除", string.Join("|", sqlList));
            int i = AppPublic.appSQL.ExecuteSqlTran(sqlList);
            if (i > 0)
                return true;
            else
                return false;
        }
        public bool AddUserForBlackIP(Guid BlackIP_ID, List<string> userList)
        {
            List<string> sqlList = new List<string>();

            string sqlTest = string.Format("Delete ACL_BlackIP_User Where BlackIP_ID='{0}'",BlackIP_ID);

            sqlList.Add(sqlTest);
            foreach (string s in userList)
            {
                string sql = string.Format(@"Insert into ACL_BlackIP_User ([BlackIP_ID],[User_ID]) VALUES('{0}','{1}')", BlackIP_ID,s);
                sqlList.Add(sql);
            }
            //AppPublic.appLogs.Add_OperationLog("黑白名单添加用户信息", DateTime.Now, "ACL_BlackIP_User", "新增", string.Join("|", sqlList));
            int i = AppPublic.appSQL.ExecuteSqlTran(sqlList);
            if (i > 0)
                return true;
            else
                return false;
        }
        public bool RemoveUserForBlackIP(Guid BlackIP_ID, Guid User_ID)
        {
            string sqlTest = string.Format("Delete ACL_BlackIP_User Where [BlackIP_ID]='{0}' and [User_ID]='{1}'", BlackIP_ID,User_ID);
          
          //  AppPublic.appLogs.Add_OperationLog("黑白名单移除用户信息", DateTime.Now, "ACL_BlackIP_User", "删除", sqlTest);
            int i = AppPublic.appSQL.ExecuteSql(sqlTest);
            if (i > 0)
                return true;
            else
                return false;
        }
        public System.Data.DataTable Query(string where)
        {
            string sqlText = string.Format(@"select [ID],[Name],[AuthorizeType],[IsEnabled],[IPStart],[IPEnd],[Note],[Creator],[Creator_ID],[CreateTime],[SysID] from ACL_BlackIP");
            if (where != "")
                sqlText = sqlText + where;
            return AppPublic.appSQL.Query(sqlText).Tables[0];
        }
        public System.Data.DataTable getUserForBlackIP(Guid BlackIP_ID)
        {
            string sqlText = string.Format(@"select a.BlackIP_ID,a.[User_ID],B.LoginName + ' | ' + b.FullName + ' | ' + b.HandNo UserDesc
from ACL_BlackIP_User  as a left join ACL_User as b on a.[User_ID]=b.[UserID]
where a.BlackIP_ID='{0}'", BlackIP_ID);
           return AppPublic.appSQL.Query(sqlText).Tables[0];
        }

        #region 列表关联用户

        public System.Data.DataTable GetOUInfo()
        {
            string sqlText = " select * from ACL_OU  where [Enabled]=1 and Deleted=0 ";
            return AppPublic.appSQL.Query(sqlText).Tables[0];
        }

        public System.Data.DataTable GetRoleInfo()
        {
            string sqlText = " select* from vw_Role_Tree ";
            return AppPublic.appSQL.Query(sqlText).Tables[0];
        }

        public System.Data.DataTable FillTableUserForBlackIP(Guid BlackIP_ID)
        {
            string sqlText = string.Format(@"  SELECT  cast( ( CASE WHEN c.[User_ID] is null THEN 0  ELSE 1 END ) as bit) IsCheck, a.[UserID],a.[LoginName],a.[LoginPwd],a.[LastLoginTime],a.[LastLoginMac],a.[LastLoginIP],a.[RoleID],a.[IsOnline],a.[FullName],a.[HandNo],a.[OUID],a.[MobilePhone],a.[Email],a.[Address],a.[Gender],a.[Birthday],a.[CardNo],a.[JoinDay],a.[Job],a.[Creator],a.[CreateDate],a.[Enabled],a.[Deleted],b.[Name] OUName
            FROM[dbo].[ACL_User] as a left join ACL_OU as b on a.OUID=b.ID
			                          left join ( SELECT * FROM  ACL_BlackIP_User  where [BlackIP_ID]='{0}' )as c on a.UserID=c.[User_ID]  ", BlackIP_ID);
            return AppPublic.appSQL.Query(sqlText).Tables[0];
        }

        #endregion
    }
}
