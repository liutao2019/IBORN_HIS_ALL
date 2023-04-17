using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls.BedCard
{
    /// <summary>
    /// 床头卡显示控件
    /// </summary>
    public partial class ucBedCardPanel : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.RADT.IBedCard
    {
        public ucBedCardPanel()
        {
            InitializeComponent();
        }

        #region IBedCard 成员

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPage(0, 0, this.neuPanel1);
            return 0;
        }

        public int PrintPreview()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPreview(this.neuPanel1);
            return 0;
        }

        public int ShowBedCard(ArrayList patients)
        {
            this.neuPanel1.Controls.Clear();
            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(patients, typeof(ucBedCardFp), this.neuPanel1, new System.Drawing.Size(790, 1150));
            return 1;
        }

        #endregion
    }
}