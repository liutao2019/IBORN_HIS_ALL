using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.Search
{
    public partial class ucPatientList : UserControl
    {
        public delegate void ListShowdelegate(FS.HISFC.Models.HealthRecord.Base obj);

        public event ListShowdelegate SelectItem;

        public ucPatientList()
        {
            InitializeComponent();
        }

        #region ȫ�ֱ���
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string strErr = "";
        FS.HISFC.BizLogic.HealthRecord.Base baseMgr = new FS.HISFC.BizLogic.HealthRecord.Base();
        FS.HISFC.Models.HealthRecord.Base baseObj = new FS.HISFC.Models.HealthRecord.Base();
        FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        #endregion 

        public FS.HISFC.Models.HealthRecord.Base CaseBase
        {
            get
            {
                return baseObj;
            }
        }

        #region ö��
        private enum Cols
        {
            outDept, //��Ժ����
            outTime,//��Ժ����
            strName,//����
            sexName,//�Ա�
            inpatientNO,//סԺ��ˮ��
            caseNo,//������
            patientNO,//סԺ��
            times,//�ڼ���
            Memo

        }
        #endregion 

        #region ����סԺ�Ų�ѯ ,���ز�ѯ�����������
        /// <summary>
        /// ����סԺ�Ų�ѯ
        /// </summary>
        /// <param name="PatientNO">����</param>
        /// <param name="CardNOType">1,������,2 סԺ�� 3 ����</param>
        /// <returns></returns>
        public ArrayList Init(string PatientNO ,string CardNOType)
        {
            try
            {
                this.fpSpread1_Sheet1.RowCount = 0;
                ArrayList list = null;
                if (CardNOType == "1")
                { 
                    list = baseMgr.QueryCaseBaseInfoByCaseNO(PatientNO);//����סԺ�Ų�ѯ
                }
                else if (CardNOType == "2") //����סԺ�Ų�ѯ
                { 
                    list = baseMgr.QueryPatientInfo(PatientNO);
                }
                else if (CardNOType == "3") //����������ѯ
                {
                    list = baseMgr.QueryPatientInfoByName(PatientNO);
                }
                if (list == null)
                {
                    this.strErr = baseMgr.Err;
                    return null;
                }
                foreach (FS.HISFC.Models.HealthRecord.Base obj in list)
                {
                    int row = this.fpSpread1_Sheet1.Rows.Count;
                    this.fpSpread1_Sheet1.Rows.Add(row, 1);
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.outDept].Text = obj.OutDept.Name;//��Ժ����
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.outTime].Text = obj.PatientInfo.PVisit.OutTime.ToString();//��Ժʱ��
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.strName].Text = obj.PatientInfo.Name;//����
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.sexName].Text = obj.PatientInfo.Sex.Name;//�Ա�
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.inpatientNO].Text = obj.PatientInfo.ID;//סԺ��ˮ��
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.caseNo].Text = obj.CaseNO; //������
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.caseNo].Tag = obj; //������
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.patientNO].Text = obj.PatientInfo.PID.PatientNO;//סԺ��
                    this.fpSpread1_Sheet1.Cells[row,(int)Cols.times].Text = obj.PatientInfo.InTimes.ToString();//��Ժ����
                    if (obj.PatientInfo.User01 == "������Ϣ" || CardNOType =="1" )
                    {
                        this.fpSpread1_Sheet1.Rows[row].BackColor = System.Drawing.Color.Green;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion 

        #region ˫��ʱѡȡ��Ŀ
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            baseObj = GetCaseInfo();
            this.Visible = false;
            SelectItem(baseObj);
        }
        #endregion 

        #region ��ȡ��ǰѡ�����
        public FS.HISFC.Models.HealthRecord.Base GetCaseInfo()
        {
            int Row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (Row == -1)
            {
                return null;
            }
            baseObj = (FS.HISFC.Models.HealthRecord.Base)this.fpSpread1_Sheet1.Cells[Row, (int)Cols.caseNo].Tag;
            return baseObj;
        }
        #endregion 
        #region  ��������
        /// <summary>
        /// ��һ��
        /// </summary>
        public void NextRow()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            int _Row = fpSpread1_Sheet1.ActiveRowIndex;
            if (_Row < this.fpSpread1_Sheet1.RowCount-1)
            {
                _Row = _Row + 1;
                fpSpread1_Sheet1.ActiveRowIndex = _Row;
                fpSpread1_Sheet1.AddSelection(_Row, 0, 1, 0);
            }
        }
        /// <summary>
        /// ǰһ��
        /// </summary>
        public void PriorRow()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            int _Row = fpSpread1_Sheet1.ActiveRowIndex;
            if (_Row > 0)
            {
                _Row = _Row - 1;
                fpSpread1_Sheet1.ActiveRowIndex = _Row;
                fpSpread1_Sheet1.AddSelection(_Row, 0, 1, 0);
            }
        }
        #endregion 

        private void ucPatientList_Load(object sender, EventArgs e)
        {
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

    }
}
