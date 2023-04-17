using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.ObjectModel;

namespace FS.SOC.Local.Registration.FoSan
{
    /// <summary>
    /// 挂号发票打印
    /// </summary>
    public partial class ucRegInvoicePrint : UserControl, FS.HISFC.BizProcess.Interface.Registration.IRegPrint
    {
        public ucRegInvoicePrint()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        /// <summary>
        /// 打印用的标签集合
        /// </summary>
        public Collection<Label> lblPrint;

        /// <summary>
        /// 预览用的标签集合
        /// </summary>
        public Collection<Label> lblPreview;

        private bool isPreview = false;

        private bool IsPreview
        {
            get { return isPreview; }
            set { isPreview = value; }
        }

        #endregion

        #region IRegPrint 成员

        public int Clear()
        {
            return 0;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
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

                FS.HISFC.Models.Base.PageSize ps = null;
                ps = psManager.GetPageSize("GHFP");

                if (ps == null)
                {
                    //没有维护，就采取默认设置
                    ps = new FS.HISFC.Models.Base.PageSize("GHFP", 500, 276);
                    ps.Top = 0;
                    ps.Left = 0;
                }
                
                print.SetPageSize(ps);

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsCanCancel = false;
                print.PrintPage(0, 0, this);

                //打印文档的左侧硬页边距的X坐标，这个硬边距和打印机有关，如果硬边距>0，可以设置打印控件的边距值为负数
                //print.PrintDocument.PrinterSettings.DefaultPageSettings.HardMarginX;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return - 1;
            }

