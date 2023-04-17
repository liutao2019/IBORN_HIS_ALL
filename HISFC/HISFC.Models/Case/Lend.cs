using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// Lend ��ժҪ˵������������ʵ�� ID ¼��������Ա���� Name ¼��������Ա����
	/// </summary>
	public class Lend : neusoft.neuFC.Object.neuObject
	{
		public Lend()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ˽�б���
		
		private neusoft.HISFC.Object.Case.Base  myPatientInfo = new Base();
		private neusoft.neuFC.Object.neuObject myEmplInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myEmplDeptInfo = new neusoft.neuFC.Object.neuObject();
		private neusoft.neuFC.Object.neuObject myReturnOperInfo = new neusoft.neuFC.Object.neuObject();
		private DateTime lendDate;
		private DateTime prerDate;
		private string lendKind;
		private string lendStus;
		private DateTime operDate;
		private DateTime returnDate;
		private string cardNo;
		#endregion

		#region ����
		/// <summary>
		/// ���� 
		/// </summary>
		public string CardNO
		{
			set
			{
				cardNo =  value;
			}
			get
			{
				if(cardNo ==null)
				{
					cardNo ="";
				}
				return cardNo;
			}
		}
		/// <summary>
		/// ���˻�����Ϣ
		/// </summary>
		public neusoft.HISFC.Object.Case.Base PatientInfo
		{
			get{ return myPatientInfo; }
			set{ myPatientInfo = value; }
		}
		/// <summary>
		/// ��������Ϣ ID �����˱�� Name ����������
		/// </summary>
		public neusoft.neuFC.Object.neuObject EmplInfo
		{
			get{ return myEmplInfo; }
			set{ myEmplInfo = value; }
		}
		/// <summary>
		/// ���������ڿ�����Ϣ ID ���ұ�� Name ��������
		/// </summary>
		public neusoft.neuFC.Object.neuObject EmplDeptInfo
		{
			get{ return myEmplDeptInfo; }
			set{ myEmplDeptInfo = value; }
		}
		
		/// <summary>
		/// ��������(����)
		/// </summary>
		public DateTime LendDate
		{
			get{ return lendDate; }
			set{ lendDate = value; }
		}
		
		/// <summary>
		/// Ԥ���黹����(����)
		/// </summary>
		public DateTime PrerDate
		{
			get{ return prerDate; }
			set{ prerDate = value; }
		}
		
		/// <summary>
		/// ��������(����)
		/// </summary>
		public string LendKind
		{
			get
			{
				if(lendKind == null)
				{
					lendKind = "";
				}
				return lendKind; 
			}
			set
			{
				if( CaseFunc.ExLength( value, 1, "��������" ) )
				{
					lendKind = value;
				}
			}
		}
	
		/// <summary>
		/// ����״̬ 1���/2���� 
		/// </summary>
		public string LendStus
		{
			get{
				if(lendStus == null)
				{
					lendStus = "";
				}
				return lendStus; 
			}
			set
			{
				if( CaseFunc.ExLength( value, 1, "����״̬ 1���/2���� " ) )
				{
					lendStus = value;
				}
			}
		}
	
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime OperDate
		{
			get{ return operDate; }
			set{ operDate = value; }
		}
		/// <summary>
		///�黹����Ա��Ϣ ID �黹����Ա���� Name �黹����Ա����
		/// </summary>
		public neusoft.neuFC.Object.neuObject ReturnOperInfo
		{
			get{ return myReturnOperInfo; }
			set{ myReturnOperInfo = value; }
		}
		
		/// <summary>
		/// �黹����
		/// </summary>
		public DateTime ReturnDate
		{
			get{ return returnDate; }
			set{ returnDate = value; }
		}

		#endregion

		#region ���ú���

		public new Lend Clone()
		{
			Lend LendClone = base.MemberwiseClone() as Lend;

			LendClone.PatientInfo = this.PatientInfo.Clone();
			LendClone.EmplInfo = this.EmplInfo.Clone();
			LendClone.EmplDeptInfo = this.EmplDeptInfo.Clone();
			LendClone.ReturnOperInfo = this.ReturnOperInfo.Clone();

			return LendClone;
		}
		
		#endregion

	}
}
