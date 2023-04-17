using System;

namespace FS.HISFC.DCP.Object
{
	/// <summary>
	/// CancerAdd ��ժҪ˵����
	/// ������������ʵ��
	/// </summary>
	public class CancerAdd: FS.FrameWork.Models.NeuObject
	{

		#region ˽���ֶ�

		/// <summary>
		/// �������
		/// </summary>
		private string  report_no = "";

		/// <summary>
		/// ҽ������ 
		/// </summary>
		private string meidcal_card = "";

		/// <summary>
		/// ����
		/// </summary>
		private string nation = "";

		/// <summary>
		/// ����
		/// </summary>
		private string work_type = "";

		/// <summary>
		/// ����״��  
		/// </summary>
		private string marriage = "";

		/// <summary>
		/// ����ICD����
		/// </summary>
		private string diagnostic_icd = "";

		/// <summary>
		/// ���ڵ�ַ-ʡ
		/// </summary>
		private string register_province ="";
		/// <summary>
		/// ���ڵ�ַ-��
		/// </summary>
		private string  register_city = "";
		
		/// <summary>
		/// ���ڵ�ַ-��/��
		/// </summary>
		private string register_district = "";

	

		/// <summary>
		/// ���ڵ�ַ-���硢�ֵ���
		/// </summary>
		private string register_street ="";
        
		/// <summary>
		/// ���ڵ�ַ����
		/// </summary>
		private string register_housenumber="";

		/// <summary>
		/// ���ڵ�ַ 
		/// </summary>
		private string  register_post="" ;

		/// <summary>
		///  ������ַ
		/// </summary>
		private  string work_place = "";

		/// <summary>
		/// ��ϵ��
		/// </summary>
		private string contact_person="";
		/// <summary>
		/// ��ϵ�˹�ϵ
		/// </summary>
		private string relationship="";

		/// <summary>
		/// ��ϵ�˵绰
		/// </summary>
		private string contact_person_tel="";
		/// <summary>
		/// ��ϵ�˵�ַ 
		/// </summary>
		private string contact_person_addr="";

		/*
		/// <summary>
		/// ��ϵ�˵�ַ 
		/// </summary>
		private string contact_person_addr="";	
        */

		/// <summary>
		/// �ٴ�����T 
		/// </summary>
		private string clinical_t="";
		/// <summary>
		/// �ٴ�����N 
		/// </summary>
		private string clinical_n="";
		/// <summary>
		/// �ٴ�����M 
		/// </summary>
		private string clinical_m="";
		/// <summary>
		/// TNM����
		/// </summary>
		 
		private string  term_tnm="";

		/// <summary>
		/// �������־ 
		/// </summary>
		private string pathology_check="";
		/// <summary>
		/// ����� 
		/// </summary>
		private string pathology_no="";
		/// <summary>
		/// ������� 
		/// </summary>
		private string pathology_type="";
		/// <summary>
		/// �ֻ��̶�
		/// </summary>
		private string pathology_degree="";
		/// <summary>
		/// ICD-O
		/// </summary>
		private string icd_o="";





		/*
		/// <summary>
		/// ��������ٴ�
		/// </summary>
		private string diagnoses_clincl="";
		/// <summary>
		/// �������X��
		/// </summary>

		private string diagnoses_x="";
		/// <summary>
		/// ������ݳ�����
		/// </summary>
			
		private string diagnoses_ultrasonic="";
		/// <summary>
		/// ��������ڿ���
		/// </summary>
                
		private string diagnoses_endoscopy="";
		/// <summary>
		/// �������CT
		/// </summary>
		private string diagnoses_ct="";


		/// <summary>
		/// �������PET
		/// </summary>
		private string diagnoses_pet="";

		/// <summary>
		/// �����������
		/// </summary>
		private string diagnoses_ops="";


		/// <summary>
		/// �������ʬ�죨�в���
		/// </summary>
		private string diagnoses_autopsy="";

		/// <summary>
		/// �������ʬ�죨�޲���
		/// </summary>
		private string diagnoses_autopsy_no="";


		/// <summary>
		/// �����������
		/// </summary>
		private string diagnoses_biocgemistry="";

		/// <summary>
		/// �����������
		/// </summary>
		private string diagnoses_immunity="";

		/// <summary>
		/// �������ϸ��
		/// </summary>
		private string diagnoses_cell="";

		/// <summary>
		/// �������ѪƬ
		/// </summary>
		private string diagnoses_blood="";

		/// <summary>
		/// ������ݲ���ԭ����
		/// </summary>
		private string diagnoses_pathology_o="";

		/// <summary>
		/// ������ݲ����̷���
		/// </summary>
		private string diagnoses_pathology_s="";

		/// <summary>
		/// ���������������
		/// </summary>
		private string diagnoses_dead="";

		/// <summary>
		/// ������ݺ˴Ź���
		/// </summary>
		private string diagnoses_mri="";

		/// <summary>
		/// ������ݲ���
		/// </summary>
		private string diagnoses_nuknown="";

		/// <summary>
		/// �����������
		/// </summary>
		private string diagnoses_other="";
		*/

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		private string treatment="";

