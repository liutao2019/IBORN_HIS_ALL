using System;

namespace neusoft.HISFC.Object.MedTech
{
	/// <summary>
	/// MedTechBookApply ��������ҽ��ԤԼ��Ϣ��
	/// </summary>
	public class MedTechBookApply :neusoft.neuFC.Object.neuObject
	{
		public MedTechBookApply()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		///    ҽ����ˮ��                   
		/// </summary>
		public string MoOrder
		{
			get
			{
				return ItemList.MoOrder;
			}
			set
			{
				ItemList.MoOrder = value;
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject noon = new neusoft.neuFC.Object.neuObject();
		private int sortid;
		/// <summary>
		/// �������ʵ��
		/// </summary>
		/// <returns></returns>
		public neusoft.HISFC.Object.Fee.OutPatient.FeeItemList ItemList= new neusoft.HISFC.Object.Fee.OutPatient.FeeItemList();
        
		/// <summary>
		/// ��ĿԤԼ��չ��Ϣ
		/// </summary>
		/// <returns></returns>
		public neusoft.HISFC.Object.MedTech.ItemExtend ItemExtend = new ItemExtend();


		/// <summary>
		/// ԤԼ��Ϣ
		/// </summary>
		/// <returns></returns>
		public neusoft.HISFC.Object.MedTech.MedTechBookInfo MedTechBookInfo = new MedTechBookInfo();
		
		/// <summary>
		/// �����
		/// </summary>
		/// <returns></returns>
		public int SortID
		{
			get
			{
				return sortid;
				}
			set
			{
                sortid = value; 
			}

		}


		/// <summary>
		/// ����״��
		/// </summary>
		public string HealthFlag;
		/// <summary>
		/// ִ�еص�
		/// </summary>
		public neusoft.neuFC.Object.neuObject ExecLocate = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ȡ����ʱ��
		/// </summary>
		public System.DateTime ReportDate ;
		/// <summary>
		/// �Գ�Ա���п�¡
		/// </summary>
		/// <returns></returns>
		public new MedTechBookApply Clone()
		{
			MedTechBookApply obj = base.Clone() as MedTechBookApply;
			obj.ItemList = this.ItemList.Clone();
			obj.ItemExtend = this.ItemExtend.Clone();
			obj.MedTechBookInfo = this.MedTechBookInfo.Clone();
			return obj;
		}

	}
}
