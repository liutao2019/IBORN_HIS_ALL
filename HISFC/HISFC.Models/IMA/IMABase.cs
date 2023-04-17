using System;

namespace FS.HISFC.Models.IMA
{
	/// <summary>
	/// [��������: ҩƷ�����ʿ��������]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-13]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	///  
	/// </summary>
    [Serializable]
    public class IMABase : FS.FrameWork.Models.NeuObject
	{
		public IMABase()
		{
			
		}


		#region ����

		/// <summary>
		/// ���������ʵ��
		/// </summary>
		private object imaItem = new object();

        /// <summary>
        /// ����Ȩ������ 0310 ��� 0320 ���� 
        /// </summary>
        private string class2Type;

		/// <summary>
		/// ϵͳ����
		/// </summary>
		private string systemType;

		/// <summary>
		/// Ȩ������
		/// </summary>
		private string privType;

		/// <summary>
		/// ������
		/// </summary>
		private string specialFlag;

		/// <summary>
		/// ���������
		/// </summary>
		private FS.FrameWork.Models.NeuObject stockDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ���״̬
		/// </summary>
		private string state;

		/// <summary>
		/// ��������Ϣ ����������Ա������
		/// </summary>
		private FS.HISFC.Models.IMA.IMAOperation operation = new IMAOperation();

		#endregion

		/// <summary>
		/// �����������Ŀʵ�� 
		/// </summary>
		public virtual object IMAItem
		{
			get
			{
				return this.imaItem;
			}
			set
			{
				this.imaItem = value;
			}
		}

        /// <summary>
        /// ����Ȩ������ 0310 ��� 0320 ����
        /// </summary>
        public string Class2Type
        {
            get
            {
                return this.class2Type;
            }
            set
            {
                this.class2Type = value;
            }
        }

		/// <summary>
		/// Ȩ������ ���� �û��Զ�������
		/// </summary>
		public string PrivType
		{
			get
			{
				return this.privType;
			}
			set
			{
				this.privType = value;
			}
		}

		/// <summary>
		/// ϵͳ���� ��Ȩ���ڵ�ϵͳ����
		/// </summary>
		public string SystemType
		{
			get
			{
				return this.systemType;
			}
			set
			{
				this.systemType = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string SpecialFlag
		{
			get
			{
				return this.specialFlag;
			}
			set
			{
				this.specialFlag = value;
			}
		}

		/// <summary>
		/// ���������
		/// </summary>
		public FS.FrameWork.Models.NeuObject StockDept
		{
			get
			{
				return this.stockDept;
			}
			set
			{
				this.stockDept = value;
			}
		}

		/// <summary>
		/// ���״̬
		/// </summary>
		public string State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		/// <summary>
		/// ��������Ϣ ��Ա������
		/// </summary>
		public FS.HISFC.Models.IMA.IMAOperation Operation
		{
			get
			{
				return this.operation;
			}
			set
			{
				this.operation = value;
			}
		}


		#region ����

		public new IMABase Clone()
		{
			IMABase imaBase = base.Clone() as IMABase;

			imaBase.StockDept = this.StockDept.Clone();
			imaBase.Operation = this.Operation.Clone();

			return imaBase;
		}


		#endregion
	}
}
