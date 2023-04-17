using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    public partial class ucAdvanceDays : UserControl
    {
        public ucAdvanceDays()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// �ҵ��Զ���ί��
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="myEventArgs"></param>
        public delegate void CustomEvent(FS.FrameWork.Models.NeuObject msg, EventArgs myEventArgs);
        /// <summary>
        /// �Զ����¼� ʹ��ʱ���޸Ĳ���
        /// </summary>
        public event CustomEvent CustomEventHandler = null;

        private string text = string.Empty;

        /// <summary>
        /// ��֤�����ַ�����ֻ������
        /// </summary>
        /// <param name="str">����֤�ַ���</param>
        /// <returns>true ֻ������ false �������������ַ�</returns>
        public bool IsValidString(string str)
        {
            return Regex.IsMatch(str, @"^(-?\d+)(\.\d+)?$");//(str, @"^[0-9\u4e00-\u9fa5]+$");
        }

        
        /// <summary>
        /// ��ȡ��ǰ������
        /// </summary>
        public int AdvanceDays
        {
            get 
            {
                //��֤������Ч��
                return Convert.ToInt32(this.neuTextBox1.Text.Trim());
            }
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!this.IsValidString(this.neuTextBox1.Text.Trim()))
            {
                //MessageBox.Show("����������", "��ʾ");

                this.neuTextBox1.Clear();
                return;
            }
        }


    }
}
