namespace FS.HISFC.Models.MedTech.Management.Relation 
{

    /// <summary>
    /// [��������: �豸�����Ա�Ĺ�ϵ]<br></br>
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
	public class MachineOperRelation : FS.HISFC.Models.Base.Spell
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public MachineOperRelation( ) 
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
		
	}
}
