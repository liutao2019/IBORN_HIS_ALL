using System;
using Neusoft.HISFC.Object.Base;
using Neusoft.NFC.Object;
using Neusoft.HISFC;
namespace Neusoft.HISFC.Object.Order
{
	/// <summary>
	/// ҽ��ִ������ʵ��written by caiy
	/// 2004-6
	/// </summary>
	public class ExecOrder:Neusoft.NFC.Object.NeuObject,Neusoft.HISFC.Object.Base.IValid
		,Neusoft.HISFC.Object.Base.IDept
	{
		/// <summary>
		/// ҽ������ʵ��
		/// ID ҽ����ˮ��
		/// </summary>
		public ExecOrder()
		{
			
		}
		/// <summary>
		/// ҽ����Ϣ
		/// </summary>
		public Object.Order.Order  Order = new Order();
		/// <summary>
		/// ��ʼʹ��ʱ��
		/// </summary>
		public DateTime DateUse;
		/// <summary>
		/// �ֽ�ʱ��
		/// </summary>
		public DateTime DateDeco;
		/// <summary>
		/// 0���跢��/1���з���/2��ɢ����/3����ҩ
		/// </summary>
		public int DrugFlag = 0;
		/// <summary>
		/// �Ƿ���Ч��1����Ч 0������ ��
		/// </summary>
		protected bool isValid = true;
		/// <summary>
		/// ������
		/// </summary>
		public Base.Employee DcExecUser = new Employee();
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime DcExecTime;

		/// <summary>
		/// �Ƿ�ִ�У�1����ִ�� 0��δִ�� ��
		/// </summary>
		public bool IsExec =false;
		/// <summary>
		/// ִ����
		/// </summary>
		public NeuObject ExecUser=new NeuObject();
		/// <summary>
		/// ִ��ʱ��
		/// </summary>
		public DateTime ExecTime;
		/// <summary>
		/// �Ƿ��շѣ�1�����շ� 0��δ�շ� ��
		/// </summary>
		public bool IsCharge = false;
		/// <summary>
		/// �շ���
		/// </summary>
		public NeuObject ChargeUser=new NeuObject();
		/// <summary>
		/// �շѿ���
		/// </summary>
		public NeuObject ChargeDept=new NeuObject();
		/// <summary>
		/// �շ�ʱ��
		/// </summary>
		public DateTime ChargeTime;
		/// <summary>
		/// ��ҩʱ��
		/// </summary>
		public DateTime DrugedTime;

		#region IsValid ��Ա

		public bool IsValid
		{
			get
			{
				// TODO:  ��� ExecOrder.IsInvalid getter ʵ��
				return this.isValid;
			}
			set
			{
				// TODO:  ��� ExecOrder.IsInvalid setter ʵ��
				this.isValid =value;
			}
		}

		#endregion

		#region IDept ��Ա

		public NeuObject InDept
		{
			get
			{
				// TODO:  ��� ExecOrder.InDept getter ʵ��
				return this.Order.InDept;
			}
			set
			{
				this.Order.InDept = value;
				// TODO:  ��� ExecOrder.InDept setter ʵ��
			}
		}

		public NeuObject ExeDept
		{
			get
			{
				// TODO:  ��� ExecOrder.ExeDept getter ʵ��
				return this.Order.ExeDept;
			}
			set
			{
				this.Order.ExeDept=value;
				// TODO:  ��� ExecOrder.ExeDept setter ʵ��
			}
		}

		public NeuObject ReciptDept
		{
			get
			{
				// TODO:  ��� ExecOrder.ReciptDept getter ʵ��
				return ChargeDept;
			}
			set
			{
				// TODO:  ��� ExecOrder.ReciptDept setter ʵ��
				ChargeDept =value;
			}
		}

		public NeuObject NurseStation
		{
			get
			{
				// TODO:  ��� ExecOrder.NurseStation getter ʵ��
				return this.Order.Patient.PVisit.PatientLocation.NurseCell;
			}
			set
			{
				this.Order.Patient.PVisit.PatientLocation.NurseCell=value;
				// TODO:  ��� ExecOrder.NurseStation setter ʵ��
			}
		}

		public NeuObject StockDept
		{
			get
			{
				// TODO:  ��� ExecOrder.StockDept getter ʵ��
				return this.Order.StockDept;
			}
			set
			{
				this.Order.StockDept=value;
				// TODO:  ��� ExecOrder.StockDept setter ʵ��
			}
		}

		public NeuObject DoctorDept
		{
			get
			{
				// TODO:  ��� ExecOrder.ReciptDoct getter ʵ��
				return this.Order.DoctorDept;
			}
			set
			{
				// TODO:  ��� ExecOrder.ReciptDoct setter ʵ��
				this.Order.DoctorDept =value;
			}
		}

		#endregion

		#region ICloneable ��Ա

		public new ExecOrder Clone()
		{
			// TODO:  ��� Order.Clone ʵ��
			ExecOrder obj=base.Clone() as ExecOrder;

			try{obj.Order =this.Order.Clone();}
			catch{};
			obj.DcExecUser=this.DcExecUser.Clone();
			obj.ExecUser=this.ExecUser.Clone();
			obj.ChargeUser=this.ChargeUser.Clone();
			obj.ChargeDept=this.ChargeDept.Clone();

			try{obj.InDept=this.InDept.Clone();}
			catch{};
			try{obj.ExeDept=this.ExeDept.Clone();}
			catch{};
			try{obj.NurseStation=this.NurseStation.Clone();}
			catch{};
			try{obj.ReciptDept=this.ReciptDept.Clone();}
			catch{};
			try{obj.DoctorDept=this.DoctorDept.Clone();}
			catch{};
			try{obj.StockDept=this.StockDept.Clone();}
			catch{};

			return obj;
		}

		#endregion
	}
}
