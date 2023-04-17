using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace UFC.HealthRecord
{
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
        private Neusoft.HISFC.Object.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Object.RADT.PatientInfo();
        #endregion

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
            foreach (Neusoft.HISFC.Object.RADT.Patient obj in list)
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
                if (Neusoft.NFC.Function.NConvert.ToDecimal(obj.IDCard) > (decimal)99999999.99)
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
                dtfeeInfo.AcceptChanges();//
                feeInfoList = new ArrayList();
                Neusoft.HISFC.Object.RADT.Patient info = null;
                foreach (DataRow row in dtfeeInfo.Rows)
                {
                    info = new Neusoft.HISFC.Object.RADT.Patient();
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
        public int LoadInfo(Neusoft.HISFC.Object.RADT.PatientInfo patient)
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
            Neusoft.HISFC.Management.HealthRecord.Base ba = new Neusoft.HISFC.Management.HealthRecord.Base();
            Neusoft.HISFC.Management.HealthRecord.Fee feeCaseMgr = new Neusoft.HISFC.Management.HealthRecord.Fee();
            //��ѯ��������������
            if (patientInfo.CaseState == "1")
            {
                feeInfoList = feeCaseMgr.QueryFeeInfoState(patientInfo.ID);
            }
            else
            {
                feeInfoList = feeCaseMgr.QueryFeeInfoState(patientInfo.ID);
            }
            if (feeInfoList == null)
            {
                return -1;
            }
            //ѭ����������
            foreach (Neusoft.HISFC.Object.RADT.Patient info in feeInfoList)
            {
                DataRow row = dtfeeInfo.NewRow();
                SetRow(row, info);
                dtfeeInfo.Rows.Add(row);
            }
            decimal tempDec = 0;
            foreach (Neusoft.HISFC.Object.RADT.Patient info in feeInfoList)
            {
                tempDec = tempDec + Neusoft.NFC.Function.NConvert.ToDecimal(info.IDCard);
            }
            Neusoft.HISFC.Object.RADT.Patient obj = new Neusoft.HISFC.Object.RADT.Patient();
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
        /// ��ʵ���е�ֵ��ֵ��row��
        /// </summary>
        /// <param name="row">�����row</param>
        /// <param name="info">�����ʵ��</param>
        private void SetRow(DataRow row, Neusoft.HISFC.Object.RADT.Patient info)
        {
            row["ͳ�Ʊ���"] = info.DIST;//ͳ�ƴ������
            row["��������"] = info.AreaCode; //ͳ������ 
            row["���ý��"] = Neusoft.NFC.Function.NConvert.ToDecimal(info.IDCard);//ͳ�Ʒ��� 
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
                //					Neusoft.NFC.Interface.Classes.CustomerFp.ReadColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
                //				else
                //				{
                //					Neusoft.NFC.Interface.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1,filePath);
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
                    tempDecimal += Neusoft.NFC.Function.NConvert.ToDecimal(fpSpread1_Sheet1.Cells[i, 2].Text);
                }
                //���ĺϼƽ��
                fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.Rows.Count - 1, 2].Text = tempDecimal.ToString();
            }
        }
        #endregion 
    }
}
