using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Inpatient 
{

	/// <summary>
	/// BalancePayBase<br></br>
	/// [��������: סԺ֧����ʽ��]<br></br>
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
	public class BalancePay : BalancePayBase 
	{
		#region ����
		
		/// <summary>
		/// �������
		/// </summary>
		private int balanceNO;
		
		/// <summary>
		/// �������� ID: 0 Ԥ���� 1 �����
		/// </summary>
		private NeuObject transKind = new NeuObject();
	    
		#endregion
		
        #region ����

		/// <summary>
		///�������� ID: 0 Ԥ���� 1 �����
		/// </summary>
		public NeuObject TransKind
		{
			get
			{
				return this.transKind;
			}
			set
			{
				this.transKind = value;
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		public int BalanceNO
		{
			get
			{
				return this.balanceNO;
			}
			set
			{
				this.balanceNO = value;
			}
		}
       
		#endregion
		
        #region �������Ա���

		/// <summary>
		/// ��Ʊ��
		/// </summary>д�� ������ID���� �����з�Ʊ��Ϣ
		[Obsolete("����,�����Invoice.ID����", true)]
		public string InvoiceNo;
		
		
		/// <summary>
		/// ���
		/// </summary>ûд������ FT
		[Obsolete("����,����FT����", true)]
		public decimal Cost= 0m;

		/// <summary>
		/// ����ʱ��
		/// </summary>???ûд
		[Obsolete("����,����BalanceOper.OperTime����", true)]
		public System.DateTime DtBalance;
		/// <summary>
		/// �������
		/// </summary>

        [Obsolete("����,ʹ��BalanceNO����", true)]
		public int BalanceNo;
		
		#endregion
        
	    #region ����
        
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ�����ʵ������</returns>
		public new BalancePay Clone()
		{
			return base.Clone() as BalancePay;
		}
		
		#endregion
        
		#endregion
	}
}
