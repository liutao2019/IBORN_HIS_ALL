using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// <修改记录>
    ///    1.门诊配药清单增加条码号显示 by Sunjh 2010-11-1 {347AE1EE-CC12-486b-9E65-F7F97256F587}
    /// </修改记录>
    /// </summary>
    public partial class ucOutHerbalBill : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint,FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeRecipePrint
    {
        public ucOutHerbalBill()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.Registration.Register patientInfo;

        /// <summary>
        /// 发票号
        /// </summary>
        private string invoiceNo = "";

        /// <summary>
        /// 收费类别
        /// </summary>
        private string payKind = "";

        /// <summary>
        /// 收费员
        /// </summary>
        private string feeOper = "";

        private bool isPrint;

        protected bool Isprint
        {
            get
            {
                return this.isPrint;
            }
            set
            {
                this.isPrint = value;
            }
        }


        protected override void OnLoad(EventArgs e)
        {
 
            base.OnLoad(e);
        }

        /// <summary>
        /// 用法帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper usageHelper = null;
   

        private void Init()
        {
            //获取所用用法
            if (this.usageHelper == null)
            {
                FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList alUsage = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);                
                if (alUsage == null)
                {
                    MessageBox.Show(Language.Msg("获取用法列表出错!"));
                    return;
                }

                ArrayList tempAl = new ArrayList();
                foreach (FS.FrameWork.Models.NeuObject info in alUsage)
                {
                    if (info.Memo != "")
                        info.Name = info.Memo;
                    tempAl.Add(info);
                }

                this.usageHelper = new FS.FrameWork.Public.ObjectHelper(tempAl);
            }
        }

        /// <summary>
        /// 清屏操作
        /// </summary>
        public void Clear()
        {
            this.lbPatiInfo.Text = "";
            this.lbRegInfo.Text = "";

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <returns></returns>
        public int SetPatiInfo()
        {
            this.invoiceNo = this.patientInfo.InvoiceNO;
            this.feeOper = this.patientInfo.DoctorInfo.Templet.Dept.User01;
            if (this.patientInfo.Pact.PayKind != null)
            {
                switch (this.patientInfo.Pact.PayKind.ID)
                {
                    case "01":
                        this.payKind = "自费";
                        break;
                    case "02":
                        this.payKind = "医保";
                        break;
                    case "03":
                        this.payKind = "公费";
                        break;
                }
            }
            //合同单位变更为结算种类 by Sunjh 2010-9-14 {8FE2CA47-D536-4dde-B892-44276F89593B} 
            this.lbPatiInfo.Text = string.Format("姓名:{0}  科室:{1}  结算种类:{2}  ", this.patientInfo.Name, this.patientInfo.DoctorInfo.Templet.Dept.Name, this.payKind);

            return 1;
        }



        public void AddBackFeeItem(int iIndex, FS.HISFC.Models.Fee.Outpatient.FeeItemList infoItem)
        {

            //this.lbPatiInfo.Text = string.Format("姓名:{0}  科室:{1}",infoItem.Patient.Name,infoItem.RecipeOper.Dept.ID);           
            //this.lbRegInfo.Text = string.Format("发票:{0}", infoItem.Invoice.ID);

            this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);

            this.lbPatiInfo.Text = "                 退药单";
            this.lbPatiInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));

            this.lbRegInfo.Text = string.Format("病例号：{0}   姓名：{1}  发票号：{2}", this.outPatient.PID.CardNO, this.outPatient.Name, infoItem.Invoice.ID);

            this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = string.Format("{0}[{1}]", infoItem.Item.Name, infoItem.Item.Specs); //药品名,规格

            if (infoItem.FeePack == "1")
            {
                this.neuSpread1_Sheet1.Cells[iIndex, 1].Text = (infoItem.Item.Qty / infoItem.Item.PackQty).ToString() + infoItem.Item.PriceUnit;//数量
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[iIndex, 1].Text = infoItem.Item.Qty.ToString() + infoItem.Item.PriceUnit;//数量
            }
            this.neuSpread1_Sheet1.Cells[iIndex, 2].Text = infoItem.FT.TotCost.ToString(); //金额
        }

        private void AddItem(int iIndex, FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            //try
            //{

                if (info.Item.PackQty == 0)
                    info.Item.PackQty = 1;
                if (info.Item.Type.ID == "C")
                {
                    try
                    {
                        //获取医嘱备注
                        FS.HISFC.BizLogic.Order.OutPatient.Order outPatientOrderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                        FS.HISFC.Models.Order.OutPatient.Order outPatientOrder = outPatientOrderManager.QueryOneOrder(info.PatientNO,info.OrderNO);
                        if (outPatientOrder != null)
                        {
                            info.Memo = outPatientOrder.Memo;
                        }
                    }
                    catch
                    { }
                    if (info.Memo == "")
                        this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = info.Item.Name;
                    else
                        this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = string.Format("{0}[{1}]", info.Item.Name, info.Memo);
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = string.Format("{0}[{1}]", info.Item.Name, info.Item.Specs);
                }

                int minQty = 0;
                int packQty = System.Math.DivRem((int)info.Operation.ApplyQty, (int)info.Item.PackQty, out minQty);

                if (minQty == 0)
                {
                    this.neuSpread1_Sheet1.Cells[iIndex, 1].Text = string.Format("{0}{1}", packQty, info.Item.PackUnit);
                }
                else if (packQty == 0)
                {
                    this.neuSpread1_Sheet1.Cells[iIndex, 1].Text = string.Format("{0}{1}", minQty, info.Item.MinUnit);
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[iIndex, 1].Text = string.Format("{0}{1}{2}{3}", packQty, info.Item.PackUnit, minQty, info.Item.MinUnit);
                }



                this.neuSpread1_Sheet1.Cells[iIndex, 2].Text = info.Item.PriceCollection.RetailPrice.ToString();
                this.neuSpread1_Sheet1.Cells[iIndex, 3].Text = (System.Math.Round(info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 2)).ToString();
                //if (this.usageHelper != null)
                //{
                //    this.neuSpread1_Sheet1.Cells[iIndex, 4].Text = this.usageHelper.GetName(info.Usage.ID);
                //}
                //if ((this.neuSpread1_Sheet1.Cells[iIndex, 4].Text) == "")
                //    this.neuSpread1_Sheet1.Cells[iIndex, 4].Text = "遵医嘱";
            //}
            //catch
            //{ }

        }


        #region IRecipeLabel 成员

        public decimal DrugTotNum
        {
            set
            {
                // TODO:  添加 ucClinicBill.DrugTotNum setter 实现
            }
        }

        public void AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            ArrayList al = new ArrayList();

            al.Add(info);

            this.AddAllData(al);
        }

        public void AddCombo(ArrayList alCombo)
        {
            // TODO:  添加 ucClinicBill.AddCombo 实现

            this.AddAllData(alCombo);
        }

        public void Print()
        {
            // TODO:  添加 ucClinicBill.Print 实现
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;//p.ShowPageSetup();           

            FS.HISFC.Components.Common.Classes.Function.GetPageSize("MZPY", ref p);
            
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            if (this.isPrint)
            {
                p.PrintPreview(this);
            }
            else
            {
                p.PrintPage(5, 5, this);
            }
            //p.PrintPage(70, 5, this);       


            this.Clear();
        }

        public decimal LabelTotNum
        {
            set
            {
                // TODO:  添加 ucClinicBill.LabelTotNum setter 实现
            }
        }

        public void AddAllData(ArrayList al)
        {
            // TODO:  添加 ucClinicBill.AddAllData 实现
            ArrayList cArrayList = new ArrayList();			//草药
            ArrayList pArrayList = new ArrayList();			//西药
            ArrayList zArrayList = new ArrayList();			//中成药
            string tempRecipeNo = "";
            FS.HISFC.Models.Pharmacy.ApplyOut tempApp = al[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
            tempRecipeNo = tempApp.RecipeNO;

            #region 按照药品类别 赋值至各个数组
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)
            {
                switch (info.Item.Type.ID)
                {
                    case "P":
                        pArrayList.Add(info);
                        break;
                    case "C":
                        cArrayList.Add(info);
                        break;
                    case "Z":
                        zArrayList.Add(info);
                        break;
                }
            }
            #endregion

            this.neuSpread1_Sheet1.Rows.Count = 0;
            int iIndex = 0;

            decimal pCost = 0;		//本次西药金额
            decimal zCost = 0;		//本次中成药金额
            decimal cCost = 0;		//本次草药金额
            decimal totCost = 0;	//总金额
            bool isInit = true;
            DateTime feeDate = System.DateTime.MinValue;

            #region 西药
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in pArrayList)
            {
                if (isInit)
                {
                    this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
                    this.neuSpread1_Sheet1.Cells[iIndex, 0].ColumnSpan = 3;
                    this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = "西药取药";
                    this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                    iIndex++;
                    isInit = false;
                }
                this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
                this.AddItem(iIndex, info);
                pCost = pCost + System.Math.Round(info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 2);
                iIndex++;
            }
            if (pCost > 0)
            {
                pCost = System.Math.Round(pCost, 2);
                //this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
                //this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = "西药金额";
                //this.neuSpread1_Sheet1.Cells[iIndex, 3].Text = pCost.ToString();
                //this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                totCost = totCost + pCost;
                //iIndex++;
            }
            #endregion

            #region 中成药
            isInit = true;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in zArrayList)
            {
                if (isInit)
                {
                    this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
                    this.neuSpread1_Sheet1.Cells[iIndex, 0].ColumnSpan = 3;
                    this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = "中成药取药";
                    this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                    iIndex++;
                    isInit = false;
                }
                this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
                this.AddItem(iIndex, info);
                zCost = zCost + System.Math.Round(info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 2);
                iIndex++;
            }
            if (zCost > 0)
            {
                zCost = System.Math.Round(zCost, 2);
                //this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
                //this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = "中成药金额";
                //this.neuSpread1_Sheet1.Cells[iIndex, 3].Text = zCost.ToString();
                //this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                totCost = totCost + zCost;
                //iIndex++;
            }
            #endregion

            try
            {
                #region 草药
                if (cArrayList.Count > 0)
                {
                    isInit = true;
                    try
                    {
                        //					ComboSort comboSort = new ComboSort();
                        //					cArrayList.Sort(comboSort);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("数组排序发生错误" + ex.Message);
                        return;
                    }
                    string privCombo = "";
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in cArrayList)
                    {
                        feeDate = info.Operation.ApplyOper.OperTime;

                        if (privCombo != info.CombNO)
                        {                           
                            isInit = true;
                            privCombo = info.CombNO;
                            if (iIndex > 0)
                                iIndex++;
                            totCost = totCost + cCost;
                            cCost = 0;
                        }

                        if (isInit)
                        {
                            iIndex = this.neuSpread1_Sheet1.Rows.Count;
                            this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
                            this.neuSpread1_Sheet1.Cells[iIndex, 0].ColumnSpan = 3;
                            this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = string.Format("中草药取药  剂数：{0}", info.Days.ToString());
                            this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                            isInit = false;
                            privCombo = info.CombNO;
                            iIndex++;
                        }
                        this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
                        this.AddItem(iIndex, info);
                        if (info.Item.PackQty == 0)
                            info.Item.PackQty = 1;
                        cCost = cCost + System.Math.Round(info.Operation.ApplyQty * info.Days / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 2);
                        iIndex++;
                    }
                    if (cCost > 0)
                    {                        
                        totCost = totCost + cCost;

                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.neuSpread1_Sheet1.Rows.Add(iIndex, 2);
            iIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
            this.neuSpread1_Sheet1.Cells[iIndex, 0].ColumnSpan = 4;
            //this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = string.Format("收费员:{0}   配药员：     核对员：", this.feeOper);
            this.neuSpread1_Sheet1.Cells[iIndex - 1, 0].Text = "合计：   " +System.Math.Round(totCost, 2).ToString();
            this.neuSpread1_Sheet1.Cells[iIndex, 0].Text =     "配药员：      核对员：";
            this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));

            this.lbRegInfo.Text = string.Format("{0} 病历号:{1}", this.patientInfo.User01.ToString(), this.OutpatientInfo.PID.CardNO);
            //this.lbRegInfo.Text = string.Format("{0} 病历号:{1} 合计:{2}", this.patientInfo.User01.ToString(), this.OutpatientInfo.PID.CardNO, System.Math.Round(totCost, 2).ToString());

            //门诊配药清单增加条码号显示 by Sunjh 2010-11-1 {347AE1EE-CC12-486b-9E65-F7F97256F587}
            try
            {
                this.picBarCode.Image = FS.FrameWork.WinForms.Classes.CodePrint.GetCode39(tempRecipeNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.lblBarCode.Text = tempRecipeNo;
        }

        #endregion       

        #region IDrugPrint 成员

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public FS.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public FS.HISFC.Models.Registration.Register OutpatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                // TODO:  添加 ucClinicBill.PatientInfo setter 实现
                this.patientInfo = value;
                this.Clear();

                this.SetPatiInfo();
            }
        }

        public void Preview()
        {
            // TODO:  添加 ucClinicBill.Print 实现
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;//p.ShowPageSetup();       
            FS.HISFC.Components.Common.Classes.Function.GetPageSize("MZPY", ref p);

            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            p.PrintPreview(10, 0, this);
            //p.PrintPage(70, 5, this);

            this.Clear();
        }

        #endregion

        protected class ComboSort : System.Collections.IComparer
        {
            public ComboSort() { }


            #region IComparer 成员

            public int Compare(object x, object y)
            {
                // TODO:  添加 FeeSort.Compare 实现
                FS.HISFC.Models.Pharmacy.ApplyOut obj1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut obj2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (obj1 == null || obj2 == null)
                    throw new Exception("数组内必须为Pharmacy.ApplyOut类型");
                int i1 = NConvert.ToInt32(obj1.CombNO);
                int i2 = NConvert.ToInt32(obj2.CombNO);
                return i1 - i2;
            }

            #endregion
        }

        #region IBackFeeRecipePrint 成员
       
        public int SetData(ArrayList alBackFee)
        {
            this.Clear();
            this.isPrint = true;
            int i = 0;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alBackFee)
            {
                this.AddBackFeeItem(i, f);
                i++;
            }

            return 1;
        }

        #endregion

        #region IBackFeeRecipePrint 成员

        private FS.HISFC.Models.Registration.Register outPatient = null;

        public FS.HISFC.Models.Registration.Register Patient
        {
            set {

                if (value != null)
                {
                    this.outPatient = value;
                }
            }
        }

        #endregion
    }
}
