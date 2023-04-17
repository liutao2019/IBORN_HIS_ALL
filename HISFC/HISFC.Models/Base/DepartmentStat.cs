namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// DepartmentStat<br></br>
	/// [��������: ������֯�ṹʵ��]<br></br>
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
    public class DepartmentStat :Spell,IValidState 
	{

		public DepartmentStat()
        {
        }


		#region ����

	

		
		/// <summary>
		/// ͳ�Ʒ��� 0000 ��֯���� 0001 ���Һ��� 0002 ����ͳ�� 0003 ���Ҳ�����ϵ
		/// </summary>
		private string statCode = ""; 

		/// <summary>
		/// ��������
		/// </summary>		
		private FS.FrameWork.Models.NeuObject superDept = new FS.FrameWork.Models.NeuObject(); 

		/// <summary>
		/// ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject(); 

		/// <summary>
		/// �ڵ�����
		/// </summary>
		private int nodeType ;

		/// <summary>
		/// �������
		/// </summary>
		private int gradeCode ;

		/// <summary>
		/// ������ 
		/// </summary>
		private int sortID ;

		/// <summary>
		/// ��Ч��״̬
		/// </summary>
        private HISFC.Models.Base.EnumValidState validState;

		/// <summary>
		/// ��չ��־
		/// </summary>
		private bool extFlag ;

		/// <summary>
		/// ��չ��־1
		/// </summary>
		private bool extFlag1 ;

		/// <summary>
		/// ��������
		/// </summary>
		private EnumDepartmentType deptType = new EnumDepartmentType();

		private string pkID;

		private string pardepCode;

		private string pardepName;
		#endregion

		#region ����

		/// <summary>
		/// ��дID �����ұ���
		/// </summary>
		public new string ID 
        {
			get 
            { 
                return this.dept.ID;
            }
			set 
            { 
				this.dept.ID = value;
				base.ID = value;
			}
		}

		/// <summary>
		/// ��дName ����������
		/// </summary>
		public new string Name 
        {
			get 
            { 
                return this.dept.Name;
            }
			set 
            { 
				this.dept.Name = value;
				base.Name = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string PkID 
		{
			get
			{
				return this.pkID;
			}
			set
			{
				this.pkID = value;
			}
		}

		/// <summary>
		/// ͳ�Ʒ��� 0000 ��֯���� 0001 ���Һ��� 0002 ����ͳ�� 0003 ���Ҳ�����ϵ
		/// </summary>
		public string StatCode
		{
			get
			{
				return this.statCode;
			}
			set
			{
				this.statCode = value;
			}
		}

		/// <summary>
		/// ���ű��루�������룩
		/// </summary>
		public string PardepCode
		{
			get
			{
				return this.pardepCode;
			}
			set
			{
				this.pardepCode = value;
			}
		}

		/// <summary>
		/// �������ƣ���������ƣ�
		/// </summary>
		public string PardepName
		{
			get
			{
				return this.pardepName;
			}
			set
			{
				this.pardepName = value;
			}
		}

		/// <summary>
		/// ���ұ���
		/// </summary>
		public string DeptCode
		{
			get
			{
				return this.dept.ID;
			}
			set
			{
				this.dept.ID = value;
				base.ID = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string DeptName
		{
			get
			{
				return this.dept.Name;
			}
			set
			{
				this.dept.Name = value;
				base.Name = value;
			}
		}
		/// <summary>
		/// �ڵ����ͣ�1�ռ����ң�0���ҷ���
		/// </summary>
		public int NodeKind
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType = value;
			}
		}

		/// <summary>
		/// ��ǰ����ָ�ڵ�Ĳ���
		/// </summary>
		public int GradeCode
		{
			get
			{
				return this.gradeCode;
			}
			set
			{
				this.gradeCode = value;
			}
		}

		/// <summary>
		/// ˳���
		/// </summary>
		public int SortId
		{
			get
			{
				return this.sortID;
			}
			set
			{
				this.sortID = value;
			}
		}

		/// <summary>
		/// ��Ч�Ա�־ 0 ���� 1 ͣ�� 2 ����
		/// </summary>
        public HISFC.Models.Base.EnumValidState ValidState
		{
			get
			{
				return this.validState;
			}
			set
			{
				this.validState = value;
			}
		}


		/// <summary>
		/// ��չ��־
		/// </summary>
		public bool ExtFlag
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
		/// ��չ��־1
		/// </summary>
		public bool Ext1Flag
		{
			get
			{
				return this.extFlag1;
			}
			set
			{
				this.extFlag1 = value;
			}
		}

        /// <summary>
        /// ���ض���
        /// </summary>
		private System.Collections.IList childs = null;
		public System.Collections.IList Childs
		{
			get
			{
				if(childs == null)
				{
					childs = new System.Collections.ArrayList();
				}

				return this.childs;
			}
			set
			{
				this.childs = value;
			}
		}	
	
		/// <summary>
		/// ��������
		/// </summary>
		public FS.HISFC.Models.Base.EnumDepartmentType DeptType 
        {
			get
            { 
                return deptType;
            }
			set
            { 
                deptType = value; 
            }
		}
        #endregion

		#region ����

	

		#region ��¡
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>DepartmentStat��ʵ��</returns>
		public new DepartmentStat Clone()
		{
			DepartmentStat obj = base.Clone() as DepartmentStat;

			//obj.DeptType = this.DeptType.Clone();

			return obj;            
		}
		#endregion
		#endregion

		
	}
}