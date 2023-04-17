﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Local.Printer.Package.IPackageInvoice
{
    public partial class ucPackageInvoiceBel : UserControl, FS.HISFC.BizProcess.Interface.MedicalPackage.IPackageInvoice
    {

        #region

        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// 发票管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice invoiceManager = new FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice();

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        private bool isPreview = false;

        private bool IsPreview
        {
            get { return isPreview; }
            set { isPreview = value; }
        }

        #endregion

        public ucPackageInvoiceBel()
        {
            InitializeComponent();
        }

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
                ps = psManager.GetPageSize("TCFP");

                if (ps == null )
                {
                    //|| string.IsNullOrEmpty(ps.Printer)
                    MessageBox.Show("没有找到打印机【TCFP】");
                    return -1;
                }

                //{13E56BF8-16D4-48b9-AD1F-E352C3DDFD73}
                string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
                //{3E7EFECA-5375-420b-A435-323463A0E56C}
                if (string.IsNullOrEmpty(printerName))
                {
                    return 1;
                }
                print.SetPageSize(ps);
                //if (ps != null && !string.IsNullOrEmpty(ps.Printer))
                //{
                //    print.PrintDocument.PrinterSettings.PrinterName = ps.Printer;
                //}
                print.PrintDocument.PrinterSettings.PrinterName = printerName;

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsCanCancel = false;
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(ps.Left, ps.Top, this);
                }
                else
                {
                    print.PrintPage(ps.Left, ps.Top, this);
                }

                //打印文档的左侧硬页边距的X坐标，这个硬边距和打印机有关，如果硬边距>0，可以设置打印控件的边距值为负数
                //print.PrintDocument.PrinterSettings.DefaultPageSettings.HardMarginX;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
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

        /// <summary>
        /// 设置打印值
        /// </summary>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int SetPrintValue(string InvoiceNO)
        {
            try
            {
                string strSQL = @"select wm_concat(pay_type)
                                    from ( select distinct (select y.name|| '  ' || to_char(f.tot_cost) 
                                                              from com_dictionary y
                                                             where y.type='PAYMODES'
                                                               and (y.code=f.mode_code or (f.mode_code  = 'DE' and y.code=f.realated_code ))) pay_type
                                             from exp_packagepaymode f
                                            where f.invoice_no='{0}'
                                                 and cancel_flag = '0')";

                strSQL = string.Format(strSQL, InvoiceNO);
                string strPayType = this.invoiceManager.ExecSqlReturnOne(strSQL);

                //缴费方式
                this.lblPayType.Text = strPayType;

                this.lbinvoiceNO.Text = InvoiceNO;

                strSQL = @"select sum(tot_cost)
                             from exp_packagepaymode
                            where cancel_flag = '0'
                              and invoice_no = '{0}'";
                strSQL = string.Format(strSQL, InvoiceNO);
                string strCost = this.invoiceManager.ExecSqlReturnOne(strSQL);

                this.lbCost.Text = strCost;


                strSQL = @" select wm_concat(packagename)
                                 from ( select (select package_name from bd_com_package a  where a.package_id = t.package_id ) packagename
                                          from exp_package t
                                         where t.invoice_no = '{0}'
                                           and t.cancel_flag = '0')";
                
                strSQL = string.Format(strSQL, InvoiceNO);
                
                string strNameList = this.invoiceManager.ExecSqlReturnOne(strSQL);
                this.lbPackageList.Text = strNameList;

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

        /// <summary>
        /// 设置为打印模式
        /// </summary>
        public void SetToPrintMode()
        {
            //将预览项设为不可见
            //SetLableVisible(false, lblPreview);
            //foreach (Label lbl in lblPrint)
            //{
            //    lbl.BorderStyle = BorderStyle.None;
            //    lbl.BackColor = SystemColors.ControlLightLight;
            //}
        }

        /// <summary>
        /// 设置为预览模式
        /// </summary>
        public void SetToPreviewMode()
        {
            //将预览项设为可见
            //SetLableVisible(true, lblPreview);
            //foreach (Label lbl in lblPrint)
            //{
            //    lbl.BorderStyle = BorderStyle.None;
            //    lbl.BackColor = SystemColors.ControlLightLight;
            //}
        }

    }
}
