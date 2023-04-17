using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1314 : FS.FrameWork.Models.NeuObject
	{
        public YB_1314()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        ///<summary>
        ///中医疾病诊断id
        ///<summary>
        public string zyjbzd_id { get; set; }

        ///<summary>
        ///科别类目代码
        ///<summary>
        public string kblm_code { get; set; }

        ///<summary>
        ///科别类目名称
        ///<summary>
        public string kblm_name { get; set; }

        ///<summary>
        ///专科系统分类目代码
        ///<summary>
        public string zkxtfl_code { get; set; }

        ///<summary>
        ///专科系统分类目名称
        ///<summary>
        public string zkxtfl_name { get; set; }

        ///<summary>
        ///疾病分类代码
        ///<summary>
        public string jbfl_code { get; set; }

        ///<summary>
        ///疾病分类名称
        ///<summary>
        public string jbfl_name { get; set; }

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
