using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Xml;
using System.IO;

namespace ProvinceAcrossSI.Controls
{
    public partial class frmInpatientRegYiDi : Form
    {
        #region 变量

        /// <summary>
        /// 住院业务实体
        /// </summary>
        ProvinceAcrossSI.Business.InPatient.InPatientService inPatientService = new ProvinceAcrossSI.Business.InPatient.InPatientService();

        /// <summary>
        /// 公用业务实体
        /// </summary>
        ProvinceAcrossSI.Business.Common.CommonService comService = new ProvinceAcrossSI.Business.Common.CommonService();

        /// <summary>
        /// 中心返回患者医保信息
        /// </summary>
        ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();

        /// <summary>
        /// 克隆首次赋值时的患者HIS信息
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 是否为登记界面查询调用
        /// </summary>
        bool isQueryByBtn = true;

        public InPatientReg inpateintReg;

        ProvinceAcrossSI.ProvinceAcrossSIDatabase SIMgr = new ProvinceAcrossSIDatabase();
        /// <summary>
        /// 是否已经查询住院资格
        /// </summary>
        bool isQuery = false;

        /// <summary>
        /// 是否点击过确定按钮【防止快捷键关闭医保查询窗口】
        /// </summary>
        public bool isClickBtnEnter = false;

        /// <summary>
        /// 错误状态
        /// </summary>
        public int ErrCode { get; set; }

        private string errMsg = "";

        private string msg = "";

        private ArrayList alDiag = new ArrayList();
        private ArrayList alZyyy = new ArrayList();
        private ArrayList alBzlx = new ArrayList();
        public string ErrMsg
        {
            set
            {
                this.errMsg = value;
            }
            get
            {
                return this.errMsg;
            }
        }

        public string Msg
        {
            set
            {
                this.msg = value;
            }
            get
            {
                return this.msg;
            }
        }
        #endregion
        private CommonController commonController = CommonController.CreateInstance();

        public frmInpatientRegYiDi()
        {
            InitializeComponent();
            //获得异地医保参数
            this.comService.GetYdConstParm(ref this.personInfo);
            
        }
       
        /// <summary>
        /// 初始化ICD10
        /// </summary>
        private void InitDiagnoses()
        {
            alDiag = this.SIMgr.QueryICD();
            if (alDiag.Count < 1)
            {
                MessageBox.Show("查询ICD10出错", "异地医保");
                return;
            }
        }

