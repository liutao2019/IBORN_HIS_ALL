using System;

namespace Neusoft.HISFC.Models.Medical
{
	/// <summary>
	/// [��������: �¼���������Ϣʵ��]<br></br>
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
	public class NewTechnology:Neusoft.FrameWork.Models.NeuObject
	{
		public NewTechnology()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �¼������
		/// </summary>
		private string tech_ID;

		public string TechID {
			get {
			   return tech_ID; 
			}
			set {
			   tech_ID = value;	 
			}
		}
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		private string tech_Name;

		public string TechName {
			get {
			   return tech_Name;
			}	 
			set {
			   tech_Name = value;	 
			}
		}
		/// <summary>
		/// ���ұ���
		/// </summary>
        private string dept_Code;

		public string DeptCode {
			get {
			   return dept_Code;    
			}
			set {
			   dept_Code = value;	 
			}
		}
		/// <summary>
		/// ��Ŀ����
		/// </summary>
        private string tech_Type;

		public string TechType {
			get {
			    return tech_Type;	 
			}
			set {
			    tech_Type = value;	 
			}
		}
		/// <summary>
		/// ��Ŀ���
		/// </summary>
		private string tech_Asst;
		public string TechAsst {
			get {
			   return tech_Asst;	 
			}
			set {
			   tech_Asst = value;	 
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		private string apply_Date;

		public string ApplyDate {
			get {
			   return apply_Date;	 
			}
			set {
			   apply_Date = value;	 
			}
		}
		/// <summary>
		/// ��Ŀ������
		/// </summary>
        private string tech_Pricipal;

		public string TechPricipal {
			get {
			    return tech_Pricipal;    
			}
			set {
			    tech_Pricipal = value;	 
			}
		}
		/// <summary>
		/// ���ʱ��
		/// </summary>
		private string audit_Date;

		public string AuditDate {
			get {
			    return audit_Date;
			}
			set {
			    audit_Date = value;	 
			}
		}
		/// <summary>
		/// ��˲���
		/// </summary>
		private string audit_Dept;

		public string AuditDept {
			get {
			    return audit_Dept;	 
			}
			set {
			    audit_Dept = value;	 
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		private string audit_Notion;

		public string AuditNotion {
			get {
			    return audit_Notion;	 
			}
			set {
			    audit_Notion = value;	 
			}
		}
		/// <summary>
		/// �����´�ʱ��
		/// </summary>
		private string feedBack_Date;

		public string FeedBackDate {
			get {
			    return feedBack_Date;    
			}
			set {
			    feedBack_Date = value;	 
			}
		}
		/// <summary>
		/// �Ǽ���
		/// </summary>
		private string booker;
       
		public string Booker {
			get {
			   return booker;    
			}
			set {
			   booker = value;	 
			}
		}
		/// <summary>
		/// �Ǽ�ʱ��
		/// </summary>
		private string reg_Date;

		public string RegDate {
			get {
			   return reg_Date;	 
			}
			set {
			   reg_Date = value;	 
			}
		}
		private string oper_Date;

		public string OperDate {
			get {
				return oper_Date;	 
			}
			set {
				oper_Date = value;	 
			}
		}
	}
}
