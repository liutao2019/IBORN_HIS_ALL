using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace API.GZSI.Print
{
    public partial class ucInBalanceInfoForClinicNew : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInBalanceInfoForClinicNew()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 患者入出转转业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// 挂号业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        private LocalManager localMgr = new LocalManager();

        public int SetValue(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {

            try
            {
                if (String.IsNullOrEmpty(patientInfo.SIMainInfo.Psn_no))
                {
                    MessageBox.Show("患者人员编号为空！");
                    return -1;
                }

                if (String.IsNullOrEmpty(patientInfo.SIMainInfo.Mdtrt_id))
                {
                    MessageBox.Show("患者就诊ID，请确定是否登记成功！");
                    return -1;
                }

                if (String.IsNullOrEmpty(patientInfo.SIMainInfo.Setl_id))
                {
                    MessageBox.Show("患者结算ID为空，请确定是否结算成功！");
                    return -1;
                }
                #region 费用结算信息
                bool isTotQuerySucce = true;//是否获取本地显示

                API.GZSI.Business.Patient5203 patient5203 = new API.GZSI.Business.Patient5203();
                Models.Request.RequestGzsiModel5203 requestGzsiModel5203 = new Models.Request.RequestGzsiModel5203();
                Models.Response.ResponseGzsiModel5203 responseGzsiModel5203 = new Models.Response.ResponseGzsiModel5203();
                Models.Request.RequestGzsiModel5203.Data data5203 = new Models.Request.RequestGzsiModel5203.Data();
                data5203.psn_no = patientInfo.SIMainInfo.Psn_no;
                data5203.setl_id = patientInfo.SIMainInfo.Setl_id;
                data5203.mdtrt_id = patientInfo.SIMainInfo.Mdtrt_id;

                requestGzsiModel5203.data = data5203;
                if (patient5203.CallService(requestGzsiModel5203, ref responseGzsiModel5203) < 0)
                {
                    //MessageBox.Show("人员结算信息查询失败！" + responseGzsiModel5203.err_msg);
                    //return -1;
                    isTotQuerySucce = false;
                }
                else if (responseGzsiModel5203.infcode != "0")
                {
                    //MessageBox.Show("人员结算信息查询失败！" + responseGzsiModel5203.err_msg);
                    //return -1;
                    isTotQuerySucce = false;
                }
                else if (responseGzsiModel5203.output == null)
                {
                    //MessageBox.Show("未找到人员结算信息！" + responseGzsiModel5203.err_msg);
                    //return -1;
                    isTotQuerySucce = false;
                }
                else if (responseGzsiModel5203.output.setlinfo == null)
                {
                    //MessageBox.Show("人员结算信息为空！" + responseGzsiModel5203.err_msg);
                    //return -1;
                    isTotQuerySucce = false;
                }
                else if (string.IsNullOrEmpty(responseGzsiModel5203.output.setlinfo.mdtrt_id))
                {
                    //MessageBox.Show("结算单号" + patientInfo.SIMainInfo.Mdtrt_id + "信息为空！" + responseGzsiModel5203.err_msg);
                    //return -1;
                    isTotQuerySucce = false;
                }

                if (isTotQuerySucce)
                {
                    //if (!string.IsNullOrEmpty(patientInfo.ID))
                    //{
                    //    if (patientInfo.SIMainInfo.TypeCode == "1")
                    //    {
                    //        FS.HISFC.Models.Registration.Register reg = null; //患者基本信息
                    //        reg = this.registerManager.GetByClinic(patientInfo.ID);
                    //        if (reg == null)
                    //        {
                    //            MessageBox.Show("查询挂号信息失败!");
                    //            return -1;
                    //        }
                    //        patientInfo.PID = reg.PID;
                    //        patientInfo.Birthday = reg.Birthday;
                    //        patientInfo.PVisit.PatientLocation.Dept = reg.DoctorInfo.Templet.Dept;
                    //        patientInfo.PVisit.InTime = reg.DoctorInfo.SeeDate;
                    //        patientInfo.Sex = reg.Sex;
                    //        patientInfo.Name = reg.Name;
                    //        patientInfo.CompanyName = reg.CompanyName;
                    //        patientInfo.IDCard = reg.IDCard;
                    //        patientInfo.PhoneHome = reg.PhoneHome;
                    //    }
                    //    else
                    //    {
                    //        FS.HISFC.Models.RADT.PatientInfo patientInfoTemp = new FS.HISFC.Models.RADT.PatientInfo();
                    //        patientInfoTemp = radtIntegrate.QueryPatientInfoByInpatientNO(patientInfo.ID);
                    //        if (patientInfoTemp == null)
                    //        {
                    //            MessageBox.Show("查询住院信息失败!");
                    //            return -1;
                    //        }
                    //        patientInfo.PID = patientInfoTemp.PID;
                    //        patientInfo.Birthday = patientInfoTemp.Birthday;
                    //        patientInfo.PVisit = patientInfoTemp.PVisit;
                    //        patientInfo.Sex = patientInfoTemp.Sex;
                    //        patientInfo.Name = patientInfoTemp.Name;
                    //        patientInfo.CompanyName = patientInfoTemp.CompanyName;
                    //        patientInfo.IDCard = patientInfoTemp.IDCard;
                    //        patientInfo.PhoneHome = patientInfoTemp.PhoneHome;
                    //    }
                    //}
                    this.neuSpread1_Sheet1.Cells[2, 1].Text = "机构名称:" + FS.FrameWork.Management.Connection.Hospital.Name;
                    this.neuSpread1_Sheet1.Cells[2, 4].Text = "机构编码:" + Models.Config.HospitalCode;

                    string ybjsjb = consMgr.GetConstant("GZSI_hosp_lv", responseGzsiModel5203.output.setlinfo.hosp_lv).Name;
                    this.neuSpread1_Sheet1.Cells[2, 6].Text = "医保结算级别:" + (string.IsNullOrEmpty(ybjsjb) ? responseGzsiModel5203.output.setlinfo.hosp_lv : ybjsjb)
                      ; //+ "   医保结算类别:"

                    this.neuSpread1_Sheet1.Cells[3, 1].Text = "就医登记号:" + responseGzsiModel5203.output.setlinfo.mdtrt_id;
                    this.neuSpread1_Sheet1.Cells[3, 4].Text = "结算号:" + responseGzsiModel5203.output.setlinfo.setl_id;
                    //if (FS.FrameWork.Management.Connection.Hospital.Name == "")
                    //{
                    //}
                    this.neuSpread1_Sheet1.Cells[3, 6].Text = "";// "行政级别:";// +Models.Config.FixmedinsCode;


                    this.neuSpread1_Sheet1.Cells[4, 1].Text = "姓名:" + responseGzsiModel5203.output.setlinfo.psn_name;
                    this.neuSpread1_Sheet1.Cells[4, 2].Text = "性别:" + (responseGzsiModel5203.output.setlinfo.gend == "2" ? "女" : "男");
                    this.neuSpread1_Sheet1.Cells[4, 3].Text = "出生日期:" + responseGzsiModel5203.output.setlinfo.brdy;
                    this.neuSpread1_Sheet1.Cells[4, 4].Text = "个人电脑号:" + responseGzsiModel5203.output.setlinfo.psn_no;

                    string rylb = consMgr.GetConstant("GZSI_psn_type", responseGzsiModel5203.output.setlinfo.psn_type).Name;
                    this.neuSpread1_Sheet1.Cells[4, 6].Text = "人员类别:" + (string.IsNullOrEmpty(rylb) ? responseGzsiModel5203.output.setlinfo.psn_type : rylb);
                    // {DF92CB37-6922-46b2-BE54-436BBD826FD2}
                    this.neuSpread1_Sheet1.Cells[5, 1].Text = "单位名称:" + responseGzsiModel5203.output.setlinfo.emp_name; //patientInfo.CompanyName;
                    this.neuSpread1_Sheet1.Cells[5, 3].Text = "联系电话:" + patientInfo.PhoneHome;
                    this.neuSpread1_Sheet1.Cells[5, 5].Text = "身份证号:" + responseGzsiModel5203.output.setlinfo.certno;

                    if (patientInfo.SIMainInfo.TypeCode == "1")
                    {
                        this.neuSpread1_Sheet1.Cells[6, 1].Text = "门诊号:" + patientInfo.PID.CardNO.TrimStart('0');
                        this.neuSpread1_Sheet1.Cells[6, 3].Text = "科别:" + patientInfo.PVisit.PatientLocation.Dept.Name;
                        this.neuSpread1_Sheet1.Cells[6, 6].Text = "住院天数:-";
                        this.neuSpread1_Sheet1.Cells[7, 3].Text = "门诊诊断:" + localMgr.QueryICDByCode(patientInfo.SIMainInfo.InDiagnose.ID).Name;
                        this.neuSpread1_Sheet1.Cells[7, 5].Text = "";

                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[6, 1].Text = "住院号:" + patientInfo.PID.PatientNO.TrimStart('0');
                        this.neuSpread1_Sheet1.Cells[6, 3].Text = "科别:" + patientInfo.PVisit.PatientLocation.Dept.Name;
                        TimeSpan indays = DateTime.Parse(responseGzsiModel5203.output.setlinfo.enddate) - DateTime.Parse(responseGzsiModel5203.output.setlinfo.begndate);

                        string days = "1";
                        if (indays.Days <= 0)
                        {
                            days = "1";
                        }
                        else
                        {

                            days = indays.Days.ToString();
                        }

                        this.neuSpread1_Sheet1.Cells[6, 6].Text = "住院天数:" + days;
                        this.neuSpread1_Sheet1.Cells[7, 3].Text = "入院第一诊断:" + localMgr.QueryICDByCode(patientInfo.SIMainInfo.InDiagnose.ID).Name;
                        this.neuSpread1_Sheet1.Cells[7, 5].Text = "出院第一诊断:" + localMgr.QueryICDByCode(patientInfo.SIMainInfo.InDiagnose.ID).Name;


                        this.neuSpread1_Sheet1.Cells[6, 2].Text = "床号:" + (patientInfo.PVisit.PatientLocation.Bed.ID.Length >= 4 ? patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4, patientInfo.PVisit.PatientLocation.Bed.ID.Length - 4) : patientInfo.PVisit.PatientLocation.Bed.ID);
                    }

                    this.neuSpread1_Sheet1.Cells[6, 4].Text = "入院日期:" + responseGzsiModel5203.output.setlinfo.begndate;
                    this.neuSpread1_Sheet1.Cells[6, 5].Text = "出院日期:" + responseGzsiModel5203.output.setlinfo.enddate;


                    string xz = consMgr.GetConstant("GZSI_insutype", responseGzsiModel5203.output.setlinfo.insutype).Name;
                    this.neuSpread1_Sheet1.Cells[7, 1].Text = "险种:" + (string.IsNullOrEmpty(xz) ? responseGzsiModel5203.output.setlinfo.insutype : xz);

                    this.neuSpread1_Sheet1.Cells[8, 1].Text = "业务类别:" + consMgr.GetConstant("GZSI_med_type", responseGzsiModel5203.output.setlinfo.med_type).Name;

                    this.neuSpread1_Sheet1.Cells[8, 5].Text = "结算时间:" + responseGzsiModel5203.output.setlinfo.setl_time;

                    //先自付
                    decimal xzfTot = decimal.Parse(responseGzsiModel5203.output.setlinfo.fulamt_ownpay_amt)
                        + decimal.Parse(responseGzsiModel5203.output.setlinfo.overlmt_selfpay) + decimal.Parse(responseGzsiModel5203.output.setlinfo.preselfpay_amt);

                    //段内自付
                    decimal dnzfTot = decimal.Parse(responseGzsiModel5203.output.setlinfo.medfee_sumamt) - decimal.Parse(responseGzsiModel5203.output.setlinfo.fund_pay_sumamt) -
                        decimal.Parse(responseGzsiModel5203.output.setlinfo.fulamt_ownpay_amt) - decimal.Parse(responseGzsiModel5203.output.setlinfo.overlmt_selfpay) -
                        decimal.Parse(responseGzsiModel5203.output.setlinfo.preselfpay_amt);
                    if (dnzfTot < 0)
                    {
                        dnzfTot = 0;
                    }
                    this.neuSpread1_Sheet1.Cells[9, 1].Text = @"本次就医：总费用" + responseGzsiModel5203.output.setlinfo.medfee_sumamt + "元，基金支付" +
                        responseGzsiModel5203.output.setlinfo.fund_pay_sumamt + "元，个人支付" +
                        (decimal.Parse(responseGzsiModel5203.output.setlinfo.medfee_sumamt) - decimal.Parse(responseGzsiModel5203.output.setlinfo.fund_pay_sumamt)).ToString() + "元"
                        + "（起付线" + responseGzsiModel5203.output.setlinfo.act_pay_dedc + "元 先自负" + xzfTot.ToString() + "元 段内自付" + dnzfTot.ToString() + "元）";


                    this.neuSpread1_Sheet1.Cells[21, 1].Text = "基本医疗保险统筹基金支付：" + responseGzsiModel5203.output.setlinfo.hifp_pay;
                    this.neuSpread1_Sheet1.Cells[21, 5].Text = "大额基金支付：" + responseGzsiModel5203.output.setlinfo.hifob_pay;

                    this.neuSpread1_Sheet1.Cells[23, 1].Text = "大病基金支付：" + responseGzsiModel5203.output.setlinfo.hifmi_pay;
                    this.neuSpread1_Sheet1.Cells[23, 5].Text = "补充保险基金支付：" + responseGzsiModel5203.output.setlinfo.hifes_pay;
                    this.neuSpread1_Sheet1.Cells[24, 1].Text = "医疗救助基金支付：" + responseGzsiModel5203.output.setlinfo.maf_pay;
                    this.neuSpread1_Sheet1.Cells[24, 5].Text = "公务员基金支付：" + responseGzsiModel5203.output.setlinfo.cvlserv_pay;
                    this.neuSpread1_Sheet1.Cells[25, 1].Text = "其他基金支付：" + responseGzsiModel5203.output.setlinfo.oth_pay;
                    this.neuSpread1_Sheet1.Cells[25, 5].Text = "个人账户支付：" + responseGzsiModel5203.output.setlinfo.acct_pay;

                    //TNND，说要改后面又不改
                    this.neuSpread1_Sheet1.Rows.Get(26).Height = 0F;
                    this.neuSpread1_Sheet1.Rows.Get(28).Height = 0F;
                    this.neuSpread1_Sheet1.Rows.Get(29).Height = 0F;

                    this.neuSpread1_Sheet1.Cells[26, 1].Text = "核报金额：" + "0.00";
                    this.neuSpread1_Sheet1.Cells[26, 5].Text = "当年度特定门诊累计统筹基金支付余额：" + "0.00";
                    this.neuSpread1_Sheet1.Cells[27, 1].Text = "";
                    this.neuSpread1_Sheet1.Cells[27, 5].Text = "";
                    this.neuSpread1_Sheet1.Cells[28, 1].Text = "当年度统筹基金支付余额：" + "0.00";
                    this.neuSpread1_Sheet1.Cells[28, 5].Text = "累计大病保险核报基数：" + "0.00";
                    this.neuSpread1_Sheet1.Cells[29, 1].Text = "纳入大病保险金额：" + "0.00";
                    this.neuSpread1_Sheet1.Cells[29, 5].Text = "";



                    this.neuSpread1_Sheet1.Cells[31, 5].Text = "制单人：" + FS.FrameWork.Management.Connection.Operator.Name;
                    this.neuSpread1_Sheet1.Cells[31, 7].Text = "打印日期：" + DateTime.Now.ToString();

                }
                else
                {
                    LocalSetTotInfo(patientInfo.SIMainInfo.Mdtrt_id, patientInfo.SIMainInfo.Setl_id, patientInfo);
                }
                #endregion

                #region 费用明细

                bool isQueryFeeDetail = true;
                API.GZSI.Business.Patient5204 patient5204 = new API.GZSI.Business.Patient5204();
                Models.Request.RequestGzsiModel5204 requestGzsiModel5204 = new Models.Request.RequestGzsiModel5204();
                Models.Response.ResponseGzsiModel5204 responseGzsiModel5204 = new Models.Response.ResponseGzsiModel5204();
                Models.Request.RequestGzsiModel5204.Data data5204 = new Models.Request.RequestGzsiModel5204.Data();
                data5204.psn_no = patientInfo.SIMainInfo.Psn_no;
                data5204.setl_id = patientInfo.SIMainInfo.Setl_id;
                data5204.mdtrt_id = patientInfo.SIMainInfo.Mdtrt_id;

                requestGzsiModel5204.data = data5204;
                if (patient5204.CallService(requestGzsiModel5204, ref responseGzsiModel5204) < 0)
                {
                    //MessageBox.Show("费用明细查询失败！" + responseGZSIModel5204.err_msg);
                    //return -1;
                    isQueryFeeDetail = false;
                }
                else if (responseGzsiModel5204.infcode != "0")
                {
                    //MessageBox.Show("费用明细查询失败！" + responseGZSIModel5204.err_msg);
                    //return -1;
                    isQueryFeeDetail = false;
                }
                else if (responseGzsiModel5204.output == null)
                {
                    //MessageBox.Show("未找到人员费用明细查询！" + responseGZSIModel5204.err_msg);
                    //return -1;
                    isQueryFeeDetail = false;
                }
                else if (responseGzsiModel5204.output == null || responseGzsiModel5204.output.Count <= 0)
                {
                    //MessageBox.Show("费用明细查询为空！" + responseGZSIModel5204.err_msg);
                    //return -1;
                    isQueryFeeDetail = false;
                }



                decimal alTot = 0;
                decimal alPub = 0;
                decimal alOwn = 0;
                this.neuSpread1_Sheet1.Cells[11, 2].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[11, 3].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[11, 4].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[11, 6].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[11, 7].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[11, 8].Text = "0.00";


                this.neuSpread1_Sheet1.Cells[12, 2].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[12, 3].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[12, 4].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[21, 6].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[12, 7].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[12, 8].Text = "0.00";

                this.neuSpread1_Sheet1.Cells[13, 2].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[13, 3].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[13, 4].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[13, 6].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[13, 7].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[13, 8].Text = "0.00";

                this.neuSpread1_Sheet1.Cells[14, 2].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[14, 3].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[14, 4].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[14, 6].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[14, 7].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[14, 8].Text = "0.00";

                this.neuSpread1_Sheet1.Cells[15, 2].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[15, 3].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[15, 4].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[15, 6].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[15, 7].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[15, 8].Text = "0.00";

                this.neuSpread1_Sheet1.Cells[16, 2].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[16, 3].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[16, 4].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[16, 6].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[16, 7].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[16, 8].Text = "0.00";

                this.neuSpread1_Sheet1.Cells[17, 2].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[17, 3].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[17, 4].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[17, 6].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[17, 7].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[17, 8].Text = "0.00";

                this.neuSpread1_Sheet1.Cells[20, 2].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[20, 3].Text = "0.00";
                this.neuSpread1_Sheet1.Cells[20, 4].Text = "0.00";

                if (isQueryFeeDetail)
                {
                    for (int index = 0; index < responseGzsiModel5204.output.Count; index++)
                    {
                        Models.Response.ResponseGzsiModel5204.Output out5204 = responseGzsiModel5204.output[index] as Models.Response.ResponseGzsiModel5204.Output;

                        alTot += decimal.Parse(out5204.det_item_fee_sumamt);
                        alOwn += decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt);
                        alPub += decimal.Parse(out5204.preselfpay_amt);
                        if (alOwn > 0)
                        {
                        }
                        if (out5204.med_chrgitm_type == "01")//床位费
                        {
                            this.neuSpread1_Sheet1.Cells[11, 2].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[11, 2].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[11, 3].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[11, 3].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[11, 4].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[11, 4].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "02")//诊察费
                        {
                            this.neuSpread1_Sheet1.Cells[11, 6].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[11, 6].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[11, 7].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[11, 7].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[11, 8].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[11, 8].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "03")//检查费
                        {
                            this.neuSpread1_Sheet1.Cells[12, 2].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[12, 2].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[12, 3].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[12, 3].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[12, 4].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[12, 4].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "04")//化验费
                        {
                            this.neuSpread1_Sheet1.Cells[12, 6].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[12, 6].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[12, 7].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[12, 7].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[12, 8].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[12, 8].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "05")//治疗费
                        {
                            this.neuSpread1_Sheet1.Cells[13, 2].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[13, 2].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[13, 3].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[13, 3].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[13, 4].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[13, 4].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "06")//手术费
                        {
                            this.neuSpread1_Sheet1.Cells[13, 6].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[13, 6].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[13, 7].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[13, 7].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[13, 8].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[13, 8].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "07")//护理费
                        {
                            this.neuSpread1_Sheet1.Cells[14, 2].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[14, 2].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[14, 3].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[14, 3].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[14, 4].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[14, 4].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "08")//卫生材料费
                        {
                            this.neuSpread1_Sheet1.Cells[14, 6].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[14, 6].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[14, 7].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[14, 7].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[14, 8].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[14, 8].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "09")//西药费
                        {
                            this.neuSpread1_Sheet1.Cells[15, 2].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[15, 2].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[15, 3].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[15, 3].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[15, 4].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[15, 4].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "10")//中药饮片费
                        {
                            this.neuSpread1_Sheet1.Cells[15, 6].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[15, 6].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[15, 7].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[15, 7].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[15, 8].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[15, 8].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "11")//中成药费
                        {
                            this.neuSpread1_Sheet1.Cells[16, 2].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[16, 2].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[16, 3].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[16, 3].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[16, 4].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[16, 4].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "12")//一般诊疗费
                        {
                            this.neuSpread1_Sheet1.Cells[16, 6].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[16, 6].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[16, 7].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[16, 7].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[16, 8].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[16, 8].Text)).ToString("F2");
                        }
                        else if (out5204.med_chrgitm_type == "13")//挂号费
                        {
                            this.neuSpread1_Sheet1.Cells[17, 2].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[17, 2].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[17, 3].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[17, 3].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[17, 4].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[17, 4].Text)).ToString("F2");
                        }
                        else// if (out5204.med_chrgitm_type == "14")//其他费
                        {
                            this.neuSpread1_Sheet1.Cells[17, 6].Text = (decimal.Parse(out5204.det_item_fee_sumamt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[17, 6].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[17, 7].Text = (decimal.Parse(out5204.fulamt_ownpay_amt) + decimal.Parse(out5204.overlmt_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[17, 7].Text)).ToString("F2");
                            this.neuSpread1_Sheet1.Cells[17, 8].Text = (decimal.Parse(out5204.preselfpay_amt) + decimal.Parse(this.neuSpread1_Sheet1.Cells[17, 8].Text)).ToString("F2");
                        }
                    }
                    this.neuSpread1_Sheet1.Cells[20, 2].Text = alTot.ToString("F2");
                    this.neuSpread1_Sheet1.Cells[20, 3].Text = alOwn.ToString("F2");
                    this.neuSpread1_Sheet1.Cells[20, 4].Text = alPub.ToString("F2");

                }
                else
                {
                    ArrayList al = new ArrayList();
                    if (patientInfo.SIMainInfo.TypeCode == "1")
                    {
                        al = localMgr.GetInvoiceDetail(patientInfo.SIMainInfo.InvoiceNo, true);
                    }
                    else
                    {
                        al = localMgr.GetInvoiceDetail(patientInfo.SIMainInfo.InvoiceNo, false);
                    }
                    if (al == null || al.Count <= 0)
                    {
                        MessageBox.Show("未找到结算明细：" + patientInfo.SIMainInfo.InvoiceNo);
                        return -1;
                    }
                    decimal othFee = 0;
                    alTot = 0;
                    alPub = 0;
                    alOwn = 0;
                    foreach (FS.FrameWork.Models.NeuObject obj in al)
                    {
                        alTot += decimal.Parse(obj.Memo);
                        if (obj.Name.Contains("床位"))//床位费
                        {
                            this.neuSpread1_Sheet1.Cells[11, 2].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[11, 2].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("诊查"))//诊察费
                        {
                            this.neuSpread1_Sheet1.Cells[11, 6].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[11, 6].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("检查"))//检查费
                        {
                            this.neuSpread1_Sheet1.Cells[12, 2].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[12, 2].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("化验") || obj.Name.Contains("检验"))//化验费
                        {
                            this.neuSpread1_Sheet1.Cells[12, 6].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[12, 6].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("治疗"))//治疗费
                        {
                            this.neuSpread1_Sheet1.Cells[13, 2].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[13, 2].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("手术"))//手术费
                        {
                            this.neuSpread1_Sheet1.Cells[13, 6].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[13, 6].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("护理"))//护理费
                        {
                            this.neuSpread1_Sheet1.Cells[14, 2].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[14, 2].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("材料"))//卫生材料费
                        {
                            this.neuSpread1_Sheet1.Cells[14, 6].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[14, 6].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("西药"))//西药费
                        {
                            this.neuSpread1_Sheet1.Cells[15, 2].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[15, 2].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("中草") || obj.Name.Contains("草"))//中药饮片费
                        {
                            this.neuSpread1_Sheet1.Cells[15, 6].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[15, 6].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("中成药"))//中成药费
                        {
                            this.neuSpread1_Sheet1.Cells[16, 2].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[16, 2].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("诊查"))//一般诊疗费
                        {
                            this.neuSpread1_Sheet1.Cells[16, 6].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[16, 6].Text)).ToString("F2");
                        }
                        else if (obj.Name.Contains("挂号"))//挂号费
                        {
                            this.neuSpread1_Sheet1.Cells[17, 2].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[17, 2].Text)).ToString("F2");
                        }
                        else// if (out5204.med_chrgitm_type == "14")//其他费
                        {
                            this.neuSpread1_Sheet1.Cells[17, 6].Text = (decimal.Parse(obj.Memo) + decimal.Parse(this.neuSpread1_Sheet1.Cells[17, 6].Text)).ToString("F2");
                        }
                    }

                }

                this.neuSpread1_Sheet1.Cells[20, 2].Text = alTot.ToString("F2");
                this.neuSpread1_Sheet1.Cells[20, 3].Text = alOwn.ToString("F2");
                this.neuSpread1_Sheet1.Cells[20, 4].Text = alPub.ToString("F2");


                #endregion


                //this.neuSpread1_Sheet1.Cells[2, 1].Text = "机构名称:" + FS.FrameWork.Management.Connection.Hospital.Name;
                //this.neuSpread1_Sheet1.Cells[2, 4].Text = "机构编码:" + Models.Config.FixmedinsCode;
                //this.neuSpread1_Sheet1.Cells[2, 6].Text = "医保结算级别:";

                //this.neuSpread1_Sheet1.Cells[3, 1].Text = "就医登记号:" + patientInfo.SIMainInfo.patientInfoNo;

                //this.neuSpread1_Sheet1.Cells[4, 1].Text = "姓名:" + patientInfo.Name;
                //this.neuSpread1_Sheet1.Cells[4, 2].Text = "性别:" + patientInfo.Sex.Name;
                //this.neuSpread1_Sheet1.Cells[4, 3].Text = "出生日期:" + patientInfo.Birthday.ToShortDateString();
                //this.neuSpread1_Sheet1.Cells[4, 4].Text = "个人电脑号:" + patientInfo.SIMainInfo.Psn_no;
                //this.neuSpread1_Sheet1.Cells[4, 5].Text = "人员类别:";

                //this.neuSpread1_Sheet1.Cells[5, 1].Text = "单位名称:" + patientInfo.CompanyName;
                //this.neuSpread1_Sheet1.Cells[5, 3].Text = "联系电话:" + patientInfo.PhoneHome;
                //this.neuSpread1_Sheet1.Cells[5, 5].Text = "身份证号:" + patientInfo.IDCard;

                //if (patientInfo.SIMainInfo.TypeCode == "1")
                //{
                //    this.neuSpread1_Sheet1.Cells[6, 1].Text = "门诊号:" + patientInfo.PID.CardNO;
                //    this.neuSpread1_Sheet1.Cells[6, 4].Text = "入院日期:" + patientInfo.DoctorInfo.SeeDate.ToShortDateString();
                //    this.neuSpread1_Sheet1.Cells[6, 5].Text = "出院日期:" + patientInfo.DoctorInfo.SeeDate.ToShortDateString();
                //}
                //else
                //{
                //    this.neuSpread1_Sheet1.Cells[6, 1].Text = "住院号:" + patientInfo.PID.PatientNO;
                //    this.neuSpread1_Sheet1.Cells[6, 4].Text = "入院日期:" + patientInfo.PVisit.InTime.ToShortDateString();
                //    this.neuSpread1_Sheet1.Cells[6, 5].Text = "出院日期:" + patientInfo.PVisit.OutTime.ToShortDateString();
                //}

                //this.neuSpread1_Sheet1.Cells[6, 3].Text = "科别:" + patientInfo.PVisit.PatientLocation.Dept.Name;
                //TimeSpan indays = patientInfo.PVisit.OutTime - patientInfo.PVisit.InTime;
                //this.neuSpread1_Sheet1.Cells[6, 6].Text = "住院天数:" + indays.Days;

                //this.neuSpread1_Sheet1.Cells[7, 1].Text = "险种:" + consMgr.GetConstant("GZSI_INSUTYPE", patientInfo.SIMainInfo.Insutype).Name;
                //this.neuSpread1_Sheet1.Cells[7, 2].Text = "入院第一诊断:" + localMgr.QueryICDByCode(patientInfo.SIMainInfo.InDiagnose.ID).Name;
                //this.neuSpread1_Sheet1.Cells[7, 3].Text = "出院第一诊断:" + localMgr.QueryICDByCode(patientInfo.SIMainInfo.InDiagnose.ID).Name;

                //this.neuSpread1_Sheet1.Cells[8, 1].Text = "业务类别:" + consMgr.GetConstant("GZSI_MED_TYPE", patientInfo.SIMainInfo.Psn_type).Name;
                //if (patientInfo.SIMainInfo.BalanceDate == DateTime.MinValue)
                //{
                //    this.neuSpread1_Sheet1.Cells[8, 5].Text = "结算时间:" + DateTime.Now.ToString();
                //}
                //else
                //{
                //    this.neuSpread1_Sheet1.Cells[8, 5].Text = "结算时间:" + patientInfo.SIMainInfo.BalanceDate; 
                //}


                //this.neuSpread1_Sheet1.Cells[9, 1].Text = "本次就医：总费用" + patientInfo.SIMainInfo.TotCost.ToString() + "元，基金支付" + patientInfo.SIMainInfo.PubCost.ToString() + "元，个人支付" + patientInfo.SIMainInfo.OwnCost.ToString() + "元";//（起付线 act_pay_dedc 元 先自负 fulamt_ownpay_amt + overlmt_selfpay + preselfpay_amt元 段内自付 medfee_sumamt - fund_pay_sumamt - fulamt_ownpay_amt - overlmt_selfpay - preselfpay_amt 元）;
                //this.neuSpread1_Sheet1.Cells[9, 2].Text = patientInfo.SIMainInfo.PubCost.ToString();//基金支付合计
                //this.neuSpread1_Sheet1.Cells[10, 2].Text = patientInfo.SIMainInfo.Ake039.ToString();//统筹基金
                //this.neuSpread1_Sheet1.Cells[11, 2].Text = patientInfo.SIMainInfo.Akb066.ToString();//个人账户基金
                //this.neuSpread1_Sheet1.Cells[12, 2].Text = patientInfo.SIMainInfo.Ake035.ToString();//公务员员医疗补助
                //this.neuSpread1_Sheet1.Cells[13, 2].Text = patientInfo.SIMainInfo.Ake026.ToString();//补充医疗保险基金
                //this.neuSpread1_Sheet1.Cells[14, 2].Text = patientInfo.SIMainInfo.Ake029.ToString();//大病保险
                //this.neuSpread1_Sheet1.Cells[15, 2].Text = patientInfo.SIMainInfo.Bka821.ToString();//医疗救助
                //this.neuSpread1_Sheet1.Cells[16, 2].Text = "0.00";//伤残人员医疗保险基金
                //this.neuSpread1_Sheet1.Cells[17, 2].Text = "0.00";//离休人员医疗统筹基金
                //this.neuSpread1_Sheet1.Cells[18, 2].Text = patientInfo.SIMainInfo.Bka840.ToString();//其他基金

                //this.neuSpread1_Sheet1.Cells[9, 5].Text = patientInfo.SIMainInfo.OwnCost.ToString();//自费金额




                //this.neuSpread1_Sheet1.Cells[2, 1].Text = "患者姓名:" + patientInfo.Name;
                //this.neuSpread1_Sheet1.Cells[2, 2].Text = 
                //this.neuSpread1_Sheet1.Cells[2, 3].Text = "年龄:" + patientInfo.Age;
                //this.neuSpread1_Sheet1.Cells[2, 4].Text = "身份证号:" + patientInfo.IDCard;
                //this.neuSpread1_Sheet1.Cells[2, 5].Text = "社保卡号:" + patientInfo.SSN;


                ////this.neuSpread1_Sheet1.Cells[3, 3].Text = 
                //this.neuSpread1_Sheet1.Cells[3, 3].Text = "就医登记号:" + patientInfo.SIMainInfo.patientInfoNo;


                ////this.neuSpread1_Sheet1.Cells[5, 1].Text = "入院方式:" + consMgr.GetConstant("INSOURCE", patientInfo.PVisit.InSource.ID).Name;
                ////this.neuSpread1_Sheet1.Cells[5, 4].Text = "就医登记号:" + patientInfo.SIMainInfo.patientInfoNo;
                ////this.neuSpread1_Sheet1.Cells[5, 5].Text = "出院科室:" + patientInfo.PVisit.PatientLocation.Dept.Name;

                //this.neuSpread1_Sheet1.Cells[6, 1].Text = "主要诊断:" + localMgr.QueryICDByCode(patientInfo.SIMainInfo.InDiagnose.ID).Name;
                //this.neuSpread1_Sheet1.Cells[6, 4].Text = "单位:元(保留两位小数)";

                //this.neuSpread1_Sheet1.Cells[7, 1].Text = "入院时间:" + patientInfo.PVisit.InTime.ToShortDateString();
                //this.neuSpread1_Sheet1.Cells[7, 2].Text = "出院时间:" + patientInfo.PVisit.OutTime.ToShortDateString();
                //
                //this.neuSpread1_Sheet1.Cells[7, 4].Text = "共" + indays.Days + "天                   单位:元(保留两位小数)";

                //this.neuSpread1_Sheet1.Cells[8, 1].Text = "总费用:" + patientInfo.SIMainInfo.TotCost.ToString();
                //this.neuSpread1_Sheet1.Cells[8, 2].Text = "统筹内费用:" + patientInfo.SIMainInfo.Bka826;//patient.SIMainInfo.PubCost.ToString();// "????";
                //this.neuSpread1_Sheet1.Cells[8, 4].Text = "自费费用:" + patientInfo.SIMainInfo.Bka825;//patient.SIMainInfo.OwnCost.ToString(); ;// "????";
                //this.neuSpread1_Sheet1.Cells[8, 5].Text = "本次起付标准:" + patientInfo.SIMainInfo.Aka151.ToString();



                //this.neuSpread1_Sheet1.Cells[19, 1].Text = "门诊号:" + patientInfo.PID.CardNO;//住院号
                //this.neuSpread1_Sheet1.Cells[19, 5].Text = "打印日期:" + localMgr.GetSysDate("yyyyMMdd");//打印日期
            }
            catch
            {
                return -1;
            }
            return 1;
        }

        public int Query(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool isOut)
        {
            if (patientInfo == null)
            {
                MessageBox.Show("查找患者失败");
                return -1;
            }
            if (string.IsNullOrEmpty(patientInfo.SIMainInfo.Mdtrt_id))
            {
                MessageBox.Show("患者结算单号为空！");
                return -1;
            }
            //if (!string.IsNullOrEmpty(patientInfo.ID))
            //{
            //    if (isOut)
            //    {
            //        patientInfo = localMgr.GetPatientBalanceInfo(patientInfo, "1");
            //    }
            //    else
            //    {
            //        patientInfo = localMgr.GetPatientBalanceInfo(patientInfo, "2");
            //    }
            //}
            //else if (!string.IsNullOrEmpty(patientInfo.SIMainInfo.Mdtrt_id))
            {
                localMgr.GetPatientInfoByFeeInfo(patientInfo.SIMainInfo.Mdtrt_id, patientInfo.SIMainInfo.Setl_id, "", "", ref patientInfo);
            }

            if (!patientInfo.SIMainInfo.IsBalanced)
            {
                MessageBox.Show("患者未医保结算!");
                return -1;
            }
            if (SetValue(patientInfo) < 0)
            {
                MessageBox.Show("赋值失败!");
                return -1;
            }
            //PrintInBalanceInfo();
            return 1;
        }

        /// <summary>
        /// 本地设置头部
        /// </summary>
        private void LocalSetTotInfo(string Mdtrt_id, string Setl_id, FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            FS.HISFC.Models.RADT.PatientInfo inp = patientInfo;
            localMgr.GetSIPersonInfo(Mdtrt_id, Setl_id, ref inp);
            if (string.IsNullOrEmpty(inp.ID))
            {
                MessageBox.Show("查询本地结算信息失败!");
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfoTemp = new FS.HISFC.Models.RADT.PatientInfo();
            if (inp.SIMainInfo.TypeCode == "1")
            {
                FS.HISFC.Models.Registration.Register reg = null; //患者基本信息
                reg = this.registerManager.GetByClinic(inp.ID);
                if (reg == null)
                {
                    MessageBox.Show("查询挂号信息失败!");
                    return;
                }
                patientInfoTemp.PID = reg.PID;
                patientInfoTemp.Birthday = reg.Birthday;
                patientInfoTemp.PVisit.PatientLocation.Dept = reg.DoctorInfo.Templet.Dept;
                patientInfoTemp.PVisit.InTime = reg.DoctorInfo.SeeDate;
                patientInfoTemp.Sex = reg.Sex;
                patientInfoTemp.Name = reg.Name;
                patientInfoTemp.CompanyName = reg.CompanyName;
                patientInfoTemp.IDCard = reg.IDCard;
                patientInfoTemp.PhoneHome = reg.PhoneHome;
                patientInfoTemp.SIMainInfo = inp.SIMainInfo;
            }
            else
            {
                patientInfoTemp = radtIntegrate.QueryPatientInfoByInpatientNO(inp.ID);
                if (patientInfoTemp == null)
                {
                    MessageBox.Show("查询住院信息失败!");
                    return;
                }
                patientInfoTemp.SIMainInfo = inp.SIMainInfo;
            }

            this.neuSpread1_Sheet1.Cells[2, 1].Text = "机构名称:" + FS.FrameWork.Management.Connection.Hospital.Name;
            this.neuSpread1_Sheet1.Cells[2, 4].Text = "机构编码:" + Models.Config.HospitalCode;

            string ybjsjb = "";
            ybjsjb = "二级";
            this.neuSpread1_Sheet1.Cells[2, 6].Text = "医保结算级别:" + ybjsjb;

            this.neuSpread1_Sheet1.Cells[3, 1].Text = "就医登记号:" + Mdtrt_id;
            this.neuSpread1_Sheet1.Cells[3, 4].Text = "结算号:" + Setl_id;

            this.neuSpread1_Sheet1.Cells[3, 6].Text = "";// "行政级别:";// +Models.Config.FixmedinsCode;


            this.neuSpread1_Sheet1.Cells[4, 1].Text = "姓名:" + patientInfoTemp.Name;
            this.neuSpread1_Sheet1.Cells[4, 2].Text = "性别:" + (patientInfoTemp.Sex.ID.ToString() == "F" ? "女" : "男");
            this.neuSpread1_Sheet1.Cells[4, 2].Text = "性别:" + (patientInfoTemp.Sex.ID.ToString() == "F" ? "女" : "男");
            this.neuSpread1_Sheet1.Cells[4, 3].Text = "出生日期:" + patientInfoTemp.Birthday.ToString("yyyy-MM-dd");
            this.neuSpread1_Sheet1.Cells[4, 4].Text = "个人电脑号:" + patientInfoTemp.SIMainInfo.Psn_no;

            string rylb = consMgr.GetConstant("GZSI_psn_type", patientInfoTemp.SIMainInfo.Psn_type).Name;
            this.neuSpread1_Sheet1.Cells[4, 6].Text = "人员类别:" + (string.IsNullOrEmpty(rylb) ? patientInfoTemp.SIMainInfo.Psn_type : rylb);
            // {DF92CB37-6922-46b2-BE54-436BBD826FD2}
            this.neuSpread1_Sheet1.Cells[5, 1].Text = "单位名称:" + patientInfoTemp.CompanyName; //patientInfo.CompanyName;
            this.neuSpread1_Sheet1.Cells[5, 3].Text = "联系电话:" + patientInfoTemp.PhoneHome;
            this.neuSpread1_Sheet1.Cells[5, 5].Text = "身份证号:" + patientInfoTemp.IDCard;

            if (patientInfoTemp.SIMainInfo.TypeCode == "1")
            {
                this.neuSpread1_Sheet1.Cells[6, 1].Text = "门诊号:" + patientInfoTemp.PID.CardNO.TrimStart('0');
                this.neuSpread1_Sheet1.Cells[6, 3].Text = "科别:" + patientInfoTemp.PVisit.PatientLocation.Dept.Name;
                this.neuSpread1_Sheet1.Cells[6, 6].Text = "住院天数:-";
                this.neuSpread1_Sheet1.Cells[7, 3].Text = "门诊诊断:" + localMgr.QueryICDByCode(patientInfoTemp.SIMainInfo.InDiagnose.ID).Name;
                this.neuSpread1_Sheet1.Cells[7, 5].Text = "";

            }
            else
            {
                this.neuSpread1_Sheet1.Cells[6, 1].Text = "住院号:" + patientInfoTemp.PID.PatientNO.TrimStart('0');
                this.neuSpread1_Sheet1.Cells[6, 3].Text = "科别:" + patientInfoTemp.PVisit.PatientLocation.Dept.Name;
                TimeSpan indays = patientInfoTemp.PVisit.OutTime.Date - patientInfoTemp.PVisit.InTime.Date;

                string days = "1";
                if (indays.Days <= 0)
                {
                    days = "1";
                }
                else
                {

                    days = indays.Days.ToString();
                }

                this.neuSpread1_Sheet1.Cells[6, 6].Text = "住院天数:" + days;
                this.neuSpread1_Sheet1.Cells[7, 3].Text = "入院第一诊断:" + localMgr.QueryICDByCode(patientInfoTemp.SIMainInfo.InDiagnose.ID).Name;
                this.neuSpread1_Sheet1.Cells[7, 5].Text = "出院第一诊断:" + localMgr.QueryICDByCode(patientInfoTemp.SIMainInfo.InDiagnose.ID).Name;


                this.neuSpread1_Sheet1.Cells[6, 2].Text = "床号:" + (patientInfoTemp.PVisit.PatientLocation.Bed.ID.Length >= 4 ? patientInfoTemp.PVisit.PatientLocation.Bed.ID.Substring(4, patientInfoTemp.PVisit.PatientLocation.Bed.ID.Length - 4) : patientInfoTemp.PVisit.PatientLocation.Bed.ID);
            }

            if (patientInfoTemp.SIMainInfo.TypeCode == "1")
            {
                this.neuSpread1_Sheet1.Cells[6, 4].Text = "看诊日期:" + patientInfoTemp.PVisit.InTime.ToString("yyyy-MM-dd");
                this.neuSpread1_Sheet1.Cells[6, 5].Text = "";
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[6, 4].Text = "入院日期:" + patientInfoTemp.PVisit.InTime.ToString("yyyy-MM-dd");
                this.neuSpread1_Sheet1.Cells[6, 5].Text = "出院日期:" + patientInfoTemp.PVisit.OutTime.ToString("yyyy-MM-dd");
            }


            string xz = consMgr.GetConstant("GZSI_insutype", patientInfoTemp.SIMainInfo.Insutype).Name;
            this.neuSpread1_Sheet1.Cells[7, 1].Text = "险种:" + (string.IsNullOrEmpty(xz) ? patientInfoTemp.SIMainInfo.Insutype : xz);

            this.neuSpread1_Sheet1.Cells[8, 1].Text = "业务类别:" + consMgr.GetConstant("GZSI_med_type", patientInfoTemp.SIMainInfo.Med_type).Name;

            this.neuSpread1_Sheet1.Cells[8, 5].Text = "结算时间:" + patientInfoTemp.SIMainInfo.Setl_time.ToString("yyyy-MM-dd HH:mm:ss");

            //先自付
            decimal xzfTot = patientInfoTemp.SIMainInfo.Ownpay_amt + patientInfoTemp.SIMainInfo.Preselfpay_amt + patientInfoTemp.SIMainInfo.Overlmt_selfpay;

            //段内自付
            decimal dnzfTot = patientInfoTemp.SIMainInfo.Medfee_sumamt - patientInfoTemp.SIMainInfo.Fund_pay_sumamt - patientInfoTemp.SIMainInfo.Ownpay_amt
                - patientInfoTemp.SIMainInfo.Overlmt_selfpay - patientInfoTemp.SIMainInfo.Preselfpay_amt;
            if (dnzfTot < 0)
            {
                dnzfTot = 0;
            }
            this.neuSpread1_Sheet1.Cells[9, 1].Text = @"本次就医：总费用" + patientInfoTemp.SIMainInfo.Medfee_sumamt.ToString("F2") + "元，基金支付" +
                patientInfoTemp.SIMainInfo.Fund_pay_sumamt.ToString("F2") + "元，个人支付" +
                (patientInfoTemp.SIMainInfo.Medfee_sumamt - patientInfoTemp.SIMainInfo.Fund_pay_sumamt).ToString("F2") + "元"
                + "（起付线" + patientInfoTemp.SIMainInfo.Act_pay_dedc.ToString("F2") + "元 先自负" + xzfTot.ToString("F2") + "元 段内自付" + dnzfTot.ToString("F2") + "元）";


            this.neuSpread1_Sheet1.Cells[21, 1].Text = "基本医疗保险统筹基金支付：" + patientInfoTemp.SIMainInfo.Hifp_pay.ToString("F2");
            this.neuSpread1_Sheet1.Cells[21, 5].Text = "大额基金支付：" + patientInfoTemp.SIMainInfo.Hifob_pay.ToString("F2");

            this.neuSpread1_Sheet1.Cells[23, 1].Text = "大病基金支付：" + patientInfoTemp.SIMainInfo.Hifmi_pay.ToString("F2");
            this.neuSpread1_Sheet1.Cells[23, 5].Text = "补充保险基金支付：" + patientInfoTemp.SIMainInfo.Hifes_pay.ToString("F2");
            this.neuSpread1_Sheet1.Cells[24, 1].Text = "医疗救助基金支付：" + patientInfoTemp.SIMainInfo.Maf_pay.ToString("F2");
            this.neuSpread1_Sheet1.Cells[24, 5].Text = "公务员基金支付：" + patientInfoTemp.SIMainInfo.Cvlserv_pay.ToString("F2");
            this.neuSpread1_Sheet1.Cells[25, 1].Text = "其他基金支付：" + patientInfoTemp.SIMainInfo.Oth_pay.ToString("F2");
            this.neuSpread1_Sheet1.Cells[25, 5].Text = "个人账户支付：" + patientInfoTemp.SIMainInfo.Acct_pay.ToString("F2");

            //TNND，说要改后面又不改
            this.neuSpread1_Sheet1.Rows.Get(26).Height = 0F;
            this.neuSpread1_Sheet1.Rows.Get(28).Height = 0F;
            this.neuSpread1_Sheet1.Rows.Get(29).Height = 0F;

            this.neuSpread1_Sheet1.Cells[26, 1].Text = "核报金额：" + "0.00";
            this.neuSpread1_Sheet1.Cells[26, 5].Text = "当年度特定门诊累计统筹基金支付余额：" + "0.00";
            this.neuSpread1_Sheet1.Cells[27, 1].Text = "";
            this.neuSpread1_Sheet1.Cells[27, 5].Text = "";
            this.neuSpread1_Sheet1.Cells[28, 1].Text = "当年度统筹基金支付余额：" + "0.00";
            this.neuSpread1_Sheet1.Cells[28, 5].Text = "累计大病保险核报基数：" + "0.00";
            this.neuSpread1_Sheet1.Cells[29, 1].Text = "纳入大病保险金额：" + "0.00";
            this.neuSpread1_Sheet1.Cells[29, 5].Text = "";



            this.neuSpread1_Sheet1.Cells[31, 5].Text = "制单人：" + FS.FrameWork.Management.Connection.Operator.Name;
            this.neuSpread1_Sheet1.Cells[31, 7].Text = "打印日期：" + DateTime.Now.ToString();


        }
        /// <summary>
        /// 本地设置费用明细
        /// </summary>
        private void LocalSetFeeDetail(string Mdtrt_id, string Setl_id)
        {


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public int ShowInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (SetValue(patientInfo) < 0)
            {
                MessageBox.Show("赋值失败!");
                return -1;
            }
            return 1;
        }

        public int PrintInBalanceInfo(int left, int top)
        {
            try
            {
                //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                //FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                //FS.HISFC.Models.Base.PageSize ps = pgMgr.GetPageSize("InPatientZHJSD");  //住院珠海医保结算单
                //if (ps == null)
                //{
                //    //默认大小
                //    ps = new FS.HISFC.Models.Base.PageSize("InPatientZHJSD", 830, 480);
                //}
                //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //print.SetPageSize(ps);

                //if ((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager)
                //{
                //    //print.PrintPreview(ps.Left, ps.Top, this);
                //    print.PrintPage(ps.Left, ps.Top, neuPanel1);
                //}
                //else
                //{
                //    print.PrintPage(ps.Left, ps.Top, neuPanel1);
                //}

                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize prSize = pgMgr.GetPageSize("InPatientGDJSD");//InPatientGDJSD
                if (prSize != null)
                {
                    FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize("MZGuide", prSize.Width, prSize.Height);
                    if (!string.IsNullOrEmpty(prSize.Printer))
                    {
                        print.PrintDocument.PrinterSettings.PrinterName = prSize.Printer;
                    }
                    print.SetPageSize(pSize);

                }
                else
                {
                }
                print.IsLandScape = true;//横向打印

                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    return print.PrintPreview(0, 0, this);
                }
                else
                {
                    return print.PrintPage(0, 0, this);
                }

                //FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize("MZGuide", prSize.Width, prSize.Height);
                //print.SetPageSize(pSize);
                ////获得打印机名
                ////print.PrintDocument.PrinterSettings.PrinterName = "InPatientGDJSD";
                ////if (true)
                ////{
                //return print.PrintPreview(left, top, this);
                //}
                ////return print.PrintPage(left, top, this);
            }
            catch
            {
                MessageBox.Show("打印发生错误,请确认是否有连接好打印机");
                return -1;
            }
            return 1;
        }
    }
}
