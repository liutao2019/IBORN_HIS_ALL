using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient
{
    public partial class ucDrugGuid : UserControl
    {
        public ucDrugGuid()
        {
            InitializeComponent();
        }

        FS.HISFC.Models.Base.PageSize pageSize = null;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// 清空赋值的数据
        /// 对界面所有的label的Text赋值""
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Label && (c.Name != "lblHospitalInfo" && c.Name != "lbLine" ))
                {
                    c.Text = "";
                }
            }

            //二维码清空
            return 1;
        }


        /// <summary>
        /// 设置患者信息
        /// 同一张处方只设置一次
        /// </summary>
        /// <param name="drugRecipe"></param>
        /// <returns></returns>
        private int SetPatientInfo(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe,FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            this.lbPatientInfo.Text = "姓名：" + drugRecipe.PatientName
                + " " + SOC.Local.DrugStore.ShenZhen.Common.Function.GetAge(drugRecipe.Age)
                + " " + drugRecipe.Sex.Name;
            this.lbPrintTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.lbTitle.Text  = "取药标签";
            this.lbMemo.Text  = "请留意广播叫号，请到 " + SOC.Local.DrugStore.ShenZhen.Common.Function.GetTerminalNameById(drugRecipe.SendTerminal.ID)+ " 取药。";
            this.lblPhone.Text = "门诊药房电话：";
            this.lblPhoneNum.Text = "86913333-3530";
            return 1;
        }



        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            ////启用华南打印基类打印
            //FS.SOC.Windows.Forms.PrintExtendPaper print = new FS.SOC.Windows.Forms.PrintExtendPaper();

            ////获取维护的纸张
            //if (pageSize == null)
            //{
            //    pageSize = pageSizeMgr.GetPageSize("OutPatientDrugLabel");
            //    //指定打印处理，default说明使用默认打印机的处理
            //    if (pageSize != null && pageSize.Printer.ToLower() == "default")
            //    {
            //        pageSize.Printer = "";
            //    }
            //    //没有维护时默认一个纸张
            //    if (pageSize == null)
            //    {
            //        pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 400, 200);
            //    }
            //}

            ////打印边距处理
            //print.DrawingMargins = new System.Drawing.Printing.Margins(pageSize.Left, 0, pageSize.Top, 0);
            //print.PaperName = pageSize.Name;
            //print.PaperHeight = pageSize.Height;
            //print.PaperWidth = pageSize.Width;
            //print.PrinterName = pageSize.Printer;

            ////不显示页码选择
            //print.IsShowPageNOChooseDialog = false;

            ////管理员使用预览功能
            //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
            //    print.PrintPageView(this);
            //}
            //else
            //{
            //    print.PrintPage(this);
            //}
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OutPatientDrugLabel");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 400, 100);
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
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }

        public int PrintDrugGuid
            (
            FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe,
            FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal
            )
        {

            this.Clear();
            this.SetPatientInfo(drugRecipe,drugTerminal);
            this.Print();
            return 1;
        }


    }
}
