using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Outpatient
{
    public partial class ucReicpeInfoLabel : UserControl
    {
        public ucReicpeInfoLabel()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;
        private GraphicsPath path;

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
        }

        /// <summary>
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
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 360, 160);
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
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
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 360, 160);
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

            this.BackColor = System.Drawing.Color.White;
            this.npbBarCode.Image = this.CreateBarCode(drugRecipe.RecipeNO);
            Bitmap bmp = (Bitmap)this.npbBarCode.Image;
            this.path = this.CalculateGrahicsPath(bmp);
            this.npbBarCode.Region = new Region(this.path);

            Image image = this.npbBarCode.Image;
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            this.npbBarCode.Image = image;
            Matrix matrix = new Matrix(-1, 0, 0, 1, image.Width, 0);
            this.path.Transform(matrix);
            this.npbBarCode.Region = new Region(this.path);



            this.Print();
            return 1;
        }

        private GraphicsPath CalculateGrahicsPath(Bitmap bitmap)//Region设定前，Graphicpaths获取函数
        {
            int iWidth = bitmap.Width;
            int iHeight = bitmap.Height;
            GraphicsPath graphicpath = new GraphicsPath();
            System.Drawing.Color color;
            for (int row = 0; row < iHeight; row++)
                for (int wid = 0; wid < iWidth; wid++)
                {
                    color = bitmap.GetPixel(wid, row);
                    //if (255 == color.A)
                    if (255 == color.A)
                        graphicpath.AddRectangle(new Rectangle(wid, row, 1, 1));

                }

            return graphicpath;
        }


        private string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";

        private int GetHospitalNameLocationX()
        {
            return FS.FrameWork.Function.NConvert.ToInt32(SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "HospitalNameLocationX", this.nlbHospitalInfo.Location.X.ToString()));
        }

    }
}
