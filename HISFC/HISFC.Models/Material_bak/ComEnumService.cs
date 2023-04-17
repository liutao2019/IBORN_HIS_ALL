using System;
using System.Collections;
using Neusoft.NFC.Object;

namespace Neusoft.HISFC.Object.material
{
	/// <summary>
	/// ComEnumService ��ժҪ˵����
	/// </summary>
	public class ComEnumService:Neusoft.NFC.Object.NeuObject
	{		
		static ComEnumService()
		{
			items[EnumCompanyKind.Phamacy.ToString()] = "ҩƷ";
			items[EnumCompanyKind.Material.ToString()] = "����";
			items[EnumCompanyKind.Equipment.ToString()] = "�豸";
			items[EnumCompanyKind.All.ToString()] = "ȫ��";
		}

		private EnumCompanyKind enumCompanyKind;

		protected static System.Collections.Hashtable items = new System.Collections.Hashtable();

		protected  System.Collections.Hashtable Items
		{
			get 
			{
				return items;
			}
		}


		protected  Enum EnumItem
		{
			get 
			{
				return enumCompanyKind;
			}
		}


		public new string ID
		{
			get
			{
				if (base.ID == null)
					return string.Empty;
				else
					return base.ID;
			}
			set
			{
				if (value == null)
				{
					base.ID = "";
					base.Name = "";
					return;
				}
				base.ID = value.ToString();
				System.Enum enumTemp = (EnumCompanyKind)Enum.Parse(this.enumCompanyKind.GetType(),base.ID);
				if (items.ContainsKey(base.ID))
					this.Name = items[base.ID].ToString();
				else
					this.Name = "";
			}
		}

		/// <summary>
		/// �õ���Ŀ�����б�����
		/// </summary>
		/// <param name="items">Enum�ֵ�</param>
		/// <returns>NeuObject[]</returns>
		protected static NeuObject[] GetObjectItems(Hashtable items)
		{
			
			Neusoft.NFC.Object.NeuObject[] ret = new NeuObject[items.Count];
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
		/// �����б�
		/// </summary>
		/// <returns>neuobject����</returns>
		public new static System.Collections.ArrayList List()
		{
			return (new System.Collections.ArrayList(GetObjectItems(items)));
		}
	}

	public enum EnumCompanyKind
	{
		/// <summary>
		/// ҩƷ
		/// </summary>
		Phamacy = 1,
		/// <summary>
		/// ����
		/// </summary>
		Material = 2,
		/// <summary>
		/// �豸
		/// </summary>
		Equipment = 3,
		/// <summary>
		/// ȫ��
		/// </summary>
		All = 0,
	}
	
}
