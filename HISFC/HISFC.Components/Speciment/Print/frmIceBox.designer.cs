namespace FS.HISFC.Components.Speciment.Print
{
    partial class frmIceBox
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.tvIceBox = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(470, 30);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExport.Location = new System.Drawing.Point(0, 30);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(470, 32);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tvIceBox
            // 
            this.tvIceBox.CheckBoxes = true;
            this.tvIceBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvIceBox.Location = new System.Drawing.Point(0, 62);
            this.tvIceBox.Name = "tvIceBox";
            this.tvIceBox.Size = new System.Drawing.Size(470, 526);
            this.tvIceBox.TabIndex = 5;
            // 
            // frmIceBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 588);
            this.Controls.Add(this.tvIceBox);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnPrint);
            this.Name = "frmIceBox";
            this.Text = "冰箱位置图打印";
            this.Load += new System.EventHandler(this.frmIceBox_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TreeView tvIceBox;

    }
}