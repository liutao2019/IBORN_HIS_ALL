using System;

using Neusoft.NFC.Object;
namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	///������Ŀ��Ϣ��  
    ///IDסԺ��ˮ��
	///name ��������
	/// </summary>
	public class FeeItemList:Neusoft.NFC.Object.NeuObject
	{
		/// <summary>
		/// ������Ŀ��Ϣ
		/// </summary>
		public FeeItemList()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FeeInfo FeeInfo=new FeeInfo();
		/// <summary>
		/// ��Ŀ��Ϣ ҩƷ/��ҩƷ
		/// </summary>
		public Object.Base.Item  Item
		{
			get
			{
				return this.pOrder.Order.Item;
			}
			set
			{
				this.pOrder.Order.Item= value;
			}
		}
		protected Neusoft.HISFC.Object.Order.ExecOrder pOrder=new Neusoft.HISFC.Object.Order.ExecOrder();
		/// <summary>
		/// ִ�е���Ϣ
		/// ��Ҫ��Ŀ��Ϣ����Ҫִ�е��ţ�ҽ����ˮ��
		/// </summary>
		public Neusoft.HISFC.Object.Order.ExecOrder  Order
		{
			get
			{
				return this.pOrder;		
			}set
			{
				 this.pOrder = value;
			}
		}
		/// <summary>
		/// ҽ����ˮ��
		/// </summary>
		public string MoOrder
		{
			get
			{
				return this.Order.Order.ID;
			}
			set
			{
				this.Order.Order.ID=value;
			}
		}
		
		/// <summary>
		/// �����������Ϣ
		/// </summary>
		public NeuObject ItemGroup = new NeuObject()   ;
		/// <summary>
		/// ��������ˮ��
		/// </summary>
		public int SequenceNo;
		/// <summary>
		/// 0����1�շ�2ִ�з���
		/// </summary>
		public string SendState;
		/// <summary>
		/// ִ����
		/// </summary>
		public NeuObject ExecOper = new NeuObject();
		/// <summary>
		/// ִ������
		/// </summary>
		public DateTime DtExec;
		/// <summary>
		/// �Ƿ��Ժ����(��Ϊҽ������)
		/// </summary>
		public string IsBrought;
		/// <summary>
		/// ���ⵥ��
		/// </summary>
		public int SendSequence;
		/// <summary>
		/// �ۿ���ˮ��
		/// </summary>
		public int UpdateSequence;
		/// <summary>
		/// �豸���
		/// </summary>
		public string MachineNo;
		/// <summary>
		/// ��������
		/// </summary>
		public decimal NoBackNum=0m;
		/// <summary>
		/// �շѱ���(������)
		/// </summary>
		public decimal Rate=0m;
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new FeeItemList Clone()
		{
			FeeItemList obj = base.Clone() as FeeItemList;
			obj.FeeInfo =this.FeeInfo.Clone();
			obj.pOrder=this.pOrder.Clone();
			obj.ItemGroup=this.ItemGroup.Clone();
			obj.ExecOper=this.ExecOper.Clone();
			return obj;
		}

	}
}
