using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// Firstpagesearch ��ժҪ˵����
	/// </summary>
	public class Firstpagesearch : neusoft.neuFC.Object.neuObject 
	{
		public Firstpagesearch()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		///  �������� 
		/// </summary>
		public string PARENT_CODE ;                              
		/// <summary>
		///  ��������   
		/// </summary>
		public string  CURRENT_CODE; 
        /// <summary>
        /// �������
        /// </summary>
		public string SequenceNO ;   
        /// <summary>
        ///  ���� 1 �������� ,2�������� ,3 ��ʷ��¼ 
        /// </summary>
		public string TypeCode ;
		/// <summary>
		/// ��ʼʱ�� 
		/// </summary>
		public System.DateTime BeginDate;
		/// <summary>
		///����ʱ�� 
		/// </summary>
		public System.DateTime EndDate;
		/// <summary>
		///  Ҫ���ѯ��ҽ�� 
		/// </summary>
		public string SearchDoc;
		/// <summary>
		///�Ƿ������ 
		/// </summary>
		public string Diagnoseflag;
		/// <summary>
		/// ����� �ö��� �� ~����
		/// </summary>
		public string Clinic_diag;
		/// <summary>
		/// ��Ժ��� 
		/// </summary>
		public string InhosDiag ;
		/// <summary>
		/// ͬһ����Ժ 
		/// </summary>
		public string Samein;
		/// <summary>
		/// ICD-10                                 
		/// </summary>
		public string Diag1;
		/// <summary>
		/// icd-10 ��Ҫ��� 
		/// </summary>
		public string DiagMainFlag1;
		/// <summary>
		/// ֤ʵ(�������)                         
		/// </summary>
		public string CL_PA1;
		/// <summary>
		///  ������Ƿ�������                       
		/// </summary>
		public string OperationType1;
		/// <summary>
		///  ��Ժ���                               
		/// </summary>
		public string OutState1;
		/// <summary>
		///  �����                                 
		/// </summary>
		public string  SingleDiag1;
		/// <summary>
		/// ����ICD-10 
		/// </summary>
		public string Diag2;
		/// <summary>
		/// icd-10 ��Ҫ  (����) 
		/// </summary>
		public string DiagMainFlag2;
		/// <summary>
		///  ֤ʵ(�������)    ����                 
		/// </summary>
		public string CL_PA2;
		/// <summary>
		///  ������Ƿ������� ����                  
		/// </summary>
		public string OperationType2;
		/// <summary>
		/// ��Ժ��� ���� ���                     
		/// </summary>
		public string OutState2;
		/// <summary>
		/// �ų�ICD-10                             
		/// </summary>
		public string Diag3;
		/// <summary>
		/// icd-10 ��Ҫ �ų�                       
		/// </summary>
		public string DiagMainflag3;
		/// <summary>
		///  ֤ʵ(�������)    �ų�                 
		/// </summary>
		public string  CL_PA3;
		/// <summary>
		/// ������Ƿ������� �ų�                  
		/// </summary>
		public string OperationType3;
		/// <summary>
		/// ��Ժ��� �ų�                      
		/// </summary>
		public string OutState3;
		/// <summary>
		///  �����                                 
		/// </summary>
		public string SingleDiag3;
		/// <summary>
		///  ������־   
		/// </summary>
		public string OperationFlag;
		/// <summary>
		/// ������ �ö��� �� ~����                 
		/// </summary>
		public string OperationCode1;
		/// <summary>
		/// ����1                                  
		/// </summary>
		public string OperatorCode1;
		/// <summary>
		///  ����ʦ                 
		/// </summary>
		public string Anesthetist1;
		/// <summary>
		/// ����ʽ                               
		/// </summary>
		public string Hocustype1;
		/// <summary>
		///  ��������־                             
		/// </summary>
		public string SingleOper1;
		/// <summary>
		///  ���������� �ö��� �� ~����             
		/// </summary>
		public string OperationCode2;
		/// <summary>
		/// ����2 ����                             
		/// </summary>
		public string OperatorCode2;
		/// <summary>
		///  ����ʦ2 ����                           
		/// </summary>
		public string Anesthetist2;
		/// <summary>
		/// �ų������� �ö��� �� ~����             
		/// </summary>
		public string OperationCode3;
		/// <summary>
		/// ����3 (�ų�)                           
		/// </summary>
		public string OperatorCode3;
		/// <summary>
		///  ����ʦ3( �ų�)                         
		/// </summary>
		public string Anesthetist3;
		/// <summary>
		///  ��������־(�ų�)                       
		/// </summary>
		public string singleOper3;   
		/// <summary>
		///  ������Ŀ                               
		/// </summary>
		public string SPecalItem1;
		/// <summary>
		/// ������Ŀ �ų�                          
		/// </summary>
		public string SpecalItem2;
		/// <summary>
		///  ����֢                                 
		/// </summary>
		public string Syndrome1;
		/// <summary>
		///  Ժ�ڸ�Ⱦ                               
		/// </summary>
		public string Infection ;
		/// <summary>
		/// ct 
		/// </summary>
		public string CT;
		/// <summary>
		///  UFCT   
		/// </summary>
		public string UFCT ;
		/// <summary>
		///MR 
		/// </summary>
		public string MR;
		/// <summary>
		/// X��                                    
		/// </summary>
		public string XFLAG;
		/// <summary>
		///  B��                                    
		/// </summary>
		public string BFLAG;
		/// <summary>
		///  ������Ϣ                               
		/// </summary>
		public string BaseFlag;
		/// <summary>
		/// ��Ժ����                               
		/// </summary>
		public string InNum;
		/// <summary>
		///  �Ա�                                   
		/// </summary>
		public string SEX;
		/// <summary>
		/// ����                                   
		/// </summary>
		public string AGE;
		/// <summary>
		///  ���䵥λ                               
		/// </summary>
		public string AGETYPE;
		/// <summary>
		///  ��������         
		/// </summary>
		public string HomeZip;
		///���ұ���                               
		public string DeptCode;
		/// <summary>
		/// ������Դ                               
		/// </summary>
		public string Inavenue;
		/// <summary>
		/// ��Ժ���                               
		/// </summary>
		public string Instate;
		/// <summary>
		/// ��λ��������                           
		/// </summary>
		public string WorkZip;
		//ǩ��ҽ��                               
		public string TxDoctor;
		/// <summary>
		/// ��Ժʱ��                               
		/// </summary>
		public System.DateTime IndateBegin;
		/// <summary>
		/// ��Ժʱ��                               
		/// </summary>
		public System.DateTime INDateEnd;
		/// <summary>
		/// ����Ա 
		/// </summary>
		public string OperCode;
		/// <summary>
		/// ����ʱ�� 
		/// </summary>
		public System.DateTime OperDate;
		/// <summary>
		/// һ���ڸ���Ժ
		/// </summary>
		public string InafteroneWeek;
	}
}
