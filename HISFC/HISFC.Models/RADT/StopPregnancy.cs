using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.RADT
{
    /// [功能描述: 终止妊娠登记管理实体层]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2012-2-10]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    public class StopPregnancy : FS.FrameWork.Models.NeuObject
    {
        /// 终止妊娠周数
        /// </summary>
        private int weeks = 0;

        /// 终止妊娠周数
        /// </summary>
        public int Weeks
        {
            get { return weeks; }
            set { weeks = value; }
        }

        /// 地区
        /// </summary>
        private FS.FrameWork.Models.NeuObject area;

        /// 地区
        /// </summary>
        public FS.FrameWork.Models.NeuObject Area
        {
            get {
                if (this.area == null)
                {
                    this.area = new FS.FrameWork.Models.NeuObject();
                }
                return area; }
            set { area = value; }
        }

        /// 登记时间
        /// </summary>
        private DateTime regDate = new DateTime();

        /// 登记时间
        /// </summary>
        public DateTime RegDate
        {
            get { return regDate; }
            set { regDate = value; }
        }

        /// 操作环境
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper;

        /// 操作环境
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get {
                if (this.oper == null)
                {
                    this.oper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return oper; 
                
                }
            set { oper = value; }
        }

        /// 深度克隆方法
        /// </summary>
        /// <returns></returns>
        public new StopPregnancy Clone()
        {
            StopPregnancy stoppregnancy = base.Clone() as StopPregnancy;
            stoppregnancy.oper = this.Oper.Clone();
            stoppregnancy.area = this.Area.Clone();

            return stoppregnancy;
        }
    }
}
