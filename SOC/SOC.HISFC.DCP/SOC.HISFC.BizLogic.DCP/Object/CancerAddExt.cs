using System;

namespace FS.HISFC.DCP.Object
{
	/// <summary>
	/// CancerAddExt 的摘要说明。
	/// 肿瘤报告扩展
	/// </summary>
    public class CancerAddExt : FS.FrameWork.Models.NeuObject
    {


        #region 私有变量
        /// <summary>
        /// 报表编号
        /// </summary>
        private string report_no;
        /// <summary>
        /// 扩展分类代码
        /// </summary>
        private string class_code;
        /// <summary>
        /// 扩展分类名称
        /// </summary>
        private string class_name;
        /// <summary>
        /// 项目代码
        /// </summary>
        private string item_code;

        /// <summary>
        /// 项目名称
        /// </summary>
        private string item_name;

        /// <summary>
        /// 项目备注
        /// </summary>
        private string item_demo;

        #endregion

        #region 公共属性
        /// <summary>
        /// 报表编号
        /// </summary>
        public string Report_No
        {
            set { this.report_no = value; }
            get { return this.report_no; }
        }

        /// <summary>
        /// 扩展分类代码
        /// </summary>
        public string Class_Code
        {
            set { this.class_code = value; }
            get { return this.class_code; }
        }
        /// <summary>
        /// 扩展分类名称
        /// </summary>

        public string Class_Name
        {
            set { this.class_name = value; }
            get { return this.class_name; }
        }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string Item_Code
        {
            set { this.item_code = value; }
            get { return this.item_code; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Item_Name
        {
            set { this.item_name = value; }
            get { return this.item_name; }
        }

        /// <summary>
        /// 项目备注
        /// </summary>
        public string Item_Demo
        {
            set { this.item_demo = value; }
            get { return this.item_demo; }
        }

        #endregion
        public CancerAddExt()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public new CancerAddExt Clone()
        {
            CancerAddExt cancerAddExt = base.Clone() as CancerAddExt;
            return cancerAddExt;

        }
    }
}
