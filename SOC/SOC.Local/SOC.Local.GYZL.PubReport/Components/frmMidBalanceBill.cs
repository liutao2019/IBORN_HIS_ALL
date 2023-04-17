using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.GYZL.PubReport.Components
{
    public partial class frmMidBalanceBill : FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        public frmMidBalanceBill()
        {
            this.ProgressRun(true);
            InitializeComponent();
        }

        FS.HISFC.Models.RADT.PatientInfo p = null;
        string errText;

        bool IsBalance = false;

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
            string dtBegin = "", dtEnd = this.dTEnd.Value.ToString();
            if (this.IsBalance)
            {
                this.ucBalanceBill1.IsBalance = true;
                this.ucBalanceBill1.DisplayPatient(this.ucQueryInpatientNo1.InpatientNo, ref dtBegin, dtEnd);
                this.dTBegin.Value = FS.FrameWork.Function.NConvert.ToDateTime(dtBegin);
                //this.ucBalanceBill1.SetPatientFee(p);
            }
            else
            {

                if (p == null || p.ID == null)
                {
                    MessageBox.Show("获得患者基本信息失败!");
                    return;
                }
                if (p.PVisit.InState.ID.ToString() == "O")
                {
                    MessageBox.Show("该患者已作出院清账！");
                    return;
                }
                this.ucBalanceBill1.DisplayPatient(this.ucQueryInpatientNo1.InpatientNo, ref dtBegin, dtEnd);
                this.dTBegin.Value = FS.FrameWork.Function.NConvert.ToDateTime(dtBegin);

                //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //myInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //if (p.PVisit.InState.ID.ToString() == "B")
                //{
                //    int iReturn = myInpatient.UpdateCloseAccount(this.ucQueryInpatientNo1.InpatientNo, "C");
                //    if (iReturn <= 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("更新患者在院状态出错!" + myInpatient.Err);
                //        return;
                //    }
                //}
                //FS.FrameWork.Management.PublicTrans.Commit();
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

        private void frmMidBalanceBill_Load(object sender, System.EventArgs e)
        {
            this.ucQueryInpatientNo1.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dTBegin.Enabled = false;
            this.dTEnd.Value = this.dTEnd.Value.AddDays(-1);
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
            FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            DateTime now = myInpatient.GetDateTimeFromSysDateTime();
            if (this.dTEnd.Value.Date == now.Date)
            {
                MessageBox.Show("未防止费用计算错误，中途结算不能结算当日费用");
                return;
            }
            if (this.dTEnd.Value.Date < this.dTBegin.Value.Date)
            {
                MessageBox.Show("结算开始日期大于结算结束日期！");
                return;

            }
            if (p != null)
            {
                this.ucBalanceBill1.QueryFee(p, this.dTBegin.Value.ToString(),
                    new DateTime(this.dTEnd.Value.Year, this.dTEnd.Value.Month, this.dTEnd.Value.Day, 23, 59, 59).ToString());
            }
        }

        private int Deal(string state)
        {
            FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //int iReturn = myInpatient.UpdateCloseAccount(this.ucQueryInpatientNo1.InpatientNo, state);
            int iReturn = myInpatient.CloseAccount(this.ucQueryInpatientNo1.InpatientNo);
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
            if (p.IsStopAcount == true)
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
     
    }
}
