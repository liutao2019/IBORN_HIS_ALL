using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.ZhuHai.ZDWY.IInjectBottleLabel
{
    /// <summary>
    /// ucPrintCureNewControl<br></br>
    /// <Font color='#FF1111'>[功能描述:门诊注射瓶签打印{EB016FFE-0980-479c-879E-225462ECA6D0}]</Font><br></br>
    /// [创 建 者: 耿晓雷]<br></br>
    /// [创建时间: 2010-7-29]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///		/>
    /// </summary>
    public partial class ucPrintBottleLabel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrintBottleLabel()
        {
            InitializeComponent();
        }

        #region 私有变量

        /// <summary>
        /// 当前记录的组合号
        /// </summary>
        private string rememberComboNO = " ";

        /// <summary>
        /// 每页打印的行数
        /// </summary>
        private int printCount = 5;

        public bool IsReprint
        {
            set
            {
                this.lblReprint.Visible = value;
            }
        }

        #endregion

        #region 公开方法


        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="totPage"></param>
        /// <param name="currPage"></param>
        public void SetData(List<FS.HISFC.Models.Nurse.Inject> alData, int totPage, int currPage)
        {
            this.SetLable(alData, totPage, currPage);
            this.SetFpData(alData);
        }

        #endregion

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="totPage"></param>
        /// <param name="currPage"></param>
        private void SetLable(List<FS.HISFC.Models.Nurse.Inject> alData, int totPage, int currPage)
        {
            //标题
            //this.lblTitle.Text = this.interManager.GetHospitalName() + "输液单";
            //位置
            int localX = 0;
            if (this.lblTitle.Width < this.Width)
            {
                localX = (this.Width - this.lblTitle.Width) / 2;
            }
            this.lblTitle.Location = new Point(localX, this.lblTitle.Location.Y);

            //打印时间
            this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            //页码
            //this.lblPage.Text = currPage.ToString() + "/共" + totPage.ToString() + "贴";
            //取出第一个注射记录
            FS.HISFC.Models.Nurse.Inject inject = alData[0];
            //门诊号也就是病历号
            this.labelOutpatient.Text = inject.Patient.PID.CardNO;

            //科室
            this.lblDept.Text = inject.Item.Order.DoctorDept.Name;
            //姓名
            this.lblName.Text = inject.Patient.Name;
            //性别
            this.lblSex.Text = (inject.Patient.Sex.ID.ToString() == "M") ? "男" : "女";
            //年龄
            this.lblAge.Text = (DateTime.Now.Year - inject.Patient.Birthday.Year).ToString() + "岁";
            //流水号
            this.lblPrintNo.Text = inject.PrintNo;
            //序号
            List<string> orderList = new List<string>();
            string strOrderNo = "";
            foreach (FS.HISFC.Models.Nurse.Inject tmpInject in alData)
            {
                if (!orderList.Contains(tmpInject.OrderNO))
                {
                    orderList.Add(tmpInject.OrderNO);
                    strOrderNo += tmpInject.OrderNO + ".";
                }
            }
            this.lblOrderNo.Text = strOrderNo;
            //开方医生
            this.lblDoctor.Text = inject.Item.Order.Doctor.Name;
        }

        private void Clear()
        {
            labelUsage.Text = "";
            neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 将数据填入farpoint中
        /// </summary>
        /// <param name="alData"></param>
        private void SetFpData(List<FS.HISFC.Models.Nurse.Inject> alData)
        {
            int showGroupNO = 1;
            string curComboNo = string.Empty;
            int pageRowCount = 0;
            decimal freqCount = 1;
            int minus = 0;//剩余注射次数
            foreach (FS.HISFC.Models.Nurse.Inject inject in alData)
            {
                if ((!string.IsNullOrEmpty(curComboNo)
                    && inject.Item.Order.Combo.ID != curComboNo)
                    || pageRowCount > printCount)
                {
                    if (pageRowCount > printCount)
                    {
                        this.lblPage.Text = "接上页";
                    }
                    else
                    {
                        this.lblPage.Text = "";
                    }

                    //PrintPage(freqCount);
                    if (freqCount < minus)
                    {
                        PrintPage(FS.FrameWork.Function.NConvert.ToInt32(freqCount));
                    }
                    else
                    {
                        PrintPage(minus);
                    }

                    pageRowCount = 0;
                    curComboNo = inject.Item.Order.Combo.ID;

                    showGroupNO++;

                    neuSpread1_Sheet1.Rows.Add(pageRowCount, 1);

                    this.neuSpread1_Sheet1.Cells[pageRowCount, 0].Text = showGroupNO.ToString();
                    this.neuSpread1_Sheet1.Cells[pageRowCount, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[pageRowCount, 1].Text = inject.Item.Name;
                    this.neuSpread1_Sheet1.Cells[pageRowCount, 2].Text = inject.Item.Item.Specs;
                    this.neuSpread1_Sheet1.Cells[pageRowCount, 3].Text = inject.Item.Order.DoseOnce + inject.Item.Order.DoseUnit;
                }
                else
                {
                    curComboNo = inject.Item.Order.Combo.ID;

                    neuSpread1_Sheet1.Rows.Add(pageRowCount, 1);

                    this.neuSpread1_Sheet1.Cells[pageRowCount, 0].Text = showGroupNO.ToString();
                    this.neuSpread1_Sheet1.Cells[pageRowCount, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[pageRowCount, 1].Text = inject.Item.Name;
                    this.neuSpread1_Sheet1.Cells[pageRowCount, 2].Text = inject.Item.Item.Specs;
                    this.neuSpread1_Sheet1.Cells[pageRowCount, 3].Text = inject.Item.Order.DoseOnce + inject.Item.Order.DoseUnit;
                }

                //根据院注次数打印瓶签
                freqCount = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(inject.Item.Order.Frequency.ID);
                int ConfirmedInjectCount=inject.Item.ConfirmedInjectCount;//已确认的院注次数

                int InjectCount=inject.Item.InjectCount;//院内注射次数
                if (InjectCount > ConfirmedInjectCount)
                {
                    minus = InjectCount - ConfirmedInjectCount;
                }
                this.labelUsage.Text = inject.Item.Order.Usage.Name;

                pageRowCount++;
            }
            if (freqCount < minus)
            {
                PrintPage(FS.FrameWork.Function.NConvert.ToInt32(freqCount));
            }
            else
            {
                PrintPage(minus);
            }
        }

        private void PrintPage(int printCount)
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                return;
            }

            FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize();
            FS.HISFC.BizLogic.Manager.PageSize pMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            pSize = pMgr.GetPageSize("门诊瓶签");

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsShowFarPointBorder = true;
            print.SetPageSize(pSize);

            for (int i = 0; i < printCount; i++)
            {
                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    print.PrintPreview(pSize.Left, pSize.Top, this);
                }
                else
                {
                    print.PrintPage(pSize.Left, pSize.Top, this);
                }
            }
            Clear();
        }
    }
}
