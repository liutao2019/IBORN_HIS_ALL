using System;


namespace FS.HISFC.Models.Admin {

    
	/// <summary>
	/// SysModelFunction ��ժҪ˵����
	/// </summary>
    /// 

    [System.Serializable]
	public class SysModelFunction: FS.FrameWork.Models.NeuObject
	{
		private System.String sysCode ;
		private System.String winName ;
		private System.String funName ;
		private System.String mark ;
		private System.Int32 sortId ;

        private SysModelFunction treeControl = null;

        /// <summary>
        /// ���ؼ�
        /// </summary>
        public SysModelFunction TreeControl
        {
            get
            {
                if (treeControl == null)
                    treeControl = new SysModelFunction();
                return this.treeControl;
            }
            set
            {
                this.treeControl = value;
            }
        }
        
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
        ///  Ψһ���� 
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
        [Obsolete("��Memo����")]
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
        /// ���
		/// </summary>
		public System.Int32 SortID
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
		public string FormShowType
        {
            get
            {
                if (frmShowType == "") frmShowType = "MDI";
                return frmShowType;
            }
            set
            {
                frmShowType = value;
            }
        }
        protected string frmShowType= "MDI";
       
		/// <summary>
		/// ��������
        /// Form ,Control,Report and so on
		/// </summary>
		public string FormType 
        {
            get
            {
                if (frmType == "") frmType = "Form";
                return frmType;
            }
            set
            {
                
                frmType = value;
            }
        }

        protected string frmType = "";
        
		/// <summary>
		/// ���� tag
		/// </summary>
        public string Param
        {
            get
            {
                return this.param;
            }
            set
            {
                this.param = value;
            }
        }
        protected string param = "";
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
