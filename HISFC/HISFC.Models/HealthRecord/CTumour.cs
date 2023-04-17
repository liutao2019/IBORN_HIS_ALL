using System;


namespace Neusoft.HISFC.Object.HealthRecord {


	/// <summary>
	/// CTumour ��ժҪ˵����
	/// </summary>
	public class CTumour : Neusoft.NFC.Object.NeuObject
	{
		public CTumour()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		private string inpatientNo;//   --סԺ��ˮ��
		private string  rmodeid; //  --���Ʒ�ʽ
		private string  rprocessid;//   --���Ƴ�ʽ
		private string  rdeviceid;//   --����װ��
		private string cmodeid;//   --���Ʒ�ʽ
		private string  cmethod;//   --���Ʒ���
		private decimal  gy1;//   --ԭ����gy����
		private decimal  time1;   //--ԭ�������
		private decimal  day1;//   --ԭ��������
		private System.DateTime begin_date1;//   --ԭ���ʼʱ��
		private System.DateTime   end_date1;//   --ԭ�������ʱ��
		private decimal  gy2;//   --�����ܰͽ�gy����
		private decimal   time2; //  --�����ܰͽ����
		private decimal   day2; //  --�����ܰͽ�����
		private System.DateTime    begin_date2;//   --�����ܰͽῪʼʱ��
		private System.DateTime   end_date2;//   --�����ܰͽ����ʱ��
		private decimal   gy3;//   --ת����gy����
		private decimal   time3;//   --ת�������
		private decimal   day3; //  --ת��������
		private System.DateTime   begin_date3;//   --ת���ʼʱ��
		private System.DateTime  end_date3;//   --ת�������ʱ��
		//--����Ա����
		private Neusoft.NFC.Object.NeuObject operInfo = new Neusoft.NFC.Object.NeuObject() ;
		#region ���� 
		public string Cmethod
		{
			get
			{
				if(cmethod == null)
				{
					cmethod = "";
				}
				return cmethod;
			}
			set
			{
				cmethod = value;
			}
		}
		/// <summary>
		/// ����Ա����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject OperInfo 
		{
			get
			{
				return operInfo;
			}
			set
			{
				operInfo = value;
			}
		}
		/// <summary>
		/// ת�������ʱ��
		/// </summary>
		public System.DateTime EndDate3
		{
			get
			{
				return end_date3;
			}
			set
			{
				end_date3 = value;
			}
		}
		/// <summary>
		/// ת���ʼʱ��
		/// </summary>
		public System.DateTime BeginDate3
		{
			get
			{
				return begin_date3;
			}
			set
			{
				begin_date3 = value;
			}
		}
		/// <summary>
		/// ת�������
		/// </summary>
		public decimal Time3
		{
			get
			{
				return time3;
			}
			set
			{
				time3 = value;
			}
		}
		/// <summary>
		/// ת����gy����
		/// </summary>
		public decimal Gy3
		{
			get
			{
				return gy3;
			}
			set
			{
				gy3 = value;
			}
		}
		/// <summary>
		/// ת��������
		/// </summary>
		public decimal Day3
		{
			get
			{
				return day3; 
			}
			set
			{
				day3 = value;
			}
		}
		/// <summary>
		/// �����ܰͽ����ʱ��
		/// </summary>
		public System.DateTime EndDate2
		{
			get
			{
				return end_date2;
			}
			set
			{
				end_date2 = value;
			}
		}
		/// <summary>
		/// �����ܰͽῪʼʱ��
		/// </summary>
		public System.DateTime BeginDate2
		{
			get
			{
				return begin_date2;
			}
			set
			{
				begin_date2 = value;
			}
		}
		/// <summary>
		/// �����ܰͽ�����
		/// </summary>
		public decimal Day2
		{
			get
			{
				return day2; 
			}
			set
			{
				day2 = value;
			}
		}
		/// <summary>
		/// �����ܰͽ����
		/// </summary>
		public decimal Time2
		{
			get
			{
				return time2;
			}
			set
			{
				time2 = value;
			}
		}
		/// <summary>
		/// �����ܰͽ�gy����
		/// </summary>
		public decimal Gy2
		{
			get
			{
				return gy2;
			}
			set
			{
				gy2 = value;
			}
		}

		/// <summary>
		/// ԭ�������ʱ��
		/// </summary>
		public System.DateTime EndDate1
		{
			get
			{
				return end_date1;
			}
			set
			{
				end_date1 = value;
			}
		}
		/// <summary>
		/// ԭ���ʼʱ��
		/// </summary>
		public System.DateTime BeginDate1
		{
			get
			{
				return begin_date1;
			}
			set
			{
				begin_date1 = value;
			}
		}
		/// <summary>
		/// ԭ��������
		/// </summary>
		public decimal Day1
		{
			get
			{
				return day1;
			}
			set
			{
				day1 = value;
			}
		}
		/// <summary>
		/// ԭ�������
		/// </summary>
		public decimal Time1
		{
			get
			{
				return time1;
			}
			set
			{
				time1 = value;
			}
		}
		/// <summary>
		/// ԭ����gy����
		/// </summary>
		public decimal Gy1
		{
			get
			{
				return gy1 ;
			}
			set
			{
				gy1 = value;
			}
		}
		/// <summary>
		/// ���Ʒ�ʽ
		/// </summary>
		public string Cmodeid
		{
			get
			{
				if(cmodeid == null)
				{
					cmodeid = "";
				}
				return cmodeid;
			}
			set
			{
				cmodeid = value;
			}
		}
		/// <summary>
		/// ����װ��
		/// </summary>
		public string  Rdeviceid
		{
			get
			{
				if(rdeviceid == null)
				{
					rdeviceid = "";
				}
				return rdeviceid;
			}
			set
			{
				rdeviceid = value;
			}
		}
		/// <summary>
		/// ���Ƴ�ʽ
		/// </summary>
		public  string Rprocessid
		{
			get
			{
				if(rprocessid == null)
				{
					rprocessid = "";
				}
				return rprocessid;
			}
			set
			{
				rprocessid = value;
			}
		}
		/// <summary>
		/// ���Ʒ�ʽ
		/// </summary>
		public string Rmodeid
		{
			get
			{
				if(rmodeid == null)
				{
					rmodeid = "";
				}
				return rmodeid;
			}
			set
			{
				rmodeid = value;
			}
		}
		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		public string InpatientNo
		{
			get
			{
				if(inpatientNo == null)
				{
					inpatientNo = "";
				}
				return inpatientNo;
			}
			set
			{
				 inpatientNo =value;
			}
		}
		#endregion 


		public new CTumour Clone()
		{
			CTumour ct = (CTumour)base.Clone();
			ct.operInfo = this.operInfo.Clone();
			return ct;
		}
	}
}
