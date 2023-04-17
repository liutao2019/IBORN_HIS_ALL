using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYSY.Outpatient
{
    public partial class frmWorkLoad : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">工作量类型0是直接发药 1是配药 2是发药</param>
        /// <param name="deptNO">原处方调配的药房科室编码</param>
        /// <param name="operDeptNO">当前调配处方的科室</param>
        /// <param name="recipeNO">处方号</param>
        /// <param name="qty">处方品种数</param>
        /// <param name="patientName">患者姓名</param>
        public frmWorkLoad(string type, string deptNO, string operDeptNO, decimal qty, string recipeNO, string patientName)
        {
            InitializeComponent();
            this.nlbPatientName.Text = "患者姓名：" + patientName;
            this.nlbRecipeNO.Text = "处方号：" + recipeNO;
            this.ntbEmplNOBarCode.Text = "";

            this.curType = type;
            this.curDeptNO = deptNO;
            this.curOperDeptNO = operDeptNO;
            this.curQty = qty;
            this.curRecipeNO = recipeNO;

            this.StartPosition = FormStartPosition.CenterScreen;

            this.ntbEmplNOBarCode.KeyPress += new KeyPressEventHandler(ntbEmplNOBarCode_KeyPress);
        }

        private string curRecipeNO = "";
        private string curOperDeptNO = "";
        private string curDeptNO = "";
        private decimal curQty = 0;
        private string curType = "";
        public string emplID = "";//员工工号

        public void SetWorkLoad()
        {
            Common.WorkLoadManager workLoadManager = new FS.SOC.Local.DrugStore.GuangZhou.Common.WorkLoadManager();
            if (workLoadManager.SetOutpatientWorkLoad(this.curRecipeNO, this.curDeptNO, this.curOperDeptNO, curType, emplID, this.curQty) == 1)
            {
                this.Close();
            }
        }

        void ntbEmplNOBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.Models.Base.Employee empl =personMgr.GetPersonByID(this.ntbEmplNOBarCode.Text);
                if (empl == null || string.IsNullOrEmpty(empl.ID))
                {
                    MessageBox.Show("找不到条形码对应的员工信息");
                    return;
                }
                this.SetWorkLoad();
            }

        }


    }
}
