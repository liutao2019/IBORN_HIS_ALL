using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.GuangZhou.GYZL.OrderPrint
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

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

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
                this.lblHospital.Text = FS.FrameWork.Management.Connection.Hospital.Name;

                try
                {
                    //System.IO.MemoryStream me = new System.IO.MemoryStream(((FS.HISFC.Models.Base.Hospital)deptMgr.Hospital).HosLogoImage);
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
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo;

		/// <summary>
		/// 设置转床床号
		/// </summary>
		/// <param name="dtBegin"></param>
		/// <param name="dtEnd"></param>
		/// <param name="bReprint"></param>
		/// <param name="chgBed"></param>
        public void SetChangedInfo(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            try
            {
                if (order == null)
                    return;

                FS.HISFC.Models.Base.Department temp = this.deptMgr.GetDeptmentById(order.InDept.ID);

                this.lblDept.Text = temp.Name;
                //this.lblBedNo.Text = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                if (order.Patient.PVisit.PatientLocation.Bed.ID.Length >= 4)
                {
                    string room = SOC.HISFC.BizProcess.Cache.Common.GetBedInfo(order.Patient.PVisit.PatientLocation.Bed.ID).SickRoom.ID;
                    this.lblBedNo.Text = room + "房" + order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床";
                }


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
            //this.label8.Visible = vis;
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
            //this.lblNurseCell.Visible = vis;
            this.lblPatientNo.Visible = vis;
            this.lblSex.Visible = vis;
        }

		/// <summary>
        /// 设置患者基本信息
		/// </summary>
		/// <param name="patientInfo"></param>
		public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
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
                    FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                    FS.HISFC.Models.RADT.PatientInfo babyInfo = radtIntegrate.GetPatientInfoByPatientNO(this.myPatientInfo.ID);
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
                    string room = SOC.HISFC.BizProcess.Cache.Common.GetBedInfo(this.myPatientInfo.PVisit.PatientLocation.Bed.ID).SickRoom.ID;
                    this.lblBedNo.Text = room + "房" + this.myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床";
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
