using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.GuangZhou.ZDLY.BedCard
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
            p.SetPageSize(new FS.HISFC.Models.Base.PageSize("bedCard", 800, 550));
            //p.PrintPage(0, 0, this.neuPanel1);
            //return 0;
            foreach (Control c in this.neuPanel1.Controls)
            {
                FS.SOC.Local.Order.GuangZhou.ZDLY.BedCard.ucBedCardFp ucBed = c as FS.SOC.Local.Order.GuangZhou.ZDLY.BedCard.ucBedCardFp;

                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {

                    p.PrintPreview(10, 10, c);
                }
                else
                {
                    p.PrintPage(10, 10, c);
                }
            }
            return 1;
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
            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(patients, typeof(ucBedCardFp), this.neuPanel1, new System.Drawing.Size(850, 1100));
            return 1;
        }

        #endregion
    }
}