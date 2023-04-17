using System;

namespace FS.HISFC.Models.IMA
{
	/// <summary>
	/// [��������: ҩƷ�����ʿ����������]<br></br>
	/// [�� �� ��: ������]<br></br>
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
	public class IMAOperation : FS.FrameWork.Models.NeuObject
	{
		public IMAOperation()
		{
			
		}


		#region ����

		/// <summary>
		/// ��������
		/// </summary>
		private decimal applyQty;

		/// <summary>
		/// ���������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment applyOper = new FS.HISFC.Models.Base.OperEnvironment();

		/// <summary>
		/// ��������
		/// </summary>
		private decimal examQty;
	
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
		/// �˿�����
		/// </summary>
		private decimal returnNum;

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

		#endregion

		/// <summary>
		/// ��������
		/// </summary>
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
		/// ���������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment ApplyOper
		{
			get
			{
				return this.applyOper;
			}
			set
			{
				this.applyOper = value;

				if (value != null)
				{
					base.ID = value.ID;
					base.Name = value.Name;
				}
			}
		}


		/// <summary>
		/// �������
		/// </summary>
		public decimal ExamQty
		{
			get
			{
				return this.examQty;
			}
			set
			{
				this.examQty = value;
			}
		}


		/// <summary>
		/// ����������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment ExamOper 
		{
			get
			{
				return this.examOper;
			}
			set
			{
				this.examOper = value;

				if (value != null)
				{
					base.ID = value.ID;
					base.Name = value.Name;
				}
			}
		}


		/// <summary>
		/// ��׼����
		/// </summary>
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
		public FS.HISFC.Models.Base.OperEnvironment ApproveOper
		{
			get
			{
				return this.approveOper;
			}
			set
			{
				this.approveOper = value;

				if (value != null)
				{
					base.ID = value.ID;
					base.Name = value.Name;
				}
			}
		}


		/// <summary>
		/// �˿�����
		/// </summary>
		public decimal ReturnQty
		{
			get
			{
				return this.returnNum;
			}
			set
			{
				this.returnNum = value;
			}
		}


		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;

				if (value != null)
				{
					base.ID = value.ID;
					base.Name = value.Name;
				}
			}
		}


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>���ص�ǰʵ���ĸ���</returns>
		public new IMAOperation Clone()
		{
			IMAOperation imaOperation = base.Clone() as IMAOperation;
			
			imaOperation.ApplyOper = this.ApplyOper.Clone();
			imaOperation.ExamOper = this.ExamOper.Clone();
			imaOperation.ApproveOper = this.ApproveOper.Clone();
			imaOperation.Oper = this.Oper.Clone();

			return imaOperation;

		}


		#endregion
	}
}
