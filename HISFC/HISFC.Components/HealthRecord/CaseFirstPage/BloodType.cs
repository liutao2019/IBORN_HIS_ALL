using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
 
	public class BloodType : EnumServiceBase
	{
        public BloodType()
		{
			this.Items[EnumBloodTypeByABO.A] = "A��";
            this.Items[EnumBloodTypeByABO.AB] = "AB��";
            this.Items[EnumBloodTypeByABO.B] = "B��";
            this.Items[EnumBloodTypeByABO.O] = "O��";
            this.Items[EnumBloodTypeByABO.U] = "δ֪";
		}

		#region ����
 
        EnumBloodTypeByABO enumBloodType;

		/// <summary>
		/// �洢ö�ٶ���
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
		
		/// <summary>
		/// ö����Ŀ
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
				return this.enumBloodType;
			}
		}

		#endregion

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
        public new BloodType Clone()
		{
            BloodType bloodEnumService = base.Clone() as BloodType;

            return bloodEnumService;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList( GetObjectItems(items)));
        }
	}

	
}
