using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.Report.Logistics.DrugStore
{
    public partial class ucPhaStatOutAlert : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaStatOutAlert()
        {
            InitializeComponent();

        }
     protected FS.HISFC.Models.Base.Employee employee = null;

     protected override int OnRetrieve(params object[] objects)
        {
            if (textBox1.Text == null && textBox1.Text == "")
            {
                MessageBox.Show("������Ƚ�������");
                this.textBox1.Focus();
                return -1;
            }
            int num = 0;

            if (!int.TryParse(textBox1.Text, out num))
            {
                MessageBox.Show("ֻ����������");
                this.textBox1.Focus();
                return -1;
            }

            if (Convert.ToInt32(textBox1.Text) > 999999)
            {
                MessageBox.Show("�Ƚ��������ܳ������ֵ999999�죬����������");
                this.textBox1.Focus();
                return -1;
            }
            this.employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;
            return base.OnRetrieve(System.DateTime.Now,Convert.ToInt32(textBox1.Text),employee.Dept.ID);
        }
       
    }
}

