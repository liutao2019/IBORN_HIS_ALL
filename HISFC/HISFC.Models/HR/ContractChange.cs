using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>Neusoft.HISFC.Models.HR.ContractChange</br>
    /// <br>[��������: ��ͬ���ʵ����]</br>
    /// <br>[�� �� ��: ����]</br>
    /// <br>[����ʱ��: 2008-07-14]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class ContractChange : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�

        /// <summary>
        /// �������
        /// </summary>
        private string occurNumber = "";

        /// <summary>
        /// Ա��ʵ��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��ͬ���
        /// </summary>
        private string contractCode = "";

        /// <summary>
        /// ִ��ʱ��
        /// </summary>
        private DateTime executeTime;

        /// <summary>
        /// ǩ����ʽ
        /// </summary>
        private string signedType = "";

        /// <summary>
        /// ��ͬ״̬
        /// </summary>
        private string contractStatus = "";

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        public string OccurNumber
        {
            get
            {
                return occurNumber;
            }
            set
            {
                occurNumber = value;
            }
        }

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
        /// ִ��ʱ��
        /// </summary>
        public DateTime ExecuteTime
        {
            get
            {
                return executeTime;
            }
            set
            {
                executeTime = value;
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
        /// ��ͬ״̬
        /// </summary>
        public string ContractStatus
        {
            get
            {
                return contractStatus;
            }
            set
            {
                contractStatus = value;
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

            return contract;
        }

        #endregion
    }
}
