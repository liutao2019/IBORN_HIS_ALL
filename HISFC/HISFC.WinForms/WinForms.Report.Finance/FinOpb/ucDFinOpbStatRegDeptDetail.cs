using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinOpb
{
    public partial class ucDFinOpbStatRegDeptDetail : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucDFinOpbStatRegDeptDetail()
        {
            InitializeComponent();
        }
        protected override void OnLoad()
        {
            this.Init();
            base.OnLoad();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            //InitializeComponent();
            //dwMain.Size = plRightTop.Size;
            //OnLoad();

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);

        }

    }
}
