using System;


namespace Neusoft.HISFC.Object.HealthRecord {


	/// <summary>
	/// CTumourDetail ��ժҪ˵����
	/// </summary>
	public class CTumourDetail :Neusoft.NFC.Object.NeuObject 
	{
		public CTumourDetail()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private  string   inpatient_no;  // --סԺ��ˮ��
		private int   happen_no;  // --�������
		private System.DateTime   cure_date;  // --��������
		//ҩ����Ϣ
		private Neusoft.NFC.Object.NeuObject drugInfo = new Neusoft.NFC.Object.NeuObject();

		private decimal   qty;//   --����
		private string unit;//   --��λ
		private string   period;//   --�Ƴ�
		private string   result; //  --��Ч
		//����Ա����	
		private Neusoft.NFC.Object.NeuObject   operInfo = new Neusoft.NFC.Object.NeuObject();
		#region  ����
		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		public string InpatientNo
		{
			get
			{
				return inpatient_no;
			}
			set
			{
				inpatient_no = value;
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public int HappenNO
		{
			get
			{
				return happen_no;
			}
			set
			{
				happen_no =  value;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public System.DateTime CureDate
		{
			get
			{
				return cure_date;
			}
			set
			{
				cure_date = value;
			}
		}
		/// <summary>
		/// ҩ����Ϣ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject  DrugInfo
		{
			get
			{
				return drugInfo;
			}
			set
			{
				drugInfo = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public decimal Qty
		{
			get
			{
				return qty;
			}
			set
			{
				qty = value;
			}
		}
		/// <summary>
		/// ��λ
		/// </summary>
		public string Unit
		{
			get
			{
				if(unit == null)
				{
					unit = "";
				}
				return unit;
			}
			set
			{
				unit = value;
			}
		}
		/// <summary>
		/// �Ƴ�
		/// </summary>
		public string Period 
		{
			get
			{
				if(period == null)
				{
					period  = "";
				}
				return period;
			}
			set
			{
				period = value;
			}
		}
		/// <summary>
		/// ��Ч
		/// </summary>
		public string Result
		{
			get
			{
				if(result == null)
				{
					result = "";
				}
				return result;
			}
			set
			{
				result = value;
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
				operInfo =  value;
			}

		}
		#endregion


		public new CTumourDetail Clone()
		{
			CTumourDetail ctd = (CTumourDetail)base.Clone();
			ctd.drugInfo = this.drugInfo.Clone();
			ctd.operInfo = this.operInfo.Clone();
			return ctd;
		}
	}
}
