using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList ��ժҪ˵����
	/// </summary>
    public class YB_1307 : FS.FrameWork.Models.NeuObject
	{
        public YB_1307()
		{
			//
			// TODO: �ڴ˴����ӹ��캯���߼�
			//
		}
        ///<summary>
        ///��ҽ�������id
        ///<summary>
        public string wm_dise_id { get; set; }

        ///<summary>
        ///��
        ///<summary>
        public string cpr { get; set; }

        ///<summary>
        ///�´��뷶Χ
        ///<summary>
        public string cpr_code_scp { get; set; }

        ///<summary>
        ///������
        ///<summary>
        public string cpr_name { get; set; }

        ///<summary>
        ///�ڴ��뷶Χ
        ///<summary>
        public string sec_code_scp { get; set; }

        ///<summary>
        ///������
        ///<summary>
        public string sec_name { get; set; }

        ///<summary>
        ///��Ŀ����
        ///<summary>
        public string cgy_code { get; set; }

        ///<summary>
        ///��Ŀ����
        ///<summary>
        public string cgy_name { get; set; }

        ///<summary>
        ///��Ŀ����
        ///<summary>
        public string sor_code { get; set; }

        ///<summary>
        ///��Ŀ����
        ///<summary>
        public string sor_name { get; set; }

        ///<summary>
        ///��ϴ���
        ///<summary>
        public string dise_code { get; set; }

        ///<summary>
        ///�������
        ///<summary>
        public string dise_name { get; set; }

        ///<summary>
        ///ʹ�ñ��
        ///<summary>
        public string used_std { get; set; }

        ///<summary>
        ///�������ϴ���
        ///<summary>
        public string gb_dise_code { get; set; }

        ///<summary>
        ///������������
        ///<summary>
        public string gb_dise_name { get; set; }

        ///<summary>
        ///�ٴ�����ϴ���
        ///<summary>
        public string lc_dise_code { get; set; }

        ///<summary>
        ///�ٴ����������
        ///<summary>
        public string lc_dise_name { get; set; }

        ///<summary>
        ///��ע
        ///<summary>
        public string mark { get; set; }

        ///<summary>
        ///��Ч��־
        ///<summary>
        public string valid_state { get; set; }

        ///<summary>
        ///Ψһ��¼��
        ///<summary>
        public string wyjlh { get; set; }

        ///<summary>
        ///���ݴ���ʱ��
        ///<summary>
        public string oper_date { get; set; }

        ///<summary>
        ///���ݸ���ʱ��
        ///<summary>
        public string update_date { get; set; }

        ///<summary>
        ///�汾��
        ///<summary>
        public string ver { get; set; }

        ///<summary>
        ///�汾����
        ///<summary>
        public string ver_name { get; set; }




}
}