using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.EPR.Controls
{
    public partial class ucSendMessage : UserControl
    {
        public ucSendMessage()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// ���ϵĹ�����
        /// </summary>
        private FS.HISFC.BizProcess.Factory.ManagerManagement managerManager = new FS.HISFC.BizProcess.Factory.ManagerManagement();
        /// <summary>
        /// ��ǰ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.Message message = null;

        private ArrayList lis = new ArrayList();
 

        
       
        public ucSendMessage(FS.FrameWork.Models.NeuObject patient,string eprId,string eprName,FS.FrameWork.Models.NeuObject oper)
        {
            InitializeComponent();
           
            this.InitialCombox();

            lis.Add(eprId);
            lis.Add(patient.Name);
            lis.Add(eprName);
            lis.Add(patient.ID);

            if (patient == null) return;
            if (eprName == "" && patient.Name == "")
            {
            }
            else if (eprName == "")
            {
                this.lblInfo.Text = string.Format("���ߣ�{0} ", patient.Name);
                this.textBox1.Text = string.Format("{0}�Ĳ��������⣬��Ҫ����!", patient.Name);

            }
            else
            {
                this.lblInfo.Text = string.Format("���ߣ�{0} �Ĳ���{1}", patient.Name, eprName);
                this.textBox1.Text = string.Format("{0}��{1}�����⣬��Ҫ����!", patient.Name, eprName);

            }

            if(oper !=null)
                this.comboBox1.Text = oper.Name;
            this.richTextBox1.Text = "�뼰ʱ���ģ�\n" + FS.FrameWork.Management.Connection.Operator.Name ;
        }
       

        #region �¼�
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "")
            {
                MessageBox.Show("��ѡ�����ˣ�", "��ʾ");

                this.comboBox1.Focus();

                return;
            }
            this.Save();

            DialogResult dr = MessageBox.Show("��Ϣ���ͳɹ����Ƿ��������Ϣ��", "��ʾ", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                this.ClearUp();
            }
            else if (dr == DialogResult.No)
            {
                this.FindForm().Close();
            }

            
        }
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
        #endregion 

        #region ����
        /// <summary>
        /// ��ʼ����Ա�б�
        /// </summary>
        private void InitialCombox()
        {
            ArrayList personLis = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.QueryEmployeeAll();

            this.comboBox1.AddItems(personLis);

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
        /// ��ȡҳ���ϵ�����
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Message GetMessage()
        {

            FS.HISFC.Models.Base.Message message = new FS.HISFC.Models.Base.Message();

            message.Receiver.Name = this.comboBox1.Text;

            message.Receiver.ID = this.comboBox1.Tag.ToString();

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

            message.ReplyType = -1;
            message.InpatientNo = lis[3].ToString();//eprId
            message.Emr.User01 = lis[1].ToString();// patient.Name
            message.Emr.Name = lis[2].ToString();//emrname
            message.Emr.ID = lis[0].ToString();
            
           


            return message;

        }
        /// <summary>
        /// �������
        /// </summary>
        private void ClearUp()
        {
            this.comboBox1.Text = "";

            this.textBox1.Text = "";

            this.richTextBox1.Text = "";
        }
        #endregion 
    }
}
