using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;

namespace FS.HISFC.Components.InpatientFee.Balance
{
    public partial class ucBalanceSave : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBalanceSave()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        DateTime beginTime = DateTime.Now;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        DateTime endTime = DateTime.Now;

        /// <summary>
        /// סԺ�շ�ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// סԺ���תҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// toolBar
        /// </summary>
        ToolBarService toolBarService = new ToolBarService();

        #endregion

        #region ����

        #endregion

        #region ����

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ƽ��", "ƽ�˽��㻼�߷���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("ȡ��", "ȡ��ƽ�˲���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ƽ��") 
            {
                this.Save(false);
            }
            if (e.ClickedItem.Text == "ȡ��")
            {
                this.Save(true);
            }
            
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// ��ѯ��¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            beginTime = this.dtpBegin.Value;
            endTime = this.dtpEnd.Value;

            this.spDetail_Sheet1.RowCount = 0;
            this.spDetail_Sheet1.RowCount = 10;

            this.spQuery_Sheet1.RowCount = 0;
            this.spQuery_Sheet1.RowCount = 10;

            //��ѯδ����� Ƿ�Ѽ�¼
            ArrayList detail = this.inpatientFeeManager.QueryBalancesBySaveTime(this.beginTime, this.endTime, "0");
            if (detail == null) 
            {
                MessageBox.Show("��ѯδ����Ƿ�Ѽ�¼����!" + this.inpatientFeeManager.Err);

                return -1;
            }

            this.SetValue(this.spDetail_Sheet1, detail);

            //��ѯ�Ѿ������ Ƿ�Ѽ�¼
            ArrayList query = this.inpatientFeeManager.QueryBalancesBySaveTime(this.beginTime, this.endTime, "1");
            if (query == null)
            {
                MessageBox.Show("��ѯ�Ѿ�����Ƿ�Ѽ�¼����!" + this.inpatientFeeManager.Err);

                return -1;
            }

            this.SetValue(this.spQuery_Sheet1, query);
            
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="isCancel">�Ƿ���ȡ������</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int Save(bool isCancel) 
        {
            if (!isCancel)//ƽ�� 
            {
                if (this.neuTabControl1.SelectedTab != this.tpDetail)
                {
                    MessageBox.Show("��ѡ��δ�����¼Tabҳ,��ѡ����Ӧ��¼!");

                    return -1;
                }
            }
            else 
            {
                if (this.neuTabControl1.SelectedTab != this.tpQuery)
                {
                    MessageBox.Show("��ѡ���Ѵ����¼Tabҳ,��ѡ����Ӧ��¼!");

                    return -1;
                }
            }

            TabPage tpSelected = this.neuTabControl1.SelectedTab;

            FarPoint.Win.Spread.SheetView svSelected = (tpSelected.Controls[0] as FS.FrameWork.WinForms.Controls.NeuSpread).Sheets[0];

            if (svSelected.RowCount == 0) 
            {
                return -1;
            }
            int rowSelected = svSelected.ActiveRowIndex;
            if (svSelected.Rows[rowSelected].Tag == null) return -1;

            DialogResult result = MessageBox.Show("�Ƿ���ǰѡ����?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No) 
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.inpatientFeeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            

            FS.HISFC.Models.Fee.Inpatient.Balance balance = svSelected.Rows[rowSelected].Tag as FS.HISFC.Models.Fee.Inpatient.Balance;

            //���´�����
            int returnValue = this.inpatientFeeManager.UpdateBalanceSaveInfo(balance,FS.FrameWork.Function.NConvert.ToInt32(!isCancel).ToString());
            if (returnValue <= 0) 
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show("����ʧ��!" + this.inpatientFeeManager.Err);

                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(isCancel ? "ȡ��ƽ�˼�¼�ɹ�!" : "ƽ�˳ɹ�!");

            //���¼�����¼
            this.OnQuery(this, new FS.FrameWork.Models.NeuObject());

            return 1;
        }

        /// <summary>
        /// ��ʾֵ
        /// </summary>
        /// <param name="sv">��ǰSheetҳ</param>
        /// <param name="balances">������Ϣ</param>
        private void SetValue(FarPoint.Win.Spread.SheetView sv, ArrayList balances) 
        {
            if (balances == null || balances.Count == 0)
            {
                return; 
            }

            sv.RowCount = balances.Count;

            for (int i = 0; i < balances.Count; i++) 
            {
                FS.HISFC.Models.Fee.Inpatient.Balance b = balances[i] as FS.HISFC.Models.Fee.Inpatient.Balance;
                FS.HISFC.Models.RADT.PatientInfo patient = this.radtIntegrate.GetPatientInfomation(b.Patient.ID);
                if (patient == null)
                {
                    MessageBox.Show("��û��߻�����Ϣ����!" + this.radtIntegrate.Err);

                    return;
                }
                sv.Cells[i, 0].Text = patient.PID.PatientNO;
                sv.Cells[i, 1].Text = patient.Name;
                sv.Cells[i, 2].Text = patient.PVisit.PatientLocation.Dept.Name;

                string balanceTypeName = string.Empty;
                if (b.BalanceType.ID.ToString() == "Q") 
                {
                    balanceTypeName = "Ƿ��";
                }
                sv.Cells[i, 3].Text = balanceTypeName;
                sv.Cells[i, 4].Text = b.FT.TotCost.ToString();
                sv.Cells[i, 5].Text = b.FT.SupplyCost > 0 ? "-"+b.FT.SupplyCost.ToString() :b.FT.ReturnCost.ToString();
                sv.Cells[i, 6].Text = b.BalanceOper.OperTime.ToString();
                sv.Rows[i].Tag = b.Clone();
            }
        }

        #endregion

        #region �¼�

        #endregion
    }
}
