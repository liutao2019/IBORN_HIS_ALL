using FS.FrameWork.Models;
using System;

namespace FS.HISFC.Models.Base
{


	/// <summary>
	/// Pact<br></br>
	/// [��������: ��ͬ��λʵ�壬���ڳ���ά��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Pact : Spell,  ISort, IValid
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Pact( ) 
		{
			
		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FS.FrameWork.Models.NeuObject group = new NeuObject();

		/// <summary>
		/// ��Ч�Ա�ʶ
		/// </summary>
		private bool isValid;
		
		/// <summary>
		/// ��������
		/// </summary>
		private OperEnvironment operEnvironment = new OperEnvironment();

		/// <summary>
		/// �������
		/// </summary>
		private int sortID;
		
		private string validState;
		#endregion
		
		#region ����
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FS.FrameWork.Models.NeuObject Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>
		/// ��Ч�Ա�ʶ
		/// </summary>
		public string ValidState
		{
			get
			{
				return this.validState;
			}
			set
			{
				this.validState = value;
			}
		}

		/// <summary>
		/// ����Ա
		/// </summary>
		public OperEnvironment OperEnvironment
		{
			get
			{
				return this.operEnvironment;
			}
			set
			{
				this.operEnvironment = value;
			}
		}
		#endregion

		#region ����
		
		#region �ͷ���Դ
		
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

			if (this.group != null)
			{
				this.group.Dispose();
				this.group = null;
			}
			if (this.operEnvironment != null)
			{
				this.operEnvironment.Dispose();
				this.operEnvironment = null;
			}

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Pact Clone()
		{
			Pact pact = base.Clone() as Pact;

			pact.Group = this.Group.Clone();
			pact.OperEnvironment = this.OperEnvironment.Clone();

			return pact;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region ISort ��Ա

		/// <summary>
		/// �������
		/// </summary>
		public int SortID
		{
			get
			{
				return this.sortID;
			}
			set
			{
				this.sortID = value;
			}
		}

		#endregion

		#region IValid ��Ա

		/// <summary>
		/// ��Ч��
		/// </summary>
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		#endregion

		#endregion
	}
}
