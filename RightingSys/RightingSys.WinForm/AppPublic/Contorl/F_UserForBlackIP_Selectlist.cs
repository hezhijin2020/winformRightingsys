﻿using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RightingSys.WinForm.AppPublic.Contorl
{
    public partial class F_UserForBlackIP_Selectlist : Form
    {
        BLL.ACL_BlackIP bll = new BLL.ACL_BlackIP();
        DataTable dtAll = null;
        private Guid _BlackIP_ID= Guid.Empty;
        public F_UserForBlackIP_Selectlist(Guid BlackIP_ID)
        {
            InitializeComponent();
            _BlackIP_ID = BlackIP_ID;
            Initial();
        }

        private void Initial()
        {
            tl_OU.DataSource = bll.GetOUInfo();
            tl_Role.DataSource = bll.GetRoleInfo();
            dtAll = bll.FillTableUserForBlackIP(_BlackIP_ID);
            gcUser.DataSource = dtAll;
        }
        
        #region 组织机构和角色树焦点改变事件,筛选用户信息
        private void tl_OU_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null && dtAll != null)
            {
                dtAll.DefaultView.RowFilter = string.Format("OUID='{0}' or IsCheck=True ", e.Node.GetValue("ID"));
                gcUser.DataSource = dtAll.DefaultView;
            }
        }

        private void tl_Role_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null && dtAll != null)
            {
                dtAll.DefaultView.RowFilter = string.Format("RoleID='{0}' or IsCheck=True", e.Node.GetValue("ID"));
                gcUser.DataSource = dtAll.DefaultView;
            }
        }

        private void GetNodChildrenID(TreeListNode node, ref string strListID)
        {
            strListID = strListID + ",'" + node.GetValue("ID") + "'";

            foreach (TreeListNode n in node.Nodes)
            {
                GetNodChildrenID(n, ref strListID);
            }
        }
        #endregion

        private void tabPaneView_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (e.Page.PageText == "tabpage_Dept")
            {
                tl_OU.FocusedNode = null;
            }
            else
            {
                tl_Role.FocusedNode = null;
            }
        }

        private void gvUser_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null) return;

            if (e.Column.FieldName == "Deleted")
            {
                if (e.Value.ToString() == "0")
                { e.DisplayText = ""; }
                else
                { e.DisplayText = "已删除"; }
            }
            if (e.Column.FieldName == "Enabled")
            {
                if (e.Value.ToString() == "0")
                { e.DisplayText = "未启用"; }
                else
                { e.DisplayText = "已启用"; }
            }
        }
        
        private void sbtnClose_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void sbtnFinish_Click(object sender, EventArgs e)
        {
            DataRow[] rows=  dtAll.Select("IsCheck=True");
            if (rows.Count() > 0)
            {
                List<string> UserList = new List<string>();
                foreach (DataRow r in rows)
                {
                    UserList.Add(r["UserID"].ToString());
                }

              if( bll.AddUserForBlackIP(_BlackIP_ID, UserList))
                {
                    base.DialogResult = DialogResult.OK;
                    appPublic.ShowMessage("成功", Text);
                    return;
                }
            }
            base.DialogResult = DialogResult.Cancel;
        }
    }
}
