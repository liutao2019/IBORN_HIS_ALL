using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Local.GYZL.PubReport.Models
{
    public class PactInfo : FS.FrameWork.Models.NeuObject
    {
        public PactInfo()
        {
        }
        public FS.FrameWork.Models.NeuObject Pact = new FS.FrameWork.Models.NeuObject();
        public string PactHead = "";
        public FS.FrameWork.Models.NeuObject ParentPact = new FS.FrameWork.Models.NeuObject();
        public string PactFlag = "";
        public string PayKind = "";
        public decimal PactRate = 0;
        public new PactInfo Clone()
        {
            PactInfo obj = base.MemberwiseClone() as PactInfo;
            obj.Pact = this.Pact.Clone();
            obj.ParentPact = this.ParentPact.Clone();
            return obj;
        }
    }
}
