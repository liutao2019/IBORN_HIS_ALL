using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// Diagnose ��ժҪ˵����
	/// ���������
	/// </summary>
	public class DiagnoseBase:neusoft.neuFC.Object.neuObject 
	{
		public DiagnoseBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//.
		}
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public neusoft.HISFC.Object.RADT.Patient Patient = new neusoft.HISFC.Object.RADT.Patient();
		/// <summary>
		/// �������(10λ����)
		/// </summary>
		public int HappenNo;
		/// <summary>
		/// ������
		/// </summary>
		public neusoft.neuFC.Object.neuObject DiagType = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ICD10
		/// </summary>
		public neusoft.HISFC.Object.Case.ICD  ICD10 = new ICD();
		/// <summary>
		/// ���ʱ��
		/// </summary>
		public DateTime DiagDate;
		/// <summary>
		/// ���ҽ��
		/// </summary>
		public neusoft.neuFC.Object.neuObject Doctor = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ��Ͽ���
		/// </summary>
		public neusoft.neuFC.Object.neuObject Dept = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// �������
		/// </summary>
		public string OperationNo="";
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		public bool IsValid;
		/// <summary>
		/// �Ƿ������
		/// </summary>
		public bool IsMain;
		/// <summary>
		/// ��������
		/// </summary>
		public neusoft.HISFC.Object.Base.SpellCode SpellCode=new neusoft.HISFC.Object.Base.SpellCode();

		public new DiagnoseBase Clone()
		{
			DiagnoseBase obj= base.Clone() as DiagnoseBase;
			obj.DiagType = DiagType.Clone();
			obj.ICD10 = ICD10.Clone();
			obj.Dept = Dept.Clone();
			obj.Doctor = Doctor.Clone();
			obj.SpellCode = SpellCode.Clone();
			return obj;
		}
	}
}
