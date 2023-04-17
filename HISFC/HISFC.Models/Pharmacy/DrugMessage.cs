using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ��ҩ֪ͨ��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶����'
	///  />
	///  ID		�����˱���
	///  Name	����������
	/// </summary>
    [Serializable]
    public class DrugMessage : FS.FrameWork.Models.NeuObject 
	{
		public DrugMessage() 
		{

		}


		#region ����

		/// <summary>
		/// �������
		/// </summary>
		private FS.FrameWork.Models.NeuObject applyDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ������(ȡҩ����)
		/// </summary>
		private FS.FrameWork.Models.NeuObject myMedDept  = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��ҩ������
		/// </summary>
		private FS.FrameWork.Models.NeuObject myDrugBillClass = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ֪ͨ����ʱ��  
		/// </summary>
		private DateTime mySendDtime ;

		/// <summary>
		/// ֪ͨ��������
		/// </summary>
		private int mySendType;

		/// <summary>
		/// ��ҩ֪ͨ��� 0 ֪ͨ 1 �Ѱ�
		/// </summary>
		private int mySendFlag;

		#endregion

		/// <summary>
		/// ������ұ��� 0-ȫ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject ApplyDept 
		{
			get
			{
				return this.applyDept; 
			}
			set
			{ 
				this.applyDept = value; 
			}
		}


		/// <summary>
		/// ��ҩ������
		/// </summary>
		public FS.FrameWork.Models.NeuObject DrugBillClass 
		{
			get
			{
				return this.myDrugBillClass;
			}
			set
			{
				this.myDrugBillClass = value; 
			}
		}


		/// <summary>
		/// �������ͣ�1-���з��ͣ�0-��ʱ����
		/// </summary>
		public int SendType 
		{
			get
			{
				return this.mySendType;
			}
			set
			{
				this.mySendType = value; 
			}
		}


		/// <summary>
		/// ����֪ͨʱ��
		/// </summary>
		public System.DateTime SendTime 
		{
			get
			{
				return this.mySendDtime; 
			}
			set
			{
				this.mySendDtime = value; 
			}
		}


		/// <summary>
		/// ��ҩ���0-֪ͨ1-�Ѱ�
		/// </summary>
		public int SendFlag 
		{
			get
			{
				return this.mySendFlag; 
			}
			set
			{
				this.mySendFlag = value;
			}
		}

		
		/// <summary>
		/// ȡҩ����(������)
		/// </summary>
		public FS.FrameWork.Models.NeuObject StockDept 
		{
			get
			{ 
				return this.myMedDept;
			}
			set
			{ 
				this.myMedDept = value;
			}
		}


		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ������</returns>
		public new DrugMessage Clone()
		{
			DrugMessage drugMessage = base.Clone() as DrugMessage;

			drugMessage.ApplyDept = this.ApplyDept.Clone();

			drugMessage.DrugBillClass = this.DrugBillClass.Clone();

			drugMessage.StockDept = this.StockDept.Clone();

			return drugMessage;
		}


		#region ��Ч����

		/// <summary>
		/// �������
		/// </summary>
		private FS.FrameWork.Models.NeuObject mySendDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ������ұ��� 0-ȫ������
		/// </summary>
		[System.Obsolete("�������� ����ΪApplyDept����",true)]
		public FS.FrameWork.Models.NeuObject SendDept 
		{
			get{ return this.mySendDept; }
			set{ this.mySendDept = value; }
		}


		/// <summary>
		/// ȡҩ����
		/// </summary>
		[System.Obsolete("�������� ����ΪStockDept����",true)]
		public FS.FrameWork.Models.NeuObject MedDept 
		{
			get{ return this.myMedDept; }
			set{ this.myMedDept = value; }
		}



		/// <summary>
		/// ����֪ͨʱ��
		/// </summary>
		[System.Obsolete("�������� ����ΪSendTime����",true)]
		public System.DateTime SendDtime 
		{
			get
			{
				return this.mySendDtime; 
			}
			set
			{
				this.mySendDtime = value; 
			}
		}


		#endregion

	}
}
