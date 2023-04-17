using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Neusoft.SOC.Local.HISWebService.PE
{
    /// <summary>
    /// Account ��ժҪ˵����
    /// </summary>
    public class PEChargeService_Db : Neusoft.FrameWork.Management.Database
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PEChargeService_Db()
        { }

        //public IBM.Data.DB2.DB2Command command;

        #region ˽�з���
        /// <summary>
        /// ��ȡ����Ϣ
        /// </summary>
        /// <param name="Sql">sql���</param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.Account.AccountCard GetAccountCardInfo(string Sql)
        {
            Neusoft.HISFC.Models.Account.AccountCard accountCard = null;
            //try
            //{
            //    if (this.ExecQuery(Sql) == -1) return null;
            //    while (this.Reader.Read())
            //    {
            //        accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
            //        accountCard.CardNO = Reader[0].ToString();
            //        accountCard.MarkNO = Reader[1].ToString();
            //        accountCard.MarkType.ID = Reader[2].ToString();
            //        accountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Err = ex.Message;
            //    this.ErrCode = ex.Message;
            //    return null;
            //}
            return accountCard;
        }

        #endregion


        #region ����շѽӿ�
        /// <summary>
        /// ���ڲ�ѯ
        /// </summary>
        /// <returns></returns>
        public int _dsExcuteQuery(string strSQL,ref DataSet objDs)
        {
            objDs=new DataSet();
            return this.ExecQuery(strSQL, ref objDs);
        }
        /// <summary>
        /// ����ִ��sql
        /// </summary>
        /// <returns></returns>
        public int _ExcuteSQL(string strSQL)
        {       
            return this.ExecNoQuery(strSQL);
        }
        #region ����Ǽ���Ϣ
        /// <summary>
		/// ����Һż�¼��
		/// </summary>
		/// <param name="register"></param>
		/// <returns></returns>
		public int InsertPERegister(Neusoft.HISFC.Models.Registration.Register register)
		{
			string sql="";

			if(this.Sql.GetSql("Registration.Register.Insert.PeCharge",ref sql)==-1)return -1;
			
			try
			{
				sql = string.Format(sql,register.ID,    register.PID.CardNO,
                    register.DoctorInfo.SeeDate.ToString(),     register.DoctorInfo.Templet.Noon.ID,
					register.Name,  register.IDCard,  register.Sex.ID,  register.Birthday.ToString(),
					register.Pact.PayKind.ID,register.Pact.PayKind.Name,register.Pact.ID,register.Pact.Name,
					register.SSN,  register.DoctorInfo.Templet.RegLevel.ID,     register.DoctorInfo.Templet.RegLevel.Name,
                    register.DoctorInfo.Templet.Dept.ID,    register.DoctorInfo.Templet.Dept.Name,
                    register.DoctorInfo.SeeNO,  register.DoctorInfo.Templet.Doct.ID,
                    register.DoctorInfo.Templet.Doct.Name,	Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsFee),
                    (int)register.RegType,      Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsFirst),
					register.RegLvlFee.RegFee.ToString(),   register.RegLvlFee.ChkFee.ToString(),
                    register.RegLvlFee.OwnDigFee.ToString(),    register.RegLvlFee.OthFee.ToString(),
                    register.OwnCost.ToString(),    register.PubCost.ToString(),    register.PayCost.ToString(),
                    (int)register.Status,		register.InputOper.ID,  Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsSee),
					Neusoft.FrameWork.Function.NConvert.ToInt32(register.CheckOperStat.IsCheck),  register.PhoneHome,
					register.AddressHome,   (int)register.TranType,     register.CardType.ID,
                    register.DoctorInfo.Templet.Begin.ToString(),   register.DoctorInfo.Templet.End.ToString(),
					register.CancelOper.ID,     register.CancelOper.OperTime.ToString(),
                    register.InvoiceNO, register.RecipeNO,		Neusoft.FrameWork.Function.NConvert.ToInt32(register.DoctorInfo.Templet.IsAppend),
                    register.OrderNO,   register.DoctorInfo.Templet.ID,
					register.InputOper.OperTime.ToString(),     register.InSource.ID ,Neusoft.FrameWork.Function.NConvert.ToInt32(register.CaseState),
                    Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt),register.NormalName) ;

				return this.ExecNoQuery(sql);				
			}
			catch(Exception e)
			{
				this.Err="����Һ������������![Registration.Register.Insert.PeCharge]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}			
		}
        #endregion


        #endregion

        #region �������ݲ���

        #region ��ѯ������Ϣ

        /// <summary>
        /// ��ѯ������Ϣ(���ݾ��￨��)
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Account.PatientAccount GetPatientInfo(string cardNO, string temp)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.SelectPatient", ref Sql) == -1)
                return null;
            if (this.Sql.GetSql("Fee.Account.SelectPatientWhere10", ref SqlWhere) == -1)
                return null;
            Neusoft.HISFC.Models.Account.PatientAccount patientInfo = null;
            try
            {
                SqlWhere = string.Format(SqlWhere, cardNO);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1)
                    return null;
                #region SQL
                /*
                 SELECT a.card_no,   --���￨��
                   a.ic_cardno,   --���Ժ�
                   a.name,   --����
                   a.spell_code,   --ƴ����
                   a.wb_code,   --���
                   a.birthday,   --��������
                   a.sex_code,   --�Ա�
                   a.idenno,   --���֤��
                   a.blood_code,   --Ѫ��
                   a.prof_code,   --ְҵ
                   a.work_home,   --������λ
                   a.work_tel,   --��λ�绰
                   a.work_zip,   --��λ�ʱ�
                   a.home,   --���ڻ��ͥ����
                   a.home_tel,   --��ͥ�绰
                   a.home_zip,   --���ڻ��ͥ��������
                   a.district,   --����
                   a.nation_code,   --����
                   a.linkman_name,   --��ϵ������
                   a.linkman_tel,   --��ϵ�˵绰
                   a.linkman_add,   --��ϵ��סַ
                   a.rela_code,   --��ϵ�˹�ϵ
                   a.mari,   --����״��
                   a.coun_code,   --����
                   a.paykind_code,   --�������
                   a.paykind_name,   --�����������
                   a.pact_code,   --��ͬ����
                   a.pact_name,   --��ͬ��λ����
                   a.mcard_no,   --ҽ��֤��
                   a.area_code,   --������
                   a.framt,   --ҽ�Ʒ���
                   a.anaphy_flag,   --ҩ�����
                   a.hepatitis_flag,   --��Ҫ����
                   a.act_code,   --�ʻ�����
                   a.act_amt,   --�ʻ��ܶ�
                   a.lact_sum,   --�����ʻ����
                   a.lbank_sum,   --�����������
                   a.arrear_times,   --Ƿ�Ѵ���
                   a.arrear_sum,   --Ƿ�ѽ��
                   a.inhos_source,   --סԺ��Դ
                   a.lihos_date,   --���סԺ����
                   a.inhos_times,   --סԺ����
                   a.louthos_date,   --�����Ժ����
                   a.fir_see_date,   --��������
                   a.lreg_date,   --����Һ�����
                   a.disoby_cnt,   --ΥԼ����
                   a.end_date,   --��������
                   a.mark,   --��ע
                   a.oper_code,   --����Ա
                   a.oper_date,   --��������
                   a.is_valid,   --�Ƿ���Ч0��Ч1��Ч2����
                   a.fee_kind,   --�㷨���  0 ȫ��
                   a.old_cardno,    --�ɿ���,���������л���
              FROM com_patientinfo a,   --���˻�����Ϣ��
              where and a.card_no='{0}'
             
                 */
                #endregion
                while (this.Reader.Read())
                {
                    patientInfo = new Neusoft.HISFC.Models.Account.PatientAccount();
                    #region ������Ϣ
                    if (Reader[0] != DBNull.Value)
                        patientInfo.PID.CardNO = Reader[0].ToString();//��������
                    if (Reader[1] != DBNull.Value)
                        patientInfo.Card.ICCard.ID = Reader[1].ToString();
                    if (Reader[2] != DBNull.Value)
                        patientInfo.Name = Reader[2].ToString(); //����
                    if (Reader[4] != DBNull.Value)
                        patientInfo.WBCode = Reader[4].ToString(); //��ʱ��
                    if (Reader[6] != DBNull.Value)
                        patientInfo.Sex.ID = Reader[6].ToString();//�Ա�
                    if (Reader[0] != DBNull.Value)
                        patientInfo.PID.CardNO = Reader[0].ToString();//���￨��
                    if (Reader[26] != DBNull.Value)
                        patientInfo.Pact.ID = Reader[26].ToString();//�������code
                    if (Reader[27] != DBNull.Value)
                        patientInfo.Pact.Name = Reader[27].ToString();//�����������
                    if (Reader[29] != DBNull.Value)
                        patientInfo.AreaCode = Reader[29].ToString();//������
                    if (Reader[23] != DBNull.Value)
                        patientInfo.Country.ID = Reader[23].ToString();//����
                    if (Reader[17] != DBNull.Value)
                        patientInfo.Nationality.ID = Reader[17].ToString();//����
                    patientInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);//��������
                    if (Reader[16] != DBNull.Value)
                        patientInfo.DIST = Reader[16].ToString();//����
                    if (Reader[9] != DBNull.Value)
                        patientInfo.Profession.ID = Reader[9].ToString();//ְҵ
                    if (Reader[7] != DBNull.Value)
                        patientInfo.IDCard = Reader[7].ToString();//���֤��
                    if (Reader[10] != DBNull.Value)
                        patientInfo.CompanyName = Reader[10].ToString();//������λ
                    if (Reader[11] != DBNull.Value)
                        patientInfo.PhoneBusiness = Reader[11].ToString();//��λ�绰
                    if (Reader[22] != DBNull.Value)
                        patientInfo.MaritalStatus.ID = Reader[22].ToString();//����״��
                    if (Reader[13] != DBNull.Value)
                        patientInfo.AddressHome = Reader[13].ToString();//��ͥ��ַ
                    if (Reader[14] != DBNull.Value)
                        patientInfo.PhoneHome = Reader[14].ToString();//��ͥ�绰
                    if (Reader[18] != DBNull.Value)
                        patientInfo.Kin.Name = Reader[18].ToString();//��ϵ������
                    if (Reader[19] != DBNull.Value)
                        patientInfo.Kin.RelationPhone = Reader[19].ToString();//��ϵ�˵绰
                    if (Reader[20] != DBNull.Value)
                        patientInfo.Kin.RelationAddress = Reader[20].ToString();//��ϵ�˵�ַ
                    if (Reader[21] != DBNull.Value)
                        patientInfo.Kin.Relation.ID = Reader[21].ToString();//��ϵ�˹�ϵ
                    patientInfo.IsEncrypt = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[53]);
                    if (Reader[54] != DBNull.Value)
                        patientInfo.NormalName = Reader[54].ToString();
                    if (patientInfo.IsEncrypt)
                    {
                        //patientInfo.DecryptName = Neusoft.FrameWork.Interface.Classes.Function.Decrypt3DES(patientInfo.NormalName);
                    }
                    else
                    {
                        patientInfo.DecryptName = patientInfo.Name;
                    }
                    if (Reader[55] != DBNull.Value)
                        patientInfo.IDCardType.ID = Reader[55].ToString();
                    if (Reader[28] != DBNull.Value)
                        patientInfo.SSN = Reader[28].ToString();
                    if (Reader[15] != DBNull.Value)
                        patientInfo.HomeZip = Reader[15].ToString();
                    #endregion
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return patientInfo;
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Account.PatientAccount GetPatientInfo(string cardNO)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.SelectPatient", ref Sql) == -1) return null;
            if (this.Sql.GetSql("Fee.Account.SelectPatientWhere1", ref SqlWhere) == -1) return null;
            Neusoft.HISFC.Models.Account.PatientAccount patientInfo = null;
            try
            {
                SqlWhere = string.Format(SqlWhere, cardNO);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1) return null;
                #region SQL
                /*
                 SELECT a.card_no,   --���￨��
                   a.ic_cardno,   --���Ժ�
                   a.name,   --����
                   a.spell_code,   --ƴ����
                   a.wb_code,   --���
                   a.birthday,   --��������
                   a.sex_code,   --�Ա�
                   a.idenno,   --���֤��
                   a.blood_code,   --Ѫ��
                   a.prof_code,   --ְҵ
                   a.work_home,   --������λ
                   a.work_tel,   --��λ�绰
                   a.work_zip,   --��λ�ʱ�
                   a.home,   --���ڻ��ͥ����
                   a.home_tel,   --��ͥ�绰
                   a.home_zip,   --���ڻ��ͥ��������
                   a.district,   --����
                   a.nation_code,   --����
                   a.linkman_name,   --��ϵ������
                   a.linkman_tel,   --��ϵ�˵绰
                   a.linkman_add,   --��ϵ��סַ
                   a.rela_code,   --��ϵ�˹�ϵ
                   a.mari,   --����״��
                   a.coun_code,   --����
                   a.paykind_code,   --�������
                   a.paykind_name,   --�����������
                   a.pact_code,   --��ͬ����
                   a.pact_name,   --��ͬ��λ����
                   a.mcard_no,   --ҽ��֤��
                   a.area_code,   --������
                   a.framt,   --ҽ�Ʒ���
                   a.anaphy_flag,   --ҩ�����
                   a.hepatitis_flag,   --��Ҫ����
                   a.act_code,   --�ʻ�����
                   a.act_amt,   --�ʻ��ܶ�
                   a.lact_sum,   --�����ʻ����
                   a.lbank_sum,   --�����������
                   a.arrear_times,   --Ƿ�Ѵ���
                   a.arrear_sum,   --Ƿ�ѽ��
                   a.inhos_source,   --סԺ��Դ
                   a.lihos_date,   --���סԺ����
                   a.inhos_times,   --סԺ����
                   a.louthos_date,   --�����Ժ����
                   a.fir_see_date,   --��������
                   a.lreg_date,   --����Һ�����
                   a.disoby_cnt,   --ΥԼ����
                   a.end_date,   --��������
                   a.mark,   --��ע
                   a.oper_code,   --����Ա
                   a.oper_date,   --��������
                   a.is_valid,   --�Ƿ���Ч0��Ч1��Ч2����
                   a.fee_kind,   --�㷨���  0 ȫ��
                   a.old_cardno,    --�ɿ���,���������л���
              FROM com_patientinfo a,   --���˻�����Ϣ��
              where and a.card_no='{0}'
             
                 */
                #endregion
                while (this.Reader.Read())
                {
                    patientInfo = new Neusoft.HISFC.Models.Account.PatientAccount();
                    #region ������Ϣ
                    if (Reader[0] != DBNull.Value) patientInfo.PID.CardNO = Reader[0].ToString();//��������
                    if (Reader[1] != DBNull.Value)
                        patientInfo.Card.ICCard.ID = Reader[1].ToString();
                    if (Reader[2] != DBNull.Value) patientInfo.Name = Reader[2].ToString(); //����
                if (Reader[4] != DBNull.Value)
                    patientInfo.WBCode = Reader[4].ToString(); //��ʱ��
                    if (Reader[6] != DBNull.Value) patientInfo.Sex.ID = Reader[6].ToString();//�Ա�
                    if (Reader[0] != DBNull.Value) patientInfo.PID.CardNO = Reader[0].ToString();//���￨��
                    if (Reader[26] != DBNull.Value) patientInfo.Pact.ID = Reader[26].ToString();//�������code
                    if (Reader[27] != DBNull.Value) patientInfo.Pact.Name = Reader[27].ToString();//�����������
                    if (Reader[29] != DBNull.Value) patientInfo.AreaCode = Reader[29].ToString();//������
                    if (Reader[23] != DBNull.Value) patientInfo.Country.ID = Reader[23].ToString();//����
                    if (Reader[17] != DBNull.Value) patientInfo.Nationality.ID = Reader[17].ToString();//����
                    patientInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);//��������
                    if (Reader[16] != DBNull.Value) patientInfo.DIST = Reader[16].ToString();//����
                    if (Reader[9] != DBNull.Value) patientInfo.Profession.ID = Reader[9].ToString();//ְҵ
                    if (Reader[7] != DBNull.Value) patientInfo.IDCard = Reader[7].ToString();//���֤��
                    if (Reader[10] != DBNull.Value) patientInfo.CompanyName = Reader[10].ToString();//������λ
                    if (Reader[11] != DBNull.Value) patientInfo.PhoneBusiness = Reader[11].ToString();//��λ�绰
                    if (Reader[22] != DBNull.Value) patientInfo.MaritalStatus.ID = Reader[22].ToString();//����״��
                    if (Reader[13] != DBNull.Value) patientInfo.AddressHome = Reader[13].ToString();//��ͥ��ַ
                    if (Reader[14] != DBNull.Value) patientInfo.PhoneHome = Reader[14].ToString();//��ͥ�绰
                    if (Reader[18] != DBNull.Value) patientInfo.Kin.Name = Reader[18].ToString();//��ϵ������
                    if (Reader[19] != DBNull.Value) patientInfo.Kin.RelationPhone = Reader[19].ToString();//��ϵ�˵绰
                    if (Reader[20] != DBNull.Value) patientInfo.Kin.RelationAddress = Reader[20].ToString();//��ϵ�˵�ַ
                    if (Reader[21] != DBNull.Value) patientInfo.Kin.Relation.ID = Reader[21].ToString();//��ϵ�˹�ϵ
                    patientInfo.IsEncrypt = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[53]);
                    if (Reader[54] != DBNull.Value) patientInfo.NormalName = Reader[54].ToString();
                    if (patientInfo.IsEncrypt)
                    {
                        //patientInfo.DecryptName = Neusoft.Models.Interface.Classes.Function.Decrypt3DES(patientInfo.NormalName);
                    }
                    else
                    {
                        patientInfo.DecryptName = patientInfo.Name;
                    }
                    if (Reader[55] != DBNull.Value) patientInfo.IDCardType.ID=Reader[55].ToString();
                    if (Reader[28] != DBNull.Value)
                        patientInfo.SSN = Reader[28].ToString();
                    if (Reader[15] != DBNull.Value)
                        patientInfo.HomeZip = Reader[15].ToString();
                    #endregion
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return patientInfo;
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="idenNO">���֤��</param>
        /// <returns></returns>
        public List<Neusoft.HISFC.Models.Account.PatientAccount> GetPatientInfo(string name, string idenNO, string birthday, string homestr)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.SelectPatient", ref Sql) == -1) return null;
            if (this.Sql.GetSql("Fee.Account.SelectPatientWhere2", ref SqlWhere) == -1) return null;
            Neusoft.HISFC.Models.Account.PatientAccount patientInfo = null;
            List<Neusoft.HISFC.Models.Account.PatientAccount> list = new List<Neusoft.HISFC.Models.Account.PatientAccount>();
            try
            {
                SqlWhere = string.Format(SqlWhere, name,birthday, idenNO,homestr);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1) return null;
                while (this.Reader.Read())
                {
                    patientInfo = new Neusoft.HISFC.Models.Account.PatientAccount();
                    #region ������Ϣ
                    if (Reader[0] != DBNull.Value) patientInfo.PID.CardNO = Reader[0].ToString();//��������
                    if (Reader[1] != DBNull.Value)
                        patientInfo.Card.ICCard.ID = Reader[1].ToString();
                    if (Reader[2] != DBNull.Value) patientInfo.Name = Reader[2].ToString(); //����
                if (Reader[4] != DBNull.Value)
                    patientInfo.WBCode = Reader[4].ToString(); //��ʱ��
                    if (Reader[6] != DBNull.Value) patientInfo.Sex.ID = Reader[6].ToString();//�Ա�
                    if (Reader[0] != DBNull.Value) patientInfo.PID.CardNO = Reader[0].ToString();//���￨��
                    if (Reader[26] != DBNull.Value) patientInfo.Pact.ID = Reader[26].ToString();//�������code
                    if (Reader[27] != DBNull.Value) patientInfo.Pact.Name = Reader[27].ToString();//�����������
                    if (Reader[29] != DBNull.Value) patientInfo.AreaCode = Reader[29].ToString();//������
                    if (Reader[23] != DBNull.Value) patientInfo.Country.ID = Reader[23].ToString();//����
                    if (Reader[17] != DBNull.Value) patientInfo.Nationality.ID = Reader[17].ToString();//����
                    patientInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);//��������
                    if (Reader[16] != DBNull.Value) patientInfo.DIST = Reader[16].ToString();//����
                    if (Reader[9] != DBNull.Value) patientInfo.Profession.ID = Reader[9].ToString();//ְҵ
                    if (Reader[7] != DBNull.Value) patientInfo.IDCard = Reader[7].ToString();//���֤��
                    if (Reader[10] != DBNull.Value) patientInfo.CompanyName = Reader[10].ToString();//������λ
                    if (Reader[11] != DBNull.Value) patientInfo.PhoneBusiness = Reader[11].ToString();//��λ�绰
                    if (Reader[22] != DBNull.Value) patientInfo.MaritalStatus.ID = Reader[22].ToString();//����״��
                    if (Reader[13] != DBNull.Value) patientInfo.AddressHome = Reader[13].ToString();//��ͥ��ַ
                    if (Reader[14] != DBNull.Value) patientInfo.PhoneHome = Reader[14].ToString();//��ͥ�绰
                    if (Reader[18] != DBNull.Value) patientInfo.Kin.Name = Reader[18].ToString();//��ϵ������
                    if (Reader[19] != DBNull.Value) patientInfo.Kin.RelationPhone = Reader[19].ToString();//��ϵ�˵绰
                    if (Reader[20] != DBNull.Value) patientInfo.Kin.RelationAddress = Reader[20].ToString();//��ϵ�˵�ַ
                    if (Reader[21] != DBNull.Value) patientInfo.Kin.Relation.ID = Reader[21].ToString();//��ϵ�˹�ϵ
                    patientInfo.IsEncrypt = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[53]);//�Ƿ��������
                    patientInfo.NormalName = Reader[54].ToString();//����
                    if (patientInfo.IsEncrypt)
                    {
                        //patientInfo.DecryptName = Neusoft.FrameWork.Interface.Classes.Function.Decrypt3DES(patientInfo.NormalName);
                    }
                    else
                    {
                        patientInfo.DecryptName = patientInfo.Name;
                    }
                    if (this.Reader[55] != DBNull.Value) patientInfo.IDCardType.ID = this.Reader[55].ToString();
                    if (Reader[28] != DBNull.Value)
                        patientInfo.SSN = Reader[28].ToString();
                    if (Reader[15] != DBNull.Value)
                        patientInfo.HomeZip = Reader[15].ToString();
                    #endregion
                    list.Add(patientInfo);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return list;
        }
        #endregion

        #region ���»�����Ϣ
        /// <summary>
        /// ���»�����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        public int UpdatePatient(Neusoft.HISFC.Models.Account.PatientAccount patientInfo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.UpdatePatient", ref Sql) == -1) return -1;
            try
            {
                #region Sql
                /*UPDATE com_patientinfo   --���˻�����Ϣ��
                       SET name='{1}',   --����
                           birthday={2},   --��������
                           sex_code='{3}',   --�Ա�
                           idenno='{4}',   --���֤��
                           prof_code='{5}',   --ְҵ
                           work_home='{6}',   --������λ
                           work_tel='{7}',   --��λ�绰
                           home='{8}',   --���ڻ��ͥ����
                           home_tel='{9}',   --��ͥ�绰
                           district='{10}',   --����
                           nation_code='{11}',   --����
                           linkman_name='{12}',   --��ϵ������
                           linkman_tel='{13}',   --��ϵ�˵绰
                           linkman_add='{14}',   --��ϵ��סַ
                           rela_code='{15}',   --��ϵ�˹�ϵ
                           mari='{16}',   --����״��
                           coun_code='{17}',   --����
                           pact_code='{18}',   --��ͬ����
                           pact_name='{19}',   --��ͬ��λ����
                           area_code='{20}'    --������
                     WHERE card_no='{0}'
                  */
                #endregion

                #region ��ʽ��SQL
                Sql = string.Format(Sql,
                                   patientInfo.PID.CardNO, //���￨��
                                   patientInfo.Name,//����
                                   //patientInfo.Birthday.ToShortDateString().ToString(),//��������
                                   patientInfo.Birthday.ToString(),//��������
                                   patientInfo.Sex.ID,//�Ա�
                                   patientInfo.IDCard,//���֤��
                                   patientInfo.Profession.ID,//ְҵ
                                   patientInfo.CompanyName,//������λ
                                   patientInfo.PhoneBusiness,//��λ�绰
                                   patientInfo.AddressHome,//���ڻ��ͥ����
                                   patientInfo.PhoneHome,//��ͥ�绰
                                   patientInfo.DIST,//����
                                   patientInfo.Nationality.ID,//����
                                   patientInfo.Kin.Name,//��ϵ������
                                   patientInfo.Kin.RelationPhone,//��ϵ�˵绰
                                   patientInfo.Kin.RelationAddress,//��ϵ��סַ
                                   patientInfo.Kin.Relation.ID,//��ϵ�˹�ϵ
                                   patientInfo.MaritalStatus.ID,//����״��
                                   patientInfo.Country.ID,//����
                                   patientInfo.Pact.ID,//��ͬ����
                                   patientInfo.Pact.Name,//��ͬ��λ����
                                   patientInfo.AreaCode,//������
                                   patientInfo.Oper.ID,//����Ա
                                   patientInfo.Oper.OperTime, //����ʱ��
                                   Neusoft.FrameWork.Function.NConvert.ToInt32(patientInfo.IsEncrypt),//�Ƿ��������
                                   patientInfo.NormalName, //����
                                   patientInfo.IDCardType.ID //֤������
                                   );
                #endregion
                
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }
        /// <summary>
        /// ���»������סԺ��Ϣ��д�������¼
        /// </summary>
        /// <param name="patientInfo"></param>
        public int UpdatePatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientinfo, Neusoft.HISFC.Models.RADT.PatientInfo patientinfo_old)
        {
            //change_type�����޸����ͣ�����޸������֣����޸�����ΪN������޸����Ա����޸�����ΪS~~~
            string change_type=string .Empty;
             if(patientinfo_old.Name!=patientinfo.Name)
                change_type+="N";
              if(patientinfo_old.Sex .ID!=patientinfo.Sex.ID)
                change_type+="S";
              if(patientinfo_old.Birthday!=patientinfo.Birthday)
                change_type+="B";

              Neusoft.HISFC.BizLogic.Manager.EmployeeRecord emp = new Neusoft.HISFC.BizLogic.Manager.EmployeeRecord();
             string empl_code = emp.Operator.ID;

            string Sql_ZSY = string.Empty;
            if (this.Sql.GetSql("Fee.Account.UpdateZSY", ref Sql_ZSY) == -1) return -1;

            string Sql_InmainInfo = string.Empty;
            if (this.Sql.GetSql("Fee.Account.UpdateInmainInfo", ref Sql_InmainInfo) == -1) return -1;
            
            string Sql_InsertChangeRecord = string.Empty;
            if (this.Sql.GetSql("Fee.Account.InsertModifyInfo", ref Sql_InsertChangeRecord) == -1) return -1;
           

            try
            {
                Sql_ZSY = string.Format(Sql_ZSY, patientinfo.Name, patientinfo.Sex.ID.ToString(), patientinfo.Birthday, patientinfo.PID.CardNO);
                Sql_InmainInfo = string.Format(Sql_InmainInfo, patientinfo.Name, patientinfo.Sex.ID.ToString(), patientinfo.Birthday, patientinfo.PID.CardNO);
                Sql_InsertChangeRecord = string.Format(Sql_InsertChangeRecord, patientinfo.Card.ICCard.ID, patientinfo.PID.CardNO,change_type, empl_code);
                int reuslt = 1;
                if (this.ExecNoQuery(Sql_ZSY) ==-1)
                    reuslt=-1;
                if (this.ExecNoQuery(Sql_InmainInfo) ==-1)
                    reuslt = -1;
                if (this.ExecNoQuery(Sql_InsertChangeRecord) == -1)
                    reuslt = -1;
                return reuslt;

            }
            catch(Exception e)
            {
                return -1;
            }
        }
        #endregion

        #region ���뻼����Ϣ
        /// <summary>
        /// ���뻼����Ϣ
        /// </summary>
        /// <param name="patientInfo">����ʵ��</param>
        /// <returns></returns>
        public int InsertPatient(Neusoft.HISFC.Models.Account.PatientAccount patientInfo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.InsertPatient", ref Sql) == -1) return -1;
            try
            {
                #region ��ʽ��SQL
                Sql = string.Format(Sql,
                                   patientInfo.PID.CardNO, //���￨��
                                   patientInfo.Name,//����
                                   patientInfo.Birthday.ToShortDateString().ToString(),//��������
                                   patientInfo.Sex.ID,//�Ա�
                                   patientInfo.IDCard,//���֤��
                                   patientInfo.Profession,//ְҵ
                                   patientInfo.CompanyName,//������λ
                                   patientInfo.PhoneBusiness,//��λ�绰
                                   patientInfo.AddressHome,//���ڻ��ͥ����
                                   patientInfo.PhoneHome,//��ͥ�绰
                                   patientInfo.DIST,//����
                                   patientInfo.Nationality.ID,//����
                                   patientInfo.Kin.Name,//��ϵ������
                                   patientInfo.Kin.RelationPhone,//��ϵ�˵绰
                                   patientInfo.Kin.RelationAddress,//��ϵ��סַ
                                   patientInfo.Kin.Relation.ID,//��ϵ�˹�ϵ
                                   patientInfo.MaritalStatus.ID,//����״��
                                   patientInfo.Country.ID,//����
                                   patientInfo.Pact.ID,//��ͬ����
                                   patientInfo.Pact.Name,//��ͬ��λ����
                                   patientInfo.AreaCode,//������
                                   patientInfo.Oper.ID,//����Ա
                                   patientInfo.Oper.OperTime, //����ʱ��
                                   Neusoft.FrameWork.Function.NConvert.ToInt32(patientInfo.IsEncrypt),//�Ƿ��������
                                   patientInfo.NormalName, //����
                                   patientInfo.IDCardType.ID
                                   );
                #endregion

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }
        #endregion

        #endregion

        #region �ʻ�������

//        #region ����������ȡ����Ϣ
//        /// <summary>
//        /// ����������ȡ����Ϣ
//        /// </summary>
//        /// <param name="markNO">������</param>
//        /// <returns></returns>
//        public Neusoft.HISFC.Models.Account.AccountCard GetAccountCard(string markNO, string markType)
//        {
//            Neusoft.HISFC.Models.Account.AccountCard accountCard = null;
//            try
//            {
//                string Sql = string.Empty;
//                string SqlWhere = string.Empty;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCard", ref Sql) == -1) return null;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCardWhere3", ref SqlWhere) == -1) return null;
//                SqlWhere = string.Format(SqlWhere, markNO, markType);
//                Sql += " " + SqlWhere;
//                if (this.ExecQuery(Sql) == -1) return null;
//                while (this.Reader.Read())
//                {
//                    accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
//                    accountCard.CardNO = Reader[0].ToString();
//                    accountCard.MarkNO = Reader[1].ToString();
//                    accountCard.MarkType.ID = Reader[2].ToString();
//                    accountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
//                }
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return null;
//            }
//            return accountCard;
//        }
//        #endregion

//        #region ���뿨������¼
//        /// <summary>
//        /// ���뿨������¼
//        /// </summary>
//        /// <param name="accountCardRecord">��������¼ʵ��</param>
//        /// <returns></returns>
//        public int InsertAccountCardRecord(Neusoft.HISFC.Models.Account.AccountCardRecord accountCardRecord)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.InsetAccountCardRecord", ref Sql) == -1) return -1;
//            try
//            {
//                Sql = string.Format(Sql,
//                                accountCardRecord.MarkNO,
//                                accountCardRecord.MarkType.ID.ToString(),
//                                accountCardRecord.CardNO,
//                                accountCardRecord.OperateTypes.ID.ToString(),
//                                accountCardRecord.Oper.ID.ToString(),
//                                accountCardRecord.CardMoney);
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                return -1;
//            }
//            return this.ExecNoQuery(Sql);
//        }

//        #endregion

//        #region ���ݻ������￨�Ų��ҿ���Ϣ
//        /// <summary>
//        /// ͨ�����Ų��ҿ���Ϣ
//        /// </summary>
//        /// <param name="cardNO"></param>
//        /// <returns></returns>
//        public Neusoft.HISFC.Models.Account.AccountCard GetMarkByCardNo(string cardNO, string markType)
//        {
//            Neusoft.HISFC.Models.Account.AccountCard accountCard = null;
//            try
//            {
//                string Sql = string.Empty;
//                string SqlWhere = string.Empty;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCard", ref Sql) == -1) return null;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCardWhere1", ref SqlWhere) == -1) return null;
//                SqlWhere = string.Format(SqlWhere, cardNO, markType);
//                Sql += " " + SqlWhere;
//                if (this.ExecQuery(Sql) == -1) return null;
//                while (this.Reader.Read())
//                {
//                    accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
//                    accountCard.CardNO = Reader[0].ToString();
//                    accountCard.MarkNO = Reader[1].ToString();
//                    accountCard.MarkType.ID = Reader[2].ToString();
//                    accountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
//                }
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return null;
//            }
//            return accountCard;
//        }

//        /// <summary>
//        /// ͨ�����Ų��ҿ���Ϣ
//        /// </summary>
//        /// <param name="cardNO">���￨��</param>
//        /// <returns></returns>
//        public List<Neusoft.HISFC.Models.Account.AccountCard> GetMarkList(string cardNO)
//        {

//            List<Neusoft.HISFC.Models.Account.AccountCard> list = new List<Neusoft.HISFC.Models.Account.AccountCard>();
//            try
//            {
//                string Sql = string.Empty;
//                string SqlWhere = string.Empty;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCard", ref Sql) == -1) return null;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCardWhere2", ref SqlWhere) == -1) return null;
//                SqlWhere = string.Format(SqlWhere, cardNO);
//                Sql += " " + SqlWhere;
//                if (this.ExecQuery(Sql) == -1) return null;
//                Neusoft.HISFC.Models.Account.AccountCard accountCard = null;

//                while (this.Reader.Read())
//                {
//                    accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
//                    accountCard.CardNO = Reader[0].ToString();
//                    accountCard.MarkNO = Reader[1].ToString();
//                    accountCard.MarkType.ID = Reader[2].ToString();
//                    accountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
//                    list.Add(accountCard);
//                }
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return null;
//            }
//            return list
//;

//        }
//        #endregion

//        #region ���������ʻ�������
//        /// <summary>
//        /// ���������ʻ�������
//        /// </summary>
//        /// <param name="accountCard"></param>
//        /// <returns></returns>
//        public int InsertAccountCard(Neusoft.HISFC.Models.Account.AccountCard accountCard)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.InsertAccountCard", ref Sql) == -1) return -1;
//            try
//            {
//                Sql = string.Format(Sql,
//                                    accountCard.CardNO, //���￨��
//                                    accountCard.MarkNO,//��ݱ�ʶ����
//                                    accountCard.MarkType.ID.ToString(),//��ݱ�ʶ����� 1�ſ� 2IC�� 3���Ͽ�
//                                    Neusoft.FrameWork.Function.NConvert.ToInt32(accountCard.IsValid).ToString() //״̬'1'����'0'ͣ�� 
//                                    );
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return -1;
//            }
//            return this.ExecNoQuery(Sql);
//        }
//        #endregion

//        #region ���¿�״̬
//        /// <summary>
//        /// ���¿�״̬
//        /// </summary>
//        /// <param name="markNO">������</param>
//        /// <param name="type">������</param>
//        /// <param name="state">״̬</param>
//        /// <returns></returns>
//        public int UpdateAccountCardState(string markNO, string type, string state)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.UpdateAccountCardState", ref Sql) == -1) return -1;
//            try
//            {
//                Sql = string.Format(Sql, markNO, state, type);
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return -1;
//            }
//            return this.ExecNoQuery(Sql);

//        }
//        #endregion

//        #region ���ҿ�ʹ�ü�¼
//        /// <summary>
//        /// ���ҿ�ʹ�ü�¼
//        /// </summary>
//        /// <param name="cardNO">���￨��</param>
//        /// <param name="begin">��ʼʱ��</param>
//        /// <param name="end">����ʱ��</param>
//        /// <returns></returns>
//        public List<Neusoft.HISFC.Models.Account.AccountCardRecord> GetAccountCardRecord(string cardNO, string begin, string end)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.SelectAccountCardRecord", ref Sql) == -1)
//            {
//                this.Err = "����SQL���ʧ�ܣ�";
//                return null;
//            }
//            try
//            {
//                Sql = string.Format(Sql, cardNO, begin, end);
//                if (this.ExecQuery(Sql) == -1)
//                {
//                    this.Err = "���ҿ�ʹ������ʧ�ܣ�";
//                    return null;
//                }
//                List<Neusoft.HISFC.Models.Account.AccountCardRecord> list = new List<Neusoft.HISFC.Models.Account.AccountCardRecord>();
//                Neusoft.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;
//                while (this.Reader.Read())
//                {
//                    accountCardRecord = new Neusoft.HISFC.Models.Account.AccountCardRecord();
//                    accountCardRecord.MarkNO = Reader[0].ToString();
//                    accountCardRecord.MarkType.ID = Reader[1];
//                    accountCardRecord.CardNO = Reader[2].ToString();
//                    accountCardRecord.OperateTypes.ID = Reader[3];
//                    accountCardRecord.Oper.Name = Reader[4].ToString();
//                    accountCardRecord.Oper.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
//                    accountCardRecord.CardMoney = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[6]);
//                    list.Add(accountCardRecord);
//                }
//                return list;
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                return null;
//            }
//        }
//        #endregion

//        #region ɾ��������
//        /// <summary>
//        /// ɾ��������
//        /// </summary>
//        /// <param name="markNO">����</param>
//        /// <param name="markType">������</param>
//        /// <returns></returns>
//        public int DeleteAccoutCard(string markNO, string markType)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.DeleteAccountCard", ref Sql) == -1) return -1;
//            try
//            {
//                Sql = string.Format(Sql, markNO, markType);
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                return -1;
//            }
//            return this.ExecNoQuery(Sql);
//        }

//        #endregion

//        #region �ʻ�����
//        /// <summary>
//        /// �ʻ�����
//        /// </summary>
//        /// <param name="newMark">�¿���</param>
//        /// <param name="oldMark">ԭ</param>
//        /// <returns></returns>
//        public int UpdateAccountCardMark(string newMark, string oldMark)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.UpdateAccountCardMarkNo", ref Sql) == -1)
//            {
//                this.Err = "����SQL���ʧ�ܣ�";
//                return -1;
//            }
//            try
//            {
//                Sql = string.Format(Sql, newMark, oldMark);
//            }
//            catch (Exception ex)
//            {

//                this.Err = ex.Message;
//            }
//            return this.Sql.ExecNoQuery(Sql);
//        }

//        #endregion

//        #region ���ݿ��Ź����������
//        /// <summary>
//        /// ���ݿ��Ź����������
//        /// </summary>
//        /// <param name="markNo">����Ŀ���</param>
//        /// <param name="validedMarkNo"></param>
//        /// <returns></returns>
//        public int ValidMarkNO(string markNo, ref string validedMarkNo)
//        {
//            string firstleter = markNo.Substring(0, 1);
//            string lastleter = markNo.Substring(markNo.Length - 1, 1);
//            if (firstleter != ";")
//            {
//                this.Err = "��������ȷ�Ŀ��ţ�";
//                return -1;
//            }
//            if (lastleter != "?")
//            {
//                this.Err = "��������ȷ�Ŀ��ţ�";
//                return -1;
//            }
//            validedMarkNo = markNo.Substring(1, markNo.Length - 2);
//            char[] charArray = validedMarkNo.ToCharArray();
//            foreach (char c in charArray)
//            {
//                if (!char.IsNumber(c))
//                {
//                    this.Err = "��������ȷ�Ŀ��ţ�";
//                    return -1;
//                }
//            }
//            return 1;
//        }
//        #endregion

        #endregion

        #region �ʻ��������ݲ���

       // #region �ʻ�Ԥ����
       ///// <summary>
       // /// �ʻ�Ԥ����
       ///// </summary>
       ///// <param name="accountRecord">����ʵ��</param>
       ///// <returns></returns>
       // public bool AccountPrePay(Neusoft.HISFC.Models.Account.AccountRecord accountRecord)
       // {


       //     try
       //     {
       //         #region ���뽻�׼�¼
       //         if (this.InsertAccountRecord(accountRecord) < 0)
       //         {
       //             MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }
       //         #endregion

       //         #region �����ʻ����
       //         //�ڼ����ʻ����ʱ�����-���ν��׵�Ǯ�����Խ���ʱ��ǮӦ���Ǹ���
       //         decimal consumeMoney = -accountRecord.Money;
       //         if (this.UpdateAccountVacancy(accountRecord.CardNO, consumeMoney) < 0)
       //         {
       //             MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }
       //         #endregion

       //         MessageBox.Show("���� ��" + accountRecord.Money.ToString() + "�� �ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         return true;
       //     }
       //     catch
       //     {
       //         MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         return false;
       //     }

       // }
       // #endregion

       // #region  ͨ�������Ų������￨��
       // /// <summary>
       // /// ͨ�������Ų������￨��
       // /// </summary>
       // /// <param name="markNo">������</param>
       // /// <param name="markType">������</param>
       // /// <param name="cardNo">���￨��</param>
       // /// <returns>bool true �ɹ���false ʧ��</returns>
       // public bool GetCardNoByMarkNo(string markNo, Neusoft.HISFC.Models.Account.MarkTypes markType, ref string cardNo)
       // {
       //     string Sql = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.SelectCardNoByMarkNo", ref Sql) == -1)
       //     {
       //         this.Err = "����SQL���ʧ�ܣ�";
       //         return false;
       //     }
       //     try
       //     {
       //         Sql = string.Format(Sql, markNo, ((int)markType).ToString());
       //         if (this.ExecQuery(Sql) == -1)
       //         {
       //             this.Err = "��������ʧ�ܣ�";
       //             return false;
       //         }
       //         #region Sql
       //         /*select b.card_no,
       //                    b.markno,
       //                    b.type,
       //                    b.state as cardstate,
       //                    a.state as accountstate,
       //                    a.vacancy 
       //             from fin_opb_account a,fin_opb_accountcard b 
       //             where a.card_no=b.card_no 
       //               and b.markno='{0}' 
       //               and type='{1}'*/
       //         #endregion
       //         Neusoft.HISFC.Models.Account.Account account = null;
       //         while (this.Reader.Read())
       //         {
       //             account = new Neusoft.HISFC.Models.Account.Account();
       //             account.AccountCard.CardNO = this.Reader[0].ToString();
       //             account.AccountCard.MarkNO = this.Reader[1].ToString();
       //             account.AccountCard.MarkType.ID = Neusoft.FrameWork.Function.NConvert.ToInt32(Reader[2]);
       //             account.AccountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[3]);
       //         }
       //         if (account == null)
       //         {
       //             this.Err = "�ÿ�" + markNo + "�ѱ�ȡ��ʹ�ã�";
       //             return false;
       //         }
       //         if (!account.AccountCard.IsValid)
       //         {
       //             this.Err = "�ÿ�"+ markNo +"�ѱ�ֹͣʹ�ã�";
       //             return false;
       //         }
       //         cardNo = account.AccountCard.CardNO;
       //         return true;
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = "�������￨��ʧ�ܣ�" + ex.Message;
       //         return false;
       //     }

       // }

       // #endregion

       // #region �ʻ�֧��
       // /// <summary>
       // /// �ʻ�֧��
       // /// </summary>
       // /// <param name="cardNO">���￨��</param>
       // /// <param name="money">��֧����ֵ��</param>
       // /// <param name="reMark">��ʶ</param>
       // /// <param name="deptCode">���ұ���</param>
       // /// <returns>True�ɹ�Falseʧ��</returns>
       // public bool AccountPay(string cardNO, decimal money, string reMark, string deptCode)
       // {
       //     try
       //     {
       //         #region �õ��ʻ����
       //         decimal accountVacancy = 0;
       //         int result = this.GetVacancy(cardNO, ref accountVacancy);
       //         if (result <= 0)
       //         {
       //             MessageBox.Show(this.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //             return false;
       //         }
       //         #endregion

       //         #region ֧�������ж��ʻ�����Ƿ�

       //         if (accountVacancy < money)
       //         {
       //             MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�ʻ����" + accountVacancy.ToString() + "����" + money.ToString() + "��"), Neusoft.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
       //             return false;
       //         }

       //         #endregion

       //         #region ���뽻�׼�¼
       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //         accountRecord.CardNO = cardNO; //������   
       //         accountRecord.OperType.ID = (int)Neusoft.HISFC.Models.Account.OperTypes.Pay;//��������
       //         accountRecord.Money = -money;//���
       //         accountRecord.DeptCode = deptCode;//����
       //         accountRecord.Oper = (this.Operator as Neusoft.HISFC.Models.Base.Employee).ID;//����Ա
       //         accountRecord.OperTime = this.GetDateTimeFromSysDateTime();//����ʱ��
       //         accountRecord.ReMark = reMark;//��Ʊ��
       //         accountRecord.IsValid = true;//�Ƿ���Ч
       //         accountRecord.Vacancy = accountVacancy - money;//���ν������
       //         if (this.InsertAccountRecord(accountRecord) == -1)
       //         {
       //             MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("���뽻������ʧ�ܣ�"), Neusoft.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }
       //         #endregion

       //         #region �����ʻ����
       //         if (this.UpdateAccountVacancy(cardNO, money) == -1)
       //         {
       //             MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�����ʻ����ʧ�ܣ�"), Neusoft.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }

       //         #endregion

       //         MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("֧������" + money.ToString() + "�ɹ���"), Neusoft.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         return true;
       //     }
       //     catch
       //     {
       //         MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("֧������ʧ�ܣ�"), Neusoft.FrameWork.Management.Language.Msg("����"), MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         return false;
       //     }

       // }
       // #endregion

       // #region �˷��뻧
       // /// <summary>
       // /// �˷��뻧
       // /// </summary>
       // /// <param name="cardNO">���￨��</param>
       // /// <param name="money">���(�˷�ʱ��ֵ)</param>
       // /// <param name="reMark">��ʶ</param>
       // /// <param name="deptCode">���ұ���</param>
       // /// <returns></returns>
       // public bool AccountCancelPay(string cardNO, decimal money, string reMark, string deptCode)
       // {
       //     try
       //     {
       //         #region �����ʻ����
       //         //�ʻ����
       //         decimal vacancy = 0;
       //         int resullt = this.GetVacancy(cardNO, ref vacancy);
       //         if (resullt <= 0)
       //         {
       //             MessageBox.Show(this.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }
       //         //�ڸ����ʻ��������ԭ���-money�������˷��뻧ʱӦ���븺�����������
       //         if (this.UpdateAccountVacancy(cardNO, money) == -1)
       //         {
       //             MessageBox.Show("�����ʻ����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }

       //         #endregion

       //         #region ����һ���½��׼�¼

       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //         accountRecord.CardNO = cardNO;//���￨��
       //         accountRecord.DeptCode = deptCode;//���ұ���
       //         accountRecord.IsValid = true;//����״̬
       //         accountRecord.Money = -money;//���
       //         accountRecord.Oper = (this.Operator as Neusoft.HISFC.Models.Base.Employee).ID;//����Ա
       //         accountRecord.OperType.ID = ((int)Neusoft.HISFC.Models.Account.OperTypes.CancelPay).ToString();//��������
       //         accountRecord.Vacancy = vacancy - money;//���β��������
       //         accountRecord.ReMark = reMark;//��Ʊ��
       //         accountRecord.OperTime = this.GetDateTimeFromSysDateTime();//����ʱ��
       //         if (this.InsertAccountRecord(accountRecord) == -1)
       //         {
       //             MessageBox.Show("�����˷��뻧����ʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }

       //         #region ����
       //         //Neusoft.HISFC.Models.Account.AccountRecord accountRecord = this.GetAccountRecord(cardNO, reMark);
       //         //if (accountRecord == null)
       //         //{
       //         //    MessageBox.Show("��ȡ֧����¼ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         //    return false;
       //         //}
       //         //if (!accountRecord.IsValid)
       //         //{
       //         //    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�÷������ˣ��������ˣ�"), Neusoft.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         //    return false;
       //         //}
       //         //Neusoft.HISFC.Models.Account.AccountRecord oldRecord = accountRecord.Clone();
       //         //accountRecord.Vacancy = vacancy + money;
       //         //accountRecord.OperType.ID = ((int)Neusoft.HISFC.Models.Account.OperTypes.CancelPay).ToString();
       //         //accountRecord.OperTime = this.GetDateTimeFromSysDateTime();
       //         //accountRecord.DeptCode = deptCode;
       //         //accountRecord.IsValid = true;
       //         //accountRecord.Money = money;
       //         //accountRecord.Oper = (this.Operator as Neusoft.HISFC.Models.Base.Employee).ID;

       //         //if (this.InsertAccountRecord(accountRecord) == -1)
       //         //{
       //         //    MessageBox.Show("�����˷��뻧����ʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         //    return false;
       //         //}
       //         #endregion

       //         #endregion

       //         #region ����ԭ���׼�¼״̬ ��������
       //         ////int result = this.UpdateAccountRecordState(((int)Neusoft.FrameWork.Function.NConvert.ToInt32(false)).ToString(), //״̬
       //         ////                                             cardNO,//���￨��
       //         ////                                             oldRecord.OperTime.ToString(),//����ʱ��
       //         ////                                             oldRecord.ReMark);//��Ʊ��
       //         ////if (result == -1)
       //         ////{
       //         ////    MessageBox.Show("����֧�����ݱ�־ʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         ////    return false;
       //         ////}
       //         #endregion

       //         MessageBox.Show("�˷��뻧�ɹ� ��" + (-money).ToString() + "�� ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         return true;
       //     }
       //     catch (Exception ex)
       //     {
       //         MessageBox.Show("�˷��뻧ʧ�ܣ�" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         return false;
       //     }
       // }

       // #endregion

       // #region ���������ʻ�������ʱ�䡢����״̬���ҽ��׼�¼
       // /// <summary>
       // /// ���������ʻ�������ʱ�䡢����״̬���ҽ��׼�¼
       // /// </summary>
       // /// <param name="cardNO">�����ʻ�</param>
       // /// <param name="begin">��ʼʱ��</param>
       // /// <param name="end">����ʱ��</param>
       // /// <param name="opertype">��������</param>
       // /// <returns></returns>
       // public List<Neusoft.HISFC.Models.Account.AccountRecord> GetAccountRecordList(string cardNO, string begin, string end, string opertype)
       // {
       //     string Sql = string.Empty;
       //     string SqlWhere = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecord", ref Sql) == -1)
       //     {
       //         this.Err = "��ȡSQL������";
       //         return null;
       //     }
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecordWhere1", ref SqlWhere) == -1)
       //     {
       //         this.Err = "��ȡSQL������";
       //         return null;
       //     }

       //     try
       //     {
       //         SqlWhere = string.Format(SqlWhere, cardNO, begin, end, opertype);
       //         Sql += " " + SqlWhere;
       //         if (this.ExecQuery(Sql) == -1)
       //         {
       //             this.Err = "�����ʻ���������ʧ�ܣ�";
       //             return null;
       //         }
       //         List<Neusoft.HISFC.Models.Account.AccountRecord> list = new List<Neusoft.HISFC.Models.Account.AccountRecord>();
       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = null;
       //         while (this.Reader.Read())
       //         {
       //             accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //             accountRecord.CardNO = Reader[0].ToString();
       //             accountRecord.OperType.ID = Reader[1].ToString();
       //             if (Reader[2] != DBNull.Value) accountRecord.Money = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2]);
       //             accountRecord.DeptCode = Reader[3].ToString();
       //             accountRecord.Oper = Reader[4].ToString();
       //             accountRecord.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
       //             if (Reader[5] != DBNull.Value) accountRecord.ReMark = Reader[6].ToString();
       //             accountRecord.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[7]);
       //             if (Reader[8] != DBNull.Value) accountRecord.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[8]);
       //             list.Add(accountRecord);
       //         }
       //         return list;
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = ex.Message;
       //         return null;
       //     }
       // }
       // #endregion

       // #region  ���������ʻ�������ʱ����ҽ��׼�¼
       // /// <summary>
       // /// ���������ʻ�������ʱ����ҽ��׼�¼
       // /// </summary>
       // /// <param name="cardNO">���￨��</param>
       // /// <param name="begin">��ʼʱ��</param>
       // /// <param name="end">����ʱ��</param>
       // /// <returns></returns>
       // public List<Neusoft.HISFC.Models.Account.AccountRecord> GetAccountRecordList(string cardNO, string begin, string end)
       // {
       //     string Sql = string.Empty;
       //     string SqlWhere = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecord", ref Sql) == -1)
       //     {
       //         this.Err = "��ȡSQL������";
       //         return null;
       //     }
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecordWhere3", ref SqlWhere) == -1)
       //     {
       //         this.Err = "��ȡSQL������";
       //         return null;
       //     }

       //     try
       //     {
       //         SqlWhere = string.Format(SqlWhere, cardNO, begin, end);
       //         Sql += " " + SqlWhere;
       //         if (this.ExecQuery(Sql) == -1)
       //         {
       //             this.Err = "�����ʻ���������ʧ�ܣ�";
       //             return null;
       //         }
       //         List<Neusoft.HISFC.Models.Account.AccountRecord> list = new List<Neusoft.HISFC.Models.Account.AccountRecord>();
       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = null;
       //         while (this.Reader.Read())
       //         {
       //             accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //             accountRecord.CardNO = Reader[0].ToString();
       //             accountRecord.OperType.ID = Reader[1].ToString();
       //             if (Reader[2] != DBNull.Value) accountRecord.Money = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2]);
       //             accountRecord.DeptCode = Reader[3].ToString();
       //             accountRecord.Oper = Reader[4].ToString();
       //             accountRecord.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
       //             if (Reader[5] != DBNull.Value) accountRecord.ReMark = Reader[6].ToString();
       //             accountRecord.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[7]);
       //             if (Reader[8] != DBNull.Value) accountRecord.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[8]);
       //             list.Add(accountRecord);
       //         }
       //         return list;
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = ex.Message;
       //         return null;
       //     }
       // }

       // #endregion

       // #region �������￨�š���Ʊ�Ų�ѯ���׼�¼
       // /// <summary>
       // /// �������￨�š���Ʊ�Ų�ѯ���׼�¼
       // /// </summary>
       // /// <param name="cardNO">���￨��</param>
       // /// <param name="invoiceNO">��Ʊ��</param>
       // /// <returns>����ʵ��</returns>
       // private Neusoft.HISFC.Models.Account.AccountRecord GetAccountRecord(string cardNO, string invoiceNO)
       // {
       //     string Sql = string.Empty;
       //     string SqlWhere = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecord", ref Sql) == -1)
       //     {
       //         this.Err = "��ȡSQL������";
       //         return null;
       //     }
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecordWhere2", ref SqlWhere) == -1)
       //     {
       //         this.Err = "��ȡSQL������";
       //         return null;
       //     }

       //     try
       //     {
       //         SqlWhere = string.Format(SqlWhere, cardNO, invoiceNO);
       //         Sql += " " + SqlWhere;
       //         if (this.ExecQuery(Sql) == -1)
       //         {
       //             this.Err = "�����ʻ���������ʧ�ܣ�";
       //             return null;
       //         }
       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = null;
       //         while (this.Reader.Read())
       //         {
       //             accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //             accountRecord.CardNO = Reader[0].ToString();
       //             accountRecord.OperType.ID = Reader[1].ToString();
       //             if (Reader[2] != DBNull.Value) accountRecord.Money = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2]);
       //             accountRecord.DeptCode = Reader[3].ToString();
       //             accountRecord.Oper = Reader[4].ToString();
       //             accountRecord.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
       //             if (Reader[5] != DBNull.Value) accountRecord.ReMark = Reader[6].ToString();
       //             accountRecord.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[7]);
       //             if (Reader[8] != DBNull.Value) accountRecord.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[8]);
       //         }
       //         return accountRecord;
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = ex.Message;
       //         return null;
       //     }
       // }
       // #endregion

       // #region ���½���״̬
       // /// <summary>
       // /// ���½���״̬
       // /// </summary>
       // /// <param name="valid">�Ƿ���Ч0��Ч1��Ч</param>
       // /// <param name="cardNO">�����ʺ�</param>
       // /// <param name="operTime">����ʱ��</param>
       // /// <returns></returns>
       // public int UpdateAccountRecordState(string valid, string cardNO, string operTime, string remark)
       // {
       //     string Sql = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.UpdateAccountRecordValid", ref Sql) == -1) return -1;
       //     try
       //     {
       //         Sql = string.Format(Sql, valid, cardNO, operTime, remark);
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = ex.Message;
       //         this.ErrCode = ex.Message;
       //         return -1;
       //     }
       //     return this.ExecNoQuery(Sql);
       // }
       // #endregion

       // #region ��������Ϣ���׼�¼
       // /// <summary>
       // /// ��������Ϣ���׼�¼
       // /// </summary>
       // /// <returns></returns>
       // public int InsertAccountRecord(Neusoft.HISFC.Models.Account.AccountRecord accountRecord)
       // {
       //     string Sql = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.InsertAccountRecord", ref Sql) == -1) return -1;
       //     try
       //     {
       //         Sql = string.Format(Sql,
       //                           accountRecord.CardNO,
       //                           accountRecord.OperType.ID,
       //                           accountRecord.Money,
       //                           accountRecord.DeptCode,
       //                           accountRecord.Oper,
       //                           accountRecord.OperTime.ToString(),
       //                           accountRecord.ReMark,
       //                           Neusoft.FrameWork.Function.NConvert.ToInt32(accountRecord.IsValid),
       //                           accountRecord.Vacancy);
       //     }
       //     catch (Exception ex)
       //     {

       //         this.Err = ex.Message;
       //         this.ErrCode = ex.Message;
       //         return -1;
       //     }
       //     return this.ExecNoQuery(Sql);

       // }
       // #endregion

        #endregion

        #region �ʻ����ݲ���

        //#region �õ��ʻ����
        ///// <summary>
        ///// �õ��ʻ����
        ///// </summary>
        ///// <param name="cardNO">���￨��</param>
        ///// <param name="vacancy">�ʻ����</param>
        ///// <returns>-1 ʧ�� 0��û���ʻ����ʻ�ͣ�� 1�ɹ�</returns>
        //public int GetVacancy(string cardNO, ref decimal vacancy)
        //{
        //    string Sql = string.Empty;
        //    bool isHaveVacancy = false;
        //    if (this.Sql.GetSql("Fee.Account.GetVacancy", ref Sql) == -1)
        //    {
        //        this.Err = "Ϊ�ҵ�SQL��䣡";

        //        return -1;
        //    }
        //    try
        //    {
        //        if (this.ExecQuery(Sql, cardNO) == -1)
        //        {
        //            return -1;
        //        }

        //        string state = string.Empty;

        //        while (this.Reader.Read())
        //        {
        //            vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
        //            state = Reader[1].ToString();
        //            isHaveVacancy = true;
        //        }
        //        if (isHaveVacancy)
        //        {
        //            if (state == "0")
        //            {
        //                this.Err = "���ʻ���ͣ��";
        //                return 0;

        //            }
        //            return 1;
        //        }
        //        else
        //        {
        //            this.Err = "���ʻ������ڣ�";
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = "����ʻ����ʧ�ܣ�" + ex.Message;

        //        return -1;
        //    }
        //}
        //#endregion

        //#region �������￨�Ÿ����ʻ����
        ///// <summary>
        ///// �������￨�Ÿ����ʻ����
        ///// </summary>
        ///// <param name="cardNO">���￨��</param>
        ///// <param name="money">���ѽ��</param>
        ///// <returns></returns>
        //public int UpdateAccountVacancy(string cardNO, decimal money)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.UpdateAccountVacancy", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO, money);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region �������￨�Ų�������
        ///// <summary>
        ///// �������￨�Ų�������
        ///// </summary>
        ///// <param name="cardNO">���￨��</param>
        ///// <returns>�û�����</returns>
        //public string GetPassWordByCardNO(string cardNO)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.SelectPassWordByCardNo", ref Sql) == -1)
        //    {
        //        this.Err = "����SQL���ʧ�ܣ�";
        //        return "-1";
        //    }
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return "-1";
        //    }
        //    return this.ExecSqlReturnOne(Sql);
        //}
        //#endregion

        //#region �������￨�Ÿ����û�����
        ///// <summary>
        ///// �������￨�Ÿ����û�����
        ///// </summary>
        ///// <param name="cardNO">���￨��</param>
        ///// <param name="passWord">����</param>
        ///// <returns></returns>
        //public int UpdatePassWordByCardNO(string cardNO, string passWord)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.UpdatePassWord", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO, passWord);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region �������￨�Ÿ����û�ÿ�������޶�
        ///// <summary>
        ///// �������￨�Ÿ����û�ÿ�������޶�
        ///// </summary>
        ///// <param name="cardNO"></param>
        ///// <param name="dayLimit"></param>
        ///// <returns></returns>
        //public int UpdateDaylimitByCardNO(string cardNO, decimal dayLimit)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.UpdateDaylimit", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO, dayLimit);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region �����ʻ�״̬
        ///// <summary>
        ///// �����ʻ�״̬
        ///// </summary>
        ///// <param name="cardNO"></param>
        ///// <param name="state"></param>
        ///// <returns></returns>
        //public int UpdateAccountState(string cardNO, string state)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.UpdateAccountState", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql, state, cardNO);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region �½��ʻ�
        ///// <summary>
        ///// �½��ʻ�
        ///// </summary>
        ///// <param name="account">�ʻ�ʵ��</param>
        ///// <returns></returns>
        //public int InsertAccount(Neusoft.HISFC.Models.Account.Account account)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.InsertAccount", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql,
        //                            account.AccountCard.CardNO, //���￨��
        //                            Neusoft.FrameWork.Function.NConvert.ToInt32(account.IsValid).ToString() //�Ƿ���Ч
        //                            );

        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region �������￨��ȡ�ʻ���Ϣ
        ///// <summary>
        ///// �������￨��ȡ�ʻ���Ϣ
        ///// </summary>
        ///// <param name="cardNO"></param>
        ///// <returns></returns>
        //public Neusoft.HISFC.Models.Account.Account GetAccount(string cardNO)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.SelectAccount", ref Sql) == -1) return null;
        //    Neusoft.HISFC.Models.Account.Account account = null;
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO);
        //        if (this.ExecQuery(Sql) == -1) return null; ;
        //        while (this.Reader.Read())
        //        {
        //            account = new Neusoft.HISFC.Models.Account.Account();
        //            if (this.Reader[1] != DBNull.Value) account.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[1]);
        //            if (this.Reader[2] != DBNull.Value) account.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
        //            if (this.Reader[3] != DBNull.Value) account.PassWord = this.Reader[3].ToString();
        //            if (this.Reader[4] != DBNull.Value) account.DayLimit = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return null;
        //    }
        //    return account;
        //}
        //#endregion

        //#region ���������Ų����ʻ�����
        ///// <summary>
        ///// ���������Ų����ʻ�����
        ///// </summary>
        ///// <param name="markNo">������</param>
        ///// <returns></returns>
        //public Neusoft.HISFC.Models.Account.Account GetAccountByMarkNo(string markNo)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.AccountByMarkNo", ref Sql) == -1)
        //    {
        //        this.Err = "����SQL���ʧ�ܣ�";
        //        return null;
        //    }
        //    try
        //    {
        //        Sql = string.Format(Sql, markNo);
        //        if (this.ExecQuery(Sql) < 0)
        //        {
        //            this.Err = "��������ʧ�ܣ�";
        //            return null;
        //        }
        //        Neusoft.HISFC.Models.Account.Account account = new Neusoft.HISFC.Models.Account.Account();
        //        //һ������ֻ�ܶ�Ӧһ���ʻ�
        //        while (this.Reader.Read())
        //        {
        //            account.AccountCard.CardNO = this.Reader[0].ToString();
        //            account.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[1]);
        //            account.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
        //            account.PassWord = this.Reader[3].ToString();
        //            account.DayLimit = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
        //        }
        //        return account;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = "��������ʧ�ܣ�" + ex.Message;
        //        return null;
        //    }

        //}

        //#endregion

        //#region ����֤���Ų����ʻ�����
        ///// <summary>
        ///// ����֤���Ų����ʻ�����
        ///// </summary>
        ///// <param name="idenno">֤����</param>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public int SelectAccountPassWord(string idenno, ref List<Neusoft.FrameWork.Models.NeuObject> list)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.SelectAccountPassWord", ref Sql) == -1)
        //    {
        //        this.Err = "����SQL���ʧ�ܣ�";
        //        return -1;
        //    }
        //    try
        //    {
        //        Sql = string.Format(Sql, idenno);
        //        if (this.ExecQuery(Sql) == -1)
        //        {
        //            this.Err = "��������ʧ�ܣ�";
        //            return -1;
        //        }
        //        list = new List<Neusoft.FrameWork.Models.NeuObject>();
        //        while (this.Reader.Read())
        //        {
        //            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
        //            obj.ID = this.Reader[0].ToString();
        //            obj.Name = this.Reader[1].ToString();
        //            list.Add(obj);
        //        }
        //        return 1;
        //    }
        //    catch(Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        return -1;
        //    }
        //}
        //#endregion
        #endregion

        #region �����д���������  2007-11-30

        /// <summary>
        /// ���뻼����Ϣ �ౣ��һ����ӡ������ˮ��
        /// </summary>
        /// <param name="patientInfo">����ʵ��</param>
        /// <returns></returns>
        public int InsertPatientZDZL(Neusoft.HISFC.Models.Account.PatientAccount patientInfo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.InsertPatientZDZL", ref Sql) == -1)
                return -1;
            try
            {
                #region ��ʽ��SQL
                Sql = string.Format(Sql,
                                   patientInfo.PID.CardNO, //���￨��
                                   
                                   patientInfo.Name,//����
                                   patientInfo.Birthday.ToShortDateString().ToString(),//��������
                                   patientInfo.Sex.ID,//�Ա�
                                   patientInfo.IDCard,//���֤��
                                   patientInfo.Profession,//ְҵ
                                   patientInfo.CompanyName,//������λ
                                   patientInfo.PhoneBusiness,//��λ�绰
                                   patientInfo.AddressHome,//���ڻ��ͥ����
                                   patientInfo.PhoneHome,//��ͥ�绰
                                   patientInfo.DIST,//����
                                   patientInfo.Nationality.ID,//����
                                   patientInfo.Kin.Name,//��ϵ������
                                   patientInfo.Kin.RelationPhone,//��ϵ�˵绰
                                   patientInfo.Kin.RelationAddress,//��ϵ��סַ
                                   patientInfo.Kin.Relation.ID,//��ϵ�˹�ϵ
                                   patientInfo.MaritalStatus.ID,//����״��
                                   patientInfo.Country.ID,//����
                                   patientInfo.Pact.ID,//��ͬ����
                                   patientInfo.Pact.Name,//��ͬ��λ����
                                   patientInfo.AreaCode,//������
                                   patientInfo.Oper.ID,//����Ա
                                   patientInfo.Oper.OperTime, //����ʱ��
                                   Neusoft.FrameWork.Function.NConvert.ToInt32(patientInfo.IsEncrypt),//�Ƿ��������
                                   patientInfo.NormalName, //����
                                   patientInfo.IDCardType.ID,
                                   patientInfo.PID.ID //�����ӡ��
                                   );
                #endregion

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        /// <summary>
        /// ��ȡ��ӡ������ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetPrintCardID()
        {
            return this.GetSequence("Fee.Account.GetPrintCardID");
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetPatientInfo(DateTime operTime)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.SelectPatient", ref Sql) == -1)
                return null;
            if (this.Sql.GetSql("Fee.Account.SelectPatientWhere11", ref SqlWhere) == -1)
                return null;
            Neusoft.HISFC.Models.Account.PatientAccount patientInfo = null;
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            try
            {
                SqlWhere = string.Format(SqlWhere, operTime);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1)
                    return null;
                while (this.Reader.Read())
                {
                    patientInfo = new Neusoft.HISFC.Models.Account.PatientAccount();
                    #region ������Ϣ
                    if (Reader[0] != DBNull.Value)
                        patientInfo.PID.CardNO = Reader[0].ToString();//��������
                    if (Reader[1] != DBNull.Value)
                        patientInfo.Card.ICCard.ID = Reader[1].ToString(); //�����
                    if (Reader[2] != DBNull.Value)
                        patientInfo.Name = Reader[2].ToString(); //����
                    if (Reader[6] != DBNull.Value)
                        patientInfo.Sex.ID = Reader[6].ToString();//�Ա�
                    if (Reader[0] != DBNull.Value)
                        patientInfo.PID.CardNO = Reader[0].ToString();//���￨��
                    if (Reader[26] != DBNull.Value)
                        patientInfo.Pact.ID = Reader[26].ToString();//�������code
                    if (Reader[27] != DBNull.Value)
                        patientInfo.Pact.Name = Reader[27].ToString();//�����������
                    if (Reader[29] != DBNull.Value)
                        patientInfo.AreaCode = Reader[29].ToString();//������
                    if (Reader[23] != DBNull.Value)
                        patientInfo.Country.ID = Reader[23].ToString();//����
                    if (Reader[17] != DBNull.Value)
                        patientInfo.Nationality.ID = Reader[17].ToString();//����
                    patientInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);//��������
                    if (Reader[16] != DBNull.Value)
                        patientInfo.DIST = Reader[16].ToString();//����
                    if (Reader[9] != DBNull.Value)
                        patientInfo.Profession.ID = Reader[9].ToString();//ְҵ
                    if (Reader[7] != DBNull.Value)
                        patientInfo.IDCard = Reader[7].ToString();//���֤��
                    if (Reader[10] != DBNull.Value)
                        patientInfo.CompanyName = Reader[10].ToString();//������λ
                    if (Reader[11] != DBNull.Value)
                        patientInfo.PhoneBusiness = Reader[11].ToString();//��λ�绰
                    if (Reader[22] != DBNull.Value)
                        patientInfo.MaritalStatus.ID = Reader[22].ToString();//����״��
                    if (Reader[13] != DBNull.Value)
                        patientInfo.AddressHome = Reader[13].ToString();//��ͥ��ַ
                    if (Reader[14] != DBNull.Value)
                        patientInfo.PhoneHome = Reader[14].ToString();//��ͥ�绰
                    if (Reader[18] != DBNull.Value)
                        patientInfo.Kin.Name = Reader[18].ToString();//��ϵ������
                    if (Reader[19] != DBNull.Value)
                        patientInfo.Kin.RelationPhone = Reader[19].ToString();//��ϵ�˵绰
                    if (Reader[20] != DBNull.Value)
                        patientInfo.Kin.RelationAddress = Reader[20].ToString();//��ϵ�˵�ַ
                    if (Reader[21] != DBNull.Value)
                        patientInfo.Kin.Relation.ID = Reader[21].ToString();//��ϵ�˹�ϵ
                    patientInfo.IsEncrypt = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[53]);//�Ƿ��������
                    patientInfo.NormalName = Reader[54].ToString();//����
                    if (patientInfo.IsEncrypt)
                    {
                        //patientInfo.DecryptName = Neusoft.FrameWork.Interface.Classes.Function.Decrypt3DES(patientInfo.NormalName);
                    }
                    else
                    {
                        patientInfo.DecryptName = patientInfo.Name;
                    }
                    if (this.Reader[55] != DBNull.Value)
                        patientInfo.IDCardType.ID = this.Reader[55].ToString();
                    #endregion
                    list.Add(patientInfo);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return list;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="icCard"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int UpdatePrintID(string icCard, string cardNo)
        {
            string strSQL = "";

            if (this.Sql.GetSql("Neusoft.HISFC.Management.Fee.UpdatePrintID", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Neusoft.HISFC.Management.Fee.UpdatePrintID�ֶΣ�";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, icCard, cardNo);
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;

                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ��������,������
        /// </summary>
        /// <param name="icCard"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int UpdatePrintIDCase(string icCard, string cardNo)
        {
            string strSQL = "";

            if (this.Sql.GetSql("Neusoft.HISFC.Management.Fee.UpdatePrintIDCase", ref strSQL) == -1)
            {
                this.Err = "Neusoft.HISFC.Management.Fee.UpdatePrintIDCase�ֶΣ�";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, icCard, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oper"></param>
        /// <param name="operTime"></param>
        /// <param name="newFlag"></param>
        /// <param name="cost"></param>
        /// <param name="printNo"></param>
        /// <param name="patientNo"></param>
        /// <param name="patientName"></param>
        /// <param name="extend1"></param>
        /// <param name="extend2"></param>
        /// <param name="extend3"></param>
        /// <param name="validstat"></param>
        /// <returns></returns>
        public int InsertCardFee(string id, string oper, DateTime operTime, string newFlag, decimal cost, string printNo, string patientNo, string patientName, string extend1, string extend2, string extend3, string validstat)
        {
            string strSQL = "";

            if (this.Sql.GetSql("Neusoft.HISFC.Management.Fee.InsertCardFee", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Neusoft.HISFC.Management.Fee.InsertCardFee�ֶΣ�";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL,  id,  oper,  operTime.ToString(),  newFlag,  cost.ToString(),  printNo,  patientNo,  patientName,  extend1,  extend2, extend3,  validstat);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �����²���ͼƬ
        /// </summary>
        /// <param name="icCard"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public int InsertPatientPhoto(Neusoft.FrameWork.Management.Transaction trans, string icCard ,Byte[] photo)
        {
            //try
            //{
            //    this.command = new IBM.Data.DB2.DB2Command ( );   
            //    this.command.Transaction = trans.Trans as IBM.Data.DB2.DB2Transaction;
            //    this.command.Connection = this.con as IBM.Data.DB2.DB2Connection;
            //    //this.command.Connection = Neusoft.FrameWork.Management.Connection.Instance;
            //    this.command.CommandText = "Insert into COM_PATIENTINFO_PHOTO (photo,ic_cardno) values(?,?) ";
            //    IBM.Data.DB2.DB2Parameter parPhoto = new IBM.Data.DB2.DB2Parameter ( "PHOTO", IBM.Data.DB2.DB2Type.Binary, photo.Length );
            //    parPhoto.Value = photo;
            //    IBM.Data.DB2.DB2Parameter parCard = new IBM.Data.DB2.DB2Parameter ( "CARD", IBM.Data.DB2.DB2Type.VarChar, icCard.Length );
            //    parCard.Value = icCard;
            //    this.command.Parameters.Add ( parPhoto );
            //    this.command.Parameters.Add ( parCard );
            //}
            //catch ( Exception ex )
            //{
            //    this.Err = ex.Message;

            //    return -1;
            //}
            //return this.command.ExecuteNonQuery ( );

            return -1;

            
        }

        /// <summary>
        /// ��ѯ���ߵ�ͼƬ
        /// </summary>
        /// <returns></returns>
        public int QueryPatinePhoto ( string ic_CardNo, ref Byte[] Photo )
        {
            string Sql = string.Empty;
            
            if ( this.Sql.GetSql ( "Fee.Account.SelectPatientPhoto", ref Sql ) == -1 )
                return -1;


            Photo = new Byte [204800];
            try
            {
                 Sql= string.Format ( Sql,ic_CardNo);
                 System.Data.DataSet ds =new DataSet();
                if ( this.ExecQuery ( Sql,ref  ds ) == -1 )
                    return -1;
                Photo = ds.Tables[0].Rows[0][0] as Byte [ ];
                //while ( this.Reader.Read ( ) )
                //{

                //    if ( Reader [0] != DBNull.Value )
                //        Photo =  Reader [0] as Byte[];
                    
                //}
                //this.Reader.Close ( );
            }
            catch ( Exception ex )
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        ///  ɾ������ͼƬ
        /// </summary>
        /// <param name="icCard"></param>
        /// <returns></returns>
        public int DeletePatinePhoto ( string icCard )
        {
            string strSQL = "";

            if ( this.Sql.GetSql ( "Fee.Account.DeletePatientPhoto", ref strSQL ) == -1 )
            {
                this.Err = "Fee.Account.DeletePatientPhoto�ֶΣ�";

                return -1;
            }

            try
            {
                strSQL = string.Format ( strSQL, icCard);
            }
            catch ( Exception ex )
            {
                this.Err = ex.Message;

                return -1;
            }

            return this.ExecNoQuery ( strSQL );
        }

        #endregion

        #region ����Ǽ���Ϣ
        /// <summary>
        /// ����Һż�¼��
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdatePERegister(Neusoft.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            if (this.Sql.GetSql("Registration.Register.Update.PeCharge", ref sql) == -1)
                return -1;

            try
            {
                sql = string.Format(sql, register.ID, register.PID.CardNO,
                     register.DoctorInfo.SeeDate.ToString(), register.DoctorInfo.Templet.Noon.ID,
                     register.Name, register.IDCard, register.Sex.ID, register.Birthday.ToString(),
                     register.Pact.PayKind.ID, register.Pact.PayKind.Name, register.Pact.ID, register.Pact.Name,
                     register.SSN, register.DoctorInfo.Templet.RegLevel.ID, register.DoctorInfo.Templet.RegLevel.Name,
                     register.DoctorInfo.Templet.Dept.ID, register.DoctorInfo.Templet.Dept.Name,
                     register.DoctorInfo.SeeNO, register.DoctorInfo.Templet.Doct.ID,
                     register.DoctorInfo.Templet.Doct.Name, Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsFee),
                     (int)register.RegType, Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsFirst),
                     register.RegLvlFee.RegFee.ToString(), register.RegLvlFee.ChkFee.ToString(),
                     register.RegLvlFee.OwnDigFee.ToString(), register.RegLvlFee.OthFee.ToString(),
                     register.OwnCost.ToString(), register.PubCost.ToString(), register.PayCost.ToString(),
                     (int)register.Status, register.InputOper.ID, Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsSee),
                     Neusoft.FrameWork.Function.NConvert.ToInt32(register.CheckOperStat.IsCheck), register.PhoneHome,
                     register.AddressHome, (int)register.TranType, register.CardType.ID,
                     register.DoctorInfo.Templet.Begin.ToString(), register.DoctorInfo.Templet.End.ToString(),
                     register.CancelOper.ID, register.CancelOper.OperTime.ToString(),
                     register.InvoiceNO, register.RecipeNO, Neusoft.FrameWork.Function.NConvert.ToInt32(register.DoctorInfo.Templet.IsAppend),
                     register.OrderNO, register.DoctorInfo.Templet.ID,
                     register.InputOper.OperTime.ToString(), register.InSource.ID, Neusoft.FrameWork.Function.NConvert.ToInt32(register.CaseState),
                     Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt), register.NormalName);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���¹Һ������������![Registration.Register.Update.PeCharge]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion
    }
}
