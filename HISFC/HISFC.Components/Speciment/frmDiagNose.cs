using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public partial class frmDiagNose : Form
    {
        //手术申请单管理对象
        private OperApplyManage operApplyManage;
        //诊断信息管理对象
        private BaseManage baseManage;
        //标本库病人管理对象
        private PatientManage patientManage;
        //标本源管理对象
        private SpecSourceManage specSourceManage;
        //查询结果
        private DataSet ds;
        public frmDiagNose()
        {
            operApplyManage = new OperApplyManage();
            baseManage = new BaseManage();
            patientManage = new PatientManage();
            specSourceManage = new SpecSourceManage();
            InitializeComponent();
        }

        /// <summary>
        /// 显示查询结果
        /// </summary>
        private void Query()
        {
            neuSpread1_Sheet1.DataSource = null;
            ds = new DataSet();
            string start = "";
            start = dtpStart.Value.Date.ToString();
            string end = "";
            end =  dtpEnd.Value.Date.ToString();
            ds = baseManage.GetNotInBaseByTime(start, end);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return;
            }
            neuSpread1_Sheet1.AutoGenerateColumns = false;
            neuSpread1_Sheet1.DataSource = ds.Tables[0];
            neuSpread1_Sheet1.BindDataColumn(0, "SPECID");
            neuSpread1_Sheet1.BindDataColumn(1, "HISBARCODE");
            neuSpread1_Sheet1.BindDataColumn(2, "SENDDATE");
            neuSpread1_Sheet1.BindDataColumn(3, "MAIN_DIAGICD");
            neuSpread1_Sheet1.BindDataColumn(4, "MAIN_DIAGICDNAME");
            neuSpread1_Sheet1.BindDataColumn(5, "MAIN_DIAGICD");
            neuSpread1_Sheet1.BindDataColumn(6, "MAIN_DIAGICDNAME");
            neuSpread1_Sheet1.BindDataColumn(7, "CLINIC_DIAGICD");
            neuSpread1_Sheet1.BindDataColumn(8, "CLINIC_DIAGICDNAME");
            neuSpread1_Sheet1.BindDataColumn(9, "INHOS_DIAGICD");
            neuSpread1_Sheet1.BindDataColumn(10, "INHOS_DIAGICDNAME");
        }

        private void Save()
        {                   
            //1.更新标本信息（主刀医生）
            //2.更新诊断信息
            if (ds == null || ds.Tables.Count <= 0)
            {
                MessageBox.Show("没有需要更新的记录", "提示");
                return;
            }
            DataTable dtTmp = ds.Tables[0];
            if (dtTmp.Rows.Count <= 0)
            {
                MessageBox.Show("没有需要更新的记录", "提示");
                return;
            }

            //手术安排管理对象，获取做手术的医生信息
            
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            specSourceManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            baseManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            patientManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (DataRow row in dtTmp.Rows)
            {
                string operationNo = row["OPERAPPLYID"].ToString();
                string specId= row["SPECID"].ToString();
                SpecSource s1 = specSourceManage.GetSource(specId, "");
                SpecSource s2 = specSourceManage.GetSendDocInfoInHis(operationNo);
                s1.MediDoc = s2.MediDoc.Clone();
                s1.IsInBase = '1';
                s1.DeptNo = s2.DeptNo;
                if (specSourceManage.UpdateSpecSource(s1) <= -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新标本源失败");
                    return;
                }
                
                //SpecPatient tmpPatient = patientManage.GetPatientInfoInNum(row["INPATIENT_NO"].ToString());
                //if (tmpPatient != null && tmpPatient.PatientID > 0)
                //{
                //    tmpPatient.Birthday = row["BIRTHDAY"].ToString();
                //    tmpPatient.ContactNum = row["
                //}
                SpecBase specBase = new SpecBase();
                specBase.SpecSource.SpecId = s1.SpecId;
                specBase.CliDiagICD = row["CLINIC_DIAGICD"].ToString();
                specBase.CliDiagName = row["CLINIC_DIAGICDNAME"].ToString();
                specBase.InBaseTime = DateTime.Now;
                specBase.InDiaICD = row["INHOS_DIAGICD"].ToString();
                specBase.InDiaName = row["INHOS_DIAGICDNAME"].ToString();
                //specBase.MainDiagName2 = row[""].ToString();
                specBase.MainDiaICD = row["MAIN_DIAGICD"].ToString();
                //specBase.MainDiaICD1 = row[""].ToString();
                //specBase.MainDiaICD2 = row[""].ToString();
                specBase.MainDiaName = row["MAIN_DIAGICDNAME"].ToString();
                //specBase.MainDiaName1 = row[""].ToString();
                specBase.ModICD = row["MAIN_DIAGICD"].ToString();
                specBase.ModName = row["MAIN_DIAGICDNAME"].ToString();
                specBase.OutDiaICD = row["MAIN_DIAGICD"].ToString();
                specBase.OutDiaName = row["MAIN_DIAGICDNAME"].ToString();
                string sequence = "";
                baseManage.GetNextSequence(ref sequence);
                specBase.BaseID = Convert.ToInt32(sequence);
                if (baseManage.InsertBase(specBase) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新诊断信息失败");
                    return;
                }                
            }

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            Query();
        }
    }
}