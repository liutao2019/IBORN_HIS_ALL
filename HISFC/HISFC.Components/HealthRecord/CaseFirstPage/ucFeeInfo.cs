using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucFeeInfo<br></br>
    /// [��������: ����������Ϣ¼��]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucFeeInfo : UserControl
    {
        public ucFeeInfo()
        {
            InitializeComponent();
        }
        #region  ����
        private DataTable dtfeeInfo = new DataTable("���");
        public ArrayList feeInfoList = null;
        //������Ƿ������ı�־λ 
        private bool boolType = false;
        //���没����Ϣ
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        #endregion
        /// <summary>
        /// ������Ϣ
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }
        #region ������Ƿ���޸�
        /// <summary>
        /// �趨����Ƿ���޸� 
        /// </summary>
        public bool BoolType
        {
            get
            {
                return boolType;
            }
            set
            {
                boolType = value;
            }
        }
        #endregion

        #region  ����
        /// <summary>
        /// �޶���Ŀ�Ⱥܿɼ��� 
        /// </summary>
        private void SetFpEnter()
        {
            this.fpSpread1_Sheet1.Columns[0].Visible = false; //ͳ�Ʊ���
            this.fpSpread1_Sheet1.Columns[1].Width = 129;//��������
            this.fpSpread1_Sheet1.Columns[1].Locked = true;
            this.fpSpread1_Sheet1.Columns[2].Width = 80;//���ý��
            this.fpSpread1_Sheet1.Columns[2].Locked = !boolType;
        }
        /// <summary>
        /// ���ԭ�е�����
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtfeeInfo != null)
            {
                this.dtfeeInfo.Clear();
                SetFpEnter();
            }
            else
            {
                MessageBox.Show("���ñ�Ϊnull");
            }
            return 1;
        }
        /// <summary>
        /// У�����ݵĺϷ��ԡ�
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ValueState(ArrayList list)
        {
            if (list == null)
            {
                return -2;
            }
            foreach (FS.HISFC.Models.RADT.Patient obj in list)
            {
                if (obj.ID == null || obj.ID == "")
                {
                    MessageBox.Show("������Ϣ סԺ��ˮ�Ų���Ϊ��");
                    return -1;
                }
                if (obj.ID.Length > 14)
                {
                    MessageBox.Show("������Ϣ סԺ��ˮ�Ź���");
                    return -1;
                }
                if (obj.DIST == null || obj.DIST == "")
                {
                    MessageBox.Show("������Ϣ ͳ�ƴ��벻��Ϊ��");
                    return -1;
                }
                if (obj.DIST.Length > 3)
                {
                    MessageBox.Show("������Ϣ ͳ�ƴ������");
                    return -1;
                }
                if (obj.AreaCode == null || obj.AreaCode == "")
                {
                    MessageBox.Show("������Ϣ ͳ�����Ʋ���Ϊ��");
                    return -1;
                }
                if (obj.AreaCode.Length > 16)
                {
                    MessageBox.Show("������Ϣ ͳ�����ƹ���");
                    return -1;
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal(obj.IDCard) > (decimal)99999999.99)
                {
                    MessageBox.Show("������Ϣ ������");
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList GetFeeInfoList()
        {
            feeInfoList = null;
            if (dtfeeInfo != null)
            {
                foreach (DataRow dr in this.dtfeeInfo.Rows)
                {
                    dr.EndEdit();
                }

                dtfeeInfo.AcceptChanges();//
                feeInfoList = new ArrayList();
                FS.HISFC.Models.RADT.Patient info = null;
                foreach (DataRow row in dtfeeInfo.Rows)
                {
                    info = new FS.HISFC.Models.RADT.Patient();
                    info.ID = patientInfo.ID;
                    info.DIST = row["ͳ�Ʊ���"].ToString();//ͳ�ƴ������
                    if (info.DIST == "" || info.DIST == null)
                    {
                        continue;
                    }
                    info.AreaCode = row["��������"].ToString(); //ͳ������ 
                    if (row["���ý��"] != DBNull.Value)
                    {
                        info.IDCard = row["���ý��"].ToString();//ͳ�Ʒ��� 
                    }
                    feeInfoList.Add(info);
                }
            }
            return feeInfoList;
        }
        /// <summary>
        /// ��ѯ����ʾ����
        /// </summary>
        /// <returns>������ ��1 ���� 0 �������в���1  </returns>
        public int LoadInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (this.dtfeeInfo != null)
            {
                this.dtfeeInfo.Clear();
                this.dtfeeInfo.AcceptChanges();
            }
            if (patient == null)
            {
                return -1;
            }
            patientInfo = patient;
            if (patientInfo.CaseState == "0")
            {
                //�������в���
                return 1;
            }
            FS.HISFC.BizLogic.HealthRecord.Fee ba = new FS.HISFC.BizLogic.HealthRecord.Fee();
            //��ѯ��������������
            if (patientInfo.CaseState == "1")
            {
                feeInfoList = ba.QueryFeeInfoState(patientInfo.ID);
            }
            else
            {
                feeInfoList = ba.QueryFeeInfoState(patientInfo.ID);
            }
            if (feeInfoList == null)
            {
                return -1;
            }
            //ѭ����������
            foreach (FS.HISFC.Models.RADT.Patient info in feeInfoList)
            {
                DataRow row = dtfeeInfo.NewRow();
                SetRow(row, info);
                dtfeeInfo.Rows.Add(row);
            }
            decimal tempDec = 0;
            foreach (FS.HISFC.Models.RADT.Patient info in feeInfoList)
            {
                tempDec = tempDec + FS.FrameWork.Function.NConvert.ToDecimal(info.IDCard);
            }
            FS.HISFC.Models.RADT.Patient obj = new FS.HISFC.Models.RADT.Patient();
            obj.AreaCode = "�ϼƣ�";
            obj.IDCard = tempDec.ToString();
            DataRow rows = dtfeeInfo.NewRow();
            SetRow(rows, obj);
            dtfeeInfo.Rows.Add(rows);

            //���ı�־
            dtfeeInfo.AcceptChanges();
            SetFpEnter();
            return 0;
        }
        /// <summary>
        /// ��ѯ����ʾ����
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int LoadInfoDrgs(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //��������ʾ������
            patientInfo = patient;
            if (patientInfo.CaseState == "0")
            {
                //�������в���
                return 1;
            }
            FS.HISFC.BizLogic.HealthRecord.Fee ba = new FS.HISFC.BizLogic.HealthRecord.Fee();
            DataSet ds = new DataSet();
            ba.QueryFeeForDrgsByInpatientNO(patient.ID, ref ds);
            if (ds == null || ds.Tables.Count < 0 || ds.Tables[0].Rows.Count < 0)
            {
                return -1;
            }
            this.SetFarpointForDrgs(ds);
            return 0;
        }
        /// <summary>
        /// ��ʵ���е�ֵ��ֵ��row��
        /// </summary>
        /// <param name="row">�����row</param>
        /// <param name="info">�����ʵ��</param>
        private void SetRow(DataRow row, FS.HISFC.Models.RADT.Patient info)
        {
            row["ͳ�Ʊ���"] = info.DIST;//ͳ�ƴ������
            row["��������"] = info.AreaCode; //ͳ������ 
            row["���ý��"] = FS.FrameWork.Function.NConvert.ToDecimal(info.IDCard);//ͳ�Ʒ��� 
        }
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        /// <returns></returns>
        public int InitInfo()
        {
            this.InitDateTable();
            fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            SetFpEnter();
            return 0;
        }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        private void InitDateTable()
        {
            try
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type DecimalType = typeof(System.Decimal);

                dtfeeInfo.Columns.AddRange(new DataColumn[]{
															new DataColumn("ͳ�Ʊ���", strType),	//0
															new DataColumn("��������", strType),	 //1
															new DataColumn("���ý��", DecimalType)});//9
                //������Դ
                this.fpSpread1_Sheet1.DataSource = dtfeeInfo;
                //				//����fpSpread1 ������
                //				if(System.IO.File.Exists(filePath))
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
                //				else
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �뿪CELLʱ����,����  ����ϼƽ�� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            if (fpSpread1_Sheet1.ActiveColumnIndex == 2)
            {
                decimal tempDecimal = 0;
                for (int i = 0; i < fpSpread1_Sheet1.Rows.Count - 1; i++)
                {
                    //�ۼƽ��
                    tempDecimal += FS.FrameWork.Function.NConvert.ToDecimal(fpSpread1_Sheet1.Cells[i, 2].Text);
                }
                //���ĺϼƽ��
                fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.Rows.Count - 1, 2].Text = tempDecimal.ToString();
            }
        }

        /// <summary>
        /// ��ʾdrgs����
        /// </summary>
        /// <param name="ds"></param>
        private void SetFarpointForDrgs(DataSet ds)
        {
            this.fpSpread1_Sheet1.RowCount = 40;
            this.fpSpread1_Sheet1.Columns[0].Width = 0;
            this.fpSpread1_Sheet1.Columns[1].Width = 180;

            this.fpSpread1_Sheet1.Cells[0, 1].Text = "�ܷ���";
            this.fpSpread1_Sheet1.Cells[0, 2].Text = ds.Tables[0].Rows[0][0].ToString();
            this.fpSpread1_Sheet1.Cells[1, 1].Text = "�Ը����";
            this.fpSpread1_Sheet1.Cells[1, 2].Text = ds.Tables[0].Rows[0][1].ToString();

            this.fpSpread1_Sheet1.Cells[2, 1].Text = "1���ۺ�ҽ�Ʒ�����";
            this.fpSpread1_Sheet1.Cells[2, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[3, 1].Text = "һ��ҽ�Ʒ����";
            this.fpSpread1_Sheet1.Cells[3, 2].Text = ds.Tables[0].Rows[0][2].ToString();
            this.fpSpread1_Sheet1.Cells[4, 1].Text = "һ�����Ʋ�����";
            this.fpSpread1_Sheet1.Cells[4, 2].Text = ds.Tables[0].Rows[0][3].ToString();
            this.fpSpread1_Sheet1.Cells[5, 1].Text = "�����";
            this.fpSpread1_Sheet1.Cells[5, 2].Text = ds.Tables[0].Rows[0][4].ToString();
            this.fpSpread1_Sheet1.Cells[6, 1].Text = "��������";
            this.fpSpread1_Sheet1.Cells[6, 2].Text = ds.Tables[0].Rows[0][5].ToString();

            this.fpSpread1_Sheet1.Cells[7, 1].Text = "2�������";
            this.fpSpread1_Sheet1.Cells[7, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[8, 1].Text = "������Ϸ�";
            this.fpSpread1_Sheet1.Cells[8, 2].Text = ds.Tables[0].Rows[0][6].ToString();
            this.fpSpread1_Sheet1.Cells[9, 1].Text = "ʵ������Ϸ�";
            this.fpSpread1_Sheet1.Cells[9, 2].Text = ds.Tables[0].Rows[0][7].ToString();
            this.fpSpread1_Sheet1.Cells[10, 1].Text = "Ӱ����Ϸ�";
            this.fpSpread1_Sheet1.Cells[10, 2].Text = ds.Tables[0].Rows[0][8].ToString();
            this.fpSpread1_Sheet1.Cells[11, 1].Text = "�ٴ���Ϸ�";
            this.fpSpread1_Sheet1.Cells[11, 2].Text = ds.Tables[0].Rows[0][9].ToString();

            this.fpSpread1_Sheet1.Cells[12, 1].Text = "3��������";
            this.fpSpread1_Sheet1.Cells[12, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[13, 1].Text = "������������Ŀ��";
            this.fpSpread1_Sheet1.Cells[13, 2].Text = ds.Tables[0].Rows[0][10].ToString();
            this.fpSpread1_Sheet1.Cells[14, 1].Text = "�ٴ��������Ʒ�";
            this.fpSpread1_Sheet1.Cells[14, 2].Text = ds.Tables[0].Rows[0][11].ToString();
            this.fpSpread1_Sheet1.Cells[15, 1].Text = "�������Ʒ�";
            this.fpSpread1_Sheet1.Cells[15, 2].Text = ds.Tables[0].Rows[0][12].ToString();
            this.fpSpread1_Sheet1.Cells[16, 1].Text = "�����";
            this.fpSpread1_Sheet1.Cells[16, 2].Text = ds.Tables[0].Rows[0][13].ToString();
            this.fpSpread1_Sheet1.Cells[17, 1].Text = "������";
            this.fpSpread1_Sheet1.Cells[17, 2].Text = ds.Tables[0].Rows[0][14].ToString();

            this.fpSpread1_Sheet1.Cells[18, 1].Text = "4��������";
            this.fpSpread1_Sheet1.Cells[18, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[19, 1].Text = "������";
            this.fpSpread1_Sheet1.Cells[19, 2].Text = ds.Tables[0].Rows[0][15].ToString();

            this.fpSpread1_Sheet1.Cells[20, 1].Text = "5����ҽ�ࣨ��ҽ������ҽҽ�Ʒ���";
            this.fpSpread1_Sheet1.Cells[20, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[21, 1].Text = "��ҽ����";//��ҽ�����Ҫ�ٴ���
            this.fpSpread1_Sheet1.Cells[21, 2].Text = ds.Tables[0].Rows[0][16].ToString();

            this.fpSpread1_Sheet1.Cells[22, 1].Text = "6����ҩ��";
            this.fpSpread1_Sheet1.Cells[22, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[23, 1].Text = "��ҩ��";
            this.fpSpread1_Sheet1.Cells[23, 2].Text = ds.Tables[0].Rows[0][17].ToString();
            this.fpSpread1_Sheet1.Cells[24, 1].Text = "����ҩ�����";
            this.fpSpread1_Sheet1.Cells[24, 2].Text = ds.Tables[0].Rows[0][18].ToString();

            this.fpSpread1_Sheet1.Cells[25, 1].Text = "7����ҩ��";
            this.fpSpread1_Sheet1.Cells[25, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[26, 1].Text = "�г�ҩ��";
            this.fpSpread1_Sheet1.Cells[26, 2].Text = ds.Tables[0].Rows[0][19].ToString();
            this.fpSpread1_Sheet1.Cells[27, 1].Text = "�в�ҩ��";
            this.fpSpread1_Sheet1.Cells[27, 2].Text = ds.Tables[0].Rows[0][20].ToString();

            this.fpSpread1_Sheet1.Cells[28, 1].Text = "8��ѪҺ��ѪҺ��Ʒ��";
            this.fpSpread1_Sheet1.Cells[28, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[29, 1].Text = "Ѫ��";
            this.fpSpread1_Sheet1.Cells[29, 2].Text = ds.Tables[0].Rows[0][21].ToString();
            this.fpSpread1_Sheet1.Cells[30, 1].Text = "�׵�������Ʒ��";
            this.fpSpread1_Sheet1.Cells[30, 2].Text = ds.Tables[0].Rows[0][22].ToString();
            this.fpSpread1_Sheet1.Cells[31, 1].Text = "�򵰰�����Ʒ��";
            this.fpSpread1_Sheet1.Cells[31, 2].Text = ds.Tables[0].Rows[0][23].ToString();
            this.fpSpread1_Sheet1.Cells[32, 1].Text = "��Ѫ��������Ʒ��";
            this.fpSpread1_Sheet1.Cells[32, 2].Text = ds.Tables[0].Rows[0][24].ToString();
            this.fpSpread1_Sheet1.Cells[33, 1].Text = "ϸ����������Ʒ��";
            this.fpSpread1_Sheet1.Cells[33, 2].Text = ds.Tables[0].Rows[0][25].ToString();

            this.fpSpread1_Sheet1.Cells[34, 1].Text = "9���Ĳ���";
            this.fpSpread1_Sheet1.Cells[34, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[35, 1].Text = "�����һ����ҽ�ò��Ϸ�";
            this.fpSpread1_Sheet1.Cells[35, 2].Text = ds.Tables[0].Rows[0][26].ToString();
            this.fpSpread1_Sheet1.Cells[36, 1].Text = "������һ����ҽ�ò��Ϸ�";
            this.fpSpread1_Sheet1.Cells[36, 2].Text = ds.Tables[0].Rows[0][27].ToString();
            this.fpSpread1_Sheet1.Cells[37, 1].Text = "������һ����ҽ�ò��Ϸ�";
            this.fpSpread1_Sheet1.Cells[37, 2].Text = ds.Tables[0].Rows[0][28].ToString();

            this.fpSpread1_Sheet1.Cells[38, 1].Text = "10��������";
            this.fpSpread1_Sheet1.Cells[38, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[39, 1].Text = "������";
            this.fpSpread1_Sheet1.Cells[39, 2].Text = ds.Tables[0].Rows[0][29].ToString();
            this.fpSpread1_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(this.fpSpread1_Sheet1_CellChanged);
        }
        #endregion 
    }
}