		/*
		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		private string treat_ops="";
		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		private string treat_chemistry="";
		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		private string treat_radial="";
		/// <summary>
		/// ���Ʒ�ʽ��ҩ
		/// </summary>
		private string treat_herbal="";
		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		private string treat_immunity="";

		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		private string treat_intervention="";

		/// <summary>
		/// ���Ʒ�ʽ��֢����
		/// </summary>
		private string treat_heteropathy="";

		/// <summary>
		/// ���Ʒ�ʽֹʹ����
		/// </summary>
		private string treat_anodyne="";

		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		private string treat_other="";

		/// <summary>
		/// ת��
		/// </summary>
		private string reback="";

		/// <summary>
		/// ת��������ע
		/// </summary>
		private string reback_demo="";
		*/
		/// <summary>
		/// ����ԭ��
		/// </summary>
		private string death_reason="";

		/// <summary>
		/// ԭ���
		/// </summary>
		private string old_diagnoses="";

		/// <summary>
		/// ԭ�������
		/// </summary>
		private System.DateTime old_diagnoses_date;

		/// <summary>
		/// ����
		/// </summary>
		private string district;

		/// <summary>
		/// �ֻ���
		/// </summary>
		private string handPhone;



        /// <summary>
        /// ������չ����Ϣ
        /// </summary>
		private System.Collections .ArrayList  arrayLisExt = new System.Collections.ArrayList();


		#endregion

		#region �����ֶ�

		/// <summary>
		/// �������
		/// </summary>
		public string REPORT_NO  
		{
			get
			{
				return this.report_no;
			}
			set
			{
				this.report_no= value;
			}
		}

