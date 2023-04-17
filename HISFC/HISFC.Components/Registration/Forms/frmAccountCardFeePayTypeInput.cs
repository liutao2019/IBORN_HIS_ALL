using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Registration.Forms
{
    /// <summary>
    /// 挂号费支付方式输入界面
    /// </summary>
    public partial class frmAccountCardFeePayTypeInput : FS.FrameWork.WinForms.Forms.BaseForm
    {
        /// <summary>
        /// 挂号费用类别最大数(包含：3挂号费 4诊金 2病历本费 1卡费用 5婴儿体检费)
        /// </summary>
        private static int MAXFEETYPE = 5;

        private FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();

        private FS.FrameWork.Public.ObjectHelper payWayHelper = new FS.FrameWork.Public.ObjectHelper();

        private List<AccountCardFee> accountCardFeeList = null;

        /// <summary>
        /// 挂号费集合
        /// </summary>
        public List<AccountCardFee> AccountCardFeeList
        {
            set
            {
                this.accountCardFeeList = value;
                this.SetCost();
                this.SetFocus();
            }
            get
            {
                return this.accountCardFeeList;
            }
        }

        /// <summary>
        /// 挂号费支付方式输入界面
        /// </summary>
        public frmAccountCardFeePayTypeInput()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            #region 屏蔽FP回车
            InputMap im;
            im = this.neuFpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            #endregion

            #region 初始化FP格式
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            ArrayList al = constantManager.GetList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (al == null)
            {
                MessageBox.Show("获取支付方式失败!");
                return;
            }
            payWayHelper.ArrayObject = al;
            comboType.Items = new string[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = al[i] as FS.FrameWork.Models.NeuObject;
                comboType.Items[i] = obj.Name;
            }
            this.neuFpEnter1_Sheet1.Columns[(int)ColumnPay.PayWay].CellType = comboType;
            #endregion
        }

        private void SetCost()
        {
            if (accountCardFeeList.Count > MAXFEETYPE || accountCardFeeList.Count < 1)
            {
                MessageBox.Show("挂号费类别错误!");
                return;
            }
            foreach (AccountCardFee fee in accountCardFeeList)
            {
                this.neuFpEnter1_Sheet1.Cells["&FeeType" + (int)fee.FeeType].Value = fee.Tot_cost;
                this.neuFpEnter1_Sheet1.Cells["&FeeType" + (int)fee.FeeType].Locked = true;
                if (string.IsNullOrEmpty(fee.PayType.ID) == true)
                {
                    this.neuFpEnter1_Sheet1.Cells["&PayWay" + (int)fee.FeeType].Text = "现金";
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells["&PayWay" + (int)fee.FeeType].Text = this.payWayHelper.GetName(fee.PayType.ID);
                }
                this.neuFpEnter1_Sheet1.Cells["&PayWay" + (int)fee.FeeType].Locked = false;
            }
        }

        private void SetFocus()
        {
            int focusRowIndex = 0;
            for (int i = 0; i < this.neuFpEnter1_Sheet1.RowCount; i++)
            {
                if (string.IsNullOrEmpty(this.neuFpEnter1_Sheet1.Cells[i, (int)ColumnPay.PayWay].Text) == false)
                {
                    focusRowIndex = i;
                    break;
                }
            }
            this.neuFpEnter1.Focus();
            this.neuFpEnter1_Sheet1.SetActiveCell(focusRowIndex, (int)ColumnPay.PayWay);
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            foreach (AccountCardFee fee in accountCardFeeList)
            {
                string payModeName = this.neuFpEnter1_Sheet1.Cells["&PayWay" + (int)fee.FeeType].Text;
                fee.PayType.ID = payWayHelper.GetID(payModeName);
                fee.PayType.Name = payModeName;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }

    enum ColumnPay
    {
        /// <summary>
        /// 挂号费类别
        /// </summary>
        FeeType,
        /// <summary>
        /// 挂号费金额
        /// </summary>
        PayCost,
        /// <summary>
        /// 支付方式
        /// </summary>
        PayWay
    }
}
