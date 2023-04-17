using System;
using FS.HISFC;
namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.Combo<br></br>
	/// [��������: ҽ�������Ϣʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Combo:FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ID ��Ϻ� Name ����
		/// </summary>
		public Combo()
		{
			// TODO: �ڴ˴���ӹ��캯���߼�
		}
		
		#region ����

		/// <summary>
		/// �Ƿ���ҩ
		/// </summary>
		private bool isMainDrug = false;

		#endregion

		#region ����

		/// <summary>
		/// �Ƿ���ҩ
		/// </summary>
		public bool IsMainDrug 
		{
			get
			{
				return this.isMainDrug;
			}
			set
			{
				this.isMainDrug = value;
			}
		}

		#endregion

		#region ���ϵ�

		/// <summary>
		/// �Ƿ���ҩ
		/// </summary>
		[Obsolete("��IsMainDrug����",false)]
		public bool MainDrug 
		{
			get
			{
				return this.isMainDrug;
			}
			set
			{
				this.isMainDrug = value;
			}
		}

		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new  Combo Clone()
		{
			return base.Clone() as Combo;
		}

		#endregion

		#endregion

	}
}
