using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// [��������: ����״̬ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ΰ'
	///		�޸�ʱ��='2006-9-12'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
    [Serializable]
    public class MaritalStatusEnumService :EnumServiceBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		static MaritalStatusEnumService()
		{
			items[EnumMaritalStatus.S] = "δ��";
			items[EnumMaritalStatus.M] = "�ѻ�";
			items[EnumMaritalStatus.D] = "����";
			items[EnumMaritalStatus.R] = "�ٻ�";
			items[EnumMaritalStatus.A] = "�־�";
			items[EnumMaritalStatus.W] = "ɥż";
		}
		
		
		#region ����

		private EnumMaritalStatus enumMaritalStatus;
		
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
				return this.enumMaritalStatus;
			}
		}

		#endregion  	
		
		#region ����
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            ArrayList al = new ArrayList(GetObjectItems(items));
            al.Sort(new MaritalCompare());
            return al;
            //return (new ArrayList(GetObjectItems(items)));
        }

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new MaritalStatusEnumService Clone()
        {
            return base.Clone() as MaritalStatusEnumService;
        }

		#endregion

    }

    #region ����

    /// <summary>
    /// �Ի���״����������-��Ϊ��ϣ����˳��������һ������
    /// </summary>
    public class MaritalCompare  :IComparer
    {

        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj1 = x as FS.FrameWork.Models.NeuObject;
                FS.FrameWork.Models.NeuObject obj2 = y as FS.FrameWork.Models.NeuObject;
                int order1 = this.GetOrderNum(obj1);
                int order2 = this.GetOrderNum(obj2);
                if (order1 > order2)
                {
                    return 1;
                }
                else if (order1 == order2)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }


            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion

        /// <summary>
        /// ��ȡ����״����˳��
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int GetOrderNum(FS.FrameWork.Models.NeuObject obj)
        {
            int i = 0;
            if (obj.ID == "S")
            {
                i = 0;
            }
            else if (obj.ID == "M")
            {
                i = 1;
            }
            else if (obj.ID == "D")
            {
                i = 3;
            }
            else if (obj.ID == "R")
            {
                i = 4;
            }
            else if (obj.ID == "A")
            {
                i = 5;
            }
            else if (obj.ID == "W")
            {
                i = 6;
            }
            else
            {
                i = 99;
            }
            return i;
        }
    }

    #endregion

}
