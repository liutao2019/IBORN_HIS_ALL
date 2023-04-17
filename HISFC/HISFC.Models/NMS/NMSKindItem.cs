using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.NMS
{
    /// <summary>
    /// [����������������Ŀʵ��]
    /// [�� �� �ߣ�Ѧ�Ľ�]
    /// [����ʱ�䣺2008-09-28]
    /// [ID:��Ŀ��ˮ�� NAME����Ŀ���� MEME:��ע]
    /// [
    /// �޸��ˣ�ʯ����
    /// �޸�ʱ�䣺2008-10-10
    /// �޸ļ�¼������ֶ�itemId��Ŀ��ˮ�š�isValid ��Ч�ԡ�itemName ��Ŀ���ơ�itemMark ������Ŀ��ע
    /// �޸Ľ��飺ʵ���ֶ��������ݱ�������Bool��ö�٣����������ݿ⽻��������ҵ��ͽӿڲ�Ĺ�������
    /// ]
    /// </summary>
    [System.Serializable]
    public class NMSKindItem:Neusoft.FrameWork.Models.NeuObject
    {
        #region ���캯��

        /// <summary>
        /// ���캯����ID:��Ŀ��ˮ�� NAME����Ŀ���� MEME:��ע��
        /// </summary>
        public NMSKindItem()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region ����

        #region �����ֶ�
        
        private int isValid;
        /// <summary>
        /// ������Ŀ��Ч��
        /// </summary>
        public int IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        #endregion




        /// <summary>
        /// ������ˮ��
        /// </summary>
        private string kindID;
                       
        /// <summary>
        /// ��Ŀ˳���
        /// </summary>
        private int orderNO;

        /// <summary>
        /// ��Ŀ���� 1�ַ���2��ֵ3����4��Ա5����6����7�Ƿ�8���9�Զ���
        /// </summary>
        private HISFC.Models.Base.EnumNMSKindType itemType = new Neusoft.HISFC.Models.Base.EnumNMSKindType();

        /// <summary>
        /// ȡֵ��Χ 0����ֵ��1����ֵ�����ֵ��
        /// </summary>
        private bool itemArea;

        /// <summary>
        /// ��Ŀ��λ
        /// </summary>
        private string itemUnit;

        /// <summary>
        /// ���������ʶ 0���������1�ӷ�2�۷�
        /// </summary>
        private Neusoft.HISFC.Models.Base.EnumNMSKindGradeState kindGradeState = new Neusoft.HISFC.Models.Base.EnumNMSKindGradeState();

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private decimal itemGradeValue;

        /// <summary>
        /// ��Ŀ��ѯ��
        /// </summary>
        private string itemSpell;

        /// <summary>
        /// �Ƿ�ɱ༭ 1���ɱ༭��Ĭ�ϣ���0�����ɱ༭
        /// </summary>
        private bool isEdit;

        /// <summary>
        /// ��Ŀ��ţ����ڻ���ģ����ϸ��
        /// </summary>
        private string itemSign;

        /// <summary>
        /// ��Ŀֵ�����ڻ���ʱ����˼�¼��ϸ��
        /// </summary>
        private string itemValue;
 
        /// <summary>
        /// ����Ա����
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment itemOper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ������ˮ��
        /// </summary>
        public string KindID
        {
            get
            {
                return kindID;
            }
            set
            {
                kindID = value;
            }
        }

        /// <summary>
        /// ��Ŀ˳���
        /// </summary>
        public int OrderNO
        {
            get
            {
                return this.orderNO;
            }
            set
            {
                this.orderNO = value;
            }
        }

        /// <summary>
        /// ��Ŀ���� 1�ַ���2��ֵ3����4��Ա5����6����7�Ƿ�8���9�Զ���
        /// </summary>
        public Neusoft.HISFC.Models.Base.EnumNMSKindType ItemType
        {
            get
            {
                return this.itemType;
            }
            set
            {
                this.itemType = value;
            }
        }

        /// <summary>
        /// ȡֵ��Χ0����ֵ��1����ֵ�����ֵ��
        /// </summary>
        public bool ItemArea
        {
            get
            {
                return this.itemArea;
            }
            set
            {
                this.itemArea = value;
            }
        }

        /// <summary>
        /// ��Ŀ��λ
        /// </summary>
        public string ItemUnit
        {
            get
            {
                return this.itemUnit;
            }
            set
            {
                this.itemUnit = value;
            }
        }

        /// <summary>
        /// ���������ʶ0���������1�ӷ�2�۷�
        /// </summary>
        public Neusoft.HISFC.Models.Base.EnumNMSKindGradeState KindGradeState
        {
            get
            {
                return this.kindGradeState;
            }
            set
            {
                this.kindGradeState = value;
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public decimal ItemGradeValue
        {
            get
            {
                return this.itemGradeValue;
            }
            set
            {
                this.itemGradeValue = value;
            }
        }

        /// <summary>
        /// ��Ŀ��ѯ��
        /// </summary>
        public string ItemSpell
        {
            get
            {
                return this.itemSpell;
            }
            set
            {
                this.itemSpell = value;
            }
        }

        /// <summary>
        /// �Ƿ�ɱ༭ 1���ɱ༭��Ĭ�ϣ���0�����ɱ༭
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return this.isEdit;
            }
            set
            {
                this.isEdit = value;
            }
        }

        /// <summary>
        /// ��Ŀ��ţ����ڻ���ģ����ϸ��
        /// </summary>
        public string ItemSign
        {
            get
            {
                return itemSign;
            }
            set
            {
                itemSign = value;
            }
        }

        /// <summary>
        /// ��Ŀֵ�����ڻ���ʱ����˼�¼��ϸ��
        /// </summary>
        public string ItemValue
        {
            get
            {
                return itemValue;
            }
            set
            {
                itemValue = value;
            }
        }

        /// <summary>
        /// ����Ա����
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment ItemOper
        {
            get
            {
                return this.itemOper;
            }
            set
            {
                this.itemOper = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new NMSKindItem Clone()
        {
            NMSKindItem kindItem = base.Clone() as NMSKindItem;
            kindItem.ItemOper = this.itemOper.Clone();

            return kindItem;
        }

        #endregion


    }
}
