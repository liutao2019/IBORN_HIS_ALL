using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseLend
{
    public partial class ucCasePrintRegister :FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 常数维护打印内容； 住院号检索信息，保存打印内容
        /// </summary>
        public ucCasePrintRegister()
        {
            InitializeComponent();
        }
        
        FS.HISFC.BizLogic.HealthRecord.Case.CasePrintRegister cprMgr = new FS.HISFC.BizLogic.HealthRecord.Case.CasePrintRegister();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        FS.HISFC.BizProcess.Integrate.RADT radtMana = new FS.HISFC.BizProcess.Integrate.RADT();

        private bool isUserInterFace = false;//是否使用接口  --广东省病案接口  
        /// <summary>
        /// 是否使用广东省病案接口
        /// </summary>
        [Category("是否使用广东省病案接口"), Description("从广东省病案系统获取数据")]
        public bool IsUserInterFace
        {
            get { return this.isUserInterFace; }
            set { this.isUserInterFace = value; }
        }


        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.Init();
            this.txtCaseNO.Tag = null;
            return base.OnInit(sender, neuObject, param);
        }

        private void Init()
        {
            ArrayList al = new ArrayList();
            al = this.conMgr.GetList("CasePrintRegister");
            if (al.Count > 0)
            {
                this.SetPrintReginster(al);
            }
        }

        private void SetPrintReginster(ArrayList al)
        {
            this.fpSpread1_Sheet1.RowCount = 0;

            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                int row = this.fpSpread1_Sheet1.RowCount - 1;
                int mod = row % 2;
                if (mod == 0)
                {
                    this.fpSpread1_Sheet1.Rows[row].BackColor = System.Drawing.Color.PeachPuff;
                }
                this.fpSpread1_Sheet1.Cells[row, 0].Text = "False";
                this.fpSpread1_Sheet1.Cells[row, 1].Text = obj.Name;
                this.fpSpread1_Sheet1.Cells[row, 2].Text = obj.User01;
                this.fpSpread1_Sheet1.Cells[row, 3].Text = obj.ID;
            }
        }

        private void txtCaseNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.txtCaseNO.Text.Trim() == "")
                {
                    return;
                }
                this.Query();
            }
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.checkBox1.Checked == false)
            {
                if (this.txtCaseNO.Text.Trim() == "")
                {
                    return -1;
                }
                this.Query();
            }
            else if (this.checkBox1.Checked == true)
            {
                this.QueryPRCount();
            }
            return base.OnQuery(sender, neuObject);
        }
        private void Query()
        {
            if (this.txtCaseNO.Tag != null)//判断是否保存了上一次的记录
            {
                FS.HISFC.Models.HealthRecord.Case.CaseStore infotemp = this.txtCaseNO.Tag as FS.HISFC.Models.HealthRecord.Case.CaseStore;
                if (infotemp.ID == "")
                {
                    if (MessageBox.Show(infotemp.PatientInfo.PID.PatientNO + "(" + infotemp.PatientInfo.Name + ")未保存，是否保存该患者记录", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.Save();
                        return;
                    }
                    else
                    {
                        this.Clear();
                    }
                }
            }

            string caseNo = "";
            string CaseId = "";//住院号
            string CaseInTimes = "";//住院次数
            caseNo = this.txtCaseNO.Text.Trim();
            caseNo = caseNo.Replace("—", "-");

            if (caseNo.IndexOf('-') > 0)
            {
                string[] CaseNO = caseNo.Split('-');
                CaseId = CaseNO[0].ToString().Trim();
                CaseInTimes = CaseNO[1].ToString().Trim();
            }
            else//应该考虑一下医务科审核的情况；
            {
                CaseId = caseNo;
                CaseInTimes = "1";
            }
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

            if (this.isUserInterFace == true)//使用广东省病案接口获取数据
            {
                FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadMgr = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();//广东省病案接口

                string fprn = CaseId.TrimStart('0').ToString();
                int inTimes = FS.FrameWork.Function.NConvert.ToInt32(CaseInTimes.Trim());
                patientInfo = uploadMgr.GetPatientByIdAndTimes(fprn, inTimes);
                if (patientInfo == null || patientInfo.ID == "")
                {
                    //MessageBox.Show("广东省病案接口中，未找到患者信息!", "提示");
                    //return;
                    string argCardNo = "T" + this.txtCaseNO.Text.TrimStart('0').PadLeft(9, '0'); //广东省估计需要通过病案接口获取信息会准确一点
                    patientInfo = this.radtMana.QueryComPatientInfo(argCardNo);//查com_patientinfo（卡号：T + 住院号（用0补齐9位））；这个时候需要录入住院次数；
                    if (patientInfo == null || patientInfo.PID.CardNO == null || patientInfo.PID.CardNO == string.Empty)
                    {
                        argCardNo = argCardNo.Replace('T', '0').PadLeft(10, '0');
                        patientInfo = this.radtMana.QueryComPatientInfo(argCardNo);//查com_patientinfo 卡号：住院号（用0补齐10位）
                        if (patientInfo == null || patientInfo.PID.CardNO == null || patientInfo.PID.CardNO == string.Empty)
                        {
                            MessageBox.Show("com_patientinfo未找到患者信息!" + radtMana.Err);
                            return;
                        }
                    }
                    patientInfo.PID.PatientNO = patientInfo.PID.CardNO.Replace('T', '0').PadLeft(10, '0');
                    patientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(CaseInTimes) ;
                }
            }
            else
            {
                string argCardNo = "T" + this.txtCaseNO.Text.TrimStart('0').PadLeft(9, '0'); //广东省估计需要通过病案接口获取信息会准确一点
                patientInfo = this.radtMana.QueryComPatientInfo(argCardNo);//查com_patientinfo（卡号：T + 住院号（用0补齐9位））；这个时候需要录入住院次数；
                if (patientInfo == null || patientInfo.PID.CardNO == null || patientInfo.PID.CardNO == string.Empty)
                {
                    argCardNo = argCardNo.Replace('T', '0').PadLeft(10, '0');
                    patientInfo = this.radtMana.QueryComPatientInfo(argCardNo);//查com_patientinfo 卡号：住院号（用0补齐10位）
                    if (patientInfo == null || patientInfo.PID.CardNO == null || patientInfo.PID.CardNO == string.Empty)
                    {
                        MessageBox.Show("com_patientinfo未找到患者信息!" + radtMana.Err);
                        return;
                    }
                }
                patientInfo.PID.PatientNO = patientInfo.PID.CardNO.Replace('T', '0').PadLeft(10, '0');
                patientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(CaseInTimes);
            }

            FS.HISFC.Models.HealthRecord.Case.CaseStore info = new FS.HISFC.Models.HealthRecord.Case.CaseStore(); //不写实体了，直接用这个吧

            info.PatientInfo.PID.PatientNO = patientInfo.PID.PatientNO.Replace('T', '0');
            info.PatientInfo.Name = patientInfo.Name;
            info.PatientInfo.InTimes = patientInfo.InTimes;
            info.OperEnv.Memo = "1";// 0 医务科批准 1 病案室执行完
            info.OperEnv.ID = this.conMgr.Operator.ID;
            info.OperEnv.OperTime = this.conMgr.GetDateTimeFromSysDateTime();
            this.txtCaseNO.Tag = info;
            label2.Text = "当前复印患者“" + info.PatientInfo.Name + "”第“" + info.PatientInfo.InTimes + "”住院病历";
            this.QueryHistory(info);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        private void Save()
        {
            if (this.txtCaseNO.Tag != null)
            {

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.cprMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                FS.HISFC.Models.HealthRecord.Case.CaseStore info = this.txtCaseNO.Tag as FS.HISFC.Models.HealthRecord.Case.CaseStore;
                string CaseNum = this.cprMgr.GetSequence("HealthReacord.Case.CasePrintRegister.Seq");
                ArrayList al= this.GetDetail(CaseNum);
                info.ID = CaseNum;
                if (this.cprMgr.InsertCasePrintRegister(info) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存复印记录失败！", "提示");
                    return;
                }
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    if (this.cprMgr.InsertCasePrintRegisterDetail(obj)<0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存复印明细记录失败！", "提示");
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("保存复印信息成功！", "提示");
                this.Clear();
            }
        }

        private ArrayList GetDetail(string Id)
        {
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj =null;
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                if (this.fpSpread1_Sheet1.Cells[i, 0].Text =="False")
                {
                    continue;
                }
                obj.ID = Id;
                obj.Name = this.fpSpread1_Sheet1.Cells[i, 1].Text;
                if (this.fpSpread1_Sheet1.Cells[i, 2].Text.Trim() == "")
                {
                    obj.User01 = "1";
                }
                else
                {
                    obj.User01 = this.fpSpread1_Sheet1.Cells[i, 2].Text.Trim();
                }
                obj.Memo = this.fpSpread1_Sheet1.Cells[i, 3].Text;
                al.Add(obj);                    
            }
            return al;
        }


        private void QueryHistory(FS.HISFC.Models.HealthRecord.Case.CaseStore info)
        {
            ArrayList al = this.cprMgr.QueryCasePrintRegister(info.PatientInfo.PID.PatientNO);
            if (al.Count > 0)
            {
                this.ShowHistory(al);
            }

        }

        private void ShowHistory(ArrayList al)
        {
            this.fpSpread_Sheet2.RowCount = 0;
            foreach (FS.HISFC.Models.HealthRecord.Case.CaseStore info in al)
            {
                this.fpSpread_Sheet2.Rows.Add(this.fpSpread_Sheet2.RowCount, 1);
                int row = this.fpSpread_Sheet2.RowCount - 1;
                this.fpSpread_Sheet2.Cells[row, 0].Text = info.PatientInfo.PID.PatientNO;
                this.fpSpread_Sheet2.Cells[row, 1].Text = info.PatientInfo.InTimes.ToString();
                this.fpSpread_Sheet2.Cells[row, 2].Text = info.PatientInfo.Name;
                this.fpSpread_Sheet2.Cells[row, 3].Text = info.OperEnv.OperTime.ToString();
                this.fpSpread_Sheet2.Cells[row, 4].Text = info.OperEnv.ID;
                this.fpSpread_Sheet2.Rows[row].Tag = info;
            }
            this.fpSpread_Sheet2.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
        }

        private void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.HISFC.Models.HealthRecord.Case.CaseStore info = this.fpSpread_Sheet2.Rows[this.fpSpread_Sheet2.ActiveRowIndex].Tag as FS.HISFC.Models.HealthRecord.Case.CaseStore;
            ArrayList al = this.cprMgr.QueryCasePrintRegisterDetail(info.ID);
            if (al == null)
            {
                return;
            }
            if (al.Count > 0)
            {
                this.fpSpread_Sheet3.RowCount = 0;
                this.ShowHistoryDetail(al);
            }
        }

        private void ShowHistoryDetail(ArrayList al)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                this.fpSpread_Sheet3.Rows.Add(this.fpSpread_Sheet3.RowCount, 1);
                int row = this.fpSpread_Sheet3.RowCount - 1;
                this.fpSpread_Sheet3.Cells[row, 0].Text = "True";
                this.fpSpread_Sheet3.Cells[row, 1].Text = obj.Name;
                this.fpSpread_Sheet3.Cells[row, 2].Text = obj.User01;
                this.fpSpread_Sheet3.Cells[row, 3].Text = obj.Memo;
            }
            this.fpSpread_Sheet3.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
        }

        private void Clear()
        {
            this.txtCaseNO.Tag = null;
            this.txtCaseNO.Text = "";
            this.label2.Text = "";
            this.txtCaseNO.Focus();
            this.Init();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == false)
            {
                if (this.txtCaseNO.Text.Trim() == "")
                {
                    return ;
                }
                this.Query();
            }
            else if (this.checkBox1.Checked == true)
            {
                this.QueryPRCount();
            }
        }
        /// <summary>
        /// 根据时间统计：复印的数量
        /// </summary>
        private void QueryPRCount()
        {
            DateTime dtBegin = this.dateTimePicker1.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00);
            DateTime dtEnd = this.dateTimePicker2.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            string count = this.cprMgr.QueryCasePrintRegisterCount(dtBegin, dtEnd);
            this.label3.Text = dtBegin.ToShortDateString() + "至" + dtEnd.ToShortDateString() + "共复印病案张数为“" + count + "”张";
            ArrayList alDetail = this.cprMgr.QueryCasePrintRegisterDetailCount(dtBegin, dtEnd);
            if (alDetail == null)
            {
                return;
            }
            this.fpSpread_Sheet3.RowCount = 0;
            this.ShowHistoryDetail(alDetail);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.dateTimePicker1.Enabled = true;
                this.dateTimePicker2.Enabled = true;
            }
            else if (this.checkBox1.Checked == false)
            {
                this.label3.Text = "";
                this.fpSpread_Sheet3.RowCount = 0;
                this.dateTimePicker1.Enabled = false;
                this.dateTimePicker2.Enabled = false;
            }
        }
    }
}
