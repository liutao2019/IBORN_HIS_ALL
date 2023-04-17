using System;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy 
{
	/// <summary>
	/// [��������: ������˾����������ʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	///  �̳���Base.Spell�� 
	///  ID ���� Name ����
	/// </summary>
    [Serializable]
    public class Company: FS.HISFC.Models.Base.Spell,FS.HISFC.Models.Base.IValid
	{
		public Company()
		{

		}


		#region ����

		/// <summary>
		/// ģ������
		/// </summary>
		private FS.HISFC.Models.IMA.EnumModuelType enuType;

		/// <summary>
		/// ��ϵ��ʽ
		/// </summary>
		private FS.HISFC.Models.Pharmacy.Base.Relation relationCollection = new FS.HISFC.Models.Pharmacy.Base.Relation();

		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		private bool isValid = true;

		/// <summary>
		/// Gmp��Ϣ
		/// </summary>
		private System.String gMPInfo ;

		/// <summary>
		/// Gsp��Ϣ
		/// </summary>
		private System.String gSPInfo;

		/// <summary>
		/// ISO��Ϣ
		/// </summary>
		private System.String iSOInfo;

		/// <summary>
		/// ��˾���
		/// </summary>
		private System.String type ;

		/// <summary>
		/// ��������
		/// </summary>
		private System.String openBank ;

		/// <summary>
		/// �����ʺ�
		/// </summary>
		private System.String openAccounts ;

		/// <summary>
		/// ���߿���
		/// </summary>
		private System.Decimal actualRate ;
		
		/// <summary>
		/// ��ע
		/// </summary>
		private System.String remark = "";

		/// <summary>
		/// ��չ�ֶ�1
		/// </summary>
		private System.String extend1 = "";

		/// <summary>
		/// ��չ�ֶ�2
		/// </summary>
		private System.String extend2 = "";		

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();

		#endregion		

		/// <summary>
		/// ��дID
		/// </summary>
		public new string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
				this.relationCollection.ID = value;
			}
		}


		/// <summary>
		/// ��дName
		/// </summary>
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
				this.relationCollection.Name = value;
			}
		}


		/// <summary>
		/// ģ������
		/// </summary>
		public FS.HISFC.Models.IMA.EnumModuelType EnuType
		{
			get
			{
				return this.enuType;
			}
			set
			{
				this.enuType = value;
			}
		}


		/// <summary>
		/// ��ϵ��ʽ
		/// </summary>
		public FS.HISFC.Models.Pharmacy.Base.Relation RelationCollection
		{
			get
			{
				return this.relationCollection;
			}
			set
			{
				this.relationCollection = value;
			}
		}

		
		/// <summary>
		/// GMP��Ϣ
		/// </summary>
		public System.String GMPInfo 
		{
			get
			{ 
				return this.gMPInfo; }
			set
			{
				this.gMPInfo = value; 
			}
		}


		/// <summary>
		/// GSP��Ϣ
		/// </summary>
		public System.String GSPInfo 
		{
			get
			{ 
				return this.gSPInfo; }
			set
			{ 
				this.gSPInfo = value; }
		}


		/// <summary>
		/// ISO��Ϣ
		/// </summary>
		public System.String ISOInfo
		{
			get
			{
				return this.iSOInfo;
			}
			set
			{
				this.iSOInfo = value;
			}
		}


		/// <summary>
		/// ��˾���0���������ң�1��������
		/// </summary>
		public System.String Type 
		{
			get
			{
				return this.type; }
			set
			{
				this.type = value;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		public System.String OpenBank 
		{
			get
			{ 
				return this.openBank; }
			set
			{
				this.openBank = value;
			}
		}


		/// <summary>
		/// �����˺�
		/// </summary>
		public System.String OpenAccounts 
		{
			get
			{ 
				return this.openAccounts; }
			set
			{
				this.openAccounts = value;
			}
		}


		/// <summary>
		/// ���߿���
		/// </summary>
		public System.Decimal ActualRate 
		{
			get
			{
				return this.actualRate; 
			}
			set
			{ 
				this.actualRate = value;
			}
		}
		

		#region IValid ��Ա

		/// <summary>
		/// �Ƿ���Ч
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
		
		/// <summary>
		/// Ԥ���ֶ�1
		/// </summary>
		public string  Extend1
		{
			get
			{
				return this.extend1;
			}
			set
			{
				this.extend1 = value ;
			}
		}


		/// <summary>
		/// Ԥ���ֶ�2
		/// </summary>
		public string  Extend2
		{
			get
			{
				return this.extend2;
			}
			set
			{
				this.extend2 = value ;
			}
		}
		

		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment Oper
		{
			get
			{
				return this.operInfo;
			}
			set
			{
				this.operInfo = value;
			}
		}


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���Ŀ�¡ʵ��</returns>
		public new Company Clone()
		{
			Company company = base.Clone() as Company;

			company.RelationCollection = this.RelationCollection.Clone();
			company.Oper = this.Oper.Clone();

			return company;
		}


		#endregion

		#region ��Ч����

		private System.String myEmail;

		/// <summary>
		/// ��ַ
		/// </summary>
		private System.String address ;

		/// <summary>
		/// ��ϵ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject linkPerson = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��ϵ�绰
		/// </summary>
		private string phone;

		/// <summary>
		/// Email��ַ
		/// </summary>
		private string email;

		/// <summary>
		/// ����
		/// </summary>
		private string faxCode = "";

		/// <summary>
		/// ��˾��ַ
		/// </summary>
		private string linkUrl;

		/// <summary>
		/// ��ʹͨѶ��ϵ����
		/// </summary>
		private string linkNo;

		/// <summary>
		/// ��ϵ��ʽ
		/// </summary>
		private System.String relation ;

		/// <summary>
		/// ��չ�ֶ�1
		/// </summary>
		private System.String specialFlag1 = "";

		/// <summary>
		/// ��չ�ֶ�2
		/// </summary>
		private System.String specialFlag2 = "";	

		/// <summary>
		/// ��Ч��
		/// </summary>
		private string valid;


		/// <summary>
		/// ��ע
		/// </summary>
		[System.Obsolete("�����ع� ʹ��Memo���Դ���",true)]
		public string  Remark
		{
			get
			{
				return this.remark;
			}
			set
			{
				this.remark = value ;
			}
		}


		/// <summary>
		/// EMAIL��ϵ��ʽ
		/// </summary>
		[System.Obsolete("�����ع� ʹ��RelationCollection���Դ���",true)]
		public string  EmailAdd
		{
			get
			{
				return myEmail;
			}
			set
			{
				myEmail = value ;
			}
		}


		[System.Obsolete("�����ع� ʹ��Oper���Դ���",true)]
		public FS.FrameWork.Models.NeuObject OperPerson = new FS.FrameWork.Models.NeuObject();

		[System.Obsolete("�����ع� ʹ��Oper���Դ���",true)]
		public DateTime OperDate ; 

		/// <summary>
		/// ��˾��ַ
		/// </summary>
		[System.Obsolete("�����ع� ʹ��RelationCollection���Դ���",true)]
		public System.String Address 
		{
			get
			{ 
				return this.address;
			}
			set
			{
				this.address = value; 
			}
		}


		/// <summary>
		/// ��ϵ��
		/// </summary>
		[System.Obsolete("�����ع� ʹ��RelationCollection���Դ���",true)]
		public FS.FrameWork.Models.NeuObject LinkPerson
		{
			get
			{
				return this.linkPerson;
			}
			set
			{
				this.linkPerson = value;
			}
		}


		/// <summary>
		/// ��ϵ�绰
		/// </summary>
		[System.Obsolete("�����ع� ʹ��RelationCollection���Դ���",true)]
		public string Phone
		{
			get
			{
				return this.phone;
			}
			set
			{
				this.phone = value;
			}
		}


		/// <summary>
		/// Email��ַ
		/// </summary>
		[System.Obsolete("�����ع� ʹ��RelationCollection���Դ���",true)]
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		[System.Obsolete("�����ع� ʹ��RelationCollection���Դ���",true)]
		public string  FaxCode
		{
			get
			{
				return this.faxCode;
			}
			set
			{
				this.faxCode = value ;
			}
		}


		/// <summary>
		/// ��˾��ַ
		/// </summary>
		[System.Obsolete("�����ع� ʹ��RelationCollection���Դ���",true)]
		public string LinkUrl
		{
			get
			{
				return this.linkUrl;
			}
			set
			{
				this.linkUrl = value;
			}
		}


		/// <summary>
		/// ��ʹͨѶ��ϵ��ʽ
		/// </summary>
		[System.Obsolete("�����ع� ʹ��RelationCollection���Դ���",true)]
		public string LinkNo
		{
			get
			{
				return this.linkNo;
			}
			set
			{
				this.linkNo = value;
			}
		}


		/// <summary>
		/// ��ϵ��ʽ
		/// </summary>
		[System.Obsolete("�����ع� ʹ��RelationCollection���Դ���",true)]
		public System.String Relation 
		{
			get
			{
				return this.relation; 
			}
			set
			{ 
				this.relation = value;
			}
		}


		/// <summary>
		/// Ԥ���ֶ�1
		/// </summary>
		[System.Obsolete("�����ع� ʹ��Extend1���Դ���",true)]
		public string  SpecialFlag1
		{
			get
			{
				return this.specialFlag1;
			}
			set
			{
				this.specialFlag1 = value ;
			}
		}


		/// <summary>
		/// Ԥ���ֶ�2
		/// </summary>
		[System.Obsolete("�����ع� ʹ��Extend2���Դ���",true)]
		public string  SpecialFlag2
		{
			get
			{
				return this.specialFlag2;
			}
			set
			{
				this.specialFlag2 = value ;
			}
		}
		

		/// <summary>
		/// ��Ч��
		/// </summary>
		[System.Obsolete("�����ع� ʹ��IsValid���Դ���",true)]
		public string ValidState
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
			}
		}
		

		#endregion
	}
}