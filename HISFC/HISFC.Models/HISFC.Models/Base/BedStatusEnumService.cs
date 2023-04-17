using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// [��������: ��λ״̬ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ΰ'
	///		�޸�ʱ��='2006-9-12'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
    [System.Serializable]
    public class BedStatusEnumService:EnumServiceBase	
	{

		/// <summary>
		/// ö�ٶ��� ��λ״̬ʵ��������
		/// </summary>
		public BedStatusEnumService()
		{
			items[EnumBedStatus.C] = "�ر�";//Closed
			items[EnumBedStatus.U] = "�մ�";//Unoccupied
			items[EnumBedStatus.K] = "��Ⱦ";
			items[EnumBedStatus.I] = "����";
			items[EnumBedStatus.O] = "ռ��";
			items[EnumBedStatus.R] = "�ٴ�";
			items[EnumBedStatus.W] = "����";
			items[EnumBedStatus.H] = "�Ҵ�";
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new  static ArrayList List()
        {
             return (new ArrayList(GetObjectItems(items)));
        }

        /// <summary>
        /// ռ��
        /// </summary>
        /// <returns></returns>
        public static ArrayList OccupiedList()
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Base.BedStatusEnumService obj1 = new BedStatusEnumService();
            obj1.ID = EnumBedStatus.O;
            al.Add(obj1);

            FS.HISFC.Models.Base.BedStatusEnumService obj2 = new BedStatusEnumService();
            obj2.ID = EnumBedStatus.R;
            al.Add(obj2);

            return al;
        }

        /// <summary>
        /// ��ռ�ò���
        /// </summary>
        /// <returns></returns>
        public static ArrayList UnoccupiedList()
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Base.BedStatusEnumService obj1 = new BedStatusEnumService();
            obj1.ID = EnumBedStatus.C;
            al.Add(obj1);

            FS.HISFC.Models.Base.BedStatusEnumService obj2 = new BedStatusEnumService();
            obj2.ID = EnumBedStatus.U;
            al.Add(obj2);

            FS.HISFC.Models.Base.BedStatusEnumService obj3 = new BedStatusEnumService();
            obj3.ID = EnumBedStatus.K;
            al.Add(obj3);

            FS.HISFC.Models.Base.BedStatusEnumService obj4 = new BedStatusEnumService();
            obj4.ID = EnumBedStatus.I;
            al.Add(obj4);

            return al;
        }
		#region ����

		private EnumBedStatus enumBedStatus;	
		/// <summary>
		/// ����ö������
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
		
		protected override Enum EnumItem
		{
			get
			{
				return this.enumBedStatus;
			}
		}

       
		#endregion  	
	}
}