        /// <summary>
        /// 初始化补助类型
        /// </summary>
        private void InitBzlx()
        {
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "1";
            obj1.Name = "普通";
            alBzlx.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "2";
            obj2.Name = "抢救期间";
            alBzlx.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "3";
            obj3.Name = "非抢救期间";
            alBzlx.Add(obj3);

            
        }
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
       
        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="patient">住院患者实体</param>
        /// <returns></returns>
        public int InitControl(FS.HISFC.Models.RADT.PatientInfo patient)
        {
           
            if (string.IsNullOrEmpty(patient.ID))
            {
                this.ErrMsg = "  【请选择住院患者！】";
                this.ErrCode = -1;
                return -1;
            }
            patient = this.radtIntegrate.GetPatientInfomation(patient.ID);// {80867F3F-AE91-4525-82AF-0618AB01C92B}

            if (string.IsNullOrEmpty(patient.Kin.Name))
            {
                this.ErrMsg = "  【请填写患者的联系人！】";
                this.ErrCode = -1;
                return -1;
            }
            if (string.IsNullOrEmpty(patient.Kin.RelationPhone))
            {
                this.ErrMsg = "  【请填写患者的联系人电话！】";
                this.ErrCode = -1;
                return -1;
            }

            if (string.IsNullOrEmpty(patient.ClinicDiagnose))
            {
                this.ErrMsg = "  【请填写门诊诊断！】";
                this.ErrCode = -1;
                return -1;
            }
            if (!string.IsNullOrEmpty(patient.ID) && (patient.Kin.Name.Trim().Length == 0 ||
                patient.Kin.RelationPhone.Trim().Length == 0 ||
                patient.ClinicDiagnose.Trim().Length == 0))
            {
                FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                patient = radtIntegrate.GetPatientInfomation(patient.ID);
            }
            this.ControlBox = false;//隐藏关闭按钮            
            //this.ps.InsuredAreaCode = "";
            //this.ps.InsuredCenterAreaCode = "";
            //this.ps.HospitalCode = "";
            //this.ps.HospitalizeAreaCode = "";
            //this.ps.HospitalizeCenterAreaCode = "";
            InitDiagnoses();
            this.cmbDiag1.AddItems(alDiag);
            this.cmbDiag1.Text = "";
            foreach (FS.FrameWork.Models.NeuObject diagn in alDiag)
            {
                if (diagn.Name == patient.ClinicDiagnose)
                {
                    cmbDiag1.Text = diagn.Name;
                    break;
                }
            }
            this.txtIdNo.Text = patient.IDCard;
            this.txtMCarNo.Text = patient.SSN;     
            //this.txtJYD.Text = this.personInfo.HospitalizeAreaCode;
            //this.txtJYFZX.Text = this.personInfo.HospitalizeCenterAreaCode;
            this.txtYYBM.Text = "4400001059978";
            //this.txtCBD.Text = "";//参保地
            this.cmbJZLB.AddItems(this.comService.QueryTYPEFormComDictionary("YD_REG_TYPE"));
            this.cmbJZLB.Tag = "25";
            this.cmbJZLB.Text = "异地住院";
            this.cmbClass.AddItems(this.comService.QueryTYPEFormComDictionary("YD_CLASS_TYPE"));
            this.cmbClass.Tag = "01";
            this.cmbClass.Text = "居民身份证(户口簿)";
            System.Collections.ArrayList alTemp = this.comService.QueryTYPEFormComDictionary("YD_AREA_CODE");
            this.cmbHospitalizeArea.AddItems(alTemp);
            this.cmbHospitalizeArea.Tag = "440600";
            this.cmbHospitalizeArea.Text = "佛山市";
            this.cmbInsuredArea.AddItems(alTemp);            
            this.cmbDoctor.AddItems(commonController.QueryEmployee(EnumEmployeeType.D));
            this.cmbDoctor.Tag = patient.DoctorReceiver.ID;
            this.cmbDoctor.Text = this.commonController.GetEmployeeName(patient.DoctorReceiver.ID);
            this.txtdocIddeno.Text = this.inPatientService.GetDocIDenno(cmbDoctor.Tag.ToString());
            //联系人、联系电话、门诊诊断是否为空判断，因为住院登记接口要求不能为空
            if (patient.Kin.Name.Trim().Length == 0 ||
                patient.Kin.RelationPhone.Trim().Length == 0 ||
                patient.ClinicDiagnose.Trim().Length == 0)
            {
                this.ErrMsg = "\n异地医保" + "\n联系人、联系电话、门诊诊断均不能为空！";
                return -1;
            }
            p = patient.Clone();
            this.InitZyyy();
            this.InitBzlx();
            this.cmbZYYY.AddItems(alZyyy);
            this.cmbZYYY.Tag = "";
            this.cmbZYYY.Text = "";
            this.cmbBZLX.AddItems(alBzlx);
            this.ErrCode = -1;
            return 1;
        }

        private void InitZyyy()
        {
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "0";
            obj1.Name = "普通原因住院";
            alZyyy.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "1";
            obj2.Name = "恶性肿瘤住院";
            alZyyy.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "2";
            obj3.Name = "心脑疾病手术治疗住院";
            alZyyy.Add(obj3);
            FS.HISFC.Models.Base.Item obj4 = new FS.HISFC.Models.Base.Item();
            obj4.ID = "3";
            obj4.Name = "肝、肾和骨髓移植住院";
            alZyyy.Add(obj4);
            FS.HISFC.Models.Base.Item obj5 = new FS.HISFC.Models.Base.Item();
            obj5.ID = "4";
            obj5.Name = "外伤住院";
            alZyyy.Add(obj5);
            FS.HISFC.Models.Base.Item obj6 = new FS.HISFC.Models.Base.Item();
            obj6.ID = "5";
            obj6.Name = "生育住院";
            alZyyy.Add(obj6);
            FS.HISFC.Models.Base.Item obj7 = new FS.HISFC.Models.Base.Item();
            obj7.ID = "6";
            obj7.Name = "安胎住院";
            alZyyy.Add(obj7);
        }

