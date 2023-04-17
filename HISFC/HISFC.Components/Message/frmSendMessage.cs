using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Message
{
    public partial class frmSendMessage : Form
    {
        public frmSendMessage()
        {
            InitializeComponent();
        }

        private void frmSendMessage_Load(object sender, EventArgs e)
        {
            InitialCombox();
        }
        
        private FS.HISFC.Models.Base.Message message = null;
       

        /// <summary>
        /// ���ϵĹ�����
        /// </summary>
        private FS.HISFC.BizProcess.Factory.ManagerManagement managerManager = new FS.HISFC.BizProcess.Factory.ManagerManagement();

        /// <summary>
        /// ��ǰ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.Message Message
        {
            get { return message; }
            set { message = value; }
        }
        #region ��ʼ����Ա�б�
        private void InitialCombox()
        {
            ArrayList personLis = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.QueryEmployeeAll();

            this.ComboBox1.AddItems(personLis);
            
        }
        #endregion 
        #region �¼�
        /// <summary>
        /// ������Ϣ������Ϣ�������ݿ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.ValueValidated()) return;
            
            this.Save();

            DialogResult dr = MessageBox.Show("��Ϣ���ͳɹ����Ƿ��������Ϣ��", "��ʾ",MessageBoxButtons.YesNo);
          
            if (dr == DialogResult.Yes)
            {
                this.ClearUp();
            }
            else if(dr == DialogResult.No)
            {
                this.Close();
            }


            
        }
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 
       
        #region ����
        /// <summary>
        /// �����ݴ���ʵ����
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Message GetMessage()
        {

            FS.HISFC.Models.Base.Message message = new FS.HISFC.Models.Base.Message();

            message.Receiver.Name = this.ComboBox1.Text;

            message.Receiver.ID = this.ComboBox1.Tag.ToString();

            FS.HISFC.Models.Base.Employee receiver = managerManager.GetPersonByID( message.Receiver.ID);

            message.ReceiverDept.ID = receiver.Dept.ID;

            message.ReceiverDept.Name = receiver.Dept.Name;
           
            message.Name = this.textBox1.Text.Trim();

            message.Text = this.richTextBox1.Text.Trim();

            message.Oper.OperTime = System.DateTime.Now;

            message.Oper.Name = FS.FrameWork.Management.Connection.Operator.Name;

            message.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;

            FS.HISFC.Models.Base.Employee oper = managerManager.GetPersonByID(message.Oper.ID);

            message.SenderDept.ID = oper.Dept.ID;

            message.SenderDept.Name = oper.Dept.Name;

            message.ReplyType = -1;


            return message;
            
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            message = GetMessage();

            try
            {
                FS.HISFC.BizProcess.Factory.Function.BeginTransaction();

                int returnValue = 0;
               
                returnValue = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.InsertMessage(message);

                if (returnValue == -1)
                {
                    FS.HISFC.BizProcess.Factory.Function.RollBack();

                    MessageBox.Show("����ʧ�ܣ�" + FS.HISFC.BizProcess.Factory.Function.IntegrateManager.Err);

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
        /// <summary>
        /// �������
        /// </summary>
        private void ClearUp()
        {
            this.ComboBox1.Text = "";

            this.textBox1.Text = "";

            this.richTextBox1.Text = "";
        }
        /// <summary>
        /// ����У��
        /// </summary>
        /// <returns></returns>
        private bool ValueValidated()
        {
            if (this.ComboBox1.Text == "")
            {
                MessageBox.Show("��ѡ�����ˣ�", "��ʾ");

                this.ComboBox1.Focus();

                return false;
            }

            else
            {
                return true;
            }
        }
        #endregion 


    }
}