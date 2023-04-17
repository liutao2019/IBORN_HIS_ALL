using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Management;
using Neusoft.HISFC.Models.Base;
using System.Threading;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.ZDLY
{
    public partial class ucOutPatientNotice : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOutPatientNotice()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 住院如出转业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.RADT.OutPatient radtManager = new Neusoft.HISFC.BizLogic.RADT.OutPatient();

        /// <summary>
        /// 病案基本信息
        /// </summary>
        private Neusoft.HISFC.Models.HealthRecord.Base CaseBase = new Neusoft.HISFC.Models.HealthRecord.Base();

        private Neusoft.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();


        /// <summary>
        /// 床位管理类
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Bed managerBed = new Neusoft.HISFC.BizLogic.Manager.Bed();


        /// <summary>
        /// 管理类
        /// </summary>
        private Neusoft.SOC.HISFC.BizProcess.CommonInterface.CommonController commonController = Neusoft.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance();


        /// <summary>
        /// 挂号实体
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register reg = null;
       

        #endregion


        /// <summary>
        /// 设置病人信息
        /// </summary>
        /// <param name="reg"></param>
        private void SetPatient(Neusoft.HISFC.Models.Registration.Register reg)
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
                this.txtDoctor.Name = ((Neusoft.HISFC.Models.Base.Employee)(radtManager.Operator)).Name;
                this.txtDoctorCode.Text = ((Neusoft.HISFC.Models.Base.Employee)(radtManager.Operator)).ID.Substring(2);

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
            this.cmbBedNO.Text = string.Empty;
            this.cmbBedNO.Tag = "";
            this.cmbBedNO.ClearItems();
            this.dtInDate.Value = commonController.GetSystemTime();
            
            this.txtIndetity.Text = string.Empty;
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
            this.txtLinkPhone.Text = string.Empty;
            this.txtHomePhone.Text = string.Empty;
            this.cmbSex.Tag = "";
            this.cmbSex.Text = "";
            this.cmbCircs.Tag = "";
            this.cmbCircs.Text = "";
            this.cmbNation.Tag = "";
            this.cmbNation.Text = "";



        }


        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void initCombo()
        {
            //性别列表
            this.cmbSex.AddItems(Neusoft.HISFC.Models.Base.SexEnumService.List());
            this.cmbSex.Text = "";
            this.cmbSex.Tag = "";

            //国籍
            this.cmbCountry.AddItems(commonController.QueryConstant(EnumConstant.COUNTRY));
            //职业
            ArrayList profList = commonController.QueryConstant(EnumConstant.PROFESSION);
            this.cmbProfession.AddItems(profList);
            //出生地信息
            this.cmbBirthArea.AddItems(commonController.QueryConstant(EnumConstant.DIST));
            //民族
            this.cmbNation.AddItems(commonController.QueryConstant(EnumConstant.NATION));
            //联系人关系
            this.cmbRelation.AddItems(commonController.QueryConstant(EnumConstant.RELATIVE));

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
           
            System.IO.MemoryStream image = new System.IO.MemoryStream(((Neusoft.HISFC.Models.Base.Hospital)this.radtManager.Hospital).HosLogoImage);
            this.picLogo.Image = Image.FromStream(image);


        }


        #region 事件

        protected override int OnPrint(object sender, object neuObject)
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;   
            print.PrintDocument.DefaultPageSettings.Landscape = false;
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this.panel1);
            }
            else
            {
                print.PrintPage(5, 5, this.panel1);
            }
            
            return 1;
        }

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

            cmbBedNO.ClearItems();
            this.cmbBedNO.AddItems(alBed);
           
        }


        private void cmbDept_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbDept.Tag == null)
            {
                return;
            }

            Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alNurse = new ArrayList();
            ArrayList al = deptStatMgr.LoadByParent("01", this.cmbDept.Tag.ToString());
            if (al == null || al.Count == 0)
            {
                al = deptStatMgr.LoadByChildren("01", this.cmbDept.Tag.ToString());
                if (al != null)
                {
                    foreach (Neusoft.HISFC.Models.Base.DepartmentStat deptStat in al)
                    {
                        alNurse.Add(new Neusoft.FrameWork.Models.NeuObject(deptStat.PardepCode, deptStat.PardepName, ""));
                    }
                }
            }
            else
            {
                foreach (Neusoft.HISFC.Models.Base.DepartmentStat deptStat in al)
                {
                    alNurse.Add(new Neusoft.FrameWork.Models.NeuObject(deptStat.DeptCode, deptStat.DeptName, ""));
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
                string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
                ArrayList alRegs = this.regMgr.Query(cardNo, DateTime.Now.AddDays(-7));
                if (alRegs == null || alRegs.Count == 0)
                {
                    MessageBox.Show("没有门诊号为:" + cardNo + "的患者!", "提示");
                    this.txtCardNo.Focus();
                    return;
                }
                reg = alRegs[alRegs.Count - 1] as Neusoft.HISFC.Models.Registration.Register;
                if (reg == null || reg.ID == "")
                {
                    MessageBox.Show("没有门诊号为:" + cardNo + "的患者!", "提示");
                    this.txtCardNo.Focus();
                    return;
                }

                this.txtCardNo.Text = cardNo;
                this.cmbCircs.SelectedIndex = 0;
                this.SetPatient(reg);

            }
        }

        private void ucOutPatientNotice_Load(object sender, EventArgs e)
        {
            initCombo();
        }

        #endregion

      

      

    }
}
