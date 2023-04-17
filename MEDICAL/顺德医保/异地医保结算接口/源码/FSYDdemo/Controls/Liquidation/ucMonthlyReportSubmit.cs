using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FoShanYDSI.Controls.Liquidation
{
    public partial class ucMonthlyReportSubmit : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMonthlyReportSubmit()
        {
            InitializeComponent();
        }

        FoShanYDSI.Business.InPatient.InPatientService prService = new FoShanYDSI.Business.InPatient.InPatientService();

        FoShanYDSI.Business.Common.YDStatReport reportMgr = new FoShanYDSI.Business.Common.YDStatReport();
        FoShanYDSI.FoShanYDSIDatabase ydMgr = new FoShanYDSIDatabase();

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
            toolBarService.AddToolButton("月报汇总撤销", "撤销月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("分险种汇总撤销", "撤销月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("月报明细撤销", "撤销月度申报", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
           
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

                case "月报汇总撤销":
                    this.CancelApply();
                    break;
                case "分险种汇总撤销":
                    this.CancelApplyByCustomType();
                    break;
                case "月报明细撤销":
                    this.CancelApplyDetail();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            ArrayList al = perMgr.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);//.GetEmployee("F");

            //al.AddRange(perMgr.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.O));

            this.cmbFZ.AddItems(al);
            this.cmbFH.AddItems(al);
            this.cmbTB.AddItems(al);

            al = constMgr.GetList("YD_AAE140");
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            al.Add(obj);

            this.cmbCustomType.AddItems(al);
            this.cmbCustomType.SelectedValue= obj.ID;

            if (System.IO.File.Exists(this.settingFilePatch))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpTotal, this.settingFilePatch);
            }
            this.fpTotal.RowCount = 0;

            string sql = @"select a.respons_oper,a.check_oper,a.fill_oper,a.telephone,a.oper_date from  YD_LIQUIDATION_INFO a
                        where rownum = 1
                        order by a.oper_date";
            System.Data.DataSet dsResult = null;

             if (this.reportMgr.ExecQuery(sql, ref dsResult) == -1)
            {
                return;
            }
             string respons_oper = "";
             string check_oper = "";
             string fill_oper = "";
             string telephone = "";
             DataTable dtResult = dsResult.Tables[0];
             foreach (DataRow dr in dtResult.Rows)
             {
                 respons_oper = dr["respons_oper"].ToString();
                 check_oper = dr["check_oper"].ToString();
                 fill_oper = dr["fill_oper"].ToString();
                 telephone = dr["telephone"].ToString();
             }

             this.cmbFZ.Text = respons_oper;
             this.cmbFH.Text = check_oper;
             this.cmbTB.Text = fill_oper;
             this.txtPhone.Text = telephone;
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
            foreach (FoShanYDSI.Objects.SIPersonInfo patient in al)
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
                this.fpTotal.Cells[rowcount, 8].Text = patientTemp.BalanceDate.ToString(); //patientTemp.PVisit.OutTime.ToString();//patientTemp.BalanceDate.ToString();
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
            //if (this.cmbCustomType.Text!="全部")
            //{
            //    MessageBox.Show("明细提交请查询所有明细，险种不作选择再查选！");
            //    return ;
            //}
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
                neuObj.Name = this.month.ToString().PadLeft(2, '0');
                neuObj.Memo = this.cmbCustomType.Tag.ToString();
                neuObj.User01 = serNo.PadLeft(7, '0');
                neuObj.User02 = FS.FrameWork.Management.Connection.Operator.ID;

                errInfo = "";

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                reportMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int result = reportMgr.InsertLiquidInfo(neuObj, commObj);
                if (result < 0)
                {
                    MessageBox.Show("插入申报结算表出错！");
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
                string msg = "";
                result = prService.MonthlyReportSubmit(year.ToString(), month.ToString().PadLeft(2, '0'), ref commObj, neuObj, alPatientInfo, hsPartientInfo, ref msg);

                if (result < 0)
                {
                    MessageBox.Show("提交月度申报出错！" + msg);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }

                result = reportMgr.UpdateLiquidInfoTransID(neuObj, commObj);
                if (result < 0)
                {
                    MessageBox.Show("插入申报结算表出错！");
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }

                if (result > 0)
                {
                    MessageBox.Show("申报明细提交成功！");
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                else
                {
                    MessageBox.Show("申报明细提交提交失败！" + msg);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
            }
        }

        private void TotApply()
        {
            if (this.cmbCustomType.Text != "全部")
            {
                MessageBox.Show("汇总提交请查询所有明细，险种不作选择再查选！");
                return ;
            }
            if (alPatientInfo != null)
            {
                commObj = new FS.FrameWork.Models.NeuObject();
                neuObj = new FS.FrameWork.Models.NeuObject();

                string serNo = reportMgr.GetLiquidationBusNO();

                //commObj.ID = serNo.PadLeft(7, '0');//结算申报业务号
                commObj.Name = this.cmbCustomType.Tag.ToString();
                commObj.User01 = this.cmbFZ.Text.Trim();
                commObj.User02 = this.cmbFH.Text.Trim();
                commObj.User03 = this.cmbTB.Text.Trim();
                commObj.Memo = this.txtPhone.Text.Trim();

                neuObj.ID = this.year.ToString();
                neuObj.Name = this.month.ToString().PadLeft(2, '0');
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
                ArrayList alo = reportMgr.QueryLiquidInfo(neuObj.ID, neuObj.Name, neuObj.Memo);
                if (alo == null)
                {
                    MessageBox.Show("请先提交明细！");
                    return;
                }
                else if (alo.Count <= 0)
                {
                    MessageBox.Show("请先提交明细！");
                    return;
                }
                FS.FrameWork.Models.NeuObject ot = alo[0] as FS.FrameWork.Models.NeuObject;
                commObj.ID = ot.Name;
                string msg = "";
                int result = prService.MonthlyReportTot(year.ToString(), month.ToString().PadLeft(2, '0'), ref commObj, alPS, ref msg);

                if (result > 0)
                {
                    MessageBox.Show("申报汇总提交成功！");
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                else
                {
                    MessageBox.Show("申报汇总提交提交失败！" + msg);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
            }
        }

        private ArrayList dealTotApply(ArrayList alPatientInfo)
        {
            InsuredAreaCodeSort iSort = new InsuredAreaCodeSort();
            alPatientInfo.Sort(iSort);
            ArrayList alReturn = new ArrayList();
            FoShanYDSI.Objects.SIPersonInfo patient = new FoShanYDSI.Objects.SIPersonInfo();

            foreach (FoShanYDSI.Objects.SIPersonInfo patientTemp in alPatientInfo)
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
                        patient = new FoShanYDSI.Objects.SIPersonInfo();
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
            if (this.cmbCustomType.Text=="全部" || string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()))
            {
                MessageBox.Show("分险种提交请选择险种类型！");
                return ;
            }
            if (alPatientInfo != null)
            {
                commObj = new FS.FrameWork.Models.NeuObject();
                neuObj = new FS.FrameWork.Models.NeuObject();

                string serNo = reportMgr.GetLiquidationBusNO();

                //commObj.ID = serNo.PadLeft(7, '0');//结算申报业务号
                commObj.Name = this.cmbCustomType.Tag.ToString();
                commObj.User01 = this.cmbFZ.Text.Trim();
                commObj.User02 = this.cmbFH.Text.Trim();
                commObj.User03 = this.cmbTB.Text.Trim();
                commObj.Memo = this.txtPhone.Text.Trim();

                neuObj.ID = this.year.ToString();
                neuObj.Name = this.month.ToString().PadLeft(2, '0');
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
                ArrayList alo =  reportMgr.QueryLiquidInfo(neuObj.ID, neuObj.Name, neuObj.Memo);
                if (alo == null)
                {
                    MessageBox.Show("请先提交明细！");
                    return;
                }
                else if (alo.Count == 0)
                {
                    MessageBox.Show("请先提交明细！");
                    return;
                }

                FS.FrameWork.Models.NeuObject ot = alo[0] as FS.FrameWork.Models.NeuObject;
                commObj.ID = ot.Name;
                string msg = "";
                int result = prService.MonthlyReportSubmitXZ(year.ToString(), month.ToString().PadLeft(2, '0'), alPS.Count.ToString()
                    , ref commObj, alPS, ref msg);
                if (result > 0)
                {
                    MessageBox.Show("申报分险种汇总提交成功！");
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                else
                {
                    MessageBox.Show("申报分险种汇总提交失败！" + msg);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
            }
        }

        private void CancelApply()
        {
            if (this.cmbCustomType.Text == "全部" || string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()))
            {
                MessageBox.Show("分险种提交请选择险种类型！");
                return;
            }
            string serNo = reportMgr.GetLiquidationBusNO();
            commObj = new FS.FrameWork.Models.NeuObject();
            neuObj = new FS.FrameWork.Models.NeuObject();


            neuObj.ID = this.year.ToString();
            neuObj.Name = this.month.ToString().PadLeft(2, '0');
            neuObj.User02 = FS.FrameWork.Management.Connection.Operator.ID;
            neuObj.Memo = this.cmbCustomType.Tag.ToString();

            DialogResult dr = MessageBox.Show("是否确定撤销汇总？", "异地医保", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                return;
            }
            ArrayList alo = reportMgr.QueryLiquidInfo(neuObj.ID, neuObj.Name, neuObj.Memo);
            if (alo == null)
            {
                MessageBox.Show("没有找到提交的信息！");
                return;
            }
            else if (alo.Count <= 0)
            {
                MessageBox.Show("没有找到提交的信息！");
                return;
            }

            FS.FrameWork.Models.NeuFileInfo ot = alo[0] as FS.FrameWork.Models.NeuFileInfo;
            commObj.Memo = ot.FilePath;//原交易业务流水号
            commObj.ID = ot.Name;
            string msg = "";
            int result = prService.MonthlyReportCancel(year.ToString(), month.ToString().PadLeft(2, '0'), ref commObj, neuObj, ref msg);

            if (result > 0)
            {
                MessageBox.Show("撤销成功！");
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else
            {
                MessageBox.Show("撤销失败！" + msg);
                FS.FrameWork.Management.PublicTrans.RollBack();
            }


        }
        /// <summary>
        /// 分险种汇总撤销
        /// </summary>
        private void CancelApplyByCustomType()
        {
            if (this.cmbCustomType.Text == "全部" || string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()))
            {
                MessageBox.Show("分险种提交请选择险种类型！");
                return;
            }

            if (alPatientInfo != null)
            {

                ArrayList alPS = this.dealTotApply(alPatientInfo);
                if (alPS.Count > 0)
                {
                    string serNo = reportMgr.GetLiquidationBusNO();
                    commObj = new FS.FrameWork.Models.NeuObject();
                    neuObj = new FS.FrameWork.Models.NeuObject();

                    //commObj.ID = serNo.PadLeft(7, '0');//结算申报业务号
                    commObj.Name = this.cmbCustomType.Tag.ToString();

                    neuObj.ID = this.year.ToString();
                    neuObj.Name = this.month.ToString().PadLeft(2, '0');
                    neuObj.Memo = this.cmbCustomType.Tag.ToString();
                    neuObj.User02 = FS.FrameWork.Management.Connection.Operator.ID;

                    DialogResult dr = MessageBox.Show("是否确定撤销？", "异地医保", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                    ArrayList alo = reportMgr.QueryLiquidInfo(neuObj.ID, neuObj.Name, neuObj.Memo);
                    if (alo == null)
                    {
                        MessageBox.Show("没有找到提交的信息！");
                        return;
                    }
                    else if (alo.Count <= 0)
                    {
                        MessageBox.Show("没有找到提交的信息！");
                        return;
                    }

                    FS.FrameWork.Models.NeuFileInfo ot = alo[0] as FS.FrameWork.Models.NeuFileInfo;
                    commObj.Memo = ot.FilePath;//原交易业务流水号
                    commObj.ID = ot.Name;

                    string msg = "";
                    int result = prService.MonthlyReportCancelByCustomType0537(year.ToString(), month.ToString().PadLeft(2, '0'), ref commObj, neuObj, ref msg);

                    if (result > 0)
                    {
                        MessageBox.Show("撤销成功！");
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    else
                    {
                        MessageBox.Show("撤销失败！" + msg);
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //return;
                    }
                    //Hashtable InsuredAreaCodeHS = new Hashtable();
                    //foreach (FoShanYDSI.Objects.SIPersonInfo patient in alPS)
                    //{

                    //    if (InsuredAreaCodeHS.Contains(patient.InsuredAreaCode))
                    //    {
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        InsuredAreaCodeHS.Add(patient.InsuredAreaCode, patient);
                    //    }
                    //    neuObj.User01 = patient.InsuredAreaCode;

                    //}


                }
            }
            else
            {
                MessageBox.Show("患者信息为空！");
            }
        }


        /// <summary>
        /// 提交审核支付明细回退0524
        /// </summary>
        private void CancelApplyDetail()
        {
            if (this.cmbCustomType.Text == "全部" || string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()))
            {
                MessageBox.Show("分险种提交请选择险种类型！");
                return;
            }

            if (alPatientInfo != null)
            {

                ArrayList alPS = this.dealTotApply(alPatientInfo);
                if (alPS.Count > 0)
                {
                    string serNo = reportMgr.GetLiquidationBusNO();
                    commObj = new FS.FrameWork.Models.NeuObject();
                    neuObj = new FS.FrameWork.Models.NeuObject();

                    //commObj.ID = serNo.PadLeft(7, '0');//结算申报业务号
                    commObj.Name = this.cmbCustomType.Tag.ToString();

                    neuObj.ID = this.year.ToString();
                    neuObj.Name = this.month.ToString().PadLeft(2, '0');
                    neuObj.Memo = this.cmbCustomType.Tag.ToString();
                    neuObj.User02 = FS.FrameWork.Management.Connection.Operator.ID;

                    DialogResult dr = MessageBox.Show("是否确定撤销？", "异地医保", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                    ArrayList alo = reportMgr.QueryLiquidInfo(neuObj.ID, neuObj.Name, neuObj.Memo);
                    if (alo == null)
                    {
                        MessageBox.Show("没有找到提交的信息！");
                        return;
                    }
                    else if (alo.Count <= 0)
                    {
                        MessageBox.Show("没有找到提交的信息！");
                        return;
                    }

                    FS.FrameWork.Models.NeuFileInfo ot = alo[0] as FS.FrameWork.Models.NeuFileInfo;
                    commObj.Memo = ot.FilePath;//原交易业务流水号
                    commObj.ID = ot.Name;

                    string msg = "";
                    int result = prService.MonthlyReportCancelByCustomType(year.ToString(), month.ToString().PadLeft(2, '0'), ref commObj, neuObj, ref msg);

                    if (result > 0)
                    {
                        MessageBox.Show("撤销成功！");
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    else
                    {
                        MessageBox.Show("撤销失败！" + msg);
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //return;
                    }
                    //Hashtable InsuredAreaCodeHS = new Hashtable();
                    //foreach (FoShanYDSI.Objects.SIPersonInfo patient in alPS)
                    //{
                        
                    //    if (InsuredAreaCodeHS.Contains(patient.InsuredAreaCode))
                    //    {
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        InsuredAreaCodeHS.Add(patient.InsuredAreaCode, patient);
                    //    }
                    //    neuObj.User01 = patient.InsuredAreaCode;

                    //}


                }
            }
            else
            {
                MessageBox.Show("患者信息为空！");
            }
        }
        private void cmbCustomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fpTotal.RowCount = 0;
        }
    }

    public class InsuredAreaCodeSort : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FoShanYDSI.Objects.SIPersonInfo patient1 = x as FoShanYDSI.Objects.SIPersonInfo;
                FoShanYDSI.Objects.SIPersonInfo patient2 = y as FoShanYDSI.Objects.SIPersonInfo;
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
