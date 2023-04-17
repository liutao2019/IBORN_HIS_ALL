using System;

namespace neusoft.HISFC.Object.MedTech
{
	/// <summary>
	/// ItemExtend ��ժҪ˵��
	/// ��ҩƷ��Ϣ����չ��Ϣ
	/// by sunxh
	/// 2005-3-1
	/// 
	/// ��Ŀ��     ��Ŀ ��ҩƷ��չ
	/// DeptCode		���Ҵ���  
	/// UnitFlag		��λ��ʶ
	/// BookLocate		ԤԼ�ص�
	/// BookDate		ԤԼʱ��
	/// ExecuteLocate   ִ�еص�
	/// ReportDate		ȡ����ʱ��
	/// HurtFlag		�д��޴�
	/// SelfBookFlag	�Ƿ����ԤԼ                     
	/// Speciality      ����רҵ
	/// ClinicMeaning   �ٴ�����
	/// SimpleQty		�걾��
	/// SimpleKind		�걾
	/// ReasonableFlag  ֪��ͬ����
	/// SimpleWay		��������
	/// SimpleUnit	    �걾��λ
	/// Container		����
	/// Scope			����ֵ��Χ
	/// machineType     �豸����
	/// </summary>
	public class ItemExtend :neusoft.neuFC.Object.neuObject
	{
		public ItemExtend()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ������

		string deptCode;      //���Ҵ���
		string unitFlag;	  //��λ��ʶ
		string bookLocate;	  //ԤԼ�ص�
		string bookDate;	  //ԤԼʱ��
		string executeLocate; //ִ�еص�
		string reportDate;    //ȡ����ʱ��
		string hurtFlag;      //�д��޴�
		string selfBookFlag;  //�Ƿ����ԤԼ
		string speciality;    //����רҵ
		string clinicMeaning; //�ٴ�����
		decimal simpleQty;     //�걾��
		string reasonableFlag;//֪��ͬ����
		string simpleKind;	  //�걾
		string simpleWay;     //��������
		string simpleUnit;    //�걾��λ
		string container;     //����
		string scope;         //����ֵ��Χ
		string machineType = "";//�豸����
//		string executeWay = "";//ִ�з�ʽ
		string bloodWay = "";//��Ѫ��ʽ
		string ext1 = "";//��չ1
		string ext2 = "";//��չ2
		string ext3 = "";//��չ3

		#endregion
		/// <summary>
		/// ���Ҵ���
		/// </summary>
		public string DeptCode
		{
			get 
			{
				return deptCode;
			}
			set
			{
				deptCode = value;
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
		public string BookDate
		{
			get 
			{
				return bookDate;
			}
			set
			{
				bookDate = value;
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
		public string ReportDate
		{
			get 
			{
				return reportDate;
			}
			set
			{
				reportDate = value;
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
		
		public new ItemExtend Clone()
		{
			ItemExtend obj=base.Clone() as ItemExtend;
			return obj;
		}
	}
}


