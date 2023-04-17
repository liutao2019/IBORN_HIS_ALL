using System;

namespace Neusoft.HISFC.Models.Medical

{
	/// <summary>
	/// [��������: ������չʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ܰ'
	///		�޸�ʱ��='2008-04-08'
	///		�޸�Ŀ��='�洢��Ⱦ��λ�͸�Ⱦԭ����Ϣ'
	///		�޸�����=''
	///  />
	/// </summary>��
    [Serializable]
	public class Extend:Neusoft.FrameWork.Models.NeuObject
	{
		public Extend()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ����

		private System.String primaryId = "";
		private System.String type = "";
		private System.String valid = "";
		private System.String operCode = "";
		private System.DateTime operDate = System.DateTime.MinValue;
		private System.String dataType = "";
       #endregion

		#region ����
		/// <summary>
		/// ������
		/// </summary>
		public string PrimaryId
		{
			get
			{
				return this.primaryId;
			}
			set
			{
				this.primaryId = value;
			}
		}
		/// <summary>
		/// �������ݱ�
		/// </summary>
		public string DataType
		{
			get
			{
				return this.dataType;
			}
			set
			{
				this.dataType = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Type
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
		/// ��Ч״̬
		/// </summary>
		public string Valid
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
			}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		public string OperCode
		{
			get
			{
				return this.operCode;
			}
			set
			{
				this.operCode = value;
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			get
			{
				return this.operDate;
			}
			set
			{
				this.operDate = value;
			}
		}

		#endregion

	}


	/// <summary>
	/// ��������
	/// </summary>
	public enum DataType
	{
		/// <summary>
		/// ��������
		/// </summary>
		PertainDept = 1,
		/// <summary>
		/// ������Դ
		/// </summary>
		Source = 2,
		/// <summary>
		/// �μӵ�λ
		/// </summary>
		JoinDept = 3,
		/// <summary>
		/// ����
		/// </summary>
		Dept = 4,
		//			/// <summary>
		//			/// ר��
		//			/// </summary>
		//			SpecialDept,
		/// <summary>
		/// С���Ա
		/// </summary>
		Leaguer = 5       
	}
}
