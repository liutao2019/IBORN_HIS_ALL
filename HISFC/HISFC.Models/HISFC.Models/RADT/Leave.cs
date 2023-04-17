using System;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// [��������: ���ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ΰ'
	///		�޸�ʱ��='2006-9-12'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
    [Serializable]
    public class Leave: FS.FrameWork.Models.NeuObject 
	{
		
		/// <summary>
		/// ���캯��
		/// </summary>
		public Leave() 
		{
		}


		#region ����

		/// <summary>
		/// �������
		/// </summary>
		private System.DateTime leaveTime ;
		
	    /// <summary>
	    /// �������
	    /// </summary>
		private int leaveDays ;
		
		/// <summary>
		/// ׼��ҽ��
		/// </summary>
		private string doctCode ;
		
	    /// <summary>
	    /// ���������������ˣ�����ʱ�䣩
	    /// </summary>
		private OperEnvironment oper = new OperEnvironment();
		
		/// <summary>
		/// �ݼٱ�־
		/// </summary>
		private string leaveFlag ;
		
		/// <summary>
		/// ����ҽ��������ҽ����ţ�����ʱ�䣩
		/// </summary>
		private OperEnvironment cancelOper = new OperEnvironment() ;
		

		#endregion

		#region ����
		/// <summary>
		/// ���ʱ��
		/// </summary>
		public System.DateTime LeaveTime 
		{
			get
			{
				return this.leaveTime;
			}
			set
			{
				this.leaveTime = value;
			}
		}


		/// <summary>
		/// �������
		/// </summary>
		public int LeaveDays {
			get
			{
				return this.leaveDays;
			}
			set
			{
				this.leaveDays = value;
			}
		}


		/// <summary>
		/// ����ҽ��
		/// </summary>
		public string DoctCode {
			get
			{
				return this.doctCode; 
			}
			set
			{
				this.doctCode = value; 
			}
		}


		/// <summary>
		/// ��ٲ�����
		/// </summary>
		public OperEnvironment Oper {
			get
			{
				return this.oper; 
			}
			set
			{
				this.oper = value;
			}
		}

		/// <summary>
		/// 0��� 1����,����
		/// </summary>
		public string LeaveFlag {
			get
			{
				
				return this.leaveFlag; 
			}
			set
			{
				this.leaveFlag = value; 
			}
		}


		/// <summary>
		/// ������(���ϲ�����)
		/// </summary>
		public OperEnvironment CancelOper {
			get
			{
				return this.cancelOper; 
			}
			set
			{
				this.cancelOper = value; 
			}
		}

		#endregion
		
		#region ����
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Leave Clone()
		{
			Leave leave = base.Clone() as Leave;
			leave.Oper=this.Oper.Clone();
			leave.CancelOper = this.CancelOper.Clone();
			return leave;
		}
		
		#endregion

		#region ����
		/// <summary>
		/// ���ʱ��
		/// </summary>
		[System.Obsolete("����Ϊ LeaveTime",true)]
		public System.DateTime LeaveDate ;

		/// <summary>
		/// ��ٲ�����
		/// </summary>
		[System.Obsolete("����Ϊ this.Oper.Oper.ID",true)]
		public string OperCode ;
        
		/// <summary>
		/// ������(���ϲ�����)
		/// </summary>
		[System.Obsolete("����Ϊ this.CancelOper.Oper.ID",true)]
		public string CancelOpcd ;
	
		/// <summary>
		/// ����ʱ��(����ʱ��)
		/// </summary>
		[System.Obsolete("����Ϊ this.CancelOper.OperTime",true)]
		public System.DateTime CancelDate ;
	
		#endregion
	}
}
