using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
namespace FS.HISFC.Components.Message
{
    public partial class ucMessage : UserControl
    {
        #region ����

        private DataView dv = null;

        private DataTable dt = new DataTable();
        #endregion


        public ucMessage()
        {
            InitializeComponent();

        }

        #region ��ʼ��

        private void InitFpColumn()
        {

            dt.Columns.Add("", typeof(Image));
            dt.Columns.Add("������", typeof(string));
            dt.Columns.Add("����", typeof(string));
            dt.Columns.Add("����", typeof(DateTime));
            dt.Columns.Add("�ظ�״̬", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("�Ķ�״̬", typeof(string));
            this.dv = new DataView(dt);
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
            this.fpSpread1_Sheet1.DataSource = dv;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 17F;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 77F;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 164F;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 141F;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 59F;
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 59F;
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 59F;




        }
        /// <summary>
        /// �����е�����
        /// </summary>
        private void InitColumnsType()
        {
            FarPoint.Win.Spread.CellType.ImageCellType imageCellType = new FarPoint.Win.Spread.CellType.ImageCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType2 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).CellType = imageCellType;
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = imageCellType;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = dateTimeCellType2;
            dateTimeCellType2.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;
            
        }
        /// <summary>
        /// ��ʼ��fp
        /// </summary>
        /// <returns></returns>
        private int InitMessageData()
        {


            ArrayList messageLis =
                FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryMessage( FS.FrameWork.Management.Connection.Operator.ID );

            if (messageLis == null) return 0;


            this.dt.Rows.Clear();

            foreach (FS.HISFC.Models.Base.Message message in messageLis)
            {
                DataRow row = this.dt.NewRow();

                if (message.IsRecieved == false)
                {

                    row[0] = Properties.Resources.close;

                }
                else
                {

                    row[0] = Properties.Resources.open;
                }


                row[1] = message.Oper.Name;
                row[2] = message.Name;
                row[3] = message.Oper.OperTime;
                if (message.ReplyType == 0)
                {
                    row[4] = "���Ķ�";
                }
                else if (message.ReplyType == 1)
                {
                    row[4] = "�ѻظ�";
                }
                else if (message.ReplyType == 2)
                {
                    row[4] = "�����";
                }
                else
                {
                    row[4] = "";
                }
                row[5] = message.ID;
                row[6] = message.IsRecieved;
                this.dt.Rows.Add(row);
            }

            this.InitColumnsType();

            return 1;
        }

        private void InitCombo()
        {

            this.neuComboBox1.Items.Add("��ʾȫ��");
            this.neuComboBox1.Items.Add("���Ķ���");
            this.neuComboBox1.Items.Add("δ�Ķ���");
            this.neuComboBox1.Text = "δ�Ķ���";

        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucMessage_Load(object sender, EventArgs e)
        {

            this.InitFpColumn();
            InitMessageData();
            this.InitCombo();

        }



        #endregion
        #region �¼�

        /// <summary>
        /// ˫���ظ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            string messageId = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 5].Text;

            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Value = Properties.Resources.open;

            try
            {
                FS.HISFC.Models.Base.Message message = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryMessageById(messageId);

                message.IsRecieved = true;

                message.ReplyType = 0;

                this.UpdateState(message);

                frmReply reply = new frmReply(message, this.FindForm());

                reply.ShowDialog();

                InitMessageData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// д��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            frmSendMessage sendMessage = new frmSendMessage();

            sendMessage.Show();
        }
        /// <summary>
        /// ����Ϣ��ˢ���б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            InitMessageData();

            this.neuComboBox1.Text = "δ�Ķ���";

        }
        /// <summary>
        /// ѡ��һ����Ϣ���ظ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0) return;

            string messageId = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 5].Text;

            try
            {
                FS.HISFC.Models.Base.Message message = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryMessageById( messageId );

                message.IsRecieved = true;

                message.ReplyType = 0;

                this.UpdateState(message);

                frmReply reply = new frmReply(message, this.FindForm());

                reply.ShowDialog();

                InitMessageData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        #endregion
        #region ����
        /// <summary>
        /// �޸���Ϣ״̬
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private int UpdateState(FS.HISFC.Models.Base.Message message)
        {
            try
            {
                FS.HISFC.BizProcess.Factory.Function.BeginTransaction();

                int returnValue = 0;

                returnValue = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.UpdateMessage( message );

                if (returnValue == -1)
                {
                    FS.HISFC.BizProcess.Factory.Function.RollBack();

                    MessageBox.Show( "����ʧ�ܣ�" + FS.HISFC.BizProcess.Factory.Function.IntegrateManager.Err );

                    return -1;
                }
                else
                {
                    FS.HISFC.BizProcess.Factory.Function.Commit();

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 1;
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {

            string messageId = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 5].Text;
            if (messageId == "")
            {
                MessageBox.Show("��ѡ��һ����¼", "��ʾ");
                return;
            }

            if (MessageBox.Show("ȷʵҪɾ����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int returnvalue = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.DeleteMessage(messageId);

                if (returnvalue == 1)
                {

                    MessageBox.Show("ɾ���ɹ���");
                    this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Remove();

                }
                else
                {
                    MessageBox.Show("ɾ��ʧ�ܣ�");

                    return;
                }
            }


        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterStr = "";


            string queryCode = this.neuComboBox1.Text;

            if (queryCode == "δ�Ķ���")
            {
                filterStr = "�Ķ�״̬ = 'false'";
                this.dv.RowFilter = filterStr;
            }

            else if (queryCode == "���Ķ���")
            {
                filterStr = "�Ķ�״̬ = 'true'";
                this.dv.RowFilter = filterStr;
            }
            else if (queryCode == "��ʾȫ��")
            {
                filterStr = "(�Ķ�״̬ = 'true') OR (�Ķ�״̬ = 'false')";
                this.dv.RowFilter = filterStr;
 
            }
            this.dv.Sort = "���� DESC";
            this.InitColumnsType();

        }



















    }
}
