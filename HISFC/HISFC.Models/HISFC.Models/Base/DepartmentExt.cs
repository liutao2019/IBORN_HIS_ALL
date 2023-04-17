namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// DepartmentExt<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    [System.Obsolete("�Ѿ������ˣ�����ExtendInfo������Щ������!",false)]
	public class DepartmentExt: ComExtInfo 
    {
		/// <summary>
		/// ���캯��
		/// </summary>
		public DepartmentExt() 
        {
		}


		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;
		#endregion

		#region ����

		/// <summary>
		/// ���ұ���
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept 
        {
			get
            { 
                return this.Item;
            }
			set
            { 
                this.Item = value;
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

			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}
		#endregion

		#region ��¡
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>DepartmentExt���ʵ��</returns>
		public new DepartmentExt Clone()
		{
			return this.MemberwiseClone() as DepartmentExt;
		}
		#endregion

		#endregion

		


	}
}
