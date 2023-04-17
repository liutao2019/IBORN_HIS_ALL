using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Item
{
	/// <summary>
	/// AdjustPrice<br></br>
	/// [��������: ��ҩƷ������]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-15]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class AdjustPrice : NeuObject, IValid
	{
		#region ����
		
		/// <summary>
		/// �������
		/// </summary>
		private string adjustPriceNO;
		
		/// <summary>
		/// ԭʼ��Ŀ��Ϣ
		/// </summary>
		private Base.Item orgItem = new FS.HISFC.Models.Base.Item();
		
		/// <summary>
		/// ���ۺ���Ŀ��Ϣ
		/// </summary>
		private Base.Item newItem = new FS.HISFC.Models.Base.Item();
		
		/// <summary>
		/// ��������(����Ա,����ʱ��,������Ϣ)
		/// </summary>
		private OperEnvironment oper = new OperEnvironment();
		
		/// <summary>
		/// ����״̬ δ��Ч(0) ��Ч(1) ����(2)
		/// </summary>
		private string validState;
		
		/// <summary>
		/// ������Чʱ��
		/// </summary>
		private DateTime beginTime;
		
		/// <summary>
		/// ��Ч���ж�
		/// </summary>
		private bool isValid;

		#endregion

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		public string AdjustPriceNO
		{
			get
			{
				return this.adjustPriceNO;
			}
			set
			{
				this.adjustPriceNO = value;
			}
		}
		
		/// <summary>
		/// ԭʼ��Ŀ��Ϣ
		/// </summary>
		public Base.Item OrgItem
		{
			get
			{
				return this.orgItem;
			}
			set
			{
				this.orgItem = value;

				if (this.newItem != null)
				{
					this.newItem.ID = this.orgItem.ID;
					this.newItem.Name = this.orgItem.Name;
				}
			}
		}
		
		/// <summary>
		/// ���ۺ���Ŀ��Ϣ
		/// </summary>
		public Base.Item NewItem
		{
			get
			{
				return this.newItem;
			}
			set
			{
				this.newItem = value;

				if (this.orgItem != null)
				{
					this.orgItem.ID = this.newItem.ID;
					this.orgItem.Name = this.newItem.Name;
				}
			}
		}
		
		/// <summary>
		/// ��������(����Ա,����ʱ��,������Ϣ)
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
		
		/// <summary>
		/// ����״̬ δ��Ч(0) ��Ч(1) ����(2)
		/// </summary>
		public string ValidState
		{
			get
			{
				return this.validState;
			}
			set
			{
				this.validState = value;

				if (this.validState == "1")
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
		/// ������Чʱ��
		/// </summary>
		public DateTime BeginTime
		{
			get
			{
				return this.beginTime;
			}
			set
			{
				this.beginTime = value;
			}
		}

		#endregion

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new AdjustPrice Clone()
		{
			AdjustPrice adjustPrice = base.Clone() as AdjustPrice;

			adjustPrice.NewItem = this.NewItem.Clone();
			adjustPrice.OrgItem = this.OrgItem.Clone();
			adjustPrice.Oper = this.Oper.Clone();

			return adjustPrice;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region IValid ��Ա
		
		/// <summary>
		/// ��Ч��,ֻ�ܻ�ȡ���ܸ�ֵ.�����Ը���ValidStateֵ�仯,��ValidState = "1"ΪTrue������Ϊfalse
		/// </summary>
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
			}
		}

		#endregion

		#endregion
		
		
		[Obsolete("����,AdjustPriceNO����", true)]
		public string AdjustPriceNo;
		[Obsolete("����,OrgItem����", true)]
		public string ItemCode;
		[Obsolete("����,OrgItem����", true)]
		public string ItemName;
		[Obsolete("����,OrgItem.Price����", true)]
		public decimal PriceOld; //���׼�  ��
		[Obsolete("����,NewItem.Price����", true)]
		public decimal PriceNew; // ���׼� ��
		[Obsolete("����,OrgItem.ChildPrice����", true)]
		public decimal PriceOld1; //��ͯ��  ��
		[Obsolete("����,NewItem.ChindPrice����", true)]
		public decimal PriceNew1; // ��ͯ�� ��
		[Obsolete("����,OrgItem.SpecialPrice����", true)]
		public decimal PriceOld2; //�����  ��
		[Obsolete("����,NewItem.SpcialPrice����", true)]
		public decimal PriceNew2; // ����� ��
		[Obsolete("����,Oper����", true)]
		public string OperCode;
		[Obsolete("����,Oper.OperTime����", true)]
		public System.DateTime  operdate;
		[Obsolete("����,ValidState", true)]
		public bool IsNow;
		
	}
}
