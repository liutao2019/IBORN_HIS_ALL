using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList ��ժҪ˵����
	/// </summary>
    public class YB_1305 : FS.FrameWork.Models.NeuObject
	{
		public YB_1305()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        ///<summary>
        ///ҽ��Ŀ¼����
        ///<summary>
        public string med_list_codg { get; set; }

        ///<summary>
        ///�Ƽ۵�λ
        ///<summary>
        public string prcunt { get; set; }

        ///<summary>
        ///�Ƽ۵�λ����
        ///<summary>
        public string prcunt_name { get; set; }

        ///<summary>
        ///������Ŀ˵��
        ///<summary>
        public string trt_item_dscr { get; set; }

        ///<summary>
        ///���Ƴ�������
        ///<summary>
        public string trt_exct_cont { get; set; }

        ///<summary>
        ///������Ŀ�ں�
        ///<summary>
        public string trt_item_cont { get; set; }

        ///<summary>
        ///��Ч��־
        ///<summary>
        public string vali_flag { get; set; }

        ///<summary>
        ///��ע
        ///<summary>
        public string memo { get; set; }

        ///<summary>
        ///������Ŀ���
        ///<summary>
        public string servitem_type { get; set; }

        ///<summary>
        ///ҽ�Ʒ�����Ŀ����
        ///<summary>
        public string servitem_name { get; set; }

        ///<summary>
        ///��Ŀ˵��
        ///<summary>
        public string item_name { get; set; }

        ///<summary>
        ///��ʼ����
        ///<summary>
        public string begin_time { get; set; }

        ///<summary>
        ///��������
        ///<summary>
        public string end_time { get; set; }

        ///<summary>
        ///Ψһ��¼��
        ///<summary>
        public string record_num { get; set; }

        ///<summary>
        ///�汾��
        ///<summary>
        public string ver_num { get; set; }

        ///<summary>
        ///�汾����
        ///<summary>
        public string ver_name { get; set; }



    }
}
