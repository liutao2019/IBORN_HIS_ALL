using System;


namespace FS.HISFC.Models.SIInterface {


	/// <summary>
	/// DeptSICost ��ժҪ˵����
	/// </summary>
    [Serializable]
    public class DeptSICost : FS.FrameWork.Models.NeuObject
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
		private FS.FrameWork.Models.NeuObject operInfo = new FS.FrameWork.Models.NeuObject();
		private DateTime operDate;
		private FS.HISFC.Models.Base.Spell spellCode = new FS.HISFC.Models.Base.Spell();
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
		public FS.FrameWork.Models.NeuObject OperInfo
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
		public FS.HISFC.Models.Base.Spell SpellCode {
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
