using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Logistics.Pharmacy
{
    public partial class ucPhaDrugstoresStat : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        #region ����
        /// <summary>
        /// ��õ�ǰ����Ա��ʵ��
        /// </summary>
        FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        #endregion

        public ucPhaDrugstoresStat()
        {
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;
            this.Init();
            base.OnLoad();
        }

        #region ����

        protected override int OnRetrieve(params object[] objects)
        {
            if (this.dtpBeginTime.Value > this.dtpEndTime.Value)
            {
                MessageBox.Show("��ѯ��ʼʱ�䲻�ܴ��ڲ�ѯ����ʱ�䣡");
                return -1;
            }
            return base.OnRetrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,empl.Dept.Name,empl.Dept.ID);
        }
        #endregion 
    }
}
