using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucSetRealInvoiceNo : UserControl
    {
        public ucSetRealInvoiceNo()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucSetRealInvoiceNo_Load);
            this.cmbInvoiceType.SelectedIndexChanged += new EventHandler(cmbInvoiceType_SelectedIndexChanged);
            this.cmbInvoiceType.KeyDown += new KeyEventHandler(cmbInvoiceType_KeyDown);
            this.lblMessage.Text = "�տ�Ա��" + FS.FrameWork.Management.Connection.Operator.Name + "��" +
                FS.FrameWork.Management.Connection.Operator.ID + "��";
            this.txtReal.KeyDown += new KeyEventHandler(txtReal_KeyDown);
        }

        void txtReal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btOK.Focus();
            }
        }

        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
        private FS.HISFC.BizLogic.Fee.RealInvoice invoiceManager = new FS.HISFC.BizLogic.Fee.RealInvoice();
        string nextInvoiceType = string.Empty;//��ǰ��Ʊ����
        string nextRealInvoice = string.Empty;//��ǰ��Ʊ���Ͷ�Ӧ��һ�ŷ�Ʊ��

        void cmbInvoiceType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.GetNextRealInvoice();
            }
        }

        void cmbInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetNextRealInvoice();
        }

        void GetNextRealInvoice()
        {
            this.txtReal.Text = string.Empty;
            nextInvoiceType = string.Empty;//��ǰ��Ʊ����
            nextRealInvoice = string.Empty;//��ǰ��Ʊ���Ͷ�Ӧ��һ�ŷ�Ʊ��

            //��ȡ��һ��ʵ�ʷ�Ʊ��
            if (this.GetRealInvoice() == -1)
            {
                //�ڳ���ĵط����д�����ʾ
                //MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ" + this.cmbInvoiceType.SelectedItem.Name + "��һ��ʵ�ʷ�Ʊ�ų���"));
                return;
            }
            else
            {
                //nextInvoiceType = objNext.Name;
                this.txtReal.Text = nextRealInvoice;
                this.txtReal.Focus();
            }
        }

        int GetRealInvoice()
        {
            FS.FrameWork.Models.NeuObject objNext = new FS.FrameWork.Models.NeuObject();
            objNext.ID = FS.FrameWork.Management.Connection.Operator.ID;
            objNext.Name = this.cmbInvoiceType.SelectedItem.ID;

            if (objNext.Name == null || objNext.Name == string.Empty)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ��Ʊ���ͣ�"));
                return -1;
            }
            //��ȡ��һ��ʵ�ʷ�Ʊ��
            if (this.invoiceManager.QueryNextRealInvoiceNo(objNext, ref nextRealInvoice) == -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ" + this.cmbInvoiceType.SelectedItem.Name + "��һ��ʵ�ʷ�Ʊ�ų���"+feeInpatient.Err));
                return -1;
            }
            nextInvoiceType = objNext.Name;
            return 1;
        }

        void ucSetRealInvoiceNo_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        public int GetRealInvoiceNo(string invoiceType, string operId)
        {
            /*
            //�ȸ��ݲ���ԱID�ͷ�Ʊ���ͻ�ȡ��ǰ��Ʊ�š�
            FS.FrameWork.Models.NeuObject objGetNextRealInvoice = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
            objGetNextRealInvoice.ID = operId;  //.Operator.ID;
            objGetNextRealInvoice.Name = invoiceType;

             //ά���˶��չ�ϵ��ִ�иò���
            string realInvoiceNo = string.Empty;
            string errCode="";
            if (this.invoiceManager.QueryNextRealInvoiceNo(objGetNextRealInvoice, ref realInvoiceNo) == -1)
            {
                MessageBox.Show(invoiceManager.Err);
                return -1;
            }

            string invoiceNo_Comp = (FS.FrameWork.Function.NConvert.ToDecimal(this.invoiceManager.GetUsingInvoiceNO(invoiceType)) + 1).ToString();

            //Ȼ����ʾ������Ա��ʾ�Ƿ��޸ģ�
            if (string.IsNullOrEmpty(realInvoiceNo))
            {
                this.SetInvoiceKind(invoiceType);
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucRealinvoiceNo);
                if (ucRealinvoiceNo.FindForm().DialogResult == DialogResult.OK) //��һ������ʵ�ʷ�Ʊ�ųɹ�,������Է�Ʊ��
                {
                    FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

                    if (this.invoiceManager.InsertNewInvoice(invoiceType) == -1)
                    {
                        MessageBox.Show("���ó�ʼ��Ʊ��ʧ��,����ϵ��Ϣ��,лл");
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
                return 1;
            }
            else
            {
                DialogResult rs;

                rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ǰ����Ʊ��Ϊ��" + realInvoiceNo + "�����Է�Ʊ��Ϊ��" + invoiceNo_Comp + "��\n �Ƿ�����޸ģ�"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.Yes)
                {
                    this.SetInvoiceKind(invoiceType);
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(this);

                    return 1;
                }
                else
                {
                    return 1;
                }
            }**/
            return 1;
        }

        private void Init()
        {
            //��ȡ��Ʊ����
            this.cmbInvoiceType.AddItems((managerIntegrate.GetConstantList("GetInvoiceType")));

            if (this.cmbInvoiceType.alItems == null || this.cmbInvoiceType.alItems.Count == 0)
            {
                MessageBox.Show("���ڳ���ά����ά���վݵ����");
                return;
            }
            this.lblMessage.Text = "�տ�Ա��" + FS.FrameWork.Management.Connection.Operator.Name + "��" +
                FS.FrameWork.Management.Connection.Operator.ID + "��";
            if (this.invoiceKind != "")
            {
                this.cmbInvoiceType.Tag = this.invoiceKind;
                this.cmbInvoiceType.Enabled = false;
            }

            /*
            FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
            string invoiceNo_Comp = (FS.FrameWork.Function.NConvert.ToDecimal(feeMgr.GetUsingInvoiceNO(invoiceKind)) + 1).ToString();
            if (string.IsNullOrEmpty(invoiceNo_Comp))
            {
                MessageBox.Show("��ȡ��һ�ŵ��Է�Ʊ��ʧ�ܣ�����ϵ�������ģ�");
                return;
            }
            this.txtInvoiceNoComp.Text = invoiceNo_Comp;
             * **/
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.txtReal.Text.Trim() == string.Empty)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������һ��ʵ�ʷ�Ʊ�ţ�"));
                return;
            }
            if (this.txtInvoiceNoComp.Text.Trim() == string.Empty)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������һ�ŵ��Է�Ʊ�ţ�"));
                return;
            }


            FS.FrameWork.Models.NeuObject objUpdate = new FS.FrameWork.Models.NeuObject();
            objUpdate.ID = FS.FrameWork.Management.Connection.Operator.ID;
            objUpdate.Name = this.cmbInvoiceType.SelectedItem.ID;
            objUpdate.Memo = this.txtReal.Text.Trim();
            objUpdate.User01 = nextRealInvoice;
            //��Ҫ���µķ�Ʊ�Ž����жϣ��Ƿ�������ݿ���
            FS.HISFC.Models.Fee.InvoiceExtend realInvoice = invoiceManager.GetInoviceByRealInvoice(this.txtReal.Text.Trim(), this.cmbInvoiceType.SelectedItem.ID);
            if (realInvoice != null)
            {
                MessageBox.Show("��ʵ�ʷ�Ʊ���Ѿ������ݿ��д��ڣ�������޸�" + invoiceManager.Err);
                return;
            }
            /*
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            feeInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //�ȸ���
            if (feeInpatient.UpdateNextRealInvoiceNo(objUpdate) <= 0)
            {
                //����
                if (feeInpatient.InsertIntoNextRealInvoiceNo(objUpdate) <= 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������һ��ʵ�ʷ�Ʊ��Ϣ����") + feeInpatient.Err);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
            }
            string txtInvoiceNo = this.txtInvoiceNoComp.Text;
            if (FS.FrameWork.Function.NConvert.ToInt32(feeInpatient.ExecSqlReturnOne(@"SELECT count(*) FROM FIN_COM_REALINVOICE t WHERE t.INVOICE_NO='" + txtInvoiceNo + "' and t.INVOICE_TYPE='" + invoiceKind + "'")) > 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˵��Է�Ʊ���Ѿ������ݿ��д��ڣ�������޸ģ�"));
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }

            //�����һ�ţ���

            //���µ��Է�Ʊ�ţ��������δʹ�õķ�Ʊ��
            //�˴���������ķ�Ʊ�ŵ���һ�Ÿ��µ����ݿ⣬��Ϊ��ʹ�õķ�Ʊ

            string preInvoiceNo = (FS.FrameWork.Function.NConvert.ToDecimal(txtInvoiceNo) - 1).ToString();

            if (this.feeInpatient.ExecNoQuery(@"UPDATE FIN_COM_INVOICE 
                                                SET USED_NO='{2}'
                                                WHERE GET_PERSON_CODE='{1}'
                                                AND INVOICE_KIND='{0}'", invoiceKind, FS.FrameWork.Management.Connection.Operator.ID, preInvoiceNo) == -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���µ��Է�Ʊ��ʧ�ܣ�" + this.feeInpatient.Err));
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.FindForm().DialogResult = DialogResult.OK;
            //MessageBox.Show("�����տ�Ա��һ��ʵ�ʷ�Ʊ�ųɹ���");
            MessageBox.Show("�����տ�Ա��һ�ŷ�Ʊ�ųɹ���");
            this.FindForm().Close();
            **/
        }
        string invoiceKind = "";
        public void SetInvoiceKind(string invoiceKind)
        {
            this.invoiceKind = invoiceKind;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }
    }
}