            return 1;
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns></returns>
        public int PrintView()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this);
            return 0;
        }

        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {
            try
            {
                this.InitReceipt();

                //提示[去掉]
                //MessageBox.Show("请记录门诊号：" + register.PID.CardNO);

                //医院名称
                this.lblHospitalName.Text = manageIntegrate.GetHospitalName();

                //挂号发票号
                this.lblInvoiceNo.Text = register.InvoiceNO;

                //门诊号
                this.lblCardNo.Text = register.PID.CardNO;

                //挂号科室
                this.lblDeptName.Text = register.DoctorInfo.Templet.Dept.Name;

                //挂号级别
                this.lblRegLevel.Text = register.DoctorInfo.Templet.RegLevel.Name;

                //姓名
                this.lblPatientName.Text = register.Name;

                //挂号员号
                this.lblRegOper.Text = register.InputOper.ID;

                //挂号日期
                this.lblRegDate.Text = register.DoctorInfo.SeeDate.ToString();

                //合同单位类别
                this.lblPactName.Text = register.Pact.Name;

                #region 费用赋值

                if (register.LstCardFee != null && register.LstCardFee.Count > 0)
                {
                    #region 挂号记录和费用分离

                    decimal regFee = 0m;
                    decimal chkFee = 0m;
                    decimal cardFee = 0m;
                    decimal caseFee = 0m;

                    foreach (FS.HISFC.Models.Account.AccountCardFee accFee in register.LstCardFee)
                    {
                        if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.RegFee)
                        {
                            //挂号费
                            regFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.CaseFee)
                        {
                            //病历本费
                            caseFee += accFee.Tot_cost;
                        }
                        else if(accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.CardFee)
                        {
                            //卡工本费
                            cardFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.ChkFee)
                        {
                            //诊金费
                            chkFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.DiaFee)
                        {
                            //诊金费
                            chkFee += accFee.Tot_cost;
                        }
                    }

                    //佛三本地化处理打印内容
                    if (regFee < 30 || register.Pact.Name.Contains("职工"))
                    {
                        //挂号费
                        this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regFee, 2) + "元";

                        //诊察费
                        this.lblChkFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(chkFee, 2) + "元";
                    }
                    //如果合同单位是老年说明挂号费里面全部是诊金
                    else if (register.Pact.Name.Contains("老年"))
                    {
                        //挂号费
                        this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(0, 2) + "元";

                        //诊察费
                        this.lblChkFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regFee, 2) + "元";
                    }
                    else
                    {
                        //挂号费
                        this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(1, 2) + "元";

                        //诊察费
                        this.lblChkFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regFee-1, 2) + "元";
                    }

                    if (register.DoctorInfo.Templet.RegLevel.ID == "5")
                    {
                        //诊察费
                        this.lblChkFee.Text = this.lblChkFee.Text+"(首诊)";
                    }
                    else if (register.DoctorInfo.Templet.RegLevel.ID == "6"||register.DoctorInfo.Templet.RegLevel.ID == "7"||register.DoctorInfo.Templet.RegLevel.ID == "8")
                    {
                        //诊察费
                        this.lblChkFee.Text = this.lblChkFee.Text + "(心理咨询)";
                    }
                    else if (register.DoctorInfo.Templet.RegLevel.ID == "9" || register.DoctorInfo.Templet.RegLevel.ID == "10" || register.DoctorInfo.Templet.RegLevel.ID == "11")
                    {
                        //诊察费
                        this.lblChkFee.Text = this.lblChkFee.Text + "(心理治疗)";
                    }

                    //病历本费
                    if (caseFee > 0)
                    {
                        this.lblCaseBook.Text = "病历本费";
                        this.lblCaseBookCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(caseFee, 2) + "元";
                    }

                    // 卡工本费
                    if (cardFee > 0)
                    {
                        this.lblCard.Text = "卡工本费";
                        this.lblCardFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(cardFee, 2) + "元";
                    }

                    //合计
                    this.lblTotCost.Text = "合计：" + (regFee + chkFee + caseFee + cardFee).ToString("F2") + " 元";

                    #endregion 

                }
                else
                {
                    #region 费用还是保持在fin_opr_register(旧)

                    //挂号费
                    this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                        register.RegLvlFee.RegFee, 2) +
                        "元";
                    //诊察费
                    this.lblChkFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                        register.RegLvlFee.ChkFee + register.RegLvlFee.PubDigFee + register.RegLvlFee.OwnDigFee, 2) +
                        "元";


                    //病历手册
                    if (register.RegLvlFee.OthFee > 0)
                    {
                        this.lblCaseBook.Text = "病历本费";
                        this.lblCaseBookCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                        register.RegLvlFee.OthFee, 2) +
                        "元";
                    }

                    // 卡工本费
                    decimal decCardFee = 0;
                    if (register.User01 == "CARDFEE")
                    {
                        decCardFee = FS.FrameWork.Function.NConvert.ToDecimal(register.User02);
                    }

                    if (decCardFee > 0)
                    {
                        this.lblCard.Text = "卡工本费";
                        this.lblCardFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(decCardFee, 2) + "元";
                    }

                    #endregion
                }
                

                #endregion


                FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.Models.Base.Employee doct = personMgr.GetPersonByID(register.DoctorInfo.Templet.Doct.ID);
                if (doct != null)
                {
                    this.lblDoctor.Text = doct.Name + " " + doct.ID;
                }
                else
                {
                    this.lblDoctor.Text = "";
                }
                
                
                //控制根据打印和预览显示选项
                if (IsPreview)
                {
                    SetToPreviewMode();
                }
                else
                {
                    SetToPrintMode();
                }

            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        public IDbTransaction Trans
        {
            get
            {
                return this.trans.Trans;
            }
            set
            {
                this.trans.Trans = value;
            }
        }

        #endregion

        #region 打印方法

        /// <summary>
        /// 初始化收据
        /// </summary>
        /// <remarks>
        /// 把打印项和预览项根据tag标签的值区分开
        /// </remarks>
        private void InitReceipt(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            if (!string.IsNullOrEmpty(l.Text) || l.Text == "印")
                            {
                                l.Text = "";
                            }
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        }


        /// <summary>
        /// 初始化收据
        /// </summary>
        /// <remarks>
        /// 把打印项和预览项根据tag标签的值区分开
        /// </remarks>
        private void InitReceipt()
        {
            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            foreach (Control c in this.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            #region 将代印字的打印项值清空
                            if (!string.IsNullOrEmpty(l.Text) && l.Text == "印")
                            {
                                l.Text = "";
                            }
                            #endregion
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        }

        /// <summary>
        /// 设置标签集合的可见性
        /// </summary>
        /// <param name="v">是否可见</param>
        /// <param name="l">标签集合</param>
        private void SetLableVisible(bool v, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                lbl.Visible = v;
            }
        }


        /// <summary>
        /// 设置打印集合的值
        /// </summary>
        /// <param name="t">值数组</param>
        /// <param name="l">标签集合</param>
        private void SetLableText(string[] t, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                lbl.Text = "";
            }
            if (t != null)
            {
                if (t.Length <= l.Count)
                {
                    int i = 0;
                    foreach (string s in t)
                    {
                        l[i].Text = s;
                        i++;
                    }
                }
                else
                {
                    if (t.Length > l.Count)
                    {
                        int i = 0;
                        foreach (Label lbl in l)
                        {
                            lbl.Text = t[i];
                            i++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置为打印模式
        /// </summary>
        public void SetToPrintMode()
        {
            //将预览项设为不可见
            SetLableVisible(false, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }

        /// <summary>
        /// 设置为预览模式
        /// </summary>
        public void SetToPreviewMode()
        {
            //将预览项设为可见
            SetLableVisible(true, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }

        #endregion
    }
}
