using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// 住院患者床位等级维护
    /// </summary>
    public partial class ucPatientBedGrade : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientBedGrade()
        {
            InitializeComponent();
        }

        #region 变量

        #region 成员变量

        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = null;

        #endregion

        #region 业务层变量
        /// <summary>
        /// 综合管理业务层
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 综合费用业务层
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();
        #endregion

        #endregion

        #region 属性

        #endregion

        #region 方法

        private void Init()
        {
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            this.btnModifyPrice.Click += new EventHandler(btnModifyPrice_Click);
            this.cmbItemInfo.SelectedIndexChanged += new EventHandler(cmbItemInfo_SelectedIndexChanged);

            ArrayList itemInfoList = SOC.HISFC.BizProcess.Cache.Fee.GetValidItem();
            this.cmbItemInfo.AddItems(itemInfoList);
        }

        private void Clear()
        {
            this.patientInfo = null;
            this.txtPatientNo.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.cmbBedNo.Text = string.Empty;
            this.cmbBedLevl.Text = string.Empty;
            this.lblBedLevlFee.Text = string.Empty;

            this.ClearFeeItemList();

            this.ClearFeeItem();
        }

        private void ClearFeeItemList()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        private void ClearFeeItem()
        {
            this.cmbItemInfo.Text = "";
            this.ntxtPrice.Text = "0";
            this.ntxtOldPrice.Text = "0";
            this.ntxtQty.Text = "0";
            this.ckbBabyRelation.Checked = false;
            this.ckbTimeRelation.Checked = true;
            this.cmbValidState.Text = "";
            this.ckExtFlag.Checked = false;
            this.ckOutFeeFlag.Checked = false;

            this.gbFeeItem.Tag = null;
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        private void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.patientInfo = patientInfo;
            this.txtPatientNo.Text = patientInfo.PID.PatientNO;
            this.txtName.Text = patientInfo.Name;
            this.cmbBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID.Replace(patientInfo.PVisit.PatientLocation.NurseCell.ID, "")+"床";

        }

        /// <summary>
        /// 显示床位等级信息
        /// </summary>
        /// <param name="bed"></param>
        private void SetBedLvlInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            Neusoft.HISFC.Models.Base.Bed bed=managerIntegrate.GetBed(patientInfo.PVisit.PatientLocation.Bed.ID);
            this.cmbBedLevl.Text = bed.BedGrade.Name;
            this.ClearFeeItemList();
            ArrayList alFeeItem=feeIntegrate.QueryBedFeeItemByMinFeeCode(bed.BedGrade.ID);
            decimal sumPrice = 0.00M;
            if (alFeeItem != null)
            {
                for (int i = 0; i < alFeeItem.Count; i++)
                {
                    Neusoft.HISFC.Models.Fee.BedFeeItem bedFeeItem = alFeeItem[i] as Neusoft.HISFC.Models.Fee.BedFeeItem;
                    Neusoft.HISFC.Models.Fee.Item.Undrug item = this.feeIntegrate.GetUndrugByCode(bedFeeItem.ID);

                    decimal price=0.00M;
                    decimal oldPrice=0.00M;
                    this.feeIntegrate.GetPriceForInpatient(patientInfo, item, ref price, ref oldPrice);

                    //查找个人价格
                    Neusoft.HISFC.Models.Fee.BedFeeItem personFeeItem = this.feeIntegrate.QueryBedFeeItemForPatient(patientInfo.ID, bed.ID, bedFeeItem.PrimaryKey);

                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey)==false)
                    {
                        bedFeeItem = personFeeItem;
                        bedFeeItem.OldPrice = price;
                    }
                    else
                    {
                        if (item.ValidState != ((int)Neusoft.HISFC.Models.Base.EnumValidState.Valid).ToString())
                        {
                            bedFeeItem.ValidState = Neusoft.HISFC.Models.Base.EnumValidState.Invalid;
                        }
                        bedFeeItem.OldPrice = price;
                        bedFeeItem.Price = price;
                    }

                    bedFeeItem.PatientID = patientInfo.ID;

                    if (bedFeeItem.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        sumPrice += bedFeeItem.Price * bedFeeItem.Qty;
                    }

                    SetFeeItemListInfo(bedFeeItem);
                }

                this.lblBedLevlFee.Text = "床位价钱：" + sumPrice;
            }
        }

        /// <summary>
        /// 添充床位等级所包含内容
        /// </summary>
        /// <param name="feeCodeStat"></param>
        /// <param name="row"></param>
        private void SetFeeItemListInfo(Neusoft.HISFC.Models.Fee.BedFeeItem bedFeeItem)
        {
            this.neuSpread1_Sheet1.RowCount++;
            int row = this.neuSpread1_Sheet1.RowCount - 1;

            this.neuSpread1_Sheet1.SetValue(row, 0, bedFeeItem.PrimaryKey);
            this.neuSpread1_Sheet1.SetValue(row, 1, bedFeeItem.ID);
            this.neuSpread1_Sheet1.SetValue(row, 2, bedFeeItem.Name);
            this.neuSpread1_Sheet1.SetValue(row, 3, bedFeeItem.Qty);
            this.neuSpread1_Sheet1.SetValue(row, 4, bedFeeItem.Price.ToString());
            this.neuSpread1_Sheet1.SetValue(row, 5, bedFeeItem.BeginTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : bedFeeItem.BeginTime.Date.ToString("yyyy-MM-dd"));
            this.neuSpread1_Sheet1.SetValue(row, 6, bedFeeItem.EndTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : bedFeeItem.EndTime.Date.ToString("yyyy-MM-dd"));
            this.neuSpread1_Sheet1.SetValue(row, 7, bedFeeItem.IsBabyRelation);
            this.neuSpread1_Sheet1.SetValue(row, 8, bedFeeItem.IsTimeRelation);
            this.neuSpread1_Sheet1.SetValue(row, 9, Neusoft.FrameWork.Function.NConvert.ToBoolean(bedFeeItem.ExtendFlag));
            this.neuSpread1_Sheet1.SetValue(row, 10, bedFeeItem.IsOutFeeFlag);
            this.neuSpread1_Sheet1.SetValue(row, 11, GetValidName(bedFeeItem.ValidState));
            this.neuSpread1_Sheet1.SetValue(row, 12, bedFeeItem.SortID);
            this.neuSpread1_Sheet1.Rows[row].Tag = bedFeeItem;
        }

        /// <summary>
        /// 显示固定费用信息
        /// </summary>
        /// <param name="bedFeeItem"></param>
        private void SetFeeItemInfo(Neusoft.HISFC.Models.Fee.BedFeeItem bedFeeItem)
        {
            this.cmbItemInfo.Text = bedFeeItem.Name;
            this.ntxtPrice.Text = bedFeeItem.Price.ToString();
            this.ntxtOldPrice.Text = bedFeeItem.OldPrice.ToString();
            this.ntxtQty.Text = bedFeeItem.Qty.ToString();
            if (bedFeeItem.IsTimeRelation == true)
            {
                this.dtBegin.Checked = true;
                this.dtBegin.Text = bedFeeItem.BeginTime.ToString();
            }
            else
            {
                this.dtBegin.Checked = false;
            }
            if (bedFeeItem.IsTimeRelation == true)
            {
                this.dtEnd.Checked = true;
                this.dtEnd.Text = bedFeeItem.EndTime.ToString();
            }
            else
            {
                this.dtEnd.Checked = false;
            }
            this.ckbBabyRelation.Checked = bedFeeItem.IsBabyRelation;
            this.ckbTimeRelation.Checked = bedFeeItem.IsTimeRelation;
            this.cmbValidState.Text = this.GetValidName(bedFeeItem.ValidState);
            this.ckExtFlag.Checked = Neusoft.FrameWork.Function.NConvert.ToBoolean(bedFeeItem.ExtendFlag);
            this.ckOutFeeFlag.Checked = bedFeeItem.IsOutFeeFlag;

            this.gbFeeItem.Tag = bedFeeItem.Clone();
        }

        /// <summary>
        /// 通过有效性ID得到名称
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        private string GetValidName(Neusoft.HISFC.Models.Base.EnumValidState strID)
        {
            switch (strID)
            {
                case Neusoft.HISFC.Models.Base.EnumValidState.Valid:
                    return "在用";
                case Neusoft.HISFC.Models.Base.EnumValidState.Invalid:
                    return "停用";
                case Neusoft.HISFC.Models.Base.EnumValidState.Ignore:
                    return "废弃";
                default:
                    return "在用";
            }
        }

        /// <summary>
        /// 修改价钱
        /// </summary>
        /// <returns></returns>
        private int ModifyPrice()
        {
            Neusoft.HISFC.Models.Fee.BedFeeItem bedFeeItem = this.gbFeeItem.Tag as Neusoft.HISFC.Models.Fee.BedFeeItem;
            
            if (bedFeeItem != null&&this.patientInfo!=null)
            {
                bedFeeItem.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.ntxtPrice.Text);
                bedFeeItem.BedNO = this.patientInfo.PVisit.PatientLocation.Bed.ID;
                bedFeeItem.PatientID = this.patientInfo.ID;
                bedFeeItem.Qty = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.ntxtQty.Text);
                if (this.cmbItemInfo.Tag == null || string.IsNullOrEmpty(this.cmbItemInfo.Tag.ToString()))
                {
                    MessageBox.Show(this, string.Format("选择的项目为空", bedFeeItem.Name), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                bedFeeItem.ID = this.cmbItemInfo.Tag.ToString();
                bedFeeItem.Name = SOC.HISFC.BizProcess.Cache.Fee.GetItem(bedFeeItem.ID).Name;

                if (bedFeeItem.Price <= 0)
                {
                    MessageBox.Show(this, string.Format("项目【{0}】价格小于零", bedFeeItem.Name), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

                if (bedFeeItem.Qty <= 0)
                {
                    MessageBox.Show(this, string.Format("项目【{0}】数量小于零", bedFeeItem.Name), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

                if (MessageBox.Show(this, string.Format("确认将床位费的项目修改为【{0}】,数量【{1}】,价格【{2}】？",bedFeeItem.Name,bedFeeItem.Qty,bedFeeItem.Price), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                    feeIntegrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

                    if (feeIntegrate.SaveBedFeeItemForPatient(bedFeeItem) < 0)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this, string.Format("保存项目【{0}】的价格失败，{1}", bedFeeItem.Name, feeIntegrate.Err), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                    Neusoft.FrameWork.Management.PublicTrans.Commit();

                }
            }

            return 1;
        }

        #region 事件代理
        /// <summary>
        /// 患者信息
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (e.Tag is Neusoft.HISFC.Models.RADT.PatientInfo)
            {
                Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = e.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;
                this.Clear();
                this.SetPatientInfo(patientInfo);
                this.SetBedLvlInfo(patientInfo);
            }
            return base.OnSetValue(neuObject, e);
        }

        void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
            {
                this.ClearFeeItem();
                Neusoft.HISFC.Models.Fee.BedFeeItem bedFeeItem = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as Neusoft.HISFC.Models.Fee.BedFeeItem;
                this.SetFeeItemInfo(bedFeeItem);
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        void btnModifyPrice_Click(object sender, EventArgs e)
        {
            if (this.ModifyPrice() > 0)
            {
                this.SetBedLvlInfo(this.patientInfo);
            }
        }

        void cmbItemInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbItemInfo.Tag != null && string.IsNullOrEmpty(this.cmbItemInfo.Tag.ToString()) == false)
            {
                Neusoft.HISFC.Models.Fee.Item.Undrug undrug=this.feeIntegrate.GetItem(this.cmbItemInfo.Tag.ToString());
                decimal feePrice=undrug.Price;
                decimal orgPrice=undrug.Price;
                this.feeIntegrate.GetPriceForInpatient(patientInfo,undrug,ref feePrice,ref orgPrice);
                this.ntxtOldPrice.Text = orgPrice.ToString();
                this.ntxtPrice.Text = feePrice.ToString();
            }
        }

        #endregion
        #endregion
    }
}
