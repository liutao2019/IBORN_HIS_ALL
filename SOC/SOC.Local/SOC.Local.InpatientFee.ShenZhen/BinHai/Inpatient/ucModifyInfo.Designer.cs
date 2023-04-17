using FS.SOC.HISFC.RADT.Components.Common;
namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Inpatient
{
    /// <summary>
    /// 入院登记界面
    /// </summary>
    public partial class ucModifyInfo
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
            this.lblBirthArea = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBedNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblComputerNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.plInfomation = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuContextMenuStrip1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.txtHomeAddressZip = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.neuInputLabel4 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtWorkZip = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.neuInputLabel3 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.rbtnTempPatientNO = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.txtHomeZip = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.neuInputLabel2 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtName = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.neuInputLabel1 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtClinicNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.lblName = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.chbencrypt = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbSex = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.dtpBirthDay = new FS.SOC.HISFC.RADT.Components.Common.NeuInputDateTime();
            this.txtAge = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtIDNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.lblDoctDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblIDNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBirthday = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblSex = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.cmbBirthArea = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbCountry = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbMarry = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbProfession = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.txtBirthArea = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.cmbNation = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.txtWorkAddress = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtWorkPhone = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtHomePhone = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtAddressNow = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtHomeAddress = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtLinkMan = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.cmbRelation = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.txtLinkPhone = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtLinkAddr = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtDiagnose = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.cmbInSource = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbCircs = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbApproach = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbNurseCell = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbBedNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbDoctor = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbPact = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.txtMCardNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtComputerNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.mtxtBloodFee = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.mTxtIntimes = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.mTxtPrepay = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.cmbPayMode = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.dtpInTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtInpatientNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.neuLabel2 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.neuLabel1 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.cmbOldDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.lblOldDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lbAddressNow = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.neuLabel6 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBedCount = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblInTimes = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBloodFee = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDiagnose = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDoctor = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblCircs = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblApproach = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblInSource = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblLinkPhone = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblLinkAddress = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblRelation = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblLinkMan = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblHomeAddress = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblWorkPhone = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblProfession = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblCountry = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblMarry = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblWorkAddress = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblNation = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblPact = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblInTime = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblMCardNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.rdoInpatientNO = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.lblPrepay = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblPayMode = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.plInfomation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBirthArea
            // 
            this.lblBirthArea.AutoSize = true;
            this.lblBirthArea.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBirthArea.ForeColor = System.Drawing.Color.Blue;
            this.lblBirthArea.InputMsg = "";
            this.lblBirthArea.IsDefaultCHInput = false;
            this.lblBirthArea.IsTagInput = false;
            this.lblBirthArea.IsTextInput = false;
            this.lblBirthArea.Location = new System.Drawing.Point(485, 40);
            this.lblBirthArea.Name = "lblBirthArea";
            this.lblBirthArea.Size = new System.Drawing.Size(35, 12);
            this.lblBirthArea.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblBirthArea.TabIndex = 156;
            this.lblBirthArea.Text = "籍贯:";
            // 
            // lblBedNO
            // 
            this.lblBedNO.AutoSize = true;
            this.lblBedNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedNO.InputMsg = "";
            this.lblBedNO.IsDefaultCHInput = false;
            this.lblBedNO.IsTagInput = false;
            this.lblBedNO.IsTextInput = false;
            this.lblBedNO.Location = new System.Drawing.Point(473, 236);
            this.lblBedNO.Name = "lblBedNO";
            this.lblBedNO.Size = new System.Drawing.Size(47, 12);
            this.lblBedNO.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblBedNO.TabIndex = 143;
            this.lblBedNO.Text = "病床号:";
            // 
            // lblComputerNO
            // 
            this.lblComputerNO.AutoSize = true;
            this.lblComputerNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblComputerNO.InputMsg = "";
            this.lblComputerNO.IsDefaultCHInput = false;
            this.lblComputerNO.IsTagInput = false;
            this.lblComputerNO.IsTextInput = false;
            this.lblComputerNO.Location = new System.Drawing.Point(461, 264);
            this.lblComputerNO.Name = "lblComputerNO";
            this.lblComputerNO.Size = new System.Drawing.Size(59, 12);
            this.lblComputerNO.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblComputerNO.TabIndex = 131;
            this.lblComputerNO.Text = "电 脑 号:";
            // 
            // plInfomation
            // 
            this.plInfomation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plInfomation.ContextMenuStrip = this.neuContextMenuStrip1;
            this.plInfomation.Controls.Add(this.txtHomeAddressZip);
            this.plInfomation.Controls.Add(this.neuInputLabel4);
            this.plInfomation.Controls.Add(this.txtWorkZip);
            this.plInfomation.Controls.Add(this.neuInputLabel3);
            this.plInfomation.Controls.Add(this.rbtnTempPatientNO);
            this.plInfomation.Controls.Add(this.txtHomeZip);
            this.plInfomation.Controls.Add(this.neuInputLabel2);
            this.plInfomation.Controls.Add(this.txtName);
            this.plInfomation.Controls.Add(this.neuInputLabel1);
            this.plInfomation.Controls.Add(this.txtClinicNO);
            this.plInfomation.Controls.Add(this.lblName);
            this.plInfomation.Controls.Add(this.chbencrypt);
            this.plInfomation.Controls.Add(this.cmbSex);
            this.plInfomation.Controls.Add(this.dtpBirthDay);
            this.plInfomation.Controls.Add(this.txtAge);
            this.plInfomation.Controls.Add(this.txtIDNO);
            this.plInfomation.Controls.Add(this.lblDoctDept);
            this.plInfomation.Controls.Add(this.lblIDNO);
            this.plInfomation.Controls.Add(this.lblBirthday);
            this.plInfomation.Controls.Add(this.lblSex);
            this.plInfomation.Controls.Add(this.cmbBirthArea);
            this.plInfomation.Controls.Add(this.cmbCountry);
            this.plInfomation.Controls.Add(this.cmbMarry);
            this.plInfomation.Controls.Add(this.cmbProfession);
            this.plInfomation.Controls.Add(this.txtBirthArea);
            this.plInfomation.Controls.Add(this.cmbNation);
            this.plInfomation.Controls.Add(this.txtWorkAddress);
            this.plInfomation.Controls.Add(this.txtWorkPhone);
            this.plInfomation.Controls.Add(this.txtHomePhone);
            this.plInfomation.Controls.Add(this.txtAddressNow);
            this.plInfomation.Controls.Add(this.txtHomeAddress);
            this.plInfomation.Controls.Add(this.txtLinkMan);
            this.plInfomation.Controls.Add(this.cmbRelation);
            this.plInfomation.Controls.Add(this.txtLinkPhone);
            this.plInfomation.Controls.Add(this.txtLinkAddr);
            this.plInfomation.Controls.Add(this.txtDiagnose);
            this.plInfomation.Controls.Add(this.cmbInSource);
            this.plInfomation.Controls.Add(this.cmbCircs);
            this.plInfomation.Controls.Add(this.cmbApproach);
            this.plInfomation.Controls.Add(this.cmbDept);
            this.plInfomation.Controls.Add(this.cmbNurseCell);
            this.plInfomation.Controls.Add(this.cmbBedNO);
            this.plInfomation.Controls.Add(this.cmbDoctor);
            this.plInfomation.Controls.Add(this.cmbPact);
            this.plInfomation.Controls.Add(this.txtMCardNO);
            this.plInfomation.Controls.Add(this.txtComputerNO);
            this.plInfomation.Controls.Add(this.mtxtBloodFee);
            this.plInfomation.Controls.Add(this.mTxtIntimes);
            this.plInfomation.Controls.Add(this.mTxtPrepay);
            this.plInfomation.Controls.Add(this.cmbPayMode);
            this.plInfomation.Controls.Add(this.dtpInTime);
            this.plInfomation.Controls.Add(this.txtInpatientNO);
            this.plInfomation.Controls.Add(this.neuLabel2);
            this.plInfomation.Controls.Add(this.neuLabel1);
            this.plInfomation.Controls.Add(this.cmbOldDept);
            this.plInfomation.Controls.Add(this.lblOldDept);
            this.plInfomation.Controls.Add(this.lbAddressNow);
            this.plInfomation.Controls.Add(this.neuLabel6);
            this.plInfomation.Controls.Add(this.lblBedCount);
            this.plInfomation.Controls.Add(this.lblDept);
            this.plInfomation.Controls.Add(this.lblInTimes);
            this.plInfomation.Controls.Add(this.lblBloodFee);
            this.plInfomation.Controls.Add(this.lblDiagnose);
            this.plInfomation.Controls.Add(this.lblDoctor);
            this.plInfomation.Controls.Add(this.lblCircs);
            this.plInfomation.Controls.Add(this.lblApproach);
            this.plInfomation.Controls.Add(this.lblInSource);
            this.plInfomation.Controls.Add(this.lblLinkPhone);
            this.plInfomation.Controls.Add(this.lblLinkAddress);
            this.plInfomation.Controls.Add(this.lblRelation);
            this.plInfomation.Controls.Add(this.lblLinkMan);
            this.plInfomation.Controls.Add(this.lblHomeAddress);
            this.plInfomation.Controls.Add(this.lblWorkPhone);
            this.plInfomation.Controls.Add(this.lblProfession);
            this.plInfomation.Controls.Add(this.lblCountry);
            this.plInfomation.Controls.Add(this.lblBirthArea);
            this.plInfomation.Controls.Add(this.lblMarry);
            this.plInfomation.Controls.Add(this.lblWorkAddress);
            this.plInfomation.Controls.Add(this.lblBedNO);
            this.plInfomation.Controls.Add(this.lblNation);
            this.plInfomation.Controls.Add(this.lblComputerNO);
            this.plInfomation.Controls.Add(this.lblPact);
            this.plInfomation.Controls.Add(this.lblInTime);
            this.plInfomation.Controls.Add(this.lblMCardNO);
            this.plInfomation.Controls.Add(this.rdoInpatientNO);
            this.plInfomation.Controls.Add(this.lblPrepay);
            this.plInfomation.Controls.Add(this.lblPayMode);
            this.plInfomation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plInfomation.Location = new System.Drawing.Point(3, 3);
            this.plInfomation.Name = "plInfomation";
            this.plInfomation.Size = new System.Drawing.Size(901, 345);
            this.plInfomation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plInfomation.TabIndex = 2;
            // 
            // neuContextMenuStrip1
            // 
            this.neuContextMenuStrip1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuContextMenuStrip1.Name = "neuContextMenuStrip1";
            this.neuContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.neuContextMenuStrip1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // txtHomeAddressZip
            // 
            this.txtHomeAddressZip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHomeAddressZip.InputMsg = "户口邮编不能为空！";
            this.txtHomeAddressZip.IsDefaultCHInput = false;
            this.txtHomeAddressZip.IsEnter2Tab = false;
            this.txtHomeAddressZip.IsTagInput = false;
            this.txtHomeAddressZip.IsTextInput = true;
            this.txtHomeAddressZip.Location = new System.Drawing.Point(308, 143);
            this.txtHomeAddressZip.MaxLength = 0;
            this.txtHomeAddressZip.Name = "txtHomeAddressZip";
            this.txtHomeAddressZip.Size = new System.Drawing.Size(132, 21);
            this.txtHomeAddressZip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHomeAddressZip.TabIndex = 139;
            // 
            // neuInputLabel4
            // 
            this.neuInputLabel4.AutoSize = true;
            this.neuInputLabel4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuInputLabel4.ForeColor = System.Drawing.Color.Blue;
            this.neuInputLabel4.InputMsg = "";
            this.neuInputLabel4.IsDefaultCHInput = false;
            this.neuInputLabel4.IsTagInput = false;
            this.neuInputLabel4.IsTextInput = false;
            this.neuInputLabel4.Location = new System.Drawing.Point(266, 148);
            this.neuInputLabel4.Name = "neuInputLabel4";
            this.neuInputLabel4.Size = new System.Drawing.Size(35, 12);
            this.neuInputLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuInputLabel4.TabIndex = 200;
            this.neuInputLabel4.Text = "邮编:";
            // 
            // txtWorkZip
            // 
            this.txtWorkZip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWorkZip.InputMsg = "单位邮编不能为空！";
            this.txtWorkZip.IsDefaultCHInput = false;
            this.txtWorkZip.IsEnter2Tab = false;
            this.txtWorkZip.IsTagInput = false;
            this.txtWorkZip.IsTextInput = true;
            this.txtWorkZip.Location = new System.Drawing.Point(736, 91);
            this.txtWorkZip.MaxLength = 0;
            this.txtWorkZip.Name = "txtWorkZip";
            this.txtWorkZip.Size = new System.Drawing.Size(143, 21);
            this.txtWorkZip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtWorkZip.TabIndex = 134;
            // 
            // neuInputLabel3
            // 
            this.neuInputLabel3.AutoSize = true;
            this.neuInputLabel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuInputLabel3.ForeColor = System.Drawing.Color.Blue;
            this.neuInputLabel3.InputMsg = "";
            this.neuInputLabel3.IsDefaultCHInput = false;
            this.neuInputLabel3.IsTagInput = false;
            this.neuInputLabel3.IsTextInput = false;
            this.neuInputLabel3.Location = new System.Drawing.Point(674, 97);
            this.neuInputLabel3.Name = "neuInputLabel3";
            this.neuInputLabel3.Size = new System.Drawing.Size(59, 12);
            this.neuInputLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuInputLabel3.TabIndex = 199;
            this.neuInputLabel3.Text = "单位邮编:";
            // 
            // rbtnTempPatientNO
            // 
            this.rbtnTempPatientNO.AutoSize = true;
            this.rbtnTempPatientNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnTempPatientNO.ForeColor = System.Drawing.Color.Blue;
            this.rbtnTempPatientNO.Location = new System.Drawing.Point(332, 320);
            this.rbtnTempPatientNO.Name = "rbtnTempPatientNO";
            this.rbtnTempPatientNO.Size = new System.Drawing.Size(83, 16);
            this.rbtnTempPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnTempPatientNO.TabIndex = 196;
            this.rbtnTempPatientNO.Text = "临时号(F2)";
            this.rbtnTempPatientNO.UseVisualStyleBackColor = false;
            // 
            // txtHomeZip
            // 
            this.txtHomeZip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHomeZip.InputMsg = "住址邮编不能为空！";
            this.txtHomeZip.IsDefaultCHInput = false;
            this.txtHomeZip.IsEnter2Tab = false;
            this.txtHomeZip.IsTagInput = false;
            this.txtHomeZip.IsTextInput = true;
            this.txtHomeZip.Location = new System.Drawing.Point(736, 119);
            this.txtHomeZip.MaxLength = 0;
            this.txtHomeZip.Name = "txtHomeZip";
            this.txtHomeZip.Size = new System.Drawing.Size(143, 21);
            this.txtHomeZip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHomeZip.TabIndex = 137;
            // 
            // neuInputLabel2
            // 
            this.neuInputLabel2.AutoSize = true;
            this.neuInputLabel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuInputLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuInputLabel2.InputMsg = "";
            this.neuInputLabel2.IsDefaultCHInput = false;
            this.neuInputLabel2.IsTagInput = false;
            this.neuInputLabel2.IsTextInput = false;
            this.neuInputLabel2.Location = new System.Drawing.Point(671, 125);
            this.neuInputLabel2.Name = "neuInputLabel2";
            this.neuInputLabel2.Size = new System.Drawing.Size(59, 12);
            this.neuInputLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuInputLabel2.TabIndex = 195;
            this.neuInputLabel2.Text = "住址邮编:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.InputMsg = "姓名不能为空！";
            this.txtName.IsDefaultCHInput = false;
            this.txtName.IsEnter2Tab = false;
            this.txtName.IsTagInput = false;
            this.txtName.IsTextInput = true;
            this.txtName.Location = new System.Drawing.Point(308, 7);
            this.txtName.MaxLength = 0;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(132, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 1;
            // 
            // neuInputLabel1
            // 
            this.neuInputLabel1.AutoSize = true;
            this.neuInputLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuInputLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuInputLabel1.InputMsg = "";
            this.neuInputLabel1.IsDefaultCHInput = false;
            this.neuInputLabel1.IsTagInput = false;
            this.neuInputLabel1.IsTextInput = false;
            this.neuInputLabel1.Location = new System.Drawing.Point(36, 12);
            this.neuInputLabel1.Name = "neuInputLabel1";
            this.neuInputLabel1.Size = new System.Drawing.Size(47, 12);
            this.neuInputLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuInputLabel1.TabIndex = 193;
            this.neuInputLabel1.Text = "门诊号:";
            // 
            // txtClinicNO
            // 
            this.txtClinicNO.Enabled = false;
            this.txtClinicNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtClinicNO.InputMsg = "门诊号不能为空";
            this.txtClinicNO.IsDefaultCHInput = false;
            this.txtClinicNO.IsEnter2Tab = false;
            this.txtClinicNO.IsTagInput = false;
            this.txtClinicNO.IsTextInput = true;
            this.txtClinicNO.Location = new System.Drawing.Point(90, 7);
            this.txtClinicNO.Name = "txtClinicNO";
            this.txtClinicNO.Size = new System.Drawing.Size(144, 21);
            this.txtClinicNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtClinicNO.TabIndex = 0;
            this.txtClinicNO.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.Blue;
            this.lblName.InputMsg = "";
            this.lblName.IsDefaultCHInput = false;
            this.lblName.IsTagInput = false;
            this.lblName.IsTextInput = false;
            this.lblName.Location = new System.Drawing.Point(271, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 12);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblName.TabIndex = 13;
            this.lblName.Text = "姓名:";
            // 
            // chbencrypt
            // 
            this.chbencrypt.AutoSize = true;
            this.chbencrypt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbencrypt.Location = new System.Drawing.Point(392, 10);
            this.chbencrypt.Name = "chbencrypt";
            this.chbencrypt.Size = new System.Drawing.Size(48, 16);
            this.chbencrypt.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chbencrypt.TabIndex = 0;
            this.chbencrypt.Text = "加密";
            this.chbencrypt.UseVisualStyleBackColor = true;
            this.chbencrypt.Visible = false;
            // 
            // cmbSex
            // 
            this.cmbSex.EnterVisiable = true;
            this.cmbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSex.InputMsg = "性别不能为空！";
            this.cmbSex.IsDefaultCHInput = false;
            this.cmbSex.IsFind = true;
            this.cmbSex.IsTagInput = true;
            this.cmbSex.IsTextInput = true;
            this.cmbSex.ListBoxHeight = 100;
            this.cmbSex.ListBoxWidth = 100;
            this.cmbSex.Location = new System.Drawing.Point(527, 7);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.OmitFilter = true;
            this.cmbSex.SelectedItem = null;
            this.cmbSex.SelectNone = true;
            this.cmbSex.ShowID = true;
            this.cmbSex.Size = new System.Drawing.Size(64, 21);
            this.cmbSex.TabIndex = 2;
            this.cmbSex.Tag = "";
            // 
            // dtpBirthDay
            // 
            this.dtpBirthDay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpBirthDay.InputMsg = "出生日期格式不正确!";
            this.dtpBirthDay.IsDefaultCHInput = false;
            this.dtpBirthDay.IsTagInput = false;
            this.dtpBirthDay.IsTextInput = true;
            this.dtpBirthDay.Location = new System.Drawing.Point(653, 7);
            this.dtpBirthDay.Mask = "0000-00-00";
            this.dtpBirthDay.Name = "dtpBirthDay";
            this.dtpBirthDay.Size = new System.Drawing.Size(118, 21);
            this.dtpBirthDay.TabIndex = 3;
            // 
            // txtAge
            // 
            this.txtAge.Enabled = false;
            this.txtAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAge.InputMsg = "年龄不允许为空！";
            this.txtAge.IsDefaultCHInput = false;
            this.txtAge.IsEnter2Tab = false;
            this.txtAge.IsTagInput = false;
            this.txtAge.IsTextInput = true;
            this.txtAge.Location = new System.Drawing.Point(777, 7);
            this.txtAge.MaxLength = 3;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(103, 21);
            this.txtAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAge.TabIndex = 3;
            // 
            // txtIDNO
            // 
            this.txtIDNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIDNO.InputMsg = "";
            this.txtIDNO.IsDefaultCHInput = false;
            this.txtIDNO.IsEnter2Tab = false;
            this.txtIDNO.IsTagInput = false;
            this.txtIDNO.IsTextInput = false;
            this.txtIDNO.Location = new System.Drawing.Point(90, 35);
            this.txtIDNO.MaxLength = 18;
            this.txtIDNO.Name = "txtIDNO";
            this.txtIDNO.Size = new System.Drawing.Size(350, 21);
            this.txtIDNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIDNO.TabIndex = 4;
            // 
            // lblDoctDept
            // 
            this.lblDoctDept.AutoSize = true;
            this.lblDoctDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoctDept.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblDoctDept.InputMsg = "";
            this.lblDoctDept.IsDefaultCHInput = false;
            this.lblDoctDept.IsTagInput = false;
            this.lblDoctDept.IsTextInput = false;
            this.lblDoctDept.Location = new System.Drawing.Point(856, 237);
            this.lblDoctDept.Name = "lblDoctDept";
            this.lblDoctDept.Size = new System.Drawing.Size(29, 12);
            this.lblDoctDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoctDept.TabIndex = 27;
            this.lblDoctDept.Text = "----";
            // 
            // lblIDNO
            // 
            this.lblIDNO.AutoSize = true;
            this.lblIDNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIDNO.ForeColor = System.Drawing.Color.Black;
            this.lblIDNO.InputMsg = "";
            this.lblIDNO.IsDefaultCHInput = false;
            this.lblIDNO.IsTagInput = false;
            this.lblIDNO.IsTextInput = false;
            this.lblIDNO.Location = new System.Drawing.Point(26, 40);
            this.lblIDNO.Name = "lblIDNO";
            this.lblIDNO.Size = new System.Drawing.Size(59, 12);
            this.lblIDNO.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblIDNO.TabIndex = 39;
            this.lblIDNO.Text = "身份证号:";
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBirthday.ForeColor = System.Drawing.Color.Blue;
            this.lblBirthday.InputMsg = "";
            this.lblBirthday.IsDefaultCHInput = false;
            this.lblBirthday.IsTagInput = false;
            this.lblBirthday.IsTextInput = false;
            this.lblBirthday.Location = new System.Drawing.Point(597, 12);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(59, 12);
            this.lblBirthday.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblBirthday.TabIndex = 23;
            this.lblBirthday.Text = "出生日期:";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.ForeColor = System.Drawing.Color.Blue;
            this.lblSex.InputMsg = "";
            this.lblSex.IsDefaultCHInput = false;
            this.lblSex.IsTagInput = false;
            this.lblSex.IsTextInput = false;
            this.lblSex.Location = new System.Drawing.Point(485, 12);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(35, 12);
            this.lblSex.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblSex.TabIndex = 15;
            this.lblSex.Text = "性别:";
            // 
            // cmbBirthArea
            // 
            this.cmbBirthArea.EnterVisiable = true;
            this.cmbBirthArea.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbBirthArea.InputMsg = "籍贯不能为空！";
            this.cmbBirthArea.IsDefaultCHInput = false;
            this.cmbBirthArea.IsFind = true;
            this.cmbBirthArea.IsTagInput = false;
            this.cmbBirthArea.IsTextInput = true;
            this.cmbBirthArea.ListBoxHeight = 100;
            this.cmbBirthArea.ListBoxWidth = 100;
            this.cmbBirthArea.Location = new System.Drawing.Point(527, 34);
            this.cmbBirthArea.Name = "cmbBirthArea";
            this.cmbBirthArea.OmitFilter = true;
            this.cmbBirthArea.SelectedItem = null;
            this.cmbBirthArea.SelectNone = true;
            this.cmbBirthArea.ShowID = true;
            this.cmbBirthArea.Size = new System.Drawing.Size(131, 21);
            this.cmbBirthArea.TabIndex = 122;
            this.cmbBirthArea.Tag = "";
            // 
            // cmbCountry
            // 
            this.cmbCountry.EnterVisiable = true;
            this.cmbCountry.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCountry.InputMsg = "国籍不能为空！";
            this.cmbCountry.IsDefaultCHInput = false;
            this.cmbCountry.IsFind = true;
            this.cmbCountry.IsTagInput = false;
            this.cmbCountry.IsTextInput = true;
            this.cmbCountry.ListBoxHeight = 100;
            this.cmbCountry.ListBoxWidth = 100;
            this.cmbCountry.Location = new System.Drawing.Point(736, 34);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.OmitFilter = true;
            this.cmbCountry.SelectedItem = null;
            this.cmbCountry.SelectNone = true;
            this.cmbCountry.ShowID = true;
            this.cmbCountry.Size = new System.Drawing.Size(144, 21);
            this.cmbCountry.TabIndex = 124;
            this.cmbCountry.Tag = "";
            // 
            // cmbMarry
            // 
            this.cmbMarry.EnterVisiable = true;
            this.cmbMarry.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbMarry.InputMsg = "婚姻状况不能为空！";
            this.cmbMarry.IsDefaultCHInput = false;
            this.cmbMarry.IsFind = true;
            this.cmbMarry.IsTagInput = false;
            this.cmbMarry.IsTextInput = true;
            this.cmbMarry.ListBoxHeight = 100;
            this.cmbMarry.ListBoxWidth = 100;
            this.cmbMarry.Location = new System.Drawing.Point(90, 63);
            this.cmbMarry.Name = "cmbMarry";
            this.cmbMarry.OmitFilter = true;
            this.cmbMarry.SelectedItem = null;
            this.cmbMarry.SelectNone = true;
            this.cmbMarry.ShowID = true;
            this.cmbMarry.Size = new System.Drawing.Size(143, 21);
            this.cmbMarry.TabIndex = 126;
            this.cmbMarry.Tag = "";
            // 
            // cmbProfession
            // 
            this.cmbProfession.EnterVisiable = true;
            this.cmbProfession.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbProfession.InputMsg = "职业不能为空！";
            this.cmbProfession.IsDefaultCHInput = false;
            this.cmbProfession.IsFind = true;
            this.cmbProfession.IsTagInput = false;
            this.cmbProfession.IsTextInput = true;
            this.cmbProfession.ListBoxHeight = 100;
            this.cmbProfession.ListBoxWidth = 100;
            this.cmbProfession.Location = new System.Drawing.Point(308, 62);
            this.cmbProfession.Name = "cmbProfession";
            this.cmbProfession.OmitFilter = true;
            this.cmbProfession.SelectedItem = null;
            this.cmbProfession.SelectNone = true;
            this.cmbProfession.ShowID = false;
            this.cmbProfession.Size = new System.Drawing.Size(132, 21);
            this.cmbProfession.TabIndex = 127;
            this.cmbProfession.Tag = "";
            // 
            // txtBirthArea
            // 
            this.txtBirthArea.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBirthArea.InputMsg = "出生地不能为空！";
            this.txtBirthArea.IsDefaultCHInput = true;
            this.txtBirthArea.IsEnter2Tab = false;
            this.txtBirthArea.IsTagInput = false;
            this.txtBirthArea.IsTextInput = true;
            this.txtBirthArea.Location = new System.Drawing.Point(527, 62);
            this.txtBirthArea.Name = "txtBirthArea";
            this.txtBirthArea.Size = new System.Drawing.Size(131, 21);
            this.txtBirthArea.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBirthArea.TabIndex = 128;
            // 
            // cmbNation
            // 
            this.cmbNation.EnterVisiable = true;
            this.cmbNation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbNation.InputMsg = "民族不能为空！";
            this.cmbNation.IsDefaultCHInput = false;
            this.cmbNation.IsFind = true;
            this.cmbNation.IsTagInput = false;
            this.cmbNation.IsTextInput = true;
            this.cmbNation.ListBoxHeight = 100;
            this.cmbNation.ListBoxWidth = 100;
            this.cmbNation.Location = new System.Drawing.Point(736, 62);
            this.cmbNation.Name = "cmbNation";
            this.cmbNation.OmitFilter = true;
            this.cmbNation.SelectedItem = null;
            this.cmbNation.SelectNone = true;
            this.cmbNation.ShowID = true;
            this.cmbNation.Size = new System.Drawing.Size(144, 21);
            this.cmbNation.TabIndex = 130;
            this.cmbNation.Tag = "";
            // 
            // txtWorkAddress
            // 
            this.txtWorkAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWorkAddress.InputMsg = "单位地址不能为空！";
            this.txtWorkAddress.IsDefaultCHInput = true;
            this.txtWorkAddress.IsEnter2Tab = false;
            this.txtWorkAddress.IsTagInput = false;
            this.txtWorkAddress.IsTextInput = true;
            this.txtWorkAddress.Location = new System.Drawing.Point(90, 91);
            this.txtWorkAddress.Name = "txtWorkAddress";
            this.txtWorkAddress.Size = new System.Drawing.Size(350, 21);
            this.txtWorkAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtWorkAddress.TabIndex = 132;
            // 
            // txtWorkPhone
            // 
            this.txtWorkPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWorkPhone.InputMsg = "单位电话不能为空！";
            this.txtWorkPhone.IsDefaultCHInput = false;
            this.txtWorkPhone.IsEnter2Tab = false;
            this.txtWorkPhone.IsTagInput = false;
            this.txtWorkPhone.IsTextInput = true;
            this.txtWorkPhone.Location = new System.Drawing.Point(527, 90);
            this.txtWorkPhone.MaxLength = 0;
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.Size = new System.Drawing.Size(131, 21);
            this.txtWorkPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtWorkPhone.TabIndex = 133;
            // 
            // txtHomePhone
            // 
            this.txtHomePhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHomePhone.InputMsg = "家庭电话不能为空！";
            this.txtHomePhone.IsDefaultCHInput = false;
            this.txtHomePhone.IsEnter2Tab = false;
            this.txtHomePhone.IsTagInput = false;
            this.txtHomePhone.IsTextInput = true;
            this.txtHomePhone.Location = new System.Drawing.Point(527, 119);
            this.txtHomePhone.MaxLength = 0;
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Size = new System.Drawing.Size(131, 21);
            this.txtHomePhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHomePhone.TabIndex = 136;
            // 
            // txtAddressNow
            // 
            this.txtAddressNow.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAddressNow.InputMsg = "现住址不能为空！";
            this.txtAddressNow.IsDefaultCHInput = false;
            this.txtAddressNow.IsEnter2Tab = false;
            this.txtAddressNow.IsTagInput = false;
            this.txtAddressNow.IsTextInput = true;
            this.txtAddressNow.Location = new System.Drawing.Point(90, 119);
            this.txtAddressNow.Name = "txtAddressNow";
            this.txtAddressNow.Size = new System.Drawing.Size(350, 21);
            this.txtAddressNow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAddressNow.TabIndex = 135;
            // 
            // txtHomeAddress
            // 
            this.txtHomeAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHomeAddress.InputMsg = "户口地址不能为空！";
            this.txtHomeAddress.IsDefaultCHInput = true;
            this.txtHomeAddress.IsEnter2Tab = false;
            this.txtHomeAddress.IsTagInput = false;
            this.txtHomeAddress.IsTextInput = true;
            this.txtHomeAddress.Location = new System.Drawing.Point(90, 146);
            this.txtHomeAddress.Name = "txtHomeAddress";
            this.txtHomeAddress.Size = new System.Drawing.Size(165, 21);
            this.txtHomeAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHomeAddress.TabIndex = 138;
            // 
            // txtLinkMan
            // 
            this.txtLinkMan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLinkMan.InputMsg = "联系人不能为空！";
            this.txtLinkMan.IsDefaultCHInput = false;
            this.txtLinkMan.IsEnter2Tab = false;
            this.txtLinkMan.IsTagInput = false;
            this.txtLinkMan.IsTextInput = true;
            this.txtLinkMan.Location = new System.Drawing.Point(527, 145);
            this.txtLinkMan.MaxLength = 0;
            this.txtLinkMan.Name = "txtLinkMan";
            this.txtLinkMan.Size = new System.Drawing.Size(131, 21);
            this.txtLinkMan.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLinkMan.TabIndex = 140;
            // 
            // cmbRelation
            // 
            this.cmbRelation.EnterVisiable = true;
            this.cmbRelation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbRelation.InputMsg = "关系不能为空！";
            this.cmbRelation.IsDefaultCHInput = false;
            this.cmbRelation.IsFind = true;
            this.cmbRelation.IsTagInput = false;
            this.cmbRelation.IsTextInput = true;
            this.cmbRelation.ListBoxHeight = 100;
            this.cmbRelation.ListBoxWidth = 100;
            this.cmbRelation.Location = new System.Drawing.Point(736, 146);
            this.cmbRelation.Name = "cmbRelation";
            this.cmbRelation.OmitFilter = true;
            this.cmbRelation.SelectedItem = null;
            this.cmbRelation.SelectNone = true;
            this.cmbRelation.ShowID = true;
            this.cmbRelation.Size = new System.Drawing.Size(143, 21);
            this.cmbRelation.TabIndex = 141;
            this.cmbRelation.Tag = "";
            // 
            // txtLinkPhone
            // 
            this.txtLinkPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLinkPhone.InputMsg = "联系人电话不能为空！";
            this.txtLinkPhone.IsDefaultCHInput = false;
            this.txtLinkPhone.IsEnter2Tab = false;
            this.txtLinkPhone.IsTagInput = false;
            this.txtLinkPhone.IsTextInput = true;
            this.txtLinkPhone.Location = new System.Drawing.Point(308, 174);
            this.txtLinkPhone.MaxLength = 20;
            this.txtLinkPhone.Name = "txtLinkPhone";
            this.txtLinkPhone.Size = new System.Drawing.Size(132, 21);
            this.txtLinkPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLinkPhone.TabIndex = 143;
            // 
            // txtLinkAddr
            // 
            this.txtLinkAddr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLinkAddr.InputMsg = "联系人地址不能为空！";
            this.txtLinkAddr.IsDefaultCHInput = true;
            this.txtLinkAddr.IsEnter2Tab = false;
            this.txtLinkAddr.IsTagInput = false;
            this.txtLinkAddr.IsTextInput = true;
            this.txtLinkAddr.Location = new System.Drawing.Point(90, 175);
            this.txtLinkAddr.Name = "txtLinkAddr";
            this.txtLinkAddr.Size = new System.Drawing.Size(144, 21);
            this.txtLinkAddr.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLinkAddr.TabIndex = 142;
            // 
            // txtDiagnose
            // 
            this.txtDiagnose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDiagnose.InputMsg = "";
            this.txtDiagnose.IsDefaultCHInput = true;
            this.txtDiagnose.IsEnter2Tab = false;
            this.txtDiagnose.IsTagInput = false;
            this.txtDiagnose.IsTextInput = false;
            this.txtDiagnose.Location = new System.Drawing.Point(527, 174);
            this.txtDiagnose.MaxLength = 0;
            this.txtDiagnose.Name = "txtDiagnose";
            this.txtDiagnose.Size = new System.Drawing.Size(353, 21);
            this.txtDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDiagnose.TabIndex = 144;
            // 
            // cmbInSource
            // 
            this.cmbInSource.EnterVisiable = true;
            this.cmbInSource.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbInSource.InputMsg = "病人来源不能为空！";
            this.cmbInSource.IsDefaultCHInput = false;
            this.cmbInSource.IsFind = true;
            this.cmbInSource.IsTagInput = true;
            this.cmbInSource.IsTextInput = false;
            this.cmbInSource.ListBoxHeight = 100;
            this.cmbInSource.ListBoxWidth = 100;
            this.cmbInSource.Location = new System.Drawing.Point(90, 203);
            this.cmbInSource.Name = "cmbInSource";
            this.cmbInSource.OmitFilter = true;
            this.cmbInSource.SelectedItem = null;
            this.cmbInSource.SelectNone = false;
            this.cmbInSource.ShowID = true;
            this.cmbInSource.Size = new System.Drawing.Size(144, 21);
            this.cmbInSource.TabIndex = 145;
            this.cmbInSource.Tag = "";
            // 
            // cmbCircs
            // 
            this.cmbCircs.EnterVisiable = true;
            this.cmbCircs.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCircs.InputMsg = "入院情况不能为空！";
            this.cmbCircs.IsDefaultCHInput = false;
            this.cmbCircs.IsFind = true;
            this.cmbCircs.IsTagInput = true;
            this.cmbCircs.IsTextInput = false;
            this.cmbCircs.ListBoxHeight = 100;
            this.cmbCircs.ListBoxWidth = 100;
            this.cmbCircs.Location = new System.Drawing.Point(308, 202);
            this.cmbCircs.Name = "cmbCircs";
            this.cmbCircs.OmitFilter = true;
            this.cmbCircs.SelectedItem = null;
            this.cmbCircs.SelectNone = true;
            this.cmbCircs.ShowID = true;
            this.cmbCircs.Size = new System.Drawing.Size(132, 21);
            this.cmbCircs.TabIndex = 146;
            this.cmbCircs.Tag = "";
            // 
            // cmbApproach
            // 
            this.cmbApproach.EnterVisiable = true;
            this.cmbApproach.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbApproach.InputMsg = "入院途径不能为空！";
            this.cmbApproach.IsDefaultCHInput = false;
            this.cmbApproach.IsFind = true;
            this.cmbApproach.IsTagInput = true;
            this.cmbApproach.IsTextInput = false;
            this.cmbApproach.ListBoxHeight = 100;
            this.cmbApproach.ListBoxWidth = 100;
            this.cmbApproach.Location = new System.Drawing.Point(527, 202);
            this.cmbApproach.Name = "cmbApproach";
            this.cmbApproach.OmitFilter = true;
            this.cmbApproach.SelectedItem = null;
            this.cmbApproach.SelectNone = false;
            this.cmbApproach.ShowID = true;
            this.cmbApproach.Size = new System.Drawing.Size(131, 21);
            this.cmbApproach.TabIndex = 147;
            this.cmbApproach.Tag = "";
            // 
            // cmbDept
            // 
            this.cmbDept.Enabled = false;
            this.cmbDept.EnterVisiable = true;
            this.cmbDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDept.InputMsg = "入院科室不能为空！";
            this.cmbDept.IsDefaultCHInput = false;
            this.cmbDept.IsFind = true;
            this.cmbDept.IsTagInput = true;
            this.cmbDept.IsTextInput = true;
            this.cmbDept.ListBoxHeight = 100;
            this.cmbDept.ListBoxWidth = 100;
            this.cmbDept.Location = new System.Drawing.Point(90, 231);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.OmitFilter = true;
            this.cmbDept.SelectedItem = null;
            this.cmbDept.SelectNone = false;
            this.cmbDept.ShowID = true;
            this.cmbDept.Size = new System.Drawing.Size(143, 21);
            this.cmbDept.TabIndex = 149;
            this.cmbDept.Tag = "";
            // 
            // cmbNurseCell
            // 
            this.cmbNurseCell.AccessibleDescription = "cmbPayMode";
            this.cmbNurseCell.Enabled = false;
            this.cmbNurseCell.EnterVisiable = true;
            this.cmbNurseCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbNurseCell.InputMsg = "入院病区不能为空！";
            this.cmbNurseCell.IsDefaultCHInput = false;
            this.cmbNurseCell.IsFind = true;
            this.cmbNurseCell.IsTagInput = true;
            this.cmbNurseCell.IsTextInput = true;
            this.cmbNurseCell.ListBoxHeight = 100;
            this.cmbNurseCell.ListBoxWidth = 100;
            this.cmbNurseCell.Location = new System.Drawing.Point(308, 230);
            this.cmbNurseCell.Name = "cmbNurseCell";
            this.cmbNurseCell.OmitFilter = true;
            this.cmbNurseCell.SelectedItem = null;
            this.cmbNurseCell.SelectNone = false;
            this.cmbNurseCell.ShowID = true;
            this.cmbNurseCell.Size = new System.Drawing.Size(132, 21);
            this.cmbNurseCell.TabIndex = 150;
            this.cmbNurseCell.Tag = "";
            // 
            // cmbBedNO
            // 
            this.cmbBedNO.Enabled = false;
            this.cmbBedNO.EnterVisiable = true;
            this.cmbBedNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbBedNO.InputMsg = "";
            this.cmbBedNO.IsDefaultCHInput = false;
            this.cmbBedNO.IsFind = true;
            this.cmbBedNO.IsTagInput = false;
            this.cmbBedNO.IsTextInput = false;
            this.cmbBedNO.ListBoxHeight = 100;
            this.cmbBedNO.ListBoxWidth = 100;
            this.cmbBedNO.Location = new System.Drawing.Point(527, 230);
            this.cmbBedNO.Name = "cmbBedNO";
            this.cmbBedNO.OmitFilter = true;
            this.cmbBedNO.SelectedItem = null;
            this.cmbBedNO.SelectNone = true;
            this.cmbBedNO.ShowID = true;
            this.cmbBedNO.Size = new System.Drawing.Size(74, 21);
            this.cmbBedNO.TabIndex = 151;
            this.cmbBedNO.Tag = "";
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.Enabled = false;
            this.cmbDoctor.EnterVisiable = true;
            this.cmbDoctor.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDoctor.InputMsg = "收住医师不能为空！";
            this.cmbDoctor.IsDefaultCHInput = false;
            this.cmbDoctor.IsFind = true;
            this.cmbDoctor.IsTagInput = true;
            this.cmbDoctor.IsTextInput = true;
            this.cmbDoctor.ListBoxHeight = 100;
            this.cmbDoctor.ListBoxWidth = 100;
            this.cmbDoctor.Location = new System.Drawing.Point(736, 230);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.OmitFilter = true;
            this.cmbDoctor.SelectedItem = null;
            this.cmbDoctor.SelectNone = true;
            this.cmbDoctor.ShowID = true;
            this.cmbDoctor.Size = new System.Drawing.Size(114, 21);
            this.cmbDoctor.TabIndex = 152;
            this.cmbDoctor.Tag = "";
            // 
            // cmbPact
            // 
            this.cmbPact.Enabled = false;
            this.cmbPact.EnterVisiable = true;
            this.cmbPact.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPact.InputMsg = "结算方式不能为空！";
            this.cmbPact.IsDefaultCHInput = false;
            this.cmbPact.IsFind = true;
            this.cmbPact.IsTagInput = true;
            this.cmbPact.IsTextInput = true;
            this.cmbPact.ListBoxHeight = 100;
            this.cmbPact.ListBoxWidth = 100;
            this.cmbPact.Location = new System.Drawing.Point(90, 259);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.OmitFilter = true;
            this.cmbPact.SelectedItem = null;
            this.cmbPact.SelectNone = true;
            this.cmbPact.ShowID = true;
            this.cmbPact.Size = new System.Drawing.Size(143, 21);
            this.cmbPact.TabIndex = 153;
            this.cmbPact.Tag = "";
            // 
            // txtMCardNO
            // 
            this.txtMCardNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMCardNO.InputMsg = "";
            this.txtMCardNO.IsDefaultCHInput = false;
            this.txtMCardNO.IsEnter2Tab = false;
            this.txtMCardNO.IsTagInput = false;
            this.txtMCardNO.IsTextInput = false;
            this.txtMCardNO.Location = new System.Drawing.Point(308, 258);
            this.txtMCardNO.MaxLength = 18;
            this.txtMCardNO.Name = "txtMCardNO";
            this.txtMCardNO.Size = new System.Drawing.Size(132, 21);
            this.txtMCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMCardNO.TabIndex = 155;
            // 
            // txtComputerNO
            // 
            this.txtComputerNO.Enabled = false;
            this.txtComputerNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtComputerNO.InputMsg = "";
            this.txtComputerNO.IsDefaultCHInput = false;
            this.txtComputerNO.IsEnter2Tab = false;
            this.txtComputerNO.IsTagInput = false;
            this.txtComputerNO.IsTextInput = false;
            this.txtComputerNO.Location = new System.Drawing.Point(527, 258);
            this.txtComputerNO.MaxLength = 0;
            this.txtComputerNO.Name = "txtComputerNO";
            this.txtComputerNO.Size = new System.Drawing.Size(131, 21);
            this.txtComputerNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtComputerNO.TabIndex = 157;
            // 
            // mtxtBloodFee
            // 
            this.mtxtBloodFee.AllowNegative = false;
            this.mtxtBloodFee.Enabled = false;
            this.mtxtBloodFee.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mtxtBloodFee.IsAutoRemoveDecimalZero = false;
            this.mtxtBloodFee.IsEnter2Tab = false;
            this.mtxtBloodFee.Location = new System.Drawing.Point(90, 287);
            this.mtxtBloodFee.Name = "mtxtBloodFee";
            this.mtxtBloodFee.NumericPrecision = 10;
            this.mtxtBloodFee.NumericScaleOnFocus = 2;
            this.mtxtBloodFee.NumericScaleOnLostFocus = 2;
            this.mtxtBloodFee.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.mtxtBloodFee.SetRange = new System.Drawing.Size(-1, -1);
            this.mtxtBloodFee.Size = new System.Drawing.Size(142, 21);
            this.mtxtBloodFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.mtxtBloodFee.TabIndex = 158;
            this.mtxtBloodFee.TabStop = false;
            this.mtxtBloodFee.Text = "0.00";
            this.mtxtBloodFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtBloodFee.UseGroupSeperator = true;
            this.mtxtBloodFee.ZeroIsValid = true;
            // 
            // mTxtIntimes
            // 
            this.mTxtIntimes.AllowNegative = false;
            this.mTxtIntimes.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mTxtIntimes.IsAutoRemoveDecimalZero = false;
            this.mTxtIntimes.IsEnter2Tab = false;
            this.mTxtIntimes.Location = new System.Drawing.Point(308, 286);
            this.mTxtIntimes.MaxLength = 2;
            this.mTxtIntimes.Name = "mTxtIntimes";
            this.mTxtIntimes.NumericPrecision = 10;
            this.mTxtIntimes.NumericScaleOnFocus = 0;
            this.mTxtIntimes.NumericScaleOnLostFocus = 0;
            this.mTxtIntimes.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.mTxtIntimes.SetRange = new System.Drawing.Size(-1, -1);
            this.mTxtIntimes.Size = new System.Drawing.Size(132, 21);
            this.mTxtIntimes.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.mTxtIntimes.TabIndex = 159;
            this.mTxtIntimes.TabStop = false;
            this.mTxtIntimes.Text = "1";
            this.mTxtIntimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mTxtIntimes.UseGroupSeperator = true;
            this.mTxtIntimes.ZeroIsValid = true;
            // 
            // mTxtPrepay
            // 
            this.mTxtPrepay.AllowNegative = false;
            this.mTxtPrepay.Enabled = false;
            this.mTxtPrepay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mTxtPrepay.IsAutoRemoveDecimalZero = false;
            this.mTxtPrepay.IsEnter2Tab = false;
            this.mTxtPrepay.Location = new System.Drawing.Point(527, 286);
            this.mTxtPrepay.Name = "mTxtPrepay";
            this.mTxtPrepay.NumericPrecision = 10;
            this.mTxtPrepay.NumericScaleOnFocus = 2;
            this.mTxtPrepay.NumericScaleOnLostFocus = 2;
            this.mTxtPrepay.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.mTxtPrepay.SetRange = new System.Drawing.Size(-1, -1);
            this.mTxtPrepay.Size = new System.Drawing.Size(131, 21);
            this.mTxtPrepay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.mTxtPrepay.TabIndex = 161;
            this.mTxtPrepay.TabStop = false;
            this.mTxtPrepay.Text = "0.00";
            this.mTxtPrepay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mTxtPrepay.UseGroupSeperator = true;
            this.mTxtPrepay.ZeroIsValid = true;
            // 
            // cmbPayMode
            // 
            this.cmbPayMode.AccessibleDescription = "";
            this.cmbPayMode.Enabled = false;
            this.cmbPayMode.EnterVisiable = true;
            this.cmbPayMode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPayMode.InputMsg = "入院病区不能为空！";
            this.cmbPayMode.IsDefaultCHInput = false;
            this.cmbPayMode.IsFind = true;
            this.cmbPayMode.IsTagInput = true;
            this.cmbPayMode.IsTextInput = true;
            this.cmbPayMode.ListBoxHeight = 100;
            this.cmbPayMode.ListBoxWidth = 100;
            this.cmbPayMode.Location = new System.Drawing.Point(736, 286);
            this.cmbPayMode.Name = "cmbPayMode";
            this.cmbPayMode.OmitFilter = true;
            this.cmbPayMode.SelectedItem = null;
            this.cmbPayMode.SelectNone = false;
            this.cmbPayMode.ShowID = true;
            this.cmbPayMode.Size = new System.Drawing.Size(144, 21);
            this.cmbPayMode.TabIndex = 163;
            this.cmbPayMode.TabStop = false;
            this.cmbPayMode.Tag = "";
            // 
            // dtpInTime
            // 
            this.dtpInTime.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dtpInTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpInTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInTime.IsEnter2Tab = false;
            this.dtpInTime.Location = new System.Drawing.Point(90, 317);
            this.dtpInTime.Name = "dtpInTime";
            this.dtpInTime.Size = new System.Drawing.Size(144, 21);
            this.dtpInTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpInTime.TabIndex = 190;
            // 
            // txtInpatientNO
            // 
            this.txtInpatientNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInpatientNO.InputMsg = "住院号不能为空";
            this.txtInpatientNO.IsDefaultCHInput = false;
            this.txtInpatientNO.IsEnter2Tab = false;
            this.txtInpatientNO.IsTagInput = false;
            this.txtInpatientNO.IsTextInput = true;
            this.txtInpatientNO.Location = new System.Drawing.Point(421, 317);
            this.txtInpatientNO.MaxLength = 10;
            this.txtInpatientNO.Name = "txtInpatientNO";
            this.txtInpatientNO.Size = new System.Drawing.Size(132, 21);
            this.txtInpatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInpatientNO.TabIndex = 191;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.InputMsg = "";
            this.neuLabel2.IsDefaultCHInput = false;
            this.neuLabel2.IsTagInput = false;
            this.neuLabel2.IsTextInput = false;
            this.neuLabel2.Location = new System.Drawing.Point(272, 236);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(35, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 189;
            this.neuLabel2.Text = "病区:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.InputMsg = "";
            this.neuLabel1.IsDefaultCHInput = false;
            this.neuLabel1.IsTagInput = false;
            this.neuLabel1.IsTextInput = false;
            this.neuLabel1.Location = new System.Drawing.Point(465, 125);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuLabel1.TabIndex = 188;
            this.neuLabel1.Text = "家庭电话:";
            // 
            // cmbOldDept
            // 
            this.cmbOldDept.EnterVisiable = false;
            this.cmbOldDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbOldDept.InputMsg = "";
            this.cmbOldDept.IsDefaultCHInput = false;
            this.cmbOldDept.IsFind = true;
            this.cmbOldDept.IsTagInput = false;
            this.cmbOldDept.IsTextInput = false;
            this.cmbOldDept.ListBoxHeight = 100;
            this.cmbOldDept.ListBoxWidth = 100;
            this.cmbOldDept.Location = new System.Drawing.Point(736, 202);
            this.cmbOldDept.Name = "cmbOldDept";
            this.cmbOldDept.OmitFilter = true;
            this.cmbOldDept.SelectedItem = null;
            this.cmbOldDept.SelectNone = true;
            this.cmbOldDept.ShowID = true;
            this.cmbOldDept.Size = new System.Drawing.Size(144, 21);
            this.cmbOldDept.TabIndex = 148;
            this.cmbOldDept.Tag = "";
            this.cmbOldDept.Visible = false;
            // 
            // lblOldDept
            // 
            this.lblOldDept.AutoSize = true;
            this.lblOldDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOldDept.InputMsg = "";
            this.lblOldDept.IsDefaultCHInput = false;
            this.lblOldDept.IsTagInput = false;
            this.lblOldDept.IsTextInput = false;
            this.lblOldDept.Location = new System.Drawing.Point(686, 208);
            this.lblOldDept.Name = "lblOldDept";
            this.lblOldDept.Size = new System.Drawing.Size(47, 12);
            this.lblOldDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOldDept.TabIndex = 187;
            this.lblOldDept.Text = "原科室:";
            this.lblOldDept.Visible = false;
            // 
            // lbAddressNow
            // 
            this.lbAddressNow.AutoSize = true;
            this.lbAddressNow.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAddressNow.ForeColor = System.Drawing.Color.Blue;
            this.lbAddressNow.InputMsg = "";
            this.lbAddressNow.IsDefaultCHInput = false;
            this.lbAddressNow.IsTagInput = false;
            this.lbAddressNow.IsTextInput = false;
            this.lbAddressNow.Location = new System.Drawing.Point(38, 124);
            this.lbAddressNow.Name = "lbAddressNow";
            this.lbAddressNow.Size = new System.Drawing.Size(47, 12);
            this.lbAddressNow.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lbAddressNow.TabIndex = 186;
            this.lbAddressNow.Text = "现住址:";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel6.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel6.InputMsg = "";
            this.neuLabel6.IsDefaultCHInput = false;
            this.neuLabel6.IsTagInput = false;
            this.neuLabel6.IsTextInput = false;
            this.neuLabel6.Location = new System.Drawing.Point(473, 68);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(47, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuLabel6.TabIndex = 185;
            this.neuLabel6.Text = "出生地:";
            // 
            // lblBedCount
            // 
            this.lblBedCount.AutoSize = true;
            this.lblBedCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedCount.InputMsg = "";
            this.lblBedCount.IsDefaultCHInput = false;
            this.lblBedCount.IsTagInput = false;
            this.lblBedCount.IsTextInput = false;
            this.lblBedCount.Location = new System.Drawing.Point(620, 236);
            this.lblBedCount.Name = "lblBedCount";
            this.lblBedCount.Size = new System.Drawing.Size(29, 12);
            this.lblBedCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBedCount.TabIndex = 184;
            this.lblBedCount.Text = "0 张";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.ForeColor = System.Drawing.Color.Blue;
            this.lblDept.InputMsg = "";
            this.lblDept.IsDefaultCHInput = false;
            this.lblDept.IsTagInput = false;
            this.lblDept.IsTextInput = false;
            this.lblDept.Location = new System.Drawing.Point(50, 236);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(35, 12);
            this.lblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDept.TabIndex = 182;
            this.lblDept.Text = "科室:";
            // 
            // lblInTimes
            // 
            this.lblInTimes.AutoSize = true;
            this.lblInTimes.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInTimes.InputMsg = "";
            this.lblInTimes.IsDefaultCHInput = false;
            this.lblInTimes.IsTagInput = false;
            this.lblInTimes.IsTextInput = false;
            this.lblInTimes.Location = new System.Drawing.Point(247, 292);
            this.lblInTimes.Name = "lblInTimes";
            this.lblInTimes.Size = new System.Drawing.Size(59, 12);
            this.lblInTimes.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblInTimes.TabIndex = 179;
            this.lblInTimes.Text = "住院次数:";
            // 
            // lblBloodFee
            // 
            this.lblBloodFee.AutoSize = true;
            this.lblBloodFee.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBloodFee.InputMsg = "";
            this.lblBloodFee.IsDefaultCHInput = false;
            this.lblBloodFee.IsTagInput = false;
            this.lblBloodFee.IsTextInput = false;
            this.lblBloodFee.Location = new System.Drawing.Point(26, 292);
            this.lblBloodFee.Name = "lblBloodFee";
            this.lblBloodFee.Size = new System.Drawing.Size(59, 12);
            this.lblBloodFee.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblBloodFee.TabIndex = 178;
            this.lblBloodFee.Text = "血滞纳金:";
            // 
            // lblDiagnose
            // 
            this.lblDiagnose.AutoSize = true;
            this.lblDiagnose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiagnose.InputMsg = "";
            this.lblDiagnose.IsDefaultCHInput = false;
            this.lblDiagnose.IsTagInput = false;
            this.lblDiagnose.IsTextInput = false;
            this.lblDiagnose.Location = new System.Drawing.Point(461, 180);
            this.lblDiagnose.Name = "lblDiagnose";
            this.lblDiagnose.Size = new System.Drawing.Size(59, 12);
            this.lblDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblDiagnose.TabIndex = 177;
            this.lblDiagnose.Text = "门诊诊断:";
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoctor.ForeColor = System.Drawing.Color.Blue;
            this.lblDoctor.InputMsg = "";
            this.lblDoctor.IsDefaultCHInput = false;
            this.lblDoctor.IsTagInput = false;
            this.lblDoctor.IsTextInput = false;
            this.lblDoctor.Location = new System.Drawing.Point(674, 236);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(59, 12);
            this.lblDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblDoctor.TabIndex = 176;
            this.lblDoctor.Text = "收住医师:";
            // 
            // lblCircs
            // 
            this.lblCircs.AutoSize = true;
            this.lblCircs.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCircs.ForeColor = System.Drawing.Color.Blue;
            this.lblCircs.InputMsg = "";
            this.lblCircs.IsDefaultCHInput = false;
            this.lblCircs.IsTagInput = false;
            this.lblCircs.IsTextInput = false;
            this.lblCircs.Location = new System.Drawing.Point(248, 208);
            this.lblCircs.Name = "lblCircs";
            this.lblCircs.Size = new System.Drawing.Size(59, 12);
            this.lblCircs.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblCircs.TabIndex = 175;
            this.lblCircs.Text = "入院情况:";
            // 
            // lblApproach
            // 
            this.lblApproach.AutoSize = true;
            this.lblApproach.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblApproach.ForeColor = System.Drawing.Color.Blue;
            this.lblApproach.InputMsg = "";
            this.lblApproach.IsDefaultCHInput = false;
            this.lblApproach.IsTagInput = false;
            this.lblApproach.IsTextInput = false;
            this.lblApproach.Location = new System.Drawing.Point(26, 208);
            this.lblApproach.Name = "lblApproach";
            this.lblApproach.Size = new System.Drawing.Size(59, 12);
            this.lblApproach.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblApproach.TabIndex = 174;
            this.lblApproach.Text = "病人来源:";
            // 
            // lblInSource
            // 
            this.lblInSource.AutoSize = true;
            this.lblInSource.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInSource.ForeColor = System.Drawing.Color.Blue;
            this.lblInSource.InputMsg = "";
            this.lblInSource.IsDefaultCHInput = false;
            this.lblInSource.IsTagInput = false;
            this.lblInSource.IsTextInput = false;
            this.lblInSource.Location = new System.Drawing.Point(461, 208);
            this.lblInSource.Name = "lblInSource";
            this.lblInSource.Size = new System.Drawing.Size(59, 12);
            this.lblInSource.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblInSource.TabIndex = 173;
            this.lblInSource.Text = "入院途径:";
            // 
            // lblLinkPhone
            // 
            this.lblLinkPhone.AutoSize = true;
            this.lblLinkPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLinkPhone.ForeColor = System.Drawing.Color.Blue;
            this.lblLinkPhone.InputMsg = "";
            this.lblLinkPhone.IsDefaultCHInput = false;
            this.lblLinkPhone.IsTagInput = false;
            this.lblLinkPhone.IsTextInput = false;
            this.lblLinkPhone.Location = new System.Drawing.Point(236, 178);
            this.lblLinkPhone.Name = "lblLinkPhone";
            this.lblLinkPhone.Size = new System.Drawing.Size(71, 12);
            this.lblLinkPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblLinkPhone.TabIndex = 172;
            this.lblLinkPhone.Text = "联系人电话:";
            // 
            // lblLinkAddress
            // 
            this.lblLinkAddress.AutoSize = true;
            this.lblLinkAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLinkAddress.ForeColor = System.Drawing.Color.Blue;
            this.lblLinkAddress.InputMsg = "";
            this.lblLinkAddress.IsDefaultCHInput = false;
            this.lblLinkAddress.IsTagInput = false;
            this.lblLinkAddress.IsTextInput = false;
            this.lblLinkAddress.Location = new System.Drawing.Point(14, 180);
            this.lblLinkAddress.Name = "lblLinkAddress";
            this.lblLinkAddress.Size = new System.Drawing.Size(71, 12);
            this.lblLinkAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblLinkAddress.TabIndex = 171;
            this.lblLinkAddress.Text = "联系人地址:";
            // 
            // lblRelation
            // 
            this.lblRelation.AutoSize = true;
            this.lblRelation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRelation.ForeColor = System.Drawing.Color.Blue;
            this.lblRelation.InputMsg = "";
            this.lblRelation.IsDefaultCHInput = false;
            this.lblRelation.IsTagInput = false;
            this.lblRelation.IsTextInput = false;
            this.lblRelation.Location = new System.Drawing.Point(694, 151);
            this.lblRelation.Name = "lblRelation";
            this.lblRelation.Size = new System.Drawing.Size(35, 12);
            this.lblRelation.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblRelation.TabIndex = 170;
            this.lblRelation.Text = "关系:";
            // 
            // lblLinkMan
            // 
            this.lblLinkMan.AutoSize = true;
            this.lblLinkMan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLinkMan.ForeColor = System.Drawing.Color.Blue;
            this.lblLinkMan.InputMsg = "";
            this.lblLinkMan.IsDefaultCHInput = false;
            this.lblLinkMan.IsTagInput = false;
            this.lblLinkMan.IsTextInput = false;
            this.lblLinkMan.Location = new System.Drawing.Point(474, 151);
            this.lblLinkMan.Name = "lblLinkMan";
            this.lblLinkMan.Size = new System.Drawing.Size(47, 12);
            this.lblLinkMan.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblLinkMan.TabIndex = 169;
            this.lblLinkMan.Text = "联系人:";
            // 
            // lblHomeAddress
            // 
            this.lblHomeAddress.AutoSize = true;
            this.lblHomeAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHomeAddress.ForeColor = System.Drawing.Color.Blue;
            this.lblHomeAddress.InputMsg = "";
            this.lblHomeAddress.IsDefaultCHInput = false;
            this.lblHomeAddress.IsTagInput = false;
            this.lblHomeAddress.IsTextInput = false;
            this.lblHomeAddress.Location = new System.Drawing.Point(24, 152);
            this.lblHomeAddress.Name = "lblHomeAddress";
            this.lblHomeAddress.Size = new System.Drawing.Size(59, 12);
            this.lblHomeAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblHomeAddress.TabIndex = 168;
            this.lblHomeAddress.Text = "户口地址:";
            // 
            // lblWorkPhone
            // 
            this.lblWorkPhone.AutoSize = true;
            this.lblWorkPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWorkPhone.ForeColor = System.Drawing.Color.Blue;
            this.lblWorkPhone.InputMsg = "";
            this.lblWorkPhone.IsDefaultCHInput = false;
            this.lblWorkPhone.IsTagInput = false;
            this.lblWorkPhone.IsTextInput = false;
            this.lblWorkPhone.Location = new System.Drawing.Point(461, 96);
            this.lblWorkPhone.Name = "lblWorkPhone";
            this.lblWorkPhone.Size = new System.Drawing.Size(59, 12);
            this.lblWorkPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblWorkPhone.TabIndex = 165;
            this.lblWorkPhone.Text = "单位电话:";
            // 
            // lblProfession
            // 
            this.lblProfession.AutoSize = true;
            this.lblProfession.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProfession.ForeColor = System.Drawing.Color.Blue;
            this.lblProfession.InputMsg = "";
            this.lblProfession.IsDefaultCHInput = false;
            this.lblProfession.IsTagInput = false;
            this.lblProfession.IsTextInput = false;
            this.lblProfession.Location = new System.Drawing.Point(271, 68);
            this.lblProfession.Name = "lblProfession";
            this.lblProfession.Size = new System.Drawing.Size(35, 12);
            this.lblProfession.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblProfession.TabIndex = 162;
            this.lblProfession.Text = "职业:";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCountry.ForeColor = System.Drawing.Color.Blue;
            this.lblCountry.InputMsg = "";
            this.lblCountry.IsDefaultCHInput = false;
            this.lblCountry.IsTagInput = false;
            this.lblCountry.IsTextInput = false;
            this.lblCountry.Location = new System.Drawing.Point(698, 40);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(35, 12);
            this.lblCountry.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblCountry.TabIndex = 160;
            this.lblCountry.Text = "国籍:";
            // 
            // lblMarry
            // 
            this.lblMarry.AutoSize = true;
            this.lblMarry.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMarry.ForeColor = System.Drawing.Color.Blue;
            this.lblMarry.InputMsg = "";
            this.lblMarry.IsDefaultCHInput = false;
            this.lblMarry.IsTagInput = false;
            this.lblMarry.IsTextInput = false;
            this.lblMarry.Location = new System.Drawing.Point(26, 68);
            this.lblMarry.Name = "lblMarry";
            this.lblMarry.Size = new System.Drawing.Size(59, 12);
            this.lblMarry.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblMarry.TabIndex = 154;
            this.lblMarry.Text = "婚姻状况:";
            // 
            // lblWorkAddress
            // 
            this.lblWorkAddress.AutoSize = true;
            this.lblWorkAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWorkAddress.ForeColor = System.Drawing.Color.Blue;
            this.lblWorkAddress.InputMsg = "";
            this.lblWorkAddress.IsDefaultCHInput = false;
            this.lblWorkAddress.IsTagInput = false;
            this.lblWorkAddress.IsTextInput = false;
            this.lblWorkAddress.Location = new System.Drawing.Point(14, 96);
            this.lblWorkAddress.Name = "lblWorkAddress";
            this.lblWorkAddress.Size = new System.Drawing.Size(71, 12);
            this.lblWorkAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblWorkAddress.TabIndex = 150;
            this.lblWorkAddress.Text = "单位及地址:";
            // 
            // lblNation
            // 
            this.lblNation.AutoSize = true;
            this.lblNation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNation.ForeColor = System.Drawing.Color.Blue;
            this.lblNation.InputMsg = "";
            this.lblNation.IsDefaultCHInput = false;
            this.lblNation.IsTagInput = false;
            this.lblNation.IsTextInput = false;
            this.lblNation.Location = new System.Drawing.Point(698, 68);
            this.lblNation.Name = "lblNation";
            this.lblNation.Size = new System.Drawing.Size(35, 12);
            this.lblNation.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblNation.TabIndex = 138;
            this.lblNation.Text = "民族:";
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPact.ForeColor = System.Drawing.Color.Blue;
            this.lblPact.InputMsg = "";
            this.lblPact.IsDefaultCHInput = false;
            this.lblPact.IsTagInput = false;
            this.lblPact.IsTextInput = false;
            this.lblPact.Location = new System.Drawing.Point(26, 264);
            this.lblPact.Name = "lblPact";
            this.lblPact.Size = new System.Drawing.Size(59, 12);
            this.lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblPact.TabIndex = 129;
            this.lblPact.Text = "结算方式:";
            // 
            // lblInTime
            // 
            this.lblInTime.AutoSize = true;
            this.lblInTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInTime.ForeColor = System.Drawing.Color.Blue;
            this.lblInTime.InputMsg = "";
            this.lblInTime.IsDefaultCHInput = false;
            this.lblInTime.IsTagInput = false;
            this.lblInTime.IsTextInput = false;
            this.lblInTime.Location = new System.Drawing.Point(24, 320);
            this.lblInTime.Name = "lblInTime";
            this.lblInTime.Size = new System.Drawing.Size(59, 12);
            this.lblInTime.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblInTime.TabIndex = 125;
            this.lblInTime.Text = "入院日期:";
            // 
            // lblMCardNO
            // 
            this.lblMCardNO.AutoSize = true;
            this.lblMCardNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMCardNO.InputMsg = "";
            this.lblMCardNO.IsDefaultCHInput = false;
            this.lblMCardNO.IsTagInput = false;
            this.lblMCardNO.IsTextInput = false;
            this.lblMCardNO.Location = new System.Drawing.Point(248, 264);
            this.lblMCardNO.Name = "lblMCardNO";
            this.lblMCardNO.Size = new System.Drawing.Size(59, 12);
            this.lblMCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblMCardNO.TabIndex = 123;
            this.lblMCardNO.Text = "医疗证号:";
            // 
            // rdoInpatientNO
            // 
            this.rdoInpatientNO.AutoSize = true;
            this.rdoInpatientNO.Checked = true;
            this.rdoInpatientNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoInpatientNO.ForeColor = System.Drawing.Color.Blue;
            this.rdoInpatientNO.Location = new System.Drawing.Point(243, 320);
            this.rdoInpatientNO.Name = "rdoInpatientNO";
            this.rdoInpatientNO.Size = new System.Drawing.Size(83, 16);
            this.rdoInpatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rdoInpatientNO.TabIndex = 183;
            this.rdoInpatientNO.TabStop = true;
            this.rdoInpatientNO.Text = "住院号(F1)";
            this.rdoInpatientNO.UseVisualStyleBackColor = false;
            // 
            // lblPrepay
            // 
            this.lblPrepay.AutoSize = true;
            this.lblPrepay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrepay.InputMsg = "";
            this.lblPrepay.IsDefaultCHInput = false;
            this.lblPrepay.IsTagInput = false;
            this.lblPrepay.IsTextInput = false;
            this.lblPrepay.Location = new System.Drawing.Point(461, 292);
            this.lblPrepay.Name = "lblPrepay";
            this.lblPrepay.Size = new System.Drawing.Size(59, 12);
            this.lblPrepay.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblPrepay.TabIndex = 180;
            this.lblPrepay.Text = "预交金额:";
            // 
            // lblPayMode
            // 
            this.lblPayMode.AutoSize = true;
            this.lblPayMode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPayMode.InputMsg = "";
            this.lblPayMode.IsDefaultCHInput = false;
            this.lblPayMode.IsTagInput = false;
            this.lblPayMode.IsTextInput = false;
            this.lblPayMode.Location = new System.Drawing.Point(674, 292);
            this.lblPayMode.Name = "lblPayMode";
            this.lblPayMode.Size = new System.Drawing.Size(59, 12);
            this.lblPayMode.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblPayMode.TabIndex = 181;
            this.lblPayMode.Text = "支付方式:";
            // 
            // ucModifyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.Controls.Add(this.plInfomation);
            this.Name = "ucModifyInfo";
            this.Size = new System.Drawing.Size(907, 351);
            this.plInfomation.ResumeLayout(false);
            this.plInfomation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuPanel plInfomation;
        private NeuInputLabel lblDoctDept;
        private NeuInputListTextBox cmbSex;
        private NeuInputDateTime dtpBirthDay;
        protected FS.FrameWork.WinForms.Controls.NeuCheckBox chbencrypt;
        protected NeuInputTextBox txtIDNO;
        protected NeuInputLabel lblIDNO;
        protected NeuInputTextBox txtAge;
        protected NeuInputLabel lblBirthday;
        protected NeuInputLabel lblSex;
        protected NeuInputLabel lblName;
        protected NeuInputTextBox txtClinicNO;
        protected FS.FrameWork.WinForms.Controls.NeuContextMenuStrip neuContextMenuStrip1;
        private NeuInputListTextBox cmbPayMode;
        private NeuInputListTextBox cmbNurseCell;
        protected NeuInputLabel neuLabel2;
        protected NeuInputTextBox txtHomePhone;
        protected NeuInputLabel neuLabel1;
        private NeuInputListTextBox cmbOldDept;
        protected NeuInputLabel lblOldDept;
        private NeuInputTextBox txtAddressNow;
        protected NeuInputLabel lbAddressNow;
        private NeuInputTextBox txtBirthArea;
        protected NeuInputLabel neuLabel6;
        private NeuInputListTextBox cmbBirthArea;
        private NeuInputListTextBox cmbBedNO;
        private NeuInputListTextBox cmbDoctor;
        private NeuInputListTextBox cmbCircs;
        private NeuInputListTextBox cmbRelation;
        private NeuInputListTextBox cmbInSource;
        private NeuInputListTextBox cmbMarry;
        private NeuInputListTextBox cmbApproach;
        private NeuInputListTextBox cmbNation;
        private NeuInputListTextBox cmbDept;
        private NeuInputListTextBox cmbProfession;
        private NeuInputListTextBox cmbCountry;
        private NeuInputListTextBox cmbPact;
        private NeuInputLabel lblBedCount;
        private NeuInputTextBox txtWorkAddress;
        private NeuInputTextBox txtHomeAddress;
        private NeuInputTextBox txtLinkAddr;
        protected NeuInputLabel lblDept;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox mTxtIntimes;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox mtxtBloodFee;
        protected NeuInputLabel lblInTimes;
        protected NeuInputLabel lblBloodFee;
        protected NeuInputTextBox txtDiagnose;
        protected NeuInputLabel lblDiagnose;
        protected NeuInputLabel lblDoctor;
        protected NeuInputLabel lblCircs;
        protected NeuInputLabel lblApproach;
        protected NeuInputLabel lblInSource;
        protected NeuInputTextBox txtLinkPhone;
        protected NeuInputLabel lblLinkPhone;
        protected NeuInputLabel lblLinkAddress;
        protected NeuInputLabel lblRelation;
        protected NeuInputTextBox txtLinkMan;
        protected NeuInputLabel lblLinkMan;
        protected NeuInputLabel lblHomeAddress;
        protected NeuInputTextBox txtWorkPhone;
        protected NeuInputLabel lblWorkPhone;
        protected NeuInputLabel lblProfession;
        protected NeuInputLabel lblCountry;
        protected NeuInputLabel lblMarry;
        protected NeuInputLabel lblWorkAddress;
        protected NeuInputLabel lblNation;
        protected NeuInputTextBox txtComputerNO;
        protected NeuInputLabel lblPact;
        protected NeuInputLabel lblInTime;
        protected NeuInputTextBox txtMCardNO;
        protected NeuInputLabel lblMCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuRadioButton rdoInpatientNO;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox mTxtPrepay;
        protected NeuInputLabel lblPrepay;
        protected NeuInputLabel lblPayMode;
        protected FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpInTime;
        protected NeuInputTextBox txtInpatientNO;
        protected NeuInputLabel neuInputLabel1;
        protected NeuInputTextBox txtName;
        protected NeuInputTextBox txtHomeZip;
        protected NeuInputLabel neuInputLabel2;
        protected FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnTempPatientNO;
        private NeuInputLabel lblBirthArea;
        private NeuInputLabel lblBedNO;
        private NeuInputLabel lblComputerNO;
        protected NeuInputTextBox txtHomeAddressZip;
        protected NeuInputLabel neuInputLabel4;
        protected NeuInputTextBox txtWorkZip;
        protected NeuInputLabel neuInputLabel3;


    }
}
