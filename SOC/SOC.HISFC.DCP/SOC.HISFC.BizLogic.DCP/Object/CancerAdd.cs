using System;

namespace FS.HISFC.DCP.Object
{
	/// <summary>
	/// CancerAdd 的摘要说明。
	/// 广州肿瘤报告实体
	/// </summary>
	public class CancerAdd: FS.FrameWork.Models.NeuObject
	{

		#region 私有字段

		/// <summary>
		/// 报卡编号
		/// </summary>
		private string  report_no = "";

		/// <summary>
		/// 医保卡号 
		/// </summary>
		private string meidcal_card = "";

		/// <summary>
		/// 民族
		/// </summary>
		private string nation = "";

		/// <summary>
		/// 工种
		/// </summary>
		private string work_type = "";

		/// <summary>
		/// 婚姻状况  
		/// </summary>
		private string marriage = "";

		/// <summary>
		/// 疾病ICD编码
		/// </summary>
		private string diagnostic_icd = "";

		/// <summary>
		/// 户口地址-省
		/// </summary>
		private string register_province ="";
		/// <summary>
		/// 户口地址-市
		/// </summary>
		private string  register_city = "";
		
		/// <summary>
		/// 户口地址-区/县
		/// </summary>
		private string register_district = "";

	

		/// <summary>
		/// 户口地址-镇（乡、街道）
		/// </summary>
		private string register_street ="";
        
		/// <summary>
		/// 户口地址门牌
		/// </summary>
		private string register_housenumber="";

		/// <summary>
		/// 户口地址 
		/// </summary>
		private string  register_post="" ;

		/// <summary>
		///  工作地址
		/// </summary>
		private  string work_place = "";

		/// <summary>
		/// 联系人
		/// </summary>
		private string contact_person="";
		/// <summary>
		/// 联系人关系
		/// </summary>
		private string relationship="";

		/// <summary>
		/// 联系人电话
		/// </summary>
		private string contact_person_tel="";
		/// <summary>
		/// 联系人地址 
		/// </summary>
		private string contact_person_addr="";

		/*
		/// <summary>
		/// 联系人地址 
		/// </summary>
		private string contact_person_addr="";	
        */

		/// <summary>
		/// 临床分期T 
		/// </summary>
		private string clinical_t="";
		/// <summary>
		/// 临床分期N 
		/// </summary>
		private string clinical_n="";
		/// <summary>
		/// 临床分期M 
		/// </summary>
		private string clinical_m="";
		/// <summary>
		/// TNM分期
		/// </summary>
		 
		private string  term_tnm="";

		/// <summary>
		/// 病理检查标志 
		/// </summary>
		private string pathology_check="";
		/// <summary>
		/// 病理号 
		/// </summary>
		private string pathology_no="";
		/// <summary>
		/// 病理分型 
		/// </summary>
		private string pathology_type="";
		/// <summary>
		/// 分化程度
		/// </summary>
		private string pathology_degree="";
		/// <summary>
		/// ICD-O
		/// </summary>
		private string icd_o="";





		/*
		/// <summary>
		/// 诊断依据临床
		/// </summary>
		private string diagnoses_clincl="";
		/// <summary>
		/// 诊断依据X光
		/// </summary>

		private string diagnoses_x="";
		/// <summary>
		/// 诊断依据超声波
		/// </summary>
			
		private string diagnoses_ultrasonic="";
		/// <summary>
		/// 诊断依据内窥镜
		/// </summary>
                
		private string diagnoses_endoscopy="";
		/// <summary>
		/// 诊断依据CT
		/// </summary>
		private string diagnoses_ct="";


		/// <summary>
		/// 诊断依据PET
		/// </summary>
		private string diagnoses_pet="";

		/// <summary>
		/// 诊断依据手术
		/// </summary>
		private string diagnoses_ops="";


		/// <summary>
		/// 诊断依据尸检（有病理）
		/// </summary>
		private string diagnoses_autopsy="";

		/// <summary>
		/// 诊断依据尸检（无病理）
		/// </summary>
		private string diagnoses_autopsy_no="";


		/// <summary>
		/// 诊断依据生化
		/// </summary>
		private string diagnoses_biocgemistry="";

		/// <summary>
		/// 诊断依据免疫
		/// </summary>
		private string diagnoses_immunity="";

		/// <summary>
		/// 诊断依据细胞
		/// </summary>
		private string diagnoses_cell="";

		/// <summary>
		/// 诊断依据血片
		/// </summary>
		private string diagnoses_blood="";

		/// <summary>
		/// 诊断依据病理（原发）
		/// </summary>
		private string diagnoses_pathology_o="";

		/// <summary>
		/// 诊断依据病理（继发）
		/// </summary>
		private string diagnoses_pathology_s="";

		/// <summary>
		/// 诊断依据死亡补发
		/// </summary>
		private string diagnoses_dead="";

		/// <summary>
		/// 诊断依据核磁共振
		/// </summary>
		private string diagnoses_mri="";

		/// <summary>
		/// 诊断依据不详
		/// </summary>
		private string diagnoses_nuknown="";

		/// <summary>
		/// 诊断依据其他
		/// </summary>
		private string diagnoses_other="";
		*/

