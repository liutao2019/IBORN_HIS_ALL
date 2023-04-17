using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// ������������ʵ�塣
	/// �̳�neusoft.neuFC.Object.neuObject 
	/// neusoft.neuFC.Object.neuObject.ID ����Ա���� neusoft.neuFC.Object.neuObject.Name ����Ա����
	/// 
	/// ����: WangYu 2004-12-04
	/// </summary>
	public class QC : neusoft.neuFC.Object.neuObject
	{
		public QC()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		//˽���ֶ�
		private string myInpatientNO;
		private neusoft.neuFC.Object.neuObject myRuleInfo = new neusoft.neuFC.Object.neuObject();
		private decimal myMark;
		private string myDenyFlag;
		private DateTime myOperDate;


		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		public string InpatientNO 
		{
			get	{ return  myInpatientNO;}
			set	{  myInpatientNO = value; }
		}

		/// <summary>
		/// ������Ϣ ID ������� Name ������Ϣ
		/// </summary>
		public neusoft.neuFC.Object.neuObject RuleInfo 
		{
			get	{ return  myRuleInfo;}
			set	{  myRuleInfo = value; }
		}

		/// <summary>
		/// �÷�
		/// </summary>
		public decimal Mark 
		{
			get	{ return  myMark;}
			set	{  myMark = value; }
		}

		/// <summary>
		/// �Ƿ����
		/// </summary>
		public string DenyFlag 
		{
			get	{ return  myDenyFlag;}
			set	{  myDenyFlag = value; }
		}

		/// <summary>
		/// ��������
		/// </summary>
		public DateTime OperDate 
		{
			get	{ return  myOperDate;}
			set	{  myOperDate = value; }
		}
		
		public new QC Clone()
		{
			QC QCCLone = base.MemberwiseClone() as QC;
			
			QCCLone.myRuleInfo = this.myRuleInfo.Clone();

			return QCCLone;
		}

	}
}
