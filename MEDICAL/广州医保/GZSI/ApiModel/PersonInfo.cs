using System;
using System.Collections.Generic;
using System.Text;

namespace GZSI.ApiModel
{
    /// <summary>
    /// ���˻�����Ϣ
    /// </summary>
    public class PersonInfo
    {
        private string indi_id=string.Empty;
        /// <summary>
        /// ���˵��Ժ�
        /// </summary>
        public string Api_indi_id
        {
            get
            {
                return indi_id;
            }
            set { indi_id = value; }
        }
        private string name = string.Empty;
        /// <summary>
        /// ����
        /// </summary>
        public string Api_name
        {
            get
            {
                return name;
            }
            set { name = value; }
        }

        private string sex=string.Empty;
        /// <summary>
        /// �Ա�
        /// </summary>
        public string Api_sex
        {
            get
            {
                return sex;
            }
            set { sex = value; }
        }


        private string pers_identity=string.Empty;
        /// <summary>
        /// ��Ա���
        /// </summary>
        public string Api_pers_identity
        {
            get
            {
                return pers_identity;
            }
            set { pers_identity = value; }
        }

        private string pers_status=string.Empty;
        /// <summary>
        /// ��Ա״̬
        /// </summary>
        public string Api_pers_status
        {
            get
            {
                return pers_status;
            }
            set { pers_status = value; }
        }

        private string office_grade=string.Empty;
        /// <summary>
        /// ����
        /// </summary>
        public string Api_office_grade
        {
            get
            {
                return office_grade;
            }
            set { office_grade = value; }
        }

        private string idcard=string.Empty;
        /// <summary>
        /// ������ݺ���
        /// </summary>
        public string Api_idcard
        {
            get
            {
                return idcard;
            }
            set { idcard = value; }
        }

        private string telephone=string.Empty;
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string Api_telephone
        {
            get
            {
                return telephone;
            }
            set { telephone = value; }
        }

        private string birthday=string.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string Api_birthday
        {
            get
            {
                return birthday;
            }
            set { birthday = value; }
        }

        private string post_code=string.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string Api_post_code
        {
            get
            {
                return post_code;
            }
            set { post_code = value; }
        }


        private string corp_id=string.Empty;
        /// <summary>
        /// ��λ����
        /// </summary>
        public string Api_corp_id
        {
            get
            {
                return corp_id;
            }
            set { corp_id = value; }
        }

        private string corp_name=string.Empty;
        /// <summary>
        /// ��λ����
        /// </summary>
        public string Api_corp_name
        {
            get
            {
                return corp_name;
            }
            set { corp_name = value; }
        }

        private string last_balance = string.Empty;
        /// <summary>
        /// �����ʻ����
        /// </summary>
        public string Api_last_balance
        {
            get
            {
                return last_balance;
            }
            set { last_balance = value; }
        }



        ///////////////////////////////////////////////
        //�����ʻ�������Ϣ
        //////////////////////////////////////////////

        private string fund_id = string.Empty;
        /// <summary>
        /// ������ max = 3
        /// </summary>
        public string Api_fund_id
        {
            get
            {
                return fund_id;
            }
            set { fund_id = value; }
        }

        private string fund_name = string.Empty;
        /// <summary>
        /// �������� max = 30
        /// </summary>
        public string Api_fund_name
        {
            get
            {
                return fund_name;
            }
            set { fund_name = value; }
        }

        private string indi_freeze_status = string.Empty;
        /// <summary>
        /// ����״̬��־ max = 1
        /// </summary>
        public string Api_indi_freeze_status
        {
            get
            {
                //switch (indi_freeze_status)
                //{
                //    case "0":
                //        return "����";
                //    case "1":
                //        return "����";
                //    case "2":
                //        return "��ͣ�α�";
                //    case "3":
                //        return "��ֹ�α�";
                //    case "4":
                //        return "δ�α�";
                //    default:
                //        return "";
                //}
                return indi_freeze_status;
            }
            set 
            {
                indi_freeze_status = value; 
            }
        }


        //////////////////////
        //����ѡ����Ϣclinicapplyinfo
        /////////////////////
        private string serial_apply = string.Empty;

        /// <summary>
        /// ����ѡ���������к�(�����������к�)
        /// </summary>
        public string Api_serial_apply
        {
            get { return serial_apply; }
            set
            {
                serial_apply = value;
            }
        }


        /////////////////////////////
        //�����϶���Ϣinjuryorbirthinfo�������϶���Ϣ��
        /////////////////////////////
        private string serial_mn = string.Empty;
        /// <summary>
        /// ����ҽ��ƾ֤��
        /// </summary>
        public string Api_serial_mn
        {
            get { return serial_mn; }
            set { serial_mn = value; }
        }

        private string serial_wi = string.Empty;
        /// <summary>
        /// ���˾�ҽƾ֤��(���湤������ҵ����Ϣʱ����ֵ��Ϊ������injuryorbirth_serial����ֵ����)
        /// </summary>
        public string Api_serial_wi
        {
            get { return serial_wi; }
            set { serial_wi = value; }
        }

        private string begin_date = string.Empty;
        /// <summary>
        /// ҽ���ڿ�ʼʱ��(��ʽ����yyyy-mm-dd��)
        /// </summary>
        public string Api_begin_date
        {
            get { return begin_date; }
            set { begin_date = value; }
        }

        private string end_date = string.Empty;
        /// <summary>
        /// ҽ���ڽ���ʱ��(��ʽ����yyyy-mm-dd��)
        /// </summary>
        public string Api_end_date
        {
            get { return end_date; }
            set { end_date = value; }
        }

        /////////////////////////////////////
        //����������Ϣ
        /////////////////////////////////////
        private string treatment_type = string.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string Api_treatment_type
        {
            get { return treatment_type; }
            set { treatment_type = value; }
        }


        private string treatment_name = string.Empty;
        /// <summary>
        /// ������������
        /// </summary>
        public string Api_treatment_name
        {
            get { return treatment_name; }
            set { treatment_name = value; }
        }


        private string biz_type = string.Empty;
        /// <summary>
        /// ҵ������
        /// </summary>
        public string Api_biz_type
        {
            get { return biz_type; }
            set { biz_type = value; }
        }


        private string icd = string.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string Api_icd
        {
            get { return icd; }
            set { icd = value; }
        }

        private string disease = string.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string Api_disease
        {
            get { return disease; }
            set { disease = value; }
        }

        private string admit_effect = string.Empty;
        /// <summary>
        /// ������Чʱ��
        /// </summary>
        public string Api_admit_effect
        {
            get { return admit_effect; }
            set { admit_effect = value; }
        }

        private string admit_date = string.Empty;
        /// <summary>
        /// ���뵽��ʱ��
        /// </summary>
        public string Api_admit_date
        {
            get { return admit_date; }
            set { admit_date = value; }
        }


    }
}
