using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using API.GZSI.Business;
using API.GZSI.Models.Response;

namespace API.GZSI.UI
{
    public partial class ucPatientService : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 业务类

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 入出转业务层
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 挂号业务层
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        #endregion 

        #region 属性

        private FS.HISFC.Models.RADT.PatientInfo patientInfo;
        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
          get { return patientInfo; }
          set { patientInfo = value; }
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

        public ucPatientService()
        {
            InitializeComponent();
            InitControls();
            InitEvents();
        }

        private void InitControls()
        {
            //5201就诊信息相关事件
            this.cmbMedType.AddItems(constMgr.GetAllList("GZSI_med_type"));
            this.cmbMedType.SelectedIndex = 0;
            this.dtBeginTime.Value = DateTime.Now.Date.AddMonths(-1);
            this.dtEndTime.Value = DateTime.Now.Date;

            //5302人员定点备案相关
            this.cmbApplyType.AddItems(constMgr.GetAllList("GZSI_biz_appy_type"));
            this.cmbApplyType.SelectedIndex = 0;

            //5304转院相关
            this.dtpBegin5304.Value = DateTime.Now.Date.AddMonths(-1);
            this.dtpEnd5304.Value = DateTime.Now.Date;


            //2501A
            this.cmbZYFixType.AddItems(constMgr.GetAllList("GZSI_fixmedins_type"));//定点医疗机构类型
            this.cmbZYMdtrtarea.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "insuplc"));//转往医院所属区
            this.cmbZYMdtrtarea.Tag = "440600";
            this.ncbReflType.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "refl_type"));//转院类型
            this.cmbZYinsu_optins.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "insuplc"));//参保机构医保区划
            this.cmbZYinsu_optins.Tag = "440600";
            this.cmbZYDiag1.AddItems(this.localMgr.getDiagList());//诊断

            //1101
            this.cmbmdtrt_cert_type.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "mdtrt_cert_type"));//证件类型

            //添加参保地
            this.cmbInsuplc.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "insuplc"));
            this.cmbInsuplc.Tag = "440606";
        }

        private void InitEvents()
        {
            //患者信息查询
            this.tbQuery.KeyDown += new KeyEventHandler(tbQuery_KeyDown);
            this.btnQueryPatient.Click += new EventHandler(btnQueryPatient_Click);

            //就诊信息相关事件
            this.btnQuery5201.Click += new EventHandler(btnQuery5201_Click);
            this.btnQuery5303.Click += new EventHandler(btnQuery5303_Click);
            this.btnPrint5201.Click += new EventHandler(btnPrint5201_Click);
            this.btnPrint4101.Click += new EventHandler(btnPrint4101_Click);
            this.fpSpread5201.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread5201_CellClick);
            this.tab5201Detail.SelectedIndexChanged += new EventHandler(tab5201Detail_SelectedIndexChanged);

            //人员累计信息查询
            this.btnQuery5206.Click += new EventHandler(btnQuery5206_Click);

            //人员定点备案相关
            this.btnQuery5302.Click += new EventHandler(btnQuery5302_Click);
            this.btn5302.Click += new EventHandler(btn5302_Click);
            this.btn5302Cancel.Click += new EventHandler(btn5302Cancel_Click);

            //转院信息查询
            this.btnQuery5304.Click += new EventHandler(btnQuery5304_Click);

            //缴费查询
            this.btnQuery90100.Click += new EventHandler(btnQuery90100_Click);

            //生育登记相关
            this.btnQuery90101.Click += new EventHandler(btnQuery90101_Click);
            this.btn90201.Click += new EventHandler(btn90201_Click);
            this.btnPrint90101Receipt.Click += new EventHandler(btnPrint90101Receipt_Click);
            this.btnPrint90101.Click += new EventHandler(btnPrint90101_Click);
        }

        #region 患者信息查询事件

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.queryPatientInfo();
            }
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryPatient_Click(object sender, EventArgs e)
        {
            this.queryPatientInfo();
        }

        #endregion 

        #region 就诊信息相关事件

        /// <summary>
        /// 就诊信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery5201_Click(object sender, EventArgs e)
        {
            this.Clear5201();
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            Patient5201 patient5201 = new Patient5201();
            Models.Request.RequestGzsiModel5201 RequestGzsiModel5201 = new Models.Request.RequestGzsiModel5201();
            Models.Response.ResponseGzsiModel5201 ResponseGzsiModel5201 = new Models.Response.ResponseGzsiModel5201();
            Models.Request.RequestGzsiModel5201.Data data5201 = new Models.Request.RequestGzsiModel5201.Data();
            data5201.psn_no = this.PatientInfo.SIMainInfo.Psn_no;
            data5201.begntime = this.dtBeginTime.Value.Date.ToShortDateString();
            data5201.endtime = this.dtEndTime.Value.Date.ToShortDateString();
            data5201.med_type = this.cmbMedType.Tag.ToString();
            data5201.mdtrt_id = "";

            RequestGzsiModel5201.data = data5201;
            returnvalue = patient5201.CallService(RequestGzsiModel5201, ref ResponseGzsiModel5201, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("人员就诊信息查询失败：" + patient5201.ErrorMsg);
                return;
            }
            
            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5201.Output.Mdtrtinfo>(this.fpSpread5201_Sheet1, ResponseGzsiModel5201.output.mdtrtinfo);
        }

        /// <summary>
        /// 在院信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery5303_Click(object sender, EventArgs e)
        {
            this.Clear5201();
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            Patient5303 patient5303 = new Patient5303();
            Models.Request.RequestGzsiModel5303 RequestGzsiModel5303 = new Models.Request.RequestGzsiModel5303();
            Models.Response.ResponseGzsiModel5303 ResponseGzsiModel5303 = new Models.Response.ResponseGzsiModel5303();
            Models.Request.RequestGzsiModel5303.Data data5303 = new Models.Request.RequestGzsiModel5303.Data();
            data5303.psn_no = this.PatientInfo.SIMainInfo.Psn_no;
            data5303.begntime = this.dtBeginTime.Value.Date.ToShortDateString();
            data5303.endtime = this.dtEndTime.Value.Date.ToShortDateString();

            RequestGzsiModel5303.data = data5303;
            returnvalue = patient5303.CallService(RequestGzsiModel5303, ref ResponseGzsiModel5303, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("在院信息查询失败：" + patient5303.ErrorMsg);
                return;
            }

            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5303.Output.Data>(this.fpSpread5201_Sheet1, ResponseGzsiModel5303.output.data);
        }

        /// <summary>
        /// 结算单打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint5201_Click(object sender, EventArgs e)
        {
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.PatientInfo.ID))
            {
                MessageBox.Show("请选择一条就诊记录！");
                return;
            }

            if (string.IsNullOrEmpty(this.PatientInfo.SIMainInfo.Setl_id))
            {
                MessageBox.Show("当前就诊记录无结算信息！");
                return;
            }

            FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
            if (this.PatientInfo.SIMainInfo.TypeCode == "1")
            {
                FS.HISFC.Models.Registration.Register regInfo = this.registerManager.GetByClinic(this.PatientInfo.ID);
                regInfo.SIMainInfo = this.PatientInfo.SIMainInfo;
                patient = regInfo;
            }
            else if (this.PatientInfo.SIMainInfo.TypeCode == "2")
            {
                FS.HISFC.Models.RADT.PatientInfo inpatientInfo = this.inpatientManager.QueryPatientInfoByInpatientNO(this.PatientInfo.ID);
                inpatientInfo.SIMainInfo = this.PatientInfo.SIMainInfo;
                patient = inpatientInfo;
            }
            else
            {
                MessageBox.Show("未知的就诊类型！");
            }

            API.GZSI.Print.ucPrint5203 ucPrint = new API.GZSI.Print.ucPrint5203();
            ucPrint.Patient = patient;
            ucPrint.SetValue(this.PatientInfo.SIMainInfo.TypeCode);
            ucPrint.Print();
        }

        /// <summary>
        /// 医疗保障基金结算清单打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint4101_Click(object sender, EventArgs e)
        {
            InPatient4101 inPatient4101 = new InPatient4101();
            Models.Request.RequestGzsiModel4101 requestGzsiModel4101 = new API.GZSI.Models.Request.RequestGzsiModel4101();
            Models.Response.ResponseGzsiModel4101 responseGzsiModel4101 = new Models.Response.ResponseGzsiModel4101();
            requestGzsiModel4101.setlinfo = new API.GZSI.Models.Request.RequestGzsiModel4101.Setlinfo();
            requestGzsiModel4101.payinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Payinfo>();
            requestGzsiModel4101.opspdiseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Opspdiseinfo>();
            requestGzsiModel4101.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo>();
            requestGzsiModel4101.iteminfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo>();
            requestGzsiModel4101.oprninfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo>();
            requestGzsiModel4101.icuinfo = new List<API.GZSI.Models.Request.RequestGzsiModel4101.Icuinfo>();

            DataTable dtSetlinfo = new DataTable();
            DataTable dtPayinfo = new DataTable();
            DataTable dtOpspdiseinfo = new DataTable();
            DataTable dtDiseinfo = new DataTable();
            DataTable dtIteminfo = new DataTable();
            DataTable dtOprninfo = new DataTable();
            DataTable dtIcuinfo = new DataTable();

            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.PatientInfo.ID))
            {
                MessageBox.Show("请选择一条就诊记录！");
                return;
            }

            if (string.IsNullOrEmpty(this.PatientInfo.SIMainInfo.Setl_id))
            {
                MessageBox.Show("当前就诊记录无结算信息！");
                return;
            }

            FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
            if (this.PatientInfo.SIMainInfo.TypeCode == "1")
            {
                FS.HISFC.Models.Registration.Register regInfo = this.registerManager.GetByClinic(this.PatientInfo.ID);
                regInfo.SIMainInfo = this.PatientInfo.SIMainInfo;
                patient = regInfo;
            }
            else if (this.PatientInfo.SIMainInfo.TypeCode == "2")
            {
                FS.HISFC.Models.RADT.PatientInfo inpatientInfo = this.inpatientManager.QueryPatientInfoByInpatientNO(this.PatientInfo.ID);
                inpatientInfo.SIMainInfo = this.PatientInfo.SIMainInfo;
                patient = inpatientInfo;
            }
            else
            {
                MessageBox.Show("未知的就诊类型！");
                return;
            }

            if (patient is FS.HISFC.Models.Registration.Register)
            {
                MessageBox.Show("门诊记录无法打印医疗保障基金结算清单！");
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.localMgr.GetPatientForJSQD(patient.ID);

            //结算信息
            if (this.localMgr.GetJsqdSetlInfo(patientInfo.SIMainInfo.RegNo, ref dtSetlinfo) < 0)
            {
                MessageBox.Show("查询结算信息失败："+this.localMgr);
                return ;
            }
            else if (dtSetlinfo.Rows.Count == 0)
            {
                MessageBox.Show("未找到结算信息");
                return;
            }

            requestGzsiModel4101.setlinfo = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Setlinfo>(dtSetlinfo.Rows[0]);

            //基金信息
            if (this.localMgr.GetJsqdPayInfo(patientInfo.SIMainInfo.RegNo, ref dtPayinfo) < 0)
            {
                 MessageBox.Show("查询基金信息失败：" + this.localMgr.Err);
                 return;
            }
            foreach (System.Data.DataRow row in dtPayinfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Payinfo pay = new API.GZSI.Models.Request.RequestGzsiModel4101.Payinfo();
                pay = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Payinfo>(row);
                requestGzsiModel4101.payinfo.Add(pay);
            }

            //门诊慢特病诊断信息
            if (this.localMgr.GetJsqdOpspDiseinfo(patientInfo.SIMainInfo.RegNo, ref dtOpspdiseinfo) < 0)
            {
                MessageBox.Show("查询慢性病诊断信息失败：" + this.localMgr.Err);
                return;
            }
            foreach (System.Data.DataRow row in dtOpspdiseinfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Opspdiseinfo opspDiseinfo = new API.GZSI.Models.Request.RequestGzsiModel4101.Opspdiseinfo();
                opspDiseinfo = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Opspdiseinfo>(row);
                requestGzsiModel4101.opspdiseinfo.Add(opspDiseinfo);
            }

            //住院诊断信息
            if (this.localMgr.GetJsqdDiseInfo(patientInfo.SIMainInfo.RegNo, ref dtDiseinfo) < 0)
            {
                 MessageBox.Show("查询住院诊断信息失败："+this.localMgr.Err);
                 return;
            }
            foreach (System.Data.DataRow row in dtDiseinfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo dise = new API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo();
                dise = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Diseinfo>(row);
                requestGzsiModel4101.diseinfo.Add(dise);
            }

            //收费项目信息
            if (this.localMgr.GetJsqdIteminfo(patientInfo.SIMainInfo.RegNo, ref dtIteminfo) < 0)
            {
                 MessageBox.Show("查询收费项目信息失败："+this.localMgr.Err);
                 return;
            }
            foreach (System.Data.DataRow row in dtIteminfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo item = new API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo();
                item = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Iteminfo>(row);
                requestGzsiModel4101.iteminfo.Add(item);
            }

            //手术信息
            if (this.localMgr.GetJsqdOprnInfo(patientInfo.SIMainInfo.RegNo, ref dtOprninfo) < 0)
            {
                 MessageBox.Show("查询手术信息失败："+this.localMgr.Err);
                 return;
            }
            foreach (System.Data.DataRow row in dtOprninfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo oprn = new API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo();
                oprn = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Oprninfo>(row);
                requestGzsiModel4101.oprninfo.Add(oprn);
            }

            //重症监护信息
            if (this.localMgr.GetJsqdIcuInfo(patientInfo.SIMainInfo.RegNo, ref dtIcuinfo) < 0)
            {
                 MessageBox.Show("查询重症监护信息失败："+this.localMgr.Err);
                 return;
            }
            foreach (System.Data.DataRow row in dtIcuinfo.Rows)
            {
                API.GZSI.Models.Request.RequestGzsiModel4101.Icuinfo icu = new API.GZSI.Models.Request.RequestGzsiModel4101.Icuinfo();
                icu = Common.Function.DataRowToObject<API.GZSI.Models.Request.RequestGzsiModel4101.Icuinfo>(row);
                requestGzsiModel4101.icuinfo.Add(icu);
            }

            API.GZSI.Print.uc4101.ucPrint4101 ucPrint = new API.GZSI.Print.uc4101.ucPrint4101();
            ucPrint.RequestModel = requestGzsiModel4101;
            ucPrint.PatientInfo = patientInfo;
            ucPrint.SetValue();
            ucPrint.Print();
        }

        /// <summary>
        /// 选择就诊信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread5201_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.fpSpread5201_Sheet1.SetActiveCell(e.Row,0);
            this.query5201Detail();
        }

        /// <summary>
        /// 就诊明细选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab5201Detail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.query5201Detail();
        }

        #endregion

        #region 人员累计信息查询
        
        /// <summary>
        /// 人员累计信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery5206_Click(object sender, EventArgs e)
        {
            this.Clear5206();
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            Patient5206 patient5206 = new Patient5206();
            Models.Request.RequestGzsiModel5206 RequestGzsiModel5206 = new Models.Request.RequestGzsiModel5206();
            Models.Response.ResponseGzsiModel5206 ResponseGzsiModel5206 = new Models.Response.ResponseGzsiModel5206();
            Models.Request.RequestGzsiModel5206.Data data5206 = new Models.Request.RequestGzsiModel5206.Data();
            data5206.psn_no = this.PatientInfo.SIMainInfo.Psn_no;
            data5206.cum_ym = this.dtpDate5206.Value.Date.ToString("yyyyMM");

            RequestGzsiModel5206.data = data5206;
            returnvalue = patient5206.CallService(RequestGzsiModel5206, ref ResponseGzsiModel5206, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("人员累计信息查询失败：" + patient5206.ErrorMsg);
                return;
            }

            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5206.Output.Cuminfo>(this.fpSpread5206_Sheet1, ResponseGzsiModel5206.output.cuminfo);
        }

        #endregion

        #region 人员定点备案相关

        /// <summary>
        /// 人员定点备案查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery5302_Click(object sender, EventArgs e)
        {
            this.Clear5302();
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            Patient5302 patient5302 = new Patient5302();
            Models.Request.RequestGzsiModel5302 RequestGzsiModel5302 = new Models.Request.RequestGzsiModel5302();
            Models.Response.ResponseGzsiModel5302 ResponseGzsiModel5302 = new Models.Response.ResponseGzsiModel5302();
            Models.Request.RequestGzsiModel5302.Data data5302 = new Models.Request.RequestGzsiModel5302.Data();
            data5302.psn_no = this.PatientInfo.SIMainInfo.Psn_no;
            data5302.biz_appy_type = this.cmbApplyType.Tag.ToString();

            RequestGzsiModel5302.data = data5302;
            returnvalue = patient5302.CallService(RequestGzsiModel5302, ref ResponseGzsiModel5302, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("人员定点备案查询失败：" + patient5302.ErrorMsg);
                return;
            }

            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5302.Output.Psnfixmedin>(this.fpSpread5302_Sheet1, ResponseGzsiModel5302.output.psnfixmedin);
        }

        /// <summary>
        /// 人员定点备案tab页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5302_Click(object sender, EventArgs e)
        {
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            frmReport2505 frmReport = new frmReport2505();
            frmReport.PatientInfo = this.patientInfo;

            if (frmReport.ShowDialog() != DialogResult.OK)
            {
                frmReport.Dispose();
                MessageBox.Show("操作取消");
                return;
            }

            int returnvalue = 0;
            Patient2505 patient2505 = new Patient2505();
            Models.Request.RequestGzsiModel2505 requestGzsiModel2505 = new API.GZSI.Models.Request.RequestGzsiModel2505();
            Models.Response.ResponseGzsiModel2505 responseGzsiModel2505 = new Models.Response.ResponseGzsiModel2505();
            Models.Request.RequestGzsiModel2505.Data data = new API.GZSI.Models.Request.RequestGzsiModel2505.Data();

            data.psn_no = frmReport.psn_no;
            data.tel = frmReport.tel;
            data.addr = frmReport.addr;
            data.biz_appy_type = "03";
            data.begndate = frmReport.begndate;
            data.enddate = frmReport.enddate;
            data.agnter_name = "";
            data.agnter_cert_type = "";
            data.agnter_certno = "";
            data.agnter_tel = "";
            data.agnter_addr = "";
            data.agnter_rlts = "";
            data.fix_srt_no = frmReport.fix_srt_no;
            data.fixmedins_code = frmReport.fixmedins_code; 
            data.fixmedins_name = frmReport.fixmedins_name;
            data.memo = "";

            //释放弹出框资源
            frmReport.Dispose();

            requestGzsiModel2505.data = data;

            returnvalue = patient2505.CallService(requestGzsiModel2505, ref responseGzsiModel2505, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("定点备案失败：" + patient2505.ErrorMsg);
                return;
            }
            else
            {
                MessageBox.Show("人员定点备案成功！待遇流水号：" + responseGzsiModel2505.output.result.trt_dcla_detl_sn);
            }
        }

        /// <summary>
        /// 人员定点备案取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5302Cancel_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region 转院信息查询

        private void btnQuery5304_Click(object sender, EventArgs e)
        {
            this.Clear5304();
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            Patient5304 patient5304 = new Patient5304();
            Models.Request.RequestGzsiModel5304 RequestGzsiModel5304 = new Models.Request.RequestGzsiModel5304();
            Models.Response.ResponseGzsiModel5304 ResponseGzsiModel5304 = new Models.Response.ResponseGzsiModel5304();
            Models.Request.RequestGzsiModel5304.Data data5304 = new Models.Request.RequestGzsiModel5304.Data();
            data5304.psn_no = patientInfo.SIMainInfo.Psn_no;
            data5304.begntime = this.dtpBegin5304.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
            data5304.endtime = this.dtpEnd5304.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");

            RequestGzsiModel5304.data = data5304;
            returnvalue = patient5304.CallService(RequestGzsiModel5304, ref ResponseGzsiModel5304, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("转院信息查询失败：" + patient5304.ErrorMsg);
                return;
            }

            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5304.Output.Refmedin>(this.fpSpread5304_Sheet1, ResponseGzsiModel5304.output.refmedin);
        }

        #endregion 

        #region 缴费查询

        /// <summary>
        /// 缴费查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery90100_Click(object sender, EventArgs e)
        {
            this.Clear90100();
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            Patient90100 patient90100 = new Patient90100();
            Models.Request.RequestGzsiModel90100 RequestGzsiModel90100 = new Models.Request.RequestGzsiModel90100();
            Models.Response.ResponseGzsiModel90100 ResponseGzsiModel90100 = new Models.Response.ResponseGzsiModel90100();
            Models.Request.RequestGzsiModel90100.Data data90100 = new Models.Request.RequestGzsiModel90100.Data();
            data90100.psn_no = this.PatientInfo.SIMainInfo.Psn_no;

            RequestGzsiModel90100.data = data90100;
            returnvalue = patient90100.CallService(RequestGzsiModel90100, ref ResponseGzsiModel90100, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("缴费信息查询失败：" + patient90100.ErrorMsg);
                return;
            }

            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel90100.Output>(this.fpSpread90100_Sheet1, ResponseGzsiModel90100.output);
        }

        #endregion

        #region 生育登记相关

        /// <summary>
        /// 生育登记查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery90101_Click(object sender, EventArgs e)
        {
            this.Clear90101();
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            Patient90101 patient90101 = new Patient90101();
            Models.Request.RequestGzsiModel90101 RequestGzsiModel90101 = new Models.Request.RequestGzsiModel90101();
            Models.Response.ResponseGzsiModel90101 ResponseGzsiModel90101 = new Models.Response.ResponseGzsiModel90101();
            Models.Request.RequestGzsiModel90101.Data data90101 = new Models.Request.RequestGzsiModel90101.Data();
            data90101.psn_no = this.PatientInfo.SIMainInfo.Psn_no;

            RequestGzsiModel90101.data = data90101;
            returnvalue = patient90101.CallService(RequestGzsiModel90101, ref ResponseGzsiModel90101, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("生育登记查询失败：" + patient90101.ErrorMsg);
                return;
            }

            if (ResponseGzsiModel90101.output.result == null || ResponseGzsiModel90101.output.result.Count <= 0)
            {
                MessageBox.Show("未找到人员生育登记！" + ResponseGzsiModel90101.err_msg);
            }

            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel90101.Output.Result>(this.fpSpread90101_Sheet1, ResponseGzsiModel90101.output.result);

        }

        /// <summary>
        /// 生育登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn90201_Click(object sender, EventArgs e)
        {
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            frmReport90201 frmReport = new frmReport90201();
            frmReport.PatientInfo = this.patientInfo;

            if (frmReport.ShowDialog() != DialogResult.OK)
            {
                frmReport.Dispose();
                MessageBox.Show("操作取消");
                return;
            }

            Patient90201 patient90201 = new Patient90201();
            Models.Request.RequestGzsiModel90201 requestGzsiModel90201 = new API.GZSI.Models.Request.RequestGzsiModel90201();
            Models.Response.ResponseGzsiModel90201 responseGzsiModel90201 = new API.GZSI.Models.Response.ResponseGzsiModel90201();
            Models.Request.RequestGzsiModel90201.Data data = new API.GZSI.Models.Request.RequestGzsiModel90201.Data();

            data.psn_no = frmReport.psn_no;	//人员编号 Y
            data.fixmedins_code = frmReport.fixmedins_code;	//定点医药机构编号 Y
            data.fixmedins_name = frmReport.fixmedins_name;	//定点医药机构名称 Y
            data.tel = frmReport.tel;	//联系电话 Y
            data.geso_val = frmReport.geso_val;	//孕周数
            data.fetts = frmReport.fetts;	//胎次
            data.matn_type = frmReport.matn_type;	//生育类别  Y
            data.matn_trt_dclaer_type = frmReport.matn_trt_dclaer_type;	//生育待遇申报人员类别
            data.fpsc_no = frmReport.fpsc_no;	//计划生育服务证号
            data.last_mena_date = frmReport.last_mena_date;	//末次月经日期
            data.plan_matn_date = frmReport.plan_matn_date;	//预计生育日期
            data.begndate = frmReport.begndate;	//开始日期 Y
            data.enddate = frmReport.enddate;	//结束日期 Y
            data.agnter_name = "";	//代办人姓名
            data.agnter_cert_type = "";	//代办人证件类型
            data.agnter_certno = "";	//代办人证件号码
            data.agnter_tel = "";	//代办人联系方式
            data.agnter_rlts = "";	//代办人关系
            data.agnter_addr = "-";	//代办人联系地址
            data.spus_name = "";	//配偶姓名
            data.spus_cert_type = "";	//配偶证件类型
            data.spus_certno = "";	//配偶证件号码

            //释放弹出框资源
            frmReport.Dispose();

            requestGzsiModel90201.data = data;
            returnvalue = patient90201.CallService(requestGzsiModel90201, ref responseGzsiModel90201, SerialNumber, strTransVersion, strVerifyCode);
            if (returnvalue == -1)
            {
                MessageBox.Show("生育登记失败：" + patient90201.ErrorMsg);
                return;
            }
            else
            {
                MessageBox.Show("生育登记成功:" + responseGzsiModel90201.output.result.trt_dcla_detl_sn);
                this.btnQuery90101_Click(null, null);
            }
        }

        /// <summary>
        /// 生育登记回执
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint90101Receipt_Click(object sender, EventArgs e)
        {
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            if (this.fpSpread90101_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请先选择一条定点记录！");
                return;
            }

            API.GZSI.Print.ucPirnt90101Receipt ucPrint = new API.GZSI.Print.ucPirnt90101Receipt();
            ResponseGzsiModel90101.Output.Result result = this.fpSpread90101_Sheet1.ActiveRow.Tag as ResponseGzsiModel90101.Output.Result;
            if (result == null)
            {
                MessageBox.Show("获取生育定点信息出错！");
                return;
            }

            ucPrint.SetValue(result.psn_name,
                             result.certno,
                             result.tel,
                             result.fixmedins_name,
                             result.geso_val,
                             result.fetts,
                             result.matn_type,
                             result.plan_matn_date,
                             result.begndate,
                             result.enddate,
                             result.agnter_name,
                             result.agnter_certno,
                             result.agnter_tel,
                             result.agnter_addr,
                             result.dcladate);
            ucPrint.Print();
        }

        /// <summary>
        /// 生育就医确认单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint90101_Click(object sender, EventArgs e)
        {
            if (this.queryPatientSIInfo() < 0)
            {
                return;
            }

            if (this.fpSpread90101_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请先选择一条定点记录！");
                return;
            }

            API.GZSI.Print.ucPrint90101 ucPrint = new API.GZSI.Print.ucPrint90101();
            ResponseGzsiModel90101.Output.Result result = this.fpSpread90101_Sheet1.ActiveRow.Tag as ResponseGzsiModel90101.Output.Result;
            if (result == null)
            {
                MessageBox.Show("获取生育定点信息出错！");
                return;
            }

            ucPrint.SetValue(result.trt_dcla_detl_sn, 
                             this.PatientInfo.Name, 
                             this.PatientInfo.SIMainInfo.Psn_no, 
                             this.PatientInfo.IDCard,
                             result.plan_matn_date,
                             result.begndate);
            ucPrint.Print();
        }

        #endregion

        /// <summary>
        /// 查询患者信息
        /// </summary>
        private void queryPatientInfo()
        {
            this.Clear();
            FS.HISFC.Components.Common.Controls.ucQueryPatientInfo uc = new FS.HISFC.Components.Common.Controls.ucQueryPatientInfo(); 
            uc.QueryStr = this.tbQuery.Text;
            DialogResult diaResult = FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            if (diaResult != DialogResult.OK)
            {
                return;
            }

            this.PatientInfo = uc.Patient;
            if (this.PatientInfo != null && !string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                this.tbQuery.Text = this.PatientInfo.PID.CardNO;
                this.lblName.Text = "患者姓名：" + this.PatientInfo.Name;
                this.lblSex.Text = "性别：" + (this.PatientInfo.Sex.ID.ToString() == "M" ? "男" : "女");
                this.lblAge.Text = "年龄：" + inpatientManager.GetAge(this.PatientInfo.Birthday);
                this.lblPhone.Text = "电话：" + this.PatientInfo.PhoneHome;
                this.lblIDNO.Text = "证件号：" + this.PatientInfo.IDCard;
            }
            else
            {
                MessageBox.Show("查询患者信息失败！");
            }
              
        }

        /// <summary>
        /// 查询患者医保信息
        /// </summary>
        private int queryPatientSIInfo()
        {
            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                MessageBox.Show("请先检索患者！");
                return -1;
            }

            if (!string.IsNullOrEmpty(this.PatientInfo.SIMainInfo.Psn_no))
            {
                return 1;
            }

            //存在身份证时通过身份证查询医保编码
            //否则弹出信息输入框
            if (!string.IsNullOrEmpty(this.PatientInfo.IDCard))
            {
                int returnvalue = 0;
                Patient1101 patient1101 = new Patient1101();
                Models.Request.RequestGzsiModel1101 requestGzsiModel1101 = new API.GZSI.Models.Request.RequestGzsiModel1101();
                Models.Response.ResponseGzsiModel1101 responseGzsiModel1101 = new Models.Response.ResponseGzsiModel1101();
                Models.Request.RequestGzsiModel1101.Data data = new API.GZSI.Models.Request.RequestGzsiModel1101.Data();

                data.mdtrt_cert_type = "02";                   //就诊凭证类型
                // {32232925-B715-44DD-83E6-7E392EB2A7AD}
                if (this.PatientInfo.IDCard.Contains("MAC") || this.PatientInfo.IDCard.Contains("HKG") || this.PatientInfo.IDCard.StartsWith("8") || this.patientInfo.IDCard.Length<18)
                {
                    data.mdtrt_cert_type = "99";
                }
                data.mdtrt_cert_no = this.PatientInfo.IDCard; //凭证号码
                data.card_sn = "";                     //卡识别码，凭证类型为03时必填
                data.psn_cert_type = "";             //人员证件类型
                data.psn_name = this.PatientInfo.Name; //人员姓名
                data.certno = this.PatientInfo.IDCard; //证件号

                requestGzsiModel1101.data = data;
                returnvalue = patient1101.CallService(requestGzsiModel1101, ref responseGzsiModel1101, SerialNumber, strTransVersion, strVerifyCode);
                if (returnvalue == -1)
                {
                    MessageBox.Show(patient1101.ErrorMsg);
                    return -1;
                }

                this.PatientInfo.SIMainInfo.Psn_no = responseGzsiModel1101.output.baseinfo.psn_no;
            }
            else
            {
                frmPatientQuery frmSIPatient = new frmPatientQuery();
                frmSIPatient.Patient = this.PatientInfo;
                frmSIPatient.OperateType = "2";

                if (frmSIPatient.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("查询患者医保信息失败！");
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 查询就诊明细
        /// </summary>
        /// <returns></returns>
        private int query5201Detail()
        {
            this.fpSpread5201Detail_Sheet1.RowCount = 0;
            if (this.fpSpread5201_Sheet1.ActiveRow == null)
            {
                return -1;
            }

            if (this.fpSpread5201_Sheet1.ActiveRow.Tag is ResponseGzsiModel5201.Output.Mdtrtinfo)
            {
                ResponseGzsiModel5201.Output.Mdtrtinfo mdtrtInfo = this.fpSpread5201_Sheet1.ActiveRow.Tag as ResponseGzsiModel5201.Output.Mdtrtinfo;
                this.PatientInfo.SIMainInfo.Mdtrt_id = mdtrtInfo.mdtrt_id;
                this.PatientInfo.ID = mdtrtInfo.ipt_otp_no.Substring(0, mdtrtInfo.ipt_otp_no.Length-1);
            }
            else if (this.fpSpread5201_Sheet1.ActiveRow.Tag is ResponseGzsiModel5303.Output.Data)
            {
                ResponseGzsiModel5303.Output.Data data = this.fpSpread5201_Sheet1.ActiveRow.Tag as ResponseGzsiModel5303.Output.Data;
                this.PatientInfo.SIMainInfo.Mdtrt_id = data.mdtrt_id;
                this.PatientInfo.ID = data.ipt_otp_no.Substring(0, data.ipt_otp_no.Length-1);
            }

            patientInfo.SIMainInfo.Setl_id = "";   //结算序号
            patientInfo.SIMainInfo.TypeCode = "";  //实体类型
            patientInfo.SIMainInfo.Dise_code = ""; //病种编码
            if (this.localMgr.GetPatientInfo(ref this.patientInfo) < 0)
            {
                MessageBox.Show("查询患者医保信息出错：" + this.localMgr.Err);
                return -1;
            }

            if (string.IsNullOrEmpty(this.PatientInfo.SIMainInfo.Setl_id))
            {
                return -1;
            }

            if (this.tab5201Detail.SelectedIndex == 0)  
            {
                //查询就诊诊断信息
                return this.query5202Info();
            }
            else if (this.tab5201Detail.SelectedIndex == 1) 
            {
                // 查询就诊结算信息
                return this.query5203Info();
            }
            else if (this.tab5201Detail.SelectedIndex == 2)
            {
                // 查询就诊费用明细
                return this.query5204Info();
            }

            return -1;
        }

        /// <summary>
        /// 查询就诊诊断信息
        /// </summary>
        /// <returns></returns>
        private int query5202Info()
        {
            if (this.queryPatientSIInfo() < 0)
            {
                return -1;
            }

            Patient5202 patient5202 = new Patient5202();
            Models.Request.RequestGzsiModel5202 RequestGzsiModel5202 = new Models.Request.RequestGzsiModel5202();
            Models.Response.ResponseGzsiModel5202 ResponseGzsiModel5202 = new Models.Response.ResponseGzsiModel5202();
            Models.Request.RequestGzsiModel5202.Data data5202 = new Models.Request.RequestGzsiModel5202.Data();
            data5202.psn_no = this.PatientInfo.SIMainInfo.Psn_no;
            data5202.mdtrt_id = this.PatientInfo.SIMainInfo.Mdtrt_id;

            RequestGzsiModel5202.data = data5202;
            returnvalue = patient5202.CallService(RequestGzsiModel5202, ref ResponseGzsiModel5202, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("人员诊断信息查询失败：" + patient5202.ErrorMsg);
                return -1;
            }

            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5202.Output.Diseinfo>(this.fpSpread5201Detail_Sheet1, ResponseGzsiModel5202.output.diseinfo);
            return 1;
        }

        /// <summary>
        /// 查询就诊结算信息
        /// </summary>
        /// <returns></returns>
        private int query5203Info()
        {
            if (this.queryPatientSIInfo() < 0)
            {
                return -1;
            }

            Patient5203 patient5203 = new Patient5203();
            Models.Request.RequestGzsiModel5203 RequestGzsiModel5203 = new Models.Request.RequestGzsiModel5203();
            Models.Response.ResponseGzsiModel5203 ResponseGzsiModel5203 = new Models.Response.ResponseGzsiModel5203();
            Models.Request.RequestGzsiModel5203.Data data5203 = new Models.Request.RequestGzsiModel5203.Data();
            data5203.psn_no = this.PatientInfo.SIMainInfo.Psn_no;
            data5203.setl_id = this.PatientInfo.SIMainInfo.Setl_id;
            data5203.mdtrt_id = this.PatientInfo.SIMainInfo.Mdtrt_id;

            RequestGzsiModel5203.data = data5203;
            returnvalue = patient5203.CallService(RequestGzsiModel5203, ref ResponseGzsiModel5203, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("人员结算信息查询失败：" + patient5203.ErrorMsg);
                return -1;
            }

            List<Models.Response.ResponseGzsiModel5203.Output.SetlInfo> SetlinfoList = new List<ResponseGzsiModel5203.Output.SetlInfo>();
            SetlinfoList.Add(ResponseGzsiModel5203.output.setlinfo);
            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5203.Output.SetlInfo>(this.fpSpread5201Detail_Sheet1, SetlinfoList);

            return 1;
        }

        /// <summary>
        /// 查询就诊费用明细
        /// </summary>
        /// <returns></returns>
        private int query5204Info()
        {
            if (this.queryPatientSIInfo() < 0)
            {
                return -1;
            }

            Patient5204 patient5204 = new Patient5204();
            Models.Request.RequestGzsiModel5204 RequestGzsiModel5204 = new Models.Request.RequestGzsiModel5204();
            Models.Response.ResponseGzsiModel5204 ResponseGzsiModel5204 = new Models.Response.ResponseGzsiModel5204();
            Models.Request.RequestGzsiModel5204.Data data5204 = new Models.Request.RequestGzsiModel5204.Data();
            data5204.psn_no = this.PatientInfo.SIMainInfo.Psn_no;
            data5204.setl_id = this.PatientInfo.SIMainInfo.Setl_id;
            data5204.mdtrt_id = this.PatientInfo.SIMainInfo.Mdtrt_id;

            RequestGzsiModel5204.data = data5204;
            returnvalue = patient5204.CallService(RequestGzsiModel5204, ref ResponseGzsiModel5204, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("费用明细查询失败：" + patient5204.ErrorMsg);
                return -1;
            }

            Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5204.Output>(this.fpSpread5201Detail_Sheet1, ResponseGzsiModel5204.output.OrderBy(m=>m.hilist_name).ToList());
            return 1;
        }

        /// <summary>
        /// 信息清空
        /// </summary>
        private void Clear()
        {
            this.PatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            this.lblName.Text = "患者姓名：";
            this.lblSex.Text = "性别：";
            this.lblAge.Text = "年龄：";
            this.lblPhone.Text = "电话：";
            this.lblIDNO.Text = "证件号：";

            this.Clear5201();
            this.Clear5206();
            this.Clear5302();
            this.Clear5304();
            this.Clear90100();
            this.Clear90101();
        }

        /// <summary>
        /// 清空就诊信息
        /// </summary>
        private void Clear5201()
        {
            this.fpSpread5201_Sheet1.RowCount = 0;
            this.fpSpread5201Detail_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 清空累计信息
        /// </summary>
        private void Clear5206()
        {
            this.fpSpread5206_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 清空定点信息
        /// </summary>
        private void Clear5302() 
        {
            this.fpSpread5302_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 清空转院信息
        /// </summary>
        private void Clear5304()
        {
            this.fpSpread5304_Sheet1.RowCount = 0;
        }
        
        /// <summary>
        /// 清空缴费信息 
        /// </summary>
        private void Clear90100()
        {
            this.fpSpread90100_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 清空生育登记信息
        /// </summary>
        private void Clear90101()
        {
            this.fpSpread90101_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 查询信息
        /// </summary>
        private void QueryInfo()
        {
            if (this.PatientInfo != null)
            {

                //{
                //    InPatient90101 inPatient90101 = new InPatient90101();
                //    Models.Request.RequestGzsiModel90101 RequestGzsiModel90101 = new Models.Request.RequestGzsiModel90101();
                //    Models.Response.ResponseGzsiModel90101 ResponseGzsiModel90101 = new Models.Response.ResponseGzsiModel90101();
                //    Models.Request.RequestGzsiModel90101.Data data90101 = new Models.Request.RequestGzsiModel90101.Data();
                //    data90101.psn_no = "";

                //    RequestGzsiModel90101.data = data90101;

                //    if (inPatient90101.CallService(RequestGzsiModel90101, ref ResponseGzsiModel90101) < 0)
                //    {
                //        MessageBox.Show("生育登记查询失败！" + ResponseGzsiModel90101.err_msg);
                //        return;
                //    }

                //    if (ResponseGzsiModel90101.infcode != "0")
                //    {
                //        MessageBox.Show("生育登记查询失败！" + ResponseGzsiModel90101.err_msg);
                //        return;
                //    }

                //    if (ResponseGzsiModel90101.result == null || ResponseGzsiModel90101.result.Count <= 0)
                //    {
                //        MessageBox.Show("未找到人员生育登记！" + ResponseGzsiModel90101.err_msg);
                //        return;
                //    }

                //    Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel90101.Result>(this.fpSpreadbaseinfo_Sheet1, ResponseGzsiModel90101.result);

                //}

                //{
                //    InPatient90102 inPatient90102 = new InPatient90102();
                //    Models.Request.RequestGzsiModel90102 RequestGzsiModel90102 = new Models.Request.RequestGzsiModel90102();
                //    Models.Response.ResponseGzsiModel90102 ResponseGzsiModel90102 = new Models.Response.ResponseGzsiModel90102();
                //    Models.Request.RequestGzsiModel90102.Data data90102 = new Models.Request.RequestGzsiModel90102.Data();
                //    data90102.psn_no = "";

                //    RequestGzsiModel90102.data = data90102;

                //    if (inPatient90102.CallService(RequestGzsiModel90102, ref ResponseGzsiModel90102) < 0)
                //    {
                //        MessageBox.Show("家庭医生签约登记查询失败！" + ResponseGzsiModel90102.err_msg);
                //        return;
                //    }

                //    if (ResponseGzsiModel90102.infcode != "0")
                //    {
                //        MessageBox.Show("家庭医生签约登记查询失败！" + ResponseGzsiModel90102.err_msg);
                //        return;
                //    }

                //    if (ResponseGzsiModel90102.result == null || ResponseGzsiModel90102.result.Count <= 0)
                //    {
                //        MessageBox.Show("未找到人员家庭医生签约登记！" + ResponseGzsiModel90102.err_msg);
                //        return;
                //    }

                //    Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel90102.Result>(this.fpSpreadbaseinfo_Sheet1, ResponseGzsiModel90102.result);

                //}

                //{
                //    
                //    InPatient90103 inPatient90103 = new InPatient90103();
                //    Models.Request.RequestGzsiModel90103 RequestGzsiModel90103 = new Models.Request.RequestGzsiModel90103();
                //    Models.Response.ResponseGzsiModel90103 ResponseGzsiModel90103 = new Models.Response.ResponseGzsiModel90103();
                //    Models.Request.RequestGzsiModel90103.Data data90103 = new Models.Request.RequestGzsiModel90103.Data();
                //    data90103.psn_no = "";

                //    RequestGzsiModel90103.data = data90103;

                //    if (inPatient90103.CallService(RequestGzsiModel90103, ref ResponseGzsiModel90103) < 0)
                //    {
                //        MessageBox.Show("家庭病床登记查询失败！" + ResponseGzsiModel90103.err_msg);
                //        return;
                //    }

                //    if (ResponseGzsiModel90103.infcode != "0")
                //    {
                //        MessageBox.Show("家庭病床登记查询失败！" + ResponseGzsiModel90103.err_msg);
                //        return;
                //    }

                //    if (ResponseGzsiModel90103.result == null || ResponseGzsiModel90103.result.Count <= 0)
                //    {
                //        MessageBox.Show("未找到人员家庭病床登记！" + ResponseGzsiModel90103.err_msg);
                //        return;
                //    }

                //    Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel90103.Result>(this.fpSpreadbaseinfo_Sheet1, ResponseGzsiModel90103.result);

                //}
            }
        }

        private void btCancelIn_Click(object sender, EventArgs e)
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(patientInfo.SIMainInfo.Psn_no))
            {
                MessageBox.Show("请先查询患者信息");
                return;
            }
            if (string.IsNullOrEmpty(this.txtZYRegID.Text))
            {
                MessageBox.Show("请输入就诊ID");
                return;
            }

            InPatient2404 inPatient2404 = new InPatient2404();
            Models.Request.RequestGzsiModel2404 requestGzsiModel2404 = new Models.Request.RequestGzsiModel2404();
            Models.Request.RequestGzsiModel2404.Data tempData = new Models.Request.RequestGzsiModel2404.Data();
            Models.Response.ResponseGzsiModel2404 responseGzsiModel2404 = new ResponseGzsiModel2404();
            tempData.mdtrt_id = this.txtZYRegID.Text;// this.fpSpreadbaseinfo_Sheet1.Cells[rowIndex, 0].Text;//patientObj.SIMainInfo.Mdtrt_id;
            tempData.psn_no = patientInfo.SIMainInfo.Psn_no;// this.fpSpreadbaseinfo_Sheet1.Cells[rowIndex, 1].Text; //patientObj.SIMainInfo.Psn_no;
            requestGzsiModel2404.data = tempData;


            if (inPatient2404.CallService( requestGzsiModel2404, ref responseGzsiModel2404) < -1)
            {
                MessageBox.Show(inPatient2404.ErrorMsg);
                return;
            }
            if (responseGzsiModel2404.infcode != "0")
            {
                MessageBox.Show(responseGzsiModel2404.err_msg);
                return;
            }
            if (!string.IsNullOrEmpty(patientInfo.ID))
            {
                if (this.localMgr.UpdateSiMainInfoValidFlag(patientInfo.ID, patientInfo.SIMainInfo.Mdtrt_id) < 0)
                {
                    MessageBox.Show(this.localMgr.Err);
                    return;
                }
            }

            MessageBox.Show("入院撤销成功！");
            //QueryInfo();//刷新
            return;
        }

        private void btCancelOut_Click(object sender, EventArgs e)
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(patientInfo.SIMainInfo.Psn_no))
            {
                MessageBox.Show("请先查询患者信息");
                return;
            }
            if (string.IsNullOrEmpty(this.txtZYRegID.Text))
            {
                MessageBox.Show("请输入就诊ID");
                return;
            }
            #region 出院撤销（2405）
            InPatient2405 inPatient2405 = new InPatient2405();
            Models.Request.RequestGzsiModel2405 requestGzsiModel2405 = new Models.Request.RequestGzsiModel2405();
            Models.Response.ResponseGzsiModel2405 responseGzsiModel2405 = new ResponseGzsiModel2405();
            Models.Request.RequestGzsiModel2405.Data data2405 = new Models.Request.RequestGzsiModel2405.Data();
            data2405.mdtrt_id = this.txtZYRegID.Text;
            data2405.psn_no = patientInfo.SIMainInfo.Psn_no;
            requestGzsiModel2405.data = data2405;

            if (inPatient2405.CallService(requestGzsiModel2405, ref responseGzsiModel2405) < 0)
            {
                MessageBox.Show(inPatient2405.ErrorMsg);
                return;
            }
            #endregion
            if (responseGzsiModel2405.infcode != "0")
            {
                MessageBox.Show(responseGzsiModel2405.err_msg);
                return;
            }
            MessageBox.Show("出院撤销成功！");
            return;
        }

        private void btCancelPay_Click(object sender, EventArgs e)
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(patientInfo.SIMainInfo.Psn_no))
            {
                MessageBox.Show("请先查询患者信息");
                return;
            }
            if (string.IsNullOrEmpty(this.txtZYRegID.Text))
            {
                MessageBox.Show("请输入就诊ID");
                return;
            }
            if (string.IsNullOrEmpty(this.txtZYFeeID.Text))
            {
                MessageBox.Show("请输入结算ID");
                return;
            }
            InPatient2305 inPatient2305 = new InPatient2305();
            Models.Request.RequestGzsiModel2305 requestGzsiModel2305 = new Models.Request.RequestGzsiModel2305();
            Models.Response.ResponseGzsiModel2305 responseGzsiModel2305 = new ResponseGzsiModel2305();
            Models.Request.RequestGzsiModel2305.Mdtrtinfo data2305 = new Models.Request.RequestGzsiModel2305.Mdtrtinfo();
            data2305.setl_id = this.txtZYFeeID.Text;
            data2305.mdtrt_id = this.txtZYRegID.Text;
            data2305.psn_no = patientInfo.SIMainInfo.Psn_no;
            requestGzsiModel2305.data = data2305;
            if (inPatient2305.CallService( requestGzsiModel2305, ref responseGzsiModel2305) < 0)
            {
                MessageBox.Show(inPatient2305.ErrorMsg);
                return;
            }
            if (responseGzsiModel2305.infcode != "0")
            {
                MessageBox.Show(responseGzsiModel2305.err_msg);
                return;
            }

            MessageBox.Show("取消住院结算成功！");
            return;
        }

        private void label92_Click(object sender, EventArgs e)
        {

        }

        private void label88_Click(object sender, EventArgs e)
        {

        }

        private void bQueryJG2501A_Click(object sender, EventArgs e)
        {
            if (this.cmbZYFixType.Tag == null || string.IsNullOrEmpty(this.cmbZYFixType.Tag.ToString()))
            {
                MessageBox.Show("查询时定点医疗机构类型不能为空！");
                return;
            }

            if (this.cmbZYMdtrtarea.Tag == null || string.IsNullOrEmpty(this.cmbZYMdtrtarea.Tag.ToString()))
            {
                MessageBox.Show("查询时转往医院所属区不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(this.txtZYFixName.Text))
            {
                MessageBox.Show("查询时定点医疗机构名称不能为空！");
                return;
            }

            InPatient1201 inPatient1201 = new InPatient1201();
            Models.Request.RequestGzsiModel1201 requestGzsiModel1201 = new Models.Request.RequestGzsiModel1201();
            Models.Response.ResponseGzsiModel1201 responseGzsiModel1201 = new ResponseGzsiModel1201();
            Models.Request.RequestGzsiModel1201.Medinsinfo medinsinfo1201 = new Models.Request.RequestGzsiModel1201.Medinsinfo();

            medinsinfo1201.fixmedins_type = (this.cmbZYFixType.SelectedItem as FS.HISFC.Models.Base.Const).ID;
            medinsinfo1201.fixmedins_code = "";
            medinsinfo1201.fixmedins_name = this.txtZYFixName.Text;
            requestGzsiModel1201.medinsinfo = medinsinfo1201;

            if (inPatient1201.CallService(requestGzsiModel1201, ref responseGzsiModel1201) < 0)
            {
                MessageBox.Show(inPatient1201.ErrorMsg);
                return;
            }

            if (responseGzsiModel1201.output.medinsinfo != null && responseGzsiModel1201.output.medinsinfo.Count > 0)
            {
                Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel1201.Output.Medinsinfo>(this.fpSpread_2501A, responseGzsiModel1201.output.medinsinfo);
            }
            else
            {
                MessageBox.Show("查询定点医药机构为空！");
            }
        }

        private void btTransferBak_Click(object sender, EventArgs e)
        {
            if (this.patientInfo == null)// || string.IsNullOrEmpty(pInfo.ID)
            {
                MessageBox.Show("请先查询患者信息");
                return;
            }

            //patientInfo.SIMainInfo.Trt_dcla_detl_sn2501 = "";

            if (this.cmbZYMdtrtarea.Tag == null || string.IsNullOrEmpty(this.cmbZYMdtrtarea.Tag.ToString()))
            {
                MessageBox.Show("转往医院所属的行政区划不能为空！");
                this.cmbZYMdtrtarea.Focus();
                return;
            }

            if (this.cmbZYDiag1.Tag == null || string.IsNullOrEmpty(this.cmbZYDiag1.Tag.ToString()))
            {
                MessageBox.Show("请选择诊断！");
                this.cmbZYDiag1.Focus();
                return;
            }

            if (this.ncbReflType.Tag == null || string.IsNullOrEmpty(this.ncbReflType.Tag.ToString()))
            {
                MessageBox.Show("转诊类型不能为空！");
                this.ncbReflType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.ntbReflRea.Text))
            {
                MessageBox.Show("转院原因不能为空！");
                this.ntbReflRea.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.ntbReflOpnn.Text))
            {
                MessageBox.Show("转院意见不能为空！");
                this.ntbReflRea.Focus();
                return;
            }

            #region 机构信息
            int index = this.fpSpread_2501A.ActiveRowIndex;
            if (index < 0)
            {
                MessageBox.Show("请选择定点医药机构！");
                return;
            }

            string fixmedins_code = this.fpSpread_2501A.Cells[index, 2].Text;
            string fixmedins_name = this.fpSpread_2501A.Cells[index, 1].Text;

            if (string.IsNullOrEmpty(fixmedins_code))
            {
                MessageBox.Show("请选择定义医药机构！");
                return;
            }
            #endregion

            #region 查询医保登记记录，获取就诊ID
            FS.HISFC.Models.RADT.PatientInfo patientObj = null;
            if (!string.IsNullOrEmpty(patientInfo.ID))
            {
                patientObj = this.localMgr.GetSIPersonInfo(patientInfo.ID, patientInfo,"");
            }
            else
            {
                patientObj = patientInfo.Clone();
            }

            if (patientObj == null)
            {
                MessageBox.Show("没有找到患者医保信息！");
                return;
            }

            if (string.IsNullOrEmpty(patientObj.SIMainInfo.Psn_no))
            {
                MessageBox.Show("患者个人编号为空，请先查询患者信息！");
                return;
            }

            if (string.IsNullOrEmpty(this.txtZYrefl_old_mdtrt_id.Text))
            {
                this.txtZYrefl_old_mdtrt_id.Text = patientObj.SIMainInfo.Mdtrt_id;
            }

            #endregion

            #region 初始化医疗机构
            //InPatient1201 inPatient1201 = new InPatient1201();
            //Models.Request.RequestGzsiModel1201 requestGzsiModel1201 = new Neusoft.HIT.Plugins.SI.Gzsi.Models.Request.RequestGzsiModel1201();
            //Models.Response.ResponseGzsiModel1201 responseGzsiModel1201 = new Neusoft.HIT.Plugins.SI.Gzsi.Models.Response.ResponseGzsiModel1201();
            //Models.Request.RequestGzsiModel1201.Medinsinfo medinsinfo1201 = new Neusoft.HIT.Plugins.SI.Gzsi.Models.Request.RequestGzsiModel1201.Medinsinfo();

            //medinsinfo1201.fixmedins_type = (this.cmbZYFixType.SelectedItem as Neusoft.HISFC.Models.Base.Const).ID;
            //medinsinfo1201.fixmedins_code = "";
            //medinsinfo1201.fixmedins_name = this.txtZYFixName.Text;
            //requestGzsiModel1201.medinsinfo = medinsinfo1201;

            //if (inPatient1201.CallService("",requestGzsiModel1201, ref responseGzsiModel1201) < 0)
            //{
            //    MessageBox.Show(inPatient1201.ErrorMsg);
            //    return;
            //}

            //InPatient2501A inPatient2501A = new InPatient2501A();
            //Models.Request.RequestGzsiModel2501A requestGzsiModel2501A = new Neusoft.HIT.Plugins.SI.Gzsi.Models.Request.RequestGzsiModel2501A();
            //Models.Response.ResponseGzsiModel2501A responseGzsiModel2501A = new Neusoft.HIT.Plugins.SI.Gzsi.Models.Response.ResponseGzsiModel2501A();
            //Models.Request.RequestGzsiModel2501A.Refmedin refmedin2501A = new Neusoft.HIT.Plugins.SI.Gzsi.Models.Request.RequestGzsiModel2501A.Refmedin();

            //if (responseGzsiModel1201.infcode != "0")
            //{
            //    MessageBox.Show(inPatient1201.ErrorMsg);
            //    return;
            //}
            //else if (responseGzsiModel1201.infcode == "0" && responseGzsiModel1201.output.medinsinfo.Count == 0)
            //{
            //    MessageBox.Show("没有找到对应名称的定点医疗机构!");
            //    return;
            //}
            //else
            //{
            //    using (frmTransferBakDetail frmDel = new frmTransferBakDetail(responseGzsiModel1201.output.medinsinfo))
            //    {
            //        frmDel.ShowDialog();
            //        if (frmDel.DialogResult == DialogResult.OK)
            //        {
            //            refmedin2501A = frmDel.refmedin2501A;
            //        }
            //        else
            //        {
            //            return;
            //        }
            //    }
            //}
            #endregion

            InPatient2501A inPatient2501A = new InPatient2501A();
            Models.Request.RequestGzsiModel2501A requestGzsiModel2501A = new Models.Request.RequestGzsiModel2501A();
            Models.Response.ResponseGzsiModel2501A responseGzsiModel2501A = new ResponseGzsiModel2501A();
            Models.Request.RequestGzsiModel2501A.Refmedin refmedin2501A = new Models.Request.RequestGzsiModel2501A.Refmedin();

            refmedin2501A.psn_no = patientObj.SIMainInfo.Psn_no;//个人编号
            refmedin2501A.insutype = patientObj.SIMainInfo.Insutype;//险种类型
            refmedin2501A.tel = this.txtZYtel.Text;//联系电话 
            refmedin2501A.addr = this.txtZYaddr.Text;//联系地址
            refmedin2501A.insu_optins = (this.cmbZYinsu_optins.Tag == null ? "" : this.cmbZYinsu_optins.Tag.ToString());//参保机构医保区划
            refmedin2501A.diag_code = this.cmbZYDiag1.SelectedItem == null ? "" : (this.cmbZYDiag1.SelectedItem as FS.FrameWork.Models.NeuObject).ID;//诊断代码
            refmedin2501A.diag_name = this.cmbZYDiag1.SelectedItem == null ? "" : (this.cmbZYDiag1.SelectedItem as FS.FrameWork.Models.NeuObject).Name;//诊断名称
            refmedin2501A.dise_cond_dscr = this.txtZYdise_cond_dscr.Text;//疾病病情描述
            refmedin2501A.reflin_medins_no = fixmedins_code;//转往定点医药机构编号
            refmedin2501A.reflin_medins_name = fixmedins_name;//转往医院名称
            refmedin2501A.mdtrtarea_admdvs = (this.cmbZYMdtrtarea.Tag == null ? "" : this.cmbZYMdtrtarea.Tag.ToString());//就医地行政区划
            refmedin2501A.refl_type = (this.ncbReflType.Tag == null ? "" : this.ncbReflType.Tag.ToString());//转诊类型
            refmedin2501A.refl_old_mdtrt_id = this.txtZYrefl_old_mdtrt_id.Text;
            refmedin2501A.refl_date = this.dtZYrefl_date.Value.Date.ToShortDateString();
            refmedin2501A.begndate = this.dtZYbegndate.Value.Date.ToShortDateString();
            refmedin2501A.enddate = this.dtZYenddate.Value.Date.ToShortDateString();
            refmedin2501A.refl_rea = this.ntbReflRea.Text;
            refmedin2501A.refl_opnn = this.ntbReflOpnn.Text;

            requestGzsiModel2501A.refmedin = refmedin2501A;

            if (inPatient2501A.CallService(requestGzsiModel2501A, ref responseGzsiModel2501A) < 0)
            {
                MessageBox.Show(inPatient2501A.ErrorMsg);
                return;
            }


            if (responseGzsiModel2501A.infcode != "0")
            {
                MessageBox.Show(responseGzsiModel2501A.err_msg);
                return;
            }

            //if (responseGzsiModel2501A.output != null)
            //{
            //    if (this.localMgr.InsertBakInfo(patientObj.SIMainInfo.Psn_no, patientObj.IDCard, "2501AA",
            //        responseGzsiModel2501A.output.result.trt_dcla_detl_sn, "转[" + refmedin2501A.reflin_medins_no + "]" + refmedin2501A.reflin_medins_name) < 0)
            //    {
            //        MessageBox.Show("插入备案信息失败!" + this.localMgr.Err);
            //        return;
            //    }
            //    this.txtZYBakNo.Text = responseGzsiModel2501A.output.result.trt_dcla_detl_sn;
            //    MessageBox.Show("患者转院备案成功！备案号：" + responseGzsiModel2501A.output.result.trt_dcla_detl_sn);
            //}
            //else
            //{
            //    MessageBox.Show("患者转院备案成功！");
            //}

            MessageBox.Show("患者转院备案成功！备案号：" + responseGzsiModel2501A.output.result.trt_dcla_detl_sn);
            //pInfo.SIMainInfo.Trt_dcla_detl_sn2501 = this.txtZYBakNo.Text;
            //pInfo.SIMainInfo.User01 = this.txtZYMemo1.Text;
            //pInfo.SIMainInfo.Memo = this.txtZYMemo2.Text;
            //PrintZY(refmedin2501A);
        }

        private void btClearZY_Click(object sender, EventArgs e)
        {

        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            if (this.cmbmdtrt_cert_type.Tag == null || string.IsNullOrEmpty(this.cmbmdtrt_cert_type.Tag.ToString()))
            {
                MessageBox.Show("请选择证件类型！");
                this.cmbmdtrt_cert_type.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.txtIDNO.Text))
            {
                MessageBox.Show("请输入证件号！");
                this.txtIDNO.Focus();
                return;
            }

            string mdtrt_cert_type = this.cmbmdtrt_cert_type.Tag.ToString();
            Patient1101 patient1101 = new Patient1101();
            Models.Request.RequestGzsiModel1101 requestGzsiModel1101 = new API.GZSI.Models.Request.RequestGzsiModel1101();
            Models.Response.ResponseGzsiModel1101 responseGzsiModel1101 = new Models.Response.ResponseGzsiModel1101();
            Models.Request.RequestGzsiModel1101.Data data = new API.GZSI.Models.Request.RequestGzsiModel1101.Data();
            data.mdtrt_cert_type = mdtrt_cert_type;//就诊类型
            data.mdtrt_cert_no = this.txtIDNO.Text;//卡号
            data.card_sn = "";
            data.psn_cert_type = "";// (mdtrt_cert_type == "02" ? "1" : "99");//就诊类型
            data.psn_name = "";
            data.certno = this.txtIDNO.Text;//卡号
            requestGzsiModel1101.data = data;
            if (patient1101.CallService(requestGzsiModel1101, ref responseGzsiModel1101) < 0)
            {
                MessageBox.Show("获取人员信息失败！" + patient1101.ErrorMsg);
                return;
            }
            if (responseGzsiModel1101.infcode != "0")
            {
                MessageBox.Show("获取人员信息失败！" + responseGzsiModel1101.err_msg);
                return;
            }
            if (responseGzsiModel1101 == null || responseGzsiModel1101.output == null)
            {
                MessageBox.Show("获取人员信息失败!" + responseGzsiModel1101.err_msg);
                return;
            }

            this.fpSpread_1101A.RowCount = 0;
            this.fpSpread_1101B.RowCount = 0;
            List<Models.Baseinfo> dataList = new List<Models.Baseinfo>();
          
            if (responseGzsiModel1101.output.baseinfo != null) {
                dataList.Add(responseGzsiModel1101.output.baseinfo);
                Common.Function.ShowOutDateToFarpoint<Models.Baseinfo>(this.fpSpread_1101A, dataList);
            }

            if (responseGzsiModel1101.output.insuinfo != null) {
                Common.Function.ShowOutDateToFarpoint<Models.Insuinfo>(this.fpSpread_1101B, responseGzsiModel1101.output.insuinfo);
            }
        }

        private void bCancelMZIn_Click(object sender, EventArgs e)
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(patientInfo.SIMainInfo.Psn_no))
            {
                MessageBox.Show("请先查询患者信息");
                return;
            }
            if (string.IsNullOrEmpty(this.txtMZRegID.Text))
            {
                MessageBox.Show("请输入就诊ID");
                return;
            }

            OutPatient2202 outPatient2202 = new OutPatient2202();
            Models.Request.RequestGzsiModel2202 requestGzsiModel2202 = new Models.Request.RequestGzsiModel2202();
            Models.Request.RequestGzsiModel2202.Mdtrtinfo tempData = new Models.Request.RequestGzsiModel2202.Mdtrtinfo();
            Models.Response.ResponseGzsiModel2202 responseGzsiModel2202 = new ResponseGzsiModel2202();
            tempData.mdtrt_id = this.txtMZRegID.Text;
            tempData.psn_no = patientInfo.SIMainInfo.Psn_no;
            tempData.ipt_otp_no = patientInfo.ID + patientInfo.SIMainInfo.BalNo;
            requestGzsiModel2202.data = tempData;

            if (outPatient2202.CallService(requestGzsiModel2202, ref responseGzsiModel2202) < -1)
            {
                MessageBox.Show(outPatient2202.ErrorMsg);
                return;
            }
            if (responseGzsiModel2202.infcode != "0")
            {
                MessageBox.Show(responseGzsiModel2202.err_msg);
                return;
            }

            if (!string.IsNullOrEmpty(patientInfo.ID))
            {

                if (this.localMgr.CancelOutPatientRegInfo(this.txtMZRegID.Text) < 0)
                {
                    MessageBox.Show(this.localMgr.Err);
                    return;
                }
            }

            MessageBox.Show("入院撤销成功！");
            //QueryInfo();//刷新
            return;
        }

        private void bCancelMZBal_Click(object sender, EventArgs e)
        {

            if (this.patientInfo == null || string.IsNullOrEmpty(patientInfo.SIMainInfo.Psn_no))
            {
                MessageBox.Show("请先查询患者信息");
                return;
            }
            if (string.IsNullOrEmpty(this.txtMZRegID.Text))
            {
                MessageBox.Show("请输入就诊ID");
                return;
            }
            if (string.IsNullOrEmpty(this.txtMZFeeID.Text))
            {
                MessageBox.Show("请输入结算ID");
                return;
            }

            OutPatient2208 outPatient2208 = new OutPatient2208();
            Models.Request.RequestGzsiModel2208 requestGzsiModel2208 = new API.GZSI.Models.Request.RequestGzsiModel2208();
            Models.Response.ResponseGzsiModel2208 responseGzsiModel2208 = new API.GZSI.Models.Response.ResponseGzsiModel2208();
            Models.Request.RequestGzsiModel2208.Mdtrtinfo mdtrtinfo2208 = new API.GZSI.Models.Request.RequestGzsiModel2208.Mdtrtinfo();

            mdtrtinfo2208.setl_id =this.txtMZFeeID.Text; //结算ID
            mdtrtinfo2208.mdtrt_id = this.txtMZRegID.Text; //就诊ID 
            mdtrtinfo2208.psn_no = patientInfo.SIMainInfo.Psn_no; //人员编号
            requestGzsiModel2208.data = new API.GZSI.Models.Request.RequestGzsiModel2208.Mdtrtinfo();
            requestGzsiModel2208.data = mdtrtinfo2208;

            int ret = outPatient2208.CallService(requestGzsiModel2208, ref responseGzsiModel2208, SerialNumber, strTransVersion, strVerifyCode);

            if (ret == -1)
            {
                MessageBox.Show(this.localMgr.Err);
                return;
            }

            MessageBox.Show("门诊结算撤销成功！");
            //QueryInfo();//刷新
            return;
        }

        private void cmbInsuplc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Models.UserInfo.Instance.insuplc = this.cmbInsuplc.Tag.ToString();
        }

        private void bCZ_Click(object sender, EventArgs e)
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(patientInfo.SIMainInfo.Psn_no))
            {
                MessageBox.Show("请先查询患者信息");
                return;
            }
            if (string.IsNullOrEmpty(this.tbOmsgid.Text))
            {
                MessageBox.Show("请输入原发送方报文ID");
                return;
            }
            if (string.IsNullOrEmpty(this.tbOinfno.Text))
            {
                MessageBox.Show("请输入原交易编号");
                return;
            }

            Patient2601 patient2601 = new Patient2601();
            Models.Request.RequestGzsiModel2601 requestGzsiModel2601 = new API.GZSI.Models.Request.RequestGzsiModel2601();
            Models.Response.ResponseGzsiModel2601 responseGzsiModel2601 = new API.GZSI.Models.Response.ResponseGzsiModel2601();
            Models.Request.RequestGzsiModel2601.Data mdtrtinfo2601 = new API.GZSI.Models.Request.RequestGzsiModel2601.Data();

            mdtrtinfo2601.omsgid = this.tbOmsgid.Text; //原发送方报文ID
            mdtrtinfo2601.oinfno = this.tbOinfno.Text; //原交易编号 
            mdtrtinfo2601.psn_no = patientInfo.SIMainInfo.Psn_no; //人员编号
            requestGzsiModel2601.data = new API.GZSI.Models.Request.RequestGzsiModel2601.Data();
            requestGzsiModel2601.data = mdtrtinfo2601;

            int ret = patient2601.CallService(requestGzsiModel2601, ref responseGzsiModel2601, SerialNumber, strTransVersion, strVerifyCode);

            if (ret == -1)
            {
                MessageBox.Show(this.localMgr.Err);
                return;
            }

            MessageBox.Show("冲正成功！");
            //QueryInfo();//刷新
            return;
        }
    }
}
