using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.GuangZhou.GYZL
{
    public partial class frmAlterFeeRate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public frmAlterFeeRate()
        {
            InitializeComponent();           
        }

        private string gzybPactCode = string.Empty;
        [Category("控件设置"), Description("广州医保代码设置，获取项目甲乙类用")]
        public string GZYBPactCode
        {
            get
            {
                return this.gzybPactCode;
            }
            set
            {
                this.gzybPactCode = value;
            }
        }

        #region 菜单栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("修改费用", "修改费用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L历史信息, true, false, null);
            toolBarService.AddToolButton("修改费用时间", "修改费用时间", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L临时号, true, false, null);
            return this.toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "修改费用":
                    this.ucAlterFeeRate1.Modify();
                    break;
                case "修改费用时间":
                    this.ucAlterFeeRate1.ModifyFeeDate();
                    break;
                default:
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ucAlterFeeRate1.GZYBPactCode = this.GZYBPactCode;
            this.ucAlterFeeRate1.Query();
            return base.OnQuery(sender, neuObject);
        }
        #endregion

        private void frmAlterFeeRate_Load(object sender, EventArgs e)
        {
            this.ucAlterFeeRate1.GZYBPactCode = this.GZYBPactCode;
        }
    }
}
