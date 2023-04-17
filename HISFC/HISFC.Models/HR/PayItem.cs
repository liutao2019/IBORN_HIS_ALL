using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br> PayList</br>
    /// <br>[��������: ����ϸ��ʵ��]</br>
    /// <br> [�� �� ��: �εº�]</br>
    /// <br>[����ʱ��: 2008-07-18]</br>
    ///     <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class PayItem : Spell, Base.ISort, Base.IValid
    {
 
        #region �ֶ�
        /// <summary>
        /// �Ƿ��Ǹ�����Ŀ
        /// </summary>
        private bool isComplexItem = false;

        /// <summary>
        /// �Ƿ���������Ŀ
        /// </summary>
        private bool isInputItem = false;

        /// <summary>
        /// �Ƿ��ǹ��ʻ���
        /// </summary>
        private bool isPayBase = false;

        /// <summary>
        /// ���Ϲ�ʽ
        /// </summary>
        private string complexFormula = string.Empty;

        /// <summary>
        /// �Ƿ��Ǳ�������
        /// </summary>
        private bool isInsuranceType = false;

        /// <summary>
        /// ��������
        /// </summary>
        private string computeType = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        private int sortID=0;

        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isValid=true;

        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment oper = new OperEnvironment();
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ��Ǹ�����Ŀ
        /// </summary>
        public bool IsComplexItem
        {
            get 
            { 
                return isComplexItem; 
            }
            set 
            { 
                isComplexItem = value; 
            }
        }

        /// <summary>
        /// �Ƿ���������Ŀ
        /// </summary>
        public bool IsInputItem
        {
            get 
            { 
                return isInputItem; 
            }
            set 
            { 
                isInputItem = value; 
            }
        }

        /// <summary>
        /// �Ƿ��ǹ��ʻ���
        /// </summary>
        public bool IsPayBase
        {
            get 
            { 
                return isPayBase; 
            }
            set 
            { 
                isPayBase = value; 
            }
        }

        /// <summary>
        /// �����ʽ
        /// </summary>
        public string ComplexFormula
        {
            get 
            { 
                return complexFormula; 
            }
            set 
            { 
                complexFormula = value; 
            }
        }

        /// <summary>
        /// �Ƿ��Ǳ�������
        /// </summary>
        public bool IsInsuranceType
        {
            get 
            { 
                return isInsuranceType; 
            }
            set 
            { 
                isInsuranceType = value; 
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string ComputeType
        {
            get 
            { 
                return computeType; 
            }
            set 
            { 
                computeType = value; 
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

        /// <summary>
        /// �Ƿ���Ч
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
        /// ��������
        /// </summary>
        public OperEnvironment Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }
        #endregion

        #region ��¡
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>Const��ʵ��</returns>
        public new PayItem Clone()
        {
            PayItem payItem = base.Clone() as PayItem;
            payItem.Oper = this.Oper.Clone();

            return payItem;

        }
        #endregion
    }
}
