using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Finance.FinIpb
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
    public partial class ucFinIpbOutPatientDetail4 : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {

        private string feeType = "";

        public ucFinIpbOutPatientDetail4()
        {
            InitializeComponent();
        }
        [Category("�ؼ�����"), Description("סԺ�������Ĭ������0סԺ�ţ�5����")]
        public int DefaultInput
        {
            get
            {
                return this.ucQueryInpatientNo1.DefaultInputType;
            }
            set
            {
                this.ucQueryInpatientNo1.DefaultInputType = value;
            }
        }

        [Category("�ؼ�����"), Description("�����Ų�ѯ������Ϣʱ�������ߵ�״̬��ѯ�����ȫ����'ALL'")]
        public string PatientInState
        {
            get
            {
                return this.ucQueryInpatientNo1.PatientInState;
            }
            set
            {
                this.ucQueryInpatientNo1.PatientInState = value;
            }
        }

        private Size printSize = new Size();
        [Category("�ؼ�����"), Description("���ô�ӡֽ�ŵĸ߶�")]
        public int PrintHeight
        {
            get
            {
                return printSize.Height;
            }
            set
            {
                printSize.Height = value;
            }

        }

        [Category("�ؼ�����"), Description("���ô�ӡֽ�ŵĿ��")]
        public int PrintWidth
        {
            get
            {
                return printSize.Width;
            }
            set
            {
                printSize.Width = value;
            }

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
                if (!printSize.IsEmpty)
                {
                    dwMain.Modify("DataWindow.Print.Paper.Size=256");
                    dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Length={0}", printSize.Height));
                    dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Width={0}", printSize.Width));
                }
                this.dwMain.Print();
                return 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡ����","��ʾ");
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

        public void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.Text == null || this.ucQueryInpatientNo1.Text.Trim() == "")
            {
                MessageBox.Show("������סԺ��");
                
                return;
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

                this.QueryData(this.ucQueryInpatientNo1.InpatientNo, this.feeType);
               //// base.OnRetrieve(this.ucQueryInpatientNo1.InpatientNo,this.feeType);
               // switch (this.neuComboBox1.Text)
               // {
               //     case "ȫ��":
               //         this.feeType = "ALL";
               //         break;
               //     case "ҩƷ":
               //         this.feeType = "DRUG";
               //         break;
               //     case "��ҩƷ":
               //         this.feeType = "UNDRUG";
               //         break;
               // }

               // if (base.GetQueryTime() == -1)
               // {
               //     return ;
               // }
               // if (this.ucQueryInpatientNo1.InpatientNo == null)
               // {
               //     return ;
               // }

               // //ʱ��

               // if (neuCheckBox1.Checked)
               // {
               //     this.dwMain.DataWindowObject = "d_fin_ipb_outpatient6";
               //     this.MainDWDataObject = "d_fin_ipb_outpatient6";
               //     this.MainDWLabrary = "Report\\pharmacy.pbd;Report\\pharmacy.pbl";
               //     base.OnRetrieve(string.IsNullOrEmpty(this.inpatientNO)?this.inpatientNO:this.ucQueryInpatientNo1.InpatientNo, this.feeType, this.dtpBeginTime.Value, this.dtpEndTime.Value);

               // }
               // else
               // {
               //     this.dwMain.DataWindowObject = "d_fin_ipb_outpatient5";
               //     this.MainDWDataObject = "d_fin_ipb_outpatient5";
               //     this.MainDWLabrary = "Report\\pharmacy.pbd;Report\\pharmacy.pbl";
               //     base.OnRetrieve(string.IsNullOrEmpty(this.inpatientNO) ? this.inpatientNO : this.ucQueryInpatientNo1.InpatientNo, this.feeType);
               // }
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

        //public int Print()
        //{
        //    FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        //    FS.HISFC.Models.Base.PageSize pageSize = new FS.HISFC.Models.Base.PageSize();
        //    FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        //    if (pageSize == null)
        //    {
        //        pageSize = pageSizeMgr.GetPageSize("ZYQD");
        //        if (pageSize != null && pageSize.Printer.ToLower() == "default")
        //        {
        //            pageSize.Printer = "";
        //        }
        //        if (pageSize == null)
        //        {
        //            pageSize = new FS.HISFC.Models.Base.PageSize("ZYQD", 400, 200);
        //        }
        //    }
        //    print.SetPageSize(pageSize);
        //    print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
        //    print.IsDataAutoExtend = false;
        //    try
        //    {
        //        this.dwMain.Print();
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("��ӡ����", "��ʾ");
        //        return -1;
        //    }

        //}


        public int QueryData(string inpatientNO, string feeType)
        {
            try
            {
                //this.dwMain = new FSDataWindow.Controls.FSDataWindow();

                if (base.GetQueryTime() == -1)
                {
                    return -1;
                }
                if (inpatientNO == null)
                {
                    return -1;
                }

                //ʱ��

                if (neuCheckBox1.Checked)
                {
                    //this.dwMain.DataWindowObject = "d_fin_ipb_outpatient6";
                    //this.MainDWDataObject = "d_fin_ipb_outpatient6";
                    //this.MainDWLabrary = "Report\\finipb.pbl";
                    base.OnRetrieve(inpatientNO, feeType, this.dtpBeginTime.Value, this.dtpEndTime.Value);

                }
                else
                {
                    //this.dwMain.DataWindowObject = "d_fin_ipb_outpatient5";
                    //this.MainDWDataObject = "d_fin_ipb_outpatient5";
                    //this.MainDWLabrary = "Report\\finipb.pbl";
                    //base.OnRetrieve(inpatientNO, feeType);
                    base.OnRetrieve(inpatientNO, feeType, new DateTime(2000, 1, 1), new DateTime(5000, 1, 1));
                }

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
