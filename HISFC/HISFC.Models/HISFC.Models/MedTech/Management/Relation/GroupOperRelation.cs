namespace FS.HISFC.Models.MedTech.Management.Relation 
{
    /// <summary>
    /// [��������: ҽ���������Ա�Ĺ�ϵ]<br></br>
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
    /// 
    [System.Serializable]
	public class GroupOperRelation : MedTech.Management.Group 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public GroupOperRelation( ) 
		{

		}

		#region ����

		/// <summary>
		/// ����Ա
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment oper;

		#endregion

		#region ����

		/// <summary>
		/// ����Ա
		/// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper 
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

		#endregion

		#region ����
		#endregion
		
	}
}
