using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using FS.FrameWork.Management;
using System.Collections;
//using FS.HISFC.BizLogic.Pharmacy;
//using FS.HISFC.Models.Pharmacy;
//using FS.HISFC.Models.HealthRecord.EnumServer;
//using FS.HISFC.BizLogic.Registration;

namespace FS.SOC.Local.DrugStore.FOSI.Outpatient
{
    public partial class ucFoSiDrugList : UserControl
    {
        public ucFoSiDrugList()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                //Init();
            }
        }     

        #region 变量

        /// <summary>
        /// 总药价
        /// </summary>
        decimal drugListTotalPrice = 0;

        /// <summary>
        /// 打印高度
        /// </summary>
        int height = 0;

        /// <summary>
        /// 最大行数
        /// </summary>
        private int rowMaxCount = 0;

        /// <summary>
        /// 药库管理类
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 挂号管理类
        /// </summary>
        FS.HISFC.BizLogic.Registration.Register regist = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 药品管理类
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// 是否打印
        /// </summary>
        private bool isPrint = true;

        string sendWindows = "";

        private bool isMoreOnePage = false;
        #endregion

        private void AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string diagnose, string hospitalName)
        {
            if (al.Count > 0)
            {
                //有效的药品页数
                int pageNO = 1;
                //无效的药品页数
                int pageInValidNO = 1;
                //行数
                int iRow = 0;
                int num = 0;

                #region 整理出用于分页打印的有效的和无效的药品信息列表
                System.Collections.Hashtable hsApplyOut = new Hashtable();
                //有效的药品数据
                Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applyOutPageList = new Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>>();
                List<FS.HISFC.Models.Pharmacy.ApplyOut> applyOutList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                //无效的药品数据
                Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applyOutInValidPageList = new Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>>();
                List<FS.HISFC.Models.Pharmacy.ApplyOut> applyOutInValidList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in al)
                {
                    //    if (applyOut.PrintState.ToString() == "1")
                    //    {
                    //        this.lbReprint.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        this.lbReprint.Visible = false;
                    //    }
                    if (drugRecipe.RecipeState.ToString() == "0")
                    {
                        this.lbReprint.Visible = false;
                    }
                    else
                    {
                        this.lbReprint.Visible = true;
                    }

                    //处理同组合打印
                    if (!hsApplyOut.Contains(applyOut.ID))
                    {
                        hsApplyOut.Add(applyOut.ID, null);

                        ArrayList alTmp = new ArrayList();
                        if (!string.IsNullOrEmpty(applyOut.CombNO))
                        {
                            //这个重新查询后包含多余数据
                            //alTmp = this.itemManager.QueryApplyOutListForClinic(applyOut.CombNO);
                        }
                        if (alTmp == null)
                        {
                            alTmp = new ArrayList();
                        }

                        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            applyOutList.Add(applyOut);

                        }
                        else
                        {
                            applyOutInValidList.Add(applyOut);
                        }
                    }
                    num++;

                    if (applyOutList.Count == 20 || (num == al.Count && applyOutList.Count > 0))
                    {
                        applyOutPageList.Add(pageNO, applyOutList);
                        applyOutList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        pageNO++;
                    }

                    if (applyOutInValidList.Count == 20 || (num == al.Count && applyOutInValidList.Count > 0))
                    {
                        applyOutInValidPageList.Add(pageInValidNO, applyOutInValidList);
                        applyOutInValidList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        pageInValidNO++;
                    }
                }

                #endregion

                FS.HISFC.Models.Pharmacy.ApplyOut info = (FS.HISFC.Models.Pharmacy.ApplyOut)al[0];

                #region 设置数据并打印
                foreach (KeyValuePair<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applist in applyOutInValidPageList)
                {
                    this.Clear();
                    this.label1.Text = "     退单";
                    this.label1.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(0).BackColor = System.Drawing.SystemColors.Control;
                    this.neuSpread1_Sheet1.Columns.Get(1).BackColor = System.Drawing.SystemColors.Control;
                    this.neuSpread1_Sheet1.Columns.Get(2).BackColor = System.Drawing.SystemColors.Control;

                    SetLableValue(drugRecipe, diagnose, hospitalName);
                    this.isMoreOnePage = (applyOutPageList.Count > 1);
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applayOut in applist.Value)
                    {
                        SetDrugDeatail(iRow, applayOut, true);
                        this.DrawLing(iRow);
                        iRow++;
                    }
                    //设置总药价和发药时间
                    //this.AddLastRow();
                    this.AddLastRow(iRow);

                    this.DrawLing(iRow);

                    drugListTotalPrice = 0;

                    iRow = 1;

                    this.Print();
                }
                iRow = 0;
                foreach (KeyValuePair<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applist in applyOutPageList)
                {
                    // 设置Lable的值
                    this.Clear();
                    this.label1.Text = "佛山市第四人民医院药房单";
                    this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
                    this.neuSpread1_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.White;
                    this.neuSpread1_Sheet1.Columns.Get(2).BackColor = System.Drawing.Color.White;

                    SetLableValue(drugRecipe, diagnose, hospitalName);
                    this.isMoreOnePage = (applyOutPageList.Count > 1);
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applayOut in applist.Value)
                    {
                        SetDrugDeatail(iRow, applayOut, true);
                        this.DrawLing(iRow);
                        iRow++;
                    }
                    //设置总药价和发药时间
                    //this.AddLastRow();
                    this.AddLastRow(iRow);

                    this.DrawLing(iRow);

                    drugListTotalPrice = 0;

                    iRow = 1;

                    this.Print();
                }
                #endregion

                this.isPrint = true;
            }
        }

        private void DrawLing(int iRow)
        { 
             FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, false, false);
             FarPoint.Win.LineBorder lineBorder12 = new FarPoint.Win.LineBorder(Color.Black, 1, false, false, false, true);

                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow, 0).Border = lineBorder11;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow, 1).Border = lineBorder11;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow, 2).Border = lineBorder11;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow + 3, 0).Border = lineBorder12;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow + 3, 1).Border = lineBorder12;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow + 3, 2).Border = lineBorder12;
 
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            if (isPrint == true)
            {
                //this.neuPanel1.Dock = DockStyle.None;

                int heght = 138;

                int count = this.neuSpread1_Sheet1.Rows.Count;

                int addHeght = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows[0].Height);

                int addHeght1 = addHeght * count;

                heght += addHeght1;
 
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 400, 550));
                print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 460, heght + 160));
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //print.IsDataAutoExtend = false;
                try
                {
                    //普济分院4号窗口自动打印总是暂停：打印机针头或纸张太薄或太厚都可能引起暂停
                    FS.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                }
                catch { }
                this.Size = new Size(460, heght);
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(0, 0, this);
                }
                else
                {
                    print.PrintPage(0, 0, this);
                }

                //this.neuPanel1.Dock = DockStyle.Fill;
                this.Clear();
            }

            this.isPrint = true;
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
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// 清除控件文字
        /// </summary>
        private void Clear()
        {
            lbName.Text = "姓名:";
            lbCardNo.Text = "";
            lbSex.Text = "性别:";
            lbDiagnose.Text = "诊断:";
            lbAge.Text = "年龄:";
            lbDeptName.Text = "科室名称:";
            lbInvoice.Text = "发票号:";
            lbRecipe.Text = "处方号:";
            lblDoc.Text = "医师:";
            lblDoc.Text = "医师:";
            lblPhone.Text = "";
            lblPrintDate.Text = "";
            this.sendWindows = "";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.Clear();

        }

        /// <summary>
        /// 设置Lable的值
        /// </summary>
        /// <param name="info"></param>
        private void SetLableValue(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string diagnose, string hospitalName)
        {

            //姓名
            lbName.Text = "姓名:"+drugRecipe.PatientName.ToString();

            //卡号
            this.lbCardNo.Text = drugRecipe.CardNO.TrimStart('0').ToString();

            lbInvoice.Text = "";
            
            //性别
            lbSex.Text =  "性别："+drugRecipe.Sex.Name.ToString();

            //年龄
            lbAge.Text = "年龄：" + itemManager.GetAge(drugRecipe.Age);

            //医师
            this.lblDoc.Text = "医师：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            //处方号
            this.lbRecipe.Text = "处方号：" + drugRecipe.RecipeNO;
            //科室名称
            lbDeptName.Text = "科室名称：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            //诊断
            lbDiagnose.Text = "诊断：" + diagnose.ToString();

            //电话
            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
            register = regist.GetByClinic(drugRecipe.ClinicNO);
            lblPhone.Text ="电话："+ register.PhoneHome;

             //打印时间
            lblPrintDate.Text = itemManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
 
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
        /// 获取频次名称
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetFrequency(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        { 
            FS.HISFC.Models.Order.Frequency frequency = applyOut.Frequency as FS.HISFC.Models.Order.Frequency;
            return Common.Function.GetFrequenceName(frequency);
        }

        /// <summary>
        /// 将信息添加到列表中
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="info"></param>
        private void SetDrugDeatail(int iRow, FS.HISFC.Models.Pharmacy.ApplyOut info, bool isValid)
        {
            this.neuSpread1_Sheet1.Rows.Add(4 * iRow,4);

            FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (item != null)
                {
                    if(item.NameCollection.RegularName != null && item.NameCollection.RegularName != "")
                    {
                        this.neuSpread1_Sheet1.SetText(4 * iRow, 0,iRow+1+" "+ item.NameCollection.RegularName.ToString());
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.SetText(4 * iRow, 0, iRow+1+"");
                    }
                }
                else
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 0, "");
                }

                if (info.Item.Specs == null || info.Item.Specs == "")
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 1, "");
                }
                else
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 1, info.Item.Specs.ToString());
                }

                int outMinQty;

                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
                if (outPackQty == 0)
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 2, ((int)(info.Operation.ApplyQty * info.Days)).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit);
                }
                else if (outMinQty == 0)
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 2, outPackQty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.PackUnit);
                }
                else
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 2, ((int)(info.Operation.ApplyQty * info.Days)).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit);
                }

                this.neuSpread1_Sheet1.SetText(4 * iRow + 1, 0, info.Item.NameCollection.Name.ToString());

                if (string.IsNullOrEmpty(info.DoseOnce.ToString()))
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 0, "用法： " + "遵医嘱");
                }
                this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 0, "用法： " + "每次" + this.GetOnceDose(info).ToString() + "("+info.DoseOnce+info.Item.DoseUnit+")");

                if ((info.Frequency.ID) == null || info.Frequency.ID == "")
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 1, "");
                }
                else
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 1, this.GetFrequency(info));
                }

                this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 2,SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));
                try
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow + 3, 0, "注意事项：" + Common.Function.GetOrder(info.OrderNO).Memo);
                }
                catch { }

                //总药价
                this.drugListTotalPrice += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * (info.Operation.ApplyQty * info.Days);

        }

        /// <summary>
        /// 增加最后一行
        /// </summary>
        private void AddLastRow(int iRow)
        {
            try
            {
                this.neuSpread1_Sheet1.Rows.Add(4 * iRow,4); 
                this.neuSpread1_Sheet1.SetText(4 * iRow + 1, 0, "配药人签名:");
                this.neuSpread1_Sheet1.SetText(4 * iRow + 1, 1, "核对人签名:");
                this.neuSpread1_Sheet1.Rows[4 * iRow + 1].Font = new Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.SetText(4 * iRow + 3, 0, string.Format("总药价：￥{0} 打印时间：{1}    {2}",
                           new object[] { this.drugListTotalPrice.ToString("0.00"), itemManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"), this.sendWindows }));
            }
            catch { }
            //FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, false, true);
            ////this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).Border = lineBorder11;
            ////this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 1).Border = lineBorder11;
            ////this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 2).Border = lineBorder11;
            //this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            ////this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
        }

        /// <summary>
        /// 打印配药清单
        /// </summary>
        /// <param name="alData">出库申请实体</param>
        /// <param name="diagnose">诊断</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">终端信息</param>
        /// <returns></returns>
        public int PrintDrugBill(ArrayList alData, string diagnose, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, string hospitalName)
        {
            if (alData == null || drugRecipe == null)
            {
                return -1;
            }
            this.Init();
            //this.npbBarCode.Image = this.CreateBarCode(drugRecipe.RecipeNO);
            this.AddAllData(alData, drugRecipe, diagnose, hospitalName);

            return 0;
        }

    }
    
}
