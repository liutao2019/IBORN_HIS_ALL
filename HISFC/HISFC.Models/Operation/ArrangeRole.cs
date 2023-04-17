using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Operation 
{
	/// <summary>
	/// [��������: ����������Ա������Ա��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-10-02]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class ArrangeRole : FS.HISFC.Models.Base.Employee
	{
		//��ɫ����
		private OperationRoleEnumService roleType = new OperationRoleEnumService(); 
		/// <summary>
		/// ��ɫ״̬(Ŀǰֻ�������������)
		/// </summary>
        private RoleOperKindEnumService roleOperKind = new RoleOperKindEnumService();

		public ArrangeRole()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        public ArrangeRole(FS.FrameWork.Models.NeuObject neuObject)
        {
            this.ID = neuObject.ID;
            this.Name = neuObject.Name;
            this.Memo = neuObject.Memo;
        }

		//�������뵥���
		public string OperationNo = string.Empty;

		/// <summary>
		/// 0��ǰ����1�����¼ ��־
		/// </summary>
		public string ForeFlag = "0";
        //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
        private DateTime supersedeDATE;
        
        /// <summary>
        /// ��ɫ����
        /// </summary>
		public OperationRoleEnumService RoleType 
		{
			get
			{ 
				return roleType; 
			}
			set
			{ 
				roleType = value; 
			}
		}

        /// <summary>
        /// ��ɫ״̬(Ŀǰֻ�������������)
        /// </summary>
		public RoleOperKindEnumService RoleOperKind 
		{
			get
			{
				return this.roleOperKind;
			}
			set
			{
				this.roleOperKind = value;
			}
		}
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime SupersedeDATE
        {
            get
            {
                return this.supersedeDATE;
            }
            set
            {
                this.supersedeDATE = value;
            }
        }
	}
}
