using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.FuYou
{
    /// <summary>
    /// [��������: סԺ���߷��û�����ϸ��ѯ]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: 2009-11-17]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucFinIpbInPatientDetail : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        private string feeType = "";

        private string trans_type = "";

        public ucFinIpbInPatientDetail()
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

            ucQueryInpatientNo1_myEvent();
            return 1;
            //switch (this.neuComboBox1.Text)
            //{
            //    case "ȫ��":
            //        this.feeType = "ALL";
            //        break;
            //    case "ҩƷ":
            //        this.feeType = "DRUG";
            //        break;
            //    case "��ҩƷ":
            //        this.feeType = "UNDRUG";
            //        break;
            //}

            //if (base.GetQueryTime() == -1)
            //{
            //    return -1;
            //}
            //if (this.ucQueryInpatientNo1.InpatientNo == null)
            //{
            //    return -1;
            //}

            ////ʱ��

            //if (neuCheckBox1.Checked)
            //{
            //    this.dwMain.DataWindowObject = "d_fin_ipb_outpatient4";
            //    this.MainDWDataObject = "d_fin_ipb_outpatient4";
            //    this.MainDWLabrary = "Report\\pharmacy.pbd;Report\\pharmacy.pbl";
            //    return base.OnRetrieve(this.ucQueryInpatientNo1.InpatientNo, this.feeType, this.dtpBeginTime.Value, this.dtpEndTime.Value);

            //}
            //else
            //{
            //    this.dwMain.DataWindowObject = "d_fin_ipb_outpatient4";
            //    this.MainDWDataObject = "d_fin_ipb_outpatient3";
            //    this.MainDWLabrary = "Report\\pharmacy.pbd;Report\\pharmacy.pbl";
            //    return base.OnRetrieve(this.ucQueryInpatientNo1.InpatientNo, this.feeType);
            //}
           
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.Text == null || this.ucQueryInpatientNo1.Text.Trim() == "")
            {
                MessageBox.Show("������סԺ��");
                
                return;
            }

            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                if (string.IsNullOrEmpty(this.ucQueryInpatientNo1.Err))
                {
                    ucQueryInpatientNo1.Err = "�˻��߲���Ժ!";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo1.Err, 211);

                this.ucQueryInpatientNo1.Focus();
                return;
            }
            else 
            {
               // base.OnRetrieve(this.ucQueryInpatientNo1.InpatientNo,this.feeType);
                this.Query(this.ucQueryInpatientNo1.InpatientNo);
            }
        }

        private void Query(string inpatientNO)
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

            if (this.cbQuitFee.Checked)
            {
                this.trans_type = "2";
            }
            else
            {
                this.trans_type = "ALL";
            }

            if (base.GetQueryTime() == -1)
            {
                return;
            }
            if (string.IsNullOrEmpty(inpatientNO))
            {
                return;
            }
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            //ȡ����ʵ��
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            patientInfo = radt.QueryPatientInfoByInpatientNO(inpatientNO);
            //סԺ����
            int inDay = 0;
            inDay = radt.GetInDays(patientInfo.ID);// Function.GetInHosDay(patientInfo);
            //ʱ��

            this.dwMain.DataWindowObject = this.MainDWDataObject;
            this.MainDWDataObject = this.MainDWDataObject;
            this.MainDWLabrary = this.MainDWLabrary;
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
                Application.DoEvents();
                if (neuCheckBox1.Checked)
                {
                    base.OnRetrieve(inpatientNO, this.feeType, this.dtpBeginTime.Value.Date, this.dtpEndTime.Value.AddDays(1).Date, this.trans_type);

                }
                else
                {
                    base.OnRetrieve(inpatientNO, this.feeType, DateTime.MinValue, DateTime.MaxValue, this.trans_type);
                }
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            dwMain.Modify("t_inDay.text = 'סԺ����:" + inDay.ToString() + "'");
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

        private void neuCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.neuCheckBox1.Checked)
            {
                this.dtpBeginTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
            }
            else
            {
               
                this.dtpBeginTime.Enabled = false;
                this.dtpEndTime.Enabled = false;
            }
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (neuObject is FS.HISFC.Models.RADT.PatientInfo)
            {
                this.Query(((FS.HISFC.Models.RADT.PatientInfo)neuObject).ID);
            }

            return base.OnSetValue(neuObject, e);
        }

    }
}
