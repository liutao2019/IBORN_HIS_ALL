using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;
using FS.FrameWork.WinForms.Classes;

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ά����ӡ����ؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-11-27]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucMaintenanceReport : UserControl, IMaintenanceControlable
    {
        public ucMaintenanceReport()
        {
            InitializeComponent();
        }

        #region �ֶ�
        private IMaintenanceForm maintenanceForm;
        private bool isDirty;

        private ReportPrintManager DB = new ReportPrintManager();

        private List<int> insertRows = new List<int>();
        private List<int> deleteRows = new List<int>();
        private List<int> updateRows = new List<int>();

        private bool inited = false;
        private bool manual = false;    //�ֹ��޸�
        #endregion

        #region ����

        /// <summary>
        /// ��ÿؼ����ƴ�DLL
        /// </summary>
        /// <param name="dllFileName">DLL�ļ�����</param>
        /// <param name="interfaceType">��ʵ�ֵĽӿ�����</param>
        /// <returns>��������</returns>
        private string[] GetControlNames(string dllFileName, Type type)
        {
            if (dllFileName.Length == 0)
                return null;

            try
            {
                List<string> ret = new List<string>();
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dllFileName + ".dll");

                if (!System.IO.File.Exists(assembly.Location))
                {
                    MessageBox.Show(FrameWork.Management.Language.Msg("��������Ŀ¼��δ���ҵ�" + dllFileName + ".dll"));
                    return null;
                }

                Type[] types = assembly.GetTypes();


                foreach (Type mytype in types)
                {
                    //������ʵ�ֵĽӿ�
                    Type[] interfaces = mytype.GetInterfaces();
                    foreach (Type interfaceType in interfaces)
                    {
                        if (interfaceType.Equals(type))
                        {
                            ret.Add(mytype.ToString());
                        }
                    }

                }

                return ret.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// ��ýӿ�����
        /// </summary>
        /// <param name="row"></param>
        private int GetInterfaceNames(int row)
        {
            this.neuSpread2_Sheet1.RowCount = 0;
            if (this.neuSpread1_Sheet1.Cells[row, 0].Text.Length == 0)
            {
                return -1;
            }

            string dllName = this.neuSpread1_Sheet1.Cells[row, 0].Text + ".dll";
            string controlName = this.neuSpread1_Sheet1.Cells[row, 1].Text;
            if (controlName.Length == 0)
            {
                return -1;
            }
            try
            {
                IInterfaceContainer reportContainer = System.Reflection.Assembly.LoadFrom(dllName).CreateInstance(controlName) as IInterfaceContainer;
                if (reportContainer == null)
                {
                    MessageBox.Show(FrameWork.Management.Language.Msg("��ѡ��Ľӿ�����" + controlName + "�в����ڽӿڣ���ɾ���������ݣ�"));
                    return -1;
                }

                this.neuSpread2_Sheet1.RowCount = reportContainer.InterfaceTypes.Length;
                for (int i = 0; i < reportContainer.InterfaceTypes.Length; i++)
                {
                    this.neuSpread2_Sheet1.Cells[i, 0].Tag = reportContainer.InterfaceTypes[i];
                    this.neuSpread2_Sheet1.Cells[i, 0].Text = reportContainer.InterfaceTypes[i].FullName;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ���ýӿ�ʵ��Control�����Ƿ�ɱ༭
        /// </summary>
        /// <param name="isLocked">True ����༭ False ������༭</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private void SetInterfaceInstanceLocked(int iRowInex,bool isLocked)
        {
            this.neuSpread2_Sheet1.Cells[iRowInex, 2].Locked = isLocked;
        }

        #endregion

        #region IMaintenanceControlable ��Ա

        public int Add()
        {
            this.neuSpread1_Sheet1.RowCount += 1;
            this.insertRows.Add(this.neuSpread1_Sheet1.RowCount - 1);
            this.isDirty = true;

            this.inited = true;

            ReportPrint reportPrint = new ReportPrint();
            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Tag = reportPrint;

            return 0;
        }

        public int Copy()
        {
            return 0;
        }

        public int Cut()
        {
            return 0;
        }

        public int Delete()
        {
            DialogResult rs = MessageBox.Show("ȷ��ɾ����ǰѡ�е�������", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.No)
            {
                return 1;
            }

            this.neuSpread1.StopCellEditing();          

            ReportPrint delReportPrint = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as ReportPrint;

            int param = this.DB.DeleteData(delReportPrint);
            if (param == -1)
            {
                MessageBox.Show("Delete failed," + this.DB.Err);
                return -1;
            }

            this.neuSpread1_Sheet1.ActiveRow.Visible = false;

            this.isDirty = true;

            MessageBox.Show("ɾ���ɹ�");

            return 0;
        }

        public int Export()
        {
            return 0;
        }

        public int Import()
        {
            return 0;
        }

        public int Init()
        {
            return 0;
        }

        public bool IsDirty
        {
            get
            {
                return false;
            }
            set
            {
                this.isDirty = value;
            }
        }

        public int Modify()
        {
            return 0;
        }

        public int NextRow()
        {
            return 0;
        }

        public int Paste()
        {
            return 0;
        }

        public int PreRow()
        {
            return 0;
        }

        public int Print()
        {
            return 0;
        }

        public int PrintConfig()
        {
            return 0;
        }

        public int PrintPreview()
        {
            return 0;
        }

        public int Query()
        {
            this.inited = false;

            this.isDirty = false;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ؽӿ�ʵ��������Ϣ...");
            Application.DoEvents();

            List<ReportPrint> ret = this.DB.LoadData();
            this.neuSpread1_Sheet1.RowCount = 0;
            foreach (ReportPrint reportPrint in ret)
            {
                this.neuSpread1_Sheet1.RowCount += 1;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = reportPrint.ContainerDllName;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = reportPrint.ContainerContorl;

                this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Tag = reportPrint;
            }

            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return 0;
            }

            this.GetInterfaceNames(0);
            ReportPrint reportPrint1 = this.neuSpread1_Sheet1.Rows[0].Tag as ReportPrint;

            int i = 0;
            if (reportPrint1 != null)
            {
                foreach (ReportPrintControl reportPrintControl in reportPrint1.ReportPrintControls)
                {
                    this.neuSpread2_Sheet1.Rows.Add(i, 1);
                    this.neuSpread2_Sheet1.Cells[i, 1].Text = reportPrintControl.DllName;
                    this.neuSpread2_Sheet1.Cells[i, 2].Text = reportPrintControl.ControlName;
                    this.neuSpread2_Sheet1.Cells[i, 3].Text = reportPrintControl.Memo;                   

                    this.SetInterfaceInstanceLocked(i, true);

                    i++;
                }                
            }

            this.inited = true;

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 0;
        }

        public IMaintenanceForm QueryForm
        {
            get
            {
                return this.maintenanceForm;
            }
            set
            {
                this.maintenanceForm = value;
            }
        }

        public int Save()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
          
            this.DB.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int activeRowIndex = this.neuSpread1_Sheet1.ActiveRowIndex;
            ReportPrint reportPrint = this.neuSpread1_Sheet1.Rows[activeRowIndex].Tag as ReportPrint;

            foreach (ReportPrintControl reportPrintControl in reportPrint.ReportPrintControls)
            {
                if (string.IsNullOrEmpty(reportPrintControl.DllName) || string.IsNullOrEmpty(reportPrintControl.ControlName))
                {
                    reportPrintControl.InterfaceName = "";
                }
                if (!string.IsNullOrEmpty(reportPrintControl.DllName) && !string.IsNullOrEmpty(reportPrintControl.ControlName))
                {
                    if (string.IsNullOrEmpty(reportPrintControl.InterfaceName))
                    {
                        MessageBox.Show("�ӿ�ʵ�ֲ�Ϊ�� ����Ӧ�ӿ���ϢΪ�� ���˳�ά����������", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                }
            }

            //����ɾ��ԭ����
            int ret = this.DB.DeleteData(reportPrint);
            if (ret == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                MessageBox.Show("Save failed," + this.DB.Err);
                return -1;
            }
            //��ά�����ݽ������²���
            ret = this.DB.InsertData(reportPrint);
            if (ret == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                MessageBox.Show("Save failed," + this.DB.Err);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.isDirty = false;

            MessageBox.Show("Save Success", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Query();

            return 0;
        }

        #endregion

        #region �¼�

        #endregion

        private void neuSpread1_ComboSelChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            this.GetInterfaceNames(e.Row);

            ReportPrint reportPrint = this.neuSpread1_Sheet1.Rows[e.Row].Tag as ReportPrint;

            reportPrint.ContainerContorl = this.neuSpread1_Sheet1.Cells[e.Row, 1].Text;

            if (reportPrint.ReportPrintControls.Count == 0)
            {
                for (int j = 0; j < this.neuSpread2_Sheet1.RowCount; j++)
                {
                    ReportPrintControl reportPrintControl = new ReportPrintControl();
                    reportPrint.ReportPrintControls.Add(reportPrintControl);
                    this.neuSpread2_Sheet1.Rows[j].Tag = reportPrintControl;
                }
            }
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ReportPrint reportPrint = this.neuSpread1_Sheet1.Rows[e.Row].Tag as ReportPrint;
            this.manual = true;

            if (reportPrint != null)
            {
                int ret = this.GetInterfaceNames(e.Row);
                if (ret == -1)
                {
                    return;
                }

                int i = 0;

                foreach (ReportPrintControl reportPrintControl in reportPrint.ReportPrintControls)
                {
                    i = reportPrintControl.Index;
                    
                    reportPrintControl.InterfaceName = this.neuSpread2_Sheet1.Cells[i, 0].Text;
                    this.neuSpread2_Sheet1.Cells[i, 1].Text = reportPrintControl.DllName;
                    this.neuSpread2_Sheet1.Cells[i, 2].Text = reportPrintControl.ControlName;
                    this.neuSpread2_Sheet1.Cells[i, 3].Text = reportPrintControl.Memo;
                    this.neuSpread2_Sheet1.Rows[i].Tag = reportPrintControl;

                    this.SetInterfaceInstanceLocked(i, true);

                }
            }

            this.manual = false;
        }       

        private void neuSpread1_EditModeOff(object sender, EventArgs e)
        {
            if (!this.inited)
            {
                return;
            }

            int activeRowIndex = this.neuSpread1_Sheet1.ActiveRowIndex;
            int activeColumnIndex = this.neuSpread1_Sheet1.ActiveColumnIndex;

            ReportPrint reportPrint = this.neuSpread1_Sheet1.Rows[activeRowIndex].Tag as ReportPrint;

            if (this.neuSpread1_Sheet1.ActiveColumnIndex == 0)
            {
                reportPrint.ContainerDllName = this.neuSpread1_Sheet1.Cells[activeRowIndex, 0].Text;
                string[] r = this.GetControlNames(this.neuSpread1_Sheet1.Cells[activeRowIndex, activeColumnIndex].Text, typeof(IInterfaceContainer));
                if (r == null) return;
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                comboBoxCellType.Items = r;
                this.neuSpread1_Sheet1.Cells[activeRowIndex, 1].CellType = comboBoxCellType;
            }
        }

        private void neuSpread2_EditModeOff(object sender, EventArgs e)
        {
            int activeRowIndex = this.neuSpread2_Sheet1.ActiveRowIndex;
            int activeColumnIndex = this.neuSpread2_Sheet1.ActiveColumnIndex;

            if (activeColumnIndex == 1)
            {
                Type t = this.neuSpread2_Sheet1.Cells[activeRowIndex, 0].Tag as Type;
                string[] r = this.GetControlNames(this.neuSpread2_Sheet1.Cells[activeRowIndex, activeColumnIndex].Text, t);
                if (r == null)
                {
                    return;
                }

                FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                comboBoxCellType.Items = r;

                this.neuSpread2_Sheet1.Cells[activeRowIndex, 2].CellType = comboBoxCellType;

                this.SetInterfaceInstanceLocked(activeRowIndex, false);
            }
        }

        private void neuSpread2_ComboCloseUp(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            ReportPrintControl reportPrintControl = this.neuSpread2_Sheet1.Rows[e.Row].Tag as ReportPrintControl;
            if (reportPrintControl != null)
            {
                reportPrintControl.DllName = this.neuSpread2_Sheet1.Cells[e.Row, 1].Text;
                reportPrintControl.ControlName = this.neuSpread2_Sheet1.Cells[e.Row, 2].Text;
                reportPrintControl.Memo = this.neuSpread2_Sheet1.Cells[e.Row, 3].Text;
                reportPrintControl.InterfaceName = this.neuSpread2_Sheet1.Cells[e.Row, 0].Text;

                reportPrintControl.Index = (short)e.Row;
            }
        }
    }
}