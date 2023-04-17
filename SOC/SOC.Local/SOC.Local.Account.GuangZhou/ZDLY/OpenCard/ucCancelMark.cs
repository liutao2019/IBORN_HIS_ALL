using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Account;

namespace FS.SOC.Local.Account.GuangZhou.Zdly.OpenCard
{
    public partial class ucCancelMark : UserControl
    {
        /// <summary>
        /// 就诊卡停用
        /// </summary>
        /// <param name="list"></param>
        public ucCancelMark(List<AccountCard> list)
        {
            InitializeComponent();
            markList = list;
        }

        #region 变量
        /// <summary>
        /// 患者有效卡集合
        /// </summary>
        List<AccountCard> markList = new List<AccountCard>();

        /// <summary>
        /// 综合管理业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        public delegate bool EventStopCard(List<AccountCard> list);
        /// <summary>
        /// 作废卡事件
        /// </summary>
        public event EventStopCard StopCardEvent;

        /// <summary>
        /// 新建卡是否收取卡成本费，0=不收取，1=收取，2=按卡类别收取
        /// </summary>
        public string IsAcceptCardFee = "0";
        /// <summary>
        /// 补卡是否收取卡成本费 0=不收取，1=收取，2=按卡类别收取
        /// </summary>
        public string IsAcceptChangeCardFee = "0";
        /// <summary>
        /// 退卡时退费 0=不退，1=退费
        /// </summary>
        public string ReturnCardReturnFee = "0";
        /// <summary>
        /// 所选卡列表
        /// </summary>
        public List<AccountCard> lstCard = null;
        #endregion

        #region 方法
        private void InitListView()
        {
            this.neuSpMarkSheet.RowCount = 0;
            this.neuSpMarkSheet.RowCount = markList.Count;

            FS.FrameWork.Models.NeuObject obj = null;
            AccountCard accountCard = null;

            for (int idx = 0; idx < markList.Count; idx++)
            {
                accountCard = markList[idx] as AccountCard;
                if (accountCard == null)
                    continue;

                this.neuSpMarkSheet.Cells[idx, 2].Value = accountCard.MarkNO;

                //查找卡类型名称
                obj = managerIntegrate.GetConstant("MarkType", accountCard.MarkType.ID);
                if (obj != null)
                {
                    accountCard.MarkType.Name = obj.Name;
                }
                this.neuSpMarkSheet.Cells[idx, 3].Value = accountCard.MarkType.Name;

                this.neuSpMarkSheet.Rows[idx].Tag = accountCard;
            }
        }
        #endregion

        #region 事件
        private void ucCancelMark_Load(object sender, EventArgs e)
        {
            this.FindForm().Text = "就诊卡信息";
            InitListView();

            lstCard = null;

            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
        }

        void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 0 || e.Column == 1)
            {
                bool bln = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpMarkSheet.Cells[e.Row, e.Column].Value);
                if (bln)
                {
                    this.neuSpMarkSheet.Cells[e.Row, 1 - e.Column].Value = 0;
                }

            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            lstCard = null;
            List<AccountCard> tempMarkList = new List<AccountCard>();
            AccountCard accCard = null;

            for (int idx = 0; idx < this.neuSpMarkSheet.RowCount; idx++)
            {
                accCard = this.neuSpMarkSheet.Rows[idx].Tag as AccountCard;
                if (accCard == null)
                    continue;

                accCard.MarkStatus = MarkOperateTypes.Begin;
                bool bln = Convert.ToBoolean(this.neuSpMarkSheet.Cells[idx, 0].Value);
                if (bln)
                {
                    accCard.MarkStatus = MarkOperateTypes.Stop;
                }
                bln = Convert.ToBoolean(this.neuSpMarkSheet.Cells[idx, 1].Value);
                if (bln)
                {
                    accCard.MarkStatus = MarkOperateTypes.Cancel;
                }

                if (accCard.MarkStatus == MarkOperateTypes.Begin)
                    continue;

                tempMarkList.Add(accCard);
            }

            if (tempMarkList.Count == 0)
            {
                MessageBox.Show("请选择要停用或退回的卡号！", "提示");
                return;
            }
            else
            {
                lstCard = tempMarkList;
            }

            if (StopCardEvent != null)
            {
                bool resultValue = StopCardEvent(tempMarkList);
                if (resultValue)
                {
                    this.FindForm().DialogResult = DialogResult.OK;
                }
                else
                {
                    this.FindForm().DialogResult = DialogResult.Cancel;
                }
            }

            this.FindForm().DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.No;
        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            bool bl = this.ckAllStop.Checked;
            if (bl)
            {
                this.ckAllBack.CheckedChanged -= new EventHandler(ckAllBack_CheckedChanged);

                this.ckAllBack.Checked = false;

                this.ckAllBack.CheckedChanged += new EventHandler(ckAllBack_CheckedChanged);

                for (int idx = 0; idx < this.neuSpMarkSheet.RowCount; idx++)
                {
                    this.neuSpMarkSheet.Cells[idx, 0].Value = 1;
                    this.neuSpMarkSheet.Cells[idx, 1].Value = 0;
                }
            }
            else
            {
                for (int idx = 0; idx < this.neuSpMarkSheet.RowCount; idx++)
                {
                    this.neuSpMarkSheet.Cells[idx, 0].Value = 0;
                }
            }
        }
        #endregion

        private void ckAllBack_CheckedChanged(object sender, EventArgs e)
        {
            bool bl = this.ckAllBack.Checked;
            if (bl)
            {
                this.ckAllStop.CheckedChanged -= new EventHandler(ckAll_CheckedChanged);

                this.ckAllStop.Checked = false;

                this.ckAllStop.CheckedChanged += new EventHandler(ckAll_CheckedChanged);

                for (int idx = 0; idx < this.neuSpMarkSheet.RowCount; idx++)
                {
                    this.neuSpMarkSheet.Cells[idx, 0].Value = 0;
                    this.neuSpMarkSheet.Cells[idx, 1].Value = 1;
                }
            }
            else
            {
                for (int idx = 0; idx < this.neuSpMarkSheet.RowCount; idx++)
                {
                    this.neuSpMarkSheet.Cells[idx, 1].Value = 0;
                }
            }
        }
    }
}
