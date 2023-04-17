using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace API.GZSI.UI
{
    public partial class ucBalanceInfomation : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        int returnvalue = 0;
        string SerialNumber = string.Empty;//交易流水号
        string strTransVersion = string.Empty;//交易版本号
        string strVerifyCode = string.Empty;//交易验证码

        public ucBalanceInfomation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 患者入出转转业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        private LocalManager localMgr = new LocalManager();

        public int SetValue(FS.HISFC.Models.Registration.Register reg)
        {
            
            try
            {
                if (String.IsNullOrEmpty(reg.SIMainInfo.Psn_no))
                {
                    MessageBox.Show("未查询到患者人员编号！");
                    return -1;
                }

                if (String.IsNullOrEmpty(reg.SIMainInfo.Mdtrt_id))
                {
                    MessageBox.Show("未查询到患者就诊ID！");
                    return -1;
                }

                if (String.IsNullOrEmpty(reg.SIMainInfo.Setl_id))
                {
                    MessageBox.Show("未查询到患者结算ID！");
                    return -1;
                }

                API.GZSI.Business.Patient5203 patient5203 = new API.GZSI.Business.Patient5203();
                Models.Request.RequestGzsiModel5203 requestGzsiModel5203 = new Models.Request.RequestGzsiModel5203();
                Models.Response.ResponseGzsiModel5203 responseGzsiModel5203 = new Models.Response.ResponseGzsiModel5203();
                Models.Request.RequestGzsiModel5203.Data data5203 = new Models.Request.RequestGzsiModel5203.Data();
                data5203.psn_no = reg.SIMainInfo.Psn_no;
                data5203.setl_id = reg.SIMainInfo.Setl_id;
                data5203.mdtrt_id = reg.SIMainInfo.Mdtrt_id;

                requestGzsiModel5203.data = data5203;

                returnvalue = patient5203.CallService(requestGzsiModel5203, ref responseGzsiModel5203, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    MessageBox.Show("人员结算信息查询失败！" + patient5203.ErrorMsg);
                    return -1;
                }

                this.neuSpread1_Sheet1.Cells[2, 1].Text = "机构名称:" + FS.FrameWork.Management.Connection.Hospital.Name;
                this.neuSpread1_Sheet1.Cells[2, 4].Text = "机构编码:" + Models.UserInfo.Instance.userId;
                this.neuSpread1_Sheet1.Cells[2, 6].Text = "医保结算级别:" + consMgr.GetConstant("GZSI_hosp_lv", responseGzsiModel5203.output.setlinfo.hosp_lv).Name; 
                this.neuSpread1_Sheet1.Cells[3, 1].Text = "就医登记号:" + responseGzsiModel5203.output.setlinfo.mdtrt_id + "   结算号:" + responseGzsiModel5203.output.setlinfo.setl_id;

                this.neuSpread1_Sheet1.Cells[4, 1].Text = "姓名:" + responseGzsiModel5203.output.setlinfo.psn_name;
                this.neuSpread1_Sheet1.Cells[4, 2].Text = "性别:" + (responseGzsiModel5203.output.setlinfo.gend == "2" ? "女" : "男");
                this.neuSpread1_Sheet1.Cells[4, 3].Text = "出生日期:" + responseGzsiModel5203.output.setlinfo.brdy;
                this.neuSpread1_Sheet1.Cells[4, 4].Text = "个人电脑号:" + responseGzsiModel5203.output.setlinfo.psn_no;
                this.neuSpread1_Sheet1.Cells[4, 6].Text = "人员类别:" + consMgr.GetConstant("GZSI_psn_type", responseGzsiModel5203.output.setlinfo.psn_type).Name;
                this.neuSpread1_Sheet1.Cells[5, 1].Text = "单位名称:" + reg.CompanyName;
                this.neuSpread1_Sheet1.Cells[5, 3].Text = "联系电话:" + reg.PhoneHome;
                this.neuSpread1_Sheet1.Cells[5, 5].Text = "身份证号:" + responseGzsiModel5203.output.setlinfo.certno;

                if (reg.SIMainInfo.TypeCode == "1")
                {
                    this.neuSpread1_Sheet1.Cells[6, 1].Text = "门诊号:" + reg.PID.CardNO;
                    this.neuSpread1_Sheet1.Cells[6, 3].Text = "科别:" + reg.DoctorInfo.Templet.Dept.Name;
                    this.neuSpread1_Sheet1.Cells[6, 6].Text = "住院天数:-";
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[6, 1].Text = "住院号:" + reg.PID.PatientNO;
                    this.neuSpread1_Sheet1.Cells[6, 3].Text = "科别:" + reg.PVisit.PatientLocation.Dept.Name;
                    TimeSpan indays = DateTime.Parse(responseGzsiModel5203.output.setlinfo.enddate) - DateTime.Parse(responseGzsiModel5203.output.setlinfo.begndate);
                    this.neuSpread1_Sheet1.Cells[6, 6].Text = "住院天数:" + indays.Days;

                    this.neuSpread1_Sheet1.Cells[6, 2].Text = "床号:" + (reg.PVisit.PatientLocation.Bed.ID.Length >= 4 ? reg.PVisit.PatientLocation.Bed.ID.Substring(4, reg.PVisit.PatientLocation.Bed.ID.Length - 4) : reg.PVisit.PatientLocation.Bed.ID);
                }

                this.neuSpread1_Sheet1.Cells[6, 4].Text = "入院日期:" + responseGzsiModel5203.output.setlinfo.begndate;
                this.neuSpread1_Sheet1.Cells[6, 5].Text = "出院日期:" + responseGzsiModel5203.output.setlinfo.enddate;


                this.neuSpread1_Sheet1.Cells[7, 1].Text = "险种:" + consMgr.GetConstant("GZSI_insutype", responseGzsiModel5203.output.setlinfo.insutype).Name;

                if (reg.SIMainInfo.TypeCode == "1")
                {
                    List<Models.Request.RequestGzsiModel2203.Diseinfo> diaList = localMgr.DiagnoseList(reg.ID);

                    //this.neuSpread1_Sheet1.Cells[7, 3].Text = "入院第一诊断:" + localMgr.QueryICDByCode(reg.SIMainInfo.InDiagnose.ID).Name;
                    //this.neuSpread1_Sheet1.Cells[7, 5].Text = "出院第一诊断:" + localMgr.QueryICDByCode(reg.SIMainInfo.InDiagnose.ID).Name;
                    this.neuSpread1_Sheet1.Cells[7, 3].Text = "入院第一诊断:" + diaList[0].diag_name;
                    this.neuSpread1_Sheet1.Cells[7, 5].Text = "出院第一诊断:" + diaList[0].diag_name;
                }
                else
                {
                    //ArrayList Diagnoses = this.localMgr.InpatientClinicDiagnoseList(reg.ID);
                    //this.neuSpread1_Sheet1.Cells[7, 3].Text = "入院第一诊断:" + ((FS.HISFC.Models.RADT.Diagnose)Diagnoses[0]).Name;
                    //this.neuSpread1_Sheet1.Cells[7, 5].Text = "出院第一诊断:" + ((FS.HISFC.Models.RADT.Diagnose)Diagnoses[0]).Name;
                    #region 诊断// {1624908D-0C01-4E1C-A7FB-445DE755E85E}
                    if(true)//if (responseGzsiModel5203.output.setlinfo.med_type == "21")
                    {
                        #region  普通住院
                        //医保登记
                        ArrayList Diagnoses = null;// this.localMgr.InpatientSIDiagnoseList(reg.ID);
                        if (Diagnoses == null || Diagnoses.Count <= 0)
                        {
                            //电子病历
                            Diagnoses = this.localMgr.InpatientEMRDiagnoseList(reg.ID,"");
                        }

                        if (Diagnoses == null || Diagnoses.Count <= 0)
                        {
                            //门诊
                            Diagnoses = this.localMgr.InpatientClinicDiagnoseList(reg.ID);//没有电子病历时，只能拿门诊诊断了
                        }

                        FS.HISFC.Models.RADT.Diagnose inDiag = new FS.HISFC.Models.RADT.Diagnose();
                        FS.HISFC.Models.RADT.Diagnose outDiag = new FS.HISFC.Models.RADT.Diagnose();
                        foreach (FS.HISFC.Models.RADT.Diagnose diag in Diagnoses) {
                            if (diag.isMain) {
                                if (diag.Type.ID == "1") inDiag = diag;
                                else outDiag = diag;
                            }
                        }

                        this.neuSpread1_Sheet1.Cells[7, 3].Text = "入院第一诊断：" + inDiag.Name;
                        this.neuSpread1_Sheet1.Cells[7, 3].Text = "出院第一诊断：" + outDiag.Name;
                        #endregion
                    }
                    else
                    {
                        #region 生育住院
                        this.neuSpread1_Sheet1.Cells[7, 3].Text = "入院第一诊断：" + responseGzsiModel5203.output.setlinfo.dise_name;
                        this.neuSpread1_Sheet1.Cells[7, 3].Text = "出院第一诊断：" + responseGzsiModel5203.output.setlinfo.dise_name;
                        #endregion
                    }
                    #endregion

                }

                this.neuSpread1_Sheet1.Cells[8, 1].Text = "业务类别:" + consMgr.GetConstant("GZSI_med_type", responseGzsiModel5203.output.setlinfo.med_type).Name;

                this.neuSpread1_Sheet1.Cells[8, 5].Text = "结算时间:" + responseGzsiModel5203.output.setlinfo.setl_time;

                this.neuSpread1_Sheet1.Cells[9, 1].Text = @"本次就医：总费用" + responseGzsiModel5203.output.setlinfo.medfee_sumamt + "元，基金支付" +
                    responseGzsiModel5203.output.setlinfo.fund_pay_sumamt + "元，个人支付" +
                    (decimal.Parse(responseGzsiModel5203.output.setlinfo.medfee_sumamt) - decimal.Parse(responseGzsiModel5203.output.setlinfo.fund_pay_sumamt)).ToString() + "元"
                    +"（起付线"+ responseGzsiModel5203.output.setlinfo.act_pay_dedc +"元 先自负"+ (decimal.Parse(responseGzsiModel5203.output.setlinfo.fulamt_ownpay_amt)
                    + decimal.Parse(responseGzsiModel5203.output.setlinfo.overlmt_selfpay) + decimal.Parse(responseGzsiModel5203.output.setlinfo.preselfpay_amt)).ToString() + "元 段内自付" 
                    + (decimal.Parse(responseGzsiModel5203.output.setlinfo.medfee_sumamt) - decimal.Parse(responseGzsiModel5203.output.setlinfo.fund_pay_sumamt) - 
                    decimal.Parse(responseGzsiModel5203.output.setlinfo.fulamt_ownpay_amt) - decimal.Parse(responseGzsiModel5203.output.setlinfo.overlmt_selfpay) -
                    decimal.Parse(responseGzsiModel5203.output.setlinfo.preselfpay_amt)).ToString() + "元）";


                this.neuSpread1_Sheet1.Cells[21, 1].Text = "基本医疗保险统筹基金支付：" + responseGzsiModel5203.output.setlinfo.hifp_pay;
                this.neuSpread1_Sheet1.Cells[21, 5].Text = "大额基金支付：" + responseGzsiModel5203.output.setlinfo.hifob_pay;

                this.neuSpread1_Sheet1.Cells[23, 1].Text = "大病基金支付：" + responseGzsiModel5203.output.setlinfo.hifmi_pay;
                this.neuSpread1_Sheet1.Cells[23, 5].Text = "补充保险基金支付：" + responseGzsiModel5203.output.setlinfo.hifes_pay;
                this.neuSpread1_Sheet1.Cells[24, 1].Text = "医疗救助基金支付：" + responseGzsiModel5203.output.setlinfo.maf_pay;
                this.neuSpread1_Sheet1.Cells[24, 5].Text = "公务员基金支付：" + responseGzsiModel5203.output.setlinfo.cvlserv_pay;
                this.neuSpread1_Sheet1.Cells[25, 1].Text = "其他基金支付：" + responseGzsiModel5203.output.setlinfo.oth_pay;
                this.neuSpread1_Sheet1.Cells[25, 5].Text = "个人账户支付：" + responseGzsiModel5203.output.setlinfo.acct_pay;

                this.neuSpread1_Sheet1.Cells[26, 5].Text = "制单人：" + FS.FrameWork.Management.Connection.Operator.Name;
                this.neuSpread1_Sheet1.Cells[26, 7].Text = "打印日期：" + DateTime.Now.ToString();

                //this.neuSpread1_Sheet1.Cells[28, 1].Text = @"注：
                //  1、部分项目自付金额是指基本医疗保险范围内的项目需参保人先按规定比例自付的金额。
                //  2、此表由医院打印。
                //  3、此表一式两份，医院、参保人各一份。 ";
                //this.neuSpread1_Sheet1.Cells[28, 1].Text = @"注：1、部分项目自付金额是指基本医疗保险范围内的项目需参保人先按规定比例自付的金额。";

                API.GZSI.Business.Patient5204 patient5204 = new API.GZSI.Business.Patient5204();
                Models.Request.RequestGzsiModel5204 requestGzsiModel5204 = new Models.Request.RequestGzsiModel5204();
                Models.Response.ResponseGzsiModel5204 responseGzsiModel5204 = new Models.Response.ResponseGzsiModel5204();
                Models.Request.RequestGzsiModel5204.Data data5204 = new Models.Request.RequestGzsiModel5204.Data();
                data5204.psn_no = reg.SIMainInfo.Psn_no;
                data5204.setl_id = reg.SIMainInfo.Setl_id;
                data5204.mdtrt_id = reg.SIMainInfo.Mdtrt_id;

                requestGzsiModel5204.data = data5204;

                returnvalue = patient5204.CallService(requestGzsiModel5204, ref responseGzsiModel5204, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    MessageBox.Show("人员结算明细信息查询失败！" + patient5204.ErrorMsg);
                    return -1;
                }

                foreach(Models.Response.ResponseGzsiModel5204.Output output in responseGzsiModel5204.output)
                {
                    //output.det_item_fee_sumamt //总金额
                    //output.overlmt_amt; //全自费
                    //output.preselfpay_amt; //部分自费

                    if (this.localMgr.UpdateSIfeeDetail(reg.SIMainInfo.Mdtrt_id,
                        reg.SIMainInfo.Setl_id,
                        output.feedetl_sn,
                        reg.SIMainInfo.TypeCode,
                        output.det_item_fee_sumamt,
                        output.overlmt_amt,
                        output.preselfpay_amt,
                        output.fulamt_ownpay_amt) < 0)
                    {
                        MessageBox.Show("更新结算明细信息失败！" + this.localMgr.Err);
                        return -1;
                    }
                }

                #region 费用明细

                ArrayList al = localMgr.GetFeeTypeDetail(reg.SIMainInfo.Mdtrt_id, reg.SIMainInfo.Setl_id, reg.SIMainInfo.TypeCode);
                ArrayList al2 = localMgr.GetOverlmtFeeTypeDetail(reg.SIMainInfo.Mdtrt_id, reg.SIMainInfo.Setl_id, reg.SIMainInfo.TypeCode);
                ArrayList al3 = localMgr.GetPreselfPayFeeTypeDetail(reg.SIMainInfo.Mdtrt_id, reg.SIMainInfo.Setl_id, reg.SIMainInfo.TypeCode);

                if (al == null || al.Count <= 0)
                {
                    MessageBox.Show("未找到结算明细：" + reg.SIMainInfo.InvoiceNo);
                    return -1;
                }

                decimal othFee = 0;
                decimal alTot = 0;
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    alTot+=decimal.Parse(obj.Memo);
                    if (obj.Name.Contains("西药"))
                    {
                        this.neuSpread1_Sheet1.Cells[11, 6].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("中成药"))
                    {
                        this.neuSpread1_Sheet1.Cells[12, 6].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("诊查"))
                    {
                        this.neuSpread1_Sheet1.Cells[13, 2].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("床位"))
                    {
                        this.neuSpread1_Sheet1.Cells[11, 2].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("中草") || obj.Name.Contains("草"))
                    {
                        this.neuSpread1_Sheet1.Cells[12, 2].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("治疗"))
                    {
                        this.neuSpread1_Sheet1.Cells[14, 2].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("检查"))
                    {
                        this.neuSpread1_Sheet1.Cells[13, 6].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("护理"))
                    {
                        this.neuSpread1_Sheet1.Cells[14, 6].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("手术"))
                    {
                        this.neuSpread1_Sheet1.Cells[15, 2].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("化验")||obj.Name.Contains("检验"))
                    {
                        this.neuSpread1_Sheet1.Cells[15, 6].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("材料"))
                    {
                        this.neuSpread1_Sheet1.Cells[16, 2].Text  = obj.Memo;
                    }
                    else if (obj.Name.Contains("挂号"))
                    {
                        this.neuSpread1_Sheet1.Cells[16, 6].Text  = obj.Memo;
                    }
                    else
                    {
                        othFee += decimal.Parse(obj.Memo);
                        this.neuSpread1_Sheet1.Cells[17, 2].Text  = othFee.ToString();
                    }
                }

                //自费信息
                decimal alTot2 = 0;
                othFee = 0;
                foreach (FS.FrameWork.Models.NeuObject obj in al2)
                {
                    alTot2 += decimal.Parse(obj.Memo);
                    if (obj.Name.Contains("西药"))
                    {
                        this.neuSpread1_Sheet1.Cells[11, 7].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("中成药"))
                    {
                        this.neuSpread1_Sheet1.Cells[12, 7].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("检查"))
                    {
                        this.neuSpread1_Sheet1.Cells[13, 7].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("护理"))
                    {
                        this.neuSpread1_Sheet1.Cells[14, 7].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("化验") || obj.Name.Contains("检验"))
                    {
                        this.neuSpread1_Sheet1.Cells[15, 7].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("挂号"))
                    {
                        this.neuSpread1_Sheet1.Cells[16, 7].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("床位"))
                    {
                        this.neuSpread1_Sheet1.Cells[11, 3].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("中草") || obj.Name.Contains("草"))
                    {
                        this.neuSpread1_Sheet1.Cells[12, 3].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("诊查") || obj.Name.Contains("诊察"))
                    {
                        this.neuSpread1_Sheet1.Cells[13, 3].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("治疗"))
                    {
                        this.neuSpread1_Sheet1.Cells[14, 3].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("手术"))
                    {
                        this.neuSpread1_Sheet1.Cells[15, 3].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("材料"))
                    {
                        this.neuSpread1_Sheet1.Cells[16, 3].Text = obj.Memo;
                    }
                    else
                    {
                        othFee += decimal.Parse(obj.Memo);
                        this.neuSpread1_Sheet1.Cells[17, 3].Text = othFee.ToString();
                    }
                }

                //部分自费
                decimal alTot3 = 0;
                othFee = 0;
                foreach (FS.FrameWork.Models.NeuObject obj in al3)
                {
                    alTot3 += decimal.Parse(obj.Memo);
                    if (obj.Name.Contains("西药"))
                    {
                        this.neuSpread1_Sheet1.Cells[11, 8].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("中成药"))
                    {
                        this.neuSpread1_Sheet1.Cells[12, 8].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("检查"))
                    {
                        this.neuSpread1_Sheet1.Cells[13, 8].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("护理"))
                    {
                        this.neuSpread1_Sheet1.Cells[14, 8].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("化验") || obj.Name.Contains("检验"))
                    {
                        this.neuSpread1_Sheet1.Cells[15, 8].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("挂号"))
                    {
                        this.neuSpread1_Sheet1.Cells[16, 8].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("床位"))
                    {
                        this.neuSpread1_Sheet1.Cells[11, 4].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("中草") || obj.Name.Contains("草"))
                    {
                        this.neuSpread1_Sheet1.Cells[12, 4].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("诊查") || obj.Name.Contains("诊察"))
                    {
                        this.neuSpread1_Sheet1.Cells[13, 4].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("治疗"))
                    {
                        this.neuSpread1_Sheet1.Cells[14, 4].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("手术"))
                    {
                        this.neuSpread1_Sheet1.Cells[15, 4].Text = obj.Memo;
                    }
                    else if (obj.Name.Contains("材料"))
                    {
                        this.neuSpread1_Sheet1.Cells[16, 4].Text = obj.Memo;
                    }
                    else
                    {
                        othFee += decimal.Parse(obj.Memo);
                        this.neuSpread1_Sheet1.Cells[17, 4].Text = othFee.ToString();
                    }
                }

                this.neuSpread1_Sheet1.Cells[20, 2].Text = alTot.ToString();
                this.neuSpread1_Sheet1.Cells[20, 3].Text = alTot2.ToString();
                this.neuSpread1_Sheet1.Cells[20, 4].Text = alTot3.ToString();


                #endregion

            }
            catch
            {
                return -1;
            }
            return 1;
        }

        public int Query(FS.HISFC.Models.Registration.Register reg,string typeCode)
        {
            if (reg == null)
            {
                MessageBox.Show("查找患者失败");
                return -1;
            }

            ArrayList invoiceList = this.localMgr.GetInvoiceList(reg, typeCode);

            if (invoiceList == null)
            {
                MessageBox.Show("查找结算记录失败：" + this.localMgr.Err);
                return -1;
            }

            if (invoiceList.Count == 0)
            {
                MessageBox.Show("未找到医保结算记录");
                return -1;
            }

            FS.HISFC.Models.Base.Const choose = new FS.HISFC.Models.Base.Const();

            if (invoiceList.Count > 1)
            {

                FS.FrameWork.WinForms.Forms.frmEasyChoose fc = new FS.FrameWork.WinForms.Forms.frmEasyChoose(invoiceList);
                string[] cols = { "就医登记号", "发票号", "结算序号", "日期" };
                bool[] visibles = { true, true, true, true };
                int[] widths = { 100, 120, 80, 120 };
                fc.SetFormat(cols, visibles, widths);
                fc.ShowDialog();

                choose = fc.Object as FS.HISFC.Models.Base.Const;
                if (choose == null)
                {
                    MessageBox.Show("请选择一条记录!");
                    return -1;
                }
            }
            else
            {
                choose = invoiceList[0] as FS.HISFC.Models.Base.Const;
            }

            reg.SIMainInfo.InvoiceNo = choose.Name;

            //reg = localMgr.GetOutPatientBalanceInfo(reg, typeCode);
            int rtn = localMgr.GetBalanceInfo(ref reg, typeCode);

            if (!reg.SIMainInfo.IsValid)
            {
                MessageBox.Show("结算无效!");
                return -1;
            }

            if (SetValue(reg) < 0)
            {
                MessageBox.Show("赋值失败!");
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public int ShowInfo(FS.HISFC.Models.Registration.Register reg)
        {
            if (SetValue(reg) < 0)
            {
                MessageBox.Show("赋值失败!");
                return -1;
            }
            return 1;
        }

        public int PrintInBalanceInfo(int left,int top)
        {
            try
            {
                //Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
                //Neusoft.HISFC.BizLogic.Manager.PageSize pgMgr = new Neusoft.HISFC.BizLogic.Manager.PageSize();
                //Neusoft.HISFC.Models.Base.PageSize ps = pgMgr.GetPageSize("InPatientZHJSD");  //住院珠海医保结算单
                //if (ps == null)
                //{
                //    //默认大小
                //    ps = new Neusoft.HISFC.Models.Base.PageSize("InPatientZHJSD", 830, 480);
                //}
                //print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
                //print.SetPageSize(ps);

                //if ((Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee).IsManager)
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
                FS.HISFC.Models.Base.PageSize prSize = pgMgr.GetPageSize("InPatientGDJSD");

                if (prSize == null || string.IsNullOrEmpty(prSize.Name))
                {
                    prSize = new FS.HISFC.Models.Base.PageSize();
                    prSize.Width = 900;
                    prSize.Height = 900;
                }

                FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize("SIBalancePrint", prSize.Width, prSize.Height);
                print.SetPageSize(pSize);
                return print.PrintPreview(left, top, this);
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
