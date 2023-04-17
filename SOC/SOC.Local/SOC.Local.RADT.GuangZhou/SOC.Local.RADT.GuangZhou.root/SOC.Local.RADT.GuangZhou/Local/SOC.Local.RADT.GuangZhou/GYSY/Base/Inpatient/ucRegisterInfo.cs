using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Base;
using System.Xml;
using Neusoft.FrameWork.Function;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;
using Neusoft.HISFC.Models.RADT;
using Neusoft.SOC.HISFC.RADT.Interface;
using System.IO;

namespace Neusoft.SOC.Local.RADT.GuangZhou.GYSY.Base.Inpatient
{
    /// <summary>
    /// 入院登记界面
    /// </summary>
    public partial class ucRegisterInfo : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.SOC.HISFC.RADT.Interface.Register.IInpatient
    {
        public ucRegisterInfo()
        {
            InitializeComponent();
            this.cmbDept.TextChanged += new EventHandler(cmbDept_TextChanged);
            this.cmbDoctor.TextChanged += new EventHandler(cmbDoctor_TextChanged);
            this.cmbInSource.TextChanged += new EventHandler(cmbInSource_TextChanged);
            this.dtpBirthDay.TextChanged += new EventHandler(dtpBirthDay_TextChanged);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            this.Click += new EventHandler(c_Click);
            this.plInfomation.Click += new EventHandler(c_Click);
            this.dtpBirthDay.KeyDown += new KeyEventHandler(dtpBirthDay_KeyDown);
            this.rbtnTempPatientNO.CheckedChanged += new EventHandler(rbtnTempPatientNO_CheckedChanged);
            this.txtIDNO.KeyDown += new KeyEventHandler(txtIDNO_KeyDown);
            this.cmbNurseCell.TextChanged += new EventHandler(cmbNurseCell_TextChanged);
        }

        #region 成员变量

        /// <summary>
        /// 管理类
        /// </summary>
        private CommonController commonController = CommonController.CreateInstance();
        Neusoft.HISFC.BizLogic.Fee.PactUnitInfo PactUnit = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
        Neusoft.HISFC.BizLogic.Manager.Constant Constant = new Neusoft.HISFC.BizLogic.Manager.Constant();
        Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctlParamManage = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
        Neusoft.HISFC.BizLogic.Fee.InPatient feeInpatient = new Neusoft.HISFC.BizLogic.Fee.InPatient();
        protected Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        private bool isArriveProcess = false;

        private bool isCanModifyInTime = false;

        private string fileName = "";

        private string deptCode = "";
        private string nurseCode = "";

        private bool isAutoPatientNO = false;

        private string autoPatientNO = "";    //自动生成的住院号
        private string auoPatientNOTemp = ""; //自动生成的临时号

        //---add--
        decimal DayLimit = 0;
        private bool MedTag = false;
        string strPayKindID = "";
        string strPayKindName = "";
        //---add--
        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void initCombo()
        {
            //性别列表
            ArrayList alSex = Neusoft.HISFC.Models.Base.SexEnumService.List();
            //去掉全部和其他选项
            alSex.RemoveAt(3);
            alSex.RemoveAt(2);
            this.cmbSex.AddItems(alSex);
            this.cmbSex.Text = "";
            this.cmbSex.Tag = "";

            //国籍
            this.cmbCountry.AddItems(commonController.QueryConstant(EnumConstant.COUNTRY));
            //婚姻状态          
            this.cmbMarry.AddItems(Neusoft.HISFC.Models.RADT.MaritalStatusEnumService.List());
            //职业
            ArrayList profList = commonController.QueryConstant(EnumConstant.PROFESSION);
            this.cmbProfession.AddItems(profList);
            //出生地信息
            this.cmbBirthArea.AddItems(commonController.QueryConstant(EnumConstant.DIST));
            //民族
            this.cmbNation.AddItems(commonController.QueryConstant(EnumConstant.NATION));
            //联系人关系
            this.cmbRelation.AddItems(commonController.QueryConstant(EnumConstant.RELATIVE));
            //入院科室
            this.cmbDept.AddItems(commonController.QueryDepartment(true));
            //病区
            this.cmbNurseCell.AddItems(commonController.QueryDepartment(EnumDepartmentType.N));
            //病人来源信息
            this.cmbInSource.AddItems(commonController.QueryConstant(EnumConstant.INSOURCE));
            //入院情况信息
            this.cmbCircs.AddItems(commonController.QueryConstant(EnumConstant.INCIRCS));
            //入院途径信息
            this.cmbApproach.AddItems(commonController.QueryConstant(EnumConstant.INAVENUE));
            //转科的原科室
            this.cmbOldDept.AddItems(commonController.QueryDepartment(EnumDepartmentType.I));
            //医生信息
            this.cmbDoctor.AddItems(commonController.QueryEmployee(EnumEmployeeType.D));
            //结算方式
            this.cmbPact.AddItems(commonController.QueryInPatientPactInfo());
            //支付方式
            //this.cmbPayMode.AddItems(commonController.QueryConstant(Neusoft.HISFC.Models.Base.EnumConstant.PAYMODES));

            //住院次数
            this.mTxtIntimes.Text = "1";
            //支付方式
            this.cmbPayType.Tag = "CA";
            this.cmbPayType.Text = "现金";
            //确定选择方式
            this.cmbPayType.IsListOnly = true;
            //入院日期
            this.dtpInTime.Value = commonController.GetSystemTime(); //入院日期
            //生日
            this.dtpBirthDay.Text = string.Empty;

            //是否有病历
            ArrayList al = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject Obj = new Neusoft.FrameWork.Models.NeuObject();
            Obj.ID = "1";
            Obj.Name = "有";
            al.Add(Obj);
            Neusoft.FrameWork.Models.NeuObject o = new Neusoft.FrameWork.Models.NeuObject();
            o.ID = "0";
            o.Name = "无";
            al.Add(o);

            //this.cmbCase.AddItems(al);
            //this.cmbCase.Tag = "1";
            //this.cmbCase.Text = "有";

            //超标处理
            ArrayList alLimit = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
            Neusoft.FrameWork.Models.NeuObject O = new Neusoft.FrameWork.Models.NeuObject();
            O.ID = "0";
            O.Name = "超标不限";
            alLimit.Add(O);

            obj.ID = "1";
            obj.Name = "超标自理";
            alLimit.Add(obj);

            Neusoft.FrameWork.Models.NeuObject obj1 = new Neusoft.FrameWork.Models.NeuObject();
            obj1.ID = "2";
            obj1.Name = "超标不计";
            alLimit.Add(obj1);
            this.cmbBedOverDeal.AddItems(alLimit);
            this.cmbBedOverDeal.IsListOnly = true;
            this.cmbBedOverDeal.SelectedIndex = ctlParamManage.GetControlParam<int>("Fee001");
            InputLanguageCollection ilc = InputLanguage.InstalledInputLanguages;
            //ArrayList alillist = new ArrayList();
            //foreach (InputLanguage alil in ilc)
            //{
            //    Neusoft.FrameWork.Models.NeuObject objil = new Neusoft.FrameWork.Models.NeuObject();
            //    objil.ID = alil.LayoutName;
            //    objil.Name = alil.LayoutName;

            //    alillist.Add(objil);
            //}
            //this.comboBox1.AddItems(alillist);      //登记类型
            //comboBox1.SelectedIndex = InputLanguage.InstalledInputLanguages.IndexOf(InputLanguage.CurrentInputLanguage);

            ArrayList alOverLop = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject objOverLop1 = new Neusoft.FrameWork.Models.NeuObject();
            objOverLop1.ID = "0";
            objOverLop1.Name = "不同意超标";
            alOverLop.Add(objOverLop1);
            Neusoft.FrameWork.Models.NeuObject objOverLop2 = new Neusoft.FrameWork.Models.NeuObject();
            objOverLop2.ID = "1";
            objOverLop2.Name = "同意超标";
            alOverLop.Add(objOverLop2);
            this.cmbOverLop.AddItems(alOverLop);
            this.cmbOverLop.IsListOnly = true;
            this.cmbOverLop.SelectedIndex = ctlParamManage.GetControlParam<int>("Fee002");
            //cmbOverLop.Text = "不同意超标";
            //广医默认为同意超标
            //cmbOverLop.Tag = "0";
        }
        #region 属性
        bool isSpReg = false;//是否公费特殊登记患者
        /// <summary>
        /// 是否公费特殊登记患者
        /// </summary>
        public bool IsSpReg
        {
            set
            {
                isSpReg = value;
                #region 暂时屏蔽
                /*
                if (isSpReg)
                {
                    //this.ucQueryInpatientNo1.Visible = true;
                    this.vtbRate.Enabled = true;
                    //this.cmbMedicalType.AddItems(new ArrayList());

                    //初始化操作员
                    Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
                    ArrayList alPact = pactManager.GetPactUnitInfo();
                    ArrayList alPactForInpatient = Constant.GetPactunitForInpatient(); //住院可以使用的通用合同单位
                    ArrayList alTemp = new ArrayList();
                    //取受控制的合同单位,只有特殊入院才能选择这些合同单位
                    foreach (neusoft.HISFC.Object.Fee.PactUnitInfo info in alPact)
                    {
                        if (info.IsInControl == "1")
                        {
                            alTemp.Add(info);
                        }
                    }
                    alPact = alTemp;

                    if (alPact == null)
                    {
                        MessageBox.Show("由合同单位表加载合同单位信息失败!" + pactManager.Err);
                        this.cmbMedicalType.AddItems(alPactForInpatient);
                    }
                    else
                    {
                        if (this.isSpReg)			//特殊入院患者
                        {
                            //this.cmbMedicalType.AddItems(alPact);
                            //住院使用特有的合同单位
                            this.cmbMedicalType.AddItems(alPactForInpatient);
                        }
                        //下面的代码没有用到,可以屏蔽edited by cuipeng 2006-06-27
                        //						else
                        //						{
                        //							alTemp = new ArrayList();
                        //							bool parm = false;
                        //							//在通用的合同单位中去除受限制的合同单位
                        //							foreach(neusoft.neuFC.Object.neuObject neuobj in alPactForInpatient) 
                        //							{
                        //								foreach(neusoft.HISFC.Object.Fee.PactUnitInfo info in alPact)
                        //								{
                        //									if (info.ID == neuobj.ID) 
                        //									{
                        //										parm = true;
                        //										break;
                        //									}
                        //								}
                        //								//去除特殊控制的合同单位
                        //								if(parm)
                        //									continue;
                        //								
                        //								alTemp.Add(neuobj);
                        //
                        //							}
                        //							this.cmbMedicalType.AddItems(alTemp);
                        //						}
                    }

                }
                else
                {
                    this.ucQueryInpatientNo1.Visible = false;
                    this.vtbRate.Enabled = false;
                }
                */
                #endregion
            }
        }
        #endregion

