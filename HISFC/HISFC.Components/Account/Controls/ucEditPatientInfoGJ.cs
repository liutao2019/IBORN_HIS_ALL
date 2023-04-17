using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FS.FrameWork.Models;
using FS.HISFC.BizProcess.Interface.Account;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// 患者基本信息修改
    /// </summary>
    public partial class ucEditPatientInfoGJ : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucEditPatientInfoGJ()
        {
            InitializeComponent();
        }
        #region 变量
        /// <summary>
        /// 健康业务层
        /// </summary>
        private FS.HISFC.BizLogic.HealthCard.HealthCardManager healthCardManager = new FS.HISFC.BizLogic.HealthCard.HealthCardManager();

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        /// <summary>
        /// 卡类型帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper markTypeHelp = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 健康卡实体
        /// </summary>
        private FS.HISFC.BizLogic.HealthCard.HealthCard healthCard = new FS.HISFC.BizLogic.HealthCard.HealthCard();

        /// <summary>
        /// 卡操作实体
        /// </summary>
        private FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;

        /// <summary>
        /// 帐户管理业务层
        /// </summary>
        private HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 就诊卡实体
        /// </summary>
        private HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// 是否自动打印
        /// </summary>
        private bool isAutoPrint = false;

        /// <summary>
        /// 是否直接挂号
        /// </summary>
        private bool isRegister = false;

        /// <summary>
        /// 是否打印标签
        /// </summary>
        private bool isPrintCard = false;

        /// <summary>
        /// 办卡标签打印接口
        /// </summary>
        IPrintLable iPrintLable = null;

        /// <summary>
        /// 自动打印时那些卡类型自动打印，以“;”结尾
        /// </summary>
        private string printCardType = "";

        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 入出转实体
        /// </summary>
        private HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 挂号管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 输入类型 
        /// </summary>
        private EnumInputType inputType=new EnumInputType();

        /// <summary>
        /// 患者信息
        /// </summary>
        private HISFC.Models.RADT.PatientInfo oldPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 变更记录业务层
        /// </summary>
        HISFC.BizProcess.Integrate.Function functionIntegrate = new FS.HISFC.BizProcess.Integrate.Function();

        /// <summary>
        /// EMPI管理类
        /// </summary>
        //private FS.HISFC.BizProcess.Integrate.Registration.EMPIManager empiManager = new FS.HISFC.BizProcess.Integrate.Registration.EMPIManager();

        /// <summary>
        /// 是否可以修改最近一次挂号记录
        /// </summary>
        private bool isCanEditLastRegInfo = false;

        #endregion

        #region 修改控制属性
        [Category("修改控制"), Description("费用来源是否可以修改")]
        public bool IsEnablePact
        {
            get 
            {
                return this.ucRegPatientInfo1.IsEnablePact; 
            }
            set
            {
                this.ucRegPatientInfo1.IsEnablePact = value;
            }
        }


        [Category("修改控制"), Description("医保证号是否可以修改")]
        public bool IsEnableSiNO
        {
            get 
            { 
                return  this.ucRegPatientInfo1.IsEnableSiNO; 
            }
            set
            {
                this.ucRegPatientInfo1.IsEnableSiNO = value;
            }
        }

        [Category("修改控制"), Description("是否可以修改证件类型")]
        public bool IsEnableIDEType
        {
            get 
            {
                return this.ucRegPatientInfo1.IsEnableIDEType;  
            }
            set
            {
                this.ucRegPatientInfo1.IsEnableIDEType = value;
            }
        }

        [Category("修改控制"), Description("是否判断本院职工")]
        public string IsValidHospitalStaff
        {
            get 
            {
                return this.ucRegPatientInfo1.IsValidHospitalStaff;
            }
            set
            {
                this.ucRegPatientInfo1.IsValidHospitalStaff = value;
            }
        }

        [Category("修改控制"), Description("是否可以修改证件号")]
        public bool IsEnableIDENO
        {
            get 
            {
                return this.ucRegPatientInfo1.IsEnableIDENO; 
            }
            set
            {
                this.ucRegPatientInfo1.IsEnableIDENO = value;
            }
        }

        [Category("修改控制"), Description("患者姓名加密是否可以修改")]
        public bool IsEnableEntry
        {
            get
            {
                return this.ucRegPatientInfo1.IsEnableEntry;
            }
            set
            {
                this.ucRegPatientInfo1.IsEnableEntry = value;
            }
        }

        [Category("修改控制"), Description("是否可以修改Vip标识")]
        public bool IsEnableVip
        {
            get
            {
                return this.ucRegPatientInfo1.IsEnableVip;
            }
            set
            {
                this.ucRegPatientInfo1.IsEnableVip = value;
            }
        }

        [Category("控件设置"),Description("输入类型 CardNO:门诊卡号 MarkNO:就诊卡号")]
        public EnumInputType InputType
        {
            get
            {
                return inputType;
            }
            set
            {
                inputType = value;
            }
        }

        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 是否可以修改最近一次挂号记录
        /// </summary>
        [Category("修改控制"), Description("是否可以修改最近一次挂号记录（3天以内）")]
        public bool IsCanEditLastRegInfo
        {
            get
            {
                return isCanEditLastRegInfo;
            }
            set
            {
                isCanEditLastRegInfo = value;
            }
        }

        [Category("参数设置"), Description("一卡多合同单位")]
        public bool IsMutilPactInfo
        {
            get
            {
                return this.ucRegPatientInfo1.IsMutilPactInfo;
            }
            set
            {
                this.ucRegPatientInfo1.IsMutilPactInfo = value;
            }
        }
        [Description("是否自动打印标签卡"), Category("设置")]
        public bool IsAutoPrint
        {
            get
            {
                return isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        [Description("自动打印时那些卡类型自动打印，以“;”结尾"), Category("设置")]
        public string PrintCardType
        {
            get
            {
                return printCardType;
            }
            set
            {
                this.printCardType = value;
            }
        }

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        /// <summary>
        /// 在输入过程中是否动态查找患者信息
        /// </summary>
        private bool isSelectPatientByEnter = false;

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        [Category("控件设置"), Description("在输入过程中是否动态查找患者信息 True:是 False:否")]
        public bool IsSelectPatientByEnter
        {
            get
            {
                isSelectPatientByEnter = this.ucRegPatientInfo1.IsSelectPatientByNameIDCardByEnter;
                return isSelectPatientByEnter;
            }
            set
            {
                isSelectPatientByEnter = value;
                this.ucRegPatientInfo1.IsSelectPatientByNameIDCardByEnter = isSelectPatientByEnter;
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="consList">提示信息集合</param>
        private void DealConstantList(ArrayList consList)
        {
            if (consList == null || consList.Count <= 0)
            {
                return;
            }

            this.spInfo.RowCount = 0;
            this.spInfo.RowCount = (consList.Count / 3) + (consList.Count % 3 == 0 ? 0 : 1);

            int row = 0;
            int col = 0;

            foreach (FS.FrameWork.Models.NeuObject obj in consList)
            {
                if (col >= 5)
                {
                    col = 0;
                    row++;
                }

                this.spInfo.SetValue(row, col, obj.ID);
                this.spInfo.SetValue(row, col + 1, obj.Name);

                col = col + 2;
            }
        }

        /// <summary>
        /// 保存患者信息
        /// </summary>
        protected virtual void SavePatient()
        {
            if (!this.ucRegPatientInfo1.InputValid()) return;
             
            HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
            FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #region 更新患者基本信息
            int resultValue = radtIntegrate.RegisterComPatient(patient);
            if (resultValue <= 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存患者信息失败！" + accountManager.Err);
                return;
            }

            if (accountManager.InsertPatientPactInfo(patient) < 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存患者多个合同单位信息失败！" + accountManager.Err);
                return;
            }

            //更新最近一次挂号记录
            if (isCanEditLastRegInfo)
            {
                if (MessageBox.Show("是否修改最近一次挂号信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    resultValue = regIntegrate.UpdateRegByPatientInfo(patient);
                    if (resultValue < 0)
                    {
                        FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新挂号信息失败：" + regIntegrate.Err);
                        return;
                    }
                }
            }

            #endregion

            //变更记录由触发器代替

            resultValue = functionIntegrate.SaveChange<HISFC.Models.RADT.Patient>(false, false, patient.PID.CardNO, oldPatient, patient);
            if (resultValue < 0)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("生成变更记录失败！");
                return;
            }

            #region 集成平台EMPI
            
            //if (this.empiManager.SaveOutpatientEMPI(this.ucRegPatientInfo1.GetPatientInfomation()) == -1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg(this.empiManager.Err));
            //    return;
            //}

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            string register = this.controlParma.GetControlParam<string>("MZ9951", false, "0");

            if (register == "0")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存患者信息成功！"), FS.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //if (MessageBox.Show("是否直接挂号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //{
                //this.save();
                //}
            }

            //打印标签
            if (this.isAutoPrint)
            {
                DialogResult result = MessageBox.Show("是否直接打印标签","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                if (result==DialogResult.Yes)
                {
                    if (printCardType.Contains(accountCard.MarkType.ID))
                    {
                        PrintLable();
                    }
                }
            }


            //FrameWork.Management.PublicTrans.Commit();
            //MessageBox.Show("保存患者信息成功！");
            this.ucRegPatientInfo1.Clear(true);
            this.txtMarkNO.Text = string.Empty;
            this.txtMarkNO.Focus();
        }
        /// <summary>
        /// 打印标签
        /// </summary>
        private void PrintLable()
        {
            if (iPrintLable != null)
            {
                if (accountCard == null)
                {
                    return;
                }
                iPrintLable.PrintLable(accountCard);
            }
            else
            {
                MessageBox.Show("未能打印，请联系信息科维护标签打印接口[IPrintLable]！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        protected virtual void Clear()
        {
            this.ucRegPatientInfo1.Clear(true);
            this.txtMarkNO.Text = string.Empty;
            this.txtName.Text = string.Empty;
            //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
            this.spPatient.RowCount = 0;
        }

        /// <summary>
        /// 添加身份证按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBar.AddToolButton("清空", "清空", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBar.AddToolButton("身份证", "身份证", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M模版, true, false, null);
            toolBar.AddToolButton("刷卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            return toolBar;
        }

        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "身份证")
            {
                this.ucRegPatientInfo1.Clear(false);
                this.ucRegPatientInfo1.ReadIDCard();
            }
            else if (e.ClickedItem.Text == "刷卡")
            {
                string cardNo = "";
                string error = "";
                if (Function.OperMCard(ref cardNo, ref error) == -1)
                {
                    MessageBox.Show(error, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtMarkNO.Text = cardNo;
                txtMarkNO.Focus();
                this.txtMarkNO_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }
            else if (e.ClickedItem.Text == "清空")
            {
                this.Clear();
            }
        }
        /// <summary>
        /// 查找患者信息
        /// </summary>
        //{4F908F64-CD37-4690-A092-FB21AB29B2BC}
        protected virtual void QueryPatientInfo()
        {
            string cardNO = string.Empty;
            string tempStr = this.txtMarkNO.Text.Trim();
            string showStr = string.Empty;
            if (tempStr == string.Empty) return;
            if (InputType == EnumInputType.MarkNO)
            {
                accountCard = new FS.HISFC.Models.Account.AccountCard();
                int resultValue = accountManager.GetCardByRule(tempStr, ref accountCard);

                //获取本地卡信息失败
                if (resultValue <= 0)
                {
                    //if (accountCard.MarkType.Name.Contains("健康"))
                    //{
                    //    //外院发放的健康卡，从区域平台上获取患者基本信息到本地
                    //    if (!accountCard.MarkNO.StartsWith(healthCard.UnitCode))
                    //    {
                    //        healthCard.DepartmentCode = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                    //        healthCard.CardNumber = accountCard.MarkNO;
                    //        int getResult;
                    //        getResult = healthCardManager.GetPatientInfo(healthCard);
                    //        if (getResult == 3)
                    //        {
                    //            try
                    //            {
                    //                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    //                this.isPrintCard = true;
                    //                if (this.BulidCard(accountCard) == -1)
                    //                {
                    //                    FS.FrameWork.Management.PublicTrans.RollBack();
                    //                    MessageBox.Show("获取患者信息失败，请重试或者去发卡界面录入患者信息！");
                    //                    this.Clear();
                    //                    return;
                    //                }

                    //                if (accountManager.InsertIdenInfo(patientInfo) == -1)
                    //                {
                    //                    if (this.accountManager.DBErrCode == 1)
                    //                    {
                    //                        if (accountManager.UpdateIdenInfo(patientInfo) == -1)
                    //                        {
                    //                            FS.FrameWork.Management.PublicTrans.RollBack();
                    //                            MessageBox.Show("插入证件号码表出错！"+accountManager.Err);
                    //                            this.Clear();
                    //                            return ;
                    //                        }
                    //                    }
                    //                }

                    //                //加入照片
                    //                if (healthCard.Photo != null)
                    //                {
                    //                    if (accountManager.UpdatePhoto(patientInfo,healthCard.Photo) == -1)
                    //                    {
                    //                        FS.FrameWork.Management.PublicTrans.RollBack();
                    //                        MessageBox.Show("更新图片出错！"+accountManager.Err);
                    //                        this.Clear();
                    //                        return;
                    //                    }
                    //                }

                    //                FS.FrameWork.Management.PublicTrans.Commit();
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                FS.FrameWork.Management.PublicTrans.RollBack();
                    //                MessageBox.Show("获取患者信息失败，请重试或者去发卡界面录入患者信息！");
                    //                this.Clear();
                    //                return;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("获取患者信息失败，请重试或者去发卡界面录入患者信息！");
                    //            this.Clear();
                    //            return;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show(accountManager.Err);
                    //        this.txtMarkNO.Focus();
                    //        this.txtMarkNO.SelectAll();
                    //        return;
                    //    }
                    //    this.accountCard.Patient = patientInfo;
                    //}
                    //else
                    //{
                        MessageBox.Show(accountManager.Err);
                        this.txtMarkNO.Focus();
                        this.txtMarkNO.SelectAll();
                        return;
                    //}
                }
                cardNO = this.accountCard.Patient.PID.CardNO;
                showStr = accountCard.MarkNO;

            }
            if (InputType == EnumInputType.CardNO)
            {
                cardNO = tempStr;
                //cardNO = cardNO.PadLeft(HISFC.BizProcess.Integrate.Common.ControlParam.GetCardNOLen(), '0');
                cardNO = cardNO.PadLeft(10, '0');
                showStr = cardNO;
            }
            this.txtMarkNO.Text = showStr;
            this.ucRegPatientInfo1.CardNO = cardNO;

            //#endregion
            if (this.ucRegPatientInfo1.IsJudgePactByIdno)
            {
                this.ucRegPatientInfo1.JudgePactByIdno();
            }
            this.ucRegPatientInfo1.Focus();
        }

        /// <summary>
        /// 建立新的病理卡号
        /// </summary>
        private int BulidCard(AccountCard tempAccountCard)
        {
            this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            string cardNO =null;
            string register = this.controlParma.GetControlParam<string>("MZ9950", false, "0");
            if (register == "0")
            {
                cardNO = outPatientManager.GetAutoCardNO();//门诊卡号
            }
            else
            {
                FS.HISFC.BizProcess.Integrate.RADT Mgr = new FS.HISFC.BizProcess.Integrate.RADT();
                ArrayList al = Mgr.QueryPatientByName(healthCard.Name);
                if (al != null && al.Count != 0)
                {
                    cardNO = this.GetCardNoByName(al, healthCard.Id);
                }
                //不新建一个门诊号，使用原有的门诊号
                //DataTable IdennoList = outPatientManager.GetAutoCardNObyIdenno(healthCard.Id);
                //if (IdennoList == null)
                //{
                //    cardNO = outPatientManager.GetAutoCardNO();//门诊卡号
                //}
                //else
                //{

                //    foreach (DataRow dr in IdennoList.Rows)
                //    {
                //        cardNO = dr["card_no"].ToString().Trim();
                //    }
                //}
            }
            if (string.IsNullOrEmpty(cardNO))
            {
                cardNO = outPatientManager.GetAutoCardNO();//门诊卡号
            }
            patientInfo.PID.CardNO = cardNO.PadLeft(10, '0');

            //if (this.cmbPact.Tag != null && this.cmbPact.Tag.ToString() != string.Empty)
            //    patientInfo.Pact.PayKind = GetPayKindFromPactID(this.cmbPact.Tag.ToString());//结算类别
            //patientInfo.Pact.ID = this.cmbPact.Tag.ToString();//合同单位  
            //patientInfo.Pact.Name = this.cmbPact.Text.ToString();//合同单位名称
            patientInfo.Name = healthCard.Name;//患者姓名
            //patientInfo.IsEncrypt = false;//是否加密

            if (healthCard.Sex == "1")
            {
                patientInfo.Sex.ID = "M";//性别
            }
            if (healthCard.Sex == "2")
            {
                patientInfo.Sex.ID = "F";
            }

            //patientInfo.AreaCode = this.cmbArea.Tag.ToString();//地区
            //patientInfo.Country.ID = this.cmbCountry.Tag.ToString();//国籍
            //patientInfo.Nationality.ID = this.cmbNation.Tag.ToString();//民族
            patientInfo.Birthday = DateTime.ParseExact(healthCard.Birthday, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);//出生日期
            //patientInfo.Age = outpatientManager.GetAge(patientInfo.Birthday);//年龄
            //patientInfo.DIST = this.cmbDistrict.Text.ToString();//籍贯
            patientInfo.Profession.ID = healthCard.Profession;//职业
            FS.FrameWork.Public.ObjectHelper IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper(
                                                            managerIntegrate.QueryConstantList("IDCard"));//证件类型
            patientInfo.IDCard = healthCard.Id;//证件号
            patientInfo.IDCardType.ID = healthCard.IdType;//证件类型
            patientInfo.IDCardType.Name = IdCardTypeHelp.GetName(healthCard.IdType);
            //patientInfo.CompanyName = this.cmbWorkAddress.Text.Trim();//工作单位
            patientInfo.PhoneBusiness = healthCard.Mphone;//单位电话
            //patientInfo.MaritalStatus.ID = this.cmbMarry.Tag.ToString();//婚姻状况 
            patientInfo.AddressHome = healthCard.Address;//家庭住址
            patientInfo.PhoneHome = healthCard.Phone;//家庭电话
            //patientInfo.Kin.Name = this.txtLinkMan.Text.Trim();//联系人 
            //patientInfo.Kin.Relation.ID = this.cmbRelation.Tag.ToString();//与联系人关系
            //patientInfo.Kin.RelationAddress = this.cmbLinkAddress.Text;//联系人地址
            //patientInfo.Kin.RelationPhone = this.txtLinkPhone.Text.Trim();  //联系人电话
            //patientInfo.VipFlag = this.ckVip.Checked; //是否vip
            //patientInfo.MatherName = this.txtMatherName.Text;//母亲姓名
            //patientInfo.IsTreatment = this.IsTreatment;//是否急诊
            //patientInfo.SSN = this.txtSiNO.Text;//社会保险号
            //patientInfo.Kin.RelationDoorNo = this.txtLinkManDoorNo.Text.Trim();//联系人地址门牌号
            //patientInfo.AddressHomeDoorNo = healthCard.IdAddress;//家庭地址门牌号
            //patientInfo.Email = txtEmail.Text.Trim();//电子邮箱

            if (radtIntegrate.RegisterComPatient(patientInfo) < 0)
            {
                MessageBox.Show(radtIntegrate.Err);
                return -1;
            }

            tempAccountCard.Patient = patientInfo;
            tempAccountCard.IsValid = true;
            try
            {
                if (accountManager.InsertAccountCard(tempAccountCard) == -1)
                {
                    MessageBox.Show("保存卡记录失败！" + accountManager.Err, "错误");
                    return -1;
                }
                accountCardRecord = new FS.HISFC.Models.Account.AccountCardRecord();
                //插入卡的操作记录
                accountCardRecord.MarkNO = tempAccountCard.MarkNO;
                accountCardRecord.MarkType.ID = tempAccountCard.MarkType.ID;
                accountCardRecord.CardNO = tempAccountCard.Patient.PID.CardNO;
                accountCardRecord.OperateTypes.ID = (int)FS.HISFC.Models.Account.MarkOperateTypes.Begin;
                accountCardRecord.Oper.ID = (this.accountManager.Operator as FS.HISFC.Models.Base.Employee).ID;

                if (accountManager.InsertAccountCardRecord(accountCardRecord) == -1)
                {
                    MessageBox.Show("保存卡操作记录失败！" + accountManager.Err);
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！" + ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// 通过患者姓名检索患者挂号信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string GetCardNoByName(ArrayList al,string idenNO)
        {
            Forms.frmQueryPatientByIdenno f = new Forms.frmQueryPatientByIdenno();

            f.QueryByName(al,idenNO);
            DialogResult dr = f.ShowDialog();

            //取消的时候 不应获取了，否则会覆盖别人的信息！！！！！！！！
            if (dr == DialogResult.OK)// || dr == DialogResult.Cancel)
            {
                string CardNo = f.SelectedCardNo;
                f.Dispose();
                return CardNo;
            }

            f.Dispose();

            return null;
        }
        #endregion

        #region 事件
        private void ucEditPatientInfo_Load(object sender, EventArgs e)
        {
            //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
            ArrayList al = managerIntegrate.GetConstantList("MarkType");
            markTypeHelp.ArrayObject = al;

            if (al == null)
            {
                MessageBox.Show("查找就诊卡类型失败");
                return;
            }


            this.ucRegPatientInfo1.IsEditMode = true;
            this.ucRegPatientInfo1.CmbFoucs += new HandledEventHandler(ucRegPatientInfo1_CmbFoucs);
            //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
            this.ucRegPatientInfo1.OnEnterSelectPatient += new HandledEventHandler(ucRegPatientInfo1_OnEnterSelectPatient);
            iPrintLable = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintLable)) as IPrintLable;
            this.ActiveControl = this.txtMarkNO;
        }

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        void ucRegPatientInfo1_OnEnterSelectPatient(object sender, HandledEventArgs e)
        {
            if (this.IsSelectPatientByEnter)
            {
                this.QueryPatientInfoByEnter();
            }
        }

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        protected int QueryPatientInfoByEnter()
        {
            FS.HISFC.Models.RADT.PatientInfo patientTemp = this.ucRegPatientInfo1.patientInfo;
            FS.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();
            this.ucRegPatientInfo1.patientInfo = patientTemp;

            // 性别与合同单位不做为查询条件
            return this.QueryPatientInfo(patient.Name,
                                         patient.PhoneHome,
                                         patient.Mobile,
                                         patient.IDCardType.ID,
                                         patient.IDCard,
                                         patient.SSN);
        }

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        protected virtual int QueryPatientInfo(string patientName, string homePhone, string mobile, string idCardType, string idCard, string SSN)
        {
            if (string.IsNullOrEmpty(patientName)
             && string.IsNullOrEmpty(idCard)
             && string.IsNullOrEmpty(homePhone)
             && string.IsNullOrEmpty(mobile))
            {
                return -1;
            }

            //if (!IsSelectPatientByNameIDCardByEnter)
            //{
            //    if (string.IsNullOrEmpty(idCardType) && string.IsNullOrEmpty(idCard))
            //    {
            //        return -1;
            //    }
            //}

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查找患者信息，请稍后...");
            Application.DoEvents();
            //查找患者信息

            //如果证件号码为空，证件类型为空
            if (string.IsNullOrEmpty(idCard))
            {
                idCardType = "";
            }
            List<AccountCard> list = accountManager.GetAccountCard(patientName, homePhone, mobile, idCardType, idCard, SSN);

            if (list == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(accountManager.Err);
                return -1;
            }
            try
            {
                if (this.spPatient.Rows.Count > 0)
                {
                    this.spPatient.Rows.Remove(0, this.spPatient.Rows.Count);
                }
                this.spPatient.Rows.Count = list.Count;
                int count = 0, beginIndex = 0, rangCount = 1;
                count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    AccountCard tempCard = list[i];
                    //姓名
                    this.spPatient.Cells[i, 0].Text = tempCard.Patient.Name;
                    //性别
                    this.spPatient.Cells[i, 1].Text = tempCard.Patient.Sex.Name;
                    //生日
                    this.spPatient.Cells[i, 2].Text = this.accountManager.GetAge(tempCard.Patient.Birthday);
                    //民族
                    this.spPatient.Cells[i, 3].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.Nationality.ID, 0);
                    //合同单位
                    this.spPatient.Cells[i, 4].Text = tempCard.Patient.Pact.Name;
                    //证件类型
                    this.spPatient.Cells[i, 5].Text = this.ucRegPatientInfo1.GetName(tempCard.Patient.IDCardType.ID, 1);
                    //证件号
                    this.spPatient.Cells[i, 6].Text = tempCard.Patient.IDCard;
                    string telephone = "";
                    if (tempCard.Patient.PhoneHome != null && tempCard.Patient.PhoneHome != "")
                    {
                        telephone = tempCard.Patient.PhoneHome;
                    }
                    else if (tempCard.Patient.Kin.RelationPhone != null && tempCard.Patient.Kin.RelationPhone != "")
                    {
                        telephone = tempCard.Patient.Kin.RelationPhone;
                    }
                    else
                    {
                        telephone = "";
                    }
                    this.spPatient.Cells[i, 7].Text = telephone;
                    this.spPatient.Cells[i, 8].Text = tempCard.Patient.AddressHome;
                    this.spPatient.Cells[i, 9].Text = tempCard.Patient.PID.CardNO;
                    this.spPatient.Cells[i, 10].Text = markTypeHelp.GetName(tempCard.MarkType.ID);
                    this.spPatient.Cells[i, 11].Text = tempCard.MarkNO;
                    this.spPatient.Columns.Get(11).Visible = true;
                    //{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
                    //if (isShowMarkNo)
                    //{
                    //}
                    this.spPatient.Rows[i].Tag = tempCard;
                    //计算合并单元格
                    if (i < count - 1)
                    {
                        if (tempCard.Patient.PID.CardNO == list[i + 1].Patient.PID.CardNO)
                        {
                            rangCount += 1;
                            if (i == count - 2)
                            {
                                if (rangCount > 1)
                                {
                                    RangFpCell(beginIndex, rangCount);
                                }
                            }
                        }
                        else
                        {
                            if (rangCount > 1)
                            {
                                RangFpCell(beginIndex, rangCount);
                            }
                            beginIndex = i + 1;
                            rangCount = 1;
                        }
                    }

                }
                this.neuSpread1.ActiveSheet = spPatient;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="count"></param>
        private void RangFpCell(int begin, int count)
        {
            for (int col = 0; col < this.spPatient.Columns.Count - 2; col++)
            {
                this.spPatient.Models.Span.Add(begin, col, count, 1);
            }
        }


        private void ucRegPatientInfo1_CmbFoucs(object sender, EventArgs e)
        {
            if (sender is FS.FrameWork.WinForms.Controls.NeuComboBox)
            {
                FrameWork.WinForms.Controls.NeuComboBox cmb = sender as FrameWork.WinForms.Controls.NeuComboBox;
                ArrayList al = cmb.alItems;
                DealConstantList(al);
                this.neuSpread1.ActiveSheet = this.spInfo;
            }//{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
            else
            {
                this.neuSpread1.ActiveSheet = this.spPatient;
            }
        }

        private void txtMarkNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //{4F908F64-CD37-4690-A092-FB21AB29B2BC}
                QueryPatientInfo();
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNO;
                string name = this.txtName.Text;
                FS.HISFC.Components.Common.Forms.frmQueryPatientByName f = new FS.HISFC.Components.Common.Forms.frmQueryPatientByName();
                if (f.QueryByName(name) > 0)
                {
                    DialogResult dr = f.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        cardNO = f.SelectedCardNo;
                        this.ucRegPatientInfo1.CardNO = cardNO;

                        if (this.ucRegPatientInfo1.IsJudgePactByIdno)
                        {
                            this.ucRegPatientInfo1.JudgePactByIdno();
                        }
                        this.ucRegPatientInfo1.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("没有找到相应患者信息!");
                }
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            //{4F908F64-CD37-4690-A092-FB21AB29B2BC}
            QueryPatientInfo1();
            return base.OnQuery(sender, neuObject);
        }

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        /// <summary>
        /// 查询患者信息
        /// </summary>
        protected virtual int QueryPatientInfo1()
        {
            FS.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.GetPatientInfomation();

            if (string.IsNullOrEmpty(patient.Name) == true)
            {
                DialogResult dr = MessageBox.Show("当前查询姓名为空！\n将消耗很长时间，是否继续？", "提示", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    return -1;
                }
            }

            // 性别与合同单位不做为查询条件
            return this.QueryPatientInfo(patient.Name,
                                         patient.PhoneHome,
                                         patient.Mobile,
                                         patient.IDCardType.ID,
                                         patient.IDCard,
                                         patient.SSN);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            SavePatient();
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 处理快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                this.txtName.Focus();
            }
            if (keyData == Keys.F2)
            {
                this.txtMarkNO.Focus();
            }

            return base.ProcessDialogKey(keyData);
        }

        #endregion

        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        private int GetRecipeType = 2;
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private int save()
        {
            //if (this.valid() == -1)
            //    return 2;

            // 费用明细
            List<AccountCardFee> lstAccFee = null;

            if (this.getValue(out lstAccFee) == -1)
                return 2;

            //if (this.ValidCardNO(this.regObj.PID.CardNO) < 0)
            //{
            //    return -1;
            //}

            //if (this.IsPrompt)
            //{
            //    //确认提示
            //    if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("请确认录入数据是否正确"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            //        MessageBoxDefaultButton.Button1) == DialogResult.No)
            //    {
            //        this.cmbRegLevel.Focus();
            //        return -1;
            //    }
            //}


            int rtn;
            string Err = "";
            ////接口实现{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
            //if (this.iProcessRegiter != null)
            //{
            //    rtn = this.iProcessRegiter.SaveBegin(ref regObj, ref Err);

            //    if (rtn < 0)
            //    {
            //        MessageBox.Show(Err);
            //        return -1;
            //    }
            //}

            //this.MedcareInterfaceProxy.SetPactCode(this.regObj.Pact.ID);

            #region 账户新增
            //bool isAccountFee = false;
            //decimal vacancy = 0;
            //int result = this.feeMgr.GetAccountVacancy(this.regObj.PID.CardNO, ref vacancy);
            //if (result > 0)
            //{
            //    if (!feeMgr.CheckAccountPassWord(this.regObj))
            //        return -1;
            //    if (vacancy > 0)
            //    {
            //        isAccountFee = true;
            //    }

            //}
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.regMgr.con);
            //t.BeginTransaction();

            //this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.SchemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.SIMgr.SetTrans(t);
            //this.InterfaceMgr.SetTrans(t.Trans);


            //事务开始
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            // 卡费用
            // {23BA226E-A1E5-4a0b-A1D5-92FA97AF3E85}
            AccountCardFee cardFee = null;

            #region 卡费用特殊处理

            //if (chbCardFee.Visible && chbCardFee.Checked)
            //{
            //    AccountCard accountCard = this.txtCardNo.Tag as AccountCard;
            //    if (accountCard != null)
            //    {
            //        cardFee = new AccountCardFee();
            //        cardFee.FeeType = AccCardFeeType.CardFee;
            //        cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            //        cardFee.MarkNO = accountCard.MarkNO;
            //        cardFee.MarkType = accountCard.MarkType;

            //        FS.HISFC.Models.Base.Const obj = cardFee.MarkType as FS.HISFC.Models.Base.Const;
            //        if (obj != null)
            //        {
            //            cardFee.Tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(obj.UserCode);
            //        }
            //        cardFee.Own_cost = cardFee.Tot_cost;

            //        cardFee.IsBalance = false;
            //        cardFee.BalanceNo = "";
            //        cardFee.BalnaceOper.ID = "";
            //        cardFee.IStatus = 1;

            //    }
            //}

            if (lstAccFee == null)
            {
                lstAccFee = new List<AccountCardFee>();
            }
            if (cardFee != null)
            {
                lstAccFee.Add(cardFee);

                // 处理挂号记录， 卡费用归到挂号表其他费用中
                this.regObj.RegLvlFee.OthFee += cardFee.Tot_cost;
                this.regObj.OwnCost += cardFee.Own_cost;
                this.regObj.PubCost += cardFee.Pub_cost;
                this.regObj.PayCost += cardFee.Pay_cost;
            }

            //如果打印发票则lstAccFee.count > 0；不打印发票lstAccFee.count = 0
            //如果要打印发票的情况，费用必须要有挂号费信息，否则不让挂号
            if (lstAccFee.Count > 0)
            {
                bool isExistRegFee = false;
                foreach (FS.HISFC.Models.Account.AccountCardFee tempCardFee in lstAccFee)
                {
                    if (tempCardFee.FeeType == AccCardFeeType.RegFee)
                    {
                        isExistRegFee = true;
                        break;
                    }
                }

                if (!isExistRegFee)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    //this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show("挂号必须要有挂号费信息!", "警告");
                    return -1;
                }

            }

            #endregion

            #region 取发票号

            string strInvioceNO = "";
            string strRealInvoiceNO = "";
            string strErrText = "";
            int iRes = 0;
            string strInvoiceType = "R";

            FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

            //有费用信息的时候才打发票
            if (lstAccFee.Count > 0)
            {

                if (this.GetRecipeType == 1)
                {
                    strInvioceNO = this.regObj.RecipeNO.ToString().PadLeft(12, '0');
                    strRealInvoiceNO = "";
                }
                else
                {
                    if (this.GetRecipeType == 2)
                    {
                        strInvoiceType = "R";
                    }
                    else if (this.GetRecipeType == 3)
                    {
                        // 取门诊收据
                        strInvoiceType = "C";
                    }

                    iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText);

                    if (iRes == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(strErrText);
                        return -1;
                    }
                }
            }

            this.regObj.InvoiceNO = strInvioceNO;

            #endregion



            #region 处理费用明细信息

            //有费用信息的时候才处理
            if (lstAccFee.Count > 0)
            {

                foreach (AccountCardFee accFee in lstAccFee)
                {
                    accFee.InvoiceNo = strInvioceNO;
                    accFee.Print_InvoiceNo = strRealInvoiceNO;
                    accFee.ClinicNO = this.regObj.ID;

                    accFee.Patient.PID.CardNO = this.regObj.PID.CardNO;
                    accFee.Patient.Name = this.regObj.Name;

                    accFee.IStatus = 1;

                    accFee.FeeOper.ID = employee.ID;
                    accFee.FeeOper.Name = employee.Name;
                    accFee.FeeOper.OperTime = current;

                    accFee.Oper.ID = employee.ID;
                    accFee.Oper.Name = employee.Name;
                    accFee.Oper.OperTime = current;

                    accFee.IsBalance = false;
                    accFee.BalanceNo = "";

                }

            }

            #endregion

            decimal OwnCostTot = this.regObj.OwnCost;

            #region 账户新增
            //if (isAccountFee)
            //{
            //    decimal cost = 0m;

            //    if (vacancy < OwnCostTot)
            //    {
            //        cost = vacancy;
            //        this.regObj.PayCost = vacancy;
            //        this.regObj.OwnCost = this.regObj.OwnCost - vacancy;
            //    }
            //    else
            //    {
            //        cost = OwnCostTot;
            //        this.regObj.PayCost = this.regObj.OwnCost;
            //        this.regObj.OwnCost = 0;
            //    }
            //    if (this.feeMgr.AccountPay(this.regObj, cost, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID, "R") < 0)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show(this.feeMgr.Err);
            //        return -1;
            //    }
            //    this.regObj.IsAccount = true;
            //}
            #endregion

            try
            {
                #region 更新看诊序号
                int orderNo = 0;

                //2看诊序号		
                if (this.UpdateSeeID(this.regObj.DoctorInfo.Templet.Dept.ID, this.regObj.DoctorInfo.Templet.Doct.ID,
                    this.regObj.DoctorInfo.Templet.Noon.ID, this.regObj.DoctorInfo.SeeDate, ref orderNo,
                    ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                regObj.DoctorInfo.SeeNO = orderNo;

                //专家、专科、特诊、预约号更新排班限额
                #region schema
                //if (this.UpdateSchema(this.SchemaMgr, this.regObj.RegType, ref orderNo, ref Err) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    if (Err != "")
                //        MessageBox.Show(Err, "提示");
                //    return -1;
                //}

                //regObj.DoctorInfo.SeeNO = orderNo;
                #endregion

                //1全院流水号			
                if (this.Update(this.regMgr, current, ref orderNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                regObj.OrderNO = orderNo;
                #endregion

                //预约号更新已看诊标志
                #region booking
                //if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                //{
                //    //更新看诊限额
                //    rtn = this.bookingMgr.Update((this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).ID,
                //                true, regMgr.Operator.ID, current);
                //    if (rtn == -1)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("更新预约看诊信息出错!" + this.bookingMgr.Err, "提示");
                //        return -1;
                //    }
                //    if (rtn == 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show(FS.FrameWork.Management.Language.Msg("预约挂号信息状态已经变更,请重新检索"), "提示");
                //        return -1;
                //    }
                //}
                #endregion

                #region 待遇接口实现
                //this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //this.MedcareInterfaceProxy.Connect();

                //this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //this.MedcareInterfaceProxy.BeginTranscation();

                //this.regObj.SIMainInfo.InvoiceNo = this.regObj.InvoiceNO;
                //int returnValue = this.MedcareInterfaceProxy.UploadRegInfoOutpatient(this.regObj);
                //if (returnValue == -1)
                //{
                //    this.MedcareInterfaceProxy.Rollback();
                //    FS.FrameWork.Management.PublicTrans.RollBack()
                //        ;
                //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("上传挂号信息失败!") + this.MedcareInterfaceProxy.ErrMsg);

                //    return -1;
                //}
                //////医保患者登记医保信息
                ////if (this.regObj.Pact.PayKind.ID == "02")
                ////{
                //this.regObj.OwnCost = this.regObj.SIMainInfo.OwnCost;  //自费金额
                //this.regObj.PubCost = this.regObj.SIMainInfo.PubCost;  //统筹金额
                //this.regObj.PayCost = this.regObj.SIMainInfo.PayCost;  //帐户金额
                ////}
                #endregion

                #region addby xuewj 2010-3-15

                //if (this.adt == null)
                //{
                //    this.adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
                //}
                //if (this.adt != null)
                //{
                //    this.adt.RegOutPatient(this.regObj);
                //}

                #endregion

                //登记挂号信息
                if (this.regMgr.Insert(this.regObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    //this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                #region 保存费用明细信息

                if (lstAccFee != null && lstAccFee.Count > 0)
                {
                    if (this.feeMgr.SaveAccountCardFee(lstAccFee) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //this.MedcareInterfaceProxy.Rollback();
                        MessageBox.Show(this.feeMgr.Err, "提示");
                        return -1;
                    }
                }

                #endregion


                ////更新患者基本信息
                //if (this.UpdatePatientinfo(this.regObj, this.patientMgr, this.regMgr, ref Err) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    this.MedcareInterfaceProxy.Rollback();
                //    MessageBox.Show(Err, "提示");
                //    return -1;
                //}
                ////接口实现{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
                //if (this.iProcessRegiter != null)
                //{
                //    //{4F5BD7B2-27AF-490b-9F09-9DB107EA7AA0}
                //    //rtn = this.iProcessRegiter.SaveBegin(ref regObj, ref Err);

                //    rtn = this.iProcessRegiter.SaveEnd(ref regObj, ref Err);
                //    if (rtn < 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        this.MedcareInterfaceProxy.Rollback();
                //        MessageBox.Show(Err);
                //        return -1;
                //    }
                //}

                //处理医保患者
                //this.MedcareInterfaceProxy.UploadRegInfoOutpatient

                #region 发票走号

                //有费用信息的时候，才处理发票
                if (lstAccFee.Count > 0)
                {

                    if (this.GetRecipeType == 2 || this.GetRecipeType == 3)
                    {
                        string invoiceStytle = controlParma.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
                        if (this.feeMgr.UseInvoiceNO(employee, invoiceStytle, strInvoiceType, 1, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }

                        if (this.feeMgr.InsertInvoiceExtend(strInvioceNO, strInvoiceType, strRealInvoiceNO, "00") < 1)
                        {
                            // 发票头暂时先保存00
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }
                    }

                }

                #endregion

                FS.FrameWork.Management.PublicTrans.Commit();
                //this.MedcareInterfaceProxy.Commit();
                //this.MedcareInterfaceProxy.Disconnect();

                //最后更新处方号,加 1,防止中途返回跳号
                //this.UpdateRecipeNo(1);

                //this.QueryRegLevl();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            #region 找零放在发票打印后面【废弃】

            ////找零{F0661633-4754-4758-B683-CB0DC983922B}
            //if (this.isShowChangeCostForm)
            //{
            //    rtn = this.ShowChangeForm(this.regObj);
            //    {
            //        if (rtn < 0)
            //        {
            //            return -1;
            //        }
            //    }
            //}

            #endregion

            // 记录发票费用的打印信息
            this.regObj.LstCardFee = lstAccFee;

            //有费用信息的时候，才打印发票
            if (lstAccFee.Count > 0)
            {

                //if (this.isAutoPrint)
                //{
                this.PrintReg(this.regObj, this.regMgr);
                //}
                //else
                //{
                //    DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择是否打印挂号票"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                //    if (rs == DialogResult.Yes)
                //    {
                //        this.Print(this.regObj, this.regMgr);
                //    }
                //}

            }
            else if (lstAccFee.Count == 0)
            {
                MessageBox.Show("挂号成功! 不打印发票!", "提示");
            }


            //this.addRegister(this.regObj);

            //this.clear();
            //ChangeInvoiceNOMessage();
            return 0;


        }

        /// <summary>
        /// 根据病历号获得患者挂号信息
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register getRegInfo(string CardNo)
        {
            if (string.IsNullOrEmpty(CardNo))
            {
                return null;
            }

            FS.HISFC.Models.Registration.Register ObjReg = new FS.HISFC.Models.Registration.Register();
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.RADT.PatientInfo p;
            int regCount = this.regMgr.QueryRegiterByCardNO(CardNo);

            if (regCount == 1)
            {
                ObjReg.IsFirst = false;
            }
            else
            {
                if (regCount == 0)
                {
                    ObjReg.IsFirst = true;

                }
                else
                {
                    return null;
                }
            }
            //先检索患者基本信息表,看是否存在该患者信息
            p = radt.QueryComPatientInfo(CardNo);

            if (p == null || p.Name == "")
            {
                //不存在基本信息
                ObjReg.PID.CardNO = CardNo;
                //ObjReg.IsFirst = true;
                ObjReg.Sex.ID = "M";
                //ObjReg.Pact.ID = this.DefaultPactID;
            }
            else
            {
                //存在患者基本信息,取基本信息

                ObjReg.PID.CardNO = CardNo;
                ObjReg.Name = p.Name;
                ObjReg.Sex.ID = p.Sex.ID;
                ObjReg.Birthday = p.Birthday;
                ObjReg.Pact.ID = p.Pact.ID;
                ObjReg.Pact.PayKind.ID = p.Pact.PayKind.ID;
                ObjReg.SSN = p.SSN;
                ObjReg.PhoneHome = p.PhoneHome;
                ObjReg.AddressHome = p.AddressHome;
                ObjReg.IDCard = p.IDCard;
                ObjReg.NormalName = p.NormalName;
                ObjReg.IsEncrypt = p.IsEncrypt;
                //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}
                ObjReg.IDCard = p.IDCard;

                if (p.IsEncrypt == true)
                {
                    ObjReg.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(p.NormalName);
                }
                //this.chbEncrpt.Checked = p.IsEncrypt;
                ////ObjReg.IsFirst = false;

                //if (this.validCardType(p.IDCardType.ID))//借用Memo存储证件类别
                //{
                //    ObjReg.CardType.ID = p.IDCardType.ID;

                //}
            }

            return ObjReg;
        }

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <returns></returns>
        private int getValue(out List<AccountCardFee> lstAccFee)
        {

            FS.HISFC.Models.RADT.PatientInfo patient = this.ucRegPatientInfo1.patientInfo;
            regObj = getRegInfo(patient.PID.CardNO);
            lstAccFee = null;
            //门诊号
            this.regObj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
            this.regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//正交易

            this.regObj.DoctorInfo.Templet.RegLevel.ID = RegisterLevel;//this.cmbRegLevel.Tag.ToString();
            //this.regObj.DoctorInfo.Templet.RegLevel.Name = this.cmbRegLevel.Text;
            //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
            this.regObj.DoctorInfo.Templet.RegLevel.IsEmergency = false;

            this.regObj.DoctorInfo.Templet.Dept.ID = registerDept;
            //this.regObj.DoctorInfo.Templet.Dept.Name = this.cmbDept.Text;

            //this.regObj.DoctorInfo.Templet.Doct.ID = this.cmbDoctor.Tag.ToString();
            //this.regObj.DoctorInfo.Templet.Doct.Name = this.cmbDoctor.Text;

            //{0BA561B1-376F-4412-AAD0-F19A0C532A03}
            this.regObj.Name = patient.Name;//FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtName.Text.Trim(), "'");//患者姓名
            this.regObj.Sex.ID = patient.Sex.ID;// this.cmbSex.Tag.ToString();//性别

            this.regObj.Birthday = patient.Birthday;//this.dtBirthday.Value;//出生日期			

            //FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            //this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;
            ////不为空说明是预约号
            //if (this.txtOrder.Tag != null)
            //{
            //    this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Pre;
            //}
            //else if (level.IsSpecial)
            //{
            //    this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Spe;
            //}

            //FS.HISFC.Models.Registration.Schema schema = null;

            ////只有专家、专科、特诊需要输入看诊时间段、更新限额
            //if (this.regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre
            //            && (level.IsSpecial || level.IsFaculty || level.IsExpert))
            //{
            //    schema = this.GetValidSchema(level);
            //    if (schema == null)
            //    {
            //        MessageBox.Show("预约时间指定错误,没有符合条件的排班信息!", "提示");
            //        this.dtBookingDate.Focus();
            //        return -1;
            //    }
            //    this.SetBookingTag(schema);
            //}


            //if (level.IsExpert && this.regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre)
            //{
            //    if (this.VerifyIsProfessor(level, schema) == false)
            //    {
            //        this.cmbRegLevel.Focus();
            //        return -1;
            //    }
            //}


            #region 结算类别
            this.regObj.Pact.ID = patient.Pact.ID;//this.cmbPayKind.Tag.ToString();//合同单位
            //this.regObj.Pact.Name = this.cmbPayKind.Text;

            FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
            if (pact == null || pact.ID == "")
            {
                MessageBox.Show("获取代码为:" + this.regObj.Pact.ID + "的合同单位信息出错!" + this.conMgr.Err, "提示");
                return -1;
            }
            this.regObj.Pact.Name = pact.Name;
            this.regObj.Pact.PayKind.Name = pact.PayKind.Name;
            this.regObj.Pact.PayKind.ID = pact.PayKind.ID;
            //this.regObj.SSN = this.txtMcardNo.Text.Trim();//医疗证号

            //if (pact.IsNeedMCard && this.regObj.SSN == "")
            //{
            //    MessageBox.Show("需要输入医疗证号!", "提示");
            //    this.txtMcardNo.Focus();
            //    return -1;
            //}
            ////人员黑名单判断
            //if (this.validMcardNo(this.regObj.Pact.ID, this.regObj.SSN) == -1)
            //    return -1;

            #endregion

            this.regObj.PhoneHome = patient.PhoneHome;//FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtPhone.Text.Trim(), "'");//联系电话
            this.regObj.AddressHome = patient.AddressHome;//FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtAddress.Text.Trim(), "'");//联系地址
            //this.regObj.CardType.ID = this.cmbCardType.Tag.ToString();

            #region 预约时间段
            //if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)//预约号扣排班限额
            //{
            //    this.regObj.IDCard = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).IDCard;
            //    this.regObj.DoctorInfo.Templet.Noon.ID = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.Noon.ID;
            //    this.regObj.DoctorInfo.Templet.IsAppend = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.IsAppend;
            //    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtBegin.Value.ToString("HH:mm:ss"));//挂号时间
            //    this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
            //    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtEnd.Value.ToString("HH:mm:ss"));//结束时间
            //    this.regObj.DoctorInfo.Templet.ID = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.ID;
            //}
            //else if (level.IsSpecial || level.IsExpert || level.IsFaculty)//专家、专科、特诊号扣排班限额
            //{
            //    this.regObj.DoctorInfo.Templet.Noon.ID = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.Noon.ID;
            //    this.regObj.DoctorInfo.Templet.IsAppend = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.IsAppend;
            //    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtBegin.Value.ToString("HH:mm:ss"));//挂号时间
            //    this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
            //    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //        this.dtEnd.Value.ToString("HH:mm:ss"));//结束时间
            //    this.regObj.DoctorInfo.Templet.ID = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID;

            //}
            //else//其他号不扣限额
            //{
            //    this.regObj.DoctorInfo.SeeDate = this.regMgr.GetDateTimeFromSysDateTime();
            //    this.regObj.DoctorInfo.Templet.Begin = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
            //            this.dtBegin.Value.ToString("HH:mm:ss"));
            //    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
            //            this.dtEnd.Value.ToString("HH:mm:ss"));

            //    ///如果挂号日期大于今天,为预约挂明日的号,更新挂号时间
            //    ///
            //    if (this.regObj.DoctorInfo.SeeDate.Date < this.dtBookingDate.Value.Date)
            //    {
            //        this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //            this.dtBegin.Value.ToString("HH:mm:ss"));//挂号时间
            //        this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
            //        this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            //            this.dtEnd.Value.ToString("HH:mm:ss"));//结束时间

            //        this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.Templet.Begin);
            //    }
            //    else
            //    {
            //        this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.SeeDate);
            //    }


            //    if (this.regObj.DoctorInfo.Templet.Noon.ID == "")
            //    {
            //        MessageBox.Show("未维护午别信息,请先维护!", "提示");
            //        return -1;
            //    }
            //    this.regObj.DoctorInfo.Templet.ID = "";
            //}
            #endregion

            //if (this.regObj.Pact.PayKind.ID == "03")//公费日限判断
            //{
            //    if (this.IsAllowPubReg(this.regObj.PID.CardNO, this.regObj.DoctorInfo.SeeDate) == -1)
            //        return -1;
            //}

            regObj.DoctorInfo.Templet.Noon.ID = "1";
            #region 挂号费
            int rtn = ConvertRegFeeToObject(regObj);
            if (rtn == -1)
            {
                MessageBox.Show("获取挂号费出错!" + this.regFeeMgr.Err, "提示");
                //this.cmbRegLevel.Focus();
                return -1;
            }
            if (rtn == 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号级别未维护挂号费,请先维护挂号费"), "提示");
                //this.cmbRegLevel.Focus();
                return -1;
            }

            //获得患者应收、报销
            ConvertCostToObject(regObj, out lstAccFee);

            #endregion

            //处方号
            //  this.regObj.InvoiceNO = this.txtRecipeNo.Text.Trim();
            this.regObj.RecipeNO = this.conMgr.GetConstansObj("RegRecipeNo", regMgr.Operator.ID).Name;//this.txtRecipeNo.Text.Trim();


            this.regObj.IsFee = false;
            this.regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            this.regObj.IsSee = false;
            this.regObj.InputOper.ID = this.regMgr.Operator.ID;
            this.regObj.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();
            //add by niuxinyuan
            this.regObj.DoctorInfo.SeeDate = this.regObj.InputOper.OperTime;
            //this.regObj.DoctorInfo.Templet.Noon.Name = this.QeryNoonName(this.regObj.DoctorInfo.Templet.Noon.ID);
            // add by niuxinyuan
            this.regObj.CancelOper.ID = "";
            this.regObj.CancelOper.OperTime = DateTime.MinValue;
            ArrayList al = new ArrayList();

            //if (this.chbEncrpt.Checked)
            //{
            //    this.regObj.IsEncrypt = true;
            //    this.regObj.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.regObj.Name);
            //    this.regObj.Name = "******";
            //}

            //this.regObj.IDCard = this.txtIdNO.Text;
            this.regObj.IsFee = true;
            return 0;
        }

        /// <summary>
        /// 挂号实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register regObj;

        /// <summary>
        /// 合同单位管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 更新医生或科室的看诊序号
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <param name="noonID"></param>
        /// <param name="regDate"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSeeID(string deptID, string doctID, string noonID, DateTime regDate,
            ref int seeNo, ref string Err)
        {
            string Type = "", Subject = "";

            #region ""

            if (doctID != null && doctID != "")
            {
                Type = "1";//医生
                Subject = doctID;
            }
            else
            {
                Type = "2";//科室
                Subject = deptID;
            }

            #endregion

            //更新看诊序号
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //获取看诊序号		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 更新全院看诊序号
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(FS.HISFC.BizLogic.Registration.Register rMgr, DateTime current, ref int seeNo,
            ref string Err)
        {
            //更新看诊序号
            //全院是全天大排序，所以午别不生效，默认 1
            if (rMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            //获取全院看诊序号
            if (rMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 系统参数控制
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParma = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 将应缴金额转为挂号实体,
        /// 属性不能作为ref参数传递 TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int ConvertRegFeeToObject(FS.HISFC.Models.Registration.Register obj)
        {
            decimal regFee = 0, chkFee = 0, digFee = 0, othFee = 0;

            int rtn = this.GetRegFee(obj.Pact.ID, obj.DoctorInfo.Templet.RegLevel.ID,
                          ref regFee, ref chkFee, ref digFee, ref othFee);

            obj.RegLvlFee.RegFee = regFee;
            obj.RegLvlFee.ChkFee = chkFee;
            obj.RegLvlFee.OwnDigFee = digFee;
            obj.RegLvlFee.OthFee = othFee;

            return rtn;
        }

        private int iFeeDiagReg = 3;

        /// <summary>
        /// 获取挂号费
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="regLvlID"></param>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <returns></returns>
        private int GetRegFee(string pactID, string regLvlID, ref decimal regFee, ref decimal chkFee, ref decimal digFee, ref decimal othFee)
        {
            FS.HISFC.Models.Registration.RegLvlFee p = this.regFeeMgr.Get(pactID, regLvlID);
            if (p == null)//出错
            {
                return -1;
            }
            if (p.ID == null || p.ID == "")//没有维护挂号费
            {
                return 1;
            }

            regFee = p.RegFee;
            chkFee = p.ChkFee;
            digFee = p.OwnDigFee;
            othFee = p.OthFee;

            //判断是否只收取挂号费
            switch (this.iFeeDiagReg)
            {
                case 1:
                    // 收取挂号费
                    chkFee = 0;
                    digFee = 0;
                    break;
                case 2:
                    // 收取诊金
                    regFee = 0;
                    break;
                case 3:
                    // 不收取诊金、挂号
                    regFee = 0;
                    chkFee = 0;
                    digFee = 0;
                    break;

                default:
                    // 默认都收取
                    break;

            }

            return 0;
        }

        /// <summary>
        /// 挂号费管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();

        /// <summary>
        /// 将应缴金额转为挂号实体,
        /// 属性不能作为ref参数传递 TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="lstCardFee"></param>
        private void ConvertCostToObject(FS.HISFC.Models.Registration.Register obj, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            decimal othFee = 0, ownCost = 0, pubCost = 0;
            othFee = obj.RegLvlFee.OthFee; //add by niux
            //lstCardFee = this.getCost(obj.RegLvlFee.RegFee, obj.RegLvlFee.ChkFee, obj.RegLvlFee.OwnDigFee,
            //        ref othFee, ref ownCost, ref pubCost, this.regObj.PID.CardNO);
            lstCardFee = new List<AccountCardFee>();

            AccountCardFee cardFee = new AccountCardFee();

            cardFee.FeeType = AccCardFeeType.RegFee;
            cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            cardFee.IStatus = 1;
            cardFee.Own_cost = ownCost;
            cardFee.Pub_cost = pubCost;
            cardFee.Tot_cost = ownCost + pubCost;
            lstCardFee.Add(cardFee);

            obj.RegLvlFee.OthFee = othFee;
            obj.OwnCost = ownCost;
            obj.PubCost = pubCost;

        }

        string registerLevel = "1";

        /// <summary>
        /// 默认挂号级别
        /// </summary>
        public string RegisterLevel
        {
            get
            {
                return registerLevel;
            }
            set
            {
                registerLevel = value;
            }
        }

        string registerDept = "2001";

        /// <summary>
        /// 默认挂号科室
        /// </summary>
        public string RegisterDept
        {
            get
            {
                return registerDept;
            }
            set
            {
                registerDept = value;
            }
        }

        /// <summary>
        /// 打印挂号发票
        /// </summary>
        /// <param name="regObj"></param>
        private void PrintReg(FS.HISFC.Models.Registration.Register regObj, FS.HISFC.BizLogic.Registration.Register regmr)
        {
            #region 屏蔽
            /*if( this.PrintWhat == "Invoice")//打印发票
            {
                this.ucInvoice.Registeration = regObj ;
			
                System.Drawing.Printing.PaperSize size ;

                if( PrintCnt % 2 == 0)
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice1", 425 ,288);
                }
                else
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice2",425,280) ;
                }

                PrintCnt ++ ;

                printer.SetPageSize(size);
                printer.PrintPage(0,0,ucInvoice) ;
            }
            else//打印处方
            {
                //fuck
                FS.neuFC.Object.neuObject obj = this.conMgr.Get("PrintRecipe",regObj.RegDept.ID) ;

                //不包含的，都打印
                if( obj == null || obj.ID == "")
                {
                    this.ucBill.Register = regObj ;
					
                    System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("Recipe", 670 ,1120);
                    printer.SetPageSize(size);
                    printer.PrintPage(0,0,this.ucBill) ;
                }
            }*/
            #endregion
            #region by niuxy
            /*
            try
            {
                if (IRegPrint != null)
                {
                    this.IRegPrint.RegInfo = regObj;
                    this.IRegPrint.Print();
                }
            }
            catch { }
             */
            #endregion
            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;

            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            if (regprint == null)
            {
                MessageBox.Show(FS.FrameWork.WinForms.Classes.UtilInterface.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //regprint.SetPrintValue(regObj,regmr);
            if (regObj.IsEncrypt)
            {
                regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.regObj.NormalName);
            }

            regprint.SetPrintValue(regObj);
            int i = regprint.Print();
            //regprint.PrintView();

        }

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet != this.spPatient) return;
            if (this.spPatient.ActiveRow.Tag == null) return;

            AccountCard tempCard = this.spPatient.ActiveRow.Tag as AccountCard;
            if (tempCard.Patient == null)
            {
                MessageBox.Show("查询患者信息失败！");
                return;
            }
            this.ucRegPatientInfo1.CardNO = tempCard.Patient.PID.CardNO;
            this.txtMarkNO.Focus();
        }

        //{8AC26BF8-C7F9-4d96-8984-FCEA5B6F4D51}
        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            if (this.neuSpread1.ActiveSheet != spPatient )
            {
                return;
            }

            if (this.spPatient.ActiveRow == null || this.spPatient.ActiveRow.Tag == null)
            {
                return;
            }  

            AccountCard tempAccountCard = new AccountCard();
            tempAccountCard = this.spPatient.ActiveRow.Tag as AccountCard;


            this.menu.Show(neuSpread1 as Control, new Point(e.X, e.Y));
        }
    }
}
