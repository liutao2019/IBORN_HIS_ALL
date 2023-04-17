using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Print
{

    public partial class ucBColForm : Form
    {
        protected FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInPatientNo;
        //private BarCodeManage barCodeMange;
        //private SpecSourceManage sourceManage;
        private DisTypeManage disTypeManage;
        private OperApplyManage operApplyManage;
        //private PatientManage patientManage;       
        private Dictionary<string, string> dicBarCode;
        private OperApply operApply;
        public delegate void ReLoad(object sender, EventArgs e);
        public event ReLoad OnReLoad;

        public ucBColForm()
        {
            InitializeComponent();
            ucQueryInPatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            //barCodeMange = new BarCodeManage();
            //sourceManage = new SpecSourceManage();
            disTypeManage = new DisTypeManage();
            operApplyManage = new OperApplyManage();
            operApply = new OperApply();
            //patientManage = new PatientManage();
            dicBarCode = new Dictionary<string, string>();
        }

        private void ClearContent()
        {
            cmbDept.Text = "";            
            cmbSendDoc.Text = "";           
        }

        private bool ValidateContent()
        {
            if (cmbDept.Tag == null || cmbDept.Text == "")
            {
                MessageBox.Show("送存科室不能为空");
                return false;
            }
            if (cmbSendDoc.Tag == null || cmbSendDoc.Text == "")
            {
                MessageBox.Show("送存医生不能为空");
                return false;
            }
            return true;
        }

        private void GetApplyInfo()
        {
            operApply.MediDoc.MainDoc.ID = cmbSendDoc.Tag.ToString();
            operApply.MediDoc.MainDoc.Name = cmbSendDoc.Text;
            
            operApply.OperApplyId = operApplyManage.GetSequence();
            operApply.OperDeptId = cmbDept.Tag.ToString();
            operApply.OperDeptName = cmbDept.Text;
            
            operApply.OperId = operApply.OperApplyId.ToString().PadLeft(12, '0');
            operApply.OrgOrBlood = "B";            
        }

        /// <summary>
        /// 绑定病种类型
        /// </summary>
        //private void DisTypeBinding()
        //{
        //    Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
        //    if (dicDisType.Count > 0)
        //    {
        //        BindingSource bs = new BindingSource();
        //        bs.DataSource = dicDisType;
        //        cmbDisType.DisplayMember = "Value";
        //        cmbDisType.ValueMember = "Key";
        //        cmbDisType.DataSource = bs;
        //    }
        //} 

        /// <summary>
        ///  查询HIS中病人信息
        /// </summary>
        private void PatientInfoMyEvent()
        {          
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

            ArrayList arrPatient = new ArrayList();             
            arrPatient = ucQueryInPatientNo.InpatientNos;
            
            if (arrPatient == null || arrPatient.Count <= 0)
            {
                MessageBox.Show("获取病人信息失败！", "条码生成");
                return;
            }
            int inNum = 0;
            string inHosNum = "";
            foreach (FS.FrameWork.Models.NeuObject o in arrPatient)
            {                
                string sub = o.ID.Substring(2, 2);
                int tmpNum = Convert.ToInt32(sub);
                if (tmpNum > inNum)
                {
                    inNum = tmpNum;
                    inHosNum = o.ID;                     
                }
            }           
            patientInfo = radtIntegrate.GetPatientInfomation(inHosNum);
            if (patientInfo == null)
            {            
                MessageBox.Show("获取病人信息失败！", "条码生成");
                return;
            }           
            operApply.PatientName = patientInfo.Name;
            operApply.InHosNum = patientInfo.ID;
            Save();
            lblType.Text = "住院流水号:";
            //ConvertToSpecPatient(patientInfo);    
        }

        private void ucBColForm_Load(object sender, EventArgs e)
        {
            ucQueryInPatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.PatientInfoMyEvent);
            ucQueryInPatientNo.Name = "QueryPatientNo";
            
            Size size = new Size();
            size.Height = 50;
            size.Width = 200;
            ucQueryInPatientNo.Size = size;
            flpQueryInpatientNo.Controls.Add(ucQueryInPatientNo);
            flpQueryInpatientNo.Visible = true;
            //初始化送存部门
            FS.HISFC.Models.Base.Employee loginPerson = new FS.HISFC.Models.Base.Employee();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            ArrayList alDepts = new ArrayList();
            FS.HISFC.BizLogic.Manager.Department manager = new FS.HISFC.BizLogic.Manager.Department();
            alDepts = manager.GetDeptmentByType("I");//.GetMultiDept(loginPerson.ID);
            this.cmbDept.AddItems(alDepts);
            this.cmbDept.Tag = loginPerson.Dept.ID;

            //初始化送存医生
            FS.HISFC.BizLogic.Manager.Person personList = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList arrPerson = personList.GetEmployeeAll();// GetEmployee(FS.HISFC.Object.Base.EnumEmployeeType.D);
            cmbSendDoc.AddItems(arrPerson);
            this.cmbSendDoc.Tag = "";
            this.cmbSendDoc.Text = "";
            //this.cmbSendDoc.Tag = loginPerson.ID;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(650, 600);
        }

        private void Save()
        {
            if (!ValidateContent())
            {
                return;
            }
            GetApplyInfo();
            if (operApplyManage.InsertOperApply(operApply) == -1)
            {
                MessageBox.Show("保存失败");
                return;
            }
            MessageBox.Show("保存成功");
            lblBarCode.Text = operApply.OperId;
            lblName.Text = operApply.PatientName;
            lblSpecNum.Text = operApply.InHosNum;
            operApply = new OperApply();
            ClearContent();
        }

        private void ucBColForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //OnSetContainerId(this, new EventArgs());
                this.OnReLoad(this,new EventArgs());
            }
            catch
            { }
        }

        private void cmbDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbSendDoc.Focus();
            }
        }

        private void cmbSendDoc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    flpQueryInpatientNo.Controls[0].Focus();
                }
            }
            catch
            { }
        }
        private void p_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    cmbDept.Focus();
                }
            }
            catch
            { }
        }

        private void ucBColForm_Shown(object sender, EventArgs e)
        {
            cmbDept.Focus();
            cmbSendDoc_KeyDown(sender, new KeyEventArgs(Keys.Space));
            
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                patientInfo = radtIntegrate.QueryComPatientInfo(txtCardNo.Text.Trim().PadLeft(10, '0'));
                if (patientInfo == null)
                {
                    MessageBox.Show("获取病人信息失败！", "条码生成");
                    return;
                }
                operApply.PatientName = patientInfo.Name;
                operApply.InHosNum = patientInfo.PID.CardNO;
                lblType.Text = "门诊号：";
                Save();               
            }
        }
    }
}
