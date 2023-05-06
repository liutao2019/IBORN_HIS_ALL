using System;
using Neusoft.FrameWork.Models;

namespace HISTIMEJOB.XNH.Models
{
	/// <summary>
	/// PYNBMainInfo ��ժҪ˵����
	/// Id inpatientNo, name ��������
	/// </summary>
    public class PYNBMainInfo : Neusoft.FrameWork.Models.NeuObject
	{
		public PYNBMainInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�   
			//
		}
		private int appl_year;
		/// <summary>
		/// Ͷ�����
		/// </summary>
		public int Appl_year
		{
			set
			{
				appl_year = value;
			}
			get
			{
				return appl_year;
			}
		}
		private string branch_no;
		/// <summary>
		/// Ͷ�������
		/// </summary>
		public string Branch_no
		{
			get
			{
				return branch_no;
			}
			set
			{
				branch_no = value;
			}
		}

		private string country_no;
		/// <summary>
		/// Ͷ�������
		/// </summary>
		public string Country_no
		{
			set
			{
				country_no = value;
			}
			get
			{
				return country_no;
			}
		}

		private string poli_no;
		/// <summary>
		/// Ͷ�������嵥���
		/// </summary>
		public string Poli_no
		{
			set{poli_no = value;}
			get{return poli_no;}
		}

		private string hu_no;
		/// <summary>
		///  ���ں�  ֻ��Ϊ9λ
		/// </summary>
		public string Hu_no
		{
			get
			{
				
				return hu_no;
			}
			set{hu_no = value;}
		}

		private string man_no;
		/// <summary>
		/// �����˱��
		/// </summary>
		public string Man_no
		{
			get{return man_no;}
			set{man_no = value;}
		}
		
		private string card_no;
		/// <summary>
		/// ������ũ�Ͽ�
		/// </summary>
		public string Card_no
		{
			get{return card_no;}
			set{card_no = value;}
		}

		private string man_type;
		/// <summary>
		/// -���������
		/// </summary>
		public string Man_type
		{
			get{return man_type;}
			set{man_type = value;}
		}

		private string man_name;
		/// <summary>
		/// ����
		/// </summary>
		public string Man_name
		{
			get{return man_name;}
			set{man_name = value;}
		}

		private string man_id;
		/// <summary>
		/// ���֤��
		/// </summary>
		public string Man_id
		{
			get{return man_id;}
			set{man_id = value;}
		}

		private string man_sex;
		/// <summary>
		/// ��    �� M.��  F.Ů
		/// </summary>
		public string Man_sex
		{
			get{return man_sex;}
			set{man_sex = value;}
		}

		private DateTime man_birth;
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime Man_birth
		{
			get{return man_birth;}
			set{man_birth = value;}
		}		

		private decimal man_age;
		/// <summary>
		/// �б�����
		/// </summary>
		public decimal Man_age
		{
			get{return man_age;}
			set{man_age = value;}
		}

		private DateTime work_date;
		/// <summary>
		/// ¼������
		/// </summary>
		public DateTime Work_date
		{
			get{return work_date;}
			set{work_date = value;}
		}

		private string bank_type;
		/// <summary>
		/// ���ʷ�ʽ:0���˺� A���ֽ�
		///--B�������� C����˾��(�����ʣ�
		/// </summary>
		public string Bank_type
		{
			get{return bank_type;}
			set{bank_type = value;}
		}

		private string bank_fg;
		/// <summary>
		/// --���д���
		/// --�ֽ���:A-��֧Ʊ B-���ֽ�
		/// --������:����(1-6) ��������(A-Z);
		/// --��˾��:��˾�ʻ��ı��(1-9);

		/// </summary>
		public string Bank_fg
		{
			get{return bank_fg;}
			set{bank_fg = value;}
		}

		private string acct_name;
		/// <summary>
		/// -�˺ſ���������
		/// </summary>
		public string Acct_name
		{
			get{return acct_name;}
			set{acct_name = value;}
		}

		private string bank_acct;
		/// <summary>
		/// �����˺�
		/// </summary>
		public string Bank_acct
		{
			get{return bank_acct;}
			set{bank_acct = value;}
		}

		private string bank_name;
		/// <summary>
		/// ������������
		/// </summary>
		public string Bank_name
		{
			get{return bank_name;}
			set{bank_name = value;}
		}

		private DateTime poli_date;
		/// <summary>
		/// ҽ����������
		/// </summary>
		public DateTime Poli_date
		{
			set{poli_date = value;}
			get{return poli_date;}
		}

