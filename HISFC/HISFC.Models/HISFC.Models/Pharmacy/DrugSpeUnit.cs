using System;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ���ⵥλ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    [Serializable]
    public class DrugSpeUnit : FS.FrameWork.Models.NeuObject
	{
		public DrugSpeUnit()
		{
			
		}


		#region ˽�б���
		/// <summary>
		/// ҩƷ��Ϣ
		/// </summary>
		private FS.HISFC.Models.Pharmacy.Item item = new Item();
		/// <summary>
		/// ���ⵥλ���
		/// </summary>
		private FS.FrameWork.Models.NeuObject unitType = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// ���ⵥλ
		/// </summary>
		private string unit;
		/// <summary>
		/// ���ⵥλ�������� (1���ⵥλ������С��λ����)
		/// </summary>
		private decimal num;
		/// <summary>
		/// ���յ�λ��־ 0 ��С��λ 1 ��װ��λ
		/// </summary>
		private string unitFlag = "0";
		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		private string extFlag;
		/// <summary>
		/// ��չ�ֶ�1
		/// </summary>
		private string extFlag1;
		/// <summary>
		/// ����������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
		#endregion

		/// <summary>
		/// ҩƷ��Ϣ
		/// </summary>
		public FS.HISFC.Models.Pharmacy.Item Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
				if (value != null)
					this.ID = value.ID;
			}
		}


		/// <summary>
		/// ���ⵥλ���
		/// </summary>
		public FS.FrameWork.Models.NeuObject UnitType
		{
			get
			{
				return this.unitType;
			}
			set
			{
				this.unitType = value;
			}
		}


		/// <summary>
		/// ���ⵥλ
		/// </summary>
		public string Unit
		{
			get
			{
				return this.unit;
			}
			set
			{
				this.unit = value;
				this.Name = value;
			}
		}


		/// <summary>
		/// ���ⵥλ�������� (1���ⵥλ������С��λ����)
		/// </summary>
		public decimal Qty
		{
			get
			{
				return this.num;
			}
			set
			{
				this.num = value;
				this.Memo = value.ToString();
			}
		}


		/// <summary>
		/// ���յ�λ��־ 0 ��С��λ 1 ��װ��λ  �ñ�־��Ӱ���������
		/// </summary>
		public string UnitFlag
		{
			get
			{
				return this.unitFlag;
			}
			set
			{
				this.unitFlag = value;
			}
		}


		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		public string Extend
		{
			get
			{
				return this.extFlag;
			}
			set
			{
				this.extFlag = value;
			}
		}


		/// <summary>
		/// ��չ�ֶ�1
		/// </summary>
		public string Extend1
		{
			get
			{
				return this.extFlag1;
			}
			set
			{
				this.extFlag1 = value;
			}
		}


		/// <summary>
		/// ����������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment Oper
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


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new DrugSpeUnit Clone()
		{
			DrugSpeUnit drugSpeUnit = base.Clone() as DrugSpeUnit;

			drugSpeUnit.Item = this.Item.Clone();
			drugSpeUnit.UnitType = this.UnitType.Clone();
			drugSpeUnit.Oper = this.Oper.Clone();

			return drugSpeUnit;
		}


		#endregion

		#region ��Ч����

		/// <summary>
		/// ����Ա
		/// </summary>
		private string operCode;

		/// <summary>
		/// ����ʱ��
		/// </summary>
		private DateTime operDate;

		/// <summary>
		/// ���ⵥλ�������� (1���ⵥλ������С��λ����)
		/// </summary>
		[System.Obsolete("�������� ����ΪQty����",true)]
		public decimal Num
		{
			get
			{
				return this.num;
			}
			set
			{
				this.num = value;
				this.Memo = value.ToString();
			}
		}


		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		[System.Obsolete("�������� ����ΪExtend",true)]
		public string ExtFlag
		{
			get
			{
				return this.extFlag;
			}
			set
			{
				this.extFlag = value;
			}
		}


		/// <summary>
		/// ��չ�ֶ�1
		/// </summary>
		[System.Obsolete("�������� ����ΪExtend1",true)]
		public string ExtFlag1
		{
			get
			{
				return this.extFlag1;
			}
			set
			{
				this.extFlag1 = value;
			}
		}


		/// <summary>
		/// ����Ա
		/// </summary>
		[System.Obsolete("�������� ����ΪOper����",true)]
		public string OperCode
		{
			get
			{
				return this.operCode;
			}
			set
			{
				this.operCode = value;
			}
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�������� ����ΪOper����",true)]
		public DateTime OperDate
		{
			get
			{
				return this.operDate;
			}
			set
			{
				this.operDate = value;
			}
		}


		#endregion
	}
}
