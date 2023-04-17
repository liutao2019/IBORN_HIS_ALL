using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// Neusoft.HISFC.Models.HR.Constract<br></br>
    /// [��������: ��ͬʵ���ࡣ]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-07-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class Contract : Neusoft.FrameWork.Models.NeuObject 
    {
        #region �ֶ�

        /// <summary>
        /// �������
        /// </summary>
        //private string occurNumber = "";

        /// <summary>
        /// Ա��ʵ��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��ͬ���
        /// </summary>
        private string contractCode = "";

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// ǩ��ʱ��
        /// </summary>
        private DateTime signedTime;

        /// <summary>
        /// ���ʱ��
        /// </summary>
        private DateTime releaseTime;

        /// <summary>
        /// �����ͬԭ��
        /// </summary>
        private string releaseReason;

        /// <summary>
        /// ǩ����ʽ
        /// </summary>
        private string signedType;

        /// <summary>
        /// ��Ա���
        /// </summary>
        private string personType;
        
        /// <summary>
        /// ��Ա�������
        /// </summary>
        private string  personTypeName;

        /// <summary>
        /// ΥԼ��
        /// </summary>
        private decimal renegeMoney;

        /// <summary>
        /// Ƹ��
        /// </summary>
        private decimal retainTerm;

        /// <summary>
        /// Ƹ�÷�ʽ
        /// </summary>
        private string retainType;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��ͬ����
        /// </summary>
        private byte[] contractAttach = null;

        #endregion

        #region �ֶ�

        /// <summary>
        /// �������
        /// </summary>
        //public string OccurNumber
        //{
        //    get
        //    {
        //        return occurNumber;
        //    }
        //    set
        //    {
        //        occurNumber = value;
        //    }
        //}

        /// <summary>
        /// Ա��ʵ��
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
        /// ��ͬ���
        /// </summary>
        public string ContractCode
        {
            get
            {
                return contractCode;
            }
            set
            {
                contractCode = value;
            }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// ǩ��ʱ��
        /// </summary>
        public DateTime SignedTime
        {
            get
            {
                return signedTime;
            }
            set
            {
                signedTime = value;
            }
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime ReleaseTime
        {
            get
            {
                return releaseTime;
            }
            set
            {
                releaseTime = value;
            }
        }

        /// <summary>
        /// �����ͬԭ��
        /// </summary>
        public string ReleaseReason
        {
            get
            {
                return releaseReason;
            }
            set
            {
                releaseReason = value;
            }
        }

        /// <summary>
        /// ǩ����ʽ
        /// </summary>
        public string SignedType
        {
            get
            {
                return signedType;
            }
            set
            {
                signedType = value;
            }
        }

        /// <summary>
        /// ��Ա���
        /// </summary>
        public string PersonType
        {
            get
            {
                return personType;
            }
            set
            {
                personType = value;
            }
        }

        /// <summary>
        /// ��Ա�������
        /// </summary>
        public string PersonTypeName
        {
            get
            {
                return personTypeName;
            }
            set
            {
                personTypeName = value;
            }
        }

        /// <summary>
        /// ΥԼ��
        /// </summary>
        public decimal RenegeMoney
        {
            get
            {
                return renegeMoney;
            }
            set
            {
                renegeMoney = value;
            }
        }

        /// <summary>
        /// Ƹ��
        /// </summary>
        public decimal RetainTerm
        {
            get
            {
                return retainTerm;
            }
            set
            {
                retainTerm = value;
            }
        }

        /// <summary>
        /// Ƹ�÷�ʽ
        /// </summary>
        public string RetainType
        {
            get
            {
                return retainType;
            }
            set
            {
                retainType = value;
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
        /// ��ͬ����
        /// </summary>
        public byte[] ContractAttach
        {
            get
            {
                return contractAttach;
            }
            set
            {
                contractAttach = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new Contract Clone()
        {
            Contract contract = base.Clone() as Contract;

            contract.Employee = this.Employee.Clone();
            contract.Oper = this.Oper.Clone();
            contract.ContractAttach = this.ContractAttach.Clone() as byte[];

            return contract;
        }
        #endregion
    }
}
