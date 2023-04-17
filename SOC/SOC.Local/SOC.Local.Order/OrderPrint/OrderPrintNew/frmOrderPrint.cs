using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;
using System.IO;
using System.Xml;

namespace FS.SOC.Local.Order.OrderPrint.OrderPrintNew
{
    /// <summary>
    /// 医嘱单打印
    /// </summary>
    public partial class frmOrderPrint : Form
    {
        public frmOrderPrint()
        {
            InitializeComponent();
        }
        #region 变量

        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizLogic.Order.Order orderTermManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizProcess.Integrate.Fee interFeeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        System.Windows.Forms.ContextMenu menu = new ContextMenu();
        FS.HISFC.BizLogic.Order.OrderBill orderBillMgr = new FS.HISFC.BizLogic.Order.OrderBill();
        FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
        //protected FS.HISFC.BizLogic.Order.InpatientOrderExtend orderExtendMgr = new FS.HISFC.BizLogic.Order.InpatientOrderExtend();
        FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.FrameWork.Public.ObjectHelper emplHelPer = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 存储打印不同类型时使用的打印机型号
        /// </summary>
        private string fileName = Application.StartupPath + "\\Setting\\EMRPRINTER.xml";

        /// <summary>
        /// 打印机名
        /// </summary>
        private string printerName = string.Empty;

        /// <summary>
        /// 东莞本地医嘱打印
        /// </summary>
        OrderQuery orderMgr = new OrderQuery();

        /// <summary>
        /// 查询停止医嘱审核护士SQL
        /// </summary>
        private string execDCConfirmOper = "SELECT (SELECT EMPL_NAME FROM COM_EMPLOYEE WHERE EMPL_CODE=DC_CONFIRM_OPER) FROM MET_IPM_ORDER WHERE MO_ORDER='{0}'";

        /// <summary>
        /// 长期医嘱列表
        /// </summary>
        ArrayList alLong = new ArrayList();

        /// <summary>
        /// 临时医嘱列表
        /// </summary>
        ArrayList alShort = new ArrayList();

        /// <summary>
        /// 重整医嘱列表
        /// </summary>
        ArrayList alReDealRecord = new ArrayList();

        /// <summary>
        /// 是否查询医嘱标记
        /// </summary>
        bool bHaveSplitPage = false;

        Hashtable htLongSeq = new Hashtable();
        Hashtable htShortSeq = new Hashtable();
        Hashtable htSpecFrequency = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        private int PageLineNO = 21;

        /// <summary>
        /// 长嘱上边距
        /// </summary>
        private int printLongTop = 8;

        /// <summary>
        /// 长嘱左边距
        /// </summary>
        private int printLongLeft = 64;

        /// <summary>
        /// 临嘱上边距
        /// </summary>
        private int printShortTop = 4;

        /// <summary>
        /// 临嘱左边距
        /// </summary>
        private int printShortLeft = 60;

        /// <summary>
        /// 打印纸张设置XML
        /// </summary>
        private string orderPrintSetting = "orderPrintSetting.xml";

        /// <summary>
        /// 临嘱下边距
        /// </summary>
        private int printShortPageY = 55;

        /// <summary>
        /// 长嘱下边距
        /// </summary>
        private int printLongPageY = 65;

        /// <summary>
        /// 是否护嘱
        /// </summary>
        public bool isNurseOrder = false;

        /// <summary>
        /// 临嘱列数
        /// </summary>
        private int ShortColumnCount = Enum.GetNames(typeof(EnumShortOrderPrint)).Length;

        #endregion

        #region 患者信息

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo pInfo;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PInfo
        {
            get
            {
                return this.pInfo;
            }
            set
            {
                this.pInfo = value;
                this.SetTreeView();
            }
        }

        #endregion

        #region 函数

