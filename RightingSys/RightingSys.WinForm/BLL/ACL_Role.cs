using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.BLL
{
    public class ACL_Role
    {
        #region 基本的方法
        DAL.ACL_Role dal = new DAL.ACL_Role();
        public bool AddNew(Model.ACL_Role model)
        {
            return dal.AddNew(model);
        }
        public bool Modify(Model.ACL_Role model)
        {

            return dal.Modify(model);
        }
        public bool IsDelete(Guid guidID)
        {
          return  dal.IsDelete(guidID);
        }
        public bool Delete(Guid guidID)
        {
            return dal.Delete(guidID);
        }

        public System.Data.DataTable Query(string where)
        {
            return dal.Query(where);
        }

        public System.Data.DataTable Query()
        {
            return dal.Query("");
        }
        
        public Model.ACL_Role GetModel(string ID)
        {
            Model.ACL_Role model = new Model.ACL_Role();
            System.Data.DataTable dt = dal.Query(" where ID='" + ID + "'");
            List<Model.ACL_Role> list = AppPublic.appPublic.DataTableToList<Model.ACL_Role>(dt).ToList();
            if (list.Count > 0)
            {
                model = list[0];
                return model;
            }
            else
                return null;
        }

        public List<Model.ACL_Role> GetModelList()
        {
            System.Data.DataTable table = Query("");
            List<Model.ACL_Role> list = AppPublic.appPublic.DataTableToList<Model.ACL_Role>(table).ToList();
            return list;
        }

        public List<Model.ACL_Role> GetModelList(string strWhere)
        {
            System.Data.DataTable table = Query(strWhere);
            List<Model.ACL_Role> list = AppPublic.appPublic.DataTableToList<Model.ACL_Role>(table).ToList();
            return list;
        }
        #endregion

        #region 权限方法
        public string GetFuncHandleText(int pEnum)
        {
            string FuncHandleText = "";
            if ((pEnum & 1) == 1)
            {
                FuncHandleText = "新增";
            }
            if ((pEnum & 2) == 2)
            {
                FuncHandleText = FuncHandleText+","+"查询";
            }
            if ((pEnum & 4) == 4)
            {
                FuncHandleText = FuncHandleText + "," + "修改";
            }
            if ((pEnum & 8) == 8)
            {
                FuncHandleText = FuncHandleText + "," + "删除";
            }
            if ((pEnum & 16) == 16)
            {
                FuncHandleText = FuncHandleText + "," + "保存";
            }
            if ((pEnum & 32) == 32)
            {
                FuncHandleText = FuncHandleText + "," + "取消";
            }
            if ((pEnum & 64) == 64)
            {
                FuncHandleText = FuncHandleText + "," + "审核";
            }
            if ((pEnum & 128) == 128)
            {
                FuncHandleText = FuncHandleText + "," + "反审";
            }
            if ((pEnum & 256) == 256)
            {
                FuncHandleText = FuncHandleText + "," + "导入";
            }
            if ((pEnum & 512) == 512)
            {
                FuncHandleText = FuncHandleText + "," + "导出";
            }
            if ((pEnum & 1024) == 1024)
            {
                FuncHandleText = FuncHandleText + "," + "预览";
            }
            if ((pEnum & 2048) == 2048)
            {
                FuncHandleText = FuncHandleText + "," + "打印";
            }
            if ((pEnum & 4096) == 4096)
            {
                FuncHandleText = FuncHandleText + "," + "首笔";
            }
            if ((pEnum & 8192) == 8192)
            {
                FuncHandleText = FuncHandleText + "," + "上笔";
            }
            if ((pEnum & 16384) == 16384)
            {
                FuncHandleText = FuncHandleText + "," + "下笔";
            }
            if ((pEnum & 32768) == 32768)
            {
                FuncHandleText = FuncHandleText + "," + "末笔";
            }
            return FuncHandleText;
        }

        public string GetFuncHandle(int pEnum)
        {
            string FuncHandle = "0";
            if ((pEnum & 1) == 1)
            {
                FuncHandle = "1";
            }
            if ((pEnum & 2) == 2)
            {
                FuncHandle = FuncHandle + "," + "2";
            }
            if ((pEnum & 4) == 4)
            {
                FuncHandle = FuncHandle + "," + "4";
            }
            if ((pEnum & 8) == 8)
            {
                FuncHandle = FuncHandle + "," + "8";
            }
            if ((pEnum & 16) == 16)
            {
                FuncHandle = FuncHandle + "," + "16";
            }
            if ((pEnum & 32) == 32)
            {
                FuncHandle = FuncHandle + "," + "32";
            }
            if ((pEnum & 64) == 64)
            {
                FuncHandle = FuncHandle + "," + "64";
            }
            if ((pEnum & 128) == 128)
            {
                FuncHandle = FuncHandle + "," + "128";
            }
            if ((pEnum & 256) == 256)
            {
                FuncHandle = FuncHandle + "," + "256";
            }
            if ((pEnum & 512) == 512)
            {
                FuncHandle = FuncHandle + "," + "512";
            }
            if ((pEnum & 1024) == 1024)
            {
                FuncHandle = FuncHandle + "," + "1024";
            }
            if ((pEnum & 2048) == 2048)
            {
                FuncHandle = FuncHandle + "," + "2048";
            }
            if ((pEnum & 4096) == 4096)
            {
                FuncHandle = FuncHandle + "," + "4096";
            }
            if ((pEnum & 8192) == 8192)
            {
                FuncHandle = FuncHandle + "," + "8192";
            }
            if ((pEnum & 16384) == 16384)
            {
                FuncHandle = FuncHandle + "," + "16384";
            }
            if ((pEnum & 32768) == 32768)
            {
                FuncHandle = FuncHandle + "," + "32768";
            }
            return FuncHandle;
        }


        public System.Data.DataTable FilldtRoleTree()
        {
            return dal.FilldtRoleTree();
        }

        public System.Data.DataTable FilldtFuncTree(string guidRoleID)
        {
            System.Data.DataTable dtFunc=dal.FilldtFuncTree(guidRoleID);
            return FillFuncHandle(dtFunc);
        }


        public System.Data.DataTable FillFuncHandle(System.Data.DataTable dtFunc)
        {
            System.Data.DataTable dt = dtFunc.Copy();
            foreach (System.Data.DataRow s in dtFunc.Rows)
            {
                int pEnum = int.Parse(s["FuncHandle"].ToString());
                int Opcode = 0;
                int spEnum= int.TryParse(s["OpCode"].ToString() ,out Opcode) ==true?Opcode:0;
                string handleNos = GetFuncHandle(pEnum);
                string shandleNos = GetFuncHandle(spEnum);
                System.Data.DataTable dtHandle = dal.FilldtFuncHandle(handleNos);
                System.Data.DataTable sdtHandle = dal.FilldtFuncHandle(shandleNos);
                foreach (System.Data.DataRow r in dtHandle.Rows)
                {
                    System.Data.DataRow Row= dt.NewRow();
                    Row["ID"] = Guid.NewGuid();
                    Row["PID"] = s["ID"];
                    Row["Name"] = s["Name"] + "-" + r["Name"];
                    Row["FuncHandle"] = r["HandleNo"];
                    Row["SortCode"] = r["HandleNo"];
                    Row["TypeID"] = 2;
                    if (sdtHandle.Select("HandleNo=" + r["HandleNo"]).ToList().Count > 0)
                    {
                        Row["Enabled"] = 1;
                    }
                    else {
                        Row["Enabled"] = 0;
                    }
                    dt.Rows.Add(Row);
                }
            }
            return dt;
        }

        #endregion



        ACL_OU bll = new ACL_OU();
        ACL_Function bllFunc = new ACL_Function();
        public List<Model.ACL_OU> GetACL_OU_Company()
        {
           return bll.GetModelList(" where (Category ='公司' or  Category='集团' ) and Deleted=0 and [Enabled]=1 ");
        }
        public List<Model.ACL_Function> GetACL_Functions()
        {
            return bllFunc.GetModelList(" where Deleted=0 and [Enabled]=1 ");
        }

        public bool SaveRighting(System.Data.DataTable dt,string RoleID)
        {
            System.Data.DataRow[] array = dt.Select("TypeID=1");
            System.Data.DataTable dts = new System.Data.DataTable();
            dts.Columns.AddRange(
                new System.Data.DataColumn[] {
                    new System.Data.DataColumn("RoleID"),
                    new System.Data.DataColumn("FuncID"),
                    new System.Data.DataColumn("OpCode")
                });
            foreach (System.Data.DataRow r in array)
            {
                object OpCode=0;
                if (AppPublic.appPublic.GetIntValue( r["FuncHandle"])>0)
                {
                    OpCode = dt.Compute("sum(FuncHandle)", "PID='" + r["ID"] + "'");
                    OpCode = OpCode.ToString() == "" ? 0 : OpCode;
                }
                dts.Rows.Add(RoleID, r["ID"], OpCode);
                
            }

          return  dal.SaveRighting(dts, RoleID);
        }


        #region 用户关联角色       
        public bool AddUserForRole(Guid RoleId, List<string> UserList)
        {
           return dal.AddUserForRole(RoleId, UserList);
        }

        public bool RemoveUserForRole(Guid User_ID, Guid Role_ID)
        {
            return dal.RemoveUserForRole(User_ID, Role_ID);
        }

        public System.Data.DataTable GetUserForRole(Guid RoleID)
        {
            return dal.GetUserForRole(RoleID);
        }

        public System.Data.DataTable FillTableUserForRole(Guid RoleID)
        {
            return dal.FillTableUserForRole(RoleID);
        }

        #endregion

        #region 机构关联角色
        public bool AddOUForRole(Guid RoleId, List<string> OUList)
        {
            return dal.AddOUForRole(RoleId, OUList);
        }
        public bool RemoveOUForRole(Guid Role_ID, Guid OU_ID)
        {
           return  dal.RemoveOUForRole(Role_ID, OU_ID);
        }
        public System.Data.DataTable FillTableOUForRole(Guid RoleID)
        {
            return dal.FillTableOUForRole(RoleID);
        }
        public System.Data.DataTable GetOUForRole(Guid RoleID)
        {
           return  dal.GetOUForRole(RoleID);
        }

        #endregion


        public System.Data.DataTable GetOUInfo()
        {
            return dal.GetOUInfo();
        }

        public System.Data.DataTable GetRoleInfo()
        {
            return dal.GetRoleInfo();
        }



    }
}
