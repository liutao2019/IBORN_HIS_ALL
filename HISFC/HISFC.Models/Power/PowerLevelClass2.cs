using System;

namespace neusoft.HISFC.Object.Power
{
	/// <summary>
	/// PowerLevelClass2 ��ժҪ˵����
	/// </summary>
	public class PowerLevelClass2: neusoft.neuFC.Object.neuObject
	{
		private PowerLevelClass1 powerLevelClass1;
		private System.String class1Code = "";
		private System.String class2Code = "";
		private System.String class2Name = "";
		private System.String validState = "0" ;
		private System.String flag;

		//��дID������Ȩ�ޱ���
		public new string ID {
			get {return this.class2Code;}
			set {this.class2Code = value;}
		}

		//��дName������Ȩ������
		public new string Name {
			get {return this.class2Name;}
			set {this.class2Name = value;}
		}


		/// <summary>
		/// һ��Ȩ�޷�����
		/// </summary>
		public PowerLevelClass1 PowerLevelClass1
		{
			get
			{
				return this.powerLevelClass1;
			}
			set
			{
				this.powerLevelClass1 = value;
			}
		}

		/// <summary>
		/// һ��Ȩ�޷����룬Ȩ������
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
		/// ��Ч״̬(0��Ч��1��Ч)
		/// </summary>
		public System.String ValidState {
			get{ return this.validState;}
			set{ this.validState = value;}
		}
		/// <summary>
		/// �����ǣ�1�жϴ���Ȩ��ʱ��ֻҪ����Ȩ�޾�������룬����Ҫ�û�ѡ�����
		/// </summary>
		public System.String Flag {
			get{ return this.flag;}
			set{ this.flag = value;}
		}
	}
}
