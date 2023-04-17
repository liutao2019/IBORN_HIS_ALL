using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data; 
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FS.HISFC.BizProcess.Interface.Account;
using FS.HISFC.Components.Account.Forms;
using FS.HISFC.BizProcess.Interface.Fee;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    /// <summary>
    /// 卡卷管理
    /// </summary>
    public partial class ucCardVolume : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucCardVolume()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
       
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        
        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        
        /// <summary>
        /// 卡卷状态
        /// </summary>
        private ArrayList validStateList = new ArrayList();

        /// <summary>
        /// 门诊卡号
        /// </summary>
        HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// 所有收费员
        /// </summary>
        private ArrayList allOper = new ArrayList();

        /// <summary>
        /// 所有卡卷信息
        /// </summary>
        private ArrayList alCardVolume = new ArrayList();
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.Init() < 0)
            {
                MessageBox.Show("初始化失败！");
                return;
            }
            this.ckSelect.CheckedChanged += new EventHandler(ckSelect_CheckedChanged);
            base.OnLoad(e);
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("卡卷登记", "登记", FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolbarService.AddToolButton("卡卷查询", "查询", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolbarService.AddToolButton("清屏", "清屏", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolbarService.AddToolButton("卡卷停用", "作废", FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);
            toolbarService.AddToolButton("卡卷启用", "作废", FS.FrameWork.WinForms.Classes.EnumImageList.K开帐, true, false, null);

            return toolbarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "卡卷登记":
                    {
                        Save();
                        break;
                    }
                case "卡卷查询":
                    {
                        Query();
                        break;
                    }
                case "清屏":
                    {
                        this.Clear();
                        break;
                    }
                case "卡卷停用":
                    {
                        this.Stop();
                        break;
                    }
                case "卡卷启用":
                    {
                        this.Start();
                        break;
                    }

                    base.ToolStrip_ItemClicked(sender, e);
            }
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                ExecCmdKey();
            }
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// 回车处理
        /// </summary>
        protected virtual void ExecCmdKey()
        {
            if (this.txtCardNo.Focused)
            {
                GetAccountInfo();
            }
        }
        private int Init()
        {
            this.allOper = this.conMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            FS.HISFC.Models.Base.Const cons = new FS.HISFC.Models.Base.Const();
            cons.ID = "ALL";
            cons.Name = "全部";
            validStateList.Add(cons);
            cons = new FS.HISFC.Models.Base.Const();
            cons.ID = "1";
            cons.Name = "有效"; ;
            validStateList.Add(cons);
            cons = new FS.HISFC.Models.Base.Const();
            cons.ID = "0";
            cons.Name = "无效"; ;
            validStateList.Add(cons);
            this.cmbCardVolumeState.AddItems(validStateList);
            this.cmbCardVolumeState.Tag = "ALL";

            this.cmbOper.AddItems(allOper);

            this.dtBegTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegTime.Value.ToLongDateString() + " 00:00:00");
            this.dtEndTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEndTime.Value.ToLongDateString() + " 23:59:59");
            return 1;
        }
        /// <summary>
        /// 输入就诊卡号获取账户信息
        /// </summary>
        private void GetAccountInfo()
        {
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            string markNO = this.txtCardNo.Text.Trim();
            if (markNO == string.Empty)
            {
                MessageBox.Show("请输入就诊卡号！");
                return;
            }
            int resultValue = accountManager.GetCardByRule(markNO, ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(accountManager.Err);
                return;
            }

            this.txtCardNo.Text = accountCard.Patient.PID.CardNO;

        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            this.dtBegTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegTime.Value.ToLongDateString() + " 00:00:00");
            this.dtEndTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEndTime.Value.ToLongDateString() + " 23:59:59");
            
            if (Valid() < 0) return;

            if (this.txtBegVoluneNo.Text.Trim() == this.txtEndVolumeNo.Text.Trim())
            {
                this.txtBegVoluneNo.Text = "";
            }

            string begVoluneNo = this.txtBegVoluneNo.Text;
            string endVoluneNo = this.txtEndVolumeNo.Text;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (string.IsNullOrEmpty(this.txtBegVoluneNo.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndVolumeNo.Text.Trim()))
            {
                this.txtBegVoluneNo.Text = this.txtEndVolumeNo.Text;
                begVoluneNo = endVoluneNo;
                this.txtEndVolumeNo.Text = "";
                if (RegCardVolume(begVoluneNo) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("登记卡卷失败！" + accountManager.Err, "错误");
                    return;
                }
            }
            else if (!string.IsNullOrEmpty(this.txtBegVoluneNo.Text.Trim()) && string.IsNullOrEmpty(this.txtEndVolumeNo.Text.Trim()))
            {
                if (RegCardVolume(begVoluneNo) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("登记卡卷失败！" + accountManager.Err, "错误");
                    return;
                }
            }
            else
            {
                string begVolumeNoStr1 = "";
                string endVolumeNoStr1 = "";
                int begVolumeNoNum1 = 0;
                int endVolumeNoNum1 = 0;
                int begVolumeIndex1 = 0;
                int endVolumeIndex1 = 0;
                int begNumLength1 = 0;
                int endNumLength1 = 0;
                if (begVoluneNo.Length != endVoluneNo.Length)
                {
                    MessageBox.Show("卡卷号码开始卡卷号长度与结束卡卷号长度不一致！");
                    return;
                }
                begVolumeIndex1 = this.JudgeVolumeNo(begVoluneNo.ToString(), ref begVolumeNoStr1, ref begVolumeNoNum1, ref begNumLength1);
                endVolumeIndex1 = this.JudgeVolumeNo(endVoluneNo.ToString(), ref endVolumeNoStr1, ref endVolumeNoNum1, ref endNumLength1);

                if (begVolumeIndex1 < 0 || endVolumeIndex1 < 0)
                {
                    MessageBox.Show("卡卷号码规则不符！");
                    return;
                } 
                if (begVolumeNoStr1 != endVolumeNoStr1)
                {
                    MessageBox.Show("卡卷号码前缀不一致！");
                    return;
                }
                if (begVolumeNoNum1 > endVolumeNoNum1)
                {
                    MessageBox.Show("登记卡卷号段，开始号不允许大于结束号！");
                    return;
                }
                int Num = 0;
                string volumeNo = "";
                for (Num = begVolumeNoNum1; Num <= endVolumeNoNum1; Num++)
                {
                    string str = Num.ToString().PadLeft(begNumLength1,'0');//以防前缀有0
                    volumeNo = begVolumeNoStr1 + str;
                    if (RegCardVolume(volumeNo) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("登记卡卷失败！" + accountManager.Err, "错误");
                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("登记成功！");
            this.txtBegVoluneNo.Text = "";
            this.txtEndVolumeNo.Text = "";




           
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {
            if (string.IsNullOrEmpty(this.txtBegVoluneNo.Text.Trim()) && string.IsNullOrEmpty(this.txtEndVolumeNo.Text.Trim()))
            {
                MessageBox.Show("请输入卡卷号码！");
                return -1;
            }
            decimal money = FS.FrameWork.Function.NConvert.ToDecimal(this.txtMoney.Text);
            if (money <= 0)
            {
                MessageBox.Show("请输入正确的卡卷金额！");
                this.txtMoney.Focus();
                return -1;
            }
            if (this.dtBegTime.Value > this.dtEndTime.Value)
            {
                MessageBox.Show("截止时间不能小于开始时间！");
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 登记卡卷
        /// </summary>
        /// <returns></returns>
        private int RegCardVolume(string volumeNo)
        {
            if (string.IsNullOrEmpty(volumeNo))
            {
                return -1;
            }
            decimal money = FS.FrameWork.Function.NConvert.ToDecimal(this.txtMoney.Text);
            DateTime begTime = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegTime.Value.ToShortDateString() + " 00:00:00");
            DateTime endTime = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEndTime.Value.ToShortDateString() + " 23:59:59");
            FS.HISFC.Models.Account.CardVolume cardVolume = new FS.HISFC.Models.Account.CardVolume();
            cardVolume.VolumeNo = volumeNo;
            cardVolume.Money = money;
            cardVolume.BegTime = begTime;
            cardVolume.EndTime = endTime;
            cardVolume.Mark = this.txtMark.Text;
            cardVolume.CreateEnvironment.ID = this.accountManager.Operator.ID;
            cardVolume.CreateEnvironment.OperTime = this.accountManager.GetDateTimeFromSysDateTime();
            cardVolume.OperEnvironment.ID = this.accountManager.Operator.ID;
            cardVolume.OperEnvironment.OperTime = this.accountManager.GetDateTimeFromSysDateTime();
            cardVolume.ValidState = EnumValidState.Valid;

            if (this.accountManager.InsertAccountCardVolume(cardVolume) < 0)
            {
                return -1;
            }            
            return 1;

        }
        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.txtBegVoluneNo.Text = string.Empty;
            this.txtEndVolumeNo.Text = string.Empty;
            this.txtInvoiceNo.Text = string.Empty;
            this.txtMark.Text = string.Empty;
            this.txtMoney.Text = "0.00";
            this.txtCardNo.Text = string.Empty;
            this.cmbCardVolumeState.Tag = "ALL";
            this.cmbOper.Text = "全部";
            this.cmbOper.Tag = "ALL";


        }
        /// <summary>
        /// 查询卡卷信息
        /// </summary>
        private void Query()
        {
            if (this.dtBegTime.Value > this.dtEndTime.Value)
            {
                MessageBox.Show("开始时间不能大于结束时间");
                return;
            }
            if(string.IsNullOrEmpty( this.cmbCardVolumeState.Tag.ToString())||string.IsNullOrEmpty( this.cmbCardVolumeState.Text))
            {
                MessageBox.Show("请选择卡卷状态！");
                return;
            }

            string begVolume = this.txtBegVoluneNo.Text.Trim();
            string endVolume = this.txtEndVolumeNo.Text.Trim();
            string invoiceNo = this.txtInvoiceNo.Text.Trim();
            string operCode = this.cmbOper.Tag.ToString();
            string vaildState = this.cmbCardVolumeState.Tag.ToString();
            string cardNo = this.txtCardNo.Text.Trim();
            string memo = this.txtMark.Text.Trim();// {BF0CE580-BF7B-486a-897A-DBBC2A5CB653}

            alCardVolume = this.accountManager.QueryAccountCardVolumeList(begVolume, endVolume, this.dtBegTime.Value, this.dtEndTime.Value, invoiceNo, vaildState, operCode, cardNo, memo);
            if(alCardVolume == null)
            {
                MessageBox.Show("获取卡卷信息失败！" + this.accountManager.Err);
                return;
            }
            this.SetFP();

        }
        /// <summary>
        /// 设置表格
        /// </summary>
        private void SetFP()
        {
            if (alCardVolume == null)
            {
                return;
            }
            this.neuSpread1_Sheet1.RowCount = 0;            
            this.neuSpread1_Sheet1.RowCount = alCardVolume.Count;
            int count = 0;
            foreach (CardVolume cardVolume in alCardVolume)
            {
                this.neuSpread1_Sheet1.Cells[count, 0].Text = cardVolume.VolumeNo;
                this.neuSpread1_Sheet1.Cells[count, 1].Text = cardVolume.BegTime.ToString();
                this.neuSpread1_Sheet1.Cells[count, 2].Text = cardVolume.EndTime.ToString();
                this.neuSpread1_Sheet1.Cells[count, 3].Text = cardVolume.Money.ToString("F2");
                string validState = string.Empty;
                if (cardVolume.ValidState == EnumValidState.Valid)
                {
                    validState = "有效";
                }
                else if (cardVolume.ValidState == EnumValidState.Invalid)
                {
                    validState = "无效";
                }
                this.neuSpread1_Sheet1.Cells[count, 4].Text = validState;
                if (cardVolume.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    neuSpread1_Sheet1.Rows[count].ForeColor = Color.Red;
                }
                this.neuSpread1_Sheet1.Cells[count, 5].Text = cardVolume.UseType.Name;
                this.neuSpread1_Sheet1.Cells[count, 6].Text = cardVolume.InvoiceNo;
                this.neuSpread1_Sheet1.Cells[count, 7].Text = cardVolume.Patient.PID.CardNO;
                this.neuSpread1_Sheet1.Cells[count, 8].Text = cardVolume.Mark;
                this.neuSpread1_Sheet1.Cells[count, 9].Text = cardVolume.OperEnvironment.Name;
                this.neuSpread1_Sheet1.Cells[count, 10].Text = cardVolume.OperEnvironment.OperTime.ToString();
                this.neuSpread1_Sheet1.Rows[count].Tag = cardVolume;
                count++;
            }

        }

        /// <summary>
        /// 卡卷停用
        /// </summary>
        private void Stop()
        {
            if (string.IsNullOrEmpty(this.txtBegVoluneNo.Text.Trim()) && string.IsNullOrEmpty(this.txtEndVolumeNo.Text.Trim()))
            {
                MessageBox.Show("请输入卡卷号码！");
                return;
            }
            string begVoluneNo = this.txtBegVoluneNo.Text.Trim();
            string endVoluneNo = this.txtEndVolumeNo.Text.Trim();
            string operCode = this.accountManager.Operator.ID;
            DateTime date = this.accountManager.GetDateTimeFromSysDateTime();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (string.IsNullOrEmpty(this.txtBegVoluneNo.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndVolumeNo.Text.Trim()))
            {
                this.txtBegVoluneNo.Text = this.txtEndVolumeNo.Text;
                begVoluneNo = endVoluneNo;
                this.txtEndVolumeNo.Text = "";
                if (this.accountManager.UpdateCardVolumeState(begVoluneNo, "1", "0", operCode, date) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("停用卡卷失败！" + accountManager.Err, "错误");
                    return;
                }
            }
            else if (!string.IsNullOrEmpty(this.txtBegVoluneNo.Text.Trim()) && string.IsNullOrEmpty(this.txtEndVolumeNo.Text.Trim()))
            {
                if (this.accountManager.UpdateCardVolumeState(begVoluneNo, "1", "0", operCode, date) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("停用卡卷失败！" + accountManager.Err, "错误");
                    return;
                }
            }
            else
            {
                string begVolumeNoStr1 = "";
                string endVolumeNoStr1 = "";
                int begVolumeNoNum1 = 0;
                int endVolumeNoNum1 = 0;
                int begVolumeIndex1 = 0;
                int endVolumeIndex1 = 0;
                int begNumLength1 = 0;
                int endNumLength1 = 0;
                if (begVoluneNo.Length != endVoluneNo.Length)
                {
                    MessageBox.Show("卡卷号码开始卡卷号长度与结束卡卷号长度不一致！");
                    return;
                }
                begVolumeIndex1 = this.JudgeVolumeNo(begVoluneNo.ToString(), ref begVolumeNoStr1, ref begVolumeNoNum1, ref begNumLength1);
                endVolumeIndex1 = this.JudgeVolumeNo(endVoluneNo.ToString(), ref endVolumeNoStr1, ref endVolumeNoNum1, ref endNumLength1);

                if (begVolumeIndex1 < 0 || endVolumeIndex1 < 0)
                {
                    MessageBox.Show("卡卷号码规则不符！");
                    return;
                }
                if (begVolumeNoStr1 != endVolumeNoStr1)
                {
                    MessageBox.Show("卡卷号码前缀不一致！");
                    return;
                }
                if (begVolumeNoNum1 > endVolumeNoNum1)
                {
                    MessageBox.Show("登记卡卷号段，开始号不允许大于结束号！");
                    return;
                }
                int Num = 0;
                string volumeNo = "";
                for (Num = begVolumeNoNum1; Num <= endVolumeNoNum1; Num++)
                {
                    string str = Num.ToString().PadLeft(begNumLength1, '0');//以防前缀有0
                    volumeNo = begVolumeNoStr1 + str;
                    if (this.accountManager.UpdateCardVolumeState(volumeNo, "1", "0", operCode, date) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("停用卡卷失败！" + accountManager.Err, "错误");
                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("停用成功！");
            this.txtBegVoluneNo.Text = "";
            this.txtEndVolumeNo.Text = "";

            this.Query();

        }
        /// <summary>
        /// 卡卷启用
        /// </summary>
        private void Start()
        {
            if (string.IsNullOrEmpty(this.txtBegVoluneNo.Text.Trim()) && string.IsNullOrEmpty(this.txtEndVolumeNo.Text.Trim()))
            {
                MessageBox.Show("请输入卡卷号码！");
                return;
            }
            string begVoluneNo = this.txtBegVoluneNo.Text.Trim();
            string endVoluneNo = this.txtEndVolumeNo.Text.Trim();
            string operCode = this.accountManager.Operator.ID;
            DateTime date = this.accountManager.GetDateTimeFromSysDateTime();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (string.IsNullOrEmpty(this.txtBegVoluneNo.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndVolumeNo.Text.Trim()))
            {
                this.txtBegVoluneNo.Text = this.txtEndVolumeNo.Text;
                begVoluneNo = endVoluneNo;
                this.txtEndVolumeNo.Text = "";
                if (this.accountManager.UpdateCardVolumeState(begVoluneNo, "0", "1", operCode, date) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("停用卡卷失败！" + accountManager.Err, "错误");
                    return;
                }
            }
            else if (!string.IsNullOrEmpty(this.txtBegVoluneNo.Text.Trim()) && string.IsNullOrEmpty(this.txtEndVolumeNo.Text.Trim()))
            {
                if (this.accountManager.UpdateCardVolumeState(begVoluneNo, "0", "1", operCode, date) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("停用卡卷失败！" + accountManager.Err, "错误");
                    return;
                }
            }
            else
            {
                string begVolumeNoStr1 = "";
                string endVolumeNoStr1 = "";
                int begVolumeNoNum1 = 0;
                int endVolumeNoNum1 = 0;
                int begVolumeIndex1 = 0;
                int endVolumeIndex1 = 0;
                int begNumLength1 = 0;
                int endNumLength1 = 0;
                if (begVoluneNo.Length != endVoluneNo.Length)
                {
                    MessageBox.Show("卡卷号码开始卡卷号长度与结束卡卷号长度不一致！");
                    return;
                }
                begVolumeIndex1 = this.JudgeVolumeNo(begVoluneNo.ToString(), ref begVolumeNoStr1, ref begVolumeNoNum1, ref begNumLength1);
                endVolumeIndex1 = this.JudgeVolumeNo(endVoluneNo.ToString(), ref endVolumeNoStr1, ref endVolumeNoNum1, ref endNumLength1);

                if (begVolumeIndex1 < 0 || endVolumeIndex1 < 0)
                {
                    MessageBox.Show("卡卷号码规则不符！");
                    return;
                }
                if (begVolumeNoStr1 != endVolumeNoStr1)
                {
                    MessageBox.Show("卡卷号码前缀不一致！");
                    return;
                }
                if (begVolumeNoNum1 > endVolumeNoNum1)
                {
                    MessageBox.Show("登记卡卷号段，开始号不允许大于结束号！");
                    return;
                }
                int Num = 0;
                string volumeNo = "";
                for (Num = begVolumeNoNum1; Num <= endVolumeNoNum1; Num++)
                {
                    string str = Num.ToString().PadLeft(begNumLength1, '0');//以防前缀有0
                    volumeNo = begVolumeNoStr1 + str;
                    if (this.accountManager.UpdateCardVolumeState(volumeNo, "0", "1", operCode, date) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("停用卡卷失败！" + accountManager.Err, "错误");
                        return;
                    }
                }
            }


            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("启用成功！");
            this.txtBegVoluneNo.Text = "";
            this.txtEndVolumeNo.Text = "";

            this.Query();
        }
        private void CancelCardVolume(string volume)
        {

        }
        /// <summary>
        /// 判断卡卷号码开头是否包含字母
        /// </summary>
        /// <param name="volumeNo"></param>
        /// <param name="volumeNoHead"></param>
        /// <param name="volumeNum"></param>
        /// <param name="numLength">以防前缀有0</param>
        /// <returns></returns>
        private int JudgeVolumeNo(string volumeNo, ref string volumeNoHead,ref int volumeNum,ref int numLength)
        {
            int index = 0;
            volumeNoHead = string.Empty;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            string numOrStr = "";//volumeNo.Substring(0, index + 1);
            foreach (Char ch in volumeNo.ToCharArray())
            {
                numOrStr = ch.ToString();
                if (System.Text.RegularExpressions.Regex.IsMatch(numOrStr, @"^\d+$"))
                {
                    //return index;
                }
                else
                {
                    volumeNoHead += numOrStr;
                    index ++;
                    numOrStr = "";
                }

            }
            string volumeEnd = volumeNo.Substring(index, volumeNo.Length - volumeNoHead.Length);
            numLength = volumeEnd.Length;
            if (!System.Text.RegularExpressions.Regex.IsMatch(volumeEnd, @"^\d+$"))
            {
                index = -1;
            }
            else
            {
                volumeNum = int.Parse(volumeEnd);
            }

            return index;

        }
        private void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.gbQueryCon.Visible = this.ckSelect.Checked;

        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (this.neuSpread1.ActiveSheet != this.neuSpread1_Sheet1)
            {
                return;
            }

            if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet1 && this.neuSpread1_Sheet1.ActiveRow == null)
            {
                return;
            }
            if (e.ColumnHeader || this.neuSpread1_Sheet1.RowCount == 0) return;
            int row = e.Row;
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                if (this.neuSpread1_Sheet1.Rows[row].Tag != null)
                {
                    CardVolume cardVolume = new CardVolume();
                    cardVolume = this.neuSpread1_Sheet1.Rows[row].Tag as CardVolume;

                    if (string.IsNullOrEmpty(this.txtBegVoluneNo.Text))
                    {
                        this.txtBegVoluneNo.Text = cardVolume.VolumeNo;
                    }
                    else if (string.IsNullOrEmpty(this.txtEndVolumeNo.Text))
                    {
                        this.txtEndVolumeNo.Text = cardVolume.VolumeNo;
                    }
                    else if (!string.IsNullOrEmpty(this.txtEndVolumeNo.Text) && !string.IsNullOrEmpty(this.txtBegVoluneNo.Text))
                    {
                        this.txtBegVoluneNo.Text = cardVolume.VolumeNo;
                    }

                    if (cardVolume.ValidState == EnumValidState.Valid)
                    {
                        if (MessageBox.Show("是否停用该卡卷？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            return;
                        }
                        this.Stop();
                    }
                    else
                    {

                        if (MessageBox.Show("是否启用该卡卷？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            return;
                        }
                        this.Start();
                    }
                }
            }
            //this.Query();
        }



    }
}
