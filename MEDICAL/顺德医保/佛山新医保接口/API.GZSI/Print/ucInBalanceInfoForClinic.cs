using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.Print
{
    public partial class ucInBalanceInfoForClinic : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInBalanceInfoForClinic()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 患者入出转转业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        private LocalManager localMgr = new LocalManager();

        public int SetValue(FS.HISFC.Models.Registration.Register reg)
        {
            
            try
            {
                this.neuSpread1_Sheet1.Cells[2, 1].Text = "患者姓名:" + reg.Name;
                this.neuSpread1_Sheet1.Cells[2, 2].Text = "性别:" + reg.Sex.Name;
                this.neuSpread1_Sheet1.Cells[2, 3].Text = "年龄:" + reg.Age;
                this.neuSpread1_Sheet1.Cells[2, 4].Text = "身份证号:" + reg.IDCard;
                this.neuSpread1_Sheet1.Cells[2, 5].Text = "社保卡号:" + reg.SSN;

                this.neuSpread1_Sheet1.Cells[3, 1].Text = "挂号时间:" + reg.SIMainInfo.BalanceDate.ToShortDateString();
                //this.neuSpread1_Sheet1.Cells[3, 3].Text = "险种类型:" + ConstAKA130manger.GetObjectFromID(consMgr.GetConstant("PactToMedType", patient.Pact.ID).Memo).Name;
                this.neuSpread1_Sheet1.Cells[3, 3].Text = "就医登记号:" + reg.SIMainInfo.RegNo;

                this.neuSpread1_Sheet1.Cells[4, 1].Text = "就医地:广州市";
                this.neuSpread1_Sheet1.Cells[4, 3].Text = "医院名称:广州中医药大学附属第三医院";
                this.neuSpread1_Sheet1.Cells[4, 5].Text = "医院等级:二级";

                //this.neuSpread1_Sheet1.Cells[5, 1].Text = "入院方式:" + consMgr.GetConstant("INSOURCE", reg.PVisit.InSource.ID).Name;
                //this.neuSpread1_Sheet1.Cells[5, 4].Text = "就医登记号:" + reg.SIMainInfo.RegNo;
                //this.neuSpread1_Sheet1.Cells[5, 5].Text = "出院科室:" + reg.PVisit.PatientLocation.Dept.Name;

                this.neuSpread1_Sheet1.Cells[6, 1].Text = "主要诊断:" + localMgr.QueryICDByCode(reg.SIMainInfo.InDiagnose.ID).Name;
                this.neuSpread1_Sheet1.Cells[6, 4].Text = "单位:元(保留两位小数)";

                this.neuSpread1_Sheet1.Cells[7, 1].Text = "入院时间:" + reg.PVisit.InTime.ToShortDateString();
                this.neuSpread1_Sheet1.Cells[7, 2].Text = "出院时间:" + reg.PVisit.OutTime.ToShortDateString();
                TimeSpan indays = reg.PVisit.OutTime - reg.PVisit.InTime;
                this.neuSpread1_Sheet1.Cells[7, 4].Text = "共" + indays.Days + "天                   单位:元(保留两位小数)";

                this.neuSpread1_Sheet1.Cells[8, 1].Text = "总费用:" + reg.SIMainInfo.TotCost.ToString();
                this.neuSpread1_Sheet1.Cells[8, 2].Text = "统筹内费用:" + reg.SIMainInfo.Bka826;//patient.SIMainInfo.PubCost.ToString();// "????";
                this.neuSpread1_Sheet1.Cells[8, 4].Text = "自费费用:" + reg.SIMainInfo.Bka825;//patient.SIMainInfo.OwnCost.ToString(); ;// "????";
                this.neuSpread1_Sheet1.Cells[8, 5].Text = "本次起付标准:" + reg.SIMainInfo.Aka151.ToString();

                this.neuSpread1_Sheet1.Cells[9, 2].Text = reg.SIMainInfo.PubCost.ToString();//基金支付合计
                this.neuSpread1_Sheet1.Cells[10, 2].Text = reg.SIMainInfo.Ake039.ToString();//统筹基金
                this.neuSpread1_Sheet1.Cells[11, 2].Text = reg.SIMainInfo.Akb066.ToString();//个人账户基金
                this.neuSpread1_Sheet1.Cells[12, 2].Text = reg.SIMainInfo.Ake035.ToString();//公务员员医疗补助
                this.neuSpread1_Sheet1.Cells[13, 2].Text = reg.SIMainInfo.Ake026.ToString();//补充医疗保险基金
                this.neuSpread1_Sheet1.Cells[14, 2].Text = reg.SIMainInfo.Ake029.ToString();//大病保险
                this.neuSpread1_Sheet1.Cells[15, 2].Text = reg.SIMainInfo.Bka821.ToString();//医疗救助
                this.neuSpread1_Sheet1.Cells[16, 2].Text = "0.00";//伤残人员医疗保险基金
                this.neuSpread1_Sheet1.Cells[17, 2].Text = "0.00";//离休人员医疗统筹基金
                this.neuSpread1_Sheet1.Cells[18, 2].Text = reg.SIMainInfo.Bka840.ToString();//其他基金

                this.neuSpread1_Sheet1.Cells[9, 5].Text = reg.SIMainInfo.OwnCost.ToString();//自费金额

                this.neuSpread1_Sheet1.Cells[19, 1].Text = "门诊号:" + reg.PID.CardNO;//住院号
                this.neuSpread1_Sheet1.Cells[19, 5].Text = "打印日期:" + localMgr.GetSysDate("yyyyMMdd");//打印日期
            }
            catch
            {
                return -1;
            }
            return 1;
        }

        public int Query(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg == null)
            {
                MessageBox.Show("查找患者失败");
                return -1;
            }

            reg = localMgr.GetOutPatientBalanceInfo(reg, "1");

            if (reg.SIMainInfo.Bka438 != "2")
            {
                MessageBox.Show("患者未医保结算!");
                return -1;
            }
            if (SetValue(reg) < 0)
            {
                MessageBox.Show("赋值失败!");
                return -1;
            }
            //PrintInBalanceInfo();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public int ShowInfo(FS.HISFC.Models.Registration.Register reg)
        {
            if (SetValue(reg) < 0)
            {
                MessageBox.Show("赋值失败!");
                return -1;
            }
            return 1;
        }

        public int PrintInBalanceInfo(int left,int top)
        {
            try
            {
                //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                //FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                //FS.HISFC.Models.Base.PageSize ps = pgMgr.GetPageSize("InPatientZHJSD");  //住院珠海医保结算单
                //if (ps == null)
                //{
                //    //默认大小
                //    ps = new FS.HISFC.Models.Base.PageSize("InPatientZHJSD", 830, 480);
                //}
                //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //print.SetPageSize(ps);

                //if ((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager)
                //{
                //    //print.PrintPreview(ps.Left, ps.Top, this);
                //    print.PrintPage(ps.Left, ps.Top, neuPanel1);
                //}
                //else
                //{
                //    print.PrintPage(ps.Left, ps.Top, neuPanel1);
                //}

                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize prSize = pgMgr.GetPageSize("InPatientGDJSD");
                FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize("MZGuide", prSize.Width, prSize.Height);
                print.SetPageSize(pSize);
                //获得打印机名
                //print.PrintDocument.PrinterSettings.PrinterName = "InPatientGDJSD";
                //if (true)
                //{
                return print.PrintPreview(left, top, this);
                //}
                //return print.PrintPage(left, top, this);
            }
            catch 
            {
                MessageBox.Show("打印发生错误,请确认是否有连接好打印机");
                return -1;
            }
            return 1;
        }
    }
}
