using System;
//using FS.NFC;
using FS.HISFC;
using System.Collections;


namespace FS.HISFC.Models.Operation 
{

	/// <summary>
	/// ������Ŀ��Ϣ�� Written By liling
	/// </summary>
    [Serializable]
    public class OperationInfo : FS.FrameWork.Models.NeuObject
	{
		public OperationInfo()
		{
			
		}

		private FS.HISFC.Models.Base.Item operationItem;

		///<summary>
		///�շѱ���
		///</summary>
		private decimal feeRate = 1;
		public decimal FeeRate
		{
			get
			{
				return this.feeRate;
			}
			set
			{
				this.feeRate = value;
			}
		}

		///<summary>
		///����
		///</summary>
		private int qty = 0;
		public int Qty
		{
			get
			{
				return this.qty;
			}
			set
			{
				this.qty = value;
			}
		}

		///<summary>
		///��λ
		///</summary>
		private string stockUnit = string.Empty;

#region ����
        ///<summary>
        ///����������Ŀ
        ///</summary>
        public FS.HISFC.Models.Base.Item OperationItem
        {
            get
            {
                if (this.operationItem == null)
                {
                    this.operationItem = new FS.HISFC.Models.Base.Item();
                }

                return this.operationItem;
            }

            set
            {
                this.operationItem = value;
            }
        }

		public string StockUnit
		{
			get
			{
				return this.stockUnit;
			}
			set
			{
				this.stockUnit = value;
			}
		}
		///<summary>
		///������ģ
		///</summary>
		public FS.FrameWork.Models.NeuObject OperateType = new FS.FrameWork.Models.NeuObject();
		
		///<summary>
		///�п�����
		///</summary>
		public FS.FrameWork.Models.NeuObject InciType = new FS.FrameWork.Models.NeuObject();

		///<summary>
		///������λ
		///</summary>
		public FS.FrameWork.Models.NeuObject OpePos = new FS.FrameWork.Models.NeuObject();
		///<summary>
		///��ע
		///</summary>
		private string remark = string.Empty;
		public string Remark
		{
			get
			{
				return this.remark;
			}
			set
			{
				this.remark = value;
			}
		}

		///<summary>
		///��������־ 1��/0��
		///</summary>
		private bool mainFlag;

		[Obsolete("��ΪIsMainFlag",true)]
		public bool bMainFlag
		{
			get
			{
				return this.mainFlag;
			}
			set
			{
				this.mainFlag = value;
			}
		}

		/// <summary>
		/// ��������־
		/// </summary>
		public bool IsMainFlag
		{
			get
			{
				return this.mainFlag;
			}
			set
			{
				this.mainFlag = value;
			}
		}
		///<summary>
		///1��Ч/0��Ч
		///</summary>
		private bool isValid = true;
		[Obsolete("��ΪIsValid",true)]
		public bool bValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
        }
#endregion
        public new OperationInfo Clone()
		{
			OperationInfo newOpsInfo = base.Clone() as OperationInfo;
            newOpsInfo.operationItem = this.operationItem.Clone();
			newOpsInfo.OperateType = this.OperateType.Clone();
			newOpsInfo.InciType = this.InciType.Clone();
			newOpsInfo.OpePos = this.OpePos.Clone();
			return newOpsInfo;
		}
	}
}
