using System;

namespace neusoft.HISFC.Object.Base
{
	/// <summary>
	/// DepartmentStatInfo ��ժҪ˵����
	/// </summary>
	/// 

	 



	public class DepartmentStatInfo: neusoft.neuFC.Object.neuObject
	{
		private System.String _pkID ;
		private System.String _statCode ;
		private System.String _pardepCode ;
		private System.String _pardepName ;
		private System.String _deptCode ;
		private System.String _deptName ;
		private System.String _spellCode ;
		private System.String _wbCode ;
		private System.Int32 _nodeKind ;
		private System.Int32 _gradeCode ;
		private System.Int32 _sortId ;
		private System.String _validState ;

		/// <summary>
		/// ������
		/// </summary>
		public System.String PkID
		{
			get
			{
				return this._pkID;
			}
			set
			{
				this._pkID = value;
			}
		}

		/// <summary>
		/// ͳ�Ʒ��� 0000 ��֯���� 0001 ���Һ��� 0002 ����ͳ�� 0003 ���Ҳ�����ϵ
		/// </summary>
		public System.String StatCode
		{
			get
			{
				return this._statCode;
			}
			set
			{
				this._statCode = value;
			}
		}

		/// <summary>
		/// ���ű��루�������룩
		/// </summary>
		public System.String PardepCode
		{
			get
			{
				return this._pardepCode;
			}
			set
			{
				this._pardepCode = value;
			}
		}

		/// <summary>
		/// �������ƣ���������ƣ�
		/// </summary>
		public System.String PardepName
		{
			get
			{
				return this._pardepName;
			}
			set
			{
				this._pardepName = value;
			}
		}

		/// <summary>
		/// ���ұ���
		/// </summary>
		public System.String DeptCode
		{
			get
			{
				return this._deptCode;
			}
			set
			{
				this._deptCode = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.String DeptName
		{
			get
			{
				return this._deptName;
			}
			set
			{
				this._deptName = value;
			}
		}

		/// <summary>
		/// ƴ����
		/// </summary>
		public System.String SpellCode
		{
			get
			{
				return this._spellCode;
			}
			set
			{
				this._spellCode = value;
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		public System.String WbCode
		{
			get
			{
				return this._wbCode;
			}
			set
			{
				this._wbCode = value;
			}
		}

		/// <summary>
		/// �ڵ����ͣ�1�ռ����ң�0���ҷ���
		/// </summary>
		public System.Int32 NodeKind
		{
			get
			{
				return this._nodeKind;
			}
			set
			{
				this._nodeKind = value;
			}
		}

		/// <summary>
		/// ��ǰ����ָ�ڵ�Ĳ���
		/// </summary>
		public System.Int32 GradeCode
		{
			get
			{
				return this._gradeCode;
			}
			set
			{
				this._gradeCode = value;
			}
		}

		/// <summary>
		/// ˳���
		/// </summary>
		public System.Int32 SortId
		{
			get
			{
				return this._sortId;
			}
			set
			{
				this._sortId = value;
			}
		}

		/// <summary>
		/// ��Ч�Ա�־ 0 ���� 1 ͣ�� 2 ����
		/// </summary>
		public System.String ValidState
		{
			get
			{
				return this._validState;
			}
			set
			{
				this._validState = value;
			}
		}

	}
}
