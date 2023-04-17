using System;
using System.Collections.Generic;

namespace FS.HISFC.Models.Operation
{
	/// <summary>
	/// [��������: ������ʵ��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-09-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class OpsRoom : FS.FrameWork.Models.NeuObject
	{
		public OpsRoom()
		{}

        #region �ֶ�
        private List<OpsTable> tables = new List<OpsTable>();
        #endregion

        #region ����


		[Obsolete("InputCode",true)]
		public string Input_Code;

		private string inputCode = string.Empty;
		///<summary>
		///������
		///</summary>
		public string InputCode
		{
			get
			{
				return this.inputCode;
			}
			set
			{
				this.inputCode = value;
			}
		}
		
		private string deptID = string.Empty;
		///<summary>
		///��������(������)
		///</summary>
		public string DeptID
		{
			get
			{
				return this.deptID;
			}
			set
			{
				this.deptID = value;
			}
		}

		///<summary>
		///1��Ч/0��Ч
		///</summary>
		private bool isValid = true;
		[Obsolete("IsValid",true)]
		public bool bValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}
		
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}


		private string operCode = string.Empty;
		/// <summary>
		/// ����Ա
		/// </summary>
		public string OperCode
		{
			get
			{
				return this.operCode;
			}
			set
			{
				this.operCode = value;
			}
		}


		private DateTime operDate = DateTime.MinValue;
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime OperDate
		{
			get
			{
				return this.operDate;
			}
			set
			{
				this.operDate = value;
			}
		}

        /// <summary>
        /// ������������̨
        /// </summary>
        /// Robin   2007-01-16
        public List<OpsTable> Tables
        {
            get
            {
                return this.tables;
            }
        }


        private string ipAddress = string.Empty;
        /// <summary>
        ///IP��ַ
        /// </summary>
        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }
		#endregion

        /// <summary>
        /// �������̨
        /// </summary>
        /// <param name="table">����̨</param>
        /// Robin   2007-01-16
        public void AddTable(OpsTable table)
        {
            table.Room = this;
            this.tables.Add(table);            
        }
		public new OpsRoom Clone()
		{
			OpsRoom newOpsRoom = base.Clone() as OpsRoom;
			return newOpsRoom;
		}
	}
}
