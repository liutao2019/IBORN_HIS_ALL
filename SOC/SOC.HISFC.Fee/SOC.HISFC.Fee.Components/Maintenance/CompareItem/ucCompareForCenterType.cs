using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.CompareItem
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
            }
            else 
            {
                this.neuCenterCode.Tag = item;
                this.neuCenterCode.Text = item.HisCode;
                this.neuCenerName.Text = item.HisName;
                
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
            FS.SOC.HISFC.Fee.BizLogic.CompareItem compareItemMgr = new FS.SOC.HISFC.Fee.BizLogic.CompareItem();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            compareItemMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.SOC.HISFC.Fee.Models.CompareItemModel item = (FS.SOC.HISFC.Fee.Models.CompareItemModel)this.neuItemCode.Tag;
            item.CenterItemType = ((FS.SOC.HISFC.Fee.Models.CompareItemModel)this.neuCenterCode.Tag ).CenterItemType;
            item.CenterCode = this.neuCenterCode.Text.Trim();
            item.CenterName = this.neuCenerName.Text.Trim();

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

            if (saveEvent!=null)
            {
                saveEvent();
            }

        }

    }
}
