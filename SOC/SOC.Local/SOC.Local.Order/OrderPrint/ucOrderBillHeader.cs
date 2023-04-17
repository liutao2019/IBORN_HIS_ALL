using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.OrderPrint
{
	/// <summary>
	/// ucOrderBillHeader 的摘要说明。
	/// </summary>
	public class ucOrderBillHeader : System.Windows.Forms.UserControl
    {
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label lblPatientNo;
		private System.Windows.Forms.Label lblSex;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblAge;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label lblBedNo;
		private System.Windows.Forms.Label lblDept;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblChgBed;
		private System.Windows.Forms.Label lblNurseCell;
		private System.Windows.Forms.Label label8;
        private Label lblHosName;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucOrderBillHeader()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.lblHeader = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPatientNo = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblBedNo = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblChgBed = new System.Windows.Forms.Label();
            this.lblNurseCell = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblHosName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHeader.Location = new System.Drawing.Point(118, 59);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(492, 39);
            this.lblHeader.TabIndex = 42;
            this.lblHeader.Text = "长  期  医  嘱  单";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(1, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "姓名:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(43, 117);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(121, 20);
            this.lblName.TabIndex = 45;
            this.lblName.Text = "斯瓦辛格之长子";
            // 
            // lblPatientNo
            // 
            this.lblPatientNo.AutoSize = true;
            this.lblPatientNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientNo.Location = new System.Drawing.Point(636, 117);
            this.lblPatientNo.Name = "lblPatientNo";
            this.lblPatientNo.Size = new System.Drawing.Size(99, 20);
            this.lblPatientNo.TabIndex = 54;
            this.lblPatientNo.Text = "0123456789";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(577, 117);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 20);
            this.label15.TabIndex = 52;
            this.label15.Text = "住院号:";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(213, 117);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(25, 20);
            this.lblSex.TabIndex = 56;
            this.lblSex.Text = "男";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(167, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.TabIndex = 55;
            this.label3.Text = "性别:";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.Location = new System.Drawing.Point(280, 117);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(68, 20);
            this.lblAge.TabIndex = 58;
            this.lblAge.Text = "43岁7天";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(239, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 20);
            this.label6.TabIndex = 57;
            this.label6.Text = "年龄:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(492, 117);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 20);
            this.label12.TabIndex = 49;
            this.label12.Text = "床号:";
            // 
            // lblBedNo
            // 
            this.lblBedNo.AutoSize = true;
            this.lblBedNo.BackColor = System.Drawing.Color.White;
            this.lblBedNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedNo.Location = new System.Drawing.Point(534, 117);
            this.lblBedNo.Name = "lblBedNo";
            this.lblBedNo.Size = new System.Drawing.Size(44, 20);
            this.lblBedNo.TabIndex = 51;
            this.lblBedNo.Text = "J005";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(391, 117);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(105, 20);
            this.lblDept.TabIndex = 60;
            this.lblDept.Text = "肿瘤内科二区";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(349, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 59;
            this.label5.Text = "科室:";
            // 
            // lblChgBed
            // 
            this.lblChgBed.Location = new System.Drawing.Point(492, 98);
            this.lblChgBed.Name = "lblChgBed";
            this.lblChgBed.Size = new System.Drawing.Size(162, 17);
            this.lblChgBed.TabIndex = 61;
            // 
            // lblNurseCell
            // 
            this.lblNurseCell.AutoSize = true;
            this.lblNurseCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNurseCell.Location = new System.Drawing.Point(664, 64);
            this.lblNurseCell.Name = "lblNurseCell";
            this.lblNurseCell.Size = new System.Drawing.Size(99, 20);
            this.lblNurseCell.TabIndex = 63;
            this.lblNurseCell.Text = "0000000000";
            this.lblNurseCell.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(626, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 20);
            this.label8.TabIndex = 62;
            this.label8.Text = "病区:";
            this.label8.Visible = false;
            // 
            // lblHosName
            // 
            this.lblHosName.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHosName.Location = new System.Drawing.Point(117, 0);
            this.lblHosName.Name = "lblHosName";
            this.lblHosName.Size = new System.Drawing.Size(495, 35);
            this.lblHosName.TabIndex = 64;
            this.lblHosName.Text = "医院";
            this.lblHosName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucOrderBillHeader
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblHosName);
            this.Controls.Add(this.lblNurseCell);
            this.Controls.Add(this.lblChgBed);
            this.Controls.Add(this.lblDept);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPatientNo);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblBedNo);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.label12);
            this.Name = "ucOrderBillHeader";
            this.Size = new System.Drawing.Size(801, 142);
            this.Load += new System.EventHandler(this.ucOrderBillHeader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        void ucOrderBillHeader_Load(object sender, EventArgs e)
        {
            try
            {
                this.lblHosName.Text = manager.GetHospitalName();
            }
            catch { }
        }
		#endregion

		/// <summary>
		/// 标题
		/// </summary>
        private string header;
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
		/// 设置显示
		/// </summary>
		/// <param name="vis"></param>
		public  void SetVisible(bool vis)
		{
			this.label12.Visible = vis;
			this.label15.Visible = vis;
			this.label3.Visible = vis;
			this.label4.Visible = vis;
			this.label5.Visible = vis;
			this.label6.Visible = vis;
			this.label8.Visible = false;
            this.lblHosName.Visible = vis;
			this.lblHeader.Visible = vis;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="vis"></param>
        public void SetValueVisible(bool vis)
        {
            this.lblName.Visible = vis;
            this.lblAge.Visible = vis;
            this.lblBedNo.Visible = vis;
            this.lblDept.Visible = vis;
            this.lblNurseCell.Visible = false;
            this.lblPatientNo.Visible = vis;
            this.lblSex.Visible = vis;
        }

        /// <summary>
        /// 获得年龄
        /// </summary>
        /// <param name="dtBirthday"></param>
        /// <returns></returns>
        private string GetAge(DateTime dtBirthday)
        {
            if (dtBirthday == System.DateTime.MinValue)
            {
                return "";
            }

            DateTime systime = this.deptMgr.GetDateTimeFromSysDateTime();
            TimeSpan span = new TimeSpan(systime.Ticks - dtBirthday.Ticks);
            string strAge = "";
            if (span.Days / 365 <= 0)
            {
                if (span.Days / 30 <= 0)
                {
                    strAge = span.Days.ToString() + "天";
                }
                else
                {
                    strAge = System.Convert.ToString(span.Days / 30) + "月";
                }
            }
            else
            {
                strAge = System.Convert.ToString(span.Days / 365) + "岁";
                if (span.Days / 365 < 5)
                {
                    int diff = span.Days - (span.Days / 365) * 365;
                    if (diff > 30)
                    {
                        strAge = strAge + System.Convert.ToString(diff / 30) + "月";
                    }
                    else
                    {
                        strAge = strAge + System.Convert.ToString(diff) + "天";
                    }
                }
            }
            return strAge;
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
			this.lblChgBed.Text = "";
			this.lblPatientNo.Text = "";
		}
	}
}
