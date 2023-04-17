using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse.Controls
{
    /// <summary>
    /// [��������: ��Ժ�Ǽ����]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPatientOut : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientOut()
        {
            InitializeComponent();
        }

        private void ucPatientOut_Load(object sender, EventArgs e)
        {

        }

        #region ����
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

        //FS.HISFC.Models.RADT.PatientInfo PatientInfo = null;
        FS.HISFC.Models.Registration.Register PatientInfo = null;
        //�Һ��м��
        FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {
            try
            {
                this.cmbZg.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ZG));
                //��Ժ�Ǽǵ�ʱ��Ĭ��Ϊϵͳʱ��
                this.dtOutDate.Value = this.dataManager.GetDateTimeFromSysDateTime();
            }
            catch { }

        }


        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="PatientInfo"></param>
       
        ///<summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(FS.HISFC.Models.Registration.Register patientInfo)
        {

            //this.txtPatientNo.Text = patientInfo.PID.PatientNO;		//סԺ��
            this.txtPatientNo.Text =patientInfo.ID;
            this.txtCard.Text = patientInfo.PID.CardNO;	//���￨��
            this.txtPatientNo.Tag = patientInfo.ID;				//סԺ��ˮ��
            this.txtName.Text = patientInfo.Name;						//����
            this.txtSex.Text = patientInfo.Sex.Name;					//�Ա�
            this.txtIndate.Text = patientInfo.PVisit.InTime.ToString();	//��Ժ����
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;	//��������
            this.txtDept.Tag = patientInfo.PVisit.PatientLocation.Dept.ID;	//���ұ���

            FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND));
            this.txtBalKind.Text = helper.GetName(patientInfo.Pact.PayKind.ID);


            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;	//����
            //this.txtFreePay.Text = patientInfo.FT.LeftCost.ToString();		//ʣ��Ԥ����
            //this.txtTotcost.Text = patientInfo.FT.TotCost.ToString();		//�ܽ��
            this.cmbZg.Tag = patientInfo.PVisit.ZG;				//ת��
            this.dtOutDate.Value = patientInfo.PVisit.OutTime;				//��Ժ����
        }


        /// <summary>
        /// �ӿؼ���ó�Ժ�Ǽ���Ϣ
        /// </summary>
        private void GetOutInfo()
        {
            PatientInfo.PVisit.ZG.ID = this.cmbZg.Tag.ToString();
            PatientInfo.PVisit.ZG.Name = this.cmbZg.Text;
            PatientInfo.PVisit.PreOutTime = this.dtOutDate.Value;
            PatientInfo.PVisit.OutTime = this.dtOutDate.Value;
        }


        /// <summary>
        ///����
        /// </summary>
        private void ClearPatintInfo()
        {
            this.cmbZg.Text = "";
            this.cmbZg.Tag = "";
            this.dtOutDate.Value = this.dataManager.GetDateTimeFromSysDateTime();
        }


       

        string Err = "";
        /// <summary>
        /// ��дУ����
        /// </summary>
        /// <param name="Inpatient_no"></param>
        /// <returns></returns>
        public virtual int Valid(string Inpatient_no)
        {
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            if (this.Valid(this.PatientInfo.ID) < 0)
            {
                return -1;
            }
            //������߲��ǵ����Ժ��ʾ
            if (this.dtOutDate.Value.Date != this.dataManager.GetDateTimeFromSysDateTime().Date)
            {
                DialogResult dr = MessageBox.Show("�û��ߵ����۳�Ժ�����ǣ� " +
                    this.dtOutDate.Value.ToString("yyyy��MM��dd��") + "  �Ƿ������", "��ʾ",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                if (dr == DialogResult.No)
                {
                    this.Err = "";
                    return -1;
                }
            }
            //ȡ�������µ�סԺ������Ϣ

            ArrayList alPatientlist = new ArrayList();
            alPatientlist = this.registerIntegrate.QueryPatient(this.PatientInfo.ID);
            PatientInfo = alPatientlist[0] as FS.HISFC.Models.Registration.Register;
            if (PatientInfo == null)
            {
                this.Err = this.registerIntegrate.Err;
                return -1;
            }
            this.Err = "";

            #region {BD72C9FF-2F8D-46f3-8EE6-3AE410A4A459}
            //�������۲���Ҫ�жϻ��߿��ҺͲ���---sunm
            //��������Ѳ��ڱ���,���������---������ת�ƺ�,���������û�йر�,����ִ������
            //if (PatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            //{
            //    this.Err = "�����Ѳ��ڱ�����,��ˢ�µ�ǰ����";
            //    return -1;
            //}

            #endregion

            //���������Ժ״̬�����仯,���������
            if (PatientInfo.PVisit.InState.ID.ToString() != "P")
            {
                this.Err = "������Ϣ�ѷ����仯,��ˢ�µ�ǰ����";
                return -1;
            }

            //ȡ��Ժ�Ǽ���Ϣ
            this.GetOutInfo();

            //��Ժ�Ǽ�

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(inpatient.con);
            //t.BeginTransaction();

            this.radt.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int i =this.radt.OutPatient(PatientInfo);
            this.Err = radt.Err;
            if( i== -1)��//ʧ��
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            else if (i == 0)//ȡ��
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "";
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            base.OnRefreshTree();//ˢ����
            return 1;
        }
        #endregion

        #region �¼�
        private void button1_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            if (this.Save() > 0)//�ɹ�
            {
                MessageBox.Show(Err);
                base.OnRefreshTree();
                ((Control)sender).Enabled = true;
                return;
            }
            else
            {
                if (Err != "") MessageBox.Show(Err);
            }
            ((Control)sender).Enabled = true;

        }
        string strInpatientNo;

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            //this.strInpatientNo = (neuObject as FS.FrameWork.Models.NeuObject).ID;
            FS.HISFC.Models.Registration.Register register = e.Tag as FS.HISFC.Models.Registration.Register;
            if ( register != null  )
            {
                try
                {
                    this.PatientInfo = register;
                    this.SetPatientInfo(register);  
                }
                catch (Exception ex) { this.Err = ex.Message; }

            }
            return 0;
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return null;
        }

        #endregion

    }
}
