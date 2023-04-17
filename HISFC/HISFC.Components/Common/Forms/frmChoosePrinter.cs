using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Forms
{
    /// <summary>
    /// 选择打印机
    /// </summary>
    public partial class frmChoosePrinter : FS.FrameWork.WinForms.Forms.BaseForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmChoosePrinter()
        {
            InitializeComponent();
        }

        #region 变量和属性

        /// <summary>
        /// 打印机名称
        /// </summary>
        private string printerName = string.Empty;

        /// <summary>
        /// 打印机名称
        /// </summary>
        public string PrinterName
        {
            get
            {
                printerName = this.cmbPrinterList.Tag.ToString();
                return printerName;
            }
            set
            {
                printerName = value;
            }
        }

        #endregion

        #region 方法和事件


        /// <summary>
        /// 初始化打印机
        /// </summary>
        public void Init()
        {
            System.Drawing.Printing.PrintDocument print = new System.Drawing.Printing.PrintDocument();
            string pDefault = print.PrinterSettings.PrinterName;//默认打印机名

            System.Collections.ArrayList alPrint = new System.Collections.ArrayList();
            foreach (string pName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = pName;
                obj.Name = pName;
                obj.Memo = pName;
                alPrint.Add(obj);
            }
            this.cmbPrinterList.AddItems(alPrint);
            this.cmbPrinterList.Tag = pDefault;
            
        }
        
        /// <summary>
        /// 选择打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOK_Click(object sender, EventArgs e)
        {
            //{3E7EFECA-5375-420b-A435-323463A0E56C}
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion
    }
}
