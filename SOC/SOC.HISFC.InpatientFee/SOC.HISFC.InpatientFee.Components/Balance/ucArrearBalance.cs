using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    /// <summary>
    /// [功能描述: 欠费清账表现类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    public partial class ucArrearBalance :FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucArrearBalance()
        {
            InitializeComponent();
            this.ucQueryInpatientNo.myEvent+=new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo_myEvent);
        }

        #region 变量

        ToolBarService toolBarService = new ToolBarService();

        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        #endregion

        #region 方法

        /// <summary>
        /// 清除信息
        /// </summary>
        private void Clear()
        {
            this.patientInfo = null;
            this.spDetail_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 显示值
        /// </summary>
        /// <param name="sv">当前Sheet页</param>
        /// <param name="balances">结算信息</param>
        private void SetValue( FarPoint.Win.Spread.SheetView sv, ArrayList balances)
        {
            if (balances == null || balances.Count == 0)
            {
                return;
            }

            FS.SOC.HISFC.InpatientFee.BizProcess.RADT radtMananger = new FS.SOC.HISFC.InpatientFee.BizProcess.RADT();
            sv.RowCount = balances.Count;
            FS.HISFC.Models.RADT.PatientInfo patient = radtMananger.GetPatientInfo((balances[0] as FS.HISFC.Models.Fee.Inpatient.Balance).Patient.ID);
            if (patient == null)
            {
                SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("获得患者信息出错!" + radtMananger.Err, MessageBoxIcon.Error);
                return;
            }

            this.patientInfo = patient;

            for (int i = 0; i < balances.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.Balance b = balances[i] as FS.HISFC.Models.Fee.Inpatient.Balance;
                sv.Cells[i, 0].Value = true;
                sv.Cells[i, 1].Text = patient.PID.PatientNO;
                sv.Cells[i, 2].Text = patient.Name;
                sv.Cells[i, 3].Text = patient.PVisit.PatientLocation.Dept.Name;
                string balanceTypeName = string.Empty;
                if (b.BalanceType.ID.ToString() == "Q")
                {
                    balanceTypeName = "欠费";
                }
                sv.Cells[i, 4].Text = balanceTypeName;
                sv.Cells[i, 5].Text = b.FT.TotCost.ToString("F2");
                sv.Cells[i, 6].Text = (-b.FT.ArrearCost).ToString("F2");
                sv.Cells[i, 7].Text = b.BalanceOper.OperTime.ToString();
                sv.Cells[i, 8].Text = b.IsTempInvoice ? "未打印" : "已打印";
                sv.Rows[i].Tag = b.Clone();
            }
        }

        /// <summary>
        /// 保存结果
        /// </summary>
        /// <param name="isCancel">是否是取消操作</param>
        /// <returns>成功 1 失败 -1</returns>
        private int Save(bool isCancel)
        {
            if(patientInfo==null)
            {
                return -1;
            }

            if (this.spDetail_Sheet1.RowCount == 0)
            {
                return -1;
            }

            this.spDetail.StopCellEditing();

            List<FS.HISFC.Models.Fee.Inpatient.Balance> list = new List<FS.HISFC.Models.Fee.Inpatient.Balance>();
            List<FS.HISFC.Models.Fee.Inpatient.Balance> listPrint = new List<FS.HISFC.Models.Fee.Inpatient.Balance>();

            for (int i = 0; i < this.spDetail_Sheet1.RowCount; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.spDetail_Sheet1.Cells[i, 0].Value))
                {
                    FS.HISFC.Models.Fee.Inpatient.Balance balance = this.spDetail_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Inpatient.Balance;
                    if (FS.FrameWork.Function.NConvert.ToBoolean(balance.BalanceSaveType).Equals(isCancel))
                    {
                        list.Add(balance);
                        if (balance.IsTempInvoice)
                        {
                            listPrint.Add(balance);
                        }
                    }
                }
            }

            if (list.Count > 0)
            {
                if (DialogResult.Yes == CommonController.CreateInstance().MessageBox(this, "确认处理欠费结算记录？", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    List<FS.HISFC.Models.Fee.Inpatient.BalancePay> listBalancePay = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();
                    //处理支付方式问题
                    if (!isCancel)
                    {
                        decimal shouldPayCost = 0M;
                        foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in list)
                        {
                            shouldPayCost += balance.FT.ArrearCost;
                        }
                        if (shouldPayCost > 0)
                        {
                            frmBalancePay frmBalancePay = new frmBalancePay();
                            frmBalancePay.ArrearBalance = 0M;
                            frmBalancePay.ShouldPay = shouldPayCost;
                            frmBalancePay.ShowDialog(this);
                            if (!frmBalancePay.IsOK)
                            {
                                return -1;
                            }

                            if (frmBalancePay.ListBalancePay.Count > 0)
                            {
                                listBalancePay.AddRange(frmBalancePay.ListBalancePay);
                            }
                        }
                    }

                    FS.SOC.HISFC.InpatientFee.BizProcess.Balance balanceManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Balance();
                    if (balanceManager.ArrearBalance(this.patientInfo, list, listBalancePay, isCancel ? FS.HISFC.Models.Base.TransTypes.Negative : FS.HISFC.Models.Base.TransTypes.Positive) <= 0)
                    {
                        CommonController.CreateInstance().MessageBox(balanceManager.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    if (listPrint.Count > 0)
                    {
                        //如果存在临时发票的情况，则补打发票
                        string errText = "";
                        if (Function.PrintInvoice(this.patientInfo, list, ref errText)<0)
                        {
                            CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                            return -1;
                        }
                    }
                    this.Clear();
                    this.InitTree();
                }
            }


            return 1;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
            this.dtpBegin.Value = inpatientFeeManager.GetDateTimeFromSysDateTime().Date;
            this.dtpEnd.Value = this.dtpBegin.Value.AddDays(1);
            this.InitTree();
            this.Focus();
            this.ucQueryInpatientNo.Focus();
        }

        /// <summary>
        /// 初始化树列表
        /// </summary>
        private void InitTree()
        {
            if (this.tv is tvPatientListForArrear)
            {
                ((tvPatientListForArrear)this.tv).Init(this.dtpBegin.Value, this.dtpEnd.Value);
            }
        }

        #endregion

        #region 重载覆写

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("平账", "平账结算患者费用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("取消", "取消平账操作", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "平账")
            {
                this.Save(false);
            }
            if (e.ClickedItem.Text == "取消")
            {
                this.Save(true);
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (e != null)
            {
                this.SetValue(this.spDetail_Sheet1, e.Tag as ArrayList);
            }
            return base.OnSetValue(neuObject, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.InitTree();
            return base.OnQuery(sender, neuObject);
        }

        void ucQueryInpatientNo_myEvent()
        {
            //根据流水号查找对应欠费信息
            FS.HISFC.BizLogic.Fee.InPatient inpatientFeeMgr=new FS.HISFC.BizLogic.Fee.InPatient();
            ArrayList alBalances = inpatientFeeMgr.QueryBalancesByInpatientNO(this.ucQueryInpatientNo.InpatientNo);
            if (alBalances == null)
            {
                CommonController.CreateInstance().MessageBox("获得患者结算信息失败，" + inpatientFeeMgr.Err, MessageBoxIcon.Error);
                return;
            }

            ArrayList alQBalances = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in alBalances)
            {
                //必须是有效的，未处理过欠费结算的
                if (balance.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString()
                    && balance.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid
                    && balance.BalanceSaveType != "1")
                {
                    alQBalances.Add(balance);
                }
            }

            if (alQBalances.Count == 0)
            {
                CommonController.Instance.MessageBox("患者没有欠费结算信息，请重新输入住院号", MessageBoxIcon.Information);
                return;
            }
            this.SetValue(this.spDetail_Sheet1, alQBalances);
        }
        #endregion

    }
}
