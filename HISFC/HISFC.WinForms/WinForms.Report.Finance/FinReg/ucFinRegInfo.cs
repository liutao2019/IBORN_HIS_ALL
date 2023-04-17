using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using Common = Report.Common;
//using Manager = Report.Manager;
using System.Collections;

namespace FS.Report.Finance.FinReg
{
    public partial class ucFinRegInfo : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinRegInfo()
        {
            InitializeComponent();
        }
        //科室
     
        string deptCode = string.Empty;
        string deptName = string.Empty;
        
        #region 管理类

        FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();


     
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();


        System.Collections.ArrayList DeptList = new System.Collections.ArrayList();

        #endregion
        protected override void OnLoad()
        {
            base.OnLoad();
            //填充数据
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList constantList = manager.GetDeptmentAllValid();

            FS.HISFC.Models.Base.Department top = new FS.HISFC.Models.Base.Department();
            top.ID = "all";
            top.Name = "全  部";
            top.IsRegDept = false;
            top.IsStatDept = false;
            top.SpellCode = "QB";
            top.WBCode = "WU";


            this.neuComboBox1.Items.Add(top);
            foreach (FS.HISFC.Models.Base.Department con in constantList)
            {
                neuComboBox1.Items.Add(con);
            }
            this.neuComboBox1.alItems.Add(top);
            this.neuComboBox1.alItems.AddRange(constantList);

            if (neuComboBox1.Items.Count > 0)
            {
                neuComboBox1.SelectedIndex = 0;
                deptCode = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[0]).ID;
                deptName = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[0]).Name;
            }
        }

        protected override int OnRetrieve(params object[] objects)
        {
            #region 科室
          

            #endregion
            
         
            

            dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptCode);
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptCode);
        }

       

        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           


            if (neuComboBox1.SelectedIndex > -1)
            {
                deptCode = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).ID;
                deptName = ((FS.HISFC.Models.Base.Department)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).Name;
            }

            

        }

    }
}