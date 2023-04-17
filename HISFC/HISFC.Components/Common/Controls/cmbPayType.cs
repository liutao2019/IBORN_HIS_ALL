using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
namespace FS.HISFC.Components.Common.Controls
{
    public partial class cmbPayType : FS.FrameWork.WinForms.Controls.NeuComboBox
    {
         public cmbPayType()
        {
            InitializeComponent();
        }

        public cmbPayType(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.initControl();
            this.SelectedIndexChanged+=new EventHandler(cmbPayType_SelectedIndexChanged);
        }

        #region "变量"
        /// <summary>
        /// 是否弹出
        /// </summary>
        private bool bPop = true;

        /// <summary>
        /// 工作单位
        /// </summary>
        private string workUnit = "";
        #endregion

        #region "实体变量"

        /// <summary>
        /// 银行实体
        /// </summary>
        public FS.HISFC.Models.Base.Bank bank = new FS.HISFC.Models.Base.Bank();

        #endregion

        #region"属性"
        /// <summary>
        /// 弹出属性

        /// </summary>
        public bool Pop
        {
            get
            {
                return this.bPop;
            }
            set
            {
                this.bPop = value;
            }
        }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string WorkUnit
        {
            get
            {
                return this.workUnit;
            }
            set
            {
                this.workUnit = value;
            }
        }
        #endregion 

        #region 方法
        /// <summary>
        /// 初始化控件

        /// </summary>
        private void initControl()
        {
            this.Items.Clear();


            //住院显示部分支付方式。


            try
            {
                //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                //this.AddItems(FS.HISFC.Models.Fee.EnumPayTypeService.List());
                this.AddItems(managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES));
            }
            catch (Exception ex)
            {
                MessageBox.Show("initControl" + ex.Message);
            }
        }

        /// <summary>
        /// 显示bank控件
        /// </summary>
        private void ShowBank()
        {

            FS.FrameWork.WinForms.Forms.BaseForm f;
            f = new FS.FrameWork.WinForms.Forms.BaseForm();

            ucBank Bank = new ucBank();

            Bank.Dock = System.Windows.Forms.DockStyle.Fill;
            f.Controls.Add(Bank);

            Bank.Bank = this.bank;
            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            f.Size = new System.Drawing.Size(295, 240);
            f.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            f.Text = "选择银行";
            f.ShowDialog();
        }

        /// <summary>
        /// 获取支付方式名称
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetNameByID(string ID)
        {
            foreach(FS.HISFC.Models.Base.Const con in this.alItems)
            {
                if(con.ID==ID)
                {
                    return con.Name;
                }
            }
            return "";
        }
        #endregion

        #region 事件
        private void cmbPayType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (this.bPop == false) return;
                if(this.Tag == null || this.Tag.ToString()==string.Empty) return;
                this.bank = new FS.HISFC.Models.Base.Bank();
                //FS.HISFC.Models.Fee.EnumPayType payType =
                //    (FS.HISFC.Models.Fee.EnumPayType)Enum.Parse(typeof(FS.HISFC.Models.Fee.EnumPayType), this.Tag.ToString());
                //switch (payType)
                //{
                //    //借记卡
                //    case FS.HISFC.Models.Fee.EnumPayType.DB:

                //        break;
                //    //支票
                //    case FS.HISFC.Models.Fee.EnumPayType.CH:
                //        this.ShowBank();
                //        break;
                //    //信用卡

                //    case FS.HISFC.Models.Fee.EnumPayType.CD:

                //        break;
                //    //汇票
                //    case FS.HISFC.Models.Fee.EnumPayType.PO:
                //        this.ShowBank();
                //        break;

                //    default:
                //        break;
                //}
                FS.FrameWork.Models.NeuObject payType = this.SelectedItem as FS.FrameWork.Models.NeuObject;
                switch (payType.ID)
                {
                    //借记卡
                    case "DB":

                        break;
                    //支票
                    case "CH":
                        this.ShowBank();
                        break;
                    //信用卡

                    case "CD":

                        break;
                    //汇票
                    case "PO":
                        this.ShowBank();
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("cmbPayType_SelectedIndexChanged" + ex.Message);
                return;
            }
        }
        #endregion
    }
}
