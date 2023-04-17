using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// IDept<br></br>
	/// [��������: ʵ�ֻ��߼����������Ŀ�����Ϣ]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    //[System.Serializable]
    public interface IDept
	{
		/// <summary>
		/// ����סԺ���һ�������Һſ���
		/// </summary>
		NeuObject InDept 
		{
			get;
			set;
		}

		/// <summary>
		/// ִ�п���
		/// </summary>
		NeuObject ExeDept
		{
			get;
			set;
		}

		/// <summary>
		/// ��������
		/// </summary>
		NeuObject ReciptDept
		{
			get;
			set;
		}

		/// <summary>
		/// ��ʿվ
		/// </summary>
		NeuObject NurseStation
		{
			get;
			set;
		}

		/// <summary>
		/// �ۿ����
		/// </summary>
		NeuObject StockDept
		{
			get;
			set;
		}

		/// <summary>
		/// ����ҽ�����ڿ���
		/// </summary>
		NeuObject DoctorDept
		{
			get;
			set;
		}
	}
}
