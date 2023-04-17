using System;

namespace neusoft.HISFC.Object.Power {

	/// <summary>
	/// ID    �������ͱ���
	/// Name  ������������
	/// </summary>
	public class ConstantGroup: neusoft.neuFC.Object.neuObject {
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


//		public new neusoft.HISFC.Object.Power.SysMenu Clone() {
//			return (neusoft.HISFC.Object.Power.SysMenu)this.MemberwiseClone();
//		}

	}

}