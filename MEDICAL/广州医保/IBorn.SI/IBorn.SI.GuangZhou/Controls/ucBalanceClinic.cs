using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.WinForms.Controls;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using FS.HISFC.Models.Registration;

namespace IBorn.SI.GuangZhou.Controls
{
    public partial class ucBalanceClinic : System.Windows.Forms.UserControl
    {
        private NeuTextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private NeuTextBox txtID;
        private System.Windows.Forms.Label label3;
        private NeuTextBox txtTotCost;
        private System.Windows.Forms.Label label4;
        private NeuTextBox txtPubCost;
        private System.Windows.Forms.Label label5;
        private NeuTextBox txtPayCost;
        private System.Windows.Forms.Label label6;
        private NeuTextBox txtItemPayCost;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private NeuTextBox txtOwnItemCost;
        private System.Windows.Forms.Label label9;
        private NeuTextBox txtOwnPayCost;
        private System.Windows.Forms.Label label10;
        private NeuTextBox txtOwnOwnCost;
        private System.Windows.Forms.Label label11;
        private NeuTextBox txtOverCost;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private NeuTextBox txtBaseCost;
        private NeuTextBox txtHosCost;
        private NeuTextBox txtOwnReason;
        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.Container components = null;

        public ucBalanceClinic()
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
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPubCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPayCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtItemPayCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBaseCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOwnItemCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOwnPayCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOwnOwnCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOverCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtHosCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOwnReason = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(43, 6);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(69, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "姓名:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(118, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "身份证:";
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtID.IsEnter2Tab = false;
            this.txtID.Location = new System.Drawing.Point(175, 6);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(156, 21);
            this.txtID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(74, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "总金额:";
            // 
            // txtTotCost
            // 
            this.txtTotCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotCost.IsEnter2Tab = false;
            this.txtTotCost.Location = new System.Drawing.Point(122, 42);
            this.txtTotCost.Name = "txtTotCost";
            this.txtTotCost.ReadOnly = true;
            this.txtTotCost.Size = new System.Drawing.Size(59, 21);
            this.txtTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtTotCost.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(189, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "社保支付金额:";
            // 
            // txtPubCost
            // 
            this.txtPubCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPubCost.IsEnter2Tab = false;
            this.txtPubCost.Location = new System.Drawing.Point(275, 42);
            this.txtPubCost.Name = "txtPubCost";
            this.txtPubCost.ReadOnly = true;
            this.txtPubCost.Size = new System.Drawing.Size(59, 21);
            this.txtPubCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPubCost.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(338, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "帐户支付金额:";
            // 
            // txtPayCost
            // 
            this.txtPayCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPayCost.IsEnter2Tab = false;
            this.txtPayCost.Location = new System.Drawing.Point(426, 43);
            this.txtPayCost.Name = "txtPayCost";
            this.txtPayCost.ReadOnly = true;
            this.txtPayCost.Size = new System.Drawing.Size(59, 21);
            this.txtPayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPayCost.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(11, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "部分项目自付金额:";
            // 
            // txtItemPayCost
            // 
            this.txtItemPayCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtItemPayCost.IsEnter2Tab = false;
            this.txtItemPayCost.Location = new System.Drawing.Point(122, 68);
            this.txtItemPayCost.Name = "txtItemPayCost";
            this.txtItemPayCost.ReadOnly = true;
            this.txtItemPayCost.Size = new System.Drawing.Size(59, 21);
            this.txtItemPayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtItemPayCost.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(189, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "个人起付金额:";
            // 
            // txtBaseCost
            // 
            this.txtBaseCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBaseCost.IsEnter2Tab = false;
            this.txtBaseCost.Location = new System.Drawing.Point(275, 68);
            this.txtBaseCost.Name = "txtBaseCost";
            this.txtBaseCost.ReadOnly = true;
            this.txtBaseCost.Size = new System.Drawing.Size(59, 21);
            this.txtBaseCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBaseCost.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "个人自费项目金额:";
            // 
            // txtOwnItemCost
            // 
            this.txtOwnItemCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtOwnItemCost.IsEnter2Tab = false;
            this.txtOwnItemCost.Location = new System.Drawing.Point(122, 94);
            this.txtOwnItemCost.Name = "txtOwnItemCost";
            this.txtOwnItemCost.ReadOnly = true;
            this.txtOwnItemCost.Size = new System.Drawing.Size(59, 21);
            this.txtOwnItemCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOwnItemCost.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(189, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "个人自付金额:";
            // 
            // txtOwnPayCost
            // 
            this.txtOwnPayCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtOwnPayCost.IsEnter2Tab = false;
            this.txtOwnPayCost.Location = new System.Drawing.Point(275, 94);
            this.txtOwnPayCost.Name = "txtOwnPayCost";
            this.txtOwnPayCost.ReadOnly = true;
            this.txtOwnPayCost.Size = new System.Drawing.Size(59, 21);
            this.txtOwnPayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOwnPayCost.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(339, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "个人自负金额:";
            // 
            // txtOwnOwnCost
            // 
            this.txtOwnOwnCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtOwnOwnCost.IsEnter2Tab = false;
            this.txtOwnOwnCost.Location = new System.Drawing.Point(426, 93);
            this.txtOwnOwnCost.Name = "txtOwnOwnCost";
            this.txtOwnOwnCost.ReadOnly = true;
            this.txtOwnOwnCost.Size = new System.Drawing.Size(59, 21);
            this.txtOwnOwnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOwnOwnCost.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(13, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(172, 16);
            this.label11.TabIndex = 21;
            this.label11.Text = "超统筹支付限额个人自付金额:";
            // 
            // txtOverCost
            // 
            this.txtOverCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtOverCost.IsEnter2Tab = false;
            this.txtOverCost.Location = new System.Drawing.Point(196, 122);
            this.txtOverCost.Name = "txtOverCost";
            this.txtOverCost.ReadOnly = true;
            this.txtOverCost.Size = new System.Drawing.Size(59, 21);
            this.txtOverCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOverCost.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(339, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 16);
            this.label12.TabIndex = 23;
            this.label12.Text = "机构分担金额:";
            // 
            // txtHosCost
            // 
            this.txtHosCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtHosCost.IsEnter2Tab = false;
            this.txtHosCost.Location = new System.Drawing.Point(426, 68);
            this.txtHosCost.Name = "txtHosCost";
            this.txtHosCost.ReadOnly = true;
            this.txtHosCost.Size = new System.Drawing.Size(59, 21);
            this.txtHosCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHosCost.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(13, 148);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "自费原因:";
            // 
            // txtOwnReason
            // 
            this.txtOwnReason.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtOwnReason.IsEnter2Tab = false;
            this.txtOwnReason.Location = new System.Drawing.Point(75, 146);
            this.txtOwnReason.Name = "txtOwnReason";
            this.txtOwnReason.ReadOnly = true;
            this.txtOwnReason.Size = new System.Drawing.Size(410, 21);
            this.txtOwnReason.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOwnReason.TabIndex = 24;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Location = new System.Drawing.Point(16, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 42);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(7, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(454, 14);
            this.label14.TabIndex = 0;
            this.label14.Text = "个人自负金额 = 个人自费项目金额 + 个人自付金额 + 超统筹支付限额个人自付金额";
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRefresh.Location = new System.Drawing.Point(252, 218);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "刷新(&R)";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOk.Location = new System.Drawing.Point(333, 218);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 28;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(413, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ucBalanceClinic
            // 
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtOwnReason);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtHosCost);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtOverCost);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtOwnOwnCost);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtOwnPayCost);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtOwnItemCost);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBaseCost);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtItemPayCost);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPayCost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPubCost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTotCost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Name = "ucBalanceClinic";
            this.Size = new System.Drawing.Size(501, 249);
            this.Load += new System.EventHandler(this.ucBalanceClinic_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region 变量

        private Register rInfo = new Register();//挂号信息        
        private bool isOK = false;//是否正常结算
        IBorn.SI.GuangZhou.SILocalManager myInterface = new IBorn.SI.GuangZhou.SILocalManager();

        #endregion

        #region 属性

        /// <summary>
        /// 挂号信息
        /// </summary>
        public Register RInfo
        {
            set
            {
                this.rInfo = value;
            }
            get
            {
                return this.rInfo;
            }
        }
        /// <summary>
        /// 是否正常结算
        /// </summary>
        public bool IsOK
        {
            get
            {
                return isOK;
            }
        }


        #endregion

        #region 函数
        /// <summary>
        /// 刷新结算信息.
        /// </summary>
        /// <returns>-1失败 0 成功</returns>
        public int RefreshBalance()
        {

            IBorn.SI.GuangZhou.OutPatient.GetBalanceResult getBalance = new OutPatient.GetBalanceResult();
            if (getBalance.CallService(RInfo, ref rInfo) < 0)
            {
                MessageBox.Show("获得医保结算信息出错!" + getBalance.ErrorMsg);
                return -1;
            }
            this.txtTotCost.Text = rInfo.SIMainInfo.TotCost.ToString();
            this.txtPubCost.Text = rInfo.SIMainInfo.PubCost.ToString();
            this.txtPayCost.Text = rInfo.SIMainInfo.PayCost.ToString();
            this.txtItemPayCost.Text = rInfo.SIMainInfo.ItemYLCost.ToString();
            this.txtBaseCost.Text = rInfo.SIMainInfo.BaseCost.ToString();
            this.txtHosCost.Text = rInfo.SIMainInfo.HosCost.ToString();
            this.txtOwnItemCost.Text = rInfo.SIMainInfo.ItemPayCost.ToString();
            this.txtOwnPayCost.Text = rInfo.SIMainInfo.PubOwnCost.ToString();
            this.txtOwnOwnCost.Text = rInfo.SIMainInfo.OwnCost.ToString();
            this.txtOverCost.Text = rInfo.SIMainInfo.OverTakeOwnCost.ToString();
            this.txtOwnReason.Text = rInfo.SIMainInfo.Memo;
            this.txtName.Text = rInfo.Name;
            this.txtID.Text = rInfo.IDCard;

            ////总金额 加上不上传的特需服务费 与门诊收费主程序 里面的总费用进行判断 
            //rInfo.SIMainInfo.TotCost += FS.FrameWork.Function.NConvert.ToDecimal(rInfo.SIMainInfo.OperInfo.User03);

            this.btnOk.Enabled = true;
            this.btnCancel.Enabled = false;
            return 0;
        }
        public int CloseWindow()
        {
            //DialogResult result = MessageBox.Show("关闭此窗口将取消已经上传得明细!是否确定?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //if (result == DialogResult.Yes)
            //{                
            //    return 0;
            //}
            //else
            //{
            //    isCorrect = false;
            //    return -1;
            //}
            return 1;
        }

        #endregion

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.isOK = false;
            this.FindForm().Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (this.RInfo.SIMainInfo.TotCost <= 0)
            {
                MessageBox.Show("请先到医保客户端结算。\r\n");
                return;
            }
            isOK = true;
            ////把医保结算信息存储到HIS
            //if (myInterface.SaveBlanceSIOutPatient(this.RInfo) < 0)
            //{
            //    MessageBox.Show("医保结算信息存储到HIS发生错误。\r\n" + myInterface.Err);
            //    return;
            //}
            this.FindForm().Close();
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            if (RefreshBalance() == -1)
            {
                return;
            }
            this.btnOk.Enabled = true;
            this.btnOk.Select();
            this.btnOk.Focus();
        }

        private void ucBalanceClinic_Load(object sender, EventArgs e)
        {
            this.btnRefresh.Select();
            this.btnRefresh.Focus();
            if (!this.DesignMode && RInfo != null)
            {
                this.txtName.Text = RInfo.Name;
                this.txtID.Text = RInfo.IDCard;
            }
        }
    }
}
