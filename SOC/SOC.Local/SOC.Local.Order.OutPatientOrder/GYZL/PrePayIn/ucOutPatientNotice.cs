using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.HISFC.Models.Base;
using System.Threading;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.PrePayIn
{
    public partial class ucOutPatientNotice : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintInHosNotice
    {
        public ucOutPatientNotice()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 住院如出转业务层
        /// </summary>
        FS.HISFC.BizLogic.RADT.OutPatient radtManager = new FS.HISFC.BizLogic.RADT.OutPatient();

        /// <summary>
        /// 病案基本信息
        /// </summary>
        private FS.HISFC.Models.HealthRecord.Base CaseBase = new FS.HISFC.Models.HealthRecord.Base();

        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();


        /// <summary>
        /// 床位管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Bed managerBed = new FS.HISFC.BizLogic.Manager.Bed();


        /// <summary>
        /// 管理类
        /// </summary>
        private FS.SOC.HISFC.BizProcess.CommonInterface.CommonController commonController = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance();



        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();


        /// <summary>
        /// 挂号实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register reg = null;
       

        #endregion


        /// <summary>
        /// 设置病人信息
        /// </summary>
        /// <param name="reg"></param>
        private void SetPatient(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg == null || reg.ID == "")
            {
                return;
            }
            else
            {
                this.txtName.Text = reg.Name;
                this.cmbSex.Tag = reg.Sex.ID;
                this.txtAge.Text = this.commonController.GetAge(reg.Birthday);

                this.dtInDate.Value = commonController.GetSystemTime();
                this.txtName.Text =reg.Name;
                this.cmbDept.SelectedIndex = 0;
                this.txtDoctor.Name = ((FS.HISFC.Models.Base.Employee)(radtManager.Operator)).Name;
                this.txtDoctorCode.Text = ((FS.HISFC.Models.Base.Employee)(radtManager.Operator)).ID.Substring(2);

                if (!string.IsNullOrEmpty(reg.IDCard))
                {
                    this.lblindetity.Text = reg.IDCard;
                }
                this.dtBirthday.Value = reg.Birthday;
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            this.txtName.Text = string.Empty;
            this.cmbDept.Text = string.Empty;
            this.cmbDept.Tag = "";
            this.cmbBedNo.Text = string.Empty;
            this.cmbBedNo.Tag = "";
            this.cmbBedNo.ClearItems();
            this.dtInDate.Value = commonController.GetSystemTime();
            
            this.txtIdentity.Text = string.Empty;
            this.cmbHomePlace.Text = string.Empty;
            this.cmbHomePlace.Tag = "";
            this.cmbCountry.Text = string.Empty;
            this.cmbCountry.Tag = "";
            this.cmbPos.Text = string.Empty;
            this.cmbPos.Tag = "";
            this.txtLinkMan.Text = string.Empty;
            this.cmbLinkManRel.Text = string.Empty;
            this.cmbLinkManRel.Tag = "";
            this.txtLinkManAddr.Text = string.Empty;
            this.txtWorkTel.Text = string.Empty;
            this.txtLinkTel.Text = string.Empty;
            this.txtHomeAddr.Text = string.Empty;
            this.cmbSex.Tag = "";
            this.cmbSex.Text = "";
            this.cmbCircs.Tag = "";
            this.cmbCircs.Text = "";
            this.cmbNationality.Tag = "";
            this.cmbNationality.Text = "";



        }


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
            //职业
            ArrayList profList = commonController.QueryConstant(EnumConstant.PROFESSION);
            this.cmbPos.AddItems(profList);
            //出生地信息
            this.cmbHomePlace.AddItems(commonController.QueryConstant(EnumConstant.DIST));
            //民族
            this.cmbNationality.AddItems(commonController.QueryConstant(EnumConstant.NATION));
            //联系人关系
            this.cmbLinkManRel.AddItems(commonController.QueryConstant(EnumConstant.RELATIVE));

            this.cmbPayKind.AddItems(commonController.QueryConstant(EnumConstant.PAYKIND));


            //病区
            //this.cmbNurseCell.AddItems(commonController.QueryDepartment(EnumDepartmentType.N));
            //病人来源信息
            //this.cmbInSource.AddItems(commonController.QueryConstant(EnumConstant.INSOURCE));
            //入院情况信息
            this.cmbCircs.AddItems(commonController.QueryConstant(EnumConstant.INCIRCS));
            //入院途径信息
            //this.cmbApproach.AddItems(commonController.QueryConstant(EnumConstant.INAVENUE));
            //转入院科室
            this.cmbDept.AddItems(commonController.QueryDepartment(EnumDepartmentType.I));
           
            System.IO.MemoryStream image = new System.IO.MemoryStream(((FS.HISFC.Models.Base.Hospital)this.radtManager.Hospital).HosLogoImage);
            this.picLogo.Image = Image.FromStream(image);


        }


        #region 事件


        private void cmbNurseCell_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbNurseCell.Tag == null)
            {
                return;
            }

            ArrayList alBed = managerBed.GetUnoccupiedBed(this.cmbNurseCell.Tag.ToString());
            if (alBed == null)
            {
                MessageBox.Show("查找床位失败！");
                return;
            }

            this.lblBedCount.Text = "剩" + alBed.Count + "张";
            //if (alBed.Count == 0)
            //{
            //    MessageBox.Show("病区：" + this.cmbNurseCell.Text + "目前没有床位！");
            //    return;
            //}

            cmbBedNo.ClearItems();
            this.cmbBedNo.AddItems(alBed);
           
        }


        private void cmbDept_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbDept.Tag == null)
            {
                return;
            }

            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alNurse = new ArrayList();
            ArrayList al = deptStatMgr.LoadByParent("01", this.cmbDept.Tag.ToString());
            if (al == null || al.Count == 0)
            {
                al = deptStatMgr.LoadByChildren("01", this.cmbDept.Tag.ToString());
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                    {
                        alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.PardepCode, deptStat.PardepName, ""));
                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                {
                    alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.DeptCode, deptStat.DeptName, ""));
                }
            }
            if (alNurse != null)
            {
                this.cmbNurseCell.ClearItems();
                this.cmbNurseCell.AddItems(alNurse);
            }
        }


        /// <summary>
        /// 回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("请输入门诊号!", "提示");
                    this.txtCardNo.Focus();
                    return;
                }

                FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();
                if (this.feeIntegrate.ValidMarkNO(this.txtCardNo.Text, ref accountCardObj) <= 0)
                {
                    MessageBox.Show(this.feeIntegrate.Err);
                    return;
                }

                ArrayList alRegs = this.regMgr.Query(accountCardObj.Patient.PID.CardNO, DateTime.Now.AddDays(-7));
                if (alRegs == null || alRegs.Count == 0)
                {
                    MessageBox.Show("没有门诊号为:" + accountCardObj.Patient.PID.CardNO + "的患者!", "提示");
                    this.txtCardNo.Focus();
                    return;
                }
                reg = alRegs[alRegs.Count - 1] as FS.HISFC.Models.Registration.Register;
                if (reg == null || reg.ID == "")
                {
                    MessageBox.Show("没有门诊号为:" + accountCardObj.Patient.PID.CardNO + "的患者!", "提示");
                    this.txtCardNo.Focus();
                    return;
                }

                this.txtCardNo.Text = accountCardObj.Patient.PID.CardNO;
                this.cmbCircs.SelectedIndex = 0;
                this.SetPatient(reg);

            }
        }

        private void ucOutPatientNotice_Load(object sender, EventArgs e)
        {
            initCombo();
        }

        #endregion



        #region IPrintInHosNotice 成员

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintDocument.DefaultPageSettings.Landscape = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this.panel1);
            }
            else
            {
                print.PrintPage(5, 5, this.panel1);
            }
            return 1;
        }

        public int PrintView()
        {
            throw new NotImplementedException();
        }

        public int SetValue(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            try
            {
                if (obj.PID.CardNO != null)
                {
                    this.txtCardNo.Text = obj.PID.CardNO;//病例号
                    this.cmbDept.Tag = obj.PVisit.PatientLocation.Dept.ID;//住院科室
                    this.cmbDept.Text = obj.PVisit.PatientLocation.Dept.Name;
                    this.dtInDate.Value = obj.PVisit.InTime;//预约日期----------

                    this.txtName.Text = obj.Name;//姓名
                    this.cmbSex.Tag = obj.Sex.ID;//性别
                    //this.cmbPactUnit.Tag = obj.Pact.ID;//合同单位
                    this.cmbPayKind.Tag = obj.Pact.PayKind.ID;//结算类别

                    this.dtBirthday.Value = obj.Birthday;//出生日期
                    //this.cmbMarriage.Tag = obj.MaritalStatus.ID;//婚姻状况
                    this.txtIdentity.Text = obj.IDCard;//身份证号
                    this.cmbPos.Tag = obj.Profession.ID;//职业
                    this.cmbHomePlace.Tag = obj.DIST;//出生地
                    this.cmbCountry.Tag = obj.Country.ID;//国籍

                    this.txtHomeAddr.Text = obj.AddressHome;//家庭住址
                    //this.txtHomeTel.Text = obj.PhoneHome;//家庭电话
                    this.txtWorkUnit.Text = obj.CompanyName;//工作单位
                    this.txtLinkMan.Text = obj.Kin.ID;//联系人
                    this.txtLinkTel.Text = obj.Kin.RelationAddress;//联系人住址
                    this.txtLinkTel.Text = obj.Kin.RelationPhone;//联系人电话
                    this.txtWorkTel.Text = obj.PhoneBusiness;//工作单位电话
                    this.cmbNationality.Tag = obj.Nationality.ID;//民族
                    this.cmbLinkManRel.Tag = obj.Kin.Relation.ID;//联系人关系
                    this.cmbBedNo.Tag = obj.PVisit.PatientLocation.Bed.ID;//病床号
                    //this.cmbPreDoc.Tag = obj.PVisit.AdmittingDoctor.ID;//预约医生


                    if(!string.IsNullOrEmpty(obj.PVisit.AdmittingDoctor.ID))
                    {
                        this.txtDoctorCode.Text = obj.PVisit.AdmittingDoctor.ID.Substring(2);


                        this.txtDoctor.Text=commonController.GetEmployee(obj.PVisit.AdmittingDoctor.ID).Name;
                    }

                    if (obj.Diagnoses.Count > 0)
                        this.txtInDiagnose.Tag = (obj.Diagnoses[0] as FS.FrameWork.Models.NeuObject).ID;//门诊诊断编码

                    this.txtInDiagnose.Text = obj.ClinicDiagnose;//门诊诊断名称

                    this.txtSSN.Text = obj.SSN;//医疗证号
                    this.cmbNurseCell.Tag = obj.PVisit.PatientLocation.NurseCell.ID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return 1;
        }

        #endregion

        private void txtDiag_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
