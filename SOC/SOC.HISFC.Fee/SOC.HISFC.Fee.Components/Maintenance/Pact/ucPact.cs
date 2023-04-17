using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    /// <summary>
    /// [功能描述: 合同单位信息维护控件]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
     partial class ucPact : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPact()
        {
            InitializeComponent();
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.chbIsStop.CheckedChanged += new EventHandler(chbIsStop_CheckedChanged);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            this.txtOtherName.KeyDown += new KeyEventHandler(txtOtherName_KeyDown);
        }

        public delegate int SaveItemHandler(FS.HISFC.Models.Base.PactInfo item);

        public event SaveItemHandler EndSave;

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.cmbSIDllType.AddItems(PactInfoClass.SIDllTypeEditor.SIDllHelper.ArrayObject);
            this.cmbSystemType.AddItems(PactInfoClass.SystemTypeEditor.SystemTypeHelper.ArrayObject);
            this.cmbPriceForm.AddItems(PactInfoClass.PriceFormTypeEditor.PriceFormHelper.ArrayObject);
            this.cmbPayKind.AddItems(PactInfoClass.PayKindTypeEditor.PayKindHelper.ArrayObject);
            this.cmbItemType.AddItems(PactInfoClass.ItemTypeEditor.ItemTypeHelper.ArrayObject);
            return 1;
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                        {
                            crl.Text = null;
                            crl.Tag = null;
                            continue;
                        }
                        if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                            continue;
                        }
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            ((FS.FrameWork.WinForms.Controls.NeuCheckBox)crl).Checked = false;
                        }
                    }
                }
            }

            this.txtItemCode.Text = "";

            return 1;
        }

        /// <summary>
        /// 根据传入的Item实体信息 设置控件显示
        /// </summary>
        public void SetItem(FS.HISFC.Models.Base.PactInfo item, bool isUsed)
        {
            this.txtItemCode.Text = item.ID;
            this.txtName.Text = item.Name;
            this.txtOtherName.Text = item.ShortName;
            this.txtSortID.Text = item.SortID.ToString();
            this.nTxtDayQuota.Text = item.DayQuota.ToString();
            this.nTxtMonthQuota.Text = item.MonthQuota.ToString();
            this.nTxtYearQuota.Text = item.YearQuota.ToString();
            this.nTxtOnceQuota.Text = item.OnceQuota.ToString();
            this.nTxtBedQuota.Text = item.BedQuota.ToString();
            this.nTxtAirConditionQuota.Text = item.AirConditionQuota.ToString();
            this.nTxtArrearageRate.Text = item.Rate.ArrearageRate.ToString();
            this.nTxtEcoRate.Text = item.Rate.RebateRate.ToString();
            this.nTxtOwnRate.Text = item.Rate.OwnRate.ToString();
            this.nTxtPayRate.Text = item.Rate.PayRate.ToString();
            this.nTxtPubRate.Text = item.Rate.PubRate.ToString();
            this.nTxtAirConditionQuota.Text = item.AirConditionQuota.ToString();
            this.cmbItemType.Tag = item.ItemType;
            this.cmbPayKind.Tag = item.PayKind.ID;
            this.cmbPriceForm.Tag = item.PriceForm;
            this.cmbSIDllType.Tag = item.PactDllName;
            this.cmbSystemType.Tag = item.PactSystemType;

            this.ckBabyShared.Checked = item.Rate.IsBabyShared;
            this.ckInControl.Checked = item.IsInControl;
            this.ckNeedMCard.Checked = item.IsNeedMCard;
            this.chbIsStop.Checked = !FS.FrameWork.Function.NConvert.ToBoolean(item.ValidState);

        }

        /// <summary>
        /// 有效性检测
        /// </summary>
        /// <returns></returns>
        private bool CheckValid()
        {
            if (this.txtItemCode.Text.Trim().Length==0)
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("编码不能为空!"), MessageBoxIcon.Error);
                this.txtItemCode.Focus();
                return false;
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("名称不能为空!"), MessageBoxIcon.Error);
                this.txtName.Focus();
                return false;
            }

            if (this.txtSpellCode.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("拼音码不能为空!"), MessageBoxIcon.Error);
                this.txtSpellCode.Focus();
                return false;
            }

            if (this.txtWbCode.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("五笔码不能为空!"), MessageBoxIcon.Error);
                this.txtWbCode.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取基本信息
        /// </summary>
        private FS.HISFC.Models.Base.PactInfo GetItem()
        {
            FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();
            pact.ValidState = FS.FrameWork.Function.NConvert.ToInt32(!this.chbIsStop.Checked).ToString();
            pact.IsNeedMCard = this.ckNeedMCard.Checked;
            pact.IsInControl = this.ckInControl.Checked;
            pact.Rate.IsBabyShared = this.ckBabyShared.Checked;
            if (this.cmbSystemType.Tag != null)
            {
                pact.PactSystemType = this.cmbSystemType.Tag.ToString();
            }
            if (this.cmbSIDllType.Tag != null)
            {
                pact.PactDllName = this.cmbSIDllType.Tag.ToString();
                pact.PactDllDescription = this.cmbSIDllType.Text;
            }
            if (this.cmbPriceForm.Tag != null)
            {
                pact.PriceForm = this.cmbPriceForm.Tag.ToString();
            }
            if (this.cmbPayKind.Tag != null)
            {
                pact.PayKind.ID = this.cmbPayKind.Tag.ToString();
                pact.PayKind.Name = this.cmbPayKind.Text;
            }
            if (this.cmbItemType.Tag != null)
            {
                pact.ItemType = this.cmbItemType.Tag.ToString();
            }
            pact.Name = this.txtName.Text;
            pact.SpellCode = this.txtSpellCode.Text;
            pact.WBCode = this.txtWbCode.Text;
            pact.ID = this.txtItemCode.Text;
            pact.ShortName = this.txtOtherName.Text;
            pact.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.txtSortID.Text);
            pact.Rate.PubRate = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtPubRate.Text);
            pact.Rate.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtPayRate.Text);
            pact.Rate.OwnRate = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtOwnRate.Text);
            pact.Rate.RebateRate = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtEcoRate.Text);
            pact.Rate.ArrearageRate = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtArrearageRate.Text);
            pact.DayQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtDayQuota.Text);
            pact.MonthQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtMonthQuota.Text);
            pact.YearQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtYearQuota.Text);
            pact.OnceQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtOnceQuota.Text);
            pact.BedQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtBedQuota.Text);
            pact.AirConditionQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.nTxtAirConditionQuota.Text);
            return pact;
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (this.CheckValid())
            {
                FS.HISFC.Models.Base.PactInfo item = this.GetItem();

                if (item != null)
                {
                    if (this.EndSave != null)
                    {
                        if (this.EndSave(item) < 0)
                        {
                            return;
                        }
                    }

                    if (this.continueCheckBox.Checked)
                    {
                        this.Clear();
                    }
                    else
                    {
                        if (this.FindForm() != null)
                        {
                            this.FindForm().Close();
                        }
                    }
                }
            }
        }

        #endregion

        #region 事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.FindForm() != null)
            {
                this.FindForm().Close();
            }
        }

        void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            FS.HISFC.Models.Base.Spell spell = Function.GetSpellCode(this.txtName.Text);
            this.txtSpellCode.Text = spell.SpellCode;
            this.txtWbCode.Text = spell.WBCode;
            if (this.txtSpellCode.Text.Length > 8)
            {
                this.txtSpellCode.Text = this.txtSpellCode.Text.Substring(0, 8);
            }
            if (this.txtWbCode.Text.Length > 8)
            {
                this.txtWbCode.Text = this.txtWbCode.Text.Substring(0, 8);
            }


        }

        void txtOtherName_KeyDown(object sender, KeyEventArgs e)
        {
            FS.HISFC.Models.Base.Spell spell = Function.GetSpellCode(this.txtOtherName.Text);
            this.txtOtherSpellCode.Text = spell.SpellCode;
            this.txtOtherWBCode.Text = spell.WBCode;
            if (this.txtOtherSpellCode.Text.Length > 8)
            {
                this.txtOtherSpellCode.Text = this.txtOtherSpellCode.Text.Substring(0, 8);
            }
            if (this.txtOtherWBCode.Text.Length > 8)
            {
                this.txtOtherWBCode.Text = this.txtOtherWBCode.Text.Substring(0, 8);
            }
        }

        void chbIsStop_CheckedChanged(object sender, EventArgs e)
        {
            this.txtStopReason.Visible = this.chbIsStop.Checked;
            this.lblStopReason.Visible = this.chbIsStop.Checked;
        }

        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.txtName.Focused)
                {
                    this.txtName_KeyDown(null, null);
                }

                SendKeys.Send("{TAB}");
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            {
                if (this.btnSave.Visible && this.btnSave.Enabled)
                    this.btnSave_Click(null, null);
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.C.GetHashCode())
            {
                if (this.btnCancel.Visible && this.btnCancel.Enabled)
                    this.btnCancel_Click(null, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                if (this.btnCancel.Visible && this.btnCancel.Enabled)
                    this.btnCancel_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
