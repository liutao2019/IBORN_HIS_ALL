using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.DataImport
{
    public partial class ucAutoDiag : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private DiagnoseManage diagManage;
        private FS.HISFC.Models.Base.Employee loginPerson;
      
        public ucAutoDiag()
        {
            InitializeComponent();
        }

        private void SaveDiag()
        {
            diagManage = new DiagnoseManage();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            string sqlDiag = @"select d.INPATIENT_NO 住院号,nvl(diag_name,'') 主诊断, d.INPATIENT_NO 住院流水号,
                                ( case OPER_TYPE when '1' then '医生站' when '2' then '病案库' end) 录入类型,
                                ( case diag_kind when '1' then '主诊断' when '2' then '其他诊断' end) 诊断类型,
                                nvl(icd_code,'') 主诊断ICD,nvl(SECOND_ICD,'') 形态码ICD, nvl(remark,'') 形态码,
                                nvl(d.p_code,''), nvl(d.T_code,''), nvl(d.N_code,''), nvl(d.M_code,'')
                                from MET_CAS_DIAGNOSE d
                                where (diag_kind='1' or diag_kind='2') and d.OPER_TYPE = '2'
                                and icd_code not in 
                                (select code from com_dictionary where  type ='NoNeedDiagForSpec') 
                                and  d.INPATIENT_NO = '{0}'
                                order by d.INPATIENT_NO desc, 录入类型 asc , 诊断类型 desc ";

            string sqlNotinBase = @"select distinct s.specid, s.INPATIENT_NO  
                                    from   SPEC_SOURCE s  
                                    where  s.ishis= 'O' and s.isinbase = '0'  and s.INPATIENT_NO<>''";

            DataSet dsSource = new DataSet();
            diagManage.ExecQuery(sqlNotinBase, ref dsSource);

            foreach (DataRow dr in dsSource.Tables[0].Rows)
            {
                string tmpSql = string.Format(sqlDiag, dr["INPATIENT_NO"].ToString());
                DataSet dsDiag = new DataSet();
                diagManage.ExecQuery(tmpSql, ref dsDiag);
                SpecSourceManage sourceManage = new SpecSourceManage();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                try
                {
                    sourceManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    diagManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //如果第一个诊断没有录入，就认为没有录入诊断

                    string specId = dr["SPECID"].ToString();
                
                    if (sourceManage.UpdateInBase(specId) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        continue;
                        //MessageBox.Show("更新失败");
                        //return -1;
                    }

                    SpecDiagnose specDia = new SpecDiagnose();
                    specDia.SpecSource.SpecId = Convert.ToInt32(specId);
                    DiaDetail diag = new DiaDetail();
                    try
                    {
                        // DiaDetail;
                        diag.Icd = dsDiag.Tables[0].Rows[0][5].ToString();// diagnose.Cells[k, 4].Text;
                        diag.IcdName = dsDiag.Tables[0].Rows[0][1].ToString();
                        diag.Mod = dsDiag.Tables[0].Rows[0][6].ToString();
                        diag.ModName = dsDiag.Tables[0].Rows[0][7].ToString();
                        diag.P_Code = dsDiag.Tables[0].Rows[0][8].ToString();
                        diag.T_Code = dsDiag.Tables[0].Rows[0][9].ToString();
                        diag.N_Code = dsDiag.Tables[0].Rows[0][10].ToString();
                        diag.M_Code = dsDiag.Tables[0].Rows[0][11].ToString();
                    }
                    catch
                    {
 
                    }

                    specDia.Diag = diag;

                    DiaDetail diag1 = new DiaDetail();
                    try
                    {
                        // DiaDetail;
                        diag1.Icd = dsDiag.Tables[0].Rows[1][5].ToString();// diagnose.Cells[k, 4].Text;
                        diag1.IcdName = dsDiag.Tables[0].Rows[1][1].ToString();
                        diag1.Mod = dsDiag.Tables[0].Rows[1][6].ToString();
                        diag1.ModName = dsDiag.Tables[0].Rows[1][7].ToString();
                        diag1.P_Code = dsDiag.Tables[0].Rows[1][8].ToString();
                        diag1.T_Code = dsDiag.Tables[0].Rows[1][9].ToString();
                        diag1.N_Code = dsDiag.Tables[0].Rows[1][10].ToString();
                        diag1.M_Code = dsDiag.Tables[0].Rows[1][11].ToString();
                    }
                    catch
                    {

                    }
                    specDia.Diag1 = diag1;


                    DiaDetail diag2 = new DiaDetail();
                    try
                    {
                        // DiaDetail;
                        diag2.Icd = dsDiag.Tables[0].Rows[2][5].ToString();// diagnose.Cells[k, 4].Text;
                        diag2.IcdName = dsDiag.Tables[0].Rows[2][1].ToString();
                        diag2.Mod = dsDiag.Tables[0].Rows[2][6].ToString();
                        diag2.ModName = dsDiag.Tables[0].Rows[2][7].ToString();
                        diag2.P_Code = dsDiag.Tables[0].Rows[2][8].ToString();
                        diag2.T_Code = dsDiag.Tables[0].Rows[2][9].ToString();
                        diag2.N_Code = dsDiag.Tables[0].Rows[2][10].ToString();
                        diag2.M_Code = dsDiag.Tables[0].Rows[2][11].ToString();
                    }
                    catch
                    {

                    }
                    specDia.Diag2 = diag2;

                    DataTable dtOper = new DataTable();
                    dtOper = diagManage.GetOperInfoByInPatNo(dr["INPATIENT_NO"].ToString());

                    string operRemark = "";
                    try
                    {
                        foreach (DataRow dtr in dtOper.Rows)
                        {
                            string rmk = "[";
                            if (dtr["OPERATION_CODE"] != null)
                            {
                                rmk += dtr["OPERATION_CODE"];
                            }
                            rmk += ",";
                            if (dtr["OPERATION_CNNAME"] != null)
                            {
                                rmk += dtr["OPERATION_CNNAME"].ToString();
                            }
                            rmk += "]";
                            operRemark += rmk;
                        }
                    }
                    catch
                    { }
                    specDia.Diagnose_Oper_Flag = operRemark;
                    specDia.OperId = loginPerson.ID;
                    specDia.OperName = loginPerson.Name;

                   
                    string sequence = "";
                    diagManage.GetNextSequence(ref sequence);
                    specDia.BaseID = Convert.ToInt32(sequence);
                    if (diagManage.InsertDiagnose(specDia) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        continue;
                        //MessageBox.Show("录入失败");
                        //return -1;
                    }
                    
                    //neuSpread1_Sheet1.BindDataColumn(0, "SPECID");
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    continue;
                    //MessageBox.Show("操作失败");
                }
                //finally
                //{
                //    MessageBox.Show("操作成功!");
                //}

            }

            
        }

        public override int Save(object sender, object neuObject)
        {
            try
            {
                this.SaveDiag();
            }
            catch
            { }
            return base.Save(sender, neuObject);
        }
    }
}
