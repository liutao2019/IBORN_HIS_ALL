using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;
using System.Runtime.InteropServices;

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class frmBloodInfo : Form
    {
        //�Ƿ����
        public bool saved = false;
        //����
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;
        //ҽ��
        private FS.HISFC.Models.Base.Employee loginPerson;
        private OperApply apply;
        private OperApplyManage applyManage;
        private FS.HISFC.BizLogic.Manager.Constant conMgr;

        /// <summary>
        /// ������ȡ����ҪѪ�걾�Ŀ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        List<string> noOperSpecList = new List<string>();

        private string deptName;
        private string deptId;
        private string diagnose = "";
        //�Ƿ����ȡ��
        private bool canCancel = false;
        private bool canceled = false;

        private const int SC_CLOSE = 0xF060;
        private const int MF_ENABLED = 0x00000000;
        private const int MF_GRAYED = 0x00000001;
        private const int MF_DISABLED = 0x00000002;
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, int bRevert);
        [DllImport("User32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, int uIDEnableItem, int uEnable);

        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }

        public string DeptName
        {
            set
            {
                deptName = value;
            }
        }

        public string DeptId
        {
            set
            {
                deptId = value;
            }
        }

        public bool Cancel
        {
            set
            {
                canCancel = value;
            }
        }

        public bool Canceled
        {
            get
            {
                return canceled;
            }
        }

        public OperApply Apply
        {
            get
            {
                return apply;
            }
            set
            {
                apply = value;
            }
        }
        public frmBloodInfo()
        {
            InitializeComponent();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            patientInfo = new FS.HISFC.Models.RADT.PatientInfo();           
            apply = new OperApply();
            applyManage = new OperApplyManage();
            conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            IntPtr hMenu = GetSystemMenu(this.Handle, 0);
            EnableMenuItem(hMenu, SC_CLOSE, MF_DISABLED | MF_GRAYED);
        }

        public void SetTrans(System.Data.IDbTransaction trans)
        {
            applyManage.SetTrans(trans);
        }

        public int SetValue()
        {
            if (txtDiagnose.Text.Trim() == "")
            {
                MessageBox.Show("��ϲ���Ϊ��");
                return 0;
            }
            apply.MainDiaName = txtDiagnose.Text;
            apply.Comment = txtMedComment.Text;

            //���òɱ걾ǰ��������Ϣ
            string period = "";
            if (chkMed.Checked)
            {
                period = rbtAfterMed.Checked ? Convert.ToInt32(Constant.GetPeriod.���ƺ�).ToString() : Convert.ToInt32(Constant.GetPeriod.����ǰ).ToString();
            }
            if (chkRad.Checked)
            {
                period += rbtAfterRad.Checked ? Convert.ToInt32(Constant.GetPeriod.���ƺ�).ToString() : Convert.ToInt32(Constant.GetPeriod.����ǰ).ToString();// "4";      
            }
            if (chkNone.Checked) period += Convert.ToInt32(Constant.GetPeriod.��).ToString();// "8";
            if (chkOther.Checked) period += Convert.ToInt32(Constant.GetPeriod.����).ToString();// "7";
            if(chkOperation.Checked)
            {
                period += rbtAfterOper.Checked ? Convert.ToInt32(Constant.GetPeriod.������).ToString() : Convert.ToInt32(Constant.GetPeriod.����ǰ).ToString();// "4";      
            }
            apply.GetPeriod = period;
            apply.InHosNum = patientInfo.ID;
            apply.MediDoc.MainDoc.ID = loginPerson.ID;
            apply.MediDoc.MainDoc.Name = loginPerson.Name;
            if (deptId != null && deptId != "")
            {
                apply.OperDeptId = deptId;                
                apply.OperDeptName = deptName;
            }
            else
            {
                apply.OperDeptId = loginPerson.Dept.ID;
                apply.OperDeptName = loginPerson.Dept.Name;
            }
            apply.OrgOrBlood = "B";
            apply.PatientName = patientInfo.Name;
            string tumorPro = "";
            if (chkFirst.Checked) tumorPro = Convert.ToInt32(Constant.TumorPro.ԭ����).ToString();
            if (chkSecond.Checked) tumorPro += Convert.ToInt32(Constant.TumorPro.������).ToString();
            if (chkTransfer.Checked) tumorPro += Convert.ToInt32(Constant.TumorPro.ת�ư�).ToString();
            if (chkOther.Checked) tumorPro += Convert.ToInt32(Constant.TumorPro.����).ToString();
            if (tumorPro != null && tumorPro.Length > 2)
            {
                MessageBox.Show("�걾���Բ���ͬʱ����2�����ϣ�");
                return 0;
            }
            apply.OperTime = Convert.ToDateTime(DateTime.Now.AddDays(1.0).Date.ToString().Replace("0:00:00", "8:00:00"));// + " 08:00:00");          
            return 1;
        }

        /// <summary>
        /// �������뵥
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            try
            {                          
                apply.OperApplyId = applyManage.GetSequence();
                apply.OperId = apply.OperApplyId.ToString().PadLeft(12, '0');
                if (applyManage.InsertOperApply(apply) <= 0)
                {                   
                    return -1;
                }
                if (applyManage.UpdateDiagInMain(patientInfo.ID, apply.MainDiaName) == -1)
                {
                    return -1;
                }
                return 1;
            }
            catch
            {
                return -1;
            }            
        }

        //�鿴�Ƿ��б�Ҫ��ȡѪ�걾
        public bool IsGetBld()
        {            
            diagnose = applyManage.GetDiaFromInMain(patientInfo.ID);
            string returnValue = applyManage.ExecSqlReturnOne("select sort_id from COM_DICTIONARY where type = 'DiagnosebyNurse' and NAME = '" + diagnose + "'");
            if (returnValue == "0")
            {
                return false;
            }
            return true;
        }

        private void frmBloodInfo_Load(object sender, EventArgs e)
        {          
           if (canCancel)
           {
               btn_Cancel.Visible = true;
               lblCancel.Visible = true;
           }
           diagnose = applyManage.GetDiaFromInMain(patientInfo.ID);
           
           ArrayList arrDiagnoseList = conMgr.GetListbyDept("DiagnosebyNurse", patientInfo.PVisit.PatientLocation.Dept.ID);
           if (arrDiagnoseList != null && arrDiagnoseList.Count > 0)
           {
               txtDiagnose.AddItems(arrDiagnoseList);
           }

           if (diagnose != null && diagnose.Trim() != "")
           {
               txtDiagnose.Text = diagnose;
           }
        }

        private void chkNone_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNone.Checked)
            {
                chkMed.Checked = false;
                chkRad.Checked = false;
                chkOther.Checked = false;
                chkOperation.Checked = false;
            }
        }

        private void chkRad_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRad.Checked)
            {
                chkNone.Checked = false;
                pnRad.Visible = true;
            }
            if (!chkRad.Checked)
            {
                pnRad.Visible = false;
            }
        }

        private void chkMed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMed.Checked)
            {
                chkNone.Checked = false;
                pnMed.Visible = true;
            }
            if (!chkMed.Checked)
            {
                pnMed.Visible = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //���û�б������ݣ�����
            if (!saved && apply.OperApplyId > 0)
            {
                if (SetValue() == 0)
                {
                    return;
                }
                applyManage.UpdateOperApply1(apply);                
                this.Close();
            }
            if (SetValue() == 0)
            {
                return ;
            }
            //int result = Save();
            //if (result == 0)
            //{
            //    return;
            //}
            //if (result == -1)
            //{
            //    MessageBox.Show("����ʧ��");
            //    return;
            //}
            //if (result == 1)
            //{
            //    MessageBox.Show("�����ɹ�");                
            //}
            //saved = true;
            this.Close();
        }

        private void frmBloodInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!saved)
            //{
            //    int result = Save();
            //    if (result == 0)
            //    {
            //        return;
            //    }
            //    if (result == -1)
            //    {
            //        MessageBox.Show("����ʧ��");
            //        return;
            //    }
            //    if (result == 1)
            //    {
            //        MessageBox.Show("�����ɹ�");
            //        return;
            //    }
            //}           
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            saved = true;
            canceled = true;
            this.Close();
        }

        private void chkOperation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOperation.Checked)
            {
                chkNone.Checked = false;
                pnOper.Visible = true;
            }
            if (!chkOperation.Checked)
            {
                pnOper.Visible = false;
            }
        }
    }
}