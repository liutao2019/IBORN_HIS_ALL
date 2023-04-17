using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.ZhuHai.ZDWY.InPatient
{
    public partial class frmWorkLoad : Form
    {
        public frmWorkLoad(string deptNO, string operDeptNO, string billNO, FS.FrameWork.Models.NeuObject drugBillClass,decimal qty)
        {
            InitializeComponent();
            this.nlbBillClassName.Text = "摆药单：" + drugBillClass.Name;
            this.nlbDeptName.Text = "科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptNO);
            this.curDeptNO = deptNO;
            this.curBillNO = billNO;
            this.curQty = qty;
            this.curOperDeptNO = operDeptNO;
            this.StartPosition = FormStartPosition.CenterScreen;
            isSetWorkLoad = false;
            this.ntbSendEmplBarCode.KeyPress += new KeyPressEventHandler(ntbEmplNOBarCode_KeyPress);
            this.ntbCheckEmplBarCode.KeyPress += new KeyPressEventHandler(ntbCheckEmplBarCode_KeyPress);
        }

        void ntbCheckEmplBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.Models.Base.Employee empl = personMgr.GetPersonByID(this.ntbCheckEmplBarCode.Text);
                if (empl == null || string.IsNullOrEmpty(empl.ID))
                {
                    MessageBox.Show("找不到条形码对应的员工信息");
                    this.ntbCheckEmplBarCode.SelectAll();
                    return;
                }
                checkID = empl.ID;
                this.SetWorkLoad();
            }
        }

      
        private string curOperDeptNO = "";
        private string curDeptNO = "";
        private decimal curQty = 0;
        private string curBillNO = "";
        public string emplID = "";//员工工号
        public string checkID = "";//核对工号
        private bool isSetWorkLoad = false;

        public void SetWorkLoad()
        {
            Common.WorkLoadManager workLoadManager = new FS.SOC.Local.DrugStore.ZhuHai.Common.WorkLoadManager();
            if (workLoadManager.SetInpatientWorkLoad(this.curBillNO, this.curDeptNO, this.curOperDeptNO, "1", emplID, this.curQty) == 1)
            {
                if (workLoadManager.SetInpatientWorkLoad(this.curBillNO, this.curDeptNO, this.curOperDeptNO, "2", checkID, this.curQty) == 1)
                isSetWorkLoad = true;
                this.Close();
            }
        }

        void ntbEmplNOBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.Models.Base.Employee empl =personMgr.GetPersonByID(this.ntbSendEmplBarCode.Text);
                if (empl == null || string.IsNullOrEmpty(empl.ID))
                {
                    MessageBox.Show("找不到条形码对应的员工信息");
                    return;
                }
                emplID = empl.ID;
                this.ntbCheckEmplBarCode.SelectAll();
                this.ntbCheckEmplBarCode.Focus();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = !isSetWorkLoad;
            base.OnFormClosing(e);
        }
    }
}
