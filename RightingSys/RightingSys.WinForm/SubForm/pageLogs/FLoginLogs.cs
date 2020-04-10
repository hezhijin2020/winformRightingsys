using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RightingSys.WinForm.AppPublic.Enum;

namespace RightingSys.WinForm.SubForm.pageLogs
{
    public partial class FLoginLogs : BaseForm
    {
        BLL.LoginLog bll = new BLL.LoginLog();
        public FLoginLogs()
        {
            InitializeComponent();
            Query();
        }
        public override void InitFeatureButton()
        {
            base.SetFeatureButton(new FeatureButton[] {FeatureButton.Query,FeatureButton.Delete,FeatureButton.Export});
        }
        public override void Query()
        {
            gcData.DataSource = bll.Query();
        }
        public override void Delete()
        {
            if (MessageBox.Show("您确定删除90天前的日志吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (bll.Delete(DateTime.Now.Date.AddDays(-90)))
                {
                    MessageBox.Show("删除成功", "提示");
                }
                else
                {
                    MessageBox.Show("90天前没有日志供删除", "提示");
                }
            }
        }
        public override void Export()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.FileName = "用户登录信息";
            fileDialog.Title = "用户登录信息导出到Excel";
            fileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                gcData.ExportToXls(fileDialog.FileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
