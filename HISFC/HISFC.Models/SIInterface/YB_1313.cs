using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList ��ժҪ˵����
	/// </summary>
    public class YB_1313 : FS.FrameWork.Models.NeuObject
	{
		public YB_1313()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        ///<summary>
        ///������̬ѧid
        ///<summary>
        public string zlxtx_id { get; set; }

        ///<summary>
        ///����/ϸ�����ʹ���
        ///<summary>
        public string zllx_code { get; set; }

        ///<summary>
        ///����/ϸ������
        ///<summary>
        public string zllx_name { get; set; }

        ///<summary>
        ///��̬ѧ�������
        ///<summary>
        public string xtxfl_code { get; set; }

        ///<summary>
        ///��̬ѧ����
        ///<summary>
        public string xtxfl_name { get; set; }

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
