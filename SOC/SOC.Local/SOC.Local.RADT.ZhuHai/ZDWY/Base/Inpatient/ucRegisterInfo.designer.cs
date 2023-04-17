﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
using System.Xml;
using FS.FrameWork.Function;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.RADT;
using FS.SOC.HISFC.RADT.Interface;
using FS.SOC.HISFC.RADT.Components.Common;
using FS.SOC.Local.RADT.ZhuHai.ZDWY.Common;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient
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
            FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel lblBirthArea;
            FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel lblBedNO;
            this.plInfomation = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuContextMenuStrip1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.cbxChild = new System.Windows.Forms.CheckBox();
            this.cmbOldDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.lblOldDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblComputerNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.cmbPayType = new FS.SOC.Local.RADT.ZhuHai.ZDWY.Common.cmbTransType();
            this.mTxtPrepay = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.lblPrepay = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblPayMode = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtComputerNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.mtxtBloodFee = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.lblBloodFee = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.cmbPatientType = new FS.SOC.Local.RADT.ZhuHai.ZDWY.Common.NeuCombox();
            this.neuInputLabel4 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.ckHour = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.lbAgeThree = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lbAgeTwo = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lbAgeOne = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtAgeThree = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.txtAgeTwo = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.txtAgeOne = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.txtHomeZip = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbBirthArea = new FS.SOC.Local.RADT.ZhuHai.ZDWY.Common.NeuCombox();
            this.cmbPact = new FS.SOC.Local.RADT.ZhuHai.ZDWY.Common.NeuCombox();
            this.cmbProfession = new FS.SOC.Local.RADT.ZhuHai.ZDWY.Common.NeuCombox();
            this.cmbMarry = new FS.SOC.Local.RADT.ZhuHai.ZDWY.Common.NeuCombox();
            this.cmbCountry = new FS.SOC.Local.RADT.ZhuHai.ZDWY.Common.NeuCombox();
            this.txtRealInvoiceNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbRealInvoiceNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnUpdateRealInvoiceNO = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cmbOverLop = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblOverLop = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.cmbBedOverDeal = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblBedOverDeal = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.vtbRate = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.lblRate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.rbtnTempPatientNO = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuInputLabel2 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtName = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.neuInputLabel1 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtClinicNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.lblName = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.chbencrypt = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbSex = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.txtIDNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.lblDoctDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblIDNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBirthday = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblSex = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
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
            this.txtMemo = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.txtDiagnose = new FS.SOC.Local.RADT.ZhuHai.ZDWY.Common.NeuCombox();
            this.cmbInSource = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbCircs = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbApproach = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbNurseCell = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbBedNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.cmbDoctor = new FS.SOC.HISFC.RADT.Components.Common.NeuInputListTextBox();
            this.txtMCardNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.mTxtIntimes = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.dtpBirthDay = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpInTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtInpatientNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputTextBox();
            this.neuLabel2 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.neuLabel1 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lbAddressNow = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.neuLabel6 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBedCount = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDept = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblInTimes = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDiagnose = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDoctor = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblCircs = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblApproach = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblInSource = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblLinkPhone = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblLinkAddress = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.neuInputLabel3 = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblRelation = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblLinkMan = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblHomeAddress = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblWorkPhone = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblProfession = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblCountry = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblMarry = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblWorkAddress = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblNation = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblAirLimit = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblBedLimit = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblDayLimit = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.txtAirLimit = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtBedLimit = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtDayLimit = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.lblPact = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblInTime = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.lblMCardNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            this.rdoInpatientNO = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            lblBirthArea = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
            lblBedNO = new FS.SOC.HISFC.RADT.Components.Common.NeuInputLabel();
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
            lblBirthArea.Location = new System.Drawing.Point(577, 40);
            lblBirthArea.Name = "lblBirthArea";
            lblBirthArea.Size = new System.Drawing.Size(35, 12);
            lblBirthArea.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            lblBedNO.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            lblBedNO.TabIndex = 143;
            lblBedNO.Text = "病床号:";
            // 
            // plInfomation
            // 
            this.plInfomation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plInfomation.ContextMenuStrip = this.neuContextMenuStrip1;
            this.plInfomation.Controls.Add(this.cbxChild);
            this.plInfomation.Controls.Add(this.cmbOldDept);
            this.plInfomation.Controls.Add(this.lblOldDept);
            this.plInfomation.Controls.Add(this.lblComputerNO);
            this.plInfomation.Controls.Add(this.cmbPayType);
            this.plInfomation.Controls.Add(this.mTxtPrepay);
            this.plInfomation.Controls.Add(this.lblPrepay);
            this.plInfomation.Controls.Add(this.lblPayMode);
            this.plInfomation.Controls.Add(this.txtComputerNO);
            this.plInfomation.Controls.Add(this.mtxtBloodFee);
            this.plInfomation.Controls.Add(this.lblBloodFee);
            this.plInfomation.Controls.Add(this.cmbPatientType);
            this.plInfomation.Controls.Add(this.neuInputLabel4);
            this.plInfomation.Controls.Add(this.ckHour);
            this.plInfomation.Controls.Add(this.lbAgeThree);
            this.plInfomation.Controls.Add(this.lbAgeTwo);
            this.plInfomation.Controls.Add(this.lbAgeOne);
            this.plInfomation.Controls.Add(this.txtAgeThree);
            this.plInfomation.Controls.Add(this.txtAgeTwo);
            this.plInfomation.Controls.Add(this.txtAgeOne);
            this.plInfomation.Controls.Add(this.txtHomeZip);
            this.plInfomation.Controls.Add(this.cmbBirthArea);
            this.plInfomation.Controls.Add(this.cmbPact);
            this.plInfomation.Controls.Add(this.cmbProfession);
            this.plInfomation.Controls.Add(this.cmbMarry);
            this.plInfomation.Controls.Add(this.cmbCountry);
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
            this.plInfomation.Controls.Add(this.txtMemo);
            this.plInfomation.Controls.Add(this.txtDiagnose);
            this.plInfomation.Controls.Add(this.cmbInSource);
            this.plInfomation.Controls.Add(this.cmbCircs);
            this.plInfomation.Controls.Add(this.cmbApproach);
            this.plInfomation.Controls.Add(this.cmbDept);
            this.plInfomation.Controls.Add(this.cmbNurseCell);
            this.plInfomation.Controls.Add(this.cmbBedNO);
            this.plInfomation.Controls.Add(this.cmbDoctor);
            this.plInfomation.Controls.Add(this.txtMCardNO);
            this.plInfomation.Controls.Add(this.mTxtIntimes);
            this.plInfomation.Controls.Add(this.dtpBirthDay);
            this.plInfomation.Controls.Add(this.dtpInTime);
            this.plInfomation.Controls.Add(this.txtInpatientNO);
            this.plInfomation.Controls.Add(this.neuLabel2);
            this.plInfomation.Controls.Add(this.neuLabel1);
            this.plInfomation.Controls.Add(this.lbAddressNow);
            this.plInfomation.Controls.Add(this.neuLabel6);
            this.plInfomation.Controls.Add(this.lblBedCount);
            this.plInfomation.Controls.Add(this.lblDept);
            this.plInfomation.Controls.Add(this.lblInTimes);
            this.plInfomation.Controls.Add(this.lblDiagnose);
            this.plInfomation.Controls.Add(this.lblDoctor);
            this.plInfomation.Controls.Add(this.lblCircs);
            this.plInfomation.Controls.Add(this.lblApproach);
            this.plInfomation.Controls.Add(this.lblInSource);
            this.plInfomation.Controls.Add(this.lblLinkPhone);
            this.plInfomation.Controls.Add(this.lblLinkAddress);
            this.plInfomation.Controls.Add(this.neuInputLabel3);
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
            this.plInfomation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plInfomation.Location = new System.Drawing.Point(3, 3);
            this.plInfomation.Name = "plInfomation";
            this.plInfomation.Size = new System.Drawing.Size(901, 409);
            this.plInfomation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plInfomation.TabIndex = 0;
            // 
            // neuContextMenuStrip1
            // 
            this.neuContextMenuStrip1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuContextMenuStrip1.Name = "neuContextMenuStrip1";
            this.neuContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.neuContextMenuStrip1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // cbxChild
            // 
            this.cbxChild.AutoSize = true;
            this.cbxChild.Location = new System.Drawing.Point(173, 150);
            this.cbxChild.Name = "cbxChild";
            this.cbxChild.Size = new System.Drawing.Size(84, 16);
            this.cbxChild.TabIndex = 213;
            this.cbxChild.Text = "是否是儿童";
            this.cbxChild.UseVisualStyleBackColor = true;
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
            this.cmbOldDept.Location = new System.Drawing.Point(783, 370);
            this.cmbOldDept.Name = "cmbOldDept";
            this.cmbOldDept.OmitFilter = true;
            this.cmbOldDept.SelectedItem = null;
            this.cmbOldDept.SelectNone = true;
            this.cmbOldDept.ShowID = true;
            this.cmbOldDept.Size = new System.Drawing.Size(97, 21);
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
            this.lblOldDept.Location = new System.Drawing.Point(733, 376);
            this.lblOldDept.Name = "lblOldDept";
            this.lblOldDept.Size = new System.Drawing.Size(47, 12);
            this.lblOldDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOldDept.TabIndex = 187;
            this.lblOldDept.Text = "原科室:";
            this.lblOldDept.Visible = false;
            // 
            // lblComputerNO
            // 
            this.lblComputerNO.AutoSize = true;
            this.lblComputerNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblComputerNO.InputMsg = "";
            this.lblComputerNO.IsDefaultCHInput = false;
            this.lblComputerNO.IsTagInput = false;
            this.lblComputerNO.IsTextInput = false;
            this.lblComputerNO.Location = new System.Drawing.Point(260, 376);
            this.lblComputerNO.Name = "lblComputerNO";
            this.lblComputerNO.Size = new System.Drawing.Size(47, 12);
            this.lblComputerNO.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblComputerNO.TabIndex = 212;
            this.lblComputerNO.Text = "电脑号:";
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
            this.cmbPayType.IsShowIDAndName = false;
            this.cmbPayType.Location = new System.Drawing.Point(736, 318);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Pop = false;
            this.cmbPayType.ShowCustomerList = false;
            this.cmbPayType.ShowID = false;
            this.cmbPayType.Size = new System.Drawing.Size(144, 20);
            this.cmbPayType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPayType.TabIndex = 38;
            this.cmbPayType.Tag = "";
            this.cmbPayType.ToolBarUse = false;
            this.cmbPayType.WorkUnit = "";
            // 
            // mTxtPrepay
            // 
            this.mTxtPrepay.AllowNegative = false;
            this.mTxtPrepay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mTxtPrepay.IsAutoRemoveDecimalZero = false;
            this.mTxtPrepay.IsEnter2Tab = false;
            this.mTxtPrepay.Location = new System.Drawing.Point(527, 317);
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
            this.mTxtPrepay.TabIndex = 37;
            this.mTxtPrepay.Text = "0.00";
            this.mTxtPrepay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mTxtPrepay.UseGroupSeperator = true;
            this.mTxtPrepay.ZeroIsValid = true;
            // 
            // lblPrepay
            // 
            this.lblPrepay.AutoSize = true;
            this.lblPrepay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrepay.InputMsg = "";
            this.lblPrepay.IsDefaultCHInput = false;
            this.lblPrepay.IsTagInput = false;
            this.lblPrepay.IsTextInput = false;
            this.lblPrepay.Location = new System.Drawing.Point(461, 319);
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
            this.lblPayMode.Location = new System.Drawing.Point(674, 319);
            this.lblPayMode.Name = "lblPayMode";
            this.lblPayMode.Size = new System.Drawing.Size(59, 12);
            this.lblPayMode.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblPayMode.TabIndex = 181;
            this.lblPayMode.Text = "支付方式:";
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
            this.txtComputerNO.Location = new System.Drawing.Point(309, 371);
            this.txtComputerNO.MaxLength = 0;
            this.txtComputerNO.Name = "txtComputerNO";
            this.txtComputerNO.Size = new System.Drawing.Size(131, 21);
            this.txtComputerNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtComputerNO.TabIndex = 34;
            // 
            // mtxtBloodFee
            // 
            this.mtxtBloodFee.AllowNegative = false;
            this.mtxtBloodFee.Enabled = false;
            this.mtxtBloodFee.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mtxtBloodFee.IsAutoRemoveDecimalZero = false;
            this.mtxtBloodFee.IsEnter2Tab = false;
            this.mtxtBloodFee.Location = new System.Drawing.Point(90, 371);
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
            this.mtxtBloodFee.Size = new System.Drawing.Size(144, 21);
            this.mtxtBloodFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.mtxtBloodFee.TabIndex = 35;
            this.mtxtBloodFee.TabStop = false;
            this.mtxtBloodFee.Text = "0.00";
            this.mtxtBloodFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtBloodFee.UseGroupSeperator = true;
            this.mtxtBloodFee.ZeroIsValid = true;
            // 
            // lblBloodFee
            // 
            this.lblBloodFee.AutoSize = true;
            this.lblBloodFee.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBloodFee.InputMsg = "";
            this.lblBloodFee.IsDefaultCHInput = false;
            this.lblBloodFee.IsTagInput = false;
            this.lblBloodFee.IsTextInput = false;
            this.lblBloodFee.Location = new System.Drawing.Point(26, 376);
            this.lblBloodFee.Name = "lblBloodFee";
            this.lblBloodFee.Size = new System.Drawing.Size(59, 12);
            this.lblBloodFee.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblBloodFee.TabIndex = 178;
            this.lblBloodFee.Text = "血滞纳金:";
            // 
            // cmbPatientType
            // 
            this.cmbPatientType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPatientType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPatientType.FormattingEnabled = true;
            this.cmbPatientType.InputMsg = "";
            this.cmbPatientType.IsDefaultCHInput = false;
            this.cmbPatientType.IsEnter2Tab = false;
            this.cmbPatientType.IsFind = false;
            this.cmbPatientType.IsFlat = false;
            this.cmbPatientType.IsLike = true;
            this.cmbPatientType.IsListOnly = false;
            this.cmbPatientType.IsPopForm = true;
            this.cmbPatientType.IsShowCustomerList = false;
            this.cmbPatientType.IsShowID = false;
            this.cmbPatientType.IsShowIDAndName = false;
            this.cmbPatientType.IsTagInput = false;
            this.cmbPatientType.IsTextInput = false;
            this.cmbPatientType.Location = new System.Drawing.Point(736, 200);
            this.cmbPatientType.Name = "cmbPatientType";
            this.cmbPatientType.ShowCustomerList = false;
            this.cmbPatientType.ShowID = false;
            this.cmbPatientType.Size = new System.Drawing.Size(144, 20);
            this.cmbPatientType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPatientType.TabIndex = 211;
            this.cmbPatientType.Tag = "";
            this.cmbPatientType.ToolBarUse = false;
            this.cmbPatientType.SelectedIndexChanged += new System.EventHandler(this.cmbPatientType_SelectedIndexChanged);
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
            this.neuInputLabel4.Location = new System.Drawing.Point(674, 206);
            this.neuInputLabel4.Name = "neuInputLabel4";
            this.neuInputLabel4.Size = new System.Drawing.Size(59, 12);
            this.neuInputLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuInputLabel4.TabIndex = 210;
            this.neuInputLabel4.Text = "患者类型:";
            // 
            // ckHour
            // 
            this.ckHour.AutoSize = true;
            this.ckHour.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckHour.Location = new System.Drawing.Point(406, 39);
            this.ckHour.Name = "ckHour";
            this.ckHour.Size = new System.Drawing.Size(48, 16);
            this.ckHour.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckHour.TabIndex = 208;
            this.ckHour.TabStop = false;
            this.ckHour.Text = "时分";
            this.ckHour.UseVisualStyleBackColor = true;
            // 
            // lbAgeThree
            // 
            this.lbAgeThree.AutoSize = true;
            this.lbAgeThree.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAgeThree.ForeColor = System.Drawing.Color.Blue;
            this.lbAgeThree.InputMsg = "";
            this.lbAgeThree.IsDefaultCHInput = false;
            this.lbAgeThree.IsTagInput = false;
            this.lbAgeThree.IsTextInput = false;
            this.lbAgeThree.Location = new System.Drawing.Point(386, 42);
            this.lbAgeThree.Name = "lbAgeThree";
            this.lbAgeThree.Size = new System.Drawing.Size(17, 12);
            this.lbAgeThree.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lbAgeThree.TabIndex = 207;
            this.lbAgeThree.Text = "天";
            // 
            // lbAgeTwo
            // 
            this.lbAgeTwo.AutoSize = true;
            this.lbAgeTwo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAgeTwo.ForeColor = System.Drawing.Color.Blue;
            this.lbAgeTwo.InputMsg = "";
            this.lbAgeTwo.IsDefaultCHInput = false;
            this.lbAgeTwo.IsTagInput = false;
            this.lbAgeTwo.IsTextInput = false;
            this.lbAgeTwo.Location = new System.Drawing.Point(337, 42);
            this.lbAgeTwo.Name = "lbAgeTwo";
            this.lbAgeTwo.Size = new System.Drawing.Size(17, 12);
            this.lbAgeTwo.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lbAgeTwo.TabIndex = 206;
            this.lbAgeTwo.Text = "月";
            // 
            // lbAgeOne
            // 
            this.lbAgeOne.AutoSize = true;
            this.lbAgeOne.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAgeOne.ForeColor = System.Drawing.Color.Blue;
            this.lbAgeOne.InputMsg = "";
            this.lbAgeOne.IsDefaultCHInput = false;
            this.lbAgeOne.IsTagInput = false;
            this.lbAgeOne.IsTextInput = false;
            this.lbAgeOne.Location = new System.Drawing.Point(291, 42);
            this.lbAgeOne.Name = "lbAgeOne";
            this.lbAgeOne.Size = new System.Drawing.Size(17, 12);
            this.lbAgeOne.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lbAgeOne.TabIndex = 205;
            this.lbAgeOne.Text = "岁";
            // 
            // txtAgeThree
            // 
            this.txtAgeThree.AllowNegative = false;
            this.txtAgeThree.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAgeThree.IsAutoRemoveDecimalZero = true;
            this.txtAgeThree.IsEnter2Tab = false;
            this.txtAgeThree.Location = new System.Drawing.Point(357, 37);
            this.txtAgeThree.MaxLength = 2;
            this.txtAgeThree.Name = "txtAgeThree";
            this.txtAgeThree.NumericPrecision = 10;
            this.txtAgeThree.NumericScaleOnFocus = 0;
            this.txtAgeThree.NumericScaleOnLostFocus = 0;
            this.txtAgeThree.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAgeThree.SetRange = new System.Drawing.Size(-1, -1);
            this.txtAgeThree.Size = new System.Drawing.Size(25, 21);
            this.txtAgeThree.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAgeThree.TabIndex = 4;
            this.txtAgeThree.TabStop = false;
            this.txtAgeThree.Text = "1";
            this.txtAgeThree.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAgeThree.UseGroupSeperator = true;
            this.txtAgeThree.ZeroIsValid = true;
            // 
            // txtAgeTwo
            // 
            this.txtAgeTwo.AllowNegative = false;
            this.txtAgeTwo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAgeTwo.IsAutoRemoveDecimalZero = true;
            this.txtAgeTwo.IsEnter2Tab = false;
            this.txtAgeTwo.Location = new System.Drawing.Point(309, 37);
            this.txtAgeTwo.MaxLength = 2;
            this.txtAgeTwo.Name = "txtAgeTwo";
            this.txtAgeTwo.NumericPrecision = 2;
            this.txtAgeTwo.NumericScaleOnFocus = 0;
            this.txtAgeTwo.NumericScaleOnLostFocus = 0;
            this.txtAgeTwo.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAgeTwo.SetRange = new System.Drawing.Size(-1, -1);
            this.txtAgeTwo.Size = new System.Drawing.Size(25, 21);
            this.txtAgeTwo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAgeTwo.TabIndex = 4;
            this.txtAgeTwo.TabStop = false;
            this.txtAgeTwo.Text = "1";
            this.txtAgeTwo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAgeTwo.UseGroupSeperator = true;
            this.txtAgeTwo.ZeroIsValid = true;
            // 
            // txtAgeOne
            // 
            this.txtAgeOne.AllowNegative = false;
            this.txtAgeOne.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAgeOne.IsAutoRemoveDecimalZero = false;
            this.txtAgeOne.IsEnter2Tab = false;
            this.txtAgeOne.Location = new System.Drawing.Point(263, 37);
            this.txtAgeOne.MaxLength = 3;
            this.txtAgeOne.Name = "txtAgeOne";
            this.txtAgeOne.NumericPrecision = 3;
            this.txtAgeOne.NumericScaleOnFocus = 0;
            this.txtAgeOne.NumericScaleOnLostFocus = 0;
            this.txtAgeOne.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAgeOne.SetRange = new System.Drawing.Size(-1, -1);
            this.txtAgeOne.Size = new System.Drawing.Size(25, 21);
            this.txtAgeOne.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAgeOne.TabIndex = 4;
            this.txtAgeOne.TabStop = false;
            this.txtAgeOne.Text = "1";
            this.txtAgeOne.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAgeOne.UseGroupSeperator = true;
            this.txtAgeOne.ZeroIsValid = true;
            // 
            // txtHomeZip
            // 
            this.txtHomeZip.EnterVisiable = true;
            this.txtHomeZip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHomeZip.InputMsg = "邮编不能为空！";
            this.txtHomeZip.IsDefaultCHInput = false;
            this.txtHomeZip.IsFind = true;
            this.txtHomeZip.IsTagInput = false;
            this.txtHomeZip.IsTextInput = false;
            this.txtHomeZip.ListBoxHeight = 100;
            this.txtHomeZip.ListBoxWidth = 100;
            this.txtHomeZip.Location = new System.Drawing.Point(90, 147);
            this.txtHomeZip.Name = "txtHomeZip";
            this.txtHomeZip.OmitFilter = true;
            this.txtHomeZip.SelectedItem = null;
            this.txtHomeZip.SelectNone = false;
            this.txtHomeZip.ShowID = true;
            this.txtHomeZip.Size = new System.Drawing.Size(80, 21);
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
            this.cmbBirthArea.IsShowIDAndName = false;
            this.cmbBirthArea.IsTagInput = false;
            this.cmbBirthArea.IsTextInput = false;
            this.cmbBirthArea.Location = new System.Drawing.Point(619, 34);
            this.cmbBirthArea.Name = "cmbBirthArea";
            this.cmbBirthArea.ShowCustomerList = false;
            this.cmbBirthArea.ShowID = false;
            this.cmbBirthArea.Size = new System.Drawing.Size(74, 20);
            this.cmbBirthArea.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
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
            this.cmbPact.IsShowIDAndName = false;
            this.cmbPact.IsTagInput = false;
            this.cmbPact.IsTextInput = false;
            this.cmbPact.Location = new System.Drawing.Point(90, 259);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(144, 20);
            this.cmbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
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
            this.cmbProfession.IsShowIDAndName = false;
            this.cmbProfession.IsTagInput = false;
            this.cmbProfession.IsTextInput = false;
            this.cmbProfession.Location = new System.Drawing.Point(308, 65);
            this.cmbProfession.Name = "cmbProfession";
            this.cmbProfession.ShowCustomerList = false;
            this.cmbProfession.ShowID = false;
            this.cmbProfession.Size = new System.Drawing.Size(132, 20);
            this.cmbProfession.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
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
            this.cmbMarry.IsShowIDAndName = false;
            this.cmbMarry.IsTagInput = false;
            this.cmbMarry.IsTextInput = true;
            this.cmbMarry.Location = new System.Drawing.Point(91, 65);
            this.cmbMarry.Name = "cmbMarry";
            this.cmbMarry.ShowCustomerList = false;
            this.cmbMarry.ShowID = false;
            this.cmbMarry.Size = new System.Drawing.Size(166, 20);
            this.cmbMarry.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
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
            this.cmbCountry.IsShowIDAndName = false;
            this.cmbCountry.IsTagInput = false;
            this.cmbCountry.IsTextInput = false;
            this.cmbCountry.Location = new System.Drawing.Point(736, 34);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.ShowCustomerList = false;
            this.cmbCountry.ShowID = false;
            this.cmbCountry.Size = new System.Drawing.Size(144, 20);
            this.cmbCountry.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCountry.TabIndex = 6;
            this.cmbCountry.Tag = "";
            this.cmbCountry.ToolBarUse = false;
            // 
            // txtRealInvoiceNO
            // 
            this.txtRealInvoiceNO.IsEnter2Tab = false;
            this.txtRealInvoiceNO.Location = new System.Drawing.Point(529, 371);
            this.txtRealInvoiceNO.Name = "txtRealInvoiceNO";
            this.txtRealInvoiceNO.Size = new System.Drawing.Size(131, 21);
            this.txtRealInvoiceNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRealInvoiceNO.TabIndex = 49;
            // 
            // lbRealInvoiceNO
            // 
            this.lbRealInvoiceNO.AutoSize = true;
            this.lbRealInvoiceNO.Location = new System.Drawing.Point(446, 376);
            this.lbRealInvoiceNO.Name = "lbRealInvoiceNO";
            this.lbRealInvoiceNO.Size = new System.Drawing.Size(83, 12);
            this.lbRealInvoiceNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRealInvoiceNO.TabIndex = 201;
            this.lbRealInvoiceNO.Text = "预交金印刷号:";
            // 
            // btnUpdateRealInvoiceNO
            // 
            this.btnUpdateRealInvoiceNO.Location = new System.Drawing.Point(673, 369);
            this.btnUpdateRealInvoiceNO.Name = "btnUpdateRealInvoiceNO";
            this.btnUpdateRealInvoiceNO.Size = new System.Drawing.Size(54, 23);
            this.btnUpdateRealInvoiceNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnUpdateRealInvoiceNO.TabIndex = 200;
            this.btnUpdateRealInvoiceNO.Text = "更新";
            this.btnUpdateRealInvoiceNO.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
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
            this.cmbOverLop.IsShowIDAndName = false;
            this.cmbOverLop.Location = new System.Drawing.Point(308, 346);
            this.cmbOverLop.Name = "cmbOverLop";
            this.cmbOverLop.ShowCustomerList = false;
            this.cmbOverLop.ShowID = false;
            this.cmbOverLop.Size = new System.Drawing.Size(132, 20);
            this.cmbOverLop.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbOverLop.TabIndex = 44;
            this.cmbOverLop.TabStop = false;
            this.cmbOverLop.Tag = "";
            this.cmbOverLop.ToolBarUse = false;
            this.cmbOverLop.Enter += new System.EventHandler(this.cmbOverLop_Enter);
            this.cmbOverLop.Leave += new System.EventHandler(this.cmbOverLop_Leave);
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
            this.lblOverLop.Location = new System.Drawing.Point(247, 351);
            this.lblOverLop.Name = "lblOverLop";
            this.lblOverLop.Size = new System.Drawing.Size(59, 12);
            this.lblOverLop.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.cmbBedOverDeal.IsShowIDAndName = false;
            this.cmbBedOverDeal.Location = new System.Drawing.Point(90, 346);
            this.cmbBedOverDeal.Name = "cmbBedOverDeal";
            this.cmbBedOverDeal.ShowCustomerList = false;
            this.cmbBedOverDeal.ShowID = false;
            this.cmbBedOverDeal.Size = new System.Drawing.Size(144, 20);
            this.cmbBedOverDeal.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbBedOverDeal.TabIndex = 43;
            this.cmbBedOverDeal.TabStop = false;
            this.cmbBedOverDeal.Tag = "";
            this.cmbBedOverDeal.ToolBarUse = false;
            this.cmbBedOverDeal.Enter += new System.EventHandler(this.cmbBedOverDeal_Enter);
            this.cmbBedOverDeal.Leave += new System.EventHandler(this.cmbBedOverDeal_Leave);
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
            this.lblBedOverDeal.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblBedOverDeal.TabIndex = 198;
            this.lblBedOverDeal.Text = "超标处理:";
            // 
            // vtbRate
            // 
            this.vtbRate.AllowNegative = false;
            this.vtbRate.AutoPadRightZero = true;
            this.vtbRate.Enabled = false;
            this.vtbRate.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.vtbRate.Location = new System.Drawing.Point(736, 345);
            this.vtbRate.MaxDigits = 2;
            this.vtbRate.MaxLength = 8;
            this.vtbRate.Name = "vtbRate";
            this.vtbRate.Size = new System.Drawing.Size(144, 21);
            this.vtbRate.TabIndex = 42;
            this.vtbRate.Text = "0.00";
            this.vtbRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.vtbRate.WillShowError = false;
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Location = new System.Drawing.Point(674, 351);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(59, 12);
            this.lblRate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblRate.TabIndex = 197;
            this.lblRate.Text = "自负比例:";
            // 
            // rbtnTempPatientNO
            // 
            this.rbtnTempPatientNO.AutoSize = true;
            this.rbtnTempPatientNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnTempPatientNO.ForeColor = System.Drawing.Color.Blue;
            this.rbtnTempPatientNO.Location = new System.Drawing.Point(571, 287);
            this.rbtnTempPatientNO.Name = "rbtnTempPatientNO";
            this.rbtnTempPatientNO.Size = new System.Drawing.Size(83, 16);
            this.rbtnTempPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.neuInputLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.txtClinicNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtClinicNO.InputMsg = "门诊号不能为空";
            this.txtClinicNO.IsDefaultCHInput = false;
            this.txtClinicNO.IsEnter2Tab = false;
            this.txtClinicNO.IsTagInput = false;
            this.txtClinicNO.IsTextInput = true;
            this.txtClinicNO.Location = new System.Drawing.Point(90, 7);
            this.txtClinicNO.Name = "txtClinicNO";
            this.txtClinicNO.Size = new System.Drawing.Size(142, 21);
            this.txtClinicNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.cmbSex.Location = new System.Drawing.Point(527, 33);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.OmitFilter = true;
            this.cmbSex.SelectedItem = null;
            this.cmbSex.SelectNone = true;
            this.cmbSex.ShowID = true;
            this.cmbSex.Size = new System.Drawing.Size(42, 21);
            this.cmbSex.TabIndex = 3;
            this.cmbSex.Tag = "";
            this.cmbSex.TextChanged += new System.EventHandler(this.cmbSex_TextChanged);
            this.cmbSex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSex_KeyDown);
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
            this.txtIDNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.lblDoctDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.lblBirthday.Location = new System.Drawing.Point(27, 42);
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
            this.lblSex.Location = new System.Drawing.Point(485, 38);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(35, 12);
            this.lblSex.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.txtBirthArea.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBirthArea.TabIndex = 11;
            // 
            // cmbNation
            // 
            this.cmbNation.EnterVisiable = true;
            this.cmbNation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbNation.InputMsg = "";
            this.cmbNation.IsDefaultCHInput = false;
            this.cmbNation.IsFind = false;
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
            this.txtWorkAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.txtWorkPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.txtHomePhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.txtAddressNow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.txtHomeAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHomeAddress.TabIndex = 17;
            // 
            // txtLinkMan
            // 
            this.txtLinkMan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLinkMan.InputMsg = "请填写联系人";
            this.txtLinkMan.IsDefaultCHInput = false;
            this.txtLinkMan.IsEnter2Tab = false;
            this.txtLinkMan.IsTagInput = false;
            this.txtLinkMan.IsTextInput = false;
            this.txtLinkMan.Location = new System.Drawing.Point(309, 147);
            this.txtLinkMan.MaxLength = 0;
            this.txtLinkMan.Name = "txtLinkMan";
            this.txtLinkMan.Size = new System.Drawing.Size(131, 21);
            this.txtLinkMan.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.txtLinkPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.txtLinkAddr.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLinkAddr.TabIndex = 22;
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMemo.InputMsg = "";
            this.txtMemo.IsDefaultCHInput = true;
            this.txtMemo.IsEnter2Tab = false;
            this.txtMemo.IsTagInput = false;
            this.txtMemo.IsTextInput = false;
            this.txtMemo.Location = new System.Drawing.Point(90, 286);
            this.txtMemo.MaxLength = 0;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(350, 21);
            this.txtMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMemo.TabIndex = 47;
            // 
            // txtDiagnose
            // 
            this.txtDiagnose.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.txtDiagnose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtDiagnose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtDiagnose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDiagnose.InputMsg = "请填写门诊诊断";
            this.txtDiagnose.IsDefaultCHInput = false;
            this.txtDiagnose.IsEnter2Tab = false;
            this.txtDiagnose.IsFind = false;
            this.txtDiagnose.IsFlat = false;
            this.txtDiagnose.IsLike = true;
            this.txtDiagnose.IsListOnly = false;
            this.txtDiagnose.IsPopForm = true;
            this.txtDiagnose.IsShowCustomerList = false;
            this.txtDiagnose.IsShowID = false;
            this.txtDiagnose.IsShowIDAndName = false;
            this.txtDiagnose.IsTagInput = false;
            this.txtDiagnose.IsTextInput = false;
            this.txtDiagnose.Location = new System.Drawing.Point(527, 174);
            this.txtDiagnose.Name = "txtDiagnose";
            this.txtDiagnose.ShowCustomerList = false;
            this.txtDiagnose.ShowID = false;
            this.txtDiagnose.Size = new System.Drawing.Size(353, 20);
            this.txtDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.txtDiagnose.TabIndex = 23;
            this.txtDiagnose.Tag = "";
            this.txtDiagnose.ToolBarUse = false;
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
            this.cmbNurseCell.BackColor = System.Drawing.SystemColors.ControlLightLight;
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
            this.txtMCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMCardNO.TabIndex = 33;
            // 
            // mTxtIntimes
            // 
            this.mTxtIntimes.AllowNegative = false;
            this.mTxtIntimes.Enabled = false;
            this.mTxtIntimes.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mTxtIntimes.IsAutoRemoveDecimalZero = false;
            this.mTxtIntimes.IsEnter2Tab = false;
            this.mTxtIntimes.Location = new System.Drawing.Point(527, 257);
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
            this.mTxtIntimes.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.mTxtIntimes.TabIndex = 36;
            this.mTxtIntimes.TabStop = false;
            this.mTxtIntimes.Text = "1";
            this.mTxtIntimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mTxtIntimes.UseGroupSeperator = true;
            this.mTxtIntimes.ZeroIsValid = true;
            // 
            // dtpBirthDay
            // 
            this.dtpBirthDay.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dtpBirthDay.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBirthDay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpBirthDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBirthDay.IsEnter2Tab = false;
            this.dtpBirthDay.Location = new System.Drawing.Point(90, 37);
            this.dtpBirthDay.Name = "dtpBirthDay";
            this.dtpBirthDay.Size = new System.Drawing.Size(167, 21);
            this.dtpBirthDay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBirthDay.TabIndex = 45;
            // 
            // dtpInTime
            // 
            this.dtpInTime.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dtpInTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpInTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInTime.IsEnter2Tab = false;
            this.dtpInTime.Location = new System.Drawing.Point(738, 257);
            this.dtpInTime.Name = "dtpInTime";
            this.dtpInTime.Size = new System.Drawing.Size(144, 21);
            this.dtpInTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.txtInpatientNO.Location = new System.Drawing.Point(674, 284);
            this.txtInpatientNO.MaxLength = 10;
            this.txtInpatientNO.Name = "txtInpatientNO";
            this.txtInpatientNO.Size = new System.Drawing.Size(206, 21);
            this.txtInpatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
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
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuLabel1.TabIndex = 188;
            this.neuLabel1.Text = "家庭电话:";
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
            this.lbAddressNow.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblInTimes.Location = new System.Drawing.Point(461, 262);
            this.lblInTimes.Name = "lblInTimes";
            this.lblInTimes.Size = new System.Drawing.Size(59, 12);
            this.lblInTimes.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblInTimes.TabIndex = 179;
            this.lblInTimes.Text = "住院次数:";
            // 
            // lblDiagnose
            // 
            this.lblDiagnose.AutoSize = true;
            this.lblDiagnose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiagnose.ForeColor = System.Drawing.Color.Black;
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
            this.lblLinkPhone.InputMsg = "";
            this.lblLinkPhone.IsDefaultCHInput = false;
            this.lblLinkPhone.IsTagInput = false;
            this.lblLinkPhone.IsTextInput = false;
            this.lblLinkPhone.Location = new System.Drawing.Point(662, 152);
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
            // neuInputLabel3
            // 
            this.neuInputLabel3.AutoSize = true;
            this.neuInputLabel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuInputLabel3.InputMsg = "";
            this.neuInputLabel3.IsDefaultCHInput = false;
            this.neuInputLabel3.IsTagInput = false;
            this.neuInputLabel3.IsTextInput = false;
            this.neuInputLabel3.Location = new System.Drawing.Point(26, 291);
            this.neuInputLabel3.Name = "neuInputLabel3";
            this.neuInputLabel3.Size = new System.Drawing.Size(59, 12);
            this.neuInputLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuInputLabel3.TabIndex = 170;
            this.neuInputLabel3.Text = "备注(F3):";
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
            this.lblRelation.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblRelation.TabIndex = 170;
            this.lblRelation.Text = "关系:";
            // 
            // lblLinkMan
            // 
            this.lblLinkMan.AutoSize = true;
            this.lblLinkMan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLinkMan.ForeColor = System.Drawing.Color.Black;
            this.lblLinkMan.InputMsg = "";
            this.lblLinkMan.IsDefaultCHInput = false;
            this.lblLinkMan.IsTagInput = false;
            this.lblLinkMan.IsTextInput = false;
            this.lblLinkMan.Location = new System.Drawing.Point(261, 152);
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
            this.lblHomeAddress.InputMsg = "";
            this.lblHomeAddress.IsDefaultCHInput = false;
            this.lblHomeAddress.IsTagInput = false;
            this.lblHomeAddress.IsTextInput = false;
            this.lblHomeAddress.Location = new System.Drawing.Point(461, 124);
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
            this.lblWorkAddress.InputMsg = "";
            this.lblWorkAddress.IsDefaultCHInput = false;
            this.lblWorkAddress.IsTagInput = false;
            this.lblWorkAddress.IsTextInput = false;
            this.lblWorkAddress.Location = new System.Drawing.Point(26, 96);
            this.lblWorkAddress.Name = "lblWorkAddress";
            this.lblWorkAddress.Size = new System.Drawing.Size(59, 12);
            this.lblWorkAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblNation.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblAirLimit.Location = new System.Drawing.Point(461, 351);
            this.lblAirLimit.Name = "lblAirLimit";
            this.lblAirLimit.Size = new System.Drawing.Size(59, 12);
            this.lblAirLimit.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblBedLimit.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
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
            this.lblDayLimit.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.lblDayLimit.TabIndex = 129;
            this.lblDayLimit.Text = "公费日限:";
            // 
            // txtAirLimit
            // 
            this.txtAirLimit.AllowNegative = false;
            this.txtAirLimit.AutoPadRightZero = true;
            this.txtAirLimit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAirLimit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtAirLimit.Location = new System.Drawing.Point(527, 340);
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
            this.lblInTime.Location = new System.Drawing.Point(672, 262);
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
            this.rdoInpatientNO.Location = new System.Drawing.Point(482, 287);
            this.rdoInpatientNO.Name = "rdoInpatientNO";
            this.rdoInpatientNO.Size = new System.Drawing.Size(83, 16);
            this.rdoInpatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rdoInpatientNO.TabIndex = 46;
            this.rdoInpatientNO.TabStop = true;
            this.rdoInpatientNO.Text = "住院号(F1)";
            this.rdoInpatientNO.UseVisualStyleBackColor = false;
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

        protected FS.FrameWork.WinForms.Controls.NeuPanel plInfomation;
        private NeuInputLabel lblDoctDept;
        private NeuInputListTextBox cmbSex;
        protected FS.FrameWork.WinForms.Controls.NeuCheckBox chbencrypt;
        protected NeuInputTextBox txtIDNO;
        protected NeuInputLabel lblIDNO;
        protected NeuInputLabel lblBirthday;
        protected NeuInputLabel lblSex;
        protected NeuInputLabel lblName;
        protected NeuInputTextBox txtClinicNO;
        protected FS.FrameWork.WinForms.Controls.NeuContextMenuStrip neuContextMenuStrip1;
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
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox mTxtIntimes;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox mtxtBloodFee;
        protected NeuInputLabel lblInTimes;
        protected NeuInputLabel lblBloodFee;
        protected NeuCombox txtDiagnose;
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
        protected NeuInputLabel neuInputLabel2;
        protected FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnTempPatientNO;
        protected NeuInputLabel lblDayLimit;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtDayLimit;
        protected NeuInputLabel lblBedLimit;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtBedLimit;
        protected NeuInputLabel lblAirLimit;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtAirLimit;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblRate;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox vtbRate;
        protected NeuInputLabel lblBedOverDeal;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbBedOverDeal;
        protected NeuInputLabel lblOverLop;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOverLop;
        private FS.FrameWork.WinForms.Controls.NeuButton btnUpdateRealInvoiceNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbRealInvoiceNO;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtRealInvoiceNO;
        private global::FS.SOC.Local.RADT.ZhuHai.ZDWY.Common.cmbTransType cmbPayType;
        private NeuCombox cmbProfession;
        private NeuCombox cmbMarry;
        private NeuCombox cmbCountry;
        private NeuCombox cmbPact;
        private NeuCombox cmbBirthArea;
        private NeuInputListTextBox txtHomeZip;
        protected NeuInputLabel lbAgeThree;
        protected NeuInputLabel lbAgeTwo;
        protected NeuInputLabel lbAgeOne;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtAgeThree;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtAgeTwo;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtAgeOne;
        protected FS.FrameWork.WinForms.Controls.NeuCheckBox ckHour;
        protected NeuInputTextBox txtMemo;
        protected NeuInputLabel neuInputLabel3;
        private NeuCombox cmbPatientType;
        protected NeuInputLabel neuInputLabel4;
        protected NeuInputLabel lblComputerNO;
        private CheckBox cbxChild;
        protected FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBirthDay;

    }
}