        /// <summary>
        /// 初始化文件
        /// </summary>
        private void initFile()
        {
            Function.ReadConfig(this.plInfomation, this.fileName);
            this.setInputMenu();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void save()
        {
            //保存前验证
            if (this.isInputValid())
            {
                if (this.OnSavePatientInfo != null)
                {
                    if (MessageBox.Show(this, "确定是否保存？", "提示>>", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.OnSavePatientInfo(null, null);
                    }
                    else
                    {
                        this.txtInpatientNO.Select();
                    }
                }
            }
        }

        //判断输入的是否为"-"，如果输入的是"-"符号则代表为空
        private string SpecialFilte(string controlValue)
        {
            if (controlValue == "-")
            {
                return "";
            }
            else
            {
                return controlValue;
            }
        }

        /// <summary>
        /// 设置患者信息到控件
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="isAll">是否显示全部信息，Add时只显示部分信息</param>
        private void setPatientInfo(PatientInfo patient)
        {
            this.setPatient(patient);

            if (string.IsNullOrEmpty(patient.PID.PatientNO) == false)
            {
                this.txtInpatientNO.Text = patient.PID.PatientNO.PadLeft(10, '0');//住院号
                //添加临时标志，以防保存时验证无法通过
                this.txtInpatientNO.Tag = "T001";
                this.autoPatientNO = this.txtInpatientNO.Text;
                //
            }

            this.cmbCircs.Text = patient.PVisit.Circs.Name;//入院情况
            this.cmbCircs.Tag = patient.PVisit.Circs.ID;//入院情况
            if (patient.InTimes == 0)
            {
                patient.InTimes = 1;
            }
            else if (patient.InTimes > 1)
            {
                this.txtName.Enabled = false;
                this.txtClinicNO.Enabled = false;
            }


            this.cmbDept.Tag = patient.PVisit.PatientLocation.Dept.ID;//科室
            this.cmbNurseCell.Tag = patient.PVisit.PatientLocation.NurseCell.ID;//病区
            //收住医师
            if (patient.DoctorReceiver != null && !string.IsNullOrEmpty(patient.DoctorReceiver.ID.ToString()))
            {
                this.cmbDoctor.Tag = patient.DoctorReceiver.ID;
                this.cmbDoctor.Text = this.commonController.GetEmployeeName(patient.DoctorReceiver.ID);
            }
            this.txtDiagnose.Text = patient.ClinicDiagnose; //门诊诊断
            this.txtComputerNO.Text = patient.ProCreateNO;//生育保险电脑号

            //病床
            this.cmbBedNO.Text = patient.PVisit.PatientLocation.Bed.ID;
            this.mTxtPrepay.Text = patient.FT.PrepayCost.ToString();//预交金
            //住院日期
            if (patient.PVisit.InTime == DateTime.MinValue)
            {
                this.dtpInTime.Value = commonController.GetSystemTime().Date;
            }
            else
            {
                if (patient.PVisit.InTime > new DateTime(1975, 1, 1))
                {
                    this.dtpInTime.Value = patient.PVisit.InTime;
                }
            }

            this.cmbApproach.Tag = patient.PVisit.AdmitSource.ID;

            //入院来源
            if (patient.PVisit.InSource.ID == string.Empty)
            {
                this.cmbInSource.Tag = "1";
            }
            else
            {
                this.cmbInSource.Tag = patient.PVisit.InSource.ID;

                if (this.cmbInSource.Text.Trim() == "转科")// || this.cmbInSource.Tag.ToString() == "3")
                {
                    this.cmbOldDept.Visible = true;
                    this.lblOldDept.Visible = true;
                }
                else
                {
                    this.cmbOldDept.Visible = false;
                    this.lblOldDept.Visible = false;
                    this.cmbOldDept.Text = "";
                }
            }
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="patient"></param>
        private void setPatient(Neusoft.HISFC.Models.RADT.Patient patient)
        {
            if (string.IsNullOrEmpty(patient.PID.CardNO))
            {
                this.txtClinicNO.Text = Function.GetCardNOByPatientNO(this.txtClinicNO.Text, this.txtInpatientNO.Text);
            }
            else
            {
                this.txtClinicNO.Text = patient.PID.CardNO.PadLeft(10, '0');//门诊卡号
            }

            if (!string.IsNullOrEmpty(patient.ID))
            {
                this.txtInpatientNO.Tag = patient.ID;//住院流水号
            }

            //是否加密
            this.chbencrypt.Checked = patient.IsEncrypt;
            if (patient.IsEncrypt)
            {
                patient.Name = Neusoft.FrameWork.WinForms.Classes.Function.Decrypt3DES(patient.NormalName);
            }
            this.txtName.Text = patient.Name;//患者姓名
            this.dtpBirthDay.Text = patient.Birthday.ToString();//出生日期
            this.txtWorkAddress.Text = patient.CompanyName;//公司名称
            this.txtBirthArea.Text = patient.AreaCode; //出生地
            this.txtHomePhone.Text = patient.PhoneHome;//患者电话
            this.txtWorkPhone.Text = patient.PhoneBusiness;//单位电话  
            this.txtHomeZip.Text = patient.HomeZip;//家庭地址邮编

            this.cmbCountry.Tag = patient.Country.ID;//国籍
            if (!string.IsNullOrEmpty(patient.Country.Name))
            {
                this.cmbCountry.Text = patient.Country.Name;
            }
            this.txtHomeAddress.Text = patient.AddressHome;//家庭地址

            if (!string.IsNullOrEmpty(patient.DIST))
            {
                this.cmbBirthArea.Tag = "";
                this.cmbBirthArea.Text = patient.DIST;//出生地／籍贯
            }
            //婚姻状况
            this.cmbMarry.Tag = patient.MaritalStatus.ID;
            if (!string.IsNullOrEmpty(patient.MaritalStatus.Name))
            {
                this.cmbMarry.Text = patient.MaritalStatus.Name.ToString();
            }
            this.cmbSex.Tag = patient.Sex.ID;//性别
            //民族
            if (patient.Nationality.ID != string.Empty && patient.Nationality.ID != null)
            {
                this.cmbNation.Tag = patient.Nationality.ID;
            }
            this.cmbProfession.Tag = patient.Profession.ID;//职业ID
            if (!string.IsNullOrEmpty(patient.Profession.Name))
            {
                this.cmbProfession.Text = patient.Profession.Name;//职业名称
            }


            this.txtLinkMan.Text = patient.Kin.Name;//联系人姓名
            this.txtLinkPhone.Text = patient.Kin.RelationPhone;//联系人备注-电话／地址

            //与患者关系
            if (!string.IsNullOrEmpty(patient.Kin.Relation.ID))
            {
                this.cmbRelation.Tag = patient.Kin.Relation.ID;
            }
            else
            {
                this.cmbRelation.Text = patient.Kin.Relation.Name;
            }

            this.txtIDNO.Text = patient.IDCard;//身份证号码
            this.mTxtIntimes.Text = patient.InTimes.ToString();// 住院次数
            this.txtLinkAddr.Text = patient.Kin.RelationAddress; //联系人地址
            this.txtAddressNow.Text = patient.AddressBusiness;
            if (string.IsNullOrEmpty(this.txtAddressNow.Text))
            {
                this.txtAddressNow.Text = this.txtHomeAddress.Text;
            }
            this.txtMCardNO.Text = patient.SSN;//医保号
            this.cmbPact.Text = patient.Pact.Name;
            this.cmbPact.Tag = patient.Pact.ID;//合同单位
            Neusoft.HISFC.Models.Base.PactInfo pact = new Neusoft.HISFC.Models.Base.PactInfo();
            pact = this.PactUnit.GetPactUnitInfoByPactCode(patient.Pact.ID);
            try
            {
                this.txtAirLimit.Text = pact.AirConditionQuota.ToString();//监护床
            }
            catch
            {
                this.txtAirLimit.Text = "0";
            }
            try
            {
                this.txtBedLimit.Text = pact.BedQuota.ToString();//普通标准
            }
            catch
            {
                this.txtBedLimit.Text = "0";
            }
            try
            {
                this.txtDayLimit.Text = pact.DayQuota.ToString();//日限额
            }
            catch
            {
                this.txtDayLimit.Text = "0";
            }
            try
            {
                this.vtbRate.Text = pact.Rate.PayRate.ToString();//自付比例     
            }
            catch
            {
                this.vtbRate.Text = "0";
            }
        }

        /// <summary>
        /// 获得控件输入的信息,合成患者基本信息实体
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns> 成功: 1 失败 : -1</returns>
        private PatientInfo getPatientInfomation(PatientInfo patient)
        {
            if (patient == null)
            {
                patient = new PatientInfo();
            }
            patient.PID.PatientNO = this.txtInpatientNO.Text; //住院号
            patient.PID.CardNO = this.txtClinicNO.Text;//门诊卡号
            patient.ID = this.txtInpatientNO.Tag.ToString();//住院流水号
            patient.SSN = SpecialFilte(this.txtMCardNO.Text);//医保号
            patient.ProCreateNO = this.txtComputerNO.Text;//生育保险电脑号
            if (this.isCanModifyInTime)
            {
                patient.PVisit.InTime = this.dtpInTime.Value;//入院日期
            }
            else
            {
                patient.PVisit.InTime = commonController.GetSystemTime(); //入院日期
            }
            patient.Pact.ID = this.cmbPact.Tag.ToString();//合同单位编码
            patient.Pact.Name = this.cmbPact.Text;//合同单位名称
            patient.Pact.PayKind = this.commonController.GetPayKind(patient.Pact.ID);//结算类别
            if (patient.Pact.PayKind == null)
            {
                this.myMessageBox("获取结算类别错误！", MessageBoxIcon.Warning);
                return null;
            }
            //暂时屏蔽掉 接诊时候给床位
            //接诊
            if (this.isArriveProcess)
            {
                Neusoft.HISFC.Models.Base.Bed bedObj = this.cmbBedNO.SelectedItem as Neusoft.HISFC.Models.Base.Bed;
                patient.PVisit.PatientLocation.NurseCell = bedObj.NurseStation;
                patient.PVisit.PatientLocation.Bed = bedObj;
                patient.PVisit.InState.ID = Neusoft.HISFC.Models.Base.EnumInState.I;
            }
            else
            {
                patient.PVisit.InState.ID = Neusoft.HISFC.Models.Base.EnumInState.R;
            }

            patient.Name = this.txtName.Text;//名字
            patient.Sex.ID = this.cmbSex.Tag.ToString();//性别
            if (this.cmbNation.SelectedItem != null)
            {
                patient.Nationality = this.cmbNation.SelectedItem;//民族
            }
            patient.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtpBirthDay.Text);//生日
            patient.PVisit.PatientLocation.Dept.ID = this.cmbDept.Tag.ToString();//科室编码
            patient.PVisit.PatientLocation.Dept.Name = this.commonController.GetDepartmentName(patient.PVisit.PatientLocation.Dept.ID);//科室名称

            patient.CompanyName = this.txtWorkAddress.Text;//工作单位
            patient.MaritalStatus.ID = SpecialFilte(this.cmbMarry.Tag.ToString());//婚姻状况
            patient.DIST = this.cmbBirthArea.Text;//籍贯
            patient.Country.ID = this.cmbCountry.Tag.ToString();//国籍ID
            patient.Country.Name = this.cmbCountry.Text;//国籍
            patient.Profession.ID = this.cmbProfession.Tag.ToString();//职位ID
            patient.Profession.Name = this.cmbProfession.Text;//职位名称
            patient.Kin.Name = SpecialFilte(this.txtLinkMan.Text);//联系人姓名
            patient.Kin.RelationPhone = this.txtLinkPhone.Text;//联系人备注-电话
            patient.Kin.Relation.ID = this.cmbRelation.Tag.ToString();//与患者关系编码
            patient.Kin.Relation.Name = this.cmbRelation.Text;//与患者关系
            patient.Kin.RelationAddress = this.txtLinkAddr.Text;//联系人地址
            patient.AddressHome = this.txtHomeAddress.Text;//家庭地址
            patient.HomeZip = this.txtHomeZip.Text;//家庭地址邮编
            patient.AddressBusiness = this.txtAddressNow.Text;//单位地址
            patient.PhoneHome = this.txtHomePhone.Text;//患者电话
            patient.PhoneBusiness = this.txtWorkPhone.Text;//单位电话 
            patient.IDCard = SpecialFilte(this.txtIDNO.Text);//身份证
            patient.PVisit.AdmitSource.ID = this.cmbApproach.Tag.ToString();//入院途径
            patient.PVisit.AdmitSource.Name = this.cmbApproach.Text;//入院途径
            patient.PVisit.InSource.ID = this.cmbInSource.Tag.ToString();//入院来源
            patient.PVisit.InSource.Name = this.cmbInSource.Text;//入院来源
            patient.PVisit.Circs.ID = this.cmbCircs.Tag.ToString();//入院情况
            patient.PVisit.Circs.Name = this.cmbCircs.Text;//入院情况
            patient.DoctorReceiver.ID = this.cmbDoctor.Tag.ToString();//收住医师

            //patient.PVisit.AdmittingDoctor.ID = this.cmbDoctor.Tag.ToString();
            //patient.PVisit.AdmittingDoctor.Name = this.cmbDoctor.Text;
            //patient.PVisit.AttendingDoctor.ID = this.cmbDoctor.Tag.ToString();
            //patient.PVisit.AttendingDoctor.Name = this.cmbDoctor.Text;
            patient.DoctorReceiver.ID = this.cmbDoctor.Tag.ToString();//收住医师

            patient.ClinicDiagnose = this.txtDiagnose.Text;//门诊诊断

            patient.FT.BloodLateFeeCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.mtxtBloodFee.Text);//血滞纳金
            //路志鹏 修改住院次数 目的：本次住院登记的住院次数应该是上一次住院次数加1
            patient.InTimes = NConvert.ToInt32(this.mTxtIntimes.Text);//住院次数？初次就增加啦
            patient.FT.LeftCost = NConvert.ToDecimal(this.mTxtPrepay.Text);//预交金
            patient.FT.PrepayCost = NConvert.ToDecimal(this.mTxtPrepay.Text);//预交金
            patient.FT.FixFeeInterval = 1;//默认为1

            patient.FT.AirLimitCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(txtAirLimit.Text);  //监护床(空调上限)
            patient.FT.DayLimitCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(txtDayLimit.Text);  //公费日限
            patient.FT.BedLimitCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(txtBedLimit.Text);  //普通标准(床位上限)
            patient.FT.FTRate.PayRate = Neusoft.FrameWork.Function.NConvert.ToDecimal(vtbRate.Text);  //自付比例
            patient.FT.BedOverDeal = this.cmbBedOverDeal.SelectedIndex.ToString();        //超标处理 
            patient.FT.OvertopCost = -patient.FT.DayLimitCost;//超标金额
            patient.ExtendFlag = this.cmbOverLop.SelectedIndex.ToString();   //日限处理
            patient.FT.DayLimitTotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.txtDayLimit.Text);//日限额累计     

            //加密
            patient.IsEncrypt = this.chbencrypt.Checked;

            if (patient.IsEncrypt)
            {

                patient.NormalName = Neusoft.FrameWork.WinForms.Classes.Function.Encrypt3DES(patient.Name);
                patient.Name = "******";
            }

            patient.PVisit.PatientLocation.NurseCell.ID = this.cmbNurseCell.Tag.ToString();//病区编码
            patient.PVisit.PatientLocation.NurseCell.Name = this.commonController.GetDepartmentName(patient.PVisit.PatientLocation.NurseCell.ID);//病区名称
            return patient;
        }

