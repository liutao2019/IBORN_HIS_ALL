using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    //{FC2B9551-0246-4375-8667-8EFF39A5CC6C}
    public partial class ucMesSend : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucMesSend()
        {
            InitializeComponent();
            this.fpOrder.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpOrder_ButtonClicked);
        }



        #region 变量属性

        HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();
        HISFC.Models.Registration.Register regobjtmp = new FS.HISFC.Models.Registration.Register();
        ArrayList regList =null;

        #endregion

        #region 方法

        private void ucOutPatientMessageSend_Load(object sender, EventArgs e)
        {
            regList = registerManager.PatientQueryByNurseCell("5080");


            InitControl();
        }

        public int InitControl()
        {
            InitFp();
            return 1;
        }

        public void InitFp()
        {
            int i = 0;
            if (regList != null && regList.Count > 0)
            {
                this.fpOrder_Sheet1.RowCount = 0;
                foreach (HISFC.Models.Registration.Register reg in regList) 
                {
                    this.fpOrder_Sheet1.Rows.Add(this.fpOrder_Sheet1.Rows.Count, 1);

                    this.fpOrder_Sheet1.Cells[i, 0].Value = reg.PID.CardNO;
                    this.fpOrder_Sheet1.Cells[i, 1].Value = reg.Name;
                    this.fpOrder_Sheet1.Cells[i, 2].Value = reg.PhoneHome;
                    this.fpOrder_Sheet1.Cells[i, 3].Value = reg.DoctorInfo.Templet.Doct.Name;

                    // this.fpEleInvoice_Sheet1.Cells[row,8].Value = balance.DayTime.ToString();
                    FarPoint.Win.Spread.CellType.ButtonCellType bct = new FarPoint.Win.Spread.CellType.ButtonCellType();
                    bct.Text = "发送";
                    this.fpOrder_Sheet1.Cells[i, 4].CellType = bct;

                    this.fpOrder_Sheet1.Rows[i].Tag = reg;
                    i++;
                }
            }
        }

        void fpOrder_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            //int a = e.Column;

            HISFC.Models.Registration.Register reg = new FS.HISFC.Models.Registration.Register();
            reg = this.fpOrder_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Registration.Register;

            FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

            FS.HISFC.Models.RADT.PatientInfo selectedPatient = accountMgr.GetPatientInfoByCardNO(reg.PID.CardNO);//.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (selectedPatient == null || string.IsNullOrEmpty(selectedPatient.PID.CardNO))
            {
                MessageBox.Show("请先检索患者！");
                return;
            }
            FS.HISFC.Components.Order.Forms.frmAddMsgForm msg = new FS.HISFC.Components.Order.Forms.frmAddMsgForm();
            msg.patient = selectedPatient;
            msg.Init();
            msg.ShowDialog();
        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            if (this.txtCardNo.Text=="")
            {
                MessageBox.Show("请输入卡号或姓名");
                return;
            }

            regList = null;

            regList=registerManager.PatientQueryByNameOrPhone(this.txtCardNo.Text.Trim());
            InitFp();
        }


        #endregion




    }
}
