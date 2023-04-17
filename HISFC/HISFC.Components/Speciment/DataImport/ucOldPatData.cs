using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.DataImport
{
    public partial class ucOldPatData : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;

        private string sql = "";
        private string sqlPatNo = "";
        private string sqlPatName = "";

        private PatientManage patManage = new PatientManage();

        public ucOldPatData()
        {
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            InitializeComponent();
        }

        private void FilterDataTable(ref DataTable dt)
        {
            try
            {
                Dictionary<string, string> dicTmp = new Dictionary<string, string>();
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if (!dicTmp.ContainsKey(dr["PAT_NO"].ToString()))
                    {
                        dicTmp.Add(dr["PAT_NO"].ToString(), "");
                    }
                    else
                    {
                        dt.Rows.RemoveAt(i);                        
                    }
                }
            }
            catch
            { }
        }


        private void Save()
        {
            string updateSql = "update SPEC_OLDDATA set both_noval = '{1}' where oldid = {0} ";
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                string val = neuSpread1_Sheet1.Cells[i, 4].Text;
                if (val == null)
                    continue;
                
                    string id = neuSpread1_Sheet1.Cells[i, 5].Text;
                    string tmpsql = string.Format(updateSql, new string[] { id ,val});
                    patManage.ExecNoQuery(tmpsql);                
            }
        }

        private DataTable ArrToTable(ArrayList arr)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] 
                                            {
                                                new DataColumn("住院号",typeof(string)),
                                                new DataColumn("姓名",typeof(string)),
                                                new DataColumn("性别",typeof(string))                                              
                                            });
            foreach (FS.FrameWork.Models.NeuObject o in arr)
            {
                DataRow dr = dt.Rows.Add(new string[] { o.ID, o.Name, o.Memo });             
            }           
            return dt;
        }

        /// <summary>
        /// 从病案中获取病人的信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="patNo">病历号</param>
        /// <param name="val">0姓名和病历号没找到，1 姓名相符，2 病历号相符，3 全部相符</param>
        /// <returns></returns>
        private DataTable GetPat(string name, string patNo, ref string val)
        {
            ArrayList arr = new ArrayList();
            string tmp = "";
            if (chkName.Checked)
            {
                tmp = string.Format(sqlPatName, new string[] { patNo, name });

            }
            else
            {
                tmp = string.Format(sqlPatNo, new string[] { patNo, name });
            }
            arr = patManage.GetPatientForOldData(patNo, name, tmp, ref val);
            foreach (FS.FrameWork.Models.NeuObject o in arr)
            {
                if (name == o.Name && o.ID == patNo)
                {
                    val = "3";
                    return null;
                }                
            }
            return ArrToTable(arr);
        }        
        
        private void Query()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询信息，请稍候...");
            neuSpread1_Sheet1.Rows.Count = 0;
            if (chkName.Checked)
            {
                sql = "select * from SPEC_OLDDATA where (both_noval ='' or both_noval is null) and name is not null ";
            }
            else
            {
                sql = "select * from SPEC_OLDDATA where (both_noval ='' or both_noval is null) and PAT_NO is not null ";
            }
            sql += " where OPERTIME between '{0}' and '{1}'";
            sql = string.Format(sql, new string[] { dtpStart.Value.Date.ToString(), dtpEnd.Value.Date.AddDays(1.0).ToString() });
            sqlPatNo = " select * from COM_PATIENTINFO p where CARD_NO = '{0}' ";
            sqlPatName = "select * from COM_PATIENTINFO p where NAME = '{1}'";
            DataSet dsResult = new DataSet();
            patManage.ExecQuery(sql, ref dsResult);
            #region
            if (dsResult == null || dsResult.Tables.Count <= 0)
            {
                return;
            }
            else
            {
                int count1 = dsResult.Tables[0].Rows.Count;

                DataSet ds = new DataSet();
                DataTable dtResultTmp = dsResult.Tables[0].Copy();
                dtResultTmp.Columns.Add("PATVAL");
                this.FilterDataTable(ref dtResultTmp);
                int count2 = dsResult.Tables[0].Rows.Count;

                lblCount.Text = "记录: 共" + dtResultTmp.Rows.Count.ToString() + "条";
                ds.Tables.Add(dtResultTmp);
                DataTable dtPat = ds.Tables.Add("Pat");
                dtPat.Columns.AddRange(new DataColumn[] 
                                            {
                                                new DataColumn("住院号",typeof(string)),
                                                 new DataColumn("姓名",typeof(string)),
                                                new DataColumn("性别",typeof(string)),
                                                new DataColumn("标本号",typeof(string)),
                                                new DataColumn("住院科室",typeof(string)),
                                                new DataColumn("年龄",typeof(string))
                                            });

                #region
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        dr["PAT_NO"] = dr["PAT_NO"].ToString().PadLeft(10, '0');
                        string pat = dr["PAT_NO"] == null ? "" : dr["PAT_NO"].ToString().PadLeft(10, '0');
                        string name = dr["NAME"] == null ? "" : dr["NAME"].ToString();
                        string specNo = dr[""] == null ? "" : dr[""].ToString();
                        if (pat == "")
                        {
                            continue;
                        }
                        string val = "";

                        DataTable dtTmp = GetPat(name, pat, ref val);
                        dr["PATVAL"] = val;
                        if (dtTmp == null || dtTmp.Rows.Count <= 0)
                        {
                            continue;
                        }
                        dtPat.Merge(dtTmp);
                    }

                    neuSpread1_Sheet1.AutoGenerateColumns = false;
                    neuSpread1_Sheet1.DataSource = ds;
                    neuSpread1_Sheet1.DataAutoSizeColumns = false;
                    neuSpread1_Sheet1.DataAutoCellTypes = false;
                    neuSpread1_Sheet1.BindDataColumn(0, "PAT_NO");
                    neuSpread1_Sheet1.BindDataColumn(1, "NAME");
                    neuSpread1_Sheet1.BindDataColumn(2, "GENDER");
                    neuSpread1_Sheet1.BindDataColumn(3, "OPERTIME");
                    neuSpread1_Sheet1.BindDataColumn(4, "PATVAL");
                    neuSpread1_Sheet1.BindDataColumn(5, "OLDID");
                    neuSpread1_Sheet1.BindDataColumn(6, "SPECID");
                    neuSpread1_Sheet1.BindDataColumn(7, "INDEPT");
                    neuSpread1_Sheet1.BindDataColumn(8, "OLDY");
                    neuSpread1_Sheet1.Columns[4].Visible = false;
                    for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        if (neuSpread1_Sheet1.Cells[i, 4].Text == "3")
                        {
                            neuSpread1_Sheet1.Rows[i].BackColor = Color.Azure;
                        }
                        else
                        {
                            neuSpread1_Sheet1.Rows[i].BackColor = Color.White;
                        }
                    }

                    if (chkName.Checked)
                    {
                        ds.Relations.Add("PAT", ds.Tables[0].Columns["NAME"], dtPat.Columns["姓名"]);
                    }
                    else
                    {
                        ds.Relations.Add("PAT", ds.Tables[0].Columns["PAT_NO"], dtPat.Columns["住院号"]);

                    }
                }
                catch
                {

                }
                #endregion
                #region
                finally
                {
                    neuSpread2_Sheet1.DataSource = dtPat;
                    FarPoint.Win.Spread.SheetView tmpView;
                    for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        if (neuSpread1_Sheet1.Cells[i, 4].Text == "3")
                        {
                            neuSpread1_Sheet1.Rows[i].BackColor = Color.Azure;
                        }
                        else
                        {
                            neuSpread1_Sheet1.Rows[i].BackColor = Color.White;
                        }
                        tmpView = neuSpread1_Sheet1.GetChildView(i, 0);
                        if (tmpView == null || tmpView.RowCount <= 0)
                        {
                            continue;
                        }
                        tmpView.AutoGenerateColumns = false;
                        tmpView.Columns.Count = 3;

                        tmpView.BindDataColumn(0, "住院号");
                        tmpView.BindDataColumn(1, "姓名");
                        tmpView.BindDataColumn(2, "性别");
                        int count = tmpView.Rows.Count;

                        if (count > 0)
                        {
                            //neuSpread1_Sheet1.Rows[i].Label = count.ToString();
                            neuSpread1_Sheet1.ExpandRow(i, true);
                        }
                        if (count <= 0)
                        {
                            neuSpread1_Sheet1.Rows[i].Visible = false;
                            //neuSpread1_Sheet1.Rows[i].Label = "无";
                            neuSpread1_Sheet1.ExpandRow(i, false);
                        }
                    }
                }
#endregion
            }
            #endregion

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }
    
        public override int Query(object sender, object neuObject)
        {
            this.Query();
            return base.Query(sender, neuObject);
        }

        public override int Save(object sender, object neuObject)
        {
            try
            {
                this.Save();
                MessageBox.Show("保存成功！");
                this.Query();
            }
            catch
            {
                MessageBox.Show("保存数据失败！");
            }
            return base.Save(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "查询结果导出，选择Excel文件保存位置";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "标本旧数据";
            DialogResult dr = saveFileDiaglog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
            }

            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "HIS数据";
            dr = saveFileDiaglog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.neuSpread2.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
            } 
            return base.Export(sender, neuObject);
        }
        
    }
}
