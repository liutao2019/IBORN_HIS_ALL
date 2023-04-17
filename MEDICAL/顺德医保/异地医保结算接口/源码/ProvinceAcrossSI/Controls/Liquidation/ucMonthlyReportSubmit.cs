using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ProvinceAcrossSI.Controls.Liquidation
{
    public partial class ucMonthlyReportSubmit : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMonthlyReportSubmit()
        {
            InitializeComponent();
        }

        ProvinceAcrossSI.Business.InPatient.InPatientService prService = new ProvinceAcrossSI.Business.InPatient.InPatientService();

        ProvinceAcrossSI.Business.Common.YDStatReport reportMgr = new ProvinceAcrossSI.Business.Common.YDStatReport();
        ProvinceAcrossSI.ProvinceAcrossSIDatabase ydMgr = new ProvinceAcrossSIDatabase();

        /// <summary>
        /// 患者入出转业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.BizLogic.Manager.Person perMgr = new FS.HISFC.BizLogic.Manager.Person();

        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.FrameWork.Models.NeuObject neuObj = null;

        FS.FrameWork.Models.NeuObject commObj = null;

        private string settingFilePatch = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\YD\月度申报.xml";

        Hashtable hsPartientInfo = new Hashtable();
 
        int year = 0;
        int month = 0;
        string type = "";
        string errInfo = "";
        ArrayList alPatientInfo = null;

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("申报明细提交", "提交明细月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("申报汇总提交", "提交汇总月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("申报分险种汇总提交", "提交分险种汇总月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("撤销", "撤销月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "申报明细提交":
                    this.SubmitApply();
                    break;
                case "申报汇总提交":
                    this.TotApply();
                    break;
                case "申报分险种汇总提交":
                    this.SubmitXZApply();
                    break;

                case "撤销":
                    this.CancelApply();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            ArrayList al = perMgr.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);//.GetEmployee("F");

            al.AddRange(perMgr.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.O));

            this.cmbFZ.AddItems(al);
            this.cmbFH.AddItems(al);
            this.cmbTB.AddItems(al);

            al = constMgr.GetList("YD_AAE140");

            this.cmbCustomType.AddItems(al);

            if (System.IO.File.Exists(this.settingFilePatch))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpTotal, this.settingFilePatch);
            }
            this.fpTotal.RowCount = 0;

            base.OnLoad(e);
        }
        //月度结算申请提交情况查询
        protected override int OnQuery(object sender, object neuObject)
        {
            year = this.dtMonth.Value.Date.Year;
            month = this.dtMonth.Value.Month;
            type = this.cmbCustomType.Tag.ToString();

            DateTime dtBegin = new DateTime(year, month, 1,0,0,0);
            DateTime dtEnd = dtBegin.AddMonths(1).AddSeconds(-1);

            hsPartientInfo = new Hashtable();
            alPatientInfo = reportMgr.QueryYDLiquidation(dtBegin, dtEnd,type);


            this.SetValueToFp(alPatientInfo);

            return 1;
        }

        private void SetValueToFp(ArrayList al)
        {
            this.fpTotal.RowCount = 0;

            int rowcount = 0;
            foreach (ProvinceAcrossSI.Objects.SIPersonInfo patient in al)
            {
                FS.HISFC.Models.RADT.PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(patient.InPatient_No);
                if (!hsPartientInfo.ContainsKey(patient.InPatient_No))
                {
                    hsPartientInfo.Add(patient.InPatient_No, patientTemp);
                }
                rowcount = this.fpTotal.RowCount;
                this.fpTotal.Rows.Add(rowcount,1);
                this.fpTotal.Cells[rowcount, 0].Text = "true";
                this.fpTotal.Cells[rowcount, 0].Locked = true;
                this.fpTotal.Cells[rowcount, 1].Text = patient.IdenNo;
                this.fpTotal.Cells[rowcount, 2].Text = patient.Name;
                this.fpTotal.Cells[rowcount, 3].Text = patient.ClinicNo;
                this.fpTotal.Cells[rowcount, 4].Text = patient.MCardNo;

                this.fpTotal.Cells[rowcount, 5].Text = patient.JSYWH;
                this.fpTotal.Cells[rowcount, 6].Text =  patientTemp.PVisit.InTime.ToString();
                this.fpTotal.Cells[rowcount, 7].Text =  patientTemp.PVisit.OutTime.ToString();
                this.fpTotal.Cells[rowcount, 8].Text = patientTemp.PVisit.OutTime.ToString();//patientTemp.BalanceDate.ToString();
                this.fpTotal.Cells[rowcount, 9].Text = patientTemp.ClinicDiagnose;
                this.fpTotal.Cells[rowcount, 10].Text = this.ydMgr.QueryICDName(patient.Diagn1);//patient.DiagnName;
                this.fpTotal.Cells[rowcount, 11].Text = patient.tot_cost.ToString();
                this.fpTotal.Cells[rowcount, 12].Text = patient.YBTCZF_cost.ToString();
                this.fpTotal.Cells[rowcount, 13].Text = patient.GRZIFUJE.ToString();
                this.fpTotal.Cells[rowcount, 14].Text = "异地住院"; //险种类型
                this.fpTotal.Cells[rowcount, 15].Text = this.cmbCustomType.Text.ToString();//patient.xzlx; //险种类型
                this.fpTotal.Cells[rowcount, 16].Text = patientTemp.PID.PatientNO;

                this.fpTotal.Rows[rowcount].Tag = patient;
            }

        }

        private void SubmitApply()
        {
            if (alPatientInfo != null)
            {
                commObj = new FS.FrameWork.Models.NeuObject();
                neuObj = new FS.FrameWork.Models.NeuObject();

                string serNo = reportMgr.GetLiquidationBusNO();

                commObj.ID = serNo.PadLeft(7,'0') ;//结算申报业务号
                commObj.Name = this.cmbCustomType.Tag.ToString();
                commObj.User01 = this.cmbFZ.Text.Trim();
                commObj.User02 = this.cmbFH.Text.Trim();
                commObj.User03 = this.cmbTB.Text.Trim();
                commObj.Memo = this.txtPhone.Text.Trim();

                neuObj.ID = this.year.ToString();
                neuObj.Name = this.month.ToString();
                neuObj.Memo = this.cmbCustomType.Tag.ToString();
                neuObj.User01 = serNo.PadLeft(7, '0');
                neuObj.User02 = FS.FrameWork.Management.Connection.Operator.ID;

                errInfo = "";

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                reportMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int result = reportMgr.InsertLiquidInfo(neuObj, commObj);
                if (result <= 0)
                {
                    MessageBox.Show("插入申报结算表出错！");
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
                string msg = "";
                result = prService.MonthlyReportSubmit(year.ToString(), month.ToString(),ref commObj, neuObj, alPatientInfo, hsPartientInfo, ref msg);

                if (result <= 0)
                {
                    MessageBox.Show("提交月度申报出错！" + msg);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }

                result = reportMgr.UpdateLiquidInfoTransID(neuObj, commObj);
                if (result <= 0)
                {
                    MessageBox.Show("插入申报结算表出错！");
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }

                if (result > 0)
                {
                    MessageBox.Show("提交月度申报成功！");
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                else
                {
                    MessageBox.Show("提交失败！"+errInfo);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
            }
        }

        private void TotApply()
        {
            if (alPatientInfo != null)
            {
                commObj = new FS.FrameWork.Models.NeuObject();
                neuObj = new FS.FrameWork.Models.NeuObject();

                string serNo = reportMgr.GetLiquidationBusNO();

                commObj.ID = serNo.PadLeft(7, '0');//结算申报业务号
                commObj.Name = this.cmbCustomType.Tag.ToString();
                commObj.User01 = this.cmbFZ.Text.Trim();
                commObj.User02 = this.cmbFH.Text.Trim();
                commObj.User03 = this.cmbTB.Text.Trim();
                commObj.Memo = this.txtPhone.Text.Trim();

                neuObj.ID = this.year.ToString();
                neuObj.Name = this.month.ToString();
                neuObj.Memo = this.cmbCustomType.Tag.ToString();
                neuObj.User01 = serNo.PadLeft(7, '0');
                neuObj.User02 = FS.FrameWork.Management.Connection.Operator.ID;

                errInfo = "";
                DialogResult dr = MessageBox.Show("请确保当前内容已经进行明细提交！", "异地医保", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return;
                }
                ArrayList alPS = this.dealTotApply(alPatientInfo);
                string msg = "";
                int result = prService.MonthlyReportTot(year.ToString(), month.ToString(), ref commObj, alPS, ref msg);
            }
        }

        private ArrayList dealTotApply(ArrayList alPatientInfo)
        {
            InsuredAreaCodeSort iSort = new InsuredAreaCodeSort();
            alPatientInfo.Sort(iSort);
            ArrayList alReturn = new ArrayList();
            ProvinceAcrossSI.Objects.SIPersonInfo patient = new ProvinceAcrossSI.Objects.SIPersonInfo();

            foreach (ProvinceAcrossSI.Objects.SIPersonInfo patientTemp in alPatientInfo)
            {
                if (patient.InsuredAreaCode == patientTemp.InsuredAreaCode)
                {
                    patient.Diagn1 = (FS.FrameWork.Function.NConvert.ToInt32(patient.Diagn1) + 1).ToString();//就医人数
                    patient.Diagn2 = (FS.FrameWork.Function.NConvert.ToInt32(patient.Diagn2) + 1).ToString();//就医人次
                    patient.tot_cost = patient.tot_cost + patientTemp.tot_cost;
                    patient.YBTCZF_cost = patient.YBTCZF_cost + patientTemp.YBTCZF_cost;
                    patient.DBYLTCZF_cost = patient.DBYLTCZF_cost + patientTemp.DBYLTCZF_cost;
                    patient.GRZIFUJE = patient.GRZIFUJE + patientTemp.GRZIFUJE;
                }
                else
                {
                    if (!string.IsNullOrEmpty(patient.InsuredAreaCode))
                    {
                        alReturn.Add(patient);
                        patient = new ProvinceAcrossSI.Objects.SIPersonInfo();
                    }

                    patient.InsuredAreaCode = patientTemp.InsuredAreaCode;
                    patient.Diagn1 = (FS.FrameWork.Function.NConvert.ToInt32(patient.Diagn1) + 1).ToString();//就医人数
                    patient.Diagn2 = (FS.FrameWork.Function.NConvert.ToInt32(patient.Diagn2) + 1).ToString();//就医人次
                    patient.tot_cost = patient.tot_cost + patientTemp.tot_cost;
                    patient.YBTCZF_cost = patient.YBTCZF_cost + patientTemp.YBTCZF_cost;
                    patient.DBYLTCZF_cost = patient.DBYLTCZF_cost + patientTemp.DBYLTCZF_cost;
                    patient.GRZIFUJE = patient.GRZIFUJE + patientTemp.GRZIFUJE;
                }
            }

            alReturn.Add(patient);
            return alReturn;
        }

        private void SubmitXZApply()
        {
            if (alPatientInfo != null)
            {
                commObj = new FS.FrameWork.Models.NeuObject();
                neuObj = new FS.FrameWork.Models.NeuObject();

                string serNo = reportMgr.GetLiquidationBusNO();

                commObj.ID = serNo.PadLeft(7, '0');//结算申报业务号
                commObj.Name = this.cmbCustomType.Tag.ToString();
                commObj.User01 = this.cmbFZ.Text.Trim();
                commObj.User02 = this.cmbFH.Text.Trim();
                commObj.User03 = this.cmbTB.Text.Trim();
                commObj.Memo = this.txtPhone.Text.Trim();

                neuObj.ID = this.year.ToString();
                neuObj.Name = this.month.ToString();
                neuObj.Memo = this.cmbCustomType.Tag.ToString();
                neuObj.User01 = serNo.PadLeft(7, '0');
                neuObj.User02 = FS.FrameWork.Management.Connection.Operator.ID;

                errInfo = "";
                DialogResult dr = MessageBox.Show("请确保当前内容已经进行明细提交！", "异地医保", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return;
                }
                ArrayList alPS = this.dealTotApply(alPatientInfo);
                string msg = "";
                int result = prService.MonthlyReportSubmitXZ(year.ToString(), month.ToString(), "1", ref commObj, alPS, ref msg);
            }
        }

        private void CancelApply()
        {

        }
    }

    public class InsuredAreaCodeSort : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                ProvinceAcrossSI.Objects.SIPersonInfo patient1 = x as ProvinceAcrossSI.Objects.SIPersonInfo;
                ProvinceAcrossSI.Objects.SIPersonInfo patient2 = y as ProvinceAcrossSI.Objects.SIPersonInfo;
                return string.Compare(patient1.InsuredAreaCode ,patient2.InsuredAreaCode);
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }

}
