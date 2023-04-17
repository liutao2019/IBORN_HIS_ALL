using System;


namespace FS.HISFC.Models.Admin {

	/// <summary>
	/// ������<br></br>
	/// ID    �������ͱ���
	/// Name  ������������
	/// <Font color='#FF1111'>[��������: ]</Font><br></br>
	/// [�� �� ��: ]<br></br>
	/// [����ʱ��: ]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///		/>
	/// </summary>
    /// 
    [System.Serializable]
	public class ConstantGroup: FS.FrameWork.Models.NeuObject 
	{
		private System.String myPargrpCode ;
		private System.String myCurgrpCode ;
		private System.String myControlName ;
		private System.String myControlNote ;

		/// <summary>
		/// ����������
		/// </summary>
		public System.String PargrpCode {
			get {
				return this.myPargrpCode;
			}
			set {
				this.myPargrpCode = value;
			}
		}


		/// <summary>
		/// ����������
		/// </summary>
		public System.String CurgrpCode {
			get {
				return this.myCurgrpCode;
			}
			set {
				this.myCurgrpCode = value;
			}
		}


		/// <summary>
		/// �ؼ�����
		/// </summary>
		public System.String ControlName {
			get {
				return this.myControlName;
			}
			set {
				this.myControlName = value;
			}
		}


		/// <summary>
		/// �ؼ�ע��
		/// </summary>
		public System.String ControlNote {
			get {
				return this.myControlNote;
			}
			set {
				this.myControlNote = value;
			}
		}


//		public new FS.HISFC.Models.Power.SysMenu Clone() {
//			return (FS.HISFC.Models.Power.SysMenu)this.MemberwiseClone();
//		}

	}
}