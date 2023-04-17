using System;
using System.Collections;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Function;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Registration
{
    /// <summary>
    /// �ҺŹ�����
    /// </summary>
    public class Register : FS.FrameWork.Management.Database
    {
        /// <summary>
        ///  �ҺŹ�����
        /// </summary>
        public Register()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        //private ArrayList al = new ArrayList();
        private FS.HISFC.Models.Registration.Register reg;
       
        #region ����ɾ����

        //�˻����� ҽ��վ�չҺŷѣ��ùҺŷ��շ�״̬ {6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// �����չҺŷѱ�־
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="operID"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateAccountFeeState(string clinicID, string operID, string dept, DateTime operDate)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateAccountFeeState", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, operID, dept, operDate.ToString());
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "�û����շѱ�־����![Registration.Register.UpdateAccountFeeState]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// ���¹Һż�¼������Ϣ
        /// </summary>
        /// <param name="objRegister"></param>
        /// <returns></returns>
        public int UpdateRegFeeCost(FS.HISFC.Models.Registration.Register objRegister)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateRegFeeCost", ref sql) == -1)
            {
                this.Err = "��������Ϊ Registration.Register.UpdateRegFeeCost ��Sql���ʧ�ܣ�";
                return -1;
            }

            try
            {
                sql = string.Format(sql,
                    objRegister.ID,
                    objRegister.InvoiceNO,
                    objRegister.RegLvlFee.RegFee,
                    objRegister.RegLvlFee.ChkFee,
                    objRegister.RegLvlFee.OwnDigFee,
                    objRegister.RegLvlFee.OthFee,
                    objRegister.OwnCost,
                    objRegister.PubCost,
                    objRegister.PayCost);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "�û����շѱ�־����![Registration.Register.UpdateRegFeeCost]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ɾ���Һż�¼��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int DeleteByClinic(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Delete", ref sql) == -1) return -1;

            try
            {
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                sql = string.Format(sql, register.ID);
            }
            catch
            {
                return -1;
            }

            return this.ExecNoQuery(sql);

        }


        /// <summary>
        /// ����Һż�¼��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.Register register)
        {
            //{C8C76028-D071-41ce-8276-C7FA91F9F0C0}
            FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
            try
            {
                dept =this.GetDeptmentById(register.DoctorInfo.Templet.Dept.ID.ToString());
            }
            catch (Exception)
            {
               
            }
            string sql = "";
            if (register.TranType == FS.HISFC.Models.Base.TransTypes.Positive)
            {
                if (this.Sql.GetCommonSql("Registration.Register.GetInTimes", ref sql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }

                //�Ȼ�ȡ�ǼǴ���
                string inTimes = this.ExecSqlReturnOne(string.Format(sql, register.PID.CardNO));
                if (string.IsNullOrEmpty(inTimes) || inTimes.Equals("-1"))
                {
                    return -1;
                }

                register.InTimes = FS.FrameWork.Function.NConvert.ToInt32(inTimes);
            }

            if (this.Sql.GetCommonSql("Registration.Register.Insert.1", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {



                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                sql = string.Format(sql,
                    register.ID,                                                        //�����/��Ʊ��
                    register.PID.CardNO,                                                //���￨��
                    register.DoctorInfo.SeeDate.ToString(),                             // --�Һ�����
                    register.DoctorInfo.Templet.Noon.ID,                                //���
                    register.Name,                                                      //����
                    register.IDCard,                                                    //���֤��
                    register.Sex.ID,                                                    //�Ա�
                    register.Birthday.ToString(),                                       //������
                    register.Pact.PayKind.ID,                                           //��������
                    register.Pact.PayKind.Name,                                         //�����������
                    register.Pact.ID,                                                   //��ͬ��
                    register.Pact.Name,                                                 //��ͬ��λ����
                    register.SSN,                                                       //ҽ��֤��
                    register.DoctorInfo.Templet.RegLevel.ID,                            //�Һż���
                    register.DoctorInfo.Templet.RegLevel.Name,                          //�Һż�������
                    register.DoctorInfo.Templet.Dept.ID,                                //���Һ�
                    register.DoctorInfo.Templet.Dept.Name,                              //��������
                    register.DoctorInfo.SeeNO,                                          //�������
                    register.DoctorInfo.Templet.Doct.ID,                                //ҽʦ����
                    register.DoctorInfo.Templet.Doct.Name,                              //ҽʦ����
                    FS.FrameWork.Function.NConvert.ToInt32(register.IsFee),                       //�Һ��շѱ�־  
                    (int)register.RegType,                                                             //�Ƿ�ԤԼ   
                    FS.FrameWork.Function.NConvert.ToInt32(register.IsFirst),                     //1����/2����   
                    register.RegLvlFee.RegFee.ToString(),                                              //�Һŷ�   
                    register.RegLvlFee.ChkFee.ToString(),                                              //����   
                    register.RegLvlFee.OwnDigFee.ToString(),                                           //����   
                    register.RegLvlFee.OthFee.ToString(),                                              //���ӷ�   
                    register.OwnCost.ToString(),                                                       //�Էѽ��   
                    register.PubCost.ToString(),                                                       //�������   
                    register.PayCost.ToString(),                                                       //�Ը����   
                    (int)register.Status,                                                              //��Ч��־   
                    register.InputOper.ID,                                                             //����Ա����   
                    FS.FrameWork.Function.NConvert.ToInt32(register.IsSee),                       //�Ƿ���   
                    FS.FrameWork.Function.NConvert.ToInt32(register.CheckOperStat.IsCheck),       //1δ�˲�/2�Ѻ˲�   
                    register.PhoneHome,                                                                //��ϵ�绰   
                    register.AddressHome,                                                              //��ַ   
                    (int)register.TranType,                                                            //��������   
                    register.CardType.ID,                                                              //֤������   
                    register.DoctorInfo.Templet.Begin.ToString(),                                      //��ʼʱ��   
                    register.DoctorInfo.Templet.End.ToString(),                                        //��ʼʱ��
                    register.CancelOper.ID,                                                             //������
                    register.CancelOper.OperTime.ToString(),                                          //����ʱ��
                    register.InvoiceNO,                                                               //��Ʊ��
                    register.RecipeNO,                                                                //������
                    FS.FrameWork.Function.NConvert.ToInt32(register.DoctorInfo.Templet.IsAppend),
                    register.OrderNO,
                    register.DoctorInfo.Templet.ID,
                    register.InputOper.OperTime.ToString(),
                    register.InSource.ID,
                    FS.FrameWork.Function.NConvert.ToInt32(register.CaseState),
                    FS.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt),
                    register.NormalName,
                    register.EcoCost,
                    NConvert.ToInt32(register.IsAccount).ToString(),
                    NConvert.ToInt32(register.DoctorInfo.Templet.RegLevel.IsEmergency), /*{156C449B-60A9-4536-B4FB-D00BC6F476A1}*/
                    register.Mark1,
                    register.Card.ID,
                    register.Card.CardType.ID,
                    register.InTimes.ToString(),
                    register.PatientType,
                    register.Class1Desease, //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
                    register.Class2Desease,
                    register.User01,  //{91E7755E-E0D6-405d-92F3-A0585C0C1F2C}
                    register.RealDoctorID,//{AE399953-4F87-4199-8060-EFDC16AFAAF3}
                    register.RealDoctorName,
                    register.HospitalFirstVisit,  //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 
                    register.RootDeptFirstVisit,  //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 
                    register.DoctFirstVist,        //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 
                    register.AssignFlag,        //�����ʶ 0-δ���� 1-����
                    register.AssignStatus,      //����״̬ 0-δ���� 1-���� 2-���� 3-��� 4-���� 
                    register.FirstSeeFlag,      //�����ʶ 1-�� 0-�� 
                    register.PreferentialFlag,  //���ȱ�ʶ 1-�� 0-��
                    register.SequenceNO  ,      //������� 
                    dept.HospitalID,
                    dept.HospitalName //{3515892E-1541-47de-8E0B-E306798A358C}
                    );
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "����Һ������������![Registration.Register.Insert.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ���¹Һ���Ϣ,����(ע��)���˺š�ȡ�����ϡ����ơ��޸Ļ�����Ϣ
        /// </summary>
        /// <param name="status"></param>
        /// <param name="register"></param>
        /// <returns></returns>
        public int Update(EnumUpdateStatus status, FS.HISFC.Models.Registration.Register register)
        {
            if (status == EnumUpdateStatus.Cancel)
            {
                int i = this.CancelRegUnSeeDoctor(register.ID, register.CancelOper.ID, register.CancelOper.OperTime, status);
                return i;
            }
            else if (status == EnumUpdateStatus.Return)
            {
                return this.CancelReg(register.ID, register.CancelOper.ID, register.CancelOper.OperTime, status);
            }
            else if (status == EnumUpdateStatus.ChangeDept)
            {
                return this.ChangeDept(register);
            }
            else if (status == EnumUpdateStatus.PatientInfo)
            {
                return this.UpdatePatientInfoForNewClinicFee(register);//{69C503A2-4C1C-44D4-82A3-174ABDAC34C1}���ܸ��Ļ�����Ϣ
                // return this.UpdatePatientInfo(register);
            }
            else if (status == EnumUpdateStatus.Uncancel)
            {
                return this.Uncancel(register.ID);
            }
            else if (status == EnumUpdateStatus.Bad)
            {
                return this.CancelReg(register.ID, register.CancelOper.ID, register.CancelOper.OperTime, status);
            }
            return 0;
        }

        /// <summary>
        /// ���ѷ����־
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="operID"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int Update(string clinicID, string operID, DateTime operDate)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateTriage", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, operID, operDate.ToString());
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "�û��߷����־����![Registration.Register.UpdateTriage]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ����ԭ�йҺż�¼
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="cancelID"></param>
        /// <param name="cancelDate"></param>
        /// <param name="cancelFlag"></param>
        /// <returns></returns>
        private int CancelReg(string clinicID, string cancelID, DateTime cancelDate, EnumUpdateStatus cancelFlag)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.CancelReg", ref sql) == -1) return -1;

            try
            {
                int flag = (int)cancelFlag;
                if (cancelFlag == EnumUpdateStatus.Bad)
                {
                    flag = 3;
                }
                sql = string.Format(sql, clinicID, cancelID, cancelDate.ToString(), flag);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���ϹҺż�¼����![Registration.Register.CancelReg]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ����ԭ�йҺż�¼ {A2E63BDD-4FC3-488A-85AE-EC9791F820D9}
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="cancelID"></param>
        /// <param name="cancelDate"></param>
        /// <param name="cancelFlag"></param>
        /// <returns></returns>
        private int CancelRegUnSeeDoctor(string clinicID, string cancelID, DateTime cancelDate, EnumUpdateStatus cancelFlag)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.CancelRegUnSeeDoctor", ref sql) == -1) return -1;

            try
            {
                int flag = (int)cancelFlag;
                if (cancelFlag == EnumUpdateStatus.Bad)
                {
                    flag = 3;
                }
                sql = string.Format(sql, clinicID, cancelID, cancelDate.ToString(), flag);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���ϹҺż�¼����![Registration.Register.CancelRegUnSeeDoctor]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// ����(���ã����޸�����)
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        private int ChangeDept(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.ChangeDept", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.ID, register.DoctorInfo.Templet.Dept.ID, register.DoctorInfo.Templet.Dept.Name,
                    register.DoctorInfo.SeeNO, register.DoctorInfo.Templet.Doct.ID, register.DoctorInfo.Templet.Doct.Name,
                    register.RegLvlFee.RegFee, register.RegLvlFee.ChkFee, register.RegLvlFee.OwnDigFee, register.RegLvlFee.OthFee,
                    register.OwnCost, register.PubCost, register.PayCost);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���¹Һż�¼����![Registration.Register.ChangeDept]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// ȡ������(ע��)
        /// </summary>
        /// <param name="clinicID"></param>
        /// <returns></returns>
        private int Uncancel(string clinicID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Uncancel", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���ϹҺż�¼����![Registration.Register.Uncancel]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// ȡ������״̬
        /// </summary>
        /// <param name="clinicID"></param>
        /// <returns></returns>
        public int CancelTriage(string clinicID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.CancelTriage", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "ȡ���Һ���Ϣ�ķ���״̬����![Registration.Register.CancelTriage]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #region ���»�����Ϣ�������շѣ�
        /// <summary>
        /// ���»��߻�����Ϣ�������շѣ�
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdatePatientInfoForClinicFee(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            #region SQL
            /* UPDATE com_patientinfo   --���˻�����Ϣ��
               SET name='{0}',   --����
                   birthday=to_date('{1}','yyyy-mm-dd hh24:mi:ss'),   --��������
                   sex_code='{2}',   --�Ա�
                   home='{3}',   --���ڻ��ͥ����
                   home_tel='{4}',   --��ͥ�绰       
                   mark ='{6}',
                   inhos_source='{7}',
                   paykind_code='{8}',
                   pact_code='{9}',
                   pact_name='{10}',
                   mcard_no='{11}',
                   is_encryptname = '{12}',
                   normalname = '{13}'
             WHERE card_no = '{5}'*/
            #endregion

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Name, register.Birthday.ToString(), register.Sex.ID,
                                        register.AddressHome, register.PhoneHome, register.PID.CardNO, register.CardType.ID,
                                        register.InSource.ID, register.Pact.PayKind.ID, register.Pact.ID, register.Pact.Name,
                                        register.SSN, FS.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt), register.NormalName);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���»�����Ϣ����![Registration.Register.Update.PatientInfo.2]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// �޸Ļ��߻�����Ϣ�������շ�ֻ���Ľ������ͣ�//{69C503A2-4C1C-44D4-82A3-174ABDAC34C1}���»�����Ϣֻ���ĸ�������
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdatePatientInfoForNewClinicFee(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            #region SQL
            /* UPDATE com_patientinfo   --���˻�����Ϣ��
               SET name='{0}',   --����
                   birthday=to_date('{1}','yyyy-mm-dd hh24:mi:ss'),   --��������
                   sex_code='{2}',   --�Ա�
                   home='{3}',   --���ڻ��ͥ����
                   home_tel='{4}',   --��ͥ�绰       
                   mark ='{6}',
                   inhos_source='{7}',
                   paykind_code='{8}',
                   pact_code='{9}',
                   pact_name='{10}',
                   mcard_no='{11}',
                   is_encryptname = '{12}',
                   normalname = '{13}'
             WHERE card_no = '{5}'*/
            #endregion

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.4", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Pact.PayKind.ID, register.Pact.ID, register.Pact.Name,
                                        register.PID.CardNO);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���»�����Ϣ����![Registration.Register.Update.PatientInfo.4]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ���¹Һű��еĻ�����Ϣ�������շѣ�
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdateRegInfoForClinicFee(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            #region SQL
            /* UPDATE fin_opr_register   --�Һ�����
                SET name='{0}',   --����
                    birthday=to_date('{1}','yyyy-mm-dd hh24:mi:ss'),   --��������
                    sex_code='{2}',   --�Ա�
                    address='{3}',   --��ַ
                    rela_phone ='{4}' --��ϵ�绰
               WHERE clinic_code='{5}' and trans_type='1'*/
            #endregion

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.3", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Name,
                                        register.Birthday.ToString(),
                                        register.Sex.ID,
                                        register.AddressHome,
                                        register.PhoneHome,
                                        register.ID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���»�����Ϣ����![Registration.Register.Update.PatientInfo.3]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

        }
        #endregion
        /// <summary>
        /// ���»��߻�����Ϣ
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        private int UpdatePatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            //{D944AF1A-3BDE-4d51-BBA3-EB0FE779C7FC}�������֤��
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql,
                    register.Name,
                    register.Birthday.ToString(),
                    register.Sex.ID,
                    register.AddressHome,
                    register.PhoneHome,
                    register.PID.CardNO,
                    register.CardType.ID,
                    register.InSource.ID,
                    register.Pact.PayKind.ID,
                    register.Pact.ID,
                    register.Pact.Name,
                    register.SSN,
                    NConvert.ToInt32(register.IsEncrypt),
                    register.NormalName,
                    //{58B76445-C6F0-4492-921E-6407AAE9901A}���ӱ�ע��Ϣ����
                    register.IDCard,
                    register.Memo
                    );
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���»�����Ϣ����![Registration.Register.Update.PatientInfo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        #region {FCEC42B4-DF78-45c2-8D1A-EDAB94AA56DD} ����ʱ�޸Ļ��߻�����Ϣ

        /// <summary>
        /// ���¹Һű��еĻ�����Ϣ
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdateRegInfo(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Name,
                                                        register.Birthday.ToString(),
                                                        register.Sex.ID,
                                                        register.AddressHome,
                                                        register.PhoneHome,
                    //register.CardType.ID,
                    //register.InSource.ID,
                    //register.Pact.PayKind.ID,
                    //register.Pact.PayKind.Name,
                    //register.Pact.ID, 
                    //register.Pact.Name, 
                    //register.Pact.Name,
                    //register.SSN,
                    //FS.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt),
                    //register.NormalName,
                                                        register.IDCard,
                                                        register.ID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���»�����Ϣ����![Registration.Register.Update.PatientInfo.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

        }

        /// <summary>
        /// �޸���Ϣ���¹Һű����־���¶�
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdateRegInfoAdd(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.2", ref sql) == -1)
            {
                sql = "UPDATE fin_opr_register   SET is_emergency='{0}',  temperature='{1}'  WHERE clinic_code='{2}' and trans_type='1'";
            }
            string isEmergency = "";
            if (register.DoctorInfo.Templet.RegLevel.IsEmergency == true)
            { isEmergency = "1"; }
            else
            { isEmergency = "0"; }
            try
            {
                sql = string.Format(sql, isEmergency,
                                                        register.Temperature,
                                                        register.ID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���»�����Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

        }

        /// <summary>
        /// ����ʱ���»��߻�����Ϣ
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdatePatientForNurse(FS.HISFC.Models.Registration.Register register)
        {
            //{D944AF1A-3BDE-4d51-BBA3-EB0FE779C7FC}�������֤��
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Name, register.Birthday.ToString(), register.Sex.ID,
                                        register.AddressHome, register.PhoneHome, register.PID.CardNO, register.IDCard);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���»�����Ϣ����![Registration.Register.Update.PatientInfo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// ����{87C56F02-B81A-4fac-BA4D-654C8E56C500}
        /// </summary>
        /// <param name="clinicNO">�Һ���ˮ��</param>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="deptName">��������</param>
        /// <param name="doctCode">ҽ������</param>
        /// <param name="doctName">ҽ������</param>
        /// <param name="dtReg">�Һ�ʱ��</param>
        /// <returns></returns>
        public int UpdateDeptAndDoct(string clinicNO, string deptCode, string deptName, string doctCode, string doctName, string dtReg)
        {
            string strSql = string.Empty;
            int returnValue = this.Sql.GetCommonSql("Registration.Register.UpdateDeptAndDoct", ref  strSql);
            if (returnValue < 0)
            {
                this.Err = "û��Registration.Register.UpdateDeptAndDoct��Ӧ��sql���";
                return -1;
            }
            strSql = string.Format(strSql, clinicNO, deptCode, deptName, doctCode, doctName, dtReg);
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region ����

        #region �ҺŸ����޶�
        /// <summary>
        /// ���¿������
        /// </summary>
        /// <param name="Type">1ҽ�� 2���� 4ȫԺ</param>
        /// <param name="seeDate">��������</param>
        /// <param name="Subject">Type=1ʱ,ҽ������;Type=2,���Ҵ���;Type=4,ALL</param>
        /// <param name="noonID">���</param>
        /// <returns></returns>
        public int UpdateSeeNo(string Type, DateTime seeDate, string Subject, string noonID)
        {
            string sql = "";

            #region ���¿������

            if (this.Sql.GetCommonSql("Registration.Register.UpdateSeeSequence", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), Type, Subject, noonID);
                int rtn = this.ExecNoQuery(sql);

                if (rtn == -1) return -1;

                //û�и��¼�¼,����һ���¼�¼
                if (rtn == 0)
                {
                    if (this.Sql.GetCommonSql("Registration.Register.InsertSeeSequence", ref sql) == -1) return -1;

                    sql = string.Format(sql, seeDate.Date.ToString(), Type, Subject, "", 1, noonID);

                    if (this.ExecNoQuery(sql) == -1) return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = "���¿�����ų���" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            #endregion
            return 0;
        }


        /// <summary>
        /// ��û��߿������
        /// </summary>
        /// <param name="Type">Type:1ר����š�2������š�4ȫԺ���</param>
        /// <param name="current">��������</param>
        /// <param name="subject">Type=1ʱ,ҽ������;Type=2,���Ҵ���;Type=4,ALL</param>
        /// <param name="noonID">���</param>
        /// <param name="seeNo">��ǰ�����</param>
        /// <returns></returns>
        public int GetSeeNo(string Type, DateTime current, string subject, string noonID, ref int seeNo)
        {
            string sql = "", rtn = "";

            if (this.Sql.GetCommonSql("Registration.Register.getSeeNo", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, current.Date.ToString(), Type, subject, noonID);

                rtn = this.ExecSqlReturnOne(sql, "0");

                seeNo = FS.FrameWork.Function.NConvert.ToInt32(rtn);

                return 0;
            }
            catch (Exception e)
            {
                this.Err = "��ѯ������ų���![Registration.Register.getSeeNo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        #endregion

        #region �����ս�����
        /// <summary>
        /// ���ݲ���Ա��ʱ��θ����ս���Ϣ
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="OperID"></param>
        /// <param name="BalanceID"></param>
        /// <returns></returns>
        public int Update(DateTime begin, DateTime end, string OperID, string BalanceID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.DayBalance", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, begin.ToString(), end.ToString(), OperID, BalanceID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "�ùҺ���Ϣ�ս��־����![Registration.Register.Update.DayBalance]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #region �����ѿ�����շѱ��

        /// <summary>
        /// �����ѿ�����շѱ��
        /// </summary>
        /// <param name="Type">1ҽ�� 2���� 4ȫԺ</param>
        /// <param name="seeDate">��������</param>
        /// <param name="Subject">Type=1ʱ,ҽ������;Type=2,���Ҵ���;Type=4,ALL</param>
        /// <param name="noonID">���</param>
        /// <returns></returns>
        public int UpdateYNSeeAndCharge(string clinicCode)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateYNFlag", ref sql) == -1)
                return -1;
            try
            {
                sql = string.Format(sql, clinicCode);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "�����ѿ�����շѱ��" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return 0;
        }

        #endregion

        #region ����com_patientinfoʱ���¹Һű�

        /// <summary>
        /// �޸Ļ��߻�����Ϣʱ�����¹ҺŲ�����Ϣ ����clinicCode
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣʵ��</param>
        /// <returns></returns>
        public int UpdateRegInfoByClinicCode(FS.HISFC.Models.Registration.Register patientInfo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateRegByClinicNo", ref sql) == -1)
                return -1;
            try
            {
                sql = string.Format(sql,
                                patientInfo.ID,
                                patientInfo.Name,
                                patientInfo.Sex.ID,
                                patientInfo.Birthday,
                                patientInfo.IDCard,
                                patientInfo.Pact.PayKind.ID,
                                patientInfo.Pact.PayKind.Name,
                                patientInfo.Pact.ID,
                                patientInfo.Pact.Name
                                );
                this.ExecNoQuery(sql);
                return 1;
            }
            catch (Exception e)
            {
                this.Err = "�Һ���Ϣʧ�ܣ�" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// �޸Ļ��߻�����Ϣʱ�����¹Һ������Ϣ
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣʵ��</param>
        /// <returns></returns>
        public int UpdateRegByPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateRegByPatientInfo", ref sql) == -1)
                return -1;
            try
            {
                sql = string.Format(sql,

                                patientInfo.PID.CardNO,
                                patientInfo.Name, patientInfo.Sex.ID,
                                patientInfo.Birthday,
                                patientInfo.IDCard,
                                patientInfo.Pact.PayKind.ID,
                                patientInfo.Pact.PayKind.Name,
                                patientInfo.Pact.ID,
                                patientInfo.Pact.Name
                                );

                this.ExecNoQuery(sql);
                return 1;
            }
            catch (Exception e)
            {
                this.Err = "�Һ���Ϣʧ�ܣ�" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return 0;
        }

        #endregion

        #region ����CRM�ķ�����Ϣ���Һű�
        /// <summary>
        /// ����CRM�ķ�����Ϣ���Һű�
        /// </summary>        
        /// <param name="register">�Һ���Ϣ</param>
        /// <returns></returns>
        public int UpdateCRMAssign(FS.HISFC.Models.Registration.Register register)
        {
            if (string.IsNullOrEmpty(register.ID))
            {
                this.Err = "��������";
                return -1;
            }
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.UpdateCRMAssign", ref sql) == -1)
                return -1;
            try
            {
                sql = string.Format(sql, register.ID, register.AssignFlag, register.AssignStatus, register.FirstSeeFlag, register.PreferentialFlag, register.SequenceNO);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "����CRM�ķ�����Ϣʧ��" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #endregion

        #region �Զ�ȡ����
        /// <summary>
        /// ȡ���ݿ�����ֵ����Ϊ���￨��
        /// </summary>
        /// <returns>����ֵ</returns>
        public int AutoGetCardNO()
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.GetNewCardNo", ref sql) == -1) return -1;

            try
            {
                return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
            }
            catch (Exception e)
            {
                this.Err = "�Զ�ȡ���ų���![Registration.Register.GetNewCardNo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// ȡ���ݿ�����ֵ����Ϊ���￨�ţ����������Һţ�
        /// </summary>
        /// <returns>����ֵ</returns>
        public long AutoGetCardNOForSelfHelpReg()
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.GetNewCardNo.SelfHelpReg", ref sql) == -1) return -1;

            try
            {
                return Convert.ToInt64(this.ExecSqlReturnOne(sql));
            }
            catch (Exception e)
            {
                this.Err = "�Զ�ȡ���ų���![Registration.Register.GetNewCardNo.SelfHelpReg]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #region ���ʹ��
        #region �����Ѿ�����

        /// <summary>
        ///  �����Ѿ����������������ˮ��
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public int UpdateSeeDone(string clinicNo)
        {
            string sql = "Registration.Register.Update.SeeDone";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            return this.ExecNoQuery(sql, clinicNo);
        }

        #endregion

        #region ���¿������
        /// <summary>
        /// ���¿������
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="seeDeptID"></param>
        /// <param name="seeDoctID"></param>
        /// <returns></returns>
        public int UpdateDept(string clinicID, string seeDeptID, string seeDoctID)
        {
            string sql = "";
            string[] parm = new string[] { clinicID, seeDeptID, seeDoctID };

            if (this.Sql.GetCommonSql("Registration.Register.Query.17", ref sql) == -1) return -1;

            return this.ExecNoQuery(sql, parm);
        }
        #endregion



        #endregion


        #region �������Ų�ѯһ������ĹҺ���Ϣ,����

        /// <summary>
        /// ���ݲ����Ų�ѯ�������һ�ιҺ���Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Register Query(string cardNo)
        {
            ArrayList al = this.QueryRegListBase("Registration.Register.Query.2", cardNo);

            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.HISFC.Models.Registration.Register();
            }
            else
            {
                return (FS.HISFC.Models.Registration.Register)al[0];
            }
        }

        #endregion

        #region �����������ӹҺű��ѯ������Ϣ
        public ArrayList QueryRegisterByName(string name)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryByName", ref sql) == -1) return null;

            sql = string.Format(sql, name);

            if (this.ExecQuery(sql) == -1) return null;

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.reg = new FS.HISFC.Models.Registration.Register();

                    reg.PID.CardNO = this.Reader[0].ToString();
                    reg.Name = this.Reader[1].ToString();
                    reg.IDCard = this.Reader[2].ToString();
                    reg.Sex.ID = this.Reader[3].ToString();
                    reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());
                    reg.PhoneHome = this.Reader[5].ToString();
                    reg.AddressHome = this.Reader[6].ToString();
                    reg.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());

                    al.Add(reg);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "�������߻�����Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;

        }
        #endregion
        #region ���������Ʋ�ѯ���߻�����Ϣ
        /// <summary>
        /// ���ݻ���������ѯ
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ArrayList QueryByName(string Name)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.10", ref sql) == -1) return null;

            sql = string.Format(sql, Name);

            if (this.ExecQuery(sql) == -1) return null;

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.reg = new FS.HISFC.Models.Registration.Register();

                    reg.PID.CardNO = this.Reader[0].ToString();
                    reg.Name = this.Reader[1].ToString();
                    reg.IDCard = this.Reader[2].ToString();
                    reg.Sex.ID = this.Reader[3].ToString();
                    reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());
                    reg.PhoneHome = this.Reader[5].ToString();
                    reg.AddressHome = this.Reader[6].ToString();

                    al.Add(reg);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "�������߻�����Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;
        }
        #endregion

        public ArrayList GetByIDCard(string IDCard)
        {
            return this.QueryRegListBase("Registration.Register.Query.IDCard", IDCard);
        }

        /// <summary>
        /// ���ݲ����� ��� ҽ�� ���� �Һ�ʱ�� ���Ƿ��Ѿ��ҹ���D8F6425B-1CFD-4b3f-921E-03B1ECA0F95E
        /// </summary>
        /// <param name="card_no"></param>
        /// <param name="noon"></param>
        /// <param name="doctorId"></param>
        /// <param name="depteCode"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public bool QueryRegByNoonDoctor(string card_no, string noon, string doctorId, string depteCode, DateTime regDate)
        {
            string[] arr = new string[] { card_no, noon, doctorId, depteCode, regDate.ToString("yyyy-MM-dd") };
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.QueryRegByNoodDoctor", ref sql) == -1)
            {
                return false;
            }
            sql = string.Format(sql, arr);
            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
            return result > 0;
        }

        #region ������Ų�ѯһ���Һ���Ϣ
        /// <summary>
        /// ��������ˮ�Ų�ѯ�Һ���Ϣ
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Register GetByClinic(string clinicNo)
        {
            ArrayList al = this.QueryRegListBase("Registration.Register.Query.4", clinicNo);

            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.HISFC.Models.Registration.Register();
            }
            else
            {
                return (FS.HISFC.Models.Registration.Register)al[0];
            }
        }

        #endregion

        #region �������Ų�ѯһ���Һ���Ϣ
        /// <summary>
        /// �������Ų�ѯ
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <returns></returns>
        public ArrayList QueryByRecipe(string recipeNo)
        {
            string sql = "", where = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.14", ref where) == -1) return null;

            try
            {
                where = string.Format(where, recipeNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.14]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }
        #endregion

        //{B6E76F4C-1D79-4fa2-ABAD-4A22DE89A6F7}
        #region ���ݷ�Ʊ�Ų�ѯ�Һ���Ϣ
        /// <summary>
        /// ���ݷ�Ʊ�Ų�ѯ�Һ���Ϣ
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <returns></returns>
        public ArrayList QueryByRegInvoice(string invoiceNo)
        {
            string sql = "", where = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.22", ref where) == -1) return null;

            try
            {
                where = string.Format(where, invoiceNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.22]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        /// <summary>
        /// add by lijp 2012-08-24
        /// ���ݷ�Ʊ�Ų�ѯһ��ʱ���ڻ��ߵ���Ч�Һ���Ϣ
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <returns></returns>
        public ArrayList QueryByRegInvoice(string invoiceNo, DateTime limitDate)
        {
            string sql = "", where = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.24", ref where) == -1)
            {
                this.Err = "SQL���û���ҵ���Registration.Register.Query.24";
                return null;
            }

            try
            {
                where = string.Format(where, invoiceNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.24]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        #endregion

        #region ���ղ����ţ�ҽ����𣨴��ࣩ��ʱ����Ч��ѯ�Һ���Ϣ
        /// <summary>
        ///  ���ղ����ţ�ҽ����𣨴��ࣩ��ʱ����Ч��ѯ�Һ���Ϣ{46F865E4-9B79-4cc6-814D-3847DDBC85F9}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="beginDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <param name="payKindCode"></param>
        /// <returns></returns>
        public ArrayList QueryRegInfo(string cardNO, string beginDateTime, string EndDateTime, string payKindCode)
        {
            string sql = "", where = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.23", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNO, beginDateTime, EndDateTime, payKindCode);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.23]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }
        #endregion

        #region �������š���ʼʱ���ѯ���ߵĹҺ���Ϣ

        public ArrayList QueryRegListBase(string whereSQL)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
            {
                return null;
            }

            sql = sql + "\r\n" + whereSQL;

            return this.QueryRegister(sql);
        }

        private ArrayList QueryRegListBase(string whereSQLIndex, params string[] args)
        {

            string where = "";

            if (this.Sql.GetCommonSql(whereSQLIndex, ref where) == -1)
            {
                return null;
            }

            try
            {
                where = string.Format(where, args);
            }
            catch (Exception e)
            {
                this.Err = "[" + whereSQLIndex + "]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return QueryRegListBase(where);
        }

        /// <summary>
        /// ���ղ����Ų�ѯһ��ʱ���ڵĹҺż�¼
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="valide">�Ƿ���Ч 1 ��Ч��0 �˷ѣ�2 ���ϣ� ���� ȫ����¼</param>
        /// <returns></returns>
        public ArrayList QueryRegList(string cardNo, DateTime beginDate, DateTime endDate, string valide)
        {
            if (valide != "1" && valide != "0" && valide != "2")
            {
                valide = "All";
            }

            return this.QueryRegListBase("Registration.Register.Query.ByDateAndState", cardNo, beginDate.ToString(), endDate.ToString(), valide);
        }

        /// <summary>
        /// ��ѯ����һ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList Query(string cardNo, DateTime limitDate)
        {
            return this.QueryRegListBase("Registration.Register.Query.3", cardNo, limitDate.ToString());
        }

        /// <summary>
        /// ��ѯ����һ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="name"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryName(string name, DateTime limitDate)
        {
            return this.QueryRegListBase("Registration.Register.Query.25", name, limitDate.ToString());
        }

        ///��ѯ�����Ƿ����һ��ͬҽ��ͬ���ҵĹҺ���Ϣ {A2E63BDD-4FC3-488A-85AE-EC9791F820D9}
        public bool ExixtRegList(string cardNo, string deptcode, string doccode)
        {
            ArrayList list = this.QueryRegByDeptDocAndDay( cardNo,  deptcode,  doccode);//("Registration.Register.Query.ByDeptDocAndDay", cardNo, deptcode, doccode);//Registration.Register.Query.ByDeptDocAndDay
            if (list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //{A2E63BDD-4FC3-488A-85AE-EC9791F820D9}
        public ArrayList QueryRegByDeptDocAndDay(string cardNo, string deptcode, string doccode)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.ByDeptDocAndDay", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNo, deptcode,doccode);
            }
            catch (Exception e)
            {
                this.Err = "Registration.Register.Query.ByDeptDocAndDay" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryUnionNurse(string cardNo, DateTime limitDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.20", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.20]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        /// <summary>
        /// ��ѯһ��ʱ�������ϹҺ���Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryCancel(string cardNo, DateTime limitDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.16", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.16]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        /// <summary>
        /// ���ݲ����Ų�ѯ�ѿ������Ч�Һ���Ϣ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        public ArrayList GetRegisterByCardNODate(string cardNO, DateTime beginDate, DateTime endDate)
        {
            //Registration.Register.Query.Where
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.Where", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNO, beginDate.ToString(), endDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.Where]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        #region �����߷���ִ�п��Ҳ�ѯ�Һ���Ϣ
        /// <summary>
        /// �����߷���ִ�п��Ҳ�ѯ�Һ���Ϣ
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDept(string excuDeptID, string beginDate, string endDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByFeeExcuDept", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByFeeExcuDept]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        #region �����߷���ִ�п��Ҳ�ѯ�Һ���Ϣ--���Һ�ʱ��
        /// <summary>
        /// �����߷���ִ�п��Ҳ�ѯ�Һ���Ϣ
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDeptOrderByRegDate(string excuDeptID, string beginDate, string endDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByFeeExcuDeptOrderByRegDate", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByFeeExcuDept]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        /// <summary>
        /// �����߷�����С���ùҺ���Ϣ
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByMinFeeOrderByRegDate(string minFee, string beginDate, string endDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByMinFeeOrderByRegDate", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, minFee);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByMinFeeOrderByRegDate]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        #region �����߷���ִ�п��ҺͿ��Ų�ѯ�Һ���Ϣ
        /// <summary>
        /// �����߷���ִ�п��ҺͿ��Ų�ѯ�Һ���Ϣ
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDeptAndCardNo(string excuDeptID, string beginDate, string endDate, string CardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByFeeExcuDeptAndCardNo", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID, CardNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByFeeExcuDeptAndCardNo]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// �����߷���ִ�п��ҺͿ��Ų�ѯ�Һ���Ϣ--���Һ�ʱ��
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDeptAndCardNoOrderByRegDate(string excuDeptID, string beginDate, string endDate, string CardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByFeeExcuDeptAndCardNoOrderByRegDate", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID, CardNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByFeeExcuDeptAndCardNoOrderByFeeDate]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ��������С���úͿ��Ų�ѯ�Һ���Ϣ--���Һ�ʱ��
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByMinFeeAndCardNoOrderByRegDate(string excuDeptID, string beginDate, string endDate, string CardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByMinFeeAndCardNoOrderByRegDate", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID, CardNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByMinFeeAndCardNoOrderByRegDate]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        #endregion



        #region ��������Ų�ѯ���߹Һ���Ϣ �����շ�ʹ��
        /// <summary>
        /// ��������š���ʼʱ���ѯ�Һ���Ϣ
        /// </summary>
        /// <param name="seeNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeNo(string seeNo, DateTime limitDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.18", ref where) == -1) return null;

            try
            {
                where = string.Format(where, seeNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.18]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        /// <summary>
        /// �����Ƿ�Ժ��ְ�����������֤����
        /// </summary>
        /// <param name="IdenNO">����ߺ���</param>
        /// <returns></returns>
        public bool CheckIsEmployee(string IdenNO)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Registration.Register.CheckIsEmployee", ref sql) == -1)
            {
                this.Err += "û���ҵ�����Ϊ:Registration.Register.CheckIsEmployee ��SQL���";
                return false;
            }
            try
            {
                sql = string.Format(sql, IdenNO);
            }
            catch (Exception e)
            {
                this.Err = "����sql���ʧ��[Registration.Register.CheckIsEmployee]" + e.Message;
                this.ErrCode = e.Message;
                return false;
            }

            int count = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));

            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// �����Ƿ�Ժ��ְ�����������֤����
        /// </summary>
        /// <param name="IdenNO">����ߺ���</param>
        /// <returns></returns>
        public bool CheckIsEmployee(FS.HISFC.Models.Registration.Register register)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Registration.Register.CheckIsEmployeeByClinicNO", ref sql) == -1)
            {
                return this.CheckIsEmployee(register.IDCard);
            }
            try
            {
                sql = string.Format(sql, register.ID);
            }
            catch (Exception e)
            {
                this.Err = "����sql���ʧ��[Registration.Register.CheckIsEmployeeByClinicNO]" + e.Message;
                this.ErrCode = e.Message;
                return false;
            }

            int count = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));

            if (count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// ��ʱ���ͳ�Ʋ�ѯ�Һ�Ա����Ч�Һ���
        /// </summary>
        /// <param name="operID">�Һ�Աid</param>
        /// <param name="beginDateTime">��ʼʱ��</param>
        /// <param name="endDateTime">����ʱ��</param>
        /// <returns></returns>
        public string QueryValidRegNumByOperAndOperDT(string operID, string beginDateTime, string endDateTime)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Registration.QueryValidRegNumByOperAndOperDT.Select1", ref sql) == -1)
            {
                this.Err += "û���ҵ�����Ϊ:Registration.QueryValidRegNumByOperAndOperDT.Select1 ��SQL���";
                return "-1";
            }
            try
            {
                sql = string.Format(sql, operID, beginDateTime, endDateTime);
            }
            catch (Exception e)
            {
                this.Err = "���sql���ʧ��[Registration.QueryValidRegNumByOperAndOperDT.Select1]" + e.Message;
                this.ErrCode = e.Message;
            }

            return this.ExecSqlReturnOne(sql);
        }

        #region ������Ա��ʱ��β�ѯ�Һ���Ϣ
        /// <summary>
        /// ������Ա��ʱ��β�ѯ�Һ���Ϣ
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="operID"></param>
        /// <returns></returns>
        public ArrayList Query(DateTime beginDate, DateTime endDate, string operID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.9", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate.ToString(), endDate.ToString(), operID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.9]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        /// <summary>
        /// ��ѯ�����¼
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public int QueryRegiterByCardNO(string cardNO)
        {
            string sql = string.Empty;
            int returnValue = Sql.GetCommonSql("Registration.QueryRegiterByCardNO.Select.1", ref sql);
            if (returnValue == -1)
            {
                return -1;
            }
            try
            {
                sql = string.Format(sql, cardNO);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.QueryRegiterByCardNO.Select.1]����" + e.Message;
                return -1;

            }


            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));

            return result;
        }


        #region �ļ������ж�
        //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 

        /// <summary>
        /// �жϳ���
        /// </summary>
        /// <param name="type">�������� 1-Ժ�� 2-��Ƽ� 3-�Ƽ� 4-ҽ��</param>
        /// <param name="cardNO"></param>
        /// <param name="dept"></param>
        /// <param name="doct"></param>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public int IsFirstRegister(string type, string cardNO, string dept, string doct, DateTime beginTime)
        {
            int ret = 0;
            string sql = string.Empty;

            switch (type)
            {
                case "1"://Ժ��
                    ret = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.2", ref sql);
                    break;
                case "2"://��Ƽ�
                    ret = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.3", ref sql);
                    break;
                case "3"://�Ƽ�
                    ret = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.1", ref sql);
                    break;
                case "4"://ҽ����
                    ret = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.4", ref sql);
                    break;
                default:
                    ret = -1;
                    break;
            }

            if (ret == -1)
            {
                return ret;
            }

            try
            {
                if (type == "1")
                {
                    sql = string.Format(sql, cardNO, beginTime.ToString());
                }
                else if (type == "4")
                {
                    sql = string.Format(sql, cardNO, doct, beginTime.ToString());
                }
                else  //��Ƽ��ͿƼ�
                {
                    sql = string.Format(sql, cardNO, dept, beginTime.ToString());
                }
            }
            catch (Exception e)
            {
                this.Err = "��ȡ�����ж�SQL������" + e.Message;
                return -1;
            }

            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));

            return result;
        }

        /// <summary>
        /// {496701C2-CCAE-4a8d-B3DB-7D528CFF7025}
        /// ���ݿ���ʱ����Ҳ�ѯ�Һ�����
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="Dept"></param>
        /// <param name="BeginTime"></param>
        /// <returns></returns>
        public int QueryRegisterByCardNOTimeDept(string cardNO, string Dept, DateTime BeginTime)
        {
            string sql = string.Empty;
            int returnValue = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.1", ref sql);
            if (returnValue == -1)
            {
                return -1;
            }
            try
            {
                sql = string.Format(sql, cardNO, Dept, BeginTime.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.QueryRegiterByCardNODeptTime.Select.1]����" + e.Message;
                return -1;

            }
            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
            return result;
        }


        /// <summary>
        /// {2888444F-50BA-4956-A5F7-D71F0C6448BB}
        /// ���ݿ���ʱ��ҽ����ѯ�Һ�����
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="Dept"></param>
        /// <param name="BeginTime"></param>
        /// <returns></returns>
        public int QueryRegisterByCardNODoctTime(string cardNO, string deptID, string Doct, DateTime BeginTime)
        {
            string sql = string.Empty;
            int returnValue = Sql.GetCommonSql("Registration.QueryRegiterByCardNODoctTime.Select.1", ref sql);
            if (returnValue == -1)
            {
                return -1;
            }
            try
            {
                sql = string.Format(sql, cardNO, deptID, Doct, BeginTime.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.QueryRegiterByCardNODoctTime.Select.1]����" + e.Message;
                return -1;

            }
            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
            return result;
        }

        #endregion

        #region ��ѯһ��ʱ����δ����ĹҺŻ��� ���ﻤʿʹ��
        /// <summary>
        /// ��ѯһ��ʱ����δ����ĹҺŻ���
        /// </summary>
        /// <param name="begin"></param>
        /// <returns></returns>
        public ArrayList QueryNoTriage(DateTime begin)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.5", ref where) == -1) return null;

            try
            {
                where = string.Format(where, begin.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.5]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        #region ����
        /// <summary>
        /// ͨ��һ��ʱ���� ĳ����վ��Ӧ���ҵĹҺŻ��� addby sunxh
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="myNurseDept">����վ����</param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyDept(DateTime begin, string myNurseDept)
        {

            string sql = ""; string where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byNurseDept", ref where) == -1) return null;

            where = string.Format(where, begin.ToString(), myNurseDept);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ͨ��һ��ʱ���� ĳ����վ�ĹҺŻ���{F044FCF3-6736-4aaa-AA04-4088BB194C20}
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="myNurseDept">����վ����</param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyNurse(DateTime begin, string NurseID)
        {
            string sql = ""; string where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byNurseID", ref where) == -1) return null;

            where = string.Format(where, begin.ToString(), NurseID);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ͨ��һ��ʱ���� ĳ����վ��Ӧ���ҵĹҺŻ���δ���� addby niuxy
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="myNurseDept">����վ����</param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyDeptUnSee(DateTime begin, string myNurseDept)
        {
            string sql = ""; string where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byNurseDept1", ref where) == -1) return null;

            where = string.Format(where, begin.ToString(), myNurseDept);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ����������жϹҺ���Ϣ�Ƿ����
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public bool QueryIsTriage(string clinicNo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.IsTriage", ref sql) == -1) return false;

            try
            {
                sql = string.Format(sql, clinicNo);

                string rtn = this.ExecSqlReturnOne(sql, "0");

                // return FS.FrameWork.Function.NConvert.ToBoolean(rtn) ;
                if (rtn == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.IsTriage]" + e.Message;
                this.ErrCode = e.Message;
                return false;
            }
        }

        /// <summary>
        /// ����������жϹҺ���Ϣ�Ƿ�����
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public bool QueryIsCancel(string clinicNo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.IsCancel", ref sql) == -1) return false;

            try
            {
                sql = string.Format(sql, clinicNo);

                string rtn = this.ExecSqlReturnOne(sql, "0");

                if (rtn == "1")
                {
                    return false;//��Ч,δ����
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.IsCancel]" + e.Message;
                this.ErrCode = e.Message;
                return false;
            }
        }

        /// <summary>
        /// ��ѯ������Ч�Һż�¼
        /// ��������������״̬
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryUnionNurseTriage(string cardNo, DateTime limitDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.ByInTriage", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.ByInTriage]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ��û��߿������
        /// </summary>
        /// <param name="Type">Type:1ר����š�2������š�4ȫԺ���</param>
        /// <param name="current">��������</param>
        /// <param name="subject">Type=1ʱ,ҽ������;Type=2,���Ҵ���;Type=4,ALL</param>
        /// <param name="noonID">���</param>
        /// <param name="seeNo">��ǰ�����</param>
        /// <returns></returns>
        public int GetSeeNo(string Type, DateTime current, string subject, string noonID, ref string seeNo)
        {
            string sql = "", rtn = "";

            if (this.Sql.GetCommonSql("Registration.Register.getSeeNo", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, current.Date.ToString(), Type, subject, noonID);

                rtn = this.ExecSqlReturnOne(sql, "0");

                seeNo = rtn;

                return 0;
            }
            catch (Exception e)
            {
                this.Err = "��ѯ������ų���![Registration.Register.getSeeNo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        #endregion

        #region ��ѯ���ѻ���ĳ�չҺ�����
        /// <summary>
        /// ��ѯ���ѻ���ĳ�չҺ�����
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public int QuerySeeNum(string cardNo, DateTime regDate)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.12", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, cardNo, regDate.Date.ToString(), regDate.Date.AddDays(1).ToString());
                string Cnt = this.ExecSqlReturnOne(sql, "0");

                return FS.FrameWork.Function.NConvert.ToInt32(Cnt);
            }
            catch (Exception e)
            {
                this.Err = "��û��߹Һ���������![Registration.Register.Query.12]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #region ������Ų�ѯ�Ѵ�ӡ��Ʊ����
        /// <summary>
        /// ������Ų�ѯ�Ѵ�ӡ��Ʊ����
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public int QueryPrintedInvoiceCnt(string clinicNo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.15", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicNo);
                string Cnt = this.ExecSqlReturnOne(sql, "0");

                return FS.FrameWork.Function.NConvert.ToInt32(Cnt);
            }
            catch (Exception e)
            {
                this.Err = "��û��ߴ�ӡ��Ʊ��������![Registration.Register.Query.15]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ������Ÿ����Ѵ�ӡ��Ʊ����
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public int UpdatePrintInvoiceCnt(string clinicNo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.InvoiceCnt", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicNo);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���»��ߴ�ӡ��Ʊ��������![Registration.Register.Update.InvoiceCnt]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #region ���в�ѯ

        /// <summary>
        /// �ҺŲ�ѯ
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList QueryRegister(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.reg = new FS.HISFC.Models.Registration.Register();

                    this.reg.ID = this.Reader[0].ToString();//���
                    this.reg.PID.CardNO = this.Reader[1].ToString();//������
                    this.reg.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString());//�Һ�����
                    this.reg.DoctorInfo.Templet.Noon.ID = this.Reader[3].ToString();
                    this.reg.Name = this.Reader[4].ToString();
                    this.reg.IDCard = this.Reader[5].ToString();
                    this.reg.Sex.ID = this.Reader[6].ToString();

                    this.reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());//��������

                    this.reg.Pact.PayKind.ID = this.Reader[8].ToString();//�������
                    this.reg.Pact.PayKind.Name = this.Reader[9].ToString();

                    this.reg.Pact.ID = this.Reader[10].ToString();//��ͬ��λ
                    this.reg.Pact.Name = this.Reader[11].ToString();
                    this.reg.SSN = this.Reader[12].ToString();
                    this.reg.SIMainInfo.RegNo = this.reg.SSN;

                    this.reg.DoctorInfo.Templet.RegLevel.ID = this.Reader[13].ToString();//�Һż���
                    this.reg.DoctorInfo.Templet.RegLevel.Name = this.Reader[14].ToString();

                    this.reg.DoctorInfo.Templet.Dept.ID = this.Reader[15].ToString();//�Һſ���
                    this.reg.DoctorInfo.Templet.Dept.Name = this.Reader[16].ToString();

                    this.reg.DoctorInfo.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[17].ToString());

                    this.reg.DoctorInfo.Templet.Doct.ID = this.Reader[18].ToString();//����ҽ��
                    this.reg.DoctorInfo.Templet.Doct.Name = this.Reader[19].ToString();

                    this.reg.RegType = (FS.HISFC.Models.Base.EnumRegType)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[20].ToString());
                    this.reg.IsFirst = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[21].ToString());

                    this.reg.RegLvlFee.RegFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());
                    this.reg.RegLvlFee.ChkFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23].ToString());
                    this.reg.RegLvlFee.OwnDigFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24].ToString());
                    this.reg.RegLvlFee.OthFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25].ToString());

                    this.reg.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());
                    this.reg.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString());
                    this.reg.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[28].ToString());

                    this.reg.Status = (FS.HISFC.Models.Base.EnumRegisterStatus)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());

                    this.reg.InputOper.ID = this.Reader[30].ToString();
                    this.reg.IsSee = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[31].ToString());
                    this.reg.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[32].ToString());
                    this.reg.TranType = (FS.HISFC.Models.Base.TransTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[33].ToString());
                    this.reg.BalanceOperStat.IsCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[34]);//�ս�
                    this.reg.BalanceOperStat.CheckNO = this.Reader[35].ToString();
                    this.reg.BalanceOperStat.Oper.ID = this.Reader[36].ToString();

                    if (!this.Reader.IsDBNull(37))
                        this.reg.BalanceOperStat.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[37].ToString());

                    this.reg.PhoneHome = this.Reader[38].ToString();//��ϵ�绰
                    this.reg.AddressHome = this.Reader[39].ToString();//��ַ
                    this.reg.IsFee = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[40].ToString());
                    //��������Ϣ
                    this.reg.CancelOper.ID = this.Reader[41].ToString();
                    this.reg.CancelOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42].ToString());
                    this.reg.CardType.ID = this.Reader[43].ToString();//֤������
                    this.reg.DoctorInfo.Templet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[44].ToString());
                    this.reg.DoctorInfo.Templet.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[45].ToString());
                    //this.reg.InvoiceNo = this.Reader[50].ToString() ;
                    //this.reg.InvoiceNO = this.Reader[51].ToString() ; by niuxinyuan
                    this.reg.InvoiceNO = this.Reader[50].ToString();
                    this.reg.RecipeNO = this.Reader[51].ToString();

                    this.reg.DoctorInfo.Templet.IsAppend = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[52].ToString());
                    this.reg.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[53].ToString());
                    this.reg.DoctorInfo.Templet.ID = this.Reader[54].ToString();
                    this.reg.InSource.ID = this.Reader[55].ToString();
                    this.reg.PVisit.InState.ID = this.Reader[56].ToString();
                    this.reg.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[57].ToString());
                    this.reg.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[58].ToString());
                    this.reg.PVisit.ZG.ID = this.Reader[59].ToString();
                    this.reg.PVisit.PatientLocation.Bed.ID = this.Reader[60].ToString();

                    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    //��ʶ�Ƿ����˻����̹Һ� 1������
                    this.reg.IsAccount = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[61].ToString());

                    //{E26C3EE9-D480-421e-9FD3-7094D8E4E1D0}
                    this.reg.SeeDoct.Dept.ID = this.Reader[62].ToString(); //�������
                    this.reg.SeeDoct.ID = this.Reader[63].ToString();//����ҽ��
                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    this.reg.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[64].ToString());
                    //{921FBFCA-3D0D-4bc6-8EEA-A9BBE152E69A}
                    this.reg.Mark1 = this.Reader[65].ToString();
                    // this.reg.PID.CaseNO =this.q;

                    // {531B6C65-1DF5-4f16-94EC-F7D87287966F}
                    this.reg.SeeDoct.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[46].ToString());
                    //�����Ƿ��Ѿ�����
                    this.reg.IsTriage = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[47].ToString());
                    //{4AC12996-BC4B-4272-9FA4-E06DB8326330}
                    if (this.Reader.FieldCount >= 67)
                    {
                        this.reg.NormalName = this.Reader[66].ToString();

                    }
                    if (this.Reader.FieldCount > 67)
                    {
                        this.reg.Card.ID = this.Reader[67].ToString();
                        this.reg.Card.CardType.ID = this.Reader[68].ToString();
                        this.reg.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[69].ToString());
                    }
                    if (this.Reader.FieldCount > 70)
                    {
                        this.reg.Temperature = this.Reader[70].ToString();
                    }
                    if (Reader.FieldCount > 71)
                    {
                        reg.PatientType = Reader[71].ToString();
                    }

                    if (Reader.FieldCount > 72)//ʵ�����// {F53BD032-1D92-4447-8E20-6C38033AA607}
                    {
                        reg.EcoCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[72].ToString());
                    }

                    if (Reader.FieldCount > 73)//һ������  //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
                    {
                        reg.Class1Desease = this.Reader[73].ToString();
                    }

                    if (Reader.FieldCount > 74)//��������  //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
                    {
                        reg.Class2Desease = this.Reader[74].ToString();
                    }

                    if (Reader.FieldCount > 75)//ֱ���շѱ�� {91E7755E-E0D6-405d-92F3-A0585C0C1F2C}
                    {
                        reg.User01 = this.Reader[75].ToString();
                    }
                    if (Reader.FieldCount > 76)//�����ʶ 0-δ���� 1-����
                    {
                        reg.AssignFlag = this.Reader[76].ToString();
                    }
                    if (Reader.FieldCount > 77)//����״̬ 0-δ���� 1-���� 2-���� 3-��� 4-����
                    {
                        reg.AssignStatus = this.Reader[77].ToString();
                    }
                    if (Reader.FieldCount > 78)//�����ʶ1-�� 0-��
                    {
                        reg.FirstSeeFlag = this.Reader[78].ToString();
                    }
                    if (Reader.FieldCount > 79)//���ȱ�ʶ1-�� 0-��
                    {
                        reg.PreferentialFlag = this.Reader[79].ToString();
                    }
                    if (Reader.FieldCount > 80)//�������
                    {
                        reg.SequenceNO = NConvert.ToInt32(this.Reader[80].ToString());
                    }
                    if (Reader.FieldCount > 81)//�������
                    {
                        reg.Hospital_id = this.Reader[81].ToString();
                    }
                    if (Reader.FieldCount > 82)//�������
                    {
                        reg.Hospital_name = this.Reader[82].ToString();
                    }
                    al.Add(this.reg);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "�����Һ���Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }

        /// <summary>
        /// ��ѯҽ���ϴ���־���еĺ�ͬ��λ����
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public string GetPactCodeFoMedcare(string clinicCode)
        {
            string defaultsql = "select pact_code from fin_ipr_sirecord where clinic_code='{0}'";
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.GetPactCodeFoMedcare.1", ref sql) == -1)
            {
                sql = defaultsql;
            }
            try
            {
                sql = string.Format(sql, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "Registration.Register.GetPactCodeFoMedcare.1" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            return ExecSqlReturnOne(sql);
        }
        #endregion

        #region ����ҽ��վʹ�ò�ѯ

        /// <summary>
        /// ���Һ�ҽ����ѯĳһ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="doctID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryByDoct(string doctID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.7", ref where) == -1) return null;

            try
            {
                where = string.Format(where, doctID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.7]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ���Һſ��Ҳ�ѯĳһ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryByDept(string deptID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.8", ref where) == -1) return null;

            try
            {
                where = string.Format(where, deptID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.8]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ������ҽ����ѯĳһ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDoc(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.19", ref where) == -1) return null;

            try
            {
                where = string.Format(where, docID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.19]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ������ҽ����ѯĳһ��ʱ�����Ѿ��������Ч��{A448C42B-AEA2-4a36-889C-C5AB97C38A6B}
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDocAndSeeDate(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.21", ref where) == -1) return null;

            try
            {
                where = string.Format(where, docID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.21]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ��������ʱ��һ��ʱ�����Ѿ��������Ч��
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDocAndSeeDate2(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.opsApply", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.opsApplyWhere", ref where) == -1) return null;

            try
            {
                where = string.Format(where, docID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.21]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        #endregion

        #region
        /// <summary>
        /// ��ѯע���һ�����Ϣ
        /// </summary>
        /// <param name="cardNo">���ţ�Ϊ��ʱ��ʾ��ѯȫ��</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="isPrint">�Ƿ��ӡ</param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryInject(string cardNo, DateTime beginTime, DateTime endTime, bool isPrint, string dept, string unDrugUsage, string drugUsage, ref System.Data.DataSet ds)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.Inject", ref sql) == -1)
            {
                ds = null;
                return -1;
            }

            try
            {
                if (isPrint)
                {
                    //�Ѵ�ӡ���޶�  999999>��ӡ����>=1
                    sql = string.Format(sql, beginTime.ToString(), endTime.ToString(), cardNo.Trim(), 1, 9999999, dept, unDrugUsage, drugUsage);
                }
                else
                {
                    //δ��ӡ���޶�  1>��ӡ����>=0
                    sql = string.Format(sql, beginTime.ToString(), endTime.ToString(), cardNo.Trim(), 0, 1, dept, unDrugUsage, drugUsage);
                }
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.Inject]" + e.Message;
                this.ErrCode = e.Message;
                ds = null;
                return -1;
            }

            return this.ExecQuery(sql, ref ds);
        }

        #endregion



        #region ����������ѯ���л�����Ϣ�Ļ���
        /// <summary>
        /// ����������ѯ���л�����Ϣ�Ļ���
        /// </summary>
        /// <param name="name" >����</param>
        /// <param name="days ">��Ч����</param>
        /// <returns></returns>
        public ArrayList QueryRegHaveChargedInfo(string name, int days)
        {
            string strSql = "";

            ArrayList al = new ArrayList();

            if (this.Sql.GetCommonSql("Registration.Register.Query.HaveChargedInfo", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Registration.Register.Query.HaveChargedInfo";
                return null;
            }
            strSql = System.String.Format(strSql, name, days);
            if (this.ExecQuery(strSql) < 0)
            {
                this.Err = "Execute Err;";
                return null;
            }

            while (this.Reader.Read())
            {
                this.reg = new FS.HISFC.Models.Registration.Register();

                reg.ID = this.Reader[0].ToString();//��ˮ��
                reg.PID.CardNO = this.Reader[1].ToString();//������
                reg.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());//����
                reg.Name = this.Reader[3].ToString();//����
                reg.DoctorInfo.Templet.Dept.ID = this.Reader[4].ToString();
                reg.DoctorInfo.Templet.Dept.Name = this.Reader[5].ToString();//�Һſ���
                reg.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
                reg.Sex.ID = this.Reader[7].ToString();
                reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());//��������
                reg.Pact.ID = this.Reader[9].ToString();
                reg.Pact.Name = this.Reader[10].ToString();//��ͬ��λ
                reg.DoctorInfo.Templet.Doct.ID = this.Reader[11].ToString();
                reg.DoctorInfo.Templet.Doct.Name = this.Reader[12].ToString();//�Һ�ҽ��
                reg.SSN = this.Reader[13].ToString();//ҽ��֤��
                reg.DoctorInfo.Templet.RegLevel.ID = this.Reader[14].ToString();
                reg.DoctorInfo.Templet.RegLevel.Name = this.Reader[15].ToString();

                al.Add(reg);
            }
            this.Reader.Close();
            return al;
        }
        #endregion


        #region ����ʿվ�ͼ�������״̬��ѯ�����б�
        /// <summary>
        /// ����ʿվ�ͼ�������״̬��ѯ�����б�
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByNurseCell(string nurseCellCode, string status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byNurseCellCode", ref where) == -1) return null;

            where = string.Format(where, nurseCellCode, status);

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// ҽ��վ�������ۻ�����Ϣ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByNurseCell(string deptCode)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.QueryEnEmergencyPatient.byDeptCode", ref where) == -1) return null;

            where = string.Format(where, deptCode);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }


        //{FC2B9551-0246-4375-8667-8EFF39A5CC6C}
        /// <summary>
        /// ���ػ�����Ϣ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByNameOrPhone(string code)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.QueryEnEmergencyPatient.byNameOrCardNo", ref where) == -1) return null;

            where = string.Format(where, code);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        #endregion

        #region ����ʿվ�ͼ�������״̬��ѯ�����б�

        /// <summary>
        /// �����Ҳ�ѯ�ͼ�������״̬��ѯ�����б�
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(string deptcode, string status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byDeptCode", ref where) == -1) return null;

            where = string.Format(where, deptcode, status);

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        /// <summary>
        /// �������۲�ѯ��ǰ����վ�Ĳ�ͬ״̬�Ĳ�����Ϣ(����)
        /// </summary>
        /// <param name="deptcode">���ұ���</param>
        /// <param name="status">״̬</param>
        /// <param name="fromDate">������ʼʱ��</param>
        /// <param name="toDate">���۽���ʱ��</param>
        /// <returns></returns>
        public ArrayList QueryPatient(string deptcode, string status, string fromDate, string toDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byDeptCodeAndOutDate", ref where) == -1) return null;

            where = string.Format(where, deptcode, status, fromDate, toDate);

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        /// <summary>
        /// ���������ȥ��Ч�ĹҺ���Ϣ
        /// </summary>
        /// <param name="clinicNO">�����</param>
        /// <returns></returns>
        public ArrayList QueryPatient(string clinicNO)
        {
            string sql = string.Empty;
            string whereSql = string.Empty;

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
            {
                this.Err = "δ���ҵ�����Ϊ[Registration.Register.Query.1]��sql���";
                return null;
            }

            if (this.Sql.GetCommonSql("Registration.Register.Query.WhereByClinic", ref whereSql) == -1)
            {
                this.Err = "δ���ҵ�����Ϊ[Registration.Register.Query.WhereByClinic]��sql���";
                return null;
            }

            try
            {
                whereSql = string.Format(whereSql, clinicNO);
                sql = sql + "  " + whereSql;
            }
            catch (Exception ex)
            {

                this.Err = "���ò�������" + ex.Message;
                return null;
            }

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// ����������ˮ�Ų�ѯ�Һż�¼
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="state">0 ��Ч��1 ��Ч������ ȫ��</param>
        /// <returns></returns>
        public ArrayList QueryPatientByState(string clinicNO, string state)
        {
            string sql = string.Empty;
            string whereSql = string.Empty;

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
            {
                this.Err = "δ���ҵ�����Ϊ[Registration.Register.Query.1]��sql���";
                return null;
            }

            if (this.Sql.GetCommonSql("Registration.Register.Query.WhereByClinicAndState", ref whereSql) == -1)
            {
                this.Err = "δ���ҵ�����Ϊ[Registration.Register.Query.WhereByClinicAndState]��sql���";
                return null;
            }

            try
            {
                whereSql = string.Format(whereSql, clinicNO, state);
                sql = sql + "  " + whereSql;
            }
            catch (Exception ex)
            {

                this.Err = "���ò�������" + ex.Message;
                return null;
            }

            return this.QueryRegister(sql);
        }

        #endregion

        #region ����ְ�ƻ�ȡ������Ŀ

        /// <summary>
        /// ����ҽ��ְ����ȡ��Ӧ��������Ŀ
        /// </summary>
        /// <param name="doctRank"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public string GetDiagItemCodeByDoctRank(string doctRank)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
                return null;

            try
            {
                sql = string.Format(sql, doctRank);

                return this.ExecSqlReturnOne(sql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
        }

        #endregion

        #region �����ж�

        #endregion

        #region ��ѯָ���ϵ�λ�ѿ��ﻼ����Ϣ
        /// <summary>
        /// ��ѯָ���ϵ�λ�ѿ��ﻼ����Ϣ
        /// {4C5542EA-E90E-4831-B430-3D3DBDE12066}
        /// </summary>
        /// <param name="strPactArr"></param>
        /// <param name="dtSeeDateBeg"></param>
        /// <param name="dtSeeDateEnd"></param>
        /// <returns></returns>
        public ArrayList QueryYNSeeRegister(DateTime dtSeeDateBeg, DateTime dtSeeDateEnd)
        {
            string sql = ""; string where = "";

            try
            {
                if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
                if (this.Sql.GetCommonSql("Registration.Register.Query.24", ref where) == -1) return null;

                where = string.Format(where, dtSeeDateBeg.ToString(), dtSeeDateEnd.ToString());

                sql = sql + " " + where;

                return this.QueryRegister(sql);
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                return null;
            }
        }


        #endregion

        #region ˳��ҽ�������洢ҽ�����ص�������ˮ��2010-9-16
        /// <summary>
        /// �����ж��Ƿ�Ϊ30�ֲ���
        /// ˳��ҽ�������洢ҽ�����ص�������ˮ��
        /// {2C4A235D-390F-41d5-92DE-B59E87448BDE}
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="seeDeptID"></param>
        /// <param name="seeDoctID"></param>
        /// <returns></returns>
        public int UpdateDiagnose(FS.HISFC.Models.Registration.Register reg)
        {
            string sql = "";

            string[] parm = new string[] { reg.ID, reg.NormalName };

            if (this.Sql.GetCommonSql("Registration.Register.Update.Diagnose", ref sql) == -1) return -1;

            return this.ExecNoQuery(sql, parm);
        }

        public string QueryDiagnose(FS.HISFC.Models.Registration.Register reg)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.Diagnose", ref sql) == -1)
            {
                return "";
            }

            sql = string.Format(sql, reg.ID);
            return this.ExecSqlReturnOne(sql);
        }
        #endregion

        #region ���Һ���ز�ѯ

        /// <summary>
        /// ����ҽ��ְ����ȡ���ڵĹҺż��������
        /// </summary>
        /// <param name="doctCode">ҽ������</param>
        /// <param name="doctLevl">ҽ��ְ������</param>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="regLevl">�Һż������</param>
        /// <param name="diagItemCode">������Ŀ</param>
        /// <returns></returns>
        public int GetSupplyRegInfo(string doctCode, string doctLevl, string deptCode, ref string regLevl, ref string diagItemCode)
        {
            string sql = "";
            #region �Ȱ����Ű��ȡ�Ű�ĹҺż���������Ŀ

            sql = @"select f.reglevl_code,
                       (select t.item_code from fin_com_regfeeset t
                       where t.reglevl_code=f.reglevl_code
                               and t.dept_code='ALL'
                               and rownum=1) item_code,
                               1 sort
                        from fin_opr_schema f
                        where f.doct_code='{0}'
                        and f.dept_code='{1}'
                        --and f.noon_code='{2}'
                        and f.begin_time<=to_date('{3}','yyyy-mm-dd hh24:mi:ss')
                        and f.end_time>=to_date('{3}','yyyy-mm-dd hh24:mi:ss')

                        union all

                        select f.reglevl_code,
                               (select t.item_code from fin_com_regfeeset t
                               where t.reglevl_code=f.reglevl_code
                               and t.dept_code='{1}'
                               and rownum=1) item_code,
                               2 sort
                        from fin_opr_schema f
                        where f.doct_code='{0}'
                        and f.dept_code='{1}'
                        --and f.noon_code='{2}'
                        and f.begin_time<=to_date('{3}','yyyy-mm-dd hh24:mi:ss')
                        and f.end_time>=to_date('{3}','yyyy-mm-dd hh24:mi:ss')
                        order by sort";

            sql = string.Format(sql, doctCode, deptCode, "", this.GetDateTimeFromSysDateTime().ToString());

            try
            {
                if (this.ExecQuery(sql) == -1)
                {
                    return -1;
                }
                while (this.Reader.Read())
                {
                    regLevl = Reader[0].ToString();
                    diagItemCode = Reader[1].ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }
            #endregion

            #region ���û���Ű�ʱ������ְ����ȡ

            //{A1BAF267-6053-44e3-B96E-36E2C48DE4BD}
            if (string.IsNullOrEmpty(regLevl) || string.IsNullOrEmpty(diagItemCode))
            {
                sql = @"select t.reglevl_code,--�Һż���
                               t.item_code, --������Ŀ
                               1 sort
                        from fin_com_regfeeset t
                        where t.levl_code='{0}'
                        and t.dept_code='{1}'

                        union
                        
                        select t.reglevl_code, --�Һż���
                               t.item_code, --������Ŀ
                               2 sort
                        from fin_com_regfeeset t
                        where t.dept_code = '{1}'
                        and t.reglevl_code = '{2}'

                        union

                        select t.reglevl_code,--�Һż���
                               t.item_code, --������Ŀ
                               3 sort
                        from fin_com_regfeeset t
                        where t.levl_code='{0}'
                        and t.dept_code='ALL'
                       
                        union

                        select t.reglevl_code, --�Һż���
                                t.item_code, --������Ŀ
                                4 sort
                        from fin_com_regfeeset t
                        where t.levl_code = 'ALL'
                        and t.dept_code = 'ALL'
   
                        order by sort --�����������";

                //{61CC27CE-6E87-4412-8CCF-051A3862DDBD}

                sql = string.Format(sql, doctLevl, deptCode, regLevl);
                try
                {
                    if (this.ExecQuery(sql) == -1)
                    {
                        return -1;
                    }
                    while (this.Reader.Read())
                    {
                        regLevl = Reader[0].ToString();
                        diagItemCode = Reader[1].ToString();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    return -1;
                }
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// ���ݹҺż����ȡ������Ŀ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="regLevl">�Һż������</param>
        /// <param name="diagItemCode">������Ŀ</param>
        /// <returns></returns>
        public int GetSupplyRegInfo(string deptCode, string regLevl, ref string diagItemCode)
        {
            string sql = @"select t.item_code, --������Ŀ
                               1 sort
                        from fin_com_regfeeset t
                        where t.reglevl_code='{0}'
                        and t.dept_code='{1}'

                        union

                        select t.item_code, --������Ŀ
                               2 sort
                        from fin_com_regfeeset t
                        where t.reglevl_code='{0}'
                        and t.dept_code='ALL'
   
                        union

                        select --t.reglevl_code, --�Һż���
                        t.item_code, --������Ŀ
                        3 sort
                        from fin_com_regfeeset t
                        where t.levl_code = 'ALL'
                        and t.dept_code = 'ALL'
   
                        order by sort --�����������";

            try
            {
                if (this.ExecQuery(sql, regLevl, deptCode) == -1)
                {
                    Err = this.Sql.Err;
                    return -1;
                }
                while (this.Reader.Read())
                {
                    diagItemCode = Reader[0].ToString();
                    break;
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// ���ݹҺż����ȡ������Ŀ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="doctLevl">ҽ��ְ������</param>
        /// <param name="regLevl">�Һż������</param>
        /// <param name="diagItemCode">������Ŀ</param>
        /// <returns></returns>
        public int GetSupplyRegInfo(string deptCode, string operLevel, string regLevl, ref string diagItemCode)
        {
            string sql = @"select t.item_code, --������Ŀ
                               1 sort
                        from fin_com_regfeeset t
                        where t.reglevl_code='{0}'
                        and t.dept_code='{1}'
                        and t.levl_code='{2}'

                        union

                        select t.item_code, --������Ŀ
                               2 sort
                        from fin_com_regfeeset t
                        where t.reglevl_code='{0}'
                        and t.dept_code='ALL'
                        --and t.levl_code='{2}'
   
                        union

                        select --t.reglevl_code, --�Һż���
                        t.item_code, --������Ŀ
                        3 sort
                        from fin_com_regfeeset t
                        where t.levl_code = 'ALL'
                        and t.dept_code = 'ALL'
   
                        order by sort --�����������";

            try
            {
                if (this.ExecQuery(sql, regLevl, deptCode, operLevel) == -1)
                {
                    Err = this.Sql.Err;
                    return -1;
                }
                while (this.Reader.Read())
                {
                    diagItemCode = Reader[0].ToString();
                    break;
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }
            return 1;
        }

        #endregion

        #region �Ż���ѯ

        /// <summary>
        /// ��ѯ�Һ���Ϣ 
        /// �����ѯ��������ˮ�š�������𡢺�ͬ��λ���������Ա𡢳�������
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QuerySimpleRegInfo(string whereIndex, params string[] args)
        {
            //��ѯ��SQL
            string sql = @"select clinic_code,--������ˮ��
                                   name,--����
                                   sex_code,--�Ա�
                                   birthday,--����
                                   paykind_code,--�������
                                   pact_code,--��ͬ��λ
                                   seeno �������,
                                   card_no ,--������
                                   reg_date ,--�Һ�ʱ��
                                   dept_code, --�Һſ���
                                   doct_code, --�Һ�ҽ��
                                   reglevl_code,    --�Һż���
                                   reglevl_name,
                                   order_no, --ÿ�����
                                   reglevl_code,
                                   reglevl_name,
                                    oper_date,
                                    assign_flag,
                                    assign_status,
                                    first_see_flag,
                                    preferential_flag,
                                    sequence_no
                            from fin_opr_register
                            ";
            if (this.Sql.GetCommonSql(whereIndex, ref whereIndex) == -1)
            {
                this.Err = Sql.Err;
                this.ErrCode = Sql.ErrCode;
                return null;
            }

            try
            {
                sql = sql + "\r\n" + whereIndex;

                sql = string.Format(sql, args);

                if (this.ExecQuery(sql) == -1)
                {
                }

                ArrayList al = new ArrayList();

                FS.HISFC.Models.Registration.Register regObj = null;
                while (this.Reader.Read())
                {
                    regObj = new FS.HISFC.Models.Registration.Register();
                    regObj.ID = this.Reader[0].ToString();//������ˮ��
                    regObj.Name = this.Reader[1].ToString();//����
                    regObj.Sex.ID = this.Reader[2].ToString();//�Ա�
                    regObj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[3]);//����
                    regObj.Pact.PayKind.ID = Reader[4].ToString();//�������
                    regObj.Pact.ID = this.Reader[5].ToString();//��ͬ��λ
                    regObj.DoctorInfo.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString()); //�������
                    regObj.PID.CardNO = this.Reader[7].ToString();
                    regObj.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[8]);
                    regObj.DoctorInfo.Templet.Dept.ID = Reader[9].ToString();
                    regObj.DoctorInfo.Templet.Doct.ID = Reader[10].ToString();
                    regObj.DoctorInfo.Templet.RegLevel.ID = Reader[11].ToString();
                    regObj.DoctorInfo.Templet.RegLevel.Name = Reader[12].ToString();
                    regObj.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[13].ToString());
                    regObj.DoctorInfo.Templet.RegLevel.ID = Reader[14].ToString();
                    regObj.DoctorInfo.Templet.RegLevel.Name = Reader[15].ToString();
                    regObj.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[16]);

                    //{8FE4C905-279D-48c7-9D1B-D0742556A102}
                    regObj.AssignFlag = Reader[17].ToString();
                    regObj.AssignStatus = Reader[18].ToString();
                    regObj.FirstSeeFlag = Reader[19].ToString();
                    regObj.PreferentialFlag = Reader[20].ToString();

                    if (Reader[21] == null || string.IsNullOrEmpty(Reader[21].ToString()))
                    {
                        regObj.SequenceNO = 0;
                    }
                    else
                    {
                        regObj.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[21].ToString());
                    }

                    al.Add(regObj);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;

                return null;
            }
            finally
            {
                if (this.Reader != null && !Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// ���չҺſ��Ҳ�ѯһ��ʱ������Ч�Һ���Ϣ
        /// ֻ��ѯ��Ҫ��Ϣ��������ˮ�š�������𡢺�ͬ��λ���������Ա𡢳�������
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee">0 ��1 �ǣ�ALL ȫ��</param>
        /// <param name="isValid">0 �˷ѣ�1 ��Ч��2 ���ϣ�ALL ȫ��</param>
        /// <returns></returns>
        public ArrayList QuerySimpleRegByDept(string deptID, DateTime beginDate, DateTime endDate, string isSee, string isValid)
        {
            return this.QuerySimpleRegInfo("Registration.Register.QuerySimple.ByDept", deptID, beginDate.ToString(), endDate.ToString(), isSee, isValid);
        }

        #endregion

        /// <summary>
        /// �õ���ǰ����Ա�ӵ�ǰ��ʼ����ǰN�ŷ�Ʊ����Ϣ
        /// </summary>
        /// <param name="count">����</param>
        /// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
        public ArrayList QueryRegistersByCount(string operCode, int count)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
            {
                return null;
            }
            if (this.Sql.GetCommonSql("Registration.Register.Query.ByOperAndCount", ref where) == -1)
            {
                where = @" where ROWNUM <= {1}
                                       and  oper_date > trunc(sysdate)
	                                   and  oper_code = '{0}'
	                                   order by   OPER_DATE DESC";
            }

            try
            {
                where = string.Format(where, operCode, count);
            }
            catch (Exception e)
            {
                this.Err = "[" + where + "]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;
            return this.QueryRegister(sql);
        }
        public FS.HISFC.Models.Base.Department GetDeptmentById(string id)
        {

            string sql = "";
            if (this.Sql.GetCommonSql("Department.SelectDepartmentByID", ref sql) == -1)
                return null;

            try
            {
                sql = string.Format(sql, id);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�ӿڴ���" + ex.Message;
                this.WriteErr();
                return null;
            }

            if (this.ExecQuery(sql) == -1) return null;

            if (this.Reader.Read())
            {
                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                dept.ID = (string)this.Reader[0];
                dept.Name = (string)this.Reader[1];

                if (!(this.Reader.IsDBNull(2)))
                    dept.SpellCode = this.Reader.GetString(2);
                if (!(this.Reader.IsDBNull(3)))
                    dept.WBCode = this.Reader.GetString(3);
                if (!(this.Reader.IsDBNull(4)))
                    dept.EnglishName = this.Reader.GetString(4);

                //dept.DeptType =	 new DepartmentType();
                dept.DeptType.ID = (string)this.Reader[5];

                if (!(this.Reader.IsDBNull(6)))
                {
                    if (this.Reader[6].ToString() == "0")
                        dept.IsRegDept = false;//Convert.ToBoolean(this.Reader[6]);
                    else
                        dept.IsRegDept = true;
                }
                if (!(this.Reader.IsDBNull(7)))
                {
                    //	dept.IsRegDept = Convert.ToBoolean(this.Reader[7]);
                    if (this.Reader[7].ToString() == "0")
                        dept.IsStatDept = false;
                    else
                        dept.IsStatDept = true;
                }

                dept.SpecialFlag = this.Reader[8].ToString();
                dept.ValidState = (FS.HISFC.Models.Base.EnumValidState)Convert.ToInt32(this.Reader[9]);

                if (!(this.Reader.IsDBNull(10)))
                    dept.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
                //�Զ�����
                dept.UserCode = this.Reader[11].ToString();
                dept.ShortName = this.Reader[12].ToString();

                //{335827AE-42F6-4cbd-A73F-1B900B070E74}
                try
                {
                    dept.HospitalID = this.Reader[13].ToString();
                    dept.HospitalName = this.Reader[14].ToString();
                }
                catch
                {
                }

                this.Reader.Close();
                return dept;

            }

            return null;


        }

        //public void GetPhyDataByPhone(string phone,Register r)
        //{
        //    string sql = "";

        //    if (this.Sql.GetCommonSql("HISFC.BizLogic.Registration.Register.GetPhyDataByPhone", ref sql) == -1)
        //    {
        //        return ;
        //    }
        //    sql = string.Format(sql, phone);

        //    try
        //    {
        //        if (this.ExecQuery(sql) == -1)
        //        {
        //            return ;
        //        }
        //        while (this.Reader.Read())
        //        {
        //            regLevl = Reader[0].ToString();
        //            diagItemCode = Reader[1].ToString();
        //            break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Err = ex.Message;
        //        return ;
        //    }
        //    //HISFC.BizLogic.Registration.Register.GetPhyDataByPhone
        //}

    }

    /// <summary>
    /// �ҺŲ���������
    /// </summary>
    public enum EnumUpdateStatus
    {
        /// <summary>
        /// �˺�
        /// </summary>
        Return,
        /// <summary>
        /// ����
        /// </summary>
        ChangeDept,
        /// <summary>
        /// ����
        /// </summary>
        Cancel,
        /// <summary>
        /// ������Ϣ
        /// </summary>
        PatientInfo,
        /// <summary>
        /// ȡ������
        /// </summary>
        Uncancel,
        /// <summary>
        /// �Ϻ�
        /// </summary>
        Bad
    }

    /// <summary>
    /// �ҺŴ�ӡ�ӿ�
    /// </summary>
    public interface IRegPrint
    {
        /// <summary>
        /// ���߹Һ���Ϣ
        /// </summary>
        FS.HISFC.Models.Registration.Register RegInfo
        {
            get;
            set;
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <returns></returns>
        int Print();
    }


}
