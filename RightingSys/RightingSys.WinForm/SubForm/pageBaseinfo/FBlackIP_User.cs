using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace RightingSys.WinForm.SubForm.pageBaseinfo
{
    public partial class FBlackIP_User : BaseForm
    {
        Model.ACL_BlackIP model = new Model.ACL_BlackIP();
        BLL.ACL_BlackIP bll = new BLL.ACL_BlackIP();
        public FBlackIP_User()
        {
            InitializeComponent();
            this.Text = "新增--" + this.Text;
            this.panelControl2.Enabled = false;
        }

        public FBlackIP_User(Guid BlackIP_ID)
        {
            InitializeComponent();
            this.Text = "编辑--" + this.Text;
            model = bll.GetModel(BlackIP_ID);
            if (model != null)
            {
                txtName.Text = model.Name;
                cboxType.SelectedItem = model.AuthorizeType == 0 ? "黑名单" : "白名单";
                txtStartIP.Text = model.IPStart;
                txtEndIP.Text = model.IPEnd;
                txtRemark.Text = model.Note;
                IsEnabled.Checked = model.IsEnabled == 0 ? false : true;
                gcUser.DataSource = bll.getUserForBlackIP(BlackIP_ID);
            }
            else {
                AppPublic.appPublic.ShowMessage("[ID]="+BlackIP_ID+"的记录不存在",Text);
                base.DialogResult = DialogResult.Cancel;
            }

        }

        public override void Query()
        {
            gcUser.DataSource = bll.getUserForBlackIP(model.ID);
        }
        private void sbtnClose_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void sbtnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "" || txtStartIP.Text.Trim() == "" || txtEndIP.Text.Trim() == "")
            {
                AppPublic.appPublic.ShowMessage("信息输入不完整", Text );
                return;
            }
            if (!AppPublic.appPublic.IsIP(txtStartIP.Text) || !AppPublic.appPublic.IsIP(txtEndIP.Text))
            {
                AppPublic.appPublic.ShowMessage("输入的IP地址无效", Text);
                return;
            }
            if (!AppPublic.appPublic.validateIP(txtStartIP.Text, txtEndIP.Text))
            {
                AppPublic.appPublic.ShowMessage("开始IP大于结束IP", Text);
                return;
            }
            
            
            model.Name = txtName.Text.Trim();
            model.AuthorizeType = cboxType.Text == "黑名单" ? 0 : 1;
            model.IsEnabled = IsEnabled.Checked == true ? 1 : 0;
            model.IPStart = txtStartIP.Text;
            model.IPEnd = txtEndIP.Text;
            model.Note = txtRemark.Text;
            model.SysID = AppPublic.appSession._SystemId;
            model.CreateTime = DateTime.Now;
            model.Creator = AppPublic.appSession._LoginName;
            model.Creator_ID = AppPublic.appSession._UserId;
            if (model.ID == Guid.Empty)
            {
                model.ID = Guid.NewGuid();
                if (bll.Add(model, null))
                {
                    AppPublic.appPublic.ShowMessage("成功",Text);
                    base.DialogResult = DialogResult.OK;
                }
                else
                {
                    base.DialogResult = DialogResult.Cancel;
                }

            }
            else
            {

                if (bll.Modify(model))
                {
                    AppPublic.appPublic.ShowMessage("成功", Text);
                    base.DialogResult = DialogResult.OK;
                }
                else
                {
                    base.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void sbtnAdd_Click(object sender, EventArgs e)
        {
            AppPublic.Contorl.F_UserForBlackIP_Selectlist sub = new AppPublic.Contorl.F_UserForBlackIP_Selectlist(model.ID);
            if (sub.ShowDialog() == DialogResult.OK)
            {
                Query();
            }
        }

        private void sbtnRemove_Click(object sender, EventArgs e)
        {
            object obj_ID = gvUser.GetFocusedRowCellValue("User_ID");
            if (obj_ID != null)
            {
                if (bll.RemoveUserForBlackIP(model.ID, AppPublic.appPublic.GetObjGUID(obj_ID)))
                {
                    AppPublic.appPublic.ShowMessage("移除成功",Text);
                    Query();
                }
            }
        }
    }
}
