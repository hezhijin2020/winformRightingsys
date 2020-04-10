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
    public partial class FOprationLogs : BaseForm
    {
        BLL.OperationLog bll = new BLL.OperationLog();
        public FOprationLogs()
        {
            InitializeComponent();
            Query();
        }
        public override void InitFeatureButton()
        {
            base.SetFeatureButton(new FeatureButton[] {FeatureButton.Query,FeatureButton.Export});
        }
        public override void Query()
        {
            gcData.DataSource = bll.Query();
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
