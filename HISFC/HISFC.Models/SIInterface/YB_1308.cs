using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1308 : FS.FrameWork.Models.NeuObject
	{
		public YB_1308()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        ///<summary>
        ///手术标准目录id
        ///<summary>
        public string ssbzml_id { get; set; }

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
        ///细目代码
        ///<summary>
        public string xm_code { get; set; }

        ///<summary>
        ///细目名称
        ///<summary>
        public string xm_name { get; set; }

        ///<summary>
        ///手术操作代码
        ///<summary>
        public string oprn_oprt_code { get; set; }

        ///<summary>
        ///手术操作名称
        ///<summary>
        public string oprn_oprt_name { get; set; }

        ///<summary>
        ///使用标记
        ///<summary>
        public string used_std { get; set; }

        ///<summary>
        ///团标版手术操作代码
        ///<summary>
        public string tb_oprn_oprt_code { get; set; }

        ///<summary>
        ///团标版手术操作名称
        ///<summary>
        public string tb_oprn_oprt_name { get; set; }

        ///<summary>
        ///临床版手术操作代码
        ///<summary>
        public string lc_oprn_oprt_code { get; set; }

        ///<summary>
        ///临床版手术操作名称
        ///<summary>
        public string lc_oprn_oprt_name { get; set; }

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
