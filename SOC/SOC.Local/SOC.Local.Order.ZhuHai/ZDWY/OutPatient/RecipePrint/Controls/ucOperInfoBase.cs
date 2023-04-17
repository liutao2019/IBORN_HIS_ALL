using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.Controls
{
    public partial class ucOperInfoBase : UserControl, RecipePrint.Interface.IOperInfo
    {
        public ucOperInfoBase()
        {
            InitializeComponent();
        }

        public int SetOperInfo(FS.FrameWork.Models.NeuObject doct,decimal cost)
        {
            this.lblPhaDoc.Text = doct.ID;
            lblPhaMoney.Text = FS.FrameWork.Public.String.ToSimpleString(cost) + "元";

            return 1;
        }
    }
}
