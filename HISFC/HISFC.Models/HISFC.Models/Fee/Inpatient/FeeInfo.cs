using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Inpatient
{
	/// <summary>
	/// FeeItemBase<br></br>
	/// [��������: סԺ������Ϣ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-13]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class FeeInfo : Inpatient.FeeItemList																			
	{
		public FeeInfo()
		{
			this.Patient = new FS.HISFC.Models.RADT.PatientInfo();
		}
		
		#region ����
		
		#endregion

		#region ����

		#endregion
		
		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new FeeInfo Clone()
		{
			FeeInfo feeInfo = base.Clone() as FeeInfo;
			
			return feeInfo;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region IBaby ��Ա

		#endregion

		#endregion
		
		#region �������Ա���

		/// <summary>
		/// ��չ��Ա����
		/// </summary>
		[Obsolete("����,����ExtOper����", true)]
		public string ExtOperCode = "";
		/// <summary>
		/// ��չ����
		/// </summary>
		[Obsolete("����,����ExtOper.OperTime����", true)]
		public DateTime ExtDate;

		/// <summary>
		/// ������ˮ��
		/// </summary>
		[Obsolete("����,����RecipeNO����", true)]
		public string NoteNO;
		/// <summary>
		/// ������Ϣ
		/// </summary>
		[Obsolete("����,����FT����", true)]
		public FT Fee=new FT();
		/// <summary>
		/// �������
		/// </summary>
		[Obsolete("����,����Patient.Pact.PayKind����", true)]
		public NeuObject PayKind=new NeuObject();
		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		[Obsolete("����,����Patient.Pact����", true)]
		public FS.FrameWork.Models.NeuObject  Pact=new NeuObject();
		/// <summary>
		/// �������
		/// </summary>
		[Obsolete("����,BalanceNO����", true)]
		public int BalanceSequance;
		/// <summary>
		/// ����״̬
		/// </summary>
		[Obsolete("����,����BalanceState����", true)]
		public string BalanceStatus;
		
		/// <summary>
		/// �շ�ʱ��
		/// </summary>
		[Obsolete("����,����FeeOper.OperTime����", true)]
		public DateTime DtFee = new DateTime();

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("����,����ChargeOper.OperTime����", true)]
		public DateTime  DtCharge;
	
		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("����,����BalanceOper.OperTime����", true)]
		public DateTime  DtBalance;
		/// <summary>
		/// ���㷢Ʊ��
		/// </summary>
		[Obsolete("����,����Invoice.ID����", true)]
		public string BalanceInvoice;
		/// <summary>
		/// ������
		/// </summary>
		[Obsolete("����,����AuditingNO����", true)]
		public string CheckNo;
	   
		/// <summary>
		/// �շ�Ա����
		/// </summary>
		[Obsolete("����,����FeeOper.Dept����", true)]
		public NeuObject FeeOperDept = new NeuObject();

		/// <summary>
		/// ����ҽ��
		/// </summary>
		[Obsolete("����,����RecipeOper����", true)]
		public NeuObject ReciptDoctor = new NeuObject();

		#endregion		
	}
}
