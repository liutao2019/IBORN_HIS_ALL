using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Logistics.DrugStore
{
    public partial class ucStoDrugStoreZxb : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucStoDrugStoreZxb()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        private ArrayList deptArry = new ArrayList();
        private ArrayList drugArry = new ArrayList();
        private List<FS.HISFC.Models.Pharmacy.Item> itemList = new List<FS.HISFC.Models.Pharmacy.Item>();
        private ArrayList deptYKArry = new ArrayList();

        private string queryStr = "(¿ÆÊÒ like '{0}%') and (Æ·Ãû like '{1}%')";

        protected override void OnLoad()
        {
            deptArry = new ArrayList();
            deptYKArry = new ArrayList();
            deptArry = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            deptYKArry = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.PI);
            if (deptYKArry != null)
            {
                foreach (FS.HISFC.Models.Base.Department deptObj in deptYKArry)
                {
                    deptArry.Add(deptYKArry);
                }
            }
            this.cmbDeptName.AddItems(deptArry);


            drugArry = new ArrayList();
            itemList = new List<FS.HISFC.Models.Pharmacy.Item>();
            itemList = itemManager.QueryItemList();
            if (itemList != null)
            {
                foreach (FS.HISFC.Models.Pharmacy.Item itemObj in itemList)
                {
                    drugArry.Add(itemObj);
                }

                this.cmbDrugName.AddItems(drugArry);
            }

            base.OnLoad();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(this.beginTime, this.endTime);
            //return 1;
        }

        private void cmbDeptName_TextChanged(object sender, EventArgs e)
        {

            string dept = this.cmbDeptName.Text.Trim().ToUpper().Replace(@"\", "");
            string drug = this.cmbDrugName.Text.Trim().ToUpper().Replace(@"\", "");

            if (dept.Equals("") && drug.Equals(""))
            {

                this.dwMain.SetFilter("");
                this.dwMain.Filter();

                return;
            }
            
            string str = string.Format(this.queryStr, dept, drug);
            this.dwMain.SetFilter(str);
            this.dwMain.Filter();
          
           
        }

    }
}