		private DateTime end_date;
		/// <summary>
		/// ҽ���յ�������
		/// </summary>
		public DateTime End_date
		{
			get{return end_date;}
			set{end_date = value;}
		}
		
		private string medi_mark;
		/// <summary>
		/// ������� 0ͣ�� A-Z���
		/// </summary>
		public string Medi_mark
		{
			set
			{
				medi_mark = value;
			}
			get
			{
				return medi_mark;
			}
		}
		
		private decimal poli_sum;
		/// <summary>
		/// ������������
		/// </summary>
		public decimal Poli_sum
		{
			get{return poli_sum;}
			set{poli_sum = value;}
		}

		private decimal medi_sum;
		/// <summary>
		/// ҽ�Ʊ��ձ���
		/// </summary>
		public decimal Medi_sum
		{
			get{return medi_sum;}
			set{medi_sum = value;}
		}

		private decimal pay_sum;
		/// <summary>
		/// �ϼƱ��ձ���
		/// </summary>
		public decimal Pay_sum
		{
			get{return pay_sum;}
			set{pay_sum = value;}
		}

		private string remarks;
		/// <summary>
		/// ��    ע
		/// </summary>
		public string Remarks
		{
			get
			{
				return remarks;
			}
			set
			{
				remarks = value;
			}
		}

		private string stuff_no;
		/// <summary>
		/// �� �� Ա
		/// </summary>
		public string Stuff_no
		{
			set{stuff_no = value;}
			get{return stuff_no;}
		}
		
		private DateTime sys_date;
		/// <summary>
		/// ϵͳ����
		/// </summary>
		public DateTime Sys_date
		{
			set
			{
				sys_date = value;
			}
			get
			{
				return sys_date;
			}
		}

		private string n_hosp_code;
		/// <summary>
		/// ��ǰסԺ����
		/// </summary>
		public string N_hosp_code
		{
			get
			{
				return n_hosp_code;
			}
			set
			{
				n_hosp_code = value;
			}
		}

		private decimal year_insur_sum;
		/// <summary>
		/// �����ۼƷ���
		/// </summary>
		public decimal Year_insur_sum
		{
			get
			{
				return year_insur_sum;
			}
			set
			{
				year_insur_sum = value;
			}
		}

		private decimal yu_insur_sum;
		/// <summary>
		/// ����Ԥ�����
		/// </summary>
		public decimal Yu_insur_sum
		{
			get
			{
				return yu_insur_sum;
			}
			set
			{
				yu_insur_sum = value;
			}
		}

		private DateTime stock_date;
		/// <summary>
		/// ��Ժ����
		/// </summary>
		public DateTime Stock_date
		{
			get
			{
				return stock_date;
			}
			set
			{
				stock_date = value;
			}
		}

		private DateTime out_date;
		/// <summary>
		/// ��Ժ����
		/// </summary>
		public DateTime Out_date
		{
			get
			{
				return out_date;
			}
			set
			{
				out_date = value;
			}

		}
		private string medi_no;
		/// <summary>
		/// ��ҽ�ǼǺ�
		/// </summary>
		public string Medi_no
		{
			get
			{
				return medi_no;
			}
			set
			{
				medi_no = value;
			}
		}

		private string medi_nn;
		/// <summary>
		/// סԺ��
		/// </summary>
		public string Medi_nn
		{
			get
			{
				return medi_nn;
			}
			set
			{
				medi_nn = value;
			}
		}

		private string medi_sn;
		/// <summary>
		/// ��ˮ��
		/// </summary>
		public string Medi_sn
		{
			get
			{
				return medi_sn;
			}
			set
			{
				medi_sn = value;
			}
		}
		private string hosp_code;
		/// <summary>
		/// ҽԺ����
		/// </summary>
		public string Hosp_code
		{
			get
			{
				return hosp_code;
			}
			set
			{
				hosp_code = value;
			}
		}
		
		private string zykb;
		/// <summary>
		/// ��ס�Ʊ�
		/// </summary>
		public string Zykb
		{
			get
			{
				return zykb;
			}
			set
			{
				zykb = value;
			}
		}

		private string jyzd;
		/// <summary>
		/// ��Ժ���
		/// </summary>
		public string Jyzd
		{
			get
			{
				return jyzd;
			}
			set
			{
				jyzd = value;
			}
		}

		private string turn_flag;
		/// <summary>
		/// �Ƿ�תԺ
		/// </summary>
		public string Turn_flag
		{
			get
			{
				return turn_flag;
			}
			set
			{
				turn_flag = value;
			}
		}

