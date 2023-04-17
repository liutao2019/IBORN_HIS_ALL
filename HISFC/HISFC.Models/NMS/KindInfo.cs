using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.NMS
{
    /// <summary>
    /// [����������������Ŀ����ʵ��]
    /// [�� �� �ߣ�����]
    /// [����ʱ�䣺2008-10-7]
    /// [ID:������ˮ�� NAME:�������� MEMO:��ע spellCode ƴ���� wubiCode����� userCode �Զ�����]
    /// </summary>
    [System.Serializable]
    public class KindInfo:Neusoft.HISFC.Models.Base.Spell
    {
        #region ���캯��

        /// <summary>
        /// ���캯��[ID:������ˮ�� NAME:�������� MEMO:��ע spellCode ƴ���� wubiCode����� userCode �Զ�����]
        /// </summary>
        public KindInfo()
        {
        }

        #endregion

        #region ����

        /// <summary>
        /// �ϼ����루�����ϼ�Ϊ0��
        /// </summary>
        private string preCode;

        /// <summary>
        /// ���������ʶ 0���������1�ӷ�2�۷�
        /// </summary>
        private int kindGradeState;

        /// <summary>
        /// �������ֵ
        /// </summary>
        private decimal kindGradeValue;

        /// <summary>
        /// �Ƿ�ĩ��1��0��
        /// </summary>
        private bool leafFlag;

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        private bool validFlag;

        /// <summary>
        /// ˳���
        /// </summary>
        private int orderNo;

        /// <summary>
        /// ����Ա����(����Ա���룬����ʱ��)
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment itemOper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        ///// <summary>
        ///// ����˳���(���ڻ���ģ����ϸ)
        ///// </summary>
        //private int kindOrder;

        /// <summary>
        /// ������(���ڻ���ģ����ϸ)
        /// </summary>
        private string kindSign;

        /// <summary>
        /// ������ʾ���:1��ʾ����,0����ʾ����(���ڻ���ģ����ϸ)
        /// </summary>
        private bool kindShowFlag;

        #endregion

        #region ����

        /// <summary>
        /// �ϼ����루�����ϼ�Ϊ0��
        /// </summary>
        public string PreCode
        {
            get
            {
                return preCode;
            }
            set
            {
                preCode = value;
            }
        }

        /// <summary>
        /// ���������ʶ 0���������1�ӷ�2�۷�
        /// </summary>
        public int KindGradeState
        {
            get
            {
                return kindGradeState;
            }
            set
            {
                kindGradeState = value;
            }
        }

        /// <summary>
        /// �������ֵ
        /// </summary>
        public decimal KindGradeValue
        {
            get
            {
                return kindGradeValue;
            }
            set
            {
                kindGradeValue = value;
            }
        }

        /// <summary>
        /// �Ƿ�ĩ��1��0��
        /// </summary>
        public bool LeafFlag
        {
            get
            {
                return leafFlag;
            }
            set
            {
                leafFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        public bool ValidFlag
        {
            get
            {
                return validFlag;
            }
            set
            {
                validFlag = value;
            }
        }

        /// <summary>
        /// ˳���
        /// </summary>
        public int OrderNo
        {
            get
            {
                return orderNo;
            }
            set
            {
                orderNo = value;
            }
        }

        /// <summary>
        /// ����Ա����
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment ItemOper
        {
            get
            {
                return itemOper;
            }
            set
            {
                itemOper = value;
            }
        }

        ///// <summary>
        ///// ����˳���(���ڻ���ģ����ϸ)
        ///// </summary>
        //public int KindOrder
        //{
        //    get
        //    {
        //        return kindOrder;
        //    }
        //    set
        //    {
        //        kindOrder = value;
        //    }
        //}

        /// <summary>
        /// ������(���ڻ���ģ����ϸ)
        /// </summary>
        public string KindSign
        {
            get
            {
                return kindSign;
            }
            set
            {
                kindSign = value;
            }
        }

        /// <summary>
        /// ������ʾ���:1��ʾ����,0����ʾ����(���ڻ���ģ����ϸ)
        /// </summary>
        public bool KindShowFlag
        {
            get
            {
                return kindShowFlag;
            }
            set
            {
                kindShowFlag = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new KindInfo Clone()
        {
            KindInfo kindinfo = base.Clone() as KindInfo;
            kindinfo.ItemOper = this.itemOper.Clone();
            return kindinfo;
        }

        #endregion
    }
}
