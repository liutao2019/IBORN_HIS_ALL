using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectCurePrint
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
    public partial class ucPrintCureNewControl : FS.FrameWork.WinForms.Controls.ucBaseControl
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

        /// <summary>
        /// 当前显示组号
        /// </summary>
        private int showGroupNO = 0;

        /// <summary>
        /// 当前记录的组合号
        /// </summary>
        private string rememberComboNO = " ";

        /// <summary>
        /// 合并第一列单元格的起始行号
        /// </summary>
        private int spanRowIndex = 1;

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
            //this.lblTitle.Text = this.interManager.GetHospitalName() + "输液单";
            //位置
            int localX = 0;
            if (this.lblTitle.Width < this.Width)
            {
                localX = (this.Width - this.lblTitle.Width) / 2;
            }
            this.lblTitle.Location = new Point(localX, this.lblTitle.Location.Y);
            //打印时间
            this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd 　HH:mm:ss");
        
            //页码
            this.lblPage.Text = currPage.ToString() + "/共" + totPage.ToString() + "贴";
            //取出第一个注射记录
            FS.HISFC.Models.Nurse.Inject inject = alData[0];
            //门诊号也就是病历号
            this.labelOutpatient.Text =inject.Patient.PID.CardNO;
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

        /// <summary>
        /// 将数据填入farpoint中
        /// </summary>
        /// <param name="alData"></param>
        private void SetFpData(List<FS.HISFC.Models.Nurse.Inject> alData)
        {
            showGroupNO = 1;//必须初始化，否则重新打印组号会出错
            spanRowIndex = 0;

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Gray, 1, false, false, false, true);
            FarPoint.Win.LineBorder allBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);
            string curComboNo = string.Empty;
            int j = 0;
            for (int i = 0; i < alData.Count; i++)
            {
                FS.HISFC.Models.Nurse.Inject inject = alData[i];

                if (!string.IsNullOrEmpty(curComboNo) && (inject.Item.Order.Combo.ID != curComboNo))
                {
                    p.PrintPage(12, 1, this);
                    j = 0;
                    curComboNo = inject.Item.Order.Combo.ID;

                    #region 清空表
                    ///瓶签上的医嘱数最多为5行
                    for (int k = 0; k < 5; k++)
                    {
                        this.neuSpread1_Sheet1.Cells[k, 0].Text = "";
                        this.neuSpread1_Sheet1.Cells[k, 1].Text = "";
                        this.neuSpread1_Sheet1.Cells[k, 2].Text = "";
                        this.neuSpread1_Sheet1.Cells[k, 3].Text = "";
                    }
                    #endregion

                    showGroupNO++;
                    //rememberComboNO = inject.Item.Order.Combo.ID;
                    spanRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                    this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].Border = allBorder;

                    this.neuSpread1_Sheet1.Cells[j, 0].Text = showGroupNO.ToString();
                    this.neuSpread1_Sheet1.Cells[j, 1].Text = inject.Item.Name;
                    this.neuSpread1_Sheet1.Cells[j, 2].Text = inject.Item.Item.Specs;
                    this.neuSpread1_Sheet1.Cells[j, 3].Text = inject.Item.Order.DoseOnce + inject.Item.Order.DoseUnit;


                }
                else
                {
                    curComboNo = inject.Item.Order.Combo.ID;
                    this.neuSpread1_Sheet1.Cells[j, 0].Text = showGroupNO.ToString();
                    this.neuSpread1_Sheet1.Cells[j, 1].Text = inject.Item.Name;
                    this.neuSpread1_Sheet1.Cells[j, 2].Text = inject.Item.Item.Specs;
                    this.neuSpread1_Sheet1.Cells[j, 3].Text = inject.Item.Order.DoseOnce + inject.Item.Order.DoseUnit;
                }
                j++;
                if (j > 4)
                {
                    DialogResult dr = MessageBox.Show("药品组合超过5个，未打印完全,请手写", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        //确定
                    }
                    break;
                }
            }
            p.PrintPage(12, 1, this);
       
            //else 
            //{
            //    DialogResult dr = MessageBox.Show("药品组合超过5个，不予打印，请手写", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //    if (dr == DialogResult.OK)
            //    {
            //        //确定
            //    }
            //}

        }

        #endregion
    }
}
