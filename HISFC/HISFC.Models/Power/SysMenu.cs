using System;

namespace neusoft.HISFC.Object.Power
{

	public class SysMenu: neusoft.neuFC.Object.neuObject
	{
		private System.String pargrpCode ;
		private System.String curgrpCode ;
		private System.Int32 x ;
		private System.Int32 y ;
		private System.String menuName ;
		private System.String shortCut ;
		private System.String icon ;
		private System.Boolean enabled ;
		private System.String sysCode ;
		private System.String menuWin ;
		private System.String menuParm ;
		private System.Boolean ownerFlag ;

		private System.Int32 newX ;
		private System.Int32 newY ;
		/// <summary>
		/// ����������
		/// </summary>
		public System.String PargrpCode
		{
			get
			{
				return this.pargrpCode;
			}
			set
			{
				this.pargrpCode = value;
			}
		}

		/// <summary>
		/// ����������
		/// </summary>
		public System.String CurgrpCode
		{
			get
			{
				return this.curgrpCode;
			}
			set
			{
				this.curgrpCode = value;
				this.ID = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.Int32 X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
				this.newX = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public System.Int32 Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
				this.newY = value;
			}
		}

		/// <summary>
		/// �˵�����
		/// </summary>
		public System.String MenuName
		{
			get
			{
				return this.menuName;
			}
			set
			{
				this.menuName = value;
			}
		}

		/// <summary>
		/// ��ݷ�ʽ
		/// </summary>
		public System.String ShortCut
		{
			get
			{
				return this.shortCut;
			}
			set
			{
				this.shortCut = value;
			}
		}

		/// <summary>
		/// ͼ��
		/// </summary>
		public System.String Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}

		/// <summary>
		/// �Ƿ����
		/// </summary>
		public System.Boolean Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}
		/// <summary>
		/// ģ���������
		/// </summary>
		public SysModelFunction ModelFuntion = new SysModelFunction();

		/// <summary>
		/// ģ�����
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
		/// ���ô���
		/// </summary>
		public System.String MenuWin
		{
			get
			{
				return this.menuWin;
			}
			set
			{
				this.menuWin = value;
			}
		}

		/// <summary>
		/// ���ò���
		/// </summary>
		public System.String MenuParm
		{
			get
			{
				return this.menuParm;
			}
			set
			{
				this.menuParm = value;
			}
		}

		/// <summary>
		/// �����־
		/// </summary>
		public System.Boolean OwnerFlag
		{
			get
			{
				return this.ownerFlag;
			}
			set
			{
				this.ownerFlag = value;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public System.Int32 NewX
		{
			get
			{
				return this.newX;
			}
			set
			{
				this.newX = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public System.Int32 NewY
		{
			get
			{
				return this.newY;
			}
			set
			{
				this.newY = value;
			}
		}


		private SysMenu[] menus ;
		public SysMenu[] Children
		{
			get
			{
				return menus;
			}
			set
			{
				menus = value;
			}
		
		}

		public new neusoft.HISFC.Object.Power.SysMenu Clone()
		{
			return (neusoft.HISFC.Object.Power.SysMenu)this.MemberwiseClone();
		}

	}

}