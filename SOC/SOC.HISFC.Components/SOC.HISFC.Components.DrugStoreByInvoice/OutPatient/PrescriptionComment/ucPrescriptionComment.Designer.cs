namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.PrescriptionComment
{
    partial class ucPrescriptionComment
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPrescriptionComment));
            this.ucPrescriptionCommentTree1 = new SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.PrescriptionComment.ucPrescriptionCommentTree();
            this.ucRecipeDetail1 = new SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.ucRecipeDetail(this.components);
            this.ucPatientInfo1 = new SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.ucPatientInfo();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.ngbPatientInfo.SuspendLayout();
            this.ngbRecipeDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ucPrescriptionCommentTree1);
            // 
            // ngbAdd
            // 
            this.ngbAdd.Location = new System.Drawing.Point(0, 587);
            this.ngbAdd.Size = new System.Drawing.Size(775, 55);
            // 
            // ngbPatientInfo
            // 
            this.ngbPatientInfo.Controls.Add(this.ucPatientInfo1);
            // 
            // ngbRecipeDetail
            // 
            this.ngbRecipeDetail.Controls.Add(this.ucRecipeDetail1);
            this.ngbRecipeDetail.Size = new System.Drawing.Size(775, 514);
            // 
            // ucPrescriptionCommentTree1
            // 
            this.ucPrescriptionCommentTree1.AllDrugRecipe = null;
            this.ucPrescriptionCommentTree1.BeginTime = new System.DateTime(2012, 8, 22, 18, 6, 38, 234);
            this.ucPrescriptionCommentTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrescriptionCommentTree1.DoctCode = "医生：";
            this.ucPrescriptionCommentTree1.DrugRecipe = null;
            this.ucPrescriptionCommentTree1.EndTime = new System.DateTime(2012, 8, 22, 18, 6, 38, 234);
            this.ucPrescriptionCommentTree1.IsAtomizationOnOper = true;
            this.ucPrescriptionCommentTree1.IsShowInvoice = true;
            this.ucPrescriptionCommentTree1.Location = new System.Drawing.Point(0, 0);
            this.ucPrescriptionCommentTree1.Name = "ucPrescriptionCommentTree1";
            this.ucPrescriptionCommentTree1.PriveDept = ((FS.FrameWork.Models.NeuObject)(resources.GetObject("ucPrescriptionCommentTree1.PriveDept")));
            this.ucPrescriptionCommentTree1.Size = new System.Drawing.Size(233, 642);
            this.ucPrescriptionCommentTree1.TabIndex = 0;
            this.ucPrescriptionCommentTree1.TabPage1Name = "处方列表";
            // 
            // ucRecipeDetail1
            // 
            this.ucRecipeDetail1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucRecipeDetail1.Decimals = 2;
            this.ucRecipeDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRecipeDetail1.EnumQtyShowType = SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.Function.EnumQtyShowType.最小单位;
            this.ucRecipeDetail1.IsFullConvertToHalf = true;
            this.ucRecipeDetail1.IsPrint = false;
            this.ucRecipeDetail1.Location = new System.Drawing.Point(3, 17);
            this.ucRecipeDetail1.Name = "ucRecipeDetail1";
            this.ucRecipeDetail1.Size = new System.Drawing.Size(769, 494);
            this.ucRecipeDetail1.TabIndex = 0;
            // 
            // ucPatientInfo1
            // 
            this.ucPatientInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPatientInfo1.Location = new System.Drawing.Point(3, 17);
            this.ucPatientInfo1.Name = "ucPatientInfo1";
            this.ucPatientInfo1.Size = new System.Drawing.Size(769, 53);
            this.ucPatientInfo1.TabIndex = 0;
            // 
            // ucPrescriptionComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ucPrescriptionComment";
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.ngbPatientInfo.ResumeLayout(false);
            this.ngbRecipeDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private ucPrescriptionCommentTree ucPrescriptionCommentTree1;
        private SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.ucRecipeDetail ucRecipeDetail1;
        private SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common.ucPatientInfo ucPatientInfo1;

    }
}
