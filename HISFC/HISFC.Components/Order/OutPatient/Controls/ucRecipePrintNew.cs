using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;

namespace Neusoft.HISFC.Components.Order.OutPatient.Controls
{
	/// <summary>
	/// ucRecipePrintNew 的摘要说明。
	/// </summary>
    public class ucRecipePrintNew : System.Windows.Forms.UserControl, Neusoft.HISFC.BizProcess.Interface.IRecipePrint
    {
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
        private System.ComponentModel.Container components = null;
		public  ArrayList alOrder = new ArrayList();//医嘱数组
        private ArrayList alTemp = new ArrayList();


        private Hashtable recipeItem = new Hashtable();
        /// <summary>
        /// 是否补打
        /// </summary>
        private bool isReprint = false;
        /// <summary>
        /// 是否是手工方
        /// </summary>
        private bool isManualRecipe = false;
        /// <summary>
        /// 药房传过来的摆药信息
        /// </summary>
        private ArrayList applyList = new ArrayList();


        /// <summary>
        /// 是否加急
        /// </summary>
        private bool isEmergency = false;
		/// <summary>
		/// 频次
		/// </summary>
		private Neusoft.FrameWork.Public.ObjectHelper freHelper = null;
        public Neusoft.FrameWork.Public.ObjectHelper FreHelper
		{
			set
			{
				this.freHelper = value;
			}
		}
		/// <summary>
		/// 处方号
		/// </summary>
		private string recipeId = "";

        /// <summary>
        /// 是否需要打印，如果处方不包含药品就不打印
        /// </summary>
        bool isNeedPrint = true;

        /// <summary>
        /// 打印机名
        /// </summary>
        private string printer = string.Empty;

        /// <summary>
        /// 打印机名
        /// </summary>
        [Category("设置"), Description("打印机名")]
        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }


        #region 变量
        private Panel panel1;
        private Panel panel2;
        private Label lblCardNo;
        private Label label2;
        private Panel panel3;
        private Label lblSeeYear;
        private Label label31;
        private Label label14;
        private Panel panel4;
        private Label label19;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Panel panel6;
        private Label lblTitle;
        private Label label12;
        private Label label10;
        private Label label13;
        private Label lblDiag;
        private Label lblSeeDept;
        private Label lblName;
        private Label lblOrder;
        private Label lblTel;
        private Label lblPhaDoc;
        private Label label24;
        private Label label25;
        private Label label20;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label9;
        private Label label11;
        private Label label21;
        private Label lblSendWindow;
        private Panel panel5;
        private Label lblPhaMoney;
        private Label label23;
        private Label lblSex;
        private Label lblAge;
        private Label lblMCardNo;
        private Label lblHosCode;
        private Label lblFeeType;
        private Label label4;
        private Label lblAddess;
        private Label lblRecipeNo;
        private Label lblWindow;
        private Label lblPringDate;
        private Label label27;
        private Panel pnlP;
        private Label lblPCCOrder;
        private Panel pnlPCC;
        private Label lblPCCTitleL;
        private Label lblPCCTitleR;
        private Panel panel7;
        private Label label15;
        private Panel panel8;
        private Label lblHosName;
        #endregion
        private Label lblReprint;
        private Label lblEmergency;

        /// <summary>
		/// 患者挂号信息
		/// </summary>
        private Neusoft.HISFC.Models.Registration.Register myReg;
	
		public  Neusoft.HISFC.Models.Registration.Register Reg
		{
			get
			{
				return this.myReg;
			}
			set
			{
				this.myReg = value;
				this.SetPatientInfo();
			}
		}
		
