using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// PowerRole<br></br>
	/// [��������: Ȩ��ʵ��,ID:һ��Ȩ�ޱ���,Name:һ��Ȩ������]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class PowerRole :  NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PowerRole()
		{

		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ��ɫ˵��
		/// </summary>
		private string roleExplain;

		/// <summary>
		/// ����Ȩ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject grade2 = new NeuObject();

		/// <summary>
		/// ����Ȩ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject grade3 = new NeuObject();


		#endregion

		#region ����

		/// <summary>
		/// ��ɫ˵��
		/// </summary>
		public string RoleExplain
		{
			get
			{
				return this.roleExplain;
			}
			set
			{
				this.roleExplain = value;
			}
		}

		/// <summary>
		/// ����Ȩ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Grade2 
		{
			get
			{
				return this.grade2;
			}
			set
			{
				this.grade2 = value;
			}
		}

		/// <summary>
		/// ����Ȩ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject Grade3 
		{
			get
			{
				return this.grade3;
			}
			set
			{
				this.grade3 = value;
			}
		}

		#endregion

		#region ����

		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing"></param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}

			if (this.grade2 != null)
			{
				this.grade2.Dispose();
				this.grade2 = null;
			}
			if (this.grade3 != null)
			{
				this.grade3.Dispose();
				this.grade3 = null;
			}

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>��ǰ�����ʵ���ĸ���</returns>
		public new PowerRole Clone()
		{
			PowerRole powerRole = base.Clone() as PowerRole;

			powerRole.Grade2 = this.Grade2.Clone();
			powerRole.Grade3 = this.Grade3.Clone();

			return powerRole;
		}

		#endregion
        
	}
}