        /// <summary>
        /// 根据医保患者实体设置lbl内容
        /// </summary>
        public void SetLlb(ProvinceAcrossSI.Objects.SIPersonInfo p)
        {
            this.txtCenterMCarNo.Text = p.MCardNo;
            this.txtCenterIdNo.Text = p.IdenNo;
            this.txtCenterName.Text = p.Name;
            this.txtCenterSex.Text = p.Sex;
            this.txtCenterBirth.Text = p.Birth;
            this.txtCenterPersonType.Text = p.PersonType;
            this.txtCenterRQtype.Text = p.RQtype;
            this.txtCenterGWYflag.Text = p.GWYflag;
            this.txtCenterCompanyCode.Text = p.CompanyCode;
            this.txtCenterCompanyName.Text = p.CompanyName;
            this.txtCenterCBFZX.Text = p.InsuredCenterAreaCode;
            this.txtCenterPayYears.Text = p.PayYears;
            this.txtCenterRestAccount.Text = p.RestAccount;
            this.txtCenterSumCostLine.Text = p.CostLine;
            this.txtCenterInTimes.Text = p.InTimes;
            this.txtCenterJBYLBCZFXE.Text = p.LimitCostJBYL;
            this.txtCenterDBYLBCZFXE.Text = p.LimitCostDBYL;
            this.txtCenterGWYBCZFXE.Text = p.LimitCostGWY;
            this.txtCenterSumJBYL.Text = p.SumCostJBYL.ToString();
            this.txtCenterSumDBYL.Text = p.SumCostDBYL;
            this.txtCenterReturnFlag.Text = p.ReturnsFlag;
            this.txtCenterChangeFlag.Text = p.ChangeOutFlag;
            this.txtCenterChangeOutClinicNo.Text = p.ChangeOutClinicNo;
            this.txtCenterChangeOutHosName.Text = p.ChangeOutHosName;
            this.txtCenterChangeOutDate.Text = p.ChangeDate;
            this.txtCenterChangeInHosName.Text = p.ChangeInHosName;
            this.txtCenterSeeType.Text = p.SeeDocType;
            this.txtCenterChangeType.Text = p.ChangeType;
            this.txtCenterChangeDiagn.Text = p.ChangeDiagn;
            this.txtCenterChangePassFlag.Text = p.ChangePassFlag;
            this.txtCenterChangeReason.Text = p.ChangeReason;

        }

        #region 事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string status = string.Empty;
            string msg = string.Empty;
            this.personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
            if (string.IsNullOrEmpty(p.ID))
            {
                this.ErrMsg = "  【请选择住院患者！】";
                MessageBox.Show(" 【请选择住院患者！】");
                this.ErrCode = -1;
                return;
            }
            //this.p.SSN = this.txtMCarNo.Text;
            if (string.IsNullOrEmpty(this.p.IDCard))
            {
                MessageBox.Show("验证失败！患者【身份证号】为空！请输入身份证号");
                return;
            }
            //if (string.IsNullOrEmpty(this.p.SSN))
            //{
            //    MessageBox.Show("验证失败！患者【社保卡号】为空！请输入社保卡号");
            //    return;
            //}
            if(this.cmbClass.Tag == "90" && this.p.IDCard != this.p.SSN)
            {
                MessageBox.Show("当证件类型为“社会保障卡”时，社保卡号和身份证号需一致","异地医保");
                return;
            }
            if(string.IsNullOrEmpty(this.p.SSN))
            {
                this.p.SSN = "-";
            }

            //住院号8位处理

            //int pnLength = this.p.PID.PatientNO.Length;
            //int needLength = 8;//需要自右向左几位
            //this.p.PID.PatientNO = this.p.PID.PatientNO.Substring(pnLength - needLength, needLength);

