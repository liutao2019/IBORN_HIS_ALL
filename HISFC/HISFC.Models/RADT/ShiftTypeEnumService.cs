using System;
using System.Collections;

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
    public class ShiftTypeEnumService : Base.EnumServiceBase
	{
        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
		/// <summary>
		/// ���캯��
		/// </summary>
		public ShiftTypeEnumService()
		{
			items[Base.EnumShiftType.RD] = "ת��";
			items[Base.EnumShiftType.RB] = "ת��1";
			items[Base.EnumShiftType.RI] = "ת��";
			items[Base.EnumShiftType.RO] = "ת��";
			items[Base.EnumShiftType.K] = "����2";
			items[Base.EnumShiftType.B] = "סԺ�Ǽ�3";
			items[Base.EnumShiftType.C] = "�ٻ�4";
			items[Base.EnumShiftType.OF] = "�޷���Ժ";
			items[Base.EnumShiftType.BA] = "����";
			items[Base.EnumShiftType.BB] = "�����ٻ�";
			items[Base.EnumShiftType.MB] = "��;����";
			items[Base.EnumShiftType.F] = "������Ϣ�޸�";
			items[Base.EnumShiftType.LB] = "���괲�ͳ���յ��޸�";
			items[Base.EnumShiftType.DL] = "�������޶���";
			items[Base.EnumShiftType.BT] = "�������޶��ۼ�";
			items[Base.EnumShiftType.BP] = "�����嵥��ӡ";
			items[Base.EnumShiftType.CP] = "��ݱ��";
			items[Base.EnumShiftType.ZM] = "��ҽ���շ�֤����";
			items[Base.EnumShiftType.O] = "��Ժ�Ǽ�5";
            items[Base.EnumShiftType.EB] = "���۵Ǽ�";
            items[Base.EnumShiftType.CPI] = "����תסԺ";
            items[Base.EnumShiftType.CI] = "������Ա";
            items[Base.EnumShiftType.IC] = "����תסԺ�ٻ�";
            //{D97A6AA0-5AFB-443f-B74D-1AD1604B1567} ���ӿ�������־ yangw 20100907
            items[Base.EnumShiftType.AC] = "����";
            items[Base.EnumShiftType.AO] = "����";
            items[Base.EnumShiftType.EC] = "��Ժ�ٻ�ȡ��";
            items[Base.EnumShiftType.EO] = "��Ժ�Ǽ�ȡ��";

		}
		
		
		#region ����

		private Base.EnumShiftType  enumShiftType ;
		
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
				return this.enumShiftType;
			}
		}

        /// <summary>
        /// �õ�ö����������
        /// </summary>
        /// <param name="enumType">ö��</param>
        /// <returns>ö����������</returns>
        //public string GetName(Enum enumType)
        //{
        //    if (Items.ContainsKey(enumType))
        //    {
        //        return this.Items[enumType].ToString();
        //    }
        //    else
        //    {
        //        throw new FS.HISFC.Models.Base.EnumNotFoundException(enumType);
        //    }
        //}

		#endregion  	
		
		#region ����
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(items.Values));
        }
		#endregion
	}
}
