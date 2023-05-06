using System;
using System.Collections.Generic;
using System.Data;
using FS.FrameWork.Models;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Account;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// ReturnApply<br></br>
    /// [��������: �ʻ�����]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2007-10-01]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Account : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public Account()
        { }

        #region ����
        /// <summary>
        /// ���ݿ������ȡ���źͿ�����
        /// </summary>
        private static IReadMarkNO IreadMarkNO = null;
        #endregion

        #region ˽�з���
        /// <summary>
        /// ��ȡ����Ϣ
        /// </summary>
        /// <param name="Sql">sql���</param>
        /// <returns></returns>
        private FS.HISFC.Models.Account.AccountCard GetAccountCardInfo(string Sql)
        {
            FS.HISFC.Models.Account.AccountCard accountCard = null;
            try
            {
                if (this.ExecQuery(Sql) == -1) return null;
                while (this.Reader.Read())
                {
                    accountCard = new FS.HISFC.Models.Account.AccountCard();
                    accountCard.Patient.PID.CardNO = Reader[0].ToString();
                    accountCard.MarkNO = Reader[1].ToString();
                    accountCard.MarkType.ID = Reader[2].ToString();
                    accountCard.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return accountCard;
        }

        /// <summary>
        /// ���µ���(update��insert)
        /// </summary>
        /// <param name="sqlIndex">sql����</param>
        /// <param name="args">where��������</param>
        /// <returns>1�ɹ� -1ʧ�� 0û�и��µ���¼</returns>
        private int UpdateSingTable(string sqlIndex, params string[] args)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql(sqlIndex, ref strSql) == -1)
            {
                this.Err = "��������Ϊ" + sqlIndex + "��Sql���ʧ�ܣ�";
                return -1;
            }
            return this.ExecNoQuery(strSql, args);
        }

        /// <summary>
        /// Ԥ���������ַ�������
        /// </summary>
        /// <param name="prePay"></param>
        /// <returns></returns>
        private string[] GetPrePayArgs(PrePay prePay)
        {
            string[] args = new string[] {
                                            prePay.Patient.PID.CardNO,//��������
                                            prePay.HappenNO.ToString(),//�������
                                            prePay.Patient.Name,//��������
                                            prePay.InvoiceNO,//��Ʊ��
                                            prePay.PayType.ID.ToString(),//֧����ʽ
                                            prePay.BaseCost.ToString(),//Ԥ�����
                                            prePay.Bank.Name,//��������
                                            prePay.Bank.Account,//�����ʻ�
                                            prePay.Bank.InvoiceNO,//pos������ˮ�Ż�֧Ʊ�Ż��Ʊ��
                                            NConvert.ToInt32(prePay.IsValid).ToString(),//0δ�ս�/1���ս�
                                            prePay.BalanceNO,//�ս����
                                            prePay.BalanceOper.ID,//�ս���
                                            prePay.BalanceOper.OperTime.ToString(),// �ս�ʱ��
                                            ((int)prePay.ValidState).ToString(),//Ԥ����״̬
                                            prePay.PrintTimes.ToString(),//�ش����
                                            prePay.OldInvoice,//ԭƱ�ݺ�
                                            prePay.PrePayOper.ID, //����Ա
                                            prePay.AccountNO, //�ʺ�
                                            NConvert.ToInt32(prePay.IsHostory).ToString(),//�Ƿ���ʷ����
                                            prePay.Bank.WorkName ,//������λ
                                            prePay.DonateCost.ToString() //�Żݽ��
                                        };
            return args;
        }

        /// <summary>
        /// Ԥ���������ַ�������NEW// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="prePay"></param>
        /// <returns></returns>
        private string[] GetPrePayArgsEX(PrePay prePay)
        {
            //{089AE7A4-C045-4782-9709-72F1E4B9A3FF}
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new FS.HISFC.Models.Base.Employee();
            }
            FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;
            if (dept == null)
            {
                dept = new FS.HISFC.Models.Base.Department();
            }
            if (string.IsNullOrEmpty(prePay.Hospital_id)) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                string hospitalid = dept.HospitalID;
                string hospitalname = dept.HospitalName;
                prePay.Hospital_id = hospitalid;
                prePay.Hospital_name = hospitalname;
            }

            string[] args = null;
            try
            {
                args = new string[] {
                                            prePay.Patient.PID.CardNO,//��������
                                            prePay.HappenNO.ToString(),//�������
                                            prePay.Patient.Name,//��������
                                            prePay.InvoiceNO,//��Ʊ��
                                            prePay.PayType.ID.ToString(),//֧����ʽ
                                            prePay.BaseCost.ToString(),//Ԥ�����
                                            prePay.Bank.Name,//��������
                                            prePay.Bank.Account,//�����ʻ�
                                            prePay.Bank.InvoiceNO,//pos������ˮ�Ż�֧Ʊ�Ż��Ʊ��
                                            NConvert.ToInt32(prePay.IsValid).ToString(),//0δ�ս�/1���ս�
                                            prePay.BalanceNO,//�ս����
                                            prePay.BalanceOper.ID,//�ս���
                                            prePay.BalanceOper.OperTime.ToString(),// �ս�ʱ��
                                            ((int)prePay.ValidState).ToString(),//Ԥ����״̬
                                            prePay.PrintTimes.ToString(),//�ش����
                                            prePay.OldInvoice,//ԭƱ�ݺ�
                                            prePay.PrePayOper.ID, //����Ա
                                            prePay.AccountNO, //�ʺ�
                                            NConvert.ToInt32(prePay.IsHostory).ToString(),//�Ƿ���ʷ����
                                            prePay.Bank.WorkName ,//������λ
                                            prePay.DonateCost.ToString(),//���ͽ��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9} lfhm
                                            prePay.BaseVacancy.ToString(),//���׺�Ļ����˻����
                                            prePay.DonateVacancy.ToString(), //���׺���������
                                            prePay.PrintNo,//��ӡ���ݺ�
                                            prePay.AccountType.ID,//�˻����ͱ���
                                            prePay.Hospital_id,
                                            prePay.Hospital_name// {3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
                                        };
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return args;
        }
        /// <summary>
        /// ���һ��߾��￨
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<AccountCard> GetAccountMarkList(string whereIndex, params string[] args)
        {
            List<FS.HISFC.Models.Account.AccountCard> list = new List<FS.HISFC.Models.Account.AccountCard>();
            try
            {
                string Sql = string.Empty;
                string SqlWhere = string.Empty;
                if (this.Sql.GetCommonSql("Fee.Account.SelectAccountCard", ref Sql) == -1) return null;
                if (this.Sql.GetCommonSql(whereIndex, ref SqlWhere) == -1) return null;
                SqlWhere = string.Format(SqlWhere, args);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1) return null;
                FS.HISFC.Models.Account.AccountCard accountCard = null;

                while (this.Reader.Read())
                {
                    accountCard = new FS.HISFC.Models.Account.AccountCard();
                    accountCard.Patient.PID.CardNO = Reader[0].ToString();
                    accountCard.MarkNO = Reader[1].ToString();
                    accountCard.MarkType.ID = Reader[2].ToString();

                    accountCard.MarkStatus = (MarkOperateTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    if (!Reader.IsDBNull(4))
                    {
                        accountCard.AccountLevel.ID = Reader[4].ToString();// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    }
                    if (!Reader.IsDBNull(5))
                    {
                        accountCard.BegTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5].ToString());

                    }
                    if (!Reader.IsDBNull(6))
                    {
                        accountCard.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                    }
                    list.Add(accountCard);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// ��ʼ����̬��
        /// </summary>
        /// <returns></returns>
        private bool InitReadMark()
        {
            //if (IreadMarkNO == null)
            //{
            try
            {
                IreadMarkNO = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IReadMarkNO)) as IReadMarkNO;

                if (IreadMarkNO == null)
                {
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(@"./ReadMarkNO.dll");
                    if (assembly == null) return false;
                    Type[] vType = assembly.GetTypes();
                    foreach (Type type in vType)
                    {
                        if (type.GetInterface("IReadMarkNO") != null)
                        {
                            System.Runtime.Remoting.ObjectHandle obj = System.Activator.CreateInstance(type.Assembly.ToString(), type.FullName);
                            IreadMarkNO = obj.Unwrap() as IReadMarkNO;
                            break;
                        }
                    }
                }
            }
            catch
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(@"./ReadMarkNO.dll");
                if (assembly == null) return false;
                Type[] vType = assembly.GetTypes();
                foreach (Type type in vType)
                {
                    if (type.GetInterface("IReadMarkNO") != null)
                    {
                        System.Runtime.Remoting.ObjectHandle obj = System.Activator.CreateInstance(type.Assembly.ToString(), type.FullName);
                        IreadMarkNO = obj.Unwrap() as IReadMarkNO;
                        break;
                    }
                }
            }
            // }
            return true;
        }

        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        /// <param name="Sql">WhereSql��������</param>
        /// <param name="args">Where��������</param>
        /// <returns>nullʧ��</returns>
        private List<FS.HISFC.Models.RADT.PatientInfo> GetPatient(string Sql)
        {
            try
            {
                if (this.ExecQuery(Sql) == -1) return null;
                List<FS.HISFC.Models.RADT.PatientInfo> list = new List<FS.HISFC.Models.RADT.PatientInfo>();
                FS.HISFC.Models.RADT.PatientInfo PatientInfo = null;
                while (this.Reader.Read())
                {
                    PatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    #region ������Ϣ
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
                    if (!Reader.IsDBNull(47)) PatientInfo.Memo = Reader[47].ToString(); //��ע
                    if (!Reader.IsDBNull(48)) PatientInfo.User01 = Reader[48].ToString(); //����Ա
                    if (!Reader.IsDBNull(49)) PatientInfo.User02 = Reader[49].ToString(); //��������
                    if (!Reader.IsDBNull(50)) PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());//�Ƿ����
                    if (!Reader.IsDBNull(51)) PatientInfo.NormalName = Reader[51].ToString(); //����
                    if (!Reader.IsDBNull(52)) PatientInfo.IDCardType.ID = Reader[52].ToString();//֤������
                    if (!Reader.IsDBNull(53)) PatientInfo.CrmID = Reader[53].ToString();//crmid{67CE2526-5E7F-4c92-911F-56CA0077679A}
                    //if (!Reader.IsDBNull(53)) PatientInfo.VipFlag = NConvert.ToBoolean(Reader[53]);//vip��ʶ
                    //if (!Reader.IsDBNull(54)) PatientInfo.MatherName = Reader[54].ToString();//ĸ������
                    //if (!Reader.IsDBNull(55)) PatientInfo.IsTreatment = NConvert.ToBoolean(Reader[55]);//�Ƿ���
                    //{6036F4C6-9452-4f21-8634-940AACD4B296}
                    //if (!Reader.IsDBNull(56)) PatientInfo.PID.CaseNO = Reader[56].ToString();//������
                    #endregion
                    list.Add(PatientInfo);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }

        //{63F68506-F49D-4ed5-92BD-28A52AF54626}
        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        /// <param name="Sql">WhereSql��������</param>
        /// <returns>nullʧ��</returns>
        private List<FS.HISFC.Models.Account.AccountCard> GetAccountCardList(string Sql)
        {
            try
            {
                if (this.ExecQuery(Sql) == -1) return null;
                List<FS.HISFC.Models.Account.AccountCard> list = new List<AccountCard>();
                FS.HISFC.Models.Account.AccountCard accountCard = null;
                while (this.Reader.Read())
                {
                    accountCard = new AccountCard();
                    #region ������Ϣ
                    if (!Reader.IsDBNull(0)) accountCard.Patient.PID.CardNO = Reader[0].ToString(); //���￨��
                    if (!Reader.IsDBNull(1)) accountCard.Patient.Name = Reader[1].ToString(); //����
                    if (!Reader.IsDBNull(2)) accountCard.Patient.SpellCode = Reader[2].ToString(); //ƴ����
                    if (!Reader.IsDBNull(3)) accountCard.Patient.WBCode = Reader[3].ToString(); //���
                    if (!Reader.IsDBNull(4)) accountCard.Patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //��������
                    if (!Reader.IsDBNull(5)) accountCard.Patient.Sex.ID = Reader[5].ToString(); //�Ա�
                    if (!Reader.IsDBNull(6)) accountCard.Patient.IDCard = Reader[6].ToString(); //���֤��
                    if (!Reader.IsDBNull(7)) accountCard.Patient.BloodType.ID = Reader[7].ToString(); //Ѫ��
                    if (!Reader.IsDBNull(8)) accountCard.Patient.Profession.ID = Reader[8].ToString(); //ְҵ
                    if (!Reader.IsDBNull(9)) accountCard.Patient.CompanyName = Reader[9].ToString(); //������λ
                    if (!Reader.IsDBNull(10)) accountCard.Patient.PhoneBusiness = Reader[10].ToString(); //��λ�绰
                    if (!Reader.IsDBNull(11)) accountCard.Patient.BusinessZip = Reader[11].ToString(); //��λ�ʱ�
                    if (!Reader.IsDBNull(12)) accountCard.Patient.AddressHome = Reader[12].ToString(); //���ڻ��ͥ����
                    if (!Reader.IsDBNull(13)) accountCard.Patient.PhoneHome = Reader[13].ToString(); //��ͥ�绰
                    if (!Reader.IsDBNull(14)) accountCard.Patient.HomeZip = Reader[14].ToString(); //���ڻ��ͥ��������
                    if (!Reader.IsDBNull(15)) accountCard.Patient.DIST = Reader[15].ToString(); //����
                    if (!Reader.IsDBNull(16)) accountCard.Patient.Nationality.ID = Reader[16].ToString(); //����
                    if (!Reader.IsDBNull(17)) accountCard.Patient.Kin.Name = Reader[17].ToString(); //��ϵ������
                    if (!Reader.IsDBNull(18)) accountCard.Patient.Kin.RelationPhone = Reader[18].ToString(); //��ϵ�˵绰
                    if (!Reader.IsDBNull(19)) accountCard.Patient.Kin.RelationAddress = Reader[19].ToString(); //��ϵ��סַ
                    if (!Reader.IsDBNull(20)) accountCard.Patient.Kin.Relation.ID = Reader[20].ToString(); //��ϵ�˹�ϵ
                    if (!Reader.IsDBNull(21)) accountCard.Patient.MaritalStatus.ID = Reader[21].ToString(); //����״��
                    if (!Reader.IsDBNull(22)) accountCard.Patient.Country.ID = Reader[22].ToString(); //����
                    if (!Reader.IsDBNull(23)) accountCard.Patient.Pact.PayKind.ID = Reader[23].ToString(); //�������
                    if (!Reader.IsDBNull(24)) accountCard.Patient.Pact.PayKind.Name = Reader[24].ToString(); //�����������
                    if (!Reader.IsDBNull(25)) accountCard.Patient.Pact.ID = Reader[25].ToString(); //��ͬ����
                    if (!Reader.IsDBNull(26)) accountCard.Patient.Pact.Name = Reader[26].ToString(); //��ͬ��λ����
                    if (!Reader.IsDBNull(27)) accountCard.Patient.SSN = Reader[27].ToString(); //ҽ��֤��
                    if (!Reader.IsDBNull(28)) accountCard.Patient.AreaCode = Reader[28].ToString(); //����
                    if (!Reader.IsDBNull(29)) accountCard.Patient.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //ҽ�Ʒ���
                    if (!Reader.IsDBNull(30)) accountCard.Patient.Card.ICCard.ID = Reader[30].ToString(); //���Ժ�
                    if (!Reader.IsDBNull(31)) accountCard.Patient.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //ҩ�����
                    if (!Reader.IsDBNull(32)) accountCard.Patient.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //��Ҫ����
                    if (!Reader.IsDBNull(33)) accountCard.Patient.Card.NewPassword = Reader[33].ToString(); //�ʻ�����
                    if (!Reader.IsDBNull(34)) accountCard.Patient.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //�ʻ��ܶ�
                    if (!Reader.IsDBNull(35)) accountCard.Patient.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //�����ʻ����
                    if (!Reader.IsDBNull(47)) accountCard.Patient.Memo = Reader[47].ToString(); //��ע
                    if (!Reader.IsDBNull(48)) accountCard.Patient.User01 = Reader[48].ToString(); //����Ա
                    if (!Reader.IsDBNull(49)) accountCard.Patient.User02 = Reader[49].ToString(); //��������
                    if (!Reader.IsDBNull(50)) accountCard.Patient.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());//�Ƿ����
                    if (!Reader.IsDBNull(51)) accountCard.Patient.NormalName = Reader[51].ToString(); //����
                    if (!Reader.IsDBNull(52)) accountCard.Patient.IDCardType.ID = Reader[52].ToString();//֤������
                    //if (!Reader.IsDBNull(53)) accountCard.Patient.VipFlag = NConvert.ToBoolean(Reader[53]);//vip��ʶ
                    //if (!Reader.IsDBNull(54)) accountCard.Patient.MatherName = Reader[54].ToString();//ĸ������
                    //if (!Reader.IsDBNull(55)) accountCard.Patient.IsTreatment = NConvert.ToBoolean(Reader[55]);//�Ƿ���
                    if (!Reader.IsDBNull(56)) accountCard.Patient.PID.CaseNO = Reader[56].ToString();//������
                    if (!Reader.IsDBNull(57)) accountCard.MarkNO = this.Reader[57].ToString(); //���￨��
                    if (!Reader.IsDBNull(58)) accountCard.MarkType.ID = this.Reader[58].ToString(); //������
                    if (!Reader.IsDBNull(59)) accountCard.AccountLevel.ID = this.Reader[59].ToString(); //��Ա�ȼ�
                    if (!Reader.IsDBNull(60)) accountCard.AccountLevel.Name = this.Reader[60].ToString(); //��Ա�ȼ�����
                    if (!Reader.IsDBNull(61)) accountCard.BegTime = NConvert.ToDateTime(this.Reader[61].ToString()); //��ʼʱ��
                    if (!Reader.IsDBNull(62)) accountCard.EndTime = NConvert.ToDateTime(this.Reader[62].ToString()); //��ֹʱ��
                    if (!Reader.IsDBNull(63)) accountCard.Patient.User01 = this.Reader[63].ToString(); //��סַ

                    #endregion
                    list.Add(accountCard);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }



        /// <summary>
        /// ��ȡ��ǰԺ��������Ա��// {3218011F-CCDA-49f3-B468-06F25B3F7F72}
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Base.Employee> GetCurrDeptEmployee(string hospitalID)
        {
            try
            {

                string sqlStr = string.Empty;
                if (this.Sql.GetCommonSql("Fee.Account.GetCurrDeptEmployee", ref sqlStr) == -1)
                {
                    this.Err = "��������ΪFee.Account.GetCurrDeptEmployee��sql���ʧ�ܣ�";
                    return null;
                }
                sqlStr = string.Format(sqlStr, hospitalID);
                if (this.ExecQuery(sqlStr) == -1) return null;
                List<FS.HISFC.Models.Base.Employee> list = new List<FS.HISFC.Models.Base.Employee>();
                FS.HISFC.Models.Base.Employee employee = null;
                while (this.Reader.Read())
                {
                    employee = new FS.HISFC.Models.Base.Employee();
                    #region ������Ϣ
                    if (!Reader.IsDBNull(0)) employee.ID = Reader[0].ToString();

                    #endregion
                    list.Add(employee);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }
        /// <summary>
        /// �����ʻ���ϢNEW lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="whereIndex">where��������</param>
        /// <param name="args">����</param>
        /// <returns></returns>
        private FS.HISFC.Models.Account.Account GetAccountEX(string whereIndex, params string[] args)
        {
            string sqlStr = string.Empty;
            string sqlWhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccount.1", ref sqlStr) == -1)
            {
                this.Err = "��������ΪFee.Account.SelectAccount.1��sql���ʧ�ܣ�";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref sqlWhere) == -1)
            {
                this.Err = "��������Ϊ" + whereIndex + "��sql���ʧ�ܣ�";
                return null;
            }
            sqlStr += " " + sqlWhere;
            FS.HISFC.Models.Account.Account account = null;
            try
            {
                sqlStr = string.Format(sqlStr, args);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return null; ;
                }
                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.CardNO = this.Reader[0].ToString();
                    if (this.Reader[1] != DBNull.Value) account.ValidState = (HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[1]));
                    if (this.Reader[2] != DBNull.Value) account.PassWord = HisDecrypt.Decrypt(this.Reader[2].ToString());
                    if (this.Reader[3] != DBNull.Value) account.DayLimit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    account.ID = this.Reader[4].ToString();
                    account.IsEmpower = NConvert.ToBoolean(this.Reader[5]);
                    account.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    account.DonateVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                    account.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                    account.Limit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                    account.BaseAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                    account.DonateAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                    account.CouponAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                    account.AccountLevel.ID = this.Reader[13].ToString();// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    account.AccountLevel.Name = this.Reader[14].ToString();
                    account.CreateEnvironment.ID = this.Reader[15].ToString();
                    account.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[16].ToString());
                    account.OperEnvironment.ID = this.Reader[17].ToString();
                    account.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                }
                return account;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

        }

        /// <summary>
        /// ���ݿ��Ų�ѯ��ͨ�˻����
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public string GetAccountDetailPTYE(string cardNO)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetAccountDetailPTYE", ref sqlStr) == -1)
            {
                this.Err = "��������ΪFee.Account.GetAccountDetailPTYE��sql���ʧ�ܣ�";
                return null;
            }

            try
            {
                string CKDonateAmout = string.Empty;
                sqlStr = string.Format(sqlStr, cardNO);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return null; ;
                }
                while (this.Reader.Read())
                {
                    CKDonateAmout = this.Reader[0].ToString();
                }
                return CKDonateAmout;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// ���ݿ��Ų�ѯ��ͨ�˻��������
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public string GetAccountDetailPT(string cardNO)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetAccountDetailPT", ref sqlStr) == -1)
            {
                this.Err = "��������ΪFee.Account.GetAccountDetailPT��sql���ʧ�ܣ�";
                return null;
            }

            try
            {
                string CKDonateAmout = string.Empty;
                sqlStr = string.Format(sqlStr, cardNO);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return null; ;
                }
                while (this.Reader.Read())
                {
                    CKDonateAmout = this.Reader[0].ToString();
                }
                return CKDonateAmout;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }


        /// <summary>
        /// ���ݿ��Ų�ѯ�����˻����
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public string GetAccountDetailCK(string cardNO)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetAccountDetailCK", ref sqlStr) == -1)
            {
                this.Err = "��������ΪFee.Account.GetAccountDetailCK��sql���ʧ�ܣ�";
                return null;
            }

            try
            {
                string CKDonateAmout = string.Empty;
                sqlStr = string.Format(sqlStr, cardNO);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return null; ;
                }
                while (this.Reader.Read())
                {
                    CKDonateAmout = this.Reader[0].ToString();
                }
                return CKDonateAmout;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// �����ʻ�Ԥ������Ϣ
        /// </summary>
        /// <param name="whereIndex">WhereSql��������</param>
        /// <param name="args">Where��������</param>
        /// <returns>null ʧ��</returns>
        private List<PrePay> GetPrePayList(string whereIndex, params string[] args)
        {
            string sqlstr = string.Empty;
            string sqlwhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetPrePayRecrod", ref sqlstr) < 0)
            {
                this.Err = "����ΪFee.Account.GetPrePayRecrod��SQL��䲻���ڣ�";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref sqlwhere) < 0)
            {
                this.Err = "����Ϊ" + whereIndex + "��SQL��䲻���ڣ�";
                return null;
            }
            sqlstr += " " + sqlwhere;
            if (this.ExecQuery(sqlstr, args) < 0) return null;
            List<PrePay> list = new List<PrePay>();
            PrePay prepay = null;
            try
            {
                while (this.Reader.Read())
                {
                    prepay = new PrePay();
                    prepay.Patient.PID.CardNO = this.Reader[0].ToString(); //���￨��
                    prepay.HappenNO = NConvert.ToInt32(this.Reader[1]); //�������
                    prepay.Patient.Name = this.Reader[2].ToString(); //��������
                    prepay.InvoiceNO = this.Reader[3].ToString();//Ʊ�ݺ�
                    prepay.PayType.ID = this.Reader[4].ToString();//֧����ʽ
                    prepay.BaseCost = NConvert.ToDecimal(this.Reader[5]); //Ԥ����
                    prepay.Bank.Name = this.Reader[6].ToString(); //����
                    prepay.Bank.Account = this.Reader[7].ToString();//�����ʺ�
                    prepay.Bank.InvoiceNO = this.Reader[8].ToString();//�����ʺ�
                    prepay.IsValid = NConvert.ToBoolean(this.Reader[9]);//�Ƿ��ս���
                    prepay.BalanceNO = this.Reader[10].ToString();//�ս��
                    prepay.BalanceOper.ID = this.Reader[11].ToString(); //�ս���
                    prepay.BalanceOper.OperTime = NConvert.ToDateTime(this.Reader[12]);//�ս�ʱ��
                    prepay.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[13]); //״̬
                    prepay.PrintTimes = NConvert.ToInt32(this.Reader[14]);//��ӡ����;
                    prepay.OldInvoice = this.Reader[15].ToString(); //ԭ�վݺ�
                    prepay.PrePayOper.ID = this.Reader[16].ToString();//����Ա
                    prepay.PrePayOper.OperTime = NConvert.ToDateTime(this.Reader[17]);//����ʱ��
                    prepay.AccountNO = this.Reader[18].ToString();//�˺�
                    prepay.IsHostory = NConvert.ToBoolean(this.Reader[19].ToString());
                    prepay.Bank.WorkName = this.Reader[20].ToString();
                    list.Add(prepay);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// ҽ�����
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cardNO"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        private int UpdateMedicalByCardNo(string sql, string cardNO, string operId)
        {
            string sqlstr = string.Empty;
            string sqlhead = string.Empty;

            if (this.Sql.GetCommonSql(sql, ref sqlstr) < 0)
            {
                this.Err = "����ΪFee.Account.UpdateMedicalInsurance��SQL��䲻���ڣ�";
                return 0;
            }

            if (this.Sql.GetCommonSql("Fee.Account.InsertMedicalInsuranceHead", ref sqlhead) < 0)
            {
                this.Err = "����ΪFee.Account.InsertMedicalInsuranceHead��SQL��䲻���ڣ�";
                return 0;
            }

            string headseq = GetMedicalInsuranceHeadSeq();

            sqlhead = string.Format(sqlhead, headseq, cardNO, "1", operId);
            sqlstr = string.Format(sqlstr, cardNO, operId, headseq);

            int row = this.ExecNoQuery(sqlhead);

            if (row > 0)
            {
                return this.ExecNoQuery(sqlstr);
            }
            else
            {
                return 0;
            }


        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        private string GetMedicalInsuranceHeadSeq()
        {
            string sqlstr = @"select seq_gzsi_his_accountaudithead.Nextval from dual ";
            if (this.ExecQuery(sqlstr) < 0) return null;
            string headseq = "";
            try
            {
                while (this.Reader.Read())
                {
                    headseq = this.Reader[0].ToString();
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            return headseq;
        }

        /// <summary>
        /// ҽ��������Ŀ��ϸ
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        private List<AccountMedicalInsurance> GetMedicalInsuranceEX(string sql, string cardNO)
        {
            string sqlstr = string.Empty;
            if (this.Sql.GetCommonSql(sql, ref sqlstr) < 0)
            {
                this.Err = "����ΪFee.Account.GetMedicalInsurance��SQL��䲻���ڣ�";
                return null;
            }
            sqlstr = string.Format(sqlstr, cardNO);

            if (this.ExecQuery(sqlstr) < 0) return null;

            List<AccountMedicalInsurance> list = new List<AccountMedicalInsurance>();
            AccountMedicalInsurance prepay = null;
            try
            {
                while (this.Reader.Read())
                {
                    prepay = new AccountMedicalInsurance();
                    prepay.Cardno = this.Reader[0].ToString(); //���￨��
                    prepay.Name = this.Reader[1].ToString();
                    prepay.Xmbh = this.Reader[2].ToString();
                    prepay.Xmmc = this.Reader[3].ToString();
                    prepay.Createtime = NConvert.ToDateTime(this.Reader[4]);
                    prepay.Cliniccode = this.Reader[5].ToString();
                    prepay.Je = NConvert.ToDecimal(this.Reader[6]);
                    prepay.Qty = NConvert.ToDecimal(this.Reader[7]);
                    prepay.State = this.Reader[8].ToString();
                    prepay.Operenvironment.ID = this.Reader[9].ToString();
                    prepay.Operenvironment.OperTime = NConvert.ToDateTime(this.Reader[10]);
                    list.Add(prepay);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }


        /// <summary>
        /// �����ʻ�Ԥ������ϢNEW lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="whereIndex">WhereSql��������</param>
        /// <param name="args">Where��������</param>
        /// <returns>null ʧ��</returns>
        private List<PrePay> GetPrePayListEX(string whereIndex, params string[] args)
        {
            string sqlstr = string.Empty;
            string sqlwhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetPrePayRecrod.1", ref sqlstr) < 0)
            {
                this.Err = "����ΪFee.Account.GetPrePayRecrod.1��SQL��䲻���ڣ�";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref sqlwhere) < 0)
            {
                this.Err = "����Ϊ" + whereIndex + "��SQL��䲻���ڣ�";
                return null;
            }
            sqlstr += " " + sqlwhere;
            if (this.ExecQuery(sqlstr, args) < 0) return null;
            List<PrePay> list = new List<PrePay>();
            PrePay prepay = null;
            try
            {
                while (this.Reader.Read())
                {
                    prepay = new PrePay();
                    prepay.Patient.PID.CardNO = this.Reader[0].ToString(); //���￨��
                    prepay.HappenNO = NConvert.ToInt32(this.Reader[1]); //�������
                    prepay.Patient.Name = this.Reader[2].ToString(); //��������
                    prepay.InvoiceNO = this.Reader[3].ToString();//Ʊ�ݺ�
                    prepay.PayType.ID = this.Reader[4].ToString();//֧����ʽ
                    prepay.BaseCost = NConvert.ToDecimal(this.Reader[5]); //Ԥ����
                    prepay.Bank.Name = this.Reader[6].ToString(); //����
                    prepay.Bank.Account = this.Reader[7].ToString();//�����ʺ�
                    prepay.Bank.InvoiceNO = this.Reader[8].ToString();//�����ʺ�
                    prepay.IsValid = NConvert.ToBoolean(this.Reader[9]);//�Ƿ��ս���
                    prepay.BalanceNO = this.Reader[10].ToString();//�ս��
                    prepay.BalanceOper.ID = this.Reader[11].ToString(); //�ս���
                    prepay.BalanceOper.OperTime = NConvert.ToDateTime(this.Reader[12]);//�ս�ʱ��
                    prepay.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[13]); //״̬
                    prepay.PrintTimes = NConvert.ToInt32(this.Reader[14]);//��ӡ����;
                    prepay.OldInvoice = this.Reader[15].ToString(); //ԭ�վݺ�
                    prepay.PrePayOper.ID = this.Reader[16].ToString();//����Ա
                    prepay.PrePayOper.OperTime = NConvert.ToDateTime(this.Reader[17]);//����ʱ��
                    prepay.AccountNO = this.Reader[18].ToString();//�˺�
                    prepay.IsHostory = NConvert.ToBoolean(this.Reader[19].ToString());
                    prepay.Bank.WorkName = this.Reader[20].ToString();

                    prepay.DonateCost = NConvert.ToDecimal(this.Reader[21]); //���ͽ��
                    prepay.BaseVacancy = NConvert.ToDecimal(this.Reader[22]); //�����˻����׺����
                    prepay.DonateVacancy = NConvert.ToDecimal(this.Reader[23]); //�����˻����׺����
                    prepay.PrintNo = this.Reader[24].ToString();//��ӡ���ݺ�
                    prepay.AccountType.ID = this.Reader[25].ToString();//�˻�����
                    prepay.AccountType.Name = this.Reader[26].ToString();
                    prepay.Memo = this.Reader[27].ToString();//��ע
                    list.Add(prepay);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }



        /// <summary>
        /// ��������˻����ͱ�������˻����������Ϣ// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="accountType"></param>
        /// <returns></returns>
        private List<AccountDetail> GetAccountDetailSelect(string accountNo, string accountType, string vailFlag, string whereSql)
        {
            string sqlstr = string.Empty;
            string whereStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetAccountDetail.Select", ref sqlstr) < 0)
            {
                this.Err = "����ΪFee.Account.GetAccountDetail.Select��SQL��䲻���ڣ�";
                return null;
            }
            if (this.Sql.GetCommonSql(whereSql, ref whereStr) < 0)
            {
                this.Err = "����Ϊ" + whereSql + "��SQL��䲻���ڣ�";
                return null;
            }
            sqlstr += "  " + whereStr;
            if (this.ExecQuery(sqlstr, accountNo, accountType, vailFlag) < 0) return null;
            List<AccountDetail> list = new List<AccountDetail>();
            AccountDetail accountDetail = null;
            try
            {
                while (this.Reader.Read())
                {
                    accountDetail = new AccountDetail();
                    accountDetail.ID = this.Reader[0].ToString(); //�˺�
                    accountDetail.AccountType.ID = this.Reader[1].ToString(); //�˻����ͱ���
                    accountDetail.AccountType.Name = this.Reader[2].ToString(); //�˻���������
                    accountDetail.CardNO = this.Reader[3].ToString(); //���￨��
                    if (this.Reader[4].ToString() == "0")//�˻�״̬
                    {
                        accountDetail.ValidState = EnumValidState.Invalid;
                    }
                    else if (this.Reader[4].ToString() == "1")
                    {
                        accountDetail.ValidState = EnumValidState.Valid;
                    }
                    else if (this.Reader[4].ToString() == "2")
                    {
                        accountDetail.ValidState = EnumValidState.Ignore;
                    }
                    else if (this.Reader[4].ToString() == "3")
                    {
                        accountDetail.ValidState = EnumValidState.Extend;
                    }

                    accountDetail.DayLimit = NConvert.ToDecimal(this.Reader[5]); //������������
                    accountDetail.BaseVacancy = NConvert.ToDecimal(this.Reader[6]); //�˻��������
                    accountDetail.DonateVacancy = NConvert.ToDecimal(this.Reader[7]);//�˻��������
                    accountDetail.CouponVacancy = NConvert.ToDecimal(this.Reader[8]);//�˻�����ʣ��
                    accountDetail.BaseAccumulate = NConvert.ToDecimal(this.Reader[9]);//�˻������ۼƽ��
                    accountDetail.DonateAccumulate = NConvert.ToDecimal(this.Reader[10]);//�˻������ۼƽ��
                    accountDetail.CouponAccumulate = NConvert.ToDecimal(this.Reader[11]);//�˻������ۼ�
                    accountDetail.CreateEnvironment.ID = this.Reader[12].ToString();//������
                    accountDetail.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[13].ToString());//����ʱ��
                    accountDetail.OperEnvironment.ID = this.Reader[14].ToString();//������
                    accountDetail.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[15].ToString()); //����ʱ��
                    list.Add(accountDetail);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }
        /// <summary>
        /// �����ʻ����ײ�����ˮ��Ϣ
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<AccountRecord> GetAccountRecord(string whereIndex, params string[] args)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountRecord", ref Sql) == -1)
            {
                this.Err = "��ȡSQL������";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref SqlWhere) == -1)
            {
                this.Err = "��ȡSQL������";
                return null;
            }

            try
            {
                SqlWhere = string.Format(SqlWhere, args);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "�����ʻ���������ʧ�ܣ�";
                    return null;
                }
                List<FS.HISFC.Models.Account.AccountRecord> list = new List<FS.HISFC.Models.Account.AccountRecord>();
                FS.HISFC.Models.Account.AccountRecord accountRecord = null;
                while (this.Reader.Read())
                {
                    accountRecord = new FS.HISFC.Models.Account.AccountRecord();
                    accountRecord.Patient.PID.CardNO = Reader[0].ToString();
                    accountRecord.AccountNO = Reader[1].ToString();
                    accountRecord.OperType.ID = Reader[2].ToString();
                    if (Reader[3] != DBNull.Value) accountRecord.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3]);
                    accountRecord.DonateCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[4]);
                    if (Reader[5] != DBNull.Value) accountRecord.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5]);
                    accountRecord.DonateVacancy = FS.FrameWork.Function.NConvert.ToDecimal(Reader[6]);
                    accountRecord.FeeDept.ID = Reader[7].ToString();
                    accountRecord.FeeDept.Name = Reader[8].ToString();
                    accountRecord.Oper.ID = Reader[9].ToString();
                    accountRecord.Oper.Name = Reader[10].ToString();
                    accountRecord.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11]);
                    if (Reader[9] != DBNull.Value) accountRecord.ReMark = Reader[12].ToString();
                    accountRecord.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[13]);
                    if (Reader[14] != DBNull.Value) accountRecord.EmpowerPatient.PID.CardNO = this.Reader[14].ToString();
                    if (Reader[15] != DBNull.Value) accountRecord.EmpowerPatient.Name = this.Reader[15].ToString();
                    accountRecord.EmpowerCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    accountRecord.InvoiceType.ID = this.Reader[17].ToString();
                    accountRecord.InvoiceNo = this.Reader[18].ToString();
                    accountRecord.PayType.ID = this.Reader[19].ToString();
                    accountRecord.PayInvoiceNo = this.Reader[20].ToString();
                    if (Reader[21] != DBNull.Value)
                        accountRecord.AccountType.ID = this.Reader[21].ToString();// {74621CF6-D26C-44aa-87DC-C68D0867BAC5}
                    list.Add(accountRecord);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ��Ȩʵ�������ַ�������
        /// </summary>
        /// <param name="accountEmpower"></param>
        /// <returns></returns>
        private string[] GetEmpowerArgs(AccountEmpower accountEmpower)
        {
            string[] args = new string[] {accountEmpower.AccountCard.Patient.PID.CardNO,
                                          accountEmpower.AccountCard.Patient.Name,  
                                          accountEmpower.AccountNO,
                                          accountEmpower.AccountCard.MarkNO,  
                                          accountEmpower.AccountCard.MarkType.ID,
                                          accountEmpower.EmpowerCard.Patient.PID.CardNO, 
                                          accountEmpower.EmpowerCard.Patient.Name,
                                          accountEmpower.EmpowerCard.MarkNO,  
                                          accountEmpower.EmpowerCard.MarkType.ID,
                                          accountEmpower.EmpowerLimit.ToString(),
                                          FS.HisDecrypt.Encrypt(accountEmpower.PassWord),
                                          accountEmpower.Oper.ID,
                                          accountEmpower.Vacancy.ToString(),
                                          (NConvert.ToInt32(accountEmpower.ValidState)).ToString()
                                          };
            return args;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<AccountEmpower> GetEmpowerList(string whereIndex, params string[] args)
        {
            string sql = string.Empty;
            string sqlwhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectEmpower", ref sql) < 0)
            {
                this.Err = "��������ΪFee.Account.SelectEmpower��SQL���ʧ�ܣ�";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref sqlwhere) < 0)
            {
                this.Err = "��������Ϊ" + whereIndex + "��SQL���ʧ�ܣ�";
                return null;
            }
            sql += " " + string.Format(sqlwhere, args);
            if (this.ExecQuery(sql) < 0) return null;
            List<AccountEmpower> list = new List<AccountEmpower>();
            AccountEmpower obj = null;
            try
            {
                while (this.Reader.Read())
                {
                    obj = new AccountEmpower();
                    obj.AccountCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    obj.AccountCard.Patient.Name = this.Reader[1].ToString();
                    obj.AccountNO = this.Reader[2].ToString();
                    obj.AccountCard.MarkNO = this.Reader[3].ToString();
                    obj.AccountCard.MarkType.ID = this.Reader[4].ToString();
                    obj.EmpowerCard.Patient.PID.CardNO = this.Reader[5].ToString();
                    obj.EmpowerCard.Patient.Name = this.Reader[6].ToString();
                    obj.EmpowerCard.MarkNO = this.Reader[7].ToString();
                    obj.EmpowerCard.MarkType.ID = this.Reader[8].ToString();
                    obj.EmpowerLimit = NConvert.ToDecimal(this.Reader[9]);
                    obj.PassWord = FS.HisDecrypt.Decrypt(this.Reader[10].ToString());
                    obj.Oper.ID = this.Reader[11].ToString();
                    obj.Oper.OperTime = NConvert.ToDateTime(this.Reader[12]);
                    obj.Vacancy = NConvert.ToDecimal(this.Reader[13]);
                    obj.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[14]);
                    list.Add(obj);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            return list;
        }

        /// <summary>
        /// �õ��ʻ����
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="vacancy">�ʻ����</param>
        /// <returns>-1 ʧ�� 0��û���ʻ����ʻ�ͣ�û��ʻ��ѱ�ע�� 1�ɹ�</returns>
        private int GetAccountVacancy(string cardNO, ref decimal vacancy)
        {
            string Sql = string.Empty;
            bool isHaveVacancy = false;
            if (this.Sql.GetCommonSql("Fee.Account.GetVacancy", ref Sql) == -1)
            {
                this.Err = "Ϊ�ҵ�SQL��䣡";

                return -1;
            }
            try
            {
                if (this.ExecQuery(Sql, cardNO) == -1)
                {
                    return -1;
                }

                string state = string.Empty;

                while (this.Reader.Read())
                {
                    vacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                    state = Reader[1].ToString();
                    isHaveVacancy = true;
                }
                this.Reader.Close();
                if (isHaveVacancy)
                {
                    if (state == "0")
                    {
                        this.Err = "���ʻ��Ѿ�ͣ��";
                        return 0;
                    }
                    return 1;
                }
                else
                {
                    this.Err = "�û���δ�����ʻ����ʻ���ע��";
                    return 0;
                }
            }
            catch (Exception ex)
            {
                this.Err = "����ʻ����ʧ�ܣ�" + ex.Message;

                return -1;
            }
        }
        /// <summary>
        /// �õ��ʻ����NEW// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="vacancy">�ʻ����</param>
        /// <returns>-1 ʧ�� 0��û���ʻ����ʻ�ͣ�û��ʻ��ѱ�ע�� 1�ɹ�</returns>
        public FS.HISFC.Models.Account.Account GetAccountVacancyEX(string cardNO)
        {
            FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
            string Sql = string.Empty;
            bool isHaveVacancy = false;
            if (this.Sql.GetCommonSql("Fee.Account.GetVacancy.1", ref Sql) == -1)
            {
                this.Err = "Ϊ�ҵ�SQL��䣡";

                return null;
            }
            try
            {
                if (this.ExecQuery(Sql, cardNO) == -1)
                {
                    return null;
                }

                string state = string.Empty;

                while (this.Reader.Read())
                {
                    account.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                    account.DonateVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                    account.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    account.Limit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    account.BaseAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    account.DonateAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    account.CouponAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    account.AccountLevel.ID = this.Reader[7].ToString();// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    account.AccountLevel.Name = this.Reader[8].ToString();// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    if (this.Reader[9].ToString() == "1")
                    {
                        account.ValidState = EnumValidState.Valid;
                    }
                    else
                    {
                        account.ValidState = EnumValidState.Invalid;
                    }
                    isHaveVacancy = true;
                }
                this.Reader.Close();
                if (isHaveVacancy)
                {
                    if (state == "0")
                    {
                        this.Err = "���ʻ��Ѿ�ͣ��";
                        return null;
                    }
                    return account;
                }
                else
                {
                    this.Err = "�û���δ�����ʻ����ʻ���ע��";
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.Err = "����ʻ����ʧ�ܣ�" + ex.Message;

                return null;
            }
        }
        /// <summary>
        /// ��ȡ����Ȩ�������
        /// </summary>
        /// <param name="empowerCardNO">����Ȩ���￨��</param>
        /// <returns>1�ɹ� 0�����ڿ��õ���Ȩ��Ϣ��-1�����ڱ���Ȩ��Ϣ</returns>
        private int GetEmpowerVacancy(string empowerCardNO, ref decimal vacancy)
        {
            AccountEmpower accountEmpower = new AccountEmpower();
            int resultValue = QueryAccountEmpowerByEmpwoerCardNO(empowerCardNO, ref accountEmpower);
            if (resultValue == 1)
            {
                vacancy = accountEmpower.Vacancy;
            }
            return resultValue;
        }

        /// <summary>
        /// ��ѯ����֤����Ϣ
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList GetPatientIdenInfo(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }
            ArrayList al = new ArrayList();
            NeuObject obj = null;
            while (this.Reader.Read())
            {
                obj = new NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
                obj.User01 = this.Reader[2].ToString();
                obj.User02 = this.Reader[3].ToString();
                al.Add(obj);
            }
            return al;
        }

        #endregion

        #region ��ѯ������Ϣ
        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="markNO">���￨��</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string markNO)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatientByMarkNO", ref strSql) == -1) return null;
            strSql = string.Format(strSql, markNO);
            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            if (list == null || list.Count == 0) return null;
            return list[0];
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="IDCard">���֤��</param>
        /// <returns></returns>
        public int GetPatientInfoByIDCard(string IDCard)
        {
            try
            {
                string strSql = string.Empty;
                if (this.Sql.GetCommonSql("Fee.Account.SelectPatientByIDCard", ref strSql) == -1) return 0;
                strSql = string.Format(strSql, IDCard);
                int i = 0;
                if (this.ExecQuery(strSql) == -1)
                {
                    return -1;
                }
                while (this.Reader.Read())
                {
                    if (!Reader.IsDBNull(0)) i = NConvert.ToInt32(Reader[0]); //���￨��
                }
                return i;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// ����ID�Ų��������
        /// </summary>
        /// <param name="IDCard">���֤</param>
        /// <returns></returns>
        public string GetCardNoByIDCard(string IDCard)
        {
            try
            {
                string strSql = string.Empty;
                if (this.Sql.GetCommonSql("Fee.Account.QeuryCardNoByIDCard", ref strSql) == -1) return string.Empty;
                strSql = string.Format(strSql, IDCard);
                string CardNo = string.Empty;
                if (this.ExecQuery(strSql) == -1)
                {
                    return string.Empty;
                }
                while (this.Reader.Read())
                {
                    if (!Reader.IsDBNull(0)) CardNo = Reader[0].ToString(); //���￨��
                }
                return CardNo;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return string.Empty;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }
        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="CardNO">���￨��</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfoByCardNO(string CardNO)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatientByCardNO", ref strSql) == -1) return null;
            strSql = string.Format(strSql, CardNO);
            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            if (list == null || list.Count == 0) return null;
            return list[0];
        }

        /// <summary>
        /// �޸Ļ��ߺ�ͬ��λ{}
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="pactName"></param>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public int UpdatePatientPactByCardNO(string pactCode, string pactName, string cardNO)
        {
            string strSql = @" update com_patientinfo set pact_code='{0}',pact_name='{1}' where card_no = '{2}' ";
            strSql = string.Format(strSql, pactCode, pactName, cardNO);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCardInDays(string operCode, string days)
        {
            string sqlWhere = string.Empty;
            string strSql = string.Empty;
            if (!string.IsNullOrEmpty(operCode) && !string.IsNullOrEmpty(days))
            {// {3218011F-CCDA-49f3-B468-06F25B3F7F72}
                sqlWhere += " and (tt.createoper = '" + operCode + "' or 'ALL' = '" + operCode + "') and tt.createdate > sysdate - '" + days + "' ";
            }
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatient", ref strSql) == -1)
            {
                this.Err = "��������ΪFee.Account.SelectPatient��Sql���ʧ�ܣ�";
                return null;
            }
            strSql += sqlWhere + "  and tt.state = 1 order by tt.createdate";
            List<FS.HISFC.Models.Account.AccountCard> list = GetAccountCardList(strSql);
            return list;


        }
        //{63F68506-F49D-4ed5-92BD-28A52AF54626}
        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="sex">�Ա�</param>
        /// <param name="pact">��ͬ��λ</param>
        /// <param name="caseNO">������</param>
        /// <param name="idenType">֤������</param>
        /// <param name="idenNo">֤����</param>
        /// <param name="ssNO">ҽ��֤��</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCard(string name, string sex, string pact, string caseNO, string idenType, string idenNo, string ssNO)
        {

            return this.GetAccountCard(name, sex, pact, caseNO, idenType, idenNo, ssNO, "", "");
        }


        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="sex">�Ա�</param>
        /// <param name="pact">��ͬ��λ</param>
        /// <param name="caseNO">������</param>
        /// <param name="idenType">֤������</param>
        /// <param name="idenNo">֤����</param>
        /// <param name="ssNO">ҽ��֤��</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCardEX(string name, string sex, string pact, string caseNO, string idenType, string idenNo, string ssNO, string phone, string cardNo)
        {

            return this.GetAccountCard(name, sex, pact, caseNO, idenType, idenNo, ssNO, phone, cardNo);
        }

        #region 20161118����

        //{B062ABDC-7545-4e5d-A9F5-DCBF217052F9}
        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="sex">�Ա�</param>
        /// <param name="pact">��ͬ��λ</param>
        /// <param name="caseNO">������</param>
        /// <param name="idenType">֤������</param>
        /// <param name="idenNo">֤����</param>
        /// <param name="ssNO">ҽ��֤��</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCard(string name, string homePhone, string mobile, string idenType, string idenNo, string ssNO)
        {

            string sqlWhere = string.Empty;
            bool isInput = false;
            if (name != null && name != string.Empty)
            {
                sqlWhere += " and t.NAME = '" + name + "' ";
                isInput = true;
            }

            if (idenNo != null && idenNo != string.Empty)
            {
                sqlWhere += " and t.IDENNO = '" + idenNo + "'";
                isInput = true;
            }

            if (!string.IsNullOrEmpty(ssNO))
            {
                sqlWhere += " and t.MCARD_NO = '" + ssNO + "'";
                isInput = true;
            }

            if (!string.IsNullOrEmpty(homePhone))
            {
                sqlWhere += " and t.HOME_TEL = '" + homePhone + "'";
                isInput = true;
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                sqlWhere += " and t.MOBILE = '" + mobile + "'";
                isInput = true;
            }

            if (!isInput)
            {
                this.Err = "�����뻼����Ϣ��";
                return null;
            }
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatient", ref strSql) == -1)
            {
                this.Err = "��������ΪFee.Account.SelectPatient��Sql���ʧ�ܣ�";
                return null;
            }
            strSql += sqlWhere + " order by t.card_no";
            List<FS.HISFC.Models.Account.AccountCard> list = GetAccountCardList(strSql);
            return list;
        }

        #endregion

        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="sex">�Ա�</param>
        /// <param name="pact">��ͬ��λ</param>
        /// <param name="caseNO">������</param>
        /// <param name="idenType">֤������</param>
        /// <param name="idenNo">֤����</param>
        /// <param name="ssNO">ҽ��֤��</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCard(string name, string sex, string pact, string caseNO, string idenType, string idenNo, string ssNO, string phone, string cardNo)
        {
            string sqlWhere = string.Empty;
            bool isInput = false;

            //����
            if (!string.IsNullOrEmpty(name))
            {
                sqlWhere += " and t.NAME = '" + name + "' ";
                //sqlWhere += " and t.NAME like '%" + name + "%' ";
                isInput = true;
            }

            //�Ա�
            if (!string.IsNullOrEmpty(sex))
            {
                sqlWhere += " and t.SEX_CODE  = '" + sex + "' ";
                isInput = true;
            }

            //ȥ����ͬ������
            //if (pact != null && pact != string.Empty)
            //{
            //    sqlWhere += " and t.PACT_CODE  = '" + pact + "' ";
            //    isInput = true;
            //}

            //������
            if (!string.IsNullOrEmpty(caseNO))
            {
                sqlWhere += " and t.CASE_NO = '" + caseNO + "' ";
                isInput = true;
            }

            //֤������
            if (!string.IsNullOrEmpty(idenType))
            {
                sqlWhere += " and t.IDCARDTYPE = '" + idenType + "' ";
                isInput = true;
            }

            //֤������
            if (!string.IsNullOrEmpty(idenNo))
            {
                sqlWhere += " and t.IDENNO = '" + idenNo + "'";
                isInput = true;
            }

            //ҽ��֤��
            if (!string.IsNullOrEmpty(ssNO))
            {
                sqlWhere += " and t.MCARD_NO = '" + ssNO + "'";
                isInput = true;
            }

            //��ϵ�绰
            if (!string.IsNullOrEmpty(phone))
            {
                sqlWhere += " and t.HOME_TEL = '" + phone + "'";
                isInput = true;
            }

            if (!string.IsNullOrEmpty(cardNo))
            {
                sqlWhere += " and t.CARD_NO = '" + cardNo + "'";
                isInput = true;
            }
            if (!isInput)
            {
                this.Err = "�����뻼����Ϣ��";
                return null;
            }
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatient", ref strSql) == -1)
            {
                this.Err = "��������ΪFee.Account.SelectPatient��Sql���ʧ�ܣ�";
                return null;
            }
            //sqlWhere = sqlWhere.Substring(0, sqlWhere.LastIndexOf("and") - 1);
            strSql += sqlWhere + " order by t.card_no";
            //List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            List<FS.HISFC.Models.Account.AccountCard> list = GetAccountCardList(strSql);
            return list;
        }
        /// <summary>
        /// ��ȡ�˻���Ϣ
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.Account.AccountCard> MyGetAccountCard(String whereSql, params string[] parms)
        {
            string selectSql = string.Empty;
            string sql = "";
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatient", ref selectSql) == -1)
            {
                this.Err = "��������ΪFee.Account.SelectPatient��Sql���ʧ�ܣ�";
                return null;
            }
            if (this.Sql.GetCommonSql(whereSql, ref whereSql) == -1)
            {
                this.Err = "��������Ϊ" + whereSql + "��Sql���ʧ�ܣ�";
                return null;
            }

            selectSql = selectSql + whereSql;

            sql = string.Format(selectSql, parms);

            return GetAccountCardList(sql);
        }

        /// <summary>
        /// ���ݿ��ţ��������ʶ)���ҿ���¼
        /// </summary>
        /// <param name="normalNo"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCardByNormalNo(string normalNo)
        {
            return MyGetAccountCard("Fee.Account.QueryByNormalNo", normalNo);
        }

        /// <summary>
        /// ���һ�����Ϣ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sex"></param>
        /// <param name="pact"></param>
        /// <param name="idenType"></param>
        /// <param name="idenNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> QueryPatient(string name, string sex, string pact, string idenType, string idenNo, string cardNo, string phone)
        {
            string sqlWhere = string.Empty;
            bool isInput = false;
            if (name != null && name != string.Empty)
            {
                sqlWhere += " and t.NAME like '%" + name + "%' ";
                isInput = true;
            }
            if (sex != null && sex != string.Empty && (sex == "M" || sex == "F"))
            {
                sqlWhere += " and t.SEX_CODE  = '" + sex + "' ";
                isInput = true;
            }
            if (pact != null && pact != string.Empty)
            {
                sqlWhere += " and t.PACT_CODE  = '" + pact + "' ";
                isInput = true;
            }

            if (idenType != null && idenType != string.Empty)
            {
                sqlWhere += " and t.IDCARDTYPE = '" + idenType + "' ";
                isInput = true;
            }
            if (idenNo != null && idenNo != string.Empty)
            {
                sqlWhere += " and t.IDENNO = '" + idenNo + "'";
                isInput = true;
            }
            if (cardNo != null && cardNo != string.Empty)
            {
                sqlWhere += " and t.CARD_NO = '" + cardNo + "'";
                isInput = true;
            }
            if (phone != null && phone != string.Empty)
            {
                sqlWhere += " and t.HOME_TEL = '" + phone + "'";
                isInput = true;
            }
            if (!isInput)
            {
                this.Err = "�����뻼����Ϣ��";
                return null;
            }
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.PatientInfo", ref strSql) == -1)
            {
                this.Err = "��������Ϊ Fee.Account.PatientInfo ��Sql���ʧ�ܣ�";
                return null;
            }
            strSql += sqlWhere + " order by t.card_no";

            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            return list;
        }

        /// <summary>
        /// ���Ҷ�ʧ���Ļ�����Ϣ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="sex">�Ա�</param>
        /// <param name="pact">��ͬ��λ</param>
        /// <param name="idenType">֤������</param>
        /// <param name="idenNo">֤����</param>
        /// <param name="cardNo">���￨��</param>
        /// <param name="phone">ҽ��֤��</param>
        /// <returns></returns>
        public List<AccountCard> GetLostAccountCard(string cardNo)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.PatientCard", ref strSql) == -1)
            {
                this.Err = "��������Ϊ Fee.Account.SelectPatient ��Sql���ʧ�ܣ�";
                return null;
            }

            try
            {
                strSql = string.Format(strSql, cardNo);

                if (this.ExecQuery(strSql) == -1) return null;

                AccountCard card = null;
                List<AccountCard> lstCard = new List<AccountCard>();
                while (this.Reader.Read())
                {
                    card = new AccountCard();
                    card.Patient.PID.CardNO = this.Reader[0].ToString().Trim();
                    card.MarkNO = this.Reader[1].ToString().Trim();
                    card.MarkType.ID = this.Reader[2].ToString().Trim();
                    card.MarkStatus = (MarkOperateTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    card.ReFlag = this.Reader[4].ToString().Trim();
                    card.CreateOper.ID = this.Reader[5].ToString().Trim();
                    card.CreateOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                    card.StopOper.ID = this.Reader[7].ToString().Trim();
                    card.StopOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                    card.BackOper.ID = this.Reader[9].ToString().Trim();
                    card.BackOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10]);
                    card.SecurityCode = this.Reader[11].ToString().Trim();

                    lstCard.Add(card);

                }

                return lstCard;

            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                return null;
            }
        }
        /// <summary>
        /// ��ȡָ����ͬ��λ
        /// </summary>
        /// <param name="lstPact"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> QueryPatientByPact(List<string> lstPact)
        {
            if (lstPact == null || lstPact.Count <= 0)
            {
                return null;
            }

            string sqlWhere = " and pact_code in ('";

            foreach (string pact in lstPact)
            {
                sqlWhere += pact + "', '";
            }
            sqlWhere = sqlWhere.Trim(new char[] { '\'' });
            sqlWhere = sqlWhere.Trim();
            sqlWhere = sqlWhere.Trim(new char[] { ',' });
            sqlWhere += ")";

            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.PatientInfo", ref strSql) == -1)
            {
                this.Err = "��������Ϊ Fee.Account.PatientInfo ��Sql���ʧ�ܣ�";
                return null;
            }

            strSql = strSql + " " + sqlWhere + " order by card_no";

            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            return list;
        }
        /// <summary>
        /// ��ѯ���ߵĻ�����Ϣ// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
        /// </summary>
        /// <param name="IDCardNO"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <param name="SexCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> QueryPatientInfoByWhere(string IDCardNO, string Name, string Phone, string SexCode)
        {
            string strSql = @"
                    SELECT card_no,   --���￨��
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
                           ic_cardno, --���Ժ�
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
                           oper_date,   --��������
                           IS_ENCRYPTNAME, --�Ƿ��������
                           NORMALNAME, --���� 
                           IDCARDTYPE, --֤������
                           CRMID  --{67CE2526-5E7F-4c92-911F-56CA0077679A}
                      FROM com_patientinfo
                      where IS_VALID = '1' ";

            if (!string.IsNullOrEmpty(IDCardNO))
            {
                strSql += "  and IDENNO = '" + IDCardNO + "' ";
            }

            if (!string.IsNullOrEmpty(Name))
            {
                strSql += "  and NAME = '" + Name + "' ";
            }
            if (!string.IsNullOrEmpty(Phone))
            {
                strSql += "  and HOME_TEL = '" + Phone + "' ";
            }
            if (!string.IsNullOrEmpty(SexCode))
            {
                strSql += "  and SEX_CODE = '" + SexCode + "' ";
            }
            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);

            return list;
        }


        /// <summary>
        /// ��ѯ���ߵĻ�����Ϣ// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
        /// </summary>
        /// <param name="IDCardNO"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <param name="SexCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> QueryPatientInfoByLinkPhone(string Phone)
        {
            string strSql = @"
                    SELECT card_no,   --���￨��
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
                           ic_cardno, --���Ժ�
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
                           oper_date,   --��������
                           IS_ENCRYPTNAME, --�Ƿ��������
                           NORMALNAME, --���� 
                           IDCARDTYPE, --֤������
                           CRMID
                      FROM com_patientinfo
                      where IS_VALID = '1' ";

            if (!string.IsNullOrEmpty(Phone))
            {
                strSql += "  and linkman_tel = '" + Phone + "' ";
            }

            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);

            return list;
        }
        #endregion

        #region �ʻ�������

        #region ���뿨������¼
        /// <summary>
        /// ���뿨������¼
        /// </summary>
        /// <param name="accountCardRecord">��������¼ʵ��</param>
        /// <returns></returns>
        public int InsertAccountCardRecord(FS.HISFC.Models.Account.AccountCardRecord accountCardRecord)
        {
            string[] args = null;
            try
            {
                args = new string[] { accountCardRecord.MarkNO,
                                accountCardRecord.MarkType.ID.ToString(),
                                accountCardRecord.CardNO,
                                accountCardRecord.OperateTypes.ID.ToString(),
                                accountCardRecord.Oper.ID.ToString(),
                                accountCardRecord.CardMoney.ToString()};
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.UpdateSingTable("Fee.Account.InsetAccountCardRecord", args);
        }

        #endregion

        #region ���ݻ������￨�Ų��ҿ���Ϣ

        /// <summary>
        /// ���Ҿ��￨��Ϣ
        /// </summary>
        /// <param name="markNO">������</param>
        /// <param name="markType">������</param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.AccountCard GetAccountCard(string markNO, string markType)
        {
            //��������ȡǰ16λ
            if (markType.Trim() == "Health_CARD")
            {
                markNO = markNO.Substring(0, 16);
            }
            if (!string.IsNullOrEmpty(markNO))
            {
                string numStr = markNO.Substring(0, 1);// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                if (!System.Text.RegularExpressions.Regex.IsMatch(numStr, @"^\d+$"))
                {
                    markNO = markNO.Substring(1, markNO.Length - 1);
                }
            }
            List<AccountCard> list = this.GetAccountMarkList("Fee.Account.SelectAccountCardWhere3", markNO, markType);
            if (list == null || list.Count == 0) return null;
            return list[0];
        }
        /// <summary>
        /// �������û�Ա��Ϣ��������һ��// {9EE79BEB-608C-4bc1-991E-7F5E197A326C}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.AccountCard GetAccountCardForOne(string cardNo)
        {
            List<AccountCard> list = this.GetAccountMarkList("Fee.Account.SelectAccountCardWhere5", cardNo);
            if (list == null || list.Count == 0) return null;
            return list[0];
        }
        /// <summary>
        /// ���Ҿ��￨��Ϣ
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetMarkList(string cardNO)
        {
            return this.GetAccountMarkList("Fee.Account.SelectAccountCardWhere2", cardNO);

        }

        /// <summary>
        /// ���Ҿ��￨��Ϣ
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="state">״̬ 0ͣ�á�1����</param>
        /// <returns></returns>
        public List<AccountCard> GetMarkList(string cardNO, bool state)
        {
            return this.GetAccountMarkList("Fee.Account.SelectAccountCardWhere4", cardNO, (NConvert.ToInt32(state)).ToString());
        }

        /// <summary>
        /// ���Ҿ��￨��Ϣ
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="markType">������ Allȫ��</param>
        /// <param name="state">״̬ 0ͣ�� 1���� Allȫ��</param>
        /// <returns></returns>
        public List<AccountCard> GetMarkList(string cardNO, string markType, string state)
        {
            //return this.GetMarkList("Fee.Account.SelectAccountCardWhere5", cardNO, markType, (NConvert.ToInt32(state)).ToString());
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountMarkNO", ref sqlStr) == -1)
            {
                this.Err = "��������ΪFee.Account.SelectAccountMarkNO��SQL���ʧ�ܣ�";
                return null;
            }
            List<AccountCard> list = new List<AccountCard>();
            AccountCard tempCard = null;
            try
            {
                sqlStr = string.Format(sqlStr, cardNO, markType, state);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return null;
                }

                string strTemp = string.Empty;
                while (this.Reader.Read())
                {
                    tempCard = new AccountCard();
                    tempCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    tempCard.MarkNO = this.Reader[1].ToString();
                    tempCard.MarkType.ID = this.Reader[2].ToString();

                    //{6036F4C6-9452-4f21-8634-940AACD4B296}
                    tempCard.AccountLevel.ID = this.Reader[12].ToString();

                    strTemp = this.Reader[3].ToString();
                    if (strTemp == "0")
                    {
                        tempCard.MarkStatus = MarkOperateTypes.Cancel;
                    }
                    else if (strTemp == "1")
                    {
                        tempCard.MarkStatus = MarkOperateTypes.Begin;
                    }
                    else if (strTemp == "2")
                    {
                        tempCard.MarkStatus = MarkOperateTypes.Stop;
                    }

                    tempCard.ReFlag = this.Reader[4].ToString().Trim();
                    tempCard.CreateOper.ID = this.Reader[5].ToString().Trim();
                    tempCard.CreateOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                    tempCard.StopOper.ID = this.Reader[7].ToString().Trim();
                    tempCard.StopOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                    tempCard.BackOper.ID = this.Reader[9].ToString().Trim();
                    tempCard.BackOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10]);

                    tempCard.SecurityCode = this.Reader[11].ToString().Trim();

                    list.Add(tempCard);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��������ʧ�ܣ�" + ex.Message;
                return null;
            }
            return list;
        }

        /// <summary>
        /// ���Ҿ��￨��Ϣ
        /// </summary>
        /// <param name="idCardType"></param>
        /// <param name="idenno"></param>
        /// <param name="markType"></param>
        /// <returns></returns>
        public List<AccountCard> GetMarkListFromIdenno(string idCardType, string idenno, string markType)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountCardFromIdenno", ref sqlStr) == -1)
            {
                this.Err = "��������ΪFee.Account.SelectAccountCardFromIdenno��SQL���ʧ�ܣ�";
                return null;
            }
            List<AccountCard> list = new List<AccountCard>();
            AccountCard tempCard = null;
            try
            {
                sqlStr = string.Format(sqlStr, idCardType, idenno, markType);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return null;
                }
                while (this.Reader.Read())
                {
                    tempCard = new AccountCard();
                    tempCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    tempCard.MarkNO = this.Reader[1].ToString();
                    tempCard.MarkType.ID = this.Reader[2].ToString();
                    if (this.Reader[3].ToString() == "1")
                    {
                        tempCard.IsValid = true;
                    }
                    else
                    {
                        tempCard.IsValid = false;
                    }
                    list.Add(tempCard);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��������ʧ�ܣ�" + ex.Message;
                return null;
            }
            return list;
        }

        #endregion

        #region ���������ʻ�������
        /// <summary>
        /// ���������ʻ�������
        /// </summary>
        /// <param name="accountCard"></param>
        /// <returns></returns>
        public int InsertAccountCard(FS.HISFC.Models.Account.AccountCard accountCard)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.InsertAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql,
                                    accountCard.Patient.PID.CardNO, //���￨��
                                    accountCard.MarkNO,//��ݱ�ʶ����
                                    accountCard.MarkType.ID.ToString(),//��ݱ�ʶ����� 1�ſ� 2IC�� 3���Ͽ�
                                    FS.FrameWork.Function.NConvert.ToInt32(accountCard.MarkStatus).ToString(), //״̬'1'����'0'ͣ�� 
                                    accountCard.ReFlag,
                                    accountCard.CreateOper.ID,
                                    accountCard.CreateOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    accountCard.SecurityCode
                                    );
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
        /// ���������ʻ�������NEW // {AE74D7CC-B573-48a7-9EAD-60340E3F01C9}
        /// </summary>
        /// <param name="accountCard"></param>
        /// <returns></returns>
        public int InsertAccountCardEX(FS.HISFC.Models.Account.AccountCard accountCard)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.InsertAccountCard.1", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql,
                                    accountCard.Patient.PID.CardNO, //���￨��
                                    accountCard.MarkNO,//��ݱ�ʶ����
                                    accountCard.MarkType.ID.ToString(),//��ݱ�ʶ����� 1�ſ� 2IC�� 3���Ͽ�
                                    FS.FrameWork.Function.NConvert.ToInt32(accountCard.MarkStatus).ToString(), //״̬'1'����'0'ͣ�� 
                                    accountCard.ReFlag,
                                    accountCard.CreateOper.ID,
                                    accountCard.CreateOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    accountCard.SecurityCode,
                                    accountCard.AccountLevel.ID,
                                    accountCard.BegTime.ToString(),
                                    accountCard.EndTime.ToString()
                                    );
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

        #region ���¿�״̬
        /// <summary>
        /// ���¿�״̬
        /// </summary>
        /// <param name="markNO">������</param>
        /// <param name="type">������</param>
        /// <param name="valid">״̬</param>
        /// <returns></returns>
        public int UpdateAccountCardState(string markNO, NeuObject markType, bool valid)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountCardState", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, markNO, NConvert.ToInt32(valid).ToString(), markType.ID);
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
        /// ���¿���Ϣ
        /// </summary>
        /// <param name="markNO"></param>
        /// <param name="markType"></param>
        /// <param name="levelCode"></param>
        /// <param name="begTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int UpdateAccountCardInfo(string markNO, NeuObject markType, string levelCode, DateTime begTime, DateTime endTime)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, markNO, markType.ID, levelCode, begTime.ToString(), endTime.ToString());
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
        /// ��������// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        public int UpdateCouponForPay(string cardNo, decimal money, string invoiceNo)
        {
            CounponOperTypes operType = CounponOperTypes.Pay;
            if (money < 0)
            {
                operType = CounponOperTypes.Quit;
            }
            else
            {
                operType = CounponOperTypes.Pay;
            }
            return this.UpdateCoupon(cardNo, money, invoiceNo, "", "", operType);
        }

        /// <summary>
        /// ���»�����Ϣ// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="exchangeGoods"></param>
        /// <param name="mark"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        public int UpdateCoupon(string cardNo, decimal money, string invoiceNo, string exchangeGoods, string mark, CounponOperTypes operType)
        {
            //if (money == 0)
            //{
            //    this.Err = "���Ϊ0��";
            //    return -1;
            //}
            if (string.IsNullOrEmpty(cardNo))
            {
                this.Err = "����Ϊ�գ�";
                return -1;
            }

            string couponOperType = "";
            decimal coupon = 0m;
            decimal couponAccumulate = 0m;
            if (operType == CounponOperTypes.Quit)
            {
                couponOperType = "0";
                if (money > 0)
                {
                    money = -money;
                }
                CardCoupon cardCoupon2 = new CardCoupon();
                cardCoupon2 = this.QueryCardCouponByCardNo(cardNo);
                if (string.IsNullOrEmpty(cardCoupon2.CardNo))
                {
                    this.Err = "û�иû��ߵĻ����˻���Ϣ��";
                    return -1;
                }
                if (cardCoupon2.CouponVacancy < -money)
                {
                    if (System.Windows.Forms.MessageBox.Show("���ֲ��㣡Ӧ�˻���Ϊ��" + -money + "�����˻���Ϊ��" + cardCoupon2.CouponVacancy
                        + "\r\n�Ƿ������", "ϵͳ��ʾ", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                    {

                        this.Err = "���ֲ��㣡";
                        return -1;
                    }
                    money = -cardCoupon2.CouponVacancy;
                }
                coupon = money;
                couponAccumulate = money;

            }
            else if (operType == CounponOperTypes.Pay)
            {
                couponOperType = "1";
                if (money < 0)
                {
                    money = -money;
                }
                coupon = money;
                couponAccumulate = money;
            }
            else if (operType == CounponOperTypes.Exc)
            {
                couponOperType = "2";
                couponAccumulate = 0m;
                if (money > 0)
                {
                    money = -money;
                }
                coupon = money;
                CardCoupon cardCoupon2 = new CardCoupon();
                cardCoupon2 = this.QueryCardCouponByCardNo(cardNo);
                if (string.IsNullOrEmpty(cardCoupon2.CardNo))
                {
                    this.Err = "û�иû��ߵĻ����˻���Ϣ��";
                    return -1;
                }

                if (cardCoupon2.CouponVacancy < -money)
                {
                    this.Err = "���ֲ��㣡";
                    return -1;
                }

                money = 0;
            }

            CardCoupon cardCoupon = new CardCoupon();

            cardCoupon.CardNo = cardNo;
            cardCoupon.Coupon = coupon;
            cardCoupon.CouponAccumulate = couponAccumulate;

            if (this.UpdateCardCoupon(cardCoupon) <= 0)
            {
                return -1;
            }
            CardCoupon cardCoupon1 = new CardCoupon();
            cardCoupon1 = this.QueryCardCouponByCardNo(cardCoupon.CardNo);

            CardCouponRecord cardCouponRecord = new CardCouponRecord();
            cardCouponRecord.CardNo = cardNo;
            cardCouponRecord.Money = money;
            cardCouponRecord.Coupon = coupon;
            cardCouponRecord.CouponVacancy = cardCoupon1.CouponVacancy;
            cardCouponRecord.InvoiceNo = invoiceNo;
            cardCouponRecord.ExchangeGoods = exchangeGoods;
            cardCouponRecord.Memo = mark;
            cardCouponRecord.OperEnvironment.ID = this.Operator.ID;
            cardCouponRecord.OperEnvironment.Name = this.Operator.Name;
            cardCouponRecord.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();
            cardCouponRecord.OperType = couponOperType;
            if (this.InsertCardCouponRecord(cardCouponRecord) <= 0)
            {
                return -1;
            }



            return 1;
        }


        #region ��Ĭ���û�������
        //{0EBA6A50-3F87-4e6a-AD8E-66062E90FDA0}

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        public int CouponCostSilence(string cardNo, decimal money, string invoiceNo, string exchangeGoods, ref string vacancy)
        {
            if (string.IsNullOrEmpty(cardNo) || money < 0 || string.IsNullOrEmpty(invoiceNo) || string.IsNullOrEmpty(exchangeGoods))
            {
                this.Err = "������,��Ʊ��,��Ʒ���鲻��Ϊ��,���ѽ���С��0!";
                return -1;
            }
            return this.UpdateCouponSilence(cardNo, money, invoiceNo, exchangeGoods, "���ֶһ�", CounponOperTypes.Exc, ref vacancy);
        }

        /// <summary>
        /// �����˷�
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        //public int CoupoQuitSilence(string cardNo, decimal money, string invoiceNo, string exchangeGoods,ref string vacancy)
        //{
        //    return this.UpdateCouponSilence(cardNo, money, invoiceNo, exchangeGoods, "", CounponOperTypes.Pay, ref vacancy);
        //}

        /// <summary>
        /// ���»�����Ϣ// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="exchangeGoods"></param>
        /// <param name="mark"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        private int UpdateCouponSilence(string cardNo, decimal money, string invoiceNo, string exchangeGoods, string mark, CounponOperTypes operType, ref string vacancy)
        {
            if (string.IsNullOrEmpty(cardNo))
            {
                this.Err = "����Ϊ�գ�";
                return -1;
            }

            string couponOperType = "";
            decimal coupon = 0m;
            decimal couponAccumulate = 0m;
            if (operType == CounponOperTypes.Exc)
            {
                couponOperType = "2";
                couponAccumulate = 0m;
                coupon = -money;
                CardCoupon cardCoupon = new CardCoupon();
                cardCoupon = this.QueryCardCouponByCardNo(cardNo);
                if (string.IsNullOrEmpty(cardCoupon.CardNo))
                {
                    this.Err = "û�иû��ߵĻ����˻���Ϣ��";
                    return -1;
                }

                if (cardCoupon.CouponVacancy < money)
                {
                    this.Err = "���ֲ��㣡";
                    return -1;
                }

                vacancy = Math.Round((cardCoupon.CouponVacancy - money), 2).ToString();
            }
            else
            {
                this.Err = "��֧�ֵĲ������ͣ�";
                return -1;
            }

            CardCoupon cardCouponForUpdate = new CardCoupon();

            cardCouponForUpdate.CardNo = cardNo;
            cardCouponForUpdate.Coupon = coupon;
            cardCouponForUpdate.CouponAccumulate = couponAccumulate;

            if (this.UpdateCardCoupon(cardCouponForUpdate) <= 0)
            {
                return -1;
            }

            CardCoupon cardCouponForRecord = new CardCoupon();
            cardCouponForRecord = this.QueryCardCouponByCardNo(cardCouponForUpdate.CardNo);

            CardCouponRecord cardCouponRecord = new CardCouponRecord();
            cardCouponRecord.CardNo = cardNo;
            cardCouponRecord.Money = -money;
            cardCouponRecord.Coupon = coupon;
            cardCouponRecord.CouponVacancy = cardCouponForRecord.CouponVacancy;
            cardCouponRecord.InvoiceNo = invoiceNo;
            cardCouponRecord.ExchangeGoods = exchangeGoods;
            cardCouponRecord.Memo = mark;
            cardCouponRecord.OperEnvironment.ID = this.Operator.ID;
            cardCouponRecord.OperEnvironment.Name = this.Operator.Name;
            cardCouponRecord.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();
            cardCouponRecord.OperType = couponOperType;
            if (this.InsertCardCouponRecord(cardCouponRecord) <= 0)
            {
                return -1;
            }

            return 1;
        }

        #endregion

        /// <summary>
        /// ���»���// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="coupon"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        public int UpdateCardCoupon(CardCoupon cardCoupon)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateCoupon", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, cardCoupon.CardNo, cardCoupon.Coupon.ToString("F2"), cardCoupon.CouponAccumulate.ToString("F2"));
                int i = this.ExecNoQuery(Sql);
                if (i <= 0)
                {
                    return this.InsertCardCoupon(cardCoupon);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return 1;

        }
        #endregion

        #region ���¿�״̬
        /// <summary>
        /// ���¿�״̬
        /// </summary>
        /// <param name="markNO">������</param>
        /// <param name="markType">������</param>
        /// <param name="state">״̬</param>
        /// <returns></returns>
        public int UpdateAccountCardState(string markNO, string markType, string state)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountCardState", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, markNO, state, markType);
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
        /// ͣ�ÿ�����
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int StopAccountCard(AccountCard card)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.StopAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, card.MarkNO, card.MarkType.ID, card.Patient.PID.CardNO, ((int)card.MarkStatus).ToString(), card.StopOper.ID, card.StopOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"));
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
        /// �˿�����
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int BackAccountCard(AccountCard card)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.BackAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, card.MarkNO, card.MarkType.ID, card.Patient.PID.CardNO, ((int)card.MarkStatus).ToString(), card.BackOper.ID, card.BackOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"));
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
        /// ͣ���˿����ò���
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int StopBackAccountCard(AccountCard card)
        {
            if (card == null)
            {
                this.Err = "����Ϊ�գ�";
                return -1;
            }

            int iRes = 0;
            if (card.MarkStatus == MarkOperateTypes.Stop)
            {
                iRes = this.StopAccountCard(card);
            }
            else if (card.MarkStatus == MarkOperateTypes.Cancel)
            {
                iRes = this.BackAccountCard(card);
            }
            else if (card.MarkStatus == MarkOperateTypes.Begin)
            {
                iRes = this.RecoverdAccountCard(card);
            }
            if (iRes == -1)
            {
                return -1;
            }

            AccountCardRecord accountCardRecord = new FS.HISFC.Models.Account.AccountCardRecord();
            //���뿨�Ĳ�����¼
            accountCardRecord.MarkNO = card.MarkNO;
            accountCardRecord.MarkType.ID = card.MarkType.ID;
            accountCardRecord.CardNO = card.Patient.PID.CardNO;
            accountCardRecord.OperateTypes.ID = (int)card.MarkStatus;
            accountCardRecord.Oper.ID = (this.Operator as FS.HISFC.Models.Base.Employee).ID;
            //�Ƿ���ȡ���ɱ���
            accountCardRecord.CardMoney = 0;

            if (this.InsertAccountCardRecord(accountCardRecord) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int RecoverdAccountCard(AccountCard card)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.RecoverCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, card.MarkNO, card.MarkType.ID, card.Patient.PID.CardNO, ((int)card.MarkStatus).ToString());
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

        #region ���¿��ϴ����

        /// <summary>
        /// ���¿��ϴ����
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="markNO"></param>
        /// <param name="markType"></param>
        /// <param name="upLoadFlag"></param>
        /// <returns></returns>
        public int UpdateAccountCardUploadFlag(string cardNo, string markNO, string markType, string upLoadFlag)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountUpLoadFlag", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, cardNo, markNO, markType, upLoadFlag);
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

        #region ���濨���ñ�
        /// <summary>
        /// ���濨���ñ�
        /// </summary>
        /// <param name="cardFee"></param>
        /// <returns></returns>
        public int InsertAccountCardFee(AccountCardFee cardFee)
        {
            if (cardFee == null)
                return -1;

            if (string.IsNullOrEmpty(cardFee.InvoiceNo))
            {
                this.Err = "��Ʊ��ˮ��Ϊ�գ�";

                return -1;
            }
            //Ĭ��֧����ʽ
            if (string.IsNullOrEmpty(cardFee.PayType.ID)) cardFee.PayType.ID = "CA";

            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Insert2", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Insert ��Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql,
                    cardFee.InvoiceNo,
                    ((int)cardFee.TransType).ToString(),
                    cardFee.CardNo,
                    cardFee.MarkNO,
                    cardFee.MarkType.ID,
                    cardFee.Tot_cost,
                    cardFee.FeeOper.ID,
                    cardFee.FeeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    cardFee.Oper.ID,
                    cardFee.Oper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    0,
                    "",
                    "",
                    "",
                    cardFee.IStatus,
                    cardFee.Print_InvoiceNo,
                    ((int)cardFee.FeeType).ToString(),
                    cardFee.ClinicNO,
                    cardFee.Remark,
                    cardFee.Own_cost,
                    cardFee.Pub_cost,
                    cardFee.Pay_cost,
                    cardFee.PayType.ID);
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

        #region ���Ͽ����ñ�
        /// <summary>
        /// ���Ͽ����ñ�����
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="transType"></param>
        /// <param name="feeType"></param>
        /// <returns></returns>
        public int CancelAccountCardFee(string invoice, TransTypes transType, AccCardFeeType feeType)
        {
            if (string.IsNullOrEmpty(invoice))
            {
                this.Err = "��Ʊ��ˮ��Ϊ�գ�";

                return -1;
            }

            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Cancel", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Cancel ��Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql,
                    invoice,
                    ((int)transType).ToString(),
                    ((int)feeType).ToString());
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
        /// �˷ѿ�������Ϣ
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int CancelAccountCardFeeByInvoice(string invoice)
        {
            if (string.IsNullOrEmpty(invoice))
            {
                this.Err = "��Ʊ��ˮ��Ϊ�գ�";

                return -1;
            }

            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Cancel.ByInvoice", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Cancel.ByInvoice ��Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql, invoice);
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
        /// �˷ѿ�������Ϣ
        /// </summary>
        /// <param name="?"></param>
        /// <param name="flag">0����Ч 1����Ч 2:�˷� 3������</param>
        /// <returns></returns>
        public int CancelAccountCardFeeByInvoice(string invoice, int flag)
        {
            if (string.IsNullOrEmpty(invoice))
            {
                this.Err = "��Ʊ��ˮ��Ϊ�գ�";

                return -1;
            }

            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Cancel.ByInvoice.1", ref Sql) == -1)
            {
                #region Ĭ��sql
                Sql = @"update fin_opb_accountcardfee a
   set a.cancel_flag ={1}
 where a.invoice_no = '{0}'";
                #endregion
            }
            try
            {
                Sql = string.Format(Sql, invoice, flag);
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


        #region ��ѯ��������Ϣ
        /// <summary>
        /// ��ѯ��������Ϣ -- ֱ���շѼ�¼
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccCardFeeDirectory(string cardNo, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            if (string.IsNullOrEmpty(cardNo))
            {
                this.Err = "�������ԣ�";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Where.2", ref strWhere) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Where.2 ��Sql���ʧ�ܣ�";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo);

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;

        }
        /// <summary>
        /// ��ѯ�������˵ļ�ͥ��Ա��Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="cardNo">�������˲�����</param>
        /// <param name="validState">��ѯ��Ա��״̬</param>
        /// <param name="accountFamilyInfoList"></param>
        /// <returns></returns>
        public int GetAccountFamilyInfo(string cardNo, string validState, out List<AccountFamilyInfo> accountFamilyInfoList)
        {
            accountFamilyInfoList = null;
            if (string.IsNullOrEmpty(cardNo))
            {
                this.Err = "�������ԣ�";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountFamilyInfo.Where.1", ref strWhere) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.SelectAccountFamilyInfo.Where.1��Sql���ʧ�ܣ�";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo, validState);

                iRes = this.QueryAccountFamilyInfo(strWhere, out accountFamilyInfoList);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;

        }

        /// <summary>
        /// ���ݹ����˵Ŀ��Ų�ѯ�����˵���Ϣ
        /// </summary>
        /// <param name="linkedardNo">�����˲�����</param>
        /// <param name="validState">��ѯ��Ա��״̬</param>
        /// <param name="accountFamilyInfoList"></param>
        /// <returns></returns>
        public int GetLinkedFamilyInfo(string linkedardNo, string validState, out List<AccountFamilyInfo> accountFamilyInfoList)
        {
            accountFamilyInfoList = null;
            if (string.IsNullOrEmpty(linkedardNo))
            {
                this.Err = "�������ԣ�";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountFamilyInfo.Where.2", ref strWhere) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.SelectAccountFamilyInfo.Where.2��Sql���ʧ�ܣ�";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, linkedardNo, validState);

                iRes = this.QueryAccountFamilyInfo(strWhere, out accountFamilyInfoList);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;

        }

        /// <summary>
        /// ���ݼ�ͥ�Ų�ѯ��ͥ��Ա��Ϣ
        /// {793CA9DB-FD85-460a-B8B4-971C31FFAD45}
        /// </summary>
        /// <param name="linkedardNo">�����˲�����</param>
        /// <param name="validState">��ѯ��Ա��״̬</param>
        /// <param name="accountFamilyInfoList"></param>
        /// <returns></returns>
        public int GetFamilyInfoByCode(string familyCode, string validState, out List<AccountFamilyInfo> accountFamilyInfoList)
        {
            accountFamilyInfoList = null;
            if (string.IsNullOrEmpty(familyCode))
            {
                this.Err = "�������ԣ�";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountFamilyInfo.Where.3", ref strWhere) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.SelectAccountFamilyInfo.Where.3��Sql���ʧ�ܣ�";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, familyCode, validState);

                iRes = this.QueryAccountFamilyInfo(strWhere, out accountFamilyInfoList);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;

        }
        /// <summary>
        /// ��ѯ������Ϣ// {E0683A80-F23C-4848-8482-7257F9263221}
        /// </summary>
        /// <param name="begVolumeNo"></param>
        /// <param name="endVolumeNo"></param>
        /// <param name="begTime"></param>
        /// <param name="endTime"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="validState"></param>
        /// <param name="operCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public ArrayList QueryAccountCardVolumeList(string begVolumeNo, string endVolumeNo, DateTime begTime, DateTime endTime, string invoiceNo, string validState, string operCode, string cardNo, string memo)
        {
            ArrayList accountCardVolumeList = new ArrayList();
            begTime = FS.FrameWork.Function.NConvert.ToDateTime(begTime.ToLongDateString() + " 00:00:00");
            endTime = FS.FrameWork.Function.NConvert.ToDateTime(endTime.ToLongDateString() + " 23:59:59");
            string sqlWhere = "";
            if (string.IsNullOrEmpty(validState))
            {
                return null;
            }
            else
            {
                sqlWhere = " Where (VALID = '{0}' or '{0}' = 'ALL') ";
            }

            if (begTime > endTime)
            {
                return null;
            }
            else
            {
                sqlWhere += " And BEG_DATE >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')  And END_DATE <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')  ";
            }

            if (!string.IsNullOrEmpty(begVolumeNo) && string.IsNullOrEmpty(endVolumeNo))
            {
                sqlWhere += " And VOLUME_NO like '%{3}%' ";
            }
            else if (string.IsNullOrEmpty(begVolumeNo) && !string.IsNullOrEmpty(endVolumeNo))
            {
                sqlWhere += " And VOLUME_NO like '%{4}%' ";
            }
            else if (!string.IsNullOrEmpty(begVolumeNo) && !string.IsNullOrEmpty(endVolumeNo))
            {
                sqlWhere += " And VOLUME_NO >= '{3}' And  VOLUME_NO <= '{4}'";
            }

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                sqlWhere += " And INVOICE_NO like '%{5}%' ";
            }
            if (!string.IsNullOrEmpty(operCode))
            {
                sqlWhere += " And OPER_CODE like '%{6}%' ";
            }

            if (!string.IsNullOrEmpty(cardNo))
            {
                sqlWhere += " And CARD_NO like '%{7}%' ";
            }
            if (!string.IsNullOrEmpty(memo))
            {
                sqlWhere += " And MARK like '%{8}%' ";
            }
            sqlWhere += " Order By VOLUME_NO";
            sqlWhere = string.Format(sqlWhere, validState, begTime.ToString(), endTime.ToString(), begVolumeNo, endVolumeNo, invoiceNo, operCode, cardNo, memo);

            accountCardVolumeList = this.QueryAccountCardVolume(sqlWhere);


            return accountCardVolumeList;

        }
        /// <summary>
        /// ��ѯ��������Ϣ -- ָ���Һż�¼
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="clinicNo"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccCardFeeByClinic(string cardNo, string clinicNo, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            if (string.IsNullOrEmpty(cardNo) || string.IsNullOrEmpty(clinicNo))
            {
                this.Err = "�������ԣ�";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Where.3", ref strWhere) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Where.3 ��Sql���ʧ�ܣ�";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo, clinicNo);

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }

        /// <summary>
        /// ��ѯ��Ч��������Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="markNo"></param>
        /// <param name="cardType"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccountCardFee(string cardNo, string markNo, string cardType, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            if (string.IsNullOrEmpty(cardNo) || string.IsNullOrEmpty(markNo) || string.IsNullOrEmpty(cardType))
            {
                this.Err = "�������ԣ�";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Where.1", ref strWhere) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Where.1 ��Sql���ʧ�ܣ�";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo, markNo, cardType);

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }
        /// <summary>
        /// ��ѯ��������Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccountCardFee(string cardNo, out List<AccountCardFee> lstCardFee)
        {
            return QueryAccountCardFee(cardNo, "ALL", "ALL", out lstCardFee);
        }

        /// <summary>
        /// ��ѯ�Һŷ�����Ϣ-ͨ�������ź�ʱ��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public int QueryAccountCardFeeByCardNOAndDate(string cardNo, string begin, string end, ref DataSet dsResult)
        {
            dsResult = new DataSet();

            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Select.4", ref strSql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Select.4 ��Sql���ʧ�ܣ�";
                return -1;
            }

            int iRes = 0;
            try
            {
                strSql = string.Format(strSql, cardNo, begin, end);

                iRes = this.ExecQuery(strSql, ref dsResult);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }

        /// <summary>
        /// ͨ����Ʊ�Ų�ѯ������Ϣ
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccountCardFeeByInvoiceNO(string invoiceNO, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            if (string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(invoiceNO))
            {
                this.Err = "�������ԣ�";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Where.4", ref strWhere) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Where.4 ��Sql���ʧ�ܣ�";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, invoiceNO);

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }
        /// <summary>
        /// ����Card_No��ѯ��Ч��������// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public string QueryCardAountByCardNo(string CardNo)
        {
            string sqlstr = "";
            if (this.Sql.GetCommonSql("Fee.Account.QueryCardSum", ref sqlstr) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.QueryCardSum ��Sql���ʧ�ܣ�";
                return "";
            }
            sqlstr = string.Format(sqlstr, CardNo);
            return this.ExecSqlReturnOne(sqlstr, "");

        }

        /// <summary>
        /// ��ѯ��������Ϣ
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        private int QueryAccountCardFeeSQL(string sql, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;

            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Select", ref strSql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Select ��Sql���ʧ�ܣ�";
                return -1;
            }

            try
            {
                strSql = strSql + sql;

                if (this.ExecQuery(strSql) == -1)
                {
                    return -1;
                }

                lstCardFee = new List<AccountCardFee>();
                AccountCardFee cardFee = null;
                while (this.Reader.Read())
                {
                    cardFee = new AccountCardFee();
                    cardFee.InvoiceNo = this.Reader[0].ToString().Trim();
                    cardFee.TransType = this.Reader[1].ToString().Trim() == "1" ? TransTypes.Positive : TransTypes.Negative;
                    cardFee.MarkNO = this.Reader[2].ToString().Trim();
                    cardFee.MarkType.ID = this.Reader[3].ToString().Trim();
                    cardFee.Tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
                    cardFee.FeeOper.ID = this.Reader[5].ToString().Trim();
                    cardFee.FeeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                    cardFee.Oper.ID = this.Reader[7].ToString().Trim();
                    cardFee.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                    cardFee.IsBalance = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    cardFee.BalanceNo = this.Reader[10].ToString().Trim();
                    cardFee.BalnaceOper.ID = this.Reader[11].ToString().Trim();
                    cardFee.BalnaceOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12]);
                    cardFee.IStatus = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13]);
                    cardFee.CardNo = this.Reader[14].ToString().Trim();

                    cardFee.Print_InvoiceNo = this.Reader[15].ToString().Trim();
                    switch (this.Reader[16].ToString().Trim())
                    {
                        case "1":
                            cardFee.FeeType = AccCardFeeType.CardFee;
                            break;
                        case "2":
                            cardFee.FeeType = AccCardFeeType.CaseFee;
                            break;
                        case "3":
                            cardFee.FeeType = AccCardFeeType.RegFee;
                            break;
                        case "4":
                            cardFee.FeeType = AccCardFeeType.DiaFee;
                            break;
                        case "5":
                            cardFee.FeeType = AccCardFeeType.ChkFee;
                            break;
                        case "6":
                            cardFee.FeeType = AccCardFeeType.AirConFee;
                            break;
                        case "7":
                            cardFee.FeeType = AccCardFeeType.OthFee;
                            break;
                    }
                    cardFee.ClinicNO = this.Reader[17].ToString().Trim();
                    cardFee.Remark = this.Reader[18].ToString().Trim();
                    cardFee.PayType.ID = this.Reader[19].ToString();
                    cardFee.Own_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                    cardFee.Pub_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString());
                    cardFee.Pay_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());

                    cardFee.Oper.Name = this.Reader[23].ToString().Trim();
                    cardFee.MarkType.Name = this.Reader[24].ToString().Trim();

                    lstCardFee.Add(cardFee);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ��ѯ������Ϣ// {E0683A80-F23C-4848-8482-7257F9263221}
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        private ArrayList QueryAccountCardVolume(string sqlWhere)
        {
            ArrayList cardVolumeList = new ArrayList();
            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.AccountCardVolume.Select", ref strSql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Select ��Sql���ʧ�ܣ�";
                return null;
            }

            try
            {
                strSql = strSql + " " + sqlWhere;

                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                FS.HISFC.Models.Account.CardVolume cardVolume = null;
                while (this.Reader.Read())
                {
                    cardVolume = new CardVolume();

                    cardVolume.ID = this.Reader[0].ToString();
                    cardVolume.VolumeNo = this.Reader[1].ToString();
                    cardVolume.Money = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
                    cardVolume.BegTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]);
                    cardVolume.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                    cardVolume.UseType.Name = this.Reader[5].ToString();
                    cardVolume.InvoiceNo = this.Reader[6].ToString();
                    cardVolume.Patient.PID.CardNO = this.Reader[7].ToString();
                    cardVolume.Mark = this.Reader[8].ToString();
                    string validState = this.Reader[9].ToString();
                    if (validState == "0")
                    {
                        cardVolume.ValidState = EnumValidState.Invalid;
                    }
                    else if (validState == "1")
                    {
                        cardVolume.ValidState = EnumValidState.Valid;
                    }
                    cardVolume.CreateEnvironment.ID = this.Reader[10].ToString();
                    cardVolume.CreateEnvironment.Name = this.Reader[11].ToString();
                    cardVolume.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12]);
                    cardVolume.OperEnvironment.ID = this.Reader[13].ToString();
                    cardVolume.OperEnvironment.Name = this.Reader[14].ToString();
                    cardVolume.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15]);

                    cardVolumeList.Add(cardVolume);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return cardVolumeList;
        }

        /// <summary>
        /// ��ѯ�����˻�// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.CardCoupon QueryCardCouponByCardNo(string cardNo)
        {
            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.QueryCoupon", ref strSql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.QueryCoupon ��Sql���ʧ�ܣ�";
                return null;
            }

            strSql = string.Format(strSql, cardNo);
            FS.HISFC.Models.Account.CardCoupon cardCoupon = new CardCoupon();
            try
            {
                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (this.Reader.Read())
                {
                    cardCoupon = new CardCoupon();

                    cardCoupon.ID = this.Reader[0].ToString();
                    cardCoupon.CardNo = this.Reader[1].ToString();
                    cardCoupon.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
                    cardCoupon.CouponAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());

                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return cardCoupon;
        }
        /// <summary>
        /// ��ѯ�һ���¼// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="exchangeGoods"></param>
        /// <param name="mark"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="operCode"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList QueryCardCouponRecord(string cardNo, string exchangeGoods, string mark, string startTime, string endTime, string operCode, string invoiceNo, string couponType)
        {
            string sqlWhere = "";
            if (!string.IsNullOrEmpty(cardNo))
            {
                sqlWhere += " and a.card_no = '" + cardNo + "'";
            }

            if (!string.IsNullOrEmpty(exchangeGoods))
            {
                sqlWhere += " and a.EXCHANGE_GOODS = '" + exchangeGoods + "'";
            }

            if (!string.IsNullOrEmpty(mark))
            {
                sqlWhere += " and a.REMARK = '" + mark + "'";
            }

            if (!string.IsNullOrEmpty(startTime))
            {
                sqlWhere += " and a.OPER_DATE >= to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')";
            }

            if (!string.IsNullOrEmpty(endTime))
            {
                sqlWhere += " and a.OPER_DATE <= to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')";
            }

            if (!string.IsNullOrEmpty(operCode))
            {
                sqlWhere += " and a.OPER_CODE = '" + operCode + "' or 'ALL' = '" + operCode + "'";
            }

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                sqlWhere += " and a.INVOICE_NO like '%" + invoiceNo + "'";
            }

            if (!string.IsNullOrEmpty(couponType))
            {
                sqlWhere += " and a.OPERTYPE = '" + couponType + "'";
            }
            return QueryCardCouponRecord(sqlWhere);
        }

        /// <summary>
        /// ��ѯ�����˻�// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private ArrayList QueryCardCouponRecord(string sqlWhere)
        {
            ArrayList cardCouponRecordList = new ArrayList();
            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.QueryCouponRecord", ref strSql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.QueryCouponRecord ��Sql���ʧ�ܣ�";
                return null;
            }

            try
            {
                strSql = strSql + " " + sqlWhere;

                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                FS.HISFC.Models.Account.CardCouponRecord couponRecord = null;
                while (this.Reader.Read())
                {
                    couponRecord = new CardCouponRecord();

                    couponRecord.ID = this.Reader[0].ToString();
                    couponRecord.CardNo = this.Reader[1].ToString();
                    couponRecord.Name = this.Reader[2].ToString();
                    couponRecord.Money = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
                    couponRecord.Coupon = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
                    couponRecord.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                    couponRecord.InvoiceNo = this.Reader[6].ToString();
                    couponRecord.OperType = this.Reader[7].ToString();
                    couponRecord.ExchangeGoods = this.Reader[8].ToString();
                    couponRecord.Memo = this.Reader[9].ToString();
                    couponRecord.OperEnvironment.ID = this.Reader[10].ToString();
                    couponRecord.OperEnvironment.Name = this.Reader[11].ToString();
                    couponRecord.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12]);

                    cardCouponRecordList.Add(couponRecord);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return cardCouponRecordList;
        }
        /// <summary>
        /// ��ѯ��ͥ��Ա��Ϣ// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="accountFamilyInfoList"></param>
        /// <returns></returns>
        private int QueryAccountFamilyInfo(string sqlWhere, out List<AccountFamilyInfo> accountFamilyInfoList)
        {
            accountFamilyInfoList = null;

            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountFamilyInfo.Select", ref strSql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Select ��Sql���ʧ�ܣ�";
                return -1;
            }

            try
            {
                strSql = strSql + " " + sqlWhere;

                if (this.ExecQuery(strSql) == -1)
                {
                    return -1;
                }

                accountFamilyInfoList = new List<AccountFamilyInfo>();
                AccountFamilyInfo accountFamilyInfo = null;
                while (this.Reader.Read())
                {
                    accountFamilyInfo = new AccountFamilyInfo();
                    accountFamilyInfo.ID = this.Reader[0].ToString();
                    accountFamilyInfo.LinkedCardNO = this.Reader[1].ToString();
                    accountFamilyInfo.LinkedAccountNo = this.Reader[2].ToString();
                    accountFamilyInfo.Relation.ID = this.Reader[3].ToString();
                    accountFamilyInfo.Relation.Name = this.Reader[4].ToString();
                    accountFamilyInfo.Name = this.Reader[5].ToString();
                    accountFamilyInfo.Sex.ID = this.Reader[6].ToString();
                    string sexID = this.Reader[6].ToString();
                    string sexName = "";
                    if (sexID == "M")
                    {
                        sexName = "��";
                    }
                    else if (sexID == "F")
                    {
                        sexName = "Ů";
                    }
                    else
                    {
                        sexName = "����";
                    }
                    accountFamilyInfo.Sex.Name = sexName;
                    accountFamilyInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7]);
                    accountFamilyInfo.CardType.ID = this.Reader[8].ToString();
                    accountFamilyInfo.CardType.Name = this.Reader[9].ToString();
                    accountFamilyInfo.IDCardNo = this.Reader[10].ToString();
                    accountFamilyInfo.Phone = this.Reader[11].ToString();
                    accountFamilyInfo.Address = this.Reader[12].ToString();
                    accountFamilyInfo.CardNO = this.Reader[13].ToString();
                    accountFamilyInfo.AccountNo = this.Reader[14].ToString();
                    string validState = this.Reader[15].ToString();
                    if (validState == "0")
                    {
                        accountFamilyInfo.ValidState = EnumValidState.Invalid;
                    }
                    else if (validState == "1")
                    {
                        accountFamilyInfo.ValidState = EnumValidState.Valid;
                    }

                    accountFamilyInfo.CreateEnvironment.ID = this.Reader[16].ToString();
                    accountFamilyInfo.CreateEnvironment.Name = this.Reader[17].ToString();
                    accountFamilyInfo.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18]);
                    accountFamilyInfo.OperEnvironment.ID = this.Reader[19].ToString();
                    accountFamilyInfo.OperEnvironment.Name = this.Reader[20].ToString();
                    accountFamilyInfo.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[21]);
                    accountFamilyInfo.FamilyCode = this.Reader[22].ToString();
                    accountFamilyInfo.FamilyName = this.Reader[23].ToString();
                    accountFamilyInfoList.Add(accountFamilyInfo);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return 1;
        }
        #endregion


        #region ���ҿ�ʹ�ü�¼
        /// <summary>
        /// ���ҿ�ʹ�ü�¼
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCardRecord> GetAccountCardRecord(string cardNO, string begin, string end)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountCardRecord", ref Sql) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                Sql = string.Format(Sql, cardNO, begin, end);
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "���ҿ�ʹ������ʧ�ܣ�";
                    return null;
                }
                List<FS.HISFC.Models.Account.AccountCardRecord> list = new List<FS.HISFC.Models.Account.AccountCardRecord>();
                FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;
                while (this.Reader.Read())
                {
                    accountCardRecord = new FS.HISFC.Models.Account.AccountCardRecord();
                    accountCardRecord.MarkNO = Reader[0].ToString();
                    accountCardRecord.MarkType.ID = Reader[1].ToString();
                    accountCardRecord.CardNO = Reader[2].ToString();
                    accountCardRecord.OperateTypes.ID = Reader[3];
                    accountCardRecord.Oper.ID = Reader[4].ToString();
                    accountCardRecord.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
                    accountCardRecord.CardMoney = FS.FrameWork.Function.NConvert.ToDecimal(Reader[6]);
                    list.Add(accountCardRecord);
                }
                this.Reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        #endregion

        #region ɾ��������
        /// <summary>
        /// ɾ��������
        /// </summary>
        /// <param name="markNO">����</param>
        /// <param name="markType">������</param>
        /// <returns></returns>
        public int DeleteAccoutCard(string markNO, string markType)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.DeleteAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, markNO, markType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        #endregion

        #region �ʻ�����
        /// <summary>
        /// �ʻ�����
        /// </summary>
        /// <param name="newMark">�¿���</param>
        /// <param name="oldMark">ԭ</param>
        /// <returns></returns>
        public int UpdateAccountCardMark(string newMark, string oldMark)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountCardMarkNo", newMark, oldMark);
        }

        #endregion

        #region �����˻�����д���¼// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm

        /// <summary>
        /// �����˻�����д���¼
        /// </summary>
        /// <param name="patient">��Ȩ��</param>
        /// <param name="accountNo">�˺�</param>
        /// <param name="acountTypeCode">�˻�����</param>
        /// <param name="baseCost">�������</param>
        /// <param name="donateCost">���ͽ��</param>
        /// <param name="payInvoiceNo">��Ӧ���ѷ�Ʊ��</param>
        /// <param name="empowerPatient">����Ȩ��</param>
        /// <param name="payWayTypes">�������ͣ�P�����ײͣ�R����Һţ�C�������ѣ�IסԺ����;M�ײ�Ѻ��</param>
        /// <param name="aMod">0�˷�1����</param>
        /// <returns></returns>
        public int UpdateAccountAndWriteRecord(HISFC.Models.RADT.Patient patient, string accountNo, string acountTypeCode, decimal baseCost, decimal donateCost, string payInvoiceNo, HISFC.Models.RADT.Patient empowerPatient, PayWayTypes payWayTypes, int aMod)
        {
            //����ʱ���ж����
            if (aMod == 1)
            {
                // {0304EC3C-ECA4-4b90-8040-5EBEC93F2EA5}
                AccountDetail currAccountDetail = new AccountDetail();
                currAccountDetail = this.GetAccountDetail(accountNo, acountTypeCode, "1")[0] as AccountDetail;
                if (currAccountDetail.BaseVacancy < baseCost)
                {
                    this.Err = "�����˻����㣡";
                    return -1;
                }
                if (currAccountDetail.DonateVacancy < donateCost)
                {
                    this.Err = "�����˻����㣡";
                    return -1;
                }
            }



            AccountDetail accountDetail = new AccountDetail();

            //���Ѷ��˻��ۼƲ���Ӱ��
            accountDetail.BaseAccumulate = 0;
            accountDetail.DonateAccumulate = 0;
            accountDetail.CouponAccumulate = 0;
            accountDetail.ID = accountNo;
            accountDetail.AccountType.ID = acountTypeCode;
            accountDetail.BaseCost = baseCost;
            accountDetail.DonateCost = donateCost;
            accountDetail.OperEnvironment.ID = this.Operator.ID;
            accountDetail.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();


            #region �����˻�������ϸ����Ϣ���
            if (this.UpdateAccountDetail(accountDetail) <= 0)
            {
                this.Err = "�����˻���ϸ����Ϣ���ʧ�ܣ�";
                return -1;
            }

            #endregion

            #region �����˻����
            FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
            account.ID = accountDetail.ID;
            account.BaseCost = accountDetail.BaseCost;
            account.DonateCost = accountDetail.DonateCost;
            account.CouponCost = 0;
            account.BaseAccumulate = 0;
            account.DonateAccumulate = 0;
            account.CouponAccumulate = 0;
            account.OperEnvironment.ID = this.Operator.ID;
            account.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();

            if (this.UpdateAccountVacancyEX(account) <= 0)
            {
                this.Err = "�����˻����ʧ�ܣ�";
                return -1;
            }
            #endregion

            #region �������Ѽ�¼

            List<AccountDetail> accountDetailList1 = new List<AccountDetail>();
            accountDetailList1 = this.GetAccountDetail(accountDetail.ID, accountDetail.AccountType.ID, "1");
            if (accountDetailList1.Count <= 0)
            {
                this.Err = "��ȡ�˻���ϸ�����ʧ�ܣ�";
                return -1;
            }
            AccountDetail accountDetail2 = accountDetailList1[0] as AccountDetail;
            FS.HISFC.Models.Account.AccountRecord accountRecord = new FS.HISFC.Models.Account.AccountRecord();
            accountRecord = accountDetail.AccountRecord.Clone();
            accountDetail.AccountRecord.Patient = patient;
            accountRecord.EmpowerPatient = empowerPatient;
            accountRecord.PayInvoiceNo = payInvoiceNo;
            if (aMod == 0)
            {
                accountRecord.OperType.ID = "5";
            }
            else if (aMod == 1)
            {
                accountRecord.OperType.ID = "4";
            }

            if (accountDetail.AccountRecord.Patient != null)
            {
                accountRecord.Patient = accountDetail.AccountRecord.Patient.Clone();
                accountRecord.CardNo = accountDetail.AccountRecord.Patient.PID.CardNO;
                accountRecord.Name = accountDetail.AccountRecord.Patient.Name;
            }
            else
            {
                this.Err = "��Ȩ����ϢΪ�գ�";
                return -1;
            }
            if (accountRecord.EmpowerPatient == null)
            {
                this.Err = "����Ȩ����ϢΪ�գ�";
                return -1;
            }
            if (string.IsNullOrEmpty(accountRecord.PayInvoiceNo))
            {
                this.Err = "���ѷ�Ʊ�Ų���Ϊ�գ�";
                return -1;
            }
            accountRecord.AccountNO = accountDetail.ID;
            accountRecord.ID = accountDetail.ID;
            accountRecord.AccountType = accountDetail.AccountType;
            accountRecord.BaseCost = accountDetail.BaseCost;//�˷ѡ�����
            accountRecord.DonateCost = accountDetail.DonateCost;//���ѡ��˷�
            accountRecord.BaseVacancy = accountDetail2.BaseVacancy;//���
            accountRecord.DonateVacancy = accountDetail2.DonateVacancy;//���
            accountRecord.IsValid = true;
            accountRecord.FeeDept.ID = ((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID; ////{1C42FA6C-C70A-4cd4-82C4-9FA1FCABD73B}
            accountRecord.Oper.ID = this.Operator.ID;
            accountRecord.OperTime = this.GetDateTimeFromSysDateTime();
            accountRecord.EmpowerCost = accountDetail.BaseVacancy + accountDetail.DonateVacancy;
            accountRecord.PayType.ID = payWayTypes;
            if (this.InsertAccountRecordEX(accountRecord) <= 0)
            {
                this.Err = "�������Ѽ�¼ʧ�ܣ�";
                return -1;
            }
            #endregion

            return 1;
        }

        #endregion

        #region ����״̬����
        /// <summary>
        /// ����״̬����// {E0683A80-F23C-4848-8482-7257F9263221}
        /// </summary>
        /// <param name="volumeNo"></param>
        /// <param name="OldValidState"></param>
        /// <param name="NewValidState"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateCardVolumeState(string volumeNo, string OldValidState, string NewValidState, string operCode, DateTime operDate)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountCardVolumeState", volumeNo, OldValidState, NewValidState, operCode, operDate.ToString());
        }
        #endregion
        #region ��ͥ��Ա��ϵ״̬�޸�
        /// <summary>
        /// ��ͥ��Ա��ϵ״̬�޸�
        /// </summary>
        /// <param name="seqNo"></param>
        /// <param name="validState"></param>
        /// <returns></returns>
        public int UpdateAccountFamilyInfoState(string seqNo, string validState, string operCode, string date)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountFamilyInfo", seqNo, validState, operCode, date);
        }

        #endregion

        #region ���ݿ��Ź����������
        ///// <summary>
        ///// ���ݿ��Ź����������
        ///// </summary>
        ///// <param name="markNo">����Ŀ���</param>
        ///// <param name="validedMarkNo"></param>
        ///// <returns></returns>
        //public int ValidMarkNO(string markNo, ref string validedMarkNo)
        //{
        //    string firstleter = markNo.Substring(0, 1);
        //    string lastleter = markNo.Substring(markNo.Length - 1, 1);
        //    if (firstleter != ";")
        //    {
        //        this.Err = "��������ȷ�Ŀ��ţ�";
        //        return -1;
        //    }
        //    if (lastleter != "?")
        //    {
        //        this.Err = "��������ȷ�Ŀ��ţ�";
        //        return -1;
        //    }
        //    validedMarkNo = markNo.Substring(1, markNo.Length - 2);
        //    if (!FS.FrameWork.Public.String.IsNumeric(validedMarkNo))
        //    {
        //        this.Err = "��������ȷ�Ŀ��ţ�";
        //        return -1;
        //    }
        //    return 1;
        //}

        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetMaxMcard(string type)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetMAXMCardNO", ref sql) == -1)
            {
                this.Err = "��ѯ����ΪFee.Account.GetMAXMCardNO��SQL���ʧ�ܣ�";
                return null;
            }
            try
            {

                string maxid = ExecSqlReturnOne(sql);
                int maxnum = int.Parse(maxid);
                maxnum = maxnum + 1;
                maxid = maxnum.ToString().PadLeft(5, '0');
                return "T" + maxid;

            }
            catch
            {
                throw new Exception("����");
            }
        }

        #endregion

        #endregion

        #region �ʻ��������ݲ���
        /// <summary>
        /// �ʻ�֧�����˷ѹ���ȡ�ֹ���
        /// </summary>
        /// <param name="patient">����</param>
        /// <param name="money">���</param>
        /// <param name="reMark">��ʶ</param>
        /// <param name="invoiceType">��Ʊ����</param>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="aMod">0�շ� 1�˷� 2ȡ��-��Ȩ�ʻ��޷�ȡ��</param>
        /// <returns></returns>
        public bool AccountPayManager(HISFC.Models.RADT.Patient patient, decimal money, string reMark, string invoiceType, string deptCode, int aMod)
        {
            string strSeqNo = null;

            return AccountPayManager(patient, money, reMark, invoiceType, deptCode, aMod, out strSeqNo);
        }
        /// <summary>
        /// �ʻ�֧�����˷ѹ���ȡ�ֹ��� �����ؽ�����ˮ��
        /// {48508EFF-7D63-42d4-AF73-87C5645B9D7E}
        /// </summary>
        /// <param name="patient">����</param>
        /// <param name="money">���</param>
        /// <param name="reMark">��ʶ</param>
        /// <param name="invoiceType">��Ʊ����</param>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="aMod">0�շ� 1�˷� 2ȡ��-��Ȩ�ʻ��޷�ȡ��</param>
        /// <param name="strSeqNo">���ؽ�����ˮ��</param>
        /// <returns></returns>
        public bool AccountPayManager(HISFC.Models.RADT.Patient patient, decimal money, string reMark, string invoiceType, string deptCode, int aMod, out string strSeqNo)
        {
            strSeqNo = null;

            //�ʻ���������ʻ������ʻ����������Ȩ��Ϣ�򷵻���Ȩ��Ϣ�����
            decimal vacancy = 0m;
            //��Ȩ��Ϣ
            HISFC.Models.Account.AccountEmpower accountEmpower = null;
            //�ʻ���Ϣ
            HISFC.Models.Account.Account account = null;

            HISFC.Models.RADT.Patient tempPaient = new FS.HISFC.Models.RADT.Patient();

            #region ��ѯ���
            //-1ʧ�� 0û�� �ʻ�����Ȩ��Ϣ 1�ʻ��ʻ� 2��Ȩ��Ϣ
            int result = this.GetVacancy(patient.PID.CardNO, ref vacancy);
            if (result <= 0)
            {
                return false;
            }
            #endregion

            #region ��ѯ�ʻ���Ϣ
            if (result == 1)
            {
                //tempCardNO = patient.PID.CardNO;
                tempPaient = patient;
            }
            else
            {
                // ȡ��
                // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                if (aMod == 2)
                {
                    this.Err = "��Ȩ�ʻ��޷�ȡ��!";
                    return false;
                }
                //�����Ȩ��Ϣ
                int resultValue = this.QueryAccountEmpowerByEmpwoerCardNO(patient.PID.CardNO, ref accountEmpower);
                if (resultValue <= 0)
                {

                    return false;
                }
                tempPaient = accountEmpower.AccountCard.Patient;
            }
            account = this.GetAccountByCardNoEX(tempPaient.PID.CardNO);//����ʻ���Ϣ
            if (account == null)
            {
                this.Err = "�û��߲�������Ч�ʻ���";
                return false;
            }
            #endregion

            #region �ж��ж�
            //���շѵ�ʱ���ж�
            if (aMod == 0)
            {
                #region ֧�������ж��ʻ�����Ƿ�
                if (vacancy < money)
                {
                    this.Err = FS.FrameWork.Management.Language.Msg("��" + vacancy.ToString() + "Ԫ�����㱾�ο۷ѽ�" + money.ToString() + "��");
                    return false;
                }
                //��Ȩ��Ϣ
                if (result == 2)
                {
                    //����Ȩ��Ϣ�������ڷ��ý�����Ȩ���ʻ����С�ڷ��õĽ�������ʾ
                    if (account.BaseVacancy < money)
                    {
                        this.Err = "��Ȩ�ʻ������Ϊ��" + account.BaseVacancy.ToString() + "Ԫ�����㱾�ο۷ѽ�" + money.ToString() + "Ԫ";
                        return false;
                    }
                }
                #endregion
            }
            else if (aMod == 2)
            {
                // ȡ��
                // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                #region ȡ�ֲ����ж��ʻ�����Ƿ�
                if (vacancy < money)
                {
                    this.Err = FS.FrameWork.Management.Language.Msg("���" + vacancy.ToString() + "����" + money.ToString() + "��");
                    return false;
                }
                #endregion
            }

            #endregion
            try
            {
                #region ���ɽ��׼�¼
                //���ɽ��׼�¼
                FS.HISFC.Models.Account.AccountRecord accountRecord = new FS.HISFC.Models.Account.AccountRecord();
                //�γɽ��׼�¼
                accountRecord.Patient = tempPaient;
                accountRecord.AccountNO = account.ID;//�ʺ�
                if (result == 1)
                {
                    if (aMod == 0)
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.Pay;//��������
                    }
                    else if (aMod == 1)
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.CancelPay;//��������
                    }
                    else if (aMod == 2)
                    {
                        // �˻�ȡ��
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.AccountTaken;
                    }
                }
                else
                {
                    if (aMod == 0)
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.EmpowerPay;//��������
                    }
                    if (aMod == 1)
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.EmpowerCancelPay;//��������
                    }
                    //����Ȩ����ʵ��
                    accountRecord.EmpowerPatient = accountEmpower.EmpowerCard.Patient;
                }
                accountRecord.BaseCost = -money;//���
                accountRecord.FeeDept.ID = deptCode;//����
                accountRecord.Oper.ID = this.Operator.ID;//����Ա
                accountRecord.OperTime = this.GetDateTimeFromSysDateTime();//����ʱ��
                accountRecord.ReMark = reMark;//��Ʊ��
                accountRecord.IsValid = true;//�Ƿ���Ч
                accountRecord.BaseVacancy = account.BaseVacancy - money;//���ν������
                accountRecord.InvoiceType.ID = invoiceType;
                if (!string.IsNullOrEmpty(patient.HomeZip))
                {
                    accountRecord.Patient.HomeZip = patient.HomeZip;
                }
                else
                {
                    accountRecord.Patient.HomeZip = "";
                }
                //�����ʻ����׼�¼
                //strSeqNo = accountRecord.ID;
                if (this.InsertAccountRecordEX(accountRecord) == -1)
                {
                    this.Err = "���뽻������ʧ�ܣ�" + this.Err;
                    return false;
                }
                #endregion

                #region �������
                //���±�����Ȩ�ʻ������
                if (result == 2)
                {

                    if (UpdateEmpowerVacancy(account.ID, patient.PID.CardNO, money) <= 0)
                    {
                        this.Err = "������Ȩ��Ϣ���ʧ�ܣ�";
                        return false;
                    }
                }
                //�����ʻ����
                if (UpdateAccountVacancy(account.ID, money) <= 0)
                {
                    this.Err = "�����ʻ����ʧ�ܣ�";
                    return false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }

            return true;
        }

        #region �ʻ�Ԥ����
        /// <summary>
        /// �ʻ�Ԥ����
        /// </summary>
        /// <param name="accountRecord">����ʵ��</param>
        /// <param name="aMod">1��ȡ 0����</param>
        /// <returns></returns>
        public bool AccountPrePayManager(PrePay prePay, int aMode)
        {
            try
            {
                //����
                if (aMode == 0)
                {
                    prePay.BaseCost = -prePay.BaseCost;
                    prePay.DonateCost = -prePay.DonateCost;
                    //prePay.OldInvoice = prePay.InvoiceNO;
                    //if (UpdatePrePayState(prePay) < 1)
                    //{
                    //    this.Err = this.Err + "������¼�Ѿ����й������������������״̬����!";
                    //    return false;
                    //}
                }

                if (this.InsertPrePay(prePay) < 0)
                {
                    this.Err = "����Ԥ��������ʧ�ܣ�";
                    return false;
                }

                #region ���뽻�׼�¼

                decimal vacancy = 0;
                int result = this.GetVacancy(prePay.Patient.PID.CardNO, ref vacancy);
                if (result <= 0)
                {
                    return false;
                }
                #region ����ʵ��

                AccountRecord accountRecord = new AccountRecord();
                accountRecord.Patient.PID.CardNO = prePay.Patient.PID.CardNO; //���￨��
                accountRecord.FeeDept.ID = (Operator as FS.HISFC.Models.Base.Employee).Dept.ID; //����
                accountRecord.Oper.ID = this.Operator.ID; //����Ա
                accountRecord.OperTime = prePay.PrePayOper.OperTime; //����
                accountRecord.IsValid = true;//����״̬
                accountRecord.AccountNO = prePay.AccountNO;//�ʺ�
                accountRecord.Name = prePay.Patient.Name;//����
                if (aMode == 0)
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.CancelPrePay;//��������

                }
                else
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.PrePay;//��������
                }
                accountRecord.BaseCost = prePay.BaseCost;//���
                accountRecord.ReMark = prePay.InvoiceNO;//��Ʊ��
                accountRecord.BaseVacancy = prePay.BaseVacancy;//���ν������
                //accountRecord.Money = prePay.FT.PrepayCost;
                accountRecord.InvoiceType.ID = "A";
                #endregion

                if (this.InsertAccountRecord(accountRecord) < 0)
                {
                    return false;
                }
                #endregion

                #region �����ʻ����
                //�ڼ����ʻ����ʱ�����-���ν��׵�Ǯ
                decimal consumeMoney = -accountRecord.BaseCost;

                if (this.UpdateAccountVacancy(accountRecord.AccountNO, consumeMoney) < 0)
                {
                    return false;
                }

                #endregion

                return true;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }

        }
        /// <summary>
        /// �ʻ�Ԥ�����ֵ
        /// NEW// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="accountRecord">����ʵ��</param>
        /// <param name="aMod">1��ȡ 0����</param>
        /// <returns></returns>
        public bool AccountPrePayManagerEX(PrePay prePay, int aMode)
        {
            try
            {
                if (prePay == null)
                {
                    this.Err = "Ԥ��������Ϊ�գ�";
                    return false;
                }
                if (this.InsertPrePayEX(prePay) < 0)//lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                {
                    this.Err = "����Ԥ��������ʧ�ܣ�";
                    return false;
                }

                #region ���뽻�׼�¼

                #region ����ʵ��

                AccountRecord accountRecord = new AccountRecord();
                accountRecord.Patient.PID.CardNO = prePay.Patient.PID.CardNO; //���￨��
                accountRecord.FeeDept.ID = (Operator as FS.HISFC.Models.Base.Employee).Dept.ID; //����
                accountRecord.Oper.ID = this.Operator.ID; //����Ա
                accountRecord.OperTime = prePay.PrePayOper.OperTime; //����
                accountRecord.IsValid = true;//����״̬
                accountRecord.AccountNO = prePay.AccountNO;//�ʺ�
                accountRecord.Name = prePay.Patient.Name;//����
                if (aMode == 0)
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.CancelPrePay;//��������
                }
                else if (aMode == 10)// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.BalanceVacancy;//��������
                    if (UpdatePrePayHistory(prePay.AccountNO, false, true) < 0)// {74621CF6-D26C-44aa-87DC-C68D0867BAC5}
                    {
                        Err = "�����˻���¼״̬ʧ�ܣ�" + Err;
                        return false;
                    }
                }
                else
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.PrePay;//��������
                }
                // {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                accountRecord.BaseCost = prePay.BaseCost;//���
                accountRecord.DonateCost = prePay.DonateCost;
                accountRecord.InvoiceType.ID = "A";
                accountRecord.BaseVacancy = prePay.BaseVacancy;//�����˻��������
                accountRecord.DonateVacancy = prePay.DonateVacancy;
                accountRecord.AccountType.ID = prePay.AccountType.ID;//�˻�����
                accountRecord.InvoiceNo = prePay.InvoiceNO;//��Ʊ��
                accountRecord.ReMark = prePay.Memo;
                accountRecord.PayInvoiceNo = prePay.Bank.InvoiceNO;
                if (this.InsertAccountRecordEX(accountRecord) < 0)
                {
                    return false;
                }
                #endregion
                #endregion

                #region �����ʻ����
                //�ڼ����ʻ����ʱ�����-���ν��׵�Ǯ
                //lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                //decimal consumeMoney = -accountRecord.Money;

                FS.HISFC.Models.Account.Account account2 = new FS.HISFC.Models.Account.Account();
                account2.ID = accountRecord.AccountNO;
                account2.BaseCost = prePay.BaseCost;
                account2.DonateCost = prePay.DonateCost;
                if (aMode == 0 || aMode == 10)
                {
                    account2.BaseAccumulate = 0;//�����ۼƽ�������
                    account2.DonateAccumulate = 0;
                    account2.CouponAccumulate = 0;
                }
                else
                {
                    account2.BaseAccumulate = prePay.BaseCost;
                    account2.DonateAccumulate = prePay.DonateCost;
                }
                account2.CouponCost = 0;//������ʱ������
                account2.Limit = 0;//����ֵ��ʱ������
                account2.OperEnvironment.ID = this.Operator.ID;
                account2.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();
                if (this.UpdateAccountVacancyEX(account2) < 0)
                {
                    return false;
                }
                #endregion

                #region ���¶�Ӧ�ʻ�������ϸ���
                FS.HISFC.Models.Account.AccountDetail accountDetail = new AccountDetail();
                accountDetail.ID = prePay.AccountNO;//�˺�
                accountDetail.AccountType.ID = prePay.AccountType.ID;//�˻�����
                accountDetail.CardNO = prePay.Patient.PID.CardNO;
                accountDetail.BaseCost = prePay.BaseCost;//��ֵ���
                accountDetail.DonateCost = prePay.DonateCost;//���ͽ��
                if (aMode == 0 || aMode == 10)
                {
                    accountDetail.BaseAccumulate = 0;//�����ۼƽ�������
                    accountDetail.DonateAccumulate = 0;
                    accountDetail.CouponAccumulate = 0;
                }
                else
                {
                    accountDetail.BaseAccumulate = prePay.BaseCost;
                    accountDetail.DonateAccumulate = prePay.DonateCost;
                }
                accountDetail.OperEnvironment.ID = this.Operator.ID;
                accountDetail.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();
                accountDetail.CouponCost = 0;//�����ݲ�����
                if (this.UpdateAccountDetail(accountDetail) < 0)
                {
                    return false;
                }
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }

        }
        /// <summary>
        /// ����Ԥ��������
        /// </summary>
        /// <param name="prePay">Ԥ����ʵ��</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int InsertPrePay(PrePay prePay)
        {
            return this.UpdateSingTable("Fee.Account.InsertAccountPrePay", GetPrePayArgs(prePay));
        }
        /// <summary>
        /// ����Ԥ��������NEW
        /// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="prePay">Ԥ����ʵ��</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int InsertPrePayEX(PrePay prePay)
        {
            return this.UpdateSingTable("Fee.Account.InsertAccountPrePay.1", GetPrePayArgsEX(prePay));
        }

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// ����ʱ��β�ѯԤ��������
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="isHistory">1��ʷ���� 0��ǰ���� ALLȫ������</param>
        /// <returns>nullʧ��</returns>
        public List<PrePay> GetPrepayByAccountNO(string accountNO, string isHistory)
        {
            return this.GetPrePayList("Fee.Account.GetPrePayWhere1", accountNO, isHistory);
        }

        /// <summary>
        /// �����˺Ų�ѯԤ��������// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="accountNO">�˺�</param>
        /// <param name="isHistory">1��ʷ���� 0��ǰ���� ALLȫ������</param>
        /// <returns>nullʧ��</returns>
        public List<PrePay> GetPrepayByAccountNOEX(string accountNO, string isHistory)
        {
            return this.GetPrePayListEX("Fee.Account.GetPrePayWhere1", accountNO, isHistory);
        }
        /// <summary>
        /// �����˺ź��˻����Ͳ���Ԥ��������
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="isHistory">1��ʷ���� 0��ǰ���� ALLȫ������</param>
        /// <returns>nullʧ��</returns>
        public List<PrePay> GetPrepayByAccountNOAndType(string accountNO, string AccountType, string isHistory)
        {
            return this.GetPrePayListEX("Fee.Account.GetPrePayWhere.2", accountNO, AccountType, isHistory);
        }

        /// <summary>
        /// ���ݾ��￨�Ų�ѯҽ����Ŀ��ϸ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public List<AccountMedicalInsurance> GetMedicalInsuranceByCardNo(string cardNO)
        {
            return this.GetMedicalInsuranceEX("Fee.Account.GetMedicalInsurance", cardNO);
        }

        /// <summary>
        /// ���ݾ��￨�Ų�ѯҽ����Ŀ��ϸ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public int UpdateMedicalInsuranceByCardNo(string cardNO, string operId)
        {
            return this.UpdateMedicalByCardNo("Fee.Account.UpdateMedicalInsurance", cardNO, operId);
        }

        /// <summary>
        /// ����Ԥ����״̬ --����Ϊ���ϻ򲹴�״̬
        /// </summary>
        /// <param name="prePay">Ԥ����ʵ��</param>
        /// <returns>1�ɹ� -1ʧ�� 0û�и��¼�¼</returns>
        public int UpdatePrePayState(PrePay prePay)
        {
            return this.UpdateSingTable("Fee.Account.UpdatePrePayState", prePay.AccountNO, prePay.HappenNO.ToString(), ((int)prePay.ValidState).ToString());
        }

        /// <summary>
        /// �����ʻ�Ԥ������ʷ����״̬
        /// </summary>
        /// <returns></returns>
        public int UpdatePrePayHistory(string accountNO, bool currentState, bool updateState)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountPrePayHistoryState", accountNO, NConvert.ToInt32(currentState).ToString(), NConvert.ToInt32(updateState).ToString());
        }
        #endregion

        #region  ͨ�������Ų������￨��
        /// <summary>
        /// ͨ�������Ų������￨��
        /// </summary>
        /// <param name="markNo">������</param>
        /// <param name="markType">������</param>
        /// <param name="cardNo">���￨��</param>
        /// <returns>bool true �ɹ���false ʧ��</returns>
        public bool GetCardNoByMarkNo(string markNo, NeuObject markType, ref string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectCardNoByMarkNo", ref Sql) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return false;
            }
            try
            {
                Sql = string.Format(Sql, markNo, markType.ID);
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return false;
                }
                #region Sql
                /*select b.card_no,
                           b.markno,
                           b.type,
                           b.state as cardstate,
                           a.state as accountstate,
                           a.vacancy 
                    from fin_opb_account a,fin_opb_accountcard b 
                    where a.card_no=b.card_no 
                      and b.markno='{0}' 
                      and type='{1}'*/
                #endregion
                FS.HISFC.Models.Account.Account account = null;
                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.AccountCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    account.AccountCard.MarkNO = this.Reader[1].ToString();
                    account.AccountCard.MarkType.ID = Reader[2].ToString();
                    if (this.Reader[3].ToString() == "0")
                    {
                        account.AccountCard.IsValid = false;
                    }
                    else
                    {
                        account.AccountCard.IsValid = true;
                    }
                }
                this.Reader.Close();
                if (account == null)
                {
                    this.Err = "�ÿ�" + markNo + "�ѱ�ȡ��ʹ�ã�";
                    return false;
                }
                if (!account.AccountCard.IsValid)
                {
                    this.Err = "�ÿ�" + markNo + "�ѱ�ֹͣʹ�ã�";
                    return false;
                }
                cardNo = account.AccountCard.Patient.PID.CardNO;

                return true;
            }
            catch (Exception ex)
            {
                this.Err = "�������￨��ʧ�ܣ�" + ex.Message;
                return false;
            }

        }

        /// <summary>
        /// ͨ�������Ų������￨��
        /// </summary>
        /// <param name="markNo">������</param>
        /// <param name="cardNo">���￨��</param>
        /// <returns>bool true �ɹ���false ʧ��</returns>
        public bool GetCardNoByMarkNo(string markNo, ref string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectCardNoByMarkNo1", ref Sql) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return false;
            }
            try
            {
                Sql = string.Format(Sql, markNo);
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return false;
                }
                #region Sql
                /*select b.card_no,
                           b.markno,
                           b.type,
                           b.state as cardstate,
                           a.state as accountstate,
                           a.vacancy 
                    from fin_opb_account a,fin_opb_accountcard b 
                    where a.card_no=b.card_no 
                      and b.markno='{0}' 
                      and type='{1}'*/
                #endregion
                FS.HISFC.Models.Account.Account account = null;
                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.AccountCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    account.AccountCard.MarkNO = this.Reader[1].ToString();
                    account.AccountCard.MarkType.ID = Reader[2].ToString();
                    if (this.Reader[3].ToString() == "0")
                    {
                        account.AccountCard.IsValid = false;
                    }
                    else
                    {
                        account.AccountCard.IsValid = true;
                    }
                }
                this.Reader.Close();
                if (account == null)
                {
                    this.Err = "�ÿ�" + markNo + "�ѱ�ȡ��ʹ�ã�";
                    return false;
                }
                if (!account.AccountCard.IsValid)
                {
                    this.Err = "�ÿ�" + markNo + "�ѱ�ֹͣʹ�ã�";
                    return false;
                }
                cardNo = account.AccountCard.Patient.PID.CardNO;

                return true;
            }
            catch (Exception ex)
            {
                this.Err = "�������￨��ʧ�ܣ�" + ex.Message;
                return false;
            }

        }

        #endregion

        #region {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
        /// <summary>
        ///  ��ȡ�µ�Ԥ����Ʊ����
        /// </summary>
        /// <returns></returns>
        public string GetNewInvoiceNO(string InvoiceNoType)
        {
            string invoiceno = "";
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetNewInvoiceNO", ref Sql) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                Sql = string.Format(Sql, this.Operator.ID, InvoiceNoType);

                string result = this.ExecSqlReturnOne(Sql, "");
                if (string.IsNullOrEmpty(result))
                {
                    DateTime curr = this.GetDateTimeFromSysDateTime();
                    invoiceno = "A" + curr.ToString("yyMMdd") + this.Operator.ID + "0001";
                }
                else
                {
                    invoiceno = result.Substring(0, 13) + Convert.ToString((Convert.ToInt32(result.Substring(13, 4)) + 1)).PadLeft(4, '0');
                }
                return invoiceno;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

        }
        #endregion




        /// <summary>
        /// ���������ʻ�������ʱ�䡢����״̬�����ʻ����ײ�����ˮ��¼
        /// </summary>
        /// <param name="cardNO">�����ʻ�</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <param name="opertype">��������</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountRecord> GetAccountRecordList(string cardNO, string begin, string end, string opertype)
        {
            return this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere1", cardNO, begin, end, opertype);
        }
        #endregion

        #region  ���������ʻ�������ʱ������ʻ����ײ�����ˮ��¼
        /// <summary>
        /// ���������ʻ�������ʱ������ʻ����ײ�����ˮ��¼
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountRecord> GetAccountRecordList(string cardNO, string begin, string end)
        {
            return this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere3", cardNO, begin, end);
        }

        #endregion


        #region �����ʺ��Լ��������Ͳ����ʻ�������ˮ��¼

        /// <summary>
        /// �����ʺ��Լ��������Ͳ����ʻ�������ˮ��¼
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="operType">������¼</param>
        /// <returns></returns>
        public List<AccountRecord> GetAccountRecordList(string cardNO, string operType)
        {
            return this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere4", cardNO, operType);
        }

        #endregion

        #region ���ݽ�����ˮ�Ų����ʻ�������ˮ��¼
        /// <summary>
        /// ���ݽ�����ˮ�Ų����ʻ�������ˮ��¼
        /// {48314E1F-72EC-4044-A41A-833C84687A40}
        /// </summary>
        /// <param name="strSeqNo">������ˮ��</param>
        /// <returns></returns>
        public AccountRecord GetAccountRecord(string strSeqNo)
        {
            List<AccountRecord> accountList = this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere5", strSeqNo, "");

            if (accountList == null)
            {
                return null;
            }
            return accountList[0];
        }
        /// <summary>
        /// ��ȡ�˻���ALLȫ����������Ч��// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="accountTypeCode"></param>
        /// <param name="vailFlag">�Ƿ����ЧALL��ȫ��</param>
        /// <returns></returns>
        public List<AccountDetail> GetAccountDetail(string accountNo, string accountTypeCode, string vailFlag)
        {
            return this.GetAccountDetailSelect(accountNo, accountTypeCode, vailFlag, "Fee.Account.GetAccountDetail.Where.1");
        }

        #endregion


        #region �������￨�š���Ʊ�Ų�ѯ���׼�¼
        /// <summary>
        /// �������￨�š���Ʊ�Ų�ѯ���׼�¼
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <returns>����ʵ��</returns>
        public FS.HISFC.Models.Account.AccountRecord GetAccountRecord(string cardNO, string invoiceNO)
        {
            List<AccountRecord> accountList = this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere2", cardNO, invoiceNO);
            if (accountList.Count > 0)
            {
                return accountList[0];
            }
            return null;
        }

        #endregion


        #region ���½���״̬
        /// <summary>
        /// ���½���״̬
        /// </summary>
        /// <param name="valid">�Ƿ���Ч0��Ч1��Ч</param>
        /// <param name="cardNO">�����ʺ�</param>
        /// <param name="operTime">����ʱ��</param>
        /// <returns></returns>
        public int UpdateAccountRecordState(string valid, string cardNO, string operTime, string remark)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountRecordValid", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, valid, cardNO, operTime, remark);
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

        #region �ʻ����׼�¼
        /// <summary>
        /// ��������Ϣ���׼�¼
        /// </summary>
        /// <returns></returns>
        public int InsertAccountRecord(FS.HISFC.Models.Account.AccountRecord accountRecord)
        {
            string[] args = new string[] {
                                  accountRecord.Patient.PID.CardNO, //���￨��
                                  accountRecord.OperType.ID.ToString(),//��������
                                  accountRecord.BaseCost.ToString(), //���
                                  accountRecord.FeeDept.ID,//����
                                  accountRecord.Oper.ID,//������
                                  accountRecord.OperTime.ToString(),//����ʱ��
                                  accountRecord.ReMark, //��ע
                                  FS.FrameWork.Function.NConvert.ToInt32(accountRecord.IsValid).ToString(),//�Ƿ���Ч
                                  accountRecord.AccountNO,//�ʺ�
                                  accountRecord.EmpowerPatient.PID.CardNO, //����Ȩ����
                                  accountRecord.EmpowerPatient.Name, //����Ȩ������
                                  accountRecord.Patient.Name,//��Ȩ������
                                  accountRecord.EmpowerCost.ToString(),//��Ȩ���
                                  accountRecord.InvoiceType.ID,//��Ʊ����
                                  accountRecord.AccountType.ID //�˻�����// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
            };
            return this.UpdateSingTable("Fee.Account.InsertAccountRecord", args);
        }

        /// <summary>
        /// ��������Ϣ���׼�¼
        /// </summary>
        /// <returns></returns>
        public int InsertAccountDetail(FS.HISFC.Models.Account.AccountDetail accountDetail)
        {
            string[] args = new string[] {
                                  accountDetail.ID, //���￨��
                                  accountDetail.AccountType.ID,//��������
                                  accountDetail.CardNO, //���
                                  FS.FrameWork.Function.NConvert.ToInt32(accountDetail.IsValid).ToString(),
                                  accountDetail.CreateEnvironment.ID,//������
                                  accountDetail.CreateEnvironment.OperTime.ToString(),//����ʱ��
                                  accountDetail.OperEnvironment.ID,//������
                                  accountDetail.OperEnvironment.OperTime.ToString(),//����ʱ��
            };
            return this.UpdateSingTable("Fee.Account.InsertAccountDetail", args);
        }
        /// <summary>
        /// ��������Ϣ���׼�¼NEW lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <returns></returns>
        public int InsertAccountRecordEX(FS.HISFC.Models.Account.AccountRecord accountRecord)
        {
            string[] args = new string[] {
                                  accountRecord.Patient.PID.CardNO, //���￨��
                                  accountRecord.OperType.ID.ToString(),//��������
                                  accountRecord.BaseCost.ToString(), //���
                                  accountRecord.FeeDept.ID,//����
                                  accountRecord.Oper.ID,//������
                                  accountRecord.OperTime.ToString(),//����ʱ��
                                  accountRecord.ReMark, //��ע
                                  FS.FrameWork.Function.NConvert.ToInt32(accountRecord.IsValid).ToString(),//�Ƿ���Ч
                                  accountRecord.AccountNO,//�ʺ�
                                  accountRecord.EmpowerPatient.PID.CardNO, //����Ȩ����
                                  accountRecord.EmpowerPatient.Name, //����Ȩ������
                                  accountRecord.Patient.Name,//��Ȩ������
                                  accountRecord.EmpowerCost.ToString(),//��Ȩ���
                                  accountRecord.InvoiceType.ID,// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                                  accountRecord.DonateCost.ToString(),//���ͽ��
                                  accountRecord.BaseVacancy.ToString(),//���׺�����˻����
                                  accountRecord.DonateVacancy.ToString(),//���׺������˻����
                                  accountRecord.AccountType.ID,//�˻�����
                                  accountRecord.InvoiceNo,//��Ʊ��
                                  accountRecord.PayType.Name,//��������
                                  accountRecord.PayInvoiceNo//���ѷ�Ʊ��
            };//��Ʊ����
            return this.UpdateSingTable("Fee.Account.InsertAccountRecord.1", args);
        }

        #endregion

        #region ���ݷ�Ʊ�Ų��ҷ�����ϸ
        /// <summary>
        /// ���ݷ�Ʊ�Ų��ҷ�����ϸ
        /// </summary>
        /// <param name="invoiceNO">��Ʊ����</param>
        /// <param name="isQuite">�Ƿ��˷�</param>
        /// <returns></returns>
        public DataSet GetFeeDetailByInvoiceNO(string invoiceNO, bool isQuite)
        {
            DataSet dsFeeDetail = new DataSet();
            string quiteFlg = isQuite ? "2" : "1";
            if (this.ExecQuery("Fee.Account.QueryFeeDetailByInvoiceForAccout", ref dsFeeDetail, invoiceNO, quiteFlg) < 0)
            {
                return null;
            }
            return dsFeeDetail;
        }
        #endregion

        #region �ʻ����ݲ���

        #region �����ʺ�
        /// <summary>
        /// �����ʺ�
        /// </summary>
        /// <returns></returns>
        public string GetAccountNO()
        {
            return this.GetSequence("Fee.Account.GetAccountNO");
        }
        #endregion

        #region �õ��ʻ����

        /// <summary>
        /// �����ʻ����
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="vacancy">���</param>
        /// <returns>-1����ʧ�� 0������ 1�ʻ���� 2��Ȩ���</returns>
        public int GetVacancy(string cardNO, ref decimal vacancy)
        {
            //�����ʻ����
            int resultValue = this.GetAccountVacancy(cardNO, ref vacancy);
            //�������ʻ�
            if (resultValue == 0)
            {
                //���ұ���Ȩ���
                resultValue = this.GetEmpowerVacancy(cardNO, ref vacancy);
                if (resultValue > 0)
                {
                    return 2;
                }
            }
            return resultValue;

        }
        #endregion

        #region �������￨�Ÿ����ʻ����
        /// <summary>
        /// �������￨�Ÿ����ʻ����
        /// </summary>
        /// <param name="cardNO">�ʺ�</param>
        /// <param name="money">���ѽ��</param>
        /// <returns></returns>
        public int UpdateAccountVacancy(string accountNO, decimal money)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountVacancy", accountNO, money.ToString());
        }
        /// <summary>
        /// �������￨�Ÿ����ʻ����NEW lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="cardNO">�ʺ�</param>
        /// <param name="money">���ѽ��</param>
        /// <returns></returns>
        public int UpdateAccountVacancyEX(FS.HISFC.Models.Account.Account account)
        {

            string[] args = new string[] {
                                  account.ID, //Account_No
                                  account.BaseCost.ToString(),//�����˻����
                                  account.DonateCost.ToString(), //���ͽ��
                                  account.CouponCost.ToString(),//����
                                  account.Limit.ToString(),//����ֵ
                                  account.BaseAccumulate.ToString(),//�����˻��ۼ�
                                  account.DonateAccumulate.ToString(),//�����ۼ�
                                  account.CouponAccumulate.ToString(),//�����ۼ�
                                  account.OperEnvironment.ID,//������
                                  account.OperEnvironment.OperTime.ToString()//����ʱ��

            };
            return this.UpdateSingTable("Fee.Account.UpdateAccountVacancy.1", args);
        }
        /// <summary>
        /// �����˻��Ļ�Ա���ȼ�
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="accountLevelCode"></param>
        /// <returns></returns>
        public int UpdateAccountLevel(string accountNo, string accountLevelCode)
        {
            string[] args = new string[] { accountNo, accountLevelCode };
            return this.UpdateSingTable("Fee.Account.UpdateAccountLevel", args);
        }
        /// <summary>
        /// �����˺ź��˻����͸��½��// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <returns></returns>
        public int UpdateAccountDetail(FS.HISFC.Models.Account.AccountDetail accountDetail)
        {

            string[] args = new string[] {
                                  accountDetail.ID, //Account_No
                                  accountDetail.AccountType.ID,//�˻�����
                                  accountDetail.BaseCost.ToString(),//�����˻����
                                  accountDetail.DonateCost.ToString(), //���ͽ��
                                  accountDetail.CouponCost.ToString(),//����
                                  accountDetail.BaseAccumulate.ToString(),//�����˻��ۼ�
                                  accountDetail.DonateAccumulate.ToString(),//�����ۼ�
                                  accountDetail.CouponAccumulate.ToString(),//�����ۼ�
                                  accountDetail.OperEnvironment.ID,//������
                                  accountDetail.OperEnvironment.OperTime.ToString()//����ʱ��

            };
            return this.UpdateSingTable("Fee.Account.UpdateAccountDetail", args);
        }
        #endregion

        #region �������￨�Ų�������
        /// <summary>
        /// �������￨�Ų�������
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <returns>�û�����</returns>
        public string GetPassWordByCardNO(string cardNO)
        {
            HISFC.Models.Account.Account account = GetAccountByCardNoEX(cardNO);
            if (account == null)
            {
                AccountEmpower accountEmpower = new AccountEmpower();
                int result = this.QueryAccountEmpowerByEmpwoerCardNO(cardNO, ref accountEmpower);
                if (result <= 0) return "-1";
                return accountEmpower.PassWord;
            }
            else
            {
                return account.PassWord;
            }
        }

        #endregion

        #region �������￨�Ÿ����û�����
        /// <summary>
        /// �������￨�Ÿ����û�����
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="passWord">����</param>
        /// <returns></returns>
        public int UpdatePassWordByCardNO(string accountNO, string passWord)
        {
            return this.UpdateSingTable("Fee.Account.UpdatePassWord", accountNO, HisDecrypt.Encrypt(passWord));
        }
        #endregion

        #region �����ʻ�״̬
        /// <summary>
        /// �����ʻ�״̬
        /// </summary>
        /// <param name="accountNO">�ʺ�</param>
        /// <param name="state">״̬</param>
        /// <returns></returns>
        public int UpdateAccountState(string accountNO, string state)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountState", state, accountNO, this.Operator.ID, this.GetDateTimeFromSysDateTime().ToString());
        }
        #endregion
        #region �����˺ź��˻����͸����ʻ�״̬
        /// <summary>
        /// �����ʻ�״̬// {74621CF6-D26C-44aa-87DC-C68D0867BAC5}
        /// </summary>
        /// <param name="accountNO">�ʺ�</param>
        /// <param name="state">״̬</param>
        /// <returns></returns>
        public int UpdateAccountDetailState(string accountNO, string accountType, string state)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountDetailState", state, accountNO, accountType, this.Operator.ID, this.GetDateTimeFromSysDateTime().ToString());
        }
        #endregion

        #region �½��ʻ�
        /// <summary>
        /// �½��ʻ�
        /// </summary>
        /// <param name="account">�ʻ�ʵ��</param>
        /// <returns></returns>
        public int InsertAccount(FS.HISFC.Models.Account.Account account)
        {

            return this.UpdateSingTable("Fee.Account.InsertAccount", account.AccountCard.Patient.PID.CardNO, //���￨��
                                            FS.FrameWork.Function.NConvert.ToInt32(account.ValidState).ToString(), //�ʻ�״̬
                                            account.ID,//�ʺ�
                                            HisDecrypt.Encrypt(account.PassWord),
                                            account.AccountLevel.ID,// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                                            account.CreateEnvironment.ID,
                                            account.CreateEnvironment.OperTime.ToString(),
                                            account.OperEnvironment.ID,
                                            account.OperEnvironment.OperTime.ToString());//����
        }
        #endregion

        #region �����˻�
        /// <summary>
        /// �����˻�// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int InsertCardCoupon(FS.HISFC.Models.Account.CardCoupon cardCoupon)
        {

            return this.UpdateSingTable("Fee.Account.InsertCoupon",
                                            cardCoupon.CardNo, //���￨��
                                            cardCoupon.Coupon.ToString("F2"), //����
                                            cardCoupon.CouponAccumulate.ToString("F2"));//�����ۼ�
        }
        #endregion


        #region �����˻�
        /// <summary>
        /// �����˻���¼// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int InsertCardCouponRecord(FS.HISFC.Models.Account.CardCouponRecord cardCouponRecord)
        {

            return this.UpdateSingTable("Fee.Account.InsertCouponRecord",
                                            cardCouponRecord.CardNo, //���￨��
                                            cardCouponRecord.Money.ToString("F2"),
                                            cardCouponRecord.Coupon.ToString("F2"),
                                            cardCouponRecord.CouponVacancy.ToString("F2"),
                                            cardCouponRecord.InvoiceNo,
                                            cardCouponRecord.OperType,
                                            cardCouponRecord.ExchangeGoods,
                                            cardCouponRecord.Memo,
                                            cardCouponRecord.OperEnvironment.ID,
                                            cardCouponRecord.OperEnvironment.OperTime.ToString()
                                            );//����
        }
        #endregion
        #region �Ǽǿ���
        /// <�Ǽǿ���>
        /// �½��ʻ�// {E0683A80-F23C-4848-8482-7257F9263221}
        /// </summary>
        /// <param name="account">�ʻ�ʵ��</param>
        /// <returns></returns>
        public int InsertAccountCardVolume(FS.HISFC.Models.Account.CardVolume cardVolume)
        {

            return this.UpdateSingTable("Fee.Account.InsertAccountCardVolume", cardVolume.VolumeNo,
                cardVolume.Money.ToString(),
                cardVolume.BegTime.ToString(),
                cardVolume.EndTime.ToString(),
                cardVolume.UseType.Name,
                cardVolume.InvoiceNo,
                cardVolume.Patient.PID.CardNO,
                cardVolume.Mark,
                FS.FrameWork.Function.NConvert.ToInt32(cardVolume.ValidState).ToString(),
                cardVolume.CreateEnvironment.ID,
                cardVolume.CreateEnvironment.OperTime.ToString(),
                cardVolume.OperEnvironment.ID,
                cardVolume.OperEnvironment.OperTime.ToString());//����
        }
        #endregion
        #region �½���Ա
        /// <summary>
        /// �½���ͥ��Ա// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="accountFamilyInfo"></param>
        /// <returns></returns>
        public int InsertAccountFamilyInfo(FS.HISFC.Models.Account.AccountFamilyInfo accountFamilyInfo)
        {
            string validState = "1";
            if (accountFamilyInfo.ValidState == EnumValidState.Valid)
            {
                validState = "1";
            }
            else if (accountFamilyInfo.ValidState == EnumValidState.Invalid)
            {
                validState = "0";
            }

            return this.UpdateSingTable("Fee.Account.InsertAccountFamilyInfo",
                accountFamilyInfo.LinkedCardNO,
                accountFamilyInfo.LinkedAccountNo,
                accountFamilyInfo.Relation.ID,
                accountFamilyInfo.Name,
                accountFamilyInfo.Sex.ID,
                accountFamilyInfo.Birthday.ToString(),
                accountFamilyInfo.CardType.ID,
                accountFamilyInfo.IDCardNo,
                accountFamilyInfo.Phone,
                accountFamilyInfo.Address,
                accountFamilyInfo.CardNO,
                accountFamilyInfo.AccountNo,
                validState,
                accountFamilyInfo.CreateEnvironment.ID,
                accountFamilyInfo.CreateEnvironment.OperTime.ToString(),
                accountFamilyInfo.OperEnvironment.ID,
                accountFamilyInfo.OperEnvironment.OperTime.ToString(),
                accountFamilyInfo.FamilyCode,
                accountFamilyInfo.FamilyName);//
        }

        /// <summary>
        /// �޸Ļ�����Ϣ�ļ�ͥ��
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="familyCode"></param>
        /// <param name="familyName"></param>
        /// <returns></returns>
        public int UpdatePatientFamilyCode(string CardNo, string familyCode, string familyName)
        {
            return this.UpdateSingTable("Fee.Account.UpdatePatientFamilyCode", CardNo, familyCode, familyName);//����
        }
        #endregion

        #region �������￨��ȡ�ʻ���Ϣ
        /// <summary>
        /// �������￨��ȡ�ʻ���Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.Account GetAccountByCardNoEX(string cardNO)
        {
            return this.GetAccountEX("Fee.Account.where1", cardNO);
        }
        #endregion

        #region �����ʺŻ�ȡȡ�ʻ���Ϣ
        /// <summary>
        /// �����ʺ�ȡ�ʻ���Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.Account GetAccountByAccountNO(string accountNO)
        {
            return this.GetAccountEX("Fee.Account.where2", accountNO);
        }
        #endregion

        #region ���������Ų����ʻ�����
        /// <summary>
        /// ���������Ų����ʻ�����
        /// </summary>
        /// <param name="markNo">������</param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.Account GetAccountByMarkNo(string markNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.AccountByMarkNo", ref Sql) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                Sql = string.Format(Sql, markNo);
                if (this.ExecQuery(Sql) < 0)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return null;
                }
                FS.HISFC.Models.Account.Account account = null;
                //һ������ֻ�ܶ�Ӧһ���ʻ�
                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.CardNO = this.Reader[0].ToString();
                    account.ValidState = (HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[1]));
                    account.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    account.DonateVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    account.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    account.BaseAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    account.DonateAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    account.CouponAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                    account.PassWord = HisDecrypt.Decrypt(this.Reader[8].ToString());
                    account.DayLimit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                    account.ID = this.Reader[10].ToString();
                }
                this.Reader.Close();
                return account;
            }
            catch (Exception ex)
            {
                this.Err = "��������ʧ�ܣ�" + ex.Message;
                return null;
            }

        }

        #endregion

        #region �����ʻ�����
        /// <summary>
        /// ����֤������
        /// </summary>
        /// <param name="idCardNO">֤����</param>
        /// <param name="idCardType">֤������</param>
        /// <returns>-1ʧ��</returns>
        public ArrayList GetAccountByIdNO(string idCardNO, string idCardType)
        {
            string sqlstr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountByIdNO", ref sqlstr) == -1)
            {
                this.Err = "��������ΪFee.Account.SelectAccountByIdNO��Sql���ʧ�ܣ�";
                return null;
            }
            ArrayList al = new ArrayList();
            HISFC.Models.Account.Account account = null;
            try
            {
                sqlstr = string.Format(sqlstr, idCardNO, idCardType);
                if (this.ExecQuery(sqlstr) < 0) return null;

                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.CardNO = this.Reader[0].ToString();
                    if (this.Reader[1] != DBNull.Value) account.ValidState = (HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[1]));
                    if (this.Reader[2] != DBNull.Value) account.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    if (this.Reader[3] != DBNull.Value) account.PassWord = HisDecrypt.Decrypt(this.Reader[3].ToString());
                    if (this.Reader[4] != DBNull.Value) account.DayLimit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    account.ID = this.Reader[5].ToString();
                    account.IsEmpower = NConvert.ToBoolean(this.Reader[6]);
                    al.Add(account);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ���ݳ���" + ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                    this.Reader.Close();
            }
            return al;
        }
        #endregion

        #region ������ѯ

        /// <summary>
        /// ��ȡ��ǰ�շ�Ա�ӵ�ǰʱ�俪ʼ����ǰN�ŷ�Ʊ��Ϣ
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetAccountInvoiceByCount(string operCode, int count, ref DataSet dsResult)
        {
            dsResult = new DataSet();
            string sqlStr = "";

            if (this.Sql.GetCommonSql("FS.OutPatient.Account.Fee.GetInvoicesCountsInfosSinceNow.Select.1", ref sqlStr) == -1)
            {
                this.Err = "û���ҵ�FS.OutPatient.Account.Fee.GetInvoicesCountsInfosSinceNow.Select.1����sql���!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, operCode, count);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err += "ִ��FS.OutPatient.Account.Fee.GetInvoicesCountsInfosSinceNow.Select.1����!";
                    return -1;
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return 1;

        }

        #endregion

        #endregion

        #region ���ݿ��Ź����ȡ����

        /// <summary>
        ///  ���ݿ��Ź�����ҿ�����
        /// </summary>
        /// <param name="markNo">������</param>
        /// <param name="accountCard">��ʵ��</param>
        /// <returns>1:�ɹ� 0��δ���� -1ʧ��</returns>
        public int GetCardByRule(string markNo, ref FS.HISFC.Models.Account.AccountCard accountCard)
        {
            //markNo = FS.FrameWork.Public.String.TakeOffSpecialChar(markNo);
            if (string.IsNullOrEmpty(markNo))
            {
                this.Err = "��������Ч�ľ��￨�ţ�";
                return -1;
            }
            if (!InitReadMark())
            {
                this.Err = "��ʼ����̬��ʧ�ܣ�";
                return -1;
            }
            int resultValue = IreadMarkNO.ReadMarkNOByRule(markNo, ref accountCard);
            this.Err = IreadMarkNO.Error;
            return resultValue;
        }
        #endregion

        #region ��Ȩ
        /// <summary>
        /// ������Ȩ��
        /// </summary>
        /// <param name="accontEmpower">��Ȩʵ��</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int InsertEmpower(AccountEmpower accontEmpower)
        {
            return this.UpdateSingTable("Fee.Account.InsertEmpower", GetEmpowerArgs(accontEmpower));
        }

        /// <summary>
        /// ������Ȩ��
        /// </summary>
        /// <param name="accountEmpower">��Ȩʵ��</param>
        /// <returns>1�ɹ� -1ʧ�� 0û�и��µ���¼</returns>
        public int UpdateEmpower(AccountEmpower accountEmpower)
        {
            return this.UpdateSingTable("Fee.Account.UpdateEmpower", GetEmpowerArgs(accountEmpower));
        }

        /// <summary>
        /// �����ʻ���Ȩ��ʶ
        /// </summary>
        /// <param name="accountNO">�ʺ�</param>
        /// <returns>1�ɹ� -1ʧ�ܡ�0�ʻ����ݷ����仯</returns>
        public int UpdateAccountEmpowerFlag(string accountNO)
        {
            return UpdateSingTable("Fee.Account.UpdateAccountEmpowerFlag", accountNO);
        }

        /// <summary>
        /// ������Ȩ���￨�Ų��ұ���Ȩ��Ϣ
        /// </summary>
        /// <param name="accountNO">��Ȩ�ʺ�</param>
        /// <returns></returns>
        public List<AccountEmpower> QueryEmpowerByAccountNO(string accountNO)
        {
            return this.GetEmpowerList("Fee.Account.SelectEmpowerwhere2", accountNO);
        }

        /// <summary>
        /// ������Ȩ���￨�Ų��ұ���Ȩ��Ϣ
        /// </summary>
        /// <param name="accountNO"></param>
        /// <returns></returns>
        public List<AccountEmpower> QueryAllEmpowerByAccountNO(string accountNO)
        {
            return this.GetEmpowerList("Fee.Account.SelectEmpowerwhere3", accountNO);
        }

        /// <summary>
        /// ���ݱ���Ȩ���￨�Ų�����Ȩ��Ϣ
        /// </summary>
        /// <param name="empowerCardNO">����Ȩ���￨��</param>
        /// <returns>-1ʧ�� 0��������Ч����Ȩ��Ϣ 1�ɹ�</returns>
        public int QueryAccountEmpowerByEmpwoerCardNO(string empowerCardNO, ref AccountEmpower accountEmpower)
        {
            List<AccountEmpower> list = this.GetEmpowerList("Fee.Account.SelectEmpowerwhere1", empowerCardNO);
            if (list == null) return -1;
            if (list.Count == 0)
            {
                this.Err = "�ÿ���������Ч����Ȩ��Ϣ��";
                return 0;
            }
            accountEmpower = list[0];
            return 1;
        }

        /// <summary>
        /// ������Ȩ�ʺźͱ���Ȩ���￨�Ų�����Ȩ��Ϣ
        /// </summary>
        /// <param name="accountNO">��Ȩ�ʺ�</param>
        /// <param name="empowerCardNO">���￨��</param>
        /// <param name="accountEmpower">��Ȩ��Ϣ</param>
        /// <returns>-1ʧ�� 0��������Ȩ��Ϣ 1�ɹ�</returns>
        public int QueryEmpower(string accountNO, string empowerCardNO, ref AccountEmpower accountEmpower)
        {
            List<AccountEmpower> list = this.GetEmpowerList("Fee.Account.SelectEmpowerwhere4", accountNO, empowerCardNO);
            if (list == null) return -1;
            if (list.Count == 0)
            {
                this.Err = "�ÿ���������Ч����Ȩ��Ϣ��";
                return 0;
            }
            accountEmpower = list[0];
            return 1;
        }

        /// <summary>
        /// ������Ȩ��Ϣ���
        /// </summary>
        /// <param name="accountNO">�ʺ�</param>
        /// <param name="empowerCardNO">����Ȩ���￨��</param>
        /// <param name="money">���</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int UpdateEmpowerVacancy(string accountNO, string empowerCardNO, decimal money)
        {
            return this.UpdateSingTable("Fee.Account.UpdateEmpowerVacancy", accountNO, empowerCardNO, money.ToString());
        }

        /// <summary>
        /// ����������Ȩ״̬
        /// </summary>
        /// <param name="accountNO">�ʺ�</param>
        /// <param name="validState">���µ�״̬</param>
        /// <param name="currentState">��ǰ״̬</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int UpdateEmpowerState(string accountNO, HISFC.Models.Base.EnumValidState validState, HISFC.Models.Base.EnumValidState currentState)
        {
            return this.UpdateSingTable("Fee.Account.UpdateEmpowerState", accountNO, ((int)validState).ToString(), ((int)currentState).ToString());
        }

        #endregion

        #region ֤����Ϣ
        /// <summary>
        /// ���뻼��֤����Ϣ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int InsertIdenInfo(FS.HISFC.Models.RADT.Patient p)
        {
            return this.UpdateSingTable("Fee.Account.InsertIdenInfo", p.PID.CardNO, p.IDCardType.ID, p.IDCardType.Name, p.IDCard);
        }

        /// <summary>
        /// ���»���֤����Ϣ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int UpdateIdenInfo(FS.HISFC.Models.RADT.Patient p)
        {
            return this.UpdateSingTable("Fee.Account.UpdateIdenInfo", p.PID.CardNO, p.IDCardType.ID, p.IDCardType.Name, p.IDCard);
        }

        /// <summary>
        /// ֤����Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList QueryIdenInfo(string cardNO)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.QueryIdenInfo", ref sql) == -1)
            {
                this.Err = "��ѯ����ΪFee.Account.QueryIdenInfo��SQL���ʧ�ܣ�";
                return null;
            }
            return this.GetPatientIdenInfo(sql, cardNO);

        }
        /// <summary>
        /// ����CardNO��֤�����������Ƭ
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int DeletePhoto(FS.HISFC.Models.RADT.Patient p)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateIdenInfo.DeletePhoto", ref strSql) == -1)
            {
                this.Err = "��������ΪFee.Account.UpdateIdenInfo.DeletePhoto��Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, p.PID.CardNO, p.IDCardType.ID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����CardNO��֤�����͸�����Ƭ
        /// </summary>
        /// <param name="p"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public int UpdatePhoto(FS.HISFC.Models.RADT.Patient p, byte[] photo)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateIdenInfo.Photo", ref strSql) == -1)
            {
                this.Err = "��������ΪFee.Account.UpdateIdenInfo.Photo��Sql���ʧ�ܣ�";
                return -1;
            }
            return this.InputBlob(string.Format(strSql, p.PID.CardNO, p.IDCardType.ID), photo);
        }

        /// <summary>
        /// ����CardNO��֤�����ͻ�ȡ��Ƭ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="cardType"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public int GetIdenInfoPhoto(string cardNO, string cardType, ref byte[] photo)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetIdenInfo.Photo", ref sql) == -1)
            {
                this.Err = "��ѯ����ΪFee.Account.GetIdenInfo.Photo��SQL���ʧ�ܣ�";
                return -1;
            }

            photo = this.OutputBlob(string.Format(sql, cardNO, cardType));

            return 1;
        }

        #endregion

        #region �����ͬ��λ

        /// <summary>
        /// ע�Ỽ����Ϣ
        /// </summary>
        /// <param name="PatientInfo">�ǼǵĻ�����Ϣ</param>
        /// <returns>0�ɹ� -1ʧ��</returns>
        public int InsertPatientPactInfo(Patient PatientInfo)
        {
            //��ɾ��
            if (PatientInfo.MutiPactInfo == null || PatientInfo.MutiPactInfo.Count == 0)
            {
                return 1;
            }

            string strSql = string.Empty;
            if (this.ExecNoQueryByIndex("RADT.OutPatient.DeleteMutiPactInfo", PatientInfo.PID.CardNO) < 0)
            {
                return -1;
            }
            //ɾ������

            strSql = string.Empty;
            if (Sql.GetSql("RADT.OutPatient.InsertMutiPactInfo", ref strSql) == -1) return -1;
            foreach (FS.HISFC.Models.Base.PactInfo pact in PatientInfo.MutiPactInfo)
            {
                try
                {
                    string[] s = new string[4];
                    try
                    {
                        s[0] = PatientInfo.PID.CardNO; //���￨��
                        s[1] = pact.ID; //��ͬ��λ
                        s[2] = pact.Name; //��ͬ��λ
                        s[3] = pact.ValidState; //״̬
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                    }
                    if (ExecNoQuery(strSql, s) <= 0)
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    Err = "��ֵʱ�����" + ex.Message;
                    WriteErr();
                    return -1;
                }
            }

            return 1;
        }

        public int GetPatientPactInfo(Patient PatientInfo)
        {
            //��ɾ��
            if (PatientInfo == null)
            {
                return -1;
            }

            string strSql = string.Empty;
            if (Sql.GetSql("RADT.OutPatient.QueryMutiPactInfo", ref strSql) == -1) return -1;
            if (this.ExecQuery(strSql, PatientInfo.PID.CardNO) < 0)
            {
                return -1;
            }
            if (this.Reader != null)
            {
                try
                {
                    PatientInfo.MutiPactInfo = new System.Collections.Generic.List<PactInfo>();
                    while (this.Reader.Read())
                    {
                        FS.HISFC.Models.Base.PactInfo pact = new PactInfo();
                        pact.ID = this.Reader[0].ToString();
                        pact.Name = this.Reader[1].ToString();
                        pact.ValidState = this.Reader[2].ToString();
                        pact.Memo = pact.ValidState;//�����б�ѡ��
                        if (Reader.FieldCount > 3)
                        {
                            pact.PayKind.ID = this.Reader[3].ToString();
                        }

                        PatientInfo.MutiPactInfo.Add(pact);
                    }
                }
                catch (Exception e)
                {
                    Err = "��ֵʱ�����" + e.Message;
                    WriteErr();
                    return -1;
                }
                finally
                {
                    this.Reader.Close();
                }
            }

            return 1;
        }

        #endregion

        #region ͨ�������Ų�ѯ����

        /// <summary>
        /// �������е������б�
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="cardType">������ ALLΪȫ��</param>
        /// <param name="state">��Ч״̬ ALLΪȫ��</param>
        /// <returns></returns>
        public ArrayList GetMarkByCardNo(string cardNO, string cardType, string state)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetMarkNoByCardNo", ref Sql) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                Sql = string.Format(Sql, cardNO, cardType, state);
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "���ҿ�ʹ������ʧ�ܣ�";
                    return null;
                }
                ArrayList cardList = new ArrayList();
                FS.FrameWork.Models.NeuObject markCardObj = null;
                while (this.Reader.Read())
                {
                    markCardObj = new NeuObject();
                    //������
                    markCardObj.ID = Reader[0].ToString();
                    //�������
                    markCardObj.Name = Reader[1].ToString();
                    //��״̬����Ч����Ч
                    markCardObj.Memo = Reader[2].ToString();
                    //������
                    markCardObj.User01 = Reader[3].ToString();

                    cardList.Add(markCardObj);
                }
                return cardList;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// ���������Ż�ȡ�սɷ���Ϣ
        /// </summary>
        /// <param name="markNO"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCardFee> QueryCardFeebyMCardNo(string markNO)
        {
            return null;
        }

        #endregion

        #region ͣ��Ʊ
        /// <summary>
        /// ����ͣ��Ʊ������Ϣ// {17D86AD6-A28C-4518-951C-EE0F3504598B}
        /// </summary>
        /// <param name="ParkingTicketFeeInfo"></param>
        /// <returns></returns>
        public int InsertParkingTicketInfo(ParkingTicketFeeInfo ParkingTicketFeeInfo)
        {
            // {23F37636-DC34-44a3-A13B-071376265450}
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new FS.HISFC.Models.Base.Employee();
            }
            FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;
            if (dept == null)
            {
                dept = new FS.HISFC.Models.Base.Department();
            }
            if (string.IsNullOrEmpty(ParkingTicketFeeInfo.Hospital_id)) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                string hospitalid = dept.HospitalID;
                string hospitalname = dept.HospitalName;
                ParkingTicketFeeInfo.Hospital_id = hospitalid;
                ParkingTicketFeeInfo.Hospital_name = hospitalname;
            }
            string strSql = string.Empty;
            if (this.UpdateSingTable("Fee.Account.InsertParkingTicketInfo", ParkingTicketFeeInfo.InvoiceNo, ((int)ParkingTicketFeeInfo.TransType).ToString(),
                ParkingTicketFeeInfo.ItemCode, ParkingTicketFeeInfo.ItemName, ParkingTicketFeeInfo.Unit, ParkingTicketFeeInfo.UnitPrice.ToString(), ParkingTicketFeeInfo.Qty.ToString(),
                ParkingTicketFeeInfo.TotCost.ToString(), ParkingTicketFeeInfo.PayMode.ID, ParkingTicketFeeInfo.TicketNo, ParkingTicketFeeInfo.OldInvoiceNo, ParkingTicketFeeInfo.CancelDate.ToString(),
                ParkingTicketFeeInfo.OldTicketNo, ParkingTicketFeeInfo.Memo, ParkingTicketFeeInfo.Flag1, ParkingTicketFeeInfo.Flag2, ParkingTicketFeeInfo.Flag3, ((int)ParkingTicketFeeInfo.ValidState).ToString(),
                ParkingTicketFeeInfo.OperEnvironment.ID, ParkingTicketFeeInfo.OperEnvironment.OperTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(ParkingTicketFeeInfo.IsBalance).ToString(),
                ParkingTicketFeeInfo.BalanceNo, ParkingTicketFeeInfo.BalanceEnvironment.ID, ParkingTicketFeeInfo.BalanceEnvironment.OperTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(ParkingTicketFeeInfo.IsCheck).ToString(),
                ParkingTicketFeeInfo.CheckEnvironment.ID, ParkingTicketFeeInfo.CheckEnvironment.OperTime.ToString(), ParkingTicketFeeInfo.Hospital_id, ParkingTicketFeeInfo.Hospital_name) < 0) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ��ѯͣ��Ʊ������Ϣ// {17D86AD6-A28C-4518-951C-EE0F3504598B}
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="ticketNo"></param>
        /// <param name="memo"></param>
        /// <param name="ticketState"></param>
        /// <param name="operCode"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList QueryParkingTicketInfo(string itemCode, string ticketNo, string memo, string ticketState, string operCode, string invoiceNo, string beginTime, string endTime)
        {
            string sqlWhere = "";
            sqlWhere = " where oper_date >= to_date('" + beginTime + "','yyyy-mm-dd hh24:mi:ss') ";
            sqlWhere += " and oper_date <= to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')";
            if (!string.IsNullOrEmpty(itemCode))
            {
                sqlWhere += " and item_code = '" + itemCode + "'";
            }
            if (!string.IsNullOrEmpty(ticketNo))
            {
                sqlWhere += " and ticket_no like '%" + ticketNo + "%'";
            }
            if (!string.IsNullOrEmpty(memo))
            {
                sqlWhere += " and remark = '%" + memo + "%'";
            }
            if (!string.IsNullOrEmpty(ticketState))
            {
                sqlWhere += " and (TRANS_TYPE = '" + ticketState + "' or 'ALL' = '" + ticketState + "')";
            }
            if (!string.IsNullOrEmpty(operCode))
            {
                sqlWhere += " and (oper_code = '" + operCode + "' or 'ALL' = '" + operCode + "')";
            }
            if (!string.IsNullOrEmpty(invoiceNo))
            {
                invoiceNo = invoiceNo.PadLeft(12, '0');
                sqlWhere += " and invoice_no = '" + invoiceNo + "'";
            }
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.QueryParkingTicketInfo", ref strSql) == -1)
            {
                this.Err = "��������ΪFee.Account.QueryParkingTicketInfo��Sql���ʧ�ܣ�";
                return null;
            }
            strSql = strSql + " " + sqlWhere;

            ArrayList al = new ArrayList();
            try
            {
                if (this.ExecQuery(strSql) == -1) return null;
                while (this.Reader.Read())
                {
                    ParkingTicketFeeInfo parkingTicketFeeInfo = new ParkingTicketFeeInfo();
                    parkingTicketFeeInfo.InvoiceNo = Reader[0].ToString();
                    parkingTicketFeeInfo.TransType = Reader[1].ToString() == "1" ? TransTypes.Positive : TransTypes.Negative;
                    parkingTicketFeeInfo.ItemCode = Reader[2].ToString();
                    parkingTicketFeeInfo.ItemName = Reader[3].ToString();
                    parkingTicketFeeInfo.Unit = Reader[4].ToString();
                    parkingTicketFeeInfo.UnitPrice = NConvert.ToDecimal(Reader[5].ToString());
                    parkingTicketFeeInfo.Qty = NConvert.ToDecimal(Reader[6].ToString());
                    parkingTicketFeeInfo.TotCost = NConvert.ToDecimal(Reader[7].ToString());
                    parkingTicketFeeInfo.PayMode.ID = Reader[8].ToString();
                    parkingTicketFeeInfo.PayMode.Name = Reader[9].ToString();
                    parkingTicketFeeInfo.TicketNo = Reader[10].ToString();
                    parkingTicketFeeInfo.OldInvoiceNo = Reader[11].ToString();
                    parkingTicketFeeInfo.CancelDate = NConvert.ToDateTime(Reader[12].ToString());
                    parkingTicketFeeInfo.OldTicketNo = Reader[13].ToString();
                    parkingTicketFeeInfo.Memo = Reader[14].ToString();
                    parkingTicketFeeInfo.Flag1 = Reader[15].ToString();
                    parkingTicketFeeInfo.Flag2 = Reader[16].ToString();
                    parkingTicketFeeInfo.Flag3 = Reader[17].ToString();
                    parkingTicketFeeInfo.ValidState = Reader[18].ToString() == "1" ? EnumValidState.Valid : EnumValidState.Invalid;
                    parkingTicketFeeInfo.OperEnvironment.ID = Reader[19].ToString();
                    parkingTicketFeeInfo.OperEnvironment.Name = Reader[20].ToString();
                    parkingTicketFeeInfo.OperEnvironment.OperTime = NConvert.ToDateTime(Reader[21].ToString());
                    parkingTicketFeeInfo.IsBalance = NConvert.ToBoolean(Reader[22].ToString());
                    parkingTicketFeeInfo.BalanceNo = Reader[23].ToString();
                    parkingTicketFeeInfo.BalanceEnvironment.ID = Reader[24].ToString();
                    parkingTicketFeeInfo.BalanceEnvironment.Name = Reader[25].ToString();
                    parkingTicketFeeInfo.BalanceEnvironment.OperTime = NConvert.ToDateTime(Reader[26].ToString());
                    parkingTicketFeeInfo.IsCheck = NConvert.ToBoolean(Reader[27].ToString());
                    parkingTicketFeeInfo.CheckEnvironment.ID = Reader[28].ToString();
                    parkingTicketFeeInfo.CheckEnvironment.Name = Reader[29].ToString();
                    parkingTicketFeeInfo.CheckEnvironment.OperTime = NConvert.ToDateTime(Reader[30].ToString());

                    al.Add(parkingTicketFeeInfo);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }

            return al;
        }


        public int InsertExpItemMedical(FS.HISFC.Models.RADT.PatientInfo patient, List<ItemMedicalDetail> details)
        {

            string Sql = string.Empty;

            string headSql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.InsertExpItems", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.InsertExpItems ��Sql���ʧ�ܣ�";
                return -1;
            }

            if (this.Sql.GetSql("Fee.Account.ItemMedical.InsertHeadExpItems", ref headSql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.InsertHeadExpItems ��Sql���ʧ�ܣ�";
                return -1;
            }

            string sequece = this.GetSequence("Fee.Account.GetItemMedicalHeadNO");

            headSql = string.Format(headSql, sequece, patient.PID.CardNO, patient.Name, patient.ExtendFlag1, patient.ExtendFlag2, patient.Memo, (this.Operator as FS.HISFC.Models.Base.Employee).ID);

            if (this.ExecNoQuery(headSql) <= 0) { return -1; }


            try
            {
                foreach (ItemMedicalDetail item in details)
                {

                    string sqlstr = string.Format(Sql, patient.PID.CardNO,
                                                      patient.Name,//����
                                                      item.ItemCode,//��Ŀ����
                                                      item.ItemName,//��Ŀ����
                                                      item.ItemSubcode,//����Ŀ����
                                                      item.ItemSubname,
                                                      item.UnitPrice,
                                                      item.ItemNum,
                                                      item.ItemNum,
                                                      0,
                                                      item.UnitPrice * item.ItemNum,
                                                      patient.Memo,
                                                      item.OperEnvironment.ID,
                                                      item.OperEnvironment.OperTime.ToString(),
                                                      item.CreateEnvironment.ID,
                                                      item.CreateEnvironment.OperTime.ToString(),
                                                      item.ItemMediacl.PackageId,
                                                      item.ItemMediacl.PackageName,
                                                      sequece
                                                   );

                    if (this.ExecNoQuery(sqlstr) <= 0) { return -1; }
                }
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }


            return 1;
        }



        public List<ExpItemMedical> QueryExpItemMedical(string sql)
        {
            if (this.ExecQuery(sql) < 0) return new List<ExpItemMedical>();

            List<ExpItemMedical> list = new List<ExpItemMedical>();
            ExpItemMedical item = null;

            try
            {
                while (this.Reader.Read())
                {
                    item = new ExpItemMedical();
                    item.ClinicCode = this.Reader[0].ToString();
                    item.CardNo = this.Reader[1].ToString();
                    item.PatientName = this.Reader[2].ToString();
                    item.ItemCode = this.Reader[3].ToString();
                    item.ItemName = this.Reader[4].ToString();
                    item.ItemSubcode = this.Reader[5].ToString();
                    item.ItemSubname = this.Reader[6].ToString();
                    item.UnitPrice = NConvert.ToDecimal(this.Reader[7].ToString());
                    item.Qty = int.Parse(this.Reader[8].ToString());
                    item.RtnQty = int.Parse(this.Reader[9].ToString());
                    item.ConfirmQty = int.Parse(this.Reader[10].ToString());
                    item.TotPrice = NConvert.ToDecimal(this.Reader[11]);
                    item.Memo = this.Reader[12].ToString();
                    item.OperEnvironment.ID = this.Reader[13].ToString();
                    item.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[14]);
                    item.CreateEnvironment.ID = this.Reader[15].ToString();
                    item.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[16]);
                    item.PackageId = this.Reader[17].ToString();
                    item.PackageName = this.Reader[18].ToString();
                    item.CancelFlag = this.Reader[19].ToString();
                    item.CancelEnvironment.ID = this.Reader[20].ToString();
                    item.CancelEnvironment.OperTime = NConvert.ToDateTime(this.Reader[21]);
                    item.ItemMedicalHeadNo = this.Reader[22].ToString();

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return new List<ExpItemMedical>();
            }

            return list;


        }

        /// <summary>
        /// ��ѯδ������Ŀ����>0
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public List<ExpItemMedical> QueryExpItemMedicalByCardNo(string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.QueryExpItemMedicalByCardNo", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.QueryExpItemMedicalByCardNo ��Sql���ʧ�ܣ�";
                return null;
            }
            Sql = string.Format(Sql, cardNo);
            return QueryExpItemMedical(Sql);
        }

        /// <summary>
        /// ��ѯ��������0����Ŀ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public List<ExpItemMedical> QueryExpItemMedicalConsZeroByCardNo(string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.QueryExpItemMedicalConsZeroByCardNo", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.QueryExpItemMedicalConsZeroByCardNo ��Sql���ʧ�ܣ�";
                return null;
            }
            Sql = string.Format(Sql, cardNo);
            return QueryExpItemMedical(Sql);

        }

        /// <summary>
        /// ��ѯ������Ŀ
        /// </summary>
        /// <returns></returns>
        public List<ExpItemMedical> QueryExpItemMedicalALLByCardNo(string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.SelectALL", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ExpItemMedical.SelectALL ��Sql���ʧ�ܣ�";
                return null;
            }
            string Where = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.Where1", ref Where) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ExpItemMedical.Where1 ��Sql���ʧ�ܣ�";
                return null;
            }

            Sql = Sql + Where;

            Sql = string.Format(Sql, cardNo);
            return QueryExpItemMedical(Sql);

        }


        /// <summary>
        /// ���ݾ��￨�Ŵ���ʱ�䷶Χ��ѯ������Ŀ
        /// </summary>
        /// <returns></returns>
        public List<ExpItemMedical> QueryExpItemMedicalALLByCardNoAndTime(string cardNo, string timeStar, string timeEnd)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.SelectALL", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ExpItemMedical.SelectALL ��Sql���ʧ�ܣ�";
                return null;
            }
            string Where = string.Empty;

            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.Where2", ref Where) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ExpItemMedical.Where2 ��Sql���ʧ�ܣ�";
                return null;
            }

            Sql = Sql + Where;

            Sql = string.Format(Sql, cardNo, timeStar, timeEnd);
            return QueryExpItemMedical(Sql);

        }




        /// <summary>
        /// �����ײͰ���ϸ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="packageid"></param>
        /// <returns></returns>
        public bool UpdateCancelFlag(string cardNo, string packageid,string headno)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.UpdateCancelFlag", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ExpItemMedical.UpdateCancelFlag ��Sql���ʧ�ܣ�";
                return false;
            }

            string sqlhead = string.Empty;

            if (!string.IsNullOrEmpty(headno))
            {

                if (this.Sql.GetSql("Fee.Account.ExpItemMedical.UpdateHeadCancelFlag", ref sqlhead) == -1)
                {
                    this.Err = this.Err = "��������Ϊ Fee.Account.ExpItemMedical.UpdateHeadCancelFlag ��Sql���ʧ�ܣ�";
                    return false;
                }

            }

            string ss = @" update exp_itemmedicalhead set UNIT_PRICE = UNIT_PRICE - '3001'  where CLINIC_CODE='1000000007' and UNIT_PRICE - '3001' >=0 ";

           
            Sql = string.Format(Sql, packageid, cardNo, FS.FrameWork.Management.Connection.Operator.ID);

            sqlhead = string.Format(sqlhead, cardNo, headno, FS.FrameWork.Management.Connection.Operator.ID);

            if (string.IsNullOrEmpty(sqlhead))
            {
                this.ExecNoQuery(ss);
                return this.ExecNoQuery(Sql) > 0;
            }
            else
            {


                if (this.ExecNoQuery(Sql) > 0) { return this.ExecNoQuery(sqlhead) > 0; }
                else
                {
                    return false;
                }
            }
           

            
        }


        public bool UpdateExpItemMedical(string cardNo, List<ExpItemMedical> details)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.UpdateExpItemMedical", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ExpItemMedical.UpdateExpItemMedical ��Sql���ʧ�ܣ�";
                return false;
            }

            try
            {
                foreach (ExpItemMedical item in details)
                {

                    string sqlstr = string.Format(Sql, cardNo,
                                                      item.ClinicCode,//����
                                                      item.RtnQty,//��Ŀ����
                                                      item.ConfirmQty,//��Ŀ����
                                                      item.Memo,
                                                      item.CreateEnvironment.ID
                                                   );

                    if (this.ExecNoQuery(sqlstr) <= 0) { return false; }
                }
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return false;
            }

            return true;

        }


        /// <summary>
        /// ��ѯ���пطѰ�(��Ч)
        /// </summary>
        /// <param name="accountNO"></param>
        /// <returns></returns>
        public List<ItemMedical> QueryAllItemMedical(string isvalid)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.SelectALL", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.SelectALL ��Sql���ʧ�ܣ�";
                return null;
            }

            string where = "";
            if (!string.IsNullOrEmpty(isvalid) && isvalid != "ALL")
            {
                where = "where VALID_FLAG =" + isvalid;
            }

            Sql = Sql + where;

            return QueryItemMedical(Sql);

        }


        public List<ItemMedical> QueryItemMedical(string sql)
        {

            if (this.ExecQuery(sql) < 0) return null;
            List<ItemMedical> list = new List<ItemMedical>();
            ItemMedical item = null;
            try
            {
                while (this.Reader.Read())
                {
                    item = new ItemMedical();
                    item.PackageId = this.Reader[0].ToString();
                    item.PackageName = this.Reader[1].ToString();
                    item.PackageCost = NConvert.ToDecimal(this.Reader[2]);
                    item.SpellCode = this.Reader[3].ToString();
                    item.InputCode = this.Reader[4].ToString();
                    item.SortId = this.Reader[5].ToString();
                    item.ValidState = this.Reader[6].ToString();
                    item.Memo = this.Reader[7].ToString();
                    item.OperEnvironment.ID = this.Reader[8].ToString();
                    item.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[9]);
                    item.CreateEnvironment.ID = this.Reader[10].ToString();
                    item.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[11]);

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return list;

        }


        public ItemMedical QueryAllItemMedicalById(string packageid)
        {

            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.SelectALL", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.SelectALL ��Sql���ʧ�ܣ�";
                return null;
            }

            string sqlwhere = string.Empty;

            if (this.Sql.GetSql("Fee.Account.ItemMedical.Where1", ref sqlwhere) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.Where1 ��Sql���ʧ�ܣ�";
                return null;
            }

            Sql += sqlwhere;

            if (this.ExecQuery(Sql, packageid) < 0) return null;
            List<ItemMedical> list = new List<ItemMedical>();
            ItemMedical item = null;
            try
            {
                while (this.Reader.Read())
                {
                    item = new ItemMedical();
                    item.PackageId = this.Reader[0].ToString();
                    item.PackageName = this.Reader[1].ToString();
                    item.PackageCost = NConvert.ToDecimal(this.Reader[2]);
                    item.SpellCode = this.Reader[3].ToString();
                    item.InputCode = this.Reader[4].ToString();
                    item.SortId = this.Reader[5].ToString();
                    item.ValidState = this.Reader[6].ToString();
                    item.Memo = this.Reader[7].ToString();
                    item.OperEnvironment.ID = this.Reader[8].ToString();
                    item.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[9]);
                    item.CreateEnvironment.ID = this.Reader[10].ToString();
                    item.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[11]);

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return list[0] as ItemMedical;
        }

        /// <summary>
        /// �����װ�
        /// </summary>
        /// <param name="mediacl"></param>
        /// <returns></returns>
        public int AddMedicalPackage(ItemMedical mediacl)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.Insert", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.Insert ��Sql���ʧ�ܣ�";
                return 0;
            }


            Sql = string.Format(Sql, mediacl.PackageName, mediacl.PackageCost, mediacl.SpellCode, mediacl.InputCode, mediacl.ValidState, mediacl.Memo, mediacl.OperEnvironment.ID, mediacl.OperEnvironment.OperTime, mediacl.CreateEnvironment.ID, mediacl.CreateEnvironment.OperTime);

            return this.ExecNoQuery(Sql);

        }

        /// <summary>
        /// �޸��װ�
        /// </summary>
        /// <param name="mediacl"></param>
        /// <returns></returns>
        public int UpdateMedicalPackage(ItemMedical mediacl)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.Update", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.Update ��Sql���ʧ�ܣ�";
                return 0;
            }

            Sql = string.Format(Sql, mediacl.PackageId, mediacl.PackageName, mediacl.PackageCost, mediacl.SpellCode, mediacl.ValidState, mediacl.Memo, mediacl.OperEnvironment.ID, mediacl.OperEnvironment.OperTime);

            return this.ExecNoQuery(Sql);
        }





        /// <summary>
        /// ��ѯ���пطѰ���ϸ
        /// </summary>
        /// <param name="packid"></param>
        /// <returns></returns>
        public List<ItemMedicalDetail> QueryItemMedicalDetailById(string packid)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.SelectDetailByID", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.SelectDetailByID ��Sql���ʧ�ܣ�";
                return null;
            }
            if (this.ExecQuery(Sql, packid) < 0) return null;
            List<ItemMedicalDetail> list = new List<ItemMedicalDetail>();
            ItemMedicalDetail itemdetail = null;
            try
            {
                while (this.Reader.Read())
                {
                    itemdetail = new ItemMedicalDetail();
                    itemdetail.PackageId = this.Reader[0].ToString();
                    itemdetail.SequenceNo = this.Reader[1].ToString();
                    itemdetail.ItemCode = this.Reader[2].ToString();
                    itemdetail.ItemName = this.Reader[3].ToString();
                    itemdetail.ItemNum = int.Parse(this.Reader[4].ToString());
                    itemdetail.ItemSubcode = this.Reader[5].ToString();
                    itemdetail.ItemSubname = this.Reader[6].ToString();
                    itemdetail.UnitPrice = NConvert.ToDecimal(this.Reader[7]);
                    itemdetail.Memo = this.Reader[8].ToString();
                    itemdetail.OperEnvironment.ID = this.Reader[9].ToString();
                    itemdetail.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[10]);
                    itemdetail.CreateEnvironment.ID = this.Reader[11].ToString();
                    itemdetail.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[12]);
                    itemdetail.MedicalDetailId = this.Reader[13].ToString();
                    list.Add(itemdetail);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return list;

        }

        public int UpdateItemMedicalDetail(ItemMedicalDetail detail)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.UpdateItemMedicalDetail", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.UpdateItemMedicalDetail ��Sql���ʧ�ܣ�";
                return -1;
            }
            Sql = string.Format(Sql, detail.MedicalDetailId, detail.SequenceNo, detail.ItemCode, detail.ItemName, detail.ItemNum, detail.ItemSubcode, detail.ItemSubname, detail.UnitPrice, detail.Memo, detail.OperEnvironment.ID, detail.OperEnvironment.OperTime);

            return this.ExecNoQuery(Sql);
        }

        public int InsertItemMedicalDetail(ItemMedicalDetail detail)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.InsertItemMedicalDetail", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.InsertItemMedicalDetail ��Sql���ʧ�ܣ�";
                return -1;
            }
            Sql = string.Format(Sql, detail.PackageId, detail.SequenceNo, detail.ItemCode, detail.ItemName, detail.ItemNum, detail.ItemSubcode, detail.ItemSubname, detail.UnitPrice, detail.Memo, detail.OperEnvironment.ID, detail.OperEnvironment.OperTime, detail.CreateEnvironment.ID, detail.CreateEnvironment.OperTime);

            return this.ExecNoQuery(Sql);
        }

        public int DeleteItemMedicalDetail(ItemMedicalDetail detail)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.DeleteItemMedicalDetail", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.ItemMedical.DeleteItemMedicalDetail ��Sql���ʧ�ܣ�";
                return -1;
            }
            Sql = string.Format(Sql, detail.MedicalDetailId);

            return this.ExecNoQuery(Sql);
        }



        /// <summary>
        /// ����ͣ��Ʊ��Ʊ״̬// {17D86AD6-A28C-4518-951C-EE0F3504598B}
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="state"></param>
        /// <param name="dtTime"></param>
        /// <returns></returns>
        public int UpdateTicketInfoState(string invoiceNo, string state, string dtTime)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateTicketInfoState", ref strSql) == -1)
            {
                this.Err = "��������ΪFee.Account.UpdateTicketInfoState��Sql���ʧ�ܣ�";
                return -1;
            }
            strSql = string.Format(strSql, invoiceNo, state, dtTime);

            return this.ExecNoQuery(strSql);

        }
        /// <summary>
        /// ����ͣ��Ʊʣ������
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public int UpdateTicketTotalQty(string operCode, string itemCode, string qty, string getQty)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateTicketTotalQty", ref strSql) == -1)
            {
                this.Err = "��������ΪFee.Account.UpdateTicketTotalQty��Sql���ʧ�ܣ�";
                return -1;
            }
            strSql = string.Format(strSql, operCode, itemCode, qty, getQty, FS.FrameWork.Management.Connection.Operator.ID);

            return this.ExecNoQuery(strSql);

        }
        /// <summary>
        /// ����ͣ��Ʊ������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertTicketTotal(FS.HISFC.Models.Base.Const obj)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.InsertTicketTotal", ref strSql) == -1)
            {
                this.Err = "��������ΪFee.Account.InsertTicketTotal��Sql���ʧ�ܣ�";
                return -1;
            }
            strSql = string.Format(strSql, obj.ID, obj.Name, obj.UserCode, obj.Memo, obj.SpellCode, obj.SpellCode, obj.User01, obj.User02, obj.User03, obj.OperEnvironment.ID, obj.OperEnvironment.OperTime.ToString());

            return this.ExecNoQuery(strSql);

        }
        #endregion

        #region �ֽ�������[�����ײ͡�������㡢��ֵ��סԺ������ֽ������ۼ����]
        //{F166B18B-62E3-4835-A729-4CA384F9ADEE}

        public string GetCashConponVacancySeq()
        {
            return this.GetSequence("Fee.Account.CashCouponRecord");
        }

        /// <summary>
        /// �����ֽ������ֻ�����Ϣ
        /// </summary>
        /// <param name="cashCoupon"></param>
        /// <returns></returns>
        public int InsertCashCouponVacancy(HISFC.Models.Account.CashCoupon cashCoupon)
        {
            try
            {
                string strSql = string.Empty;

                if (this.Sql.GetCommonSql("Fee.Account.InsertCashCouponVacancy", ref strSql) == -1)
                {
                    this.Err = "��������ΪFee.Account.InsertCashCouponVacancy��Sql���ʧ�ܣ�";
                    return -1;
                }

                string[] couponParams = this.getCouponParams(cashCoupon);

                strSql = string.Format(strSql, couponParams);

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// �����ֽ������ֻ�����Ϣ
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public int UpdateCashCouponVacancy(string cardno, decimal coupon)
        {
            try
            {
                string strSql = string.Empty;

                if (this.Sql.GetCommonSql("Fee.Account.UpdateCashCouponVacancy", ref strSql) == -1)
                {
                    this.Err = "��������ΪFee.Account.UpdateCashCouponVacancy��Sql���ʧ�ܣ�";
                    return -1;
                }

                strSql = string.Format(strSql, cardno, coupon.ToString(), coupon.ToString());

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// �����ֽ���������ϸ��¼
        /// </summary>
        /// <param name="cashCouponRecord"></param>
        /// <returns></returns>
        public int InsertCashCouponRecord(HISFC.Models.Account.CashCouponRecord cashCouponRecord)
        {
            try
            {
                string strSql = string.Empty;

                if (this.Sql.GetCommonSql("Fee.Account.InsertCashCouponRecord", ref strSql) == -1)
                {
                    this.Err = "��������ΪFee.Account.InsertCashCouponRecord��Sql���ʧ�ܣ�";
                    return -1;
                }

                string[] couponParams = this.getCouponParams(cashCouponRecord);

                strSql = string.Format(strSql, couponParams);

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ��ȡ�ֽ������ֻ��ܲ���
        /// </summary>
        /// <param name="cashCoupon"></param>
        /// <returns></returns>
        public string[] getCouponParams(HISFC.Models.Account.CashCoupon cashCoupon)
        {
            string[] couponParams = {
                                        cashCoupon.CardNo,
                                        cashCoupon.CouponVacancy.ToString(),
                                        cashCoupon.CouponAccumulate.ToString()
                                    };
            return couponParams;

        }

        /// <summary>
        /// ��ȡ�ֽ���������ϸ����
        /// </summary>
        /// <param name="cashCouponRecord"></param>
        /// <returns></returns>
        public string[] getCouponParams(HISFC.Models.Account.CashCouponRecord cashCouponRecord)
        {
            string[] couponRecordParams = {
                                        cashCouponRecord.ID,
                                        cashCouponRecord.CardNo,
                                        cashCouponRecord.Coupon.ToString(),
                                        cashCouponRecord.CouponVacancy.ToString(),
                                        cashCouponRecord.InvoiceNo,
                                        cashCouponRecord.CouponType,
                                        cashCouponRecord.Memo,
                                        cashCouponRecord.OperEnvironment.ID,
                                        cashCouponRecord.OperEnvironment.OperTime.ToString()
                                    };
            return couponRecordParams;

        }

        #endregion


        /// <summary>
        /// // {473865F9-C2E6-4f05-BEB3-7CD1F0349126} ��ɽ����ҽ�����θ���
        /// ��ѯĳ������ĳ��ʱ����ڵ�ҽԺ�渶��������
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccountCardFeeByPubAndCardNoAndDate(string cardNo, DateTime dtBegin, DateTime dtEnd, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;

            string strWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.CardFee.Where.ByPubAndCardNoAndDate", ref strWhere) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.Where.ByPubAndCardNoAndDate ��Sql���ʧ�ܣ�";
                return -1;
            }
            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo, dtBegin.ToString(), dtEnd.ToString());

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }

        /// <summary>
        /// ���¹Һ����ѵ��籣״̬// {473865F9-C2E6-4f05-BEB3-7CD1F0349126}
        /// </summary>
        /// <param name="cardFee"></param>
        /// <returns></returns>
        public int UpdateAccountCardFeeSiState(AccountCardFee cardFee)
        {
            if (cardFee == null)
            {
                return -1;
            }

            if (string.IsNullOrEmpty(cardFee.InvoiceNo))
            {
                this.Err = "��Ʊ��ˮ��Ϊ�գ�";
                return -1;
            }

            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.CardFee.UpdateSiState", ref Sql) == -1)
            {
                this.Err = this.Err = "��������Ϊ Fee.Account.CardFee.UpdateSiState ��Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql,
                    cardFee.SiFlag,
                    cardFee.SiBalanceNO,

                    cardFee.InvoiceNo,
                    ((int)cardFee.TransType).ToString(),
                    ((int)cardFee.FeeType).ToString()
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }


    }

    /// <summary>
    /// ��ȡ���￨�Žӿڣ����ݿ��Ź����ȡ���źͿ�����
    /// </summary>
    public interface IReadMarkNO
    {
        /// <summary>
        /// ���ݱ��ؿ��Ź����ȡ��ʵ��
        /// </summary>
        /// <param name="markNO">����</param>
        /// <returns>-1 ʧ�� 0��������ȷ����û�з��� 1����</returns>
        int ReadMarkNOByRule(string markNO, ref FS.HISFC.Models.Account.AccountCard accountCard);
        /// <summary>
        /// ����
        /// </summary>
        string Error
        {
            get;
            set;
        }
    }
}
