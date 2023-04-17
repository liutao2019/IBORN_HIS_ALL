using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmSearchPatient : Form
    {
        public frmSearchPatient()
        {
            InitializeComponent();
        }

        #region ȫ�ֱ���
        //���˻�����Ϣ��
        private DataTable patientTable;
        private DataView patientView;
        // �����ļ�·�� 
        private string FilePath = Application.StartupPath + "\\profile\\SearchPatient.xml";
        //˫���¼� �Ƿ�رմ���
        //private FS.Common.Class.FormStyleInfo dcEvent;
        #endregion

        #region �Զ����¼�
        //�����й��¼�
        public delegate void SaveHandel(FS.HISFC.Models.RADT.PatientInfo pa);
        public event SaveHandel SaveInfo;
        #endregion

        #region ����

        /// <summary>
        /// ��ѯ������������Ϣ
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        private void SearchInfo()
        {
            try
            {
                FS.HISFC.BizLogic.RADT.InPatient inPatient = new FS.HISFC.BizLogic.RADT.InPatient();
                string strWhere = this.ucCustomQuery1.GetWhereString();
                if (strWhere == "")
                {
                    MessageBox.Show("�������ѯ������");
                    return;
                }
                else
                {
                    strWhere = " where " + strWhere;
                }
                //�ȴ�����
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                Application.DoEvents();
                ArrayList list = inPatient.PatientInfoGet(strWhere);
                if (list == null)
                {
                    MessageBox.Show("��ѯ����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                //��������
                InsertInfo(list);
                //����fpSpread1 ������
                if (System.IO.File.Exists(FilePath))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, FilePath);
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ʵ���� Table ��ѯ���� ���������
        /// zhangjunyi@FS.com 2005.6.29 
        /// </summary>
        private void LoadAndAddDateToTable()
        {
            patientTable = new DataTable("���˻�����Ϣ��");
            //��������ļ�����,ͨ�������ļ�����DataTable dtICD����Ϣ,����fp
            if (File.Exists(FilePath))
            {
                //����DataTable
                //FS.Common.Class.Function.CreatColumnByXML(FilePath, patientTable, ref patientView, this.neuSpread1_Sheet1);
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(FilePath, patientTable, ref patientView,this.neuSpread1_Sheet1);
                //��������Ϊsequence_no��
                CreateKeys(patientTable);
            }
            else//��������ļ�������,�������������ļ�
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);

                patientTable.Columns.AddRange(new DataColumn[]{new DataColumn("סԺ��ˮ��", strType),
																  new DataColumn("סԺ����", intType),
																  new DataColumn("סԺ��", strType),
																  new DataColumn("����", strType),
																  new DataColumn("�Ա�", strType),
																  new DataColumn("���֤��", strType),
                                                                  new DataColumn("�������",strType),
																  new DataColumn("����", dtType),
																  new DataColumn("������λ", strType),
																  new DataColumn("�����绰", strType),
																  new DataColumn("��λ�ʱ�", strType),
																  new DataColumn("���ڻ��ͥ��ַ", strType),
																  new DataColumn("��ͥ�绰", strType),
																  new DataColumn("���ڻ��ͥ�ʱ�", strType),
																  new DataColumn("����", strType),
																  new DataColumn("����", strType),
																  new DataColumn("��ϵ��", strType),
																  new DataColumn("��ϵ�˵绰", strType),
																  new DataColumn("��ϵ�˵�ַ", strType),
																  new DataColumn("��ϵ�˹�ϵ", strType),
																  new DataColumn("����״��", strType),
																  new DataColumn("��Ժ����",dtType),
																  new DataColumn("Ѫ��", strType),
																  new DataColumn("����", strType),
																  new DataColumn("��Ժ����", dtType),
																  new DataColumn("���￨��", strType),
																  new DataColumn("ҽ��֤��", strType),
																  new DataColumn("��ͬ��λ", strType)});

                //��������Ϊsequence_no��
                CreateKeys(patientTable);
                patientView = new DataView(patientTable);
                this.neuSpread1_Sheet1.DataSource = patientView;
                //��������Ŀ��
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, FilePath);
                
            }

            //����fpSpread1 ������
            if (System.IO.File.Exists(FilePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, FilePath);
            }
        }

        /// <summary>
        ///����FarPoint ������
        ///zhangjunyi@FS.com 2005.6.29
        /// </summary>
        private void SetUp(string filePath)
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn uc = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            uc.FilePath = filePath;
            if (filePath == FilePath)
            {
                //uc.GoDisplay += new FS.Common.Controls.ucSetCol.DisplayNow(uc_GoDisplay);
            }
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }
        /// <summary>
        /// ˢ�»�����Ϣ
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        private void uc_GoDisplay()
        {
            //			SearchInfo();
        }
        /// <summary>
        /// ��table�в������ݡ�
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        /// <param name="alReturn"> Ҫ���������</param>
        private void InsertInfo(ArrayList alReturn)
        {
            if (patientTable != null)
            {
                patientTable.Clear();
            }
            //ѭ��������Ϣ
            foreach (FS.HISFC.Models.RADT.PatientInfo obj in alReturn)
            {
                DataRow row = patientTable.NewRow();
                SetRow(obj, row);
                patientTable.Rows.Add(row);
                patientTable.AcceptChanges();
            }
        }
        /// <summary>
        /// ��Table ����Ӽ�һ��
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <param name="row"></param>
        private void SetRow(FS.HISFC.Models.RADT.PatientInfo PatientInfo, DataRow row)
        {
            row["סԺ��ˮ��"] = PatientInfo.ID;
            row["סԺ��"] = PatientInfo.PID.PatientNO;
            row["����"] = PatientInfo.Name;
            if (PatientInfo.Sex.ID.ToString() == "M")
            {
                row["�Ա�"] = "��";
            }
            else if (PatientInfo.Sex.ID.ToString() == "F")
            {
                row["�Ա�"] = "Ů";
            }
            else
            {
                row["�Ա�"] = "δ֪";
            }
            row["���֤��"] = PatientInfo.IDCard;
            row["����"] = PatientInfo.Birthday;
            row["������λ"] = PatientInfo.CompanyName;
            row["�����绰"] = PatientInfo.PhoneBusiness;
            row["��λ�ʱ�"] = PatientInfo.BusinessZip;
            row["���ڻ��ͥ��ַ"] = PatientInfo.AddressHome;
            row["��ͥ�绰"] = PatientInfo.PhoneHome;
            row["���ڻ��ͥ�ʱ�"] = PatientInfo.HomeZip;
            row["����"] = PatientInfo.DIST;
            row["����"] = PatientInfo.Nationality.ID;
            row["��ϵ��"] = PatientInfo.Kin.Name;
            row["��ϵ�˵绰"] = PatientInfo.Kin.Memo;
            row["��ϵ�˵�ַ"] = PatientInfo.Kin.User01;
            row["��ϵ�˹�ϵ"] = PatientInfo.Kin.Relation.Name;
            row["����״��"] = PatientInfo.MaritalStatus.Name;
            row["��Ժ����"] = PatientInfo.PVisit.InTime;
            row["Ѫ��"] = PatientInfo.BloodType.Name;
            row["����"] = PatientInfo.PVisit.PatientLocation.Dept.Name;
            row["סԺ����"] =PatientInfo.InTimes;
            row["�������"] = PatientInfo.ClinicDiagnose;
            row["��Ժ����"] = PatientInfo.PVisit.OutTime;
            row["���￨��"] = PatientInfo.PID.CardNO;
            row["ҽ��֤��"] = PatientInfo.SSN;
            row["��ͬ��λ"] = PatientInfo.Pact.Name;
        }

        /// <summary>
        /// ����������
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        /// <param name="table"></param>
        private void CreateKeys(DataTable table)
        {
            DataColumn[] keys = new DataColumn[] { table.Columns["סԺ��ˮ��"] };
            table.PrimaryKey = keys;
        }

        /// <summary>
        /// �Զ����ݼ�
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.F.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //��ѯ
                this.SearchInfo();
            }
            if (keyData.GetHashCode() == Keys.R.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //����
            }
            if (keyData.GetHashCode() == Keys.S.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //����
                SetUp(FilePath);
            }
            if (keyData.GetHashCode() == Keys.X.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //�ر�
                this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ���п�ȷ����仯ʱ,�洢�������ļ�
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (File.Exists(FilePath))
            {
                //FS.neuFC.Interface.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, FilePath);
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, FilePath);
            }
        }

        /// <summary>
        /// �ؼ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            this.ucCustomQuery1.btnReset_Click(sender, e);
        }

        private int GetColumnId(string str)
        {
            foreach (FarPoint.Win.Spread.Column col in neuSpread1_Sheet1.Columns)
            {
                if (col.Label == str)
                {
                    return col.Index;
                }
            }
            return 0;
        }
        #endregion

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (neuSpread1_Sheet1.Rows.Count < 1)
            {
                //û������ ���� 
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            //ȡ��סԺ��ˮ�� 
            patient.ID = neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, GetColumnId("סԺ��ˮ��")].Text;
            FS.HISFC.BizLogic.RADT.InPatient inPatient = new FS.HISFC.BizLogic.RADT.InPatient();
            patient = inPatient.QueryPatientInfoByInpatientNO(patient.ID);
            //�����¼�
            try
            {
                SaveInfo(patient);
            }
            catch (Exception ex)
            {
                string Err = ex.Message;
            }
            this.Close();
            //if (dcEvent == FS.Common.Class.FormStyleInfo.DCAutoClose)
            //{
            //    this.Close();
            //}
        }

        private void frmSearchPatient_Load(object sender, EventArgs e)
        {
            //����Table�Ľṹ
            LoadAndAddDateToTable();
        }

        private void neuToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (this.neuToolBar1.Buttons.IndexOf(e.Button))
            {
                case 0:
                    //��ѯ
                    SearchInfo();
                    break;
                case 1:

                    //����
                    break;
                case 2:
                    SetUp(FilePath);
                    //����
                    break;
                case 3: //guan�ر�
                    this.Close();
                    break;
            }
        }
    }
}