using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace FS.HISFC.Components.EPR.Controls
{
    /// <summary>
    /// frmDiagnoseICD10 ��ժҪ˵����
    /// </summary>
    internal class frmDiagnose : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        public string strTest = "";
        DataSet ds = new DataSet();
        DataView dv;
        //FS.HISFC.Management.EPR.EprIcd10 icdMgr=new FS.HISFC.Management.EPR.EprIcd10();
       
        public frmDiagnose()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();


            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
        }

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("����", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(8, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(624, 31);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "����ICD10ƴ������в�ѯ";
            // 
            // fpSpread1
            // 
            this.fpSpread1.Location = new System.Drawing.Point(8, 80);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
																				   this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(624, 320);
            this.fpSpread1.TabIndex = 4;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.ColumnCount = 10;
            this.fpSpread1_Sheet1.RowCount = 1;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // 
            // frmDiagnoseICD10
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(640, 414);
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.MaximumSize = new System.Drawing.Size(648, 448);
            this.MinimumSize = new System.Drawing.Size(648, 448);
            this.Name = "frmDiagnoseICD10";
            this.Text = "���ICD10��׼";
            this.Load += new System.EventHandler(this.frmDiagnoseICD10_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void frmDiagnoseICD10_Load(object sender, System.EventArgs e)
        {

            //initialDataTable();
            Retrieve();
        }

        /// <summary>
        /// ��ʼ��DataTable
        /// </summary>
        public void initialDataTable()
        {
            DataTable dataTable = new DataTable("icd10");

            DataColumn datacolumn = new DataColumn("ICD����");
            datacolumn.DataType = typeof(string);
            dataTable.Columns.Add(datacolumn);

            DataColumn datacolumn1 = new DataColumn("ƴ����");
            datacolumn.DataType = typeof(string);
            dataTable.Columns.Add(datacolumn1);

            DataColumn datacolumn2 = new DataColumn("ICD����");
            datacolumn.DataType = typeof(string);
            dataTable.Columns.Add(datacolumn2);

            DataColumn datacolumn3 = new DataColumn("ICD����1");
            datacolumn.DataType = typeof(string);
            dataTable.Columns.Add(datacolumn3);

            DataColumn datacolumn4 = new DataColumn("ICD����2");
            datacolumn.DataType = typeof(string);
            dataTable.Columns.Add(datacolumn4);

            DataColumn datacolumn5 = new DataColumn("�����");
            datacolumn.DataType = typeof(string);
            dataTable.Columns.Add(datacolumn5);

            DataColumn datacolumn6 = new DataColumn("��������");
            datacolumn.DataType = typeof(string);
            dataTable.Columns.Add(datacolumn6);

            ds.Tables.Add(dataTable);
            dv = new DataView(ds.Tables["icd10"]);
            this.fpSpread1_Sheet1.DataSource = dv;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.SetFP();

        }


        /// <summary>
        ///ˢ���б� 
        /// </summary>
        /// <returns></returns>
        public int Retrieve()
        {
            //ds.Tables[0].Rows.Clear();		
            ds = new DataSet();
            ArrayList al = new ArrayList();
            {
                //���޸�
                //ds = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.QueryICDList();

            }
            if (ds == null) return -1;
            dv = new DataView(ds.Tables[0]);
            //			
            //			foreach(FS.HISFC.Object.EPR.DiagnoseICD10 obj in al)
            //			{
            //				DataRow dr=ds.Tables["icd10"].NewRow();
            //				dr["ICD����"]=obj.Icd_code;
            //				dr["ICD����"]=obj.Icd_name;
            //				dr["ƴ����"]=obj.Spell_code;
            //				dr["ICD����1"]=obj.Icd_name1;
            //				dr["ICD����2"]=obj.Icd_name2;
            //				dr["�����"]=obj.Wb_code;
            //				dr["��������"]=obj.Disease_code;
            //				//����
            //				ds.Tables[0].Rows.Add(dr);
            //
            //			}
            this.fpSpread1_Sheet1.DataSource = dv;
            SetFP();//����FarPoint��ʽ
            return 0;

        }
        /// <summary>
        /// ����FarPoint��ʽ
        /// </summary>
        protected void SetFP()
        {
            this.fpSpread1.Sheets[0].Columns[0].Width = 50;
            this.fpSpread1.Sheets[0].Columns[0].Visible = false;
            this.fpSpread1.Sheets[0].Columns[2].Visible = false;
            this.fpSpread1.Sheets[0].Columns[3].Visible = false;
            this.fpSpread1.Sheets[0].Columns[1].Width = 100;
            this.fpSpread1.Sheets[0].Columns[2].Width = 100;
            this.fpSpread1.Sheets[0].Columns[3].Width = 70;
            this.fpSpread1.Sheets[0].Columns[4].Width = 70;
            this.fpSpread1.Sheets[0].Columns[5].Width = 50;
            this.fpSpread1.Sheets[0].Columns[6].Width = 200;

        }


        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            dv.RowFilter = "ƴ���� like '%" + this.textBox1.Text + "%'";
            this.SetFP();
        }


        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int index = this.fpSpread1_Sheet1.ActiveRowIndex;
            this.strTest = this.fpSpread1_Sheet1.Cells[index, 6].Text;
            this.FindForm().Close();
            DialogResult = DialogResult.OK;
        }


    }
}
