using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ҩƷ������ҳ���ά��]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��˹'
	///		�޸�ʱ��='2006-09-12'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� '
	///  />
	///  ID �������
    /// 
    /// 
	/// </summary>
    [Serializable]
    public class DeptConstant :  FS.FrameWork.Models.NeuObject 
	{
		public DeptConstant() 
		{

		}


		#region ����

		/// <summary>
		/// ��߿����(��)
		/// </summary>
		private System.Int32 myStoreMaxDays  = 1;

		/// <summary>
		/// ��Ϳ����(��)
		/// </summary>
		private System.Int32 myStoreMinDays  = 1;

		/// <summary>
		/// �ο�����
		/// </summary>
		private System.Int32 myReferenceDays = 1;

		/// <summary>
		/// �Ƿ����Ź���ҩƷ
		/// </summary>
		private System.Boolean myIsBatch = false;

		/// <summary>
		/// �Ƿ������
		/// </summary>
		private System.Boolean myIsStore = false;

		/// <summary>
		/// ������Ĭ�ϵ�λ 1 ��װ��λ 0 ��С��λ
		/// </summary>
		private System.String myUnitFlag   = "0";

        /// <summary>
        /// �Ƿ�ҩ�����
        /// </summary>
        private bool myIsArk = false;


        /// <summary>
        /// ��ǰ����������ⵥ�ݺ�
        /// </summary>
        private string inListNO;

        /// <summary>
        /// ��ǰ�������ó��ⵥ�ݺ�
        /// </summary>
        private string outListNO;
		#endregion

		/// <summary>
		/// �ⷿ��߿����(��)
		/// </summary>
		public System.Int32 StoreMaxDays 
		{
			get
			{
				return this.myStoreMaxDays; 
			}
			set
			{ 
				this.myStoreMaxDays = value; 
			}
		}


		/// <summary>
		/// �ⷿ��Ϳ����(��)
		/// </summary>
		public System.Int32 StoreMinDays 
		{
			get
			{
				return this.myStoreMinDays; 
			}
			set
			{ 
				this.myStoreMinDays = value; 
			}
		}


		/// <summary>
		/// �ο�����
		/// </summary>
		public System.Int32 ReferenceDays 
		{
			get
			{
				return this.myReferenceDays;
			}
			set
			{ 
				this.myReferenceDays = value; 
			}
		}


		/// <summary>
		/// �Ƿ����Ź���ҩƷ
		/// </summary>
		public System.Boolean IsBatch 
		{
			get
			{
				return this.myIsBatch;
			}
			set
			{
				this.myIsBatch = value; 
			}
		}


		/// <summary>
		/// �Ƿ����ҩƷ���
		/// </summary>
		public System.Boolean IsStore 
		{
			get
			{
				return this.myIsStore;
			}
			set
			{
				this.myIsStore = value;
			}
		}


		/// <summary>
		/// ������ʱĬ�ϵĵ�λ��1��װ��λ��0��С��λ
		/// </summary>
		public System.String UnitFlag 
		{
			get
			{
				return this.myUnitFlag; 
			}
			set
			{
				this.myUnitFlag = value; 
			}
		}

        /// <summary>
        /// �Ƿ�ҩ�����
        /// </summary>
        public bool IsArk
        {
            get
            {
                return this.myIsArk;
            }
            set
            {
                this.myIsArk = value;
            }
        }

        /// <summary>
        /// ��ǰ����������ⵥ�ݺ�  {59C9BD46-05E6-43f6-82F3-C0E3B53155CB} 
        /// </summary>
        public string InListNO
        {
            get
            {
                return this.inListNO;
            }
            set
            {
                this.inListNO = value;
            }
        }

        /// <summary>
        /// ��ǰ�������ó��ⵥ�ݺ�  {59C9BD46-05E6-43f6-82F3-C0E3B53155CB} 
        /// </summary>
        public string OutListNO
        {
            get
            {
                return this.outListNO;
            }
            set
            {
                this.outListNO = value;
            }
        }


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>���ص�ǰʵ���ĸ���</returns>
		public new DeptConstant Clone()
		{
			DeptConstant deptConstant = base.Clone() as DeptConstant;

			return deptConstant;
		}


		#endregion
	}
}
