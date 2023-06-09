using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.HISFC.Models.Base;
using System.Collections;


namespace FS.HISFC.Components.Registration
{
    public partial class ucRegPatientInfo : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucRegPatientInfo()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// Manager业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        
        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();


        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// Acount业务层{A97E4C98-8820-45b9-9916-132D784D374B}
        /// </summary>
        //private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 患者信息
        /// </summary>
        /// 
        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        //private FS.HISFC.Models.Account.PatientAccount patientInfo = null;
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
        /// <summary>
        /// 存放密文
        /// </summary>
        private string NormalName = string.Empty;

        /// <summary>
        /// 门诊卡号
        /// </summary>
        private string cardNO = string.Empty;
        ///// <summary>
        ///// 是否是更新患者信息状态
        ///// </summary>
        //private bool isUpdate = true;
        //是否显示保存按钮
        private bool isShowButton = true;
        /// <summary>
        /// 是否新建门诊卡号
        /// </summary>
        private bool IsNewCardNo = true;
        /// <summary>
        /// 卡类型
        /// </summary>
        private string cardType = string.Empty;

        /// <summary>
        /// 物理卡号
        /// </summary>
        private string markNO = string.Empty;
        
        /// <summary>
        /// 卡实体{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
       // private FS.HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// 卡操作实体{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
        //private FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private bool isBulidCard = false;


        /// <summary>
        /// 是否同时更新挂号信息
        /// </summary>
        private bool updateRegInfo = false;

        private bool isSDYB = false;

        private int validDays = -1;

        #endregion

        #region 属性
        /// <summary>
        /// 门诊卡号
        /// </summary>
        public string CardNO
        {
            set
            {
                if (this.DesignMode) return;
                cardNO = value;
                if (value == string.Empty)
                    IsNewCardNo = true;
                else
                {
                    IsNewCardNo = false;
                    SetInfo(cardNO);
                }
            }
            get
            {
                return cardNO;
            }
        }

        /// <summary>
        /// 物理卡号
        /// </summary>
        public string MarkNO
        {
            get
            {
                return markNO;
            }
            set
            {
                markNO = value;
            }
        }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType
        {
            get
            {
                return cardType;
            }
            set
            {
                cardType = value;
            }
        }


        /// <summary>
        /// 是否同时更新挂号信息
        /// </summary>        
        public bool UpdateRegInfo
        {
            get { return this.updateRegInfo; }
            set { this.updateRegInfo = value; }
        }

        /// <summary>
        /// 是否显示保存按钮
        /// </summary>
        public bool IsShowButton
        {
            get
            {
                return isShowButton;
            }
            set
            {
                isShowButton = value;
            }
        }

        /// <summary>
        /// 是否处理发卡操作
        /// </summary>
        public bool IsBulidCard
        {
            set
            {
                isBulidCard = value;
            }
        }

        /// <summary>
        /// 挂号有效天数
        /// </summary>
        public int ValidDays
        {
            set
            {
                validDays = value;
            }
        }

        #endregion

        #region 事件
        public delegate void SetCardNO(string cardNO);
        /// <summary>
        /// 保存完患者基本信息后将CardNo传给ucAccount
        /// </summary>
        public event SetCardNO OnSetCardNO;
        #endregion

