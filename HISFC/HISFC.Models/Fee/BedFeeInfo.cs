using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// BedFeeInfo ��ժҪ˵����
	/// </summary>
	public class BedFeeInfo	: Neusoft.NFC.Object.NeuObject, Neusoft.HISFC.Object.Base.ISort
	{
		public BedFeeInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public string ItemCode;
		public string ItemName;
		public int  Number;
		public DateTime StartTime;
		public DateTime EndTime;
		public bool HasRelationToBaby;
		public bool HasRelationToTime;
		public bool ValidState;

		private int sortId;
		#region ISort ��Ա

		public int SortID
		{
			get
			{
				// TODO:  ��� BedFeeInfo.SortID getter ʵ��
				return sortId;
			}
			set
			{
				// TODO:  ��� BedFeeInfo.SortID setter ʵ��
				sortId = value;
			}
		}

		#endregion
	}
}
