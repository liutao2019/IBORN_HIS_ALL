using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.MET.MetTec
{
    public partial class ucMetTecSuperCheckUp : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetTecSuperCheckUp()
        {
            InitializeComponent();
        }

        private string feecode=string.Empty;
        private string dept=string.Empty;
        private string title=string.Empty;

        [Description("ͳ�Ƶ�Ϊĳһ��С���õ����з���"),Category("����"),DefaultValue("001")]
        public string Feecode
        {
            get
            {
                return this.feecode;
            }
            set
            {
                this.feecode=value;
            }
        }

        [Description("����ͳ�����ͣ���MZ�� ͳ�Ƶ�Ϊ������ã���ZY��ΪסԺ����ALL��ΪסԺ������ĺϼ�"),Category("����"),DefaultValue("ALL")]
        public string Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept=value;
            }

        }
        [Description("���ñ�����ʾ����"),Category("����"),DefaultValue("�������CT")]
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title=value;
            }
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value,this.feecode,this.dept,this.title);

        }


        }
    
}