		private string turn_hosp;
		/// <summary>
		/// תԺǰ��ҽԺ����
		/// </summary>
		public string Turn_hosp
		{
			get
			{
				return turn_hosp;
			}
			set
			{
				turn_hosp = value;
			}
		}

		private string regstuff_no;
		/// <summary>
		/// ����Ա����
		/// </summary>
		public string RegStuff_no
		{
			get
			{
				return regstuff_no;
			}
			set
			{
				regstuff_no = value;
			}
		}

		private string stuff_name;
		/// <summary>
		/// ����Ա����
		/// </summary>
		public string Stuff_name
		{
			get
			{
				return stuff_name;
			}
			set
			{
				stuff_name = value;
			}
		}

		private string state_flag;
		/// <summary>
		/// ״̬
		/// </summary>
		public string State_flag
		{
			get
			{
				return state_flag;
			}
			set
			{
				state_flag = value;
			}
		}

		private string rec_state;
		/// <summary>
		/// ��������״̬
		/// </summary>
		public string Rec_state
		{
			get
			{
				return rec_state;
			}
			set
			{
				rec_state = value;
			}
		}

		private int last_ph;
		/// <summary>
		/// �������
		/// </summary>
		public int Last_ph
		{
			get
			{
				return last_ph;
			}
			set
			{
				last_ph = value;
			}
		}

		private string memo1;
		/// <summary>
		/// ��ע1
		/// </summary>
		public string Memo1
		{
			set{memo1 = value;}
			get{return memo1;}
		}

		private string memo2;
		/// <summary>
		/// ��ע2
		/// </summary>
		public string Memo2
		{
			set{memo2 = value;}
			get{return memo2;}
		}

		private string memo3;
		/// <summary>
		/// ��ע3
		/// </summary>
		public string Memo3
		{
			set{memo3 = value;}
			get{return memo3;}
		}
		
        private string outDiagnosis;
		/// <summary>
		/// ��Ժ���
		/// </summary>
		public string OutDiagnosis
		{
			set{outDiagnosis=value;}
			get{return outDiagnosis;}
		}

		private string outConditions;
		/// <summary>
		/// ��Ժ���
		/// </summary>
		public string OutConditions
		{
			set {outConditions=value;}
			get{return outConditions;}
		}

		public class Drawwork
		{
            /// <summary>
            /// ��¼��(ϵͳ����)  ũ������
            /// </summary>
            public string ddata_sn = string.Empty;//��¼��
            /// <summary>
            /// Ͷ�������
            /// </summary>
			public string dbranch_no = string.Empty;
            /// <summary>
            /// Ͷ�������
            /// </summary>
            public string dcountry_no = string.Empty;      //Ͷ�������
            /// <summary>
            /// ��ҽ�ǼǺ�
            /// </summary>
            public string dmedi_no;   //��ҽ�ǼǺ�
            /// <summary>
            /// ��������
            /// </summary>
            public int dPH ;     //��������

            /// <summary>
            ///  0-ҽ�� 1-����
            /// </summary>
            public string dmedi_type = string.Empty;  // 0-ҽ�� 1-����
            /// <summary>
            /// ��������
            /// </summary>
            public DateTime dwork_date ; //��������

            /// <summary>
            /// ҽԺ����
            /// </summary>
            public string dhosp_code= string.Empty;  //ҽԺ����  
            /// <summary>
            /// ҽԺסԺ��
            /// </summary>
            public string dmedi_nn = string.Empty;//ҽԺסԺ�� 

            /// <summary>
            /// ��������
            /// </summary>
            public string dunit_name = string.Empty; // ��������
            /// <summary>
            /// �����
            /// </summary>
            public string ddraw_name = string.Empty; //�����

            /// <summary>
            /// ��������֤����
            /// </summary>
            public string ddraw_id = string.Empty; //��������֤����

            /// <summary>
            /// ��Ժ�ܷ���
            /// </summary>
            public decimal dsum_medi_sum; //��Ժ�ܷ��� 

            /// <summary>
            /// ���÷���������
            /// </summary>
            public DateTime ds_stock_date; //���÷���������

            /// <summary>
            /// ���÷���ֹ����
            /// </summary>
            public DateTime de_stock_date; //���÷���ֹ����

            /// <summary>
            /// �𸶽��
            /// </summary>
            public decimal dstart_pay_sum; //�𸶽�� 

            /// <summary>
            /// Ӧ�����
            /// </summary>
            public decimal dinsur_sum; //Ӧ�����

