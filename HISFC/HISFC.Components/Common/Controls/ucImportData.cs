using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// [��������: ���ݵ���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007��04]<br></br>
    /// <˵��>
    ///     1�����ݵ��벢��ʾ .xls �� .dbf
    /// </˵��>
    /// </summary>
    public partial class ucImportData : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucImportData()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ִ��Sql
        /// </summary>
        private System.Data.OleDb.OleDbCommand oledbDataCommand = null;

        /// <summary>
        /// ��������
        /// </summary>
        private System.Data.OleDb.OleDbConnection oledbDataConnection = null;

        /// <summary>
        /// ����������
        /// </summary>
        private System.Data.OleDb.OleDbDataAdapter oledbDataAdapter = null;

        /// <summary>
        /// ODBC
        /// </summary>
        private System.Data.Odbc.OdbcCommand odbcDataCommand = null;

        /// <summary>
        /// ODBC ��������
        /// </summary>
        private System.Data.Odbc.OdbcConnection odbcDataConnection = null;

        /// <summary>
        /// ODBC����������
        /// </summary>
        private System.Data.Odbc.OdbcDataAdapter odbcDataAdapter = null;
        
        /// <summary>
        /// ���ļ��ڶ�ȡ�����ݼ�
        /// </summary>
        private DataSet ds = null;

        /// <summary>
        /// ���
        /// </summary>
        private DialogResult rs = DialogResult.Cancel;

        #endregion

        #region ����

        /// <summary>
        /// ���ļ��ڶ�ȡ�����ݼ�
        /// </summary>
        public DataSet ImportData
        {
            get
            {
                return this.ds;
            }
        }

        /// <summary>
        /// ��� 
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return this.rs;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        protected void Claer()
        {
            if (this.ds != null)
            {
                this.ds.Clear();
            }

            this.txtFilePath.Text = "";
            this.lbDataInfo.Text = "������Ϣ:";

            this.rs = DialogResult.Cancel;
        }

        /// <summary>
        /// Excel���ݵ���
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        protected int ImportExcel(string dataFilePath)
        {
            if (this.ds != null)
            {
                this.ds.Clear();
            }
            else
            {
                this.ds = new DataSet();
            }

            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg("���ڶ������� ���Ժ�.."));
                Application.DoEvents();

                this.oledbDataConnection = new System.Data.OleDb.OleDbConnection();
                this.oledbDataConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dataFilePath + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";

                this.oledbDataCommand = new System.Data.OleDb.OleDbCommand();
                this.oledbDataCommand.Connection = this.oledbDataConnection;
                this.oledbDataCommand.CommandText = "SELECT *  FROM " + "[sheet1$]";

                this.oledbDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
                this.oledbDataAdapter.SelectCommand = this.oledbDataCommand;
                this.oledbDataAdapter.Fill(this.ds);

                if (this.ds.Tables.Count <= 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������"));
                    return 1;
                }

                int column = this.ds.Tables[0].Columns.Count;

                int row = this.ds.Tables[0].Rows.Count;

                this.lbDataInfo.Text = string.Format("������Ϣ:�� {0} �� {1} ��", column.ToString(), row.ToString());

                this.neuSpread1_Sheet1.DataSource = this.ds;

                this.rs = DialogResult.OK;

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// DBF���ݵ���
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int ImportDBF(string dataFilePath)
        {
            if (this.ds != null)
            {
                this.ds.Clear();
            }
            else
            {
                this.ds = new DataSet();
            }

            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg("���ڶ������� ���Ժ�.."));
                Application.DoEvents();

                string sourcePathName = dataFilePath.Substring(0, dataFilePath.LastIndexOf("\\"));
                string sourceFileName = dataFilePath.Substring(dataFilePath.LastIndexOf("\\") + 1, dataFilePath.Length - dataFilePath.LastIndexOf("\\") - 1);

                this.odbcDataAdapter = new System.Data.Odbc.OdbcDataAdapter();
                this.odbcDataCommand = new System.Data.Odbc.OdbcCommand();
                this.odbcDataConnection = new System.Data.Odbc.OdbcConnection();
                this.odbcDataConnection.ConnectionString = "MaxBufferSize=2048;DSN=dBASE Files;PageTimeout=5;DefaultDir=" + sourcePathName + "\\;DBQ=" + sourcePathName + "\\;DriverId=" + "533";
                this.odbcDataCommand.Connection = this.odbcDataConnection;
                this.odbcDataAdapter.SelectCommand = this.odbcDataCommand;
                this.odbcDataCommand.CommandText = "SELECT *  FROM " + sourceFileName;
                this.odbcDataAdapter.Fill(this.ds);

                if (this.ds.Tables.Count <= 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������"));
                    return 1;
                }

                int column = this.ds.Tables[0].Columns.Count;
                int row = this.ds.Tables[0].Rows.Count;

                this.lbDataInfo.Text = "�������ļ�����" + column.ToString() + "�С�����" + row.ToString() + "����¼��";

                this.neuSpread1_Sheet1.DataSource = this.ds;

                this.rs = DialogResult.OK;

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �ر�
        /// </summary>
        protected void Close()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            this.Claer();

            this.ds = null;

            base.OnLoad(e);
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            Stream dataStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (this.ckDbf.Checked)         //DBF�ļ�
            {
                openFileDialog1.Filter = "DBF files (*.dbf)|*.dbf";               
            }
            else                            //XLS�ļ�
            {
                openFileDialog1.Filter = "Excel files (*.xls)|*.xls"; 
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dataStream = openFileDialog1.OpenFile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message));
                    return;
                }

                if (dataStream != null)
                {
                    this.txtFilePath.Text = openFileDialog1.FileName;
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ч�ļ�"));
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFilePath.Text))
            {
                MessageBox.Show("��ѡ�������ļ�·��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtFilePath.Focus();
                return;
            }

            if (this.ckDbf.Checked)
            {
                this.ImportDBF(this.txtFilePath.Text);
            }
            else
            {
                this.ImportExcel(this.txtFilePath.Text);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();

            this.rs = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

            this.rs = DialogResult.Cancel;
        }
    }
}