            //if (string.IsNullOrEmpty(this.cmbInsuredArea.Text))
            //{
            //    MessageBox.Show("请先选择参保地！", "异地医保");
            //}
            //else
            //{
            //    personInfo.InsuredAreaCode = this.cmbInsuredArea.Tag.ToString();
            //}
            //MessageBox.Show("异地就医城市：" + p.SIMainInfo.AnotherCity.ID);
            if (p.SIMainInfo.AnotherCity == null)
            {
                MessageBox.Show("请先操作【卡鉴权】，鉴别卡信息1！", "异地医保");
                return;
            }
            if (string.IsNullOrEmpty(p.SIMainInfo.AnotherCity.ID))
            {
                MessageBox.Show("请先操作【卡鉴权】，鉴别卡信息2！", "异地医保");
                return;
            }
            personInfo.InsuredAreaCode = p.SIMainInfo.AnotherCity.ID;
            if (string.IsNullOrEmpty(this.cmbClass.Text))
            {
                MessageBox.Show("请先选择证件类别！", "异地医保");
                return;
            }
            else
            {
                personInfo.ZJLX = this.cmbClass.Tag.ToString();
            }

            this.inPatientService.YDInPatientAccreditation(this.p, ref this.personInfo, ref status, ref msg);


            switch (status)
            {
                case "-1":
                    this.ErrCode = -1;
                    MessageBox.Show(msg, "异地医保");
                    break;
                case "3":
                    this.ErrCode = -1;
                    MessageBox.Show(msg, "异地医保");
                    break;
                default:
                    break;
            }

            if (status == "0" || status=="1")
            {
                MessageBox.Show("该患者符合异地医保身份，请继续登记操作。", "异地医保");
                this.isQuery = true;
            }
            else
            {
                MessageBox.Show(msg, "异地医保");
            }
            this.SetLlb(this.personInfo);
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (!this.isQuery)
            {
                MessageBox.Show("请先查询患者住院结算资格!", "异地医保");
                return;
            }

