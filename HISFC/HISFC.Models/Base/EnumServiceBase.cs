using System;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// EnumService<br></br>
	/// [��������: Enum��������࣬����ʵ��Enum��������]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-31]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
	public abstract class EnumServiceBase : NeuObject//, IEnumService
	{
		#region ����

		
		#endregion
		
		#region ����

		/// <summary>
		/// ����Enum��������
		/// </summary>
		protected abstract Hashtable Items
		{
			get;
		}

		/// <summary>
		/// ö����Ŀ
		/// </summary>
		protected abstract Enum EnumItem
		{
			get;
		}
        /// <summary>
        /// �����õ�ID�Ҳ���ʱ��������DefaultItem,Ĭ��ΪEnum��һ��
        /// </summary>
        protected virtual Enum DefaultItem
        {
            get
            {
                return (Enum.GetValues(this.EnumItem.GetType())).GetValue(0) as Enum;
            }
        }
		/// <summary>
		/// ID
		/// </summary>
        public new object ID
        {
            get
            {
                if (base.ID == null)
                {
                    return string.Empty;
                }
                else
                {
                    return base.ID;
                }
            }
            set
            {
                
                if(value==null)
                {
                    base.ID = " ";
                    this.Name = " ";
                    return;
                }

                base.ID = value.ToString();

                if (base.ID.Trim().Length == 0)
                {
                    this.Name = base.ID;
                    return;
                }
                string t = base.ID;
                if (char.IsNumber(base.ID[0]))
                {
                    t = Enum.GetName(this.EnumItem.GetType(), int.Parse(base.ID));
                }
                if (t == null)
                {
                    t = this.DefaultItem.ToString();
                }
                this.Name = this.GetName((Enum)Enum.Parse(this.EnumItem.GetType(), t));
            }
        }

        ///// <summary>
        ///// ��Ŀ�б�����
        ///// </summary>
        //public static NeuObject[] ObjectItems
        //{
        //    get
        //    {
        //        return GetObjectItems(item);
        //    }
        //}

		/// <summary>
		/// ��Ŀ�����б�����
		/// </summary>
		public string[] StringItems
		{
			get
			{
				return this.GetStringItems(this.Items);
			}
		}
		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new EnumServiceBase Clone()
		{
			EnumServiceBase enumServiceBase = base.Clone() as EnumServiceBase;

			return enumServiceBase;
		}

		/// <summary>
		/// �õ���Ŀ�����б�����
		/// </summary>
		/// <param name="items">Enum�ֵ�</param>
		/// <returns>NeuObject[]</returns>
		protected static NeuObject[] GetObjectItems(Hashtable items)
		{
			
			NeuObject[] ret = new NeuObject[items.Count];
			int i=0;
			DictionaryEntry de;
			IEnumerator en=items.GetEnumerator();
			while(en.MoveNext())
			{
				ret[i] = new NeuObject();
				de=(DictionaryEntry)en.Current;
				ret[i].ID = (de.Key).ToString();
				ret[i].Name = items[de.Key] as string;
				i++;
			}

			return ret;
			
		}

		/// <summary>
		/// �õ���Ŀ�����б�����
		/// </summary>
		/// <param name="items">Enum�ֵ�</param>
		/// <returns>string[]</returns>
		private string[] GetStringItems(Hashtable items)
		{
			
			string[] ret = new string[items.Count];
			int i=0;
			IEnumerator en=items.Values.GetEnumerator();
			while(en.MoveNext())
			{
				ret[i]=en.Current as string;
				i++;
			}

			return ret;		
		}

		/// <summary>
		/// �õ�ö����������
		/// </summary>
		/// <param name="enumType">ö��</param>
		/// <returns>ö����������</returns>
		public string GetName(Enum enumType)
		{
			if (Items.ContainsKey(enumType)) 
			{
				return this.Items[enumType].ToString();
			}
			else
			{
				throw new EnumNotFoundException(enumType);
			}
		}

        public  ArrayList List()
        {
            return (new ArrayList(GetObjectItems(this.Items)));
        }
		#endregion

	}

}
