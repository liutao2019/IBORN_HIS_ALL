using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    /// <summary>
    /// [��������: סԺ���߷��û�����ϸ��ѯ]<br></br>
    /// [�� �� ��: wangjianfeng]<br></br>
    /// [����ʱ��: 2009-11-17]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucFinIpbOutPatientDetail2 : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        private string feeType = "";

        public ucFinIpbOutPatientDetail2()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                this.dwMain.Print();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡ����", "��ʾ");
                return -1;
            }

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected override int OnExport()
        {
            //������ڶ��DataWindowʱ����������Ҫ��д��������Ҫ��д�÷��������ݽ����жϵ��������ĸ�DataWindow
            try
            {
                //����Excel��ʽ�ļ�
                SaveFileDialog saveDial = new SaveFileDialog();
                saveDial.Filter = "Excel�ļ���*.xls��|*.xls";
                //�ļ���

                string fileName = string.Empty;
                if (saveDial.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveDial.FileName;
                }
                this.dwMain.SaveAs(fileName, Sybase.DataWindow.FileSaveAsType.Excel);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������", "��ʾ");
                return -1;
            }
        }

        protected override int OnRetrieve(params object[] objects)
        {
            switch (this.neuComboBox1.Text)
            {
                case "ȫ��":
                    this.feeType = "ALL";
                    break;
                case "ҩƷ":
                    this.feeType = "DRUG";
                    break;
                case "��ҩƷ":
                    this.feeType = "UNDRUG";
                    break;
            }

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            if (this.ucQueryInpatientNo1.InpatientNo == null)
            {
                return -1;
            }
            return base.OnRetrieve(this.ucQueryInpatientNo1.InpatientNo,this.feeType);
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            switch (this.neuComboBox1.Text)
            {
                case "ȫ��":
                    this.feeType = "ALL";
                    break;
                case "ҩƷ":
                    this.feeType="DRUG";
                    break;
                case "��ҩƷ":
                    this.feeType = "UNDRUG";
                    break;
            }

            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                if (this.ucQueryInpatientNo1.Err == "")
                {
                    ucQueryInpatientNo1.Err = "�˻��߲���Ժ!";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo1.Err, 211);

                this.ucQueryInpatientNo1.Focus();
                return;
            }
            else 
            {
                base.OnRetrieve(this.ucQueryInpatientNo1.InpatientNo,this.feeType);
            }
        }

        private void ucMetNuiOutPatientDetail_Load(object sender, EventArgs e)
        {
            if (this.neuComboBox1.Items.Count > 0)
            {
                this.neuComboBox1.SelectedIndex = 0;
            }
            else
            {
                return;
            }
        }        

    }
}
