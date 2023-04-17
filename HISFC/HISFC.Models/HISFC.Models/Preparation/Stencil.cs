using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Preparation
{
	/// <summary>
	/// Stecil<br></br>
	/// [��������: �Ƽ���Ʒģ��ά��]<br></br>
	/// [�� �� ��: ]<br></br>
	/// [����ʱ��: 2006-09-14]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
    /// <˵��>
    ///     1��ID ��ˮ����
    /// </˵��>
	/// </summary>
    [Serializable]
    public class Stencil : FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Stencil()
		{

		}

		#region  ����

		/// <summary>
		/// ��Ʒ����
		/// </summary>
        private Pharmacy.Item drug = new Pharmacy.Item();

		/// <summary>
		/// ���
		/// </summary>
		private NeuObject type = new NeuObject();

		/// <summary>
		/// ��Ŀ
		/// </summary>
		private NeuObject item = new NeuObject();

		/// <summary>
		/// ģ����� 0 ����ģ�� 1 �������� 2 ����
		/// </summary>
		private EnumStencialType kind = EnumStencialType.SemiAssayStencial;
		
		/// <summary>
		/// ��ע
		/// </summary>
		private string mark;

		/// <summary>
		/// ��չ���
		/// </summary>
		private string extend;

		/// <summary>
		/// ����---�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment operEnv = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private EnumStencilItemType itemType = EnumStencilItemType.Person;

        /// <summary>
        /// ��׼ֵ������Сֵ
        /// </summary>
        private decimal standardMin;

        /// <summary>
        /// ��׼ֵ�������ֵ
        /// </summary>
        private decimal standardMax;

        /// <summary>
        /// ��׼��������
        /// </summary>
        private string standardDes;
		#endregion

		#region  ����

        /// <summary>
        /// ��׼ֵ������Сֵ
        /// </summary>
        public decimal StandardMin
        {
            get
            {
                return this.standardMin;
            }
            set
            {
                this.standardMin = value;
            }
        }

        /// <summary>
        /// ��׼ֵ�������ֵ
        /// </summary>
        public decimal StandardMax
        {
            get
            {
                return this.standardMax;
            }
            set
            {
                this.standardMax = value;
            }
        }

        /// <summary>
        /// ��׼��������
        /// </summary>
        public string StandardDes
        {
            get
            {
                return this.standardDes;
            }
            set
            {
                this.standardDes = value;
            }
        }

		/// <summary>
		/// ��Ʒ����
		/// </summary>
        public Pharmacy.Item Drug
		{
			get
			{
				return this.drug;
			}
			set
			{
				this.drug = value;
			}
		}
		
		/// <summary>
		/// ���
		/// </summary>
		public NeuObject Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>
		/// ��Ŀ
		/// </summary>
		public NeuObject Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		/// <summary>
		/// ����---�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment OperEnv
		{
			get
			{
				return this.operEnv;
			}
			set
			{
				this.operEnv = value;
			}
		}

		/// <summary>
		/// ��չ���
		/// </summary>
		public string Extend
		{
			get
			{
				return this.extend;
			}
			set
			{
				this.extend = value;
			}
		}

		/// <summary>
		/// ģ����� 0 ����ģ�� 1 �������� 2 ����
		/// </summary>
		public EnumStencialType Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        public EnumStencilItemType ItemType
        {
            get
            {
                return this.itemType;
            }
            set
            {
                this.itemType = value;
            }
        }
		#endregion

		#region ����

		/// <summary>
		/// ���ƶ���
		/// </summary>
		/// <returns>Stencil</returns>
		public new Stencil Clone()
		{
			Stencil stencil = base.Clone() as Stencil;
			stencil.item = this.item.Clone();
			stencil.type = this.type.Clone();
			stencil.drug = this.drug.Clone();
			stencil.operEnv = this.operEnv.Clone();
			return stencil;
		}

		#endregion

		#region  ���ڵ����Ժ��ֶ�
		
		/// <summary>
		/// ��ע
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��NeuObject������", true)]
		public string Mark
		{
			get
			{
				return this.mark;
			}
			set
			{
				this.mark = value;
			}
		}
		/// <summary>
		/// ��չ���
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��Extend", true)]
		public string ExtFlag
		{
			get
			{
				return this.extend;
			}
			set
			{
				this.extend = value;
			}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��OperEnv", true)]
		public string OperCode;
		/// <summary>
		/// ��������
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��OperEnv", true)]
		public DateTime OperDate;
		#endregion
	}
}