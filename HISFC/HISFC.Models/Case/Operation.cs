using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// Operation ��ժҪ˵����������������������Ϣ
	/// </summary>
	public class Operation : neusoft.neuFC.Object.neuObject, neusoft.HISFC.Object.Base.ISpellCode
	{
		public Operation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		#region ˽�б���

		private neusoft.neuFC.Object.neuObject myOperationInfo = new neusoft.neuFC.Object.neuObject();
		private string operationEnName;
		
		#endregion

		#region ����

		/// <summary>
		/// ������Ŀ��Ϣ ID �������� Name ������������
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperationInfo
		{
			get{ return myOperationInfo; }
			set{ myOperationInfo = value; }
		}
		/// <summary>
		/// ����Ӣ������
		/// </summary>
		public string OperationEnName
		{
			get{ return operationEnName; }
			set{ operationEnName = value; }
		}

		#endregion

		#region ���к���

		public new Operation Clone()
		{
			Operation OpClone = base.MemberwiseClone() as Operation;

			OpClone.OperationInfo = this.OperationInfo.Clone();

			return OpClone;
		}

		#endregion
		
		#region ISpellCode ��Ա
		
		private string spellCode;
		public string Spell_Code
		{
			get
			{
				return spellCode;
			}
			set
			{
				spellCode = value;
			}
		}
		private string wbCode;
		public string WB_Code
		{
			get
			{
				return wbCode;
			}
			set
			{
				wbCode = value;
			}
		}
		private string userCode;
		public string User_Code
		{
			get
			{
				return userCode;
			}
			set
			{
				userCode = value;
			}
		}

		#endregion
	}		
}
