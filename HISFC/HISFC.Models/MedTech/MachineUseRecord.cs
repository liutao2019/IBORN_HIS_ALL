namespace Neusoft.HISFC.Object.MedTech 
{
    /// <summary>
    /// [��������: �豸ʹ�ü�¼]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2006-12-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
    public class MachineUseRecord : MedTech.Management.Machine 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public MachineUseRecord( ) 
		{
		}

		#region ����

		/// <summary>
		/// ʹ�õ���ʼʱ��
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment startTime;

		/// <summary>
		/// ʹ�ý�ֹʱ��
		/// </summary>
		private Neusoft.HISFC.Object.Base.OperEnvironment endTime;

		/// <summary>
		/// ʹ���ˣ���Ӧ����
		/// </summary>
        private MedTech.Management.Oper oper;

		#endregion

		#region ����

		/// <summary>
		/// ʹ���ˣ���Ӧ����
		/// </summary>
        public MedTech.Management.Oper Oper
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
		/// ʹ�õ���ʼʱ��
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment StartTime 
		{
			get 
			{
				return this.startTime;
			}
			set 
			{
				this.startTime = value;
			}
		}

		/// <summary>
		/// ʹ�ý�ֹʱ��
		/// </summary>
		public Neusoft.HISFC.Object.Base.OperEnvironment EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}
		#endregion
		
	}
}
