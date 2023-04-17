using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml.Serialization;

namespace FS.HISFC.Components.EPR.QC
{
    public partial class ucQCStat : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQCStat()
        {
            InitializeComponent();
        }

        #region ����
        //FS.HISFC.Management.EPR.QC qcManager = new FS.HISFC.Management.EPR.QC();
        //FS.HISFC.Management.Manager.Department deptManager = new FS.HISFC.Management.Manager.Department();
        //FS.HISFC.Management.RADT.InPatient patientManager = new FS.HISFC.Management.RADT.InPatient();
        //FS.HISFC.Management.EPR.QCInfo qcInfoManager = new FS.HISFC.Management.EPR.QCInfo();

        private ArrayList alConditions = null;
        private ArrayList alDepts = null;
        /// <summary>
        /// ����ѡ��������
        /// </summary>
        private ArrayList alSelectedCondition;//add by pantiejun 2008-4-1
        #endregion

        #region ��ʼ��
        protected override void OnLoad(EventArgs e)
        {
            this.alDepts=FS.HISFC.BizProcess.Factory.Function.IntegrateManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            this.cmbDept.AddItems(this.alDepts);
            FS.HISFC.Models.RADT.InStateEnumService instate = new FS.HISFC.Models.RADT.InStateEnumService();

            this.cmbState.AddItems(FS.HISFC.Models.RADT.InStateEnumService.List());

            this.alConditions = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetQCConditionList();

            #region modified by pantiejun 2008-4-1 begin
            
            string conditionXml = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetSetting("1");
            if (!string.IsNullOrEmpty(conditionXml))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(FS.HISFC.Models.EPR.QCCondition), typeof(FS.HISFC.Models.EPR.QCConditions), typeof(FS.FrameWork.Models.NeuObject) });
                    System.IO.StringReader sr = new System.IO.StringReader(conditionXml);
                    this.alSelectedCondition = xs.Deserialize(sr) as ArrayList;
                    this.InitFp();
                }catch{}
            }
            else
            {

                if (this.alConditions != null)
                {
                    this.InitFp();
                }
                else
                {
                    MessageBox.Show("��ȡ�ʿ���������");
                }
            }
            #endregion modified by pantiejun 2008-4-1 end
            base.OnLoad(e);
        }

        #region add by pantiejun 2008-4-1 begin
        /// <summary>
        /// ��ʼ���˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "��ʾ����":
                    this.SetSetting();
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// �����ʿ�����
        /// </summary>
        private void SetSetting()
        {
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "������ʾ����";
            ArrayList alNewSelectionCondition = new ArrayList();
            DialogResult dr = FS.FrameWork.WinForms.Classes.Function.PopShowControl(new ucQCSetting(this.alConditions, this.alSelectedCondition, ref alNewSelectionCondition));
            if (dr == DialogResult.OK)
            {
                if (alNewSelectionCondition.Count > 0)
                {
                    try
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(FS.HISFC.Models.EPR.QCCondition),typeof(FS.HISFC.Models.EPR.QCConditions), typeof(FS.FrameWork.Models.NeuObject) });
                        StringBuilder sb = new StringBuilder();
                        System.IO.StringWriter sw = new System.IO.StringWriter(sb);
                        xs.Serialize(sw, alNewSelectionCondition);
                        FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.SaveSetting(new FS.FrameWork.Models.NeuObject("1", "�ʿ�ͳ������", ""), sb.ToString());
                        this.alSelectedCondition = alNewSelectionCondition;
                        this.InitFp();
                    }
                    catch
                    {
                        MessageBox.Show("�������ó��ִ������Ժ����ԡ�");
                    }
                }
            }

        }
        #endregion add by pantiejun 2008-4-1 end

        private void InitFp()
        {
            this.fpSheetView.ColumnCount = (this.alSelectedCondition == null ? this.alConditions.Count : this.alSelectedCondition.Count) + 2;////add by pantiejun 2008-4-1
            this.fpSheetView.Columns[-1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSheetView.Columns.Count = this.alConditions.Count + 2;
            this.fpSheetView.Columns[0].Label = "סԺ��";
            this.fpSheetView.Columns[1].Label = "����";
            this.fpSheetView.ColumnHeader.Rows.Get(0).Height = 59F;
            this.fpSheetView.Columns[-1].Width = 100;

            #region modified by pantiejun 2008-4-1 begin
            if (this.alSelectedCondition != null && this.alSelectedCondition.Count>0)
            {
                for (int i = 0; i < this.alSelectedCondition.Count; i++)
                {
                    this.fpSheetView.Columns[i + 2].Label = alSelectedCondition[i].ToString();
                }
            }
            else
            {
                for (int i = 0; i < this.alConditions.Count; i++)
                {
                    this.fpSheetView.Columns[i + 2].Label = this.alConditions[i].ToString();
                }
            }
            #endregion modified by pantiejun 2008-4-1 end

            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
        }

        void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSheetView.Rows[e.Row].Tag == null) return;//add by pantiejun 2008-4-1
            if (this.fpSheetView.Rows[e.Row].Tag.GetType() ==typeof( FS.HISFC.Models.RADT.PatientInfo))
            {
                FS.HISFC.Models.RADT.PatientInfo curPatient = this.fpSheetView.Rows[e.Row].Tag as FS.HISFC.Models.RADT.PatientInfo;
                System.Windows.Forms.Panel emrPanel=new Panel();
                emrPanel.Size=new Size(800,600);
                Common.Classes.Function.EMRShow(emrPanel, curPatient, "0", false);
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(emrPanel);
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            toolBarService.AddToolButton("��ʾ����", "��ʾ����", 0, true, false, null);//add by pantiejun 2008-4-1
            //toolBarService.AddToolButton("����", "�����������ļ�", FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��, true, false, null);
            return toolBarService;
        }

       

        #endregion

        #region ����
        public override int Export(object sender, object neuObject)
        {
            SaveFileDialog form = new SaveFileDialog();
            form.Filter = "*.xls|*.xls";
            form.ShowDialog();
            this.fpSpread1.SaveExcel(form.FileName);
            return 0;
        }
       
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.radDept.Checked)
            {
                if (this.cmbDept.Tag == null)
                {
                    MessageBox.Show("��ѡ����ң�");
                    this.cmbDept.Focus();
                    return -1;
                }
                else
                {
                    //modify by pantiejun 2008-4-8
                    FS.HISFC.Models.RADT.InStateEnumService state = new FS.HISFC.Models.RADT.InStateEnumService();
                    state.ID = this.cmbState.SelectedItem.ID;
                    //if (this.QueryByDept(this.cmbDept.Tag.ToString(), this.cmbState.SelectedItem as FS.HISFC.Models.RADT.InStateEnumService) == -1)
                    if (this.QueryByDept(this.cmbDept.Tag.ToString(), state) == -1)
                    {
                        MessageBox.Show("û�з��������Ļ��ߣ�");
                        this.cmbDept.Focus();
                        return -1;
                    }
                }
            }

            else if (this.radInDate.Checked)
            {
                if (this.dtpBegin.Value > this.dtpEnd.Value)
                {
                    MessageBox.Show("��ѯ��ʼʱ�䲻�ܴ��ڽ���ʱ�䣡");
                    this.dtpBegin.Focus();
                    return -1;
                }
                if (this.QueryByInDate() == -1)
                {
                    MessageBox.Show("û�з��������Ļ��ߣ�");
                    this.dtpBegin.Focus();
                    return -1;
                }
            }

            else if (this.radInpatientNo.Checked)
            {
                if (this.txtInpatientNo.Text == "")
                {
                    MessageBox.Show("������סԺ�ţ�");
                    this.txtInpatientNo.Focus();
                    return -1;
                }
                this.QueryByPatientNo();
            }

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// �����Ҳ�ѯ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="state"></param>
        /// <returns>0 �ɹ��� -1 ʧ�� �� -2 û�з��ϵĻ���</returns>
        private int  QueryByDept(string deptCode, FS.HISFC.Models.RADT.InStateEnumService state)
        {
            this.fpSheetView.Columns[0].Label = "סԺ��";
            this.fpSheetView.Columns[1].Label = "����";
            ArrayList alPatients = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QueryPatientByDept(deptCode, state);
            if (alPatients == null || alPatients.Count == 0)
            {
                return -1;
            }

            this.QueryQcDate(alPatients);
            return 0;
        }

        /// <summary>
        /// ����Ժ����Ժʱ���ѯ
        /// </summary>
        /// <param name="alPatients"></param>
        private int QueryByInDate()
        {
            ArrayList alPatients = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QuereyPatientByDate(this.dtpBegin.Value, this.dtpEnd.Value);
            if (alPatients == null || alPatients.Count == 0)
            {
                return -1;
            }
            this.QueryQcDate(alPatients);
            return 0;
        }

        /// <summary>
        /// ��סԺ�Ų�ѯ
        /// </summary>
        private void QueryByPatientNo()
        {
            FS.HISFC.Models.RADT.PatientInfo patient = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QueryPatientInfoByInpatientNO(this.txtInpatientNo.Text);
            if (patient != null)
            {

                this.fpSheetView.Rows.Add(0, 1);
                this.fpSheetView.Rows[0].Tag = patient;
                this.fpSheetView.Cells[0, 0].Text = patient.PID.PatientNO;
                this.fpSheetView.Cells[0, 1].Text = patient.Name;

                int column = 2;
                foreach (FS.HISFC.Models.EPR.QCConditions condition in this.alConditions)
                {
                    bool isAccord = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.ExecQCInfo(patient.PID.ID, Common.Classes.Function.ISql, condition);
                    if (isAccord)
                    {

                        this.fpSheetView.Cells[0, column].ForeColor = Color.Red;
                        try
                        {
                            this.fpSheetView.Cells[0, column].Text = "��";
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        this.fpSheetView.Cells[0, column].Text = "��";
                    }

                    column++;
                }
            }
        }

        /// <summary>
        /// �ʿ���Ϣ��ѯ
        /// </summary>
        /// <param name="alPatients"></param>
        private void QueryQcDate(ArrayList alPatients)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�ʿ���Ϣ�����Ժ�");
            Application.DoEvents();

            int count = 0;
            this.fpSheetView.RowCount = 0;

            foreach (FS.HISFC.Models.RADT.PatientInfo patient in alPatients)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(count, alPatients.Count);
                count++;
                Application.DoEvents();

                this.fpSheetView.Rows.Add(0, 1);
                this.fpSheetView.Rows[0].Tag = patient;
                this.fpSheetView.Cells[0, 0].Text = patient.PID.PatientNO;
                this.fpSheetView.Cells[0, 1].Text = patient.Name;

                int column = 2;
                //add by pantiejun 2008-4-1
                ArrayList alSearchCondition = this.alSelectedCondition == null ? this.alConditions : this.alSelectedCondition;
                //foreach (FS.HISFC.Models.EPR.QCConditions condition in this.alConditions)
                foreach (FS.HISFC.Models.EPR.QCConditions condition in alSearchCondition)////modify by pantiejun 2008-4-1
                {
                    bool isAccord = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.ExecQCInfo(patient.PID.ID, Common.Classes.Function.ISql, condition);
                    if (isAccord)
                    {

                        this.fpSheetView.Cells[0, column].ForeColor = Color.Red;
                        try
                        {
                            this.fpSheetView.Cells[0, column].Text = "��";
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        this.fpSheetView.Cells[0,column].Text="��";
                    }

                    column++;
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();//add by pantiejun 2008-4-1
        }


        #endregion
    }
}
