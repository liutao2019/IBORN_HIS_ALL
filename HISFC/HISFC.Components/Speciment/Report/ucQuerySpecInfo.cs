using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucQuerySpecInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQuerySpecInfo()
        {
            InitializeComponent();
        }

        private DisTypeManage disMgr = new DisTypeManage();
        private SpecTypeManage typeMgr = new SpecTypeManage();

        private string specCols = " and SPEC_SOURCE.SPEC_NO in( ";

        private string GenerateSql(bool isFileImp)
        {
            string sql = "";
            if (rbtSubSpec.Checked)
            {
                sql = "select distinct SPEC_SOURCE.HISBARCODE,subBarCode,SPEC_BOX.BOXBARCODE, SPEC_SUBSPEC.BOXENDROW,SPEC_SUBSPEC.BOXENDCOL, SPEC_TYPE.SPECIMENTNAME,SPEC_DISEASETYPE.DISEASENAME, st.TUMORPOS \n" +
                      " from spec_subspec , SPEC_BOX,SPEC_SOURCE,Spec_Type,spec_diseaseTYpe, spec_source_store st \n" +
                      " where spec_subspec.SPECID = SPEC_SOURCE.SPECID and SPEC_SOURCE.ORGORBLOOD = 'B' and SPEC_TYPE.SPECIMENTTYPEID = SPEC_SUBSPEC.SPECTYPEID\n" +
                      " and SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID and st.sotreid = spec_subspec.storeid and SPEC_BOX.BOXID = SPEC_SUBSPEC.BOXID";
                if (isFileImp)
                {
                    sql += specCols;
                }
                else
                {
                    if (cbxNeedTime.Checked)
                    {
                        if (dtpStart.Value != null)
                            sql += " and spec_subspec.storetime>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                        if (dtpEnd.Value != null)
                            sql += " and spec_subspec.storetime<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                    }
                    if (txtBarCode.Text.Trim() != "")
                        sql += " and subBarCode like '%" + txtBarCode.Text.Trim() + "%'";
                    if (tbSpecNo.Text.Trim() != "")
                        sql += " and SPEC_SOURCE.SPEC_NO = '" + tbSpecNo.Text.Trim().PadLeft(6, '0') + "'";
                }
                if (cmbDis.SelectedValue != null && cmbDis.Text.Trim() != "")
                {
                    sql += " and SPEC_SOURCE.DISEASETYPEID = " + cmbDis.SelectedValue.ToString();
                }
                if (cmbSpecType.Text.Trim() != "" && cmbSpecType.SelectedValue != null)
                {
                    sql += "\n and st.SPECTYPEID = " + cmbSpecType.SelectedValue.ToString() + " ";
                }
            }
            if (rbtOrg.Checked)
            {
                sql = "select distinct SPEC_SOURCE.HISBARCODE,subBarCode,SPEC_BOX.BOXBARCODE, SPEC_SUBSPEC.BOXENDROW,SPEC_SUBSPEC.BOXENDCOL,SPEC_TYPE.SPECIMENTNAME,SPEC_DISEASETYPE.DISEASENAME, st.TUMORPOS  \n" +
                                    " from spec_subspec ,SPEC_BOX, SPEC_SOURCE, SPEC_TYPE,spec_diseaseTYpe, spec_source_store st\n" +
                                    " where spec_subspec.SPECID = SPEC_SOURCE.SPECID and SPEC_SOURCE.ORGORBLOOD = 'O' and SPEC_TYPE.SPECIMENTTYPEID = SPEC_SUBSPEC.SPECTYPEID" +
                                    " and SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID and st.sotreid = spec_subspec.storeid and SPEC_BOX.BOXID = SPEC_SUBSPEC.BOXID";
                if (isFileImp)
                {
                    sql += specCols;
                }
                else
                {
                    if (cbxNeedTime.Checked)
                    {
                        if (dtpStart.Value != null)
                            sql += " and st.storetime>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                        if (dtpEnd.Value != null)
                            sql += " and st.storetime<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                    }
                    if (txtBarCode.Text.Trim() != "")
                        sql += " and subBarCode like '%" + txtBarCode.Text.Trim() + "%'";
                    if (tbSpecNo.Text.Trim() != "")
                        sql += " and SPEC_SOURCE.SPEC_NO = '" + tbSpecNo.Text.Trim().PadLeft(6, '0') + "'";
                }
                if (cmbDis.SelectedValue != null && cmbDis.Text.Trim() != "")
                {
                    sql += " and SPEC_SOURCE.DISEASETYPEID = " + cmbDis.SelectedValue.ToString();
                }
                if (cmbSpecType.Text.Trim() != "" && cmbSpecType.SelectedValue != null)
                {
                    sql += "\n and st.SPECTYPEID = " + cmbSpecType.SelectedValue.ToString() + " ";
                }
            }
            return sql;
        }

        private void LoadSubSpec(bool isFileImp)
        {
            SubSpecManage subSpecManage = new SubSpecManage();
            string sql = GenerateSql(isFileImp);
            DataSet ds = new DataSet();
            subSpecManage.ExecQuery(sql, ref ds);
            if (ds == null || ds.Tables.Count == 0)
            {
                neuSpread1_Sheet1.DataSource = null;
                return;
            }
            neuSpread1_Sheet1.AutoGenerateColumns = false;
            neuSpread1_Sheet1.DataSource = null;
            neuSpread1_Sheet1.DataSource = ds.Tables[0];
            neuSpread1_Sheet1.BindDataColumn(0, "HISBARCODE");
            neuSpread1_Sheet1.BindDataColumn(1, "SUBBARCODE");

            neuSpread1_Sheet1.BindDataColumn(2, "BOXBARCODE");
            neuSpread1_Sheet1.BindDataColumn(3, "BOXENDROW");
            neuSpread1_Sheet1.BindDataColumn(4, "BOXENDCOL");

            neuSpread1_Sheet1.BindDataColumn(5, "SPECIMENTNAME");
            neuSpread1_Sheet1.BindDataColumn(6, "DISEASENAME");
            neuSpread1_Sheet1.BindDataColumn(7, "TUMORPOS");
            
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(neuSpread1_Sheet1.Cells[i, 2].Text))
                {
                    string loc = ParseLocation.ParseSpecBox(neuSpread1_Sheet1.Cells[i, 2].Text.Trim());
                    neuSpread1_Sheet1.Cells[i, 2].Text = loc;
                }
            }
            neuSpread1_Sheet1.Columns[2].Width = 260;
            if (rbtSubSpec.Checked)
            {
                neuSpread1_Sheet1.Columns[7].Visible = false;
            }
            if (rbtOrg.Checked)
            {
                neuSpread1_Sheet1.Columns[7].Visible = true;
            }
        }

        public override int Query(object sender, object neuObject)
        {
            LoadSubSpec(false);
            return base.Query(sender, neuObject);
        }

        private void rbtCheckedChanged(object sender, EventArgs e)
        {
            if (rbtOrg.Checked)
            {
                neuSpread1_Sheet1.Columns[7].Visible = true;
            }

            if (rbtSubSpec.Checked)
            {
                neuSpread1_Sheet1.Columns[7].Visible = false;
            }
        }

        public override int Print(object sender, object neuObject)
        {
            try
            {
                List<string> barCodeList = new List<string>();
                List<string> sequenceList = new List<string>();
                List<string> disTypeList = new List<string>();
                List<string> numList = new List<string>();
                List<string> specTypeList = new List<string>();
                List<string> tumorPosList = new List<string>();

                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    string chk = "";
                    if (neuSpread1_Sheet1.Cells[i, 0].Value != null)
                    {
                        chk = neuSpread1_Sheet1.Cells[i, 0].Value.ToString();
                        if (chk.ToUpper() == "TRUE" || chk == "1")
                        {
                            string barCode = neuSpread1_Sheet1.Cells[i, 2].Value.ToString();
                            string sequence = "";
                            string num = "";
                            if (barCode.Length == 9)
                            {
                                sequence = barCode.Substring(1, 6);
                                num = barCode.Substring(8, 1);
                            }
                            if (barCode.Length >= 11)
                            {
                                num = barCode.Substring(barCode.Length - 1, 1);
                                sequence = barCode.Substring(0, 6);
                            }
                            if (rbtOrg.Checked)
                            {
                                try
                                {
                                    tumorPosList.Add(barCode.Substring(barCode.Length - 3, 1));
                                }
                                catch
                                {
                                    MessageBox.Show("获取信息失败！");
                                    return 0;
                                }
                            }
                            string disType = neuSpread1_Sheet1.Cells[i, 4].Value.ToString();
                            string specType = neuSpread1_Sheet1.Cells[i, 3].Value.ToString();

                            barCodeList.Add(barCode);
                            sequenceList.Add(sequence);
                            disTypeList.Add(disType);
                            specTypeList.Add(specType);
                            numList.Add(num);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                if (barCodeList.Count > 0)
                {
                    if (rbtOrg.Checked)
                    {
                        if (rbtOperRoom.Checked)
                        {
                            PrintLabel.bradyIP = "172.16.130.25";
                        }
                        PrintLabel.Print2DBarCodeOrg(barCodeList, sequenceList, tumorPosList, disTypeList, numList);
                    }
                    if (rbtSubSpec.Checked)
                    {
                        rbtSpec.Checked = true;
                        PrintLabel.Print2DBarCode(barCodeList, sequenceList, specTypeList, disTypeList, numList);
                    }
                }

            }
            catch
            {

            }
            return base.Print(sender, neuObject);
        }

        private void neuSpread1_AutoFilteredColumn(object sender, FarPoint.Win.Spread.AutoFilteredColumnEventArgs e)
        {
            string filterString = e.FilterString;
            if (filterString == "(All)")
            {
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {
                    FarPoint.Win.Spread.Row r = neuSpread1_Sheet1.Rows[i];
                    r.Visible = true;
                }
                return;
            }
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                FarPoint.Win.Spread.Row r = neuSpread1_Sheet1.Rows[i];
                string s1 = neuSpread1_Sheet1.Cells[i, e.Column].Value.ToString();
                if (s1 != filterString)
                {
                    r.Visible = false;
                }
                else
                    r.Visible = true;
            }
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            this.Query(null, null);
        }

        private void ucQuerySpecInfo_Load(object sender, EventArgs e)
        {
            IPAddress[] addressList = Dns.GetHostAddresses(Dns.GetHostName());
            string ip = "";
            try
            {
                ip = addressList[0].ToString();
            }
            catch
            {
                ip = "";
            }
            FS.FrameWork.Management.ControlParam controlMgr = new FS.FrameWork.Management.ControlParam();
            FS.HISFC.Models.Base.ControlParam cpm = controlMgr.QueryControlInfoByCode("OPERIP");
            if (cpm != null)
            {
                string operIP = cpm.ControlerValue;
                if (ip == operIP)
                {
                    rbtOrg.Checked = true;
                    rbtOperRoom.Checked = true;
                }
            }
            this.DisTypeBinding();
            this.BindingSpecType();
        }

        private void DisTypeBinding()
        {
            Dictionary<int, string> dicDisType = disMgr.GetAllDisType();
            if (dicDisType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDis.DisplayMember = "Value";
                cmbDis.ValueMember = "Key";
                cmbDis.DataSource = bs;
            }
            cmbDis.Text = "";
        }

        /// <summary>
        /// 绑定标本类型
        /// </summary>
        private void BindingSpecType()
        {
            ArrayList arrType = new ArrayList();
            arrType = typeMgr.GetAllSpecType();
            if (arrType.Count > 0)
            {
                cmbSpecType.DisplayMember = "SpecTypeName";
                cmbSpecType.ValueMember = "SpecTypeID";
                cmbSpecType.DataSource = arrType;
            }
            cmbSpecType.Text = "";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            txtSpecNoExl.Text = fileDialog.FileName;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ExlToDb2Manage exlMgr = new ExlToDb2Manage();
            DataSet ds = new DataSet();
            exlMgr.ExlConnect(txtSpecNoExl.Text, ref ds);
            if (ds == null)
            {
                MessageBox.Show("读取文件失败");
            }

            int index = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询明细信息，请稍候...");
                Application.DoEvents();

                specCols = " and SPEC_SOURCE.SPEC_NO in( ";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        if (specCols.Trim() == "")
                        {
                            index++;
                            continue;
                        }
                        if (index == ds.Tables[0].Rows.Count - 1)
                        {
                            index++;
                            specCols += "'" + dr[0].ToString().Trim().PadLeft(6, '0') + "')\n";
                        }
                        else
                        {
                            index++;
                            specCols += "'" + dr[0].ToString().Trim().PadLeft(6, '0') + "',\n";
                        }
                    }
                    catch
                    {
                        index++;
                        continue;
                    }
                }
                this.LoadSubSpec(true);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        public override int Export(object sender, object neuObject)
        {
            try
            {
                SaveFileDialog saveFileDiaglog = new SaveFileDialog();

                saveFileDiaglog.Title = "查询结果导出，选择Excel文件保存位置";
                saveFileDiaglog.RestoreDirectory = true;
                saveFileDiaglog.InitialDirectory = Application.StartupPath;
                saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
                saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "标本位置";
                DialogResult dr = saveFileDiaglog.ShowDialog();

                neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
            }
            catch
            {
            }
            return base.Export(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                this.neuSpread1_Sheet1.PrintInfo.Header = "查询标本位置信息";
                this.neuSpread1_Sheet1.PrintInfo.Preview = true;
                this.neuSpread1_Sheet1.PrintInfo.ShowPrintDialog = true;
                this.neuSpread1_Sheet1.PrintInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
                this.neuSpread1.PrintSheet(0);
            }
            catch
            {
            }
            return base.OnPrint(sender, neuObject);
        }
    }
}