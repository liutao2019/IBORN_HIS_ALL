using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [功能描述: 药库基类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-04]<br></br>
    /// 说明：
    /// 1、不允许添加业务逻辑
    /// </summary>
    public partial class ucBaseControl : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBaseControl()
        {
            InitializeComponent();
            ucTreeViewChooseList = new ucTreeViewChooseList();
            ucTreeViewChooseList.Dock = DockStyle.Fill;
            ucTreeViewChooseList.TreeView.Nodes.Add("example1", "图片0", 0, 0);
            ucTreeViewChooseList.TreeView.Nodes.Add("example2", "图片1", 1, 1);
            ucTreeViewChooseList.TreeView.Nodes.Add("example3", "图片2", 2, 2);
            ucTreeViewChooseList.TreeView.Nodes.Add("example4", "图片3", 3, 3);
            ucTreeViewChooseList.TreeView.Nodes.Add("example5", "图片4", 4, 4);
            this.neuPanelLeftChoose.Controls.Add(ucTreeViewChooseList);

            ucDataDetail = new ucDataDetail();
            ucDataDetail.Dock = DockStyle.Fill;
            this.neuPanelData.Controls.Add(ucDataDetail);
        }

        protected ucTreeViewChooseList ucTreeViewChooseList = null;
        protected ucDataDetail ucDataDetail = null;
      
    }
}
