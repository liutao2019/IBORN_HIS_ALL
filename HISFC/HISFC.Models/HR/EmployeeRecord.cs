using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.FrameWork.Models;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>EmployeeRecord</br>
    /// [��������: ���µ�������ʵ��]<br></br>
    /// [�� �� ��: �εº�]<br></br>
    /// [����ʱ��: 2008-07-03]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class EmployeeRecord : Neusoft.FrameWork.Models.NeuObject
    {

        #region �ֶ�

        /// <summary>
        /// Ա��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ����״̬
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject recordStatus=new NeuObject();

        /// <summary>
        /// �浵λ��
        /// </summary>
        private string recordPlace=string.Empty;

        /// <summary>
        /// �Ա�
        /// </summary>
        private SexEnumService sex = new SexEnumService();

        /// <summary>
        /// ����ȥ��
        /// </summary>
        private string record_In;

        /// <summary>
        /// ˳���
        /// </summary>
        private int sortID;

        /// <summary>
        /// ����Ա
        /// </summary>
        Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region ����
        /// <summary>
        /// Ա��
        /// </summary>
        public Neusoft.HISFC.Models.Base.Employee Employee
        {
            get 
            { 
                return employee; 
            }
            set 
            { 
                employee = value; 
            }
        }

        /// <summary>
        /// �浵λ��
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
        /// �Ա�
        /// </summary>
        public SexEnumService Sex
        {
            get
            {
                return this.sex;
            }
            set
            {
                this.sex = value;
            }
        }

        /// <summary>
        /// �浵ȥ��
        /// </summary>
        public string Record_In
        {
            get
            {
                return record_In;
            }
            set
            {
                record_In = value;
            }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject RecordStatus
        {
            get 
            { 
                return recordStatus; 
               
            }
            set 
            { 
                recordStatus = value; 
            }
        }
          
        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment Oper
        {
            get 
            { 
                return oper; 
            }
            set 
            { 
                oper = value; 
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int SortID
        {
            get
            {
                return this.sortID;
            }
            set
            {
                sortID = value;
            }
        }
        #endregion

        #region ��¡����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new EmployeeRecord Clone()
        {
            EmployeeRecord employeeRecord = base.Clone() as EmployeeRecord;
            employeeRecord.Employee = this.Employee.Clone();
            employeeRecord.RecordStatus = this.RecordStatus.Clone();
            employeeRecord.Oper = this.Oper.Clone();

            return employeeRecord;

        }
        #endregion
    }
}
