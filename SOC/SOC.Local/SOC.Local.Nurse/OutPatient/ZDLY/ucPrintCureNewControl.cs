using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.ZDLY
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
    public partial class ucPrintCureNewControl : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint
    {
        #region 构造函数
        public ucPrintCureNewControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量

        /// <summary>
        /// 整合的管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// 缓存用法
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper usageHelper;
        /// <summary>
        /// 纸张
        /// </summary>
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        #endregion

        #region 公开方法
        public void Init(System.Collections.ArrayList alPrintData)
        {
            //用来将要分开打的数据分开
            FS.HISFC.Models.Nurse.Inject info = alPrintData[0] as FS.HISFC.Models.Nurse.Inject;
          
            Hashtable htInject = new Hashtable();
            foreach (FS.HISFC.Models.Nurse.Inject inject in alPrintData)
            {
                string key = string.Empty;
                //为了适应补打的时候界面传过来的判断标识为inject.User03为true ，而注射时间为空的问题，不然同组药品多次注射就无法分组打瓶签而 修改，xiaohf。
                if (string.IsNullOrEmpty(inject.Item.User03.Trim()))
                {
                    key = inject.Item.Order.Combo.ID + inject.PrintNo;//补打的时候，将打印号取代注射时间，和组合号一起作为主键
                }
                else
                {
                    key = inject.Item.Order.Combo.ID + inject.Item.User03.Trim();
                }
                if (htInject.ContainsKey(key))
                {
                    List<FS.HISFC.Models.Nurse.Inject> injectList = htInject[key] as List<FS.HISFC.Models.Nurse.Inject>;
                    injectList.Add(inject);
                }
                else
                {
                    List<FS.HISFC.Models.Nurse.Inject> injectList = new List<FS.HISFC.Models.Nurse.Inject>();
                    htInject.Add(key, injectList);
                    injectList.Add(inject);
                }
            }
            //分别打印
            int pageCount = 1;
            int totPageCount = htInject.Count;
            foreach (string key in htInject.Keys)
            {
                SetData(htInject[key] as List<FS.HISFC.Models.Nurse.Inject>, totPageCount, pageCount);
                pageCount++;

            }

        }
       
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
            FS.HISFC.Models.Nurse.Inject objInject = alData[0] as FS.HISFC.Models.Nurse.Inject;
            this.Print();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="totPage"></param>
        /// <param name="currPage"></param>
        private void SetLable(List<FS.HISFC.Models.Nurse.Inject> alData, int totPage, int currPage)
        {
            //标题
            this.lblTitle.Text = this.interManager.GetHospitalName() + "输液单";
            //位置
            int localX = 0;
            if (this.lblTitle.Width < this.Width)
            {
                localX = (this.Width - this.lblTitle.Width) / 2;
            }
            this.lblTitle.Location = new Point(localX, this.lblTitle.Location.Y);
            //页码
            this.lblPage.Text = currPage.ToString() + "/共" + totPage.ToString() + "贴";
            //取出第一个注射记录
            FS.HISFC.Models.Nurse.Inject inject = alData[0];
            //科室
            //this.lblAge.Text = inject.Item.Order.DoctorDept.Name;
            //姓名
            this.lblName.Text = inject.Patient.Name;
            //补打
            if (inject.User03.Trim().ToUpper() == "TRUE")
            {
                this.lblReprint.Visible = true;
            }
            else
            {
                this.lblReprint.Visible = false;
            }
            //性别
            this.lblCard.Text = (inject.Patient.Sex.ID.ToString() == "M") ? "男" : "女";
            //年龄
            this.lblAge.Text = injectMgr.GetAge(inject.Patient.Birthday, System.DateTime.Now);
            //(DateTime.Now.Year - inject.Patient.Birthday.Year).ToString() + "岁";            
            this.date.Text = "日期："+System.DateTime.Now.ToShortDateString();
            //流水号
            //this.lblPrintNo.Text = inject.PrintNo;
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
            //this.lblOrderNo.Text = strOrderNo;
            //开方医生
            //this.lblDoctor.Text = inject.Item.Order.Doctor.Name;
        }

        /// <summary>
        /// 将数据填入farpoint中
        /// </summary>
        /// <param name="alData"></param>
        private void SetFpData(List<FS.HISFC.Models.Nurse.Inject> alData)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            if (alData == null)
            {
                return;
            }

            for (int i = 0; i < alData.Count; i++)
            {
                FS.HISFC.Models.Nurse.Inject inject = alData[i];
                if (i == 0)
                {
                    this.date.Text ="注射时间："+ inject.Item.User03;
                }
                this.neuSpread1_Sheet1.Rows.Add(i, 1);
                this.neuSpread1_Sheet1.Cells[i, 0].Text = inject.Item.Name;
                this.neuSpread1_Sheet1.Cells[i, 1].Text = inject.Item.Order.DoseOnce.ToString() + inject.Item.Order.DoseUnit.ToString();
                this.neuSpread1_Sheet1.Cells[i, 2].Text = inject.Item.Order.Usage.Name;// inject.Item.Order.DoseOnce + inject.Item.Order.DoseUnit;
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OutPatientCureCard");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientCureCard", 400, 300);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }
        #endregion
    }
}