        #region 方法
        /// <summary>
        /// 清空数据
        /// </summary>
        protected virtual void Clear()
        {
            this.txtName.Text = string.Empty;
            this.txtIDNO.Text = string.Empty;
            this.cmbMarry.Text = string.Empty;
            this.cmbPact.Text = string.Empty;
            this.cmbArea.Text = string.Empty;
            this.cmbCountry.Text = string.Empty;
            this.cmbProfession.Text = string.Empty;
            this.txtLinkMan.Text = string.Empty;
            this.cmbRelation.Text = string.Empty;
            this.cmbLinkAddress.Text = string.Empty;
            this.txtHomePhone.Text = string.Empty;
            this.txtWorkPhone.Text = string.Empty;
            this.cmbArea.Text = string.Empty;
            this.txtName.Enabled = true;
            this.txtIDNO.Enabled = true;
            this.cmbHomeAddress.Text = string.Empty;
            this.cmbWorkAddress.Text = string.Empty;
            //this.txtAge.Text = string.Empty;
            //this.txtAge.ReadOnly = true;
            this.txtHomePhone.Text = string.Empty;
            this.txtLinkPhone.Text = string.Empty;
            this.NormalName = string.Empty;
            this.ckEncrypt.Checked = false;
            this.isSDYB = false;
            this.neuSpread2_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 初始化下拉列表
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            try
            {
                //性别列表
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
                this.cmbSex.Text = "男";

                //民族
                this.cmbNation.AddItems(managerIntegrate.GetConstantList(EnumConstant.NATION));
                this.cmbNation.Text = "汉族";

                //婚姻状态

                this.cmbMarry.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());

                //国家
                this.cmbCountry.AddItems(managerIntegrate.GetConstantList(EnumConstant.COUNTRY));
                this.cmbCountry.Text = "中国";

                //职业信息
                this.cmbProfession.AddItems(managerIntegrate.GetConstantList(EnumConstant.PROFESSION));

                //工作单位
                this.cmbWorkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //联系人信息

                this.cmbRelation.AddItems(managerIntegrate.GetConstantList(EnumConstant.RELATIVE));

                //联系人地址信息
                this.cmbLinkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //家庭住址信息
                this.cmbHomeAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //籍贯
                this.cmbDistrict.AddItems(managerIntegrate.GetConstantList(EnumConstant.DIST));


                //生日
                //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                //this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();//出生日期
                this.dtpBirthDay.Value = this.regMgr.GetDateTimeFromSysDateTime().AddYears(1);//出生日期

                //地区
                this.cmbArea.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //合同单位{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
                //this.cmbPact.AddItems(managerIntegrate.GetConstantList(EnumConstant.PACTUNIT));
                this.cmbPact.AddItems(this.feeIntegrate.QueryPactUnitOutPatient());
                this.cmbPact.IsListOnly = true;
                this.btSave.Visible = IsShowButton;
                //证件类型
                this.cmbCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));
                if(cmbCardType.Items.Count>0)
                    this.cmbCardType.SelectedIndex = 0;

                if (validDays == -1)
                {
                    validDays = FS.FrameWork.Function.NConvert.ToInt32(managerIntegrate.QueryControlerInfo("MZ0014"));

                }
            }
            catch (Exception e)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(e.Message);

                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="cardno">门诊卡号</param>
        private void SetInfo(string cardno)
        {
            if (cardno == string.Empty) return;
            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            //this.patientInfo = accountManager.GetPatientInfo(cardno);
            this.patientInfo = this.radtIntegrate.QueryComPatientInfo(cardNO);
            if (this.patientInfo != null)
            {
                SetPatient();
                //检索患者有效号
                ArrayList arlRegInfo = this.regMgr.Query(cardNO, regMgr.GetDateTimeFromSysDateTime().AddDays(-validDays));
                this.addRegisterInfo(arlRegInfo);
            }
            else
            {
                this.Clear();
            }
        }

        /// <summary>
        /// Set Age
        /// </summary>
        /// <param name="birthday"></param>
        private void setAge(DateTime birthday)
        {
            string age = this.regMgr.GetAge(birthday, true);
            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAge.Text = age;
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// 
        private void SetPatient()
        {
            //{C3115ABF-3473-43c8-BB42-89E2D9947B53}
            if (!string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
            {
                //{D5ED0D2A-8928-488c-A1B5-B78693614E1A}
                //this.txtName.Text = this.patientInfo.Name;               //姓名
                this.txtName.Tag = this.patientInfo.DecryptName;         //真实姓名   
                this.cmbSex.Text = this.patientInfo.Sex.Name;            //性别
                this.cmbSex.Tag = this.patientInfo.Sex.ID;               //性别
                this.cmbPact.Text = this.patientInfo.Pact.Name;          //合同单位名称
                this.cmbPact.Tag = this.patientInfo.Pact.ID;             //合同单位ID
                this.cmbPact.IsListOnly = true;
                this.cmbArea.Tag = this.patientInfo.AreaCode;            //地区
                this.cmbCountry.Tag = this.patientInfo.Country.ID;       //国籍
                this.cmbNation.Tag = this.patientInfo.Nationality.ID;    //民族
                //{2A19B7BA-453A-4c09-9F9B-5E7D3DA74E92}
                if (this.patientInfo.Birthday != DateTime.MinValue)
                {
                    this.dtpBirthDay.Value = this.patientInfo.Birthday;      //出生日期
                    //{A97E4C98-8820-45b9-9916-132D784D374B}
                    //this.txtAge.Text = this.accountManager.GetAge(this.patientInfo.Birthday);//年龄
                    //this.txtAge.Text = this.regMgr.GetAge(this.patientInfo.Birthday);//年龄
                    this.setAge(this.patientInfo.Birthday);//显示年龄
                }
                //this.dtpBirthDay.Value = this.patientInfo.Birthday;      //出生日期
                //this.txtAge.Text = this.accountManager.GetAge(this.patientInfo.Birthday);//年龄
                this.cmbDistrict.Text = this.patientInfo.DIST;            //籍贯
                this.cmbProfession.Tag = this.patientInfo.Profession.ID; //职业
                this.txtIDNO.Text = this.patientInfo.IDCard;             //身份证号
                this.cmbWorkAddress.Text = this.patientInfo.CompanyName; //工作单位
                this.txtWorkPhone.Text = this.patientInfo.PhoneBusiness; //单位电话
                this.cmbMarry.Tag = this.patientInfo.MaritalStatus.ID.ToString();//婚姻状况
                this.cmbHomeAddress.Text = this.patientInfo.AddressHome;  //家庭住址
                this.txtHomePhone.Text = this.patientInfo.PhoneHome;     //家庭电话
                this.txtLinkMan.Text = this.patientInfo.Kin.Name;        //联系人 
                this.cmbRelation.Tag = this.patientInfo.Kin.Relation.ID; //联系人关系
                this.cmbLinkAddress.Text = this.patientInfo.Kin.RelationAddress;//联系人地址
                this.txtLinkPhone.Text = this.patientInfo.Kin.RelationPhone;//联系人电话
                this.ckEncrypt.Checked = this.patientInfo.IsEncrypt; //是否加密姓名
                this.txtMCardNO.Text = this.patientInfo.SSN;//医疗证号
                if (this.ckEncrypt.Checked)
                {
                    this.NormalName = this.patientInfo.NormalName; //密文
                    //{D5ED0D2A-8928-488c-A1B5-B78693614E1A}
                    this.txtName.Text = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.NormalName);
                }
                else
                {
                    this.txtName.Text = this.patientInfo.Name;
                }

                if (patientInfo.Pact.ID == "2" || patientInfo.Pact.ID == "3")
                {
                    this.isSDYB = true;
                }
                else
                {
                    this.isSDYB = false;
                }
                
            }
        }

        /// <summary>
        /// 数据合理化校验
        /// </summary>
        /// <returns></returns>
        protected virtual bool InputValid()
        {
            if (this.txtName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("请输入患者姓名，姓名不能为空！"));
                this.txtName.Focus();
                return false;
            }

            if (this.cmbSex.Tag.ToString() == string.Empty)
            {
                MessageBox.Show(Language.Msg("请输入患者性别，性别不能为空！"));
                this.cmbSex.Focus();
                return false;
            }

            if (this.cmbPact.Tag.ToString() == string.Empty)
            {
                MessageBox.Show(Language.Msg("请输入合同单位，合同单位不能为空！"));
                this.cmbPact.Focus();
                return false;
            }

            if (this.cmbPact.Tag.ToString()=="2"||this.cmbPact.Tag.ToString()=="3")
            {
                if (!this.isSDYB)
                {
                    MessageBox.Show(Language.Msg("若要将合同单位改为医保请重新挂号！"));
                    this.cmbPact.Focus();
                    return false;
                }
            }

            if (!this.ckEncrypt.Checked && this.txtName.Text=="******")
            {
                MessageBox.Show(Language.Msg("该患者姓名没有加密，请输入正确的患者姓名！"));
                this.txtName.Focus();
                this.txtName.SelectAll();
                return false;
            }
            //判断单位电话长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWorkPhone.Text, 25))
            {
                MessageBox.Show(Language.Msg("单位电话号码长度过长"));
                this.txtWorkPhone.Focus();
                return false;
            }
            //判断家庭电话号码长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomePhone.Text, 25))
            {
                MessageBox.Show(Language.Msg("家庭电话号码长度过长"));
                this.txtHomePhone.Focus();
                return false;
            }
            if (this.cmbCardType.SelectedItem!=null && this.cmbCardType.SelectedItem.Name=="身份证")
            {

                //{FD1FD98C-1997-42a4-A046-3EFB15DCA804}
                string errText = string.Empty;
                if ((this.txtIDNO.Text.Trim() != null && this.txtIDNO.Text.Trim() != ""))
                {
                    int returnValue = this.ProcessIDENNO(this.txtIDNO.Text.Trim(), EnumCheckIDNOType.Saveing);
                    if (returnValue < 0)
                    {
                        return false;
                    }
                }

            }


            //判断联系电话号码长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkPhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("联系人电话号码长度过长"));
                this.txtLinkPhone.Focus();
                return false;
            }

            //判断联系电话号码长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text, 16))
            {
                MessageBox.Show(Language.Msg("姓名长度过长"));
                this.txtName.Focus();
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkMan.Text, 10))
            {
                MessageBox.Show(Language.Msg("联系人姓名过长"));
                this.txtLinkMan.Focus();
                return false;
            }
            //判断性别
            if (this.cmbSex.Text.Trim() == "")
            {
                MessageBox.Show(Language.Msg("性别不能为空，请输入性别"));
                this.cmbSex.Focus();
                return false;
            }
            //this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();//出生日期
            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            //if (this.dtpBirthDay.Value > this.accountManager.GetDateTimeFromSysDateTime().Date)
            if (this.dtpBirthDay.Value > this.regMgr.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show(Language.Msg("出生日期大于当前日期,请重新输入!"));
                this.dtpBirthDay.Focus();

                return false;
            }

           
            return true;
        }


        /// <summary>
        /// 显示挂号患者信息
        /// </summary>
        /// <param name="registerList"></param>
        private void addRegisterInfo(ArrayList registerList)
        {
            //先清空
            this.neuSpread2_Sheet1.Rows.Count = 0;

            if (registerList == null || registerList.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < registerList.Count; i++)
            {
                FS.HISFC.Models.Registration.Register reg = registerList[i] as FS.HISFC.Models.Registration.Register;
                if (reg == null)
                {
                    return;
                }

                this.neuSpread2_Sheet1.Rows.Add(this.neuSpread2_Sheet1.Rows.Count, 1);
                int index = this.neuSpread2_Sheet1.Rows.Count - 1;

                this.neuSpread2_Sheet1.SetValue(index, 0, i == registerList.Count - 1 ? updateRegInfo : reg.IsSee, false);
                this.neuSpread2_Sheet1.Cells[index, 0].Locked = !updateRegInfo;
                this.neuSpread2_Sheet1.SetValue(index, 1, reg.InvoiceNO, false);
                this.neuSpread2_Sheet1.SetValue(index, 2, reg.Name, false);
                this.neuSpread2_Sheet1.SetValue(index, 3, reg.DoctorInfo.SeeDate.ToString(), false);
                this.neuSpread2_Sheet1.SetValue(index, 4, reg.DoctorInfo.Templet.RegLevel.Name, false);
                this.neuSpread2_Sheet1.SetValue(index, 5, reg.DoctorInfo.Templet.Dept.Name, false);
                this.neuSpread2_Sheet1.SetValue(index, 6, reg.DoctorInfo.Templet.Doct.Name, false);
                this.neuSpread2_Sheet1.SetValue(index, 7, reg.RegLvlFee.RegFee + reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee + reg.RegLvlFee.OthFee, false);
                this.neuSpread2_Sheet1.SetValue(index, 8, reg.IsSee, false);
                this.neuSpread2_Sheet1.Rows[index].Tag = reg;

            }

        }

       
        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// <summary>
        /// 获取名字字符串
        /// </summary>
        /// <param name="patient"></param>
        //private void GetPatienName(FS.HISFC.Models.Account.PatientAccount patient)
        private void GetPatienName(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //选择加密
            if (ckEncrypt.Checked)
            {
                string encryptStr = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.txtName.Tag.ToString());
                //在更新
                if (!IsNewCardNo)
                {
                    //如果名字为空
                    if (this.NormalName == string.Empty)
                    {
                        patient.Name = "******";
                        patient.NormalName = encryptStr;
                    }
                    else
                    {
                        if (encryptStr == this.NormalName)
                        {
                            if (this.txtName.Text == "******")
                            {
                                patient.Name = this.txtName.Text;
                                patient.NormalName = encryptStr;
                            }
                            else
                            {
                                patient.Name = "******";
                                patient.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.txtName.Text.Trim());
                            }
                        }
                        else
                        {
                            patientInfo.Name = "******";
                            patientInfo.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.txtName.Text.Trim());
                        }
                    }
                }
                else
                {
                    patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.txtName.Text.Trim());
                }
            }
            else
            {
                patientInfo.Name = this.txtName.Text;
            }
        }

        /// <summary>
        /// 获得患者实体
        /// </summary>
        /// <returns></returns>
         
        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        //private FS.HISFC.Models.Account.PatientAccount GetPatientInfomation()
        private FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation()
        {

            //patientInfo = new FS.HISFC.Models.Account.PatientAccount();
            patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            patientInfo.PID.CardNO = cardNO;//门诊卡号
            patientInfo.Pact.ID = this.cmbPact.Tag.ToString();//合同单位  
            patientInfo.Pact.Name = this.cmbPact.Text.ToString();//合同单位名称
            this.GetPatienName(patientInfo); //患者姓名       
            patientInfo.Sex.ID = this.cmbSex.Tag.ToString();//性别
            patientInfo.AreaCode = this.cmbArea.Tag.ToString();//地区
            patientInfo.Country.ID = this.cmbCountry.Tag.ToString();//国籍
            patientInfo.Nationality.ID = this.cmbNation.Tag.ToString();//民族
            patientInfo.Birthday = this.dtpBirthDay.Value;//出生日期
            patientInfo.DIST = this.cmbDistrict.Text.ToString();//籍贯
            patientInfo.Profession.ID = this.cmbProfession.Tag.ToString();//职业
            patientInfo.IDCard = this.txtIDNO.Text;//证件号
            patientInfo.IDCardType.ID = this.cmbCardType.Tag.ToString();//证件类型
            patientInfo.CompanyName = this.cmbWorkAddress.Text;//工作单位
            patientInfo.PhoneBusiness = this.txtHomePhone.Text;//单位电话
            patientInfo.PhoneBusiness = this.txtWorkPhone.Text;//单位电话
            patientInfo.MaritalStatus.ID = this.cmbMarry.Tag.ToString();//婚姻状况 
            patientInfo.AddressHome = this.cmbHomeAddress.Text.ToString();//家庭住址
            patientInfo.PhoneHome = this.txtHomePhone.Text;//家庭电话
            patientInfo.Kin.Name = this.txtLinkMan.Text;//联系人 
            patientInfo.Kin.Relation.ID = this.cmbRelation.Tag.ToString();//与联系人关系
            patientInfo.Kin.RelationAddress = this.cmbLinkAddress.Text;//联系人地址
            patientInfo.Kin.RelationPhone = this.txtLinkPhone.Text;  //联系人电话
            patientInfo.Kin.User01 = this.cmbLinkAddress.Text;//联系人地址
            patientInfo.Kin.Memo = this.txtLinkPhone.Text; //联系人电话
            //patientInfo.Oper.ID = this.accountManager.Operator.ID; //操作员
            //patientInfo.Oper.OperTime = this.accountManager.GetDateTimeFromSysDateTime();//操作时间
            patientInfo.IsEncrypt = this.ckEncrypt.Checked;
            patientInfo.SSN = this.txtMCardNO.Text;
         
            return patientInfo;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public virtual void save()
        {
            if (this.cardNO == string.Empty)
            {
                MessageBox.Show(Language.Msg("请输入卡号，然后在保存保存数据！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!this.InputValid())
            {
                return;
            }

            //的到患者实体
            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            // FS.HISFC.Models.Account.PatientAccount patientInfo = this.GetPatientInfomation();
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.GetPatientInfomation();
            if (patientInfo == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者基本信息不能为空"));
                return;
            }            

            #region 设置事物

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new Transaction(this.accountManager.Connection);
            //trans.BeginTransaction();
            ////{DA67A335-E85E-46e1-A672-4DB409BCC11B
            //this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #endregion

            #region 发卡
            
            #endregion

            #region  保存患者信息
           

            int returnValue = 0;
            
            returnValue = radtIntegrate.RegisterComPatient(patientInfo);
            if (returnValue <= 0)
            { 
                //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                //MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存数据失败！") + this.accountManager.Err);
                //FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存数据失败！") + this.radtIntegrate.Err);
               
                return;
            }

            if (feeIntegrate.UpdatePatientIden(patientInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存数据失败！") + this.feeIntegrate.Err);
                return;
            }

            //更新最近一次挂号记录
            if (this.neuSpread2_Sheet1.RowCount > 0 && updateRegInfo)
            {
                for (int i = 0; i < this.neuSpread2_Sheet1.RowCount; i++)
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread2_Sheet1.Cells[i, 0].Value))
                    {
                        FS.HISFC.Models.Registration.Register register = this.neuSpread2_Sheet1.Rows[i].Tag as FS.HISFC.Models.Registration.Register;
                        register.Name = patientInfo.Name;
                        register.Sex.ID = patientInfo.Sex.ID;
                        register.Birthday = patientInfo.Birthday;
                        register.IDCard = patientInfo.IDCard;
                        register.IDCardType = patientInfo.IDCardType;
                        register.SSN = patientInfo.SSN;
                        int resultValue = this.regMgr.UpdateRegInfo(register);
                        if (resultValue < 0)
                        {
                            FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("变更发票号为" + register.InvoiceNO + "的挂号信息失败：" + regMgr.Err);
                            return;
                        }

                        if (InterfaceManager.GetIADT() != null)
                        {
                            if (InterfaceManager.GetIADT().PatientInfo(patientInfo, register) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(this, "修改患者信息失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;

                            }
                        }

                    }
                }
            }
            else
            {
                if (InterfaceManager.GetIADT() != null)
                {
                    if (InterfaceManager.GetIADT().PatientInfo(patientInfo, new FS.HISFC.Models.Registration.Register()) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this, "修改患者信息失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;

                    }
                }
            }

            #endregion
            FS.FrameWork.Management.PublicTrans.Commit();
            //显示患者信息
            this.SetInfo(cardNO);
            //把cardno给ucaccount
            if (OnSetCardNO != null)
                OnSetCardNO(cardNO);
            IsNewCardNo = false;
            
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存数据成功！"), FS.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Clear();
         }

        //打印
        /// <summary>
         /// 条码打印{D2F77BDA-F5E5-48fe-AB73-B7FE6D92E6E2}
        /// </summary>
        public void PrintBar()
        {
            FS.HISFC.BizProcess.Interface.Registration.IPrintBar ip = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar))
            as FS.HISFC.BizProcess.Interface.Registration.IPrintBar;
            if (ip == null)//默认实现打印
            {
                if(string.IsNullOrEmpty(CardNO))
                {
                    MessageBox.Show("病历号为空，不能打印");
                    return;
                }

                FS.FrameWork.WinForms.Controls.ucBaseControl uc = new FS.FrameWork.WinForms.Controls.ucBaseControl();
                FS.FrameWork.WinForms.Controls.NeuPictureBox p = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
                p.Image = FS.FrameWork.WinForms.Classes.CodePrint.GetCode39(CardNO);
                FS.FrameWork.WinForms.Controls.NeuPanel pn = new FS.FrameWork.WinForms.Controls.NeuPanel();
                pn.Controls.Add(p);
                pn.BackColor = Color.White;
                uc.Controls.Add(pn);
                uc.BackColor = Color.White;

                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.PrintPage(0, 0, uc);
            }
            else //接口实现打印
            {
                string errText = string.Empty;
                int returnValue = ip.printBar((patientInfo as FS.HISFC.Models.RADT.Patient), ref errText);
                if (returnValue < 0)
                {
                    MessageBox.Show(errText);
                    return;
                }
            }

        }

        #endregion

        #region 事件
        private void ucPatientInfo_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.Init();
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            this.save();
        }

        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
        {
            ////{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            //this.txtAge.Text = this.accountManager.GetAge(this.dtpBirthDay.Value);
            //this.txtAge.Text = this.regMgr.GetAge(this.dtpBirthDay.Value);
            this.setAge(this.dtpBirthDay.Value);
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            DateTime birthDay = this.regMgr.GetDateTimeFromSysDateTime();
            string ageStr = this.txtAge.Text.Trim();
            ageStr = ageStr.Replace("_", "");
            int iyear = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(0, ageStr.IndexOf("岁")));
            int iMonth = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(ageStr.IndexOf("岁") + 1, ageStr.IndexOf("月") - ageStr.IndexOf("岁") - 1));
            int iDay = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(ageStr.IndexOf("月") + 1, ageStr.IndexOf("天") - ageStr.IndexOf("月") - 1));
            birthDay = birthDay.AddYears(-iyear).AddMonths(-iMonth).AddDays(-iDay - 1);
            this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);
            if (birthDay < dtpBirthDay.MinDate || birthDay > dtpBirthDay.MaxDate)
            {
                MessageBox.Show("年龄输入过大请重新输入！");
                this.txtAge.Text = "  0岁 0月 0天";
                this.txtAge.Focus();
                return;
            }
            this.dtpBirthDay.Value = birthDay;
            this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.txtIDNO.ContainsFocus)//{FD1FD98C-1997-42a4-A046-3EFB15DCA804}
                {
                    string idNO = txtIDNO.Text.Trim();
                    if (!string.IsNullOrEmpty(idNO))
                    {
                        int returnValue = this.ProcessIDENNO(idNO, EnumCheckIDNOType.BeforeSave);
                        if (returnValue < 0)
                        {
                            return false;
                        }

                    }
                }
                if (btSave.ContainsFocus)
                {
                    btSave_Click(this, null);
                }
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                System.Type[] t = new Type[1];

                t[1] = typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar);
                return t;
            }

        }

        #endregion

       
        /// <summary>
        /// {FD1FD98C-1997-42a4-A046-3EFB15DCA804}
        /// </summary>
        /// <param name="idNO"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private int ProcessIDENNO(string idNO, EnumCheckIDNOType enumType)
        {
            string errText = string.Empty;

            //校验身份证号


            //{99BDECD8-A6FC-44fc-9AAA-7F0B166BB752}

            //string idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            string idNOTmp = string.Empty;
            if (idNO.Length == 15)
            {
                idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            else
            {
                idNOTmp = idNO;
            }

            //校验身份证号
            int returnValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref errText);



            if (returnValue < 0)
            {
                MessageBox.Show(errText);
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
                    //this.txtAge.Text = this.regMgr.GetAge(this.dtpBirthDay.Value);
                    this.setAge(this.dtpBirthDay.Value);
                    //this.cmbPayKind.Focus();
                }
                else
                {
                    if (this.dtpBirthDay.Value.Date != (FS.FrameWork.Function.NConvert.ToDateTime(reurnString[1])).Date)
                    {
                        MessageBox.Show("输入的生日日期与身份证中号的生日不符");
                        this.dtpBirthDay.Focus();
                        return -1;
                    }

                    if (this.cmbSex.Text != reurnString[2])
                    {
                        MessageBox.Show("输入的性别与身份证中号的性别不符");
                        this.cmbSex.Focus();
                        return -1;
                    }
                }
            }
            return 1;
        }

       
        /// <summary>
        /// 判断身份证//{FD1FD98C-1997-42a4-A046-3EFB15DCA804}身份证信息
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
    }
}
