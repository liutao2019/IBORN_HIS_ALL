using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.CircuitControl
{
    ////{C42F14B0-81D2-4eae-B708-6431EA819622}

    public partial class ucCircuitCardPrint : UserControl
    {
        public ucCircuitCardPrint()
        {
            InitializeComponent();
        }

        private ArrayList alOrders = new ArrayList();
        public ArrayList AlOrders
        {
            set
            {
                this.alOrders = value;
                this.SetItems();
            }
        }

        public string execTime = string.Empty;

        public DateTime QueryDate
        {
            get
            {
                return queryDate;
            }
            set
            {
                queryDate = value;
            }
        }

        private DateTime queryDate;

        /// <summary>
        /// 是否打印备注
        /// </summary>
        public bool isPrintMemo = false;

        ///// <summary>
        ///// 每页打印的行数
        ///// </summary>
        //private int printRowNum = 16;

        ///// <summary>
        ///// 每页打印的行数
        ///// </summary>
        //public int PrintRowNum
        //{
        //    get
        //    {
        //        return printRowNum;
        //    }
        //}

        private void SetItems()//构造预览页面内容
        {
            neuSpread1_Sheet1.RowCount = 1;
            neuSpread1_Sheet1.RowCount = 13;
            //for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
            //{
            //    for (int j = 0; j < this.neuSpread1_Sheet1.ColumnCount; j++)
            //    {
            //        this.neuSpread1_Sheet1.Cells[i, j].Text = string.Empty;
            //    }
            //}

            //上边框虚线
            FarPoint.Win.ComplexBorder border_Top_Virtual = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.HairLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None));

            //无边框
            FarPoint.Win.ComplexBorder border_None = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None));

            //下边框实线
            FarPoint.Win.ComplexBorder border_Button = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));

            //上下边框
            FarPoint.Win.ComplexBorder border_Button_Top_Virtual = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.HairLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));

            string preCombNo = string.Empty;

            Dictionary<string, string> dicExecOrder = new Dictionary<string, string>();
            for (int i = 0; i < alOrders.Count; i++)
            {
                string s = alOrders[i].ToString();
                string[] items = s.Split('|');

                string key = items[4];
                string subCombNo = items[(int)EnumCol.Z组标记];//组号
                string combNo = key;
                if (dicExecOrder.ContainsKey(key))
                {
                }
                else
                {
                    dicExecOrder.Add(key, null);
                    this.neuSpread1_Sheet1.Cells[i + 1, 3].Text = subCombNo + ")";//组号

                    this.neuSpread1_Sheet1.Cells[i + 1, 10].Text = items[10];//用法

                    #region 频次\时间
                    //this.neuSpread1_Sheet1.Cells[i + 1, 11].Text = items[9];//次数
                    DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(items[12]);
                    if (queryDate > new DateTime(2000, 1, 1))
                    {
                    }
                    else
                    {
                        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
                        queryDate = dbMgr.GetDateTimeFromSysDateTime();
                    }
                    //this.neuSpread1_Sheet1.Cells[i + 1, 11].Text = Function.GetShowTime(dt, queryDate);//次数

                    #endregion
                }
                this.neuSpread1_Sheet1.Cells[i + 1, 4].Text = items[5];//名称
                this.neuSpread1_Sheet1.Cells[i + 1, 5].Text = items[6];//名称
                this.neuSpread1_Sheet1.Cells[i + 1, 9].Text = items[11];//用量               

                if (preCombNo != combNo)
                {
                    preCombNo = combNo;
                    neuSpread1_Sheet1.Rows[i + 1].Border = border_Top_Virtual;
                    neuSpread1_Sheet1.Rows[i + 1].Height = 40;
                    if (i + 1 == alOrders.Count)
                    {
                        neuSpread1_Sheet1.Rows[i + 1].Border = border_Button_Top_Virtual;
                    }
                }
                else
                {
                    neuSpread1_Sheet1.Rows[i + 1].Border = border_None;
                    if (i + 1 == alOrders.Count)
                    {
                        neuSpread1_Sheet1.Rows[i + 1].Border = border_Button;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetItemsFZC(IList<FS.HISFC.Models.Order.ExecOrder> orderList)//构造预览页面内容
        {
            neuSpread1_Sheet1.RowCount = 1;
            neuSpread1_Sheet1.RowCount = 13;

            //上边框虚线
            FarPoint.Win.ComplexBorder border_Top_Virtual = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.HairLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None));

            //无边框
            FarPoint.Win.ComplexBorder border_None = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None));

            //下边框实线
            FarPoint.Win.ComplexBorder border_Button = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));

            //上下边框
            FarPoint.Win.ComplexBorder border_Button_Top_Virtual = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.HairLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));

            //分组 用于添加组号
            var comboSplitList = orderList.GroupBy(o => o.Order.Combo.ID + o.DateUse.Date.ToString());
            int rowCount = 1;

            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            foreach (var list in comboSplitList.ToList())
            {
                IList<FS.HISFC.Models.Order.ExecOrder> oList = list.ToList<FS.HISFC.Models.Order.ExecOrder>();

                for (int i = 0; i < list.Count(); i++)
                {
                    this.neuSpread1_Sheet1.Cells[rowCount, 10].Text = oList[i].Order.Usage.Name;//用法

                    DateTime dtUse = oList[i].DateUse;// {A196A50F-36E2-49b4-B530-DCC38D9D4464}
                    string useDate = "";
                    int hour = dtUse.Hour;
                   
                    if (oList[i].Order.OrderType.IsDecompose)
                    {
                        this.neuSpread1_Sheet1.Cells[rowCount, 3].Text = "长" + oList[i].Order.SubCombNO.ToString(); //医嘱类别
                        this.neuSpread1_Sheet1.Cells[rowCount, 4].Text = oList[i].Order.Combo.Memo; //组
                        this.neuSpread1_Sheet1.Cells[rowCount, 4].Tag = oList[i].Order.Combo.ID; //组
                        this.neuSpread1_Sheet1.Cells[rowCount, 11].Text = oList[i].Order.Frequency.ID;//频次
                        //if (hour == 0)// {A196A50F-36E2-49b4-B530-DCC38D9D4464}
                        //{
                        //    hour = 24;
                        //    dtUse = dtUse.AddDays(-1);
                        //}

                        //if (hour <= 12)
                        //{
                        //    useDate = dtUse.ToString("MM.dd ") + hour + "a";
                        //}
                        //else
                        //{
                        //    useDate = dtUse.ToString("MM.dd ") + (hour - 12) + "p";
                        //}
                        this.neuSpread1_Sheet1.Cells[rowCount, 12].Text = oList[i].ChargeOper.Memo;//执行时间
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[rowCount, 3].Text = "临" + oList[i].Order.SubCombNO.ToString();//医嘱类别
                        this.neuSpread1_Sheet1.Cells[rowCount, 11].Text = oList[i].Order.Frequency.ID;//
                        useDate = dtUse.ToString("MM.dd");
                        this.neuSpread1_Sheet1.Cells[rowCount, 12].Text = useDate;//执行时间
                    }

                    //自备、嘱托标记  用于护士打印单据和医嘱单显示区分
                    string byoStr = "";
                    if (!oList[i].Order.OrderType.IsCharge || oList[i].Order.Item.ID == "999")
                    {
                        if (oList[i].Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            byoStr = "[自备]";
                        }
                        else
                        {
                            byoStr = "[嘱托]";
                        }
                    }
                    this.neuSpread1_Sheet1.Cells[rowCount, 5].Text = byoStr + oList[i].Order.Item.Name;//项目名称
                    this.neuSpread1_Sheet1.Cells[rowCount, 9].Text = oList[i].Order.DoseOnce + oList[i].Order.DoseUnit;//用量
                    //this.neuSpread1_Sheet1.Cells[rowCount, 17].Text = oList[i].Order.Combo.ID + oList[i].DateUse.Date.ToString();
                    this.neuSpread1_Sheet1.Cells[rowCount, 17].Text = oList[i].Order.Memo;//{7B238DEC-A1AD-41e4-AF75-29629920C41B}
                    //备注
                    if (this.isPrintMemo)
                    {
                        this.neuSpread1_Sheet1.Cells[rowCount, 12].Text = oList[i].Order.Memo;
                    }
                    neuSpread1_Sheet1.Rows[rowCount].Tag = oList[i];
                    //画组号
                    //if (list.Count() == 1)
                    //{
                    //    //就一个不用画组号了
                    //}
                    //else
                    //{
                    //    if (i == 0)
                    //    {
                    //        //组合头
                    //        this.neuSpread1_Sheet1.Cells[rowCount, 4].Text = "┏";
                    //        this.neuSpread1_Sheet1.Rows[rowCount].Border = border_Button_Top_Virtual;
                    //    }
                    //    else if (i == (list.Count() - 1))
                    //    {
                    //        //组合尾
                    //        this.neuSpread1_Sheet1.Cells[rowCount, 4].Text = "┗";
                    //        this.neuSpread1_Sheet1.Cells[rowCount, 11].Text = "";
                    //        this.neuSpread1_Sheet1.Cells[rowCount, 10].Text = "";

                    //        neuSpread1_Sheet1.Rows[rowCount].Border = border_Button;
                    //    }
                    //    else
                    //    {
                    //        this.neuSpread1_Sheet1.Cells[rowCount, 4].Text = "┃";
                    //        this.neuSpread1_Sheet1.Cells[rowCount, 11].Text = "";
                    //        this.neuSpread1_Sheet1.Cells[rowCount, 10].Text = "";
                    //    }
                    //}
                    rowCount++;
                }
            }

            //FS.SOC.Local.Order.ZhuHai.Classes.Function.DrawComboLeft(this.neuSpread1_Sheet1, 16, 4);

            //for (int row = 0; row < neuSpread1_Sheet1.RowCount; row++)
            //{
            //    neuSpread1_Sheet1.Rows[row].Border = border_None;
            //    if (row == neuSpread1_Sheet1.RowCount - 1)
            //    {
            //        neuSpread1_Sheet1.Rows[row].Border = bevelBorder1;
            //    }
            //    else if (neuSpread1_Sheet1.Cells[row, 4].Text.Trim() == "┗")
            //    {
            //        neuSpread1_Sheet1.Rows[row].Border = bevelBorder1;
            //    }
            //}
            FS.SOC.Local.Order.ZhuHai.Classes.Function.DrawComboLeft(this.neuSpread1_Sheet1, 3, 4);// {E97273E4-CF5A-47bf-97C6-8025504486C4}

        }
       
        /// <summary>
        /// 设置人员基本信息
        /// </summary>
        /// <param name="patientInfo"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            _patientInfo = patientInfo;

            this.UpdateHeadingInfo();
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="titleDate"></param>
        /// <param name="printTime"></param>
        /// <param name="page"></param>
        public void SetHeader(string title, string titleDate, string printTime, int currentPage, int totalPag)
        {
            _title = title;
            _titleDate = titleDate;
            _printTime = printTime;
            _currentPage = currentPage;
            _totalPag = totalPag;

            this.UpdateHeadingInfo();
        }

        FS.HISFC.Models.RADT.PatientInfo _patientInfo = null;
        string _title;
        string _titleDate;
        string _printTime;
        int _currentPage;
        int _totalPag;

        private void UpdateHeadingInfo()
        {
            string txt = string.Empty;

            try
            {
                if (null != _patientInfo)
                {
                    txt += "病区:" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(_patientInfo.PVisit.PatientLocation.NurseCell.ID) + " ";
                    txt += "  床位:" + _patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
                    txt += "  姓名:" + _patientInfo.Name + " ";
                }

                DateTime dt;
                if (DateTime.TryParse(_printTime, out dt))
                {
                    txt += "  日期:" + this.execTime;//dt.ToString("yyyy.MM.dd");
                }

                //txt += "  执行日期:" + _printTime;
            }
            finally
            {
                this.lblHeadinginfo.Text = txt;
            }
        }

    }
}
