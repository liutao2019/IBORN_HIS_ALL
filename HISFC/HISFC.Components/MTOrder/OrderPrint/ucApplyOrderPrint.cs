using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.MTOrder.OrderPrint
{
    //FS.HISFC.BizProcess.Interface.IRecipePrint
    public partial class ucApplyOrderPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucApplyOrderPrint()
        {
            InitializeComponent();
        }

        #region 变量

        #region 业务层

        /// <summary>
        /// 诊断管理
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        /// <summary>
        /// 住院医嘱管理
        /// </summary>
        FS.HISFC.BizLogic.Order.Order inOrderMgr = new FS.HISFC.BizLogic.Order.Order();
        /// <summary>
        /// 门诊医嘱管理
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMge = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// 病人信息管理类
        /// </summary>
        private HISFC.BizLogic.Registration.Register registerMgr = new HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 住院病人信息管理类
        /// </summary>
        private HISFC.BizLogic.RADT.InPatient inPatMgr = new HISFC.BizLogic.RADT.InPatient();
        /// <summary>
        /// 科室分类维护
        /// </summary>
        FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStat = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

        FS.SOC.HISFC.Fee.BizLogic.Undrug unDrugMgr = new FS.SOC.HISFC.Fee.BizLogic.Undrug();

        FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        FS.HISFC.BizLogic.Order.OutPatient.Order orderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        FS.HISFC.BizLogic.Pharmacy.DrugStore drugManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();


        #endregion
        /// <summary>
        /// 判断是否是门诊
        /// </summary>
        private bool IsClinic
        {
            get
            {
                return Appoint.OrderType == Models.MedicalTechnology.EnumApplyType.Clinic;
            }
        }

        /// <summary>
        /// 患者信息(住院/门诊通用)
        /// </summary>
        private FS.HISFC.Models.RADT.Patient PatientInfo;
        /// <summary>
        /// 申请单列表
        /// </summary>
        private FS.HISFC.Models.MedicalTechnology.Appointment Appoint = new FS.HISFC.Models.MedicalTechnology.Appointment();

        #endregion

        #region 初始化

        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucNewRecipePrint_Load(object sender, System.EventArgs e)
        {

        }

        #endregion

        #region IOutPatientOrderPrint 成员

        public void SetValues(FS.HISFC.Models.MedicalTechnology.Appointment app)
        {
            if ( app == null)
                throw new ArgumentNullException("通用医技申请信息不能为空");
            this.Appoint = app;

            if (IsClinic)
                this.PatientInfo = registerMgr.GetByClinic(app.ClinicCode);
            else this.PatientInfo = inPatMgr.QueryPatientInfoByInpatientNO(app.ClinicCode);

            this.SetPatientInfo();
            this.SetPrintValue();
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo()
        {
            //GetHospLogo();
            #region 基本信息
            this.lblName.Text = this.PatientInfo.Name;
            this.lblSex.Text = this.PatientInfo.Sex.Name; //性别
            // this.npbRecipeNo.Text = this.myReg.; //申请单号??
            if (IsClinic)
            {
                lbClinicType.Text = "门诊号：";
                lblCardNo.Text = PatientInfo.PID.CardNO;
                this.npbBarCode.Image = FS.SOC.Local.Order.OutPatientOrder.Common.ComFunc.CreateBarCode(this.PatientInfo.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
            }
            else
            {
                lbClinicType.Text = "住院号：";
                lblCardNo.Text = PatientInfo.PID.PatientNO;
                this.npbBarCode.Image = FS.SOC.Local.Order.OutPatientOrder.Common.ComFunc.CreateBarCode(this.PatientInfo.PID.PatientNO, this.npbBarCode.Width, this.npbBarCode.Height);
            }
            //床号
            this.lblBedNo.Text = Appoint.BedNo;
            //ClinicNo或住院号
            //this.lblClinic_Code.Text = PatientInfo.ID;
            //电话号码
            this.lblTelephone.Text = Appoint.Telephone;
            this.lblAge.Text = this.inOrderMgr.GetAge(this.PatientInfo.Birthday, false); //年龄
            #endregion

            #region 诊断/病史/注意事项
            lblDig.Text = Appoint.Diagnosis;
            lblMedHis.Text = Appoint.MedicalHistory;
            FS.SOC.HISFC.Fee.Models.Undrug undrug = unDrugMgr.GetUndrug(Appoint.ItemCode);
            if (!string.IsNullOrEmpty(undrug.Memo))
            {
                lblMemo.Text = undrug.Memo;
            }
            #endregion
        }
        FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue()
        {

            lbArrangeDate.Text = Appoint.ArrangeDate.ToString("yyyy-MM-dd");
            //lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lblPrintTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            lblCheckItem.Text = Appoint.ItemName;

            //如果生殖中心开的项目则不显示科室地点
            if ("3046".Equals(Appoint.DeptCode))
            {
                this.lblDeptPlace.Text = "";
            }
            else
            {
                ArrayList deptMemo = deptStat.LoadByChildren("00", Appoint.ExecDeptCode);
                if (deptMemo != null && deptMemo.Count > 0)
                {
                    this.lblDeptPlace.Text += (deptMemo[0] as FS.HISFC.Models.Base.DepartmentStat).Memo;
                }
            }
            //开立医生
            lblDocName.Text = Appoint.DoctCode + "/" + Appoint.DoctName;
            //开立科室
            lblDept.Text = Appoint.DeptName;
            //执行医生
            lblExecDoct.Text += Appoint.ExecDoctName;
            //执行科室
            lblExecDept.Text += Appoint.ExecDeptName;
            //检查项目
            lblCheckItem.Text = Appoint.ItemName;
            //预约时间
            lbArrangeTime.Text = Appoint.ArrangeTime;
            //预约项目及类型
            lblMT.Text = Appoint.MTName + "/" + Appoint.TypeName;
            //项目费用
            lblFee.Text = Appoint.ItemCost.ToString("F2");
            //到达方式
            lblArrivalPattern.Text = Appoint.ArrivalPattern;
            //标题
            lblTitle.Text = Appoint.ExecDeptName + "申请单";
            npbRecipeNo.Text = Appoint.ID;

            lblOrderNo.Text = Appoint.OrderNo;

            PrintPage();

        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //  print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));
            //  print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 808, 586));

            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 586, 808));

            print.IsLandScape = true;

            print.SetPageSize(FS.SOC.Local.Order.OutPatientOrder.ZDLY.Common.Function.getPrintPage(true));


            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (FS.SOC.Local.Order.OutPatientOrder.ZDLY.Common.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        #endregion

        //private void GetHospLogo()
        //{
        //    try
        //    {
        //        System.IO.MemoryStream image = new System.IO.MemoryStream(((FS.HISFC.Models.Base.Hospital)this.orderManager.Hospital).HosLogoImage);
        //        this.picLogo.Image = Image.FromStream(image);
        //        this.picLogo.Visible = true;
        //    }
        //    catch { }
        //}

    }
}
