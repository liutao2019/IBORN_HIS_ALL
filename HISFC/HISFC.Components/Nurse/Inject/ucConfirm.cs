﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Nurse.Inject
{
    /// <summary>
    /// [功能描述: 配药确认]<br></br>
    /// [创 建 者: 徐伟哲]<br></br>
    /// [创建时间: 2007-08-20]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary> 
    public partial class ucConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.Nurse.InjectManager.Decompound decompound = new FS.HISFC.BizLogic.Nurse.InjectManager.Decompound();
        private FS.HISFC.BizLogic.Nurse.InjectManager.InjectRecordMgr irManager = new FS.HISFC.BizLogic.Nurse.InjectManager.InjectRecordMgr();
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        public ucConfirm()
        {
            InitializeComponent();
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "确认":
                    this.ProceedConfirm();
                    break;
                default:
                    break;
            }
            //base.ToolStrip_ItemClicked(sender, e);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("确认", "执行确认", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z执行, true, false, null);
            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        private void ProceedConfirm()
        {
            List<FS.HISFC.Models.Nurse.InjectInfo> injectList = this.QueryConfirmItem();
            if (injectList == null || injectList.Count == 0)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t1 = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t1.BeginTransaction();

            this.decompound.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (FS.HISFC.Models.Nurse.InjectInfo inject in injectList)
            {
                if( this.decompound.UpdateInjectItemState(inject) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("确认失败"));
                    return;
                }
            }

            //FS.FrameWork.Management.Transaction t2 = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t2.BeginTransaction();
            this.irManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (this.irManager.InsertOperEnv(this.Get(injectList[0])) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("确认失败"));
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("确认成功"));

            this.ClearFarPoint();
            this.InitControl();

        }

        private FS.HISFC.Models.Nurse.InjectRecord Get(FS.HISFC.Models.Nurse.InjectInfo inject)
        {
            FS.HISFC.Models.Nurse.InjectRecord record = new FS.HISFC.Models.Nurse.InjectRecord();
            record.ID = inject.Memo;//注射单号
            record.OperEnv.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID;
            record.OperEnv.Name = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name;
            record.OperEnv.OperTime = this.irManager.GetDateTimeFromSysDateTime();
            record.OperType = 1;

            /* 这几行暂时不用，以后用时再整 */
            //record.KBack.ID = "";
            //record.KBack.Name = "";
            //record.Memo = "";
            /*------------------------------*/

            return record;
        }

        private List<FS.HISFC.Models.Nurse.InjectInfo> QueryConfirmItem()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return null;
            }

            List<FS.HISFC.Models.Nurse.InjectInfo> injectList = new List<FS.HISFC.Models.Nurse.InjectInfo>();


            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Nurse.InjectInfo inject = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Nurse.InjectInfo;
                if (inject == null)
                {
                    return null;
                }

                int temp = 0;
                int temp1 = FS.FrameWork.Function.NConvert.ToInt32(inject.User01);//可退数量
                int temp2 = FS.FrameWork.Function.NConvert.ToInt32(inject.User02);//已确认数量

                /*--------------------------------------*/
                if (temp1 > 0)
                {
                    temp1 -= 1;
                }
                else
                {
                    temp1 = 0;
                }
                /*--------------------------------------*/
                if (temp2 < inject.Order.InjectCount)
                {
                    temp2 += 1;
                }
                else
                {
                    temp2 = inject.Order.InjectCount;
                }
                /*--------------------------------------*/
                inject.User01 = temp1.ToString();
                inject.User02 = temp2.ToString();


                injectList.Add(inject);
            }
            return injectList;
        }

        private void ClearFarPoint()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            }
        }

        private void SetFarPoint()
        {
            this.neuSpread1_Sheet1.Columns[0].Visible = false;
            this.neuSpread1_Sheet1.Columns[2].Visible = false;
            this.neuSpread1_Sheet1.Columns[3].Visible = false;
            this.neuSpread1_Sheet1.Columns[4].Visible = false;
            this.neuSpread1_Sheet1.Columns[5].Visible = false;
            this.neuSpread1_Sheet1.Columns[6].Visible = false;
        }

        private int QueryInjectItem(string cardNO, DateTime dtCurrent)
        {
            List<FS.HISFC.Models.Nurse.InjectInfo> itemList = new List<FS.HISFC.Models.Nurse.InjectInfo>();
            itemList = this.decompound.QueryInjectItem(cardNO, dtCurrent);
            if( itemList == null || itemList.Count == 0 )
            {
                return -1;
            }

            this.ClearFarPoint();
            this.ShowPatientInfo(itemList[0]);

            for (int i = 0; i < itemList.Count; i++)
            {
                this.neuSpread1_Sheet1.Rows.Add(i, 1);//添加新行

                this.neuSpread1_Sheet1.SetText(i, 0, itemList[i].ID.PadLeft(12,'0'));//表ID
                this.neuSpread1_Sheet1.SetText(i, 1, itemList[i].Memo.PadLeft(12,'0'));//注射单号
                this.neuSpread1_Sheet1.SetText(i, 2, itemList[i].Name);//注射次数

                this.neuSpread1_Sheet1.SetText(i, 3, itemList[i].PrecontractDate.ToShortDateString());//预约注射日期
                this.neuSpread1_Sheet1.SetText(i, 4, itemList[i].PrecontractOrder);//预约注射序号
                this.neuSpread1_Sheet1.SetText(i, 5, itemList[i].InjectType);//注射类型
                this.neuSpread1_Sheet1.SetText(i, 6, itemList[i].InjectTypeNumber);//注射类型序号

                this.neuSpread1_Sheet1.SetText(i, 7, itemList[i].Order.Item.Name);//项目名称
                this.neuSpread1_Sheet1.SetText(i, 8, itemList[i].Order.Item.Specs);//规格
                this.neuSpread1_Sheet1.SetText(i, 9, itemList[i].Order.DoseOnce.ToString());//一次用量

                this.neuSpread1_Sheet1.SetText(i, 10, itemList[i].Order.DoseUnit);//一次用量单位

                this.neuSpread1_Sheet1.SetText(i, 11, itemList[i].GlassNum.ToString());//接瓶数


                this.neuSpread1_Sheet1.SetText(i, 12, itemList[i].Order.Usage.Name);//用法名称
                this.neuSpread1_Sheet1.SetText(i, 13, itemList[i].Order.Frequency.ID);//频次ID
                this.neuSpread1_Sheet1.SetText(i, 14, itemList[i].Order.Frequency.Name);//频次名称
                this.neuSpread1_Sheet1.SetText(i, 15, itemList[i].Order.Qty.ToString());//开立数量

                this.neuSpread1_Sheet1.SetText(i, 16, itemList[i].BaseDose.ToString());//基本用量
                this.neuSpread1_Sheet1.SetText(i, 17, itemList[i].Order.Item.PackQty.ToString());//包装数量
                this.neuSpread1_Sheet1.SetText(i, 18, itemList[i].Quality.Name);//药品性质
                this.neuSpread1_Sheet1.SetText(i, 19, itemList[i].Dosage.Name);//剂型
                if (itemList[i].IsMainDrug)
                {
                    this.neuSpread1_Sheet1.SetText(i, 20, "是");//是否主药
                }
                else
                {
                    this.neuSpread1_Sheet1.SetText(i, 20, "否");//是否主药
                }
                this.neuSpread1_Sheet1.Rows[i].Tag = itemList[i];
            }
            return 1;
        }

        private void ShowPatientInfo(FS.HISFC.Models.Nurse.InjectInfo patient)
        {
            this.lbName.Text = patient.Order.Patient.Name;
            this.lbSex.Text = patient.Order.Patient.Sex.Name;
            this.lbBirthday.Text = patient.Order.Patient.Birthday.ToShortDateString();
        }

        private void txtCardNO_Leave(object sender, EventArgs e)
        {
            if (this.txtCardNO.Text.Trim().Equals(""))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("病历号不能为空"));
                return;
            }
            this.txtCardNO.Text = this.GenerateCardNO(this.txtCardNO.Text);
        }

        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtCardNO.Text.Trim().Equals(""))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("病历号不能为空"));
                    return;
                }
                this.txtCardNO.Text = this.GenerateCardNO(this.txtCardNO.Text);
            }
        }

        private string GenerateCardNO(string value)
        {
            return value.PadLeft(10, '0');
        }

        private void InitControl()
        {
            this.lbBirthday.Text = "";
            this.lbSex.Text = "";
            this.lbName.Text = "";
            this.txtCardNO.Text = "";
            this.dtInject.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.decompound.GetSysDate());
            this.txtCardNO.Focus();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.QueryInjectItem(this.txtCardNO.Text, this.dtInject.Value) == -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有找到患者注射信息"));
            }
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    this.InitControl();
        //    this.SetFarPoint();
        //    base.OnLoad(e);
        //}

        private void ucConfirm_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitControl();
                this.SetFarPoint();
            }
            catch { }
        }
    }
}