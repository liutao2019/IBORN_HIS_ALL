using System;

namespace neusoft.HISFC.Object.InterfaceSi
{
	/// <summary>
	/// BlackList ��ժҪ˵����
	/// </summary>
	public class BlackList:neusoft.neuFC.Object.neuObject
	{
		public BlackList()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		private string mCardNo;
		private string kind;
		private string validState;
		private neusoft.neuFC.Object.neuObject operInfo = new neusoft.neuFC.Object.neuObject();
		private DateTime operDate;
		/// <summary>
		/// ��ͬ��λ�����ҽ��֤��
		/// </summary>
		public string MCardNo
		{
			get{return mCardNo;}
			set{mCardNo = value;}
		}
		/// <summary>
		/// ���� 0 ��λ 1 ����
		/// </summary>
		public string Kind
		{
			get{return kind;}
			set{kind = value;}
		}
		/// <summary>
		/// ��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ����
		/// </summary>
		public string ValidState
		{
			get{return validState;}
			set{validState = value;}
		}
		/// <summary>
		/// ����Ա��Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperInfo
		{
			get{return operInfo;}
			set{operInfo = value;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			get{return operDate;}
			set{operDate = value;}
		}

		public new BlackList Clone()
		{
			BlackList obj = base.Clone() as BlackList;
			obj.OperInfo = this.OperInfo.Clone();

			return obj;
		}

	}
}