        /// <summary>
        /// 验证输入的信息是否合法
        /// </summary>
        /// <returns>成功: true 失败: null</returns>
        private bool isInputValid()
        {
            if (this.isAutoPatientNO && string.IsNullOrEmpty(this.txtInpatientNO.Text))
            {
                this.getAutoPatientNO();
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtInpatientNO.Text) == false)
                {
                    string patientNO = this.txtInpatientNO.Text.Trim().PadLeft(10, '0');
                    this.txtInpatientNO.Text = patientNO;
                    this.txtInpatientNO.Tag = "T001";
                    //如果住院次数大于1
                    if (Neusoft.FrameWork.Function.NConvert.ToInt32(this.mTxtIntimes.Text) <= 1)
                    {
                        this.mTxtIntimes.Text = "1";
                    }
                }
                else
                {
                    //住院号为空
                    this.myMessageBox("住院号为空，请输入住院号", MessageBoxIcon.Information);
                    return false;
                }
            }

            Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();
            if (Function.GetInputPatientNO(this.txtInpatientNO.Text, ref patientInfo) < 1)
            {
                this.myMessageBox(Function.Err, MessageBoxIcon.Information);
                return false;
            }
            //如果住院号相同，则不获取患者信息           
            if (string.IsNullOrEmpty(patientInfo.ID) == false && patientInfo.PatientNOType == EnumPatientNOType.First)
            {
                //临时号和一般住院号分开来判断
                if (rdoInpatientNO.Checked)
                {
                    if (this.isAutoPatientNO && this.autoPatientNO != this.txtInpatientNO.Text)
                    {
                        this.myMessageBox("自动生成的住院号不允许修改", MessageBoxIcon.Warning);
                        this.txtInpatientNO.Text = this.autoPatientNO;
                        this.txtInpatientNO.Select();
                        return false;
                    }
                }
                else if (rbtnTempPatientNO.Checked)
                {
                    if (this.isAutoPatientNO && this.auoPatientNOTemp != this.txtInpatientNO.Text)
                    {
                        this.myMessageBox("自动生成的临时号不允许修改", MessageBoxIcon.Warning);
                        this.txtInpatientNO.Text = this.autoPatientNO;
                        this.txtInpatientNO.Select();
                        return false;
                    }
                }
            }
            else if (string.IsNullOrEmpty(patientInfo.ID) == false && patientInfo.PatientNOType == EnumPatientNOType.Second && patientInfo.PID.PatientNO.Equals(this.autoPatientNO) == false)
            {
                this.SetPatientInfo(patientInfo, true);
                return false;
            }


            this.GetCardNO(this.txtInpatientNO.Text);

