using System;


public class RolePowerDetail: neusoft.neuFC.Object.neuObject
{
	private System.String pkID ;
	private System.String roleCode ;
	private System.String class1Code ;
	private System.String class1Name ;
	private System.String class2Code ;
	private System.String class2Name ;
	private System.String class3Code ;
	private System.String class3Name ;
	private System.String mark ;

	/// <summary>
	/// ���������
	/// </summary>
	public System.String PkID
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
	/// ��ɫ����
	/// </summary>
	public System.String RoleCode
	{
		get
		{
			return this.roleCode;
		}
		set
		{
			this.roleCode = value;
		}
	}


	/// <summary>
	/// һ��Ȩ�޷�����
	/// </summary>
	public System.String Class1Code
	{
		get
		{
			return this.class1Code;
		}
		set
		{
			this.class1Code = value;
		}
	}

	/// <summary>
	/// һ��Ȩ�޷�������
	/// </summary>
	public System.String Class1Name
	{
		get
		{
			return this.class1Name;
		}
		set
		{
			this.class1Name = value;
		}
	}

	/// <summary>
	/// ����Ȩ�޷�����
	/// </summary>
	public System.String Class2Code
	{
		get
		{
			return this.class2Code;
		}
		set
		{
			this.class2Code = value;
		}
	}

	/// <summary>
	/// ����Ȩ�޷�������
	/// </summary>
	public System.String Class2Name
	{
		get
		{
			return this.class2Name;
		}
		set
		{
			this.class2Name = value;
		}
	}

	/// <summary>
	/// ����Ȩ�޷�����
	/// </summary>
	public System.String Class3Code
	{
		get
		{
			return this.class3Code;
		}
		set
		{
			this.class3Code = value;
		}
	}

	/// <summary>
	/// ����Ȩ�޷�������
	/// </summary>
	public System.String Class3Name
	{
		get
		{
			return this.class3Name;
		}
		set
		{
			this.class3Name = value;
		}
	}

	/// <summary>
	/// ��ע
	/// </summary>
	public System.String Mark
	{
		get
		{
			return this.mark;
		}
		set
		{
			this.mark = value;
		}
	}

}