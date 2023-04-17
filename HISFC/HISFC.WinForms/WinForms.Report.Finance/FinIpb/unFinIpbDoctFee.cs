using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinIpb
{
    public partial class unFinIpbDoctFee : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public unFinIpbDoctFee()
        {
            InitializeComponent();
        }
        #region ���� 
        private DeptZone deptZone1 = DeptZone.ALL;
        /// <summary>
        /// ���ڴ洢ͳ������
        /// </summary>
        private string reportCode = string.Empty;
        /// <summary>
        /// ���ڴ洢ͳ�ƴ���list
        /// </summary>
        private List<string> feeStatList = new List<string>();
        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();
        #endregion

        #region ����
        [Category("��������"), Description("��ѯ��Χ��MZ:���ZY:סԺ��ALL:ȫԺ")]
        public DeptZone DeptZone1
        {
            get
            {
                return deptZone1;
            }
            set
            {
                deptZone1 = value;
            }
        }
        #endregion
        protected override int OnRetrieve(params object[] objects)
        {


            if (cmbDeptZone.SelectedIndex == -1)
            {
                MessageBox.Show("�������ѯ���ݣ�");
                return -1;
            }


            if (this.cmbDeptZone.Items[cmbDeptZone.SelectedIndex].ToString() == "��ҽ�����ڿ���ͳ��")
            {
                this.MainDWDataObject = "d_fin_ipb_doctfee_recipe";
                dwMain.DataWindowObject = "d_fin_ipb_doctfee_recipe";
            }
            else if (this.cmbDeptZone.Items[cmbDeptZone.SelectedIndex].ToString() == "���������ڿ���ͳ��")
            {
                this.MainDWDataObject = "d_fin_ipb_doctfee_reg";
                dwMain.DataWindowObject = "d_fin_ipb_doctfee_reg";
            }
         

            this.MainDWLabrary = "Report\\finipb.pbd;Report\\finipb.pbd";
            dwMain.LibraryList = "Report\\finipb.pbd;Report\\finipb.pbd";


            string strFeelan = "ȫԺ";

            if (!string.IsNullOrEmpty(cmbFeelan.Items[cmbFeelan.SelectedIndex].ToString()))
            {
                strFeelan = cmbFeelan.Items[cmbFeelan.SelectedIndex].ToString();
            }

            string[] feeStatStr = this.feeStatList.ToArray();
            if (string.IsNullOrEmpty(reportCode) || this.feeStatList.Count == 0)
            {
                MessageBox.Show("��ѡ��ͳ�ƴ��࣡");
                return -1;
            }

           

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, this.reportCode, feeStatStr, strFeelan);
           
        }

        /// <summary>
        /// ��������¼�
        /// </summary>
        protected override void OnLoad()
        {
            base.OnLoad();

            cmbFeelan.Items.Clear();
            if (this.deptZone1 == DeptZone.ALL)
            {
                this.cmbFeelan.Items.Add("����");
                this.cmbFeelan.Items.Add("סԺ");
                this.cmbFeelan.Items.Add("ȫԺ");

                this.cmbFeelan.SelectedIndex = 0;
            }
            if (this.deptZone1 == DeptZone.MZ)
            {
                this.cmbFeelan.Items.Add("����");

                this.cmbFeelan.SelectedIndex = 0;

            }
            if (this.deptZone1 == DeptZone.ZY)
            {
                this.cmbFeelan.Items.Add("סԺ");

                this.cmbFeelan.SelectedIndex = 0;
            }

            this.cmbDeptZone.Items.Clear();
            this.cmbDeptZone.Items.Add("���������ڿ���ͳ��");
            this.cmbDeptZone.Items.Add("��ҽ�����ڿ���ͳ��");
            
            this.cmbDeptZone.SelectedIndex = 0;

            this.isAcross = true;
            this.isSort = false;
        }

        /// <summary>
        /// ö��
        /// </summary>
        public enum DeptZone
        {
            //����
            MZ = 0,
            //סԺ
            ZY = 1,
            //ȫԺ
            ALL = 2,
        }
        /// <summary>
        /// ͳ�ƴ��ఴť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFeeStat_Click(object sender, EventArgs e)
        {
            //ucPatientInfoFY patientInfoFY = new ucPatientInfoFY();

            ucFeeStatSelect feeStatSelect = new ucFeeStatSelect();
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��Ŀѡ��";
            DialogResult r = FS.FrameWork.WinForms.Classes.Function.PopShowControl(feeStatSelect);
            if (r == DialogResult.Cancel)
            {
                return;
            }
            this.reportCode = string.Empty;
            this.feeStatList = new List<string>();
            if (!string.IsNullOrEmpty(feeStatSelect.ReportCodeStr))
            {
                this.reportCode = feeStatSelect.ReportCodeStr;
                this.lblMemo.Text = "����ǰѡ����ͳ��������:[" + conManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.FEECODESTAT, feeStatSelect.ReportCodeStr.ToString()) + "]";
            }
            else
            {
                this.reportCode = string.Empty;
            }
            if (feeStatSelect.FeeStatList != null)
            {
                this.feeStatList = feeStatSelect.FeeStatList;
            }        

        }
    }
}
