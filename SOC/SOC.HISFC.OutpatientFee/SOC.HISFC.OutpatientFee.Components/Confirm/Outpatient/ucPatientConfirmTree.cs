using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.OutpatientFee.Components.Confirm.Outpatient
{
    public partial class ucPatientConfirmTree : UserControl, FS.SOC.HISFC.OutpatientFee.Interface.Confirm.IPatientTree
    {
        public ucPatientConfirmTree()
		{
			InitializeComponent();
        }

        #region 私有方法


        #endregion

        #region IPatientTree 成员

        public int Init()
        {

            this.neuTreeView1.ImageList = Function.PatientSmallImageList();

           return this.LoadOutpatient();

        }


        /// <summary>
        /// 初始化/刷新门诊患者列表树
        /// </summary>
        public int LoadOutpatient()
        {
           
            return 1;
        }

        public int Init(DateTime beginTime, DateTime endTime)
        {
            return 1;
        }

        #endregion
    }
}
