﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ChaoYang.Outpatient
{
    public partial class ucReicpeInfoLabelZJ : UserControl
    {
        public ucReicpeInfoLabelZJ()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;

        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void Clear()
        {
            this.nlbRecipeNO.Text = "";
            this.lbPatientName.Text = "";
            this.nlbPrintTime.Text = "";
            this.nlbPageNO.Text = "";
            this.nlbSendTerminal.Text = "";
            lblPactInfo.Text = "";
            this.label1.Text = "";
            this.label2.Text = "";
        }

        /// <summary>d
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OutPatientDrugZJ");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugZJ", 400, 200);
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
        
        FS.HISFC.BizLogic.Registration.Register regist = new FS.HISFC.BizLogic.Registration.Register();


        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, bool isPrintRegularName, string hospitalName, bool isPrintMemo, string diagnose, DateTime printTime)
        {
            if (alData == null || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }

            this.Clear();
            this.lbPatientName.Text = drugRecipe.PatientName + "  " + drugRecipe.Sex.Name+ "  " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
            this.nlbDocName.Text = "医生:" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.nlbDocDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            this.nlbDiagnose.Text = "诊断:" + diagnose;
            this.nlbPrintTime.Text = printTime.ToString();
            this.nlbPageNO.Text = alData.Count.ToString();
            //this.nlbFeeTime.Text = "收费:" + drugRecipe.FeeOper.OperTime.ToString();
            this.nlbPrintTime.Text = "" + printTime.ToString();
            this.nlbSendTerminal.Text = drugTerminal.Name;
            this.nlbHospitalInfo.Text = hospitalName;
            int x = this.GetHospitalNameLocationX();
            this.nlbHospitalInfo.Location = new Point(x, this.nlbHospitalInfo.Location.Y);
            this.nlbRecipeNO.Text = drugRecipe.RecipeNO + "方";
            this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");
            if (!string.IsNullOrEmpty(drugRecipe.DrugedOper.Memo))
            {
                this.label1.Text = drugRecipe.DrugedOper.Memo.ToString();
            }

            this.BackColor = System.Drawing.Color.White;
            this.npbBarCode.Image = this.CreateBarCode(drugRecipe.RecipeNO);

            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
            register = regist.GetByClinic(drugRecipe.ClinicNO);
            FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();
            ArrayList al = account.GetMarkByCardNo(drugRecipe.CardNO,"ALL","1");
            if(al!=null && al.Count >0)
            {
               
                for (int i = 0; i < al.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject neuObject = al[i] as FS.FrameWork.Models.NeuObject;
                    if(neuObject.ID.Length >= 7)
                    {
                    this.label2.Text += neuObject.ID.Substring(neuObject.ID.Length - 6, 6)+"\n";
                    }
                }
                    
            }
            if (register != null)
            {
                this.lblPactInfo.Text = register.Pact.Name;
            }
            else
            {
                this.lblPactInfo.Text="";
            }

            this.Print();
            return 1;
        }
        private string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";

        private int GetHospitalNameLocationX()
        {
            return FS.FrameWork.Function.NConvert.ToInt32(SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "HospitalNameLocationX", this.nlbHospitalInfo.Location.X.ToString()));
        }

    }
}
