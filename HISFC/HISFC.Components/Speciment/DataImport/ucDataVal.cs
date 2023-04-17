using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.DataImport
{
    public partial class ucDataVal : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        ExlToDb2Manage exlMgr = new ExlToDb2Manage();

        public ucDataVal()
        {
            InitializeComponent();
        }

        private string GetFileName()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = "*.xls";
            //fileDialog.Filter = "Excel(*.xls)";
            fileDialog.ShowDialog();
            return fileDialog.FileName;
        }

        private void GetData(string fileName, ref DataSet ds)
        {
            exlMgr.ExlConnect(fileName, ref ds);
        }

        private void btnB1_Click(object sender, EventArgs e)
        {
            try
            {
                neuSpread1_Sheet1.Rows.Count = 0;
                txtFile1.Text = GetFileName();
                DataSet ds = new DataSet();
                GetData(txtFile1.Text, ref ds);
                neuSpread1_Sheet1.DataSource = ds;
                neuSpread1_Sheet1.Columns[0].Visible = false;
            }
            catch
            { }
        }

        private void btnB2_Click(object sender, EventArgs e)
        {
            try
            {
                neuSpread2_Sheet1.Rows.Count = 0;
                txtFile2.Text = GetFileName();
                DataSet ds = new DataSet();
                GetData(txtFile2.Text, ref ds);
                neuSpread2_Sheet1.DataSource = ds;
                neuSpread2_Sheet1.Columns[0].Visible = false;
            }
            catch
            { }
        }

        public override int Query(object sender, object neuObject)
        {
            return base.Query(sender, neuObject);
        }

        public override int Save(object sender, object neuObject)
        {
            try
            {
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {
                    string patNo = neuSpread1_Sheet1.Cells[i, 1].Text.PadLeft(10, '0');
                    string name = neuSpread1_Sheet1.Cells[i, 3].Text.Trim();

                    for (int k = 0; k < neuSpread2_Sheet1.RowCount; k++)
                    {
                        string patNo1 = neuSpread2_Sheet1.Cells[k, 1].Text.PadLeft(10, '0');
                        string name1 = neuSpread2_Sheet1.Cells[k, 2].Text.Trim();
                        if (patNo == patNo1 && name.ToLower().Trim() == name1.ToLower().Trim())
                        {
                            neuSpread1_Sheet1.Rows[i].BackColor = Color.SkyBlue;
                            neuSpread2_Sheet1.RemoveRows(k, 1);
                            break;
                        }
                    }
                }
            }
            catch
            {}
            return base.Save(sender, neuObject);

           
        }

        public override int Export(object sender, object neuObject)
        {
            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "结果导出，选择Excel文件保存位置";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "未匹配数据";
            DialogResult dr = saveFileDiaglog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.neuSpread2.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
            }
            
            return base.Export(sender, neuObject);
        }
    }
}
