using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Fin
{
    public partial class ucFinBill : UserControl
    {
        public ucFinBill()
        {
            InitializeComponent();
            this.lnbCloseFin.LinkClicked += new LinkLabelLinkClickedEventHandler(lnbCloseFin_LinkClicked);
            this.cbNeedInvoiceNO.CheckStateChanged += new EventHandler(cbNeedInvoiceNO_CheckStateChanged);
        }

        string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyFinSettring.xml";
     
        string stockDeptNO = "";

        public int InitFinBillNO(string stockDeptNO, string class2Code, string class3Code)
        {
            cbNeedInvoiceNO.Visible = class2Code == "0310";
            this.stockDeptNO = stockDeptNO;

            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();

            string ListNO = "";
            decimal iSequence = 0;

            //获取当前科室的单据最大流水号
            FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "PhaFinNO", stockDeptNO);
            if (deptExt != null)
            {
                if (deptExt.Item.ID == "")          //当前科室尚无记录 流水号置为1
                {
                    iSequence = 1;
                }
                else                                //当前科室存在记录 根据日期是否为当天 确定流水号是否归1
                {
                    iSequence = deptExt.NumberProperty;
                }
                //生成单据号
                ListNO = iSequence.ToString();
             
            }
            this.nlblFinNO.Text = ListNO;

            string invoiceNullable = "0";
            invoiceNullable = SOC.Public.XML.SettingFile.ReadSetting(fileName, "ApproveInput", "InvoiceNONullable", invoiceNullable);
            this.cbNeedInvoiceNO.Checked = !FS.FrameWork.Function.NConvert.ToBoolean(invoiceNullable);

            return 1;
        }

        public string GetFinBillNO(string stockDeptNO, string class2Code, string class3Code)
        {
            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();

            string ListNO = "";
            decimal iSequence = 0;

            //获取当前科室的单据最大流水号
            FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "PhaFinNO", stockDeptNO);
            if (deptExt == null)
            {
                return null;
            }
            else
            {
                if (deptExt.Item.ID == "")          //当前科室尚无记录 流水号置为1
                {
                    iSequence = 1;
                }
                else                                //当前科室存在记录 根据日期是否为当天 确定流水号是否归1
                {
                    iSequence = deptExt.NumberProperty + 1;
                }
                //生成单据号
                ListNO = iSequence.ToString();

                //保存当前最大流水号
                deptExt.Item.ID = stockDeptNO;
                deptExt.DateProperty = DateTime.Now;
                deptExt.NumberProperty = iSequence;
                deptExt.PropertyCode = "PhaFinNO";
                deptExt.PropertyName = "药品财务记账号";

                if (extentManager.SetComExtInfo(deptExt) == -1)
                {
                    Function.ShowMessage("操作失败，请向系统管理员报告错误：" + extentManager.Err, MessageBoxIcon.Error);
                    return null;
                }
            }
            this.nlblFinNO.Text = ListNO;
            return ListNO;
        }

        public bool InvoceNONullabe
        {
            get { return !this.cbNeedInvoiceNO.Checked; }
        }

        void lnbCloseFin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认清零吗？", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();

            string ListNO = "";
            decimal iSequence = 0;

            //获取当前科室的单据最大流水号
            FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "PhaFinNO", stockDeptNO);
            if (deptExt == null)
            {
                return;
            }
            else
            {
                iSequence = 1;
                //生成单据号
                ListNO = iSequence.ToString();

                //保存当前最大流水号
                deptExt.Item.ID = stockDeptNO;
                deptExt.DateProperty = DateTime.Now;
                deptExt.NumberProperty = iSequence;
                deptExt.PropertyCode = "PhaFinNO";
                deptExt.PropertyName = "药品财务记账号";

                if (extentManager.SetComExtInfo(deptExt) == -1)
                {
                    Function.ShowMessage("操作失败，请向系统管理员报告错误：" + extentManager.Err, MessageBoxIcon.Error);
                    return;
                }

                Function.ShowMessage("操作成功！", MessageBoxIcon.None);
            }

            this.nlblFinNO.Text = ListNO;
        }

        void cbNeedInvoiceNO_CheckStateChanged(object sender, EventArgs e)
        {
            SOC.Public.XML.SettingFile.SaveSetting(fileName, "ApproveInput", "InvoiceNONullable", this.cbNeedInvoiceNO.Checked ? "0" : "1");
        }

      
    }
}
