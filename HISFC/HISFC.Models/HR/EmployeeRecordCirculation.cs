using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>EmployeeRecordCirculation</br>
    /// <br>[��������: Ա��������תʵ��]</br>
    /// <br>[�� �� ��: �εº�]</br>
    /// <br>[����ʱ��: 2008-07-03]</br>
    ///     <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class EmployeeRecordCirculation : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// Ա����������
        /// </summary>
        private Neusoft.HISFC.Models.HR.EmployeeRecord emplRecord = new EmployeeRecord();

        /// <summary>
        /// ����ȥ��
        /// </summary>
        private string recordPlace=string.Empty;

        /// <summary>
        /// ���յ�λ
        /// </summary>
        private string inPlace = string.Empty;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime queryDate = new DateTime();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime borrowDate = new DateTime();


        /// <summary>
        /// ������
        /// </summary>
        private string borrowPerson = string.Empty;

        /// <summary>
        ///��ְʱ��
        /// </summary>
        private DateTime leaveDate = new DateTime();

        /// <summary>
        ///ת��ʱ��
        /// </summary>
        private DateTime outDate = new DateTime();
 
        #endregion

        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        public Neusoft.HISFC.Models.HR.EmployeeRecord EmplRecord
        {
            get 
            { 
                return emplRecord; 
            }
            set 
            { 
                emplRecord = value; 
            }
        }

        /// <summary>
        /// �浵ȥ��
        /// </summary>
        public string RecordPlace
        {
            get 
            { 
                return recordPlace; 
            }
            set 
            { 
                recordPlace = value; 
            }
        }

        /// <summary>
        /// ���յ�λ
        /// </summary>
        public string InPlace
        {
            get 
            { 
                return inPlace; 
            }
            set 
            { 
                inPlace = value; 
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime QueryDate
        {
            get 
            {
                return queryDate; 
            }
            set 
            { 
                queryDate = value; 
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime BorrowDate
        {
            get 
            { 
                return borrowDate; 
            }
            set 
            { 
                borrowDate = value; 
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string BorrowPerson
        {
            get 
            { 
                return borrowPerson; 
            }
            set 
            { 
                borrowPerson = value; 
            }
        }

        /// <summary>
        /// ��ְʱ��
        /// </summary>
        public DateTime LeaveDate
        {
            get 
            { 
                return leaveDate; 
            }
            set 
            { 
                leaveDate = value; 
            }
        }

        /// <summary>
        /// ת��ʱ��
        /// </summary>
        public DateTime OutDate
        {
            get 
            { 
                return outDate; 
            }
            set 
            { 
                outDate = value; 
            }
        }
        #endregion

        #region ��¡����
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new EmployeeRecordCirculation Clone()
        {
            EmployeeRecordCirculation employeeRecordCirculation = base.Clone() as EmployeeRecordCirculation;
            employeeRecordCirculation.EmplRecord = this.EmplRecord.Clone();
           
            return employeeRecordCirculation;
        }
        #endregion

    }
}