		public ucRecipePrintNew()
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblEmergency = new System.Windows.Forms.Label();
            this.lblFeeType = new System.Windows.Forms.Label();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.lblReprint = new System.Windows.Forms.Label();
            this.lblHosName = new System.Windows.Forms.Label();
            this.pnlPCC = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblPCCTitleL = new System.Windows.Forms.Label();
            this.lblPCCTitleR = new System.Windows.Forms.Label();
            this.lblPCCOrder = new System.Windows.Forms.Label();
            this.pnlP = new System.Windows.Forms.Panel();
            this.lblOrder = new System.Windows.Forms.Label();
            this.lblWindow = new System.Windows.Forms.Label();
            this.lblRecipeNo = new System.Windows.Forms.Label();
            this.lblAddess = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblHosCode = new System.Windows.Forms.Label();
            this.lblMCardNo = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblSendWindow = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblPhaMoney = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblPringDate = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblPhaDoc = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblSeeYear = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblDiag = new System.Windows.Forms.Label();
            this.lblSeeDept = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.pnlPCC.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlP.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(608, 1);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblEmergency);
            this.panel2.Controls.Add(this.lblFeeType);
            this.panel2.Controls.Add(this.lblCardNo);
            this.panel2.Controls.Add(this.lblReprint);
            this.panel2.Controls.Add(this.lblHosName);
            this.panel2.Controls.Add(this.pnlPCC);
            this.panel2.Controls.Add(this.pnlP);
            this.panel2.Controls.Add(this.lblWindow);
            this.panel2.Controls.Add(this.lblRecipeNo);
            this.panel2.Controls.Add(this.lblAddess);
            this.panel2.Controls.Add(this.lblTel);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblHosCode);
            this.panel2.Controls.Add(this.lblMCardNo);
            this.panel2.Controls.Add(this.lblSex);
            this.panel2.Controls.Add(this.lblAge);
            this.panel2.Controls.Add(this.lblSendWindow);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.lblPhaMoney);
            this.panel2.Controls.Add(this.label23);
            this.panel2.Controls.Add(this.lblPringDate);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.lblPhaDoc);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.lblSeeYear);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.lblDiag);
            this.panel2.Controls.Add(this.lblSeeDept);
            this.panel2.Controls.Add(this.lblName);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(608, 764);
            this.panel2.TabIndex = 1;
            // 
            // lblEmergency
            // 
            this.lblEmergency.AutoSize = true;
            this.lblEmergency.Font = new System.Drawing.Font("宋体", 30F);
            this.lblEmergency.Location = new System.Drawing.Point(26, 17);
            this.lblEmergency.Name = "lblEmergency";
            this.lblEmergency.Size = new System.Drawing.Size(0, 40);
            this.lblEmergency.TabIndex = 156;
            // 
            // lblFeeType
            // 
            this.lblFeeType.AutoSize = true;
            this.lblFeeType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFeeType.ForeColor = System.Drawing.Color.Black;
            this.lblFeeType.Location = new System.Drawing.Point(80, 100);
            this.lblFeeType.Name = "lblFeeType";
            this.lblFeeType.Size = new System.Drawing.Size(35, 14);
            this.lblFeeType.TabIndex = 148;
            this.lblFeeType.Text = "公费";
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardNo.ForeColor = System.Drawing.Color.Black;
            this.lblCardNo.Location = new System.Drawing.Point(80, 73);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(119, 14);
            this.lblCardNo.TabIndex = 106;
            this.lblCardNo.Text = "1111111111111111";
            // 
            // lblReprint
            // 
            this.lblReprint.AutoSize = true;
            this.lblReprint.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblReprint.Location = new System.Drawing.Point(461, 28);
            this.lblReprint.Name = "lblReprint";
            this.lblReprint.Size = new System.Drawing.Size(0, 24);
            this.lblReprint.TabIndex = 155;
            // 
            // lblHosName
            // 
            this.lblHosName.AutoSize = true;
            this.lblHosName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHosName.ForeColor = System.Drawing.Color.Black;
            this.lblHosName.Location = new System.Drawing.Point(276, 72);
            this.lblHosName.Name = "lblHosName";
            this.lblHosName.Size = new System.Drawing.Size(105, 14);
            this.lblHosName.TabIndex = 154;
            this.lblHosName.Text = "东莞市人民医院";
            // 
            // pnlPCC
            // 
            this.pnlPCC.Controls.Add(this.panel7);
            this.pnlPCC.Controls.Add(this.lblPCCOrder);
            this.pnlPCC.Location = new System.Drawing.Point(215, 152);
            this.pnlPCC.Name = "pnlPCC";
            this.pnlPCC.Size = new System.Drawing.Size(506, 386);
            this.pnlPCC.TabIndex = 99;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label15);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 56);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(506, 330);
            this.panel7.TabIndex = 155;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(0, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(84, 14);
            this.label15.TabIndex = 155;
            this.label15.Text = "   以下空白";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.lblPCCTitleL);
            this.panel8.Controls.Add(this.lblPCCTitleR);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(506, 54);
            this.panel8.TabIndex = 155;
            // 
            // lblPCCTitleL
            // 
            this.lblPCCTitleL.AutoSize = true;
            this.lblPCCTitleL.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPCCTitleL.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPCCTitleL.ForeColor = System.Drawing.Color.Black;
            this.lblPCCTitleL.Location = new System.Drawing.Point(0, 0);
            this.lblPCCTitleL.Name = "lblPCCTitleL";
            this.lblPCCTitleL.Size = new System.Drawing.Size(192, 24);
            this.lblPCCTitleL.TabIndex = 153;
            this.lblPCCTitleL.Text = "--22222-------";
            // 
            // lblPCCTitleR
            // 
            this.lblPCCTitleR.AutoSize = true;
            this.lblPCCTitleR.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPCCTitleR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPCCTitleR.ForeColor = System.Drawing.Color.Black;
            this.lblPCCTitleR.Location = new System.Drawing.Point(401, 0);
            this.lblPCCTitleR.Name = "lblPCCTitleR";
            this.lblPCCTitleR.Size = new System.Drawing.Size(105, 14);
            this.lblPCCTitleR.TabIndex = 154;
            this.lblPCCTitleR.Text = "------33333---";
            // 
            // lblPCCOrder
            // 
            this.lblPCCOrder.AutoSize = true;
            this.lblPCCOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPCCOrder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPCCOrder.ForeColor = System.Drawing.Color.Black;
            this.lblPCCOrder.Location = new System.Drawing.Point(0, 0);
            this.lblPCCOrder.Name = "lblPCCOrder";
            this.lblPCCOrder.Size = new System.Drawing.Size(539, 56);
            this.lblPCCOrder.TabIndex = 152;
            this.lblPCCOrder.Text = "----11111-------------------------------------------------------------------\r\n\r\n\r" +
                "\n---------------\r\n";
            // 
            // pnlP
            // 
            this.pnlP.Controls.Add(this.lblOrder);
            this.pnlP.Location = new System.Drawing.Point(27, 252);
            this.pnlP.Name = "pnlP";
            this.pnlP.Size = new System.Drawing.Size(506, 386);
            this.pnlP.TabIndex = 153;
            // 
            // lblOrder
            // 
            this.lblOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOrder.ForeColor = System.Drawing.Color.Black;
            this.lblOrder.Location = new System.Drawing.Point(0, 0);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(506, 386);
            this.lblOrder.TabIndex = 98;
            this.lblOrder.Text = "--------44---------------------------------------------------------------";
            // 
            // lblWindow
            // 
            this.lblWindow.AutoSize = true;
            this.lblWindow.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblWindow.ForeColor = System.Drawing.Color.Black;
            this.lblWindow.Location = new System.Drawing.Point(386, 740);
            this.lblWindow.Name = "lblWindow";
            this.lblWindow.Size = new System.Drawing.Size(28, 14);
            this.lblWindow.TabIndex = 136;
            this.lblWindow.Text = "---";
            // 
            // lblRecipeNo
            // 
            this.lblRecipeNo.AutoSize = true;
            this.lblRecipeNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRecipeNo.ForeColor = System.Drawing.Color.Black;
            this.lblRecipeNo.Location = new System.Drawing.Point(464, 70);
            this.lblRecipeNo.Name = "lblRecipeNo";
            this.lblRecipeNo.Size = new System.Drawing.Size(56, 14);
            this.lblRecipeNo.TabIndex = 151;
            this.lblRecipeNo.Text = "No.0001";
            // 
            // lblAddess
            // 
            this.lblAddess.AutoSize = true;
            this.lblAddess.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAddess.ForeColor = System.Drawing.Color.Black;
            this.lblAddess.Location = new System.Drawing.Point(72, 184);
            this.lblAddess.Name = "lblAddess";
            this.lblAddess.Size = new System.Drawing.Size(63, 14);
            this.lblAddess.TabIndex = 150;
            this.lblAddess.Text = "广东东莞";
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTel.ForeColor = System.Drawing.Color.Black;
            this.lblTel.Location = new System.Drawing.Point(403, 184);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(28, 14);
            this.lblTel.TabIndex = 104;
            this.lblTel.Text = "138";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(365, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 149;
            this.label4.Text = "电话：";
            // 
            // lblHosCode
            // 
            this.lblHosCode.AutoSize = true;
            this.lblHosCode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHosCode.ForeColor = System.Drawing.Color.Black;
            this.lblHosCode.Location = new System.Drawing.Point(208, 72);
            this.lblHosCode.Name = "lblHosCode";
            this.lblHosCode.Size = new System.Drawing.Size(77, 14);
            this.lblHosCode.TabIndex = 147;
            this.lblHosCode.Text = "医院代码：";
            // 
            // lblMCardNo
            // 
            this.lblMCardNo.AutoSize = true;
            this.lblMCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMCardNo.ForeColor = System.Drawing.Color.Black;
            this.lblMCardNo.Location = new System.Drawing.Point(297, 98);
            this.lblMCardNo.Name = "lblMCardNo";
            this.lblMCardNo.Size = new System.Drawing.Size(28, 14);
            this.lblMCardNo.TabIndex = 146;
            this.lblMCardNo.Text = "111";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.ForeColor = System.Drawing.Color.Black;
            this.lblSex.Location = new System.Drawing.Point(209, 122);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(35, 14);
            this.lblSex.TabIndex = 145;
            this.lblSex.Text = "未知";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.ForeColor = System.Drawing.Color.Black;
            this.lblAge.Location = new System.Drawing.Point(285, 122);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(21, 14);
            this.lblAge.TabIndex = 141;
            this.lblAge.Text = "19";
            // 
            // lblSendWindow
            // 
            this.lblSendWindow.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblSendWindow.ForeColor = System.Drawing.Color.Black;
            this.lblSendWindow.Location = new System.Drawing.Point(425, 743);
            this.lblSendWindow.Name = "lblSendWindow";
            this.lblSendWindow.Size = new System.Drawing.Size(78, 12);
            this.lblSendWindow.TabIndex = 138;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.ForeColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(15, 734);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(548, 1);
            this.panel5.TabIndex = 137;
            // 
            // lblPhaMoney
            // 
            this.lblPhaMoney.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblPhaMoney.ForeColor = System.Drawing.Color.Black;
            this.lblPhaMoney.Location = new System.Drawing.Point(246, 690);
            this.lblPhaMoney.Name = "lblPhaMoney";
            this.lblPhaMoney.Size = new System.Drawing.Size(72, 14);
            this.lblPhaMoney.TabIndex = 134;
            this.lblPhaMoney.Text = "000.000";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(195, 688);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 14);
            this.label23.TabIndex = 135;
            this.label23.Text = "药品费：";
            // 
            // lblPringDate
            // 
            this.lblPringDate.AutoSize = true;
            this.lblPringDate.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblPringDate.ForeColor = System.Drawing.Color.Black;
            this.lblPringDate.Location = new System.Drawing.Point(23, 742);
            this.lblPringDate.Name = "lblPringDate";
            this.lblPringDate.Size = new System.Drawing.Size(77, 14);
            this.lblPringDate.TabIndex = 132;
            this.lblPringDate.Text = "打印时间：";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(392, 714);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(112, 14);
            this.label27.TabIndex = 133;
            this.label27.Text = "核对/发药药师：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(392, 689);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 14);
            this.label21.TabIndex = 131;
            this.label21.Text = "护士签名：";
            // 
            // lblPhaDoc
            // 
            this.lblPhaDoc.AutoSize = true;
            this.lblPhaDoc.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblPhaDoc.ForeColor = System.Drawing.Color.Black;
            this.lblPhaDoc.Location = new System.Drawing.Point(63, 689);
            this.lblPhaDoc.Name = "lblPhaDoc";
            this.lblPhaDoc.Size = new System.Drawing.Size(21, 14);
            this.lblPhaDoc.TabIndex = 52;
            this.lblPhaDoc.Text = "--";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(23, 688);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 14);
            this.label18.TabIndex = 130;
            this.label18.Text = "医师：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(450, 152);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 14);
            this.label9.TabIndex = 129;
            this.label9.Text = "体温：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(365, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 14);
            this.label11.TabIndex = 128;
            this.label11.Text = "体重：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(23, 714);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 14);
            this.label24.TabIndex = 27;
            this.label24.Text = "审核药师：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(69, 70);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 14);
            this.label16.TabIndex = 127;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(195, 714);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 14);
            this.label25.TabIndex = 28;
            this.label25.Text = "配剂药师/士：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(25, 72);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 14);
            this.label17.TabIndex = 126;
            this.label17.Text = "病历号：";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(20, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 32);
            this.label2.TabIndex = 123;
            this.label2.Text = "R";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(15, 683);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(548, 1);
            this.panel3.TabIndex = 121;
            // 
            // lblSeeYear
            // 
            this.lblSeeYear.AutoSize = true;
            this.lblSeeYear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSeeYear.ForeColor = System.Drawing.Color.Black;
            this.lblSeeYear.Location = new System.Drawing.Point(437, 99);
            this.lblSeeYear.Name = "lblSeeYear";
            this.lblSeeYear.Size = new System.Drawing.Size(77, 14);
            this.lblSeeYear.TabIndex = 109;
            this.lblSeeYear.Text = "2010-01-01";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(396, 99);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(49, 14);
            this.label31.TabIndex = 108;
            this.label31.Text = "日期：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(25, 99);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 14);
            this.label14.TabIndex = 107;
            this.label14.Text = "费别：";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(20, 211);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(548, 1);
            this.panel4.TabIndex = 97;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(25, 152);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 14);
            this.label19.TabIndex = 95;
            this.label19.Text = "临床诊断：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(251, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 91;
            this.label8.Text = "年龄：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(172, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 90;
            this.label7.Text = "性别：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(25, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 14);
            this.label6.TabIndex = 89;
            this.label6.Text = "姓名：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(396, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 88;
            this.label5.Text = "处方编号：";
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.BackColor = System.Drawing.Color.Black;
            this.panel6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel6.ForeColor = System.Drawing.Color.Black;
            this.panel6.Location = new System.Drawing.Point(18, 118);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(550, 1);
            this.panel6.TabIndex = 87;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(147, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(315, 32);
            this.lblTitle.TabIndex = 86;
            this.lblTitle.Text = "东莞市人民医院处方笺";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(180, 98);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 14);
            this.label12.TabIndex = 94;
            this.label12.Text = "医疗证号/医保号：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(365, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 14);
            this.label10.TabIndex = 93;
            this.label10.Text = "科室：";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(37, 217);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 32);
            this.label13.TabIndex = 124;
            this.label13.Text = "p";
            // 
            // lblDiag
            // 
            this.lblDiag.AutoSize = true;
            this.lblDiag.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiag.ForeColor = System.Drawing.Color.Black;
            this.lblDiag.Location = new System.Drawing.Point(101, 152);
            this.lblDiag.Name = "lblDiag";
            this.lblDiag.Size = new System.Drawing.Size(63, 14);
            this.lblDiag.TabIndex = 103;
            this.lblDiag.Text = "门诊诊断";
            // 
            // lblSeeDept
            // 
            this.lblSeeDept.AutoSize = true;
            this.lblSeeDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSeeDept.ForeColor = System.Drawing.Color.Black;
            this.lblSeeDept.Location = new System.Drawing.Point(408, 122);
            this.lblSeeDept.Name = "lblSeeDept";
            this.lblSeeDept.Size = new System.Drawing.Size(77, 14);
            this.lblSeeDept.TabIndex = 102;
            this.lblSeeDept.Text = "二门急诊科";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(71, 122);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 14);
            this.lblName.TabIndex = 101;
            this.lblName.Text = "张三";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(25, 184);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 14);
            this.label20.TabIndex = 96;
            this.label20.Text = "地址：";
            // 
            // ucRecipePrintNew
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ucRecipePrintNew";
            this.Size = new System.Drawing.Size(608, 765);
            this.Load += new System.EventHandler(this.ucNewRecipePrint_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlPCC.ResumeLayout(false);
            this.pnlPCC.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.pnlP.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        #region 变量

        //控制参数管理类
		Neusoft.HISFC.BizLogic.Manager.Constant cnst = new Neusoft.HISFC.BizLogic.Manager.Constant();
		Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();
		Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList fee = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();
		Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
		Neusoft.HISFC.BizLogic.Order.OutPatient.Order orderManager = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();
		Neusoft.HISFC.BizLogic.Registration.Register regManager = new Neusoft.HISFC.BizLogic.Registration.Register();
		Neusoft.HISFC.BizLogic.Pharmacy.DrugStore drugManager = new Neusoft.HISFC.BizLogic.Pharmacy.DrugStore();
		Neusoft.HISFC.BizLogic.Pharmacy.Item phaManager = new Neusoft.HISFC.BizLogic.Pharmacy.Item();
		Neusoft.HISFC.BizLogic.Fee.Interface myInterface = new Neusoft.HISFC.BizLogic.Fee.Interface();
		Neusoft.HISFC.Models.SIInterface.Compare CompareItem = new Neusoft.HISFC.Models.SIInterface.Compare();
        Neusoft.HISFC.BizProcess.Integrate.Fee feeMgr = new Neusoft.HISFC.BizProcess.Integrate.Fee();
		bool bJS = false;
        #endregion

        /// <summary>
		/// load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ucNewRecipePrint_Load(object sender, System.EventArgs e) 
		{
			try
			{
				this.lblTitle.Text = this.cnst.GetHospitalName();
			}
			catch
			{}
		}

		/// <summary>
		/// 设置患者信息
		/// </summary>
		private void SetPatientInfo()
		{
			if(this.myReg == null)
			{
				return;
			}

            //姓名
			this.lblName.Text = this.myReg.Name;
			
			if(this.myReg.Pact.PayKind.ID == "03")
			{
				try
				{
					Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pact = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
					Neusoft.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(this.myReg.Pact.ID);
				}
				catch
				{}
            }

            this.printer = this.myReg.User03;

            #region 年龄

            //this.lblAge.Text = this.feeMgr.GetAgeStr(this.myReg.Birthday);

            //TimeSpan span = new TimeSpan(this.myInterface.GetDateTimeFromSysDateTime().Ticks -this.myReg.Birthday.Ticks);
            //if(span.Days/365<=0)
            //{
            //    this.lblMonth.Text = System.Convert.ToString(span.Days/30);

            //    this.lblDay.Text = System.Convert.ToString(span.Days%30);
            //}
            //else
            //{
            //    this.lblAge.Text = System.Convert.ToString(span.Days/365);

            //    if(span.Days/365<14)
            //    {
            //        int diff = span.Days - (span.Days/365)*365;
            //        if(diff>30)
            //        {
            //            this.lblMonth.Text = System.Convert.ToString(diff/30);
            //        }
            //        this.lblDay.Text = System.Convert.ToString(diff%30);
            //    }
            //}

            #endregion

            //性别
            this.lblSex.Text = myReg.Sex.Name;
            //科室
			this.lblSeeDept.Text = this.myReg.DoctorInfo.Templet.Dept.Name;
            //医疗证号
			this.lblMCardNo.Text = this.myReg.SSN;
            //病历号
			this.lblCardNo.Text = this.myReg.PID.CardNO;
            //处方号
            this.lblRecipeNo.Text =this.recipeId.PadLeft(7, '0');

            #region 费别

            this.lblHosCode.Visible = this.myReg.Pact.PayKind.ID == "02" ? true : false;
            this.lblHosName.Visible = this.myReg.Pact.PayKind.ID == "02" ? true : false;

            if (this.myReg.Pact.PayKind.ID == "01")
			{
                this.lblFeeType.Text = "自费";
			}
			else if(this.myReg.Pact.PayKind.ID == "02")
            {
                this.lblFeeType.Text = "医保";
			}
            else if (this.myReg.Pact.PayKind.ID == "03")
            {
                this.lblFeeType.Text = "公费";
            }
            else
            {
                this.lblFeeType.Text = "其他";
            }
            #endregion

            //发药窗口
            lblWindow.Text = this.orderManager.ExecSqlReturnOne("SELECT (SELECT d.DEPT_NAME FROM COM_DEPARTMENT d WHERE d.DEPT_CODE=t.DRUG_DEPT_CODE)||'-'||f.T_NAME FROM PHA_STO_RECIPE t,pha_sto_terminal f WHERE t.SEND_TERMINAL=f.T_CODE AND t.RECIPE_NO='" + this.recipeId + "'");

            //打印时间
            this.lblPringDate.Text = "打印时间：" + this.orderManager.GetDateTimeFromSysDateTime().ToString();

            //地址
            if (this.myReg.CompanyName != null && this.myReg.CompanyName.Length > 0)
			{
                this.lblAddess.Text = this.myReg.CompanyName;
			}

            //电话
            this.lblTel.Text = this.myReg.PhoneBusiness;

            #region 诊断
            try
            {
                ArrayList alDiag = null;// this.diagManager.QueryOpsDiagnose(this.myReg.ID, "1");
                string diag = "";
                //先查主诊断  如果没有再查门诊诊断
                if (alDiag != null && alDiag.Count > 0)
                {
                    diag = (alDiag[0] as Neusoft.HISFC.Models.HealthRecord.Diagnose).DiagInfo.Name;
                }
                else if (alDiag.Count == 0)
                {
                    //alDiag = this.diagManager.QueryOpsDiagnose(this.myReg.ID, "10");
                    if (alDiag != null && alDiag.Count > 0)
                    {
                        diag = (alDiag[0] as Neusoft.HISFC.Models.HealthRecord.Diagnose).DiagInfo.Name;
                    }
                }
                this.lblDiag.Text = diag;
            }
            catch
            {
            }
            #endregion

        }

		/// <summary>
		/// 查询医嘱
		/// </summary>
		private void Query()
		{
			//this.lblSeeID.Text = this.seeID;
			//ArrayList al = this.orderManager.QueryOrderByRecipeNO(this.recipeId);

            //如果处方中没有药品，就不打印了
            //处方表
            ArrayList alDrug = null;// this.orderManager.QueryOrderByRecipeNOItemType(this.recipeId, true);
            if (alDrug == null)
            {
                this.isNeedPrint = false;
                MessageBox.Show("查询处方中药品信息失败：" + this.orderManager.Err);
                return;
            }

            //费用表
            DataSet ds=new DataSet();
            if (outPatientManager.ExecQuery("SELECT * FROM FIN_OPB_FEEDETAIL WHERE RECIPE_NO='" + this.recipeId + "' AND DRUG_FLAG='1' and COST_SOURCE='0'", ref ds) == -1)
            {
                this.isNeedPrint = false;
                MessageBox.Show("查询处方中药品信息失败：" + this.orderManager.Err);
                return;
            }
            if (ds == null || ds.Tables.Count < 0)
            {
                this.isNeedPrint = false;
                MessageBox.Show("查询处方中药品信息失败：" + this.orderManager.Err);
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                //MessageBox.Show("手工处方不打印，请在取药时向药房索取！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }


            if (alDrug.Count <= 0)
            {
                this.isNeedPrint = false;
                return;
            }

            this.isManualRecipe = false;
            this.isNeedPrint = true;
            this.isEmergency = false;

			if(this.alOrder.Count>0)
			{
				this.alOrder.Clear();
			}
			foreach(Neusoft.HISFC.Models.Order.OutPatient.Order order in alDrug)
			{
                if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
				{
					this.alOrder.Add(order);
				}
			}
			this.MakaLabel(this.alOrder);
		}


        ///<summary>
        /// 取处方内项目参考信息 (判断是否手工处方,药品天数,药品备注)
        /// </summary>
        private void getRecipeItemHash(string recipeNo)
        {
            this.recipeItem.Clear();
            if (string.IsNullOrEmpty(recipeNo))
            {
                return;
            }

            string querySql = "";
            if (this.drugManager.Sql.GetSql("DongGuan.Pharmacy.Outpatient.GetRecipeOtherMessage", ref querySql) == -1)
            {
                return;
            }

            try
            {
                querySql = string.Format(querySql, recipeNo);

                if (this.drugManager.ExecQuery(querySql) == -1)
                {
                    throw new Exception("SQL语句错误");
                }
                while (drugManager.Reader.Read())
                {
                    Neusoft.FrameWork.Models.NeuObject tempObj = new Neusoft.FrameWork.Models.NeuObject();
                    tempObj.ID = drugManager.Reader[0].ToString();
                    tempObj.Name = drugManager.Reader[1].ToString();
                    tempObj.User01 = drugManager.Reader[2].ToString();  //天数
                    tempObj.User02 = drugManager.Reader[3].ToString();
                    tempObj.User03 = drugManager.Reader[4].ToString();
                    //tempObj.Memo = drugStoreMgr.Reader[5].ToString();
                    this.recipeItem.Add(tempObj.ID, tempObj);
                }
            }
            catch (Exception ex)
            {
                drugManager.Err = "查询处方内项目信息出错!错误信息:" + ex.Message;
                drugManager.WriteErr();
                if (!drugManager.Reader.IsClosed)
                {
                    drugManager.Reader.Close();
                }
                return;
            }

        }


        /// <summary>
        /// 判断手工方、电子方
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        private bool GetRecipeSourceIsFeeOper(string recipeNO)
        {
            if (string.IsNullOrEmpty(recipeNO))
            {
                return false;
            }
            Neusoft.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new Neusoft.HISFC.BizLogic.Pharmacy.DrugStore();
            string SQL = "";
            if (drugStoreMgr.Sql.GetSql("DongGuan.Pharmacy.Outpatien.GetRecipeSource", ref SQL) == -1)
            {
                return false;
            }
            string parm = drugStoreMgr.ExecSqlReturnOne(string.Format(SQL, recipeNO));
            if (parm == "-1")
            {
                return false;
            }
            if (parm != "1")
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 药房打印查询
        /// </summary>
        private void QueryPharmacy()
        {

            ArrayList alDrug = null;// this.orderManager.QueryOrderByRecipeNOItemType(this.recipeId, true);
            isEmergency = false;
            if (GetRecipeSourceIsFeeOper(this.recipeId))
            {
                isManualRecipe = true;
            }
            else
            {
                isManualRecipe = false;
                foreach (object drug in alDrug)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = drug as Neusoft.HISFC.Models.Order.OutPatient.Order;
                    if (order.IsEmergency)
                    {
                        this.isEmergency = true;
                        break;
                    }
 
                }
            }

            alDrug = TransformApplyDrugToOrder(applyList);
            this.applyList.Clear();

            this.isNeedPrint = true;

            if (this.alOrder.Count > 0)
            {
                this.alOrder.Clear();
            }
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alDrug)
            {
                if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.alOrder.Add(order);
                }
            }
            
            this.MakaLabel(this.alOrder);
        }

		/// <summary>
		/// 生成显示信息
		/// </summary>
		/// <param name="alOrder"></param>
		private void MakaLabel(ArrayList alOrder)
		{
			if(alOrder == null)
			{
				return;
			}
			for(int i=0;i<alOrder.Count;i++)
			{
				this.alTemp.Add(alOrder[i]);
			}
			System.Text.StringBuilder buffer = new System.Text.StringBuilder();
			//填充数据
			string[] parm = {"  ","┌","│","└"};
   
			decimal phaMoney = 0m;//药费

			string drugType = "";

			# region /*画组合符号*/
			for(int i=0;i<alOrder.Count;i++) 
			{
				Neusoft.HISFC.Models.Order.OutPatient.Order order = alOrder[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;

				if(order == null) 
				{
					continue;
				}
				if(i==0)
				{
					drugType = order.Item.SysClass.ID.ToString();
                    this.lblRecipeNo.Text =order.ReciptNO.PadLeft(7, '0');
                    if (!String.IsNullOrEmpty(order.ReciptDoctor.Name))
                    {
                        this.lblPhaDoc.Text = order.ReciptDoctor.Name + "/" + order.ReciptDoctor.ID.Substring(2);
                    }
                    else 
                    {
                        this.lblPhaDoc.Text = String.Empty;
                    }
					this.lblSeeYear.Text = order.MOTime.Date.ToString("yyyy-MM-dd");
                    //this.lblSeeMonth.Text = order.MOTime.Date.ToString("MM");
                    //this.lblSeeDay.Text = order.MOTime.Date.ToString("dd");
					this.myReg = this.regManager.GetByClinic(order.Patient.ID);
					this.SetPatientInfo();
				}
				ArrayList al = this.GetOrderByCombId(order);
				if(al.Count == 0) 
				{
					continue;
				}
				else 
				{
					#region 形成字符串

                    #region 草药
                    if (drugType == "PCC")
					{
                        //动态显示不同的pnl
                        this.pnlP.Visible = false;
                        this.pnlPCC.Location = new Point(36, 252);
                        this.pnlPCC.Visible = true;

						decimal days = 1m;
						string  freq = "";
						string  usage = "";

                        for (int k = 0; k < al.Count; k++)
                        {
                            order = al[k] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                            phaMoney += order.FT.OwnCost;

                            //付数
                            days = order.HerbalQty;
                            //频次
                            freq = order.Frequency.Name;
                            //用法
                            usage = order.Usage.Name;

                            System.Text.StringBuilder buff = new System.Text.StringBuilder();

                            if (order.Memo != "")
                            {
                                buff.Append(order.Item.Name + "(" + order.Memo + ")");
                            }
                            else
                            {
                                buff.Append(order.Item.Name);
                            }

                            buff.Append("  ");
                            
                            string name = buff.ToString();
                            string dosestr = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;

                            try
                            {
                                int len = System.Text.Encoding.GetEncoding("gb2312").GetBytes(name).Length;
                                string pad = "";
                                if (len > 23)
                                {
                                    len = 23;
                                    pad = new string(' ', 23 - len);
                                }
                                else
                                {
                                    pad = new string(' ', 23 - len);
                                }
                                buffer.Append(name + pad);

                                len = System.Text.Encoding.GetEncoding("gb2312").GetBytes(dosestr).Length;
                                if (len > 6)
                                {
                                    len = 6;
                                    pad = new string(' ', 6 - len);
                                }
                                else
                                {
                                    pad = new string(' ', 6 - len);
                                }
                                buffer.Append(dosestr + pad);
                                buffer.Append("    ");
                            }
                            catch { }

                            /*----------------*/

                            if ((k + 1) % 2 == 0 || k == al.Count - 1)
                            {
                                buffer.Append("\n");
                                buffer.Append("\n");
                            }
                        }

                        //buffer.Append("             ");
                        //buffer.Append(days.ToString());
                        //buffer.Append("剂");
                        //buffer.Append("\n");
                        //buffer.Append("       用法: ");
                        buffer.Append("\n");

                        if (!isManualRecipe)
                        {
                            #region 剂数信息显示
                            System.Text.StringBuilder buffPCCTitleR = new System.Text.StringBuilder();
                            freq = "每日" + this.GetFrequencyCount(order.Frequency.ID).ToString() + "剂";
                            buffPCCTitleR.Append(freq.PadLeft(35, ' '));
                            buffPCCTitleR.Append("\n");
                            buffPCCTitleR.Append(usage.PadLeft(35, ' '));
                            this.lblPCCTitleR.Text = buffPCCTitleR.ToString();

                            //buffer.Append("\n");
                            //buffer.Append("以下空白");
                            #endregion
                        }
                        else 
                        {
                            this.lblPCCTitleR.Text = String.Empty;
                        }
                        

                        this.lblPCCOrder.Text = buffer.ToString();
                        this.lblPCCTitleL.Location = new Point(this.lblPCCOrder.Location.X, this.lblPCCOrder.Location.Y + this.lblPCCOrder.Size.Height - 30);
                        string dose = FrameWork.Public.String.LowerMoneyToUpper(days);
                        this.lblPCCTitleL.Text = dose.Substring(0, dose.Length - 2).PadLeft(25, ' ') + "剂";
                    }
                    #endregion

                    #region 西药、成药
                    else
                    {
                        //动态显示不同的pnl
                        this.pnlP.Visible = true;
                        this.pnlPCC.Visible = false;


						for(int k=0;k<al.Count;k++) 
						{
							order = al[k] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                            phaMoney += order.FT.OwnCost;
							string combLabel = "";

                            #region 组合符号
                            if (al.Count==1)
							{
								buffer.Append(parm[0]);

								combLabel = parm[0];
							}
							else
							{
								if(k==0)
								{
									buffer.Append(parm[1]);

									combLabel = parm[2];
								}
								else if(k==al.Count -1)
								{
									buffer.Append(parm[2]);

									combLabel = parm[3];
								}
								else
								{
									buffer.Append(parm[2]);

									combLabel = parm[2];
								}
                            }

                       
                            #endregion

                            Neusoft.HISFC.Models.Pharmacy.Item phaItem = phaManager.GetItem(order.Item.ID);

							if(phaItem != null)
							{
								buffer.Append(phaItem.NameCollection.Name);
								if(phaItem.Quality.ID == "P2")
								{
									this.bJS = true;
								}
							}
							else
							{
								buffer.Append(order.Item.Name);
							}
							buffer.Append(" ");

                           
                            int splitIndex = order.Item.Specs.IndexOf("*", 0);
                            if (splitIndex >1)
                            {
                                buffer.Append(order.Item.Specs.Split('*')[0]);
                            }
                            else 
                            {
                                buffer.Append(phaItem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaItem.DoseUnit);
                            }
                            buffer.Append("×");
                            buffer.Append(order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.'));
                            buffer.Append(order.Unit);

                            buffer.Append("\n");

                            if (!isManualRecipe)
                            {
                                #region 显示用法
                                buffer.Append(combLabel);
                                buffer.Append("    Sig.");
                                buffer.Append(" ");

                                buffer.Append(order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.'));
                                buffer.Append(order.DoseUnit);

                                buffer.Append(" ");

                                buffer.Append(order.Usage.Name);
                                buffer.Append(" ");

                                if ((order.Frequency.Name == null || order.Frequency.Name.Length <= 0) && this.freHelper != null)
                                {
                                    Neusoft.FrameWork.Models.NeuObject objFre = this.freHelper.GetObjectFromID(order.Frequency.ID);
                                    if (objFre != null)
                                        buffer.Append(objFre.ID.ToLower() + "  (" + order.HerbalQty + "天)");
                                }
                                else
                                {
                                    buffer.Append(order.Frequency.ID.ToLower() + "  (" + order.HerbalQty + "天)");
                                }
                                buffer.Append(" ");

                                buffer.Append(order.Memo);

                                if (order.InjectCount > 0)
                                {
                                    buffer.Append("           院注:");
                                    buffer.Append(order.InjectCount.ToString() + "次");
                                }

                                buffer.Append("\n");
                                #endregion
                            }
                            
                            buffer.Append("\n");
						}
                    }

                    #endregion

                    #endregion
                }
			}
			# endregion

            buffer.Append("以下空白");
            this.lblOrder.Text = buffer.ToString();

			this.lblPhaMoney.Text = phaMoney.ToString();			
		}

        /// <summary>
        /// 根据频次获得每天剂数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
        {
            if (string.IsNullOrEmpty(frequencyID))
            {
                return -1;
            }
            string id = frequencyID.ToLower();
            if (id == "qd")//每天一次
            {
                return 1;
            }
            else if (id == "bid")//每天两次
            {
                return 2;
            }
            else if (id == "tid")//每天三次
            {
                return 3;
            }
            else if (id == "hs")//睡前
            {
                return 1;
            }
            else if (id == "qn")//每晚一次
            {
                return 1;
            }
            else if (id == "qid")//每天四次
            {
                return 4;
            }
            else if (id == "pcd")//晚餐后
            {
                return 1;
            }
            else if (id == "pcl")//午餐后
            {
                return 1;
            }
            else if (id == "pcm")//早餐后
            {
                return 1;
            }
            else if (id == "prn")//必要时服用
            {
                return 1;
            }
            else if (id == "遵医嘱")
            {
                return 1;
            }
            else
            {
                Neusoft.HISFC.BizLogic.Manager.Frequency frequencyManagement = new Neusoft.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID);
                if (alFrequency != null || alFrequency.Count > 0)
                {
                    Neusoft.HISFC.Models.Order.Frequency obj = alFrequency[0] as Neusoft.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');
                    return str.Length;
                }
                return -1;
            }
        }

		/// <summary>
		/// 得到组合处方的组合显示
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		private ArrayList GetOrderByCombId(Neusoft.HISFC.Models.Order.OutPatient.Order order) 
		{
			ArrayList al = new ArrayList();

			for(int i=0;i<this.alTemp.Count;i++) 
			{
				Neusoft.HISFC.Models.Order.OutPatient.Order ord = alTemp[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;
				if(order.Combo.ID == ord.Combo.ID&&ord.Combo.ID!=string.Empty) 
				{
					al.Add(ord);
					//alTemp.Remove(ord);
				}
			}

			for(int j=0;j<al.Count;j++) 
			{
				alTemp.Remove(al[j]);	 
			}
			return al;
		}

		/// <summary>
		/// print the recipe
		/// </summary>
		public void Print()
		{
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            Neusoft.HISFC.BizLogic.Manager.PageSize pzMgr=new Neusoft.HISFC.BizLogic.Manager.PageSize();
            Neusoft.HISFC.Models.Base.PageSize pSize = null;
            pSize = pzMgr.GetPageSize("MZRecipe");
            if (pSize != null)
            {
                print.SetPageSize(pSize);
            }
			print.PrintPage(0,0,this);
		}

		/// <summary>
		/// print the recipe
		/// </summary>
		public void Print1()
		{
			lblTitle.Visible = false;
			label2.Visible = false;
			lblWindow.Visible = false;
			label5.Visible = false;
			label6.Visible = false;
			label7.Visible = false;
			label8.Visible = false;
			label9.Visible = false;
			label10.Visible = false;
			label12.Visible = false;
			label13.Visible = false;
			label19.Visible = false;
			label20.Visible = false;
            //label22.Visible = false;
			label24.Visible = false;
			label25.Visible = false;
			label27.Visible = false;
			label14.Visible = false;
			label31.Visible = false;
            //label33.Visible = false;
			label4.Visible = false;
			label11.Visible = false;
			this.panel3.Visible = false;
			this.panel3.Visible = false;
			this.panel4.Visible = false;
			this.panel6.Visible = false;

            if (this.myReg.DoctorInfo.Templet.Dept.Name.IndexOf("儿") >= 0)
            {
                this.panel1.Height = 33;
            }

            if (this.myReg.DoctorInfo.Templet.Dept.Name.IndexOf("急") >= 0)
            {
                this.panel1.Height = 33;
            }

			if(bJS)
			{
                this.panel1.Height = 33;
			}

            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
			System.Drawing.Printing.PaperSize psize = new System.Drawing.Printing.PaperSize("Recipe",787,1200);
			print.SetPageSize(psize);
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
			
			#region 横打
//			System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
//			pSet.Landscape = true;
//			print.PrintDocument.DefaultPageSettings.Landscape = true;
//          print.PrintPage(560,45,this);
			#endregion

			print.PrintPage(1,40,this);
		}

		/// <summary>
		/// print the recipe
		/// </summary>
		public void Print2()
		{
            if (!this.isNeedPrint)
            {
                return;
            }

			if(this.myReg.DoctorInfo.Templet.Dept.Name.IndexOf("儿")>=0)
			{
                //this.lblMonth.Visible = true;
                //this.lblDay.Visible = true;
			}
			else
			{
                //this.lblMonth.Visible = false;
                //this.lblDay.Visible = false;
			}

            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
			System.Drawing.Printing.PaperSize psize = new System.Drawing.Printing.PaperSize("Recipe",1200,1200);
			print.SetPageSize(psize);

			System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
            //pSet.Landscape = true;

            //print.PrintDocument.DefaultPageSettings.Landscape = true;
			print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            try
            {
                //普济分院4号窗口自动打印总是暂停：打印机针头或纸张太薄或太厚都可能引起暂停
                string errInfo = "";
                ArrayList al = (Neusoft.FrameWork.WinForms.Classes.Function.GetDefaultValue("DrugStore", "ResumePrintJob", out errInfo));
                if (al == null || al.Count == 0)
                {
                     Neusoft.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                }
                else
                {
                    string valueInfo = al[0].ToString();
                    if (valueInfo == "1")
                    {
                        Neusoft.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                    }
                }

            }
            catch
            {

            }
            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.PrinterSettings.PrinterName = printer;
            }
			print.PrintPage(90,0,this);
		}

        /// <summary>
        /// print the recipe
        /// </summary>
        public void DrugStorePrint()
        {
            if (!this.isNeedPrint)
            {
                return;
            }

            if (this.myReg.DoctorInfo.Templet.Dept.Name.IndexOf("儿") >= 0)
            {
                //this.lblMonth.Visible = true;
                //this.lblDay.Visible = true;
            }
            else
            {
                //this.lblMonth.Visible = false;
                //this.lblDay.Visible = false;
            }

            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            System.Drawing.Printing.PaperSize psize = new System.Drawing.Printing.PaperSize("Recipe", 1200, 1200);
            print.SetPageSize(psize);

            System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
            //pSet.Landscape = true;

            //print.PrintDocument.DefaultPageSettings.Landscape = true;
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            try
            {
                //普济分院4号窗口自动打印总是暂停：打印机针头或纸张太薄或太厚都可能引起暂停
                string errInfo = "";
                ArrayList al = this.GetDefaultValue("DrugStore", "ResumePrintJob", out errInfo);
                //MessageBox.Show(al.Count.ToString());
                if (al == null || al.Count == 0)
                {
                    Neusoft.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                }
                else
                {
                    string valueInfo = al[0].ToString();
                    if (valueInfo == "1")
                    {
                        Neusoft.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                    }
                }

            }
            catch
            {

            }

            //如果只有以下空白这几个字就不再打印了
            if (this.lblOrder.Text.IndexOf("以下空白") >= 0 && this.lblOrder.Text.Length <= 5)
            {
                return;
            }

            //if (!string.IsNullOrEmpty(printer))
            //{
            //    print.PrintDocument.PrinterSettings.PrinterName = printer;
            //}
            if (Neusoft.FrameWork.Management.Connection.Operator.ID == "009999")
            {
                print.PrintPreview(90, 0, this);
            }
            else
            {
                print.PrintPage(90, 0, this);
            }
        }


		/// <summary>
		/// 从消耗品和医嘱数组中移除医嘱
		/// </summary>
		/// <param name="alOrder"></param>
		/// <param name="alOrderAndSub"></param>
		private void RemoveOrderFromArray(ArrayList alOrder,ref ArrayList alOrderAndSub)
		{
			if(alOrder == null||alOrder.Count==0)
			{
				return;
			}
			if(alOrderAndSub == null||alOrderAndSub.Count==0)
			{
				return;
			}
			ArrayList alTemp = new ArrayList();
			for(int i=0;i<alOrderAndSub.Count;i++)
			{
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item = alOrderAndSub[i] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
				for(int j=0;j<alOrder.Count;j++)
				{
					Neusoft.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                    if ((temp.ReciptNO == item.RecipeNO) && (temp.ReciptSequence == item.RecipeSequence))
					{
						item.Item.MinFee.User03 = "1";
					}
				}
			}
			foreach(Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item in alOrderAndSub)
			{
				if(item.Item.MinFee.User03 != "1")
				{
					alTemp.Add(item);
				}
			}
			alOrderAndSub = alTemp;
		}


        public string RecipeNO
        {
            get
            {
                return this.recipeId;//throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                this.recipeId = value;
                this.Query();
            }
        }

        private void mySetPatientInfo(Neusoft.HISFC.Models.Registration.Register register)

        {
            this.myReg = register;

            if (this.myReg == null)
            {
                return;
            }
            this.lblName.Text = this.myReg.Name;

            /*
            if (this.myReg.Pact.PayKind.ID == "03")
            {
                try
                {
                    Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pact = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
                    Neusoft.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(this.myReg.Pact.ID);
                }
                catch
                { }
            }
            */


            this.printer = this.myReg.User03;

            #region 年龄

            //this.lblAge.Text = this.feeMgr.GetAgeStr(this.myReg.Birthday);
            #endregion

            //科室
            this.lblSeeDept.Text = this.myReg.DoctorInfo.Templet.Dept.Name;
            //医疗证号
            this.lblMCardNo.Text = this.myReg.SSN;
            //病历号
            this.lblCardNo.Text = this.myReg.PID.CardNO;

            #region 费别


            if (this.myReg.Pact.PayKind.ID == "01")
            {
                this.lblFeeType.Text = "自费";
            }
            else if (this.myReg.Pact.PayKind.ID == "02")
            {
                this.lblFeeType.Text = "医保";
            }
            else if (this.myReg.Pact.PayKind.ID == "03")
            {
                this.lblFeeType.Text = "公费";
            }
            else
            {
                this.lblFeeType.Text = "其他";
            }

            //从处方表里取诊断、特定门诊
            string pact = this.myReg.Pact.Name.Contains("特定") ? "1" : "0";

            string happenNO = "";
            if (pact == "1")
            {
                DataSet pactSet = new DataSet();
                this.orderManager.ExecQuery(string.Format("SELECT t.ISSPEORDER,t.HAPPEN_NO FROM MET_ORD_RECIPEDETAIL_EXTEND t WHERE t.CLINIC_NO='{0}' AND t.SEE_NO=(SELECT max(SEE_NO) FROM MET_ORD_RECIPEDETAIL WHERE RECIPE_NO='{1}') ", this.myReg.ID, this.recipeId), ref pactSet);
                if (pactSet.Tables.Count > 0)
                {
                    if (pactSet.Tables[0].Rows.Count > 0)
                    {
                        pact = pactSet.Tables[0].Rows[0][0].ToString();
                        happenNO = pactSet.Tables[0].Rows[0][1].ToString();
                    }
                }
            }

            if (pact=="1")
            {
                this.lblTitle.Text = "东莞市社会保险门诊专用处方笺";

                this.lblFeeType.Text = "医保";
                this.lblHosCode.Visible = true;
                this.lblHosName.Visible = true;

            }
            else
            {
                this.lblTitle.Text = "东莞市人民医院处方笺";
                this.lblHosCode.Visible = false;
                this.lblHosName.Visible = false;
            }
            #endregion


            #region 诊断
            try
            {
                                
                string diag = "";
                if (happenNO != "")
                {
                    string sql = string.Format("SELECT DIAG_NAME FROM  MET_COM_DIAGNOSE WHERE HAPPEN_NO={0}", happenNO);
                    diag = this.orderManager.ExecSqlReturnOne(sql, "");
                }
                if(diag==""||diag=="-1")
                {
                    ArrayList alDiag =null;// this.diagManager.QueryOpsDiagnose(this.myReg.ID, "1");
                    //先查主诊断  如果没有再查门诊诊断
                    if (alDiag != null && alDiag.Count > 0)
                    {
                        diag = (alDiag[0] as Neusoft.HISFC.Models.HealthRecord.Diagnose).DiagInfo.Name;
                    }
                    else if (alDiag.Count == 0)
                    {
                        //alDiag = this.diagManager.QueryOpsDiagnose(this.myReg.ID, "10");
                        if (alDiag != null && alDiag.Count > 0)
                 
                        {
                            diag = (alDiag[0] as Neusoft.HISFC.Models.HealthRecord.Diagnose).DiagInfo.Name;
                        }
                    }
                }
                             
                this.lblDiag.Text = diag;
            }
            catch
            {

            }
            #endregion


            //发药窗口
            lblWindow.Text = this.orderManager.ExecSqlReturnOne("SELECT (SELECT d.DEPT_NAME FROM COM_DEPARTMENT d WHERE d.DEPT_CODE=t.DRUG_DEPT_CODE)||'-'||f.T_NAME FROM PHA_STO_RECIPE t,pha_sto_terminal f WHERE t.SEND_TERMINAL=f.T_CODE AND t.RECIPE_NO='" + this.recipeId + "'");

            //打印时间
            this.lblPringDate.Text = "打印时间："+this.orderManager.GetDateTimeFromSysDateTime().ToString();

            //地址
            if (this.myReg.CompanyName != null && this.myReg.CompanyName.Length > 0)
            {
                this.lblAddess.Text = this.myReg.CompanyName;
            }

            //电话
            this.lblTel.Text = this.myReg.PhoneBusiness;


            if (this.isReprint)
            {
                this.lblReprint.Text = "处方补打";
            }
            else 
            {
                this.lblReprint.Text = string.Empty;
            }
            if (this.isEmergency)
            {
                this.lblEmergency.Text = "急";
            }
            else 
            {
                this.lblEmergency.Text = "";
            }
        }


        /// <summary>
        /// 转换药品摆药信息到医嘱实体
        /// </summary>
        /// <param name="al"></param>
        private ArrayList TransformApplyDrugToOrder(ArrayList al)
        {
            ArrayList ordeList = new ArrayList();
            int i = 0;
            DateTime applyTime=DateTime.Now;
            HISFC.Models.Base.Employee employ = new Neusoft.HISFC.Models.Base.Employee();

            
            if(al.Count>0)
            {

                Neusoft.HISFC.Models.Pharmacy.ApplyOut applyInfo = phaManager.GetApplyOutByID(((Neusoft.HISFC.Models.Pharmacy.ApplyOut)al[0]).ID);
                applyTime=applyInfo.Operation.ApplyOper.OperTime;
                employ=new HISFC.BizProcess.Integrate.Manager().GetEmployeeInfo(applyInfo.RecipeInfo.ID);
            }

            foreach (object a in al)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                Neusoft.HISFC.Models.Pharmacy.ApplyOut apply = a as Neusoft.HISFC.Models.Pharmacy.ApplyOut;

                if (apply.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug&&apply.ValidState==HISFC.Models.Base.EnumValidState.Valid)
                {

                    Neusoft.FrameWork.Models.NeuObject tempObj=null;
                    if (recipeItem.Contains(apply.OrderNO))
                    {
                        tempObj = recipeItem[apply.OrderNO] as Neusoft.FrameWork.Models.NeuObject;
                    }
                    if (tempObj != null)
                    {
                        order.HerbalQty = tempObj == null ? apply.Days : Neusoft.FrameWork.Function.NConvert.ToDecimal(tempObj.User01);
                        if (tempObj.User02.Split('|').Length > 1)
                        {
                            order.Memo = tempObj.User02.Split('|')[1];
                            order.InjectCount = Neusoft.FrameWork.Function.NConvert.ToInt32(tempObj.User02.Split('|')[0].Replace("院注","").Replace("次",""));
                        }
                        else 
                        {
                            order.Memo = tempObj.User02;
                            order.InjectCount = 0;
                        }
                    }
                    order.Item = apply.Item;
                    order.ReciptNO = apply.RecipeNO;
                    order.Patient.ID = apply.PatientNO;
                    order.FT.OwnCost =Math.Round((apply.Item.PriceCollection.RetailPrice / apply.Item.PackQty) * (apply.Operation.ApplyQty * apply.Days),2);
                    //order.HerbalQty = apply.Days;
                    order.ExeDept.ID = apply.StockDept.ID;
                    order.Frequency = apply.Frequency;
                    order.Usage = apply.Usage;
                    order.DoseOnce = apply.DoseOnce;
                    order.DoseUnit = apply.Item.DoseUnit;
                    order.Qty = apply.Operation.ApplyQty;
                    order.Unit = apply.Item.MinUnit;
                    //order.InjectCount = apply.InjectQty;
                    if (apply.Item.Type.ID == "C")
                    {
                        if (isManualRecipe)
                        {
                            order.Combo.ID = "1";
                        }
                        else 
                        {
                            order.Combo.ID = apply.CombNO;
                        }
                        order.Item.SysClass.ID = "PCC";
                    }
                    else
                    {
                        if (isManualRecipe)
                        {
                            order.Combo.ID = i.ToString();
                        }
                        else 
                        {
                            order.Combo.ID = apply.CombNO;
                        }
                        order.Item.SysClass.ID = "P";
                    }

                    order.MOTime=applyTime;
                    order.ReciptDoctor.ID=employ.ID;
                    order.ReciptDoctor.Name=employ.Name;
                    order.Item.MinFee.User01 = apply.PrintState;
                    ordeList.Add(order);
                    i = i + 1;
                }
            }
            return ordeList;
        }


        #region IDrugPrint 成员

        public void AddAllData(ArrayList al, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddAllData(ArrayList al, Neusoft.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddAllData(ArrayList al)
        {
            //this.RecipeNO = ((Neusoft.HISFC.Models.Pharmacy.ApplyOut)al[0]).RecipeNO;
            if (al != null && al.Count > 0)
            {
                this.recipeId = ((Neusoft.HISFC.Models.Pharmacy.ApplyOut)al[0]).RecipeNO;
                this.isReprint = ((Neusoft.HISFC.Models.Pharmacy.ApplyOut)al[0]).PrintState == "1" ? true : false;
                this.getRecipeItemHash(this.recipeId);
                this.applyList = al;
                this.QueryPharmacy();

                if (this.outpatientInfo.MedicalType == "1")
                {
                    this.outpatientInfo.Pact.Name = "特定门诊";
                }

                this.mySetPatientInfo((Neusoft.HISFC.Models.Registration.Register)this.outpatientInfo);
            }

            //this.Print2();
        }

        public void AddCombo(ArrayList alCombo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddSingle(Neusoft.HISFC.Models.Pharmacy.ApplyOut info)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public decimal DrugTotNum
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public Neusoft.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public decimal LabelTotNum
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        Neusoft.HISFC.Models.Registration.Register outpatientInfo;
        public Neusoft.HISFC.Models.Registration.Register OutpatientInfo
        {
            get
            {                return outpatientInfo;
            }
            set
            {
                outpatientInfo = value;
            }
        }

        public void Preview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //void Neusoft.HISFC.BizProcess.Integrate.PharmacyInterface.IDrugPrint.Print()
        //{
        //    //this.Print2();
        //    this.DrugStorePrint();
        //}

        /// <summary>
        /// 获取程序运行默认配置
        /// </summary>
        /// <param name="GroupID">组编码</param>
        /// <param name="FunctionID">功能编码</param>
        /// <param name="strErr">错误信息</param>
        /// <returns>成功返回string数组 未找到返回空ArrayList 失败返回null</returns>
        private ArrayList GetDefaultValue(string GroupID, string FunctionID, out string strErr)
        {
            strErr = "";
            ArrayList al = new ArrayList();
            try
            {
                if (System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "\\HISDefaultValue.xml"))
                {
                    //加载Xml文件
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    //读取文件
                    using (System.IO.StreamReader rs = new System.IO.StreamReader(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "\\HISDefaultValue.xml"))
                    {
                        string cleandown = rs.ReadToEnd();
                        doc.LoadXml(cleandown);
                    }
                    System.Xml.XmlElement funElement = (System.Xml.XmlElement)doc.SelectSingleNode(string.Format("/PersonalConfig/Group[@ID='{0}']/Function[@ID='{1}']", GroupID, FunctionID));
                    if (funElement != null)         //功能节点存在
                    {
                        foreach (System.Xml.XmlNode valueNode in funElement.ChildNodes)
                        {
                            al.Add(valueNode.InnerText);
                        }
                    }
                }
                else
                {
                    strErr = "配置文件不存在 请与管理员联系";
                }
                return al;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion



        #region IRecipePrint 成员

        public void PrintRecipe()
        {
            throw new NotImplementedException();
        }

        public void SetPatientInfo(Neusoft.HISFC.Models.Registration.Register register)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IRecipePrint 成员


        public void PrintRecipeView()
        {
            return;
        }

        #endregion
    }
}
