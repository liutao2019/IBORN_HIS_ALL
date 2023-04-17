using System;

namespace neusoft.HISFC.Object.InterfaceSi
{
	/// <summary>
	/// DeptSICost ��ժҪ˵����
	/// </summary>
	public class DeptSICost : neusoft.neuFC.Object.neuObject
	{
		public DeptSICost()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private string month;
		private decimal alertMoney;
		private string validStateId;
		private string validStateName;
		private int sortId;
		private neusoft.neuFC.Object.neuObject operInfo = new neusoft.neuFC.Object.neuObject();
		private DateTime operDate;
		private Base.SpellCode spellCode = new neusoft.HISFC.Object.Base.SpellCode();
		/// <summary>
		/// �·�
		/// </summary>
		public string Month
		{
			set{month = value;}
			get{return month;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public decimal AlertMoney
		{
			set{alertMoney = value;}
			get{return alertMoney;}
		}
		/// <summary>
		/// ��Ч������
		/// </summary>
		public string ValidStateName
		{
			get{return validStateName;}
			set
			{
				validStateName = value;
				switch(validStateName)
				{
					case "����":
						validStateId = "0";
						break;
					case "ͣ��":
						validStateId = "1";
						break;
					case "����":
						validStateId = "2";
						break;
					default:
						validStateId = "0";
						break;
				}
			}
		}

		/// <summary>
		/// ��Ч��Id
		/// </summary>
		public string ValidStateId
		{
			set
			{
				validStateId = value;
				switch(validStateId)
				{
					case "0":
						validStateName = "����";
						break;
					case "1":
						validStateName = "ͣ��";
						break;
					case "2":
						validStateName = "����";
						break;
					default:
						validStateName = "����";
						break;
				}
			}
			get{return validStateId;}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int SortId
		{
			set{sortId = value;}
			get{return sortId;}
		}
		/// <summary>
		/// ����Ա��Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperInfo
		{
			set{operInfo = value;}
			get{return operInfo;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			set{operDate = value;}
			get{return operDate;}
		}
		/// <summary>
		/// ��������Ϣ
		/// </summary>
		public Base.SpellCode SpellCode
		{
			set{spellCode = value;}
			get{return spellCode;}
		}
		/// <summary>
		/// ��¡ʵ��
		/// </summary>
		/// <returns></returns>
		public new DeptSICost Clone()
		{
			DeptSICost obj = base.Clone() as DeptSICost;
			obj.operInfo = this.operInfo.Clone();
			obj.spellCode = this.spellCode.Clone();

			return obj;
		}
	}
}
