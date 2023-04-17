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

        FS.HISFC.Models.HealthRecord.Base baseObj = new FS.HISFC.Models.HealthRecord.Base();

        public delegate void ListShowdelegate(FS.HISFC.Models.HealthRecord.Base obj);
        public event ListShowdelegate SelectItem;

        #region ����

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

        /// <summary>
        /// ��ѯ������������Ϣ
        /// zhangjunyi@FS.com 2005.6.29
        /// </summary>
        private void SearchInfo()
        {
            try
            {
                //����������Ϣ������
                FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
                string strWhere = "";
                if (this.neuTabControl2.SelectedIndex == 0)  //������Ϣ
                {
                    strWhere = this.ucCustomQuery1.GetWhereString();
                    if (strWhere == "")
                    {
                        MessageBox.Show("�������ѯ������");
                        return;
                    }
                        strWhere = " where " + strWhere;
                }
                else if (this.neuTabControl2.SelectedIndex == 1)  //�����Ϣ
                {
                    strWhere = this.ucCustomQuery2.GetWhereString();
                    if (strWhere == "")
                    {
                        MessageBox.Show("�������ѯ������");
                        return;
                    }
                    //strWhere = "WHERE Inpatient_No IN  (select Inpatient_No from met_cas_diagnose WHERE " + strWhere + " )";
                    strWhere = "WHERE Inpatient_No IN  (select Inpatient_No from met_cas_diagnose WHERE " + strWhere + " )";//֮ǰ���˸�������
                }
                else   //������Ϣ
                {
                    strWhere = this.ucCustomQuery3.GetWhereString();
                    if (strWhere == "")
                    {
                        MessageBox.Show("�������ѯ������");
                        return;
                    }
                    strWhere = "WHERE Inpatient_No IN  (select Inpatient_No from met_cas_operationdetail WHERE " + strWhere + " )";
                }


                //�ȴ�����
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                Application.DoEvents();
                ArrayList list = baseDml.QueryCaseBaseInfoByOwnConditions(strWhere);

                if (list == null)
                {
                    MessageBox.Show("��ѯ����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                this.fpSpread1_Sheet1.RowCount = 0;
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
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.times].Text = obj.PatientInfo.InTimes.ToString();//��Ժ����
                    this.fpSpread1_Sheet1.Cells[row, (int)Cols.Memo].Text = obj.FourDiseasesReport;//������� 2012-1-10 chengym �Ĳ����治֪������ӵģ������������������Ϣ
                } 

                //��������
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            if (keyData.GetHashCode() == Keys.X.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //�ر�
                this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ��ȡ��ǰѡ�����
        /// </summary>
        /// <returns></returns>
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
            if (_Row < this.fpSpread1_Sheet1.RowCount - 1)
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
        #endregion

        private void frmSearchPatient_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
                    this.ucCustomQuery1.btnReset_Click(sender, e);
                    //����
                    break;
                case 2: //guan�ر�
                    this.Close();
                    break;
            }
        }

        private void fpSpread1_DoubleClick(object sender, EventArgs e)
        {
            baseObj = GetCaseInfo();
            SelectItem(baseObj);
            this.Close();
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            baseObj = GetCaseInfo();
            SelectItem(baseObj);
            this.Close();
        }

    }
}