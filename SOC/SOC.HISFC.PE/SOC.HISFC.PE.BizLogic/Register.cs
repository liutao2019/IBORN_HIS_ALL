using System;
using System.Collections;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.PE.BizLogic
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

        private FS.HISFC.Models.Registration.Register reg;

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
                return this.CancelReg(register.ID, register.CancelOper.ID, register.CancelOper.OperTime, status);
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
                return this.UpdatePatientInfo(register);
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

            if (this.Sql.GetCommonSql("Registration.Register.CancelReg.forPE", ref sql) == -1) return -1;

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
                this.Err = "���ϹҺż�¼����![Registration.Register.CancelReg.forPE]" + e.Message;
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

            if (this.Sql.GetCommonSql("Registration.Register.ChangeDept.forPE", ref sql) == -1) return -1;

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
                this.Err = "���¹Һż�¼����![Registration.Register.ChangeDept.forPE]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
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
                sql = string.Format(sql, register.Name, register.Birthday.ToString(), register.Sex.ID,
                                        register.AddressHome, register.PhoneHome, register.PID.CardNO, register.CardType.ID,
                                        register.InSource.ID, register.Pact.PayKind.ID, register.Pact.ID, register.Pact.Name,
                                        register.SSN, FS.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt), register.NormalName, register.IDCard);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���»�����Ϣ����![Registration.Register.Update.PatientInfo]" + e.Message;
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

            if (this.Sql.GetCommonSql("Registration.Register.Uncancel.forPE", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���ϹҺż�¼����![Registration.Register.Uncancel.forPE]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
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

            if (this.Sql.GetCommonSql("egistration.Register.UpdateTriage.forPE", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, operID, operDate.ToString());
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "�û��߷����־����![egistration.Register.UpdateTriage.forPE]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// ���¹Һű��еĻ�����Ϣ
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdateRegInfo(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.1.forPE", ref sql) == -1) return -1;

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
                this.Err = "���»�����Ϣ����![Registration.Register.Update.PatientInfo.1.forPE]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

        }
        /// <summary>
        ///  �����Ѿ����������������ˮ��
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public int UpdateSeeDone(string clinicNo)
        {
            string sql = "Registration.Register.Update.SeeDone.forPE";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            return this.ExecNoQuery(sql, clinicNo);
        }
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

            if (this.Sql.GetCommonSql("Registration.Register.Query.17.forPE", ref sql) == -1) return -1;

            return this.ExecNoQuery(sql, parm);
        }
        #endregion

        /// <summary>
        /// ����Һż�¼��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";
            if (register.TranType == FS.HISFC.Models.Base.TransTypes.Positive)
            {
                if (this.Sql.GetCommonSql("Registration.Register.GetInTimes.forPE", ref sql) == -1)
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

            if (this.Sql.GetCommonSql("Registration.Register.Insert.1.forPE", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                sql = string.Format(sql, register.ID, register.PID.CardNO,
                    register.DoctorInfo.SeeDate.ToString(), register.DoctorInfo.Templet.Noon.ID,
                    register.Name, register.IDCard, register.Sex.ID, register.Birthday.ToString(),
                    register.Pact.PayKind.ID, register.Pact.PayKind.Name, register.Pact.ID, register.Pact.Name,
                    register.SSN, register.DoctorInfo.Templet.RegLevel.ID, register.DoctorInfo.Templet.RegLevel.Name,
                    register.DoctorInfo.Templet.Dept.ID, register.DoctorInfo.Templet.Dept.Name,
                    register.DoctorInfo.SeeNO, register.DoctorInfo.Templet.Doct.ID,
                    register.DoctorInfo.Templet.Doct.Name, FS.FrameWork.Function.NConvert.ToInt32(register.IsFee),
                    (int)register.RegType, FS.FrameWork.Function.NConvert.ToInt32(register.IsFirst),
                    register.RegLvlFee.RegFee.ToString(), register.RegLvlFee.ChkFee.ToString(),
                    register.RegLvlFee.OwnDigFee.ToString(), register.RegLvlFee.OthFee.ToString(),
                    register.OwnCost.ToString(), register.PubCost.ToString(), register.PayCost.ToString(),
                    (int)register.Status, register.InputOper.ID, FS.FrameWork.Function.NConvert.ToInt32(register.IsSee),
                    FS.FrameWork.Function.NConvert.ToInt32(register.CheckOperStat.IsCheck), register.PhoneHome,
                    register.AddressHome, (int)register.TranType, register.CardType.ID,
                    register.DoctorInfo.Templet.Begin.ToString(), register.DoctorInfo.Templet.End.ToString(),
                    register.CancelOper.ID, register.CancelOper.OperTime.ToString(),
                    register.InvoiceNO, register.RecipeNO, FS.FrameWork.Function.NConvert.ToInt32(register.DoctorInfo.Templet.IsAppend),
                    register.OrderNO, register.DoctorInfo.Templet.ID,
                    register.InputOper.OperTime.ToString(), register.InSource.ID, FS.FrameWork.Function.NConvert.ToInt32(register.CaseState),
                    FS.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt), register.NormalName, register.EcoCost, NConvert.ToInt32(register.IsAccount).ToString(),
                    /*{156C449B-60A9-4536-B4FB-D00BC6F476A1}*/FS.FrameWork.Function.NConvert.ToInt32(register.DoctorInfo.Templet.RegLevel.IsEmergency), register.Mark1, register.Card.ID, register.Card.CardType.ID, register.InTimes.ToString());

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "�������Һ������������![Registration.Register.Insert.1.forPE]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        #region ��ѯ�Һ���Ϣ
        /// <summary>
        /// ��ѯ����һ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryValidPatientsByCardNO(string cardNo, DateTime limitDate)
        {
            return this.QueryRegListBase("Registration.Register.Query.3", cardNo, limitDate.ToString());
        }
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
        /// <summary>
        /// ��������š���ʼʱ���ѯ�Һ���Ϣ
        /// </summary>
        /// <param name="seeNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryValidPatientsBySeeNO(string seeNo, DateTime limitDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1.forPE", ref sql) == -1) return null;
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
        private ArrayList QueryRegListBase(string whereSQL, params string[] args)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1.forPE", ref sql) == -1)
            {
                return null;
            }
            if (this.Sql.GetCommonSql(whereSQL, ref where) == -1)
            {
                return null;
            }

            try
            {
                where = string.Format(where, args);
            }
            catch (Exception e)
            {
                this.Err = "[" + whereSQL + "]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
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
                    al.Add(this.reg);
                }
            }
            catch (Exception e)
            {
                this.Err = "�����Һ���Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }

        #endregion

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
