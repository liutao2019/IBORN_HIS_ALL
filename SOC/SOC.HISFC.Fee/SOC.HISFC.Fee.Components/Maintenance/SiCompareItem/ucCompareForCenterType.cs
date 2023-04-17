using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem
{
    /// <summary>
    /// 针对整个医保类型对照
    /// </summary>
    public partial class ucCompareForCenterType : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public delegate void SaveHandle();
        public event SaveHandle saveEvent;
        public ucCompareForCenterType()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        { 
            this.btnSave.Click +=new EventHandler(btnSave_Click);
            base.OnLoad(e);
            this.neuCenterCode.Focus();
        }
        private Hashtable listPact = null;
        public Hashtable ListPact
        {
            get { return listPact; }
            set { listPact = value; }
        }
 
        public void SetCenterType(string strType)
        {
            this.txtCenterType.Text = strType;
        }
        public void SetLocalItem(FS.SOC.HISFC.Fee.Models.CompareItemModel item)
        {
            if (item.User01 == "1")
            {
                this.neuItemCode.Tag = item;
                this.neuItemCode.Text = item.HisCode;
                this.neuItemName.Text = item.HisName;
                this.neuUseCode.Text = item.UserCode;
                this.neuCenterCode.Text = item.CenterCode;
                this.neuCenerName.Text = item.CenterName;
                this.cmbGrade.Text = string.Empty;
                this.neuMemo.Text = item.User02;
                this.neuPayRate.Text = item.User03;
            }
            else 
            {
                this.neuCenterCode.Tag = item;
                this.neuCenterCode.Text = item.HisCode;
                this.neuCenerName.Text = item.HisName;
                //this.neuUseCode.Text = item.UserCode;
                this.neuCenterCode.Text = item.CenterCode;
                this.neuCenerName.Text = item.CenterName;
                this.cmbGrade.Text = item.User01;
                this.neuMemo.Text = item.User02;
                this.neuPayRate.Text = item.User03;
                
            }
        }
        public void SetModifyLabel()
        {
            string strModify = "";
            foreach (string str in listPact.Keys )
            {
                strModify += listPact[str] + ",";
            }
            strModify =strModify.Trim(',');
            lbModifyInfo.Text = "正在维护：" + strModify;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listPact ==null ||listPact.Count==0 )
            {
                MessageBox.Show("请选择要对照维护的合同单位");
                return;               
            }

            decimal payRate = 0;
            try
            {
                payRate = FS.FrameWork.Function.NConvert.ToDecimal(this.neuPayRate.Text.Trim());


            }
            catch
            {
                MessageBox.Show("自付比例请输入数字！");
                return;

            }
            FS.SOC.HISFC.Fee.BizLogic.SiCompareItem compareItemMgr = new FS.SOC.HISFC.Fee.BizLogic.SiCompareItem();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            compareItemMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.SOC.HISFC.Fee.Models.CompareItemModel item = (FS.SOC.HISFC.Fee.Models.CompareItemModel)this.neuItemCode.Tag;
            if (this.neuCenterCode.Tag != null)
            {
              //  FS.SOC.HISFC.Fee.Models.CompareItemModel centrlMode=(FS.SOC.HISFC.Fee.Models.CompareItemModel)(this.neuCenterCode.Tag);
             
            }
            //item.CenterItemType = ((FS.SOC.HISFC.Fee.Models.CompareItemModel)this.neuCenterCode.Tag ).CenterItemType;
            item.CenterCode = this.neuCenterCode.Text.Trim();
            item.CenterName = this.neuCenerName.Text.Trim();
            item.HisUserCode = this.neuUseCode.Text.Trim();
            string grade="";
            if(cmbGrade.SelectedIndex!=0 && cmbGrade.Text!="" && cmbGrade.Text !="未知")
            {
                grade=cmbGrade.SelectedIndex.ToString();
            }

            item.User01 = grade;//医保等级
            item.User02 = this.neuMemo.Text.Trim();//备注
            item.User03=  payRate.ToString();
            if (string.IsNullOrEmpty(item.CenterCode))
            {
                MessageBox.Show("中心编码为空，不能对照！");
                return ;
            }

            foreach(string str in listPact.Keys)
            {
                item.PactCode =str;

                int result = compareItemMgr.Update(item);
                if (result<=0)
                {
                    result = compareItemMgr.Insert(item);
                }
                if (result ==-1)
                {
                    MessageBox.Show(compareItemMgr.Err);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }

            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("对照成功！");

            this.neuCenterCode.Text = "";
            this.neuCenerName.Text = "";
            this.cmbGrade.Text = "";
            this.neuPayRate.Text = "0.00";
            this.neuMemo.Text = "";

            if (saveEvent!=null)
            {
                saveEvent();
            }

        }

        private void neuCenterCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.neuCenterCode.Text.Trim()))
                {
                    this.neuCenerName.Focus();
                }
            }
        }

        private void neuCenerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.neuCenerName.Text.Trim()))
                {
                    this.cmbGrade.Focus();
                }
            }
        }

        private void cmbGrade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.cmbGrade.Text.Trim()))
                {
                    this.neuPayRate.Focus();
                }
            }
        }

        private void neuMemo_KeyDown(object sender, KeyEventArgs e)
        {
            this.btnSave.Focus();
        }

        private void neuPayRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.neuPayRate.Text.Trim()))
                {
                    this.neuMemo.Focus();
                }
            }
        }

    }
}
