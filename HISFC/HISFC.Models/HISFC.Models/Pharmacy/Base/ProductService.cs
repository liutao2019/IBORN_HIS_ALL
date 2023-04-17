using System;

namespace FS.HISFC.Models.Pharmacy.Base
{
	/// <summary>
	/// [��������: ҩƷ���ʲ�Ʒ������Ϣ]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-11]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class ProductService:FS.FrameWork.Models.NeuObject
	{
		public ProductService()
		{
		}


		#region ����

		/// <summary>
		/// ��������
		/// </summary>
		private FS.FrameWork.Models.NeuObject producer = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ���¹�����˾
		/// </summary>
		private FS.FrameWork.Models.NeuObject company = new FS.FrameWork.Models.NeuObject();	
	
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private string approvalInfo;

		/// <summary>
		/// ע���̱�
		/// </summary>
		private string label;

		/// <summary>
		/// ע������
		/// </summary>
		private string caution;

		/// <summary>
		/// ������
		/// </summary>
		private string barCode;

		/// <summary>
		/// ����
		/// </summary>
		private string producingArea;

		/// <summary>
		/// ���
		/// </summary>
		private string briefIntroduction;

		/// <summary>
		/// ˵��������
		/// </summary>
		private string manual;

		/// <summary>
		/// ���ͼƬ
		/// </summary>
		private System.Drawing.Image appearanceImage;

		/// <summary>
		/// ��������
		/// </summary>
		private string storeCondition;	

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		private bool isSelfMade;		

		#endregion

		/// <summary>
		/// ��������
		/// </summary>
		public FS.FrameWork.Models.NeuObject Producer
		{
			get
			{
				return this.producer;
			}
			set
			{
				this.producer = value;
			}
		}


		/// <summary>
		/// ���¹�����˾
		/// </summary>
		public FS.FrameWork.Models.NeuObject Company
		{
			get
			{
				return this.company;
			}
			set
			{
				this.company = value;
			}
		}
		

		/// <summary>
		/// ������Ϣ
		/// </summary>
		public string ApprovalInfo
		{
			get
			{
				return this.approvalInfo;
			}
			set
			{
				this.approvalInfo = value;
			}
		}


		/// <summary>
		/// ע���̱�
		/// </summary>
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				this.label = value;
			}
		}


		/// <summary>
		/// ע������
		/// </summary>
		public string Caution
		{
			get
			{
				return this.caution;
			}
			set
			{
				this.caution = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		public string BarCode
		{
			get
			{
				return this.barCode;
			}
			set
			{
				this.barCode = value;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		public string ProducingArea
		{
			get
			{
				return this.producingArea;
			}
			set
			{
				this.producingArea = value;
			}
		}


		/// <summary>
		/// ҩƷ���
		/// </summary>
		public string BriefIntroduction
		{
			get
			{
				return this.briefIntroduction;
			}
			set
			{
				this.briefIntroduction = value;
			}
		}


		/// <summary>
		/// ҩƷ˵��������
		/// </summary>
		public string Manual
		{
			get
			{
				return this.manual;
			}
			set
			{
				this.manual = value;
			}
		}
		

		/// <summary>
		/// ҩƷ���ͼƬ
		/// </summary>
		public System.Drawing.Image Image
		{
			get
			{
				return this.appearanceImage;
			}
			set
			{
				this.appearanceImage = value;
			}
		}		


		/// <summary>
		/// ��������
		/// </summary>
		public string StoreCondition
		{
			get
			{
				return this.storeCondition;
			}
			set
			{
				this.storeCondition = value;
			}
		}


		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public bool IsSelfMade
		{
			get
			{
				return this.isSelfMade;
			}
			set
			{
				this.isSelfMade = value;
			}
		}
		

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>�ɹ�����ProductSerice��¡��ʵ��</returns>
		public new ProductService Clone()
		{
			ProductService productS = base.Clone() as ProductService;

			productS.Producer = this.Producer.Clone();
			productS.Company = this.Company.Clone();

			return productS;
		}


		#endregion
	}
}
