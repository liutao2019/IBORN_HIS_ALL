using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    public partial class frmBalanceBill : FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        public frmBalanceBill()
        {
            this.ProgressRun(true);
            InitializeComponent();
        }

        FS.HISFC.Models.RADT.PatientInfo p = null;
        string errText;

        bool IsBalance = false;
        //
        //		private bool IsNum(String str) 
        //		{ 
        //			for(int i=0;i<str.Length;i++) 
        //			{ 
        //				if(!Char.IsNumber(str,i) )
        //					return false; 
        //			} 
        //			return true; 
        //		}

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                MessageBox.Show("住院号错误，没有找到该患者");
                this.ucBalanceBill1.Clear();
                this.ucQueryInpatientNo1.Focus();
                return;
            }
            //Add by 王宇，当患者开始打结算清单时修改在院状态为C,其他地方不允许录入费用
            //2005-12-26
            FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.HISFC.BizLogic.RADT.InPatient myRADT = new FS.HISFC.BizLogic.RADT.InPatient();
            p = myRADT.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);

            if (p.Pact.PayKind.ID == "03")
            {

                #region 检测
                //if (this.txtGJ.Text.Trim() == "")
                //{
                //    MessageBox.Show("请正确输入高检费或者肿瘤审批费,如果没有，请输0");
                //    this.txtGJ.Focus();
                //    return;
                //}
                //if (this.txtZLSP.Text.Trim() == "")
                //{
                //    MessageBox.Show("请正确输入高检费或者肿瘤审批费,如果没有，请输0");
                //    this.txtZLSP.Focus();
                //    return;
                //}
                if (this.txtGJ.Text.IndexOf(".") > -1)
                {

                    string[] tee = this.txtGJ.Text.Split('.');
                    if (tee[1].Length > 2)
                    {
                        MessageBox.Show("请保留两位小数", "提示");
                        this.txtGJ.Focus();
                        return;
                    }
                }
                if (this.txtZLSP.Text.IndexOf(".") > -1)
                {
                    string[] tee = this.txtZLSP.Text.Split('.');
                    if (tee[1].Length > 2)
                    {
                        MessageBox.Show("请保留两位小数", "提示");
                        this.txtZLSP.Focus();
                        return;
                    }
                }
                #endregion
                p.Sex.User01 = this.txtGJ.Text;
                p.Sex.User02 = this.txtZLSP.Text;
            }
            if (this.IsBalance)
            {
                ///补打功能赋不到病人流水号的值
                ///据说之前用过这个功能？奇怪！先这样打个补丁吧 呵呵ch2010-12-22
                if (this.Tag.ToString() == "O")
                {
                    this.ucBalanceBill1.InPatientNo = this.ucQueryInpatientNo1.InpatientNo;
                    this.ucBalanceBill1.SetPatientInfo(p);
                }
                this.ucBalanceBill1.IsBalance = true;
                this.ucBalanceBill1.SetPatientFee(p);
            }
            else
            {

                if (p == null || p.ID == null)
                {
                    MessageBox.Show("获得患者基本信息失败!");
                    return;
                }
                if (p.PVisit.InState.ID.ToString() == "I")
                {
                    MessageBox.Show("该患者不是出院登记状态,通知病区作住院登记!");
                    return;
                }
                if (p.PVisit.InState.ID.ToString() == "O")
                {
                    MessageBox.Show("该患者已作出院清账！");
                    return;
                }
                this.ucBalanceBill1.pInfo.Sex.User01 = p.Sex.User01;
                this.ucBalanceBill1.pInfo.Sex.User02 = p.Sex.User02;
                if (this.ucBalanceBill1.SetPatientNo(this.ucQueryInpatientNo1.InpatientNo) < 0)
                {
                    return;
                }

                //FS.neuFC.Management.Transaction t = new FS.neuFC.Management.Transaction(myInpatient.Connection);
                //t.BeginTransaction();
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                myInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (p.PVisit.InState.ID.ToString() == "B")
                {
                    //int iReturn = myInpatient.UpdateCloseAccount(this.ucQueryInpatientNo1.InpatientNo, "C");
                    int iReturn = myInpatient.CloseAccount(this.ucQueryInpatientNo1.InpatientNo);
                    if (iReturn <= 0)
                    {
                       FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新患者在院状态出错!" + myInpatient.Err);
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }



            //Add End

            this.btnPrint.Focus();
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            //封账
            if (e.Button == this.tbPrint)
            {
                this.ucBalanceBill1.Print();
                return;
            }

            //开账
            if (e.Button == this.tbbOpenAccount)
            {
                this.btnOpenAccount_Click(this.btnPrint, null);
                return;
            }


            //清屏
            if (e.Button == this.tbFresh)
            {
                this.ucQueryInpatientNo1.Text = "";
                //this.ucQueryInpatientNo1.c = "";
                this.ucBalanceBill1.Clear();
                return;
            }

            //退出
            if (e.Button == this.tbQuit)
            {
                this.Close();
                return;
            }
            if (e.Button == this.tbCDFee)
            {
                //Fee.frmConfirmOrder frmCDFee = new frmConfirmOrder(this.var);
                //frmCDFee.ISOutPatient = true;
                //frmCDFee.ShowDialog();

                return;
            }
        }

        private void frmBalanceBill_Load(object sender, System.EventArgs e)
        {
            this.ucQueryInpatientNo1.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            if (this.Tag.ToString() == "O")
            {
                this.IsBalance = true;
            }
            else
            {
                this.IsBalance = false;
            }
        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            if (this.IsBalance)
            {
                this.ucBalanceBill1.Reprint();
            }
            else
            {
                this.ucBalanceBill1.Print();
            }
        }

        private int Deal(string state)
        {
            FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            //FS.neuFC.Management.Transaction t = new FS.neuFC.Management.Transaction(myInpatient.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //int iReturn = myInpatient.UpdateCloseAccount(this.ucQueryInpatientNo1.InpatientNo, state);
            int iReturn = myInpatient.OpenAccount(this.ucQueryInpatientNo1.InpatientNo);
            if (iReturn <= 0)
            {
                errText = myInpatient.Err;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return iReturn;
            }
            else
            {
                errText = "";
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        private void btnOpenAccount_Click(object sender, System.EventArgs e)
        {
            FS.HISFC.BizLogic.RADT.InPatient myRADT = new FS.HISFC.BizLogic.RADT.InPatient();
            p = myRADT.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);

            if (p == null || p.ID == null)
            {
                MessageBox.Show("获得患者基本信息失败!");
                return;
            }
            //if (p.PVisit.InState.ID.ToString() != "C")
            //{
            //    MessageBox.Show("该患者不是封账状态,不能进行开帐处理!");
            //    return;
            //}

            if (p.IsStopAcount == false)
            {
                MessageBox.Show("该患者不是封账状态,不能进行开帐处理!");
                return;
            }


            if (Deal("B") != 1)
            {
                MessageBox.Show("开账失败!" + errText);
            }
            else
            {
                MessageBox.Show("开账成功!" + errText);
            }
        }
        /// <summary>
        /// 高检费只允许数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGJ_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                FS.FrameWork.Function.NConvert.ToDecimal(this.txtGJ.Text.Trim());
            }
            catch
            {
                this.txtGJ.Text = "";
                return;
            }
        }
        /// <summary>
        /// 肿瘤只允许数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtZLSP_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                FS.FrameWork.Function.NConvert.ToDecimal(this.txtZLSP.Text.Trim());
            }
            catch
            {
                this.txtZLSP.Text = "";
                return;
            }
        }
    }
}
