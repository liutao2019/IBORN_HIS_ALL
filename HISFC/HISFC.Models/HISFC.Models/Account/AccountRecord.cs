using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Account
{
    
    /// <summary>
    /// FS.HISFC.Models.Account.AccountRecord<br></br>
    /// [��������: �����ʻ�����ʵ��]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2007-05-04]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountRecord : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AccountRecord()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����
        
        /// <summary>
        /// �ʻ�������Ϣ
        /// </summary>
        private HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// �ʺ�
        /// </summary>
        private string accountNO = string.Empty;

        /// <summary>
        /// ��������0Ԥ����1���ʻ�2ͣ�ʻ�3�����ʻ�4֧��5�˷��뻧
        /// </summary>
        private EnumOperTypesService operType=new EnumOperTypesService();

        /// <summary>
        /// ���
        /// </summary>
        private decimal money;

        /// <summary>
        /// ���ѿ���
        /// </summary>
        private string deptCode=string.Empty;

        /// <summary>
        /// ����Ա
        /// </summary>
        private string oper = string.Empty;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime opertime;

        /// <summary>
        /// ��ע
        /// </summary>
        private string reMark = string.Empty;

        /// <summary>
        /// ����״̬
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ���׺����
        /// </summary>
        private decimal vacancy;

        /// <summary>
        /// ����Ȩ���߻�����Ϣ
        /// </summary>
        private HISFC.Models.RADT.Patient empwoerPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ��Ȩ���
        /// </summary>
        private decimal empowerCost = 0m;

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        FS.FrameWork.Models.NeuObject invoiceType = new FS.FrameWork.Models.NeuObject();
        #endregion

        #region ����

        /// <summary>
        /// �ʻ�������Ϣ
        /// </summary>
        public RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        /// <summary>
        /// �ʺ�
        /// </summary>
        public string AccountNO
        {
            get { return accountNO; }
            set { accountNO = value; }
        }

        /// <summary>
        /// ��������0Ԥ����1���ʻ�2ͣ�ʻ�3�����ʻ�4֧��5�˷��뻧
        /// </summary>
        public EnumOperTypesService OperType
        {
            get
            {
                return operType;
            }
            set
            {
                operType = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public decimal Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
            }
        }

        /// <summary>
        /// ���ѿ���
        /// </summary>
        public string DeptCode
        {
            get
            {
                return deptCode;
            }
            set
            {
                deptCode = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public string Oper
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
        /// ����ʱ��
        /// </summary>
        public DateTime OperTime
        {
            get
            {
                return opertime;
            }
            set
            {
                opertime = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string ReMark
        {
            get
            {
                return reMark;
            }
            set
            {
                reMark = value;
            }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        /// <summary>
        /// ���׺����
        /// </summary>
        public decimal Vacancy
        {
            get
            {
                return vacancy;
            }
            set
            {
                vacancy = value;
            }
        }

        /// <summary>
        /// ����Ȩ���߻�����Ϣ
        /// </summary>
        public HISFC.Models.RADT.Patient EmpowerPatient
        {
            get
            {
                return empwoerPatient;
            }
            set
            {
                empwoerPatient = value;
            }

        }

        /// <summary>
        /// ��Ȩ���
        /// </summary>
        public decimal EmpowerCost
        {
            get
            {
                return empowerCost;
            }
            set
            {
                empowerCost = value;
            }
        }

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject InvoiceType
        {
            get
            {
                return invoiceType;
            }
            set
            {
                invoiceType = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new AccountRecord Clone()
        {
            AccountRecord accountCard = base.Clone() as AccountRecord;
            accountCard.patient = this.Patient.Clone();
            accountCard.empwoerPatient = this.EmpowerPatient.Clone();
            accountCard.operType = this.OperType.Clone() as EnumOperTypesService;
            accountCard.invoiceType = this.InvoiceType.Clone();
            return accountCard;
        }

        #endregion

    }
}
