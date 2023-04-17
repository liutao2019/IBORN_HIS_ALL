﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.HISFC.Components.InpatientFee.Controls
{
    public partial class tvPatientListForPrepay : FS.HISFC.Components.Common.Controls.tvPatientList
    {
        public tvPatientListForPrepay()
        {
            InitializeComponent();

            #region {7655A89B-5996-4651-BAB4-62B53AACA6CF}
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            {
                return;
            }
            #endregion
            this.Refresh();

        }

        public string ShowType = "In";

        public string ShowDept = "1";

        public tvPatientListForPrepay(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public override void Refresh()
        {
            //{707F2343-20AC-445b-9ACB-2B707C8EA249}
            InitControlParam();
            this.BeginUpdate();
            this.Nodes.Clear();
            if (manager == null)
                manager = new FS.HISFC.BizProcess.Integrate.RADT();


            ArrayList al = new ArrayList();//患者列表

            addPatientList(al);
            //显示所有患者列表
            this.SetPatient(al);

            this.EndUpdate();
            this.CollapseAll();
            //this.Scrollable = false;     
        }

        FS.HISFC.BizProcess.Integrate.RADT manager = null;
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        //出院召回的有效天数
        private int callBackVaildDays;
        public const string control_id = "ZY0001";

        /// <summary>
        /// 初始化控制参数,获得出院召回的有效天数
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            this.callBackVaildDays = ctrlParamIntegrate.GetControlParam<int>(control_id, true, 1);
        }


        /// <summary>
        /// 根据病区站得到患者
        /// </summary>
        /// <param name="al"></param>
        private void addPatientList(ArrayList al)
        {
            ArrayList al1 = new ArrayList();
            ArrayList deplist = new ArrayList();
            FS.HISFC.BizLogic.Manager.Department deptmanager = new FS.HISFC.BizLogic.Manager.Department();
            deplist = deptmanager.GetNurseAll();


            foreach (FS.HISFC.Models.Base.Department tempdept in deplist)
            {
                if (Function.IsContainYKDept(tempdept.ID).Equals(Function.IsContainYKDept()))
                {
                    al1.Clear();
                    al1.AddRange(this.radtManager.PatientQueryByNurseCell(tempdept.ID, FS.HISFC.Models.Base.EnumInState.B));

                    al1.AddRange(this.radtManager.PatientQueryByNurseCell(tempdept.ID, FS.HISFC.Models.Base.EnumInState.R));

                    al1.AddRange(this.radtManager.PatientQueryByNurseCell(tempdept.ID, FS.HISFC.Models.Base.EnumInState.I));

                    if (al1 != null && al1.Count > 0)
                    {
                        if (ShowDept == "1")//显示科室 
                        {
                            al.Add(tempdept.Name);

                        }
                        al.AddRange(al1);
                    }
                }
            }
        }

       
    }
}
