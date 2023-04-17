using System;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ҩƷ��ҩ����̨]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶����'
	///  />
	///  ID		����̨����
	///  Name	����̨����
	/// </summary>
    [Serializable]
    public class DrugControl: FS.FrameWork.Models.NeuObject 
	{
		public DrugControl() 
		{

		}
        
		#region ����

		/// <summary>
		/// ��ҩ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject myDept = new FS.FrameWork.Models.NeuObject() ;

		/// <summary>
		/// ��ҩ����
		/// </summary>
		private DrugBillClass myDrugBillClass = new DrugBillClass();

		/// <summary>
		/// ��ҩ������
		/// </summary>
		private DrugAttribute myDrugAttribute = new DrugAttribute();

		/// <summary>
		/// ��������
		/// </summary>
		private int mySendType;

		/// <summary>
		/// ��ʾ�ȼ�
		/// </summary>
		private int myShowLevel;

        /// <summary>
        /// �Ƿ��Զ���ӡ��ҩ��
        /// </summary>
        private bool isAutoPrint = false;

        /// <summary>
        /// �Ƿ��ӡ�����ǩ �ò���ֻ�Գ�Ժ��ҩ��ҩ��Ч
        /// </summary>
        private bool isPrintLabel = false;

        /// <summary>
        /// ��ҩ���Ƿ���ҪԤ�� ��ӡ�����ǩʱ���ֶ���Ч
        /// </summary>
        private bool isBillPreview = false;

		#endregion

		/// <summary>
		/// ��ҩ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Dept 
		{
			get
			{
				return this.myDept; 
			}
			set
			{
				this.myDept = value; 
			}
		}

		/// <summary>
		/// ��ҩ����
		/// </summary>
		public DrugAttribute DrugAttribute 
		{
			get
			{
				return this.myDrugAttribute; 
			}
			set
			{ 
				this.myDrugAttribute = value; 
			}
		}

		/// <summary>
		/// ��ҩ�����༯��
		/// </summary>
		public DrugBillClass DrugBillClass  
		{
			get
			{
				return this.myDrugBillClass;
			}
			set
			{ 
				this.myDrugBillClass = value;
			}
		}

		/// <summary>
		/// �˰�ҩ̨���յķ�������0ȫ����1���У�2��ʱ
		/// </summary>
		public int SendType 
		{
			get
			{
				return this.mySendType; 
			}
			set
			{
				this.mySendType = value;
			}
		}

		/// <summary>
		/// ��ʾ�ȼ�
		/// </summary>
		public int ShowLevel 
		{
			get 
			{
				return myShowLevel;
			}
			set 
			{
				myShowLevel = value;
			}
		}

        /// <summary>
        /// �Ƿ��Զ���ӡ��ҩ��
        /// </summary>
        public bool IsAutoPrint
        {
            get
            {
                return this.isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        /// <summary>
        /// �Ƿ��ӡ�����ǩ �ò���ֻ�Գ�Ժ��ҩ��ҩ��Ч
        /// </summary>
        public bool IsPrintLabel
        {
            get
            {
                return this.isPrintLabel;
            }
            set
            {
                this.isPrintLabel = value;
            }
        }

        /// <summary>
        /// ��ҩ���Ƿ���ҪԤ�� ��ӡ�����ǩʱ���ֶ���Ч
        /// </summary>
        public bool IsBillPreview
        {
            get
            {
                return this.isBillPreview;
            }
            set
            {
                this.isBillPreview = value;
            }
        }

        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        public string ExtendFlag;

        /// <summary>
        /// ��չ�ֶ�1
        /// </summary>
        public string ExtendFlag1;


		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>���ص�ǰʵ���ĸ���</returns>
		public new DrugControl Clone()
		{
			DrugControl drugControl = base.Clone() as DrugControl;

			drugControl.Dept = this.Dept.Clone();
			drugControl.DrugAttribute = this.DrugAttribute.Clone();
			drugControl.DrugBillClass = this.DrugBillClass.Clone();

			return drugControl;
		}
	}
}
