using System;

namespace FS.HISFC.Models.Fee.Inpatient
{


	/// <summary>
	/// BedFeeInfo ��ժҪ˵����
	/// </summary>
    /// 
    [System.Serializable]
	public class BedFee : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.ISort {

		public BedFee( ) {
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
