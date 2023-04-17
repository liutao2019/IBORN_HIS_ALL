using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectPrint
{
    public partial class ucInjectPrintNew : UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint
    {
        public ucInjectPrintNew()
        {
            InitializeComponent();
            ArrayList al = this.inteManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null)
            {
                return;
            }
            this.doctHelper.ArrayObject = al;

            al = this.inteManager.GetDepartment();
            if (al == null)
            {
                return;
            }
            this.deptHelper.ArrayObject = al;

        }

        /// <summary>
        /// 每页显示的条数
        /// </summary>
        private int iSet = 10;

        /// <summary>
        /// 当前显示组号
        /// </summary>
        private int showGroupNO = 0;

        /// <summary>
        /// 当前记录的组合号
        /// </summary>
        private string rememberComboNO = "";

        /// <summary>
        /// 合并第一列单元格的起始行号
        /// </summary>
        private int spanRowIndex = 1;

        /// <summary>
        /// 医院名称
        /// </summary>
        string strHosName = string.Empty;

        private FS.HISFC.Models.Registration.Register register;

        /// <summary>
        /// 院注管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject injectManager = new FS.HISFC.BizLogic.Nurse.Inject();

        /// <summary>
        /// 综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 医生帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        ///常数维护业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();


        #region IInjectPrint 成员

        public void Init(System.Collections.ArrayList alPrintData)
        {
            string data = alPrintData[0] as string;
            if (data.Contains("(*)"))
            {
                DialogResult dr = MessageBox.Show("是否补打?");
                if (dr == DialogResult.OK)
                {
                    this.lblReprint.Visible = true;
                    data = data.Replace("(*)", "");
                }
                else
                {
                    return;
                }
            }
            this.lblOrder.Text = data;
            //this.print();
        }

        #endregion
        /// <summary>
        /// 打印注射单
        /// </summary>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(System.Collections.ArrayList alInject, bool isPreview)
        {
            //[2012-08-20]注射单不用分开打印
            FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();
            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            usageHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            //FS.HISFC.BizLogic.Fee.Outpatient feeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

            //ArrayList alIM = new ArrayList();    //IM肌注，单独打一张注射单，其他用法打另外一张注射单
            //ArrayList alOther = new ArrayList(); //除肌注以外的药品
            ArrayList al = new ArrayList();
            ArrayList alSpecial = new ArrayList();//已经维护的需要特别打单的药品


            //ArrayList alFeeItemList = feeMgr.QueryFeeItemListsByInvoiceNO(drugRecipe.InvoiceNO);

            ArrayList alSpecialInjectBill = constMgr.GetAllList("SpecialInjectBill");

            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alInject)
            {
                if (FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID))
                {
                    #region 肌注和其他用法分单打印
                    FS.FrameWork.Models.NeuObject neuObject = usageHelper.GetObjectFromID(order.Usage.ID);
                    if (neuObject == null)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
                    if (usage == null)
                    {
                        continue;
                    }
                    //[2011-6-20]zhaozf 有三个术中用的药品要单独打单
                    bool isSpecial = false;
                    if (alSpecialInjectBill != null && alSpecialInjectBill.Count > 0)
                    {
                        foreach (FS.HISFC.Models.Base.Const cnst in alSpecialInjectBill)
                        {
                            if (cnst.ID == order.Item.ID)
                            {
                                alSpecial.Add(order);
                                isSpecial = true;
                                break;
                            }
                        }
                    }
                    if (!isSpecial)
                    {
                        //if (usage.UserCode == "IM")//肌肉注射
                        //{
                        //    alIM.Add(order);
                        //}
                        //else
                        //{
                        //    alOther.Add(order);
                        //}
                        al.Add(order);
                    }
                    #endregion
                }
            }
            if (alSpecial.Count > 0)
            {
                alSpecial.Sort(new CompareApplyOutByCombNO());
                PrintAllPage(alSpecial, isPreview);
            }
            if (al.Count > 0)
            {
                al.Sort(new CompareApplyOutByCombNO());
                PrintAllPage(al, isPreview);
            }
            else
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 打印所有院注射单
        /// </summary>
        /// <param name="alData"></param>
        private void PrintAllPage(ArrayList alData, bool isPreview)
        {
            try
            {
                //获取医院名称
                //strHosName = this.constMgr.GetHospitalName();
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = alData.GetRange(iSet * (i - 1), iSet);
                        this.PrintOnePage(alPrint, i, icount, isPreview);
                    }
                    else
                    {
                        int num = alData.Count % iSet;
                        if (alData.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = alData.GetRange(iSet * (i - 1), num);
                        this.PrintOnePage(alPrint, i, icount, isPreview);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("打印出错!" + e.Message);
                return;
            }
        }

        private int ComboCount(ArrayList alTmp, FS.HISFC.Models.Order.OutPatient.Order ord)
        {
            int j = 1;
            for (int i = 0; i < alTmp.Count; i++)
            {
                FS.HISFC.Models.Order.OutPatient.Order order = (FS.HISFC.Models.Order.OutPatient.Order)alTmp[i];
                if (ord.ID == order.ID)
                {
                    continue;
                }
                else
                {
                    if (order.Combo.ID == ord.Combo.ID)
                    {
                        j++;
                    }
                }
            }

            return j;
        }

        /// <summary>
        /// 打印一个院注射单
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="current"></param>
        /// <param name="total"></param>
        private void PrintOnePage(ArrayList alData, int current, int total, bool isPreview)
        {
            //showGroupNO = 0;//必须初始化，否则重新打印组号会出错
            //this.neuLabel2.Text = strHosName + "院注单";
            try
            {
                spanRowIndex = 0;
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }

                //设置对齐
                for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
                {
                    if (i == 1)
                    {
                        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    }

                    this.neuSpread1_Sheet1.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }

                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Gray, 1, false, false, false, true);
                FarPoint.Win.LineBorder allBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);

                //赋值并打印
                for (int i = 0; i < alData.Count; i++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order order = (FS.HISFC.Models.Order.OutPatient.Order)alData[i];

                    FS.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = bottomBorder;

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name + "\n" + order.Item.Specs;
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Height *= 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = order.Frequency.Name;//次数
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = order.Usage.Name;//用法
                    //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Border = allBorder;
                    //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Border = allBorder;
                    //修改组号
                    if (order.Combo.ID != rememberComboNO)
                    {
                        rememberComboNO = order.Combo.ID;
                        spanRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                        showGroupNO++;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].Border = allBorder;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        //this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].ColumnSpan = 2;
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = showGroupNO.ToString() + "组用法：" + order.Usage.Name;//用法
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = order.Frequency.Name; 
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = order.Sample.Memo;//用法
                        //this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                        //this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = bottomBorder;
                    }
                    
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = showGroupNO.ToString();
                    
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                    if (this.ComboCount(alData, order) == 1)
                    {
                        //this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = showGroupNO.ToString();
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].ColumnSpan = 2;
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = showGroupNO.ToString() + "组用法：" + order.Usage.Name;
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = order.Frequency.Name; 
                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = order.Sample.Memo;//用法
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                    }
                    
                    
                }
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 5;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "\n总输液量:";
                this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Height *= 2;
                //this.lbCard.Text = regObj.PID.CardNO;
                //this.lbName.Text = regObj.Name;

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd 　HH:mm:ss"); //打印时间
                FS.FrameWork.Management.DataBaseManger dataBaseManger = new FS.FrameWork.Management.DataBaseManger();

                //this.lbAge.Text = dataBaseManger.GetAge(regObj.Birthday);
                //this.lbSex.Text = regObj.Sex.Name;
                this.lbPage.Text = "第" + current.ToString() + "页" + "/" + "共" + total.ToString() + "页";

                //////增加fp下面的内容
                //this.neuDoctName.Text = "医生：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(reciptDoct.Name);//医生姓名 
                Print(isPreview);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int setPatient(FS.HISFC.Models.Registration.Register patient)
        {
            this.register = patient;
            if(this.register == null)
            {
                return -1;
            }

            this.lblName.Text = register.Name;
            if("男".Equals(register.Sex.Name))
            {
                this.chkMale.Checked = true;
                this.chkFemale.Checked = false;
            }
            else
            {
                this.chkMale.Checked = false;
                this.chkFemale.Checked = true;
            }
            this.lblAge.Text = this.injectManager.GetAge(register.Birthday);
            this.lblSeeDept.Text = this.deptHelper.GetName(register.SeeDoct.Dept.ID);
            this.lblSeeDoct.Text = this.doctHelper.GetName(register.SeeDoct.ID);
            this.lblCardNo.Text = register.PID.CardNO;
            this.npbBarCode.Image = this.CreateBarCode(register.PID.CardNO);//生成条码;
            if (string.IsNullOrEmpty(register.AddressHome))
            {
                if (string.IsNullOrEmpty(register.PhoneHome))
                {
                    this.lblTel.Visible = false;
                }
                else
                {
                    this.lblTel.Text = register.PhoneHome;
                }
            }
            else
            {
                this.lblTel.Text = register.AddressHome + " / " + register.PhoneHome;
            }
            if (string.IsNullOrEmpty(register.IDCard))
            {
                this.lblIdenNO.Visible = false;
            }
            else
            {
                this.lblIdenNO.Text = register.IDCard;
            }

            if (this.register.Pact.PayKind.ID == "01")
            {
                this.lblFeeType.Text = "自费";
            }
            else if (this.register.Pact.PayKind.ID == "02")
            {
                this.lblFeeType.Text = "医保";
            }
            else
            {
                this.lblFeeType.Text = "公费";
            }

            return 1;
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
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }

        private void Print(bool isPreview)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("Inject",this.Width,this.Height));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            if (isPreview)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }
    }
    #region 排序类

    /// <summary>
    /// 按处方排序类
    /// </summary>
    public class CompareApplyOutByCombNO : IComparer
    {
        /// <summary>
        /// 排序方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.OutPatient.Order o1 = (x as FS.HISFC.Models.Order.OutPatient.Order).Clone();
            FS.HISFC.Models.Order.OutPatient.Order o2 = (y as FS.HISFC.Models.Order.OutPatient.Order).Clone();

            string oX = o1.Combo.ID;
            string oY = o2.Combo.ID;

            int nComp;

            if (oX == null)
            {
                nComp = (oY != null) ? -1 : 0;
            }
            else if (oY == null)
            {
                nComp = 1;
            }
            else
            {
                nComp = string.Compare(oX.ToString(), oY.ToString());
            }

            return nComp;
        }

    }
    #endregion
}
