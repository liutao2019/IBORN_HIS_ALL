using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Outpatient
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
        FS.HISFC.Models.Base.PageSize pageSize;
        private GraphicsPath path;

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
            this.nlbPrintTime.Text = "";
            this.nlbPageNO.Text = "";
            this.lbDrugInfo.Text = "";
            this.lbUsage.Text = "";
            this.nlbFrequence.Text = "";
            this.nlbOnceDose.Text = "";
            this.nlbMemo.Text = "";
            this.nlbRecipeNO.Text = "";

            this.nlbSendTerminal.Text = "";
            this.nlbComboNO.Text = "";
            this.lblPlaceNO.Text = "";
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
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 315, 160);
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

            return Common.Function.GetOnceDoseGYZL(applyOut);
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
        /// 获取图片路径
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, bool isPrintRegularName, string hospitalName, bool isPrintMemo, string qtyShowType, DateTime printTime, string pageNO)
        {
            if (alData == null || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }
            if (alApplyOut != null)
            {
                alApplyOut.Clear();
            }
            this.alApplyOut = alData;

            hsCombo.Clear();
            int comboNO = 1;

            //翻转图片90度
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

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                this.Clear();
                this.lbPatientName.Text = drugRecipe.PatientName + "  " + drugRecipe.Sex.Name + "  " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
                this.nlbPrintTime.Text = printTime.ToString();
                this.nlbPageNO.Text = pageNO;
                if (pageNO.StartsWith("1"))
                {
                    this.nlbPageNO.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                else
                {
                    this.nlbPageNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                string itemName = applyOut.Item.Name;
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                if (isPrintRegularName)
                {
                    if (item == null)
                    {
                        MessageBox.Show("打印标签时获取药品基本信息失败");
                        return -1;
                    }

                    itemName = item.NameCollection.RegularName;
                }
                this.lbDrugInfo.Text = itemName;

                this.nlbSpecs.Text = applyOut.Item.Specs;

                
                this.nlbFrequence.Text = this.GetFrequenceName(applyOut.Frequency);

                this.nlbMemo.Visible = isPrintMemo;
                this.nlbMemo1.Visible = isPrintMemo;
                //this.nlbMemo.Text = applyOut.Memo;
                FS.HISFC.Models.Order.OutPatient.Order order = SOC.Local.DrugStore.GuangZhou.Common.Function.GetOrder(applyOut.OrderNO);
                if (order != null)
                {
                    this.nlbMemo.Text = order.Memo;

                    switch ((Int32)order.HypoTest)
                    {
                        case 2:
                            this.lbDrugInfo.Text += "(皮试)";
                            break;
                        case 3:
                            this.lbDrugInfo.Text += "(阳性)";
                            break;
                        case 4:
                            this.lbDrugInfo.Text += "(阴性)";
                            break;
                        default:
                            break;
                    }
                }

                string temp = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);

                if (order != null && order.InjectCount > 0)
                {
                    temp = temp + "-院注(" + order.InjectCount.ToString() + ")";
                }

                this.lbUsage.Text = temp;
                if (applyOut.DoseOnce == decimal.Zero)
                {
                    this.nlbOnceDose.Text = "";
                    this.lbUsage.Text = "用法说明";
                }
                else
                {
                    this.nlbOnceDose.Text = "每次" + this.GetOnceDose(applyOut);
                }

                if (!string.IsNullOrEmpty(item.Product.StoreCondition))
                {
                    this.nlbMemo.Text = (this.nlbMemo.Text + "、" + item.Product.Caution + "、储存条件：" + item.Product.StoreCondition).TrimStart('、');
                }
                if (applyOut.DoseOnce == decimal.Zero)
                {
                    this.nlbMemo.Text = (this.nlbMemo.Text + " " + "手工处方具体用法药师手写说明");
                }
                this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");

                this.nlbSendTerminal.Text = drugTerminal.Name;
                this.nlbHospitalInfo.Text = hospitalName;
                this.nlbRecipeNO.Text = drugRecipe.RecipeNO + "方";

                int x = this.GetHospitalNameLocationX();
                this.nlbHospitalInfo.Location = new Point(x, this.nlbHospitalInfo.Location.Y);
                if (!string.IsNullOrEmpty(applyOut.CombNO))
                {
                    if (!hsCombo.Contains(applyOut.CombNO))
                    {
                        hsCombo.Add(applyOut.CombNO, comboNO.ToString());
                        comboNO++;
                    }
                    this.nlbComboNO.Text = hsCombo[applyOut.CombNO].ToString() + "组";
                }
                else
                {
                    this.nlbComboNO.Text = "";
                }
                //数量显示处理
                string applyPackQty = "";
                if (qtyShowType == "包装单位")
                {
                    int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                    decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                    applyQtyInt = (int)(applyOut.Operation.ApplyQty  / applyOut.Item.PackQty);
                    applyRe = applyOut.Operation.ApplyQty  - applyQtyInt * applyOut.Item.PackQty;
                    if (applyQtyInt > 0)
                    {
                        applyPackQty += applyQtyInt.ToString() + applyOut.Item.PackUnit;
                    }
                    if (applyRe > 0)
                    {
                        applyPackQty += applyRe.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    }
                }
                else
                {
                    applyPackQty = (applyOut.Operation.ApplyQty ).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                }
                this.nlbDrugQty.Text = applyPackQty;
                string placeNO = this.storageMgr.GetPlaceNO(applyOut.StockDept.ID, applyOut.Item.ID);
                
                this.lblPlaceNO.Text = "货位号：" + placeNO;

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
