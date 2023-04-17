using System;

namespace neusoft.HISFC.Object.Power
{
	/// <summary>
	/// PowerLevelClass1 ��ժҪ˵����
	/// </summary>
	public class PowerLevelClass1: neusoft.neuFC.Object.neuObject
	{
		private System.String class1Code ;
		private System.String class1Name ;
		private System.Boolean uniteFlag ;
		private System.Int32 typeProperty ;
		private System.String uniteCode ;
		private System.String vocationType ;
		private System.String vocationName ;
		private System.String validState = "0" ;

		//��дID��һ��Ȩ�ޱ���
		public new string ID {
			get {return this.class1Code;}
			set {this.class1Code = value;}
		}

		//��дName��һ��Ȩ������
		public new string Name {
			get {return this.class1Name;}
			set {this.class1Name = value;}
		}

		/// <summary>
		/// һ��Ȩ�޷����룬Ȩ�����ͣ���Ӧ�ڱ�COM_DEPTSTAT.STAT_CODE
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
		/// �Ƿ�����ͳһά��0��������1������
		/// </summary>
		public System.Boolean UniteFlag
		{
			get
			{
				return this.uniteFlag;
			}
			set
			{
				this.uniteFlag = value;
			}
		}

		/// <summary>
		/// �������ԣ�0�������ӷ��ֻ࣬�����¼������Զ�����ң�1�����ҷ��������Աֻ�������ռ����ң���2�����ڿ��ҷ�������������Ա��3ֻ��ά�����ҹ�ϵ��������������Ա��4������ӿ��Һ���Ա
		/// </summary>
		public System.Int32 TypeProperty
		{
			get
			{
				return this.typeProperty;
			}
			set
			{
				this.typeProperty = value;
			}
		}

		/// <summary>
		/// ͳһά���룺��ͬ�ı���ͳһά����һ��
		/// </summary>
		public System.String UniteCode
		{
			get
			{
				return this.uniteCode;
			}
			set
			{
				this.uniteCode = value;
			}
		}

		/// <summary>
		/// ����ҵ����
		/// </summary>
		public System.String VocationType
		{
			get
			{
				return this.vocationType;
			}
			set
			{
				this.vocationType = value;
			}
		}

		/// <summary>
		/// ����ҵ��������
		/// </summary>
		public System.String VocationName
		{
			get
			{
				return this.vocationName;
			}
			set
			{
				this.vocationName = value;
			}
		}

		/// <summary>
		/// ��Ч״̬(0��Ч��1��Ч)
		/// </summary>
		public System.String ValidState {
			get{ return this.validState;}
			set{ this.validState = value;}
		}
	}
}
