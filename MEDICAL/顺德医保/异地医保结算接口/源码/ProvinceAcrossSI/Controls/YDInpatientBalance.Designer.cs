namespace ProvinceAcrossSI.Controls
{
    partial class YDInpatientBalance
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
            this.components = new System.ComponentModel.Container();
            this.ucQueryPatientInfo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.button1 = new System.Windows.Forms.Button();
            this.btCancelReg = new System.Windows.Forms.Button();
            this.btBalancePrint = new System.Windows.Forms.Button();
            this.btCancelInBalance = new System.Windows.Forms.Button();
            this.btCancelFeeUpload = new System.Windows.Forms.Button();
            this.btCancelOutReg = new System.Windows.Forms.Button();
            this.btOutReg = new System.Windows.Forms.Button();
            this.btUpLoadFeeDetail = new System.Windows.Forms.Button();
            this.btBalanceInfo = new System.Windows.Forms.Button();
            this.btInReg = new System.Windows.Forms.Button();
            this.lblIdNo = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblMCarNo = new System.Windows.Forms.Label();
            this.lblDiagn = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblIndate = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblOutDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.saveDiagnose = new System.Windows.Forms.Button();
            this.cmbDiag1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDiag2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDiag3 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbJSLX = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbJSLX = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btCorrentTrain = new System.Windows.Forms.Button();
            this.cbTrainsType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txbTrainsID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucQueryPatientInfo
            // 
            this.ucQueryPatientInfo.DefaultInputType = 0;
            this.ucQueryPatientInfo.InputType = 0;
            this.ucQueryPatientInfo.IsDeptOnly = true;
            this.ucQueryPatientInfo.Location = new System.Drawing.Point(24, 30);
            this.ucQueryPatientInfo.Name = "ucQueryPatientInfo";
            this.ucQueryPatientInfo.PatientInState = "ALL";
            this.ucQueryPatientInfo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.AfterArrived;
            this.ucQueryPatientInfo.Size = new System.Drawing.Size(198, 27);
            this.ucQueryPatientInfo.TabIndex = 3;
            this.ucQueryPatientInfo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryPatientInfo_myEvent);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(86, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "一键出院结算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btCancelReg
            // 
            this.btCancelReg.Location = new System.Drawing.Point(340, 34);
            this.btCancelReg.Name = "btCancelReg";
            this.btCancelReg.Size = new System.Drawing.Size(95, 23);
            this.btCancelReg.TabIndex = 5;
            this.btCancelReg.Text = "取消住院登记";
            this.btCancelReg.UseVisualStyleBackColor = true;
            this.btCancelReg.Click += new System.EventHandler(this.btCancelReg_Click);
            // 
            // btBalancePrint
            // 
            this.btBalancePrint.Location = new System.Drawing.Point(389, 238);
            this.btBalancePrint.Name = "btBalancePrint";
            this.btBalancePrint.Size = new System.Drawing.Size(95, 23);
            this.btBalancePrint.TabIndex = 6;
            this.btBalancePrint.Text = "结算单打印";
            this.btBalancePrint.UseVisualStyleBackColor = true;
            this.btBalancePrint.Click += new System.EventHandler(this.btBalancePrint_Click);
            // 
            // btCancelInBalance
            // 
            this.btCancelInBalance.Location = new System.Drawing.Point(288, 272);
            this.btCancelInBalance.Name = "btCancelInBalance";
            this.btCancelInBalance.Size = new System.Drawing.Size(95, 23);
            this.btCancelInBalance.TabIndex = 7;
            this.btCancelInBalance.Text = "取消费用结算";
            this.btCancelInBalance.UseVisualStyleBackColor = true;
            this.btCancelInBalance.Click += new System.EventHandler(this.btCancelInBalance_Click);
            // 
            // btCancelFeeUpload
            // 
            this.btCancelFeeUpload.Location = new System.Drawing.Point(86, 272);
            this.btCancelFeeUpload.Name = "btCancelFeeUpload";
            this.btCancelFeeUpload.Size = new System.Drawing.Size(95, 23);
            this.btCancelFeeUpload.TabIndex = 8;
            this.btCancelFeeUpload.Text = "取消费用上传";
            this.btCancelFeeUpload.UseVisualStyleBackColor = true;
            this.btCancelFeeUpload.Click += new System.EventHandler(this.btCancelFeeUpload_Click);
            // 
            // btCancelOutReg
            // 
            this.btCancelOutReg.Location = new System.Drawing.Point(187, 272);
            this.btCancelOutReg.Name = "btCancelOutReg";
            this.btCancelOutReg.Size = new System.Drawing.Size(95, 23);
            this.btCancelOutReg.TabIndex = 9;
            this.btCancelOutReg.Text = "取消出院登记";
            this.btCancelOutReg.UseVisualStyleBackColor = true;
            this.btCancelOutReg.Click += new System.EventHandler(this.btCancelOutReg_Click);
            // 
            // btOutReg
            // 
            this.btOutReg.Location = new System.Drawing.Point(187, 301);
            this.btOutReg.Name = "btOutReg";
            this.btOutReg.Size = new System.Drawing.Size(95, 23);
            this.btOutReg.TabIndex = 13;
            this.btOutReg.Text = "出院登记";
            this.btOutReg.UseVisualStyleBackColor = true;
            this.btOutReg.Visible = false;
            this.btOutReg.Click += new System.EventHandler(this.btOutReg_Click);
            // 
            // btUpLoadFeeDetail
            // 
            this.btUpLoadFeeDetail.Location = new System.Drawing.Point(86, 301);
            this.btUpLoadFeeDetail.Name = "btUpLoadFeeDetail";
            this.btUpLoadFeeDetail.Size = new System.Drawing.Size(95, 23);
            this.btUpLoadFeeDetail.TabIndex = 12;
            this.btUpLoadFeeDetail.Text = "费用上传";
            this.btUpLoadFeeDetail.UseVisualStyleBackColor = true;
            this.btUpLoadFeeDetail.Visible = false;
            this.btUpLoadFeeDetail.Click += new System.EventHandler(this.btUpLoadFeeDetail_Click);
            // 
            // btBalanceInfo
            // 
            this.btBalanceInfo.Location = new System.Drawing.Point(288, 301);
            this.btBalanceInfo.Name = "btBalanceInfo";
            this.btBalanceInfo.Size = new System.Drawing.Size(95, 23);
            this.btBalanceInfo.TabIndex = 11;
            this.btBalanceInfo.Text = "费用结算";
            this.btBalanceInfo.UseVisualStyleBackColor = true;
            this.btBalanceInfo.Visible = false;
            this.btBalanceInfo.Click += new System.EventHandler(this.btBalanceInfo_Click);
            // 
            // btInReg
            // 
            this.btInReg.Location = new System.Drawing.Point(239, 34);
            this.btInReg.Name = "btInReg";
            this.btInReg.Size = new System.Drawing.Size(95, 23);
            this.btInReg.TabIndex = 10;
            this.btInReg.Text = "住院登记";
            this.btInReg.UseVisualStyleBackColor = true;
            this.btInReg.Click += new System.EventHandler(this.btInReg_Click);
            // 
            // lblIdNo
            // 
            this.lblIdNo.AutoSize = true;
            this.lblIdNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIdNo.Location = new System.Drawing.Point(538, 105);
            this.lblIdNo.Name = "lblIdNo";
            this.lblIdNo.Size = new System.Drawing.Size(53, 12);
            this.lblIdNo.TabIndex = 52;
            this.lblIdNo.Text = "身份证号";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.Location = new System.Drawing.Point(272, 105);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(53, 12);
            this.lblAge.TabIndex = 59;
            this.lblAge.Text = "出生日期";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(155, 105);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(29, 12);
            this.lblSex.TabIndex = 62;
            this.lblSex.Text = "性别";
            // 
            // lblMCarNo
            // 
            this.lblMCarNo.AutoSize = true;
            this.lblMCarNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMCarNo.Location = new System.Drawing.Point(70, 126);
            this.lblMCarNo.Name = "lblMCarNo";
            this.lblMCarNo.Size = new System.Drawing.Size(41, 12);
            this.lblMCarNo.TabIndex = 65;
            this.lblMCarNo.Text = "社保号";
            // 
            // lblDiagn
            // 
            this.lblDiagn.AutoSize = true;
            this.lblDiagn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiagn.Location = new System.Drawing.Point(230, 126);
            this.lblDiagn.Name = "lblDiagn";
            this.lblDiagn.Size = new System.Drawing.Size(29, 12);
            this.lblDiagn.TabIndex = 80;
            this.lblDiagn.Text = "诊断";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(21, 105);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(41, 12);
            this.label35.TabIndex = 78;
            this.label35.Text = "姓名：";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblName.Location = new System.Drawing.Point(63, 105);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(29, 12);
            this.lblName.TabIndex = 77;
            this.lblName.Text = "姓名";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(196, 126);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 12);
            this.label22.TabIndex = 44;
            this.label22.Text = "诊断：";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(120, 105);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(41, 12);
            this.label33.TabIndex = 76;
            this.label33.Text = "性别：";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(394, 105);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(29, 12);
            this.lblDept.TabIndex = 75;
            this.lblDept.Text = "科室";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(480, 105);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(65, 12);
            this.label32.TabIndex = 54;
            this.label32.Text = "身份证号：";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(215, 148);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 12);
            this.label26.TabIndex = 69;
            this.label26.Text = "出院时间：";
            // 
            // lblIndate
            // 
            this.lblIndate.AutoSize = true;
            this.lblIndate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIndate.ForeColor = System.Drawing.Color.Black;
            this.lblIndate.Location = new System.Drawing.Point(84, 148);
            this.lblIndate.Name = "lblIndate";
            this.lblIndate.Size = new System.Drawing.Size(119, 12);
            this.lblIndate.TabIndex = 61;
            this.lblIndate.Text = "2014-08-18 08:08:08";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(201, 105);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 60;
            this.label18.Text = "出生日期：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(352, 105);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 58;
            this.label16.Text = "病区：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(22, 148);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 55;
            this.label13.Text = "入院时间：";
            // 
            // lblOutDate
            // 
            this.lblOutDate.AutoSize = true;
            this.lblOutDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutDate.Location = new System.Drawing.Point(279, 148);
            this.lblOutDate.Name = "lblOutDate";
            this.lblOutDate.Size = new System.Drawing.Size(119, 12);
            this.lblOutDate.TabIndex = 53;
            this.lblOutDate.Text = "2014-08-18 18:08:08";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "医保卡：";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(19, 179);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(71, 12);
            this.label46.TabIndex = 83;
            this.label46.Text = "出院诊断1：";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(19, 203);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(71, 12);
            this.label45.TabIndex = 82;
            this.label45.Text = "出院诊断2：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(352, 203);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(71, 12);
            this.label24.TabIndex = 81;
            this.label24.Text = "出院诊断3：";
            // 
            // saveDiagnose
            // 
            this.saveDiagnose.Location = new System.Drawing.Point(389, 272);
            this.saveDiagnose.Name = "saveDiagnose";
            this.saveDiagnose.Size = new System.Drawing.Size(95, 23);
            this.saveDiagnose.TabIndex = 87;
            this.saveDiagnose.Text = "修改诊断";
            this.saveDiagnose.UseVisualStyleBackColor = true;
            this.saveDiagnose.Click += new System.EventHandler(this.saveDiagnose_Click);
            // 
            // cmbDiag1
            // 
            this.cmbDiag1.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDiag1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDiag1.FormattingEnabled = true;
            this.cmbDiag1.IsEnter2Tab = false;
            this.cmbDiag1.IsFlat = false;
            this.cmbDiag1.IsLike = true;
            this.cmbDiag1.IsListOnly = false;
            this.cmbDiag1.IsPopForm = true;
            this.cmbDiag1.IsShowCustomerList = false;
            this.cmbDiag1.IsShowID = false;
            this.cmbDiag1.IsShowIDAndName = false;
            this.cmbDiag1.Location = new System.Drawing.Point(87, 176);
            this.cmbDiag1.Name = "cmbDiag1";
            this.cmbDiag1.PopForm = null;
            this.cmbDiag1.ShowCustomerList = false;
            this.cmbDiag1.ShowID = false;
            this.cmbDiag1.Size = new System.Drawing.Size(565, 20);
            this.cmbDiag1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDiag1.TabIndex = 88;
            this.cmbDiag1.Tag = "";
            this.cmbDiag1.ToolBarUse = false;
            // 
            // cmbDiag2
            // 
            this.cmbDiag2.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDiag2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDiag2.FormattingEnabled = true;
            this.cmbDiag2.IsEnter2Tab = false;
            this.cmbDiag2.IsFlat = false;
            this.cmbDiag2.IsLike = true;
            this.cmbDiag2.IsListOnly = false;
            this.cmbDiag2.IsPopForm = true;
            this.cmbDiag2.IsShowCustomerList = false;
            this.cmbDiag2.IsShowID = false;
            this.cmbDiag2.IsShowIDAndName = false;
            this.cmbDiag2.Location = new System.Drawing.Point(87, 200);
            this.cmbDiag2.Name = "cmbDiag2";
            this.cmbDiag2.PopForm = null;
            this.cmbDiag2.ShowCustomerList = false;
            this.cmbDiag2.ShowID = false;
            this.cmbDiag2.Size = new System.Drawing.Size(247, 20);
            this.cmbDiag2.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDiag2.TabIndex = 89;
            this.cmbDiag2.Tag = "";
            this.cmbDiag2.ToolBarUse = false;
            // 
            // cmbDiag3
            // 
            this.cmbDiag3.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDiag3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDiag3.FormattingEnabled = true;
            this.cmbDiag3.IsEnter2Tab = false;
            this.cmbDiag3.IsFlat = false;
            this.cmbDiag3.IsLike = true;
            this.cmbDiag3.IsListOnly = false;
            this.cmbDiag3.IsPopForm = true;
            this.cmbDiag3.IsShowCustomerList = false;
            this.cmbDiag3.IsShowID = false;
            this.cmbDiag3.IsShowIDAndName = false;
            this.cmbDiag3.Location = new System.Drawing.Point(416, 200);
            this.cmbDiag3.Name = "cmbDiag3";
            this.cmbDiag3.PopForm = null;
            this.cmbDiag3.ShowCustomerList = false;
            this.cmbDiag3.ShowID = false;
            this.cmbDiag3.Size = new System.Drawing.Size(236, 20);
            this.cmbDiag3.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDiag3.TabIndex = 90;
            this.cmbDiag3.Tag = "";
            this.cmbDiag3.ToolBarUse = false;
            // 
            // cmbJSLX
            // 
            this.cmbJSLX.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbJSLX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbJSLX.FormattingEnabled = true;
            this.cmbJSLX.IsEnter2Tab = false;
            this.cmbJSLX.IsFlat = false;
            this.cmbJSLX.IsLike = true;
            this.cmbJSLX.IsListOnly = false;
            this.cmbJSLX.IsPopForm = true;
            this.cmbJSLX.IsShowCustomerList = false;
            this.cmbJSLX.IsShowID = false;
            this.cmbJSLX.IsShowIDAndName = false;
            this.cmbJSLX.Location = new System.Drawing.Point(86, 70);
            this.cmbJSLX.Name = "cmbJSLX";
            this.cmbJSLX.PopForm = null;
            this.cmbJSLX.ShowCustomerList = false;
            this.cmbJSLX.ShowID = false;
            this.cmbJSLX.Size = new System.Drawing.Size(349, 20);
            this.cmbJSLX.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbJSLX.TabIndex = 92;
            this.cmbJSLX.Tag = "";
            this.cmbJSLX.ToolBarUse = false;
            // 
            // lbJSLX
            // 
            this.lbJSLX.AutoSize = true;
            this.lbJSLX.Location = new System.Drawing.Point(18, 73);
            this.lbJSLX.Name = "lbJSLX";
            this.lbJSLX.Size = new System.Drawing.Size(65, 12);
            this.lbJSLX.TabIndex = 91;
            this.lbJSLX.Text = "结算类型：";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btCorrentTrain);
            this.panel1.Controls.Add(this.cbTrainsType);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txbTrainsID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 341);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 98);
            this.panel1.TabIndex = 94;
            // 
            // btCorrentTrain
            // 
            this.btCorrentTrain.Location = new System.Drawing.Point(498, 58);
            this.btCorrentTrain.Name = "btCorrentTrain";
            this.btCorrentTrain.Size = new System.Drawing.Size(95, 23);
            this.btCorrentTrain.TabIndex = 91;
            this.btCorrentTrain.Text = "冲正交易确认";
            this.btCorrentTrain.UseVisualStyleBackColor = true;
            this.btCorrentTrain.Click += new System.EventHandler(this.btCorrentTrain_Click);
            // 
            // cbTrainsType
            // 
            this.cbTrainsType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cbTrainsType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbTrainsType.FormattingEnabled = true;
            this.cbTrainsType.IsEnter2Tab = false;
            this.cbTrainsType.IsFlat = false;
            this.cbTrainsType.IsLike = true;
            this.cbTrainsType.IsListOnly = false;
            this.cbTrainsType.IsPopForm = true;
            this.cbTrainsType.IsShowCustomerList = false;
            this.cbTrainsType.IsShowID = false;
            this.cbTrainsType.IsShowIDAndName = false;
            this.cbTrainsType.Location = new System.Drawing.Point(79, 31);
            this.cbTrainsType.Name = "cbTrainsType";
            this.cbTrainsType.PopForm = null;
            this.cbTrainsType.ShowCustomerList = false;
            this.cbTrainsType.ShowID = false;
            this.cbTrainsType.Size = new System.Drawing.Size(178, 20);
            this.cbTrainsType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbTrainsType.TabIndex = 90;
            this.cbTrainsType.Tag = "";
            this.cbTrainsType.ToolBarUse = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "交易类型：";
            // 
            // txbTrainsID
            // 
            this.txbTrainsID.Location = new System.Drawing.Point(364, 31);
            this.txbTrainsID.Name = "txbTrainsID";
            this.txbTrainsID.Size = new System.Drawing.Size(229, 21);
            this.txbTrainsID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "交易流水号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "冲正交易（慎用）：";
            // 
            // YDInpatientBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbJSLX);
            this.Controls.Add(this.lbJSLX);
            this.Controls.Add(this.cmbDiag3);
            this.Controls.Add(this.cmbDiag2);
            this.Controls.Add(this.cmbDiag1);
            this.Controls.Add(this.saveDiagnose);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.lblIdNo);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblMCarNo);
            this.Controls.Add(this.lblDiagn);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.lblDept);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.lblIndate);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblOutDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btOutReg);
            this.Controls.Add(this.btUpLoadFeeDetail);
            this.Controls.Add(this.btBalanceInfo);
            this.Controls.Add(this.btInReg);
            this.Controls.Add(this.btCancelOutReg);
            this.Controls.Add(this.btCancelFeeUpload);
            this.Controls.Add(this.btCancelInBalance);
            this.Controls.Add(this.btBalancePrint);
            this.Controls.Add(this.btCancelReg);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucQueryPatientInfo);
            this.Name = "YDInpatientBalance";
            this.Size = new System.Drawing.Size(681, 518);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryPatientInfo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btCancelReg;
        private System.Windows.Forms.Button btBalancePrint;
        private System.Windows.Forms.Button btCancelInBalance;
        private System.Windows.Forms.Button btCancelFeeUpload;
        private System.Windows.Forms.Button btCancelOutReg;
        private System.Windows.Forms.Button btOutReg;
        private System.Windows.Forms.Button btUpLoadFeeDetail;
        private System.Windows.Forms.Button btBalanceInfo;
        private System.Windows.Forms.Button btInReg;
        private System.Windows.Forms.Label lblIdNo;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblMCarNo;
        private System.Windows.Forms.Label lblDiagn;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblIndate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblOutDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button saveDiagnose;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiag1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiag2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiag3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbJSLX;
        private System.Windows.Forms.Label lbJSLX;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btCorrentTrain;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbTrainsType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbTrainsID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
