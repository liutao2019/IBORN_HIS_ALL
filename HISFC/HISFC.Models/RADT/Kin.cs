using FS.FrameWork.Models;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// [��������: ������ϵʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ΰ'
	///		�޸�ʱ��='2006-9-12'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
    [System.Serializable]
    public class Kin:NeuObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Kin()
		{
		}

		#region ����

		/// <summary>
		/// ��ϵ
		/// </summary>
        private NeuObject relation;

		/// <summary>
		/// ��ϵ�˵绰 
		/// </summary>
		private string relationPhone;

		/// <summary>
		/// ��ϵ�˵�ַ 
		/// </summary>
		private string relationAddress;

		/// <summary>
		/// ��ϵ�˹�ϵ
		/// </summary>
		private string relationLink ;

        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// <summary>
        /// ��ϵ�˵�ַ���ƺ�
        /// </summary>
        private string relationDoorNo;

		#endregion

		#region ����

		/// <summary>
		/// ��ϵ
		/// </summary>
		public NeuObject  Relation
		{
			get
			{
                if (relation == null)
                {
                    relation = new NeuObject();
                }
				return this.relation;
			}
			set
			{
				this.relation = value;
			}
		}

		/// <summary>
		/// ��ϵ�˵绰 
		/// </summary>
		public string RelationPhone
		{
			get
			{
				return this.relationPhone;
			}
			set
			{
				this.relationPhone = value;
			}
		}

		/// <summary>
		/// ��ϵ�˵�ַ 
		/// </summary>
		public string RelationAddress
		{
			get
			{
				return this.relationAddress;
			}
			set
			{
				this.relationAddress = value;
			}
		}

		/// <summary>
		/// ��ϵ�˹�ϵ
		/// </summary>
		public string RelationLink
		{
			get
			{
				return this.relationLink;
			}
			set
			{
				this.relationLink = value;
			}
		}


        //{9543865B-629A-4353-A45A-99D3FC1136BB}
        /// <summary>
        /// ��ϵ�˵�ַ���ƺ�
        /// </summary>
        public string RelationDoorNo
        {
            get
            {
                if (relationDoorNo == null)
                {
                    relationDoorNo = string.Empty;
                }
                return relationDoorNo;
            }
            set
            {
                relationDoorNo = value;
            }
        }

 

		#endregion

		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Kin Clone()
		{
			Kin kin=base.Clone() as Kin;
			kin.Relation=this.Relation.Clone();
			return kin;
		}
		
		#endregion
	}
}
