using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//{28C63B3A-9C64-4010-891D-46F846EA093D}
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: ȡ������]<br></br>
    /// [�� �� ��: cao-lin]<br></br>
    /// [����ʱ��: 2013-09-23]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCancelArrive: FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucCancelArrive()
        {
            InitializeComponent();
        }

        private void ucPatientOut_Load(object sender, EventArgs e)
        {

        }

        #region ����

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.BizLogic.RADT.InPatient inpatient = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;

        /// <summary>
        /// ����������{28C63B3A-9C64-4010-891D-46F846EA093D}
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ��ǰ��½��Ա��������Ϣ���ݣ�
        /// </summary>
        private FS.HISFC.Models.Base.Employee Oper = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        string Err = "";

        /// <summary>
        /// ��ǰ����סԺ��ˮ��
        /// </summary>
        string strInpatientNo;

        #endregion


        #region ����

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {
            try
            {
                this.Oper = manager.GetEmployeeInfo(this.inpatient.Operator.ID);
                if (this.Oper == null)
                {
                    MessageBox.Show("��ȡ��Ա������Ϣ����:" + manager.Err);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtPatientNo.Text = patientInfo.PID.PatientNO;		//���˺�
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
            this.txtFreePay.Text = patientInfo.FT.LeftCost.ToString();		//ʣ��Ԥ����
            this.txtTotcost.Text = patientInfo.FT.TotCost.ToString();		//�ܽ��
            this.dtpInDate.Value = patientInfo.PVisit.InTime; //��Ժʱ��
        }

     
        /// <summary>
        /// ˢ�»�����Ϣ
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void RefreshList(string inPatientNo)
        {
            try
            {
                myPatientInfo = this.inpatient.QueryPatientInfoByInpatientNO(inPatientNo);

                //��������Ѳ��ڱ���,���������
                if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
                {
                    MessageBox.Show("�����Ѳ��ڱ�����,��ˢ�µ�ǰ����", "��ʾ");
                    myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                }

                this.SetPatientInfo(myPatientInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {

            if (this.CheckPatientState() != 1)
            {
                MessageBox.Show(Err);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            #region ��ʿȡ������

            //��մ�λ:�մ�,���˺�ΪN
            FS.HISFC.Models.Base.Bed newBed = myPatientInfo.PVisit.PatientLocation.Bed.Clone();

            newBed.Status.ID = FS.HISFC.Models.Base.EnumBedStatus.U.ToString();

            newBed.InpatientNO = "N";

            //���´�λ״̬,���жϲ���
            int i = inpatient.UpdateBedStatus(newBed, myPatientInfo.PVisit.PatientLocation.Bed);

            this.Err = inpatient.Err;

            if (i <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show(Err);

                return i;
            }

            //���»���״̬
            FS.HISFC.Models.RADT.InStateEnumService inState = new FS.HISFC.Models.RADT.InStateEnumService();

            inState.ID = FS.HISFC.Models.Base.EnumInState.R.ToString();

            i = radt.UpdatePatientState(myPatientInfo, inState);

            this.Err = radt.Err;

            if (i == -1)��//ʧ��
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show(Err);

                return -1;
            }


            string sql = @"UPDATE fin_ipr_inmaininfo
   set bed_no          = null,
       CHARGE_DOC_CODE = null,
       CHARGE_DOC_NAME = null,
       CHIEF_DOC_CODE  = null,
       CHIEF_DOC_NAME  = null,
       HOUSE_DOC_CODE  = null,
       HOUSE_DOC_NAME  = null,
       DUTY_NURSE_CODE = null,
       DUTY_NURSE_NAME = null,
       --nurse_cell_code = null,
       --nurse_cell_name = null,
       prefixfee_date  = null,
       out_date        = null,
       TEND            = null,
       DIETETIC_MARK   = null
 WHERE inpatient_no = '{0}'
";

            sql = string.Format(sql, myPatientInfo.ID);
            i = inpatient.ExecNoQuery(sql);
            if (i == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show("��������ʧ�ܣ�δ�ҵ�������Ϣ��\r\n" + inpatient.Err);

                return -1;
            }
            else if (i < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show("��������ʧ�ܣ�\r\n" + inpatient.Err);

                return -1;
            }

            //�����¼����
            i = inpatient.SetShiftData(myPatientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.CK, "ȡ������", myPatientInfo.PVisit.PatientLocation.Dept, myPatientInfo.PVisit.PatientLocation.NurseCell, myPatientInfo.IsBaby);

            if (i <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show(Err);

                return -1;
            }
           

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("ȡ������ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            #endregion

            
            return 1;
        }

        /// <summary>
        /// ��黼��״̬
        /// </summary>
        /// <returns></returns>
        private int CheckPatientState()
        {
            #region �жϻ���״̬
            //��ȡ���ݿ���Ϣ
            //ȡ�������µ�סԺ������Ϣ
            myPatientInfo = this.inpatient.QueryPatientInfoByInpatientNO(this.myPatientInfo.ID);
            if (myPatientInfo == null)
            {
                this.Err = this.inpatient.Err;
                return -1;
            }
            this.Err = "";

            //��������Ѳ��ڱ���,���������---������ת�ƺ�,���������û�йر�,����ִ������
            if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            {
                this.Err = "�����Ѳ��ڱ�����,��ˢ�µ�ǰ����";
                return -1;
            }

            //���������Ժ״̬�����仯,���������
            if (myPatientInfo.PVisit.InState.ID.ToString() != "I")
            {
                this.Err = "������Ϣ�ѷ����仯,��ˢ�µ�ǰ����";
                return -1;
            }

            #endregion

            //#region �ж����
            ////// {5EE0CCC3-9A2B-4039-AA59-F779D222E3AD}  ȡ�������ж���Ч����ж�
            ////ArrayList allDiagNose = diagMgr.QueryDiagenoseByPateintID(myPatientInfo.ID, FS.HISFC.Models.Base.ServiceTypes.I);
            ////ArrayList allDiagNose = diagMgr.QueryDiagenoseByPateintIDSZ(myPatientInfo.ID, FS.HISFC.Models.Base.ServiceTypes.I);
            ////ArrayList allDiagNose = diagMgr.QueryCaseDiagnoseForClinicSI(myPatientInfo.ID, FS.HISFC.Models.Base.ServiceTypes.I);
            //if (allDiagNose.Count > 0)
            //{
            //    this.Err = "�û����Ѿ��¹���ϣ�������ȡ�������֪ͨҽ�������Ժ";
            //    return -1;
            //}
            //#endregion

            #region �ж�ҽ��

            int count = this.GetOrderCount(myPatientInfo.ID);

            if (count > 0)
            {
                this.Err = "�û����Ѿ��¹�ҽ����������ȡ�������֪ͨҽ�������Ժ";
                return -1;
            }

            #endregion

            #region �жϷ���

            if (myPatientInfo.FT.TotCost > 0)
            {
                this.Err = "�û����Ѿ��з��÷�����������ȡ������";
                return -1;
            }

            #endregion

            #region �ж�����

            count = this.GetOpsCount(myPatientInfo.ID);

            if (count > 0)
            {
                this.Err = "�û����Ѿ����������룬������ȡ������";
                return -1;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// ��ȡδ��ɵ���������
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        private int GetOpsCount(string inPatientNO)
        {
            string sql = @"select count(*) from met_ops_apply f
                                                                    where f.clinic_code='{0}'
                                                                    and f.ynvalid='1'
                                                                        and f.execstatus!='4'
                                                                        and f.execstatus!='5'";
            sql = string.Format(sql, inPatientNO);

            string returnValue = diagMgr.ExecSqlReturnOne(sql, "0");

            return FS.FrameWork.Function.NConvert.ToInt32(returnValue);
        }

        /// <summary>
        /// ��ȡҽ����Ŀ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int GetOrderCount(string inpatientNo)
        {
            string strSql = @"  
  select count(*) from met_ipm_order a where a.inpatient_no = '{0}'";

            strSql = string.Format(strSql, inpatientNo);

            string returnValue = diagMgr.ExecSqlReturnOne(strSql, "0");

            return FS.FrameWork.Function.NConvert.ToInt32(returnValue);
        }


        #endregion

        #region �¼�

        private void button1_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            if (this.Save() > 0)//�ɹ�
            {
                base.OnRefreshTree();
                ((Control)sender).Enabled = true;
                return;
            }
            else
            {
                if (Err != "")
                {
                    MessageBox.Show(Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            ((Control)sender).Enabled = true;

        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.strInpatientNo = (neuObject as FS.FrameWork.Models.NeuObject).ID;
            if (this.strInpatientNo != null || this.strInpatientNo != "")
            {
                try
                {
                    RefreshList(strInpatientNo);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                }
            }
            return 0;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return null;
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion

    }
}
