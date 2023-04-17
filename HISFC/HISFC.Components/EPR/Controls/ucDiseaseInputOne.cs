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
    internal partial class ucDiseaseInputOne : UserControl
    {
        public ucDiseaseInputOne()
        {
            InitializeComponent();
        }
         

        public delegate void DateTimePickerChanged(string str);
        public event DateTimePickerChanged DateTimePickerChange;
        /// <summary>
        /// �Ƿ��ύ���̼�¼����
        /// </summary>
        private string isUpSubmission = "0";
        public string IsUpSubmission
        {
            get 
            {
                return this.isUpSubmission ;
            }
            set
            {
                this.isUpSubmission = value;
                if (value == "1")
                {
                    this.label4.Text = "���ύ";
                    this.emrMultiLineTextBox1.IsShowModify = true;
                    if(this.txtDocSign.Text.Trim()=="")
                        this.txtDocSign.Text = FS.FrameWork.Management.Connection.Operator.Name;
                }
                else
                {
                    this.label4.Text = "δ�ύ";
                    this.emrMultiLineTextBox1.IsShowModify = false;
                }
            }
        }
        /// <summary>
        /// �Ƿ��ϼ�ҽ��ǩ��
        /// </summary>
        private string isUpDocSign = "0";
        public string IsUpDocSign 
        {
            get 
            {
                return this.isUpDocSign;
            }
            set 
            {
                this.isUpDocSign = value;
            }

        }
        private void ucDiseaseInputOne_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {            
            if (DateTimePickerChange != null)
            {
                this.DateTimePickerChange(this.dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            }
        }
    }
}
