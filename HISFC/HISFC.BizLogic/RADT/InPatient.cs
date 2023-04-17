using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using FS.FrameWork.Models;
using Bed = FS.HISFC.Models.Base.Bed;

namespace FS.HISFC.BizLogic.RADT
{
    /// <summary>
    /// ��Ҫ����ɻ��ߵ���Ժ��ADMISSION������Ժ(DISCHARGE)��תԺ(TRANSFER)������
    /// ���ṩ�͹������Ϣ����������Ȼ��Ϣ(Patient Identification)�����˷�����Ϣ(Patient Visit) ��
    /// ������Ϣ������������ʬ����Ϣ�ȡ��ţ�������Һš�סԺ�Ǽǣ���ԺתԺ����ȶ��Դ�Ϊ���ࡣ  
    ///1��	RegisterPatient
    ///2��	DischargePatient 
    ///3��	TransferPatient
    ///4��	--ChangeOutToIn
    ///5��	--ChangeInToOut
    ///6��	UpdatePatient
    ///7��	ArrivePatient(2)
    ///8��	CancelTransfer
    ///9��	CancelDischarge
    ///10��	PendingAdmit
    ///11��	PendingTransfer
    ///12��	PendingDischarge
    ///13��	CancelPendingAdmit
    ///14��	CancelPendingTransfer
    ///15��	CancelPendingDischarge
    ///16��	SwapPatient
    ///17��	PatientQuery
    ///18��	BedStatusUpdate
    ///19��	DeletePatient
    ///20��ChangePID
    ///  </summary>
    public class InPatient : Database
    {
        /// <summary>
        /// סԺ�������ת
        /// </summary>
        public InPatient()
        {
        }

        ShiftTypeEnumService shiftTypeEnumService = new ShiftTypeEnumService();

        /// <summary>
        /// ����ת�����  ��ʿվ����
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int UpdateZGInfo(PatientInfo patientInfo, string inStatus)
        {
            #region SQL
            /*
			        UPDATE 	FIN_IPR_INMAININFO  
					SET    	IN_STATE = '{2}',    --���º����Ժ״̬
							ZG = '{3}'  ,        --ת��
							OUT_DATE = to_date('{4}','yyyy-mm-dd HH24:mi:ss') 
					WHERE  	PARENT_CODE  = '[��������]' 
					  AND   CURRENT_CODE = '[��������]'  
					  AND   INPATIENT_NO = '{0}' 
					  AND   IN_STATE     = '{5}' 
			*/
            #endregion

            return this.ExecNoQueryByIndex("RADT.InPatient.UpdatePatientStatus.1",
                    patientInfo.ID, //0����סԺ��ˮ��
                    patientInfo.Name, //1��������
                    inStatus, //2���º����Ժ״̬
                    patientInfo.PVisit.ZG.ID, //3����ת����Ϣ
                    patientInfo.PVisit.PreOutTime.ToString(), //4Ԥ��Ժ����
                    patientInfo.PVisit.InState.ID.ToString(),//5����ǰ����Ժ״̬,�����жϲ���
                    patientInfo.PVisit.HealthCareType//6����ҽ������ 0��ͨ  1���� 2��������  3ICU   4�ھ�   5�ۿ�    6�ǿ�ָ������  7��������
                );
        }

        /// <summary>
        /// ����ת��״̬  ҽ��վ����ԤԼ��Ժҽ������ by huangchw 2012-10-29
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="ZG"></param>
        /// <param name="diagName"></param>
        /// <param name="healthCareType">ҽ������ by zhaorong 2013-7-24</param>
        /// <returns></returns>
        public int UpdateZG(string patientID, string ZG, string diagName, string healthCareType)
        {
            #region SQL
            /*
			   update fin_ipr_inmaininfo fii
                set fii.zg = '{1}',
                fii.diag_name = '{2}',
                fii.healthcare_type = '{3}'
                where fii.inpatient_no = '{0}'
			*/
            #endregion
            return this.ExecNoQueryByIndex("RADT.InPatient.UpdatePatient.ZG",
                                patientID,      //0����סԺ��ˮ��
                                ZG,             //1����ת�����
                                diagName,       //�������
                                healthCareType  //ҽ������
                            );
        }

        /// <summary>
        /// ����ҽ���������� ҽ��վ����ԤԼ��Ժҽ������ by huyungui 2019-10-15
        /// </summary>
        /// {9BCBF464-EB90-4c07-AD4D-29481A069D3D}
        /// <param name="patientID"></param>
        /// <param name="ZG"></param>
        /// <param name="diagName"></param>
        /// <param name="healthCareType">ҽ������ by huyungui 2019-10-15</param>
        /// <returns></returns>
        public int UpdateYIBAODAIYU(string patientID, string healthCareType)
        {
            #region SQL
            /*
			   update fin_ipr_inmaininfo fii
                    set 
                    fii.healthcare_type = '{1}'
                    where fii.inpatient_no = '{0}'
			*/
            #endregion
            return this.ExecNoQueryByIndex("RADT.InPatient.UpdatePatient.YIBAODAIYU",
                                patientID,      //0����סԺ��ˮ��
                                healthCareType  //ҽ������
                            );
        }

        #region ���ת


        /// <summary>
        /// ��ѯҽ���������� {d88ca0f0-6235-4a5d-b04e-4eac0f7a78e7}
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public string GetYIBAODAIYU(string patientID)
        {
            string sql = @"select dic.name from fin_ipr_inmaininfo t 
               left join com_dictionary dic on  t.healthcare_type=dic.code and type='MEDICALINSURANCEITEM'
               where inpatient_no = '{0}'";

            sql = String.Format(sql, patientID);
            return ExecSqlReturnOne(sql, string.Empty);
        }

        #region ���¾����� {A45EE85D-B1E3-4af0-ACAD-9DAF65610611}

        /// <summary>
        /// ���»��߾�����
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��
        /// <param name="moneyAlert">������
        /// <returns></returns>
        public int UpdatePatientAlert(string inpatientNO, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("RADT.UpdatePatientAlert.Update", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: RADT.UpdatePatientAlert.Update ��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, inpatientNO, moneyAlert.ToString(), alertType, beginDate.ToString(), endDate.ToString());
        }
        /// <summary>
        /// ���»��߾�����(���ݻ�ʿվ)
        /// </summary>
        /// <param name="nurseCellID">��ʿվ��
        /// <param name="moneyAlert">������
        /// <returns></returns>
        public int UpdatePatientAlertByNurseCellID(string nurseCellID, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("RADT.UpdatePatientAlertByNurseCellID.Update", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: RADT.UpdatePatientAlertByNurseCellID.Update ��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, nurseCellID, moneyAlert.ToString(), alertType.ToString(), beginDate.ToString(), endDate.ToString());
        }
        /// <summary>
        /// ���»��߾�����(���ݻ�ʿվ�Ϳ���)
        /// </summary>
        /// <param name="nurseCellID"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertByNurseCellIDAndDept(string nurseCellID, string deptCode, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("RADT.UpdatePatientAlertByNurseCellIDAndDept.Update", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: RADT.UpdatePatientAlertByNurseCellIDAndDept.Update ��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, nurseCellID, deptCode, moneyAlert.ToString(), alertType, beginDate.ToString(), endDate.ToString());
        }
        /// <summary>
        /// ���»��߾�����(����סԺ����)
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="moneyAlert">������</param>
        /// <returns></returns>
        public int UpdatePatientAlertByDeptID(string deptID, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("RADT.UpdatePatientAlertByDeptID.Update", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: RADT.UpdatePatientAlertByDeptID.Update ��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, deptID, moneyAlert.ToString(), alertType, beginDate.ToString(), endDate.ToString());
        }



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
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("RADT.UpdatePatientAlertFlag.Update", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: RADT.UpdatePatientAlertFlag.Update ��SQL���";

                return -1;
            }

            /*
             * update fin_ipr_inmaininfo t
             * set t.alter_flag='{4}'
             * where (t.paykind_code='{0}' or '{0}'='ALL')--��ͬ��λ���
             * and (t.pact_code='{1}' or '{1}'='ALL')--��ͬ��λ
             * and (t.nurse_cell_code='{2}' or '{2}'='ALL')--����
             * and (t.inpatient_no='{3}' or '{3}'='ALL')--����
             * */

            return this.ExecNoQuery(sql, payKindCode, pactCode, nureseCode, inPaitentNo, FS.FrameWork.Function.NConvert.ToInt32(alterFlag).ToString());
        }

        /// <summary>
        /// �����Ƿ����þ�����
        /// </summary>
        /// <param name="payKindCode">��ͬ��λ��� ALL��ʾȫ��</param>
        /// <param name="pactCode">��ͬ��λ ALL��ʾȫ��</param>
        /// <param name="nureseCode">�������� ALL��ʾȫ��</param>
        /// <param name="inPaitentNo">סԺ��ˮ�� ALL��ʾȫ��</param>
        /// <param name="alterFlag">�Ƿ����þ�����</param>
        /// <param name="operCode">����Ա��¼�·�ֹ˵����</param>
        /// <returns></returns>
        public int UpdatePatientAlertFlag(string payKindCode, string pactCode, string nureseCode, string inPaitentNo, bool alterFlag, string operCode)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("RADT.UpdatePatientAlertFlag.UpdateNew", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: RADT.UpdatePatientAlertFlag.UpdateNew ��SQL���";

                return -1;
            }

            /*
             * update fin_ipr_inmaininfo t
             * set t.alter_flag='{4}'��
             * t.ext_code='{5}'
             * where (t.paykind_code='{0}' or '{0}'='ALL')--��ͬ��λ���
             * and (t.pact_code='{1}' or '{1}'='ALL')--��ͬ��λ
             * and (t.nurse_cell_code='{2}' or '{2}'='ALL')--����
             * and (t.inpatient_no='{3}' or '{3}'='ALL')--����
             * and t.in_state in ('B','I','R')
             * */

            return this.ExecNoQuery(sql, payKindCode, pactCode, nureseCode, inPaitentNo, FS.FrameWork.Function.NConvert.ToInt32(alterFlag).ToString(), operCode);
        }

        /// <summary>
        /// ���»��߾�����(����)
        /// </summary>
        /// <param name="moneyAlert">������
        /// <returns></returns>
        public int UpdatePatientAlert(decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("RADT.UpdatePatientAlertAll.Update", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: RADT.UpdatePatientAlertAll.Update ��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, moneyAlert.ToString(), alertType, beginDate.ToString(), endDate.ToString());
        }
        /// <summary>
        /// ���»��߾�����(��ͬ��λ)
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��
        /// <param name="moneyAlert">������
        /// <returns></returns>
        public int UpdatePatientAlertByPactID(string pactID, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("RADT.UpdatePatientAlertByPactID.Update", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: RADT.UpdatePatientAlertByPactID.Update ��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, pactID, moneyAlert.ToString(), alertType, beginDate.ToString(), endDate.ToString());
        }

        /// <summary>
        /// ���»������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="diagName"></param>
        /// <returns></returns>
        public int UpdatePatientDiag(string inpatientNo, string diagName)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("RADT.Inpatient.Patient.UpdatePatientDiag", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:RADT.Inpatient.Patient.UpdatePatientDiag";
                return -1;
            }

            return this.ExecNoQuery(strSql, inpatientNo, diagName);
        }

        #endregion

        #region ������Ժ�Ǽ�   ����

        [Obsolete("���ڸ���Ϊ  RegisterInpatient", true)]
        public int InPatientRegister(PatientInfo PatientInfo)
        {
            return -1;
        }
        #endregion

        #region ������Ժ�Ǽ�
        /// <summary>
        /// ������Ժ�Ǽ�,ͬʱ������Ϣ���в���һ����¼
        /// </summary>
        /// <param name="PatientInfo">ԤԼ��λ������PatientInfo��bed ��</param>
        /// <returns>���� 0 �ɹ� С�� 0 ʧ��</returns>
        public int RegisterInpatient(PatientInfo PatientInfo)
        {
            //����סԺ����
            if (InsertPatient(PatientInfo) < 0)
            {
                Err = "���뻼������ʱʧ��";
                WriteErr();
                return -1;
            }

            //���²��˻�����Ϣ
            if (UpdatePatientInfo(PatientInfo) < 0)
            {
                Err = "���»��߻�����Ϣʱʧ��";
                WriteErr();
                return -1;
            }

            //���±����¼����
            if (SetShiftData(PatientInfo.ID, EnumShiftType.B, "סԺ�Ǽ�", PatientInfo.PVisit.PatientLocation.Dept, PatientInfo.PVisit.PatientLocation.Dept, PatientInfo.IsBaby) >= 0)
            {
                return 0;
            }

            Err = "���±����¼��ʧ��";
            WriteErr();
            return -1;
        }

        #endregion

        /// <summary>
        /// ����δʹ�õ�סԺ��Ϊʹ��״̬
        /// </summary>
        /// <param name="oldPatienNO">�ɵ�סԺ�ţ�δʹ�õ�</param>
        /// <returns>1�ɹ���0,����������Ӧ�����»�ȡסԺ�ţ�-1 ʧ��</returns>
        public int UpdatePatientNOState(string oldPatienNO)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("RADT.InPatient.UpdatePateintNoState", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                    oldPatienNO,		//0 ���߾ɵ�סԺ��
                    this.Operator.ID	//1 ����Ա
                    );
            }
            catch
            {
                this.Err = "�����������RADT.InPatient.UpdatePateintNoState!";
                this.WriteErr();
                return -1;
            }

            int parm = this.ExecNoQuery(strSql);
            if (parm == 0)
            {
                this.Err = "��סԺ���ѱ�ʹ�á�";
            }
            return parm;
        }

        #region �����￨�Ų�ѯ������Ϣ

        /// <summary>
        /// ͨ��ҽ�ƿ��ż����Ѿ����ڵĻ�����Ϣ
        /// </summary>
        /// <param name="CardNO">ҽ�ƿ�����</param>
        /// <returns>����һ��������Ϣ��¼</returns>
        public ArrayList QureyPatientInfo(string CardNO)
        {
            ArrayList al = new ArrayList();

            PatientInfo PatientInfo = new PatientInfo();
            //��ѯԤԼ��
            al = this.GetPreInByCardNO(CardNO);
            if (al != null)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    PatientInfo = (PatientInfo)al[i];
                    ArrayList al1 = new ArrayList();
                    al1 = this.GetPatientInfoByCardNO(CardNO);
                    if (al1 != null && al1.Count > 0) //by niuxinyuan 
                    {
                        PatientInfo obj = new PatientInfo();
                        obj = (PatientInfo)al1[0];
                        PatientInfo.ID = obj.ID;
                        PatientInfo.ID = obj.ID;
                    }
                }
            }
            //��ѯ����
            al = GetPatientInfoByCardNO(CardNO);
            if (al != null)
            {
                return al;
            }
            else
            {
                al = new ArrayList();
            }


            //��ѯ���߻�����Ϣ��
            PatientInfo = QueryComPatientInfo(CardNO);
            if (PatientInfo.ID != null)
            {
                al.Add(PatientInfo);
                return al;
            }
            //��ѯ������
            return null;
        }

        #endregion

        #region ȡ������סԺ��

        [System.Obsolete("����ΪGetPatientNO", true)]
        public string GetMaxPatientNo(string parm)
        {
            return null;
        }

        /// <summary>
        /// ȡ������סԺ��---wangrc
        /// </summary>
        /// <param name="parm"></param>
        /// <returns>string -��סԺ��</returns>
        public string GetMaxPatientNO(string parm)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.QueryMaxPatientNo.1", ref strSql) == -1) return null;
            #region SQL
            /*
			   select nvl(max(t.patient_no),'0000000001') from fin_ipr_inmaininfo t
				where t.parent_code = '[��������]'
				and t.current_code = '[��������]'
				and t.patient_no not like '{0}%'
				and t.patient_no not like '%B%'
				and t.patient_no not like '%F%'
				and t.patient_no not like '%L%'
				and t.patient_no not like '%C%'
				and t.patient_no not like '0009%'
                and t.in_state <> '6'
								*/
            #endregion
            strSql = String.Format(strSql, parm);
            return ExecSqlReturnOne(strSql, string.Empty);
        }

        /// <summary>
        /// ����סԺ�Ż�ȡ��Ժ����  {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
        /// </summary>
        /// <param name="patientno"></param>
        /// <returns></returns>
        public string GetInDateByPatientNo(string patientno)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.QueryPatientIndate", ref strSql) == -1) return null;

            strSql = String.Format(strSql, patientno);
            return ExecSqlReturnOne(strSql, string.Empty);
        }


        #endregion

        #region	�Զ����סԺ�� û���õ��ĵط�������ע���ˡ�

        /// <summary>
        /// ���һ���µ���ʱסԺ��-�Զ����סԺ��ʱ����
        /// </summary>
        /// <returns></returns>
        public string GetNewTempPatientNo()
        {
            #region "�ӿ�˵��"

            //���һ���µ���ʱסԺ��-�Զ����סԺ����
            //RADT.Inpatient.GetNewTempPatientNo.1
            //���룺��
            //����:��ʱסԺ��

            #endregion

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.GetNewTempPatientNo.1", ref strSql) == -1) return null;
            #region SQL
            /*
			   SELECT  nvl( 'L'||to_char(to_number(substr(max(patient_no),INSTR(max(patient_no),'L', 1, 1)+1))+1),'L1')  FROM fin_ipr_inmaininfo t where t.patient_no like '%L%'
								*/
            #endregion
            return ExecSqlReturnOne(strSql, string.Empty);
        }


        /// <summary>
        /// ���ݻ������ͻ�ȡ��ʱסԺ��-�Զ����סԺ��ʱ����// {F6204EF5-F295-4d91-B81A-736A268DD394}
        /// </summary>
        /// <returns></returns>
        public string GetNewTempPatientNoByType(string TypeCode)
        {

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.GetNewTempPatientNoByType.1", ref strSql) == -1) return null;
            strSql = string.Format(strSql, TypeCode);
            return ExecSqlReturnOne(strSql, string.Empty);
        }
        /// <summary>
        /// ���һ���µ���ֳסԺ��-�Զ����סԺ��ֳסԺ����
        /// </summary>
        /// <returns></returns>
        public string GetNewFertilityPatientNo()
        {
            #region "�ӿ�˵��"

            //���һ���µ���ʱ��ֳסԺ��-�Զ������ֳסԺ����
            //RADT.Inpatient.GetNewFertilityPatientNo.1
            //���룺��
            //����:��ֳסԺ��

            #endregion

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.GetNewFertilityPatientNo.1", ref strSql) == -1) return null;
            #region SQL
            /*
			    SELECT   'S'||nvl(to_char(to_number(substr(max(patient_no),INSTR(max(patient_no),'S', 1, 1)+1))+1),'1')  
                FROM fin_ipr_inmaininfo t 
                where t.patient_no like '%S%' and t.in_state<>'N' and t.in_state<>'C'
                and t.patient_no not like 'B%'
			*/
            #endregion
            return ExecSqlReturnOne(strSql, string.Empty);
        }
        #endregion

        #region ����סԺ������סԺ��ˮ��

        /// <summary>
        /// ����µ�סԺ��ˮ��
        /// <memo>��Ժ�Ǽ�ʱ�����뻼�ߵ�סԺ�ţ��Զ�������סԺ��סԺ��ˮ��</memo>
        /// </summary>
        /// <param name="PatientNO">����סԺ��</param>
        /// <returns>�µ�סԺ��ˮ��</returns>
        [Obsolete("����ʹ�ã����� GetNewInpatientNO()")]
        public string GetNewInpatientNO(string PatientNO)
        {
            #region "�ӿ�˵��"

            //����µ�סԺ��ˮ��
            //RADT.InPatient>GetNewInpatientNo.1
            //���룺����סԺ��
            //�������µĻ���סԺ��ˮ��

            #endregion

            ArrayList al = new ArrayList();
            string strReturn = string.Empty;
            int Max = 0;
            NeuObject obj = new NeuObject();

            try
            {
                al = QueryInpatientNOByPatientNO(PatientNO); //���סԺ��ˮ���б�
                for (int i = 0; i < al.Count; i++)
                {
                    obj = (NeuObject)al[i];
                    strReturn = obj.ID.Substring(2, 2);
                    if (Max < FS.FrameWork.Function.NConvert.ToInt32(strReturn))
                    {
                        Max = FS.FrameWork.Function.NConvert.ToInt32(strReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }
            Max++;
            strReturn = "ZY" + Max.ToString().PadLeft(2, '0') + PatientNO;

            return strReturn;
        }


        #endregion

        #region ͨ�����Ż�����סԺ��

        /// <summary>
        /// ������סԺ��by CardNO
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public string GetMaxPatientNOByCardNO(string CardNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.GetMaxInpatientNoByCardNo.1", ref strSql) == -1) return null;

            #region SQL
            /*
			Select Max(inpatient_no)
			From fin_ipr_inmaininfo
			where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and card_no = '{0}' 
			*/
            #endregion
            try
            {
                strSql = string.Format(strSql, CardNO);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }
            return ExecSqlReturnOne(strSql, string.Empty);
        }

        [System.Obsolete("����ΪGetMaxPatientNOByCardNO", true)]
        public string GetMaxInpatientNoByCardNo(string CardNO)
        {
            return null;
        }
        #endregion

        #region ��õǼǻ��ߵ����￨��

        /// <summary>
        /// ��õǼǻ��ߵ����￨�� ----------wangrc
        /// </summary>
        /// <param name="PatientNO"></param>
        /// <returns></returns>
        public string GetCardNOByPatientNO(string PatientNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.GetCardNoForEnregister.1", ref strSql) == -1) return null;
            try
            {
                #region SQL
                /*
				Select nvl(card_no,'')
				From fin_ipr_inmaininfo
				where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and patient_no = '{0}' and in_state <> '6'and rownum=1
				*/
                #endregion
                strSql = string.Format(strSql, PatientNO);
            }
            catch (Exception ex)
            {
                ErrCode = ex.Message;
                Err = ex.Message;
                WriteErr();
                return null;
            }
            return ExecSqlReturnOne(strSql, string.Empty);
        }
        [Obsolete("GetCardNoForEnregister ����Ϊ GetCardNOByPatientNO", true)]
        public string GetCardNoForEnregister(string PatientNO)
        {
            return null;
        }
        #endregion

        #region ��û��ߵ���Ժ״̬

        /// <summary>
        /// ��û��ߵ���Ժ״̬-----wangrc
        /// </summary>
        /// <param name="InpatientNO"></param>
        /// <returns></returns>
        public string GetInStateByInpatientNO(string InpatientNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.GetInStateByInpatientNo.1", ref strSql) == -1)
            {
                return null;
            }
            try
            {
                strSql = string.Format(strSql, InpatientNO);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }
            return ExecSqlReturnOne(strSql);
        }
        [Obsolete("����ΪGetInStateByInpatientNO", true)]
        public string GetInStateByInpatientNo(string InpatientNo)
        {
            return null;
        }
        /// <summary>
        /// ��û��ߵ���Ժ״̬���ݾ��￨��
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public string GetInStateByCardNo(string CardNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.GetInStateByCardNo.1", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, CardNO);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }
            return ExecSqlReturnOne(strSql);
        }

        #endregion

        #region ���סԺ���Ƿ��ظ�

        /// <summary>
        /// �����PatientNO�Ƿ��ظ�
        /// </summary>
        /// <param name="PatientNO"></param>
        /// <returns>0 ���ظ� 1�ظ�</returns>
        public int CheckIsPatientNORepeated(string PatientNO)
        {
            ArrayList al = new ArrayList();
            try
            {
                al = this.QueryInpatientNOByPatientNO(PatientNO);
                if (al.Count > 0)
                {
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }
        [Obsolete("����Ϊ CheckIsPatientNORepeated ,ԭ�� ��1 ��ʾ�ظ� ���ڸĳ� 1 Ϊ�ظ���ע����ã�", true)]
        public int CheckPatientNo(string PatientNO)
        {
            return 0;
        }
        #endregion

        #region ��סԺ�Ų�ѯ���߶��סԺ�����������Ϣ

        /// <summary>
        /// ��ѯסԺ��ˮ��-����סԺ��
        /// �����Ժ����
        /// </summary>
        /// <param name="patientNO">סԺ��</param>
        /// <returns> ���סԺ��¼ ArrayList</returns>
        public ArrayList QueryInpatientNOByPatientNO(string patientNO)
        {
            string strSql = string.Empty;

            #region �ӿ�˵��

            //RADT.Inpatient.QeryInpatientNoFromPatientNo.1
            //���룺סԺ��
            //������סԺ��ˮ�ţ���������Ժ״̬

            #endregion

            if (Sql.GetCommonSql("RADT.Inpatient.QeryInpatientNoFromPatientNo.1", ref strSql) == 0)
            {
                #region SQL
                /*	select
				 inpatient_no,name,
				 in_state,
				 DEPT_CODE,
				 dept_name,
				 in_date from fin_ipr_inmaininfo	
				 where  PARENT_CODE='[��������]'	and	CURRENT_CODE='[��������]' and  patient_no = '{0}'
				 */
                #endregion
                try
                {
                    strSql = string.Format(strSql, patientNO);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                return GetPatientInfoBySQL(strSql);
            }
            else
            {
                return null;
            }
        }
        [Obsolete("����Ϊ QueryInpatientNOByPatientNO ", true)]
        public ArrayList QueryInpatientNoFromPatientNo(string PatientNO)
        {
            return null;
        }
        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="patientNO">����סԺ��</param>
        /// <param name="bShowBaby">�Ƿ���ʾӤ��</param>
        /// <returns>���ػ�����Ϣ</returns>
        public ArrayList QueryInpatientNOByPatientNO(string patientNO, bool bShowBaby)
        {
            string strSql = string.Empty;

            #region �ӿ�˵��

            //RADT.Inpatient.QeryInpatientNoFromPatientNo.1
            //���룺סԺ��
            //������סԺ��ˮ�ţ���������Ժ״̬

            #endregion

            if (!bShowBaby)
            {
                if (Sql.GetCommonSql("RADT.Inpatient.QeryInpatientNoFromPatientNo.1", ref strSql) == 0)
                {
                    #region SQL
                    /*
					 select inpatient_no,name,in_state,DEPT_CODE,dept_name,in_date from fin_ipr_inmaininfo	where  PARENT_CODE='[��������]'	and	CURRENT_CODE='[��������]' and  patient_no = '{0}'
					 */
                    #endregion
                    try
                    {
                        strSql = string.Format(strSql, patientNO);
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        ErrCode = ex.Message;
                        WriteErr();
                        return null;
                    }
                    return GetPatientInfoBySQL(strSql);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (Sql.GetCommonSql("RADT.Inpatient.QeryInpatientNoFromPatientNo.AndBaby", ref strSql) == 0)
                {
                    #region SQL
                    /*select 
					 * inpatient_no,name,
					 * in_state,
					 * DEPT_CODE,
					 * dept_name,
					 * in_date from fin_ipr_inmaininfo	
					 * where  PARENT_CODE='[��������]'	and	CURRENT_CODE='[��������]' and  patient_no like '%'||substr('{0}',3,8) */
                    #endregion
                    try
                    {
                        strSql = string.Format(strSql, patientNO);
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        ErrCode = ex.Message;
                        WriteErr();
                        return null;
                    }
                    return GetPatientInfoBySQL(strSql);
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region ���ݴ���Sql�����סԺ���߻�����Ϣ -- ����

        protected ArrayList GetPatientInfoBySQL(string strSql)
        {
            ArrayList al = new ArrayList();
            if (ExecQuery(strSql) == -1)
            {
                return null;
            }
            while (Reader.Read())
            {
                //�޸�Ϊspellʵ�� ֻ��Ϊ�� �ܹ������Ϣ
                Spell obj = new Spell();
                obj.ID = Reader[0].ToString();
                obj.Name = Reader[1].ToString();
                obj.Memo = Reader[2].ToString();
                try
                {
                    obj.User01 = Reader[3].ToString();
                    obj.User02 = Reader[4].ToString();
                    obj.User03 = Reader[5].ToString();

                    //��λ��
                    if (this.Reader.FieldCount > 6)
                    {
                        obj.SpellCode = Reader[6].ToString();
                    }
                    //��ʿվ����
                    if (this.Reader.FieldCount > 7)
                    {
                        obj.UserCode = Reader[7].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                al.Add(obj);
            }
            Reader.Close();
            return al;
        }


        [System.Obsolete("����Ϊ GetPatientInfoBySQL", true)]
        protected ArrayList GetPatientInfo(string strSql)
        {
            return null;
        }
        #endregion

        #region ��סԺ��ˮ�Ÿ��»��߲�����Ϣ    ----  �ú����ظ�,ֻ����һ������

        /// <summary>
        /// ���²���---wangrc
        /// </summary>
        /// <param name="InpatientNO"></param>
        /// <returns></returns>
        [System.Obsolete("�Ѿ����ϣ��ú�����һ�¸��ظ���", true)]
        public int UpdateCase(string InpatientNO, bool IsCase)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.UpdateCase.1", ref strSql) == -1)
            {
                return -1;
            }

            #region SQL
            //update fin_ipr_inmaininfo set case_flag = '{1}' where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and inpatient_no = '{0}'
            #endregion
            try
            {
                strSql = string.Format(strSql, InpatientNO, NConvert.ToInt32(IsCase).ToString());
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }


        /// <summary>
        /// ���²�����CaseFlag ������״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4�������
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <param name="CaseFlag">������־</param>
        /// <returns></returns>
        public int UpdateCase(string InpatientNO, string CaseFlag)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.UpdateCase.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, InpatientNO, CaseFlag);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #region ��סԺ��ˮ�Ÿ��»�����Ժ״̬

        /// <summary>
        /// ��סԺ��ˮ�Ÿ��»�����Ժ״̬
        /// </summary>
        /// <param name="InpatientNo">סԺ��ˮ��</param>
        /// <param name="inState">��Ժ״̬</param>
        /// <param name="nurseCellCode">��ʿվ����</param>
        /// <param name="nurseCellName">��ʿվ����</param>
        /// <returns></returns>
        public int UpdateInState(string InpatientNo, string inState, string nurseCellCode, string nurseCellName)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.UpdateInState.Update", ref strSql) == -1)
            {
                return -1;
            }
            #region SQL
            /*
			 * update fin_ipr_inmaininfo set IN_STATE = '{1}',
                     nurse_cell_code = '{2}',
                     nurse_cell_name = '{3}'		                          
				where PARENT_CODE='[��������]' 
				and CURRENT_CODE='[��������]' 
				and inpatient_no = '{0}'*/
            #endregion
            try
            {
                strSql = string.Format(strSql, InpatientNo, inState, nurseCellCode, nurseCellName);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #region ɾ�����߻�����Ϣ �����ǻ�������   addby sun.xh@FS.com

        /// <summary>
        /// ɾ�����߻�����Ϣ �����ǻ�������
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int DeletePatientInfo(string cardNo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.DeletePatientInfo.1", ref strSql) == -1)
            {
                return -1;
            }
            #region SQL
            /*���˻�����Ϣ�� com_patientinfo
			  delete from com_patientinfo where card_no = '{0}'
			  */
            #endregion
            try
            {
                strSql = string.Format(strSql, cardNo);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }
            return ExecQuery(strSql);
        }

        #endregion

        #region ��סԺ��ˮ�ż��������Ƿ��в��� �ù�����ֻ�з���ֵ

        /// <summary>
        /// �Ƿ��в���
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns></returns>
        [System.Obsolete("�����ã��ù�����ֻ�з���ֵtrue ��û�к�����", true)]
        public bool QueryIsHaveCase(string InpatientNo)
        {
            return true;
        }

        #endregion

        #region ���벡�˻�����Ϣ��

        /// <summary>
        /// ���벡�˻�����Ϣ��-���ǻ������� ������com_patientinfo 
        /// </summary>
        /// <param name="PatientInfo">���߻�����Ϣ</param>
        /// <returns>�ɹ���־ 0 ʧ�ܣ�1 �ɹ�</returns>
        /// <returns></returns>
        public int InsertPatientInfo(PatientInfo PatientInfo)
        {
            #region "�ӿ�"

            //			0 ���￨��,          1 ����,              2 ƴ����,             3 ���,
            //			4 ��������,          5 �Ա�,              6 ���֤��,           7 Ѫ��,
            //			8 ְҵ,              9 ������λ,         10 ��λ�绰,          11 ��λ�ʱ�,
            //			12 ���ڻ��ͥ����,   13 ��ͥ�绰,         14 ���ڻ��ͥ��������,15 ����,
            //			16 ����,	     17 ��ϵ������,       18 ��ϵ�˵绰,        19 ��ϵ��סַ,
            //			20 ��ϵ�˹�ϵ,       21 ����״��,         22 ����,              23 �������,
            //			24 �����������,     25 ��ͬ����,         26 ��ͬ��λ����,      27 ҽ��֤��,
            //			28 ����,             29 ҽ�Ʒ���,         30 ���Ժ�,            31 ҩ�����,
            //			32 ��Ҫ����,         33 �ʻ�����,         34 �ʻ��ܶ�,          35 �����ʻ����,
            //			36 �����������,     37 Ƿ�Ѵ���,         38 Ƿ�ѽ��,          39 סԺ��Դ,
            //			40 ���סԺ����,     41 סԺ����,         42 �����Ժ����,      43 ��������,
            //			44 ����Һ�����,     45 ΥԼ����,         46 ��������,          47 ��ע,
            //			48 ����Ա,           49 ��������

            #endregion

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.CreatePatientInfo.1", ref strSql) == -1) return -1;
            try
            {
                //05024624-3FF4-44B5-92BF-BD4C6FAF6897���϶�ͯ��ǩ
                string[] s = new string[72];
                try
                {
                    s[0] = PatientInfo.PID.CardNO; //���￨��
                    s[1] = PatientInfo.Name; //����
                    s[2] = PatientInfo.SpellCode; //ƴ����
                    s[3] = PatientInfo.WBCode; //���
                    s[4] = PatientInfo.Birthday.ToString(); //��������
                    s[5] = PatientInfo.Sex.ID.ToString(); //�Ա�
                    s[6] = PatientInfo.IDCard; //���֤��
                    s[7] = PatientInfo.BloodType.ID.ToString(); //Ѫ��
                    s[8] = PatientInfo.Profession.ID; //ְҵ
                    s[9] = PatientInfo.CompanyName; //������λ
                    s[10] = PatientInfo.PhoneBusiness; //��λ�绰
                    s[11] = PatientInfo.BusinessZip; //��λ�ʱ�
                    s[12] = PatientInfo.AddressHome; //���ڻ��ͥ����
                    s[13] = PatientInfo.PhoneHome; //��ͥ�绰
                    s[14] = PatientInfo.HomeZip; //���ڻ��ͥ��������
                    s[15] = PatientInfo.DIST; //����
                    s[16] = PatientInfo.Nationality.ID; //����
                    s[17] = PatientInfo.Kin.Name; //��ϵ������
                    s[18] = PatientInfo.Kin.RelationPhone; //��ϵ�˵绰
                    s[19] = PatientInfo.Kin.RelationAddress; //��ϵ��סַ
                    s[20] = PatientInfo.Kin.Relation.ID; //��ϵ�˹�ϵ
                    s[21] = PatientInfo.MaritalStatus.ID.ToString(); //����״��
                    s[22] = PatientInfo.Country.ID; //����
                    s[23] = PatientInfo.Pact.PayKind.ID; //�������
                    s[24] = PatientInfo.Pact.PayKind.Name; //�����������
                    s[25] = PatientInfo.Pact.ID; //��ͬ����
                    s[26] = PatientInfo.Pact.Name; //��ͬ��λ����
                    s[27] = PatientInfo.SSN; //ҽ��֤��
                    s[28] = PatientInfo.AreaCode; //������
                    s[29] = PatientInfo.FT.TotCost.ToString(); //ҽ�Ʒ���
                    s[31] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsAlleray).ToString(); //ҩ�����
                    s[30] = string.Empty; //���Ժ�
                    s[32] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsMainDisease).ToString(); //��Ҫ����
                    s[33] = string.Empty; //�ʻ�����
                    s[34] = "0"; //�ʻ��ܶ�
                    s[35] = "0"; //�����ʻ����
                    s[36] = "0"; //�����������
                    s[37] = "0"; //Ƿ�Ѵ���
                    s[38] = "0"; //Ƿ�ѽ��
                    s[39] = string.Empty; //סԺ��Դ
                    s[40] = string.Empty; //���סԺ����
                    s[41] = PatientInfo.InTimes.ToString(); //סԺ����
                    s[42] = string.Empty; //�����Ժ����
                    s[43] = GetSysDateTime().ToString(); //��������
                    s[44] = string.Empty; //����Һ�����
                    s[45] = "0"; //ΥԼ����
                    s[46] = string.Empty; //��������
                    s[47] = PatientInfo.Memo; //��ע
                    s[48] = Operator.ID; //����Ա
                    s[49] = GetSysDateTime().ToString(); //��������
                    s[50] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.IsEncrypt).ToString();
                    s[51] = PatientInfo.NormalName;

                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    s[52] = PatientInfo.IDCardType.ID;//֤������
                    s[53] = NConvert.ToInt32(PatientInfo.VipFlag).ToString(); //�Ƿ�Vip
                    s[54] = PatientInfo.MatherName;//ĸ������
                    s[55] = NConvert.ToInt32(PatientInfo.IsTreatment).ToString(); //�Ƿ��ﻼ��
                    //s[56] = PatientInfo.CaseNO;//������
                    s[56] = PatientInfo.PID.CaseNO;//������
                    //{112F6B96-DC1D-4e20-8290-0403A25B443C}
                    s[57] = PatientInfo.Insurance.ID; //���չ�˾����
                    s[58] = PatientInfo.Insurance.Name; //���չ�˾����
                    s[59] = PatientInfo.Kin.RelationDoorNo;//��ϵ�˵�ַ���ƺ�
                    s[60] = PatientInfo.AddressHomeDoorNo;//��ͥסַ���ƺ�
                    s[61] = PatientInfo.Email;//email��ַ
                    s[62] = PatientInfo.User01;//��סַ

                    // {793CA9DB-FD85-460a-B8B4-971C31FFAD45}

                    s[63] = PatientInfo.FamilyCode;//��ͥ��
                    s[64] = PatientInfo.OtherCardNo;//��������
                    s[65] = PatientInfo.ServiceInfo.ID;//�ͷ�רԱ
                    s[66] = PatientInfo.ServiceInfo.Name;//
                    s[67] = PatientInfo.PatientSourceInfo.ID;//������Դ
                    s[68] = PatientInfo.ReferralPerson;//ת����
                    s[69] = PatientInfo.ChannelInfo.ID;//��������

                    s[70] = PatientInfo.FamilyName;//��ͥ��
                    //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ
                    s[71] = PatientInfo.ChildFlag;
                    //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//����̱���˾
                    //s[71] = PatientInfo.BICompanyID;//�̱���˾���
                    //s[72] = PatientInfo.BICompanyName;//�̱���˾����

                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                }
                //strSql = string.Format(strSql, s);
                return ExecNoQuery(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

        }
        [System.Obsolete("����Ϊ InsertPatientInfo", true)]
        public int CreatePatientInfo(PatientInfo PatientInfo)
        {
            return -1;
        }
        #endregion
        #region  ��ѯ���˻�����Ϣ com_patientinfo��
        public FS.HISFC.Models.RADT.PatientInfo GetPatient(string CardNO)
        {
            FS.HISFC.Models.RADT.PatientInfo obj = new PatientInfo();
            string strSql = "";
            if (this.Sql.GetCommonSql("RADT.InPatient.GetPatient.1", ref strSql) == -1) return null;
            strSql = string.Format(strSql, CardNO);
            if (this.ExecQuery(strSql) == -1) return null;
            while (this.Reader.Read())
            {
                obj.PID.CardNO = Reader[0].ToString(); //���� 
                obj.Name = Reader[1].ToString();//����
            }
            this.Reader.Close();
            return obj;
        }
        #endregion
        #region ���»��߻�����Ϣ

        /// <summary>
        /// ���»�����Ϣ�����ǻ�������  ������com_patientinfo
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns></returns>
        public int UpdatePatientInfo(PatientInfo PatientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdatePatientInfo.1", ref strSql) == -1)
            {
                this.Err = "δ�ҵ�SQL��䣺RADT.InPatient.UpdatePatientInfo.1";
                return -1;
            }

            try
            {
                //05024624-3FF4-44B5-92BF-BD4C6FAF6897���϶�ͯ��ǩ
                string[] s = new string[72];
                try
                {
                    s[0] = PatientInfo.PID.CardNO; //���￨��
                    s[1] = PatientInfo.Name; //����
                    s[2] = PatientInfo.SpellCode; //ƴ����
                    s[3] = PatientInfo.WBCode; //���
                    s[4] = PatientInfo.Birthday.ToString(); //��������
                    s[5] = PatientInfo.Sex.ID.ToString(); //�Ա�
                    s[6] = PatientInfo.IDCard; //���֤��
                    s[7] = PatientInfo.BloodType.ID.ToString(); //Ѫ��
                    s[8] = PatientInfo.Profession.ID; //ְҵ
                    s[9] = PatientInfo.CompanyName; //������λ
                    s[10] = PatientInfo.PhoneBusiness; //��λ�绰
                    s[11] = PatientInfo.BusinessZip; //��λ�ʱ�
                    s[12] = PatientInfo.AddressHome; //���ڻ��ͥ����
                    s[13] = PatientInfo.PhoneHome; //��ͥ�绰
                    s[14] = PatientInfo.HomeZip; //���ڻ��ͥ��������
                    s[15] = PatientInfo.DIST; //����
                    s[16] = PatientInfo.Nationality.ID; //����
                    s[17] = PatientInfo.Kin.Name; //��ϵ������
                    s[18] = PatientInfo.Kin.RelationPhone; //��ϵ�˵绰
                    s[19] = PatientInfo.Kin.RelationAddress; //��ϵ��סַ
                    s[20] = PatientInfo.Kin.Relation.ID; //��ϵ�˹�ϵ
                    s[21] = PatientInfo.MaritalStatus.ID.ToString(); //����״��
                    s[22] = PatientInfo.Country.ID; //����
                    s[23] = PatientInfo.Pact.PayKind.ID; //�������
                    s[24] = PatientInfo.Pact.PayKind.Name; //�����������
                    s[25] = PatientInfo.Pact.ID; //��ͬ����
                    s[26] = PatientInfo.Pact.Name; //��ͬ��λ����
                    s[27] = PatientInfo.SSN; //ҽ��֤��
                    s[28] = PatientInfo.AreaCode; //������
                    s[29] = PatientInfo.FT.TotCost.ToString(); //ҽ�Ʒ���
                    s[31] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsAlleray).ToString(); //ҩ�����
                    s[30] = string.Empty; //���Ժ�
                    s[32] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsMainDisease).ToString(); //��Ҫ����
                    s[33] = string.Empty; //�ʻ�����
                    s[34] = "0"; //�ʻ��ܶ�
                    s[35] = "0"; //�����ʻ����
                    s[36] = "0"; //�����������
                    s[37] = "0"; //Ƿ�Ѵ���
                    s[38] = "0"; //Ƿ�ѽ��
                    s[39] = string.Empty; //סԺ��Դ
                    s[40] = GetSysDateTime(); //���סԺ����
                    s[41] = PatientInfo.InTimes.ToString(); //סԺ����
                    s[42] = GetSysDateTime(); //�����Ժ����
                    s[43] = GetSysDateTime().ToString(); //��������
                    s[44] = GetSysDateTime(); //����Һ�����
                    s[45] = "0"; //ΥԼ����
                    s[46] = GetSysDateTime(); //��������
                    s[47] = PatientInfo.Memo; //��ע
                    s[48] = Operator.ID; //����Ա
                    s[49] = GetSysDateTime().ToString(); //��������
                    s[50] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.IsEncrypt).ToString();
                    s[51] = PatientInfo.NormalName;

                    s[52] = PatientInfo.IDCardType.ID;//֤������
                    s[53] = NConvert.ToInt32(PatientInfo.VipFlag).ToString(); //�Ƿ�Vip
                    s[54] = PatientInfo.MatherName;//ĸ������
                    s[55] = NConvert.ToInt32(PatientInfo.IsTreatment).ToString(); //�Ƿ��ﻼ��
                    //s[56] = PatientInfo.CaseNO;//������
                    s[56] = PatientInfo.PID.CaseNO;//������
                    //{112F6B96-DC1D-4e20-8290-0403A25B443C}
                    s[57] = PatientInfo.Insurance.ID;//���չ�˾����
                    s[58] = PatientInfo.Insurance.Name;//���չ�˾����
                    s[59] = PatientInfo.Kin.RelationDoorNo;//��ϵ�˵�ַ���ƺ�
                    s[60] = PatientInfo.AddressHomeDoorNo;//��ͥסַ���ƺ�
                    s[61] = PatientInfo.Email; //email��ַ
                    s[62] = PatientInfo.User01;//��סַ

                    // {793CA9DB-FD85-460a-B8B4-971C31FFAD45}
                    s[63] = PatientInfo.FamilyCode;//��ͥ��
                    s[64] = PatientInfo.OtherCardNo;//��������
                    s[65] = PatientInfo.ServiceInfo.ID;//�ͷ�רԱ
                    s[66] = PatientInfo.ServiceInfo.Name;//
                    s[67] = PatientInfo.PatientSourceInfo.ID;//������Դ
                    s[68] = PatientInfo.ReferralPerson;//ת����
                    s[69] = PatientInfo.ChannelInfo.ID;//��������
                    s[70] = PatientInfo.FamilyName;//��ͥ����
                    //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//����̱���˾
                    //s[71] = PatientInfo.BICompanyID;//�̱���˾���
                    //s[72] = PatientInfo.BICompanyName;//�̱���˾����
                    ////{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ
                    s[71] = PatientInfo.ChildFlag;

                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    return -1;
                }
                return ExecNoQuery(strSql, s);
                //strSql = string.Format(strSql);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }


        }

        /// <summary>
        /// ���»�����Ϣ��סԺ�ã������ǻ�������  ������com_patientinfo
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns></returns>
        public int UpdatePatientInfoForInpatient(PatientInfo PatientInfo)
        {
            string strSql = string.Empty;
            if (!PatientInfo.PID.PatientNO.Contains("B"))
            {
                if (Sql.GetCommonSql("RADT.InPatient.UpdatePatientInfo.2", ref strSql) == -1)
                {
                    this.Err = "δ�ҵ�SQL��䣺RADT.InPatient.UpdatePatientInfo.2";
                    return -1;
                }

                try
                {
                    string[] s = new string[62];
                    try
                    {
                        s[0] = PatientInfo.PID.CardNO; //���￨��
                        s[1] = PatientInfo.Name; //����
                        s[2] = PatientInfo.SpellCode; //ƴ����
                        s[3] = PatientInfo.WBCode; //���
                        s[4] = PatientInfo.Birthday.ToString(); //��������
                        s[5] = PatientInfo.Sex.ID.ToString(); //�Ա�
                        s[6] = PatientInfo.IDCard; //���֤��
                        s[7] = PatientInfo.BloodType.ID.ToString(); //Ѫ��
                        s[8] = PatientInfo.Profession.ID; //ְҵ
                        s[9] = PatientInfo.CompanyName; //������λ
                        s[10] = PatientInfo.PhoneBusiness; //��λ�绰
                        s[11] = PatientInfo.BusinessZip; //��λ�ʱ�
                        s[12] = PatientInfo.AddressHome; //���ڻ��ͥ����
                        s[13] = PatientInfo.PhoneHome; //��ͥ�绰
                        s[14] = PatientInfo.HomeZip; //���ڻ��ͥ��������
                        s[15] = PatientInfo.DIST; //����
                        s[16] = PatientInfo.Nationality.ID; //����
                        s[17] = PatientInfo.Kin.Name; //��ϵ������
                        s[18] = PatientInfo.Kin.RelationPhone; //��ϵ�˵绰
                        s[19] = PatientInfo.Kin.RelationAddress; //��ϵ��סַ
                        s[20] = PatientInfo.Kin.Relation.ID; //��ϵ�˹�ϵ
                        s[21] = PatientInfo.MaritalStatus.ID.ToString(); //����״��
                        s[22] = PatientInfo.Country.ID; //����
                        s[23] = PatientInfo.Pact.PayKind.ID; //�������
                        s[24] = PatientInfo.Pact.PayKind.Name; //�����������
                        s[25] = PatientInfo.Pact.ID; //��ͬ����
                        s[26] = PatientInfo.Pact.Name; //��ͬ��λ����
                        s[27] = PatientInfo.SSN; //ҽ��֤��
                        s[28] = PatientInfo.AreaCode; //������
                        s[29] = PatientInfo.FT.TotCost.ToString(); //ҽ�Ʒ���
                        s[31] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsAlleray).ToString(); //ҩ�����
                        s[30] = string.Empty; //���Ժ�
                        s[32] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsMainDisease).ToString(); //��Ҫ����
                        s[33] = string.Empty; //�ʻ�����
                        s[34] = "0"; //�ʻ��ܶ�
                        s[35] = "0"; //�����ʻ����
                        s[36] = "0"; //�����������
                        s[37] = "0"; //Ƿ�Ѵ���
                        s[38] = "0"; //Ƿ�ѽ��
                        s[39] = string.Empty; //סԺ��Դ
                        s[40] = GetSysDateTime(); //���סԺ����
                        s[41] = PatientInfo.InTimes.ToString(); //סԺ����
                        s[42] = GetSysDateTime(); //�����Ժ����
                        s[43] = GetSysDateTime().ToString(); //��������
                        s[44] = GetSysDateTime(); //����Һ�����
                        s[45] = "0"; //ΥԼ����
                        s[46] = GetSysDateTime(); //��������
                        s[47] = PatientInfo.Memo; //��ע
                        s[48] = Operator.ID; //����Ա
                        s[49] = GetSysDateTime().ToString(); //��������
                        s[50] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.IsEncrypt).ToString();
                        s[51] = PatientInfo.NormalName;

                        s[52] = PatientInfo.IDCardType.ID;//֤������
                        s[53] = NConvert.ToInt32(PatientInfo.VipFlag).ToString(); //�Ƿ�Vip
                        s[54] = PatientInfo.MatherName;//ĸ������
                        s[55] = NConvert.ToInt32(PatientInfo.IsTreatment).ToString(); //�Ƿ��ﻼ��
                        //s[56] = PatientInfo.CaseNO;//������
                        s[56] = PatientInfo.PID.CaseNO;//������
                        //{112F6B96-DC1D-4e20-8290-0403A25B443C}
                        s[57] = PatientInfo.Insurance.ID;//���չ�˾����
                        s[58] = PatientInfo.Insurance.Name;//���չ�˾����
                        s[59] = PatientInfo.Kin.RelationDoorNo;//��ϵ�˵�ַ���ƺ�
                        s[60] = PatientInfo.AddressHomeDoorNo;//��ͥסַ���ƺ�
                        s[61] = PatientInfo.Email; //email��ַ

                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        return -1;
                    }
                    return ExecNoQuery(strSql, s);
                    //strSql = string.Format(strSql);
                }
                catch (Exception ex)
                {
                    Err = "��ֵʱ�����" + ex.Message;
                    WriteErr();
                    return -1;
                }
            }
            else
            {

                if (Sql.GetCommonSql("RADT.InPatient.UpdateBabyPatientInfo.2", ref strSql) == -1)
                {
                    this.Err = "δ�ҵ�SQL��䣺RADT.InPatient.UpdateBabyPatientInfo.2";
                    return -1;
                }

                try
                {
                    string[] s = new string[7];
                    try
                    {
                        s[0] = PatientInfo.ID;
                        s[1] = PatientInfo.Name;
                        s[2] = PatientInfo.Sex.ID.ToString();
                        s[3] = PatientInfo.Birthday.ToString();
                        s[4] = PatientInfo.Height;
                        s[5] = PatientInfo.Weight;
                        s[6] = PatientInfo.BloodType.ID.ToString();
                        //  strSql = string.Format(strSql, s);
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        return -1;
                    }


                    return ExecNoQuery(strSql, s);

                    //strSql = string.Format(strSql);
                }
                catch (Exception ex)
                {
                    Err = "��ֵʱ�����" + ex.Message;
                    WriteErr();
                    return -1;
                }
            }


        }

        #endregion

        #region ����סԺ����

        /// <summary>
        /// ����סԺ���� 
        /// </summary>
        /// <param name="PatientInfo">����סԺ����</param>
        /// <returns>0�ɹ� -1ʧ��</returns>���߻�����Ϣ
        public int InsertPatient(PatientInfo PatientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.RegisterPatient.New.1", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[82]; //{23F37636-DC34-44a3-A13B-071376265450}
                try
                {
                    s[0] = PatientInfo.ID; // --סԺ��ˮ��
                    s[1] = PatientInfo.Name; //--����
                    s[2] = PatientInfo.PID.PatientNO; //  --סԺ��
                    s[3] = PatientInfo.PID.CardNO; //  --���￨��
                    s[4] = PatientInfo.SSN; //  --ҽ��֤��
                    s[5] = PatientInfo.PVisit.MedicalType.ID; //    --ҽ�����id zhouxs
                    s[6] = PatientInfo.Sex.ID.ToString(); //  --�Ա�
                    s[7] = PatientInfo.IDCard; //  --���֤��
                    s[8] = PatientInfo.Memo; //  --��ע
                    s[9] = PatientInfo.Birthday.ToString(); //  --����
                    s[10] = PatientInfo.Profession.ID; //  --ְҵ����
                    s[11] = PatientInfo.CompanyName; //  --������λ
                    s[12] = PatientInfo.PhoneBusiness; //  --������λ�绰
                    s[13] = PatientInfo.BusinessZip; //  --��λ�ʱ�
                    s[14] = PatientInfo.AddressHome; //  --���ڻ��ͥ��ַ
                    s[15] = PatientInfo.PhoneHome; //  --��ͥ�绰
                    s[16] = PatientInfo.HomeZip; //  --���ڻ��ͥ�ʱ�
                    s[17] = PatientInfo.DIST; // --����name
                    s[18] = PatientInfo.AreaCode; //  --�����ش���---����
                    s[19] = PatientInfo.Nationality.ID; //  --����id
                    s[20] = PatientInfo.Nationality.Name; // --����name
                    s[21] = PatientInfo.Kin.Name; //  --��ϵ������
                    s[22] = PatientInfo.Kin.RelationPhone; //  --��ϵ�˵绰
                    s[23] = PatientInfo.Kin.RelationAddress; //  --��ϵ�˵�ַ
                    s[24] = PatientInfo.Kin.Relation.ID; //  --��ϵ�˹�ϵid
                    s[25] = PatientInfo.Kin.Relation.Name; //  --��ϵ�˹�ϵname
                    s[26] = PatientInfo.MaritalStatus.ID.ToString(); //  --����״��id
                    s[27] = PatientInfo.MaritalStatus.Name; // --����״��name
                    s[28] = PatientInfo.Country.ID; //  --����id
                    s[29] = PatientInfo.Country.Name; //--��������
                    s[30] = PatientInfo.Height; //  --���
                    s[31] = PatientInfo.Weight; //  --����
                    s[32] = PatientInfo.Profession.ID; //  --ְҵid
                    s[33] = PatientInfo.BloodType.ID.ToString(); //  --ABOѪ��
                    s[34] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsMainDisease).ToString(); //  --�ش󼲲���־
                    s[35] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsAlleray).ToString(); //  --������־
                    s[36] = PatientInfo.PVisit.InTime.ToString(); //  --��Ժ����
                    s[37] = PatientInfo.PVisit.PatientLocation.Dept.ID; //  --���Ҵ���
                    s[38] = PatientInfo.PVisit.PatientLocation.Dept.Name; // --��������
                    s[39] = PatientInfo.Pact.PayKind.ID; // --�������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
                    s[40] = PatientInfo.Pact.PayKind.Name; //  --�����������
                    s[41] = PatientInfo.Pact.ID; // --��ͬ����
                    s[42] = PatientInfo.Pact.Name; // --��ͬ��λ����
                    s[43] = PatientInfo.PVisit.PatientLocation.Bed.ID; // --����
                    s[44] = PatientInfo.PVisit.PatientLocation.NurseCell.ID; //--����Ԫ����
                    s[45] = PatientInfo.PVisit.PatientLocation.NurseCell.Name; // --����Ԫ����
                    s[46] = PatientInfo.PVisit.AdmittingDoctor.ID; //--ҽʦ����(סԺ)
                    s[47] = PatientInfo.PVisit.AdmittingDoctor.Name; //--ҽʦ����(סԺ)
                    s[48] = PatientInfo.PVisit.AttendingDoctor.ID; // --ҽʦ����(����)
                    s[49] = PatientInfo.PVisit.AttendingDoctor.Name; //--ҽʦ����(����)
                    s[50] = PatientInfo.PVisit.ConsultingDoctor.ID; // --ҽʦ����(����)
                    s[51] = PatientInfo.PVisit.ConsultingDoctor.Name; //--ҽʦ����(����)
                    s[52] = PatientInfo.PVisit.TempDoctor.ID; //--ҽʦ����(ʵϰ)
                    s[53] = PatientInfo.PVisit.TempDoctor.Name; //--ҽʦ����(ʵϰ)
                    s[54] = PatientInfo.PVisit.AdmittingNurse.ID; // --��ʿ����(����)
                    s[55] = PatientInfo.PVisit.AdmittingNurse.Name; // --��ʿ����(����)
                    s[56] = PatientInfo.PVisit.Circs.ID; // --��Ժ���id
                    s[57] = PatientInfo.PVisit.Circs.Name; // --��Ժ���name
                    s[58] = PatientInfo.PVisit.AdmitSource.ID; // --��Ժ;��id
                    s[59] = PatientInfo.PVisit.AdmitSource.Name; // --��Ժ;��name
                    s[60] = PatientInfo.PVisit.InSource.ID; // --��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ
                    s[61] = PatientInfo.PVisit.InSource.Name; // --��Ժ��Դname
                    s[62] = PatientInfo.PVisit.InState.ID.ToString(); // --סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
                    s[63] = PatientInfo.PVisit.PreOutTime.ToString(); // --��Ժ����(ԤԼ)
                    s[64] = PatientInfo.PVisit.OutTime.ToString(); // --��Ժ����
                    if (PatientInfo.PVisit.ICULocation == null)
                    {
                        s[65] = "0";
                    }
                    else
                    {
                        s[65] = "1"; // --�Ƿ���ICU
                    }
                    s[66] = Operator.ID;
                    s[67] = PatientInfo.DoctorReceiver.ID; //��סҽʦ
                    s[68] = PatientInfo.InTimes.ToString(); //סԺ����
                    s[69] = PatientInfo.FT.FixFeeInterval.ToString(); //���Ѽ��
                    s[70] = PatientInfo.ClinicDiagnose; //�������

                    try
                    {
                        s[71] = PatientInfo.ExtendFlag; //�Ƿ��������޶�� 0 ��ͬ�� 1 ͬ��
                        s[72] = PatientInfo.ExtendFlag1;
                        s[73] = PatientInfo.ExtendFlag2;
                    }
                    catch
                    {
                    }
                    s[74] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.IsEncrypt).ToString();
                    s[75] = PatientInfo.NormalName;
                    s[76] = PatientInfo.AddressBusiness;//��סַ
                    s[77] = PatientInfo.PatientType.ID;//��������// {F6204EF5-F295-4d91-B81A-736A268DD394}
                    s[78] = PatientInfo.Hospital_id; //{23F37636-DC34-44a3-A13B-071376265450}
                    s[79] = PatientInfo.Hospital_name;
                    s[80] = PatientInfo.PVisit.ResponsibleDoctor.ID; //����ҽʦ����
                    s[81] = PatientInfo.PVisit.ResponsibleDoctor.Name; //����ҽʦ����
                }
                catch (Exception ex)
                {
                    Err = "��ֵʱ�����" + ex.Message;
                    WriteErr();
                    return -1;
                }
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }
        [System.Obsolete("����Ϊ InsertPatient", true)]
        public int RegisterPatient(PatientInfo PatientInfo)
        {
            return 0;
        }
        #endregion

        #region ����סԺ����

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns></returns>
        public int UpdatePatient(PatientInfo PatientInfo)
        {
            #region "�ӿ�˵��"

            //			/�ӿ����� RADT.InPatient.UpdatePatient.1
            //			/max 66
            //					<!-- 0  --סԺ��ˮ��,1 --���� 2   --סԺ��   ,3   --���￨��  ,4   --ҽ��֤��
            //			    ,5     --ҽ�����,   ,6   --�Ա�   ,7   --���֤��  ,8   --ƴ��     ,9   --����
            //			    ,10   --ְҵ����     ,11   --������λ    ,12   --������λ�绰      ,13   --��λ�ʱ�
            //			    ,14   --���ڻ��ͥ��ַ     ,15   --��ͥ�绰   ,16   --���ڻ��ͥ�ʱ�   , 17  --����name
            //			    ,18   --�����ش���        ,19   --����id    ,20  --����name    ,21   --��ϵ������
            //			    ,22   --��ϵ�˵绰       ,23   --��ϵ�˵�ַ     ,24   --��ϵ�˹�ϵid , 25   --��ϵ�˹�ϵid 
            //			    ,26   --����״��id              ,27  --����״��name  ,28   --����id     29 --��������
            //			    ,30   --���           ,31   --����         ,32   -- ְλid    ,33   --ABOѪ��
            //			    ,34   --�ش󼲲���־    ,35   --������־            
            //			    ,36   --��Ժ����      ,37   --���Ҵ���   , 38  --��������  , 39  --�������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
            //			    ,40   --�����������   , 41  --��ͬ����   , 42  --��ͬ��λ����  , 43  --����
            //			   , 44 --����Ԫ����  , 45  --����Ԫ����, 46 --ҽʦ����(סԺ), 47 --ҽʦ����(סԺ)
            //			   , 48 --ҽʦ����(����) , 49 --ҽʦ����(����) , 50 --ҽʦ����(����) , 51 --ҽʦ����(����)
            //			   , 52 --ҽʦ����(ʵϰ) , 53 --ҽʦ����(ʵϰ), 54  --��ʿ����(����), 55  --��ʿ����(����)
            //			   , 56  --��Ժ���id  , 57  --��Ժ���name   , 58  --��Ժ;��id    , 59  --��Ժ;��name      
            //			   , 60  --��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ    , 61  --��Ժ��Դname
            //			   , 62  --סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
            //			  ,  63  --��Ժ����(ԤԼ)  , 64  --��Ժ���� , 65  --�Ƿ���ICU 0 no 1 yes ,66 ����Ա -->

            #endregion

            string strSql = string.Empty;// {F6204EF5-F295-4d91-B81A-736A268DD394}
            if (Sql.GetCommonSql("RADT.InPatient.UpdatePatient.New.2", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[90];
                try
                {
                    s[0] = PatientInfo.ID; // --סԺ��ˮ��
                    s[1] = PatientInfo.Name; //--����
                    s[2] = PatientInfo.PID.PatientNO; //  --סԺ��
                    s[3] = PatientInfo.PID.CardNO; //  --���￨��
                    s[4] = PatientInfo.SSN; //  --ҽ��֤��
                    s[5] = PatientInfo.PVisit.MedicalType.ID; //    --ҽ�����id
                    s[6] = PatientInfo.Sex.ID.ToString(); //  --�Ա�
                    s[7] = PatientInfo.IDCard; //  --���֤��
                    s[8] = PatientInfo.SpellCode; //  --ƴ��
                    s[9] = PatientInfo.Birthday.ToString(); //  --����
                    s[10] = PatientInfo.Profession.ID; //  --ְҵ����
                    s[11] = PatientInfo.CompanyName; //  --������λ
                    s[12] = PatientInfo.PhoneBusiness; //  --������λ�绰
                    s[13] = PatientInfo.BusinessZip; //  --��λ�ʱ�
                    s[14] = PatientInfo.AddressHome; //  --���ڻ��ͥ��ַ
                    s[15] = PatientInfo.PhoneHome; //  --��ͥ�绰
                    s[16] = PatientInfo.HomeZip; //  --���ڻ��ͥ�ʱ�
                    s[17] = PatientInfo.DIST; // --����name
                    s[18] = PatientInfo.AreaCode; //  --�����ش���
                    s[19] = PatientInfo.Nationality.ID; //  --����id
                    s[20] = PatientInfo.Nationality.Name; // --����name
                    s[21] = PatientInfo.Kin.Name; //  --��ϵ������
                    s[22] = PatientInfo.Kin.RelationPhone; //  --��ϵ�˵绰
                    s[23] = PatientInfo.Kin.RelationAddress; //  --��ϵ�˵�ַ
                    s[24] = PatientInfo.Kin.Relation.ID; //  --��ϵ�˹�ϵid
                    s[25] = PatientInfo.Kin.Relation.Name; //  --��ϵ�˹�ϵname
                    s[26] = PatientInfo.MaritalStatus.ID.ToString(); //  --����״��id
                    s[27] = PatientInfo.MaritalStatus.Name; // --����״��name
                    s[28] = PatientInfo.Country.ID; //  --����id
                    s[29] = PatientInfo.Country.Name; //--��������
                    s[30] = PatientInfo.Height; //  --���
                    s[31] = PatientInfo.Weight; //  --����
                    s[32] = PatientInfo.Profession.Name; //  --ְҵid
                    s[33] = PatientInfo.BloodType.ID.ToString(); //  --ABOѪ��
                    s[34] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsMainDisease).ToString(); //  --�ش󼲲���־
                    s[35] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsAlleray).ToString(); //  --������־
                    s[36] = PatientInfo.PVisit.InTime.ToString(); //  --��Ժ����
                    s[37] = PatientInfo.PVisit.PatientLocation.Dept.ID; //  --���Ҵ���
                    s[38] = PatientInfo.PVisit.PatientLocation.Dept.Name; // --��������
                    s[39] = PatientInfo.Pact.PayKind.ID; // --�������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
                    s[40] = PatientInfo.Pact.PayKind.Name; //  --�����������
                    s[41] = PatientInfo.Pact.ID; // --��ͬ����
                    s[42] = PatientInfo.Pact.Name; // --��ͬ��λ����
                    s[43] = PatientInfo.PVisit.PatientLocation.Bed.ID; // --����
                    s[44] = PatientInfo.PVisit.PatientLocation.NurseCell.ID; //--����Ԫ����
                    s[45] = PatientInfo.PVisit.PatientLocation.NurseCell.Name; // --����Ԫ����
                    s[46] = PatientInfo.PVisit.AdmittingDoctor.ID; //--ҽʦ����(סԺ)
                    s[47] = PatientInfo.PVisit.AdmittingDoctor.Name; //--ҽʦ����(סԺ)
                    s[48] = PatientInfo.PVisit.AttendingDoctor.ID; // --ҽʦ����(����)
                    s[49] = PatientInfo.PVisit.AttendingDoctor.Name; //--ҽʦ����(����)
                    s[50] = PatientInfo.PVisit.ConsultingDoctor.ID; // --ҽʦ����(����)
                    s[51] = PatientInfo.PVisit.ConsultingDoctor.Name; //--ҽʦ����(����)
                    s[52] = PatientInfo.PVisit.TempDoctor.ID; //--ҽʦ����(ʵϰ)
                    s[53] = PatientInfo.PVisit.TempDoctor.Name; //--ҽʦ����(ʵϰ)
                    s[54] = PatientInfo.PVisit.AdmittingNurse.ID; // --��ʿ����(����)
                    s[55] = PatientInfo.PVisit.AdmittingNurse.Name; // --��ʿ����(����)
                    s[56] = PatientInfo.PVisit.Circs.ID; // --��Ժ���id
                    s[57] = PatientInfo.PVisit.Circs.Name; // --��Ժ���name
                    s[58] = PatientInfo.PVisit.AdmitSource.ID; // --��Ժ;��id
                    s[59] = PatientInfo.PVisit.AdmitSource.Name; // --��Ժ;��name
                    s[60] = PatientInfo.PVisit.InSource.ID; // --��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ
                    s[61] = PatientInfo.PVisit.InSource.Name; // --��Ժ��Դname
                    s[62] = PatientInfo.PVisit.InState.ID.ToString(); // --סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
                    s[63] = PatientInfo.PVisit.PreOutTime.ToString(); // --��Ժ����(ԤԼ)
                    s[64] = PatientInfo.PVisit.OutTime.ToString(); // --��Ժ����
                    try
                    {
                        if (PatientInfo.PVisit.ICULocation == null)
                        {
                            s[65] = "0";
                        }
                        else
                        {
                            s[65] = "1"; // --�Ƿ���ICU
                        }
                        s[66] = Operator.ID;
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                    }
                    s[67] = PatientInfo.Memo;
                    s[68] = PatientInfo.FT.BloodLateFeeCost.ToString(); //Ѫ���ɽ�
                    s[69] = PatientInfo.FT.BedLimitCost.ToString(); //��λ����
                    s[70] = PatientInfo.FT.AirLimitCost.ToString(); //�յ�����
                    s[71] = PatientInfo.ProCreateNO; //�������յ��Ժ�
                    s[72] = PatientInfo.FT.FixFeeInterval.ToString(); //���Ѽ��
                    s[73] = PatientInfo.FT.BedOverDeal.ToString(); //���괦��
                    s[74] = PatientInfo.ExtendFlag; //�Ƿ��������޶�� 0 ��ͬ�� 1 ͬ��
                    s[75] = PatientInfo.ExtendFlag1;
                    s[76] = PatientInfo.ExtendFlag2;
                    s[77] = PatientInfo.ClinicDiagnose; //�������
                    s[78] = PatientInfo.MainDiagnose; //סԺ�����
                    s[79] = PatientInfo.DoctorReceiver.ID;//��סҽʦ
                    s[80] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.IsEncrypt).ToString();
                    s[81] = PatientInfo.NormalName;

                    s[82] = PatientInfo.IDCardType.ID;//֤������z
                    s[83] = PatientInfo.FT.DayLimitCost.ToString(); //����ҩƷ���޶�
                    s[84] = PatientInfo.AddressBusiness;//��סַ
                    s[85] = PatientInfo.FT.DayLimitCost.ToString();//���޶�
                    s[86] = PatientInfo.AllergyInfo;//����Դ
                    s[87] = PatientInfo.PatientType.ID;//��������// {F6204EF5-F295-4d91-B81A-736A268DD394}

                    s[88] = PatientInfo.PVisit.ResponsibleDoctor.ID;//����ҽʦ����
                    s[89] = PatientInfo.PVisit.ResponsibleDoctor.Name;//����ҽʦ����

                    strSql = string.Format(strSql, s);

                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                }
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            int parm = ExecNoQuery(strSql);
            if (parm != 1)
            {
                return parm;
            }

            //���Ӥ��������Ϣ�޸�,��ͬʱ����Ӥ�����е���Ϣ
            if (PatientInfo.PID.PatientNO.Contains("B"))
            {
                return UpdateBabyInfo(PatientInfo);
            }

            return 1;
        }

        /// <summary>
        /// ����סԺ����
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public int UpdatePatientInTimes(string inPatientNO, int times)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdatePatient.InTimes", ref strSql) == -1)
            {
                return -1;
            }

            return ExecNoQuery(strSql, inPatientNO, times.ToString());
        }


        /// <summary>
        /// �����������Ϣ,���»����������к��ӵ�סԺ������Ϣ
        /// �ڻ��߿���,��λ��סԺ״̬�����仯��ʱ�����
        /// UpdatePatient
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int UpdatePatientByMother(PatientInfo patientInfo)
        {
            //ȡ��������к�����Ϣ
            ArrayList al = QueryBabiesByMother(patientInfo.ID);
            if (al == null)
            {
                return -1;
            }

            int parm;
            //����ÿ��Ӥ����סԺ������Ϣ
            foreach (PatientInfo baby in al)
            {
                //��������Ѿ������˳�Ժ��������޷���Ժ,�򲻶Դ�Ӥ�����д���    ���ڳ�Ժ�ǼǵĻ���Ҳ��Ӧ���� add by niuxinyuan  2007-03-16
                if (baby.PVisit.InState.ID.ToString() == "O"
                    || baby.PVisit.InState.ID.ToString() == "N"
                    || baby.PVisit.InState.ID.ToString() == "B"
                    )
                {
                    continue;
                }

                //������Ŀ�����Ϣ��������
                baby.PVisit.PatientLocation = patientInfo.PVisit.PatientLocation;
                //���������Ժ״̬��������
                baby.PVisit.InState = patientInfo.PVisit.InState;

                //ת�����
                baby.PVisit.ZG = patientInfo.PVisit.ZG;
                //��Ժ���� houwb û�и��µ���Ժ���ڵ��� �����б�����Ӥ��
                baby.PVisit.PreOutTime = patientInfo.PVisit.PreOutTime;
                baby.PVisit.OutTime = patientInfo.PVisit.PreOutTime;

                //����Ӥ��סԺ������Ϣ
                parm = UpdatePatient(baby);
                if (parm == -1)
                {
                    return parm;
                }
            }
            return 1;
        }

        /// <summary>
        /// //����Ӥ����Ϣ�������ҽʦ�����Լ�����ҽʦ����
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int UpdateBabyResponsibleDoc(PatientInfo patientInfo)
        {
            string strSql = @" update     fin_ipr_babyinfo   --Ӥ��סԺ����
            set       RESPONSIBLE_DOC_CODE = '{1}',
				      RESPONSIBLE_DOC_NAME = '{2}',
                      oper_date = sysdate
             where inpatient_no = '{0}'
                            ";

            strSql = string.Format(strSql, patientInfo.ID, patientInfo.PVisit.ResponsibleDoctor.ID,patientInfo.PVisit.ResponsibleDoctor.Name);

            if (ExecQuery(strSql) == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// CA���ղ���fin_com_ca  by ren.jch 2015-01-15
        /// </summary>
        /// <param name="emplcode"></param>
        /// <param name="cacode"></param>
        /// <returns></returns> 
        public int InsertCACompare(string emplcode, string cacode)
        {
            string strSql = "";
            /*
             INSERT INTO fin_com_ca   --CA���ձ�
              ( empl_code,   --Ա������
                empl_name,   --Ա������
                key_code,   --KEY
                oper_code,   --������
                oper_date    --��������
               )  
         VALUES 
              ( '{0}',   --Ա������
               fun_get_employee_name('{0}'),--Ա������
                '{1}',   --KEY
                '{2}',   --������
                sysdate    --��������
               )
             */
            if (this.Sql.GetSql("RADT.InsertCACompare.emplCA", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, emplcode, cacode, Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "�����������!";
                return -1;
            }
            return ExecNoQuery(strSql);
        }
        /// <summary>
        /// CA��������fin_com_ca  by ren.jch 2015-01-15
        /// </summary>
        /// <param name="emplcode"></param>
        /// <param name="cacode"></param>
        /// <returns></returns> 
        public int UpdateCancelCACompare(string cacode)
        {
            string strSql = "";
            /*
             update fin_com_ca fii
             set fii.valid_state = '0',
             fii.oper_code   = '{1}',
             fii.oper_date   = sysdate
             where fii.key_code = '{0}'
             and fii.valid_state = '1'

             */
            if (this.Sql.GetSql("RADT.UpdateCancelCACompare.emplCA", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, cacode, Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "�����������!";
                return -1;
            }
            return ExecNoQuery(strSql);
        }
        /// <summary>
        /// ��ö�����Ա��Ϣ�Ƿ��ظ�
        /// </summary>
        /// <param name="emplcode">Ա������</param>
        public ArrayList QueryAllCompare(string emplcode, string cacode)
        {
            string strSql = @"select *
                              from fin_com_ca f
                             where f.valid_state = '1'
                               and f.empl_code = '{0}'
                                or f.key_code = '{1}'
                            ";


            strSql = string.Format(strSql, emplcode, cacode);
            ArrayList al = new ArrayList();
            if (ExecQuery(strSql) == -1)
            {
                return null;
            }
            while (Reader.Read())
            {
                //�޸�Ϊspellʵ�� ֻ��Ϊ�� �ܹ������Ϣ
                Spell obj = new Spell();
                obj.ID = Reader[0].ToString();
                obj.Name = Reader[1].ToString();
                obj.Memo = Reader[2].ToString();
                try
                {
                    obj.User01 = Reader[3].ToString();
                    obj.User02 = Reader[4].ToString();
                    obj.User03 = Reader[5].ToString();
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                al.Add(obj);
            }
            Reader.Close();
            return al;
        }

        /// <summary>
        /// ���»���Ѫ��  ��Ժ�Ǽ�ʱ����  by huangchw 2012-10-24
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="bloodType"></param>
        /// <returns></returns> 
        public int UpdateBloodType(string inpatientNo, string bloodType)
        {
            string strSql = "";
            /*
             update fin_ipr_inmaininfo fii
             set fii.blood_code = '{1}'
             where fii.inpatient_no = '{0}'
             */
            if (this.Sql.GetSql("RADT.UpdatePatient.BloodType", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNo, bloodType);
            }
            catch (Exception ex)
            {
                this.Err = "�����������!";
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���»��߳�Ժ���� ������Ժʱ����
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <param name="dtOutTime">��Ժ����</param>
        /// <returns>�ɹ�����1 �����أ�1 δ�ҵ����ݷ���0</returns>
        public int UpdatePatientOutDate(string inpatientNO, DateTime dtOutTime)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.UpdatePatientOutDate", ref strSql) == -1) return -1;
            #region SQl
            /*
			    update fin_ipr_inmaininfo t
				set    t.out_date = to_date('{1}','yyyy-mm-dd hh24:mi:ss')
				where  t.parent_code = '[��������]'
				and    t.current_code = '[��������]'
				and    t.inpatient_no = '{0}'
			*/
            #endregion
            try
            {
                strSql = string.Format(strSql, inpatientNO, dtOutTime.ToString());
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #region ����סԺ�����Ѫ���ɽ�͹������޶�����޶��ۼ�and�������յ��Ժ�and���޶����and���괦��

        /// <summary>
        /// ���µǼ�ʱ���Ѫ���ɽ�͹������޶�����޶��ۼ�and�������յ��Ժ�and���޶����-----wangrc
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns>���� 0 �ɹ� С��0 ʧ��</returns>
        public int UpdateOtherPatientInfoForRegister(PatientInfo patientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.UpdatePatientInfoForRegister.1", ref strSql) == -1)
            {
                return -1;
            }
            #region SQL
            /*
			  update fin_ipr_inmaininfo 
			        set    BLOOD_LATEFEE = {0} ,          --Ѫ���ɽ�
			               DAY_LIMIT={1} ,                --����ҩƷ���޶�
			               LIMIT_TOT ={2},                --���޶��ۼ�
			               PROCREATE_PCNO='{4}',          --�������յ��Ժ�
			               LIMIT_OVERTOP={5},             --�������޶��ҩƷ����
			               BURSARY_TOTMEDFEE=0,           --����ҩ�Ѻϼ�
			               BED_LIMIT={6},                 --��λ���ƽ��
			               AIR_LIMIT={7},                 --�յ����ƽ��
			               BEDOVERDEAL='{8}',             --���ѳ��괦�� 0���겻��1��������
			               OWN_RATE = {9},                --�Էѱ���
			               PAY_RATE = {10}                --�Ը�����
			        where  PARENT_CODE  = '[��������]'  
			        and    CURRENT_CODE = '[��������]'   
			        and    inpatient_no = '{3}' 
				*/
            #endregion
            try
            {
                //0Ѫ���ɽ�1���޶�2���޶��ۼ�3סԺ��ˮ��4�������յ��Ժ�5���޶����6��λ��������7�յ���������8���괦�� 0���겻��1��������
                strSql = string.Format(strSql,
                                       patientInfo.FT.BloodLateFeeCost.ToString(), //0 Ѫ���ɽ�
                                       patientInfo.FT.DayLimitCost.ToString(), //1 ���޶�
                                       patientInfo.FT.DayLimitTotCost.ToString(), //2 ���޶��ۼ�
                                       patientInfo.ID, //3 סԺ��ˮ��
                                       patientInfo.ProCreateNO, //4 �������յ��Ժ�
                                       patientInfo.FT.OvertopCost.ToString(), //5 ���޶����
                                       patientInfo.FT.BedLimitCost.ToString(), //6 ��λ��������
                                       patientInfo.FT.AirLimitCost.ToString(), //7 �յ���������
                                       patientInfo.FT.BedOverDeal, //8 ���괦�� 0���겻��1��������
                                       patientInfo.FT.FTRate.OwnRate, //9 �Էѱ���
                                       patientInfo.FT.FTRate.PayRate //10�Ը�����
                    );
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }
        #endregion

        #region סԺ�Ǽ��޸�סԺ����

        /// <summary>
        /// ���µǼǻ��ߵĿ�����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns> 0 �ɹ�  ��1 ʧ��</returns>
        public int UpdatePatientDeptByInpatientNo(PatientInfo patientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.UpdatePatientDeptByInpatientNo", ref strSql) == -1)
            {
                return -1;
            }
            #region SQL
            /*
			 update fin_ipr_inmaininfo
             set   dept_code = '{1}',dept_name = '{2}'
			 where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and inpatient_no = '{0}' 
			 */
            #endregion
            try
            {
                strSql =
                    string.Format(strSql, patientInfo.ID, patientInfo.PVisit.PatientLocation.Dept.ID, patientInfo.PVisit.PatientLocation.Dept.Name);
            }
            catch (Exception ex)
            {
                ErrCode = ex.Message;
                Err = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #region סԺ�Ǽ��޸�סԺ����

        /// <summary>
        /// ���µǼǻ��ߵĲ�����Ϣ{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns> 0 �ɹ�  ��1 ʧ��</returns>
        public int UpdatePatientNursCellByInpatientNo(PatientInfo patientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.UpdatePatientNurseCellByInpatientNo", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                strSql =
                    string.Format(strSql, patientInfo.ID, patientInfo.PVisit.PatientLocation.NurseCell.ID, patientInfo.PVisit.PatientLocation.NurseCell.Name);
            }
            catch (Exception ex)
            {
                ErrCode = ex.Message;
                Err = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #region  ������Ϣ���� ��Ժ״̬��ת����ת��

        /// <summary>
        /// ���߳�Ժ�Ǽ�
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣ</param>
        /// <returns></returns>
        public int RegisterOutHospital(PatientInfo patientInfo)
        {
            //����ö��������Ժ״̬:��Ժ�Ǽ�
            InStateEnumService inState = new InStateEnumService();
            inState.ID = EnumInState.B.ToString();

            //���»�����Ժ״̬,����0���ʾ����
            int parm = UpdatePatientStatus(patientInfo, inState);
            if (parm != 1)
            {
                this.Err = "���»���״̬��ת����Ϣ����" + this.Err;
                return -1;
            }

            //�����Ϣ����
            DateTime dt = this.GetDateTimeFromSysDateTime();

            if (dt.Date == patientInfo.PVisit.PreOutTime.Date)
            {
                if (SetShiftData(patientInfo.ID, EnumShiftType.O, "��Ժ�Ǽ�", patientInfo.PVisit.PatientLocation.Dept, patientInfo.PVisit.PatientLocation.NurseCell, patientInfo.IsBaby) < 0)
                {
                    return -1;
                }

                #region add by liuww 2013-04-03
                //�ٻػ��߸��³�Ժ�ٻأ�C��Ϊ��Ժ�ٻ�ȡ����EC��
                if (this.UpdateShiftData(patientInfo.ID, EnumShiftType.C.ToString(), EnumShiftType.EC.ToString(), shiftTypeEnumService.GetName(EnumShiftType.EC)) < 0)
                {
                    return -1;
                }
                #endregion
            }
            else
            {
                //��ע�����¼ʵ�ʲ���ʱ��
                patientInfo.PVisit.PatientLocation.Dept.Memo = "ʵ�ʲ���ʱ�䣺" + dt.ToString();

                //�ò������ڼ�¼��Ժ���ڣ�ֻ��Ϊ�˴�λ�ձ���׼ȷ
                if (SetShiftData(patientInfo.ID, EnumShiftType.O, "��Ժ�Ǽ�", patientInfo.PVisit.PatientLocation.Dept, patientInfo.PVisit.PatientLocation.NurseCell, patientInfo.IsBaby, patientInfo.PVisit.PreOutTime) < 0)
                {
                    return -1;
                }

                #region add by liuww 2013-04-03
                //�ٻػ��߸��³�Ժ�ٻأ�C��Ϊ��Ժ�ٻ�ȡ����EC��
                if (this.UpdateShiftData(patientInfo.ID, EnumShiftType.C.ToString(), EnumShiftType.EC.ToString(), shiftTypeEnumService.GetName(EnumShiftType.EC)) < 0)
                {
                    return -1;
                }
                #endregion
            }

            //������߲���Ӥ��,����´�λ��Ϣ
            if (patientInfo.ID.IndexOf("B") < 0)
            {
                //��Ժ�ǼǺ�λ��Ϣ:�մ�,סԺ��ΪN
                Bed newBed = patientInfo.PVisit.PatientLocation.Bed.Clone();
                newBed.Status.ID = EnumBedStatus.U.ToString();
                newBed.InpatientNO = "N";

                //���´�λ״̬,���жϲ���
                parm = UpdateBedStatus(newBed, patientInfo.PVisit.PatientLocation.Bed);
                if (parm <= 0)
                {
                    return parm;
                }
            }

            return 1;
        }

        #endregion

        #region ���»���״̬

        /// <summary>
        /// ���»���״̬ ��סԺ��ˮ��Ϊ����
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣ</param>
        /// <param name="patientStatus">����״̬</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int UpdatePatientStatus(PatientInfo patientInfo, InStateEnumService patientStatus)
        {
            string strSql = string.Empty;
            int parm = this.UpdateZGInfo(patientInfo, patientStatus.ID.ToString());
            if (parm != 1)
            {
                this.Err = "���»���״̬��ת����Ϣ����" + this.Err;
                return -1;
            }


            //���»���״̬ʱͬ��Ӥ����Ϣ----��������Ļ�����ĸ�ײ��Ҳ��ǳ�Ժ����,�����Ӥ������Ժ״̬
            if (patientInfo.IsHasBaby && patientInfo.PVisit.InState.ID.ToString() != "O")
            {
                //ĸ����Ϣ
                PatientInfo motherInfo = patientInfo.Clone();
                //ĸ����Ժ״̬
                motherInfo.PVisit.InState.ID = patientStatus.ID;
                //ͬ��Ӥ����Ϣ
                if (UpdatePatientByMother(motherInfo) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        #endregion

        #region �޸Ļ�����Ϣ


        #region ɾ��סԺ������Ϣ

        /// <summary>
        /// ɾ��������Ϣ-ɾ��һ�����ߵ���Ϣ����
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣ</param>
        /// <returns>0 �ɹ� -1ʧ��</returns>
        [System.Obsolete("���ڣ�������û��Ӧ�õ�λ��", true)]
        public int DeletePatient(PatientInfo patientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.DeletePatient.1", ref strSql) == -1)
            {
                return -1;
            }

            #region SQL
            /*
			 delete fin_ipr_inmaininfo	 WHERE PARENT_CODE='[��������]' and CURRENT_CODE='[��������]'  and  inpatient_no	= '{0}'		
			 */
            #endregion

            try
            {
                strSql = string.Format(strSql, patientInfo.ID);
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.DeletePatient.1";
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #endregion

        #region ���Ĳ���סԺ��

        /// <summary>
        /// �仯סԺ��-��ʱ��д
        /// </summary>
        /// <param name="PatientInfo">���߻�����Ϣ</param>
        /// <param name="newPatientNo">�µ�סԺ��</param>
        /// <returns></returns>
        [System.Obsolete("���ڣ�������û��Ӧ�õ�λ��", true)]
        public int ChangePID(PatientInfo PatientInfo, string newPatientNo)
        {
            #region �ӿ�˵��

            //�仯סԺ��
            //RADT.InPatient.ChangePID.1
            //���룺0 InpatientNoסԺ��ˮ��,1��������,2oldסԺ��,3��סԺ��
            //������0 

            #endregion

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.ChangePID.1", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, PatientInfo.ID, PatientInfo.Name, PatientInfo.PID.PatientNO, newPatientNo);
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.ChangePID.1";
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #region ת�Ʋ���

        /// <summary>
        /// ת�ƻ�������/ȡ�� -�����ң�������
        /// Ҳ���ø��»�����Ϣ������TempLocation��������Ŀ��� 
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        /// <param name="newLocation">�µ�λ����Ϣ</param>
        /// <param name="isCancel">�Ƿ�ȡ��</param>
        /// <returns>0,û�и�������,1�ɹ� -1ʧ��</returns>
        public int TransferPatientApply(PatientInfo patientInfo, Location newLocation, bool isCancel, string state)
        {
            //ת��
            if (newLocation.Dept.ID != patientInfo.PVisit.PatientLocation.Dept.ID || newLocation.NurseCell.ID != patientInfo.PVisit.PatientLocation.NurseCell.ID)
            {
                int parm;
                //ȡת���������
                int intHappenNo = CheckShiftOutHappenNo(patientInfo.ID, patientInfo.PVisit.PatientLocation, state);
                //�����ת������
                if (isCancel == false)
                {
                    //����Ѵ���ת������,����´�ת������
                    if (intHappenNo > 0)
                    {
                        parm = UpdateShiftDept(patientInfo.ID, intHappenNo, state, "1", newLocation);
                        if (parm < 1)
                        {
                            return parm;
                        }
                    }
                    else
                    {
                        //���û���Ѵ��ڵ�ת������,�����һ���µ�ת������
                        if (InsertShiftDept(patientInfo.ID, patientInfo.PVisit.PatientLocation, newLocation, "1", newLocation.Dept.Memo) != 1)
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    //ȡ��ת��
                    if (intHappenNo <= 0)
                    {
                        Err = "��Ч���������";
                        return -1;
                    }
                    parm = UpdateShiftDept(patientInfo.ID, intHappenNo, state, "3", newLocation);
                    if (parm < 1)
                    {
                        return parm;
                    }
                }
            }

            return 1;
        }

        #endregion

        #region ����ת��������� --˽��

        /// <summary>
        /// ����ת�뵽ĳ���ҵ�ת���������
        /// </summary>
        /// <param name="InpatientNo">����סԺ��ˮ��</param>
        /// <param name="newLocation">ת�������Ϣ</param>
        /// <param name="Type">ת��״̬</param>
        /// <returns></returns>
        private int CheckShiftInHappenNo(string InpatientNo, Location newLocation, string Type)
        {
            string strSQL = string.Empty;
            int intHappenNo = 0;
            //ȡSQL���
            if (Sql.GetCommonSql("RADT.InPatient.CheckShiftInHappenNo.1", ref strSQL) == -1)
            {
                return -1;
            }

            //��ʽ������
            try
            {
                strSQL = string.Format(strSQL,
                                       InpatientNo, //����סԺ��ˮ��
                                       newLocation.Dept.ID, //ת����ұ���
                                       newLocation.NurseCell.ID, //ת�뻤��վ����
                                       Type //ת��״̬
                    );
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.CheckShiftInHappenNo.1";
                WriteErr();
                return -1;
            }

            if (ExecQuery(strSQL) < 0)
            {
                return -1;
            }
            //			if(this.Reader.HasRows) {
            if (Reader.Read())
            {
                intHappenNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                Reader.Close();
            }
            //�������
            return intHappenNo;
        }


        /// <summary>
        /// ����ת����ĳ���ҵ�ת���������
        /// </summary>
        /// <param name="inpatientNo">����סԺ��ˮ��</param>
        /// <param name="location">ת��������Ϣ</param>
        /// <param name="type">ת��״̬</param>
        /// <returns></returns>
        private int CheckShiftOutHappenNo(string inpatientNo, Location location, string type)
        {
            string strSQL = string.Empty;
            int intHappenNo = 0;

            //ȡSQL���
            if (Sql.GetCommonSql("RADT.InPatient.CheckShiftOutHappenNo.1", ref strSQL) == -1)
            {
                return -1;
            }

            #region SQL
            /*
			    select 	NVL(happen_no,0) 
				from  	fin_ipr_shiftapply  
				WHERE 	PARENT_CODE  = '[��������]' 
				AND    	CURRENT_CODE = '[��������]' 
				AND    	inpatient_no = '{0}'  
				AND    	old_dept_code= '{1}'     
				AND    	shift_state  in('1','0') --��ǰ״̬,0δ��Ч,1ת������,2ȷ��,3ȡ������
			 */
            #endregion

            //��ʽ������
            try
            {
                strSQL = string.Format(strSQL, inpatientNo, location.Dept.ID);
            }
            catch (Exception ee)
            {
                Err = ee.Message;
                WriteErr();
                return -1;
            }

            if (ExecQuery(strSQL) < 0)
            {
                return -1;
            }
            try
            {
                if (Reader.Read())
                {
                    intHappenNo = NConvert.ToInt32(Reader[0].ToString());

                }
            }
            catch (Exception ee)
            {
                Reader.Close();
                Err = ee.Message;
                WriteErr();
                return -1;
            }

            Reader.Close();
            //�������
            return intHappenNo;
        }

        #endregion

        #region ת����ת���ң�ת����

        /// <summary>
        /// ת�ƻ��� -�����������ң�������
        /// ��Ҫ�������(����סԺ�������²�����,���±����)
        /// </summary>
        /// <param name="PatientInfo">������Ϣ</param>
        /// <param name="newLocation">�µ�λ����Ϣ</param>
        /// <returns>0û�и���(��������),1�ɹ� -1ʧ��</returns>
        public int TransferPatient(PatientInfo PatientInfo, Location newLocation)
        {
            //ת����
            if (newLocation.NurseCell.ID == string.Empty)
            {
                newLocation.NurseCell.ID = PatientInfo.PVisit.PatientLocation.NurseCell.ID;
                newLocation.NurseCell.Name = PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            }
            //ת��
            if (newLocation.Dept.ID == string.Empty)
            {
                newLocation.Dept.ID = PatientInfo.PVisit.PatientLocation.Dept.ID;
                newLocation.Dept.Name = PatientInfo.PVisit.PatientLocation.Dept.Name;
            }
            //ת��
            if (newLocation.Bed.ID == string.Empty)
            {
                newLocation.Bed.ID = PatientInfo.PVisit.PatientLocation.Bed.ID;
            }
            //���»��߻�����Ϣ��
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.TransferPatient.1", ref strSql) == -1)
            #region SQL
            /*UPDATE 	FIN_IPR_INMAININFO 
					SET    	NURSE_CELL_CODE='{7}' ,    -- �����Ļ���վ����
					NURSE_CELL_NAME='{8}',     -- �����Ļ���վ����
					DEPT_CODE='{9}',           -- �����Ŀ��ұ���
					DEPT_NAME='{10}',          -- �����Ŀ�������
					BED_NO='{11}'              -- �����Ĵ���
					WHERE  	PARENT_CODE  = '[��������]' 
					AND   	CURRENT_CODE = '[��������]' 
					AND    	INPATIENT_NO = '{0}'        --����סԺ��ˮ��
					AND    	IN_STATE IN ('I','B')        --��Ժ״̬
					*/
            #endregion
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                                       PatientInfo.ID, //0 ����סԺ��ˮ��
                                       PatientInfo.Name, //1 ���ǰ�Ļ�������
                                       PatientInfo.PVisit.PatientLocation.NurseCell.ID, //2 ���ǰ�Ļ���վ����
                                       PatientInfo.PVisit.PatientLocation.NurseCell.Name, //3 ���ǰ�Ļ���վ����
                                       PatientInfo.PVisit.PatientLocation.Dept.ID, //4 ���ǰ�Ŀ��ұ���
                                       PatientInfo.PVisit.PatientLocation.Dept.Name, //5 ���ǰ�Ŀ�������
                                       PatientInfo.PVisit.PatientLocation.Bed.ID, //6 ���ǰ�Ĵ���
                                       newLocation.NurseCell.ID, //7 �����Ļ���վ����
                                       newLocation.NurseCell.Name, //8 �����Ļ���վ����
                                       newLocation.Dept.ID, //9 �����Ŀ��ұ���
                                       newLocation.Dept.Name, //10�����Ŀ�������
                                       newLocation.Bed.ID); //11�����Ĵ���
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }

            int parm = ExecNoQuery(strSql);
            if (parm <= 0)
            {
                return parm;
            }


            //ת�ƻ���(�����������ң�������)ʱͬ��Ӥ����Ϣ User01 == 1 Ӥ��һ��ת��User01 == 0 ��һ��
            if (PatientInfo.IsHasBaby && PatientInfo.User01 == "1")
            {
                //ĸ����Ϣ
                PatientInfo motherInfo = PatientInfo.Clone();
                //ĸ�ױ䶯��Ϣ
                motherInfo.PVisit.PatientLocation = newLocation.Clone();
                //ͬ��Ӥ����Ϣ
                if (UpdatePatientByMother(motherInfo) == -1)
                {
                    return -1;
                }
            }

            //������ߵĿ��һ��߲��������仯,����¿��һ��߲�����Ϣ
            if (newLocation.Dept.ID != PatientInfo.PVisit.PatientLocation.Dept.ID ||
                newLocation.NurseCell.ID != PatientInfo.PVisit.PatientLocation.NurseCell.ID)
            {
                //ȡת���������
                int intHappenNo;
                intHappenNo = CheckShiftInHappenNo(PatientInfo.ID, newLocation, "1");
                if (intHappenNo <= 0) return intHappenNo;

                //����ת������
                if (UpdateShiftDept(PatientInfo.ID, intHappenNo, "1", "2", newLocation) <= 0)
                {
                    return -1;
                }

                //����ת��ǰ��Ĳ����ʹ�λ���� added by cuipeng 2005-4-7
                PatientInfo.PVisit.PatientLocation.Dept.Memo = PatientInfo.PVisit.PatientLocation.NurseCell.ID; //����ת��ǰ��������
                PatientInfo.PVisit.PatientLocation.Dept.User01 = PatientInfo.PVisit.PatientLocation.Bed.ID; //����ת��ǰ��λ����
                newLocation.Dept.Memo = newLocation.NurseCell.ID; //����ת�ƺ�������

                PatientInfo.PVisit.PatientLocation.NurseCell.Memo = PatientInfo.PVisit.PatientLocation.Dept.ID; //����ת��ǰ��������
                PatientInfo.PVisit.PatientLocation.NurseCell.User01 = PatientInfo.PVisit.PatientLocation.Bed.ID; //����ת��ǰ��λ����
                //{B1E611C2-7A04-4b79-B64B-3D280D5769CE} �޸Ĳ����ձ�

                newLocation.NurseCell.Memo = newLocation.Dept.ID;
                PatientInfo.PVisit.PatientLocation.NurseCell.User02 = "N";
                PatientInfo.PVisit.PatientLocation.NurseCell.User03 = PatientInfo.PVisit.PatientLocation.Dept.ID;

                //�����Ϣ����: ����ת�������¼��
                if (SetShiftData(PatientInfo.ID, EnumShiftType.RO, "ת��", PatientInfo.PVisit.PatientLocation.Dept, newLocation.Dept, PatientInfo.IsBaby) < 0)
                {
                    return -1;
                }


                if (SetShiftData(PatientInfo.ID, EnumShiftType.CNO, "ת��", PatientInfo.PVisit.PatientLocation.NurseCell, newLocation.NurseCell, PatientInfo.IsBaby) < 0)
                {
                    return -1;
                }


                //�����Ϣ����: ����ת������¼��
                if (SetShiftData(PatientInfo.ID, EnumShiftType.RI, "ת��", PatientInfo.PVisit.PatientLocation.Dept, newLocation.Dept, PatientInfo.IsBaby) < 0)
                {
                    return -1;
                }

                //�����Ϣ����: ����ת������¼��
                if (SetShiftData(PatientInfo.ID, EnumShiftType.CN, "ת��", PatientInfo.PVisit.PatientLocation.NurseCell, newLocation.NurseCell, PatientInfo.IsBaby) < 0)
                {
                    return -1;
                }
            }

            //������ߴ�λ�����仯,����²�������Ϣ(�˴��Ĵ�����:ת��,ת��,ת����)
            if (newLocation.Bed.ID != PatientInfo.PVisit.PatientLocation.Bed.ID)
            {
                //���»������ڴ�λ����Ϣ
                //�����´�λ���ǰ����Ϣ,�����жϲ���
                Bed tempBed = newLocation.Bed.Clone();
                //oldBed.InpatientNo  = "N";
                //oldBed.BedStatus.ID = "U";

                //��������Ϣ
                if (SetShiftData(PatientInfo.ID, EnumShiftType.RB,
                                 "ת��", PatientInfo.PVisit.PatientLocation.Bed, tempBed, PatientInfo.IsBaby) < 0)
                {
                    return -1;
                }

                //�޸��µĴ�λ��Ϣ(����ID�ͻ���ԭ��λ��״̬)
                tempBed.InpatientNO = PatientInfo.ID;
                //������ǰ����ԭ��λΪ��,���޸�Ϊռ��
                if (PatientInfo.PVisit.PatientLocation.Bed.ID == string.Empty)
                    tempBed.Status.ID = "O";
                else
                    tempBed.Status.ID = PatientInfo.PVisit.PatientLocation.Bed.Status.ID;

                //{DF72A3CF-38E6-4616-8287-DC989A4155F9} Ӥ��ת��,����������Ӥ������ת�ƣ��Ҳ�ռ�ô�λ
                if (!PatientInfo.IsBaby)
                {
                    //�����´�λ:ԭ��λ�ϵĻ��߻����´�λ��
                    parm = UpdateBedStatus(tempBed, newLocation.Bed);
                    if (parm <= 0) return parm;

                    //����´�λ�ڱ��ǰ�ǿմ����һ����ڱ��ǰ�д�λ(˵�����β����ǻ���,���ǽ������),����ջ���ԭ��λ�Ĵ�λ��Ϣ
                    if (newLocation.Bed.InpatientNO == "N" && PatientInfo.PVisit.PatientLocation.Bed.ID != string.Empty)
                    {
                        //�رյĴ�λ���մ�λ��.ת�ƺ��ͷŴ�λʱ��λ״̬��ΪC . {CA479D1B-BD94-459e-AA19-1AE2C4902DAF}
                        if (PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() != "C")
                        {
                            //�޸Ļ��߱��ǰ�Ĵ�λ��Ϣ
                            tempBed = PatientInfo.PVisit.PatientLocation.Bed.Clone();
                            tempBed.InpatientNO = "N";
                            tempBed.Status.ID = "U";

                            //���»��߱��ǰ��λ:���
                            parm = UpdateBedStatus(tempBed, PatientInfo.PVisit.PatientLocation.Bed);
                            if (parm <= 0) return parm;
                        }
                        else
                        {
                            //�޸Ļ��߱��ǰ�Ĵ�λ��Ϣ
                            //tempBed = PatientInfo.PVisit.PatientLocation.Bed.Clone();
                            //tempBed.InpatientNO = "N";
                            tempBed.Status.ID = "O";

                            //���»��߱��ǰ��λ:���
                            parm = UpdateBedStatus(tempBed, PatientInfo.PVisit.PatientLocation.Bed);
                            if (parm <= 0) return parm;
                        }
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// ��סԺ��ʹ�ã�ʡ�������룩ת�ƻ��� -�����������ң�������
        /// ��Ҫ�������(����סԺ�������²�����,���±����)
        /// </summary>
        /// <param name="PatientInfo">������Ϣ</param>
        /// <param name="newLocation">�µ�λ����Ϣ</param>
        /// <returns>0û�и���(��������),1�ɹ� -1ʧ��</returns>
        public int TransferPatientLocation(PatientInfo PatientInfo, Location newLocation)
        {
            //ת����
            if (newLocation.NurseCell.ID == string.Empty)
            {
                newLocation.NurseCell.ID = PatientInfo.PVisit.PatientLocation.NurseCell.ID;
                newLocation.NurseCell.Name = PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            }
            //ת��
            if (newLocation.Dept.ID == string.Empty)
            {
                newLocation.Dept.ID = PatientInfo.PVisit.PatientLocation.Dept.ID;
                newLocation.Dept.Name = PatientInfo.PVisit.PatientLocation.Dept.Name;
            }
            //ת��
            if (newLocation.Bed.ID == string.Empty)
            {
                newLocation.Bed.ID = PatientInfo.PVisit.PatientLocation.Bed.ID;
            }
            //���»��߻�����Ϣ��
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.TransferPatient.1", ref strSql) == -1)
            #region SQL
            /*UPDATE 	FIN_IPR_INMAININFO 
					SET    	NURSE_CELL_CODE='{7}' ,    -- �����Ļ���վ����
					NURSE_CELL_NAME='{8}',     -- �����Ļ���վ����
					DEPT_CODE='{9}',           -- �����Ŀ��ұ���
					DEPT_NAME='{10}',          -- �����Ŀ�������
					BED_NO='{11}'              -- �����Ĵ���
					WHERE  	PARENT_CODE  = '[��������]' 
					AND   	CURRENT_CODE = '[��������]' 
					AND    	INPATIENT_NO = '{0}'        --����סԺ��ˮ��
					AND    	IN_STATE IN ('I','B')        --��Ժ״̬
					*/
            #endregion
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                                       PatientInfo.ID, //0 ����סԺ��ˮ��
                                       PatientInfo.Name, //1 ���ǰ�Ļ�������
                                       PatientInfo.PVisit.PatientLocation.NurseCell.ID, //2 ���ǰ�Ļ���վ����
                                       PatientInfo.PVisit.PatientLocation.NurseCell.Name, //3 ���ǰ�Ļ���վ����
                                       PatientInfo.PVisit.PatientLocation.Dept.ID, //4 ���ǰ�Ŀ��ұ���
                                       PatientInfo.PVisit.PatientLocation.Dept.Name, //5 ���ǰ�Ŀ�������
                                       PatientInfo.PVisit.PatientLocation.Bed.ID, //6 ���ǰ�Ĵ���
                                       newLocation.NurseCell.ID, //7 �����Ļ���վ����
                                       newLocation.NurseCell.Name, //8 �����Ļ���վ����
                                       newLocation.Dept.ID, //9 �����Ŀ��ұ���
                                       newLocation.Dept.Name, //10�����Ŀ�������
                                       newLocation.Bed.ID); //11�����Ĵ���
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }

            int parm = ExecNoQuery(strSql);
            if (parm <= 0)
            {
                return parm;
            }


            //ת�ƻ���(�����������ң�������)ʱͬ��Ӥ����Ϣ
            if (PatientInfo.IsHasBaby)
            {
                //ĸ����Ϣ
                PatientInfo motherInfo = PatientInfo.Clone();
                //ĸ�ױ䶯��Ϣ
                motherInfo.PVisit.PatientLocation = newLocation.Clone();
                //ͬ��Ӥ����Ϣ
                if (UpdatePatientByMother(motherInfo) == -1)
                {
                    return -1;
                }
            }

            //������ߵĿ��һ��߲��������仯,����¿��һ��߲�����Ϣ
            if (newLocation.Dept.ID != PatientInfo.PVisit.PatientLocation.Dept.ID ||
                newLocation.NurseCell.ID != PatientInfo.PVisit.PatientLocation.NurseCell.ID)
            {
                //ȡת���������
                //int intHappenNo;
                //intHappenNo = CheckShiftInHappenNo(PatientInfo.ID, newLocation, "1");
                //if (intHappenNo <= 0) return intHappenNo;

                ////����ת������
                //if (UpdateShiftDept(PatientInfo.ID, intHappenNo, "1", "2", newLocation) <= 0)
                //{
                //    return -1;
                //}

                //����ת��ǰ��Ĳ����ʹ�λ���� added by cuipeng 2005-4-7
                PatientInfo.PVisit.PatientLocation.Dept.Memo = PatientInfo.PVisit.PatientLocation.NurseCell.ID; //����ת��ǰ��������
                PatientInfo.PVisit.PatientLocation.Dept.User01 = PatientInfo.PVisit.PatientLocation.Bed.ID; //����ת��ǰ��λ����
                newLocation.Dept.Memo = newLocation.NurseCell.ID; //����ת�ƺ�������
                //�����Ϣ����: ����ת�������¼��
                if (SetShiftData(PatientInfo.ID, EnumShiftType.RO, "ת��", PatientInfo.PVisit.PatientLocation.Dept, newLocation.Dept, PatientInfo.IsBaby) < 0)
                {
                    return -1;
                }


                //�����Ϣ����: ����ת������¼��
                if (SetShiftData(PatientInfo.ID, EnumShiftType.RI, "ת��", PatientInfo.PVisit.PatientLocation.Dept, newLocation.Dept, PatientInfo.IsBaby) < 0)
                {
                    return -1;
                }
            }

            //������ߴ�λ�����仯,����²�������Ϣ(�˴��Ĵ�����:ת��,ת��,ת����)
            if (newLocation.Bed.ID != PatientInfo.PVisit.PatientLocation.Bed.ID)
            {
                //���»������ڴ�λ����Ϣ
                //�����´�λ���ǰ����Ϣ,�����жϲ���
                Bed tempBed = newLocation.Bed.Clone();
                //oldBed.InpatientNo  = "N";
                //oldBed.BedStatus.ID = "U";

                //��������Ϣ
                if (SetShiftData(PatientInfo.ID, EnumShiftType.RB,
                                 "ת��", PatientInfo.PVisit.PatientLocation.Bed, tempBed, PatientInfo.IsBaby) < 0)
                {
                    return -1;
                }

                //�޸��µĴ�λ��Ϣ(����ID�ͻ���ԭ��λ��״̬)
                tempBed.InpatientNO = PatientInfo.ID;
                //������ǰ����ԭ��λΪ��,���޸�Ϊռ��
                if (PatientInfo.PVisit.PatientLocation.Bed.ID == string.Empty)
                    tempBed.Status.ID = "O";
                else
                    tempBed.Status.ID = PatientInfo.PVisit.PatientLocation.Bed.Status.ID;

                //�����´�λ:ԭ��λ�ϵĻ��߻����´�λ��
                parm = UpdateBedStatus(tempBed, newLocation.Bed);
                if (parm <= 0) return parm;

                //����´�λ�ڱ��ǰ�ǿմ����һ����ڱ��ǰ�д�λ(˵�����β����ǻ���,���ǽ������),����ջ���ԭ��λ�Ĵ�λ��Ϣ
                if (newLocation.Bed.InpatientNO == "N" && PatientInfo.PVisit.PatientLocation.Bed.ID != string.Empty)
                {
                    //�޸Ļ��߱��ǰ�Ĵ�λ��Ϣ
                    tempBed = PatientInfo.PVisit.PatientLocation.Bed.Clone();
                    tempBed.InpatientNO = "N";
                    tempBed.Status.ID = "U";

                    //���»��߱��ǰ��λ:���
                    parm = UpdateBedStatus(tempBed, PatientInfo.PVisit.PatientLocation.Bed);
                    if (parm <= 0) return parm;
                }
            }

            return 1;
        }

        #endregion

        #region �������д�λ����
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int RecievePatient(PatientInfo PatientInfo, FS.HISFC.Models.RADT.InStateEnumService Status)
        {
            string strSql = string.Empty;
            int parm;
            if (Sql.GetCommonSql("RADT.InPatient.ArrivePatient.1", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                string[] s = {
				             	PatientInfo.ID, //0 סԺ��ˮ��
				             	PatientInfo.PVisit.PatientLocation.Dept.ID, //1 ���ұ���
				             	PatientInfo.PVisit.PatientLocation.Dept.Name, //2 ��������
				             	PatientInfo.PVisit.PatientLocation.Bed.ID, //3 ����
				             	PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString(), //4 ��λ״̬(�˴�����û��,��֪��˭Ϲд��)
				             	PatientInfo.PVisit.AttendingDoctor.ID, //5
				             	PatientInfo.PVisit.AttendingDoctor.Name, //6
				             	PatientInfo.PVisit.ReferringDoctor.ID, //7
				             	PatientInfo.PVisit.ReferringDoctor.Name, //8
				             	PatientInfo.PVisit.ConsultingDoctor.ID, //9 ����ҽʦ����
				             	PatientInfo.PVisit.ConsultingDoctor.Name, //10����ҽʦ����
				             	PatientInfo.PVisit.AdmittingDoctor.ID, //11
				             	PatientInfo.PVisit.AdmittingDoctor.Name, //12
				             	PatientInfo.PVisit.AdmitSource.ID, //13
				             	PatientInfo.PVisit.AdmitSource.Name, //14
				             	PatientInfo.PVisit.AdmittingNurse.ID, //15���λ�ʿ����
				             	PatientInfo.PVisit.AdmittingNurse.Name, //16���λ�ʿ����
				             	PatientInfo.PVisit.InSource.ID, //17��Ժ��ԴID
				             	PatientInfo.PVisit.InSource.Name, //18��Ժ��Դ����
				             	PatientInfo.PVisit.Circs.ID, //19��Ժ���ID
				             	PatientInfo.PVisit.Circs.Name, //20��Ժ�������
				             	PatientInfo.PVisit.PatientLocation.NurseCell.ID, //21����վ����
				             	PatientInfo.PVisit.PatientLocation.NurseCell.Name, //22����վ����
				             	Status.ID.ToString() //23������Ժ״̬
                                
                                //{FA32C976-E003-4a10-9028-71F2CD154052} ����ʱ��
                                ,PatientInfo.PVisit.RegistTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                PatientInfo.Disease.Tend.Name,
                                PatientInfo.Disease.Memo,
                                PatientInfo.BloodType.ID.ToString(),   //27Ѫ�� 

                                PatientInfo.PVisit.ResponsibleDoctor.ID, //28 ����ҽʦ����
                                PatientInfo.PVisit.ResponsibleDoctor.Name //29 ����ҽʦ����

				             };
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "����ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            parm = ExecNoQuery(strSql);
            if (parm <= 0)
            {
                return parm;
            }

            //���»��߽����¼
            if (Sql.GetCommonSql("RADT.InPatient.ArrivePatient.2", ref strSql) == -1) return -1;
            try
            {
                string[] n = {
				             	PatientInfo.ID,//0
				             	PatientInfo.PVisit.PatientLocation.Dept.ID,
				             	PatientInfo.PVisit.PatientLocation.Dept.Name,
				             	PatientInfo.PVisit.PatientLocation.Bed.ID,
				             	PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString(),
				             	PatientInfo.PVisit.AttendingDoctor.ID,
				             	PatientInfo.PVisit.AttendingDoctor.Name,
				             	PatientInfo.PVisit.ReferringDoctor.ID,
				             	PatientInfo.PVisit.ReferringDoctor.Name,
				             	PatientInfo.PVisit.ConsultingDoctor.ID,
				             	PatientInfo.PVisit.ConsultingDoctor.Name,
				             	PatientInfo.PVisit.AdmittingDoctor.ID,
				             	PatientInfo.PVisit.AdmittingDoctor.Name,
				             	PatientInfo.PVisit.AdmitSource.ID,
				             	PatientInfo.PVisit.AdmitSource.Name,
				             	PatientInfo.PVisit.AdmittingNurse.ID,
				             	PatientInfo.PVisit.AdmittingNurse.Name,
				             	PatientInfo.PVisit.InSource.ID,
				             	PatientInfo.PVisit.InSource.Name,
				             	PatientInfo.PVisit.Circs.ID,
				             	PatientInfo.PVisit.Circs.Name,
				             	PatientInfo.PVisit.PatientLocation.NurseCell.ID,
				             	PatientInfo.PVisit.PatientLocation.NurseCell.Name,
				             	Operator.ID ,//�����˱���
                                PatientInfo.PVisit.AttendingDirector.ID,//�����α���
                                PatientInfo.PVisit.AttendingDirector.Name//����������
				             };

                strSql = string.Format(strSql, n);
            }
            catch (Exception ex)
            {
                Err = "����ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            parm = ExecNoQuery(strSql);
            if (parm <= 0)
            {
                return parm;
            }

            //�ٻ�,ת��ʱͬ��Ӥ����Ϣ--Ӥ���Ĵ��źͿ��Ҹ�ĸ����ͬ
            if (PatientInfo.IsHasBaby && PatientInfo.PVisit.InState.ID.ToString() != "O")
            {
                //ĸ����Ϣ
                PatientInfo motherInfo = PatientInfo.Clone();
                //������ĸ����Ժ״̬Ϊ��Ժ�Ǽ�״̬"I"
                motherInfo.PVisit.InState.ID = "I";
                //ͬ��Ӥ����Ϣ
                if (UpdatePatientByMother(motherInfo) == -1)
                {
                    return -1;
                }
            }

            //if (PatientInfo.IsBaby)//�Ƿ�Ӥ��
            //{
            //    //����Ӥ����Ϣ�������ҽʦ�����Լ�����ҽʦ����
            //    if (UpdateBabyResponsibleDoc(PatientInfo) == -1)
            //    {
            //        return -1;
            //    }
            //}

            return 1;
        }

        /// <summary>
        /// ���߽���-���»��߽�����Ϣ �ý�����
        /// </summary>
        /// <param name="PatientInfo">���߻�����Ϣ(������Ժ�Ǽ�ʱ��ȷ����λ)</param>
        /// <param name="Status">����״̬</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int RecievePatient(PatientInfo PatientInfo, FS.HISFC.Models.Base.EnumInState Status)
        {
            #region �ӿ�˵��

            // ���߽���
            //RADT.InPatient.ArrivePatient.1
            //���룺0 InpatientNoסԺ��ˮ��,1����ID,2��������,3����,4����״̬ID,
            //5����ҽ��ID,6����ҽ������,7������ҽʦID,8������ҽʦ����,9����ҽʦID,10����ҽʦ����
            //11����ҽʦID,12����ҽʦ����,13��Ժ;��ID,14��Ժ;������,15���λ�ʿID,16���λ�ʿ����,
            //17��Ժ��ԴID,18��Ժ��Դ����,19��Ժ���ID,20��Ժ�������  27Ѫ�� 
            //������0 

            #endregion

            string strSql = string.Empty;
            int parm;
            if (Sql.GetCommonSql("RADT.InPatient.ArrivePatient.1", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                string[] s = {
				             	PatientInfo.ID, //0 סԺ��ˮ��
				             	PatientInfo.PVisit.PatientLocation.Dept.ID, //1 ���ұ���
				             	PatientInfo.PVisit.PatientLocation.Dept.Name, //2 ��������
				             	PatientInfo.PVisit.PatientLocation.Bed.ID, //3 ����
				             	PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString(), //4 ��λ״̬(�˴�����û��,��֪��˭Ϲд��)
				             	PatientInfo.PVisit.AttendingDoctor.ID, //5
				             	PatientInfo.PVisit.AttendingDoctor.Name, //6
				             	PatientInfo.PVisit.ReferringDoctor.ID, //7
				             	PatientInfo.PVisit.ReferringDoctor.Name, //8
				             	PatientInfo.PVisit.ConsultingDoctor.ID, //9 ����ҽʦ����
				             	PatientInfo.PVisit.ConsultingDoctor.Name, //10����ҽʦ����
				             	PatientInfo.PVisit.AdmittingDoctor.ID, //11
				             	PatientInfo.PVisit.AdmittingDoctor.Name, //12
				             	PatientInfo.PVisit.AdmitSource.ID, //13
				             	PatientInfo.PVisit.AdmitSource.Name, //14
				             	PatientInfo.PVisit.AdmittingNurse.ID, //15���λ�ʿ����
				             	PatientInfo.PVisit.AdmittingNurse.Name, //16���λ�ʿ����
				             	PatientInfo.PVisit.InSource.ID, //17��Ժ��ԴID
				             	PatientInfo.PVisit.InSource.Name, //18��Ժ��Դ����
				             	PatientInfo.PVisit.Circs.ID, //19��Ժ���ID
				             	PatientInfo.PVisit.Circs.Name, //20��Ժ�������
				             	PatientInfo.PVisit.PatientLocation.NurseCell.ID, //21����վ����
				             	PatientInfo.PVisit.PatientLocation.NurseCell.Name, //22����վ����
				             	Status.ToString() //23������Ժ״̬

                               //{FA32C976-E003-4a10-9028-71F2CD154052} ����ʱ��
                               ,PatientInfo.PVisit.RegistTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                PatientInfo.Disease.Tend.Name,
                                PatientInfo.Disease.Memo,
                                PatientInfo.BloodType.ID.ToString(),   //27Ѫ�� 

                                PatientInfo.PVisit.ResponsibleDoctor.ID, //28 ����ҽʦ����
                                PatientInfo.PVisit.ResponsibleDoctor.Name //29 ����ҽʦ����
				             };
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "����ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            parm = ExecNoQuery(strSql);
            if (parm <= 0)
            {
                return parm;
            }

            //���»��߽����¼
            if (Sql.GetCommonSql("RADT.InPatient.ArrivePatient.2", ref strSql) == -1) return -1;
            try
            {
                string[] n = {
				             	PatientInfo.ID,
				             	PatientInfo.PVisit.PatientLocation.Dept.ID,
				             	PatientInfo.PVisit.PatientLocation.Dept.Name,
				             	PatientInfo.PVisit.PatientLocation.Bed.ID,
				             	PatientInfo.PVisit.PatientLocation.Bed.Status.ID.ToString(),
				             	PatientInfo.PVisit.AttendingDoctor.ID,
				             	PatientInfo.PVisit.AttendingDoctor.Name,
				             	PatientInfo.PVisit.ReferringDoctor.ID,
				             	PatientInfo.PVisit.ReferringDoctor.Name,
				             	PatientInfo.PVisit.ConsultingDoctor.ID,
				             	PatientInfo.PVisit.ConsultingDoctor.Name,
				             	PatientInfo.PVisit.AdmittingDoctor.ID,
				             	PatientInfo.PVisit.AdmittingDoctor.Name,
				             	PatientInfo.PVisit.AdmitSource.ID,
				             	PatientInfo.PVisit.AdmitSource.Name,
				             	PatientInfo.PVisit.AdmittingNurse.ID,
				             	PatientInfo.PVisit.AdmittingNurse.Name,
				             	PatientInfo.PVisit.InSource.ID,
				             	PatientInfo.PVisit.InSource.Name,
				             	PatientInfo.PVisit.Circs.ID,
				             	PatientInfo.PVisit.Circs.Name,
				             	PatientInfo.PVisit.PatientLocation.NurseCell.ID,
				             	PatientInfo.PVisit.PatientLocation.NurseCell.Name,
				             	Operator.ID, //�����˱���
                                PatientInfo.PVisit.AttendingDirector.ID,//�����α���
                                PatientInfo.PVisit.AttendingDirector.Name//����������
				             };

                strSql = string.Format(strSql, n);
            }
            catch (Exception ex)
            {
                Err = "����ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            parm = ExecNoQuery(strSql);
            if (parm <= 0)
            {
                return parm;
            }

            //�ٻ�,ת��ʱͬ��Ӥ����Ϣ--Ӥ���Ĵ��źͿ��Ҹ�ĸ����ͬ,User01 = 1��ĸ��һ��ת = 0 ��ת
            if (PatientInfo.IsHasBaby && PatientInfo.PVisit.InState.ID.ToString() != "O" && PatientInfo.User01 == "1")
            {
                //ĸ����Ϣ
                PatientInfo motherInfo = PatientInfo.Clone();
                //������ĸ����Ժ״̬Ϊ��Ժ�Ǽ�״̬"I"
                motherInfo.PVisit.InState.ID = "I";
                //ͬ��Ӥ����Ϣ
                if (UpdatePatientByMother(motherInfo) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ���߽���-���»��߽�����Ϣ �ý�����
        /// </summary>
        /// <param name="PatientInfo">���߻�����Ϣ(������Ժ�Ǽ�ʱ��ȷ����λ)</param>
        /// <param name="Status">����״̬</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int CancelRecievePatient(PatientInfo PatientInfo, FS.HISFC.Models.Base.EnumInState Status)
        {
            #region �ӿ�˵��

            // ���߽���
            //RADT.InPatient.ArrivePatient.1
            //���룺0 InpatientNoסԺ��ˮ��,1����ID,2��������,3����,4����״̬ID,
            //5����ҽ��ID,6����ҽ������,7������ҽʦID,8������ҽʦ����,9����ҽʦID,10����ҽʦ����
            //11����ҽʦID,12����ҽʦ����,13��Ժ;��ID,14��Ժ;������,15���λ�ʿID,16���λ�ʿ����,
            //17��Ժ��ԴID,18��Ժ��Դ����,19��Ժ���ID,20��Ժ�������
            //������0 

            #endregion

            string strSql = string.Empty;
            int parm;
            if (Sql.GetCommonSql("RADT.InPatient.ArrivePatient.3", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                string[] s = {
 				             	PatientInfo.ID, //0 סԺ��ˮ��
 				             	Status.ToString() //1������Ժ״̬
 				             };
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "����ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            parm = ExecNoQuery(strSql);
            if (parm <= 0)
            {
                return parm;
            }

            return 1;
        }


        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="bed"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CancelRecievePatient(PatientInfo patientInfo, Bed bed, EnumShiftType type, string notes)
        {
            FS.HISFC.Models.Base.EnumInState status = EnumInState.R;
            int parm;
            //Ӥ�������´�λ��Ϣ
            if (patientInfo.IsBaby == false)
            {
                //����ǰ�Ĵ�λ��Ϣ
                Bed oldBed = new Bed();
                oldBed.ID = patientInfo.PVisit.PatientLocation.Bed.ID;
                oldBed.InpatientNO = patientInfo.ID;
                oldBed.Status.ID = "O";

                //���º�Ĵ�λ
                bed.InpatientNO = "N"; //'N'����û�л���
                bed.Status.ID = "U"; //'U'����մ�
                bed.ID = patientInfo.PVisit.PatientLocation.Bed.ID;

                //���´�λ��Ϣ
                parm = UpdateBedStatus(bed, oldBed);
                if (parm != 1)
                {
                    return parm;
                }
            }

            patientInfo.PVisit.PatientLocation.Bed = bed;

            //if (type.ToString() == "K")
            //    //���ڱ�����͵���"����K"����Ժ״̬��"��Ժ�Ǽ�R"
            //    status = EnumInState.R;
            //else if (type.ToString() == "C")
            //    //���ڱ�����͵���"�ٻ�C"����Ժ״̬��"��Ժ�Ǽ�B"
            //    status = EnumInState.B;

            //ֻ���ѽ��� ��Ҫȡ������
            if (patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.I.ToString())
            {
                parm = CancelRecievePatient(patientInfo, status);
                if (parm <= 0)
                {
                    return parm;
                }

                #region {6738F887-6A7B-48eb-8653-FC2AA1893DA8}
                if (type.ToString() == "K")
                {
                    //�����Ϣ
                    //{FA32C976-E003-4a10-9028-71F2CD154052} ����ʱ��
                    if (SetShiftData(patientInfo.ID, patientInfo.PVisit.RegistTime, type, notes, patientInfo.PVisit.PatientLocation.NurseCell, patientInfo.PVisit.PatientLocation.Bed, patientInfo.IsBaby) < 0)
                    {
                        return -1;
                    }

                }
                else
                {
                    if (SetShiftData(patientInfo.ID, type, notes, patientInfo.PVisit.PatientLocation.NurseCell, patientInfo.PVisit.PatientLocation.Bed, patientInfo.IsBaby) < 0)
                    {
                        return -1;
                    }
                }

                #endregion
            }
            return 1;

        }


        #endregion
        [System.Obsolete("����ΪRecievePatient", true)]
        public int ArrivePatient(PatientInfo PatientInfo, string Status)
        {
            return 0;
        }

        #region ����û�д�λ����

        /// <summary>
        /// ���߽���-���»��߽�����Ϣ �ý����� ���´�λ��¼
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        /// <param name="bed">��λ��Ϣ</param>
        /// <param name="type">������</param>
        /// <param name="notes">ע����Ϣ</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int RecievePatient(PatientInfo patientInfo, Bed bed, EnumShiftType type, string notes)
        {
            FS.HISFC.Models.Base.EnumInState status = EnumInState.R;
            int parm;
            //Ӥ�������´�λ��Ϣ
            if (patientInfo.IsBaby == false)
            {
                //����ǰ�Ĵ�λ��Ϣ
                Bed oldBed = new Bed();
                oldBed.InpatientNO = "N"; //'N'����û�л���
                oldBed.Status.ID = "U"; //'U'����մ�

                //���º�Ĵ�λ��Ϣ
                bed.InpatientNO = patientInfo.ID;
                bed.Status.ID = "O";

                //���´�λ��Ϣ
                parm = UpdateBedStatus(bed, oldBed);
                if (parm != 1)
                {
                    return parm;
                }
            }

            patientInfo.PVisit.PatientLocation.Bed = bed;

            if (type.ToString() == "K")
            {
                //���ڱ�����͵���"����K"����Ժ״̬��"��Ժ�Ǽ�R"
                status = EnumInState.R;

                //���䴦��
                parm = RecievePatient(patientInfo, status);
                if (parm <= 0)
                {
                    return parm;
                }

                //�����Ϣ
                //{FA32C976-E003-4a10-9028-71F2CD154052} ����ʱ��
                if (SetShiftData(patientInfo.ID, patientInfo.PVisit.RegistTime, type, notes, patientInfo.PVisit.PatientLocation.NurseCell, patientInfo.PVisit.PatientLocation.Bed, patientInfo.IsBaby) < 0)
                {
                    return -1;
                }
            }
            else if (type.ToString() == "C")
            {
                //���ڱ�����͵���"�ٻ�C"����Ժ״̬��"��Ժ�Ǽ�B"
                status = EnumInState.B;

                //���䴦��
                parm = RecievePatient(patientInfo, status);
                if (parm <= 0)
                {
                    return parm;
                }

                if (SetShiftData(patientInfo.ID, type, notes, patientInfo.PVisit.PatientLocation.NurseCell, patientInfo.PVisit.PatientLocation.Bed, patientInfo.IsBaby) < 0)
                {
                    return -1;
                }

                #region

                //�ٻػ��߸��³�Ժ�ȼ�״̬��O��Ϊ��Ժ�ȼ�ȡ����EO��
                if (this.UpdateShiftData(patientInfo.ID, EnumShiftType.O.ToString(), EnumShiftType.EO.ToString(), shiftTypeEnumService.GetName(EnumShiftType.EO)) < 0)
                {
                    return -1;
                }
                #endregion

            }
            return 1;
        }

        #endregion

        #region ת��

        /// <summary>
        /// ���������˵Ĵ�λ��Ϣ
        /// <br>���룺Դ����,Ŀ�껼��</br>
        /// </summary>
        /// <param name="sourcePatientInfo">ԭ���߻�����Ϣ</param>
        /// <param name="targetPatientInfo">Ŀ�껼�߻�����Ϣ</param>
        /// <returns>0�ɹ� -1ʧ��</returns>
        public int SwapPatientBed(PatientInfo sourcePatientInfo, PatientInfo targetPatientInfo)
        {
            #region �ӿ�˵��

            //���������˵Ĵ�λ��Ϣ
            //���»��ߵĴ�λ�����ҡ�������Ϣ��
            //RADT.InPatient.SwapPatient.1
            //���룺old (0 InpatientNoסԺ��ˮ��,1��������,(2����id,3����name,
            //		4����id,5����name,6����id)
            //		new(7 סԺ��ˮ�ţ�8����,9����id,10����name,
            //		11����id,12����name,13����id)
            //������0 

            #endregion

            //���²�����
            if (sourcePatientInfo.PVisit.PatientLocation.Bed.ID != targetPatientInfo.PVisit.PatientLocation.Bed.ID)
            {
                //ǰ�߻�������
                int parm = TransferPatient(sourcePatientInfo, targetPatientInfo.PVisit.PatientLocation);
                if (parm != 1)
                {
                    return parm;
                }

                //���߻�������
                parm = TransferPatient(targetPatientInfo, sourcePatientInfo.PVisit.PatientLocation);
                if (parm != 1)
                {
                    return parm;
                }
            }

            return 1;
        }

        [System.Obsolete("����Ϊ SwapPatientBed", true)]
        public int SwapPatient(PatientInfo sourcePatientInfo, PatientInfo targetPatientInfo)
        {


            return 0;
        }
        #endregion

        #region ���ⴲλ���� ���������Ҵ���

        /// <summary>
        /// ���ⴲλ���� ���������Ҵ���
        /// �Ҵ��ô˺�������ȷ��Ŀ�괲λδռ��
        /// ��λ״̬˵���� C  = "�ر�" U = "�մ�" K = "��Ⱦ"  I = "����" O = "ռ��" R = "�ٴ�" W = "����" H = "�Ҵ�"
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣ</param>
        /// <param name="bedNO">����id</param>
        /// <param name="type">1�Ҵ� 2  ���� 3 �ر� status = "C" </param>
        /// <returns>0û�и��� 1 �ɹ� -1 ʧ��</returns>
        public int SwapPatientBed(PatientInfo patientInfo, string bedNO, string type)
        {
            #region �ӿ�˵��

            //���ⴲλ���� ���������Ҵ���
            //RADT.InPatient.PatientWapBed.1
            //���룺0 InpatientNoסԺ��ˮ��,1��������,2������
            //������0 

            #endregion

            string strSql = string.Empty;
            string strSql1 = string.Empty;
            string strStatus = string.Empty;
            int parm;
            if (type == "1")
            {
                strStatus = "H";
                //����ǰ�Ĵ�λ��Ϣ
                Bed oldBed = patientInfo.PVisit.PatientLocation.Bed.Clone();
                //���ĺ�Ĵ�λ��Ϣ
                patientInfo.PVisit.PatientLocation.Bed.Status.ID = NConvert.ToInt32(EnumBedStatus.U).ToString();
                patientInfo.PVisit.PatientLocation.Bed.InpatientNO = "N";

                //���´�λ״̬
                parm = UpdateBedStatus(patientInfo.PVisit.PatientLocation.Bed, oldBed);
                if (parm <= 0)
                {
                    return parm;
                }

                //���»��߻�����Ϣ��"
                if (Sql.GetCommonSql("RADT.InPatient.TransferPatient.1", ref strSql1) == 0)
                #region SQL
                /*
					UPDATE 	FIN_IPR_INMAININFO 
			        SET    	NURSE_CELL_CODE='{7}' ,    -- �����Ļ���վ����
					NURSE_CELL_NAME='{8}',     -- �����Ļ���վ����
					DEPT_CODE='{9}',           -- �����Ŀ��ұ���
					DEPT_NAME='{10}',          -- �����Ŀ�������
					BED_NO='{11}'              -- �����Ĵ���
			        WHERE  	PARENT_CODE  = '[��������]' 
			        AND   	CURRENT_CODE = '[��������]' 
			        AND    	INPATIENT_NO = '{0}'        --����סԺ��ˮ��
			        AND    	IN_STATE IN ('I','B')        --��Ժ״̬
					*/
                #endregion
                {
                    try
                    {
                        strSql1 =
                            string.Format(strSql1, patientInfo.ID, patientInfo.Name, patientInfo.PVisit.PatientLocation.NurseCell.ID,
                                          patientInfo.PVisit.PatientLocation.NurseCell.Name,
                                          patientInfo.PVisit.PatientLocation.Dept.ID, patientInfo.PVisit.PatientLocation.Dept.Name,
                                          patientInfo.PVisit.PatientLocation.Bed.ID, patientInfo.PVisit.PatientLocation.NurseCell.ID,
                                          patientInfo.PVisit.PatientLocation.NurseCell.Name, patientInfo.PVisit.PatientLocation.Dept.ID,
                                          patientInfo.PVisit.PatientLocation.Dept.Name, bedNO);
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        ErrCode = ex.Message;
                        WriteErr();
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
                if (ExecNoQuery(strSql1) <= 0)
                {
                    return -1;
                }
            }
            else if (type == "2")
            {
                strStatus = "W";
            }
            //�رյĴ�λ���մ�λ��.ת�ƺ��ͷŴ�λʱ��λ״̬��ΪC . {CA479D1B-BD94-459e-AA19-1AE2C4902DAF}
            else if (type == "3")
            {
                strStatus = "C";
            }


            if (Sql.GetCommonSql("RADT.InPatient.PatientWapBed.1", ref strSql) == -1)
            #region SQL
            /*
				 UPDATE COM_BEDINFO 
			     SET  CLINIC_NO= '{0}',BED_STATE = '{3}' 
			     WHERE  PARENT_CODE='[��������]'  and 
			     CURRENT_CODE='[��������]' and 
			     BED_NO = '{2}' and
			     BED_STATE <> 'O'
				 */
            #endregion
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, patientInfo.ID, patientInfo.Name, bedNO, strStatus);
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.PatientWapBed.1";
                WriteErr();
                return -1;
            }
            parm = ExecNoQuery(strSql);
            if (parm < 0) return parm;

            if (ChangeBedInfo(patientInfo.ID, bedNO, type) < 0)
            {
                return -1;
            }
            return 0;
        }


        [System.Obsolete("����Ϊ SwapPatientBed", true)]
        public int PatientWapBed(PatientInfo patientInfo, string bedNO, string type)
        {
            return 0;
        }
        #endregion

        #region ���»��߲���״̬

        /// <summary>
        /// ���»��߲���״̬-���´�λ״̬
        /// </summary>
        /// <param name="Bed">������Ϣ</param>
        /// <returns>0û���� >1 �ɹ� -1 ʧ��</returns>
        public int UpdateBedStatus(Bed Bed)
        {
            #region �ӿ�˵��

            //���»��ߵĴ�λ��Ϣ��
            //RADT.InPatient.UpdateSickBedInfo.1
            //���룺0 bed no ���� 1 InpatientNoסԺ��ˮ��,2 ��λ״̬)
            //������0 

            #endregion

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdateSickBedInfo.1", ref strSql) == -1)
            #region SQL
            /*
				 UPDATE COM_BEDINFO 
					SET   CLINIC_NO= '{1}',BED_STATE = '{2}' 
					WHERE PARENT_CODE='[��������]'  and 
			      CURRENT_CODE='[��������]' and 
			      BED_NO =	'{0}'
				  */
            #endregion
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, Bed.ID, Bed.InpatientNO, Bed.Status.ID);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���´�λ״̬��inpatientNo,����ԭ������Ϣ���в����ж�
        /// writed by cuipeng
        /// 2005-5
        /// </summary>
        /// <param name="newBed">�²�����Ϣ</param>
        /// <param name="oldBed">�ɲ�����Ϣ</param>
        /// <returns>0û���� >1 �ɹ� -1 ʧ��</returns>
        public int UpdateBedStatus(Bed newBed, Bed oldBed)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdateSickBedInfo.2", ref strSql) == -1)
            #region SQL
            /*
				UPDATE 	COM_BEDINFO 
			    SET   	CLINIC_NO= '{3}',        --���º�Ļ�����ˮ��
	            		BED_STATE = '{4}'        --���º�Ĵ�λ״̬
			    WHERE 	PARENT_CODE  = '[��������]'  
		        AND   	CURRENT_CODE = '[��������]' 
		        AND   	BED_NO       = '{0}'     --����ǰ�Ĵ���
		        AND   	CLINIC_NO    = '{1}'     --����ǰ�Ļ�����ˮ��
		        AND   	BED_STATE    = '{2}'     --����ǰ�Ĵ�λ״̬ 
				*/
            #endregion
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                                       newBed.ID, //0����
                                       oldBed.InpatientNO, //1����ǰ����ID
                                       oldBed.Status.ID, //2����ǰ��λ״̬
                                       newBed.InpatientNO, //3���º���ID
                                       newBed.Status.ID //4���º�λ״̬
                    );
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #region ��¼�������Ҵ���Ϣ


        /// <summary>
        /// ��¼�������Ҵ���Ϣ BED_KIND 1 �Ҵ� 2 ����
        /// STATUS ״̬ 0 �Ҵ� 1 ���
        /// </summary>
        /// <param name="inpatientNO">סԺ��</param>
        /// <param name="bedNO">����</param>
        /// <param name="kind">���</param>
        /// <returns>���� 0 �ɹ���С�� 0 ʧ��</returns>
        public int ChangeBedInfo(string inpatientNO, string bedNO, string kind)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdateBedInfoRecord.1", ref strSql) == -1)
            #region SQL
            /*UPDATE fin_ipr_hangbedinfo   --�Ҵ���Ϣ��

					 SET  status='1',   --״̬ 0 �Ҵ� 1 ���
							  bed_kind = '{2}',
						  oper_code='{3}',   --����Ա

						  oper_date=sysdate    --��������
				   WHERE PARENT_CODE='[��������]'  and 
						 CURRENT_CODE='[��������]' and 
						 INPATIENT_NO='{0}' and
						 BED_NO = '{1}' and
						 STATUS='0'
						 */
            #endregion
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNO, bedNO, kind, Operator.ID);
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.UpdateBedInfoRecord.1";
                WriteErr();
                return -1;
            }
            if (ExecNoQuery(strSql) <= 0)
            {
                if (Sql.GetCommonSql("RADT.InPatient.InsertBedInfoRecord.1", ref strSql) == -1)
                {
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, inpatientNO, bedNO, kind, Operator.ID);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    return -1;
                }
                return ExecNoQuery(strSql);
            }
            return 0;
        }
        [System.Obsolete("����ΪChangeBedInfo", true)]
        public int ChangeBedInfoRecord(string InPatientNo, string BedNo, string kind)
        {
            return 0;
        }
        #endregion

        #region  ����� ��ĳ�������ͷ�

        /// <summary>
        /// ����� ��ĳ�������ͷ�
        /// ��Ҵ��ô˺�������ȷ��Ŀ�괲λδռ��
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣ</param>
        /// <param name="bedNO">����id</param>
        /// <param name="type">1�Ҵ� 2  ����</param>
        /// <returns>0û�и��� 1�ɹ� -1 ʧ��</returns>
        public int UnWrapPatientBed(PatientInfo patientInfo, string bedNO, string type)
        {
            #region �ӿ�˵��

            //���� ����� ��ĳ�������ͷ�
            //RADT.InPatient.PatientUnWapBed.2
            //���룺0 InpatientNoסԺ��ˮ��,1��������,2������
            //������0 

            #endregion

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.PatientWapBed.2", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, patientInfo.ID, patientInfo.Name, bedNO);
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.PatientWapBed.2";
                WriteErr();
                return -1;
            }
            if (ExecNoQuery(strSql) <= 0)
            {
                return -1;
            }
            if (ChangeBedInfo(patientInfo.ID, bedNO, type) < 0)
            {
                return -1;
            }
            return 0;
        }
        [System.Obsolete("����Ϊ UnWrapPatientBed", true)]
        public int PatientUnWapBed(PatientInfo PatientInfo, string BedNo, string Type)
        {
            return 0;
        }
        #endregion

        #endregion

        #region ����ԤԼ��Ժ�Ǽ���Ϣ

        /// <summary>
        /// ���µ�����ȫ��ԤԼ��Ϣ
        /// </summary>
        /// <param name="patientInfo">������ż�¼��PatientInfo.memo</param>
        /// <returns></returns>
        public int UpdatePreIn(PatientInfo patientInfo)
        {
            #region "�ӿ�"

            //			�ӿ����� RADT.InPatient.UpdatePrepayIn.1
            //			<!-- 0 ���￨��,  1 �������, 2 ����,3 �Ա�,4 ���֤��, 5 ����, 6 ҽ��֤��,
            //			7 �������,  8 ��ͬ��λ, 9 ����,10 ��ʿվ����,    11 ְ��,12 ������λ,
            //			13 ������λ�绰,         14 ��ͥסַ,  15 ��ͥ�绰,16 ����,17 ������,
            //			18 ����,     19 ��ϵ��,  20 ��ϵ�˵绰,21 ��ϵ�˵�ַ,      22 ��ϵ�˹�ϵ,
            //			23 ����״��, 24 ����,    25 ��ϴ���,  26 �������,        27 ԤԼ����,
            //			28 �������� ,29 ԤԼҽʦ,30 ״̬, 31 ԤԼ����,   32 ����Ա,33 ��������-->

            #endregion

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdatePrepayIn.1", ref strSql) == -1) return -1;

            #region --

            try
            {
                string[] s = new string[34];

                s[0] = patientInfo.PID.CardNO; //���￨��
                s[1] = patientInfo.Memo;       //�������
                s[2] = patientInfo.Name;       //����
                s[3] = patientInfo.Sex.ID.ToString(); //�Ա�
                s[4] = patientInfo.IDCard; //���֤��
                s[5] = patientInfo.Birthday.ToString(); //����
                s[6] = patientInfo.SSN; //ҽ��֤��
                s[7] = patientInfo.Pact.PayKind.ID; //�������
                s[8] = patientInfo.Pact.ID; //��ͬ��λ
                s[9] = patientInfo.PVisit.PatientLocation.Bed.ID; //����
                s[10] = patientInfo.PVisit.PatientLocation.NurseCell.ID; //��ʿվ����
                s[11] = patientInfo.Profession.ID; //ְ��
                s[12] = patientInfo.CompanyName; //������λ
                s[13] = patientInfo.PhoneBusiness; //������λ�绰
                s[14] = patientInfo.AddressHome; //��ͥסַ
                s[15] = patientInfo.PhoneHome; //��ͥ�绰
                s[16] = patientInfo.DIST; //����
                s[17] = patientInfo.DIST; //������
                s[18] = patientInfo.Nationality.ID; //����
                s[19] = patientInfo.Kin.ID; //��ϵ��
                s[20] = patientInfo.Kin.RelationPhone; //��ϵ�˵绰
                s[21] = patientInfo.Kin.RelationAddress; //��ϵ�˵�ַ
                s[22] = patientInfo.Kin.Relation.ID; //��ϵ�˹�ϵ
                s[23] = patientInfo.MaritalStatus.ID.ToString(); //����״��
                s[24] = patientInfo.Country.ID; //����

                if (patientInfo.Diagnoses.Count > 0)
                {
                    s[25] = patientInfo.Diagnoses[0].ToString();//��ϴ���
                }

                s[26] = string.Empty; //�������
                s[27] = patientInfo.PVisit.PatientLocation.Dept.ID; //ԤԼ����
                s[28] = patientInfo.PVisit.PatientLocation.Dept.Name; //��������
                s[29] = patientInfo.PVisit.AdmittingDoctor.ID; //ԤԼҽʦ
                s[30] = "0"; //״̬
                s[31] = patientInfo.PVisit.InTime.ToString(); //ԤԼ����
                s[32] = Operator.ID; //����Ա
                s[33] = GetSysDateTime().ToString(); //��������

                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            #endregion
            return ExecNoQuery(strSql);
        }
        [System.Obsolete("����Ϊ UpdatePreIn", true)]
        public int UpdatePrepayIn(PatientInfo PatientInfo)
        {
            return 0;
        }
        #endregion

        #region ��Ӥ���Ǽ�

        /// <summary>
        /// ��Ӥ���Ǽ�
        /// </summary>
        /// <param name="babyInfo">��Ӥ����Ϣ</param>
        /// <returns> ���� 0 �ɹ�  С�� 0 ʧ��</returns>
        public int InsertNewBabyInfo(PatientInfo babyInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.ResigerNewBaby.1", ref strSql) == -1)
            {
                return -1;
            }

            #region SQL
            /*
			 INSERT INTO fin_ipr_babyinfo   --Ӥ��סԺ����
		          ( parent_code,   --����ҽ�ƻ�������
		            current_code,   --����ҽ�ƻ�������
		            inpatient_no,   --סԺ��ˮ��
		            happen_no,   --�������
		            name,   --����
		            sex_code,   --�Ա�
		            birthday,   --����
		            height,   --���
		            weight,   --����
		            blood_code,   --Ѫ�ͱ���
		            in_date,   --��Ժ����
		            prepay_outdate,   --��Ժ����(ԤԼ)
		            oper_code,   --����Ա
		            oper_date,
		            cancel_flag,
					MOTHER_INPATIENT_NO )  
		     VALUES 
		          ( '[��������]',   --����ҽ�ƻ�������
		            '[��������]',   --����ҽ�ƻ�������
		            '{0}',   --סԺ��ˮ��
		            (select NVL(MAX(happen_no),0)+1 from fin_ipr_babyinfo where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and  MOTHER_INPATIENT_NO = '{12}' ),   --�������
		            '{2}',   --����
		            '{3}',   --�Ա�
		            to_date('{4}','yyyy-mm-dd hh24:mi:ss'),   --����
		            '{5}',   --���
		            '{6}',   --����
		            '{7}',   --Ѫ�ͱ���
		            to_date('{8}','yyyy-mm-dd hh24:mi:ss'),   --��Ժ����
		            to_date('{9}','yyyy-mm-dd hh24:mi:ss'),   --��Ժ����(ԤԼ)
		            '{10}',   --����Ա
		            to_date('{11}','yyyy-mm-dd hh24:mi:ss'),'0', '{12}')
		*/
            #endregion

            try
            {
                string[] s = new string[16];
                s[0] = babyInfo.ID;     //סԺ��ˮ��
                s[1] = babyInfo.User01; //�������
                s[2] = babyInfo.Name;   //����
                s[3] = babyInfo.Sex.ID.ToString();   //�Ա�
                s[4] = babyInfo.Birthday.ToString(); //����
                s[5] = babyInfo.Height; //���
                s[6] = babyInfo.Weight; //����
                s[7] = babyInfo.BloodType.ID.ToString();      //Ѫ�ͱ���
                s[8] = babyInfo.PVisit.InTime.ToString();     //��Ժ����
                s[9] = babyInfo.PVisit.PreOutTime.ToString(); //��Ժ����(ԤԼ)
                s[10] = Operator.ID; //����Ա
                s[11] = GetSysDateTime().ToString();    //��������
                s[12] = babyInfo.PID.MotherInpatientNO; //ĸ��סԺ��ˮ��
                s[13] = babyInfo.DeliveryMode.ID;// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
                s[14] = babyInfo.DeliveryMode.Name;
                s[15] = babyInfo.Gestation; //�������ܣ�̥�䣩
                //s[16] = babyInfo.ResponsibleDoctor.ID;//����ҽʦ����
                //s[17] = babyInfo.ResponsibleDoctor.Name;//����ҽʦ����
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }
        [System.Obsolete("����Ϊ InsertNewBabyInfo", true)]
        public int ResigerNewBaby(PatientInfo BabyInfo)
        {
            return 0;
        }
        public int UpdateBabyInfo(PatientInfo babyInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.UpdateBabyInfo.1", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[16];
                s[0] = babyInfo.ID; //Ӥ��סԺ��ˮ��
                s[1] = babyInfo.User01; //�������
                s[2] = babyInfo.Name; //����
                s[3] = babyInfo.Sex.ID.ToString(); //�Ա�
                s[4] = babyInfo.Birthday.ToString(); //����
                s[5] = babyInfo.Height; //���
                s[6] = babyInfo.Weight; //����
                s[7] = babyInfo.BloodType.ID.ToString(); //Ѫ�ͱ���
                s[8] = babyInfo.PVisit.InTime.ToString(); //��Ժ����
                s[9] = babyInfo.PVisit.PreOutTime.ToString(); //��Ժ����(ԤԼ)
                s[10] = Operator.ID; //����Ա
                s[11] = GetSysDateTime().ToString(); //��������
                s[12] = babyInfo.PID.MotherInpatientNO; //ĸ��סԺ��ˮ��
                s[13] = babyInfo.DeliveryMode.ID;// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
                s[14] = babyInfo.DeliveryMode.Name;
                s[15] = babyInfo.Gestation;   // �������ܣ�̥�䣩
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// ȡ��Ӥ���Ǽ�
        /// </summary>
        /// <param name="inpatientNO"> סԺ��ˮ��</param>
        /// <returns>���� 0 �ɹ� С�� 0ʧ��</returns>
        public int DiscardBabyRegister(string inpatientNO)
        {
            string strSql = string.Empty;
            //���룺 0סԺ��ˮ��,1������� 
            if (Sql.GetCommonSql("RADT.Inpatient.CancelBaby.1", ref strSql) < 0)
            {
                return -1;
            }
            #region SQL
            /*
				update 	fin_ipr_babyinfo   --Ӥ��סԺ����
				set   	cancel_flag='1'    --
				where 	PARENT_CODE='[��������]' 
				AND     CURRENT_CODE='[��������]' 
				AND     inpatient_no='{0}' 
			*/
            #endregion
            try
            {
                strSql = string.Format(strSql, inpatientNO);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }
        [System.Obsolete("����Ϊ DiscardBabyRegister", true)]
        public int CancelBaby(string InpatientNo)
        {
            return 0;
        }

        /// <summary>
        /// ��û�������Ӥ��������ȡ���Ǽǵģ�
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <returns>Ӥ���б�</returns>
        public ArrayList QueryAllBabiesByMother(string inpatientNo)
        {
            ArrayList al = new ArrayList();
            string strSql = @"select 	INPATIENT_NO,		--0 Ӥ��סԺ��ˮ��
        				happen_no,		--1 �������   
                        name,      --2 ����
                          SEX_CODE,    --3 �Ա�
                          birthday,    --4 ��������
                          HEIGHT,      --5 ���
                          WEIGHT,      --6 ����
                          BLOOD_CODE,    --7 Ѫ�ͱ���
                          IN_DATE,    --8 ��Ժ����
                          PREPAY_OUTDATE,    --9 ��Ժ����(ԤԼ)
                          MOTHER_INPATIENT_NO,  --10Ӥ��סԺ��ˮ��
                          OPER_CODE,    --11����Ա
                          OPER_DATE,    --12��������
                          cancel_flag --ȡ����־ 1ȡ����0 ��Ч δȡ��
                           
                        from   fin_ipr_babyinfo 
                        where MOTHER_INPATIENT_NO = '{0}'  ";

            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }

            if (ExecQuery(strSql) == -1)
            {
                return null;
            }

            PatientInfo obj = null;
            while (Reader.Read())
            {
                obj = new PatientInfo();
                try
                {
                    obj.ID = Reader[0].ToString(); //0Ӥ��סԺ��ˮ��
                    obj.User01 = Reader[1].ToString(); //1�������
                    obj.Name = Reader[2].ToString(); //2����
                    obj.Sex.ID = Reader[3].ToString(); //3�Ա�
                    obj.Birthday = NConvert.ToDateTime(Reader[4]); //4��������
                    obj.Height = Reader[5].ToString(); //5���
                    obj.Weight = Reader[6].ToString(); //6����
                    obj.BloodType.ID = Reader[7].ToString(); //7Ѫ�ͱ���
                    obj.PVisit.InTime = NConvert.ToDateTime(Reader[8].ToString()); //8��Ժ����
                    obj.PVisit.PreOutTime = NConvert.ToDateTime(Reader[9].ToString()); //9��Ժ����(ԤԼ)
                    obj.PID.MotherInpatientNO = Reader[10].ToString(); //10ĸ��סԺ��ˮ��
                    obj.User02 = Reader[11].ToString(); //11����Ա
                    obj.User03 = Reader[12].ToString(); //12��������

                    //ȡ���Ǽǵ�Ӥ��
                    if (Reader[13].ToString() == "1")
                    {
                        obj.PVisit.InState.ID = "C";
                    }
                    al.Add(obj);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    Reader.Close();
                    return null;
                }

            }
            Reader.Close();

            //ȡӤ����סԺ�����е���Ϣ
            ArrayList alReturn = new ArrayList();
            PatientInfo babyMainInfo = null; //Ӥ��סԺ������Ϣ
            foreach (PatientInfo baby in al)
            {
                //ȡסԺ�����е�Ӥ����Ϣ
                babyMainInfo = QueryPatientInfoByInpatientNO(baby.ID);
                if (babyMainInfo == null || babyMainInfo.ID == string.Empty)
                {
                    return null;
                }

                babyMainInfo.PID.MotherInpatientNO = baby.PID.MotherInpatientNO; //ĸ��סԺ��ˮ��
                babyMainInfo.User01 = baby.User01; //Ӥ���������
                babyMainInfo.User02 = baby.User02; //�����˱���
                babyMainInfo.User03 = baby.User03; //��������

                //��ӵ�Ӥ��������
                alReturn.Add(babyMainInfo);
            }

            return alReturn;
        }

        /// <summary>
        /// ��û���Ӥ��
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <returns>Ӥ���б�</returns>
        public ArrayList QueryBabiesByMother(string inpatientNo)
        {
            ArrayList al = new ArrayList();
            string strSql = string.Empty;
            //ȡSQL���
            if (Sql.GetCommonSql("RADT.Inpatient.GetBabys.1", ref strSql) == -1)
            #region SQL
            /*
				select 	INPATIENT_NO,	--0 Ӥ��סԺ��ˮ��
        				happen_no,		--1 ������� 	
						name,			--2 ����
						SEX_CODE,		--3 �Ա�
						birthday,		--4 ��������
						HEIGHT,			--5 ���
						WEIGHT,			--6 ����
						BLOOD_CODE,		--7 Ѫ�ͱ���
						IN_DATE,		--8 ��Ժ����
						PREPAY_OUTDATE,		--9 ��Ժ����(ԤԼ)
						MOTHER_INPATIENT_NO,	--10Ӥ��סԺ��ˮ��
						OPER_CODE,		--11����Ա
						OPER_DATE		--12��������
				from 	fin_ipr_babyinfo 
				where 	PARENT_CODE='[��������]' 
					AND	CURRENT_CODE='[��������]' 
					AND	MOTHER_INPATIENT_NO = '{0}' 
					AND	cancel_flag='0'
				*/
            #endregion
            {
                return null;
            }

            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }

            if (ExecQuery(strSql) == -1)
            {
                return null;
            }

            PatientInfo obj = null;
            while (Reader.Read())
            {
                obj = new PatientInfo();
                try
                {
                    obj.ID = Reader[0].ToString(); //0Ӥ��סԺ��ˮ��
                    obj.User01 = Reader[1].ToString(); //1�������
                    obj.Name = Reader[2].ToString(); //2����
                    obj.Sex.ID = Reader[3].ToString(); //3�Ա�
                    obj.Birthday = NConvert.ToDateTime(Reader[4]); //4��������
                    obj.Height = Reader[5].ToString(); //5���
                    obj.Weight = Reader[6].ToString(); //6����
                    obj.BloodType.ID = Reader[7].ToString(); //7Ѫ�ͱ���
                    obj.PVisit.InTime = NConvert.ToDateTime(Reader[8].ToString()); //8��Ժ����
                    obj.PVisit.PreOutTime = NConvert.ToDateTime(Reader[9].ToString()); //9��Ժ����(ԤԼ)
                    obj.PID.MotherInpatientNO = Reader[10].ToString(); //10ĸ��סԺ��ˮ��
                    obj.User02 = Reader[11].ToString(); //11����Ա
                    obj.User03 = Reader[12].ToString(); //12��������
                    obj.DeliveryMode.ID = Reader[13].ToString();// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
                    obj.DeliveryMode.Name = Reader[14].ToString();
                    obj.Gestation = Reader[15].ToString(); //15 ��������

                    al.Add(obj);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    Reader.Close();
                    return null;
                }

            }
            Reader.Close();

            //ȡӤ����סԺ�����е���Ϣ
            ArrayList alReturn = new ArrayList();
            PatientInfo babyMainInfo = null; //Ӥ��סԺ������Ϣ
            foreach (PatientInfo baby in al)
            {
                //ȡסԺ�����е�Ӥ����Ϣ
                babyMainInfo = QueryPatientInfoByInpatientNO(baby.ID);
                if (babyMainInfo == null || babyMainInfo.ID == string.Empty)
                {
                    return null;
                }

                babyMainInfo.PID.MotherInpatientNO = baby.PID.MotherInpatientNO; //ĸ��סԺ��ˮ��
                babyMainInfo.User01 = baby.User01; //Ӥ���������
                babyMainInfo.User02 = baby.User02; //�����˱���
                babyMainInfo.User03 = baby.User03; //��������
                babyMainInfo.DeliveryMode.ID = baby.DeliveryMode.ID;// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
                babyMainInfo.DeliveryMode.Name = baby.DeliveryMode.Name;
                babyMainInfo.Gestation = baby.Gestation; //��������

                //babyMainInfo.ResponsibleDoctor.ID = baby.PVisit.ResponsibleDoctor.ID;//����ҽʦ����
                //babyMainInfo.ResponsibleDoctor.Name = baby.PVisit.ResponsibleDoctor.Name;//����ҽʦ����

                //��ӵ�Ӥ��������
                alReturn.Add(babyMainInfo);
            }

            return alReturn;
        }


        [System.Obsolete("����Ϊ QueryBabiesByMother", true)]
        public ArrayList GetBabys(string inpatientNo)
        {
            return null;
        }
        /// <summary>
        /// ���ĳ�����Ƿ�����ԺӤ��
        /// </summary>
        /// <param name="inpatientNO">ĸ��סԺ��ˮ��</param>
        /// <returns>1�У�0û��</returns>
        public int IsMotherHasBabiesInHos(string inpatientNO)
        {
            string strSql = string.Empty;
            //ȡSQL���
            if (Sql.GetCommonSql("RADT.Inpatient.GetBabys.2", ref strSql) == -1)
            #region SQL
            /*				
				SELECT  COUNT(*) 
				FROM   	FIN_IPR_INMAININFO R 
				WHERE  	R.IN_STATE IN ('I','B') 
				AND  	R.INPATIENT_NO IN 
				       ( 
						SELECT 	T.INPATIENT_NO 
						FROM   	FIN_IPR_BABYINFO T  
						WHERE  	T.MOTHER_INPATIENT_NO = '{0}'
					    ) 
				 */
            #endregion
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, inpatientNO);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }

            if (ExecQuery(strSql) == -1)
            {
                return -1;
            }

            int parm = 0;
            try
            {
                parm = FS.FrameWork.Function.NConvert.ToInt32(ExecSqlReturnOne(strSql));
            }
            catch
            {
                parm = 0;
            }

            return parm;
        }
        [System.Obsolete("����Ϊ IsMotherHasBabiesInHos", true)]
        public int GetBabysInHos(string inpatientNO)
        {
            return 0;
        }
        /// <summary>
        /// ������Ӥ��סԺ�� 
        /// </summary>
        /// <param name="inpatientNO">סԺ��</param>
        /// <returns>û�з��� 0 �з������� �ַ���</returns>
        public string GetMaxBabyNO(string inpatientNO)
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.Baby.GetMaxBabyNo", ref sql) == -1)
            {
                return "-1";
            }
            #region SQL
            /*
			    SELECT NVL(MAX(HAPPEN_NO),0)
				FROM  FIN_IPR_BABYINFO   
				WHERE PARENT_CODE  = '[��������]' 
				AND   CURRENT_CODE = '[��������]' 
				AND   MOTHER_INPATIENT_NO = '{0}'
				*/
            #endregion
            try
            {
                sql = string.Format(sql, inpatientNO);
            }
            catch
            {
                Err = "RADT.InPatient.Baby.GetMaxBabyNo ������ʱ�����";
                WriteErr();
                return "-1";
            }
            return ExecSqlReturnOne(sql);
        }
        [System.Obsolete("����Ϊ GetMaxBabyNO", true)]
        public string GetMaxBabyNo(string InpatientNo)
        {
            return string.Empty;
        }
        /// <summary>
        /// ����ĸ���Ƿ���Ӥ��־
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="isHasBaby">�Ƿ���Ӥ��</param>
        /// <returns>-1 ,0 </returns>
        public int UpdateMumBabyFlag(string inpatientNO, bool isHasBaby)
        {
            string strSQL = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdateMumBabyFlag", ref strSQL) == -1)
            {
                return -1;
            }
            #region SQL
            /*
			 UPDATE 	fin_ipr_inmaininfo  
				SET	baby_flag ='{1}' 
				WHERE	parent_code='[��������]' 
				AND	current_code='[��������]'  
				AND	inpatient_no='{0}'
		
			 */
            #endregion

            try
            {
                strSQL = string.Format(strSQL, inpatientNO, NConvert.ToInt32(isHasBaby).ToString());
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSQL);
        }

        //{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F} Ӥ���ķ����Ƿ������ȡ����������

        /// <summary>
        /// ͨ��Ӥ����סԺ��ˮ��,��ѯĸ�׵�סԺ��ˮ��
        /// </summary>
        /// <param name="babyInpatientNO">Ӥ��סԺ��ˮ��</param>
        /// <returns>ĸ�׵�סԺ��ˮ�� ���󷵻� null ���� string.Empty</returns>
        public string QueryBabyMotherInpatientNO(string babyInpatientNO)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("RADT.Inpatient.QueryBabyMotherInpatientNO.Query.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:RADT.Inpatient.QueryBabyMotherInpatientNO.Query.1��SQL���";

                return null;
            }
            try
            {
                sql = string.Format(sql, babyInpatientNO);
            }
            catch (Exception e)
            {
                this.Err = e.Message;

                return null;
            }

            return this.ExecSqlReturnOne(sql);
        }
        ////{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F} Ӥ���ķ����Ƿ������ȡ���������� ����

        #endregion

        #region shiftData

        /// <summary>
        /// ��ȡת����Ϣ
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientRADTInfo(string patientNo)
        {
            string strSQL = string.Empty;
            ArrayList alPatientRADT = new ArrayList();
            if (Sql.GetCommonSql("RADT.InPatient.QueryShiftDeptInfo", ref strSQL) == -1)
            {
                this.Err = "RADT.InPatient.QueryShiftDeptInfo����!";
                WriteErr();
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, patientNo);
            }
            catch (Exception ee)
            {
                Err = ee.Message;
                WriteErr();
                return null;
            }

            if (ExecQuery(strSQL) < 0)
            {
                return null;
            }
            try
            {
                while (Reader.Read())
                {
                    FS.HISFC.Models.Invalid.CShiftData myCShiftDate = new FS.HISFC.Models.Invalid.CShiftData();
                    myCShiftDate.ClinicNo = this.Reader[0].ToString();//סԺ��
                    myCShiftDate.Mark = this.Reader[1].ToString();//�������
                    myCShiftDate.OldDataCode = this.Reader[2].ToString();//�洢ԭ����
                    myCShiftDate.OldDataName = this.Reader[3].ToString();//�洢ԭ��ʿվ
                    myCShiftDate.NewDataCode = this.Reader[4].ToString();//�洢�¿���
                    myCShiftDate.NewDataName = this.Reader[5].ToString();//�洢�»�ʿվ
                    myCShiftDate.User01 = this.Reader[6].ToString();//ԭ����
                    myCShiftDate.User02 = this.Reader[7].ToString();//�´���
                    myCShiftDate.OperCode = this.Reader[8].ToString();//����Ա
                    myCShiftDate.User03 = this.Reader[9].ToString();//ȷ��ʱ��

                    alPatientRADT.Add(myCShiftDate);
                }
            }
            catch (Exception ee)
            {
                Reader.Close();
                Err = ee.Message;
                WriteErr();
                return null;
            }
            this.Reader.Close();
            return alPatientRADT;
        }
        /// <summary>
        /// ��ȡת����Ϣ
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientRADTShiftInfo(string patientNo)
        {
            string strSQL = string.Empty;
            ArrayList alPatientRADT = new ArrayList();
            if (Sql.GetCommonSql("RADT.InPatient.QueryShiftDeptInfo.1", ref strSQL) == -1)
            {
                this.Err = "RADT.InPatient.QueryShiftDeptInfo.1����!";
                WriteErr();
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, patientNo);
            }
            catch (Exception ee)
            {
                Err = ee.Message;
                WriteErr();
                return null;
            }

            if (ExecQuery(strSQL) < 0)
            {
                return null;
            }
            try
            {
                while (Reader.Read())
                {
                    FS.HISFC.Models.Invalid.CShiftData myCShiftDate = new FS.HISFC.Models.Invalid.CShiftData();
                    myCShiftDate.ClinicNo = this.Reader[0].ToString();//סԺ��
                    myCShiftDate.Mark = this.Reader[1].ToString();//�������
                    myCShiftDate.OldDataCode = this.Reader[2].ToString();//�洢ԭ����
                    myCShiftDate.OldDataName = this.Reader[3].ToString();//�洢ԭ��ʿվ
                    myCShiftDate.NewDataCode = this.Reader[4].ToString();//�洢�¿���
                    myCShiftDate.NewDataName = this.Reader[5].ToString();//�洢�»�ʿվ
                    myCShiftDate.User01 = this.Reader[6].ToString();//ԭ����
                    myCShiftDate.User02 = this.Reader[7].ToString();//�´���
                    myCShiftDate.OperCode = this.Reader[8].ToString();//����Ա
                    myCShiftDate.User03 = this.Reader[9].ToString();//ȷ��ʱ��
                    myCShiftDate.Memo = this.Reader[10].ToString();//���ã�Ӥ���Ƿ�һ��ת

                    alPatientRADT.Add(myCShiftDate);
                }
            }
            catch (Exception ee)
            {
                Reader.Close();
                Err = ee.Message;
                WriteErr();
                return null;
            }
            this.Reader.Close();
            return alPatientRADT;
        }

        /// <summary>
        /// ��û��ߵı����¼
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryPatientShiftInfo(string inpatientNO)
        {
            ArrayList al = new ArrayList();
            string strSql = string.Empty;

            #region �ӿ�˵��

            //RADT.Inpatient.GetShiftDataList.1
            //���룺סԺ����ˮ��
            //������סԺ��ˮ��,�������,�������,ԭ���ϴ���,ԭ��������,�����ϴ���,����������,���ԭ�� ,����Ա���� ,����ʱ��,��ע                                                            

            #endregion

            if (Sql.GetCommonSql("RADT.Inpatient.GetShiftDataList.1", ref strSql) == 0)
            {
                #region SQL
                /* select	HAPPEN_NO,SHIFT_TYPE,OLD_DATA_CODE,
				 * OLD_DATA_NAME,NEW_DATA_CODE,NEW_DATA_NAME,mark,
				 * SHIFT_CAUSE,OPER_CODE,OPER_Date from com_shiftdata	
				 * where  PARENT_CODE='[��������]' 
				 * and CURRENT_CODE='[��������]' 
				 * and clinic_no='{0}' 	
				 */
                #endregion
                try
                {
                    strSql = string.Format(strSql, inpatientNO);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                ExecQuery(strSql);
                while (Reader.Read())
                {
                    NeuObject obj = new NeuObject();
                    NeuObject old_Obj = new NeuObject();
                    NeuObject new_Obj = new NeuObject();
                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    old_Obj.ID = Reader[2].ToString();
                    old_Obj.Name = Reader[3].ToString();
                    new_Obj.ID = Reader[4].ToString();
                    new_Obj.Name = Reader[5].ToString();
                    obj.Memo = Reader[6].ToString();
                    obj.User01 = Reader[7].ToString();
                    obj.User02 = Reader[8].ToString();
                    obj.User03 = Reader[9].ToString();
                    al.Add(obj);
                }
                Reader.Close();
            }
            else
            {
                return null;
            }
            return al;
        }

        /// <summary>
        /// ��û��ߵı����¼,���ص���CShiftData(ע����Щ�ֶ�û����Ӧʵ�壬�����鿴������ȡֵ!)
        /// �ϱ�ԭ�����Ǹ����ص���NeuObject����ȫ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryPatientShiftInfoNew(string inpatientNO)
        {
            ArrayList al = new ArrayList();
            string strSql = string.Empty;
            #region �ӿ�˵��
            //RADT.Inpatient.GetShiftDataList.1
            //���룺סԺ����ˮ��
            //������סԺ��ˮ��,�������,�������,ԭ���ϴ���,ԭ��������,�����ϴ���,����������,���ԭ�� ,����Ա���� ,����ʱ��,��ע
            #endregion
            if (Sql.GetCommonSql("RADT.Inpatient.GetShiftDataList.1", ref strSql) == 0)
            {
                /*
                 *  select  HAPPEN_NO,
                            SHIFT_TYPE,
                            OLD_DATA_CODE,
                            OLD_DATA_NAME,
                            NEW_DATA_CODE,
                            NEW_DATA_NAME,
                            mark,
                            SHIFT_CAUSE,
                            OPER_CODE,
                            OPER_Date 
                    from com_shiftdata  
                    where   clinic_no='{0}'   
                 */
                try
                {
                    strSql = string.Format(strSql, inpatientNO);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.Invalid.CShiftData myCShiftDate = new FS.HISFC.Models.Invalid.CShiftData();

                    myCShiftDate.User01 = Reader[0].ToString();
                    myCShiftDate.ShitType = Reader[1].ToString();
                    myCShiftDate.OldDataCode = Reader[2].ToString();
                    myCShiftDate.OldDataName = Reader[3].ToString();
                    myCShiftDate.NewDataCode = Reader[4].ToString();
                    myCShiftDate.NewDataName = Reader[5].ToString();
                    myCShiftDate.Mark = Reader[6].ToString();
                    myCShiftDate.ShitCause = Reader[7].ToString();
                    myCShiftDate.OperCode = Reader[8].ToString();
                    myCShiftDate.Memo = ((DateTime)Reader[9]).GetDateTimeFormats('g')[0].ToString();
                    al.Add(myCShiftDate);
                }
                Reader.Close();
            }
            else
            {
                return null;
            }
            return al;
        }


        [System.Obsolete("����Ϊ QueryPatientShiftInfo", true)]
        public ArrayList GetShiftDataList(string inpatientNO)
        {
            return null;
        }



        /// <summary>
        /// ���ñ����Ϣ-��������Ϣ��
        /// insert {FA32C976-E003-4a10-9028-71F2CD154052} ����ʱ��
        /// </summary>
        /// <param name="InpatientNo">סԺ��ˮ��</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="Type">�������</param>
        /// <param name="Memo">��ע</param>
        /// <param name="oldObject">���ǰ����</param>
        /// <param name="newObject">���������</param>
        /// <param name="IsBaby">�Ƿ�Ӥ��</param>
        /// <returns>0 �ɹ�  -1ʧ��</returns>
        public int SetShiftData(string InpatientNo, DateTime operDate, EnumShiftType Type, string Memo, NeuObject oldObject, NeuObject newObject, bool IsBaby)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("RADT.InPatient.ShiftData.2", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                    InpatientNo,		//0 ����סԺ��ˮ��
                    Type.ToString(),	//1 �������
                    oldObject.ID,		//2 ���ǰ���ݱ���
                    oldObject.Name,		//3 ���ǰ��������
                    newObject.ID,		//4 ��������ݱ���
                    newObject.Name,		//5 �������������
                    Memo,				//6 ��ע
                    this.Operator.ID,	//7 ������
                    ((Employee)Operator).Dept.ID,//8 ���������ڿ���
                    oldObject.Memo,		//9 ���ǰ����վ����
                    newObject.Memo		//10 �������վ����

                    //{FA32C976-E003-4a10-9028-71F2CD154052} ����ʱ��
                    , operDate.ToString("yyyy-MM-dd HH:mm:ss")
                    );
            }
            catch
            {
                this.Err = "�����������RADT.InPatient.ShiftData.2!";
                this.WriteErr();
                return -1;
            }

            int parm = this.ExecNoQuery(strSql);
            if (parm != 1)
                return parm;

            //������߲���Ӥ��,���Ҵ�λ�����仯�������סԺ�ձ���
            //added by cuipeng 
            //2005-4
            //1����FS.HISFC.Management.RADT.InPatient.enuShiftType.K�����
            //2ת�ƣ�ת�롢ת����FS.HISFC.Management.RADT.InPatient.enuShiftType.RD��ת�ƣ�
            //3��Ժ�Ǽ�FS.HISFC.Management.RADT.InPatient.enuShiftType.O����Ժ�Ǽǣ�
            //4�л�FS.HISFC.Management.RADT.InPatient.enuShiftType.C���ٻأ�
            //5�޷���Ժ��FS.HISFC.Management.RADT.InPatient.enuShiftType.OF���޷���Ժ"
            if (!IsBaby && (Type.ToString() == "K" || Type.ToString() == "RI" || Type.ToString() == "RO" || Type.ToString() == "O" || Type.ToString() == "C" || Type.ToString() == "OF"))
            {
                try
                {
                    //ȡ���ߴ�ʱ�̵���Ϣ��ת��֮�����Ϣ��
                    //FS.HISFC.Models.RADT.PatientInfo patientInfo = this.GetPatientInfoByPatientNO(InpatientNo);
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.QueryPatientInfoByInpatientNO(InpatientNo);
                    if (patientInfo == null)
                        return -1;

                    //����޷���Ժʱû�д�λ���򲻸���סԺ�ձ�
                    if (patientInfo.PVisit.PatientLocation.Bed.ID == "" && Type.ToString() == "OF")
                        return 1;


                    //ȡϵͳʱ��
                    DateTime sysDate = Convert.ToDateTime(this.GetSysDate() + " 00:00:00");

                    //����סԺ�ձ�������
                    InpatientDayReport reportManager = new InpatientDayReport();

                    //����Transaction
                    reportManager.SetTrans(this.Trans);

                    //�������µĿ��ұ���ͻ���վ����,����ת��ǰ�Ŀ��Ҳ���ȷ
                    if (Type.ToString() == "RO")
                    {
                        patientInfo.PVisit.PatientLocation.Dept.ID = oldObject.ID;			//ת��ǰ���ұ���
                        patientInfo.PVisit.PatientLocation.NurseCell.ID = oldObject.Memo;	//ת��ǰ����վ����
                        patientInfo.PVisit.PatientLocation.Bed.ID = oldObject.User01;		//ת��ǰ��λ����
                        patientInfo.PVisit.PatientLocation.Dept.User03 = newObject.ID;		//user03��������ת�ƺ�Ŀ��ұ���
                    }
                    else
                    {
                        patientInfo.PVisit.PatientLocation.Dept.User03 = oldObject.ID;		//user03��������ת��ǰ�Ŀ��ұ���
                    }

                    parm = reportManager.DynamicUpdate(patientInfo, Type.ToString());
                    if (parm != 1)
                    {
                        this.Err = reportManager.Err;
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
            }//end if
            return 1;
        }

        /// <summary>
        /// ����סԺ��ˮ�š�������š�������͸���һ�������Ϣ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="shiftData"></param>
        /// <returns></returns>
        public int UpdateShiftData(FS.HISFC.Models.Invalid.CShiftData shiftData)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("RADT.InPatient.ShiftData.Update", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                    shiftData.ClinicNo,
                    shiftData.HappenNo,
                    shiftData.ShitType,
                    shiftData.OldDataCode,
                    shiftData.OldDataName,
                    shiftData.NewDataCode,
                    shiftData.NewDataName,
                    shiftData.ShitCause,
                    shiftData.Mark,
                    shiftData.OperCode,
                    //����ʱ��ȡsysdate
                    //����Ա����ʵ�����޶�Ӧ
                    ((Employee)Operator).Dept.ID
                    );
            }
            catch
            {
                this.Err = "�����������RADT.InPatient.ShiftData.Update!";
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���»���״̬
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <param name="OldType"></param>
        /// <param name="Type"></param>
        /// <param name="Mark"></param>
        /// <returns></returns>
        public int UpdateShiftData(string InpatientNo, string OldType, string Type, string TypeName)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("RADT.InPatient.ShiftData.UpdateStatus", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, InpatientNo, OldType, Type, TypeName);
            }
            catch
            {
                this.Err = "�����������RADT.InPatient.ShiftData.UpdateStatus!";
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ���ñ����Ϣ-��������Ϣ��
        /// insert
        /// </summary>
        /// <param name="InpatientNo">סԺ��ˮ��</param>
        /// <param name="Type">�������</param>
        /// <param name="Memo">��ע</param>
        /// <param name="oldObject">���ǰ����</param>
        /// <param name="newObject">���������</param>
        /// <param name="IsBaby">�Ƿ�Ӥ��</param>
        /// <returns>0 �ɹ�  -1ʧ��</returns>
        public int SetShiftData(string InpatientNo, EnumShiftType Type, string Memo, NeuObject oldObject, NeuObject newObject, bool IsBaby)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("RADT.InPatient.ShiftData.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                    InpatientNo,		//0 ����סԺ��ˮ��
                    Type.ToString(),	//1 �������
                    oldObject.ID,		//2 ���ǰ���ݱ���
                    oldObject.Name,		//3 ���ǰ��������
                    newObject.ID,		//4 ��������ݱ���
                    newObject.Name,		//5 �������������
                    Memo,				//6 ��ע
                    this.Operator.ID,	//7 ������
                    ((Employee)Operator).Dept.ID,//8 ���������ڿ���
                    oldObject.Memo,		//9 ���ǰ����վ����
                    newObject.Memo		//10 �������վ����
                    );
            }
            catch
            {
                this.Err = "�����������RADT.InPatient.ShiftData.1!";
                this.WriteErr();
                return -1;
            }

            int parm = this.ExecNoQuery(strSql);
            if (parm != 1) return parm;

            //������߲���Ӥ��,���Ҵ�λ�����仯�������סԺ�ձ���
            //added by cuipeng 
            //2005-4
            //1����FS.HISFC.Management.RADT.InPatient.enuShiftType.K�����
            //2ת�ƣ�ת�롢ת����FS.HISFC.Management.RADT.InPatient.enuShiftType.RD��ת�ƣ�
            //3��Ժ�Ǽ�FS.HISFC.Management.RADT.InPatient.enuShiftType.O����Ժ�Ǽǣ�
            //4�л�FS.HISFC.Management.RADT.InPatient.enuShiftType.C���ٻأ�
            //5�޷���Ժ��FS.HISFC.Management.RADT.InPatient.enuShiftType.OF���޷���Ժ"
            //if (InpatientNo.IndexOf("ZY") == 0 && InpatientNo.IndexOf("B") < 0 && (Type.ToString() == "K" || Type.ToString() == "RI" || Type.ToString() == "RO" || Type.ToString() == "O" || Type.ToString() == "C" || Type.ToString() == "OF" || Type.ToString() == "CN" || Type.ToString() == "CNO"))
            if (!IsBaby && (Type.ToString() == "K" || Type.ToString() == "RI" || Type.ToString() == "RO" || Type.ToString() == "O" || Type.ToString() == "C" || Type.ToString() == "OF" || Type.ToString() == "CN" || Type.ToString() == "CNO"))
            {
                try
                {
                    //ȡ���ߴ�ʱ�̵���Ϣ��ת��֮�����Ϣ��
                    //FS.HISFC.Models.RADT.PatientInfo patientInfo = this.GetPatientInfoByPatientNO(InpatientNo);
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.QueryPatientInfoByInpatientNO(InpatientNo);
                    if (patientInfo == null) return -1;

                    //����޷���Ժʱû�д�λ���򲻸���סԺ�ձ�
                    if (patientInfo.PVisit.PatientLocation.Bed.ID == "" && Type.ToString() == "OF") return 1;


                    //ȡϵͳʱ��
                    DateTime sysDate = Convert.ToDateTime(this.GetSysDate() + " 00:00:00");

                    //����סԺ�ձ�������
                    InpatientDayReport reportManager = new InpatientDayReport();

                    //����Transaction
                    reportManager.SetTrans(this.Trans);

                    //�������µĿ��ұ���ͻ���վ����,����ת��ǰ�Ŀ��Ҳ���ȷ
                    //{B1E611C2-7A04-4b79-B64B-3D280D5769CE} �޸Ĳ����ձ�
                    if (Type.ToString() == "RO" || Type.ToString() == "CNO")
                    {
                        if (oldObject.User02 != "N")
                        {
                            patientInfo.PVisit.PatientLocation.Dept.ID = oldObject.ID;			//ת��ǰ���ұ���
                            patientInfo.PVisit.PatientLocation.NurseCell.ID = oldObject.Memo;	//ת��ǰ����վ����
                            patientInfo.PVisit.PatientLocation.Bed.ID = oldObject.User01;		//ת��ǰ��λ����
                            patientInfo.PVisit.PatientLocation.Dept.User03 = newObject.ID;		//user03��������ת�ƺ�Ŀ��ұ���

                        }
                        else
                        {
                            patientInfo.PVisit.PatientLocation.Dept.ID = oldObject.User03;			//ת��ǰ���ұ���
                            patientInfo.PVisit.PatientLocation.NurseCell.ID = oldObject.ID;	//ת��ǰ����վ����
                            patientInfo.PVisit.PatientLocation.Bed.ID = oldObject.User01;		//ת��ǰ��λ����
                            patientInfo.PVisit.PatientLocation.Dept.User03 = newObject.ID;		//user03��������ת�ƺ�Ŀ��ұ���
                        }
                    }
                    else
                    {
                        patientInfo.PVisit.PatientLocation.Dept.User03 = oldObject.ID;		//user03��������ת��ǰ�Ŀ��ұ���
                    }

                    //[2011-5-17] zhaozf ����ת�ƺ�Ļ���վ����
                    if (Type.ToString() == "RI" || Type.ToString() == "CN")
                    {
                        patientInfo.PVisit.PatientLocation.NurseCell.User03 = oldObject.Memo; //ת�ƺ�Ļ���վ����
                    }
                    else
                    {
                        patientInfo.PVisit.PatientLocation.NurseCell.User03 = newObject.Memo; //ת�ƺ�Ļ���վ����
                    }

                    #region {8997C648-0AE4-42f4-943A-4E34EC127B39}
                    //�ٻص��ձ������ǿ�ʹ���˻��ߵĳ�Ժʱ�䣬���ٻز����ѻ�����Ϣ�ĳ�Ժʱ�������
                    //�˺�����������ȡ�˻�����Ϣ������ûȡ����Ժʱ�䣬������ȡһ�±����Ϣ���ѻ������µĳ�Ժʱ���ҵ�
                    if (Type.ToString() == "C")
                    {
                        ArrayList altmp = this.QueryPatientShiftInfoNew(patientInfo.ID);

                        DateTime outtime = DateTime.MinValue;

                        foreach (FS.HISFC.Models.Invalid.CShiftData myCShiftDate in altmp)
                        {
                            if (myCShiftDate.ShitType == "O")
                            {
                                if (outtime < FS.FrameWork.Function.NConvert.ToDateTime(myCShiftDate.Memo))
                                {
                                    outtime = FS.FrameWork.Function.NConvert.ToDateTime(myCShiftDate.Memo);
                                }
                            }
                        }

                        patientInfo.PVisit.OutTime = outtime;
                    }
                    #endregion

                    //����¿��ұ���==�ɿ��ұ������ת���������ò����ձ�{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
                    if (oldObject.ID != newObject.ID || oldObject.Memo != newObject.Memo)
                    {
                        parm = reportManager.DynamicUpdate(patientInfo, Type.ToString());
                        if (parm != 1)
                        {
                            this.Err = reportManager.Err;
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
            }//end if
            return 1;
        }

        /// <summary>
        /// ���ñ����Ϣ-��������Ϣ��
        /// insert
        /// </summary>
        /// <param name="InpatientNo">סԺ��ˮ��</param>
        /// <param name="Type">�������</param>
        /// <param name="Memo">��ע</param>
        /// <param name="oldObject">���ǰ����</param>
        /// <param name="newObject">���������</param>
        /// <param name="IsBaby">�Ƿ�Ӥ��</param>
        /// <returns>0 �ɹ�  -1ʧ��</returns>
        public int SetShiftData(string InpatientNo, EnumShiftType Type, string Memo, NeuObject oldObject, NeuObject newObject, bool IsBaby, DateTime operDate)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("RADT.InPatient.ShiftData.Insert", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                    InpatientNo,		//0 ����סԺ��ˮ��
                    Type.ToString(),	//1 �������
                    oldObject.ID,		//2 ���ǰ���ݱ���
                    oldObject.Name,		//3 ���ǰ��������
                    newObject.ID,		//4 ��������ݱ���
                    newObject.Name,		//5 �������������
                    Memo,				//6 ��ע
                    this.Operator.ID,	//7 ������
                    ((Employee)Operator).Dept.ID,//8 ���������ڿ���
                    oldObject.Memo,		//9 ���ǰ����վ����
                    newObject.Memo,		//10 �������վ����
                    operDate.ToString()
                    );
            }
            catch
            {
                this.Err = "�����������RADT.InPatient.ShiftData.Insert!";
                this.WriteErr();
                return -1;
            }

            int parm = this.ExecNoQuery(strSql);
            if (parm != 1) return parm;

            //������߲���Ӥ��,���Ҵ�λ�����仯�������סԺ�ձ���
            //added by cuipeng 
            //2005-4
            //1����FS.HISFC.Management.RADT.InPatient.enuShiftType.K�����
            //2ת�ƣ�ת�롢ת����FS.HISFC.Management.RADT.InPatient.enuShiftType.RD��ת�ƣ�
            //3��Ժ�Ǽ�FS.HISFC.Management.RADT.InPatient.enuShiftType.O����Ժ�Ǽǣ�
            //4�л�FS.HISFC.Management.RADT.InPatient.enuShiftType.C���ٻأ�
            //5�޷���Ժ��FS.HISFC.Management.RADT.InPatient.enuShiftType.OF���޷���Ժ"
            //if (InpatientNo.IndexOf("ZY") == 0 && InpatientNo.IndexOf("B") < 0 && (Type.ToString() == "K" || Type.ToString() == "RI" || Type.ToString() == "RO" || Type.ToString() == "O" || Type.ToString() == "C" || Type.ToString() == "OF" || Type.ToString() == "CN" || Type.ToString() == "CNO"))
            if (!IsBaby && (Type.ToString() == "K" || Type.ToString() == "RI" || Type.ToString() == "RO" || Type.ToString() == "O" || Type.ToString() == "C" || Type.ToString() == "OF" || Type.ToString() == "CN" || Type.ToString() == "CNO"))
            {
                try
                {
                    //ȡ���ߴ�ʱ�̵���Ϣ��ת��֮�����Ϣ��
                    //FS.HISFC.Models.RADT.PatientInfo patientInfo = this.GetPatientInfoByPatientNO(InpatientNo);
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.QueryPatientInfoByInpatientNO(InpatientNo);
                    if (patientInfo == null) return -1;

                    //����޷���Ժʱû�д�λ���򲻸���סԺ�ձ�
                    if (patientInfo.PVisit.PatientLocation.Bed.ID == "" && Type.ToString() == "OF") return 1;


                    //ȡϵͳʱ��
                    DateTime sysDate = Convert.ToDateTime(this.GetSysDate() + " 00:00:00");

                    //����סԺ�ձ�������
                    InpatientDayReport reportManager = new InpatientDayReport();

                    //����Transaction
                    reportManager.SetTrans(this.Trans);

                    //�������µĿ��ұ���ͻ���վ����,����ת��ǰ�Ŀ��Ҳ���ȷ
                    //{B1E611C2-7A04-4b79-B64B-3D280D5769CE} �޸Ĳ����ձ�
                    if (Type.ToString() == "RO" || Type.ToString() == "CNO")
                    {
                        if (oldObject.User02 != "N")
                        {
                            patientInfo.PVisit.PatientLocation.Dept.ID = oldObject.ID;			//ת��ǰ���ұ���
                            patientInfo.PVisit.PatientLocation.NurseCell.ID = oldObject.Memo;	//ת��ǰ����վ����
                            patientInfo.PVisit.PatientLocation.Bed.ID = oldObject.User01;		//ת��ǰ��λ����
                            patientInfo.PVisit.PatientLocation.Dept.User03 = newObject.ID;		//user03��������ת�ƺ�Ŀ��ұ���

                        }
                        else
                        {
                            patientInfo.PVisit.PatientLocation.Dept.ID = oldObject.User03;			//ת��ǰ���ұ���
                            patientInfo.PVisit.PatientLocation.NurseCell.ID = oldObject.ID;	//ת��ǰ����վ����
                            patientInfo.PVisit.PatientLocation.Bed.ID = oldObject.User01;		//ת��ǰ��λ����
                            patientInfo.PVisit.PatientLocation.Dept.User03 = newObject.ID;		//user03��������ת�ƺ�Ŀ��ұ���
                        }
                    }
                    else
                    {
                        patientInfo.PVisit.PatientLocation.Dept.User03 = oldObject.ID;		//user03��������ת��ǰ�Ŀ��ұ���
                    }

                    //[2011-5-17] zhaozf ����ת�ƺ�Ļ���վ����
                    if (Type.ToString() == "RI" || Type.ToString() == "CN")
                    {
                        patientInfo.PVisit.PatientLocation.NurseCell.User03 = oldObject.Memo; //ת�ƺ�Ļ���վ����
                    }
                    else
                    {
                        patientInfo.PVisit.PatientLocation.NurseCell.User03 = newObject.Memo; //ת�ƺ�Ļ���վ����
                    }

                    #region {8997C648-0AE4-42f4-943A-4E34EC127B39}
                    //�ٻص��ձ������ǿ�ʹ���˻��ߵĳ�Ժʱ�䣬���ٻز����ѻ�����Ϣ�ĳ�Ժʱ�������
                    //�˺�����������ȡ�˻�����Ϣ������ûȡ����Ժʱ�䣬������ȡһ�±����Ϣ���ѻ������µĳ�Ժʱ���ҵ�
                    if (Type.ToString() == "C")
                    {
                        ArrayList altmp = this.QueryPatientShiftInfoNew(patientInfo.ID);

                        DateTime outtime = DateTime.MinValue;

                        foreach (FS.HISFC.Models.Invalid.CShiftData myCShiftDate in altmp)
                        {
                            if (myCShiftDate.ShitType == "O")
                            {
                                if (outtime < FS.FrameWork.Function.NConvert.ToDateTime(myCShiftDate.Memo))
                                {
                                    outtime = FS.FrameWork.Function.NConvert.ToDateTime(myCShiftDate.Memo);
                                }
                            }
                        }

                        patientInfo.PVisit.OutTime = outtime;
                    }
                    #endregion

                    //����¿��ұ���==�ɿ��ұ������ת���������ò����ձ�{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
                    if (oldObject.ID != newObject.ID || oldObject.Memo != newObject.Memo)
                    {
                        parm = reportManager.DynamicUpdate(patientInfo, Type.ToString());
                        if (parm != 1)
                        {
                            this.Err = reportManager.Err;
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
            }//end if
            return 1;
        }

        #endregion

        #region ��ݱ��
        /// <summary>
        /// �޸�סԺ�����ͬ��λ�ͽ��������Ϣ
        /// </summary>
        /// <param name="PatientNo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdatemainInfoPact(string PatientNo, NeuObject obj)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("RADT.InPatient.UpdatePact", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                    PatientNo,	//4 		סԺ��	
                    obj.ID,	//1 ��ͬ��λ����
                    obj.Name,		//2 	��ͬ��λ����	
                    obj.User01,	//3 ����������
                    //					obj.User02,	//0 �����������
                    obj.User03

                    );
            }
            catch (Exception ex)
            {
                this.Err = "�����������RADT.InPatient.ShiftData.1!";
                this.ErrCode = ex.Message;
                this.WriteErr();

                return -1;
            }
            int parm = this.ExecNoQuery(strSql);
            if (parm != 1)
            {
                return parm;
            }

            return 0;

        }
        /// <summary>
        /// ��ݱ�������ı���󹫷ѻ���������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateMainInfoPubPact(PatientInfo patient, NeuObject obj)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("RADT.InPatient.UpdateMainInfoPubPact", ref strSql) == -1)
            {
                return -1;
            }
            /*update fin_ipr_inmaininfo 
            Set    PACT_CODE = '{1}',--		��ͬ����
                PACT_NAME ='{2}',--	��ͬ��λ����
                PAYKIND_CODE	='{3}',--		������� 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�       
                MCARD_NO = '{4}',
                FEE_INTERVAL = {5},
                bed_limit = {6},
                bedoverdeal='{7}',
                day_limit ={8},
                limit_tot={9},
                limit_overtop={10},
                air_limit={11}       
            WHERE  PARENT_CODE='[��������]'	
            and 	CURRENT_CODE='[��������]' 
            and inpatient_no ='{0}'
            */
            try
            {
                strSql = string.Format(strSql,
                    patient.ID,	//0 		סԺ��	
                    obj.ID,	//1 ��ͬ��λ����
                    obj.Name,		//2 	��ͬ��λ����	
                    obj.User01,	//3 ����������
                    obj.User03,//4����
                    patient.FT.FixFeeInterval.ToString(),//5�̶���������
                    patient.FT.BedLimitCost.ToString(),//6��λ��׼
                    patient.FT.BedOverDeal,//7��λ����
                    patient.FT.DayLimitCost.ToString(),//8���޶�
                    patient.FT.DayLimitTotCost.ToString(),//9�����ܶ�
                    patient.FT.OvertopCost.ToString(),//10������
                    patient.FT.AirLimitCost.ToString()//11�໤��									
                    );
            }
            catch (Exception ex)
            {
                this.Err = "�����������RADT.InPatient.UpdateMainInfoPubPact!";
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }
        /// <summary>
        /// �޸Ļ�����Ϣ���ͬ��λ�ͽ��������Ϣ
        /// </summary>
        /// <param name="PatientNo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdatePatientPact(string PatientNo, NeuObject obj)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("RADT.InPatient.SetPact", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                    PatientNo,
                    obj.User01,
                    obj.User02,//0 ������ˮ��
                    obj.ID,	//1 ��ͬ��λ����
                    obj.Name		//2 ��ͬ��λ����
                    //3 ����������				
                    );
            }
            catch (Exception ex)
            {
                this.Err = "�����������RADT.InPatient.SetPact!";
                this.ErrCode = ex.Message;
                this.WriteErr();

                return -1;
            }
            int parm = this.ExecNoQuery(strSql);
            if (parm != 1)
            {
                return parm;
            }

            return 0;
        }
        /// <summary>
        /// ��ݱ��
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int SetPactShiftData(PatientInfo patient, NeuObject newobj, NeuObject oldobj)
        {
            if (patient.Pact.PayKind.ID == "03")/*���ѻ��ߣ�Add by maokb -2006-2-24*/
            {
                if (this.UpdateMainInfoPubPact(patient, newobj) != 1)
                {
                    this.Err = "����סԺ����ʧ��!";
                    return -1;
                }
            }
            else
            {
                if (this.UpdatemainInfoPact(patient.ID, newobj) != 0)
                {
                    this.Err = "����סԺ����ʧ��!";
                    return -1;
                }
            }
            //if (this.UpdatePatientPact(patient.PID.CardNO, newobj) != 0)
            //{
            //    this.Err = "���»�����Ϣ��ʧ��!";
            //    return -1;
            //}
            if (this.SetShiftData(patient.ID, EnumShiftType.CP, "��ݱ��", oldobj, newobj, patient.IsBaby) < 0)
            {
                this.Err = "���±����ʧ��!";
                return -1;
            }
            return 0;

        }
        #endregion

        #region ȡסԺ�ţ�סԺ��ˮ�����ɹ��򣬱��סԺ��,ȡ�������ڲ�����סԺ�źţ�����סԺ��״̬

        /// <summary>
        /// �Զ���ȡסԺ��
        /// </summary>
        /// <param name="patientNO">סԺ��</param>
        /// <param name="isRecycle">�Ƿ���պ���</param>
        /// <returns></returns>
        public int GetAutoPatientNO(ref string patientNO, ref bool isRecycle)
        {
            //�Ӻ�����л�ȡסԺ��
            string usedPatientNO = string.Empty;
            isRecycle = GetNoUsedPatientNO(ref patientNO, ref usedPatientNO);
            if (!isRecycle)
            {
                //����������û�����ݣ����ȡ�µ���һ�����롣
                //��ȡ�µ�סԺ�ź�סԺ��ˮ��
                try
                {
                    string MaxPatientNo = string.Empty;
                    Int64 iPatientNo = 0;
                    MaxPatientNo = GetMaxPatientNO("0008");
                    iPatientNo = Int64.Parse(MaxPatientNo) + 1;
                    MaxPatientNo = iPatientNo.ToString();
                    MaxPatientNo = MaxPatientNo.PadLeft(10, '0');
                    patientNO = MaxPatientNo;
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    return -1;
                }
            }
            return 1;

        }

        /// <summary>
        /// ��ȡ��û���ù���סԺ��
        /// </summary>
        /// <param name="patientNO">δ��סԺ��</param>
        /// <param name="usedPatientNo">����סԺ��</param>
        /// <returns>true,ȡ����false û��δʹ�õ�סԺ��</returns>
        public bool GetNoUsedPatientNO(ref string patientNO, ref string usedPatientNo)
        {
            string strSql = string.Empty;
            bool rtn = false;
            try
            {
                if (Sql.GetCommonSql("RADT.InPatient.GetNoUsedPatientNo", ref strSql) == -1)
                {
                    return false;
                }

                if (ExecQuery(strSql) == -1)
                {
                    return false;
                }
                while (Reader.Read())
                {
                    rtn = true;
                    usedPatientNo = Reader[0].ToString();
                    patientNO = Reader[1].ToString();
                }
                this.Reader.Close();
                return rtn;
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                WriteErr();
                return false;
            }
        }

        /// <summary>
        /// ������סԺ��by CardNO
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public string GetMaxinPatientNOByPatientNO(string patientNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.GetMaxinPatientNOByPatientNO", ref strSql) == -1) return null;

            #region SQL

            #endregion
            try
            {
                strSql = string.Format(strSql, patientNO);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }
            return ExecSqlReturnOne(strSql, string.Empty);
        }

        /// <summary>
        /// �����Զ�����סԺ�ţ�סԺ��ˮ��
        /// </summary>
        /// <param name="pInfo">�����δ��סԺ�ű���ȡ�ã�pInfo.User03 = "UnUsed"</param>
        /// <returns></returns>
        public int AutoCreatePatientNO(PatientInfo pInfo)
        {
            string patientNO = string.Empty;
            string usedPatientNO = string.Empty;
            if (GetNoUsedPatientNO(ref patientNO, ref usedPatientNO))
            {
                //����Ǵ�δ��סԺ�ű���ȡ�����ݣ���ʾ����
                pInfo.User03 = "UNUSED";
                return AutoCreatePatientNO(patientNO, usedPatientNO, ref pInfo);
            }
            else
            {
                return AutoCreatePatientNO(string.Empty, ref pInfo);
            }
        }

        /// <summary>
        /// ����סԺ������סԺ��ˮ��--���ڶ����Ժ,������סԺ��
        /// </summary>
        /// <param name="patientNO">����סԺ��</param>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        public int AutoCreatePatientNO(string patientNO, ref PatientInfo pInfo)
        {
            return AutoCreatePatientNO(patientNO, patientNO, ref pInfo);
        }
        /// <summary>
        /// ����סԺ��ȡ��һ�����￨��
        /// </summary>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        public int GetCardNOByPatientNO(string patientNO, ref string CardNo)
        {
            string Sqlstr = string.Empty;
            try
            {
                if (this.Sql.GetCommonSql("RADT.InPatient.GetCardNOByPatientNO", ref Sqlstr) < 0)
                    return -1;
                Sqlstr = string.Format(Sqlstr, patientNO);
                CardNo = this.ExecSqlReturnOne(Sqlstr);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// ����סԺ������סԺ��ˮ��
        /// </summary>
        /// <param name="patientNO">����סԺ��</param>
        /// <param name="usedPatientNO">ԭ���û�����סԺ�ŵĻ��ߵĵ�ǰסԺ��</param>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        public int AutoCreatePatientNO(string patientNO, string usedPatientNO, ref PatientInfo pInfo)
        {
            if (pInfo == null)
            {
                pInfo = new PatientInfo();
            }
            if (patientNO == string.Empty)
            {
                //��ȡ�µ�סԺ�ź�סԺ��ˮ��
                string MaxPatientNo = string.Empty;
                Int64 iPatientNo = 0;
                MaxPatientNo = GetMaxPatientNO("0008");
                try
                {
                    iPatientNo = Int64.Parse(MaxPatientNo) + 1;
                    MaxPatientNo = iPatientNo.ToString();
                    MaxPatientNo = MaxPatientNo.PadLeft(10, '0');
                    pInfo.PID.PatientNO = MaxPatientNo;
                    if (pInfo.ID == string.Empty)
                    {
                        pInfo.ID = this.GetNewInpatientNO();
                    }
                    pInfo.PID.CardNO = "T" + MaxPatientNo.Substring(1, 9);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    return -1;
                }
            }
            else
            {
                pInfo.PID.PatientNO = patientNO;
                string CardNo = string.Empty;
                //����סԺ��ȡ��һ�����￨�ţ���ԤԼ�Ǽ���ѡȡ�������￨���п��ܲ����Զ����ɵģ�
                if (this.GetCardNOByPatientNO(patientNO, ref CardNo) < 0)
                    return -1;
                if (CardNo != "-1")
                {
                    pInfo.PID.CardNO = CardNo;
                }
                else
                {
                    pInfo.PID.CardNO = "T" + patientNO.Substring(1, 9);
                }
                if (pInfo.ID == string.Empty)
                {
                    string InPatientNo = GetMaxPatientNOByCardNO(pInfo.PID.CardNO);
                    if (InPatientNo != string.Empty)
                    {
                        pInfo = this.QueryPatientInfoByInpatientNO(InPatientNo);
                        pInfo.PatientNOType = EnumPatientNOType.Second;
                        pInfo.User03 = "SECOND";
                    }
                    else
                    {
                        pInfo.PatientNOType = EnumPatientNOType.First;
                    }
                    //����סԺ��ȡ�µ���ˮ��
                    pInfo.ID = this.GetNewInpatientNO();
                }
            }
            return 1;
        }
        [Obsolete("����Ϊ AutoCreatePatientNO", true)]
        public int AutoPatientNo(string PatientNO, string UsedPatientNo, PatientInfo pInfo)
        { return 0; }
        /// <summary>
        /// ����סԺ�ű����¼
        /// </summary>
        /// <param name="NewPatientNO">��סԺ��</param>
        /// <param name="OldPatientNo">ԭסԺ��</param>
        /// <returns>1�ɹ� ��������ʧ��</returns>
        public int SetPatientNOShift(string NewPatientNO, string OldPatientNo)
        {
            //������סԺ�� ������
            if (!FS.FrameWork.Public.String.IsNumeric(OldPatientNo))
            {
                return 1;
            }

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.SetPatientNoShift", ref strSql) == -1)
            {
                return -1;
            }
            //�жϲ��� ������³ɹ�����ڲ���
            if (UpdatePatientNoState(OldPatientNo) == 1)
            {
                Err = "�Ѿ�����δʹ�õ�סԺ��" + OldPatientNo + ",���ܴ��ڲ���";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                                       NewPatientNO, //0 �����µ�סԺ��
                                       OldPatientNo, //1 ���߾ɵ�סԺ��
                                       Operator.ID //2 ��������ݱ���
                    );
            }
            catch
            {
                Err = "�����������RADT.InPatient.SetPatientNoShift!";
                WriteErr();
                return -1;
            }

            int parm = ExecNoQuery(strSql);
            return parm;
        }
        [Obsolete("����Ϊ SetPatientNOShift", true)]
        public int SetPatientNoShift(string NewPatientNO, string OldPatientNo)
        {
            return 0;
        }

        [System.Obsolete("����Ϊ GetNoUsedPatientNO ", true)]
        public bool GetNoUsedPatientNo(ref string PatientNO, ref string UsedPatientNo)
        {
            return true;
        }
        /// <summary>
        /// ����δʹ�õ�סԺ��Ϊʹ��״̬
        /// </summary>
        /// <param name="OldPatienNo">�ɵ�סԺ�ţ�δʹ�õ�</param>
        /// <returns>1�ɹ���0,����������Ӧ�����»�ȡסԺ�ţ�-1 ʧ��</returns>
        public int UpdatePatientNoState(string OldPatienNo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdatePateintNoState", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql,
                                       OldPatienNo, //0 ���߾ɵ�סԺ��
                                       Operator.ID //1 ����Ա
                    );
            }
            catch
            {
                Err = "�����������RADT.InPatient.UpdatePateintNoState!";
                WriteErr();
                return -1;
            }

            int parm = ExecNoQuery(strSql);
            if (parm == 0)
            {
                Err = "��סԺ���ѱ�ʹ�á�";
            }
            return parm;
        }

        /// <summary>
        /// ����סԺ��
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="oldCardNo">ԭ���￨��</param>
        /// <param name="newPatientNO">��סԺ��</param>
        /// <returns></returns>
        public int UpdatePatientNO(string inpatientNO, string oldCardNo, string newPatientNO)
        {
            string NewCardNo = null;
            if (oldCardNo.StartsWith("T"))
            {
                NewCardNo = "T" + newPatientNO.Substring(1, 9);
            }
            else
            {
                if (FS.FrameWork.Public.String.IsNumeric(newPatientNO))
                {
                    NewCardNo = oldCardNo;
                }
                else
                {
                    NewCardNo = newPatientNO;
                }
            }

            return UpdatePatientNO(inpatientNO, oldCardNo, newPatientNO, NewCardNo);
        }

        public int UpdatePatientNO(string inpatientNO, string oldCardNo, string newPatientNO, string newCardNo)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdatePatientNo", ref strSql1) == -1)
            {
                return -1;
            }
            if (Sql.GetCommonSql("RADT.InPatient.UpdateCardNo", ref strSql2) == -1)
            {
                return -1;
            }
            try
            {
                strSql1 = string.Format(strSql1, inpatientNO, newPatientNO, newCardNo);
                strSql2 = string.Format(strSql2, oldCardNo, newCardNo);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            if (ExecNoQuery(strSql1) != 1)
            {
                return -1;
            }
            if (ExecNoQuery(strSql2) != 1)
            {
                if (DBErrCode == 1)
                {
                    //�����ظ�,���������
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            return 1;
        }

        [Obsolete("����Ϊ UpdatePatientNO", true)]
        public int UpdatePatientNo(string inpatientNO, string oldCardNo, string newPatientNO)
        {
            return 0;
        }
        /// <summary>
        /// ����ҽ������סԺ��
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="newPatientNO">��סԺ��</param>
        /// <returns></returns>
        public int UpdateSIPatientNO(string inpatientNO, string newPatientNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdateSIPatientNo", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNO, newPatientNO);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        public string GetNewInpatientNO()
        {
            //�������Ի�ȡ�µ�סԺ��ˮ��
            return this.GetSequence("RADT.InPatient.NewInaptientNO");
        }

        #endregion

        #region ת������


        /// <summary>
        /// ����ת�Ʊ�
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="oldLocation">ԭ����</param>
        /// <param name="newLocation">�¿���</param>
        /// <param name="type"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        private int InsertShiftDept(string inpatientNO, Location oldLocation, Location newLocation, string type, string reason)
        {
            string strSQL = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.InsertShiftDept.1", ref strSQL) == -1)
            {
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL,
                                       inpatientNO, //0 סԺ��ˮ��
                                       oldLocation.Dept.ID, //1 ԭ������
                                       newLocation.Dept.ID, //2 ת������
                                       oldLocation.NurseCell.ID, //3 ��ʿվ����
                                       newLocation.NurseCell.ID, //4 ת������վ����
                                       type, //5 ��ǰ״̬,1ת������,2ȷ��,3ȡ������
                                       Operator.ID, //6 ����Ա
                                       reason, //7 ��ע
                                       oldLocation.Bed.ID,//8 ԭ������
                                       newLocation.User01); //�Ƿ����Ӥ��
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.InsertShiftDept.1";
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSQL);
        }
        [System.Obsolete("δ��", true)]
        private int myInsertShiftDept(string InpatientNo, Location oldLocation, Location newLocation, string type, string reason)
        {
            return 0;
        }
        /// <summary>
        /// ����ת��ȷ���·���Ĳ�����ȷ����.
        /// </summary>
        /// <param name="inpatientNo">����סԺ��ˮ��</param>
        /// <param name="location">�·���Ĵ�λ��Ϣ</param>
        /// <returns>-1 ʧ��, 0 ����ʧ��(û���ҵ���¼), rows ���µ�����</returns>
        public int UpdateShiftApply(string inpatientNo, Location location)
        {
            #region "�ӿ�˵��"

            //����ת�Ʊ�
            //RADT.InPatient.UpdateShiftDept.1
            //���룺0 סԺ��ˮ��,1�·���Ĵ���
            //������0

            #endregion

            string strSQL = string.Empty;
            int happenNo = 0;

            if (Sql.GetCommonSql("RADT.Inpatient.UpdateShiftApply.1", ref strSQL) == -1)
            {
                Err = "���SQL������!";
                WriteErr();
                return -1;
            }

            happenNo = CheckShiftInHappenNo(inpatientNo, location, "1");

            try
            {
                strSQL = string.Format(strSQL, inpatientNo, location.Bed.ID, Operator.ID, happenNo);
            }
            catch (Exception e)
            {
                Err = "����������ԣ�RADT.InPatient.UpdateShiftDept.1" + e.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���¿������루ȷ�Ϻ�ȡ����
        /// </summary>
        /// <param name="inPatientNo">������Ϣ</param>
        /// <param name="happenno">ת�����</param>
        /// <param name="type">1ת������,2ȷ��,3ȡ������</param>
        /// <param name="newLocation">��λ��Ϣ</param>
        /// <returns></returns>
        private int UpdateShiftDept(string inPatientNo, int happenno, string oldType, string type, Location newLocation)
        {
            #region "�ӿ�˵��"

            //����ת�Ʊ�
            //RADT.InPatient.UpdateShiftDept.1
            //���룺0 סԺ��ˮ��,1������� 2 ԭ����״̬ 3 ��״̬ 4 ȷ����ID 5 ȷ��ʱ�� 6 ȡ����ID 7 ȡ��ʱ�� 
            //������0

            #endregion

            string strSQL = string.Empty;
            string old_type = "1";
            old_type = oldType;
            //����ת��������Ϣ
            if (type == "2" || type == "1")
            {
                if (Sql.GetCommonSql("RADT.InPatient.UpdateShiftDept.1", ref strSQL) == -1)
                {
                    return -1;
                }

                try
                {
                    strSQL = string.Format(strSQL,
                                           inPatientNo, //0סԺ��ˮ��
                                           happenno, //1�������	
                                           old_type, //2����ǰ����״̬
                                           type, //3���º�����״̬
                                           Operator.ID, //4ȷ����
                                           newLocation.Dept.ID, //5ת����ұ���
                                           newLocation.NurseCell.ID, //6ת�뻤��վ����
                                           newLocation.Bed.ID,//7����
                                           newLocation.User01
                    );
                }
                catch
                {
                    Err = "����������ԣ�RADT.InPatient.UpdateShiftDept.1";
                    WriteErr();
                    return -1;
                }
            }
            //����ת������
            else if (type == "3")
            {
                if (Sql.GetCommonSql("RADT.InPatient.UpdateShiftDept.2", ref strSQL) == -1)
                {
                    return -1;
                }

                try
                {
                    strSQL = string.Format(strSQL,
                                           inPatientNo, //0סԺ��ˮ��
                                           happenno, //1�������	
                                           old_type, //2����ǰ����״̬
                                           type, //3���º�����״̬
                                           Operator.ID); //4ȡ���˱���
                }
                catch
                {
                    Err = "����������ԣ�RADT.InPatient.UpdateShiftDept.2";
                    WriteErr();
                    return -1;
                }
            }
            else
            {
                Err = "��Ч��ת��״̬";
                return -1;
            }

            return ExecNoQuery(strSQL);
        }
        [System.Obsolete("�ѹ���", true)]
        private int myUpdateShiftDept(string inPatientNo, int happenno, string oldType, string type, Location newLocation)
        {
            return 0;
        }
        /// <summary>
        ///  ����ת�������״̬
        /// </summary>
        /// <param name="inpatientNo"></param>// סԺ��ˮ��
        /// <param name="newState"></param>// ��״̬
        /// <param name="oldState"></param>// ԭ��״̬
        /// <returns></returns>
        public int UpdateApplyState(string inpatientNo, Location location, string newState, string oldState)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdateApplyState", ref strSql) == -1)
            {
                return -1;
            }
            int happenNo = CheckShiftOutHappenNo(inpatientNo, location, oldState);
            try
            {
                strSql = String.Format(strSql, inpatientNo, newState, oldState, location.Dept.ID, location.NurseCell.ID, happenNo);
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.UpdateShiftDept.2";
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #region ת��������

        /// <summary>
        /// ����ת�Ʊ�
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="oldLocation">ԭ����</param>
        /// <param name="newLocation">�¿���</param>
        /// <param name="type"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        private int InsertShiftNurseCell(string inpatientNO, Location oldLocation, Location newLocation, string type, string reason)
        {
            string strSQL = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.InsertShiftNurseCell.1", ref strSQL) == -1)
            {
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL,
                                       inpatientNO, //0 סԺ��ˮ��
                                       oldLocation.Dept.ID, //1 ԭ������
                                       newLocation.Dept.ID, //2 ת������
                                       oldLocation.NurseCell.ID, //3 ��ʿվ����
                                       newLocation.NurseCell.ID, //4 ת������վ����
                                       type, //5 ��ǰ״̬,1ת������,2ȷ��,3ȡ������
                                       Operator.ID, //6 ����Ա
                                       reason, //7 ��ע
                                       oldLocation.Bed.ID); //8 ԭ������
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.InsertShiftNurseCell.1";
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ����ת��ȷ���·���Ĳ�����ȷ����.
        /// </summary>
        /// <param name="inpatientNo">����סԺ��ˮ��</param>
        /// <param name="location">�·���Ĵ�λ��Ϣ</param>
        /// <returns>-1 ʧ��, 0 ����ʧ��(û���ҵ���¼), rows ���µ�����</returns>
        public int UpdateShiftNurseCell(string inpatientNo, Location location)
        {
            #region "�ӿ�˵��"

            //����ת�Ʊ�
            //RADT.InPatient.UpdateShiftDept.1
            //���룺0 סԺ��ˮ��,1�·���Ĵ���
            //������0

            #endregion

            string strSQL = string.Empty;
            int happenNo = 0;

            if (Sql.GetCommonSql("RADT.InPatient.UpdateShiftNurseCell.1", ref strSQL) == -1)
            {
                Err = "���SQL������!";
                WriteErr();
                return -1;
            }

            happenNo = CheckShiftInHappenNo(inpatientNo, location, "1");

            try
            {
                strSQL = string.Format(strSQL, inpatientNo, location.Bed.ID, Operator.ID, happenNo);
            }
            catch (Exception e)
            {
                Err = "����������ԣ�RADT.InPatient.UpdateShiftNurseCell.1" + e.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���¿������루ȷ�Ϻ�ȡ����
        /// </summary>
        /// <param name="inPatientNo">������Ϣ</param>
        /// <param name="happenno">ת�����</param>
        /// <param name="type">1ת������,2ȷ��,3ȡ������</param>
        /// <param name="newLocation">��λ��Ϣ</param>
        /// <returns></returns>
        private int UpdateShiftNurseCell(string inPatientNo, int happenno, string oldType, string type, Location newLocation)
        {
            #region "�ӿ�˵��"

            //����ת�Ʊ�
            //RADT.InPatient.UpdateShiftDept.1
            //���룺0 סԺ��ˮ��,1������� 2 ԭ����״̬ 3 ��״̬ 4 ȷ����ID 5 ȷ��ʱ�� 6 ȡ����ID 7 ȡ��ʱ�� 
            //������0

            #endregion

            string strSQL = string.Empty;
            string old_type = "1";
            old_type = oldType;
            //����ת��������Ϣ
            if (type == "2" || type == "1")
            {
                if (Sql.GetCommonSql("RADT.InPatient.UpdateShiftNurseCell.2", ref strSQL) == -1)
                {
                    return -1;
                }

                try
                {
                    strSQL = string.Format(strSQL,
                                           inPatientNo, //0סԺ��ˮ��
                                           happenno, //1�������	
                                           old_type, //2����ǰ����״̬
                                           type, //3���º�����״̬
                                           Operator.ID, //4ȷ����
                                           newLocation.Dept.ID, //5ת����ұ���
                                           newLocation.NurseCell.ID, //6ת�뻤��վ����
                                           newLocation.Bed.ID); //7����
                }
                catch
                {
                    Err = "����������ԣ�RADT.InPatient.UpdateShiftNurseCell.2";
                    WriteErr();
                    return -1;
                }
            }
            //����ת������
            else if (type == "3")
            {
                if (Sql.GetCommonSql("RADT.InPatient.UpdateShiftNurseCell.3", ref strSQL) == -1)
                {
                    return -1;
                }

                try
                {
                    strSQL = string.Format(strSQL,
                                           inPatientNo, //0סԺ��ˮ��
                                           happenno, //1�������	
                                           old_type, //2����ǰ����״̬
                                           type, //3���º�����״̬
                                           Operator.ID); //4ȡ���˱���
                }
                catch
                {
                    Err = "����������ԣ�RADT.InPatient.UpdateShiftNurseCell.3";
                    WriteErr();
                    return -1;
                }
            }
            else
            {
                Err = "��Ч��ת��״̬";
                return -1;
            }

            return ExecNoQuery(strSQL);
        }

        /// <summary>
        ///  ����ת�������״̬
        /// </summary>
        /// <param name="inpatientNo"></param>// סԺ��ˮ��
        /// <param name="newState"></param>// ��״̬
        /// <param name="oldState"></param>// ԭ��״̬
        /// <returns></returns>
        public int UpdateShiftNurseCellState(string inpatientNo, Location location, string newState, string oldState)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdateShiftNurseCellState.1", ref strSql) == -1)
            {
                return -1;
            }
            int happenNo = CheckShiftOutHappenNo(inpatientNo, location, oldState);
            try
            {
                strSql = String.Format(strSql, inpatientNo, newState, oldState, location.Dept.ID, location.NurseCell.ID, happenNo);
            }
            catch
            {
                Err = "����������ԣ�RADT.InPatient.UpdateShiftNurseCellState.1";
                return -1;
            }
            return ExecNoQuery(strSql);
        }

        #endregion

        #region "��ٹ���"

        /// <summary>
        /// ���������Ϣ�� ���룺0 InpatientNo,1 docid,2 days,3 remark 4 operator
        /// </summary>
        /// <param name="patientLeave">�����Ϣ</param>
        /// <param name="bed">��λ</param>
        /// <returns>���� 0 �ɹ� С�� 0 ʧ��</returns>
        public int InsertPatientLeave(Leave patientLeave, Bed bed)
        {
            string strSql = string.Empty;

            if (Sql.GetCommonSql("RADT.InPatient.InsertPatientLeave", ref strSql) == 0)
            {
                #region SQL
                /*			
					INSERT INTO MET_NUI_LEAVE (
					PARENT_CODE ,                           --����ҽ�ƻ�������
					CURRENT_CODE ,                          --����ҽ�ƻ�������
					INPATIENT_NO ,                          --סԺ��ˮ��
					LEAVE_DATE ,                            --���ʱ��
					LEAVE_DAYS ,                            --�������
					DOCT_CODE ,                             --����ҽ��
					OPER_CODE ,                             --��ٲ�����
					LEAVE_FLAG ,                            --0��� 1����,2����
					REMARK                                  --��ע
					)  VALUES(
						'[��������]',       --����ҽ�ƻ�������
						'[��������]',       --����ҽ�ƻ�������
						'{0}' ,       --סԺ��ˮ��
						SYSDATE ,     --���ʱ��
						{1} ,         --�������
						'{2}' ,       --����ҽ��
						'{3}' ,       --��ٲ�����
						'0' ,       --0��� 1����,����
						'{4}'         --��ע
					) 
			    */
                #endregion
                try
                {
                    strSql = string.Format(strSql,
                                           patientLeave.ID, //סԺ��ˮ��
                                           patientLeave.LeaveDays.ToString(), //�������
                                           patientLeave.DoctCode, //����ҽ��
                                           Operator.ID, //��ٲ�����
                                           patientLeave.Memo); //��ע
                }
                catch
                {
                    Err = "�����������RADT.InPatient.InsertPatientLeave!";
                    WriteErr();
                    return -1;
                }
            }
            if (ExecNoQuery(strSql) <= 0)
            {
                return -1;
            }

            //����ǰ�Ĵ�λ��Ϣ
            Bed newBed = bed.Clone();
            //���ĺ�Ĵ�λ��Ϣ:���R
            newBed.Status.ID = "R";
            //���´�λ״̬
            return UpdateBedStatus(newBed, bed);
        }

        /// <summary>
        /// ���������Ϣ����٣�
        /// </summary>
        /// <param name="patientLeave"></param>
        /// <returns></returns>
        private int UpdatePatientLeave(Leave patientLeave)
        {
            string strSql = string.Empty;

            if (Sql.GetCommonSql("RADT.InPatient.UpdatePatientLeave", ref strSql) == 0)
            {
                #region SQL
                /*UPDATE	MET_NUI_LEAVE 
					SET	LEAVE_DAYS = {2} ,                      --�������
					DOCT_CODE = '{3}' ,                     --����ҽ��
					OPER_CODE = '{4}' ,                   	--��ٲ�����
					LEAVE_FLAG = '0' ,                    	--0��� 1����,2����
					REMARK = '{5}'                          --��ע
					WHERE 	PARENT_CODE = '[��������]' 
					AND  	CURRENT_CODE = '[��������]' 
					AND 	INPATIENT_NO = '{0}'  
					AND 	LEAVE_DATE = to_date('{1}','yyyy-mm-dd HH24:mi:ss')  
				*/
                #endregion
                try
                {
                    strSql = string.Format(strSql,
                                           patientLeave.ID, //0סԺ��ˮ��
                                           patientLeave.LeaveTime, //1�������
                                           patientLeave.LeaveDays.ToString(), //2�������
                                           patientLeave.DoctCode, //3����ҽ��
                                           Operator.ID, //4��ٲ�����
                                           patientLeave.Memo); //5��ע
                }
                catch
                {
                    Err = "�����������RADT.InPatient.UpdatePatientLeave!";
                    WriteErr();
                    return -1;
                }
            }
            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���������Ϣ������ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
        /// </summary>
        /// <param name="patientLeave"></param>
        /// <param name="bed"></param>
        /// <returns>0δ���£�����1�ɹ���-1ʧ��</returns>
        public int SetPatientLeave(Leave patientLeave, Bed bed)
        {
            int parm;
            //ִ�и��²���
            parm = UpdatePatientLeave(patientLeave);

            //���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            if (parm == 0)
            {
                parm = InsertPatientLeave(patientLeave, bed);
            }

            return parm;
        }


        /// <summary>
        /// ȡ�����������Ϣ
        /// </summary>
        /// <param name="patientLeave"></param>
        /// <param name="bed"></param>
        /// <returns>0û�и���, 1�ɹ�, -1δ�ɹ�</returns>
        public int DiscardPatientLeave(Leave patientLeave, Bed bed)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.CancelPatientLeave", ref strSql) == -1)
            #region SQL
            /*  UPDATE 	MET_NUI_LEAVE   	--�����Ϣ��
					SET 	LEAVE_FLAG   = '{2}',   --0��� 1����,2����
			   		CANCEL_OPCD  = '{3}',	--������(���ϲ�����)
			   		CANCEL_DATE  = sysdate	--����ʱ��(����ʱ��)
					WHERE 	PARENT_CODE  = '[��������]' 
					AND 	CURRENT_CODE = '[��������]'
					AND 	INPATIENT_NO = '{0}' 
					AND 	LEAVE_DATE = to_date('{1}','yyyy-mm-dd HH24:mi:ss') 
				*/
            #endregion
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql,
                                       patientLeave.ID, //סԺ��ˮ��
                                       patientLeave.LeaveTime.ToString(), //�������
                                       patientLeave.LeaveFlag, //0��� 1����,2����
                                       Operator.ID); //����(����)������
            }
            catch
            {
                Err = "�����������RADT.InPatient.CancelPatientLeave!";
                WriteErr();
                return -1;
            }

            int parm = ExecNoQuery(strSql);
            if (parm != 1) return parm;

            //����ǰ�Ĵ�λ��Ϣ
            Bed newBed = bed.Clone();
            //���ĺ�Ĵ�λ��Ϣ:ռ��O
            newBed.Status.ID = EnumBedStatus.O.ToString();
            //���´�λ״̬
            return UpdateBedStatus(newBed, bed);
        }
        [Obsolete("����Ϊ DiscardPatientLeave", true)]
        public int CancelPatientLeave(Leave patientLeave, Bed bed)
        {
            return 0;
        }
        /// <summary>
        /// ͨ��סԺ��ˮ�������������Ϣ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns>���������Ϣ</returns>
        public ArrayList QueryPatientLeaveInfo(string inpatientNO)
        {
            string strSql = string.Empty;
            ArrayList al = new ArrayList();

            //ȡselect���
            if (Sql.GetCommonSql("RADT.InPatient.GetPatientLeave", ref strSql) == -1)
            {
                #region SQL
                /* 
				SELECT	INPATIENT_NO,                   --סԺ��ˮ��
				LEAVE_DATE,                             --���ʱ��
				LEAVE_DAYS,                             --�������
				DOCT_CODE,                              --����ҽ��
				OPER_CODE,                              --��ٲ�����
				LEAVE_FLAG,                             --0��� 1����,2����
				REMARK,                                 --��ע
				CANCEL_OPCD,                            --������(���ϲ�����)
				CANCEL_DATE                             --����ʱ��(����ʱ��)
				FROM 	MET_NUI_LEAVE 
				WHERE	PARENT_CODE  = '[��������]' 
				AND  	CURRENT_CODE = '[��������]' 
				AND 	INPATIENT_NO = '{0}' */
                #endregion
            }

            try
            {
                strSql = string.Format(strSql, inpatientNO);
            }
            catch
            {
                Err = "�����������GetPatientLeaveAll!";
                WriteErr();
                return null;
            }

            //ִ��SQL���
            return QueryPatientLeave(strSql);
        }
        [System.Obsolete("����Ϊ QueryPatientLeaveInfo", true)]
        public ArrayList GetPatientLeaveAll(string InpatientNo)
        {
            return null;
        }

        /// <summary>
        /// �������������Ϣ
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <returns>סԺ����һ����Ч�������Ϣ</returns>
        public ArrayList GetPatientLeaveAvailable(string inpatientNo)
        {
            string strSql = string.Empty;
            string strWhere = string.Empty;
            ArrayList al = new ArrayList();

            //ȡselect���
            if (Sql.GetCommonSql("RADT.InPatient.GetPatientLeave", ref strSql) == -1)
            #region SQL
            /* 
				SELECT	INPATIENT_NO,                   --סԺ��ˮ��
				LEAVE_DATE,                             --���ʱ��
				LEAVE_DAYS,                             --�������
				DOCT_CODE,                              --����ҽ��
				OPER_CODE,                              --��ٲ�����
				LEAVE_FLAG,                             --0��� 1����,2����
				REMARK,                                 --��ע
				CANCEL_OPCD,                            --������(���ϲ�����)
				CANCEL_DATE                             --����ʱ��(����ʱ��)
				FROM 	MET_NUI_LEAVE 
				WHERE	PARENT_CODE  = '[��������]' 
				AND  	CURRENT_CODE = '[��������]' 
				AND 	INPATIENT_NO = '{0}' */
            #endregion
            {
                return null;
            }

            //ȡwhere���
            if (Sql.GetCommonSql("RADT.InPatient.GetPatientLeave.Available", ref strWhere) == -1)
            #region SQL
            /*	AND	LEAVE_FLAG <> '2' */
            #endregion
            {
                return null;
            }

            try
            {
                strSql = string.Format(strSql + " " + strWhere, inpatientNo);
            }
            catch
            {
                Err = "�����������GetPatientLeaveAvailable!";
                WriteErr();
                return null;
            }

            //ִ��SQL���
            return QueryPatientLeave(strSql);
        }


        /// <summary>
        /// ȡ���������Ϣ
        /// </summary>
        /// <param name="strSQL">ȡ��ٵ�SQL���</param>
        /// <returns>�����Ϣ����</returns>
        private ArrayList QueryPatientLeave(string strSQL)
        {
            if (ExecQuery(strSQL) == -1)
            {
                return null;
            }
            ArrayList al = new ArrayList();
            try
            {
                Leave info; //���ʵ��	
                while (Reader.Read())
                {
                    info = new Leave();
                    info.ID = Reader[0].ToString(); //0סԺ��ˮ��
                    info.LeaveTime = NConvert.ToDateTime(Reader[1].ToString()); //1���ʱ��
                    info.LeaveDays = NConvert.ToInt32(Reader[2].ToString()); //2�������
                    info.DoctCode = Reader[3].ToString(); //3����ҽ��
                    info.Oper.ID = Reader[4].ToString(); //4��ٲ�����
                    info.LeaveFlag = Reader[5].ToString(); //5���״̬0��� 1����,2����
                    info.Memo = Reader[6].ToString(); //6��ע
                    info.CancelOper.ID = Reader[7].ToString(); //7������(���ϲ�����)
                    info.CancelOper.OperTime = NConvert.ToDateTime(Reader[8].ToString()); //8����ʱ��(����ʱ��)
                    al.Add(info);
                }
                Reader.Close();
            } //�׳�����
            catch (Exception ex)
            {
                Err = "ȡ���������Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }

            return al;
        }

        #endregion

        #region ��û�����

        /// <summary>
        /// ��û�����
        /// </summary>
        /// <param name="inpatientNO">����סԺ��</param>
        /// <returns></returns>
        public string GetTendName(string inpatientNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.GetTendName", ref strSql) == -1)
            {
                return "";
            }
            #region SQL
            /* SELECT tend                --������(TEND):������ʾ����������(һ����������������������)
				FROM fin_ipr_inmaininfo   --סԺ����
				WHERE PARENT_CODE='[��������]'	and 	CURRENT_CODE='[��������]' and inpatient_no ='{0}'*/
            #endregion
            try
            {
                strSql = string.Format(strSql, inpatientNO);
            }
            catch
            {
                Err = "�����������RADT.InPatient.GetTendName";
                WriteErr();
                return string.Empty;
            }
            return ExecSqlReturnOne(strSql);
        }

        #endregion

        #region �����ʳ

        /// <summary>
        /// ��û�����ʳ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns>���ػ�����ʳ����</returns>
        public string GetFoodName(string inpatientNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.GetFoodName", ref strSql) == -1)
            {
                return string.Empty;
            }

            #region SQL
            /*SELECT DIETETIC_MARK
				FROM fin_ipr_inmaininfo   --סԺ����
				WHERE PARENT_CODE='[��������]'	and CURRENT_CODE='[��������]' and inpatient_no ='{0}'
	        */
            #endregion

            try
            {
                strSql = string.Format(strSql, inpatientNO);
            }
            catch
            {
                Err = "�����������RADT.InPatient.GetFoodName";
                WriteErr();
                return string.Empty;
            }
            return ExecSqlReturnOne(strSql);
        }

        #endregion

        #region ������ʳ

        /// <summary>
        /// ������ʳ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="food"></param>
        /// <returns></returns>
        public int UpdatePatientFood(string inpatientNO, string food)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.SetFoodName", ref strSql) == -1) return -1;
            #region SQL
            /*UPDATE fin_ipr_inmaininfo   --סԺ����
				SET DIETETIC_MARK='{1}'
				WHERE  PARENT_CODE='[��������]'	and 	CURRENT_CODE='[��������]' and inpatient_no ='{0}'*/
            #endregion
            try
            {
                strSql = string.Format(strSql, inpatientNO, food);
            }
            catch
            {
                Err = "�����������RADT.InPatient.SetFoodName";
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }
        [Obsolete("����Ϊ UpdatePatientFood", true)]
        public int SetFood(string inpatientNO, string food)
        {
            return 0;
        }
        #endregion

        #region ���»���

        /// <summary>
        /// ����סԺ���߻�����
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="tend">������</param>
        /// <returns>���� 0 �ɹ� С�� 0 ʧ��</returns>
        public int UpdatePatientTend(string inpatientNO, string tend)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.SetTend", ref strSql) == -1) return -1;
            #region SQL
            /* UPDATE fin_ipr_inmaininfo	 --סԺ����
				SET tend='{1}'				 --������(TEND):������ʾ����������(һ����������������������)
				WHERE  PARENT_CODE='[��������]'	and 	CURRENT_CODE='[��������]' and inpatient_no ='{0}'
			*/
            #endregion
            try
            {
                strSql = string.Format(strSql, inpatientNO, tend);
            }
            catch
            {
                Err = "�����������RADT.InPatient.SetTend";
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }
        [Obsolete("����Ϊ UpdatePatientTend", true)]
        public int SetTend(string inpatientNO, string tend)
        {
            return 0;
        }
        #endregion

        #region ��ȡ����

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public string GetCardNOSequece()
        {
            string str = GetSequence("Exami.ChkPatient.GetCHKCardNoSequence");
            #region SQL
            /* SELECT SEQ_COM_CARDNO.NEXTVAL FROM DUAL */
            #endregion
            str = str.PadLeft(10, '0');
            return str;
        }
        [Obsolete("����Ϊ GetCardNOSequece", true)]
        public string GetCardNoSequence()
        {
            return null;
        }
        #endregion

        #region ��ѯ����

        #region ��סԺ��ˮ�Ų�ѯ���߻�����Ϣ

        /// <summary>
        /// ���߲�ѯ-��סԺ�Ų�
        /// </summary>
        /// <param name="inPatientNO">����סԺ��ˮ��</param>
        /// <returns>���ػ�����Ϣ</returns>
        public PatientInfo GetPatientInfoByPatientNO(string inPatientNO)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            strSql1 = PatientQueryBasicSelect();

            if (strSql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.7", ref strSql2) == -1)
            {
                return null;
            }
            #region SQL
            /*	where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and Inpatient_no='{0}'*/
            #endregion
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, inPatientNO);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientQuery.Where.7��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientQuery.Where.7��ֵ��ƥ�䣡";
                return null;
            }
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo = new PatientInfo();
            ;
            try
            {
                al = myPatientBasicQuery(strSql1);
                if (al.Count == 0) return PatientInfo;
                PatientInfo = (PatientInfo)al[0];
            }
            catch (Exception ee)
            {
                string Error = ee.Message;
                return null;
            }
            return PatientInfo;
        }
        [Obsolete("����Ϊ GetPatientInfoByPatientNO", true)]
        public PatientInfo PatientQueryBasic(string inPatientNo)
        {
            return null;
        }
        #region ��ѯһ��ʱ����ڵ����г�Ժ���㻼��

        /// <summary>
        /// ��ѯһ��ʱ����ڵ����г�Ժ���㻼��
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatientInfoByTimeInState(DateTime dtBegin, DateTime dtEnd, string inState)
        {
            ArrayList al = new ArrayList();

            string sql1 = string.Empty;
            string sql2 = string.Empty;

            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.PatientQueryBalance", ref sql2) == -1)
            {
                #region SQL
                /*  where in_state = 'O'
					and parent_code = '[��������]'
					and current_code = '[��������]'
					and balance_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
					and balance_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
					and paykind_code = '{2}'
					and pact_code <> '4' 
				 */
                #endregion
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, dtBegin.ToString(), dtEnd.ToString(), inState);

            return myPatientQuery(sql1);
        }
        [Obsolete("����Ϊ QueryPatientInfoByTimeInState", true)]
        public ArrayList PatientQuery(DateTime dtBegin, DateTime dtEnd, string inState)
        {
            return null;
        }
        #endregion

        #endregion

        #region ��סԺ�Ų�ѯ������Ϣ
        /// <summary>
        /// ��סԺ�Ų�
        /// </summary>
        /// <param name="patientNO">����סԺ��</param>
        /// <returns>���ػ���List��Ϣ</returns>
        public ArrayList GetPatientInfoByPatientNo(string patientNO)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.Where.40
            //���룺סԺ��
            //������������Ϣ
            //SQL���:    where patient_no='{0}'

            #endregion

            strSql1 = PatientQuerySelect();
            if (strSql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.40", ref strSql2) == -1)
            {
                return null;
            }
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, patientNO);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientQuery.Where.40��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientQuery.Where.40��ֵ��ƥ�䣡";
                return null;
            }
            ArrayList arrayList = new ArrayList();
            try
            {
                arrayList = myPatientQuery(strSql1);
                if (arrayList.Count < 1)
                {
                    return null;
                }
            }
            catch (Exception ee)
            {
                Err = "û���ҵ�������Ϣ!" + ee.Message;
                WriteErr();
                return null;
            }
            return arrayList;
        }
        #endregion

        #region ��סԺ״̬��ѯ���߻�����Ϣ

        /// <summary>
        /// ���߲�ѯ-��סԺ״̬��ѯ���߻�����Ϣ
        /// </summary>
        /// <param name="inState">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList QueryPatientBasicByInState(InStateEnumService inState)
        {

            ArrayList al = new ArrayList();

            string sql1 = string.Empty;
            string sql2 = string.Empty;

            sql1 = PatientQueryBasicSelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.9", ref sql2) == -1)
            #region SQL
            /*  where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and In_state='{0}'  */
            #endregion
            {
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, inState.ID.ToString());
            return myPatientBasicQuery(sql1);
        }

        [Obsolete("����Ϊ QueryPatientInfoByInState ", true)]
        public ArrayList PatientQueryBasic(InStateEnumService State)
        {
            return null;
        }

        #endregion

        #region ��סԺ��ˮ�Ų�ѯ������Ϣ

        //���߲�ѯ
        /// <summary>
        /// ��ѯסԺ������Ϣ-��סԺ��ˮ�Ų�
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns>������Ϣ PatientInfo</returns>
        public PatientInfo QueryPatientInfoByInpatientNO(string inPatientNO)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            #region �ӿ�˵��

            /////RADT.Inpatient.PatientQuery.where.7
            //����:סԺ��ˮ��
            //������������Ϣ

            #endregion

            strSql1 = PatientQuerySelect();
            if (strSql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.7", ref strSql2) == -1)
            {
                return null;
            }
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, inPatientNO);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientQuery.Where.7��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientQuery.Where.7��ֵ��ƥ�䣡";
                return null;
            }
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            try
            {
                al = myPatientQuery(strSql1);
                if (al.Count < 1)
                {
                    return null;
                }
                PatientInfo = (PatientInfo)al[0];
            }
            catch (Exception ee)
            {
                Err = "û���ҵ�������Ϣ!" + ee.Message;
                WriteErr();
                return null;
            }
            return PatientInfo;
        }

        //���߲�ѯ add by zhy ���ڴ������ն˷���
        /// <summary>
        /// ��ѯסԺ������Ϣ-��סԺ��ˮ�Ų�
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns>δȷ�Ϸ���</returns>
        public decimal QueryPatientTerminalFeeByInpatientNO(string inPatientNO)
        {
            string strSql1 = string.Empty;
            decimal terminalcost = 0;

            strSql1 = PatientTerminalFeeQuerySelect();
            if (strSql1 == null)
            {
                return 0;
            }


            try
            {
                strSql1 = string.Format(strSql1, inPatientNO);
            }
            catch
            {
                Err = "��ֵ��ƥ�䣡";
                ErrCode = "��ֵ��ƥ�䣡";
                return 0;
            }


            try
            {
                terminalcost = myPatientTerminalFeeQuery(strSql1);

            }
            catch (Exception ee)
            {
                Err = "û���ҵ�������Ϣ!" + ee.Message;
                WriteErr();
                return 0;
            }

            return terminalcost;
        }

        public PatientInfo QueryPatientInfoByInpatientNONew(string inPatientNO)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            #region �ӿ�˵��

            /////RADT.Inpatient.PatientQuery.where.7
            //����:סԺ��ˮ��
            //������������Ϣ

            #endregion

            strSql1 = PatientQuerySelectNew();
            if (strSql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.7", ref strSql2) == -1)
            {
                return null;
            }
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, inPatientNO);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientQuery.Where.7��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientQuery.Where.7��ֵ��ƥ�䣡";
                return null;
            }
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            try
            {
                al = myPatientQueryNew(strSql1);
                if (al.Count < 1)
                {
                    return null;
                }
                PatientInfo = (PatientInfo)al[0];
            }
            catch (Exception ee)
            {
                Err = "û���ҵ�������Ϣ!" + ee.Message;
                WriteErr();
                return null;
            }
            return PatientInfo;
        }

        [Obsolete("����Ϊ QueryPatientInfoByInpatientNO", true)]
        public PatientInfo PatientQuery(string inPatientNO)
        {
            return null;
        }
        #endregion

        #region ��ȡӤ��ĸ��סԺ��ˮ��
        /// <summary>
        /// ��ȡӤ��ĸ��סԺ��ˮ��
        /// </summary>
        /// <param name="babyInpatientNO"></param>
        /// <param name="motherInpationNo"></param>
        /// <returns></returns>
        [Obsolete("���з���QueryBabyMotherInpatientNO", true)]
        public int QueryMotherInPatientNOByBabyID(string babyInpatientNO, out string motherInpationNo)
        {
            motherInpationNo = null;
            string strSQL = @"";
            if (this.Sql.GetCommonSql("RADT.Inpatient.QueryInpatientNoByBabyID", ref strSQL) == -1)
            {
                this.Err = "û�ҵ� RADT.Inpatient.QueryInpatientNoByBabyID SQL ��䣡";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, babyInpatientNO);

                if (ExecQuery(strSQL) == -1)
                {
                    return -1;
                }
                while (this.Reader.Read())
                {
                    motherInpationNo = this.Reader[0].ToString().Trim();
                }
                this.Reader.Close();

            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                this.ErrCode = objEx.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region ����λ�ŵõ�������Ϣ

        /// <summary>
        /// ���סԺ��ˮ�ŴӲ�����
        /// </summary>
        /// <param name="BedNo">����</param>
        /// <returns>��Ժ������Ϣ</returns>
        public ArrayList QueryInpatientNOByBedNO(string BedNo)
        {
            return this.QueryInpatientNOByBedNO(BedNo, "ALL");
        }

        /// <summary>
        /// ���סԺ��ˮ�ŴӲ�����
        /// </summary>
        /// <param name="BedNo">����</param>
        /// <returns>��Ժ������Ϣ</returns>
        public ArrayList QueryInpatientNOByBedNO(string BedNo, string inState)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.QeryInpatientNoFromPatientNo.2", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, BedNo, inState);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    return null;
                }
                return GetPatientInfoBySQL(strSql);
            }
            else
            {
                return null;
            }
        }

        [Obsolete("����Ϊ QueryInpatientNOByBedNO", true)]
        public ArrayList QueryInpatientNoFromBedNo(string BedNo)
        {
            return null;
        }
        #endregion

        #region ����ͬ��λ,����,��Ժ״̬��ѯ������Ϣ

        /// <summary>
        /// ���ݹ��������õ�����סԺ��ˮ��
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <param name="deptCode">���ű���</param>
        /// <param name="status">����״̬</param>
        /// <param name="beginDate">��ʼ����</param>
        /// <param name="endDate">��������</param>
        /// <returns>���������Ļ�����Ϣ�б�סԺ�ţ�������</returns>
        [Obsolete("����", true)]
        public ArrayList QueryPatienByConditons(string pactCode, string deptCode, string status, DateTime beginDate,
                                                 DateTime endDate)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.QueryPatientByConditons.Select.1", ref strSql) == -1)
            #region SQL
            /*
				    select inpatient_no,name
					from fin_ipr_inmaininfo
					where 
					pact_code like '{0}'
					and dept_code like '{1}'
					and in_state like '{2}'
					and in_date >= trunc(to_date('{3}','yyyy-mm-dd hh24:mi:ss'))
					and in_date <= to_date('{4}','yyyy-mm-dd hh24:mi:ss')
				*/
            #endregion
            {
                return null;
            }
            try
            {
                strSql = string.Format(strSql, pactCode, deptCode, status, beginDate, endDate);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = "-1";
                WriteErr();
                return null;
            }
            try
            {
                ArrayList al = new ArrayList();
                ExecQuery(strSql);
                while (Reader.Read())
                {
                    NeuObject obj = new NeuObject();
                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
        }

        #endregion

        #region ����ͬ��λ,����,��Ժ״̬��ѯ������ϸ��Ϣ�б�

        /// <summary>
        /// ���ݹ��������õ�����סԺ��ϸ��Ϣ--Edit By Maokb
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="status"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByConditons(string pactCode, string deptCode, string status, DateTime beginDate,
                                              DateTime endDate)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;
            ArrayList al = new ArrayList();

            strSql1 = PatientQuerySelect();
            if (strSql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.12", ref strSql2) == -1)
            {
                return null;
            }
            try
            #region SQL
            /*
				 * where PARENT_CODE='[��������]' 
				 * and CURRENT_CODE='[��������]' 
				 * and NURSE_CELL_CODE like '{0}' 
				 * and  pact_code like '{1}'
				 * and (TRUNC(in_date) >=to_date('{2}','yyyy-mm-dd HH24:mi:ss')) 
				 * and (TRUNC(in_date) <=to_date('{3}','yyyy-mm-dd HH24:mi:ss')) 
				 * and In_state like '{4}'
				 */
            #endregion
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, deptCode, pactCode, beginDate, endDate, status);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientQuery.Where.12��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientQuery.Where.12��ֵ��ƥ�䣡";
                return null;
            }

            return myPatientQuery(strSql1);
        }

        /// <summary>
        /// ���ݹ��������õ�����סԺ��ϸ��Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="status"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="doctCode"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByConditons(string pactCode, string deptCode, string status, DateTime beginDate,
                                              DateTime endDate, string doctCode)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;
            ArrayList al = new ArrayList();

            strSql1 = PatientQuerySelect();
            if (strSql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.12", ref strSql2) == -1)
            {
                return null;
            }
            try
            #region SQL
            /*
				 * where PARENT_CODE='[��������]' 
				 * and CURRENT_CODE='[��������]' 
				 * and NURSE_CELL_CODE like '{0}' 
				 * and  pact_code like '{1}'
				 * and (TRUNC(in_date) >=to_date('{2}','yyyy-mm-dd HH24:mi:ss')) 
				 * and (TRUNC(in_date) <=to_date('{3}','yyyy-mm-dd HH24:mi:ss')) 
				 * and In_state like '{4}'
				 */
            #endregion
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, deptCode, pactCode, beginDate, endDate, status, doctCode);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientQuery.Where.12��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientQuery.Where.12��ֵ��ƥ�䣡";
                return null;
            }

            return myPatientQuery(strSql1);
        }

        #endregion

        #region ��סԺ��ˮ�Ų�ѯ���߽�����Ϣ

        /// <summary>
        /// ��ѯ���߽�����Ϣ
        /// </summary>
        /// <param name="inPatientNO">סԺ��ˮ��</param>
        /// <returns>���ؽ��ﲡ����Ϣ</returns>
        public ArrayList QueryPatientReceiveInfo(string inPatientNO)
        {
            string sql = string.Empty;
            ArrayList al = new ArrayList();

            if (Sql.GetCommonSql("RADT.Inpatient.GetPatientReceiveInfo.1", ref sql) == -1)
            #region SQL
            /*SELECT happen_no,   --�������
					   nurse_cell_code,   --����Ԫ����
					   nurse_cell_name,   --����Ԫ����
					   house_doc_code,   --ҽʦ����(סԺ)
					   house_doc_name,   --ҽʦ����(סԺ)
					   charge_doc_code,   --ҽʦ����(����)
					   charge_doc_name,   --ҽʦ����(����)
					   chief_doc_code,   --ҽʦ����(����)
					   chief_doc_name,   --ҽʦ����(����)
					   duty_nurse_code,   --��ʿ����(����)
					   duty_nurse_name,   --��ʿ����(����)
					   practice_doc_code,   --ʵϰҽʦ
					   practice_doc_name,   --ʵϰҽʦ����
					   noviciate_doc_code,   --����ҽʦ
					   noviciate_doc_name,   --����ҽʦ����
					   convoy_nrs_code,   --���ͻ�ʿ
					   convoy_nrs_name,   --���ͻ�ʿ����
					   convoy_tool_code,   --���͹���
					   convoy_tool_name,   --���͹�������
					   director_code,   --�����δ���
					   director_name,   --����������
					   oper_code,   --����Ա
					   oper_date    --��������
				  FROM fin_ipr_receiveinfo   --������Ϣ��
				 where PARENT_CODE='[��������]' and 
					   CURRENT_CODE='[��������]' and  
					   inpatient_no='{0}' and cancel_flag='0'
					   */
            #endregion
            {
                return null;
            }
            sql = string.Format(sql, inPatientNO);
            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            try
            {
                while (Reader.Read())
                {
                    PatientInfo PatientInfo = new PatientInfo();

                    PatientInfo.ID = inPatientNO;
                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.Memo = Reader[0].ToString(); //�������
                        if (!Reader.IsDBNull(1)) PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[1].ToString(); //����Ԫ����
                        if (!Reader.IsDBNull(2)) PatientInfo.PVisit.PatientLocation.NurseCell.Name = Reader[2].ToString(); //����Ԫ����
                        if (!Reader.IsDBNull(3)) PatientInfo.PVisit.AdmittingDoctor.ID = Reader[3].ToString(); //ҽʦ����(סԺ)
                        if (!Reader.IsDBNull(4)) PatientInfo.PVisit.AdmittingDoctor.Name = Reader[4].ToString(); //ҽʦ����(סԺ)
                        if (!Reader.IsDBNull(5)) PatientInfo.PVisit.AttendingDoctor.ID = Reader[5].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(6)) PatientInfo.PVisit.AttendingDoctor.Name = Reader[6].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(7)) PatientInfo.PVisit.ConsultingDoctor.ID = Reader[7].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(8)) PatientInfo.PVisit.ConsultingDoctor.Name = Reader[8].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(9)) PatientInfo.PVisit.AdmittingNurse.ID = Reader[9].ToString(); //��ʿ����(����)
                        if (!Reader.IsDBNull(10)) PatientInfo.PVisit.AdmittingNurse.Name = Reader[10].ToString(); //��ʿ����(����)
                        if (!Reader.IsDBNull(11)) PatientInfo.PVisit.TempDoctor.ID = Reader[11].ToString(); //ʵϰҽʦ
                        if (!Reader.IsDBNull(12)) PatientInfo.PVisit.TempDoctor.Name = Reader[12].ToString(); //ʵϰҽʦ����
                        if (!Reader.IsDBNull(13)) PatientInfo.PVisit.ReferringDoctor.ID = Reader[13].ToString(); //����ҽʦ
                        if (!Reader.IsDBNull(14)) PatientInfo.PVisit.ReferringDoctor.Name = Reader[14].ToString(); //����ҽʦ����
                        //						if(!this.Reader.IsDBNull(15)) PatientInfo=this.Reader[15].ToString();//���ͻ�ʿ
                        //						if(!this.Reader.IsDBNull(16)) PatientInfo=this.Reader[17].ToString();//���ͻ�ʿ����
                        //						if(!this.Reader.IsDBNull(18)) PatientInfo=this.Reader[18].ToString();//���͹���
                        //						if(!this.Reader.IsDBNull(19)) PatientInfo=this.Reader[19].ToString();//���͹�������
                        //						if(!this.Reader.IsDBNull(20)) PatientInfo=this.Reader[20].ToString();//�����δ���		
                        //						if(!this.Reader.IsDBNull(21)) PatientInfo=this.Reader[21].ToString();//����������
                        if (!Reader.IsDBNull(22)) PatientInfo.User01 = Reader[22].ToString(); //����Ա
                        if (!Reader.IsDBNull(23)) PatientInfo.User02 = Reader[23].ToString(); //��������
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                    }
                    al.Add(PatientInfo);
                }
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            Reader.Close();

            return al;
        }
        [Obsolete("����Ϊ QueryPatientReceiveInfo", true)]
        public ArrayList GetPatientReceiveInfo(string InPatientNo)
        {
            return null;
        }
        #endregion

        #region ��סԺ��ˮ�Ų�ѯ�������ⴲλռ��״̬

        /// <summary>
        /// ��ѯ�������ⴲλռ����Ϣ���������Ҵ���
        /// </summary>
        /// <param name="InPatientNo"></param>
        /// <returns></returns>
        public ArrayList GetSpecialBedInfo(string InPatientNo)
        {
            string sql = string.Empty;
            ArrayList al = new ArrayList();

            if (Sql.GetCommonSql("RADT.Inpatient.GetSpecialBedInfo.1", ref sql) == -1)
            #region SQL
            /*
				  SELECT bed_no,                    --����
				         bed_kind                   --1 �Ҵ� 2 ���� 
				         FROM fin_ipr_hangbedinfo   --�Ҵ���Ϣ��
				   where PARENT_CODE='[��������]' and 
							CURRENT_CODE='[��������]' and 
							inpatient_no='{0}'  and
							status = '0'            --�Ҵ�
				*/
            #endregion
            {
                return null;
            }
            sql = string.Format(sql, InPatientNo);
            if (ExecQuery(sql) < 0) return null;

            #region "read"

            try
            {
                while (Reader.Read())
                {
                    Bed obj = new Bed();

                    try
                    {
                        obj.ID = Reader[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                    }
                    try
                    {
                        obj.Memo = Reader[1].ToString();
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                    }
                    al.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            Reader.Close();

            #endregion

            return al;
        }

        #endregion

        #region ��ҽ��֤�ź͵��ԺŲ�ѯ������Ϣ

        /// <summary>
        /// ��ҽ��֤�ź͵��ԺŲ�ѯ
        /// </summary>
        /// <param name="strPcNo">���պ�</param>
        /// <param name="strMcardNo">ҽ��֤��</param>
        /// <returns></returns>
        //		public PatientInfo PatientQueryByPcNo(string strPcNo,string strMcardNo)
        //		{
        //			string strSql1=string.Empty,strSql2=string.Empty,strSql3 = string.Empty,strSqlWhere = string.Empty;
        //					
        //			strSql1 = PatientQuerySelect();
        //			if (strSql1==null ) return null;
        //			
        //			if(this.Sql.GetCommonSql("FT.FeeReport.Where",ref strSqlWhere)==-1)return null;
        //			strSql1 = strSql1+" "+ strSqlWhere;
        //			if(strPcNo!=string.Empty)
        //			{
        //				if(this.Sql.GetCommonSql("FT.FeeReport.Where1",ref strSql2)==-1)return null;//���Ժ�
        //				try
        //				{
        //					strSql1=strSql1+" "+string.Format(strSql2,strPcNo);
        //				}
        //				catch
        //				{
        //					this.Err="��ֵ��ƥ�䣡";
        //					this.ErrCode="��ֵ��ƥ�䣡";
        //					return null;
        //				}
        //			}
        //			if(strMcardNo!=string.Empty)
        //			{
        //				if(this.Sql.GetCommonSql("FT.FeeReport.Where2",ref strSql3)==-1)return null;//ҽ��֤��
        //				try
        //				{
        //					strSql1 = strSql1+" "+string.Format(strSql3,strMcardNo);
        //				}
        //				catch
        //				{
        //					this.Err="��ֵ��ƥ�䣡";
        //					this.ErrCode="��ֵ��ƥ�䣡";
        //					return null;
        //				}
        //			}
        //			ArrayList al=new ArrayList();
        //			FS.HISFC.Models.RADT.PatientInfo PatientInfo;
        //			try
        //			{
        //				al=this.myPatientQuery(strSql1);
        //				PatientInfo=(FS.HISFC.Models.RADT.PatientInfo)al[0];
        //			}
        //			catch(Exception ee)
        //			{
        //				string Error = ee.Message;
        //				return null;
        //			}
        //			return PatientInfo;
        //						
        //		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPcNo"></param>
        /// <param name="strMcardNo"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByPcNoRetArray(string strPcNo, string strMcardNo)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;
            string strSql3 = string.Empty;
            string strSqlWhere = string.Empty;

            ArrayList al = new ArrayList();
            strSql1 = PatientQuerySelect();
            if (strSql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("FT.FeeReport.Where", ref strSqlWhere) == -1)
            {
                return null;
            }
            strSql1 = strSql1 + " " + strSqlWhere;
            if (strPcNo != string.Empty)
            {
                if (Sql.GetCommonSql("FT.FeeReport.Where1", ref strSql2) == -1)
                {
                    return null;
                } //���Ժ�
                try
                {
                    strSql1 = strSql1 + " " + string.Format(strSql2, strPcNo);
                }
                catch
                {
                    Err = "��ֵ��ƥ�䣡";
                    ErrCode = "��ֵ��ƥ�䣡";
                    WriteErr();
                    return null;
                }
                try
                {
                    al = myPatientQuery(strSql1);
                }
                catch (Exception ee)
                {
                    Err = ee.Message;
                    WriteErr();
                    return null;
                }
            }
            else
            {
            }
            if (strMcardNo != string.Empty)
            {
                if (Sql.GetCommonSql("FT.FeeReport.Where2", ref strSql3) == -1)
                {
                    return null;
                } //ҽ��֤��
                try
                {
                    strSql1 = strSql1 + " " + string.Format(strSql3, strMcardNo);
                }
                catch
                {
                    Err = "��ֵ��ƥ�䣡";
                    ErrCode = "��ֵ��ƥ�䣡";
                    al = null;
                }
                try
                {
                    al = myPatientQuery(strSql1);
                }
                catch (Exception ee)
                {
                    string Error = ee.Message;
                    return null;
                }
            }
            else
            {
            }

            return al;
        }

        #endregion

        #region ��ѯ��������ת�Ƶ�Ŀ�����

        /// <summary>
        ///  ��ѯ��������ת�Ƶ�Ŀ�����
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="oldLocationID"></param>
        /// <returns></returns>
        public Location QueryShiftNewLocation(string inpatientNO, string oldLocationID)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.QueryShiftNewLocation.1
            //���룺0 סԺ��ˮ�� 1ԭ����ID
            //��������Ŀ�겿����Ϣ

            #endregion

            string sql = string.Empty;
            Location newLocation = new Location();
            if (sql == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.QueryShiftNewLocation.1", ref sql) == -1)
            {
                return null;
            }

            try
            #region SQL
            /*SELECT       new_dept_code,         --ת������
							   new_nurse_cell_code,   --ת������վ����
							   confirm_opercode,      --ȷ����
							   confirm_date,          --ת��ȷ��ʱ��
							   cancel_code,           --ȡ����
							   cancel_date,           --ȡ������ʱ��
							   mark,                  --��ע
						       SHIFT_STATE --����״̬
						FROM fin_ipr_shiftapply   --ת�������
						where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and 
							  inpatient_no = '{0}' and old_dept_code= '{1}' and shift_state in('1','0')
				*/
            #endregion
            {
                sql = string.Format(sql, inpatientNO, oldLocationID);
            }
            catch (Exception ex)
            {
                Err = "RADT.Inpatient.QueryShiftNewLocation.1�Ĳ�������" + ex.Message;
                WriteErr();
                return null;
            }
            if (ExecQuery(sql) == -1)
            {
                return null;
            }
            if (Reader.Read())
            {
                newLocation.Dept.ID = Reader[0].ToString();
                try
                {
                    newLocation.NurseCell.ID = Reader[1].ToString();
                }
                catch
                {
                }
                newLocation.Memo = Reader[6].ToString();
                newLocation.User03 = Reader[7].ToString();
                newLocation.User01 = Reader[8].ToString();
            }
            Reader.Close();
            return newLocation;
        }

        #endregion

        #region ��������״̬��ѯ���߻�����Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��ѯ������ͬ״̬�Ļ���
        /// </summary>
        /// <param name="nurseCode">��������</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList QueryPatientBasicByNurseCell(string nurseCode, InStateEnumService State)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.5
            //���룺�������룬סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty;
            string sql2 = string.Empty;
            sql1 = PatientQueryBasicSelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.5", ref sql2) == -1)
            #region SQL
            /* WHERE PARENT_CODE='[��������]' AND CURRENT_CODE='[��������]' AND NURSE_CELL_CODE ='{0}' AND IN_STATE = '{1}' ORDER BY BED_NO */
            #endregion
            {
                Err = "û���ҵ�RADT.Inpatient.PatientQuery.Where.5�ֶ�!";
                ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, nurseCode, State.ID.ToString());
            return myPatientBasicQuery(sql1);
        }

        #endregion

        #region ��������ѯ��Ҫȷ���˷ѻ��߻�����Ϣ�б�

        /// <summary>
        /// ��������ѯ��Ҫȷ���˷ѻ��߻�����Ϣ�б�
        /// </summary>
        /// <param name="nurseCode">��������</param>
        /// <returns></returns>
        public ArrayList QueryQuitFeePatientByNurseCell(string nurseCode)
        {

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.QueryQuitFeePatientByNurseCell", ref sql2) == -1)
            {
                Err = "û���ҵ�RADT.Inpatient.QueryQuitFeePatientByNurseCell�ֶ�!";
                ErrCode = "-1";
                WriteErr();
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, nurseCode);
            return myPatientQuery(sql1);
        }

        /// <summary>
        /// ��������ѯ��Ҫȷ���˷ѻ��߻�����Ϣ�б�
        /// </summary>
        /// <param name="nurseCode">��������</param>
        /// <param name="feeDeptCode">�˷��������</param>
        /// <returns></returns>
        public ArrayList QueryQuitFeePatientByNurseCell(string nurseCode, string feeDeptCode)
        {
            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.QueryQuitFeePatientByNurseCell2", ref sql2) == -1)
            {
                Err = "û���ҵ�RADT.Inpatient.QueryQuitFeePatientByNurseCell2�ֶ�!";
                ErrCode = "-1";
                WriteErr();
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, nurseCode, feeDeptCode);
            return myPatientQuery(sql1);
        }


        #endregion

        #region ��������״̬��ѯ������Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��ѯ������ͬ״̬�Ļ���
        /// </summary>
        /// <param name="nurseCode">��������</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList PatientQueryByNurseCell(string nurseCode, FS.HISFC.Models.Base.EnumInState State)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.5
            //���룺�������룬סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.5", ref sql2) == -1)
            {
                Err = "û���ҵ�RADT.Inpatient.PatientQuery.Where.5�ֶ�!";
                ErrCode = "-1";
                WriteErr();
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, nurseCode, State.ToString());
            return myPatientQuery(sql1);
        }

        /// <summary>
        /// ���߲�ѯ-��ѯ���Ҳ�ͬ״̬�Ļ���
        /// </summary>
        /// <param name="deptCode">��������</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList PatientQueryByInState(string inState)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.5.1
            //���룺���ұ��룬סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.5.1", ref sql2) == -1)
            {
                sql2 = @"        WHERE  IN_STATE in ( '{0}' )
                                          ORDER BY d.DEPT_CODE,lpad(replace(d.BED_NO,d.NURSE_CELL_CODE),3,'0'),d.PATIENT_NO";
            }
            sql1 = sql1 + " " + string.Format(sql2, inState.ToString());
            return myPatientQuery(sql1);
        }
        //{62EAD92D-49F6-45d5-B378-1E573EC27728}
        /// <summary>
        /// ���߲�ѯ-��ѯ������ͬ״̬�Ļ���(Ƿ��)
        /// </summary>
        /// <param name="nurseCode">��������</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList PatientQueryByNurseCellForAlert(string nurseCode, FS.HISFC.Models.Base.EnumInState State)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.5
            //���룺�������룬סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.19", ref sql2) == -1)
            {
                Err = "û���ҵ�RADT.Inpatient.PatientQuery.Where.19�ֶ�!";
                ErrCode = "-1";
                WriteErr();
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, nurseCode, State.ToString());
            return myPatientQuery(sql1);
        }
        /// <summary>
        /// ���߲�ѯ-�������Ϳ��Ҳ�ѯ��ͬ״̬�Ļ���
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByNurseCellAndDept(string nurseCode, string deptCode, FS.HISFC.Models.Base.EnumInState State)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.5
            //���룺�������룬����,סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.16", ref sql2) == -1)
            {
                Err = "û���ҵ�RADT.Inpatient.PatientQuery.Where.16�ֶ�!";
                ErrCode = "-1";
                WriteErr();
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, nurseCode, deptCode, State.ToString());
            return myPatientQuery(sql1);
        }
        //
        /// <summary>
        /// ���߲�ѯ-�������Ϳ��Ҳ�ѯ��ͬ״̬�Ļ���(Ƿ��){62EAD92D-49F6-45d5-B378-1E573EC27728}
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByNurseCellAndDeptForAlert(string nurseCode, string deptCode, FS.HISFC.Models.Base.EnumInState State)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.5
            //���룺�������룬����,סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.17", ref sql2) == -1)
            {
                Err = "û���ҵ�RADT.Inpatient.PatientQuery.Where.17�ֶ�!";
                ErrCode = "-1";
                WriteErr();
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, nurseCode, deptCode, State.ToString());
            return myPatientQuery(sql1);
        }
        /// <summary>
        /// ���߲�ѯ-��ѯ������ͬ״̬�Ļ���(������Ӥ��)
        /// </summary>
        /// <param name="nurseCode">��������</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList PatientQueryExceptBaby(string nurseCode, FS.HISFC.Models.Base.EnumInState State)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.5
            //���룺�������룬סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.ExceptBaby", ref sql2) == -1)
            {
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, nurseCode, State.ToString());
            return myPatientQuery(sql1);
        }

        #endregion

        #region ���ݳ�Ժ�ٻص���Ч������ó�Ժ���ߵ���Ϣ

        /// <summary>
        /// ������Ч��Ժ�ٻص���Ч������ѯ��Ժ�Ǽǻ�����Ϣ
        /// ----Create By ZhangQi
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <param name="vaildDays"></param> 
        /// <returns></returns>
        public ArrayList PatientQueryByVaildDate(string deptCode, FS.HISFC.Models.RADT.InStateEnumService inState, int vaildDays)
        {
            ArrayList al = new ArrayList();
            ArrayList al2 = new ArrayList();
            FS.HISFC.Models.RADT.PatientInfo PatientInfo;
            al = this.PatientQuery(deptCode, inState);
            for (int i = 0; i < al.Count; i++)
            {
                PatientInfo = al[i] as FS.HISFC.Models.RADT.PatientInfo;
                DateTime dt = PatientInfo.PVisit.OutTime;
                DateTime ds = dt.AddDays(vaildDays);
                ds = ds.Date;
                ds = ds.AddHours(23);
                ds = ds.AddMinutes(59);
                ds = ds.AddSeconds(59);
                DateTime sysdate = GetDateTimeFromSysDateTime();
                if (ds > sysdate)
                    al2.Add(PatientInfo);
            }
            return al2;
        }

        /// <summary>
        /// ������Ч��Ժ�ٻص���Ч������ѯ��Ժ�Ǽǻ�����Ϣ
        /// ----Create By Sunm
        /// {9A2D53D3-25BE-4630-A547-A121C71FB1C5}
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <param name="vaildDays"></param> 
        /// <returns></returns>
        public ArrayList PatientQueryByNurseCellVaildDate(string deptCode, FS.HISFC.Models.Base.EnumInState inState, int vaildDays)
        {
            ArrayList al = new ArrayList();
            ArrayList al2 = new ArrayList();
            DateTime sysdate = GetDateTimeFromSysDateTime();
            FS.HISFC.Models.RADT.PatientInfo PatientInfo;
            al = this.PatientQueryByNurseCell(deptCode, inState);
            for (int i = 0; i < al.Count; i++)
            {
                PatientInfo = al[i] as FS.HISFC.Models.RADT.PatientInfo;
                DateTime dt = PatientInfo.PVisit.OutTime;
                DateTime ds = dt.AddDays(vaildDays);
                ds = ds.Date;
                #region update by xuewj �������ж� {D6E46E15-2856-4cb1-9503-0816CB1F834E}
                //ds = ds.AddHours(23);
                //ds = ds.AddMinutes(59);
                //ds = ds.AddSeconds(59); 
                #endregion
                if (ds > sysdate)
                    al2.Add(PatientInfo);
            }
            return al2;
        }


        #endregion

        #region ������Ч�ٻ��ڲ�ѯһ��ʱ����ĳ����������Ч�ٻ��ڵĳ�Ժ�Ǽǻ���(��ֹʱ��  ���Ҵ��� ��Ч����)

        /// <summary>
        /// ������Ч�ٻ��ڲ�ѯһ��ʱ����ĳ�����ҵĳ�Ժ�Ǽǻ���(��ֹʱ��  ���Ҵ��� ��Ч����)
        /// ----Create By ZhangQi
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="beginTime"></param>
        ///  <param name="endTime"></param>
        /// <param name="vaildDays"></param> 
        ///  <param name="myPatientState"></param>
        /// <returns></returns>
        public ArrayList OutHosPatientByState(string deptCode, string beginTime, string endTime, int vaildDays, int myPatientState)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.Where.OutHos
            //���룺���ұ��룬ʱ�䷶Χ, ��Ч����
            //������������Ϣ
            #endregion
            FS.HISFC.Models.Base.EnumInState inState = new EnumInState();
            inState = EnumInState.B;
            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;

            ArrayList al = new ArrayList();
            ArrayList al2 = new ArrayList();
            FS.HISFC.Models.RADT.PatientInfo PatientInfo;
            al = this.PatientQuery(deptCode, istate);

            for (int i = 0; i < al.Count; i++)
            {
                PatientInfo = al[i] as FS.HISFC.Models.RADT.PatientInfo;
                DateTime dt = PatientInfo.PVisit.OutTime;
                DateTime ds = dt.AddDays(vaildDays);
                ds = ds.Date;
                ds = ds.AddHours(23);
                ds = ds.AddMinutes(59);
                ds = ds.AddSeconds(59);
                DateTime sysdate = GetDateTimeFromSysDateTime();

                if (PatientInfo.PVisit.OutTime >= Convert.ToDateTime(beginTime)
                    && (PatientInfo.PVisit.OutTime <= Convert.ToDateTime(endTime)))
                {
                    if ((myPatientState == 0) && (ds > sysdate))
                    {
                        al2.Add(PatientInfo);
                    }
                    else if ((myPatientState == 1) && (ds <= sysdate))
                    {
                        al2.Add(PatientInfo);
                    }
                    else
                    {
                    }
                }
            }
            return al2;
        }



        #endregion

        #region �����Һ�״̬��ѯ���߻�����Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��ѯ���Ҳ�ͬ״̬�Ļ���
        /// </summary>
        /// <param name="dept_code">���ұ���</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList QueryPatientBasic(string dept_code, InStateEnumService State)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.6
            //���룺���ұ��룬סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQueryBasicSelect();
            if (sql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.6", ref sql2) == -1)
            {
                return null;
            }
            sql2 = " " + string.Format(sql2, dept_code, State.ID.ToString());
            return myPatientBasicQuery(sql1 + sql2);
        }

        #endregion

        #region �����Һ�״̬��ѯ������Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��ѯ���Ҳ�ͬ״̬�Ļ���
        /// </summary>
        /// <param name="dept_code">���ұ���</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList PatientQuery(string dept_code, InStateEnumService State)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.6
            //���룺���ұ��룬סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.6", ref sql2) == -1)
            {
                return null;
            }
            sql2 = " " + string.Format(sql2, dept_code, State.ID.ToString());
            return myPatientQuery(sql1 + sql2);
        }

        #endregion

        #region �����Һ�״̬��ѯ������Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��ѯҽ���鲻ͬ״̬�Ļ���//{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
        /// </summary>
        /// <param name="medicalTeamCode">���ұ���</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList PatientQueryByMedicalTeam(string medicalTeamCode, InStateEnumService State, string deptCode)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.6
            //���룺���ұ��룬סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.98", ref sql2) == -1)
            {
                return null;
            }
            sql2 = " " + string.Format(sql2, medicalTeamCode, State.ID.ToString(), deptCode);
            return myPatientQuery(sql1 + sql2);
        }

        #endregion

        #region  ����������סԺʱ�����Ժ״̬��ѯ�������б�

        #endregion

        #region ���տ��Һ�ʱ��β�ѯ�޷���Ժ������Ϣ
        /// <summary>
        /// ���տ��Һ�ʱ��β�ѯ�޷���Ժ������Ϣ --Create by ZhangQi
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList PatientNoFeeQuery(string deptCode, string beginTime, string endTime)
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("Local.Report.Patient.NoFee.Query", ref sql) == -1)
            {
                return null;
            }
            sql = " " + string.Format(sql, deptCode, beginTime, endTime);
            return myPatientNoFeeQuery(sql);
        }

        #endregion

        #region ���տ��� ʱ��� ��ҽ�������ѯ��Ժ�������
        /// <summary>
        /// ���տ��� ʱ��� ��ҽ�������ѯ��Ժ������� --Create By ZhangQi
        /// </summary>
        /// <param name="inState"></param>
        /// <param name="deptCode"></param>
        /// <param name="docCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList PatientOutHosQuery(string inState, string deptCode, string docCode, string beginTime, string endTime)
        {
            //inState.ID
            string sql = string.Empty;
            if (Sql.GetCommonSql("Local.Report.Patient.OutHos.Query", ref sql) == -1)
            {
                return null;
            }
            sql = " " + string.Format(sql, inState, deptCode, docCode, beginTime, endTime);
            return myPatientOutHosQuery(sql);
        }
        #endregion

        #region ���տ��� ʱ��κ���Ҫ��ϲ�ѯ��Ժ���ߵ�������
        /// <summary>
        /// ���տ��� ʱ��κ���Ҫ��ϲ�ѯ��Ժ���ߵ�������--Create By ZhangQi
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="diagnoreName"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList PatientDiagnoreQuery(string deptCode, string diagnoreName, string beginTime, string endTime)
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("Local.Report.Patient.Diagnore.Query", ref sql) == -1)
            {
                return null;
            }
            sql = " " + string.Format(sql, deptCode, diagnoreName, beginTime, endTime);
            return myPatientDiagnoreQuery(sql);
        }
        #endregion

        #region ��ϲ�ѯ
        /// <summary>
        /// ��ϲ�ѯ ---Create By ZhangQi
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList myPatientDiagnoreQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;

            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            //ȡϵͳʱ��,�����õ������ַ���
            DateTime sysDate = GetDateTimeFromSysDateTime();

            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new PatientInfo();
                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.PID.PatientNO = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); // ����
                        if (!Reader.IsDBNull(2)) PatientInfo.Sex.ID = Reader[2].ToString();//�Ա�
                        if (!Reader.IsDBNull(3)) PatientInfo.Age = Reader[3].ToString();//����
                        if (!Reader.IsDBNull(4)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[4].ToString();//����
                        if (!Reader.IsDBNull(5)) PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5]);//��Ժ����
                        if (!Reader.IsDBNull(6)) PatientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6]);//��Ժ����
                        if (!Reader.IsDBNull(7)) PatientInfo.MainDiagnose = Reader[7].ToString();//��Ҫ���
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                        return null;
                    }
                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }
            }
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return al;
        }
        #endregion

        #region ��Ժ���������ѯ
        /// <summary>
        /// ��Ժ���������ѯ --Create By ZhangQi
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList myPatientOutHosQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo = new PatientInfo();
            ProgressBarText = "���ڲ�ѯ...";
            ProgressBarValue = 0;

            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            try
            {
                while (Reader.Read())
                {
                    try
                    {
                        PatientInfo = new PatientInfo();
                        if (!Reader.IsDBNull(0)) PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[0].ToString();
                        if (!Reader.IsDBNull(1)) PatientInfo.PID.PatientNO = Reader[1].ToString();
                        if (!Reader.IsDBNull(2)) PatientInfo.Name = Reader[2].ToString();
                        if (!Reader.IsDBNull(3)) PatientInfo.Sex.ID = Reader[3].ToString();
                        if (!Reader.IsDBNull(4)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[4].ToString();
                        if (!Reader.IsDBNull(5)) PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
                        if (!Reader.IsDBNull(6)) PatientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6]);
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                        return null;
                    }
                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }

            }
            catch (Exception ex)
            {
                Err = "��û�����Ϣ����" + ex.Message;
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();
            ProgressBarValue = -1;
            return al;
        }

        #endregion

        #region �޷���Ժ����
        /// <summary>
        /// �޷���Ժ���߲�ѯ --Create By ZhangQi
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList myPatientNoFeeQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo = new PatientInfo();
            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            try
            {
                while (Reader.Read())
                {
                    try
                    {
                        PatientInfo = new PatientInfo();
                        if (!Reader.IsDBNull(0)) PatientInfo.PID.PatientNO = Reader[0].ToString();
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString();
                        if (!Reader.IsDBNull(2)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[2].ToString();
                        if (!Reader.IsDBNull(3)) PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[3]);
                        if (!Reader.IsDBNull(4)) PatientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4]);
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                        return null;
                    }
                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }
            }
            catch (Exception ex)
            {
                Err = "��û�����Ϣ����" + ex.Message;
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();
            ProgressBarValue = -1;
            return al;
        }
        #endregion

        #region ���ݿ��� ʱ����ѯĳһʱ������Ժ�������
        /// <summary>
        /// �޷���Ժ���߲�ѯ --Create By ZhangQi �ĳ���Ŀ����
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        public ArrayList PatientInHosQueryByTime(string deptCode, string queryTime)
        {
            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }
            string sql = string.Empty;
            if (Sql.GetCommonSql("Local.Report.Patient.InHos.QueryByTime", ref sql2) == -1)
            {
                Err = "û���ҵ�Local.Report.Patient.InHos.QueryByTime�ֶ�!";
                ErrCode = "-1";
                WriteErr();
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, deptCode, queryTime);
            return myPatientQuery(sql1);
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <param name="State"></param>
        /// <param name="PayKind"></param>
        /// <returns></returns>
        public ArrayList PatientQuery(string Begin, string End, string State, string PayKind)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.6
            //���룺��ʼʱ�䣬����ʱ�䣬סԺ״̬��֧�����
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.26", ref sql2) == -1)
            {
                return null;
            }
            sql2 = " " + string.Format(sql2, Begin, End, PayKind, State);
            return myPatientQuery(sql1 + " " + sql2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <param name="State"></param>
        /// <param name="PayKind"></param>
        /// <returns></returns>
        public ArrayList PatientQuery(string Begin, string End, string State, string PayKind, string flag)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.6
            //���룺��ʼʱ�䣬����ʱ�䣬סԺ״̬��֧�����
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.26.ForBill", ref sql2) == -1)
            {
                return null;
            }
            sql2 = " " + string.Format(sql2, Begin, End, PayKind, State);
            return myPatientQuery(sql1 + " " + sql2);
        }

        #endregion

        public DataSet PatientqueryByUnitRetdts(string pactUnit, string beginTime, string endTime)
        {
            string strSql = string.Empty;
            DataSet dts = new DataSet();
            string strsql1 = string.Empty;
            string strsql2 = string.Empty;
            string strSqlBegin = string.Empty;
            string strSqlEnd = string.Empty;
            string strPact = string.Empty;

            //��ȡsql select ���
            strsql1 = QueryComPatientInfoSelect();
            strSql += strsql1;
            if (strsql1 == null)
            {
                return null;
            }
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.20", ref strsql2) == -1)
            {
                #region SQL
                //	where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]'  
                #endregion
                return null;
            }
            else
            {
                strSql += strsql2;
            }
            string[] arg = new string[2];
            if (pactUnit != string.Empty)
            {
                if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.Pact", ref strPact) == -1)
                {
                    #region SQL
                    //and  pact_code='{0}'
                    #endregion

                    return null;
                }
                else
                {
                    strSql += " " + string.Format(strPact, pactUnit);
                }
            }
            if (beginTime != string.Empty)
            {
                if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.DateBegin", ref strSqlBegin) == -1)
                {
                    #region SQL
                    //	and OPER_DATE >= to_date('{0}','yyyy-mm-dd HH24:mi:ss') 
                    #endregion

                    return null;
                }
                else
                {
                    strSql += " " + string.Format(strSqlBegin, beginTime);
                }
            }
            if (endTime != string.Empty)
            {
                if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.DateEnd", ref strSqlEnd) == -1)
                {
                    #region SQL
                    //	and OPER_DATE <= to_date('{0}','yyyy-mm-dd HH24:mi:ss') 
                    #endregion

                    return null;
                }
                else
                {
                    strSql += " " + string.Format(strSqlEnd, endTime);
                }
            }

            dts = new DataSet();
            if (ExecQuery(strSql, ref dts) == -1)
            {
                return null;
            }
            else
            {
                return dts;
            }
        }

        # region ��ѯ��Σ���ߣ���ҽ��ƹ���

        /// <summary>
        /// ���ղ����ѯ����
        /// </summary>
        /// <param name="criticalFlag">�������  0 ��ͨ 1 ���� 2 ��Σ</param>
        /// <returns>���ػ�����Ϣ�б�</returns>
        public ArrayList QuerySpecialPatient(string criticalFlag)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("Manager.SpecialPatient.QuerySpecialPatient", ref strSql) == -1)
            #region SQL ��Σ��0 ��ͨ 1 ���� 2 ��Σ
            /*SELECT INPATIENT_NO,   --סԺ��ˮ��
				 CRITICAL_FLAG   --���˱�־
					FROM  FIN_IPR_INMAININFO
					WHERE PARENT_CODE = '[��������]' AND CURRENT_CODE = '[��������]' AND IN_STATE = 'I' AND CRITICAL_FLAG = '2' 
					ORDER BY INPATIENT_NO
				*/
            #endregion
            {
                return null;
            }
            if (strSql == null || strSql == string.Empty)
            {
                return null;
            }
            strSql = string.Format(strSql, criticalFlag);

            return myGetSpecialPatient(strSql);
        }
        [Obsolete("����Ϊ QuerySpecialPatient(string criticalFlag) �Ӳ��� 2", true)]
        public ArrayList QuerySpecialPatient()
        {
            return null;
        }
        # endregion

        # region ��ò�λ���߻�����Ϣ

        /// <summary>
        /// ��ò�λ���� ���߻�����Ϣ
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public ArrayList QuerySpecilPatient(string strWhere)
        {
            string strSql = string.Empty;
            strSql = PatientQuerySelect();

            if (strSql == null || strSql == string.Empty)
            {
                return null;
            }
            strSql = strSql + strWhere;
            return myPatientQuery(strSql);
        }

        # endregion

        # region ��ѯ���ػ��ߣ���ҽ��ƹ���

        /// <summary>
        /// ��ѯ���ػ��ߣ���ҽ��ƹ���
        /// </summary>
        /// <returns></returns>
        [Obsolete("����Ϊ QuerySpecialPatient(string criticalFlag) �Ӳ��� 1", true)]
        public ArrayList QuerySpecialPatient1()
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("Manager.SpecialPatient.QuerySpecialPatient1", ref strSql) == -1)
            #region SQL
            /* SELECT INPATIENT_NO,   --סԺ��ˮ��
				   CRITICAL_FLAG  --���˱�־
					FROM  FIN_IPR_INMAININFO
					WHERE PARENT_CODE = '[��������]' AND CURRENT_CODE = '[��������]' AND IN_STATE = 'I' AND CRITICAL_FLAG = '1' 
					ORDER BY INPATIENT_NO*/
            #endregion
            {
                return null;
            }
            if (strSql == null || strSql == string.Empty)
            {
                return null;
            }
            return myGetSpecialPatient(strSql);
        }

        # endregion

        #region ����ͬ��λ��ѯ������Ϣ�б�

        /// <summary>
        /// ���պ�ͬ��λ��ʱ��β�ѯ������Ϣ
        /// </summary>
        /// <param name="pactUnit">��ͬ��λ</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByUnit(string pactUnit, string beginTime, string endTime)
        {
            string strSql = string.Empty;
            ArrayList al = new ArrayList();
            string strsql1 = string.Empty;
            string strsql2 = string.Empty;
            string strSqlBegin = string.Empty;
            string strSqlEnd = string.Empty;
            string strPact = string.Empty;
            strsql1 = QueryComPatientInfoSelect();
            strSql += strsql1;

            if (strsql1 == null)
            {
                return null;
            }
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.20", ref strsql2) == -1)
            #region SQL
            /*  where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' */
            #endregion
            {
                return null;
            }
            else
            {
                strSql += strsql2;
            }
            string[] arg = new string[2];
            if (pactUnit != string.Empty)
            {
                if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.Pact", ref strPact) == -1)
                #region SQL
                /*	and  pact_code='{0}' */
                #endregion
                {
                    return null;
                }
                else
                {
                    strSql += " " + string.Format(strPact, pactUnit);
                }
            }
            if (beginTime != string.Empty)
            {
                if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.DateBegin", ref strSqlBegin) == -1)
                #region SQL
                /*  and OPER_DATE >= to_date('{0}','yyyy-mm-dd HH24:mi:ss') */
                #endregion
                {
                    return null;
                }
                else
                {
                    strSql += " " + string.Format(strSqlBegin, beginTime);
                }
            }
            if (endTime != string.Empty)
            {
                if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.DateEnd", ref strSqlEnd) == -1)
                #region SQL
                /*	and OPER_DATE <= to_date('{0}','yyyy-mm-dd HH24:mi:ss') */
                #endregion
                {
                    return null;
                }
                else
                {
                    strSql += " " + string.Format(strSqlEnd, endTime);
                }
            }

            return myCardPatientQuery(strSql);
        }

        [Obsolete("��QueryPatientByUnit����", true)]
        public ArrayList PatientQueryByUnit(string pactUnit, string beginTime, string endTime)
        {
            return null;
        }
        #endregion

        #region ��������ѯ���߻�����Ϣcom_patientinfo
        /// <summary>
        /// ��������ѯ���߻�����Ϣcom_patientinfo
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByName(string name)
        {
            string sql = QueryComPatientInfoSelect();
            string where = "";

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.WhereByName", ref where) == -1)
            {
                return null;
            }

            sql = sql + where;

            try
            {
                sql = string.Format(sql, name);
            }
            catch (Exception e)
            {
                Err = "��ѯ������Ϣ����!" + e.Message;
                return null;
            }

            return myCardPatientQuery(sql);
        }

        #region ��������ѯ���߻�����Ϣcom_patientinfo
        /// <summary>
        /// ���������Ų�ѯ���߻�����Ϣcom_patientinfo{F5F57671-B453-45ff-A663-A682A000F567}
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByNameAndCardno(string info)
        {
            string sql = QueryComPatientInfoSelect();
            string where = "";

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.WhereByNameAndCardno", ref where) == -1)
            {
                return null;
            }

            sql = sql + where;

            try
            {
                sql = string.Format(sql, info);
            }
            catch (Exception e)
            {
                Err = "��ѯ������Ϣ����!" + e.Message;
                return null;
            }

            return myCardPatientQuery(sql);
        }
        #endregion

        // RADT.Inpatient.PatientQuery.WhereByNameAndCardno

        /// <summary>
        /// ���绰��ѯ���߻�����Ϣcom_patientinfo
        /// {8659FDB2-4200-475c-83B6-37092AD86D7D}
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByPhone(string phone)
        {
            string sql = QueryComPatientInfoSelect();
            string where = "";

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.WhereByPhone", ref where) == -1)
            {
                return null;
            }

            sql = sql + where;

            try
            {
                sql = string.Format(sql, phone);
            }
            catch (Exception e)
            {
                Err = "��ѯ������Ϣ����!" + e.Message;
                return null;
            }

            return myCardPatientQuery(sql);
        }

        #endregion

        #region �����￨�Ų�ѯ������Ϣ�б�

        /// <summary>
        /// �����￨�Ų�ѯ����
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientInfoByCardNO(string cardNO)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            ArrayList al = new ArrayList();

            #region �ӿ�˵��

            /////RADT.Inpatient.PatientOneQuery.where.1
            //����:���￨��
            //������������Ϣ

            #endregion

            strSql1 = PatientQuerySelect();
            if (strSql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientCardNoQuery.Where.1", ref strSql2) == -1)
            #region SQL
            /* where PARENT_CODE='[��������]'  and  CURRENT_CODE='[��������]' and  card_no='{0}' */
            #endregion
            {
                return null;
            }
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, cardNO);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientCardNoQuery.Where.1��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientCardNoQuery.Where.1��ֵ��ƥ�䣡";
                WriteErr();
                return null;
            }

            return myPatientQuery(strSql1);
        }
        [System.Obsolete("����Ϊ GetPatientInfoByCardNO", true)]
        public ArrayList PatientCardNoQuery(string CardNO)
        {
            return null;
        }

        /// <summary>
        /// �����￨�Ų�ѯ��Ժ����  {839B57B8-B74C-4818-9647-9881A0CE9013}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientInfoByCardNOAndInState(string cardNO)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            ArrayList al = new ArrayList();

            #region �ӿ�˵��

            /////RADT.Inpatient.PatientOneQuery.where.1
            //����:���￨��
            //������������Ϣ

            #endregion

            strSql1 = PatientQuerySelect();
            if (strSql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientCardNoQuery.Where.CardNoAndInState", ref strSql2) == -1)
            #region SQL
            /* where PARENT_CODE='[��������]'  and  CURRENT_CODE='[��������]' and  card_no='{0}' */
            #endregion
            {
                return null;
            }
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, cardNO);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientCardNoQuery.Where.1��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientCardNoQuery.Where.1��ֵ��ƥ�䣡";
                WriteErr();
                return null;
            }

            return myPatientQuery(strSql1);
        }

        #endregion

        /// <summary>
        /// �����￨�Ų�ѯסԺ�ڼ��в����Ļ���
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientInfoHaveCaseByCardNO(string cardNO)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            ArrayList al = new ArrayList();

            #region �ӿ�˵��

            /////RADT.Inpatient.PatientOneQuery.where.1
            //����:���￨��
            //������������Ϣ

            #endregion

            strSql1 = PatientQuerySelect();
            if (strSql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientCardNoQuery.Where.99", ref strSql2) == -1)
            #region SQL
            /* where PARENT_CODE='[��������]'  and  CURRENT_CODE='[��������]' and  card_no='{0}' */
            #endregion
            {
                return null;
            }
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, cardNO);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientCardNoQuery.Where.99��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientCardNoQuery.Where.99��ֵ��ƥ�䣡";
                WriteErr();
                return null;
            }

            return myPatientQuery(strSql1);
        }


        #region �����￨�Ų�ѯ���˻�����Ϣ

        /// <summary>
        /// ���߻�����Ϣ��ѯ  com_patientinfo
        /// </summary>
        /// <param name="cardNO">����</param>
        /// <returns></returns>
        public PatientInfo QueryComPatientInfo(string cardNO)
        {
            PatientInfo PatientInfo = new PatientInfo();
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientInfoQuery.1", ref sql) == -1)
            #region SQL
            /*SELECT card_no,
						   name,   --����
								   spell_code,   --ƴ����

								   wb_code,   --���
								   birthday,   --��������
								   sex_code,   --�Ա�
								   idenno,   --���֤��
								   blood_code,   --Ѫ��

								   prof_code,   --ְҵ
								   work_home,   --������λ
								   work_tel,   --��λ�绰
								   work_zip,   --��λ�ʱ�
								   home,   --���ڻ��ͥ����

								   home_tel,   --��ͥ�绰
								   home_zip,   --���ڻ��ͥ��������

								   district,   --����
								   nation_code,   --����
								   linkman_name,   --��ϵ������

								   linkman_tel,   --��ϵ�˵绰

								   linkman_add,   --��ϵ��סַ
								   rela_code,   --��ϵ�˹�ϵ

								   mari,   --����״��
								   coun_code,   --����
								   paykind_code,   --�������
								   paykind_name,   --�����������
								   pact_code,   --��ͬ����
								   pact_name,   --��ͬ��λ����
								   mcard_no,   --ҽ��֤��
								   area_code,   --������

								   framt,   --ҽ�Ʒ���
								   ic_cardno,   --���Ժ�

								   anaphy_flag,   --ҩ�����
								   hepatitis_flag,   --��Ҫ����
								   act_code,   --�ʻ�����
								   act_amt,   --�ʻ��ܶ�
								   lact_sum,   --�����ʻ����
								   lbank_sum,   --�����������
								   arrear_times,   --Ƿ�Ѵ���
								   arrear_sum,   --Ƿ�ѽ��
								   inhos_source,   --סԺ��Դ
								   lihos_date,   --���סԺ����

								   inhos_times,   --סԺ����
								   louthos_date,   --�����Ժ����

								   fir_see_date,   --��������
								   lreg_date,   --����Һ�����

								   disoby_cnt,   --ΥԼ����
								   end_date,   --��������
								   mark,   --��ע
								   oper_code,   --����Ա

								   oper_date    --��������
							  FROM com_patientinfo   --���˻�����Ϣ��
							 WHERE PARENT_CODE='[��������]'  and 
								   CURRENT_CODE='[��������]' and 
								   card_no='{0}'
								   */
            #endregion
            {
                return null;
            }
            sql = string.Format(sql, cardNO);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            if (Reader.Read())
            {
                try
                {
                    if (!Reader.IsDBNull(0))
                    {
                        PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                        PatientInfo.ID = PatientInfo.PID.CardNO;
                    }
                    if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                    if (!Reader.IsDBNull(2)) PatientInfo.SpellCode = Reader[2].ToString(); //ƴ����
                    if (!Reader.IsDBNull(3)) PatientInfo.WBCode = Reader[3].ToString(); //���
                    if (!Reader.IsDBNull(4)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //��������
                    if (!Reader.IsDBNull(5)) PatientInfo.Sex.ID = Reader[5].ToString(); //�Ա�
                    if (!Reader.IsDBNull(6)) PatientInfo.IDCard = Reader[6].ToString(); //���֤��
                    if (!Reader.IsDBNull(7)) PatientInfo.BloodType.ID = Reader[7].ToString(); //Ѫ��
                    if (!Reader.IsDBNull(8)) PatientInfo.Profession.ID = Reader[8].ToString(); //ְҵ
                    if (!Reader.IsDBNull(9)) PatientInfo.CompanyName = Reader[9].ToString(); //������λ
                    if (!Reader.IsDBNull(10)) PatientInfo.PhoneBusiness = Reader[10].ToString(); //��λ�绰
                    if (!Reader.IsDBNull(11)) PatientInfo.BusinessZip = Reader[11].ToString(); //��λ�ʱ�
                    if (!Reader.IsDBNull(12)) PatientInfo.AddressHome = Reader[12].ToString(); //���ڻ��ͥ����
                    if (!Reader.IsDBNull(13)) PatientInfo.PhoneHome = Reader[13].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(14)) PatientInfo.HomeZip = Reader[14].ToString(); //���ڻ��ͥ��������
                    if (!Reader.IsDBNull(15)) PatientInfo.DIST = Reader[15].ToString(); //����
                    if (!Reader.IsDBNull(16)) PatientInfo.Nationality.ID = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) PatientInfo.Kin.Name = Reader[17].ToString(); //��ϵ������
                    if (!Reader.IsDBNull(18)) PatientInfo.Kin.RelationPhone = Reader[18].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationAddress = Reader[19].ToString(); //��ϵ��סַ
                    if (!Reader.IsDBNull(20)) PatientInfo.Kin.Relation.ID = Reader[20].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(21)) PatientInfo.MaritalStatus.ID = Reader[21].ToString(); //����״��
                    if (!Reader.IsDBNull(22)) PatientInfo.Country.ID = Reader[22].ToString(); //����
                    if (!Reader.IsDBNull(23)) PatientInfo.Pact.PayKind.ID = Reader[23].ToString(); //�������
                    if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.Name = Reader[24].ToString(); //�����������
                    if (!Reader.IsDBNull(25)) PatientInfo.Pact.ID = Reader[25].ToString(); //��ͬ����
                    if (!Reader.IsDBNull(26)) PatientInfo.Pact.Name = Reader[26].ToString(); //��ͬ��λ����
                    if (!Reader.IsDBNull(27)) PatientInfo.SSN = Reader[27].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(28)) PatientInfo.AreaCode = Reader[28].ToString(); //����
                    if (!Reader.IsDBNull(29)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //ҽ�Ʒ���
                    if (!Reader.IsDBNull(30)) PatientInfo.Card.ICCard.ID = Reader[30].ToString(); //���Ժ�
                    if (!Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //ҩ�����
                    if (!Reader.IsDBNull(32)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //��Ҫ����
                    if (!Reader.IsDBNull(33)) PatientInfo.Card.NewPassword = Reader[33].ToString(); //�ʻ�����
                    if (!Reader.IsDBNull(34)) PatientInfo.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //�ʻ��ܶ�
                    if (!Reader.IsDBNull(35)) PatientInfo.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //�����ʻ����
                    //					if (!this.Reader.IsDBNull(36)) PatientInfo=this.Reader[36].ToString();//�����������
                    //					if (!this.Reader.IsDBNull(37)) PatientInfo=this.Reader[37].ToString();//Ƿ�Ѵ���
                    //					if (!this.Reader.IsDBNull(38)) PatientInfo=this.Reader[38].ToString();//Ƿ�ѽ��
                    //					if (!this.Reader.IsDBNull(39)) PatientInfo=this.Reader[39].ToString();//סԺ��Դ
                    //					if (!this.Reader.IsDBNull(40)) PatientInfo=this.Reader[40].ToString();//���סԺ����
                    //					if (!this.Reader.IsDBNull(41)) PatientInfo=this.Reader[41].ToString();//סԺ����
                    //					if (!this.Reader.IsDBNull(42)) PatientInfo=this.Reader[42].ToString();//�����Ժ����
                    //					if (!this.Reader.IsDBNull(43)) PatientInfo=this.Reader[43].ToString();//��������
                    //					if (!this.Reader.IsDBNull(44)) PatientInfo=this.Reader[44].ToString();//����Һ�����
                    //					if (!this.Reader.IsDBNull(45)) PatientInfo=this.Reader[45].ToString();//ΥԼ����
                    //					if (!this.Reader.IsDBNull(46)) PatientInfo=this.Reader[46].ToString();//��������
                    if (!Reader.IsDBNull(47)) PatientInfo.Memo = Reader[47].ToString(); //��ע
                    if (!Reader.IsDBNull(48)) PatientInfo.User03 = Reader[48].ToString(); //����Ա
                    if (!Reader.IsDBNull(49)) PatientInfo.User02 = Reader[49].ToString(); //��������
                    if (!Reader.IsDBNull(50)) PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) PatientInfo.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) PatientInfo.IDCardType.ID = Reader[52].ToString();//֤������
                    if (!Reader.IsDBNull(53)) PatientInfo.VipFlag = NConvert.ToBoolean(Reader[53]);//vip��ʶ
                    if (!Reader.IsDBNull(54)) PatientInfo.MatherName = Reader[54].ToString();//ĸ������
                    if (!Reader.IsDBNull(55)) PatientInfo.IsTreatment = NConvert.ToBoolean(Reader[55]);//�Ƿ���
                    if (!Reader.IsDBNull(56)) PatientInfo.PID.CaseNO = Reader[56].ToString();//������
                    if (PatientInfo.IsEncrypt && PatientInfo.NormalName != string.Empty)
                    {
                        PatientInfo.DecryptName = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(PatientInfo.NormalName);
                    }
                    if (!Reader.IsDBNull(57)) PatientInfo.Insurance.ID = Reader[57].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(58)) PatientInfo.Insurance.Name = Reader[58].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(59)) PatientInfo.AddressHomeDoorNo = Reader[59].ToString(); //��ͥסַ���ƺ�
                    if (!Reader.IsDBNull(60)) PatientInfo.Kin.RelationDoorNo = Reader[60].ToString(); //��ϵ�˵�ַ���ƺ�
                    if (!Reader.IsDBNull(61)) PatientInfo.Email = Reader[61].ToString(); //email
                    if (!Reader.IsDBNull(62)) PatientInfo.User01 = Reader[62].ToString(); //��סַ
                    if (!Reader.IsDBNull(63)) PatientInfo.FamilyCode = Reader[63].ToString(); //��ͥ��
                    if (!Reader.IsDBNull(64)) PatientInfo.OtherCardNo = Reader[64].ToString(); //�ͷ�רԱ����
                    if (!Reader.IsDBNull(65)) PatientInfo.ServiceInfo.ID = Reader[65].ToString(); //�ͷ�רԱ����
                    if (!Reader.IsDBNull(66)) PatientInfo.ServiceInfo.Name = Reader[66].ToString(); //�ͷ�רԱ����
                    if (!Reader.IsDBNull(67)) PatientInfo.PatientSourceInfo.ID = Reader[67].ToString(); //������Դ����
                    if (!Reader.IsDBNull(68)) PatientInfo.ReferralPerson = Reader[68].ToString(); //ת����
                    if (!Reader.IsDBNull(69)) PatientInfo.ChannelInfo.ID = Reader[69].ToString(); //������������

                    if (!Reader.IsDBNull(70)) PatientInfo.FamilyName = Reader[70].ToString(); //��ͥ����
                    //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ
                    if (!Reader.IsDBNull(71)) PatientInfo.ChildFlag = Reader[71].ToString(); //��ͯ���

                    //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                    if (!Reader.IsDBNull(72)) PatientInfo.CrmID = Reader[72].ToString(); //crmID
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();

            return PatientInfo;
        }

        //{971E891B-4E05-42c9-8C7A-98E13996AA17}
        /// <summary>
        /// ���߻�����Ϣ��ѯ  com_patientinfo
        /// </summary>
        /// <param name="idNO">���֤��</param>
        /// <returns></returns>
        public PatientInfo QueryComPatientInfoByIDNO(string IDNO)
        {
            PatientInfo PatientInfo = new PatientInfo();
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientInfoQuerybyIDNO", ref sql) == -1)
            #region SQL
            /*SELECT card_no,
						   name,   --����
								   spell_code,   --ƴ����

								   wb_code,   --���
								   birthday,   --��������
								   sex_code,   --�Ա�
								   idenno,   --���֤��
								   blood_code,   --Ѫ��

								   prof_code,   --ְҵ
								   work_home,   --������λ
								   work_tel,   --��λ�绰
								   work_zip,   --��λ�ʱ�
								   home,   --���ڻ��ͥ����

								   home_tel,   --��ͥ�绰
								   home_zip,   --���ڻ��ͥ��������

								   district,   --����
								   nation_code,   --����
								   linkman_name,   --��ϵ������

								   linkman_tel,   --��ϵ�˵绰

								   linkman_add,   --��ϵ��סַ
								   rela_code,   --��ϵ�˹�ϵ

								   mari,   --����״��
								   coun_code,   --����
								   paykind_code,   --�������
								   paykind_name,   --�����������
								   pact_code,   --��ͬ����
								   pact_name,   --��ͬ��λ����
								   mcard_no,   --ҽ��֤��
								   area_code,   --������

								   framt,   --ҽ�Ʒ���
								   ic_cardno,   --���Ժ�

								   anaphy_flag,   --ҩ�����
								   hepatitis_flag,   --��Ҫ����
								   act_code,   --�ʻ�����
								   act_amt,   --�ʻ��ܶ�
								   lact_sum,   --�����ʻ����
								   lbank_sum,   --�����������
								   arrear_times,   --Ƿ�Ѵ���
								   arrear_sum,   --Ƿ�ѽ��
								   inhos_source,   --סԺ��Դ
								   lihos_date,   --���סԺ����

								   inhos_times,   --סԺ����
								   louthos_date,   --�����Ժ����

								   fir_see_date,   --��������
								   lreg_date,   --����Һ�����

								   disoby_cnt,   --ΥԼ����
								   end_date,   --��������
								   mark,   --��ע
								   oper_code,   --����Ա

								   oper_date    --��������
							  FROM com_patientinfo   --���˻�����Ϣ��
							 WHERE PARENT_CODE='[��������]'  and 
								   CURRENT_CODE='[��������]' and 
								   card_no='{0}'
								   */
            #endregion
            {
                return null;
            }
            sql = string.Format(sql, IDNO);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            if (Reader.Read())
            {
                try
                {
                    if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                    if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                    if (!Reader.IsDBNull(2)) PatientInfo.SpellCode = Reader[2].ToString(); //ƴ����
                    if (!Reader.IsDBNull(3)) PatientInfo.WBCode = Reader[3].ToString(); //���
                    if (!Reader.IsDBNull(4)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //��������
                    if (!Reader.IsDBNull(5)) PatientInfo.Sex.ID = Reader[5].ToString(); //�Ա�
                    if (!Reader.IsDBNull(6)) PatientInfo.IDCard = Reader[6].ToString(); //���֤��
                    if (!Reader.IsDBNull(7)) PatientInfo.BloodType.ID = Reader[7].ToString(); //Ѫ��
                    if (!Reader.IsDBNull(8)) PatientInfo.Profession.ID = Reader[8].ToString(); //ְҵ
                    if (!Reader.IsDBNull(9)) PatientInfo.CompanyName = Reader[9].ToString(); //������λ
                    if (!Reader.IsDBNull(10)) PatientInfo.PhoneBusiness = Reader[10].ToString(); //��λ�绰
                    if (!Reader.IsDBNull(11)) PatientInfo.BusinessZip = Reader[11].ToString(); //��λ�ʱ�
                    if (!Reader.IsDBNull(12)) PatientInfo.AddressHome = Reader[12].ToString(); //���ڻ��ͥ����
                    if (!Reader.IsDBNull(13)) PatientInfo.PhoneHome = Reader[13].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(14)) PatientInfo.HomeZip = Reader[14].ToString(); //���ڻ��ͥ��������
                    if (!Reader.IsDBNull(15)) PatientInfo.DIST = Reader[15].ToString(); //����
                    if (!Reader.IsDBNull(16)) PatientInfo.Nationality.ID = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) PatientInfo.Kin.Name = Reader[17].ToString(); //��ϵ������
                    if (!Reader.IsDBNull(18)) PatientInfo.Kin.RelationPhone = Reader[18].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationAddress = Reader[19].ToString(); //��ϵ��סַ
                    if (!Reader.IsDBNull(20)) PatientInfo.Kin.Relation.ID = Reader[20].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(21)) PatientInfo.MaritalStatus.ID = Reader[21].ToString(); //����״��
                    if (!Reader.IsDBNull(22)) PatientInfo.Country.ID = Reader[22].ToString(); //����
                    if (!Reader.IsDBNull(23)) PatientInfo.Pact.PayKind.ID = Reader[23].ToString(); //�������
                    if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.Name = Reader[24].ToString(); //�����������
                    if (!Reader.IsDBNull(25)) PatientInfo.Pact.ID = Reader[25].ToString(); //��ͬ����
                    if (!Reader.IsDBNull(26)) PatientInfo.Pact.Name = Reader[26].ToString(); //��ͬ��λ����
                    if (!Reader.IsDBNull(27)) PatientInfo.SSN = Reader[27].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(28)) PatientInfo.AreaCode = Reader[28].ToString(); //����
                    if (!Reader.IsDBNull(29)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //ҽ�Ʒ���
                    if (!Reader.IsDBNull(30)) PatientInfo.Card.ICCard.ID = Reader[30].ToString(); //���Ժ�
                    if (!Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //ҩ�����
                    if (!Reader.IsDBNull(32)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //��Ҫ����
                    if (!Reader.IsDBNull(33)) PatientInfo.Card.NewPassword = Reader[33].ToString(); //�ʻ�����
                    if (!Reader.IsDBNull(34)) PatientInfo.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //�ʻ��ܶ�
                    if (!Reader.IsDBNull(35)) PatientInfo.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //�����ʻ����
                    //					if (!this.Reader.IsDBNull(36)) PatientInfo=this.Reader[36].ToString();//�����������
                    //					if (!this.Reader.IsDBNull(37)) PatientInfo=this.Reader[37].ToString();//Ƿ�Ѵ���
                    //					if (!this.Reader.IsDBNull(38)) PatientInfo=this.Reader[38].ToString();//Ƿ�ѽ��
                    //					if (!this.Reader.IsDBNull(39)) PatientInfo=this.Reader[39].ToString();//סԺ��Դ
                    //					if (!this.Reader.IsDBNull(40)) PatientInfo=this.Reader[40].ToString();//���סԺ����
                    //					if (!this.Reader.IsDBNull(41)) PatientInfo=this.Reader[41].ToString();//סԺ����
                    //					if (!this.Reader.IsDBNull(42)) PatientInfo=this.Reader[42].ToString();//�����Ժ����
                    //					if (!this.Reader.IsDBNull(43)) PatientInfo=this.Reader[43].ToString();//��������
                    //					if (!this.Reader.IsDBNull(44)) PatientInfo=this.Reader[44].ToString();//����Һ�����
                    //					if (!this.Reader.IsDBNull(45)) PatientInfo=this.Reader[45].ToString();//ΥԼ����
                    //					if (!this.Reader.IsDBNull(46)) PatientInfo=this.Reader[46].ToString();//��������
                    if (!Reader.IsDBNull(47)) PatientInfo.Memo = Reader[47].ToString(); //��ע
                    if (!Reader.IsDBNull(48)) PatientInfo.User01 = Reader[48].ToString(); //����Ա
                    if (!Reader.IsDBNull(49)) PatientInfo.User02 = Reader[49].ToString(); //��������
                    if (!Reader.IsDBNull(50)) PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) PatientInfo.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) PatientInfo.IDCardType.ID = Reader[52].ToString();//֤������
                    if (!Reader.IsDBNull(53)) PatientInfo.VipFlag = NConvert.ToBoolean(Reader[53]);//vip��ʶ
                    if (!Reader.IsDBNull(54)) PatientInfo.MatherName = Reader[54].ToString();//ĸ������
                    if (!Reader.IsDBNull(55)) PatientInfo.IsTreatment = NConvert.ToBoolean(Reader[55]);//�Ƿ���
                    if (!Reader.IsDBNull(56)) PatientInfo.PID.CaseNO = Reader[56].ToString();//������
                    if (PatientInfo.IsEncrypt && PatientInfo.NormalName != string.Empty)
                    {
                        PatientInfo.DecryptName = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(PatientInfo.NormalName);
                    }
                    if (!Reader.IsDBNull(57)) PatientInfo.Insurance.ID = Reader[57].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(58)) PatientInfo.Insurance.Name = Reader[58].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(59)) PatientInfo.Kin.RelationDoorNo = Reader[59].ToString(); //��ϵ�˵�ַ���ƺ�
                    if (!Reader.IsDBNull(60)) PatientInfo.AddressHomeDoorNo = Reader[60].ToString(); //��ͥסַ���ƺ�
                    if (!Reader.IsDBNull(61)) PatientInfo.Email = Reader[61].ToString(); //email
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();

            return PatientInfo;
        }

        /// <summary>
        /// ���߻�����Ϣ��ѯ  com_patientinfo
        /// </summary>
        /// <param name="IDNO">���֤��</param>
        /// <returns></returns>
        public ArrayList QueryComPatientInfoListByIDNO(string IDNO)
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientInfoQuerybyIDNO", ref sql) == -1)
            #region SQL
            /*SELECT card_no,
						   name,   --����
								   spell_code,   --ƴ����

								   wb_code,   --���
								   birthday,   --��������
								   sex_code,   --�Ա�
								   idenno,   --���֤��
								   blood_code,   --Ѫ��

								   prof_code,   --ְҵ
								   work_home,   --������λ
								   work_tel,   --��λ�绰
								   work_zip,   --��λ�ʱ�
								   home,   --���ڻ��ͥ����

								   home_tel,   --��ͥ�绰
								   home_zip,   --���ڻ��ͥ��������

								   district,   --����
								   nation_code,   --����
								   linkman_name,   --��ϵ������

								   linkman_tel,   --��ϵ�˵绰

								   linkman_add,   --��ϵ��סַ
								   rela_code,   --��ϵ�˹�ϵ

								   mari,   --����״��
								   coun_code,   --����
								   paykind_code,   --�������
								   paykind_name,   --�����������
								   pact_code,   --��ͬ����
								   pact_name,   --��ͬ��λ����
								   mcard_no,   --ҽ��֤��
								   area_code,   --������

								   framt,   --ҽ�Ʒ���
								   ic_cardno,   --���Ժ�

								   anaphy_flag,   --ҩ�����
								   hepatitis_flag,   --��Ҫ����
								   act_code,   --�ʻ�����
								   act_amt,   --�ʻ��ܶ�
								   lact_sum,   --�����ʻ����
								   lbank_sum,   --�����������
								   arrear_times,   --Ƿ�Ѵ���
								   arrear_sum,   --Ƿ�ѽ��
								   inhos_source,   --סԺ��Դ
								   lihos_date,   --���סԺ����

								   inhos_times,   --סԺ����
								   louthos_date,   --�����Ժ����

								   fir_see_date,   --��������
								   lreg_date,   --����Һ�����

								   disoby_cnt,   --ΥԼ����
								   end_date,   --��������
								   mark,   --��ע
								   oper_code,   --����Ա

								   oper_date    --��������
							  FROM com_patientinfo   --���˻�����Ϣ��
							 WHERE PARENT_CODE='[��������]'  and 
								   CURRENT_CODE='[��������]' and 
								   card_no='{0}'
								   */
            #endregion
            {
                return null;
            }
            sql = string.Format(sql, IDNO);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }
            PatientInfo PatientInfo = null;
            ArrayList alPatient = new ArrayList();
            while (Reader.Read())
            {
                try
                {
                    PatientInfo = new PatientInfo();
                    if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                    if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                    if (!Reader.IsDBNull(2)) PatientInfo.SpellCode = Reader[2].ToString(); //ƴ����
                    if (!Reader.IsDBNull(3)) PatientInfo.WBCode = Reader[3].ToString(); //���
                    if (!Reader.IsDBNull(4)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //��������
                    if (!Reader.IsDBNull(5)) PatientInfo.Sex.ID = Reader[5].ToString(); //�Ա�
                    if (!Reader.IsDBNull(6)) PatientInfo.IDCard = Reader[6].ToString(); //���֤��
                    if (!Reader.IsDBNull(7)) PatientInfo.BloodType.ID = Reader[7].ToString(); //Ѫ��
                    if (!Reader.IsDBNull(8)) PatientInfo.Profession.ID = Reader[8].ToString(); //ְҵ
                    if (!Reader.IsDBNull(9)) PatientInfo.CompanyName = Reader[9].ToString(); //������λ
                    if (!Reader.IsDBNull(10)) PatientInfo.PhoneBusiness = Reader[10].ToString(); //��λ�绰
                    if (!Reader.IsDBNull(11)) PatientInfo.BusinessZip = Reader[11].ToString(); //��λ�ʱ�
                    if (!Reader.IsDBNull(12)) PatientInfo.AddressHome = Reader[12].ToString(); //���ڻ��ͥ����
                    if (!Reader.IsDBNull(13)) PatientInfo.PhoneHome = Reader[13].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(14)) PatientInfo.HomeZip = Reader[14].ToString(); //���ڻ��ͥ��������
                    if (!Reader.IsDBNull(15)) PatientInfo.DIST = Reader[15].ToString(); //����
                    if (!Reader.IsDBNull(16)) PatientInfo.Nationality.ID = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) PatientInfo.Kin.Name = Reader[17].ToString(); //��ϵ������
                    if (!Reader.IsDBNull(18)) PatientInfo.Kin.RelationPhone = Reader[18].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationAddress = Reader[19].ToString(); //��ϵ��סַ
                    if (!Reader.IsDBNull(20)) PatientInfo.Kin.Relation.ID = Reader[20].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(21)) PatientInfo.MaritalStatus.ID = Reader[21].ToString(); //����״��
                    if (!Reader.IsDBNull(22)) PatientInfo.Country.ID = Reader[22].ToString(); //����
                    if (!Reader.IsDBNull(23)) PatientInfo.Pact.PayKind.ID = Reader[23].ToString(); //�������
                    if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.Name = Reader[24].ToString(); //�����������
                    if (!Reader.IsDBNull(25)) PatientInfo.Pact.ID = Reader[25].ToString(); //��ͬ����
                    if (!Reader.IsDBNull(26)) PatientInfo.Pact.Name = Reader[26].ToString(); //��ͬ��λ����
                    if (!Reader.IsDBNull(27)) PatientInfo.SSN = Reader[27].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(28)) PatientInfo.AreaCode = Reader[28].ToString(); //����
                    if (!Reader.IsDBNull(29)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //ҽ�Ʒ���
                    if (!Reader.IsDBNull(30)) PatientInfo.Card.ICCard.ID = Reader[30].ToString(); //���Ժ�
                    if (!Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //ҩ�����
                    if (!Reader.IsDBNull(32)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //��Ҫ����
                    if (!Reader.IsDBNull(33)) PatientInfo.Card.NewPassword = Reader[33].ToString(); //�ʻ�����
                    if (!Reader.IsDBNull(34)) PatientInfo.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //�ʻ��ܶ�
                    if (!Reader.IsDBNull(35)) PatientInfo.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //�����ʻ����
                    //					if (!this.Reader.IsDBNull(36)) PatientInfo=this.Reader[36].ToString();//�����������
                    //					if (!this.Reader.IsDBNull(37)) PatientInfo=this.Reader[37].ToString();//Ƿ�Ѵ���
                    //					if (!this.Reader.IsDBNull(38)) PatientInfo=this.Reader[38].ToString();//Ƿ�ѽ��
                    //					if (!this.Reader.IsDBNull(39)) PatientInfo=this.Reader[39].ToString();//סԺ��Դ
                    //					if (!this.Reader.IsDBNull(40)) PatientInfo=this.Reader[40].ToString();//���סԺ����
                    //					if (!this.Reader.IsDBNull(41)) PatientInfo=this.Reader[41].ToString();//סԺ����
                    //					if (!this.Reader.IsDBNull(42)) PatientInfo=this.Reader[42].ToString();//�����Ժ����
                    //					if (!this.Reader.IsDBNull(43)) PatientInfo=this.Reader[43].ToString();//��������
                    //					if (!this.Reader.IsDBNull(44)) PatientInfo=this.Reader[44].ToString();//����Һ�����
                    //					if (!this.Reader.IsDBNull(45)) PatientInfo=this.Reader[45].ToString();//ΥԼ����
                    //					if (!this.Reader.IsDBNull(46)) PatientInfo=this.Reader[46].ToString();//��������
                    if (!Reader.IsDBNull(47)) PatientInfo.Memo = Reader[47].ToString(); //��ע
                    if (!Reader.IsDBNull(48)) PatientInfo.User01 = Reader[48].ToString(); //����Ա
                    if (!Reader.IsDBNull(49)) PatientInfo.User02 = Reader[49].ToString(); //��������
                    if (!Reader.IsDBNull(50)) PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) PatientInfo.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) PatientInfo.IDCardType.ID = Reader[52].ToString();//֤������
                    if (!Reader.IsDBNull(53)) PatientInfo.VipFlag = NConvert.ToBoolean(Reader[53]);//vip��ʶ
                    if (!Reader.IsDBNull(54)) PatientInfo.MatherName = Reader[54].ToString();//ĸ������
                    if (!Reader.IsDBNull(55)) PatientInfo.IsTreatment = NConvert.ToBoolean(Reader[55]);//�Ƿ���
                    if (!Reader.IsDBNull(56)) PatientInfo.PID.CaseNO = Reader[56].ToString();//������
                    if (PatientInfo.IsEncrypt && PatientInfo.NormalName != string.Empty)
                    {
                        PatientInfo.DecryptName = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(PatientInfo.NormalName);
                    }
                    if (!Reader.IsDBNull(57)) PatientInfo.Insurance.ID = Reader[57].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(58)) PatientInfo.Insurance.Name = Reader[58].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(59)) PatientInfo.Kin.RelationDoorNo = Reader[59].ToString(); //��ϵ�˵�ַ���ƺ�
                    if (!Reader.IsDBNull(60)) PatientInfo.AddressHomeDoorNo = Reader[60].ToString(); //��ͥסַ���ƺ�
                    if (!Reader.IsDBNull(61)) PatientInfo.Email = Reader[61].ToString(); //email
                    alPatient.Add(PatientInfo);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();

            return alPatient;
        }

        /// <summary>
        /// ����ҽ����Ų�ѯ���߻�����Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public PatientInfo QueryComPatientInfoByMcardNO(string cardNO)
        {
            PatientInfo PatientInfo = new PatientInfo();
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientInfoQuery.96", ref sql) == -1)
            {
                return null;
            }
            sql = string.Format(sql, cardNO);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            if (Reader.Read())
            {
                try
                {
                    if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                    if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                    if (!Reader.IsDBNull(2)) PatientInfo.SpellCode = Reader[2].ToString(); //ƴ����
                    if (!Reader.IsDBNull(3)) PatientInfo.WBCode = Reader[3].ToString(); //���
                    if (!Reader.IsDBNull(4)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //��������
                    if (!Reader.IsDBNull(5)) PatientInfo.Sex.ID = Reader[5].ToString(); //�Ա�
                    if (!Reader.IsDBNull(6)) PatientInfo.IDCard = Reader[6].ToString(); //���֤��
                    if (!Reader.IsDBNull(7)) PatientInfo.BloodType.ID = Reader[7].ToString(); //Ѫ��
                    if (!Reader.IsDBNull(8)) PatientInfo.Profession.ID = Reader[8].ToString(); //ְҵ
                    if (!Reader.IsDBNull(9)) PatientInfo.CompanyName = Reader[9].ToString(); //������λ
                    if (!Reader.IsDBNull(10)) PatientInfo.PhoneBusiness = Reader[10].ToString(); //��λ�绰
                    if (!Reader.IsDBNull(11)) PatientInfo.BusinessZip = Reader[11].ToString(); //��λ�ʱ�
                    if (!Reader.IsDBNull(12)) PatientInfo.AddressHome = Reader[12].ToString(); //���ڻ��ͥ����
                    if (!Reader.IsDBNull(13)) PatientInfo.PhoneHome = Reader[13].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(14)) PatientInfo.HomeZip = Reader[14].ToString(); //���ڻ��ͥ��������
                    if (!Reader.IsDBNull(15)) PatientInfo.DIST = Reader[15].ToString(); //����
                    if (!Reader.IsDBNull(16)) PatientInfo.Nationality.ID = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) PatientInfo.Kin.Name = Reader[17].ToString(); //��ϵ������
                    if (!Reader.IsDBNull(18)) PatientInfo.Kin.RelationPhone = Reader[18].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationAddress = Reader[19].ToString(); //��ϵ��סַ
                    if (!Reader.IsDBNull(20)) PatientInfo.Kin.Relation.ID = Reader[20].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(21)) PatientInfo.MaritalStatus.ID = Reader[21].ToString(); //����״��
                    if (!Reader.IsDBNull(22)) PatientInfo.Country.ID = Reader[22].ToString(); //����
                    if (!Reader.IsDBNull(23)) PatientInfo.Pact.PayKind.ID = Reader[23].ToString(); //�������
                    if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.Name = Reader[24].ToString(); //�����������
                    if (!Reader.IsDBNull(25)) PatientInfo.Pact.ID = Reader[25].ToString(); //��ͬ����
                    if (!Reader.IsDBNull(26)) PatientInfo.Pact.Name = Reader[26].ToString(); //��ͬ��λ����
                    if (!Reader.IsDBNull(27)) PatientInfo.SSN = Reader[27].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(28)) PatientInfo.AreaCode = Reader[28].ToString(); //����
                    if (!Reader.IsDBNull(29)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //ҽ�Ʒ���
                    if (!Reader.IsDBNull(30)) PatientInfo.Card.ICCard.ID = Reader[30].ToString(); //���Ժ�
                    if (!Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //ҩ�����
                    if (!Reader.IsDBNull(32)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //��Ҫ����
                    if (!Reader.IsDBNull(33)) PatientInfo.Card.NewPassword = Reader[33].ToString(); //�ʻ�����
                    if (!Reader.IsDBNull(34)) PatientInfo.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //�ʻ��ܶ�
                    if (!Reader.IsDBNull(35)) PatientInfo.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //�����ʻ����
                    //					if (!this.Reader.IsDBNull(36)) PatientInfo=this.Reader[36].ToString();//�����������
                    //					if (!this.Reader.IsDBNull(37)) PatientInfo=this.Reader[37].ToString();//Ƿ�Ѵ���
                    //					if (!this.Reader.IsDBNull(38)) PatientInfo=this.Reader[38].ToString();//Ƿ�ѽ��
                    //					if (!this.Reader.IsDBNull(39)) PatientInfo=this.Reader[39].ToString();//סԺ��Դ
                    //					if (!this.Reader.IsDBNull(40)) PatientInfo=this.Reader[40].ToString();//���סԺ����
                    //					if (!this.Reader.IsDBNull(41)) PatientInfo=this.Reader[41].ToString();//סԺ����
                    //					if (!this.Reader.IsDBNull(42)) PatientInfo=this.Reader[42].ToString();//�����Ժ����
                    //					if (!this.Reader.IsDBNull(43)) PatientInfo=this.Reader[43].ToString();//��������
                    //					if (!this.Reader.IsDBNull(44)) PatientInfo=this.Reader[44].ToString();//����Һ�����
                    //					if (!this.Reader.IsDBNull(45)) PatientInfo=this.Reader[45].ToString();//ΥԼ����
                    //					if (!this.Reader.IsDBNull(46)) PatientInfo=this.Reader[46].ToString();//��������
                    if (!Reader.IsDBNull(47)) PatientInfo.Memo = Reader[47].ToString(); //��ע
                    if (!Reader.IsDBNull(48)) PatientInfo.User01 = Reader[48].ToString(); //����Ա
                    if (!Reader.IsDBNull(49)) PatientInfo.User02 = Reader[49].ToString(); //��������
                    if (!Reader.IsDBNull(50)) PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) PatientInfo.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) PatientInfo.IDCardType.ID = Reader[52].ToString();//֤������
                    if (!Reader.IsDBNull(53)) PatientInfo.VipFlag = NConvert.ToBoolean(Reader[53]);//vip��ʶ
                    if (!Reader.IsDBNull(54)) PatientInfo.MatherName = Reader[54].ToString();//ĸ������
                    if (!Reader.IsDBNull(55)) PatientInfo.IsTreatment = NConvert.ToBoolean(Reader[55]);//�Ƿ���
                    if (!Reader.IsDBNull(56)) PatientInfo.PID.CaseNO = Reader[56].ToString();//������
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();

            return PatientInfo;
        }

        /// <summary>
        /// ���߻�����Ϣ��ѯ  {F55EE363-24DB-4a01-B540-49761A5ADBC6}
        /// </summary>
        /// <param name="cardNO">����</param>
        /// <returns></returns>
        public PatientInfo QueryComPatientInfoByCRMID(string CRMID)
        {
            PatientInfo PatientInfo = new PatientInfo();
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientInfoQuery.CRMID", ref sql) == -1)
            #region SQL
            /*SELECT card_no,
						   name,   --����
								   spell_code,   --ƴ����

								   wb_code,   --���
								   birthday,   --��������
								   sex_code,   --�Ա�
								   idenno,   --���֤��
								   blood_code,   --Ѫ��

								   prof_code,   --ְҵ
								   work_home,   --������λ
								   work_tel,   --��λ�绰
								   work_zip,   --��λ�ʱ�
								   home,   --���ڻ��ͥ����

								   home_tel,   --��ͥ�绰
								   home_zip,   --���ڻ��ͥ��������

								   district,   --����
								   nation_code,   --����
								   linkman_name,   --��ϵ������

								   linkman_tel,   --��ϵ�˵绰

								   linkman_add,   --��ϵ��סַ
								   rela_code,   --��ϵ�˹�ϵ

								   mari,   --����״��
								   coun_code,   --����
								   paykind_code,   --�������
								   paykind_name,   --�����������
								   pact_code,   --��ͬ����
								   pact_name,   --��ͬ��λ����
								   mcard_no,   --ҽ��֤��
								   area_code,   --������

								   framt,   --ҽ�Ʒ���
								   ic_cardno,   --���Ժ�

								   anaphy_flag,   --ҩ�����
								   hepatitis_flag,   --��Ҫ����
								   act_code,   --�ʻ�����
								   act_amt,   --�ʻ��ܶ�
								   lact_sum,   --�����ʻ����
								   lbank_sum,   --�����������
								   arrear_times,   --Ƿ�Ѵ���
								   arrear_sum,   --Ƿ�ѽ��
								   inhos_source,   --סԺ��Դ
								   lihos_date,   --���סԺ����

								   inhos_times,   --סԺ����
								   louthos_date,   --�����Ժ����

								   fir_see_date,   --��������
								   lreg_date,   --����Һ�����

								   disoby_cnt,   --ΥԼ����
								   end_date,   --��������
								   mark,   --��ע
								   oper_code,   --����Ա

								   oper_date    --��������
							  FROM com_patientinfo   --���˻�����Ϣ��
							 WHERE PARENT_CODE='[��������]'  and 
								   CURRENT_CODE='[��������]' and 
								   card_no='{0}'
								   */
            #endregion
            {
                return null;
            }
            sql = string.Format(sql, CRMID);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            if (Reader.Read())
            {
                try
                {
                    if (!Reader.IsDBNull(0))
                    {
                        PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                        PatientInfo.ID = PatientInfo.PID.CardNO;
                    }
                    if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                    if (!Reader.IsDBNull(2)) PatientInfo.SpellCode = Reader[2].ToString(); //ƴ����
                    if (!Reader.IsDBNull(3)) PatientInfo.WBCode = Reader[3].ToString(); //���
                    if (!Reader.IsDBNull(4)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //��������
                    if (!Reader.IsDBNull(5)) PatientInfo.Sex.ID = Reader[5].ToString(); //�Ա�
                    if (!Reader.IsDBNull(6)) PatientInfo.IDCard = Reader[6].ToString(); //���֤��
                    if (!Reader.IsDBNull(7)) PatientInfo.BloodType.ID = Reader[7].ToString(); //Ѫ��
                    if (!Reader.IsDBNull(8)) PatientInfo.Profession.ID = Reader[8].ToString(); //ְҵ
                    if (!Reader.IsDBNull(9)) PatientInfo.CompanyName = Reader[9].ToString(); //������λ
                    if (!Reader.IsDBNull(10)) PatientInfo.PhoneBusiness = Reader[10].ToString(); //��λ�绰
                    if (!Reader.IsDBNull(11)) PatientInfo.BusinessZip = Reader[11].ToString(); //��λ�ʱ�
                    if (!Reader.IsDBNull(12)) PatientInfo.AddressHome = Reader[12].ToString(); //���ڻ��ͥ����
                    if (!Reader.IsDBNull(13)) PatientInfo.PhoneHome = Reader[13].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(14)) PatientInfo.HomeZip = Reader[14].ToString(); //���ڻ��ͥ��������
                    if (!Reader.IsDBNull(15)) PatientInfo.DIST = Reader[15].ToString(); //����
                    if (!Reader.IsDBNull(16)) PatientInfo.Nationality.ID = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) PatientInfo.Kin.Name = Reader[17].ToString(); //��ϵ������
                    if (!Reader.IsDBNull(18)) PatientInfo.Kin.RelationPhone = Reader[18].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationAddress = Reader[19].ToString(); //��ϵ��סַ
                    if (!Reader.IsDBNull(20)) PatientInfo.Kin.Relation.ID = Reader[20].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(21)) PatientInfo.MaritalStatus.ID = Reader[21].ToString(); //����״��
                    if (!Reader.IsDBNull(22)) PatientInfo.Country.ID = Reader[22].ToString(); //����
                    if (!Reader.IsDBNull(23)) PatientInfo.Pact.PayKind.ID = Reader[23].ToString(); //�������
                    if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.Name = Reader[24].ToString(); //�����������
                    if (!Reader.IsDBNull(25)) PatientInfo.Pact.ID = Reader[25].ToString(); //��ͬ����
                    if (!Reader.IsDBNull(26)) PatientInfo.Pact.Name = Reader[26].ToString(); //��ͬ��λ����
                    if (!Reader.IsDBNull(27)) PatientInfo.SSN = Reader[27].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(28)) PatientInfo.AreaCode = Reader[28].ToString(); //����
                    if (!Reader.IsDBNull(29)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //ҽ�Ʒ���
                    if (!Reader.IsDBNull(30)) PatientInfo.Card.ICCard.ID = Reader[30].ToString(); //���Ժ�
                    if (!Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //ҩ�����
                    if (!Reader.IsDBNull(32)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //��Ҫ����
                    if (!Reader.IsDBNull(33)) PatientInfo.Card.NewPassword = Reader[33].ToString(); //�ʻ�����
                    if (!Reader.IsDBNull(34)) PatientInfo.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //�ʻ��ܶ�
                    if (!Reader.IsDBNull(35)) PatientInfo.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //�����ʻ����
                    //					if (!this.Reader.IsDBNull(36)) PatientInfo=this.Reader[36].ToString();//�����������
                    //					if (!this.Reader.IsDBNull(37)) PatientInfo=this.Reader[37].ToString();//Ƿ�Ѵ���
                    //					if (!this.Reader.IsDBNull(38)) PatientInfo=this.Reader[38].ToString();//Ƿ�ѽ��
                    //					if (!this.Reader.IsDBNull(39)) PatientInfo=this.Reader[39].ToString();//סԺ��Դ
                    //					if (!this.Reader.IsDBNull(40)) PatientInfo=this.Reader[40].ToString();//���סԺ����
                    //					if (!this.Reader.IsDBNull(41)) PatientInfo=this.Reader[41].ToString();//סԺ����
                    //					if (!this.Reader.IsDBNull(42)) PatientInfo=this.Reader[42].ToString();//�����Ժ����
                    //					if (!this.Reader.IsDBNull(43)) PatientInfo=this.Reader[43].ToString();//��������
                    //					if (!this.Reader.IsDBNull(44)) PatientInfo=this.Reader[44].ToString();//����Һ�����
                    //					if (!this.Reader.IsDBNull(45)) PatientInfo=this.Reader[45].ToString();//ΥԼ����
                    //					if (!this.Reader.IsDBNull(46)) PatientInfo=this.Reader[46].ToString();//��������
                    if (!Reader.IsDBNull(47)) PatientInfo.Memo = Reader[47].ToString(); //��ע
                    if (!Reader.IsDBNull(48)) PatientInfo.User03 = Reader[48].ToString(); //����Ա
                    if (!Reader.IsDBNull(49)) PatientInfo.User02 = Reader[49].ToString(); //��������
                    if (!Reader.IsDBNull(50)) PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) PatientInfo.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) PatientInfo.IDCardType.ID = Reader[52].ToString();//֤������
                    if (!Reader.IsDBNull(53)) PatientInfo.VipFlag = NConvert.ToBoolean(Reader[53]);//vip��ʶ
                    if (!Reader.IsDBNull(54)) PatientInfo.MatherName = Reader[54].ToString();//ĸ������
                    if (!Reader.IsDBNull(55)) PatientInfo.IsTreatment = NConvert.ToBoolean(Reader[55]);//�Ƿ���
                    if (!Reader.IsDBNull(56)) PatientInfo.PID.CaseNO = Reader[56].ToString();//������
                    if (PatientInfo.IsEncrypt && PatientInfo.NormalName != string.Empty)
                    {
                        PatientInfo.DecryptName = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(PatientInfo.NormalName);
                    }
                    if (!Reader.IsDBNull(57)) PatientInfo.Insurance.ID = Reader[57].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(58)) PatientInfo.Insurance.Name = Reader[58].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(59)) PatientInfo.AddressHomeDoorNo = Reader[59].ToString(); //��ͥסַ���ƺ�
                    if (!Reader.IsDBNull(60)) PatientInfo.Kin.RelationDoorNo = Reader[60].ToString(); //��ϵ�˵�ַ���ƺ�
                    if (!Reader.IsDBNull(61)) PatientInfo.Email = Reader[61].ToString(); //email
                    if (!Reader.IsDBNull(62)) PatientInfo.User01 = Reader[62].ToString(); //��סַ
                    if (!Reader.IsDBNull(63)) PatientInfo.FamilyCode = Reader[63].ToString(); //��ͥ��
                    if (!Reader.IsDBNull(64)) PatientInfo.OtherCardNo = Reader[64].ToString(); //�ͷ�רԱ����
                    if (!Reader.IsDBNull(65)) PatientInfo.ServiceInfo.ID = Reader[65].ToString(); //�ͷ�רԱ����
                    if (!Reader.IsDBNull(66)) PatientInfo.ServiceInfo.Name = Reader[66].ToString(); //�ͷ�רԱ����
                    if (!Reader.IsDBNull(67)) PatientInfo.PatientSourceInfo.ID = Reader[67].ToString(); //������Դ����
                    if (!Reader.IsDBNull(68)) PatientInfo.ReferralPerson = Reader[68].ToString(); //ת����
                    if (!Reader.IsDBNull(69)) PatientInfo.ChannelInfo.ID = Reader[69].ToString(); //������������

                    if (!Reader.IsDBNull(70)) PatientInfo.FamilyName = Reader[70].ToString(); //��ͥ����
                    //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}��Ӷ�ͯ��ǩ
                    if (!Reader.IsDBNull(71)) PatientInfo.ChildFlag = Reader[71].ToString(); //��ͯ���
                    if (!Reader.IsDBNull(72)) PatientInfo.CrmID = Reader[72].ToString(); //CRMID
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();

            return PatientInfo;
        }


        #region ˽�з���
        /// <summary>
        /// ��ѯ���ﻼ����Ϣ(com_patientinfo)
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        private ArrayList QueryComPatient(string whereIndex, params string[] parms)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientInfoQuery", ref sql) == -1)
            {
                this.Err = "��ѯ����ΪRADT.Inpatient.PatientInfoQuery��SQL���ʧ�ܣ�";
                return null;
            }
            string sqlWhere = string.Empty;
            if (this.Sql.GetCommonSql(whereIndex, ref sqlWhere) == -1)
            {
                this.Err = "��������Ϊ" + whereIndex + "��SQL���ʧ�ܣ�";
                return null;
            }
            sqlWhere = string.Format(sqlWhere, parms);
            sql += sqlWhere;
            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            PatientInfo PatientInfo = null;
            ArrayList alPatient = new ArrayList();
            while (Reader.Read())
            {
                try
                {
                    PatientInfo = new PatientInfo();
                    if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                    if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                    if (!Reader.IsDBNull(2)) PatientInfo.SpellCode = Reader[2].ToString(); //ƴ����
                    if (!Reader.IsDBNull(3)) PatientInfo.WBCode = Reader[3].ToString(); //���
                    if (!Reader.IsDBNull(4)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //��������
                    if (!Reader.IsDBNull(5)) PatientInfo.Sex.ID = Reader[5].ToString(); //�Ա�
                    if (!Reader.IsDBNull(6)) PatientInfo.IDCard = Reader[6].ToString(); //���֤��
                    if (!Reader.IsDBNull(7)) PatientInfo.BloodType.ID = Reader[7].ToString(); //Ѫ��
                    if (!Reader.IsDBNull(8)) PatientInfo.Profession.ID = Reader[8].ToString(); //ְҵ
                    if (!Reader.IsDBNull(9)) PatientInfo.CompanyName = Reader[9].ToString(); //������λ
                    if (!Reader.IsDBNull(10)) PatientInfo.PhoneBusiness = Reader[10].ToString(); //��λ�绰
                    if (!Reader.IsDBNull(11)) PatientInfo.BusinessZip = Reader[11].ToString(); //��λ�ʱ�
                    if (!Reader.IsDBNull(12)) PatientInfo.AddressHome = Reader[12].ToString(); //���ڻ��ͥ����
                    if (!Reader.IsDBNull(13)) PatientInfo.PhoneHome = Reader[13].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(14)) PatientInfo.HomeZip = Reader[14].ToString(); //���ڻ��ͥ��������
                    if (!Reader.IsDBNull(15)) PatientInfo.DIST = Reader[15].ToString(); //����
                    if (!Reader.IsDBNull(16)) PatientInfo.Nationality.ID = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) PatientInfo.Kin.Name = Reader[17].ToString(); //��ϵ������
                    if (!Reader.IsDBNull(18)) PatientInfo.Kin.RelationPhone = Reader[18].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationAddress = Reader[19].ToString(); //��ϵ��סַ
                    if (!Reader.IsDBNull(20)) PatientInfo.Kin.Relation.ID = Reader[20].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(21)) PatientInfo.MaritalStatus.ID = Reader[21].ToString(); //����״��
                    if (!Reader.IsDBNull(22)) PatientInfo.Country.ID = Reader[22].ToString(); //����
                    if (!Reader.IsDBNull(23)) PatientInfo.Pact.PayKind.ID = Reader[23].ToString(); //�������
                    if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.Name = Reader[24].ToString(); //�����������
                    if (!Reader.IsDBNull(25)) PatientInfo.Pact.ID = Reader[25].ToString(); //��ͬ����
                    if (!Reader.IsDBNull(26)) PatientInfo.Pact.Name = Reader[26].ToString(); //��ͬ��λ����
                    if (!Reader.IsDBNull(27)) PatientInfo.SSN = Reader[27].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(28)) PatientInfo.AreaCode = Reader[28].ToString(); //����
                    if (!Reader.IsDBNull(29)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //ҽ�Ʒ���
                    if (!Reader.IsDBNull(30)) PatientInfo.Card.ICCard.ID = Reader[30].ToString(); //���Ժ�
                    if (!Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //ҩ�����
                    if (!Reader.IsDBNull(32)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //��Ҫ����
                    if (!Reader.IsDBNull(33)) PatientInfo.Card.NewPassword = Reader[33].ToString(); //�ʻ�����
                    if (!Reader.IsDBNull(34)) PatientInfo.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //�ʻ��ܶ�
                    if (!Reader.IsDBNull(35)) PatientInfo.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //�����ʻ����
                    //					if (!this.Reader.IsDBNull(36)) PatientInfo=this.Reader[36].ToString();//�����������
                    //					if (!this.Reader.IsDBNull(37)) PatientInfo=this.Reader[37].ToString();//Ƿ�Ѵ���
                    //					if (!this.Reader.IsDBNull(38)) PatientInfo=this.Reader[38].ToString();//Ƿ�ѽ��
                    //					if (!this.Reader.IsDBNull(39)) PatientInfo=this.Reader[39].ToString();//סԺ��Դ
                    //					if (!this.Reader.IsDBNull(40)) PatientInfo=this.Reader[40].ToString();//���סԺ����
                    //					if (!this.Reader.IsDBNull(41)) PatientInfo=this.Reader[41].ToString();//סԺ����
                    //					if (!this.Reader.IsDBNull(42)) PatientInfo=this.Reader[42].ToString();//�����Ժ����
                    //					if (!this.Reader.IsDBNull(43)) PatientInfo=this.Reader[43].ToString();//��������
                    //					if (!this.Reader.IsDBNull(44)) PatientInfo=this.Reader[44].ToString();//����Һ�����
                    //					if (!this.Reader.IsDBNull(45)) PatientInfo=this.Reader[45].ToString();//ΥԼ����
                    //					if (!this.Reader.IsDBNull(46)) PatientInfo=this.Reader[46].ToString();//��������
                    if (!Reader.IsDBNull(47)) PatientInfo.Memo = Reader[47].ToString(); //��ע
                    if (!Reader.IsDBNull(48)) PatientInfo.User01 = Reader[48].ToString(); //����Ա
                    if (!Reader.IsDBNull(49)) PatientInfo.User02 = Reader[49].ToString(); //��������
                    if (!Reader.IsDBNull(50)) PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) PatientInfo.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) PatientInfo.IDCardType.ID = Reader[52].ToString();//֤������
                    if (!Reader.IsDBNull(53)) PatientInfo.VipFlag = NConvert.ToBoolean(Reader[53]);//vip��ʶ
                    if (!Reader.IsDBNull(54)) PatientInfo.MatherName = Reader[54].ToString();//ĸ������
                    if (!Reader.IsDBNull(55)) PatientInfo.IsTreatment = NConvert.ToBoolean(Reader[55]);//�Ƿ���
                    if (!Reader.IsDBNull(56)) PatientInfo.PID.CaseNO = Reader[56].ToString();//������
                    if (PatientInfo.IsEncrypt && PatientInfo.NormalName != string.Empty)
                    {
                        PatientInfo.DecryptName = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(PatientInfo.NormalName);
                    }
                    if (!Reader.IsDBNull(57)) PatientInfo.Insurance.ID = Reader[57].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(58)) PatientInfo.Insurance.Name = Reader[58].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(59)) PatientInfo.Kin.RelationDoorNo = Reader[59].ToString(); //��ϵ�˵�ַ���ƺ�
                    if (!Reader.IsDBNull(60)) PatientInfo.AddressHomeDoorNo = Reader[60].ToString(); //��ͥסַ���ƺ�
                    if (!Reader.IsDBNull(61)) PatientInfo.Email = Reader[61].ToString(); //email
                    alPatient.Add(PatientInfo);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();
            return alPatient;

        }
        #endregion
        /// <summary>
        /// ����������ѯ������Ϣ
        /// </summary>
        /// <param name="name">��������</param>
        /// <returns></returns>
        public ArrayList QueryComPatientInfoByName(string name)
        {
            return this.QueryComPatient("RADT.Inpatient.PatientInfoQuery.Where1", name);
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="idenType">֤������</param>
        /// <param name="idenNO">֤����</param>
        /// <returns></returns>
        public ArrayList QueryComPatientInfoByIdenInfo(string idenType, string idenNO)
        {
            return this.QueryComPatient("RADT.Inpatient.PatientInfoQuery.Where2", idenType, idenNO);
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="idenType">֤������</param>
        /// <param name="idenNO">֤����</param>
        /// <returns></returns>
        public ArrayList QueryComPatientInfoByIdenInfoAndName(string idenType, string idenNO, string name)
        {
            return this.QueryComPatient("RADT.Inpatient.PatientInfoQuery.Where3", idenType, idenNO, name);
        }

        [Obsolete("����Ϊ QueryComPatientInfo", true)]
        public PatientInfo PatientInfoQuery(string CardNO)
        {
            return null;
        }

        #endregion

        #region ��סԺ����ʵ���û��߱����Ϣ

        private void myGetTempLocation(PatientInfo PatientInfo)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.Trans.1
            //��û��߱����Ϣ
            //���룺סԺ��ˮ��
            //������0 ����id,1���� name ,2 ����id ,3����name ��4¥��5�㣬6 �ݣ�7 bedno

            #endregion

            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.Trans.1", ref strSql) == -1)
            #region SQL
            /*select	new_data_code,new_data_name,'','','','','','' from com_shiftdata	 
		                 where	PARENT_CODE='[��������]' 
						 and CURRENT_CODE='[��������]' 
						 and  shift_type='RD' and clinic_no='{0}' 
						 and happen_no=(select max(happen_no) 
						 from	com_shiftdata where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and clinic_no='{0}') */
            #endregion
            {
                return;
            }
            strSql = string.Format(strSql, PatientInfo.ID);
            try
            {
                this.ExecQueryByTempReader(strSql);
                this.TempReader.Read();
                if (!this.TempReader.IsDBNull(0)) PatientInfo.PVisit.TemporaryLocation.Dept.ID = this.TempReader[0].ToString();
                if (!this.TempReader.IsDBNull(1)) PatientInfo.PVisit.TemporaryLocation.Dept.Name = this.TempReader[1].ToString();
                if (!this.TempReader.IsDBNull(2))
                    PatientInfo.PVisit.TemporaryLocation.NurseCell.ID = this.TempReader[2].ToString();
                if (!this.TempReader.IsDBNull(3))
                    PatientInfo.PVisit.TemporaryLocation.NurseCell.Name = this.TempReader[3].ToString();
                if (!this.TempReader.IsDBNull(4)) PatientInfo.PVisit.TemporaryLocation.Building = this.TempReader[4].ToString();
                //				if (!this.TempReader1.IsDBNull(5)) PatientInfo.PVisit.TemporaryLocation.Floor = this.TempReader1[5].ToString();
                if (!this.TempReader.IsDBNull(6)) PatientInfo.PVisit.TemporaryLocation.Room = this.TempReader[6].ToString();
                if (!this.TempReader.IsDBNull(7)) PatientInfo.PVisit.TemporaryLocation.Bed.ID = this.TempReader[7].ToString();
                this.Reader.Close();
            }

            catch (Exception ex)
            {
                Err = ex.Message;
                WriteErr();
            }
        }

        #endregion



        #region ��ѯ������Ϣ�б�--������סԺ�Ǽǵ�ʱ����ʾ���컼�ߺ�δ����Ļ���

        /// <summary>
        /// ���߲�ѯ--������סԺ�Ǽǵ�ʱ����ʾ���컼�ߺ�δ����Ļ���
        /// </summary>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public ArrayList PatientsForRegiDisplay(DateTime beginDateTime, DateTime endDateTime)
        {
            string sql1 = string.Empty;
            string sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            string[] arg = new string[2];

            arg[0] = beginDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            arg[1] = endDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            if (Sql.GetCommonSql("RADT.Inpatient.PatientsForRegiDisplay", ref sql2) == -1)
            #region SQL
            /* where PARENT_CODE='[��������]' 
					and CURRENT_CODE='[��������]' 
					and ((TRUNC(in_date) >=to_date('{0}','yyyy-mm-dd')) 
					and (TRUNC(in_date)  <=to_date('{1}','yyyy-mm-dd')) 
					or In_state='R')
					order by in_date desc 
				*/
            #endregion
            {
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, arg);
            return myPatientQuery(sql1);
        }

        #endregion

        #region ��������ѯ������Ϣ�б�

        /// <summary>
        /// ����������ѯ����---wangrc
        /// </summary>
        /// <param name="patientName"></param>
        /// <returns></returns>
        public ArrayList QueryPatientInfoByName(string patientName)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            strSql1 = PatientQuerySelect();
            if (strSql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientNameQuery.Where.12", ref strSql2) == -1)
            #region SQL
            /*
				 * 	where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and NAME like '{0}'and in_state in('I','B') 
				 * */
            #endregion
            {
                return null;
            }
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, patientName);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientCardNoQuery.Where.1��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientCardNoQuery.Where.1��ֵ��ƥ�䣡";
                WriteErr();
                return null;
            }

            return myPatientQuery(strSql1);
        }



        /// <summary>
        /// ����������ѯ���� wolf
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryInpatientNOByName(string name)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.QeryInpatientNoFromPatientNo.3", ref strSql) == 0)
            {
                #region SQL
                /*
				 * select inpatient_no,name,in_state,DEPT_CODE,dept_name,in_date 
				 * from fin_ipr_inmaininfo  where  PARENT_CODE='[��������]'	
				 * and	CURRENT_CODE='[��������]' 
				 * and  name='{0}'
				*/
                #endregion
                try
                {
                    strSql = string.Format(strSql, name);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                return GetPatientInfoBySQL(strSql);
            }
            else
            {
                return null;
            }
        }

        [Obsolete("����ΪQueryPatientInfoByName", true)]
        public ArrayList PatientNameQuery(string PatientName)
        {
            return null;
        }
        #endregion

        #region ��������û�����Ϣ�б�1

        /// <summary>
        /// ���סԺ��ˮ�Ŵ�����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Obsolete("����Ϊ QueryInpatientNOByName", true)]
        public ArrayList QueryInpatientNoFromName(string name)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.QeryInpatientNoFromPatientNo.3", ref strSql) == 0)
            {
                #region SQL
                /*
				 * select inpatient_no,name,in_state,DEPT_CODE,dept_name,in_date 
				 * from fin_ipr_inmaininfo  where  PARENT_CODE='[��������]'	
				 * and	CURRENT_CODE='[��������]' 
				 * and  name='{0}'
				*/
                #endregion
                try
                {
                    strSql = string.Format(strSql, name);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                return GetPatientInfoBySQL(strSql);
            }
            else
            {
                return null;
            }
        }


        #endregion

        #region ��סԺ״̬��ѯ������Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��סԺ״̬��
        /// </summary>
        /// <param name="State">סԺ״̬</param>
        /// <returns></returns>
        public ArrayList QueryPatient(FS.HISFC.Models.Base.EnumInState State)
        {
            #region �ӿ�˵��

            /////RADT.Inpatient.4
            //���룺סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            sql1 = PatientQuerySelect();

            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.9", ref sql2) == -1)
            {
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, State.ToString());
            return myPatientQuery(sql1);
        }

        #endregion

        #region ����Ժʱ���ѯ������Ϣ�б�

        /// <summary>
        /// ���߲�ѯ--����Ժʱ���ѯ wangrc
        /// </summary>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(DateTime beginDateTime, DateTime endDateTime)
        {
            #region �ӿ�˵��

            /////RADT.Inpatient.2
            //����:סԺʱ�俪ʼ������
            //������������Ϣ

            #endregion

            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            string[] arg = new string[2];
            arg[0] = beginDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            arg[1] = endDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQueryByDateIn", ref sql2) == -1)
            {
                Err = "û���ҵ�RADT.Inpatient.PatientQuery.Where.8�ֶ�!";
                ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, arg);
            return myPatientQuery(sql1);
        }

        #endregion

        #region ����Ժʱ���״̬��ѯ������Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-����Ժʱ���״̬��
        /// </summary>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(DateTime beginDateTime, DateTime endDateTime, FS.HISFC.Models.Base.EnumInState State)
        {
            #region �ӿ�˵��

            /////RADT.Inpatient.2
            //����:סԺʱ�俪ʼ��������״̬
            //������������Ϣ

            #endregion

            string sql1 = string.Empty;
            string sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            string[] arg = new string[3];
            arg[0] = beginDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            arg[1] = endDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            arg[2] = State.ToString();
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.8", ref sql2) == -1)
            {
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, arg);
            return myPatientQuery(sql1);
        }

        #endregion



        #region ���� ��Ժʱ��,����״̬ ,���� ��ѯҽ���������߻�����Ϣ

        /// <summary>
        ///  ��Ժʱ��,����״̬ ,���� ��ѯҽ�� ����
        /// </summary>
        /// <param name="beginDateTime">��Ժ��ʼʱ��</param>
        /// <param name="endDateTime">��Ժ����ʱ��</param>
        /// <param name="inState">����״̬</param>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>�������������ݼ�</returns>
        /// creator zhangjunyi@FS.com
        public ArrayList QueryMedicarePatientBasic(DateTime beginDateTime, DateTime endDateTime, FS.HISFC.Models.Base.EnumInState inState, string deptCode, string pactCode)
        {
            //�����ַ��� �洢SQL���
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            string beginTime = string.Empty;
            string endTime = string.Empty;
            //��ȡ ��ѯ������Ϣ SQL��� 
            sql1 = PatientQueryBasicSelect();

            if (sql1 == null)
            {
                return null;
            }

            beginTime = beginDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            endTime = endDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);


            //��ȡ ��ѯ���� 
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQueryMedicare", ref sql2) == -1)
            {
                return null;
            }
            //�齨��ѯSQL��� 
            sql1 = sql1 + " " + string.Format(sql2, beginTime, endTime, inState.ToString(), deptCode, pactCode);

            return myPatientBasicQuery(sql1);
        }

        #endregion

        #region ��ѯδ�ύ�����ǼǵĻ�����Ϣ�б�

        /// <summary>
        /// ��ѯδ�ύ�����ǼǵĻ�����Ϣ
        /// </summary>
        /// <param name="caseFlag">���ߵ��ύ״̬</param>
        /// <returns></returns>
        [Obsolete("���ڣ��ú��������⣡", true)]
        public ArrayList PatientsHavingCase(string caseFlag)
        {
            string strSQLSelect = string.Empty;
            string strSQLWhere = string.Empty;

            strSQLSelect = PatientQuerySelect();

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.10", ref strSQLWhere) == -1)
            #region SQL
            /* where PARENT_CODE='[��������]' and C
				 * URRENT_CODE='[��������]' 
				 * and CASESEND_FLAG = '0' 
				 * and In_state = '{0}' 
				 * and CASE_FLAG <>'0' 
				*/
            #endregion
            {
                return null;
            }
            try
            {
                strSQLSelect += " " + string.Format(strSQLWhere, caseFlag);
            }
            catch
            {
                Err = "RADT.Inpatient.PatientQuery.Where.10 ��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.PatientQuery.Where.10 ��ֵ��ƥ�䣡";
                WriteErr();
                return null;
            }

            return myPatientQuery(strSQLSelect);
        }

        #endregion

        #region ��������״̬��ת�롢ת������ѯ������Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��ѯ������ת�뻼��
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="status">״̬ 1 ���� 2 ȷ��</param>
        /// <returns>������Ϣ</returns>
        public ArrayList QueryPatientShiftInApply(string deptCode, string status)
        {
            ArrayList al = new ArrayList();
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            sql1 = PatientQuerySelect();

            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.1", ref sql2) == -1)
            {
                #region SQL
                /*
				 *where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' 
				 and inpatient_no in 
				 (select inpatient_no from fin_ipr_shiftapply where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and new_dept_code = '{0}' and shift_state = '{1}') 
				 and in_state = 'I' */
                #endregion
                return null;
            }
            sql2 = " " + string.Format(sql2, deptCode, status);
            return myPatientQuery(sql1 + sql2);
        }

        /// <summary>
        /// ���߲�ѯ-��ѯ������ת�뻼��
        /// </summary>
        /// <param name="deptCode">��������</param>
        /// <param name="status">״̬ 1 ���� 2 ȷ��</param>
        /// <returns>������Ϣ</returns>
        public ArrayList QueryPatientShiftInApplyByNurseCell(string deptCode, string status)
        {
            ArrayList al = new ArrayList();
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            sql1 = PatientQuerySelect();

            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.ShiftNurseCell.Where.1", ref sql2) == -1)
            {
                #region SQL
                /*
				 *where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' 
				 and inpatient_no in 
				 (select inpatient_no from fin_ipr_shiftapply where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and new_dept_code = '{0}' and shift_state = '{1}') 
				 and in_state = 'I' */
                #endregion
                return null;
            }
            sql2 = " " + string.Format(sql2, deptCode, status);
            return myPatientQuery(sql1 + sql2);
        }

        [Obsolete("����Ϊ QueryPatientShiftInApply", true)]
        public ArrayList PatientQueryShiftInApply(string locationID, string status)
        {
            return null;
        }
        #endregion

        #region

        /// <summary>
        /// �����Ҫȷ�ϵļ������ۿ��һ����б�
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryShiftOutPatientNeedConfirm(string state)
        {
            string strSql = string.Empty;
            ArrayList al = new ArrayList();

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.ShiftOutPatientNeedConfirm", ref strSql) == -1)
            {
                return null;
            }
            strSql = String.Format(strSql, state);
            if (ExecQuery(strSql) == -1)
            {
                return null;
            }
            while (Reader.Read())
            {
                NeuObject obj = new NeuObject();
                obj.ID = Reader[0].ToString(); //סԺ��ˮ��
                obj.Name = Reader[1].ToString(); //ԭ���Ҵ���
                obj.Memo = Reader[2].ToString(); //ԭ��������
                obj.User01 = Reader[3].ToString(); //Ŀ����Ҵ���
                obj.User02 = Reader[4].ToString(); //Ŀ���������
                al.Add(obj);
            }
            Reader.Close();
            return al;
        }

        #endregion

        #region ��ѯ����ת��������Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��ѯ������ת������
        /// </summary>
        /// <param name="deptCode">ת�����ұ���</param>
        /// <param name="status">ת��ת��״̬</param>
        /// <returns>ת��������Ϣ</returns>
        public ArrayList QueryPatientShiftOutApply(string deptCode, string status)
        {
            ArrayList al = new ArrayList();
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            sql1 = PatientQuerySelect();

            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.2", ref sql2) == -1)
            #region SQL
            /* where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' 
				 * and inpatient_no in 
				 * (select inpatient_no from fin_ipr_shiftapply where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and old_dept_code = '{0}' and shift_state = '{1}') 
				 * and in_state = 'I'*/
            #endregion
            {
                return null;
            }
            sql2 = " " + string.Format(sql2, deptCode, status);
            return myPatientQuery(sql1 + sql2);
        }
        /// <summary>
        /// ���߲�ѯ-��ѯ������ת������
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList QueryPatientShiftOutApplyByNurseCell(string deptCode, string status)
        {
            ArrayList al = new ArrayList();
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            sql1 = PatientQuerySelect();

            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.99", ref sql2) == -1)
            #region SQL

            #endregion
            {
                return null;
            }
            sql2 = " " + string.Format(sql2, deptCode, status);
            return myPatientQuery(sql1 + sql2);
        }

        [Obsolete("����Ϊ QueryPatientShiftOutApply", true)]
        public ArrayList PatientQueryShiftOutApply(string locationID, string status)
        {
            return null;
        }
        #endregion

        #region ��ѯת�뻼��ת����Ϣ
        /// <summary>
        /// ��ѯת�뻼��ת����Ϣ
        /// </summary>
        /// <param name="inPatientNo">סԺ��ˮ��</param>
        /// <param name="shiftState">��ǰ״̬,0δ��Ч,1ת������,2ȷ��,3ȡ������</param>
        /// <returns></returns>
        public ShiftApply QueryPatientShiftApplyInfo(string inPatientNo, string shiftState)
        {
            ShiftApply shiftApply = new ShiftApply();
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.ShiftApplyQuery.1", ref sql) == -1)
            #region SQL
            /*  SELECT   a.inpatient_no,            --סԺ��ˮ��
                         a.happen_no,               --�������
                         a.new_dept_code,           --ת������
                         a.old_dept_code,           --ԭ������
                         a.new_nurse_cell_code,     --ת������վ����
                         a.nurse_cell_code,         --��ʿվ����
                         a.shift_state,             --��ǰ״̬,0δ��Ч,1ת������,2ȷ��,3ȡ������
                         a.confirm_opercode,        --ȷ����
                         a.confirm_date,            --ת��ȷ��ʱ��
                         a.cancel_code,             --ȡ����
                         a.cancel_date,             --ȡ������ʱ��
                         a.mark,                    --��ע
                         a.oper_code,               --����Ա
                         a.oper_date,               --��������
                         a.old_bed_code,            --ԭ������
                         a.new_bed_code             --ת������
                FROM 
                         fin_ipr_shiftapply a       --ת�������
                WHERE 
                         inpatient_no='{0}'
                         AND a.shift_state = '{1}'
                         AND a.confirm_date =(select max(confirm_date) from fin_ipr_shiftapply where inpatient_no='{0}')*/

            #endregion
            {
                return null;
            }
            sql = string.Format(sql, inPatientNo, shiftState);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            if (Reader.Read())
            {
                try
                {
                    if (!Reader.IsDBNull(0)) shiftApply.InPatientNo = Reader["inpatient_no"].ToString(); //סԺ��ˮ��
                    if (!Reader.IsDBNull(1)) shiftApply.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(Reader["happen_no"].ToString()); //�������
                    if (!Reader.IsDBNull(2)) shiftApply.NewDeptCode = Reader["new_dept_code"].ToString(); //ת������
                    if (!Reader.IsDBNull(3)) shiftApply.OldDeptCode = Reader["old_dept_code"].ToString(); //ԭ������
                    if (!Reader.IsDBNull(4)) shiftApply.NewNurseCellCode = Reader["new_nurse_cell_code"].ToString(); //ת������վ����
                    if (!Reader.IsDBNull(5)) shiftApply.NurseCellCode = Reader["nurse_cell_code"].ToString(); //��ʿվ����
                    if (!Reader.IsDBNull(6)) shiftApply.ShiftState = Reader["shift_state"].ToString(); //��ǰ״̬,0δ��Ч,1ת������,2ȷ��,3ȡ������
                    if (!Reader.IsDBNull(7)) shiftApply.ConfirmOper.ID = Reader["confirm_opercode"].ToString(); //ȷ����
                    if (!Reader.IsDBNull(8)) shiftApply.ConfirmOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader["confirm_date"].ToString()); //ת��ȷ��ʱ��
                    if (!Reader.IsDBNull(9)) shiftApply.CancelOper.ID = Reader["cancel_code"].ToString(); //ȡ����
                    if (!Reader.IsDBNull(10)) shiftApply.CancelOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader["cancel_date"].ToString()); //ȡ������ʱ��
                    if (!Reader.IsDBNull(11)) shiftApply.Mark = Reader["mark"].ToString(); //��ע
                    if (!Reader.IsDBNull(12)) shiftApply.Oper.ID = Reader["oper_code"].ToString(); //����Ա
                    if (!Reader.IsDBNull(13)) shiftApply.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader["oper_date"].ToString()); //��������
                    if (!Reader.IsDBNull(14)) shiftApply.OldBedCode = Reader["old_bed_code"].ToString(); //ԭ������
                    if (!Reader.IsDBNull(15)) shiftApply.NewBedCode = Reader["new_bed_code"].ToString(); //ת������
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();

            return shiftApply;
        }

        #endregion

        #region ��ѯҽ���ֹܻ�����Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��ѯҽ���ֹܻ��ߣ��ֹܻ��߷ָ�סԺҽ��
        /// </summary>
        /// <param name="objOpertor">ҽ������</param>
        /// <param name="State">סԺ״̬</param>
        /// <returns>������Ϣ�б�</returns>
        public ArrayList QueryHouseDocPatient(NeuObject objOpertor, FS.HISFC.Models.Base.EnumInState State, string deptCode)
        {
            ArrayList al = new ArrayList();
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.3", ref sql2) == -1)
            #region SQL
            /* where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and house_doc_code = '{0}' and in_state ='{1}' and dept_code = '{2}'*/
            #endregion
            {
                return null;
            }
            sql2 = sql1 + " " + string.Format(sql2, objOpertor.ID, State.ToString(), deptCode);

            return myPatientQuery(sql2);
        }
        [Obsolete("����Ϊ QueryHouseDocPatient", true)]
        public ArrayList PatientQueryHouseDoc(NeuObject objOpertor, InStateEnumService State, string deptCode)
        {
            return null;
        }
        #endregion

        #region ��ѯҽ�����ﻼ����Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-��ѯҽ�����ﻼ��
        /// </summary>
        /// <param name="objOpertor"></param>
        /// <param name="status">1 ���� 2 ȷ��</param>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public ArrayList PatientQueryConsultation(NeuObject objOpertor, string status, DateTime beginDateTime, DateTime endDateTime, string deptID)
        {
            ArrayList al = new ArrayList();

            string sql1 = string.Empty;
            string sql2 = string.Empty;
            string sql3 = string.Empty;

            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.4", ref sql2) == -1)
            {
                return null;
            }
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.15", ref sql3) == -1)
            {
                return null;
            }

            string beginTime = string.Empty; ;
            string endTime = string.Empty;
            beginTime = beginDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            endTime = endDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            sql2 = " " + string.Format(sql2, objOpertor.ID, status, beginTime, endTime);
            al = myPatientQuery(sql1 + sql2);
            status = "1";
            sql3 = " " + string.Format(sql3, deptID, status, beginTime, endTime);

            al.AddRange(myPatientQuery(sql1 + sql3));

            return al;
        }

        /// <summary>
        /// ��ѯҽ���Լ��Ļ��ﻼ��
        /// </summary>
        /// <param name="objOpertor">ҽ������</param>
        /// <param name="status">״̬</param>
        /// <param name="beginDateTime">������ʼʱ��</param>
        /// <param name="endDateTime">������ֹʱ��</param>
        /// <returns>������Ϣ</returns>
        public ArrayList QueryConsultationPatientInfo(NeuObject objOpertor, string status, DateTime beginDateTime, DateTime endDateTime)
        {
            ArrayList al = new ArrayList();
            string sql1 = string.Empty;
            string sql2 = string.Empty;
            string sql3 = string.Empty;

            sql1 = PatientQuerySelect();

            if (sql1 == null)
            {
                return null;
            }
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.4", ref sql2) == -1)
            #region SQL
            /* where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and inpatient_no in 
				 * (select inpatient_no from	MET_IPM_CONSULTATION  where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and cnsl_doccd = '{0}' and CNSL_KIND	= '{1}' and sysdate>=MO_STDT and sysdate<=MO_EDDT )
				 */
            #endregion
            {
                return null;
            }
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.15", ref sql3) == -1)
            #region SQL
            /*where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and inpatient_no in 
				 * (select inpatient_no from	MET_IPM_CONSULTATION  where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and cnsl_deptcd = '{0}' and CNSL_KIND	= '{1}' and sysdate>=MO_STDT and sysdate<=MO_EDDT )
				 */
            #endregion
            {
                return null;
            }
            string[] arg = new string[2];

            string beginTime = string.Empty;
            string endTime = string.Empty;

            beginTime = beginDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            endTime = endDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            sql2 = " " + string.Format(sql2, objOpertor.ID, status, beginTime, endTime);

            al = myPatientQuery(sql1 + sql2);
            return al;
        }
        [Obsolete("����Ϊ QueryConsultationPatientInfo", true)]
        public ArrayList PatientQueryConsultation(NeuObject objOpertor, string Status, DateTime beginDateTime, DateTime endDateTime)
        {
            return null;
        }
        #endregion

        #region ��ѯҽ����Ȩ������Ϣ�б�

        /// <summary>
        /// ��ѯ��Ȩ�Ļ��� met_ipm_permission
        /// </summary>
        /// <param name="strDoc">ҽ������</param>
        /// <returns>������Ϣ</returns>
        public ArrayList QueryPatientByPermission(string strDoc, string srtDept)
        {
            ArrayList al = new ArrayList();
            string strsql1 = string.Empty;
            string strsql2 = string.Empty;

            strsql1 = PatientQuerySelect();

            if (strsql1 == null)
            {
                return null;
            }
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.30", ref strsql2) == -1)
            #region SQL
            /*
				 *where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and inpatient_no in 
				 (select inpatient_no from	 met_ipm_permission where  PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and valid_flag ='0'  and DOC_CODE ='{0}' and sysdate>=mo_stdt and sysdate <= mo_eddt)
				 */
            #endregion
            {
                return null;
            }

            strsql2 = " " + string.Format(strsql2, strDoc, srtDept);
            string strSql = string.Empty;

            strSql = strsql1 + strsql2;
            return myPatientQuery(strSql);
        }

        [Obsolete("�� QueryPatientByPermission(string strDoc) �����˵�˵", true)]
        public ArrayList PatientQueryByPermission(string strDoc)
        {
            return null;
        }
        #endregion
        #region ����ҽ�����Ų�ѯסԺ������Ϣ
        /// <summary>
        /// ����ҽ�����Ų�ѯסԺ������Ϣ
        /// </summary>
        /// <param name="markNO"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByMcardNO(string markNO)
        {
            #region �ӿ�˵��

            //RADT.Inpatient.PatientQuery.where.5
            //���룺�������룬סԺ״̬
            //������������Ϣ

            #endregion

            ArrayList al = new ArrayList();
            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQuerySelect();
            if (sql1 == null)
            {
                return null;
            }

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.18", ref sql2) == -1)
            {
                Err = "û���ҵ�RADT.Inpatient.PatientQuery.Where.18�ֶ�!";
                ErrCode = "-1";
                WriteErr();
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, markNO);
            return myPatientQuery(sql1);
        }
        #endregion

        #region "ԤԼ�Ǽǹ���"

        #region �����￨�Ų�ѯԤԼ��Ϣ�б�

        /// <summary>
        ///  ԤԼ��Ϣ��ѯ--wangrc
        /// </summary>
        /// <param name="CardNO">���￨��</param>
        /// <returns>����1�ɹ� -1ʧ��</returns>
        private ArrayList GetPreInByCardNO(string CardNO)
        {
            string strSql = string.Empty;
            string strSqlWhere = string.Empty;

            strSql = GetCommonSqlForPrepayin();
            if (Sql.GetCommonSql("RADT.Inpatient.PrepayInQuery.1", ref strSqlWhere) == -1)
            {
                #region SQL
                /*   
					WHERE  PARENT_CODE='[��������]'  and 
					CURRENT_CODE='[��������]' and 
					card_no='{0}'
				 */
                #endregion
                return null;
            }
            strSql = strSql + strSqlWhere;
            try
            {
                strSql = string.Format(strSql, CardNO);
            }
            catch (Exception ex)
            {
                ErrCode = ex.Message;
                Err = ex.Message;
                return null;
            }
            return GetPreInpatientInfo(strSql);
        }
        [System.Obsolete("����ΪGetPreInByCardNO", true)]
        public ArrayList GetPrepayInByCardNo(string CardNO)
        {
            string strSql = string.Empty;
            string strSqlWhere = string.Empty;

            strSql = GetCommonSqlForPrepayin();
            if (Sql.GetCommonSql("RADT.Inpatient.PrepayInQuery.1", ref strSqlWhere) == -1)
            {
                #region SQL
                /*   
					WHERE  PARENT_CODE='[��������]'  and 
					CURRENT_CODE='[��������]' and 
					card_no='{0}'
				 */
                #endregion
                return null;
            }
            strSql = strSql + strSqlWhere;
            try
            {
                strSql = string.Format(strSql, CardNO);
            }
            catch (Exception ex)
            {
                ErrCode = ex.Message;
                Err = ex.Message;
                WriteErr();
                return null;
            }
            return GetPreInpatientInfo(strSql);
        }
        #endregion

        #region �����￨�Ų�ѯԤԼ����
        /// <summary>
        /// �����￨�Ų�ѯԤԼ����--wangrc
        /// </summary>
        /// <param name="CardNO">���￨��</param>
        /// <returns>-1ʧ��1�ɹ�</returns>
        public string QueryPreInPatientBedNO(string CardNO)
        {
            string sql = string.Empty;
            string bedNO = string.Empty;

            if (Sql.GetCommonSql("RADT.InPatient.PrepayInBedNoQuery", ref sql) == -1)
            {
                return null;
            }
            sql = string.Format(sql, CardNO);

            if (ExecQuery(sql) <= 0)
            {
                return null;
            }
            if (Reader.Read())
            {
                try
                {
                    bedNO = Reader[0].ToString(); //����
                }

                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                }
            }
            Reader.Close();

            return bedNO;
        }
        [Obsolete("����Ϊ QueryPreInPatientBedNO", true)]
        public string PrepayInBedNoQuery(string CardNO)
        {
            return null;
        }
        #endregion
        #region  "��������ѯԤԼ�ǼǱ�"

        /// <summary>
        /// ��������ѯԤԼ�ǼǱ�---wangrc
        /// </summary>
        /// <returns>string sql</returns>
        private string GetCommonSqlForPrepayin()
        {
            //����ԤԼ�ǼǱ��sql
            string strSql = string.Empty;

            //if (Sql.GetCommonSql("RADT.Inpatient.GetSqlForPrepayin", ref strSql) == -1) return null;  {0935cb7a-b021-4c17-94bf-eaf68b523472}
            strSql = @"
SELECT card_no,
       happen_no, --�������
       name, --����
       sex_code, --�Ա�
       idenno, --���֤��
       birthday, --����
       mcard_no, --ҽ��֤��
       paykind_code, --������� 01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸�
       pact_code, --��ͬ��λ
       bed_no, --����
       nurse_cell_code, --��ʿվ����
       prof_code, --ְ��
       work_name, --������λ
       work_tel, --������λ�绰
       home, --��ͥסַ
       home_tel, --��ͥ�绰
       dist, --����
       birth_area, --������
       nation_code, --����
       linkma_name, --��ϵ��
       linkman_tel, --��ϵ�˵绰
       linkman_add, --��ϵ�˵�ַ
       rela_code, --��ϵ�˹�ϵ
       mari, --����״��
       coun_code, --����
       diag_code, --��ϴ���
       diag_name, --�������
       dept_code, --ԤԼ����
       dept_name, --��������
       predoct_code, --ԤԼҽʦ
       pre_state, --״̬
       pre_date, --ԤԼ����
       oper_code, --����Ա
       oper_date, --��������
       nurse_cell_code, -- ��ʿվ����       
       FOREGIFT, --��ԺѺ��
       INDICATIONS, --��Ժָ��
       PATIENT_TYPE, --��������
       MSDIAGNOSES   --�������
  FROM fin_ipr_prepayin --סԺԤԼ��
";

            return strSql;
        }
        #endregion
        #region "�������sql����ѯԤԼ�ǼǱ�"

        /// <summary>
        /// �������sql����ѯԤԼ�ǼǱ�
        /// </summary>
        /// <param name="strSql">sql���</param>
        /// <returns>���� ʵ��Ϊpatientinfo</returns>
        private ArrayList GetPreInpatientInfo(string strSql)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            if (ExecQuery(strSql) == -1) return null;

            while (Reader.Read())
            {
                PatientInfo = new PatientInfo();
                try
                {
                    if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                    if (!Reader.IsDBNull(1)) PatientInfo.User01 = Reader[1].ToString(); //�������
                    if (!Reader.IsDBNull(2)) PatientInfo.Name = Reader[2].ToString(); //����
                    if (!Reader.IsDBNull(3)) PatientInfo.Sex.ID = Reader[3].ToString(); //�Ա�
                    if (!Reader.IsDBNull(4)) PatientInfo.IDCard = Reader[4].ToString(); //���֤��
                    if (!Reader.IsDBNull(5)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5].ToString()); //����
                    if (!Reader.IsDBNull(6)) PatientInfo.SSN = Reader[6].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(7)) PatientInfo.Pact.PayKind.ID = Reader[7].ToString(); //�������
                    if (!Reader.IsDBNull(8)) PatientInfo.Pact.ID = Reader[8].ToString(); //��ͬ��λ
                    if (!Reader.IsDBNull(9)) PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[9].ToString(); //����
                    if (!Reader.IsDBNull(10)) PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[10].ToString(); //��ʿվ����
                    if (!Reader.IsDBNull(11)) PatientInfo.Profession.ID = Reader[11].ToString(); //ְ��
                    if (!Reader.IsDBNull(12)) PatientInfo.CompanyName = Reader[12].ToString(); //������λ
                    if (!Reader.IsDBNull(13)) PatientInfo.PhoneBusiness = Reader[13].ToString(); //������λ�绰
                    if (!Reader.IsDBNull(14)) PatientInfo.AddressHome = Reader[14].ToString(); //��ͥסַ
                    if (!Reader.IsDBNull(15)) PatientInfo.PhoneHome = Reader[15].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(16)) PatientInfo.DIST = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) PatientInfo.AreaCode = Reader[17].ToString(); //������
                    if (!Reader.IsDBNull(18)) PatientInfo.Nationality.ID = Reader[18].ToString(); //����
                    if (!Reader.IsDBNull(19)) PatientInfo.Kin.ID = Reader[19].ToString(); //��ϵ��
                    if (!Reader.IsDBNull(20)) PatientInfo.Kin.RelationPhone = Reader[20].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(21)) PatientInfo.Kin.RelationAddress = Reader[21].ToString(); //��ϵ�˵�ַ
                    if (!Reader.IsDBNull(22)) PatientInfo.Kin.Relation.ID = Reader[22].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(23)) PatientInfo.MaritalStatus.ID = Reader[23].ToString(); //����״��
                    if (!Reader.IsDBNull(24)) PatientInfo.Country.ID = Reader[24].ToString(); //����
                    if (!this.Reader.IsDBNull(25))
                    {
                        NeuObject obj = new NeuObject();
                        obj.ID = this.Reader[25].ToString();
                        PatientInfo.Diagnoses.Add(obj);
                    }
                    //if (!this.Reader.IsDBNull(25)) PatientInfo.Diagnoses = this.Reader[25].ToString();//��ϴ���
                    if (!this.Reader.IsDBNull(26)) PatientInfo.ClinicDiagnose = this.Reader[26].ToString();//�������
                    if (!Reader.IsDBNull(27)) PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[27].ToString(); //ԤԼ����
                    if (!Reader.IsDBNull(28)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[28].ToString(); //��������
                    if (!Reader.IsDBNull(29)) PatientInfo.PVisit.AdmittingDoctor.ID = Reader[29].ToString(); //ԤԼҽʦ
                    if (!Reader.IsDBNull(30)) PatientInfo.User02 = Reader[30].ToString(); //״̬
                    if (!Reader.IsDBNull(31)) PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString()); //ԤԼ����
                    if (!Reader.IsDBNull(32) && !Reader.IsDBNull(33))
                        PatientInfo.User03 = Reader[32].ToString() + Reader[33].ToString(); //����Ա������ʱ��

                    //ȡԤԼ�ǼǵĻ�ʿվ���� {E9EC275C-F044-40f1-BDDA-0F17410983EB}
                    if (Reader.FieldCount > 33)
                    {
                        PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[34].ToString();

                        if (Reader.FieldCount > 34)
                        {
                            if (!Reader.IsDBNull(35)) PatientInfo.FT.PrepayCost = (decimal)Reader[35]; //Ѻ��
                            if (!Reader.IsDBNull(36)) PatientInfo.Memo = Reader[36].ToString(); //ָ��
                            if (!Reader.IsDBNull(37)) PatientInfo.PatientType.ID = Reader[37].ToString(); //��������
                        }

                        if (Reader.FieldCount > 37)
                        {
                             if (!Reader.IsDBNull(38)) PatientInfo.MSDiagnoses = Reader[38].ToString(); //�������
                        }
                    }
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
                al.Add(PatientInfo);
            }
            Reader.Close();
            return al;
        }
        [System.Obsolete("����Ϊ GetPreInpatientInfo ", true)]
        private ArrayList GetPrepayInAllData(string strSql)
        {
            return null;
        }
        #endregion

        #region "�������sql����ѯԤԼ�ǼǱ���ݷ�����ŷ���ʵ��"

        /// <summary>
        /// ����ʵ�尴�������
        /// </summary>
        /// <param name="strSql">sql���</param>
        /// <returns>ʵ��patientinfo</returns>
        [Obsolete("����", true)]
        private PatientInfo GetPrepayInAllDataByNo(string strSql)
        {
            PatientInfo PatientInfo = null;

            if (ExecQuery(strSql) == -1)
            {
                return null;
            }

            if (Reader.Read())
            {
                PatientInfo = new PatientInfo();
                try
                {
                    if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                    if (!Reader.IsDBNull(1)) PatientInfo.User01 = Reader[1].ToString(); //�������
                    if (!Reader.IsDBNull(2)) PatientInfo.Name = Reader[2].ToString(); //����
                    if (!Reader.IsDBNull(3)) PatientInfo.Sex.ID = Reader[3].ToString(); //�Ա�
                    if (!Reader.IsDBNull(4)) PatientInfo.IDCard = Reader[4].ToString(); //���֤��
                    if (!Reader.IsDBNull(5)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5].ToString()); //����
                    if (!Reader.IsDBNull(6)) PatientInfo.SSN = Reader[6].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(7)) PatientInfo.Pact.PayKind.ID = Reader[7].ToString(); //�������
                    if (!Reader.IsDBNull(8)) PatientInfo.Pact.ID = Reader[8].ToString(); //��ͬ��λ
                    if (!Reader.IsDBNull(9)) PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[9].ToString(); //����
                    if (!Reader.IsDBNull(10)) PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[10].ToString(); //��ʿվ����
                    if (!Reader.IsDBNull(11)) PatientInfo.Profession.ID = Reader[11].ToString(); //ְ��
                    if (!Reader.IsDBNull(12)) PatientInfo.CompanyName = Reader[12].ToString(); //������λ
                    if (!Reader.IsDBNull(13)) PatientInfo.PhoneBusiness = Reader[13].ToString(); //������λ�绰
                    if (!Reader.IsDBNull(14)) PatientInfo.AddressHome = Reader[14].ToString(); //��ͥסַ
                    if (!Reader.IsDBNull(15)) PatientInfo.PhoneHome = Reader[15].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(16)) PatientInfo.DIST = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) PatientInfo.AreaCode = Reader[17].ToString(); //������
                    if (!Reader.IsDBNull(18)) PatientInfo.Nationality.ID = Reader[18].ToString(); //����
                    if (!Reader.IsDBNull(19)) PatientInfo.Kin.ID = Reader[19].ToString(); //��ϵ��
                    if (!Reader.IsDBNull(20)) PatientInfo.Kin.RelationPhone = Reader[20].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(21)) PatientInfo.Kin.RelationAddress = Reader[21].ToString(); //��ϵ�˵�ַ
                    if (!Reader.IsDBNull(22)) PatientInfo.Kin.Relation.ID = Reader[22].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(23)) PatientInfo.MaritalStatus.ID = Reader[23].ToString(); //����״��
                    if (!Reader.IsDBNull(24)) PatientInfo.Country.ID = Reader[24].ToString(); //����
                    //						if (!this.Reader.IsDBNull(25)) PatientInfo.Diagnoses=this.Reader[25].ToString();//��ϴ���
                    //						if (!this.Reader.IsDBNull(26)) PatientInfo.Diagnoses=this.Reader[26].ToString();//�������
                    if (!Reader.IsDBNull(27)) PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[27].ToString(); //ԤԼ����
                    if (!Reader.IsDBNull(28)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[28].ToString(); //��������
                    if (!Reader.IsDBNull(29)) PatientInfo.PVisit.AdmittingDoctor.ID = Reader[29].ToString(); //ԤԼҽʦ
                    if (!Reader.IsDBNull(30)) PatientInfo.User02 = Reader[30].ToString(); //״̬
                    if (!Reader.IsDBNull(31)) PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString()); //ԤԼ����
                    if (!Reader.IsDBNull(32) && !Reader.IsDBNull(33))
                        PatientInfo.User03 = Reader[32].ToString() + Reader[33].ToString(); //����Ա������ʱ��
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            return PatientInfo;
        }

        #endregion

        #region ����ԤԼ��Ժ�ǼǱ�

        /// <summary>
        /// ����ԤԼ��Ժ�Ǽǻ���-������Ϣ
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns>���� 0 �ɹ� С�� 0 ʧ��</returns>
        public int InsertPreInPatient(PatientInfo PatientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.PrepayInPatient.2", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                #region SQL
                /*
				 *  INSERT INTO fin_ipr_prepayin   --סԺԤԼ��

							          ( parent_code,   --����ҽ�ƻ�������
							            current_code,   --����ҽ�ƻ�������
							            card_no,   --���￨��
							            happen_no,   --�������
							            name,   --����
							            sex_code,   --�Ա�
							            idenno,   --���֤��
							            birthday,   --����
							            mcard_no,   --ҽ��֤��
							            paykind_code,   --������� 01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸�
							            pact_code,   --��ͬ��λ
							            bed_no,   --����
							            nurse_cell_code,   --��ʿվ����

							            prof_code,   --ְ��
							            work_name,   --������λ
							            work_tel,   --������λ�绰
							            home,   --��ͥסַ
							            home_tel,   --��ͥ�绰
							            dist,   --����
							            birth_area,   --������

							            nation_code,   --����
							            linkma_name,   --��ϵ��

							            linkman_tel,   --��ϵ�˵绰

							            linkman_add,   --��ϵ�˵�ַ
							            rela_code,   --��ϵ�˹�ϵ

							            mari,   --����״��
							            coun_code,   --����
							            diag_code,   --��ϴ���
							            diag_name,   --�������
							            dept_code,   --ԤԼ����
							            dept_name,   --��������
							            predoct_code,   --ԤԼҽʦ
							            pre_state,   --״̬

							            pre_date,   --ԤԼ����
							            oper_code,   --����Ա

							            oper_date )  --��������
							     VALUES 
							          ( '[��������]',   --����ҽ�ƻ�������
							            '[��������]',   --����ҽ�ƻ�������
							            '{0}',   --���￨��
							            (select NVL(MAX(happen_no),0)+1 from fin_ipr_prepayin where PARENT_CODE='[��������]' and CURRENT_CODE='[������	��]' 	and 	card_no='{0}'),   --�������
							            '{2}',   --����
							            '{3}',   --�Ա�
							            '{4}',   --���֤��
							            to_date('{5}','yyyy-mm-dd HH24:mi:ss'),   --����
							            '{6}',   --ҽ��֤��
							            '{7}',   --������� 01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸�
							            '{8}',   --��ͬ��λ
							            '{9}',   --����
							            '{10}',   --��ʿվ����

							            '{11}',   --ְ��
							            '{12}',   --������λ
							            '{13}',   --������λ�绰
							            '{14}',   --��ͥסַ
							            '{15}',   --��ͥ�绰
							            '{16}',   --����
							            '{17}',   --������

							            '{18}',   --����
							            '{19}',   --��ϵ��

							            '{20}',   --��ϵ�˵绰

							            '{21}',   --��ϵ�˵�ַ
							            '{22}',   --��ϵ�˹�ϵ

							            '{23}',   --����״��
							            '{24}',   --����
							            '{25}',   --��ϴ���
							            '{26}',   --�������
							            '{27}',   --ԤԼ����
							            '{28}',   --��������
							            '{29}',   --ԤԼҽʦ
							            '{30}',   --״̬

							            to_date('{31}','yyyy-mm-dd HH24:mi:ss'),   --ԤԼ����
							            '{32}',   --����Ա

							            sysdate )
			*/
                #endregion
                string[] s = new string[38];
                try
                {
                    s[0] = PatientInfo.PID.CardNO; //���￨��
                    s[2] = PatientInfo.Name; //����
                    s[3] = PatientInfo.Sex.ID.ToString(); //�Ա�
                    s[4] = PatientInfo.IDCard; //���֤��
                    s[5] = PatientInfo.Birthday.ToString(); //����
                    s[6] = PatientInfo.SSN; //ҽ��֤��
                    s[7] = PatientInfo.Pact.PayKind.ID; //�������
                    s[8] = PatientInfo.Pact.ID; //��ͬ��λ
                    s[9] = PatientInfo.PVisit.PatientLocation.Bed.ID; //����
                    s[10] = PatientInfo.PVisit.PatientLocation.NurseCell.ID; //��ʿվ����
                    s[11] = PatientInfo.Profession.ID; //ְ��
                    s[12] = PatientInfo.CompanyName; //������λ
                    s[13] = PatientInfo.PhoneBusiness; //������λ�绰
                    s[14] = PatientInfo.AddressHome; //��ͥסַ
                    s[15] = PatientInfo.PhoneHome; //��ͥ�绰
                    s[16] = PatientInfo.DIST; //����
                    s[17] = PatientInfo.DIST; //������
                    s[18] = PatientInfo.Nationality.ID; //����
                    s[19] = PatientInfo.Kin.ID; //��ϵ��
                    s[20] = PatientInfo.Kin.RelationPhone; //��ϵ�˵绰
                    s[21] = PatientInfo.Kin.RelationAddress; //��ϵ�˵�ַ
                    s[22] = PatientInfo.Kin.Relation.ID; //��ϵ�˹�ϵ
                    s[23] = PatientInfo.MaritalStatus.ID.ToString(); //����״��
                    s[24] = PatientInfo.Country.ID; //����
                    if (PatientInfo.Diagnoses.Count > 0)
                    {
                        s[25] = (PatientInfo.Diagnoses[0] as NeuObject).ID; //��ϴ���
                    }

                    s[26] = PatientInfo.ClinicDiagnose.ToString(); //�������

                    s[27] = PatientInfo.PVisit.PatientLocation.Dept.ID; //ԤԼ����
                    s[28] = PatientInfo.PVisit.PatientLocation.Dept.Name; //��������
                    s[29] = PatientInfo.PVisit.AdmittingDoctor.ID; //ԤԼҽʦ
                    s[30] = "0"; //״̬
                    s[31] = PatientInfo.PVisit.InTime.ToString(); //ԤԼ����
                    s[32] = Operator.ID; //����Ա
                    s[33] = PatientInfo.FT.PrepayCost.ToString();//Ѻ��
                    s[34] = PatientInfo.Memo;//ָ��
                    s[35] = GetSysDateTime().ToString(); //��������
                    s[36] = PatientInfo.PatientType.ID;
                    s[37] = PatientInfo.MSDiagnoses;  //{0935cb7a-b021-4c17-94bf-eaf68b523472}
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                }

                strSql = string.Format(strSql, s);
                return ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

        }

        [Obsolete("����Ϊ InsertPreInPatient", true)]
        public int PrepayInPatient(PatientInfo PatientInfo)
        {
            return 0;
        }
        #endregion

        //�޸��ˣ�·־�� ʱ�䣺2007-4-19 
        //Ŀ�ģ����߿���ԤԼ��Σ���������� ������Ÿ���ԤԼ״̬ 0 ΪԤԼ 1 Ϊ���� 2ת��Ժ
        /// <summary>
        /// ���߿���ԤԼ��Σ���������� ������Ÿ���ԤԼ״̬ 0 ΪԤԼ 1 Ϊ���� 2ת��Ժ
        /// </summary>
        /// <param name="CardNO">���￨��</param>
        /// <param name="State">״̬</param>
        /// <param name="HappenNO">�������</param>
        /// <returns></returns>
        public int UpdatePreInPatientState(string CardNO, string State, string HappenNO)
        {
            string StrSql = string.Empty;
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdatePrepayinState.2", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                //���� ״̬ ����Ա  �������
                strSql = string.Format(strSql, CardNO, State, Operator.ID, HappenNO);
                return ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
        }



        /// <summary>
        /// ����ԤԼ��Ժ�Ǽǻ���-������Ϣ
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns>���� 0 �ɹ� С�� 0 ʧ��</returns>
        public int UpdatePreInPatientByHappenNo(PatientInfo PatientInfo)
        {
            string strSql = string.Empty;
            strSql = @"UPDATE FIN_IPR_PREPAYIN     --סԺԤԼ��
                    SET
                    CARD_NO='{0}',   --���￨��
                    HAPPEN_NO='{1}',   --�������
                    NAME='{2}',   --����
                    SEX_CODE='{3}',   --�Ա�
                    IDENNO='{4}',   --���֤��
                    BIRTHDAY=TO_DATE('{5}','YYYY-MM-DD HH24:MI:SS'),   --����
                    MCARD_NO='{6}',   --ҽ��֤��
                    PAYKIND_CODE='{7}',   --������� 01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸�
                    PACT_CODE='{8}',   --��ͬ��λ
                    BED_NO='{9}',   --����
                    NURSE_CELL_CODE='{10}',   --��ʿվ����
                    PROF_CODE='{11}',   --ְ��
                    WORK_NAME='{12}',   --������λ
                    WORK_TEL='{13}',   --������λ�绰
                    HOME='{14}',   --��ͥסַ
                    HOME_TEL='{15}',   --��ͥ�绰
                    DIST='{16}',   --����
                    BIRTH_AREA='{17}',   --������
                    NATION_CODE='{18}',   --����
                    LINKMA_NAME='{19}',   --��ϵ��
                    LINKMAN_TEL='{20}',   --��ϵ�˵绰
                    LINKMAN_ADD='{21}',   --��ϵ�˵�ַ
                    RELA_CODE='{22}',   --��ϵ�˹�ϵ
                    MARI='{23}',   --����״��
                    COUN_CODE='{24}',   --����
                    DIAG_CODE='{25}',   --��ϴ���
                    DIAG_NAME='{26}',   --�������
                    DEPT_CODE='{27}',   --ԤԼ����
                    DEPT_NAME='{28}',   --��������
                    PREDOCT_CODE='{29}',   --ԤԼҽʦ
                    PRE_STATE='{30}',   --״̬
                    PRE_DATE=TO_DATE('{31}','YYYY-MM-DD HH24:MI:SS'),   --ԤԼ����
                    OPER_CODE='{32}',   --����Ա
                    OPER_DATE=TO_DATE('{33}','YYYY-MM-DD HH24:MI:SS'),   --��������
                    FOREGIFT='{34}',   --��ԺѺ��
                    INDICATIONS='{35}',   --��Ժָ��
                    PATIENT_TYPE = '{36}'
                    WHERE card_no = '{0}' 
                           and pre_state in ('0','1')
                           and happen_no = '{1}'";

            try
            {
                string[] s = new string[37];
                try
                {
                    s[0] = PatientInfo.PID.CardNO; //���￨��
                    s[1] = PatientInfo.User01; //�������
                    s[2] = PatientInfo.Name; //����
                    s[3] = PatientInfo.Sex.ID.ToString(); //�Ա�
                    s[4] = PatientInfo.IDCard; //���֤��
                    s[5] = PatientInfo.Birthday.ToString(); //����
                    s[6] = PatientInfo.SSN; //ҽ��֤��
                    s[7] = PatientInfo.Pact.PayKind.ID; //�������
                    s[8] = PatientInfo.Pact.ID; //��ͬ��λ
                    s[9] = PatientInfo.PVisit.PatientLocation.Bed.ID; //����
                    s[10] = PatientInfo.PVisit.PatientLocation.NurseCell.ID; //��ʿվ����
                    s[11] = PatientInfo.Profession.ID; //ְ��
                    s[12] = PatientInfo.CompanyName; //������λ
                    s[13] = PatientInfo.PhoneBusiness; //������λ�绰
                    s[14] = PatientInfo.AddressHome; //��ͥסַ
                    s[15] = PatientInfo.PhoneHome; //��ͥ�绰
                    s[16] = PatientInfo.DIST; //����
                    s[17] = PatientInfo.DIST; //������
                    s[18] = PatientInfo.Nationality.ID; //����
                    s[19] = PatientInfo.Kin.ID; //��ϵ��
                    s[20] = PatientInfo.Kin.RelationPhone; //��ϵ�˵绰
                    s[21] = PatientInfo.Kin.RelationAddress; //��ϵ�˵�ַ
                    s[22] = PatientInfo.Kin.Relation.ID; //��ϵ�˹�ϵ
                    s[23] = PatientInfo.MaritalStatus.ID.ToString(); //����״��
                    s[24] = PatientInfo.Country.ID; //����
                    if (PatientInfo.Diagnoses.Count > 0)
                    {
                        s[25] = (PatientInfo.Diagnoses[0] as NeuObject).ID; //��ϴ���
                    }

                    s[26] = PatientInfo.ClinicDiagnose.ToString(); //�������
                    s[27] = PatientInfo.PVisit.PatientLocation.Dept.ID; //ԤԼ����
                    s[28] = PatientInfo.PVisit.PatientLocation.Dept.Name; //��������
                    s[29] = PatientInfo.PVisit.AdmittingDoctor.ID; //ԤԼҽʦ
                    s[30] = "0"; //״̬
                    s[31] = PatientInfo.PVisit.InTime.ToString(); //ԤԼ����
                    s[32] = Operator.ID; //����Ա
                    s[33] = GetSysDateTime().ToString(); //��������
                    s[34] = PatientInfo.FT.PrepayCost.ToString();//Ѻ��
                    s[35] = PatientInfo.Memo;//ָ��
                    s[36] = PatientInfo.PatientType.ID;//ָ��

                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                }

                strSql = string.Format(strSql, s);
                return ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
        }

        /// <summary>
        /// ����ԤԼ״̬ 0 ΪԤԼ 1 Ϊ���� 2ת��Ժ
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="State">״̬</param>
        /// <returns></returns>
        public int UpdatePreInPatientState(string cardNO, string State)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.UpdatePrepayinState.1", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                //���� ״̬ ����Ա
                strSql = string.Format(strSql, cardNO, State, Operator.ID);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return -1;
            }
            return ExecNoQuery(strSql);
        }
        [Obsolete("����Ϊ UpdatePreInPatientState", true)]
        public int UpdatePrepayinState(string CardNO, string State)
        {
            return 0;
        }
        /// <summary>
        /// ��ȡԤԼ�Ǽ���Ϣͨ��״̬��ԤԼʱ��
        /// </summary>
        /// <param name="State"></param>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public ArrayList GetPreInPatientInfoByDateAndState(string State, string Begin, string End)
        {
            string strSql = string.Empty;
            string strSqlWhere = string.Empty;

            strSql = GetCommonSqlForPrepayin();

            if (Sql.GetCommonSql("RADT.Inpatient.GetPrepayInByDateAndState", ref strSqlWhere) == -1) return null;
            strSql = strSql + strSqlWhere;

            try
            {
                strSql = string.Format(strSql, State, Begin, End);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }

            return GetPreInpatientInfo(strSql);
        }
        /// <summary>
        /// ��ȡԤԼ�Ǽ���Ϣͨ�����ź�ԤԼʱ��// {6BF1F99D-7307-4d05-B747-274D24174895}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ArrayList GetPrepayInByCardNoAndDate(string cardNo)
        {
            string strSql = string.Empty;
            string strSqlWhere = string.Empty;

            strSql = GetCommonSqlForPrepayin();

            if (Sql.GetCommonSql("RADT.Inpatient.GetPrepayInByCardNoAndDate", ref strSqlWhere) == -1) return null;
            strSql = strSql + strSqlWhere;

            try
            {
                strSql = string.Format(strSql, cardNo);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }

            return GetPreInpatientInfo(strSql);
        }

        [Obsolete("����Ϊ GetPreInPatientInfoByDateAndState", true)]
        public ArrayList GetPrepayInByDateAndState(string State, string Begin, string End)
        {
            return null;
        }
        /// <summary>
        /// ��������Ż�õǼ�ʵ��
        /// </summary>
        /// <param name="strNo">�������</param>
        /// <param name="cardNO">����</param>
        /// <returns></returns>
        public PatientInfo GetPreInPatientInfoByCardNO(string strNo, string cardNO)
        {
            string strSql = string.Empty;
            string strSqlWhere = string.Empty;

            strSql = GetCommonSqlForPrepayin();

            if (Sql.GetCommonSql("RADT.Inpatient.GetPrepayInByNo", ref strSqlWhere) == -1)
            #region SQL
            /*		WHERE PARENT_CODE='[��������]'  and 
						 CURRENT_CODE='[��������]' and 
						 happen_no= '{0}'  and 
						 CARD_NO = '{1}'						
				 */
            #endregion
            {
                return null;
            }
            strSql = strSql + strSqlWhere;

            try
            {
                strSql = string.Format(strSql, strNo, cardNO);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }
            ArrayList al = new ArrayList();
            al = GetPreInpatientInfo(strSql);
            if (al == null)
            {
                return null;
            }
            PatientInfo PatientInfo;
            PatientInfo = (PatientInfo)al[0];

            return PatientInfo;
            //			return this.GetPrepayInAllDataByNo(strSql);
        }
        [Obsolete("����Ϊ GetPreInPatientInfoByCardNO", true)]
        public PatientInfo GetPrepayInByNo(string strNo, string cardNO)
        {
            return null;
        }
        #endregion

        #region ��ѯ��ָ������ָ��ʱ���ڷ��������õ���

        /// <summary>
        /// ��ȡ��ָ������ָ��ʱ���ڷ��������õ��˵�סԺ��ˮ��
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public ArrayList GetDeptInpatientNo(string DeptID, string BeginTime, string EndTime)
        {
            ArrayList al = new ArrayList();
            string strsql1 = string.Empty;

            if (Sql.GetCommonSql("RADT.Inpatient.GetDeptInpatientNo", ref strsql1) == -1)
            #region SQL
            /*
				 * select distinct t.inpatient_no  
					from fin_ipb_feeinfo t  
					where t.feeoper_deptcode = '6400' 
					and t.fee_date >= to_date('2005-7-31 0:00:00','yyyy-mm-dd HH24:mi:ss')  
					and t.fee_date <= to_date('2005-10-13 0:00:00','yyyy-mm-dd HH24:mi:ss') 
				*/
            #endregion
            {
                return null;
            }
            strsql1 = string.Format(strsql1, DeptID, BeginTime, EndTime);
            ExecQuery(strsql1);
            PatientInfo PatientInfo;
            while (Reader.Read())
            {
                PatientInfo = new PatientInfo();
                PatientInfo.ID = Reader[0].ToString();
                PatientInfo.ID = Reader[0].ToString();
                al.Add(PatientInfo);
                PatientInfo = null;
            }
            Reader.Close();
            return al;
        }

        #endregion

        #region  ������ϵ�������ѯ ������Ϣ  ���� where ��䲿���� ���ݲ�ѯ�ؼ���ϳ�����

        /// <summary>
        /// ��ѯ���˻�����Ϣ ������ ����ϲ�ѯ�� ���ɲ�ѯ�����������
        /// </summary>
        /// <param name="strWhere"> �����where ����</param>
        /// <returns> �ɹ��������飬ʧ�ܷ���null</returns>
        public ArrayList PatientInfoGet(string strWhere)
        {
            //����Ҫ���ص�����
            ArrayList al = new ArrayList();
            try
            {
                //��ȡ������SQL���
                string strSql = PatientQuerySelect();
                strSql = strSql + " " + strWhere;
                //��ѯ������Ϣ
                al = myPatientQuery(strSql);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                WriteErr();
                return null;
            }
            return al;
        }

        #endregion

        #region ˽��

        #region �������Sql����ѯ������Ϣ�б�--˽��

        /// <summary>
        /// ���￨��ѯ
        /// </summary>
        /// <param name="SQLPatient">SQL���</param>
        /// <returns>����һ�����߿�������Ϣ</returns>
        private ArrayList myCardPatientQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;

            ExecQuery(SQLPatient);
            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new PatientInfo();
                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                        if (!Reader.IsDBNull(2)) PatientInfo.SpellCode = Reader[2].ToString(); //ƴ����
                        if (!Reader.IsDBNull(3)) PatientInfo.WBCode = Reader[3].ToString(); //���
                        if (!Reader.IsDBNull(4)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //��������
                        if (!Reader.IsDBNull(5)) PatientInfo.Sex.ID = Reader[5].ToString(); //�Ա�
                        if (!Reader.IsDBNull(6)) PatientInfo.IDCard = Reader[6].ToString(); //���֤��
                        if (!Reader.IsDBNull(7)) PatientInfo.BloodType.ID = Reader[7].ToString(); //Ѫ��
                        if (!Reader.IsDBNull(8)) PatientInfo.Profession.ID = Reader[8].ToString(); //ְҵ
                        if (!Reader.IsDBNull(9)) PatientInfo.CompanyName = Reader[9].ToString(); //������λ
                        if (!Reader.IsDBNull(10)) PatientInfo.PhoneBusiness = Reader[10].ToString(); //��λ�绰
                        if (!Reader.IsDBNull(11)) PatientInfo.BusinessZip = Reader[11].ToString(); //��λ�ʱ�
                        if (!Reader.IsDBNull(12)) PatientInfo.AddressHome = Reader[12].ToString(); //���ڻ��ͥ����
                        if (!Reader.IsDBNull(13)) PatientInfo.PhoneHome = Reader[13].ToString(); //��ͥ�绰
                        if (!Reader.IsDBNull(14)) PatientInfo.HomeZip = Reader[14].ToString(); //���ڻ��ͥ��������
                        if (!Reader.IsDBNull(15)) PatientInfo.DIST = Reader[15].ToString(); //����
                        if (!Reader.IsDBNull(16)) PatientInfo.Nationality.ID = Reader[16].ToString(); //����
                        if (!Reader.IsDBNull(17)) PatientInfo.Kin.Name = Reader[17].ToString(); //��ϵ������
                        if (!Reader.IsDBNull(18)) PatientInfo.Kin.RelationPhone = Reader[18].ToString(); //��ϵ�˵绰
                        if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationAddress = Reader[19].ToString(); //��ϵ��סַ
                        if (!Reader.IsDBNull(20)) PatientInfo.Kin.Relation.ID = Reader[20].ToString(); //��ϵ�˹�ϵ
                        if (!Reader.IsDBNull(21)) PatientInfo.MaritalStatus.ID = Reader[21].ToString(); //����״��
                        if (!Reader.IsDBNull(22)) PatientInfo.Country.ID = Reader[22].ToString(); //����
                        if (!Reader.IsDBNull(23)) PatientInfo.Pact.PayKind.ID = Reader[23].ToString(); //�������
                        if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.Name = Reader[24].ToString(); //�����������
                        if (!Reader.IsDBNull(25)) PatientInfo.Pact.ID = Reader[25].ToString(); //��ͬ����
                        if (!Reader.IsDBNull(26)) PatientInfo.Pact.Name = Reader[26].ToString(); //��ͬ��λ����
                        if (!Reader.IsDBNull(27)) PatientInfo.SSN = Reader[27].ToString(); //ҽ��֤��
                        if (!Reader.IsDBNull(28)) PatientInfo.AreaCode = Reader[28].ToString(); //����
                        //						if(!this.Reader.IsDBNull(29)) PatientInfo.FT.TotCost=this.Reader[29].ToString();//ҽ�Ʒ���
                        //						if(!this.Reader.IsDBNull(30)) PatientInfo.User03 = this.Reader[30].ToString();//���Ժ�
                        //						if(!this.Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray=System.Convert.ToBoolean(this.Reader[31].ToString());//ҩ�����
                        //						if(!this.Reader.IsDBNull(32)) PatientInfo.Disease.IsMainDisease=System.Convert.ToBoolean(this.Reader[32].ToString());//��Ҫ����
                        //						if(!this.Reader.IsDBNull(33)) PatientInfo=this.Reader[33].ToString();//�ʻ�����
                        //						if(!this.Reader.IsDBNull(34)) PatientInfo=this.Reader[34].ToString();//�ʻ��ܶ�
                        //						if(!this.Reader.IsDBNull(35)) PatientInfo=this.Reader[35].ToString();//�����ʻ����
                        //						if(!this.Reader.IsDBNull(36)) PatientInfo=this.Reader[36].ToString();//�����������
                        //						if(!this.Reader.IsDBNull(37)) PatientInfo=this.Reader[37].ToString();//Ƿ�Ѵ���
                        //						if(!this.Reader.IsDBNull(38)) PatientInfo=this.Reader[38].ToString();//Ƿ�ѽ��
                        //						if(!this.Reader.IsDBNull(39)) PatientInfo=this.Reader[39].ToString();//סԺ��Դ
                        //						if(!this.Reader.IsDBNull(40)) PatientInfo=this.Reader[40].ToString();//���סԺ����
                        //						if(!this.Reader.IsDBNull(41)) PatientInfo=this.Reader[41].ToString();//סԺ����
                        //						if(!this.Reader.IsDBNull(42)) PatientInfo=this.Reader[42].ToString();//�����Ժ����
                        //						if(!this.Reader.IsDBNull(44)) PatientInfo=this.Reader[44].ToString();//����Һ�����
                        //						if(!this.Reader.IsDBNull(45)) PatientInfo=this.Reader[45].ToString();//ΥԼ����
                        //						if(!this.Reader.IsDBNull(46)) PatientInfo=this.Reader[46].ToString();//��������
                        if (!Reader.IsDBNull(47)) PatientInfo.Memo = Reader[47].ToString(); //��ע
                        if (!Reader.IsDBNull(48)) PatientInfo.User01 = Reader[48].ToString(); //����Ա
                        if (!Reader.IsDBNull(49)) PatientInfo.User02 = Reader[49].ToString(); //��������

                        if (Reader.FieldCount > 51)
                        {
                            if (!Reader.IsDBNull(51)) PatientInfo.CrmID = Reader[51].ToString(); //��������   {AA59299C-6C72-4c61-84E4-C7443E85FFBD}
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                    }
                    al.Add(PatientInfo);
                }
            } //�׳�����
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return al;
        }


        #endregion

        #region �������Sql����ѯ���˻�����Ϣ�б�--˽��
        /// <summary>
        /// ��ѯ���߻�����Ϣ ������ѯ
        /// </summary>
        /// <param name="SQLPatient">SQL���</param>
        /// <returns>����һ�����߻�����Ϣ</returns>
        private ArrayList myPatientBasicQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;
            if (ExecQuery(SQLPatient) == -1) return null;
            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new PatientInfo();
                    if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString();
                    PatientInfo.ID = PatientInfo.ID;
                    PatientInfo.PID.ID = PatientInfo.ID;
                    if (!Reader.IsDBNull(1)) PatientInfo.PID.PatientNO = Reader[1].ToString();
                    if (!Reader.IsDBNull(2)) PatientInfo.Name = Reader[2].ToString();
                    PatientInfo.Name = PatientInfo.Name;
                    if (!Reader.IsDBNull(3)) PatientInfo.Sex.ID = Reader[3].ToString();
                    if (!Reader.IsDBNull(4)) PatientInfo.Birthday = NConvert.ToDateTime(Reader[4].ToString());
                    if (!Reader.IsDBNull(5)) PatientInfo.PVisit.InTime = NConvert.ToDateTime(Reader[5].ToString());
                    if (!Reader.IsDBNull(6)) PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[6].ToString();
                    if (!Reader.IsDBNull(7)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[7].ToString();
                    if (!Reader.IsDBNull(8)) PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[8].ToString();
                    if (!Reader.IsDBNull(9)) PatientInfo.PVisit.PatientLocation.Bed.Status.ID = Reader[9].ToString();
                    if (!Reader.IsDBNull(10)) PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[10].ToString();
                    if (!Reader.IsDBNull(11)) PatientInfo.PVisit.PatientLocation.NurseCell.Name = Reader[11].ToString();
                    if (!Reader.IsDBNull(12)) PatientInfo.PVisit.AdmittingDoctor.ID = Reader[12].ToString(); //ҽʦ����(סԺ)
                    if (!Reader.IsDBNull(13)) PatientInfo.PVisit.AdmittingDoctor.Name = Reader[13].ToString(); //ҽʦ����(סԺ)
                    if (!Reader.IsDBNull(14)) PatientInfo.PVisit.AttendingDoctor.ID = Reader[14].ToString(); //ҽʦ����(����)
                    if (!Reader.IsDBNull(15)) PatientInfo.PVisit.AttendingDoctor.Name = Reader[15].ToString(); //ҽʦ����(����)
                    if (!Reader.IsDBNull(16)) PatientInfo.PVisit.ConsultingDoctor.ID = Reader[16].ToString(); //ҽʦ����(����)
                    if (!Reader.IsDBNull(17)) PatientInfo.PVisit.ConsultingDoctor.Name = Reader[17].ToString(); //ҽʦ����(����)
                    if (!Reader.IsDBNull(18)) PatientInfo.PVisit.TempDoctor.ID = Reader[18].ToString(); //ҽʦ����(ʵϰ)
                    if (!Reader.IsDBNull(19)) PatientInfo.PVisit.TempDoctor.Name = Reader[19].ToString(); //ҽʦ����(ʵϰ)
                    if (!Reader.IsDBNull(20)) PatientInfo.PVisit.AdmittingNurse.ID = Reader[20].ToString(); // ��ʿ����(����)
                    if (!Reader.IsDBNull(21)) PatientInfo.PVisit.AdmittingNurse.Name = Reader[21].ToString(); // ��ʿ����(����)
                    if (!Reader.IsDBNull(22)) PatientInfo.Disease.Tend.Name = Reader[22].ToString();
                    if (!Reader.IsDBNull(23)) PatientInfo.Disease.Memo = Reader[23].ToString(); //��ʳ
                    if (!Reader.IsDBNull(24)) PatientInfo.Diagnoses.Add(Reader[24].ToString()); //���
                    if (!Reader.IsDBNull(25)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[25]);
                    if (!Reader.IsDBNull(26)) PatientInfo.FT.LeftCost = NConvert.ToDecimal(Reader[26]);
                    if (!Reader.IsDBNull(27)) PatientInfo.PVisit.MoneyAlert = NConvert.ToDecimal(Reader[27]);
                    if (!Reader.IsDBNull(28)) PatientInfo.FT.PrepayCost = NConvert.ToDecimal(Reader[28]);//Ԥ����
                    PatientInfo.PVisit.AlertType.ID = this.Reader[29].ToString();
                    PatientInfo.PVisit.BeginDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[30]);
                    PatientInfo.PVisit.EndDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[31]);
                    if (Reader.FieldCount > 32)
                    {
                        if (!Reader.IsDBNull(32)) PatientInfo.FT.BalancedCost = NConvert.ToDecimal(Reader[32]);//�������
                    }

                    //���������ñ��
                    if (Reader.FieldCount > 33)
                    {
                        PatientInfo.PVisit.AlertFlag = FS.FrameWork.Function.NConvert.ToBoolean(Reader[33]);
                    }

                    //��ͬ��λ
                    if (Reader.FieldCount > 34)
                    {
                        PatientInfo.Pact.ID = this.Reader[34].ToString();
                        PatientInfo.Pact.Name = this.Reader[35].ToString();
                    }

                    //��ͬ��λ
                    if (Reader.FieldCount > 36)// {F417D766-19C0-4d3e-AB72-D774058B497E}
                    {
                        PatientInfo.AddressHome = this.Reader[36].ToString();
                        PatientInfo.User01 = this.Reader[37].ToString();
                    }
                    //��Ժ״̬{7FDF13F6-47F9-40de-B7BD-62CBA854D0CB}
                    if (Reader.FieldCount > 38)
                    {
                        PatientInfo.PVisit.InState.ID = this.Reader[38].ToString();
                    }
                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }
            } //�׳�����
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return al;
        }

        #endregion

        #region �������Sql����ѯ������Ϣ�б�--˽��

        private ArrayList myPatientQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;

            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            //ȡϵͳʱ��,�����õ������ַ���
            DateTime sysDate = GetDateTimeFromSysDateTime();

            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new PatientInfo();

                    #region "�ӿ�˵��"

                    //0 סԺ��ˮ�� 1 ���� 2 סԺ�� 3 ���￨�� 4 ������ 5 ҽ��֤�� 6 ҽ����� 7 �Ա� M,F,O,U 8 ���֤��
                    //9 ƴ�� 10 ���� 11 ְҵ���� 12 ְҵ����(���ݿ����ֶ�) 13������λ 14������λ�绰 15��λ�ʱ�Memo 16���ڻ��ͥ��ַ
                    //17��ͥ�绰 18���ڻ��ͥ�ʱ� 19����id�����ݿ����ֶΣ� 20����name 21�����ر���   
                    //22���������ƣ����ݿ����ֶΣ� 23����id 24�������ƣ����ݿ����ֶΣ� 25��ϵ��ID�����ݿ����ֶΣ� 26��ϵ������    
                    //27��ϵ�˵绰   28��ϵ�˵�ַ 29��ϵ�˹�ϵID 30��ϵ�˹�ϵ���ƣ����ݿ����ֶΣ� 31����״��id 32����״�����ƣ����ݿ����ֶΣ� 
                    //33����id 34�������ƣ����ݿ����ֶΣ� 35��� 36���� 37Ѫѹ 38Ѫ�ͱ��� 39�ش󼲲���־ 1:��  0:�� 40������־ 1��  0:�� 
                    //41��Ժ���� 42���Ҵ��� 43�������� 44������� 01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸� 
                    //45����������ƣ����ݿ����ֶΣ� 46��ͬ���� 47��ͬ��λ���� 48���� 49����Ԫ���� 50����Ԫ���� 51ҽʦ����(סԺ) 
                    //52ҽʦ����(סԺ) 53ҽʦ����(����) 54ҽʦ����(����) 55ҽʦ����(����) 56ҽʦ����(����) 57ҽ�����루ʵϰ�������ݿ����ֶΣ� 
                    //58ҽ��������ʵϰ�������ݿ����ֶΣ� 59��ʿ����(����) 60��ʿ����(����) 61��Ժ���ID 62��Ժ���Name�����ݿ����ֶΣ� 
                    //63��Ժ;��ID 64��Ժ;��Name�����ݿ����ֶΣ� 65��Ժ��ԴID 1:���2:���3:ת�ƣ�4:תԺ 66��Ժ��ԴName�����ݿ����ֶΣ� 
                    //67��Ժ״̬ R-סԺ�Ǽ�  I �������� B-��Ժ�Ǽ� O-��Ժ���� P-ԤԼ��Ժ,N-�޷���Ժ C ȡ��  68��Ժ����(ԤԼ)  
                    //69��Ժ���� 70�Ƿ���ICU 71ICU���루���ݿ����ֶΣ� 72ICU���ƣ����ݿ����ֶΣ� 73¥�����ݿ����ֶΣ� 74�㣨���ݿ����ֶΣ� 
                    //75���䣨���ݿ����ֶΣ� 76���ý��(δ��) 77�Էѽ��(δ��) 78�Ը����(δ��) 79���ѽ��(δ��) 80���(δ��) 
                    //81�Żݽ��(δ��) 82Ԥ�����(δ��) 83���ý��(�ѽ�) 84Ԥ�����(�ѽ�) 85��������(�ϴ�) 86������ 87ת����� 
                    //88ת��Ԥ���� 89ת����� 90��λ״̬ 
                    //91�������޶�겿�� 92��ע 93���޶� 94Ѫ���ɽ� 95��Ժ���� 96��λ���� 97�յ����� 98������� 99��סҽ������ 
                    //100�������յ��Ժ� 101�Ƿ���Ӥ�� 102������  103�̶����ü������ 104�ϴι̶�����ʱ�� 105���ѳ��괦�� 0���겻��1�������� 
                    //106���ѻ������޶��ۼ� 107����״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4������� 
                    //108�Ƿ��������޶�� 0 ��ͬ�� 1 ͬ�� ��ɽһԺ���� 109��չ���1 110��չ���2 111��ʳ�����ܶ� 112��ʳԤ����� 
                    //113��ʳ����״̬��0��Ժ 1��Ժ 114�Էѱ��� 115�Ը����� 116���ѻ��߹���ҩƷ�ۼ�(���޶�) 117סԺ���������  
                    //118�Ƿ���� 119���� 120������� 121���飺0 ��ͨ 1 ���� 2 ��Σ       122�Ƿ���� 123�������ͣ�M ��� Dʱ��� 
                    //124�����߿�ʼʱ�� 125�����߽���ʱ��

                    #endregion

                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                        if (!Reader.IsDBNull(2)) PatientInfo.PID.PatientNO = Reader[2].ToString(); //  סԺ��
                        if (!Reader.IsDBNull(3)) PatientInfo.PID.CardNO = Reader[3].ToString(); //���￨��
                        if (!Reader.IsDBNull(4)) PatientInfo.PID.CaseNO = Reader[4].ToString(); // ������
                        if (!Reader.IsDBNull(5)) PatientInfo.SSN = Reader[5].ToString(); // ҽ��֤��
                        if (!Reader.IsDBNull(6)) PatientInfo.PVisit.MedicalType.ID = Reader[6].ToString(); //   ҽ�����id
                        if (!Reader.IsDBNull(7)) PatientInfo.Sex.ID = Reader[7].ToString(); //  �Ա�
                        if (!Reader.IsDBNull(8)) PatientInfo.IDCard = Reader[8].ToString(); //  ���֤��
                        if (!Reader.IsDBNull(9)) PatientInfo.Memo = Reader[9].ToString(); //  ƴ��
                        if (!Reader.IsDBNull(10)) PatientInfo.Birthday = NConvert.ToDateTime(Reader[10]); // ����
                        if (!Reader.IsDBNull(11)) PatientInfo.Profession.ID = Reader[11].ToString(); //  ְҵ����
                        if (!Reader.IsDBNull(12)) PatientInfo.Profession.Name = Reader[12].ToString(); //ְҵ����
                        if (!Reader.IsDBNull(13)) PatientInfo.CompanyName = Reader[13].ToString(); //  ������λ
                        if (!Reader.IsDBNull(14)) PatientInfo.PhoneBusiness = Reader[14].ToString(); //  ������λ�绰
                        if (!Reader.IsDBNull(15)) PatientInfo.BusinessZip = Reader[15].ToString(); //  ��λ�ʱ�
                        if (!Reader.IsDBNull(16)) PatientInfo.AddressHome = Reader[16].ToString(); //  ���ڻ��ͥ��ַ
                        if (!Reader.IsDBNull(17)) PatientInfo.PhoneHome = Reader[17].ToString(); //  ��ͥ�绰
                        if (!Reader.IsDBNull(18)) PatientInfo.HomeZip = Reader[18].ToString(); //  ���ڻ��ͥ�ʱ�
                        //19����ID
                        if (!Reader.IsDBNull(20)) PatientInfo.DIST = Reader[20].ToString(); // ����name
                        if (!Reader.IsDBNull(21)) PatientInfo.AreaCode = Reader[21].ToString(); //  �����ش���
                        //22����������
                        if (!Reader.IsDBNull(23)) PatientInfo.Nationality.ID = Reader[23].ToString(); //  ����id
                        if (!Reader.IsDBNull(24)) PatientInfo.Nationality.Name = Reader[24].ToString(); // ����name
                        if (!Reader.IsDBNull(25)) PatientInfo.Kin.ID = Reader[25].ToString(); //  ��ϵ��id
                        if (!Reader.IsDBNull(26)) PatientInfo.Kin.Name = Reader[26].ToString(); //  ��ϵ������
                        if (!Reader.IsDBNull(27)) PatientInfo.Kin.RelationPhone = Reader[27].ToString(); //  ��ϵ�˵绰
                        if (!Reader.IsDBNull(28)) PatientInfo.Kin.RelationAddress = Reader[28].ToString(); //  ��ϵ�˵�ַ
                        if (!Reader.IsDBNull(29)) PatientInfo.Kin.Relation.ID = Reader[29].ToString(); //  ��ϵ�˹�ϵid
                        if (!Reader.IsDBNull(30)) PatientInfo.Kin.Relation.Name = Reader[30].ToString(); //  ��ϵ�˹�ϵname
                        if (!Reader.IsDBNull(31)) PatientInfo.MaritalStatus.ID = Reader[31].ToString(); //  ����״��id
                        //32����״������
                        if (!Reader.IsDBNull(33)) PatientInfo.Country.ID = Reader[33].ToString(); //  ����id
                        if (!Reader.IsDBNull(34)) PatientInfo.Country.Name = Reader[34].ToString(); //��������
                        if (!Reader.IsDBNull(35)) PatientInfo.Height = Reader[35].ToString(); //  ���
                        if (!Reader.IsDBNull(36)) PatientInfo.Weight = Reader[36].ToString(); //  ����
                        //37Ѫѹ
                        if (!Reader.IsDBNull(38)) PatientInfo.BloodType.ID = Reader[38]; // ABOѪ��
                        //if (!Reader.IsDBNull(38)) PatientInfo.BloodType.Name = Reader[38].ToString();
                        if (!Reader.IsDBNull(39)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[39]); //  �ش󼲲���־
                        if (!Reader.IsDBNull(40)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[40]); //  ������־
                        if (!Reader.IsDBNull(41)) PatientInfo.PVisit.InTime = NConvert.ToDateTime(Reader[41]); //  ��Ժ����
                        if (!Reader.IsDBNull(42)) PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[42].ToString(); //  ���Ҵ���
                        if (!Reader.IsDBNull(43)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[43].ToString(); // ��������
                        if (!Reader.IsDBNull(44)) PatientInfo.Pact.PayKind.ID = Reader[44].ToString();  // �������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
                        if (!Reader.IsDBNull(45)) PatientInfo.Pact.PayKind.Name = Reader[45].ToString(); //  �����������
                        if (!Reader.IsDBNull(46)) PatientInfo.Pact.ID = Reader[46].ToString(); //��ͬ����
                        if (!Reader.IsDBNull(47)) PatientInfo.Pact.Name = Reader[47].ToString(); // ��ͬ��λ����
                        if (!Reader.IsDBNull(48)) PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[48].ToString(); // ����
                        if (!Reader.IsDBNull(48))
                            PatientInfo.PVisit.PatientLocation.Bed.Name = PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4
                                                                            ? PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4)
                                                                            : PatientInfo.PVisit.PatientLocation.Bed.ID; // ����                        
                        if (!Reader.IsDBNull(49)) PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[49].ToString(); //����Ԫ����
                        if (!Reader.IsDBNull(50)) PatientInfo.PVisit.PatientLocation.NurseCell.Name = Reader[50].ToString(); // ����Ԫ����
                        if (!Reader.IsDBNull(51)) PatientInfo.PVisit.AdmittingDoctor.ID = Reader[51].ToString(); //ҽʦ����(סԺ)
                        if (!Reader.IsDBNull(52)) PatientInfo.PVisit.AdmittingDoctor.Name = Reader[52].ToString(); //ҽʦ����(סԺ)
                        if (!Reader.IsDBNull(53)) PatientInfo.PVisit.AttendingDoctor.ID = Reader[53].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(54)) PatientInfo.PVisit.AttendingDoctor.Name = Reader[54].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(55)) PatientInfo.PVisit.ConsultingDoctor.ID = Reader[55].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(56)) PatientInfo.PVisit.ConsultingDoctor.Name = Reader[56].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(57)) PatientInfo.PVisit.TempDoctor.ID = Reader[57].ToString(); //ҽʦ����(ʵϰ)
                        if (!Reader.IsDBNull(58)) PatientInfo.PVisit.TempDoctor.Name = Reader[58].ToString(); //ҽʦ����(ʵϰ)
                        if (!Reader.IsDBNull(59)) PatientInfo.PVisit.AdmittingNurse.ID = Reader[59].ToString(); // ��ʿ����(����)
                        if (!Reader.IsDBNull(60)) PatientInfo.PVisit.AdmittingNurse.Name = Reader[60].ToString(); // ��ʿ����(����)
                        if (!Reader.IsDBNull(61)) PatientInfo.PVisit.Circs.ID = Reader[61].ToString(); // ��Ժ���id
                        if (!Reader.IsDBNull(62)) PatientInfo.PVisit.Circs.Name = Reader[62].ToString(); // ��Ժ���name
                        if (!Reader.IsDBNull(63)) PatientInfo.PVisit.AdmitSource.ID = Reader[63].ToString(); // ��Ժ;��id
                        if (!Reader.IsDBNull(64)) PatientInfo.PVisit.AdmitSource.Name = Reader[64].ToString(); // ��Ժ;��name
                        if (!Reader.IsDBNull(65)) PatientInfo.PVisit.InSource.ID = Reader[65].ToString();   // ��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ
                        if (!Reader.IsDBNull(66)) PatientInfo.PVisit.InSource.Name = Reader[66].ToString(); // ��Ժ��Դname
                        if (!Reader.IsDBNull(67)) PatientInfo.PVisit.InState.ID = Reader[67].ToString();    // ��Ժ״̬ סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
                        if (!Reader.IsDBNull(68)) PatientInfo.PVisit.PreOutTime = NConvert.ToDateTime(Reader[68]); // ��Ժ����(ԤԼ)
                        #region {8D72F2C7-624C-41e4-9922-7A5556B9D82E}
                        if (!Reader.IsDBNull(69))
                        {
                            if (NConvert.ToDateTime(Reader[69]) < NConvert.ToDateTime("1000-01-01"))
                            {
                                PatientInfo.PVisit.OutTime = DateTime.MinValue;
                            }
                            else//{3D0766DE-A5AA-409f-8A04-C56F4C9D53DA}
                            {
                                PatientInfo.PVisit.OutTime = NConvert.ToDateTime(Reader[69]);
                            }

                        }
                        #endregion
                        //70�Ƿ�ICU
                        if (!Reader.IsDBNull(71)) PatientInfo.PVisit.ICULocation.ID = Reader[71].ToString(); //ICU����
                        if (!Reader.IsDBNull(72)) PatientInfo.PVisit.ICULocation.Name = Reader[72].ToString(); //ICU����
                        if (!Reader.IsDBNull(73)) PatientInfo.PVisit.PatientLocation.Building = Reader[73].ToString(); //¥
                        if (!Reader.IsDBNull(74)) PatientInfo.PVisit.PatientLocation.Floor = Reader[74].ToString(); //��
                        if (!Reader.IsDBNull(75)) PatientInfo.PVisit.PatientLocation.Room = Reader[75].ToString(); //��
                        if (!Reader.IsDBNull(76)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[76]); //�ܹ����TotCost
                        if (!Reader.IsDBNull(77)) PatientInfo.FT.OwnCost = NConvert.ToDecimal(Reader[77]); //�Էѽ�� OwnCost
                        if (!Reader.IsDBNull(78)) PatientInfo.FT.PayCost = NConvert.ToDecimal(Reader[78]); //�Ը���� PayCost
                        if (!Reader.IsDBNull(79)) PatientInfo.FT.PubCost = NConvert.ToDecimal(Reader[79]); //���ѽ�� PubCost
                        if (!Reader.IsDBNull(80)) PatientInfo.FT.LeftCost = NConvert.ToDecimal(Reader[80]); //ʣ���� LeftCost
                        if (!Reader.IsDBNull(81)) PatientInfo.FT.RebateCost = NConvert.ToDecimal(Reader[81]); //�Żݽ��
                        if (!Reader.IsDBNull(82)) PatientInfo.FT.PrepayCost = NConvert.ToDecimal(Reader[82]); //Ԥ�����
                        if (!Reader.IsDBNull(83)) PatientInfo.FT.BalancedCost = NConvert.ToDecimal(Reader[83]); //���ý��(�ѽ�)
                        if (!Reader.IsDBNull(84)) PatientInfo.FT.BalancedPrepayCost = NConvert.ToDecimal(Reader[84]); //Ԥ�����(�ѽ�)
                        if (!Reader.IsDBNull(85))
                        {
                            try
                            {
                                PatientInfo.BalanceDate = NConvert.ToDateTime(Reader[85]); //����ʱ��
                            }
                            catch { }
                        }
                        if (!Reader.IsDBNull(86)) PatientInfo.PVisit.MoneyAlert = NConvert.ToDecimal(Reader[86]); //������
                        if (!Reader.IsDBNull(87)) PatientInfo.PVisit.ZG.ID = Reader[87].ToString(); //ת�����
                        if (!Reader.IsDBNull(88)) PatientInfo.FT.TransferPrepayCost = NConvert.ToDecimal(Reader[88]); // ת��Ԥ����δ��) 
                        if (!Reader.IsDBNull(89)) PatientInfo.FT.TransferTotCost = NConvert.ToDecimal(Reader[89]); //  ת����ý�δ��)
                        if (!Reader.IsDBNull(90)) PatientInfo.PVisit.PatientLocation.Bed.Status.ID = Reader[90].ToString(); //��λ״̬
                        if (!Reader.IsDBNull(0)) PatientInfo.PVisit.PatientLocation.Bed.InpatientNO = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(91)) PatientInfo.FT.OvertopCost = NConvert.ToDecimal(Reader[91]); //   ���ѳ�����
                        if (!Reader.IsDBNull(92)) PatientInfo.Memo = Reader[92].ToString(); //��ע
                        if (!Reader.IsDBNull(93)) PatientInfo.FT.DayLimitCost = NConvert.ToDecimal(Reader[93]); //   �������޶�
                        if (!Reader.IsDBNull(94)) PatientInfo.FT.BloodLateFeeCost = NConvert.ToDecimal(Reader[94]); //  Ѫ���ɽ�
                        if (!Reader.IsDBNull(95)) PatientInfo.InTimes = NConvert.ToInt32(Reader[95]); //סԺ����
                        if (!Reader.IsDBNull(96)) PatientInfo.FT.BedLimitCost = NConvert.ToDecimal(Reader[96].ToString()); //��λ����
                        if (!Reader.IsDBNull(97)) PatientInfo.FT.AirLimitCost = NConvert.ToDecimal(Reader[97].ToString()); //�յ�����                        
                        if (!Reader.IsDBNull(98)) PatientInfo.ClinicDiagnose = Reader[98].ToString();//�������
                        if (!Reader.IsDBNull(99)) PatientInfo.DoctorReceiver.ID = Reader[99].ToString();//��סҽ������
                        if (!Reader.IsDBNull(100)) PatientInfo.ProCreateNO = Reader[100].ToString(); //�������յ��Ժ�
                        PatientInfo.IsHasBaby = NConvert.ToBoolean(Reader[101].ToString()); //�Ƿ���Ӥ��
                        PatientInfo.Disease.Tend.Name = Reader[102].ToString(); //������                        
                        PatientInfo.FT.FixFeeInterval = NConvert.ToInt32(Reader[103].ToString());//�̶�������ȡ�������
                        PatientInfo.FT.PreFixFeeDateTime = NConvert.ToDateTime(Reader[104].ToString());//�ϴι̶�������ȡʱ��
                        PatientInfo.FT.BedOverDeal = Reader[105].ToString();//���ѳ��괦�� 0���겻��1��������
                        PatientInfo.FT.DayLimitTotCost = NConvert.ToDecimal(Reader[106].ToString());//���޶��ۼ�
                        PatientInfo.CaseState = Reader[107].ToString();//����״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4�������
                        PatientInfo.ExtendFlag = Reader[108].ToString(); //�Ƿ��������޶�� 0 ��ͬ�� 1 ͬ�� ��ɽһԺ����
                        PatientInfo.ExtendFlag1 = Reader[109].ToString(); //��չ���1
                        PatientInfo.ExtendFlag2 = Reader[110].ToString(); //��չ���2
                        PatientInfo.FT.BoardCost = NConvert.ToDecimal(Reader[111]); //��ʳ�����ܶ�
                        PatientInfo.FT.BoardPrepayCost = NConvert.ToDecimal(Reader[112]); //��ʳԤ�����
                        PatientInfo.PVisit.BoardState = Reader[113].ToString(); //��ʳ����״̬��0��Ժ 1��Ժ
                        PatientInfo.FT.FTRate.OwnRate = NConvert.ToDecimal(Reader[114].ToString()); //�Էѱ���
                        PatientInfo.FT.FTRate.PayRate = NConvert.ToDecimal(Reader[115].ToString()); //�Ը�����
                        PatientInfo.FT.DrugFeeTotCost = NConvert.ToDecimal(Reader[116].ToString()); //���ѻ��߹���ҩƷ�ۼ�(���޶�)
                        PatientInfo.MainDiagnose = Reader[117].ToString(); //����סԺ�����
                        //{C5CCA8E3-2893-4268-8680-76242D2CE05A}
                        //{524298EE-2A42-4487-B472-51AA204C0196}���㵽��
                        PatientInfo.Age = GetAge2(PatientInfo.ID); //���ݳ�������ȡ��������
                        //PatientInfo.Age = GetAge1(PatientInfo.Birthday, DateTime.Now, false); //���ݳ�������ȡ��������
                        PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[118].ToString());
                        PatientInfo.NormalName = Reader[119].ToString();
                        if (!Reader.IsDBNull(120)) PatientInfo.BalanceNO = NConvert.ToInt32(Reader[120].ToString());//�������
                        //{F0C48258-8EFB-4356-B730-E852EE4888A0}
                        PatientInfo.ExtendFlag1 = Reader[121].ToString();//����


                        if (PatientInfo.PID.PatientNO.StartsWith("B"))
                        {
                            //�Ƿ�Ӥ��
                            PatientInfo.IsBaby = true;
                        }
                        else
                        {
                            PatientInfo.IsBaby = false;
                        }

                        //{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
                        if (!Reader.IsDBNull(122))
                        {
                            PatientInfo.IsStopAcount = FS.FrameWork.Function.NConvert.ToBoolean(Reader[122].ToString());//�Ƿ����
                        }
                        PatientInfo.PVisit.AlertType.ID = this.Reader[123].ToString();//�������ͣ�M ��� Dʱ���
                        PatientInfo.PVisit.BeginDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[124]);//�����߿�ʼʱ��
                        PatientInfo.PVisit.EndDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[125]);//�����߽���ʱ��
                        if (!Reader.IsDBNull(126)) PatientInfo.AddressBusiness = this.Reader[126].ToString();//��סַ
                        if (Reader.FieldCount > 127)
                        {
                            if (!Reader.IsDBNull(127))
                            {
                                PatientInfo.PVisit.AlertFlag = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[127].ToString());//�Ƿ����þ�����
                            }
                        }

                        if (Reader.FieldCount > 128)
                        {
                            if (!Reader.IsDBNull(128))
                            {
                                PatientInfo.AllergyInfo = this.Reader[128].ToString();//����Դ
                            }
                        }
                        if (Reader.FieldCount > 129)
                        {
                            if (!Reader.IsDBNull(129))// {F6204EF5-F295-4d91-B81A-736A268DD394}
                            {
                                PatientInfo.PatientType.ID = this.Reader[129].ToString();//�������� 
                            }
                        }
                        //if (Reader.FieldCount > 130)
                        //{
                        //    if (!Reader.IsDBNull(130))
                        //    {
                        //        PatientInfo.IsPtjtState = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[130].ToString());//�Ƿ���·������
                        //    }
                        //}                                               
                        //if (Reader.FieldCount > 131)
                        //{
                        //    if (!Reader.IsDBNull(131))
                        //    {
                        //        PatientInfo.PVisit.HealthCareType = this.Reader[131].ToString();//ҽ������
                        //    }
                        //}

                        if (Reader.FieldCount > 130)
                        {
                            if (!Reader.IsDBNull(130))
                            {
                                PatientInfo.PVisit.ResponsibleDoctor.ID = this.Reader[130].ToString();//����ҽʦ����
                            }
                        }
                        if (Reader.FieldCount > 131)
                        {
                            if (!Reader.IsDBNull(131))
                            {
                                PatientInfo.PVisit.ResponsibleDoctor.Name = this.Reader[131].ToString();//����ҽʦ����
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                        return null;
                    }
                    //��ñ����Ϣ

                    #region "��ñ����Ϣ"

                    //deleted by cuipeng 2005-5 ��֪���˹���Ϊɶ��,����������
                    //this.myGetTempLocation(PatientInfo);

                    #endregion

                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }
            } //�׳�����
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return al;
        }




        /// <summary>
        /// ��ȡ��������{1C22561B-E2E1-4372-A1EE-07B557A5BF6C}
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public string GetAge2(string INPATIENT_NO)
        {
            string sql = @"select fun_get_age_new_emr(t.birthday,(select oper_date from com_shiftdata a
                            where a.clinic_no = t.inpatient_no
                            and a.happen_no =  (select max(a.happen_no)  from com_shiftdata a
                            where a.clinic_no = t.inpatient_no
                            and a.shift_type = 'K'))) from fin_ipr_inmaininfo t where t.inpatient_no='{0}'  ";
            try
            {
                sql = string.Format(sql, INPATIENT_NO);
            }
            catch
            {
                Err = "��ȡ��������sql����";
                WriteErr();
                return "-1";
            }
            return ExecSqlReturnOne(sql);
        }



        /// <summary>
        /// �������
        /// </summary>
        /// <param name="birthday"></param>
        /// <param name="sysDate"></param>
        /// <returns></returns>
        public string GetAge1(DateTime birthday, DateTime sysDate, bool detailFormat)
        {
            string result;
            // TimeSpan ts = sysDate - birthday;

            try
            {
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                if (sysDate > birthday)
                {
                    if (birthday.Year <= 1800)
                    {
                        result = "δ֪";
                        return result;
                    }
                    num = sysDate.Year - birthday.Year;
                    if (num < 0)
                    {
                        num = 0;
                    }
                    num2 = sysDate.Month - birthday.Month;
                    if (num2 < 0)
                    {
                        if (num > 0)
                        {
                            num--;
                            DateTime dateTime = new DateTime(birthday.Year + 1, 1, 1);
                            num2 = dateTime.AddMonths(-1).Month + num2;
                        }
                        if (num2 < 0)
                        {
                            num2 = 0;
                        }
                    }
                    //{006E13C1-572B-49EE-A9E4-5FF872E99507}�������� С��1�����Ҫ��ʾСʱ
                    num3 = sysDate.Day - birthday.Day;
                    double numday = (sysDate - birthday).TotalDays;
                    if (numday < 1) num3 = 0;
                    if (num3 < 0)
                    {
                        if (num2 > 0)
                        {
                            num2--;
                            DateTime dateTime = new DateTime(birthday.Year, birthday.Month, 1).AddMonths(1);
                            num3 = dateTime.AddDays(-1.0).Day + num3;
                        }
                        else if (num > 0)
                        {
                            num--;
                            DateTime dateTime = new DateTime(birthday.Year + 1, 1, 1);
                            num2 = dateTime.AddMonths(-1).Month - 1;
                            dateTime = new DateTime(birthday.Year, birthday.Month, 1).AddMonths(1);
                            num3 = dateTime.AddDays(-1.0).Day + num3;
                        }
                        else
                        {
                            num3 = 0;
                        }
                    }
                }
                if (num == 0 && num2 == 0 && num3 == 0)
                {
                    TimeSpan timeSpan = sysDate - birthday;
                    if (timeSpan.TotalHours < 0.0)
                    {
                        num4 = 0;
                    }
                    else if (timeSpan.TotalHours < 1.0)
                    {
                        if (timeSpan.TotalMinutes < 0.0)
                        {
                            num5 = 0;
                        }
                        else
                        {
                            num5 = int.Parse(Math.Floor(timeSpan.TotalMinutes).ToString());
                        }
                    }
                    else
                    {
                        num4 = int.Parse(Math.Floor(timeSpan.TotalHours).ToString());
                        if (timeSpan.TotalMinutes < 0.0)
                        {
                            num5 = 0;
                        }
                        else
                        {
                            num5 = int.Parse(Math.Floor(timeSpan.TotalMinutes).ToString());
                        }
                    }
                }
                if (detailFormat)
                {
                    result = string.Concat(new string[]
                    {
                        num.ToString().PadLeft(1, ' '),
                        Language.Msg("��"),
                        num2.ToString().PadLeft(1, ' '),
                        Language.Msg("��"),
                        num3.ToString().PadLeft(1, ' '),
                        Language.Msg("��"),
                        num4.ToString().PadLeft(1, ' '),
                        Language.Msg("Сʱ"),
                        num5.ToString().PadLeft(1, ' '),
                        Language.Msg("��")
                    });
                }
                else if (num >= Database.DetailAgeBoundry)
                {
                    result = num.ToString().PadLeft(1, ' ') + Language.Msg("��");
                }
                else if (num >= 1)
                {
                    if (num2 == 0)
                    {
                        result = num.ToString().PadLeft(1, ' ') + Language.Msg("��");
                    }
                    else
                    {
                        result = num.ToString().PadLeft(1, ' ') + Language.Msg("��") + num2.ToString().PadLeft(1, ' ') + Language.Msg("��");
                    }
                }
                else if (num2 == 0 && num3 == 0)
                {
                    TimeSpan timeSpan = sysDate - birthday;
                    if (timeSpan.TotalHours < 0.0)
                    {
                        num4 = 0;
                    }
                    else
                    {
                        if (timeSpan.TotalHours < 1.0)
                        {
                            if (timeSpan.TotalMinutes < 0.0)
                            {
                                num5 = 0;
                            }
                            else
                            {
                                num5 = int.Parse(Math.Floor(timeSpan.TotalMinutes).ToString());
                            }
                            result = num5.ToString().PadLeft(1, ' ') + Language.Msg("��");
                            return result;
                        }
                        num4 = int.Parse(Math.Floor(timeSpan.TotalHours).ToString());
                    }
                    result = num4.ToString().PadLeft(1, ' ') + Language.Msg("Сʱ");
                    if (timeSpan.Minutes > 0)
                    {
                        result += timeSpan.Minutes.ToString().PadLeft(1, ' ') + Language.Msg("��");
                    }
                }
                else if (num2 == 0)
                {
                    result = num3.ToString().PadLeft(1, ' ') + Language.Msg("��");
                }
                else if (num3 == 0)
                {
                    result = num2.ToString().PadLeft(1, ' ') + Language.Msg("��");
                }
                else
                {
                    result = num2.ToString().PadLeft(1, ' ') + Language.Msg("��") + num3.ToString().PadLeft(1, ' ') + Language.Msg("��");
                }
            }
            catch
            {
                result = "";
            }
            return result;
        }

        #endregion


        //add by zhy ��ʾ�ն˿���δȷ�ϵķ���(ҽ��վ)
        #region
        private decimal myPatientTerminalFeeQuery(string SQLPatient)
        {

            decimal terminalcost = 0;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;

            if (ExecQuery(SQLPatient) == -1)
            {
                return 0;
            }

            try
            {
                while (Reader.Read())
                {

                    try
                    {
                        //if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // סԺ��ˮ��
                        //if (!Reader.IsDBNull(1)) PatientInfo.PID.PatientNO = Reader[1].ToString(); // סԺ��ˮ��
                        //if (!Reader.IsDBNull(2)) PatientInfo.Name = Reader[2].ToString();//��������
                        //if (!Reader.IsDBNull(3)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[3].ToString();
                        //if (!Reader.IsDBNull(4)) PatientInfo.FT.TerminalCost = NConvert.ToDecimal(Reader[4].ToString()); // 
                        if (!Reader.IsDBNull(4)) terminalcost = NConvert.ToDecimal(Reader[4].ToString());
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                // return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return terminalcost;
        }
        #endregion

        //add by zhy ��ʾ�ն˿���δȷ�ϵķ���
        #region
        private ArrayList myPatientQueryNew1(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;

            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }

            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new PatientInfo();

                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(1)) PatientInfo.PID.PatientNO = Reader[1].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(2)) PatientInfo.Name = Reader[2].ToString();//��������
                        if (!Reader.IsDBNull(3)) PatientInfo.PVisit.PatientLocation.NurseCell.Name = Reader[3].ToString();
                        if (!Reader.IsDBNull(4)) PatientInfo.FT.TerminalCost = NConvert.ToDecimal(Reader[4].ToString()); // 
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                        return null;
                    }

                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }
            }
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return al;
        }
        #endregion

        #region �������Sql����ѯ������Ϣ�б�������ʳ���--˽��

        private ArrayList myPatientQueryNew(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;

            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            //ȡϵͳʱ��,�����õ������ַ���
            DateTime sysDate = GetDateTimeFromSysDateTime();

            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new PatientInfo();

                    #region "�ӿ�˵��"

                    //<!-- 0  סԺ��ˮ��,1 ���� ,2   סԺ��   ,3 ���￨��  ,4  ������, 5  ҽ��֤��
                    //,6    ҽ�����,   7   �Ա�   ,8   ���֤��  ,9   ƴ��     ,10  ����
                    //,11   ְҵ����     ,12 ְҵ����,13   ������λ    ,14   ������λ�绰      ,15   ��λ�ʱ�
                    //,16   ���ڻ��ͥ��ַ     ,17   ��ͥ�绰   ,18   ���ڻ��ͥ�ʱ�   , 19  ����id,20  ����name
                    //,21   �����ش���    , 22 ����������   ,23   ����id    ,24  ����name    ,25   ��ϵ��id
                    //,26   ��ϵ������    ,27   ��ϵ�˵绰       ,28   ��ϵ�˵�ַ     ,29   ��ϵ�˹�ϵid , 30   ��ϵ�˹�ϵname
                    //,31   ����״��id    ,32  ����״��name  ,33   ����id    , 34 ��������
                    //,35   ���           ,36   ����         ,37   Ѫѹ      ,38   ABOѪ��
                    //,39   �ش󼲲���־    ,40   ������־            
                    //,41   ��Ժ����      ,42   ���Ҵ���   , 43  ��������  , 44  �������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
                    //,45   �����������   , 46 ��ͬ����   , 47  ��ͬ��λ����  , 48  ����
                    //, 49 ����Ԫ����  , 50  ����Ԫ����, 51 ҽʦ����(סԺ), 52 ҽʦ����(סԺ)
                    //, 53 ҽʦ����(����) , 54 ҽʦ����(����) , 55 ҽʦ����(����) , 56 ҽʦ����(����)
                    //, 57 ҽʦ����(ʵϰ) , 58 ҽʦ����(ʵϰ), 59  ��ʿ����(����), 60  ��ʿ����(����)
                    //, 61  ��Ժ���id  , 62  ��Ժ���name   , 63  ��Ժ;��id    , 64  ��Ժ;��name      
                    //, 65  ��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ    , 66  ��Ժ��Դname
                    //, 67  ��Ժ״̬ סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
                    //,  68  ��Ժ����(ԤԼ)  , 69  ��Ժ���� , 70  �Ƿ���ICU 0 no 1 yes,71 icu code,72 icu name
                    //,73 ¥ ,74 ��,75 �� 
                    //,76 �ܹ����TotCost ,77 �Էѽ�� OwnCost,	78 �Ը���� PayCost,79 ���ѽ�� PubCost
                    //,80 ʣ���� LeftCost,81 �Żݽ��
                    //,82  Ԥ����� ��83    ���ý��(�ѽ�)��84    Ԥ�����(�ѽ�) �� 85 ��������(�ϴ�)     
                    //��86 ������, 87 ת�����,88 TransferPrepayCost ת��Ԥ����δ��)  ,89 ת����ý��(δ��),90 ����״̬91�������޶�겿��
                    //,92 ��ע93�������޶�94Ѫ���ɽ�95סԺ����96��λ����97�յ�����98�������99��סҽʦ100�������յ��Ժ�
                    //-->

                    #endregion

                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //����
                        if (!Reader.IsDBNull(2)) PatientInfo.PID.PatientNO = Reader[2].ToString(); //  סԺ��						
                        if (!Reader.IsDBNull(3)) PatientInfo.PID.CardNO = Reader[3].ToString(); //���￨��
                        if (!Reader.IsDBNull(4)) PatientInfo.PID.CaseNO = Reader[4].ToString(); // ������
                        if (!Reader.IsDBNull(5)) PatientInfo.SSN = Reader[5].ToString(); // ҽ��֤��
                        if (!Reader.IsDBNull(6)) PatientInfo.PVisit.MedicalType.ID = Reader[6].ToString(); //   ҽ�����id
                        if (!Reader.IsDBNull(7)) PatientInfo.Sex.ID = Reader[7].ToString(); //  �Ա�
                        if (!Reader.IsDBNull(8)) PatientInfo.IDCard = Reader[8].ToString(); //  ���֤��
                        if (!Reader.IsDBNull(9)) PatientInfo.Memo = Reader[9].ToString(); //  ƴ��
                        if (!Reader.IsDBNull(10)) PatientInfo.Birthday = NConvert.ToDateTime(Reader[10]); // ����
                        if (!Reader.IsDBNull(11)) PatientInfo.Profession.ID = Reader[11].ToString(); //  ְҵ����
                        if (!Reader.IsDBNull(12)) PatientInfo.Profession.Name = Reader[12].ToString(); //ְҵ����
                        if (!Reader.IsDBNull(13)) PatientInfo.CompanyName = Reader[13].ToString(); //  ������λ
                        if (!Reader.IsDBNull(14)) PatientInfo.PhoneBusiness = Reader[14].ToString(); //  ������λ�绰
                        if (!Reader.IsDBNull(15)) PatientInfo.User01 = Reader[15].ToString(); //  ��λ�ʱ�
                        if (!Reader.IsDBNull(16)) PatientInfo.AddressHome = Reader[16].ToString(); //  ���ڻ��ͥ��ַ
                        if (!Reader.IsDBNull(17)) PatientInfo.PhoneHome = Reader[17].ToString(); //  ��ͥ�绰
                        if (!Reader.IsDBNull(18)) PatientInfo.User02 = Reader[18].ToString(); //  ���ڻ��ͥ�ʱ�
                        if (!Reader.IsDBNull(20)) PatientInfo.DIST = Reader[20].ToString(); // ����name
                        if (!Reader.IsDBNull(21)) PatientInfo.AreaCode = Reader[21].ToString(); //  �����ش���
                        if (!Reader.IsDBNull(23)) PatientInfo.Nationality.ID = Reader[23].ToString(); //  ����id
                        if (!Reader.IsDBNull(24)) PatientInfo.Nationality.Name = Reader[24].ToString(); // ����name
                        if (!Reader.IsDBNull(25)) PatientInfo.Kin.ID = Reader[25].ToString(); //  ��ϵ��id
                        if (!Reader.IsDBNull(26)) PatientInfo.Kin.Name = Reader[26].ToString(); //  ��ϵ������
                        if (!Reader.IsDBNull(27)) PatientInfo.Kin.RelationPhone = Reader[27].ToString(); //  ��ϵ�˵绰
                        if (!Reader.IsDBNull(28)) PatientInfo.Kin.RelationAddress = Reader[28].ToString(); //  ��ϵ�˵�ַ
                        if (!Reader.IsDBNull(29)) PatientInfo.Kin.Relation.ID = Reader[29].ToString(); //  ��ϵ�˹�ϵid
                        if (!Reader.IsDBNull(30)) PatientInfo.Kin.Relation.Name = Reader[30].ToString(); //  ��ϵ�˹�ϵname
                        if (!Reader.IsDBNull(31)) PatientInfo.MaritalStatus.ID = Reader[31].ToString(); //  ����״��id
                        if (!Reader.IsDBNull(33)) PatientInfo.Country.ID = Reader[33].ToString(); //  ����id
                        if (!Reader.IsDBNull(34)) PatientInfo.Country.Name = Reader[34].ToString(); //��������
                        if (!Reader.IsDBNull(35)) PatientInfo.Height = Reader[35].ToString(); //  ���
                        if (!Reader.IsDBNull(36)) PatientInfo.Weight = Reader[36].ToString(); //  ����
                        if (!Reader.IsDBNull(38)) PatientInfo.BloodType.ID = Reader[38].ToString(); //  ABOѪ��
                        if (!Reader.IsDBNull(39)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[39]); //  �ش󼲲���־
                        if (!Reader.IsDBNull(40)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[40]); //  ������־
                        if (!Reader.IsDBNull(41)) PatientInfo.PVisit.InTime = NConvert.ToDateTime(Reader[41]); //  ��Ժ����
                        if (!Reader.IsDBNull(42)) PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[42].ToString(); //  ���Ҵ���
                        if (!Reader.IsDBNull(43)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[43].ToString(); // ��������
                        if (!Reader.IsDBNull(44)) PatientInfo.Pact.PayKind.ID = Reader[44].ToString();
                        // �������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
                        if (!Reader.IsDBNull(45)) PatientInfo.Pact.PayKind.Name = Reader[45].ToString(); //  �����������
                        if (!Reader.IsDBNull(46)) PatientInfo.Pact.ID = Reader[46].ToString(); //��ͬ����
                        if (!Reader.IsDBNull(47)) PatientInfo.Pact.Name = Reader[47].ToString(); // ��ͬ��λ����
                        if (!Reader.IsDBNull(48)) PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[48].ToString(); // ����
                        if (!Reader.IsDBNull(48))
                            PatientInfo.PVisit.PatientLocation.Bed.Name = PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4
                                                                            ? PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4)
                                                                            : PatientInfo.PVisit.PatientLocation.Bed.ID; // ����
                        if (!Reader.IsDBNull(90)) PatientInfo.PVisit.PatientLocation.Bed.Status.ID = Reader[90].ToString(); //��λ״̬
                        if (!Reader.IsDBNull(90)) PatientInfo.PVisit.PatientLocation.Bed.InpatientNO = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(49)) PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[49].ToString(); //����Ԫ����
                        if (!Reader.IsDBNull(50)) PatientInfo.PVisit.PatientLocation.NurseCell.Name = Reader[50].ToString(); // ����Ԫ����
                        if (!Reader.IsDBNull(51)) PatientInfo.PVisit.AdmittingDoctor.ID = Reader[51].ToString(); //ҽʦ����(סԺ)
                        if (!Reader.IsDBNull(52)) PatientInfo.PVisit.AdmittingDoctor.Name = Reader[52].ToString(); //ҽʦ����(סԺ)
                        if (!Reader.IsDBNull(53)) PatientInfo.PVisit.AttendingDoctor.ID = Reader[53].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(54)) PatientInfo.PVisit.AttendingDoctor.Name = Reader[54].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(55)) PatientInfo.PVisit.ConsultingDoctor.ID = Reader[55].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(56)) PatientInfo.PVisit.ConsultingDoctor.Name = Reader[56].ToString(); //ҽʦ����(����)
                        if (!Reader.IsDBNull(57)) PatientInfo.PVisit.TempDoctor.ID = Reader[57].ToString(); //ҽʦ����(ʵϰ)
                        if (!Reader.IsDBNull(58)) PatientInfo.PVisit.TempDoctor.Name = Reader[58].ToString(); //ҽʦ����(ʵϰ)
                        if (!Reader.IsDBNull(59)) PatientInfo.PVisit.AdmittingNurse.ID = Reader[59].ToString(); // ��ʿ����(����)
                        if (!Reader.IsDBNull(60)) PatientInfo.PVisit.AdmittingNurse.Name = Reader[60].ToString(); // ��ʿ����(����)
                        if (!Reader.IsDBNull(61)) PatientInfo.PVisit.Circs.ID = Reader[61].ToString(); // ��Ժ���id
                        if (!Reader.IsDBNull(62)) PatientInfo.PVisit.Circs.Name = Reader[62].ToString(); // ��Ժ���name
                        if (!Reader.IsDBNull(63)) PatientInfo.PVisit.AdmitSource.ID = Reader[63].ToString(); // ��Ժ;��id
                        if (!Reader.IsDBNull(64)) PatientInfo.PVisit.AdmitSource.Name = Reader[64].ToString(); // ��Ժ;��name
                        if (!Reader.IsDBNull(65)) PatientInfo.PVisit.InSource.ID = Reader[65].ToString();
                        // ��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ
                        if (!Reader.IsDBNull(66)) PatientInfo.PVisit.InSource.Name = Reader[66].ToString(); // ��Ժ��Դname
                        if (!Reader.IsDBNull(67)) PatientInfo.PVisit.InState.ID = Reader[67].ToString();
                        // ��Ժ״̬ סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
                        if (!Reader.IsDBNull(68)) PatientInfo.PVisit.PreOutTime = NConvert.ToDateTime(Reader[68]); // ��Ժ����(ԤԼ)
                        #region {8D72F2C7-624C-41e4-9922-7A5556B9D82E}
                        if (!Reader.IsDBNull(69))
                        {
                            if (NConvert.ToDateTime(Reader[69]) < NConvert.ToDateTime("1000-01-01"))
                            {
                                PatientInfo.PVisit.OutTime = DateTime.MinValue;
                            }
                            else//{3D0766DE-A5AA-409f-8A04-C56F4C9D53DA}
                            {
                                PatientInfo.PVisit.OutTime = NConvert.ToDateTime(Reader[69]);
                            }

                        }
                        #endregion
                        if (!Reader.IsDBNull(71)) PatientInfo.PVisit.ICULocation.ID = Reader[71].ToString(); //icu code
                        if (!Reader.IsDBNull(72)) PatientInfo.PVisit.ICULocation.Name = Reader[72].ToString(); //icu name
                        if (!Reader.IsDBNull(73)) PatientInfo.PVisit.PatientLocation.Building = Reader[73].ToString(); //¥
                        if (!Reader.IsDBNull(74)) PatientInfo.PVisit.PatientLocation.Floor = Reader[74].ToString(); //��
                        if (!Reader.IsDBNull(75)) PatientInfo.PVisit.PatientLocation.Room = Reader[75].ToString(); //��
                        if (!Reader.IsDBNull(76)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[76]); //�ܹ����TotCost
                        if (!Reader.IsDBNull(77)) PatientInfo.FT.OwnCost = NConvert.ToDecimal(Reader[77]); //�Էѽ�� OwnCost
                        if (!Reader.IsDBNull(78)) PatientInfo.FT.PayCost = NConvert.ToDecimal(Reader[78]); //�Ը���� PayCost
                        if (!Reader.IsDBNull(79)) PatientInfo.FT.PubCost = NConvert.ToDecimal(Reader[79]); //���ѽ�� PubCost
                        if (!Reader.IsDBNull(80)) PatientInfo.FT.LeftCost = NConvert.ToDecimal(Reader[80]); //ʣ���� LeftCost
                        if (!Reader.IsDBNull(81)) PatientInfo.FT.RebateCost = NConvert.ToDecimal(Reader[81]); //�Żݽ��
                        if (!Reader.IsDBNull(82)) PatientInfo.FT.PrepayCost = NConvert.ToDecimal(Reader[82]); // Ԥ�����
                        if (!Reader.IsDBNull(83)) PatientInfo.FT.BalancedCost = NConvert.ToDecimal(Reader[83]); //   ���ý��(�ѽ�)
                        if (!Reader.IsDBNull(84)) PatientInfo.FT.BalancedPrepayCost = NConvert.ToDecimal(Reader[84]); //   Ԥ�����(�ѽ�)
                        if (!Reader.IsDBNull(85))
                        {
                            try
                            {
                                PatientInfo.BalanceDate = NConvert.ToDateTime(Reader[85]); //����ʱ��
                            }
                            catch { }
                        }
                        if (!Reader.IsDBNull(86)) PatientInfo.PVisit.MoneyAlert = NConvert.ToDecimal(Reader[86]); //������
                        if (!Reader.IsDBNull(87)) PatientInfo.PVisit.ZG.ID = Reader[87].ToString(); //  ת�����
                        if (!Reader.IsDBNull(88)) PatientInfo.FT.TransferPrepayCost = NConvert.ToDecimal(Reader[88]); // ת��Ԥ����δ��) 
                        if (!Reader.IsDBNull(89)) PatientInfo.FT.TransferTotCost = NConvert.ToDecimal(Reader[89]); //  ת����ý�δ��) 
                        if (!Reader.IsDBNull(91)) PatientInfo.FT.OvertopCost = NConvert.ToDecimal(Reader[91]); //   ���ѳ�����
                        if (!Reader.IsDBNull(92)) PatientInfo.Memo = Reader[92].ToString(); //��ע
                        if (!Reader.IsDBNull(93)) PatientInfo.FT.DayLimitCost = NConvert.ToDecimal(Reader[93]); //   �������޶�
                        if (!Reader.IsDBNull(94)) PatientInfo.FT.BloodLateFeeCost = NConvert.ToDecimal(Reader[94]); //  Ѫ���ɽ�
                        if (!Reader.IsDBNull(95)) PatientInfo.InTimes = NConvert.ToInt32(Reader[95]); //סԺ����
                        if (!Reader.IsDBNull(96)) PatientInfo.FT.BedLimitCost = NConvert.ToDecimal(Reader[96].ToString()); //��λ����
                        if (!Reader.IsDBNull(97)) PatientInfo.FT.AirLimitCost = NConvert.ToDecimal(Reader[97].ToString()); //�յ�����
                        if (!Reader.IsDBNull(99)) PatientInfo.DoctorReceiver.ID = Reader[99].ToString();
                        if (!Reader.IsDBNull(98)) PatientInfo.ClinicDiagnose = Reader[98].ToString();
                        if (!Reader.IsDBNull(100)) PatientInfo.ProCreateNO = Reader[100].ToString(); //�������յ��Ժ�
                        PatientInfo.IsHasBaby = NConvert.ToBoolean(Reader[101].ToString()); //�Ƿ���Ӥ��
                        PatientInfo.Disease.Tend.Name = Reader[102].ToString(); //������
                        //������ȡ���
                        PatientInfo.FT.FixFeeInterval = NConvert.ToInt32(Reader[103].ToString());
                        //�ϴ���ȡʱ��
                        PatientInfo.FT.PreFixFeeDateTime = NConvert.ToDateTime(Reader[104].ToString());
                        //���ѳ��괦�� 0���겻��1��������
                        PatientInfo.FT.BedOverDeal = Reader[105].ToString();
                        //���޶��ۼ�
                        PatientInfo.FT.DayLimitTotCost = NConvert.ToDecimal(Reader[106].ToString());
                        //����״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4�������
                        PatientInfo.CaseState = Reader[107].ToString();

                        PatientInfo.ExtendFlag = Reader[108].ToString(); //�Ƿ��������޶�� 0 ��ͬ�� 1 ͬ�� ��ɽһԺ����
                        PatientInfo.ExtendFlag1 = Reader[109].ToString(); //��չ���1
                        PatientInfo.ExtendFlag2 = Reader[110].ToString(); //��չ���2
                        PatientInfo.FT.BoardCost = NConvert.ToDecimal(Reader[111]); //��ʳ�����ܶ�
                        PatientInfo.FT.BoardPrepayCost = NConvert.ToDecimal(Reader[112]); //��ʳԤ�����
                        PatientInfo.PVisit.BoardState = Reader[113].ToString(); //��ʳ����״̬��0��Ժ 1��Ժ
                        PatientInfo.FT.FTRate.OwnRate = NConvert.ToDecimal(Reader[114].ToString()); //�Էѱ���
                        PatientInfo.FT.FTRate.PayRate = NConvert.ToDecimal(Reader[115].ToString()); //�Ը�����
                        PatientInfo.FT.DrugFeeTotCost = NConvert.ToDecimal(Reader[116].ToString()); //���ѻ��߹���ҩƷ�ۼ�(���޶�)
                        PatientInfo.MainDiagnose = Reader[117].ToString(); //����סԺ�����

                        PatientInfo.Age = GetAge(PatientInfo.Birthday, sysDate); //���ݳ�������ȡ��������
                        PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[118].ToString());
                        PatientInfo.NormalName = Reader[119].ToString();
                        if (!Reader.IsDBNull(120)) PatientInfo.BalanceNO = NConvert.ToInt32(Reader[120].ToString());//�������
                        //{F0C48258-8EFB-4356-B730-E852EE4888A0}
                        PatientInfo.ExtendFlag1 = Reader[121].ToString();//����


                        if (PatientInfo.PID.PatientNO.StartsWith("B"))
                        {
                            //�Ƿ�Ӥ��
                            PatientInfo.IsBaby = true;
                        }
                        else
                        {
                            PatientInfo.IsBaby = false;
                        }

                        //{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
                        if (!Reader.IsDBNull(122))
                        {
                            PatientInfo.IsStopAcount = FS.FrameWork.Function.NConvert.ToBoolean(Reader[122].ToString());
                        }
                        PatientInfo.PVisit.AlertType.ID = this.Reader[123].ToString();
                        PatientInfo.PVisit.BeginDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[124]);
                        PatientInfo.PVisit.EndDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[125]);
                        PatientInfo.Disease.Memo = Reader[126].ToString(); //��ʳ

                        //���������ñ��
                        if (Reader.FieldCount > 127)
                        {
                            PatientInfo.PVisit.AlertFlag = FS.FrameWork.Function.NConvert.ToBoolean(Reader[127]);
                        }
                        if (Reader.FieldCount > 128)  //ҽ����������
                        {
                            PatientInfo.HealthTreamType = Reader[128].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                        return null;
                    }
                    //��ñ����Ϣ

                    #region "��ñ����Ϣ"

                    //deleted by cuipeng 2005-5 ��֪���˹���Ϊɶ��,����������
                    //this.myGetTempLocation(PatientInfo);

                    #endregion

                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }
            } //�׳�����
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return al;
        }

        #endregion

        #region ��ѯ������Ϣ��select��䣨��where������--˽��
        /// <summary>
        /// RADT.RADT.InPatient.QueryBaseInfo (fin_ipr_inmaininfo)
        /// </summary>
        /// <returns></returns>
        private string PatientQueryBasicSelect()
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.InPatient.QueryBaseInfo", ref sql) == -1)
            {
                #region SQL
                /*
				select inpatient_no,patient_no,name,
				sex_code,   --�Ա� M,F,O,U 
				birthday,   --���� 
				in_date,   --��Ժ���� 
				dept_code,   --���Ҵ��� 
				dept_name,   --�������� 
				bed_no,   --���� 
				 (select t.bed_state from com_bedinfo t where t.bed_no=d.bed_no) ,
				nurse_cell_code,   --����Ԫ���� 
				nurse_cell_name,   --����Ԫ���� 
				house_doc_code,   --ҽʦ����(סԺ) 
				house_doc_name,   --ҽʦ����(סԺ) 
				charge_doc_code,   --ҽʦ����(����) 50 
				charge_doc_name,   --ҽʦ����(����) 
				chief_doc_code,   --ҽʦ����(����) 
				chief_doc_name,   --ҽʦ����(����) 
				'', --ʵϰ 
				'', --ʵϰ55 
				duty_nurse_code,   --��ʿ����(����) 
				duty_nurse_name,   --��ʿ����(����) 
				tend,--����
				dietetic_mark, --��ʳ
				(select t.diag_name from MET_COM_DIAGNOSE t where t.inpatient_no = inpatient_no and t.main_flag='1' and rownum=1)
				from fin_ipr_inmaininfo  d
				*/
                #endregion
                return null;
            }
            return sql;
        }

        #endregion

        #region ���ݻ�����������Ժ״̬ģ�����һ����б�

        /// <summary>
        /// ���ݻ�����������Ժ״̬ģ�����һ����б�
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ArrayList QueryInpatientBySlurName(string name, string state)
        {
            string strSql = string.Empty;
            string strSqlWhere = string.Empty;

            strSql = PatientQuerySelect();

            if (Sql.GetCommonSql("RADT.Inpatient.QueryInpatientBySlurName.Where.1", ref strSqlWhere) == -1)
            {
                return null;
            }

            try
            {
                strSqlWhere = string.Format(strSqlWhere, name, state);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                WriteErr();
                return null;
            }
            strSql = strSql + strSqlWhere;

            return myPatientQuery(strSql);
        }

        #endregion

        #region ������ָ����׼���Ƿ�ѻ�����Ϣ�б�

        /// <summary>
        /// ���Ƿ�ѻ�����Ϣ(��ָ����׼)
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="alert"></param>
        /// <returns></returns>
        public ArrayList GetAlertPerson(string nurseCellCode, decimal alert)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            strSql1 = PatientQuerySelect();
            if (strSql1 == null)
            {
                return null;
            }

            //ȡ��ѯ�����where���
            if (Sql.GetCommonSql("RADT.Inpatient.GetAlertPerson.1", ref strSql2) == -1)
            {
                return null;
            }

            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, nurseCellCode, alert.ToString());
            }
            catch
            {
                Err = "RADT.Inpatient.GetAlertPerson.1��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.GetAlertPerson.1��ֵ��ƥ�䣡";
                WriteErr();
                return null;
            }

            return myPatientQuery(strSql1);
        }
        /// <summary>
        /// ���Ƿ�ѻ�����Ϣ(��ָ����׼)
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="alert"></param>
        /// <returns></returns>
        public ArrayList GetAlertPerson(string nurseCellCode, decimal alert, string patientNo)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            strSql1 = PatientQuerySelect();
            if (strSql1 == null)
            {
                return null;
            }

            //ȡ��ѯ�����where���
            if (Sql.GetCommonSql("RADT.Inpatient.GetAlertPerson.1", ref strSql2) == -1)
            {
                return null;
            }

            try
            {
                if (string.IsNullOrEmpty(patientNo.Trim()))
                {
                    patientNo = "ALL";
                }
                strSql1 = strSql1 + " " + string.Format(strSql2, nurseCellCode, alert.ToString(), patientNo);
            }
            catch
            {
                Err = "RADT.Inpatient.GetAlertPerson.1��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.GetAlertPerson.1��ֵ��ƥ�䣡";
                WriteErr();
                return null;
            }

            return myPatientQuery(strSql1);
        }

        #endregion


        #region ������ָ����׼���Ƿ�ѻ�����Ϣ�б�1

        /// <summary>
        /// ���Ƿ�ѻ�����Ϣ(��ָ����׼)1 add by zhy
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="alert"></param>
        /// <returns></returns>
        public ArrayList GetAlertPersonNew1(string nurseCellCode, decimal alert)
        {
            string strSql1 = string.Empty;
            string strSql2 = string.Empty;

            strSql1 = PatientQuerySelectNew1();
            if (strSql1 == null)
            {
                return null;
            }

            ////ȡ��ѯ�����where���
            //if (Sql.GetCommonSql("RADT.Inpatient.GetAlertPerson.1", ref strSql2) == -1)
            //{
            //    return null;
            //}

            try
            {
                strSql1 = string.Format(strSql1, nurseCellCode, alert.ToString());
            }
            catch
            {
                Err = "RADT.Inpatient.PatientQuery.Select.1.New1��ֵ��ƥ�䣡";

                WriteErr();
                return null;
            }

            return myPatientQueryNew1(strSql1);
        }

        #endregion


        #region ������ָ���������Ƿ�ѻ�����Ϣ�б�

        /// <summary>
        /// ���Ƿ�ѻ�����Ϣ(��ָ������)
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="alert"></param>
        /// <returns></returns>
        public ArrayList GetAlertPercent(string nurseCellCode, decimal alert)
        {
            string strSql1 = string.Empty, strSql2 = string.Empty;
            strSql1 = PatientQuerySelect();
            if (strSql1 == null) return null;

            //ȡ��ѯ�����where���
            if (Sql.GetCommonSql("RADT.Inpatient.GetAlertPerson.2", ref strSql2) == -1)
            {
                return null;
            }

            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, nurseCellCode, alert.ToString());
            }
            catch
            {
                Err = "RADT.Inpatient.GetAlertPerson.2��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.GetAlertPerson.2��ֵ��ƥ�䣡";
                WriteErr();
                return null;
            }

            return myPatientQuery(strSql1);
        }

        #endregion

        #region	���������Ƿ�ѻ�����Ϣ�б�

        /// <summary>
        /// ���Ƿ�ѻ�����Ϣ(���������)
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <returns></returns>
        public ArrayList GetAlertPerson(string nurseCellCode)
        {
            string strSql1 = string.Empty, strSql2 = string.Empty;
            strSql1 = PatientQuerySelect();
            if (strSql1 == null) return null;

            //ȡ��ѯ�����where���
            if (Sql.GetCommonSql("RADT.Inpatient.GetAlertPerson.3", ref strSql2) == -1)
            {
                return null;
            }

            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, nurseCellCode);
            }
            catch
            {
                Err = "RADT.Inpatient.GetAlertPerson.3��ֵ��ƥ�䣡";
                ErrCode = "RADT.Inpatient.GetAlertPerson.3��ֵ��ƥ�䣡";
                WriteErr();
                return null;
            }

            return myPatientQuery(strSql1);
        }

        #endregion

        # region  ������ⲡ���б�,��ҽ��ƹ���

        /// <summary>
        /// ������ⲡ���б�
        /// </summary>
        /// <returns></returns>
        private ArrayList myGetSpecialPatient(string strSql)
        {
            if (strSql == string.Empty || strSql == null)
                return null;
            if (ExecQuery(strSql) == -1)
                return null;
            ArrayList alPatient = new ArrayList();
            try
            {
                while (Reader.Read())
                {
                    PatientInfo pInfo = new PatientInfo();
                    try
                    {
                        pInfo.PID.PatientNO = Reader[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                        return null;
                    }
                    alPatient.Add(pInfo);
                }
            }
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return null;
            }
            Reader.Close();
            return alPatient;
        }

        # endregion

        #region Ԥ���ӿ�

        #region ����һ�����ߵ���Ϣ����

        /// <summary>
        /// ע���û� ����һ�����ߵ���Ϣ����
        /// </summary>
        /// <param name="PatientInfo">ע���Ļ�����Ϣ</param>
        /// <returns>0�ɹ� -1ʧ��</returns>
        public int DischargePatient(PatientInfo PatientInfo)
        {
            return 0;
        }

        #endregion

        #region ���»�����Ϣ�Ƿ��Ͳ�����־

        /// <summary>
        /// ���²����Ƿ��Ͳ���
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <param name="IsSended"></param>
        /// <returns></returns>
        public int UpdateCaseSend(string InpatientNo, bool IsSended)
        {
            return 1;
        }

        #endregion

        #region ��ѯ���߲����Ƿ��Ͳ�����

        /// <summary>
        /// ��ѯ�Ƿ����Ͳ���
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns></returns>
        public bool QueryIsCaseSended(string InpatientNo)
        {
            return true;
        }

        #endregion

        #region ��ѯû���Ͳ����Ļ�����Ϣ�б�

        /// <summary>
        /// ��ѯû���Ͳ����Ļ���
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPatientNoSendCase()
        {
            return null;
        }

        #endregion

        #endregion

        #region ��ȡ ��������ѯ������Ϣ�б� SQL��� com_patientinfo

        /// <summary>
        /// ���߾��￨���б��ѯ com_patientinfo
        /// </summary>
        /// <returns>COM_PATIENTINFO Select SQL���</returns>
        private string QueryComPatientInfoSelect()
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Select.2", ref sql) == -1)
            #region SQL
            /* SELECT card_no,
					name,   --����
					spell_code,   --ƴ����
					wb_code,   --���
					to_char(birthday,'YYYY-MM-DD') as birthday,   --��������
					decode( sex_code,'U','δ֪','F','Ů','M','��') as sex_code,   --�Ա�
					idenno,   --���֤��
					blood_code,   --Ѫ��
					prof_code,   --ְҵ
					work_home,   --������λ
					work_tel,   --��λ�绰
					work_zip,   --��λ�ʱ�
					home,   --���ڻ��ͥ����
					home_tel,   --��ͥ�绰
					home_zip,   --���ڻ��ͥ��������
					district,   --����
					nation_code,   --����
					linkman_name,   --��ϵ������
					linkman_tel,   --��ϵ�˵绰
					linkman_add,   --��ϵ��סַ
					rela_code,   --��ϵ�˹�ϵ
					mari,   --����״��
					coun_code,   --����
					paykind_code,   --�������
					paykind_name,   --�����������
					pact_code,   --��ͬ����
					pact_name,   --��ͬ��λ����
					mcard_no,   --ҽ��֤��
					area_code,   --������
					framt,   --ҽ�Ʒ���
					ic_cardno,   --���Ժ�
					anaphy_flag,   --ҩ�����
					hepatitis_flag,   --��Ҫ����
					act_code,   --�ʻ�����
					act_amt,   --�ʻ��ܶ�
					lact_sum,   --�����ʻ����
					lbank_sum,   --�����������
					arrear_times,   --Ƿ�Ѵ���
					arrear_sum,   --Ƿ�ѽ��
					inhos_source,   --סԺ��Դ
					lihos_date,   --���סԺ����
					inhos_times,   --סԺ����
					louthos_date,   --�����Ժ����
					fir_see_date,   --��������
					lreg_date,   --����Һ�����
					disoby_cnt,   --ΥԼ����
					end_date,   --��������
					mark,   --��ע
					oper_code,   --����Ա								   
					oper_date,    --��������
					is_valid
					FROM com_patientinfo   --���˻�����Ϣ��
					*/
            #endregion
            {
                return null;
            }
            return sql;
        }
        [Obsolete("����Ϊ QueryComPatientInfoSelect", true)]
        private string PatientInfoQuerySelect()
        {
            return null;
        }
        #endregion

        #region ��ȡ ��������ѯ������Ϣ�б� SQL��� FIN_IPR_INMAININFO

        /// <summary>
        /// ��ѯ������Ϣ FIN_IPR_INMAININFO
        /// </summary>
        /// <returns>FIN_IPR_INMAININFO SQL ���</returns>
        private string PatientQuerySelect()
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Select.New", ref sql) == -1)
            {
                #region SQL
                /*
				SELECT	inpatient_no,	--0 סԺ��ˮ��
				name,		--1 ����
				patient_no,	--2 סԺ��
				card_no,	--3 ���￨��
				patient_no,	--4 ������
				mcard_no,	--5 ҽ��֤��
				MEDICAL_TYPE,	--6 ҽ�����
				sex_code,   	--7 �Ա� M,F,O,U
				idenno,   	--8 ���֤��
				spell_code,   	--9 ƴ��10
				birthday,   	--10����
				prof_code,   	--11ְҵ����
				'',		--12 ְҵ����
				work_name,   	--13������λ
				work_tel,   	--14������λ�绰15
				work_zip,   	--15��λ�ʱ�Memo
				home,   	--16���ڻ��ͥ��ַ
				home_tel,   	--17��ͥ�绰
				home_zip,   	--18���ڻ��ͥ�ʱ�		
				'',		--19����id20
				dist,   	--20����name
				birth_area,   	--21�����ش���		
				'',		--22����������
				nation_code,   	--23����id
				'',		--24��������
				'',		--25��ϵ��ID
				linkman_name,   --26��ϵ������			
				linkman_tel,   	--27��ϵ�˵绰		
				linkman_add,   	--28��ϵ�˵�ַ
				rela_code,   	--29��ϵ�˹�ϵID
				'',		--30��ϵ�˹�ϵ����
				mari,   	--31����״��id
				'',		--32����״������
				coun_code,   	--33����id
				'',		--34
				height,   	--35���30
				weight,   	--36����
				blood_dress,   	--37Ѫѹ
				blood_code,  	--38Ѫ�ͱ���
				hepatitis_flag, --39�ش󼲲���־ 1:��  0:��
				anaphy_flag,   	--40������־ 1��  0:�� 35
				in_date,   	--41��Ժ����
				dept_code,   	--42���Ҵ���
				dept_name,   	--43��������
				paykind_code,   --44������� 01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸� 40
				'',		--45
				pact_code,   	--46��ͬ����
				pact_name,   	--47��ͬ��λ����
				bed_no,   	--48����
				nurse_cell_code,--49����Ԫ����
				nurse_cell_name,--50����Ԫ����
				house_doc_code, --51ҽʦ����(סԺ)
				house_doc_name, --52ҽʦ����(סԺ)
				charge_doc_code,--53ҽʦ����(����) 50
				charge_doc_name,--54ҽʦ����(����)
				chief_doc_code, --55ҽʦ����(����)
				chief_doc_name, --56ҽʦ����(����)
				'', 		--57ʵϰ
				'', 		--58ʵϰ55
				duty_nurse_code,--59��ʿ����(����)
				duty_nurse_name,--60��ʿ����(����)
				in_circs,   	--61��Ժ���ID
				'',		--62��Ժ���Name
				in_avenue,   	--63��Ժ;��ID
				'',		--64��Ժ;��Name
				in_source,   	--65��Ժ��ԴID 1:���2:���3:ת�ƣ�4:תԺ 60
				'',		--66��Ժ��ԴName
				in_state,   	--67��Ժ״̬ R-סԺ�Ǽ�  I �������� B-��Ժ�Ǽ� O-��Ժ���� P-ԤԼ��Ժ,N-�޷���Ժ C ȡ�� 
				prepay_outdate, --68��Ժ����(ԤԼ) 
				out_date,   	--69��Ժ����
				in_icu, 	--70�Ƿ���ICU
				'', 		--71 icu code
				'', 		--72 icu name
				'', 		--73¥
				'', 		--74��
				'',		--75����
				tot_cost,   	--76���ý��(δ��)
				own_cost,   	--77�Էѽ��(δ��)75
				pay_cost,   	--78�Ը����(δ��)
				pub_cost,   	--79���ѽ��(δ��)
				free_cost,   	--80���(δ��)80
				eco_cost,   	--81�Żݽ��(δ��)
				prepay_cost,   	--82Ԥ�����(δ��)
				balance_cost,   --83���ý��(�ѽ�)
				balance_prepay, --84Ԥ�����(�ѽ�)
				balance_date,   --85��������(�ϴ�)
				money_alert,   	--86������
				zg,            	--87ת�����
				CHANGE_PREPAYCOST,--88ת��Ԥ����
				CHANGE_TOTCOST,   --89ת����� 
				(SELECT t.BED_STATE FROM COM_BEDINFO t WHERE t.BED_NO=d.BED_NO AND PARENT_CODE='[��������]' AND CURRENT_CODE='[��������]'), --����״̬
				LIMIT_OVERTOP,  --91�������޶�겿��
				Memo,  		--92��ע
				DAY_LIMIT,	--93���޶�
				BLOOD_LATEFEE,	--94Ѫ���ɽ�
				in_times,	--95��Ժ����
				BED_LIMIT,	--96��λ����
				AIR_LIMIT,	--97�յ�����
				CLINIC_DIAGNOSE,--98�������
				EMPL_CODE,	--99��סҽ������
				PROCREATE_PCNO,	--100�������յ��Ժ�
				BABY_FLAG,	--101�Ƿ���Ӥ��
				TEND,		--102������ 
				fee_interval,	--103�̶����ü������
				prefixfee_date,	--104�ϴι̶�����ʱ��
				BEDOVERDEAL,	--105���ѳ��괦�� 0���겻��1��������
				LIMIT_TOT,	--106���ѻ������޶��ۼ�
				CASE_FLAG,	--107����״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4�������
				ext_flag,	--108�Ƿ��������޶�� 0 ��ͬ�� 1 ͬ�� ��ɽһԺ����
				ext_flag1,	--109��չ���1
				ext_flag2,	--110��չ���2
				BOARD_COST,	--111��ʳ�����ܶ�
				BOARD_PREPAY,	--112��ʳԤ�����
				BOARD_STATE,	--113��ʳ����״̬��0��Ժ 1��Ժ
				OWN_RATE,	--114�Էѱ���
				PAY_RATE,	--115�Ը�����
				BURSARY_TOTMEDFEE,	--116���ѻ��߹���ҩƷ�ۼ�(���޶�)
				DIAG_NAME   ---117סԺ��������� 
			FROM 	FIN_IPR_INMAININFO  d				*/
                #endregion

                return null;
            }
            return sql;
        }

        #endregion

        #region
        /// <summary>
        /// ��ѯ������Ϣ FIN_IPR_INMAININFO add by zhy ���ڴ����ն˷���
        /// </summary>
        /// <returns>FIN_IPR_INMAININFO SQL ���</returns>
        private string PatientTerminalFeeQuerySelect()
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.TerminalFeeSelect.1", ref sql) == -1)
            {
                sql = @"select i.INPATIENT_NO,i.PATIENT_NO,
                           i.NAME,
                           fun_get_dept_name(i.DEPT_CODE),
                           sum(e.unit_price * e.qty_tot)
                      from met_ipm_execundrug e, v_fin_ipr_inmaininfo i
                     where (e.exec_flag = '0' or
                           e.qty_tot >
                           nvl((select sum(min(m.confirm_number - nvl(m.cancel_qty, 0)))
                                  from met_tec_inpatientconfirm m
                                 where e.exec_sqn = m.exec_sqn
                                   and e.mo_order = m.mo_order
                                   and e.inpatient_no = m.inpatient_no
                                 group by m.exec_sqn,
                                          m.mo_order,
                                          nvl(m.recipe_no, m.current_sequence)),0))
                       and e.valid_flag = fun_get_valid                           
                       and e.charge_state = '1'
                       and e.need_confirm = '1'                         
                       and e.inpatient_no = i.inpatient_no
                       and i.INPATIENT_NO = '{0}'                     
                       and i.IN_STATE = 'I'
                      group by i.INPATIENT_NO,i.PATIENT_NO, i.NAME, i.DEPT_CODE";
            }
            return sql;
        }
        #endregion


        //add by zhy

        #region ��ȡ ��������ѯ������Ϣ�б� SQL��� FIN_IPR_INMAININFO

        /// <summary>
        /// ��ѯ������Ϣ FIN_IPR_INMAININFO   add by zhy
        /// </summary>
        /// <returns>FIN_IPR_INMAININFO SQL ���</returns>
        private string PatientQuerySelectNew1()
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Select.1.New1", ref sql) == -1)
            {
                #region  sql
                sql = @"select i.INPATIENT_NO,i.PATIENT_NO,
                           i.NAME,
                           fun_get_dept_name(i.DEPT_CODE),
                           sum(e.unit_price * e.qty_tot)
                      from met_ipm_execundrug e, v_fin_ipr_inmaininfo i
                     where (e.exec_flag = '0' or
                           e.qty_tot >
                           nvl((select sum(min(m.confirm_number - nvl(m.cancel_qty, 0)))
                                  from met_tec_inpatientconfirm m
                                 where e.exec_sqn = m.exec_sqn
                                   and e.mo_order = m.mo_order
                                   and e.inpatient_no = m.inpatient_no
                                 group by m.exec_sqn,
                                          m.mo_order,
                                          nvl(m.recipe_no, m.current_sequence)),0))
                       and e.valid_flag = fun_get_valid
                          --  and (e.exec_dpcd='6041' or 'all' = '6041')
                       and e.charge_state = '1'
                       and e.need_confirm = '1'
                          --  and e.nurse_cell_code='3263'
                       and e.inpatient_no = i.inpatient_no
                       and i.NURSE_CELL_CODE in ({0}) --����1
                       and i.FREE_COST < ({1}) --����2
                       and i.IN_STATE = 'I'
                      group by i.INPATIENT_NO,i.PATIENT_NO, i.NAME, i.DEPT_CODE";


                #endregion

                // return null;
            }
            return sql;
        }

        #endregion



        #region ��ȡ ��������ѯ������Ϣ�б� SQL��� FIN_IPR_INMAININFO

        /// <summary>
        /// ��ѯ������Ϣ FIN_IPR_INMAININFO
        /// </summary>
        /// <returns>FIN_IPR_INMAININFO SQL ���</returns>
        private string PatientQuerySelectNew()
        {
            string sql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Select.1.New", ref sql) == -1)
            {
                #region SQL
                /*
				SELECT	inpatient_no,	--0 סԺ��ˮ��
				name,		--1 ����
				patient_no,	--2 סԺ��
				card_no,	--3 ���￨��
				patient_no,	--4 ������
				mcard_no,	--5 ҽ��֤��
				MEDICAL_TYPE,	--6 ҽ�����
				sex_code,   	--7 �Ա� M,F,O,U
				idenno,   	--8 ���֤��
				spell_code,   	--9 ƴ��10
				birthday,   	--10����
				prof_code,   	--11ְҵ����
				'',		--12 ְҵ����
				work_name,   	--13������λ
				work_tel,   	--14������λ�绰15
				work_zip,   	--15��λ�ʱ�Memo
				home,   	--16���ڻ��ͥ��ַ
				home_tel,   	--17��ͥ�绰
				home_zip,   	--18���ڻ��ͥ�ʱ�		
				'',		--19����id20
				dist,   	--20����name
				birth_area,   	--21�����ش���		
				'',		--22����������
				nation_code,   	--23����id
				'',		--24��������
				'',		--25��ϵ��ID
				linkman_name,   --26��ϵ������			
				linkman_tel,   	--27��ϵ�˵绰		
				linkman_add,   	--28��ϵ�˵�ַ
				rela_code,   	--29��ϵ�˹�ϵID
				'',		--30��ϵ�˹�ϵ����
				mari,   	--31����״��id
				'',		--32����״������
				coun_code,   	--33����id
				'',		--34
				height,   	--35���30
				weight,   	--36����
				blood_dress,   	--37Ѫѹ
				blood_code,  	--38Ѫ�ͱ���
				hepatitis_flag, --39�ش󼲲���־ 1:��  0:��
				anaphy_flag,   	--40������־ 1��  0:�� 35
				in_date,   	--41��Ժ����
				dept_code,   	--42���Ҵ���
				dept_name,   	--43��������
				paykind_code,   --44������� 01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸� 40
				'',		--45
				pact_code,   	--46��ͬ����
				pact_name,   	--47��ͬ��λ����
				bed_no,   	--48����
				nurse_cell_code,--49����Ԫ����
				nurse_cell_name,--50����Ԫ����
				house_doc_code, --51ҽʦ����(סԺ)
				house_doc_name, --52ҽʦ����(סԺ)
				charge_doc_code,--53ҽʦ����(����) 50
				charge_doc_name,--54ҽʦ����(����)
				chief_doc_code, --55ҽʦ����(����)
				chief_doc_name, --56ҽʦ����(����)
				'', 		--57ʵϰ
				'', 		--58ʵϰ55
				duty_nurse_code,--59��ʿ����(����)
				duty_nurse_name,--60��ʿ����(����)
				in_circs,   	--61��Ժ���ID
				'',		--62��Ժ���Name
				in_avenue,   	--63��Ժ;��ID
				'',		--64��Ժ;��Name
				in_source,   	--65��Ժ��ԴID 1:���2:���3:ת�ƣ�4:תԺ 60
				'',		--66��Ժ��ԴName
				in_state,   	--67��Ժ״̬ R-סԺ�Ǽ�  I �������� B-��Ժ�Ǽ� O-��Ժ���� P-ԤԼ��Ժ,N-�޷���Ժ C ȡ�� 
				prepay_outdate, --68��Ժ����(ԤԼ) 
				out_date,   	--69��Ժ����
				in_icu, 	--70�Ƿ���ICU
				'', 		--71 icu code
				'', 		--72 icu name
				'', 		--73¥
				'', 		--74��
				'',		--75����
				tot_cost,   	--76���ý��(δ��)
				own_cost,   	--77�Էѽ��(δ��)75
				pay_cost,   	--78�Ը����(δ��)
				pub_cost,   	--79���ѽ��(δ��)
				free_cost,   	--80���(δ��)80
				eco_cost,   	--81�Żݽ��(δ��)
				prepay_cost,   	--82Ԥ�����(δ��)
				balance_cost,   --83���ý��(�ѽ�)
				balance_prepay, --84Ԥ�����(�ѽ�)
				balance_date,   --85��������(�ϴ�)
				money_alert,   	--86������
				zg,            	--87ת�����
				CHANGE_PREPAYCOST,--88ת��Ԥ����
				CHANGE_TOTCOST,   --89ת����� 
				(SELECT t.BED_STATE FROM COM_BEDINFO t WHERE t.BED_NO=d.BED_NO AND PARENT_CODE='[��������]' AND CURRENT_CODE='[��������]'), --����״̬
				LIMIT_OVERTOP,  --91�������޶�겿��
				Memo,  		--92��ע
				DAY_LIMIT,	--93���޶�
				BLOOD_LATEFEE,	--94Ѫ���ɽ�
				in_times,	--95��Ժ����
				BED_LIMIT,	--96��λ����
				AIR_LIMIT,	--97�յ�����
				CLINIC_DIAGNOSE,--98�������
				EMPL_CODE,	--99��סҽ������
				PROCREATE_PCNO,	--100�������յ��Ժ�
				BABY_FLAG,	--101�Ƿ���Ӥ��
				TEND,		--102������ 
				fee_interval,	--103�̶����ü������
				prefixfee_date,	--104�ϴι̶�����ʱ��
				BEDOVERDEAL,	--105���ѳ��괦�� 0���겻��1��������
				LIMIT_TOT,	--106���ѻ������޶��ۼ�
				CASE_FLAG,	--107����״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4�������
				ext_flag,	--108�Ƿ��������޶�� 0 ��ͬ�� 1 ͬ�� ��ɽһԺ����
				ext_flag1,	--109��չ���1
				ext_flag2,	--110��չ���2
				BOARD_COST,	--111��ʳ�����ܶ�
				BOARD_PREPAY,	--112��ʳԤ�����
				BOARD_STATE,	--113��ʳ����״̬��0��Ժ 1��Ժ
				OWN_RATE,	--114�Էѱ���
				PAY_RATE,	--115�Ը�����
				BURSARY_TOTMEDFEE,	--116���ѻ��߹���ҩƷ�ۼ�(���޶�)
				DIAG_NAME   ---117סԺ��������� 
			FROM 	FIN_IPR_INMAININFO  d				*/
                #endregion

                return null;
            }
            return sql;
        }

        #endregion
        #endregion

        #region ����

        #region ����Ժʱ���״̬��ѯ���߻�����Ϣ�б�

        /// <summary>
        /// ���߲�ѯ-����Ժʱ���״̬��
        /// </summary>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        [Obsolete("QueryPatientBasic����", true)]
        public ArrayList PatientQueryBasic(DateTime beginDateTime, DateTime endDateTime, InStateEnumService State)
        {
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            sql1 = PatientQueryBasicSelect();

            if (sql1 == null)
            {
                return null;
            }

            string[] arg = new string[3];
            arg[0] = beginDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            arg[1] = endDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            arg[2] = State.ID.ToString();
            if (Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.8", ref sql2) == -1)
            #region SQL
            /*where PARENT_CODE='[��������]' and CURRENT_CODE='[��������]' and (TRUNC(in_date) >=to_date('{0}','yyyy-mm-dd')) and (TRUNC(in_date) <=to_date('{1}','yyyy-mm-dd')) and In_state='{2}'*/
            #endregion
            {
                ;
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, arg);

            return myPatientBasicQuery(sql1);
        }

        #endregion

        #region ����Ժ��ʼʱ��ͽ���ʱ���ѯ���߻�����Ϣ�б�

        /// <summary>
        /// ���߲�ѯ--����Ժʱ���ѯ wangrc
        /// </summary>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public ArrayList QueryPatientBasic(DateTime beginDateTime, DateTime endDateTime)
        {
            #region �ӿ�˵��

            /////RADT.Inpatient.2
            //����:סԺʱ�俪ʼ������
            //������������Ϣ

            #endregion

            string sql1 = string.Empty, sql2 = string.Empty;
            sql1 = PatientQueryBasicSelect();
            if (sql1 == null)
            {
                return null;
            }

            string[] arg = new string[2];
            arg[0] = beginDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            arg[1] = endDateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);

            if (Sql.GetCommonSql("RADT.Inpatient.PatientQueryByDateIn", ref sql2) == -1)
            {
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, arg);
            return myPatientBasicQuery(sql1);
        }

        #endregion

        [Obsolete("����Ϊ QueryMedicarePatientInfo", true)]
        public ArrayList PatientQueryMedicare(DateTime beginDateTime, DateTime endDateTime, string inState, string deptCode, string pactCode)
        {
            return null;
        }
        #endregion

        #region ��ѯ����
        /// <summary>
        ///  ��ѯ��Ժ�����ڵĻ�����Ϣ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="outDays"></param>
        /// <returns></returns>
        public ArrayList PatientQuery(string deptCode, int outDays)
        {
            string sql = "";
            string strWhere = "";
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Select.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.35", ref strWhere) == -1) return null;

            sql = sql + " " + strWhere;
            try
            {
                sql = string.Format(sql, deptCode, outDays.ToString());
            }
            catch { return null; }
            return this.myPatientQuery(sql);
        }

        /// <summary>
        /// ��ѯ����סԺ�ţ�סԺ�������ظ����{4949C040-E8C9-49d9-9BC2-548F7892206B}
        /// </summary>
        /// <param name="inpatientno"></param>
        /// <param name="inTimes"></param>
        /// <returns></returns>
        public int VerifyInpatientInTimes(string inpatientno, string inTimes)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientExtendInfo.VerifyInpatientInTimes", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                //���� ״̬ ����Ա
                strSql = string.Format(strSql, inpatientno, inTimes);
            }
            catch { return 0; }

            int count = 0;

            strSql = string.Format(strSql, inpatientno, inTimes);
            ExecQuery(strSql);
            while (Reader.Read())
            {
                count++;
            }
            Reader.Close();
            return count;
        }
        #endregion

        /// <summary>
        ///  ���ݿ��ұ�����Ժʱ���ѯ����
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="outDays"></param>
        /// <returns></returns>
        public ArrayList PatientQuery(string deptCode, DateTime fromdate, DateTime toDate)
        {
            string sql = "";
            string strWhere = "";
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Select.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.97", ref strWhere) == -1) return null;

            sql = sql + " " + strWhere;
            try
            {
                sql = string.Format(sql, deptCode, fromdate.ToString(), toDate.ToString());
            }
            catch { return null; }
            return this.myPatientQuery(sql);
        }

        /// <summary>
        ///  ���ݿ��ұ����Ժʱ���ѯ����// {F417D766-19C0-4d3e-AB72-D774058B497E}
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="outDays"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByDeptAndOutDate(string deptCode, DateTime fromdate, DateTime toDate)
        {
            string sql = "";
            string strWhere = "";
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Select.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.101", ref strWhere) == -1) return null;

            sql = sql + " " + strWhere;
            try
            {
                sql = string.Format(sql, deptCode, fromdate.ToString(), toDate.ToString());
            }
            catch { return null; }
            return this.myPatientQuery(sql);
        }
        /// <summary>
        ///  ��������ģ����ѯ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="outDays"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByName(string name)
        {
            string sql = "";
            string strWhere = "";
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Select.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.96", ref strWhere) == -1) return null;

            sql = sql + " " + strWhere;
            try
            {
                sql = string.Format(sql, name);
            }
            catch
            {
                return null;
            }
            return this.myPatientQuery(sql);
        }

        #region ���Ļ��߲���{F0C48258-8EFB-4356-B730-E852EE4888A0}
        /// <summary>
        /// ���»��߲���״̬������Ϊ���أ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateBZ_Info(string id)
        {
            string strsql = "";
            try
            {
                string sql = "update  fin_ipr_inmaininfo t  set t.CRITICAL_FLAG='1'  where t.inpatient_no='{0}'";
                strsql = string.Format(sql, id);
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return -1;
            }
            return ExecNoQuery(strsql);
        }
        /// <summary>
        /// ���»��߲���״̬������Ϊ��ͨ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdatePT_Info(string id)
        {
            string strsql = "";
            try
            {
                string sql = "update  fin_ipr_inmaininfo t  set t.CRITICAL_FLAG='0'  where t.inpatient_no='{0}'";
                strsql = string.Format(sql, id);
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return -1;
            }
            return ExecNoQuery(strsql);
        }

        /// <summary>
        /// ���»��߲���״̬������Ϊ���أ�{C9F9006D-AE0A-4e73-9ECE-68265A7A583E}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateCondition_Info(string flag, string id)
        {
            string strsql = "";
            try
            {
                string sql = "update  fin_ipr_inmaininfo t  set t.CRITICAL_FLAG='{1}'  where t.inpatient_no='{0}'";
                #region {66BD2D82-A609-4a8a-A947-7CFC5A246028}
                //����ط�����д���˰�
                //strsql = string.Format(sql, flag, id); 
                strsql = string.Format(sql, id, flag);
                #endregion
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return -1;
            }
            return ExecNoQuery(strsql);
        }

        public string SelectBQ_Info(string id)
        {
            PatientInfo PatientInfo = new PatientInfo();
            string strsql = "";
            string sql = "select a.critical_flag from fin_ipr_inmaininfo a where a.inpatient_no ='{0}'";
            strsql = string.Format(sql, id);
            if (ExecQuery(strsql) < 0)
            {
                return null;
            }
            if (Reader.Read())
            {
                try
                {
                    PatientInfo.ExtendFlag1 = Reader[0].ToString();//��ѯ���߲�����Ϣ
                }
                catch (Exception e)
                {
                    Err = e.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return "0";
                }
            }
            Reader.Close();
            return PatientInfo.ExtendFlag1;
        }
        #endregion

        #region �㶫ʡ����ϵͳ�ӿ�
        /// <summary>
        /// ���ݹ��������õ�����סԺ��ϸ��Ϣ--Edit By zhangjunyi
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="status"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryInfoByConditons(string pactCode, string deptCode, string status, DateTime beginDate, DateTime endDate, DateTime OutBegin, DateTime OutEnd)
        {
            string strSql1 = "", strSql2 = "";
            ArrayList al = new ArrayList();
            #region �ӿ�˵��
            /////RADT.Inpatient.PatientOneQuery.where.12
            //����:��ͬ��λ�����ң�״̬����ֹʱ��
            //������������Ϣ
            #endregion

            strSql1 = PatientQuerySelect();
            if (strSql1 == null) return null;

            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery.Where.12.zjy", ref strSql2) == -1) return null;
            try
            {
                strSql1 = strSql1 + " " + string.Format(strSql2, deptCode, pactCode, beginDate, endDate, status, OutBegin, OutEnd);
            }
            catch
            {
                this.Err = "RADT.Inpatient.PatientQuery.Where.12��ֵ��ƥ�䣡";
                this.ErrCode = "RADT.Inpatient.PatientQuery.Where.12��ֵ��ƥ�䣡";
                return null;
            }

            return this.myPatientQuery(strSql1);
        }

        /// <summary>
        /// �ϴ���־
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="uploadFlag"></param>
        /// <returns></returns>
        public int CaseUpLoadFlag(string inpatientNo, string uploadFlag)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Inpatient.Case.UpLoad.Flag", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, inpatientNo, uploadFlag);
            }
            catch { return -1; }
            return this.ExecNoQuery(sql);
        }
        #endregion

        #region ��ȡסԺ������չ��Ϣ

        public ArrayList QueryPatientExtendInfo(string InpatientNo)
        {
            ArrayList al = new ArrayList();
            string sql = "";
            string strWhere = "";
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientExtendInfo.Select.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientExtendInfo.where.1", ref strWhere) == -1) return null;
            sql = sql + " " + strWhere;
            try
            {
                sql = string.Format(sql, InpatientNo);
            }
            catch { return null; }
            al = this.MyPatientExtendQuery(sql);
            return al;
        }

        /// <summary>
        /// ��ȡ������չ��Ϣ--���������ֶν���PatientInfo���Ժ�����Ҫʱ������дһ��ʵ��
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList MyPatientExtendQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;

            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }

            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new PatientInfo();
                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // סԺ��ˮ��
                        if (!Reader.IsDBNull(1)) PatientInfo.SIMainInfo.ICCardCode = Reader[1].ToString();//�Ÿ�֤��
                        if (!Reader.IsDBNull(2)) PatientInfo.SIMainInfo.AnotherCity.Name = Reader[2].ToString();//����
                        if (!Reader.IsDBNull(3)) PatientInfo.SIMainInfo.ApplyType.Name = Reader[3].ToString();//�������
                        if (!Reader.IsDBNull(4)) PatientInfo.SIMainInfo.ApplyType.ID = Reader[4].ToString();//����������
                        if (!Reader.IsDBNull(5)) PatientInfo.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());//���
                        if (!Reader.IsDBNull(6)) PatientInfo.SIMainInfo.AddTotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[6].ToString());//�����
                        if (!Reader.IsDBNull(7)) PatientInfo.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());//��λ��
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                        if (!Reader.IsClosed)
                        {
                            Reader.Close();
                        }
                        return null;
                    }
                    //��ñ����Ϣ

                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }
            } //�׳�����
            catch (Exception ex)
            {
                Err = "��û��߻�����Ϣ����" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return al;
        }

        #endregion

        #region ����סԺ��չ����Ϣ

        public int UpdatePatientExtendInfo(PatientInfo patientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientExtendInfo.Update", ref strSql) == -1) return -1;

            #region --

            try
            {
                string[] s = new string[9];

                s[0] = patientInfo.ID;// סԺ��ˮ��
                s[1] = patientInfo.SIMainInfo.ICCardCode;//�Ÿ�֤��
                s[2] = patientInfo.SIMainInfo.AnotherCity.Name;//����
                s[3] = patientInfo.SIMainInfo.ApplyType.Name;//�������
                s[4] = patientInfo.SIMainInfo.ApplyType.ID;//����������
                s[5] = patientInfo.SIMainInfo.ItemPayCost.ToString();//���
                s[6] = patientInfo.SIMainInfo.AddTotCost.ToString();//�����
                s[7] = patientInfo.SIMainInfo.BaseCost.ToString();//��λ��
                s[8] = Operator.ID;//������
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                return -1;
            }

            #endregion
            return ExecNoQuery(strSql);
        }
        #endregion

        #region ����סԺ��չ��Ϣ
        public int InsertPatientExtendInfo(PatientInfo patientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.PatientExtendInfo.Insert", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[9];
                s[0] = patientInfo.ID;// סԺ��ˮ��
                s[1] = patientInfo.SIMainInfo.ICCardCode;//�Ÿ�֤��
                s[2] = patientInfo.SIMainInfo.AnotherCity.Name;//����
                s[3] = patientInfo.SIMainInfo.ApplyType.Name;//�������
                s[4] = patientInfo.SIMainInfo.ApplyType.ID;//����������
                s[5] = patientInfo.SIMainInfo.ItemPayCost.ToString();//���
                s[6] = patientInfo.SIMainInfo.AddTotCost.ToString();//�����
                s[7] = patientInfo.SIMainInfo.BaseCost.ToString();//��λ��
                s[8] = Operator.ID;//������

                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }
        #endregion

        #region ����ҽ��
        /// <summary>
        /// ����סԺ�Ż�ȡ����ҽ������Ļ��߼����Ϣ
        /// </summary>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        public DataTable QuerySIPatient(string patientNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.Patient.Select.ForGZSI", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, patientNO);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                DataSet dsRes = new DataSet();
                if (this.ExecQuery(strSql, ref dsRes) < 0)
                {
                    this.Err = "��ȡ����סԺ��Ϣʧ�ܡ�";
                    return null;
                }
                return dsRes.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ����סԺ�Ż�ȡ����ҽ������Ļ��߼����Ϣ���������ѽ�������
        /// {6A7E6EED-9AC4-4198-BD90-DEE5057BF5F6}
        /// </summary>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        public DataTable QuerySIPatientAll(string patientNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.Patient.Select.ForGZSIAll", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, patientNO);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                DataSet dsRes = new DataSet();
                if (this.ExecQuery(strSql, ref dsRes) < 0)
                {
                    this.Err = "��ȡ����סԺ��Ϣʧ�ܡ�";
                    return null;
                }
                return dsRes.Tables[0];
            }
            else
            {
                return null;
            }
        }

        ///{D3446DAF-E319-47a0-8BD5-EA748FCC4342}
        /// <summary>
        /// ͨ��ʱ�䣬סԺ�Ų�ѯ������Ϣ
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> GetInPatientByDatePatientNO(DateTime beginDate, DateTime endDate, string patientNO, string name)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.GetSIPersonInfo.Select.2", ref strSql) == -1)
            {
                return null;
            }

            try
            {
                strSql = string.Format(strSql, patientNO, name, beginDate.ToShortDateString(), endDate.ToShortDateString());
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            List<FS.HISFC.Models.RADT.PatientInfo> patientList = getSIRegisterInfo(strSql);

            return patientList;
        }

        /// <summary>
        /// {D3446DAF-E319-47a0-8BD5-EA748FCC4342}
        /// ��ȡҽ�����ߵǼ���Ϣ
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.RADT.PatientInfo> getSIRegisterInfo(string sql)
        {
            List<FS.HISFC.Models.RADT.PatientInfo> patientList = new List<FS.HISFC.Models.RADT.PatientInfo>();

            this.ExecQuery(sql);
            try
            {
                while (Reader.Read())
                {
                    FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
                    obj.SIMainInfo.HosNo = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SIMainInfo.BalNo = Reader[2].ToString();
                    obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
                    if (obj.SIMainInfo.MedicalType.ID == "1") { obj.SIMainInfo.MedicalType.Name = "סԺ"; }
                    else if (obj.SIMainInfo.MedicalType.ID == "2") { obj.SIMainInfo.MedicalType.Name = "�����ض���Ŀ"; }
                    else if (obj.SIMainInfo.MedicalType.ID == "2") { obj.SIMainInfo.MedicalType.Name = "����"; }
                    obj.PID.PatientNO = Reader[5].ToString();
                    obj.PID.CardNO = Reader[6].ToString();
                    obj.SSN = Reader[7].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[8].ToString());
                    obj.SIMainInfo.ProceatePcNo = Reader[9].ToString();
                    obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    obj.SIMainInfo.SiState = Reader[11].ToString();
                    obj.Name = Reader[12].ToString();
                    obj.Sex.ID = Reader[13].ToString();
                    obj.IDCard = Reader[14].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                    obj.SIMainInfo.EmplType = Reader[16].ToString();
                    obj.CompanyName = Reader[17].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[18].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[19].ToString();
                    obj.PVisit.PatientLocation.Dept.Name = Reader[20].ToString();
                    obj.Pact.PayKind.ID = Reader[21].ToString();
                    obj.Pact.ID = Reader[22].ToString();
                    obj.Pact.Name = Reader[23].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[24].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnose.ID = Reader[26].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[27].ToString();
                    if (!Reader.IsDBNull(28))
                        obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());
                    obj.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();
                    obj.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();
                    if (!Reader.IsDBNull(31))
                        obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString());
                    obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());
                    obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());
                    obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());
                    obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());
                    obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());
                    obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());
                    obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());
                    obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());
                    obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString());
                    obj.SIMainInfo.Memo = Reader[41].ToString();
                    obj.SIMainInfo.OperInfo.ID = Reader[42].ToString();
                    obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[43].ToString());
                    obj.SIMainInfo.RegNo = Reader[44].ToString();
                    obj.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[45].ToString());
                    obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[46].ToString());
                    obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[47].ToString());
                    obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[48].ToString());
                    obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString());
                    obj.SIMainInfo.IsSIUploaded = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());

                    if (string.IsNullOrEmpty(obj.SIMainInfo.HosNo) && obj.SIMainInfo.RegNo.Length > 6)
                    {
                        obj.SIMainInfo.HosNo = obj.SIMainInfo.RegNo.Substring(0, 6);
                    }

                    patientList.Add(obj);
                }

                return patientList;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                Reader.Close();
            }
        }

        /// <summary>
        /// {D3446DAF-E319-47a0-8BD5-EA748FCC4342}
        /// �����ϴ���ʶ
        /// </summary>
        /// <param name="patientNO"></param>
        /// <param name="uploadFlag"></param>
        /// <returns></returns>
        public int setSiUploadFlag(string patientNO, string uploadFlag)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.UpdateSIPersonInfo.1", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, patientNO, uploadFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        #endregion


        #region ��ɽҽ��

        /// <summary>
        /// ����סԺ�Ż�ȡ��ɽҽ������Ļ��߼����Ϣ// {09B8B8C2-203C-4ca0-A28C-FA4C55254856} lfhm 2020-01-09
        /// </summary>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        public DataTable QueryFoShanSIPatient(string patientNO)
        {
            string strSql = string.Empty;
            if (Sql.GetCommonSql("RADT.Inpatient.Patient.Select.ForFoShanSI", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, patientNO);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    ErrCode = ex.Message;
                    WriteErr();
                    return null;
                }
                DataSet dsRes = new DataSet();
                if (this.ExecQuery(strSql, ref dsRes) < 0)
                {
                    this.Err = "��ȡ����סԺ��Ϣʧ�ܡ�";
                    return null;
                }
                return dsRes.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡסԺ��Ҫ�ϴ��ķ�����ϸ
        /// </summary>
        /// <param name="registerID"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryInPatientNeedUploadFeeDetail(string registerID)
        {
            string sql = string.Format(@"select * from v_fin_ipb_fee_gzsi t where t.inpatient_no='{0}'  and t.upload_flag<>'2' ", registerID);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// ��ȡ��ƥ�䵫�ֹ�ѡ���ϴ�ҽ������Ŀ
        /// </summary>
        /// <param name="registerID"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryInPatientNotUploadFeeDetail(string registerID)
        {
            string sql = string.Format(@"select * from v_fin_ipb_fee_gzsi t where t.inpatient_no='{0}'  and t.upload_flag='2' ", registerID);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }
        #endregion
    }
}