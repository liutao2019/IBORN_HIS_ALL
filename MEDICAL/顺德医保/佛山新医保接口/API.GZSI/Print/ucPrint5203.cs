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
    public partial class ucPrint5203 : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 业务类

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        /// <summary>
        /// 患者入出转转业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        #endregion

        #region 属性

        private FS.HISFC.Models.RADT.Patient patient;
        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        #endregion

        #region 变量

        /// <summary>
        /// 公共返回值
        /// </summary>
        int returnvalue = 0;

        /// <summary>
        /// 交易流水号
        /// </summary>
        string SerialNumber = string.Empty;

        /// <summary>
        /// 交易版本号
        /// </summary>
        string strTransVersion = string.Empty;

        /// <summary>
        /// 交易验证码
        /// </summary>
        string strVerifyCode = string.Empty;

        #endregion

        public ucPrint5203()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置显示内容
        /// </summary>
        /// <param name="typeCode">实体类型 1-门诊，2-住院</param>
        /// <returns></returns>
        public int SetValue(string typeCode)
        {
            try
            {
                string psn_no = "";
                string setl_id = "";
                string mdtrt_id = "";
                FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                if (typeCode == "1")
                {
                    regInfo = this.patient as FS.HISFC.Models.Registration.Register;
                    psn_no = regInfo.SIMainInfo.Psn_no;
                    setl_id = regInfo.SIMainInfo.Setl_id;
                    mdtrt_id = regInfo.SIMainInfo.Mdtrt_id;
                }
                else if (typeCode == "2")
                {
                    patientInfo = this.patient as FS.HISFC.Models.RADT.PatientInfo;
                    psn_no = patientInfo.SIMainInfo.Psn_no;
                    setl_id = patientInfo.SIMainInfo.Setl_id;
                    mdtrt_id = patientInfo.SIMainInfo.Mdtrt_id;
                }
                else
                {
                    MessageBox.Show("人员结算信息查询失败，未知的患者信息！");
                    return -1;
                }

                //查询结算信息
                API.GZSI.Business.Patient5203 patient5203 = new API.GZSI.Business.Patient5203();
                Models.Request.RequestGzsiModel5203 requestGzsiModel5203 = new Models.Request.RequestGzsiModel5203();
                Models.Response.ResponseGzsiModel5203 responseGzsiModel5203 = new Models.Response.ResponseGzsiModel5203();
                Models.Request.RequestGzsiModel5203.Data data5203 = new Models.Request.RequestGzsiModel5203.Data();
                
                data5203.psn_no = psn_no;
                data5203.mdtrt_id = mdtrt_id;
                data5203.setl_id = setl_id;
                requestGzsiModel5203.data = data5203;
                returnvalue = patient5203.CallService(requestGzsiModel5203, ref responseGzsiModel5203, SerialNumber, strTransVersion, strVerifyCode);
                if (returnvalue == -1)
                {
                    MessageBox.Show("人员结算信息查询失败！" + patient5203.ErrorMsg);
                    return -1;
                }

                //基本信息
                //第2行
                this.fpSpread5203_Sheet1.Cells[1, 0].Text = "机构名称：" + FS.FrameWork.Management.Connection.Hospital.Name;
                this.fpSpread5203_Sheet1.Cells[1, 3].Text = "机构编码：" + Models.UserInfo.Instance.userId;
                this.fpSpread5203_Sheet1.Cells[1, 5].Text = "医保结算级别：二级"; //+ consMgr.GetConstant("GZSI_hosp_lv", responseGzsiModel5203.output.setlinfo.hosp_lv).Name; 
                //第3行
                this.fpSpread5203_Sheet1.Cells[2, 0].Text = "就医登记号：" + responseGzsiModel5203.output.setlinfo.mdtrt_id + "   结算号:" + responseGzsiModel5203.output.setlinfo.setl_id;
                //第4行
                this.fpSpread5203_Sheet1.Cells[3, 0].Text = "姓名：" + responseGzsiModel5203.output.setlinfo.psn_name;
                this.fpSpread5203_Sheet1.Cells[3, 1].Text = "性别：" + (responseGzsiModel5203.output.setlinfo.gend == "2" ? "女" : "男");
                this.fpSpread5203_Sheet1.Cells[3, 2].Text = "出生日期：" + responseGzsiModel5203.output.setlinfo.brdy;
                this.fpSpread5203_Sheet1.Cells[3, 4].Text = "个人电脑号：" + responseGzsiModel5203.output.setlinfo.psn_no;
                this.fpSpread5203_Sheet1.Cells[3, 6].Text = "人员类别：" + consMgr.GetConstant("GZSI_psn_type", responseGzsiModel5203.output.setlinfo.psn_type).Name;
                //第5行
                this.fpSpread5203_Sheet1.Cells[4, 0].Text = "单位名称：" + this.Patient.CompanyName;
                this.fpSpread5203_Sheet1.Cells[4, 2].Text = "联系电话：" + this.Patient.PhoneHome;
                this.fpSpread5203_Sheet1.Cells[4, 4].Text = "身份证号：" + responseGzsiModel5203.output.setlinfo.certno;

                if (typeCode == "1")
                {
                    //第6行
                    this.fpSpread5203_Sheet1.Cells[5, 0].Text = "门诊号：" + regInfo.PID.CardNO;
                    this.fpSpread5203_Sheet1.Cells[5, 1].Text = "床号：-";
                    this.fpSpread5203_Sheet1.Cells[5, 2].Text = "科别：" + regInfo.DoctorInfo.Templet.Dept.Name;
                    this.fpSpread5203_Sheet1.Cells[5, 3].Text = "入院日期：" + responseGzsiModel5203.output.setlinfo.begndate;
                    this.fpSpread5203_Sheet1.Cells[5, 5].Text = "出院日期：" + responseGzsiModel5203.output.setlinfo.enddate;
                    this.fpSpread5203_Sheet1.Cells[5, 7].Text = "住院天数：-";
                    //第7行
                    List<Models.Request.RequestGzsiModel2203.Diseinfo> diaList = localMgr.DiagnoseList(regInfo.ID);

                    if (regInfo.SIMainInfo.Dise_code == "T01003" || regInfo.SIMainInfo.Dise_code == "XGJC002")
                    {
                        if (diaList.Count == 0)
                        {
                            Models.Request.RequestGzsiModel2203.Diseinfo diseinfo = new Models.Request.RequestGzsiModel2203.Diseinfo();
                            diseinfo.diag_type = "1";//诊断类别 
                            diseinfo.diag_srt_no = "1";//诊断排序号 
                            diseinfo.diag_code = "Z11.500";//诊断代码 
                            diseinfo.diag_name = "病毒性其他疾病的特殊筛查";//诊断名称 
                            diseinfo.diag_dept = regInfo.DoctorInfo.Templet.Dept.ID;// this.localMgr.getDeptYBCode(regInfo.DoctorInfo.Templet.Dept.ID);//诊断科室 
                            diseinfo.dise_dor_no = this.localMgr.GetGDDoct(regInfo.DoctorInfo.Templet.Doct.ID);//诊断医生编码 
                            diseinfo.dise_dor_name = regInfo.DoctorInfo.Templet.Doct.Name;//诊断医生姓名 
                            diseinfo.diag_time = regInfo.DoctorInfo.SeeDate.ToString();//诊断时间 
                            diseinfo.maindiag_flag = "1";
                            diseinfo.vali_flag = "1";//有效标志 
                            diaList.Add(diseinfo);
                        }
                    }

                    this.fpSpread5203_Sheet1.Cells[6, 0].Text = "险种：" + consMgr.GetConstant("GZSI_insutype", responseGzsiModel5203.output.setlinfo.insutype).Name;
                    this.fpSpread5203_Sheet1.Cells[6, 2].Text = "入院第一诊断：" + diaList[0].diag_name;
                    this.fpSpread5203_Sheet1.Cells[6, 5].Text = "出院第一诊断：" + diaList[0].diag_name;
                }
                else
                {
                    TimeSpan indays = DateTime.Parse(responseGzsiModel5203.output.setlinfo.enddate) - DateTime.Parse(responseGzsiModel5203.output.setlinfo.begndate);
                    //第6行
                    this.fpSpread5203_Sheet1.Cells[5, 0].Text = "住院号：" + patientInfo.PID.PatientNO;
                    this.fpSpread5203_Sheet1.Cells[5, 1].Text = "床号：" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? 4 : 0);
                    this.fpSpread5203_Sheet1.Cells[5, 2].Text = "科别：" + patientInfo.PVisit.PatientLocation.Dept.Name;
                    this.fpSpread5203_Sheet1.Cells[5, 3].Text = "入院日期：" + responseGzsiModel5203.output.setlinfo.begndate;
                    this.fpSpread5203_Sheet1.Cells[5, 5].Text = "出院日期：" + responseGzsiModel5203.output.setlinfo.enddate;
                    this.fpSpread5203_Sheet1.Cells[5, 7].Text = "住院天数：" + indays.Days + "天";
                    //第7行
                    //ArrayList Diagnoses = this.localMgr.InpatientClinicDiagnoseList(patientInfo.ID);
                    //this.fpSpread5203_Sheet1.Cells[6, 2].Text = "入院第一诊断：" + ((FS.HISFC.Models.RADT.Diagnose)Diagnoses[0]).Name;
                    //this.fpSpread5203_Sheet1.Cells[6, 5].Text = "出院第一诊断：" + ((FS.HISFC.Models.RADT.Diagnose)Diagnoses[0]).ICD10.Name;

                    #region 诊断// {1624908D-0C01-4E1C-A7FB-445DE755E85E}
                    if(true)//if (responseGzsiModel5203.output.setlinfo.med_type == "21")
                    {
                        #region  普通住院
                        //医保登记诊断
                        ArrayList Diagnoses = null;// this.localMgr.InpatientSIDiagnoseList(patientInfo.ID);
                        if (Diagnoses == null || Diagnoses.Count <= 0)
                        {
                            //电子病历
                            Diagnoses = this.localMgr.InpatientEMRDiagnoseList(patientInfo.ID,"");
                        }

                        if (Diagnoses == null || Diagnoses.Count <= 0)
                        {
                            //门诊诊断
                            Diagnoses = this.localMgr.InpatientClinicDiagnoseList(patientInfo.ID);//没有电子病历时，只能拿门诊诊断了
                        }

                        FS.HISFC.Models.RADT.Diagnose inDiag = new FS.HISFC.Models.RADT.Diagnose();
                        FS.HISFC.Models.RADT.Diagnose outDiag = new FS.HISFC.Models.RADT.Diagnose();
                        foreach (FS.HISFC.Models.RADT.Diagnose diag in Diagnoses)
                        {
                            if (diag.isMain)
                            {
                                if (diag.Type.ID == "1") inDiag = diag;
                                else outDiag = diag;
                            }
                        }

                        this.fpSpread5203_Sheet1.Cells[6, 2].Text = "入院第一诊断：" + inDiag.Name;
                        this.fpSpread5203_Sheet1.Cells[6, 5].Text = "出院第一诊断：" + outDiag.Name;
                        #endregion
                    }
                    else {
                        #region 生育住院
                        //获取登记病种
                        this.fpSpread5203_Sheet1.Cells[6, 2].Text = "入院第一诊断：" + responseGzsiModel5203.output.setlinfo.dise_name;
                        this.fpSpread5203_Sheet1.Cells[6, 5].Text = "出院第一诊断：" + responseGzsiModel5203.output.setlinfo.dise_name;
                        #endregion
                    }
                    #endregion

                    this.fpSpread5203_Sheet1.Cells[6, 0].Text = "险种：" + consMgr.GetConstant("GZSI_insutype", responseGzsiModel5203.output.setlinfo.insutype).Name;
                }

                //第8行
                this.fpSpread5203_Sheet1.Cells[7, 0].Text = "业务类别：" + consMgr.GetConstant("GZSI_med_type", responseGzsiModel5203.output.setlinfo.med_type).Name;
                this.fpSpread5203_Sheet1.Cells[7, 4].Text = "结算时间：" + responseGzsiModel5203.output.setlinfo.setl_time;
                //第9行
                this.fpSpread5203_Sheet1.Cells[8, 0].Text = @"本次就医：" 
                    + "总费用" + responseGzsiModel5203.output.setlinfo.medfee_sumamt + "元，"
                    + "基金支付" + responseGzsiModel5203.output.setlinfo.fund_pay_sumamt + "元，"
                    + "个人支付" + (decimal.Parse(responseGzsiModel5203.output.setlinfo.medfee_sumamt) - decimal.Parse(responseGzsiModel5203.output.setlinfo.fund_pay_sumamt)).ToString() + "元"
                    + "（起付线"+ responseGzsiModel5203.output.setlinfo.act_pay_dedc +"元 "
                    + "先自负"+ (decimal.Parse(responseGzsiModel5203.output.setlinfo.fulamt_ownpay_amt) 
                               + decimal.Parse(responseGzsiModel5203.output.setlinfo.overlmt_selfpay) 
                               + decimal.Parse(responseGzsiModel5203.output.setlinfo.preselfpay_amt)).ToString() + "元 "
                    + "段内自付" + (decimal.Parse(responseGzsiModel5203.output.setlinfo.medfee_sumamt) 
                                  - decimal.Parse(responseGzsiModel5203.output.setlinfo.fund_pay_sumamt) 
                                  - decimal.Parse(responseGzsiModel5203.output.setlinfo.fulamt_ownpay_amt) 
                                  - decimal.Parse(responseGzsiModel5203.output.setlinfo.overlmt_selfpay)
                                  - decimal.Parse(responseGzsiModel5203.output.setlinfo.preselfpay_amt)).ToString() + "元）";

                //第19行
                this.fpSpread5203_Sheet1.Cells[18, 0].Text = "基本医疗保险统筹基金支付：" + responseGzsiModel5203.output.setlinfo.hifp_pay;
                this.fpSpread5203_Sheet1.Cells[18, 4].Text = "大额基金支付：" + responseGzsiModel5203.output.setlinfo.hifob_pay;
                //第20行
                this.fpSpread5203_Sheet1.Cells[19, 0].Text = "大病基金支付：" + responseGzsiModel5203.output.setlinfo.hifmi_pay;
                this.fpSpread5203_Sheet1.Cells[19, 4].Text = "平安佛医保理赔金额：" + responseGzsiModel5203.output.setlinfo.hifes_pay;
                //第21行
                this.fpSpread5203_Sheet1.Cells[20, 0].Text = "医疗救助基金支付：" + responseGzsiModel5203.output.setlinfo.maf_pay;
                this.fpSpread5203_Sheet1.Cells[20, 4].Text = "公务员基金支付：" + responseGzsiModel5203.output.setlinfo.cvlserv_pay;
                //第22行
                this.fpSpread5203_Sheet1.Cells[21, 0].Text = "其他基金支付：" + responseGzsiModel5203.output.setlinfo.oth_pay;
                this.fpSpread5203_Sheet1.Cells[21, 4].Text = "个人账户支付：" + responseGzsiModel5203.output.setlinfo.acct_pay;
                //第23行
                this.fpSpread5203_Sheet1.Cells[22, 4].Text = "制单人：" + FS.FrameWork.Management.Connection.Operator.Name;
                this.fpSpread5203_Sheet1.Cells[22, 6].Text = "打印日期：" + DateTime.Now.ToString();
                //第26行 
                this.fpSpread5203_Sheet1.Cells[26, 5].Text = "打印日期：" + DateTime.Now.ToString();

                //查询结算明细信息
                API.GZSI.Business.Patient5204 patient5204 = new API.GZSI.Business.Patient5204();
                Models.Request.RequestGzsiModel5204 requestGzsiModel5204 = new Models.Request.RequestGzsiModel5204();
                Models.Response.ResponseGzsiModel5204 responseGzsiModel5204 = new Models.Response.ResponseGzsiModel5204();
                Models.Request.RequestGzsiModel5204.Data data5204 = new Models.Request.RequestGzsiModel5204.Data();

                data5204.psn_no = psn_no;
                data5204.setl_id = setl_id;
                data5204.mdtrt_id = mdtrt_id;
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
                    if (this.localMgr.UpdateSIfeeDetail(mdtrt_id,
                        setl_id,
                        output.feedetl_sn,
                        typeCode,
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

                ArrayList detItemFeeSumamtList = localMgr.GetFeeTypeDetail(mdtrt_id, setl_id, typeCode);     //总费用
                ArrayList overLmtList = localMgr.GetOverlmtFeeTypeDetail(mdtrt_id, setl_id, typeCode);       //自费
                ArrayList preselfPayList = localMgr.GetPreselfPayFeeTypeDetail(mdtrt_id, setl_id, typeCode); //部分项目自付

                if (detItemFeeSumamtList == null || detItemFeeSumamtList.Count <= 0)
                {
                    MessageBox.Show("未找到结算明细：" + setl_id);
                    return -1;
                }

                decimal detItemFeeSumamtTot = 0;
                decimal overLmtTot = 0;
                decimal preselfPayTot = 0;

                #region 总金额
                foreach (FS.FrameWork.Models.NeuObject obj in detItemFeeSumamtList)
                {
                    try
                    {
                        detItemFeeSumamtTot += decimal.Parse(obj.Memo);
                        this.fpSpread5203_Sheet1.Cells[obj.Name + "1"].Text = obj.Memo;
                    }
                    catch
                    {
                        MessageBox.Show("显示费用明细失败：未找到【" + obj.Name + " 总金额】的费用类别！");
                    }
                }
                #endregion

                #region 自费金额
                foreach (FS.FrameWork.Models.NeuObject obj in overLmtList)
                {
                    try
                    {
                        overLmtTot += decimal.Parse(obj.Memo);
                        this.fpSpread5203_Sheet1.Cells[obj.Name + "2"].Text = obj.Memo;
                    }
                    catch
                    {
                        MessageBox.Show("显示费用明细失败：未找到【" + obj.Name + " 自费金额】的费用类别！");
                    }
                }
                #endregion

                #region 部分项目自付金额
                foreach (FS.FrameWork.Models.NeuObject obj in preselfPayList)
                {
                    try
                    {
                        preselfPayTot += decimal.Parse(obj.Memo);
                        this.fpSpread5203_Sheet1.Cells[obj.Name + "3"].Text = obj.Memo;
                    }
                    catch
                    {
                        MessageBox.Show("显示费用明细失败：未找到【" + obj.Name + " 自费金额】的费用类别！");
                    }
                }
                #endregion 

                this.fpSpread5203_Sheet1.Cells[17, 1].Text = detItemFeeSumamtTot.ToString();
                this.fpSpread5203_Sheet1.Cells[17, 2].Text = overLmtTot.ToString();
                this.fpSpread5203_Sheet1.Cells[17, 3].Text = preselfPayTot.ToString();

                #endregion
            }
            catch
            {
                return -1;
            }
            return 1;
        }

        public int Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                //FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("A4");
                FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize("5203", 850, 1102);
            
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.SetPageSize(pSize);

                string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
                if (string.IsNullOrEmpty(printerName)) return 1;
                print.PrintDocument.PrinterSettings.PrinterName = printerName;

                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(0, 10, this);
                }
                else
                {
                    print.PrintPage(0, 10, this);
                }
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
