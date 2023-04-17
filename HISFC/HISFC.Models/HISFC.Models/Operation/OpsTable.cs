using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Operation 
{
	/// <summary>
	/// [��������: ����̨ʵ��]<br></br>
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
    public class OpsTable : FS.FrameWork.Models.NeuObject
	{
#region ���캯��
		public OpsTable()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}		

		public OpsTable(Department dept)
		{
			this.dept = dept;
		}

		public OpsTable(NeuObject user)
		{
			this.user = user;
		}

		public OpsTable(Department dept, NeuObject user)
		{
			this.dept = dept;
			this.user = user;
		}
#endregion

#region �ֶ�
        private OpsRoom room;
        private string okInfo = string.Empty;
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


		private FS.HISFC.Models.Base.Department dept;
		///<summary>
		///��������(������)
		///</summary>
		public FS.HISFC.Models.Base.Department Dept
		{
			get
			{
				if (this.dept == null) 
				{
					this.dept = new FS.HISFC.Models.Base.Department();
				}
				return this.dept;
			}
			set
			{
				this.dept = value;
			}
		}


		private FS.FrameWork.Models.NeuObject user;
		/// <summary>
		/// ����Ա
		/// </summary>
		public FS.FrameWork.Models.NeuObject User
		{
			get
			{
				if (this.user == null) 
				{
					this.user = new FS.FrameWork.Models.NeuObject();
				}
				return this.user;
			}
			set
			{
				this.user = value;
			}
		}


		private string roomID = string.Empty;
		/// <summary>
		/// �����Ҵ���
		/// </summary>
		public string RoomID
		{
			get
			{
				return this.roomID;
			}
			set
			{
				this.roomID = value;
			}
		}

        /// <summary>
        /// ����������
        /// </summary>
        /// Robin   2007-01-16
        public OpsRoom Room
        {
            get
            {
                return this.room;
            }
            set
            {
                this.room = value;
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


		private string remark = string.Empty;
		///<summary>
		///��ע
		///</summary>
		public string Remark
		{
			get
			{
				return this.remark;
			}
			set
			{
				this.remark = value;
			}
		}

        /// <summary>
        /// ���Ϸ���Ϣ
        /// </summary>
        public string InvalidInfo
        {         
           get
            {
                return this.okInfo;
            }
            set
            {
                this.okInfo = value;
            }
        }
#endregion

#region ����
        /// <summary>
        /// �Ƿ�Ϸ�
        /// </summary>
        /// <returns></returns>
        public bool IsOK()
        {
            if(this.Name.Length==0)
            {
                this.okInfo = "������̨����Ϊ��";
                return false;
            }
  
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.Name, 20) == false)
            {
                this.okInfo = "����̨�����ѳ���10������!";
                return false;
            }
                        
            if (this.inputCode == "")
            {
                this.okInfo =  "������̨������Ϊ��";
                return false;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.inputCode, 8) == false)
            {
                this.okInfo = "�����볤���ѳ���8���ַ�!";
                return false;
            }
            
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.Memo, 50) == false)
            {
                this.okInfo = "��ע�����ѳ���25������!";
                return false;
            }

            return true;
        }

		public new OpsTable Clone()
		{
			OpsTable newOpsTable = base.Clone() as OpsTable;
			newOpsTable.Dept = this.Dept.Clone();
			newOpsTable.User = this.User.Clone();
			return newOpsTable;
		}
#endregion
		
	}

	
}
