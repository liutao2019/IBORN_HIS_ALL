using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Input
{
    public partial class ucChangeCompany : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucChangeCompany()
        {
            InitializeComponent();
            this.lnbNOType.LinkClicked += new LinkLabelLinkClickedEventHandler(lnbNOType_LinkClicked);
            this.lnbCompany.LinkClicked += new LinkLabelLinkClickedEventHandler(lnbCompany_LinkClicked);
            this.txtNO.KeyPress += new KeyPressEventHandler(txtNO_KeyPress);
            this.nlbInfo.DoubleClick += new EventHandler(nlbInfo_DoubleClick);
        }

      

        /// <summary>
        /// 权限编码
        /// </summary>
        private string privePowerString = "0310";

        /// <summary>
        /// 权限编码
        /// </summary>
        [Description("权限编码"), Category("设置"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }


        /// <summary>
        /// 供货公司信息
        /// </summary>
        private ArrayList alCompany = new ArrayList();

        /// <summary>
        /// 供货公司帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper companyHerlper = null;

        /// <summary>
        /// 查询单据类别 0 发票号 1 入库单据号
        /// </summary>
        private string noType = "0";

        /// <summary>
        /// 新供货公司
        /// </summary>
        private FS.FrameWork.Models.NeuObject newCompany = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 当前登陆科室信息
        /// </summary>
        private FS.FrameWork.Models.NeuObject deptInfo = new FS.FrameWork.Models.NeuObject();

        #region 工具栏

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }
        #endregion

        private void Init()
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
            this.alCompany = phaConsManager.QueryCompany("1");
            if (this.alCompany == null)
            {
                MessageBox.Show("获取供货单位列表发生错误");
                return;
            }

            this.companyHerlper = new FS.FrameWork.Public.ObjectHelper(this.alCompany);

        }

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            if (string.IsNullOrEmpty(PrivePowerString))
            {
                PrivePowerString = "0310";
            }
            int param = Function.ChoosePriveDept(PrivePowerString, ref this.deptInfo);
            if (param == 0 || param == -1)
            {
                return -1;
            }


            this.nlbInfo.Text = "您选择的科室是【" + this.deptInfo.Name + "】";

            return 1;

        }

        private void Query()
        {
            ArrayList al = new ArrayList();


            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            if (this.noType == "0")
            {
                al = itemManager.QueryInputInfoByInvoice(this.deptInfo.ID, this.txtNO.Text, "0");
                if (al == null)
                {
                    Function.ShowMessage("根据发票号获取入库信息发生错误", MessageBoxIcon.Error);
                    return;
                }
                if (al.Count == 0)
                {
                    al = itemManager.QueryInputInfoByInvoice(this.deptInfo.ID, this.txtNO.Text, "1");
                    if (al == null)
                    {
                        Function.ShowMessage("根据发票号获取入库信息发生错误", MessageBoxIcon.Error);
                        return;
                    }
                }
                if (al.Count == 0)
                {
                    al = itemManager.QueryInputInfoByInvoice(this.deptInfo.ID, this.txtNO.Text, "2");
                    if (al == null)
                    {
                        Function.ShowMessage("根据发票号获取入库信息发生错误", MessageBoxIcon.Error);
                        return;
                    }
                    if (al.Count > 0)
                    {
                        Function.ShowMessage("单据已经核准，不可以更改", MessageBoxIcon.Information);
                        return;
                    }
                }
                if (al.Count == 0)
                {
                    Function.ShowMessage("发票号不正确，请确认录入正确", MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                al = itemManager.QueryInputInfoByListID(this.deptInfo.ID, this.txtNO.Text, "AAAA", "0");
                if (al == null)
                {
                    Function.ShowMessage("根据入库单号获取入库信息发生错误", MessageBoxIcon.Error);
                    return;
                }
                if (al.Count == 0)
                {
                    al = itemManager.QueryInputInfoByListID(this.deptInfo.ID, this.txtNO.Text, "AAAA", "1");
                    if (al == null)
                    {
                        Function.ShowMessage("根据入库单号获取入库信息发生错误", MessageBoxIcon.Error);
                        return;
                    }
                }
                if (al.Count == 0)
                {
                    al = itemManager.QueryInputInfoByListID(this.deptInfo.ID, this.txtNO.Text, "AAAA", "2");
                    if (al == null)
                    {
                        Function.ShowMessage("根据入库单号获取入库信息发生错误", MessageBoxIcon.Error);
                        return;
                    }
                    if (al.Count > 0)
                    {
                        Function.ShowMessage("单据已经核准，不可以更改", MessageBoxIcon.Information);
                        return;
                    }
                }
                if (al.Count == 0)
                {
                    Function.ShowMessage("入库单号不正确，请确认录入正确", MessageBoxIcon.Information);
                    return;
                }
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Pharmacy.Input info in al)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                this.neuSpread1_Sheet1.Cells[0, 0].Value = true;
                this.neuSpread1_Sheet1.Cells[0, 1].Text = info.InListNO;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.InvoiceNO;
                this.neuSpread1_Sheet1.Cells[0, 3].Text = this.companyHerlper.GetName(info.Company.ID);
                this.neuSpread1_Sheet1.Cells[0, 4].Text = info.Item.Name + "【" + info.Item.Specs + "】";
                this.neuSpread1_Sheet1.Cells[0, 5].Text = (info.Quantity / info.Item.PackQty).ToString("N");
                this.neuSpread1_Sheet1.Cells[0, 6].Text = info.Item.PackUnit;
                this.neuSpread1_Sheet1.Rows[0].Tag = info;
            }
        }

        private void Save()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {

                //if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value))
                {
                    FS.HISFC.Models.Pharmacy.Input input = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.Input;

                    if (itemManager.UpdateInputCompany(input.ID,input.SerialNO,this.newCompany.ID) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("更新供货公司信息失败，数据可能已经核准",MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("保存成功");
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.Init();
            }
            catch
            { }

            base.OnLoad(e);
        }

        private void lnbCompany_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            //操作员对窗口选择返回的信息
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alCompany, ref this.newCompany) == 0)
            {
                return;
            }
            else
            {
                this.lbCompany.Text = this.newCompany.Name;
            }
        }

        private void lnbNOType_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (this.noType == "0")
            {
                this.noType = "1";
                this.lnbNOType.Text = "单据号";
            }
            else
            {
                this.noType = "0";
                this.lnbNOType.Text = "发票号";
            }
            this.txtNO.Text = "";
        }

        void nlbInfo_DoubleClick(object sender, EventArgs e)
        {
            this.SetPriveDept();
        }

        void txtNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Query();
            }
        }

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }

            return this.SetPriveDept();
        }

        #endregion

    }
}
