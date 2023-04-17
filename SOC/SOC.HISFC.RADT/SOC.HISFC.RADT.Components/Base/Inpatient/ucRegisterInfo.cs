﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
using System.Xml;
using FS.FrameWork.Function;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.RADT;
using FS.SOC.HISFC.RADT.Interface;
using System.IO;
using FS.SOC.HISFC.RADT.Components.Interface;

namespace FS.SOC.HISFC.RADT.Components.Base.Inpatient
{
    /// <summary>
    /// 入院登记界面
    /// </summary>
    public partial class ucRegisterInfo : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.RADT.Interface.Register.IInpatient
    {
        public ucRegisterInfo()
        {
            InitializeComponent();
            this.cmbDept.TextChanged += new EventHandler(cmbDept_TextChanged);
            this.cmbDoctor.TextChanged += new EventHandler(cmbDoctor_TextChanged);
            this.cmbInSource.TextChanged+=new EventHandler(cmbInSource_TextChanged);
            this.dtpBirthDay.TextChanged+=new EventHandler(dtpBirthDay_TextChanged);
            this.txtAge.TextChanged+=new EventHandler(txtAge_TextChanged);
            this.Click+=new EventHandler(c_Click);
            this.plInfomation.Click+=new EventHandler(c_Click);
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

        private bool isArriveProcess = false;

        private bool isCanModifyInTime = false;

        private string fileName = "";

        private string deptCode = "";
        private string nurseCode = "";

        private bool isAutoPatientNO = false;

        private string autoPatientNO = "";

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
        private void setPatient(FS.HISFC.Models.RADT.Patient patient)
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
        private PatientInfo getPatientInfomation(PatientInfo patient)
        {
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
                FS.HISFC.Models.Base.Bed bedObj = this.cmbBedNO.SelectedItem as FS.HISFC.Models.Base.Bed;
                patient.PVisit.PatientLocation.NurseCell = bedObj.NurseStation;
                patient.PVisit.PatientLocation.Bed = bedObj;
                patient.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.I;
            }
            else
            {
                patient.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.R;
            }

            patient.Name = this.txtName.Text;//名字
            patient.Sex.ID = this.cmbSex.Tag.ToString();//性别
            if (this.cmbNation.SelectedItem != null)
            {
                patient.Nationality = this.cmbNation.SelectedItem;//民族
            }
            patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.dtpBirthDay.Text);//生日
            patient.PVisit.PatientLocation.Dept.ID = this.cmbDept.Tag.ToString();//科室编码
            patient.PVisit.PatientLocation.Dept.Name = this.commonController.GetDepartmentName(patient.PVisit.PatientLocation.Dept.ID);//科室名称

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
            patient.HomeZip = this.txtHomeZip.Text;//家庭地址邮编
            patient.AddressBusiness = this.txtAddressNow.Text;//单位地址
            patient.PhoneHome = this.txtHomePhone.Text;//患者电话
            patient.PhoneBusiness = this.txtWorkPhone.Text;//单位电话 
            patient.IDCard = this.txtIDNO.Text;//身份证
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

            patient.FT.BloodLateFeeCost = FS.FrameWork.Function.NConvert.ToDecimal(this.mtxtBloodFee.Text);//血滞纳金
            //路志鹏 修改住院次数 目的：本次住院登记的住院次数应该是上一次住院次数加1
            patient.InTimes = NConvert.ToInt32(this.mTxtIntimes.Text);//住院次数？初次就增加啦
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
                    string patientNO = Function.GetPatientNO(this.txtInpatientNO.Text);
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
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();
            if (Function.GetInputPatientNO(this.txtInpatientNO.Text, ref patientInfo) < 1)
            {
                this.myMessageBox(Function.Err, MessageBoxIcon.Information);
                return false;
            }
            //如果住院号相同，则不获取患者信息

            if (string.IsNullOrEmpty(patientInfo.ID) == false && patientInfo.PatientNOType == EnumPatientNOType.First)
            {
                if (this.isAutoPatientNO&&this.autoPatientNO != this.txtInpatientNO.Text)
                {
                    this.myMessageBox("自动生成的住院号不允许修改,请重新输入!", MessageBoxIcon.Warning);
                    this.txtInpatientNO.Text = this.autoPatientNO;
                    this.txtInpatientNO.Select();
                    return false;
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
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtMCardNO.Text, 18))
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

