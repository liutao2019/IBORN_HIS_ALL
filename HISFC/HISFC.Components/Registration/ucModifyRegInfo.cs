using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.HISFC.Components.Registration
{
    public partial class ucModifyRegInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 域
        //{971E891B-4E05-42c9-8C7A-98E13996AA17}
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();
        #endregion

        /// <summary>
        /// 是否同时更新挂号信息
        /// </summary>
        private bool updateRegInfo = false;

        /// <summary>
        /// 是否同时更新挂号信息
        /// </summary>
        [Description("是否同时更新最近一次挂号信息"), Category("设置"), Browsable(true)]
        public bool UpdateRegInfo
        {
            get { return this.updateRegInfo; }
            set 
            {
                this.updateRegInfo = value;
                this.ucRegPatientInfo1.UpdateRegInfo = value;
            }
        }

        public ucModifyRegInfo()
        {
            InitializeComponent();
        }
        private int save()
        {
            this.ucRegPatientInfo1.save();
            this.tbCardNO.Text = "";
            return 1;
        }
       
        private void tbCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) {
                return; 
            }
            string cardnum = this.tbCardNO.Text.Trim();//就诊卡号
            if (cardnum == null||cardnum=="") {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("就诊卡号不能为空"), "提示");
                this.tbCardNO.Focus();
                return;
            }
            string cardno = "";
            cardnum=cardnum.PadLeft(10,'0');

            bool flag=account.GetCardNoByMarkNo(cardnum, ref cardno);
            if (flag)
            {
                this.tbCardNO.Text = cardnum;
                this.txtIDNO.Text = "";
                this.ucRegPatientInfo1.CardNO = cardno;
            }
            else {
                FS.HISFC.Models.RADT.PatientInfo tempInfo = radtIntegrate.QueryComPatientInfo(cardnum);

                if (tempInfo == null || tempInfo.PID.CardNO == null || tempInfo.PID.CardNO.Length <= 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的就诊卡号不存在"), "提示");
                    return;
                }
                else
                {
                    this.tbCardNO.Text = cardnum;
                    this.txtIDNO.Text = "";
                    this.ucRegPatientInfo1.CardNO = cardnum;
                }
            }

            this.ucRegPatientInfo1.Focus();
        }
 

        protected override int OnSave(object sender, object neuObject)
        {
            this.save();

            return base.OnSave(sender, neuObject);
        }

        
        //{971E891B-4E05-42c9-8C7A-98E13996AA17}
        private void txtIDNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string idNO = this.txtIDNO.Text.Trim();
                string IdMessage = string.Empty;
                int returnValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref IdMessage);
                if (returnValue < 0)
                {
                    MessageBox.Show(IdMessage);
                    return;
                }

                FS.HISFC.Models.RADT.PatientInfo p = this.radtIntegrate.QueryComPatientInfoByIDNO(idNO);

                if (p == null)
                {
                    MessageBox.Show("根据身份证查询患者信息出错" + this.radtIntegrate.Err);
                    return;
                }
                this.tbCardNO.Text = p.PID.CardNO;
                if (!string.IsNullOrEmpty(p.PID.CardNO))
                {
                    this.ucRegPatientInfo1.CardNO = p.PID.CardNO;
                }
            }
        }

        /// <summary>
        /// 打印{D2F77BDA-F5E5-48fe-AB73-B7FE6D92E6E2}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            this.ucRegPatientInfo1.PrintBar();
            return base.OnPrint(sender, neuObject);
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            toolBarService.AddToolButton("刷卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "刷卡":
                    {
                        string cardNo = "";
                        string error = "";
                        if (Function.OperCard(ref cardNo, ref error) == -1)
                        {
                            CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                            return;
                        }

                        tbCardNO.Text = cardNo;
                        tbCardNO.Focus();
                        this.tbCardNO_KeyDown(null, new KeyEventArgs(Keys.Enter));

                        break;
                    }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
       

    }
}
