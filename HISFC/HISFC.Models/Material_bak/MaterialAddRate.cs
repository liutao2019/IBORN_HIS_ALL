using System;
using System.Collections.Generic;
using System.Collections;

namespace Neusoft.HISFC.Object.Material
{
    /// <summary>
    /// [��������: ���ʼӼ���ʵ��]
    /// [�� �� ��: Sunm]
    /// [����ʱ��: 2007-11-23]
    /// </summary>
    public class MaterialAddRate : Neusoft.NFC.Object.NeuObject
    {
        #region ����

        /// <summary>
        /// ���
        /// </summary>
        private string specs;

        /// <summary>
        /// �Ӽ۷�ʽ
        /// </summary>
        private MaterialAddRateEnumService rateKind = new MaterialAddRateEnumService();

        /// <summary>
        /// �۸�����
        /// </summary>
        private decimal priceLow;

        /// <summary>
        /// �۸�����
        /// </summary>
        private decimal priceHigh;

        /// <summary>
        /// �Ӽ���
        /// </summary>
        private decimal addRate;

        /// <summary>
        /// ���ӷ�
        /// </summary>
        private decimal appendFee;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.NFC.Object.NeuObject oper = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private DateTime operDate;

        /// <summary>
        /// ���ʿ�Ŀ
        /// </summary>
        private Neusoft.NFC.Object.NeuObject materialKind = new Neusoft.NFC.Object.NeuObject();

        #endregion

        #region ����

        /// <summary>
        /// �Ӽ۷�ʽ
        /// </summary>
        public MaterialAddRateEnumService RateKind
        {
            get 
            {
                return this.rateKind;
            }
            set 
            {
                this.rateKind = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public string Specs
        {
            get 
            {
                return this.specs;
            }
            set 
            {
                this.specs = value;
            }
        }

        /// <summary>
        /// �۸�����
        /// </summary>
        public decimal PriceLow
        {
            get 
            {
                return this.priceLow;
            }
            set
            {
                this.priceLow = value;
            }
        }

        /// <summary>
        /// �۸�����
        /// </summary>
        public decimal PriceHigh
        {
            get 
            {
                return this.priceHigh;
            }
            set 
            {
                this.priceHigh = value;
            }
        }

        /// <summary>
        /// �Ӽ���
        /// </summary>
        public decimal AddRate
        {
            get 
            {
                return this.addRate;
            }
            set 
            {
                this.addRate = value;
            }
        }

        /// <summary>
        /// ���ӷ�
        /// </summary>
        public decimal AppendFee
        {
            get 
            {
                return this.appendFee;
            }
            set 
            {
                this.appendFee = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.NFC.Object.NeuObject Oper
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

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime OperDate
        {
            get 
            {
                return this.operDate;
            }
            set 
            {
                this.operDate = value;
            }
        }

        /// <summary>
        /// ���ʿ�Ŀ
        /// </summary>
        public Neusoft.NFC.Object.NeuObject MaterialKind
        {
            get 
            {
                return this.materialKind;
            }
            set 
            {
                this.materialKind = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// Clone����
        /// </summary>
        /// <returns></returns>
        public new MaterialAddRate Clone()
        {
            MaterialAddRate materialAddRate = base.Clone() as MaterialAddRate;

            materialAddRate.oper = this.oper.Clone();

            materialAddRate.materialKind = this.materialKind.Clone();

            return materialAddRate;
        }

        #endregion
    }

    /// <summary>
    /// ö��
    /// </summary>
    public enum EnumRateKind
    {
        /// <summary>
        /// ���Ӽ�
        /// </summary>
        N = 0,
        /// <summary>
        /// �����Ӽ�
        /// </summary>
        S = 1,
        /// <summary>
        /// ���۸�Ӽ�
        /// </summary>
        P = 2,
        /// <summary>
        /// ���̶��Ӽ���
        /// </summary>
        R = 3
    }

    /// <summary>
    /// ö��
    /// </summary>
    public class MaterialAddRateEnumService : Neusoft.HISFC.Object.Base.EnumServiceBase
    {
        public MaterialAddRateEnumService()
        {
            items[EnumRateKind.N] = "���Ӽ�";
            items[EnumRateKind.S] = "�����Ӽ�";
            items[EnumRateKind.P] = "���۸�Ӽ�";
            items[EnumRateKind.R] = "���̶��Ӽ���";
        }

        #region ����
        /// <summary>
        /// �Ӽ۷�ʽ
        /// </summary>
        EnumRateKind enumRateKind;

        /// <summary>
        /// �洢ö�ٶ���
        /// </summary>
        protected static Hashtable items = new Hashtable();

        #endregion

        #region ����

        /// <summary>
        /// ����ö������
        /// </summary>
        protected override Hashtable Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// ö����Ŀ
        /// </summary>
        protected override System.Enum EnumItem
        {
            get
            {
                return this.enumRateKind;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }

    }
}
