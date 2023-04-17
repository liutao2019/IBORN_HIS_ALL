using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1310 : FS.FrameWork.Models.NeuObject
	{
        public YB_1310()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        ///<summary>
        ///病种结算目录id
        ///<summary>
        public string bzjsml_id { get; set; }

        ///<summary>
        ///按病种结算病种目录代码
        ///<summary>
        public string bzjsml_code { get; set; }

        ///<summary>
        ///按病种结算病种名称
        ///<summary>
        public string bzjsml_name { get; set; }

        ///<summary>
        ///限定手术操作代码
        ///<summary>
        public string ydsscz_code { get; set; }

        ///<summary>
        ///限定手术操作名称
        ///<summary>
        public string ydsscz_name { get; set; }

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
        ///病种内涵
        ///<summary>
        public string bznh { get; set; }

        ///<summary>
        ///备注
        ///<summary>
        public string mark { get; set; }

        ///<summary>
        ///版本名称
        ///<summary>
        public string ver_name { get; set; }

        ///<summary>
        ///诊疗指南页码
        ///<summary>
        public string zlznym { get; set; }

        ///<summary>
        ///诊疗指南电子档案
        ///<summary>
        public string zlzndzda { get; set; }




}
}
