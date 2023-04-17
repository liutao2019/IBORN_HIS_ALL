using System;

namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.SpecialFrequency<br></br>
	/// [��������: ����Ƶ��ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class SpecialFrequency:FS.FrameWork.Models.NeuObject
	{
		public SpecialFrequency()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		///  //ҽ����ˮ��
		/// </summary>
		private string orderID;
	
		/// <summary>
		/// //ҽ����Ϻ�
		/// </summary>
		private FS.HISFC.Models.Order.Combo combo = new Combo();  
		
		/// <summary>
		///  //Ƶ�ε�
		/// </summary>
		private string point;
		
		/// <summary>
		/// //  Ƶ�ε�����
		/// </summary>
		private string dose; 
		
		/// <summary>
		/// ����Ա
		/// </summary>
		private Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();//��������

		#endregion

		#region ����
		/// <summary>
		/// ҽ����
		/// </summary>
		public string OrderID
		{
			get
			{
				return this.orderID;
			}
			set
			{
				this.orderID = value;
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		public Combo Combo
		{
			set
			{
				this.combo = value;
			}
			get
			{
				return this.combo;	
			}
		}

		/// <summary>
		/// ʱ���
		/// </summary>
		public string Point
		{
			get
			{
				return this.point;
			}
			set
			{
				this.point = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Dose
		{
			get
			{
				return this.dose;
			}
			set
			{
				this.dose = value;
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public Base.OperEnvironment Oper
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
		#endregion

		#region ���ϵ�

		[Obsolete("��OrderID����",true)]
		public string moOrder; //ҽ����ˮ��
		[Obsolete("��Combo.ID����",true)]
		public string combNo;  //ҽ����Ϻ�
		[Obsolete("��ID����",true)]
		public string drqFreqtype; //Ƶ������
		[Obsolete("��Name����",true)]
		public string drefreqName; //Ƶ������
		[Obsolete("��Point����",true)]
		public string drqPoint; //Ƶ�ε�
		[Obsolete("��Dose����",true)]
		public string dosePoint; //  Ƶ�ε�����
		[Obsolete("��Oper.ID����",true)]
		public string OperID; // ����Ա
		[Obsolete("��Oper.OperTime����",true)]
		public System.DateTime operDate; //����ʱ��

		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new SpecialFrequency Clone()
		{
			SpecialFrequency obj = base.Clone() as SpecialFrequency;
			obj.combo = this.combo.Clone();
			obj.oper = this.oper.Clone();
			return obj;
		}

		#endregion

		#endregion

	}
}
