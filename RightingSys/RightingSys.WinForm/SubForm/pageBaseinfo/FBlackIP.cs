using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RightingSys.WinForm.AppPublic.Enum;

namespace RightingSys.WinForm.SubForm.pageBaseinfo
{
    public partial class FBlackIP : BaseForm
    {
        BLL.ACL_BlackIP bll = new BLL.ACL_BlackIP();
        public FBlackIP()
        {
            InitializeComponent();
        }

        public override void InitFeatureButton()
        {
            base.SetFeatureButton(new FeatureButton[] { FeatureButton.Add,FeatureButton.Modify,FeatureButton.Delete, FeatureButton.Query });
        }
        public override void AddNew()
        {
            FBlackIP_User sub = new FBlackIP_User();
            if (sub.ShowDialog() == DialogResult.OK)
            {
                Query();
            }
        }
        public override void Modify()
        {
            object obj_ID = gvBlackIP.GetFocusedRowCellValue("ID");
            if (obj_ID != null)
            {
                FBlackIP_User sub = new FBlackIP_User(AppPublic.appPublic.GetObjGUID( obj_ID));
                if (sub.ShowDialog() == DialogResult.OK)
                {
                    Query();
                }
            }
        }
        public override void Delete()
        {
            object obj_ID = gvBlackIP.GetFocusedRowCellValue("ID");
            if (obj_ID != null)
            {
                if (bll.Delete(AppPublic.appPublic.GetObjGUID(obj_ID)))
                {
                    if (AppPublic.appPublic.GetMessageBoxYesNoResult("是否删除该记录？", Text))
                    {
                        AppPublic.appPublic.ShowMessage("删除成功", Text);
                    }
                }
            }
        }
        public override void Query()
        {
            gcBlackIP.DataSource = bll.Query();
        }

        private void FBlackIP_Load(object sender, EventArgs e)
        {
            Query();
        }

        private void gvBlackIP_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AuthorizeType")
            {
                e.DisplayText =e.Value.ToString() =="1" ?"白名单" : "黑名单";
            }
            if (e.Column.FieldName == "IsEnabled")
            {
                e.DisplayText = e.Value.ToString() == "1" ? "已启用" : "未启用";
            }
        }
    }
}
