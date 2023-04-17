using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    public partial class ucPatientMedicalInsurance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public delegate int DelegateVoidSet();
        public delegate int DelegateHashtableSet(Hashtable hsTable);
        public delegate int DelegateArrayListSet(ArrayList al);
        public delegate int DelegateStringSet(string str);
        public delegate int DelegateTripleStringSet(string str, string str1, string str2);
        public delegate int DelegateBoolPointSet(bool flag, Point point);
        public delegate int DelegateKeysSet(Keys keyData);
        public delegate int DelegateBoolSet(bool bo);
        public delegate ArrayList DelegateArrayListGetString(string str);
        public delegate bool DelegateBoolGet();
        public delegate Hashtable DelegateHashtableGet();
        public delegate ArrayList DelegatArraListeGet();
        public delegate string DelegateStringGet();


        /// <summary>
        /// 门诊卡号
        /// </summary>
        HISFC.Models.Account.AccountCard accountCard = null;


        /// <summary>
        /// 卡操作实体
        /// </summary>
        private FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();


        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 患者信息控件
        /// </summary>
        private ucPatientPayItems ucpatientpayItems = null;

        public ucPatientMedicalInsurance()
        {
            InitializeComponent();
            init();
        }


        private void init()
        {
            ///项目包
            if (this.ucpatientpayItems == null)
            {
                this.ucpatientpayItems = new ucPatientPayItems();
                this.ucpatientpayItems.Dock = DockStyle.Fill;
                this.plFeeInfo.Height = ucpatientpayItems.Height;
                this.plFeeInfo.Controls.Add(this.ucpatientpayItems);
            }

            this.ucItemMedicalSelector2.Init();
            this.initControlsEvents();

        }


        /// <summary>
        /// 初始化控件委托事件
        /// </summary>
        private void initControlsEvents()
        {

            this.ucpatientpayItems.SetSelectorPosition += new DelegateBoolPointSet(ucpatientpayItems_SetSelectorPosition);

            this.ucpatientpayItems.SetSelectorFliter += new DelegateTripleStringSet(ucpatientpayItems_SetSelectorFliter);

            this.ucItemMedicalSelector2.RtnSelectedItem += new DelegateRtnSelectedItem(ucpatientpayItems_RtnSelectedItem);

        }


        protected FS.FrameWork.WinForms.Forms.ToolBarService _toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            _toolBarService.AddToolButton("确认", "确认", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q确认收费, true, false, null);
            _toolBarService.AddToolButton("清屏", "清屏", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return _toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "确认":
                    this.SaveMediacl();
                    break;

                case "清屏":
                    this.Clear();
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// 确认
        /// </summary>
        private void SaveMediacl()
        {

            if (accountCard == null || accountCard.Patient == null)
            {
                MessageBox.Show("请选择一个客户！！！");
                return;
            }

            if (this.txtNum.Text == "" || this.txtMony.Text == "")
            {
                MessageBox.Show("定点次数/本次定点金额不可为空！！！");
                return;
            }

            accountCard.Patient.ExtendFlag1 = this.txtMony.Text;
            accountCard.Patient.ExtendFlag2 = this.txtNum.Text;

            List<ItemMedicalDetail> details = this.ucpatientpayItems.GetPackageSelected();

            if (details == null || details.Count == 0)
            {
                MessageBox.Show("请选择套餐包！！！");
                return;
            }

            accountCard.Patient.Memo = this.txtmemo.Text;


            List<ExpItemMedical> explist = accountManager.QueryExpItemMedicalByCardNo(accountCard.Patient.PID.CardNO);

            if (explist != null && explist.Count > 0)
            {
                string packagename = explist[0].PackageName;
                MessageBox.Show("存在有效【" + packagename + "】套包请先确认再操作！！！");
                return;
            }


            int i = accountManager.InsertExpItemMedical(accountCard.Patient, details);

            if (i > 0)
            {
                MessageBox.Show("保存成功！！！");
                Clear();
            }
            else
            {
                MessageBox.Show("保存失败！！！" + accountManager.Err);
            }

        }


        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="isShow"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private int ucpatientpayItems_SetSelectorPosition(bool isShow, Point location)
        {
            this.ucItemMedicalSelector2.Visible = isShow;
            if (isShow)
            {
                this.ucItemMedicalSelector2.Location = this.PointToClient(new Point(location.X + 5, location.Y + 15));
            }
            return 1;
        }

        private int ucpatientpayItems_SetSelectorFliter(string str, string strclass, string parent)
        {
            this.ucItemMedicalSelector2.Filter(str, strclass, parent);
            return 1;
        }


        private void ucpatientpayItems_RtnSelectedItem(ItemMedical package)
        {
            if (package != null)
            {
                this.txtMony.Text = package.PackageCost.ToString();
                this.ucpatientpayItems.SetPackageInfo(package);
                this.ucpatientpayItems.SetChildPackage(accountManager.QueryItemMedicalDetailById(package.PackageId));
                this.ucpatientpayItems_SetSelectorPosition(false, new Point());
                this.ucpatientpayItems.Focus();

            }
        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// <param name="CardNo"></param>
        private void ShowPatienInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.txtName.Text = patient.Name;
            this.txtSex.Text = patient.Sex.Name;
            this.txtAge.Text = accountManager.GetAge(patient.Birthday);
            this.txtIdCardNO.Text = patient.IDCard;
            txtMarkNo.Text = patient.PID.CardNO;
            this.cmbIdCardType.Tag = patient.IDCardType.ID;
            //FS.FrameWork.Models.NeuObject tempObj = null;
            //tempObj = managerIntegrate.GetConstant(HISFC.Models.Base.EnumConstant.NATION.ToString(), patient.Nationality.ID);
            //if (tempObj != null)
            //{
            //    this.txtNation.Text = tempObj.Name;
            //}
            //tempObj = managerIntegrate.GetConstant(HISFC.Models.Base.EnumConstant.COUNTRY.ToString(), patient.Country.ID);
            //if (tempObj != null)
            //{
            //    this.txtCountry.Text = tempObj.Name;
            //}
            //this.txtsiNo.Text = patient.SSN;
        }

        /// <summary>
        /// 输入就诊卡号获取账户信息
        /// </summary>
        private void GetAccountInfo()
        {
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            string markNO = this.txtMarkNo.Text.Trim();
            if (markNO == string.Empty)
            {
                MessageBox.Show("请输入就诊卡号！");
                this.txtMarkNo.Focus();
                return;
            }
            int resultValue = accountManager.GetCardByRule(markNO, ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(accountManager.Err);
                this.Clear();
                return;
            }

            this.txtMarkNo.Text = accountCard.MarkNO;

            this.ucpatientpayItems.Clear();
            //显示患者信息
            ShowPatienInfo(accountCard.Patient);
            //01 为身份证号，在常数维护中维护
            if (this.cmbIdCardType.Tag != null && this.cmbIdCardType.Tag.ToString() != string.Empty && this.txtIdCardNO.Text.Trim() != string.Empty)
            {
                this.txtIdCardNO.Enabled = false;
            }
            else
            {
                this.txtIdCardNO.Enabled = true;
                this.cmbIdCardType.Tag = "01";//身份证号
                this.txtIdCardNO.Focus();
            }

        }

        /// <summary>
        /// 回车处理
        /// </summary>
        protected virtual void ExecCmdKey()
        {
            if (this.txtMarkNo.Focused)
            {
                GetAccountInfo();
                return;
            }

        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.txtName.Focused)
                {
                    txtName_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
                ExecCmdKey();

                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.txtName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {

                    this.txtMarkNo.Text = frmQuery.PatientInfo.PID.CardNO;
                    this.txtMarkNo.Focus();
                    ExecCmdKey();
                }
            }
        }


        /// <summary>
        /// 清空显示数据
        /// </summary>
        private void Clear()
        {
            this.txtMarkNo.Text = string.Empty;
            this.txtMarkNo.Tag = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.txtIdCardNO.Text = string.Empty;
            this.txtNation.Text = string.Empty;
            this.txtNum.Text = string.Empty;
            this.txtMony.Text = string.Empty;
            this.txtmemo.Text = string.Empty;
            this.txtSex.Text = string.Empty;
            this.accountCard = null;
            this.txtMarkNo.Focus();
            this.ucpatientpayItems.Clear();
        }

    }
}
