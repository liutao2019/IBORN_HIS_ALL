using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using FS.HISFC.Models.Base;
using System.Collections;
using FS.FrameWork.Function;
namespace FS.HISFC.Components.InpatientFee.Register.Classes
{
    public class Function :FS.FrameWork.Management.Database
    {

    
        #region ˽�к���

        /// <summary>
        /// ��ѯ������Ϣ��SQL(ZL)
        /// </summary>
        /// <returns>FIN_IPR_INMAININFO SQL ���</returns>
        private string PatientQuerySelectALL()
        {
            string sql = string.Empty;
            if (Sql.GetSql("RADT.Inpatient.OldPatientQuery.Select.All", ref sql) == -1)
            {
                this.Err = Sql.Err;
                return null;
            }
            return sql;
        }

        /// <summary>
        /// ����SQL����ѯ��ϵͳ���߻�����Ϣ
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList myPatientQuery(string SQLPatient)
        {
             
            ArrayList al = new ArrayList();
            FS.HISFC.Models.RADT.PatientInfo PatientInfo;
            ProgressBarText = "���ڲ�ѯ����...";
            ProgressBarValue = 0;
            //ȡϵͳʱ��,�����õ������ַ���
            DateTime sysDate = GetDateTimeFromSysDateTime();
            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }

            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new FS.HISFC.Models.RADT.PatientInfo();

     

                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); // סԺ��
                        if (!Reader.IsDBNull(0)) PatientInfo.PID.PatientNO = Reader[0].ToString(); // סԺ��
                        if (!Reader.IsDBNull(1)) PatientInfo.Card.ICCard.ID = Reader[1].ToString(); //IC����
                        if (!Reader.IsDBNull(2)) PatientInfo.Name = Reader[2].ToString(); //  ����						
                        if (!Reader.IsDBNull(3)) PatientInfo.SpellCode = Reader[3].ToString(); //ƴ����
                        if (!Reader.IsDBNull(4)) PatientInfo.WBCode = Reader[4].ToString(); // �����
                        if (!Reader.IsDBNull(5)) PatientInfo.Birthday = NConvert.ToDateTime(Reader[5]); // ����
                        if (!Reader.IsDBNull(6)) PatientInfo.Sex.ID = Reader[6].ToString(); //   �Ա�
                        if (!Reader.IsDBNull(7)) PatientInfo.IDCard = Reader[7].ToString(); //  ���֤��
                        if (!Reader.IsDBNull(8)) PatientInfo.BloodType.ID = Reader[8].ToString(); //  Ѫ��
                        if (!Reader.IsDBNull(9)) PatientInfo.Profession.ID = Reader[9].ToString(); //  ְҵ
                        if (!Reader.IsDBNull(10)) PatientInfo.CompanyName = Reader[10].ToString(); // ��λ����
                        if (!Reader.IsDBNull(11)) PatientInfo.PhoneBusiness = Reader[11].ToString(); //  ��λ�绰
                        if (!Reader.IsDBNull(12)) PatientInfo.User01 = Reader[12].ToString(); //��λ�ʱ�
                        if (!Reader.IsDBNull(13)) PatientInfo.AddressHome = Reader[13].ToString(); //  ��ͥ��ַ
                        if (!Reader.IsDBNull(14)) PatientInfo.PhoneHome = Reader[14].ToString(); //  ��ͥ�绰
                        if (!Reader.IsDBNull(15)) PatientInfo.User02 = Reader[15].ToString(); //  ��ͥ�ʱ�
                        if (!Reader.IsDBNull(16)) PatientInfo.DIST = Reader[16].ToString(); //  ����
                        if (!Reader.IsDBNull(17)) PatientInfo.Nationality.ID = Reader[17].ToString(); //  ����
                        if (!Reader.IsDBNull(18)) PatientInfo.Kin.Name = Reader[18].ToString(); //  ��ϵ������
                        if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationPhone = Reader[19].ToString(); // ��ϵ�˵绰
                        if (!Reader.IsDBNull(20)) PatientInfo.Kin.RelationAddress = Reader[20].ToString(); //  ��ϵ�˵�ַ
                        if (!Reader.IsDBNull(21)) PatientInfo.Kin.Relation.ID = Reader[21].ToString(); //  �뻼�߹�ϵ
                        if (!Reader.IsDBNull(22)) PatientInfo.MaritalStatus.ID = Reader[22].ToString(); // ����״��
                        if (!Reader.IsDBNull(23)) PatientInfo.Country.ID = Reader[23].ToString(); //  ����             
                        if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.ID = Reader[24].ToString(); //  ��ͬ���
                        if (!Reader.IsDBNull(25)) PatientInfo.Pact.PayKind.Name = Reader[25].ToString(); //  ��ͬ�������
                        if (!Reader.IsDBNull(26)) PatientInfo.Pact.ID = Reader[26].ToString(); //  ��ͬ��λ
                        if (!Reader.IsDBNull(27)) PatientInfo.Pact.Name = Reader[27].ToString(); //  ��ͬ��λ����

