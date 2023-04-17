using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ���ò�ѯ�����б�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucQueryFeePatient : UserControl
    {
        public ucQueryFeePatient()
        {
            InitializeComponent();
        }

#region �ֶ�
        private List<PatientInfo> patients = new List<PatientInfo>();
#endregion

#region ����
        public PatientInfo this[int index]
        {
            get
            {
                if (index >= this.patients.Count)
                {
                    throw new ApplicationException("index�����������ֵ");
                }

                return this.patients[index];
            }
        }
#endregion

#region ����
        /// <summary>
        /// ��ӻ���
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        public void AddPatient(PatientInfo patientInfo)
        {
            fpSpread1_Sheet1.RowCount += 1;
            int row = fpSpread1_Sheet1.RowCount - 1;
            //סԺ��
            fpSpread1_Sheet1.SetValue(row, 0, patientInfo.PID.PatientNO, false);
            //����
            fpSpread1_Sheet1.SetValue(row, 1, patientInfo.Name, false);
            //�Ա�
            fpSpread1_Sheet1.SetValue(row, 2, patientInfo.Sex.Name, false);
            //����            
            fpSpread1_Sheet1.SetValue(row, 3, patientInfo.Age, false);
            //סԺ����
            fpSpread1_Sheet1.SetValue(row, 4, patientInfo.PVisit.PatientLocation.Dept.Name, false);
            //�������
            fpSpread1_Sheet1.SetValue(row, 5, patientInfo.Pact.PayKind.Name, false);
            //��Ժʱ��
            fpSpread1_Sheet1.SetValue(row, 6, patientInfo.PVisit.InTime, false);
            //״̬
            fpSpread1_Sheet1.SetValue(row, 7, patientInfo.PVisit.InState.Name, false);
            //����
            fpSpread1_Sheet1.SetValue(row, 8, patientInfo.FT.TotCost, false);
            //���
            fpSpread1_Sheet1.SetValue(row, 9, patientInfo.FT.LeftCost, false);
            #region �ж���û���ڱ����ҷ���������
            //if (this.PatientList != null) //�ڱ����ҷ��������� 
            //{
            //    bool boolTemp = false;
            //    foreach (FS.HISFC.Object.RADT.PatientInfo info in this.PatientList)
            //    {
            //        if (info.ID == patientInfo.ID)
            //        {
            //            fpSpread1_Sheet1.SetValue(row, 10, "�Ѿ��շ�");
            //            boolTemp = true;
            //            break;
            //        }
            //    }
            //    if (!boolTemp)
            //    {
            //        fpSpread1_Sheet1.SetValue(row, 10, "û���շ�");
            //    }
            //}
            //else
            //{
            //    fpSpread1_Sheet1.SetValue(row, 10, "û���շ�");
            //}
            #endregion
            // 
            fpSpread1_Sheet1.Rows[row].Tag = patientInfo;
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Reset()
        {
            this.patients.Clear();
            this.fpSpread1_Sheet1.RowCount = 0;
        }
#endregion
    }
}