		/// <summary>
		/// ҽ������ 
		/// </summary>
		public string MEIDCAL_CARD  
		{
			get
			{
				return this.meidcal_card;
			}
			set
			{
				this.meidcal_card=  value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string NATION  
		{
			get
			{
				return this.nation;
			}
			set
			{
				this.nation=  value;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string WORK_TYPE  
		{
			get
			{
				return this.work_type;
			}
			set
			{
				this.work_type=  value;
			}
		}

		/// <summary>
		/// ����״��  
		/// </summary>
		public string MARRIAGE  
		{
			get
			{
				return this.marriage;
			}
			set
			{
				this.marriage=  value;
			}
		}

		/// <summary>
		/// ����ICD����
		/// </summary>
		public string DIAGNOSTIC_ICD  
		{
			get
			{
				return this.diagnostic_icd;
			}
			set
			{
				this.diagnostic_icd=  value;
			}
		}

		/// <summary>
		/// ���ڵ�ַ-ʡ
		/// </summary>
		public string REGISTER_PROVINCE 
		{
			get
			{
				return this.register_province;
			}
			set
			{
				this.register_province=  value;
			}
		}
		/// <summary>
		/// ���ڵ�ַ-��
		/// </summary>
		public string  REGISTER_CITY  
		{
			get
			{
				return this.register_city;
			}
			set
			{
				this.register_city=  value;
			}
		}
		
		/// <summary>
		/// ���ڵ�ַ-��/��
		/// </summary>
		public string REGISTER_DISTRICT  
		{
			get
			{
				return this.register_district;
			}
			set
			{
				this.register_district=  value;
			}
		}

	

		/// <summary>
		/// ���ڵ�ַ-���硢�ֵ���
		/// </summary>
		public string REGISTER_STREET 
		{
			get
			{
				return this.register_street;
			}
			set
			{
				this.register_street=  value;
			}
		}
        
		/// <summary>
		/// ���ڵ�ַ����
		/// </summary>
		public string REGISTER_HOUSENUMBER
		{
			get
			{
				return this.register_housenumber;
			}
			set
			{
				this.register_housenumber=  value;
			}
		}

		/// <summary>
		/// ���ڵ�ַ 
		/// </summary>
		public string  REGISTER_POST
		{
			get
			{
				return this.register_post;
			}
			set
			{
				this.register_post=  value;
			}
		}

		/// <summary>
		///  ������ַ
		/// </summary>
		public  string WORK_PLACE  
		{
			get
			{
				return this.work_place;
			}
			set
			{
				this.work_place=  value;
			}
		}

		/// <summary>
		/// ��ϵ��
		/// </summary>
		public string CONTACT_PERSON
		{
			get
			{
				return this.contact_person;
			}
			set
			{
				this.contact_person=  value;
			}
		}
		/// <summary>
		/// ��ϵ�˹�ϵ
		/// </summary>
		public string RELATIONSHIP
		{
			get
			{
				return this.relationship;
			}
			set
			{
				this.relationship=  value;
			}
		}

		/// <summary>
		/// ��ϵ�˵绰
		/// </summary>
		public string CONTACT_PERSON_TEL
		{
			get
			{
				return this.contact_person_tel;
			}
			set
			{
				this.contact_person_tel=  value;
			}
		}
		/// <summary>
		/// ��ϵ�˵�ַ 
		/// </summary>
		public string CONTACT_PERSON_ADDR
		{
			get
			{
				return this.contact_person_addr;
			}
			set
			{
				this.contact_person_addr=  value;
			}
		}

		/*
		/// <summary>
		/// ��ϵ�˵�ַ 
		/// </summary>
		public string CONTACT_PERSON_ADDR
		{
			get
			{
				return this.contact_person_addr;
			}
			set
			{
				this.contact_person_addr=  value;
			}
		}*/	
		/// <summary>
		/// �ٴ�����T 
		/// </summary>
		public string CLINICAL_T
		{
			get
			{
				return this.clinical_t;
			}
			set
			{
				this.clinical_t=  value;
			}
		}
		/// <summary>
		/// �ٴ�����N 
		/// </summary>
		public string CLINICAL_N
		{
			get
			{
				return this.clinical_n;
			}
			set
			{
				this.clinical_n=  value;
			}
		}
		/// <summary>
		/// �ٴ�����M 
		/// </summary>
		public string CLINICAL_M
		{
			get
			{
				return this.clinical_m;
			}
			set
			{
				this.clinical_m=  value;
			}
		}
		/// <summary>
		/// TNM����
		/// </summary>
		 
		public string  TERM_TNM
		{
			get
			{
				return this.term_tnm;
			}
			set
			{
				this.term_tnm=  value;
			}
		}

		/// <summary>
		/// �������־ 
		/// </summary>
		public string PATHOLOGY_CHECK
		{
			get
			{
				return this.pathology_check;
			}
			set
			{
				this.pathology_check=  value;
			}
		}
		/// <summary>
		/// ����� 
		/// </summary>
		public string PATHOLOGY_NO
		{
			get
			{
				return this.pathology_no;
			}
			set
			{
				this.pathology_no=  value;
			}
		}
		/// <summary>
		/// ������� 
		/// </summary>
		public string PATHOLOGY_TYPE
		{
			get
			{
				return this.pathology_type;
			}
			set
			{
				this.pathology_type=  value;
			}
		}
		/// <summary>
		/// �ֻ��̶�
		/// </summary>
		public string PATHOLOGY_DEGREE
		{
			get
			{
				return this.pathology_degree;
			}
			set
			{
				this.pathology_degree=  value;
			}
		}
		/// <summary>
		/// ICD-O
		/// </summary>
		public string ICD_O
		{
			get
			{
				return this.icd_o;
			}
			set
			{
				this.icd_o=  value;
			}
		}


		/*
		/// <summary>
		/// ��������ٴ�
		/// </summary>
		public string DIAGNOSES_CLINCL
		{
			get
			{
				return this.diagnoses_clincl;
			}
			set
			{
				this.diagnoses_clincl=  value;
			}
		}
		/// <summary>
		/// �������X��
		/// </summary>

		public string DIAGNOSES_X
		{
			get
			{
				return this.diagnoses_x;
			}
			set
			{
				this.diagnoses_x=  value;
			}
		}
		/// <summary>
		/// ������ݳ�����
		/// </summary>
			
		public string DIAGNOSES_ULTRASONIC
		{
			get
			{
				return this.diagnoses_ultrasonic;
			}
			set
			{
				this.diagnoses_ultrasonic=  value;
			}
		}
		/// <summary>
		/// ��������ڿ���
		/// </summary>
                
		public string DIAGNOSES_ENDOSCOPY
		{
			get
			{
				return this.diagnoses_endoscopy;
			}
			set
			{
				this.diagnoses_endoscopy=  value;
			}
		}
		/// <summary>
		/// �������CT
		/// </summary>
		public string DIAGNOSES_CT
		{
			get
			{
				return this.diagnoses_ct;
			}
			set
			{
				this.diagnoses_ct=  value;
			}
		}


		/// <summary>
		/// �������PET
		/// </summary>
		public string DIAGNOSES_PET
		{
			get
			{
				return this.diagnoses_pet;
			}
			set
			{
				this.diagnoses_pet=  value;
			}
		}

		/// <summary>
		/// �����������
		/// </summary>
		public string DIAGNOSES_OPS
		{
			get
			{
				return this.diagnoses_ops;
			}
			set
			{
				this.diagnoses_ops=  value;
			}
		}


		/// <summary>
		/// �������ʬ�죨�в���
		/// </summary>
		public string DIAGNOSES_AUTOPSY
		{
			get
			{
				return this.diagnoses_autopsy;
			}
			set
			{
				this.diagnoses_autopsy=  value;
			}
		}

		/// <summary>
		/// �������ʬ�죨�޲���
		/// </summary>
		public string DIAGNOSES_AUTOPSY_NO
		{
			get
			{
				return this.diagnoses_autopsy_no;
			}
			set
			{
				this.diagnoses_autopsy_no=  value;
			}
		}


		/// <summary>
		/// �����������
		/// </summary>
		public string DIAGNOSES_BIOCGEMISTRY
		{
			get
			{
				return this.diagnoses_biocgemistry;
			}
			set
			{
				this.diagnoses_biocgemistry=  value;
			}
		}

		/// <summary>
		/// �����������
		/// </summary>
		public string DIAGNOSES_IMMUNITY
		{
			get
			{
				return this.diagnoses_immunity;
			}
			set
			{
				this.diagnoses_immunity=  value;
			}
		}

		/// <summary>
		/// �������ϸ��
		/// </summary>
		public string DIAGNOSES_CELL
		{
			get
			{
				return this.diagnoses_cell;
			}
			set
			{
				this.diagnoses_cell=  value;
			}
		}

		/// <summary>
		/// �������ѪƬ
		/// </summary>
		public string DIAGNOSES_BLOOD
		{
			get
			{
				return this.diagnoses_blood;
			}
			set
			{
				this.diagnoses_blood=  value;
			}
		}

		/// <summary>
		/// ������ݲ���ԭ����
		/// </summary>
		public string DIAGNOSES_PATHOLOGY_O
		{
			get
			{
				return this.diagnoses_pathology_o;
			}
			set
			{
				this.diagnoses_pathology_o=  value;
			}
		}

		/// <summary>
		/// ������ݲ����̷���
		/// </summary>
		public string DIAGNOSES_PATHOLOGY_S
		{
			get
			{
				return this.diagnoses_pathology_s;
			}
			set
			{
				this.diagnoses_pathology_s=  value;
			}
		}

		/// <summary>
		/// ���������������
		/// </summary>
		public string DIAGNOSES_DEAD
		{
			get
			{
				return this.diagnoses_dead;
			}
			set
			{
				this.diagnoses_dead=  value;
			}
		}

		/// <summary>
		/// ������ݺ˴Ź���
		/// </summary>
		public string DIAGNOSES_MRI
		{
			get
			{
				return this.diagnoses_mri;
			}
			set
			{
				this.diagnoses_mri=  value;
			}
		}

		/// <summary>
		/// ������ݲ���
		/// </summary>
		public string DIAGNOSES_NUKNOWN
		{
			get
			{
				return this.diagnoses_nuknown;
			}
			set
			{
				this.diagnoses_nuknown=  value;
			}
		}

		/// <summary>
		/// �����������
		/// </summary>
		public string DIAGNOSES_OTHER
		{
			get
			{
				return this.diagnoses_other;
			}
			set
			{
				this.diagnoses_other=  value;
			}
		}
        */

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public string TREATMENT
		{
			get
			{
				return this.treatment;
			}
			set
			{
				this.treatment=  value;
			}
		}



		/*
		/// <summary>
		/// 
		/// 
		/// ���Ʒ�ʽ����
		/// </summary>
		public string TREAT_OPS
		{
			get
			{
				return this.treat_ops;
			}
			set
			{
				this.treat_ops=  value;
			}
		}
		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		public string TREAT_CHEMISTRY
		{
			get
			{
				return this.treat_chemistry;
			}
			set
			{
				this.treat_chemistry=  value;
			}
		}
		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		public string TREAT_RADIAL
		{
			get
			{
				return this.treat_radial;
			}
			set
			{
				this.treat_radial=  value;
			}
		}
		/// <summary>
		/// ���Ʒ�ʽ��ҩ
		/// </summary>
		public string TREAT_HERBAL
		{
			get
			{
				return this.treat_herbal;
			}
			set
			{
				this.treat_herbal=  value;
			}
		}
		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		public string TREAT_IMMUNITY
		{
			get
			{
				return this.treat_immunity;
			}
			set
			{
				this.treat_immunity=  value;
			}
		}

		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		public string TREAT_INTERVENTION
		{
			get
			{
				return this.treat_intervention;
			}
			set
			{
				this.treat_intervention=  value;
			}
		}

		/// <summary>
		/// ���Ʒ�ʽ��֢����
		/// </summary>
		public string TREAT_HETEROPATHY
		{
			get
			{
				return this.treat_heteropathy;
			}
			set
			{
				this.treat_heteropathy=  value;
			}
		}

		/// <summary>
		/// ���Ʒ�ʽֹʹ����
		/// </summary>
		public string TREAT_ANODYNE
		{
			get
			{
				return this.treat_anodyne;
			}
			set
			{
				this.treat_anodyne=  value;
			}
		}

		/// <summary>
		/// ���Ʒ�ʽ����
		/// </summary>
		public string TREAT_OTHER
		{
			get
			{
				return this.treat_other;
			}
			set
			{
				this.treat_other=  value;
			}
		}

		/// <summary>
		/// ת��
		/// </summary>
		public string REBACK
		{
			get
			{
				return this.reback;
			}
			set
			{
				this.reback=  value;
			}
		}

		/// <summary>
		/// ת��������ע
		/// </summary>
		public string REBACK_DEMO
		{
			get
			{
				return this.reback_demo;
			}
			set
			{
				this.reback_demo=  value;
			}
		}
		
		*/


		/// <summary>
		/// ����ԭ��
		/// </summary>
		public string DEATH_REASON
		{
			get
			{
				return this.death_reason;
			}
			set
			{
				this.death_reason=  value;
			}
		}

		/// <summary>
		/// ԭ���
		/// </summary>
		public string OLD_DIAGNOSES
		{
			get
			{
				return this.old_diagnoses;
			}
			set
			{
				this.old_diagnoses=  value;
			}
		}

		/// <summary>
		/// ԭ�������
		/// </summary>
		public System.DateTime OLD_DIAGNOSES_DATE
		{
			get
			{
				return this.old_diagnoses_date;
			}
			set
			{
				this.old_diagnoses_date=  value;
			}
		}


		/// <summary>
		/// ����
		/// </summary>
		public string District
		{
			get
			{
				return this.district;
			}
			set
			{
				this.district=  value;
			}
		}

		/// <summary>
		/// �ֻ���
		/// </summary>
		public string HandPhone
		{
			get
			{
				return this.handPhone;
			}
			set
			{
				this.handPhone=  value;
			}
		}




		/// <summary>
		/// ����������չ
		/// </summary>
		public System.Collections.ArrayList ArrayListExt  
		{
			get
			{
				return this.arrayLisExt;
			}
			set
			{
				this.arrayLisExt=  value;
			}
		}
		#endregion

		public CancerAdd()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ��¡����
		public new CancerAdd Clone()
		{
			CancerAdd cancerAdd = base.Clone() as CancerAdd;
			return cancerAdd;
		}
		#endregion

	}
}