            /// <summary>
            /// �Ը����
            /// </summary>
            public decimal dmy_pay_sum; //�Ը����

            /// <summary>
            /// ���껹�ɱ����
            /// </summary>
            public decimal dye_insur_sum; //���껹�ɱ����

            /// <summary>
            /// ҵ�����Ա
            /// </summary>
            public string dop_stuff = string.Empty; //ҵ�����Ա

            /// <summary>
            /// ״̬
            /// </summary>
            public string dstate_flag = string.Empty; //״̬ 

            /// <summary>
            /// ��������״̬
            /// </summary>
            public string drec_state = string.Empty; //��������״̬
		}

        /// <summary>
        /// �������������
        /// </summary>
		public class Medilist_data
		{
            /// <summary>
            /// Ͷ�����
            /// </summary>
			public string mappl_year = string.Empty; // Ͷ�����
            /// <summary>
            /// Ͷ�������
            /// </summary>
            public string mbranch_no = string.Empty; //Ͷ�������
            /// <summary>
            /// Ͷ�������
            /// </summary>
            public string mcountry_no= string.Empty; //Ͷ�������
            /// <summary>
            /// ��ҽ�ǼǺ�
            /// </summary>
            public string mmedi_no; //��ҽ�ǼǺ�
            /// <summary>
            /// ��������
            /// </summary>
            public int mPH ;//��������
            /// <summary>
            /// ���
            /// </summary>
            public int mno;//���
            /// <summary>
            /// ��������%�޶�����
            /// </summary>
            public decimal msum1; //��������%�޶�����
            /// <summary>
            /// ��������%�޶�����
            /// </summary>
            public decimal msum2; //��������%�޶�����


            /// <summary>
            /// ��������%
            /// </summary>
            public decimal mrate; //��������%

            /// <summary>
            /// ��Χ���ڿɸ����
            /// </summary>
            public decimal mm_sum1; //��Χ���ڿɸ����

            /// <summary>
            /// ʵ�ʲ������
            /// </summary>
            public decimal mm_sum2; //ʵ�ʲ������

            /// <summary>
            /// ״̬
            /// </summary>
            public string mstate_flage = string.Empty; //״̬ 
            /// <summary>
            /// ״̬
            /// </summary>
            public string mstate_flag = string.Empty; //״̬ 
            /// <summary>
            /// ��������״̬
            /// </summary>
            public string mrec_state = string.Empty;//��������״̬
		}

		public class Hosp_pay
		{
            /// <summary>
            /// ҽԺ����
            /// </summary>
			public string hhosp_code = string.Empty; //ҽԺ����   
            /// <summary>
            /// ҽԺסԺ��
            /// </summary>
            public string hmedi_nn = string.Empty; //ҽԺסԺ�� 
            /// <summary>
            /// ��ҽ�Ǽ�
            /// </summary>
            public string hmedi_no ;//��ҽ�Ǽ�
            
            /// <summary>
            /// ��������
            /// </summary>
            public int hPH; //��������
            /// <summary>
            /// ����
            /// </summary>
            public string hman_name = string.Empty; //����
            /// <summary>
            /// �վݺ���
            /// </summary>
            public string hpay_no = string.Empty;//�վݺ���
            /// <summary>
            /// ��Ժ�ܷ���
            /// </summary>
            public decimal hsum_medi_sum;//��Ժ�ܷ���
            /// <summary>
            /// ����ʱ��
            /// </summary>
            public DateTime hh_pay_date; //����ʱ��

            /// <summary>
            /// ҽԺ��������
            /// </summary>
            public string hh_pay_stuff = string.Empty; //ҽԺ��������

            /// <summary>
            /// ҽԺ���ɴ���
            /// </summary>
            public string hh_pay_stuff_code = string.Empty; //ҽԺ���ɴ���

            /// <summary>
            /// ʵ�����
            /// </summary>
            public decimal hh_pay_sum; //ʵ�����

            /// <summary>
            /// ״̬
            /// </summary>
            public string hstate_flag = string.Empty; //״̬ 

            /// <summary>
            /// ��������״̬
            /// </summary>
            public string hrec_state= string.Empty; //��������״̬ 
            /// <summary>
            /// ũ�����˱�־ 0δ��/1�ѻ�
            /// </summary>
            public string hlb_pay_flag= string.Empty; //ũ�����˱�־ 0δ��/1�ѻ�
		}

		public new PYNBMainInfo Clone()
		{
			PYNBMainInfo obj = base.Clone() as PYNBMainInfo;			
			return obj;
		}
	
	}
}
