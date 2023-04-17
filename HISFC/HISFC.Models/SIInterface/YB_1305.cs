using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1305 : FS.FrameWork.Models.NeuObject
	{
		public YB_1305()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        ///<summary>
        ///医疗目录编码
        ///<summary>
        public string med_list_codg { get; set; }

        ///<summary>
        ///计价单位
        ///<summary>
        public string prcunt { get; set; }

        ///<summary>
        ///计价单位名称
        ///<summary>
        public string prcunt_name { get; set; }

        ///<summary>
        ///诊疗项目说明
        ///<summary>
        public string trt_item_dscr { get; set; }

        ///<summary>
        ///诊疗除外内容
        ///<summary>
        public string trt_exct_cont { get; set; }

        ///<summary>
        ///诊疗项目内涵
        ///<summary>
        public string trt_item_cont { get; set; }

        ///<summary>
        ///有效标志
        ///<summary>
        public string vali_flag { get; set; }

        ///<summary>
        ///备注
        ///<summary>
        public string memo { get; set; }

        ///<summary>
        ///服务项目类别
        ///<summary>
        public string servitem_type { get; set; }

        ///<summary>
        ///医疗服务项目名称
        ///<summary>
        public string servitem_name { get; set; }

        ///<summary>
        ///项目说明
        ///<summary>
        public string item_name { get; set; }

        ///<summary>
        ///开始日期
        ///<summary>
        public string begin_time { get; set; }

        ///<summary>
        ///结束日期
        ///<summary>
        public string end_time { get; set; }

        ///<summary>
        ///唯一记录号
        ///<summary>
        public string record_num { get; set; }

        ///<summary>
        ///版本号
        ///<summary>
        public string ver_num { get; set; }

        ///<summary>
        ///版本名称
        ///<summary>
        public string ver_name { get; set; }



    }
}
