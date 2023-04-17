using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: סԺ����ת����]<br></br>
    /// [�� �� ��: Ѧ�Ľ�]<br></br>
    /// [����ʱ��: 2010-3-29]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucInpatientToOutpatient : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region "����"

        /// <summary>
        /// ���߻�����Ϣ�ۺ�ʵ��

        /// </summary>
        Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ���תintegrate��

        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ���תmanager��

        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Manager radtManger = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// סԺ����ҵ���

        /// </summary>
        //Neusoft.HISFC.BizLogic.Fee.InPatient feeInpatient = new Neusoft.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// סԺ�������ҵ���

        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// toolBarService
        /// </summary>
        protected Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        private Neusoft.HISFC.BizProcess.Integrate.Manager conMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();


        private Neusoft.HISFC.BizProcess.Interface.IHE.IADT adt = null;
        #endregion

        public ucInpatientToOutpatient()
        {
            InitializeComponent();
        }

        #region ����
        protected override int OnSave(object sender, object neuObject)
        {
            if (adt == null)
            {
                adt = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IHE.IADT)) as Neusoft.HISFC.BizProcess.Interface.IHE.IADT;
            }

            if (adt == null)
            {
                return -1;
            }

            Neusoft.HISFC.BizProcess.Integrate.RADT radtBll = new Neusoft.HISFC.BizProcess.Integrate.RADT();
            Neusoft.HISFC.BizProcess.Integrate.Registration.Registration regBll = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();


            Neusoft.HISFC.Models.RADT.PatientInfo patient = radtBll.GetPatientInfomation(this.patientInfo.ID);
            Neusoft.HISFC.Models.Registration.Register r = adt.ProduceInpatientToOutPatientMessage(this.patientInfo);

            if (r == null)
            {
                MessageBox.Show("���ɹҺ�ʵ�����!");
                return -1;
            }
            int resultValue = radtBll.UnregisterNoFee(patient);
            if (resultValue != 1)
            {

                throw new Exception(radtBll.Err);
            }
            if (regBll.Insert(r) == -1)
            {

                throw new Exception(regBll.Err);
            }

            adt.InPatientToOutpatient(this.patientInfo);
            return base.OnSave(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Init();
        }

        private void Init()
        {
            ArrayList alDept;
            alDept = this.GetClinicDepts();
            this.cmbDept.AddItems(alDept);
        }



        /// <summary>
        /// ��Ч���ж�

        /// </summary>
        private bool IsValid()
        {
            if (this.ucQueryInpatientNo.InpatientNo == null || this.ucQueryInpatientNo.InpatientNo.Trim() == "")
            {
                //if (this.ucQueryInpatientNo.Err == "")
                //{
                //    ucQueryInpatientNo.Err = "�˻��߲���Ժ!";
                //}
                //Neusoft.FrameWork.WinForms.Controls.Classes.Function.Msg(this.ucQueryInpatientNo.Err, 111);

                //this.ucQueryInpatientNo.Focus();
                //return false;


            }

            //��ȡסԺ�Ÿ�ֵ��ʵ��
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo.InpatientNo);

            if (this.patientInfo == null)
            {
                MessageBox.Show(this.radtIntegrate.Err);
                this.ucQueryInpatientNo.Focus();

                return false;
            }
            //////////////////////////////////////////////////////////////////////////
            // Robin Add
            //////////////////////////////////////////////////////////////////////////
            this.patientInfo.PVisit.ICULocation.Dept.ID = this.patientInfo.PVisit.PatientLocation.Dept.ID;
            this.patientInfo.PVisit.ICULocation.Dept.Name = this.patientInfo.PVisit.PatientLocation.Dept.Name;
            //////////////////////////////////////////////////////////////////////////
            //�ؼ���ֵ������Ϣ


            this.EvaluteByPatientInfo(this.patientInfo);

            //�жϸû����Ƿ��Ѿ���Ժ

            //if (this.patientInfo.PVisit.InState.ID.ToString() == Neusoft.HISFC.Models.Base.EnumInState.N.ToString() || this.patientInfo.PVisit.InState.ID.ToString() == Neusoft.HISFC.Models.Base.EnumInState.O.ToString())
            //{
            //    Neusoft.FrameWork.WinForms.Controls.Classes.Function.Msg("�û����Ѿ���Ժ!", 111);
            //    this.patientInfo.ID = null;
            //    this.ucQueryInpatientNo.Focus();

            //    return false;
            //}

            //�ж�ԤԼ��

            //if (this.patientInfo.FT.PrepayCost > 0)
            //{
            //    Neusoft.FrameWork.WinForms.Controls.Classes.Function.Msg("Ԥ��������0,�����޷���Ժ", 111);
            //    this.ucQueryInpatientNo.Focus();

            //    return false;
            //}

            //�жϷ����ܶ� �����Ƿ�������|| feeInpatient.QueryBalancesByInpatientNO(this.patientInfo.ID).Count != 0
            //if (this.patientInfo.FT.TotCost > 0 || this.patientInfo.FT.BalancedCost > 0)
            //{
            //    Neusoft.FrameWork.WinForms.Controls.Classes.Function.Msg("�ѷ������û�����¼,�����޷���Ժ", 111);
            //    this.ucQueryInpatientNo.Focus();

            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// �����Ϣ
        /// </summary>
        protected virtual void Clear()
        {
            this.patientInfo = null;
            this.txtName.Text = "";
            this.txtDept.Text = "";
            this.txtPact.Text = "";
            this.txtBedNo.Text = "";

            txtDateIn.Text = "";
            txtDoctor.Text = "";

        }

        /// <summary>
        /// ���û�����Ϣʵ����пؼ���ֵ

        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣʵ��</param>
        protected virtual void EvaluteByPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
            }
            // ����
            this.txtName.Text = patientInfo.Name;
            // ����
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            // ��ͬ��λ
            this.txtPact.Text = patientInfo.Pact.Name;
            //����
            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;

            //��Ժ����
            txtDateIn.Text = patientInfo.PVisit.InTime.ToString();
            // ҽ��
            txtDoctor.Text = patientInfo.PVisit.AdmittingDoctor.Name;
            //סԺ��

            this.ucQueryInpatientNo.TextBox.Text = patientInfo.PID.PatientNO;



        }

        /// <summary>
        ///����ToolBar�ؼ�
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="neuObject"></param>
        ///<param name="param"></param>
        ///<returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("��Ժ", "�����޷���Ժ", Neusoft.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);
            toolBarService.AddToolButton("����", "����", Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("����", "�򿪰����ļ�", Neusoft.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("�˳�", "�رյǼǴ���", Neusoft.FrameWork.WinForms.Classes.EnumImageList.T�˳�, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// ����toolbar��ťclick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "��Ժ":

                    this.Confirm();
                    break;

                case "����":
                    this.Clear();
                    this.ucQueryInpatientNo.Text = "";
                    this.ucQueryInpatientNo.Focus();
                    break;

                case "����":

                    break;
                case "�˳�":

                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }


        /// <summary>
        /// �޷���Ժ

        /// </summary>
        protected virtual void Confirm()
        {
            //��Ч���ж�

            if (!IsValid())
            {
                return;
            }

            //��������
            long returnValue = this.medcareInterfaceProxy.SetPactCode(this.patientInfo.Pact.ID);
            if (returnValue != 1)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�����ӿ�ȡ��ͬ��λʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return;
            }
            //Neusoft.FrameWork.Management.Transaction t  = new Transaction(this.feeInpatient.Connection);
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            radtIntegrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            if (radtIntegrate.UnregisterNoFee(this.patientInfo) != 1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();

                Neusoft.FrameWork.WinForms.Classes.Function.Msg(this.radtManger.Err, 211);

                return;
            }
            this.medcareInterfaceProxy.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);


            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return;
            }
            returnValue = this.medcareInterfaceProxy.CancelRegInfoInpatient(this.patientInfo);
            if (returnValue != 1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�����ӿ��޷���Ժʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return;
            }

            this.medcareInterfaceProxy.Commit();
            //{009FC822-DE2B-45ac-BEB7-E49F24B1605F}
            this.medcareInterfaceProxy.Disconnect();
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            this.patientInfo.ID = "";
            this.ucQueryInpatientNo.txtInputCode.Text = "";
            Neusoft.FrameWork.WinForms.Classes.Function.Msg("�޷���Ժ�ɹ�", 111);
        }

        private ArrayList GetClinicDepts()
        {
            ArrayList al = this.conMgr.QueryRegDepartment();
            if (al == null)
            {
                MessageBox.Show("��ȡ�������ʱ����!" + this.conMgr.Err, "��ʾ");
                return null;
            }

            return al;
        }
        #endregion

        #region �¼�
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //����ICULocation����
            this.patientInfo.PVisit.PatientLocation.Dept.ID = this.cmbDept.Tag.ToString();
            this.patientInfo.PVisit.PatientLocation.Dept.Name = this.cmbDept.Text;
        }

        /// <summary>
        /// �õ�������Ϣʵ��
        /// </summary>
        private void ucQueryInpatientNo_myEvent()
        {
            //���
            this.Clear();

            //��Ч���ж�


            if (!IsValid())
            {
                return;
            }
        }
        #endregion

    }
}
