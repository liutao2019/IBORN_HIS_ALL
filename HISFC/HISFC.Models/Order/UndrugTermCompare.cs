using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Order
{
    [System.Serializable]
    public class UndrugTermCompare : Neusoft.NFC.Object.NeuObject
    {
        /// <summary>
        /// [��������: ��ҩƷ���ʶ��ձ�]
        /// [�� �� ��: �ų�]
        /// [����ʱ��: 2008-03-25]
        /// ID ������ձ���� 
        /// 
        ///     �޸�˵����ԭʹ��Undrug�������Qty�ֶΣ�����ȫ�ñ�ʵ��Qty
        ///     �� �� �ˣ�������
        ///     �޸����ڣ�2008-7-15
        /// </summary>
        public UndrugTermCompare()
        {

        }

        #region ��
        /// <summary>
        /// ����ʵ��
        /// </summary>
        private MedicalTerm term = new MedicalTerm();
        /// <summary>
        /// ��ҩƷʵ��
        /// </summary>
        private Fee.Item.Undrug undrug = new Neusoft.HISFC.Object.Fee.Item.Undrug();
        /// <summary>
        /// �������
        /// </summary>
        private int compare_NO;
        /// <summary>
        /// ͣ������
        /// </summary>
        private DateTime stopDate;

        /// <summary>
        /// ��Ч���
        /// </summary>
        private string vaildFlag;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion

        #region ����
        /// <summary>
        /// ����ʵ��
        /// </summary>
        public MedicalTerm Term
        {
            get
            {
                return this.term;
            }
            set
            {
                this.term = value;
            }
        }

        /// <summary>
        /// ��ҩƷʵ��
        /// </summary>
        public Fee.Item.Undrug Undrug
        {
            get
            {
                return this.undrug;
            }
            set
            {
                this.undrug = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        decimal qty = 1;

     

        /// <summary>
        /// �������
        /// </summary>
        public int Compare_NO
        {
            get
            {
                return this.compare_NO;
            }
            set
            {
                this.compare_NO = value;
            }
        }

        /// <summary>
        /// ͣ������
        /// </summary>
        public DateTime StopDate
        {
            get { return stopDate; }
            set { stopDate = value; }
        }

        /// <summary>
        /// ��Ч���
        /// </summary>
        public string VaildFlag
        {
            get { return vaildFlag; }
            set { vaildFlag = value; }
        }


        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment Oper
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
        /// ��ҩƷ����
        /// </summary>
        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        #endregion

        #region ����
        /// <summary>
        /// ������¡
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���UndrugMatCompareʵ�� ʧ�ܷ���null</returns>

        public new UndrugTermCompare Clone()
        {
            UndrugTermCompare compare = base.Clone() as UndrugTermCompare;

            compare.Term = this.Term.Clone();

            compare.Undrug = this.Undrug.Clone();

            compare.oper = this.oper.Clone();

            return compare;
        }
        #endregion

    }
}
