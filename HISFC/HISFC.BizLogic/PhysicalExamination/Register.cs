using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.BizLogic.PhysicalExamination.Enum;
namespace FS.HISFC.BizLogic.PhysicalExamination
{
    /// <summary>
    /// Register<br></br>
    /// [��������: ���Ǽ���]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-03-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Register : FS.FrameWork.Management.Database
    {
        #region ������Ϣ
        #region ��ѯһ��ʱ���������Ա��Ϣ ���� ��̬����
        /// <summary>
        /// ��ѯһ��ʱ���������Ա��Ϣ ���� ��̬����
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(string BeginTime, string EndTime)
        {
            string strSql = GetExamPatientSql();
            if (strSql == null)
            {
                return null;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkPatient.All", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return null;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, BeginTime, EndTime);
            //ȡ��λ��Ϣ����
            return this.myGetItem(strSQL);
        }
        #endregion

        #region ���ݿ��Ż�ȡ���˻�����Ϣ  ע�ⲻ�ǵǼ���Ϣ
        /// <summary>
        /// ���ݿ��Ż�ȡ���˻�����Ϣ  ע�ⲻ�ǵǼ���Ϣ 
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(string CardNo)
        {
            string strSql = GetExamPatientSql();
            if (strSql == null)
            {
                return null;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkPatient.ByCardNO", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return null;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, CardNo);
            //ȡ��λ��Ϣ����
            return this.myGetItem(strSQL);
        }
        #endregion

        #region ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet
        /// <summary>
        /// ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryPatient(string BeginTime, string EndTime, ref System.Data.DataSet ds)
        {
            string strSql = GetExamPatientSql();
            if (strSql == null)
            {
                return -1;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkPatient.All", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return -1;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, BeginTime, EndTime);
            //ȡ��λ��Ϣ����
            return this.ExecQuery(strSQL, ref ds);
        }
        #endregion

        #region  ���ӻ�ɾ��һ������
        /// <summary>
        /// ���ӻ�ɾ��һ������
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int AddOrUpdate(FS.HISFC.Models.PhysicalExamination.Register register)
        {
            if (this.UpdateInfo(register) <= 0)
            {
                if (InsertInfo(register) <= 0)
                {

                    return -1;
                }
            }
            return 1;
        }
        #endregion

        #region  ȡ��λ��Ϣ�����б�������һ�����߶���
        /// <summary>
        /// ȡ��λ��Ϣ�����б�������һ�����߶���
        /// ˽�з����������������е���
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>��λ��Ϣ��������</returns>
        private ArrayList myGetItem(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.PhysicalExamination.Register ChkRegister = null; //��λ��Ŀ��Ϣʵ��
            //ִ�в�ѯ���
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "��õ�λ��Ŀ��Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    ChkRegister = new FS.HISFC.Models.PhysicalExamination.Register();
                    ChkRegister.PID.CardNO = this.Reader[0].ToString();//���￨��
                    ChkRegister.Name = this.Reader[1].ToString();//����
                    ChkRegister.SpellCode = this.Reader[2].ToString();//ƴ����
                    ChkRegister.WBCode = this.Reader[3].ToString(); //���
                    ChkRegister.IDCard = this.Reader[4].ToString();//���֤
                    ChkRegister.Profession.ID = this.Reader[5].ToString();//ְҵ   
                    ChkRegister.Company.ID = this.Reader[6].ToString();//��쵥λ
                    ChkRegister.PhoneBusiness = this.Reader[7].ToString();//��λ�绰
                    ChkRegister.BusinessZip = this.Reader[8].ToString();//��λ�ʱ�
                    ChkRegister.AddressHome = this.Reader[9].ToString();//���ڻ��ͥ����
                    ChkRegister.PhoneHome = this.Reader[10].ToString();//��ͥ�绰
                    ChkRegister.HomeZip = this.Reader[11].ToString();//���ڻ��ͥ��������
                    ChkRegister.Nationality.ID = this.Reader[12].ToString();//����
                    ChkRegister.Kin.Name = this.Reader[13].ToString();//��ϵ������
                    ChkRegister.Kin.RelationPhone = this.Reader[14].ToString();//��ϵ�˵绰
                    ChkRegister.Kin.RelationAddress = this.Reader[15].ToString();//��ϵ��סַ
                    ChkRegister.Kin.RelationLink = this.Reader[16].ToString();//��ϵ�˹�ϵ
                    ChkRegister.MaritalStatus.ID = this.Reader[17].ToString();//����״��
                    ChkRegister.Country.ID = this.Reader[18].ToString(); //���� 
                    ChkRegister.Pact.PayKind.ID = this.Reader[19].ToString();//�������
                    ChkRegister.Pact.PayKind.Name = this.Reader[20].ToString();//����������� 
                    ChkRegister.SSN = this.Reader[21].ToString(); //ҽ��֤��
                    ChkRegister.DIST = this.Reader[22].ToString();//������
                    ChkRegister.AnaphyFlag = this.Reader[23].ToString();//ҩ�����
                    ChkRegister.Disease.ID = this.Reader[24].ToString();//��Ҫ����
                    ChkRegister.ArchivesNO = this.Reader[25].ToString();//����������
                    ChkRegister.Company.Name = this.Reader[26].ToString();//��쵥λ
                    ChkRegister.IdentityLevel = this.Reader[27].ToString();//������
                    ChkRegister.Sex.ID = this.Reader[30].ToString(); //�Ա�
                    ChkRegister.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[31].ToString()); //�Ա�
                    //ȡ��ѯ����еļ�¼

                    al.Add(ChkRegister);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��õ�λ��Ŀ��Ϣ��Ϣʱ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            this.Reader.Close();

            ;
            return al;
        }
        #endregion

        #region ��ȡ�Ǽ����
        /// <summary>
        /// ��ȡ�Ǽ����
        /// </summary>
        /// <returns></returns>
        public string GetExamSequence()
        {
            string str = GetSequence("Exami.ChkPatient.GetSEQ");
            str = str.PadLeft(10, '0');
            str = "TJ01" + str;
            return str;
        }
        #endregion

        #region  ɾ��һ������
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="register">���Ǽ�ʵ��</param>
        /// <returns></returns>
        public int DeleteInfo(FS.HISFC.Models.PhysicalExamination.Register register)
        {
            string strSQL = "";
            //ȡɾ��������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.DeleteInfo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.DeleteInfo�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, register.ID);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.DeleteInfo:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region  ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="register">���Ǽ�ʵ��</param>
        /// <returns></returns>
        protected int InsertInfo(FS.HISFC.Models.PhysicalExamination.Register register)
        {
            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.AddInfo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.AddInfo�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItem(register);     //ȡ�����б�
                //strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
                return this.ExecNoQuery(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.AddInfo:" + ex.Message;
                this.WriteErr();
                return -1;
            }

        }
        #endregion

        #region  �޸�һ������
        /// <summary>
        /// �޸�һ������
        /// </summary>
        /// <param name="register">���Ǽ�ʵ��</param>
        /// <returns></returns>
        protected int UpdateInfo(FS.HISFC.Models.PhysicalExamination.Register register)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.UpdateInfo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.UpdateInfo�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItem(register);     //ȡ�����б�
                //strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
                return this.ExecNoQuery(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.UpdateInfo:" + ex.Message;
                this.WriteErr();
                return -1;
            }

        }
        #endregion

        #region ���update����insert��λ��Ŀ��Ϣ��Ĵ����������
        /// <summary>
        /// ���update����insert��λ��Ŀ��Ϣ��Ĵ����������
        /// </summary>
        /// <param name="Item">��λ��Ŀ��Ϣʵ��</param>
        /// <returns>�ַ�������</returns>
        private string[] myGetParmItem(FS.HISFC.Models.PhysicalExamination.Register ChkRegister)
        {
            string[] strParm ={	
								 ChkRegister.PID.CardNO, //���￨��
								 ChkRegister.Name,//����
								 ChkRegister.SpellCode,//ƴ����
								 ChkRegister.WBCode,//���
								 ChkRegister.IDCard,//���֤
								 ChkRegister.Profession.ID,//ְҵ   
								 ChkRegister.Company.Name,//��쵥λ
								 ChkRegister.PhoneBusiness,//��λ�绰
								 ChkRegister.BusinessZip,//��λ�ʱ�
								 ChkRegister.AddressHome,//���ڻ��ͥ����
								 ChkRegister.PhoneHome ,//��ͥ�绰
								 ChkRegister.HomeZip,//���ڻ��ͥ��������
								 ChkRegister.Nationality.ID,//����
								 ChkRegister.Kin.Name,//��ϵ������
								 ChkRegister.Kin.RelationPhone,//��ϵ�˵绰
								 ChkRegister.Kin.RelationAddress,//��ϵ��סַ
								 ChkRegister.Kin.RelationLink,//��ϵ�˹�ϵ
								 ChkRegister.MaritalStatus.ID.ToString(),//����״��
								 ChkRegister.Country.ID, //���� 
								 ChkRegister.Pact.PayKind.ID,//�������
								 ChkRegister.Pact.PayKind.Name,//�����������
								 ChkRegister.SSN, //ҽ��֤��
								 ChkRegister.DIST,//������
								 ChkRegister.AnaphyFlag,//ҩ�����
								 ChkRegister.Disease.ID,//��Ҫ����
								 ChkRegister.ArchivesNO,//����������
								 ChkRegister.Company.ID,//��쵥λ
								 ChkRegister.IdentityLevel,//������
								 this.Operator.ID, //����Ա
								 ChkRegister.Sex.ID.ToString(), //�Ա�
								 ChkRegister.Birthday.ToString()//����
							 };
            return strParm;
        }

        #endregion

        #region    ��ȡ��SQL
        /// <summary>
        /// ��ȡ��SQL
        /// </summary>
        /// <returns></returns>
        private string GetExamPatientSql()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetCHKPatientSql", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return null;
            }

            //ȡ��λ��Ϣ����
            return strSQL;
        }

        #endregion
        #endregion

        #region ��Ա��Ϣ�Ǽ�
        #region ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet
        /// <summary>
        /// ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetRegisterPatient(string BeginTime, string EndTime, ref System.Data.DataSet ds)
        {
            string strSql = GetRegisterSqlSeparater();
            if (strSql == null)
            {
                return -1;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegisterPatient.All", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return -1;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, BeginTime, EndTime);
            //ȡ��λ��Ϣ����
            return this.ExecQuery(strSQL, ref ds);
        }
        #endregion

        #region ��������������ѯ
        /// <summary>
        /// ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetRegisterPatient(string strWhere, ref System.Data.DataSet ds)
        {
            string strSql = GetRegisterSqlSeparater();
            if (strSql == null)
            {
                return -1;
            }
            strSql += strWhere;
            //ȡ��λ��Ϣ����
            return this.ExecQuery(strSql, ref ds);
        }
        #endregion

        #region �������ѯ�����Ա��Ϣ
        /// <summary>
        /// �������ѯ�����Ա��Ϣ
        /// </summary>
        /// <param name="ID">���� �򽡿�������,��쵥λ,���� </param>
        /// <param name="ds"></param>
        /// <param name="type">����</param>
        /// <returns></returns>
        public int GetRegisterPatient(string dtBegin, string dtEnd, string ID, ref System.Data.DataSet ds, ExamType type)
        {
            string strSql = GetRegisterSqlSeparater();
            if (strSql == null)
            {
                return -1;
            }
            string strSQL = "";
            if (type == ExamType.CARDNO)
            {
                //ȡSELECT���
                if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegisterPatient.ID.1", ref strSQL) == -1)
                {
                    this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegisterPatient.ID.1�ֶ�!";
                    return -1;
                }
            }
            else if (type == ExamType.CHKID)
            {
                //ȡSELECT���
                if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegisterPatient.ID.2", ref strSQL) == -1)
                {
                    this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegisterPatient.ID.2�ֶ�!";
                    return -1;
                }
            }
            else if (type == ExamType.COMPANY)
            {
                //ȡSELECT���
                if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegisterPatient.CompanyCode", ref strSQL) == -1)
                {
                    this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegisterPatient.CompanyCode�ֶ�!";
                    return -1;
                }
            }
            else if (type == ExamType.NAME)
            {
                //ȡSELECT���
                if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegisterPatient.Name", ref strSQL) == -1)
                {
                    this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegisterPatient.Name�ֶ�!";
                    return -1;
                }
            }
            else if (type == ExamType.CLINICNO)
            {
                //ȡSELECT���
                if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegisterPatient.ClinicNO", ref strSQL) == -1)
                {
                    this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegisterPatient.ClinicNO�ֶ�!";
                    return -1;
                }
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, ID, dtBegin, dtEnd);
            //ȡ��λ��Ϣ����
            return this.ExecQuery(strSQL, ref ds);
        }
        #endregion

        #region �������Ż�ȡ���Ǽ���Ϣ
        /// <summary>
        /// �������Ż�ȡ���Ǽ���Ϣ
        /// </summary>
        /// <param name="ClinicNO">����</param>
        /// <returns></returns>
        public FS.HISFC.Models.PhysicalExamination.Register GetRegisterByClinicNO(string ClinicNO)
        {
            ArrayList list = null;
            FS.HISFC.Models.PhysicalExamination.Register obj = new FS.HISFC.Models.PhysicalExamination.Register();
            string strSql = GetRegisterSql();
            if (strSql == null)
            {
                return null;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegister.ClinicNO", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return null;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, ClinicNO);
            //ȡ��λ��Ϣ����
            list = this.GetmyRegister(strSQL);
            if (list == null)
            {
                return null;
            }
            if (list.Count > 0)
            {
                obj = (FS.HISFC.Models.PhysicalExamination.Register)list[0];
            }
            return obj;

        }
        #endregion

        #region �������Ż�ȡ���Ǽ���Ϣ
        /// <summary>
        /// ���ݼ������� �� �ڲ����
        /// </summary>
        /// <param name="ClinicNO">����</param>
        /// <returns></returns>
        public ArrayList QueryRegisterByCollectivity(string Collectivity, string SortNO)
        {
            ArrayList list = null;
            string strSql = GetRegisterSql();
            if (strSql == null)
            {
                return null;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegister.QueryRegisterByCollectivity", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return null;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, Collectivity, SortNO);
            //ȡ��λ��Ϣ����
            list = this.GetmyRegister(strSQL);
            return list;

        }
        #endregion

        #region ���ݿ��Ų�ѯ�Ǽ���Ϣ
        /// <summary>
        /// ���ݿ��Ų�ѯ�Ǽ���Ϣ
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByCardNO(string CardNo)
        {
            ArrayList list = null;
            FS.HISFC.Models.PhysicalExamination.Register obj = new FS.HISFC.Models.PhysicalExamination.Register();
            string strSql = GetRegisterSql();
            if (strSql == null)
            {
                return null;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegister.CardNo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegister.CardNo�ֶ�!";
                return null;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, CardNo);
            //ȡ��λ��Ϣ����
            list = this.GetmyRegister(strSQL);
            return list;
        }

        /// <summary>
        /// ���ݿ��Ż�ȡ��������µļ�������¼
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private ArrayList myQueryInfo(string CardNo)
        {
            ArrayList list = new ArrayList();
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegister.myQueryInfo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegister.myQueryInfo�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, CardNo);
            this.ExecQuery(strSQL);
            FS.HISFC.Models.PhysicalExamination.Register obj = null;
            while (this.Reader.Read())
            {
                obj = new FS.HISFC.Models.PhysicalExamination.Register();
                obj.Company.ID = this.Reader[0].ToString(); //���￨��
                obj.Company.Name = this.Reader[1].ToString(); //��쵥λ
                obj.CollectivityCode = this.Reader[2].ToString(); //��������
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #region �ṩ�������õĺ���
        /// <summary>
        /// ���ݿ��Ż���û��ܺŻ�������,��ȡ�������ͼ������Ǽ���Ա��Ϣ ������
        /// </summary>
        /// <param name="CollectivityCode">��������</param>
        /// <param name="CardNo">����</param>
        /// <returns></returns>
        public ArrayList QueryCollectivityRegisterByCardNO(string CardNo)
        {
            //Exami.GetChkRegister.QueryCollectivityRegisterByCardNO

            //��ȡĳ�������µ�������Ա��Ϣ
            ArrayList AList = QueryRegisterByCardNO(CardNo);

            if (AList == null)
            {
                return null;
            }
            #region  ���ݼ������Ż�ȡ��Ϣ
            ArrayList BList = QueryRegisterByCollectivityCode(CardNo);
            if (BList == null)
            {
                return null;
            }
            if (BList.Count > 0)
            {
                FS.HISFC.Models.PhysicalExamination.Register obj = (FS.HISFC.Models.PhysicalExamination.Register)BList[0];
                obj.ChkClinicNo = obj.CollectivityCode;
                obj.PID.CardNO = obj.CollectivityCode.Substring(5);
                obj.Name = obj.Company.Name;
                AList.Add(obj);
            }
            #endregion

            #region ���ݷ��û��ܺŻ�ȡ��Ϣ
            ArrayList CList = QueryRegisterByRecipeSequence(CardNo);
            if (CList == null)
            {
                return null;
            }
            if (CList.Count > 0)
            {
                FS.HISFC.Models.PhysicalExamination.Register obj = (FS.HISFC.Models.PhysicalExamination.Register)CList[0];
                obj.ChkClinicNo = obj.RecipeSequence;
                obj.PID.CardNO = obj.RecipeSequence.Substring(5);
                obj.Name = obj.Company.Name;
                AList.Add(obj);
            }
            #endregion 
            return AList;
        }
        #endregion

        #region ��ѯ���ݻ��ܺŲ�ѯ
        /// <summary>
        /// ��ѯ���ݻ��ܺŲ�ѯ
        /// </summary>
        /// <param name="RecipeSequence"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByRecipeSequence(string RecipeSequence)
        {
            ArrayList list = null;
            FS.HISFC.Models.PhysicalExamination.Register obj = new FS.HISFC.Models.PhysicalExamination.Register();
            string strSql = GetRegisterSql();
            if (strSql == null)
            {
                return null;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.QueryRegisterByRecipeSequence", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.QueryRegisterByRecipeSequence�ֶ�!";
                return null;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, RecipeSequence);
            //ȡ��λ��Ϣ����
            list = this.GetmyRegister(strSQL);
            return list;
        }
        #endregion

        #region ���ݽ��������Ų�ѯ�Ǽ���Ϣ
        /// <summary>
        /// ���ݽ��������Ų�ѯ�Ǽ���Ϣ
        /// </summary>
        /// <param name="ChkNO"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByArchivesNO(string ArchivesNO)
        {
            ArrayList list = null;
            FS.HISFC.Models.PhysicalExamination.Register obj = new FS.HISFC.Models.PhysicalExamination.Register();
            string strSql = GetRegisterSql();
            if (strSql == null)
            {
                return null;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegister.ChkNO", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegister.ChkNO�ֶ�!";
                return null;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, ArchivesNO);
            //ȡ��λ��Ϣ����
            list = this.GetmyRegister(strSQL);
            return list;
        }
        #endregion

        #region ���ӻ����ĳ������
        /// <summary>
        /// ���ӻ����ĳ������
        /// </summary>
        /// <param name="Register"></param>
        /// <returns></returns>
        public int AddOrUpdateRegister(FS.HISFC.Models.PhysicalExamination.Register Register)
        {
            if (UpdateInfoRegister(Register) <= 0)
            {
                if (this.AddInfoRegister(Register) <= 0)
                {
                    return -1;
                }
            }
            return 1;
        }
        #endregion

        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        private int AddInfoRegister(FS.HISFC.Models.PhysicalExamination.Register Register)
        {
            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.AddInfoRegister", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.AddInfoRegister�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItemRegister(Register);     //ȡ�����б�
                //strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
                return this.ExecNoQuery(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.AddInfoRegister:" + ex.Message;
                this.WriteErr();
                return -1;
            }

        }
        #endregion

        #region �޸�һ������
        /// <summary>
        /// �޸�һ������
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        private int UpdateInfoRegister(FS.HISFC.Models.PhysicalExamination.Register Register)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.UpdateInfoRegister", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.UpdateInfoRegister�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItemRegister(Register);     //ȡ�����б�
                //strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
                return this.ExecNoQuery(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.UpdateInfoRegister:" + ex.Message;
                this.WriteErr();
                return -1;
            }

        }
        #endregion

        #region ɾ��һ������
        /// <summary>
        /// ���������ˮ�� ɾ��һ������
        /// </summary>
        /// <param name="ClinicNo"></param>
        /// <returns></returns>
        public int DeleteInfoRegister(string ClinicNo)
        {
            string strSQL = "";
            //ȡɾ��������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.DeleteInfoRegister", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.DeleteInfoRegister�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ClinicNo);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.DeleteInfoRegister:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�������Ŀ����
        /// </summary>
        /// <param name="strBegin"></param>
        /// <param name="strEnd"></param>
        /// <returns></returns>
        public int GetMaxSeeNo(string strBegin, string strEnd)
        {
            try
            {
                string strSQL = "";
                //ȡɾ��������SQL���
                if (this.Sql.GetSql("Exami.ChkPatient.GetMaxSeeNo", ref strSQL) == -1)
                {
                    this.Err = "û���ҵ�Exami.ChkPatient.GetMaxSeeNo�ֶ�!";
                    return -1;
                }

                strSQL = string.Format(strSQL, strBegin, strEnd);    //�滻SQL����еĲ�����
                return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSQL));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        #endregion

        #region ˽��
        #region ��ȡ�Ǽ���Ա��Ϣ
        /// <summary>
        /// ��ȡ�Ǽ���Ա��Ϣ
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        private ArrayList GetmyRegister(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.PhysicalExamination.Register ChkRegister = null; //��λ��Ŀ��Ϣʵ��
            //ִ�в�ѯ���
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "��ȡ�Ǽ���Ա��Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    ChkRegister = new FS.HISFC.Models.PhysicalExamination.Register();
                    ChkRegister.TransType = this.Reader[0].ToString(); //�������� 1 ������ -1 ������
                    ChkRegister.ChkClinicNo = this.Reader[1].ToString();//-������
                    ChkRegister.ID = this.Reader[1].ToString();//-������
                    ChkRegister.PID.ID = this.Reader[1].ToString();//-������
                    ChkRegister.SpecalChkType.ID = this.Reader[2].ToString();//����������� ���й�����ѧ�ȣ� 
                    ChkRegister.PID.CardNO = this.Reader[3].ToString();//���￨��
                    ChkRegister.ChkSortNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());//�������
                    ChkRegister.Name = this.Reader[5].ToString();//����
                    ChkRegister.Name = this.Reader[5].ToString();//����
                    ChkRegister.Sex.ID = this.Reader[6].ToString(); //�Ա�//�Ա�
                    ChkRegister.MaritalStatus.ID = this.Reader[7].ToString();//����״��
                    ChkRegister.Country.ID = this.Reader[8].ToString();//����
                    ChkRegister.Height = this.Reader[9].ToString();//���
                    ChkRegister.Weight = this.Reader[10].ToString();//����
                    ChkRegister.BloodPressTop = this.Reader[11].ToString();//Ѫѹ
                    ChkRegister.BloodPressDown = this.Reader[12].ToString();//Ѫѹ
                    ChkRegister.ChkKind = this.Reader[13].ToString();//2 ���� 1 ����
                    ChkRegister.Company.ID = this.Reader[14].ToString();//��쵥λ
                    ChkRegister.Company.Name = this.Reader[15].ToString();//��쵥λ����
                    ChkRegister.SSN = this.Reader[16].ToString();//ҽ��֤��
                    ChkRegister.CheckTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[17]);//�������
                    ChkRegister.HomeCase = this.Reader[18].ToString();//����ʷ
                    ChkRegister.CaseHospital = this.Reader[19].ToString(); //�ֲ�ʷ
                    //t.oper_code �����,   --����� 
                    //t.oper_date ���ʱ�� ,    --���ʱ�� 
                    ChkRegister.SpellCode = this.Reader[22].ToString();//ƴ����
                    ChkRegister.WBCode = this.Reader[23].ToString(); //���
                    ChkRegister.IDCard = this.Reader[24].ToString();//���֤
                    ChkRegister.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[25].ToString()); //�Ա�
                    ChkRegister.Profession.ID = this.Reader[26].ToString();//ְҵ   
                    //ChkRegister.Company.Name = this.Reader[27].ToString();//��쵥λ
                    ChkRegister.PhoneBusiness = this.Reader[28].ToString();//��λ�绰
                    ChkRegister.BusinessZip = this.Reader[29].ToString();//��λ�ʱ�
                    ChkRegister.AddressHome = this.Reader[30].ToString();//���ڻ��ͥ����
                    ChkRegister.PhoneHome = this.Reader[31].ToString();//��ͥ�绰
                    ChkRegister.HomeZip = this.Reader[32].ToString();//���ڻ��ͥ��������
                    ChkRegister.Nationality.ID = this.Reader[33].ToString();//����
                    ChkRegister.Kin.Name = this.Reader[34].ToString();//��ϵ������
                    ChkRegister.Kin.RelationPhone = this.Reader[35].ToString();//��ϵ�˵绰
                    ChkRegister.Kin.RelationAddress = this.Reader[36].ToString();//��ϵ��סַ
                    ChkRegister.Kin.RelationLink = this.Reader[37].ToString();//��ϵ�˹�ϵ
                    ChkRegister.Pact.PayKind.ID = this.Reader[38].ToString();//�������
                    ChkRegister.Pact.PayKind.Name = this.Reader[39].ToString();//����������� 
                    ChkRegister.SSN = this.Reader[40].ToString(); //ҽ��֤��
                    ChkRegister.DIST = this.Reader[41].ToString();//������
                    ChkRegister.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[42]);//ҽ�Ʒ���
                    ChkRegister.AnaphyFlag = this.Reader[43].ToString();//ҩ�����
                    ChkRegister.Disease.ID = this.Reader[44].ToString();//��Ҫ����
                    ChkRegister.IdentityLevel = this.Reader[45].ToString();//������
                    ChkRegister.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[46].ToString());//������
                    ChkRegister.DutyNuse.ID = this.Reader[47].ToString(); //���λ�ʿ
                    ChkRegister.ArchivesNO = this.Reader[48].ToString(); //����������
                    ChkRegister.Operator.Dept.ID = this.Reader[49].ToString(); //����Ա���� 
                    ChkRegister.ExtCha = this.Reader[50].ToString(); // ��չ�ֶ�1 
                    ChkRegister.ExtDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[51].ToString()); //��չ�ֶ� 2
                    ChkRegister.ExtNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[52].ToString()); //��չ�ֶ�3 
                    ChkRegister.Item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[53].ToString()); //��չ�ֶ�4 
                    ChkRegister.ExtDate1 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[54].ToString()); //��չ�ֶ� 5
                    ChkRegister.ExtNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[55].ToString()); //��չ�ֶ� 6
                    ChkRegister.CollectivityCode = this.Reader[56].ToString(); //����Ǽ����
                    ChkRegister.CollectivityTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[57].ToString()); //����Ǽ�����
                    ChkRegister.CompanyDeptName = this.Reader[58].ToString();//��쵥λ�ڲ���
                    ChkRegister.CompanyDeptSeq = this.Reader[59].ToString(); //��������
                    ChkRegister.RecipeSequence = this.Reader[60].ToString();//���һ�η�Ʊ��Ϻ�
                    //ȡ��ѯ����еļ�¼
                    al.Add(ChkRegister);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ȡ�Ǽ���Ա��Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            this.Reader.Close();
            return al;
        }
        #endregion
        #region ���update����insert�Ǽ���Ϣ��Ϣ��Ĵ����������
        /// <summary>
        /// ���update����insert�Ǽ���Ϣ��Ϣ��Ĵ����������
        /// </summary>
        /// <param name="Item">�Ǽ���Ϣ��Ϣʵ��</param>
        /// <returns>�ַ�������</returns>
        private string[] myGetParmItemRegister(FS.HISFC.Models.PhysicalExamination.Register ChkRegister)
        {
            string[] strParm ={	
								 ChkRegister.TransType, //�������� 1 ������ -1 ������
								 ChkRegister.ChkClinicNo,//-������
								 ChkRegister.SpecalChkType.ID,//����������� ���й�����ѧ�ȣ� 
								 ChkRegister.PID.CardNO,//���￨��
								 ChkRegister.ChkSortNO.ToString(),//�������
								 ChkRegister.Name,//����
								 ChkRegister.MaritalStatus.ID.ToString(),//����״��
								 ChkRegister.Country.ID,//����
								 ChkRegister.Height,//���
								 ChkRegister.Weight,//����
								 ChkRegister.BloodPressTop ,//Ѫѹ
								 ChkRegister.BloodPressDown,//Ѫѹ11
								 ChkRegister.ChkKind,//1 ���� 0 ����
								 ChkRegister.Company.ID,//��쵥λ
								 ChkRegister.Company.Name,//��쵥λ����
								 ChkRegister.SSN,//ҽ��֤��
								 ChkRegister.CheckTime.ToString(),//�������16
								 ChkRegister.HomeCase,//����ʷ
								 ChkRegister.CaseHospital, //�ֲ�ʷ
								 Operator.ID,//�����
								 ChkRegister.OwnCost.ToString(),  //���ѽ�
								 ChkRegister.DutyNuse.ID, //���λ�ʿ21
								 ChkRegister.Operator.Dept.ID , // ����Ա����
								 ChkRegister.ExtCha, //--  ��չ�ַ���                  
								 ChkRegister.ExtDate.ToString(),//--  ��չ�ַ���   
								 ChkRegister.ExtNum.ToString(),//--  ��չ�ַ���   
								 ChkRegister.Item.PackQty.ToString(),//--  ��չ�ַ���   
								 ChkRegister.ExtDate1.ToString(),//--  ��չ�ַ���   
								 ChkRegister.ExtNum1.ToString(),//--  ��չ�ַ��� 
								 ChkRegister.CollectivityCode, //����������
								 ChkRegister.CollectivityTime.ToString(), // �����������
								 ChkRegister.CompanyDeptName ,//����ڲ���
								 ChkRegister.CompanyDeptSeq //��������
							 };
            return strParm;
        }

        #endregion
        #region �򵥻�ȡ�����Ա��Ϣ
        /// <summary>
        /// �򵥻�ȡ�����Ա��Ϣ
        /// </summary>
        /// <returns></returns>
        private string GetRegisterSqlSeparater()
        {
            //Exami.ChkPatient.GetRegisterSqlSeparater
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetRegisterSqlSeparater", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetRegisterSqlSeparater�ֶ�!";
                return null;
            }

            //ȡ��λ��Ϣ����
            return strSQL;
        }
        #endregion
        #region ��ȡ��Ա�ǼǱ�SQL
        /// <summary>
        /// ��ȡ��Ա�ǼǱ�SQL
        /// </summary>
        /// <returns></returns>
        private string GetRegisterSql()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetRegisterSql", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetRegisterSql�ֶ�!";
                return null;
            }

            //ȡ��λ��Ϣ����
            return strSQL;
        }
        #endregion
        #endregion

        #region  ��ȡ��쵵����
        /// <summary>
        /// ��ȡ��쵵����
        /// </summary>
        /// <returns></returns>
        public string GetChkNo()
        {
            string str = GetSequence("Exami.ChkPatient.GetChkNo");
            str = str.PadLeft(10, '0');
            return str;
        }
        /// <summary>
        /// ��ȡ���������ˮ
        /// </summary>
        /// <returns></returns>
        public string GetChkCollectivityCode()
        {
            string str = GetSequence("Exami.ChkPatient.GetChkCollectivityCode");
            str = str.PadLeft(14, '0');
            str = "JT" + str.Substring(2);
            return str;
        }
        #endregion

        #region ��ȡ �������
        /// <summary>
        /// ��ȡ��������¼
        /// </summary>
        /// <param name="strCompCode"></param>
        /// <returns></returns>
        public ArrayList QueryCollectivity(string strCompCode)
        {
            ArrayList list = new ArrayList();
            FS.HISFC.Models.PhysicalExamination.Register obj = null;
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetCollectivity", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetCollectivity�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, strCompCode);
            this.ExecQuery(strSQL);
            //ȡ��λ��Ϣ����
            while (this.Reader.Read())
            {
                obj = new FS.HISFC.Models.PhysicalExamination.Register();
                obj.Company.Name = this.Reader[0].ToString();
                obj.CollectivityCode = this.Reader[1].ToString();
                //				obj.CollectivityDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString());
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #region ���ݼ������Ż�ȡ�������Ա��Ϣ
        /// <summary>
        /// ���ݼ������Ż�ȡ�������Ա��Ϣ
        /// </summary>
        /// <param name="CollectivityCode"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByCollectivityCode(string CollectivityCode)
        {
            ArrayList list = null;
            FS.HISFC.Models.PhysicalExamination.Register obj = new FS.HISFC.Models.PhysicalExamination.Register();
            string strSql = GetRegisterSql();
            if (strSql == null)
            {
                return null;
            }
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegister.CollectivityCode", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegister.CollectivityCode�ֶ�!";
                return null;
            }
            strSQL = strSql + strSQL;
            strSQL = string.Format(strSQL, CollectivityCode);
            //ȡ��λ��Ϣ����
            list = this.GetmyRegister(strSQL);
            return list;
        }
        #endregion

        #region ���ݼ������Ż�ȡ����쵥λ�����μ�������
        /// <summary>
        /// ���ݼ������Ż�ȡ����쵥λ�����μ�������
        /// </summary>
        /// <param name="CollectivityCode"></param>
        /// <returns></returns>
        public ArrayList QueryCompanyByCollectivityCode(string CollectivityCode)
        {
            ArrayList list = new ArrayList();
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegister.GetCompanyByCollectivityCode", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegister.GetCompanyByCollectivityCode�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, CollectivityCode);
            //ȡ��λ��Ϣ����
            this.ExecQuery(strSQL);

            FS.FrameWork.Models.NeuObject obj = null;
            while (this.Reader.Read())
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #region ������쵵���Ż�ȡ����
        public ArrayList QueryCardNoByChkID(string ChkID)
        {
            ArrayList list = new ArrayList();
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegister.QueryCardNoByChkID", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegister.QueryCardNoByChkID�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, ChkID);
            //ȡ��λ��Ϣ����
            this.ExecQuery(strSQL);

            FS.FrameWork.Models.NeuObject obj = null;
            while (this.Reader.Read())
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #region ��ѯһ��ʱ���ڵļ���Ǽ���Ϣ
        public ArrayList QueryCompanyRegister(string beginDate,string endDate)
        {
            ArrayList list = new ArrayList();
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkRegister.QueryCompanyRegister", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetChkRegister.QueryCompanyRegister�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, beginDate, endDate);
            //ȡ��λ��Ϣ����
            this.ExecQuery(strSQL);

            FS.FrameWork.Models.NeuObject obj = null;
            while (this.Reader.Read())
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj.Name = this.Reader[0].ToString();
                obj.ID = this.Reader[1].ToString();
                list.Add(obj);
            }
            return list;
        }
        #endregion 
        #region ��ȡ��������б�
        /// <summary>
        /// ��ȡ��������б� -- ���ڷ��û���
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryCollectivityRegister(string CollectivityCode)
        {
            ArrayList list = new ArrayList();
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.QueryCollectivityRegisterID", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.QueryCollectivityRegister�ֶ�!";
                return null;
            }

            //ȡ��λ��Ϣ����
            this.ExecQuery(strSQL, CollectivityCode);

            FS.FrameWork.Models.NeuObject obj = null;
            while (this.Reader.Read())
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();//��ˮ�� 
                obj.User01 = this.Reader[1].ToString();//�շ���Ϻ�
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #endregion

        #region ������Ϣ
        #region  ���������ϸ��ȷ���ˣ�ȷ���¼�
        /// <summary>
        /// ���������ϸ��ȷ���ˣ�ȷ���¼�
        /// </summary>
        /// <param name="obj">Ҫȷ�ϵ�ʵ��</param>
        /// <returns></returns>
        public int UpdateConfirmInfo(string MoOrder, string ConfirmFlag, int NoBackQty)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.UpdateConfirmInfo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.UpdateConfirmInfo�ֶ�!";
                return -1;
            }
            strSQL = string.Format(strSQL, MoOrder, ConfirmFlag, this.Operator.ID, NoBackQty);
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ����ȷ������
        /// </summary>
        /// <param name="moOrder"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public int UpdateConfirmAmount(string moOrder, decimal confirmNum)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.UpdateConfirmAmount", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.UpdateConfirmInfo�ֶ�!";
                return -1;
            }
            strSQL = string.Format(strSQL, moOrder, confirmNum.ToString(), this.Operator.ID);
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region  ��ȡ�����ϸ
        /// <summary>
        /// ��ȡ�����ϸ
        /// </summary>
        /// <param name="ClinicNo"></param>
        /// <returns></returns>
        public ArrayList QueryItemListByClinicNO(string ClinicNo)
        {
            string strSQL = "";
            string strSql2 = GetChkItemSql();
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkItemList.ClinicNo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, ClinicNo);
            strSQL = strSql2 + strSQL;
            return GetItemList(strSQL); //�����Ŀ��Ϣʵ��
        }
        /// <summary>
        /// ��ȡ�����ϸ
        /// </summary>
        /// <param name="ClinicNo"></param>
        /// <param name="feeFlag"> 0 δ�շѣ�1�����շѣ�2����</param>
        /// <returns></returns>
        public ArrayList QueryItemListByClinicNO(string ClinicNo, string feeFlag)
        {
            string strSQL = "";
            string strSql2 = GetChkItemSql();
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkItemList.ClinicNoFeeFlag", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, ClinicNo, feeFlag);
            strSQL = strSql2 + strSQL;
            return GetItemList(strSQL); //�����Ŀ��Ϣʵ��
        }
        /// <summary>
        /// ������ˮ�Ż�ȡ�����Ŀ��ϸ
        /// </summary>
        /// <param name="SequenceNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.PhysicalExamination.ItemList GetItemListBySequence(string SequenceNo)
        {
            string strSQL = "";
            string strSql2 = GetChkItemSql();
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkItemBySequence", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.GetChkItemBySequence�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, SequenceNo);
            strSQL = strSql2 + strSQL;
            ArrayList list = GetItemList(strSQL); //�����Ŀ��Ϣʵ��
            if (list == null)
            {
                return null;
            }
            if (list.Count == 0)
            {
                return new FS.HISFC.Models.PhysicalExamination.ItemList();
            }
            return (FS.HISFC.Models.PhysicalExamination.ItemList)list[0];
        }
        #endregion

        #region ���ܷ���
        public ArrayList QueryGatherItemList(FS.FrameWork.Models.NeuObject obj)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.QueryGatherItemList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.QueryGatherItemList�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, obj.ID);
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��������Ŀ��Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            ArrayList list = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.PhysicalExamination.ItemList item = new FS.HISFC.Models.PhysicalExamination.ItemList();
                item.Item.ID = this.Reader[0].ToString();//��Ŀ����
                item.Item.Name = this.Reader[1].ToString();//��Ŀ����
                item.ExecDept.ID = this.Reader[2].ToString();//ִ�п���
                item.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
                item.RealCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
                item.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                item.NoBackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());
                item.UnitFlag = this.Reader[7].ToString();
                item.Item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8].ToString());
                list.Add(item);
            }
            return list;
        }
        #endregion

        #region ���¿�������
        public int UpdateNobackNum(string seqenceNO, int Qty, int BackQty)
        {

            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.UpdateNobackNum", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.UpdateNobackNum�ֶ�!";
                return -1;
            }
            try
            {
                //				string[] strParm = GetParame( item );     //ȡ�����б�
                strSQL = string.Format(strSQL, seqenceNO, Qty, BackQty, this.Operator.ID);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.InsertChkItemList:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int InsertChkItemList(FS.HISFC.Models.PhysicalExamination.ItemList item)
        {

            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.InsertChkItemList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.InsertChkItemList�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = GetParame(item);     //ȡ�����б�
                //strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
                return this.ExecNoQuery(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.InsertChkItemList:" + ex.Message;
                this.WriteErr();
                return -1;
            }

        }

        #region �����շѱ�־
        /// <summary>
        /// �����շѱ�־
        /// </summary>
        /// <param name="feeFlag">  0 δ�շѣ�1�����շѣ�2���� </param>
        /// <param name="MoOrder">ҽ����ˮ��</param>
        /// <returns></returns>
        public int UpdateItemListFeeFlagByMoOrder(string feeFlag, string MoOrder)
        {
            if (feeFlag == null || feeFlag == "")
            {
                this.Err = "��������շѱ�־ʧ��,�շѱ�־û�и�ֵ";
                return -1;
            }
            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.UpdateItemListFeeFlag", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.UpdateItemListFeeFlag�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, MoOrder, feeFlag, this.Operator.ID);
                return this.ExecNoQuery(strSQL);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.UpdateItemListFeeFlag:" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }
        /// <summary>
        /// �����շѱ�־
        /// </summary>
        /// <param name="feeFlag">  0 δ�շѣ�1�����շѣ�2���� </param>
        /// <param name="RecipeSeq">�շ���Ϻ�</param>
        /// <returns></returns>
        public int UpdateItemListFeeFlagByRecipeSeq(string feeFlag, string RecipeSeq)
        {
            if (feeFlag == null || feeFlag == "")
            {
                this.Err = "��������շѱ�־ʧ��,�շѱ�־û�и�ֵ";
                return -1;
            }
            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.UpdateItemListFeeFlagByRecipeSeq", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.UpdateItemListFeeFlagByRecipeSeq�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, RecipeSeq, feeFlag, this.Operator.ID);
                return this.ExecNoQuery(strSQL);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.UpdateItemListFeeFlag:" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }
        #endregion

        #region ɾ�����������ϸ
        /// <summary>
        /// ɾ����������¼
        /// </summary>
        /// <param name="ClinicNO"></param>
        /// <returns></returns>
        public int DeleteItemListByClinicNO(string ClinicNO)
        {
            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.DeleteCHkItemList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.DeleteCHkItemList�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ClinicNO);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.DeleteCHkItemList:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region ɾ��ĳһ�������ϸ
        /// <summary>
        /// ĳһ�������ϸ
        /// </summary>
        /// <param name="SeqenceNo"></param>
        /// <returns></returns>
        public int DeleteItemListBySeqenceNO(string SeqenceNo)
        {
            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.DeleteOneCHkItemList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.DeleteOneCHkItemList�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, SeqenceNo);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Exami.ChkPatient.DeleteOneCHkItemList:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region ���»��ܺ���շ�����
        /// <summary>
        /// ���������ϸ�ķ�Ʊ�������
        /// </summary>
        /// <param name="item"></param>
        /// <param name="recipeSequence"></param>
        /// <returns></returns>
        public int UpdateItemListRecipeSequence(FS.HISFC.Models.PhysicalExamination.ItemList item, string recipeSequence)
        {

            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.UpdateRecipeSequence", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.UpdateRecipeSequence�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, item.ClinicNO, recipeSequence); //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ���� Exami.ChkPatient.UpdateRecipeSequence:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// �������ǼǱ����һ�λ��ܷ�Ʊ����
        /// </summary>
        /// <param name="item"></param>
        /// <param name="recipeSequence"></param>
        /// <returns></returns>
        public int UpdateRegisterRecipeSequence(FS.HISFC.Models.PhysicalExamination.ItemList item, string recipeSequence)
        {

            string strSQL = "";
            //ȡ���������SQL���
            if (this.Sql.GetSql("Exami.ChkPatient.UpdateRegisterRecipeSequence", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.UpdateRegisterRecipeSequence�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, item.ClinicNO, recipeSequence); //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ���� Exami.ChkPatient.UpdateRegisterRecipeSequence:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region ��ѯ���ܷ�����Ϣ
        public int QueryGatherFeeByRecipeSeq(ref System.Data.DataSet ds, string RecipeSeq)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.QueryGatherFeeByRecipeSeq", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient.QueryGatherFeeByRecipeSeq�ֶ�!";
                return -1;
            }
            strSQL = string.Format(strSQL, RecipeSeq);
            //ȡ��λ��Ϣ����
            return this.ExecQuery(strSQL, ref ds);
        }
        #endregion

        #region ˽�г�Ա
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="itemlist"></param>
        /// <returns></returns>
        private string[] GetParame(FS.HISFC.Models.PhysicalExamination.ItemList itemlist)
        {
            string strConformFlag = "";
            //if (itemlist.Item.IsNeedConfirm == FS.HISFC.Models.Fee.ConfirmState.Outpatient || itemlist.Item.IsNeedConfirm == FS.HISFC.Models.Fee.ConfirmState.All)
            //{
            //    strConformFlag = "1";
            //}
            //else
            //{
            //    strConformFlag = "0";
            //}
            if (itemlist.Item.IsNeedConfirm)
            {
                strConformFlag = "1";
            }
            else
            {
                strConformFlag = "0";
            }
            string[] str = new string[]
				{
					itemlist.SequenceNO,//���к�0
					itemlist.ClinicNO,//������1
					itemlist.CardNO,//���￨��2
					itemlist.Item.ID ,//��Ŀ����3
					itemlist.Item.Qty.ToString(),//����4
					itemlist.UnitFlag,//��λ��ʶ5
					itemlist.EcoRate.ToString(),//�Ż��˶��ٽ��6
					itemlist.ExecDept.ID,//ִ�п���7
					strConformFlag,//8
					itemlist.ConformOper.ID, //ȷ����9
					itemlist.ConformOper.OperTime.ToString() , //ȷ��ʱ��10
					itemlist.ExtFlag,//���������11
					itemlist.RealCost.ToString(),//--ʵ�ʼ۸�12
					itemlist.Item.PriceUnit ,//   --��λ
					itemlist.Combo ,//--��չ��־14
					itemlist.Item.Price.ToString(),//   --�۸�15
					itemlist.Item.PackQty.ToString(),//  --��װ����16
					this.Operator.ID,//����Ա����17
					System.DateTime.Now.ToString(), //ִ�п���18
					itemlist.NoBackQty.ToString(),
					itemlist.RecipeDoc.ID, //��������
					itemlist.ChkFlag, //������� 2  ������� 1
					itemlist.RecipeDept.ID, //����ҽ��
					itemlist.Item.Name,//��Ŀ����
					itemlist.Item.SysClass.ID.ToString(), //ϵͳ���
                    itemlist.AccountFlag //���˻���� 0 û�п��˻�  1 �ѿ������˻�
				};
            return str;
        }
        /// <summary>
        /// ��ȡ�����Ŀ��Ϣ
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        private ArrayList GetItemList(string strSQL)
        {
            //ִ�в�ѯ���
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��������Ŀ��Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            ArrayList al = new ArrayList();
            try
            {

                FS.HISFC.Models.PhysicalExamination.ItemList itemlist = null;
                while (this.Reader.Read())
                {
                    itemlist = new FS.HISFC.Models.PhysicalExamination.ItemList();
                    itemlist.SequenceNO = this.Reader[0].ToString();//���к�
                    itemlist.ClinicNO = this.Reader[1].ToString();//������
                    itemlist.CardNO = this.Reader[2].ToString();//���￨��
                    itemlist.Item.ID = this.Reader[3].ToString();//��Ŀ����
                    itemlist.Item.Qty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);//����
                    itemlist.UnitFlag = this.Reader[5].ToString();//��λ��ʶ
                    itemlist.EcoRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);//ʵ�ʽ��6
                    itemlist.ExecDept.ID = this.Reader[7].ToString();//ִ�п���
                    itemlist.ExecDept.Name = this.Reader[8].ToString();//ִ�п���
                    if (this.Reader[9].ToString() == "1")
                    {
                        itemlist.Item.IsNeedConfirm = true;
                    }
                    else
                    {
                        itemlist.Item.IsNeedConfirm = false;
                    }
                    itemlist.ConformOper.ID = this.Reader[10].ToString(); //ȷ����
                    itemlist.ConformOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11]); //ȷ��ʱ��
                    itemlist.ExtFlag = this.Reader[12].ToString();//��չ��־12
                    itemlist.RealCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);//--��չ��־13
                    itemlist.Item.PriceUnit = this.Reader[14].ToString();//   --��λ
                    itemlist.Combo = this.Reader[15].ToString();//--��Ϻ�
                    itemlist.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());//   --��չ��־16
                    itemlist.Item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());//  --��չ�ַ��ֶ�17
                    itemlist.OperInfo.ID = this.Reader[18].ToString();//����Ա����
                    itemlist.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[19].ToString());//��������
                    itemlist.FeeFlag = this.Reader[20].ToString();//�շѱ�־
                    itemlist.FeeOperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[21]);//�շ�ʱ��
                    itemlist.FeeOperInfo.ID = this.Reader[22].ToString();//�շ�Ա
                    itemlist.NoBackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23].ToString());  //��������
                    itemlist.RecipeDoc.ID = this.Reader[24].ToString(); //��������
                    itemlist.ChkFlag = this.Reader[25].ToString(); //������� 2  ������� 1
                    itemlist.RecipeDept.ID = this.Reader[26].ToString(); //����ҽ��
                    itemlist.Item.Name = this.Reader[27].ToString();//��Ŀ����
                    itemlist.Item.SysClass.ID = this.Reader[28].ToString();
                    itemlist.OperInfo.Name = this.Reader[29].ToString();//����Ա
                    itemlist.RecipeSequence = this.Reader[30].ToString();
                    itemlist.AccountFlag = this.Reader[31].ToString();//0 �������˻� 1 �������˻�
                    //ȡ��ѯ����еļ�¼
                    al.Add(itemlist);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��õ�λ��Ŀ��Ϣ��Ϣʱ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            this.Reader.Close();
            return al;
        }
        /// <summary>
        /// ��ȡ�����Ŀsql
        /// </summary>
        /// <returns></returns>
        private string GetChkItemSql()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.Sql.GetSql("Exami.ChkPatient.GetChkItemList.Sql", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Exami.ChkPatient�ֶ�!";
                return null;
            }
            return strSQL;
        }
        #endregion

        #endregion 
    }
}
