using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucGetDiagnose : FS.FrameWork.WinForms.Controls.ucBaseControl
	{
        private BaseManage baseManage;
        public ucGetDiagnose()
		{
			InitializeComponent();
            baseManage = new BaseManage();
		}

        /// <summary>
        /// ���������������Ϣ���ж�ȡ��Ϣ
        /// </summary>
        private void Query()
        {
            neuSpread1_Sheet1.Rows.Count = 0;
            neuSpread1_Sheet1.RowHeader.Columns[0].Width = 100;
            string start = dtpStart.Value.Date.ToString();
            string end = dtpEnd.Value.AddDays(1.0).Date.ToString();
            DataSet ds = new DataSet();
            string inBase = chkInBase.Checked ? "1" : "0";
            baseManage.GetNotInBaseInfo(inBase, start, end, ref ds);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return;
            }
            else
            {
                DataTable dtDiagnose = ds.Tables.Add("Diagnose");
                dtDiagnose.Columns.AddRange(new DataColumn[] 
                                            {
                                                new DataColumn("סԺ��",typeof(string)),
                                                new DataColumn("�����",typeof(string)),
                                                new DataColumn("סԺ��ˮ��",typeof(string)),
                                                new DataColumn("¼������",typeof(string)),
                                                new DataColumn("�����ICD",typeof(string)),
                                                 new DataColumn("��̬��ICD",typeof(string)),
                                                new DataColumn("��̬��",typeof(string)),                                            
                                                new DataColumn("��ԺICD",typeof(string)),
                                                new DataColumn("��Ժ���",typeof(string)),
                                                new DataColumn("�����1ICD",typeof(string)),
                                                new DataColumn("�����1",typeof(string)),   
                                                new DataColumn("����ICD",typeof(string)),
                                                new DataColumn("�������",typeof(string))
                                            });
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        
                        string specId = dr["SPECID"].ToString();
                        if (dr["INPATIENT_NO"] == null)
                            continue;
                        string patientNo = dr["INPATIENT_NO"].ToString().Trim().Substring(4);
                         
                        DateTime dt1 = DateTime.Now;
                        DataTable dtTmp = baseManage.GetDiagnoseFromDiagnose(patientNo);
                        DateTime dt2 = DateTime.Now;
                        TimeSpan tp = dt2 - dt1;
                        if (dtTmp == null || dtTmp.Rows.Count <= 0)
                        {
                            continue;
                        }
                        dtDiagnose.Merge(dtTmp);
                    }
                    ds.Relations.Add("Diagnose", ds.Tables[0].Columns["סԺ��"], dtDiagnose.Columns["סԺ��"]);
                    neuSpread1_Sheet1.AutoGenerateColumns = false;
                    neuSpread1_Sheet1.DataSource = ds;
                    neuSpread1_Sheet1.DataAutoSizeColumns = false;
                    neuSpread1_Sheet1.DataAutoCellTypes = false;
                    neuSpread1_Sheet1.BindDataColumn(0, "SPECID");
                    neuSpread1_Sheet1.BindDataColumn(1, "INPATIENT_NO");
                    neuSpread1_Sheet1.BindDataColumn(2, "NAME");
                    neuSpread1_Sheet1.BindDataColumn(3, "MAIN_DIACODE");
                    neuSpread1_Sheet1.BindDataColumn(4, "MAIN_DIANAME");
                    neuSpread1_Sheet1.BindDataColumn(5, "MOD_ICD");
                    neuSpread1_Sheet1.BindDataColumn(6, "MOD_NAME");
                    neuSpread1_Sheet1.BindDataColumn(7, "INHOS_DIACODE");
                    neuSpread1_Sheet1.BindDataColumn(8, "INHOS_DIANAME");
                    neuSpread1_Sheet1.BindDataColumn(9, "MAIN_DIACODE1");
                    neuSpread1_Sheet1.BindDataColumn(10, "MAIN_DIANAME1");
                    neuSpread1_Sheet1.BindDataColumn(11, "MAIN_DIACODE2");
                    neuSpread1_Sheet1.BindDataColumn(12, "MAIN_DIANAME2");
                    neuSpread1_Sheet1.BindDataColumn(13, "CILINIC_DIACODE");
                    neuSpread1_Sheet1.BindDataColumn(14, "CLINIC_DIANAME");
                    neuSpread1_Sheet1.BindDataColumn(15, "M_DIAGICD");
                    neuSpread1_Sheet1.BindDataColumn(16, "סԺ��");
                    neuSpread1_Sheet1.BindDataColumn(17, "M_DIAGICDNAME");

                    FarPoint.Win.Spread.SheetView diagnose;
                    for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        diagnose = neuSpread1_Sheet1.GetChildView(i, 0);
                        diagnose.AutoGenerateColumns = false;
                        diagnose.DataAutoSizeColumns = false;
                        diagnose.Columns[1].AllowAutoSort = true;                         
                        for (int k = 0; k < diagnose.Columns.Count; k++)
                        {
                            diagnose.Columns[k].Width = 80; 
                            diagnose.Columns[k].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                            diagnose.Columns[k].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        }
                        int count = diagnose.Rows.Count;
                      
                        if (count > 0)
                        {
                            neuSpread1_Sheet1.Rows[i].Label = count.ToString();
                            neuSpread1_Sheet1.ExpandRow(i, true);
                        }
                        if (count <= 0)
                        {
                            neuSpread1_Sheet1.Rows[i].Visible = false;
                            neuSpread1_Sheet1.Rows[i].Label = "��";
                            neuSpread1_Sheet1.ExpandRow(i, false);
                        }                       
                      
                        diagnose.Columns[0].Visible = false;
                        diagnose.Columns[1].Width = 100;
                        diagnose.Columns[5].Width = 180; 
                    }
                }
                catch
                { }
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="operType">ҽ��վ��ֻ��ʾҽ��վ¼�����ϣ������⣺ֻ��ʾ�����⣬All����ʾȫ��</param>
        private void Filter(string operType)
        {
            FarPoint.Win.Spread.SheetView diagnose;
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                neuSpread1_Sheet1.ExpandRow(i, false);
                diagnose = neuSpread1_Sheet1.GetChildView(i, 0);
                string filterString = operType;
                if (filterString == "(All)")
                {
                    for (int k = 0; k < diagnose.RowCount; k++)
                    {
                        FarPoint.Win.Spread.Row r = diagnose.Rows[k];
                        string s1 = diagnose.Cells[k, 2].Value.ToString();
                        if (s1 == filterString)
                        {
                            r.Visible = false;
                        }
                        else
                            r.Visible = true;                        
                        //r.Visible = true;
                    }
                }
                else
                {
                    for (int j = 0; j < diagnose.RowCount; j++)
                    {
                        FarPoint.Win.Spread.Row r = diagnose.Rows[j];
                        string s1 = diagnose.Cells[j, 2].Value.ToString();
                        if (s1 != filterString)
                        {
                            r.Visible = false;
                        }
                        else
                            r.Visible = true;
                    }
                }
                neuSpread1_Sheet1.ExpandRow(i, true);
                //diagnose.Columns[2].AllowAutoFilter = true;
                //diagnose.AutoFilterColumn(2, operType, 1);
            }
        }

        private void ReadRecord(DiagnoseInfo diagInfo, int rowCount)
        {
            //neuSpread1_Sheet1.BindDataColumn(3, "MAIN_DIACODE");
            //neuSpread1_Sheet1.BindDataColumn(4, "MAIN_DIANAME");
            //neuSpread1_Sheet1.BindDataColumn(5, "MOD_ICD");
            //neuSpread1_Sheet1.BindDataColumn(6, "MOD_NAME");
            //neuSpread1_Sheet1.BindDataColumn(7, "INHOS_DIACODE");
            //neuSpread1_Sheet1.BindDataColumn(8, "INHOS_DIANAME");
            //neuSpread1_Sheet1.BindDataColumn(9, "MAIN_DIACODE1");
            //neuSpread1_Sheet1.BindDataColumn(10, "MAIN_DIANAME1");
            //neuSpread1_Sheet1.BindDataColumn(11, "MAIN_DIACODE2");
            //neuSpread1_Sheet1.BindDataColumn(12, "MAIN_DIANAME2");
            //neuSpread1_Sheet1.BindDataColumn(13, "CILINIC_DIACODE");
            //neuSpread1_Sheet1.BindDataColumn(14, "CLINIC_DIANAME");
            //neuSpread1_Sheet1.BindDataColumn(15, "M_DIAGICD");
            //neuSpread1_Sheet1.BindDataColumn(16, "סԺ��");
            //neuSpread1_Sheet1.BindDataColumn(17, "M_DIAGICDNAME");
            neuSpread1_Sheet1.Cells[rowCount, 3].Text = diagInfo.mainDiag;
            neuSpread1_Sheet1.Cells[rowCount, 4].Text = diagInfo.mainDiagName;
            neuSpread1_Sheet1.Cells[rowCount, 5].Text = diagInfo.posCode;
            neuSpread1_Sheet1.Cells[rowCount, 6].Text = diagInfo.posName;
            neuSpread1_Sheet1.Cells[rowCount, 7].Text = diagInfo.inDiag;
            neuSpread1_Sheet1.Cells[rowCount, 8].Text = diagInfo.inDiagName;
            neuSpread1_Sheet1.Cells[rowCount, 9].Text = diagInfo.mainDiag1;
            neuSpread1_Sheet1.Cells[rowCount, 10].Text = diagInfo.mainDiag1Name;
            neuSpread1_Sheet1.Cells[rowCount, 11].Text = diagInfo.mainDiag2;
            neuSpread1_Sheet1.Cells[rowCount, 12].Text = diagInfo.mainDiag2Name;
            neuSpread1_Sheet1.Cells[rowCount, 13].Text = diagInfo.clinicDiag;
            neuSpread1_Sheet1.Cells[rowCount, 14].Text = diagInfo.clinicDiagName;
            neuSpread1_Sheet1.Cells[rowCount, 15].Text = diagInfo.outDiag;
            neuSpread1_Sheet1.Cells[rowCount, 17].Text = diagInfo.outDiagName;
            neuSpread1_Sheet1.Rows[rowCount].Tag = diagInfo;
        }

        /// <summary>
        /// ������ȡ���ݵļ�¼
        /// </summary>
        private void IteratorSheet()
        {
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                FarPoint.Win.Spread.SheetView diagnose;
                diagnose = neuSpread1_Sheet1.GetChildView(i, 0);
                if (diagnose == null || diagnose.Rows.Count <= 0)
                {
                    continue;
                }
                for (int k = 0; k < diagnose.Rows.Count; k++)
                {
                    if (!diagnose.Rows[k].Visible)
                    {
                        continue;
                    }
                                                //new DataColumn("סԺ��",typeof(string)),
                                                //new DataColumn("סԺ��ˮ��",typeof(string)),
                                                //new DataColumn("¼������",typeof(string)),
                                                //new DataColumn("�����ICD",typeof(string)),
                                                //new DataColumn("�����",typeof(string)),
                                                // new DataColumn("��̬��ICD",typeof(string)),
                                                //new DataColumn("��̬��",typeof(string)),                                            
                                                //new DataColumn("��ԺICD",typeof(string)),
                                                //new DataColumn("��Ժ���",typeof(string)),
                                                //new DataColumn("�����1ICD",typeof(string)),
                                                //new DataColumn("�����1",typeof(string)),   
                                                //new DataColumn("����ICD",typeof(string)),
                                                //new DataColumn("�������",typeof(string))
                    DiagnoseInfo diagInfo = new DiagnoseInfo();
                    diagInfo.mainDiag = diagnose.Cells[k, 3].Text;
                    diagInfo.mainDiagName = diagnose.Cells[k, 4].Text;
                    diagInfo.posCode = diagnose.Cells[k, 5].Text;
                    diagInfo.posName = diagnose.Cells[k, 6].Text;                   
                    diagInfo.inDiag = diagnose.Cells[k, 7].Text;
                    diagInfo.inDiagName = diagnose.Cells[k, 8].Text;
                    diagInfo.mainDiag1 = diagnose.Cells[k, 9].Text;
                    diagInfo.mainDiag1Name = diagnose.Cells[k, 10].Text;
                    diagInfo.clinicDiag = diagnose.Cells[k, 11].Text;
                    diagInfo.clinicDiagName = diagnose.Cells[k, 12].Text;
                    ReadRecord(diagInfo, i);
                    break;
                }
            }
        }

        public override int Query(object sender, object neuObject)
        {
            Query();
            return base.Query(sender, neuObject);
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDoc.Checked)
            {
                if (chkBase.Checked)
                {
                    Filter("������");
                }
                if (!chkBase.Checked)
                {
                    Filter("");
                }
            }
            if (chkDoc.Checked)
            {
                if (chkBase.Checked)
                {
                    Filter("(All)");
                }
                if (!chkBase.Checked)
                {
                    Filter("ҽ��վ");
                }
            }
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                FarPoint.Win.Spread.SheetView diagnose;
                diagnose = neuSpread1_Sheet1.GetChildView(i, 0);
                if (diagnose == null || diagnose.Rows.Count <= 0)
                {
                    continue;                    
                }
                diagnose.AutoSortColumn(1, chkShow.Checked);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                IteratorSheet();
            }
            catch
            { }
        }

        public override int Save(object sender, object neuObject)
        {
            DialogResult dResult = MessageBox.Show("��Ϣ��ʵ���?", "���¼��", MessageBoxButtons.YesNo);
            if (dResult == DialogResult.No)
            {
                return 0;
            }
            SpecSourceManage sourceManage = new SpecSourceManage();
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                sourceManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                baseManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    DiagnoseInfo diagnoseInfo = neuSpread1_Sheet1.Rows[i].Tag as DiagnoseInfo;
                    if (diagnoseInfo == null)
                    {
                        continue;
                    }
                    string specId = neuSpread1_Sheet1.Cells[i, 0].Text.Trim().Split('-')[0];
                    if (sourceManage.UpdateInBase(specId) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ��");
                        
                        return -1;
                    }
                    SpecBase specBase = new SpecBase();
                    string sequence = "";
                    baseManage.GetNextSequence(ref sequence);
                    specBase.BaseID = Convert.ToInt32(sequence);
                    specBase.SpecSource.SpecId = Convert.ToInt32(specId);
                    specBase.CliDiagICD = diagnoseInfo.clinicDiag;
                    specBase.CliDiagName = diagnoseInfo.clinicDiagName;
                    specBase.InBaseTime = DateTime.Now;
                    specBase.InDiaICD = diagnoseInfo.inDiag;
                    specBase.InDiaName = diagnoseInfo.inDiagName;
                    specBase.MainDiagName2 = diagnoseInfo.mainDiag2Name;
                    specBase.MainDiaICD = diagnoseInfo.mainDiag;
                    specBase.MainDiaICD1 = diagnoseInfo.mainDiag1;
                    specBase.MainDiaICD2 = diagnoseInfo.mainDiag2;
                    specBase.MainDiaName = diagnoseInfo.mainDiagName;
                    specBase.MainDiaName1 = diagnoseInfo.mainDiag1Name;
                    specBase.ModICD = diagnoseInfo.posCode;
                    specBase.ModName = diagnoseInfo.posName;
                    specBase.OutDiaICD = diagnoseInfo.outDiag;
                    specBase.OutDiaName = diagnoseInfo.outDiagName;
                    if (baseManage.InsertBase(specBase) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ʧ��");
                        return -1;
                    }
                    //neuSpread1_Sheet1.BindDataColumn(0, "SPECID");
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("���³ɹ�!");
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            return base.Save(sender, neuObject);
        }
    }
}
