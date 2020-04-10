using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.BLL
{
    public class ACL_BlackIP
    {
        DAL.ACL_BlackIP dal = new DAL.ACL_BlackIP();
        public bool Add(Model.ACL_BlackIP model, List<string> userlist)
        {
            return dal.Add(model, userlist);
        }
        public bool Modify(Model.ACL_BlackIP model)
        {
            return dal.Modify(model);
        }
        public bool Delete(Guid BlackIP_ID)
        {
           return dal.Delete(BlackIP_ID);
        }
        public bool AddUserForBlackIP(Guid BlackIP_ID, List<string> userList)
        {
            return dal.AddUserForBlackIP(BlackIP_ID, userList);
        }
        public bool RemoveUserForBlackIP(Guid BlackIP_ID, Guid User_ID)
        {
            return dal.RemoveUserForBlackIP(BlackIP_ID, User_ID);
        }
        public System.Data.DataTable Query()
        {
            return dal.Query("");
        }
        public System.Data.DataTable getUserForBlackIP(Guid BlackIP_ID)
        {
            return dal.getUserForBlackIP(BlackIP_ID);
        }

        public Model.ACL_BlackIP GetModel(Guid BlackIP_ID)
        {
            System.Data.DataTable table = dal.Query(string.Format(" Where [ID]='{0}'",BlackIP_ID));
            List<Model.ACL_BlackIP> list = AppPublic.appPublic.DataTableToList<Model.ACL_BlackIP>(table).ToList();
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
                return null;
               
        }

        public List<Model.ACL_BlackIP> GetModelList(string strWhere)
        {
            System.Data.DataTable table = dal.Query(strWhere);
            List<Model.ACL_BlackIP> list = AppPublic.appPublic.DataTableToList<Model.ACL_BlackIP>(table).ToList();
            return list;
        }


        #region 列表关联用户

        public System.Data.DataTable GetOUInfo()
        {
            return dal.GetOUInfo();
        }

        public System.Data.DataTable GetRoleInfo()
        {
            return dal.GetRoleInfo();
        }

        public System.Data.DataTable FillTableUserForBlackIP(Guid BlackIP_ID)
        {
            return dal.FillTableUserForBlackIP(BlackIP_ID);
        }

        #endregion
    }
}
