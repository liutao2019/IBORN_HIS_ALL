using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace FS.HISFC.Components.Message
{
    public partial class frmReply : Form
    {
        public frmReply()
        {
            InitializeComponent();
        }
        Form parent = null;
       /// <summary>
        /// �вι��캯��
       /// </summary>
       /// <param name="id"></param>
        public frmReply(FS.HISFC.Models.Base.Message message,Form parent)
        {
           
            InitializeComponent();

            this.message = message;

            setInfoToControls();

            this.ComboBox1.Enabled = false;
            this.linkLabel1.Visible = false;
           
            if (message.InpatientNo != "")
            {
                this.linkLabel1.Visible = true;
            }
            this.parent = parent;
        }
        #region ����

        FS.HISFC.Models.Base.Message message = new FS.HISFC.Models.Base.Message();

        /// <summary>
        /// ���ϵĹ�����
        /// </summary>
        private FS.HISFC.BizProcess.Factory.ManagerManagement managerManager = new FS.HISFC.BizProcess.Factory.ManagerManagement();
        #endregion
        #region ��ʼ��
        /// <summary>
        /// ��ʵ�帳���ؼ���
        /// </summary>
        private void setInfoToControls()
        {
            this.textBox2.Text = message.Name;
            
            this.richTextBox2.Tag = message.ID;
            
            this.richTextBox2.Text = message.Text;

            this.ComboBox1.Text = message.Oper.Name;

            this.ComboBox1.Tag = message.Oper.ID;

            this.comboBox2.Text = this.comboBox2.Items[0].ToString();

        }
        
        #endregion 
        #region �¼�
        private void button1_Click(object sender, EventArgs e)
        {
            this.Save();

            DialogResult dr = MessageBox.Show("��Ϣ���ͳɹ����Ƿ��������Ϣ��", "��ʾ", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                this.ClearUp();
            }
            else if(dr == DialogResult.No)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 
       
       #region ����
        public int Save()
        {
            message = this.GetMessage();

            try
            {
                FS.HISFC.BizProcess.Factory.Function.BeginTransaction();

                int returnValue = 0;

                returnValue = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.InsertMessage(message);
                
               int updateValue = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.UpdateMessage(message);
                

                if (returnValue == -1||updateValue == -1)
                {
                    FS.HISFC.BizProcess.Factory.Function.RollBack();

                    MessageBox.Show("�����ύʧ�ܣ�" + FS.HISFC.BizProcess.Factory.Function.IntegrateManager.Err);

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
        /// �����ݴ���ʵ����
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Message GetMessage()
        {

            FS.HISFC.Models.Base.Message message = new FS.HISFC.Models.Base.Message();

            message.Receiver.Name = this.ComboBox1.Text;

            message.Receiver.ID = this.ComboBox1.Tag.ToString();

            FS.HISFC.Models.Base.Employee receiver = managerManager.GetPersonByID(message.Receiver.ID);

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

            message.ReplyType = this.comboBox2.Items.IndexOf(this.comboBox2.Text);

            message.ID =this.richTextBox2.Tag.ToString();

            message.IsRecieved = true;

            return message;

        }
        private void ClearUp()
        {
          
            this.textBox1.Text = "";

            this.richTextBox1.Text = "";
        }
        #endregion 

        private void frmReply_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "Re:" + this.textBox2.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo patient = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QueryPatientInfoByInpatientNO(message.InpatientNo);
            if (patient == null) return;
            this.Close();
            //FS.HISFC.Components.EPR.Query.Function.EditEMR(patient,parent);
        }
    }
}