            try
            {
                if (this.rdoInpatientNO.Checked)
                {
                    Int64 inpatientNO = Convert.ToInt64(this.txtInpatientNO.Text);

                    if (inpatientNO > 9000000000)
                    {
                        this.myMessageBox("您输入的住院号过大", MessageBoxIcon.Warning);
                        this.txtInpatientNO.Select();

                        return false;
                    }

                    if (inpatientNO <= 0)
                    {
                        this.myMessageBox("您输入的住院号不能为负数", MessageBoxIcon.Warning);
                        this.txtInpatientNO.Select();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                this.myMessageBox("您输入的住院号中含有非数字字符，请更改" + e.Message, MessageBoxIcon.Warning);
                this.txtInpatientNO.Select();
                return false;
            }

            //判断必须输入的控件是否都已经输入信息
            foreach (Control c in this.plInfomation.Controls)
            {
                if (c is Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)
                {
                    if (!((Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)c).IsValidInput())
                    {
                        this.myMessageBox(((Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)c).InputMsg, MessageBoxIcon.Warning);
                        c.Select();
                        return false;
                    }
                }
            }

            if (string.IsNullOrEmpty(this.txtClinicNO.Text))
            {
                this.myMessageBox("门诊号为空，请重新输入！", MessageBoxIcon.Warning);
                this.txtClinicNO.Select();
                return false;
            }

            //入院来源
            if (this.cmbInSource.Tag.ToString() == string.Empty)
            {
                this.myMessageBox("病人来源不能为空，请重新输入", MessageBoxIcon.Warning);
                this.cmbInSource.Select();
                return false;
            }

            //入院来源
            if (this.cmbCircs.Tag.ToString() == string.Empty)
            {
                this.myMessageBox("入院情况不能为空，请重新输入", MessageBoxIcon.Warning);
                this.cmbCircs.Select();
                return false;
            }

            //入院来源
            if (this.cmbApproach.Tag.ToString() == string.Empty)
            {
                this.myMessageBox("入院途径不能为空，请重新输入", MessageBoxIcon.Warning);
                this.cmbApproach.Select();
                return false;
            }

            //结算方式
            if (this.cmbPact.Tag.ToString() == null || this.cmbPact.Tag.ToString() == string.Empty)
            {
                this.myMessageBox("结算方式不能为空，请重新输入", MessageBoxIcon.Warning);
                this.cmbPact.Select();
                return false;
            }

            if (this.dtpInTime.Value > commonController.GetSystemTime())
            {
                this.myMessageBox("入院日期大于当前日期,请重新输入!", MessageBoxIcon.Warning);
                this.dtpInTime.Select();
                return false;
            }
            DateTime birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtpBirthDay.Text);
            if (birthday > commonController.GetSystemTime())
            {
                this.myMessageBox("出生日期大于当前日期,请重新输入!", MessageBoxIcon.Warning);
                this.dtpBirthDay.Select();
                return false;
            }

            if (birthday <= DateTime.MinValue)
            {
                this.myMessageBox("请填写出生日期!", MessageBoxIcon.Warning);
                this.dtpBirthDay.Select();

                return false;
            }

            //判断字符超长姓名
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text, 20))
            {
                this.myMessageBox("姓名录入超长！", MessageBoxIcon.Warning);
                this.txtName.Select();
                return false;
            }

            //判断字符超长籍贯
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.cmbBirthArea.Text, 50))
            {
                this.myMessageBox("籍贯录入超长！", MessageBoxIcon.Warning);
                this.cmbBirthArea.Select();
                return false;
            }

            //判断字符超长联系人
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtLinkMan.Text, 12))
            {
                this.myMessageBox("联系人录入超长！", MessageBoxIcon.Warning);
                this.txtLinkMan.Select();
                return false;
            }

            //判断字符超长工作单位
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtWorkAddress.Text, 50))
            {
                this.myMessageBox("工作单位录入超长！", MessageBoxIcon.Warning);
                this.txtWorkAddress.Select();
                return false;
            }

            //判断字符超长联系人电话
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtLinkPhone.Text, 30))
            {
                this.myMessageBox("联系人电话录入超长！", MessageBoxIcon.Warning);
                this.txtLinkPhone.Select();

                return false;
            }
            //家庭电话
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtHomePhone.Text, 30))
            {
                this.myMessageBox("家庭电话录入超长！", MessageBoxIcon.Warning);
                this.txtHomePhone.Select();

                return false;
            }
            //单位电话
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtWorkPhone.Text, 30))
            {
                this.myMessageBox("单位电话录入超长！", MessageBoxIcon.Warning);
                this.txtWorkPhone.Select();

                return false;
            }

            //诊断
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtDiagnose.Text, 50))
            {
                this.myMessageBox("门诊诊断录入超长！", MessageBoxIcon.Warning);
                this.txtDiagnose.Select();

                return false;
            }

            if (this.cmbSex.Text.Trim() == string.Empty)
            {
                this.myMessageBox("请输入患者性别!", MessageBoxIcon.Warning);
                this.cmbSex.Select();

                return false;
            }

            int returnValue = this.processIDENNO(this.txtIDNO.Text.Trim(), EnumCheckIDNOType.Saveing);
            if (returnValue < 0)
            {
                return false;
            }


            //门诊号长度
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtClinicNO.Text, 10))
            {
                this.myMessageBox("门诊号过长,请重新输入!", MessageBoxIcon.Warning);
                this.txtClinicNO.Select();
                return false;
            }
            //电脑号
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtComputerNO.Text, 20))
            {
                this.myMessageBox("电脑号过长,请重新输入!", MessageBoxIcon.Warning);
                this.txtComputerNO.Select();

                return false;
            }

            //医疗证号长度
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtMCardNO.Text, 20))
            {
                this.myMessageBox("医疗证号过长,请重新输入!", MessageBoxIcon.Warning);
                this.txtMCardNO.Select();
                return false;
            }


            //判断支付方式
            if (this.cmbPayType.Tag == null || this.cmbPayType.Tag.ToString() == string.Empty)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("请选择支付方式！", 111);
                this.cmbPayType.Focus();
                return false;
            }


            //科室
            if (this.cmbDept.Tag == null || this.cmbDept.Text.Trim() == string.Empty)
            {
                this.myMessageBox("科室不能为空，请重新输入！", MessageBoxIcon.Warning);
                this.cmbDept.Select();
                return false;
            }

            //科室长度
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.cmbDept.Text, 16))
            {
                this.myMessageBox("科室输入过长,请重新输入!", MessageBoxIcon.Warning);
                this.cmbDept.Select();

                return false;
            }

            if (this.isArriveProcess)
            {
                if (this.cmbBedNO.Tag.ToString() == string.Empty)
                {
                    this.myMessageBox("病床不能为空，请选择病床！", MessageBoxIcon.Warning);
                    this.cmbBedNO.Select();
                    return false;
                }
            }
            //-------------------Add------------------------------------------
            Neusoft.HISFC.Models.Base.PactInfo p = new Neusoft.HISFC.Models.Base.PactInfo();
            p = this.PactUnit.GetPactUnitInfoByPactCode(this.cmbPact.Tag.ToString());
            //医疗证号            
            if ((p.PayKind.ID.ToString() == "03" || p.IsNeedMCard) && this.txtMCardNO.Text.Trim() == string.Empty)
            {
                this.myMessageBox("请填写医疗证号", MessageBoxIcon.Warning);
                this.txtMCardNO.Select();
                return false;
            }
            //判断超标床和超标空调 自费患者没有报销超标所以都置为0
            if (p.PayKind.ID == "01")
            {
                this.txtBedLimit.Text = "0.00";
                this.txtAirLimit.Text = "0.00";
                this.cmbBedOverDeal.Text = "";
            }
            //判断超标处理
            if ((decimal.Parse(this.txtBedLimit.Text) > 0) && (this.cmbBedOverDeal.Text == ""))
            {
                this.cmbBedOverDeal.SelectedIndex = 1;
                this.cmbBedOverDeal.Tag = "1";
                this.cmbBedOverDeal.Text = "超标自理";
                //neusoft.neuFC.Interface.Classes.Function.Msg("请选择床费超标处理!",111);
                //this.cmbBedOverDeal.Focus();
                //return -1;
            }
            ////判断日限额是否同意超标
            //if (this.cmbBalKind.Tag.ToString() == "03")
            //{
            //    if (this.cmbOverLop.Text == "")
            //    {
            //        MessageBox.Show("请选择公费患者日限额是否允许超标");
            //        this.cmbOverLop.Focus();
            //        return false;
            //    }
            //}

            if (isSpReg)
            {
                decimal tempRate = 0;
                try
                {
                    tempRate = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.vtbRate.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("患者特殊比例输入错误!" + ex.Message);
                    this.vtbRate.Focus();
                    return false;
                }
                if (tempRate > 1 || tempRate < 0)
                {
                    MessageBox.Show("特殊患者比例请控制在0~1之间!");
                    this.vtbRate.Focus();
                    return false;
                }
                ////特殊登记 不能登记一般入院患者 Add By Maokb
                //if (this.cmbRegType.Tag.ToString() == "0")
                //{
                //    MessageBox.Show("特殊登记患者不能进行一般入院！");
                //    this.cmbRegType.Focus();
                //    return false;
                //}
            }

            //if (this.txtInpatientNO.Tag == null || this.txtInpatientNO.Tag.ToString().Trim() == "")
            //{
            //    this.myMessageBox("请回车确认住院号", MessageBoxIcon.Warning);
            //    this.txtInpatientNO.Select();

            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// 提示方法
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgIcon"></param>
        private void myMessageBox(string msg, MessageBoxIcon msgIcon)
        {
            MessageBox.Show(this, msg, "提示>>", MessageBoxButtons.OK, msgIcon);
        }

        /// <summary>
        /// 根据年龄算生日
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <returns></returns>
        private void convertBirthdayByAge(bool isUpdateAgeText)
        {
            DateTime birthDay = commonController.GetSystemTime();
            if (birthDay == null || birthDay < new DateTime(1700, 1, 1))
            {
                return;
            }
            string ageStr = this.txtAge.Text.Trim();
            int iyear = 0;
            int iMonth = 0;
            int iDay = 0;
            this.getAgeNumber(ageStr, ref iyear, ref iMonth, ref iDay);

            birthDay = commonController.GetBirthday(iyear, iMonth, iDay);

            if (birthDay < DateTime.MinValue || birthDay > DateTime.MaxValue)
            {
                MessageBox.Show("年龄输入过大请重新输入！");
                this.txtAge.Text = this.getAge(0, 0, 0);
                return;
            }
            if (isUpdateAgeText)
            {
                this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
                this.txtAge.Text = this.getAge(iyear, iMonth, iDay);
                this.dtpBirthDay.Text = birthDay.ToString("yyyy-MM-dd");
                this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            }
            else
            {
                this.dtpBirthDay.TextChanged -= new EventHandler(txtAge_TextChanged);
                this.dtpBirthDay.Text = birthDay.ToString("yyyy-MM-dd");
                this.dtpBirthDay.TextChanged += new EventHandler(txtAge_TextChanged);
            }
        }

        /// <summary>
        /// 获取年龄字符
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private string getAge(int year, int month, int day)
        {
            return string.Format("{0}岁{1}月{2}天", year <= 0 ? "___" : year.ToString().PadLeft(3, '_'), year <= 0 && month <= 0 ? "__" : month.ToString().PadLeft(2, '_'), day.ToString().PadLeft(2, '_'));
        }

        /// <summary>
        /// 获取年龄的年月日
        /// </summary>
        /// <param name="age"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        private void getAgeNumber(string age, ref int year, ref int month, ref int day)
        {
            year = 0;
            month = 0;
            day = 0;
            age = age.Replace("_", "");
            int ageIndex = age.IndexOf("岁");
            int monthIndex = age.IndexOf("月");
            int dayIndex = age.IndexOf("天");

            if (ageIndex > 0)
            {
                year = Neusoft.FrameWork.Function.NConvert.ToInt32(age.Substring(0, ageIndex));
            }
            if (ageIndex >= 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = Neusoft.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, monthIndex - ageIndex - 1));
            }
            if (ageIndex < 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = Neusoft.FrameWork.Function.NConvert.ToInt32(age.Substring(0, monthIndex));//只有月
            }
            if (monthIndex >= 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = Neusoft.FrameWork.Function.NConvert.ToInt32(age.Substring(monthIndex + 1, dayIndex - monthIndex - 1));
            }
            if (ageIndex < 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = Neusoft.FrameWork.Function.NConvert.ToInt32(age.Substring(0, dayIndex));//只有日
            }
            if (ageIndex >= 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = Neusoft.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, dayIndex - ageIndex - 1));//只有年，日
            }
        }

        /// <summary>
        /// {9B24289B-D017-4356-8A25-B0F76EB79D15}
        /// </summary>
        /// <param name="idNO"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private int processIDENNO(string idNO, EnumCheckIDNOType enumType)
        {
            string errText = string.Empty;

            //if (string.IsNullOrEmpty(idNO)) //为空的不处理
            //{
            //    return 1;
            //}

            if (idNO == "-") //特殊符号处理
            {
                return 1;
            }

            //校验身份证号

            string idNOTmp = string.Empty;
            if (idNO.Length == 15)
            {
                idNOTmp = Neusoft.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            else
            {
                idNOTmp = idNO;
            }

            //校验身份证号
            int returnValue = Neusoft.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref errText);

            if (returnValue < 0)
            {
                this.myMessageBox(errText, MessageBoxIcon.Error);
                this.txtIDNO.Focus();
                return -1;
            }
            if (!string.IsNullOrEmpty(errText))
            {
                string[] reurnString = errText.Split(',');
                if (enumType == EnumCheckIDNOType.BeforeSave)
                {
                    this.dtpBirthDay.Text = reurnString[1];
                    this.cmbSex.Text = reurnString[2];
                }
                else
                {
                    if (this.dtpBirthDay.Text != reurnString[1])
                    {
                        this.myMessageBox("输入的生日日期与身份证中号的生日不符", MessageBoxIcon.Warning);
                        this.dtpBirthDay.Focus();
                        return -1;
                    }

                    if (this.cmbSex.Text != reurnString[2])
                    {
                        this.myMessageBox("输入的性别与身份证中号的性别不符", MessageBoxIcon.Warning);
                        this.cmbSex.Focus();
                        return -1;
                    }
                }
            }
           
            return 1;
        }

        /// <summary>
        /// 查询旧系统数据
        /// </summary>
        private int queryPatientInfo()
        {
            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                this.txtName.Select();
                return -1;
            }
            //else if (this.cmbSex.Tag == null || string.IsNullOrEmpty(this.cmbSex.Text))
            //{
            //    this.cmbSex.Select();
            //    return -1;
            //}
            else
            {
                Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();

                patientInfo.Name = this.txtName.Text;
                //patientInfo.Sex.ID = this.cmbSex.Tag.ToString();
                //patientInfo.Sex.Name = this.cmbSex.Text;

                if (this.OnQueryPatientInfo != null)
                {
                    return this.OnQueryPatientInfo(patientInfo);
                }
            }

            return 0;
        }

        private void setEnable(bool eable)
        {
            this.txtName.Enabled = eable;
            this.chbencrypt.Enabled = eable;
            this.txtClinicNO.Enabled = eable;
            if (eable)
            {
                txtRealInvoiceNO.Enabled = eable;
                btnUpdateRealInvoiceNO.Enabled = eable;
                this.cmbSex.Enabled = eable;
                this.cmbSex.EnterVisiable = eable;

                this.cmbBirthArea.Enabled = eable;

                this.cmbCountry.Enabled = eable;
                this.cmbMarry.Enabled = eable;
                this.cmbProfession.Enabled = eable;
                this.cmbNation.Enabled = eable;
                this.cmbNation.EnterVisiable = eable;
                this.cmbRelation.Enabled = eable;
                this.cmbRelation.EnterVisiable = eable;
                this.cmbApproach.Enabled = eable;
                this.cmbApproach.EnterVisiable = eable;
                this.cmbCircs.Enabled = eable;
                this.cmbCircs.EnterVisiable = eable;
                this.cmbInSource.Enabled = eable;
                this.cmbInSource.EnterVisiable = eable;
                this.cmbOldDept.Enabled = eable;
                this.cmbOldDept.EnterVisiable = eable;
                this.cmbDoctor.Enabled = eable;
                this.cmbDoctor.EnterVisiable = eable;
                this.cmbPact.Enabled = eable;
                //this.cmbPact.EnterVisiable = eable;
                this.cmbPayType.Enabled = eable;
                //this.cmbPayType.EnterVisiable = eable;
            }
            else
            {
                txtRealInvoiceNO.Enabled = eable;
                btnUpdateRealInvoiceNO.Enabled = eable;
                this.cmbSex.EnterVisiable = eable;
                this.cmbSex.Enabled = eable;
                this.cmbBirthArea.Enabled = eable;
                this.cmbCountry.Enabled = eable;
                this.cmbMarry.Enabled = eable;
                this.cmbProfession.Enabled = eable;
                this.cmbNation.EnterVisiable = eable;
                this.cmbNation.Enabled = eable;
                this.cmbRelation.EnterVisiable = eable;
                this.cmbRelation.Enabled = eable;
                this.cmbApproach.EnterVisiable = eable;
                this.cmbApproach.Enabled = eable;
                this.cmbCircs.EnterVisiable = eable;
                this.cmbCircs.Enabled = eable;
                this.cmbInSource.EnterVisiable = eable;
                this.cmbInSource.Enabled = eable;
                this.cmbOldDept.EnterVisiable = eable;
                this.cmbOldDept.Enabled = eable;
                this.cmbDoctor.EnterVisiable = eable;
                this.cmbDoctor.Enabled = eable;
                //this.cmbPact.EnterVisiable = eable;
                this.cmbPact.Enabled = eable;
                //this.cmbPayType.EnterVisiable = eable;
                this.cmbPayType.Enabled = eable;
            }
            this.dtpBirthDay.Enabled = eable;
            //this.txtAge.Enabled = eable;
            this.txtIDNO.Enabled = eable;
            this.txtBirthArea.Enabled = eable;
            this.txtWorkAddress.Enabled = eable;
            this.txtWorkPhone.Enabled = eable;
            this.txtHomePhone.Enabled = eable;
            this.txtAddressNow.Enabled = eable;
            this.txtHomeAddress.Enabled = eable;
            this.txtLinkMan.Enabled = eable;
            this.txtLinkAddr.Enabled = eable;
            this.txtLinkPhone.Enabled = eable;
            this.txtDiagnose.Enabled = eable;
            this.txtMCardNO.Enabled = eable;
            //this.txtComputerNO.Enabled = eable;
            this.dtpInTime.Enabled = this.isCanModifyInTime;
            //this.mtxtBloodFee.Enabled = eable;
            this.mTxtPrepay.Enabled = eable;
            this.rdoInpatientNO.Enabled = eable;
            this.rbtnTempPatientNO.Enabled = eable;
            this.txtInpatientNO.Enabled = eable;
            this.txtHomeZip.Enabled = eable;

        }

        private int getAutoPatientNO()
        {
            string patientNO = "";
            bool isRecycle = false;
            if (Function.GetAutoPatientNO(ref patientNO, ref isRecycle) == -1)
            {
                this.myMessageBox("获得自动生成住院号出错!", MessageBoxIcon.Error);
                return -1;
            }
            this.txtInpatientNO.Text = patientNO;
            autoPatientNO = patientNO;
            this.txtInpatientNO.Tag = "T001";
            this.GetCardNO(patientNO);
            this.mTxtIntimes.Text = "1";
            return 1;
        }

        /// <summary>
        /// 用于本地化重写处理
        /// </summary>
        /// <param name="patientNO"></param>
        public virtual void GetCardNO(string patientNO)
        {
            //if (this.txtClinicNO.Enabled == false)
            //{
            //    this.txtClinicNO.Text = "T" + patientNO.Substring(1);
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(this.txtClinicNO.Text)||this.txtClinicNO.Text.Trim().StartsWith("T"))
            //    {
            //        this.txtClinicNO.Text = "T" + patientNO.Substring(1);
            //    }
            //}

            this.txtClinicNO.Text = Function.GetCardNOByPatientNO(this.txtClinicNO.Text, patientNO);
            this.txtClinicNO.Enabled = false;
        }

        /// <summary>
        /// 初始化输入法菜单
        /// </summary>
        private void setInputMenu()
        {
            for (int i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
            {
                InputLanguage t = InputLanguage.InstalledInputLanguages[i];
                System.Windows.Forms.ToolStripMenuItem m = new ToolStripMenuItem();
                m.Name = t.Culture.Name;
                m.Text = t.LayoutName;
                m.Tag = t;
                m.Click += new EventHandler(m_Click);
                if (Function.CHInput != null && Function.CHInput.Culture.Name.Equals(t.Culture.Name))
                {
                    m.Checked = true;
                }
                neuContextMenuStrip1.Items.Add(m);
            }
        }

        private void foucs()
        {
            if (this.txtClinicNO.Enabled)
            {
                this.txtClinicNO.Select();
                this.txtClinicNO.Focus();
            }
            else
            {
                this.txtName.Select();
                this.txtName.Focus();
            }

        }

        #endregion

        #region 事件

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                //查找患者信息
                if (this.txtName.Focused)
                {
                    int i = this.queryPatientInfo();
                    if (i < 0)
                    {
                        return true;
                    }
                    else if (i == 1)//说明找到患者信息且是有效的
                    {
                        this.cmbInSource.SelectAll();
                        this.cmbInSource.Focus();
                        return true;
                    }
                }
                else if (this.txtInpatientNO.Focused)
                {
                    this.save();
                    return true;
                }
                else if (this.txtMCardNO.Focused)
                {
                    this.mTxtPrepay.Focus();
                    return true;
                }
                else if (this.cmbBirthArea.Focused)
                {
                    this.cmbCountry.Focus();
                    return true;
                }
                else if (this.cmbCountry.Focused)
                {
                    cmbMarry.Focus();
                    return true;
                }
                else if (this.cmbMarry.Focused)
                {
                    switch (cmbMarry.Text)
                    {
                        case "1": cmbMarry.Tag = "M"; //已婚
                            break;
                        case "2": cmbMarry.Tag = "S"; //未婚
                            break;
                        case "3": cmbMarry.Tag = "D"; //失婚
                            break;
                        case "4": cmbMarry.Tag = "W"; //丧偶
                            break;
                        default:
                            break;
                    }
                    cmbProfession.Focus();
                    return true;
                }
                else if (this.cmbProfession.Focused)
                {
                    txtBirthArea.Focus();
                    return true;
                }
                else if (this.cmbPact.Focused)
                {
                    txtMCardNO.Focus();
                    return true;
                }
                SendKeys.Send("{Tab}");
            }
            else if (keyData == Keys.F1)
            {
                if (this.rdoInpatientNO.Enabled)
                {
                    this.rdoInpatientNO.Checked = true;
                    this.txtInpatientNO.Focus();
                    this.txtInpatientNO.SelectAll();
                }
            }
            else if (keyData == Keys.F2)
            {
                if (this.rbtnTempPatientNO.Enabled)
                {
                    this.rbtnTempPatientNO.Checked = true;
                    this.txtInpatientNO.Focus();
                    this.txtInpatientNO.SelectAll();
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void cmbDept_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbDept.Tag == null)
            {
                return;
            }

            if (deptCode.Equals(this.cmbDept.Tag.ToString()))
            {
                return;
            }
            else
            {
                deptCode = this.cmbDept.Tag.ToString();
            }

            /*  //--应广四要求不需要自动带出病区
           Neusoft.FrameWork.Models.NeuObject deptObj = this.commonController.GetDepartment(deptCode);

           if (deptObj == null)
               return;

           //查找对应的护士站           
           ArrayList al = Function.QueryNurseByDept(deptObj.ID);
           if (al != null)
           {
               //this.cmbNurseCell.Clear();
               //this.cmbNurseCell.AddItems(al);
               Neusoft.FrameWork.Models.NeuObject nurseCell = al[0] as Neusoft.FrameWork.Models.NeuObject;
               if (nurseCell != null)
               {
                   this.cmbNurseCell.Tag = nurseCell.ID;
               }
           }
            */

        }

        private void cmbNurseCell_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbNurseCell.Tag == null)
            {
                return;
            }

            if (this.nurseCode.Equals(this.cmbNurseCell.Tag.ToString()))
            {
                return;
            }
            else
            {
                this.nurseCode = this.cmbNurseCell.Tag.ToString();
            }

            ArrayList alBed = Function.QueryBedByNurse(this.nurseCode);
            if (alBed == null)
            {
                this.myMessageBox("查找床位失败！", MessageBoxIcon.Warning);
                return;
            }

            this.lblBedCount.Text = "剩" + alBed.Count + "张";
            if (alBed.Count == 0)
            {
                this.myMessageBox("病区：" + this.cmbNurseCell.Text + "目前没有床位！", MessageBoxIcon.Warning);
                return;
            }

            //包含接诊断流程
            if (this.isArriveProcess)
            {
                cmbBedNO.Clear();
                this.cmbBedNO.AddItems(alBed);
                if (alBed.Count > 0)
                {
                    this.cmbBedNO.SelectedItem = alBed[0] as Neusoft.FrameWork.Models.NeuObject;
                }
            }
        }

        private void cmbDoctor_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbDoctor.Tag == null)
            {
                return;
            }
            Neusoft.FrameWork.Models.NeuObject employee = this.commonController.GetEmployee(this.cmbDoctor.Tag.ToString());
            if (employee == null || string.IsNullOrEmpty(employee.ID))
                return;

            this.lblDoctDept.Text = this.commonController.GetDepartment(this.commonController.GetEmployee(employee.ID).Dept.ID).Name;
        }

        private void cmbInSource_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbInSource.Text.Trim() == "转科")// || this.cmbInSource.Tag.ToString() == "3")
            {
                this.cmbOldDept.Visible = true;
                this.lblOldDept.Visible = true;
            }
            else
            {
                this.cmbOldDept.Visible = false;
                this.lblOldDept.Visible = false;
                this.cmbOldDept.Text = "";
            }
        }

        private void dtpBirthDay_TextChanged(object sender, EventArgs e)
        {
            if (this.dtpBirthDay.Text == null)
            {
                return;
            }
            DateTime birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtpBirthDay.Text);

            if (birthday < new DateTime(1700, 1, 1))
            {
                return;
            }

            int iyear = 0;
            int iMonth = 0;
            int iDay = 0;

            commonController.GetAge(birthday, ref iyear, ref iMonth, ref iDay);

            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAge.Text = this.getAge(iyear, iMonth, iDay);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
        }

        void dtpBirthDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (!dtpBirthDay.IsValidInput())
                {
                    this.myMessageBox(dtpBirthDay.InputMsg, MessageBoxIcon.Warning);
                    dtpBirthDay.SelectAll();
                    return;
                }
            }
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            this.convertBirthdayByAge(false);
        }

        private void txtAge_Leave(object sender, EventArgs e)
        {
            this.convertBirthdayByAge(true);
        }

        void txtIDNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.txtIDNO.Text != "")
                {
                    int returnValue = this.processIDENNO(this.txtIDNO.Text.Trim(), EnumCheckIDNOType.BeforeSave);

                    if (returnValue < 0)
                    {
                        return;
                    }
                }
            }
        }

        private void c_Click(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                ((Control)sender).Select();
            }
        }

        private void m_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
            {
                if (sender == m)
                {
                    m.Checked = true;
                    Function.CHInput = m.Tag as InputLanguage;
                }
                else
                {
                    m.Checked = false;
                }
            }
        }

        /// <summary>
        /// 根据门诊号获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClinicNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.txtClinicNO.Text.Trim()))
                {
                    return;
                }
                string clinicNO = this.txtClinicNO.Text.Trim();
                this.Clear();

                //根据查询出来的患者信息
                Neusoft.HISFC.Models.RADT.Patient patient = Function.GetPatient(Function.GetCardNO(clinicNO));
                if (patient == null)
                {
                    this.myMessageBox("获得患者基本信息出错!" + Function.Err, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrEmpty(patient.PID.CardNO))
                {
                    this.myMessageBox("没有此患者的信息，请重新输入!", MessageBoxIcon.Information);
                    return;
                }
                this.setPatient(patient);
                int i = this.queryPatientInfo();
                if (i == 1)
                {
                    this.cmbDept.Select();
                    this.cmbDept.SelectAll();
                }
                else if (i == 0)
                {
                    this.dtpBirthDay.Select();
                }
                else if (i == -1)
                {
                    this.foucs();
                }
            }
            else if (e.KeyData == Keys.Space)
            {
                PatientInfo tempPatient = new PatientInfo();
                int resultValue = Neusoft.HISFC.Components.Common.Classes.Function.QueryComPatientInfo(ref tempPatient);
                if (resultValue == 1)
                {
                    this.setPatientInfo(tempPatient);

                    if (this.queryPatientInfo() == 1)
                    {
                        this.cmbDept.Select();
                        this.cmbDept.SelectAll();
                    }
                    else
                    {
                        this.dtpBirthDay.Select();
                    }
                }
            }
        }

        /// <summary>
        /// 选择临时号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rbtnTempPatientNO_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnTempPatientNO.Checked)
            {
                //string tempPatientNO=string.Empty;
                //if (Function.GetAutoTempPatientNO(ref tempPatientNO) > 0)
                if (Function.GetAutoTempPatientNO(ref auoPatientNOTemp) > 0)
                {
                    this.txtInpatientNO.Text = auoPatientNOTemp;
                    this.txtInpatientNO.ReadOnly = true;
                    this.txtInpatientNO.Focus();
                    this.txtInpatientNO.SelectAll();
                }
                else
                {
                    this.myMessageBox("请手动输入临时住院号，已字母L开头，" + Function.Err, MessageBoxIcon.Warning);
                    this.txtInpatientNO.Text = string.Empty;
                    this.txtInpatientNO.ReadOnly = false;
                    this.txtInpatientNO.Focus();
                    this.txtInpatientNO.SelectAll();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.autoPatientNO) == false && string.Empty.Equals(this.txtInpatientNO.Tag) == false)
                {
                    this.txtInpatientNO.Text = this.autoPatientNO;
                }
                else
                {
                    this.txtInpatientNO.Text = string.Empty;
                }

                this.txtInpatientNO.ReadOnly = false;
                this.txtInpatientNO.Focus();
                this.txtInpatientNO.SelectAll();
            }
        }

        //---------add-----------
        private void txtDayLimit_Enter(object sender, System.EventArgs e)
        {
            this.txtDayLimit.Select(0, this.txtDayLimit.Text.Length);
        }

        private void txtDayLimit_TextChanged(object sender, System.EventArgs e)
        {
            if (this.txtDayLimit.Text.Trim() == "") return;
            DayLimit = System.Convert.ToDecimal(this.txtDayLimit.Text.Trim());
            if (DayLimit >= 500)
            {
                //				MessageBox.Show("请注意,日限额达到500","提示");
                this.txtDayLimit.ForeColor = Color.Red;
            }
            else
            {
                this.txtDayLimit.ForeColor = Color.Black;
            }
        }

        private void txtBedLimit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtAirLimit.Text = this.txtBedLimit.Text;
            }
        }

        //---------add-----------

        #endregion

        /// <summary>
        /// 判断身份证
        /// </summary>
        private enum EnumCheckIDNOType
        {
            /// <summary>
            /// 保存之前校验
            /// </summary>
            BeforeSave = 0,

            /// <summary>
            /// 保存时校验
            /// </summary>
            Saveing
        }

        #region IRegisterInterface 成员

        public int Init()
        {
            this.initFile();
            this.initCombo();
            InitInvoice();
            //是否允许修改住院号
            this.dtpInTime.Enabled = this.isCanModifyInTime;
            this.foucs();
            return 1;
        }

        public void Clear()
        {
            this.autoPatientNO = "";
            this.txtInpatientNO.Tag = "";
            this.txtName.Text = string.Empty;
            this.cmbDept.Text = string.Empty;
            this.cmbDept.Tag = "";
            this.cmbBedNO.Text = string.Empty;
            this.cmbBedNO.Tag = "";
            this.cmbBedNO.AddItems(null);

            this.lblBedCount.Text = "0 张";
            this.dtpInTime.Value = commonController.GetSystemTime();
            this.txtIDNO.Text = string.Empty;
            this.cmbMarry.Text = string.Empty;
            this.cmbMarry.Tag = "";
            this.txtAddressNow.Text = string.Empty;
            this.cmbInSource.Text = string.Empty;
            this.cmbInSource.Tag = "";
            this.cmbPact.Tag = "";
            this.cmbPact.Text = "";
            this.cmbNurseCell.Text = "";
            this.cmbNurseCell.Tag = "";

            this.cmbBirthArea.Text = string.Empty;
            this.cmbBirthArea.Tag = "";
            this.cmbCountry.Text = string.Empty;
            this.cmbCountry.Tag = "";
            this.cmbProfession.Text = string.Empty;
            this.cmbProfession.Tag = "";
            this.txtLinkMan.Text = string.Empty;
            this.cmbRelation.Text = string.Empty;
            this.cmbRelation.Tag = "";
            this.txtLinkAddr.Text = string.Empty;
            this.txtWorkPhone.Text = string.Empty;
            this.cmbDoctor.Text = string.Empty;
            this.txtHomePhone.Text = string.Empty;
            this.mTxtPrepay.Text = "0.00";
            this.mTxtIntimes.Text = "1";
            this.cmbPayType.Tag = "CA";
            this.cmbPayType.Text = "现金";
            this.cmbPayType.bank = new Neusoft.HISFC.Models.Base.Bank();

            this.cmbSex.Tag = "";
            this.cmbSex.Text = "";

            this.cmbInSource.Tag = "";
            this.cmbInSource.Text = "";

            this.cmbCircs.Tag = "";
            this.cmbCircs.Text = "";

            this.cmbNation.Tag = "";
            this.cmbNation.Text = "";

            this.cmbOldDept.EnterVisiable = true;
            this.cmbOldDept.Text = "";
            this.cmbOldDept.Tag = "";

            //特殊---
            this.txtMCardNO.Text = string.Empty;
            this.txtComputerNO.Text = string.Empty;
            this.txtDiagnose.Text = string.Empty;
            this.mtxtBloodFee.Text = "0.00";
            this.txtMCardNO.Enabled = true;
            this.txtIDNO.Enabled = true;
            this.txtHomeAddress.Text = string.Empty;
            this.txtWorkAddress.Text = string.Empty;
            this.txtBirthArea.Text = string.Empty;
            this.txtInpatientNO.Text = string.Empty;
            this.txtClinicNO.Text = string.Empty;

            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAge.Text = this.getAge(0, 0, 0);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);

            this.dtpBirthDay.Text = "";

            this.txtLinkPhone.Text = string.Empty;
            this.cmbInSource.Text = "";
            this.cmbInSource.Tag = "";
            this.cmbApproach.Tag = "";

            this.chbencrypt.Checked = false;
            this.txtClinicNO.Enabled = true;
            this.txtName.Enabled = true;
            this.setEnable(true);
            this.foucs();
            this.deptCode = "";
            this.nurseCode = "";

            //---------------Add-----------------
            this.vtbRate.Text = "";      //自付比
            this.txtDayLimit.Text = "";     //公费日限
            this.txtBedLimit.Text = "";     //普通标准
            this.txtAirLimit.Text = "";      //监护床
            //this.cmbPact.SelectedItem = null; //结算方式
            //---------------EndAdd-----------------
            InitInvoice();//刷新时重新获取发票号
        }

        public void ModifyPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.setPatientInfo(patientInfo);
            //this.cmbDept.Focus();
            this.setEnable(false);
        }

        public event EventHandler OnSavePatientInfo;

        public Size ControlSize
        {
            get
            {
                return this.Size;
            }
            set
            {
                this.Size = value;
            }
        }

        public event Neusoft.SOC.HISFC.RADT.Interface.Register.SelectPatientInfo OnQueryPatientInfo;

        public bool IsArriveProcess
        {
            get
            {
                return isArriveProcess;
            }
            set
            {
                isArriveProcess = value;
            }
        }

        public bool IsCanModifyInTime
        {
            get
            {
                return this.isCanModifyInTime;
            }
            set
            {
                this.isCanModifyInTime = value;
            }
        }

        public bool IsInputValid()
        {
            return this.isInputValid();
        }

        public PatientInfo GetPatientInfo(PatientInfo patientInfo)
        {
            return getPatientInfomation(patientInfo);
        }

        public void SetPatientInfo(PatientInfo patientInfo, bool isAll)
        {
            this.setPatientInfo(patientInfo);
            this.cmbInSource.SelectAll();
            this.cmbInSource.Focus();
        }

        public Neusoft.HISFC.Models.Fee.Inpatient.Prepay GetPrepay()
        {
            //预交金实体
            Neusoft.HISFC.Models.Fee.Inpatient.Prepay prepay = new Neusoft.HISFC.Models.Fee.Inpatient.Prepay();
            prepay.PayType.ID = this.cmbPayType.Tag.ToString();
            prepay.PayType.Name = this.cmbPayType.Text;
            prepay.Bank = this.cmbPayType.bank.Clone();
            prepay.PrepayState = "0";
            prepay.BalanceState = "0";
            prepay.BalanceNO = 0;
            prepay.TransferPrepayState = "0";

            return prepay;
        }

        public bool IsAutoPatientNO
        {
            get
            {
                return this.isAutoPatientNO;
            }
            set
            {
                this.isAutoPatientNO = value;
            }
        }

        public string FileName
        {
            set
            {
                this.fileName = value;
            }
        }

        #endregion       

        private void cmbOverLop_Enter(object sender, EventArgs e)
        {
            //this.ShowInfo(this.cmbOverLop.alItems,this.richTextBox1);
            //this.fpSpread1.Visible = true;
            //this.ShowItem(6, this.cmbOverLop.alItems, 32);
        }

        private void cmbOverLop_Leave(object sender, EventArgs e)
        {
            //this.SetControlVisible(false);
            //this.fpSpread1.Visible = false;
            //this.btnOk.Focus();
        }

        private void cmbBedOverDeal_Enter(object sender, EventArgs e)
        {
            //this.ShowInfo(this.cmbBedOverDeal.alItems, this.richTextBox1);
            //this.fpSpread1.Visible = true;
            //this.ShowItem(6, this.cmbBedOverDeal.alItems, 32);
        }

        private void cmbBedOverDeal_Leave(object sender, EventArgs e)
        {
            //this.SetControlVisible(false);
            //this.fpSpread1.Visible = false;
            //this.btnOk.Focus();
        }

        /// <summary>
        /// 获得操作员的当前发票起始号
        /// </summary>
        /// <returns></returns>
        public int InitInvoice()
        {
            string invoiceNO = ""; string realInvoiceNO = ""; string errText = "";
            Neusoft.HISFC.Models.Base.Employee oper = this.feeInpatient.Operator as Neusoft.HISFC.Models.Base.Employee;
            int iReturn = this.feeIntegrate.GetInvoiceNO(oper, "P", ref invoiceNO, ref realInvoiceNO, ref errText);
            if (iReturn == -1)
            {
                MessageBox.Show(errText);
                return -1;
            }
            //显示当前发票号
            this.txtRealInvoiceNO.Text = realInvoiceNO;
            //this.tbInvoiceNO.Text = invoiceNO;            
            return 0;
        }

        /// <summary>
        /// 更新操作员发票号信息
        /// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int UpdateInvoice(string invoiceNO, ref string errInfo)
        {
            if (string.IsNullOrEmpty(invoiceNO))
            {
                errInfo = "请录入有效印刷发票号！";
                return 2;
            }
            invoiceNO = invoiceNO.PadLeft(12, '0');
            Neusoft.HISFC.Models.Base.Employee oper = feeInpatient.Operator as Neusoft.HISFC.Models.Base.Employee;
            int iRes = feeIntegrate.UpdateNextInvoiceNO(oper.ID, "P", invoiceNO);
            if (iRes <= 0)
            {
                errInfo = feeIntegrate.Err;
                return 2;
            }
            else
            {
                errInfo = "更新成功！";
                return 1;
            }
        }

        private void btnUpdateRealInvoiceNO_Click(object sender, EventArgs e)
        {
            string errInfo = "";
            int iReturn = this.UpdateInvoice(this.txtRealInvoiceNO.Text.Trim(), ref errInfo);
            if (iReturn != 2)
            {
            }
            else
            {
                MessageBox.Show(errInfo);
                return;
            }
            MessageBox.Show("更新成功！");
        }

        private void cmbSex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                switch (cmbSex.Text)
                {
                    case "1": cmbSex.Tag = "M";
                        break;
                    case "2": cmbSex.Tag = "F";
                        break;
                    default:
                        break;
                }
            }
        }
       
        private void cmbPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPact.SelectedItem == null)
            {
                return;
            }
            string strPactID = this.cmbPact.SelectedItem.ID;
            strPayKindName = "";
            Neusoft.HISFC.Models.Base.PactInfo p = new Neusoft.HISFC.Models.Base.PactInfo();
            p = this.PactUnit.GetPactUnitInfoByPactCode(strPactID);
            if (p == null)
            {
                MessageBox.Show("检索合同单位出错" + this.PactUnit.Err, "提示");
                return;
            }
            #region 医保相关注释掉的东东
            /*
            if ((this.cmbPact.SelectedItem.ID == "2" || this.cmbPact.SelectedItem.ID == "55" || this.cmbPact.SelectedItem.ID == "57" || this.cmbPact.SelectedItem.ID == "58"
                || this.cmbPact.SelectedItem.ID == "596" || this.cmbPact.SelectedItem.ID == "597" || this.cmbPact.SelectedItem.ID == "598"
                || this.cmbPact.SelectedItem.ID == "YB1001") && !MedTag)
            {
                MessageBox.Show("广州医保和居民医保不允许直接登记，请按F1键", "提示！");
                this.cmbPact.SelectedIndex = 0;
                this.txtInpatientNO.Focus();
            }
            if (this.cmbPact.SelectedItem.ID == "SXYB" && !MedTag)
            {
                MessageBox.Show("异地医保不允许直接登记，请按F5键！");
                this.cmbPact.SelectedIndex = 0;
                this.txtInpatientNO.Focus();
            }
             */
            //屏蔽 by zuowy 2007-01-16
            //if(this.cmbPact.SelectedItem.ID == "4"&&!MedTag)
            //{
            //	MessageBox.Show("请注意该生育保险患者是否享受医保待遇，如果享受请用F3选择患者后登记!","提示！");
            //				this.cmbPact.SelectedIndex = 0;
            //				this.txtInpatientNO.Focus();
            //}
            //if(this.cmbPact.SelectedItem.ID == "701"&&!MedTag)
            //{
            //	MessageBox.Show("广州医保不允许直接登记，请按F1键","提示！");
            ////	this.cmbPact.SelectedIndex = 0;
            //	this.txtInpatientNO.Focus();
            //}
            #endregion
            #region 铁路医保
            /*
            // 铁路医保住院业务
            RailwayMedicare.Function.Inpatient rmInpatient = new Inpatient();
            // 操作结果
            RailwayMedicare.Class.Enum.OperateResult result = new RailwayMedicare.Class.Enum.OperateResult();
            // 铁路医保合同单位
            string rmPact = "";
            //
            // 获取铁路医保合同单位
            //
            result = rmInpatient.GetPact(ref rmPact);
            // 如果获取成功
            if (result.Equals(RailwayMedicare.Class.Enum.OperateResult.Success))
            {
                // 如果选择的合同单位是铁路医保合同单位
                if (this.cmbPact.SelectedItem.ID.Equals(rmPact))
                {
                    // 铁路医保住院
                    RailwayMedicare.Form.frmReadCard readCardForm = new frmReadCard();
                    // 窗口返回的结果
                    DialogResult frmResult = new DialogResult();

                    // 设置窗口的类型为住院
                    readCardForm.window = "2";
                    // 设置是否需要读卡
                    readCardForm.IsReadCard = false;
                    // 打开铁路医保读卡窗口
                    frmResult = readCardForm.ShowDialog();

                    this.hosID = readCardForm.HosID;

                    // 如果确定返回
                    if (DialogResult.OK.Equals(frmResult))
                    {
                        // 获取读取的患者基本信息
                        readCardForm.GetPatient(ref this.Patient);
                        // 显示医疗证号
                        this.txtMcard.Text = this.Patient.Patient.SSN;
                        // 显示患者姓名
                        this.txtName.Text = Patient.Name;//患者姓名
                        // 显示患者性别
                        this.cmbSex.Tag = Patient.Patient.Sex.ID;//性别  Modify By Maokb
                    }
                    readCardForm.Close();
                }
            }
            if (this.cmbPact.SelectedItem.ID == "PYYB")
            {
                PYMedicare.Form.frmReadCard r = new PYMedicare.Form.frmReadCard();
                neusoft.HISFC.Object.RADT.PatientInfo tempInfo = new neusoft.HISFC.Object.RADT.PatientInfo();
                tempInfo.Patient.PID.PatientNo = this.txtInpatientNO.Text;
                //this.GetPatientInfo(tempInfo);
                r.Patient = tempInfo;
                r.ShowDialog();
                this.txtID.Text = r.Patient.Patient.IDNo;
                this.Patient.PVisit.MedicalType.ID = r.Patient.PVisit.MedicalType.ID;
                this.Patient.Patient.ClinicDiagnose = r.Patient.Patient.ClinicDiagnose;
                this.Patient.SIMainInfo.EmplType = r.Patient.SIMainInfo.EmplType;
            }
            //HZYB
            neusoft.neuFC.Object.neuObject obj = this.Constant.Get("HZYB", this.cmbPact.SelectedItem.ID);
            if (obj != null && obj.ID != "")
            {
                PYMedicare.Form.frmReadCardH r = new PYMedicare.Form.frmReadCardH();
                neusoft.HISFC.Object.RADT.PatientInfo tempInfo = new neusoft.HISFC.Object.RADT.PatientInfo();
                tempInfo.Patient.PID.PatientNo = this.txtInpatientNO.Text;
                //this.GetPatientInfo(tempInfo);
                r.Patient = tempInfo;
                r.ShowDialog();
                this.txtID.Text = r.Patient.Patient.IDNo;
                this.Patient.PVisit.MedicalType.ID = r.Patient.PVisit.MedicalType.ID;
                this.Patient.Patient.ClinicDiagnose = r.Patient.Patient.ClinicDiagnose;
                this.Patient.SIMainInfo.EmplType = r.Patient.SIMainInfo.EmplType;
            }
             */
            #endregion

            if (p.PayKind.ID == "" || p.PayKind == null)
            {
                MessageBox.Show("该合同单位的结算类别没有维护", "提示");
                return;
            }

            else
            {
                strPayKindID = p.PayKind.ID;
                //结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                #region 因不知道txtWorkAddress怎么加项而暂时注释 By kuangyh
                /* 
                switch (p.PayKind.ID)
                {
                    case "01":
                        strPayKindName = "自费";
                        this.txtWorkAddress.AddItems(Constant.GetList(neusoft.HISFC.Object.Base.enuConstant.AREA));
                        break;
                    case "02":
                        strPayKindName = "保险";
                        break;
                    case "03":
                        strPayKindName = "公费在职";
                        this.txtWorkAddress.AddItems(Constant.GetList(neusoft.HISFC.Object.Base.enuConstant.SGYDW));
                        break;
                    case "04":
                        strPayKindName = "公费退休";
                        break;
                    case "05":
                        strPayKindName = "公费高干";
                        break;
                    default:
                        strPayKindName = "自费";
                        break;
                }
                */
                #endregion

                //this.ucKnowDetail1.InitfpKnow(p.PayKind.ID);
                //this.ucKnowDetail1.ShowPermission(p.PayKind.ID);    
            }
            /*begin 根据合同单位填充自负比例,日限额,床位标准和空调标准的默认值 by zuowy*/
            this.vtbRate.Text = p.Rate.PayRate.ToString();        //自付比
            this.txtDayLimit.Text = p.DayQuota.ToString();         //公费日限
            this.txtBedLimit.Text = p.BedQuota.ToString();          //普通标准
            this.txtAirLimit.Text = p.AirConditionQuota.ToString();      //监护床
            /*end*/
        }
    }
}
