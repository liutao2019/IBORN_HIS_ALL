using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// Neusoft.HISFC.Models.HR.WorkCheckDeduct<br></br>
    /// [��������: ���ڿۿ�ʵ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-07-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class WorkCheckDeduct : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�

        /// <summary>
        /// ��������
        /// </summary>
        private string workCheckType = "";

        /// <summary>
        /// ����ϸĿ
        /// </summary>
        private string payItem = "";

        /// <summary>
        /// ������׼
        /// </summary>
        private decimal foodSubsidy = 0;

        /// <summary>
        /// �ۿ����
        /// </summary>
        private decimal deductPercent = 0;

        /// <summary>
        /// �ۿ��������
        /// </summary>
        private decimal deductPercentLimit = 0;

        /// <summary>
        /// �ۿ���
        /// </summary>
        private decimal deductAmount = 0;

        /// <summary>
        /// �ۿ�������
        /// </summary>
        private decimal deductAmountLimit = 0;

        /// <summary>
        /// ȱ�ڴ���/��������
        /// </summary>
        private decimal checkNumLimit = 0;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();


        #endregion

        #region ����

        /// <summary>
        /// ȱ��ԭ��
        /// </summary>
        public string WorkCheckType
        {
            get
            {
                return workCheckType;
            }
            set
            {
                workCheckType = value;
            }
        }

        /// <summary>
        /// ����ϸĿ
        /// </summary>
        public string PayItem
        {
            get
            {
                return payItem;
            }
            set
            {
                payItem = value;
            }
        }

        /// <summary>
        /// ������׼
        /// </summary>
        public decimal FoodSubsidy
        {
            get
            {
                return foodSubsidy;
            }
            set
            {
                foodSubsidy = value;
            }
        }

        /// <summary>
        /// �ۿ����
        /// </summary>
        public decimal DeductPercent
        {
            get
            {
                return deductPercent;
            }
            set
            {
                deductPercent = value;
            }
        }

        /// <summary>
        /// �ۿ��������
        /// </summary>
        public decimal DeductPercentLimit
        {
            get
            {
                return deductPercentLimit;
            }
            set
            {
                deductPercentLimit = value;
            }
        }

        /// <summary>
        /// �ۿ���
        /// </summary>
        public decimal DeductAmount
        {
            get
            {
                return deductAmount;
            }
            set
            {
                deductAmount = value;
            }
        }

        /// <summary>
        /// �ۿ�������
        /// </summary>
        public decimal DeductAmountLimit
        {
            get
            {
                return deductAmountLimit;
            }
            set
            {
                deductAmountLimit = value;
            }
        }

        /// <summary>
        /// ȱ�ڴ���/��������
        /// </summary>
        public decimal CheckNumLimit
        {
            get
            {
                return checkNumLimit;
            }
            set
            {
                checkNumLimit = value;
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
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new WorkCheckDeduct Clone()
        {
            WorkCheckDeduct workCheckDeduct = base.Clone() as WorkCheckDeduct;

            workCheckDeduct.Oper = this.Oper.Clone();

            return workCheckDeduct;
        }
        #endregion
    }
}
