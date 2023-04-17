using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Inpatient
{
    public partial class ucDrugLabel : UserControl

    {
        public ucDrugLabel()
        {
            InitializeComponent();
        }

        System.Collections.ArrayList alApplyOut;

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.Models.Base.PageSize pageSize;

        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 记录组合情况
        /// </summary>
        System.Collections.Hashtable hsCombo = new System.Collections.Hashtable();
       
        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void Clear()
        {
           
            this.lbPatientName.Text = "";
            
            this.nlbPageNO.Text = "";
            this.lbDrugInfo.Text = "";
            this.lbUsage.Text = "";
            this.nlbFrequence.Text = "";
            this.nlbOnceDose.Text = "";
            this.nlbMemo.Text = "";

            this.lbDeptName.Text = "";
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("InPatientDrugLabel");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("InPatientDrugLabel", 234, 151);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            //try
            //{
            //    //普济分院4号窗口自动打印总是暂停：打印机针头或纸张太薄或太厚都可能引起暂停
            //    FS.FrameWork.WinForms.Classes.Print.ResumePrintJob();
            //}
            //catch { }
            if(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }

        /// <summary>
        /// 获取频次名称
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        private string GetFrequenceName(FS.HISFC.Models.Order.Frequency frequency)
        {
            return Common.Function.GetFrequenceName(frequency);
        }

     

        /// <summary>
        /// 获取每次用量的最小单位表现形式
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetOnceDose(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {

            return Common.Function.GetOnceDose(applyOut);
        }


        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (alData == null || drugBillClass == null || stockDept == null)
            {
                return -1;
            }
            if (alApplyOut != null)
            {
                alApplyOut.Clear();
            }
            
            //打印时间
            DateTime printTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            this.alApplyOut = alData;

            hsCombo.Clear();
            int comboNO = 1;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                this.Clear();
                string bedNO = applyOut.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4) + "床";
                }
                if (drugBillClass.ID == "R")
                {
                    FS.HISFC.Models.RADT.PatientInfo p = inpatientManager.GetPatientInfoByPatientNO(applyOut.PatientNO);
                    if (p != null && p.PVisit.InState.ID.ToString()=="2")
                    {
                        bedNO = "*" + bedNO;
                    }
                }

                string age = "";
                try
                  {
                      age = inpatientManager.GetAge(inpatientManager.GetPatientInfoByPatientNO(applyOut.PatientNO).Birthday);
                  }
                catch { }



                string privPatientName = applyOut.PatientName;
                string patientInfo = string.Format("{0} {1}住院号:{2}  {3}", bedNO, SOC.Public.String.PadRight(privPatientName, 7, ' '), inpatientManager.GetPatientInfoByPatientNO(applyOut.PatientNO).PID.PatientNO, age);
                this.lbPatientName.Text = patientInfo;
                
                this.lbDeptName.Text  = "科室:" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.ApplyDept.ID);
                this.nlbPageNO.Text = comboNO + "/" + alData.Count;
                comboNO ++;
                string itemName = applyOut.Item.Name;
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);

                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {
                    this.lbDrugInfo.Text = item.NameCollection.RegularName;
                }
                else
                {
                    this.lbDrugInfo.Text = item.Name;
                }
                //this.lbDrugInfo.Text = itemName;

                this.nlbSpecs.Text = applyOut.Item.Specs;

                
                this.nlbFrequence.Text = this.GetFrequenceName(applyOut.Frequency);
                this.nlbOnceDose.Text = "每次"+this.GetOnceDose(applyOut);


                this.nlbMemo.Text = applyOut.Memo;

                string temp = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);


                this.lbUsage.Text = temp;

                if (item!=null)
                {
                    this.nlbMemo.Text = (this.nlbMemo.Text + "、" + item.Product.Caution + "、储存条件：" + item.Product.StoreCondition).TrimStart('、');
                }


                int x = this.GetHospitalNameLocationX();
                this.nlbHospitalInfo.Location = new Point(x, this.nlbHospitalInfo.Location.Y);
                this.nlbHospitalInfo.Text = this.inpatientManager.Hospital.Name.ToString();

                decimal applyQty = applyOut.Operation.ApplyQty;
                string unit = applyOut.Item.MinUnit;


                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(applyOut.Operation.ApplyQty ), (int)applyOut.Item.PackQty, out outMinQty);
                if (string.IsNullOrEmpty(applyOut.Item.PackUnit))
                {
                    if (applyOut.Item.PackQty == 1)
                    {
                        applyOut.Item.PackUnit = applyOut.Item.MinUnit;
                    }
                    else
                    {
                        try
                        {
                            applyOut.Item.PackUnit = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID).PackUnit;
                        }
                        catch { }
                    }
                }
                if (outPackQty == 0)
                {
                    applyQty = applyOut.Operation.ApplyQty;
                    unit = applyOut.Item.MinUnit;
                }
                else if (outMinQty == 0)
                {
                    applyQty = outPackQty;
                    unit = applyOut.Item.PackUnit;
                }
                else
                {
                    applyQty = outPackQty;
                    unit = applyOut.Item.PackUnit + outMinQty.ToString() + applyOut.Item.MinUnit;
                }

                this.nlbDrugQty.Text = "总量" + applyQty + unit;
                string placeNO = this.storageMgr.GetPlaceNO(applyOut.StockDept.ID, applyOut.Item.ID);
                
                //this.lblPlaceNO.Text = "货位号：" + placeNO;

                this.BackColor = System.Drawing.Color.White;
                this.Print();
            }
            return 1;
        }
        private string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";

        private int GetHospitalNameLocationX()
        {
            return FS.FrameWork.Function.NConvert.ToInt32(SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "HospitalNameLocationX", this.nlbHospitalInfo.Location.X.ToString()));
        }

    }
}
