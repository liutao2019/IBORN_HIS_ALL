namespace FS.SOC.HISFC.Assign.Components.Maintenance.Room
{
    partial class ucRoomManager
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
            this.gbAssignRoom = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lvAssignRoom = new FS.FrameWork.WinForms.Controls.NeuListView();
            this.gbAssignRoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAssignRoom
            // 
            this.gbAssignRoom.Controls.Add(this.lvAssignRoom);
            this.gbAssignRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAssignRoom.ForeColor = System.Drawing.Color.Blue;
            this.gbAssignRoom.Location = new System.Drawing.Point(0, 0);
            this.gbAssignRoom.Name = "gbAssignRoom";
            this.gbAssignRoom.Size = new System.Drawing.Size(150, 150);
            this.gbAssignRoom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbAssignRoom.TabIndex = 1;
            this.gbAssignRoom.TabStop = false;
            this.gbAssignRoom.Text = "分诊诊室";
            // 
            // lvAssignRoom
            // 
            this.lvAssignRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAssignRoom.HideSelection = false;
            this.lvAssignRoom.Location = new System.Drawing.Point(3, 17);
            this.lvAssignRoom.Name = "lvAssignRoom";
            this.lvAssignRoom.Size = new System.Drawing.Size(144, 130);
            this.lvAssignRoom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lvAssignRoom.TabIndex = 0;
            this.lvAssignRoom.UseCompatibleStateImageBehavior = false;
            // 
            // ucRoomManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbAssignRoom);
            this.Name = "ucRoomManager";
            this.gbAssignRoom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbAssignRoom;
        private FS.FrameWork.WinForms.Controls.NeuListView lvAssignRoom;
    }
}
