using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.Components.HealthRecord.ICD
{
    /// <summary>
    /// ucICDMaint<br></br>
    /// [��������: ����ICDά����Ϣ¼��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucICDMaint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucICDMaint()
        {
            InitializeComponent();
        }

        private string icdVersion = "10";
        /// <summary>
        /// ���ص�ICD�汾10 ICD10 11����ICD
        /// </summary>
        [Category("��������"), Description("���ص�ICD�汾10 ICD10 11����ICD")]
        public string IcdVersion
        {
            get { return this.icdVersion; }
            set { this.icdVersion = value; }
        }

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
            toolBarService.AddToolButton("����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("�޸�", "�޸�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            //toolBarService.AddToolButton("��ѯ(&Q))", "��ѯ(&Q)", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            //toolBarService.AddToolButton("����", "����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C���, true, false, null);
            toolBarService.AddToolButton("����ICD", "����ICD", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, true, false, null);
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
                    InsertRow();
                    break;
                case "�޸�":
                    ModifyInfo();
                    break;
                case "��ѯ(&Q)":
                    //ClearICD();
                    break;
                case "����ICD":
                    this.QueryIcdFromGD30();
                    break;
                default:
                    break;
            }
        }
        public override int Export(object sender, object neuObject)
        {
            Export();
            return base.Export(sender, neuObject);
        }
        #endregion

        #endregion

        #region �Զ������

        //�����ַ����� ȷ����ʾ��ʽXML	
        private string filePath = Application.StartupPath + "\\profile\\ICDMaint.xml";
        //ȫ�ֱ������洢���ص����� ICD10 ICD9 ����ICD ��Ϣ
        private ICDTypes type;
        private DataSet ds = new DataSet();
        ////����ȫ�ֱ��� DataTable  �洢 ��ѯ���������ݼ�
        private DataTable dtICD = new DataTable();
        //����ȫ�ֱ��� DataView   ������������
        private DataView dvICD = new DataView();
        //�༭���
        private EditTypes editType;
        //��ѯ���
        private QueryTypes queryType;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cb30dis;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cancerFlag;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox infectFlag;
        private System.Windows.Forms.Button Search;
        //ICDҵ���
        private FS.HISFC.BizLogic.HealthRecord.ICD myICD = new FS.HISFC.BizLogic.HealthRecord.ICD();

        #endregion

        #region  ����
        /// <summary>
        /// ICD���
        /// </summary>
        [Category("ICD����"),Description("ICD���� ICD10 ICD9 ������")]
        public ICDTypes ICDType
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                //��������
                //LoadInfo();
            }
        }
        /// <summary>
        /// ��ѯ���
        /// </summary>
        public QueryTypes QueryType
        {
            get
            {
                return queryType;
            }
            set
            {
                queryType = value;
            }
        }
        #endregion

        #region �¼�

        /// <summary>
        /// uc��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucICDMaint_Load(object sender, System.EventArgs e)
        
        {
            if (this.Tag == null)
            {
                return;
            }
            if (this.Tag.ToString() == "ICD10")
            {
                type =  ICDTypes.ICD10;
            }
            else if (this.Tag.ToString()== "ICD9")
            {
                type =  ICDTypes.ICD9;
            }
            else if (this.Tag.ToString() == "ICDOperation")
            {
                type =  ICDTypes.ICDOperation;
            }
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            LoadInfo();
        }
        /// <summary>
        /// ���п�ȷ����仯ʱ,�洢�������ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            //			if(File.Exists(this.filePath))
            //			{
            //				FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.filePath);
            //			}
        }
        /// <summary>
        /// frmICDInfo�Զ����¼�,��������ȷ����ť���º󴥷�
        /// </summary>
        /// <param name="info"></param>
        private void icdInfo_SaveButtonClick(FS.HISFC.Models.HealthRecord.ICD info)
        {
            try
            {
                //�����¼�
                if (editType == EditTypes.Add)
                {
                    //�������
                    DataRow row = ds.Tables[0].NewRow();
                    //����һ��
                    SetRow(info, row);
                    ds.Tables[0].Rows.Add(row);
                }
                else
                {
                    object[] keys = new object[] { info.KeyCode };
                    DataRow row = ds.Tables[0].Rows.Find(keys);
                    if (row == null)
                    {
                        MessageBox.Show("������Ŀ����!");
                        return;
                    }
                    else
                    {
                        SetRow(info, row);
                    }
                }
                ds.Tables[0].AcceptChanges();
                LockFp();
                //				//����fpSpread1 ������
                //				if(System.IO.File.Exists(this.filePath))
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.filePath);
                //					
                //				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ˫���¼� ��Ӧ�޸İ�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.ModifyInfo();
        }
        /// <summary>
        /// ��ѯ���в˵���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mAll_Click(object sender, System.EventArgs e)
        {
            this.queryType = QueryTypes.All;
            LoadInfo();
        }
        /// <summary>
        /// ��ѯ��Ч�˵���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mValid_Click(object sender, System.EventArgs e)
        {
            this.queryType = QueryTypes.Valid;
            LoadInfo();
        }
        /// <summary>
        /// ��ѯͣ�ò˵���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mStop_Click(object sender, System.EventArgs e)
        {
            this.queryType = QueryTypes.Cancel;
            LoadInfo();
        }
        /// <summary>
        /// ��ѯ����ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            string temp = " like  '%" + this.textBox1.Text + "%' ";
            string rowFilter = "�����" + temp + " or " + "ҽ�����Ĵ���" + temp + " or " + "ƴ����" + temp + " or " + "ͳ�ƴ���" + temp + " or " + "�������" + temp;
            //			if(cb30dis.Checked)
            //			{
            //				//��ʮ�ּ���
            //			}
            //			else
            //			{
            //			}
            //			if(infectFlag.Checked)
            //			{
            //			}
            //			else
            //			{
            //			}
            //			if(cancerFlag.Checked)
            //			{
            //			}
            //			else
            //			{
            //			}
            this.dvICD.RowFilter = rowFilter;
            LockFp();
        }
        /// <summary>
        /// �����Ƿ�30�ּ���ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {

        }
        /// <summary>
        /// �ж��Ƿ�Ⱦ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
        {

        }
        /// <summary>
        /// �ж��Ƿ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, System.EventArgs e)
        {

        }
        #endregion

        #region  ����
        /// <summary>
        /// ����һ��
        /// </summary>
        private void InsertRow()
        {
            //ʵ����Ҫ�����Ĵ���
            frmICDInfo icdInfo = new frmICDInfo();
            //��ֵ ICD������ 
            icdInfo.ICDType = type;
            //��ֵ �޸ĵ�����
            icdInfo.EditType = EditTypes.Add;
            //�����޸�����
            editType = EditTypes.Add;
            //�����¼� ��
            icdInfo.SaveButtonClick+=new frmICDInfo.SaveInfo(icdInfo_SaveButtonClick);
            //��ʾ����
            icdInfo.ShowDialog();
        }
        /// <summary>
        /// �޸�ICD��Ϣ
        /// </summary>
        private void ModifyInfo()
        {
            if (this.fpSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }

            int currRow = fpSpread1_Sheet1.ActiveRowIndex;//��ǰ��
            if (currRow < 0)
            {
                return;
            }
            ArrayList alReturn = new ArrayList(); //���ص�ICD��Ϣ
            string sICDCode = "";//ѡȡ��ICD����

            //����������洢Ҫ�޸ĵ���Ϣ
            FS.HISFC.Models.HealthRecord.ICD orgICD = new FS.HISFC.Models.HealthRecord.ICD();
            ////�����Ч��
            //string IsValue = fpSpread1_Sheet1.Cells[currRow, GetColumnKey("��Ч��")].Value.ToString();
            ////����Ѿ�����Ч���������޸�
            //if (IsValue == "False")
            //{
            //    MessageBox.Show("����Ŀ�Ѿ���Ч,�����ٱ��޸�");
            //    return;
            //}
            //���ICD����

            sICDCode = fpSpread1_Sheet1.Cells[currRow, GetColumnKey("�����")].Text;

            if (sICDCode == "" || sICDCode == null)
            {
                return;
            }

            alReturn = myICD.IsExistAndReturn(sICDCode, type, true);

            if (alReturn == null)
            {
                MessageBox.Show("���ICD��Ϣ����!" + myICD.Err);
                return;
            }
            if (alReturn.Count == 0)
            {
                alReturn = myICD.IsExistAndReturn(sICDCode, type, false);
            }
            if (alReturn.Count == 0)
            {
                MessageBox.Show("���ICD��Ϣ����" );
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
            //ʵ����Ҫ�����Ĵ���
            frmICDInfo icdInfo = new frmICDInfo();
            //��ʾ���޸���Ϣ
            icdInfo.OrgICD = orgICD;
            //��ֵ ICD������ 
            icdInfo.ICDType = type;
            //��ֵ �޸ĵ�����
            icdInfo.EditType = EditTypes.Modify;
            //�����޸�����
            editType = EditTypes.Modify;
            //�����¼� ��
            icdInfo.SaveButtonClick+=new frmICDInfo.SaveInfo(icdInfo_SaveButtonClick);
            //��ʾ����
            icdInfo.ShowDialog();
        }
        /// <summary>
        /// ���غ��� ����Ӧ�������ϵĿ�ݼ� 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.A.GetHashCode())
            //{
            //    this.InsertRow();//����
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.M.GetHashCode())
            //{
            //    this.ModifyInfo();//�޸�
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();//�˳�
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.Q.GetHashCode())
            //{
            //    //��ѯ 
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.E.GetHashCode())
            //{
            //    Export();//����
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.E.GetHashCode())
            //{
            //    this.SetUp();//����
            //}
            //else
            //{
            //}
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// �������� 
        /// </summary>
        private void Export()
        {
            bool ret = false;
            //��������
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Excel|.xls";
                saveFileDialog1.FileName = "";

                saveFileDialog1.Title = "��������";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //��Excel ����ʽ��������
                    ret = fpSpread1.SaveExcel(saveFileDialog1.FileName);
                    if (ret)
                    {
                        MessageBox.Show("�����ɹ���");
                    }
                }
            }
            catch (Exception ex)
            {
                //������
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        ///����fpSpread1_Sheet1 ������
        /// </summary>
        private void SetUp()
        {
            //			Common.Controls.ucSetColumn uc = new Common.Controls.ucSetColumn();
            //			uc.UpButton = false;
            //			uc.FilePath = this.filePath;
            //			uc.GoDisplay += new Common.Controls.ucSetColumn.DisplayNow(uc_GoDisplay);
            //			FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }
        /// <summary>
        /// ��ѯ������λ��
        /// </summary>
        /// <returns></returns>
        private int GetColumnKey(string str)
        {
            foreach (FarPoint.Win.Spread.Column col in this.fpSpread1_Sheet1.Columns)
            {
                if (col.Label == str)
                {
                    return col.Index;
                }
            }
            return 0;
        }
        /// <summary>
        /// ����fpSpread1_Sheet1�Ŀ�ȵ� ����󴥷����¼�
        /// </summary>
        private void uc_GoDisplay()
        {
            LoadInfo(); //���¼�������

        }

        /// <summary>
        /// ��dtICD����Ӽ�һ��
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
            row["���"] = obj.WBCode;
            row["�������"] = obj.Name;
            row["�ڶ��������"] = obj.User01;
            row["�����������"] = obj.User02;
            row["����ԭ��"] = obj.DeadReason;
            row["��������"] = obj.DiseaseCode;
            row["��׼סԺ��"] = obj.StandardDays;
            if (obj.Is30Illness == "1" || obj.Is30Illness == "��")
            {
                row["�Ƿ�30�ּ���"] = "��";
            }
            else
            {
                row["�Ƿ�30�ּ���"] = "��";
            }
            if (obj.IsInfection == "1" || obj.IsInfection == "��")
            {
                row["�Ƿ�Ⱦ��"] = "��";
            }
            else
            {
                row["�Ƿ�Ⱦ��"] = "��";
            }
            if (obj.IsTumour == "1" || obj.IsTumour == "��")
            {
                row["�Ƿ�����"] = "��";
            }
            else
            {
                row["�Ƿ�����"] = "��";
            }
            row["סԺ�ȼ�"] = obj.InpGrade;
            if (obj.IsValid)
            {
                row["��Ч��"] = "��Ч";
            }
            else
            {
                row["��Ч��"] = "��Ч";
            }
            row["���"] = obj.SeqNo;
            row["oper_code"] = obj.OperInfo.ID;
            row["����Ա"] = obj.OperInfo.Name;
            row["����ʱ��"] = obj.OperInfo.OperTime;
            row["�������"] = obj.User01;
            //if (obj.SexType.ID == "A")
            //{
            //    row["�����Ա�"] = "ȫ��";
            //}
            //else if (obj.SexType.ID == "M")
            //{
                row["�����Ա�"] = obj.SexType.Name;
            //}
            //else
            //{
            //    row["�����Ա�"] = "Ů";
            //}
        }
        /// <summary>
        /// ��������
        /// </summary>
        private void LoadInfo()
        {
            try
            {
                //�����none ֱ�ӷ��� 
                if (ICDTypes.None == type)
                {
                    return;
                }
                //�ȴ�����
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                Application.DoEvents();
                ////
                //dtICD = new DataTable();
                ////��������ļ�����,ͨ�������ļ�����DataTable dtICD����Ϣ,����fp

                //Type strType = typeof(System.String);
                //Type intType = typeof(System.Int32);
                //Type dtType = typeof(System.DateTime);
                //Type boolType = typeof(System.Boolean);

                //dtICD.Columns.AddRange(new DataColumn[]{new DataColumn(   "�����", strType),
                //                                                           new DataColumn("�������", strType),
                //                                                           new DataColumn("�������", strType),
                //                                                           new DataColumn("ͳ�ƴ���", strType),
                //                                                           new DataColumn("ƴ����", strType),
                //                                                           new DataColumn("�����", strType),
                //                                                           new DataColumn("ҽ�����Ĵ���", strType),
                //                                                           new DataColumn("�ڶ��������", strType),
                //                                                           new DataColumn("�����������", strType),
                //                                                           new DataColumn("����ԭ��", strType),
                //                                                           new DataColumn("��������", strType),
                //                                                           new DataColumn("��׼סԺ��", intType),
                //                                                           new DataColumn("30�ּ���", boolType),
                //                                                           new DataColumn("��Ⱦ��", boolType),
                //                                                           new DataColumn("����", boolType),
                //                                                           new DataColumn("סԺ�ȼ�", strType),
                //                                                           new DataColumn("��Ч��", boolType),
                //                                                           new DataColumn("���", strType),
                //                                                           new DataColumn("����Ա����", strType),
                //                                                           new DataColumn("����Ա", strType),
                //                                                           new DataColumn("����ʱ��", dtType),
                //                                                            new DataColumn("�����Ա�", strType),
                //                                                           new DataColumn("sequence_no", strType)});

                ////��������Ϊsequence_no��
                ////CreateKeys(dtICD);
                //dvICD = new DataView(dtICD);
                //this.fpSpread1_Sheet1.DataSource = dvICD;
                //if (!File.Exists(this.filePath))
                //{
                //    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.filePath);
                //}
                //ArrayList alReturn = new ArrayList();//���ص�ICD��Ϣ;
                ////���ICD��Ϣ
                //alReturn = myICD.Query(type, queryType);
                //if (alReturn == null)
                //{
                //    MessageBox.Show("���ICD��Ϣ����!" + myICD.Err);
                //    return;
                //}
                ////ѭ��������Ϣ
                //foreach (FS.HISFC.Models.HealthRecord.ICD obj in alReturn)
                //{
                //    DataRow row = dtICD.NewRow();
                //    SetRow(obj, row);
                //    dtICD.Rows.Add(row);
                //}
                ////���� 
                //this.dvICD.Sort = "����� ASC";
                ////����fpSpread1 ������
                //if (System.IO.File.Exists(this.filePath))
                //{
                //    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.filePath);
                //    LockFp();
                //}
                myICD.Query(type, queryType, ref ds); //��ѯ 
                if (ds.Tables.Count == 1)
                {
                    DataColumn[] keys = new DataColumn[] { ds.Tables[0].Columns["sequence_no"] }; //z�������� 
                    ds.Tables[0].PrimaryKey = keys;
                    this.dvICD = new DataView(ds.Tables[0]);
                }

                this.fpSpread1_Sheet1.DataSource = dvICD;
                //this.fpSpread1_Sheet1.DataSource = ds.Tables[0];
                LockFp();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
       
        private void Search_Click(object sender, System.EventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            string sICDCode = ConvertString(fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, GetColumnKey("�����")].Text);
            if (sICDCode != null)
            {
                SetActiveRow(sICDCode);
            }
            LockFp();
        }

        /// <summary>
        /// �Ǽ��ַ�ת������ 
        /// </summary>
        /// <param name="ConvertStr"></param>
        /// <returns></returns>
        public string ConvertString(string ConvertStr)
        {
            string strReturn = "";
            try
            {
                strReturn = ConvertStr;
                int i = strReturn.IndexOf("*");
                int j = strReturn.IndexOf("+");

                if (i > 0 && strReturn.IndexOf("+") > 0)
                {
                    if (i < j)
                    {
                        string str1 = strReturn.Substring(0, i + 1);
                        string str2 = strReturn.Substring(i + 1);
                        strReturn = str2 + str1;
                    }
                    else
                    {
                        string str1 = strReturn.Substring(0, j + 1);
                        string str2 = strReturn.Substring(j + 1);
                        strReturn = str2 + str1;
                    }
                }
                return strReturn;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public int LockFp()
        {
            //			FarPoint.Win.Spread.CellType.CheckBoxCellType ck = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

            for (int i = 0; i < fpSpread1_Sheet1.Columns.Count; i++)
            {
                fpSpread1_Sheet1.Columns[i].Locked = true;
            }
            this.fpSpread1_Sheet1.Columns[0].Visible = false; //��� 
            this.fpSpread1_Sheet1.Columns[1].Width = 100;//�����
            this.fpSpread1_Sheet1.Columns[2].Visible = false;//ҽ�����Ĵ���
            this.fpSpread1_Sheet1.Columns[3].Width = 60;//ͳ�ƴ���
            this.fpSpread1_Sheet1.Columns[4].Width = 60;//ƴ����
            this.fpSpread1_Sheet1.Columns[5].Width = 60;//�����
            this.fpSpread1_Sheet1.Columns[6].Width = 250;//�������
            this.fpSpread1_Sheet1.Columns[7].Visible = false;//�������
            this.fpSpread1_Sheet1.Columns[8].Visible = false;//�������
            this.fpSpread1_Sheet1.Columns[9].Visible = false; //����ԭ��
            this.fpSpread1_Sheet1.Columns[10].Width = 50;//ͳ�Ʒ���
            this.fpSpread1_Sheet1.Columns[11].Visible = false;//ƽ��סԺ��
            //			this.fpSpread1_Sheet1.Columns[12].CellType = ck;
            this.fpSpread1_Sheet1.Columns[12].Width = 60;//�Ƿ�30�ּ���
            //			this.fpSpread1_Sheet1.Columns[13].CellType = ck;
            this.fpSpread1_Sheet1.Columns[13].Width = 60; //�Ƿ��Ⱦ��
            //			this.fpSpread1_Sheet1.Columns[14].CellType = ck;
            this.fpSpread1_Sheet1.Columns[14].Width = 60; //�Ƿ�����
            this.fpSpread1_Sheet1.Columns[15].Visible = false; //סԺ�ȼ�
            //			this.fpSpread1_Sheet1.Columns[16].CellType = ck;
            this.fpSpread1_Sheet1.Columns[16].Width = 60;//��Ч
            this.fpSpread1_Sheet1.Columns[17].Visible = false;//���
            this.fpSpread1_Sheet1.Columns[18].Visible = false;//����
            this.fpSpread1_Sheet1.Columns[19].Width = 60;//����Ա
            this.fpSpread1_Sheet1.Columns[20].Width = 80;//ʱ��
            this.fpSpread1_Sheet1.Columns[21].Visible = false;
            this.fpSpread1_Sheet1.Columns[22].Width = 60;//�����Ա�
            this.fpSpread1_Sheet1.Columns[22].Visible = false;
            return 1;
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="ICDCode"></param>
        /// <returns></returns>
        private int SetActiveRow(string ICDCode)
        {
            for (int i = 0; i < fpSpread1_Sheet1.Rows.Count; i++)
            {
                if (fpSpread1_Sheet1.Cells[i, GetColumnKey("�����")].Text == ICDCode)
                {
                    //�趨���
                    //					this.fpSpread1_Sheet1.SetActiveCell(i,1);
                    this.fpSpread1_Sheet1.ActiveRowIndex = i;
                    break;
                }
            }
            return 1;
        }
        /// <summary>
        /// ����ICD10��
        /// </summary>
        private void QueryIcdFromGD30()
        {
            if (MessageBox.Show("ͬ��ʡ��ICD��Ҫ������ʱ�䣬�Ƿ����ͬ����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface up = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
            List<FS.HISFC.Models.HealthRecord.ICD> listIcd = up.GetICDCodeByType(this.type,this.icdVersion);
            if (listIcd == null)
            {
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.myICD.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڸ���icd�룡���Ժ�");
            Application.DoEvents();
            if (this.myICD.DeleteTempICD() == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }

            foreach (FS.HISFC.Models.HealthRecord.ICD info in listIcd)
            {
                if (this.type == FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICDOperation)
                {
                    info.DiseaseCode = "";
                }
                if (this.myICD.InsertTempICD(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("������ʱ����ϳ���","��ʾ");
                    return;
                }
            }
            if (!this.cbDelAndInsert.Checked)
            {
                List<FS.HISFC.Models.HealthRecord.ICD> ListICD = myICD.QueryTempICD(this.type);
                if (ListICD == null)
                {
                    return;
                }
                foreach (FS.HISFC.Models.HealthRecord.ICD info in ListICD)
                {
                    info.IsValid = true;
                    if (this.myICD.Insert(info, this.type) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("����ICD��ϳ���", "��ʾ");
                        return;
                    }
                }
            }
            else
            {
                List<FS.HISFC.Models.HealthRecord.ICD> ListICD = myICD.QueryTempICDAll(this.type);
                if (ListICD == null)
                {
                    return;
                }
                if (this.myICD.DeleteICDDrgs(this.type) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("ɾ��ICD����", "��ʾ");
                    return;
                }

                foreach (FS.HISFC.Models.HealthRecord.ICD info in ListICD)
                {
                    info.IsValid = true;
                    if (this.myICD.Insert(info, this.type) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("����ICD��ϳ���", "��ʾ");
                        return;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ICD��ϳɹ���", "��ʾ");
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
        }

        private void cbDelAndInsert_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbDelAndInsert.Checked)
            {
                MessageBox.Show("��������ɾ������ICD�룬��ͬ������Ҫʱ��Ƚϳ�����ȷ���Ƿ������������");
            }
        }
    }
}
