using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1315 : FS.FrameWork.Models.NeuObject
	{
		public YB_1315()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        ///<summary>
        ///中医证候id
        ///<summary>
        public string zyzh_id { get; set; }

        ///<summary>
        ///证候类目代码
        ///<summary>
        public string zhlm_code { get; set; }

        ///<summary>
        ///证候类目名称
        ///<summary>
        public string zhlm_name { get; set; }

        ///<summary>
        ///证候属性代码
        ///<summary>
        public string zhsx_code { get; set; }

        ///<summary>
        ///证候属性
        ///<summary>
        public string zhsx_name { get; set; }

        ///<summary>
        ///证候分类代码
        ///<summary>
        public string zhfl_code { get; set; }

        ///<summary>
        ///证候分类名称
        ///<summary>
        public string zhfl_name { get; set; }

        ///<summary>
        ///备注
        ///<summary>
        public string mark { get; set; }

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
