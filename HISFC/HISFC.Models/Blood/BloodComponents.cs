using System;

namespace Neusoft.HISFC.Models.Blood
{
    /// <summary>
    /// [��������: ѪҺ�ɷ�ʵ��]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: 2007-4-17]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='����'
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
    [System.Serializable]
	public class BloodComponents : Neusoft.HISFC.Models.Base.Spell {

		public BloodComponents()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region

        /// <summary>
        /// ���
        /// </summary>
        private int order;

        /// <summary>
        /// �Ƿ���Ҫ��Ѫ 
        /// </summary>
        private bool isNeedMixed;

        /// <summary>
        /// ��������
        /// </summary>
        private int keepingDays;

        /// <summary>
        /// �����¶�
        /// </summary>
        private decimal keepingTemperature;

        /// <summary>
        /// ��λ
        /// </summary>
        private string baseUnit;

        /// <summary>
        /// ��С�Ʒ�����
        /// </summary>
        private decimal minAmount;

        /// <summary>
        /// �����
        /// </summary>
        private decimal tradePrice;

        /// <summary>
        /// ���ۼ�
        /// </summary>
        private decimal salePrice;

        /// <summary>
        /// ���뵥��Ч���� 0 Ϊһֱ��Ч
        /// </summary>
        private int applyValidDays;

        /// <summary>
        /// �Ƿ���Ч true��Ч false��Ч
        /// </summary>
        private bool isValid;

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ���
        /// </summary>
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }


        /// <summary>
        /// �Ƿ���Ҫ��Ѫ true ��Ҫ fase ����Ҫ
        /// </summary>
        public bool IsNeedMixed
        {
            get
            {
                return isNeedMixed;
            }
            set
            {
                isNeedMixed = value;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public int KeepingDays
        {
            get
            {
                return keepingDays;
            }
            set
            {
                keepingDays = value;
            }
        }


        /// <summary>
        /// �����¶�
        /// </summary>
        public decimal KeepingTemperature
        {
            get
            {
                return keepingTemperature;
            }
            set
            {
                keepingTemperature = value;
            }
        }


        /// <summary>
        /// ��λ
        /// </summary>
        public string Unit
        {
            get
            {
                return this.baseUnit;
            }
            set
            {
                this.baseUnit = value;
            }
        }


        /// <summary>
        /// ��С�Ʒ�����
        /// </summary>
        public decimal MinAmount
        {
            get
            {
                return minAmount;
            }
            set
            {
                minAmount = value;
            }
        }


        /// <summary>
        /// �����
        /// </summary>
        public decimal TradePrice
        {
            get
            {
                return tradePrice;
            }
            set
            {
                tradePrice = value;
            }
        }


        /// <summary>
        /// ���ۼ�
        /// </summary>
        public decimal SalePrice
        {
            get
            {
                return salePrice;
            }
            set
            {
                salePrice = value;
            }
        }


        /// <summary>
        /// ���뵥��Ч���� 0 Ϊһֱ��Ч
        /// </summary>
        public int ApplyValidDays
        {
            get
            {
                return applyValidDays;
            }
            set
            {
                applyValidDays = value;
            }
        }


        /// <summary>
        /// �Ƿ���Ч true��Ч false��Ч
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
        /// ����Ա��Ϣ ID ��� Name ����
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment OperInfo
        {
            get
            {
                return this.operInfo;
            }
            set
            {
                this.operInfo = value;
            }
        }


        #endregion

        //#region ISpellCode ��Ա

        ///// <summary>
        ///// ƴ����
        ///// </summary>
        //private string spellCode;

        ///// <summary>
        ///// �����
        ///// </summary>
        //private string wbCode;

        ///// <summary>
        ///// �Զ�����
        ///// </summary>
        //private string userCode;

        ///// <summary>
        ///// ƴ����
        ///// </summary>
        //public string Spell_Code
        //{
        //    get
        //    {
        //        return spellCode;
        //    }
        //    set
        //    {
        //        spellCode = value;
        //    }
        //}


        ///// <summary>
        ///// �����
        ///// </summary>
        //public string WB_Code
        //{
        //    get
        //    {
        //        return wbCode;
        //    }
        //    set
        //    {
        //        wbCode = value;
        //    }
        //}


        ///// <summary>
        ///// �Զ�����
        ///// </summary>
        //public string User_Code
        //{
        //    get
        //    {
        //        return userCode;
        //    }
        //    set
        //    {
        //        userCode = value;
        //    }
        //}


        //#endregion

        #region Clone����

        /// <summary>
        /// Cloneʵ�屾��
        /// </summary>
        /// <returns></returns>
        public new BloodComponents Clone()
        {
            BloodComponents clone = base.Clone() as BloodComponents;

            clone.operInfo = this.operInfo.Clone();

            return clone;
        }

        #endregion
	}
}
