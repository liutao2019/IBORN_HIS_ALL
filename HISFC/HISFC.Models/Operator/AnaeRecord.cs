using System;

namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// AnaeRecord ��ժҪ˵����
	/// ����Ǽ�ʵ����
	/// </summary>
	public class AnaeRecord
	{
		public AnaeRecord()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �������뵥����(�����˾��󲿷�Ҫ�Ǽǵ���Ϣ)
		/// </summary>
		public neusoft.HISFC.Object.Operator.OpsApplication m_objOpsApp = new OpsApplication();
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime AnaeDate = DateTime.MinValue;
		/// <summary>
		/// ����Ч��
		/// </summary>
		public neusoft.neuFC.Object.neuObject AnaeResult = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// �Ƿ���PACU 1��/0��
		/// </summary>
		private string IsPACU = "";
		public bool bIsPACU
		{
			get
			{
				if(IsPACU == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value)
					IsPACU = "1";
				else 
					IsPACU = "0";
			}
		}
		/// <summary>
		/// ��(PACU)��ʱ��
		/// </summary>
		public DateTime InPacuDate = DateTime.MinValue;
		/// <summary>
		/// ��(PACU)��ʱ��
		/// </summary>
		public DateTime OutPacuDate = DateTime.MinValue;
		/// <summary>
		/// ��(PACU)��״̬
		/// </summary>
		public neusoft.neuFC.Object.neuObject InPacuStatus = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ��(PACU)��״̬
		/// </summary>
		public neusoft.neuFC.Object.neuObject OutPacuStatus = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark = "";
		/// <summary>
		/// ������ʹ 1��/0��
		/// </summary>
		private string Demulcent = "";
		public bool bIsDemulcent
		{
			get
			{
				if(Demulcent == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value)
					Demulcent = "1";
				else 
					Demulcent = "0";
			}
		}
		/// <summary>
		/// ��ʹ��ʽ
		/// </summary>
		public neusoft.neuFC.Object.neuObject DemuKind = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject DemuModel = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ��ʹ����
		/// </summary>
		public int DemuDays = 0;
		/// <summary>
		/// �ι�ʱ��
		/// </summary>
		public DateTime PullOutDate = DateTime.MinValue;
		/// <summary>
		/// �ι���
		/// </summary>
		public neusoft.neuFC.Object.neuObject PullOutOpcd = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// ��ʹЧ��
		/// </summary>
		public neusoft.neuFC.Object.neuObject DemuResult = new neusoft.neuFC.Object.neuObject();
		/// <summary>
		/// �Ƿ���� 1��/0��
		/// </summary>
		private string ChargeFlag = "";
		public bool bChargeFlag
		{
			get
			{
				if(ChargeFlag == "1")
					return true;
				else 
					return false;
			}
			set
			{
				if(value)
					ChargeFlag = "1";
				else 
					ChargeFlag = "0";
			}
		}	
		/// <summary>
		/// ִ�п���
		/// </summary>
		public neusoft.neuFC.Object.neuObject ExecDept = new neusoft.neuFC.Object.neuObject();
	}
}