            //科室长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbDept.Text, 16))
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

           birthDay= commonController.GetBirthday(iyear, iMonth, iDay);

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

            //校验身份证号

            string idNOTmp = string.Empty;
            if (idNO.Length == 15)
            {
                idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            else if(idNO.Length==18)
            {
                idNOTmp = idNO;
            }
            else if (idNO.Length == 10 || idNO.Length == 11)
            {
                if (CheckHKID(idNO) == false)
                {
                    this.myMessageBox("请输入有效的香港居民身份证", MessageBoxIcon.Error);
                    this.txtIDNO.Focus();
                    return -1;
                }
                else
                {
                    return 1;
                }
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
            if(string.IsNullOrEmpty(this.txtName.Text))
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
                this.cmbSex.Enabled = eable;
                this.cmbSex.EnterVisiable = eable;

                this.cmbBirthArea.Enabled = eable;
                this.cmbBirthArea.EnterVisiable = eable;

                this.cmbCountry.Enabled = eable;
                this.cmbCountry.EnterVisiable = eable;
                this.cmbMarry.Enabled = eable;
                this.cmbMarry.EnterVisiable = eable;
                this.cmbProfession.Enabled = eable;
                this.cmbProfession.EnterVisiable = eable;
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
                this.cmbPact.EnterVisiable = eable;
                this.cmbPayMode.Enabled = eable;
                this.cmbPayMode.EnterVisiable = eable;
            }
            else
            {
                this.cmbSex.EnterVisiable = eable;
                this.cmbSex.Enabled = eable;
                this.cmbBirthArea.EnterVisiable = eable;
                this.cmbBirthArea.Enabled = eable;
                this.cmbCountry.EnterVisiable = eable;
                this.cmbCountry.Enabled = eable;
                this.cmbMarry.EnterVisiable = eable;
                this.cmbMarry.Enabled = eable;
                this.cmbProfession.EnterVisiable = eable;
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
                this.cmbPact.EnterVisiable = eable;
                this.cmbPact.Enabled = eable;
                this.cmbPayMode.EnterVisiable = eable;
                this.cmbPayMode.Enabled = eable;
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

        #region 香港身份证

        /// <summary>
        /// 获取字符的正数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private  int GetAppNumber(char str)
        {
            /*
             * 
             *        空格 58 I 18 R 27
                        A 10 J 19 S 28
                        B 11 K 20 T 29
                        C 12 L 21 U 30
                        D 13 M 22 V 31
                        E 14 N 23 W 32
                        F 15 O 24 X 33
                        G 16 P 25 Y 34
                        H 17 Q 26 Z 35

             **/
            switch (str)
            {
                case '@':
                case ' ':
                    return 58;
                case 'A':
                case 'a':
                    return 10;
                case 'B':
                case 'b':
                    return 11;
                case 'C':
                case 'c':
                    return 12;
                case 'D':
                case 'd':
                    return 13;
                case 'E':
                case 'e':
                    return 14;
                case 'F':
                case 'f':
                    return 15;
                case 'G':
                case 'g':
                    return 16;
                case 'H':
                case 'h':
                    return 17;
                case 'I':
                case 'i':
                    return 18;
                case 'J':
                case 'j':
                    return 19;
                case 'K':
                case 'k':
                    return 20;
                case 'L':
                case 'l':
                    return 21;
                case 'M':
                case 'm':
                    return 22;
                case 'N':
                case 'n':
                    return 23;
                case 'O':
                case 'o':
                    return 24;
                case 'P':
                case 'p':
                    return 25;
                case 'Q':
                case 'q':
                    return 26;
                case 'R':
                case 'r':
                    return 27;
                case 'S':
                case 's':
                    return 28;
                case 'T':
                case 't':
                    return 29;
                case 'U':
                case 'u':
                    return 30;
                case 'V':
                case 'v':
                    return 31;
                case 'W':
                case 'w':
                    return 32;
                case 'X':
                case 'x':
                    return 33;
                case 'Y':
                case 'y':
                    return 34;
                case 'Z':
                case 'z':
                    return 35;
                default:
                    return 58;

            }
        }

        /// <summary>
        /// 验证香港身份证
        /// </summary>
        /// <param name="CardNumber"></param>
        /// <returns></returns>
        private  bool CheckHKID(string CardNumber)
        {
            if (string.IsNullOrEmpty(CardNumber))
            {
                return false;
            }

            string regex = @"^[a-zA-Z]{1,2}\d{6}\([0-9a-zAZ-Z]\)$";
            if (System.Text.RegularExpressions.Regex.IsMatch(CardNumber, regex))
            {
                if (CardNumber.Length == 10)
                {
                    int sum = 9 * GetAppNumber('@') +
                                    8 * GetAppNumber(CardNumber[0]) +
                                    7 * int.Parse(CardNumber[1].ToString()) +
                                    6 * int.Parse(CardNumber[2].ToString()) +
                                    5 * int.Parse(CardNumber[3].ToString()) +
                                    4 * int.Parse(CardNumber[4].ToString()) +
                                    3 * int.Parse(CardNumber[5].ToString()) +
                                    2 * int.Parse(CardNumber[6].ToString());

                    int checkdigit = 11 - sum % 11;
                    char checkdigitstr = ' ';
                    if (checkdigit == 10)
                    {
                        checkdigitstr = 'A';
                    }
                    if (checkdigit == 11)
                    {
                        checkdigitstr = '0';
                    }

                    if (checkdigitstr == CardNumber[8])
                    {
                        return true;
                    }
                }
                else if (CardNumber.Length == 11)
                {
                    int sum = 9 * GetAppNumber(CardNumber[0]) +
                                    8 * GetAppNumber(CardNumber[1]) +
                                    7 * int.Parse(CardNumber[2].ToString()) +
                                    6 * int.Parse(CardNumber[3].ToString()) +
                                    5 * int.Parse(CardNumber[4].ToString()) +
                                    4 * int.Parse(CardNumber[5].ToString()) +
                                    3 * int.Parse(CardNumber[6].ToString()) +
                                    2 * int.Parse(CardNumber[7].ToString());

                    int checkdigit = 11 - sum % 11;
                    char checkdigitstr = ' ';
                    if (checkdigit == 10)
                    {
                        checkdigitstr = 'A';
                    }
                    if (checkdigit == 11)
                    {
                        checkdigitstr = '0';
                    }

                    if (checkdigitstr == CardNumber[9])
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        #endregion
        
        #endregion

        #region 自动刷新

        /// <summary>
        /// 自动刷新开始时间
        /// </summary>
        private DateTime autoRefreshBeginTime = DateTime.Now;

        /// <summary>
        /// 终端设置的刷新间隔
        /// </summary>
        private int refreshInterval = 2;

        private System.Threading.Timer autoRefreshTimer = null;
        private System.Threading.TimerCallback autoRefreshCallBack = null;
        private delegate void autoRefreshHandler();
        private autoRefreshHandler autoRefreshEven;

        private void BeginAutoRefresh()
        {
            if (this.autoRefreshCallBack == null)
            {
                this.autoRefreshCallBack = new System.Threading.TimerCallback(this.AutoRefreshTimerCallback);
            }
            this.autoRefreshTimer = new System.Threading.Timer(this.autoRefreshCallBack, null, refreshInterval * 1000, this.refreshInterval * 1000);
        }

        /// <summary>
        /// 刷新处方列表
        /// </summary>
        /// <param name="param">参数（没有使用）</param>
        /// <returns></returns>
        private void AutoRefreshTimerCallback(object param)
        {
            if (this.autoRefreshEven == null)
            {
                autoRefreshEven = new autoRefreshHandler(this.AutoRefresh);
            }

            if (this.ParentForm != null)
            {
                this.ParentForm.BeginInvoke(this.autoRefreshEven);
            }

        }

        /// <summary>
        /// 自动刷新
        /// </summary>
        public void AutoRefresh()
        {
            bool isCloseRefresh = false;
            try
            {
                if (this.autoRefreshTimer != null)
                {
                    this.autoRefreshTimer.Dispose();
                }

                if (InterfaceManager.GetIReadIDCard() != null)
                {
                    string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
                    DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
                    string photoFileName = "";
                    int rtn = InterfaceManager.GetIReadIDCard().GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
                    if (rtn == -1)
                    {
                        isCloseRefresh = true;
                        return;
                    }
                    else if (rtn == 0)
                    {
                        return;
                    }

                    this.Clear();

                    this.txtName.Text = name;
                    this.txtIDNO.Text = code;
                    this.cmbSex.Text = sex;
                    this.cmbNation.Text = nation;
                    this.cmbCountry.Text = "中国";
                    this.txtHomeAddress.Text = add;
                    this.processIDENNO(code, EnumCheckIDNOType.BeforeSave);

                    int i = this.queryPatientInfo();
                    if (i < 0)
                    {
                        return;
                    }
                    else if (i == 1)//说明找到患者信息且是有效的
                    {
                        this.cmbInSource.SelectAll();
                        this.cmbInSource.Focus();
                    }

                    this.txtName.SelectAll();
                    this.txtName.Focus();

                }
                else
                {
                    isCloseRefresh = true;
                }
            }
            finally
            {
                if (isCloseRefresh==false)
                {
                    this.BeginAutoRefresh();
                }
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
                    if (i <0)
                    {
                        return true;
                    }
                    else if (i== 1)//说明找到患者信息且是有效的
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

            FS.FrameWork.Models.NeuObject deptObj = this.commonController.GetDepartment(deptCode);

            if (deptObj == null)
                return;

            //查找对应的护士站
            ArrayList al = Function.QueryNurseByDept(deptObj.ID);
            if (al != null)
            {
                this.cmbNurseCell.Clear();
                this.cmbNurseCell.AddItems(al);
            }

        }

        private void cmbNurseCell_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbNurseCell.Tag == null||string.IsNullOrEmpty(this.cmbNurseCell.Text))
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
                    this.cmbBedNO.SelectedItem = alBed[0] as FS.FrameWork.Models.NeuObject;
                }
            }
        }

        private void cmbDoctor_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbDoctor.Tag == null)
            {
                return;
            }
            FS.FrameWork.Models.NeuObject employee = this.commonController.GetEmployee(this.cmbDoctor.Tag.ToString());
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
            this.txtAge.Text = this.getAge(iyear, iMonth, iDay) ;
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
        }

