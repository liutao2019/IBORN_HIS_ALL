namespace FS.HISFC.Components.OutpatientFee.Controls
{
    partial class UCCardTicket
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            FS.FrameWork.WinForms.Controls.NeuLabel lbCardType;
            this.pnlTop = new System.Windows.Forms.Panel();
            this.txtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbSex = new System.Windows.Forms.TextBox();
            this.tbCardType = new System.Windows.Forms.TextBox();
            this.tbMedicalNO = new System.Windows.Forms.TextBox();
            this.lblMedicalNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbIDNO = new System.Windows.Forms.TextBox();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel = new System.Windows.Forms.Panel();
            this.lbcard = new System.Windows.Forms.Label();
            this.lbphone = new System.Windows.Forms.Label();
            this.lbGiftName = new System.Windows.Forms.Label();
            this.lbTicketDisccount = new System.Windows.Forms.Label();
            this.lbIsIncludeRegFee = new System.Windows.Forms.Label();
            this.lbTicketValue = new System.Windows.Forms.Label();
            this.lbLowestCost = new System.Windows.Forms.Label();
            this.lbState = new System.Windows.Forms.Label();
            this.lbGetPerson = new System.Windows.Forms.Label();
            this.lbEndTime = new System.Windows.Forms.Label();
            this.lbTicketChannel = new System.Windows.Forms.Label();
            this.lbCreateTime = new System.Windows.Forms.Label();
            this.lbUseArea = new System.Windows.Forms.Label();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.lbTicketContent = new System.Windows.Forms.Label();
            this.lbShareWay = new System.Windows.Forms.Label();
            this.lbTicketType = new System.Windows.Forms.Label();
            this.lbTicketName = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnWriteOff = new System.Windows.Forms.Button();
            this.txtTicketNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            lbCardType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlTop.SuspendLayout();
            this.panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCardType
            // 
            lbCardType.AutoSize = true;
            lbCardType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbCardType.ForeColor = System.Drawing.Color.Black;
            lbCardType.Location = new System.Drawing.Point(29, 48);
            lbCardType.Name = "lbCardType";
            lbCardType.Size = new System.Drawing.Size(59, 12);
            lbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbCardType.TabIndex = 28;
            lbCardType.Text = "证件类型:";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.pnlTop.Controls.Add(this.txtCardNO);
            this.pnlTop.Controls.Add(this.lbCardNO);
            this.pnlTop.Controls.Add(this.tbSex);
            this.pnlTop.Controls.Add(this.tbCardType);
            this.pnlTop.Controls.Add(this.tbMedicalNO);
            this.pnlTop.Controls.Add(this.lblMedicalNO);
            this.pnlTop.Controls.Add(this.tbPhone);
            this.pnlTop.Controls.Add(this.lblPhone);
            this.pnlTop.Controls.Add(this.tbIDNO);
            this.pnlTop.Controls.Add(this.tbAge);
            this.pnlTop.Controls.Add(this.tbName);
            this.pnlTop.Controls.Add(this.lbName);
            this.pnlTop.Controls.Add(this.lbSex);
            this.pnlTop.Controls.Add(this.lbAge);
            this.pnlTop.Controls.Add(this.lbRegDept);
            this.pnlTop.Controls.Add(lbCardType);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(3, 17);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(904, 70);
            this.pnlTop.TabIndex = 6;
            // 
            // txtCardNO
            // 
            this.txtCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNO.IsEnter2Tab = false;
            this.txtCardNO.Location = new System.Drawing.Point(110, 9);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(133, 23);
            this.txtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO.TabIndex = 48;
            this.txtCardNO.TabStop = false;
            this.txtCardNO.Tag = "CARDNO";
            this.txtCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNO_KeyDown);
            // 
            // lbCardNO
            // 
            this.lbCardNO.AutoSize = true;
            this.lbCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCardNO.ForeColor = System.Drawing.Color.Blue;
            this.lbCardNO.Location = new System.Drawing.Point(1, 13);
            this.lbCardNO.Name = "lbCardNO";
            this.lbCardNO.Size = new System.Drawing.Size(105, 14);
            this.lbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNO.TabIndex = 47;
            this.lbCardNO.Text = "个人信息检索:";
            // 
            // tbSex
            // 
            this.tbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbSex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSex.Location = new System.Drawing.Point(786, 11);
            this.tbSex.Name = "tbSex";
            this.tbSex.ReadOnly = true;
            this.tbSex.Size = new System.Drawing.Size(49, 14);
            this.tbSex.TabIndex = 44;
            this.tbSex.TabStop = false;
            // 
            // tbCardType
            // 
            this.tbCardType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbCardType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCardType.Location = new System.Drawing.Point(110, 49);
            this.tbCardType.Name = "tbCardType";
            this.tbCardType.ReadOnly = true;
            this.tbCardType.Size = new System.Drawing.Size(86, 14);
            this.tbCardType.TabIndex = 43;
            this.tbCardType.TabStop = false;
            // 
            // tbMedicalNO
            // 
            this.tbMedicalNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbMedicalNO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMedicalNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMedicalNO.Location = new System.Drawing.Point(305, 12);
            this.tbMedicalNO.Name = "tbMedicalNO";
            this.tbMedicalNO.ReadOnly = true;
            this.tbMedicalNO.Size = new System.Drawing.Size(95, 14);
            this.tbMedicalNO.TabIndex = 25;
            this.tbMedicalNO.TabStop = false;
            this.tbMedicalNO.Tag = "MEDNO";
            // 
            // lblMedicalNO
            // 
            this.lblMedicalNO.AutoSize = true;
            this.lblMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMedicalNO.ForeColor = System.Drawing.Color.Blue;
            this.lblMedicalNO.Location = new System.Drawing.Point(249, 13);
            this.lblMedicalNO.Name = "lblMedicalNO";
            this.lblMedicalNO.Size = new System.Drawing.Size(45, 14);
            this.lblMedicalNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblMedicalNO.TabIndex = 24;
            this.lblMedicalNO.Text = "卡号:";
            // 
            // tbPhone
            // 
            this.tbPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPhone.Location = new System.Drawing.Point(559, 43);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.ReadOnly = true;
            this.tbPhone.Size = new System.Drawing.Size(106, 14);
            this.tbPhone.TabIndex = 39;
            this.tbPhone.TabStop = false;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhone.ForeColor = System.Drawing.Color.Black;
            this.lblPhone.Location = new System.Drawing.Point(467, 44);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(59, 12);
            this.lblPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPhone.TabIndex = 38;
            this.lblPhone.Text = "联系电话:";
            // 
            // tbIDNO
            // 
            this.tbIDNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbIDNO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbIDNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbIDNO.Location = new System.Drawing.Point(305, 46);
            this.tbIDNO.Name = "tbIDNO";
            this.tbIDNO.ReadOnly = true;
            this.tbIDNO.Size = new System.Drawing.Size(151, 14);
            this.tbIDNO.TabIndex = 31;
            this.tbIDNO.TabStop = false;
            // 
            // tbAge
            // 
            this.tbAge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbAge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAge.Location = new System.Drawing.Point(786, 44);
            this.tbAge.Name = "tbAge";
            this.tbAge.ReadOnly = true;
            this.tbAge.Size = new System.Drawing.Size(49, 14);
            this.tbAge.TabIndex = 35;
            this.tbAge.TabStop = false;
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.Location = new System.Drawing.Point(559, 13);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(156, 14);
            this.tbName.TabIndex = 27;
            this.tbName.TabStop = false;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.Color.Black;
            this.lbName.Location = new System.Drawing.Point(472, 14);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(53, 12);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 26;
            this.lbName.Text = "姓   名:";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.ForeColor = System.Drawing.Color.Black;
            this.lbSex.Location = new System.Drawing.Point(734, 12);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(35, 12);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 32;
            this.lbSex.Text = "性别:";
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.ForeColor = System.Drawing.Color.Black;
            this.lbAge.Location = new System.Drawing.Point(734, 45);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(35, 12);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 34;
            this.lbAge.Text = "年龄:";
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegDept.ForeColor = System.Drawing.Color.Black;
            this.lbRegDept.Location = new System.Drawing.Point(219, 47);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(59, 12);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 30;
            this.lbRegDept.Text = "证件号码:";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.lbcard);
            this.panel.Controls.Add(this.lbphone);
            this.panel.Controls.Add(this.lbGiftName);
            this.panel.Controls.Add(this.lbTicketDisccount);
            this.panel.Controls.Add(this.lbIsIncludeRegFee);
            this.panel.Controls.Add(this.lbTicketValue);
            this.panel.Controls.Add(this.lbLowestCost);
            this.panel.Controls.Add(this.lbState);
            this.panel.Controls.Add(this.lbGetPerson);
            this.panel.Controls.Add(this.lbEndTime);
            this.panel.Controls.Add(this.lbTicketChannel);
            this.panel.Controls.Add(this.lbCreateTime);
            this.panel.Controls.Add(this.lbUseArea);
            this.panel.Controls.Add(this.lbStartTime);
            this.panel.Controls.Add(this.lbTicketContent);
            this.panel.Controls.Add(this.lbShareWay);
            this.panel.Controls.Add(this.lbTicketType);
            this.panel.Controls.Add(this.lbTicketName);
            this.panel.Location = new System.Drawing.Point(3, 49);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(904, 475);
            this.panel.TabIndex = 7;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // lbcard
            // 
            this.lbcard.AutoSize = true;
            this.lbcard.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbcard.Location = new System.Drawing.Point(29, 91);
            this.lbcard.Name = "lbcard";
            this.lbcard.Size = new System.Drawing.Size(49, 14);
            this.lbcard.TabIndex = 70;
            this.lbcard.Text = "卡券号";
            // 
            // lbphone
            // 
            this.lbphone.AutoSize = true;
            this.lbphone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbphone.Location = new System.Drawing.Point(393, 91);
            this.lbphone.Name = "lbphone";
            this.lbphone.Size = new System.Drawing.Size(105, 14);
            this.lbphone.TabIndex = 69;
            this.lbphone.Text = "领取人电话号码";
            // 
            // lbGiftName
            // 
            this.lbGiftName.AutoSize = true;
            this.lbGiftName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbGiftName.Location = new System.Drawing.Point(393, 175);
            this.lbGiftName.Name = "lbGiftName";
            this.lbGiftName.Size = new System.Drawing.Size(63, 14);
            this.lbGiftName.TabIndex = 67;
            this.lbGiftName.Text = "礼品名称";
            // 
            // lbTicketDisccount
            // 
            this.lbTicketDisccount.AutoSize = true;
            this.lbTicketDisccount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTicketDisccount.ForeColor = System.Drawing.Color.Red;
            this.lbTicketDisccount.Location = new System.Drawing.Point(29, 175);
            this.lbTicketDisccount.Name = "lbTicketDisccount";
            this.lbTicketDisccount.Size = new System.Drawing.Size(67, 14);
            this.lbTicketDisccount.TabIndex = 66;
            this.lbTicketDisccount.Text = "卡券折扣";
            // 
            // lbIsIncludeRegFee
            // 
            this.lbIsIncludeRegFee.AutoSize = true;
            this.lbIsIncludeRegFee.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbIsIncludeRegFee.Location = new System.Drawing.Point(29, 202);
            this.lbIsIncludeRegFee.Name = "lbIsIncludeRegFee";
            this.lbIsIncludeRegFee.Size = new System.Drawing.Size(105, 14);
            this.lbIsIncludeRegFee.TabIndex = 65;
            this.lbIsIncludeRegFee.Text = "是否包含挂号费";
            // 
            // lbTicketValue
            // 
            this.lbTicketValue.AutoSize = true;
            this.lbTicketValue.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTicketValue.ForeColor = System.Drawing.Color.Red;
            this.lbTicketValue.Location = new System.Drawing.Point(393, 147);
            this.lbTicketValue.Name = "lbTicketValue";
            this.lbTicketValue.Size = new System.Drawing.Size(67, 14);
            this.lbTicketValue.TabIndex = 64;
            this.lbTicketValue.Text = "卡券金额";
            // 
            // lbLowestCost
            // 
            this.lbLowestCost.AutoSize = true;
            this.lbLowestCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLowestCost.ForeColor = System.Drawing.Color.Red;
            this.lbLowestCost.Location = new System.Drawing.Point(29, 147);
            this.lbLowestCost.Name = "lbLowestCost";
            this.lbLowestCost.Size = new System.Drawing.Size(67, 14);
            this.lbLowestCost.TabIndex = 63;
            this.lbLowestCost.Text = "最低消费";
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbState.ForeColor = System.Drawing.Color.Red;
            this.lbState.Location = new System.Drawing.Point(393, 230);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(67, 14);
            this.lbState.TabIndex = 62;
            this.lbState.Text = "使用状态";
            // 
            // lbGetPerson
            // 
            this.lbGetPerson.AutoSize = true;
            this.lbGetPerson.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbGetPerson.Location = new System.Drawing.Point(393, 66);
            this.lbGetPerson.Name = "lbGetPerson";
            this.lbGetPerson.Size = new System.Drawing.Size(49, 14);
            this.lbGetPerson.TabIndex = 61;
            this.lbGetPerson.Text = "领取人";
            // 
            // lbEndTime
            // 
            this.lbEndTime.AutoSize = true;
            this.lbEndTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbEndTime.Location = new System.Drawing.Point(393, 119);
            this.lbEndTime.Name = "lbEndTime";
            this.lbEndTime.Size = new System.Drawing.Size(63, 14);
            this.lbEndTime.TabIndex = 60;
            this.lbEndTime.Text = "结束时间";
            // 
            // lbTicketChannel
            // 
            this.lbTicketChannel.AutoSize = true;
            this.lbTicketChannel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTicketChannel.Location = new System.Drawing.Point(29, 66);
            this.lbTicketChannel.Name = "lbTicketChannel";
            this.lbTicketChannel.Size = new System.Drawing.Size(63, 14);
            this.lbTicketChannel.TabIndex = 59;
            this.lbTicketChannel.Text = "卡券渠道";
            // 
            // lbCreateTime
            // 
            this.lbCreateTime.AutoSize = true;
            this.lbCreateTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCreateTime.Location = new System.Drawing.Point(29, 261);
            this.lbCreateTime.Name = "lbCreateTime";
            this.lbCreateTime.Size = new System.Drawing.Size(67, 14);
            this.lbCreateTime.TabIndex = 58;
            this.lbCreateTime.Text = "创建时间";
            // 
            // lbUseArea
            // 
            this.lbUseArea.AutoSize = true;
            this.lbUseArea.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUseArea.Location = new System.Drawing.Point(29, 232);
            this.lbUseArea.Name = "lbUseArea";
            this.lbUseArea.Size = new System.Drawing.Size(67, 14);
            this.lbUseArea.TabIndex = 57;
            this.lbUseArea.Text = "使用院区";
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoSize = true;
            this.lbStartTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStartTime.Location = new System.Drawing.Point(29, 119);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(63, 14);
            this.lbStartTime.TabIndex = 56;
            this.lbStartTime.Text = "开始时间";
            // 
            // lbTicketContent
            // 
            this.lbTicketContent.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTicketContent.ForeColor = System.Drawing.Color.Blue;
            this.lbTicketContent.Location = new System.Drawing.Point(28, 287);
            this.lbTicketContent.Name = "lbTicketContent";
            this.lbTicketContent.Size = new System.Drawing.Size(842, 132);
            this.lbTicketContent.TabIndex = 55;
            this.lbTicketContent.Text = "卡券详情";
            // 
            // lbShareWay
            // 
            this.lbShareWay.AutoSize = true;
            this.lbShareWay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbShareWay.Location = new System.Drawing.Point(393, 39);
            this.lbShareWay.Name = "lbShareWay";
            this.lbShareWay.Size = new System.Drawing.Size(63, 14);
            this.lbShareWay.TabIndex = 53;
            this.lbShareWay.Text = "分享形式";
            // 
            // lbTicketType
            // 
            this.lbTicketType.AutoSize = true;
            this.lbTicketType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTicketType.Location = new System.Drawing.Point(29, 39);
            this.lbTicketType.Name = "lbTicketType";
            this.lbTicketType.Size = new System.Drawing.Size(63, 14);
            this.lbTicketType.TabIndex = 52;
            this.lbTicketType.Text = "卡券类型";
            // 
            // lbTicketName
            // 
            this.lbTicketName.AutoSize = true;
            this.lbTicketName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTicketName.ForeColor = System.Drawing.Color.Blue;
            this.lbTicketName.Location = new System.Drawing.Point(29, 9);
            this.lbTicketName.Name = "lbTicketName";
            this.lbTicketName.Size = new System.Drawing.Size(67, 14);
            this.lbTicketName.TabIndex = 51;
            this.lbTicketName.Text = "卡券名称";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(12, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(52, 18);
            this.textBox1.TabIndex = 68;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "卡券号：";
            this.textBox1.WordWrap = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(433, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 50;
            this.button1.Text = "查询卡券";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnWriteOff
            // 
            this.btnWriteOff.Location = new System.Drawing.Point(514, 20);
            this.btnWriteOff.Name = "btnWriteOff";
            this.btnWriteOff.Size = new System.Drawing.Size(75, 23);
            this.btnWriteOff.TabIndex = 49;
            this.btnWriteOff.Text = "核销";
            this.btnWriteOff.UseVisualStyleBackColor = true;
            this.btnWriteOff.Click += new System.EventHandler(this.btnWriteOff_Click);
            // 
            // txtTicketNO
            // 
            this.txtTicketNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTicketNO.IsEnter2Tab = false;
            this.txtTicketNO.Location = new System.Drawing.Point(70, 20);
            this.txtTicketNO.Name = "txtTicketNO";
            this.txtTicketNO.Size = new System.Drawing.Size(357, 23);
            this.txtTicketNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtTicketNO.TabIndex = 48;
            this.txtTicketNO.Tag = "CARDNO";
            this.txtTicketNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTicketNO_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlTop);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(910, 90);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "个人信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.panel);
            this.groupBox2.Controls.Add(this.txtTicketNO);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.btnWriteOff);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(910, 527);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "卡券核销";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(595, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 23);
            this.button2.TabIndex = 69;
            this.button2.Text = "核销旧卡券";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // UCCardTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UCCardTicket";
            this.Size = new System.Drawing.Size(910, 617);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbCardNO;
        private System.Windows.Forms.TextBox tbSex;
        private System.Windows.Forms.TextBox tbCardType;
        protected System.Windows.Forms.TextBox tbMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblMedicalNO;
        protected System.Windows.Forms.TextBox tbPhone;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPhone;
        protected System.Windows.Forms.TextBox tbIDNO;
        protected System.Windows.Forms.TextBox tbAge;
        protected System.Windows.Forms.TextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnWriteOff;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtTicketNO;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        protected System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbcard;
        private System.Windows.Forms.Label lbphone;
        private System.Windows.Forms.Label lbGiftName;
        private System.Windows.Forms.Label lbTicketDisccount;
        private System.Windows.Forms.Label lbIsIncludeRegFee;
        private System.Windows.Forms.Label lbTicketValue;
        private System.Windows.Forms.Label lbLowestCost;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.Label lbGetPerson;
        private System.Windows.Forms.Label lbEndTime;
        private System.Windows.Forms.Label lbTicketChannel;
        private System.Windows.Forms.Label lbCreateTime;
        private System.Windows.Forms.Label lbUseArea;
        private System.Windows.Forms.Label lbStartTime;
        private System.Windows.Forms.Label lbTicketContent;
        private System.Windows.Forms.Label lbShareWay;
        private System.Windows.Forms.Label lbTicketType;
        private System.Windows.Forms.Label lbTicketName;
        private System.Windows.Forms.Button button2;
    }
}
