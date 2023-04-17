using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee
{


	/// <summary>
	/// FinanceGroup<br></br>
	/// [��������: �������� ID��������� Name ����������]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class FinanceGroup : NeuObject, ISort, IValidState
	{
		#region ����
		
		/// <summary>
		/// ��������Ա��Ϣ
		/// </summary>
		private Employee employee = new Employee();
		
		/// <summary>
		/// ��Ч�� ͣ��(0) ��Ч(1) ����(2)
		/// </summary>
		private EnumValidState validState;
		
        ///// <summary>
        ///// ��Ч�� ����ValidState���Ա仯,ValidState = "1" Ϊtrue����ֵ��Ϊfalse
        ///// </summary>
        //private bool isValid;
		
		/// <summary>
		/// ���
		/// </summary>
		private int sortID;

        /// <summary>
        /// Ψһ����
        /// </summary>
        private string pkID = string.Empty;

		#endregion

		#region ����
		
		/// <summary>
		/// ��������Ա��Ϣ
		/// </summary>
		public Employee Employee
		{
			get
			{
				return this.employee;
			}
			set
			{
				this.employee = value;
			}
		}
		
		/// <summary>
		/// ��Ч�� ͣ��(0) ��Ч(1) ����(2)
		/// </summary>
        public EnumValidState ValidState
		{
			get
			{
				return this.validState;
			}
			set
			{
				this.validState = value;
			}
		}

        /// <summary>
        /// Ψһ����
        /// </summary>
        public string PkID 
        {
            get 
            {
                return this.pkID;
            }
            set 
            {
                this.pkID = value;
            }
        }

		#endregion

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ���ĸ���</returns>
		public new FinanceGroup Clone()
		{
			FinanceGroup financeGroup = base.Clone() as FinanceGroup;

			financeGroup.Employee = this.Employee.Clone();

			return financeGroup;
		}
		
		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region ISort ��Ա

		/// <summary>
		/// ���
		/// </summary>
		public int SortID
		{
			get
			{
				return this.sortID;
			}
			set
			{
				this.sortID = value;
			}
		}

		#endregion

        //#region IValid ��Ա
		
        ///// <summary>
        ///// ��Ч�� ����ValidState���Ա仯,ValidState = "1" Ϊtrue����ֵ��Ϊfalse
        ///// </summary>
        //public bool IsValid
        //{
        //    get
        //    {
        //        if (this.validState == "1")
        //        {
        //            this.isValid = true;
        //        }
        //        else
        //        {
        //            this.isValid = false;
        //        }

        //        return this.isValid;
        //    }
        //    set
        //    {
        //        this.isValid = value;
        //    }
        //}

        //#endregion

		#endregion

		#region ��������,����

		//public FS.FrameWork.Models.NeuObject Employee = new FS.FrameWork.Models.NeuObject();
		[Obsolete("����, PkID", true)]
		public string PKId;
		[Obsolete("����, ��Emplyee.ID����", true)]
		public string EmployeeID;
		[Obsolete("����, ��Emplyee.Name��ID����", true)]
		public string EmployeeName;	   
		
		#endregion


       
    }
}
