using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.DataImport
{
    public partial class frmAutoDiag : Form
    {
        private DisTypeManage disMgr = new DisTypeManage();
        private string sql = "";
        private string diagSql = "";
        private ArrayList arrSource = new ArrayList();
        private SpecSourceManage sourceMgr = new SpecSourceManage();
        private FS.HISFC.Models.Base.Employee loginPerson = new FS.HISFC.Models.Base.Employee();
        public frmAutoDiag()
        {
            InitializeComponent();
        }
        
        
        /// <summary>
        /// 绑定病种类型
        /// </summary>
        private void BldDisTypeBinding()
        {
            Dictionary<int, string> dicDisType = disMgr.GetBldDisType();
            if (dicDisType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDisType.DisplayMember = "Value";
                cmbDisType.ValueMember = "Key";
                cmbDisType.DataSource = bs;
            }
            cmbDisType.Text = "";
        }

        private void RdbChange()
        {
            if (rbtBld.Checked)
            {
                BldDisTypeBinding();
                return;
            }
            else
            {
                Dictionary<int, string> dicDisType = disMgr.GetOrgDisType();
                if (dicDisType.Count > 0)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dicDisType;
                    cmbDisType.DisplayMember = "Value";
                    cmbDisType.ValueMember = "Key";
                    cmbDisType.DataSource = bs;
                }
                cmbDisType.Text = "";
            }
        }

        private void frmAutoDiag_Load(object sender, EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            sql = @"select substr(d.INPATIENT_NO,1,4) 次数, substr(d.INPATIENT_NO,5,10) 住院号,nvl(diag_name,'') 主诊断, d.INPATIENT_NO 住院流水号,
                    ( case OPER_TYPE when '1' then '医生站' when '2' then '病案库' end) 录入类型,
                     ( case diag_kind when '1' then '主诊断' when '2' then '其他诊断' end) 诊断类型,
                     nvl(icd_code,'') 主诊断ICD,nvl(SECOND_ICD,'') 形态码ICD, nvl(remark,'') 形态码,
                     nvl(d.p_code,'')p_code, nvl(d.T_code,'')T_code, nvl(d.N_code,'')N_code, nvl(d.M_code,'')M_code
                     from MET_CAS_DIAGNOSE d
                     where substr(d.INPATIENT_NO,5,10) = '{0}' and (diag_kind='1' or diag_kind='2') and d.OPER_TYPE = '2' 
                     and icd_code not in (select CODE from COM_DICTIONARY where TYPE='NoNeedDiagForSpec')
                     order by d.INPATIENT_NO desc, 录入类型 asc , 诊断类型 desc";
            diagSql = @" select * from SPEC_SOURCE s where s.ISINBASE='0' and s.SENDDATE between to_date('{0}','yyyy-mm-dd hh24:mi:ss') and to_date('{1}','yyyy-mm-dd hh24:mi:ss') ";

            BldDisTypeBinding();
            #region
            //if (isInBase)
            //{
            //    DialogResult dr = MessageBox.Show("确定更新以下信息？", "诊断更新", MessageBoxButtons.YesNo);
            //    if (dr == DialogResult.No)
            //    {
            //        return 0;
            //    }
            //}
            //DialogResult dResult = MessageBox.Show("信息核实完毕?", "诊断录入", MessageBoxButtons.YesNo);
            //if (dResult == DialogResult.No)
            //{
            //    return 0;
            //}

            //for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            //{

            //    SpecSourceManage sourceManage = new SpecSourceManage();
            //    FS.NFC.Management.Transaction trans = new FS.NFC.Management.Transaction(FS.NFC.Management.Connection.Instance);
            //    try
            //    {
            //        trans.BeginTransaction();
            //        sourceManage.SetTrans(trans.Trans);
            //        diagManage.SetTrans(trans.Trans);
            //        //如果第一个诊断没有录入，就认为没有录入诊断
            //        if (neuSpread1_Sheet1.Cells[i, 3].Tag == null || neuSpread1_Sheet1.Cells[i, 3].Text.Trim() == "")
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            continue;
            //        }
            //        string specId = neuSpread1_Sheet1.Cells[i, 7].Text.Trim();
            //        //string specId = row["SPECID"].ToString();
            //        SpecSource s1 = sourceManage.GetSource(specId, "");
            //        string operationNo = s1.OperApplyId;
            //        if (operationNo.Length >= 5 && operationNo.Length < 10)
            //        {
            //            SpecSource s2 = sourceManage.GetSendDocInfoInHis(operationNo);
            //            if (s2 != null)
            //            {
            //                s1.MediDoc = s2.MediDoc.Clone();
            //                s1.DeptNo = s2.DeptNo;
            //                s1.SendDoctor.ID = s2.MediDoc.MainDoc.ID;
            //            }
            //            s1.IsInBase = '1';
            //            if (sourceManage.UpdateSpecSource(s1) <= 0)
            //            {
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                continue;
            //                //MessageBox.Show("更新失败");
            //                //return -1;
            //            }
            //        }
            //        else
            //        {
            //            if (sourceManage.UpdateInBase(specId) <= 0)
            //            {
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                continue;
            //                //MessageBox.Show("更新失败");
            //                //return -1;
            //            }
            //        }
            //        SpecDiagnose specDia = new SpecDiagnose();
            //        specDia.SpecSource.SpecId = Convert.ToInt32(specId);
            //        if (neuSpread1_Sheet1.Cells[i, 3].Tag != null)
            //        {
            //            DiaDetail diag = neuSpread1_Sheet1.Cells[i, 3].Tag as DiaDetail;
            //            if (diag != null)
            //            {
            //                specDia.Diag = diag;
            //            }
            //        }

            //        if (neuSpread1_Sheet1.Cells[i, 4].Tag != null)
            //        {
            //            if (neuSpread1_Sheet1.Cells[i, 4].Text.Trim() != "")
            //            {
            //                DiaDetail diag1 = neuSpread1_Sheet1.Cells[i, 4].Tag as DiaDetail;
            //                if (diag1 != null)
            //                {
            //                    specDia.Diag1 = diag1;
            //                }
            //            }
            //        }

            //        if (neuSpread1_Sheet1.Cells[i, 5].Tag != null)
            //        {
            //            if (neuSpread1_Sheet1.Cells[i, 5].Text.Trim() != "")
            //            {
            //                DiaDetail diag2 = neuSpread1_Sheet1.Cells[i, 5].Tag as DiaDetail;
            //                if (diag2 != null)
            //                {
            //                    specDia.Diag2 = diag2;
            //                }
            //            }
            //        }

            //        DataTable dt = diagManage.GetDiagnoseByInPatientNo(s1.InPatientNo);
            //        string remark = "";
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            string tmpRemark = "[";
            //            if (dr["ICD_CODE"] != null)
            //            {
            //                tmpRemark += dr["ICD_CODE"].ToString();
            //            }

            //            tmpRemark += ",";

            //            if (dr["DIAG_NAME"] != null)
            //            {
            //                tmpRemark += dr["DIAG_NAME"].ToString();
            //            }

            //            tmpRemark += "/";

            //            if (dr["SECOND_ICD"] != null)
            //            {
            //                tmpRemark += dr["SECOND_ICD"].ToString();
            //            }
            //            tmpRemark += ",";

            //            if (dr["REMARK"] != null)
            //            {
            //                tmpRemark += dr["REMARK"].ToString();
            //            }

            //            tmpRemark += "/";

            //            if (dr["P_CODE"] != null)
            //            {
            //                tmpRemark += dr["P_CODE"].ToString();
            //            }

            //            tmpRemark += ",";

            //            if (dr["T_CODE"] != null)
            //            {
            //                tmpRemark += dr["T_CODE"].ToString();
            //            }

            //            tmpRemark += ",";

            //            if (dr["N_CODE"] != null)
            //            {
            //                tmpRemark += dr["N_CODE"].ToString();
            //            }

            //            tmpRemark += ",";

            //            if (dr["M_CODE"] != null)
            //            {
            //                tmpRemark += dr["N_CODE"].ToString();
            //            }
            //            tmpRemark += "]";
            //            remark += tmpRemark;
            //        }

            //        specDia.DiagRemark = remark;

            //        DataTable dtOper = new DataTable();
            //        dtOper = diagManage.GetOperInfoByInPatNo(s1.InPatientNo);

            //        string operRemark = "";
            //        foreach (DataRow dr in dtOper.Rows)
            //        {
            //            string rmk = "[";
            //            if (dr["OPERATION_CODE"] != null)
            //            {
            //                rmk += dr["OPERATION_CODE"];
            //            }
            //            rmk += ",";
            //            if (dr["OPERATION_CNNAME"] != null)
            //            {
            //                rmk += dr["OPERATION_CNNAME"].ToString();
            //            }
            //            rmk += "]";
            //            operRemark += rmk;
            //        }

            //        specDia.Diagnose_Oper_Flag = operRemark;
            //        specDia.OperId = loginPerson.ID;
            //        specDia.OperName = loginPerson.Name;

            //        if (!isInBase)
            //        {
            //            string sequence = "";
            //            diagManage.GetNextSequence(ref sequence);
            //            specDia.BaseID = Convert.ToInt32(sequence);
            //            if (diagManage.InsertDiagnose(specDia) <= 0)
            //            {
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                //FS.FrameWork.Management.PublicTrans.RollBack();
            //                continue;
            //                //MessageBox.Show("录入失败");
            //                //return -1;
            //            }
            //        }
            //        if (isInBase)
            //        {
            //            if (diagManage.UpdateDiagnose(specDia) <= 0)
            //            {
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                continue;
            //                //MessageBox.Show("更新失败");
            //                //return -1;
            //            }
            //        }
            //        //neuSpread1_Sheet1.BindDataColumn(0, "SPECID");
            //        FS.FrameWork.Management.PublicTrans.Commit();
            //    }

            //    catch
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        continue;
            //        //MessageBox.Show("操作失败");
            //    }
            //    //finally
            //    //{
            //    //    MessageBox.Show("操作成功!");
            //    //}
            //}
            //chkBaseInfo.Checked = false;
            #endregion
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                arrSource = new ArrayList();
                string tmpSql = string.Format(diagSql, new string[] { dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1.0).ToString() });
                if (cmbDisType.Text != "")
                {
                    try
                    {
                        tmpSql += " and DISEASETYPEID = {0}";
                        tmpSql = string.Format(tmpSql, cmbDisType.SelectedValue.ToString());
                    }
                    catch
                    { }
                }
                arrSource = sourceMgr.GetSpecSource(tmpSql);

                if (arrSource == null || arrSource.Count <= 0)
                {
                    lblCnt.Text = "记录数：0";
                    return;
                }

                lblCnt.Text = "记录数：" + arrSource.Count.ToString();
            }
            catch
            {
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            #region


            DiagnoseManage diagManage = new DiagnoseManage();
            foreach (SpecSource s in arrSource)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                diagManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                sourceMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //string mainIcd = "";
                //string mainDiag = "";
                //string icd1 = "";
                //string diag1 = "";
                //string icd2 = "";
                //string diag2 = "";
                DiaDetail dia = new DiaDetail();
                DiaDetail dia1 = new DiaDetail();
                DiaDetail dia2 = new DiaDetail();
                string diagcomment = "[自动关联]";
                try
                {
                    string tmpSql = string.Format(sql, s.InPatientNo.Substring(4, 10));
                    DataSet ds = new DataSet();
                    diagManage.ExecQuery(tmpSql, ref ds);
//select substr(d.INPATIENT_NO,1,4) 次数, substr(d.INPATIENT_NO,5,10) 住院号,diag_name 主诊断, d.INPATIENT_NO 住院流水号,
//                    ( case OPER_TYPE when '1' then '医生站' when '2' then '病案库' end) 录入类型,
//                     ( case diag_kind when '1' then '主诊断' when '2' then '其他诊断' end) 诊断类型,
//                     icd_code 主诊断ICD,SECOND_ICD 形态码ICD, remark 形态码,
//                     d.p_code, d.T_code, d.N_code, d.M_code
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["次数"].ToString() == s.InPatientNo.Substring(0, 4))
                        {

                            dia.Icd = dr["主诊断ICD"] == null ? "" : dr["主诊断ICD"].ToString();
                            dia.IcdName = dr["主诊断"]== null? "" : dr["主诊断"].ToString();
                            dia.P_Code = dr["p_code"]== null? "" : dr["p_code"].ToString();
                            dia.T_Code = dr["T_code"]== null? "" : dr["T_code"].ToString();
                            dia.N_Code = dr["N_code"]== null? "" : dr["N_code"].ToString();
                            dia.M_Code = dr["M_code"] == null ? "" : dr["M_code"].ToString();
                        }
                        if (dia.Icd.Trim() == "" || dia.IcdName.Trim() == "")
                        {
                            dia.Icd = dr["主诊断ICD"] == null ? "" : dr["主诊断ICD"].ToString();
                            dia.IcdName = dr["主诊断"] == null ? "" : dr["主诊断"].ToString();
                            dia.P_Code = dr["p_code"] == null ? "" : dr["p_code"].ToString();
                            dia.T_Code = dr["T_code"] == null ? "" : dr["T_code"].ToString();
                            dia.N_Code = dr["N_code"] == null ? "" : dr["N_code"].ToString();
                            dia.M_Code = dr["M_code"] == null ? "" : dr["M_code"].ToString();
                        }

                        dia1.Icd = dr["主诊断ICD"] == null ? "" : dr["主诊断ICD"].ToString();
                        dia1.IcdName = dr["主诊断"] == null ? "" : dr["主诊断"].ToString();
                        dia1.P_Code = dr["p_code"] == null ? "" : dr["p_code"].ToString();
                        dia1.T_Code = dr["T_code"] == null ? "" : dr["T_code"].ToString();
                        dia1.N_Code = dr["N_code"] == null ? "" : dr["N_code"].ToString();
                        dia1.M_Code = dr["M_code"] == null ? "" : dr["M_code"].ToString();

                        dia2.Icd = dr["主诊断ICD"] == null ? "" : dr["主诊断ICD"].ToString();
                        dia2.IcdName = dr["主诊断"] == null ? "" : dr["主诊断"].ToString();
                        dia2.P_Code = dr["p_code"] == null ? "" : dr["p_code"].ToString();
                        dia2.T_Code = dr["T_code"] == null ? "" : dr["T_code"].ToString();
                        dia2.N_Code = dr["N_code"] == null ? "" : dr["N_code"].ToString();
                        dia2.M_Code = dr["M_code"] == null ? "" : dr["M_code"].ToString();

                        diagcomment += dr["主诊断ICD"].ToString() + "," + dr["主诊断ICD"].ToString() + "," + dr["p_code"].ToString() + ","
                                     + dr["T_code"].ToString() + "," + dr["N_code"].ToString() + "," + dr["M_code"].ToString() + ",";
                    }
                    s.IsInBase = '1';

                    if (sourceMgr.UpdateSpecSource(s) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        continue;

                    }

                    SpecDiagnose specDia = new SpecDiagnose();
                    specDia.SpecSource.SpecId = s.SpecId;
                    specDia.Diag = dia;
                    //specDia.Diag.IcdName = 
                    specDia.Diag1 = dia1;
                    specDia.Diag2 = dia2;
                    specDia.Diagnose_Oper_Flag = diagcomment;
                    specDia.OperId = loginPerson.ID;
                    specDia.OperName = loginPerson.Name;

                        string sequence = "";
                        diagManage.GetNextSequence(ref sequence);
                        specDia.BaseID = Convert.ToInt32(sequence);
                        if (diagManage.InsertDiagnose(specDia) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();                           
                            continue;
                           
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                     
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    continue;
                }
            }
            this.btnQuery_Click(null, null);
            //        specDia.OperId = loginPerson.ID;
            //        specDia.OperName = loginPerson.Name;

            //        if (!isInBase)
            //        {
            //            string sequence = "";
            //            diagManage.GetNextSequence(ref sequence);
            //            specDia.BaseID = Convert.ToInt32(sequence);
            //            if (diagManage.InsertDiagnose(specDia) <= 0)
            //            {
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                //FS.FrameWork.Management.PublicTrans.RollBack();
            //                continue;
            //                //MessageBox.Show("录入失败");
            //                //return -1;
            //            }
            //        }
            //        if (isInBase)
            //        {
            //            if (diagManage.UpdateDiagnose(specDia) <= 0)
            //            {
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                continue;
            //                //MessageBox.Show("更新失败");
            //                //return -1;
            //            }
            //        }
            //        //neuSpread1_Sheet1.BindDataColumn(0, "SPECID");
            //        FS.FrameWork.Management.PublicTrans.Commit();
            //    }

            //    catch
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        continue;
            //        //MessageBox.Show("操作失败");
            //    }
            //    //finally
            //    //{
            //    //    MessageBox.Show("操作成功!");
            //    //}
 
            #endregion
        }

    }
}