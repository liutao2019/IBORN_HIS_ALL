using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy
{
    [Serializable]
    public class DrugStencilEnumService : FS.FrameWork.Models.NeuObject
    {
        static DrugStencilEnumService()
		{
            items[EnumDrugStencil.Check.ToString()] = "�̵�";
            items[EnumDrugStencil.Plan.ToString()] = "�ƻ�";
            items[EnumDrugStencil.Apply.ToString()] = "����";
            items[EnumDrugStencil.Custom.ToString()] = "�Զ���";
		}

        private EnumDrugStencil enumDrugStencil;

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
				return enumDrugStencil;
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
                //by cube 2011-04-24 �������ô��룬��չ�Զ������ͺ󱨴�
                //System.Enum enumTemp = (EnumDrugStencil)Enum.Parse(this.enumDrugStencil.GetType(), base.ID);
                //end by
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
		/// <returns>neuObject[]</returns>
		protected static FS.FrameWork.Models.NeuObject[] GetObjectItems(Hashtable items)
		{
            FS.FrameWork.Models.NeuObject[] ret = new FS.FrameWork.Models.NeuObject[items.Count];
			int i=0;
			DictionaryEntry de;
			IEnumerator en = items.GetEnumerator();
			while(en.MoveNext())
			{
				ret[i] = new FS.FrameWork.Models.NeuObject();
				de = (DictionaryEntry)en.Current;
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

    public enum EnumDrugStencil
    {
        /// <summary>
        /// �̵�
        /// </summary>
        Check,
        /// <summary>
        /// �ƻ�
        /// </summary>
        Plan,
        /// <summary>
        /// ����
        /// </summary>
        Apply,
        /// <summary>
        /// �Զ���
        /// </summary>
        Custom
    }
}
