using System;

namespace neusoft.HISFC.Object.MedTech
{
	/// <summary>
	/// Deptitemdetail ��ժҪ˵����
	/// </summary>
	public class MedTechItem :neusoft.neuFC.Object.neuObject
	{
		public MedTechItem()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��Ŀ��Ϣ
		/// </summary>
		public neusoft.HISFC.Object.Fee.Item Item = new neusoft.HISFC.Object.Fee.Item();
		/// <summary>
		/// ��Ŀ��չ��Ϣ
		/// </summary>
		public neusoft.HISFC.Object.MedTech.ItemExtend ItemExtend = new ItemExtend();
		public new MedTechItem Clone()
		{
			MedTechItem obj=base.Clone() as MedTechItem;
						obj.Item =this.Item.Clone();
//						obj.MedTechItemExtend = this.MedTechItemExtend.Clone();
			return obj;
		}
	}
}
