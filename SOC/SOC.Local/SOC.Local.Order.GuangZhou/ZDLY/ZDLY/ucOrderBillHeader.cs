using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.SOC.Local.Order.GuangZhou.OrderPrint.ZDLY
{
    /// <summary>
    /// 医嘱单表头
    /// </summary>
    public partial class ucOrderBillHeader : UserControl
    {
        public ucOrderBillHeader()
        {
            InitializeComponent();
        }        

        Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        Neusoft.HISFC.BizLogic.Manager.Department deptMgr = new Neusoft.HISFC.BizLogic.Manager.Department();

		/// <summary>
		/// 标题
		/// </summary>
        private string header;

        /// <summary>
        /// 标题
        /// </summary>
		public  string Header
		{
			get
			{
				return this.header;
			}
            set
            {
                this.header = value;
                this.lblHeader.Text = value;
                this.lblHospital.Text = Neusoft.FrameWork.Management.Connection.Hospital.Name;

                try
                {
                    //System.IO.MemoryStream me = new System.IO.MemoryStream(((Neusoft.HISFC.Models.Base.Hospital)deptMgr.Hospital).HosLogoImage);
                    //this.picbLogo.Image = Image.FromStream(me);
                }
                catch
                {
                }
            }
		}
		/// <summary>
		/// 患者基本信息
		/// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo myPatientInfo;

		/// <summary>
		/// 设置转床床号
		/// </summary>
		/// <param name="dtBegin"></param>
		/// <param name="dtEnd"></param>
		/// <param name="bReprint"></param>
		/// <param name="chgBed"></param>
        public void SetChangedInfo(Neusoft.HISFC.Models.Order.Inpatient.Order order)
        {
            try
            {
                if (order == null)
                    return;

                Neusoft.HISFC.Models.Base.Department temp = this.deptMgr.GetDeptmentById(order.InDept.ID);

                this.lblDept.Text = temp.Name;
                this.lblBedNo.Text = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);

                temp = this.deptMgr.GetDeptmentById(order.Patient.PVisit.PatientLocation.NurseCell.ID);

                this.lblNurseCell.Text = temp.Name;

                this.lblAge.Text = this.deptMgr.GetAge(this.myPatientInfo.Birthday, order.MOTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		/// <summary>
		/// 设置标题可见性
		/// </summary>
		/// <param name="vis"></param>
		public void SetVisible(bool vis)
		{
			this.label12.Visible = vis;
			this.label15.Visible = vis;
			this.label3.Visible = vis;
			this.label4.Visible = vis;
			this.label5.Visible = vis;
			this.label6.Visible = vis;
			this.label8.Visible = vis;
            this.picbLogo.Visible = vis;
			this.lblHeader.Visible = vis;
            this.lblHospital.Visible = vis;
		}

		/// <summary>
		/// 设置内容可见性
		/// </summary>
		/// <param name="vis"></param>
        public void SetValueVisible(bool vis)
        {
            this.lblName.Visible = vis;
            this.lblAge.Visible = vis;
            this.lblBedNo.Visible = vis;
            this.lblDept.Visible = vis;
            this.lblNurseCell.Visible = vis;
            this.lblPatientNo.Visible = vis;
            this.lblSex.Visible = vis;
        }

		/// <summary>
        /// 设置患者基本信息
		/// </summary>
		/// <param name="patientInfo"></param>
		public void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
		{
            this.myPatientInfo = patientInfo;

            if (this.myPatientInfo != null)
            {
                this.Clear();
                //this.lblAge.Text = this.GetAge(this.myPatientInfo.Birthday);
                this.lblAge.Text = this.deptMgr.GetAge(this.myPatientInfo.Birthday);
                this.lblSex.Text = this.myPatientInfo.Sex.Name;
                this.lblName.Text = this.myPatientInfo.Name;

                //经常出现不显示婴儿的姓名，此处特殊处理
                if (string.IsNullOrEmpty(this.lblName.Text))
                {
                    Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();
                    Neusoft.HISFC.Models.RADT.PatientInfo babyInfo = radtIntegrate.GetPatientInfoByPatientNO(this.myPatientInfo.ID);
                    if (babyInfo == null || string.IsNullOrEmpty(babyInfo.ID))
                    {
                        MessageBox.Show("查找患者住院信息错误!\r\n" + radtIntegrate.Err);
                    }
                    else
                    {
                        lblName.Text = babyInfo.Name;
                    }
                }

                this.lblDept.Text = this.myPatientInfo.PVisit.PatientLocation.Dept.Name;
                this.lblNurseCell.Text = this.myPatientInfo.PVisit.PatientLocation.NurseCell.Name;
                if (this.myPatientInfo.PVisit.PatientLocation.Bed.ID.Length >= 4)
                {
                    this.lblBedNo.Text = this.myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
                }
                this.lblPatientNo.Text = this.myPatientInfo.PID.PatientNO.TrimStart('0');
            }
		}

		/// <summary>
		/// 清空
		/// </summary>
		public void Clear()
		{
			this.lblSex.Text = "";
			this.lblAge.Text = "";
			this.lblName.Text = "";
			this.lblDept.Text = "";
			this.lblNurseCell.Text = "";
			this.lblBedNo.Text = "";
            //this.lblChgBed.Text = "";
			this.lblPatientNo.Text = "";
		}
    }
}
