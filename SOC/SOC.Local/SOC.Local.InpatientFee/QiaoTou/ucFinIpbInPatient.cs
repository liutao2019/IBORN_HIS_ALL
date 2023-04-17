using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.QiaoTou
{
    public partial class ucFinIpbInPatient : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        /// <summary>
        /// 桥头住院患者一览表{26AE821F-F32D-4ce6-B18E-1080B5D9E803}
        /// </summary>
        public ucFinIpbInPatient()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucFinIpbInPatient_Load);
        }

        void ucFinIpbInPatient_Load(object sender, EventArgs e)
        {
            #region 科室
            ArrayList alDept = new ArrayList();
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            alDept = deptMgr.GetDeptmentAll();
            ArrayList inpatientDept = new ArrayList();
            FS.HISFC.Models.Base.Department tempDeptAll =new FS.HISFC.Models.Base.Department ();
            tempDeptAll.ID = "ALL";
            tempDeptAll.Name = "全部";
            inpatientDept.Add(tempDeptAll);
            foreach (FS.HISFC.Models.Base.Department dept in alDept)
            {
                if (dept.DeptType.ID.ToString() == "I")
                {
                    inpatientDept.Add(dept);
                }
            }
            this.cmbDept.AddItems(inpatientDept);
            this.cmbDept.SelectedIndex = 0;
            #endregion 科室

            //#region 住院状态
            ////R-住院登记  I-病房接诊 B-出院登记 O-出院结算 P-预约出院,N-无费退院
            //ArrayList alInStata = new ArrayList();
            ////全部
            //FS.HISFC.Models.Base.Const allinstate0 = new FS.HISFC.Models.Base.Const();
            //allinstate0.ID = "ALL";
            //allinstate0.Name = "全部";
            //allinstate0.SpellCode = "QB";
            //alInStata.Add(allinstate0);
            ////住院登记
            //FS.HISFC.Models.Base.Const allinstate1 = new FS.HISFC.Models.Base.Const();
            //allinstate1.ID = "R";
            //allinstate1.Name = "住院登记";
            //allinstate1.SpellCode = "ZYDJ";
            //alInStata.Add(allinstate1);
            ////病房接诊
            //FS.HISFC.Models.Base.Const allinstate2 = new FS.HISFC.Models.Base.Const();
            //allinstate2.ID = "I";
            //allinstate2.Name = "病房接诊";
            //allinstate2.SpellCode = "BFJZ";
            //alInStata.Add(allinstate2);
            ////出院登记
            //FS.HISFC.Models.Base.Const allinstate3 = new FS.HISFC.Models.Base.Const();
            //allinstate3.ID = "B";
            //allinstate3.Name = "出院登记";
            //allinstate3.SpellCode = "CYDJ";
            //alInStata.Add(allinstate3);
            ////出院结算
            //FS.HISFC.Models.Base.Const allinstate4 = new FS.HISFC.Models.Base.Const();
            //allinstate4.ID = "O";
            //allinstate4.Name = "出院结算";
            //allinstate4.SpellCode = "CYJS";
            //alInStata.Add(allinstate4);
            ////预约出院
            //FS.HISFC.Models.Base.Const allinstate5 = new FS.HISFC.Models.Base.Const();
            //allinstate5.ID = "P";
            //allinstate5.Name = "预约出院";
            //allinstate5.SpellCode = "YYCY";
            //alInStata.Add(allinstate5);
            ////无费退院
            //FS.HISFC.Models.Base.Const allinstate6 = new FS.HISFC.Models.Base.Const();
            //allinstate6.ID = "N";
            //allinstate6.Name = "无费退院";
            //allinstate6.SpellCode = "WFTY";
            //alInStata.Add(allinstate6);
            //this.cmbInState.AddItems(alInStata);
            //this.cmbInState.SelectedIndex = 0;
            //#endregion 住院状态

            #region 结算状态
            ArrayList alFeeState = new ArrayList();
            FS.HISFC.Models.Base.Const tempFeeState1 = new FS.HISFC.Models.Base.Const();
            tempFeeState1.ID = "ALL";
            tempFeeState1.Name = "全部";
            alFeeState.Add(tempFeeState1);
            FS.HISFC.Models.Base.Const tempFeeState2 = new FS.HISFC.Models.Base.Const ();
            tempFeeState2.ID = "WJS";
            tempFeeState2.Name = "未结算";
            alFeeState.Add(tempFeeState2);
            FS.HISFC.Models.Base.Const tempFeeState3 = new FS.HISFC.Models.Base.Const ();
            tempFeeState3.ID = "JS";
            tempFeeState3.Name = "已结算";
            alFeeState.Add(tempFeeState3);
            this.cmbFeeState.AddItems(alFeeState);
            this.cmbFeeState.SelectedIndex = 0;
            #endregion 结算状态
        }
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            string temp = "";
            //R-住院登记  I-病房接诊 B-出院登记 O-出院结算 P-预约出院,N-无费退院
            if (this.cmbFeeState.SelectedItem.Name.ToString() == "全部")
            {
                temp = "'R','I','B','P','N','O'";

            }
            else if (this.cmbFeeState.SelectedItem.Name.ToString() == "已结算")
            {
                temp = "'O'";
            }
            else
            {
                temp = "'R','I','B','P','N'";

            }
            return base.OnRetrieve(base.beginTime, base.endTime, this.cmbDept.SelectedItem.ID.ToString(), temp);//, temp);
        }
    }
}

