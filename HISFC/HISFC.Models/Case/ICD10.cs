using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// ICD10 ��ժҪ˵����
	/// ICD10�������
	/// </summary>
	public class ICD10:neusoft.neuFC.Object.neuObject 
	{
		public ICD10()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//.
		}
		/// <summary>
		/// ����������
		/// </summary>
		public string  DiseaseCode;
		/// <summary>
		/// �������
		/// </summary>
		public string  SICD10;
		/// <summary>
		/// ����ԭ��
		/// </summary>
		public string DeadReason;
		/// <summary>
		/// ��׼סԺ��
		/// </summary>
		public int InDays;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate;

		public DiagnoseType DiagnoseType = new DiagnoseType();
		
		public neusoft.HISFC.Object.Base.SpellCode SpellCode=new neusoft.HISFC.Object.Base.SpellCode();
		public new ICD10 Clone()
		{
			ICD10 obj=base.Clone() as ICD10;
			//obj.DiagnoseType = this.DiagnoseType.Clone();
			obj.SpellCode = this.SpellCode.Clone();
			return obj;
		}
	}
}
