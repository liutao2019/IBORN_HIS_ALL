using System;

namespace FS.HISFC.DCP.Object
{
	/// <summary>
	/// PatientAddress ��ժҪ˵����
	/// </summary>
	public class PatientAddress :FS.HISFC.Models.Base.Const
	{
		
		public PatientAddress()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        private string addr_Code;                                      //��ַ����
        private string addr_Name;                                        //��ַ����
		private string  senior_Address;    //�ϼ���ַ����
		private string  lever; //����

		/// <summary>
		/// ��ַ����
		/// </summary>
        public string Addr_Code
		{
			get
			{
                return this.addr_Code;
			}
			set
			{
                this.addr_Code = value;
			}
		}

		/// <summary>
		/// ��ַ����
		/// </summary>
        public string Addr_Name
		{
			get
			{
				return this.addr_Name;
			}
			set
			{
                this.addr_Name = value;
			}
		}


		/// <summary>
		/// �ϼ���ַ����
		/// </summary>
		public string Senior_Address
		{
			get
			{
				return this.senior_Address;
			}
			set
			{
                this.senior_Address = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Lever
		{
			get
			{
				return this.lever ;

			}
			set
			{
				this.lever =value;
			}
		}
	}
}