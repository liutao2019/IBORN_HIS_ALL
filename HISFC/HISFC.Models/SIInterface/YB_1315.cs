using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList ��ժҪ˵����
	/// </summary>
    public class YB_1315 : FS.FrameWork.Models.NeuObject
	{
		public YB_1315()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        ///<summary>
        ///��ҽ֤��id
        ///<summary>
        public string zyzh_id { get; set; }

        ///<summary>
        ///֤����Ŀ����
        ///<summary>
        public string zhlm_code { get; set; }

        ///<summary>
        ///֤����Ŀ����
        ///<summary>
        public string zhlm_name { get; set; }

        ///<summary>
        ///֤�����Դ���
        ///<summary>
        public string zhsx_code { get; set; }

        ///<summary>
        ///֤������
        ///<summary>
        public string zhsx_name { get; set; }

        ///<summary>
        ///֤��������
        ///<summary>
        public string zhfl_code { get; set; }

        ///<summary>
        ///֤���������
        ///<summary>
        public string zhfl_name { get; set; }

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
