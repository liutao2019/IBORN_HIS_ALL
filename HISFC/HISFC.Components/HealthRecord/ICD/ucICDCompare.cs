using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.Components.HealthRecord.ICD
{
    /// <summary>
    /// ucICDCompare<br></br>
    /// [��������: ����ICD����]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucICDCompare :FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucICDCompare()
        {
            InitializeComponent();
        }

        #region  ����

        //�������ĸ��ֶμ���
        private int circle = 0;
        //����Table  �洢ICD10��Ϣ 
        private DataTable dtICD10 = null;
        //���� DataView ����ɸѡICD10 ����
        private DataView dvICD10 = null;
        //�����ļ�·��
        private string filePath10 = Application.StartupPath + "\\profile\\ICD10Compare.xml";
        //���������
        private FS.HISFC.Models.HealthRecord.ICDCompare icdCompare = new FS.HISFC.Models.HealthRecord.ICDCompare();
        //����Table  �洢ICD9��Ϣ 
        private DataTable dtICD9 = null;
        //���� DataView ����ɸѡICD9 ����
        private DataView dvICD9 = null;
        //�����ļ�·��
        private string filePath9 = Application.StartupPath + "\\profile\\ICD9Compare.xml";

        //����Table  �洢ICD10��Ϣ 
        private DataTable dtICDCompare = null;
        //���� DataView ����ɸѡICD���� ����
        private DataView dvICDCompare = null;
        //�����ļ�·��
        private string filePathCompare = Application.StartupPath + "\\profile\\ICDCompare.xml";

        //ICDҵ���
        private FS.HISFC.BizLogic.HealthRecord.ICD myICD = new FS.HISFC.BizLogic.HealthRecord.ICD();
        //		//�����ַ������洢ɸѡ��
        //		private string code ="ƴ����";
        #endregion

        #region ����
        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("���", "���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null); 
            return toolBarService;
        }
        #endregion

        #region ���������Ӱ�ť�����¼�
        /// <summary>
        /// ���������Ӱ�ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    CompareICD();
                    break;
                case "ɾ��":
                    CancelICD();
                    break;
                case "���":
                    ClearICD();
                    break; 
                default:
                    break;
            }
        }
        #endregion

        #endregion

        #region ����ؼ� �¼�
        /// <summary>
        /// �����LOAD�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucICDCompare_Load(object sender, System.EventArgs e)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                Application.DoEvents();

                //��ѯ�����δ���յ�ICD0
                LoadAndAddDateToICD10();
                //��ѯ�����δ���յ�ICD09
                LoadAndAddDateToICD9();
                //��ѯ�����δ���յ�ICDCompare
                LoadAndAddDateToICDCompare();

                this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                this.fpSpread2_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                this.fpSpread3_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ʵ���� Table ��ѯ���� ��������� 
        /// </summary>
        private void LoadAndAddDateToICD10()
        {
            dtICD10 = new DataTable("ICD 10ά��");
            //��������ļ�����,ͨ�������ļ�����DataTable dtICD����Ϣ,����fp
            if (File.Exists(filePath10))
            {
                //����DataTable
                Function.CreatColumnByXML(filePath10, dtICD10, ref dvICD10, this.fpSpread1_Sheet1);
                //��������Ϊsequence_no��
                CreateKeys(dtICD10);
            }
            else//��������ļ�������,�������������ļ�
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);

                dtICD10.Columns.AddRange(new DataColumn[]{new DataColumn("sequence_no", strType),
														   new DataColumn("�����", strType),
														   new DataColumn("ҽ�����Ĵ���", strType),
														   new DataColumn("ͳ�ƴ���", strType),
														   new DataColumn("ƴ����", strType),
														   new DataColumn("�����", strType),
														   new DataColumn("�������", strType),
														   new DataColumn("�ڶ��������", strType),
														   new DataColumn("�����������", strType),
														   new DataColumn("����ԭ��", strType),
														   new DataColumn("��������", strType),
														   new DataColumn("��׼סԺ��", intType),
														   new DataColumn("30�ּ���", boolType),
														   new DataColumn("��Ⱦ��", boolType),
														   new DataColumn("����", boolType),
														   new DataColumn("סԺ�ȼ�", strType),
														   new DataColumn("��Ч��", boolType),
														   new DataColumn("���", strType),
														   new DataColumn("����Ա����", strType),
														   new DataColumn("����Ա", strType),
														   new DataColumn("����ʱ��", dtType)});

                //��������Ϊsequence_no��
                CreateKeys(dtICD10);
                dvICD10 = new DataView(dtICD10);
                this.fpSpread1_Sheet1.DataSource = dvICD10;
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, filePath10);
            }
            ArrayList alReturn = new ArrayList();//���ص�ICD��Ϣ;
            //�����Ч��ICD10��Ϣ
            alReturn = myICD.Query(ICDTypes.ICD10, QueryTypes.Valid);
            if (alReturn == null)
            {
                MessageBox.Show("���ICD��Ϣ����!" + myICD.Err);
                return;
            }
            //ѭ��������Ϣ
            foreach (FS.HISFC.Models.HealthRecord.ICD obj in alReturn)
            {
                DataRow row = dtICD10.NewRow();
                SetRow(obj, row);
                dtICD10.Rows.Add(row);
            }
            dtICD10.AcceptChanges();
            //����fpSpread1 ������
            if (System.IO.File.Exists(filePath10))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, filePath10);
            }
        }
        /// <summary>
        /// ʵ���� Table ��ѯ���� ��������� 
        /// </summary>
        private void LoadAndAddDateToICD9()
        {
            dtICD9 = new DataTable("ICD 9 ά��");
            //��������ļ�����,ͨ�������ļ�����DataTable dtICD����Ϣ,����fp
            if (File.Exists(filePath9))
            {
                //����DataTable
                Function.CreatColumnByXML(filePath9, dtICD9, ref dvICD9, this.fpSpread2_Sheet1);
                //��������Ϊsequence_no��
                CreateKeys(dtICD9);
            }
            else//��������ļ�������,�������������ļ�
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);

                dtICD9.Columns.AddRange(new DataColumn[]{new DataColumn("sequence_no", strType),
															 new DataColumn("�����", strType),
															 new DataColumn("ҽ�����Ĵ���", strType),
															 new DataColumn("ͳ�ƴ���", strType),
															 new DataColumn("ƴ����", strType),
															 new DataColumn("�����", strType),
															 new DataColumn("�������", strType),
															 new DataColumn("�ڶ��������", strType),
															 new DataColumn("�����������", strType),
															 new DataColumn("����ԭ��", strType),
															 new DataColumn("��������", strType),
															 new DataColumn("��׼סԺ��", intType),
															 new DataColumn("30�ּ���", boolType),
															 new DataColumn("��Ⱦ��", boolType),
															 new DataColumn("����", boolType),
															 new DataColumn("סԺ�ȼ�", strType),
															 new DataColumn("��Ч��", boolType),
															 new DataColumn("���", strType),
															 new DataColumn("����Ա����",strType),
															 new DataColumn("����Ա", strType),
															 new DataColumn("����ʱ��", dtType)});

                //��������Ϊsequence_no��
                CreateKeys(dtICD9);
                dvICD9 = new DataView(dtICD9);
                this.fpSpread2_Sheet1.DataSource = dvICD9;
                //��������Ŀ��
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread2_Sheet1, filePath9);
            }
            ArrayList alReturn = new ArrayList();//���ص�ICD��Ϣ;
            //���δ���յ�ICD9��Ϣ
            alReturn = myICD.QueryNoComparedICD9(QueryTypes.Valid);
            if (alReturn == null)
            {
                MessageBox.Show("���ICD��Ϣ����!" + myICD.Err);
                return;
            }
            //ѭ��������Ϣ
            foreach (FS.HISFC.Models.HealthRecord.ICD obj in alReturn)
            {
                DataRow row = dtICD9.NewRow();
                SetRow(obj, row);
                dtICD9.Rows.Add(row);
            }
            dtICD9.AcceptChanges();
            //����fpSpread1 ������
            if (System.IO.File.Exists(filePath9))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread2_Sheet1, filePath9);
            }
        }
        /// <summary>
        /// ʵ���� Table ��ѯ���� ��������� 
        /// </summary>
        private void LoadAndAddDateToICDCompare()
        {
            dtICDCompare = new DataTable("ICD Compare ά��");
            //��������ļ�����,ͨ�������ļ�����DataTable dtICD����Ϣ,����fp
            if (File.Exists(filePathCompare))
            {
                //����DataTable
                Function.CreatColumnByXML(filePathCompare, dtICDCompare, ref dvICDCompare, this.fpSpread3_Sheet1);
                //��������Ϊsequence_no��
                CreateKeys(dtICDCompare);
            }
            else//��������ļ�������,�������������ļ�
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);

                dtICDCompare.Columns.AddRange(new DataColumn[]{   new DataColumn("�����9", strType),
																  new DataColumn("�������9", strType),
																  new DataColumn("�����10", strType),
																  new DataColumn("�������10", strType),
																  new DataColumn("ƴ����", strType),
																  new DataColumn("ͳ�ƴ���", strType),
																  new DataColumn("��Ч��", boolType),
																  new DataColumn("sequence_no", strType),
																  new DataColumn("����Ա����", strType),
																  new DataColumn("����Ա", strType),
																  new DataColumn("����ʱ��", dtType)});

                //��������Ϊsequence_no��
                CreateKeys(dtICDCompare);
                dvICDCompare = new DataView(dtICDCompare);
                this.fpSpread3_Sheet1.DataSource = dvICDCompare;
                //��������Ŀ��
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread3_Sheet1, filePathCompare);
            }
            ArrayList alReturn = new ArrayList();//���ص�ICD��Ϣ;
            //���δ���յ�ICD9��Ϣ
            alReturn = myICD.QueryComparedICD();
            if (alReturn == null)
            {
                MessageBox.Show("���ICD��Ϣ����!" + myICD.Err);
                return;
            }
            //ѭ��������Ϣ
            foreach (FS.HISFC.Models.HealthRecord.ICDCompare obj in alReturn)
            {
                DataRow row = dtICDCompare.NewRow();
                SetRowCompare(obj, row);
                dtICDCompare.Rows.Add(row);
            }
            dtICDCompare.AcceptChanges();
            //����fpSpread1 ������
            if (System.IO.File.Exists(filePathCompare))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread3_Sheet1, filePathCompare);
            }
        }
        /// <summary>
        /// ����ձ����һ��
        /// </summary>
        /// <param name="obj">������Ϣ</param>
        /// <param name="row"></param>
        private void SetRowCompare(FS.HISFC.Models.HealthRecord.ICDCompare obj, DataRow row)
        {
            row["�����9"] = obj.ICD9.ID;
            row["�������9"] = obj.ICD9.Name;

            row["�����10"] = obj.ICD10.ID;
            row["�������10"] = obj.ICD10.Name;

            row["ƴ����"] = obj.ICD9.SpellCode;
            row["ͳ�ƴ���"] = obj.ICD9.UserCode;

            row["��Ч��"] = obj.IsValid;
            row["sequence_no"] = obj.ICD9.KeyCode;
            row["����Ա����"] = obj.OperInfo.ID;
            row["����Ա"] = obj.OperInfo.Name;
            row["����ʱ��"] = obj.OperInfo.OperTime;
        }
        /// <summary>
        /// ��Table ����Ӽ�һ��
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        private void SetRow(FS.HISFC.Models.HealthRecord.ICD obj, DataRow row)
        {
            row["sequence_no"] = obj.KeyCode;
            row["�����"] = obj.ID;
            row["ҽ�����Ĵ���"] = obj.SICode;
            row["ͳ�ƴ���"] = obj.UserCode;
            row["ƴ����"] = obj.SpellCode;
            row["�����"] = obj.WBCode;
            row["�������"] = obj.Name;
            row["�ڶ��������"] = obj.User01;
            row["�����������"] = obj.User02;
            row["����ԭ��"] = obj.DeadReason;
            row["��������"] = obj.DiseaseCode;
            row["��׼סԺ��"] = obj.StandardDays;
            row["30�ּ���"] = FS.FrameWork.Function.NConvert.ToBoolean(obj.Is30Illness);
            row["��Ⱦ��"] = FS.FrameWork.Function.NConvert.ToBoolean(obj.IsInfection);
            row["����"] = FS.FrameWork.Function.NConvert.ToBoolean(obj.IsTumour);
            row["סԺ�ȼ�"] = obj.InpGrade;
            row["��Ч��"] = obj.IsValid;
            row["���"] = obj.SeqNo;
            row["����Ա����"] = obj.OperInfo.ID;
            row["����Ա"] = obj.OperInfo.Name;
            row["����ʱ��"] = obj.OperInfo.OperTime;
        }
        /// <summary>
        ///��������,Ϊ��sequence_no
        /// </summary>
        private void CreateKeys(DataTable table)
        {
            DataColumn[] keys = new DataColumn[] { table.Columns["sequence_no"] };
            table.PrimaryKey = keys;
        }
        /// <summary>
        /// ��ѯICD10 ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textSearchICD10_TextChanged(object sender, System.EventArgs e)
        {
            //ɸѡ
            dvICD10.RowFilter = FilterItem(textSearchICD10.Text);
            //����fpSpread1 ������
            if (System.IO.File.Exists(filePath10))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, filePath10);
            }
        }
        /// <summary>
        /// ��ѯICD9 ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textSearchICD9_TextChanged(object sender, System.EventArgs e)
        {
            //ɸѡ
            dvICD9.RowFilter = FilterItem(textSearchICD9.Text);
            //����fpSpread1 ������
            if (System.IO.File.Exists(filePath9))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread2_Sheet1, filePath9);
            }
        }
        /// <summary>
        /// ��ѯICD���� ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox5_TextChanged(object sender, System.EventArgs e)
        {
            //ɸѡ
            dvICDCompare.RowFilter = FilterItem(textBox5.Text);
            //����fpSpread1 ������
            if (System.IO.File.Exists(filePathCompare))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread3_Sheet1, filePathCompare);
            }
        }
        /// <summary>
        /// �齨ɸѡ�ַ���
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string FilterItem(string input)
        {
            //����Ҫ���ص��ַ���
            string filterString = "";
            try
            {

                filterString = textBox6.Text + " like '" + input + "%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return filterString;
        }
        /// <summary>
        /// ICD9 ��ѯ�Ļس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textSearchICD9_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //��ȡ���ݣ���䵽TexBox��
                AddinfoICD9();
                this.tabControl1.SelectedIndex = 1;
                textSearchICD10.Focus();
            }
        }
        /// <summary>
        /// ȷ�� ��ʾ�ĸ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textSearchICD9_Enter(object sender, System.EventArgs e)
        {
            //��ʾICD9 ����
            this.tabControl1.SelectedIndex = 0;
            this.textSearchICD9.Focus();
        }
        /// <summary>
        /// ȷ�� ��ʾ�ĸ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textSearchICD10_Enter(object sender, System.EventArgs e)
        {
            //��ʾICD10 ����
            this.tabControl1.SelectedIndex = 1;
            this.textSearchICD10.Focus();
        }
        /// <summary>
        /// ȷ�� ��ʾ�ĸ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox5_Enter(object sender, System.EventArgs e)
        {
            //��ʾ���ս���
            this.tabControl1.SelectedIndex = 2;
            this.textBox5.Focus();
        }
        /// <summary>
        /// �س��¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox5_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.tabControl1.SelectedIndex = 0;
                textSearchICD9.Focus();
            }
        }
        /// <summary>
        /// ��ȡICD9���ݣ���䵽TexBox�� �������ݴ�
        /// </summary>
        private void AddinfoICD9()
        {
            if (fpSpread2_Sheet1.Rows.Count == 0)
            {
                return;
            }
            //��ȡҪ���յ�����
            //��ǰ���
            int currRow = fpSpread2_Sheet1.ActiveRowIndex;

            string sICDCode = fpSpread2_Sheet1.Cells[currRow, GetColumnKey(fpSpread2_Sheet1, "�����")].Text;

            if (sICDCode == "" || sICDCode == null)
            {
                return;
            }

            FS.HISFC.Models.HealthRecord.ICD icd9 = new FS.HISFC.Models.HealthRecord.ICD();

            ArrayList al = myICD.IsExistAndReturn(sICDCode, ICDTypes.ICD9, true);

            if (al == null)
            {
                MessageBox.Show("���ICD9��Ϣ����!");
                return;
            }

            icd9 = al[0] as FS.HISFC.Models.HealthRecord.ICD;

            //ICD9 ����
            textBoxICD9.Text = icd9.ID;
            //ICD9 ����
            textBoxICD9Name.Text = icd9.Name;
            //ICD ƴ��
            textBoxICD9.Tag = icd9;
        }
        /// <summary>
        /// ��ȡICD9���ݣ���䵽TexBox��
        /// </summary>
        private void AddInfoICD10()
        {
            if (fpSpread1_Sheet1.Rows.Count == 0)
            {
                return;
            }
            //��ȡҪ���յ�����
            //��ǰ���
            int currRow = fpSpread1_Sheet1.ActiveRowIndex;

            string sICDCode = fpSpread1_Sheet1.Cells[currRow, GetColumnKey(fpSpread1_Sheet1, "�����")].Text;

            if (sICDCode == "" || sICDCode == null)
            {
                return;
            }

            FS.HISFC.Models.HealthRecord.ICD icd10 = new FS.HISFC.Models.HealthRecord.ICD();

            ArrayList al = myICD.IsExistAndReturn(sICDCode, ICDTypes.ICD10, true);

            if (al == null)
            {
                MessageBox.Show("���ICD10��Ϣ����!");
                return;
            }

            icd10 = al[0] as FS.HISFC.Models.HealthRecord.ICD;

            //ICD9 ����
            textBoxICD10.Text = icd10.ID;
            //ICD9 ����
            textBoxICD10Name.Text = icd10.Name;
            //ICD ƴ��
            textBoxICD10.Tag = icd10;
        }
        /// <summary>
        /// ICD10 ��ѯ�Ļس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textSearchICD10_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //��ȡ���� ����䵽TextBox��
                AddInfoICD10();
                this.tabControl1.SelectedIndex = 2;
                textBox5.Focus();
            }
        }
        /// <summary>
        /// ��ѯ������λ��
        /// </summary>
        /// <returns></returns>
        private int GetColumnKey(FarPoint.Win.Spread.SheetView view, string str)
        {
            try
            {
                foreach (FarPoint.Win.Spread.Column col in view.Columns)
                {
                    if (col.Label == str)
                    {
                        return col.Index;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// ˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //��ȡ���ݣ���䵽TexBox��
            AddinfoICD9();
            //��ʾICD
            this.tabControl1.SelectedIndex = 1;
        }

        /// <summary>
        ///˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //��ȡ���ݣ���䵽TexBox��
            AddInfoICD10();
            //��ʾICDCompare
            this.tabControl1.SelectedIndex = 2;
        }
        #region ���浥Ԫ��Ŀ��
        /// <summary>
        /// ����ICD9�Ŀ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread2_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            //����fpSPread2�ı� ��Ŀ�ȡ�
            if (File.Exists(filePath9))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(fpSpread2_Sheet1, filePath9);
            }
        }

        /// <summary>
        /// ����ICD10�Ŀ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            //����fpSPread1�ı� ��Ŀ�ȡ�
            if (File.Exists(filePath10))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(fpSpread1_Sheet1, filePath10);
            }
        }

        /// <summary>
        /// ����ICDCompare�Ŀ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread3_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            //����fpSPread2�ı� ��Ŀ�ȡ�
            if (File.Exists(filePathCompare))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(fpSpread3_Sheet1, filePathCompare);
            }
        }
        #endregion
        #region  ֧�� ���¼�
        private void textSearchICD9_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                //�趨 �ƶ����ٸ����һ��
                this.fpSpread2.SetViewportTopRow(0, fpSpread2_Sheet1.ActiveRowIndex - 5);
                //��ǰλ�������ƶ�һ��
                this.fpSpread2_Sheet1.ActiveRowIndex--;
                this.fpSpread2_Sheet1.AddSelection(fpSpread2_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                //�趨 �ƶ����ٸ����һ��
                this.fpSpread2.SetViewportTopRow(0, fpSpread2_Sheet1.ActiveRowIndex - 5);
                //��ǰλ�������ƶ�һ��
                this.fpSpread2_Sheet1.ActiveRowIndex++;
                this.fpSpread2_Sheet1.AddSelection(fpSpread2_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
        }

        private void textSearchICD10_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                //�趨 �ƶ����ٸ����һ��
                this.fpSpread1.SetViewportTopRow(0, fpSpread1_Sheet1.ActiveRowIndex - 5);
                //��ǰλ�������ƶ�һ��
                this.fpSpread1_Sheet1.ActiveRowIndex--;
                this.fpSpread1_Sheet1.AddSelection(fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                //�趨 �ƶ����ٸ����һ��
                this.fpSpread1.SetViewportTopRow(0, fpSpread1_Sheet1.ActiveRowIndex - 5);
                //��ǰλ�������ƶ�һ��
                this.fpSpread1_Sheet1.ActiveRowIndex++;
                this.fpSpread1_Sheet1.AddSelection(fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
            }

        }

        private void textBox5_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                //�趨 �ƶ����ٸ����һ��
                this.fpSpread3.SetViewportTopRow(0, fpSpread3_Sheet1.ActiveRowIndex - 5);
                //��ǰλ�������ƶ�һ��
                this.fpSpread3_Sheet1.ActiveRowIndex--;
                this.fpSpread3_Sheet1.AddSelection(fpSpread3_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                //�趨 �ƶ����ٸ����һ��
                this.fpSpread3.SetViewportTopRow(0, fpSpread3_Sheet1.ActiveRowIndex - 5);
                //��ǰλ�������ƶ�һ��
                this.fpSpread3_Sheet1.ActiveRowIndex++;
                this.fpSpread3_Sheet1.AddSelection(fpSpread3_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
        }
        #endregion
        #endregion

        #region  �Զ��庯��
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //
            if (keyData == Keys.F2)
            {
                circle++;

                switch (circle)
                {
                    case 0:
                        textBox6.Text = "ƴ����";
                        break;
                    case 1:
                        textBox6.Text = "ͳ�ƴ���";
                        break;
                    case 2:
                        textBox6.Text = "�����";
                        break;
                    case 3:
                        textBox6.Text = "�����";
                        break;
                }

                if (circle == 2)
                {
                    circle = -1;
                }
            }
            //int AltKey = Keys.Alt.GetHashCode();
            //if (keyData.GetHashCode() == AltKey + Keys.C.GetHashCode())
            //{
            //    //����
            //    CompareICD();
            //}

            //if (keyData.GetHashCode() == AltKey + Keys.D.GetHashCode())
            //{
            //    //ȡ��
            //    CancelICD();
            //}

            //if (keyData.GetHashCode() == AltKey + Keys.L.GetHashCode())
            //{
            //    //���
            //    ClearICD();
            //}

            //if (keyData.GetHashCode() == AltKey + Keys.H.GetHashCode())
            //{
            //    //����
            //}

            //if (keyData.GetHashCode() == AltKey + Keys.X.GetHashCode())
            //{
            //    //�˳�
            //    this.FindForm().Close();
            //}

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        ///����FarPoint ������
        /// </summary>
        private void SetUp(string filePath)
        {
            Common.Controls.ucSetColumn uc = new Common.Controls.ucSetColumn();
            uc.FilePath = filePath;
            if (filePath == filePath9)
            {
                //uc.DisplayEvent += new EventHandler(uc_GoDisplay9);
            }
            else if (filePath == filePath10)
            {
                //uc.DisplayEvent += new EventHandler(uc_GoDisplay10);// Common.Controls.ucSetColumn.DisplayNow(uc_GoDisplay10);
            }
            else
            {
                //uc.DisplayEvent += new EventHandler(uc_GoDisplayCompare);// Common.Controls.ucSetColumn.DisplayNow(uc_GoDisplayCompare);
            }
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }
        /// <summary>
        /// ˢ��ICD9
        /// </summary>
        private void uc_GoDisplay9()
        {
            LoadAndAddDateToICD9();
        }
        /// <summary>
        /// ˢ��ICD10
        /// </summary>
        private void uc_GoDisplay10()
        {
            LoadAndAddDateToICD10();
        }
        /// <summary>
        /// ˢ�¶��ձ�
        /// </summary>
        private void uc_GoDisplayCompare()
        {
            LoadAndAddDateToICDCompare();
        }
        /// <summary>
        /// �ж���Ч��
        /// </summary>
        /// <returns></returns>
        private bool ISValid()
        {
            if (textBoxICD9.Text == "")
            {
                MessageBox.Show("��ѡ��δ�Ե�ICD9");
                return false;
            }
            if (textBoxICD10.Text == "")
            {
                MessageBox.Show("��ѡ��ICD10");
                return false;
            }
            return true;
        }
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.HealthRecord.ICDCompare GetInfo()
        {
            //�������
            FS.HISFC.Models.HealthRecord.ICDCompare info = new FS.HISFC.Models.HealthRecord.ICDCompare();
            try
            {
                //ICD9 �ı���
                info.ICD9 = textBoxICD9.Tag as FS.HISFC.Models.HealthRecord.ICD;

                info.ICD10 = textBoxICD10.Tag as FS.HISFC.Models.HealthRecord.ICD;

                info.IsValid = true;
                //����Ա
                info.OperInfo.ID = myICD.Operator.ID;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                info = null;
            }
            return info;
        }
        /// <summary>
        /// ICD����
        /// </summary>
        private void CompareICD()
        {
            try
            {
                //������֤ʧ�� 
                if (!ISValid())
                {
                    return;
                }
                icdCompare = GetInfo();
                if (icdCompare == null)
                {
                    return;
                }
                //��������
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(myICD.Connection);
                ////��ʼ����
                //t.BeginTransaction();
                myICD.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //ִ�в������
                int iReturn = 0; //����ֵ 
                iReturn = myICD.InsertCompare(icdCompare);
                if (iReturn > 0)
                {
                    //�ύ
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //��ʾ�ո����ӵ���Ϣ
                    this.tabControl1.SelectedIndex = 2;
                    //��ȡ����ʱ��
                    icdCompare.OperInfo.OperTime = myICD.GetDateTimeFromSysDateTime();
                    //����Ա��Ϣ
                    icdCompare.OperInfo.ID = myICD.Operator.ID;
                    icdCompare.OperInfo.Name = myICD.Operator.Name;
                    //��ICD9 �б� ��ɾ�� ���������
                    DeleteICD9(icdCompare.ICD9);
                    //���ڽ����� ���Ӷ�����Ϣ��
                    AddICDCompare();
                    icdCompare = null;
                    //׼���´�����
                    //�������
                    ClearICD();
                    MessageBox.Show("������ճɹ�");
                    //					//ָ����ʾ����
                    //					this.tabControl1.SelectedIndex = 1;
                    //					//ָ�����λ��
                    //					textSearchICD9.Focus();
                }
                else
                {
                    //����
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(myICD.Err + " �������ʧ��");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ��ICD9 �б� ��ɾ�� ���������
        /// </summary>
        private void DeleteICD9(FS.HISFC.Models.HealthRecord.ICD obj)
        {
            if (dtICD9.Rows.Count < 1)
            {
                //û�����ݿ�ɾ��
                return;
            }
            object[] findObj = new object[] { obj.KeyCode };

            DataRow row = this.dtICD9.Rows.Find(findObj);

            if (row == null)
            {
                MessageBox.Show("����ICD��Ϣ����");
                return;
            }

            dtICD9.Rows.Remove(row);
        }
        /// <summary>
        /// ���ڽ����� ���Ӷ�����Ϣ��
        /// </summary>
        private void AddICDCompare()
        {
            //����һ����
            DataRow row = dtICDCompare.NewRow();
            SetRowCompare(icdCompare, row);
            //���ӵ�����
            dtICDCompare.Rows.Add(row);
            //��������
            dtICDCompare.AcceptChanges();
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        private void CancelICD()
        {
            this.tabControl1.SelectedIndex = 2;
            //ɾ���Ѿ����յ���Ϣ
            if (fpSpread3_Sheet1.Rows.Count < 1)
            {
                return;
            }
            DialogResult result = MessageBox.Show("ȷ��Ҫɾ���ö���", "ɾ��", MessageBoxButtons.YesNo);
            //����Ƿ����˳�
            if (result == DialogResult.No)
            {
                return;
            }
            //��ȡ��ǰ���
            int currRow = fpSpread3_Sheet1.ActiveRowIndex;
            //ICD ����
            string icdCode = fpSpread3_Sheet1.Cells[currRow, GetColumnKey(fpSpread3_Sheet1, "�����")].Text;
            //���к� 
            string KeyCode = fpSpread3_Sheet1.Cells[currRow, GetColumnKey(fpSpread3_Sheet1, "sequence_no")].Text;
            //�������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(myICD.Connection);
            ////��ʼ����
            //t.BeginTransaction();

            myICD.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int intResult = myICD.DeleteCompared(icdCode);
            if (intResult < 1)
            {
                //����ʧ�ܣ����˲���
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(myICD.Err + " ɾ������ʧ��");
            }
            else
            {
                //�����ɹ����ύ
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("ɾ���ɹ�");
                //�ӽ�����ɾ��������Ϣ
                DeleteICDCompare(KeyCode);
                //��ICD9������һ�С�
                AddICD9(icdCode);
            }
        }
        /// <summary>
        /// ɾ�����ձ��е�һ����¼
        /// </summary>
        /// <param name="code"></param>
        private void DeleteICDCompare(string code)
        {
            try
            {
                object[] findObj = new object[] { code };
                DataRow row = dtICDCompare.Rows.Find(findObj);
                if (row == null)
                {
                    MessageBox.Show("����ICD������Ϣ����");
                    return;
                }

                dtICDCompare.Rows.Remove(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ��δ���յ�ICD9������һ��
        /// </summary>
        private void AddICD9(string code)
        {
            try
            {
                //���嶯̬���� �洢
                ArrayList alReturn = new ArrayList();
                //����ҵ���ʵ����
                FS.HISFC.Models.HealthRecord.ICD orgICD = new FS.HISFC.Models.HealthRecord.ICD();
                //��ѯ ��Ӧ��ICD9��Ϣ 
                alReturn = myICD.IsExistAndReturn(code, ICDTypes.ICD9, true);
                if (alReturn == null)
                {
                    MessageBox.Show("���ICD��Ϣ����!" + myICD.Err);
                    return;
                }
                try
                {
                    orgICD = alReturn[0] as FS.HISFC.Models.HealthRecord.ICD;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("���ICD��Ϣ����!" + ex.Message);
                    return;
                }
                DataRow row = dtICD9.NewRow();
                SetRow(orgICD, row);
                dtICD9.Rows.Add(row);
                dtICD9.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        private void ClearICD()
        {
            textBoxICD9.Text = "";  //���ICD9����
            textBoxICD9.Tag = null;  //���ƴ����
            textBoxICD9Name.Text = ""; //���ICD9����
            textBoxICD9Name.Tag = null;  //����Զ�����
            textBoxICD10.Text = ""; //���ICD10����
            textBoxICD10Name.Text = "";//���ICD10����
            textBoxICD10.Tag = null;
            textSearchICD9.Text = "";  //��ռ�����Ϣ
            textSearchICD10.Text = "";//��ռ�����Ϣ
            textBox5.Text = ""; //��ռ�����Ϣ
        }
        #endregion		
    }
}
