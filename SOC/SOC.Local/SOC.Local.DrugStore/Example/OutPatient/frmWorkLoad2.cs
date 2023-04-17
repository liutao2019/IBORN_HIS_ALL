using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.Example.Outpatient
{
    /// <summary>
    /// [功能描述: 门诊药房工作量本地化实现]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-1]<br></br>
    /// 说明：这个从东莞移植的
    /// </summary>
    public partial class frmWorkLoad2 : Form
    {
        public frmWorkLoad2()
        {
            InitializeComponent();

            this.cmbDoseOper.KeyPress += new KeyPressEventHandler(cmbDoseOper_KeyPress);
            this.cmbSendOper.KeyPress += new KeyPressEventHandler(cmbSendOper_KeyPress);
            this.cmbConfirmOper.KeyPress += new KeyPressEventHandler(cmbConfirmOper_KeyPress);
        }

        void cmbConfirmOper_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nbtnOK.Select();
                this.nbtnOK.Focus();
            }
        }

        void cmbSendOper_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.cmbConfirmOper.Select();
                this.cmbConfirmOper.SelectAll();
                this.cmbConfirmOper.Focus();
            }
        }

        void cmbDoseOper_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.cmbSendOper.Select();
                this.cmbSendOper.SelectAll();
                this.cmbSendOper.Focus();
            }
        }

        private string drugedOperCode = "-1";
        private string sendedOperCode = "-1";
        private string confirmOperCode = "-1";

        /// <summary>
        /// 工作量是否需要核准人
        /// </summary>
        private bool isNeedConfirmOper = true;
        
        /// <summary>
        /// 配药人
        /// </summary>
        public string DrugedOperCode
        {
            get
            {
                return drugedOperCode;
            }
        }

        /// <summary>
        /// 发药人
        /// </summary>
        public string SendedOperCode
        {
            get
            {
                return sendedOperCode;
            }
        }

        /// <summary>
        /// 核准人
        /// </summary>
        public string ConfirmOperCode
        {
            get
            {
                return confirmOperCode;
            }
        }

        /// <summary>
        /// 初始化设置工作量
        /// </summary>
        /// <param name="deptNO">记录工作量的科室</param>
        public void Init(string deptNO)
        {
            FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
           
            string controlValue = "False";
            this.isNeedConfirmOper = FS.FrameWork.Function.NConvert.ToBoolean(controlValue);
            this.cmbConfirmOper.Enabled = this.isNeedConfirmOper;

            ArrayList al = personMgr.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.P);
            ArrayList alOwnDeptEmployee = new ArrayList();
            foreach (FS.HISFC.Models.Base.Employee e in al)
            {
                if (e.Dept.ID == deptNO)
                {
                    alOwnDeptEmployee.Add(e);
                }
            }
            this.cmbDoseOper.AddItems(alOwnDeptEmployee);
            this.cmbSendOper.AddItems(alOwnDeptEmployee);
            this.cmbConfirmOper.AddItems(alOwnDeptEmployee);
        }

        /// <summary>
        /// 工作量设置，用于已知人员分配情况下的工作量设置，比如上次已经设置，本次需要重新设置
        /// </summary>
        /// <param name="deptNO">记录工作量的科室</param>
        /// <param name="drugedOperCode">配药人</param>
        /// <param name="sendedOperCode">发药人</param>
        /// <param name="confirmOperCode">核对人</param>
        public void Init(string deptNO, string drugedOperCode, string sendedOperCode, string confirmOperCode)
        {
            FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();

            string controlValue = "False"; //integrateManager.QueryControlerInfo("PA" + deptNO);
            this.isNeedConfirmOper = FS.FrameWork.Function.NConvert.ToBoolean(controlValue);
            this.cmbConfirmOper.Enabled = this.isNeedConfirmOper;

            ArrayList al = personMgr.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.P);
            ArrayList alOwnDeptEmployee = new ArrayList();
            foreach (FS.HISFC.Models.Base.Employee e in al)
            {
                if (e.Dept.ID == deptNO)
                {
                    alOwnDeptEmployee.Add(e);
                }
            }
           
            this.cmbDoseOper.AddItems(alOwnDeptEmployee);
            this.cmbSendOper.AddItems(alOwnDeptEmployee);
            this.cmbConfirmOper.AddItems(alOwnDeptEmployee);

            this.SetOper(drugedOperCode, cmbDoseOper);
            this.SetOper(sendedOperCode, cmbSendOper);
            this.SetOper(confirmOperCode, cmbConfirmOper);
        }

        /// <summary>
        /// 设置操作人
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="neuComboBox"></param>
        private void SetOper(string operCode, FS.FrameWork.WinForms.Controls.NeuComboBox neuComboBox)
        {
            if (!string.IsNullOrEmpty(operCode))
            {
                neuComboBox.Tag = operCode;
                foreach (FS.HISFC.Models.Base.Employee e in neuComboBox.alItems)
                {
                    if (e.ID == operCode)
                    {
                        neuComboBox.Text = e.Name;
                        break;
                    }
                }
            }
        }


        private void nbtnOK_Click(object sender, EventArgs e)
        {
            if (this.cmbDoseOper.Tag == null || string.IsNullOrEmpty(this.cmbDoseOper.Tag.ToString()))
            {
                MessageBox.Show(this, "请输入配药人");
                this.cmbDoseOper.Select();
                this.cmbDoseOper.SelectAll();
                this.cmbDoseOper.Focus();
                return;
            }
            if (this.cmbSendOper.Tag == null || string.IsNullOrEmpty(this.cmbSendOper.Tag.ToString()))
            {
                MessageBox.Show(this, "请输入发药人");
                this.cmbSendOper.Select();
                this.cmbSendOper.SelectAll();
                this.cmbSendOper.Focus();
                return;
            }
            if (this.isNeedConfirmOper && (this.cmbConfirmOper.Tag == null || string.IsNullOrEmpty(this.cmbConfirmOper.Tag.ToString())))
            {
                MessageBox.Show(this, "请输入核对人");
                this.cmbConfirmOper.Select();
                this.cmbConfirmOper.SelectAll();
                this.cmbConfirmOper.Focus();
                return;
            }
            this.drugedOperCode = this.cmbDoseOper.Tag.ToString();
            this.sendedOperCode = this.cmbSendOper.Tag.ToString();
            try
            {
                this.confirmOperCode = this.cmbConfirmOper.Tag.ToString();
            }
            catch
            {
                this.confirmOperCode = "";
            }

            this.Hide();
        }

        private void nbCancel_Click(object sender, EventArgs e)
        {
            drugedOperCode = "-1";
            sendedOperCode = "-1";
            if (this.isNeedConfirmOper)
            {
                confirmOperCode = "-1";
            }
            else
            {
                confirmOperCode = "";
            }

            this.Hide();
        }
    }
}
