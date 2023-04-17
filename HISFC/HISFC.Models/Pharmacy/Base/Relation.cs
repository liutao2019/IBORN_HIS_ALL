using System;

namespace FS.HISFC.Models.Pharmacy.Base
{
	/// <summary>
	/// [��������: ��ϵ��ʽ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Relation : FS.FrameWork.Models.NeuObject
	{
		public Relation()
		{
			
		}


		#region ����

		/// <summary>
		/// ��ַ
		/// </summary>
		private System.String address ;

		/// <summary>
		/// ��ϵ��
		/// </summary>
		private FS.FrameWork.Models.NeuObject linkMan = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ��ϵ�绰
		/// </summary>
		private string phone;

		/// <summary>
		/// Email��ַ
		/// </summary>
		private string email;

		/// <summary>
		/// ����
		/// </summary>
		private string faxCode = "";

		/// <summary>
		/// ��˾��ַ
		/// </summary>
		private string linkUrl;

		/// <summary>
		/// ��ʱͨѶ��ϵ����
		/// </summary>
		private string linkNo;

		/// <summary>
		/// ��ϵ��ʽ
		/// </summary>
		private System.String relative ;

		#endregion

		/// <summary>
		/// ��˾��ַ
		/// </summary>
		public System.String Address 
		{
			get
			{ 
				return this.address;
			}
			set
			{
				this.address = value; 
			}
		}


		/// <summary>
		/// ��ϵ��
		/// </summary>
		public FS.FrameWork.Models.NeuObject LinkMan
		{
			get
			{
				return this.linkMan;
			}
			set
			{
				this.linkMan = value;
			}
		}


		/// <summary>
		/// ��ϵ�绰
		/// </summary>
		public string Phone
		{
			get
			{
				return this.phone;
			}
			set
			{
				this.phone = value;
			}
		}


		/// <summary>
		/// Email��ַ
		/// </summary>
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		public string  FaxCode
		{
			get
			{
				return this.faxCode;
			}
			set
			{
				this.faxCode = value ;
			}
		}


		/// <summary>
		/// ��˾��ַ
		/// </summary>
		public string LinkUrl
		{
			get
			{
				return this.linkUrl;
			}
			set
			{
				this.linkUrl = value;
			}
		}


		/// <summary>
		/// ��ʱͨѶ��ϵ��ʽ
		/// </summary>
		public string LinkNO
		{
			get
			{
				return this.linkNo;
			}
			set
			{
				this.linkNo = value;
			}
		}


		/// <summary>
		/// ��ϵ��ʽ
		/// </summary>
		public System.String Relative 
		{
			get
			{
				return this.relative; 
			}
			set
			{ 
				this.relative = value;
			}
		}


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>���ص�ǰʵ������</returns>
		public new Relation Clone()
		{
			Relation relation = base.Clone() as Relation;

			relation.LinkMan = this.LinkMan.Clone();

			return relation;
		}


		#endregion


	}
}
