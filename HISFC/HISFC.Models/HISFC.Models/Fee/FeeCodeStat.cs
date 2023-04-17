using System;
using System.Collections;

using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// FeeCodeStat<br></br>
	/// [��������: ͳ�ƴ����� ����ID����: MZ01 ���﷢Ʊ ZY01 סԺ��Ʊ]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class FeeCodeStat : NeuObject, ISort, IValid,IValidState
	{
		#region ����
		
		/// <summary>
		/// ��������(ö��)
		/// </summary>
		private ReportTypeEnumService reportType = new ReportTypeEnumService();
		
		/// <summary>
		/// ��С����
		/// </summary>
		private NeuObject minFee = new NeuObject();
		
		/// <summary>
		/// ͳ����Ϣ
		/// </summary>
		private NeuObject feeStat = new NeuObject();
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private NeuObject statCate = new NeuObject();
		
		/// <summary>
		/// ִ�п�����Ϣ
		/// </summary>
		private Department execDept = new Department();
		
		/// <summary>
		/// ҽ�����Ķ�Ӧ����
		/// </summary>
		private string centerStat;
		
		/// <summary>
		/// ��Ч�Ա�ʶ ����(1) ͣ��(0) ����(2)
		/// </summary>
        private FS.HISFC.Models.Base.EnumValidState validState = EnumValidState.Valid;
		
		/// <summary>
		/// ��������(����Ա,����ʱ��,��������)
		/// </summary>
		private OperEnvironment oper = new OperEnvironment();
		
		/// <summary>
		/// ���
		/// </summary>
		private int sortID;
		
		/// <summary>
		/// ��Ч��
		/// </summary>
		public bool isValid;

		#endregion

		#region ����
		
		/// <summary>
		/// ��������(ö��)
		/// </summary>
		public ReportTypeEnumService ReportType
		{
			get
			{
				return this.reportType;
			}
			set
			{
				this.reportType = value;
			}
		}
		
		/// <summary>
		/// ��С����
		/// </summary>
		public NeuObject MinFee
		{
			get
			{
				return this.minFee;
			}
			set
			{
				this.minFee = value;
			}
		}

		/// <summary>
		/// ͳ����Ϣ
		/// </summary>
		public NeuObject FeeStat
		{
			get
			{
				return this.feeStat;
			}
			set
			{
				this.feeStat = value;
			}
		}
		
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public NeuObject StatCate
		{
			get
			{
				return this.statCate;
			}
			set
			{
				this.statCate = value;
			}
		}
		
		/// <summary>
		/// ִ�п�����Ϣ
		/// </summary>
		public Department ExecDept
		{
			get
			{
				return this.execDept;
			}
			set
			{
				this.execDept = value;
			}
		}
		
		/// <summary>
		/// ҽ�����Ķ�Ӧ����
		/// </summary>
		public string CenterStat
		{
			get
			{
				return this.centerStat;
			}
			set
			{
				this.centerStat = value;
			}
		}
		
		/// <summary>
		/// ��Ч�Ա�ʶ ����(1) ͣ��(0) ����(2)
		/// </summary>
		public FS.HISFC.Models.Base.EnumValidState ValidState
		{
			get
			{
				return this.validState ;
			}
			set
			{
				this.validState = value;

				if (this.validState == EnumValidState.Valid)
				{
					this.isValid = true;
				}
				else
				{
					this.isValid = false;
				}
			}
		}
		
		/// <summary>
		/// ��������(����Ա,����ʱ��,��������)
		/// </summary>
		public OperEnvironment Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;
			}
		}

		#endregion

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new FeeCodeStat Clone()
		{
			FeeCodeStat feeCodeStat = base.Clone() as FeeCodeStat;

			feeCodeStat.ExecDept = this.ExecDept.Clone();
			feeCodeStat.FeeStat = this.FeeStat.Clone();
			feeCodeStat.MinFee = this.MinFee.Clone();
			feeCodeStat.Oper = this.Oper.Clone();
			feeCodeStat.StatCate = this.StatCate.Clone();
			
			return feeCodeStat;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region ISort ��Ա
		
		/// <summary>
		/// ���
		/// </summary>
		public int SortID
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

		#endregion

		#region IValid ��Ա

		/// <summary>
		/// ��Ч�� ����ValidState���Ա仯,ValidState = "1" Ϊtrue����ֵ��Ϊfalse
		/// </summary>
		public bool IsValid
		{
			get
			{
				if (this.validState == EnumValidState.Valid)
				{
					this.isValid = true;
				}
				else
				{
					this.isValid = false;
				}

				return this.isValid;
			}
			set
			{

			}
		}

		#endregion

		#endregion

		#region ��������,����

		[Obsolete("����,ʹ��base.Name����")]
		public NeuObject ReportName = new NeuObject();

		/// <summary>
		/// ��С���ô���
		/// </summary>
		[Obsolete("����,ʹ��MinFee.ID����")]
		public string FeeCode;
		/// <summary>
		/// ͳ�Ʒ��ô���
		/// </summary>
		[Obsolete("����,ʹ��FeeStat.ID����")]
		public string FeeStatCode;
		/// <summary>
		/// ͳ������
		/// </summary>
		[Obsolete("����,ʹ��FeeStat.Name����")]
		public string StatName;
		
		/// <summary>
		/// ִ�п���
		/// </summary>
		[Obsolete("����,ʹ��ExecDept����")]
		public NeuObject ExeDept = new NeuObject();
		/// <summary>
		/// ��ӡ˳��
		/// </summary>
		[Obsolete("����,ʹ��SortID����")]
		public int PrintOrder;
		/// <summary>
		/// '0'	��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ����
		/// </summary>
		[Obsolete("����,ʹ��ValidState����")]
		public string ValidStat;
		
		[Obsolete("����,ʹ��Oper.Employee.ID����")]
		public string OperCode;
		[Obsolete("����,ʹ��Oper.OperTime����")]
		public DateTime OperDate;

		#endregion
		
	}
	
	[Obsolete("����,ʹ��EnumReportType")]
	public enum eReportType
	{
		/// <summary>
		/// ��Ʊ
		/// </summary>
		FP=0,
		/// <summary>
		/// ͳ��
		/// </summary>
		TJ=1,
		/// <summary>
		/// ����
		/// </summary>
		BA=2,
		/// <summary>
		/// ֪��Ȩ
		/// </summary>
		ZQ=3
	}
}
