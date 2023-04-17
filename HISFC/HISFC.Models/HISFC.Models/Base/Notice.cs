using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// Notice<br></br>
	/// [��������: ������Ϣʵ��]<br></br>
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
    public class Notice : NeuObject
	{
		public Notice()
		{
			this.dept.ID = "AAAA";
			this.group.ID = "AAAA";
		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject dept = new NeuObject();

		/// <summary>
		/// ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject group = new NeuObject();

		/// <summary>
		/// ������Ϣ����
		/// </summary>
		private string noticeTitle = "";

		/// <summary>
		/// ������Ϣ����
		/// </summary>
		private string noticeInfo = "";

		/// <summary>
		/// ��������
		/// </summary>
		private DateTime noticeDate;

		/// <summary>
		/// ��������
		/// </summary>
		private FS.FrameWork.Models.NeuObject noticeDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��չ��־
		/// </summary>
		private string extFlag = "";

		/// <summary>
		/// ��������
		/// </summary>
		FS.HISFC.Models.Base.OperEnvironment operEnvironment = new OperEnvironment();

		#endregion

		#region ����


		/// <summary>
		/// ���ұ���
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept {
			get
			{
				return this.dept;
			}
			set
			{
				this.dept = value;
			}
		}

		/// <summary>
		/// ������
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
		/// ������Ϣ����
		/// </summary>
		public string NoticeTitle
		{
			get
			{
				return this.noticeTitle;
			}
			set
			{
				this.noticeTitle = value;
			}
		}

		/// <summary>
		/// ������Ϣ����
		/// </summary>
		public string NoticeInfo
		{
			get
			{
				return this.noticeInfo;
			}
			set
			{
				this.noticeInfo = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public DateTime NoticeDate
		{
			get
			{
				return this.noticeDate;
			}
			set
			{
				this.noticeDate = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public FS.FrameWork.Models.NeuObject NoticeDept
		{
			get
			{
				return this.noticeDept;
			}
			set
			{
				if (value == null)
					value = new FS.FrameWork.Models.NeuObject();
				this.noticeDept = value;
			}
		}

		/// <summary>
		/// ��չ��־
		/// </summary>
		public string ExtFlag 
		{
			get
			{
				return this.extFlag;
			}
			set
			{
				this.extFlag = value;
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
				this.dept.Dispose();;
				this.dept = null;
			}
			if (this.group != null)
			{
				this.group.Dispose();
				this.group = null;
			}
			if (this.noticeDept != null)
			{
				this.noticeDept.Dispose();
				this.noticeDept = null;
			}

			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>Notice</returns>
		public new Notice Clone()
		{
			Notice notice = base.Clone() as Notice;

			notice.Dept = this.Dept.Clone();
			notice.Group = this.Group.Clone();
			notice.NoticeDept = this.NoticeDept.Clone();
			notice.OperEnvironment = this.OperEnvironment.Clone();
			
			return notice;
		}

		#endregion

	}
}
