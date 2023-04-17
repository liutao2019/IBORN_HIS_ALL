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
using FS.HISFC.BizLogic.Fee;

namespace GZSI.Controls
{
    public partial class ucBalanceClinic : System.Windows.Forms.UserControl
    {
        private NeuTextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private NeuTextBox tbID;
        private System.Windows.Forms.Label label3;
        private NeuTextBox tbTotCost;
        private System.Windows.Forms.Label label4;
        private NeuTextBox tbPubCost;
        private System.Windows.Forms.Label label5;
        private NeuTextBox tbPayCost;
        private System.Windows.Forms.Label label6;
        private NeuTextBox tbItemPayCost;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private NeuTextBox tbOwnItemCost;
        private System.Windows.Forms.Label label9;
        private NeuTextBox tbOwnPayCost;
        private System.Windows.Forms.Label label10;
        private NeuTextBox tbOwnOwnCost;
        private System.Windows.Forms.Label label11;
        private NeuTextBox tbOverCost;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private NeuTextBox tbBaseCost;
        private NeuTextBox tbHosCost;
        private NeuTextBox tbOwnReason;
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
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTotCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPubCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPayCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbItemPayCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbBaseCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbOwnItemCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbOwnPayCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbOwnOwnCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbOverCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbHosCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbOwnReason = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(43, 6);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(69, 21);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 0;
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
            // tbID
            // 
            this.tbID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbID.IsEnter2Tab = false;
            this.tbID.Location = new System.Drawing.Point(175, 6);
            this.tbID.Name = "tbID";
            this.tbID.ReadOnly = true;
            this.tbID.Size = new System.Drawing.Size(156, 21);
            this.tbID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(74, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "总金额:";
            // 
            // tbTotCost
            // 
            this.tbTotCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbTotCost.IsEnter2Tab = false;
            this.tbTotCost.Location = new System.Drawing.Point(122, 42);
            this.tbTotCost.Name = "tbTotCost";
            this.tbTotCost.ReadOnly = true;
            this.tbTotCost.Size = new System.Drawing.Size(59, 21);
            this.tbTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbTotCost.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(189, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "社保支付金额:";
            // 
            // tbPubCost
            // 
            this.tbPubCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbPubCost.IsEnter2Tab = false;
            this.tbPubCost.Location = new System.Drawing.Point(275, 42);
            this.tbPubCost.Name = "tbPubCost";
            this.tbPubCost.ReadOnly = true;
            this.tbPubCost.Size = new System.Drawing.Size(59, 21);
            this.tbPubCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPubCost.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(338, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "帐户支付金额:";
            // 
            // tbPayCost
            // 
            this.tbPayCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbPayCost.IsEnter2Tab = false;
            this.tbPayCost.Location = new System.Drawing.Point(426, 43);
            this.tbPayCost.Name = "tbPayCost";
            this.tbPayCost.ReadOnly = true;
            this.tbPayCost.Size = new System.Drawing.Size(59, 21);
            this.tbPayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPayCost.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(11, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "部分项目自付金额:";
            // 
            // tbItemPayCost
            // 
            this.tbItemPayCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbItemPayCost.IsEnter2Tab = false;
            this.tbItemPayCost.Location = new System.Drawing.Point(122, 68);
            this.tbItemPayCost.Name = "tbItemPayCost";
            this.tbItemPayCost.ReadOnly = true;
            this.tbItemPayCost.Size = new System.Drawing.Size(59, 21);
            this.tbItemPayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbItemPayCost.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(189, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "个人起付金额:";
            // 
            // tbBaseCost
            // 
            this.tbBaseCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbBaseCost.IsEnter2Tab = false;
            this.tbBaseCost.Location = new System.Drawing.Point(275, 68);
            this.tbBaseCost.Name = "tbBaseCost";
            this.tbBaseCost.ReadOnly = true;
            this.tbBaseCost.Size = new System.Drawing.Size(59, 21);
            this.tbBaseCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbBaseCost.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "个人自费项目金额:";
            // 
            // tbOwnItemCost
            // 
            this.tbOwnItemCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbOwnItemCost.IsEnter2Tab = false;
            this.tbOwnItemCost.Location = new System.Drawing.Point(122, 94);
            this.tbOwnItemCost.Name = "tbOwnItemCost";
            this.tbOwnItemCost.ReadOnly = true;
            this.tbOwnItemCost.Size = new System.Drawing.Size(59, 21);
            this.tbOwnItemCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOwnItemCost.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(189, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "个人自付金额:";
            // 
            // tbOwnPayCost
            // 
            this.tbOwnPayCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbOwnPayCost.IsEnter2Tab = false;
            this.tbOwnPayCost.Location = new System.Drawing.Point(275, 94);
            this.tbOwnPayCost.Name = "tbOwnPayCost";
            this.tbOwnPayCost.ReadOnly = true;
            this.tbOwnPayCost.Size = new System.Drawing.Size(59, 21);
            this.tbOwnPayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOwnPayCost.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(339, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "个人自负金额:";
            // 
            // tbOwnOwnCost
            // 
            this.tbOwnOwnCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbOwnOwnCost.IsEnter2Tab = false;
            this.tbOwnOwnCost.Location = new System.Drawing.Point(426, 93);
            this.tbOwnOwnCost.Name = "tbOwnOwnCost";
            this.tbOwnOwnCost.ReadOnly = true;
            this.tbOwnOwnCost.Size = new System.Drawing.Size(59, 21);
            this.tbOwnOwnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOwnOwnCost.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(13, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(172, 16);
            this.label11.TabIndex = 21;
            this.label11.Text = "超统筹支付限额个人自付金额:";
            // 
            // tbOverCost
            // 
            this.tbOverCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbOverCost.IsEnter2Tab = false;
            this.tbOverCost.Location = new System.Drawing.Point(196, 122);
            this.tbOverCost.Name = "tbOverCost";
            this.tbOverCost.ReadOnly = true;
            this.tbOverCost.Size = new System.Drawing.Size(59, 21);
            this.tbOverCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOverCost.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(339, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 16);
            this.label12.TabIndex = 23;
            this.label12.Text = "机构分担金额:";
            // 
            // tbHosCost
            // 
            this.tbHosCost.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbHosCost.IsEnter2Tab = false;
            this.tbHosCost.Location = new System.Drawing.Point(426, 68);
            this.tbHosCost.Name = "tbHosCost";
            this.tbHosCost.ReadOnly = true;
            this.tbHosCost.Size = new System.Drawing.Size(59, 21);
            this.tbHosCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbHosCost.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(13, 148);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "自费原因:";
            // 
            // tbOwnReason
            // 
            this.tbOwnReason.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbOwnReason.IsEnter2Tab = false;
            this.tbOwnReason.Location = new System.Drawing.Point(75, 146);
            this.tbOwnReason.Name = "tbOwnReason";
            this.tbOwnReason.ReadOnly = true;
            this.tbOwnReason.Size = new System.Drawing.Size(410, 21);
            this.tbOwnReason.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOwnReason.TabIndex = 24;
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
            this.Controls.Add(this.tbOwnReason);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbHosCost);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbOverCost);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbOwnOwnCost);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbOwnPayCost);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbOwnItemCost);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbBaseCost);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbItemPayCost);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPayCost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPubCost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTotCost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbName);
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
        Management.SIConnect conn = null;//连接信息.
        private bool isCorrect = false;//是否正常结算

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
        public bool IsCorrect
        {
            get
            {
                return isCorrect;
            }
        }
        /// <summary>
        /// 数据库连接信息
        /// </summary>
        public Management.SIConnect Conn
        {
            set
            {
                this.conn = value;
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
            int iReturn = -1;
            bool isSpecailPact = false;//是否特殊合同单位
            Management.SILocalManager myInterface = new Management.SILocalManager();

            isSpecailPact = myInterface.IsPactDealByInpatient(this.rInfo.Pact.ID);
            bool isInPatientDealPact = myInterface.IsPactDealByInpatient(this.rInfo.Pact.ID, "INPATIENT"); 
            if (!isSpecailPact && !isInPatientDealPact)
            {
                iReturn = conn.GetBalanceInfo(this.rInfo);
            }
            else
            {
                iReturn = conn.GetBalanceInfoInpatient(this.rInfo);
            }
            //int iReturn = conn.GetBalanceInfo(this.rInfo);
            if (iReturn == -1)
            {
                MessageBox.Show("获得医保结算信息出错!" + conn.Err);
                return -1;
            }
            if (iReturn == 0)
            {
                MessageBox.Show("患者还没有在医保端结算,请稍候刷新!");
                return -1;
            }
            this.tbTotCost.Text = rInfo.SIMainInfo.TotCost.ToString();
            this.tbPubCost.Text = rInfo.SIMainInfo.PubCost.ToString();
            this.tbPayCost.Text = rInfo.SIMainInfo.PayCost.ToString();
            this.tbItemPayCost.Text = rInfo.SIMainInfo.ItemYLCost.ToString();
            this.tbBaseCost.Text = rInfo.SIMainInfo.BaseCost.ToString();
            this.tbHosCost.Text = rInfo.SIMainInfo.HosCost.ToString();
            this.tbOwnItemCost.Text = rInfo.SIMainInfo.ItemPayCost.ToString();
            this.tbOwnPayCost.Text = rInfo.SIMainInfo.PubOwnCost.ToString();
            this.tbOwnOwnCost.Text = rInfo.SIMainInfo.OwnCost.ToString();
            this.tbOverCost.Text = rInfo.SIMainInfo.OverTakeOwnCost.ToString();
            this.tbOwnReason.Text = rInfo.SIMainInfo.Memo;
            this.tbName.Text = rInfo.Name;
            this.tbID.Text = rInfo.IDCard;

            //总金额 加上不上传的特需服务费 与门诊收费主程序 里面的总费用进行判断 
            rInfo.SIMainInfo.TotCost += FS.FrameWork.Function.NConvert.ToDecimal(rInfo.SIMainInfo.OperInfo.User03);

            this.btnOk.Enabled = true;
            this.btnCancel.Enabled = false;
            return 0;
        }
        public int CloseWindow()
        {
            DialogResult result = MessageBox.Show("关闭此窗口将取消已经上传得明细!是否确定?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.BeginTranscation();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
                int iReturn = -1;
                bool isSpecailPact = false;//是否特殊合同单位
                Management.SILocalManager myInterface = new Management.SILocalManager();

                isSpecailPact = myInterface.IsPactDealByInpatient(this.rInfo.Pact.ID);
                bool isInPatientDealPact = myInterface.IsPactDealByInpatient(this.rInfo.Pact.ID, "INPATIENT"); 
                if (!isSpecailPact && !isInPatientDealPact)
                {
                    iReturn = conn.DeleteItemListClinic(rInfo.SIMainInfo.RegNo);

                    if (conn.DeleteItemListIndicationsClinic(rInfo.SIMainInfo.RegNo) == -1)
                    {
                        conn.RollBack();
                        MessageBox.Show("删除明细失败!" + conn.Err);
                        return -1;
                    }
                }
                else
                {
                    iReturn = conn.DeleteItemList(rInfo.SIMainInfo.RegNo);
                }
                if (iReturn <= 0)
                {
                    conn.RollBack();
                    MessageBox.Show("删除明细失败!" + conn.Err);
                    return -1;
                }
                conn.Commit();
                return 0;
            }
            else
            {
                isCorrect = false;
                return -1;
            }
        }

        #endregion

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            if (this.CloseWindow() == -1)
            {
                // return;
            }
            this.FindForm().Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            isCorrect = true;
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
        }
    }
}
