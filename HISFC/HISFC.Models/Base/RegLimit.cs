using System;
using neusoft.neuFC.Object;
namespace neusoft.HISFC.Object.Base
{
	/// <summary>
	///�����޶�ʵ��
	/// </summary>
	public class RegLimit:neusoft.neuFC.Object.neuObject 
	{
		public RegLimit()
		{
		}
		/// <summary>
		/// ����
		/// </summary>
		public string week;
		/// <summary>
		/// ���
		/// </summary>
		public neuObject Noon=new neuObject();
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime SeeDate;
		/// <summary>
		/// �������
		/// </summary>
		public neuObject OutDept=new neuObject();
		/// <summary>
		/// �Һż���
		/// </summary>
		public neuObject RegLevel=new neuObject();
		/// <summary>
		/// ԤԼ�޶�
		/// </summary>
		public int Pre_Limit; 
		/// <summary>
		/// �����޶�
		/// </summary>
		public int Fir_Limit;
		/// <summary>
		/// �����޶�
		/// </summary>
		public int Rep_Limit;
		/// <summary>
		/// �����޶�
		/// </summary>
		public int Urg_Limit;
		/// <summary>
		/// ԤԼ�ѹ�
		/// </summary>
		public int Pre_Reged; 
		/// <summary>
		/// �����ѹ�
		/// </summary>
		public int Fir_Reged;
		/// <summary>
		/// �����ѹ�
		/// </summary>
		public int Rep_Reged;
		/// <summary>
		/// �����ѹ�
		/// </summary>
		public int Urg_Reged;
		/// <summary>
		/// ԤԼ����
		/// </summary>
		public bool Pre_full; 
		/// <summary>
		/// �������
		/// </summary>
		public bool Fir_full;
		/// <summary>
		/// �������
		/// </summary>
		public bool Rep_full;
		/// <summary>
		/// �������
		/// </summary>
		public bool Urg_full;
		/// <summary>
		/// ����Һŷ�
		/// </summary>
		public float Fir_Fee;
		/// <summary>
		/// ����Һŷ�
		/// </summary>
		public float Rep_Fee;
		/// <summary>
		/// ����
		/// </summary>
		public float Check_Fee;
		/// <summary>
		/// ����
		/// </summary>
		public float Diag_Fee;
		/// <summary>
		/// ������
		/// </summary>
		public float Other_Fee;
		/// <summary>
		/// ����Һŷ�
		/// </summary>
		public float Urg_Fee;
		/// <summary>
		/// �������
		/// </summary>
		public float Urg_Check_Fee;
		/// <summary>
		/// ��������
		/// </summary>
		public float Urg_Diag_Fee;
		/// <summary>
		/// ����������
		/// </summary>
		public float Urg_Other_Fee;

	}
}
