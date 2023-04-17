using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Terminal
{
	/// <summary>
	/// ItemExtend <br></br>
	/// [��������: ��ҩƷ��Ϣ����չ��Ϣ]<br></br>
	/// [�� �� ��: sunxh]<br></br>
	/// [����ʱ��: 2005-3-3]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class ItemExtend : FS.FrameWork.Models.NeuObject
	{
		public ItemExtend()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ������

		/// <summary>
		/// ����
		/// </summary>
		FS.HISFC.Models.Base.Department dept = new Department();

		/// <summary>
		/// ��λ��ʶ
		/// </summary>
		string unitFlag;

		/// <summary>
		/// ԤԼ�ص�
		/// </summary>
		string bookLocate;

		/// <summary>
		/// ԤԼʱ��
		/// </summary>
		string bookTime = "";

		/// <summary>
		/// ִ�еص�
		/// </summary>
		string executeLocate;

		/// <summary>
		/// ȡ����ʱ��
		/// </summary>
		string reportTime = "";

		/// <summary>
		/// �д��޴�
		/// </summary>
		string hurtFlag;

		/// <summary>
		/// �Ƿ����ԤԼ
		/// </summary>
		string selfBookFlag;

		/// <summary>
		/// ����רҵ
		/// </summary>
		string speciality;

		/// <summary>
		/// �ٴ�����
		/// </summary>
		string clinicMeaning;

		/// <summary>
		/// �걾��
		/// </summary>
		decimal simpleQty;

		/// <summary>
		/// ֪��ͬ����
		/// </summary>
		string reasonableFlag;

		/// <summary>
		/// �걾
		/// </summary>
		string simpleKind;

		/// <summary>
		/// ��������
		/// </summary>
		string simpleWay;

		/// <summary>
		/// �걾��λ
		/// </summary>
		string simpleUnit;

		/// <summary>
		/// ����
		/// </summary>
		string container;

		/// <summary>
		/// ����ֵ��Χ
		/// </summary>
		string scope;

		/// <summary>
		/// �豸����
		/// </summary>
		string machineType = "";

		/// <summary>
		/// ��Ѫ��ʽ
		/// </summary>
		string bloodWay = "";

		/// <summary>
		/// ��չ
		/// </summary>
		string ext1 = "";

		/// <summary>
		/// ��չ
		/// </summary>
		string ext2 = "";

		/// <summary>
		/// ��չ
		/// </summary>
		string ext3 = "";

		#endregion

		#region ����

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
		/// ��λ��ʶ
		/// </summary>
		public string UnitFlag
		{
			get
			{
				return unitFlag;
			}
			set
			{
				unitFlag = value;
			}
		}
		
		/// <summary>
		/// ԤԼ�ص�
		/// </summary>
		public string BookLocate
		{
			get
			{
				return bookLocate;
			}
			set
			{
				bookLocate = value;
			}
		}

		/// <summary>
		/// ԤԼʱ��
		/// </summary>
		public string BookTime
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
		/// ִ�еص�
		/// </summary>
		public string ExecuteLocate
		{
			get
			{
				return executeLocate;
			}
			set
			{
				executeLocate = value;
			}

		}

		/// <summary>
		/// ȡ����ʱ��
		/// </summary>
		public string ReportTime
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
		/// �д��޴�
		/// </summary>
		public string HurtFlag
		{
			get
			{
				return hurtFlag;
			}
			set
			{
				hurtFlag = value;
			}

		}

		/// <summary>
		/// �Ƿ����ԤԼ
		/// </summary>
		public string SelfBookFlag
		{
			get
			{
				return selfBookFlag;
			}
			set
			{
				selfBookFlag = value;
			}

		}

		/// <summary>
		/// ����רҵ
		/// </summary>
		public string Speciality
		{
			get
			{
				return speciality;
			}
			set
			{
				speciality = value;
			}

		}

		/// <summary>
		/// �ٴ�����
		/// </summary>
		public string ClinicMeaning
		{
			get
			{
				return clinicMeaning;
			}
			set
			{
				clinicMeaning = value;
			}

		}

		/// <summary>
		/// �걾��
		/// </summary>
		public decimal SimpleQty
		{
			get
			{
				return simpleQty;
			}
			set
			{
				simpleQty = value;
			}

		}

		/// <summary>
		/// ֪��ͬ����
		/// </summary>
		public string ReasonableFlag
		{
			get
			{
				return reasonableFlag;
			}
			set
			{
				reasonableFlag = value;
			}

		}

		/// <summary>
		/// ����
		/// </summary>
		public string SimpleKind
		{
			get
			{
				return simpleKind;
			}
			set
			{
				simpleKind = value;
			}

		}

		/// <summary>
		/// ��������
		/// </summary>
		public string SimpleWay
		{
			get
			{
				return simpleWay;
			}
			set
			{
				simpleWay = value;
			}

		}

		/// <summary>
		/// �걾��λ
		/// </summary>
		public string SimpleUnit
		{
			get
			{
				return simpleUnit;
			}
			set
			{
				simpleUnit = value;
			}

		}

		/// <summary>
		/// ����
		/// </summary>
		public string Container
		{
			get
			{
				return container;
			}
			set
			{
				container = value;
			}

		}

		/// <summary>
		/// ����ֵ��Χ
		/// </summary>
		public string Scope
		{
			get
			{
				return scope;
			}
			set
			{
				scope = value;
			}

		}
		/// <summary>
		/// �豸����
		/// </summary>
		public string MachineType
		{
			get
			{
				return machineType;
			}
			set
			{
				machineType = value;
			}
		}

		/// <summary>
		/// ��Ѫ��ʽ
		/// </summary>
		public string BloodWay
		{
			get
			{
				return bloodWay;
			}
			set
			{
				bloodWay = value;
			}
		}

		/// <summary>
		/// ��չ
		/// </summary>
		public string Ext1
		{
			get
			{
				return ext1;
			}
			set
			{
				ext1 = value;
			}
		}

		/// <summary>
		/// ��չ
		/// </summary>
		public string Ext2
		{
			get
			{
				return ext2;
			}
			set
			{
				ext2 = value;
			}
		}

		/// <summary>
		/// ��չ
		/// </summary>
		public string Ext3
		{
			get
			{
				return ext3;
			}
			set
			{
				ext3 = value;
			}
		}

		#endregion

		#region ��ʱ

		/// <summary>
		/// ���Ҵ���
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪDept", true)]
		public string DeptCode
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
		/// ԤԼʱ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪBookTime", true)]
		public string BookDate
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
		/// ȡ����ʱ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪReportTime", true)]
		public string ReportDate
		{
			get
			{
				return this.reportTime;
			}
			set
			{
				reportTime = value;
			}

		}
		
		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new ItemExtend Clone()
		{
			ItemExtend itemExtend = base.Clone() as ItemExtend;

			itemExtend.Dept = this.Dept.Clone();
			
			return itemExtend;
		}

		#endregion
	}
}


