using System;

namespace neusoft.HISFC.Object.Power
{
	/// <summary>
	/// PowerLevelClass3 ��ժҪ˵����
	/// </summary>
	public class PowerLevelClass3: neusoft.neuFC.Object.neuObject
	{
		private System.String class2Code ;
		private System.String class3Code ;
		private System.String class3Name ;
		private System.String class3MeaningCode ;
		private System.String class3MeaningName ;
		private System.Boolean finFlag ;
		private System.Boolean delFlag ;
		private System.Boolean grantFlag ;
		private System.String class3JoinCode ;
		private System.String joinGroupCode ;
		private System.Int32 joinGroupOrder ;
		private System.Boolean checkFlag ;
		private PowerLevelClass2 powerLevelClass2 = new PowerLevelClass2();

		/// <summary>
		/// ��дID ������Ȩ�ޱ���
		/// </summary>
		public new string ID {
			get { return class3Code;}
			set { class3Code = value;}
		}

		/// <summary>
		/// ��дName ������Ȩ������
		/// </summary>
		public new string Name {
			get { return class3Name;}
			set { class3Name = value;}
		}

		/// <summary>
		/// ����Ȩ��
		/// </summary>
		public PowerLevelClass2 PowerLevelClass2
		{
			get
			{
				return this.powerLevelClass2;
			}
			set
			{
				this.powerLevelClass2 = value;
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
		/// ����Ȩ�޺������
		/// </summary>
		public System.String Class3MeaningCode
		{
			get
			{
				return this.class3MeaningCode;
			}
			set
			{
				this.class3MeaningCode = value;
			}
		}

		/// <summary>
		/// ����Ȩ�޺�������
		/// </summary>
		public System.String Class3MeaningName
		{
			get
			{
				return this.class3MeaningName;
			}
			set
			{
				this.class3MeaningName = value;
			}
		}

		/// <summary>
		/// �����ǣ�1������Һ���0������
		/// </summary>
		public System.Boolean FinFlag
		{
			get
			{
				return this.finFlag;
			}
			set
			{
				this.finFlag = value;
			}
		}

		/// <summary>
		/// 1-����ɾ�ĸü�¼��0-������ɾ�ĸü�¼
		/// </summary>
		public System.Boolean DelFlag
		{
			get
			{
				return this.delFlag;
			}
			set
			{
				this.delFlag = value;
			}
		}

		/// <summary>
		/// �Ƿ��������Ȩ1����0������2ϵͳȨ�ޣ����ܷ��䡢ɾ�ģ�
		/// </summary>
		public System.Boolean GrantFlag
		{
			get
			{
				return this.grantFlag;
			}
			set
			{
				this.grantFlag = value;
			}
		}

		/// <summary>
		/// ����Ȩ�޹��������룬�������һ������Ȩ�޹������򱣴���һ������Ȩ�޷�����
		/// </summary>
		public System.String Class3JoinCode
		{
			get
			{
				return this.class3JoinCode;
			}
			set
			{
				this.class3JoinCode = value;
			}
		}

		/// <summary>
		/// �����������ţ��������������Ȩ�޷�����һ���������ά��Ϊͬһ�����ţ����磺���롢��ˡ�ȷ�����ſ�����Ϊһ����
		/// </summary>
		public System.String JoinGroupCode
		{
			get
			{
				return this.joinGroupCode;
			}
			set
			{
				this.joinGroupCode = value;
			}
		}

		/// <summary>
		/// ��������˳��ţ�ͬһ���������Ⱥ�˳��ţ����磺����Ϊ1�ţ����Ϊ2�ţ�ȷ��Ϊ3��
		/// </summary>
		public System.Int32 JoinGroupOrder
		{
			get
			{
				return this.joinGroupOrder;
			}
			set
			{
				this.joinGroupOrder = value;
			}
		}

		/// <summary>
		/// �Ƿ���Ҫ��ˣ�Ĭ�ϲ���Ҫ����0����Ҫ1��Ҫ
		/// </summary>
		public System.Boolean CheckFlag
		{
			get
			{
				return this.checkFlag;
			}
			set
			{
				this.checkFlag = value;
			}
		}

	}
}
