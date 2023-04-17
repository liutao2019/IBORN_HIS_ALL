using System;
 
using FS.FrameWork.Models;
using System.Collections;
namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// �����
	/// ID ��ˮ�� name ����
	/// 2004-5 
	/// </summary>
	[Obsolete("��HealthRecord��������ʵ��",true)]
    [Serializable]
	public class DiagNose:FS.FrameWork.Models.NeuObject
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
		public FS.HISFC.Models.RADT.Patient Patient=new FS.HISFC.Models.RADT.Patient();
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
