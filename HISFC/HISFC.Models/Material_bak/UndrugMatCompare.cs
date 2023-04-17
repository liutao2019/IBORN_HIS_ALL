using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Material
{
    public class UndrugMatCompare : Neusoft.NFC.Object.NeuObject
    {
        /// <summary>
        /// [��������: ��ҩƷ���ʶ��ձ�]
        /// [�� �� ��: �ų�]
        /// [����ʱ��: 2008-03-25]
        /// ID ������ձ���� 
        /// </summary>
        public UndrugMatCompare()
        {

        }
        #region ��
        /// <summary>
        /// ����ʵ��
        /// </summary>
        private MaterialItem matitem = new MaterialItem();
        /// <summary>
        /// ��ҩƷʵ��
        /// </summary>
        private Fee.Item.Undrug undrug = new Neusoft.HISFC.Object.Fee.Item.Undrug();
        /// <summary>
        /// �������
        /// </summary>
        private int compare_NO;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();
        #endregion

        #region ����
        /// <summary>
        /// ����ʵ��
        /// </summary>
        public MaterialItem MatItem
        {
            get
            {
                return this.matitem;
            }
            set
            {
                this.matitem = value;
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

        #endregion

        #region ����
        /// <summary>
        /// ������¡
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���UndrugMatCompareʵ�� ʧ�ܷ���null</returns>

        public new UndrugMatCompare Clone()
        {
            UndrugMatCompare compare = base.Clone() as UndrugMatCompare;

            compare.MatItem = this.MatItem.Clone();

            compare.Undrug = this.Undrug.Clone();

            compare.oper = this.oper.Clone();

            return compare;
        }
        #endregion


    }
}
