using System;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Operation 
{
	/// <summary>
	/// [��������: ������̨�����¼��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-09-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class OpsTableAllot : FS.FrameWork.Models.NeuObject
	{
		public OpsTableAllot()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public OpsTableAllot(NeuObject user)
		{
			this.user = user;
		}
#region ����
		private NeuObject opsRoom = new NeuObject();
		//������
		public FS.FrameWork.Models.NeuObject OpsRoom
		{
			get
			{
				return this.opsRoom;
			}
			set
			{
				this.opsRoom = value;
			}
		}

		NeuObject dept = new NeuObject();
		//���������
		public FS.FrameWork.Models.NeuObject Dept
		{
			get
			{
				return this.dept;
			}
			set
			{
				this.dept = value;
			}
		}

		private NeuObject week =new NeuObject();
		//����
		public FS.FrameWork.Models.NeuObject Week
		{
			get
			{
				return this.week;
			}
			set
			{
				this.week = value;
			}
		}

		private NeuObject user;
		//����Ա
		public FS.FrameWork.Models.NeuObject User
		{
			get
			{
				if (this.user == null) 
				{
					this.user = new NeuObject();
				}
				return this.user;
			}
			set
			{
				this.user = value;
			}
		}
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
			get
			{
				return usedQty;
			}
		}

#endregion

#region ����
		public new OpsTableAllot Clone()
		{
			OpsTableAllot newOpsTableAllot = base.Clone() as OpsTableAllot;
			newOpsTableAllot.OpsRoom = this.OpsRoom.Clone();
			newOpsTableAllot.Dept = this.Dept.Clone();
			newOpsTableAllot.week = this.week.Clone();
			newOpsTableAllot.User = this.User.Clone();
			return newOpsTableAllot;
		}
#endregion
		
	}
}
