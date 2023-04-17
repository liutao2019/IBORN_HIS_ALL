using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FoShanYDSI.Controls
{
    public partial class YDInpatientBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public YDInpatientBalance()
        {
            InitializeComponent();
            InitDiagnoses();
            this.InitJZLX();
            //if(FS.FrameWork.Management.Connection.Operator
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                btUpLoadFeeDetail.Visible = true;
                btBalanceInfo.Visible = true;
                btOutReg.Visible = true;
                btInReg.Visible = true;
            }
        }
        

        #region 变量

        /// <summary>
        /// 查询患者信息状态
        /// </summary>
        int queryPatientFlag = -1;

        /// <summary>
        /// 查询医保信息状态
        /// </summary>
        int queryPersonFlag = -1;

        /// <summary>
        /// 查询费用明细状态
        /// </summary>
        int queryFeeDetailFlag = -1;

        /// <summary>
        /// 业务状态
        /// </summary>
        string status = "";

        /// <summary>
        /// 患者入出转业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 患者医保信息
        /// </summary>
        FoShanYDSI.Objects.SIPersonInfo ps = new FoShanYDSI.Objects.SIPersonInfo();

        /// <summary>
        /// 患者HIS信息
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

        FoShanYDSI.FoShanYDSIDatabase SIMgr = new FoShanYDSIDatabase();

        /// <summary>
        /// 公用业务实体
        /// </summary>
        FoShanYDSI.Business.Common.CommonService comService = new FoShanYDSI.Business.Common.CommonService();
        
        /// <summary>
        /// 佛山异地医保业务逻辑层
        /// </summary>
        FoShanYDSI.Business.InPatient.InPatientService inPatientService = new FoShanYDSI.Business.InPatient.InPatientService();
        /// <summary>
        /// 控制参数业务层 --com_controlargument
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 费用明细
        /// </summary>
        ArrayList feeDetails = new ArrayList();

        /// <summary>
        /// 结算类型
        /// </summary>
        ArrayList alBzlx = new ArrayList();

        /// <summary>
        /// 结算项目【中心】
        /// </summary>
        ArrayList arrCenterBalanceItem = new ArrayList();

        /// <summary>
        /// 省外合同单位// {80867F3F-AE91-4525-82AF-0618AB01C92B}
        /// </summary>
        private string pactCode = "";
        FoShanYDSI.Funtion function = new Funtion();

        /// <summary>
        /// 错误状态
        /// </summary>
        public int ErrCode { get; set; }

        private string errMsg = "";

        private string msg = "";

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
        /// <summary>
        /// 初始化ICD10
        /// </summary>
        private void InitDiagnoses()
        {
            FS.FrameWork.Models.NeuObject diagn = new FS.FrameWork.Models.NeuObject();
            ArrayList al = new ArrayList();
            al = this.SIMgr.QueryICD();
            if (al.Count < 1)
            {
                MessageBox.Show("查询ICD10出错", "异地医保");
                return;
            }
            
            this.cmbDiag1.AddItems(al);
            this.cmbDiag2.AddItems(al);
            this.cmbDiag3.AddItems(al);
            this.cmbDiag1.Text = "";
            this.cmbDiag2.Text = "";
            this.cmbDiag3.Text = "";


            pactCode = this.controlParam.GetControlParam<string>("SNYD01");// {80867F3F-AE91-4525-82AF-0618AB01C92B}
        }

        private void InitTrainsType()
        {
            #region 
            //就诊登记（0212）、
            //跨省外来就医费用结算（0304）
            //跨省外来就医结算回退（0305）、
            //跨省赴外就医费用结算（3105）、  
            //跨省赴外就医结算回退（3202）
            #endregion 
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "0212";
            obj1.Name = "就诊登记";
            alBzlx.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "0304";
            obj2.Name = "跨省外来就医费用结算";
            alBzlx.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "0305";
            obj3.Name = "跨省外来就医结算回退";
            FS.HISFC.Models.Base.Item obj4 = new FS.HISFC.Models.Base.Item();
            obj4.ID = "3105";
            obj4.Name = "跨省赴外就医费用结算";
            FS.HISFC.Models.Base.Item obj5 = new FS.HISFC.Models.Base.Item();
            obj5.ID = "3202";
            obj5.Name = "跨省赴外就医结算回退";
            

        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            return base.OnInit(sender, neuObject, param);
        }

        /// <summary>
        /// 清空函数
        /// </summary>
        private void Clear()
        {
            this.queryPatientFlag = -1;
            this.queryPersonFlag = -1;
            this.queryFeeDetailFlag = -1;
            this.feeDetails = new ArrayList();
            this.cmbDiag1.Text = "";
            this.cmbDiag2.Text = "";
            this.cmbDiag3.Text = "";
            this.lblName.Text = "";
            this.lblSex.Text ="";
            this.lblAge.Text = "";
            this.lblIdNo.Text = "";
            this.lblDept.Text = "";
            this.lblMCarNo.Text = "";
            this.lblDiagn.Text = "";//需要实现
            //this.lblSiType.Text = "";
            //this.lblPersonType.Text = "";
            this.lblIndate.Text = "";
            this.lblOutDate.Text = "";
            this.queryPersonFlag = 1;
            msg = "";
        }

        /// <summary>
        /// 初始化补助类型
        /// </summary>
        private void InitJZLX()
        {
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "1";
            obj1.Name = "正式结算";
            alBzlx.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "3";
            obj2.Name = "中途结算";
            alBzlx.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "0";
            obj3.Name = "补登";
            alBzlx.Add(obj3);

            this.cmbJSLX.AddItems(alBzlx);
        }
        public int SetPatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.Clear();
            if (patient == null || string.IsNullOrEmpty(patient.ID))
            {
                MessageBox.Show("住院信息为空！");
                return -1;
            } 
            FS.HISFC.Models.RADT.PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(patient.ID);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                MessageBox.Show("该患者不存在!请验证后输入");
                return -1;
            }
            this.p = patientTemp.Clone();
            this.SetPatientInfo();
            return 1;
        }
        /// <summary>
        /// 患者信息查询
        /// </summary>
        private void ucQueryPatientInfo_myEvent()
        {
            this.Clear();

            if (this.ucQueryPatientInfo.InpatientNo == null || this.ucQueryPatientInfo.InpatientNo == string.Empty)
            {
                MessageBox.Show("该患者不存在!请验证后输入");
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(this.ucQueryPatientInfo.InpatientNo);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                MessageBox.Show("该患者不存在!请验证后输入");
                return;
            }
            //if (patientTemp.PVisit.InState.ID.ToString() == "N")
            //{
            //    MessageBox.Show("该患者已经本地无费退院，不允许操作!");
            //    this.ucQueryPatientInfo.Focus();
            //    return;
            //}

            this.p = patientTemp.Clone();
            this.SetPatientInfo();
        }

        private int SetPatientInfo()
        {
            if (this.p == null)
            {
                return -1;
            }
            this.queryPatientFlag = 1;
            this.p.PID.PatientNO = this.p.PID.PatientNO;

            if (this.inPatientService.QueryInpatienRegInfo(this.p.ID, ref this.ps) < 1)
            {
                MessageBox.Show("查询不到患者住院登记信息，请先办理异地住院登记！", "佛山异地医保");
                //return;
            }
            else
            {

                if (this.inPatientService.QueryfeeDetails(this.p, ref this.feeDetails, ref msg) < 1)
                {
                    if (this.ps != null)
                    {
                        //00:待审核;01:审核通过;02:审核不通过;03:已结算
                        if (this.ps.InState == "03")
                        {
                            this.msg += "该患者已完成结算!";
                        }
                    }
                    MessageBox.Show(this.msg, "佛山异地医保");
                    //return;
                }
            }

            this.lblName.Text = p.Name;
            this.lblSex.Text = p.Sex.Name;
            this.lblAge.Text = p.Birthday.ToShortDateString();
            this.lblIdNo.Text = p.IDCard;
            this.lblDept.Text = p.PVisit.PatientLocation.Dept.Name;
            this.lblMCarNo.Text = p.SSN;
            FS.FrameWork.Models.NeuObject diagn = new FS.FrameWork.Models.NeuObject();
            //if (this.SIMgr.QueryDign(p.ID, ref diagn, ref msg) < 1)
            //{
            //    MessageBox.Show(msg, "佛山医保");
            //}
            //this.lblDiagn.Tag = diagn.ID;
            //this.lblDiagn.Text = diagn.Name;//需要实现
            //this.lblSiType.Text = this.GetSiType(ps.SIType);
            //this.lblPersonType.Text = this.GetPersonType(ps.PersonType);
            this.lblIndate.Text = p.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblOutDate.Text = p.PVisit.OutTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.cmbDiag1.Tag = this.ps.Diagn1;
            this.cmbDiag2.Tag = this.ps.Diagn2;
            this.cmbDiag3.Tag = this.ps.Diagn3;
            this.queryPersonFlag = 1;

            return 1;

        }

        private string GetPersonType(string code)
        {

            ArrayList al = this.comService.QueryTYPEFormComDictionary("YD_PERSON_TYPE");

            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.ID == ps.InsuredCenterAreaCode)
                {
                    return obj.Name;
                }
            }
            return null;
        }

        private string GetSiType(string code)
        {
            ArrayList arrRet = new System.Collections.ArrayList();
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "310";
            obj1.Name = "城镇职工基本医疗保险";
            arrRet.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "360";
            obj2.Name = "老红军医疗保障";
            arrRet.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "320";
            obj3.Name = "公务员医疗补助";
            arrRet.Add(obj3);
            FS.HISFC.Models.Base.Item obj4 = new FS.HISFC.Models.Base.Item();
            obj4.ID = "370";
            obj4.Name = "企业补充医疗保险";			
            arrRet.Add(obj4);
            FS.HISFC.Models.Base.Item obj5 = new FS.HISFC.Models.Base.Item();
            obj5.ID = "330";
            obj5.Name = "大额医疗费用补助";
            arrRet.Add(obj5);
            FS.HISFC.Models.Base.Item obj6 = new FS.HISFC.Models.Base.Item();
            obj6.ID = "380";
            obj6.Name = "新型农村合作医疗";
            arrRet.Add(obj6);
            FS.HISFC.Models.Base.Item obj7 = new FS.HISFC.Models.Base.Item();
            obj7.ID = "340";
            obj7.Name = "离休人员医疗保障";
            arrRet.Add(obj7);
            FS.HISFC.Models.Base.Item obj8 = new FS.HISFC.Models.Base.Item();
            obj8.ID = "390";
            obj8.Name = "城乡居民基本医疗保险";
            arrRet.Add(obj8);
            FS.HISFC.Models.Base.Item obj9 = new FS.HISFC.Models.Base.Item();
            obj9.ID = "350";
            obj9.Name = "一至六级残疾军人医疗补助";
            arrRet.Add(obj9);
            FS.HISFC.Models.Base.Item obj10 = new FS.HISFC.Models.Base.Item();
            obj10.ID = "391";
            obj10.Name = "城镇居民基本医疗保险";
            arrRet.Add(obj10);
            FS.HISFC.Models.Base.Item obj11 = new FS.HISFC.Models.Base.Item();
            obj11.ID = "410";
            obj11.Name = "工伤保险";
            arrRet.Add(obj11);
            FS.HISFC.Models.Base.Item obj12 = new FS.HISFC.Models.Base.Item();
            obj12.ID = "510";
            obj12.Name = "生育保险";
            arrRet.Add(obj12);

            string strRet = (from FS.HISFC.Models.Base.Item obj in arrRet
                             where obj.ID.Equals(code)
                             select obj.Name).FirstOrDefault();
            return strRet;
        }

        private void DualBanlanceInfo(FS.HISFC.Models.RADT.PatientInfo p, ref FoShanYDSI.Objects.SIPersonInfo ps)
        {
            return;
        }

        /// <summary>
        /// 佛山异地医保结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            msg = "";
            if (this.p.PVisit.InState.ID.ToString() != "B" && this.p.PVisit.InState.ID.ToString() != "O")
            {
                MessageBox.Show("该患者未做出院登记，不允许操作!");
                return;
            }
            if (!string.IsNullOrEmpty(pactCode))// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            {
                if (this.p.Pact.ID != pactCode)
                {
                    MessageBox.Show("该患者合同身份非【省内】异地患者！请修改患者合同身份");
                    return;
                }
            }

            if (queryPersonFlag != 1)
            {
                MessageBox.Show("请先查询患者", "佛山异地医保");
                return;
            }

            string state = this.SIMgr.GetSiState(this.p.ID);

            if (state == "3")
            {
                MessageBox.Show("已经费用结算", "佛山异地医保");
                return;

            }
            if (state == "2")
            {
                MessageBox.Show("已出院登记，请先操作【取消出院登记】 成功后 【取消费用上传】", "佛山异地医保");
                return;
            }

            if (state == "1")
            {
                MessageBox.Show("已费用上传，请先操作【取消费用上传】", "佛山异地医保");
                return;
            }

            if (this.inPatientService.QueryfeeDetails(this.p, ref this.feeDetails, ref msg) < 1)
            {
                if (this.ps != null)
                {
                    //00:待审核;01:审核通过;02:审核不通过;03:已结算
                    if (this.ps.InState == "03")
                    {
                        this.msg += "该患者已完成结算!";
                    }
                }
                MessageBox.Show(this.msg, "佛山异地医保");
                return;
            }
            if (this.ps != null)
            {
                this.ps.DiagnName = this.cmbDiag1.Text;
                this.ps.Diagn1 = this.cmbDiag1.Tag.ToString();
                this.ps.Diagn2 = this.cmbDiag2.Tag.ToString();
                this.ps.Diagn3 = this.cmbDiag3.Tag.ToString();
            }

            if (string.IsNullOrEmpty(this.ps.Diagn1))
            {
                MessageBox.Show("请选择一个出院诊断！", "异地医保");
                return;

            }
            //获取费用医保对照
            if (this.CompareFeeDetails("14", ref this.feeDetails) < 1)
            {
                return;
            }
            msg = "";
            //费用上传
            if (this.inPatientService.YDGetInpatientFeeDetail(this.p, ref this.ps, ref this.feeDetails, ref msg) < 1)
            {
                MessageBox.Show(msg, "佛山异地医保");
                return;
            }
            if (this.SIMgr.UpdateSiState(this.p.ID, "1") < 0)
            {
                MessageBox.Show("费用上传成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【已费用上传状态】失败！" + this.SIMgr.Err, "佛山异地医保");
            }

            msg = "";
            //出院登记
            string status = "";
            if (this.inPatientService.YDInPatientBalanceReg(ref this.p, ref this.ps, ref status, ref msg) < 1)
            {
                MessageBox.Show(msg, "佛山异地医保");
                return;
            }
            if (this.SIMgr.UpdateSiState(this.p.ID, "1") < 0)
            {
                MessageBox.Show("出院登记成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【出院登记状态】失败！" + this.SIMgr.Err, "佛山异地医保");
            }
            msg = "";
            //出院结算
            if (this.inPatientService.YDInpatientFeeBalance(this.p, this.ps, ref msg) < 1)
            {
                MessageBox.Show(msg, "佛山异地医保");
                return;
            }
            if (this.SIMgr.UpdateSiState(this.p.ID, "1") < 0)
            {
                MessageBox.Show("出院结算成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【出院结算状态】失败！" + this.SIMgr.Err, "佛山异地医保");
            } 

            MessageBox.Show("医保结算成功！", "佛山异地医保");
        }

        /// <summary>
        /// 取消住院登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancelReg_Click(object sender, EventArgs e)
        {
            //if (this.p.PVisit.InState.ID.ToString() != "B")
            //{
            //    MessageBox.Show("该患者未做出院登记，不允许操作!");
            //    return;
            //}
            if (queryPersonFlag != 1)
            {
                MessageBox.Show("请先查询患者", "佛山异地医保");
                return;
            }

            string state = this.SIMgr.GetSiState(this.p.ID);

            if (state == "3")
            {
                MessageBox.Show("已经费用结算，请先操作【取消费用结算】成功后【取消出院登记】，最后【取消费用上传】", "佛山异地医保");
                return;

            }
            if (state == "2")
            {
                MessageBox.Show("已经出院登记，请先操作【取消出院登记】 成功后 【取消费用上传】", "佛山异地医保");
                return;
            }

            if (state == "1")
            {
                MessageBox.Show("已费用上传，请先操作【取消费用上传】", "佛山异地医保");
                return;
            }
            msg = this.inPatientService.YDInpatientRegCancel(this.p, ref this.ps,ref msg);
            MessageBox.Show(this.msg, "佛山异地医保");
        }

        /// <summary>
        /// 对照费用明细
        /// </summary>
        /// <param name="pactId"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CompareFeeDetails(string pactId, ref ArrayList feeDetailsTemp)
        {
            decimal noCompareTotCost = 0;
            ArrayList alFeeUpload = null;
            if (function.GetCompareItem(pactId, feeDetailsTemp, out noCompareTotCost, out alFeeUpload, ref this.msg) < 0)
            {
                MessageBox.Show(this.msg + "\n\n不允许结算！", "未对照项目");
                return -1;
            }
            return 1;
        }

       /// <summary>
       /// 结算单打印
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btBalancePrint_Click(object sender, EventArgs e)
        {
            if (this.p.PVisit.InState.ID.ToString() != "B" && this.p.PVisit.InState.ID.ToString() != "O")
            {
                MessageBox.Show("该患者未做出院登记，不允许操作!");
                return;
            }
            if (queryPersonFlag != 1)
            {
                MessageBox.Show("请先查询患者", "佛山异地医保");
                return;
            }
            frmYDInBalanceInfoPrint frm = new frmYDInBalanceInfoPrint();
            frm.SetValue(this.p, this.ps);
            frm.Show();
        }

        /// <summary>
        /// 出院结算取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancelInBalance_Click(object sender, EventArgs e)
        {
            if (this.p.PVisit.InState.ID.ToString() != "B" && this.p.PVisit.InState.ID.ToString() != "O")
            {
                MessageBox.Show("该患者未做出院登记，不允许操作!");
                return;
            }
            if (!string.IsNullOrEmpty(pactCode))// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            {
                if (this.p.Pact.ID != pactCode)
                {
                    MessageBox.Show("该患者合同身份非【省内】异地患者！请修改患者合同身份");
                    return;
                }
            }
            if (this.inPatientService.CancelYDBalanceInpatient(this.p, ref this.msg) < 1)
            {
                MessageBox.Show(msg, "佛山异地医保");
                return;
            }
            else
            {
                if (this.SIMgr.UpdateSiState(this.p.ID, "2") < 0)
                {
                    MessageBox.Show("住院出院登记取消成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【出院登记状态】失败！" + this.SIMgr.Err, "佛山异地医保");
                }
                else
                {
                    MessageBox.Show("出院结算取消成功", "佛山异地医保");
                }
            }
        }

        /// <summary>
        /// 取消费用上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancelFeeUpload_Click(object sender, EventArgs e)
        {
            msg = "";
            if (this.p.PVisit.InState.ID.ToString() != "B" && this.p.PVisit.InState.ID.ToString() != "O")
            {
                MessageBox.Show("该患者未做出院登记，不允许操作!");
                return;
            }
            if (!string.IsNullOrEmpty(pactCode))// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            {
                if (this.p.Pact.ID != pactCode)
                {
                    MessageBox.Show("该患者合同身份非【省内】异地患者！请修改患者合同身份");
                    return;
                }
            }


            string state = this.SIMgr.GetSiState(this.p.ID);
            if (state == "3")
            {
                MessageBox.Show("已经费用结算，请先【取消费用结算】成功后【取消出院登记】", "佛山异地医保");
                return;

            }
            if (state == "2")
            {
                MessageBox.Show("已经出院登记，请先【取消出院登记】", "佛山异地医保");
                return;

            }

            if(this.inPatientService.CancelUploadBalanceItem(this.p,ref this.msg)<0)
            {
                MessageBox.Show(msg, "佛山异地医保");
                return;
            }
            else
            {
                if (this.SIMgr.UpdateSiState(this.p.ID, "0") < 0)
                {
                    MessageBox.Show("住院出院登记取消成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【住院登记状态】失败！" + this.SIMgr.Err, "佛山异地医保");
                }
                else
                {
                    MessageBox.Show("住院结算明细取消成功", "佛山异地医保");
                }
            }
        }

        /// <summary>
        /// 取消出院登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancelOutReg_Click(object sender, EventArgs e)
        {
            msg = "";
            if (this.p.PVisit.InState.ID.ToString() != "B" && this.p.PVisit.InState.ID.ToString() != "O")
            {
                MessageBox.Show("该患者未做出院登记，不允许操作!");
                return;
            }
            if (!string.IsNullOrEmpty(pactCode))// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            {
                if (this.p.Pact.ID != pactCode)
                {
                    MessageBox.Show("该患者合同身份非【省内】异地患者！请修改患者合同身份");
                    return;
                }
            }

            string state = this.SIMgr.GetSiState(this.p.ID);

            if (state == "3")
            {
                MessageBox.Show("已经费用结算，请先【取消费用结算】", "佛山异地医保");
                return;

            }
            if (this.inPatientService.CancelOutReg(this.p, ref this.msg) < 1)
            {
                MessageBox.Show(msg, "佛山异地医保");
                return;
            }
            else
            {
                if (this.SIMgr.UpdateSiState(this.p.ID, "1") < 0)
                {
                    MessageBox.Show("住院出院登记取消成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【已费用上传状态】失败！" + this.SIMgr.Err, "佛山异地医保");
                }
                else
                {
                    MessageBox.Show("住院出院登记取消成功", "佛山异地医保");
                }
            }
        }

        /// <summary>
        /// 费用上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btUpLoadFeeDetail_Click(object sender, EventArgs e)
        {
          
            msg = "";
            if (this.p.PVisit.InState.ID.ToString() != "B" && this.p.PVisit.InState.ID.ToString() != "O")
            {
                MessageBox.Show("该患者未做出院登记，不允许操作!");
                return;
            }
            if (!string.IsNullOrEmpty(pactCode))// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            {
                if (this.p.Pact.ID != pactCode)
                {
                    MessageBox.Show("该患者合同身份非【省内】异地患者！请修改患者合同身份");
                    return;
                }
            }
            if (queryPersonFlag != 1)
            {
                MessageBox.Show("请先查询患者", "佛山异地医保");
                return;
            }

            MessageBox.Show("上传费用明细超时，是否重新发送当前数据", "佛山异地医保", MessageBoxButtons.YesNo);

            //获取费用医保对照
            if (this.CompareFeeDetails("14", ref this.feeDetails) < 1)
            {
                return;
            }
            //费用上传
            if (this.inPatientService.YDGetInpatientFeeDetail(this.p, ref this.ps, ref this.feeDetails, ref msg) < 0)
            {
                MessageBox.Show(msg, "佛山异地医保");
                return;
            }

            if (this.SIMgr.UpdateSiState(this.p.ID, "1") < 0)
            {
                MessageBox.Show("费用上传成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【已费用上传状态】失败！" + this.SIMgr.Err, "佛山异地医保");
            }
            MessageBox.Show(msg + "，请做下一步**出院登记**", "佛山异地医保");
            queryPersonFlag = 2;
        }

        /// <summary>
        /// 出院登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOutReg_Click(object sender, EventArgs e)
        {
            //if (queryFeeDetailFlag < 2)
            //{
            //    MessageBox.Show("请先进行出院登记", "佛山异地医保");
            //    return;
            //}
            msg = "";
            if (this.p.PVisit.InState.ID.ToString() != "B" && this.p.PVisit.InState.ID.ToString() != "O")
            {
                MessageBox.Show("该患者未做出院登记，不允许操作!");
                return;
            }
            if (!string.IsNullOrEmpty(pactCode))// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            {
                if (this.p.Pact.ID != pactCode)
                {
                    MessageBox.Show("该患者合同身份非【省内】异地患者！请修改患者合同身份");
                    return;
                }
            }
            this.ps.DiagnName = this.cmbDiag1.Text.ToString();
            this.ps.Diagn1 = this.cmbDiag1.Tag.ToString();
            if (this.inPatientService.YDInPatientBalanceReg(ref this.p, ref this.ps, ref status, ref msg) < 0)
            {
                MessageBox.Show(msg, "佛山异地医保");
                return;
            }

            if (this.SIMgr.UpdateSiState(this.p.ID, "2") < 0)
            {
                MessageBox.Show("出院登记成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【出院登记状态】失败！" + this.SIMgr.Err, "佛山异地医保");
            }

            MessageBox.Show(msg+"，请做下一步**出院结算**", "佛山异地医保");
            queryPersonFlag = 3;
        }

        /// <summary>
        /// 出院结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btBalanceInfo_Click(object sender, EventArgs e)
        {
            //if (queryFeeDetailFlag < 3)
            //{
            //    MessageBox.Show("请先进行出院登记", "佛山异地医保");
            //    return;
            //}
            msg = "";
            if (this.p.PVisit.InState.ID.ToString() != "B" && this.p.PVisit.InState.ID.ToString() != "O")
            {
                MessageBox.Show("该患者未做出院登记，不允许操作!");
                return;
            }
            if (!string.IsNullOrEmpty(pactCode))// {80867F3F-AE91-4525-82AF-0618AB01C92B}
            {
                if (this.p.Pact.ID != pactCode)
                {
                    MessageBox.Show("该患者合同身份非【省内】异地患者！请修改患者合同身份");
                    return;
                }
            }
            if(this.inPatientService.YDInpatientFeeBalance(this.p, this.ps, ref msg) < 0)
            {
                MessageBox.Show(msg, "佛山异地医保");
                return;
            }

            if (this.SIMgr.UpdateSiState(this.p.ID, "3") < 0)
            {
                MessageBox.Show("医保出院结算成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【出院结算状态】失败！" + this.SIMgr.Err, "佛山异地医保");
            }

            MessageBox.Show("医保出院结算成功！", "佛山异地医保");
        }

        private void saveDiagnose_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            if(!string.IsNullOrEmpty(cmbDiag1.Text))
            {
                this.ps.DiagnName = this.cmbDiag1.Text;
                this.ps.Diagn1 = this.cmbDiag1.Tag.ToString();
                this.ps.Diagn2 = this.cmbDiag2.Tag.ToString();
                this.ps.Diagn3 = this.cmbDiag3.Tag.ToString();
                if (SIMgr.SaveDiag(p.ID, cmbDiag1.Tag.ToString(), cmbDiag2.Tag.ToString(), cmbDiag3.Tag.ToString(), ref err) == -1)
                {
                    MessageBox.Show("保存诊断失败!" + err, "佛山异地医保");
                    return;
                }
                
            }
            else
            {
                MessageBox.Show("请填写出院诊断!", "佛山异地医保");
                return;
            }
            MessageBox.Show("保存成功!", "佛山异地医保");
            this.Clear();
        }

        private void btInReg_Click(object sender, EventArgs e)
        {
            if (this.p == null)
            {
                MessageBox.Show("请选择一位患者");
                return;
            }
            FoShanYDSIManager FoShanYDSIManager = new FoShanYDSIManager();
            if (FoShanYDSIManager.UploadRegInfoInpatient(this.p) < 0)
            {
                MessageBox.Show("住院登记失败！" + FoShanYDSIManager.ErrMsg);
                return;

            }


            if (this.SIMgr.UpdateSiState(this.p.ID, "0") < 0)
            {
                MessageBox.Show("医保出院结算成功，但更新表【fin_ipr_siinmaininfo_yd】状态为【住院登记状态】失败！" + this.SIMgr.Err, "佛山异地医保");
            }
        }

        private void btBingAn_Click(object sender, EventArgs e)
        {
            if (this.p == null)
            {
                MessageBox.Show("请选择一位患者");
                return;
            }
            bool isSucc = true;
            msg = "";
            string status = "";
            //住院病人信息（病案首页）录入（0801）
            DataTable dtCaseInfo = this.SIMgr.QueryInpatientCase(this.p.PID.PatientNO,this.p.InTimes.ToString());
            if (dtCaseInfo != null && dtCaseInfo.Rows.Count > 0)
            {
                if (this.inPatientService.YDInPatientUploadDRGS(dtCaseInfo, ref this.p, ref this.ps, ref status, ref msg) < 0)
                {
                    isSucc = false;
                    MessageBox.Show("上传病案信息失败！" + msg, "佛山异地医保");
                    return;
                }
            }
            else
            {
                MessageBox.Show("病案信息为空！" + msg, "佛山异地医保");
                return;
            }

            msg = "";
            status = "";
            //住院病人诊断信息（病案首页）录入（0802）
            DataTable dtCaseDiagnose = this.SIMgr.GetCaseDiagnoseByBASY(this.p.PID.PatientNO, this.p.InTimes.ToString());
            if (dtCaseDiagnose != null && dtCaseDiagnose.Rows.Count > 0)
            {
                if (this.inPatientService.YDInPatientUploadCaseDiagnose(dtCaseDiagnose, ref this.p, ref this.ps, ref status, ref msg) < 0)
                {
                    isSucc = false;
                    MessageBox.Show("上传病案诊断信息失败！" + msg, "佛山异地医保");
                    return;
                }
                else//上传诊断完成后才能上传以下信息
                {

                    msg = "";
                    status = "";
                    //住院病人手术信息（病案首页）录入（0803）
                    DataTable dtOperation = this.SIMgr.QueryOperationInfo(this.p.PID.PatientNO, this.p.InTimes.ToString());
                    if (dtOperation != null && dtOperation.Rows.Count > 0)
                    {

                        if (this.inPatientService.YDInPatientOperationDetail(dtOperation, ref this.p, ref this.ps, ref status, ref msg) < 0)
                        {
                            isSucc = false;
                            MessageBox.Show("上传病案人手术信息失败！" + msg, "佛山异地医保");
                            //return;
                        }
                    }


                    msg = "";
                    status = "";
                    //4.85住院病人产科分娩婴儿信息（病案首页）录入（0804）
                    DataTable dtCaseBaby = this.SIMgr.GetCaseBabyByBASY(this.p.PID.PatientNO, this.p.InTimes.ToString());
                    if (dtCaseBaby != null && dtCaseBaby.Rows.Count > 0)
                    {
                        if (this.inPatientService.YDInPatientBabyInfo(dtCaseBaby, ref this.p, ref this.ps, ref status, ref msg) < 0)
                        {
                            isSucc = false;
                            MessageBox.Show("上传产科分娩婴儿信息失败！" + msg, "佛山异地医保");
                            //return;
                        }
                    }


                    //4.86肿瘤专科病人治疗记录信息（病案首页）录入（0805）
                    //目前没有该信息

                    msg = "";
                    status = "";
                    //出院小结（出院记录）录入（0806）
                    FS.HISFC.Models.Base.Const obj = this.SIMgr.QueryPatientOutRecord(this.p.ID);
                    if (obj != null && !string.IsNullOrEmpty(obj.ID))
                    {
                        if (this.inPatientService.YDInPatientOutRecord(obj, ref this.p, ref this.ps, ref status, ref msg) < 0)
                        {
                            isSucc = false;
                            MessageBox.Show("上传产科分娩婴儿信息失败！" + msg, "佛山异地医保");
                            //return;
                        }
                    }
                }
            }

            if (isSucc)
            {
                MessageBox.Show("病案信息上传成功！");
            }
        }

    }
}