		/// <summary>
		/// 是否治疗
		/// </summary>
		private string treatment="";

		/*
		/// <summary>
		/// 治疗方式手术
		/// </summary>
		private string treat_ops="";
		/// <summary>
		/// 治疗方式化疗
		/// </summary>
		private string treat_chemistry="";
		/// <summary>
		/// 治疗方式放射
		/// </summary>
		private string treat_radial="";
		/// <summary>
		/// 治疗方式中药
		/// </summary>
		private string treat_herbal="";
		/// <summary>
		/// 治疗方式免疫
		/// </summary>
		private string treat_immunity="";

		/// <summary>
		/// 治疗方式介入
		/// </summary>
		private string treat_intervention="";

		/// <summary>
		/// 治疗方式对症治疗
		/// </summary>
		private string treat_heteropathy="";

		/// <summary>
		/// 治疗方式止痛治疗
		/// </summary>
		private string treat_anodyne="";

		/// <summary>
		/// 治疗方式其他
		/// </summary>
		private string treat_other="";

		/// <summary>
		/// 转归
		/// </summary>
		private string reback="";

		/// <summary>
		/// 转归其他备注
		/// </summary>
		private string reback_demo="";
		*/
		/// <summary>
		/// 死亡原因
		/// </summary>
		private string death_reason="";

		/// <summary>
		/// 原诊断
		/// </summary>
		private string old_diagnoses="";

		/// <summary>
		/// 原诊断日期
		/// </summary>
		private System.DateTime old_diagnoses_date;

		/// <summary>
		/// 籍贯
		/// </summary>
		private string district;

		/// <summary>
		/// 手机号
		/// </summary>
		private string handPhone;



        /// <summary>
        /// 肿瘤扩展表信息
        /// </summary>
		private System.Collections .ArrayList  arrayLisExt = new System.Collections.ArrayList();


		#endregion

		#region 公有字段

		/// <summary>
		/// 报卡编号
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
		/// 医保卡号 
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
		/// 民族
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
		/// 工种
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
		/// 婚姻状况  
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
		/// 疾病ICD编码
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
		/// 户口地址-省
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
		/// 户口地址-市
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
		/// 户口地址-区/县
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
		/// 户口地址-镇（乡、街道）
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
		/// 户口地址门牌
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
		/// 户口地址 
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
		///  工作地址
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
		/// 联系人
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
		/// 联系人关系
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
		/// 联系人电话
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
		/// 联系人地址 
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
		/// 联系人地址 
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
		/// 临床分期T 
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
		/// 临床分期N 
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
		/// 临床分期M 
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
		/// TNM分期
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
		/// 病理检查标志 
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
		/// 病理号 
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
		/// 病理分型 
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
		/// 分化程度
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
		/// 诊断依据临床
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
		/// 诊断依据X光
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
		/// 诊断依据超声波
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
		/// 诊断依据内窥镜
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
		/// 诊断依据CT
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
		/// 诊断依据PET
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
		/// 诊断依据手术
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
		/// 诊断依据尸检（有病理）
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
		/// 诊断依据尸检（无病理）
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
		/// 诊断依据生化
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
		/// 诊断依据免疫
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
		/// 诊断依据细胞
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
		/// 诊断依据血片
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
		/// 诊断依据病理（原发）
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
		/// 诊断依据病理（继发）
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
		/// 诊断依据死亡补发
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
		/// 诊断依据核磁共振
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
		/// 诊断依据不详
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
		/// 诊断依据其他
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
		/// 是否治疗
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
		/// 治疗方式手术
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
		/// 治疗方式化疗
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
		/// 治疗方式放射
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
		/// 治疗方式中药
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
		/// 治疗方式免疫
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
		/// 治疗方式介入
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
		/// 治疗方式对症治疗
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
		/// 治疗方式止痛治疗
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
		/// 治疗方式其他
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
		/// 转归
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
		/// 转归其他备注
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
		/// 死亡原因
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
		/// 原诊断
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
		/// 原诊断日期
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
		/// 籍贯
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
		/// 手机号
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
		/// 肿瘤报卡扩展
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
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 克隆函数
		public new CancerAdd Clone()
		{
			CancerAdd cancerAdd = base.Clone() as CancerAdd;
			return cancerAdd;
		}
		#endregion

	}
}
