using System;
 
using Neusoft.NFC.Object;
using System.Collections;
namespace Neusoft.HISFC.Object.Order
{
	/// <summary>
	/// �����
	/// ID ��ˮ�� name ����
	/// 2004-5 
	/// </summary>
	public class DiagNose:Neusoft.NFC.Object.NeuObject
	{
		/// <summary>
		///������
		/// </summary>
		public DiagNose()
		{
		
		}
		/// <summary>
		/// ���߻�����Ϣ
		/// </summary>
		public Neusoft.HISFC.Object.RADT.Patient Patient=new Neusoft.HISFC.Object.RADT.Patient();
		/// <summary>
		/// ������ 
		/// </summary>
		public NeuObject DiagType=new NeuObject();
		/// <summary>
		/// �������
		/// </summary>
		public int HappenNo;
		/// <summary>
		/// �������
		/// </summary>
		public DateTime DiagDate;
		/// <summary>
		/// ҽ��
		/// </summary>
		public NeuObject Doctor;
		/// <summary>
		/// ����
		/// </summary>
		public NeuObject Dept;
		/// <summary>
		/// �Ƿ���Ч 
		/// </summary>
		public bool IsValid=true;
		/// <summary>
		/// �Ƿ������
		/// </summary>
		public bool IsMain=false;
		
		public new DiagNose Clone()
		{
			DiagNose obj=base.Clone() as DiagNose;
			obj.Doctor=this.Doctor.Clone();
			obj.Dept = this.Dept.Clone();
			obj.DiagType = this.DiagType.Clone();
			return obj;
		}
	}
}
