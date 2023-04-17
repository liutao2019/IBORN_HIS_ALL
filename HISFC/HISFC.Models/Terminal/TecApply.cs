using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

/// <summary>
/// TecApply <br></br>
/// [��������: ��֪����ԭ��û��ע��]<br></br>
/// [�� �� ��: ��֪��]<br></br>
/// [����ʱ��: ��֪��]<br></br>
/// <�޸ļ�¼
///		�޸���=''
///		�޸�ʱ��=''
///		�޸�Ŀ��=''
///		�޸�����=''
///  />
/// </summary>
[Serializable]
public class TecApply : FS.FrameWork.Models.NeuObject
{
	public TecApply()
	{
		// TODO: �ڴ˴���ӹ��캯���߼�
	}

	#region ����

	/// <summary>
	/// ������
	/// </summary>
	private System.String clinicCode;
	
	/// <summary>
	/// ��������
	/// </summary>
	private System.String transType;
	
	/// <summary>
	/// ���￨��
	/// </summary>
	private System.String cardNo;
	
	/// <summary>
	/// ����
	/// </summary>
	private System.String myName;
	
	/// <summary>
	/// ����
	/// </summary>
	private System.Int32 age;
	
	/// <summary>
	/// ��Ŀ
	/// </summary>
	private FS.FrameWork.Models.NeuObject item = new NeuObject();
	
	/// <summary>
	/// ��Ŀ����
	/// </summary>
	private System.Int32 myItemQty;
	
	/// <summary>
	/// ��λ��ʶ
	/// </summary>
	private System.String myUnitFlag;
	
	/// <summary>
	/// ������
	/// </summary>
	private System.String recipeNo;
	
	/// <summary>
	/// SequenceNo
	/// </summary>
	private System.Int32 sequenceNo;

	/// <summary>
	/// ��������
	/// </summary>
	private FS.HISFC.Models.Base.Department recipeDept = new Department();
	
	/// <summary>
	/// ����
	/// </summary>
	private FS.HISFC.Models.Base.Department dept = new Department();
	
	/// <summary>
	/// ״̬��0 ԤԤԼ 1 ��Ч 2 ���
	/// </summary>
	private System.String status;
	
	/// <summary>
	/// ԤԼ����
	/// </summary>
	private System.String bookId;
	
	/// <summary>
	/// ԤԼʱ��
	/// </summary>
	private System.DateTime bookTime;
	
	/// <summary>
	/// ������
	/// </summary>
	private System.String noonCode;
	
	/// <summary>
	/// ֪��ͬ����
	/// </summary>
	private System.String reasonableFlag;
	
	/// <summary>
	/// ����״̬
	/// </summary>
	private System.String healthStatus;
	
	/// <summary>
	/// ִ�еص�
	/// </summary>
	private System.String executeLocate;
	
	/// <summary>
	/// ȡ����ʱ��
	/// </summary>
	private System.String reportTime;
	
	/// <summary>
	/// �д�/�޴�
	/// </summary>
	private System.String hurtFlag;
	
	/// <summary>
	/// �걾��λ
	/// </summary>
	private System.String sampleKind;
	
	/// <summary>
	/// ��������
	/// </summary>
	private System.String sampleWay;
	
	/// <summary>
	/// ע������
	/// </summary>
	private System.String remark;
	
	/// <summary>
	/// ˳���
	/// </summary>
	private System.Int32 sortID;
	
	/// <summary>
	/// ��������
	/// </summary>
	private FS.HISFC.Models.Base.OperEnvironment operEnvironment = new OperEnvironment();

	#endregion

	#region ����

	/// <summary>
	/// ������
	/// </summary>
	public System.String ClinicCode
	{
		get
		{
			return this.clinicCode;
		}
		set
		{
			this.clinicCode = value;
		}
	}

	/// <summary>
	/// ��������
	/// </summary>
	public System.String TransType
	{
		get
		{
			return this.transType;
		}
		set
		{
			this.transType = value;
		}
	}

	/// <summary>
	/// ���￨��
	/// </summary>
	public System.String CardNo
	{
		get
		{
			return this.cardNo;
		}
		set
		{
			this.cardNo = value;
		}
	}

