using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.EPR
{
    /// <summary>
    /// Ȩ�޹���
    /// </summary>
    public partial class ucPermission : UserControl,FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucPermission()
        {
            InitializeComponent();
        }

        

        private DataSet ds = new DataSet();
        private DataView dv;
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void init()
        {
            //��ʼ��DataTable
            DataTable table = new DataTable("Table");

            DataColumn dataColumn1 = new DataColumn("Ա������");
            dataColumn1.DataType = typeof(System.String);
            table.Columns.Add(dataColumn1);

            DataColumn dataColumn2 = new DataColumn("����");
            dataColumn2.DataType = typeof(System.String);
            table.Columns.Add(dataColumn2);

            DataColumn dataColumn3 = new DataColumn("����");
            dataColumn3.DataType = typeof(System.String);
            table.Columns.Add(dataColumn3);

            DataColumn dataColumn4 = new DataColumn("ҽ��Ȩ��");
            dataColumn4.DataType = typeof(System.String);
            table.Columns.Add(dataColumn4);

            DataColumn dataColumn5 = new DataColumn("ҽ����Ȩ��ʼ");
            dataColumn5.DataType = typeof(System.DateTime);
            table.Columns.Add(dataColumn5);

            DataColumn dataColumn6 = new DataColumn("ҽ����Ȩ����");
            dataColumn6.DataType = typeof(System.DateTime);
            table.Columns.Add(dataColumn6);

            DataColumn dataColumn7 = new DataColumn("����Ȩ��");
            dataColumn7.DataType = typeof(System.String);
            table.Columns.Add(dataColumn7);

            DataColumn dataColumn8 = new DataColumn("�ʿ�Ȩ��");
            dataColumn8.DataType = typeof(System.String);
            table.Columns.Add(dataColumn8);

            DataColumn dataColumn9 = new DataColumn("����Ա");
            dataColumn9.DataType = typeof(System.String);
            table.Columns.Add(dataColumn9);

            DataColumn dataColumn10 = new DataColumn("��������");
            dataColumn10.DataType = typeof(System.DateTime);
            table.Columns.Add(dataColumn10);

            //��ʼ��dataSet
            ds.Tables.Add(table);


            dv = new DataView(ds.Tables[0]);
            this.fpSpread1.Sheets[0].DataSource = dv;
            this.fpSpread1.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this._SetFP();
        }

        protected void _SetFP()
        {
            this.fpSpread1.Sheets[0].Columns[0].Width = 80;
            this.fpSpread1.Sheets[0].Columns[1].Width = 100;
            this.fpSpread1.Sheets[0].Columns[2].Width = 100;
            this.fpSpread1.Sheets[0].Columns[3].Width = 100;
            this.fpSpread1.Sheets[0].Columns[4].Width = 100;
            this.fpSpread1.Sheets[0].Columns[5].Width = 100;
            this.fpSpread1.Sheets[0].Columns[6].Width = 100;
            this.fpSpread1.Sheets[0].Columns[7].Width = 100;
            this.fpSpread1.Sheets[0].Columns[8].Width = 60;
            this.fpSpread1.Sheets[0].Columns[9].Width = 100;
        }
        
        /// <summary>
        /// �޸�
        /// </summary>
        /// <returns></returns>
        public int Modify()
        {
            //// TODO:  ��� ucPermissionManager.Auditing ʵ��
            //if (this.fpSpread1.Sheets[0].ActiveRowIndex < 0)
            //{
            //    MessageBox.Show("��ѡ��Ҫ�޸ĵ��У�");
            //    return 0;
            //}
            //int i = this.fpSpread1.Sheets[0].ActiveRowIndex;
            ////FS.HISFC.Models.Medical.Permission obj = new FS.HISFC.Models.Medical.Permission();
            //obj.Person.ID = this.fpSpread1.Sheets[0].Cells[i, 0].Text;
            //obj.Person.Name = this.fpSpread1.Sheets[0].Cells[i, 1].Text;
            //obj.OrderPermission.Permission = this.fpSpread1.Sheets[0].Cells[i, 3].Text;
            //obj.DateBeginOrderPermission = DateTime.Parse(this.fpSpread1.Sheets[0].Cells[i, 4].Value.ToString());
            //obj.DateEndOrderPermission = DateTime.Parse(this.fpSpread1.Sheets[0].Cells[i, 5].Value.ToString());
            //obj.EMRPermission.Permission = this.fpSpread1.Sheets[0].Cells[i, 6].Text;
            //obj.QCPermission.Permission = this.fpSpread1.Sheets[0].Cells[i, 7].Text;
            //obj.OperCode = this.fpSpread1.Sheets[0].Cells[i, 8].Text;
            //ucPermissionInput uc = new ucPermissionInput(obj);
            //FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�޸�";
            //if (FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc) == DialogResult.OK)
            //{
            //    this.Retrieve();
            //}
            return 0;
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        public int Del()
        {
            //// TODO:  ��� ucPermissionManager.Del ʵ��
            //if (this.fpSpread1.Sheets[0].ActiveRowIndex < 0)
            //{
            //    MessageBox.Show("��ѡ��Ҫ�޸ĵ��У�");
            //    return 0;
            //}
            //int i = this.fpSpread1.Sheets[0].ActiveRowIndex;
            //if (MessageBox.Show("ȷ��Ҫɾ����", "ȷ��", System.Windows.Forms.MessageBoxButtons.YesNo) == DialogResult.No) return 0;

            //FS.HISFC.BizProcess.Factory.Function.BeginTransaction();

            //if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.DeletePermission(this.fpSpread1.Sheets[0].Cells[i, 0].Text) == -1)
            //{
            //    FS.HISFC.BizProcess.Factory.Function.RollBack();
            //    MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
            //    return -1;
            //}
            //FS.HISFC.BizProcess.Factory.Function.Commit();
            //MessageBox.Show("ɾ���ɹ���");
            //this.fpSpread1.Sheets[0].Rows.Remove(i, 1);
            return 0;
        }

     
        public int Pre()
        {
            // TODO:  ��� ucPermissionManager.Pre ʵ��
            this.fpSpread1.Sheets[0].ActiveRowIndex--;
            this.fpSpread1.Sheets[0].AddSelection(this.fpSpread1.Sheets[0].ActiveRowIndex, 0, 1, 1);
            return 0;
        }


        public int Next()
        {
            // TODO:  ��� ucPermissionManager.Next ʵ��
            this.fpSpread1.Sheets[0].ActiveRowIndex++;
            this.fpSpread1.Sheets[0].AddSelection(this.fpSpread1.Sheets[0].ActiveRowIndex, 0, 1, 1);
            return 0;
        }
        /// <summary>
        /// ˢ���б�
        /// </summary>
        /// <returns></returns>
        public int Retrieve()
        {
            //// TODO:  ��� ucPermissionManager.Retrieve ʵ��
            //ds.Tables[0].Rows.Clear();
            //ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetPermissionList();
            //foreach (FS.HISFC.Models.Medical.Permission obj in al)
            //{
            //    DataRow dr = ds.Tables[0].NewRow();

            //    dr["Ա������"] = obj.ID;
            //    dr["����"] = obj.Name;
            //    dr["����"] = obj.Person.Dept.Name;
            //    dr["ҽ��Ȩ��"] = obj.OrderPermission.ToString();
            //    dr["ҽ����Ȩ��ʼ"] = obj.DateBeginOrderPermission;
            //    dr["ҽ����Ȩ����"] = obj.DateEndOrderPermission;
            //    dr["����Ȩ��"] = obj.EMRPermission.ToString();
            //    dr["�ʿ�Ȩ��"] = obj.QCPermission.ToString();
            //    dr["����Ա"] = obj.OperCode;
            //    dr["��������"] = obj.OperDate;
            //    ds.Tables[0].Rows.Add(dr);
            //}
            //this._SetFP();
            return 0;
        }
        /// <summary>
        /// �������ԱȨ��
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            //// TODO:  ��� ucPermissionManager.Add ʵ��
            //FS.HISFC.Models.Medical.Permission obj = new FS.HISFC.Models.Medical.Permission();
            //ucPermissionInput uc = new ucPermissionInput(obj);
            //FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "���";
            //if (FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc) == DialogResult.OK)
            //{
            //    this.Retrieve();
            //}
            return 0;
        }

      
        /// <summary>
        /// �˳�
        /// </summary>
        /// <returns></returns>
        public int Exit()
        {
            // TODO:  ��� ucPermissionManager.Exit ʵ��
            this.FindForm().Close();
            return 0;
        }

        public int Save()
        {
            // TODO:  ��� ucPermissionManager.Save ʵ��
            return 0;
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            dv.RowFilter = "Ա������ like '%" + this.textBox1.Text.Trim() + "%'";
            this._SetFP();
        }

        public int Print()
        {
            return 0;
        }


        #region IMaintenanceControlable ��Ա


        public int Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Cut()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Delete()
        {
            return this.Del();
        }

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Import()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Init()
        {
             this.init();
             return 0;
        }

        private bool bdirty = false;
        public bool IsDirty
        {
            get
            {
                return bdirty;
            }
            set
            {
                bdirty = value;
            }
        }

       

        public int NextRow()
        {
            this.Next();
            return 0;
        }

        public int Paste()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PreRow()
        {
            this.Pre();
            return 0;
        }

        public int PrintConfig()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Query()
        {
           return this.Retrieve();
        }
        FS.FrameWork.WinForms.Forms.IMaintenanceForm a;
        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
                if (a != null)
                {
                    a.ShowCutButton = false;
                    a.ShowCopyButton = false;
                    a.ShowModifyButton = true;
                    a.ShowNextRowButton = true;
                    a.ShowPreRowButton = true;
                    a.ShowPrintButton = false;
                    a.ShowExportButton = false;
                    a.ShowImportButton = false;
                    a.ShowPasteButton = false;
                    a.ShowPrintConfigButton = false;
                    a.ShowPrintPreviewButton = false;
                    a.ShowSaveButton = false;
                }
            }
        }

        #endregion
    }
}
