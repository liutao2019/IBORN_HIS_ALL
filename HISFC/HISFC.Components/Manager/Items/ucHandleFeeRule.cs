using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.Manager.Items
{
    public partial class ucHandleFeeRule : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucHandleFeeRule()
        {
            InitializeComponent();
        }

        public Hashtable itemHash = new Hashtable();

        public Hashtable feeTypeHash = new Hashtable();

        private HISFC.BizLogic.Fee.UndrugFeeRegularManager feeRegularManager = new FS.HISFC.BizLogic.Fee.UndrugFeeRegularManager();

        /// <summary>
        /// 0  插入, 1 更新
        /// </summary>
        public int operMode = 0;

        public void InitFeeRegular(ArrayList alUndrug)
        {
            this.lbDescription.Text = "";

            this.rdoQuality.AutoEllipsis = true;
            this.rdoTime.AutoEllipsis = true;
            this.rdoQuality.Checked = true;
            this.cmbItem.Items.Clear();
            this.cmbItem.AddItems(alUndrug);
            if (this.cmbItem.Items.Count > 0)
            {
                this.cmbItem.SelectedIndex = 0;
            }
            this.cmbRegular.Items.Clear();

            ArrayList alTemp = new ArrayList();
            foreach (DictionaryEntry de in this.feeTypeHash)
            {
                FS.FrameWork.Models.NeuObject obj = de.Value as FS.FrameWork.Models.NeuObject;
                alTemp.Add(obj);
            }
            this.cmbRegular.AddItems(alTemp);

            if (this.chkedList.Items.Count >= 0)
            {
                this.chkedList.Items.Clear();
            }
        }


        /// <summary>
        /// 清除内容
        /// </summary>
        private void ClearFeeRule()
        {
            this.rdoQuality.Checked = true;
            this.lbDescription.Text = "";
            this.txtQuality.Text = "0";
            this.cmbRegular.Focus();
            this.chkedList.Items.Clear();
        }

        /// <summary>
        /// 设置界面信息
        /// </summary>
        /// <param name="undrugfeeRegular"></param>
        /// <returns></returns>
        public int SetFeeRegular(FS.HISFC.Models.Fee.Item.UndrugFeeRegular undrugfeeRegular)
        {
            ClearFeeRule();

            if (undrugfeeRegular != null)
            {
                this.ruleCode.Text = undrugfeeRegular.ID;
                this.tbItemCode.Text = undrugfeeRegular.Item.ID;
                this.neuTextBox2.Text = undrugfeeRegular.Item.Name;
                if (this.feeTypeHash.Contains(undrugfeeRegular.Regular.ID.ToString()))
                {
                    this.cmbRegular.Text = ((NeuObject)this.feeTypeHash[undrugfeeRegular.Regular.ID]).Name;
                    this.lbDescription.Text = ((NeuObject)this.feeTypeHash[undrugfeeRegular.Regular.ID]).Memo.ToString();
                }
                this.ckOutFee.Checked = undrugfeeRegular.IsOutFee;
                if (this.cmbRegular != null && this.cmbRegular.Items.Count > 0)
                {

                    this.cmbRegular.Tag = undrugfeeRegular.Regular.ID.ToString();
                    this.lbDescription.Text = "";
                }
                //this.cmbRegular.Text = undrugfeeRegular.Regular.ID.ToString();
                //  this.lbDescription.Text = this.cmbRegular.SelectedItem.Memo.ToString();
                if (FS.FrameWork.Function.NConvert.ToInt32(undrugfeeRegular.LimitCondition.ToString()) == 1)
                {
                    this.rdoTime.Checked = true;
                }
                else
                {
                    this.rdoQuality.Checked = true;
                }
                this.txtQuality.Text = undrugfeeRegular.DayLimit.ToString();

                string[] mutex = undrugfeeRegular.LimitItem.ID.Split('|');

                if (mutex != null && mutex.Length > 0)
                {

                    for (int i = 0; i < mutex.Length; i++)
                    {
                        if (itemHash.Contains(mutex[i].ToString()))
                        {
                            this.chkedList.Items.Add(itemHash[mutex[i].ToString()], true);
                        }

                    }
                }
                return 1;
            }
            return 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Fee.Item.UndrugFeeRegular undrugRuleTemp = new FS.HISFC.Models.Fee.Item.UndrugFeeRegular();
            int returnValue = 0;
            undrugRuleTemp = GetFeeregular(out returnValue);

            if (returnValue <= 0)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.feeRegularManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            ArrayList al = this.feeRegularManager.GetFeeRegularByItemCode(this.tbItemCode.Text);
            if (al == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this, "获取收费规则出错！" + this.feeRegularManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (operMode == 1)
            {
                returnValue = feeRegularManager.UpdateUndrugFeeRegular(undrugRuleTemp);

                if (returnValue == 1)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show(this,"更新成功","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this,"更新失败,请重试!"+this.feeRegularManager.Err,"错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (operMode == 0)
            {
                foreach (FS.HISFC.Models.Fee.Item.UndrugFeeRegular undrugFeeRegular in al)
                {
                    if (undrugFeeRegular.Regular.ID == undrugRuleTemp.Regular.ID)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this, "已存在该收费规则，请在原来的基础上修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                //取序列号
                string ruleSeq = feeRegularManager.GetFeeRegularSequence();

                if (!string.IsNullOrEmpty(ruleSeq))
                {
                    undrugRuleTemp.ID = ruleSeq;
                }

                returnValue = feeRegularManager.InsertUndrugFeeRegular(undrugRuleTemp);

                if (returnValue == 1)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show(this,"保存成功","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this,"插入失败,请重试!"+this.feeRegularManager.Err,"错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
            }

            this.ClearFeeRule();
            if (this.FindForm() != null)
            {
                this.FindForm().Close();
            }
        }

        /// <summary>
        /// 有效性判断
        /// </summary>
        /// <returns></returns>
        private int ValidFeeRule()
        {
            if (this.cmbRegular.SelectedItem == null || this.cmbRegular.SelectedIndex < 0)
            {
                MessageBox.Show("您应用了费用规则，必须选择收费规则！");
                this.cmbRegular.Focus();
                return -1;
            }
            decimal tot;
            if (!decimal.TryParse(this.txtQuality.Text, out tot))
            {
                MessageBox.Show("您应用了费用规则，＂数量限额＂必须为数字！");
                return -1;
                this.txtQuality.SelectAll();
            }

            return 1;
        }

        /// <summary>
        /// 获取费用规则实体
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Item.UndrugFeeRegular GetFeeregular(out int value)
        {
            FS.HISFC.Models.Fee.Item.UndrugFeeRegular feeRegular = new FS.HISFC.Models.Fee.Item.UndrugFeeRegular();
            value = 0;
            if (this.ValidFeeRule() > 0)
            {
                feeRegular.ID = this.ruleCode.Text;
                feeRegular.Item.ID = this.tbItemCode.Text.ToString();
                feeRegular.Item.Name = this.neuTextBox2.Text.ToString();
                if (this.rdoQuality.Checked)
                {
                    feeRegular.LimitCondition = "0";
                }
                if (this.rdoTime.Checked)
                {
                    feeRegular.LimitCondition = "1";
                }
                if (this.cmbRegular.SelectedItem != null)
                {
                    feeRegular.Regular.ID = this.cmbRegular.Tag.ToString();
                }
                feeRegular.DayLimit = FS.FrameWork.Function.NConvert.ToDecimal(this.txtQuality.Text.ToString());
                string limitCode = "";
                foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in this.chkedList.CheckedItems)
                {
                    string tempStr = undrug.ID + "|";
                    limitCode += tempStr;
                }
                if (!string.IsNullOrEmpty(limitCode))
                {
                    limitCode = limitCode.Substring(0, limitCode.Length - 1);
                }
                feeRegular.LimitItem.ID = limitCode;
                feeRegular.IsOutFee = this.ckOutFee.Checked;
                feeRegular.Oper.ID = this.feeRegularManager.Operator.ID.ToString();
                feeRegular.Oper.OperTime = this.feeRegularManager.GetDateTimeFromSysDateTime();
            }
            else
            {
                value = 0;
                return null;
            }
            value = 1;
            return feeRegular;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.chkedList.SelectedIndex >= 0)
            {
                this.chkedList.Items.Remove(this.chkedList.Items[this.chkedList.SelectedIndex]);
            }
            if (this.chkedList.Items.Count > 0)
            {
                this.chkedList.SelectedIndex = this.chkedList.Items.Count - 1;
            }
        }

        /// <summary>
        /// 添加互斥项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbItem_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            bool InSelected = false;

            string selectedItemcode = "";
            if (this.cmbItem.SelectedIndex < 0)
            {
                return;
            }
            else
            {
                int i = this.cmbItem.SelectedIndex;

                selectedItemcode = this.cmbItem.Tag.ToString();
            }

            if (!string.IsNullOrEmpty(selectedItemcode))
            {
                for (int i = 0; i < this.chkedList.Items.Count; i++)
                {
                    if (selectedItemcode.CompareTo(((FS.HISFC.Models.Fee.Item.Undrug)this.chkedList.CheckedItems[i]).ID.ToString()) == 0)
                    {
                        return;
                    }
                }
                if (itemHash.Contains(selectedItemcode))
                {
                    this.chkedList.Items.Add(itemHash[selectedItemcode], true);
                }
                int count = this.chkedList.Items.Count;
                this.chkedList.SelectedIndex = count - 1;
            }
            //if(this.cmbItem.SelectedItem !=null && this.cmbItem
        }

        //#region
        ///// <summary>
        ///// 收费规则管理类
        ///// </summary>
        //private HISFC.BizLogic.Fee.UndrugFeeRegularManager feeRegularManager = new FS.HISFC.BizLogic.Fee.UndrugFeeRegularManager();

        ///// <summary>
        ///// 操作类别
        ///// </summary>
        //private int operMode = 0;
        //#endregion

        //#region 方法
        //private int SaveData()
        //{
        //    int result = 0;
        //    //  DongGuan.HISFC.Object.Fee.Item.UndrugFeeRegular feeRegular = this.GetFeeregular(out result);
        //    HISFC.Object.Base.UndrugFeeRegular feeRegular = this.GetFeeregular(out result);
        //    if (feeRegular != null)
        //    {
        //        int operResult = 0;
        //        string opermode = "";
        //        switch (this.operMode)
        //        {
        //            case 0:
        //                operResult = this.feeRegularManager.InsertUndrugFeeRegular(feeRegular);
        //                break;
        //            case 2:
        //                operResult = this.feeRegularManager.InsertUndrugFeeRegular(feeRegular);
        //                opermode = "插入";
        //                break;
        //            case 1:
        //                operResult = this.feeRegularManager.UpdateUndrugFeeRegular(feeRegular);
        //                opermode = "更新";
        //                break;
        //            case 3:
        //                operResult = this.feeRegularManager.DeleteUndrugFeeRegular(this.tbItemCode.Text.ToString());
        //                opermode = "删除";
        //                break;
        //            default:
        //                break;
        //        }
        //        if (operResult != 1)
        //        {
        //            FS.FrameWork.Management.PublicTrans.RollBack();
        //            MessageBox.Show(FS.FrameWork.Management.Language.Msg(feeRegularManager.Err), "消息");
        //            return 01;
        //        }
        //    }
        //    else
        //    {
        //        if (result < 0)
        //        {
        //            FS.FrameWork.Management.PublicTrans.RollBack();
        //            MessageBox.Show("获取项目收费规则出错!");
        //            return;
        //        }
        //    }

        //}

        //private void InitFeeRegular()
        //{
        //    FillFeeRule();
        //    this.lbDescription.Text = "";
        //    FillItemList();

        //    this.rdoQuality.AutoEllipsis = true;
        //    this.rdoTime.AutoEllipsis = true;
        //    this.rdoQuality.Checked = true;
        //    if (this.chkedList.Items.Count >= 0)
        //    {
        //        this.chkedList.Items.Clear();
        //    }
        //}

        ///// <summary>
        ///// 填充 收费规则
        ///// </summary>
        //private void FillFeeRule()
        //{
        //    ArrayList feeRule = new ArrayList();

        //    feeRule = this.conManager.GetAllList("FEEREGULAR");

        //    if (feeRule != null && feeRule.Count > 0)
        //    {
        //        this.cmbRegular.AddItems(feeRule);
        //    }

        //    foreach (NeuObject tempObj in feeRule)
        //    {
        //        //初始化收费规则哈希表
        //        this.feeTypeHash.Add(tempObj.ID, tempObj);
        //    }
        //}

        ///// <summary>
        ///// 填充项目信息
        ///// </summary>
        //private void FillItemList()
        //{
        //    List<HISFC.Object.Fee.Item.Undrug> itemLists = new List<FS.HISFC.Models.Fee.Item.Undrug>();
        //    itemLists = this.feeManager.QueryAllItemsList();

        //    if (itemLists == null && itemLists.Count <= 0)
        //    {
        //        return;
        //    }
        //    ArrayList itemList = new ArrayList();
        //    foreach (HISFC.Object.Fee.Item.Undrug undrug in itemLists)
        //    {
        //        //初始化哈希表
        //        this.itemHash.Add(undrug.ID, undrug);
        //        //初始化项目列表
        //        itemList.Add(undrug);
        //    }
        //    this.cmbItem.AddItems(itemList);
        //}

        ///// <summary>
        ///// 填充收费规则TabPage
        ///// </summary>
        ///// <param name="itemcode"></param>
        //private void FillRegularPan(string ruleCode)
        //{
        //    HISFC.Object.Base.UndrugFeeRegular undrugFeeRegular = new FS.HISFC.Models.Fee.Item.UndrugFeeRegular();
        //    undrugFeeRegular = this.feeRegularManager.GetSingleFeeRegular(ruleCode);
        //    if (undrugFeeRegular != null)
        //    {
        //        SetFeeRegular(undrugFeeRegular);
        //    }
        //}

        ///// <summary>
        ///// 设置界面信息
        ///// </summary>
        ///// <param name="undrugfeeRegular"></param>
        ///// <returns></returns>
        //private int SetFeeRegular(HISFC.Object.Base.UndrugFeeRegular undrugfeeRegular)
        //{
        //    if (undrugfeeRegular != null)
        //    {
        //        if (this.feeTypeHash.Contains(undrugfeeRegular.Regular.ID.ToString()))
        //        {
        //            this.cmbRegular.Text = ((NeuObject)this.feeTypeHash[undrugfeeRegular.Regular.ID]).Name;
        //            this.lbDescription.Text = ((NeuObject)this.feeTypeHash[undrugfeeRegular.Regular.ID]).Memo.ToString();
        //        }
        //        if (this.cmbRegular != null && this.cmbRegular.Items.Count > 0)
        //        {

        //            this.cmbRegular.Text = undrugfeeRegular.Regular.ID.ToString();
        //            this.lbDescription.Text = "";
        //        }
        //        //this.cmbRegular.Text = undrugfeeRegular.Regular.ID.ToString();
        //        //  this.lbDescription.Text = this.cmbRegular.SelectedItem.Memo.ToString();
        //        if (NFC.Function.NConvert.ToInt32(undrugfeeRegular.LimitCondition.ToString()) == 1)
        //        {
        //            this.rdoTime.Checked = true;
        //        }
        //        else
        //        {
        //            this.rdoQuality.Checked = true;
        //        }
        //        this.txtQuality.Text = undrugfeeRegular.DayLimit.ToString();

        //        string[] mutex = undrugfeeRegular.LimitItem.ID.Split('|');

        //        if (mutex != null && mutex.Length > 0)
        //        {
        //            HISFC.Object.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();

        //            for (int i = 0; i < mutex.Length; i++)
        //            {
        //                if (this.itemHash.Contains(mutex[i].ToString()))
        //                {
        //                    this.chkedList.Items.Add(this.itemHash[mutex[i].ToString()], true);
        //                }

        //            }
        //        }
        //        //更新
        //        this.operMode = 1;
        //        this.TempCode = 1;
        //        this.chkSave.Text = "更新规则";
        //        return 1;
        //    }
        //    ClearFeeRule();

        //    //插入
        //    this.operMode = 2;
        //    this.TempCode = 2;
        //    this.chkSave.Text = "添加规则";
        //    this.btnDel.Visible = false;
        //    return 0;
        //    this.chkSave.Checked = false;
        //}

        ///// <summary>
        ///// 获取费用规则实体
        ///// </summary>
        ///// <returns></returns>
        //private HISFC.Object.Base.UndrugFeeRegular GetFeeregular(out int value)
        //{
        //    HISFC.Object.Base.UndrugFeeRegular feeRegular = new FS.HISFC.Models.Fee.Item.UndrugFeeRegular();
        //    value = 0;
        //    if (this.chkSave.Checked)
        //    {
        //        if (this.ValidFeeRule() > 0)
        //        {
        //            feeRegular.ID = this.ruleCode.Text.ToString();
        //            feeRegular.Item.ID = this.tbItemCode.Text.ToString();
        //            feeRegular.Item.Name = this.tbItemName.Text.ToString();
        //            if (this.rdoQuality.Checked)
        //            {
        //                feeRegular.LimitCondition = "0";
        //            }
        //            if (this.rdoTime.Checked)
        //            {
        //                feeRegular.LimitCondition = "1";
        //            }
        //            if (this.cmbRegular.SelectedItem != null)
        //            {
        //                feeRegular.Regular.ID = this.cmbRegular.Tag.ToString();
        //            }
        //            //if (this.rdoQuality.Checked)
        //            //{
        //            //    feeRegular.Regular.ID = "0";
        //            //}
        //            //if (this.rdoTime.Checked)
        //            //{
        //            //    feeRegular.Regular.ID = "1";
        //            //}

        //            feeRegular.DayLimit = NFC.Function.NConvert.ToDecimal(this.txtQuality.Text.ToString());
        //            string limitCode = "";
        //            foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in this.chkedList.CheckedItems)
        //            {
        //                string tempStr = undrug.ID + "|";
        //                limitCode += tempStr;
        //            }
        //            feeRegular.LimitItem.ID = limitCode;

        //            feeRegular.Oper.ID = this.feeRegularManager.Operator.ID.ToString();
        //            feeRegular.Oper.OperTime = DateTime.Now;
        //        }
        //        else
        //        {
        //            value = -1;
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        value = 0;
        //        return null;
        //    }
        //    value = 1;
        //    return feeRegular;
        //}

        ///// <summary>
        ///// 有效性判断
        ///// </summary>
        ///// <returns></returns>
        //private int ValidFeeRule()
        //{
        //    if (this.cmbRegular.SelectedItem == null || this.cmbRegular.SelectedIndex < 0)
        //    {
        //        MessageBox.Show("您应用了费用规则，必须选择收费规则！");
        //        this.cmbRegular.Focus();
        //        return -1;
        //    }
        //    decimal tot;
        //    if (!decimal.TryParse(this.txtQuality.Text, out tot))
        //    {
        //        MessageBox.Show("您应用了费用规则，＂数量限额＂必须为数字！");
        //        return -1;
        //        this.txtQuality.SelectAll();
        //    }
        //    //char[] validChar = this.txtQuality.Text.ToCharArray();
        //    //for (int i = 0; i < validChar.Length; i++)
        //    //{
        //    //    if (validChar[i] < '0' || validChar[i] > '9')
        //    //    {
        //    //    }
        //    //    if (NFC.Function.NConvert.ToDecimal(validChar[i].ToString()) < 0 || NFC.Function.NConvert.ToDecimal(validChar[i].ToString()) > 9)
        //    //    {
        //    //        MessageBox.Show("您应用了费用规则，＂数量限额＂必须为数字！");
        //    //        return -1;
        //    //        this.txtQuality.SelectAll();
        //    //    }
        //    //}
        //    return 1;
        //}

        ///// <summary>
        ///// 清除内容
        ///// </summary>
        //private void ClearFeeRule()
        //{
        //    this.rdoQuality.Checked = true;
        //    this.lbDescription.Text = "";
        //    this.cmbRegular.Focus();
        //    this.txtQuality.Text = "0";
        //    this.chkedList.Items.Clear();
        //}

        //#endregion

        //#region 事件
        ///// <summary>
        ///// 收费规则选择事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void cmbRegular_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbRegular.SelectedItem != null && cmbRegular.SelectedIndex >= 0)
        //    {
        //        // this.lbDescription.Text = cmbRegular.SelectedItem.Memo.ToString();

        //        if (cmbRegular.SelectedItem.ID.ToString() == "04" || cmbRegular.SelectedItem.ID == "03")
        //        {
        //            this.cmbItem.Enabled = true;
        //            this.chkedList.Enabled = true;
        //        }
        //    }

        //}

        ///// <summary>
        ///// 添加互斥项目
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void cmbItem_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    bool InSelected = false;
        //    string selectedItemcode = this.cmbItem.SelectedItem.ID.ToString();
        //    if (this.cmbItem.SelectedItem != null && selectedItemcode != string.Empty)
        //    {
        //        for (int i = 0; i < this.chkedList.Items.Count; i++)
        //        {
        //            if (selectedItemcode.CompareTo(((FS.HISFC.Models.Fee.Item.Undrug)this.chkedList.CheckedItems[i]).ID.ToString()) == 0)
        //            {
        //                return;
        //            }
        //        }
        //        if (this.itemHash.Contains(selectedItemcode))
        //        {
        //            this.chkedList.Items.Add(this.itemHash[selectedItemcode], true);
        //        }
        //        int count = this.chkedList.Items.Count;
        //        this.chkedList.SelectedIndex = count - 1;
        //    }
        //    //if(this.cmbItem.SelectedItem !=null && this.cmbItem
        //}
        //#endregion
    }
}
