using System;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// Record<br></br>
	/// [��������: �����־��¼ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Record : FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Record()
		{

		}

		#region ����

		/// <summary>
		/// ��������
		/// </summary>
		private OperEnvironment operEnvironment = new OperEnvironment();
		
		/// <summary>
		/// �ͷ���Դ��־
		/// </summary>
		private bool alreadyDisposed = false;
		
		/// <summary>
		/// ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject oldData = new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject newData = new FS.FrameWork.Models.NeuObject();

        #endregion

		#region ����

		/// <summary>
		/// ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject OldData
		{
			get
			{
				return this.oldData;
			}
			set
			{
				this.oldData = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject NewData
		{
			get
			{
				return this.newData;
			}
			set
			{
				this.newData = value;
			}
		}

		/// <summary>
		/// ��������
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

			if (this.oldData != null)
			{
				this.oldData.Dispose();
				this.oldData = null;
			}
			if (newData != null)
			{
				this.newData.Dispose();
				this.newData = null;
			}

			base.Dispose(isDisposing);
			
			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ�����ʵ���ĸ���</returns>
		public new Record Clone()
		{
			Record record = base.Clone() as Record;

			record.NewData = this.NewData.Clone();
			record.OldData = this.OldData.Clone();
			
			return record;
		}

		#endregion

		#endregion
		
	}
}
