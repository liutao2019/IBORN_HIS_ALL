using System;
using neusoft.neuFC;
using neusoft.HISFC;
using System.Collections;
namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// ������Ŀ��Ϣ�� Written By liling 
	/// </summary>
	public class OperateInfo:neusoft.neuFC.Object.neuObject
	{
		public OperateInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		///<summary>
		///��Ŀ����
		///</summary>
		public neusoft.HISFC.Object.Base.Item OperateItem = new neusoft.HISFC.Object.Base.Item();

		///<summary>
		///�շѱ���
		///</summary>
		public decimal FeeRate = 1;

		///<summary>
		///����
		///</summary>
		public int Qty = 0;

		///<summary>
		///��λ
		///</summary>
		public string StockUnit = "";
		///<summary>
		///������ģ
		///</summary>
		public neusoft.neuFC.Object.neuObject OperateType = new neusoft.neuFC.Object.neuObject();
		
		///<summary>
		///�п�����
		///</summary>
		public neusoft.neuFC.Object.neuObject InciType = new neusoft.neuFC.Object.neuObject();

		///<summary>
		///������λ
		///</summary>
		public neusoft.neuFC.Object.neuObject OpePos = new neusoft.neuFC.Object.neuObject();
		///<summary>
		///��ע
		///</summary>
		public string Remark = "";

		///<summary>
		///��������־ 1��/0��
		///</summary>
		private string Main_Flag = "0";
		public bool bMainFlag
		{
			get
			{
				if(Main_Flag =="1")
					return true;
				else
					return false;
			}
			set
			{
				if(value ==true)
					Main_Flag = "1";
				else
					Main_Flag = "0";
			}
		}
		///<summary>
		///1��Ч/0��Ч
		///</summary>
		private string YNValid = "1";
		public bool bValid
		{
			get
			{
				if(YNValid =="1")
					return true;
				else
					return false;
			}
			set
			{
				if(value ==true)
					YNValid = "1";
				else
					YNValid = "0";
			}
		}
		public new OperateInfo Clone()
		{
			OperateInfo newOpsInfo = new OperateInfo();
			newOpsInfo.OperateItem = this.OperateItem.Clone();
			newOpsInfo.FeeRate = this.FeeRate;
			newOpsInfo.Qty = this.Qty;
			newOpsInfo.StockUnit = this.StockUnit;
			newOpsInfo.OperateType = this.OperateType.Clone();
			newOpsInfo.InciType = this.InciType.Clone();
			newOpsInfo.OpePos = this.OpePos.Clone();
			newOpsInfo.Remark = this.Remark;
			newOpsInfo.Main_Flag = this.Main_Flag;
			newOpsInfo.YNValid = this.YNValid;
			return newOpsInfo;
		}
	}
}
