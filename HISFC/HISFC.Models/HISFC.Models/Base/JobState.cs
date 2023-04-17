namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// JobState<br></br>
	/// [��������: Job״̬ʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-08-30]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class JobState : FS.FrameWork.Models.NeuObject 
    {
		/// <summary>
		/// Job״̬ʵ��
		/// </summary>
		public JobState() 
        {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ����
		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ Ĭ��Ϊfalseû���ͷ�
		/// </summary>
		private bool alreadyDisposed = false;
		/// <summary>
		/// ��Ժ״̬
		/// </summary>
		public enum enuJobState 
        {
			/// <summary>
			/// None ������ִ��
			/// </summary>
			N =0,
			/// <summary>
			/// Day ����ִ��
			/// </summary>
			D =1,
			/// <summary>
			/// Month ����ִ��
			/// </summary>
			M =2,
			/// <summary>
			/// Year ����ִ��
			/// </summary>
			Y =3,
			/// <summary>
			/// ����ִ��
			/// </summary>
			S =4
		};
		/// <summary>
		/// ����ID
		/// </summary>
		private enuJobState myID;

		#endregion

		#region ����
		/// <summary>
		/// ID
		/// </summary>
		public new System.Object ID 
        {
			get 
            {
				return this.myID;
			}
			set 
            {
				try 
                {
					this.myID=(this.GetIDFromName (value.ToString())); 
				}
				catch 
                {
				}
				base.ID=this.myID.ToString();
				string s=this.Name;
			}
		}
		/// <summary>
		/// ����ID�õ�����
		/// </summary>
		public new string Name 
		{
			get 
			{
				string strJobState;
				switch ((int)this.ID) 
				{
					case 1:
						strJobState="����ִ��";
						break;
					case 2:
						strJobState="����ִ��";
						break;
					case 3:
						strJobState="����ִ��";
						break;
					case 4:
						strJobState="����ִ��";
						break;
					default:
						strJobState= "��ִ��";
						break;
				}
				base.Name= strJobState;
				return	strJobState;
			}
		}
		#endregion

		#region ����
        /// <summary>
        /// �������Ƶõ�ID
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
		public enuJobState GetIDFromName(string Name) 
        {
			enuJobState c=new enuJobState();
			for(int i=0;i<100;i++) 
            {
				c=(enuJobState)i;
				if(c.ToString()==Name) return c;
			}
			return (enuJobState)int.Parse(Name);
		}
		
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(JobState)</returns>
		public System.Collections.ArrayList List() 
		{
			JobState aJobState;
			System.Collections.ArrayList alReturn=new System.Collections.ArrayList();
			int i;
			for(i=0;i<=3;i++) 
			{
				aJobState=new JobState();
				aJobState.ID=(enuJobState)i;
				aJobState.Memo=i.ToString();
				alReturn.Add(aJobState);
			}
			return alReturn;
		}

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
			base.Dispose (isDisposing);
			this.alreadyDisposed = true;
		}

		#endregion
		
		#region ��¡
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
		public new JobState Clone() 
		{
			return this.MemberwiseClone() as JobState;
		}
		#endregion
	}
}
