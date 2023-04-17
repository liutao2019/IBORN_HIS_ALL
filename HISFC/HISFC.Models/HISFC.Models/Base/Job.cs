using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base 
{  
	/// <summary>
	/// Job<br></br>
	/// [��������: Jobʵ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Job : NeuObject 
    {
		/// <summary>
		/// ���캯��
		/// </summary>
        public Job() 
        {

		}

		#region ����

		/// <summary>
		/// ���� 0 ǰ̨Ӧ�ó����� 1 ��̨job����
		/// </summary>
        private string type;
		
		/// <summary>
		/// ������Ϣ�������ֲ���
		/// </summary>
        private NeuObject department = new NeuObject();
		
		/// <summary>
		///�ϴ�ִ��ʱ��
		/// </summary>
        private DateTime lastTime;
		
		/// <summary>
		/// �´�ִ��ʱ��
		/// </summary>
        private DateTime nextTime;
		
		/// <summary>
		/// �������(ֻ�е�StateΪ1��ʱ������)
		/// </summary>
        private int intervalDays = 1;
		
		/// <summary>
		///  ״̬0_��ͳ��, 1_ÿ��ͳ��,  2_ÿ��ͳ��, 3_ÿ��ͳ�ƣ�4_ÿ����ͳ��,5_ÿ��ͳ��,7_�Զ���,S_����ͳ��
		/// </summary>
		private JobState state = new JobState();
		
		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ Ĭ��Ϊfalseû���ͷ�
		/// </summary>
		private bool alreadyDisposed = false;

		#endregion

		#region ����

		/// <summary>
		/// ����: 0 ǰ̨Ӧ�ó�����, 1 ��̨job����
		/// </summary>
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				type = value;
			}
		}

		/// <summary>
		/// ������Ϣ�������ֲ���
		/// </summary>
		public NeuObject Department 
		{
			get
			{
				return this.department;
			}
			set
			{
				this.department = value;
			}
		}

		/// <summary>
		/// �ϴ�ִ��ʱ��
		/// </summary>
		public DateTime LastTime
		{
			get
			{
				return this.lastTime;
			}
			set
			{
				this.lastTime = value;
			}
		}

		/// <summary>
		/// �´�ִ��ʱ��
		/// </summary>
		public DateTime NextTime
		{
			get
			{
				return this.nextTime;
			}
			set
			{
				this.nextTime = value;
			}
		}

		/// <summary>
		/// �������(ֻ�е�JOB_STATEΪ1��ʱ������)
		/// </summary>
		public int IntervalDays 
		{
			get
			{
				return this.intervalDays;
			}
			set
			{
				this.intervalDays = value;
			}
		}

		/// <summary>
		/// ״̬0_��ͳ��, 1_ÿ��ͳ��,  2_ÿ��ͳ��,  3_ÿ��ͳ�ƣ�4_ÿ����ͳ��,5_ÿ��ͳ��,7_�Զ���,S_����ͳ��
		/// </summary>
		public FS.HISFC.Models.Base.JobState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}
		
		#endregion

		#region ����

		#region �ͷ���Դ
		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing">�Ƿ��ͷ� true�� false��</param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}
			
			if (this.state != null)
			{
				this.state.Dispose();
			}
			if (this.department != null)
			{
				this.department.Dispose();
			}
			
			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��¡��ĵ�ǰ�����ʵ��</returns>
		public new Job Clone()
		{
			Job job = base.Clone() as Job;
			
			job.State = this.State.Clone();
			job.Department = this.Department.Clone();
			
			return job;
		}
		
		#endregion

		#endregion

		
	}
}
