using System;

namespace FS.HISFC.Models.IMA
{
	/// <summary>
	/// [��������: ҩƷ�����ʿ��������� ����������� ���ɴ˼̳�]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// 
	/// </summary>
    [Serializable]
    public class IMAApplyBase : IMABase,Base.IValidState
	{
		public IMAApplyBase()
		{
			
		}


		#region ����

		/// <summary>
		/// �������
		/// </summary>
		private FS.FrameWork.Models.NeuObject applyDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ���뵥��
		/// </summary>
		private string billCode;

        /// <summary>
        /// ��Ч��״̬
        /// </summary>
        private Base.EnumValidState validState = FS.HISFC.Models.Base.EnumValidState.Valid;

		#endregion

		/// <summary>
		/// �������
		/// </summary>
		public FS.FrameWork.Models.NeuObject ApplyDept
		{
			get
			{
				return this.applyDept;
			}
			set
			{
				this.applyDept = value;
			}
		}

		/// <summary>
		/// ���뵥��
		/// </summary>
		public string BillNO
		{
			get
			{
				return this.billCode;
			}
			set
			{
				this.billCode = value;
			}
		}			

        #region IValidState ��Ա

        /// <summary>
        /// ��Ч��״̬  Valid ��Ч Invalid ��Ч Ignore ���Բ�����
        /// </summary>
        public FS.HISFC.Models.Base.EnumValidState ValidState
        {
            get
            {
                return this.validState;
            }
            set
            {
                this.validState = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>���ص�ǰʵ���ĸ���</returns>
        public new IMAApplyBase Clone()
        {
            IMAApplyBase imaApplyBase = base.Clone() as IMAApplyBase;

            imaApplyBase.ApplyDept = this.ApplyDept.Clone();

            return imaApplyBase;

        }


        #endregion		

        #region ��Ч����

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment applyOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��������
        /// </summary>
        private decimal applyQty;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment examOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��׼����
        /// </summary>
        private decimal approveQty;

        /// <summary>
        /// ��׼������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment approveOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        [System.Obsolete("�������� ����ΪOperation����", true)]
        public FS.HISFC.Models.Base.OperEnvironment ApplyOper
        {
            get
            {
                return this.applyOper;
            }
            set
            {
                this.applyOper = value;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        [System.Obsolete("�������� ����ΪOperation����", true)]
        public decimal ApplyQty
        {
            get
            {
                return this.applyQty;
            }
            set
            {
                this.applyQty = value;
            }
        }


        /// <summary>
        /// ����������Ϣ
        /// </summary>
        [System.Obsolete("�������� ����ΪOperation����", true)]
        public FS.HISFC.Models.Base.OperEnvironment ExamOper
        {
            get
            {
                return this.examOper;
            }
            set
            {
                this.examOper = value;
            }
        }


        /// <summary>
        /// ��׼����
        /// </summary>
        [System.Obsolete("�������� ����ΪOperation����", true)]
        public decimal ApproveQty
        {
            get
            {
                return this.approveQty;
            }
            set
            {
                this.approveQty = value;
            }
        }


        /// <summary>
        /// ��׼������Ϣ
        /// </summary>
        [System.Obsolete("�������� ����ΪOperation����", true)]
        public FS.HISFC.Models.Base.OperEnvironment ApproveOper
        {
            get
            {
                return this.approveOper;
            }
            set
            {
                this.approveOper = value;
            }
        }


        /// <summary>
        /// ������Ϣ
        /// </summary>
        [System.Obsolete("�������� ����ΪOperation����", true)]
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }


        #endregion
    }
}
