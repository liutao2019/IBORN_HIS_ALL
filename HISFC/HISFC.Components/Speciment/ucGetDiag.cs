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

namespace FS.HISFC.Components.Speciment
{
    public partial class ucGetDiag : FS.FrameWork.WinForms.Controls.ucBaseControl
    {       
        private DiagnoseManage diagManage;
        private bool isInBase = false;
        private bool readBaseInfo = false;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;

        private string noNeedDiag = "";

        private Report.ucDiagNoseQuery ucDiagQuery;
        public ucGetDiag()
		{
			InitializeComponent();
            diagManage = new DiagnoseManage();
            loginPerson = new FS.HISFC.Models.Base.Employee();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            ucDiagQuery = new FS.HISFC.Components.Speciment.Report.ucDiagNoseQuery();
		}

        private void GetNoNeedDiag()
        {
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList arrTemp = new ArrayList();
            arrTemp = conMgr.GetAllList("NoNeedDiagForSpec");
            int i = 0;
            foreach (FS.FrameWork.Models.NeuObject o in arrTemp)
            {
                if (i == 0)
                {
                    noNeedDiag += o.ID;                 
                }
                //if (i == arrTemp.Count - 1)
                //{
                //    noNeedDiag+=
                //    continue;
                //}
                else
                {
                    noNeedDiag += "','" + o.ID;
                }
                i++;
            }
        }

