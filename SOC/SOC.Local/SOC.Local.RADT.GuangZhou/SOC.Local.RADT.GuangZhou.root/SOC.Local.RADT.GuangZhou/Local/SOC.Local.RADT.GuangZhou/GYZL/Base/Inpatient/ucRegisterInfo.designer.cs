using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Base;
using System.Xml;
using Neusoft.FrameWork.Function;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;
using Neusoft.HISFC.Models.RADT;
using Neusoft.SOC.HISFC.RADT.Interface;
using Neusoft.SOC.HISFC.RADT.Components.Common;
using Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Common;

namespace Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Base.Inpatient
{
    /// <summary>
    /// 入院登记界面
    /// </summary>
    public partial class ucRegisterInfo 
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
            Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel lblBirthArea;
            Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel lblBedNO;
            Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel lblComputerNO;
            this.plInfomation = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.neuContextMenuStrip1 = new Neusoft.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.txtHomeZip = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbBirthArea = new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Common.NeuCombox();
            this.cmbPact = new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Common.NeuCombox();
            this.cmbProfession = new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Common.NeuCombox();
            this.cmbMarry = new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Common.NeuCombox();
            this.cmbCountry = new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Common.NeuCombox();
            this.cmbPayType = new Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Common.cmbTransType();
            this.txtRealInvoiceNO = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbRealInvoiceNO = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.btnUpdateRealInvoiceNO = new Neusoft.FrameWork.WinForms.Controls.NeuButton();
            this.cmbOverLop = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblOverLop = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.cmbBedOverDeal = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblBedOverDeal = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.vtbRate = new Neusoft.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.lblRate = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.rbtnTempPatientNO = new Neusoft.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuInputLabel2 = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtName = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.neuInputLabel1 = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtClinicNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.lblName = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.chbencrypt = new Neusoft.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbSex = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.dtpBirthDay = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputDateTime();
            this.txtAge = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtIDNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.lblDoctDept = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblIDNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBirthday = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblSex = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtBirthArea = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.cmbNation = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.txtWorkAddress = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtWorkPhone = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtHomePhone = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtAddressNow = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtHomeAddress = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtLinkMan = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.cmbRelation = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.txtLinkPhone = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtLinkAddr = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtDiagnose = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.cmbInSource = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbCircs = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbApproach = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbDept = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbNurseCell = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbBedNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbDoctor = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.txtMCardNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtComputerNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.mtxtBloodFee = new Neusoft.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.mTxtIntimes = new Neusoft.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.mTxtPrepay = new Neusoft.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.dtpInTime = new Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtInpatientNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.neuLabel2 = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.neuLabel1 = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.cmbOldDept = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.lblOldDept = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lbAddressNow = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.neuLabel6 = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBedCount = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDept = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblInTimes = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBloodFee = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDiagnose = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDoctor = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblCircs = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblApproach = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblInSource = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblLinkPhone = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblLinkAddress = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblRelation = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblLinkMan = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblHomeAddress = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblWorkPhone = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblProfession = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblCountry = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblMarry = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblWorkAddress = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblNation = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblAirLimit = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBedLimit = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDayLimit = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtAirLimit = new Neusoft.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtBedLimit = new Neusoft.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtDayLimit = new Neusoft.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.lblPact = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblInTime = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblMCardNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.rdoInpatientNO = new Neusoft.FrameWork.WinForms.Controls.NeuRadioButton();
            this.lblPrepay = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblPayMode = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            lblBirthArea = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            lblBedNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            lblComputerNO = new Neusoft.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.plInfomation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBirthArea
            // 
            lblBirthArea.AutoSize = true;
            lblBirthArea.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lblBirthArea.InputMsg = "";
            lblBirthArea.IsDefaultCHInput = false;
            lblBirthArea.IsTagInput = false;
            lblBirthArea.IsTextInput = false;
            lblBirthArea.Location = new System.Drawing.Point(485, 40);
            lblBirthArea.Name = "lblBirthArea";
            lblBirthArea.Size = new System.Drawing.Size(35, 12);
            lblBirthArea.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            lblBirthArea.TabIndex = 156;
            lblBirthArea.Text = "籍贯:";
            // 
            // lblBedNO
            // 
            lblBedNO.AutoSize = true;
            lblBedNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lblBedNO.InputMsg = "";
            lblBedNO.IsDefaultCHInput = false;
            lblBedNO.IsTagInput = false;
            lblBedNO.IsTextInput = false;
            lblBedNO.Location = new System.Drawing.Point(473, 236);
            lblBedNO.Name = "lblBedNO";
            lblBedNO.Size = new System.Drawing.Size(47, 12);
            lblBedNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            lblBedNO.TabIndex = 143;
            lblBedNO.Text = "病床号:";
            // 
            // lblComputerNO
            // 
            lblComputerNO.AutoSize = true;
            lblComputerNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lblComputerNO.InputMsg = "";
            lblComputerNO.IsDefaultCHInput = false;
            lblComputerNO.IsTagInput = false;
            lblComputerNO.IsTextInput = false;
            lblComputerNO.Location = new System.Drawing.Point(461, 264);
            lblComputerNO.Name = "lblComputerNO";
            lblComputerNO.Size = new System.Drawing.Size(59, 12);
            lblComputerNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            lblComputerNO.TabIndex = 131;
            lblComputerNO.Text = "电 脑 号:";
            // 
            // plInfomation
            // 
            this.plInfomation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plInfomation.ContextMenuStrip = this.neuContextMenuStrip1;
            this.plInfomation.Controls.Add(this.txtHomeZip);
            this.plInfomation.Controls.Add(this.cmbBirthArea);
            this.plInfomation.Controls.Add(this.cmbPact);
            this.plInfomation.Controls.Add(this.cmbProfession);
            this.plInfomation.Controls.Add(this.cmbMarry);
            this.plInfomation.Controls.Add(this.cmbCountry);
            this.plInfomation.Controls.Add(this.cmbPayType);
            this.plInfomation.Controls.Add(this.txtRealInvoiceNO);
            this.plInfomation.Controls.Add(this.lbRealInvoiceNO);
            this.plInfomation.Controls.Add(this.btnUpdateRealInvoiceNO);
            this.plInfomation.Controls.Add(this.cmbOverLop);
            this.plInfomation.Controls.Add(this.lblOverLop);
            this.plInfomation.Controls.Add(this.cmbBedOverDeal);
            this.plInfomation.Controls.Add(this.lblBedOverDeal);
            this.plInfomation.Controls.Add(this.vtbRate);
            this.plInfomation.Controls.Add(this.lblRate);
            this.plInfomation.Controls.Add(this.rbtnTempPatientNO);
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
            this.plInfomation.Controls.Add(this.txtMCardNO);
            this.plInfomation.Controls.Add(this.txtComputerNO);
            this.plInfomation.Controls.Add(this.mtxtBloodFee);
            this.plInfomation.Controls.Add(this.mTxtIntimes);
            this.plInfomation.Controls.Add(this.mTxtPrepay);
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
            this.plInfomation.Controls.Add(lblBirthArea);
            this.plInfomation.Controls.Add(this.lblMarry);
            this.plInfomation.Controls.Add(this.lblWorkAddress);
            this.plInfomation.Controls.Add(lblBedNO);
            this.plInfomation.Controls.Add(this.lblNation);
            this.plInfomation.Controls.Add(lblComputerNO);
            this.plInfomation.Controls.Add(this.lblAirLimit);
            this.plInfomation.Controls.Add(this.lblBedLimit);
            this.plInfomation.Controls.Add(this.lblDayLimit);
            this.plInfomation.Controls.Add(this.txtAirLimit);
            this.plInfomation.Controls.Add(this.txtBedLimit);
            this.plInfomation.Controls.Add(this.txtDayLimit);
            this.plInfomation.Controls.Add(this.lblPact);
            this.plInfomation.Controls.Add(this.lblInTime);
            this.plInfomation.Controls.Add(this.lblMCardNO);
            this.plInfomation.Controls.Add(this.rdoInpatientNO);
            this.plInfomation.Controls.Add(this.lblPrepay);
            this.plInfomation.Controls.Add(this.lblPayMode);
            this.plInfomation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plInfomation.Location = new System.Drawing.Point(3, 3);
            this.plInfomation.Name = "plInfomation";
            this.plInfomation.Size = new System.Drawing.Size(901, 409);
            this.plInfomation.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plInfomation.TabIndex = 0;
            // 
            // neuContextMenuStrip1
            // 
            this.neuContextMenuStrip1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuContextMenuStrip1.Name = "neuContextMenuStrip1";
            this.neuContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.neuContextMenuStrip1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // txtHomeZip
            // 
            this.txtHomeZip.EnterVisiable = true;
            this.txtHomeZip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHomeZip.InputMsg = "邮编不能为空！";
            this.txtHomeZip.IsDefaultCHInput = false;
            this.txtHomeZip.IsFind = true;
            this.txtHomeZip.IsTagInput = true;
            this.txtHomeZip.IsTextInput = false;
            this.txtHomeZip.ListBoxHeight = 100;
            this.txtHomeZip.ListBoxWidth = 100;
            this.txtHomeZip.Location = new System.Drawing.Point(90, 147);
            this.txtHomeZip.Name = "txtHomeZip";
            this.txtHomeZip.OmitFilter = true;
            this.txtHomeZip.SelectedItem = null;
            this.txtHomeZip.SelectNone = false;
            this.txtHomeZip.ShowID = true;
            this.txtHomeZip.Size = new System.Drawing.Size(144, 21);
            this.txtHomeZip.TabIndex = 18;
            this.txtHomeZip.Tag = "";
            // 
            // cmbBirthArea
            // 
            this.cmbBirthArea.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbBirthArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbBirthArea.FormattingEnabled = true;
            this.cmbBirthArea.InputMsg = "";
            this.cmbBirthArea.IsDefaultCHInput = false;
            this.cmbBirthArea.IsEnter2Tab = false;
            this.cmbBirthArea.IsFind = false;
            this.cmbBirthArea.IsFlat = false;
            this.cmbBirthArea.IsLike = true;
            this.cmbBirthArea.IsListOnly = false;
            this.cmbBirthArea.IsPopForm = true;
            this.cmbBirthArea.IsShowCustomerList = false;
            this.cmbBirthArea.IsShowID = false;
            this.cmbBirthArea.IsTagInput = false;
            this.cmbBirthArea.IsTextInput = false;
            this.cmbBirthArea.Location = new System.Drawing.Point(527, 34);
            this.cmbBirthArea.Name = "cmbBirthArea";
            this.cmbBirthArea.ShowCustomerList = false;
            this.cmbBirthArea.ShowID = false;
            this.cmbBirthArea.Size = new System.Drawing.Size(133, 20);
            this.cmbBirthArea.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbBirthArea.TabIndex = 5;
            this.cmbBirthArea.Tag = "";
            this.cmbBirthArea.ToolBarUse = false;
            // 
            // cmbPact
            // 
            this.cmbPact.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPact.FormattingEnabled = true;
            this.cmbPact.InputMsg = "";
            this.cmbPact.IsDefaultCHInput = false;
            this.cmbPact.IsEnter2Tab = false;
            this.cmbPact.IsFind = false;
            this.cmbPact.IsFlat = false;
            this.cmbPact.IsLike = true;
            this.cmbPact.IsListOnly = false;
            this.cmbPact.IsPopForm = true;
            this.cmbPact.IsShowCustomerList = false;
            this.cmbPact.IsShowID = false;
            this.cmbPact.IsTagInput = false;
            this.cmbPact.IsTextInput = false;
            this.cmbPact.Location = new System.Drawing.Point(90, 259);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(144, 20);
            this.cmbPact.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPact.TabIndex = 32;
            this.cmbPact.Tag = "";
            this.cmbPact.ToolBarUse = false;
            this.cmbPact.SelectedIndexChanged += new System.EventHandler(this.cmbPact_SelectedIndexChanged);
            // 
            // cmbProfession
            // 
            this.cmbProfession.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbProfession.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbProfession.FormattingEnabled = true;
            this.cmbProfession.InputMsg = "";
            this.cmbProfession.IsDefaultCHInput = false;
            this.cmbProfession.IsEnter2Tab = false;
            this.cmbProfession.IsFind = false;
            this.cmbProfession.IsFlat = false;
            this.cmbProfession.IsLike = true;
            this.cmbProfession.IsListOnly = false;
            this.cmbProfession.IsPopForm = true;
            this.cmbProfession.IsShowCustomerList = false;
            this.cmbProfession.IsShowID = false;
            this.cmbProfession.IsTagInput = false;
            this.cmbProfession.IsTextInput = false;
            this.cmbProfession.Location = new System.Drawing.Point(308, 65);
            this.cmbProfession.Name = "cmbProfession";
            this.cmbProfession.ShowCustomerList = false;
            this.cmbProfession.ShowID = false;
            this.cmbProfession.Size = new System.Drawing.Size(132, 20);
            this.cmbProfession.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbProfession.TabIndex = 10;
            this.cmbProfession.Tag = "";
            this.cmbProfession.ToolBarUse = false;
            // 
            // cmbMarry
            // 
            this.cmbMarry.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbMarry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMarry.FormattingEnabled = true;
            this.cmbMarry.InputMsg = "请填写婚姻状况";
            this.cmbMarry.IsDefaultCHInput = false;
            this.cmbMarry.IsEnter2Tab = false;
            this.cmbMarry.IsFind = false;
            this.cmbMarry.IsFlat = false;
            this.cmbMarry.IsLike = true;
            this.cmbMarry.IsListOnly = false;
            this.cmbMarry.IsPopForm = true;
            this.cmbMarry.IsShowCustomerList = false;
            this.cmbMarry.IsShowID = false;
            this.cmbMarry.IsTagInput = false;
            this.cmbMarry.IsTextInput = true;
            this.cmbMarry.Location = new System.Drawing.Point(91, 65);
            this.cmbMarry.Name = "cmbMarry";
            this.cmbMarry.ShowCustomerList = false;
            this.cmbMarry.ShowID = false;
            this.cmbMarry.Size = new System.Drawing.Size(141, 20);
            this.cmbMarry.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbMarry.TabIndex = 8;
            this.cmbMarry.Tag = "";
            this.cmbMarry.ToolBarUse = false;
            // 
            // cmbCountry
            // 
            this.cmbCountry.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbCountry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.InputMsg = "";
            this.cmbCountry.IsDefaultCHInput = false;
            this.cmbCountry.IsEnter2Tab = false;
            this.cmbCountry.IsFind = false;
            this.cmbCountry.IsFlat = false;
            this.cmbCountry.IsLike = true;
            this.cmbCountry.IsListOnly = false;
            this.cmbCountry.IsPopForm = true;
            this.cmbCountry.IsShowCustomerList = false;
            this.cmbCountry.IsShowID = false;
            this.cmbCountry.IsTagInput = false;
            this.cmbCountry.IsTextInput = false;
            this.cmbCountry.Location = new System.Drawing.Point(736, 34);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.ShowCustomerList = false;
            this.cmbCountry.ShowID = false;
            this.cmbCountry.Size = new System.Drawing.Size(144, 20);
            this.cmbCountry.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCountry.TabIndex = 6;
            this.cmbCountry.Tag = "";
            this.cmbCountry.ToolBarUse = false;
            // 
            // cmbPayType
            // 
            this.cmbPayType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPayType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPayType.FormattingEnabled = true;
            this.cmbPayType.IsEnter2Tab = false;
            this.cmbPayType.IsFlat = false;
            this.cmbPayType.IsLike = true;
            this.cmbPayType.IsListOnly = false;
            this.cmbPayType.IsPopForm = true;
            this.cmbPayType.IsShowCustomerList = false;
            this.cmbPayType.IsShowID = false;
            this.cmbPayType.Location = new System.Drawing.Point(736, 286);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Pop = false;
            this.cmbPayType.ShowCustomerList = false;
            this.cmbPayType.ShowID = false;
            this.cmbPayType.Size = new System.Drawing.Size(121, 20);
            this.cmbPayType.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPayType.TabIndex = 38;
            this.cmbPayType.Tag = "";
            this.cmbPayType.ToolBarUse = false;
            this.cmbPayType.WorkUnit = "";
            // 
            // txtRealInvoiceNO
            // 
            this.txtRealInvoiceNO.IsEnter2Tab = false;
            this.txtRealInvoiceNO.Location = new System.Drawing.Point(680, 376);
            this.txtRealInvoiceNO.Name = "txtRealInvoiceNO";
            this.txtRealInvoiceNO.Size = new System.Drawing.Size(131, 21);
            this.txtRealInvoiceNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRealInvoiceNO.TabIndex = 49;
            // 
            // lbRealInvoiceNO
            // 
            this.lbRealInvoiceNO.AutoSize = true;
            this.lbRealInvoiceNO.Location = new System.Drawing.Point(590, 379);
            this.lbRealInvoiceNO.Name = "lbRealInvoiceNO";
            this.lbRealInvoiceNO.Size = new System.Drawing.Size(83, 12);
            this.lbRealInvoiceNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRealInvoiceNO.TabIndex = 201;
            this.lbRealInvoiceNO.Text = "预交金印刷号:";
            // 
            // btnUpdateRealInvoiceNO
            // 
            this.btnUpdateRealInvoiceNO.Location = new System.Drawing.Point(817, 376);
            this.btnUpdateRealInvoiceNO.Name = "btnUpdateRealInvoiceNO";
            this.btnUpdateRealInvoiceNO.Size = new System.Drawing.Size(54, 23);
            this.btnUpdateRealInvoiceNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnUpdateRealInvoiceNO.TabIndex = 200;
            this.btnUpdateRealInvoiceNO.Text = "更新";
            this.btnUpdateRealInvoiceNO.Type = Neusoft.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnUpdateRealInvoiceNO.UseVisualStyleBackColor = true;
            this.btnUpdateRealInvoiceNO.Click += new System.EventHandler(this.btnUpdateRealInvoiceNO_Click);
            // 
            // cmbOverLop
            // 
            this.cmbOverLop.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOverLop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbOverLop.Enabled = false;
            this.cmbOverLop.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbOverLop.IsEnter2Tab = false;
            this.cmbOverLop.IsFlat = false;
            this.cmbOverLop.IsLike = true;
            this.cmbOverLop.IsListOnly = false;
            this.cmbOverLop.IsPopForm = true;
            this.cmbOverLop.IsShowCustomerList = false;
            this.cmbOverLop.IsShowID = false;
            this.cmbOverLop.Location = new System.Drawing.Point(308, 346);
            this.cmbOverLop.Name = "cmbOverLop";
            this.cmbOverLop.ShowCustomerList = false;
            this.cmbOverLop.ShowID = false;
            this.cmbOverLop.Size = new System.Drawing.Size(132, 20);
            this.cmbOverLop.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbOverLop.TabIndex = 44;
            this.cmbOverLop.TabStop = false;
            this.cmbOverLop.Tag = "";
            this.cmbOverLop.ToolBarUse = false;
            this.cmbOverLop.Leave += new System.EventHandler(this.cmbOverLop_Leave);
            this.cmbOverLop.Enter += new System.EventHandler(this.cmbOverLop_Enter);
            // 
            // lblOverLop
            // 
            this.lblOverLop.AutoSize = true;
            this.lblOverLop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOverLop.ForeColor = System.Drawing.Color.Blue;
            this.lblOverLop.InputMsg = "";
            this.lblOverLop.IsDefaultCHInput = false;
            this.lblOverLop.IsTagInput = false;
            this.lblOverLop.IsTextInput = false;
            this.lblOverLop.Location = new System.Drawing.Point(247, 349);
            this.lblOverLop.Name = "lblOverLop";
            this.lblOverLop.Size = new System.Drawing.Size(59, 12);
            this.lblOverLop.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblOverLop.TabIndex = 199;
            this.lblOverLop.Text = "日限处理:";
            // 
            // cmbBedOverDeal
            // 
            this.cmbBedOverDeal.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbBedOverDeal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbBedOverDeal.Enabled = false;
            this.cmbBedOverDeal.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbBedOverDeal.IsEnter2Tab = false;
            this.cmbBedOverDeal.IsFlat = false;
            this.cmbBedOverDeal.IsLike = true;
            this.cmbBedOverDeal.IsListOnly = false;
            this.cmbBedOverDeal.IsPopForm = true;
            this.cmbBedOverDeal.IsShowCustomerList = false;
            this.cmbBedOverDeal.IsShowID = false;
            this.cmbBedOverDeal.Location = new System.Drawing.Point(90, 346);
            this.cmbBedOverDeal.Name = "cmbBedOverDeal";
            this.cmbBedOverDeal.ShowCustomerList = false;
            this.cmbBedOverDeal.ShowID = false;
            this.cmbBedOverDeal.Size = new System.Drawing.Size(144, 20);
            this.cmbBedOverDeal.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbBedOverDeal.TabIndex = 43;
            this.cmbBedOverDeal.TabStop = false;
            this.cmbBedOverDeal.Tag = "";
            this.cmbBedOverDeal.ToolBarUse = false;
            this.cmbBedOverDeal.Leave += new System.EventHandler(this.cmbBedOverDeal_Leave);
            this.cmbBedOverDeal.Enter += new System.EventHandler(this.cmbBedOverDeal_Enter);
            // 
            // lblBedOverDeal
            // 
            this.lblBedOverDeal.AutoSize = true;
            this.lblBedOverDeal.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedOverDeal.ForeColor = System.Drawing.Color.Blue;
            this.lblBedOverDeal.InputMsg = "";
            this.lblBedOverDeal.IsDefaultCHInput = false;
            this.lblBedOverDeal.IsTagInput = false;
            this.lblBedOverDeal.IsTextInput = false;
            this.lblBedOverDeal.Location = new System.Drawing.Point(26, 351);
            this.lblBedOverDeal.Name = "lblBedOverDeal";
            this.lblBedOverDeal.Size = new System.Drawing.Size(59, 12);
            this.lblBedOverDeal.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblBedOverDeal.TabIndex = 198;
            this.lblBedOverDeal.Text = "超标处理:";
            // 
            // vtbRate
            // 
            this.vtbRate.AllowNegative = false;
            this.vtbRate.AutoPadRightZero = true;
            this.vtbRate.Enabled = false;
            this.vtbRate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.vtbRate.Location = new System.Drawing.Point(736, 317);
            this.vtbRate.MaxDigits = 2;
            this.vtbRate.MaxLength = 8;
            this.vtbRate.Name = "vtbRate";
            this.vtbRate.Size = new System.Drawing.Size(47, 21);
            this.vtbRate.TabIndex = 42;
            this.vtbRate.Text = "0.00";
            this.vtbRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.vtbRate.WillShowError = false;
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Location = new System.Drawing.Point(674, 321);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(59, 12);
            this.lblRate.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblRate.TabIndex = 197;
            this.lblRate.Text = "自负比例:";
            // 
            // rbtnTempPatientNO
            // 
            this.rbtnTempPatientNO.AutoSize = true;
            this.rbtnTempPatientNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnTempPatientNO.ForeColor = System.Drawing.Color.Blue;
            this.rbtnTempPatientNO.Location = new System.Drawing.Point(332, 379);
            this.rbtnTempPatientNO.Name = "rbtnTempPatientNO";
            this.rbtnTempPatientNO.Size = new System.Drawing.Size(83, 16);
            this.rbtnTempPatientNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnTempPatientNO.TabIndex = 47;
            this.rbtnTempPatientNO.Text = "临时号(F2)";
            this.rbtnTempPatientNO.UseVisualStyleBackColor = false;
            // 
            // neuInputLabel2
            // 
            this.neuInputLabel2.AutoSize = true;
            this.neuInputLabel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuInputLabel2.InputMsg = "";
            this.neuInputLabel2.IsDefaultCHInput = false;
            this.neuInputLabel2.IsTagInput = false;
            this.neuInputLabel2.IsTextInput = false;
            this.neuInputLabel2.Location = new System.Drawing.Point(48, 152);
            this.neuInputLabel2.Name = "neuInputLabel2";
            this.neuInputLabel2.Size = new System.Drawing.Size(35, 12);
            this.neuInputLabel2.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuInputLabel2.TabIndex = 195;
            this.neuInputLabel2.Text = "邮编:";
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
            this.txtName.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.neuInputLabel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuInputLabel1.TabIndex = 193;
            this.neuInputLabel1.Text = "门诊号:";
            // 
            // txtClinicNO
            // 
            this.txtClinicNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtClinicNO.InputMsg = "门诊号不能为空";
            this.txtClinicNO.IsDefaultCHInput = false;
            this.txtClinicNO.IsEnter2Tab = false;
            this.txtClinicNO.IsTagInput = false;
            this.txtClinicNO.IsTextInput = true;
            this.txtClinicNO.Location = new System.Drawing.Point(90, 7);
            this.txtClinicNO.Name = "txtClinicNO";
            this.txtClinicNO.Size = new System.Drawing.Size(144, 21);
            this.txtClinicNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtClinicNO.TabIndex = 0;
            this.txtClinicNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClinicNO_KeyDown);
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
            this.lblName.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.chbencrypt.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.cmbSex.Location = new System.Drawing.Point(90, 37);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.OmitFilter = true;
            this.cmbSex.SelectedItem = null;
            this.cmbSex.SelectNone = true;
            this.cmbSex.ShowID = true;
            this.cmbSex.Size = new System.Drawing.Size(42, 21);
            this.cmbSex.TabIndex = 3;
            this.cmbSex.Tag = "";
            this.cmbSex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSex_KeyDown);
            // 
            // dtpBirthDay
            // 
            this.dtpBirthDay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpBirthDay.InputMsg = "出生日期格式不正确!";
            this.dtpBirthDay.IsDefaultCHInput = false;
            this.dtpBirthDay.IsTagInput = false;
            this.dtpBirthDay.IsTextInput = true;
            this.dtpBirthDay.Location = new System.Drawing.Point(185, 37);
            this.dtpBirthDay.Mask = "0000-00-00 90:00:00";
            this.dtpBirthDay.Name = "dtpBirthDay";
            this.dtpBirthDay.Size = new System.Drawing.Size(121, 21);
            this.dtpBirthDay.TabIndex = 4;
            this.dtpBirthDay.ValidatingType = typeof(System.DateTime);
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
            this.txtAge.Location = new System.Drawing.Point(308, 37);
            this.txtAge.MaxLength = 3;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(132, 21);
            this.txtAge.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAge.TabIndex = 4;
            // 
            // txtIDNO
            // 
            this.txtIDNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIDNO.InputMsg = "请填写身份证号";
            this.txtIDNO.IsDefaultCHInput = false;
            this.txtIDNO.IsEnter2Tab = false;
            this.txtIDNO.IsTagInput = false;
            this.txtIDNO.IsTextInput = true;
            this.txtIDNO.Location = new System.Drawing.Point(527, 7);
            this.txtIDNO.MaxLength = 18;
            this.txtIDNO.Name = "txtIDNO";
            this.txtIDNO.Size = new System.Drawing.Size(353, 21);
            this.txtIDNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIDNO.TabIndex = 2;
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
            this.lblDoctDept.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoctDept.TabIndex = 27;
            this.lblDoctDept.Text = "----";
            // 
            // lblIDNO
            // 
            this.lblIDNO.AutoSize = true;
            this.lblIDNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIDNO.ForeColor = System.Drawing.Color.Blue;
            this.lblIDNO.InputMsg = "";
            this.lblIDNO.IsDefaultCHInput = false;
            this.lblIDNO.IsTagInput = false;
            this.lblIDNO.IsTextInput = false;
            this.lblIDNO.Location = new System.Drawing.Point(463, 12);
            this.lblIDNO.Name = "lblIDNO";
            this.lblIDNO.Size = new System.Drawing.Size(59, 12);
            this.lblIDNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblBirthday.Location = new System.Drawing.Point(131, 42);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(59, 12);
            this.lblBirthday.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblSex.Location = new System.Drawing.Point(48, 42);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(35, 12);
            this.lblSex.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblSex.TabIndex = 15;
            this.lblSex.Text = "性别:";
            // 
            // txtBirthArea
            // 
            this.txtBirthArea.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBirthArea.InputMsg = "";
            this.txtBirthArea.IsDefaultCHInput = true;
            this.txtBirthArea.IsEnter2Tab = false;
            this.txtBirthArea.IsTagInput = false;
            this.txtBirthArea.IsTextInput = false;
            this.txtBirthArea.Location = new System.Drawing.Point(527, 63);
            this.txtBirthArea.Name = "txtBirthArea";
            this.txtBirthArea.Size = new System.Drawing.Size(131, 21);
            this.txtBirthArea.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBirthArea.TabIndex = 11;
            // 
            // cmbNation
            // 
            this.cmbNation.EnterVisiable = true;
            this.cmbNation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbNation.InputMsg = "";
            this.cmbNation.IsDefaultCHInput = false;
            this.cmbNation.IsFind = true;
            this.cmbNation.IsTagInput = false;
            this.cmbNation.IsTextInput = false;
            this.cmbNation.ListBoxHeight = 100;
            this.cmbNation.ListBoxWidth = 100;
            this.cmbNation.Location = new System.Drawing.Point(736, 63);
            this.cmbNation.Name = "cmbNation";
            this.cmbNation.OmitFilter = true;
            this.cmbNation.SelectedItem = null;
            this.cmbNation.SelectNone = true;
            this.cmbNation.ShowID = true;
            this.cmbNation.Size = new System.Drawing.Size(144, 21);
            this.cmbNation.TabIndex = 12;
            this.cmbNation.Tag = "";
            // 
            // txtWorkAddress
            // 
            this.txtWorkAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWorkAddress.InputMsg = "";
            this.txtWorkAddress.IsDefaultCHInput = true;
            this.txtWorkAddress.IsEnter2Tab = false;
            this.txtWorkAddress.IsTagInput = false;
            this.txtWorkAddress.IsTextInput = false;
            this.txtWorkAddress.Location = new System.Drawing.Point(90, 91);
            this.txtWorkAddress.Name = "txtWorkAddress";
            this.txtWorkAddress.Size = new System.Drawing.Size(350, 21);
            this.txtWorkAddress.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtWorkAddress.TabIndex = 13;
            // 
            // txtWorkPhone
            // 
            this.txtWorkPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWorkPhone.InputMsg = "";
            this.txtWorkPhone.IsDefaultCHInput = false;
            this.txtWorkPhone.IsEnter2Tab = false;
            this.txtWorkPhone.IsTagInput = false;
            this.txtWorkPhone.IsTextInput = false;
            this.txtWorkPhone.Location = new System.Drawing.Point(527, 90);
            this.txtWorkPhone.MaxLength = 0;
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.Size = new System.Drawing.Size(131, 21);
            this.txtWorkPhone.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtWorkPhone.TabIndex = 14;
            // 
            // txtHomePhone
            // 
            this.txtHomePhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHomePhone.InputMsg = "";
            this.txtHomePhone.IsDefaultCHInput = false;
            this.txtHomePhone.IsEnter2Tab = false;
            this.txtHomePhone.IsTagInput = false;
            this.txtHomePhone.IsTextInput = false;
            this.txtHomePhone.Location = new System.Drawing.Point(736, 90);
            this.txtHomePhone.MaxLength = 0;
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Size = new System.Drawing.Size(144, 21);
            this.txtHomePhone.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHomePhone.TabIndex = 15;
            // 
            // txtAddressNow
            // 
            this.txtAddressNow.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAddressNow.InputMsg = "";
            this.txtAddressNow.IsDefaultCHInput = false;
            this.txtAddressNow.IsEnter2Tab = false;
            this.txtAddressNow.IsTagInput = false;
            this.txtAddressNow.IsTextInput = false;
            this.txtAddressNow.Location = new System.Drawing.Point(90, 119);
            this.txtAddressNow.MaxLength = 100;
            this.txtAddressNow.Name = "txtAddressNow";
            this.txtAddressNow.Size = new System.Drawing.Size(350, 21);
            this.txtAddressNow.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAddressNow.TabIndex = 16;
            // 
            // txtHomeAddress
            // 
            this.txtHomeAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHomeAddress.InputMsg = "";
            this.txtHomeAddress.IsDefaultCHInput = true;
            this.txtHomeAddress.IsEnter2Tab = false;
            this.txtHomeAddress.IsTagInput = false;
            this.txtHomeAddress.IsTextInput = false;
            this.txtHomeAddress.Location = new System.Drawing.Point(527, 118);
            this.txtHomeAddress.MaxLength = 100;
            this.txtHomeAddress.Name = "txtHomeAddress";
            this.txtHomeAddress.Size = new System.Drawing.Size(353, 21);
            this.txtHomeAddress.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHomeAddress.TabIndex = 17;
            // 
            // txtLinkMan
            // 
            this.txtLinkMan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLinkMan.InputMsg = "请填写联系人";
            this.txtLinkMan.IsDefaultCHInput = false;
            this.txtLinkMan.IsEnter2Tab = false;
            this.txtLinkMan.IsTagInput = false;
            this.txtLinkMan.IsTextInput = true;
            this.txtLinkMan.Location = new System.Drawing.Point(309, 147);
            this.txtLinkMan.MaxLength = 0;
            this.txtLinkMan.Name = "txtLinkMan";
            this.txtLinkMan.Size = new System.Drawing.Size(131, 21);
            this.txtLinkMan.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLinkMan.TabIndex = 19;
            // 
            // cmbRelation
            // 
            this.cmbRelation.EnterVisiable = true;
            this.cmbRelation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbRelation.InputMsg = "";
            this.cmbRelation.IsDefaultCHInput = false;
            this.cmbRelation.IsFind = true;
            this.cmbRelation.IsTagInput = false;
            this.cmbRelation.IsTextInput = false;
            this.cmbRelation.ListBoxHeight = 100;
            this.cmbRelation.ListBoxWidth = 100;
            this.cmbRelation.Location = new System.Drawing.Point(527, 147);
            this.cmbRelation.Name = "cmbRelation";
            this.cmbRelation.OmitFilter = true;
            this.cmbRelation.SelectedItem = null;
            this.cmbRelation.SelectNone = true;
            this.cmbRelation.ShowID = true;
            this.cmbRelation.Size = new System.Drawing.Size(132, 21);
            this.cmbRelation.TabIndex = 20;
            this.cmbRelation.Tag = "";
            // 
            // txtLinkPhone
            // 
            this.txtLinkPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLinkPhone.InputMsg = "";
            this.txtLinkPhone.IsDefaultCHInput = false;
            this.txtLinkPhone.IsEnter2Tab = false;
            this.txtLinkPhone.IsTagInput = false;
            this.txtLinkPhone.IsTextInput = false;
            this.txtLinkPhone.Location = new System.Drawing.Point(736, 147);
            this.txtLinkPhone.MaxLength = 20;
            this.txtLinkPhone.Name = "txtLinkPhone";
            this.txtLinkPhone.Size = new System.Drawing.Size(144, 21);
            this.txtLinkPhone.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLinkPhone.TabIndex = 21;
            // 
            // txtLinkAddr
            // 
            this.txtLinkAddr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLinkAddr.InputMsg = "";
            this.txtLinkAddr.IsDefaultCHInput = true;
            this.txtLinkAddr.IsEnter2Tab = false;
            this.txtLinkAddr.IsTagInput = false;
            this.txtLinkAddr.IsTextInput = false;
            this.txtLinkAddr.Location = new System.Drawing.Point(90, 175);
            this.txtLinkAddr.Name = "txtLinkAddr";
            this.txtLinkAddr.Size = new System.Drawing.Size(350, 21);
            this.txtLinkAddr.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLinkAddr.TabIndex = 22;
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
            this.txtDiagnose.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDiagnose.TabIndex = 23;
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
            this.cmbInSource.TabIndex = 24;
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
            this.cmbCircs.TabIndex = 25;
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
            this.cmbApproach.TabIndex = 26;
            this.cmbApproach.Tag = "";
            // 
            // cmbDept
            // 
            this.cmbDept.EnterVisiable = true;
            this.cmbDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDept.InputMsg = "入院科室不能为空！";
            this.cmbDept.IsDefaultCHInput = false;
            this.cmbDept.IsFind = true;
            this.cmbDept.IsTagInput = true;
            this.cmbDept.IsTextInput = true;
            this.cmbDept.ListBoxHeight = 100;
            this.cmbDept.ListBoxWidth = 100;
            this.cmbDept.Location = new System.Drawing.Point(91, 232);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.OmitFilter = true;
            this.cmbDept.SelectedItem = null;
            this.cmbDept.SelectNone = false;
            this.cmbDept.ShowID = true;
            this.cmbDept.Size = new System.Drawing.Size(184, 21);
            this.cmbDept.TabIndex = 28;
            this.cmbDept.Tag = "";
            // 
            // cmbNurseCell
            // 
            this.cmbNurseCell.AccessibleDescription = "cmbPayMode";
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
            this.cmbNurseCell.TabIndex = 29;
            this.cmbNurseCell.Tag = "";
            // 
            // cmbBedNO
            // 
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
            this.cmbBedNO.TabIndex = 30;
            this.cmbBedNO.Tag = "";
            // 
            // cmbDoctor
            // 
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
            this.cmbDoctor.TabIndex = 31;
            this.cmbDoctor.Tag = "";
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
            this.txtMCardNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMCardNO.TabIndex = 33;
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
            this.txtComputerNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtComputerNO.TabIndex = 34;
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
            this.mtxtBloodFee.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.mtxtBloodFee.TabIndex = 35;
            this.mtxtBloodFee.TabStop = false;
            this.mtxtBloodFee.Text = "0.00";
            this.mtxtBloodFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtBloodFee.UseGroupSeperator = true;
            this.mtxtBloodFee.ZeroIsValid = true;
            // 
            // mTxtIntimes
            // 
            this.mTxtIntimes.AllowNegative = false;
            this.mTxtIntimes.Enabled = false;
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
            this.mTxtIntimes.ReadOnly = true;
            this.mTxtIntimes.SetRange = new System.Drawing.Size(-1, -1);
            this.mTxtIntimes.Size = new System.Drawing.Size(132, 21);
            this.mTxtIntimes.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.mTxtIntimes.TabIndex = 36;
            this.mTxtIntimes.TabStop = false;
            this.mTxtIntimes.Text = "1";
            this.mTxtIntimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mTxtIntimes.UseGroupSeperator = true;
            this.mTxtIntimes.ZeroIsValid = true;
            // 
            // mTxtPrepay
            // 
            this.mTxtPrepay.AllowNegative = false;
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
            this.mTxtPrepay.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.mTxtPrepay.TabIndex = 37;
            this.mTxtPrepay.Text = "0.00";
            this.mTxtPrepay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mTxtPrepay.UseGroupSeperator = true;
            this.mTxtPrepay.ZeroIsValid = true;
            // 
            // dtpInTime
            // 
            this.dtpInTime.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dtpInTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpInTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInTime.IsEnter2Tab = false;
            this.dtpInTime.Location = new System.Drawing.Point(90, 376);
            this.dtpInTime.Name = "dtpInTime";
            this.dtpInTime.Size = new System.Drawing.Size(144, 21);
            this.dtpInTime.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpInTime.TabIndex = 45;
            // 
            // txtInpatientNO
            // 
            this.txtInpatientNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInpatientNO.InputMsg = "住院号不能为空";
            this.txtInpatientNO.IsDefaultCHInput = false;
            this.txtInpatientNO.IsEnter2Tab = false;
            this.txtInpatientNO.IsTagInput = false;
            this.txtInpatientNO.IsTextInput = true;
            this.txtInpatientNO.Location = new System.Drawing.Point(421, 376);
            this.txtInpatientNO.MaxLength = 10;
            this.txtInpatientNO.Name = "txtInpatientNO";
            this.txtInpatientNO.Size = new System.Drawing.Size(132, 21);
            this.txtInpatientNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInpatientNO.TabIndex = 48;
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
            this.neuLabel2.Location = new System.Drawing.Point(275, 236);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(35, 12);
            this.neuLabel2.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 189;
            this.neuLabel2.Text = "病区:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.InputMsg = "";
            this.neuLabel1.IsDefaultCHInput = false;
            this.neuLabel1.IsTagInput = false;
            this.neuLabel1.IsTextInput = false;
            this.neuLabel1.Location = new System.Drawing.Point(674, 96);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.cmbOldDept.TabIndex = 27;
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
            this.lblOldDept.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOldDept.TabIndex = 187;
            this.lblOldDept.Text = "原科室:";
            this.lblOldDept.Visible = false;
            // 
            // lbAddressNow
            // 
            this.lbAddressNow.AutoSize = true;
            this.lbAddressNow.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAddressNow.InputMsg = "";
            this.lbAddressNow.IsDefaultCHInput = false;
            this.lbAddressNow.IsTagInput = false;
            this.lbAddressNow.IsTextInput = false;
            this.lbAddressNow.Location = new System.Drawing.Point(38, 124);
            this.lbAddressNow.Name = "lbAddressNow";
            this.lbAddressNow.Size = new System.Drawing.Size(47, 12);
            this.lbAddressNow.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lbAddressNow.TabIndex = 186;
            this.lbAddressNow.Text = "现住址:";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel6.InputMsg = "";
            this.neuLabel6.IsDefaultCHInput = false;
            this.neuLabel6.IsTagInput = false;
            this.neuLabel6.IsTextInput = false;
            this.neuLabel6.Location = new System.Drawing.Point(473, 68);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(47, 12);
            this.neuLabel6.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblBedCount.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.lblDept.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.lblInTimes.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblBloodFee.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblDiagnose.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblDoctor.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblCircs.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblApproach.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblInSource.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblLinkPhone.Location = new System.Drawing.Point(662, 152);
            this.lblLinkPhone.Name = "lblLinkPhone";
            this.lblLinkPhone.Size = new System.Drawing.Size(71, 12);
            this.lblLinkPhone.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblLinkPhone.TabIndex = 172;
            this.lblLinkPhone.Text = "联系人电话:";
            // 
            // lblLinkAddress
            // 
            this.lblLinkAddress.AutoSize = true;
            this.lblLinkAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLinkAddress.InputMsg = "";
            this.lblLinkAddress.IsDefaultCHInput = false;
            this.lblLinkAddress.IsTagInput = false;
            this.lblLinkAddress.IsTextInput = false;
            this.lblLinkAddress.Location = new System.Drawing.Point(14, 180);
            this.lblLinkAddress.Name = "lblLinkAddress";
            this.lblLinkAddress.Size = new System.Drawing.Size(71, 12);
            this.lblLinkAddress.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblLinkAddress.TabIndex = 171;
            this.lblLinkAddress.Text = "联系人地址:";
            // 
            // lblRelation
            // 
            this.lblRelation.AutoSize = true;
            this.lblRelation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRelation.InputMsg = "";
            this.lblRelation.IsDefaultCHInput = false;
            this.lblRelation.IsTagInput = false;
            this.lblRelation.IsTextInput = false;
            this.lblRelation.Location = new System.Drawing.Point(485, 152);
            this.lblRelation.Name = "lblRelation";
            this.lblRelation.Size = new System.Drawing.Size(35, 12);
            this.lblRelation.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblLinkMan.Location = new System.Drawing.Point(261, 152);
            this.lblLinkMan.Name = "lblLinkMan";
            this.lblLinkMan.Size = new System.Drawing.Size(47, 12);
            this.lblLinkMan.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblLinkMan.TabIndex = 169;
            this.lblLinkMan.Text = "联系人:";
            // 
            // lblHomeAddress
            // 
            this.lblHomeAddress.AutoSize = true;
            this.lblHomeAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHomeAddress.InputMsg = "";
            this.lblHomeAddress.IsDefaultCHInput = false;
            this.lblHomeAddress.IsTagInput = false;
            this.lblHomeAddress.IsTextInput = false;
            this.lblHomeAddress.Location = new System.Drawing.Point(461, 124);
            this.lblHomeAddress.Name = "lblHomeAddress";
            this.lblHomeAddress.Size = new System.Drawing.Size(59, 12);
            this.lblHomeAddress.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblHomeAddress.TabIndex = 168;
            this.lblHomeAddress.Text = "户口地址:";
            // 
            // lblWorkPhone
            // 
            this.lblWorkPhone.AutoSize = true;
            this.lblWorkPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWorkPhone.InputMsg = "";
            this.lblWorkPhone.IsDefaultCHInput = false;
            this.lblWorkPhone.IsTagInput = false;
            this.lblWorkPhone.IsTextInput = false;
            this.lblWorkPhone.Location = new System.Drawing.Point(461, 96);
            this.lblWorkPhone.Name = "lblWorkPhone";
            this.lblWorkPhone.Size = new System.Drawing.Size(59, 12);
            this.lblWorkPhone.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblWorkPhone.TabIndex = 165;
            this.lblWorkPhone.Text = "单位电话:";
            // 
            // lblProfession
            // 
            this.lblProfession.AutoSize = true;
            this.lblProfession.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProfession.InputMsg = "";
            this.lblProfession.IsDefaultCHInput = false;
            this.lblProfession.IsTagInput = false;
            this.lblProfession.IsTextInput = false;
            this.lblProfession.Location = new System.Drawing.Point(271, 68);
            this.lblProfession.Name = "lblProfession";
            this.lblProfession.Size = new System.Drawing.Size(35, 12);
            this.lblProfession.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblProfession.TabIndex = 162;
            this.lblProfession.Text = "职业:";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCountry.InputMsg = "";
            this.lblCountry.IsDefaultCHInput = false;
            this.lblCountry.IsTagInput = false;
            this.lblCountry.IsTextInput = false;
            this.lblCountry.Location = new System.Drawing.Point(698, 40);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(35, 12);
            this.lblCountry.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblMarry.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblMarry.TabIndex = 154;
            this.lblMarry.Text = "婚姻状况:";
            // 
            // lblWorkAddress
            // 
            this.lblWorkAddress.AutoSize = true;
            this.lblWorkAddress.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWorkAddress.InputMsg = "";
            this.lblWorkAddress.IsDefaultCHInput = false;
            this.lblWorkAddress.IsTagInput = false;
            this.lblWorkAddress.IsTextInput = false;
            this.lblWorkAddress.Location = new System.Drawing.Point(26, 96);
            this.lblWorkAddress.Name = "lblWorkAddress";
            this.lblWorkAddress.Size = new System.Drawing.Size(59, 12);
            this.lblWorkAddress.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblWorkAddress.TabIndex = 150;
            this.lblWorkAddress.Text = "工作单位:";
            // 
            // lblNation
            // 
            this.lblNation.AutoSize = true;
            this.lblNation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNation.InputMsg = "";
            this.lblNation.IsDefaultCHInput = false;
            this.lblNation.IsTagInput = false;
            this.lblNation.IsTextInput = false;
            this.lblNation.Location = new System.Drawing.Point(698, 68);
            this.lblNation.Name = "lblNation";
            this.lblNation.Size = new System.Drawing.Size(35, 12);
            this.lblNation.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblNation.TabIndex = 138;
            this.lblNation.Text = "民族:";
            // 
            // lblAirLimit
            // 
            this.lblAirLimit.AutoSize = true;
            this.lblAirLimit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAirLimit.ForeColor = System.Drawing.Color.Blue;
            this.lblAirLimit.InputMsg = "";
            this.lblAirLimit.IsDefaultCHInput = false;
            this.lblAirLimit.IsTagInput = false;
            this.lblAirLimit.IsTextInput = false;
            this.lblAirLimit.Location = new System.Drawing.Point(455, 321);
            this.lblAirLimit.Name = "lblAirLimit";
            this.lblAirLimit.Size = new System.Drawing.Size(59, 12);
            this.lblAirLimit.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblAirLimit.TabIndex = 129;
            this.lblAirLimit.Text = "监 护 床:";
            // 
            // lblBedLimit
            // 
            this.lblBedLimit.AutoSize = true;
            this.lblBedLimit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedLimit.ForeColor = System.Drawing.Color.Blue;
            this.lblBedLimit.InputMsg = "";
            this.lblBedLimit.IsDefaultCHInput = false;
            this.lblBedLimit.IsTagInput = false;
            this.lblBedLimit.IsTextInput = false;
            this.lblBedLimit.Location = new System.Drawing.Point(247, 319);
            this.lblBedLimit.Name = "lblBedLimit";
            this.lblBedLimit.Size = new System.Drawing.Size(59, 12);
            this.lblBedLimit.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblBedLimit.TabIndex = 129;
            this.lblBedLimit.Text = "普通标准:";
            // 
            // lblDayLimit
            // 
            this.lblDayLimit.AutoSize = true;
            this.lblDayLimit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDayLimit.ForeColor = System.Drawing.Color.Blue;
            this.lblDayLimit.InputMsg = "";
            this.lblDayLimit.IsDefaultCHInput = false;
            this.lblDayLimit.IsTagInput = false;
            this.lblDayLimit.IsTextInput = false;
            this.lblDayLimit.Location = new System.Drawing.Point(26, 319);
            this.lblDayLimit.Name = "lblDayLimit";
            this.lblDayLimit.Size = new System.Drawing.Size(59, 12);
            this.lblDayLimit.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblDayLimit.TabIndex = 129;
            this.lblDayLimit.Text = "公费日限:";
            // 
            // txtAirLimit
            // 
            this.txtAirLimit.AllowNegative = false;
            this.txtAirLimit.AutoPadRightZero = true;
            this.txtAirLimit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAirLimit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtAirLimit.Location = new System.Drawing.Point(527, 316);
            this.txtAirLimit.MaxDigits = 2;
            this.txtAirLimit.MaxLength = 8;
            this.txtAirLimit.Name = "txtAirLimit";
            this.txtAirLimit.ReadOnly = true;
            this.txtAirLimit.Size = new System.Drawing.Size(131, 26);
            this.txtAirLimit.TabIndex = 41;
            this.txtAirLimit.TabStop = false;
            this.txtAirLimit.Text = "0.00";
            this.txtAirLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAirLimit.WillShowError = true;
            // 
            // txtBedLimit
            // 
            this.txtBedLimit.AllowNegative = false;
            this.txtBedLimit.AutoPadRightZero = true;
            this.txtBedLimit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBedLimit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtBedLimit.Location = new System.Drawing.Point(308, 312);
            this.txtBedLimit.MaxDigits = 2;
            this.txtBedLimit.MaxLength = 8;
            this.txtBedLimit.Name = "txtBedLimit";
            this.txtBedLimit.ReadOnly = true;
            this.txtBedLimit.Size = new System.Drawing.Size(132, 26);
            this.txtBedLimit.TabIndex = 40;
            this.txtBedLimit.TabStop = false;
            this.txtBedLimit.Text = "0.00";
            this.txtBedLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBedLimit.WillShowError = false;
            this.txtBedLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBedLimit_KeyPress);
            // 
            // txtDayLimit
            // 
            this.txtDayLimit.AllowNegative = false;
            this.txtDayLimit.AutoPadRightZero = true;
            this.txtDayLimit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDayLimit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtDayLimit.Location = new System.Drawing.Point(90, 314);
            this.txtDayLimit.MaxDigits = 2;
            this.txtDayLimit.MaxLength = 8;
            this.txtDayLimit.Name = "txtDayLimit";
            this.txtDayLimit.ReadOnly = true;
            this.txtDayLimit.Size = new System.Drawing.Size(142, 26);
            this.txtDayLimit.TabIndex = 39;
            this.txtDayLimit.TabStop = false;
            this.txtDayLimit.Text = "0.00";
            this.txtDayLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDayLimit.WillShowError = false;
            this.txtDayLimit.TextChanged += new System.EventHandler(this.txtDayLimit_TextChanged);
            this.txtDayLimit.Enter += new System.EventHandler(this.txtDayLimit_Enter);
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
            this.lblPact.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblInTime.Location = new System.Drawing.Point(24, 379);
            this.lblInTime.Name = "lblInTime";
            this.lblInTime.Size = new System.Drawing.Size(59, 12);
            this.lblInTime.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblMCardNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblMCardNO.TabIndex = 123;
            this.lblMCardNO.Text = "医疗证号:";
            // 
            // rdoInpatientNO
            // 
            this.rdoInpatientNO.AutoSize = true;
            this.rdoInpatientNO.Checked = true;
            this.rdoInpatientNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoInpatientNO.ForeColor = System.Drawing.Color.Blue;
            this.rdoInpatientNO.Location = new System.Drawing.Point(243, 379);
            this.rdoInpatientNO.Name = "rdoInpatientNO";
            this.rdoInpatientNO.Size = new System.Drawing.Size(83, 16);
            this.rdoInpatientNO.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rdoInpatientNO.TabIndex = 46;
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
            this.lblPrepay.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblPayMode.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblPayMode.TabIndex = 181;
            this.lblPayMode.Text = "支付方式:";
            // 
            // ucRegisterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.Controls.Add(this.plInfomation);
            this.Name = "ucRegisterInfo";
            this.Size = new System.Drawing.Size(907, 415);
            this.plInfomation.ResumeLayout(false);
            this.plInfomation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected Neusoft.FrameWork.WinForms.Controls.NeuPanel plInfomation;
        private NeuInputLabel lblDoctDept;
        private NeuInputListTextBox cmbSex;
        private NeuInputDateTime dtpBirthDay;
        protected Neusoft.FrameWork.WinForms.Controls.NeuCheckBox chbencrypt;
        protected NeuInputTextBox txtIDNO;
        protected NeuInputLabel lblIDNO;
        protected NeuInputTextBox txtAge;
        protected NeuInputLabel lblBirthday;
        protected NeuInputLabel lblSex;
        protected NeuInputLabel lblName;
        protected NeuInputTextBox txtClinicNO;
        protected Neusoft.FrameWork.WinForms.Controls.NeuContextMenuStrip neuContextMenuStrip1;
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
        private NeuInputListTextBox cmbBedNO;
        private NeuInputListTextBox cmbDoctor;
        private NeuInputListTextBox cmbCircs;
        private NeuInputListTextBox cmbRelation;
        private NeuInputListTextBox cmbInSource;
        private NeuInputListTextBox cmbApproach;
        private NeuInputListTextBox cmbNation;
        private NeuInputListTextBox cmbDept;
        private NeuInputLabel lblBedCount;
        private NeuInputTextBox txtWorkAddress;
        private NeuInputTextBox txtHomeAddress;
        private NeuInputTextBox txtLinkAddr;
        protected NeuInputLabel lblDept;
        protected Neusoft.FrameWork.WinForms.Controls.NeuNumericTextBox mTxtIntimes;
        protected Neusoft.FrameWork.WinForms.Controls.NeuNumericTextBox mtxtBloodFee;
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
        protected Neusoft.FrameWork.WinForms.Controls.NeuRadioButton rdoInpatientNO;
        protected Neusoft.FrameWork.WinForms.Controls.NeuNumericTextBox mTxtPrepay;
        protected NeuInputLabel lblPrepay;
        protected NeuInputLabel lblPayMode;
        protected Neusoft.FrameWork.WinForms.Controls.NeuDateTimePicker dtpInTime;
        protected NeuInputTextBox txtInpatientNO;
        protected NeuInputLabel neuInputLabel1;
        protected NeuInputTextBox txtName;
        protected NeuInputLabel neuInputLabel2;
        protected Neusoft.FrameWork.WinForms.Controls.NeuRadioButton rbtnTempPatientNO;
        protected NeuInputLabel lblDayLimit;
        private Neusoft.FrameWork.WinForms.Controls.ValidatedTextBox txtDayLimit;
        protected NeuInputLabel lblBedLimit;
        private Neusoft.FrameWork.WinForms.Controls.ValidatedTextBox txtBedLimit;
        protected NeuInputLabel lblAirLimit;
        private Neusoft.FrameWork.WinForms.Controls.ValidatedTextBox txtAirLimit;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel lblRate;
        private Neusoft.FrameWork.WinForms.Controls.ValidatedTextBox vtbRate;
        protected NeuInputLabel lblBedOverDeal;
        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox cmbBedOverDeal;
        protected NeuInputLabel lblOverLop;
        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox cmbOverLop;
        private Neusoft.FrameWork.WinForms.Controls.NeuButton btnUpdateRealInvoiceNO;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel lbRealInvoiceNO;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox txtRealInvoiceNO;
        private global::Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Common.cmbTransType cmbPayType;
        private NeuCombox cmbProfession;
        private NeuCombox cmbMarry;
        private NeuCombox cmbCountry;
        private NeuCombox cmbPact;
        private NeuCombox cmbBirthArea;
        private NeuInputListTextBox txtHomeZip;

    }
}
