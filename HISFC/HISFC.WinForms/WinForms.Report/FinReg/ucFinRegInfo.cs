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

namespace DongDian.Report.FinReg
{
    public partial class ucFinRegInfo : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinRegInfo()
        {
            InitializeComponent();
        }
        //����
        private string DeptCode = string.Empty;
        private string DeptName = string.Empty;
        
        #region ������

        FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();


        //Manager.PhaPriv phaPriv = new Manager.PhaPriv();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();


        System.Collections.ArrayList DeptList = new System.Collections.ArrayList();

        #endregion

        protected override void OnLoad()
        {
            this.Init();

            string strAll = "all";
            string strName = "ȫ��";

            FS.HISFC.Models.Base.Department deptO = new FS.HISFC.Models.Base.Department();
            deptO.ID = strAll;
            deptO.Name = strName;

            #region ����
            DeptList = manager.GetDeptmentAllValid();
       //     DeptList = phaPriv.GetAllPrivDept(operDeptCode);
            DeptList.Add(deptO);
            foreach (FS.FrameWork.Models.NeuObject con in DeptList)
            {

                this.neuComboBox1.Items.Add(con);

            }


            if (neuComboBox1.Items.Count >= 0)
            {
                neuComboBox1.SelectedIndex = 0;
                DeptCode = ((FS.FrameWork.Models.NeuObject)neuComboBox1.Items[0]).ID;
                DeptName = ((FS.FrameWork.Models.NeuObject)neuComboBox1.Items[0]).Name;
            }

            #endregion

            SetCmb();

            base.OnLoad();
        }
        protected override int OnRetrieve(params object[] objects)
        {
            #region ����
            string[] deptStr;
            DeptCode = neuComboBox1.SelectedItem.ID;
            DeptName = neuComboBox1.SelectedItem.Name;

            //if (DeptCode == "all")
            //{

            //    //deptStr = new string[DeptList.Count];
            //    //for (int i = 0; i < DeptList.Count; i++)
            //    //{
            //    //    FS.FrameWork.Models.NeuObject s = DeptList[i] as FS.FrameWork.Models.NeuObject;
            //    //    deptStr[i] = s.ID;
            //    //}
            //    deptStr = new string[]
            //    {
            //        DeptCode
            //    };
            //}
            //else
            //{
            deptStr = new string[]
                {
                    DeptCode
                };
            //}

            #endregion

            dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value,deptStr);
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value,deptStr);
        }

        private void SetCmb()
        {
            FS.HISFC.BizProcess.Integrate.Manager m = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList al = m.GetDepartment();
            string strAll = "all";
            string strName = "ȫ��";


            if (al == null) return;
            FS.HISFC.Models.Base.Department deptO = new FS.HISFC.Models.Base.Department();
            deptO.ID = strAll;
            deptO.Name = strName;
            al.Add(deptO);
            this.neuComboBox1.AddItems(al);
        }

        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeptCode = this.neuComboBox1.Tag.ToString();
        }

    }
}