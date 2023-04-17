using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    public partial class frmSetRetailPrice : Form
    {
        public frmSetRetailPrice()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmSetRetailPrice_Load);
        }

        FS.SOC.HISFC.BizLogic.Pharmacy.Adjust adjustMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Adjust();

        private int ShowFormula()
        {
            ArrayList alFormula = adjustMgr.QueryAdjustPriceFormula();
            if(alFormula==null)
            {
                Function.ShowMessage("获取公式发生错误，请向系统管理员联系并报错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            this.sheetView3.RowCount = 0;
            this.sheetView3.RowCount = alFormula.Count;
            int rowIndex = 0;
            foreach (FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula formula in alFormula)
            {
                this.sheetView3.Cells[rowIndex, 0].Value = formula.DrugType.ID;
                this.sheetView3.Cells[rowIndex, 1].Value = formula.DrugType.Name;
                if (formula.PriceType == "0")
                {
                    formula.PriceType = "购入价";
                }
                else if (formula.PriceType == "1")
                {
                    formula.PriceType = "批发价";
                }
                else if (formula.PriceType == "2")
                {
                    formula.PriceType = "零售价";
                }
                else
                {
                    formula.PriceType = "购入价";
                }
                this.sheetView3.Cells[rowIndex, 2].Value = formula.PriceType;

                this.sheetView3.Cells[rowIndex, 3].Value = formula.PriceLower;
                this.sheetView3.Cells[rowIndex, 4].Value = formula.PriceUpper;
                this.sheetView3.Cells[rowIndex, 5].Value = formula.Name;
                if (formula.FomulaType == "0")
                {
                    this.sheetView3.Cells[rowIndex, 6].Text = "非基药";
                }
                else if (formula.FomulaType == "1")
                {
                    this.sheetView3.Cells[rowIndex, 6].Text = "基药";
                }
                else if (formula.FomulaType == "2")
                {
                    this.sheetView3.Cells[rowIndex, 6].Text = "疫苗";
                }
                this.sheetView3.Cells[rowIndex, 7].Value = FS.FrameWork.Function.NConvert.ToBoolean(formula.ValidState);
                this.sheetView3.Rows[rowIndex].Tag = formula;
                rowIndex++;
            }
            return 1;
        }

        private int AddFormula()
        {
            this.sheetView3.Rows.Add(this.sheetView3.Rows.Count, 1);
            return 1;
        }

        private int DeleteFormula()
        {
            if (this.sheetView3.Rows[this.sheetView3.ActiveRowIndex].Tag is FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula)
            {
                DialogResult rs = MessageBox.Show("确认删除该条记录吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                {
                    return 0;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                int param =this.adjustMgr.DeleteAdjustPriceFormula(((FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula)this.sheetView3.Rows[this.sheetView3.ActiveRowIndex].Tag).ID);

                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("删除失败，请向系统管理员联系并报错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            this.sheetView3.Rows.Remove(this.sheetView3.ActiveRowIndex, 1);
            return 1;
        }

        private FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula GetFormula(int rowIndex)
        {
            FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula formula = null;
            if (this.sheetView3.Rows[rowIndex].Tag is FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula)
            {
                formula = (FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula)this.sheetView3.Rows[rowIndex].Tag;
            }
            else
            {
                formula = new FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula();
            }
            formula.ID = rowIndex.ToString();
            formula.DrugType.ID = this.sheetView3.Cells[rowIndex, 0].Value.ToString();
            formula.DrugType.Name = this.sheetView3.Cells[rowIndex, 1].Value.ToString();
            formula.PriceType = this.sheetView3.Cells[rowIndex, 2].Value.ToString();
            if (formula.PriceType == "购入价")
            {
                formula.PriceType = "0";
            }
            else if (formula.PriceType == "批发价")
            {
                formula.PriceType = "1";
            }
            else if (formula.PriceType == "零售价")
            {
                formula.PriceType = "2";
            }
            else 
            {
                formula.PriceType = "0";
            }
            formula.PriceLower = FS.FrameWork.Function.NConvert.ToDecimal(this.sheetView3.Cells[rowIndex, 3].Value);
            formula.PriceUpper = FS.FrameWork.Function.NConvert.ToDecimal(this.sheetView3.Cells[rowIndex, 4].Value);
            formula.Name = this.sheetView3.Cells[rowIndex, 5].Value.ToString();
            if (this.sheetView3.Cells[rowIndex, 6].Text == "基药")
            {
                formula.FomulaType = "1";
            }
            else if (this.sheetView3.Cells[rowIndex, 6].Text == "非基药")
            {
                formula.FomulaType = "0";
            }
            else if (this.sheetView3.Cells[rowIndex, 6].Text == "疫苗")
            {
                formula.FomulaType = "2";
            }
            if (FS.FrameWork.Function.NConvert.ToBoolean(this.sheetView3.Cells[rowIndex, 7].Value))
            {
                formula.ValidState = "1";
            }
            else
            {
                formula.ValidState = "0";
            }
            formula.OperID = this.adjustMgr.Operator.ID;
            formula.OperTime = DateTime.Now;

            return formula;
        }

        private int SaveFormula()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            for (int rowIndex = 0; rowIndex < this.sheetView3.Rows.Count; rowIndex++)
            {
           
                FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula formula = this.GetFormula(rowIndex);
                int param = 0;
                if (!string.IsNullOrEmpty(formula.ID))
                {
                    param = this.adjustMgr.DeleteAdjustPriceFormula(formula.ID);

                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存时删除原有公式失败，请向系统管理员联系并报错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                param = this.adjustMgr.InsertAdjustPriceFormula(formula);

                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败，请向系统管理员联系并报错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            Function.ShowMessage("保存成功！", MessageBoxIcon.Information);

            return 1;
        }


        private int ShowSpeFormula()
        {
            ArrayList alFormula = adjustMgr.QueryAdjustPriceSpeFormula();
            if (alFormula == null)
            {
                Function.ShowMessage("获取公式发生错误，请向系统管理员联系并报错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            this.sheetView4.RowCount = 0;
            this.sheetView4.RowCount = alFormula.Count;
            int rowIndex = 0;
            foreach (FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula formula in alFormula)
            {
                this.sheetView4.Cells[rowIndex, 0].Value = formula.DrugNO;
                this.sheetView4.Cells[rowIndex, 1].Value = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(formula.DrugNO);

                this.sheetView4.Cells[rowIndex, 2].Value = formula.DrugName + "[" + formula.DrugSpecs + "]"; ;

                this.sheetView4.Cells[rowIndex, 3].Value = formula.Formula;
                this.sheetView4.Cells[rowIndex, 4].Value = FS.FrameWork.Function.NConvert.ToBoolean(formula.ValidState);
                this.sheetView4.Rows[rowIndex].Tag = formula;
                rowIndex++;
            }
            return 1;
        }

        private int AddSpeFormula()
        {
           //弹出窗口选择药品
            FS.FrameWork.Models.NeuObject selectObj = new FS.FrameWork.Models.NeuObject();
            string[] fpLabel = { "药品编码", "名称", };
            int[] fpWidth = { 90, 120 };
            bool[] fpVisible = { true, true, true, true, true, true };
            if (SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper == null)
            {
                SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem("-");
            }
            ArrayList alItem=new ArrayList();
            foreach(FS.HISFC.Models.Pharmacy.Item item in SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper.ArrayObject)
            {
                FS.HISFC.Models.Base.Spell simpleItem = new FS.HISFC.Models.Base.Spell();
                simpleItem.ID = item.ID;
                simpleItem.Name = item.Name;
                simpleItem.Memo = item.Specs;
                simpleItem.WBCode = item.WBCode;
                simpleItem.SpellCode = item.SpellCode;
                simpleItem.UserCode = item.UserCode;

                alItem.Add(simpleItem);
            }
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alItem, fpLabel, fpVisible, fpWidth, ref selectObj) == 1)
            {
                int rowIndex=-1;
                int colIndex=-1;
                this.fpSpread4.Search(0, selectObj.ID, true, true, true, true, 0, 0, ref rowIndex, ref colIndex);
                if (rowIndex == -1 && colIndex == -1)
                { 
                    this.sheetView4.Rows.Add(this.sheetView4.Rows.Count, 1);
                    rowIndex = this.sheetView4.Rows.Count - 1;
                }
                this.sheetView4.Cells[rowIndex, 0].Value = selectObj.ID;
                this.sheetView4.Cells[rowIndex, 1].Value = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(selectObj.ID);
                this.sheetView4.Cells[rowIndex, 2].Value = selectObj.Name + "[" + selectObj.Memo + "]";
                this.sheetView4.Cells[rowIndex, 3].Value = "";
               
                this.sheetView4.ActiveRowIndex = rowIndex;
                this.fpSpread4.SetViewportTopRow(0, rowIndex);
            
            }

            return 1;
        }

        private int DeleteSpeFormula()
        {
            if (this.sheetView4.Rows[this.sheetView4.ActiveRowIndex].Tag is FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula)
            {
                DialogResult rs = MessageBox.Show("确认删除该条记录吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                {
                    return 0;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                int param = this.adjustMgr.DeleteAdjustPriceSpeFormula(((FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula)this.sheetView4.Rows[this.sheetView4.ActiveRowIndex].Tag).ID);

                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("删除失败，请向系统管理员联系并报错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            this.sheetView4.Rows.Remove(this.sheetView4.ActiveRowIndex, 1);
            return 1;
        }

        private FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula GetSpeFormula(int rowIndex)
        {
            bool isChanged = false;
            FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula formula = null;
            if (this.sheetView4.Rows[rowIndex].Tag is FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula)
            {
                formula = ((FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula)this.sheetView4.Rows[rowIndex].Tag).Clone();
            }
            else
            {
                isChanged = true;
                formula = new FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula();
            }
            formula.DrugNO = this.sheetView4.Cells[rowIndex, 0].Text;

            if (!isChanged && formula.ID != formula.DrugNO)
            {
                isChanged = true;
            }

            if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(formula.DrugNO) != null && SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(formula.DrugNO).ID != "")
            {
                formula.DrugName = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(formula.DrugNO).Name;
                formula.DrugSpecs = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(formula.DrugNO).Specs;
            }

            if (!isChanged && formula.Formula != this.sheetView4.Cells[rowIndex, 3].Text)
            {
                isChanged = true;
            }

            formula.Formula = this.sheetView4.Cells[rowIndex, 3].Text;

            if (!isChanged && FS.FrameWork.Function.NConvert.ToBoolean(formula.ValidState) != FS.FrameWork.Function.NConvert.ToBoolean(this.sheetView4.Cells[rowIndex, 4].Value))
            {
                isChanged = true;                
            }
            if (FS.FrameWork.Function.NConvert.ToBoolean(this.sheetView4.Cells[rowIndex, 4].Value))
            {
                formula.ValidState = "1";
            }
            else
            {
                formula.ValidState = "0";
            }
            if (!isChanged)
            {
                return null;
            }
            formula.OperID = this.adjustMgr.Operator.ID;
            formula.OperTime = DateTime.Now;

            return formula;
        }

        private int SaveSpeFormula()
        {
            for (int rowIndex = 0; rowIndex < this.sheetView4.Rows.Count; rowIndex++)
            {

                FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula formula = this.GetSpeFormula(rowIndex);
                if (formula == null)
                {
                    continue;
                }
                int param = 0;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                if (!string.IsNullOrEmpty(formula.ID))
                {
                    param = this.adjustMgr.DeleteAdjustPriceSpeFormula(formula.ID);

                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存时删除原有公式失败，请向系统管理员联系并报错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                }
                param = this.adjustMgr.InsertAdjustPriceSpeFormula(formula);

                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存失败，请向系统管理员联系并报错误：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                formula.ID = formula.DrugNO;
                this.sheetView4.Rows[rowIndex].Tag = formula;
            }

            Function.ShowMessage("保存成功！", MessageBoxIcon.Information);

            return 1;
        }

        void frmSetRetailPrice_Load(object sender, EventArgs e)
        {
            this.ShowFormula();
            this.ShowSpeFormula();

            this.nbtAdd.Click += new EventHandler(nbtAdd_Click);
            this.nbtDelete.Click += new EventHandler(nbtDelete_Click);
            this.nbtSave.Click += new EventHandler(nbtSave_Click);
            this.nbtReturn.Click += new EventHandler(nbtReturn_Click);
        }

        void nbtReturn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }

        void nbtSave_Click(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tpTypeLimit)
            {
                this.SaveFormula();
            }
            else
            {
                this.SaveSpeFormula();
            }
        }

        void nbtDelete_Click(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tpTypeLimit)
            {
                this.DeleteFormula();
            }
            else
            {
                this.DeleteSpeFormula();
            }
        }

        void nbtAdd_Click(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tpTypeLimit)
            {
                this.AddFormula();
            }
            else
            {
                this.AddSpeFormula();
            }
        }
    }
}
