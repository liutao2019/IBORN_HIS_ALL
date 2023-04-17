using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.HealthRecord;
using FS.FrameWork.Function;
using System.Data;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord.Case
{
    /// <summary>
    /// Visit<br></br>
    /// [��������: ������ѯ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-08-27]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
        public class CaseLend : FS.FrameWork.Management.Database
        {


        #region ��ѯ

        /// <summary>
        ///�����ݲ��������ѯ��������Ϣ
        /// </summary>
        /// <param name="">������ˮ��</param>
        /// <returns>��Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Case.CaseLend</returns>

        
        public ArrayList Query(string billID)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("HealthReacord.Case.CaseLend.Select", ref strSql) == -1) return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, billID);
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Case.CaseLend caseLend = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    caseLend = new FS.HISFC.Models.HealthRecord.Case.CaseLend();
                    caseLend.ID = this.Reader[0].ToString();         //������ 
                    caseLend.LendEmpl.ID= this.Reader[2].ToString(); //����Ա����
                    caseLend.StartingTime =NConvert.ToDateTime(this.Reader[3].ToString()); //��ʼ����ʱ��
                    caseLend.EndTime = NConvert.ToDateTime(this.Reader[4].ToString());           //�黹ʱ�� 
                    caseLend.AuditingOper.ID = this.Reader[6].ToString(); //���Ա����
                    caseLend.AuditingOper.OperTime = NConvert.ToDateTime(this.Reader[7].ToString()); //�������ʱ��
                    caseLend.IsAuditing = NConvert.ToBoolean(this.Reader[8].ToString()); //�Ƿ������� 
                    caseLend.IsReturn = NConvert.ToBoolean(this.Reader[9].ToString()); //�Ƿ��Ѿ��黹
                    caseLend.ReturnOper.ID = this.Reader[10].ToString(); //�黹Ա����
                    caseLend.ReturnOper.OperTime = NConvert.ToDateTime(this.Reader[11].ToString()); //ʵ�ʹ黹ʱ��
                    caseLend.ReturnConfirmOper.ID = this.Reader[12].ToString();           //�黹ȷ���˹��� 
                    caseLend.ReturnConfirmOper.OperTime = NConvert.ToDateTime(this.Reader[13].ToString()); //�黹ȷ��ʱ��
                    if (this.Reader[14].ToString().Equals("0"))        //ҵ������
                    {
                        caseLend.LendType = FS.HISFC.Models.HealthRecord.Case.EnumLendType.Lend;
                    }
                    else
                    {
                        caseLend.LendType = FS.HISFC.Models.HealthRecord.Case.EnumLendType.Refer;
                    }                  
                               
                    caseLend = null;
                }

                return List;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ���߻�����Ϣ��ѯ  com_patientinfo
        /// </summary>
        /// <param name="caseNo">������</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfo(string caseNo)
        {
            FS.HISFC.Models.RADT.PatientInfo PatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            string sql = string.Empty;
            if (Sql.GetSql("RADT.Inpatient.PatientInfoQuery", ref sql) == -1)
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
            string sqlWhere = @"
                where case_no='{0}'";
            sql = sql + sqlWhere;
            sql = string.Format(sql, caseNo);

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
        /// ���߻�����Ϣ��ѯ  met_cas_base 
        /// </summary>
        /// <param name="caseNo">������</param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Base QueryMetCasBase(string caseNo)
        {
            FS.HISFC.Models.HealthRecord.Base Info = new FS.HISFC.Models.HealthRecord.Base();
            string sql = string.Empty;
            if (Sql.GetSql("CASE.BaseDML.GetCaseBaseInfo.Select.HIS50", ref sql) == -1)
            {
                return null;
            }
            string sqlWhere = @"
                where case_no='{0}'";
            sql = sql + sqlWhere;
            sql = string.Format(sql, caseNo);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            if (Reader.Read())
            {
                try
                {
                    if (!Reader.IsDBNull(0)) Info.PatientInfo.PID.CardNO = Reader[0].ToString(); //���￨��
                    if (!Reader.IsDBNull(1)) Info.PatientInfo.Name = Reader[1].ToString(); //����
                    if (!Reader.IsDBNull(2)) Info.PatientInfo.SpellCode = Reader[2].ToString(); //ƴ����
                    if (!Reader.IsDBNull(3)) Info.PatientInfo.WBCode = Reader[3].ToString(); //���
                    if (!Reader.IsDBNull(4)) Info.PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //��������
                    if (!Reader.IsDBNull(5)) Info.PatientInfo.Sex.ID = Reader[5].ToString(); //�Ա�
                    if (!Reader.IsDBNull(6)) Info.PatientInfo.IDCard = Reader[6].ToString(); //���֤��
                    if (!Reader.IsDBNull(7)) Info.PatientInfo.BloodType.ID = Reader[7].ToString(); //Ѫ��
                    if (!Reader.IsDBNull(8)) Info.PatientInfo.Profession.ID = Reader[8].ToString(); //ְҵ
                    if (!Reader.IsDBNull(9)) Info.PatientInfo.CompanyName = Reader[9].ToString(); //������λ
                    if (!Reader.IsDBNull(10)) Info.PatientInfo.PhoneBusiness = Reader[10].ToString(); //��λ�绰
                    if (!Reader.IsDBNull(11)) Info.PatientInfo.BusinessZip = Reader[11].ToString(); //��λ�ʱ�
                    if (!Reader.IsDBNull(12)) Info.PatientInfo.AddressHome = Reader[12].ToString(); //���ڻ��ͥ����
                    if (!Reader.IsDBNull(13)) Info.PatientInfo.PhoneHome = Reader[13].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(14)) Info.PatientInfo.HomeZip = Reader[14].ToString(); //���ڻ��ͥ��������
                    if (!Reader.IsDBNull(15)) Info.PatientInfo.DIST = Reader[15].ToString(); //����
                    if (!Reader.IsDBNull(16)) Info.PatientInfo.Nationality.ID = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) Info.PatientInfo.Kin.Name = Reader[17].ToString(); //��ϵ������
                    if (!Reader.IsDBNull(18)) Info.PatientInfo.Kin.RelationPhone = Reader[18].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(19)) Info.PatientInfo.Kin.RelationAddress = Reader[19].ToString(); //��ϵ��סַ
                    if (!Reader.IsDBNull(20)) Info.PatientInfo.Kin.Relation.ID = Reader[20].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(21)) Info.PatientInfo.MaritalStatus.ID = Reader[21].ToString(); //����״��
                    if (!Reader.IsDBNull(22)) Info.PatientInfo.Country.ID = Reader[22].ToString(); //����
                    if (!Reader.IsDBNull(23)) Info.PatientInfo.Pact.PayKind.ID = Reader[23].ToString(); //�������
                    if (!Reader.IsDBNull(24)) Info.PatientInfo.Pact.PayKind.Name = Reader[24].ToString(); //�����������
                    if (!Reader.IsDBNull(25)) Info.PatientInfo.Pact.ID = Reader[25].ToString(); //��ͬ����
                    if (!Reader.IsDBNull(26)) Info.PatientInfo.Pact.Name = Reader[26].ToString(); //��ͬ��λ����
                    if (!Reader.IsDBNull(27)) Info.PatientInfo.SSN = Reader[27].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(28)) Info.PatientInfo.AreaCode = Reader[28].ToString(); //����
                    if (!Reader.IsDBNull(29)) Info.PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //ҽ�Ʒ���
                    if (!Reader.IsDBNull(30)) Info.PatientInfo.Card.ICCard.ID = Reader[30].ToString(); //���Ժ�
                    if (!Reader.IsDBNull(31)) Info.PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //ҩ�����
                    if (!Reader.IsDBNull(32)) Info.PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //��Ҫ����
                    if (!Reader.IsDBNull(33)) Info.PatientInfo.Card.NewPassword = Reader[33].ToString(); //�ʻ�����
                    if (!Reader.IsDBNull(34)) Info.PatientInfo.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //�ʻ��ܶ�
                    if (!Reader.IsDBNull(35)) Info.PatientInfo.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //�����ʻ����
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
                    if (!Reader.IsDBNull(47)) Info.PatientInfo.Memo = Reader[47].ToString(); //��ע
                    if (!Reader.IsDBNull(48)) Info.PatientInfo.User01 = Reader[48].ToString(); //����Ա
                    if (!Reader.IsDBNull(49)) Info.PatientInfo.User02 = Reader[49].ToString(); //��������
                    if (!Reader.IsDBNull(50)) Info.PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) Info.PatientInfo.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) Info.PatientInfo.IDCardType.ID = Reader[52].ToString();//֤������
                    if (!Reader.IsDBNull(53)) Info.PatientInfo.VipFlag = NConvert.ToBoolean(Reader[53]);//vip��ʶ
                    if (!Reader.IsDBNull(54)) Info.PatientInfo.MatherName = Reader[54].ToString();//ĸ������
                    if (!Reader.IsDBNull(55)) Info.PatientInfo.IsTreatment = NConvert.ToBoolean(Reader[55]);//�Ƿ���
                    if (!Reader.IsDBNull(56)) Info.PatientInfo.PID.CaseNO = Reader[56].ToString();//������
                    if (Info.PatientInfo.IsEncrypt && Info.PatientInfo.NormalName != string.Empty)
                    {
                        Info.PatientInfo.DecryptName = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(Info.PatientInfo.NormalName);
                    }
                    if (!Reader.IsDBNull(57)) Info.PatientInfo.Insurance.ID = Reader[57].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(58)) Info.PatientInfo.Insurance.Name = Reader[58].ToString(); //���չ�˾����
                    if (!Reader.IsDBNull(59)) Info.PatientInfo.Kin.RelationDoorNo = Reader[59].ToString(); //��ϵ�˵�ַ���ƺ�
                    if (!Reader.IsDBNull(60)) Info.PatientInfo.AddressHomeDoorNo = Reader[60].ToString(); //��ͥסַ���ƺ�
                    if (!Reader.IsDBNull(61)) Info.PatientInfo.Email = Reader[61].ToString(); //email
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

            return Info;
        }
        #endregion
    
    }
}
