using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.HISFC.Models.BlackList;
using Neusoft.HISFC.BizLogic.Fee;
using Neusoft.FrameWork.Management;
using Neusoft.FrameWork.Function;

namespace Neusoft.HISFC.Components.Account.Controls
{
    public partial class ucBlackList : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBlackList()
        {
            InitializeComponent();
        }
        #region ����
        /// <summary>
        /// ������
        /// </summary>
        Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// ������ҵ���
        /// </summary>
        PBlackList pBlackList = new PBlackList();
        /// <summary>
        /// ������ʵ��
        /// </summary>
        PatientBlackList pbl = null;
        /// <summary>
        /// ��������ϸʵ��
        /// </summary>
        PatientBlackListDetail pbld = null;
        /// <summary>
        /// ���תҵ���
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();
        private bool IsEnable = false;
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("���������", "�����߼��������", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("�ų�������", "�����ߴӺ��������ų�", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            return toolBarService;
        }

        /// <summary>
        /// ���ݲ����Ų�������
        /// </summary>
        /// <returns></returns>
        protected virtual void QueryData()
        {
            string cardNo = this.txtCardNo.Text.Trim();
            if (cardNo == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����벡���ţ�"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsEnable = false;
                return;
            }
            //{D7742D35-6162-4b30-8F60-1F22E48C271D}
            //cardNo = cardNo.PadLeft(HISFC.BizProcess.Integrate.Common.ControlParam.GetCardNOLen(), '0');
            //cardNo = cardNo.PadLeft(HISFC.BizProcess.Integrate.Common.ControlParam.GetCardNOLen(), '0');
            cardNo = cardNo.PadLeft(10, '0');
            this.txtCardNo.Text=cardNo;
            //���ݲ������ж��Ƿ���ڻ�����Ϣ
            Neusoft.HISFC.Models.RADT.PatientInfo p = radtIntegrate.QueryComPatientInfo(cardNo);
            if (p.PID.CardNO == null || p.PID.CardNO == string.Empty)
            {
                IsEnable = false;
                MessageBox.Show(Language.Msg("�ò�����") + cardNo + Language.Msg("�����ڻ�����Ϣ��"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.ucRegPatientInfo1.CardNO = cardNo;
            int result = 0;
            result = pBlackList.GetBlackList(cardNo,ref pbl);
            if (result == -1)
            {
                IsEnable = false;
                MessageBox.Show(Language.Msg("���Һ���������ʧ�ܣ�"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ckState.Checked = pbl.BlackListValid;
            this.SetBlackListDetail(pbl);
            IsEnable = true;
        }

        /// <summary>
        ///����ʾ��������ϸ����
        /// </summary>
        /// <param name="pbl"></param>
        private void SetBlackListDetail(PatientBlackList pbl)
        {
            List<PatientBlackListDetail> list = pbl.BlackListDetail;
            int count=list.Count;
            this.neuSpread1_Sheet1.Rows.Count = count;
            for (int i = 0; i < count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = pbl.CardNO;
                this.neuSpread1_Sheet1.Cells[i, 1].Text = list[i].BlackListValid.ToString();
                this.neuSpread1_Sheet1.Cells[i, 2].Text = list[i].Memo;
                this.neuSpread1_Sheet1.Cells[i, 3].Text = list[i].Oper.Name;
                this.neuSpread1_Sheet1.Cells[i, 4].Text = list[i].Oper.OperTime.ToString();
            }
        }
        /// <summary>
        /// ��ȡ������ʵ��
        /// </summary>
        /// <param name="bl">true:��������� false�Ӻ��������ų�</param>
        /// <returns></returns>
        private void GetPatientBlackList(bool bl)
        {
            pbl = new PatientBlackList();
            //����ʵ��
            pbl.CardNO = this.txtCardNo.Text;
            pbl.BlackListValid = bl;
            //��ϸ
            pbld = new PatientBlackListDetail();
            pbld.SeqNO = pBlackList.GetBlackListSeqNo();
            pbld.Memo = this.txtMemo.Text;
            pbld.Oper.ID = pBlackList.Operator.ID;
            pbld.Oper.OperTime = pBlackList.GetDateTimeFromSysDateTime();
            pbld.BlackListValid = bl;
            pbl.BlackListDetail.Clear();
            pbl.BlackListDetail.Add(pbld);
        }

        /// <summary>
        /// �����߼��������
        /// </summary>
        /// <returns></returns>
        protected virtual void AddBlackList()
        {
            if (!IsEnable)
            {
                MessageBox.Show(Language.Msg("�����벡���ţ����س�ȷ�ϣ�"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ckState.Checked)
            {
                MessageBox.Show(Language.Msg("�û������ں������У������ٴμ����������"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //�õ����ߺ�����ʵ��
            this.GetPatientBlackList(true);
            //����
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction trans = new Transaction(Connection.Instance);
            //trans.BeginTransaction();

            pBlackList.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //������������
            int resultValue= pBlackList.SaveBlackList(pbl);
            if (resultValue == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(pBlackList.Err);
                return;
            }
            //������ϸ����
            resultValue = pBlackList.AddBlackListDetail(pbl);
            if (resultValue == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(pBlackList.Err);
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("����������ɹ���"),Language.Msg("��ʾ"),MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.QueryData();
            this.txtCardNo.Text = string.Empty;
            this.txtCardNo.Focus();
            IsEnable = false;
        }

        /// <summary>
        /// �����ߴӺ��������ų�
        /// </summary>
        /// <returns></returns>
        protected virtual void CancelBlackList()
        {
            if (!IsEnable)
            {
                MessageBox.Show(Language.Msg("�����벡���ţ����س�ȷ�ϣ�"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //�õ����ߺ�����ʵ��
            this.GetPatientBlackList(false);

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction trans = new Transaction(Connection.Instance);
            //trans.BeginTransaction();

            pBlackList.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //������������
            int resultValue = pBlackList.UpdateBlackList(pbl);
            if (resultValue == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(pBlackList.Err);
                return;
            }

            //{E8D9B53F-9C12-4f6e-8F7C-A94FB6B9D173}
            if (resultValue == 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����Ա���ں������");
                return;
            }

            //������ϸ����
            resultValue = pBlackList.AddBlackListDetail(pbl);
            if (resultValue == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(pBlackList.Err);
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("�ų��������ɹ���"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.QueryData();
            this.txtCardNo.Text = string.Empty;
            this.txtCardNo.Focus();
            IsEnable = false;
        }

        #endregion

        #region �¼�
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "���������":
                    {
                        this.AddBlackList();
                        break;
                    }
                case "�ų�������":
                    {
                        this.CancelBlackList();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.QueryData();
            }
        }

        #endregion
    }
}
