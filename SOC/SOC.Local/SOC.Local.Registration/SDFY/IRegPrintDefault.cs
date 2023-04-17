using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;
using System.Collections;

namespace FS.SOC.Local.Registration.SDFY
{
    public partial class IRegPrintDefault : UserControl, FS.HISFC.BizProcess.Interface.Registration.IRegPrint
    {
        public IRegPrintDefault()
        {
            InitializeComponent();
        }

        #region 域变量
        /// <summary>
        /// 全局事务
        /// </summary>
        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();
        /// <summary>
        /// 管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 挂号业务类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        #endregion

        #region 属性

        private bool isPreview = false;
        /// <summary>
        /// 是否预览
        /// </summary>
        private bool IsPreview
        {
            get { return isPreview; }
            set { isPreview = value; }
        }

        /// <summary>
        /// 事务
        /// </summary>
        public System.Data.IDbTransaction Trans
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

        #region 方法
        /// <summary>
        /// 设置打印值
        /// </summary>
        /// <param name="register">挂号实体</param>
        /// <returns></returns>
        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {
            this.lblHosptialName.Text = manageIntegrate.GetHospitalName();
            //MessageBox.Show("请记录门诊号："+register.PID.CardNO);
            try
            {
                //门诊号
                this.npbBarCode.Image = this.CreateBarCode(register.PID.CardNO);
                //姓名
                this.lblPatientName.Text = "姓名：" + register.Name;
                //挂号科室
                this.lblDeptName.Text = "挂号科室：" + register.DoctorInfo.Templet.Dept.Name;
                //挂号医生
                this.lblDoctName.Text = "挂号医生：" + register.DoctorInfo.Templet.Doct.Name;
                //性别
                this.lblSex.Text = "性别：" + register.Sex.Name;
                //年龄
                this.lblAge.Text = "年龄：" + this.regMgr.GetAge(register.Birthday);
                //身份证号码
                this.lblIDNo.Text = "身份证号：" + register.IDCard;
                //结算种类
                this.lblPactName.Text = "结算种类：" + register.Pact.Name;
                //序号
                this.lblSeeNo.Text = "排队号码：" + register.DoctorInfo.SeeNO;
                //挂号时间
                this.lblRegDate.Text = "挂号时间：" + this.regMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                return -1;
            }
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
                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("MZGH", 300, 300);
                print.SetPageSize(ps);
                print.PrintPage(0, 0, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            return 0;
        }

        /// <summary>
        /// 设置事务
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <returns></returns>
        public int PrintView()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this);
            return 0;
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

        #region 报表打印用函数（作废）
        ///// <summary>
        ///// 设置为打印模式
        ///// </summary>
        //public void SetToPrintMode()
        //{
        //    //将预览项设为不可见
        //    SetLableVisible(false, lblPreview);
        //    foreach (Label lbl in lblPrint)
        //    {
        //        lbl.BorderStyle = BorderStyle.None;
        //        lbl.BackColor = SystemColors.ControlLightLight;
        //    }
        //}
        ///// <summary>
        ///// 设置为预览模式
        ///// </summary>
        //public void SetToPreviewMode()
        //{
        //    //将预览项设为可见
        //    SetLableVisible(true, lblPreview);
        //    foreach (Label lbl in lblPrint)
        //    {
        //        lbl.BorderStyle = BorderStyle.None;
        //        lbl.BackColor = SystemColors.ControlLightLight;
        //    }
        //}

        ///// <summary>
        ///// 打印用的标签集合
        ///// </summary>
        //public Collection<Label> lblPrint;
        ///// <summary>
        ///// 预览用的标签集合
        ///// </summary>
        //public Collection<Label> lblPreview;

        ///// <summary>
        ///// 初始化收据
        ///// </summary>
        ///// <remarks>
        ///// 把打印项和预览项根据ｔａｇ标签的值区分开
        ///// </remarks>
        //private void InitReceipt(Control control)
        //{
        //    foreach (Control c in control.Controls)
        //    {
        //        if (c.GetType().FullName == "System.Windows.Forms.Label" ||
        //            c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
        //        {
        //            Label l = (Label)c;
        //            if (l.Tag != null)
        //            {
        //                if (l.Tag.ToString() == "print")
        //                {
        //                    if (!string.IsNullOrEmpty(l.Text) || l.Text == "印")
        //                    {
        //                        l.Text = "";
        //                    }
        //                    lblPrint.Add(l);
        //                }
        //                else
        //                {
        //                    lblPreview.Add(l);
        //                }
        //            }
        //            else
        //            {
        //                lblPreview.Add(l);
        //            }
        //        }
        //    }
        //}


        ///// <summary>
        ///// 初始化收据
        ///// </summary>
        ///// <remarks>
        ///// 把打印项和预览项根据ｔａｇ标签的值区分开
        ///// </remarks>
        //private void InitReceipt()
        //{
        //    lblPreview = new Collection<Label>();
        //    lblPrint = new Collection<Label>();
        //    foreach (Control c in this.Controls)
        //    {
        //        if (c.GetType().FullName == "System.Windows.Forms.Label" ||
        //            c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
        //        {
        //            Label l = (Label)c;
        //            if (l.Tag != null)
        //            {
        //                if (l.Tag.ToString() == "print")
        //                {
        //                    #region 将代印字的打印项值清空
        //                    if (!string.IsNullOrEmpty(l.Text) && l.Text == "印")
        //                    {
        //                        l.Text = "";
        //                    }
        //                    #endregion
        //                    lblPrint.Add(l);
        //                }
        //                else
        //                {
        //                    lblPreview.Add(l);
        //                }
        //            }
        //            else
        //            {
        //                lblPreview.Add(l);
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// 设置标签集合的可见性
        ///// </summary>
        ///// <param name="v">是否可见</param>
        ///// <param name="l">标签集合</param>
        //private void SetLableVisible(bool v, Collection<Label> l)
        //{
        //    foreach (Label lbl in l)
        //    {
        //        lbl.Visible = v;
        //    }
        //}


        ///// <summary>
        ///// 设置打印集合的值
        ///// </summary>
        ///// <param name="t">值数组</param>
        ///// <param name="l">标签集合</param>
        //private void SetLableText(string[] t, Collection<Label> l)
        //{
        //    foreach (Label lbl in l)
        //    {
        //        lbl.Text = "";
        //    }
        //    if (t != null)
        //    {
        //        if (t.Length <= l.Count)
        //        {
        //            int i = 0;
        //            foreach (string s in t)
        //            {
        //                l[i].Text = s;
        //                i++;
        //            }
        //        }
        //        else
        //        {
        //            if (t.Length > l.Count)
        //            {
        //                int i = 0;
        //                foreach (Label lbl in l)
        //                {
        //                    lbl.Text = t[i];
        //                    i++;
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion

        #endregion
    }
}
