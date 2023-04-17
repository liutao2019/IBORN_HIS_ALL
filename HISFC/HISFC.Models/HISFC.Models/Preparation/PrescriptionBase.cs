using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Preparation
{
    /// <summary>
    /// Prescription<br></br>
    /// [��������: ��Ʒ�����䷽����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-05-13]<br></br>
    /// <˵��
    ///		ID��Name �洢 ��Ʒ��Ŀ���롢����
    ///  />
    /// </summary>
    [Serializable]
    public class PrescriptionBase : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
		/// ���캯��
		/// </summary>
        public PrescriptionBase()
		{
		}

		#region  ����

        /// <summary>
        /// ��Ʒ���
        /// </summary>
        private string productSpecs;

        /// <summary>
        /// ��Ʒ�������
        /// </summary>
        private Base.EnumItemType itemType = FS.HISFC.Models.Base.EnumItemType.Drug;

        /// <summary>
        /// ԭ�������
        /// </summary>
        private EnumMaterialType materialType = EnumMaterialType.Material;

		/// <summary>
		/// ԭ��
		/// </summary>
        private FS.FrameWork.Models.NeuObject material = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���
        /// </summary>
        private string specs;

        /// <summary>
        /// ԭ�ϰ�װ����
        /// </summary>
        private decimal materialPackQty;

        /// <summary>
        /// ԭ�ϵ���
        /// </summary>
        private decimal price;

		/// <summary>
		/// ��׼������
		/// </summary>
		private decimal normativeQty;

		/// <summary>
		/// ԭ�ϵ�λ
		/// </summary>
		private string normativeUnit;

		/// <summary>
		/// ����--�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment operEnv = new FS.HISFC.Models.Base.OperEnvironment();
		#endregion

		#region  ����

        /// <summary>
        /// ��Ʒ���
        /// </summary>
        public string ProductSpecs
        {
            get
            {
                return this.productSpecs;
            }
            set
            {
                this.productSpecs = value;
            }
        }

        /// <summary>
        /// ��Ʒ�������
        /// </summary>
        public Base.EnumItemType ItemType
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

        /// <summary>
        /// ԭ�������
        /// </summary>
        public EnumMaterialType MaterialType
        {
            get
            {
                return this.materialType;
            }
            set
            {
                this.materialType = value;
            }
        }
        
		/// <summary>
		/// ԭ��
		/// </summary>
        public FS.FrameWork.Models.NeuObject Material
		{
			get
			{
				return this.material;
			}
			set
			{
				this.material = value;
			}
		}

        /// <summary>
        /// ���
        /// </summary>
        public string Specs
        {
            get
            {
                return this.specs;
            }
            set
            {
                this.specs = value;
            }
        }

        /// <summary>
        /// ԭ�ϰ�װ����
        /// </summary>
        public decimal MaterialPackQty
        {
            get
            {
                return this.materialPackQty;
            }
            set
            {
                this.materialPackQty = value;
            }
        }

        /// <summary>
        /// ԭ�ϵ���
        /// </summary>
        public decimal Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
            }
        }

		/// <summary>
		/// ��׼������
		/// </summary>
		public decimal NormativeQty
		{
			get
			{
				return this.normativeQty;
			}
			set
			{
				this.normativeQty = value;
			}
		}

		/// <summary>
		/// ԭ�ϵ�λ
		/// </summary>
		public string NormativeUnit
		{
			get
			{
				return this.normativeUnit;
			}
			set
			{
				this.normativeUnit = value;
			}
		}

		/// <summary>
		/// ����--�ˣ�����
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

		#endregion

		#region ����

		/// <summary>
		/// ���ƶ���
		/// </summary>
        /// <returns>PrescriptionBase</returns>
        public new PrescriptionBase Clone()
		{
            PrescriptionBase prescription = base.Clone() as PrescriptionBase;

			prescription.material = this.material.Clone();
			prescription.operEnv = this.operEnv.Clone();
			return prescription;
		}
		#endregion
    }
}
