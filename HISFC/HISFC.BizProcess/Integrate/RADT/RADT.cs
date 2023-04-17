using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FS.HISFC.BizLogic.RADT;
using System.Windows.Forms;
namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [��������: ���ϵ����ת������]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class RADT : IntegrateBase
    {
        #region ����

        /// <summary>
        /// ��������ҵ���
        /// </summary>
        protected OutPatient radtEmrManager = new OutPatient();       

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// ��λ����
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Bed managerBed = new FS.HISFC.BizLogic.Manager.Bed();

        /// <summary>
        /// ҽ������
        /// </summary>
        protected FS.HISFC.BizLogic.Order.Order managerOrder = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// ���ҹ���
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Department managerDepartment = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// ���תҵ���
        /// </summary>
        protected InPatient inPatienMgr = new InPatient();

        /// <summary>
        /// ������������
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.LifeCharacter lfchManagement = new FS.HISFC.BizLogic.RADT.LifeCharacter();

        /// <summary>
        /// ��λ��־����
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.InpatientDayReport dayReportMgr = new InpatientDayReport();

        #endregion


        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="trans">���ݿ�����</param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            inPatienMgr.SetTrans(trans);
            radtEmrManager.SetTrans(trans);
            inpatientManager.SetTrans(trans);
            managerBed.SetTrans(trans);
            managerDepartment.SetTrans(trans);
            managerOrder.SetTrans(trans);
            lfchManagement.SetTrans(trans);
            inPatienMgr.SetTrans(trans);
            this.trans = trans;
        }

        #region ����

        public int GetAutoPatientNO(ref string patientNO, ref bool isRecycle)
        {
            return this.inPatienMgr.GetAutoPatientNO(ref patientNO, ref isRecycle);
        }

        public int GetInputPatientNO(string patientNO, ref FS.HISFC.Models.RADT.PatientInfo patient)
        {
            string inpatientNO = this.inPatienMgr.GetMaxinPatientNOByPatientNO(patientNO);
            if (inpatientNO == string.Empty)
            {
                //û��סԺ��¼��˵�����ߵ�һ����Ժ
                patient.PatientNOType = FS.HISFC.Models.RADT.EnumPatientNOType.First;
                patient.PID.PatientNO = patientNO;
                patient.PID.CardNO = "T" + patientNO.Substring(1, 9);
                patient.ID = "T001";
                patient.InTimes = 1;
            }
            else
            {
                //����Ժ��¼����ѯ���߻�����Ϣ��
                patient = this.QueryPatientInfoByInpatientNO(inpatientNO);
                patient.PatientNOType = FS.HISFC.Models.RADT.EnumPatientNOType.Second;
                //�ж���Ժ״̬
                if (patient.PVisit.InState.ID.ToString() == "R" || patient.PVisit.InState.ID.ToString() == "I" 
                    || patient.PVisit.InState.ID.ToString() == "P" 
                    //|| patient.PVisit.InState.ID.ToString() == "B"
                    )
                {
                    //if (patient.PatientType.ID == "Y")// {5B3B503C-8CF5-415b-89EB-C11A4FEE8A19}
                    //{
                    //    this.Err = "�˻�������Ժ����!";
                    //    return -1;
                    //}
                    //else if (patient.PatientType.ID == "L" || patient.PatientType.ID == "P")
                    //{
                    //    this.Err = "�˻�������Ժ����!";

                    //    return -1;
                    //}
                }
                patient.ID = "T001";
                patient.InTimes = patient.InTimes + 1;
                patient.PVisit.PatientLocation.Bed.ID = "";
            }
            return 1;

        }

        public string GetNewInpatientNO()
        {
            return this.inPatienMgr.GetNewInpatientNO();
        }

        /// <summary>
        /// �Զ�����סԺ��
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        public int CreateAutoInpatientNO(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.AutoCreatePatientNO(patient) == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �Զ�����סԺ��
        /// </summary>
        /// <param name="patientNO">��ǰסԺ��</param>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        public int CreateAutoInpatientNO(string patientNO, ref FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.AutoCreatePatientNO(patientNO, ref patient) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ͨ�����￨����Ϣ������סԺ��ˮ��
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <returns>�ɹ�: ������סԺ��ˮ�� ʧ�� :null </returns>
        public string GetMaxPatientNOByCardNO(string cardNO) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.GetMaxPatientNOByCardNO(cardNO);
        }

        /// <summary>
        /// ����סԺ�Ż�ȡ������
        /// </summary>
        /// <param name="patientNO">סԺ��</param>
        /// <returns></returns>
        public int GetMaxInTimes(string patientNO)
        {
            //�������סԺ��
            this.SetDB(inPatienMgr);

            ArrayList al = inPatienMgr.QueryInpatientNOByPatientNO(patientNO, false);
            if (al == null)
            {
                return -1;
            }
            else if (al.Count <= 0)
            {
                return 0;
            }
            string  inpatientNO="";
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                ////�ų��޷���Ժ��
                //if ((FS.HISFC.Models.Base.EnumInState)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumInState), obj.Name) == FS.HISFC.Models.Base.EnumInState.N)
                //{
                //    continue;
                //}
                if (string.Compare(inpatientNO,obj.ID)<0)
                {
                    inpatientNO = obj.ID;
                }
            }
            FS.HISFC.Models.RADT.PatientInfo patient = inPatienMgr.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patient == null)
            {
                return -1;
            }
            else if (string.IsNullOrEmpty(patient.ID))
            {
                return 0;
            }

            return patient.InTimes;
        }

        /// <summary>
        /// ��ȡסԺ����
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns></returns>
        public int GetInDays(string inpatientNO)
        {
            //�Ȳ��ҽ���ʱ��
            this.SetDB(inPatienMgr);
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //0:�Ǽ�ʱ���� �����Ժ����1 
            //1:�Ǽ�ʱ���� �����Ժ��1
            //2:����ʱ���� �����Ժ����1 
            //3:����ʱ���� �����Ժ��1
            int inHosDayType = controlParam.GetControlParam<int>("ZY0004", false, 3);

            FS.HISFC.Models.RADT.PatientInfo patient = inPatienMgr.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patient == null)
            {
                return -1;
            }
            else if (string.IsNullOrEmpty(patient.ID))
            {
                return 0;
            }

            DateTime dtInTime = DateTime.MinValue;
            if (inHosDayType == 2 || inHosDayType == 3)
            {
                ArrayList al = inPatienMgr.QueryPatientShiftInfoNew(inpatientNO);
                if (al == null)
                {
                    return -1;
                }
                else if (al.Count <= 0)
                {
                    return 0;
                }

                foreach (FS.HISFC.Models.Invalid.CShiftData shiftData in al)
                {
                    //�ҵ����ڽ����K
                    if (shiftData.ShitType == "K")
                    {
                        dtInTime = FS.FrameWork.Function.NConvert.ToDateTime(shiftData.Memo);
                        break;
                    }
                }
            }
            else if (inHosDayType == 0 || inHosDayType == 1)
            {
                dtInTime = patient.PVisit.InTime;
            }

            //���С����Сʱ��
            //if (dtInTime <= DateTime.MinValue)
            //{
            //    dtInTime = patient.PVisit.InTime;
            //}
            if (dtInTime <= DateTime.MinValue)
            {
                return 0;
            }

            DateTime dtOutTime = patient.PVisit.PreOutTime;
            if (dtOutTime <= DateTime.MinValue)
            {
                dtOutTime = patient.PVisit.OutTime;
            }

            if (dtOutTime <= DateTime.MinValue)
            {
                dtOutTime = inPatienMgr.GetDateTimeFromSysDateTime();
            }

            if (dtOutTime.Date == dtInTime.Date)//������Ժ�����Ժ ��1��
            {
                return 1;
            }

            if (inHosDayType == 1 || inHosDayType == 3)
            {
                return (dtOutTime.Date - dtInTime.Date).Days + 1;
            }
            else
            {
                return (dtOutTime.Date - dtInTime.Date).Days;
            }
        }

        /// <summary>
        /// ��ȡ����ʱ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public DateTime GetArriveDate(string inpatientNO)
        {
            //�Ȳ��ҽ���ʱ��
            this.SetDB(inPatienMgr);

            ArrayList al = inPatienMgr.QueryPatientShiftInfoNew(inpatientNO);
            if (al == null)
            {
                return DateTime.MinValue;
            }
            else if (al.Count <= 0)
            {
                return DateTime.MinValue;
            }
            DateTime dtInTime = DateTime.MinValue;

            foreach (FS.HISFC.Models.Invalid.CShiftData shiftData in al)
            {
                //�ҵ����ڽ����K
                if (shiftData.ShitType == "K")
                {
                    return dtInTime = FS.FrameWork.Function.NConvert.ToDateTime(shiftData.Memo);
                }
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// ͨ�����￨����com_patientInfo�л�û��߻�����Ϣ
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <returns>�ɹ�:���߻�����Ϣ ʧ�� null</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfo(string cardNO) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryComPatientInfo(cardNO);
        }

        //{971E891B-4E05-42c9-8C7A-98E13996AA17}
        /// <summary>
        /// ͨ�����֤����com_patientInfo�л�û��߻�����Ϣ
        /// </summary>
        /// <param name="IDNO">���֤�ſ���</param>
        /// <returns>�ɹ�:���߻�����Ϣ ʧ�� null</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfoByIDNO(string IDNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryComPatientInfoByIDNO(IDNO);
        }

        /// <summary>
        /// ͨ�����֤����com_patientInfo�л�û��߻�����Ϣ
        /// </summary>
        /// <param name="IDNO">���֤�ſ���</param>
        /// <returns>�ɹ�:���߻�����Ϣ ʧ�� null</returns>
        public ArrayList QueryComPatientInfoListByIDNO(string IDNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryComPatientInfoListByIDNO(IDNO);
        }

        /// <summary>
        /// ͨ��סԺ����com_patientInfo�л�û��߻�����Ϣ
        /// </summary>
        /// <param name="IDNO">סԺ��</param>
        /// <returns>�ɹ�:���߻�����Ϣ ʧ�� null</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfoByPatientNo(string patientNo)
        {
            this.SetDB(inPatienMgr);

            string sqlWhere = @" where patient_no='{0}' and rownum=1 order by in_date desc";
            sqlWhere = string.Format(sqlWhere, patientNo);
            ArrayList arr = inPatienMgr.PatientInfoGet(sqlWhere);

            FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

            if (arr != null && arr.Count > 0)
            {
                patientInfo = inPatienMgr.QueryComPatientInfo((arr[0] as FS.HISFC.Models.RADT.PatientInfo).PID.CardNO);
            }

            return patientInfo;
        }

        /// <summary>
        /// ��û���Ӥ��
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <returns></returns>
        public ArrayList QueryBabiesByMother(string inpatientNO) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryBabiesByMother(inpatientNO);
        }

        /// <summary>
        /// ͨ��ҽ��������com_patientInfo�л�û��߻�����Ϣ
        /// </summary>
        /// <param name="cardNO">ҽ������</param>
        /// <returns>�ɹ�:���߻�����Ϣ ʧ�� null</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfoByMcardNO(string mcardNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryComPatientInfoByMcardNO(mcardNO);
        }

        /// <summary>
        /// �Զ�����סԺ��
        /// </summary>
        /// <param name="patientNO">��ǰסԺ��</param>
        /// <param name="usedPatientNO">ʹ���˵�סԺ��</param>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        public int CreateAutoInpatientNO(string patientNO, string usedPatientNO, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);
           
            if (inPatienMgr.AutoCreatePatientNO(patientNO, usedPatientNO, ref patient) == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���סԺ�š�סԺ�����ظ��������֤����סԺ�ŵ�Ψһ��
        ///  ��ѯ����סԺ�ţ�סԺ�������ظ����{4949C040-E8C9-49d9-9BC2-548F7892206B}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        public int VerifyInpatientInTimes(string inpatientno, string inTimes)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.VerifyInpatientInTimes(inpatientno, inTimes);
        }

        /// <summary>
        /// סԺ���ߵǼ�
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        public int RegisterPatient(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.InsertPatient(patient) == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���µǼ�ʱ���Ѫ���ɽ�͹������޶�����޶��ۼ�and�������յ��Ժ�and���޶����--By kuangyh
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        public int UpdateFeePatientInfoForRegister(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);
            if (inPatienMgr.UpdateOtherPatientInfoForRegister(patient) == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���»�����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int UpdatePatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.UpdatePatient(patient) <=0)
            {
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ����δʹ�õ�סԺ��Ϊʹ��״̬
        /// </summary>
        /// <param name="oldPatientNO">�ɵ�סԺ�ţ�δʹ�õ�</param>
        /// <returns>�ɹ� 1 ���� 0 Ӧ�����»�ȡסԺ�� ʧ��: -1</returns>
        public int UpdatePatientNOState(string oldPatientNO) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.UpdatePatientNoState(oldPatientNO) == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���뻼�߻����Ǽ���Ϣcom_patientinfo
        /// </summary>
        /// <param name="patient">��ǰ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ���� 0 Ӧ�����»�ȡסԺ�� ʧ��: -1</returns>
        public int RegisterComPatient(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.InsertPatientInfo(patient) == -1) 
            {
                if (this.DBErrCode == 1)
                {
                    if (inPatienMgr.UpdatePatientInfo(patient) <= 0)
                    {
                        return -1;
                    }
                }
                else 
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ���뻼�߻����Ǽ���Ϣcom_patientinfo--���쿨���ã�������벻�ɹ��򱨴�������
        /// </summary>
        /// <param name="patient">��ǰ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ���� 0 Ӧ�����»�ȡסԺ�� ʧ��: -1</returns>
        public int RegisterComPatientbyCreateCard(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.InsertPatientInfo(patient) == -1)
            {
                MessageBox.Show("���뻼����Ϣ����");
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���뻼�߱����Ϣ
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ���� 0  ʧ��: -1</returns>
        public int InsertShiftData(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.SetShiftData(patient.ID, FS.HISFC.Models.Base.EnumShiftType.B, "סԺ�Ǽ�", patient.PVisit.PatientLocation.Dept,
                patient.PVisit.PatientLocation.Dept, patient.IsBaby) <= 0) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���ݻ��߲�ѯ�����¼{28C63B3A-9C64-4010-891D-46F846EA093D}
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryPatientShiftInfoNew(string clinicNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientShiftInfoNew(clinicNO);
        }
        //{FA3B8CE6-0414-423a-A92D-33678E5FF193}
        /// <summary>
        /// ����ǼǼ����ﻼ�߱����Ϣ
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ���� 0  ʧ��: -1</returns>
        public int InsertRecievePatientShiftData(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            //�����Ϣ
            if (inPatienMgr.SetShiftData(patient.ID, FS.HISFC.Models.Base.EnumShiftType.K, "����", patient.PVisit.PatientLocation.NurseCell, patient.PVisit.PatientLocation.Bed, patient.IsBaby) < 0)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��������¼
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="shiftType">�������</param>
        /// <param name="shiftText">���˵��</param>
        /// <param name="oldShift">��ǰ״̬</param>
        /// <param name="newShift">��ǰ״̬</param>
        /// <returns>�ɹ� 1 ���� 0  ʧ��: -1</returns>
        public int InsertShiftData(string inpatientNO, FS.HISFC.Models.Base.EnumShiftType shiftType, string shiftText, FS.FrameWork.Models.NeuObject oldShift,
            FS.FrameWork.Models.NeuObject newShift)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.SetShiftData(inpatientNO, shiftType, shiftText, oldShift, newShift, false);
        }

        /// <summary>
        /// ���뵣����Ϣ,����ĵ��������Ѿ��ж�
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ���� 0 Ӧ�����»�ȡסԺ�� ʧ��: -1</returns>
        public int InsertSurty(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            if (patient.Surety.SuretyType.ID != null && patient.Surety.SuretyCost > 0 && patient.Surety.SuretyType.ID != string.Empty)
            {
                this.SetDB(inpatientManager);

                if (inpatientManager.InsertSurty(patient) <= 0)
                {
                    return -1;
                }

            }

            return 1;
        }

        /// <summary>
        /// ���»��߿���
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û�и��µ����� 0</returns>
        public int UpdatePatientDept(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientDeptByInpatientNo(patient);
        }
        //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
        /// <summary>
        /// ���»��߿���
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û�и��µ����� 0</returns>
        public int UpdatePatientNurse(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientNursCellByInpatientNo(patient);
        }

        /// <summary>
        /// ����סԺ�Ų�ѯ���߻�����Ϣ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns>�ɹ�: ���߻�����Ϣʵ�� ʧ�� null</returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation(string inpatientNO) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryPatientInfoByInpatientNO(inpatientNO);
        }


        /// <summary>
        /// ���ݿ��Ų�ѯ����סԺ��Ϣ  //{839B57B8-B74C-4818-9647-9881A0CE9013}
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns>�ɹ�:�Ų�ѯ����סԺ��Ϣʵ�� ʧ�� null</returns>
        public ArrayList GetPatientInfomationByCardNo(string cardno)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.GetPatientInfoByCardNOAndInState(cardno);
        }

        /// <summary>
        /// ����סԺ�Ų�ѯ���߻�����Ϣ�����Ӳ�ѯ��ʳ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns>�ɹ�: ���߻�����Ϣʵ�� ʧ�� null</returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfomationNew(string inpatientNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryPatientInfoByInpatientNONew(inpatientNO);
        }

        /// <summary>
        /// ��ѯһ��ʱ���ڵĵǼǻ���
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>�ɹ�: ���߼��� ʧ�� Null</returns>
        public ArrayList QueryPatientsByDateTime(DateTime beginTime, DateTime endTime) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryPatient(beginTime, endTime);
        }
        /// <summary>
		/// ���»�����Ϣ�����ǻ�������  ������com_patientinfo
		/// </summary>
		/// <param name="PatientInfo"></param>
		/// <returns></returns>
        public int UpdatePatientInfo(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientInfo(PatientInfo);
        }
        /// <summary>
		/// ���벡�˻�����Ϣ��-���ǻ������� ������com_patientinfo 
		/// </summary>
		/// <param name="PatientInfo">���߻�����Ϣ</param>
		/// <returns>�ɹ���־ 0 ʧ�ܣ�1 �ɹ�</returns>
		/// <returns></returns>
        public int InsertPatientInfo(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.InsertPatientInfo(PatientInfo);
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public string GetCardNOSequece()
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.GetCardNOSequece();
        }

        /// <summary>
        /// ���»��߲������
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <param name="CaseFlag">�������</param>
        /// <returns>1�ɹ�elseʧ��</returns>
        public int UpdatePatientCaseFlag(string InpatientNO, string CaseFlag)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdateCase(InpatientNO, CaseFlag);
        }
        #endregion

        #region ���߲�ѯ
        #region  ��ѯ���˻�����Ϣ com_patientinfo��
        [Obsolete("����,��QueryComPatientInfo����")]
        public FS.HISFC.Models.RADT.PatientInfo GetPatient(string CardNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.GetPatient(CardNO);
        }
        #endregion 
        /// <summary>
        /// ��ѯ���һ���
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatient( string deptCode, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);

            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.PatientQuery(deptCode, istate);

        }

        /// <summary>
        /// ���ݲ�������ͻ���״̬��ѯ��������
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatientBasicByNurseCell(string nurseCode, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);

            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.QueryPatientBasicByNurseCell(nurseCode, istate);
        }

         /// <summary>
        /// ���߲�ѯ-��ѯҽ���鲻ͬ״̬�Ļ���//{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
        /// </summary>
        /// <param name="medicalTeamCode">���ұ���</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList PatientQueryByMedicalTeam(string medicalTeamCode, FS.HISFC.Models.Base.EnumInState inState, string deptCode)
        {
            this.SetDB(radtEmrManager);
            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.PatientQueryByMedicalTeam(medicalTeamCode, istate,deptCode);
        }

        /// <summary>
        /// ����״̬��ѯ����
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatient( FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);

            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.QueryPatientBasicByInState(istate);

        }
        /// <summary>
        /// ���ݲ���״̬��ѯ����
        /// </summary>
        /// <param name="nurseCellID">��������</param>
        /// <param name="inState">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList QueryPatientByNurseCellAndState(string nurseCellID,FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByNurseCell(nurseCellID,inState);

        }
        /// <summary>
        /// ���ݲ�������״̬��ѯ����
        /// </summary>
        /// <param name="nurseCellID"></param>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByNurseCellAndDept(string nurseCellID, string deptCode,FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByNurseCellAndDept(nurseCellID,deptCode,inState);

        }
                 
        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(DateTime beginTime,DateTime endTime, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);

            
            return inPatienMgr.QueryPatientInfoByTimeInState(beginTime, endTime, inState.ToString());

        }
        /// <summary>
        /// ���ҽ���Ļ���
        /// </summary>
        /// <param name="objDoc"></param>
        /// <param name="inState"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByHouseDoc(FS.FrameWork.Models.NeuObject objDoc,FS.HISFC.Models.Base.EnumInState inState,string deptCode)
        {
             this.SetDB(inPatienMgr);

            
            return inPatienMgr.QueryHouseDocPatient(objDoc, inState, deptCode);
            
        }
        /// <summary>
        /// ���ݲ���״̬��ѯ����(Ƿ��)  {62EAD92D-49F6-45d5-B378-1E573EC27728}
        /// </summary>
        /// <param name="nurseCellID">��������</param>
        /// <param name="inState">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList QueryPatientByNurseCellAndStateForAlert(string nurseCellID, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByNurseCellForAlert(nurseCellID, inState);

        }
        /// <summary>
        /// ���ָ�����Ҽ�ҽ���Ļ��ﻼ��
        /// </summary>
        /// <param name="objDoc"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByConsultation(FS.FrameWork.Models.NeuObject objDoc, DateTime dtBegin,DateTime dtEnd, string deptCode)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryConsultation(objDoc, "0", dtBegin, dtEnd, deptCode);
            
        }

        /// <summary>
        /// ��÷ָ�ҽ��Ȩ�޵Ļ���
        /// </summary>
        /// <param name="objDoc"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByPermission(FS.FrameWork.Models.NeuObject objDoc)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientByPermission(objDoc.ID, ((FS.HISFC.Models.Base.Employee)objDoc).Dept.ID);
            
        }

        /// <summary>
        /// ��ѯסԺ��ˮ�Ÿ���סԺ��
        /// ���һ��߶����Ժ��ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList QueryInpatientNoByPatientNo(string patientNo)
        {
            SetDB(inPatienMgr);
            return inPatienMgr.QueryInpatientNOByPatientNO(patientNo);
        }

        /// <summary>
        /// ���ݲ�������״̬��ѯ���ߣ�Ƿ�ѻ��ߣ�{62EAD92D-49F6-45d5-B378-1E573EC27728}
        /// </summary>
        /// <param name="nurseCellID"></param>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByNurseCellAndDeptForAlert(string nurseCellID, string deptCode, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByNurseCellAndDeptForAlert(nurseCellID, deptCode, inState);

        }
        //���߲�ѯ
		/// <summary>
		/// ���߲�ѯ-��סԺ�Ų�
		/// </summary>
		/// <param name="inPatientNO">סԺ��ˮ��</param>
		/// <returns>������Ϣ PatientInfo</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryPatientInfoByInpatientNO(string inPatientNO)
        {
            SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientInfoByInpatientNO(inPatientNO);
        }

        //���߲�ѯ add by zhy ���ڴ����ߵ��ն˷���
        /// <summary>
        /// ���߲�ѯ-��סԺ�Ų� 
        /// </summary>
        /// <param name="inPatientNO">סԺ��ˮ��</param>
        /// <returns>������Ϣ PatientInfo</returns>
        /// <summary>
        /// ��������ѯ���߻�����Ϣ FS.HISFC.Models.RADT.PatientInfo
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public decimal QueryPatientTerminalFeeByInpatientNO(string inPatientNO)
        {
            SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientTerminalFeeByInpatientNO(inPatientNO);
        }

        public ArrayList QueryPatientByName(string name)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientByName(name);
        }
        /// <summary>
        /// {F5F57671-B453-45ff-A663-A682A000F567}
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByNameAndCardNo(string name)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientByNameAndCardno(name);
        }

        //{8659FDB2-4200-475c-83B6-37092AD86D7D}
        public ArrayList QueryPatientByPhone(string phone)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientByPhone(phone);
        }

        /// <summary>
		/// ���߲�ѯ-��סԺ�Ų�
		/// </summary>
		/// <param name="inPatientNO">����סԺ��ˮ��</param>
		/// <returns>���ػ�����Ϣ</returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfoByPatientNO(string inPatientNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.GetPatientInfoByPatientNO(inPatientNO);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList QureyPatientInfobyCardno(string cardNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QureyPatientInfo(cardNO);
        }

        /// <summary>
        /// ��ѯ���߻�����Ϣ
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryComPatientInfoByName(string name)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryComPatientInfoByName(name);
        }
        #endregion



        /// <summary>
        /// ������Ч��Ժ�ٻص���Ч������ѯ���ҳ�Ժ�Ǽǻ�����Ϣ
        /// ----Create By By ZhangQi
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <param name="vaildDays"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByVaildDate(string deptCode, FS.HISFC.Models.Base.EnumInState inState, int vaildDays)
        {
            this.SetDB(inPatienMgr);
            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.PatientQueryByVaildDate(deptCode, istate, vaildDays);
        }

        /// <summary>
        /// ������Ч�ٻ��ڲ�ѯһ��ʱ����ĳ�����ҵĳ�Ժ�Ǽǻ���(��ֹʱ��  ���Ҵ��� ��Ч����)
        /// ----Create By By ZhangQi
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="vaildDays"></param>
        /// <returns></returns>
        public ArrayList QueryOutHosPatient(string deptCode, string beginTime, string endTime, int vaildDays, int myPaientState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.OutHosPatientByState(deptCode, beginTime, endTime, vaildDays, myPaientState);
        }

        /// <summary>
        /// �����￨�Ų�ѯסԺ�ڼ��в����Ļ���
        /// by niuxinyuan
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <returns></returns>
        public ArrayList GetPatientInfoHaveCaseByCardNO(string cardNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.GetPatientInfoHaveCaseByCardNO(cardNO);
        }

        //{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F} Ӥ���ķ����Ƿ������ȡ����������

        /// <summary>
        /// ͨ��Ӥ����סԺ��ˮ��,��ѯĸ�׵�סԺ��ˮ��
        /// </summary>
        /// <param name="babyInpatientNO">Ӥ��סԺ��ˮ��</param>
        /// <returns>ĸ�׵�סԺ��ˮ�� ���󷵻� null ���� string.Empty</returns>
        public string QueryBabyMotherInpatientNO(string babyInpatientNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryBabyMotherInpatientNO(babyInpatientNO);
        }
        //{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F} Ӥ���ķ����Ƿ������ȡ���������� ����
        #region ����ҽ�����Ų�ѯסԺ������Ϣ
        /// <summary>
        /// ����ҽ�����Ų�ѯסԺ������Ϣ
        /// </summary>
        /// <param name="markNO"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByMcardNO(string mcardNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByMcardNO(mcardNO);
        }
        #endregion
        #region ���ת����

        /// <summary>
        /// �����Ƿ����þ�����
        /// </summary>
        /// <param name="payKindCode">��ͬ��λ��� ALL��ʾȫ��</param>
        /// <param name="pactCode">��ͬ��λ ALL��ʾȫ��</param>
        /// <param name="nureseCode">�������� ALL��ʾȫ��</param>
        /// <param name="inPaitentNo">סԺ��ˮ�� ALL��ʾȫ��</param>
        /// <param name="alterFlag">�Ƿ����þ�����</param>
        /// <returns></returns>
        public int UpdatePatientAlertFlag(string payKindCode, string pactCode, string nureseCode, string inPaitentNo, bool alterFlag)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertFlag(payKindCode, pactCode, nureseCode, inPaitentNo, alterFlag);
        }

        /// <summary>
        /// �����Ƿ����þ�����
        /// </summary>
        /// <param name="payKindCode">��ͬ��λ��� ALL��ʾȫ��</param>
        /// <param name="pactCode">��ͬ��λ ALL��ʾȫ��</param>
        /// <param name="nureseCode">�������� ALL��ʾȫ��</param>
        /// <param name="inPaitentNo">סԺ��ˮ�� ALL��ʾȫ��</param>
        /// <param name="alterFlag">�Ƿ����þ�����</param>
        /// <param name="operCode">����Ա</param>
        /// <returns></returns>
        public int UpdatePatientAlertFlag(string payKindCode, string pactCode, string nureseCode, string inPaitentNo, bool alterFlag, string operCode)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertFlag(payKindCode, pactCode, nureseCode, inPaitentNo, alterFlag, operCode);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// ���»��߾����߸���סԺ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlert(string inpatientNO, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlert(inpatientNO, moneyAlert,alertType,beginDate,endDate);
        }
        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// ���»��߾����߸��ݺ�ͬ��λ
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertByPactID(string pactID, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertByPactID(pactID, moneyAlert,alertType,beginDate,endDate);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// ���»��߾����߸���סԺ����
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertByDeptID(string deptID, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertByDeptID(deptID, moneyAlert,alertType,beginDate,endDate);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// ���»��߾����߸��ݻ�ʿվ
        /// </summary>
        /// <param name="nurseCellID"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertByNurseCellID(string nurseCellID, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertByNurseCellID(nurseCellID, moneyAlert,alertType,beginDate,endDate);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// ���»��߾����߸��ݻ�ʿվ�Ϳ���
        /// </summary>
        /// <param name="nurseCellID"></param>
        /// <param name="deptCode"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertByNurseCellIDAndDept(string nurseCellID, string deptCode, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertByNurseCellIDAndDept(nurseCellID, deptCode, moneyAlert,alertType,beginDate,endDate);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// ���»��߾��������л���
        /// </summary>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertAll(decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.UpdatePatientAlert(moneyAlert,alertType,beginDate,endDate);           
        }


        /// <summary>
        /// �ٻػ���
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int CallBack(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed Bed)
        {
            //���Ƶ��������ڳ�Ժ�����ʱ�����ٻ�.
            string stopAccountFlag = inpatientManager.GetStopAccount(patientInfo.ID);

            if (stopAccountFlag == "1")
            {
                //����,�������ڽ���
                //this.Err = "�������ڽ���...���Ժ�����!";{92467DA0-BE20-4a4b-8596-62598E3728A3}
                this.Err = "�������ڽ�������Ѿ�����...������סԺ����ϵ";
                return -1;
            }

            int parm = 0;
            FS.HISFC.Models.RADT.PatientInfo pMother = new FS.HISFC.Models.RADT.PatientInfo();

            //�ж�Ӥ���ٻ�
            if (patientInfo.IsBaby)
            {
                string motherPatientNo = inPatienMgr.QueryBabyMotherInpatientNO(patientInfo.ID);

                if (string.IsNullOrEmpty(motherPatientNo))
                {
                    this.Err = "��ȡĸ��סԺ��ʧ��,Ӥ��סԺ�ţ�" + patientInfo.PID.PatientNO + inPatienMgr.Err;
                    return -1;
                }

                //ȡӤ�������סԺ��Ϣ
                pMother = inPatienMgr.QueryPatientInfoByInpatientNO(motherPatientNo);
                if (pMother == null || pMother.ID == "")
                {
                    this.Err = "����:" + patientInfo.Name + "ĸ����Ϣ����!" + inPatienMgr.Err;
                    return -1;
                }

                //������費����Ժ״̬,���ܵ����ٻ�Ӥ��
                if (pMother.PVisit.InState.ID.ToString() != "I")
                {
                    this.Err = patientInfo.Name + "��ĸ��" + pMother.Name + "�ǳ�Ժ�Ǽ�״̬,�����ٻ�ĸ��!";
                    return -1;
                }

                //Ӥ���ٻصĴ�Ӧ�ø�������ͬ.������λ��Ϣ
                Bed = pMother.PVisit.PatientLocation.Bed.Clone();
            }

            Bed = managerBed.GetBedInfo(Bed.ID);

            //������߲���Ӥ��,�������ٻص�ռ�õĴ�λ
            if (patientInfo.IsBaby == false &&
                Bed.Status.ID.ToString() != "U" && Bed.Status.ID.ToString() != "H")
            {
                this.Err = "����ѡ��Ĵ�λΪ" + Bed.Status.Name + ", �޷��ٻ�!";
                return -1;
            }

            //�������:�ٻ�
            parm = inPatienMgr.RecievePatient(patientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.C, "�ٻ�");

            if (parm == -1)
            {

                this.Err = "�ٻ�ʧ�ܣ�" + inPatienMgr.Err;
                return -1;
            }
            else if (parm == 0)
            {

                this.Err = "�ٻ�ʧ��! ������Ϣ�б䶯,��ˢ�µ�ǰ����";
                return -1;
            }

            patientInfo.PVisit.PatientLocation.Bed = Bed;
            if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.C) == -1)
            {
                Err = "����λ��־����\r\n" + dayReportMgr.Err;
                return -1;
            }

            //����ǰ���ֹ��Ժ���ѯ����Ӥ��
            ArrayList al = inPatienMgr.QueryBabiesByMother(patientInfo.ID);
            if (al == null)
            {
                Err = inPatienMgr.Err;
                return -1;
            }
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo babyInfo = al[i] as FS.HISFC.Models.RADT.PatientInfo;
                babyInfo = inPatienMgr.QueryPatientInfoByInpatientNO(babyInfo.ID);
                if (babyInfo == null)
                {
                    Err = "����λ��־����\r\n" + inPatienMgr.Err;
                    return -1;
                }
                if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.C) == -1)
                {
                    Err = "����λ��־����\r\n" + dayReportMgr.Err;
                    return -1;
                }
            }

            this.Err = "�ٻسɹ���";
            return 1;
        }

        /// <summary>
        /// ��Ժ�Ǽǽ���
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="bed"></param>
        /// <returns></returns>
        public int ArrivePatientForReg(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed bed)
        {
            if (managerBed.SetBedInfo(bed) == -1)
            {
                Err = managerBed.Err;
                return -1;
            }

            patientInfo.PVisit.PatientLocation.Bed = bed;
            if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.B) == -1)
            {
                Err = dayReportMgr.Err;
                return -1;
            }
            return 1;
        }


        #region �������۳�Ժ�ٻ�
        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// �������۳�Ժ�ٻ�
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Bed"></param>
        /// <param name="isOut">�Ƿ�����ٻ�</param>
        /// <returns></returns>
        public int CallBack(FS.HISFC.Models.Registration.Register patientInfo, FS.HISFC.Models.Base.Bed Bed,bool isOut)
        {
            int parm = 0;

            Bed = managerBed.GetBedInfo(Bed.ID);

            //������߲���Ӥ��,�������ٻص�ռ�õĴ�λ
            if (Bed.Status.ID.ToString() != "U" && Bed.Status.ID.ToString() != "H")
            {
                this.Err = "����ѡ��Ĵ�λΪ" + Bed.Status.Name + ", �޷��ٻ�!";
                return -1;
            }
            if (isOut)
            {
                //�������:��Ժ�ٻ�
                parm = radtEmrManager.RecievePatient(patientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.EC, "�����ٻ�");
            }
            else
            {
                //�������:תסԺ�ٻ�
                parm = radtEmrManager.RecievePatient(patientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.IC, "�����ٻ�");
            }
            if (parm == -1)
            {

                this.Err = "�����ٻ�ʧ�ܣ�" + inPatienMgr.Err;//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                return -1;
            }
            else if (parm == 0)
            {

                this.Err = "�����ٻ�ʧ��! ������Ϣ�б䶯,��ˢ�µ�ǰ����";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                return -1;
            }


            this.Err = "�����ٻسɹ���";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
            return 1;
        }

        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Bed"></param>
        /// <returns></returns>
        public int ArrivePatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed Bed)
        {
            int parm = 0;


            //�ж�ѡ��Ĵ�λ�Ƿ����
            Bed = managerBed.GetBedInfo(Bed.ID);
            if (Bed.Status.ID.ToString() != "U" &&
                Bed.Status.ID.ToString() != "H")
            {
                this.Err = "����ѡ��Ĵ�λΪ" + Bed.Status.Name;
                return -1;
            }

            //{FA32C976-E003-4a10-9028-71F2CD154052} �̶�����ʱ��
            DateTime saveDate = patientInfo.PVisit.RegistTime;
            //patientInfo.PVisit.RegistTime = inPatienMgr.GetDateTimeFromSysDateTime();// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}


            //���䴦��(1���»�����Ϣ, 2��������)
            parm = inPatienMgr.RecievePatient(patientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.K, "����");

            //{FA32C976-E003-4a10-9028-71F2CD154052} �̶�����ʱ��
            patientInfo.PVisit.RegistTime = saveDate;

            if (parm == -1)
            {

                this.Err = "����ʧ�ܣ�" + inPatienMgr.Err;
                return -1;
            }
            else if (parm == 0)
            {

                this.Err = "����ʧ��! ������Ϣ�б䶯,��ˢ�µ�ǰ����";
                return -1;
            }

            patientInfo.PVisit.PatientLocation.Bed = Bed;
            if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.K) == -1)
            {
                Err = "����λ��־����\r\n" + dayReportMgr.Err;
                return -1;
            }

            this.Err = "����ɹ���";

            return 1;
        }

        /// <summary>
        /// �������۽���
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Bed"></param>
        /// <returns></returns>
        public int EmrArrivePatient(FS.HISFC.Models.Registration.Register outpatientInfo, FS.HISFC.Models.Base.Bed Bed)
        {
            int parm = 0;


            //�ж�ѡ��Ĵ�λ�Ƿ����
            Bed = managerBed.GetBedInfo(Bed.ID);
            if (Bed.Status.ID.ToString() != "U" &&
                Bed.Status.ID.ToString() != "H")
            {
                this.Err = "����ѡ��Ĵ�λΪ" + Bed.Status.Name;
                return -1;
            }

            //���䴦��(1���»�����Ϣ, 2��������)
            parm = radtEmrManager.RecievePatient(outpatientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.EK, "����");
            if (parm == -1)
            {
                this.Err = "���۽���ʧ�ܣ�" + inPatienMgr.Err;//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                return -1;
            }
            else if (parm == 0)
            {

                this.Err = "���۽���ʧ��! ������Ϣ�б䶯,��ˢ�µ�ǰ����";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                return -1;
            }

            this.Err = "���۽���ɹ���";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}

            return 1;
        }

        /// <summary>
        /// �������Ҵ�����
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="bed"></param>
        /// <param name="kind">0 </param>
        /// <returns></returns>
        public int WapBed(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed bed, string kind)
        {
            if (this.inPatienMgr.SwapPatientBed(patientInfo, bed.ID, kind) == -1)
            {
                this.Err = "����������Ҵ���Ϣʧ�ܣ�\r\n" + inPatienMgr.Err;
                return -1;
            }

            if (dayReportMgr.AddExtentBed(patientInfo, bed, true) == -1)
            {
                Err = "����λ��־����\r\n" + dayReportMgr.Err;
                return -1;
            }

            if (this.InsertShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.ABD, "����", new FS.FrameWork.Models.NeuObject(), bed) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �������Ҵ�����
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="bed"></param>
        /// <param name="kind">0 </param>
        /// <returns></returns>
        public int UnWapBed(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed bed, string kind)
        {
            if (this.inPatienMgr.UnWrapPatientBed(patientInfo, bed.ID, kind) == -1)
            {
                this.Err = "����������Ҵ���Ϣʧ�ܣ�\r\n" + inPatienMgr.Err;
                return -1;
            }

            if (dayReportMgr.AddExtentBed(patientInfo, bed, false) == -1)
            {
                Err = "����λ��־����\r\n" + dayReportMgr.Err;
                return -1;
            }

            if (this.InsertShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.RBD, "�����", bed, new FS.FrameWork.Models.NeuObject(" ", " ", " ")) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns></returns>
        public int ChangeDoc(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            int parm = 0;
    
            //���»�����Ϣ
            parm = inPatienMgr.RecievePatient(patientInfo, patientInfo.PVisit.InState);
            if (parm == -1)
            {

                this.Err = "���³���\n" + inPatienMgr.Err;
                return -1;
            }
            else if (parm == 0)
            {
              
                this.Err = "����ʧ��! ������Ϣ�б䶯,��ˢ�µ�ǰ����";
                return -1;
            }

            this.Err = "����ɹ���";
            return 1;
        }

       /// <summary>
       /// ת��ȷ��
       /// </summary>
       /// <param name="PatientInfo"></param>
       /// <param name="nurseCell"></param>
       /// <param name="bedNo"></param>
       /// <returns></returns>
        public int ShiftIn(FS.HISFC.Models.RADT.PatientInfo PatientInfo,FS.FrameWork.Models.NeuObject nurseCell,string bedNo)
        {
            int parm = 0;

            FS.HISFC.Models.RADT.PatientInfo patientInfoOld=PatientInfo.Clone();

            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
            newLocation = inPatienMgr.QueryShiftNewLocation(PatientInfo.ID, PatientInfo.PVisit.PatientLocation.Dept.ID);
            if (newLocation == null)
            {
                this.Err = "ת�ơ�ת����ȷ�ϳ���\n������Ϣ�б䶯,��ˢ�µ�ǰ����";
                return -1;
            }

            //���û���ҵ�����,˵�������Ѿ���ȷ��,����
            if (newLocation.Dept.ID == "" && newLocation.NurseCell.ID == "")
            {
                this.Err = "ת�ơ�ת����ȷ��ʧ�ܣ�\n" + inPatienMgr.Err;
                return -1;
            }
            if (newLocation.Dept.Name == "" && newLocation.Dept.ID != "")
            {
                newLocation.Dept = managerDepartment.GetDeptmentById(newLocation.Dept.ID);
                if (newLocation.Dept == null)
                {
                    this.Err = "ת��ȷ��ʧ�ܣ�\n" + managerDepartment.Err;
                    return -1;
                }

            }
            #region {9A2D53D3-25BE-4630-A547-A121C71FB1C5}
            if (newLocation.NurseCell.Name == "" && newLocation.NurseCell.ID != "")
            {
                newLocation.NurseCell = managerDepartment.GetDeptmentById(newLocation.NurseCell.ID);
                if (newLocation.NurseCell == null)
                {
                    this.Err = "ת����ȷ��ʧ�ܣ�\n" + managerDepartment.Err;
                    return -1;
                }

            }
            #endregion
            newLocation.NurseCell = nurseCell.Clone();
            newLocation.Bed.ID = bedNo;	//�²���
            newLocation.Bed.Status.ID = "U";					//�´���״̬
            newLocation.Bed.InpatientNO = "N";					//�´��Ļ���סԺ��ˮ��
            PatientInfo.User01 = newLocation.User01;
         
            try
            {
                //ȥϵͳʱ��
                DateTime sysDate = inPatienMgr.GetDateTimeFromSysDateTime();

                //���䴦��(1���»�����Ϣ, 2��������), ע:ֻҪ�н������,�����д˴���
                if (inPatienMgr.RecievePatient(PatientInfo, FS.HISFC.Models.Base.EnumInState.I) == -1)
                {

                    this.Err = "ת��ȷ�ϳ���\n" + inPatienMgr.Err;
                    return -1;
                }

                //ת�ƴ���
                parm = inPatienMgr.TransferPatient(PatientInfo, newLocation);
                if (parm == -1)
                {

                    this.Err = "ת��ȷ�ϳ���\n" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                   
                    this.Err = "����ʧ��! \n������Ϣ�б䶯,��ˢ�µ�ǰ����";
                    return -1;
                }

                //�ͷŰ����͹Ҵ�
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(PatientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    //if (inPatienMgr.UnWrapPatientBed(PatientInfo, obj.ID, obj.Memo) < 0)
                    if (this.UnWapBed(PatientInfo, obj, obj.Memo) == -1)
                    {
                        this.Err = "�ͷŴ�λʧ�ܣ�" + inPatienMgr.Err;
                        return -1;
                    }
                }


                //ֹͣҽ��
                //System.Windows.Forms.DialogResult r = System.Windows.Forms.MessageBox.Show("�Ƿ�ֹͣ��ǰ��ҽ����", "ת��ȷ��", System.Windows.Forms.MessageBoxButtons.OKCancel);
                //if (r == System.Windows.Forms.DialogResult.OK)
                //{
                //    if (managerOrder.DcOrder(PatientInfo.ID, sysDate, "01", "ת��ֹͣ") == -1)
                //    {

                //        this.Err = "ֹͣҽ��ʧ�ܣ�" + managerOrder.Err;
                //        return -1;
                //    }
                //}
                PatientInfo.PVisit.PatientLocation = newLocation;
                if (dayReportMgr.TransBed(patientInfoOld, PatientInfo) == -1)
                {
                    this.Err = "����λ��־����\r\n" + dayReportMgr.Err;
                    return -1;
                }
              
                this.Err = "ת��ȷ�ϳɹ���";
            }
            catch (Exception ex)
            {
               
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        ///  ת�����룬ȡ��
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="newDept"></param>
        /// <param name="state">��ǰ����״̬"1"</param>
        /// <param name="isCancel">�Ƿ�ȡ��</param>
        /// <returns></returns>
        public int ShiftOut(FS.HISFC.Models.RADT.PatientInfo patientInfo,
            FS.FrameWork.Models.NeuObject newDept,FS.FrameWork.Models.NeuObject newNurseCell,string state,bool isCancel)
        {

            //{DF72A3CF-38E6-4616-8287-DC989A4155F9} Ӥ��ת��
            //Ӥ��������ת��
            //if (patientInfo.IsBaby) //
            //{
            //   this.Err = ("Ӥ�������Ե���ת��,ֻ�ܸ���ĸ��һͬת��.");
            //    return -1;
            //}

            ////ȡĸ���Ƿ�����Ժ��Ӥ��������У��Ͳ�����ת��
            //int baby = inPatienMgr.IsMotherHasBabiesInHos(patientInfo.ID);
            //if (baby == -1)
            //{
            //     this.Err = (inPatienMgr.Err);
            //    return -1;
            //}
            string isBringBaby = patientInfo.User01;// {7FFE7A7E-239D-4019-97B4-D3F80BB79713}

            //ȡ�������������µ���Ϣ,�����жϲ���
            FS.HISFC.Models.RADT.PatientInfo patient = inPatienMgr.QueryPatientInfoByInpatientNO(patientInfo.ID);
            if (patient == null)
            {
                 this.Err = (inPatienMgr.Err);
                return -1;
            }
            //��������Ѳ��ڱ���,���������---������ת�ƺ�,���������û�йر�,����ִ������
            if (patient.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            {
                 this.Err = ("�����Ѳ��ڱ�����,��ˢ�µ�ǰ����");
                return -1;
            }
            //��������Ѳ�����Ժ״̬,���������
            if (patient.PVisit.InState.ID.ToString() != patientInfo.PVisit.InState.ID.ToString())
            {
                this.Err = ("������Ϣ�ѷ����仯,��ˢ�µ�ǰ����");
                return -1;
            }


            
            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
            //{9A2D53D3-25BE-4630-A547-A121C71FB1C5}start
            FS.HISFC.Models.Base.Department tmpDept = new FS.HISFC.Models.Base.Department();
            tmpDept = managerDepartment.GetDeptmentById(newDept.ID);
            bool isShiftNurseCell = false;
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            //if (tmpDept.DeptType.ID.ToString() == "N")
            //{
            //    isShiftNurseCell = true;
            //    newLocation.NurseCell.ID = newDept.ID;
            //    newLocation.NurseCell.Name = newDept.Name;
            //    newLocation.NurseCell.Memo = newDept.Memo;

            //    if (patientInfo.PVisit.PatientLocation.NurseCell.ID == newLocation.NurseCell.ID)
            //    {
            //        this.Err = ("ԭ����������Ŀ�겡����ͬ��");
            //        return -1;
            //    }
            //}
            ////{9A2D53D3-25BE-4630-A547-A121C71FB1C5}end
            //else
            //{
            //    //���¿�����Ϣ
            //    newLocation.Dept.ID = newDept.ID;
            //    newLocation.Dept.Name = newDept.Name;
            //    newLocation.Dept.Memo = newDept.Memo;

            //    if (patientInfo.PVisit.PatientLocation.Dept.ID == newLocation.Dept.ID)
            //    {
            //        this.Err = ("ԭ���Ҳ�����Ŀ�������ͬ��");
            //        return -1;
            //    }
            //}
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            newLocation.Dept.ID = newDept.ID;
            newLocation.Dept.Name = newDept.Name;
            newLocation.Dept.Memo = newDept.Memo;
            newLocation.NurseCell.ID = newNurseCell.ID;
            newLocation.NurseCell.Name = newNurseCell.Name;
            newLocation.User01 = isBringBaby;
            

            if (patientInfo.PVisit.PatientLocation.NurseCell.ID == newLocation.NurseCell.ID && patientInfo.PVisit.PatientLocation.Dept.ID == newLocation.Dept.ID)
            {
                this.Err = ("ԭ������ԭ���Ҳ�����Ŀ�겡����Ŀ����Ҷ���ͬ��");
                return -1;
            }


            //ת������/ȡ��
            try
            {
                int parm;
                if (state == null || state == "") state = "1";
                parm = inPatienMgr.TransferPatientApply(patientInfo, newLocation,
                    isCancel, state);//״̬�����뻹��ɶ?
                if (parm == -1)
                {
                    this.Err = (inPatienMgr.Err);
                    return -1;
                }
                else if (parm == 0)
                {
                    //ȡ������ʱ,������������
                    if (isCancel)
                        this.Err = ("��ת{0}��������Ч,����ȡ��.");
                    else
                        this.Err = ("��ת{0}�����ѱ�ȡ��,����ȷ��");
                    return -1;
                }
                else
                {
                    if(isCancel)
                        this.Err = "ȡ��ת{0}����ɹ���";
                    else
                        this.Err = "ת{0}����ɹ���";
                }
                
                if(this.Err.Contains("{0}"))
                {
                    if (isShiftNurseCell)
                    {
                        this.Err = string.Format(this.Err, "����");
                    }
                    else
                    {
                        this.Err = string.Format(this.Err, "��");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        
            return 1;
        }
        /// <summary>
        /// CA����
        /// </summary>
        /// <returns></returns>
        public int InsertCACompare(string emplcode, string cacode)
        {
            if (this.inPatienMgr.InsertCACompare(emplcode, cacode) != 1)
            {
                Err = "����ҽ����Ϣ����\r\n" + inPatienMgr.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// CA��������
        /// </summary>
        /// <param name="cacode"></param>
        /// <returns></returns>
        public int UpdateCancelCACompare(string cacode)
        {
            if (this.inPatienMgr.UpdateCancelCACompare(cacode) != 1)
            {
                Err = "���϶���ҽ����Ϣ����\r\n" + inPatienMgr.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// CA�����ж�
        /// </summary>
        /// <param name="cacode"></param>
        /// <returns></returns>
        public int QueryAllCompare(string emplcode, string cacode)
        {
            ArrayList al = new ArrayList();
            al = this.inPatienMgr.QueryAllCompare(emplcode,cacode);
            if (al.Count>0)
            {
                Err = "����ҽ����Ϣ����,����Ա�Ѿ����˶��գ�\r\n" + inPatienMgr.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// �Ǽ���Ӥ��
        /// </summary>
        /// <param name="babyInfo"></param>
        /// <returns></returns>
        public int InsertNewBabyInfo(FS.HISFC.Models.RADT.PatientInfo babyInfo)
        {
            if (this.inPatienMgr.InsertNewBabyInfo(babyInfo) != 1)
            {
                Err = "�Ǽ���Ӥ������\r\n" + inPatienMgr.Err;
                return -1;
            }

            if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.K) == -1)
            {
                Err = "����λ��־����\r\n" + dayReportMgr.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ȡ��Ӥ���Ǽ�
        /// </summary>
        /// <param name="babyInfo"></param>
        /// <returns></returns>
        public int DiscardBabyRegister(FS.HISFC.Models.RADT.PatientInfo babyInfo)
        {
            if (this.inPatienMgr.DiscardBabyRegister(babyInfo.ID) != 1)
            {
                Err = "�Ǽ���Ӥ������\r\n" + inPatienMgr.Err;
                return -1;
            }

            if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.OF) == -1)
            {
                Err = "����λ��־����\r\n" + dayReportMgr.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��Ժ�Ǽǵ»���
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        /// <returns>-1 ���� 0ȡ�� 1 �ɹ�</returns>
        public int OutPatientDH(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                //ֹͣҽ��
                //���շѣ����ѡ�ҽ���շѣ�
                DialogResult r = MessageBox.Show("�Ƿ�ֹͣȫ����ҽ����", "��Ժ�Ǽ�", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    if (managerOrder.DcOrder(patientInfo.ID, managerOrder.GetDateTimeFromSysDateTime(), "01", "��Ժֹͣ") == -1)
                    {
                        this.Err = "ֹͣҽ��ʧ�ܣ�" + managerOrder.Err;
                        return -1;
                    }
                }

                //���»���״̬���ÿմ�λ
                int parm = inPatienMgr.RegisterOutHospital(patientInfo);
                if (parm == -1)
                {

                    this.Err = "����ʧ�ܣ�" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    this.Err = "����ʧ��! ������Ϣ�б䶯,��ˢ�µ�ǰ����";
                    return -1;
                }

                //�ͷŰ����͹Ҵ�
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(patientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    if (inPatienMgr.UnWrapPatientBed(patientInfo, obj.ID, obj.Memo) < 0)
                    {

                        this.Err = "�ͷŴ�λʧ�ܣ�" + inPatienMgr.Err;
                        return -1;
                    }
                }

                this.Err = "��Ժ�Ǽǳɹ���";

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// ��Ժ�Ǽ�
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        /// <returns>-1 ���� 0ȡ�� 1 �ɹ�</returns>
        public int OutPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                //����ǰ���ֹ��Ժ���ѯ����Ӥ��
                ArrayList alBaby = inPatienMgr.QueryBabiesByMother(patientInfo.ID);
                if (alBaby == null)
                {
                    Err = inPatienMgr.Err;
                    return -1;
                }
                for (int i = 0; i < alBaby.Count; i++)
                {
                    FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[i] as FS.HISFC.Models.RADT.PatientInfo;
                    babyInfo = inPatienMgr.QueryPatientInfoByInpatientNO(babyInfo.ID);
                    if (babyInfo == null)
                    {
                        Err = inPatienMgr.Err;
                        return -1;
                    }
                    if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.O) == -1)
                    {
                        Err = dayReportMgr.Err;
                        return -1;
                    }
                }


                //���»���״̬���ÿմ�λ
                int parm = inPatienMgr.RegisterOutHospital(patientInfo);
                if (parm == -1)
                {
                    this.Err = "����ʧ�ܣ�" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    //���Ҳ���Ӥ����ǣ�����ʾ���� houwb 2011-5-16
                    if (inPatienMgr.QueryBabyMotherInpatientNO(patientInfo.ID) == "-1") //���Ҳ���Ĭ�Ϸ���"-1"
                    {
                        this.Err = "����ʧ��! ������Ϣ�б䶯,��ˢ�µ�ǰ����";
                        return -1;
                    }
                }


                if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.O) == -1)
                {
                    Err = dayReportMgr.Err;
                    return -1;
                }

                //�ͷŰ����͹Ҵ�
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(patientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    if (this.UnWapBed(patientInfo, obj, obj.Memo) < 0)
                    {
                        this.Err = "�ͷŴ�λʧ�ܣ�" + inPatienMgr.Err;
                        return -1;
                    }
                }

                this.Err = "��Ժ�Ǽǳɹ���";

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �������۳�Ժ�Ǽ�
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        /// <returns>-1 ���� 0ȡ�� 1 �ɹ�</returns>
        public int OutPatient(FS.HISFC.Models.Registration.Register patientInfo)
        {
            try
            {
                //���»���״̬���ÿմ�λ
                int parm = radtEmrManager.RegisterOutHospital(patientInfo);
                
                if (parm == -1)
                {

                    this.Err = "����ʧ�ܣ�" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    this.Err = "����ʧ��! ������Ϣ�б䶯,��ˢ�µ�ǰ����";
                    return -1;
                }

                //�ͷŰ����͹Ҵ�
                ArrayList al = new ArrayList();
                al = radtEmrManager.GetSpecialBedInfo(patientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    if (radtEmrManager.UnWrapPatientBed(patientInfo, obj.ID, obj.Memo) < 0)
                    {

                        this.Err = "�ͷŴ�λʧ�ܣ�" + inPatienMgr.Err;
                        return -1;
                    }
                }

                this.Err = "���۳�Ժ�ɹ���";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ����ԤԼ��¼��״̬
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdatePreInPatientState(string cardNo, string state)
        {
            SetDB(inPatienMgr);
            return inPatienMgr.UpdatePreInPatientState(cardNo,state);
        }
        /// <summary>
        /// �޷���Ժ�޸�סԺ��
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int UnregisterChangePatientNO(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.SetDB(inPatienMgr);
            if (patientInfo == null)
            {
                this.Err = "������Ϣ����Ϊ�գ�";
                return -1;
            }

            if (patientInfo.PID.PatientNO.StartsWith("N")
                || patientInfo.PID.PatientNO.StartsWith("B")
                || patientInfo.PID.PatientNO.StartsWith("F")
                || patientInfo.PID.PatientNO.StartsWith("L")
            || patientInfo.PID.PatientNO.StartsWith("C"))
            {
                //����Ҫ���л���סԺ��
                return 1;
            }

            string newPatientNO = "C" + patientInfo.PID.PatientNO.Substring(1);
            string oldPatientNO = patientInfo.PID.PatientNO;
            string newCardNO = patientInfo.PID.CardNO;
            if (patientInfo.PID.CardNO.StartsWith("T"))//��T��ͷ�Ļ��߻�����Ϣ ��Ҫ�޸Ļ��߻�����Ϣ
            {
                newCardNO = "C" + patientInfo.PID.CardNO.Substring(1);
            }

            //�޸�סԺ��
            if (this.inPatienMgr.UpdatePatientNO(patientInfo.ID, patientInfo.PID.CardNO, newPatientNO, newCardNO) == -1)
            {
                this.Err = "�޸�סԺ�Ŵ���" + this.inPatienMgr.Err;
                return -1;
            }

            //��������¼
            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "�޷���Ժ";
            if (inPatienMgr.SetShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.F, "����סԺ��", obj1, obj2, patientInfo.IsBaby) < 0)
            {
                this.Err = "���±����־ʧ��!" + inPatienMgr.Err;
                return -1;
            }

            //���뻼��סԺ�Ż��ձ�
            if (this.inPatienMgr.SetPatientNOShift(newPatientNO, oldPatientNO) == -1)
            {
                this.Err = "����סԺ�Ż��ձ�ʧ�ܣ�" + radtEmrManager.Err;
                return -1;
            }

            return 1;
            
        }

        /// <summary>
        /// �޷���Ժ
        /// </summary>
        /// <param name="patient">סԺ������Ϣʵ��</param>
        /// <returns>1 �ɹ� -1 ʧ��</returns>
        public int UnregisterNoFee(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.SetDB(inPatienMgr);

            //�����Ժ״̬����Ժ���Ҳ���Ӥ��,���ͷŴ�λ
            if (patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.I.ToString()
                && !patientInfo.IsBaby)
            {
                //���´�λ
                FS.HISFC.Models.Base.Bed newBed = patientInfo.PVisit.PatientLocation.Bed.Clone();
                newBed.InpatientNO = "N";	//��λ�޻���
                newBed.Status.ID = "U";	//��λ״̬�ǿմ�

                //���´�λ״̬
                int parm = inPatienMgr.UpdateBedStatus(newBed, patientInfo.PVisit.PatientLocation.Bed);
                if (parm == -1)
                {
                    this.Err = "�ͷŴ�λʧ��" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    this.Err = "������Ϣ�����䶯,��ˢ�µ�ǰ����" + inPatienMgr.Err;
                    return -1;
                }

                //û�п����еǼǵ�Ӥ�����޷���Ժ������ �����������
                //ArrayList al = inPatienMgr.QueryBabiesByMother(patientInfo.ID);
                //if (al == null)
                //{
                //    Err = inPatienMgr.Err;
                //    return -1;
                //}
                //for (int i = 0; i < al.Count; i++)
                //{
                //    FS.HISFC.Models.RADT.PatientInfo babyInfo = al[i] as FS.HISFC.Models.RADT.PatientInfo;
                //    babyInfo = inPatienMgr.QueryPatientInfoByInpatientNO(babyInfo.ID);
                //    if (babyInfo == null)
                //    {
                //        Err = "����λ��־����\r\n" + inPatienMgr.Err;
                //        return -1;
                //    }
                //    if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.OF) == -1)
                //    {
                //        Err = "����λ��־����\r\n" + dayReportMgr.Err;
                //        return -1;
                //    }
                //}
                if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.OF) == -1)
                {
                    Err = dayReportMgr.Err;
                    return -1;
                }

                #region �ͷŰ���

                //�ͷŰ����͹Ҵ�
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(patientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    //if (inPatienMgr.UnWrapPatientBed(patientInfo, obj.ID, obj.Memo) < 0)
                    if (this.UnWapBed(patientInfo, obj, obj.Memo) < 0)
                    {
                        this.Err = "�ͷŴ�λʧ�ܣ�" + inPatienMgr.Err;
                        return -1;
                    }
                }
                #endregion
            }

            //���»�������:סԺ״̬��Ϊ�޷���ԺN
            patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.N.ToString();
            // patientInfo.PVisit.OutTime = (DateTime)inPatienMgr.GetSysDateTime;

            if (inPatienMgr.UpdatePatient(patientInfo) != 1)
            {
                this.Err = "����סԺ����ʧ��" + inPatienMgr.Err;
                return -1;
            }

            //��������־

            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "�޷���Ժ";
            if (inPatienMgr.SetShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.OF, "�޷���Ժ", obj1, obj2, patientInfo.IsBaby) < 0)
            {
                this.Err = "���±����־ʧ�ܡ�" + inPatienMgr.Err;
                return -1;
            }

            return 1;
        }
        #endregion

        #region ������������

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="lfch"></param>
        /// <returns></returns>
        public int InsertLifeCharacter(FS.HISFC.Models.RADT.LifeCharacter lfch)
        {
            this.SetDB(lfchManagement);
            return lfchManagement.InsertLifeCharacter(lfch);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="measureDate"></param>
        /// <returns></returns>
        public int DeleteLifeCharacter(string inPatientNO, DateTime measureDate)
        {
            this.SetDB(lfchManagement);
            return lfchManagement.DeleteLifeCharacter(inPatientNO, measureDate);
        }

        #endregion

        #region סԺ��ת��ת�� by luzhp 2007-7-11
        /// <summary>
        /// ���ݿ��Һ���Ժ״̬���һ���
        /// </summary>
        /// <param name="dept_Code">���ұ���</param>
        /// <param name="state">��Ժ״̬</param>
        /// <returns></returns>
        public ArrayList QueryPatientByDeptCode(string dept_Code,FS.HISFC.Models.RADT.InStateEnumService state)
        { 
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientBasic(dept_Code, state);
        }

        public int ChangeDept(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.Location newlocation)
        {
            try
            {
                #region ��֤����

                FS.HISFC.Models.RADT.PatientInfo patient = QueryPatientInfoByInpatientNO(PatientInfo.ID); //inPatienMgr.GetPatientInfoByPatientNO(PatientInfo.ID);
                if (patient.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                {
                    this.Err = "�û���δ���";
                    return -1;
                }
                #endregion

                if (patient.IsBaby)
                {
                    this.Err = "Ӥ�������Ե���ת�ơ�ת��,\nֻ�ܸ���ĸ��һͬת��";
                    return -1;
                }

                #region ��֤��λ
                string bedNo = newlocation.Bed.ID;
                FS.HISFC.Models.Base.Bed bed = managerBed.GetBedInfo(bedNo);
                if (bed == null)
                {
                    this.Err = "ת�ơ���ʧ�ܣ�";
                    return -1;
                }
                if (bed.Status.ID.ToString() == "W")
                {
                    MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ����������ռ�ã�", "��ʾ��");
                    return -1;
                }
                else if (bed.Status.ID.ToString() == "C")
                {
                    MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ״̬Ϊ�رգ�����ռ�ã�", "��ʾ��");
                    return -1;
                }
                else if (bed.IsPrepay)
                {
                    MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ�Ѿ�ԤԼ������ռ�ã�", "��ʾ��");
                    return -1;
                }
                else if (!bed.IsValid)
                {
                    MessageBox.Show("����Ϊ [" + bedNo + " ]�Ĵ�λ�Ѿ�ͣ�ã�����ռ�ã�", "��ʾ��");
                    return -1;
                }
                #endregion

                //ȥϵͳʱ��
                DateTime sysDate = inPatienMgr.GetDateTimeFromSysDateTime();

                //���䴦��(1���»�����Ϣ, 2��������), ע:ֻҪ�н������,�����д˴���
                if (inPatienMgr.RecievePatient(patient, FS.HISFC.Models.Base.EnumInState.I) == -1)
                {

                    this.Err = "ת��ȷ�ϳ���\n" + inPatienMgr.Err;
                    return -1;
                }
                int parm;
                //ת�ƴ���
                parm = inPatienMgr.TransferPatientLocation(patient, newlocation);
                if (parm == -1)
                {

                    this.Err = "ת��ȷ�ϳ���\n" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                   
                    this.Err = "����ʧ��! \n������Ϣ�б䶯,��ˢ�µ�ǰ����";
                    return -1;
                }

                //�ͷŰ����͹Ҵ�
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(patient.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    if (inPatienMgr.UnWrapPatientBed(patient, obj.ID, obj.Memo) < 0)
                    {
                        this.Err = "�ͷŴ�λʧ�ܣ�" + inPatienMgr.Err;
                        return -1;
                    }
                    
                }

                //ֹͣҽ��
                System.Windows.Forms.DialogResult r = System.Windows.Forms.MessageBox.Show("�Ƿ�ֹͣ��ǰ��ҽ����", "ת��ȷ��", System.Windows.Forms.MessageBoxButtons.OKCancel);
                if (r == System.Windows.Forms.DialogResult.OK)
                {
                    if (managerOrder.DcOrder(PatientInfo.ID, sysDate, "01", "ת��ֹͣ") == -1)
                    {

                        this.Err = "ֹͣҽ��ʧ�ܣ�" + managerOrder.Err;
                        return -1;
                    }
                }
              
                this.Err = "ת�ơ���ȷ�ϳɹ���";
                return parm;
            }
            catch (Exception ex)
            {
               
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="newobj"></param>
        /// <param name="oldobj"></param>
        /// <returns></returns>
        public int SetPactShiftData(FS.HISFC.Models.RADT.PatientInfo patient, FS.FrameWork.Models.NeuObject newobj, FS.FrameWork.Models.NeuObject oldobj)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.SetPactShiftData(patient, newobj, oldobj);
        }

        #endregion

        #region ���»���״̬

        /// <summary>
        /// ���»�����Ժ״̬
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="patientStatus"></param>
        /// <returns></returns>
        public int UpdatePatientState(FS.HISFC.Models.RADT.PatientInfo patientInfo,FS.HISFC.Models.RADT.InStateEnumService patientStatus)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientStatus(patientInfo, patientStatus);
        }

        #endregion

        #region ��������
        public int RegisterObservePatient(FS.HISFC.Models.Registration.Register outPatient)
        { 
            this.SetDB(radtEmrManager);
            return radtEmrManager.RegisterObservePatient(outPatient);
        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// ���ۻ��߳��غ���
        /// </summary>
        /// <returns></returns>
        public int OutObservePatientManager(FS.HISFC.Models.Registration.Register OutPatient, FS.HISFC.Models.Base.EnumShiftType type,string notes)
        {
            this.SetDB(radtEmrManager);
            return radtEmrManager.OutObservePatientManager(OutPatient, type,notes);
        }
        #endregion

        #region ���Ļ��߲���{F0C48258-8EFB-4356-B730-E852EE4888A0}
        /// <summary>
        /// ���»��߲���״̬������Ϊ���أ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateBZ_Info(string id)
        {
            this.SetDB(inPatienMgr);
            return this.inPatienMgr.UpdateBZ_Info(id);
        }
        /// <summary>
        /// ���»��߲���״̬������Ϊ��ͨ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdatePT_Info(string id)
        {
            this.SetDB(inPatienMgr);
            return this.inPatienMgr.UpdatePT_Info(id);
        }

        public string SelectBQ_Info(string id)
        {
            this.SetDB(inPatienMgr);
            return this.inPatienMgr.SelectBQ_Info(id);
        }
        //{F0C48258-8EFB-4356-B730-E852EE4888A0}
        #endregion

        #region ȡȫԺĳһ���סԺ�ձ�����{A500A213-41EC-4d2f-87DA-4A2BB0D635A4}
        public ArrayList GetInpatientDayReportList(DateTime dateStat) 
        {
            this.SetDB(dayReportMgr);
            return dayReportMgr.GetInpatientDayReportList(dateStat);
        }
        #endregion

        #region ȡȫԺĳһ���סԺ�ձ�����{CB8DF724-12C6-47b9-A375-0F32167A6659}
        public ArrayList GetDayReportDetailList(DateTime dateBegin, DateTime dateEnd, string deptCode, string nurseCellCode) 
        {
            this.SetDB(dayReportMgr);
            return dayReportMgr.GetDayReportDetailList(dateBegin, dateEnd,deptCode,nurseCellCode);
        }
        #endregion

        #region ����סԺ�ձ����ܱ���һ����¼{563EE3FB-8744-478a-8A63-B383DF637E94}
        public int UpdateInpatientDayReport(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport)
        {
            this.SetDB(dayReportMgr);
            return dayReportMgr.UpdateInpatientDayReport(dayReport);
        }
        #endregion

        #region ��סԺ�ձ����ܱ��в���һ����¼{C4275ACD-5523-4c15-903B-473527F0B43D}
        public int InsertInpatientDayReport(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport)
        {
            this.SetDB(dayReportMgr);
            return dayReportMgr.InsertInpatientDayReport(dayReport);
        }
        #endregion
    }

    ///// <summary>
    ///// ��Ժ�Ǽǽӿ�
    ///// </summary>
    //public interface IucOutPatient
    //{
    //    bool IsSelect
    //    {
    //        set;
    //    }
    //}
    ///// <summary>
    ///// ��ʿվ��Ժ�ٻؽӿ�
    ///// </summary>
    //public interface ICallBackPatient
    //{
    //    bool IsSelect
    //    {
    //        set;
    //    }
    //}

    ///// <summary>
    ///// ��Ժ����Ժ�ٻصȵط����ж�,�Ƿ����ִ����һ������
    ///// </summary>
    //public interface IPatientShiftValid
    //{
    //    /// <summary>
    //    /// ��Ժ����Ժ�ٻصȵط����ж�,�Ƿ����ִ����һ������
    //    /// </summary>
    //    /// <param name="p">������Ϣ</param>
    //    /// <param name="type">��������</param>
    //    /// <param name="err">����</param>
    //    /// <returns>true�жϳɹ� false���󷵻ش���err</returns>
    //    bool IsPatientShiftValid(FS.HISFC.Models.RADT.PatientInfo p, EnumPatientShiftValid type, ref string err);
    //}

}