	/// <summary>
	/// ����
	/// </summary>
	public System.String MyName
	{
		get
		{
			return this.myName;
		}
		set
		{
			this.myName = value;
		}
	}

	/// <summary>
	/// ����
	/// </summary>
	public System.Int32 Age
	{
		get
		{
			return this.age;
		}
		set
		{
			this.age = value;
		}
	}

	/// <summary>
	/// ��Ŀ
	/// </summary>
	public FS.FrameWork.Models.NeuObject Item
	{
		get
		{
			return this.item;
		}
		set
		{
			this.item = value;
		}
	}

	/// <summary>
	/// ��Ŀ����
	/// </summary>
	public System.Int32 ItemQty
	{
		get
		{
			return this.myItemQty;
		}
		set
		{
			this.myItemQty = value;
		}
	}

	/// <summary>
	/// ��λ��ʶ
	/// </summary>
	public System.String UnitFlag
	{
		get
		{
			return this.myUnitFlag;
		}
		set
		{
			this.myUnitFlag = value;
		}
	}

	/// <summary>
	/// ������
	/// </summary>
	public System.String RecipeNo
	{
		get
		{
			return this.recipeNo;
		}
		set
		{
			this.recipeNo = value;
		}
	}

	/// <summary>
	/// ��������Ŀ���
	/// </summary>
	public System.Int32 SequenceNo
	{
		get
		{
			return this.sequenceNo;
		}
		set
		{
			this.sequenceNo = value;
		}
	}
	
	/// <summary>
	/// ��������
	/// </summary>
	public FS.HISFC.Models.Base.Department RecipeDept
	{
		get
		{
			return this.recipeDept;
		}
		set
		{
			this.recipeDept = value;
		}
	}

	/// <summary>
	/// ����
	/// </summary>
	public FS.HISFC.Models.Base.Department Dept
	{
		get
		{
			return this.dept;
		}
		set
		{
			this.dept = value;
		}
	}

	/// <summary>
	/// ״̬��0 ԤԤԼ 1 ��Ч 2 ���
	/// </summary>
	public System.String Status
	{
		get
		{
			return this.status;
		}
		set
		{
			this.status = value;
		}
	}

	/// <summary>
	/// ԤԼ����
	/// </summary>
	public System.String BookId
	{
		get
		{
			return this.bookId;
		}
		set
		{
			this.bookId = value;
		}
	}

	/// <summary>
	/// ԤԼʱ��
	/// </summary>
	public System.DateTime BookTime
	{
		get
		{
			return this.bookTime;
		}
		set
		{
			this.bookTime = value;
		}
	}

	/// <summary>
	/// ������
	/// </summary>
	public System.String NoonCode
	{
		get
		{
			return this.noonCode;
		}
		set
		{
			this.noonCode = value;
		}
	}

	/// <summary>
	/// ֪��ͬ����
	/// </summary>
	public System.String ReasonableFlag
	{
		get
		{
			return this.reasonableFlag;
		}
		set
		{
			this.reasonableFlag = value;
		}
	}

	/// <summary>
	/// ����״̬
	/// </summary>
	public System.String HealthStatus
	{
		get
		{
			return this.healthStatus;
		}
		set
		{
			this.healthStatus = value;
		}
	}

	/// <summary>
	/// ִ�еص�
	/// </summary>
	public System.String ExecuteLocate
	{
		get
		{
			return this.executeLocate;
		}
		set
		{
			this.executeLocate = value;
		}
	}

	/// <summary>
	/// ȡ����ʱ��
	/// </summary>
	public System.String ReportTime
	{
		get
		{
			return this.reportTime;
		}
		set
		{
			this.reportTime = value;
		}
	}

	/// <summary>
	/// �д�/�޴�
	/// </summary>
	public System.String HurtFlag
	{
		get
		{
			return this.hurtFlag;
		}
		set
		{
			this.hurtFlag = value;
		}
	}

	/// <summary>
	/// �걾��λ
	/// </summary>
	public System.String SampleKind
	{
		get
		{
			return this.sampleKind;
		}
		set
		{
			this.sampleKind = value;
		}
	}

