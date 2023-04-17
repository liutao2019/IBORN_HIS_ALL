namespace FS.SOC.HISFC.Components.DCP.Controls
{
    partial class ucMyReport
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
            this.lsvMemo = new FS.FrameWork.WinForms.Controls.NeuListView();
            this.btnEnter = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.panelButtom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panelButtom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvMemo
            // 
            this.lsvMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvMemo.CheckBoxes = true;
            this.lsvMemo.Location = new System.Drawing.Point(0, 0);
            this.lsvMemo.Name = "lsvMemo";
            this.lsvMemo.Size = new System.Drawing.Size(201, 257);
            this.lsvMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lsvMemo.TabIndex = 0;
            this.lsvMemo.UseCompatibleStateImageBehavior = false;
            this.lsvMemo.View = System.Windows.Forms.View.List;
            this.lsvMemo.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lsvMemo_ItemCheck);
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(12, 3);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(75, 23);
            this.btnEnter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnEnter.TabIndex = 1;
            this.btnEnter.Text = "确定";
            this.btnEnter.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(117, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelButtom
            // 
            this.panelButtom.BackColor = System.Drawing.Color.White;
            this.panelButtom.Controls.Add(this.btnCancel);
            this.panelButtom.Controls.Add(this.btnEnter);
            this.panelButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtom.Location = new System.Drawing.Point(0, 260);
            this.panelButtom.Name = "panelButtom";
            this.panelButtom.Size = new System.Drawing.Size(201, 28);
            this.panelButtom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelButtom.TabIndex = 3;
            // 
            // ucMyReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelButtom);
            this.Controls.Add(this.lsvMemo);
            this.Name = "ucMyReport";
            this.Size = new System.Drawing.Size(201, 288);
            this.panelButtom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuListView lsvMemo;
        private FS.FrameWork.WinForms.Controls.NeuButton btnEnter;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuPanel panelButtom;

    }
}
