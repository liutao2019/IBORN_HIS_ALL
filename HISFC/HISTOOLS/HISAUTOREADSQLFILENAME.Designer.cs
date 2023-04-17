namespace HISTOOLS
{
    partial class HISAUTOREADSQLFILENAME
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nbtCheckDir = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nlbCurDir = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nbtGenerateSql = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.ntbGenerateSql = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // nbtCheckDir
            // 
            this.nbtCheckDir.Location = new System.Drawing.Point(8, 12);
            this.nbtCheckDir.Name = "nbtCheckDir";
            this.nbtCheckDir.Size = new System.Drawing.Size(75, 23);
            this.nbtCheckDir.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCheckDir.TabIndex = 0;
            this.nbtCheckDir.Text = "选择路径";
            this.nbtCheckDir.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCheckDir.UseVisualStyleBackColor = true;
            // 
            // nlbCurDir
            // 
            this.nlbCurDir.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbCurDir.ForeColor = System.Drawing.Color.Blue;
            this.nlbCurDir.Location = new System.Drawing.Point(7, 38);
            this.nlbCurDir.Name = "nlbCurDir";
            this.nlbCurDir.Size = new System.Drawing.Size(287, 37);
            this.nlbCurDir.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbCurDir.TabIndex = 1;
            this.nlbCurDir.Text = "当前路径:E:\\中大五院\\程序更新\\2014-07-05\\脚本";
            this.nlbCurDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nbtGenerateSql
            // 
            this.nbtGenerateSql.Location = new System.Drawing.Point(9, 74);
            this.nbtGenerateSql.Name = "nbtGenerateSql";
            this.nbtGenerateSql.Size = new System.Drawing.Size(75, 23);
            this.nbtGenerateSql.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtGenerateSql.TabIndex = 2;
            this.nbtGenerateSql.Text = "生成脚本";
            this.nbtGenerateSql.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtGenerateSql.UseVisualStyleBackColor = true;
            // 
            // ntbGenerateSql
            // 
            this.ntbGenerateSql.IsEnter2Tab = false;
            this.ntbGenerateSql.Location = new System.Drawing.Point(8, 103);
            this.ntbGenerateSql.Multiline = true;
            this.ntbGenerateSql.Name = "ntbGenerateSql";
            this.ntbGenerateSql.Size = new System.Drawing.Size(479, 317);
            this.ntbGenerateSql.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbGenerateSql.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // HISAUTOREADSQLFILENAME
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 425);
            this.Controls.Add(this.ntbGenerateSql);
            this.Controls.Add(this.nbtGenerateSql);
            this.Controls.Add(this.nlbCurDir);
            this.Controls.Add(this.nbtCheckDir);
            this.KeyPreview = true;
            this.Name = "HISAUTOREADSQLFILENAME";
            this.Text = "脚本生成工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuButton nbtCheckDir;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbCurDir;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtGenerateSql;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntbGenerateSql;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}