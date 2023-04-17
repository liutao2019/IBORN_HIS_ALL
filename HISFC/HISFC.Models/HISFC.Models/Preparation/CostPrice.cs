using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Preparation
{
    /// <summary>
    /// [��������: �Ƽ��Ӽ۹�ʽ��Ϣ��]<br></br>
    /// [�� �� ��: Dorian]<br></br>
    /// [����ʱ��: 2008-04-10]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class CostPrice : FS.FrameWork.Models.NeuObject
    {
        public CostPrice()
        {

        }

        #region �����

        /// <summary>
        /// ��Ʒ(ҩƷ)��Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item drugItem = new FS.HISFC.Models.Pharmacy.Item();

        /// <summary>
        /// �ɱ��ۼ��㹫ʽ
        /// </summary>
        private string costPriceFormula;

        /// <summary>
        /// ���ۼۼ��㹫ʽ
        /// </summary>
        private string salePriceFormula;

        /// <summary>
        /// ��ʽ��Դ
        /// </summary>
        private string priceSource;

        /// <summary>
        /// �ɱ�������
        /// </summary>
        private decimal priceRate;

        /// <summary>
        /// ��չ��Ϣ
        /// </summary>
        private string extend;

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
        
        /// <summary>
        ///��װ��λ 
        /// </summary>
        private string pactUnit;
       

        /// <summary>
        /// ��װ����
        /// </summary>
        private int pactQty;
       
        /// <summary>
        /// ��С��λ
        /// </summary>
        private string minUnit;
       

        /// <summary>
        /// ���
        /// </summary>
        private string specs;
       
        #endregion

        #region ����

        /// <summary>
        /// ��Ʒ(ҩƷ)��Ϣ
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item DrugItem
        {
            get
            {
                return this.drugItem;
            }
            set
            {
                this.drugItem = value;
            }
        }

        /// <summary>
        /// �ɱ��ۼ��㹫ʽ
        /// </summary>
        public string CostPriceFormula
        {
            get
            {
                return this.costPriceFormula;
            }
            set
            {
                this.costPriceFormula = value;
            }
        }

        /// <summary>
        /// ���ۼۼ��㹫ʽ
        /// </summary>
        public string SalePriceFormula
        {
            get
            {
                return this.salePriceFormula;
            }
            set
            {
                this.salePriceFormula = value;
            }
        }

        /// <summary>
        /// ��ʽ��Դ
        /// </summary>
        public string PriceSource
        {
            get
            {
                return this.priceSource;
            }
            set
            {
                this.priceSource = value;
            }
        }

        /// <summary>
        /// �ɱ�������
        /// </summary>
        public decimal PriceRate
        {
            get
            {
                return this.priceRate;
            }
            set
            {
                this.priceRate = value;
            }
        }

        /// <summary>
        /// ��չ��Ϣ
        /// </summary>
        public string Extend
        {
            get
            {
                return this.extend;
            }
            set
            {
                this.extend = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
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
        ///��װ��λ 
        /// </summary>
        public string PactUnit
        {
            get
            {
                return pactUnit;
            }
            set
            {
                pactUnit = value;
            }
        }

        /// <summary>
        /// ��װ����
        /// </summary>
        public int PactQty
        {
            get
            {
                return pactQty;
            }
            set
            {
                pactQty = value;
            }
        }
        /// <summary>
        /// ��С��λ
        /// </summary>
        public string MinUnit
        {
            get
            {
                return minUnit;
            }
            set
            {
                minUnit = value;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Specs
        {
            get
            {
                return specs;
            }
            set
            {
                specs = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���ʵ�� ʧ�ܷ���null</returns>
        public new CostPrice Clone()
        {
            CostPrice c = base.Clone() as CostPrice;

            c.oper = this.oper.Clone();
            c.drugItem = this.drugItem.Clone();

            return c;
        }

        #endregion

    }
}
