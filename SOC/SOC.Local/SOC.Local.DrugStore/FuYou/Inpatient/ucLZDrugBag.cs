using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.FuYou.Inpatient
{
    public partial class ucLZDrugBag : UserControl
    {
        public ucLZDrugBag()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清屏操作
        /// </summary>
        public void Clear()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.Text = "";
                }
            }
            this.nlbRePrint.Text = "补打";
            this.neuLabel1.Text = "温馨提示：为保证患者用药安全，药品一经发出，不";
            this.neuLabel2.Text = "得退换。";
        }

        public void PrintDrugBill(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, System.Collections.ArrayList alData, string diagnose)
        {
            if (alData == null || alData.Count == 0 || drugBillClass == null)
            {
                return;
            }
            int curPageNO = 1;
            int maxPageNO = alData.Count;
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pageSize = pageSizeMgr.GetPageSize("DrugBag");
            if (pageSize == null)
            {
                pageSize = new FS.HISFC.Models.Base.PageSize();
                pageSize.Printer = "DrugBag";
                pageSize.Width = 472;
                pageSize.Height = 350;
                pageSize.Top = 102 - 38;

            }

            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.Width = pageSize.Width;
            paperSize.Height = pageSize.Height;

            SOC.Windows.Forms.PrintExtendPaper p = new FS.SOC.Windows.Forms.PrintExtendPaper();
            p.DrawingMargins.Top = pageSize.Top;
            p.IsLandscape = true;
            p.PrinterName = pageSize.Printer;
            p.SetPaperSize(paperSize);

            CompareApplyOutByPatient com1 = new CompareApplyOutByPatient();
            alData.Sort(com1);
            string patient = "";
            Hashtable hs = new Hashtable();

            //合并长嘱
            Hashtable hsOrderTime = new Hashtable();
            Hashtable hsApplyQty = new Hashtable();
            ArrayList alTemp = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                #region 儿科特殊处理

                string useTime = "";

                //儿科打印药袋不区分日期
                if (applyOut.ApplyDept.ID == "0701"//住院小儿科
                    || applyOut.ApplyDept.ID == "8255"//儿科一区护士站
                    )
                {
                    useTime = "";
                }
                else
                {
                    useTime = applyOut.UseTime.ToString("yyyy-MM-dd");
                }

                #endregion

                //汇总时间点
                //按天汇总
                if (!hsOrderTime.Contains(applyOut.OrderNO + useTime))
                {
                    alTemp.Add(applyOut.Clone());
                    //只显示日期，不显示时间点了
                    hsOrderTime.Add(applyOut.OrderNO + useTime, applyOut.UseTime.ToString("yyyy-MM-dd HH:mm"));
                }
                else
                {
                    string time = hsOrderTime[applyOut.OrderNO + useTime].ToString();
                    if (time.Contains(applyOut.UseTime.ToString("yyyy-MM-dd")))
                    {
                        //time += "|" + applyOut.UseTime.ToString("HH");

                        //只显示日期，不显示时间点了
                        time += "|" + applyOut.UseTime.ToString("HH:mm");
                    }
                    else
                    {
                        //只显示日期，不显示时间点了
                        time += "\r\n         |" + applyOut.UseTime.ToString("yyyy-MM-dd HH:mm");
                    }

                    hsOrderTime[applyOut.OrderNO + useTime] = time;
                }

                //计算总量
                if (!hsApplyQty.Contains(applyOut.OrderNO + useTime))
                {
                    hsApplyQty.Add(applyOut.OrderNO + useTime, applyOut.Operation.ApplyQty);
                }
                else
                {
                    hsApplyQty[applyOut.OrderNO + useTime] = FS.FrameWork.Function.NConvert.ToDecimal(hsApplyQty[applyOut.OrderNO + useTime]) + applyOut.Operation.ApplyQty;
                }
            }
            alData = alTemp;

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                if (patient != applyOut.PatientNO)
                {
                    curPageNO = 1;
                    patient = applyOut.PatientNO;
                }
                if (hs.Contains(applyOut.PatientNO))
                {
                    hs[applyOut.PatientNO] = curPageNO;
                }
                else
                {
                    hs.Add(applyOut.PatientNO, curPageNO);
                }
                curPageNO++;
            }

            patient = "";
            curPageNO = 1;

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            //this.nlbPrintTime.Text = pageSizeMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");



            Hashtable hsOrder = new Hashtable();
            FS.HISFC.Models.Order.Inpatient.Order inOrder = null;

            string age = "";
            string sex = "";

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                this.Clear();

                if (patient != applyOut.PatientNO)
                {
                    curPageNO = 1;
                    patient = applyOut.PatientNO;
                    maxPageNO = (int)hs[applyOut.PatientNO];
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = inpatientManager.GetPatientInfoByPatientNO(applyOut.PatientNO);
                    if (patientInfo != null)
                    {
                        age = inpatientManager.GetAge(patientInfo.Birthday);
                        sex = patientInfo.Sex.Name;
                    }
                }
                this.lbTitle.Visible = false;
                string bedNO = applyOut.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }
                this.nlbBedNO.Text = bedNO;

                this.nlbPageNO.Text = curPageNO.ToString() + "/" + maxPageNO.ToString();
                this.panelLine1.Visible = (curPageNO == maxPageNO);

                if (hsOrder.Contains(applyOut.OrderNO))
                {
                    inOrder = hsOrder[applyOut.OrderNO] as FS.HISFC.Models.Order.Inpatient.Order;
                }
                else
                {
                    FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
                    inOrder = orderMgr.QueryOneOrder(applyOut.OrderNO);
                }

                this.nlbCardNO.Text = "住院号：" + inOrder.Patient.PID.PatientNO;

                //this.nlbCardNO.Text = "住院号：" + applyOut.PatientNO;
                this.nlbDeptName.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

                #region 儿科特殊处理

                string useTime = "";

                //儿科打印药袋不区分日期
                if (applyOut.ApplyDept.ID == "0701"//住院小儿科
                    || applyOut.ApplyDept.ID == "8255"//儿科一区护士站
                    )
                {
                    useTime = "";
                }
                else
                {
                    useTime = applyOut.UseTime.ToString("yyyy-MM-dd");
                }

                #endregion

                if (inOrder.OrderType.IsDecompose)
                {
                    if (hsOrderTime[applyOut.OrderNO + useTime].ToString().Contains("|"))
                    {
                        //nlbUseTime.Text = "服药时间：" + hsOrderTime[applyOut.OrderNO].ToString();

                        //只显示日期，不显示时间点了
                        nlbUseTime.Text = "服药时间：" + hsOrderTime[applyOut.OrderNO + useTime].ToString();//.Replace("|", "");
                    }
                    else
                    {
                        this.nlbUseTime.Text = "服药时间：" + applyOut.UseTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                else
                {
                    this.nlbUseTime.Text = "服药时间：" + applyOut.UseTime.ToString("yyyy-MM-dd");
                }

                //this.nlbDiagnose.Text = "诊断：" + diagnose;
               

                this.lbPatient.Text = "" + applyOut.PatientName + " " + sex + " " + age;

                this.lbDrugInfo.Text = applyOut.Item.Name;
                this.lblSpec.Text = applyOut.Item.Specs;

                //this.nlbDrugQty.Text = "共："+applyOut.Operation.ApplyQty.ToString() + applyOut.Item.MinUnit;
                this.nlbDrugQty.Text = "共：" + hsApplyQty[applyOut.OrderNO + useTime].ToString() + applyOut.Item.MinUnit;

                //每次量
                //this.nlbOnceDose.Text = "每次" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;
                this.nlbOnceDose.Text = "每次" + Common.Function.GetOnceDose(applyOut);
                //特殊处理：剂型为口服的，后面增加 半包这样的
                if (applyOut.Item.DosageForm.ID == "01")//片剂胶囊
                {
                    FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                    if (applyOut.DoseOnce < item.BaseDose)
                    {
                        nlbOnceDose.Text = nlbOnceDose.Text + "(1包)";
                    }
                }

                this.nlbRePrint.Visible = !(drugBillClass.ApplyState == "0");

                this.lbUsage.Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);

                //频次
                this.nlbFrequence.Text = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(applyOut.Frequency.ID);
                if (nlbFrequence.Text.Length > "每八小时一次".Length)
                {
                    nlbFrequence.Text = nlbFrequence.Text.Substring(0, "每八小时一次".Length);
                }

                this.nlbMemo.Text =     "注意事项：" + applyOut.Memo;
                this.nlbDrugMemo.Text = "          " + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID).Product.Caution;
                p.IsShowPageNOChooseDialog = false;
                if (((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).IsManager)
                {
                    p.PrintPageView(this);
                }
                else
                {
                    p.PrintPage(this);
                }

                curPageNO++;
            }

        }


        #region 排序类
        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareApplyOutByPatient : IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //患者姓名
                string oY = "";          //患者姓名


                oX = o1.BedNO + o1.PatientName + this.GetFrequencySortNO(o1.Frequency) + this.GetOrderNo(o1) + o1.UseTime.ToString();
                oY = o2.BedNO + o2.PatientName + this.GetFrequencySortNO(o2.Frequency) + this.GetOrderNo(o2) + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }

            private string GetOrderNo(FS.HISFC.Models.Pharmacy.ApplyOut app)
            {
                string id = app.Item.ID.ToString();
                return id;
            }
            private string GetFrequencySortNO(FS.HISFC.Models.Order.Frequency f)
            {
                string id = f.ID.ToLower();
                string sortNO = "";
                if (id == "qd")
                {
                    sortNO = "1";
                }
                else if (id == "bid")
                {
                    sortNO = "2";
                }
                else if (id == "tid")
                {
                    sortNO = "3";
                }
                else
                {
                    sortNO = "4";
                }
                if (f.Name == "另加")
                {
                    sortNO = "9999" + sortNO;
                }
                else
                {
                    sortNO = "0000" + sortNO;
                }
                return sortNO;
            }

        }

        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareApplyOutByOrderNO : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //患者姓名
                string oY = "";          //患者姓名


                oX = o1.OrderNO + o1.UseTime.ToString();
                oY = o2.OrderNO + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }
        }

        #endregion
       
    }
}