        void dtpBirthDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (!dtpBirthDay.IsValidInput())
                {
                    this.myMessageBox(dtpBirthDay.InputMsg, MessageBoxIcon.Warning);
                    dtpBirthDay.Clear();
                    dtpBirthDay.SelectionStart = 0;
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

        private  void c_Click(object sender, EventArgs e)
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
                FS.HISFC.Models.RADT.Patient patient = Function.GetPatient(Function.GetCardNO(clinicNO));
                if (patient == null)
                {
                    this.myMessageBox("获得患者基本信息出错!" + Function.Err, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrEmpty(patient.PID.CardNO))
                {
                    this.myMessageBox("没有此患者的信息，请重新输入!" , MessageBoxIcon.Information);
                    return;
                }
                this.setPatient(patient);
                int i = this.queryPatientInfo();
                if (i == 1)
                {
                    this.cmbDept.Select();
                    this.cmbDept.SelectAll();
                }
                else if(i==0)
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
                int resultValue = FS.HISFC.Components.Common.Classes.Function.QueryComPatientInfo(ref tempPatient);
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
                string tempPatientNO=string.Empty;
                if (Function.GetAutoTempPatientNO(ref tempPatientNO) > 0)
                {
                    this.txtInpatientNO.Text = tempPatientNO;
                    this.txtInpatientNO.ReadOnly = true;
                    this.txtInpatientNO.Focus();
                    this.txtInpatientNO.SelectAll();
                    this.autoPatientNO = tempPatientNO;
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
                //重取住院号
                this.getAutoPatientNO();
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

            this.BeginAutoRefresh();
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
            this.cmbPayMode.Tag = "CA";
            this.cmbPayMode.Text = "现金";

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

            this.txtAge.TextChanged-=new EventHandler(txtAge_TextChanged);
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
            
        }

        public void ModifyPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.setPatientInfo(patientInfo);
            this.cmbDept.Focus();
            this.setEnable(false);
        }

        public event EventHandler OnSavePatientInfo;

        public  Size ControlSize
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
            return getPatientInfomation(patientInfo);
        }

        public void SetPatientInfo(PatientInfo patientInfo, bool isAll)
        {
            this.setPatientInfo(patientInfo);
            this.cmbInSource.SelectAll();
            this.cmbInSource.Focus();
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

        #endregion


        #region IInpatient 成员

        public string CardNo
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

        public bool IsCanModifyIName
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

        public bool IsCanModifyPatientNo
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
