using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// Tend <br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-11'
	///		�޸�Ŀ��='�汾����'
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Tend : NeuObject 
	{
		public Tend()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// �Ƿ���֢
		/// </summary>
		private bool isIntensive;

		/// <summary>
		/// �Ƿ�Σ
		/// </summary>
		private bool isCritical;

		#endregion

		#region ����

		/// <summary>
		/// �Ƿ���֢
		/// </summary>
		public bool IsIntensive
		{
			get
			{
				return this.isIntensive;
			}
			set
			{
				this.isIntensive = value;
			}
		}

		/// <summary>
		/// �Ƿ�Σ
		/// </summary>
		public bool IsCritical
		{
			get
			{
				return this.isCritical;
			}
			set
			{
				this.isCritical = value;
			}
		}

		#endregion

		#region ����

		/// <summary>
		/// ��֢
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪIsIntensive",true)]
		public bool ifIntensive;

		/// <summary>
		/// �Ƿ�Σ
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪIsCritical", true)]
		public bool ifCritical;

		#endregion
		
		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Tend Clone()
		{
			return base.Clone() as Tend;
		}

		#endregion
	}
}
