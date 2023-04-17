using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    public partial class tvCaseHistory :FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvCaseHistory()
        {
            InitializeComponent();

            this.Init();
        }

        private void Init()
        {
            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            TreeNode[] trNodes = new TreeNode[2]; 

            obj1.ID = "Y";
            obj1.Name = "已经回收病历";
            obj2.ID = "N";
            obj2.Name = "未回收病历";
            trNodes[0].Tag = obj1;
            trNodes[0].Name = obj1.Name;
            trNodes[1].Tag = obj2;
            trNodes[1].Name = obj2.Name;


            this.Nodes.AddRange(trNodes);
        }
    }
}
