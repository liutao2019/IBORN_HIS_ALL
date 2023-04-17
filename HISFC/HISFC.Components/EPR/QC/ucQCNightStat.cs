using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace FS.HISFC.Components.EPR.QC
{
    public partial class ucQCNightStat : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQCNightStat()
        {
            InitializeComponent();
        }
        #region ����
        /// <summary>
        /// ��ʱ������̽��ʱ���Ƿ�ָ��ʱ��
        /// </summary>
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        /// <summary>
        /// �����ʿ�����
        /// </summary>
        private ArrayList alConditions;
        /// <summary>
        /// ѡ����ʿ�����
        /// </summary>
        private ArrayList alSelectedConditions;
        /// <summary>
        /// ����
        /// </summary>
        private ArrayList alDepts;
        /// <summary>
        /// ������Ա
        /// </summary>
        private FS.HISFC.Models.Base.Employee person = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
        /// <summary>
        /// �����ʿ�����ID
        /// </summary>
        private ArrayList alConditionIDs;
        /// <summary>
        /// ͳ��ʱ�䣬Ϊ�˷�ֹĳ�����߲�ѯʱ�����ڼ���������23��59��ʼ��ѯĳ���ߣ��п��ܴ��ղ��ܼ����꣬����������ʾ�����鷳(һ�����ߵ�һ��ͳ�Ʋ�����ʾ��ͬһ�У�ֻ�ܸ���patienNO��ͳ��ʱ��ȷ����ĳ�����ߵ�ͬһ��ͳ��)���������ݿ��е�ͳ��ʱ����ҳ��ָ������Ϊû��ʹ��Sysdate
        /// </summary>
        private DateTime statTime;
        #endregion

        #region ��ʼ��
        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                this.alDepts = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
                this.cmbDept.AddItems(this.alDepts);
                FS.HISFC.Models.RADT.InStateEnumService instate = new FS.HISFC.Models.RADT.InStateEnumService();

                this.cmbState.AddItems(FS.HISFC.Models.RADT.InStateEnumService.List());

                this.alConditions = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetQCConditionList();
                string conditionXml = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetSetting("1");
                if (!string.IsNullOrEmpty(conditionXml))
                {
                    try
                    {
                        System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(ArrayList), new Type[] { typeof(FS.HISFC.Models.EPR.QCCondition), typeof(FS.HISFC.Models.EPR.QCConditions), typeof(FS.FrameWork.Models.NeuObject) });
                        System.IO.StringReader sr = new System.IO.StringReader(conditionXml);
                        this.alSelectedConditions = xs.Deserialize(sr) as ArrayList;
                    }
                    catch { }
                }
                if (this.alConditions != null)
                {
                    this.alConditionIDs = new ArrayList();
                    this.neuFpEnter1_Sheet1.ColumnHeader.Rows[0].Height = 59F;
                    this.neuFpEnter1_Sheet1.ColumnCount = this.alConditions.Count + 3;
                    this.neuFpEnter1_Sheet1.Columns[0].Label = "���߱���";
                    this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.neuFpEnter1_Sheet1.Columns[1].Label = "��������";
                    this.neuFpEnter1_Sheet1.Columns[1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.neuFpEnter1_Sheet1.Columns[2].Label = "ͳ��ʱ��";
                    for (int i = 0; i < this.alConditions.Count; i++)
                    {
                        this.neuFpEnter1_Sheet1.Columns[i+3].Label = this.alConditions[i].ToString();
                        this.alConditionIDs.Add((this.alConditions[i] as FS.HISFC.Models.EPR.QCConditions).ID);
                    }
                    //this.neuFpEnter1_Sheet1.Columns[2].Width = 0;
                    //this.neuFpEnter1_Sheet1.Columns[2].Visible = false;
                }
                this.timer.Interval = 1000;//1��
                this.timer.Tick += new EventHandler(timer_Tick);

            }
            base.OnLoad(e);
        }
        #endregion
        /// <summary>
        /// ִ�е��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btExecute_Click(object sender, EventArgs e)
        {
            //ִ��ʱ�䲻�ܴ���24��С��0���Ҳ���8��20��
            if (!((this.neuDpExecute.Value.Hour >= 0 && this.neuDpExecute.Value.Hour <= 7) || (this.neuDpExecute.Value.Hour >= 21 && this.neuDpExecute.Value.Hour <= 23)))
            {
                MessageBox.Show("�뽫ʱ��������21��23��0��7��֮�䡣");
                return;
            }
            if (this.alConditions == null && this.alSelectedConditions ==null)
            {
                MessageBox.Show("��ȡ�ʿ����������޷�ִ�в�����");
                return;
            }
            this.timer.Start();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour == this.neuDpExecute.Value.Hour && DateTime.Now.Minute == this.neuDpExecute.Value.Minute)
            {                
                this.statTime = System.DateTime.Now;
                this.timer.Stop();
                Thread thread = new Thread(new ThreadStart(this.ExecuteQuery));
                thread.Start();
            }
        }
        private void ExecuteQuery()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�ʿ���Ϣ�����Ժ�");

            ArrayList alSearchConditions = this.alSelectedConditions == null ? this.alConditions : this.alSelectedConditions;

            ArrayList alPatients = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.PatientInfoGet("");
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in alPatients)
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = patient.ID;//���߱���
                obj.Memo = patient.PVisit.InTime.ToString();//��Ժʱ�� 
                
                obj.User02 = this.person.ID;//������Ա���� 
                obj.User03 = this.statTime.ToString();//ִ��ʱ��
                foreach (FS.HISFC.Models.EPR.QCConditions condition in alSearchConditions)
                {
                    obj.Name = condition.ID;//�ʿ�ID
                    bool isAccord = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.ExecQCInfo(patient.PID.ID, Common.Classes.Function.ISql, condition);
                    if (isAccord)
                    {

                        obj.User01 = "0";//ָ�ؽ��
                        //"X";
                    }
                    else
                    {
                        obj.User01 = "1";//ָ�ؽ��
                        //"��"
                    }
                    FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.InsertQCStat(obj);

                    //column++;
                } 

            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();


        }
        public override int Export(object sender, object neuObject)
        {
            SaveFileDialog form = new SaveFileDialog();
            form.Filter = "*.xls|*.xls";
            form.ShowDialog();
            this.neuFpEnter1.SaveExcel(form.FileName);
            return 0;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.btSearch_Click(sender,null);
            return base.OnQuery(sender, neuObject);
        }
        private void btSearch_Click(object sender, EventArgs e)
        {
            if (this.neuFpEnter1_Sheet1.Rows.Count > 0)
            {
                this.neuFpEnter1_Sheet1.Rows.Remove(0, this.neuFpEnter1_Sheet1.Rows.Count); ;//.ClearRange(0, 0, this.neuFpEnter1_Sheet1.Rows.Count, this.neuFpEnter1_Sheet1.Columns.Count, true);
            }
            if (this.radDept.Checked)
            {
                if (this.cmbDept.Tag == null || this.cmbState.SelectedItem ==null)
                {
                    MessageBox.Show("��ѡ����Һ�״̬��");
                    this.cmbDept.Focus();
                    return;
                }
                else
                {
                    FS.HISFC.Models.RADT.InStateEnumService state = new FS.HISFC.Models.RADT.InStateEnumService();
                    state.ID = this.cmbState.SelectedItem.ID;
                    //this.QueryByDept(this.cmbDept.Tag.ToString(), this.cmbState.SelectedItem as FS.HISFC.Models.RADT.InStateEnumService);
                    this.QueryByDept(this.cmbDept.Tag.ToString(), state);
                }
            }

            else if (this.radInDate.Checked)
            {
                if (this.dtpBegin.Value > this.dtpEnd.Value)
                {
                    MessageBox.Show("��ѯ��ʼʱ�䲻�ܴ��ڽ���ʱ�䣡");
                    this.dtpBegin.Focus();
                    return;
                }
                else
                {
                    this.QueryByInDate();
                }
            }

            else if (this.radInpatientNo.Checked)
            {
                if (this.txtInpatientNo.Text == "")
                {
                    MessageBox.Show("������סԺ�ţ�");
                    this.txtInpatientNo.Focus();
                    return;
                }
                else
                {
                    this.QueryByPatientNO();
                }
            }
            else if (this.radAll.Checked)
            {
                this.QueryAll();
            }
        }
        private void QueryByDept(string deptCode, FS.HISFC.Models.RADT.InStateEnumService state)
        {
            ArrayList alPatients = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QueryPatientByDept(deptCode, state);
            string patienNOs = string.Empty;
            foreach (FS.HISFC.Models.RADT.Patient patient in alPatients)
            {
                patienNOs += ",'" + patient.ID + "'";
            }
            if (!string.IsNullOrEmpty(patienNOs))
            {
                patienNOs = patienNOs.Substring(1); //ȥ��ǰ���"," 
                ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryQCStatByPatientNO(patienNOs);
                this.FillPF(al);
            }
            else
            {
                MessageBox.Show("û�м�����������ݣ�");
                return ;//��������ڵ�patienΪ�������������ֱ�ӷ���
            }

        }
        private void QueryByInDate()
        {
            ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryQCStatByInDate(this.dtpBegin.Value,this.dtpEnd.Value);
            this.FillPF(al);
        }
        private void QueryByStatDate()
        {
            ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryQCStatByStatDate(this.dtpStatBeginDate.Value,this.dtpStatEndDate.Value);
            this.FillPF(al);
        }
        private void QueryByPatientNO()
        {
            ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryQCStatByPatientNO("'"+this.txtInpatientNo.Text+"'");
            this.FillPF(al);
        }
        private void QueryAll()
        {
            ArrayList alPatients = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.PatientInfoGet("");
            string patienNOs = string.Empty;
            foreach (FS.HISFC.Models.RADT.Patient patient in alPatients)
            {
                patienNOs += ",'" + patient.ID+"'";
            }
            if (!string.IsNullOrEmpty(patienNOs))
            {
                patienNOs = patienNOs.Substring(1); //ȥ��ǰ���"," 
                ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryQCStatByPatientNO(patienNOs);
                this.FillPF(al);
            }
            else
            {
                MessageBox.Show("û�м�����������ݣ�");
                return;//��������ڵ�patienΪ�������������ֱ�ӷ���
            }
        }
        private void FillPF(ArrayList alResult)
        {
            if (alResult != null)
            {
                int index = -1;
                foreach (FS.FrameWork.Models.NeuObject result in alResult)
                {
                    if (this.cbStatDate.Checked)
                    {
                        if (this.dtpStatBeginDate.Value < this.dtpStatEndDate.Value)
                        {
                            if (DateTime.Parse(result.User03).Date > this.dtpStatEndDate.Value.Date || DateTime.Parse(result.User03).Date < this.dtpStatBeginDate.Value.Date)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    index = this.alConditionIDs.IndexOf(result.Name);
                    //�ж��Ƿ���ڸû��ߵ�ͳ�Ƽ�¼
                    if (this.neuFpEnter1_Sheet1.Rows.Count <= 0 || this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count-1,0].Text != result.ID
                        || (this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 0].Text == result.ID && this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 2].Text != result.User03))
                    {
                        this.neuFpEnter1_Sheet1.Rows.Add(this.neuFpEnter1_Sheet1.Rows.Count, 1);
                        this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 0].Text = result.ID;
                        this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 1].Text = result.User01;
                        this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, 2].Text = result.User03;
                    }

                    if (index != -1)
                    {
                        this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, index + 3].ForeColor = Color.Red;
                        this.neuFpEnter1_Sheet1.Cells[this.neuFpEnter1_Sheet1.Rows.Count - 1, index + 3].Text = (result.User02 == "0" ? "��" : "��");
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("û�м�����������ݣ�");
            }
        }
    }
}
