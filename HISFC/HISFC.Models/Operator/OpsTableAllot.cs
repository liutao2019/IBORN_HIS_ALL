using System;
using System.Collections;
namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// OpsTableAllot ��ժҪ˵����
	/// ������̨�����¼��
	/// </summary>
	public class OpsTableAllot:neusoft.neuFC.Object.neuObject
	{
		public OpsTableAllot()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//������
		public neusoft.neuFC.Object.neuObject OpsRoom = new neusoft.neuFC.Object.neuObject();
		//���������
		public neusoft.neuFC.Object.neuObject Dept = new neusoft.neuFC.Object.neuObject();
		//����
		public neusoft.neuFC.Object.neuObject week = new neusoft.neuFC.Object.neuObject();
		//����Ա
		public neusoft.neuFC.Object.neuObject User = new neusoft.neuFC.Object.neuObject();
		//������̨��
		private int limitedQty;
		public int Qty
		{
			get
			{				
				return this.limitedQty;
			}
			set
			{
				this.limitedQty = value;
			}
		}		
		//�Ѿ�ʹ����̨��
		private int usedQty;
		/// <summary>
		/// ʹ����̨��
		/// </summary>
		public int Used
		{
			get{return usedQty;}
		}

		public new OpsTableAllot Clone()
		{
			OpsTableAllot newOpsTableAllot = new OpsTableAllot();
			newOpsTableAllot.OpsRoom = this.OpsRoom.Clone();
			newOpsTableAllot.Dept = this.Dept.Clone();
			newOpsTableAllot.week = this.week.Clone();
			newOpsTableAllot.limitedQty = this.limitedQty;
			newOpsTableAllot.usedQty=this.usedQty;
			newOpsTableAllot.User = this.User.Clone();
			return newOpsTableAllot;
		}
	}
}