                        if (!Reader.IsDBNull(28)) PatientInfo.SSN = Reader[28].ToString(); //  ҽ��֤��
                        if (!Reader.IsDBNull(29)) PatientInfo.AreaCode = Reader[29].ToString(); //  ������
                        if (!Reader.IsDBNull(30)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[30]); //  ҽ�Ʒ���
                        if (!Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31]); //  ҩ�����
                       
                        if (!Reader.IsDBNull(41)) PatientInfo.InTimes = NConvert.ToInt32(Reader[41]); //��Ժ����
                        
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

        /// <summary>
        /// ��ѯ��ϵͳ����
        /// </summary>
        /// <param name="patientName">����</param>
        /// <param name="sexcode">�Ա�1 �У�2 Ů</param>
        /// <returns></returns>
        public ArrayList GetOldSysPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, string sqlWhere)
        {
             
            ArrayList al = new ArrayList();
            string sqlSelectALL_ZL = string.Empty;
            string sql2 = string.Empty;

            sqlSelectALL_ZL = this.PatientQuerySelectALL();

            if (sqlSelectALL_ZL == null)
            {
                return null;
            }
            sql2 += sqlSelectALL_ZL + " " + sqlWhere;

            ArrayList oldPatientList = this.myPatientQuery(sql2);
            if (oldPatientList == null)
            {
                oldPatientList = new ArrayList();
            }

            return oldPatientList;
        }

        /// <summary>
        /// �������֤�Ż�ȡ����
        /// </summary>
        /// <param name="idNO">���֤��</param>
        /// <returns></returns>
        public static string GetBirthDayFromIdNO(string idNO, ref string err)
        {

            if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref err) < 0)
            {
                return "-1";
            }
            if (idNO.Length == 15)
            {
                idNO = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            string datestr = idNO.Substring(6, 8);
            string year = datestr.Substring(0, 4);
            string month = datestr.Substring(4, 2);
            string day = datestr.Substring(6, 2);
            datestr = year + "-" + month + "-" + day;
            return datestr;
        }

        /// <summary>
        /// �������֤�Ż�ȡ�Ա�
        /// </summary>
        /// <param name="idNO">���֤��</param>
        /// <returns></returns>
        public static FS.FrameWork.Models.NeuObject GetSexFromIdNO(string idNO, ref string err)
        {
            if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref err) < 0)
            {
                return null;
            }

            if (idNO.Length == 15)
            {
                idNO = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }

            int flag = FS.FrameWork.Function.NConvert.ToInt32((idNO.Substring(16, 1)));
            FS.FrameWork.Models.NeuObject sexobj = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.Models.Base.SexEnumService sexlist = new FS.HISFC.Models.Base.SexEnumService();
            if (flag % 2 == 0)
            {
                sexobj.ID = FS.HISFC.Models.Base.EnumSex.F.ToString();
                sexobj.Name = sexlist.GetName(FS.HISFC.Models.Base.EnumSex.F);
            }
            else
            {
                sexobj.ID = FS.HISFC.Models.Base.EnumSex.M.ToString();
                sexobj.Name = sexlist.GetName(FS.HISFC.Models.Base.EnumSex.M);
            }
            return sexobj;
        }

        public string TransMariStr(int mari)
        {
            if (((int)EnumMaritalStatus.A) == mari)
                return "A";
            else if (((int)EnumMaritalStatus.D) == mari)
                return "D";
            else if (((int)EnumMaritalStatus.M) == mari)
                return "M";
            else if (((int)EnumMaritalStatus.R) == mari)
                return "R";
            else if (((int)EnumMaritalStatus.S) == mari)
                return "S";
            else if (((int)EnumMaritalStatus.W) == mari)
                return "W";
            else
                return "A";
        }
    }

    /// <summary>
    /// �嵥��ѯ
    /// </summary>
    public class QueryDayFee : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ��ѯ�����嵥
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="inPatientNo">סԺ��</param>
        /// <returns></returns>
        public int QueryDataSetBySql(string SQL, DateTime beginTime, DateTime endTime, string inPatientNo, string deptCode, string inState, ref DataSet ds)
        {
            if (ds == null || ds.Tables.Count > 0)
            {
                ds = new DataSet();
            }

            if (SQL.Trim() == "")
            {
                this.Err = "��ò�ѯSQLʧ�ܣ���ά��SQL��";
                return -1;
            }

            string querySql = string.Empty;

            if (this.Sql.GetSql(SQL, ref querySql) == -1)
            {
                this.Err = "��ȡSql�����������ţ�" + SQL;
                WriteErr();
                return -1;
            }

            try
            {
                querySql = string.Format(querySql, inPatientNo, beginTime.ToString(), endTime.ToString(), deptCode, inState);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
                return -1;
            }

            return this.ExecQuery(querySql, ref ds);
        }
    }
}
