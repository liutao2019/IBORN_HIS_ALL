using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientGuide
{
    public partial class ucFeeDetailGuide : UserControl
    {
        public ucFeeDetailGuide()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 医保接口业务层(本地)
        /// </summary>
        FS.HISFC.BizLogic.Fee.Interface interfaceManager = new FS.HISFC.BizLogic.Fee.Interface();
        FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();
        private void InitHeader()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.SheetCornerHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.SheetCornerVerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// 添加空白
        /// </summary>
        private void AddSpace()
        {
            //空白
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
        }

        /// <summary>
        /// 添加医院名称
        /// </summary>
        private void AddHospitalInfo()
        {
            //医院名称
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 9, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "中山大学附属第六医院门诊收费清单";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }

        /// <summary>
        /// 添加姓名
        /// </summary>
        private void AddName(string name,DateTime feeDate)
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount-1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount-1, 0].Font = new Font("宋体", 9, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "姓名:" + name;

            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount -1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = " 收费日期:" + feeDate.ToShortDateString();
        }

        /// <summary>
        /// 添加姓名和年龄
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="feeDate"></param>
        private void AddNameAndAge(string name, string sexName,string age, DateTime feeDate)
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 9, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "姓名:" + name + " " + " " + sexName + age;

            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text =
                " 收费日期:" + feeDate.ToShortDateString();

        }

        /// <summary>
        /// 添加发票号和病历号
        /// </summary>
        private decimal AddCardNOAndInvoice(string cardNO, ArrayList alInvoice)
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 9, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "病历号:" + cardNO;
            decimal TotCost = 0M;
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance balance in alInvoice)
            {
                TotCost += balance.FT.TotCost;
                if (balance.Invoice.Memo == "5")
                {
                    continue;
                }

                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = " 发票号:" + balance.Invoice.ID;
            }

            return TotCost;
        }
        
        /// <summary>
        /// 添加诊断
        /// </summary>
        /// <param name="clinicCode"></param>
        private void AddDiagnoseInfo(string clinicCode,string diagnoseName)
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "诊断:"+diagnoseName;

        }

        /// <summary>
        /// 添加费用表头
        /// </summary>
        private void AddFeeHeader()
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "项目";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "数量";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "单价";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = "单位";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "金额";
        }

       
        ///// <summary>
        ///// 根据处方号和复合项ID检查是否填充到farpoint中去打印
        ///// </summary>
        ///// <param name="recipeNo">处方号</param>
        ///// <param name="packageCode">复合项ID</param>
        ///// <returns>true则代表重复，不应添加</returns>
        //private bool CheckFillOrNot(string recipeNo, string packageCode, ArrayList al)
        //{
        //    bool res = false;
        //    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in al)
        //    {
        //        if (item.RecipeNO == recipeNo && item.UndrugComb.ID == packageCode)
        //        {
        //            res = true;
        //        }
        //    }
        //    return res;
        //}

        /// <summary>
        /// 添加费用明细
        /// </summary>
        /// <param name="alFeeDetail"></param>
        private void AddFeeDetail(ArrayList alFeeDetail)
        {
            ArrayList alFilled = new ArrayList();           
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alFeeDetail)
            {

                //if (alFilled.Count > 0 && CheckFillOrNot(feeItemList.RecipeNO, feeItemList.UndrugComb.ID, alFilled))
                //{
                //    continue;
                //}
                //alFilled.Add(feeItemList);              
                //项目名称
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 5;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = feeItemList.Item.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 9);

                //项目明细（甲乙类，数量，单位，单价，金额）
                this.neuSpread1_Sheet1.RowCount++;

                //医保信息
                try
                {
                    //this.interfaceManager.GetCompareSingleItem("2", f.Item.ID, ref f.Compare);
                    this.interfaceManager.GetCompareSingleItem("2", feeItemList.Item.ID, ref feeItemList.Compare);
                }
                catch { }


                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = this.GetItemGrade(feeItemList);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Font = new Font("宋体", 9);

                if (feeItemList.Item.ChildPrice > feeItemList.Item.Price)
                {
                    feeItemList.Item.Price = feeItemList.Item.ChildPrice;
                }

                if (feeItemList.Item.PackQty <= 0)
                {
                    feeItemList.Item.PackQty = 1;
                }
                if (feeItemList.FeePack.Equals("1"))//包装单位
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = feeItemList.Item.Price.ToString("F4");
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = (feeItemList.Item.Price * decimal.Parse(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text)).ToString("F2");
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = feeItemList.Item.Qty.ToString("F2");
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F4");
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty * decimal.Parse(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text)).ToString("F2");
                }
                
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = feeItemList.Item.PriceUnit;
               
                


            }
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 8);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "备注：以上医保等级为广州医保中心甲乙类等级";

        }

        /// <summary>
        /// 添加尾注
        /// </summary>
        /// <param name="TotCost"></param>
        /// <param name="dtNow"></param>
        private void AddFeeFooter(decimal TotCost,DateTime dtNow)
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount-1, 0, 1, 5);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "收款员: " +FS.FrameWork.Management.Connection.Operator.ID + " 合计: " + TotCost.ToString();
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount-1, 0, 1, 5);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "打印日期: " + dtNow.ToShortDateString();
        }

        /// <summary>
        /// 添加撕纸线
        /// </summary>
        private void AddLine()
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "---------撕 纸 线----------";
        }

        /// <summary>
        /// 添加温馨提示
        /// </summary>
        private void AddHospitalTipForUL()
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount-1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount-1, 0].Font = new Font("宋体", 8);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount-1, 0].Text = "广州医学院第四附属医院门诊检验项目温馨提示单";
        }

        /// <summary>
        /// 添加温馨提示
        /// </summary>
        private void AddHospitalTipForUC()
        {
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = new Font("宋体", 8);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "广州医学院第四附属医院门诊检查项目温馨提示单";
        }

        /// <summary>
        /// 添加分组项目
        /// </summary>
        private ArrayList GetLisGroupItem(ArrayList alFeeDetail)
        {
            //分组显示
            ArrayList ulList = new ArrayList();
            int count=alFeeDetail.Count;
            for (int i = 0; i < count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = alFeeDetail[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                //检验分组
                if (feeItemList.Item.SysClass.ID.ToString().Equals(FS.HISFC.Models.Base.EnumSysClass.UL.ToString()))
                {
                    ulList.Add(feeItemList);
                }
            }

            for (int i = 0; i < ulList.Count; i++)
            {
                alFeeDetail.Remove(ulList[i]);
            }

            ArrayList sameNotes = new ArrayList();
            ArrayList ReturnList = new ArrayList();
            while (ulList.Count > 0)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = ulList[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                sameNotes.Add(feeItemList);
                for (int j = 1; j < ulList.Count; j++)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemListCompare = ulList[j] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    if (!feeItemListCompare.Item.SysClass.ID.ToString().Equals(FS.HISFC.Models.Base.EnumSysClass.UL.ToString()))
                    {
                        continue;
                    }
                    //执行科室一致，样本一致分到一组
                    if (feeItemList.ExecOper.Dept.ID.Equals(feeItemListCompare.ExecOper.Dept.ID) && feeItemList.Order.Sample.Name.Equals(feeItemListCompare.Order.Sample.Name))
                    {
                        sameNotes.Add(feeItemListCompare);
                    }

                }
                ReturnList.Add(sameNotes);
                for (int i = 0; i < sameNotes.Count; i++)
                {
                    ulList.Remove(sameNotes[i]);
                }
            }

            return ReturnList;

        }

        /// <summary>
        /// 添加检验分组项目
        /// </summary>
        /// <param name="alFeeDetail"></param>
        private void AddLisGroupItem(ArrayList alFeeDetail)
        {
            if (alFeeDetail.Count == 0)
            {
                return;
            }
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = alFeeDetail[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = feeItemList.ExecOper.Dept.Name;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "样本:";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = feeItemList.Order.Sample.Name;

            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = " 项目";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "数量";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = "单价";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "金额";

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeDetail)
            {
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 5;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = feeItem.Item.Name;

                this.neuSpread1_Sheet1.RowCount++;


                //医保信息
                try
                {
                    //this.interfaceManager.GetCompareSingleItem("2", f.Item.ID, ref f.Compare);
                    this.interfaceManager.GetCompareSingleItem("2", feeItem.Item.ID, ref feeItem.Compare);
                }
                catch { }

                //甲乙类
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = this.GetItemGrade(feeItem);
                if (feeItem.FeePack.Equals("1"))//包装单位
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = (feeItem.Item.Qty / feeItem.Item.PackQty).ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = feeItem.Item.Price.ToString("F2");
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = feeItem.Item.Qty.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (feeItem.Item.Price / feeItem.Item.PackQty).ToString("F2");
                }
                //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = (feeItem.FT.PubCost+feeItem.FT.PayCost+feeItem.FT.OwnCost).ToString("F2");
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = ((feeItem.Item.Price / feeItem.Item.PackQty) * feeItem.Item.Qty).ToString("F2");
            }

        }

        /// <summary>
        /// 添加检验分组项目
        /// </summary>
        /// <param name="alFeeDetail"></param>
        private void AddConfrimGroupItem(ArrayList alFeeDetail)
        {
            if (alFeeDetail.Count == 0)
            {
                return;
            }
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = alFeeDetail[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = feeItemList.ExecOper.Dept.Name;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "部位:";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = feeItemList.Order.CheckPartRecord;

            this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "  项目名称";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "数量";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = "单价";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "金额";

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeDetail)
            {
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 5;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = feeItem.Item.Name;

                this.neuSpread1_Sheet1.RowCount++;

                //医保信息
                try
                {
                    //this.interfaceManager.GetCompareSingleItem("2", f.Item.ID, ref f.Compare);
                    this.interfaceManager.GetCompareSingleItem("2", feeItem.Item.ID, ref feeItem.Compare);
                }
                catch { }

                //甲乙类
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = this.GetItemGrade(feeItem);
                if (feeItem.FeePack.Equals("1"))//包装单位
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = (feeItem.Item.Qty / feeItem.Item.PackQty).ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = feeItem.Item.Price.ToString("F2");
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = feeItem.Item.Qty.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (feeItem.Item.Price / feeItem.Item.PackQty).ToString("F2");
                }
                //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = (feeItem.FT.PubCost + feeItem.FT.PayCost + feeItem.FT.OwnCost).ToString("F2");
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = ((feeItem.Item.Price / feeItem.Item.PackQty) * feeItem.Item.Qty).ToString("F2");
            }

        }

        /// <summary>
        /// 获取终端确认信息
        /// </summary>
        /// <param name="alFeeDetail"></param>
        /// <returns></returns>
        private ArrayList GetConfirmGroupItem(ArrayList alFeeDetail)
        {
            //分组显示
            ArrayList ulList = new ArrayList();
            int count = alFeeDetail.Count;
            for (int i = 0; i < count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = alFeeDetail[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                //检验分组
                if (feeItemList.Item.IsNeedConfirm)
                {
                    if (feeItemList.Order.CheckPartRecord == null)
                    {
                        feeItemList.Order.CheckPartRecord = string.Empty;
                    }
                    ulList.Add(feeItemList);
                }
            }

            for (int i = 0; i < ulList.Count; i++)
            {
                alFeeDetail.Remove(ulList[i]);
            }

            ArrayList sameNotes = new ArrayList();
            ArrayList ReturnList = new ArrayList();
            while (ulList.Count > 0)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = ulList[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                sameNotes.Add(feeItemList);
                for (int j = 1; j < ulList.Count; j++)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemListCompare = ulList[j] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    if (!feeItemListCompare.Item.IsNeedConfirm)
                    {
                        continue;
                    }
                    //执行科室一致，样本一致分到一组
                    if (feeItemList.ExecOper.Dept.ID.Equals(feeItemListCompare.ExecOper.Dept.ID) && feeItemList.Order.CheckPartRecord.Equals(feeItemListCompare.Order.CheckPartRecord))
                    {
                        sameNotes.Add(feeItemListCompare);
                    }

                }
                ReturnList.Add(sameNotes);
                for (int i = 0; i < sameNotes.Count; i++)
                {
                    ulList.Remove(sameNotes[i]);
                }
            }

            return ReturnList;
        }

        /// <summary>
        /// 添加第一部分
        /// </summary>
        /// <param name="register"></param>
        /// <param name="balanceList"></param>
        /// <param name="itemList"></param>
        private void AddList(FS.HISFC.Models.Registration.Register register, ArrayList balanceList, ArrayList itemList)
        {
            this.AddSpace();
           // this.AddSpace();
            this.AddHospitalInfo();
            //this.AddSpace();
           // this.AddSpace();
            //使用发票的收费日期
            if (balanceList.Count > 0)
            {
                this.AddName(register.Name, ((FS.HISFC.Models.Fee.Outpatient.Balance)balanceList[0]).BalanceOper.OperTime);
            }
            else
            {
                this.AddName(register.Name, DateTime.Now);
            }

            decimal TotCost= this.AddCardNOAndInvoice(register.PID.CardNO, balanceList);
            this.AddFeeHeader();
            this.AddFeeDetail(itemList);
            this.AddFeeFooter(TotCost, DateTime.Now);
        }

        /// <summary>
        /// 添加第二部分
        /// </summary>
        /// <param name="register"></param>
        /// <param name="balanceList"></param>
        /// <param name="itemList"></param>
        private void AddGroupList(FS.HISFC.Models.Registration.Register register, ArrayList balanceList, ArrayList itemList)
        {
            //分组显示
            ArrayList alUL = this.GetLisGroupItem(itemList);
            ArrayList alDiagnoses = new ArrayList();
            alDiagnoses = (new FS.HISFC.BizLogic.HealthRecord.Diagnose()).QueryDiagnoseNoOps(register.ID);
            string diagnoseName = "";
            if (alDiagnoses != null)
            {
                foreach (FS.HISFC.Models.HealthRecord.Diagnose dg in alDiagnoses)
                {
                    if (dg.Memo == register.ID || dg.Is30Disease == "0")
                    {//把非本次挂号的诊断过滤掉，如果以后诊断很多，通过这种方式很慢的话，要重写一个业务层
                        continue;
                    }
                    else
                    {//把本次挂号的诊断连接起来放到lblDiagnose里
                        diagnoseName += dg.DiagInfo.ICD10.Name + " ";
                    }
                }
            }
            else
            {
                diagnoseName = "";
            }

            //进行显示
            if (alUL.Count > 0)
            {
                foreach (ArrayList feeDetail in alUL)
                {
                    this.AddSpace();
                    this.AddSpace();
                    this.AddLine();
                    this.AddSpace();
                    this.AddSpace();
                   // this.AddHospitalTipForUL();
                    this.AddNameAndAge(register.Name, register.Sex.Name, register.Age, DateTime.Now);
                    this.AddCardNOAndInvoice(register.PID.CardNO, balanceList);
                    this.AddDiagnoseInfo(register.ID, diagnoseName);
                    this.AddLisGroupItem(feeDetail);
                }
            }

            ArrayList alConfrim = this.GetConfirmGroupItem(itemList);
            //进行显示
            if (alConfrim.Count > 0)
            {
                foreach (ArrayList feeDetail in alConfrim)
                {
                    this.AddSpace();
                    this.AddSpace();

                    this.AddLine();

                    this.AddSpace();
                    this.AddSpace();
                    this.AddHospitalTipForUC();

                    this.AddNameAndAge(register.Name, register.Sex.Name, register.Age, DateTime.Now);
                    this.AddCardNOAndInvoice(register.PID.CardNO, balanceList);
                    this.AddDiagnoseInfo(register.ID, diagnoseName);
                    this.AddConfrimGroupItem(feeDetail);
                }
            }

        }

        /// <summary>
        /// 显示费用信息
        /// </summary>
        /// <param name="itemList"></param>
        public void SetValue(FS.HISFC.Models.Registration.Register register,ArrayList balanceList, ArrayList itemList)
        {
            this.InitHeader();
            this.AddList(register, balanceList, itemList);
           // this.AddGroupList(register, balanceList, itemList);
        }

        /// <summary>
        /// 返回高度
        /// </summary>
        /// <returns></returns>
        public int GetHeigth() 
        {
            int height= FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.RowCount * this.neuSpread1_Sheet1.Rows.Default.Height) + 10;
            return this.Height > height ? this.Height+5 : height;
        }
        private string GetItemGrade(FS.HISFC.Models.Fee.Outpatient.FeeItemList obj)
        {
            string result = "自费";
            if (obj==null||obj.Compare == null || obj.Compare.CenterItem == null )
            {
                return result;
            }
            if (!string.IsNullOrEmpty(obj.UndrugComb.Memo) && obj.UndrugComb.Memo=="复合项")
            {
                result = "";
            }
            switch (obj.Compare.CenterItem.ItemGrade)
            {
                case "1":
                    result = "甲类";
                    break;
                case "2":
                    result = "乙类";
                    break;
                default:
                    break;

            }
            return result;
        }

        private string printerName = string.Empty;
        private System.Drawing.Printing.PaperSize GetPageSize()
        {
            System.Drawing.Printing.PaperSize pageSize = new System.Drawing.Printing.PaperSize("MZGuide", 250, 350);
            FS.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("MZGuide");
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("MZGuide", 250, 350);
            }
            pSize.Height = GetHeigth();

            pageSize.Width = pSize.Width;
            pageSize.Height = pSize.Height;
            printerName = pSize.Printer;
            return pageSize;
        }
        
        public void Print()
        {
                System.Drawing.Printing.PaperSize pageSize=GetPageSize();
                
                SOC.Windows.Forms.PrintExtendPaper p = new FS.SOC.Windows.Forms.PrintExtendPaper();
                p.PrinterName = printerName;
                //if (pageSize != null)
                //{
                //    p.SetPaperSize(pageSize);
                //}
                p.IsShowPageNOChooseDialog = false;
                p.DrawingMargins.Bottom = 30;
                p.IsAutoFillFarPoint = true;
                p.HeaderControl = this.panel2;
                p.PrintPage(this.panel1);
           
        }
    }
}
