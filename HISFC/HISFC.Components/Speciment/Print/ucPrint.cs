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

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class ucPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrint()
        {
            InitializeComponent();
        }

        private DisTypeManage disMgr = new DisTypeManage();

        private string GenerateSql()
        {
            string sql = "";
            if (rbtSubSpec.Checked)
            {
                sql = "select distinct SPEC_SOURCE.HISBARCODE,subBarCode, SPEC_TYPE.SPECIMENTNAME,SPEC_DISEASETYPE.DISEASENAME \n" +
                      " from spec_subspec , SPEC_SOURCE,Spec_Type,spec_diseaseTYpe \n" +
                      " where spec_subspec.SPECID = SPEC_SOURCE.SPECID and SPEC_SOURCE.ORGORBLOOD = 'B' and SPEC_TYPE.SPECIMENTTYPEID = SPEC_SUBSPEC.SPECTYPEID\n" +
                      " and SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID";
                if (dtpStart.Value != null) sql += " and storetime>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                if (dtpEnd.Value != null)
                    sql += " and storetime<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                if (txtBarCode.Text.Trim() != "") sql += " and subBarCode like '%" + txtBarCode.Text.Trim() + "%'";
                if (cmbDis.SelectedValue != null && cmbDis.Text.Trim() != "")
                {
                    sql += " and SPEC_SOURCE.DISEASETYPEID = " + cmbDis.SelectedValue.ToString();
                }

            }
            if (rbtOrg.Checked)
            {
                sql = "select distinct SPEC_SOURCE.HISBARCODE,subBarCode, SPEC_TYPE.SPECIMENTNAME,SPEC_DISEASETYPE.DISEASENAME, st.TUMORPOS  \n" +
                                    " from spec_subspec , SPEC_SOURCE, SPEC_TYPE,spec_diseaseTYpe, spec_source_store st\n" +
                                    " where spec_subspec.SPECID = SPEC_SOURCE.SPECID and SPEC_SOURCE.ORGORBLOOD = 'O' and SPEC_TYPE.SPECIMENTTYPEID = SPEC_SUBSPEC.SPECTYPEID"+
                                    " and SPEC_DISEASETYPE.DISEASETYPEID = SPEC_SOURCE.DISEASETYPEID and st.sotreid = spec_subspec.storeid ";

                if (dtpStart.Value != null)
                    sql += " and st.storetime>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                if (dtpEnd.Value != null)
                    sql += " and st.storetime<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                if (txtBarCode.Text.Trim() != "") sql += " and subBarCode like '%" + txtBarCode.Text.Trim() + "%'";
                if (cmbDis.SelectedValue != null && cmbDis.Text.Trim() != "")
                {
                    sql += " and SPEC_SOURCE.DISEASETYPEID = " + cmbDis.SelectedValue.ToString();
                }
            }
            return sql;
        }

        private void LoadSubSpec()
        {
            SubSpecManage subSpecManage = new SubSpecManage();
            string sql = GenerateSql();
            DataSet ds = new DataSet();
            subSpecManage.ExecQuery(sql, ref ds);
            if (ds == null || ds.Tables.Count == 0)
            {
                return;
            }
            neuSpread1_Sheet1.AutoGenerateColumns = false;
            neuSpread1_Sheet1.DataSource = null;
            neuSpread1_Sheet1.DataSource = ds.Tables[0];
            neuSpread1_Sheet1.BindDataColumn(1, "HISBARCODE");
            neuSpread1_Sheet1.BindDataColumn(2, "SUBBARCODE");
            neuSpread1_Sheet1.BindDataColumn(3, "SPECIMENTNAME");
            neuSpread1_Sheet1.BindDataColumn(4, "DISEASENAME");
            if (rbtOrg.Checked)
            {
                neuSpread1_Sheet1.BindDataColumn(5, "TUMORPOS");
            }
        }

        public override int Query(object sender, object neuObject)
        {
            LoadSubSpec();
            return base.Query(sender, neuObject);
        }

        private void rbtCheckedChanged(object sender, EventArgs e)
        {
            if (rbtOrg.Checked)
            {
                neuSpread1_Sheet1.Columns[5].Visible = true;
            }

            if (rbtSubSpec.Checked)
            {
                cmbPrinter.Text = "贝迪";
                neuSpread1_Sheet1.Columns[5].Visible = false;
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
                                    //SubSpecManage subSpecManage = new SubSpecManage();
                                    //string pos = neuSpread1_Sheet1.Cells[i, 5].Value.ToString();
                                    //string posAbre = subSpecManage.ExecSqlReturnOne("select code from com_dictionary where  type ='SpecPos' and name ='" + pos + "'");

                                    //tumorPosList.Add(neuSpread1_Sheet1.Cells[i, 5].Value.ToString());
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

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    FarPoint.Win.Spread.Row r = neuSpread1_Sheet1.Rows[i];
                    if (r.Visible && neuSpread1_Sheet1.Cells[i, 2].Value != null && neuSpread1_Sheet1.Cells[i, 2].ToString().Trim() != "")
                    {
                        neuSpread1_Sheet1.Cells[i, 0].Value = (object)1;
                        //rowIndexList.Add(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    neuSpread1_Sheet1.Cells[i, 0].Value = (object)false;
                    //rowIndexList.Clear();
                }
            }
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

        private void ucPrint_Load(object sender, EventArgs e)
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
    }
}