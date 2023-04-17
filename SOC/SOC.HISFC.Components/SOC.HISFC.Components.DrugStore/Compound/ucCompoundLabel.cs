using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    /// <summary>
    /// [功能描述: 配置标签卡片]
    /// [创 建 人: Sunjh]
    /// [创建时间: 2010-10-16]
    /// <说明>
    ///     1、移植天津配液标签
    ///     2、为了增加卡片样式显示
    /// </说明>
    /// </summary>
    public partial class ucCompoundLabel: FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundPrint
    {
        public ucCompoundLabel()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 本次医嘱总贴数
        /// </summary>
        private decimal labelTotNum = 0;

        /// <summary>
        /// 标签序号
        /// </summary>
        private int iCount = 0;
        /// <summary>
        /// 同组分页码
        /// </summary>
        private string iPage = "";
        /// <summary>
        /// 患者信息显示
        /// </summary>
        private System.Collections.Hashtable hsPatientInfo = new Hashtable();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper deptHelper = null;
        /// <summary>
        /// 药品业务管理层
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManger = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 住院患者管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntrgrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 是否补打
        /// </summary>
        private bool isReprint = false;
        /// <summary>
        /// 配置清单
        /// </summary>
        private ArrayList alCompoundListData = new ArrayList();

        /// <summary>
        /// 配液批次号 配液中心兼容卡片界面样式by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
        /// </summary>
        private string groupNO = "";
        /// <summary>
        /// 医嘱批次
        /// </summary>
        private static List<FS.HISFC.Models.Pharmacy.OrderGroup> orderGroupList = null; 
        #endregion

        #region 属性
        /// <summary>
        /// 是否补打
        /// </summary>
        public bool IsReprint
        {
            get
            {
                return isReprint;
            }
            set
            {
                isReprint = value;
            }
        }

        /// <summary>
        /// 是否为作废标签的标题 True 作废标签标题 False 正常标题
        /// </summary>
        public bool IsUnvalidTitle
        {
            set
            {
                if (value == false)
                {
                    this.lbTitle.Text = "输液单\r\n";
                }
                else
                {
                    this.lbTitle.Text = "(废)输液单\r\n";
                }
            }
        }
        /// <summary>
        /// 是否为已打印标签的标题 True  False 正常标题
        /// </summary>
        public bool IsPrintedTitle
        {
            set
            {
                if (value == false)
                {
                    this.lbTitle.Text = "输液单\r\n";
                }
                else
                {
                    this.lbTitle.Text = "(补)输液单\r\n";
                }
            }
        }

        /// <summary>
        /// 配液批次号 配液中心兼容卡片界面样式by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
        /// </summary>
        public string GroupNO
        {
            get
            {
                return groupNO;
            }
            set
            {
                groupNO = value;
            }
        } 
        #endregion

        #region 方法
        private static string GetCompoundGroup(DateTime useTime)
        {
            if (orderGroupList == null)
            {
                //FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();
                orderGroupList = itemMgr.QueryOrderGroup();
            }

            DateTime juegeTime = new DateTime(2000, 12, 12, useTime.Hour, useTime.Minute, useTime.Second);
            if (orderGroupList != null)
            {
                foreach (FS.HISFC.Models.Pharmacy.OrderGroup info in orderGroupList)
                {
                    if (juegeTime >= info.BeginTime && juegeTime <= info.EndTime)
                    {
                        return info.ID;
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// 画条形码方法
        /// </summary>
        /// <param name="barCodeType">条码类型，使用常数BARCODETYPE</param>
        /// <param name="barCode">条码号</param>
        /// <param name="foreColor">字体颜色</param>
        /// <param name="backColor">背景颜色</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>条码图片</returns>
        public System.Drawing.Image DrawingBarCode(string barCodeType, string barCode, System.Drawing.Color foreColor, System.Drawing.Color backColor, int width, int height)
        {
            //BarcodeLib.Barcode b = new BarcodeLib.Barcode(); //创建条码类

            //b.IncludeLabel = true;//显示字符串内容,false则不显示字符串内容

            //BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;//选择字体

            //if (barCodeType == "1")
            //{
            //    type = BarcodeLib.TYPE.CODE128;
            //}
            //else if (barCodeType == "2")
            //{
            //    type = BarcodeLib.TYPE.CODE39;
            //}


            //System.Drawing.Image barCodeImage = b.Encode(type, barCode, foreColor, backColor, width, height);

            return null;
        }
        
        #endregion

        #region ICompoundPrint 成员
        /// <summary>
        /// 屏幕清空
        /// </summary>
        public void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        public void AddAllData(System.Collections.ArrayList al)
        {
            return;
        }

        /// <summary>
        /// 打印页面赋值
        /// </summary>
        /// <param name="alCombo"></param>
        public void AddCombo(System.Collections.ArrayList alCombo)
        {
            this.Clear();

            if (deptHelper == null)
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.GetDeptmentAll();
                if (alDept == null)
                {
                    MessageBox.Show("获取科室帮助类信息发生错误");
                }
                deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
            }

            this.hsPatientInfo.Clear();

            foreach (ArrayList alGroup in alCombo)
            {
                this.neuSpread1_Sheet1.Rows.Count = 0;

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alGroup)
                {
                    #region 前5行
                    FS.HISFC.Models.RADT.PatientInfo myPatientInfo = GetInpatientInfo(info.PatientNO);
                    //第一行：页码/共几页
                    //第二行：'补'+摆药单号 +‘（’ +标签号 +‘）’
                    //第三行：病区名称+病区编号，床位号+住院号+批次号
                    //第四行：姓名+性别+年龄+频次+该频次的第几组
                    //第五行：配药日期时间+医嘱类型+设定的组
                    this.lblLine0.Text = (iCount + 1).ToString() + "/" + this.labelTotNum.ToString() + "页";
                    string printType = string.Empty;
                    if (isReprint == true)
                    {
                        printType = "补";
                    }
                    else
                    {
                        printType = "";
                    }
                    this.lblLine1.Text = printType + info.DrugNO + "(" + info.User03 + ")" + " [" + iPage + "]";
                    this.lblLine2.Text = deptHelper.GetName(info.ApplyDept.ID) + " " + info.ApplyDept.ID;
                    #region 床号
                    string bedNo = string.Empty;
                    switch (info.User01.Length)
                    {
                        case 5:
                            bedNo = info.User01.Substring(4, 1);
                            break;
                        case 6:
                            bedNo = info.User01.Substring(4, 2);
                            break;
                        case 7:
                            bedNo = info.User01.Substring(4, 3);
                            break;
                        case 8:
                            bedNo = info.User01.Substring(4, 4);
                            break;
                        case 9:
                            bedNo = info.User01.Substring(4, 5);
                            break;
                        case 10:
                            bedNo = info.User01.Substring(4, 6);
                            break;
                    }
                    #endregion
                    this.lblLine21.Text = bedNo + " " + info.PatientNO.Substring(4, 10) + " " ;
                    this.lbGroup.Text = "第" + info.CompoundGroup + "批";
                    #region 频次的第几组
                    string frequencyTime = string.Empty;
                    //每日一次的不显示该频次的第几组
                    if (info.Frequency.ID == "QD" || info.Frequency.ID == "QD(06)" || info.Frequency.ID == "QD(07)" || info.Frequency.ID == "QD(11)" || info.Frequency.ID == "QD(12)" || info.Frequency.ID == "QD(16)" || info.Frequency.ID == "QD(17)")
                    {
                        frequencyTime = "";
                    }
                    //每日两次的
                    else if (info.Frequency.ID == "BID" || info.Frequency.ID == "BID6" || info.Frequency.ID == "BID7" || info.Frequency.ID == "BID719" || info.Frequency.ID == "BID8" || info.Frequency.ID == "BID812" || info.Frequency.ID == "BIDCXY")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "06:00:00" || info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00" || info.UseTime.TimeOfDay.ToString() == "09:00:00")
                        {
                            frequencyTime = "(1/2)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "12:00:00" || info.UseTime.TimeOfDay.ToString() == "14:00:00" || info.UseTime.TimeOfDay.ToString() == "15:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00" || info.UseTime.TimeOfDay.ToString() == "17:00:00" || info.UseTime.TimeOfDay.ToString() == "18:00:00" || info.UseTime.TimeOfDay.ToString() == "19:00:00")
                        {
                            frequencyTime = "(2/2)";
                        }
                    }
                    //每日三次的
                    else if (info.Frequency.ID == "TID" || info.Frequency.ID == "TID3" || info.Frequency.ID == "TIDCQ")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(1/3)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "11:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00")
                        {
                            frequencyTime = "(2/3)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "17:00:00" || info.UseTime.TimeOfDay.ToString() == "20:00:00")
                        {
                            frequencyTime = "(3/3)";
                        }
                    }
                    //八小时一次
                    else if (info.Frequency.ID=="Q8H")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "00:00:00")
                        {
                            frequencyTime = "(1/3)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(2/3)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "16:00:00")
                        {
                            frequencyTime = "(3/3)";
                        }
                    }
                    //12小时一次
                    else if (info.Frequency.ID == "Q12H")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(1/2)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "20:00:00")
                        {
                            frequencyTime = "(2/2)";
                        }
                    }
                    //每日四次的
                    else if (info.Frequency.ID == "QID" || info.Frequency.ID == "QID710" || info.Frequency.ID == "QID79" || info.Frequency.ID == "QIDCH2" || info.Frequency.ID == "QIDCXT")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "06:00:00" || info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(1/4)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "09:00:00" || info.UseTime.TimeOfDay.ToString() == "10:00:00" || info.UseTime.TimeOfDay.ToString() == "11:00:00" || info.UseTime.TimeOfDay.ToString() == "12:00:00")
                        {
                            frequencyTime = "(2/4)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "14:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00")
                        {
                            frequencyTime = "(3/4)";
                        }
                        else
                        {
                            frequencyTime = "(4/4)";
                        }
                    }
                    else if (info.Frequency.ID == "QID711")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "07:00:00")
                        { 
                            frequencyTime = "(1/4)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "11:00:00")
                        {
                            frequencyTime = "(2/4)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "17:00:00")
                        {
                            frequencyTime = "(3/4)";
                        }
                        else
                        {
                            frequencyTime = "(4/4)";
                        }
                    }
                    //每日五次的
                    else if (info.Frequency.ID == "5ID")
                    {
                        if (info.UseTime.TimeOfDay.ToString() == "00:00:00")
                        {
                            frequencyTime = "(1/5)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                        {
                            frequencyTime = "(2/5)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "12:00:00")
                        {
                            frequencyTime = "(3/5)";
                        }
                        else if (info.UseTime.TimeOfDay.ToString() == "16:00:00")
                        {
                            frequencyTime = "(4/5)";
                        }
                        else
                        {
                            frequencyTime = "(5/5)";
                        }
                    }
                    #endregion
                    this.lblLine3.Text = info.User02 + " " + myPatientInfo.Sex.Name + " " + FS.FrameWork.Public.String.GetAge(myPatientInfo.Birthday, this.itemManger.GetDateTimeFromSysDateTime()) + " " + info.Frequency.ID + frequencyTime;
                    #region 医嘱类型
                    string orderType = string.Empty;
                    if (info.OrderType.ID=="CZ")
                    {
                        orderType = "长期";
                    }
                    else
                    {
                        orderType = "临时";
                    }
                    #endregion
                    //this.lblLine4.Text = info.Operation.ApproveOper.OperTime.ToString("yyyy-MM-dd HH:mm") + " " + orderType + " 第" + info.User03 + "组";
                    DateTime dateNow = FS.FrameWork.Function.NConvert.ToDateTime(this.itemManger.GetSysDateTime());
                    this.lblLine4.Text = dateNow.ToString("yyyy-MM-dd HH:mm") + " " + orderType + " 第" + info.User03 + "组";
                    #endregion

                    #region 设置用药信息
                    //this.lbDrugInfo.Text = string.Format("用药时间：{0}  共 {1} 贴  此第 {2} 贴",info.UseTime.ToString("HH:mm:ss"),this.labelTotNum.ToString(), (iCount + 1).ToString());
                    string strDosage = string.Empty;
                    //功能好使，现在不用，打不下，用的时候打开  20100507
                    //strDosage = Function.DrugDosage.GetStaticDosage(info.Item.ID);
                    this.lblCmbo.Text = this.lblCmbo.Text + info.User03; //组号
                    this.neuSpread1_Sheet1.Rows.Add(0, 2);
                    //名称
                    this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Name + "[" + strDosage + info.Item.Specs + "]";
                    this.neuSpread1_Sheet1.Cells[0, 0].ColumnSpan = 2;
                    //是否有效标志
                    this.neuSpread1_Sheet1.Cells[0, 2].Text = (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid ? "√" : "x");
                    //基本剂量||用法
                    this.neuSpread1_Sheet1.Cells[1, 0].Text = info.Item.BaseDose.ToString() + info.Item.DoseUnit + "      " + info.Usage.Name;
                    this.neuSpread1_Sheet1.Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[1, 0].ColumnSpan = 2;
                    ////用法
                    //this.neuSpread1_Sheet1.Cells[1, 1].Text = info.Usage.Name.ToString();
                    //this.neuSpread1_Sheet1.Columns[1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    //每次用量
                    this.neuSpread1_Sheet1.Cells[1, 2].Text = info.DoseOnce.ToString() + info.Item.DoseUnit;
                    this.neuSpread1_Sheet1.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Operation.ApplyQty.ToString();
                    //this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Usage.Name + "[" + info.Usage.Memo+"]";
                    #endregion
                }
                this.lblEnd1.Text = "审方：" + this.itemManger.Operator.Name + " 执行护士： ______";

                iCount++;

                if (iCount != (alCombo.Count+1))
                {
                    this.Print();   
                }
            }
            iCount = 0;
        }

        /// <summary>
        /// 卡片界面赋值
        /// </summary>
        /// <param name="alGroup"></param>
        public void AddComboNonePrint(System.Collections.ArrayList alGroup)
        {
            this.Clear();

            if (deptHelper == null)
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.GetDeptmentAll();
                if (alDept == null)
                {
                    MessageBox.Show("获取科室帮助类信息发生错误");
                }
                deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
            }

            this.hsPatientInfo.Clear();

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alGroup)
            {
                #region 设置患者信息
                this.GroupNO = info.CompoundGroup;
                //if (info.PatientNO.Substring(0, 1) == "Z")
                //{
                //    //配液中心兼容卡片界面样式by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
                //    this.GroupNO = info.CompoundGroup;
                //    this.lbPatientInfo.Text = info.CompoundGroup + "  " + info.UseTime.ToString("yyyy-MM-dd") + string.Format("    {0}   共{1}贴 此第{2}贴", GetCompoundGroup(info.UseTime), this.labelTotNum.ToString(), iCount.ToString()); ;
                //}
                //else
                //{
                //    this.lbPatientInfo.Text = info.UseTime.ToString("yyyy-MM-dd HH;mm:ss") + string.Format("      共{0}贴 此第{1}贴", this.labelTotNum.ToString(), iCount.ToString()); ;
                //}

                //if (this.hsPatientInfo.Contains(info.PatientNO))
                //{
                //    this.lbDrugInfo.Text = this.hsPatientInfo[info.PatientNO].ToString();
                //}
                //else
                //{
                //    if (info.PatientNO.Substring(0, 1) == "Z")
                //    {
                //        if (info.User01.Length > 3)
                //        {
                //            this.lbDrugInfo.Text = info.PatientNO.Substring(5) + "     " + info.User01.Substring(4) + "床  " + info.User02 + "  " + deptHelper.GetName(info.ApplyDept.ID);
                //        }
                //        else
                //        {
                //            this.lbDrugInfo.Text = info.PatientNO.Substring(5) + "     " + info.User01 + "床  " + info.User02 + "  " + deptHelper.GetName(info.ApplyDept.ID);
                //        }
                //    }
                //    else
                //    {
                //        this.lbDrugInfo.Text = info.PatientNO + "    " + info.User02 + "  " + deptHelper.GetName(info.ApplyDept.ID);
                //    }
                //    this.hsPatientInfo.Add(info.PatientNO, this.lbDrugInfo.Text);
                //}
                #endregion

                #region 前5行
                FS.HISFC.Models.RADT.PatientInfo myPatientInfo = GetInpatientInfo(info.PatientNO);
                //第一行：页码/共几页
                //第二行：'补'+摆药单号 +‘（’ +标签号 +‘）’
                //第三行：病区名称+病区编号，床位号+住院号+批次号
                //第四行：姓名+性别+年龄+频次+该频次的第几组
                //第五行：配药日期时间+医嘱类型+设定的组
                this.lblLine0.Text = (iCount).ToString() + "/" + this.labelTotNum.ToString() + "页";
                string printType = string.Empty;
                if (isReprint == true)
                {
                    printType = "补";
                }
                else
                {
                    printType = "";
                }
                this.lblLine1.Text = printType + info.DrugNO + "(" + info.User03 + ")" + "[" + iPage + "]";
                this.lblLine2.Text = deptHelper.GetName(info.ApplyDept.ID).Replace("病区", "");
                #region 床号
                string bedNo = string.Empty;
                switch (info.User01.Length)
                {
                    case 5:
                        bedNo = info.User01.Substring(4, 1);
                        break;
                    case 6:
                        bedNo = info.User01.Substring(4, 2);
                        break;
                    case 7:
                        bedNo = info.User01.Substring(4, 3);
                        break;
                    case 8:
                        bedNo = info.User01.Substring(4, 4);
                        break;
                    case 9:
                        bedNo = info.User01.Substring(4, 5);
                        break;
                    case 10:
                        bedNo = info.User01.Substring(4, 6);
                        break;
                }
                #endregion
                this.lblLine21.Text = bedNo + "床  (" + myPatientInfo.ID + ")";
                this.lbGroup.Text = "第" + info.CompoundGroup + "批";
                #region 频次的第几组
                string frequencyTime = string.Empty;
                //每日一次的不显示该频次的第几组
                if (info.Frequency.ID == "QD" || info.Frequency.ID == "QD(06)" || info.Frequency.ID == "QD(07)" || info.Frequency.ID == "QD(11)" || info.Frequency.ID == "QD(12)" || info.Frequency.ID == "QD(16)" || info.Frequency.ID == "QD(17)")
                {
                    frequencyTime = "";
                }
                //每日两次的
                else if (info.Frequency.ID == "BID" || info.Frequency.ID == "BID6" || info.Frequency.ID == "BID7" || info.Frequency.ID == "BID719" || info.Frequency.ID == "BID8" || info.Frequency.ID == "BID812" || info.Frequency.ID == "BIDCXY")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "06:00:00" || info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00" || info.UseTime.TimeOfDay.ToString() == "09:00:00")
                    {
                        frequencyTime = "(1/2)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "12:00:00" || info.UseTime.TimeOfDay.ToString() == "14:00:00" || info.UseTime.TimeOfDay.ToString() == "15:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00" 
                        || info.UseTime.TimeOfDay.ToString() == "17:00:00" || info.UseTime.TimeOfDay.ToString() == "18:00:00" || info.UseTime.TimeOfDay.ToString() == "19:00:00")
                    {
                        frequencyTime = "(2/2)";
                    }
                }
                //每日三次的
                else if (info.Frequency.ID == "TID" || info.Frequency.ID == "TID3" || info.Frequency.ID == "TIDCQ")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(1/3)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "11:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00")
                    {
                        frequencyTime = "(2/3)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "17:00:00" || info.UseTime.TimeOfDay.ToString() == "20:00:00")
                    {
                        frequencyTime = "(3/3)";
                    }
                }
                //八小时一次
                else if (info.Frequency.ID == "Q8H")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "00:00:00")
                    {
                        frequencyTime = "(3/3)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(1/3)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "16:00:00")
                    {
                        frequencyTime = "(2/3)";
                    }
                }
                //12小时一次
                else if (info.Frequency.ID == "Q12H")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(1/2)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "20:00:00")
                    {
                        frequencyTime = "(2/2)";
                    }
                }
                //每日四次的
                else if (info.Frequency.ID == "QID" || info.Frequency.ID == "QID710" || info.Frequency.ID == "QID79" || info.Frequency.ID == "QIDCH2" || info.Frequency.ID == "QIDCXT")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "06:00:00" || info.UseTime.TimeOfDay.ToString() == "07:00:00" || info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(1/4)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "09:00:00" || info.UseTime.TimeOfDay.ToString() == "10:00:00" || info.UseTime.TimeOfDay.ToString() == "11:00:00" 
                        || info.UseTime.TimeOfDay.ToString() == "12:00:00")
                    {
                        frequencyTime = "(2/4)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "14:00:00" || info.UseTime.TimeOfDay.ToString() == "16:00:00")
                    {
                        frequencyTime = "(3/4)";
                    }
                    else
                    {
                        frequencyTime = "(4/4)";
                    }
                }
                else if (info.Frequency.ID == "QID711")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "07:00:00")
                    {
                        frequencyTime = "(1/4)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "11:00:00")
                    {
                        frequencyTime = "(2/4)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "17:00:00")
                    {
                        frequencyTime = "(3/4)";
                    }
                    else
                    {
                        frequencyTime = "(4/4)";
                    }
                }
                //每日五次的
                else if (info.Frequency.ID == "5ID")
                {
                    if (info.UseTime.TimeOfDay.ToString() == "00:00:00")
                    {
                        frequencyTime = "(1/5)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "08:00:00")
                    {
                        frequencyTime = "(2/5)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "12:00:00")
                    {
                        frequencyTime = "(3/5)";
                    }
                    else if (info.UseTime.TimeOfDay.ToString() == "16:00:00")
                    {
                        frequencyTime = "(4/5)";
                    }
                    else
                    {
                        frequencyTime = "(5/5)";
                    }
                }
                #endregion
                this.lblLine3.Text = info.User02 + " " + myPatientInfo.Sex.Name + " " + FS.FrameWork.Public.String.GetAge(myPatientInfo.Birthday, this.itemManger.GetDateTimeFromSysDateTime()) + " " + info.Frequency.ID + frequencyTime;
                #region 医嘱类型
                string orderType = string.Empty;
                if (info.OrderType.ID == "CZ")
                {
                    orderType = "长期";
                }
                else
                {
                    orderType = "临时";
                }
                #endregion 
                //this.lblLine4.Text = info.Operation.ApproveOper.OperTime.ToString("yyyy-MM-dd HH:mm") + " " + orderType + " 第" + info.User03 + "组";
                DateTime dateNow = FS.FrameWork.Function.NConvert.ToDateTime(this.itemManger.GetSysDateTime());
                this.lblLine4.Text = info.UseTime.ToString() + " " + orderType + " 第" + info.User03 + "组";
                #endregion

                #region 设置用药信息
                //this.lbDrugInfo.Text = string.Format("用药时间：{0}  共 {1} 贴  此第 {2} 贴",info.UseTime.ToString("HH:mm:ss"),this.labelTotNum.ToString(), (iCount + 1).ToString());
                string strDosage = string.Empty;
                //功能好使，现在不用，打不下，用的时候打开  20100507
                //strDosage = Function.DrugDosage.GetStaticDosage(info.Item.ID);
                this.lblCmbo.Text = this.lblCmbo.Text + info.User03; //组号
                this.neuSpread1_Sheet1.Rows.Add(0, 2);
                //名称
                this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Name;//+ "[" + strDosage + info.Item.Specs + "]";
                this.neuSpread1_Sheet1.Cells[0, 0].ColumnSpan = 2;
                //是否有效标志
                this.neuSpread1_Sheet1.Cells[1, 2].Text = (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid ? "√" : "x");
                //基本剂量
                string str = "";
                if (info.Item.ShiftMark == "冷藏")
                {
                    str = "[冷藏]";
                }
                this.neuSpread1_Sheet1.Cells[1, 0].Text = info.Item.BaseDose.ToString() + info.Item.DoseUnit + str;
                this.neuSpread1_Sheet1.Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                //用法
                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Usage.Name;
                this.neuSpread1_Sheet1.Columns[1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                //每次用量
                //if (info.DoseOnce % info.Item.BaseDose != 0)
                //{
                //    Font f = new Font("宋体", 12, FontStyle.Underline);

                //    this.neuSpread1_Sheet1.Cells[1, 1].Font = f;
                //}
                Font f = new Font("宋体", 12, FontStyle.Underline);
                this.neuSpread1_Sheet1.Cells[1, 1].Font = f;
                this.neuSpread1_Sheet1.Cells[1, 1].Text = info.DoseOnce.ToString() + info.Item.DoseUnit;
                this.neuSpread1_Sheet1.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                
                #endregion
            }
            this.lblEnd1.Text = "审方：" + this.itemManger.Operator.Name + " 核  准1： ______";

            this.pbBarcode.Image = this.DrawingBarCode("1", this.groupNO, Color.Black, Color.White, pbBarcode.Width, pbBarcode.Height);
        }

        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.PatientInfo GetInpatientInfo(string inpatientNO)
        {
            if (hsPatientInfo.ContainsKey(inpatientNO))
            {
                return hsPatientInfo[inpatientNO] as FS.HISFC.Models.RADT.PatientInfo;
            }
            else
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtIntrgrate.GetPatientInfoByPatientNO(inpatientNO);
                hsPatientInfo.Add(inpatientNO, patientInfo);
                return patientInfo;
            }
        }

        public FS.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public decimal LabelTotNum
        {
            set 
            {
                this.labelTotNum = value;
            }
        }

        /// <summary>
        /// 标签序号
        /// </summary>
        public int ICount
        {
            get 
            {
                return iCount; 
            }
            set 
            { 
                iCount = value; 
            }
        }
        /// <summary>
        /// 同组分页码
        /// </summary>
        public string IPage
        {
            get
            {
                return iPage;
            }
            set
            {
                iPage = value;
            }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns></returns>
        public int Prieview()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

                FS.HISFC.Models.Base.PageSize pageSize = new FS.HISFC.Models.Base.PageSize();

                FS.HISFC.BizLogic.Manager.PageSize pageMgr = new FS.HISFC.BizLogic.Manager.PageSize();

                print.SetPageSize(pageSize);

                print.PrintPreview(0, 0, this);
            }

            return 1;
        }

        public int Print()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("compound", 0, 0);
                //上边距
                ps.Top = 0;
                //左边距
                ps.Left = 0;
                print.SetPageSize(ps);
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.PrintPage(0, 0, this);
            }

            return 1;
        }

        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        } 
        #endregion

    }
}
