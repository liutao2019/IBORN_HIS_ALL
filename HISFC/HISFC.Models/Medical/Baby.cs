using System;
namespace Neusoft.HISFC.Models.Medical
{
	/// <summary>
    /// [��������: Ӥ��ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
	public class Baby:Neusoft.FrameWork.Models.NeuObject 
	{
		public Baby() {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//ID ����֤��� Name ����������
		/// <summary>
		/// �Ա�
		/// </summary>
		private string sexCode;
		public string SexCode {
			get {
				return this.sexCode;	 
			}
			set {
				this.sexCode = value;
			}	 
		}
		/// <summary>
		/// ��������
		/// </summary>
		private DateTime birth;
		public  DateTime Birthday {
			get {
				return this.birth;
			}
			set {
				this.birth = value;	 
			}
		}
		/// <summary>
		/// ��ͥסַ
		/// </summary>
		private string home = "";
		public  string Home {
			get {
				return this.home;
			}
			set {
				this.home = value;
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		private string birthPlace;
		public  string BirthPlace {
			get {
				return this.birthPlace;	 
			}
			set {
				this.birthPlace = value;	 
			}
		}
		/// <summary>
		/// �����ص�
		/// </summary>
		private string birthAddress;
		public  string BirthAdderss {
			get {
			   return this.birthAddress;	 
			}
			set {
			   this.birthAddress = value;	 
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		private string gestation;
		public  string Gestation {
			get {
				return this.gestation;	 
			}
			set {
				this.gestation = value;	 
			}
		}
		/// <summary>
		/// ����״��
		/// </summary>
		private string healthStatus; 
	    public  string HealthStatus{
			get {
			    return this.healthStatus;	 
			}
			set {
			    this.healthStatus = value;	 
			}
	    }
		/// <summary>
		/// ���
		/// </summary>
		private double height;
		public  double Height {
			get{
			    return this.height;
			}	 
			set {
			    this.height= value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		private double weight;
		public  double Weight {
			get {
			    return this.weight;	 
			}
			set {
			   this.weight = value;	 
			}
		}
		/// <summary>
		/// ǩ������
		/// </summary>
        private DateTime issueDate;
		public  DateTime IssueDate {
			get {
			   return this.issueDate;	 
			}
			set {
			    this.issueDate = value;	 
			}
		}
		/// <summary>
		/// ĸ������
		/// </summary>
		private string motherName="";
		public  string MotherName {
			get {
			    return this.motherName;    
			}
			set {
			    this.motherName = value;	 
			}
		}
		/// <summary>
		/// ĸ������
		/// </summary>
		private string motherAge="";
		public  string MotherAge {
			get {
			   return this.motherAge;	 
			}
			set {
			   this.motherAge = value;	 
			}
		}
		/// <summary>
		/// ĸ������
		/// </summary>
        private string motherNation="";
		public  string MotherNation {
			get {
			    return this.motherNation;
			}
			set {
			    this.motherNation = value;	 
			}
		}
		/// <summary>
		/// ĸ�׹���
		/// </summary>
		private string motherNationality="";
		public  string MotherNationality {
			get {
			    return this.motherNationality;	 
			}
			set {
			    this.motherNationality = value;	 
			}
		}
		/// <summary>
		/// ĸ�����֤��
		/// </summary>
		private string motherCardNo="";
		public  string MotherCardNo {
			get {
			    return this.motherCardNo;
			}
			set {
			    this.motherCardNo = value;	 
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		private string fatherName="";
		public  string FatherName {
			get {
				return this.fatherName;    
			}
			set {
				this.fatherName = value;	 
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		private string fatherAge="";
		public  string FatherAge {
			get {
				return this.fatherAge;	 
			}
			set {
				this.fatherAge = value;	 
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		private string fatherNation="";
		public  string FatherNation {
			get {
				return this.fatherNation;
			}
			set {
				this.fatherNation = value;	 
			}
		}
		/// <summary>
		/// ���׹���
		/// </summary>
		private string fatherNationality="";
		public  string FatherNationality {
			get {
				return this.fatherNationality;	 
			}
			set {
				this.fatherNationality = value;	 
			}
		}
		/// <summary>
		/// �������֤��
		/// </summary>
		private string fatherCardNo="";
		public  string FatherCardNo {
			get {
				return this.fatherCardNo;
			}
			set {
				this.fatherCardNo = value;	 
			}
		}
		/// <summary>
		/// �����ص����
		/// </summary>
		private string placeType="";
		public  string PlaceType {
			get {
			    return this.placeType;
			}
			set {
			    this.placeType = value;	 
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		private string receiver="";
		public  string Receiver {
			get {
			    return this.receiver;    
			}
			set {
			    this.receiver = value;	 
			}
		}
		/// <summary>
		/// ������������
		/// </summary>
		private string facilityName="";
		public  string FacilityName {
			get {
			    return this.facilityName;	 
			}
			set {
			    this.facilityName = value;	 
			}
		}
		/// <summary>
		/// ����֤״̬
		/// </summary>
		private string status = "0";
		public string Status {
			get {
			    return this.status;	 
			}
			set {
			    this.status = value;	 
			}
		}
		/// <summary>
		/// Ӥ��סԺ��ˮ��
		/// </summary>
		private string inpatientNo = "";
		public string InaptientNo {
			get {
			   return this.inpatientNo;	 
			}
			set {
			   this.inpatientNo = value;	 
			}
		}
	}
}