        /// <summary>
        /// ���������������Ϣ���ж�ȡ��Ϣ
        /// </summary>
        private void Query()
        {
            if (tbResult.SelectedIndex == 1)
            {
                ucDiagQuery.Query();
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ϣ�����Ժ�...");
            neuSpread1_Sheet1.Rows.Count = 0;
            string start = "";
            string end = "";
            if (!chkAll.Checked)
            {
                start = dtpStart.Value.Date.ToString();
                end = dtpEnd.Value.AddDays(1.0).Date.ToString();
            }
            else
            {
                start = DateTime.MinValue.Date.ToString();
                end = DateTime.Now.AddDays(1.0).Date.ToString();
            }
            DataSet dsResult = new DataSet();
            string inBase = chkInBase.Checked ? "1" : "0";
            isInBase = chkInBase.Checked;
            readBaseInfo = chkBaseInfo.Checked;
            diagManage.GetNotInBaseInfo(inBase, start, end, ref dsResult);
          
            if (dsResult == null || dsResult.Tables.Count <= 0)
            {
                return;
            }
            else
            { 
                DataSet ds = new DataSet();
                DataTable dtResultTmp = dsResult.Tables[0].Copy();
                int count1 = dtResultTmp.Rows.Count;
                int count2 = dsResult.Tables[0].Rows.Count;
                if (chkBaseInfo.Checked)
                {
                    this.FilterDataTable(ref dtResultTmp);
                }
                lblCount.Text = "��¼: ��" + dtResultTmp.Rows.Count.ToString() + "��";
                ds.Tables.Add(dtResultTmp);
                DataTable dtDiagnose = ds.Tables.Add("Diagnose");
                dtDiagnose.Columns.AddRange(new DataColumn[] 
                                            {
                                                new DataColumn("סԺ��",typeof(string)),
                                                new DataColumn("סԺ��ˮ��",typeof(string)),
                                                new DataColumn("¼������",typeof(string)),
                                                new DataColumn("�������",typeof(string)),
                                                new DataColumn("�����ICD",typeof(string)),
                                                new DataColumn("�����",typeof(string)),
                                                new DataColumn("��̬��ICD",typeof(string)),
                                                new DataColumn("��̬��",typeof(string)),                                            
                                                new DataColumn("P_CODE",typeof(string)),
                                                new DataColumn("T_CODE",typeof(string)),
                                                new DataColumn("N_CODE",typeof(string)),
                                                new DataColumn("M_CODE",typeof(string)),
                                                new DataColumn("����",typeof(string))
                                            });
                #region ��ȡHIS���������Ϣ
                if (chkBaseInfo.Checked)
                {
                    try
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            string specId = dr["SPECID"].ToString();
                            if (dr["INPATIENT_NO"] == null)
                                continue;
                            string patientNo = dr["INPATIENT_NO"].ToString().Trim().Substring(4);

                            DateTime dt1 = DateTime.Now;
                            DataTable dtTmp = diagManage.GetDiagnoseFromDiagnose(patientNo,noNeedDiag);
                            DateTime dt2 = DateTime.Now;
                            TimeSpan tp = dt2 - dt1;
                            if (dtTmp == null || dtTmp.Rows.Count <= 0)
                            {
                                continue;
                            }
                            dtDiagnose.Merge(dtTmp);
                        }
                        ds.Relations.Add("Diagnose", ds.Tables[0].Columns["סԺ��"], dtDiagnose.Columns["סԺ��"]);
                        neuSpread1_Sheet1.Columns.Count = 9; 
                        neuSpread1_Sheet1.AutoGenerateColumns = false;
                        neuSpread1_Sheet1.DataSource = ds;
                        neuSpread1_Sheet1.DataAutoSizeColumns = false;
                        neuSpread1_Sheet1.DataAutoCellTypes = false;
                        neuSpread1_Sheet1.BindDataColumn(0, "SUBSPEC");
                        neuSpread1_Sheet1.BindDataColumn(7, "SPECID");
                        neuSpread1_Sheet1.BindDataColumn(1, "INPATIENT_NO");
                        neuSpread1_Sheet1.BindDataColumn(2, "NAME");
                        neuSpread1_Sheet1.BindDataColumn(3, "���1");
                        neuSpread1_Sheet1.BindDataColumn(4, "���2");
                        neuSpread1_Sheet1.BindDataColumn(5, "���3");
                        neuSpread1_Sheet1.BindDataColumn(6, "סԺ��");
                        neuSpread1_Sheet1.BindDataColumn(8, "����");
                        neuSpread1_Sheet1.Columns[6].Visible = false;
                        neuSpread1_Sheet1.Columns[7].Visible = false;
                        FarPoint.Win.Spread.SheetView diagnose;
                        for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                        {
                            neuSpread1_Sheet1.Rows[i].BackColor = Color.Azure;
                            diagnose = neuSpread1_Sheet1.GetChildView(i, 0);
                            diagnose.AutoGenerateColumns = false;

                            for (int k = 0; k < diagnose.Columns.Count; k++)
                            {
                                //diagnose.ActiveSkin. = FarPoint.Win.Spread.SheetSkin.
                                diagnose.Columns.Count = 14;
                                //diagnose.Columns[k].Width = 80;
                                diagnose.Columns[k].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                diagnose.Columns[k].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                diagnose.DataAutoSizeColumns = false;
                                diagnose.Columns[5].AllowAutoSort = true;
                                diagnose.Columns[0].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                                diagnose.ColumnHeader.Cells[0, 0].Text = "ѡ��";
                                diagnose.BindDataColumn(5, "סԺ��ˮ��");
                                diagnose.BindDataColumn(2, "¼������");
                                diagnose.BindDataColumn(3, "�������");
                                diagnose.BindDataColumn(4, "�����ICD");
                                diagnose.BindDataColumn(1, "�����");
                                diagnose.BindDataColumn(6, "��̬��ICD");

                                diagnose.BindDataColumn(7, "��̬��");
                                diagnose.BindDataColumn(8, "P_CODE");
                                diagnose.BindDataColumn(9, "T_CODE");
                                diagnose.BindDataColumn(10, "N_CODE");
                                diagnose.BindDataColumn(11, "M_CODE");
                                diagnose.BindDataColumn(12, "סԺ��");
                                diagnose.BindDataColumn(13, "����");
                                diagnose.Columns[12].Visible = false;
                            }
                            diagnose.Columns[5].Width = 100;
                            diagnose.Columns[1].Width = 290;
                            diagnose.Columns[7].Width = 200;
                            diagnose.Columns[13].Width = 95;
                            diagnose.Columns[0].Width = 40;
                            diagnose.Columns[8].Width = 50;
                            diagnose.Columns[9].Width = 50;
                            diagnose.Columns[10].Width = 50;
                            diagnose.Columns[11].Width = 50;
                            int count = diagnose.Rows.Count;
                            float width1 = diagnose.Columns[1].Width;
                            float width4 = diagnose.Columns[5].Width;

                            if (count > 0)
                            {
                                //neuSpread1_Sheet1.Rows[i].Label = count.ToString();
                                neuSpread1_Sheet1.ExpandRow(i, true);
                            }
                            if (count <= 0)
                            {
                                neuSpread1_Sheet1.Rows[i].Visible = false;
                                //neuSpread1_Sheet1.Rows[i].Label = "��";
                                neuSpread1_Sheet1.ExpandRow(i, false);
                            }
                        }
                    }
                    catch
                    { }
                }
                else
                {
                    neuSpread1_Sheet1.Columns.Count = 9;
                    neuSpread1_Sheet1.AutoGenerateColumns = false;
                    neuSpread1_Sheet1.DataSource = ds;
                    neuSpread1_Sheet1.DataAutoSizeColumns = false;
                    neuSpread1_Sheet1.DataAutoCellTypes = false;
                    neuSpread1_Sheet1.BindDataColumn(0, "SUBSPEC");
                    neuSpread1_Sheet1.BindDataColumn(7, "SPECID");
                    neuSpread1_Sheet1.BindDataColumn(1, "INPATIENT_NO");
                    neuSpread1_Sheet1.BindDataColumn(2, "NAME");
                    neuSpread1_Sheet1.BindDataColumn(3, "���1");
                    neuSpread1_Sheet1.BindDataColumn(4, "���2");
                    neuSpread1_Sheet1.BindDataColumn(5, "���3");
                    neuSpread1_Sheet1.BindDataColumn(6, "סԺ��");
                    neuSpread1_Sheet1.BindDataColumn(8, "����");
                    neuSpread1_Sheet1.Columns[6].Visible = false;
                    neuSpread1_Sheet1.Columns[7].Visible = false;
                }
                #endregion
            }
            float col4 = neuSpread1_Sheet1.RowHeader.Columns[0].Width;
            neuSpread1_Sheet1.RowHeader.Columns[0].Width = 50;
            col4 = neuSpread1_Sheet1.RowHeader.Columns[0].Width;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            ucDiagQuery.DataFromGetDiag = false;
        }