	/// <summary>
	/// ��������
	/// </summary>
	public System.String SampleWay
	{
		get
		{
			return this.sampleWay;
		}
		set
		{
			this.sampleWay = value;
		}
	}

	/// <summary>
	/// ע������
	/// </summary>
	public System.String Remark
	{
		get
		{
			return this.remark;
		}
		set
		{
			this.remark = value;
		}
	}

	/// <summary>
	/// ˳���
	/// </summary>
	public System.Int32 SortID
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
	/// ��������
	/// </summary>
	public FS.HISFC.Models.Base.OperEnvironment OperEnvironment
	{
		get
		{
			return this.operEnvironment;
		}
		set
		{
			this.operEnvironment = value;
		}
	}

	#endregion

	#region ��ʱ

	/// <summary>
	/// ��Ŀ����
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪItem", true)]
	private System.String myItemCode;

	/// <summary>
	/// ��Ŀ����
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪItem", true)]
	private System.String myItemName;

	/// <summary>
	/// ������������
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪRecipeDept", true)]
	private System.String recipeDeptname;

	/// <summary>
	/// ���ұ��
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪDept", true)]
	private System.String myDeptCode;

	/// <summary>
	/// ��������
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪDept", true)]
	private System.String myDeptName;

	/// <summary>
	/// ����Ա����
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪOperEnvironment")]
	private System.String myOperCode;

	/// <summary>
	/// �������ұ���
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪOperEnvironment")]
	private System.String myOperDeptcode;

	/// <summary>
	/// ����ʱ��
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪOperEnvironment")]
	private System.DateTime myOperDate;

	/// <summary>
	/// ��Ŀ����
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪItem", true)]
	public System.String ItemCode
	{
		get
		{
			return this.item.ID;
		}
		set
		{
			this.item.ID = value;
		}
	}

	/// <summary>
	/// ��Ŀ����
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪItem", true)]
	public System.String ItemName
	{
		get
		{
			return this.item.Name;
		}
		set
		{
			this.item.Name = value;
		}
	}

	/// <summary>
	/// ������������
	/// </summary>
	[Obsolete("�Ѿ���ʱ�� ����ΪRecipeDept", true)]
	public System.String RecipeDeptname
	{
		get
		{
			return this.recipeDept.Name;
		}
		set
		{
			this.recipeDept.Name = value;
		}
	}

	/// <summary>
	/// ���Һ�
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪDept", true)]
	public System.String DeptCode
	{
		get
		{
			return this.dept.ID;
		}
		set
		{
			this.dept.ID = value;
		}
	}

	/// <summary>
	/// ��������
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪDept", true)]
	public System.String DeptName
	{
		get
		{
			return this.dept.Name;
		}
		set
		{
			this.dept.Name = value;
		}
	}

	/// <summary>
	/// ����Ա
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪOperEnvironment")]
	public System.String OperCode
	{
		get
		{
			return this.operEnvironment.ID;
		}
		set
		{
			this.operEnvironment.ID = value;
		}
	}

	/// <summary>
	/// ��������
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪOperEnvironment")]
	public System.String OperDeptcode
	{
		get
		{
			return this.dept.ID;
		}
		set
		{
			this.dept.ID = value;
		}
	}

	/// <summary>
	/// ��������
	/// </summary>
	[Obsolete("�Ѿ���ʱ������ΪOperEnvironment")]
	public System.DateTime OperDate
	{
		get
		{
			return this.operEnvironment.OperTime;
		}
		set
		{
			this.operEnvironment.OperTime = value;
		}
	}
	
	#endregion

	#region ����

	/// <summary>
	/// ��¡
	/// </summary>
	/// <returns>TecApply</returns>
	public new TecApply Clone()
	{
		TecApply tecApply = base.Clone() as TecApply;

		tecApply.Item = this.Item.Clone();
		tecApply.RecipeDept = this.RecipeDept.Clone();
		tecApply.Dept = this.Dept.Clone();
		tecApply.OperEnvironment = this.OperEnvironment.Clone();

		return tecApply;
	}
	
	#endregion

}