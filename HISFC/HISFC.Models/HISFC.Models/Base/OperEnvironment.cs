using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// OperEnvironment<br></br>
	/// [��������: ��������ʵ��:��������Ա�����Һ�ʱ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-08-31]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class OperEnvironment :NeuObject
	{
		public OperEnvironment()
		{
			
		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ����ʱ��
		/// </summary>
		private DateTime operTime = new DateTime();
		
		/// <summary>
		/// ����Ա
		/// </summary>
		private Operator oper = new Operator();

		/// <summary>
		/// ����
		/// </summary>
		private NeuObject dept = new NeuObject();

		#endregion

		#region ����

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperTime
		{
			get
			{
				return this.operTime;
			}
			set
			{
				this.operTime = value;
			}
		}

		/// <summary>
		/// ����Ա
		/// </summary>
		[Obsolete("�ñ����ID,Name������",true)]
		public Operator Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public NeuObject Dept
		{
			get
			{
				return this.dept;
			}
			set
			{
				this.dept = value;
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

			if (this.dept != null)
			{
				this.dept.Dispose();
				this.dept = null;
			}
			if (this.oper != null)
			{
				this.oper.Dispose();
				this.oper = null;
			}

			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new OperEnvironment Clone()
		{
			OperEnvironment operEnvironment = base.Clone() as OperEnvironment;

			//operEnvironment.Oper = this.Oper.Clone();
			operEnvironment.Dept = this.Dept.Clone();

			return operEnvironment;
		}

		#endregion
	}
}
