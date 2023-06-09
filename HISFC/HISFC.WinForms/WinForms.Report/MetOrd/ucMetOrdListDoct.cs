using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.MetOrd
{
    public partial class ucMetOrdListDoct : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucMetOrdListDoct()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                this.dwMain.Print();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印出错", "提示");
                return -1;
            }

        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <returns></returns>
        protected override int OnExport()
        {
            //如果存在多个DataWindow时导出方法需要重写，否则不需要重写该方法，根据焦点判断导出具体哪个DataWindow
            try
            {
                //导出Excel格式文件
                SaveFileDialog saveDial = new SaveFileDialog();
                saveDial.Filter = "Excel文件（*.xls）|*.xls";
                //文件名
                string fileName = string.Empty;
                if (saveDial.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveDial.FileName;
                }
                this.dwMain.SaveAs(fileName, Sybase.DataWindow.FileSaveAsType.Excel);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出出错", "提示");
                return -1;
            }
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            dwMain.Modify("t_date.text = '" + this.dtpBeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + " -- " + this.dtpEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            return base.OnRetrieve(beginTime, endTime);
        }
    }
}
