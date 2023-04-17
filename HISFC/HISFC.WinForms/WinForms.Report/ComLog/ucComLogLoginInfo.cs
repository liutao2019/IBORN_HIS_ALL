using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.ComLog
{
    public partial class ucComLogLoginInfo : Report.Common.ucQueryBaseForDataWindow
    {
        public ucComLogLoginInfo()
        {
            InitializeComponent();
        }
        private string personCode = string.Empty;
        private string personName = string.Empty;
        System.Collections.ArrayList constantList=null ;
        protected override void OnLoad()
        {
            this.Init();

            base.OnLoad();
            //����ʱ�䷶Χ
            DateTime now = DateTime.Now;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            this.dtpBeginTime.Value = dt;

            //�������
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            constantList = manager.QueryEmployeeAll();
            
            
            FS.HISFC.Models.Base.Employee allPerson = new FS.HISFC.Models.Base.Employee();
            allPerson.ID = "%%";
            allPerson.Name = "ȫ��";
            allPerson.SpellCode = "QB";
            //cboPersonCode.Items.Insert(0, allPerson);
            constantList.Insert(0,allPerson);
            this.cboPersonCode.AddItems(constantList);
            cboPersonCode.SelectedIndex = 0;

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            return base.OnRetrieve(personCode,this.dtpBeginTime.Value, this.dtpEndTime.Value);

        }

        private void cboPersonCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPersonCode.SelectedIndex >=0)
            {
                //personCode = ((FS.HISFC.Models.Base.Employee)cboPersonCode.Items[this.cboPersonCode.SelectedIndex]).ID.ToString();
                //personName = ((FS.HISFC.Models.Base.Employee)cboPersonCode.Items[this.cboPersonCode.SelectedIndex]).Name.ToString();
                personCode = ((FS.HISFC.Models.Base.Employee)constantList[this.cboPersonCode.SelectedIndex]).ID.ToString();
                personName = ((FS.HISFC.Models.Base.Employee)constantList[this.cboPersonCode.SelectedIndex]).Name.ToString();
            }
        }
    }
}

