//using Neusoft.WinForms;
//using Neusoft.WinForms.Controls;
namespace Neusoft.Privilege.WinForms
{
    partial class ZZZZAddMenuForm
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMenuName = new NFC.Interface.Controls.NeuTextBox();
            this.txtParentName = new NFC.Interface.Controls.NeuTextBox();
            this.nLabel2 = new NFC.Interface.Controls.NeuLabel();
            this.cmbShortcut = new NFC.Interface.Controls.NeuComboBox();
            this.nLabel4 = new NFC.Interface.Controls.NeuLabel();
            this.txtTooltips = new NFC.Interface.Controls.NeuTextBox();
            this.nLabel5 = new NFC.Interface.Controls.NeuLabel();
            this.txtDllName = new NFC.Interface.Controls.NeuTextBox();
            this.nLabel6 = new NFC.Interface.Controls.NeuLabel();
            this.txtWinName = new NFC.Interface.Controls.NeuTextBox();
            this.nLabel7 = new NFC.Interface.Controls.NeuLabel();
            this.nLabel8 = new NFC.Interface.Controls.NeuLabel();
            this.txtParam = new NFC.Interface.Controls.NeuTextBox();
            this.nLabel9 = new NFC.Interface.Controls.NeuLabel();
            this.cmbControlType = new NFC.Interface.Controls.NeuComboBox();
            this.nLabel10 = new NFC.Interface.Controls.NeuLabel();
            this.cmbShowType = new NFC.Interface.Controls.NeuComboBox();
            this.nLabel11 = new NFC.Interface.Controls.NeuLabel();
            this.chbEnabled = new NFC.Interface.Controls.NeuCheckBox();
            this.btnOK = new NFC.Interface.Controls.NeuButton();
            this.btnCancel = new NFC.Interface.Controls.NeuButton();
            this.txtTreeDllName = new NFC.Interface.Controls.NeuTextBox();
            this.txtTreeName = new NFC.Interface.Controls.NeuTextBox();
            this.nLabel3 = new NFC.Interface.Controls.NeuLabel();
            this.ContentPanel.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.nLabel3);
            this.ContentPanel.Controls.Add(this.txtTreeName);
            this.ContentPanel.Controls.Add(this.cmbControlType);
            this.ContentPanel.Controls.Add(this.txtTreeDllName);
            this.ContentPanel.Controls.Add(this.chbEnabled);
            this.ContentPanel.Controls.Add(this.txtParam);
            this.ContentPanel.Controls.Add(this.txtWinName);
            this.ContentPanel.Controls.Add(this.txtDllName);
            this.ContentPanel.Controls.Add(this.nLabel9);
            this.ContentPanel.Controls.Add(this.nLabel8);
            this.ContentPanel.Controls.Add(this.nLabel7);
            this.ContentPanel.Controls.Add(this.nLabel6);
            this.ContentPanel.Controls.Add(this.txtTooltips);
            this.ContentPanel.Controls.Add(this.nLabel5);
            this.ContentPanel.Controls.Add(this.nLabel4);
            this.ContentPanel.Controls.Add(this.cmbShowType);
            this.ContentPanel.Controls.Add(this.nLabel11);
            this.ContentPanel.Controls.Add(this.nLabel10);
            this.ContentPanel.Controls.Add(this.cmbShortcut);
            this.ContentPanel.Controls.Add(this.nLabel2);
            this.ContentPanel.Controls.Add(this.txtParentName);
            this.ContentPanel.Controls.Add(this.txtMenuName);
            this.ContentPanel.Size = new System.Drawing.Size(564, 302);
            this.ContentPanel.Controls.SetChildIndex(this.txtMenuName, 0);
            this.ContentPanel.Controls.SetChildIndex(this.txtParentName, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel2, 0);
            this.ContentPanel.Controls.SetChildIndex(this.cmbShortcut, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel10, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel11, 0);
            this.ContentPanel.Controls.SetChildIndex(this.cmbShowType, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel4, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel5, 0);
            this.ContentPanel.Controls.SetChildIndex(this.txtTooltips, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel6, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel7, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel8, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel9, 0);
            this.ContentPanel.Controls.SetChildIndex(this.txtDllName, 0);
            this.ContentPanel.Controls.SetChildIndex(this.txtWinName, 0);
            this.ContentPanel.Controls.SetChildIndex(this.txtParam, 0);
            this.ContentPanel.Controls.SetChildIndex(this.chbEnabled, 0);
            this.ContentPanel.Controls.SetChildIndex(this.txtTreeDllName, 0);
            this.ContentPanel.Controls.SetChildIndex(this.cmbControlType, 0);
            this.ContentPanel.Controls.SetChildIndex(this.txtTreeName, 0);
            this.ContentPanel.Controls.SetChildIndex(this.nLabel3, 0);
            // 
            // TitlePanel
            // 
            this.TitlePanel.Size = new System.Drawing.Size(564, 50);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.btnCancel);
            this.BottomPanel.Controls.Add(this.btnOK);
            this.BottomPanel.Location = new System.Drawing.Point(0, 352);
            this.BottomPanel.Size = new System.Drawing.Size(564, 48);
            // 
            // txtMenuName
            // 
            this.txtMenuName.IsEnter2Tab = true;
            this.txtMenuName.Location = new System.Drawing.Point(174, 20);
            this.txtMenuName.Name = "txtMenuName";
            this.txtMenuName.Size = new System.Drawing.Size(220, 21);
            this.txtMenuName.TabIndex = 0;
            // 
            // txtParentName
            // 
            this.txtParentName.Enabled = false;
            this.txtParentName.IsEnter2Tab = true;
            this.txtParentName.Location = new System.Drawing.Point(174, 47);
            this.txtParentName.Name = "txtParentName";
            this.txtParentName.Size = new System.Drawing.Size(220, 21);
            this.txtParentName.TabIndex = 1;
            // 
            // nLabel2
            // 
            this.nLabel2.AutoSize = true;
            this.nLabel2.Location = new System.Drawing.Point(90, 23);
            this.nLabel2.Name = "nLabel2";
            this.nLabel2.Size = new System.Drawing.Size(53, 12);
            this.nLabel2.TabIndex = 3;
            this.nLabel2.Text = "�˵�����";
            // 
            // cmbShortcut
            // 
            this.cmbShortcut.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbShortcut.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbShortcut.FormattingEnabled = true;
            this.cmbShortcut.IsColumnHeader = false;
            this.cmbShortcut.IsIllegibility = false;
            this.cmbShortcut.IsShowDataView = false;
            this.cmbShortcut.Location = new System.Drawing.Point(174, 74);
            this.cmbShortcut.Name = "cmbShortcut";
            this.cmbShortcut.NeuHeight = 209;
            this.cmbShortcut.Opacity = 1;
            this.cmbShortcut.Size = new System.Drawing.Size(159, 20);
            this.cmbShortcut.TabIndex = 2;
            this.cmbShortcut.UserCode = "CustomCode";
            // 
            // nLabel4
            // 
            this.nLabel4.AutoSize = true;
            this.nLabel4.Location = new System.Drawing.Point(102, 77);
            this.nLabel4.Name = "nLabel4";
            this.nLabel4.Size = new System.Drawing.Size(41, 12);
            this.nLabel4.TabIndex = 3;
            this.nLabel4.Text = "��ݼ�";
            // 
            // txtTooltips
            // 
            this.txtTooltips.IsEnter2Tab = true;
            this.txtTooltips.Location = new System.Drawing.Point(174, 100);
            this.txtTooltips.Name = "txtTooltips";
            this.txtTooltips.Size = new System.Drawing.Size(267, 21);
            this.txtTooltips.TabIndex = 3;
            // 
            // nLabel5
            // 
            this.nLabel5.AutoSize = true;
            this.nLabel5.Location = new System.Drawing.Point(90, 103);
            this.nLabel5.Name = "nLabel5";
            this.nLabel5.Size = new System.Drawing.Size(53, 12);
            this.nLabel5.TabIndex = 3;
            this.nLabel5.Text = "������ʾ";
            // 
            // txtDllName
            // 
            this.txtDllName.IsEnter2Tab = true;
            this.txtDllName.Location = new System.Drawing.Point(174, 127);
            this.txtDllName.Name = "txtDllName";
            this.txtDllName.Size = new System.Drawing.Size(267, 21);
            this.txtDllName.TabIndex = 4;
            // 
            // nLabel6
            // 
            this.nLabel6.AutoSize = true;
            this.nLabel6.Location = new System.Drawing.Point(54, 130);
            this.nLabel6.Name = "nLabel6";
            this.nLabel6.Size = new System.Drawing.Size(89, 12);
            this.nLabel6.TabIndex = 3;
            this.nLabel6.Text = "���ó�������";
            // 
            // txtWinName
            // 
            this.txtWinName.IsEnter2Tab = true;
            this.txtWinName.Location = new System.Drawing.Point(174, 154);
            this.txtWinName.Name = "txtWinName";
            this.txtWinName.Size = new System.Drawing.Size(267, 21);
            this.txtWinName.TabIndex = 5;
            // 
            // nLabel7
            // 
            this.nLabel7.AutoSize = true;
            this.nLabel7.Location = new System.Drawing.Point(66, 157);
            this.nLabel7.Name = "nLabel7";
            this.nLabel7.Size = new System.Drawing.Size(77, 12);
            this.nLabel7.TabIndex = 3;
            this.nLabel7.Text = "���ÿؼ�����";
            // 
            // nLabel8
            // 
            this.nLabel8.AutoSize = true;
            this.nLabel8.Location = new System.Drawing.Point(90, 237);
            this.nLabel8.Name = "nLabel8";
            this.nLabel8.Size = new System.Drawing.Size(53, 12);
            this.nLabel8.TabIndex = 3;
            this.nLabel8.Text = "�������";
            // 
            // txtParam
            // 
            this.txtParam.IsEnter2Tab = true;
            this.txtParam.Location = new System.Drawing.Point(174, 234);
            this.txtParam.Name = "txtParam";
            this.txtParam.Size = new System.Drawing.Size(267, 21);
            this.txtParam.TabIndex = 8;
            // 
            // nLabel9
            // 
            this.nLabel9.AutoSize = true;
            this.nLabel9.Location = new System.Drawing.Point(78, 210);
            this.nLabel9.Name = "nLabel9";
            this.nLabel9.Size = new System.Drawing.Size(65, 12);
            this.nLabel9.TabIndex = 3;
            this.nLabel9.Text = "���ؼ�����";
            // 
            // cmbControlType
            // 
            this.cmbControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbControlType.FormattingEnabled = true;
            this.cmbControlType.Items.AddRange(new object[] {
            "",
            "����",
            "�ؼ�",
            "����"});
            this.cmbControlType.Location = new System.Drawing.Point(496, 262);
            this.cmbControlType.Name = "cmbControlType";
            this.cmbControlType.NeuHeight = 209;
            this.cmbControlType.Opacity = 1;
            this.cmbControlType.Size = new System.Drawing.Size(121, 20);
            this.cmbControlType.TabIndex = 7;
            this.cmbControlType.UserCode = "CustomCode";
            this.cmbControlType.Visible = false;
            // 
            // nLabel10
            // 
            this.nLabel10.AutoSize = true;
            this.nLabel10.Location = new System.Drawing.Point(66, 50);
            this.nLabel10.Name = "nLabel10";
            this.nLabel10.Size = new System.Drawing.Size(77, 12);
            this.nLabel10.TabIndex = 3;
            this.nLabel10.Text = "�����˵�����";
            // 
            // cmbShowType
            // 
            this.cmbShowType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShowType.FormattingEnabled = true;
            this.cmbShowType.IsColumnHeader = false;
            this.cmbShowType.IsIllegibility = false;
            this.cmbShowType.IsShowDataView = false;
            this.cmbShowType.Items.AddRange(new object[] {
            "ģʽ����",
            "��ģʽ����",
            "Webҳ��"});
            this.cmbShowType.Location = new System.Drawing.Point(174, 261);
            this.cmbShowType.Name = "cmbShowType";
            this.cmbShowType.NeuHeight = 209;
            this.cmbShowType.Opacity = 1;
            this.cmbShowType.Size = new System.Drawing.Size(121, 20);
            this.cmbShowType.TabIndex = 9;
            this.cmbShowType.UserCode = "CustomCode";
            // 
            // nLabel11
            // 
            this.nLabel11.AutoSize = true;
            this.nLabel11.Location = new System.Drawing.Point(90, 264);
            this.nLabel11.Name = "nLabel11";
            this.nLabel11.Size = new System.Drawing.Size(53, 12);
            this.nLabel11.TabIndex = 3;
            this.nLabel11.Text = "��ʾ����";
            // 
            // chbEnabled
            // 
            this.chbEnabled.AutoSize = true;
            this.chbEnabled.Checked = true;
            this.chbEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbEnabled.Location = new System.Drawing.Point(322, 264);
            this.chbEnabled.Name = "chbEnabled";
            this.chbEnabled.Size = new System.Drawing.Size(72, 16);
            this.chbEnabled.TabIndex = 10;
            this.chbEnabled.Text = "�Ƿ����";
            this.chbEnabled.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(348, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "����(&S)";
            this.btnOK.Type = Neusoft.WinForms.ButtonType.Save;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(442, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "�ر�(&C)";
            this.btnCancel.Type = Neusoft.WinForms.ButtonType.Close;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtTreeDllName
            // 
            this.txtTreeDllName.IsEnter2Tab = true;
            this.txtTreeDllName.Location = new System.Drawing.Point(174, 180);
            this.txtTreeDllName.Name = "txtTreeDllName";
            this.txtTreeDllName.Size = new System.Drawing.Size(267, 21);
            this.txtTreeDllName.TabIndex = 6;
            // 
            // txtTreeName
            // 
            this.txtTreeName.IsEnter2Tab = true;
            this.txtTreeName.Location = new System.Drawing.Point(174, 207);
            this.txtTreeName.Name = "txtTreeName";
            this.txtTreeName.Size = new System.Drawing.Size(267, 21);
            this.txtTreeName.TabIndex = 7;
            // 
            // nLabel3
            // 
            this.nLabel3.AutoSize = true;
            this.nLabel3.Location = new System.Drawing.Point(42, 183);
            this.nLabel3.Name = "nLabel3";
            this.nLabel3.Size = new System.Drawing.Size(101, 12);
            this.nLabel3.TabIndex = 12;
            this.nLabel3.Text = "���ؼ���������";
            // 
            // AddMenuForm
            // 
            this.ClientSize = new System.Drawing.Size(564, 422);
            this.IsStatusStripVisible = true;
            this.Name = "AddMenuForm";
            this.Text = "���Ӳ˵�";
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NFC.Interface.Controls.NeuTextBox txtMenuName;
        private NFC.Interface.Controls.NeuLabel nLabel2;
        private NFC.Interface.Controls.NeuTextBox txtParentName;
        private NFC.Interface.Controls.NeuTextBox txtWinName;
        private NFC.Interface.Controls.NeuTextBox txtDllName;
        private NFC.Interface.Controls.NeuLabel nLabel6;
        private NFC.Interface.Controls.NeuTextBox txtTooltips;
        private NFC.Interface.Controls.NeuLabel nLabel5;
        private NFC.Interface.Controls.NeuLabel nLabel4;
        private NFC.Interface.Controls.NeuComboBox cmbShortcut;
        private NFC.Interface.Controls.NeuCheckBox chbEnabled;
        private NFC.Interface.Controls.NeuTextBox txtParam;
        private NFC.Interface.Controls.NeuLabel nLabel9;
        private NFC.Interface.Controls.NeuLabel nLabel8;
        private NFC.Interface.Controls.NeuLabel nLabel7;
        private NFC.Interface.Controls.NeuComboBox cmbControlType;
        private NFC.Interface.Controls.NeuComboBox cmbShowType;
        private NFC.Interface.Controls.NeuLabel nLabel11;
        private NFC.Interface.Controls.NeuLabel nLabel10;
        private NFC.Interface.Controls.NeuButton btnCancel;
        private NFC.Interface.Controls.NeuButton btnOK;
        private NFC.Interface.Controls.NeuTextBox txtTreeName;
        private NFC.Interface.Controls.NeuTextBox txtTreeDllName;
        private NFC.Interface.Controls.NeuLabel nLabel3;
    }
}
