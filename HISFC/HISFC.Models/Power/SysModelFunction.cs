using System;

namespace neusoft.HISFC.Object.Power
{
	/// <summary>
	/// SysModelFunction ��ժҪ˵����
	/// </summary>
	public class SysModelFunction: neusoft.neuFC.Object.neuObject
	{
		private System.String sysCode ;
		private System.String winName ;
		private System.String funName ;
		private System.String mark ;
		private System.Int32 sortId ;

		/// <summary>
		/// ����ϵͳ
		/// </summary>
		public System.String SysCode
		{
			get
			{
				return this.sysCode;
			}
			set
			{
				this.sysCode = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.String WinName
		{
			get
			{
				return this.winName;
			}
			set
			{
				this.winName = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.String FunName
		{
			get
			{
				return this.funName;
			}
			set
			{
				this.funName = value;
			}
		}

		/// <summary>
		/// ��ע
		/// </summary>
		public System.String Mark
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
		/// ˳���
		/// </summary>
		public System.Int32 SortId
		{
			get
			{
				return this.sortId;
			}
			set
			{
				this.sortId = value;
			}
		}
		/// <summary>
		/// ������ʾ����
		/// </summary>
		public string FormShowType = "MDI";
		/// <summary>
		/// ��������
		/// </summary>
		public string FormType = "Form";
		/// <summary>
		/// ����
		/// </summary>
		public string Param ="";
		protected string strDllName ="";
		/// <summary>
		/// ��������
		/// </summary>
		public string DllName 
		{
			get
			{
				if(this.strDllName =="")
				{
					try
					{
						this.strDllName = this.WinName.Substring(0,this.WinName.IndexOf("."));
					}
					catch{}
				}
				return this.strDllName;
			}
			set
			{
				this.strDllName = value;
			}
		}
	}

}
