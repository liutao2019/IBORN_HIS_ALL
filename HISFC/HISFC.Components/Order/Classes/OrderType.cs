using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Order.Classes
{
    /// <summary>
    /// [��������: ҽ��״̬�����ڴ泣��]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class OrderType
    {

        protected static System.Collections.ArrayList longOrderTypes = null;
        
        /// <summary>
        /// ҽ���õĳ���ҽ�������б�
        /// </summary>
        public static System.Collections.ArrayList LongOrderTypes
        {
            get
            {
                if (longOrderTypes == null)
                {
                    GetOrderType();
                }
                return longOrderTypes;
            }
            set
            {
                longOrderTypes = value;
            }
        }

        protected static System.Collections.ArrayList shortOrderTypes = null;

        /// <summary>
        /// ҽ���õ���ʱҽ�������б�
        /// </summary>
        public static System.Collections.ArrayList ShortOrderTypes
        {
            get
            {
                if (shortOrderTypes == null)
                {
                    GetOrderType();
                }
                return shortOrderTypes;
            }
            set
            {
                shortOrderTypes = value;
            }
        }

        private static void GetOrderType()
        {
            System.Collections.ArrayList al = CacheManager.InterMgr.QueryOrderTypeList();
            if (al == null) return;
            longOrderTypes = new System.Collections.ArrayList();
            shortOrderTypes = new System.Collections.ArrayList();
            foreach (FS.HISFC.Models.Order.OrderType obj in al)
            {
                if (obj.IsDecompose)
                {
                    //�ж��Ƿ�Ӧ�õ�ǰҽԺ���������
                    longOrderTypes.Add(obj);
                }
                else
                {
                    //�ж��Ƿ�Ӧ�õ�ǰҽԺ���������
                    shortOrderTypes.Add(obj);
                }
            }
        }

        /// <summary>
        /// ���ܻ��õ���ѯ���ݿ⣬С����
        /// </summary>
        /// <param name="charge"></param>
        /// <param name="isLong"></param>
        public static void CheckChargeableOrderType(ref FS.HISFC.Models.Order.OrderType currentType,bool charge)
        {
            if(currentType.IsDecompose)
                CheckChargeableOrderType(ref currentType, charge, LongOrderTypes);
            else
                CheckChargeableOrderType(ref currentType, charge, ShortOrderTypes);
        }
        /// <summary>
        /// ����ȡ
        /// </summary>
        /// <param name="currentType"></param>
        /// <param name="charge"></param>
        /// <param name="reset">�Ƿ�����ȡ</param>
        public static void CheckChargeableOrderType(ref FS.HISFC.Models.Order.OrderType currentType, bool charge,bool reset)
        {
            charge = currentType.IsCharge;

            if (currentType.IsDecompose)
                CheckChargeableOrderType(ref currentType, charge, LongOrderTypes,reset);
            else
                CheckChargeableOrderType(ref currentType, charge, ShortOrderTypes,reset);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="charge"></param>
        public static void CheckChargeableOrderType(ref FS.HISFC.Models.Order.OrderType currentType,bool charge,System.Collections.ArrayList orderTypes)
        {
            //�жϵ�ǰҽ���շ�����            
            if (currentType != null)
            {
                if (currentType.IsCharge == charge)
                    return;
            }
            //�����ϣ����ҵ�һ�����ϵ��շ�����
            foreach (FS.HISFC.Models.Order.OrderType obj in orderTypes)
            {
                if (obj.IsCharge == charge)
                {
                    currentType = obj.Clone();
                    return;
                }
            }
        }
        public static void CheckChargeableOrderType(ref FS.HISFC.Models.Order.OrderType currentType, bool charge, System.Collections.ArrayList orderTypes,bool reset)
        {
            //�жϵ�ǰҽ���շ�����            
            if (reset == false && currentType != null)
            {
                if (currentType.IsCharge == charge)
                    return;
            }
            //�����ϣ����ҵ�һ�����ϵ��շ�����
            foreach (FS.HISFC.Models.Order.OrderType obj in orderTypes)
            {
                if (obj.IsCharge == charge)
                {
                    currentType = obj.Clone();
                    return;
                }
            }
        }

    }
}
