using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.InpatientFee
{
    public partial class ucFinIpbStatFee3 : Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinIpbStatFee3()
        {
            InitializeComponent();
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }          

           //FS.FrameWork.Function.NConvert convert =new FS.FrameWork.Function.NConvert()       
            return base.OnRetrieve(base.beginTime, base.endTime);
        }
    }
}
