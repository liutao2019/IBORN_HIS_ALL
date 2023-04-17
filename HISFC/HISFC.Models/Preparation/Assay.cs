using System;

namespace FS.HISFC.Models.Preparation
{
	/// <summary>
	/// Assay<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ]<br></br>
	/// [����ʱ��: 2006-09-14]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Assay : PPRBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Assay()
		{
		}


		#region  ����
		/// <summary>
		/// �ͼ�--�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment applyEnv = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ����--�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment assayEnv = new FS.HISFC.Models.Base.OperEnvironment();
		
		/// <summary>
		/// ��������
		/// </summary>
		private Stencil stencil = new Stencil();

		/// <summary>
		/// ���麬��
		/// </summary>
		private decimal content;

		/// <summary>
		/// ��ֵ������
		/// </summary>
		private decimal resultQty;

		/// <summary>
		/// �ַ�������
		/// </summary>
		private string resultStr;

		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		private bool isEligibility;

		/// <summary>
		/// �����׼����
		/// </summary>
		private string assayRule;

		/// <summary>
		/// ������
		/// </summary>
		private string checkOper;

		/// <summary>
		/// ��������
		/// </summary>
		private string reportNum;

		/// <summary>
		/// ������(��װ��Ʒ����)
		/// </summary>
		private decimal divisionQty;


		#endregion

		#region ����
		/// <summary>
		/// �ͼ�--�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment ApplyEnv
		{
			get
			{
				return this.applyEnv;
			}
			set
			{
				this.applyEnv = value;
			}
		}

		/// <summary>
		/// ����--�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment AssayEnv
		{
			get
			{
				return this.assayEnv;
			}
			set
			{
				this.assayEnv = value;
			}
		}


		/// <summary>
		/// ��������
		/// </summary>
		public Stencil Stencil
		{
			get
			{
				return this.stencil;
			}
			set
			{
				this.stencil = value;
			}
		}


		/// <summary>
		/// ���麬��
		/// </summary>
		public decimal Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		/// <summary>
		/// ��ֵ������
		/// </summary>
		public decimal ResultQty
		{
			get
			{
				return this.resultQty;
			}
			set
			{
				this.resultQty = value;
			}
		}

		/// <summary>
		/// �ַ�������
		/// </summary>
		public string ResultStr
		{
			get
			{
				return this.resultStr;
			}
			set
			{
				this.resultStr = value;
			}
		}
        
		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		public bool IsEligibility
		{
			get
			{
				return this.isEligibility;
			}
			set
			{
				this.isEligibility = value;
			}
		}

		/// <summary>
		/// �����׼����
		/// </summary>
		public string AssayRule
		{
			get
			{
				return this.assayRule;
			}
			set
			{
				this.assayRule = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string CheckOper
		{
			get
			{
				return this.checkOper;
			}
			set
			{
				this.checkOper = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary> 
		public string ReportNum
		{
			get
			{
				return this.reportNum;
			}
			set
			{
				this.reportNum = value;
			}
		}

		/// <summary>
		/// ������(��װ��Ʒ����)
		/// </summary>
		public decimal DivisionQty
		{
			get
			{
				return this.divisionQty;
			}
			set
			{
				this.divisionQty = value;
			}
		}
		#endregion

		#region ���ڵ�����
		
		/// <summary>
		/// ��������
		/// </summary> 
		[System.Obsolete("�Ѿ����ڣ�ʹ��ReportNum", true)]
		public string ReportID
		{
			get
			{
				return this.reportNum;
			}
			set
			{
				this.reportNum = value;
			}
		}

		/// <summary>
		/// ��ֵ������
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��ResultQty",true)]
		public decimal ResultNum
		{
			get
			{
				return this.resultQty;
			}
			set
			{
				this.resultQty = value;
			}
		}

		/// <summary>
		/// �ͼ���
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��ApplyEnv", true)]
		public string ApplyOper
		{
			get
			{
				return this.applyOper;
			}
			set
			{
				this.applyOper = value;
			}
		}
		/// <summary>
		/// �ͼ�����
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��ApplyEnv", true)]
		public DateTime ApplyDate
		{
			get
			{
				return this.applyDate;
			}
			set
			{
				this.applyDate = value;
			}
		}
				/// <summary>
				/// ��������(��������)
				/// </summary>
				private DateTime assayDate;
		
		/// <summary>
		/// ��������(��������)
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��AssayEnv", true)]
		public DateTime AssayDate
		{
			get
			{
				return this.assayDate;
			}
			set
			{
				this.assayDate = value;
			}
		}
				/// <summary>
				/// ������
				/// </summary>
				private string assayOper;
		
		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��AssayEnv", true)]
		public string AssayOper
		{
			get
			{
				return this.assayOper;
			}
			set
			{
				this.assayOper = value;
			}
		}
				private string applyOper;
				private DateTime applyDate;
		#endregion

		#region  ����

		/// <summary>
		/// ���ƶ���
		/// </summary>
		/// <returns>Assay</returns>
		public new Assay Clone()
		{
			Assay assay = base.Clone() as Assay;
			assay.applyEnv = applyEnv.Clone();
			assay.assayEnv = assayEnv.Clone();
			assay.stencil = stencil.Clone();
			return assay;
		}

		#endregion

	}
}