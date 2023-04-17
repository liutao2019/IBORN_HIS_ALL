using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Models
{
    /// <summary>
    /// [功能描述: 非药品补充实体]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    [System.ComponentModel.DisplayName("默认执行科室字典")]
    [Serializable]
    public class DefaultExecDept : FS.FrameWork.Models.NeuObject
    {
        private FS.FrameWork.Models.NeuObject compare = new FS.FrameWork.Models.NeuObject();
        private FS.HISFC.Models.Base.Spell original = new FS.HISFC.Models.Base.Spell();
        private FS.HISFC.Models.Base.Spell target = new FS.HISFC.Models.Base.Spell();
        private FS.HISFC.Models.Base.Spell target2 = new FS.HISFC.Models.Base.Spell();

        public FS.FrameWork.Models.NeuObject Compare
        {
            get { return compare; }
            set { compare = value; }
        }
        public FS.HISFC.Models.Base.Spell Original
        {
            get { return original; }
            set { original = value; }
        }
        public FS.HISFC.Models.Base.Spell Target
        {
            get { return target; }
            set { target = value; }
        }
        public FS.HISFC.Models.Base.Spell Target2
        {
            get { return target2; }
            set { target2 = value; }
        }

        public new DefaultExecDept Clone()
        {
            DefaultExecDept u = base.Clone() as DefaultExecDept;
            u.compare = this.Compare.Clone();
            u.original = this.Original.Clone();
            u.target = this.Target.Clone();
            u.target2 = this.Target2.Clone();

            return u;
        }

    }
}
