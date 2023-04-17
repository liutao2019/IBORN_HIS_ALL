using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Text;

namespace InterfaceInstanceDefault.IRecipePrint
{
    /// <summary>
    /// ucNewRecipePrint 的摘要说明。
    /// </summary>
    public class ucNewRecipePrint : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IRecipePrint, FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.Panel lblDoc;
        private System.Windows.Forms.Label lblRecipeNo;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSeeDept;
        private System.Windows.Forms.Label lblDiag;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblPhaDoc;
        private System.Windows.Forms.Label lblMCardNo;
        private System.Windows.Forms.Label lblCardNo;

        /// <summary>
        /// 频次
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freHelper = null;
        public FS.FrameWork.Public.ObjectHelper FreHelper
        {
            set
            {
                this.freHelper = value;
            }
        }

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private FS.HISFC.Models.Registration.Register myReg;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblPhaMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkPub;
        private System.Windows.Forms.CheckBox chkOwn;
        private System.Windows.Forms.CheckBox chkPay;
        private System.Windows.Forms.CheckBox chkOth;
        private System.Windows.Forms.CheckBox chkMale;
        private System.Windows.Forms.CheckBox chkFemale;
        private System.Windows.Forms.Panel pnlSex;
        private System.Windows.Forms.Label lblSeeYear;
        private System.Windows.Forms.Label lblSeeMonth;
        private System.Windows.Forms.Label lblSeeDay;
        private System.Windows.Forms.Label lblJS;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        public bool bEnglish = false;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblCard;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblDay;
        private Label lblHeight;
        private FS.FrameWork.WinForms.Controls.NeuPictureBox npbBarCode;
        private FS.FrameWork.WinForms.Controls.NeuPictureBox npbRecipeNo;
        private Label lblSendDoct;
        private Label lblDrugDoct;
        //当天处方
        FS.HISFC.Models.Pharmacy.DrugRecipe currentRecipe;
        public ucNewRecipePrint()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化

        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblOrder = new System.Windows.Forms.Label();
            this.lblDoc = new System.Windows.Forms.Panel();
            this.lblSendDoct = new System.Windows.Forms.Label();
            this.lblDrugDoct = new System.Windows.Forms.Label();
            this.lblPhaMoney = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPhaDoc = new System.Windows.Forms.Label();
            this.lblRecipeNo = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSeeDept = new System.Windows.Forms.Label();
            this.lblDiag = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.lblMCardNo = new System.Windows.Forms.Label();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSeeYear = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.chkPub = new System.Windows.Forms.CheckBox();
            this.chkOwn = new System.Windows.Forms.CheckBox();
            this.chkPay = new System.Windows.Forms.CheckBox();
            this.chkOth = new System.Windows.Forms.CheckBox();
            this.chkMale = new System.Windows.Forms.CheckBox();
            this.chkFemale = new System.Windows.Forms.CheckBox();
            this.pnlSex = new System.Windows.Forms.Panel();
            this.lblSeeMonth = new System.Windows.Forms.Label();
            this.lblSeeDay = new System.Windows.Forms.Label();
            this.lblJS = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblCard = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.npbBarCode = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.npbRecipeNo = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.lblDoc.SuspendLayout();
            this.pnlSex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npbRecipeNo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("楷体_GB2312", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(76, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(398, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "顺德区妇幼保健院处方笺";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(4, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 1);
            this.panel1.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(244, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 6;
            this.label5.Text = "处方编号：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 14);
            this.label6.TabIndex = 7;
            this.label6.Text = "姓名：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(271, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 8;
            this.label7.Text = "性别：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(417, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 9;
            this.label8.Text = "年龄：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(245, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 14);
            this.label9.TabIndex = 10;
            this.label9.Text = "医疗证/医保卡号：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(329, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(140, 14);
            this.label10.TabIndex = 11;
            this.label10.Text = "科别(病区/床位号)：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(108, 167);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 14);
            this.label12.TabIndex = 13;
            this.label12.Text = "门诊/住院病历号：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(2, 195);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 14);
            this.label19.TabIndex = 20;
            this.label19.Text = "临床诊断：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(2, 224);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 14);
            this.label20.TabIndex = 21;
            this.label20.Text = "住址/电话：";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(4, 247);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 1);
            this.panel2.TabIndex = 23;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(5, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(49, 14);
            this.label22.TabIndex = 25;
            this.label22.Text = "医师：";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(5, 29);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 14);
            this.label24.TabIndex = 27;
            this.label24.Text = "审核药师：";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(163, 29);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 14);
            this.label25.TabIndex = 28;
            this.label25.Text = "调配药师/士：";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(350, 29);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(119, 14);
            this.label27.TabIndex = 30;
            this.label27.Text = "核对、发药药师：";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrder
            // 
            this.lblOrder.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOrder.ForeColor = System.Drawing.Color.Black;
            this.lblOrder.Location = new System.Drawing.Point(42, 263);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(521, 425);
            this.lblOrder.TabIndex = 31;
            // 
            // lblDoc
            // 
            this.lblDoc.Controls.Add(this.lblSendDoct);
            this.lblDoc.Controls.Add(this.lblDrugDoct);
            this.lblDoc.Controls.Add(this.lblPhaMoney);
            this.lblDoc.Controls.Add(this.label3);
            this.lblDoc.Controls.Add(this.label27);
            this.lblDoc.Controls.Add(this.lblPhaDoc);
            this.lblDoc.Controls.Add(this.label22);
            this.lblDoc.Controls.Add(this.label24);
            this.lblDoc.Controls.Add(this.label25);
            this.lblDoc.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoc.ForeColor = System.Drawing.Color.Black;
            this.lblDoc.Location = new System.Drawing.Point(4, 702);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(559, 48);
            this.lblDoc.TabIndex = 32;
            // 
            // lblSendDoct
            // 
            this.lblSendDoct.AutoSize = true;
            this.lblSendDoct.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSendDoct.ForeColor = System.Drawing.Color.Black;
            this.lblSendDoct.Location = new System.Drawing.Point(456, 29);
            this.lblSendDoct.Name = "lblSendDoct";
            this.lblSendDoct.Size = new System.Drawing.Size(98, 14);
            this.lblSendDoct.TabIndex = 57;
            this.lblSendDoct.Text = "111111/王立网";
            this.lblSendDoct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSendDoct.Visible = false;
            // 
            // lblDrugDoct
            // 
            this.lblDrugDoct.AutoSize = true;
            this.lblDrugDoct.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDrugDoct.ForeColor = System.Drawing.Color.Black;
            this.lblDrugDoct.Location = new System.Drawing.Point(250, 29);
            this.lblDrugDoct.Name = "lblDrugDoct";
            this.lblDrugDoct.Size = new System.Drawing.Size(98, 14);
            this.lblDrugDoct.TabIndex = 56;
            this.lblDrugDoct.Text = "111111/王立网";
            this.lblDrugDoct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDrugDoct.Visible = false;
            // 
            // lblPhaMoney
            // 
            this.lblPhaMoney.AutoSize = true;
            this.lblPhaMoney.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhaMoney.ForeColor = System.Drawing.Color.Black;
            this.lblPhaMoney.Location = new System.Drawing.Point(246, 3);
            this.lblPhaMoney.Name = "lblPhaMoney";
            this.lblPhaMoney.Size = new System.Drawing.Size(105, 14);
            this.lblPhaMoney.TabIndex = 54;
            this.lblPhaMoney.Text = "不知道多少钱啊";
            this.lblPhaMoney.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(163, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 53;
            this.label3.Text = "药品金额：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhaDoc
            // 
            this.lblPhaDoc.AutoSize = true;
            this.lblPhaDoc.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhaDoc.ForeColor = System.Drawing.Color.Black;
            this.lblPhaDoc.Location = new System.Drawing.Point(50, 3);
            this.lblPhaDoc.Name = "lblPhaDoc";
            this.lblPhaDoc.Size = new System.Drawing.Size(98, 14);
            this.lblPhaDoc.TabIndex = 52;
            this.lblPhaDoc.Text = "111111/王立网";
            this.lblPhaDoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPhaDoc.Visible = false;
            // 
            // lblRecipeNo
            // 
            this.lblRecipeNo.AutoSize = true;
            this.lblRecipeNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecipeNo.ForeColor = System.Drawing.Color.Black;
            this.lblRecipeNo.Location = new System.Drawing.Point(315, 73);
            this.lblRecipeNo.Name = "lblRecipeNo";
            this.lblRecipeNo.Size = new System.Drawing.Size(311, 39);
            this.lblRecipeNo.TabIndex = 34;
            this.lblRecipeNo.Text = "*20091117100001*";
            this.lblRecipeNo.Visible = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(51, 139);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(91, 14);
            this.lblName.TabIndex = 35;
            this.lblName.Text = "默罕默德萨尔";
            // 
            // lblSeeDept
            // 
            this.lblSeeDept.AutoSize = true;
            this.lblSeeDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSeeDept.ForeColor = System.Drawing.Color.Black;
            this.lblSeeDept.Location = new System.Drawing.Point(462, 168);
            this.lblSeeDept.Name = "lblSeeDept";
            this.lblSeeDept.Size = new System.Drawing.Size(105, 14);
            this.lblSeeDept.TabIndex = 36;
            this.lblSeeDept.Text = "门诊妇科一诊室";
            // 
            // lblDiag
            // 
            this.lblDiag.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiag.ForeColor = System.Drawing.Color.Black;
            this.lblDiag.Location = new System.Drawing.Point(68, 196);
            this.lblDiag.Name = "lblDiag";
            this.lblDiag.Size = new System.Drawing.Size(215, 28);
            this.lblDiag.TabIndex = 39;
            // 
            // lblTel
            // 
            this.lblTel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTel.ForeColor = System.Drawing.Color.Black;
            this.lblTel.Location = new System.Drawing.Point(92, 226);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(267, 14);
            this.lblTel.TabIndex = 40;
            // 
            // lblMCardNo
            // 
            this.lblMCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMCardNo.ForeColor = System.Drawing.Color.Black;
            this.lblMCardNo.Location = new System.Drawing.Point(371, 111);
            this.lblMCardNo.Name = "lblMCardNo";
            this.lblMCardNo.Size = new System.Drawing.Size(192, 14);
            this.lblMCardNo.TabIndex = 46;
            this.lblMCardNo.Text = "1111111111111111";
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardNo.ForeColor = System.Drawing.Color.Black;
            this.lblCardNo.Location = new System.Drawing.Point(224, 167);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(105, 14);
            this.lblCardNo.TabIndex = 47;
            this.lblCardNo.Text = "01234567890123";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(2, 101);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 14);
            this.label14.TabIndex = 52;
            this.label14.Text = "费别：";
            // 
            // lblSeeYear
            // 
            this.lblSeeYear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSeeYear.ForeColor = System.Drawing.Color.Black;
            this.lblSeeYear.Location = new System.Drawing.Point(355, 195);
            this.lblSeeYear.Name = "lblSeeYear";
            this.lblSeeYear.Size = new System.Drawing.Size(36, 14);
            this.lblSeeYear.TabIndex = 58;
            this.lblSeeYear.Text = "2010";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(271, 195);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(77, 14);
            this.label31.TabIndex = 57;
            this.label31.Text = "开具日期：";
            // 
            // chkPub
            // 
            this.chkPub.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkPub.ForeColor = System.Drawing.Color.Black;
            this.chkPub.Location = new System.Drawing.Point(61, 90);
            this.chkPub.Name = "chkPub";
            this.chkPub.Size = new System.Drawing.Size(48, 21);
            this.chkPub.TabIndex = 68;
            // 
            // chkOwn
            // 
            this.chkOwn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkOwn.Location = new System.Drawing.Point(120, 90);
            this.chkOwn.Name = "chkOwn";
            this.chkOwn.Size = new System.Drawing.Size(53, 21);
            this.chkOwn.TabIndex = 69;
            // 
            // chkPay
            // 
            this.chkPay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkPay.Location = new System.Drawing.Point(61, 110);
            this.chkPay.Name = "chkPay";
            this.chkPay.Size = new System.Drawing.Size(48, 16);
            this.chkPay.TabIndex = 70;
            // 
            // chkOth
            // 
            this.chkOth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkOth.Location = new System.Drawing.Point(120, 110);
            this.chkOth.Name = "chkOth";
            this.chkOth.Size = new System.Drawing.Size(53, 16);
            this.chkOth.TabIndex = 71;
            // 
            // chkMale
            // 
            this.chkMale.Location = new System.Drawing.Point(7, 3);
            this.chkMale.Name = "chkMale";
            this.chkMale.Size = new System.Drawing.Size(53, 19);
            this.chkMale.TabIndex = 72;
            // 
            // chkFemale
            // 
            this.chkFemale.Location = new System.Drawing.Point(51, 3);
            this.chkFemale.Name = "chkFemale";
            this.chkFemale.Size = new System.Drawing.Size(48, 19);
            this.chkFemale.TabIndex = 74;
            // 
            // pnlSex
            // 
            this.pnlSex.Controls.Add(this.chkFemale);
            this.pnlSex.Controls.Add(this.chkMale);
            this.pnlSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlSex.Location = new System.Drawing.Point(310, 136);
            this.pnlSex.Name = "pnlSex";
            this.pnlSex.Size = new System.Drawing.Size(103, 21);
            this.pnlSex.TabIndex = 75;
            // 
            // lblSeeMonth
            // 
            this.lblSeeMonth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSeeMonth.ForeColor = System.Drawing.Color.Black;
            this.lblSeeMonth.Location = new System.Drawing.Point(404, 195);
            this.lblSeeMonth.Name = "lblSeeMonth";
            this.lblSeeMonth.Size = new System.Drawing.Size(23, 14);
            this.lblSeeMonth.TabIndex = 76;
            // 
            // lblSeeDay
            // 
            this.lblSeeDay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSeeDay.ForeColor = System.Drawing.Color.Black;
            this.lblSeeDay.Location = new System.Drawing.Point(444, 195);
            this.lblSeeDay.Name = "lblSeeDay";
            this.lblSeeDay.Size = new System.Drawing.Size(22, 14);
            this.lblSeeDay.TabIndex = 77;
            // 
            // lblJS
            // 
            this.lblJS.AutoSize = true;
            this.lblJS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblJS.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblJS.Location = new System.Drawing.Point(467, 7);
            this.lblJS.Name = "lblJS";
            this.lblJS.Size = new System.Drawing.Size(56, 23);
            this.lblJS.TabIndex = 80;
            this.lblJS.Text = "精二";
            this.lblJS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblJS.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(4, 695);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(560, 1);
            this.panel3.TabIndex = 81;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.ForeColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(4, 756);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(560, 1);
            this.panel5.TabIndex = 82;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 32);
            this.label2.TabIndex = 83;
            this.label2.Text = "R";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(19, 256);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 32);
            this.label13.TabIndex = 84;
            this.label13.Text = "p";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(431, 195);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 14);
            this.label16.TabIndex = 86;
            this.label16.Text = "/";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(389, 195);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 14);
            this.label17.TabIndex = 87;
            this.label17.Text = "/";
            // 
            // lblCard
            // 
            this.lblCard.AutoSize = true;
            this.lblCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCard.ForeColor = System.Drawing.Color.Black;
            this.lblCard.Location = new System.Drawing.Point(3, 2);
            this.lblCard.Name = "lblCard";
            this.lblCard.Size = new System.Drawing.Size(249, 39);
            this.lblCard.TabIndex = 88;
            this.lblCard.Text = "A23423432432";
            this.lblCard.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.ForeColor = System.Drawing.Color.Black;
            this.lblAge.Location = new System.Drawing.Point(454, 140);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(42, 14);
            this.lblAge.TabIndex = 89;
            this.lblAge.Text = "100岁";
            // 
            // lblMonth
            // 
            this.lblMonth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMonth.ForeColor = System.Drawing.Color.Black;
            this.lblMonth.Location = new System.Drawing.Point(491, 140);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(42, 14);
            this.lblMonth.TabIndex = 90;
            this.lblMonth.Text = "12月";
            this.lblMonth.Visible = false;
            // 
            // lblDay
            // 
            this.lblDay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDay.ForeColor = System.Drawing.Color.Black;
            this.lblDay.Location = new System.Drawing.Point(521, 140);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(42, 14);
            this.lblDay.TabIndex = 91;
            this.lblDay.Text = "100天";
            this.lblDay.Visible = false;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHeight.ForeColor = System.Drawing.Color.Black;
            this.lblHeight.Location = new System.Drawing.Point(2, 167);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(91, 14);
            this.lblHeight.TabIndex = 93;
            this.lblHeight.Text = "体重    千克";
            // 
            // npbBarCode
            // 
            this.npbBarCode.Location = new System.Drawing.Point(9, 3);
            this.npbBarCode.Name = "npbBarCode";
            this.npbBarCode.Size = new System.Drawing.Size(150, 39);
            this.npbBarCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npbBarCode.TabIndex = 94;
            this.npbBarCode.TabStop = false;
            // 
            // npbRecipeNo
            // 
            this.npbRecipeNo.Location = new System.Drawing.Point(324, 73);
            this.npbRecipeNo.Name = "npbRecipeNo";
            this.npbRecipeNo.Size = new System.Drawing.Size(191, 35);
            this.npbRecipeNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npbRecipeNo.TabIndex = 95;
            this.npbRecipeNo.TabStop = false;
            // 
            // ucNewRecipePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.npbRecipeNo);
            this.Controls.Add(this.npbBarCode);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblCard);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblJS);
            this.Controls.Add(this.lblSeeDay);
            this.Controls.Add(this.lblSeeMonth);
            this.Controls.Add(this.pnlSex);
            this.Controls.Add(this.chkOwn);
            this.Controls.Add(this.chkPub);
            this.Controls.Add(this.lblSeeYear);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblCardNo);
            this.Controls.Add(this.lblMCardNo);
            this.Controls.Add(this.lblTel);
            this.Controls.Add(this.lblDiag);
            this.Controls.Add(this.lblSeeDept);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblRecipeNo);
            this.Controls.Add(this.lblDoc);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblOrder);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chkPay);
            this.Controls.Add(this.chkOth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label13);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ucNewRecipePrint";
            this.Size = new System.Drawing.Size(568, 760);
            this.Load += new System.EventHandler(this.ucNewRecipePrint_Load);
            this.lblDoc.ResumeLayout(false);
            this.lblDoc.PerformLayout();
            this.pnlSex.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npbRecipeNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        //控制参数管理类
        FS.HISFC.BizLogic.Manager.Constant cnst = new FS.HISFC.BizLogic.Manager.Constant();
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        FS.HISFC.Models.Fee.Outpatient.FeeItemList fee = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
        FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        FS.HISFC.BizLogic.Order.OutPatient.Order orderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        FS.HISFC.BizLogic.Registration.Register regManager = new FS.HISFC.BizLogic.Registration.Register();
        FS.HISFC.BizLogic.Pharmacy.DrugStore drugManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
        FS.HISFC.BizLogic.Pharmacy.Item phaManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
        //FS.HISFC.BizLogic.RADT.InPatient myInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.FrameWork.Public.ObjectHelper hsDrugQuaulity = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 是否配药单
        /// </summary>
        bool bDrugBill = false;
        string speRecipeLabel = "";

        private static FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 是否配药单
        /// </summary>
        public bool BDrugBill
        {
            set
            {
                this.bDrugBill = value;
            }
        }
        /// <summary>
        /// 打印头名称
        /// </summary>
        public string PrintHeader
        {
            set
            {
                this.label1.Text = value;
            }
        }
        /// <summary>
        /// 设置诊断内容
        /// </summary>
        public string SetDiagText
        {
            set
            {
                this.lblDiag.Text = value;
            }
        }

        /// <summary>
        /// 打印处方项目名称是否是通用名：1 通用名；0 商品名
        /// </summary>
        private int printItemNameType = -1;

        /// <summary>
        /// 是否打印签名信息？（否则手工签名）
        /// </summary>
        private int isPrintSignInfo = -1;


        /// <summary>
        /// 医嘱数组
        /// </summary>
        public ArrayList alOrder = new ArrayList();

        private ArrayList alTemp = new ArrayList();

        /// <summary>
        /// 存储处方组合号
        /// </summary>
        private Hashtable hsCombID = new Hashtable();

        /// <summary>
        /// 员工显示工号的位数
        /// </summary>
        private int showEmplLength = -1;

        /// <summary>
        /// 用法列表，用来判断是否是针剂用法
        /// </summary>
        private ArrayList alUsage = null;

        private FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucNewRecipePrint_Load(object sender, System.EventArgs e)
        {
            if (alUsage == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager interMgr=new FS.HISFC.BizProcess.Integrate.Manager();
                alUsage = interMgr.GetConstantList("USAGE");
                if (alUsage == null)
                {
                    MessageBox.Show("获取用法列表出错：" + interMgr.Err);
                    return;
                }

                usageHelper.ArrayObject = alUsage;
            }

            if (printItemNameType == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
            }
        }

        private void Init()
        {
            if (alUsage == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                alUsage = interMgr.GetConstantList("USAGE");
                if (alUsage == null)
                {
                    MessageBox.Show("获取用法列表出错：" + interMgr.Err);
                    return;
                }

                usageHelper.ArrayObject = alUsage;
            }
        }

        /// <summary>
        /// 判断是否针剂用法
        /// </summary>
        /// <param name="usage"></param>
        /// <returns></returns>
        private bool isInjectUsage(string usage)
        {
            if (alUsage == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                alUsage = interMgr.GetConstantList("USAGE");
                if (alUsage == null)
                {
                    MessageBox.Show("获取用法列表出错：" + interMgr.Err);
                    return false;
                }

                usageHelper.ArrayObject = alUsage;
            }
            FS.HISFC.Models.Base.Const usageObj = usageHelper.GetObjectFromID(usage) as FS.HISFC.Models.Base.Const;
            if (usageObj != null)
            {
                switch (usageObj.UserCode)
                {
                    case "IAST"://皮试
                        return true;
                        break;
                    case "IH"://皮下注射
                        return true;
                        break;
                    case "IM"://肌注
                        return true;
                        break;
                    case "IV"://静注
                        return true;
                        break;
                    case "IVD"://静滴
                        return true;
                        break;
                    case "IZ"://肿瘤注射
                        return true;
                        break;
                    case "IO"://其它注射
                        return true;
                        break;
                    default:
                        return false;
                        break;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            if (this.myReg == null)
            {
                return;
            }

            try
            {
                this.label1.Text = this.cnst.GetHospitalName() + "处方笺";

                if (hsDrugQuaulity.ArrayObject.Count <= 0)
                {
                    //取药品剂型
                    ArrayList alDrugQuaulity = cnst.GetAllList("DRUGQUALITY");

                    if (alDrugQuaulity != null && alDrugQuaulity.Count > 0)
                    {
                        hsDrugQuaulity.ArrayObject = alDrugQuaulity;
                    }
                }
            }
            catch
            { }

            ////FS.HISFC.Models.RADT.PatientInfo pinfo = this.myInpatient.QueryComPatientInfo(this.myReg.PID.CardNO);

            //this.lblName.Text = pinfo.Name;

            this.lblName.Text = myReg.Name;

            if (this.myReg.Pact.PayKind.ID == "03")
            {
                try
                {
                    FS.HISFC.BizLogic.Fee.PactUnitInfo pact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                    FS.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(this.myReg.Pact.ID);
                }
                catch
                { }
            }

            //年龄按照统一格式
            this.lblAge.Text = this.orderManager.GetAge(this.myReg.Birthday, false);
            //TimeSpan span = new TimeSpan(this.myInterface.GetDateTimeFromSysDateTime().Ticks - this.myReg.Birthday.Ticks);
            //if (span.Days / 365 <= 0)
            //{
            //    this.lblMonth.Text = System.Convert.ToString(span.Days / 30) + "月";

            //    this.lblDay.Text = System.Convert.ToString(span.Days % 30);
            //}
            //else
            //{
            //    this.lblAge.Text = System.Convert.ToString(span.Days / 365) + "岁";

            //    if (span.Days / 365 < 14)
            //    {
            //        int diff = span.Days - (span.Days / 365) * 365;
            //        if (diff > 30)
            //        {
            //            this.lblMonth.Text = System.Convert.ToString(diff / 30) + "月";
            //        }
            //        this.lblDay.Text = System.Convert.ToString(diff % 30) + "天";
            //    }
            //}


            if (this.myReg.Sex.Name == "男")
            {
                this.chkMale.Checked = true;
                this.chkFemale.Checked = false;
            }
            else
            {
                this.chkMale.Checked = false;
                this.chkFemale.Checked = true;
            }
            this.lblSeeDept.Text = this.myReg.DoctorInfo.Templet.Dept.Name;
            this.lblMCardNo.Text = this.myReg.SSN;
            this.lblCardNo.Text = myReg.PID.CardNO;

            this.lblCard.Text =  "*" + myReg.PID.CardNO + "*";

            this.npbBarCode.Image = this.CreateBarCode(myReg.PID.CardNO);

            this.chkOwn.Checked = false;
            this.chkPay.Checked = false;
            this.chkPub.Checked = false;
            this.chkOth.Checked = false;

            if (this.myReg.Pact.PayKind.ID == "01")
            {
                this.chkOwn.Checked = true;
            }
            else if (this.myReg.Pact.PayKind.ID == "02")
            {
                this.chkPay.Checked = true;
            }
            else
            {
                this.chkPub.Checked = true;
            }
  
            if (myReg.AddressHome != null && myReg.AddressHome.Length > 0)
            {
                this.lblTel.Text = myReg.AddressHome + "/" + myReg.PhoneHome;
            }
            else
            {
                this.lblTel.Text = myReg.PhoneHome;
            }

            #region 诊断

            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(this.myReg.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string strDiag = "";
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += diag.DiagInfo.ICD10.Name + "、";
                }
            }

            lblDiag.Text = strDiag.TrimEnd('、');
            #endregion
        }

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void Query()
        {
            Init();
            if (deptHelper.ArrayObject.Count <= 0)
            {
                FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                deptHelper.ArrayObject = interMgr.GetDepartment();
                if (deptHelper.ArrayObject == null)
                {
                    MessageBox.Show(interMgr.Err);
                }
            }

            //从处方表获取
            ArrayList al = this.orderManager.QueryOrderByRecipeNO(this.myReg.ID,this.recipeId);

            //没有的话从费用表获取
            if (al.Count <= 0)
            {
                ArrayList alFees = new ArrayList();
                alFees = this.outPatientManager.QueryFeeDetailFromRecipeNO(this.recipeId);

                if (alFees == null || alFees.Count <= 0)
                {
                    return;
                }
                else
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFees)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order ot = new FS.HISFC.Models.Order.OutPatient.Order();
                        ot.Item.SysClass = itemlist.Item.SysClass;
                        ot.ReciptNO = itemlist.RecipeNO;
                        ot.DoctorDept = itemlist.RecipeOper.Dept;
                        ot.MOTime = itemlist.ChargeOper.OperTime;
                        ot.StockDept = itemlist.ExecOper.Dept;
                        ot.Patient.PID.ID = itemlist.Patient.ID;
                        ot.Frequency.ID = itemlist.Order.Frequency.ID;
                        ot.Frequency.Name = itemlist.Order.Frequency.Name;
                        ot.Usage = itemlist.Order.Usage;
                        ot.Item.Name = itemlist.Name;
                        ot.HerbalQty = itemlist.Days;
                        ot.ReciptDept = itemlist.RecipeOper.Dept;
                        ot.Combo.ID = itemlist.Order.Combo.ID;
                        ot.Qty = itemlist.Item.Qty;
                        ot.Unit = itemlist.Item.PriceUnit;
                        ot.DoseOnce = itemlist.Order.DoseOnce;
                        ot.DoseUnit = itemlist.Order.DoseUnit;
                        ot.Item.Specs = itemlist.Item.Specs;
                        ot.Item.ItemType = itemlist.Item.ItemType;
                        al.Add(ot);
                    }
                }
            }
            if (this.alOrder.Count > 0)
            {
                this.alOrder.Clear();
            }
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in al)
            {
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.alOrder.Add(order);
                }
            }
            this.MakaLabel(this.alOrder);
        }

        /// <summary>
        /// 返回组号
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int GetSubCombNo(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            int subCombNo = 1;
            Hashtable hsCombNo = new Hashtable();
            
            foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alOrder)
            {
                if (ord.SubCombNO < order.SubCombNO && !hsCombNo.Contains(ord.Combo.ID))
                {
                    hsCombNo.Add(ord.Combo.ID, ord);
                    subCombNo++;
                }
            }

            return subCombNo;
        }

        OrderCompare orderCompare = new OrderCompare();

        /// <summary>
        /// 门诊处方调剂实体
        /// </summary>
        FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = new FS.HISFC.Models.Pharmacy.DrugRecipe();

        /// <summary>
        /// 药房编码
        /// </summary>
        string drugDept = ""; 
        
        /// <summary>
        /// 操作员
        /// </summary>
        FS.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// 人员列表
        /// </summary>
        Hashtable hsEmpl = new Hashtable();

        /// <summary>
        /// 生成显示信息
        /// </summary>
        /// <param name="alOrder"></param>
        private void MakaLabel(ArrayList alOrder)
        {
            if (alOrder == null)
            {
                return;
            }

            alTemp = new ArrayList();
            for (int i = 0; i < alOrder.Count; i++)
            {
                this.alTemp.Add(alOrder[i]);
            }
            hsCombID = new Hashtable();
            
            StringBuilder buffer = new System.Text.StringBuilder();
            //填充数据
            string[] parm = { "  ", "┌", "│", "└" };

            decimal phaMoney = 0m;//药费
            decimal ownPhaMoney = 0m;//自费药费
            decimal injectMoney = 0m;//注射费
            decimal ownInjectMoney = 0m;//自费注射费

            string drugType = "";

            if (showEmplLength == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                showEmplLength = controlMgr.GetControlParam<int>("HN0002", true, 6);
            }

            # region /*画组合符号*/

            //按照sortID排序
            alOrder.Sort(orderCompare);
            FS.HISFC.Models.Order.OutPatient.Order order = null;
            for (int i = 0; i < alOrder.Count; i++)
            {
                order = alOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                if (order == null)
                {
                    continue;
                }
                if (i == 0)
                {
                    drugType = order.Item.SysClass.ID.ToString();
                    this.lblRecipeNo.Text = "*" + order.ReciptNO + "*";
                    this.npbRecipeNo.Image = this.CreateBarCode(order.ReciptNO);

                    if (hsEmpl.Contains(order.ReciptDoctor.ID))
                    {
                        oper = hsEmpl[order.ReciptDoctor.ID] as FS.HISFC.Models.Base.Employee;
                    }
                    else
                    {
                        oper = this.personManager.GetPersonByID(order.ReciptDoctor.ID);
                    }

                    if (oper != null && oper.UserCode.Length > 0)
                    {
                        this.lblPhaDoc.Text = oper.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + order.ReciptDoctor.Name;
                    }
                    else
                    {
                        this.lblPhaDoc.Text = order.ReciptDoctor.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + order.ReciptDoctor.Name;
                    }
                    this.lblSeeYear.Text = order.MOTime.Date.ToString("yyyy");
                    this.lblSeeMonth.Text = order.MOTime.Date.ToString("MM");
                    this.lblSeeDay.Text = order.MOTime.Date.ToString("dd");
                    this.myReg = this.regManager.GetByClinic(this.myReg.ID);
                    this.SetPatientInfo();
                    this.currentRecipe = this.drugManager.GetDrugRecipe(order.StockDept.ID, "M1", this.recipeId, "1");

                    drugDept = order.StockDept.ID;
                }

                string printUsage = "";
                ArrayList al = this.GetOrderByCombId(order, ref printUsage);
                if (al.Count == 0)
                {
                    continue;
                }
                else
                {
                    #region 计算费用
                    if (order.InjectCount > 0)
                    {
                        ArrayList alOrderAndSub = this.outPatientManager.QueryFeeDetailbyComoNOAndClinicCode(order.Combo.ID, this.myReg.ID);

                        if (alOrderAndSub != null && alOrderAndSub.Count > 1)
                        {
                            //this.RemoveOrderFromArray(alOrder, ref alOrderAndSub);

                            if (alOrderAndSub.Count > 0)
                            {
                                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alOrderAndSub)
                                {
                                    itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
                                }

                                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alOrderAndSub)
                                {
                                    injectMoney += item.FT.PubCost + item.FT.PayCost + item.FT.OwnCost;
                                    ownInjectMoney += item.FT.OwnCost;
                                }
                            }

                        }
                    }
                    #endregion

                    #region 计算费用

                    try
                    {
                        ArrayList alFee;
                        alFee = this.outPatientManager.QueryFeeDetailbyComoNOAndClinicCode(order.Combo.ID, this.myReg.ID);
                        if (alFee != null && alFee.Count >= 1)
                        {
                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                            {
                                itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
                                phaMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;
                                ownPhaMoney += itemlist.FT.OwnCost;
                            }
                        }
                    }
                    catch
                    { }

                    #endregion

                    #region 形成字符串

                    if (drugType == "PCC")
                    {
                        #region 草药处理

                        decimal days = 1m;
                        string freq = "";
                        string usage = "";

                        for (int k = 0; k < al.Count; k++)
                        {
                            order = al[k] as FS.HISFC.Models.Order.OutPatient.Order;

                            days = order.HerbalQty;
                            freq = order.Frequency.Name;

                            try
                            {
                                //草药用法只有系统类别为HJZ的才是正常用法，特殊用法不统一打印
                                if (((FS.HISFC.Models.Base.Const)usageHelper.GetObjectFromID(order.Usage.ID)).UserCode == "HJZ")
                                {
                                    usage = order.Usage.Name;
                                }
                            }
                            catch
                            {
                                usage = order.Usage.Name;
                            }

                            string priceAndCost = "";

                            if (this.bDrugBill)
                            {
                                System.Text.StringBuilder buff = new System.Text.StringBuilder();

                                if (order.Memo != "")
                                {
                                    //判断是否是煎药方式
                                    if ("自煎、代煎、复渣、代复渣".Contains(order.Usage.Memo))
                                    {
                                        buff.Append(order.Item.Name);
                                    }
                                    else
                                    {
                                        buff.Append(order.Item.Name + "(" + order.Memo + ")");
                                    }
                                }
                                else
                                {
                                    buff.Append(order.Item.Name);
                                }

                                buff.Append("  ");

                                string name = buff.ToString();
                                string dose = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;

                                try
                                {
                                    int len = System.Text.Encoding.GetEncoding("gb2312").GetBytes(name).Length;
                                    string pad = "";
                                    if (len > 8)
                                    {
                                        len = 8;
                                        pad = new string(' ', 8 - len);
                                    }
                                    else
                                    {
                                        pad = new string(' ', 8 - len);
                                    }
                                    buffer.Append(name + pad);

                                    len = System.Text.Encoding.GetEncoding("gb2312").GetBytes(dose).Length;
                                    if (len > 3)
                                    {
                                        len = 3;
                                        pad = new string(' ', 3 - len);
                                    }
                                    else
                                    {
                                        pad = new string(' ', 3 - len);
                                    }
                                    buffer.Append((dose + pad));

                                }
                                catch { }
                                if ((k + 1) % 2 == 0 || k == al.Count - 1)
                                {
                                    buffer.Append("\n");
                                }
                            }
                            else
                            {
                                System.Text.StringBuilder buff = new System.Text.StringBuilder();

                                if (order.Memo != "")
                                {
                                    //判断是否是煎药方式
                                    if ("自煎、代煎、复渣、代复渣".Contains(order.Usage.Memo))
                                    {
                                        buff.Append(order.Item.Name);
                                    }
                                    else
                                    {
                                        buff.Append(order.Item.Name + "(" + order.Memo + ")");
                                    }
                                }
                                else
                                {
                                    buff.Append(order.Item.Name);
                                }

                                //特殊用法
                                try
                                {
                                    //草药用法只有系统类别为HJZ的才是正常用法，特殊用法不统一打印
                                    if (((FS.HISFC.Models.Base.Const)usageHelper.GetObjectFromID(order.Usage.ID)).UserCode != "HJZ")
                                    {
                                        buff.Append("(" + order.Usage.Name + ")");
                                    }
                                }
                                catch
                                {
                                }

                                buff.Append("  ");

                                string name = buff.ToString();
                                string dose = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;
                                string cost = "  " + priceAndCost;

                                try
                                {
                                    buffer.Append(this.SetToByteLength(name + dose, 30));
                                }
                                catch { }
                                if ((k + 1) % 2 == 0 || k == al.Count - 1)
                                {
                                    buffer.Append("\n\n");
                                }
                            }
                        }

                        buffer.Append("             ");
                        buffer.Append(days.ToString());
                        buffer.Append("剂");
                        buffer.Append("\n");
                        buffer.Append("       用法: ");
                        //buffer.Append("每日一剂");
                        buffer.Append("每日" + GetFrequencyCount(order.Frequency.ID) + "剂");
                        buffer.Append(" ");
                        buffer.Append(usage);

                        //判断是否是煎药方式
                        if ("自煎、代煎、复渣、代复渣".Contains(order.Usage.Memo))
                        {
                            buffer.Append("(" + order.Memo + ")");
                        }

                        #endregion
                    }
                    else
                    {
                        #region 西药、成药

                        //皮试信息
                        string hyTest = "";

                        for (int k = 0; k < al.Count; k++)
                        {
                            order = al[k] as FS.HISFC.Models.Order.OutPatient.Order;

                            string combLabel = "";
                            hyTest = "";

                            if (al.Count == 1)
                            {
                                buffer.Append(parm[0]);

                                combLabel = parm[0];
                            }
                            else
                            {
                                if (k == 0)
                                {
                                    buffer.Append(parm[1]);

                                    combLabel = parm[1];
                                }
                                else if (k == al.Count - 1)
                                {
                                    buffer.Append(parm[3]);

                                    combLabel = parm[3];
                                }
                                else
                                {
                                    buffer.Append(parm[2]);

                                    combLabel = parm[2];
                                }
                            }

                            //buffer.Append("组[");
                            buffer.Append("[");
                            buffer.Append(this.GetSubCombNo(order).ToString());
                            buffer.Append("]");

                            FS.HISFC.Models.Pharmacy.Item phaItem = phaManager.GetItem(order.Item.ID);

                            if (phaItem != null)
                            {
                                if (this.bEnglish)
                                {
                                    buffer.Append(phaItem.NameCollection.EnglishName);
                                }
                                else
                                {
                                    if (printItemNameType == -1)
                                    {
                                        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                                        printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
                                    }

                                    if (printItemNameType == 0)
                                    {
                                        buffer.Append(phaItem.Name);
                                    }
                                    else
                                    {
                                        //2011-6-10 houwb 通用名没有维护时，打印商品名
                                        if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                        {
                                            buffer.Append(phaItem.Name);
                                        }
                                        else
                                        {
                                            buffer.Append(phaItem.NameCollection.RegularName);
                                        }
                                    }
                                }

                                FS.FrameWork.Models.NeuObject quaulity = hsDrugQuaulity.GetObjectFromID(phaItem.Quality.ID);

                                if (quaulity != null && quaulity.ID.Length > 0)
                                {
                                    //处方规范规定，麻醉药品和一类精神药品印刷为“麻、精一”
                                    if (quaulity.Memo.Trim().IndexOf("精一") >= 0
                                        || quaulity.Memo.Trim().IndexOf("麻") >= 0)
                                    {
                                        speRecipeLabel = "麻、精一";
                                    }
                                    else if (quaulity.Memo.Trim().IndexOf("精二") >= 0)
                                    {
                                        speRecipeLabel = "精 二";
                                    }
                                }
                            }
                            else
                            {
                                buffer.Append(order.Item.Name);
                            }
                            buffer.Append(" ");


                            FS.HISFC.BizLogic.Order.OutPatient.Order ord = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                            string specs = "";

                            //妇幼特殊需求：组合的药品不显示基本剂量和总量
                            if (combLabel == parm[0])
                            {
                                specs = phaItem.BaseDose + phaItem.DoseUnit;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(order.DoseOnceDisplay))
                                {
                                    specs = order.DoseOnceDisplay.Trim() + order.DoseUnit;
                                }
                                else
                                {
                                    specs = order.DoseOnce + order.DoseUnit;
                                }
                            }
                            buffer.Append(specs);
                            if (combLabel == parm[0])
                            {
                                buffer.Append("×");
                                if (this.bEnglish)
                                {
                                    buffer.Append(order.Qty.ToString());
                                }
                                else
                                {
                                    buffer.Append(order.Qty.ToString() + order.Unit);
                                }
                            }

                            buffer.Append(" ");
                            if (order.InjectCount > 0)
                            {
                                //妇幼无院注次数显示，此处先屏蔽
                                //buffer.Append("  院注:" + order.InjectCount.ToString() + "次");
                            }

                            buffer.Append("\n");

                            if (combLabel == parm[0])
                            {
                                if (this.bEnglish)
                                {
                                    buffer.Append("    Usage:");
                                }
                                else
                                {
                                    buffer.Append("    用法:");
                                }
                                buffer.Append(" ");

                                if (!string.IsNullOrEmpty(order.DoseOnceDisplay))
                                {
                                    buffer.Append(order.DoseOnceDisplay.Trim());
                                }
                                else
                                {
                                    buffer.Append(order.DoseOnce.ToString());
                                }

                                buffer.Append(order.DoseUnit);

                                buffer.Append(" ");

                                if (this.bEnglish)
                                {
                                    buffer.Append(order.Usage.ID);
                                }
                                else
                                {
                                    buffer.Append(order.Usage.Name);
                                }
                                buffer.Append("   ");

                                if ((order.Frequency.Name == null || order.Frequency.Name.Length <= 0) && this.freHelper != null && this.freHelper.ArrayObject.Count > 0)
                                {
                                    FS.FrameWork.Models.NeuObject objFre = this.freHelper.GetObjectFromID(order.Frequency.ID);
                                    if (objFre != null)
                                    {
                                        if (this.bEnglish)
                                        {
                                            buffer.Append(objFre.ID.ToLower());
                                        }
                                        else
                                        {
                                            buffer.Append(objFre.Name);
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.bEnglish)
                                    {
                                        buffer.Append(order.Frequency.ID.ToLower());
                                    }
                                    else
                                    {
                                        buffer.Append(order.Frequency.ID.ToLower());
                                    }
                                }

                                buffer.Append("×");

                                if (order.HerbalQty <= 0)
                                    order.HerbalQty = 1;

                                buffer.Append(order.HerbalQty.ToString());

                                buffer.Append(" ");

                                //if (phaItem.IsAllergy)
                                //{
                                //    hyTest = this.orderManager.TransHypotest(order.HypoTest);
                                //    if (!string.IsNullOrEmpty(hyTest))
                                //    {
                                //        buffer.Append(hyTest + " ");
                                //    }
                                //}

                                buffer.Append(order.Memo);

                                buffer.Append("\n");

                                buffer.Append("\n");
                            }
                            else if (combLabel == parm[3])
                            {
                                for (int kk = 0; kk < al.Count; kk++)
                                {
                                    FS.HISFC.Models.Order.OutPatient.Order order1 = al[kk] as FS.HISFC.Models.Order.OutPatient.Order;

                                    if (kk == 0)
                                    {
                                        if (this.bEnglish)
                                        {
                                            buffer.Append("    Usage:");
                                        }
                                        else
                                        {
                                            buffer.Append("    用法:");
                                        }
                                        buffer.Append(" ");

                                        #region 用法这里不再打印项目名称

                                        //FS.HISFC.Models.Pharmacy.Item phaItem1 = phaManager.GetItem(order1.Item.ID);

                                        //if (phaItem1 != null)
                                        //{
                                        //    if (this.bEnglish)
                                        //    {
                                        //        buffer.Append(phaItem1.NameCollection.EnglishName);
                                        //    }
                                        //    else
                                        //    {
                                        //        buffer.Append(phaItem1.NameCollection.RegularName);
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    buffer.Append(order1.Item.Name);
                                        //}
                                        //buffer.Append(" ");
                                        #endregion

                                        buffer.Append(printUsage);

                                        buffer.Append(" ");

                                        if ((order1.Frequency.Name == null
                                            || order1.Frequency.Name.Length <= 0)
                                            && this.freHelper != null
                                            && this.freHelper.ArrayObject.Count > 0)
                                        {
                                            FS.FrameWork.Models.NeuObject objFre = this.freHelper.GetObjectFromID(order1.Frequency.ID);
                                            if (objFre != null)
                                            {
                                                if (this.bEnglish)
                                                {
                                                    buffer.Append(objFre.ID.ToLower());
                                                }
                                                else
                                                {
                                                    buffer.Append(objFre.ID.ToLower());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (this.bEnglish)
                                            {
                                                buffer.Append(order1.Frequency.ID.ToLower());
                                            }
                                            else
                                            {
                                                buffer.Append(order1.Frequency.ID.ToLower());
                                            }
                                        }

                                        buffer.Append("×");

                                        if (order.HerbalQty <= 0)
                                            order.HerbalQty = 1;

                                        buffer.Append(order.HerbalQty.ToString());

                                        buffer.Append(" ");

                                        //if (phaItem.IsAllergy)
                                        //{
                                        //    hyTest = this.orderManager.TransHypotest(order.HypoTest);
                                        //    if (!string.IsNullOrEmpty(hyTest))
                                        //    {
                                        //        buffer.Append(hyTest + " ");
                                        //    }
                                        //}

                                        buffer.Append(order1.Memo);

                                        buffer.Append("\n");
                                    }
                                    #region 一组只显示一个用法即可
                                    //else
                                    //{
                                    //    buffer.Append("        ");

                                    //    FS.HISFC.Models.Pharmacy.Item phaItem1 = phaManager.GetItem(order1.Item.ID);

                                    //    if (phaItem1 != null)
                                    //    {
                                    //        if (this.bEnglish)
                                    //        {
                                    //            buffer.Append(phaItem1.NameCollection.EnglishName);
                                    //        }
                                    //        else
                                    //        {
                                    //            buffer.Append(phaItem1.NameCollection.RegularName);
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        buffer.Append(order1.Item.Name);
                                    //    }
                                    //    buffer.Append(" ");

                                    //    if (order1.Usage.Name != "外用" && order1.Usage.Name != "喷喉" && order1.Usage.Name != "灌肠" &&
                                    //        order1.Usage.Name != "滴耳" && order1.Usage.Name != "滴眼" && order1.Usage.Name != "滴鼻" &&
                                    //        order1.Usage.Name != "喷眼" && order1.Usage.Name != "喷鼻")
                                    //    {
                                    //if (!string.IsNullOrEmpty(order.DoseOnceDisplay))
                                    //{
                                    //    buffer.Append(order.DoseOnceDisplay.Trim());
                                    //}
                                    //else
                                    //{
                                    //        buffer.Append(order1.DoseOnce.ToString());
                                    //}
                                    //        buffer.Append(order1.DoseUnit);

                                    //        buffer.Append(" ");
                                    //    }

                                    //    if (this.bEnglish)
                                    //    {
                                    //        buffer.Append(order1.Usage.ID);
                                    //    }
                                    //    else
                                    //    {
                                    //        buffer.Append(order1.Usage.Name);
                                    //    }
                                    //    buffer.Append(" ");

                                    //    if ((order1.Frequency.Name == null || order1.Frequency.Name.Length <= 0) && this.freHelper != null && this.freHelper.ArrayObject.Count > 0)
                                    //    {
                                    //        FS.FrameWork.Models.NeuObject objFre = this.freHelper.GetObjectFromID(order1.Frequency.ID);
                                    //        if (objFre != null)
                                    //        {
                                    //            if (this.bEnglish)
                                    //            {
                                    //                buffer.Append(objFre.ID);
                                    //            }
                                    //            else
                                    //            {
                                    //                buffer.Append(objFre.ID);
                                    //            }
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        buffer.Append(order1.Frequency.ID);
                                    //    }

                                    //    buffer.Append("×");

                                    //    if (order.HerbalQty <= 0)
                                    //        order.HerbalQty = 1;

                                    //    buffer.Append(order.HerbalQty.ToString());

                                    //    buffer.Append(" ");

                                    //    buffer.Append(order.Memo);

                                    //    buffer.Append("\n");
                                    //}
                                    #endregion
                                }
                                buffer.Append("\n");
                            }
                            //buffer.Append("\n");
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            #endregion

            buffer.Append("\n以下空白");

            this.lblOrder.Text = buffer.ToString();

            this.lblPhaMoney.Text = phaMoney.ToString();
            this.drugRecipe = drugManager.GetDrugRecipe(this.drugDept, this.recipeId);
            if (this.drugDept == null)
            {
                MessageBox.Show("查询处方发药信息错误：" + drugManager.Err);
                return;
            }

            if (!string.IsNullOrEmpty(drugRecipe.DrugedOper.ID))
            {
                if (hsEmpl.Contains(drugRecipe.DrugedOper.ID))
                {
                    oper = hsEmpl[drugRecipe.DrugedOper.ID] as FS.HISFC.Models.Base.Employee;
                }
                else
                {
                    oper = this.personManager.GetPersonByID(drugRecipe.DrugedOper.ID);
                    this.hsEmpl.Add(drugRecipe.DrugedOper.ID, oper.Clone());
                }
                this.lblDrugDoct.Text = oper.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + oper.Name;
            }
            else
            {
                this.lblDrugDoct.Text = "";
            }

            if (!string.IsNullOrEmpty(drugRecipe.SendOper.ID))
            {
                if (hsEmpl.Contains(drugRecipe.SendOper.ID))
                {
                    oper = hsEmpl[drugRecipe.SendOper.ID] as FS.HISFC.Models.Base.Employee;
                }
                else
                {
                    oper = this.personManager.GetPersonByID(drugRecipe.SendOper.ID);
                    this.hsEmpl.Add(drugRecipe.SendOper.ID, oper.Clone());
                }
                this.lblSendDoct.Text = oper.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + oper.Name;
            }
            else
            {
                this.lblSendDoct.Text = "";
            }

            if (this.isPrintSignInfo == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                isPrintSignInfo = controlMgr.GetControlParam<int>("HNMZ12", true, 0);
            }

            if (isPrintSignInfo == 1)
            {
                this.lblPhaDoc.Visible = true;
                this.lblDrugDoct.Visible = true;
                this.lblSendDoct.Visible = true;
                this.lblPhaMoney.Visible = true;
            }
            else
            {
                this.lblPhaDoc.Visible = false;
                this.lblDrugDoct.Visible = false;
                this.lblSendDoct.Visible = false;
                this.lblPhaMoney.Visible = false;
            }
        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }

        /// <summary>
        /// 判断是否为双字符的正则表达式
        /// </summary>
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{15,18}$");

        /// <summary>
        /// 获取字节长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padLength"></param>
        /// <returns></returns>
        private string SetToByteLength(string str,int padLength)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[^\x00-\xff]"))
                {
                    len += 1;
                }
            }

            if (padLength - str.Length - len > 0)
            {
                return str + "".PadRight(padLength - str.Length - len, ' ');
            }
            else
            {
                return str;
            }
        }

        UsageCompare compare = new UsageCompare();
        ArrayList alSort = new ArrayList();

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <param name="usageID">一组统一显示的用法</param>
        /// <returns></returns>
        private ArrayList GetOrderByCombId(FS.HISFC.Models.Order.OutPatient.Order order, ref string usage)
        {
            ArrayList al = new ArrayList();
            //alSort = new ArrayList();


            //for (int i = 0; i < this.alTemp.Count; i++)
            //{
            //    FS.HISFC.Models.Order.OutPatient.Order ord = alTemp[i] as FS.HISFC.Models.Order.OutPatient.Order;
            //    if (order.Combo.ID == ord.Combo.ID)
            //    {
            //        al.Add(ord);
            //        alSort.Add(ord);
            //        //alTemp.Remove(ord);
            //    }
            //}

            //for (int j = 0; j < al.Count; j++)
            //{
            //    alTemp.Remove(al[j]);
            //}

            //if (alSort.Count > 0)
            //{
            //    alSort.Sort(compare);
            //    usage = ((FS.HISFC.Models.Order.OutPatient.Order)alSort[0]).Usage.Name;
            //}

            //al.Sort(orderCompare);

            FS.HISFC.Models.Order.OutPatient.Order ord = null;
            if (!hsCombID.Contains(order.Combo.ID))
            {
                for (int i = 0; i < this.alTemp.Count; i++)
                {
                    ord = alTemp[i] as FS.HISFC.Models.Order.OutPatient.Order;
                    if (order.Combo.ID == ord.Combo.ID)
                    {
                        al.Add(ord);
                    }
                }
                hsCombID.Add(order.Combo.ID, al);
            }
            //else
            //{
            //    al = hsCombID[order.Combo.ID] as ArrayList;
            //}

            if (al.Count > 0)
            {
                al.Sort(compare);
                usage = ((FS.HISFC.Models.Order.OutPatient.Order)al[0]).Usage.Name;
                al.Sort(orderCompare);
            }

            return al;
        }

        /// <summary>
        /// print the recipe
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //GetPageSize("MZRecipe", ref print);
            print.PrintPage(0, 0, this);
        }
        /// <summary>
        /// print the recipe 套打
        /// </summary>
        public void Print1()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
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
            label22.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label27.Visible = false;
            label14.Visible = false;
            label31.Visible = false;
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            this.panel5.Visible = false;


            if (this.myReg.DoctorInfo.Templet.Dept.Name.IndexOf("儿") >= 0)
            {
                this.lblCardNo.Location = new Point(266, 160);
                this.lblSeeDept.Location = new Point(514, 160);
                //this.lblMonth.Visible = true;
                //this.lblDay.Visible = true;
                this.pnlSex.Location = new System.Drawing.Point(250, 137);
            }
            else
            {
                this.lblCardNo.Location = new System.Drawing.Point(176, 160);
                this.lblSeeDept.Location = new System.Drawing.Point(464, 160);
                //this.lblMonth.Visible = false;
                //this.lblDay.Visible = false;
                this.pnlSex.Location = new System.Drawing.Point(350, 132);
            }

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            System.Drawing.Printing.PaperSize psize = new System.Drawing.Printing.PaperSize("Recipe", 787, 1200);
            print.SetPageSize(psize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            #region 横打
            //			System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
            //			pSet.Landscape = true;
            //			print.PrintDocument.DefaultPageSettings.Landscape = true;
            //          print.PrintPage(560,45,this);
            #endregion

            print.PrintPage(90, 40, this);
        }

        /// <summary>
        ///打印预览 
        /// </summary>
        public int PrintView()
        {
            this.chkPub.Text = "公费";
            this.chkPay.Text = "医保";
            this.chkOwn.Text = "自费";
            this.chkOth.Text = "其他";
            this.chkMale.Text = "男";
            this.chkFemale.Text = "女";

            if (this.bEnglish)
            {
                this.ChangeToEnglish();
            }

            #region 显示体重信息

            FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
            string height = "";
            string weight = "";
            string SBP = "";
            string DBP = "";
            string tem = "";//体温
            string bloodGlu = ""; //血糖
            if (outOrderMgr.GetHealthInfo(this.myReg.ID, ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu) == -1)
            {
                this.lblHeight.Text = "";
            }
            else
            {
                if (string.IsNullOrEmpty(weight))
                {
                    this.lblHeight.Text = "";
                }
                else
                {
                    this.lblHeight.Text = "体重" + weight.ToString() + "千克";
                }
            }
            #endregion

            if (this.speRecipeLabel.Length <= 0)
            {
                if (this.alOrder == null || this.alOrder.Count <= 0)
                {
                    return 1;
                }
                //判断是否急诊？ 妇幼要求在急诊时间段都是打印急诊处方
                if (this.regIntegrate.IsEmergency((this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID, (this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).MOTime))
                {
                    this.speRecipeLabel = "急 诊";
                    if (string.IsNullOrEmpty(this.lblHeight.Text))
                    {
                        this.lblHeight.Text = "体重    千克";
                    }
                }
                //儿科和急诊科显示体重信息
                //else if (this.myReg.DoctorInfo.Templet.Dept.Name.IndexOf("急") >= 0)
                //根据第一条医嘱的开立科室判断 houwb
                else if (deptHelper.GetObjectFromID((this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID).Name.IndexOf("急") >= 0)
                {
                    this.speRecipeLabel = "急 诊";
                    if (string.IsNullOrEmpty(this.lblHeight.Text))
                    {
                        this.lblHeight.Text = "体重    千克";
                    }
                }
                else if (deptHelper.GetObjectFromID((this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID).Name.IndexOf("儿") >= 0)
                {
                    this.speRecipeLabel = "儿 科";
                    //this.lblMonth.Visible = true;
                    //this.lblDay.Visible = true;
                    if (string.IsNullOrEmpty(this.lblHeight.Text))
                    {
                        this.lblHeight.Text = "体重    千克";
                    }
                }
            }
            //调整控件位置
            if (string.IsNullOrEmpty(this.lblHeight.Text))
            {
                this.label12.Location = new Point(6, 164);
                this.lblCardNo.Location = new Point(122, 166);
                this.label10.Location = new Point(271, 164);
                this.lblSeeDept.Location = new Point(402, 165);
            }
            else
            {
                this.label12.Location = new Point(108, 164);
                this.lblCardNo.Location = new Point(224, 166);
                this.label10.Location = new Point(332, 164);
                this.lblSeeDept.Location = new Point(464, 165);
            }

            if (this.speRecipeLabel.Length > 0)
            {
                this.lblJS.Visible = true;
            }
            else
            {
                this.lblJS.Visible = false;
            }

            this.lblJS.Text = this.speRecipeLabel;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //System.Drawing.Printing.PaperSize psize = new System.Drawing.Printing.PaperSize("rerer", 827, 827);
            //print.SetPageSize(psize);

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            #region 横打
            //System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
            //pSet.Landscape = true;
            print.PrintDocument.DefaultPageSettings.Landscape = true;
            //print.PrintPage(580, 1, this);

            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            //print.PrintPage(0, 0, this);
            print.PrintPreview(580, 1, this);
            #endregion

            return 1;
        }

        /// <summary>
        /// print the recipe 白纸打印
        /// </summary>
        public void Print2()
        {
            this.chkPub.Text = "公费";
            this.chkPay.Text = "医保";
            this.chkOwn.Text = "自费";
            this.chkOth.Text = "其他";
            this.chkMale.Text = "男";
            this.chkFemale.Text = "女";

            if (this.bEnglish)
            {
                this.ChangeToEnglish();
            }

            #region 显示体重信息

            FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
            string height = "";
            string weight = "";
            string SBP = "";
            string DBP = "";
            string tem = "";//体温
            string bloodGlu = ""; //血糖
            if (outOrderMgr.GetHealthInfo(this.myReg.ID, ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu) == -1)
            {
                this.lblHeight.Text = "";
            }
            else
            {
                if (string.IsNullOrEmpty(weight))
                {
                    this.lblHeight.Text = "";
                }
                else
                {
                    this.lblHeight.Text = "体重" + weight.ToString() + "千克";
                }
            }
            #endregion

            if (this.speRecipeLabel.Length <= 0)
            {
                if(this.alOrder==null || this.alOrder.Count<=0)
                {
                    return;
                }
                //判断是否急诊？ 妇幼要求在急诊时间段都是打印急诊处方
                if (this.regIntegrate.IsEmergency((this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID,(this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).MOTime))
                {
                    this.speRecipeLabel = "急 诊";
                    if (string.IsNullOrEmpty(this.lblHeight.Text))
                    {
                        this.lblHeight.Text = "体重    千克";
                    }
                }
                //儿科和急诊科显示体重信息
                //else if (this.myReg.DoctorInfo.Templet.Dept.Name.IndexOf("急") >= 0)
                //根据第一条医嘱的开立科室判断 houwb
                else if (deptHelper.GetObjectFromID((this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID).Name.IndexOf("急") >= 0)
                {
                    this.speRecipeLabel = "急 诊";
                    if (string.IsNullOrEmpty(this.lblHeight.Text))
                    {
                        this.lblHeight.Text = "体重    千克";
                    }
                }
                else if (deptHelper.GetObjectFromID((this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID).Name.IndexOf("儿") >= 0)
                {
                    this.speRecipeLabel = "儿 科";
                    //this.lblMonth.Visible = true;
                    //this.lblDay.Visible = true;
                    if (string.IsNullOrEmpty(this.lblHeight.Text))
                    {
                        this.lblHeight.Text = "体重    千克";
                    }
                }
            }
            //调整控件位置
            if (string.IsNullOrEmpty(this.lblHeight.Text))
            {
                this.label12.Location = new Point(6, 164);
                this.lblCardNo.Location = new Point(122, 166);
                this.label10.Location = new Point(271, 164);
                this.lblSeeDept.Location = new Point(402, 165);
            }
            else
            {
                this.label12.Location = new Point(108, 164);
                this.lblCardNo.Location = new Point(224, 166);
                this.label10.Location = new Point(332, 164);
                this.lblSeeDept.Location = new Point(464, 165);
            }
            
            if (this.speRecipeLabel.Length > 0)
            {
                this.lblJS.Visible = true;
            }
            else
            {
                this.lblJS.Visible = false;
            }

            this.lblJS.Text = this.speRecipeLabel;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //System.Drawing.Printing.PaperSize psize = new System.Drawing.Printing.PaperSize("rerer", 827, 827);
            //print.SetPageSize(psize);

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            #region 横打
            //System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
            //pSet.Landscape = true;
            print.PrintDocument.DefaultPageSettings.Landscape = true;
            //print.PrintPage(580, 1, this);

            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            //print.PrintPage(0, 0, this);
            print.PrintPage(580, 1, this);
            #endregion
        }
        /// <summary>
        /// 转换成英文
        /// </summary>
        private void ChangeToEnglish()
        {
            label14.Text = "FeeType:";
            label5.Text = "RecipeNum:";
            label9.Text = "MedicalNum:";
            label6.Text = "Name:";
            label7.Text = "Sex:";
            label8.Text = "Age:";
            label10.Text = "Dept:";
            label12.Text = "CardNum:";
            label19.Text = "Diagnose:";
            label31.Text = "Date:";
            label20.Text = "Phone/Address:";
            label22.Text = "Doctor:";
            label3.Text = "Cost:";
            label24.Text = "Audit Apothecary:";
            label25.Text = "Prepare Apothecary:";
            label27.Text = "Check Apothecary:";

            this.chkOwn.Text = "OwnCost";
            this.chkPub.Text = "PubCost";
            this.chkPay.Text = "PayCost";
            this.chkOth.Text = "Ohter";
            this.chkMale.Text = "Male";
            this.chkFemale.Text = "Female";

            label1.Text = "         Recipe";
            lblJS.Text = "";
        }
        /// <summary>
        /// 从消耗品和医嘱数组中移除医嘱
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="alOrderAndSub"></param>
        private void RemoveOrderFromArray(ArrayList alOrder, ref ArrayList alOrderAndSub)
        {
            if (alOrder == null || alOrder.Count == 0)
            {
                return;
            }
            if (alOrderAndSub == null || alOrderAndSub.Count == 0)
            {
                return;
            }
            ArrayList alTemp = new ArrayList();
            for (int i = 0; i < alOrderAndSub.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item = alOrderAndSub[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                for (int j = 0; j < alOrder.Count; j++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as FS.HISFC.Models.Order.OutPatient.Order;
                    if ((temp.ReciptNO == item.RecipeNO) && (temp.SequenceNO == item.SequenceNO))
                    {
                        item.Item.MinFee.User03 = "1";
                    }
                }
            }
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alOrderAndSub)
            {
                if (item.Item.MinFee.User03 != "1")
                {
                    alTemp.Add(item);
                }
            }
            alOrderAndSub = alTemp;
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
                FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID.ToLower());

                if (alFrequency != null && alFrequency.Count > 0)
                {
                    FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');
                    return str.Length;
                }
                else
                {
                    alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID.ToUpper());
                    if (alFrequency != null && alFrequency.Count > 0)
                    {
                        FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                        string[] str = obj.Time.Split('-');
                        return str.Length;
                    }
                }

                return -1;
            }
        }

        #region IRecipePrint 成员

        /// <summary>
        /// 处方号
        /// </summary>
        private string recipeId = "";

        public int PrintRecipe()
        {
            this.Print2();
            return 1;
        }

        public int PrintRecipeView(System.Collections.ArrayList alRecipe)
        {
            this.PrintView();
            return 1;
        }

        public string RecipeNO
        {
            get
            {
                return this.recipeId;
            }
            set
            {
                this.recipeId = value;
                this.speRecipeLabel = "";
                this.Query();
            }
        }

        public int SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.myReg = register;
            return 1;
        }

        #endregion

        #region IDrugPrint 成员

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new NotImplementedException();
        }

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            throw new NotImplementedException();
        }

        public void AddAllData(ArrayList al)
        {
            throw new NotImplementedException();
        }

        public void AddCombo(ArrayList alCombo)
        {
            throw new NotImplementedException();
        }

        public void AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            throw new NotImplementedException();
        }

        public decimal DrugTotNum
        {
            set { throw new NotImplementedException(); }
        }

        public FS.HISFC.Models.RADT.PatientInfo InpatientInfo
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

        public decimal LabelTotNum
        {
            set { throw new NotImplementedException(); }
        }

        public FS.HISFC.Models.Registration.Register OutpatientInfo
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

        public void Preview()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IRecipePrint 成员

        /// <summary>
        /// 打印机名称
        /// </summary>
        private string printer = "";

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

        #endregion
    }

    public class OrderCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.OutPatient.Order order1 = x as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Order.OutPatient.Order order2 = y as FS.HISFC.Models.Order.OutPatient.Order;

                if (order1.SortID > order2.SortID)
                {
                    return 1;
                }
                else if (order1.SortID == order2.SortID)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// 用法排序：一组内可能用法不一致，此处只显示一个用法
    /// </summary>
    public class UsageCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.OutPatient.Order order1 = x as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Order.OutPatient.Order order2 = y as FS.HISFC.Models.Order.OutPatient.Order;

                if (FS.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) < FS.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
                {
                    return 1;
                }
                else if (FS.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) == FS.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }
}
