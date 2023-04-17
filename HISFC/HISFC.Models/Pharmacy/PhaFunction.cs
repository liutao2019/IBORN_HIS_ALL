using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Pharmacy 
{
	/// <summary>
	/// [��������: ҩ������ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-11]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class PhaFunction :  FS.HISFC.Models.Base.Spell,FS.HISFC.Models.Base.IValid
	{

		public PhaFunction() 
		{
			
		}


		#region ����

		/// <summary>
		/// �����ڵ����
		/// </summary>
		private System.String myParentNode ;

		/// <summary>
		/// �ڵ�����
		/// </summary>
		private System.String myNodeName;

		/// <summary>
		/// �ڵ�����
		/// </summary>
		private System.Int32 myNodeKind ;

		/// <summary>
		/// ��ǰ����
		/// </summary>
		private System.Int32 myGradeLevel;
		
		/// <summary>
		/// ˳���
		/// </summary>
		private System.Int32 mySortId ;

		/// <summary>
		/// ��Ч��
		/// </summary>
		private bool isValid;

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment oper = new OperEnvironment();
		
		#endregion		

		/// <summary>
		/// �����ڵ����
		/// </summary>
		public System.String ParentNode
		{
			get
			{
				return this.myParentNode;
			}
			set
			{
				this.myParentNode = value; 
			}
		}


		/// <summary>
		/// �ڵ����
		/// </summary>
		public System.String NodeID
		{
			get
			{
				return this.myNodeCode; 
			}
			set
			{
				this.myNodeCode = value; 
			}
		}

		
		/// <summary>
		/// �ڵ�����
		/// </summary>
		public System.String NodeName
		{
			get
			{
				return this.myNodeName; 
			}
			set
			{
				this.myNodeName = value; 
			}
		}


		/// <summary>
		/// �ڵ����ͣ�0��Ҷ�ӽڵ㣬1Ҷ�ӽڵ�
		/// </summary>
		public System.Int32 NodeKind
		{
			get
			{
				return this.myNodeKind;
			}
			set
			{
				this.myNodeKind = value;
			}
		}


		/// <summary>
		/// ��ǰ����ָ�ڵ�Ĳ���
		/// </summary>
		public System.Int32 GradeLevel
		{
			get
			{ 
				return this.myGradeLevel;
			}
			set
			{
				this.myGradeLevel = value; 
			}
		}


		/// <summary>
		/// ˳���
		/// </summary>
		public System.Int32 SortID
		{
			get
			{
				return this.mySortId; 
			}
			set
			{
				this.mySortId = value; 
			}
		}


		/// <summary>
		/// ������Ϣ
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

		
		#region IValid ��Ա

		/// <summary>
		/// ��Ч�� 
		/// </summary>
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}


		#endregion

		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new PhaFunction Clone()
		{
			return base.Clone() as PhaFunction;
		}


		#endregion

		#region ��Ч����

		/// <summary>
		/// �ڵ����
		/// </summary>
		private System.String myNodeCode ;

		/// <summary>
		/// ��ǰ����
		/// </summary>
		private System.Int32 myGradeCode ;

		/// <summary>
		/// ����Ա
		/// </summary>
		private System.String myOperCode ;

		/// <summary>
		/// ����ʱ��
		/// </summary>
		private System.DateTime myOperDate ;

		/// <summary>
		/// ��Ч��
		/// </summary>
		private System.String myValidState ;

		/// <summary>
		/// �ڵ����
		/// </summary>
		[System.Obsolete("�����ع� ����ΪNodeID����",true)]
		public System.String NodeCode
		{
			get{ return this.myNodeCode; }
			set{ this.myNodeCode = value; }
		}


		/// <summary>
		/// ��ǰ����ָ�ڵ�Ĳ���
		/// </summary>
		[System.Obsolete("�����ع� ����ΪGradeLevel����",true)]
		public System.Int32 GradeCode
		{
			get{ return this.myGradeCode; }
			set{ this.myGradeCode = value; }
		}


		/// <summary>
		/// ˳���
		/// </summary>
		[System.Obsolete("�����ع� ����ΪSortID����",true)]
		public System.Int32 SortId
		{
			get{ return this.mySortId; }
			set{ this.mySortId = value; }
		}


		/// <summary>
		/// ����Ա
		/// </summary>
		[System.Obsolete("�����ع� ����ΪOper����",true)]
		public System.String OperCode
		{
			get{ return this.myOperCode; }
			set{ this.myOperCode = value; }
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�����ع� ����ΪOper����",true)]
		public System.DateTime OperDate
		{
			get{ return this.myOperDate; }
			set{ this.myOperDate = value; }
		}


		/// <summary>
		/// ��Ч�Ա�־ 0��Ч 1��Ч
		/// </summary>
		[System.Obsolete("�����ع� ����ΪBool���͵�IsValid����",true)]
		public System.String ValidState
		{
			get{ return this.myValidState; }
			set{ this.myValidState = value; }
		}


		#endregion
	}
}
