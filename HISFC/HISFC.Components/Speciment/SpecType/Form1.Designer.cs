namespace WindowsApplication1
{
    partial class Form1
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listTable = new System.Windows.Forms.ListView();
            this.listCol = new System.Windows.Forms.ListView();
            this.listSelColumn = new System.Windows.Forms.ListView();
            this.btnAddCol = new System.Windows.Forms.Button();
            this.btnRemoveCol = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtShowField = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.flpCondition = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddCon = new System.Windows.Forms.Button();
            this.ucCondition1 = new UFC.Speciment.ucCondition();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.flpCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(133, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            // 
            // listTable
            // 
            this.listTable.Location = new System.Drawing.Point(28, 80);
            this.listTable.MultiSelect = false;
            this.listTable.Name = "listTable";
            this.listTable.Size = new System.Drawing.Size(166, 374);
            this.listTable.TabIndex = 4;
            this.listTable.UseCompatibleStateImageBehavior = false;
            this.listTable.View = System.Windows.Forms.View.List;
            this.listTable.SelectedIndexChanged += new System.EventHandler(this.listTable_SelectedIndexChanged);
            // 
            // listCol
            // 
            this.listCol.Location = new System.Drawing.Point(241, 80);
            this.listCol.Name = "listCol";
            this.listCol.Size = new System.Drawing.Size(139, 374);
            this.listCol.TabIndex = 5;
            this.listCol.UseCompatibleStateImageBehavior = false;
            this.listCol.View = System.Windows.Forms.View.List;
            // 
            // listSelColumn
            // 
            this.listSelColumn.Location = new System.Drawing.Point(433, 80);
            this.listSelColumn.Name = "listSelColumn";
            this.listSelColumn.Size = new System.Drawing.Size(163, 374);
            this.listSelColumn.TabIndex = 8;
            this.listSelColumn.UseCompatibleStateImageBehavior = false;
            this.listSelColumn.View = System.Windows.Forms.View.List;
            // 
            // btnAddCol
            // 
            this.btnAddCol.Location = new System.Drawing.Point(386, 207);
            this.btnAddCol.Name = "btnAddCol";
            this.btnAddCol.Size = new System.Drawing.Size(41, 23);
            this.btnAddCol.TabIndex = 9;
            this.btnAddCol.Text = ">>";
            this.btnAddCol.UseVisualStyleBackColor = true;
            this.btnAddCol.Click += new System.EventHandler(this.btnAddCol_Click);
            // 
            // btnRemoveCol
            // 
            this.btnRemoveCol.Location = new System.Drawing.Point(386, 264);
            this.btnRemoveCol.Name = "btnRemoveCol";
            this.btnRemoveCol.Size = new System.Drawing.Size(41, 23);
            this.btnRemoveCol.TabIndex = 10;
            this.btnRemoveCol.Text = "<<";
            this.btnRemoveCol.UseVisualStyleBackColor = true;
            this.btnRemoveCol.Click += new System.EventHandler(this.btnRemoveCol_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(621, 264);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(44, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtShowField
            // 
            this.txtShowField.Location = new System.Drawing.Point(28, 473);
            this.txtShowField.Multiline = true;
            this.txtShowField.Name = "txtShowField";
            this.txtShowField.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtShowField.Size = new System.Drawing.Size(568, 111);
            this.txtShowField.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 16;
            // 
            // flpCondition
            // 
            this.flpCondition.AutoScroll = true;
            this.flpCondition.Controls.Add(this.ucCondition1);
            this.flpCondition.Controls.Add(this.btnAddCon);
            this.flpCondition.Location = new System.Drawing.Point(22, 610);
            this.flpCondition.Name = "flpCondition";
            this.flpCondition.Size = new System.Drawing.Size(666, 195);
            this.flpCondition.TabIndex = 21;
            // 
            // btnAddCon
            // 
            this.btnAddCon.Location = new System.Drawing.Point(604, 3);
            this.btnAddCon.Name = "btnAddCon";
            this.btnAddCon.Size = new System.Drawing.Size(56, 23);
            this.btnAddCon.TabIndex = 28;
            this.btnAddCon.Text = "添加";
            this.btnAddCon.UseVisualStyleBackColor = true;
            this.btnAddCon.Click += new System.EventHandler(this.btnAddCon_Click);
            // 
            // ucCondition1
            // 
            this.ucCondition1.Location = new System.Drawing.Point(3, 3);
            this.ucCondition1.Name = "ucCondition1";
            this.ucCondition1.Size = new System.Drawing.Size(595, 28);
            this.ucCondition1.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(621, 512);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(44, 23);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(688, 636);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(44, 23);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "确定";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 920);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.flpCondition);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtShowField);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnRemoveCol);
            this.Controls.Add(this.btnAddCol);
            this.Controls.Add(this.listSelColumn);
            this.Controls.Add(this.listCol);
            this.Controls.Add(this.listTable);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flpCondition.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listTable;
        private System.Windows.Forms.ListView listCol;
        private System.Windows.Forms.ListView listSelColumn;
        private System.Windows.Forms.Button btnAddCol;
        private System.Windows.Forms.Button btnRemoveCol;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtShowField;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.FlowLayoutPanel flpCondition;
        private UFC.Speciment.ucCondition ucCondition1;
        private System.Windows.Forms.Button btnAddCon;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
    }
}