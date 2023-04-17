using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace SOC.Fee.DayBalance.InpatientPrepay.GYSY
{
	/// <summary>
	/// ucPrepayDayByOP 的摘要说明。
	/// </summary>
	public class ucPrepayDayByOP : System.Windows.Forms.UserControl
	{
		internal System.Windows.Forms.Label lbOpsRoom;
		internal System.Windows.Forms.Label lb4;
		internal System.Windows.Forms.Label lb3;
		internal System.Windows.Forms.Label lbTitle;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label21;
		public System.Windows.Forms.Label labOpsDate;
		public System.Windows.Forms.Label labPrintDate;
		private System.Windows.Forms.Label lblOPerName;
		internal System.Windows.Forms.Label label30;
		internal System.Windows.Forms.Label label31;
		internal System.Windows.Forms.Label label32;
		internal System.Windows.Forms.Label label33;
		internal System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		internal System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.Label label10;
		internal System.Windows.Forms.Label label4;
		internal System.Windows.Forms.Label lb10;
		internal System.Windows.Forms.Label lbDiagnose;
		internal System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label lblTotPO;
		private System.Windows.Forms.Label labeee;
		private System.Windows.Forms.Label labTotal;
		private System.Windows.Forms.Label labTotalHis;
		private System.Windows.Forms.Label labTotalPay;
		public System.Windows.Forms.Label labCard;
		private System.Windows.Forms.Label labOtherPay;
		public System.Windows.Forms.Label labPay;
		private System.Windows.Forms.TextBox txtWasteNo;
		private System.Windows.Forms.TextBox txtRealNo;
		private System.Windows.Forms.Label lblBillSpan;
		private System.Windows.Forms.Label labOtherCard;
		private System.Windows.Forms.Label labOtherCheck;
		private System.Windows.Forms.Label lblOtherPO;
		private System.Windows.Forms.Label labTotalCheck;
		private System.Windows.Forms.Label labTotalCard;
		private System.Windows.Forms.Label lblPO;
		private System.Windows.Forms.Label labOtherTotal;
		private System.Windows.Forms.Label lblTotalNum;
		private System.Windows.Forms.Label lblWasteNum;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
	
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucPrepayDayByOP()
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
            this.lbOpsRoom = new System.Windows.Forms.Label();
            this.lb4 = new System.Windows.Forms.Label();
            this.labOpsDate = new System.Windows.Forms.Label();
            this.lb3 = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.labPrintDate = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblOPerName = new System.Windows.Forms.Label();
            this.lblTotPO = new System.Windows.Forms.Label();
            this.lblPO = new System.Windows.Forms.Label();
            this.labeee = new System.Windows.Forms.Label();
            this.labTotal = new System.Windows.Forms.Label();
            this.labTotalHis = new System.Windows.Forms.Label();
            this.labTotalCheck = new System.Windows.Forms.Label();
            this.labTotalCard = new System.Windows.Forms.Label();
            this.labTotalPay = new System.Windows.Forms.Label();
            this.labCard = new System.Windows.Forms.Label();
            this.labOtherPay = new System.Windows.Forms.Label();
            this.labPay = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtWasteNo = new System.Windows.Forms.TextBox();
            this.txtRealNo = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lblBillSpan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb10 = new System.Windows.Forms.Label();
            this.lbDiagnose = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labOtherCard = new System.Windows.Forms.Label();
            this.labOtherCheck = new System.Windows.Forms.Label();
            this.lblOtherPO = new System.Windows.Forms.Label();
            this.labOtherTotal = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTotalNum = new System.Windows.Forms.Label();
            this.lblWasteNum = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbOpsRoom
            // 
            this.lbOpsRoom.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOpsRoom.Location = new System.Drawing.Point(352, 493);
            this.lbOpsRoom.Name = "lbOpsRoom";
            this.lbOpsRoom.Size = new System.Drawing.Size(144, 19);
            this.lbOpsRoom.TabIndex = 101;
            this.lbOpsRoom.Text = "操作员:";
            this.lbOpsRoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb4
            // 
            this.lb4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb4.Location = new System.Drawing.Point(352, 64);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(80, 16);
            this.lb4.TabIndex = 100;
            this.lb4.Text = "打印日期";
            this.lb4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labOpsDate
            // 
            this.labOpsDate.Font = new System.Drawing.Font("宋体", 10F);
            this.labOpsDate.Location = new System.Drawing.Point(176, 64);
            this.labOpsDate.Name = "labOpsDate";
            this.labOpsDate.Size = new System.Drawing.Size(136, 16);
            this.labOpsDate.TabIndex = 99;
            this.labOpsDate.Text = "1900-01-01";
            this.labOpsDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb3
            // 
            this.lb3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb3.Location = new System.Drawing.Point(96, 64);
            this.lb3.Name = "lb3";
            this.lb3.Size = new System.Drawing.Size(72, 16);
            this.lb3.TabIndex = 98;
            this.lb3.Text = "报表日期";
            this.lb3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(296, 24);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(200, 24);
            this.lbTitle.TabIndex = 92;
            this.lbTitle.Text = "收款员预收日报";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labPrintDate
            // 
            this.labPrintDate.Font = new System.Drawing.Font("宋体", 10F);
            this.labPrintDate.Location = new System.Drawing.Point(432, 64);
            this.labPrintDate.Name = "labPrintDate";
            this.labPrintDate.Size = new System.Drawing.Size(144, 16);
            this.labPrintDate.TabIndex = 138;
            this.labPrintDate.Text = "1900-01-01";
            this.labPrintDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(248, 493);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(42, 14);
            this.label19.TabIndex = 146;
            this.label19.Text = "出纳:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(120, 493);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 14);
            this.label18.TabIndex = 147;
            this.label18.Text = "复核:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(496, 493);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(42, 14);
            this.label21.TabIndex = 149;
            this.label21.Text = "制单:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOPerName
            // 
            this.lblOPerName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOPerName.Location = new System.Drawing.Point(544, 493);
            this.lblOPerName.Name = "lblOPerName";
            this.lblOPerName.Size = new System.Drawing.Size(100, 19);
            this.lblOPerName.TabIndex = 154;
            this.lblOPerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotPO
            // 
            this.lblTotPO.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotPO.Location = new System.Drawing.Point(504, 176);
            this.lblTotPO.Name = "lblTotPO";
            this.lblTotPO.Size = new System.Drawing.Size(72, 23);
            this.lblTotPO.TabIndex = 200;
            this.lblTotPO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPO
            // 
            this.lblPO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblPO.Location = new System.Drawing.Point(504, 121);
            this.lblPO.Name = "lblPO";
            this.lblPO.Size = new System.Drawing.Size(72, 24);
            this.lblPO.TabIndex = 198;
            // 
            // labeee
            // 
            this.labeee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labeee.Location = new System.Drawing.Point(416, 121);
            this.labeee.Name = "labeee";
            this.labeee.Size = new System.Drawing.Size(72, 24);
            this.labeee.TabIndex = 197;
            // 
            // labTotal
            // 
            this.labTotal.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labTotal.Location = new System.Drawing.Point(600, 176);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(88, 23);
            this.labTotal.TabIndex = 196;
            this.labTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTotalHis
            // 
            this.labTotalHis.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labTotalHis.Location = new System.Drawing.Point(600, 121);
            this.labTotalHis.Name = "labTotalHis";
            this.labTotalHis.Size = new System.Drawing.Size(88, 24);
            this.labTotalHis.TabIndex = 194;
            // 
            // labTotalCheck
            // 
            this.labTotalCheck.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labTotalCheck.Location = new System.Drawing.Point(416, 176);
            this.labTotalCheck.Name = "labTotalCheck";
            this.labTotalCheck.Size = new System.Drawing.Size(72, 23);
            this.labTotalCheck.TabIndex = 193;
            this.labTotalCheck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTotalCard
            // 
            this.labTotalCard.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labTotalCard.Location = new System.Drawing.Point(312, 176);
            this.labTotalCard.Name = "labTotalCard";
            this.labTotalCard.Size = new System.Drawing.Size(88, 23);
            this.labTotalCard.TabIndex = 192;
            this.labTotalCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTotalPay
            // 
            this.labTotalPay.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labTotalPay.Location = new System.Drawing.Point(216, 176);
            this.labTotalPay.Name = "labTotalPay";
            this.labTotalPay.Size = new System.Drawing.Size(80, 23);
            this.labTotalPay.TabIndex = 191;
            this.labTotalPay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labCard
            // 
            this.labCard.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labCard.Location = new System.Drawing.Point(312, 121);
            this.labCard.Name = "labCard";
            this.labCard.Size = new System.Drawing.Size(88, 24);
            this.labCard.TabIndex = 188;
            // 
            // labOtherPay
            // 
            this.labOtherPay.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labOtherPay.Location = new System.Drawing.Point(216, 144);
            this.labOtherPay.Name = "labOtherPay";
            this.labOtherPay.Size = new System.Drawing.Size(80, 16);
            this.labOtherPay.TabIndex = 187;
            this.labOtherPay.Visible = false;
            // 
            // labPay
            // 
            this.labPay.BackColor = System.Drawing.Color.White;
            this.labPay.Location = new System.Drawing.Point(216, 121);
            this.labPay.Name = "labPay";
            this.labPay.Size = new System.Drawing.Size(80, 24);
            this.labPay.TabIndex = 186;
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.Location = new System.Drawing.Point(152, 176);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(48, 24);
            this.label30.TabIndex = 185;
            this.label30.Text = "合计";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label31.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.Location = new System.Drawing.Point(104, 144);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(96, 24);
            this.label31.TabIndex = 184;
            this.label31.Text = "其它预收款";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.Location = new System.Drawing.Point(104, 120);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(96, 24);
            this.label32.TabIndex = 183;
            this.label32.Text = "医疗预收款";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWasteNo
            // 
            this.txtWasteNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWasteNo.Location = new System.Drawing.Point(200, 426);
            this.txtWasteNo.Multiline = true;
            this.txtWasteNo.Name = "txtWasteNo";
            this.txtWasteNo.Size = new System.Drawing.Size(489, 53);
            this.txtWasteNo.TabIndex = 182;
            // 
            // txtRealNo
            // 
            this.txtRealNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRealNo.Location = new System.Drawing.Point(200, 294);
            this.txtRealNo.Multiline = true;
            this.txtRealNo.Name = "txtRealNo";
            this.txtRealNo.Size = new System.Drawing.Size(489, 128);
            this.txtRealNo.TabIndex = 181;
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.Location = new System.Drawing.Point(104, 424);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(96, 23);
            this.label33.TabIndex = 180;
            this.label33.Text = "作废收据号";
            this.label33.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label34
            // 
            this.label34.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label34.Location = new System.Drawing.Point(104, 292);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(96, 23);
            this.label34.TabIndex = 179;
            this.label34.Text = "实际收据号";
            this.label34.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label35.Location = new System.Drawing.Point(352, 239);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(100, 24);
            this.label35.TabIndex = 176;
            this.label35.Text = "收 据 号";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label35.Visible = false;
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label36.Location = new System.Drawing.Point(104, 263);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(96, 23);
            this.label36.TabIndex = 177;
            this.label36.Text = "收据流水号";
            this.label36.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblBillSpan
            // 
            this.lblBillSpan.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBillSpan.Location = new System.Drawing.Point(200, 263);
            this.lblBillSpan.Name = "lblBillSpan";
            this.lblBillSpan.Size = new System.Drawing.Size(272, 23);
            this.lblBillSpan.TabIndex = 178;
            this.lblBillSpan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(504, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 24);
            this.label1.TabIndex = 206;
            this.label1.Text = "其它";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(608, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 24);
            this.label10.TabIndex = 205;
            this.label10.Text = "合计";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(416, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 24);
            this.label4.TabIndex = 204;
            this.label4.Text = "支票";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb10
            // 
            this.lb10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb10.Location = new System.Drawing.Point(120, 88);
            this.lb10.Name = "lb10";
            this.lb10.Size = new System.Drawing.Size(80, 24);
            this.lb10.TabIndex = 201;
            this.lb10.Text = "预收收入";
            this.lb10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDiagnose
            // 
            this.lbDiagnose.BackColor = System.Drawing.Color.White;
            this.lbDiagnose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDiagnose.Location = new System.Drawing.Point(224, 88);
            this.lbDiagnose.Name = "lbDiagnose";
            this.lbDiagnose.Size = new System.Drawing.Size(72, 24);
            this.lbDiagnose.TabIndex = 202;
            this.lbDiagnose.Text = "现金";
            this.lbDiagnose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(336, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 24);
            this.label3.TabIndex = 203;
            this.label3.Text = "银行卡";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labOtherCard
            // 
            this.labOtherCard.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labOtherCard.Location = new System.Drawing.Point(312, 144);
            this.labOtherCard.Name = "labOtherCard";
            this.labOtherCard.Size = new System.Drawing.Size(88, 23);
            this.labOtherCard.TabIndex = 189;
            this.labOtherCard.Visible = false;
            // 
            // labOtherCheck
            // 
            this.labOtherCheck.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labOtherCheck.Location = new System.Drawing.Point(416, 144);
            this.labOtherCheck.Name = "labOtherCheck";
            this.labOtherCheck.Size = new System.Drawing.Size(72, 23);
            this.labOtherCheck.TabIndex = 190;
            this.labOtherCheck.Visible = false;
            // 
            // lblOtherPO
            // 
            this.lblOtherPO.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblOtherPO.Location = new System.Drawing.Point(504, 144);
            this.lblOtherPO.Name = "lblOtherPO";
            this.lblOtherPO.Size = new System.Drawing.Size(72, 23);
            this.lblOtherPO.TabIndex = 199;
            this.lblOtherPO.Visible = false;
            // 
            // labOtherTotal
            // 
            this.labOtherTotal.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labOtherTotal.Location = new System.Drawing.Point(600, 144);
            this.labOtherTotal.Name = "labOtherTotal";
            this.labOtherTotal.Size = new System.Drawing.Size(88, 23);
            this.labOtherTotal.TabIndex = 195;
            this.labOtherTotal.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(96, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 1);
            this.panel1.TabIndex = 207;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(96, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 1);
            this.panel2.TabIndex = 208;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(96, 201);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(584, 1);
            this.panel3.TabIndex = 209;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(96, 485);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(584, 1);
            this.panel4.TabIndex = 210;
            // 
            // lblTotalNum
            // 
            this.lblTotalNum.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblTotalNum.Location = new System.Drawing.Point(212, 208);
            this.lblTotalNum.Name = "lblTotalNum";
            this.lblTotalNum.Size = new System.Drawing.Size(96, 23);
            this.lblTotalNum.TabIndex = 212;
            // 
            // lblWasteNum
            // 
            this.lblWasteNum.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lblWasteNum.Location = new System.Drawing.Point(587, 208);
            this.lblWasteNum.Name = "lblWasteNum";
            this.lblWasteNum.Size = new System.Drawing.Size(96, 23);
            this.lblWasteNum.TabIndex = 214;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(119, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 23);
            this.label5.TabIndex = 215;
            this.label5.Text = "票据总数:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(500, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 23);
            this.label2.TabIndex = 216;
            this.label2.Text = "作废票据:";
            // 
            // ucPrepayDayByOP
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblWasteNum);
            this.Controls.Add(this.lblTotalNum);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb10);
            this.Controls.Add(this.lbDiagnose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTotPO);
            this.Controls.Add(this.lblOtherPO);
            this.Controls.Add(this.lblPO);
            this.Controls.Add(this.labeee);
            this.Controls.Add(this.labTotal);
            this.Controls.Add(this.labOtherTotal);
            this.Controls.Add(this.labTotalHis);
            this.Controls.Add(this.labTotalCheck);
            this.Controls.Add(this.labTotalCard);
            this.Controls.Add(this.labTotalPay);
            this.Controls.Add(this.labOtherCheck);
            this.Controls.Add(this.labOtherCard);
            this.Controls.Add(this.labCard);
            this.Controls.Add(this.labOtherPay);
            this.Controls.Add(this.labPay);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.txtWasteNo);
            this.Controls.Add(this.txtRealNo);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.lblBillSpan);
            this.Controls.Add(this.lblOPerName);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.labPrintDate);
            this.Controls.Add(this.lbOpsRoom);
            this.Controls.Add(this.lb4);
            this.Controls.Add(this.labOpsDate);
            this.Controls.Add(this.lb3);
            this.Controls.Add(this.lbTitle);
            this.Name = "ucPrepayDayByOP";
            this.Size = new System.Drawing.Size(848, 515);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region 定义
        private SOC.Fee.DayBalance.Object.PrepayDayBalance oEPrePayStat;
        private SOC.Fee.DayBalance.Manager.PrepayDayBalance oCFeeReport = new  SOC.Fee.DayBalance.Manager.PrepayDayBalance();
		public string strOper = "";


	
		#endregion

		#region 属性
		private string beginDate = "";

		private string endDate = "";
		public string BeginDate
		{
			get{return beginDate;}
			set{beginDate=value;}
		}
		public string EndDate
		{
			get{return endDate;}
			set{endDate = value;}
		}
		/// <summary>
		/// 操作员姓名
		/// </summary>
		public string OperName
		{
			get
			{
				return this.lblOPerName.Text.Trim();
			}
			set
			{
				this.lblOPerName.Text=value;
			}
		}
		#endregion

		#region 方法

		/// <summary>
		/// 按操作员
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Oper"></param>

		public void GetCost(string Begin,string End,string Oper)
		{
            oEPrePayStat = new SOC.Fee.DayBalance.Object.PrepayDayBalance();
            string strPay = "",strCard = "",strCheck = "";
            string strPO = "";string totNum = "";
            strPay = oCFeeReport.GetPrepayCost(Begin, End, Oper,"CA");//现金
            strCard = oCFeeReport.GetPrepayCost(Begin, End, Oper, "DB','CD");//银行卡
            strCheck = oCFeeReport.GetPrepayCost(Begin, End, Oper, "CH");//支票
            strPO = oCFeeReport.GetPrepayCost(Begin, End, Oper, "PO");//汇票	

            totNum = this.oCFeeReport.GetReceiptNumByState(Begin, End, "0','1','2", this.oCFeeReport.Operator.ID);
            if (totNum == "")
            {
                this.lblTotalNum.Text = "0 张";
            }
            else
            {
                this.lblTotalNum.Text = totNum + " 张";
            }
            totNum = this.oCFeeReport.GetReceiptNumByState1(Begin, End, "1','2", this.oCFeeReport.Operator.ID);
            if (totNum == "")
            {
                this.lblWasteNum.Text = "0 张";
            }
            else
            {
                this.lblWasteNum.Text = totNum + " 张";
            }

            string tempZone = GetZone(this.oCFeeReport.GetOutReceiptNormal(Begin, End, "0','1','2", this.oCFeeReport.Operator.ID));


            this.txtRealNo.Text = SpiltString(tempZone);
            this.txtWasteNo.Text =SpiltString(GetZone(this.oCFeeReport.GetOutReceiptBack(Begin, End, "1','2", this.oCFeeReport.Operator.ID)));
            SetCost(strPay, strCard, strCheck, strPO);
		}
		public void SetCost(string strPay,string strCard,string strCheck,string strPO)
		{
			try
			{
				//			//return "";//
				if(strPay!="")
				{
					this.labPay.Text = strPay;//现金
				
				}
				else
				{
					this.labPay.Text = "0.00";
				}
				if(strCard!="")//银行卡
				{
					this.labCard.Text = strCard; 
				
				}
				else
				{
					this.labCard.Text = "0.00";
				}
				if(strCheck!="")//支票
				{
					this.labeee.Text = strCheck;
					
				}
				else
				{
					this.labeee.Text = "0.00";
				}
				if(strPO!="")//汇票
				{
					this.lblPO.Text = strPO;
				
				}
				else
				{
					this.lblPO.Text  = "0.00";
				}
				this.labOtherPay.Text = "0.00";
			
				this.labOtherCard.Text = "0.00";
			
				this.labOtherCheck.Text = "0.00";
				
				this.lblOtherPO.Text = "0.00";
				
			     
				this.labTotalPay.Text = Convert.ToString(Convert.ToDecimal(this.labPay.Text)+Convert.ToDecimal(this.labOtherPay.Text));
			
				this.labTotalCard.Text = Convert.ToString(Convert.ToDecimal(this.labCard.Text)+Convert.ToDecimal(this.labOtherCard.Text));
				
				this.labTotalCheck.Text = Convert.ToString(Convert.ToDecimal(this.labeee.Text)+Convert.ToDecimal(this.labOtherCheck.Text));
			
				this.lblTotPO.Text = Convert.ToString(Convert.ToDecimal(this.lblPO.Text)+Convert.ToDecimal(this.lblOtherPO.Text));
			
				this.labTotalHis.Text = Convert.ToString(Convert.ToDecimal(labPay.Text)+Convert.ToDecimal(labCard.Text)+Convert.ToDecimal(this.labeee.Text)+Convert.ToDecimal(this.lblPO.Text));
			
					
				this.labOtherTotal.Text = Convert.ToString(Convert.ToDecimal(this.labOtherPay.Text)+Convert.ToDecimal(this.labOtherCard.Text)+Convert.ToDecimal(this.labOtherCheck.Text)+Convert.ToDecimal(this.lblOtherPO.Text));
				
				this.labTotal.Text = Convert.ToString(Convert.ToDecimal(this.labTotalPay.Text)+Convert.ToDecimal(this.labTotalCard.Text)+Convert.ToDecimal(this.labTotalCheck.Text)+Convert.ToDecimal(this.lblTotPO.Text));
			
				return;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}
		public void SetBillSpan(string billNO)
		{
			this.lblBillSpan.Text = billNO;
		}


        private string SpiltString(string tempZone)
        {
             string[] arZone=tempZone.Split(',');
             string result="";
             if (arZone.Length >0)
             {
                 int i=0;
                 foreach (string str in arZone)
                 {
                     result +=str+"  ";
                     if (++i == 3)
                     {
                         result += System.Environment.NewLine;
                         i = 0;
                     }
                 }            
             }
             return result;
 
        }

		/// <summary>
		/// 保存预交金日结数据
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
        public int Add(SOC.Fee.DayBalance.Object.PrepayDayBalance obj)
		{
			int iRet = 0;
			
              obj.CACost = Convert.ToDecimal(this.labTotalPay.Text);//现金
              obj.CHCost = Convert.ToDecimal(this.labTotalCheck.Text);//支票
              obj.POSCost = Convert.ToDecimal(this.labTotalCard.Text);//银行卡
              obj.ORCost = Convert.ToDecimal(this.lblTotPO.Text);//其他	
              obj.User02 = this.txtRealNo.Text.Replace(" ","");
              obj.User03 = this.txtWasteNo.Text.Replace(" ","");


              obj.RealCost = obj.TotCost = Convert.ToDecimal(this.labTotal.Text);//总额


              iRet = this.oCFeeReport.InsertPrepayStat(obj);
              if (iRet < 0)
              {
                  MessageBox.Show("保存日结数据出错");
                  return -1;
              }
				
			return iRet;			
		}


        public SOC.Fee.DayBalance.Object.PrepayDayBalance GetPrepayStat()
		{
			this.oEPrePayStat = new  SOC.Fee.DayBalance.Object.PrepayDayBalance();
			this.oEPrePayStat.BeginDate = this.BeginDate.ToString();//起始时间
			this.oEPrePayStat.EndDate = this.EndDate.ToString();//结束时间
			this.oEPrePayStat.BalancOper.ID = this.oCFeeReport.Operator.ID;//操作员
			this.oEPrePayStat.RealCost = Convert.ToDecimal(this.labPay.Text);//
			return oEPrePayStat;
		}

		/// <summary>
		/// 判断输入的数据是否数字
		/// </summary>
		/// <param name="strNumber"></param>
		/// <returns></returns>
		public bool IsNumber(string strNumber)
		{
			Regex objNotNumberPattern=new Regex("[^0-9.-]");
			Regex objTwoDotPattern=new Regex("[0-9]*[.][0-9]*[.][0-9]*");
			Regex objTwoMinusPattern=new Regex("[0-9]*[-][0-9]*[-][0-9]*");
			string strValidRealPattern="^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
			string strValidIntegerPattern="^([-]|[0-9])[0-9]*$";
			Regex objNumberPattern =new Regex("(" + strValidRealPattern +")|(" + strValidIntegerPattern + ")");

			return !objNotNumberPattern.IsMatch(strNumber) &&
				!objTwoDotPattern.IsMatch(strNumber) &&
				!objTwoMinusPattern.IsMatch(strNumber) &&
				objNumberPattern.IsMatch(strNumber);			
		}

		private int CheckMoney(TextBox obj)
		{
			int i=0;
			if(obj.Text == string.Empty)
			{
				MessageBox.Show("存款金额不能为空!");
				i = -1;
			}
				 
			else
			{
				if(!this.IsNumber(obj.Text))
				{
					MessageBox.Show("您输入的不是数字!");
					return -1;
				}
			}
			return i;
		}

		private int Edit()
		{
			int iRet = 1;
		
			return iRet;
		}

		private string GetZone(string zone)
		{
			if(zone == "")
			{
				return "";
			}
			int temp=-2;
			string[] invoice = zone.Split('|');
			string result = "";
			for(int i=0;i<invoice.Length;i++)
			{
				if(temp+1!= FS.FrameWork.Function.NConvert.ToInt32(invoice[i]))
				{
					if(temp == -2)
					{
                        temp = FS.FrameWork.Function.NConvert.ToInt32(invoice[i]);
						result += invoice[i];

						if(i == invoice.Length -1)
						{
							result += "--"+invoice[i]+",";
						}
					}
					else
					{
						result += "--"+temp+",";
						result += invoice[i];
                        temp = FS.FrameWork.Function.NConvert.ToInt32(invoice[i]);

						if(i == invoice.Length -1)
						{
							result += "--"+invoice[i]+",";
						}
					}
				}
				else
				{
                    temp = FS.FrameWork.Function.NConvert.ToInt32(invoice[i]);

					if(i == invoice.Length -1)
					{
						result += "--"+invoice[i]+",";
					}
				}
			}

			if(result.Length >=1 )
			{
				result = result.Substring(0,result.Length -1);
			}

			return result;
		}

        public SOC.Fee.DayBalance.Object.PrepayDayBalance GetListByNo(string strNo)
        {
            SOC.Fee.DayBalance.Object.PrepayDayBalance obj = this.oCFeeReport.GetPrepayStatListBystaticNo(strNo);
            return obj;
        }
		#endregion

	}
}
