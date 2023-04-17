using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using FS.FrameWork.WinForms.Forms;
using FS.FrameWork.Models;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Management;

namespace InterfaceInstanceDefault.IFeeOweMessage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmGreenSwitch : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public frmGreenSwitch()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerInteger = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Manager.Constant conManger = new FS.HISFC.BizLogic.Manager.Constant();
        private FS.FrameWork.Management.DataBaseManger dbm = new FS.FrameWork.Management.DataBaseManger();
        PatientInfo patientInfo = null;
        /// <summary>
        /// 患者入出转业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// 费用类业务层 {2CEA3B1D-2E59-44ac-9226-7724413173C5} 对业务层的引用全部改为非静态的变量
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();


        private void ucQueryPatientInfo_myEvent()
        {
            if (this.ucQueryPatientInfo.InpatientNo == null || this.ucQueryPatientInfo.InpatientNo == string.Empty)
            {
                MessageBox.Show(Language.Msg("该患者不存在!请验证后输入"));

                return;
            }

            PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(this.ucQueryPatientInfo.InpatientNo);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("该患者不存在!请验证后输入"));

                return;
            }
         
            this.SetPatientInfomation(patientTemp);
            this.Query(patientTemp);
        }

        protected virtual int SetPatientInfomation(PatientInfo patientInfo)
        {
             if (patientInfo == null)
            {
                return -1;
            }

            ucQueryPatientInfo.Text = patientInfo.PID.PatientNO;
            txtName.Text = patientInfo.Name;
            txtInTime.Text = patientInfo.PVisit.InTime.ToString("d");
            txtPact.Text = patientInfo.Pact.Name;
            txtInDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            txtAlarm.Text = patientInfo.PVisit.MoneyAlert.ToString();
            txtLeft.Text = patientInfo.FT.LeftCost.ToString();
            this.patientInfo = patientInfo;
         
            return 0;
        }
        protected virtual int SetPatientCurrentStatus(bool s)
        {
            bool currStatus = false;
            currStatus = s;
            if (currStatus == true)
            {
                this.lbCurr.Text = "当前状态 已办理";
                this.btnOpen.Enabled = false;
                this.btnClose.Enabled = true;
            }
            else
            {
                this.lbCurr.Text = "当前状态 已终止";
                this.btnOpen.Enabled = true ;
                this.btnClose.Enabled = false;
            }
            #region 交过预交金的停止办理
             ArrayList al =
           inpatientManager.QueryPrepays(this.patientInfo.ID);
            if (al != null)
            {
                if (al.Count > 0)
                {
                    this.lbCurr.Text = "当前状态 已有预交金交易记录，仅能查询不能修改！";

                    this.btnOpen.Enabled = false;
                    this.btnClose.Enabled = false;
                }
            }

            #endregion
            return 0;
        }
        /// <summary>
        /// 清空函数
        /// </summary>
        protected virtual int Clear()
        {
            this.txtName.Text = string.Empty;
            this.txtInTime.Text = string.Empty;
            this.txtPact.Text = string.Empty;
            this.txtInDept.Text = string.Empty;
            this.txtAlarm.Text = string.Empty;
            this.txtLeft.Text = string.Empty;
            this.lbCurr.Text = string.Empty;
            this.ucQueryPatientInfo.Focus();
            this.patientInfo = null;
            this.btnClose.Enabled = false;
            this.btnOpen.Enabled = false;
            return 0;
        }

        /// <summary>
        /// 清空函数
        /// </summary>
        protected virtual int Query(PatientInfo patientInfo)
        {
            bool currStatus = false;
            if (patientInfo == null)
            {
                return -1;
            }

            DataSet ds = new DataSet();
            string sql = string.Empty;
            sql = @"
select   OPER_DATE, OPER_CODE, OPER_NAME, decode(STATUS,'0','已终止','1','已办理')
  from TJ_LOCAL_GREEN_PASSAGE_LOG where INPATIENT_NO = '" + patientInfo .ID+ @"'
 order by oper_date desc
";
            if (dbm.ExecQuery(sql, ref ds)<0)
            {
                MessageBox.Show(Language.Msg("查询患者办理/终止记录出错！"));
                return -1;
            }
            if (ds.Tables.Count<=0)
            {
                return -1;
            }
            this.neuSpread1_Sheet1.DataSource = ds.Tables[0];
            if (ds.Tables[0].Rows.Count>0)
            {
                if (ds.Tables[0].Rows[0][3].ToString() == "已办理")
                {
                    currStatus = true;
                }
            }
            SetPatientCurrentStatus(currStatus);
            return 0;
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.patientInfo!=null)
            {
                if (this.Switch(this.patientInfo,true )<0)
                {
                    MessageBox.Show(Language.Msg("患者办理绿色通道出错！"));
                }
                this.Query(this.patientInfo); 
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.patientInfo != null)
            {
                if (this.Switch(this.patientInfo, false) < 0)
                {
                    MessageBox.Show(Language.Msg("患者终止绿色通道出错！"));
                }
                this.Query(this.patientInfo);
            }
        }
        protected virtual int Switch(PatientInfo patientInfo,bool s)
        {
            int retVal = 0;
            string sql = string.Empty;
            string status = "0";
            if (s==true)
            {
                status = "1";
            }
            sql = @"
        insert into TJ_LOCAL_GREEN_PASSAGE_LOG
          (INPATIENT_NO, OPER_DATE, OPER_CODE, OPER_NAME, STATUS)
        values
          ('" +patientInfo.ID + @"',sysdate,'" + dbm.Operator.ID + @"','" + dbm.Operator.Name + @"','" + status + @"')
";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {

                if (dbm.ExecNoQuery(sql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();                 
                    return -1;
                }


                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();                
                return -1;
            }
            return retVal;
        }
    }
}
