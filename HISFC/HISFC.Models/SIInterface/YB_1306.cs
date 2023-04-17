using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList ��ժҪ˵����
	/// </summary>
    public class YB_1306 : FS.FrameWork.Models.NeuObject
	{
        public YB_1306()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        ///<summary>
        ///ҽ��Ŀ¼����
        ///<summary>
        public string wm_dise_id { get; set; }

        ///<summary>
        ///�Ĳ�����
        ///<summary>
        public string cpr { get; set; }

        ///<summary>
        ///ҽ����еΨһ��ʶ��
        ///<summary>
        public string cpr_code_scp { get; set; }

        ///<summary>
        ///ҽ��ͨ��������
        ///<summary>
        public string cpr_name { get; set; }

        ///<summary>
        ///ҽ��ͨ����
        ///<summary>
        public string sec_code_scp { get; set; }

        ///<summary>
        ///��Ʒ�ͺ�
        ///<summary>
        public string prod_mol { get; set; }

        ///<summary>
        ///������
        ///<summary>
        public string spec_code { get; set; }

        ///<summary>
        ///���
        ///<summary>
        public string spec { get; set; }

        ///<summary>
        ///�Ĳķ���
        ///<summary>
        public string mcs_type { get; set; }

        ///<summary>
        ///����ͺ�
        ///<summary>
        public string spec_mol { get; set; }

        ///<summary>
        ///���ʴ���
        ///<summary>
        public string dise_code { get; set; }

        ///<summary>
        ///�ĲĲ���
        ///<summary>
        public string mcs_matl { get; set; }

        ///<summary>
        ///��װ���
        ///<summary>
        public string pacspec { get; set; }

        ///<summary>
        ///��װ����
        ///<summary>
        public string pac_cnt { get; set; }

        ///<summary>
        ///��Ʒ��װ����
        ///<summary>
        public string prod_pacmatl { get; set; }

        ///<summary>
        ///��װ��λ
        ///<summary>
        public string pacunt { get; set; }

        ///<summary>
        ///��Ʒת����
        ///<summary>
        public string prod_convrat { get; set; }

        ///<summary>
        ///��Сʹ�õ�λ
        ///<summary>
        public string min_useunt { get; set; }

        ///<summary>
        ///���������
        ///<summary>
        public string prodplac_type { get; set; }

        ///<summary>
        ///�������������
        ///<summary>
        public string prodplac_name { get; set; }

        ///<summary>
        ///��Ʒ��׼
        ///<summary>
        public string cpbz { get; set; }

        ///<summary>
        ///��Ʒ��Ч��
        ///<summary>
        public string prodexpy { get; set; }

        ///<summary>
        ///���ܽṹ�����
        ///<summary>
        public string xnjgyzc { get; set; }

        ///<summary>
        ///���÷�Χ
        ///<summary>
        public string syfw { get; set; }

        ///<summary>
        ///��Ʒʹ�÷���
        ///<summary>
        public string cpsyff { get; set; }

        ///<summary>
        ///��ƷͼƬ���
        ///<summary>
        public string cptpbh { get; set; }

        ///<summary>
        ///��Ʒ������׼
        ///<summary>
        public string cpzlbz { get; set; }

        ///<summary>
        ///˵����
        ///<summary>
        public string manl { get; set; }

        ///<summary>
        ///����֤������
        ///<summary>
        public string qtzmcl { get; set; }

        ///<summary>
        ///ר��ר�ñ�־
        ///<summary>
        public string zjzybz { get; set; }

        ///<summary>
        ///ר������
        ///<summary>
        public string zj_name { get; set; }

        ///<summary>
        ///��������
        ///<summary>
        public string zt_name { get; set; }

        ///<summary>
        ///���ױ�־
        ///<summary>
        public string zt_flag { get; set; }

        ///<summary>
        ///����ʹ�ñ�־
        ///<summary>
        public string lmt_used_flag { get; set; }

        ///<summary>
        ///ҽ�����÷�Χ
        ///<summary>
        public string lmt_usescp { get; set; }

        ///<summary>
        ///��С���۵�λ
        ///<summary>
        public string min_salunt { get; set; }

        ///<summary>
        ///��ֵ�Ĳı�־
        ///<summary>
        public string highval_mcs_flag { get; set; }

        ///<summary>
        ///ҽ�ò��Ϸ������
        ///<summary>
        public string yyclfl_code { get; set; }

        ///<summary>
        ///ֲ����Ϻ��������ٱ�־
        ///<summary>
        public string impt_matl_hmorgn_flag { get; set; }

        ///<summary>
        ///�����־
        ///<summary>
        public string mj_flag { get; set; }

        ///<summary>
        ///�����־����
        ///<summary>
        public string mj_name { get; set; }

        ///<summary>
        ///ֲ���������־
        ///<summary>
        public string impt_itvt_clss_flag { get; set; }

        ///<summary>
        ///ֲ������������
        ///<summary>
        public string impt_itvt_clss_name { get; set; }

        ///<summary>
        ///һ����ʹ�ñ�־
        ///<summary>
        public string dspo_used_flag { get; set; }

        ///<summary>
        ///һ����ʹ�ñ�־����
        ///<summary>
        public string dspo_used_name { get; set; }

        ///<summary>
        ///ע�ᱸ��������
        ///<summary>
        public string rzcbar_name { get; set; }

        ///<summary>
        ///��ʼ����
        ///<summary>
        public string begndate { get; set; }

        ///<summary>
        ///��������
        ///<summary>
        public string enddate { get; set; }

        ///<summary>
        ///ҽ����е�������
        ///<summary>
        public string ylqxgllb_flag { get; set; }

        ///<summary>
        ///ҽ����е�����������
        ///<summary>
        public string ylqxgllb_name { get; set; }

        ///<summary>
        ///ע�ᱸ����
        ///<summary>
        public string reg_fil_no { get; set; }

        ///<summary>
        ///ע�ᱸ����Ʒ����
        ///<summary>
        public string reg_fil_name { get; set; }

        ///<summary>
        ///�ṹ�����
        ///<summary>
        public string jgjzc { get; set; }

        ///<summary>
        ///��������
        ///<summary>
        public string qtnr { get; set; }

        ///<summary>
        ///��׼����
        ///<summary>
        public string aprv_date { get; set; }

        ///<summary>
        ///ע�ᱸ����ס��
        ///<summary>
        public string zcbar_addr { get; set; }

        ///<summary>
        ///ע��֤��Ч�ڿ�ʼʱ��
        ///<summary>
        public string zcz_begndate { get; set; }

        ///<summary>
        ///ע��֤��Ч�ڽ���ʱ��
        ///<summary>
        public string zcz_enddate { get; set; }

        ///<summary>
        ///������ҵ���
        ///<summary>
        public string scqy_code { get; set; }

        ///<summary>
        ///������ҵ����
        ///<summary>
        public string scqy_name { get; set; }

        ///<summary>
        ///������ַ
        ///<summary>
        public string sc_addr { get; set; }

        ///<summary>
        ///��������ҵ
        ///<summary>
        public string dlrqy { get; set; }

        ///<summary>
        ///��������ҵ��ַ
        ///<summary>
        public string dlrqy_addr { get; set; }

        ///<summary>
        ///�����������
        ///<summary>
        public string scghdq { get; set; }

        ///<summary>
        ///�ۺ�������
        ///<summary>
        public string shfwjg { get; set; }

        ///<summary>
        ///ע��򱸰�֤���ӵ���
        ///<summary>
        public string zchbazdzda { get; set; }

        ///<summary>
        ///��ƷӰ��
        ///<summary>
        public string cpyx { get; set; }

        ///<summary>
        ///��Ч��־
        ///<summary>
        public string valid_state { get; set; }

        ///<summary>
        ///Ψһ��¼��
        ///<summary>
        public string wyjlh { get; set; }

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
