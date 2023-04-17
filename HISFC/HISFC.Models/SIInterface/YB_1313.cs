using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1313 : FS.FrameWork.Models.NeuObject
	{
		public YB_1313()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        ///<summary>
        ///肿瘤形态学id
        ///<summary>
        public string zlxtx_id { get; set; }

        ///<summary>
        ///肿瘤/细胞类型代码
        ///<summary>
        public string zllx_code { get; set; }

        ///<summary>
        ///肿瘤/细胞类型
        ///<summary>
        public string zllx_name { get; set; }

        ///<summary>
        ///形态学分类代码
        ///<summary>
        public string xtxfl_code { get; set; }

        ///<summary>
        ///形态学分类
        ///<summary>
        public string xtxfl_name { get; set; }

        ///<summary>
        ///有效标志
        ///<summary>
        public string valid_state { get; set; }

        ///<summary>
        ///唯一记录号
        ///<summary>
        public string wyjlh { get; set; }

        ///<summary>
        ///数据创建时间
        ///<summary>
        public string oper_date { get; set; }

        ///<summary>
        ///数据更新时间
        ///<summary>
        public string update_date { get; set; }

        ///<summary>
        ///版本号
        ///<summary>
        public string ver { get; set; }

        ///<summary>
        ///版本名称
        ///<summary>
        public string ver_name { get; set; }




}
}
