using System;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// ComExtInfo<br></br>
	/// [��������: ��չ����]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    [System.Obsolete("�Ѿ������ˣ�����ExtendInfo������Щ������!",false)]
	public class ComExtInfo : FS.FrameWork.Models.NeuObject
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public ComExtInfo()
		{
		}


		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// ��չ���Ա���
		/// </summary>
		private FS.FrameWork.Models.NeuObject item = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ���Ա���
		/// </summary>
		private string propertyCode;

		/// <summary>
		/// ��������
		/// </summary>
		private string propertyName ;

		/// <summary>
		/// �ַ�����
		/// </summary>
		private string stringProperty = "" ;

		/// <summary>
		/// ��ֵ����
		/// </summary>
		private decimal numberProperty = 0;

		/// <summary>
		/// ��������
		/// </summary>
		private System.DateTime dateProperty = DateTime.MinValue;

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private OperEnvironment operEnvironment = new OperEnvironment();

		#endregion

		#region ����

		/// <summary>
		/// ��չ���Ա���
		/// </summary>
		public FS.FrameWork.Models.NeuObject Item 
		{
			get
			{ 
				return this.item; 
			}
			set
			{
				this.item = value;
				this.ID = value.ID;
			}
		}

		/// <summary>
		/// ���Ա���
		/// </summary>
		public string PropertyCode 
		{
			get
			{ 
				return this.propertyCode; 
			}
			set
			{ 
				this.propertyCode = value; 
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string PropertyName 
		{
			get
			{ 
				return this.propertyName; 
			}
			set
			{
				this.propertyName = value; 
			}
		}

		/// <summary>
		/// �ַ�����
		/// </summary>
		public string StringProperty 
		{
			get
			{ 
				return this.stringProperty; 
			}
			set
			{
				this.stringProperty = value; 
				this.Name = value;
			}
		}

		/// <summary>
		/// ��ֵ����
		/// </summary>
		public System.Decimal NumberProperty 
		{
			get
			{ 
				return this.numberProperty; 
			}
			set
			{
				this.numberProperty = value; 
				this.Name = value.ToString();
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.DateTime DateProperty 
		{
			get
			{ 
				return this.dateProperty; 
			}
			set
			{
				this.dateProperty = value; 
				this.Name = value.ToString();
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public OperEnvironment OperEnvironment
		{
			get
			{ 
				return this.operEnvironment;
			}
			set
			{ 
				this.operEnvironment = value; 
			}
		}

		#endregion

		#region ����

		#region �ͷ�

		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing"></param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}
		
			if (this.item != null)
			{
				this.item.Dispose();
				this.item = null;
			}

			if (this.operEnvironment != null)
			{
				this.operEnvironment.Dispose();
				this.operEnvironment = null;
			}

			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}
		#endregion

		#region ��¡

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>ComExtInfo��ʵ��</returns>
		public new ComExtInfo Clone()
		{
			ComExtInfo comExtInfo = base.Clone() as ComExtInfo;

			comExtInfo.Item = this.Item.Clone();
			comExtInfo.OperEnvironment = this.OperEnvironment.Clone();

			return comExtInfo;
		}

		#endregion

		#endregion

		

	}
}