        private void FilterDataTable(ref DataTable dt)
        {
            Dictionary<string, string> dicTmp = new Dictionary<string, string>();
            for (int i = dt.Rows.Count -1 ; i >=0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (!dicTmp.ContainsKey(dr["סԺ��"].ToString()))
                {
                    dicTmp.Add(dr["סԺ��"].ToString(),"");
                }
                else
                {
                    dt.Rows.RemoveAt(i);
                } 
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

        private void ReadRecord(DiaDetail diagInfo, int rowCount,int diaCont)
        {
            string diag = diagInfo.Icd + "," + diagInfo.IcdName;
            diag += "/" + diagInfo.Mod + "," + diagInfo.ModName + "/";
            diag += diagInfo.P_Code + "," + diagInfo.T_Code + "," + diagInfo.N_Code + "," + diagInfo.M_Code;
            neuSpread1_Sheet1.Cells[rowCount, (3 + diaCont)].Text = diag;
            neuSpread1_Sheet1.Cells[rowCount, (3 + diaCont)].Tag = diagInfo;
            neuSpread1_Sheet1.Cells[rowCount, 8].Text = diagInfo.User03;
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
                //��ȡ��ϵļ�¼��
                int diaCount = 0;
                for (int k = 0; k < diagnose.Rows.Count; k++)
                {
                    if (!diagnose.Rows[k].Visible)
                    {
                        continue;
                    }
                    if (diagnose.Cells[k, 0].Value == null || diagnose.Cells[k, 0].Value.ToString() == "0" || diagnose.Cells[k, 0].Value.ToString().ToLower() == "false")
                    {
                        continue;
                    }
                    DiaDetail diagInfo = new DiaDetail();
                    diagInfo.Icd = diagnose.Cells[k, 4].Text;
                    diagInfo.IcdName = diagnose.Cells[k, 1].Text;
                    diagInfo.Mod = diagnose.Cells[k, 6].Text;                   
                    diagInfo.ModName = diagnose.Cells[k, 7].Text;
                    diagInfo.P_Code = diagnose.Cells[k, 8].Text;
                    diagInfo.T_Code = diagnose.Cells[k, 9].Text;
                    diagInfo.N_Code = diagnose.Cells[k, 10].Text;
                    diagInfo.M_Code = diagnose.Cells[k, 11].Text;
                    diagInfo.User03 = diagnose.Cells[k, 13].Text;
                    ReadRecord(diagInfo, i,diaCount);
                    diaCount++;
                    if (diaCount == 3)
                    {
                        break;
                    }                    
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
            if (!readBaseInfo)
            {
                return;
            }
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

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("��ȡ����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z���, true, false, null);
            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "��ȡ����":
                    try
                    {
                        IteratorSheet();
                    }
                    catch
                    { }
                    break;              
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
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

        public override int Save(object sender, object neuObject)
        {
            if (isInBase)
            {
                DialogResult dr = MessageBox.Show("ȷ������������Ϣ��", "��ϸ���", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return 0;
                }
            }
            DialogResult dResult = MessageBox.Show("��Ϣ��ʵ���?", "���¼��", MessageBoxButtons.YesNo);
            if (dResult == DialogResult.No)
            {
                return 0;
            }

            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {

                SpecSourceManage sourceManage = new SpecSourceManage();
                try
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    sourceManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    diagManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //�����һ�����û��¼�룬����Ϊû��¼�����
                    if (neuSpread1_Sheet1.Cells[i, 3].Tag == null || neuSpread1_Sheet1.Cells[i, 3].Text.Trim() == "")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        continue;
                    }
                    string specId = neuSpread1_Sheet1.Cells[i, 7].Text.Trim();
                    //string specId = row["SPECID"].ToString();
                    SpecSource s1 = sourceManage.GetSource(specId, "");
                    string operationNo = s1.OperApplyId;
                    if (operationNo.Length >= 5 && operationNo.Length < 10)
                    {
                        SpecSource s2 = sourceManage.GetSendDocInfoInHis(operationNo);
                        if (s2 != null)
                        {
                            s1.MediDoc = s2.MediDoc.Clone();
                            s1.DeptNo = s2.DeptNo;
                            s1.SendDoctor.ID = s2.MediDoc.MainDoc.ID;
                        }
                        s1.IsInBase = '1';
                        if (sourceManage.UpdateSpecSource(s1) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                            //MessageBox.Show("����ʧ��");
                            //return -1;
                        }
                    }
                    else
                    {
                        if (sourceManage.UpdateInBase(specId) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                            //MessageBox.Show("����ʧ��");
                            //return -1;
                        }
                    }
                    SpecDiagnose specDia = new SpecDiagnose();
                    specDia.SpecSource.SpecId = Convert.ToInt32(specId);
                    if (neuSpread1_Sheet1.Cells[i, 3].Tag != null)
                    {
                        DiaDetail diag = neuSpread1_Sheet1.Cells[i, 3].Tag as DiaDetail;
                        if (diag != null)
                        {
                            specDia.Diag = diag;
                        }
                    }

                    if (neuSpread1_Sheet1.Cells[i, 4].Tag != null)
                    {
                        if (neuSpread1_Sheet1.Cells[i, 4].Text.Trim() != "")
                        {
                            DiaDetail diag1 = neuSpread1_Sheet1.Cells[i, 4].Tag as DiaDetail;
                            if (diag1 != null)
                            {
                                specDia.Diag1 = diag1;
                            }
                        }
                    }

                    if (neuSpread1_Sheet1.Cells[i, 5].Tag != null)
                    {
                        if (neuSpread1_Sheet1.Cells[i, 5].Text.Trim() != "")
                        {
                            DiaDetail diag2 = neuSpread1_Sheet1.Cells[i, 5].Tag as DiaDetail;
                            if (diag2 != null)
                            {
                                specDia.Diag2 = diag2;
                            }
                        }
                    }

                    //����
                    if (!string.IsNullOrEmpty(neuSpread1_Sheet1.Cells[i, 8].Text.Trim()))
                    {
                        specDia.Ext3 = neuSpread1_Sheet1.Cells[i, 8].Text.Trim();
                    }

                    DataTable dt = diagManage.GetDiagnoseByInPatientNo(s1.InPatientNo);
                    string remark = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        string tmpRemark = "[";
                        if (dr["ICD_CODE"] != null)
                        {
                            tmpRemark += dr["ICD_CODE"].ToString();
                        }

                        tmpRemark += ",";

                        if (dr["DIAG_NAME"] != null)
                        {
                            tmpRemark += dr["DIAG_NAME"].ToString();
                        }

                        tmpRemark += "/";

                        if (dr["SECOND_ICD"] != null)
                        {
                            tmpRemark += dr["SECOND_ICD"].ToString();
                        }
                        tmpRemark += ",";

                        if (dr["REMARK"] != null)
                        {
                            tmpRemark += dr["REMARK"].ToString();
                        }

                        tmpRemark += "/";

                        if (dr["P_CODE"] != null)
                        {
                            tmpRemark += dr["P_CODE"].ToString();
                        }

                        tmpRemark += ",";

                        if (dr["T_CODE"] != null)
                        {
                            tmpRemark += dr["T_CODE"].ToString();
                        }

                        tmpRemark += ",";

                        if (dr["N_CODE"] != null)
                        {
                            tmpRemark += dr["N_CODE"].ToString();
                        }

                        tmpRemark += ",";

                        if (dr["M_CODE"] != null)
                        {
                            tmpRemark += dr["N_CODE"].ToString();
                        }
                        tmpRemark += "]";
                        remark += tmpRemark;
                    }

                    specDia.DiagRemark = remark;

                    DataTable dtOper = new DataTable();
                    dtOper = diagManage.GetOperInfoByInPatNo(s1.InPatientNo);

                    string operRemark = "";
                    foreach (DataRow dr in dtOper.Rows)
                    {
                        string rmk = "[";
                        if (dr["OPERATION_CODE"] != null)
                        {
                            rmk += dr["OPERATION_CODE"];
                        }
                        rmk += ",";
                        if (dr["OPERATION_CNNAME"] != null)
                        {
                            rmk += dr["OPERATION_CNNAME"].ToString();
                        }
                        rmk += "]";
                        operRemark += rmk;
                    }

                    specDia.Diagnose_Oper_Flag = operRemark;
                    specDia.OperId = loginPerson.ID;
                    specDia.OperName = loginPerson.Name;

                    if (!isInBase)
                    {
                        string sequence = "";
                        diagManage.GetNextSequence(ref sequence);
                        specDia.BaseID = Convert.ToInt32(sequence);
                        if (diagManage.InsertDiagnose(specDia) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                            //MessageBox.Show("¼��ʧ��");
                            //return -1;
                        }
                    }
                    if (isInBase)
                    {
                        if (diagManage.UpdateDiagnose(specDia) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            continue;
                            //MessageBox.Show("����ʧ��");
                            //return -1;
                        }
                    }
                    //neuSpread1_Sheet1.BindDataColumn(0, "SPECID");
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    continue;
                    //MessageBox.Show("����ʧ��");
                }
                //finally
                //{
                //    MessageBox.Show("�����ɹ�!");
                //}
            }
            chkBaseInfo.Checked = false;
            MessageBox.Show("�����ɹ�!");
            //this.Query();
            return base.Save(sender, neuObject);
        }

        private void chkDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (!readBaseInfo)
            {
                return;
            }
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                FarPoint.Win.Spread.SheetView diagnose;
                diagnose = neuSpread1_Sheet1.GetChildView(i, 0);
                if (diagnose == null || diagnose.Rows.Count <= 0)
                {
                    continue;
                }
                //��ȡ��ϵļ�¼��

                int diagCount = 0;
                for (int k = 0; k < diagnose.Rows.Count; k++)
                {
                    if (!chkDefault.Checked)
                    {
                        diagnose.Cells[k, 0].Value = (object)0;
                        continue;
                    }
                    if (!diagnose.Rows[k].Visible)
                    {
                        continue;
                    }
                    diagnose.Cells[k, 0].Value = (object)1;
                    diagCount++;
                    if (diagCount == 3)
                        break;
                }
            }
        }

        private void ucGetDiag_Load(object sender, EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            ucDiagQuery.BtnVisible = true;
            tp2.Controls.Add(ucDiagQuery);
            ucDiagQuery.Dock = DockStyle.Fill;
            GetNoNeedDiag();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                chkBaseInfo.Checked = false;                
                dtpEnd.Enabled = false;
                dtpStart.Enabled = false;
            }
            if (!chkAll.Checked)
            {
                dtpStart.Enabled = true;
                dtpEnd.Enabled = true;
                //chkBaseInfo.Checked = true;
            }
        }

        private void chkInBase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInBase.Checked)
            {
                chkBaseInfo.Checked = false;
                chkAll.Checked = false;
            }
            if (!chkInBase.Checked)
            {
                chkBaseInfo.Checked = true;                
            }
        }

        private void tbResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbResult.SelectedIndex == 0)
            {
                if (ucDiagQuery.DataFromGetDiag)
                {
                    this.dtpStart.Value = ucDiagQuery.StartTime;
                    this.dtpEnd.Value = ucDiagQuery.EndTime;
                    this.Query();
                }
            }
        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            try
            {
                IteratorSheet();
            }
            catch
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save(sender, null);
        }
    }
}
