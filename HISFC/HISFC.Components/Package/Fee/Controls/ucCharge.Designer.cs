namespace HISFC.Components.Package.Fee.Controls
{
    partial class ucCharge
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
            this.plPatientInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plPayInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plFeeInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucPackageSelector1 = new HISFC.Components.Package.Fee.Controls.ucPackageSelector();
            this.SuspendLayout();
            // 
            // plPatientInfo
            // 
            this.plPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.plPatientInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plPatientInfo.Location = new System.Drawing.Point(0, 0);
            this.plPatientInfo.Name = "plPatientInfo";
            this.plPatientInfo.Size = new System.Drawing.Size(722, 136);
            this.plPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plPatientInfo.TabIndex = 0;
            // 
            // plPayInfo
            // 
            this.plPayInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plPayInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plPayInfo.Location = new System.Drawing.Point(0, 262);
            this.plPayInfo.Name = "plPayInfo";
            this.plPayInfo.Size = new System.Drawing.Size(722, 258);
            this.plPayInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plPayInfo.TabIndex = 2;
            // 
            // plFeeInfo
            // 
            this.plFeeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFeeInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plFeeInfo.Location = new System.Drawing.Point(0, 136);
            this.plFeeInfo.Name = "plFeeInfo";
            this.plFeeInfo.Size = new System.Drawing.Size(722, 126);
            this.plFeeInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plFeeInfo.TabIndex = 3;
            // 
            // ucPackageSelector1
            // 
            this.ucPackageSelector1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucPackageSelector1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPackageSelector1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucPackageSelector1.IsFullConvertToHalf = true;
            this.ucPackageSelector1.IsPrint = false;
            this.ucPackageSelector1.Location = new System.Drawing.Point(94, 187);
            this.ucPackageSelector1.Name = "ucPackageSelector1";
            this.ucPackageSelector1.Padding = new System.Windows.Forms.Padding(1);
            this.ucPackageSelector1.ParentFormToolBar = null;
            this.ucPackageSelector1.Size = new System.Drawing.Size(800, 261);
            this.ucPackageSelector1.TabIndex = 4;
            this.ucPackageSelector1.Visible = false;
            // 
            // ucCharge
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.Controls.Add(this.ucPackageSelector1);
            this.Controls.Add(this.plFeeInfo);
            this.Controls.Add(this.plPayInfo);
            this.Controls.Add(this.plPatientInfo);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Name = "ucCharge";
            this.Size = new System.Drawing.Size(722, 520);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel plPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuPanel plPayInfo;
        private FS.FrameWork.WinForms.Controls.NeuPanel plFeeInfo;
        private ucPackageSelector ucPackageSelector1;






    }
}
