using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Order
{
    public partial class ucFinOpbExecdeptDrugDetail :FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinOpbExecdeptDrugDetail()
        {
            InitializeComponent();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ����������Ժ�......");

                //Application.DoEvents();

                //this.dwMain.Modify("time.text='ͳ��ʱ�䣺" + this.beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "��" + this.endTime.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                return this.dwMain.Retrieve(this.beginTime, this.endTime);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                this.dwMain.Print();
            }
            catch (Exception ex)
            {
                return 1;
            }

            return 1;
        }

        private string queryStr = "((dept_name like '{0}%') or (dept_spell_code like '{0}%') or (dept_spell_code_1 like '{0}%')) and ((item_name like '{1}%') or (item_spell_code like '{1}%') or (item_spell_code_1 like '{1}%')) and ((fee_name like '{2}%') or (fee_spell_code like '{2}%') or (fee_spell_code_1 like '{2}%'))";

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            string dept = this.neuTextBox1.Text.Trim().ToUpper().Replace(@"\", "");
            string drug = this.neuTextBox2.Text.Trim().ToUpper().Replace(@"\", "");
            string fee = this.neuTextBox3.Text.Trim().ToUpper().Replace(@"\", "");

            if (dept.Equals("") && drug.Equals("") && fee.Equals(""))
            {
                this.dwMain.SetFilter("");
                this.dwMain.Filter();
                return;
            }

            string str = string.Format(this.queryStr, dept, drug, fee);
            this.dwMain.SetFilter(str);
            this.dwMain.Filter();
        }

    }
}

