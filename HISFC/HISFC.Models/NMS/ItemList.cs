using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.NMS
{
    /// <summary>
    /// [����������������Ŀ����ʵ��]
    /// [�� �� �ߣ�����]
    /// [����ʱ�䣺2008-10-7]
    /// [ID:����Ŀ��ˮ�� MEMO:��ע]
    /// </summary>
    [System.Serializable]
    public class ItemList:Neusoft.FrameWork.Models.NeuObject
    {
        #region ���캯��

        /// <summary>
        /// ���캯�� [ID:����Ŀ��ˮ�� MEMO:��ע]
        /// </summary>
        public ItemList()
        {
        }

        #endregion

        #region ����

        /// <summary>
        /// ��Ŀ��ˮ��
        /// </summary>
        private string itemId;

        /// <summary>
        /// ����˳���
        /// </summary>
        private decimal orderNo;

        /// <summary>
        /// �������
        /// </summary>
        private string listCode;

        /// <summary>
        /// ��������
        /// </summary>
        private string listName;

        /// <summary>
        /// ����Ա����
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment itemOper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ��Ŀ��ˮ��
        /// </summary>
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
            }
        }

        /// <summary>
        /// ����˳���
        /// </summary>
        public decimal OrderNo
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
        /// �������
        /// </summary>
        public string ListCode
        {
            get
            {
                return listCode;
            }
            set
            {
                listCode = value;
            }
        }
        
        /// <summary>
        /// ��������
        /// </summary>
        public string ListName
        {
            get
            {
                return listName;
            }
            set
            {
                listName = value;
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

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ�����ʵ������</returns>
        public new ItemList Clone()
        {
            ItemList itemlist = base.Clone() as ItemList;
            itemlist.ItemOper = this.itemOper.Clone();
            return itemlist;
        }

        #endregion
    }
}
