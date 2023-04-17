using System;

namespace neusoft.HISFC.Object.InterfaceSi
{
	/// <summary>
	/// SIPersonInfo ��ժҪ˵����
	/// </summary>
	public class SIPersonInfo:neusoft.neuFC.Object.neuObject
	{
		public SIPersonInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private string hosNo;
		private string personId;
		private string inHosId;
		private string company;
		
		private string sexId; 
		private string sexName;
		private DateTime bornDate;
		private string personTypeId;
		private string personTypeName;
		private DateTime joinSIDate;
		private string baseSITypeId;
		private string baseSITypeName;
		/// <summary>
		/// ҽԺ����
		/// </summary>
		public string HosNo
		{
			get{return hosNo;}
			set{hosNo = value;}
		}
		/// <summary>
		/// ҽ����(���֤��)
		/// </summary>
		public string PersonId
		{
			get{return personId;}
			set{personId = value;}
		}
		/// <summary>
		/// סԺ��
		/// </summary>
		public string InHosId
		{
			get{return inHosId;}
			set{inHosId = value;}
		}
		/// <summary>
		/// ������λ
		/// </summary>
		public string Company
		{
			get{return company;}
			set{company = value;}
		}
		/// <summary>
		/// �Ա����
		/// </summary>
		public string SexId
		{
			get
			{
				return sexId;
			}
			set
			{
				sexId = value;
				
				if(sexId == "0")
					sexName = "Ů";
				if(sexId == "1")
					sexName = "��";
			}
		}
		/// <summary>
		/// �Ա�����
		/// </summary>
		public string SexName
		{
			get{return sexName;}
			set{sexName = value;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public DateTime BornDate
		{
			get{return bornDate;}
			set{bornDate = value;}
		}
		/// <summary>
		/// ��Ա���
		/// </summary>
		public string PersonTypeId
		{
			get
			{
				return personTypeId;
			}
			set
			{
				personTypeId = value;
				if(personTypeId == "1")
					this.personTypeName = "��ְ";
				if(personTypeId == "2")
					this.personTypeName = "����";
			}
		}

		/// <summary>
		/// ��Ա�������
		/// </summary>
		public string PersonTypeName {
			get {
				return this.personTypeName;
			}
		}

		/// <summary>
		/// �μӱ�������
		/// </summary>
		public DateTime JoinSIDate
		{
			get{return joinSIDate;}
			set{joinSIDate = value;}
		}
		/// <summary>
		/// �α�����
		/// </summary>
		public string BaseSITypeId
		{
			get{return baseSITypeId;}
			set
			{
				baseSITypeId = value;
				if(baseSITypeId == "3")
					baseSITypeName = "�α��ɷ�";
				if(baseSITypeId == "4")
					baseSITypeName = "��ͣ�ɷ�";
				if(baseSITypeId == "7")
					baseSITypeName = "��ֹ�α�";
			}
		}

		/// <summary>
		/// �α���������
		/// </summary>
		public string BaseSITypeName {
			get{return this.baseSITypeName;}
		}
		
	}
}
