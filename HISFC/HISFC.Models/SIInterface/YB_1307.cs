using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1307 : FS.FrameWork.Models.NeuObject
	{
        public YB_1307()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        ///<summary>
        ///西医疾病诊断id
        ///<summary>
        public string wm_dise_id { get; set; }

        ///<summary>
        ///章
        ///<summary>
        public string cpr { get; set; }

        ///<summary>
        ///章代码范围
        ///<summary>
        public string cpr_code_scp { get; set; }

        ///<summary>
        ///章名称
        ///<summary>
        public string cpr_name { get; set; }

        ///<summary>
        ///节代码范围
        ///<summary>
        public string sec_code_scp { get; set; }

        ///<summary>
        ///节名称
        ///<summary>
        public string sec_name { get; set; }

        ///<summary>
        ///类目代码
        ///<summary>
        public string cgy_code { get; set; }

        ///<summary>
        ///类目名称
        ///<summary>
        public string cgy_name { get; set; }

        ///<summary>
        ///亚目代码
        ///<summary>
        public string sor_code { get; set; }

        ///<summary>
        ///亚目名称
        ///<summary>
        public string sor_name { get; set; }

        ///<summary>
        ///诊断代码
        ///<summary>
        public string dise_code { get; set; }

        ///<summary>
        ///诊断名称
        ///<summary>
        public string dise_name { get; set; }

        ///<summary>
        ///使用标记
        ///<summary>
        public string used_std { get; set; }

        ///<summary>
        ///国标版诊断代码
        ///<summary>
        public string gb_dise_code { get; set; }

        ///<summary>
        ///国标版诊断名称
        ///<summary>
        public string gb_dise_name { get; set; }

        ///<summary>
        ///临床版诊断代码
        ///<summary>
        public string lc_dise_code { get; set; }

        ///<summary>
        ///临床版诊断名称
        ///<summary>
        public string lc_dise_name { get; set; }

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