            //if (this.ErrCode < 1)
            //{
            //    MessageBox.Show("当前患者无住院结算资格!", "异地医保");
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.cmbJZLB.Text))
            //{
            //    MessageBox.Show("请先选择就诊类别！", "异地医保");
            //    return;
            //}
            //else
            //{
            //    personInfo.SeeDocType = this.cmbJZLB.Tag.ToString();
 
            //}
            //if (string.IsNullOrEmpty(this.cmbInsuredArea.Text))
            //{
            //    MessageBox.Show("请先选择参保地！", "异地医保");
            //    return;
            //}
            //else
            //{
            //    personInfo.InsuredAreaCode = this.cmbInsuredArea.Tag.ToString();
            //}
            if (this.cmbDiag1.Tag == null)
            {
                MessageBox.Show("请先选择诊断！", "异地医保");
                return;
            }
            else
            {
                personInfo.InDiagn = this.cmbDiag1.Tag.ToString();
            }
            if (this.cmbZYYY.Tag == null)
            {
                MessageBox.Show("请先选择住院原因！", "异地医保");
                return;
            }
            else
            {
                personInfo.in_reason = this.cmbZYYY.Tag.ToString();
            }
            if (string.IsNullOrEmpty(this.cmbBZLX.Text))
            {
                MessageBox.Show("请先选择补助类型！", "异地医保");
                return;
            }
            else
            {
                personInfo.bzlx = this.cmbBZLX.Tag.ToString();

            }
            //personInfo.GSFlag = Convert.ToInt32(this.chbGS.Checked).ToString();
            //personInfo.SYFlag = Convert.ToInt32(this.chbSY.Checked).ToString();
            //if (personInfo.GSFlag.Equals("1"))
            //{
            //    personInfo.SIType = this.GetSiTypeCode("工伤医保");
            //}
            //if (personInfo.SYFlag.Equals("1"))
            //{
            //    personInfo.SIType = this.GetSiTypeCode("生育医保");
            //}
            personInfo.docIdenno = this.txtdocIddeno.Text;
            string status = string.Empty;
            string msg = string.Empty;

            int flag = this.inpateintReg(this.p, personInfo, ref status, ref msg);

            if (FS.FrameWork.Function.NConvert.ToDecimal(status)>=0)
            {
                this.ErrMsg = msg;
                this.ErrCode = 1;
            }
            else
            {
                this.ErrMsg = msg;
                this.ErrCode = -1;
                MessageBox.Show(msg, "异地医保");
                return;
            }

            this.isClickBtnEnter = true;
            this.Close();
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            this.ErrCode = 0;
            this.ErrMsg = "\n操作取消成功!";
            this.Close();
        }
        #endregion

        private void frmInpatientRegYiDi_TextChanged(object sender, EventArgs e)
        {
            if (!this.Text.Contains("|"))
            {
                return;
            }

            //this.inpateintReg += this.DefaultFunction;
            this.btnEnter.Visible = false;
            this.isQueryByBtn = true;

            this.p = new FS.HISFC.Models.RADT.PatientInfo();

            this.p.Name = this.Text.Split('|')[0];
            this.p.SSN = this.Text.Split('|')[1];
            this.p.IDCard = this.Text.Split('|')[2];
            this.p.PID.ID = this.Text.Split('|')[3];

            this.txtCenterName.Text = this.p.Name;
            this.txtIdNo.Text = this.p.IDCard;
            this.txtMCarNo.Text = this.p.SSN;

            this.txtCenterMCarNo.Text = this.p.SSN;
            this.txtCenterIdNo.Text = this.p.IDCard;

            ProvinceAcrossSIDatabase mgr = new ProvinceAcrossSIDatabase();
            this.p.PVisit.InTime = mgr.GetDateTimeFromSysDateTime();
        }


        private void SetP()
        {
            this.p.Name = this.txtCenterName.Text;
            this.p.IDCard = this.txtIdNo.Text;
            this.p.SSN = this.txtMCarNo.Text;
        }

        private void cmbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string idenno = string.Empty;
                idenno = this.inPatientService.GetDocIDenno(cmbDoctor.Tag.ToString());
                this.txtdocIddeno.Text = idenno;
            }
        }
        private int ReadCard()
        {
            if (string.IsNullOrEmpty(p.ID))
            {
                this.ErrMsg = "  【请选择住院患者！】";
                MessageBox.Show(" 【请选择住院患者！】");
                this.ErrCode = -1;
                return -1;
            }
            p = this.radtIntegrate.GetPatientInfomation(p.ID);// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            byte[] pInfo = new byte[1024];
            string resp = string.Empty;
            long result = ProvinceAcrossSI.Business.ReadCardFunction.iReadCardBas(1, pInfo);

            if (result != 0)  //失败 调用1903 之后调用1904 直接调用1904
            {
                result = ProvinceAcrossSI.Business.ReadCardFunction.iReadCardBas_HSM_Step1(1, pInfo);
                if (result <= 0)
                {
                    this.errMsg = "内部认证失败1！" + System.Text.Encoding.Default.GetString(pInfo);
                    MessageBox.Show(this.errMsg);
                    return -1;
                }


                resp = Encoding.Default.GetString(pInfo);
                string authenInfo = string.Empty;

                if (resp.Split('|').Length < 6)
                {
                    MessageBox.Show("刷卡失败！原因："+resp);
                    return -1;
                }
                result = this.InternalAuthen(resp.Split('|'), ref authenInfo);

                if (result <= 0)
                {
                    this.errMsg = "内部认证失败！";
                    MessageBox.Show(this.errMsg);
                    return -1;
                }

                result = ProvinceAcrossSI.Business.ReadCardFunction.iReadCardBas_HSM_Step2(Encoding.Default.GetBytes(authenInfo), pInfo);
                if (result < 0)
                {
                    this.errMsg = "内部认证失败2" + System.Text.Encoding.Default.GetString(Encoding.Default.GetBytes(authenInfo));
                    MessageBox.Show(this.errMsg);
                    return -1;
                }

            }

            resp = Encoding.Default.GetString(pInfo);

            if (resp.Split('|').Length < 6)
            {
                MessageBox.Show("刷卡失败！原因1：" + resp);
                return -1;
            }
            FS.FrameWork.Models.NeuObject neuObj = null;

            result = this.CardAuthen(resp.Split('|'), ref neuObj);

            if (result <= 0)
            {
                this.errMsg = "卡鉴权失败" + this.errMsg;
                MessageBox.Show(this.errMsg);
                return -1;
            }

            p.Name = neuObj.Name;
            p.SIMainInfo.AnotherCity.ID = neuObj.ID;
            p.SSN = neuObj.Memo;

            MessageBox.Show("卡鉴权成功！");
            return 1;
        }

        /// <summary>
        /// 内部认证
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="authenInfo"></param>
        /// <returns></returns>
        private int InternalAuthen(string[] pInfo, ref string authenInfo)
        {
            //1903
            string inxml = @"<?xml version='1.0' encoding='GBK'?><input><sign>{0}</sign><transid>{1}</transid>
                                <aab299>{2}</aab299><yab600>{3}</yab600><aab301>{4}</aab301><yab060>{5}</yab060>
                                <aac002>{6}</aac002><aaz507>{7}</aaz507><aaa254>{8}</aaa254><aaz501>{9}</aaz501>
                                <aaa311>{10}</aaa311><aaa312>{11}</aaa312><aaa313>{12}</aaa313>
                                <aaa314>{13}</aaa314></input>";

            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
            ProvinceAcrossSI.Funtion funMgr = new Funtion();
            funMgr.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            inxml = string.Format(inxml, "", "", personInfo.HospitalizeAreaCode, personInfo.HospitalizeCenterAreaCode,
                         pInfo[0], "", "", pInfo[1], pInfo[2], pInfo[3], pInfo[4], pInfo[5], pInfo[6], pInfo[7]);

            string outxml = inPatientService.CallService("1903", inxml);

            //MessageBox.Show(outxml);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(outxml);
            string code = doc.SelectSingleNode("result/errorcode").InnerText.Trim();


            if (FS.FrameWork.Function.NConvert.ToInt32(code) < 0) //失败
            {
                errMsg = doc.SelectSingleNode("result/errormsg").InnerText.Trim();
                return -1;
            }

            string innerAuth = doc.SelectSingleNode("result/output/aaa315").InnerText.Trim();//内部认证鉴别数据+原始信息

            string outerAuth = doc.SelectSingleNode("result/output/aaa316").InnerText.Trim(); //外部认证鉴别数据+原始信息
            authenInfo = innerAuth + "|" + outerAuth + "|";

            return 1;

        }

        /// <summary>
        /// 卡鉴权
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        private int CardAuthen(string[] pInfo, ref FS.FrameWork.Models.NeuObject patObj)
        {
            //1904
            string inxml = @"<?xml version='1.0' encoding='GBK' ?><input><sign>{0}</sign><transid>{1}</transid>
                            <aab299>{2}</aab299><yab600>{3}</yab600><aab301>{4}</aab301><yab060>{5}</yab060>
                            <aac002>{6}</aac002><aaz500>{7}</aaz500><aaz501>{8}</aaz501><aac003>{9}</aac003></input>";

            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
            ProvinceAcrossSI.Funtion funMgr= new Funtion();
            funMgr.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            //发卡地区行政区划代码（卡识别码前6位）、社会保障号码、卡号、卡识别码、姓名、卡复位信息（仅取历史字节）、规范版本、发卡日期、卡有效期、终端机编号、终端设备号。各数据项之间以“|”分割，且最后一个数据项以“|”结尾。
            //例如：639900|111111198101011110|X00000019|639900D15600000500BF7C7A48FB4966|张三|00814E43238697159900BF7C7A|1.00|20101001|20201001|410100813475|终端设备号|。
            inxml = string.Format(inxml, "", "", personInfo.HospitalizeAreaCode, personInfo.HospitalizeCenterAreaCode,
                pInfo[0], "", pInfo[1], pInfo[2], pInfo[3], pInfo[4]);

            string outxml = inPatientService.CallService("1904", inxml);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(outxml);
            string code = doc.SelectSingleNode("result/errorcode").InnerText.Trim();

            if (FS.FrameWork.Function.NConvert.ToInt32(code) < 0) //失败
            {
                errMsg = doc.SelectSingleNode("result/errormsg").InnerText.Trim();
                return -1;
            }
            patObj = new FS.FrameWork.Models.NeuObject();
            patObj.ID = doc.SelectSingleNode("result/output/aab301").InnerText.Trim();//参保地
            patObj.Name = pInfo[4]; //姓名
            patObj.Memo = pInfo[1]; //社保卡号

            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ReadCard();
        }
    }
}
