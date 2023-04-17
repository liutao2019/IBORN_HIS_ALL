using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.GuangZhou.ZDLY.BedCardYk
{
    /// <summary>
    /// 床头卡显示
    /// </summary>
    public partial class ucBedCardShow : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBedCardShow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 床头卡打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.RADT.IBedCard IBedCard = null;

        /// <summary>
        /// 选中的患者列表
        /// </summary>
        private ArrayList myPatients = null;

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            if (tv != null && tv.CheckBoxes == false)
            {
                tv.CheckBoxes = true;
            }
            if (IBedCard == null)
            {
                IBedCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBedCard)) as FS.HISFC.BizProcess.Interface.RADT.IBedCard;
            }


            return base.OnInit(sender, neuObject, param);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            //myQuery();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
            {
                tv.CheckBoxes = true;
            }
            return 0;
        }

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            if (alValues == null)
            {
                return -1;
            }

            this.myPatients = alValues;
            this.myQuery();
            return 0;
        }


        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            if (IBedCard != null)
            {
                return IBedCard.Print();
            }
            return 1;
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            if (IBedCard != null)
            {
                return IBedCard.PrintPreview();
            }
            return 1;
        }

        private int myQuery()
        {
            pnBedShow.Controls.Clear();

            //当前Tab页里面还没有输液卡
            object o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IBedCard));
            if (o == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("请维护" + this.GetType().ToString() + "里面接口FS.HISFC.BizProcess.Interface.RADT.IBedCard的实例对照！");
                return -1;
            }
            this.IBedCard = o as FS.HISFC.BizProcess.Interface.RADT.IBedCard;
            if (IBedCard == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("维护的实例不具备FS.HISFC.BizProcess.Integrate.RADT.IBedCard接口");
                return -1;
            }
            ((Control)o).Visible = true;
            ((Control)o).Dock = DockStyle.Fill;
            this.pnBedShow.Controls.Add((Control)o);

            return IBedCard.ShowBedCard(myPatients);

            return 1;
        }
    }
}
