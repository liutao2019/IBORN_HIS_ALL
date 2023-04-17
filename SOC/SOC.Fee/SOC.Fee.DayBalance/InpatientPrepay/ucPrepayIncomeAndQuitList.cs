using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Fee.DayBalance.InpatientPrepay
{
    public partial class ucPrepayIncomeAndQuitList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrepayIncomeAndQuitList()
        {
            InitializeComponent();
        }
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 退款金额是否统计预交金支付
        /// </summary>
        private string isCountPrepayPay = "1";//‘0’表示统计，‘1’表示不统计

        /// <summary>
        /// 退款金额是否统计预交金支付
        /// </summary>
        [Description("退款金额是否统计预交金支付，‘0’表示不统计，‘1’表示统计"), Category("设置")]
        public string IsCountPrepayPay
        {
            get
            {
                return this.isCountPrepayPay;
            }
            set
            {
                this.isCountPrepayPay = value;
            }
        }

        private void Init()
        {
            this.ucPrepayHistory1.Init();
            this.ucPrepayCompare1.Init();
            this.ucPrepayCompare1.IsCountPrepayPay = this.isCountPrepayPay;
            ucPrepayHistory1.evnSetValue += new ucPrepayHistory.SetValue(SetDetailValue);
        }

        public void SetDetailValue(SOC.Fee.DayBalance.Object.PrepayDayBalance prepay)
        {
            this.ucPrepayCompare1.SetValue(prepay);
        }

        //查询
        public void Query()
        {
            this.ucPrepayHistory1.Query();
        }

        //打印
        public void Print()
        {
            this.ucPrepayCompare1.Print();
        }

        //清屏
        public void Clear()
        {
            this.ucPrepayCompare1.Clear();
        }

        #region 工具栏设置
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("清屏", "清空屏幕", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清屏":
                    {
                        this.Clear();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 1;
        }
        #endregion


    }
}
