using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// ������黯����������Ӧ������
	/// </summary>
	public class CheckInfo
	{
		public CheckInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

	#region ˽�б���

		private CheckBaseInfo myCheckX = new CheckBaseInfo( "X��" );
		private CheckBaseInfo myCheckCT = new CheckBaseInfo( "CT" );
		private CheckBaseInfo myCheckMRI = new CheckBaseInfo( "MRI" );
		private CheckBaseInfo myCheckDSA = new CheckBaseInfo( "DSA" );
		private CheckBaseInfo myCheckPET = new CheckBaseInfo( "PET" );
		private CheckBaseInfo myCheckECT = new CheckBaseInfo( "ECT" );
		private CheckBaseInfo myCheckBL = new CheckBaseInfo( "����" );

	#endregion
		
	#region ����

		/// <summary>
		/// X����Ϣ
		/// </summary>
		public CheckBaseInfo CheckX
		{
			get
            { 
                return myCheckX; 
            }
			set
            { 
                myCheckX = value; 
            }
		}

		/// <summary>
		/// CT��Ϣ
		/// </summary>
		public CheckBaseInfo CheckCT
		{
			get
            { 
                return myCheckCT; 
            }
			set
            { 
                myCheckCT = value; 
            }
		}

		/// <summary>
		/// MRI��Ϣ
		/// </summary>
		public CheckBaseInfo CheckMRI
		{
			get
            { 
                return myCheckMRI; 
            }
			set
            { 
                myCheckMRI = value; 
            }
		}

		/// <summary>
		/// DSA��Ϣ
		/// </summary>
		public CheckBaseInfo CheckDSA
		{
			get
            { 
                return myCheckDSA; 
            }
			set
            { 
                myCheckDSA = value; 
            }
		}

		/// <summary>
		/// PET��Ϣ
		/// </summary>
		public CheckBaseInfo CheckPET
		{
			get
            { 
                return myCheckPET; 
            }
			set
            { 
                myCheckPET = value; 
            }
		}

		/// <summary>
		/// ECT��Ϣ
		/// </summary>
		public CheckBaseInfo CheckECT
		{
			get
            { 
                return myCheckECT; 
            }
			set
            { 
                myCheckECT = value; 
            }
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		public CheckBaseInfo CheckBL
		{
			get
            { 
                return myCheckBL; 
            }
			set
            { 
                myCheckBL = value; 
            }
		}

	#endregion

	#region ���к���
        
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
		public new CheckInfo Clone()
		{
			CheckInfo CheckInfoClone = base.MemberwiseClone() as CheckInfo;
			
			CheckInfoClone.CheckBL = this.CheckBL.Clone();
			CheckInfoClone.CheckCT = this.CheckCT.Clone();
			CheckInfoClone.CheckDSA = this.CheckDSA.Clone();
			CheckInfoClone.CheckECT = this.CheckECT.Clone();
			CheckInfoClone.CheckMRI = this.CheckMRI.Clone();
			CheckInfoClone.CheckPET = this.CheckPET.Clone();
			CheckInfoClone.CheckX = this.CheckX.Clone();

			return CheckInfoClone;
		}

	#endregion

	}
}