        /// <summary>
        /// 初始化打印机
        /// </summary>
        private void InitPrinter()
        {
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                //取打印机名
                string name = System.Text.RegularExpressions.Regex.Replace(PrinterSettings.InstalledPrinters[i], @"在(\s|\S)*上", "").Replace("自动", "");
                this.tbPrinter.Items.Add(name);
            }
            this.tbPrinter.SelectedIndexChanged += new EventHandler(cmbPrinter_SelectedIndexChanged);
            //从XML读默认打印机
            if (File.Exists(fileName))
            {
                XmlDocument file = new XmlDocument();
                file.Load(fileName);
                XmlNode node = file.SelectSingleNode("EMRPRINTER/医嘱单");
                if (node != null)
                {
                    this.printerName = node.InnerText;
                }
            }
            for (int i = 0; i < this.tbPrinter.Items.Count; i++)
            {
                if (this.tbPrinter.Items[i].ToString().Contains(this.printerName))
                {
                    this.tbPrinter.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 设置打印时选用打印机
        /// </summary>
        private void SetPrinter()
        {
            FS.FrameWork.Xml.XML xml = new FS.FrameWork.Xml.XML();
            if (!File.Exists(fileName))
            {
                if (this.tbPrinter.SelectedItem != null)
                {
                    XmlDocument doc = new XmlDocument();
                    xml.CreateRootElement(doc, "EMRPRINTER");
                    xml.AddXmlNode(doc, doc.DocumentElement, "医嘱单", this.tbPrinter.SelectedItem.ToString());
                    doc.Save(fileName);
                }
                else
                {
                    MessageBox.Show("请先选择需要的打印机！");
                }
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode node = doc.SelectSingleNode("EMRPRINTER/医嘱单");
                if (node != null)
                {
                    node.InnerText = this.printerName;
                }
                else
                {
                    if (this.tbPrinter.SelectedItem != null)
                    {
                        xml.AddXmlNode(doc, doc.DocumentElement, "医嘱单", this.tbPrinter.SelectedItem.ToString());
                    }
                }
                doc.Save(fileName);
            }


            if (!string.IsNullOrEmpty(this.printerName))
            {
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    if (PrinterSettings.InstalledPrinters[i] != null && PrinterSettings.InstalledPrinters[i].ToString().Contains(this.printerName))
                    {
                        p.PrintDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// 设置树形显示
        /// </summary>
        private void SetTreeView()
        {
            if (this.pInfo == null)
            {
                return;
            }

            if (this.treeView1.Nodes.Count > 0)
            {
                this.treeView1.Nodes.Clear();
            }

            TreeNode root = new TreeNode();

            root.Text = "住院信息:" + "[" + this.pInfo.Name + "]" + "[" + this.pInfo.PID.PatientNO + "]";

            this.treeView1.Nodes.Add(root);

            root.ImageIndex = 2;
            root.SelectedImageIndex = 2;
            TreeNode node = new TreeNode();

            node.Text = "[" + this.pInfo.PVisit.InTime.ToShortDateString() + "][" + this.pInfo.PVisit.PatientLocation.Dept.Name + "]";
            node.Tag = this.pInfo;

            node.ImageIndex = 1;
            node.SelectedImageIndex = 0;
            root.Nodes.Add(node);

            this.treeView1.ExpandAll();
        }

        /// <summary>
        /// 显示患者信息
        /// </summary>
        /// <param name="info"></param>
        private void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo info)
        {
            if (info == null)
            {
                info = new FS.HISFC.Models.RADT.PatientInfo();
            }
            this.pInfo = info;

            if (info != null)
            {
                this.lblv1.Text = info.Name;
                this.lbsv1.Text = info.Name;
                this.lblv2.Text = info.Sex.Name;
                this.lbsv2.Text = info.Sex.Name;
                this.lblv3.Text = this.orderTermManager.GetAge(info.Birthday);
                this.lbsv3.Text = this.lblv3.Text;
                this.lblv4.Text = info.PVisit.PatientLocation.Dept.Name;
                this.lbsv4.Text = info.PVisit.PatientLocation.Dept.Name;
                if (info.PVisit.PatientLocation.Bed.ID.Length >= 4)
                {
                    this.lblv5.Text = info.PVisit.PatientLocation.Bed.ID.Substring(4);
                    this.lbsv5.Text = info.PVisit.PatientLocation.Bed.ID.Substring(4);
                }
                this.lblv6.Text = info.PID.PatientNO;
                this.lbsv6.Text = info.PID.PatientNO;
            }

        }

        ///<summary>
        /// 李超峰6.20，处理中草药医嘱，一行打印三个，一组的最后打印一个总的用法用量
        /// </summary>
        /// <para name=al>要处理的医嘱列表</para>
        private void DealPCCOrder(ref ArrayList al)
        {
            //临时医嘱，按排序号降序排列


            //al.Sort(new AlSort());
            //遍历医嘱，如果有一项是草药，记录comboID，并添加到一个新的数组，直到组合遍历完成后，统一处理。
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order orderBill = al[i] as FS.HISFC.Models.Order.Inpatient.Order;
                if (orderBill.Item.SysClass.ID.ToString() == "PCC")
                {
                    //如果一个药品时中草药,向后遍历，找到同组的药，插入到临时数组中
                    string comboID = orderBill.Combo.ID;
                    string jyfs = string.Empty;//煎药方式
                    if (orderBill.Memo.Contains("自煎")) jyfs = "自煎";
                    else if (orderBill.Memo.Contains("代煎")) jyfs = "代煎";
                    else if (orderBill.Memo.Contains("复渣")) jyfs = "复渣";
                    else if (orderBill.Memo.Contains("代复渣")) jyfs = "代复渣";
                    else orderBill.Memo = " ";


                    //string flag = order.GetFlag;                   
                    ArrayList tempAl = new ArrayList();
                    for (int n = 0; n < al.Count - i; n++)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order tempBill = al[i + n] as FS.HISFC.Models.Order.Inpatient.Order;
                        if (tempBill.Combo.ID == comboID && tempBill.Item.SysClass.ID.ToString() == "PCC")
                        {
                            tempAl.Add(tempBill.Clone());
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (tempAl.Count <= 1) continue;//如果只有一条中药，不执行下面的操作
                    //跳出循环后，处理tempAl.
                    //处理医嘱备注，先煎、后下等

                    for (int m = 0; m < tempAl.Count; m++)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order tempBill = tempAl[m] as FS.HISFC.Models.Order.Inpatient.Order;

                        if (tempBill.Memo.Contains("先煎")) tempBill.Memo = "先煎";
                        else if (tempBill.Memo.Contains("后下")) tempBill.Memo = "后下";
                        else if (tempBill.Memo.Contains("另包")) tempBill.Memo = "另包";
                        else if (tempBill.Memo.Contains("另煎")) tempBill.Memo = "另煎";
                        else if (tempBill.Memo.Contains("焗服")) tempBill.Memo = "焗服";
                        else if (tempBill.Memo.Contains("冲服")) tempBill.Memo = "冲服";
                        else if (tempBill.Memo.Contains("布包煎")) tempBill.Memo = "布包煎";
                        else if (tempBill.Memo.Contains("烊化")) tempBill.Memo = "烊化";
                        else tempBill.Memo = "    ";

                    }
                    int count = tempAl.Count;
                    //加入最后一行，总用法,借用第二条医嘱的行号和页号保存最后一行总用法信息
                    FS.HISFC.Models.Order.Inpatient.Order tempOrderlast1 = tempAl[1] as FS.HISFC.Models.Order.Inpatient.Order;
                    FS.HISFC.Models.Order.Inpatient.Order tempOrderlast = tempOrderlast1.Clone();
                    tempOrderlast.Item.Name = " 共" + tempOrderlast.HerbalQty.ToString() + "剂" + "  " + tempOrderlast.Usage.Name + " " + tempOrderlast.Frequency.ToString() + " " + jyfs;
                    tempOrderlast.DoseOnce = 0;
                    tempOrderlast.DoseUnit = "";
                    //tempOrderlast.ID = (tempAl[0] as FS.HISFC.Models.Order.Inpatient.Order).ID;

                    //tempOrderlast.SortID = (tempAl[tempAl.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).SortID;
                    tempAl.Add(tempOrderlast);
                    int pccNumber = 0;
                    int rowNub = 0;
                    //for (int n = 0; n < tempAl.Count + rowNub-1; n++)
                    for (int n = 0; n + rowNub < tempAl.Count - 1; )
                    {
                        if (pccNumber < 3)
                        {
                            FS.HISFC.Models.Order.Inpatient.Order tempBill1 = tempAl[rowNub] as FS.HISFC.Models.Order.Inpatient.Order;
                            if (pccNumber == 0)
                            {
                                tempBill1.Item.Name = tempBill1.Item.Name + " " + tempBill1.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (tempBill1.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + tempBill1.Memo;
                                tempBill1.DoseUnit = "";
                                tempBill1.DoseOnce = 0;
                                n++;
                            }
                            else
                            {
                                if ((/*n - pccNumber +*/ n + rowNub) < tempAl.Count - 1)
                                {
                                    FS.HISFC.Models.Order.Inpatient.Order tempBill2 = tempAl[/*n - pccNumber +*/ n + rowNub] as FS.HISFC.Models.Order.Inpatient.Order;
                                    if (tempBill1.Item.ID == "999")
                                    {
                                        tempBill1.Item.Name += " " + tempBill2.Item.Name + " " + tempBill2.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + " " + tempBill2.Memo;
                                    }
                                    else
                                    {
                                        tempBill1.Item.Name += " " + tempBill2.Item.Name + " " + tempBill2.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (tempBill2.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + tempBill2.Memo;
                                    }
                                    tempAl.RemoveAt(/*n - pccNumber +*/ n + rowNub);
                                }
                                else
                                {
                                    break;
                                }

                            }
                            pccNumber++;

                            if (pccNumber == 3)
                            {
                                pccNumber = 0;
                                n = 0;// n - 2;
                                rowNub++;
                            }
                        }
                        //else
                        //{
                        //    pccNumber = 0;
                        //    n = n - 3;
                        //    rowNub++;
                        //}
                    }
                    //处理完tempal后，处理外面大循环，
                    for (int n = 0; n < count; n++)
                    {
                        al.RemoveAt(i);
                    }
                    for (int n = 0; n < tempAl.Count; n++)
                    {
                        al.Insert(i++, tempAl[n]);
                    }
                    i--;//多处理一位。所以要减掉

                }

            }
        }

        ///<summary>
        /// 李超峰--6.20，处理医嘱过长时的换行
        /// </summary>
        /// <param name="al">医嘱列表</param>
        private void SplitOrder(ref ArrayList al)
        {
            // int rowIncrease = 0;
            int maxLength = 100;
            for (int i = 0; i < al.Count; i++)
            {

                FS.HISFC.Models.Order.Inpatient.Order orderBill = al[i] as FS.HISFC.Models.Order.Inpatient.Order;
                //order.RowNO = order.RowNO + rowIncrease;
                string printContent = "";
                string tempstr = string.Empty;
                if (orderBill.MOTime.Date != orderBill.BeginTime.Date || orderBill.EndTime != DateTime.MinValue)
                {
                    if (orderBill.EndTime != DateTime.MinValue)
                    {
                        tempstr = orderBill.BeginTime.Month.ToString() + "." + orderBill.BeginTime.Day.ToString() + "起" + orderBill.EndTime.Month.ToString() + "." + orderBill.EndTime.Day.ToString() + "止";
                    }
                    else if (orderBill.EndTime == DateTime.MinValue)
                    {
                        tempstr = orderBill.BeginTime.Month.ToString() + "." + orderBill.BeginTime.Day.ToString() + "起";
                    }
                }
                switch (orderBill.Item.SysClass.ID.ToString())
                {
                    case "PCC":
                        printContent = orderBill.Item.Name + " " + orderBill.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (orderBill.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + orderBill.Usage.Name + " " + orderBill.Frequency.ID + " " + orderBill.Memo + tempstr;
                        break;
                    case "PCZ":
                        printContent = orderBill.Item.Name + " " + orderBill.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (orderBill.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + orderBill.Usage.Name + " " + orderBill.Frequency.ID + " " + orderBill.Memo + tempstr;
                        break;
                    case "P":
                        printContent = orderBill.Item.Name + " " + orderBill.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (orderBill.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + orderBill.Usage.Name + " " + orderBill.Frequency.ID + " " + orderBill.Memo + tempstr;
                        break;
                    default:
                        printContent = orderBill.Item.Name + " " + orderBill.Frequency.Name + " " + orderBill.Memo + tempstr;
                        break;
                }


                if (printContent.Length > maxLength)
                {

                    //计算分行点,字母占0.5 ，汉字是1，每行打印36个；
                    float temp = 0;
                    int splitNumber = 0;
                    for (int j = 0; j < printContent.Length; j++)
                    {
                        if (printContent[j] > 0 && printContent[j] < 255)
                        {
                            temp += 0.5F;
                        }
                        else
                        {
                            temp += 1F;
                        }
                        if (temp >= maxLength)
                        {
                            splitNumber = j;
                            break;
                        }
                    }

                    if (splitNumber > 0)
                    {
                        orderBill.Item.Name = printContent.Substring(0, splitNumber);
                        orderBill.DoseOnce = 0;
                        orderBill.Qty = 0;
                        orderBill.Unit = "";
                        orderBill.DoseUnit = "";
                        orderBill.Usage.Name = "";
                        orderBill.Frequency.ID = "";
                        orderBill.Memo = "";

                        FS.HISFC.Models.Order.Inpatient.Order tempBill = orderBill.Clone();
                        tempBill.Item.Name = printContent.Substring(splitNumber, printContent.Length - splitNumber);
                        // rowIncrease++;
                        tempBill.RowNo = tempBill.RowNo + 1;
                        al.RemoveAt(i);
                        al.Insert(i, orderBill);
                        al.Insert((i + 1), tempBill);
                    }

                }
            }
        }

        /// <summary>
        /// 查询患者医嘱信息
        /// </summary>
        private void QueryPatientOrder(bool success)
        {
            if (this.bHaveSplitPage)
            {
                //处理转科医嘱状态 不刷新
                RefreshColor(success);
                return;
            }
            else
            {
                try
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询显示医嘱信息,请稍候......");

                    Application.DoEvents();

                    ArrayList alAll = new ArrayList();

                    //alAll = this.orderManager.QueryPrnOrder(this.PInfo.ID, DateTime.MinValue, DateTime.MaxValue, this.isNurseOrder ? "9" : "");
                    alAll = this.orderManager.QueryPrnOrder(this.PInfo.ID);


                    if (alAll == null)
                    {
                        MessageBox.Show(this.orderManager.Err);
                        return;
                    }

                    alLong.Clear();
                    alShort.Clear();

                    foreach (FS.HISFC.Models.Order.Inpatient.Order orderBill in alAll)
                    {
                        if (orderBill.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                        {
                            alLong.Add(orderBill);
                        }
                        else
                        {
                            alShort.Add(orderBill);
                        }
                    }

                    //lichaofeng add 6-20
                    this.SplitOrder(ref alLong);

                    this.DealOrderSeq(alLong, EnumOrderType.Long);

                    this.AddObjectToFpLong(alLong);



                    this.DealOrderSeq(alShort, EnumOrderType.Short);

                    //lichaofeng add 6-20 中草药分三行打印
                    this.DealPCCOrder(ref alShort);

                    this.AddObjectToFpShort(alShort);

                    this.SetPatientInfo(this.pInfo);

                    this.lblPageLong.Visible = true;

                    this.lblPageShort.Visible = true;

                    this.GetDeptAndBed(EnumOrderType.All);

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查询医嘱出错！请退出重新操作" + ex.Message);

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }
        }

        /// <summary>
        /// 动态获得患者科室和床号
        /// </summary>
        /// <param name="orderType"></param>
        private void GetDeptAndBed(EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Long)
            {
                if (this.neuSpread1.ActiveSheet.Rows[0].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order orderObj = this.neuSpread1.ActiveSheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (orderObj == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }
                    if (!string.IsNullOrEmpty(orderObj.Patient.PVisit.PatientLocation.Bed.ID))
                    {
                        //长嘱科室
                        this.lblv4.Text = this.conMgr.GetDepartment(orderObj.Patient.PVisit.PatientLocation.Dept.ID).Name;

                        this.lblv3.Text = this.orderBillMgr.GetAge(pInfo.Birthday, orderObj.MOTime);
                        //长嘱床号
                        this.lblv5.Text = orderObj.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                    }

                }
            }
            else if (orderType == EnumOrderType.Short)
            {
                if (this.neuSpread2.ActiveSheet.Rows[0].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order orderObj = this.neuSpread2.ActiveSheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (orderObj == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }
                    if (!string.IsNullOrEmpty(orderObj.Patient.PVisit.PatientLocation.Bed.ID))
                    {
                        //临嘱科室
                        this.lbsv4.Text = this.conMgr.GetDepartment(orderObj.Patient.PVisit.PatientLocation.Dept.ID).Name;
                        this.lbsv3.Text = this.orderBillMgr.GetAge(this.pInfo.Birthday, orderObj.MOTime);
                        //临嘱床号
                        this.lbsv5.Text = orderObj.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                    }
                }
            }
            else
            {
                this.GetDeptAndBed(EnumOrderType.Long);
                this.GetDeptAndBed(EnumOrderType.Short);
            }
        }

        /// <summary>
        /// 刷新打印后的背景色
        /// </summary>
        /// <param name="success"></param>
        private void RefreshColor(bool success)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null && success)
                    {
                        this.neuSpread1.ActiveSheet.Rows[i].BackColor = Color.MistyRose;

                        FS.HISFC.Models.Order.Inpatient.Order o = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginDate, o.BeginTime.Month.ToString() + "-" + o.BeginTime.Day.ToString());
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginTime, o.BeginTime.ToShortTimeString());
                        //this.neuSpread1.ActiveSheet.SetValue(i, 2, o.Doctor.Name);
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DrawCombo, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.ComboID, o.Combo.ID);
                        if (o.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            // FS.HISFC.Models.Pharmacy.Item phaItem = helper.GetObjectFromID(o.Item.ID) as FS.HISFC.Models.Pharmacy.Item;                           
                            //if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            //{
                            //为了统一开立和打印内容
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.Memo);

                            //this.neuSpread2.ActiveSheet.SetValue(i, 5, phaItem.NameCollection.RegularName + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.Memo);
                            //}
                            //else
                            //{
                            //    this.neuSpread2.ActiveSheet.SetValue(i, 5, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.Memo);
                            //}
                        }
                        else
                        {
                            //检验
                            if (o.Item.SysClass.ID.ToString() == "UL")
                            {
                                this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.OrderName, o.Item.Name + " " + o.Frequency.ID + " " + o.Memo);
                            }
                            else
                            {
                                this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.OrderName, o.Item.Name + " " + o.Frequency.ID + " " + o.Qty.ToString() + " " + o.Unit + " " + o.Memo);
                            }
                        }
                        if (o.DCOper.OperTime != DateTime.MinValue)
                        {
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndDate, o.DCOper.OperTime.Month.ToString() + "-" + o.DCOper.OperTime.Day.ToString());
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndTime, o.DCOper.OperTime.ToShortTimeString());
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCDoctorName, o.DCOper.Name);
                            string dcConfirmOper = this.orderManager.ExecSqlReturnOne(string.Format(execDCConfirmOper, o.ID));
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCExecNurseName, dcConfirmOper);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
                {
                    if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null && success)
                    {
                        this.neuSpread2.ActiveSheet.Rows[i].BackColor = Color.MistyRose;

                        FS.HISFC.Models.Order.Inpatient.Order o = this.neuSpread2.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Date, o.BeginTime.Month.ToString() + "-" + o.BeginTime.Day.ToString());
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Time, o.BeginTime.ToShortTimeString());
                        this.neuSpread2.ActiveSheet.SetValue(i, 2, o.ReciptDoctor.Name);
                        this.neuSpread2.ActiveSheet.SetValue(i, 5, o.Nurse.Name);

                        if (o.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = helper.GetObjectFromID(o.Item.ID) as FS.HISFC.Models.Pharmacy.Item;
                            //if (phaItem != null && phaItem.NameCollection.RegularName != null && phaItem.NameCollection.RegularName != "")
                            //{
                            if (o.OrderType.ID == "CD")
                            {
                                this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Item.Specs + " " + o.Qty.ToString() + o.Unit + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + "(出院带药)" + " " + o.Memo);

                                //this.neuSpread1.ActiveSheet.SetValue(i, 4, phaItem.NameCollection.RegularName + " " + phaItem.Specs + " " + o.Qty.ToString() + o.Unit + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + "(出院带药)" + " " + o.Memo);
                            }
                            else if (o.OrderType.ID == "BL")
                            {
                                this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.Qty.ToString() + o.Unit + "(补录医嘱)" + " " + o.Memo);

                                // this.neuSpread1.ActiveSheet.SetValue(i, 4, phaItem.NameCollection.RegularName + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.Qty.ToString() + o.Unit + "(补录医嘱)" + " " + o.Memo);
                            }
                            else
                            {
                                this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + /* o.Qty.ToString() + o.Unit + " " +*/ o.Memo);

                                //this.neuSpread1.ActiveSheet.SetValue(i, 4, phaItem.NameCollection.RegularName + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + /* o.Qty.ToString() + o.Unit + " " +*/ o.Memo);
                            }
                            //}
                            //else
                            //{
                            //    if (oType.ID == "CD")
                            //    {
                            //        this.neuSpread1.ActiveSheet.SetValue(i, 4, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.Qty.ToString() + o.Unit + "(出院带药)" + " " + o.Memo);
                            //    }
                            //    else if (oType.ID == "BL")
                            //    {
                            //        this.neuSpread1.ActiveSheet.SetValue(i, 4, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + o.Qty.ToString() + o.Unit + "(补录医嘱)" + " " + o.Memo);
                            //    }
                            //    else
                            //    {
                            //        this.neuSpread1.ActiveSheet.SetValue(i, 4, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + o.Frequency.ID + " " + /*o.Qty.ToString() + o.Unit + " " +*/ o.Memo);
                            //    }
                            //}
                        }
                        else
                        {
                            //检查、检验
                            if (o.Item.SysClass.ID.ToString() == "UC" || o.Item.SysClass.ID.ToString() == "UL")
                            {
                                this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Item.Qty.ToString() + o.Unit + " " + o.Memo + " " + o.Sample.Name + " " + GetEmergencyTip(o.IsEmergency));
                            }
                            //手术
                            else if (o.Item.SysClass.ID.ToString() == "UO")
                            {
                                this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Memo);
                            }
                            else
                            {
                                this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Qty.ToString() + o.Unit + " " + o.Memo);
                            }

                        }
                        if (o.ConfirmTime != DateTime.MinValue)
                        {
                            //this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecTime, "");
                            //this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, o.ConfirmTime.ToShortTimeString());
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 显示页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread2_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.lblPageShort.Text = (FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag) + 1).ToString();

                if (this.neuSpread2.ActiveSheet.Rows.Count > 0 && this.neuSpread2.ActiveSheet.Rows[0].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oTemp = this.neuSpread2.ActiveSheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    //临嘱科室
                    this.lbsv4.Text = this.conMgr.GetDepartment(oTemp.Patient.PVisit.PatientLocation.Dept.ID).Name;

                    if (oTemp != null && oTemp.Patient.PVisit.PatientLocation.Bed.ID != null && oTemp.Patient.PVisit.PatientLocation.Bed.ID.Length >= 4)
                    {
                        this.lbsv5.Text = oTemp.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                    }
                }
                this.GetDeptAndBed(EnumOrderType.Short);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 显示页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.lblPageLong.Text = (FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag) + 1).ToString();

                if (this.neuSpread1.ActiveSheet.Rows.Count > 0 && this.neuSpread1.ActiveSheet.Rows[0].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oTemp = this.neuSpread1.ActiveSheet.Rows[0].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    //长嘱科室
                    this.lblv4.Text = this.conMgr.GetDepartment(oTemp.Patient.PVisit.PatientLocation.Dept.ID).Name;

                    if (oTemp != null && oTemp.Patient.PVisit.PatientLocation.Bed.ID != null && oTemp.Patient.PVisit.PatientLocation.Bed.ID.Length >= 4)
                    {
                        this.lbsv5.Text = oTemp.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                    }
                }
                this.GetDeptAndBed(EnumOrderType.Short);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获得最小时间和最大时间
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="bReprint"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        private void GetBeginAndEndTime(FarPoint.Win.Spread.SheetView sheet, bool bReprint, ref string dtBegin, ref string dtEnd)
        {
            if (bReprint)
            {
                for (int i = 0; i < sheet.Rows.Count; i++)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ord = sheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (ord != null)
                    {
                        if (i == 0)
                        {
                            dtBegin = ord.BeginTime.ToString();
                            dtEnd = ord.BeginTime.ToString();
                        }
                        else
                        {
                            dtEnd = ord.BeginTime.ToString();
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < sheet.Rows.Count; i++)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ord = sheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (ord != null && ord != null)
                    {
                        if (i == 0)
                        {
                            dtBegin = ord.BeginTime.ToString();
                        }

                        if (ord.RowNo >= 0 && ord.PageNo >= 0)
                        {
                            dtBegin = ord.BeginTime.ToString();
                            dtEnd = ord.BeginTime.ToString();
                        }
                        else
                        {
                            dtEnd = ord.BeginTime.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获得加急状态
        /// </summary>
        /// <param name="isEmr"></param>
        /// <returns></returns>
        private string GetEmergencyTip(bool isEmr)
        {
            if (isEmr)
            {
                return "加急";
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region 事件

        private void cmbPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.printerName = tbPrinter.SelectedItem.ToString();
            SetPrinter();
        }

        /// <summary>
        /// LOAD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrderPrint_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.tbQuery.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C查询);
                this.tbRePrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C重打);
                this.tbPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印);
                this.tbExit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T退出);
                this.toolStripButton1.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S设置);
                this.treeView1.ImageList = this.treeView1.deptImageList;
                this.tabControl1.ImageList = this.treeView1.groupImageList;
                this.tabControl1.TabPages[0].ImageIndex = 2;
                this.tabControl1.TabPages[1].ImageIndex = 3;
                this.tbReset.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

                //初始化打印机
                InitPrinter();

                //打印未打印页
                this.tbPrintNext.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
                //打印全部
                this.tbPrintAll.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

                emplHelPer.ArrayObject = this.conMgr.QueryEmployeeAll();

                //this.lblLongYear.Text = this.orderBillMgr.GetDateTimeFromSysDateTime().Date.Year.ToString();
                //this.lblShortYear.Text = this.orderBillMgr.GetDateTimeFromSysDateTime().Date.Year.ToString();
                ArrayList alSpecFrequency = this.conMgr.QueryConstantList("FrequeryMemo");
                if (alSpecFrequency == null)
                {
                    MessageBox.Show("查找特殊频次出错!" + this.conMgr.Err);
                    return;
                }

                foreach (FS.FrameWork.Models.NeuObject objInfo in alSpecFrequency)
                {
                    this.htSpecFrequency.Add(objInfo.ID, objInfo.Name);
                }

                #region 创建配置文件

                if (System.IO.Directory.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath) == false)
                {
                    System.IO.Directory.CreateDirectory(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath);
                }

                if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting) == false)
                {
                    try
                    {
                        System.IO.FileStream f = System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting);
                        f.Close();
                        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                        System.Xml.XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                        xmlDoc.AppendChild(xmlDeclaration);
                        System.Xml.XmlElement xmlElementParent = xmlDoc.CreateElement("OrderPrintSetting");

                        System.Xml.XmlElement xmlElementLong = xmlDoc.CreateElement("LongOrderPrint");
                        System.Xml.XmlAttribute attribute = xmlDoc.CreateAttribute("Top");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printLongLeft) / 40).ToString();
                        xmlElementLong.Attributes.Append(attribute);
                        attribute = xmlDoc.CreateAttribute("Left");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printLongTop) / 40).ToString();
                        xmlElementLong.Attributes.Append(attribute);
                        attribute = xmlDoc.CreateAttribute("PageY");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printLongPageY) / 40).ToString();
                        xmlElementLong.Attributes.Append(attribute);

                        System.Xml.XmlElement xmlElementShort = xmlDoc.CreateElement("ShortOrderPrint");
                        attribute = xmlDoc.CreateAttribute("Top");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printShortLeft) / 40).ToString();
                        xmlElementShort.Attributes.Append(attribute);
                        attribute = xmlDoc.CreateAttribute("Left");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printShortTop) / 40).ToString();
                        xmlElementShort.Attributes.Append(attribute);
                        attribute = xmlDoc.CreateAttribute("PageY");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printShortPageY) / 40).ToString();
                        xmlElementShort.Attributes.Append(attribute);

                        xmlElementParent.AppendChild(xmlElementLong);
                        xmlElementParent.AppendChild(xmlElementShort);
                        xmlDoc.AppendChild(xmlElementParent);


                        System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting, Encoding.UTF8);
                        xmlWriter.Formatting = System.Xml.Formatting.Indented;
                        xmlWriter.IndentChar = '\t';
                        xmlDoc.WriteContentTo(xmlWriter);
                        xmlWriter.Close();
                    }
                    catch (Exception ex)
                    {
                        System.IO.File.Delete(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting);
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                #endregion

                //取配置文件
                try
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.Load(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting);
                    if (doc.SelectSingleNode("OrderPrintSetting//LongOrderPrintNew") == null)
                    {
                        decimal printLongTop1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//LongOrderPrint").Attributes["Top"].Value);
                        decimal printLongLeft1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//LongOrderPrint").Attributes["Left"].Value);
                        decimal printLongPageY1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//LongOrderPrint").Attributes["PageY"].Value);
                        decimal printShortTop1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//ShortOrderPrint").Attributes["Top"].Value);
                        decimal printShortLeft1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//ShortOrderPrint").Attributes["Left"].Value);
                        decimal printShortPageY1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//ShortOrderPrint").Attributes["PageY"].Value);

                        this.printLongTop = FS.FrameWork.Function.NConvert.ToInt32(printLongTop1 * 40);
                        this.printLongLeft = FS.FrameWork.Function.NConvert.ToInt32(printLongLeft1 * 40);
                        this.printLongPageY = FS.FrameWork.Function.NConvert.ToInt32(printLongPageY1 * 40);
                        this.printShortTop = FS.FrameWork.Function.NConvert.ToInt32(printShortTop1 * 40);
                        this.printShortLeft = FS.FrameWork.Function.NConvert.ToInt32(printShortLeft1 * 40);
                        this.printShortPageY = FS.FrameWork.Function.NConvert.ToInt32(printShortPageY1 * 40);


                        //创建并保存
                        System.Xml.XmlNode xmlElementParent = doc.SelectSingleNode("OrderPrintSetting");
                        System.Xml.XmlElement xmlElementLong = doc.CreateElement("LongOrderPrintNew");
                        System.Xml.XmlAttribute attribute = doc.CreateAttribute("Top");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printLongTop) / 40).ToString();
                        xmlElementLong.Attributes.Append(attribute);
                        attribute = doc.CreateAttribute("Left");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printLongLeft) / 40).ToString();
                        xmlElementLong.Attributes.Append(attribute);
                        attribute = doc.CreateAttribute("PageY");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printLongPageY) / 40).ToString();
                        xmlElementLong.Attributes.Append(attribute);

                        System.Xml.XmlElement xmlElementShort = doc.CreateElement("ShortOrderPrintNew");
                        attribute = doc.CreateAttribute("Top");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printShortTop) / 40).ToString();
                        xmlElementShort.Attributes.Append(attribute);
                        attribute = doc.CreateAttribute("Left");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printShortLeft) / 40).ToString();
                        xmlElementShort.Attributes.Append(attribute);
                        attribute = doc.CreateAttribute("PageY");
                        attribute.Value = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Function.NConvert.ToDecimal(this.printShortPageY) / 40).ToString();
                        xmlElementShort.Attributes.Append(attribute);

                        xmlElementParent.AppendChild(xmlElementLong);
                        xmlElementParent.AppendChild(xmlElementShort);
                        doc.AppendChild(xmlElementParent);

                        System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting, Encoding.UTF8);
                        xmlWriter.Formatting = System.Xml.Formatting.Indented;
                        xmlWriter.IndentChar = '\t';
                        doc.WriteContentTo(xmlWriter);
                        xmlWriter.Close();
                    }
                    else
                    {
                        decimal printLongTop1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//LongOrderPrintNew").Attributes["Top"].Value);
                        decimal printLongLeft1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//LongOrderPrintNew").Attributes["Left"].Value);
                        decimal printLongPageY1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//LongOrderPrintNew").Attributes["PageY"].Value);
                        decimal printShortTop1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//ShortOrderPrintNew").Attributes["Top"].Value);
                        decimal printShortLeft1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//ShortOrderPrintNew").Attributes["Left"].Value);
                        decimal printShortPageY1 = FS.FrameWork.Function.NConvert.ToDecimal(doc.SelectSingleNode("OrderPrintSetting//ShortOrderPrintNew").Attributes["PageY"].Value);

                        this.printLongTop = FS.FrameWork.Function.NConvert.ToInt32(printLongTop1 * 40);
                        this.printLongLeft = FS.FrameWork.Function.NConvert.ToInt32(printLongLeft1 * 40);
                        this.printLongPageY = FS.FrameWork.Function.NConvert.ToInt32(printLongPageY1 * 40);
                        this.printShortTop = FS.FrameWork.Function.NConvert.ToInt32(printShortTop1 * 40);
                        this.printShortLeft = FS.FrameWork.Function.NConvert.ToInt32(printShortLeft1 * 40);
                        this.printShortPageY = FS.FrameWork.Function.NConvert.ToInt32(printShortPageY1 * 40);
                    }

                    if (this.treeView1.Nodes.Count > 0 && this.treeView1.Nodes[0].Nodes.Count > 0)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0].Nodes[0];
                    }
                }
                catch (Exception ex)
                {
                    System.IO.File.Delete(FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + this.orderPrintSetting);
                    MessageBox.Show(ex.Message);
                    return;
                }

                /* ************************************************************************
                 * 此段程序为原医嘱打印的程序，要查所有的药品；但是药品信息在医嘱打印的时候并没有使用，所以不查询以提高性能
                 //  System.Collections.Generic.List<FS.HISFC.Models.Pharmacy.Item> alList = this.myItem.QueryItemList();
               //  ArrayList alPha = new ArrayList(alList.ToArray());
               //  this.helper.ArrayObject = alPha;
                 * **************************************************************************/
                if (this.MdiParent != null)
                {
                    foreach (Form f in this.MdiParent.MdiChildren)
                    {
                        f.WindowState = FormWindowState.Maximized;
                    }

                    this.MdiParent.Refresh();
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                FS.FrameWork.Models.NeuObject obj = e.Node.Tag as FS.FrameWork.Models.NeuObject;

                pInfo = this.radtIntegrate.QueryPatientInfoByInpatientNO(obj.ID);

                QueryOrderPrint(pInfo);
            }
        }

        /// <summary>
        /// 医嘱单上患者基本信息赋值
        /// </summary>
        /// <param name="temp"></param>
        private void QueryOrderPrint(FS.HISFC.Models.RADT.PatientInfo temp)
        {
            if (temp != null)
            {
                this.lblv1.Text = temp.Name;
                this.lbsv1.Text = temp.Name;
                this.lblv2.Text = temp.Sex.Name;
                this.lbsv2.Text = temp.Sex.Name;
                this.lblv3.Text = this.orderBillMgr.GetAge(temp.Birthday);
                this.lbsv3.Text = this.orderBillMgr.GetAge(temp.Birthday);
                this.lblv4.Text = temp.PVisit.PatientLocation.Dept.Name;
                this.lbsv4.Text = temp.PVisit.PatientLocation.Dept.Name;
                if (temp.PVisit.PatientLocation.Bed.ID.Length >= 4)
                {
                    this.lblv5.Text = temp.PVisit.PatientLocation.Bed.ID.Substring(4);
                    this.lbsv5.Text = temp.PVisit.PatientLocation.Bed.ID.Substring(4);
                }
                this.lblv6.Text = temp.PID.PatientNO;
                this.lbsv6.Text = temp.PID.PatientNO;
                this.pInfo = temp;
                this.QueryPatientOrder(true);
            }
        }

        /// <summary>
        /// 住院号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        /// <summary>
        /// 工具栏按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //续打
            if (e.ClickedItem == this.tbPrint)
            {
                this.Print();
            }
            //重打
            else if (e.ClickedItem == this.tbRePrint)
            {
                this.PrintAgain();
            }
            //重置所有
            else if (e.ClickedItem == this.ResetAll)
            {
                this.ReSet(EnumOrderType.All);
            }
            //重置临嘱
            else if (e.ClickedItem == this.ResetLong)
            {
                this.ReSet(EnumOrderType.Long);
            }
            //重置长嘱
            else if (e.ClickedItem == this.ResetShort)
            {
                this.ReSet(EnumOrderType.Short);
            }
            //退出
            else if (e.ClickedItem == this.tbExit)
            {
                this.Close();
            }
            //打印当前页
            else if (e.ClickedItem == this.tbPrintActivePage)
            {
                this.PrintCurentPage();
            }
            #region 打印未打印页

            //打印未打印临嘱
            else if (e.ClickedItem == this.tbPrintNextShort)
            {
                PrintNext(EnumOrderType.Short);
            }
            //打印未打印长嘱
            else if (e.ClickedItem == this.tbPrintNextLong)
            {
                PrintNext(EnumOrderType.Long);
            }
            //打印所有未打印页
            else if (e.ClickedItem == this.tbPrintNextAll)
            {
                PrintNext(EnumOrderType.All);
            }
            #endregion

            #region 打印全部
            //打印全部临嘱
            else if (e.ClickedItem == this.tbPrintAllShort)
            {
                this.PrintAll(EnumOrderType.Short);
            }
            //打印全部长嘱
            else if (e.ClickedItem == this.tbPrintAllLong)
            {
                this.PrintAll(EnumOrderType.Long);
            }
            //打印全部医嘱
            else if (e.ClickedItem == this.tbPrintAllIn)
            {
                this.PrintAll(EnumOrderType.All);
            }
            #endregion
        }

        /// <summary>
        /// 重置当前患者医嘱单打印状态
        /// </summary>
        private void ReSet(EnumOrderType type)
        {
            if (this.pInfo == null || this.pInfo.ID == "")
            {
                return;
            }

            string message = "医嘱单";
            string orderType = "ALL";
            switch (type)
            {
                case EnumOrderType.Long:
                    message = "长期医嘱单";
                    orderType = "1";
                    break;
                case EnumOrderType.Short:
                    message = "临时医嘱单";
                    orderType = "0";
                    break;
                default:
                    break;
            }
            DialogResult rr = MessageBox.Show("重置将取" + message + "打印状态，无法继续上次打印，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rr == DialogResult.No)
            {
                return;
            }

            if (this.orderBillMgr.ResetOrderPrint("-1", "-1", pInfo.ID, orderType, "0") == -1)
            {
                MessageBox.Show("重置失败!" + this.orderManager.Err);
                return;
            }

            for (int sheetIndex = 0; sheetIndex < this.neuSpread1.Sheets.Count; sheetIndex++)
            {
                this.neuSpread1.Sheets[sheetIndex].RowCount = 0;
                this.neuSpread1.Sheets[sheetIndex].RowCount = 21;
            }
            for (int sheetIndex = 0; sheetIndex < this.neuSpread2.Sheets.Count; sheetIndex++)
            {
                this.neuSpread2.Sheets[sheetIndex].RowCount = 0;
                this.neuSpread2.Sheets[sheetIndex].RowCount = 22;
            }

            this.QueryPatientOrder(true);
            this.QueryOrderPrint(this.pInfo);
            MessageBox.Show("重置成功!");

        }

        /// <summary>
        /// 临嘱右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                this.menu.MenuItems.Clear();

                System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                printMenuItem.Text = "补打该条临时医嘱";
                printMenuItem.Click += new EventHandler(printMenuItem_Click);

                System.Windows.Forms.MenuItem splitShortMenuItem = new MenuItem();
                splitShortMenuItem.Text = "从该条医嘱往后另起一页";
                splitShortMenuItem.Click += new EventHandler(splitShortMenuItem_Click);

                System.Windows.Forms.MenuItem ChangeShortOrderState = new MenuItem();
                ChangeShortOrderState.Text = "设置医嘱为已打印";
                ChangeShortOrderState.Click += new EventHandler(ChangeShortOrderState_Click);

                if ((this.orderBillMgr.Operator as FS.HISFC.Models.Base.Employee).IsManager)
                {
                    System.Windows.Forms.MenuItem AddBlanketForShort = new MenuItem();
                    AddBlanketForShort.Text = "在该条医嘱上方增加一空行";
                    AddBlanketForShort.Click += new EventHandler(AddBlanketForShort_Click);

                    this.menu.MenuItems.Add(AddBlanketForShort);
                }


                this.menu.MenuItems.Add(printMenuItem);
                this.menu.MenuItems.Add(splitShortMenuItem);
                //this.menu.MenuItems.Add(ChangeShortOrderState);
                this.menu.Show(this.neuSpread2, new Point(e.X, e.Y));
            }
        }

        ///<summary>
        ///临时医嘱，增加空行
        ///</summary>
        ///<para> </para>
        void AddBlanketForShort_Click(object sender, EventArgs e)
        {
            //---------对于未打印的医嘱，在选定行下面增加一新行-----------//
            //获得所有页，如果最后一页已经25条，新起一页，下移一行。
            //如果最后页大于当前页，所有下移一行，上一页的最后一行插入本页的第一行，
            //如果当前页是最后一页，下移一行
            int insertRowNumber = 0;
            int insertRowPage = this.neuSpread2.ActiveSheetIndex;
            int currentPage = 0;

            FS.HISFC.Models.Order.Inpatient.Order ot1 = this.neuSpread2.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.Order;
            if (ot1 == null || ot1.GetFlag != "0")
            {
                MessageBox.Show("只能在未打印的医嘱上方增加空行");
                return;
            }
            insertRowNumber = this.neuSpread2.ActiveSheet.ActiveRowIndex;

            //找最后一个不为空的医嘱
            for (int i = this.PageLineNO; i >= 0; i--)
            {
                if (this.neuSpread2.Sheets[this.neuSpread2.Sheets.Count - 1].Rows[i].Tag != null)
                {
                    if (i == this.PageLineNO)
                    {
                        //插入新表单
                        FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                        this.InitShortSheet(ref sheet);
                        int pagenubmer = (this.neuSpread2.Sheets.Count + 1);
                        sheet.Tag = pagenubmer;
                        sheet.SheetName = "第" + pagenubmer.ToString() + "页";
                        this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, sheet);
                    }
                    break;
                }
            }
            currentPage = this.neuSpread2.Sheets.Count - 1;
            while (currentPage > insertRowPage)
            {
                for (int i = this.PageLineNO; i > 0; i--)
                {
                    for (int j = 0; j < ShortColumnCount; j++)
                    {
                        string temp = this.neuSpread2.Sheets[currentPage].Cells[i - 1, j].Text;
                        this.neuSpread2.Sheets[currentPage].SetValue(i, j, temp);
                    }
                    //修改tag值
                    FS.HISFC.Models.Order.Inpatient.Order ot = this.neuSpread2.Sheets[currentPage].Rows[i - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (ot == null)
                    {
                        this.neuSpread2.Sheets[currentPage].Rows[i].Tag = null;
                    }
                    else
                    {
                        this.neuSpread2.Sheets[currentPage].Rows[i].Tag = ot.Clone();
                    }
                }
                //处理前一页最后一条医嘱和当前页第一条医嘱
                for (int j = 0; j < ShortColumnCount; j++)
                {
                    string temp = this.neuSpread2.Sheets[currentPage - 1].Cells[this.PageLineNO, j].Text;
                    this.neuSpread2.Sheets[currentPage].SetValue(0, j, temp);
                }
                FS.HISFC.Models.Order.Inpatient.Order ot2 = this.neuSpread2.Sheets[currentPage - 1].Rows[this.PageLineNO].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (ot2 == null)
                {
                    this.neuSpread2.Sheets[currentPage].Rows[0].Tag = null;
                }
                else
                {
                    this.neuSpread2.Sheets[currentPage].Rows[0].Tag = ot2.Clone();
                }
                currentPage--;
            }
            for (int i = this.PageLineNO; i > insertRowNumber; i--)
            {
                for (int j = 0; j < ShortColumnCount; j++)
                {
                    string temp = this.neuSpread2.Sheets[currentPage].Cells[i - 1, j].Text;
                    this.neuSpread2.Sheets[currentPage].SetValue(i, j, temp);
                }
                FS.HISFC.Models.Order.Inpatient.Order ot = this.neuSpread2.Sheets[currentPage].Rows[i - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (ot == null)
                {
                    this.neuSpread2.Sheets[currentPage].Rows[i].Tag = null;
                }
                else
                {
                    this.neuSpread2.Sheets[currentPage].Rows[i].Tag = ot.Clone();
                }
            }
            for (int j = 0; j < ShortColumnCount; j++)
            {
                this.neuSpread2.Sheets[currentPage].SetValue(insertRowNumber, j, "");
            }
            this.neuSpread2.Sheets[currentPage].Rows[insertRowNumber].Tag = null;
        }

        ///<summary>
        /// 临时医嘱更改标志位
        /// </summary>
        /// <para >al ,医嘱列表</para>
        void ChangeShortOrderState_Click(object sender, EventArgs e)
        {
            DialogResult rr = MessageBox.Show(this, "确实要更新所选医嘱的打印标志么，此操作不能恢复！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (rr == DialogResult.No)
            {
                return;
            }

            FS.HISFC.BizLogic.Order.Order myOrder = new FS.HISFC.BizLogic.Order.Order();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            myOrder.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //对于选定的医嘱，更新标志位
            string orderID = " ";
            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }
            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.IsSelected(i, 0) && this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    if (oT.Patient.ID != this.pInfo.ID)
                    {
                        continue;
                    }

                    if (orderID.Contains(oT.ID))
                    {
                        continue;
                    }
                    orderID = orderID + " " + oT.ID;


                    if (oT.GetFlag == "0")
                    {
                        //if (myOrder.UpdatePageNOAndRowNO(this.pInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        oT.RowNo = i;
                        oT.PageNo = pageNo;

                        this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        if (this.orderManager.UpdatePageNoAndRowNo(this.PInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新页码出错！" + oT.Item.Name);
                            return;
                        }
                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            oT.GetFlag = "2";
                        }
                        else
                        {
                            oT.GetFlag = "1";
                        }
                        if (this.orderManager.UpdateGetFlag(this.PInfo.ID, oT.ID, oT.GetFlag, "0") <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新提取标志出错！" + oT.Item.Name + ",可能该项目已经打印！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("所选医嘱已打印");
                        return;
                    }

                }

            }
            FS.FrameWork.Management.PublicTrans.Commit();

            this.SetValueVisible(true, EnumOrderType.Long);

            this.SetHeaderVisible(true, EnumOrderType.Long);

            this.QueryPatientOrder(false);

        }

        /// <summary>
        /// 临嘱另起一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void splitShortMenuItem_Click(object sender, EventArgs e)
        {
            this.AddObjectToFpShortAfter();
        }

        /// <summary>
        /// 长嘱右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.menu.MenuItems.Clear();
                if (this.neuSpread1.ActiveSheet.SelectionCount == 1)
                {
                    System.Windows.Forms.MenuItem printMenuItem = new MenuItem();
                    printMenuItem.Text = "补打该条长期医嘱";
                    printMenuItem.Click += new EventHandler(printMenuItem_Click);

                    System.Windows.Forms.MenuItem printDateItem = new MenuItem();
                    printDateItem.Text = "只补打该条长期医嘱停止时间";
                    printDateItem.Click += new EventHandler(printDateItem_Click);

                    System.Windows.Forms.MenuItem splitMenuItem = new MenuItem();
                    splitMenuItem.Text = "从该条医嘱往后另起一页";
                    splitMenuItem.Click += new EventHandler(splitMenuItem_Click);

                    //lichaofeng 2008.7.10号添加
                    System.Windows.Forms.MenuItem ChangePrintState = new MenuItem();
                    ChangePrintState.Text = "更新选定医嘱为已打印";
                    ChangePrintState.Click += new EventHandler(ChangePrintState_Click);

                    if ((this.orderBillMgr.Operator as FS.HISFC.Models.Base.Employee).IsManager)
                    {
                        System.Windows.Forms.MenuItem AddBlanket = new MenuItem();
                        AddBlanket.Text = "在该条医嘱上方增加一空行";
                        AddBlanket.Click += new EventHandler(AddBlanket_Click);
                        this.menu.MenuItems.Add(AddBlanket);
                    }


                    this.menu.MenuItems.Add(printMenuItem);
                    this.menu.MenuItems.Add(printDateItem);
                    this.menu.MenuItems.Add(splitMenuItem);
                    //this.menu.MenuItems.Add(ChangePrintState);

                }
                else if (this.neuSpread1.ActiveSheet.SelectionCount > 1)
                {
                    System.Windows.Forms.MenuItem printDateItem = new MenuItem();
                    printDateItem.Text = "只补打选中的长期医嘱停止时间";
                    printDateItem.Click += new EventHandler(printDateItem_Click);

                    this.menu.MenuItems.Add(printDateItem);
                }
                //this.menu.MenuItems.Add(PrintSelectedItem);
                this.menu.Show(this.neuSpread1, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// 更新选定长嘱的标志位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChangePrintState_Click(object sender, EventArgs e)
        {

            DialogResult rr = MessageBox.Show("确实要更新所选医嘱的打印标志么，此操作不能恢复！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (rr == DialogResult.No)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.orderBillMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //对于选定的医嘱，更新标志位
            string orderID = " ";
            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }
            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0) && this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();

                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    if (oT.Patient.ID != this.pInfo.ID)
                    {
                        continue;
                    }

                    if (orderID.Contains(oT.ID))
                    {
                        continue;
                    }
                    orderID = orderID + " " + oT.ID;


                    if (oT.GetFlag == "0")
                    {
                        //if (myOrder.UpdatePageNOAndRowNO(this.pInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        oT.RowNo = i;
                        oT.PageNo = pageNo;

                        this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        if (this.orderManager.UpdatePageNoAndRowNo(this.PInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新页码出错！" + oT.Item.Name);
                            return;
                        }

                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            oT.GetFlag = "2";
                        }
                        else
                        {
                            oT.GetFlag = "1";

                        }
                        if (this.orderManager.UpdateGetFlag(this.PInfo.ID, oT.ID, oT.GetFlag, "0") <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新提取标志出错！" + oT.Item.Name + ",可能该项目已经打印！");
                            return;
                        }
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("所选医嘱已打印");
                        return;
                    }

                }

            }
            FS.FrameWork.Management.PublicTrans.Commit();

            //更新医嘱状态
            this.SetValueVisible(true, EnumOrderType.Long);

            this.SetHeaderVisible(true, EnumOrderType.Long);

            this.bHaveSplitPage = false;
            this.QueryPatientOrder(false);
            this.bHaveSplitPage = true;
        }

        /// <summary>
        /// 长嘱增加一空行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddBlanket_Click(object sender, EventArgs e)
        {
            //---------对于未打印的医嘱，在选定行下面增加一新行-----------//
            //获得所有页，如果最后一页已经25条，新起一页，下移一行。
            //如果最后页大于当前页，所有下移一行，上一页的最后一行插入本页的第一行，
            //如果当前页是最后一页，下移一行
            int insertRowNumber = 0;
            int insertRowPage = this.neuSpread1.ActiveSheetIndex;
            int currentPage = 0;

            for (int i = 0; i < this.PageLineNO; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    FS.HISFC.Models.Order.Inpatient.Order ot = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (ot == null || ot.GetFlag != "0")
                    {
                        MessageBox.Show("只能在未打印的医嘱上方增加空行");
                        return;
                    }
                    insertRowNumber = i;
                    break;
                }
            }
            //找最后一个不为空的医嘱
            for (int i = this.PageLineNO - 1; i >= 0; i--)
            {
                if (this.neuSpread1.Sheets[this.neuSpread1.Sheets.Count - 1].Rows[i].Tag != null)
                {
                    if (i == this.PageLineNO - 1)
                    {
                        //插入新表单
                        FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                        this.InitLongSheet(ref sheet);
                        int pagenubmer = (this.neuSpread1.Sheets.Count + 1);
                        sheet.Tag = pagenubmer;
                        sheet.SheetName = "第" + pagenubmer.ToString() + "页";
                        this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);
                    }
                    break;
                }
            }
            currentPage = this.neuSpread1.Sheets.Count - 1;
            while (currentPage > insertRowPage)
            {
                for (int i = this.PageLineNO - 1; i > 0; i--)
                {
                    for (int j = 0; j < this.neuSpread1_Sheet1.ColumnCount; j++)
                    {
                        string temp = this.neuSpread1.Sheets[currentPage].Cells[i - 1, j].Text;
                        this.neuSpread1.Sheets[currentPage].SetValue(i, j, temp);
                    }
                    //修改tag值
                    FS.HISFC.Models.Order.Inpatient.Order ot = this.neuSpread1.Sheets[currentPage].Rows[i - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                    if (ot == null)
                    {
                        this.neuSpread1.Sheets[currentPage].Rows[i].Tag = null;
                    }
                    else
                    {
                        this.neuSpread1.Sheets[currentPage].Rows[i].Tag = ot.Clone();
                    }
                }
                //处理前一页最后一条医嘱和当前页第一条医嘱
                for (int j = 0; j < this.neuSpread1_Sheet1.ColumnCount; j++)
                {
                    string temp = this.neuSpread1.Sheets[currentPage - 1].Cells[this.PageLineNO - 1, j].Text;
                    this.neuSpread1.Sheets[currentPage].SetValue(0, j, temp);
                }
                FS.HISFC.Models.Order.Inpatient.Order ot2 = this.neuSpread1.Sheets[currentPage - 1].Rows[this.PageLineNO - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (ot2 == null)
                {
                    this.neuSpread1.Sheets[currentPage].Rows[0].Tag = null;
                }
                else
                {
                    this.neuSpread1.Sheets[currentPage].Rows[0].Tag = ot2.Clone();
                }
                currentPage--;
            }
            for (int i = this.PageLineNO - 1; i > insertRowNumber; i--)
            {
                for (int j = 0; j < this.neuSpread1_Sheet1.ColumnCount; j++)
                {
                    string temp = this.neuSpread1.Sheets[currentPage].Cells[i - 1, j].Text;
                    this.neuSpread1.Sheets[currentPage].SetValue(i, j, temp);
                }
                FS.HISFC.Models.Order.Inpatient.Order ot = this.neuSpread1.Sheets[currentPage].Rows[i - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                if (ot == null)
                {
                    this.neuSpread1.Sheets[currentPage].Rows[i].Tag = null;
                }
                else
                {
                    this.neuSpread1.Sheets[currentPage].Rows[i].Tag = ot.Clone();
                }
            }
            for (int j = 0; j < this.neuSpread1_Sheet1.ColumnCount; j++)
            {
                this.neuSpread1.Sheets[currentPage].SetValue(insertRowNumber, j, "");
            }
            this.neuSpread1.Sheets[currentPage].Rows[insertRowNumber].Tag = null;
        }

        private int StartSplitPage;

        /// <summary>
        /// 长嘱另起一页打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void splitMenuItem_Click(object sender, EventArgs e)
        {
            this.StartSplitPage = this.neuSpread1.ActiveSheetIndex;
            this.AddObjectToFpLongAfter();
        }

        /// <summary>
        /// 右键打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printMenuItem_Click(object sender, EventArgs e)
        {
            this.PrintSingleItem();
        }

        /// <summary>
        /// 只补打该条长期医嘱停止时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDateItem_Click(object sender, EventArgs e)
        {
            PrintSingleDate();
        }

        /// <summary>
        /// 医嘱单边距设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmSetOrderPrintTopAndLeft frm = new frmSetOrderPrintTopAndLeft();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.IsNewOrderPrint = true;
            frm.ShowDialog(this);

            this.printLongTop = frm.printLongTop;
            this.printLongLeft = frm.printLongLeft;
            this.printLongPageY = frm.printLongPageY;
            this.printShortTop = frm.printShortTop;
            this.printShortLeft = frm.printShortLeft;
            this.printShortPageY = frm.printShortPageY;
        }

        /// <summary>
        /// 切换长、临嘱 更新是否要查询标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bHaveSplitPage)
            {
                this.bHaveSplitPage = false;
            }

            this.GetDeptAndBed(EnumOrderType.All);
        }

        #endregion

        #region 动态设置打印内容

        /// <summary>
        /// 设置医嘱头部标题的可见性
        /// </summary>
        /// <param name="vis">是否可见</param>
        /// <param name="orderType">医嘱类型</param>
        private void SetHeaderVisible(bool vis, EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Short)
            {
                //标题信息
                this.lbs1.Visible = vis;
                this.lbs2.Visible = vis;
                this.lbs3.Visible = vis;
                this.lbs4.Visible = vis;
                this.lbs5.Visible = vis;
                this.lbs6.Visible = vis;
                this.lbs7.Visible = vis;

                //FarPoint中的列头文字
                for (int i = 0; i < this.neuSpread2.ActiveSheet.ColumnHeader.RowCount; i++)
                {
                    //标题信息不显示的时候，列头也不显示
                    this.neuSpread2.ActiveSheet.ColumnHeader.Rows[i].ForeColor = vis == true ? Color.Black : Color.White;
                }

                for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
                {
                    this.neuSpread2.ActiveSheet.Rows[i].BackColor = Color.White;
                }
            }
            else if (orderType == EnumOrderType.Long)
            {
                //标题信息
                this.lbl1.Visible = vis;
                this.lbl2.Visible = vis;
                this.lbl3.Visible = vis;
                this.lbl4.Visible = vis;
                this.lbl5.Visible = vis;
                this.lbl6.Visible = vis;
                this.lbl7.Visible = vis;

                //FarPoint中的列头文字
                for (int i = 0; i < this.neuSpread1.ActiveSheet.ColumnHeader.RowCount; i++)
                {
                    this.neuSpread1.ActiveSheet.ColumnHeader.Rows[i].ForeColor = vis == true ? Color.Black : Color.White;
                }

                for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    this.neuSpread1.ActiveSheet.Rows[i].BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// 设置医嘱患者基本信息的可见性
        /// </summary>
        /// <param name="vis">是否可见</param>
        /// <param name="orderType">医嘱类型</param>
        private void SetValueVisible(bool vis, EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Short)
            {
                this.lbsv1.Visible = vis;
                this.lbsv2.Visible = vis;
                this.lbsv3.Visible = vis;
                this.lbsv4.Visible = vis;
                this.lbsv5.Visible = vis;
                this.lbsv6.Visible = vis;
            }
            else if (orderType == EnumOrderType.Long)
            {
                this.lblv1.Visible = vis;
                this.lblv2.Visible = vis;
                this.lblv3.Visible = vis;
                this.lblv4.Visible = vis;
                this.lblv5.Visible = vis;
                this.lblv6.Visible = vis;
            }
        }

        /// <summary>
        /// 设置医嘱单边框显示
        /// </summary>
        /// <param name="color">边框颜色</param>
        /// <param name="orderType">医嘱单类型</param>
        private void SetGridLineColor(Color color, EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Short)
            {
                this.neuSpread2.ActiveSheet.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, color);
                this.neuSpread2.ActiveSheet.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, color);
                this.neuSpread2.ActiveSheet.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, color);
                this.neuSpread2.ActiveSheet.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, color);

                //首次打印要全部打印边框，因为FarPoint默认没有数据的不打印，所以在最后一行末尾打印一个"."
                //化工要求有多少打多少
                //this.neuSpread2.ActiveSheet.Cells[21, 0].Text += ".";
            }
            else if (orderType == EnumOrderType.Long)
            {
                this.neuSpread1.ActiveSheet.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, color);
                this.neuSpread1.ActiveSheet.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, color);
                this.neuSpread1.ActiveSheet.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, color);
                this.neuSpread1.ActiveSheet.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, color);

                //首次打印要全部打印边框，因为FarPoint默认没有数据的不打印，所以在最后一行末尾打印一个"."
                //化工要求有多少打多少
                //this.neuSpread1.ActiveSheet.Cells[20, 0].Text += ".";
            }
        }

        /// <summary>
        /// 医嘱排序
        /// </summary>
        /// <param name="al"></param>
        /// <param name="orderType">医嘱类型</param>
        private void DealOrderSeq(ArrayList al, EnumOrderType orderType)
        {
            int i = 1;
            al.Sort(new AlSort());

            if (orderType == EnumOrderType.Short)
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order ord in al)
                {
                    if (!this.htShortSeq.Contains(ord.Combo.ID))
                    {
                        htShortSeq.Add(ord.Combo.ID, i.ToString());
                        i++;
                    }
                }
            }
            else if (orderType == EnumOrderType.Long)
            {
                int deal = 0;
                foreach (FS.HISFC.Models.Order.Inpatient.Order ord in al)
                {
                    //处理医嘱重整。使医嘱的名称为“医嘱重整”，序号为空；
                    if (ord.Item.Name == "医嘱重整[嘱托]")
                    {
                        if (ord.Item.Name == "医嘱重整[嘱托]")
                            i = 0;
                        else i = 1;
                        deal++;
                    }

                    if (!this.htLongSeq.Contains(ord.Combo.ID))
                    {
                        htLongSeq.Add(ord.Combo.ID, i.ToString());
                        i++;
                    }
                }
            }
        }

        #endregion

        #region  长嘱

        /// <summary>
        /// 更新长嘱页码和提取标志
        /// </summary>
        /// <returns></returns>
        private int UpdateOrderForLong()
        {
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.orderBillMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                string orderID = string.Empty; //为了换行使用
                for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                        if (oT == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("实体转换出错！");
                            return -1;
                        }

                        if (oT.Patient.ID != this.pInfo.ID)
                        {
                            continue;
                        }

                        if (orderID.Contains(oT.ID))
                        {
                            continue;
                        }
                        orderID = orderID + " " + oT.ID;

                        //未打印
                        if (oT.GetFlag == "0")
                        {
                            //先插入 在更新
                            oT.PageNo = pageNo;
                            oT.RowNo = i;

                            if (orderManager.UpdatePageNoAndRowNo(this.pInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新页码出错！" + oT.Item.Name);
                                return -1;
                            }

                            if (oT.DCOper.OperTime != DateTime.MinValue)
                            {
                                oT.GetFlag = "2";
                            }
                            else
                            {
                                oT.GetFlag = "1";
                            }

                            if (orderManager.UpdateGetFlag(this.pInfo.ID, oT.ID, oT.GetFlag, "0") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                        }
                        //已打印
                        else if (oT.GetFlag == "1")
                        {
                            if (oT.DCOper.OperTime != DateTime.MinValue)
                            {
                                if (this.orderManager.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                    return -1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新医嘱打印标志出错！" + ex.Message);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// 判断是否是续打
        /// -1 错误; 0 首次续打； 1 正常续打；2 重打
        /// </summary>
        /// <returns>-1 错误; 0 首次续打； 1 正常续打；2 重打</returns>
        private int GetIsPrintAgainForLong()
        {
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return -1;
                    }

                    if (oT.GetFlag == "0")
                    {
                        if (i == 0)
                        {
                            return 0;
                        }
                        return 1;
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.DCOper.OperTime != DateTime.MinValue)
                        {
                            return 1;
                        }
                    }
                }
            }

            return 2;
        }

        /// <summary>
        /// 判断某页是否可以打印
        /// 如果前面有未打印医嘱，此页不允许打印
        /// </summary>
        /// <returns></returns>
        private bool CanPrintForLong(ref string errText)
        {
            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return false;
                    }
                }
            }

            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                errText = "获得页码出错！";
                return false;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "获得页码出错！";
                return false;
            }

            int maxPageNO = 0;// this.orderManager.GetMaxPageNO(this.pInfo.ID, "1");
            int ii = 0;
            if (this.orderBillMgr.GetLastOrderBillArgNew(this.PInfo.ID, "1", out maxPageNO, out ii) == -1)
            {
                errText = this.orderBillMgr.Err;
                return false;
            }

            if (pageNo > maxPageNO + 1)
            {
                errText = "第" + (maxPageNO + 2).ToString() + "页医嘱单尚未打印！";
                return false;
            }

            if (pageNo == maxPageNO + 1 && maxPageNO != -1)
            {
                bool canprintflag = true;
                for (int j = 0; j < this.PageLineNO; j++)
                {
                    if (this.neuSpread1.Sheets[maxPageNO].Rows[j].Tag != null)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.Sheets[maxPageNO].Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                        if (oT.PageNo != maxPageNO)
                        {
                            canprintflag = false;
                            break;
                        }

                    }
                }
                if (!canprintflag)
                {
                    //errText = "第" + (maxPageNO + 1).ToString() + "页尚有未打印医嘱！";
                    return true;
                }
            }

            //MessageBox.Show("请确定已放入第" + (pageNo + 1).ToString() + "页长期医嘱单！");

            return true;
        }

        /// <summary>
        /// 设置打印显示
        /// </summary>
        /// <returns></returns>
        private void SetPrintContentsForLong()
        {
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    //未打印
                    if (oT.GetFlag == "0")
                    {
                        //this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.OrderName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DoctorName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCExecNurseName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCDoctorName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.ExecNurseName, "");
                        continue;
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.Status != 3)
                        {
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginDate, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginTime, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DrawCombo, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.OrderName, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DoctorName, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.ExecNurseName, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndDate, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndTime, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCDoctorName, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCExecNurseName, "");
                        }
                        else
                        {
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginDate, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginTime, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DrawCombo, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.OrderName, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DoctorName, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.ExecNurseName, "");
                            //this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndTime, "");
                            //this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndDate, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCExecNurseName, "");
                            this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCDoctorName, "");
                        }

                        this.lblPageLong.Visible = false;
                        this.SetValueVisible(false, EnumOrderType.Long);
                    }
                    else
                    {
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginDate, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginTime, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DrawCombo, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.OrderName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DoctorName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.ExecNurseName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndDate, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndTime, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCDoctorName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCExecNurseName, "");

                        this.lblPageLong.Visible = false;
                        this.SetValueVisible(false, EnumOrderType.Long);
                    }
                }
            }
        }

        /// <summary>
        /// 设置长期医嘱重打显示内容
        /// </summary>
        private void SetRePrintContentsForLong()
        {
            if (this.neuSpread1.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread1.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    if (oT.GetFlag == "0")
                    {
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginDate, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.BeginTime, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.OrderName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DrawCombo, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DoctorName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.ExecNurseName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndDate, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.EndTime, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCDoctorName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCExecNurseName, "");
                        continue;
                    }
                    else if (oT.GetFlag == "1" || oT.GetFlag == "2")
                    {
                        //this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.OrderName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DoctorName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCExecNurseName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCDoctorName, "");
                        //this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.ExecNurseName, "");
                    }
                }
            }

            this.lblPageLong.Visible = true;
        }

        /// <summary>
        /// 添加到Fp
        /// </summary>
        /// <param name="al"></param>
        private void AddObjectToFpLong(ArrayList al)
        {
            DateTime now = this.orderManager.GetDateTimeFromSysDateTime().Date;//当前系统时间
            #region 为空返回
            if (al.Count <= 0)
            {
                return;
            }
            #endregion

            #region 定义变量
            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNO = new Hashtable();
            ArrayList alPageNO = new ArrayList();

            int MaxPageNO = -1;
            int MaxRowNO = -1;
            #endregion

            #region 清空数据
            this.neuSpread1_Sheet1.Cells[0, 0, this.neuSpread1_Sheet1.RowCount - 1, this.neuSpread1_Sheet1.ColumnCount - 1].Text = "";
            #endregion

            #region 保留一个Sheet
            if (this.neuSpread1.Sheets.Count > 1)
            {
                for (int j = this.neuSpread1.Sheets.Count - 1; j > 0; j--)
                {
                    this.neuSpread1.Sheets.RemoveAt(j);
                }
            }
            #endregion

            #region 护嘱调整
            if (this.isNurseOrder)
            {
                this.lbl1.Text = "东莞市人民医院长期护嘱单";
                neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "护嘱";
                neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "护嘱";
                neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "开立/执行护士签名";
                neuSpread1_Sheet1.Columns[4].Width = 120;
                neuSpread1_Sheet1.ColumnHeader.Cells.Get(1, 8).Value = "护士签名";
                neuSpread1_Sheet1.Columns[5].Visible = !this.isNurseOrder;
                neuSpread1_Sheet1.Columns.Get(8).Label = "护士签名";
                neuSpread1_Sheet1.Columns[9].Visible = !this.isNurseOrder;
                this.lblDoctSignLong.Text = "护士签名:";
                this.lblNurseSignLong.Visible = false;
            }
            #endregion

            #region 按是否打印分组
            foreach (FS.HISFC.Models.Order.Inpatient.Order temp in al)
            {
                if (temp.PageNo == -1)
                {
                    alPageNull.Insert(alPageNull.Count, temp);
                }
                else
                {
                    if (!hsPageNO.ContainsKey(temp.PageNo))
                    {
                        alPageNO.Insert(alPageNO.Count, temp.PageNo);

                        hsPageNO.Add(temp.PageNo, new ArrayList());

                        (hsPageNO[temp.PageNo] as ArrayList).Insert((hsPageNO[temp.PageNo] as ArrayList).Count, temp);
                    }
                    else
                    {
                        (hsPageNO[temp.PageNo] as ArrayList).Insert((hsPageNO[temp.PageNo] as ArrayList).Count, temp);
                    }
                }
            }
            #endregion

            #region 将已打印的显示
            for (int i = 0; i < alPageNO.Count; i++)
            {
                int pageNo = FS.FrameWork.Function.NConvert.ToInt32(alPageNO[i].ToString());

                if (FS.FrameWork.Function.NConvert.ToInt32(pageNo) > MaxPageNO)
                {
                    MaxPageNO = FS.FrameWork.Function.NConvert.ToInt32(pageNo);
                    MaxRowNO = -1;
                }

                ArrayList alTemp = hsPageNO[pageNo] as ArrayList;
                if (i == 0)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order o in alTemp)
                    {
                        //List<FS.HISFC.Models.Order.Inpatient.OrderExtend> orderExtendInfoList = this.orderExtendMgr.QueryByInpatineNoOrderID(this.pInfo.ID, o.ID);
                        //if (orderExtendInfoList == null)
                        //{
                        //    MessageBox.Show("查询医嘱" + o.Item.Name + "扩展信息失败!");
                        //    return;
                        //}

                        //if (orderExtendInfoList.Count > 0)
                        //{
                        //    o.Frequency.Dept.User03 = orderExtendInfoList[0].FirstDayQty.ToString();
                        //}
                        this.neuSpread1_Sheet1.Rows[o.RowNo].BackColor = Color.MistyRose;
                        this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.BeginDate, o.BeginTime.ToString("MM-dd"));
                        this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.BeginTime, o.BeginTime.ToString("HH:mm"));
                        this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.DoctorName, o.ReciptDoctor.Name);

                        #region 如果有签名，此处打印签名护士
                        //if (orderExtendInfoList.Count > 0)
                        //{
                        //    //如果有签名就取签名，否则就取电脑执行护士
                        //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                        //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                        //    this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.ExecNurseName, name == "/" ? "" : name);

                        //    //处方权受限的医嘱，打印上级审核医生名字
                        //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                        //    {
                        //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                        //        if (!string.IsNullOrEmpty(privDoctName))
                        //        {
                        //            this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + o.ReciptDoctor.Name);
                        //        }
                        //    }

                        //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                        //    {
                        //        this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.DoctorName, o.ReciptDoctor.Name +
                        //            "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                        //    }
                        //}
                        //else
                        //{
                        //    //this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.ExecNurseName, emplHelPer.GetName(o.Nurse.ID));
                        //}
                        #endregion
                        this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.DrawCombo, "");
                        this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.ComboID, o.Combo.ID);

                        //打印开始、停止时间
                        string tempstr = string.Empty;
                        if (o.MOTime.Date != o.BeginTime.Date || o.EndTime != DateTime.MinValue)
                        {
                            if (o.EndTime == DateTime.MinValue)
                            {
                                tempstr = o.BeginTime.ToString("MM.dd") + "起";
                            }
                        }
                        string frequency = o.Frequency.ID;
                        if (htSpecFrequency != null && htSpecFrequency.Contains(o.Frequency.ID))
                        {
                            frequency = htSpecFrequency[o.Frequency.ID].ToString();
                        }
                        if (o.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (o.DoseOnce == 0)
                            {
                                this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, o.Item.Name + "[" + o.Frequency.Dept.User03 + "]");
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " + o.Memo + tempstr + "[" + o.Frequency.Dept.User03 + "]");
                            }
                        }
                        else
                        {
                            //检验
                            if (o.Item.SysClass.ID.ToString() == "UL")
                            {
                                this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, o.Item.Name + " " + frequency + " " + o.Memo + tempstr + "[" + o.Frequency.Dept.User03 + "]");
                            }
                            else
                            {
                                //2008.6.6李超峰修改。
                                this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, o.Item.Name + " " + frequency + " " + o.Memo + tempstr + "[" + o.Frequency.Dept.User03 + "]");
                                //对于自动生成的“医嘱重整”，只打印医嘱重整
                                if (o.Item.Name == "医嘱重整[嘱托]")
                                {
                                    this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, "          医嘱重整");
                                }
                            }

                        }
                        if (o.EndTime.Date <= now.Date && o.EndTime > DateTime.MinValue)
                        {
                            this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.EndDate, o.EndTime.ToString("MM-dd"));
                            this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.EndTime, o.EndTime.ToString("HH:mm"));
                            this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.DCDoctorName, o.DCOper.Name);
                            string dcConfirmOper = this.orderManager.ExecSqlReturnOne(string.Format(execDCConfirmOper, o.ID));
                            this.neuSpread1.ActiveSheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.DCExecNurseName, dcConfirmOper);

                        }

                        this.neuSpread1_Sheet1.Rows[o.RowNo].Tag = o;

                        if (pageNo == MaxPageNO && o.RowNo > MaxRowNO)
                        {
                            MaxRowNO = o.RowNo;
                        }
                    }

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(this.neuSpread1_Sheet1, (int)EnumLongOrderPrint.ComboID, (int)EnumLongOrderPrint.DrawCombo);

                    this.neuSpread1_Sheet1.Tag = pageNo;
                    this.neuSpread1_Sheet1.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "页";
                }
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitLongSheet(ref sheet);

                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);

                    foreach (FS.HISFC.Models.Order.Inpatient.Order o in alTemp)
                    {
                        //List<FS.HISFC.Models.Order.Inpatient.OrderExtend> orderExtendInfoList = this.orderExtendMgr.QueryByInpatineNoOrderID(this.pInfo.ID, o.ID);
                        //if (orderExtendInfoList == null)
                        //{
                        //    MessageBox.Show("查询医嘱" + o.Item.Name + "扩展信息失败!");
                        //    return;
                        //}

                        //if (orderExtendInfoList.Count > 0)
                        //{
                        //    o.Frequency.Dept.User03 = orderExtendInfoList[0].FirstDayQty.ToString();
                        //}
                        sheet.Rows[o.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.BeginDate, o.BeginTime.ToString("MM-dd"));
                        sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.BeginTime, o.BeginTime.ToString("HH:mm"));
                        sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.DoctorName, o.ReciptDoctor.Name);

                        #region 如果有签名，此处打印签名护士
                        //if (orderExtendInfoList.Count > 0)
                        //{
                        //    //如果有签名就取签名，否则就取电脑执行护士
                        //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                        //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                        //    sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.ExecNurseName, name == "/" ? "" : name);

                        //    //处方权受限的医嘱，打印上级审核医生名字
                        //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                        //    {
                        //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                        //        if (!string.IsNullOrEmpty(privDoctName))
                        //        {
                        //            sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + o.ReciptDoctor.Name);
                        //        }
                        //    }

                        //    if(isNurseOrder)
                        //    {
                        //        sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.DoctorName, o.ReciptDoctor.Name+
                        //            "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                        //    }
                        //}
                        //else
                        //{
                        //    //this.neuSpread1_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.ExecNurseName, emplHelPer.GetName(o.Nurse.ID));
                        //}
                        #endregion
                        sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.DrawCombo, "");
                        sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.ComboID, o.Combo.ID);

                        string tempstr = string.Empty;
                        //if (o.MOTime.Date != o.BeginTime.Date || o.EndTime != DateTime.MinValue)
                        //{
                        //    if (o.EndTime != DateTime.MinValue)
                        //    {
                        //        tempstr = o.BeginTime.ToString("MM.dd") + "起" + o.EndTime.ToString("MM.dd") + "止";
                        //    }
                        //    else if (o.EndTime == DateTime.MinValue)
                        //    {
                        //        tempstr = o.BeginTime.ToString("MM.dd") + "起";
                        //    }
                        //}
                        string frequency = o.Frequency.ID;
                        if (htSpecFrequency != null && htSpecFrequency.Contains(o.Frequency.ID))
                        {
                            frequency = htSpecFrequency[o.Frequency.ID].ToString();
                        }
                        if (o.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (o.DoseOnce == 0)
                            {
                                sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, o.Item.Name + "[" + o.Frequency.Dept.User03 + "]");
                            }
                            else
                            {
                                sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " + o.Memo + tempstr + "[" + o.Frequency.Dept.User03 + "]");
                            }
                        }
                        else
                        {
                            //检验
                            if (o.Item.SysClass.ID.ToString() == "UL")
                            {
                                sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, o.Item.Name + " " + frequency + " " + o.Memo + tempstr + "[" + o.Frequency.Dept.User03 + "]");
                            }
                            else
                            {
                                //2008.6.6李超峰修改。
                                sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, o.Item.Name + " " + frequency + " " + o.Memo + tempstr + "[" + o.Frequency.Dept.User03 + "]");
                                //对于自动生成的“医嘱重整”，只打印医嘱重整
                                if (o.Item.Name == "医嘱重整[嘱托]")
                                {
                                    sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.OrderName, "          医嘱重整");
                                }

                            }

                        }
                        if (o.EndTime.Date <= now.Date && o.EndTime > DateTime.MinValue)
                        {
                            sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.EndDate, o.EndTime.ToString("MM-dd"));
                            sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.EndTime, o.EndTime.ToString("HH:mm"));
                            sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.DCDoctorName, o.DCOper.Name);
                            string dcConfirmOper = this.orderManager.ExecSqlReturnOne(string.Format(execDCConfirmOper, o.ID));
                            sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.DCExecNurseName, dcConfirmOper);
                        }
                        sheet.Rows[o.RowNo].Tag = o;

                        if (pageNo == MaxPageNO && o.RowNo > MaxRowNO)
                        {
                            MaxRowNO = o.RowNo;
                        }
                    }

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(sheet, (int)EnumLongOrderPrint.ComboID, (int)EnumLongOrderPrint.DrawCombo);

                    sheet.Tag = pageNo;
                    sheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "页";
                }

            }
            #endregion

            #region 显示未打印医嘱

            bool fromOne = true;
            int iniIndex = -1;
            int endIndex = -1;

            if (MaxPageNO == -1)
            {
                MaxPageNO++;
                fromOne = false;
            }

            if (MaxRowNO == -1)
            {
                MaxRowNO++;
            }

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread1.Sheets[MaxPageNO];


            activeSheet.Tag = MaxPageNO++;
            activeSheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNO)).ToString() + "页";

            if (fromOne)
            {
                iniIndex = 1;
                endIndex = alPageNull.Count + 1;
            }
            else
            {
                iniIndex = 0;
                endIndex = alPageNull.Count;
            }

            for (; iniIndex < endIndex; iniIndex++)
            {
                FS.HISFC.Models.Order.Inpatient.Order oTemp;

                if (fromOne)
                {
                    oTemp = alPageNull[iniIndex - 1] as FS.HISFC.Models.Order.Inpatient.Order;
                }
                else
                {
                    oTemp = alPageNull[iniIndex] as FS.HISFC.Models.Order.Inpatient.Order;
                }

                //List<FS.HISFC.Models.Order.Inpatient.OrderExtend> orderExtendInfoList = this.orderExtendMgr.QueryByInpatineNoOrderID(this.pInfo.ID, oTemp.ID);
                //if (orderExtendInfoList == null)
                //{
                //    MessageBox.Show("查询医嘱" + oTemp.Item.Name + "扩展信息失败!");
                //    return;
                //}

                //if (orderExtendInfoList.Count > 0)
                //{
                //    oTemp.Frequency.Dept.User03 = orderExtendInfoList[0].FirstDayQty.ToString();
                //}

                if ((iniIndex + MaxRowNO) % this.PageLineNO == 0 && (iniIndex + MaxRowNO) != 0)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitLongSheet(ref sheet);

                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNO++;
                    activeSheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNO)).ToString() + "页";

                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.BeginDate, oTemp.BeginTime.ToString("MM-dd"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.BeginTime, oTemp.BeginTime.ToString("HH:mm"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, oTemp.ReciptDoctor.Name);
                    //activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, emplHelPer.GetName(oTemp.Nurse.ID));

                    #region 如果有签名，此处打印签名护士
                    //if (orderExtendInfoList.Count > 0)
                    //{
                    //    //如果有签名就取签名，否则就取电脑执行护士
                    //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                    //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, name == "/" ? "" : name);

                    //    //处方权受限的医嘱，打印上级审核医生名字
                    //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                    //    {
                    //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                    //        if (!string.IsNullOrEmpty(privDoctName))
                    //        {
                    //            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + oTemp.ReciptDoctor.Name);
                    //        }
                    //    }

                    //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                    //    {
                    //        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, oTemp.ReciptDoctor.Name +
                    //            "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                    //    }
                    //}
                    //else
                    //{
                    //    //activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, emplHelPer.GetName(oTemp.Nurse.ID));
                    //}
                    #endregion

                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ComboID, oTemp.Combo.ID);
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DrawCombo, "");
                    string tempstr = string.Empty;
                    string frequency = oTemp.Frequency.ID;
                    if (htSpecFrequency != null && htSpecFrequency.Contains(oTemp.Frequency.ID))
                    {
                        frequency = htSpecFrequency[oTemp.Frequency.ID].ToString();
                    }
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //为了和开立界面一致，使用商品名
                        if (oTemp.DoseOnce == 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + "[" + oTemp.Frequency.Dept.User03 + "]");
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Memo + tempstr + "[" + oTemp.Frequency.Dept.User03 + "]");
                        }
                    }
                    else
                    {
                        //检验
                        if (oTemp.Item.SysClass.ID.ToString() == "UL")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + frequency + " " + oTemp.Memo + tempstr + "[" + oTemp.Frequency.Dept.User03 + "]");
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + frequency + " " + oTemp.Memo + tempstr + "[" + oTemp.Frequency.Dept.User03 + "]");
                            //对于自动生成的“医嘱重整”，只打印医嘱重整
                            if (oTemp.Item.Name == "医嘱重整[嘱托]")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, 2, "          医嘱重整");
                            }
                        }

                    }
                    if (oTemp.EndTime.Date <= now.Date && oTemp.EndTime > DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.EndDate, oTemp.EndTime.ToString("MM-dd"));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.EndTime, oTemp.EndTime.ToString("HH:mm"));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DCDoctorName, oTemp.DCOper.Name);

                        string dcConfirmOper = this.orderManager.ExecSqlReturnOne(string.Format(execDCConfirmOper, oTemp.ID));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DCExecNurseName, dcConfirmOper);

                    }
                    activeSheet.Rows[(iniIndex + MaxRowNO) % this.PageLineNO].Tag = oTemp;

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(activeSheet, (int)EnumLongOrderPrint.ComboID, (int)EnumLongOrderPrint.DrawCombo);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.BeginDate, oTemp.BeginTime.ToString("MM-dd"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.BeginTime, oTemp.BeginTime.ToString("HH:mm"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, oTemp.ReciptDoctor.Name);

                    #region 如果有签名，此处打印签名护士
                    //if (orderExtendInfoList.Count > 0)
                    //{
                    //    //如果有签名就取签名，否则就取电脑执行护士
                    //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                    //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, name == "/" ? "" : name);

                    //    //处方权受限的医嘱，打印上级审核医生名字
                    //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                    //    {
                    //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                    //        if (!string.IsNullOrEmpty(privDoctName))
                    //        {
                    //            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + oTemp.ReciptDoctor.Name);
                    //        }
                    //    }

                    //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                    //    {
                    //        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, oTemp.ReciptDoctor.Name +
                    //            "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                    //    }
                    //}
                    //else
                    //{
                    //    //activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, emplHelPer.GetName(oTemp.Nurse.ID));
                    //}
                    #endregion

                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ComboID, oTemp.Combo.ID);
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DrawCombo, "");
                    string tempstr = string.Empty;
                    string frequency = oTemp.Frequency.ID;
                    if (htSpecFrequency != null && htSpecFrequency.Contains(oTemp.Frequency.ID))
                    {
                        frequency = htSpecFrequency[oTemp.Frequency.ID].ToString();
                    }
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (oTemp.DoseOnce == 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + "[" + oTemp.Frequency.Dept.User03 + "]");
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + frequency + " " + oTemp.Usage.Name + " " + oTemp.Memo + tempstr + "[" + oTemp.Frequency.Dept.User03 + "]");
                        }
                    }
                    else
                    {
                        //检验
                        if (oTemp.Item.SysClass.ID.ToString() == "UL")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + frequency + " " + oTemp.Memo + tempstr + "[" + oTemp.Frequency.Dept.User03 + "]");
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + frequency + " " + oTemp.Memo + tempstr + "[" + oTemp.Frequency.Dept.User03 + "]");
                            //对于自动生成的“医嘱重整”，只打印医嘱重整
                            if (oTemp.Item.Name == "医嘱重整[嘱托]")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, "          医嘱重整");
                            }
                        }
                    }
                    if (oTemp.EndTime.Date <= now.Date && oTemp.EndTime > DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.EndDate, oTemp.EndTime.ToString("MM-dd"));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.EndTime, oTemp.EndTime.ToString("HH:mm"));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DCDoctorName, oTemp.DCOper.Name);
                        string dcConfirmOper = this.orderManager.ExecSqlReturnOne(string.Format(execDCConfirmOper, oTemp.ID));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DCExecNurseName, dcConfirmOper);
                    }
                    activeSheet.Rows[(iniIndex + MaxRowNO) % this.PageLineNO].Tag = oTemp;

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(activeSheet, (int)EnumLongOrderPrint.ComboID, (int)EnumLongOrderPrint.DrawCombo);
                }
            }
            #endregion

            DealLongOrderCrossPage();
        }

        /// <summary>
        /// 转科分页后添加到Fp
        /// </summary>
        /// <param name="al"></param>
        private void AddObjectToFpLongAfter()
        {
            DateTime now = this.orderManager.GetDateTimeFromSysDateTime().Date;//当前系统时间

            #region 定义变量

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNO = new Hashtable();
            ArrayList alPageNO = new ArrayList();

            int MaxPageNO = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1.ActiveSheet.Tag) + 1;
            int MaxRowNO = -1;

            int row = this.neuSpread1.ActiveSheet.ActiveRowIndex;

            if (this.neuSpread1.ActiveSheet.Rows[row].Tag == null)
            {
                MessageBox.Show("所选项目为空！");
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order ord = this.neuSpread1.ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            if (MessageBox.Show("确定要从" + ord.Item.Name + "开始另起一页吗?此操作不可撤销！", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            if (ord.PageNo >= 0 || ord.RowNo >= 0)
            {
                MessageBox.Show(ord.Item.Name + "已经打印过，不能另起一页！");
                return;
            }

            #endregion

            #region 获取剩余数据
            FS.HISFC.Models.Order.Inpatient.Order orderBill = (this.neuSpread1.ActiveSheet.ActiveRow.Tag as FS.HISFC.Models.Order.Inpatient.Order).Clone() as FS.HISFC.Models.Order.Inpatient.Order;

            for (int i = this.neuSpread1.ActiveSheet.ActiveRowIndex; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.Rows[i].Tag != null)
                {
                    alPageNull.Add(this.neuSpread1.ActiveSheet.Rows[i].Tag);
                    this.neuSpread1.ActiveSheet.Rows[i].Tag = orderBill;
                }
            }

            for (int i = this.neuSpread1.ActiveSheetIndex + 1; i < this.neuSpread1.Sheets.Count; i++)
            {
                for (int j = 0; j < this.neuSpread1.Sheets[i].Rows.Count; j++)
                {
                    if (this.neuSpread1.Sheets[i].Rows[j].Tag != null)
                    {
                        alPageNull.Add(this.neuSpread1.Sheets[i].Rows[j].Tag);
                    }
                }
            }

            #endregion

            #region 清空数据

            for (int j = row; j < this.neuSpread1.ActiveSheet.Rows.Count; j++)
            {
                this.neuSpread1.ActiveSheet.SetValue(j, 0, "");
                this.neuSpread1.ActiveSheet.SetValue(j, 1, "");
                this.neuSpread1.ActiveSheet.SetValue(j, 2, "");
                this.neuSpread1.ActiveSheet.SetValue(j, 3, "");
                this.neuSpread1.ActiveSheet.SetValue(j, 4, "");
                this.neuSpread1.ActiveSheet.SetValue(j, 5, "");
                this.neuSpread1.ActiveSheet.SetValue(j, 6, "");
                this.neuSpread1.ActiveSheet.SetValue(j, 7, "");
                this.neuSpread1.ActiveSheet.SetValue(j, 8, "");
                this.neuSpread1.ActiveSheet.SetValue(j, 9, "");
                this.neuSpread1.ActiveSheet.Rows[j].Tag = null;
            }

            #endregion

            #region 保留一个Sheet

            if (this.neuSpread1.Sheets.Count > 1)
            {
                for (int j = this.neuSpread1.Sheets.Count - 1; j > this.neuSpread1.ActiveSheetIndex; j--)
                {
                    this.neuSpread1.Sheets.RemoveAt(j);
                }
            }

            #endregion

            #region 显示未打印医嘱

            int iniIndex = -1;
            int endIndex = -1;

            if (MaxRowNO == -1)
            {
                MaxRowNO++;
            }

            FarPoint.Win.Spread.SheetView orgSheet = new FarPoint.Win.Spread.SheetView();

            this.InitLongSheet(ref orgSheet);

            this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, orgSheet);

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread1.Sheets[MaxPageNO];


            activeSheet.Tag = MaxPageNO++;
            activeSheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNO)).ToString() + "页";

            iniIndex = 0;
            endIndex = alPageNull.Count;

            for (; iniIndex < endIndex; iniIndex++)
            {
                FS.HISFC.Models.Order.Inpatient.Order oTemp;

                oTemp = alPageNull[iniIndex] as FS.HISFC.Models.Order.Inpatient.Order;

                //List<FS.HISFC.Models.Order.Inpatient.OrderExtend> orderExtendInfoList = this.orderExtendMgr.QueryByInpatineNoOrderID(this.pInfo.ID, oTemp.ID);
                //if (orderExtendInfoList == null)
                //{
                //    MessageBox.Show("查询医嘱" + oTemp.Item.Name + "扩展信息失败!");
                //    return;
                //}

                //if (orderExtendInfoList.Count > 0)
                //{
                //    oTemp.Frequency.Dept.User03 = orderExtendInfoList[0].FirstDayQty.ToString();
                //}

                if ((iniIndex + MaxRowNO) % this.PageLineNO == 0 && (iniIndex + MaxRowNO) != 0)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitLongSheet(ref sheet);

                    this.neuSpread1.Sheets.Insert(this.neuSpread1.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNO++;
                    activeSheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNO)).ToString() + "页";
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.BeginDate, oTemp.BeginTime.ToString("MM-dd"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.BeginTime, oTemp.BeginTime.ToString("HH:mm"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, oTemp.ReciptDoctor.Name);
                    //activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, emplHelPer.GetName(oTemp.Nurse.ID));

                    #region 如果有签名，此处打印签名护士
                    //if (orderExtendInfoList.Count > 0)
                    //{
                    //    //如果有签名就取签名，否则就取电脑执行护士
                    //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                    //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, name == "/" ? "" : name);

                    //    //处方权受限的医嘱，打印上级审核医生名字
                    //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                    //    {
                    //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                    //        if (!string.IsNullOrEmpty(privDoctName))
                    //        {
                    //            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + oTemp.ReciptDoctor.Name);
                    //        }
                    //    }

                    //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                    //    {
                    //        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, oTemp.ReciptDoctor.Name +
                    //            "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                    //    }
                    //}
                    //else
                    //{
                    //    //activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, emplHelPer.GetName(oTemp.Nurse.ID));
                    //}
                    #endregion

                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ComboID, oTemp.Combo.ID);
                    string tempstr = string.Empty;

                    string frequency = oTemp.Frequency.ID;
                    if (htSpecFrequency != null && htSpecFrequency.Contains(oTemp.Frequency.ID))
                    {
                        frequency = htSpecFrequency[oTemp.Frequency.ID].ToString();
                    }
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (oTemp.DoseOnce == 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name);
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Memo + tempstr);
                        }
                    }
                    else
                    {
                        //检验
                        if (oTemp.Item.SysClass.ID.ToString() == "UL")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + frequency + " " + oTemp.Memo + tempstr);
                        }
                        else
                        {
                            //2008.6.6李超峰修改。
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + frequency + " " + oTemp.Memo + tempstr);
                            //对于自动生成的“医嘱重整”，只打印医嘱重整
                            if (oTemp.Item.Name == "医嘱重整[嘱托]")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, "          医嘱重整");
                            }
                        }
                    }
                    if (oTemp.EndTime.Date <= now.Date && oTemp.EndTime > DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.EndDate, oTemp.EndTime.ToString("MM-dd"));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.EndTime, oTemp.EndTime.ToString("HH:mm"));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DCDoctorName, oTemp.DCOper.Name);
                        //停止执行护士签字
                        //this.neuSpread2_Sheet1.SetValue(o.RowNO,9,o.User_DC_ConfirmOper.Name);
                        string dcConfirmOper = this.orderManager.ExecSqlReturnOne(string.Format(execDCConfirmOper, oTemp.ID));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DCExecNurseName, dcConfirmOper);
                    }
                    activeSheet.Rows[(iniIndex + MaxRowNO) % this.PageLineNO].Tag = oTemp;

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(activeSheet, (int)EnumLongOrderPrint.ComboID, (int)EnumLongOrderPrint.DrawCombo);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.BeginDate, oTemp.BeginTime.ToString("MM-dd"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.BeginTime, oTemp.BeginTime.ToString("HH:mm"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, oTemp.ReciptDoctor.Name);
                    //activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, emplHelPer.GetName(oTemp.Nurse.ID));

                    #region 如果有签名，此处打印签名护士
                    //if (orderExtendInfoList.Count > 0)
                    //{
                    //    //如果有签名就取签名，否则就取电脑执行护士
                    //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                    //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, name == "/" ? "" : name);

                    //    //处方权受限的医嘱，打印上级审核医生名字
                    //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                    //    {
                    //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                    //        if (!string.IsNullOrEmpty(privDoctName))
                    //        {
                    //            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + oTemp.ReciptDoctor.Name);
                    //        }
                    //    }

                    //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                    //    {
                    //        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DoctorName, oTemp.ReciptDoctor.Name +
                    //            "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                    //    }
                    //}
                    //else
                    //{
                    //    //activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ExecNurseName, emplHelPer.GetName(oTemp.Nurse.ID));
                    //}
                    #endregion

                    activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.ComboID, oTemp.Combo.ID);
                    string frequency = oTemp.Frequency.ID;
                    if (htSpecFrequency != null && htSpecFrequency.Contains(oTemp.Frequency.ID))
                    {
                        frequency = htSpecFrequency[oTemp.Frequency.ID].ToString();
                    }
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        string tempstr = string.Empty;
                        if (oTemp.DoseOnce == 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name);
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + frequency + " " + oTemp.Usage.Name + " " + oTemp.Memo + tempstr);
                        }
                    }
                    else
                    {
                        //检验
                        if (oTemp.Item.SysClass.ID.ToString() == "UL")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + frequency + " " + oTemp.Memo);
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, oTemp.Item.Name + " " + frequency + " " + oTemp.Memo);
                            //对于自动生成的“医嘱重整”，只打印医嘱重整
                            if (oTemp.Item.Name == "医嘱重整[嘱托]")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.OrderName, "          医嘱重整");
                            }
                        }
                    }
                    if (oTemp.EndTime.Date <= now.Date && oTemp.EndTime > DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.EndDate, oTemp.EndTime.ToString("MM-dd"));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.EndTime, oTemp.EndTime.ToString("HH:mm"));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DCDoctorName, oTemp.DCOper.Name);
                        string dcConfirmOper = this.orderManager.ExecSqlReturnOne(string.Format(execDCConfirmOper, oTemp.ID));
                        activeSheet.SetValue((iniIndex + MaxRowNO) % this.PageLineNO, (int)EnumLongOrderPrint.DCExecNurseName, dcConfirmOper);
                    }
                    activeSheet.Rows[(iniIndex + MaxRowNO) % this.PageLineNO].Tag = oTemp;

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(activeSheet, (int)EnumLongOrderPrint.ComboID, (int)EnumLongOrderPrint.DrawCombo);
                }
            }

            bHaveSplitPage = true;

            #endregion
        }

        /// <summary>
        /// 初始化长嘱Sheet
        /// </summary>
        /// <param name="sheet"></param>
        private void InitLongSheet(ref FarPoint.Win.Spread.SheetView sheet)
        {
            // 
            // neuSpread1_Sheet1
            // 
            sheet.Reset();
            sheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            sheet.ColumnCount = 11;
            sheet.ColumnHeader.RowCount = 2;
            sheet.RowCount = 21;
            sheet.RowHeader.ColumnCount = 0;
            sheet.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 0).Value = "起始";
            sheet.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 2).Value = "医嘱";
            sheet.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 3).Value = "医嘱";
            sheet.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 4).Value = "医师签名";
            if (this.isNurseOrder)
            {
                sheet.ColumnHeader.Cells.Get(0, 2).Value = "护嘱";
                sheet.ColumnHeader.Cells.Get(0, 3).Value = "护嘱";
                sheet.ColumnHeader.Cells.Get(0, 4).Value = "开立/执行护士签名";
                sheet.Columns[4].Width = 120;
            }
            sheet.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 5).Value = "核对/执行护士签名";

            sheet.Columns[5].Visible = !this.isNurseOrder;

            sheet.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 4;
            sheet.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 6).Value = "停止";
            sheet.ColumnHeader.Cells.Get(1, 0).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(1, 0).Value = "日期";
            sheet.ColumnHeader.Cells.Get(1, 1).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(1, 1).Value = "时间";
            sheet.ColumnHeader.Cells.Get(1, 6).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(1, 6).Value = "日期";
            sheet.ColumnHeader.Cells.Get(1, 7).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(1, 7).Value = "时间";
            sheet.ColumnHeader.Cells.Get(1, 8).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(1, 8).Value = "医师签名";
            if (this.isNurseOrder)
            {
                sheet.ColumnHeader.Cells.Get(1, 8).Value = "护士签名";
            }
            sheet.ColumnHeader.Cells.Get(1, 9).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(1, 9).Value = "执行护士签名";

            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Locked = false;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            sheet.ColumnHeader.Rows.Get(0).Height = 30F;
            sheet.ColumnHeader.Rows.Get(1).Height = 57F;
            sheet.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            sheet.Columns.Get(0).Label = "日期";
            sheet.Columns.Get(0).Width = 56F;
            sheet.Columns.Get(1).Label = "时间";
            sheet.Columns.Get(1).Width = 56F;
            sheet.Columns.Get(2).Width = 18F;
            textCellType1.WordWrap = true;
            sheet.Columns.Get(3).CellType = textCellType1;
            sheet.Columns.Get(4).CellType = textCellType1;
            sheet.Columns.Get(3).Width = 240F;
            sheet.Columns.Get(4).Width = 58F;
            sheet.Columns.Get(5).Width = 106F;
            sheet.Columns.Get(6).Label = "日期";
            sheet.Columns.Get(6).Width = 50F;
            sheet.Columns.Get(7).Label = "时间";
            sheet.Columns.Get(7).Width = 50F;
            sheet.Columns.Get(8).Label = "医师签名";
            if (this.isNurseOrder)
            {
                sheet.Columns.Get(8).Label = "护士签名";
            }
            sheet.Columns.Get(8).Width = 55F;
            sheet.Columns.Get(9).Label = "执行护士签名";
            sheet.Columns.Get(9).Width = 56F;

            sheet.Columns[9].Visible = !this.isNurseOrder;

            if (this.isNurseOrder)
            {
                sheet.Columns[4].Width = 120;
            }

            sheet.Columns.Get(10).Visible = false;
            sheet.DataAutoHeadings = false;
            sheet.DataAutoSizeColumns = false;
            sheet.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.DefaultStyle.Locked = false;
            sheet.DefaultStyle.Parent = "DataAreaDefault";
            sheet.GrayAreaBackColor = System.Drawing.Color.White;
            sheet.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            sheet.OperationMode = FarPoint.Win.Spread.OperationMode.MultiSelect;
            sheet.RowHeader.Columns.Default.Resizable = false;
            sheet.Rows.Default.Height = 39.4F;
            sheet.Rows.Default.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            sheet.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            sheet.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            sheet.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            sheet.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
        }

        #endregion

        #region 临嘱

        ///<sumary>
        /// 判断是否打印总量,返回false不打印，返回true打印
        ///</sumary>
        ///<para name=item>医嘱 项目</para>
        private bool IsPrintQTY(FS.HISFC.Models.Order.Inpatient.Order o)
        {
            //中草药不打印总量
            if (o.Item.SysClass.ID.ToString() == "PCC")
                return false;
            //描述医嘱不打印总量
            if (o.Item.SysClass.ID.ToString() == "M")
                return false;
            switch (o.Usage.Name)
            {
                //case "po":
                //    return true;
                //case "po-输血":
                //    return true;
                //case "po-化疗":
                //    return true;
                //case "外用":
                //    return true;
                //case "备用":
                //    return true;
                //case "封管用":
                //    return true;
                //case "舌下含服":
                //    return true;
                //case "含服":
                //    return true;
                //case "鼻饲（药物）":
                //    return true;
                //case "鼻饲（营养液）":
                //    return true;
                //case "漱口":
                //    return true;
                //case "含漱":
                //    return true;
                //case "含漱,用2ml加入50ml温开水中":
                //    return true;
                //case "吸入":
                //    return true;
                //case "喷喉":
                //    return true;
                //case "喷雾吸入":
                //    return true;
                //case "冲鼻":
                //    return true;
                //case "滴眼1-2滴":
                //    return true;
                //case "滴耳1-2滴":
                //    return true;
                //case "冲洗":
                //    return true;
                //case "冲洗湿敷":
                //    return true;
                //case "湿敷":
                //    return true;
                //case "热敷":
                //    return true;
                //case "外涂患处":
                //    return true;
                //case "阴道给药":
                //    return true; 
                //case "特殊外用":
                //    return true;
                //case "喷于患处":
                //    return true;
                case "i.v.-输血":
                    return false;
                case "i.v.-化疗":
                    return false;
                case "i.v.":
                    return false;
                case "iv drip-化疗":
                    return false;
                case "iv drip":
                    return false;
                case "iv drip，首剂1.5倍":
                    return false;
                case "iv drip，首剂加倍":
                    return false;
                default:
                    return true;
            }
            return true;
        }

        /// <summary>
        /// 更新医嘱页码和提取标志
        /// </summary>
        /// <returns></returns>
        private int UpdateOrderForShort()
        {
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.orderBillMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                string orderID = " "; //为了草药医嘱分三行，更新标志为的时候，如果最后一行的标志为已经更新，则continue。
                for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
                {
                    if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                        oT.PageNo = pageNo;
                        oT.RowNo = i;
                        if (orderID.Contains(oT.ID))
                            continue;
                        orderID += oT.ID + " ";

                        if (oT.Patient.ID != this.pInfo.ID)
                        {
                            continue;
                        }

                        if (oT == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("实体转换出错！");
                            return -1;
                        }

                        if (oT.GetFlag == "0")
                        {

                            if (orderManager.UpdatePageNoAndRowNo(this.pInfo.ID, oT.ID, pageNo.ToString(), i.ToString()) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新页码出错！" + oT.Item.Name);
                                return -1;
                            }

                            if (oT.ConfirmTime != DateTime.MinValue)
                            {
                                oT.GetFlag = "2";
                            }
                            else
                            {
                                oT.GetFlag = "1";

                            }
                            if (orderManager.UpdateGetFlag(this.pInfo.ID, oT.ID, oT.GetFlag, "0") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                return -1;
                            }
                            //if (this.orderManager.UpdateOrder(oT) <= 0)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                            //    return -1;
                            //}
                        }
                        else if (oT.GetFlag == "1")
                        {
                            if (oT.ConfirmTime != DateTime.MinValue)
                            {
                                //oT.GetFlag = "2";
                                if (this.orderManager.UpdateGetFlag(this.pInfo.ID, oT.ID, "2", oT.GetFlag) <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                    return -1;
                                }
                                //if (this.orderManager.UpdateOrder(oT) <= 0)
                                //{
                                //    FS.FrameWork.Management.PublicTrans.RollBack();
                                //    MessageBox.Show("更新提取标志出错！" + oT.Item.Name);
                                //    return -1;
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新医嘱打印标志出错！" + ex.Message);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// 判断是否是续打
        /// -1 错误; 0 首次续打； 1 正常续打；2 重打
        /// </summary>
        /// <returns>-1 错误; 0 首次续打； 1 正常续打；2 重打</returns>
        private int GetIsPrintAgainForShort()
        {
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return -1;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return -1;
                    }

                    if (oT.GetFlag == "0")
                    {
                        if (i == 0)
                        {
                            return 0;
                        }
                        return 1;
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.ConfirmTime != DateTime.MinValue)
                        {
                            return 1;
                        }
                    }
                }
            }

            return 2;
        }

        /// <summary>
        /// 获取打印提示
        /// </summary>
        /// <returns></returns>
        private bool CanPrintForShort(ref string errText)
        {
            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return false;
                    }
                }
            }

            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                errText = "获得页码出错！";
                return false;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                errText = "获得页码出错！";
                return false;
            }

            int maxPageNO = 0;// this.orderManager.GetMaxPageNO(this.pInfo.ID, "1");
            int ii = 0;
            if (this.orderBillMgr.GetLastOrderBillArgNew(this.PInfo.ID, "0", out maxPageNO, out ii) <= 0)

                if (pageNo > maxPageNO + 1)
                {
                    errText = "第" + (maxPageNO + 2).ToString() + "页医嘱单尚未打印！";
                    return false;
                }

            if (pageNo == maxPageNO + 1 && maxPageNO != -1)
            {
                bool canprintflag = true;
                for (int j = 0; j < (this.PageLineNO + 1); j++)
                {
                    if (this.neuSpread2.Sheets[maxPageNO].Rows[j].Tag != null)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.Sheets[maxPageNO].Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                        if (oT.PageNo != maxPageNO)
                        {
                            canprintflag = false;
                            break;
                        }

                    }
                }
                if (!canprintflag)
                {
                    //errText = "第" + (maxPageNO + 1).ToString() + "页尚有未打印医嘱！";
                    return true;
                }
            }

            //MessageBox.Show("请确定已放入第" + (pageNo + 1).ToString() + "页临时医嘱单！");

            return true;
        }

        /// <summary>
        /// 设置打印显示
        /// </summary>
        /// <returns></returns>
        private void SetPrintContentsForShort()
        {
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }

                    if (oT.GetFlag == "0")
                    {
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DoctorName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ApproveName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecTime, "");//执行日期不打
                        continue;
                    }
                    else if (oT.GetFlag == "1")
                    {
                        if (oT.Status != 3)
                        {
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Date, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Time, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DrawCombo, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DoctorName, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ApproveName, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecTime, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecNurseName, "");
                        }
                        else
                        {
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Date, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Time, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DrawCombo, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DoctorName, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ApproveName, "");
                            this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecTime, "");
                        }

                        this.lblPageShort.Visible = false;
                        this.SetValueVisible(false, EnumOrderType.Short);
                    }
                    else
                    {
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Date, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Time, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DrawCombo, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DoctorName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ApproveName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecTime, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecNurseName, "");

                        this.lblPageShort.Visible = false;
                        this.SetValueVisible(false, EnumOrderType.Short);
                    }
                }
            }
        }

        /// <summary>
        /// 设置临时医嘱重打显示内容
        /// </summary>
        private void SetRePrintContentsForShort()
        {
            if (this.neuSpread2.ActiveSheet.Tag == null)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            int pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag.ToString());

            if (pageNo < 0)
            {
                MessageBox.Show("获得页码出错！");
                return;
            }

            for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order oT = this.neuSpread2.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (oT == null)
                    {
                        MessageBox.Show("实体转换出错！");
                        return;
                    }
                    if (oT.GetFlag == "0")
                    {
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Date, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Time, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DrawCombo, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DoctorName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecTime, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ApproveName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecNurseName, "");//执行日期不打
                    }
                    else
                    {
                        //this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DoctorName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecTime, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ApproveName, "");//执行日期不打
                    }
                }
            }

            this.lblPageShort.Visible = true;
        }

        /// <summary>
        /// 添加到Fp
        /// </summary>
        /// <param name="al"></param>
        private void AddObjectToFpShort(ArrayList al)
        {

            #region 为空返回
            if (al.Count <= 0)
            {
                return;
            }
            #endregion

            #region 定义变量
            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNO = new Hashtable();
            ArrayList alPageNO = new ArrayList();

            int MaxPageNO = -1;
            int MaxRowNO = -1;
            #endregion

            #region 清空数据

            this.neuSpread2_Sheet1.Cells[0, 0, this.neuSpread2_Sheet1.RowCount - 1, this.neuSpread2_Sheet1.ColumnCount - 1].Text = "";
            #endregion

            #region 保留一个Sheet
            if (this.neuSpread2.Sheets.Count > 1)
            {
                for (int j = this.neuSpread2.Sheets.Count - 1; j > 0; j--)
                {
                    this.neuSpread2.Sheets.RemoveAt(j);
                }
            }
            #endregion

            #region 护嘱调整
            if (this.isNurseOrder)
            {
                this.lbs1.Text = "东莞市人民医院临时护嘱单";
                neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "临 时 护 嘱";
                neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "开立/执行护士签名";
                neuSpread2_Sheet1.Columns[4].Width = 120;
                neuSpread2_Sheet1.Columns[6].Visible = !this.isNurseOrder;
                this.lblDoctSignShort.Text = "护士签名:";
                this.lblNurseSignShort.Visible = false;
            }
            #endregion

            #region 按是否打印分组
            foreach (FS.HISFC.Models.Order.Inpatient.Order temp in al)
            {
                if (temp.PageNo == -1)
                {
                    alPageNull.Insert(alPageNull.Count, temp);
                }
                else
                {
                    if (!hsPageNO.ContainsKey(temp.PageNo))
                    {
                        alPageNO.Insert(alPageNO.Count, temp.PageNo);

                        hsPageNO.Add(temp.PageNo, new ArrayList());

                        (hsPageNO[temp.PageNo] as ArrayList).Insert((hsPageNO[temp.PageNo] as ArrayList).Count, temp);
                    }
                    else
                    {
                        (hsPageNO[temp.PageNo] as ArrayList).Insert((hsPageNO[temp.PageNo] as ArrayList).Count, temp);
                    }
                }
            }
            #endregion

            #region 将已打印的显示
            for (int i = 0; i < alPageNO.Count; i++)
            {
                int pageNo = FS.FrameWork.Function.NConvert.ToInt32(alPageNO[i].ToString());

                if (FS.FrameWork.Function.NConvert.ToInt32(pageNo) > MaxPageNO)
                {
                    MaxPageNO = FS.FrameWork.Function.NConvert.ToInt32(pageNo);
                    MaxRowNO = -1;
                }

                ArrayList alTemp = hsPageNO[pageNo] as ArrayList;

                if (i == 0)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order o in alTemp)
                    {

                        //List<FS.HISFC.Models.Order.Inpatient.OrderExtend> orderExtendInfoList = this.orderExtendMgr.QueryByInpatineNoOrderID(this.pInfo.ID, o.ID);
                        //if (orderExtendInfoList == null)
                        //{
                        //    MessageBox.Show("查询医嘱" + o.Item.Name + "扩展信息失败!");
                        //    return;
                        //}

                        this.neuSpread2_Sheet1.Rows[o.RowNo].BackColor = Color.MistyRose;
                        this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.Date, o.BeginTime.ToString("MM-dd"));
                        this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.Time, o.BeginTime.ToString("HH:mm"));
                        this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.DoctorName, o.ReciptDoctor.Name);

                        #region 如果有签名，此处打印签名护士
                        //if (orderExtendInfoList.Count > 0)
                        //{
                        //    o.ConfirmTime = FrameWork.Function.NConvert.ToDateTime(orderExtendInfoList[0].Extend4);
                        //    //如果有签名就取签名，否则就取电脑执行护士
                        //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                        //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                        //    this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.ApproveName, name == "/" ? "" : name);

                        //    //处方权受限的医嘱，打印上级审核医生名字
                        //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                        //    {
                        //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                        //        if (!string.IsNullOrEmpty(privDoctName))
                        //        {
                        //            this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + o.ReciptDoctor.Name);
                        //        }
                        //    }

                        //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                        //    {
                        //        this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.DoctorName, o.ReciptDoctor.Name
                        //             + "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                        //    }
                        //}
                        //else
                        //{
                        //    this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.ApproveName, this.emplHelPer.GetName(o.Nurse.ID));
                        //}
                        #endregion

                        if (o.ConfirmTime != DateTime.MinValue)
                        {
                            //this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecTime, o.ConfirmTime.ToString("MM-dd HH:mm"));
                        }
                        //this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecNurseName, o.Nurse.Name);

                        this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.ComboID, o.Combo.ID);
                        //this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.DrawCombo, "");
                        string tempstr = string.Empty;
                        if (o.MOTime.Date != o.BeginTime.Date)
                        {
                            tempstr = o.BeginTime.ToString("MM.dd") + "执行";
                        }
                        string frequency = o.Frequency.ID;
                        if (htSpecFrequency != null && htSpecFrequency.Contains(o.Frequency.ID))
                        {
                            frequency = htSpecFrequency[o.Frequency.ID].ToString();
                        }
                        if (o.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (o.DoseOnce > 0)
                            {

                                if (o.OrderType.ID == "CD")
                                {
                                    this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Item.Specs + " " + o.Qty.ToString() + o.Unit + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " + o.Memo + " " + "(出院带药)");
                                }
                                else if (o.OrderType.ID == "BL")
                                {
                                    this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " + o.Qty.ToString() + o.Unit + " " + o.Memo + "(补录医嘱)");
                                }
                                else
                                {
                                    //是否打印总量
                                    if (!IsPrintQTY(o))
                                    {
                                        this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " +/* o.Qty.ToString() + o.Unit + " " +*/o.Memo + " " + GetEmergencyTip(o.IsEmergency) + tempstr);
                                    }
                                    else
                                    {
                                        this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " + o.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + o.Unit + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency) + tempstr);
                                    }
                                }

                            }
                            else
                            {
                                //处理每次量是0的医嘱，主要是中草药或换行医嘱
                                this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name);
                            }
                        }
                        else
                        {
                            //检查、检验
                            if (o.Item.SysClass.ID.ToString() == "UC" || o.Item.SysClass.ID.ToString() == "UL")
                            {
                                this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Item.Qty.ToString() + o.Unit + " " + o.Memo + " " + o.Sample.Name + GetEmergencyTip(o.IsEmergency) + tempstr);
                            }
                            //手术
                            else if (o.Item.SysClass.ID.ToString() == "UO" && o.Item.Name.IndexOf("术") >= 0)
                            {
                                this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency) + tempstr);
                            }
                            else if (o.Item.SysClass.ID.ToString() == "M")
                            {
                                this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Memo + " " + tempstr);
                            }
                            else
                            {
                                this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Qty.ToString() + o.Unit + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency) + tempstr);
                            }


                        }
                        if (o.ConfirmTime != DateTime.MinValue)
                        {
                            //this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecTime, o.ConfirmTime.ToString("MM-dd"));
                            //this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecTime, o.ConfirmTime.ToString("HH:mm"));
                            this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecTime, o.ConfirmTime.ToString("MM-dd HH:mm"));
                            this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecNurseName, o.Nurse.Name);
                        }

                        //Add by zuowy 2010.6.29 作废的临嘱显示取消字样

                        if (o.Status == 3)
                        {
                            this.neuSpread2_Sheet1.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, this.neuSpread2_Sheet1.GetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName).ToString() + " [取消]");
                        }

                        this.neuSpread2_Sheet1.Rows[o.RowNo].Tag = o;

                        if (pageNo == MaxPageNO && o.RowNo > MaxRowNO)
                        {
                            MaxRowNO = o.RowNo;
                        }
                    }

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(this.neuSpread2_Sheet1, (int)EnumShortOrderPrint.ComboID, (int)EnumShortOrderPrint.DrawCombo);

                    this.neuSpread2_Sheet1.Tag = pageNo;
                    this.neuSpread2_Sheet1.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "页";
                }
                else
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(ref sheet);

                    this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, sheet);

                    foreach (FS.HISFC.Models.Order.Inpatient.Order o in alTemp)
                    {
                        //List<FS.HISFC.Models.Order.Inpatient.OrderExtend> orderExtendInfoList = this.orderExtendMgr.QueryByInpatineNoOrderID(this.pInfo.ID, o.ID);
                        //if (orderExtendInfoList == null)
                        //{
                        //    MessageBox.Show("查询医嘱" + o.Item.Name + "扩展信息失败!");
                        //    return;
                        //}

                        sheet.Rows[o.RowNo].BackColor = Color.MistyRose;
                        sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.Date, o.BeginTime.ToString("MM-dd"));
                        sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.Time, o.BeginTime.ToString("HH:mm"));
                        sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.DoctorName, o.ReciptDoctor.Name);

                        #region 如果有签名，此处打印签名护士
                        //if (orderExtendInfoList.Count > 0)
                        //{
                        //    o.ConfirmTime = FrameWork.Function.NConvert.ToDateTime(orderExtendInfoList[0].Extend4);

                        //    //如果有签名就取签名，否则就取电脑执行护士
                        //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                        //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                        //    sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.ApproveName, name == "/" ? "" : name);

                        //    //处方权受限的医嘱，打印上级审核医生名字
                        //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                        //    {
                        //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                        //        if (!string.IsNullOrEmpty(privDoctName))
                        //        {
                        //            sheet.SetValue(o.RowNo, (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + o.ReciptDoctor.Name);
                        //        }
                        //    }

                        //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                        //    {
                        //        sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.DoctorName, o.ReciptDoctor.Name
                        //             + "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                        //    }
                        //}
                        //else
                        //{
                        //    //sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.ApproveName, this.emplHelPer.GetName(o.Nurse.ID));
                        //}
                        #endregion

                        if (o.ConfirmTime != DateTime.MinValue)
                        {
                            sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecTime, o.ConfirmTime.ToString("MM-dd HH:mm"));
                        }

                        sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecNurseName, o.Nurse.Name);

                        sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.ComboID, o.Combo.ID);
                        //sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.DrawCombo, "");
                        string tempstr = string.Empty;
                        if (o.MOTime.Date != o.BeginTime.Date)
                        {
                            tempstr = o.BeginTime.ToString("MM.dd") + "执行";
                        }
                        string frequency = o.Frequency.ID;
                        if (htSpecFrequency != null && htSpecFrequency.Contains(o.Frequency.ID))
                        {
                            frequency = htSpecFrequency[o.Frequency.ID].ToString();
                        }
                        if (o.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (o.DoseOnce > 0)
                            {
                                if (o.OrderType.ID == "CD")
                                {
                                    sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " + o.Qty.ToString() + o.Unit + "(出院带药)" + " " + o.Memo);
                                }
                                else if (o.OrderType.ID == "BL")
                                {
                                    sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " + o.Qty.ToString() + o.Unit + "(补录医嘱)" + " " + o.Memo);
                                }
                                else
                                {
                                    //如果是中药，不打印总量
                                    if (!IsPrintQTY(o))
                                    {
                                        sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " +/* o.Qty.ToString() + o.Unit + " " +*/o.Memo + " " + GetEmergencyTip(o.IsEmergency) + tempstr);
                                    }
                                    else
                                    {
                                        sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (o.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + o.Usage.Name + " " + frequency + " " + o.Qty.ToString() + o.Unit + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency) + tempstr);
                                    }
                                }

                            }
                            else
                            {
                                sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name);
                            }
                        }
                        else
                        {

                            //检查、检验
                            if (o.Item.SysClass.ID.ToString() == "UC" || o.Item.SysClass.ID.ToString() == "UL")
                            {
                                sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Item.Qty.ToString() + o.Unit + " " + o.Memo + " " + o.Sample.Name + " " + GetEmergencyTip(o.IsEmergency) + tempstr);
                            }
                            //手术
                            else if (o.Item.SysClass.ID.ToString() == "UO" && o.Item.Name.IndexOf("术") >= 0)
                            {
                                sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency) + tempstr);
                            }
                            else if (o.Item.SysClass.ID.ToString() == "M")
                            {
                                sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Memo + " " + tempstr);
                            }
                            else
                            {
                                sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, o.Item.Name + " " + o.Qty.ToString() + o.Unit + " " + o.Memo + " " + GetEmergencyTip(o.IsEmergency) + tempstr);
                            }


                        }

                        //Add by zuowy 2010.6.29 作废的临嘱显示取消字样

                        if (o.Status == 3)
                        {
                            sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName, sheet.GetValue(o.RowNo, (int)EnumShortOrderPrint.OrderName).ToString() + " [取消]");
                        }

                        if (o.ConfirmTime != DateTime.MinValue)
                        {
                            //sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecTime, o.ConfirmTime.ToString("MM-dd"));
                            //sheet.SetValue(o.RowNo, (int)EnumShortOrderPrint.ExecTime, o.ConfirmTime.ToString("HH:mm"));
                            //sheet.SetValue(o.RowNO,8,o.User_Exec.Name);
                        }
                        sheet.Rows[o.RowNo].Tag = o;

                        if (pageNo == MaxPageNO && o.RowNo > MaxRowNO)
                        {
                            MaxRowNO = o.RowNo;
                        }
                    }

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(sheet, (int)EnumShortOrderPrint.ComboID, (int)EnumShortOrderPrint.DrawCombo);

                    sheet.Tag = pageNo;
                    sheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(pageNo) + 1).ToString() + "页";
                }

            }
            #endregion

            #region 显示未打印医嘱

            bool fromOne = true;
            int iniIndex = -1;
            int endIndex = -1;

            if (MaxPageNO == -1)
            {
                MaxPageNO++;
                fromOne = false;
            }

            if (MaxRowNO == -1)
            {
                MaxRowNO++;
            }

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread2.Sheets[MaxPageNO];

            activeSheet.Tag = MaxPageNO++;
            activeSheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNO)).ToString() + "页";

            if (fromOne)
            {
                iniIndex = 1;
                endIndex = alPageNull.Count + 1;
            }
            else
            {
                iniIndex = 0;
                endIndex = alPageNull.Count;
            }

            for (; iniIndex < endIndex; iniIndex++)
            {
                FS.HISFC.Models.Order.Inpatient.Order oTemp;

                if (fromOne)
                {
                    oTemp = alPageNull[iniIndex - 1] as FS.HISFC.Models.Order.Inpatient.Order;
                }
                else
                {
                    oTemp = alPageNull[iniIndex] as FS.HISFC.Models.Order.Inpatient.Order;
                }

                //List<FS.HISFC.Models.Order.Inpatient.OrderExtend> orderExtendInfoList = this.orderExtendMgr.QueryByInpatineNoOrderID(this.pInfo.ID, oTemp.ID);
                //if (orderExtendInfoList == null)
                //{
                //    MessageBox.Show("查询医嘱" + oTemp.Item.Name + "扩展信息失败!");
                //    return;
                //}

                if ((iniIndex + MaxRowNO) % (this.PageLineNO + 1) == 0 && (iniIndex + MaxRowNO) != 0)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(ref sheet);

                    this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNO++;
                    activeSheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNO)).ToString() + "页";

                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.Date, oTemp.BeginTime.ToString("MM-dd"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.Time, oTemp.BeginTime.ToString("HH:mm"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.DoctorName, oTemp.ReciptDoctor.Name);

                    #region 如果有签名，此处打印签名护士
                    //if (orderExtendInfoList.Count > 0)
                    //{
                    //    oTemp.ConfirmTime = FrameWork.Function.NConvert.ToDateTime(orderExtendInfoList[0].Extend4);

                    //    //如果有签名就取签名，否则就取电脑执行护士
                    //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                    //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, name == "/" ? "" : name);

                    //    //处方权受限的医嘱，打印上级审核医生名字
                    //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                    //    {
                    //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                    //        if (!string.IsNullOrEmpty(privDoctName))
                    //        {
                    //            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + oTemp.ReciptDoctor.Name);
                    //        }
                    //    }

                    //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                    //    {
                    //        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.DoctorName, oTemp.ReciptDoctor.Name
                    //             + "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                    //    }
                    //}
                    //else
                    //{
                    //    //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, this.emplHelPer.GetName(oTemp.Nurse.ID));
                    //}
                    #endregion

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("MM-dd HH:mm"));
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecNurseName, oTemp.Nurse.Name);
                    string tempstr = string.Empty;
                    if (oTemp.MOTime.Date != oTemp.BeginTime.Date)
                    {
                        tempstr = oTemp.BeginTime.ToString("MM.dd") + "执行";
                    }
                    string frequency = oTemp.Frequency.ID;
                    if (htSpecFrequency != null && htSpecFrequency.Contains(oTemp.Frequency.ID))
                    {
                        frequency = htSpecFrequency[oTemp.Frequency.ID].ToString();
                    }
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (oTemp.DoseOnce > 0)
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + "(出院带药)");
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + "(补录医嘱)");
                            }
                            else
                            {
                                //如果是中药，不打印总量
                                if (!IsPrintQTY(oTemp))
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + /*oTemp.Qty.ToString() + oTemp.Unit+ " "  + */oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                                }
                            }

                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name);
                        }
                    }
                    else
                    {
                        //以后修改东西 ，看明白了再修改！！！别乱改
                        //检查、检验
                        if (oTemp.Item.SysClass.ID.ToString() == "UC" || oTemp.Item.SysClass.ID.ToString() == "UL")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Item.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + oTemp.Sample.Name + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                        //手术
                        else if (oTemp.Item.SysClass.ID.ToString() == "UO" && oTemp.Item.Name.IndexOf("术") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                        else if (oTemp.Item.SysClass.ID.ToString() == "M")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Memo + " " + tempstr);
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }


                    }

                    if (oTemp.Status == 3)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, activeSheet.GetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName).ToString() + " [取消]");
                    }

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO +1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("MM-dd"));
                        //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO +1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("HH:mm"));
                        //activeSheet.SetValue((iniIndex+MaxRowNO)% 25,8,oTemp.User_Exec.Name);
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ComboID, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNO) % (this.PageLineNO + 1)].Tag = oTemp;

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(activeSheet, (int)EnumShortOrderPrint.ComboID, (int)EnumShortOrderPrint.DrawCombo);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.Date, oTemp.BeginTime.ToString("MM-dd"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.Time, oTemp.BeginTime.ToString("HH:mm"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.DoctorName, oTemp.ReciptDoctor.Name);

                    #region 如果有签名，此处打印签名护士
                    //if (orderExtendInfoList.Count > 0)
                    //{
                    //    oTemp.ConfirmTime = FrameWork.Function.NConvert.ToDateTime(orderExtendInfoList[0].Extend4);
                    //    //如果有签名就取签名，否则就取电脑执行护士
                    //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                    //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, name == "/" ? "" : name);

                    //    //处方权受限的医嘱，打印上级审核医生名字
                    //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                    //    {
                    //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                    //        if (!string.IsNullOrEmpty(privDoctName))
                    //        {
                    //            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + oTemp.ReciptDoctor.Name);
                    //        }
                    //    }

                    //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                    //    {
                    //        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.DoctorName, oTemp.ReciptDoctor.Name
                    //             + "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                    //    }
                    //}
                    //else
                    //{
                    //    //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, this.emplHelPer.GetName(oTemp.Nurse.ID));
                    //}
                    #endregion
                    //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, this.emplHelPer.GetName(oTemp.Nurse.ID));
                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("MM-dd HH:mm"));
                    }
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecNurseName, oTemp.Nurse.Name);

                    string tempstr = string.Empty;
                    if (oTemp.MOTime.Date != oTemp.BeginTime.Date)
                    {
                        tempstr = oTemp.BeginTime.ToString("MM.dd") + "执行";
                    }
                    string frequency = oTemp.Frequency.ID;
                    if (htSpecFrequency != null && htSpecFrequency.Contains(oTemp.Frequency.ID))
                    {
                        frequency = htSpecFrequency[oTemp.Frequency.ID].ToString();
                    }
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (oTemp.DoseOnce > 0)
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + "(补录医嘱)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                //如果是中药，不打印总量
                                if (!IsPrintQTY(oTemp))
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + /*oTemp.Qty.ToString() + oTemp.Unit+ " "  + */oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                                }
                            }

                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name);
                        }
                    }
                    else
                    {

                        //检查、检验
                        if (oTemp.Item.SysClass.ID.ToString() == "UC" || oTemp.Item.SysClass.ID.ToString() == "UL")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Item.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + oTemp.Sample.Name + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                        //手术
                        else if (oTemp.Item.SysClass.ID.ToString() == "UO" && oTemp.Item.Name.IndexOf("术") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                        else if (oTemp.Item.SysClass.ID.ToString() == "M")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Memo + " " + tempstr);
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }


                    }
                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO +1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("MM-dd"));
                        //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO +1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("HH:mm"));
                        //activeSheet.SetValue((iniIndex+MaxRowNO)% 25,8,oTemp.User_Exec.Name);
                    }

                    if (oTemp.Status == 3)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, activeSheet.GetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName).ToString() + " [取消]");
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ComboID, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNO) % (this.PageLineNO + 1)].Tag = oTemp;

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(activeSheet, (int)EnumShortOrderPrint.ComboID, (int)EnumShortOrderPrint.DrawCombo);
                }
            }

            #endregion

            DealShortOrderCrossPage();
        }

        /// <summary>
        /// 转科分页后添加到Fp
        /// </summary>
        /// <param name="al"></param>
        private void AddObjectToFpShortAfter()
        {

            #region 定义变量

            ArrayList alPageNull = new ArrayList();
            ArrayList alPageHave = new ArrayList();
            Hashtable hsPageNO = new Hashtable();
            ArrayList alPageNO = new ArrayList();

            int MaxPageNO = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread2.ActiveSheet.Tag) + 1;
            int MaxRowNO = -1;

            int row = this.neuSpread2.ActiveSheet.ActiveRowIndex;

            if (this.neuSpread2.ActiveSheet.Rows[row].Tag == null)
            {
                MessageBox.Show("所选项目为空！");
                return;
            }

            FS.HISFC.Models.Order.Inpatient.Order ord = this.neuSpread2.ActiveSheet.Rows[row].Tag as FS.HISFC.Models.Order.Inpatient.Order;

            if (MessageBox.Show("确定要从" + ord.Item.Name + "开始另起一页吗?此操作不可撤销！", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            if (ord.PageNo >= 0 || ord.RowNo >= 0)
            {
                MessageBox.Show(ord.Item.Name + "已经打印过，不能另起一页！");
                return;
            }

            #endregion

            #region 获取剩余数据

            for (int i = this.neuSpread2.ActiveSheet.ActiveRowIndex; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread2.ActiveSheet.Rows[i].Tag != null)
                {
                    alPageNull.Add(this.neuSpread2.ActiveSheet.Rows[i].Tag);
                }
            }

            for (int i = this.neuSpread2.ActiveSheetIndex + 1; i < this.neuSpread2.Sheets.Count; i++)
            {
                for (int j = 0; j < this.neuSpread2.Sheets[i].Rows.Count; j++)
                {
                    if (this.neuSpread2.Sheets[i].Rows[j].Tag != null)
                    {
                        alPageNull.Add(this.neuSpread2.Sheets[i].Rows[j].Tag);
                    }
                }
            }

            #endregion

            #region 清空数据

            for (int j = row; j < this.neuSpread2.ActiveSheet.Rows.Count; j++)
            {
                this.neuSpread2.ActiveSheet.SetValue(j, 0, "");
                this.neuSpread2.ActiveSheet.SetValue(j, 1, "");
                this.neuSpread2.ActiveSheet.SetValue(j, 2, "");
                this.neuSpread2.ActiveSheet.SetValue(j, 3, "");
                this.neuSpread2.ActiveSheet.SetValue(j, 4, "");
                this.neuSpread2.ActiveSheet.SetValue(j, 5, "");
                this.neuSpread2.ActiveSheet.SetValue(j, 6, "");
                this.neuSpread2.ActiveSheet.Rows[j].Tag = null;
            }

            #endregion

            #region 保留一个Sheet

            if (this.neuSpread2.Sheets.Count > 1)
            {
                for (int j = this.neuSpread2.Sheets.Count - 1; j > this.neuSpread2.ActiveSheetIndex; j--)
                {
                    this.neuSpread2.Sheets.RemoveAt(j);
                }
            }

            #endregion

            #region 显示未打印医嘱

            int iniIndex = -1;
            int endIndex = -1;

            if (MaxRowNO == -1)
            {
                MaxRowNO++;
            }

            FarPoint.Win.Spread.SheetView orgSheet = new FarPoint.Win.Spread.SheetView();

            this.InitShortSheet(ref orgSheet);

            this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, orgSheet);

            FarPoint.Win.Spread.SheetView activeSheet = this.neuSpread2.Sheets[MaxPageNO];


            activeSheet.Tag = MaxPageNO++;
            activeSheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNO)).ToString() + "页";

            iniIndex = 0;
            endIndex = alPageNull.Count;

            for (; iniIndex < endIndex; iniIndex++)
            {
                FS.HISFC.Models.Order.Inpatient.Order oTemp;

                oTemp = alPageNull[iniIndex] as FS.HISFC.Models.Order.Inpatient.Order;
                string tempstr = string.Empty;
                if (oTemp.MOTime.Date != oTemp.BeginTime.Date)
                {
                    tempstr = oTemp.BeginTime.ToString("MM.dd") + "执行";
                }

                //List<FS.HISFC.Models.Order.Inpatient.OrderExtend> orderExtendInfoList = this.orderExtendMgr.QueryByInpatineNoOrderID(this.pInfo.ID, oTemp.ID);
                //if (orderExtendInfoList == null)
                //{
                //    MessageBox.Show("查询医嘱" + oTemp.Item.Name + "扩展信息失败!");
                //    return;
                //}

                if ((iniIndex + MaxRowNO) % (this.PageLineNO + 1) == 0 && (iniIndex + MaxRowNO) != 0)
                {
                    FarPoint.Win.Spread.SheetView sheet = new FarPoint.Win.Spread.SheetView();

                    this.InitShortSheet(ref sheet);

                    this.neuSpread2.Sheets.Insert(this.neuSpread2.Sheets.Count, sheet);

                    activeSheet = sheet;

                    activeSheet.Tag = MaxPageNO++;
                    activeSheet.SheetName = "第" + (FS.FrameWork.Function.NConvert.ToInt32(MaxPageNO)).ToString() + "页";

                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.Date, oTemp.BeginTime.ToString("MM-dd"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.Time, oTemp.BeginTime.ToString("HH:mm"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.DoctorName, oTemp.ReciptDoctor.Name);

                    #region 如果有签名，此处打印签名护士
                    //if (orderExtendInfoList.Count > 0)
                    //{
                    //    //如果有签名就取签名，否则就取电脑执行护士
                    //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                    //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, name == "/" ? "" : name);

                    //    //处方权受限的医嘱，打印上级审核医生名字
                    //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                    //    {
                    //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                    //        if (!string.IsNullOrEmpty(privDoctName))
                    //        {
                    //            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + oTemp.ReciptDoctor.Name);
                    //        }
                    //    }

                    //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                    //    {
                    //        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.DoctorName, oTemp.ReciptDoctor.Name
                    //             + "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                    //    }
                    //}
                    //else
                    //{
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, emplHelPer.GetName(oTemp.Nurse.ID));
                    //}
                    #endregion


                    //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, this.emplHelPer.GetName(oTemp.Nurse.ID));
                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("MM-dd HH:mm"));
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecNurseName, oTemp.Nurse.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ComboID, oTemp.Combo.ID);
                    string frequency = oTemp.Frequency.ID;
                    if (htSpecFrequency != null && htSpecFrequency.Contains(oTemp.Frequency.ID))
                    {
                        frequency = htSpecFrequency[oTemp.Frequency.ID].ToString();
                    }
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (oTemp.DoseOnce > 0)
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + "(补录医嘱)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                //如果是中药，不打印总量
                                if (!IsPrintQTY(oTemp))
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                                }
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), 4, oTemp.Item.Name);
                        }
                    }
                    else
                    {
                        //以后修改东西 ，看明白了再修改！！！别乱改
                        //检查、检验
                        if (oTemp.Item.SysClass.ID.ToString() == "UC" || oTemp.Item.SysClass.ID.ToString() == "UL")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Item.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + oTemp.Sample.Name + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                        //手术
                        else if (oTemp.Item.SysClass.ID.ToString() == "UO" && oTemp.Item.Name.IndexOf("术") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                        else if (oTemp.Item.SysClass.ID.ToString() == "M")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Memo + " " + tempstr);
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                    }

                    if (oTemp.Status == 3)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, activeSheet.GetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName).ToString() + " [取消]");
                    }

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {

                        if (oTemp.ConfirmTime != DateTime.MinValue)
                        {
                            //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("MM-dd"));
                            //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("HH:mm"));
                        }
                        //activeSheet.SetValue((iniIndex+MaxRowNO)% 25,8,oTemp.User_Exec.Name);
                    }
                    //activeSheet.SetValue((iniIndex + MaxRowNO) % 25, 9, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNO) % (this.PageLineNO + 1)].Tag = oTemp;

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(activeSheet, (int)EnumShortOrderPrint.ComboID, (int)EnumShortOrderPrint.DrawCombo);
                }
                else
                {
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.Date, oTemp.BeginTime.ToString("MM-dd"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.Time, oTemp.BeginTime.ToString("HH:mm"));
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.DoctorName, oTemp.ReciptDoctor.Name);

                    #region 如果有签名，此处打印签名护士
                    //if (orderExtendInfoList.Count > 0)
                    //{
                    //    //如果有签名就取签名，否则就取电脑执行护士
                    //    string name = string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend1)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend1) : emplHelPer.GetName(orderExtendInfoList[0].Extend1);//审核者
                    //    name += "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2));//执行者
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, name == "/" ? "" : name);

                    //    //处方权受限的医嘱，打印上级审核医生名字
                    //    if (orderExtendInfoList[0].Extend5 == "LimitDrug" && !string.IsNullOrEmpty(orderExtendInfoList[0].Extend6))
                    //    {
                    //        string privDoctName = emplHelPer.GetName(orderExtendInfoList[0].Extend6);
                    //        if (!string.IsNullOrEmpty(privDoctName))
                    //        {
                    //            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumLongOrderPrint.DoctorName, privDoctName + "/" + oTemp.ReciptDoctor.Name);
                    //        }
                    //    }

                    //    if (this.isNurseOrder && !string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)))
                    //    {
                    //        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.DoctorName, oTemp.ReciptDoctor.Name
                    //             + "/" + (string.IsNullOrEmpty(emplHelPer.GetName(orderExtendInfoList[0].Extend2)) ? emplHelPer.GetName(orderExtendInfoList[0].Extend2) : emplHelPer.GetName(orderExtendInfoList[0].Extend2)));
                    //    }
                    //}
                    //else
                    //{
                    //    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ApproveName, emplHelPer.GetName(oTemp.Nurse.ID));
                    //}
                    #endregion

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("MM-dd HH:mm"));
                    }

                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecNurseName, oTemp.Nurse.Name);
                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ComboID, oTemp.Combo.ID);
                    string frequency = oTemp.Frequency.ID;
                    if (htSpecFrequency != null && htSpecFrequency.Contains(oTemp.Frequency.ID))
                    {
                        frequency = htSpecFrequency[oTemp.Frequency.ID].ToString();
                    }
                    if (oTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (oTemp.DoseOnce > 0)
                        {
                            if (oTemp.OrderType.ID == "CD")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + "(出院带药)" + " " + oTemp.Memo);
                            }
                            else if (oTemp.OrderType.ID == "BL")
                            {
                                activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + "(补录医嘱)" + " " + oTemp.Memo);
                            }
                            else
                            {
                                //如果是中药，不打印总量
                                if (!IsPrintQTY(oTemp))
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                                }
                                else
                                {
                                    activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + (oTemp.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit + " " + " " + oTemp.Usage.Name + " " + frequency + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                                }
                            }
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name);
                        }
                    }
                    else
                    {

                        //检查、检验
                        if (oTemp.Item.SysClass.ID.ToString() == "UC" || oTemp.Item.SysClass.ID.ToString() == "UL")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Item.Qty.ToString() + oTemp.Unit + " " + oTemp.Memo + " " + oTemp.Sample.Name + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                        //手术
                        else if (oTemp.Item.SysClass.ID.ToString() == "UO" && oTemp.Item.Name.IndexOf("术") >= 0)
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                        else if (oTemp.Item.SysClass.ID.ToString() == "M")
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Memo + " " + tempstr);
                        }
                        else
                        {
                            activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, oTemp.Item.Name + " " + oTemp.Qty.ToString() + oTemp.Unit + " " + GetEmergencyTip(oTemp.IsEmergency) + tempstr);
                        }
                    }

                    if (oTemp.Status == 3)
                    {
                        activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName, activeSheet.GetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.OrderName).ToString() + " [取消]");
                    }

                    if (oTemp.ConfirmTime != DateTime.MinValue)
                    {
                        if (oTemp.ConfirmTime != DateTime.MinValue)
                        {
                            //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("MM-dd"));
                            //activeSheet.SetValue((iniIndex + MaxRowNO) % (this.PageLineNO + 1), (int)EnumShortOrderPrint.ExecTime, oTemp.ConfirmTime.ToString("HH:mm"));
                        }
                        //activeSheet.SetValue((iniIndex+MaxRowNO)% 25,8,oTemp.User_Exec.Name);
                    }
                    //activeSheet.SetValue((iniIndex + MaxRowNO) % 25, 9, oTemp.Combo.ID);
                    activeSheet.Rows[(iniIndex + MaxRowNO) % (this.PageLineNO + 1)].Tag = oTemp;

                    FS.HISFC.Components.Common.Classes.Function.DrawCombo(activeSheet, (int)EnumShortOrderPrint.ComboID, (int)EnumShortOrderPrint.DrawCombo);
                }
            }

            bHaveSplitPage = true;

            #endregion

        }

        /// <summary>
        /// 初始化新Sheet
        /// </summary>
        /// <param name="sheet"></param>
        private void InitShortSheet(ref FarPoint.Win.Spread.SheetView sheet)
        {
            sheet.Reset();
            sheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            sheet.ColumnCount = 9;
            sheet.RowCount = 22;
            sheet.RowHeader.ColumnCount = 0;
            sheet.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 0).Value = "日 期";
            sheet.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 1).Value = "时 间";
            sheet.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 2;
            sheet.ColumnHeader.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 2).Value = "临  时  医  嘱";
            sheet.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 4).Value = "医 生 签 名";

            sheet.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 5).Value = "执 行 时 间";
            sheet.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 11F);
            sheet.ColumnHeader.Cells.Get(0, 6).Value = "核   对  者 签 名";
            sheet.Columns[6].Visible = !this.isNurseOrder;

            sheet.ColumnHeader.Cells.Get(0, 7).Value = "执 行 护 士 签 名";
            sheet.ColumnHeader.Cells.Get(0, 8).Value = "组合号";
            sheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.ColumnHeader.DefaultStyle.Locked = false;
            sheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            sheet.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            sheet.ColumnHeader.Rows.Get(0).Height = 57F;
            sheet.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            sheet.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F);
            sheet.Columns.Get(0).Label = "日 期";
            sheet.Columns.Get(0).Width = 45F;
            sheet.Columns.Get(1).Label = "时 间";
            sheet.Columns.Get(1).Width = 54F;
            sheet.Columns.Get(2).Label = "临  时  医  嘱";
            sheet.Columns.Get(2).Width = 20F;
            textCellType1.WordWrap = true;
            sheet.Columns.Get(3).CellType = textCellType1;
            sheet.Columns.Get(4).CellType = textCellType1;
            sheet.Columns.Get(3).Width = 348F;
            sheet.Columns.Get(4).Label = "医 生 签 名";
            sheet.Columns.Get(4).Width = 62F;
            sheet.Columns.Get(5).Width = 100F;
            sheet.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            sheet.Columns.Get(6).Label = "核对/执行护士签名";
            sheet.Columns.Get(6).Width = 106F;
            sheet.Columns.Get(7).Label = "执 行 护 士 签 名";
            sheet.Columns.Get(7).Width = 88F;
            sheet.Columns.Get(7).Visible = false;
            sheet.Columns.Get(8).Label = "组合号";
            sheet.Columns.Get(8).Visible = false;
            sheet.Columns.Get(8).Width = 61F;
            sheet.DefaultStyle.BackColor = System.Drawing.Color.White;
            sheet.DefaultStyle.Locked = false;

            if (this.isNurseOrder)
            {
                sheet.ColumnHeader.Cells.Get(0, 2).Value = "临 时 护 嘱";
                sheet.ColumnHeader.Cells.Get(0, 4).Value = "开立/执行护士签名";
                sheet.Columns[4].Width = 120;
            }

            sheet.DefaultStyle.Parent = "DataAreaDefault";
            sheet.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            sheet.RowHeader.Columns.Default.Resizable = false;
            sheet.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.White, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ActiveCaptionText);
            sheet.Rows.Default.Height = 39.49F;
            sheet.Rows.Default.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            sheet.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            sheet.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            sheet.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.White);
            sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
        }

        #endregion

        #region 打印

        #region 续打模式

        /// <summary>
        /// 续打
        /// </summary>
        private void Print()
        {
            DialogResult rr = MessageBox.Show("注意：请确认使用的是否为新医嘱单。如果是第一次使用新医嘱单打印，请将所有未打印医嘱另起一页后打印！确定要打印该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rr == DialogResult.No)
            {
                return;
            }

            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 1030, 1200);
            p.SetPageSize(size);
            p.IsCanCancel = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            string errText = "";
            frmNotice frmNotice = new frmNotice();
            string dtBegin = "";
            string dtEnd = "";

            #region 长期医嘱

            if (this.tabControl1.SelectedIndex == 0)
            {
                try
                {
                    //判断前面是否有未打印页
                    if (!this.CanPrintForLong(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    //判断是否是续打
                    int rev = this.GetIsPrintAgainForLong();

                    if (rev == -1)
                    {
                        return;
                    }
                    //重打
                    else if (rev == 2)
                    {
                        DialogResult r = MessageBox.Show("该页医嘱已全部打印，确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (r == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            this.SetHeaderVisible(true, EnumOrderType.Long);
                            this.SetValueVisible(true, EnumOrderType.Long);
                            this.SetGridLineColor(Color.Black, EnumOrderType.Long);
                            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;

                            this.SetRePrintContentsForLong();

                            this.GetBeginAndEndTime(this.neuSpread1.ActiveSheet, true, ref dtBegin, ref dtEnd);

                            //this.ucOrderPrintHeader2.SetChangeBedNo(dtBegin, dtEnd, true);
                            this.pnFootLong.Dock = DockStyle.None;
                            this.pnFootLong.Location = new Point(this.neuSpread1.Location.X, 1100 - printLongPageY);
                            //p.ShowPrintPageDialog();
                            p.PrintPage(this.printLongTop, this.printLongLeft, this.panel6);
                            this.pnFootLong.Dock = DockStyle.Bottom;

                            this.QueryPatientOrder(true);

                            return;
                        }
                    }
                    //续打
                    else if (rev == 1 || rev == 0)
                    {
                        this.GetBeginAndEndTime(this.neuSpread1.ActiveSheet, false, ref dtBegin, ref dtEnd);

                        //首航续打，要打印边框、标题等
                        if (rev == 0)
                        {
                            this.SetHeaderVisible(false, EnumOrderType.Long);
                            this.SetValueVisible(false, EnumOrderType.Long);
                            this.SetGridLineColor(Color.White, EnumOrderType.Long);
                            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                        }
                        else
                        {
                            this.SetHeaderVisible(false, EnumOrderType.Long);
                            this.SetValueVisible(false, EnumOrderType.Long);
                            this.SetGridLineColor(Color.White, EnumOrderType.Long);
                            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                        }

                        this.SetPrintContentsForLong();
                    }

                    this.pnFootLong.Dock = DockStyle.None;
                    this.pnFootLong.Location = new Point(this.neuSpread1.Location.X, 1100 - printLongPageY);
                    //p.ShowPrintPageDialog();
                    p.PrintPage(this.printLongTop, this.printLongLeft, this.panel6);
                    this.pnFootLong.Dock = DockStyle.Bottom;
                    DialogResult dia;

                    frmNotice.label1.Text = "续打长期医嘱单是否成功?";

                    frmNotice.ShowDialog();

                    dia = frmNotice.dr;

                    if (dia == DialogResult.No)
                    {
                        DialogResult diaWarning = MessageBox.Show("确定续打没有成功吗？误操作会造成医嘱单出现空行！", "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                        if (diaWarning == DialogResult.Yes)
                        {
                            //确定续打没有成功，没话说了
                        }
                        else
                        {
                            dia = DialogResult.Yes;
                        }
                    }

                    if (dia == DialogResult.Yes)
                    {
                        if (this.UpdateOrderForLong() <= 0)
                        {
                            this.SetValueVisible(true, EnumOrderType.Long);
                            this.SetHeaderVisible(true, EnumOrderType.Long);
                            this.SetGridLineColor(Color.White, EnumOrderType.Long);

                            this.QueryPatientOrder(false);

                            return;
                        }
                    }

                    this.SetValueVisible(true, EnumOrderType.Long);
                    this.SetHeaderVisible(true, EnumOrderType.Long);
                    this.SetGridLineColor(Color.White, EnumOrderType.Long);

                    this.QueryPatientOrder(dia == DialogResult.Yes ? true : false);
                }
                catch
                {
                    this.SetHeaderVisible(true, EnumOrderType.Long);
                    this.SetValueVisible(true, EnumOrderType.Long);
                    this.SetGridLineColor(Color.White, EnumOrderType.Long);

                    this.QueryPatientOrder(false);
                }
            }
            #endregion

            #region 临时医嘱
            else
            {
                try
                {
                    if (!this.CanPrintForShort(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    //是否是续打
                    int rev = this.GetIsPrintAgainForShort();

                    if (rev == -1)
                    {
                        return;
                    }
                    //重打
                    if (rev == 2)
                    {
                        DialogResult r = MessageBox.Show("该页医嘱已全部打印，确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (r == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            this.SetHeaderVisible(true, EnumOrderType.Short);
                            this.SetValueVisible(true, EnumOrderType.Short);
                            this.SetGridLineColor(Color.Black, EnumOrderType.Short);

                            this.SetRePrintContentsForShort();

                            this.GetBeginAndEndTime(this.neuSpread2.ActiveSheet, true, ref dtBegin, ref dtEnd);

                            this.pnFootShort.Dock = DockStyle.None;
                            this.pnFootShort.Location = new Point(this.neuSpread2.Location.X, 1100 - printShortPageY);

                            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;
                            p.SetPageSize(new System.Drawing.Printing.PaperSize("OrderBill", 1030, 1200));
                            //p.ShowPrintPageDialog();
                            p.PrintPage(this.printShortTop, this.printShortLeft, this.panel7);
                            this.pnFootShort.Dock = DockStyle.Bottom;

                            this.QueryPatientOrder(true);

                            return;
                        }
                    }
                    //续打
                    else if (rev == 0 || rev == 1)
                    {
                        this.GetBeginAndEndTime(this.neuSpread2.ActiveSheet, true, ref dtBegin, ref dtEnd);

                        if (rev == 0)
                        {
                            this.SetHeaderVisible(true, EnumOrderType.Short);
                            this.SetValueVisible(true, EnumOrderType.Short);
                            this.SetGridLineColor(Color.Black, EnumOrderType.Short);
                            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;
                        }
                        else
                        {
                            this.SetHeaderVisible(false, EnumOrderType.Short);
                            this.SetValueVisible(false, EnumOrderType.Short);
                            this.SetGridLineColor(Color.White, EnumOrderType.Short);
                            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                        }

                        this.SetPrintContentsForShort();
                    }

                    this.pnFootShort.Dock = DockStyle.None;
                    this.pnFootShort.Location = new Point(this.neuSpread2.Location.X, 1100 - printShortPageY);

                    p.SetPageSize(new System.Drawing.Printing.PaperSize("OrderBill", 1030, 1200));
                    //p.ShowPrintPageDialog();
                    p.PrintPage(this.printShortTop, this.printShortLeft, this.panel7);
                    this.pnFootShort.Dock = DockStyle.Bottom;

                    DialogResult dia;

                    frmNotice.label1.Text = "续打临时医嘱单是否成功?";

                    frmNotice.ShowDialog();

                    dia = frmNotice.dr;

                    if (dia == DialogResult.No)
                    {
                        DialogResult diaWarning = MessageBox.Show("确定续打没有成功吗？误操作会造成医嘱单出现空行！", "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                        if (diaWarning == DialogResult.Yes)
                        {
                            //确定续打没有成功，没话说了
                        }
                        else
                        {
                            dia = DialogResult.Yes;
                        }
                    }

                    if (dia == DialogResult.Yes)
                    {
                        if (this.UpdateOrderForShort() <= 0)
                        {
                            this.SetGridLineColor(Color.White, EnumOrderType.Short);
                            this.SetValueVisible(true, EnumOrderType.Short);
                            this.SetHeaderVisible(true, EnumOrderType.Short);

                            this.QueryPatientOrder(false);

                            return;
                        }
                    }

                    this.SetValueVisible(true, EnumOrderType.Short);
                    this.SetHeaderVisible(true, EnumOrderType.Short);
                    this.SetGridLineColor(Color.White, EnumOrderType.Short);
                    p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                    this.QueryPatientOrder(dia == DialogResult.Yes ? true : false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.SetHeaderVisible(true, EnumOrderType.Short);
                    this.SetValueVisible(true, EnumOrderType.Short);
                    this.SetGridLineColor(Color.White, EnumOrderType.Short);

                    this.QueryPatientOrder(false);
                }
            }
            #endregion
        }

        /// <summary>
        /// 重新打印
        /// </summary>
        private void PrintAgain()
        {
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 1030, 1200);
            p.SetPageSize(size);
            p.IsCanCancel = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            string errText = "";
            frmNotice frmNotice = new frmNotice();
            string dtBegin = "";
            string dtEnd = "";

            #region 长期医嘱
            if (this.tabControl1.SelectedIndex == 0)
            {
                try
                {

                    if (!this.CanPrintForLong(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    //判断是否是续打
                    int rev = this.GetIsPrintAgainForLong();

                    if (rev == -1)
                    {
                        return;
                    }
                    else if (rev == 0)
                    {
                        MessageBox.Show("第" + (this.neuSpread1.ActiveSheetIndex + 1).ToString() + "页长期医嘱没有已打印医嘱!");
                        return;
                    }

                    DialogResult r = MessageBox.Show("确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (r == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        this.SetHeaderVisible(false, EnumOrderType.Long);
                        this.SetGridLineColor(Color.White, EnumOrderType.Long);

                        this.SetRePrintContentsForLong();

                        this.GetBeginAndEndTime(this.neuSpread1.ActiveSheet, true, ref dtBegin, ref dtEnd);

                        //this.ucOrderPrintHeader2.SetChangeBedNo(dtBegin, dtEnd, true);

                        //p.PrintPage( 25, 36, this.panel6 );
                        this.pnFootLong.Dock = DockStyle.None;
                        this.pnFootLong.Location = new Point(this.neuSpread1.Location.X, 1100 - printLongPageY);
                        //p.ShowPrintPageDialog();

                        p.PrintPage(this.printLongTop, this.printLongLeft, this.panel6);
                        this.pnFootLong.Dock = DockStyle.Bottom;

                        this.SetHeaderVisible(true, EnumOrderType.Long);
                        this.SetGridLineColor(Color.White, EnumOrderType.Long);

                        this.QueryPatientOrder(true);

                        return;
                    }
                }
                catch
                {
                    this.SetHeaderVisible(true, EnumOrderType.Long);
                    this.SetGridLineColor(Color.White, EnumOrderType.Long);

                    this.QueryPatientOrder(true);
                }
            }
            #endregion

            #region 临时医嘱
            else
            {
                try
                {

                    if (!this.CanPrintForShort(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    //判断是否是续打
                    int rev = this.GetIsPrintAgainForShort();

                    if (rev == -1)
                    {
                        return;
                    }
                    else if (rev == 0)
                    {
                        MessageBox.Show("第" + (this.neuSpread2.ActiveSheetIndex + 1).ToString() + "页临时医嘱没有已打印医嘱!");
                        return;
                    }

                    DialogResult r = MessageBox.Show("确定要重打该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (r == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        this.SetHeaderVisible(false, EnumOrderType.Short);
                        this.SetGridLineColor(Color.White, EnumOrderType.Short);

                        this.SetRePrintContentsForShort();

                        this.GetBeginAndEndTime(this.neuSpread2.ActiveSheet, true, ref dtBegin, ref dtEnd);

                        //this.ucOrderPrintHeader1.SetChangeBedNo(dtBegin, dtEnd, true);
                        this.pnFootShort.Dock = DockStyle.None;
                        this.pnFootShort.Location = new Point(this.neuSpread2.Location.X, 1100 - printShortPageY);
                        //p.ShowPrintPageDialog();
                        p.PrintPage(this.printShortTop, this.printShortLeft, this.panel7);
                        this.pnFootShort.Dock = DockStyle.Bottom;
                        //p.PrintPage( 20, 23, this.panel7 );

                        this.SetHeaderVisible(true, EnumOrderType.Short);
                        this.SetGridLineColor(Color.White, EnumOrderType.Short);

                        this.QueryPatientOrder(true);

                        return;
                    }
                }
                catch
                {
                    this.SetHeaderVisible(true, EnumOrderType.Short);
                    this.SetGridLineColor(Color.White, EnumOrderType.Short);

                    this.QueryPatientOrder(true);
                }
            }
            #endregion
        }

        /// <summary>
        /// 补打单条项目
        /// </summary>
        private void PrintSingleItem()
        {
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 1030, 1200);
            p.SetPageSize(size);
            p.IsCanCancel = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            #region 补打单条长期医嘱
            if (this.tabControl1.SelectedIndex == 0)
            {
                if (this.neuSpread1.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.neuSpread1.ActiveSheet.ActiveRowIndex < 0 || this.neuSpread1.ActiveSheet.ActiveRowIndex > 24)
                {
                    return;
                }

                FS.HISFC.Models.Order.Inpatient.Order order = this.neuSpread1.ActiveSheet.Rows[this.neuSpread1.ActiveSheet.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                if (order == null)
                {
                    return;
                }

                if (order.RowNo < 0 && order.PageNo < 0)
                {
                    MessageBox.Show("项目:" + order.Item.Name + "尚未打印");
                    return;
                }

                DialogResult r;

                r = MessageBox.Show("确定要重打项目:" + order.Item.Name, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                this.SetHeaderVisible(false, EnumOrderType.Long);

                this.SetValueVisible(false, EnumOrderType.Long);

                this.lblPageLong.Visible = false;
                //李超峰 2008.10.21添加，一次重打多行             
                for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                    {
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCDoctorName, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, (int)EnumLongOrderPrint.DCExecNurseName, "");
                    }
                    else
                    {
                        this.neuSpread1.ActiveSheet.SetValue(i, 0, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 1, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 2, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 3, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 4, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 5, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 6, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 7, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 8, "");
                    }

                }
                this.pnFootLong.Dock = DockStyle.None;
                this.pnFootLong.Location = new Point(this.neuSpread1.Location.X, 1100 - printLongPageY);
                //p.ShowPrintPageDialog();
                p.PrintPage(this.printLongTop, this.printLongLeft, this.panel6);
                this.pnFootLong.Dock = DockStyle.Bottom;

                this.SetValueVisible(true, EnumOrderType.Long);

                this.SetHeaderVisible(true, EnumOrderType.Long);

                this.QueryPatientOrder(true);
            }
            #endregion

            #region 补打单条临时医嘱
            else
            {
                if (this.neuSpread2.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }
                if (this.neuSpread2.ActiveSheet.ActiveRowIndex < 0)
                {
                    return;
                }
                FS.HISFC.Models.Order.Inpatient.Order order = this.neuSpread2.ActiveSheet.Rows[this.neuSpread2.ActiveSheet.ActiveRowIndex].Tag
                    as FS.HISFC.Models.Order.Inpatient.Order;

                if (order == null)
                {
                    return;
                }

                if (order.RowNo < 0 && order.PageNo < 0)
                {
                    MessageBox.Show("项目:" + order.Item.Name + "尚未打印");
                    return;
                }

                DialogResult r;

                r = MessageBox.Show("确定要重打项目:" + order.Item.Name, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                this.SetHeaderVisible(false, EnumOrderType.Short);

                this.SetValueVisible(false, EnumOrderType.Short);

                this.lblPageShort.Visible = false;
                //李超峰 2008.10.21添加，一次重打多行             
                for (int i = 0; i < this.neuSpread2.ActiveSheet.Rows.Count; i++)
                {
                    if (this.neuSpread2.ActiveSheet.IsSelected(i, 0))
                    {
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ApproveName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DoctorName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecNurseName, "");//执行日期不打
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecTime, "");
                    }
                    else
                    {
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ApproveName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DoctorName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Date, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.DrawCombo, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecNurseName, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.ExecTime, "");
                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.OrderName, "");

                        this.neuSpread2.ActiveSheet.SetValue(i, (int)EnumShortOrderPrint.Time, "");
                    }
                }

                this.pnFootShort.Dock = DockStyle.None;
                this.pnFootShort.Location = new Point(this.neuSpread2.Location.X, 1100 - printShortPageY);
                //p.ShowPrintPageDialog();
                p.PrintPage(this.printShortTop, this.printShortLeft, this.panel7);
                this.pnFootShort.Dock = DockStyle.Bottom;

                this.SetValueVisible(true, EnumOrderType.Short);

                this.SetHeaderVisible(true, EnumOrderType.Short);

                this.QueryPatientOrder(true);
            }
            #endregion
        }

        /// <summary>
        /// 补打单条/多条项目停止时间
        /// </summary>
        private void PrintSingleDate()
        {
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 1030, 1200);
            p.SetPageSize(size);
            p.IsCanCancel = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            if (this.tabControl1.SelectedIndex == 0)
            {
                if (this.neuSpread1.ActiveSheet.Rows.Count <= 0)
                {
                    return;
                }

                if (this.neuSpread1.ActiveSheet.ActiveRowIndex < 0)
                {
                    return;
                }

                DialogResult r;

                r = MessageBox.Show("确定要只打印选中项目的停止时间?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No)
                {
                    return;
                }

                this.SetHeaderVisible(false, EnumOrderType.Long);

                this.SetValueVisible(false, EnumOrderType.Long);

                this.lblPageLong.Visible = false;

                for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
                {
                    if (!this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                    {
                        this.neuSpread1.ActiveSheet.SetValue(i, 0, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 1, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 2, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 3, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 4, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 5, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 6, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 7, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 8, "");
                    }
                    else
                    {
                        FS.HISFC.Models.Order.Inpatient.Order order = this.neuSpread1.ActiveSheet.Rows[i].Tag
                    as FS.HISFC.Models.Order.Inpatient.Order;

                        if (order == null)
                        {
                            return;
                        }

                        if (order.RowNo < 0 && order.PageNo < 0)
                        {
                            MessageBox.Show("项目:" + order.Item.Name + "尚未打印");
                            this.SetValueVisible(true, EnumOrderType.Long);

                            this.SetHeaderVisible(true, EnumOrderType.Long);

                            this.QueryPatientOrder(true);
                            return;
                        }

                        if (order.Status < 3)
                        {
                            MessageBox.Show("项目:" + order.Item.Name + "尚未停止");
                            this.SetValueVisible(true, EnumOrderType.Long);

                            this.SetHeaderVisible(true, EnumOrderType.Long);

                            this.QueryPatientOrder(true);
                            return;
                        }
                        this.neuSpread1.ActiveSheet.SetValue(i, 0, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 1, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 2, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 3, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 4, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 5, "");
                        this.neuSpread1.ActiveSheet.SetValue(i, 8, "");
                    }
                }

                this.pnFootLong.Dock = DockStyle.None;
                this.pnFootLong.Location = new Point(this.neuSpread1.Location.X, 1100 - printLongPageY);
                //p.ShowPrintPageDialog();
                p.PrintPage(this.printLongTop, this.printLongLeft, this.panel6);
                this.pnFootLong.Dock = DockStyle.Bottom;

                this.SetValueVisible(true, EnumOrderType.Long);

                this.SetHeaderVisible(true, EnumOrderType.Long);

                this.QueryPatientOrder(true);
            }
        }

        #endregion

        #region 空白纸打印

        /// <summary>
        /// 打印当前页
        /// </summary>
        private void PrintCurentPage()
        {
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("orderBill", 1030, 1200);
            p.SetPageSize(size);
            p.IsCanCancel = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            string errText = "";
            frmNotice frmNotice = new frmNotice();
            string dtBegin = "";
            string dtEnd = "";

            #region 长期医嘱
            if (this.tabControl1.SelectedIndex == 0)
            {
                try
                {
                    if (!this.CanPrintForLong(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    //判断是否是续打
                    int rev = this.GetIsPrintAgainForLong();

                    if (rev == -1)
                    {
                        return;
                    }
                    else if (rev == 1)
                    {
                        MessageBox.Show("第" + (this.neuSpread1.ActiveSheetIndex + 1).ToString() + "页长期医嘱尚未完全打印，请先打印此页？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (MessageBox.Show("第" + (this.neuSpread1.ActiveSheetIndex + 1).ToString() + "页长期医嘱尚未完全打印，请先打印此页？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)) ;
                        //{
                        //    MessageBox.Show("请在开立界面点击\"医嘱单打印\"按钮进行续打");
                        //    return;
                        //}
                    }

                    #region 屏掉
                    //如果该页不是整页，提示是否继续打印
                    //DialogResult r ;
                    //if (string.IsNullOrEmpty(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.RowCount - 1, 3].Text))
                    //{
                    //    r = MessageBox.Show("第" + (this.neuSpread1.ActiveSheetIndex + 1).ToString() + "页长期医嘱单尚未写满，是否确定打印？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    //}
                    //else
                    //{
                    //    r = MessageBox.Show("确定要打印该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    //}

                    //if (r == DialogResult.No)
                    //{
                    //    return;
                    //}
                    //else
                    //{
                    #endregion
                    this.SetHeaderVisible(true, EnumOrderType.Long);
                    this.SetGridLineColor(Color.Black, EnumOrderType.Long);

                    //this.SetRePrintContentsForLong();

                    this.GetBeginAndEndTime(this.neuSpread1.ActiveSheet, true, ref dtBegin, ref dtEnd);

                    this.pnFootLong.Dock = DockStyle.None;
                    this.pnFootLong.Location = new Point(this.neuSpread1.Location.X, 1100 - printLongPageY);

                    //重打显示边框
                    p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;
                    //p.ShowPrintPageDialog();
                    p.PrintPage(this.printLongTop, this.printLongLeft, this.panel6);
                    this.pnFootLong.Dock = DockStyle.Bottom;

                    //白纸打印就不判断是否打印成功了
                    #region 打印后判断是否成功

                    //DialogResult dia;

                    //frmNotice.label1.Text = "打印长期医嘱单是否成功?";

                    //frmNotice.ShowDialog();

                    //dia = frmNotice.dr;

                    //if (dia == DialogResult.No)
                    //{
                    //    DialogResult diaWarning = MessageBox.Show("确定打印没有成功吗？误操作会造成医嘱单出现空行！", "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    //    if (diaWarning == DialogResult.Yes)
                    //    {
                    //        //确定续打没有成功，没话说了
                    //    }
                    //    else
                    //    {
                    //        dia = DialogResult.Yes;
                    //    }
                    //}

                    //if (dia == DialogResult.Yes)
                    //{
                    //更新打印标记
                    if (this.UpdateOrderForLong() <= 0)
                    {
                        this.SetValueVisible(true, EnumOrderType.Long);
                        this.SetHeaderVisible(true, EnumOrderType.Long);
                        this.SetGridLineColor(Color.White, EnumOrderType.Long);

                        this.QueryPatientOrder(false);

                        return;
                    }
                    //}

                    this.SetValueVisible(true, EnumOrderType.Long);
                    this.SetHeaderVisible(true, EnumOrderType.Long);
                    this.SetGridLineColor(Color.White, EnumOrderType.Long);

                    //this.QueryPatientOrder(dia == DialogResult.Yes ? true : false);
                    this.QueryPatientOrder(true);
                    #endregion

                    return;
                    //}
                }
                catch
                {
                    this.SetHeaderVisible(true, EnumOrderType.Long);
                    this.SetGridLineColor(Color.White, EnumOrderType.Long);
                    this.QueryPatientOrder(true);
                }
            }
            #endregion

            #region 临时医嘱
            else
            {
                try
                {
                    if (!this.CanPrintForShort(ref errText))
                    {
                        MessageBox.Show(errText);
                        return;
                    }

                    //判断是否是续打
                    int rev = this.GetIsPrintAgainForShort();

                    if (rev == -1)
                    {
                        return;
                    }
                    else if (rev == 1)
                    {
                        MessageBox.Show("第" + (this.neuSpread2.ActiveSheetIndex + 1).ToString() + "页临时医嘱尚未完全打印，请先打印此页？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (MessageBox.Show("第" + (this.neuSpread2.ActiveSheetIndex + 1).ToString() + "页临时医嘱尚未完全打印，请先打印此页？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)) ;
                        //{
                        //    MessageBox.Show("请在开立界面点击\"医嘱单打印\"按钮进行续打");
                        //    return;
                        //}
                    }

                    //如果该页不是整页，提示是否继续打印
                    //DialogResult r;
                    //if (string.IsNullOrEmpty(this.neuSpread2.ActiveSheet.Cells[this.neuSpread2.ActiveSheet.RowCount - 1, 3].Text))
                    //{
                    //    r = MessageBox.Show("第" + (this.neuSpread2.ActiveSheetIndex + 1).ToString() + "页临时医嘱单尚未写满，是否确定打印？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    //}
                    //else
                    //{
                    //    r = MessageBox.Show("确定要打印该页吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    //}

                    //if (r == DialogResult.No)
                    //{
                    //    return;
                    //}
                    //else
                    //{
                    this.SetHeaderVisible(true, EnumOrderType.Short);
                    this.SetGridLineColor(Color.Black, EnumOrderType.Short);

                    //this.SetRePrintContentsForShort();

                    this.GetBeginAndEndTime(this.neuSpread2.ActiveSheet, true, ref dtBegin, ref dtEnd);

                    this.pnFootShort.Dock = DockStyle.None;
                    this.pnFootShort.Location = new Point(this.neuSpread2.Location.X, 1100 - printShortPageY);

                    p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;
                    //p.ShowPrintPageDialog();
                    p.PrintPage(this.printShortTop, this.printShortLeft, this.panel7);

                    this.pnFootShort.Dock = DockStyle.Bottom;


                    #region 判断打印是否成功

                    //DialogResult dia;

                    //frmNotice.label1.Text = "打印医嘱单是否成功?";

                    //frmNotice.ShowDialog();

                    //dia = frmNotice.dr;

                    //if (dia == DialogResult.No)
                    //{
                    //    DialogResult diaWarning = MessageBox.Show("确定打印没有成功吗？误操作会造成医嘱单出现空行！", "警告！", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    //    if (diaWarning == DialogResult.Yes)
                    //    {
                    //        //确定续打没有成功，没话说了
                    //    }
                    //    else
                    //    {
                    //        dia = DialogResult.Yes;
                    //    }
                    //}

                    //if (dia == DialogResult.Yes)
                    //{
                    if (this.UpdateOrderForShort() <= 0)
                    {
                        this.SetGridLineColor(Color.Black, EnumOrderType.Short);
                        this.SetValueVisible(true, EnumOrderType.Short);
                        this.SetHeaderVisible(true, EnumOrderType.Short);

                        this.QueryPatientOrder(false);

                        return;
                    }
                    //}

                    this.SetValueVisible(true, EnumOrderType.Short);
                    this.SetHeaderVisible(true, EnumOrderType.Short);
                    this.SetGridLineColor(Color.White, EnumOrderType.Short);
                    p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                    //this.QueryPatientOrder(dia == DialogResult.Yes ? true : false);
                    this.QueryPatientOrder(true);
                    #endregion

                    return;
                    //}
                }
                catch
                {
                    this.SetHeaderVisible(true, EnumOrderType.Short);
                    this.SetGridLineColor(Color.White, EnumOrderType.Short);
                    this.QueryPatientOrder(true);
                }
            }
            #endregion
        }

        /// <summary>
        /// 打印未打印页
        /// </summary>
        /// <param name="orderType">医嘱类型</param>
        private void PrintNext(EnumOrderType orderType)
        {
            int pageNo = 0;
            int lineNo = 0;

            if (orderType == EnumOrderType.Short)
            {
                this.tabControl1.SelectedTab = this.tpShort;

                if (this.GetMaxPageNo(orderType, ref pageNo, ref lineNo) == -1)
                {
                    return;
                }

                //自动循环打印临嘱页
                for (int i = pageNo; i < neuSpread2.Sheets.Count; i++)
                {
                    this.neuSpread2.ActiveSheetIndex = i;
                    this.PrintCurentPage();
                }
            }
            else if (orderType == EnumOrderType.Long)
            {
                this.tabControl1.SelectedTab = this.tpLong;

                if (this.GetMaxPageNo(orderType, ref pageNo, ref lineNo) == -1)
                {
                    return;
                }

                //自动循环打印临嘱页
                for (int i = pageNo; i < this.neuSpread1.Sheets.Count; i++)
                {
                    this.neuSpread1.ActiveSheetIndex = i;
                    this.PrintCurentPage();
                }
            }
            else
            {
                #region 长嘱

                MessageBox.Show("即将打印未打印长期医嘱单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.tabControl1.SelectedTab = this.tpLong;

                if (this.GetMaxPageNo(orderType, ref pageNo, ref lineNo) == -1)
                {
                    return;
                }

                //自动循环打印临嘱页
                for (int i = pageNo; i < this.neuSpread1.Sheets.Count; i++)
                {
                    this.neuSpread1.ActiveSheetIndex = i;
                    this.PrintCurentPage();
                }
                #endregion

                #region 临嘱

                MessageBox.Show("即将打印未打印长期医嘱单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.tabControl1.SelectedTab = this.tpShort;

                if (this.GetMaxPageNo(orderType, ref pageNo, ref lineNo) == -1)
                {
                    return;
                }

                //自动循环打印临嘱页
                for (int i = pageNo; i < neuSpread2.Sheets.Count; i++)
                {
                    this.neuSpread2.ActiveSheetIndex = i;
                    this.PrintCurentPage();
                }
                #endregion
            }
        }

        /// <summary>
        /// 打印全部医嘱页
        /// </summary>
        /// <param name="orderType">医嘱类型</param>
        private void PrintAll(EnumOrderType orderType)
        {
            if (orderType == EnumOrderType.Short)
            {
                this.tabControl1.SelectedTab = this.tpShort;
                if (MessageBox.Show("是否确认打印全部临时医嘱单，误操作可能导致打印错误！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                //自动循环打印临嘱页
                for (int i = 0; i < neuSpread2.Sheets.Count; i++)
                {
                    this.neuSpread2.ActiveSheetIndex = i;
                    this.PrintCurentPage();
                }
            }
            else if (orderType == EnumOrderType.Long)
            {
                this.tabControl1.SelectedTab = this.tpLong;
                if (MessageBox.Show("是否确认打印全部长期医嘱单，误操作可能导致打印错误！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                //自动循环打印临嘱页
                for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
                {
                    this.neuSpread1.ActiveSheetIndex = i;
                    this.PrintCurentPage();
                }
            }
            else
            {
                #region 长嘱

                this.tabControl1.SelectedTab = this.tpLong;
                if (MessageBox.Show("是否确认打印全部长期医嘱单，误操作可能导致打印错误！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }

                //自动循环打印临嘱页
                for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
                {
                    this.neuSpread1.ActiveSheetIndex = i;
                    this.PrintCurentPage();
                }
                #endregion

                #region 临嘱
                this.tabControl1.SelectedTab = this.tpShort;
                if (MessageBox.Show("是否确认打印全部临时医嘱单，误操作可能导致打印错误！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                //自动循环打印临嘱页
                for (int i = 0; i < neuSpread2.Sheets.Count; i++)
                {
                    this.neuSpread2.ActiveSheetIndex = i;
                    this.PrintCurentPage();
                }
                #endregion
            }
        }

        #endregion

        /// <summary>
        /// 获得最大方号和列号
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="lineNo"></param>
        /// <returns></returns>
        private int GetMaxPageNo(EnumOrderType orderType, ref int pageNo, ref int lineNo)
        {
            #region 首先判断是否有未打印医嘱

            int rev = this.orderMgr.IsExistNoPrintOrder(this.pInfo.ID, orderType);
            if (rev == -1)
            {
                MessageBox.Show(this.orderMgr.Err);
                return -1;
            }
            else if (rev == 0)
            {
                MessageBox.Show("不存在未打印医嘱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            #endregion

            #region 获得已打印的最大方号和列号

            if (this.orderMgr.GetMaxPageNo(this.pInfo.ID, orderType, ref pageNo, ref lineNo) == -1)
            {
                MessageBox.Show("获得已打印页码出错：" + orderMgr.Err);
                return -1;
            }

            //长嘱是20  临嘱是21
            int nomalLine = orderType == EnumOrderType.Long ? 20 : 21;
            string type = orderType == EnumOrderType.Long ? "长嘱" : "临嘱";

            if (lineNo != nomalLine && lineNo != -1)
            {
                //此处判断有问题
                MessageBox.Show("第" + (pageNo + 1).ToString() + "页" + type + "单未打印完毕，请先打印此页！");
                return -1;
            }

            pageNo += 1;
            #endregion

            return 1;
        }

        #endregion

        /// <summary>
        /// 处理换页组号打印
        /// </summary>
        private void DealLongOrderCrossPage()
        {
            for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
            {
                FarPoint.Win.Spread.SheetView view = this.neuSpread1.Sheets[i];

                if (view.Rows[view.Rows.Count - 1].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ot = view.Rows[view.Rows.Count - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (ot != null)
                    {
                        ArrayList alOrders = this.GetLongOrderByCombId(ot);

                        if (alOrders.Count <= 1)
                        {
                            continue;
                        }
                        else
                        {
                            for (int j = 0; j < view.Rows.Count; j++)
                            {
                                FS.HISFC.Models.Order.Inpatient.Order ot1 = view.Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                                if (ot1 != null)
                                {
                                    if (ot1.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┏");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┗");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                    {
                                        view.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┃");
                                    }
                                }

                            }

                            if (i != this.neuSpread1.Sheets.Count - 1)
                            {
                                FarPoint.Win.Spread.SheetView viewNext = this.neuSpread1.Sheets[i + 1];

                                for (int j = 0; j < viewNext.Rows.Count; j++)
                                {
                                    FS.HISFC.Models.Order.Inpatient.Order ot2 = viewNext.Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                                    if (ot2 != null)
                                    {
                                        if (ot2.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┏");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┗");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                        {
                                            viewNext.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┃");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 处理换页组号打印
        /// </summary>
        private void DealShortOrderCrossPage()
        {
            for (int i = 0; i < this.neuSpread2.Sheets.Count; i++)
            {
                FarPoint.Win.Spread.SheetView view = this.neuSpread2.Sheets[i];

                if (view.Rows[view.Rows.Count - 1].Tag != null)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ot = view.Rows[view.Rows.Count - 1].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                    if (ot != null)
                    {
                        ArrayList alOrders = this.GetShortOrderByCombId(ot);

                        if (alOrders.Count <= 1)
                        {
                            continue;
                        }
                        else
                        {
                            for (int j = 0; j < view.Rows.Count; j++)
                            {
                                FS.HISFC.Models.Order.Inpatient.Order ot1 = view.Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                                if (ot1 != null)
                                {
                                    if (ot1.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┏");
                                    }
                                    else if (ot1.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                    {
                                        view.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┗");
                                    }
                                    else if (ot1.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                    {
                                        view.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┃");
                                    }
                                }

                            }

                            if (i != this.neuSpread2.Sheets.Count - 1)
                            {
                                FarPoint.Win.Spread.SheetView viewNext = this.neuSpread2.Sheets[i + 1];

                                for (int j = 0; j < viewNext.Rows.Count; j++)
                                {
                                    FS.HISFC.Models.Order.Inpatient.Order ot2 = viewNext.Rows[j].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                                    if (ot2 != null)
                                    {
                                        if (ot2.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┏");
                                        }
                                        else if (ot2.ID == (alOrders[alOrders.Count - 1] as FS.HISFC.Models.Order.Inpatient.Order).ID)
                                        {
                                            viewNext.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┗");
                                        }
                                        else if (ot2.Combo.ID == (alOrders[0] as FS.HISFC.Models.Order.Inpatient.Order).Combo.ID)
                                        {
                                            viewNext.SetValue(j, (int)EnumLongOrderPrint.DrawCombo, "┃");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private ArrayList GetLongOrderByCombId(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            ArrayList al = new ArrayList();

            for (int i = 0; i < this.alLong.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order ord = alLong[i] as FS.HISFC.Models.Order.Inpatient.Order;

                if (order.Combo.ID == ord.Combo.ID)
                {
                    al.Add(ord);
                }
            }

            return al;
        }

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private ArrayList GetShortOrderByCombId(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            ArrayList al = new ArrayList();

            for (int i = 0; i < this.alShort.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order ord = alShort[i] as FS.HISFC.Models.Order.Inpatient.Order;

                if (order.Combo.ID == ord.Combo.ID)
                {
                    al.Add(ord);
                }
            }

            return al;
        }

        private void neuSpread1_TabIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptAndBed(EnumOrderType.Long);
        }

        private void neuSpread2_TabIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptAndBed(EnumOrderType.Short);
        }
    }

    /// <summary>
    /// 东莞本地医嘱打印查询
    /// </summary>
    public class OrderQuery : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 是否存在未打印医嘱
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public int IsExistNoPrintOrder(string inPatientNo, EnumOrderType orderType)
        {
            string strSql = "";
            string type = "";

            try
            {
                if (this.Sql.GetSql("DongGuan.Inpatient.Order.Query.NoPrintOrder", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }

                if (orderType == EnumOrderType.Long)
                {
                    type = "1";
                }
                else if (orderType == EnumOrderType.Short)
                {
                    type = "0";
                }
                else
                {
                    this.Err = "获得查询医嘱类型出错，请选择长嘱或者临嘱类型！";
                    this.WriteErr();
                    this.Reader.Close();
                    return -1;
                }

                strSql = string.Format(strSql, inPatientNo, type);

                int count = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql));

                if (count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                this.Reader.Close();
                return -1;
            }
        }

        /// <summary>
        /// 获得已打印最大页码和行号
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public int GetMaxPageNo(string inPatientNo, EnumOrderType orderType, ref int pageNo, ref int lineNo)
        {
            string strSql = "";
            string type = "";

            try
            {
                if (this.Sql.GetSql("DongGuan.Inpatient.Order.Query.GetMaxPageNoAndLineNo", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }

                if (orderType == EnumOrderType.Long)
                {
                    type = "1";
                }
                else if (orderType == EnumOrderType.Short)
                {
                    type = "0";
                }
                else
                {
                    this.Err = "获得查询医嘱类型出错，请选择长嘱或者临嘱类型！";
                    this.WriteErr();
                    this.Reader.Close();
                    return -1;
                }

                strSql = string.Format(strSql, inPatientNo, type);

                if (this.ExecQuery(strSql) == -1)
                {
                    this.Reader.Close();
                    this.WriteErr();
                    return -1;
                }

                while (this.Reader.Read())
                {
                    pageNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                    lineNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1]);
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                this.Reader.Close();
                return -1;
            }

            this.Reader.Close();
            return 1;
        }
    }

    /// <summary>
    /// 长期医嘱单列名
    /// </summary>
    public enum EnumLongOrderPrint
    {
        /// <summary>
        /// 开始月份
        /// </summary>
        BeginDate = 0,

        /// <summary>
        /// 时间
        /// </summary>
        BeginTime = 1,

        /// <summary>
        /// 
        /// </summary>
        DrawCombo = 2,

        /// <summary>
        /// 项目名
        /// </summary>
        OrderName = 3,

        /// <summary>
        /// 医生名
        /// </summary>
        DoctorName = 4,

        /// <summary>
        /// 执行护士
        /// </summary>
        ExecNurseName = 5,

        /// <summary>
        /// 停止月份
        /// </summary>
        EndDate = 6,

        /// <summary>
        /// 停止时间
        /// </summary>
        EndTime = 7,

        /// <summary>
        /// 停止医生
        /// </summary>
        DCDoctorName = 8,

        /// <summary>
        /// 停止确认护士
        /// </summary>
        DCExecNurseName = 9,

        /// <summary>
        /// 
        /// </summary>
        ComboID = 10
    }

    /// <summary>
    /// 临时医嘱单列名
    /// </summary>
    public enum EnumShortOrderPrint
    {
        /// <summary>
        /// 日期
        /// </summary>
        Date = 0,

        /// <summary>
        /// 时间
        /// </summary>
        Time = 1,

        /// <summary>
        /// 
        /// </summary>
        DrawCombo = 2,

        /// <summary>
        /// 医嘱名称
        /// </summary>
        OrderName = 3,

        /// <summary>
        /// 医生姓名
        /// </summary>
        DoctorName = 4,

        /// <summary>
        /// 执行时间
        /// </summary>
        ExecTime = 5,

        /// <summary>
        /// 核对者姓名
        /// </summary>
        ApproveName = 6,

        /// <summary>
        /// 执行护士签名
        /// </summary>
        ExecNurseName = 7,

        /// <summary>
        /// 
        /// </summary>
        ComboID = 8
    }

    /// <summary>
    /// 医嘱类型枚举
    /// </summary>
    public enum EnumOrderType
    {
        /// <summary>
        /// 长嘱
        /// </summary>
        Long,

        /// <summary>
        /// 临嘱
        /// </summary>
        Short,

        /// <summary>
        /// 全部医嘱
        /// </summary>
        All
    }

    /// <summary>
    /// 医嘱比较（根据方号）
    /// </summary>
    public class AlSort : System.Collections.IComparer
    {
        #region IComparer 成员

        /// <summary>
        /// 比较方号
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.Inpatient.Order orderInfox = x as FS.HISFC.Models.Order.Inpatient.Order;
            FS.HISFC.Models.Order.Inpatient.Order orderInfoy = y as FS.HISFC.Models.Order.Inpatient.Order;
            if (orderInfox.SortID > orderInfoy.SortID)
            {
                return 1;
            }
            else if (orderInfox.SortID == orderInfoy.SortID)
            {
                return 0;
            }
            else
            {
                return -1;
            }

        }
        #endregion
    }
}
