using System;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Terminal
{
	/// <summary>
	/// MedTechBookApply <br></br>
	/// [��������: ��������ҽ��ԤԼ��Ϣ]<br></br>
	/// [�� �� ��: sunxh]<br></br>
	/// [����ʱ��: 2005-3-3]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class MedTechBookApply : FS.FrameWork.Models.NeuObject
	{
		public MedTechBookApply()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����
		
		/// <summary>
		/// �����
		/// </summary>
		private int sortID;
		
		/// <summary>
		/// ���
		/// </summary>
		private FS.FrameWork.Models.NeuObject noon = new NeuObject();
		
		/// <summary>
		/// �������ʵ��
		/// </summary>
		private FS.HISFC.Models.Fee.Outpatient.FeeItemList itemList = new FeeItemList();
		
		/// <summary>
		/// ��ĿԤԼ��չ��Ϣ
		/// </summary>
		private ItemExtend itemExtend = new ItemExtend();
		
		/// <summary>
		/// ԤԼ��Ϣ
		/// </summary>
		private MedTechBookInfo medTechBookInfo = new MedTechBookInfo();

		/// <summary>
		/// ����״��
		/// </summary>
		private string healthFlag = "";
		
		/// <summary>
		/// ִ�еص�
		/// </summary>
		private FS.FrameWork.Models.NeuObject execLocate = new NeuObject();
		
		/// <summary>
		/// ȡ����ʱ��
		/// </summary>
		private System.DateTime reportTime = new DateTime();
		
		/// <summary>
		/// ��Ŀ��Ӧ��������ϸ��Ŀ���ƣ������B��10��
		/// </summary>
		private FS.FrameWork.Models.NeuObject itemComparison = new NeuObject();

        private int arrangeQty;//�Ѱ�������
		#endregion

		#region ����

		/// <summary>
		/// ���
		/// </summary>
		public FS.FrameWork.Models.NeuObject Noon
		{
			get
			{
				return this.noon;
			}
			set
			{
				this.noon = value;
			}
		}

		/// <summary>
		/// �������ʵ��
		/// </summary>
		/// <returns></returns>
		public FS.HISFC.Models.Fee.Outpatient.FeeItemList ItemList
		{
			get
			{
				return this.itemList;
			}
			set
			{
				this.itemList = value;
			}
		}

		/// <summary>
		/// ��ĿԤԼ��չ��Ϣ
		/// </summary>
		/// <returns></returns>
		public ItemExtend ItemExtend
		{
			get
			{
				return this.itemExtend;
			}
			set
			{
				this.itemExtend = value;
			}
		}

		/// <summary>
		/// ԤԼ��Ϣ
		/// </summary>
		/// <returns></returns>
		public MedTechBookInfo MedTechBookInfo
		{
			get
			{
				return this.medTechBookInfo;
			}
			set
			{
				this.medTechBookInfo = value;
			}
		}

		/// <summary>
		/// ����״��
		/// </summary>
		public string HealthFlag
		{
			get
			{
				return this.healthFlag;
			}
			set
			{
				this.healthFlag = value;
			}
		}
		
		/// <summary>
		/// ִ�еص�
		/// </summary>
		public FS.FrameWork.Models.NeuObject ExecLocate
		{
			get
			{
				return this.execLocate;
			}
			set
			{
				this.execLocate = value;
			}
		}
		
		/// <summary>
		/// ȡ����ʱ��
		/// </summary>
		public System.DateTime ReportTime
		{
			get
			{
				return this.reportTime;
			}
			set
			{
				this.reportTime = value;
			}
		}
		
		/// <summary>
		/// ҽ����ˮ��                   
		/// </summary>
		public string MoOrder
		{
			get
			{
				return this.ItemList.Order.ID;
			}
			set
			{
				this.ItemList.Order.ID = value;
			}
		}

		/// <summary>
		/// �����
		/// </summary>
		/// <returns></returns>
		public int SortID
		{
			get
			{
				return sortID;
			}
			set
			{
				sortID = value;
			}

		}
		
		/// <summary>
		/// ��Ŀ��Ӧ��������ϸ��Ŀ���ƣ������B��10��
		/// </summary>
		public NeuObject ItemComparison
		{
			get
			{
				return this.itemComparison;
			}
			set
			{
				this.itemComparison = value;
			}
		}
        /// <summary>
        /// �Ѱ�������
        /// </summary>
        public int ArrangeQty
        {
            get
            {
                return arrangeQty;
            }
            set
            {
                arrangeQty = value;
            }
        }
		#endregion

		#region ��ʱ

		/// <summary>
		/// ȡ����ʱ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪReportTime", true)]
		public System.DateTime ReportDate;
		
		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new MedTechBookApply Clone()
		{
			MedTechBookApply medTechBookApply = base.Clone() as MedTechBookApply;

			medTechBookApply.Noon = this.Noon.Clone();
			medTechBookApply.ItemList = this.ItemList.Clone();
			medTechBookApply.ItemExtend = this.ItemExtend.Clone();
			medTechBookApply.MedTechBookInfo = this.MedTechBookInfo.Clone();
			medTechBookApply.ExecLocate = this.ExecLocate.Clone();

			return medTechBookApply;
		}
		
		#endregion
	}
}
