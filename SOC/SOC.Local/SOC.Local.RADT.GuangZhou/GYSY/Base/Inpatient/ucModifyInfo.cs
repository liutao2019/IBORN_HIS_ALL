using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Base;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.SOC.Local.RADT.GuangZhou.GYSY.Base.Inpatient
{
    /// <summary>
    /// 患者信息修改界面
    /// </summary>
    public partial class ucModifyInfo : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.RADT.Interface.Register.IInpatient
    {
        public ucModifyInfo()
        {
            InitializeComponent();
            this.cmbInSource.TextChanged += new EventHandler(cmbInSource_TextChanged);
            this.dtpBirthDay.TextChanged += new EventHandler(dtpBirthDay_TextChanged);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            this.Click += new EventHandler(c_Click);
            this.plInfomation.Click += new EventHandler(c_Click);
            this.dtpBirthDay.KeyDown += new KeyEventHandler(dtpBirthDay_KeyDown);
            this.rbtnTempPatientNO.CheckedChanged += new EventHandler(rbtnTempPatientNO_CheckedChanged);
            this.txtIDNO.KeyDown += new KeyEventHandler(txtIDNO_KeyDown);
        }


        #region 成员变量

        /// <summary>
        /// 管理类
        /// </summary>
        private CommonController commonController = CommonController.CreateInstance();

        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctlParamManage = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private bool isArriveProcess = false;

        private bool isCanModifyInTime = false;

        private string fileName = "";

        private bool isAutoPatientNO = false;

        private string autoPatientNO = "";

        private FS.HISFC.Models.RADT.PatientInfo patientInfoOld = null;

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void initCombo()
        {
            //性别列表
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            this.cmbSex.Text = "";
            this.cmbSex.Tag = "";

            //国籍
            this.cmbCountry.AddItems(commonController.QueryConstant(EnumConstant.COUNTRY));
            //婚姻状态
            this.cmbMarry.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());
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
            this.cmbPayMode.AddItems(commonController.QueryConstant(FS.HISFC.Models.Base.EnumConstant.PAYMODES));

            //住院次数
            this.mTxtIntimes.Text = "1";
            //支付方式
            this.cmbPayMode.Tag = "CA";
            //入院日期
            this.dtpInTime.Value = commonController.GetSystemTime(); //入院日期
            //生日
            this.dtpBirthDay.Text = string.Empty;

            //超标处理
            ArrayList alLimit = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject O = new FS.FrameWork.Models.NeuObject();
            O.ID = "0";
            O.Name = "超标不限";
            alLimit.Add(O);

            obj.ID = "1";
            obj.Name = "超标自理";
            alLimit.Add(obj);

            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "2";
            obj1.Name = "超标不计";
            alLimit.Add(obj1);
            this.cmbBedOverDeal.AddItems(alLimit);
            this.cmbBedOverDeal.IsListOnly = true;

            //InputLanguageCollection ilc = InputLanguage.InstalledInputLanguages;
            //ArrayList alillist = new ArrayList();
            //foreach (InputLanguage alil in ilc)
            //{
            //    FS.FrameWork.Models.NeuObject objil = new FS.FrameWork.Models.NeuObject();
            //    objil.ID = alil.LayoutName;
            //    objil.Name = alil.LayoutName;

            //    alillist.Add(objil);
            //}
            //this.comboBox1.AddItems(alillist);      //登记类型
            //comboBox1.SelectedIndex = InputLanguage.InstalledInputLanguages.IndexOf(InputLanguage.CurrentInputLanguage);

            ArrayList alOverLop = new ArrayList();
            FS.FrameWork.Models.NeuObject objOverLop1 = new FS.FrameWork.Models.NeuObject();
            objOverLop1.ID = "0";
            objOverLop1.Name = "不同意超标";
            alOverLop.Add(objOverLop1);
            FS.FrameWork.Models.NeuObject objOverLop2 = new FS.FrameWork.Models.NeuObject();
            objOverLop2.ID = "1";
            objOverLop2.Name = "同意超标";
            alOverLop.Add(objOverLop2);
            this.cmbOverLop.AddItems(alOverLop);
            this.cmbOverLop.IsListOnly = true;

        }

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

        /// <summary>
        /// 设置患者信息到控件
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="isAll">是否显示全部信息，Add时只显示部分信息</param>
        private void setPatientInfo(PatientInfo patient)
        {
            this.patientInfoOld = patient;

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

            this.txtDayLimit.Text = patient.FT.DayLimitCost.ToString();         //公费日限
            this.txtBedLimit.Text = patient.FT.BedLimitCost.ToString();          //普通标准
            this.txtAirLimit.Text = patient.FT.AirLimitCost.ToString();      //监护床
            this.cmbBedOverDeal.SelectedIndex = ctlParamManage.GetControlParam<int>("Fee001");     //超标处理
            this.cmbOverLop.SelectedIndex = ctlParamManage.GetControlParam<int>("Fee002");        //日限处理                    
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="patient"></param>
        private void setPatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //如果是已出院状态则不给修改
            if (patient.PVisit.InState.ID.ToString() == "O")
            {
                this.plInfomation.Enabled = false;
            }
            else
            {
                this.plInfomation.Enabled = true;
            }

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
                patient.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patient.NormalName);
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
        }

        /// <summary>
        /// 获得控件输入的信息,合成患者基本信息实体
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns> 成功: 1 失败 : -1</returns>
        private PatientInfo getPatientInfomation()
        {
            FS.HISFC.Models.RADT.PatientInfo patient = this.patientInfoOld;
            if (patient == null)
            {
                patient = new PatientInfo();
            }

            patient.PID.PatientNO = this.txtInpatientNO.Text; //住院号
            patient.PID.CardNO = this.txtClinicNO.Text;//门诊卡号
            patient.ID = this.txtInpatientNO.Tag.ToString();//住院流水号
            patient.SSN = this.txtMCardNO.Text;//医保号
            patient.ProCreateNO = this.txtComputerNO.Text;//生育保险电脑号
            if (this.isCanModifyInTime)
            {
                patient.PVisit.InTime = this.dtpInTime.Value;//入院日期
            }

            patient.Pact.ID = this.cmbPact.Tag.ToString();//合同单位编码
            patient.Pact.Name = this.cmbPact.Text;//合同单位名称
            patient.Pact.PayKind = this.commonController.GetPayKind(patient.Pact.ID);//结算类别
            if (patient.Pact.PayKind == null)
            {
                this.myMessageBox("获取结算类别错误！", MessageBoxIcon.Warning);
                return null;
            }

            patient.Name = this.txtName.Text;//名字
            patient.Sex.ID = this.cmbSex.Tag.ToString();//性别
            if (this.cmbNation.SelectedItem != null)
            {
                patient.Nationality = this.cmbNation.SelectedItem;//民族
            }
            patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.dtpBirthDay.Text);//生日

            patient.CompanyName = this.txtWorkAddress.Text;//工作单位
            patient.MaritalStatus.ID = this.cmbMarry.Tag.ToString();//婚姻状况
            patient.DIST = this.cmbBirthArea.Text;//籍贯
            patient.Country.ID = this.cmbCountry.Tag.ToString();//国籍ID
            patient.Country.Name = this.cmbCountry.Text;//国籍
            patient.Profession.ID = this.cmbProfession.Tag.ToString();//职位ID
            patient.Profession.Name = this.cmbProfession.Text;//职位名称
            patient.Kin.Name = this.txtLinkMan.Text;//联系人姓名
            patient.Kin.RelationPhone = this.txtLinkPhone.Text;//联系人备注-电话
            patient.Kin.Relation.ID = this.cmbRelation.Tag.ToString();//与患者关系编码
            patient.Kin.Relation.Name = this.cmbRelation.Text;//与患者关系
            patient.Kin.RelationAddress = this.txtLinkAddr.Text;//联系人地址
            patient.AddressHome = this.txtHomeAddress.Text;//家庭地址
            patient.AreaCode = this.txtBirthArea.Text;//出生地
            patient.HomeZip = this.txtHomeZip.Text;//家庭地址邮编
            patient.AddressBusiness = this.txtAddressNow.Text; //现住地址
            patient.AddressBusiness = this.txtAddressNow.Text; //现住地址 (PS:5.0没有现住地址这字段~而且有些属性patient和patientInfo居然参杂交错)
            patient.PhoneHome = this.txtHomePhone.Text;//患者电话
            patient.PhoneBusiness = this.txtWorkPhone.Text;//单位电话 
            patient.IDCard = this.txtIDNO.Text;//身份证
            patient.PVisit.AdmitSource.ID = this.cmbApproach.Tag.ToString();//入院途径
            patient.PVisit.AdmitSource.Name = this.cmbApproach.Text;//入院途径
            patient.PVisit.InSource.ID = this.cmbInSource.Tag.ToString();//入院来源
            patient.PVisit.InSource.Name = this.cmbInSource.Text;//入院来源
            patient.PVisit.Circs.ID = this.cmbCircs.Tag.ToString();//入院情况
            patient.PVisit.Circs.Name = this.cmbCircs.Text;//入院情况
            patient.ClinicDiagnose = this.txtDiagnose.Text;//门诊诊断
            patient.FT.BloodLateFeeCost = FS.FrameWork.Function.NConvert.ToDecimal(this.mtxtBloodFee.Text);//血滞纳金
            patient.InTimes = NConvert.ToInt32(this.mTxtIntimes.Text);//住院次数
            patient.FT.LeftCost = NConvert.ToDecimal(this.mTxtPrepay.Text);//预交金
            patient.FT.PrepayCost = NConvert.ToDecimal(this.mTxtPrepay.Text);//预交金
            patient.FT.FixFeeInterval = 1;//默认为1

            //加密
            patient.IsEncrypt = this.chbencrypt.Checked;

            if (patient.IsEncrypt)
            {

                patient.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(patient.Name);
                patient.Name = "******";
            }
            patient.FT.DayLimitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDayLimit.Text);     //公费日限
            patient.FT.BedLimitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtBedLimit.Text);          //普通标准
            patient.FT.AirLimitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtAirLimit.Text);      //监护床
            //超标处理与日限处理在控制参数表COM_CONTROLARGUMENT中，索引为：Fee001和Fee002。广四这里暂时直接写死，不做更改处理。
            if (this.txtIDNO.Text == "-")
            {
                patient.IDCard = "";      //身份证
            }
            if (this.cmbMarry.Text == "-")
            {
                patient.MaritalStatus.ID = "";    //婚姻状况
            }
            if (this.txtLinkMan.Text == "-")
            {
                patient.Kin.Name = "";  //联系人
            }
            return patient;
        }

        /// <summary>
        /// 验证输入的信息是否合法
        /// </summary>
        /// <returns>成功: true 失败: null</returns>
        private bool isInputValid()
        {
            if (string.IsNullOrEmpty(this.txtInpatientNO.Text) == false)
            {
                string patientNO = this.txtInpatientNO.Text.Trim().PadLeft(10, '0');
                this.txtInpatientNO.Text = patientNO;
                this.txtInpatientNO.Tag = "T001";
                //如果住院次数大于1
                if (FS.FrameWork.Function.NConvert.ToInt32(this.mTxtIntimes.Text) <= 1)
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

            if (!this.txtInpatientNO.Text.Equals(autoPatientNO))
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();
                if (Function.GetInputPatientNO(this.txtInpatientNO.Text, ref patientInfo) < 1)
                {
                    if (string.IsNullOrEmpty(this.autoPatientNO) == false)
                    {
                        this.myMessageBox(Function.Err, MessageBoxIcon.Information);
                        return false;
                    }
                }
                //如果住院号相同，则不获取患者信息
                if (string.IsNullOrEmpty(patientInfo.ID) == false && patientInfo.PatientNOType == EnumPatientNOType.Second && patientInfo.PID.PatientNO.Equals(this.autoPatientNO) == false)
                {
                    this.SetPatientInfo(patientInfo, true);
                    return false;
                }
            }

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
                if (c is FS.SOC.HISFC.RADT.Interface.Common.IInputControl)
                {
                    if (!((FS.SOC.HISFC.RADT.Interface.Common.IInputControl)c).IsValidInput())
                    {
                        this.myMessageBox(((FS.SOC.HISFC.RADT.Interface.Common.IInputControl)c).InputMsg, MessageBoxIcon.Warning);
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
            DateTime birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.dtpBirthDay.Text);
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
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text, 20))
            {
                this.myMessageBox("姓名录入超长！", MessageBoxIcon.Warning);
                this.txtName.Select();
                return false;
            }

            //判断字符超长籍贯
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbBirthArea.Text, 50))
            {
                this.myMessageBox("籍贯录入超长！", MessageBoxIcon.Warning);
                this.cmbBirthArea.Select();
                return false;
            }

            //判断字符超长联系人
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkMan.Text, 12))
            {
                this.myMessageBox("联系人录入超长！", MessageBoxIcon.Warning);
                this.txtLinkMan.Select();
                return false;
            }

            //判断字符超长工作单位
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWorkAddress.Text, 50))
            {
                this.myMessageBox("工作单位录入超长！", MessageBoxIcon.Warning);
                this.txtWorkAddress.Select();
                return false;
            }

            //判断字符超长联系人电话
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkPhone.Text, 30))
            {
                this.myMessageBox("联系人电话录入超长！", MessageBoxIcon.Warning);
                this.txtLinkPhone.Select();

                return false;
            }
            //家庭电话
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomePhone.Text, 30))
            {
                this.myMessageBox("家庭电话录入超长！", MessageBoxIcon.Warning);
                this.txtHomePhone.Select();

                return false;
            }
            //单位电话
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWorkPhone.Text, 30))
            {
                this.myMessageBox("单位电话录入超长！", MessageBoxIcon.Warning);
                this.txtWorkPhone.Select();

                return false;
            }

            //诊断
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDiagnose.Text, 50))
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

            //校验身份证号和生日 {9B24289B-D017-4356-8A25-B0F76EB79D15}

            int returnValue = this.processIDENNO(this.txtIDNO.Text.Trim(), EnumCheckIDNOType.Saveing);

            if (returnValue < 0)
            {
                return false;
            }


            //门诊号长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtClinicNO.Text, 10))
            {
                this.myMessageBox("门诊号过长,请重新输入!", MessageBoxIcon.Warning);
                this.txtClinicNO.Select();

                return false;
            }
            //电脑号
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtComputerNO.Text, 20))
            {
                this.myMessageBox("电脑号过长,请重新输入!", MessageBoxIcon.Warning);
                this.txtComputerNO.Select();

                return false;
            }

            //医疗证号长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtMCardNO.Text, 20))
            {
                this.myMessageBox("医疗证号过长,请重新输入!", MessageBoxIcon.Warning);
                this.txtMCardNO.Select();
                return false;
            }





            //科室
            if (this.cmbDept.Tag == null || this.cmbDept.Text.Trim() == string.Empty)
            {
                this.myMessageBox("科室不能为空，请重新输入！", MessageBoxIcon.Warning);
                this.cmbDept.Select();
                return false;
            }

            ////科室长度
            //if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbDept.Text, 16))
            //{
            //    this.myMessageBox("科室输入过长,请重新输入!", MessageBoxIcon.Warning);
            //    this.cmbDept.Select();

            //    return false;
            //}

            if (this.isArriveProcess)
            {
                if (this.cmbBedNO.Tag.ToString() == string.Empty)
                {
                    this.myMessageBox("病床不能为空，请选择病床！", MessageBoxIcon.Warning);
                    this.cmbBedNO.Select();
                    return false;
                }
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
                year = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, ageIndex));
            }
            if (ageIndex >= 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, monthIndex - ageIndex - 1));
            }
            if (ageIndex < 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, monthIndex));//只有月
            }
            if (monthIndex >= 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(monthIndex + 1, dayIndex - monthIndex - 1));
            }
            if (ageIndex < 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, dayIndex));//只有日
            }
            if (ageIndex >= 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, dayIndex - ageIndex - 1));//只有年，日
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

            if (string.IsNullOrEmpty(idNO)) //为空的不处理
            {
                return 1;
            }
            if (idNO == "-") //特殊符号处理
            {
                return 1;
            }
            //校验身份证号

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
                this.myMessageBox(errText, MessageBoxIcon.Error);
                this.txtIDNO.Focus();
                return -1;
            }
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
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();
                patientInfo = this.GetPatientInfo(patientInfo);
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
                        this.cmbSex.SelectAll();
                        this.cmbSex.Focus();
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
            DateTime birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.dtpBirthDay.Text);

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
                    dtpBirthDay.Select();
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
        /// 选择临时号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rbtnTempPatientNO_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnTempPatientNO.Checked)
            {
                string tempPatientNO = string.Empty;
                if (Function.GetAutoTempPatientNO(ref tempPatientNO) > 0)
                {
                    this.txtInpatientNO.Text = tempPatientNO;
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

            //是否允许修改住院号
            this.dtpInTime.Enabled = this.isCanModifyInTime;
            this.foucs();
            return 1;
        }

        public void Clear()
        {
            patientInfoOld = null;
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
            this.cmbNurseCell.Tag = "";
            this.cmbNurseCell.Text = "";
            txtLinkPhone.Text = "";
            this.cmbBirthArea.Text = string.Empty;
            this.cmbBirthArea.Tag = "";
            this.cmbCountry.Text = string.Empty;
            this.cmbCountry.Tag = "";
            this.cmbProfession.Text = string.Empty;
            this.cmbProfession.Tag = "";
            this.txtHomeZip.Text = string.Empty;
            this.txtLinkMan.Text = string.Empty;
            this.cmbRelation.Text = string.Empty;
            this.cmbRelation.Tag = "";
            this.txtLinkAddr.Text = string.Empty;
            this.txtWorkPhone.Text = string.Empty;
            this.txtHomePhone.Text = string.Empty;
            this.cmbDoctor.Text = string.Empty;
            this.mTxtPrepay.Text = "0.00";
            this.mTxtIntimes.Text = "1";
            this.cmbPayMode.Tag = "CA";
            this.cmbPayMode.Text = "现金";

            this.cmbSex.Tag = "";
            this.cmbSex.Text = "";

            this.cmbInSource.Tag = "";
            this.cmbInSource.Text = "";

            this.cmbCircs.Tag = "";
            this.cmbCircs.Text = "";

            this.cmbApproach.Text = "";
            this.cmbApproach.Tag = "";

            this.cmbNation.Tag = "";
            this.cmbNation.Text = "";

            this.cmbOldDept.EnterVisiable = true;
            this.cmbOldDept.Text = "";
            this.cmbOldDept.Tag = "";

            this.cmbPayMode.Tag = "";
            this.cmbPayMode.Text = "";

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

            this.txtDayLimit.Text = "0.00";
            this.txtBedLimit.Text = "0.00";
            this.txtAirLimit.Text = "0.00";

            this.foucs();


        }

        public void ModifyPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.SetPatientInfo(patientInfo, true);
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

        public event FS.SOC.HISFC.RADT.Interface.Register.SelectPatientInfo OnQueryPatientInfo;

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
            return getPatientInfomation();
        }

        public void SetPatientInfo(PatientInfo patientInfo, bool isAll)
        {
            this.setPatientInfo(patientInfo);
            this.foucs();
        }

        public FS.HISFC.Models.Fee.Inpatient.Prepay GetPrepay()
        {
            //预交金实体
            FS.HISFC.Models.Fee.Inpatient.Prepay prepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
            prepay.PayType.ID = this.cmbPayMode.Tag.ToString();
            prepay.PayType.Name = this.cmbPayMode.Text;
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

        //{014680EC-6381-408b-98FB-A549DAA49B82}
        public string CardNo { get; set; }

        public bool IsCanModifyPatientNo { get; set; }

        public bool IsCanModifyIName { get; set; }

        #endregion


        #region IInpatient 成员


        public bool IsInputDiagnose
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsInputLinkMan
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsShowPrePay
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
