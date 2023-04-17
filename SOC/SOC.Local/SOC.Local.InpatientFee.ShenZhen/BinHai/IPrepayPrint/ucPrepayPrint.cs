using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.IPrepayPrint
{
    /// <summary>
    /// [功能描述: 南庄/桥头预约金发票<br></br>
    /// [创 建 者: 刘恒荣]<br></br>
    /// [创建时间: 2011-03-04]<br></br>
    /// </summary>
    public partial class ucPrepayPrint : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint
    {
        public ucPrepayPrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 控制参数
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;

        #region IPrepayPrint 成员

        ////{014680EC-6381-408b-98FB-A549DAA49B82}
        // 摘要:
        //     设置押金发票打印参数
        //
        // 参数:
        //   patient:
        //     住院患者基本信息实体
        //
        //   alPrepay:
        //     预交金打印实体
        //
        // 返回结果:
        //     成功 1 失败 -1
        public int SetValue(FS.HISFC.Models.RADT.PatientInfo patient, System.Collections.ArrayList alPrepay)
        {
            throw new NotImplementedException();
        }

        public int Clear()
        {
            return 0;
        }

        public int Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("初始化打印机失败!" + ex.Message);

                    return -1;
                }
                //获得打印机名
                //string printer = this.controlIntegrate.GetControlParam<string>("ZYYJFP", true, "");
                //if (!string.IsNullOrEmpty(printer))
                //{
                //    print.PrintDocument.PrinterSettings.PrinterName = printer;
                //}
                //FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("", 866, 366);
                //print.SetPageSize(ps);
                //print.PrintPage(0, 0, this.neuPanel1);
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("ZYYJ");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                   // pageSize = new FS.HISFC.Models.Base.PageSize("ZYYJFP", 840, 360);
                    pageSize = new FS.HISFC.Models.Base.PageSize("ZYYJ", 840, 560);
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
                print.PrintPreview(0, 0, this.neuPanel1);
            }
            else
            {
                print.PrintPage(0, 0, this.neuPanel1);
            }
        
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 1;
            }

            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction trans)
        {
            return;
        }

        /// <summary>
        /// 设置预交金发票中的数据
        /// </summary>
        /// <param name="patient">【实体】病人</param>
        /// <param name="prepay">【实体】预交金</param>
        /// <returns></returns>
        public int SetValue(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.Prepay prepay)
        {
            string upper = this.controlIntegrate.GetControlParam<string>("ZYYJDX",true,"");
            if (string.IsNullOrEmpty(upper))
            {
                System.Windows.Forms.MessageBox.Show("预交金收取成功：" + prepay.FT.PrepayCost + "元！");
            }
            else
            {
                string toUpper = FS.FrameWork.Public.String.LowerMoneyToUpper(prepay.FT.PrepayCost);
                System.Windows.Forms.MessageBox.Show("预交金收取成功：" + toUpper);
            }
             try
            {
                #region 收据打印
                //
                FS.HISFC.BizProcess.Integrate.Manager mgr = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
                //医院名称
                this.lblHosName.Text = FS.FrameWork.Management.Connection.Hospital.Name;
                //票据号
                this.neuLblPrepayNO.Text = prepay.RecipeNO;
                //年
                this.lblPriSwYear.Text = prepay.PrepayOper.OperTime.Year.ToString();
                //月
                this.lblPriSwMonth.Text = prepay.PrepayOper.OperTime.Month.ToString();
                //日
                this.lblPriSwDay.Text = prepay.PrepayOper.OperTime.Day.ToString();
                //操作日期
                this.neuLblOperDate.Text = conMgr.GetDateTimeFromSysDateTime().ToString();
                //患者姓名
                this.lblPatientName.Text = patient.Name;
                //住院科室
                this.lblDept.Text = patient.PVisit.PatientLocation.Dept.Name;
                //住院号码
                this.lblInNo.Text = patient.PID.PatientNO;
                //床号
                this.neuLblBedNo.Text = patient.PVisit.PatientLocation.Bed.ID;
                //住址
                string address = string.Empty;
                if (!string.IsNullOrEmpty(patient.AddressHome))
                {
                    address = patient.AddressHome;
                }
                this.neuLblAddress.Text = address;
                //工作单位
                string jobAddress = string.Empty;
                if (!string.IsNullOrEmpty(patient.CompanyName))
                {
                    jobAddress = patient.CompanyName.ToString();
                }
                this.neuLblJob.Text = jobAddress;
                //支付方式
                this.lblPrePayTypeName.Text = mgr.GetConstansObj("PAYMODES", prepay.PayType.ID).Name;
                //预交金大写
                this.lblCnCost.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(prepay.FT.PrepayCost);
                //预交金额
                this.lblCost.Text = FS.FrameWork.Public.String.FormatNumber(prepay.FT.PrepayCost, 2).ToString();
                //收款员
                this.lblPayeer.Text = prepay.PrepayOper.ID;
               
                 //医院名称
                this.lbHosName1.Text = FS.FrameWork.Management.Connection.Hospital.Name;
                #endregion


            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public System.Data.IDbTransaction Trans
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
