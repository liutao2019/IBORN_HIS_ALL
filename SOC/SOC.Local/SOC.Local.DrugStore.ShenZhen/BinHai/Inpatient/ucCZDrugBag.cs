using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Inpatient
{
    public partial class ucCZDrugBag : UserControl
    {
        public ucCZDrugBag()
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
           // this.nlbRePrint.Text = "补打";
            //this.nlbRePrint.Visible = false;
            this.lblHospitalInfo.Text = "香港大学深圳医院";
            this.lblPhone.Text = "电话：";
            this.lblPhoneNum.Text = "86913333-3532";
            this.lbUsage.Text = "一次用量";
        }

        private void SetOnePaperPrintInfo(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, ArrayList alData, SOC.Windows.Forms.PrintExtendPaper p)
        {
            //alData.Sort(new ApplaySort());

            #region 第一个药品
            if (alData.Count == 0)
            {
                return;
            }
            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
            string itemName = applyOut.Item.Name;

            this.nlbDrugInfo1.Text = itemName +" "+ applyOut.Item.Specs;

            this.nlbOnceDose1.Text = "" + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut);
            #endregion

            #region 第二个药品
            if (alData.Count == 1)
            {
                return;
            }
            applyOut = alData[1] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;

            this.nlbDrugInfo2.Text = itemName + " " + applyOut.Item.Specs; ;

            this.nlbOnceDose2.Text = "" + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut);

            #endregion

            #region 第三个药品
            if (alData.Count == 2)
            {
                return;
            }
            applyOut = alData[2] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;
            this.nlbDrugInfo3.Text = itemName + " " + applyOut.Item.Specs;

            this.nlbOnceDose3.Text = "" + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut); // applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;

            #endregion

            #region 第四个药品
            if (alData.Count == 3)
            {
                return;
            }
            applyOut = alData[3] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;

            this.nlbDrugInfo4.Text = itemName + " " + applyOut.Item.Specs; ;

            this.nlbOnceDose4.Text = "" + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut);

            if (alData.Count == 4)
            {
                return;
            }
            applyOut = alData[4] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;

            this.nlbDrugInfo5.Text = itemName + " " + applyOut.Item.Specs; ;

            this.nlbOnceDose5.Text = "" + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut);

            if (alData.Count == 5)
            {
                return;
            }
            applyOut = alData[5] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;

            this.nlbDrugInfo6.Text = itemName + " " + applyOut.Item.Specs; ;
            this.nlbOnceDose6.Text = "" + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut);
            if (alData.Count == 6)
            {
                return;
            }
            applyOut = alData[6] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;

            this.nlbDrugInfo7.Text = itemName + " " + applyOut.Item.Specs; ;

            this.nlbOnceDose7.Text = "" + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut);



            if (alData.Count == 7)
            {
                return;
            }
            applyOut = alData[7] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;

            this.nlbDrugInfo8.Text = itemName + " " + applyOut.Item.Specs; ;

            this.nlbOnceDose8.Text = "" + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut);
           

            #endregion

        }

        private string GetDosageForm(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string tmp = FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugDoseModual(applyOut.Item.DosageForm.ID).UserCode;
            if (string.IsNullOrEmpty(tmp))
            {
                tmp = applyOut.Item.DosageForm.ID;
            }
            return tmp;
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
                pageSize.Width = this.Width;
                pageSize.Height = this.Height;

            }

            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.Width = pageSize.Width;
            paperSize.Height = pageSize.Height;

            SOC.Windows.Forms.PrintExtendPaper p = new FS.SOC.Windows.Forms.PrintExtendPaper();
            p.DrawingMargins.Top = pageSize.Top;
            p.PrinterName = pageSize.Printer;
            p.SetPaperSize(paperSize);

            Hashtable hsPatientUseTimeUsage = new Hashtable();

            Hashtable hsPtientList = new Hashtable();

            string age = "";
            string sex = "";

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                //if (!hsPtientList.Contains(applyOut.PatientNO))
                //{
                //    applyOut.
                //}

                string key = applyOut.PatientNO + applyOut.UseTime.ToString() + applyOut.Usage.ID; //+ this.GetDosageForm(applyOut);
                if (hsPatientUseTimeUsage.Contains(key))
                {
                    ((ArrayList)hsPatientUseTimeUsage[key]).Add(applyOut);
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(applyOut);
                    hsPatientUseTimeUsage.Add(key, al);
                }

            }
            ArrayList alPapers = new ArrayList();
            foreach (ArrayList alPatientUseTimeUsage in hsPatientUseTimeUsage.Values)
            {
                //4个一页，拆分
                int index = 0;
                while (index < alPatientUseTimeUsage.Count)
                {
                    int count = 8;
                    if (index + count > alPatientUseTimeUsage.Count)
                    {
                        count = alPatientUseTimeUsage.Count - index;
                    }
                    alPapers.Add(alPatientUseTimeUsage.GetRange(index, count));
                    index = index + 8;
                }
            }
            curPageNO = 1;
            maxPageNO = alPapers.Count;

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

            alPapers.Sort(new ApplaySortByTime());

            foreach (ArrayList alPaper in alPapers)
            {
                if (alPaper == null || alPaper.Count == 0)
                {
                    continue;
                }

                this.Clear();

                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alPaper[0] as FS.HISFC.Models.Pharmacy.ApplyOut;

                FS.HISFC.Models.RADT.PatientInfo patientInfo = inpatientManager.GetPatientInfoByPatientNO(applyOut.PatientNO);
                if (patientInfo != null)
                {
                    age = inpatientManager.GetAge(patientInfo.Birthday);
                    sex = patientInfo.Sex.Name;
                }
                string bedNO = applyOut.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }
                this.nlbBedNO.Text = bedNO+"床";
                this.nlbPageNO.Text = "第" + curPageNO.ToString() + "包，共" + maxPageNO.ToString() + "包";
                this.panelLine1.Visible = (curPageNO == maxPageNO);
                this.nlbCardNO.Text = "" + patientInfo.PID.PatientNO;
                this.nlbDeptName.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);
                if (applyOut.UseTime.Hour <= 9)
                {
                    this.nlbUseTime.Text = applyOut.UseTime.ToShortDateString() +  "  早";// + applyOut.UseTime.Hour.ToString("F2") + "点"; ;
                }
                else if (applyOut.UseTime.Hour <= 12)
                {
                    this.nlbUseTime.Text = applyOut.UseTime.ToShortDateString() + "  中";//+ applyOut.UseTime.Hour.ToString("F2") + "点";
                }
                else if (applyOut.UseTime.Hour <= 20)
                {
                    this.nlbUseTime.Text = applyOut.UseTime.ToShortDateString() + "  晚";//+ applyOut.UseTime.Hour.ToString("F2") + "点";
                }
                else
                {
                    this.nlbUseTime.Text =  applyOut.UseTime.ToShortDateString() + "  睡前";
                }


               // this.nlbUseTime.Text = "" + applyOut.UseTime.ToString("yyyy-MM-dd HH:mm:ss");
                this.lbPatient.Text = "" + applyOut.PatientName;
                //this.lbUsage.Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);

                //this.nlbRePrint.Visible = !(drugBillClass.ApplyState == "0");

                this.SetOnePaperPrintInfo(drugBillClass, alPaper, p);

                //其它信息          

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

        private void nlbDrugInfo2_Click(object sender, EventArgs e)
        {


        }

        private void nlbOnceDose2_Click(object sender, EventArgs e)
        {

        }
    }

    public class ApplaySort : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;

                string str1 = applyOut1.BedNO + applyOut1.UseTime.ToString() + applyOut1.ID;
                string str2 = applyOut2.BedNO + applyOut2.UseTime.ToString() + applyOut1.ID;

                return string.Compare(str1, str2);
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }

    public class ApplaySortByTime : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut1 = ((ArrayList)x)[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut2 = ((ArrayList)y)[0] as FS.HISFC.Models.Pharmacy.ApplyOut;

                string str1 = applyOut1.BedNO + applyOut1.UseTime.ToString() + applyOut1.ID;
                string str2 = applyOut2.BedNO + applyOut2.UseTime.ToString() + applyOut1.ID;

                return string.Compare(str1, str2);

                //if (applyOut1.UseTime > applyOut2.UseTime)
                //{
                //    return 1;
                //}
                //else if (applyOut1.UseTime == applyOut2.UseTime)
                //{
                //    return 0;
                //}
                //else
                //{
                //    return -1;
                //}
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}