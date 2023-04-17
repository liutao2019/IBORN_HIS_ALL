using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// OperEnvironment<br></br>
	/// [��������: ��������ʵ��:��������Ա�����Һ�ʱ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
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
        private Operator oper;

		/// <summary>
		/// ����
		/// </summary>
        private NeuObject dept;

		#endregion

		#region ����

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperTime
		{
			get
			{
                if (operTime == null)
                {
                    operTime = new DateTime();
                }
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
                if (oper == null)
                {
                    oper = new Operator();
                }
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
                if (dept == null)
                {
                    dept = new NeuObject();
                }
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
            operEnvironment.OperTime = new DateTime(operEnvironment.OperTime.Year, operEnvironment.OperTime.Month, operEnvironment.OperTime.Day, operEnvironment.OperTime.Hour, operEnvironment.OperTime.Minute, operEnvironment.OperTime.Second);

			return operEnvironment;
		}

		#endregion
	}
}
