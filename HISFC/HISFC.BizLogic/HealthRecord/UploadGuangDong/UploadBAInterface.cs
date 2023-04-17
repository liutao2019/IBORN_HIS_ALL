using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Xml;



namespace FS.HISFC.BizLogic.HealthRecord.UploadGuangDong
{
    /// <summary>
    /// ����ҽԺ������ҳ�ϴ�ҵ���,
    /// ��������SQLSERVER���ݿ�,�����ַ�������д����
    /// [2008/04/08]
    /// /// <�޸ļ�¼
    ///		�޸���='�²���ϵͳ�ӿ�'
    ///		�޸�ʱ��='2009-5-30'
    ///		�޸�Ŀ��='��ϵͳ�ӿ�'
    ///		�޸�����='����'
    ///  />
    /// </summary>
    /// </summary>
    public class UploadBAInterface : FS.FrameWork.Management.Database
    {
        
        //private SqlConnection conn = new SqlConnection(@"Data Source=190.170.0.223;Initial Catalog=bagl_java;User ID=sa;Password=1223;");
        //private SqlConnection conn = new SqlConnection(@"Data Source=10.11.0.15;Initial Catalog=bagl_java;User ID=ba_java;Password=1223;");
       //private SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=ba_java;Integrated Security=True;");
        //private SqlConnection conn = new SqlConnection(@"Data Source=10.10.10.119\;Initial Catalog=bagl9;User ID=sa;Password=1223");
        //private SqlConnection conn = new SqlConnection(@"Data Source=CABC11E69C2A45A;Initial Catalog=bagl;User ID=sa;Password=1223");
        private SqlConnection conn = new SqlConnection();
			
       

        private SqlCommand cmd = new SqlCommand();
        private SqlTransaction transaction = null;
        private SqlDataReader reader;

        private InterfaceFunction fun = new InterfaceFunction();

        private string profileName = System.Windows.Forms.Application.StartupPath + @".\Profile\CaseDataBase.xml";//�������ݿ���������;



        FS.HISFC.BizLogic.Manager.Constant constMana = new FS.HISFC.BizLogic.Manager.Constant();
        FS.HISFC.BizLogic.Manager.Department deptMana = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
        string err = "";
        //��λ�б� ADD BY ZHY
        FS.FrameWork.Public.ObjectHelper UnitListHelper = new FS.FrameWork.Public.ObjectHelper();

        public string Err1
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }


        /// <summary>
        /// ������ҳ�ϴ��ӿڣ��㶫����3.0ϵͳ��
        /// </summary>
        public UploadBAInterface()
        {
            //if (!this.IsOpen())
            //{
            //    throw new Exception("�������ݿ�ʧ��");
            //}
            this.conn.ConnectionString = this.GetConnectString();
            this.conn.Open();
            this.transaction = this.conn.BeginTransaction();
            this.cmd.Connection = this.conn;
            this.cmd.Transaction = transaction;

            CreatFile();
        }

        public void Commit()
        {
            this.transaction.Commit();
        }

        public void Rollback()
        {
            this.transaction.Rollback();
        }

        public SqlConnection Connection
        {
            get
            {
                this.IsOpen();
                return this.conn;
            }
        }

        public bool IsOpen()
        {
            try
            {
                if (this.conn != null && this.conn.State == ConnectionState.Closed)
                {
                    this.conn.Open();
                    this.transaction = this.conn.BeginTransaction();
                    this.cmd.Connection = this.conn;
                    this.cmd.Transaction = this.transaction;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// ������Ӵ�
        /// </summary>
        /// <returns></returns>
        public string GetConnectString()
        {
            string dbInstance = "";
            string DataBaseName = "";
            string userName = "";
            string password = "";
            string connString = "";

            if (!System.IO.File.Exists(profileName))
            {
                FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
                XmlDocument doc = new XmlDocument();
                XmlElement root;
                root = myXml.CreateRootElement(doc, "SqlServerConnectForHis5.0", "1.0");

                XmlElement dbName = myXml.AddXmlNode(doc, root, "����", "");

                myXml.AddNodeAttibute(dbName, "���ݿ�ʵ����", "");
                myXml.AddNodeAttibute(dbName, "���ݿ���", "");
                myXml.AddNodeAttibute(dbName, "�û���", "");
                myXml.AddNodeAttibute(dbName, "����", "");

                try
                {
                    StreamWriter sr = new StreamWriter(profileName, false, System.Text.Encoding.Default);
                    string cleandown = doc.OuterXml;
                    sr.Write(cleandown);
                    sr.Close();
                }
                catch (Exception ex)
                {
                    this.Err = "����ҽ�����ӷ������ó���!" + ex.Message;
                    this.ErrCode = "-1";
                    this.WriteErr();
                    return "";
                }

                return "";
            }
            else
            {
                XmlDocument doc = new XmlDocument();

                try
                {
                    StreamReader sr = new StreamReader(profileName, System.Text.Encoding.Default);
                    string cleandown = sr.ReadToEnd();
                    doc.LoadXml(cleandown);
                    sr.Close();
                }
                catch { return ""; }

                XmlNode node = doc.SelectSingleNode("//����");

                try
                {

                    dbInstance = node.Attributes["���ݿ�ʵ����"].Value.ToString();
                    DataBaseName = node.Attributes["���ݿ���"].Value.ToString();
                    userName = node.Attributes["�û���"].Value.ToString();
                    password = node.Attributes["����"].Value.ToString();
                }
                catch { return ""; }

                connString = "packet size=4096;user id=" + userName + ";data source=" + dbInstance + ";pers" +
                    "ist security info=True;initial catalog=" + DataBaseName + ";password=" + password;
            }

            return connString;
        }

        #region �ϴ�

        #region HIS_BA1

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public int DeleteHISBA1(string inpatientNO,int times)
        {
            string strSQL = @"delete from HIS_BA1 where fprn = '{0}' and ftimes={1}";

            strSQL = string.Format(strSQL, inpatientNO, times);

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// ɾ���м������ by סԺ��ˮ��
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns></returns>
        public int DeleteHISBA1ByFzyid(string inpatientNO)
        {
            string strSQL = @"delete from HIS_BA1 WHERE FZYID='{0}' ";

            strSQL = string.Format(strSQL, inpatientNO);

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// ����fzkdate  fzkrq Ϊ��
        /// </summary>
        /// <param name="fprn"></param>
        /// <returns></returns>
        public int UpdateHISBA1Fzkdate(string fprn)
        {
            string strSQL = @"update HIS_BA1 set  fzkdate=null  where FPRN='{0}' and fzkdate<'2000-1-1 00:00:00'";
            try
            {
                strSQL = string.Format(strSQL, fprn.Substring(this.PatientNoSubstr()));
            }
            catch
            {
            }
            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        #endregion

        /// <summary>
        /// ����ϴ�����
        /// </summary>
        /// <param name="alICD"></param>
        private void ConverICDType(System.Collections.ArrayList alICD)
        {

            for (int i = alICD.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.HealthRecord.Diagnose obj = alICD[i] as FS.HISFC.Models.HealthRecord.Diagnose;
                if (obj.DiagInfo.DiagType.ID == "10" || obj.DiagInfo.DiagType.ID == "11" || obj.DiagInfo.DiagType.ID == "6")
                {
                    alICD.Remove(obj);
                }
                else
                {
                    obj.DiagInfo.DiagType.ID = this.constMana.GetConstant("CaseDiagnose", obj.DiagInfo.DiagType.ID).Name;

                    if (obj.DiagInfo.DiagType.ID.Trim().Equals(string.Empty))
                    {
                        obj.DiagInfo.DiagType.ID = "2";
                    }
                }
            }
        }

        /// <summary>
        /// ȡ���տ���
        /// </summary>
        /// <param name="deptCode"></param>
        private string ConverDept(string deptCode)
        {
            string strReturn = this.constMana.GetConstant("DEPTUPLOAD", deptCode).Memo;

            if (strReturn == "")
            {
                return deptCode;
            }
            else
            {
                return strReturn;
            }
        }

       /// <summary>
       ///  HIS_BA1 --������Ϣ
       /// </summary>
       /// <param name="b">������Ϣ</param>
       /// <param name="alFee">������Ϣ</param>
       /// <param name="alChangeDepe">ת����Ϣ</param>
       /// <param name="alDose"> ���</param>
        ///<param name="isMetCasBase">�Ƿ񲡰���������</param> 
       /// <returns></returns>
        public int InsertPatientInfoBA1(FS.HISFC.Models.HealthRecord.Base b, System.Collections.ArrayList alFee,
            System.Collections.ArrayList alChangeDepe, System.Collections.ArrayList alDose,bool isMetCasBase)
        {
            //this.IsOpen();

            //string sql = @"insert into ba1 (prn,name,sex,birthday,birthpl, idcard, native, nation) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
            //this.cmd.CommandText = string.Format(sql,
            //                                     patientInfo.PatientInfo.PID.PatientNO.Substring(3),
            //                                     patientInfo.PatientInfo.Name,
            //                                     patientInfo.PatientInfo.Sex.Name.Equals("��") ? "1": "2",//����
            //                                     patientInfo.PatientInfo.Birthday.ToShortDateString().Replace('-', '/'),
            //                                     patientInfo.PatientInfo.AreaCode,
            //                                     patientInfo.PatientInfo.IDCard,
            //                                     patientInfo.PatientInfo.Country.Name,
            //                                     patientInfo.PatientInfo.Nationality.Name);

            //if (this.cmd.ExecuteNonQuery() <= 0)
            //{
            //    return -1;
            //}

            string strSQL = this.fun.GetInsertHISBA1SQL(b, alFee, alChangeDepe, alDose,isMetCasBase);

            if (strSQL == null || strSQL == "")
            {
                return -1;
            }
            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public int DeleteHISBA3(string inpatientNO, int times)
        {
            string strSQL = @"delete from HIS_BA3 where fprn = '{0}' and ftimes={1}";

            strSQL = string.Format(strSQL, inpatientNO, times);

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// HIS_BA3  --���������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertHISBA3(FS.HISFC.Models.HealthRecord.Base patientInfo, FS.HISFC.Models.HealthRecord.Diagnose obj)
        {
            if (obj.DiagInfo.DiagType.ID == "16")
            {
                obj.DiagInfo.DiagType.ID = "f";
            }
            string sql = @"insert into HIS_BA3 (fprn,ftimes,FZDLX, FICDVersion, FICDM, FJBNAME,FZLJGBH,FZLJG) values ('{0}',{1},'{2}',{3},'{4}','{5}','{6}','{7}')";
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                sql = string.Format(sql,
                                                 this.PatientNoChang(patientNO.Substring(this.fun.PatientNoSubstr())),
                                                 //patientInfo.PatientInfo.InTimes.ToString().TrimStart('0'),
                                                 patientInfo.PatientInfo.InTimes.ToString().PadLeft(2, '0'),
                                                 obj.DiagInfo.DiagType.ID,//����
                                                 "11",//ICD�汾��
                                                 obj.DiagInfo.ICD10.ID,
                                                 obj.DiagInfo.Name,
                                                 obj.DiagOutState,
                                                 "");
            }
            catch
            {
            }

            ReadSQL(sql);

            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// HIS_BA4  ����������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alOperations"></param>
        /// <returns></returns>
        public int InsertPatientOperationInfo(FS.HISFC.Models.HealthRecord.Base patientInfo, System.Collections.ArrayList alOperations)
        {
            this.IsOpen();

            this.cmd.CommandText = string.Empty;
            string strsql = @"insert into tOperation (FPRN,FTIMES,FNAME, FOPTIMES,FOPCODE,FOP,FOPDATE,FQIEKOUBH,FQIEKOU,FYUHEBH,FYUHE,FDOCBH,FDOCNAME,FMAZUIBH,FMAZUI,FIFFSOP,FOPDOCT1BH,FOPDOCT1,FOPDOCT2BH,FOPDOCT2,FMZDOCTBH,FMZDOCT) 
values ('{0}',{1},'{2}',{3},'{4}','{5}','{6}', '{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}')";
            string temp = string.Empty;

            int times = 0;

            for (int i = 0; i < alOperations.Count; i++)
            //foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in alOperations)
            {
                FS.HISFC.Models.HealthRecord.OperationDetail obj = alOperations[i] as FS.HISFC.Models.HealthRecord.OperationDetail;
                this.cmd.CommandText = string.Empty;
                temp = strsql;
                string StatFlag = "";
                if (obj.StatFlag == "1")
                {
                    StatFlag = "1";
                }
                else
                {
                    StatFlag = "0";
                }

                if (StatFlag == "0")
                {
                    times++;
                }
                if ( obj.OperationInfo.ID.Substring ( 0, 2 ) != "01" || obj.OperationInfo.ID.Substring ( 0, 2 ) != "02" || obj.OperationInfo.ID.Substring ( 0, 2 ) != "03" || obj.OperationInfo.ID.Substring ( 0, 2 ) != "04" )
                {
                    obj.OperationInfo.ID = obj.OperationInfo.ID.PadRight ( obj.OperationInfo.ID.Length + 1, ' ' );
                }
                //**************
                //��������������ֻ�С�32������ĸ�ʽ�����²���ϵͳ�в�֪��Ҫ�����ٿո���߲�����Fuck
                //**************

                if ( obj.FirDoctInfo.ID != "" )
                {
                    obj.FirDoctInfo.ID = "T" + obj.FirDoctInfo.ID.Substring ( obj.FirDoctInfo.ID.Length - 4 ) + "*1";
                }
                if ( obj.FourDoctInfo.ID != "" )
                {
                    obj.FourDoctInfo.ID = "T" + obj.FourDoctInfo.ID.Substring ( obj.FourDoctInfo.ID.Length - 4 ) + "*1";
                }
                if ( obj.SecDoctInfo.ID != "" )
                {
                    obj.SecDoctInfo.ID = "T" + obj.SecDoctInfo.ID.Substring ( obj.SecDoctInfo.ID.Length - 4 ) + "*1";
                }
                

                string str = string.Format(temp,
                                                     patientInfo.PatientInfo.PID.PatientNO.Substring(3),
                                                     patientInfo.PatientInfo.InTimes.ToString().PadLeft(2, '0'),
                                                     patientInfo.PatientInfo.Name,
                                                     times.ToString()/*��������*/,
                                                     obj.OperationInfo.ID,
                                                     obj.OperationInfo.Name,//����������
                                                     obj.OperationDate.ToShortDateString().Replace('-', '/'),
                                                     obj.NickKind/*�п�*/,
                                                     this.constMana.GetConstant ( "INCITYPE", obj.NickKind ).Name,//�п�����
                                                     obj.CicaKind/*����*/,
                                                     this.constMana.GetConstant ( "CICATYPE", obj.CicaKind ).Name,//��������
                                                     obj.FirDoctInfo.ID,
                                                     obj.FirDoctInfo.Name,
                                                     obj.MarcKind,//����ʽ
                                                     this.constMana.GetConstant ( "ANESTYPE", obj.MarcKind ).Name,//����ʽ����
                                                     StatFlag,/*�Ƿ񸽼�����*/
                                                     obj.FourDoctInfo.ID,
                                                     obj.FourDoctInfo.Name,
                                                     obj.SecDoctInfo.ID,
                                                     obj.SecDoctInfo.Name,
                                                     obj.NarcDoctInfo.ID,
                                                     obj.NarcDoctInfo.Name);

                this.ReadSQL(str);

                if (this.cmd.ExecuteNonQuery() <= 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// HIS_BA6 ���������Ż�����Ϣ��
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Tumour"></param>
        /// <returns></returns>
        public int InsertPatientTumourInfo(FS.HISFC.Models.HealthRecord.Base patientInfo,FS.HISFC.Models.HealthRecord.Tumour Tumour)
        {
            this.IsOpen();
           // ArrayList tempchemoty = this.constMana.GetList ( "CHEMOTHERAPYWAY" );
           // for ( int i = 0; i <= tempchemoty.Count; i++ )
           // {
           //     FS.FrameWork.Models.NeuObject obj = tempchemoty [i] as FS.FrameWork.Models.NeuObject;
            if ( Tumour.Rdeviceid.Contains ( "��" ) )
                {
                    Tumour.User03 = "1";
                }
                else if ( Tumour.Rdeviceid.Contains ( "ֱ��" ) )
                {
                    Tumour.User03 = "2";
                }
                else if ( Tumour.Rdeviceid.Contains ( "X��" ) )
                {
                    Tumour.User03 = "3";
                }
                else if ( Tumour.Rdeviceid.Contains ( "��װ" ) )
                {
                    Tumour.User03 = "4";
                }
          //  }

          //  ArrayList radivce = this.constMana.GetList ( "RADIATEDEVICE" );
          //  for ( int i = 0; i <= radivce.Count; i++ )
          //  {
          //      FS.FrameWork.Models.NeuObject obj = radivce [i] as FS.FrameWork.Models.NeuObject;
                if ( Tumour.Cmethod.Contains ( "ȫ��" ) )
                {
                    Tumour.User02 = "1";
                }
                else if ( Tumour.Cmethod.Contains ( "�뻯" ) )
                {
                    Tumour.User02 = "2";
                }
                else if ( Tumour.Cmethod.Contains ( "A���" ) )
                {
                    Tumour.User02 = "3";
                }
                else if ( Tumour.Cmethod.Contains ( "��ǻע" ) )
                {
                    Tumour.User02 = "4";
                }
                else if ( Tumour.Cmethod.Contains ( "��ע" ) )
                {
                    Tumour.User02 = "5";
                }
                else if ( Tumour.Cmethod.Contains ( "����" ) )
                {
                    Tumour.User02 = "6";
                }
                else if ( Tumour.Cmethod.Contains ( "��ǻע" ) )
                {
                    Tumour.User02 = "7";
                }
        //    }


                string sql = @"insert into tKnubCard (fprn,ftimes,fflfsbh,fflfs,fflcxbh,fflcx,fflzzbh,fflzz,fyjy,fycs,fyts,fyrq1,fyrq2,fqjy,fqcs,fqts,fqrq1,fqrq2,fzname,fzjy,fzcs,fzts,fzrq1,fzrq2,fhlfsbh,fhlfs,fhlffbh,fhlff) 
values ('{0}',{1},'{2}','{3}','{4}','{5}','{6}', '{7}',{8},{9},{10},'{11}','{12}',{13},{14}, {15},'{16}','{17}','{18}',{19},{20},{21},'{22}','{23}','{24}','{25}','{26}','{27}')";
            this.cmd.CommandText = string.Format(sql,
                                                 patientInfo.PatientInfo.PID.PatientNO.Substring(3),
                                                 patientInfo.PatientInfo.InTimes.ToString().PadLeft(2, '0'),
                                                 Tumour.Rmodeid.TrimStart('0'),//
                                                 this.constMana.GetConstant ( "RADIATETYPE", Tumour.Rmodeid ).Name,
                                                 Tumour.Rprocessid.TrimStart ( '0' ),//
                                                 this.constMana.GetConstant ( "RADIATEPERIOD", Tumour.Rprocessid ).Name,
                                                 Tumour.User03,
                                                 Tumour.Rdeviceid,//
                                                 Tumour.Gy1.ToString(),
                                                 Tumour.Time1.ToString(),
                                                 Tumour.Day1.ToString(),
                                                 Tumour.BeginDate1.ToShortDateString(),                                                 
                                                 Tumour.EndDate1.ToShortDateString(),
                                                 Tumour.Gy2.ToString(),
                                                 Tumour.Time2.ToString(),
                                                 Tumour.Day2.ToString(),
                                                 Tumour.BeginDate2.ToShortDateString(),
                                                 Tumour.EndDate2.ToShortDateString ( ),
                                                 "",//ת�������ƣ�HISϵͳ��û�ж�Ӧ�ֶ�
                                                 Tumour.Gy3.ToString(),
                                                 Tumour.Time3.ToString(),
                                                 Tumour.Day3.ToString(),
                                                 Tumour.BeginDate3.ToShortDateString ( ),
                                                 Tumour.EndDate3.ToShortDateString ( ),
                                                 Tumour.Cmodeid.TrimStart ( '0' ),//
                                                 this.constMana.GetConstant ( "CHEMOTHERAPY", Tumour.Cmodeid ).Name,
                                                 Tumour.User02,
                                                 Tumour.Cmethod
                                                 );
            this.cmd.CommandText = this.cmd.CommandText.Replace ( "'0001-1-1'", "null" );
            this.cmd.CommandText = this.cmd.CommandText.Replace ( "'0001-01-01'", "null" );


            if (this.cmd.ExecuteNonQuery() <= 0)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ɾ��HISBA2
        /// </summary>
        /// <param name="inpatientNO">������</param>
        /// <param name="times">סԺ����</param>
        /// <returns></returns>
        public int DeleteHISBA2(string inpatientNO, int times)
        {
            string strSQL = @"DELETE FROM HIS_BA2 WHERE FPRN='{0}' AND FTIMES='{1}'";

            strSQL = string.Format(strSQL, inpatientNO, times);

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// HIS_BA2  --���������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertHISBA2(FS.HISFC.Models.HealthRecord.Base patientInfo, FS.HISFC.Models.RADT.Location obj)
        {
            string sql = @"INSERT INTO HIS_BA2
(FPRN,FTIMES,FZKTYKH,FZKDEPT,FZKDATE,FZKTIME)
VALUES
('{0}','{1}','{2}','{3}','{4}','{5}') ";

            DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(obj.Dept.Memo);
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');

            try
            {
                sql = string.Format(sql,
                                                 this.PatientNoChang(patientNO.Substring(this.fun.PatientNoSubstr())),//������
                                                 patientInfo.PatientInfo.InTimes.ToString().TrimStart('0'),//����
                                                 fun.ConverDept(obj.Dept.ID),//ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                                                 obj.Dept.Name,//ת�ƿƱ�
                                                 fun.ChangeDateTime(obj.Dept.Memo),//ת������
                                                 dt.ToShortTimeString());
            }
            catch
            {
            }

            ReadSQL(sql);

            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public int DeleteHISBA5(string inpatientNO, int times)
        {
            string strSQL = @"delete from HIS_BA5 where fprn = '{0}' and ftimes={1}";

            strSQL = string.Format(strSQL, inpatientNO,times.ToString());

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// HisBa5  --��Ӥ��Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int insertHisBa5(FS.HISFC.Models.HealthRecord.Base patientInfo,FS.HISFC.Models.HealthRecord.Baby  obj)
        {


            string sql = @"insert into HIS_BA5 (FPRN,FTIMES ,FBABYNUM ,FNAME ,FBABYSEXBH ,FBABYSEX ,FTZ ,FRESULTBH ,FRESULT ,
                  FZGBH ,FZG ,FGRICD10 ,FGRNAME ,FBABYGR ,FBABYQJ ,FBABYSUC ,FHXBH ,FHX) 
                                         values ('{0}',{1},{2},'{3}','{4}','{5}',{6},'{7}','{8}',
                  '{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')";



            if (obj.SexCode.ToString() == "M" || obj.SexCode.ToString() == "1")
            {
                obj.SexCode = "1";
                obj.Infect.Memo = "��";
            }
            else if (obj.SexCode.ToString() == "F" || obj.SexCode.ToString() == "2")
            {
                obj.SexCode = "2";
                obj.Infect.Memo = "Ů";
            }
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                sql = string.Format(sql, this.PatientNoChang(patientNO.Substring(this.fun.PatientNoSubstr())),
                                                 patientInfo.PatientInfo.InTimes.ToString().PadLeft(2, '0'),
                                                 obj.HappenNum.ToString(),//Ӥ�����
                                                 patientInfo.PatientInfo.Name.ToString(),//��������
                                                 obj.SexCode.ToString(),//Ӥ���Ա���
                                                 obj.Infect.Memo,//Ӥ���Ա�
                                                 obj.Weight.ToString(),//Ӥ������
                                                 obj.BirthEnd.ToString(), //���������
                                                 this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.CHILDBEARINGRESULT, obj.BirthEnd).Name.ToString(),  //������
                                                 obj.BabyState,//ת����
                                                 this.constMana.GetConstant("babyZG", obj.BabyState).Name.ToString(),  //ת��
                                                 obj.Infect.ID,//��Ⱦ����ICD10��
                                                 obj.Infect.Name,//
                                                 obj.InfectNum,//Ӥ����Ⱦ����
                                                 obj.SalvNum,//Ӥ�����ȴ���,
                                                 obj.SuccNum,//Ӥ�����ȳɹ�����
                                                 obj.Breath,//�������
                                                 this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.BREATHSTATE, obj.BirthEnd).Name.ToString() //����
                                                 );
            }
            catch
            {
            }
            ReadSQL(sql);
            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }


            return 1;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public int DeleteHISBA4(string inpatientNO, int times)
        {
            string strSQL = @"delete from HIS_BA4 where fprn = '{0}' and ftimes={1}";

            strSQL = string.Format(strSQL, inpatientNO,times.ToString());

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        ///  HisBa4  --������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int insertHisBa4(FS.HISFC.Models.HealthRecord.Base patientInfo,FS.HISFC.Models.HealthRecord.OperationDetail  obj)
        {

            #region  sql
            string sql = @"INSERT INTO HIS_BA4
(
FPRN ,--������ 0
FTIMES ,--	����
FNAME	,--��������
FOPTIMES   ,--��������
FOPCODE	,--	������
FOP	,--	�������Ӧ����
FOPDATE	,--	��������
FQIEKOUBH	,--	�пڱ��
FQIEKOU	,--�п�
FYUHEBH	,--���ϱ��
FYUHE	,--	����--10
FDOCBH	,--	����ҽ�����
FDOCNAME	,--	����ҽ��
FMAZUIBH   ,--	����ʽ���
FMAZUI	,--����ʽ
FIFFSOP	,--	�Ƿ񸽼�����
FOPDOCT1BH	,--I�����
FOPDOCT1	,--I������
FOPDOCT2BH	,--	II�����
FOPDOCT2	,--II������
FMZDOCTBH	,--	����ҽ�����--20
FMZDOCT	--����ҽ��
)
VALUES
(
'{0}' ,
{1},
'{2}' ,
{3},
'{4}' ,
'{5}',
'{6}' ,
'{7}',
'{8}' ,
'{9}',
'{10}' ,
'{11}',
'{12}' ,
'{13}',
'{14}' ,
{15},
'{16}' ,
'{17}',
'{18}' ,
'{19}',
'{20}' ,
'{21}'
)";
            #endregion
            string MarcKind_Code = string.Empty;
            string MarcKind_Name = string.Empty;

            FS.FrameWork.Models.NeuObject info= this.constMana.GetConstant("CASEANESTYPE", obj.MarcKind);
            if (info != null && info.ID!="")
            {
                if(info.Memo!="" && info.Memo!="true")
                {
                    MarcKind_Code = info.Memo;
                    MarcKind_Name = info.Name;
                }
            }
            else
            {
                MarcKind_Code = obj.MarcKind;
                MarcKind_Name = info.Name;
            }
            //�����пڣ�0���Ϊ01��02,03
            //string NickKind_Code = string.Empty;
            //string NickKind_Name = string.Empty;
            //FS.FrameWork.Models.NeuObject info = this.constMana.GetConstant("INCITYPE", obj.NickKind);
            //if(info!=null&&info.ID!="")
            //{
            //    if(info.Memo!=""&&info.Memo!="true")
            //    {
            //        NickKind_Code=info.Memo;
            //        NickKind_Name = info.Name;
            //    }
            //    else
            //    {
            //        NickKind_Code=obj.NickKind;
            //        NickKind_Name=this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.INCITYPE, obj.NickKind).Name;
            //    }
                    

            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                sql = string.Format(sql, this.PatientNoChang(patientNO.Substring(this.fun.PatientNoSubstr())),
                                                 patientInfo.PatientInfo.InTimes.ToString().PadLeft(2, '0'),
                                                 patientInfo.PatientInfo.Name.ToString(),//��������
                                                 obj.HappenNO.ToString(),//��������
                                                 obj.OperationInfo.ID.ToString(),//������
                                                 this.fun.ChangeCharacter(obj.OperationInfo.Name.ToString()),//�������Ӧ����
                                                 fun.ChangeDateTime(obj.OperationDate.ToShortDateString()),//��������
                                                 obj.NickKind.ToString(), //�пڱ��
                                                 this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.INCITYPE, obj.NickKind).Name.ToString(),  //�п�
                                                 obj.CicaKind.ToString(),//���ϱ��
                                                 this.constMana.GetConstant("CICATYPE", obj.CicaKind).Name.ToString(),  //����
                                                 this.fun.ConverDoc(obj.FirDoctInfo.ID),//����ҽ�����
                                                 obj.FirDoctInfo.Name,//����ҽ��
                                                 MarcKind_Code, // obj.MarcKind.ToString(),//����ʽ���
                                                 MarcKind_Name, //this.constMana.GetConstant("CASEANESTYPE",obj.MarcKind).Name.ToString(),//����ʽ
                                                 "0",//�Ƿ񸽼�����
                                                 this.fun.ConverDoc(obj.SecDoctInfo.ID),//I�����
                                                 obj.SecDoctInfo.Name.ToString(), // I������
                                                 this.fun.ConverDoc(obj.ThrDoctInfo.ID),//II�����
                                                 obj.ThrDoctInfo.Name.ToString(),//II������
                                                 this.fun.ConverDoc(obj.NarcDoctInfo.ID),//����ҽ�����
                                                 obj.NarcDoctInfo.Name.ToString() //����ҽ��
                                                 );
            }
            catch
            {
            }
            sql = sql.Replace("'NULL'", "NULL");
            ReadSQL(sql);
            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ɾ��HISBA6
        /// </summary>
        /// <param name="inpatientNO">������</param>
        /// <param name="times">סԺ����</param>
        /// <returns></returns>
        public int DeleteHISBA6(string inpatientNO, int times)
        {
            string strSQL = @"DELETE FROM HIS_BA6 WHERE FPRN='{0}' AND FTIMES='{1}'";

            strSQL = string.Format(strSQL, inpatientNO, times);

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// HIS_BA6  --����������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertHISBA6(FS.HISFC.Models.HealthRecord.Base patientInfo, FS.HISFC.Models.HealthRecord.Tumour obj)
        {
            #region sql
            string sql = @"INSERT INTO HIS_BA6
(
FPRN,--0
FTIMES,
FFLFSBH,
FFLFS,
FFLCXBH,
FFLCX,--5
FFLZZBH,
FFLZZ,
FYJY,
FYCS,
FYTS,--10
FYRQ1,
FYRQ2,
FQJY,
FQCS,
FQTS,--15
FQRQ1,
FQRQ2,
FZNAME,
FZJY,
FZCS,--20
FZTS,
FZRQ1,
FZRQ2,
FHLFSBH,
FHLFS,--25
FHLFFBH,
FHLFF,
FQTYPE,
FQT,
FQN,--30
FQM,
FQALL,
FQALLBH--33
)
VALUES
(
'{0}',
'{1}',
'{2}',
'{3}',
'{4}',
'{5}',
'{6}',
'{7}',
'{8}',
'{9}',
'{10}',
'{11}',
'{12}',
'{13}',
'{14}',
'{15}',
'{16}',
'{17}',
'{18}',
'{19}',
'{20}',
'{21}',
'{22}',
'{23}',
'{24}',
'{25}',
'{26}',
'{27}',
'{28}',
'{29}',
'{30}',
'{31}',
'{32}',
'{33}'
)";
            #endregion
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                sql = string.Format(sql,
                                                 this.PatientNoChang(patientNO.Substring(this.fun.PatientNoSubstr())),//������
                                                 patientInfo.PatientInfo.InTimes.ToString().TrimStart('0'),//����
                                                 obj.Rmodeid,//���Ʒ�ʽ���
                                                 "",//���Ʒ�ʽ
                                                 obj.Rprocessid,//���Ƴ�����
                                                 "",//���Ƴ���
                                                 obj.Rdeviceid,//����װ�ñ��
                                                 "",//����װ��
                                                 obj.Gy1,//1.ԭ�������
                                                 obj.Time1,//ԭ�������
                                                 obj.Day1,//ԭ��������
                                                 fun.ChangeDateTime(obj.BeginDate1.ToString()),//ԭ���ʼ����
                                                 fun.ChangeDateTime(obj.EndDate1.ToString()),//ԭ�������ʱ��
                                                 obj.Gy2,//2.�����ܰͽ����
                                                 obj.Time2,//�����ܰͽ����
                                                 obj.Day2,//�����ܰͽ�����
                                                 fun.ChangeDateTime(obj.BeginDate2.ToString()),//�����ܰͽῪʼʱ��
                                                 fun.ChangeDateTime(obj.EndDate2.ToString()),//�����ܰͽ����ʱ��
                                                 obj.Position,//3.ת��������
                                                 obj.Gy3,//3.ת�������
                                                 obj.Time3,//ת�������
                                                 obj.Day3,//ת��������
                                                 fun.ChangeDateTime(obj.BeginDate3.ToString()),//ת���ʼʱ��
                                                 fun.ChangeDateTime(obj.EndDate3.ToString()),//ת�������ʱ��
                                                 obj.Cmodeid,//���Ʒ�ʽ���
                                                 "",//���Ʒ�ʽ
                                                 obj.Cmethod,//���Ʒ������
                                                 "",//���Ʒ���
                                                 obj.Tumour_Type,//������������
                                                 obj.Tumour_T,//ԭ������T
                                                 obj.Tumour_N,//�ܰ�ת��N
                                                 obj.Tumour_M,//Զ��ת��M
                                                 obj.Tumour_Stage,//����
                                                 obj.Tumour_Stage//���ڱ���
                                                 );
            }
            catch
            {
            }

            sql = sql.Replace("'NULL'", "NULL");

            ReadSQL(sql);

            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ɾ��HISBA7
        /// </summary>
        /// <param name="inpatientNO">������</param>
        /// <param name="times">סԺ����</param>
        /// <returns></returns>
        public int DeleteHISBA7(string inpatientNO, int times)
        {
            string strSQL = @"DELETE FROM HIS_BA7 WHERE FPRN='{0}' AND FTIMES='{1}'";

            strSQL = string.Format(strSQL, inpatientNO, times);

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// HIS_BA7  --����������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertHISBA7(FS.HISFC.Models.HealthRecord.Base patientInfo, FS.HISFC.Models.HealthRecord.TumourDetail obj)
        {
            #region sql
            string sql = @"INSERT INTO HIS_BA7
(
FPRN,
FTIMES,
FHLRQ1,
FHLRQ2,
FHLDRUG,
FHLPROC,
FHLLXBH,
FHLLX
)
VALUES
(
'{0}',
'{1}',
'{2}',
'{3}',
'{4}',
'{5}',
'{6}',
'{7}'
)";
            #endregion
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                sql = string.Format(sql,
                                                 this.PatientNoChang(patientNO.Substring(this.fun.PatientNoSubstr())),//������
                                                 patientInfo.PatientInfo.InTimes.ToString().TrimStart('0'),//����
                                                 fun.ChangeDateTime(obj.CureDate.ToString()),//������ʼ����
                                                 fun.ChangeDateTime(obj.CureDate.ToString()),//������ֹ����
                                                 obj.DrugInfo.Name+"("+obj.Qty.ToString()+obj.Unit.ToString()+")",//����ҩ�����Ƽ�����
                                                 obj.Period.ToString(),//�����Ƴ�
                                                 obj.Result.ToString(),//��Ч���
                                                 "" //��Ч
                                                 );
            }
            catch
            {
            }

            ReadSQL(sql);

            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ba8  ������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int InsertBa8(FS.HISFC.Models.HealthRecord.Base patientInfo)
        {
            string strSQL = this.fun.GetInsertba8SQL(patientInfo);

            if (strSQL == null || strSQL == "")
            {
                return -1;
            }

            ReadSQL(strSQL);

            if (this.cmd.ExecuteNonQuery() <= 0)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// HIS_BA7 --��������ҩ��
        /// </summary>
        /// <param name="alTumourDetail"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int InsertBa10(System.Collections.ArrayList alTumourDetail, FS.HISFC.Models.HealthRecord.Base patientInfo)
        {
            foreach (FS.HISFC.Models.HealthRecord.TumourDetail info in alTumourDetail)
            {
                string strSQL = this.fun.GetInsertba10SQL(info, patientInfo);

                if (strSQL == null || strSQL == "")
                {
                    return -1;
                }

                ReadSQL(strSQL);

                if (this.cmd.ExecuteNonQuery() <= 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ԭ����ba3_f���뵽�µ�HIS_BA3��
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alBA3_F"></param>
        /// <returns></returns>
        public int InsertBa3_f(FS.HISFC.Models.HealthRecord.Base patientInfo, System.Collections.ArrayList alBA3_F)
        {
            this.IsOpen();

            this.ConverICDType(alBA3_F);

            this.cmd.CommandText = string.Empty;
            string strsql = @"insert into tDiagnose (fprn,ftimes,FZDLX, FICDVersion, FICDM, FJBNAME,FZLJGBH,FZLJG) values ('{0}',{1},'{2}',{3},'{4}','{5}','{6}','{7}')";
            string temp = string.Empty;

            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in alBA3_F)
            {
                if (obj.SecondICD.Trim() == "")
                {
                    continue;
                }

                if (obj.DiagInfo.DiagType.ID == "1")
                {
                    continue;
                }

                this.cmd.CommandText = string.Empty;
                temp = strsql;
                this.cmd.CommandText = string.Format(temp,
                                                     patientInfo.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr()),
                                                     patientInfo.PatientInfo.InTimes.ToString().PadLeft(2, '0'),
                                                     obj.DiagInfo.DiagType.ID,//.HappenNo,//
                                                     "10",
                                                     obj.SecondICD,
                                                     "",//��ûд
                                                     obj.DiagOutState,
                                                     this.constMana.GetConstant ( "ZG", obj.DiagOutState ).Name );//ûȷ��

                if (this.cmd.ExecuteNonQuery() <= 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="inpatientNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public int Delete(string tableName, string inpatientNo, string times)
        {
            string strSQL = this.fun.GetDeleteSQL(tableName, inpatientNo, times);

            if (strSQL == null || strSQL == "")
            {
                return -1;
            }

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }

        #region ����۲��Һͼ�����־
        /// <summary>
        /// tEmergeLogNoKs -- ���Ｑ����־(���ֿ�)
        /// </summary>
        /// <param name="alUpload">�����ձ�����</param>
        /// <param name="beginDate">��ʼ����</param>
        /// <param name="endDate">��������</param>
        /// <param name="isExist">�Ƿ������ݱ�����</param>
        /// <returns></returns>
        public int InserttEmergeLog(System.Collections.ArrayList alUpload, string beginDate, string endDate, ref bool isExist)
        {


            int i = 0;
            string strSQL = "delete tEmergeLog where FRQ  between '{0}' and '{1}'";
            ReadSQL(string.Format(strSQL, beginDate, endDate));
            try
            {
                i = this.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (i > 0) isExist = true;
            i = 0;

          
            foreach (FS.HISFC.Models.HealthRecord.Base b in alUpload)
            {
                string sql = @"INSERT INTO tEmergeLog
                              (frq,
                            ftykh,
                            fksname,
                            FdoctorNum,
                            Fhour,
                            Fnum,
                            FdeadNum,
                            FqjNum,
                            FqjsucNum,
                            Fcars,
                            FoperNum,
                            FWorkrq
                            )
                            VALUES
                              (
                               '{0}',
                               '{1}',
                               '{2}',
                               {3},
                               {4},
                               {5},
                               {6},
                               {7},
                               {8},
                               {9},
                               {10},
                               '{11}'
                            )";
                sql = string.Format(sql, b.PatientInfo.PVisit.InTime, b.InDept.ID,b.InDept.Name,b.PatientInfo.ID,b.PatientInfo.Memo, b.PatientInfo.User01, b.PatientInfo.User02,
                                         b.PatientInfo.User03, b.PatientInfo.UserCode, b.PatientInfo.WBCode, b.PatientInfo.PVisit.User01,
                                         b.PatientInfo.PVisit.OutTime);

                ReadSQL(sql);
                if (this.cmd.ExecuteNonQuery() < 0)
                {
                    //return -1;
                }
                i++;
            }
            //if (intDelete > 0) isExist = true;
            return i;
        }

        /// <summary>
        /// HIS_MZLOG5 -- ����۲�����־(���ֿ�)
        /// </summary>
        /// <param name="alPatientMove">�۲����ձ�����</param>
        /// <param name="beginDate">��ʼ����</param>
        /// <param name="endDate">��������</param>
        /// <param name="isExist">�Ƿ������ݱ�����</param>
        /// <returns></returns>
//        public int InsertHisMzlog5 ( System.Collections.ArrayList alObserver, string beginDate, string endDate, ref int intDelete )
//        {
//            this.IsOpen ( );
//            //int intDelete = 0;
//            string strSQL = "delete tObservelogNoKs where FRQ = '{0}'";
//            string strsql = @"INSERT INTO tObservelogNoKs
//  (FRq,
//Fbedsum,
//Foldbrrs,
//Finbrrs,
//Foutbrrs,
//Foutdeadbrrs,
//Fqjbrrsum,
//Fqjsuc,
//Fnowbrrs,
//Foutbrbedts,
//Fallbeddays,
//Fywcwwryrs,
//FWorkrq
//)
//VALUES
//  (
//   '{0}',
//   {1},
//   {2},
//   {3},
//   {4},
//   {5},
//   {6},
//   {7},
//   {8},
//   {9},
//   {10},
//   {11},
//   '{12}'
//)";
//            string temp = string.Empty;
//            int intReturn = 0;
//            foreach ( FS.FrameWork.Models.Nurse.ObserveReport clinic in alObserver )
//            {
//                int j = 0;
//                // string oldDeptCode = this.ConverDept ( patientMove.DeptCode );
//                this.cmd.CommandText = string.Empty;
//                this.cmd.CommandText = string.Format ( strSQL, clinic.ReportDate.Replace ( '-', '/' ) );
//                try
//                {
//                    j = this.cmd.ExecuteNonQuery ( );
//                }
//                catch ( Exception ex )
//                {
//                    this.Err = ex.Message;
//                }
//                if ( j > 0 )
//                    intDelete += j;


//                this.cmd.CommandText = string.Empty;
//                temp = strsql;
//                this.cmd.CommandText = string.Format ( temp,
//                                                 clinic.ReportDate.Replace ( '-', '/' ),
//                                                 clinic.BedCount,
//                                                 clinic.Count1,
//                                                 clinic.Count2,
//                                                 clinic.OutCount,
//                                                 clinic.OutCount3,
//                                                 clinic.OutCount1,
//                                                 clinic.OutCount2,
//                                                 clinic.TodayCount,
//                                                 clinic.TodayCount1,
//                                                 clinic.TodayCount2,
//                                                 "0",//���޴�λδסԺ����
//                                                 clinic.Memo.Replace ( '-', '/' )
//                                                 );

//                if ( this.cmd.ExecuteNonQuery ( ) <= 0 )
//                {
//                    return -1;
//                }
//                intReturn++;
//            }
//            //if (intDelete > 0) isExist = true;
//            return intReturn;
//        }

        /// <summary>
        /// tSpecialLog --ר�����ﲡ����
        /// </summary>
        /// <param name="alUpload">ר�����ﲡ���ձ�����</param>
        /// <param name="beginDate">��ʼ����</param>
        /// <param name="endDate">��������</param>
        /// <param name="isExist">�Ƿ������ݱ�����</param>
        /// <returns></returns>
        public int InserttSpecialLog(System.Collections.ArrayList alUpload, string beginDate, string endDate, ref bool isExist)
        {


            int i = 0;
            string strSQL = "delete tSpecialLog where FRQ  between '{0}' and '{1}'";
            ReadSQL(string.Format(strSQL, beginDate, endDate));
            try
            {
                i = this.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (i > 0) isExist = true;
            i = 0;

        
            foreach (FS.HISFC.Models.HealthRecord.Base b in alUpload)
            {
                string sql = @"INSERT INTO tSpecialLog
                              (frq,
                            FTykh,
                            FKsName,
                            FTyzkcode,
                            FzkName,
                            FZlrs,
                            FWorkrq
                            )
                            VALUES
                              (
                               '{0}',
                               '{1}',
                               '{2}',
                               '{3}',
                               '{4}',
                                {5}, 
                               '{6}'
                            )";
                sql = string.Format(sql, b.PatientInfo.PVisit.InTime, b.InDept.ID, b.InDept.Name, b.OutDept.ID,b.OutDept.Name,
                                          b.PatientInfo.ID, b.PatientInfo.PVisit.OutTime);

                ReadSQL(sql);
                if (this.cmd.ExecuteNonQuery() < 0)
                {
                    //return -1;
                }
                i++;
            }
            //if (intDelete > 0) isExist = true;
            return i;
        }

        #endregion

        #region סԺ��̬�ձ�
        /// <summary>
        /// HIS_ZYLOG -- ������̬��־
        /// ���������HIS��ʱ����Ϊ������ʽ��TZyWardWorkDayReport
        /// </summary>
        /// <param name="alPatientMove">��̬�ձ�����</param>
        /// <param name="beginDate">��ʼ����</param>
        /// <param name="endDate">��������</param>
        /// <param name="isExist">�Ƿ������ݱ�����</param>
        /// <returns></returns>
        public int InsertZbfworklog(System.Collections.ArrayList alPatientMove, string beginDate, string endDate, ref int intDelete)
        {
            this.IsOpen();
            //int intDelete = 0;
            string strSQL = "delete TZyWardWorklog where FRQ = '{0}' and FTykh= '{1}'";

            string strsql = @"INSERT INTO TZyWardWorklog
  (FRQ,
FTykh,
FKsName,
FBEDSUM,
FStatFlag,
FYRS,
FRYRS,
FTKZR,
FTQZR,
FCYRS,
FDEADRS,
FZRTK,
FZRTQ,
FXYRS,
FPRS,
FBCZYL,
FWorkrq
)
VALUES
  (
   '{0}',
   '{1}',
   '{2}',
   {3},
   '{4}',
   {5},
   {6},
   {7},
   {8},
   {9},
   {10},
   {11},
   {12},
   {13},
   {14},
   {15},
   '{16}'
)";
            string temp = string.Empty;
            int intReturn = 0;
            foreach (FS.HISFC.Models.HealthRecord.Case.PatientMove patientMove in alPatientMove)
            {
                int j = 0;
                string oldDeptCode = this.ConverDept(patientMove.DeptCode);
                this.cmd.CommandText = string.Empty;
                this.cmd.CommandText = string.Format(strSQL, patientMove.OperDate.ToShortDateString().Replace('-', '/'), oldDeptCode);
                try
                {
                    j = this.cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                }
                if (j > 0) intDelete+=j;

                
                this.cmd.CommandText = string.Empty;
                temp = strsql;
                this.cmd.CommandText = string.Format(temp,
                                                 patientMove.OperDate.ToShortDateString ( ).Replace ( '-', '/' ),
                                                 "TZY" + oldDeptCode +"*1",
                                                 this.deptMana.GetDeptmentById(patientMove.DeptCode).Name,
                                                 //GetOldInDeptCode(patientMove.DeptCode),
                                                 //patientMove.Name,
                                                 patientMove.BedNum.ToString(),
                                                 "***",
                                                 patientMove.OriginalNum.ToString(),
                                                 patientMove.InNum.ToString(),
                                                 patientMove.OtherDeptIn.ToString(),
                                                 patientMove.OtherRegionIn.ToString(),
                                                 patientMove.OutNum.ToString(),
                                                 patientMove.DeadNum.ToString(),
                                                 patientMove.ToOtherDept.ToString(),
                                                 patientMove.ToOtherRegion.ToString(),
                                                 patientMove.PatientNum.ToString(),
                                                 patientMove.AccompanyNum.ToString(),
                                                 patientMove.BeduseNum.ToString(),//**��λռ����,ûд�㷨��Ҫ�ڱ��ֲ㸳ֵ**
                                                 patientMove.Memo.Replace('-', '/')
                                                 );

                if (this.cmd.ExecuteNonQuery() <= 0)
                {
                    return -1;
                }
                intReturn++;
            }
            //if (intDelete > 0) isExist = true;
            return intReturn;
        }

        public string GetOldInDeptCode(string newDeptCode)
        {
            string oldDeptname = "";
            try
            {
                oldDeptname = this.constMana.GetConstant("DEPTUPLOAD", newDeptCode).Memo;
            }
            catch
            {
            }
           
            return oldDeptname;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientMove"></param>
        /// <returns></returns>
        public int InsertZbfworklog(FS.HISFC.Models.HealthRecord.Case.PatientMove  patientMove)
        {
            this.IsOpen();
        

            this.cmd.CommandText = string.Empty;
            string strsql = @"INSERT INTO TZyWardWorklog
  (FRQ,
FTykh,
FKsName,
FBEDSUM,
FStatFlag,
FYRS,
FRYRS,
FTKZR,
FTQZR,
FCYRS,
FDEADRS,
FZRTK,
FZRTQ,
FXYRS,
FPRS,
FBCZYL,
FWorkrq
)
VALUES
  (
   '{0}',
   '{1}',
   '{2}',
   {3},
   '{4}',
   {5},
   {6},
   {7},
   {8},
   {9},
   {10},
   {11},
   {12},
   {13},
   {14},
   {15},
   '{16}'
)";

            this.cmd.CommandText = string.Format(strsql,
                                                 patientMove.OperDate.ToShortDateString ( ).Replace ( '-', '/' ),
                                                 "TZY" + this.ConverDept ( patientMove.DeptCode ) + "*1",
                                                 this.deptMana.GetDeptmentById(patientMove.DeptCode).Name,
                                                 //patientMove.Name,
                                                 patientMove.BedNum.ToString(),
                                                 "***",
                                                 patientMove.OriginalNum.ToString(),
                                                 patientMove.InNum.ToString(),
                                                 patientMove.OtherDeptIn.ToString(),
                                                 patientMove.OtherRegionIn.ToString(),
                                                 patientMove.OutNum.ToString(),
                                                 patientMove.DeadNum.ToString(),
                                                 patientMove.ToOtherDept.ToString(),
                                                 patientMove.ToOtherRegion.ToString(),
                                                 patientMove.PatientNum.ToString(),
                                                 patientMove.AccompanyNum.ToString(),
                                                 patientMove.BeduseNum.ToString ( ),//**��λռ����,ûд�㷨��Ҫ�ڱ��ֲ㸳ֵ**
                                                 patientMove.Memo.Replace('-', '/')
                                                 );

            if (this.cmd.ExecuteNonQuery() <= 0)
            {
                return -1;
            }

            return 1;
        }


        public int DeleteZbfworklog(FS.HISFC.Models.HealthRecord.Case.PatientMove patientMove)
        {
            string strSQL = @"delete from TZyWardWorklog where FTykh = '{0}' and FRQ = '{1}'";

            strSQL = string.Format(strSQL, this.ConverDept(patientMove.DeptCode), patientMove.OperDate.ToShortDateString().Replace('-', '/'));

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }

        #endregion

        /// <summary>
        /// �ϴ�������Ϣ(���޸�)
        /// </summary>
        /// <returns></returns>
        public int UpdateFeeinfo(FS.HISFC.Models.HealthRecord.Base b, System.Collections.ArrayList alFee)
        {
            string strSQL = @"UPDATE  tPatientVisit
   SET 
       FSUM1 =  {0},
       FCWF = {1},   
       FXYF = {2},
       FZYF = {3},
       FZCYF = {4},
       FZCHYF = {5},
       FJCF = {6},
       FZLF = {7},
       FFSF = {8},
       FSSF = {9},
       FHYF = {10},
       FSXF = {11},
       FSYF = {12},
       FJSF = {13},
       FQTF = {14}
 WHERE fprn = '{15}' and frydate = '{16}' and fsum1 = 0";



            strSQL = string.Format(strSQL, this.GetFeeinfo(b, alFee));

            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ȡ������Ϣ(����Ҫ�޸�)
        /// </summary>
        /// <param name="b"></param>
        /// <param name="alFee"></param>
        /// <returns></returns>
        private string[] GetFeeinfo(FS.HISFC.Models.HealthRecord.Base b, System.Collections.ArrayList alFee)
        {
            string[] s = new string[17];

            for (int j = 0; j < s.Length; j++)
            {
                s[j] = "0.00";
            }

            decimal feeTot = 0.0M;
            decimal feeOther = 0.0M;
            foreach (FS.HISFC.Models.RADT.Patient feeInfo in alFee)
            {
                decimal fee1 = 0.0M;
                fee1 = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(feeInfo.IDCard), 2);

                feeTot += fee1;

                string fee = fee1.ToString();

                if (feeInfo.DIST == "1")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[1]) + fee1;
                    s[1] = temp.ToString();
                }
                else if (feeInfo.DIST == "3")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[2]) + fee1;
                    s[2] = temp.ToString();
                }
                else if (feeInfo.DIST == "5")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[4]) + fee1;
                    s[4] = temp.ToString();
                }
                else if (feeInfo.DIST == "4")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[5]) + fee1;
                    s[5] = temp.ToString();
                }
                else if (feeInfo.DIST == "13")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[6]) + fee1;
                    s[6] = temp.ToString();
                }
                else if (feeInfo.DIST == "10")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[7]) + fee1;
                    s[7] = temp.ToString();
                }
                else if (feeInfo.DIST == "11")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[9]) + fee1;
                    s[9] = temp.ToString();
                }
                else if (feeInfo.DIST == "7")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[10]) + fee1;
                    s[10] = temp.ToString();
                }
                else if (feeInfo.DIST == "9")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[11]) + fee1;
                    s[11] = temp.ToString();
                }
                else if (feeInfo.DIST == "8")
                {
                    decimal temp = FS.FrameWork.Function.NConvert.ToDecimal(s[12]) + fee1;
                    s[12] = temp.ToString();
                }
                else
                {
                   
                    feeOther += fee1;
                }
            }
            s[0] = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(feeTot), 2).ToString();
            s[3] = "0.00";
            s[8] = "0.00";
            s[13] = "0.00";
            s[14] = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(feeOther), 2).ToString();

            s[15] = b.PatientInfo.PID.PatientNO.Substring(this.PatientNoSubstr());
            s[16] = b.PatientInfo.PVisit.InTime.ToShortDateString().Replace('-', '/');

            return s;
            
        }

        /// <summary>
        /// �Ӳ���ȡ����
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public System.Collections.ArrayList GetPatientInfoFromBA(string begin, string end)
        {
            string strSQL = @"SELECT tPatientVisit.FPRN,
       FNAME,
       FTIMES,
       FFB,
       FAGE,
       FJOB,
       FSTATUS,
       FDWNAME,
       FDWADDR,
       FDWTELE,
       FDWPOST,
       FHKADDR,
       FHKPOST,
       FLXNAME,
       FRELATE,
       FLXADDR,
       FLXTELE,
       FRYDATE,
       FRYTIME,
       FRYINFO,
       FSOURCE,
       FRYTYKH,
       FRYDEPT,
       FZKTYKH,
       FZKDEPT,
       FCYTYKHM,
       FCYDEPT,
       FCYDATE,
       FCYTIME,
       FDAYS,
       FMZZD,
       FRYZD,
       FQZDATE,
       FIFSS,
       FIFFYK,
       FBFZ,
       FYNGR,
       FPHZD,
       FGMYW,
       FBLOOD,
       FQJTIMES,
       FQJSUCTIMES,
       FISSZ,
       FSZQX,
       FBODY,
       FSUM1,
       FCWF,
       FXYF,
       FZYF,
       FZCYF,
       FZCHYF,
       FJCF,
       FZLF,
       FFSF,
       FSSF,
       FHYF,
       FSXF,
       FSYF,
       FJSF,
       FQTF,
       FSAMPLE,
       FQUALITY,
       FZRDOCTOR,
       FZZDOCT,
       FZYDOCT,
       FSXDOCT,
       FBMY,
       FMZCYACCO,
       FRYCYACCO,
       FOPACCO,
       FISZLFIRST,
       FISJCFIRST,
       FISZDFIRST,
       FTWILL,
       FQJBR,
       FQJSUC,
       FTHREQZ,
       FBABYNUM,
       FZLFZY, 
       FIFDBZ,
       FBACK,
       FSXFY,
       FSYFY,
       FWORKRQ,
       IFZDSS,
       FIFZDSS,
       FMZDOCT,
       FJBFX
  FROM HIS_BA1, temp1 where HIS_BA1.fprn = temp1.prn and HIS_BA1.ftimes = temp1.times and HIS_BA1.fprn >= '{0}' and HIS_BA1.fprn <= '{1}'";

            this.cmd.CommandText = string.Empty;

            try
            {
                strSQL = string.Format(strSQL, begin, end);
            }
            catch (Exception ex)
            {
                return null;
            }

            ReadSQL(strSQL);

            System.Collections.ArrayList al = new System.Collections.ArrayList();

            this.reader = this.cmd.ExecuteReader();

            try
            {
                while (this.reader.Read())
                {
                    FS.HISFC.Models.HealthRecord.Base b = new FS.HISFC.Models.HealthRecord.Base();

                    b.PatientInfo.PID.PatientNO = this.reader[0].ToString();
                    b.PatientInfo.Name = this.reader[1].ToString();

                    al.Add(b);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.reader.Close();
            }


            return al;
        }

        /// <summary>
        /// ���ݲ����Ų�ѯ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientFromBA(string cardNO)
        {
            string strSQL = @"SELECT tPatientVisit.FPRN,
       FNAME,
       FTIMES,
       FFB,
       FAGE,
       FJOB,
       FSTATUS,
       FDWNAME,
       FDWADDR,
       FDWTELE,
       FDWPOST,
       FHKADDR,
       FHKPOST,
       FLXNAME,
       FRELATE,
       FLXADDR,
       FLXTELE,
       FRYDATE,
       FRYTIME,
       FRYINFO,
       FSOURCE,
       FRYTYKH,
       FRYDEPT,
       FZKTYKH,
       FZKDEPT,
       FCYTYKH,
       FCYDEPT,
       FCYDATE,
       FCYTIME,
       FDAYS,
       FMZZD,
       FRYZD,
       FQZDATE,
       FIFSS,
       FIFFYK,
       FBFZ,
       FYNGR,
       FPHZD,
       FGMYW,
       FBLOOD,
       FQJTIMES,
       FQJSUCTIMES,
       FISSZ,
       FSZQX,
       FBODY,
       FSUM1,
       FCWF,
       FXYF,
       FZYF,
       FZCYF,
       FZCHYF,
       FJCF,
       FZLF,
       FFSF,
       FSSF,
       FHYF,
       FSXF,
       FSYF,
       FJSF,
       FQTF,
       FSAMPLE,
       FQUALITY,
       FZRDOCTOR,
       FZZDOCT,
       FZYDOCT,
       FSXDOCT,
       FBMY,
       FMZCYACCO,
       FRYCYACCO,
       FOPACCO,
       FISZLFIRST,
       FISJCFIRST,
       FISZDFIRST,
       FTWILL,
       FQJBR,
       FQJSUC,
       FTHREQZ,
       FBABYNUM,
       FZLFZY, 
       FIFDBZ,
       FBACK,
       FSXFY,
       FSYFY,
       FWORKRQ,
       FIFZDSS,
       FMZDOCT,
       FJBFX
       FSEX,
        FBIRTHDAY,
        FBIRTHPLACE,
        FIDCard,
        fcountry,
        fnationality
  FROM tPatientVisit where tPatientVisit.fprn = '{0}' order by ftimes desc";

            this.cmd.CommandText = string.Empty;

            try
            {
                strSQL = string.Format(strSQL, cardNO);
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;

                return null;
            }

            ReadSQL(strSQL);

            this.reader = this.cmd.ExecuteReader();

            try
            {
                if (this.reader.Read())
                {
                    try
                    {
                        FS.HISFC.Models.RADT.PatientInfo PatientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                        PatientInfo.PID.CardNO = this.reader[0].ToString();
                        //PatientInfo.Birthday.ToString(); //��������
                        //PatientInfo.Sex.ID.ToString(); //�Ա�
                        //PatientInfo.IDCard; //���֤��
                        PatientInfo.Name = this.reader[1].ToString();

                        string sex = this.reader[88].ToString();
                        if (sex.Trim().Equals("1"))
                        {
                            sex = "M";
                        }
                        else if (sex.Trim().Equals("2"))
                        {
                            sex = "F";
                        }
                        PatientInfo.Sex.ID = sex;

                        PatientInfo.IDCard = this.reader[91].ToString();

                        PatientInfo.Profession.ID = this.reader[5].ToString(); //ְҵ

                        PatientInfo.CompanyName = this.reader[7].ToString(); //������λ
                        PatientInfo.PhoneBusiness = this.reader[9].ToString(); //��λ�绰
                        PatientInfo.BusinessZip = this.reader[10].ToString(); //��λ�ʱ�
                        PatientInfo.AddressHome = this.reader[11].ToString(); //���ڻ��ͥ����
                        //PatientInfo.PhoneHome = this.reader[10].ToString(); //��ͥ�绰
                        PatientInfo.HomeZip = this.reader[12].ToString(); //���ڻ��ͥ��������
                        //PatientInfo.DIST = this.reader[10].ToString(); //����
                        //PatientInfo.Nationality.ID = this.reader[10].ToString(); //����
                        PatientInfo.Kin.Name = this.reader[13].ToString(); //��ϵ������
                        PatientInfo.Kin.RelationPhone = this.reader[16].ToString(); //��ϵ�˵绰
                        PatientInfo.Kin.RelationAddress = this.reader[15].ToString(); //��ϵ��סַ
                        PatientInfo.Kin.Relation.Name = this.reader[14].ToString(); //��ϵ�˹�ϵ

                        PatientInfo.MaritalStatus.ID = this.reader[6].ToString(); //����״��
                        //PatientInfo.Country.ID = this.reader[10].ToString(); //����

                        //PatientInfo.AreaCode = this.reader[10].ToString(); //������
                        try
                        {
                            PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[89].ToString().Replace('/', '-') + " 00:00:00");
                        }
                        catch
                        {
                            PatientInfo.Birthday = DateTime.MinValue;
                        }

                        PatientInfo.AreaCode = this.reader[90].ToString();

                        PatientInfo.Country.Name = this.reader[92].ToString();
                        PatientInfo.Nationality.Name = this.reader[93].ToString();
                        //��Դ
                        PatientInfo.PVisit.InSource.ID = this.reader[20].ToString();
                        //ҽ����

                        PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.reader[2].ToString());

                        try
                        {
                            PatientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[27].ToString().Replace('/', '-') + " 00:00:00");
                        }
                        catch
                        {
                            PatientInfo.PVisit.OutTime = DateTime.MinValue;

                            PatientInfo.User01 = this.reader[27].ToString();
                        }

                        PatientInfo.User02 = this.reader[38].ToString();//����ҩ��

                        return PatientInfo;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    this.Err1 = "û������";

                    return null;
                }
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }

        }

        /// <summary>
        /// HIS_MZLOG9 -- ר�����ﲡ����
        /// </summary>
        /// <param name="alFee"></param>
        /// <returns></returns>
        public int InsertMspecial(System.Collections.ArrayList alFee)
        {
            int i = 0;
            foreach (FS.HISFC.Models.Base.Employee empl in alFee)
            {
                string strSQL = this.fun.GetInsertMspecialSQL(empl);

                if (strSQL == null || strSQL == "")
                {
                    //return -1;
                }

                ReadSQL(strSQL);

                if (this.cmd.ExecuteNonQuery() <= 0)
                {

                    //return -1;
                }
                //return 1;
                i++;
            }
            return i;
        }

        /// <summary>
        /// HIS_MZLOG1 -- �������﹤����־
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int InsertMworklog1(System.Collections.ArrayList alFee)
        {
            int i = 0;
            foreach (FS.HISFC.Models.Base.Employee empl in alFee)
            {
                string strSQL = this.fun.GetInsertMworklog1SQL(empl);

                if (strSQL == null || strSQL == "")
                {
                    //return -1;
                }

                ReadSQL(strSQL);

                if (this.cmd.ExecuteNonQuery() <= 0)
                {

                    //return -1;
                }
                //return 1;
                i++;
            }
            return i;
        }

        /// <summary>
        /// HIS_MZLOG2 -- ҽ�����﹤����־
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int InserttWorkLog(System.Collections.ArrayList alUpload, string beginDate, string endDate, ref bool isExist)
        {
            int i = 0;
            //ɾ��ͬһ��ļ�¼
            string strSQL = "delete tWorklog where frq between '{0}' and '{1}'";
            ReadSQL(string.Format(strSQL, beginDate, endDate));
            try
            {
                i = this.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (i > 0) isExist = true;
            i = 0;

            string sql = string.Empty;
            foreach (FS.HISFC.Models.HealthRecord.Base b in alUpload)
            {
                sql = this.fun.GetInserttWorkLogSQL(b);

                if (sql == null || sql == "")
                {
                    this.err = fun.Err;
                    return -1;
                }

                this.cmd.CommandText = sql;

                if (this.cmd.ExecuteNonQuery() <= 0)
                {

                    //return -1;
                }
                //return 1;
                i++;
            }
            return i;
        }

        /// <summary>
        /// �Ƿ��Ѿ�����
        /// ���أ�-1 ʧ��  1 ����  2 ��¼������ϴ�  3 ��Ҫ�ϴ�
        /// 1��������tPatientVisit�Ƿ���¼��
        /// 2���ӿڱ�his_ba1�Ƿ����ϴ�
        /// </summary>
        /// <param name="prn">������</param>
        /// <param name="outTime">��Ժ����</param>
        /// <returns></returns>
        public int GetIsHave(string prn,  DateTime outTime)
        {
            string strSQLHisba1 = @"select count(*) from tPatientVisit where fprn = '{0}' and fcydate = '{1}'";

            string strSQLTpatientVisit = @"select count(*) from his_ba1 where fprn = '{0}' and fcydate = '{1}'";

            try
            {
                strSQLHisba1 = string.Format(strSQLHisba1, prn, outTime.ToShortDateString().Replace('-', '/'));

                strSQLTpatientVisit = string.Format(strSQLTpatientVisit, prn, outTime.ToShortDateString().Replace('-', '/'));
            }
            catch
            {
                return -1;
            }

            ReadSQL(strSQLHisba1);

            this.reader = this.cmd.ExecuteReader();

            bool haveHisba1 = false;
            bool haveTpatientVisit = false;


            try
            {
                if (this.reader.Read())
                {
                    if (this.reader[0].ToString().Trim().Equals("0"))
                    {
                        haveHisba1 = false;
                    }
                    else
                    {
                        haveHisba1 = true;
                    }
                }
                else
                {
                    if (!this.reader.IsClosed)
                    {
                        this.reader.Close();
                    }
                    return -1;
                }
            }
            catch
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }

                return -1;
            }

            if (!this.reader.IsClosed)
            {
                this.reader.Close();
            }

            this.cmd.CommandText = strSQLTpatientVisit;

            this.reader = this.cmd.ExecuteReader();

            try
            {
                if (this.reader.Read())
                {
                    if (this.reader[0].ToString().Trim().Equals("0"))
                    {
                        haveTpatientVisit = false;
                    }
                    else
                    {
                        haveTpatientVisit = true;
                    }
                }
                else
                {
                    if (!this.reader.IsClosed)
                    {
                        this.reader.Close();
                    }
                    return -1;
                }
            }
            catch
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }

                return -1;
            }

            if (!this.reader.IsClosed)
            {
                this.reader.Close();
            }

            if (haveHisba1)
            {
                if (haveTpatientVisit)
                {
                    //������
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                if (haveTpatientVisit)
                {
                    return 2;
                }
                else
                {
                    //��û��
                    return 3;
                }
            }

        }

        /// <summary>
        /// ���סԺ����
        /// ref inTimes =0 ����Ҫ���ģ��������
        /// </summary>
        /// <param name="prn">סԺ��</param>
        /// <param name="fzyid">סԺ��ˮ��</param>
        /// <param name="itype">���� 1�Ѿ��ϴ�δ¼��  2ȡ�м���¼�������סԺ���� </param>
        /// <param name="inTimes"></param>
        /// <returns></returns>
        public int GetInTimes(string prn,string fzyid,int itype ,ref string inTimes)
        {
            string strSQLinTimes=string.Empty;
            if (itype == 1)
            {
                strSQLinTimes = @"SELECT FTIMES FROM HIS_BA1 WHERE FZYID='{0}'";
                try
                {
                    strSQLinTimes = string.Format(strSQLinTimes,fzyid);
                }
                catch
                {
                    return -1;
                }
            }
            else
            {
                strSQLinTimes = @"select case when  max(a.ftimes) is null then 0 else  max(a.ftimes) end   ftimes from 
                                    (select max(ftimes) ftimes from his_ba1        where fprn='{0}'
                                    union 
                                    select max(ftimes)  ftimes from  tpatientvisit  where fprn='{0}') a";

                try
                {
                    strSQLinTimes = string.Format(strSQLinTimes, prn);
                }
                catch
                {
                    return -1;
                }
            }
            ReadSQL(strSQLinTimes);

            this.reader = this.cmd.ExecuteReader();
            try
            {
                if (this.reader.Read())
                {
                    inTimes = this.reader[0].ToString().Trim();
                }
                else
                {
                    if (!this.reader.IsClosed)
                    {
                        this.reader.Close();
                    }
                    return -1;
                }
            }
            catch
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }

                return -1;
            }
            finally
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }
            }
            return 1;
        }

        /// <summary>
        /// �����ж��Ƿ��У�����У����ж��Ƿ�һ��
        /// </summary>
        /// <param name="prn"></param>
        /// <param name="patientNO"></param>
        /// <param name="name"></param>
        /// <param name="times"></param>
        /// <param name="inTime"></param>
        /// <returns></returns>
        public int GetIsHaveNew(string prn,string patientNO,string name, ref string times,DateTime inTime)
        {
            string strSql1= "select FNAME from tPatientVisit where FPRN='{0}'";

            string strSql2 = "select count(*) from tPatientVisit where FPRN='{0}' and FTIMES={1}";

            string strSql3 = @"select max(c),max(ftimes) from 
                                        (select max(FCYDATE) c,ftimes ftimes from his_ba1
                                        where ftimes=(select max(ftimes) from his_ba1 where fprn='{0}')
                                        and fprn='{0}' 
                                        group by ftimes
                                        union 
                                        select max(FCYDATE) c,ftimes from tpatientvisit
                                        where ftimes=(select max(ftimes) from tpatientvisit
                                        where fprn='{0}')
                                        and fprn='{0}'
                                        group by ftimes) a";

            try
            {
                strSql1 = string.Format(strSql1, prn);
                strSql2 = string.Format(strSql2, prn, times);
                strSql3 = string.Format(strSql3, prn, times);
            }
            catch(Exception e)
            {
                this.Err ="��ʽ��SQL������"+ e.Message;
                return -1;
            }

            int retValue = -1;
            try
            {
                //����Ѿ�����


                this.ReadSQL(strSql1);
                this.reader=this.cmd.ExecuteReader();

                if (this.reader.Read())
                {
                    if (this.reader[0].ToString().Trim() == name.Trim())
                    {
                        retValue = 2;
                    }
                    else
                    {
                        retValue = 0;
                    }
                }
                else
                {
                    retValue = 1;
                }
                this.reader.Close();

                //�ж��Ƿ����
                if (retValue == 2)
                {
                    this.ReadSQL(strSql2);
                    this.reader=this.cmd.ExecuteReader();
                    if (this.reader.Read())
                    {
                        if (FS.FrameWork.Function.NConvert.ToInt32(this.reader[0].ToString()) > 0)
                        {
                            retValue = 2;                            
                        }
                        else
                        {
                            retValue = 1;
                        }
                    }
                    else
                    {
                        retValue = 1;
                    }

                    this.reader.Close();
                }

                //�жϳ�Ժ����
                if (retValue == 2)
                {
                    this.ReadSQL(strSql3);
                    this.reader = this.cmd.ExecuteReader();
                    if (this.reader.Read())
                    {
                        //�ϴγ�Ժʱ��С�ڱ�����Ժʱ��  �޸Ĵ��������ϴ�
                        if (FS.FrameWork.Function.NConvert.ToDateTime(this.reader[0].ToString()).Date < inTime.Date)
                        {
                            retValue = 2;
                            //ȡ������
                            times = FS.FrameWork.Function.NConvert.ToInt32(this.reader[1].ToString()).ToString();
                        }
                        else
                        {
                            retValue = 3;
                        }
                    }
                    else
                    {
                        retValue = 1;
                    }

                    this.reader.Close();
                }


                return retValue;
            }
            catch (Exception e)
            {
                this.Err = "ִ��SQL������" + e.Message;
                return -1;
            }
            finally
            {
                if (this.reader != null && this.reader.IsClosed == false)
                {
                    this.reader.Close();
                }
            }

        }
        /// <summary>
        /// ����סԺ��ˮ���ж��Ƿ��Ѿ�¼��
        /// ����ֵ�� 0 ��Ҫ�ϴ� 1 �Ѿ��ϴ�
        /// </summary>
        /// <param name="fprn">������</param>
        /// <param name="fzyid">סԺ��ˮ��</param>
        /// <returns></returns>
        public int GetIsNeedUpload(string fprn, string fzyid)
        {
            int iReturn = 0;
            int NotNeed = 0;
            int Need = 0; 
            string strSQLNotNeed = @"select count(1) from tPatientVisit where FPRN ='{0}' AND fzyid = '{1}'";
            string strSQLNeed = @"select count(1) from HIS_BA1 where FPRN ='{0}' AND fzyid = '{1}'";
            try
            {
                strSQLNotNeed = string.Format(strSQLNotNeed, fprn,fzyid);

                strSQLNeed = string.Format(strSQLNeed,fprn, fzyid);
            }
            catch
            {
                return -1;
            }

            ReadSQL(strSQLNotNeed);

            this.reader = this.cmd.ExecuteReader();
            try
            {
                if (this.reader.Read())
                {
                    NotNeed = FS.FrameWork.Function.NConvert.ToInt32(this.reader[0].ToString());
                }
                else
                {
                    if (!this.reader.IsClosed)
                    {
                        this.reader.Close();
                    }
                    return -1;
                }
            }
            catch
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }
                return -1;
            }
            finally
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }
            }

            this.cmd.CommandText = strSQLNeed;

            this.reader = this.cmd.ExecuteReader();

            try
            {
                if (this.reader.Read())
                {
                    Need = FS.FrameWork.Function.NConvert.ToInt32(this.reader[0].ToString());
                }
                else
                {
                    if (!this.reader.IsClosed)
                    {
                        this.reader.Close();
                    }
                    return -1;
                }
            }
            catch
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }
                return -1;
            }
            finally
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }
            }


            if (NotNeed == 1)
            {
                iReturn = 3;
            }
            else
            {
                if (Need == 1)
                {
                    iReturn = 1;
                }
                else
                {
                    iReturn = 2;
                }
            }
            return iReturn;
        }

        /// <summary>
        /// ����סԺ��ˮ���ж��Ƿ��Ѿ�¼��
        /// ����ֵ�� 0 ��Ҫ�ϴ� 1 �Ѿ��ϴ�
        /// </summary>
        /// <param name="fprn">������</param>
        /// <param name="in_date">��Ժʱ��</param>
        /// <returns></returns>
        public int GetIsHavedNoUpload(string fprn, DateTime in_date)
        {
            int iReturn = 0;
            int NotNeed = 0;
            string strSQLNotNeed = @"SELECT count(1) FROM TPATIENTVISIT t WHERE t.FPRN='{0}' AND t.FRYDATE='{1}'";
            try
            {
                strSQLNotNeed = string.Format(strSQLNotNeed, this.PatientNoChang(fprn.PadLeft(10,'0').Substring(this.PatientNoSubstr())), in_date.ToShortDateString());
            }
            catch
            {
                return -1;
            }

            ReadSQL(strSQLNotNeed);

            this.reader = this.cmd.ExecuteReader();
            try
            {
                if (this.reader.Read())
                {
                    NotNeed = FS.FrameWork.Function.NConvert.ToInt32(this.reader[0].ToString());
                }
                else
                {
                    if (!this.reader.IsClosed)
                    {
                        this.reader.Close();
                    }
                    return -1;
                }
            }
            catch
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }
                return -1;
            }
            finally
            {
                if (!this.reader.IsClosed)
                {
                    this.reader.Close();
                }
            }
            if (NotNeed == 1)
            {
                iReturn = 1;
            }
            else
            {
                iReturn = 0;
            }
            return iReturn;
        }
        #endregion

        #region  ��ѯ  �½ӿڲ���Ҫʹ����Щ�����ֻ���ھ�ϵͳ���뵽��HISϵͳʱʹ��

        /// <summary>
        /// ���ݲ����żӴ���ȡ������Ϣ
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Base GetPatientByCardnoAndTimes
            ( string cardno, string times )
        {
            #region SQL

            //            string strSQL = @"SELECT PRN,
            //       NAME,
            //       TIMES,
            //       FB,
            //       AGE,
            //       JOB,
            //       STATU,
            //       DWNAME,
            //       DWADDR,
            //       DWTELE,
            //       DWPOST,
            //       HKADDR,
            //       HKPOST,
            //       LXNAME,
            //       RELATE,
            //       LXADDR,
            //       LXTELE,
            //       RYDATE,
            //       RYTIME,
            //       RYINFO,
            //       SOURCE,
            //       RYNUM,
            //       RYDEPT,
            //       ZKNUM,
            //       ZKDEPT,
            //       CYNUM,
            //       CYDEPT,
            //       CYDATE,
            //       CYTIME,
            //       DAYS,
            //       MZZD,
            //       RYZD,
            //       QZDATE,
            //       IFSS,
            //       FYK,
            //       BFZ,
            //       YNGR,
            //       PHZD,
            //       GMYW,
            //       BLOOD,
            //       QJTIMES,
            //       SUCTIMES,
            //       SZ,
            //       SZQX,
            //       BODY,
            //       SUM1,
            //       CWF,
            //       XYF,
            //       ZYF,
            //       ZCYF,
            //       ZCHF,
            //       JCF,
            //       ZLF,
            //       FSF,
            //       SSF,
            //       HYF,
            //       SXF,
            //       SYF,
            //       JSF,
            //       QTF,
            //       SAMPLE,
            //       QUALITY,
            //       ZRDOCT,
            //       ZZDOCT,
            //       ZYDOCT,
            //       SXDOCT,
            //       BMY,
            //       MZACCO,
            //       RYACCO,
            //       OPACCO,
            //       OPACCO2,
            //       OPACCO3,
            //       OPACCO4,
            //       TWILL,
            //       QJBR,
            //       QJSUC,
            //       THREQZ,
            //       BABYNUM,
            //       ZLLB,
            //       QJFF,
            //       BACK,
            //       SXFY,
            //       SYFY,
            //       WORKDATE,
            //       IFZDSS,
            //       ifbzzl,
            //       mzdoct,
            //       zlf_zy,
            //       (select ascard1 from ba8 where ba2.prn = ba8.prn and ba2.times = ba8.times) as ascard1,
            //       (select birthday from ba1 where ba1.prn=ba2.prn) as birthday,
            //       (select native from ba1 where ba1.prn=ba2.prn) as native,
            //       (select nation from ba1 where ba1.prn=ba2.prn) as nation,
            //       (select sex from ba1 where ba1.prn=ba2.prn) as sex,
            //       (select idcard from ba1 where ba1.prn=ba2.prn) as idcard,
            //       (select birthpl from ba1 where ba1.prn=ba2.prn) as birthpl,
            //       (select hbsag from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as hbsag,   
            //       (select hcv_ab from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as hcv_ab,
            //       (select hiv_ab from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as hiv_ab,
            //       (select kzr from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as kzr,  
            //       (select jxdoct from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as jxdoct,
            //       (select ysxdoct from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as ysxdoct,
            //       (select zkdoct from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as zkdoct,
            //       (select zknurse from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as zknurse,
            //       (select zkrq from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as zkrq, 
            //       (select rh from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as rh,
            //       (select ISOPFIRST from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as ISOPFIRST,
            //       (select redcell from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as redcell,
            //       (select plaque from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as plaque,
            //       (select serous from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as serous,
            //       (select ALLBLOOD from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as ALLBLOOD,
            //       (select OTHERBLOOD from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as OTHERBLOOD,
            //       (select HZ_YJ from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as HZ_YJ,  
            //       (select HZ_YC from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as HZ_YC,
            //       (select HL_TJ from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as HL_TJ,
            //       (select HL_I from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as HL_I,
            //       (select HL_II from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as HL_II,
            //       (select HL_III from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as HL_III,
            //       (select HL_ZZ from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as HL_ZZ,
            //       (select HL_TS from ba6 where ba6.prn = ba2.prn and ba6.times = ba2.times) as HL_TS
            //
            //
            //  FROM ba2
            //  where prn = '{0}' and times = '{1}'
            //";
            #endregion

            #region SQL

            string strSQL = @"SELECT PRN,
       NAME,
       TIMES,
       FB,
       AGE,
       JOB,
       STATU,
       DWNAME,
       DWADDR,
       DWTELE,
       DWPOST,
       HKADDR,
       HKPOST,
       LXNAME,
       RELATE,
       LXADDR,
       LXTELE,
       RYDATE,
       RYTIME,
       RYINFO,
       SOURCE,
       RYNUM,
       RYDEPT,
       ZKNUM,
       ZKDEPT,
       CYNUM,
       CYDEPT,
       CYDATE,
       CYTIME,
       DAYS,
       MZZD,
       RYZD,
       QZDATE,
       IFSS,
       FYK,
       BFZ,
       YNGR,
       PHZD,
       GMYW,
       BLOOD,
       QJTIMES,
       SUCTIMES,
       SZ,
       SZQX,
       BODY,
       SUM1,
       CWF,
       XYF,
       ZYF,
       ZCYF,
       ZCHF,
       JCF,
       ZLF,
       FSF,
       SSF,
       HYF,
       SXF,
       SYF,
       JSF,
       QTF,
       SAMPLE,
       QUALITY,
       ZRDOCT,
       ZZDOCT,
       ZYDOCT,
       SXDOCT,
       BMY,
       MZACCO,
       RYACCO,
       OPACCO,
       OPACCO2,
       OPACCO3,
       OPACCO4,
       TWILL,
       QJBR,
       QJSUC,
       THREQZ,
       BABYNUM,
       ZLLB,
       QJFF,
       BACK,
       SXFY,
       SYFY,
       WORKDATE,
       IFZDSS,
       ifbzzl,
       mzdoct,
       zlf_zy,
       (select top 1 ascard1 from ba8 where ba2.prn = ba8.prn and ba2.times = ba8.times) as ascard1
       

  FROM ba2
  where prn = '{0}' and times = '{1}'
";
            #endregion

            this.cmd.CommandText = string.Empty;

            try
            {
                strSQL = string.Format ( strSQL, cardno, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;

                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                if ( this.reader.Read ( ) )
                {
                    try
                    {
                        FS.HISFC.Models.HealthRecord.Base b = new FS.HISFC.Models.HealthRecord.Base ( );

                        //b.PatientInfo.ID = "BA" + times.PadLeft(2, '0') + cardno.PadLeft(10, '0');//סԺ��ˮ��

                        b.PatientInfo.PID.PatientNO = this.reader [0].ToString ( ).PadLeft ( 10, '0' );//סԺ������

                        b.PatientInfo.PID.CardNO = this.reader [0].ToString ( ).PadLeft ( 10, '0' );//����

                        b.PatientInfo.Name = this.reader [1].ToString ( );//����

                        //s[4] = b.Nomen;//������



                        b.PatientInfo.Profession.ID = this.reader [5].ToString ( );//ְҵ

                        b.PatientInfo.BloodType.ID = this.reader [39].ToString ( );//Ѫ�ͱ���

                        string mari = this.reader [6].ToString ( );

                        if ( mari.Trim ( ) == "1" )
                        {
                            b.PatientInfo.MaritalStatus.ID = "S";//���
                        }
                        else if ( mari.Trim ( ) == "2" )
                        {
                            b.PatientInfo.MaritalStatus.ID = "M";//���
                        }
                        else if ( mari.Trim ( ) == "3" )
                        {
                            b.PatientInfo.MaritalStatus.ID = "D";//���
                        }
                        else if ( mari.Trim ( ) == "4" )
                        {
                            b.PatientInfo.MaritalStatus.ID = "W";//���
                        }

                        string age = this.reader [4].ToString ( );
                        if ( age.Trim ( ) == "" )
                        {
                            b.PatientInfo.Age = "0";//����
                        }
                        else
                        {
                            try
                            {
                                b.AgeUnit = age.Substring ( 0, 1 );
                            }
                            catch
                            {
                            }

                            try
                            {
                                if ( FS.FrameWork.Public.String.IsNumeric ( age.Substring ( 1 ) ) )
                                {
                                    b.PatientInfo.Age = age.Substring ( 1 );
                                }
                                else
                                {
                                    b.PatientInfo.Age = "0";
                                }
                            }
                            catch
                            {
                            }
                        }

                        //s[13] = b.AgeUnit;//���䵥λ


                        b.PatientInfo.PVisit.InSource.ID = this.reader [20].ToString ( );//������Դ

                        //s[16] = b.PatientInfo.Pact.ID;//��������
                        b.PatientInfo.Pact.ID = this.reader [3].ToString ( );

                        //s[17] = b.PatientInfo.Pact.ID;//��ͬ����

                        b.PatientInfo.SSN = this.reader [88].ToString ( );//ҽ�����Ѻ�



                        b.PatientInfo.AddressHome = this.reader [11].ToString ( );//��ͥסַ

                        //s[22] = b.PatientInfo.PhoneHome;//��ͥ�绰

                        b.PatientInfo.HomeZip = this.reader [12].ToString ( );//סַ�ʱ�

                        b.PatientInfo.AddressBusiness = this.reader [7].ToString ( );//��λ��ַ

                        b.PatientInfo.PhoneBusiness = this.reader [9].ToString ( );//��λ�绰

                        b.PatientInfo.BusinessZip = this.reader [10].ToString ( );//��λ�ʱ�

                        b.PatientInfo.Kin.Name = this.reader [13].ToString ( );//��ϵ��

                        b.PatientInfo.Kin.RelationLink = this.reader [14].ToString ( );//�뻼�߹�ϵ

                        b.PatientInfo.Kin.RelationPhone = this.reader [16].ToString ( );//��ϵ�绰

                        b.PatientInfo.Kin.RelationAddress = this.reader [15].ToString ( );//��ϵ��ַ

                        //s[31] = b.ClinicDoc.ID;//�������ҽ��

                        b.ClinicDoc.Name = this.reader [86].ToString ( );//�������ҽ������

                        //s[33] = b.ComeFrom;//ת��ҽԺ

                        b.PatientInfo.PVisit.InTime = this.GetDateTime ( this.reader [17].ToString ( ) );//��Ժ����

                        b.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [2].ToString ( ) );//סԺ����

                        b.InDept.ID = this.reader [21].ToString ( );//��Ժ���Ҵ���

                        b.InDept.Name = this.reader [22].ToString ( );//��Ժ��������

                        b.PatientInfo.PVisit.InSource.ID = this.reader [20].ToString ( );//��Ժ��Դ

                        b.PatientInfo.PVisit.Circs.ID = this.reader [19].ToString ( );//��Ժ״̬

                        b.DiagDate = this.GetDateTime ( this.reader [32].ToString ( ) );//ȷ������

                        //s[41] = b.OperationDate.ToString();//��������

                        b.PatientInfo.PVisit.OutTime = this.GetDateTime ( this.reader [27].ToString ( ) );//��Ժ����

                        b.OutDept.ID = this.reader [25].ToString ( );//��Ժ���Ҵ���

                        b.OutDept.Name = this.reader [26].ToString ( );//��Ժ��������

                        //s[45] = b.PatientInfo.PVisit.ZG.ID;//ת�����

                        //b.DiagDays.ToString() = this.reader[32].ToString();//ȷ������

                        b.InHospitalDays = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [29].ToString ( ) );//סԺ����

                        //s[48] = b.DeadDate.ToString();//��������

                        //s[49] = b.DeadReason;//����ԭ��

                        b.CadaverCheck = this.reader [44].ToString ( );//ʬ��

                        //s[51] = b.DeadKind;//��������

                        //s[52] = b.BodyAnotomize;//ʬ����ʺ�

                        //b.Hbsag = this.reader[96].ToString();//�Ҹα��濹ԭ

                        //b.HcvAb = this.reader[97].ToString();//���β�������

                        //b.HivAb = this.reader[98].ToString();//�������������ȱ�ݲ�������

                        b.CePi = this.reader [67].ToString ( );//�ż�_��Ժ����

                        b.PiPo = this.reader [68].ToString ( );//���_Ժ����

                        b.OpbOpa = this.reader [69].ToString ( );//��ǰ_�����

                        //s[59] = b.ClX;//�ٴ�_X�����

                        //s[60] = b.ClCt;//�ٴ�_CT����

                        //s[61] = b.ClMri;//�ٴ�_MRI����

                        b.ClPa = this.reader [70].ToString ( );//�ٴ�_�������

                        b.FsBl = this.reader [71].ToString ( );//����_�������

                        b.SalvTimes = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [40].ToString ( ) );//���ȴ���

                        b.SuccTimes = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [41].ToString ( ) );//�ɹ�����

                        b.TechSerc = this.reader [60].ToString ( );//ʾ�̿���

                        b.VisiStat = this.reader [42].ToString ( );//�Ƿ�����

                        b.VisiPeriodWeek = this.reader [43].ToString ( );//�������

                        //b.InconNum = FS.FrameWork.Function.NConvert.ToInt32(this.reader[112].ToString());//Ժ�ʻ������ 70 Զ�̻������

                        //b.OutconNum = FS.FrameWork.Function.NConvert.ToInt32(this.reader[113].ToString());//Ժ�ʻ������ 70 Զ�̻������

                        //s[71] = b.AnaphyFlag;//ҩ�����

                        b.FirstAnaphyPharmacy.Name = this.reader [38].ToString ( );//����ҩ������

                        //s[73] = b.SecondAnaphyPharmacy.ID;//����ҩ������

                        //s[74] = b.CoutDate.ToString();//���ĺ��Ժ����

                        //s[75] = b.PatientInfo.PVisit.AdmittingDoctor.ID;//סԺҽʦ����

                        b.PatientInfo.PVisit.AdmittingDoctor.Name = this.reader [64].ToString ( );//סԺҽʦ����

                        //s[77] = b.PatientInfo.PVisit.AttendingDoctor.ID;//����ҽʦ����

                        b.PatientInfo.PVisit.AttendingDoctor.Name = this.reader [63].ToString ( );//����ҽʦ����

                        //s[79] = b.PatientInfo.PVisit.ConsultingDoctor.ID;//����ҽʦ����

                        b.PatientInfo.PVisit.ConsultingDoctor.Name = this.reader [62].ToString ( );//����ҽʦ����

                        //s[81] = b.PatientInfo.PVisit.ReferringDoctor.ID;//�����δ���

                        //b.PatientInfo.PVisit.ReferringDoctor.Name = this.reader[99].ToString();//����������

                        //s[83] = b.RefresherDoc.ID;//����ҽʦ����

                        //b.RefresherDoc.Name = this.reader[100].ToString();//����ҽ������

                        //s[85] = b.GraduateDoc.ID;//�о���ʵϰҽʦ����

                        //b.GraduateDoc.Name = this.reader[101].ToString();//�о���ʵϰҽʦ����

                        //s[87] = b.PatientInfo.PVisit.TempDoctor.ID;//ʵϰҽʦ����

                        b.PatientInfo.PVisit.TempDoctor.Name = this.reader [65].ToString ( );//ʵϰҽʦ����

                        //s[89] = b.CodingOper.ID;//����Ա����

                        b.CodingOper.Name = this.reader [66].ToString ( );//����Ա����
                        //b.OperationCoding.Name = b.codingCode.Name;

                        b.MrQuality = this.reader [61].ToString ( );//��������

                        //s[92] = b.MrEligible;//�ϸ񲡰�

                        //s[93] = b.QcDoc.ID;//�ʿ�ҽʦ����

                        //b.QcDoc.Name = this.reader[102].ToString();//�ʿ�ҽʦ����

                        //s[95] = b.QcNurse.ID;//�ʿػ�ʿ����

                        //b.QcNurse.Name = this.reader[103].ToString();//�ʿػ�ʿ����

                        //b.CheckDate = this.GetDateTime(this.reader[104].ToString());//���ʱ��

                        //b.YnFirst = this.reader[106].ToString();//�����������Ƽ�����Ϊ��Ժ��һ����Ŀ

                        // b.RhBlood = this.reader[105].ToString();//RhѪ��(����)

                        b.ReactionBlood = this.reader [81].ToString ( );//��Ѫ��Ӧ�����ޣ�

                        //b.BloodRed = this.reader[107].ToString();//��ϸ����

                        //b.BloodPlatelet = this.reader[108].ToString();//ѪС����

                        //b.BodyAnotomize = this.reader[109].ToString();//Ѫ����

                        //b.BloodWhole = this.reader[110].ToString();//ȫѪ��

                        //b.BloodOther = this.reader[111].ToString();//������Ѫ��

                        //s[106] = b.XNum;//X���

                        //s[107] = b.CtNum;//CT��

                        //s[108] = b.MriNum;//MRI��

                        //s[109] = b.PathNum;//�����

                        //s[110] = b.DsaNum;//DSA��

                        //s[111] = b.PetNum;//PET��

                        //s[112] = b.EctNum;//ECT��

                        //s[113] = b.XQty.ToString();//X�ߴ���

                        //s[114] = b.CTQty.ToString();//CT����

                        //s[115] = b.MRQty.ToString();//MR����

                        //s[116] = b.DSAQty.ToString();//DSA����

                        //s[117] = b.PetQty.ToString();//PET����

                        //s[118] = b.EctQty.ToString();//ECT����

                        //s[119] = b.PatientInfo.Memo;//˵��

                        //s[120] = b.BarCode;//�鵵�����

                        //s[121] = b.LendStat;//��������״̬(O��� I�ڼ�)

                        b.PatientInfo.CaseState = "3";//����״̬1�����ʼ�2�ǼǱ���3����4�������ʼ�5��Ч

                        //s[123] = b.OperInfo.ID;//����Ա

                        b.OperInfo.OperTime = this.GetDateTime ( this.reader [83].ToString ( ) );//����ʱ��
                        //s[124] = b.VisiPeriodWeek; //������� ��
                        //s[125] = b.VisiPeriodMonth; //������� ��
                        //s[126] = b.VisiPeriodYear;//������� ��
                        //try
                        //{
                        //    b.SpecalNus = FS.FrameWork.Function.NConvert.ToInt32(this.reader[114].ToString());  // ���⻤��(��)                                        
                        //}
                        //catch
                        //{
                        //}
                        //try
                        //{
                        //    b.INus = FS.FrameWork.Function.NConvert.ToInt32(this.reader[115].ToString()); //I������ʱ��(��)  
                        //}
                        //catch
                        //{
                        //}
                        //try
                        //{
                        //    b.IINus = FS.FrameWork.Function.NConvert.ToInt32(this.reader[116].ToString()); //II������ʱ��(��)  
                        //}
                        //catch
                        //{
                        //}
                        //try
                        //{
                        //    b.IIINus = FS.FrameWork.Function.NConvert.ToInt32(this.reader[117].ToString()); //III������ʱ��(��)   
                        //}
                        //catch
                        //{
                        //}
                        //try
                        //{
                        //    b.StrictNuss = FS.FrameWork.Function.NConvert.ToInt32(this.reader[118].ToString()); //��֢�໤ʱ��( Сʱ) 
                        //}
                        //catch
                        //{
                        //}
                        //try
                        //{
                        //    b.SuperNus = FS.FrameWork.Function.NConvert.ToInt32(this.reader[119].ToString()); //�ؼ�����ʱ��(Сʱ) 
                        //}
                        //catch
                        //{
                        //}
                        //s[133] = b.PackupMan.ID; //����Ա
                        //s[134] = b.Disease30; //������ 
                        //s[135] = b.IsHandCraft;//�ֹ�¼�벡�� ��־
                        //s[136] = b.SyndromeFlag; //�Ƿ��в���֢
                        //s[137] = b.InfectionNum.ToString();//Ժ�ڸ�Ⱦ���� 
                        //s[138] = b.OperationCoding.ID;//��������Ա 
                        b.CaseNO = b.PatientInfo.PID.CardNO.PadLeft ( 10, '0' );//������
                        //s[140] = b.InfectionPosition.ID; //Ժ�ڸ�Ⱦ��λ����
                        //s[141] = b.InfectionPosition.Name; //Ժ�ڸ�Ⱦ��λ����*/

                        b.SyndromeFlag = this.reader [35].ToString ( );//�Ƿ��в���֢
                        b.InfectionNum = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [36].ToString ( ) ); //Ժ�ڸ�Ⱦ����
                        b.Disease30 = this.reader [85].ToString ( );
                        b.LendStat = this.reader [73].ToString ( );//���ֲ���

                        this.GetpatientFromba1 ( b, cardno );
                        this.GetpatientFromba6 ( b, cardno, times );

                        return b;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    this.Err1 = "û������";

                    return null;
                }
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }


            /*
             * s[0] = b.PatientInfo.ID;//סԺ��ˮ��

                s[1] = b.PatientInfo.PID.PatientNO;//סԺ������

                s[2] = b.PatientInfo.PID.CardNO;//����

                s[3] = b.PatientInfo.Name;//����

                s[4] = b.Nomen;//������

                s[5] = b.PatientInfo.Sex.ID.ToString();//�Ա�

                s[6] = b.PatientInfo.Birthday.ToString();//��������

                s[7] = b.PatientInfo.Country.ID;//����

                s[8] = b.PatientInfo.Nationality.ID;//����

                s[9] = b.PatientInfo.Profession.ID;//ְҵ

                s[10] = b.PatientInfo.BloodType.ID.ToString();//Ѫ�ͱ���

                s[11] = b.PatientInfo.MaritalStatus.ID.ToString();//���

                s[12] = b.PatientInfo.Age.ToString();//����

                s[13] = b.AgeUnit;//���䵥λ

                s[14] = b.PatientInfo.IDCard;//���֤��

                s[15] = b.PatientInfo.PVisit.InSource.ID;//������Դ

                s[16] = b.PatientInfo.Pact.ID;//��������

                s[17] = b.PatientInfo.Pact.ID;//��ͬ����

                s[18] = b.PatientInfo.SSN;//ҽ�����Ѻ�

                s[19] = b.PatientInfo.DIST;//����

                s[20] = b.PatientInfo.AreaCode;//������

                s[21] = b.PatientInfo.AddressHome;//��ͥסַ

                s[22] = b.PatientInfo.PhoneHome;//��ͥ�绰

                s[23] = b.PatientInfo.HomeZip;//סַ�ʱ�

                s[24] = b.PatientInfo.AddressBusiness;//��λ��ַ

                s[25] = b.PatientInfo.PhoneBusiness;//��λ�绰

                s[26] = b.PatientInfo.BusinessZip;//��λ�ʱ�

                s[27] = b.PatientInfo.Kin.Name;//��ϵ��

                s[28] = b.PatientInfo.Kin.RelationLink;//�뻼�߹�ϵ

                s[29] = b.PatientInfo.Kin.RelationPhone;//��ϵ�绰

                s[30] = b.PatientInfo.Kin.RelationAddress;//��ϵ��ַ

                s[31] = b.ClinicDoc.ID;//�������ҽ��

                s[32] = b.ClinicDoc.Name;//�������ҽ������

                s[33] = b.ComeFrom;//ת��ҽԺ

                s[34] = b.PatientInfo.PVisit.InTime.ToString();//��Ժ����

                s[35] = b.PatientInfo.InTimes.ToString();//סԺ����

                s[36] = b.InDept.ID;//��Ժ���Ҵ���

                s[37] = b.InDept.Name;//��Ժ��������

                s[38] = b.PatientInfo.PVisit.InSource.ID;//��Ժ��Դ

                s[39] = b.PatientInfo.PVisit.Circs.ID;//��Ժ״̬

                s[40] = b.DiagDate.ToString();//ȷ������

                s[41] = b.OperationDate.ToString();//��������

                s[42] = b.PatientInfo.PVisit.OutTime.ToString();//��Ժ����

                s[43] = b.OutDept.ID;//��Ժ���Ҵ���

                s[44] = b.OutDept.Name;//��Ժ��������

                s[45] = b.PatientInfo.PVisit.ZG.ID;//ת�����

                s[46] = b.DiagDays.ToString();//ȷ������

                s[47] = b.InHospitalDays.ToString();//סԺ����

                s[48] = b.DeadDate.ToString();//��������

                s[49] = b.DeadReason;//����ԭ��

                s[50] = b.CadaverCheck;//ʬ��

                s[51] = b.DeadKind;//��������

                s[52] = b.BodyAnotomize;//ʬ����ʺ�

                s[53] = b.Hbsag;//�Ҹα��濹ԭ

                s[54] = b.HcvAb;//���β�������

                s[55] = b.HivAb;//�������������ȱ�ݲ�������

                s[56] = b.CePi;//�ż�_��Ժ����

                s[57] = b.PiPo;//���_Ժ����

                s[58] = b.OpbOpa;//��ǰ_�����

                s[59] = b.ClX;//�ٴ�_X�����

                s[60] = b.ClCt;//�ٴ�_CT����

                s[61] = b.ClMri;//�ٴ�_MRI����

                s[62] = b.ClPa;//�ٴ�_�������

                s[63] = b.FsBl;//����_�������

                s[64] = b.SalvTimes.ToString();//���ȴ���

                s[65] = b.SuccTimes.ToString();//�ɹ�����

                s[66] = b.TechSerc;//ʾ�̿���

                s[67] = b.VisiStat;//�Ƿ�����

                s[68] = b.VisiPeriod.ToString();//�������

                s[69] = b.InconNum.ToString();//Ժ�ʻ������ 70 Զ�̻������

                s[70] = b.OutconNum.ToString();//Ժ�ʻ������ 70 Զ�̻������

                s[71] = b.AnaphyFlag;//ҩ�����

                s[72] = b.FirstAnaphyPharmacy.Name;//����ҩ������

                s[73] = b.SecondAnaphyPharmacy.ID;//����ҩ������

                s[74] = b.CoutDate.ToString();//���ĺ��Ժ����

                s[75] = b.PatientInfo.PVisit.AdmittingDoctor.ID;//סԺҽʦ����

                s[76] = b.PatientInfo.PVisit.AdmittingDoctor.Name;//סԺҽʦ����

                s[77] = b.PatientInfo.PVisit.AttendingDoctor.ID;//����ҽʦ����

                s[78] = b.PatientInfo.PVisit.AttendingDoctor.Name;//����ҽʦ����

                s[79] = b.PatientInfo.PVisit.ConsultingDoctor.ID;//����ҽʦ����

                s[80] = b.PatientInfo.PVisit.ConsultingDoctor.Name;//����ҽʦ����

                s[81] = b.PatientInfo.PVisit.ReferringDoctor.ID;//�����δ���

                s[82] = b.PatientInfo.PVisit.ReferringDoctor.Name;//����������

                s[83] = b.RefresherDoc.ID;//����ҽʦ����

                s[84] = b.RefresherDoc.Name;//����ҽ������

                s[85] = b.GraduateDoc.ID;//�о���ʵϰҽʦ����

                s[86] = b.GraduateDoc.Name;//�о���ʵϰҽʦ����

                s[87] = b.PatientInfo.PVisit.TempDoctor.ID;//ʵϰҽʦ����

                s[88] = b.PatientInfo.PVisit.TempDoctor.Name;//ʵϰҽʦ����

                s[89] = b.CodingOper.ID;//����Ա����

                s[90] = b.CodingOper.Name;//����Ա����

                s[91] = b.MrQuality;//��������

                s[92] = b.MrEligible;//�ϸ񲡰�

                s[93] = b.QcDoc.ID;//�ʿ�ҽʦ����

                s[94] = b.QcDoc.Name;//�ʿ�ҽʦ����

                s[95] = b.QcNurse.ID;//�ʿػ�ʿ����

                s[96] = b.QcNurse.Name;//�ʿػ�ʿ����

                s[97] = b.CheckDate.ToString();//���ʱ��

                s[98] = b.YnFirst;//�����������Ƽ�����Ϊ��Ժ��һ����Ŀ

                s[99] = b.RhBlood;//RhѪ��(����)

                s[100] = b.ReactionBlood;//��Ѫ��Ӧ�����ޣ�

                s[101] = b.BloodRed;//��ϸ����

                s[102] = b.BloodPlatelet;//ѪС����

                s[103] = b.BodyAnotomize;//Ѫ����

                s[104] = b.BloodWhole;//ȫѪ��

                s[105] = b.BloodOther;//������Ѫ��

                s[106] = b.XNum;//X���

                s[107] = b.CtNum;//CT��

                s[108] = b.MriNum;//MRI��

                s[109] = b.PathNum;//�����

                s[110] = b.DsaNum;//DSA��

                s[111] = b.PetNum;//PET��

                s[112] = b.EctNum;//ECT��

                s[113] = b.XQty.ToString();//X�ߴ���

                s[114] = b.CTQty.ToString();//CT����

                s[115] = b.MRQty.ToString();//MR����

                s[116] = b.DSAQty.ToString();//DSA����

                s[117] = b.PetQty.ToString();//PET����

                s[118] = b.EctQty.ToString();//ECT����

                s[119] = b.PatientInfo.Memo;//˵��

                s[120] = b.BarCode;//�鵵�����

                s[121] = b.LendStat;//��������״̬(O��� I�ڼ�)

                s[122] = b.PatientInfo.CaseState;//����״̬1�����ʼ�2�ǼǱ���3����4�������ʼ�5��Ч

                s[123] = b.OperInfo.ID;//����Ա

                //				s[124]=b.OperDate.ToString() ;//����ʱ��
                s[124] = b.VisiPeriodWeek; //������� ��
                s[125] = b.VisiPeriodMonth; //������� ��
                s[126] = b.VisiPeriodYear;//������� ��
                s[127] = b.SpecalNus.ToString();  // ���⻤��(��)                                        
                s[128] = b.INus.ToString(); //I������ʱ��(��)                                     
                s[129] = b.IINus.ToString(); //II������ʱ��(��)                                    
                s[130] = b.IIINus.ToString(); //III������ʱ��(��)                                   
                s[131] = b.StrictNuss.ToString(); //��֢�໤ʱ��( Сʱ)                                 
                s[132] = b.SuperNus.ToString(); //�ؼ�����ʱ��(Сʱ)     
                s[133] = b.PackupMan.ID; //����Ա
                s[134] = b.Disease30; //������ 
                s[135] = b.IsHandCraft;//�ֹ�¼�벡�� ��־
                s[136] = b.SyndromeFlag; //�Ƿ��в���֢
                s[137] = b.InfectionNum.ToString();//Ժ�ڸ�Ⱦ���� 
                s[138] = b.OperationCoding.ID;//��������Ա 
                s[139] = b.CaseNO;//������
                s[140] = b.InfectionPosition.ID; //Ժ�ڸ�Ⱦ��λ����
                s[141] = b.InfectionPosition.Name; //Ժ�ڸ�Ⱦ��λ����
             */

        }

        public int GetpatientFromba6 ( FS.HISFC.Models.HealthRecord.Base b,
            string cardno, string times )
        {
            #region SQL

            string strSQL = @"select hbsag,   
	hcv_ab,
	hiv_ab,
	kzr,  
   	jxdoct,
        ysxdoct,
        zkdoct,
        zknurse,
        zkrq, 
        rh,
        ISOPFIRST,
        redcell,
        plaque,
        serous,
        ALLBLOOD,
        OTHERBLOOD,
        HZ_YJ,  
        HZ_YC,
        HL_TJ,
        HL_I,
        HL_II,
        HL_III,
        HL_ZZ,
        HL_TS
from ba6
where prn = '{0}' and times = '{1}'
";
            #endregion

            this.cmd.CommandText = string.Empty;

            try
            {
                strSQL = string.Format ( strSQL, cardno, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;

                return -1;
            }
            if ( !this.reader.IsClosed )
            {
                this.reader.Close ( );
            }
            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                if ( this.reader.Read ( ) )
                {
                    try
                    {


                        b.Hbsag = this.reader [0].ToString ( );//�Ҹα��濹ԭ

                        b.HcvAb = this.reader [1].ToString ( );//���β�������

                        b.HivAb = this.reader [2].ToString ( );//�������������ȱ�ݲ�������

                        b.InconNum = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [16].ToString ( ) );//Ժ�ʻ������ 70 Զ�̻������

                        b.OutconNum = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [17].ToString ( ) );//Ժ�ʻ������ 70 Զ�̻������

                        b.PatientInfo.PVisit.ReferringDoctor.Name = this.reader [3].ToString ( );//����������

                        //s[83] = b.RefresherDoc.ID;//����ҽʦ����

                        b.RefresherDoc.Name = this.reader [4].ToString ( );//����ҽ������

                        //s[85] = b.GraduateDoc.ID;//�о���ʵϰҽʦ����

                        b.GraduateDoc.Name = this.reader [5].ToString ( );//�о���ʵϰҽʦ����

                        b.QcDoc.Name = this.reader [6].ToString ( );//�ʿ�ҽʦ����

                        //s[95] = b.QcNurse.ID;//�ʿػ�ʿ����

                        b.QcNurse.Name = this.reader [7].ToString ( );//�ʿػ�ʿ����

                        b.CheckDate = this.GetDateTime ( this.reader [8].ToString ( ) );//���ʱ��

                        b.YnFirst = this.reader [10].ToString ( );//�����������Ƽ�����Ϊ��Ժ��һ����Ŀ

                        b.RhBlood = this.reader [9].ToString ( );//RhѪ��(����)

                        b.BloodRed = this.reader [11].ToString ( );//��ϸ����

                        b.BloodPlatelet = this.reader [12].ToString ( );//ѪС����

                        b.BodyAnotomize = this.reader [13].ToString ( );//Ѫ����

                        b.BloodWhole = this.reader [14].ToString ( );//ȫѪ��

                        b.BloodOther = this.reader [15].ToString ( );//������Ѫ��

                        try
                        {
                            b.SpecalNus = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [23].ToString ( ) );  // ���⻤��(��)                                        
                        }
                        catch
                        {
                        }
                        try
                        {
                            b.INus = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [19].ToString ( ) ); //I������ʱ��(��)  
                        }
                        catch
                        {
                        }
                        try
                        {
                            b.IINus = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [20].ToString ( ) ); //II������ʱ��(��)  
                        }
                        catch
                        {
                        }
                        try
                        {
                            b.IIINus = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [21].ToString ( ) ); //III������ʱ��(��)   
                        }
                        catch
                        {
                        }
                        try
                        {
                            b.StrictNuss = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [22].ToString ( ) ); //��֢�໤ʱ��( Сʱ) 
                        }
                        catch
                        {
                        }
                        try
                        {
                            b.SuperNus = FS.FrameWork.Function.NConvert.ToInt32 ( this.reader [18].ToString ( ) ); //�ؼ�����ʱ��(Сʱ) 
                        }
                        catch
                        {
                        }

                        //return b;
                    }
                    catch ( Exception ex )
                    {
                        this.Err1 = ex.Message;
                        return -1;
                    }
                }

                //return b;
                return 1;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return -1;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        public int GetpatientFromba1 ( FS.HISFC.Models.HealthRecord.Base b,
            string cardno )
        {
            #region SQL

            string strSQL = @"select birthday,
        native,
        nation,
        sex,
        idcard,
        birthpl
from ba1 where prn = '{0}'
";
            #endregion

            this.cmd.CommandText = string.Empty;

            try
            {
                strSQL = string.Format ( strSQL, cardno );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;

                return -1;
            }

            if ( !this.reader.IsClosed )
            {
                this.reader.Close ( );
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                if ( this.reader.Read ( ) )
                {
                    try
                    {
                        b.PatientInfo.Birthday = this.GetDateTime ( this.reader [0].ToString ( ) );//��������

                        //
                        b.PatientInfo.Country.Name = this.reader [1].ToString ( ).Trim ( );//����

                        //
                        b.PatientInfo.Nationality.Name = this.reader [2].ToString ( ).Trim ( );//����


                        string sex = this.reader [3].ToString ( ).Trim ( );
                        if ( sex.Trim ( ) == "1" )
                        {
                            b.PatientInfo.Sex.ID = "M";//�Ա�
                        }
                        else if ( sex.Trim ( ) == "2" )
                        {
                            b.PatientInfo.Sex.ID = "F";//�Ա�
                        }
                        else
                        {
                            b.PatientInfo.Sex.ID = "U";//�Ա�
                        }

                        b.PatientInfo.IDCard = this.reader [4].ToString ( );//���֤��
                        b.PatientInfo.DIST = this.reader [5].ToString ( );//����

                        b.PatientInfo.AreaCode = b.PatientInfo.DIST;//������
                        //return b;


                    }
                    catch ( Exception ex )
                    {
                        this.Err1 = ex.Message;
                        return -1;
                    }
                }
                return 1;
                //return b;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return -1;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// ba3
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public ArrayList GetICD ( string cardNo, string times )
        {
            ArrayList al = new ArrayList ( );
            string strSQL = @"SELECT PRN, NAME, TIMES, ZDXH, ICDM, ZLJG, ICDM10 FROM ba3
 where prn = '{0}'
   and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose obj;

                while ( this.reader.Read ( ) )
                {
                    obj = new FS.HISFC.Models.HealthRecord.Diagnose ( );
                    /*��������*/
                    obj.DiagInfo.DiagType.ID = this.reader [3].ToString ( );//����

                    obj.DiagInfo.ICD10.ID = this.reader [6].ToString ( );
                    obj.DiagOutState = this.reader [5].ToString ( );//""/*��Ч*/,
                    obj.OperType = "2";
                    al.Add ( obj );
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// ba3
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public ArrayList GetICDf ( string cardNo, string times )
        {
            ArrayList al = new ArrayList ( );
            string strSQL = @"SELECT ZDXH, ICDM10, ICDM10_F, ZLJG
  FROM  ba3_f
 where prn = '{0}'
   and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose obj;

                while ( this.reader.Read ( ) )
                {
                    obj = new FS.HISFC.Models.HealthRecord.Diagnose ( );

                    //obj.DiagInfo.DiagType.ID = this.reader[0].ToString();//����

                    obj.DiagInfo.ICD10.ID = this.reader [1].ToString ( );
                    obj.SecondICD = this.reader [2].ToString ( );
                    //obj.DiagOutState = this.reader[3].ToString();//""/*��Ч*/,
                    //obj.OperType = "2";
                    al.Add ( obj );
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        public ArrayList GetICDFromba6 ( string cardNo, string times )
        {
            ArrayList al = new ArrayList ( );
            string strSQL = @"SELECT MZZD10,
       RYZD10
  FROM  ba6 
  where prn = '{0}' and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose obj;

                while ( this.reader.Read ( ) )
                {
                    string ryzd = this.reader [0].ToString ( ).Trim ( );
                    if ( ryzd != "" )
                    {
                        obj = new FS.HISFC.Models.HealthRecord.Diagnose ( );
                        obj.DiagInfo.DiagType.ID = "10";
                        obj.DiagInfo.ICD10.ID = ryzd;
                        obj.OperType = "2";
                        al.Add ( obj );
                    }

                    string mzzd = this.reader [1].ToString ( ).Trim ( );
                    if ( mzzd != "" )
                    {
                        obj = new FS.HISFC.Models.HealthRecord.Diagnose ( );
                        obj.DiagInfo.DiagType.ID = "11";
                        obj.DiagInfo.ICD10.ID = mzzd;
                        obj.OperType = "2";
                        al.Add ( obj );
                    }
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        public ArrayList GetICDFromba2 ( string cardNo, string times )
        {
            ArrayList al = new ArrayList ( );
            string strSQL = @"select phzd , (select top 1 icd10 from icd10 where jbname = phzd) as a  from ba2 
  where prn = '{0}' and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose obj;

                if ( this.reader.Read ( ) )
                {
                    string blzdID = this.reader [1].ToString ( ).Trim ( );
                    string blzdName = this.reader [0].ToString ( ).Trim ( );
                    if ( blzdName != "" && blzdName != "��" )
                    {
                        obj = new FS.HISFC.Models.HealthRecord.Diagnose ( );
                        obj.DiagInfo.DiagType.ID = "6";
                        obj.DiagInfo.ICD10.ID = blzdID;
                        if ( obj.DiagInfo.ICD10.ID.Trim ( ) == "" )
                        {
                            obj.DiagInfo.ICD10.ID = "BA";
                        }
                        obj.DiagInfo.ICD10.Name = blzdName;
                        obj.OperType = "2";
                        al.Add ( obj );
                    }
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// ba4
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public ArrayList GetOper ( string cardNo, string times )
        {
            ArrayList al = new ArrayList ( );
            string strSQL = @"SELECT PRN,
       TIMES,
       NAME,
       OPTIMES,
       OPDATE,
       OPCODE,
       QIEKOU,
       YUHE,
       DOCNAME,
       MAZUI,
       IFFSOP,
       OPDOCTI,
       OPDOCTII,
       MZDOCT
  FROM  ba4 
  where prn = '{0}' and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.HealthRecord.OperationDetail obj;

                while ( this.reader.Read ( ) )
                {
                    obj = new FS.HISFC.Models.HealthRecord.OperationDetail ( );
                    /*��������*/
                    obj.OperationDate = GetDateTime ( this.reader [4].ToString ( ) );
                    obj.OperationInfo.ID = this.reader [5].ToString ( );
                    obj.NickKind/*�п�*/= this.reader [6].ToString ( );
                    obj.CicaKind/*����*/= this.reader [7].ToString ( );
                    obj.FirDoctInfo.Name = this.reader [8].ToString ( ).Trim ( );
                    obj.MarcKind = this.reader [9].ToString ( );//����ʽ
                    /*�Ƿ񸽼�����*/
                    string temp = this.reader [10].ToString ( );
                    if ( temp.Trim ( ) == "y" || temp.Trim ( ) == "Y" )
                    {
                        obj.StatFlag = "1";
                    }
                    else
                    {
                        obj.StatFlag = "0";
                    }

                    obj.FourDoctInfo.Name = this.reader [11].ToString ( ).Trim ( );
                    obj.SecDoctInfo.Name = this.reader [12].ToString ( ).Trim ( );
                    //obj.ThrDoctInfo.Name,
                    obj.NarcDoctInfo.Name = this.reader [13].ToString ( ).Trim ( );
                    al.Add ( obj );
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// ba7
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public ArrayList GetChangeDept ( string cardNo, string times )
        {
            ArrayList al = new ArrayList ( );
            string strSQL = @"SELECT  ZKNUM, ZKDEPT,  ZKDATE, ZKTIME
  FROM  ba7
 where prn = '{0}'
   and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.RADT.Location obj;

                while ( this.reader.Read ( ) )
                {
                    obj = new FS.HISFC.Models.RADT.Location ( );

                    obj.Dept.ID = this.reader [0].ToString ( );//����
                    obj.Dept.Name = this.reader [1].ToString ( );//""/*��Ч*/,
                    obj.User01 = this.GetDateTime ( this.reader [2].ToString ( ) ).ToString ( );

                    al.Add ( obj );
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// ��һ��ת��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.Location GetFristDept ( string cardNo, string times )
        {
            string strSQL = @"select ZKNUM,
       ZKDEPT,
       (select top 1 ZKDATE
          from ba6
         where prn = '{0}'
           and times = '{1}') as zkdate
  from ba2
 where prn = '{0}'
   and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.RADT.Location obj;

                if ( this.reader.Read ( ) )
                {
                    string deptID = this.reader [0].ToString ( ).Trim ( );

                    if ( deptID != "" )
                    {
                        obj = new FS.HISFC.Models.RADT.Location ( );

                        obj.Dept.ID = deptID;//����
                        obj.Dept.Name = this.reader [1].ToString ( );//""/*��Ч*/,
                        obj.User01 = this.GetDateTime ( this.reader [2].ToString ( ) ).ToString ( );

                        return obj;
                    }
                }
                else
                {
                }

                return null;

            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// ba9
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Tumour GetTumout
            ( string cardNo, string times )
        {
            string strSQL = @"SELECT prn,
       name,
       times,
       flfs,
       flcs,
       flzz,
       ygy,
       ycs,
       yts,
       yrq1,
       yrq2,
       qgy,
       qcs,
       qts,
       qrq1,
       qrq2,
       zgy,
       zcs,
       zts,
       zrq1,
       zrq2,
       hlfs,
       hlff
  FROM ba9
  where prn = '{0}' and times = '{1}'
";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.HealthRecord.Tumour info = new FS.HISFC.Models.HealthRecord.Tumour ( );

                if ( this.reader.Read ( ) )
                {
                    info.Rmodeid = this.reader [3].ToString ( );
                    info.Rprocessid = this.reader [4].ToString ( );
                    info.Rdeviceid = this.reader [5].ToString ( );
                    try
                    {
                        info.Gy1 = Convert.ToDecimal ( this.reader [6].ToString ( ) );
                    }
                    catch
                    {
                    }
                    try
                    {
                        info.Time1 = Convert.ToDecimal ( this.reader [7].ToString ( ) );
                    }
                    catch
                    {
                    }
                    try
                    {
                        info.Day1 = Convert.ToDecimal ( this.reader [8].ToString ( ) );
                    }
                    catch
                    {
                    }
                    info.BeginDate1 = GetDateTime ( this.reader [9].ToString ( ) );
                    info.EndDate1 = GetDateTime ( this.reader [10].ToString ( ) );
                    try
                    {
                        info.Gy2 = Convert.ToDecimal ( this.reader [11].ToString ( ) );
                    }
                    catch
                    {
                    }
                    try
                    {
                        info.Time2 = Convert.ToDecimal ( this.reader [12].ToString ( ) );
                    }
                    catch
                    {
                    }
                    try
                    {
                        info.Day2 = Convert.ToDecimal ( this.reader [13].ToString ( ) );
                    }
                    catch
                    {
                    }
                    info.BeginDate2 = GetDateTime ( this.reader [14].ToString ( ) );
                    info.EndDate2 = GetDateTime ( this.reader [15].ToString ( ) );
                    try
                    {
                        info.Gy3 = Convert.ToDecimal ( this.reader [16].ToString ( ) );
                    }
                    catch
                    {
                    }
                    try
                    {
                        info.Time3 = Convert.ToDecimal ( this.reader [17].ToString ( ) );
                    }
                    catch
                    {
                    }
                    try
                    {
                        info.Day3 = Convert.ToDecimal ( this.reader [18].ToString ( ) );
                    }
                    catch
                    {
                    }
                    info.BeginDate3 = GetDateTime ( this.reader [19].ToString ( ) );
                    info.EndDate3 = GetDateTime ( this.reader [20].ToString ( ) );
                    info.Cmodeid = this.reader [21].ToString ( );
                    info.Cmethod = this.reader [22].ToString ( );
                }
                else
                {
                    info.User01 = "0";
                }

                return info;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// ba10
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public ArrayList GetTumoutDetail ( string cardNo, string times )
        {
            string strSQL = @"SELECT prn, name, times, hldate1, hldate2, hldrug, hlproc, hljg
  FROM ba10 where prn = '{0}' and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            ArrayList al = new ArrayList ( );

            try
            {
                FS.HISFC.Models.HealthRecord.TumourDetail info = new FS.HISFC.Models.HealthRecord.TumourDetail ( );
                while ( this.reader.Read ( ) )
                {
                    info = new FS.HISFC.Models.HealthRecord.TumourDetail ( );
                    try
                    {
                        info.CureDate = FS.FrameWork.Function.NConvert.ToDateTime ( this.reader [3].ToString ( ).Replace ( '/', '-' ) + " 00:00:00" );
                    }
                    catch
                    {
                    }
                    try
                    {
                        info.OperInfo.OperTime = GetDateTime ( this.reader [4].ToString ( ) );
                    }
                    catch
                    {
                    }
                    info.DrugInfo.Name = this.reader [5].ToString ( );
                    info.Period = this.reader [6].ToString ( ).Trim ( );
                    info.Result = this.reader [7].ToString ( ).Trim ( );
                    al.Add ( info );
                }

                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// fees
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public ArrayList GetFeesFromba2 ( string cardNo, string times, ref decimal otherfee )
        {
            ArrayList al = new ArrayList ( );
            string strSQL = @"SELECT SUM1,
       CWF,
       XYF,
       ZYF,
       ZCYF,
       ZCHF,
       JCF,
       ZLF,
       FSF,
       SSF,
       HYF,
       SXF,
       SYF,
       JSF,
       QTF
  FROM ba2 where prn = '{0}' and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.RADT.Patient info;

                if ( this.reader.Read ( ) )
                {
                    info = new FS.HISFC.Models.RADT.Patient ( );

                    decimal cost = 0;

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [1].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "1";
                        info.AreaCode = "��λ";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [2].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "3";
                        info.AreaCode = "��ҩ";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [4].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "5";
                        info.AreaCode = "�в�ҩ";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [5].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "4";
                        info.AreaCode = "�г�ҩ";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [6].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "13";
                        info.AreaCode = "���";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }


                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [7].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "10";
                        info.AreaCode = "����";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [8].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "6";
                        info.AreaCode = "����";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [9].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "11";
                        info.AreaCode = "����";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [10].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "7";
                        info.AreaCode = "����";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }


                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [11].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "2";
                        info.AreaCode = "�����";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [12].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "8";
                        info.AreaCode = "����";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [14].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        //info.DIST = "17";
                        //info.AreaCode = "����";
                        //info.IDCard = cost.ToString();

                        //al.Add(info);
                        otherfee = cost;
                    }


                    //info.DIST = row["ͳ�Ʊ���"].ToString();//ͳ�ƴ������
                    //if (info.DIST == "" || info.DIST == null)
                    //{
                    //    continue;
                    //}
                    //info.AreaCode = row["��������"].ToString(); //ͳ������ 
                    //if (row["���ý��"] != DBNull.Value)
                    //{
                    //    info.IDCard = row["���ý��"].ToString();//ͳ�Ʒ��� 
                    //}
                    //feeInfoList.Add(info);

                    //al.Add(info);
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// fees
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public ArrayList GetFeesFromba6 ( string cardNo, string times, ref decimal otherfee )
        {
            ArrayList al = new ArrayList ( );
            string strSQL = @"select HLF, MZF, YEF, PCF
  from ba6
 where prn = '{0}'
   and times = '{1}'";

            try
            {
                strSQL = string.Format ( strSQL, cardNo, times );
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL ( strSQL );

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.HISFC.Models.RADT.Patient info;

                if ( this.reader.Read ( ) )
                {
                    info = new FS.HISFC.Models.RADT.Patient ( );

                    decimal cost = 0;

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [0].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "9";
                        info.AreaCode = "��Ѫ";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [1].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        info = new FS.HISFC.Models.RADT.Patient ( );
                        info.DIST = "14";
                        info.AreaCode = "����";
                        info.IDCard = cost.ToString ( );

                        al.Add ( info );
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [2].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        //info.DIST = "";
                        //info.AreaCode = "";
                        //info.IDCard = cost.ToString();

                        //al.Add(info);

                        otherfee += cost;
                    }

                    try
                    {
                        cost = FS.FrameWork.Function.NConvert.ToDecimal ( this.reader [3].ToString ( ) );
                    }
                    catch
                    {
                        cost = 0;
                    }
                    if ( cost != 0 )
                    {
                        //info.DIST = "";
                        //info.AreaCode = "";
                        //info.IDCard = cost.ToString();

                        //al.Add(info);
                        otherfee += cost;
                    }


                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// ȡ��Ҫ��������
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllCardAndTimes ( )
        {
            ArrayList al = new ArrayList ( );
            string strSQL = @"select prn,times from ba2 where prn = '0000001'";


            this.ReadSQL ( strSQL );
            //this.cmd.CommandText = strSQL;

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.FrameWork.Models.NeuObject obj;

                while ( this.reader.Read ( ) )
                {
                    obj = new FS.FrameWork.Models.NeuObject ( );
                    obj.ID = this.reader [0].ToString ( );
                    obj.Name = this.reader [1].ToString ( );
                    al.Add ( obj );
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        public ArrayList GetAllCardAndTimes ( string strSQL )
        {
            ArrayList al = new ArrayList ( );
            //string strSQL = @"select prn,times from ba2 where prn = '0000001'";


            this.ReadSQL ( strSQL );
            //this.cmd.CommandText = strSQL;

            this.reader = this.cmd.ExecuteReader ( );

            try
            {
                FS.FrameWork.Models.NeuObject obj;

                while ( this.reader.Read ( ) )
                {
                    obj = new FS.FrameWork.Models.NeuObject ( );
                    obj.ID = this.reader [0].ToString ( );
                    obj.Name = this.reader [1].ToString ( );
                    al.Add ( obj );
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close ( );
            }
        }

        /// <summary>
        /// ȡʱ��
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private DateTime GetDateTime ( string str )
        {
            if ( str.Trim ( ) == "" )
            {
                return DateTime.MinValue;
            }
            try
            {
                return FS.FrameWork.Function.NConvert.ToDateTime ( str.Replace ( '/', '-' ) + " 00:00:00" );
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        #endregion

        #region д��־

        private string fileName = "./SQLServer.log";

        private void CreatFile()
        {
            if (!System.IO.File.Exists(fileName))
            {
                System.IO.File.CreateText(fileName);
            }
        }

        private System.IO.TextWriter output;

        private void WriteLog(string log)
        {
            try
            {
                output = System.IO.File.AppendText(fileName);
                output.WriteLine(System.DateTime.Now + "\n" + log);
                output.Close();
            }
            catch
            {
                //System.w
            }
        }

        private void ReadSQL(string sql)
        {
            this.WriteLog(sql);
            this.cmd.CommandText = sql;          
        }

        #endregion

        /// <summary>
        /// ����õĺ���
        /// </summary>
        /// <param name="IcdStr">icd�ַ���</param>
        /// <returns></returns>
        public ArrayList GetPatientByICD(string IcdStr)
        {
            ArrayList al = new ArrayList();
            string strSQL = @"select distinct t.fprn from dbo.tpatientvisit  t ,dbo.tdiagnose  z
where t.fprn=z.fprn and t.ftimes=z.ftimes 
and t. fcydate>'2009-1-1 00:00:00' 
and z.ficdm in ({0})";

            try
            {
                strSQL = string.Format(strSQL, IcdStr);
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL(strSQL);

            this.reader = this.cmd.ExecuteReader();

            try
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                while (this.reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.reader[0].ToString().PadLeft(10, '0');
                    al.Add(obj);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }

        public FS.HISFC.Models.RADT.PatientInfo GetPatientByIdAndTimes(string patient_NO ,int inTimes)
        {
            //string Sql = "select t.fprn,t.ftimes,t.fname from tPatientVisit t where  t. fprn='{0}' and t.ftimes={1} ";
            string Sql = "select t.fprn,t.ftimes,t.fname,t.FSEX,t.FRYTYKH,t.FRYDEPT,t.FRYDATE,t.FCYTYKH,t.FCYDEPT,t.FCYDATE,t.FBIRTHDAY from tPatientVisit t where   t. fprn='{0}' and t.ftimes={1} ";
            try
            {
                Sql = string.Format(Sql, patient_NO,inTimes);
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }

            ReadSQL(Sql);

            this.reader = this.cmd.ExecuteReader();

            try
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                while (this.reader.Read())
                {
                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.ID = this.reader[0].ToString().PadLeft(10, '0');
                    patientInfo.PID.PatientNO = this.reader[0].ToString().PadLeft(10, '0');
                    patientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32( this.reader[1].ToString());
                    patientInfo.Name = this.reader[2].ToString();
                    patientInfo.Sex.Name = this.reader[3].ToString();
                    patientInfo.PVisit.PatientLocation.Dept.ID = this.reader[4].ToString();//��Ժ����ͳһ�� �㶫ʡ3.0ϵͳ
                    patientInfo.PVisit.PatientLocation.Dept.Name = this.reader[5].ToString();
                    patientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[6].ToString());
                    patientInfo.PVisit.PatientLocation.ID = this.reader[7].ToString();//��Ժͳһ��
                    patientInfo.PVisit.PatientLocation.Name = this.reader[8].ToString();
                    patientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[9].ToString());
                    patientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[10].ToString());
                }
                return patientInfo;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }

        public ArrayList GetPatientByName(string PatientName, int inTimes)
        {
            string Sql = "select t.fprn,t.ftimes,t.fname,t.FSEX,t.FRYTYKH,t.FRYDEPT,t.FRYDATE,t.FCYTYKH,t.FCYDEPT,t.FCYDATE,t.FBIRTHDAY,t.FLXNAME from tPatientVisit t where   t.fname='{0}' and (t.ftimes={1} or 'ALL'='{1}') ";
            try
            {
                Sql = string.Format(Sql, PatientName, inTimes);
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            ReadSQL(Sql);

            this.reader = this.cmd.ExecuteReader();

            try
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                while (this.reader.Read())
                {
                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.ID = this.reader[0].ToString().PadLeft(10, '0');
                    patientInfo.PID.PatientNO = this.reader[0].ToString().PadLeft(10, '0');
                    patientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.reader[1].ToString());
                    patientInfo.Name = this.reader[2].ToString();
                    patientInfo.Sex.Name = this.reader[3].ToString();
                    patientInfo.PVisit.PatientLocation.Dept.ID = this.reader[4].ToString();//��Ժ����ͳһ�� �㶫ʡ3.0ϵͳ
                    patientInfo.PVisit.PatientLocation.Dept.Name = this.reader[5].ToString();
                    patientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[6].ToString());
                    patientInfo.PVisit.PatientLocation.ID = this.reader[7].ToString();//��Ժͳһ��
                    patientInfo.PVisit.PatientLocation.Name = this.reader[8].ToString();
                    patientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[9].ToString());
                    patientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[10].ToString());
                    patientInfo.Kin.Relation.Name = this.reader[11].ToString();
                    al.Add(patientInfo);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }

        public ArrayList GetPatientByOutDate(DateTime dtBegin, DateTime dtEnd)
        {
            string Sql = "select t.fprn,t.ftimes,t.fname,t.FSEX,t.FRYTYKH,t.FRYDEPT,t.FRYDATE,t.FCYTYKH,t.FCYDEPT,t.FCYDATE,t.FBIRTHDAY,t.FLXNAME ,t.FWORKRQ from tpatientvisit t where t.fcydate BETWEEN '{0}' and '{1}'";
            try
            {
                Sql = string.Format(Sql, dtBegin, dtEnd);
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            ReadSQL(Sql);

            this.reader = this.cmd.ExecuteReader();

            try
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                while (this.reader.Read())
                {
                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.ID = this.reader[0].ToString().PadLeft(10, '0');
                    patientInfo.PID.PatientNO = this.reader[0].ToString().PadLeft(10, '0');
                    patientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.reader[1].ToString());
                    patientInfo.Name = this.reader[2].ToString();
                    patientInfo.Sex.Name = this.reader[3].ToString();
                    patientInfo.PVisit.PatientLocation.Dept.ID = this.reader[4].ToString();//��Ժ����ͳһ�� �㶫ʡ3.0ϵͳ
                    patientInfo.PVisit.PatientLocation.Dept.Name = this.reader[5].ToString();
                    patientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[6].ToString());
                    patientInfo.PVisit.PatientLocation.ID = this.reader[7].ToString();//��Ժͳһ��
                    patientInfo.PVisit.PatientLocation.Name = this.reader[8].ToString();
                    patientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[9].ToString());
                    patientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[10].ToString());
                    patientInfo.Kin.Relation.Name = this.reader[11].ToString();
                    patientInfo.PVisit.PreOutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.reader[12].ToString());//��������
                    al.Add(patientInfo);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }

        public List<FS.HISFC.Models.HealthRecord.Case.CaseStore> GetPatientByCaseNo(string strBegin, string strEnd)
        {
            string Sql = "select distinct  fprn,fname,ftimes from dbo.tpatientvisit where fprn>='{0}' and fprn<='{1}'";
            try
            {
                Sql = string.Format(Sql, strBegin, strEnd);
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            List<FS.HISFC.Models.HealthRecord.Case.CaseStore> list = new List<FS.HISFC.Models.HealthRecord.Case.CaseStore>();
            ReadSQL(Sql);

            this.reader = this.cmd.ExecuteReader();

            try
            {
                FS.HISFC.Models.HealthRecord.Case.CaseStore obj = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
                while (this.reader.Read())
                {
                    obj = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
                    obj.PatientInfo.ID = this.reader[0].ToString().PadLeft(10, '0');
                    obj.PatientInfo.PID.PatientNO = this.reader[0].ToString().PadLeft(10, '0');
                    obj.PatientInfo.Name = this.reader[1].ToString();
                    obj.PatientInfo.InTimes =FS.FrameWork.Function.NConvert.ToInt32(this.reader[2].ToString());

                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }


        /// <summary>
        /// ���3.0���ұ�����
        /// </summary>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> GetDeptCodeByCode()
        {

            string Sql = "select ftykh ,fksname,fkh  from tWorkroom ";
            //try
            //{
            //    Sql = string.Format(Sql, Code);
            //}
            //catch (Exception ex)
            //{
            //    this.Err1 = ex.Message;
            //    return null;
            //}
            List<FS.FrameWork.Models.NeuObject> list = new List<FS.FrameWork.Models.NeuObject>();
            ReadSQL(Sql);
            this.reader = this.cmd.ExecuteReader();
            try
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                while (this.reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.reader[0].ToString();
                    obj.Name = this.reader[1].ToString();
                    obj.Memo = this.reader[2].ToString();
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }
        /// <summary>
        /// ���3.0ר�ƿ��ұ�����
        /// </summary>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> GetZkDeptCodeByCode()
        {

            string Sql = "select  ftyzkcode,fzkname,fzkcode from tSpecialRoom";
            List<FS.FrameWork.Models.NeuObject> list = new List<FS.FrameWork.Models.NeuObject>();
            ReadSQL(Sql);
            this.reader = this.cmd.ExecuteReader();
            try
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                while (this.reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.reader[0].ToString();
                    obj.Name = this.reader[1].ToString();
                    obj.Memo = this.reader[2].ToString();
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }
        /// <summary>
        /// ���3.0ҽ��������
        /// </summary>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> GetDocCodeByCode()
        {

            string Sql = "select ftygh,fname,fgh from tdoctor ";
            List<FS.FrameWork.Models.NeuObject> list = new List<FS.FrameWork.Models.NeuObject>();
            ReadSQL(Sql);
            this.reader = this.cmd.ExecuteReader();
            try
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                while (this.reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.reader[0].ToString();
                    obj.Name = this.reader[1].ToString();
                    obj.Memo = this.reader[2].ToString();
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }
        /// <summary>
        /// ��� 3.0˳��¼�뻼�� ���ڴ�ӡ����
        /// </summary>
        /// <param name="dtBegin">��ʼ����</param>
        /// <param name="dtEnd">��������</param>
        /// <param name="operCode">¼�����Ա��</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> GetPatientBarCode(DateTime dtBegin ,DateTime dtEnd ,string operCode)
        {

            string Sql = @" select  t.fprn,t.ftimes,t.fcydept,t.fname from tpatientvisit t 
                            where t.fworkrq between '{0}'  and  '{1}' 
                            and ( fsry='{2}'  or 'ALL'='{2}')
                            order by t.fid";
            try
            {
                Sql = string.Format(Sql, dtBegin, dtEnd, operCode);
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            List<FS.HISFC.Models.RADT.PatientInfo> list = new List<FS.HISFC.Models.RADT.PatientInfo>();
            ReadSQL(Sql);
            this.reader = this.cmd.ExecuteReader();
            try
            {
                FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
                while (this.reader.Read())
                {
                    obj = new FS.HISFC.Models.RADT.PatientInfo();
                    obj.PID.PatientNO = this.reader[0].ToString();
                    obj.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.reader[1].ToString());
                    obj.PVisit.PatientLocation.Name = this.reader[2].ToString();
                    obj.Name = this.reader[3].ToString();
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }

        /// <summary>
        ///  ���3.0 ICD10����
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.HealthRecord.ICD> GetICDCodeByType(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes type,string icdVersion)
        {
            string Sql ="";
            if (type == FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10)
            {
                if (icdVersion == "10")
                {
                    Sql = "select FId,FICDM,FJBNAME,FTjm from tICD where FICDVersion='10' ";
                }
                else
                {
                    Sql = "select FId,FICDM,FJBNAME,FTjm from tICD where FICDVersion='11' or FICDVersion='12'";
                }
            }
            else if (type == FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD9)
            {
                Sql = "select FId,FICDM,FJBNAME,FTjm from tICD where FICDVersion='9' ";
            }
            else
            {
                Sql = "select FId,FOpcode,FOpname,FZjc from tOperate  ";
            }
            List<FS.HISFC.Models.HealthRecord.ICD> list = new List<FS.HISFC.Models.HealthRecord.ICD>();
            ReadSQL(Sql);
            this.reader = this.cmd.ExecuteReader();
            try
            {
                FS.HISFC.Models.HealthRecord.ICD obj = new  FS.HISFC.Models.HealthRecord.ICD();
                while (this.reader.Read())
                {
                    obj = new FS.HISFC.Models.HealthRecord.ICD();
                    obj.ID = this.reader[0].ToString();
                    obj.Name = this.reader[1].ToString();
                    obj.Memo = this.reader[2].ToString();
                    obj.DiseaseCode = this.reader[3].ToString();
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return null;
            }
            finally
            {
                this.reader.Close();
            }
        }


        #region drgs�ӿ�����  2012-6-26
        #region HIS_BA1
        /// <summary>
        ///  HIS_BA1 --������Ϣ
        /// </summary>
        /// <param name="b">������ҳʵ��</param>
        /// <param name="Feeds">������Ϣ</param>
        /// <param name="alChangeDepe">ת����Ϣ</param>
        /// <param name="alDose"> ���</param>
        ///<param name="isMetCasBase">�Ƿ񲡰���������</param> 
        /// <returns></returns>
        public int InsertPatientInfoBA1Drgs(FS.HISFC.Models.HealthRecord.Base b, DataSet Feeds,
            System.Collections.ArrayList alChangeDepe, System.Collections.ArrayList alDose, bool isMetCasBase)
        {
            string strSQL = this.GetInsertHISBA1SQLDrgs(b, Feeds, alChangeDepe, alDose, isMetCasBase);
            if (strSQL == null || strSQL == "")
            {
                return -1;
            }
            ReadSQL(strSQL);
            int intReturn = this.cmd.ExecuteNonQuery();
            if (intReturn < 0)
            {
                return -1;
            }
            return intReturn;
        }

        /// <summary>
        /// �ӿ�HIS_BA1 INSERT SQL ��ֵ
        /// </summary>
        /// <param name="b">������ҳ��ʵ����</param>
        /// <param name="Feeds">������Ϣ����</param>
        /// <param name="alChangeDepe">ת����Ϣ����</param>
        /// <param name="alDose">�����Ϣ����</param>
        /// <param name="isMetCasBase">true������ҳ��Ϣ false סԺ������Ϣ</param>
        /// <returns>ʧ�ܷ���null</returns>
        private string[] GetBaseInfoBA1Drgs(FS.HISFC.Models.HealthRecord.Base b, DataSet Feeds,
            System.Collections.ArrayList alChangeDepe, System.Collections.ArrayList alDose, bool isMetCasBase)
        {
            //ArrayList al = this.baseDml.QueryHealthRecordCaseinfo(b.PatientInfo.ID);
            if (isMetCasBase)
            {
                #region
                string[] s = new string[194];
                try
                {
                    s[0] = "0";//�Ƿ����룬0����1���ǣ�Ĭ�Ͻ���Ϊ0
                    string patientNO = b.PatientInfo.PID.PatientNO.PadLeft(10, '0');
                    s[1] = this.PatientNoChang(patientNO.Substring(this.PatientNoSubstr()));//������
                    s[2] = b.PatientInfo.InTimes.ToString().PadLeft(2, '0');//סԺ����
                    s[3] = "11";//ICD�汾��9��ICD9�ֵ�⣬10��ICD10�ֵ�⣬����չ11������ICD�⣬Ĭ�Ͻ���Ϊ11
                    s[4] = b.PatientInfo.ID;//סԺ��ˮ��
                    #region //s[5] ����
                    if (b.PatientInfo.Age != "" && b.PatientInfo.Age != "0")
                    {
                        if (b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") < 0) //����
                        {
                            s[5] = "Y" + b.AgeUnit.Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") < 0 && b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") < 0)//����
                        {
                            s[5] = "M" + b.AgeUnit.Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") < 0 && b.AgeUnit.IndexOf("��") < 0 && b.AgeUnit.IndexOf("��") > 0)//����
                        {
                            s[5] = "D" + b.AgeUnit.Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") < 0)//N��N��
                        {
                            string[] PAge = b.AgeUnit.Split('��');
                            s[5] = "Y" + PAge[0] + "M" + PAge[1].Replace("��", "").Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") < 0 && b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") > 0)//N��N��
                        {
                            string[] PAge = b.AgeUnit.Split('��');
                            s[5] = "M" + PAge[0] + "D" + PAge[1].Replace("��", "").Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") > 0)//N��N��N��
                        {
                            string[] PAge = b.AgeUnit.Split('��');

                            string[] PAge1 = PAge[1].Split('��');
                            s[5] = "Y" + PAge[0] + "M" + PAge1[0] + "D" + PAge1[1].Replace("��", "").Replace("��", "");
                        }
                    }
                    else
                    {
                        int ts = b.PatientInfo.PVisit.InTime.Year - b.PatientInfo.Birthday.Year;

                        if (ts == 0)
                        {
                            ts = b.PatientInfo.PVisit.InTime.Month - b.PatientInfo.Birthday.Month;

                            if (ts == 0)
                            {
                                ts = b.PatientInfo.PVisit.InTime.Day - b.PatientInfo.Birthday.Day;
                                s[5] = "D" + ts.ToString();//���� 
                            }
                            else
                            {
                                s[5] = "M" + ts.ToString();//���� 
                            }
                        }
                        else
                        {
                            s[5] = "Y" + ts.ToString();//���� 
                        }
                    }
                    #endregion

                    s[6] = b.PatientInfo.Name;//��������
                    //�Ա���
                    //�Ա�
                    if (b.PatientInfo.Sex.ID.ToString() == "M" || b.PatientInfo.Sex.ID.ToString() == "1")
                    {
                        s[7] = "1";
                        s[8] = "��";
                    }
                    else
                    {
                        s[7] = "2";
                        s[8] = "Ů";
                    }
                    s[9] = b.PatientInfo.Birthday.ToShortDateString().Replace('-', '/');//��������
                    s[10] = b.PatientInfo.AreaCode; //������
                    if (b.PatientInfo.IDCard == "" || b.PatientInfo.IDCard.Trim() == "-")
                    {
                        s[11] = "����";
                    }
                    else
                    {
                        s[11] = b.PatientInfo.IDCard;//���֤��
                    }
                    #region s[12]�������\ s[13]����  �й�  ��Ҫת������
                    if (b.PatientInfo.Country.ID.ToString() == "1")
                    {
                        s[12] = "A156";
                        s[13] = "�й�";
                    }
                    else
                    {
                        FS.FrameWork.Models.NeuObject countryObj = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.COUNTRY, b.PatientInfo.Country.ID.ToString());
                        if (countryObj != null && countryObj.ID != "")
                        {
                            if (countryObj.Memo != "" && countryObj.Memo.ToUpper()!="TRUE")
                            {
                                s[12] = countryObj.Memo.ToString();
                                s[13] = countryObj.Name.ToString();
                            }
                            else
                            {
                                s[12] = countryObj.ID.ToString();
                                s[13] = countryObj.Name.ToString();
                            }
                        }
                        else
                        {
                            s[12] = b.PatientInfo.Country.ID.ToString();
                            s[13] = "";
                        }
                    }
                    #endregion
                    #region s[14]������ s[15]����
                    FS.FrameWork.Models.NeuObject NationObj = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.NATION, b.PatientInfo.Nationality.ID.ToString());
                    if (NationObj != null && NationObj.ID != "")
                    {
                        if (NationObj.Memo != "" && NationObj.Memo.ToUpper()!="TRUE")
                        {
                            s[14] = NationObj.Memo;
                            s[15] = NationObj.Name;
                        }
                        else
                        {
                            s[14] = NationObj.ID;
                            s[15] = NationObj.Name;
                        }
                    }
                    else
                    {
                        s[14] = b.PatientInfo.Nationality.ID;
                        s[15] = "";
                    }
                    #endregion
                    #region  s[16] ְҵ�����Ĵ�����
                    //add by chengym 2011-6-15  �ֵ��������ֶ�varchar��100�� ��Щְҵ��������25�������ַ�����ʱ��ȡ��ע���������ƣ���֤�ϴ�������û�����⣻ 
                    FS.FrameWork.Models.NeuObject JobObj = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.CASEPROFESSION, b.PatientInfo.Profession.ID.ToString());
                    if (JobObj != null && JobObj.ID != "")
                    {
                        if (JobObj.Memo != "" && JobObj.Memo.ToUpper() != "TRUE")
                        {
                            if (JobObj.Memo.Length <= 50)
                            {
                                s[16] = JobObj.Memo;
                            }
                            else
                            {
                                s[16] = JobObj.Memo.Substring(0, 50);
                            }
                        }
                        else
                        {
                            if (JobObj.Name.Length <= 50)
                            {
                                s[16] = JobObj.Name;
                            }
                            else
                            {
                                s[16] = JobObj.Name.Substring(0, 50);
                            }
                        }
                    }
                    else
                    {
                        s[16] = b.PatientInfo.Profession.ID.ToString(); //ְҵ û�д����Ĳ�֪���Ƿ����
                    }
                    #endregion
                    #region s[17] ����״����� s[18]����״��
                    if (b.PatientInfo.MaritalStatus.ID.ToString() == "S" || b.PatientInfo.MaritalStatus.ID.ToString() == "1"
                        || b.PatientInfo.MaritalStatus.ID.ToString() == "10")
                    {
                        s[17] = "10"; //����״�����
                        s[18] = "δ��"; //����״��
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "M" || b.PatientInfo.MaritalStatus.ID.ToString() == "2"
                        || b.PatientInfo.MaritalStatus.ID.ToString() == "20")
                    {
                        s[17] = "20";
                        s[18] = "�ѻ�";
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "W" || b.PatientInfo.MaritalStatus.ID.ToString() == "3"
                        || b.PatientInfo.MaritalStatus.ID.ToString() == "30")
                    {
                        s[17] = "30";
                        s[18] = "ɥż";
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "A")
                    {
                        s[17] = "20";
                        s[18] = "�ѻ�";
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "D" || b.PatientInfo.MaritalStatus.ID.ToString() == "4"
                        || b.PatientInfo.MaritalStatus.ID.ToString() == "40")
                    {
                        s[17] = "40";
                        s[18] = "���";
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "R")
                    {
                        s[17] = "20";
                        s[18] = "�ѻ�";
                    }
                    else
                    {
                        s[17] = "90";
                        s[18] = "δ˵���Ļ���״��";
                    }
                    #endregion
                    s[19] = b.PatientInfo.AddressBusiness;  //��λ����
                    s[20] = b.PatientInfo.AddressBusiness;//��λ��ַ 
                    s[21] = b.PatientInfo.PhoneBusiness;//��λ�绰
                    s[22] = b.PatientInfo.BusinessZip;//��λ�ʱ�      
                    s[23] = b.PatientInfo.AddressHome;//���ڵ�ַ
                    s[24] = b.PatientInfo.HomeZip;//�����ʱ�
                    s[25] = b.PatientInfo.Kin.Name;//��ϵ��
                    #region s[26] �벡�˹�ϵ
                    FS.FrameWork.Models.NeuObject RelativeObj = this.constMana.GetConstant("RELATIVE", b.PatientInfo.Kin.RelationLink);
                    if (RelativeObj != null && RelativeObj.ID != "")
                    {
                        if (RelativeObj.Memo != "" && RelativeObj.Memo.ToUpper()!="TRUE")
                        {
                            if (RelativeObj.Memo.Length <= 10)
                            {
                                s[26] = RelativeObj.Memo;//�뻼�߹�ϵ
                            }
                            else
                            {
                                s[26] = RelativeObj.Memo.Substring(0, 10);//�뻼�߹�ϵ
                            }
                        }
                        else
                        {
                            if (RelativeObj.Name.Length <= 10)
                            {
                                s[26] = RelativeObj.Name;//�뻼�߹�ϵ
                            }
                            else
                            {
                                s[26] = RelativeObj.Name.Substring(0, 10);//�뻼�߹�ϵ
                            }
                        }
                    }
                    else
                    {
                        s[26] = b.PatientInfo.Kin.RelationLink;//�뻼�߹�ϵ
                    }
                    #endregion
                    s[27] = b.PatientInfo.Kin.RelationAddress;//��ϵ�˵�ַ
                    s[28] = b.PatientInfo.Kin.RelationPhone;//��ϵ�˵绰
                    if (b.PatientInfo.SSN.Trim() == "--" || b.PatientInfo.SSN.Trim() == "��" || b.PatientInfo.SSN.Trim() == "-"
                    || b.PatientInfo.SSN.Trim() == "��" || b.PatientInfo.SSN.Trim() == "����"
                    || b.PatientInfo.SSN.Trim().Length < 4)
                    {
                        s[29] = "";
                    }
                    else
                    {
                        s[29] = b.PatientInfo.SSN; // ԭ3.0��ҽ������
                    }
                    s[30] = b.PatientInfo.PVisit.InTime.ToShortDateString().Replace('-', '/');//��Ժ����
                    s[31] = b.PatientInfo.PVisit.InTime.Hour.ToString().PadLeft(2, '0'); //��Ժʱ��
                    s[32] = this.ConverDept(b.InDept.ID);//��Ժ���Ҵ��� ��Ժͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                    s[33] = this.ConverDeptName(b.InDept.ID, b.InDept.Name);//��Ժ��������2011-6-8
                    s[34] = b.InRoom;//��Ժ����    
                    s[35] = b.PatientInfo.PVisit.OutTime.ToShortDateString().Replace('-', '/');//��Ժ����
                    s[36] = b.PatientInfo.PVisit.OutTime.Hour.ToString().PadLeft(2, '0'); //��Ժʱ��
                    s[37] = this.ConverDept(b.OutDept.ID);//��Ժ���Ҵ���
                    s[38] = this.ConverDeptName(b.OutDept.ID, b.OutDept.Name);//��Ժ��������2011-6-8
                    s[39] = b.OutRoom; //��Ժ����
                    s[40] = b.InHospitalDays.ToString();//ʵ��סԺ����
                    s[41] = b.ClinicDiag.ID;//�ţ����������(ICD10��ICD9)����

                    if (b.ClinicDiag.Name.Length > 50)//�ţ����������(ICD10��ICD9)��Ӧ������
                    {
                        s[42] = this.ChangeCharacter(b.ClinicDiag.Name.Substring(0, 50).ToString());
                    }
                    else
                    {
                        s[42] = this.ChangeCharacter(b.ClinicDiag.Name);
                    }
                    s[43] = this.ConverDoc(b.ClinicDoc.ID);//�š�����ҽ����ţ���Ӧtdoctor �е�ftygh
                    s[44] = b.ClinicDoc.Name;//�š�����ҽ��
                    //�������
                    //if (b.PathologicalDiagCode == null)
                    //{
                    //    s[45] = b.PathologicalDiagName;
                    //}
                    //else
                    //{
                    //    s[45] = b.PathologicalDiagCode;
                    //}
                    if (b.PathologicalDiagName.Trim() == "-"
                    || b.PathologicalDiagName.Trim() == "��"
                    || b.PathologicalDiagName.Trim() == "--"
                    || b.PathologicalDiagName.Trim() == "����"
                    || b.PathologicalDiagName.Trim() == "��"
                    || b.PathologicalDiagName == "δ����"
                    || b.PathologicalDiagName == "/"
                    || b.PathologicalDiagName.Trim() == "��"
                    || b.PathologicalDiagName.Trim().Length < 3)
                    {
                        s[45] = "";
                    }
                    else
                    {
                        s[45] = b.PathologicalDiagName;
                    }

                    //����ҩ��
                    string anaphyPh = b.FirstAnaphyPharmacy.ID;
                    if (anaphyPh.Length > 100)
                    {
                        s[46] = this.ChangeCharacter(anaphyPh.Substring(0, 100));
                    }
                    else
                    {
                        s[46] = this.ChangeCharacter(anaphyPh);//ҩ�����  
                    }
                    //�������Ժ��Ϸ���������
                    if (b.CePi == null || b.CePi == "")
                    {
                        s[47] = "1";
                        s[48] = "����";
                    }
                    else
                    {
                        s[47] = b.CePi;
                        s[48] = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.ACCORDSTAT, b.CePi).Name;
                    }
                    //�ٴ��벡����Ϸ������
                    if (b.ClPa == null || b.ClPa == "")
                    {
                        s[49] = "1";
                        s[50] = "����";
                    }
                    else
                    {
                        s[49] = b.ClPa;
                        s[50] = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.ACCORDSTAT, b.ClPa).Name;
                    }
                    s[51] = b.SalvTimes.ToString();//���ȴ���
                    s[52] = b.SuccTimes.ToString();//�ɹ�����

                    s[53] = this.ConverDoc(b.PatientInfo.PVisit.ReferringDoctor.ID);//�����α�ţ���Ӧtdoctor �е�ftygh
                    s[54] = b.PatientInfo.PVisit.ReferringDoctor.Name;//����������
                    s[55] = this.ConverDoc(b.PatientInfo.PVisit.ConsultingDoctor.ID);//������������ҽ����ţ���Ӧtdoctor �е�ftygh
                    s[56] = b.PatientInfo.PVisit.ConsultingDoctor.Name;//����ҽʦ����
                    s[57] = this.ConverDoc(b.PatientInfo.PVisit.AttendingDoctor.ID);//����ҽ����ţ���Ӧtdoctor �е�ftygh
                    s[58] = b.PatientInfo.PVisit.AttendingDoctor.Name;//����ҽʦ����
                    s[59] = this.ConverDoc(b.PatientInfo.PVisit.AdmittingDoctor.ID);//סԺҽ����ţ���Ӧtdoctor �е�ftygh
                    s[60] = b.PatientInfo.PVisit.AdmittingDoctor.Name;//סԺҽʦ����
                    s[61] = this.ConverDoc(b.RefresherDoc.ID);//����ҽʦ��ţ���Ӧtdoctor �е�ftygh
                    s[62] = b.RefresherDoc.Name;//����ҽ��
                    s[63] = this.ConverDoc(b.PatientInfo.PVisit.TempDoctor.ID);//ʵϰҽ����ţ���Ӧtdoctor �е�ftygh
                    s[64] = b.PatientInfo.PVisit.TempDoctor.Name;//ʵϰҽʦ����
                    s[65] = this.ConverDoc(b.CodingOper.ID);//����Ա���
                    s[66] = b.CodingOper.Name;//����Ա����
                    s[67] = this.ConverDoc(b.OperInfo.ID);//���������߱��
                    s[68] = b.OperInfo.Name;//����Ա���ƣ����������ߣ�
                    s[69] = b.MrQuality;//�������� 
                    s[70] = this.constMana.GetConstant("CASEQUALITY", b.MrQuality).Name;
                    s[71] = this.ConverDoc(b.QcDoc.ID);//�ʿ�ҽʦ����
                    s[72] = b.QcDoc.Name;//�ʿ�ҽʦ
                    s[73] = this.ConverDoc(b.QcNurse.ID);
                    s[74] = b.QcNurse.Name;//�ʿػ�ʿ����
                    //�ʿ�����
                    if (b.CheckDate < new DateTime(1900, 1, 1))
                    {
                        s[75] = b.PatientInfo.PVisit.OutTime.ToShortDateString().Replace('-', '/');
                    }
                    else if (b.CheckDate <= b.PatientInfo.PVisit.OutTime)//�ʿ����ڲ�����С�ڳ�Ժ����
                    {
                        s[75] = b.PatientInfo.PVisit.OutTime.ToShortDateString().Replace('-', '/');
                    }
                    else
                    {
                        s[75] = b.CheckDate.ToShortDateString().Replace('-', '/');//�ʿ�����
                    }
                    #region ���� �ܷ���s[76] ��ҩ��s[77] ��ҩ��s[78] �г�ҩ��s[79] �в�ҩ��s[80] ������s[81]
                    if (Feeds == null || Feeds.Tables.Count == 0 || Feeds.Tables[0].Rows.Count == 0)
                    {
                        s[76] = "0.00";//�ܷ���
                        s[77] = "0.00";//��ҩ��
                        s[78] = "0.00";//��ҩ��
                        s[79] = "0.00";//�г�ҩ��
                        s[80] = "0.00";//�в�ҩ��
                        s[81] = "0.00";//������
                    }
                    else
                    {
                        s[76] = Feeds.Tables[0].Rows[0][0].ToString();//�ܷ���
                        s[77] = Feeds.Tables[0].Rows[0][17].ToString();//��ҩ��
                        s[78] = "0.00";//��ҩ��
                        s[79] = Feeds.Tables[0].Rows[0][19].ToString();//�г�ҩ��
                        s[80] = Feeds.Tables[0].Rows[0][20].ToString();//�в�ҩ��
                        s[81] = Feeds.Tables[0].Rows[0][29].ToString();//������
                    }
                    #endregion
                    //�Ƿ�ʬ����1���� 2����
                    if (b.CadaverCheck == "1")
                    {
                        s[82] = "1";
                        s[83] = "��";
                    }
                    else if (b.CadaverCheck == "2")
                    {
                        s[82] = "2";
                        s[83] = "��";
                    }
                    else
                    {
                        s[82] = "";
                        s[83] = "-";
                    }
                    //s[83] = this.constMana.GetConstant("CASEYSEORNO", b.CadaverCheck).Name;
                    #region s[84]Ѫ�ͱ�� s[85]Ѫ��
                    //if (b.PatientInfo.BloodType.ID.ToString() == "A")
                    //{
                    //    s[84] = "1";
                    //    s[85] = b.PatientInfo.BloodType.ID.ToString();
                    //}
                    //else if (b.PatientInfo.BloodType.ID.ToString() == "B")
                    //{
                    //    s[84] = "2";
                    //    s[85] = b.PatientInfo.BloodType.ID.ToString();
                    //}
                    //else if (b.PatientInfo.BloodType.ID.ToString() == "AB")
                    //{
                    //    s[84] = "4";
                    //    s[85] = b.PatientInfo.BloodType.ID.ToString();
                    //}
                    //else if (b.PatientInfo.BloodType.ID.ToString() == "O")
                    //{
                    //    s[84] = "3";
                    //    s[85] = b.PatientInfo.BloodType.ID.ToString();
                    //}
                    //else if (b.PatientInfo.BloodType.ID.ToString() == "9")
                    //{
                    //    s[84] = "6";
                    //    s[85] = "δ��";
                    //}
                    //else if (b.PatientInfo.BloodType.ID.ToString() == "6")
                    //{
                    //    s[84] = "6";
                    //    s[85] = "δ��";
                    //}
                    //else
                    //{
                    //    s[84] = "5";
                    //    s[85] = "����";
                    //} 
                    
                    //CHANGE  BY ZHY    2013-08-01                 
                    if (b.PatientInfo.BloodType.ID.ToString() == "1")
                    {
                        s[84] = "1";
                        s[85] = b.PatientInfo.BloodType.ToString();
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString()== "2")
                    {
                        s[84] = "2";
                        s[85] = b.PatientInfo.BloodType.ToString();
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString() == "3")
                    {
                        s[84] = "3";
                        s[85] = b.PatientInfo.BloodType.ToString();
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString() == "4")
                    {
                        s[84] = "4";
                        s[85] = b.PatientInfo.BloodType.ToString();
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString() == "5")
                    {
                        s[84] = "5";
                        s[85] = b.PatientInfo.BloodType.ToString();
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString() == "6")
                    {
                        s[84] = "6";
                       // s[85] = b.PatientInfo.BloodType.ToString();
                        s[85] = "δ��";
                    }
                    else
                    {
                        s[84] = "5";
                       // s[85] = b.PatientInfo.BloodType.ToString();
                        s[85] = "����";
                    }
                    #endregion
                    //s[86] = b.RhBlood;//RH���
                    //RH
                    if (b.RhBlood == "1")
                    {
                        s[86] = "1";
                        s[87] = "��";
                    }
                    else if (b.RhBlood == "2")
                    {
                        s[86] = "2";
                        s[87] = "��";
                    }
                    else if (b.RhBlood == "3")
                    {
                        s[86] = "3";
                        s[87] = "����";
                    }
                    else
                    {
                        s[86] = "4";
                        s[87] = "δ��";
                    }
                    //Ӥ����
                    int babyNum = 0;
                    try
                    {
                        babyNum = FS.FrameWork.Function.NConvert.ToInt32(b.PatientInfo.User03);
                        s[88] = babyNum.ToString();
                    }
                    catch
                    {
                        s[88] = "0";
                    }
                    s[89] = "0";//�Ƿ񲿷ֲ��֣�1�� 0��
                    #region  s[90]�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ� s[91]�״�ת�ƿƱ�  s[92]�״�ת������  s[93]�״�ת��ʱ��
                    if (alChangeDepe != null && alChangeDepe.Count > 0)
                    {
                        FS.HISFC.Models.RADT.Location changeDept = alChangeDepe[0] as FS.HISFC.Models.RADT.Location;
                        if (changeDept.Dept.ID != null && changeDept.Dept.ID != "")
                        {
                            try
                            {
                                s[90] = this.ConverDept(changeDept.Dept.ID);//�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                                s[91] = this.ConverDeptName(changeDept.Dept.ID, changeDept.Dept.Name);//�״�ת�ƿƱ�
                                s[92] = FS.FrameWork.Function.NConvert.ToDateTime(changeDept.Dept.Memo).ToShortDateString();//�״�ת������
                                s[93] = FS.FrameWork.Function.NConvert.ToDateTime(changeDept.Dept.Memo).Hour.ToString().PadLeft(2, '0');//�״�ת��ʱ��
                            }
                            catch
                            {
                                s[90] = "";//�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                                s[91] = "";//�״�ת�ƿƱ�
                                s[92] = "";//�״�ת������
                                s[93] = "";//�״�ת��ʱ��
                            }
                        }
                        else
                        {
                            s[90] = "";//�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                            s[91] = "";//�״�ת�ƿƱ�
                            s[92] = "";//�״�ת������
                            s[93] = "";//�״�ת��ʱ��
                        }
                    }
                    else
                    {
                        s[90] = "";//�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                        s[91] = "";//�״�ת�ƿƱ�
                        s[92] = "";//�״�ת������
                        s[93] = "";//�״�ת��ʱ��
                    }
                    #endregion
                    s[94] = this.ConverDoc(b.OperInfo.ID);//����Ա���
                    s[95] = b.OperInfo.Name;//����Ա
                    s[96] = System.DateTime.Now.ToShortDateString();//��������FS.FrameWork.Function.NConvert.ToDateTime(((FS.FrameWork.Models.RADT.Location)alChangeDepe[0]).User01).ToShortDateString().Replace('-', '/');
                    FS.FrameWork.Models.NeuObject CaseExaplTypeObj = this.constMana.GetConstant("CASEEXAMPLETYPE", b.ExampleType);
                    if (CaseExaplTypeObj != null && CaseExaplTypeObj.ID != "")
                    {
                        if (CaseExaplTypeObj.Memo != "" && CaseExaplTypeObj.Memo.ToUpper() != "TRUE")
                        {
                            s[97] = CaseExaplTypeObj.Memo;//�������ͱ��
                            s[98] = b.ExampleType;//��������
                        }
                        else
                        {
                            switch (b.ExampleType)//�������ͱ��
                            {
                                case "A":
                                    s[97] = "1";
                                    break;
                                case "B":
                                    s[97] = "2";
                                    break;
                                case "C":
                                    s[97] = "3";
                                    break;
                                case "D":
                                    s[97] = "4";
                                    break;
                            }
                            s[98] = b.ExampleType;//��������
                        }
                    }
                    else
                    {
                        switch (b.ExampleType)//�������ͱ��
                        {
                            case "A":
                                s[97] = "1";
                                break;
                            case "B":
                                s[97] = "2";
                                break;
                            case "C":
                                s[97] = "3";
                                break;
                            case "D":
                                s[97] = "4";
                                break;
                        }
                        s[98] = b.ExampleType;//��������
                    }

                    s[99] = "";//���Ϲ鵵���
                    s[100] = "";//���Ϲ鵵
                    s[101] = b.PatientInfo.PVisit.InSource.ID;//������Դ���
                    s[102] = this.constMana.GetConstant("INAVENUE", b.PatientInfo.PVisit.InSource.ID).Name;//������Դ
                    s[103] = b.PatientInfo.User02;//�Ƿ����� chengym
                    if (b.PatientInfo.User03 == "0")
                    {
                        s[104] = "0";//�Ƿ����븾Ӥ��
                    }
                    else
                    {
                        s[104] = "1";//�Ƿ����븾Ӥ��
                    }
                    //Ժ�д�������ԴԺ�б��� 12-8-28
                    try
                    {
                        int infNum = this.baseDml.QueryInfCount(b.PatientInfo.ID);
                        if (infNum == -1)
                        {
                            s[105] = "0";//ҽԺ��Ⱦ������������Ϊ�գ�����Ӱ�챨��ͳ�ƽ�� chengym
                        }
                        else
                        {
                            s[105] = infNum.ToString();
                        }
                    }
                    catch
                    {
                        s[105] = "0";
                    }
                    s[106] = "";//��չ1 
                    s[107] = "";
                    s[108] = "";
                    s[109] = "";
                    s[110] = "";
                    s[111] = "";
                    s[112] = "";
                    s[113] = "";//��չ8������
                    s[114] = "";//��չ9��������
                    s[115] = "";
                    s[116] = "";
                    s[117] = "";
                    s[118] = "";
                    s[119] = "";
                    s[120] = "";//��չ15
                    s[121] = b.PatientInfo.DIST;//����
                    s[122] = b.CurrentAddr;//��סַ
                    s[123] = b.CurrentPhone;//�ֵ绰
                    s[124] = b.CurrentZip;//���ʱ�
                    s[125] = b.PatientInfo.Profession.ID;//ְҵ���
                    //���ܴ���ҽԺ��д����������
                    try
                    {
                        int bweight = FS.FrameWork.Function.NConvert.ToInt32(b.BabyBirthWeight);
                        b.BabyBirthWeight = bweight.ToString();
                    }
                    catch
                    {
                        b.BabyBirthWeight = "0";
                    }
                    s[126] = b.BabyBirthWeight;//��������������
                    //���ܴ���ҽԺ��д����������
                    try
                    {
                        int biweight = FS.FrameWork.Function.NConvert.ToInt32(b.BabyInWeight);
                        b.BabyInWeight = biweight.ToString();
                    }
                    catch
                    {
                        b.BabyInWeight = "0";
                    }
                    s[127] = b.BabyInWeight;//��������Ժ����
                    s[128] = b.InPath;//��Ժ;�����
                    s[129] = this.constMana.GetConstant("CASEINAVENUE", b.InPath).Name;//��Ժ;��
                    s[130] = b.ClinicPath;//�ٴ�·���������
                    if (b.ClinicPath == "1")
                    {
                        s[131] = "��";//�ٴ�·������
                    }
                    else
                    {
                        s[131] = "��";//�ٴ�·������
                    }
                    if (b.PathologicalDiagName.Trim() == "-"
                   || b.PathologicalDiagName.Trim() == "��"
                   || b.PathologicalDiagName.Trim() == "--"
                   || b.PathologicalDiagName.Trim() == "����"
                   || b.PathologicalDiagName.Trim() == "��"
                   || b.PathologicalDiagName == "δ����"
                   || b.PathologicalDiagName == "/"
                   || b.PathologicalDiagName.Trim() == "��"
                   || b.PathologicalDiagName.Trim().Length < 3)
                    {
                        s[49] = "";
                        s[50] = "";
                        s[132] = "";//����������
                        s[133] = "";//�����
                    }
                    else
                    {
                        s[132] = b.PathologicalDiagCode;//����������
                        s[133] = b.PathNum;//�����
                    }
                    s[134] = b.AnaphyFlag;//�Ƿ�ҩ��������
                    if (b.AnaphyFlag == "1")
                    {
                        s[135] = "��";//�Ƿ�ҩ�����
                    }
                    else
                    {
                        s[135] = "��";//�Ƿ�ҩ�����
                    }
                    s[136] = this.ConverDoc(b.DutyNurse.ID);//���λ�ʿ���
                    s[137] = b.DutyNurse.Name;//���λ�ʿ
                    //s[138] = b.Out_Type;//��Ժ��ʽ���
                    if (b.Out_Type == "1")//��Ժ��ʽ
                    {
                        s[138] = "1";
                        s[139] = "ҽ����Ժ";
                    }
                    else if (b.Out_Type == "2")//��Ժ��ʽ
                    {
                        s[138] = "2";
                        s[139] = "ҽ��תԺ";
                    }
                    else if (b.Out_Type == "3")//��Ժ��ʽ
                    {
                        s[138] = "3";
                        s[139] = "ҽ��ת������������Ժ";
                    }
                    else if (b.Out_Type == "4")//��Ժ��ʽ
                    {
                        s[138] = "4";
                        s[139] = "��ҽ����Ժ";
                    }
                    else if (b.Out_Type == "5")//��Ժ��ʽ
                    {
                        s[138] = "5";
                        s[139] = "����";
                    }
                    else
                    {
                        s[138] = "9";
                        s[139] = "����";
                    }
                    s[140] = b.HighReceiveHopital;//��Ժ��ʽΪҽ��תԺ�������ҽ�ƻ�������
                    s[141] = b.LowerReceiveHopital;//��Ժ��ʽΪת������������������/��������Ժ�������ҽ�ƻ�������
                    s[142] = b.ComeBackInMonth;//�Ƿ��г�Ժ31������סԺ�ƻ����
                    s[143] = "";//�Ƿ��г�Ժ31������סԺ�ƻ�
                    s[144] = b.ComeBackPurpose;//��סԺĿ��
                    s[145] = b.OutComeDay.ToString();//­�����˻��߻���ʱ�䣺��Ժǰ ��
                    s[146] = b.OutComeHour.ToString();//­�����˻��߻���ʱ�䣺��Ժǰ Сʱ
                    s[147] = b.OutComeMin.ToString();//­�����˻��߻���ʱ�䣺��Ժǰ ����
                    s[148] = (b.OutComeDay * 24 * 60 + b.OutComeHour * 60 + b.OutComeMin).ToString();//��Ժǰ�����ܷ���(�졢Сʱ����ɷ���)
                    s[149] = b.InComeDay.ToString();//­�����˻��߻���ʱ�䣺��Ժ�� ��
                    s[150] = b.InComeHour.ToString();//­�����˻��߻���ʱ�䣺��Ժ�� Сʱ
                    s[151] = b.InComeMin.ToString();//­�����˻��߻���ʱ�䣺��Ժ�� ����
                    s[152] = (b.InComeDay * 24 * 60 + b.InComeHour * 60 + b.InComeMin).ToString();//��Ժ������ܷ���
                    if (b.PatientInfo.Pact.ID == "9")
                    {
                        s[153] = "99";//���ʽ���
                    }
                    else
                    {
                        s[153] = b.PatientInfo.Pact.ID.PadLeft(2, '0');//���ʽ���
                    }
                    //s[153] = b.PatientInfo.Pact.ID;//���ʽ���
                    s[154] = this.constMana.GetConstant("CASEPACT", b.PatientInfo.Pact.ID).Name;//���ʽ

                    if (Feeds == null || Feeds.Tables.Count == 0 || Feeds.Tables[0].Rows.Count == 0)
                    {
                        s[155] = "0.00";//סԺ�ܷ��ã��Էѽ��
                        s[156] = "0.00";//�ۺ�ҽ�Ʒ����ࣺ��1��һ��ҽ�Ʒ����
                        s[157] = "0.00";//�ۺ�ҽ�Ʒ����ࣺ��2��һ�����Ʋ�����
                        s[158] = "0.00";//�ۺ�ҽ�Ʒ����ࣺ��3�������
                        s[159] = "0.00";//�ۺ�ҽ�Ʒ����ࣺ��4����������
                        s[160] = "0.00";//����ࣺ(5) ������Ϸ�
                        s[161] = "0.00";//����ࣺ(6) ʵ������Ϸ�
                        s[162] = "0.00";//����ࣺ(7) Ӱ��ѧ��Ϸ�
                        s[163] = "0.00";//����ࣺ(8) �ٴ������Ŀ��
                        s[164] = "0.00";//�����ࣺ(9) ������������Ŀ��
                        s[165] = "0.00";//�����ࣺ������������Ŀ�� �����ٴ��������Ʒ�
                        s[166] = "0.00";//�����ࣺ(10) �������Ʒ�
                        s[167] = "0.00";//�����ࣺ�������Ʒ� ���������
                        s[168] = "0.00";//�����ࣺ�������Ʒ� ����������
                        s[169] = "0.00";//�����ࣺ(11) ������
                        s[170] = "0.00";//��ҽ�ࣺ��ҽ������
                        s[171] = "0.00";//��ҩ�ࣺ ��ҩ�� ���п���ҩ�����
                        s[172] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ Ѫ��
                        s[173] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ �׵�������Ʒ��
                        s[174] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ �򵰰���Ʒ��
                        s[175] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ��Ѫ��������Ʒ��
                        s[176] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ ϸ���������
                        s[177] = "0.00";//�Ĳ��ࣺ�����һ����ҽ�ò��Ϸ�
                        s[178] = "0.00";//�Ĳ��ࣺ������һ����ҽ�ò��Ϸ�
                        s[179] = "0.00";//�Ĳ��ࣺ������һ����ҽ�ò��Ϸ�
                        s[180] = "0.00";//�ۺ�ҽ�Ʒ����ࣺһ��ҽ�Ʒ���� ������ҽ��֤���ηѣ���ҽ��
                        s[181] = "0.00";//�ۺ�ҽ�Ʒ����ࣺһ��ҽ�Ʒ���� ������ҽ��֤���λ���ѣ���ҽ��
                        s[182] = "0.00";//��ҽ�ࣺ��ϣ���ҽ��
                        s[183] = "0.00";//��ҽ�ࣺ���ƣ���ҽ��
                        s[184] = "0.00";//��ҽ�ࣺ���� �������Σ���ҽ��
                        s[185] = "0.00";//��ҽ�ࣺ���� ���й��ˣ���ҽ��
                        s[186] = "0.00";//��ҽ�ࣺ���� ���������ķ�����ҽ��
                        s[187] = "0.00";//��ҽ�ࣺ�����������ƣ���ҽ��
                        s[188] = "0.00";//��ҽ�ࣺ���� ���иس����ƣ���ҽ��
                        s[189] = "0.00";//��ҽ�ࣺ���� �����������ƣ���ҽ��
                        s[190] = "0.00";//��ҽ�ࣺ��������ҽ��
                        s[191] = "0.00";//��ҽ�ࣺ���� ������ҩ�������ӹ�����ҽ��
                        s[192] = "0.00";//��ҽ�ࣺ���� ���б�֤ʩ�ţ���ҽ��
                        s[193] = "0.00";//��ҩ�ࣺ�г�ҩ�� ����ҽ�ƻ�����ҩ�Ƽ��ѣ���ҽ��
                        //s[194] = "0.00";//��ҽ�ࣺ���� ���й��ˣ���ҽ��
                        //s[195] = "0.00";//��ҽ�ࣺ���� ���������ķ�����ҽ��
                        //s[196] = "0.00";//��ҽ�ࣺ�����������ƣ���ҽ��
                        //s[197] = "0.00";//��ҽ�ࣺ���� ���иس����ƣ���ҽ��
                        //s[198] = "0.00";//��ҽ�ࣺ���� �����������ƣ���ҽ��
                        //s[199] = "0.00";//��ҽ�ࣺ��������ҽ��
                        //s[200] = "0.00";//��ҽ�ࣺ���� ������ҩ�������ӹ�����ҽ��
                        //s[201] = "0.00";//��ҽ�ࣺ���� ���б�֤ʩ�ţ���ҽ��
                        //s[202] = "0.00";//��ҩ�ࣺ�г�ҩ�� ����ҽ�ƻ�����ҩ�Ƽ��ѣ���ҽ��
                    }
                    else
                    {
                        s[155] = Feeds.Tables[0].Rows[0][1].ToString();//�Ը����
                        s[156] = Feeds.Tables[0].Rows[0][2].ToString();//�ۺ�ҽ�Ʒ����ࣺ��1��һ��ҽ�Ʒ����
                        s[157] = Feeds.Tables[0].Rows[0][3].ToString();//�ۺ�ҽ�Ʒ����ࣺ��2��һ�����Ʋ�����
                        s[158] = Feeds.Tables[0].Rows[0][4].ToString();//�ۺ�ҽ�Ʒ����ࣺ��3�������
                        s[159] = Feeds.Tables[0].Rows[0][5].ToString();//�ۺ�ҽ�Ʒ����ࣺ��4����������
                        s[160] = Feeds.Tables[0].Rows[0][6].ToString();//����ࣺ(5) ������Ϸ�
                        s[161] = Feeds.Tables[0].Rows[0][7].ToString();//����ࣺ(6) ʵ������Ϸ�
                        s[162] = Feeds.Tables[0].Rows[0][8].ToString();//����ࣺ(7) Ӱ��ѧ��Ϸ�
                        s[163] = Feeds.Tables[0].Rows[0][9].ToString();//����ࣺ(8) �ٴ������Ŀ��
                        s[164] = Feeds.Tables[0].Rows[0][10].ToString();//�����ࣺ(9) ������������Ŀ��
                        s[165] = Feeds.Tables[0].Rows[0][11].ToString();//�����ࣺ������������Ŀ�� �����ٴ��������Ʒ�
                        s[166] = Feeds.Tables[0].Rows[0][12].ToString();//�����ࣺ(10) �������Ʒ�
                        s[167] = Feeds.Tables[0].Rows[0][13].ToString();//�����ࣺ�������Ʒ� ���������
                        s[168] = Feeds.Tables[0].Rows[0][14].ToString();//�����ࣺ�������Ʒ� ����������
                        s[169] = Feeds.Tables[0].Rows[0][15].ToString();//�����ࣺ(11) ������
                        s[170] = Feeds.Tables[0].Rows[0][16].ToString();//��ҽ�ࣺ��ҽ������
                        s[171] = Feeds.Tables[0].Rows[0][18].ToString();//��ҩ�ࣺ ��ҩ�� ���п���ҩ�����
                        s[172] = Feeds.Tables[0].Rows[0][21].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ Ѫ��
                        s[173] = Feeds.Tables[0].Rows[0][22].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ �׵�������Ʒ��
                        s[174] = Feeds.Tables[0].Rows[0][23].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ �򵰰���Ʒ��
                        s[175] = Feeds.Tables[0].Rows[0][24].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ��Ѫ��������Ʒ��
                        s[176] = Feeds.Tables[0].Rows[0][25].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ ϸ���������
                        s[177] = Feeds.Tables[0].Rows[0][26].ToString();//�Ĳ��ࣺ�����һ����ҽ�ò��Ϸ�
                        s[178] = Feeds.Tables[0].Rows[0][27].ToString();//�Ĳ��ࣺ������һ����ҽ�ò��Ϸ�
                        s[179] = Feeds.Tables[0].Rows[0][28].ToString();//�Ĳ��ࣺ������һ����ҽ�ò��Ϸ�
                        s[180] = "0.00";//�ۺ�ҽ�Ʒ����ࣺһ��ҽ�Ʒ���� ������ҽ��֤���ηѣ���ҽ��
                        s[181] = "0.00";//�ۺ�ҽ�Ʒ����ࣺһ��ҽ�Ʒ���� ������ҽ��֤���λ���ѣ���ҽ��
                        s[182] = "0.00";//��ҽ�ࣺ��ϣ���ҽ��
                        s[183] = "0.00";//��ҽ�ࣺ���ƣ���ҽ��
                        s[184] = "0.00";//��ҽ�ࣺ���� �������Σ���ҽ��
                        s[185] = "0.00";//��ҽ�ࣺ���� ���й��ˣ���ҽ��
                        s[186] = "0.00";//��ҽ�ࣺ���� ���������ķ�����ҽ��
                        s[187] = "0.00";//��ҽ�ࣺ�����������ƣ���ҽ��
                        s[188] = "0.00";//��ҽ�ࣺ���� ���иس����ƣ���ҽ��
                        s[189] = "0.00";//��ҽ�ࣺ���� �����������ƣ���ҽ��
                        s[190] = "0.00";//��ҽ�ࣺ��������ҽ��
                        s[191] = "0.00";//��ҽ�ࣺ���� ������ҩ�������ӹ�����ҽ��
                        s[192] = "0.00";//��ҽ�ࣺ���� ���б�֤ʩ�ţ���ҽ��
                        s[193] = "0.00";//��ҩ�ࣺ�г�ҩ�� ����ҽ�ƻ�����ҩ�Ƽ��ѣ���ҽ��
                        //s[194] = "0.00";//��ҽ�ࣺ���� ���й��ˣ���ҽ��
                        //s[195] = "0.00";//��ҽ�ࣺ���� ���������ķ�����ҽ��
                        //s[196] = "0.00";//��ҽ�ࣺ�����������ƣ���ҽ��
                        //s[197] = "0.00";//��ҽ�ࣺ���� ���иس����ƣ���ҽ��
                        //s[198] = "0.00";//��ҽ�ࣺ���� �����������ƣ���ҽ��
                        //s[199] = "0.00";//��ҽ�ࣺ��������ҽ��
                        //s[200] = "0.00";//��ҽ�ࣺ���� ������ҩ�������ӹ�����ҽ��
                        //s[201] = "0.00";//��ҽ�ࣺ���� ���б�֤ʩ�ţ���ҽ��
                        //s[202] = "0.00";//��ҩ�ࣺ�г�ҩ�� ����ҽ�ƻ�����ҩ�Ƽ��ѣ���ҽ��
                    }
                    return s;
                }
                catch (Exception ex)
                {
                    this.Err = ex.ToString();
                    return null;
                }
                #endregion
            }
            else //˳�¸��״�������Ϣ 2012-12-27
            {
                #region
                string[] s = new string[194];
                try
                {
                    s[0] = "0";//�Ƿ����룬0����1���ǣ�Ĭ�Ͻ���Ϊ0
                    //s[1] = this.PatientNoChang(b.PatientInfo.PID.PatientNO.TrimStart(new char[] { '0' }));//������
                    string patientNO = b.PatientInfo.PID.PatientNO.PadLeft(10, '0');
                    s[1] = this.PatientNoChang(patientNO.Substring(this.PatientNoSubstr()));//������
                    s[2] = b.PatientInfo.InTimes.ToString();//סԺ����
                    s[3] = "11";//ICD�汾��9��ICD9�ֵ�⣬10��ICD10�ֵ�⣬����չ11������ICD�⣬Ĭ�Ͻ���Ϊ11
                    s[4] = b.PatientInfo.ID;//סԺ��ˮ��
                    #region //s[5] ����
                    if (b.PatientInfo.Age != "" && b.PatientInfo.Age != "0")
                    {
                        if (b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") < 0) //����
                        {
                            s[5] = "Y" + b.AgeUnit.Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") < 0 && b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") < 0)//����
                        {
                            s[5] = "M" + b.AgeUnit.Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") < 0 && b.AgeUnit.IndexOf("��") < 0 && b.AgeUnit.IndexOf("��") > 0)//����
                        {
                            s[5] = "D" + b.AgeUnit.Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") < 0)//N��N��
                        {
                            string[] PAge = b.AgeUnit.Split('��');
                            s[5] = "Y" + PAge[0] + "M" + PAge[1].Replace("��", "").Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") < 0 && b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") > 0)//N��N��
                        {
                            string[] PAge = b.AgeUnit.Split('��');
                            s[5] = "M" + PAge[0] + "D" + PAge[1].Replace("��", "").Replace("��", "");
                        }
                        else if (b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") > 0 && b.AgeUnit.IndexOf("��") > 0)//N��N��N��
                        {
                            string[] PAge = b.AgeUnit.Split('��');

                            string[] PAge1 = PAge[1].Split('��');
                            s[5] = "Y" + PAge[0] + "M" + PAge1[0] + "D" + PAge1[1].Replace("��", "").Replace("��", "");
                        }
                    }
                    else
                    {
                        int ts = b.PatientInfo.PVisit.InTime.Year - b.PatientInfo.Birthday.Year;

                        if (ts == 0)
                        {
                            ts = b.PatientInfo.PVisit.InTime.Month - b.PatientInfo.Birthday.Month;

                            if (ts == 0)
                            {
                                ts = b.PatientInfo.PVisit.InTime.Day - b.PatientInfo.Birthday.Day;
                                s[5] = "D" + ts.ToString();//���� 
                            }
                            else
                            {
                                s[5] = "M" + ts.ToString();//���� 
                            }
                        }
                        else
                        {
                            s[5] = "Y" + ts.ToString();//���� 
                        }
                    }
                    #endregion

                    s[6] = b.PatientInfo.Name;//��������
                    //�Ա���
                    //�Ա�
                    if (b.PatientInfo.Sex.ID.ToString() == "M" || b.PatientInfo.Sex.ID.ToString() == "1")
                    {
                        s[7] = "1";
                        s[8] = "��";
                    }
                    else
                    {
                        s[7] = "2";
                        s[8] = "Ů";
                    }
                    s[9] = b.PatientInfo.Birthday.ToShortDateString().Replace('-', '/');//��������
                    s[10] = b.PatientInfo.AddressHome; //������
                    if (b.PatientInfo.IDCard == "" || b.PatientInfo.IDCard.Trim() == "-")
                    {
                        s[11] = "����";
                    }
                    else
                    {
                        s[11] = b.PatientInfo.IDCard;//���֤��
                    }
                    #region s[12]�������\ s[13]����  �й�  ��Ҫת������
                    if (b.PatientInfo.Country.ID.ToString() == "1")
                    {
                        s[12] = "A156";
                        s[13] = "�й�";
                    }
                    else
                    {
                        FS.FrameWork.Models.NeuObject countryObj = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.COUNTRY, b.PatientInfo.Country.ID.ToString());
                        if (countryObj != null && countryObj.ID != "")
                        {
                            if (countryObj.Memo != ""&&countryObj.Memo.ToUpper()!="TRUE")
                            {
                                s[12] = countryObj.Memo.ToString();
                                s[13] = countryObj.Name.ToString();
                            }
                            else
                            {
                                s[12] = countryObj.ID.ToString();
                                s[13] = countryObj.Name.ToString();
                            }
                        }
                        else
                        {
                            s[12] = b.PatientInfo.Country.ID.ToString();
                            s[13] = "";
                        }
                    }
                    #endregion
                    #region s[14]������ s[15]����
                    FS.FrameWork.Models.NeuObject NationObj = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.NATION, b.PatientInfo.Nationality.ID.ToString());
                    if (NationObj != null && NationObj.ID != "")
                    {
                        if (NationObj.Memo != ""&&NationObj.Memo.ToUpper()!="TRUE")
                        {
                            s[14] = NationObj.Memo;
                            s[15] = NationObj.Name;
                        }
                        else
                        {
                            s[14] = NationObj.ID;
                            s[15] = NationObj.Name;
                        }
                    }
                    else
                    {
                        s[14] = b.PatientInfo.Nationality.ID;
                        s[15] = "";
                    }
                    #endregion
                    #region  s[16] ְҵ�����Ĵ�����
                    //add by chengym 2011-6-15  �ֵ��������ֶ�varchar��100�� ��Щְҵ��������25�������ַ�����ʱ��ȡ��ע���������ƣ���֤�ϴ�������û�����⣻ 
                    FS.FrameWork.Models.NeuObject JobObj = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.CASEPROFESSION, b.PatientInfo.Profession.ID.ToString());
                    if (JobObj != null && JobObj.ID != "")
                    {
                        if (JobObj.Memo != "" && JobObj.Memo.ToUpper() != "TRUE")
                        {
                            if (JobObj.Memo.Length <= 50)
                            {
                                s[16] = JobObj.Memo;
                            }
                            else
                            {
                                s[16] = JobObj.Memo.Substring(0, 50);
                            }
                        }
                        else
                        {
                            if (JobObj.Name.Length <= 50)
                            {
                                s[16] = JobObj.Name;
                            }
                            else
                            {
                                s[16] = JobObj.Name.Substring(0, 50);
                            }
                        }
                    }
                    else
                    {
                        s[16] = b.PatientInfo.Profession.ID.ToString(); //ְҵ û�д����Ĳ�֪���Ƿ����
                    }
                    #endregion
                    #region s[17] ����״����� s[18]����״��
                    if (b.PatientInfo.MaritalStatus.ID.ToString() == "S" || b.PatientInfo.MaritalStatus.ID.ToString() == "1"
                        || b.PatientInfo.MaritalStatus.ID.ToString() == "10")
                    {
                        s[17] = "10"; //����״�����
                        s[18] = "δ��"; //����״��
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "M" || b.PatientInfo.MaritalStatus.ID.ToString() == "2"
                        || b.PatientInfo.MaritalStatus.ID.ToString() == "20")
                    {
                        s[17] = "20";
                        s[18] = "�ѻ�";
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "W" || b.PatientInfo.MaritalStatus.ID.ToString() == "3"
                        || b.PatientInfo.MaritalStatus.ID.ToString() == "30")
                    {
                        s[17] = "30";
                        s[18] = "ɥż";
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "A")
                    {
                        s[17] = "20";
                        s[18] = "�ѻ�";
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "D" || b.PatientInfo.MaritalStatus.ID.ToString() == "4"
                        || b.PatientInfo.MaritalStatus.ID.ToString() == "40")
                    {
                        s[17] = "40";
                        s[18] = "���";
                    }
                    else if (b.PatientInfo.MaritalStatus.ID.ToString() == "R")
                    {
                        s[17] = "20";
                        s[18] = "�ѻ�";
                    }
                    else
                    {
                        s[17] = "90";
                        s[18] = "δ˵���Ļ���״��";
                    }
                    #endregion
                    if (b.PatientInfo.HomeZip != null && b.PatientInfo.HomeZip != "")
                    {
                        b.PatientInfo.BusinessZip=b.PatientInfo.HomeZip;
                        b.CurrentZip = b.PatientInfo.HomeZip;
                    }
                    else if (b.PatientInfo.BusinessZip != null && b.PatientInfo.BusinessZip != "")
                    {
                        b.PatientInfo.HomeZip = b.PatientInfo.BusinessZip;
                        b.CurrentZip = b.PatientInfo.BusinessZip;
                    }
                    else if (b.CurrentZip != null && b.CurrentZip != "")
                    {
                        b.PatientInfo.HomeZip = b.CurrentZip;
                        b.PatientInfo.BusinessZip = b.CurrentZip;
                    }
                    if (b.PatientInfo.AddressHome != null && b.PatientInfo.AddressHome != "")
                    {
                        b.CurrentAddr = b.PatientInfo.AddressHome;
                    }
                    s[19] = b.PatientInfo.AddressBusiness;  //��λ����
                    s[20] = b.PatientInfo.CompanyName;//��λ��ַ 
                    s[21] = b.PatientInfo.PhoneBusiness;//��λ�绰
                    s[22] = b.PatientInfo.BusinessZip;//��λ�ʱ�      
                    s[23] = b.PatientInfo.AddressHome;//���ڵ�ַ
                    s[24] = b.PatientInfo.HomeZip;//�����ʱ�
                    s[25] = b.PatientInfo.Kin.Name;//��ϵ��
                    #region s[26] �벡�˹�ϵ
                    FS.FrameWork.Models.NeuObject RelativeObj = this.constMana.GetConstant("RELATIVE", b.PatientInfo.Kin.Relation.ID);
                    if (RelativeObj != null && RelativeObj.ID != "")
                    {
                        if (RelativeObj.Memo != "" &&RelativeObj.Memo.ToUpper()!="TRUE")
                        {
                            if (RelativeObj.Memo.Length <= 10)
                            {
                                s[26] = RelativeObj.Memo;//�뻼�߹�ϵ
                            }
                            else
                            {
                                s[26] = RelativeObj.Memo.Substring(0, 10);//�뻼�߹�ϵ
                            }
                        }
                        else
                        {
                            if (RelativeObj.Name.Length <= 10)
                            {
                                s[26] = RelativeObj.Name;//�뻼�߹�ϵ
                            }
                            else
                            {
                                s[26] = RelativeObj.Name.Substring(0, 10);//�뻼�߹�ϵ
                            }
                        }
                    }
                    else
                    {
                        s[26] = b.PatientInfo.Kin.RelationLink;//�뻼�߹�ϵ
                    }
                    #endregion
                    s[27] = b.PatientInfo.Kin.RelationAddress;//��ϵ�˵�ַ
                    s[28] = b.PatientInfo.Kin.RelationPhone;//��ϵ�˵绰
                    s[29] = b.PatientInfo.SSN; // ԭ3.0��ҽ������
                    s[30] = b.PatientInfo.PVisit.InTime.ToShortDateString().Replace('-', '/');//��Ժ����
                    s[31] = b.PatientInfo.PVisit.InTime.Hour.ToString().PadLeft(2, '0'); //��Ժʱ��
                    FS.HISFC.Models.RADT.Location indept = this.baseDml.GetDeptIn(b.PatientInfo.ID);
                    if (indept != null) //��Ժ���� 
                    {
                        s[32] = this.ConverDept(indept.Dept.ID);//��Ժ���Ҵ���
                        s[33] = this.ConverDeptName(indept.Dept.ID, indept.Dept.Name);//��Ժ��������
                    }
                    else
                    {
                        s[32] = this.ConverDept(b.PatientInfo.PVisit.PatientLocation.Dept.ID);//��Ժ���Ҵ���
                        s[33] = this.ConverDeptName(b.PatientInfo.PVisit.PatientLocation.Dept.ID, b.PatientInfo.PVisit.PatientLocation.Dept.Name);//��Ժ��������
                    }
                    s[34] = b.InRoom;//��Ժ����    
                    s[35] = b.PatientInfo.PVisit.OutTime.ToShortDateString().Replace('-', '/');//��Ժ����
                    s[36] = b.PatientInfo.PVisit.OutTime.Hour.ToString().PadLeft(2, '0'); //��Ժʱ��
                    s[37] = this.ConverDept(b.PatientInfo.PVisit.PatientLocation.Dept.ID);//��Ժ���Ҵ���
                    s[38] = this.ConverDeptName(b.PatientInfo.PVisit.PatientLocation.Dept.ID, b.PatientInfo.PVisit.PatientLocation.Dept.Name);//��Ժ��������2011-6-8
                    s[39] = b.OutRoom; //��Ժ����
                    System.TimeSpan tt = b.PatientInfo.PVisit.OutTime - b.PatientInfo.PVisit.InTime;
                    s[40] = tt.Days.ToString();//ʵ��סԺ����
                    s[41] = b.ClinicDiag.ID;//�ţ����������(ICD10��ICD9)����

                    if (b.ClinicDiag.Name.Length > 50)//�ţ����������(ICD10��ICD9)��Ӧ������
                    {
                        s[42] = this.ChangeCharacter(b.ClinicDiag.Name.Substring(0, 50).ToString());
                    }
                    else
                    {
                        s[42] = this.ChangeCharacter(b.ClinicDiag.Name);
                    }
                    s[43] = this.ConverDoc(b.ClinicDoc.ID);//�š�����ҽ����ţ���Ӧtdoctor �е�ftygh
                    s[44] = b.ClinicDoc.Name;//�š�����ҽ��
                    //�������
                    if (b.PathologicalDiagCode == null)
                    {
                        s[45] = b.PathologicalDiagName;
                    }
                    else
                    {
                        s[45] = b.PathologicalDiagCode;
                    }
                    //����ҩ��
                    string anaphyPh = b.FirstAnaphyPharmacy.ID;
                    if (anaphyPh.Length > 100)
                    {
                        s[46] = this.ChangeCharacter(anaphyPh.Substring(0, 100));
                    }
                    else
                    {
                        s[46] = this.ChangeCharacter(anaphyPh);//ҩ�����  
                    }
                    //s[46] = "1";
                    //�������Ժ��Ϸ���������
                    if (b.CePi == null || b.CePi == "")
                    {
                        s[47] = "1";
                        s[48] = "����";
                    }
                    else
                    {
                        s[47] = b.CePi;
                        s[48] = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.ACCORDSTAT, b.CePi).Name;
                    }
                    //�ٴ��벡����Ϸ������
                    if (b.ClPa == null || b.ClPa == "")
                    {
                        s[49] = "1";
                        s[50] = "����";
                    }
                    else
                    {
                        s[49] = b.ClPa;
                        s[50] = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.ACCORDSTAT, b.ClPa).Name;
                    }
                    s[51] = b.SalvTimes.ToString();//���ȴ���
                    s[52] = b.SuccTimes.ToString();//�ɹ�����

                    s[53] = this.ConverDoc(b.PatientInfo.PVisit.ReferringDoctor.ID);//�����α�ţ���Ӧtdoctor �е�ftygh
                    s[54] = b.PatientInfo.PVisit.ReferringDoctor.Name;//����������
                    s[55] = this.ConverDoc(b.PatientInfo.PVisit.ConsultingDoctor.ID);//������������ҽ����ţ���Ӧtdoctor �е�ftygh
                    s[56] = b.PatientInfo.PVisit.ConsultingDoctor.Name;//����ҽʦ����
                    s[57] = this.ConverDoc(b.PatientInfo.PVisit.AttendingDoctor.ID);//����ҽ����ţ���Ӧtdoctor �е�ftygh
                    s[58] = b.PatientInfo.PVisit.AttendingDoctor.Name;//����ҽʦ����
                    s[59] = this.ConverDoc(b.PatientInfo.PVisit.AdmittingDoctor.ID);//סԺҽ����ţ���Ӧtdoctor �е�ftygh
                    s[60] = b.PatientInfo.PVisit.AdmittingDoctor.Name;//סԺҽʦ����
                    s[61] = this.ConverDoc(b.RefresherDoc.ID);//����ҽʦ��ţ���Ӧtdoctor �е�ftygh
                    s[62] = b.RefresherDoc.Name;//����ҽ��
                    s[63] = this.ConverDoc(b.PatientInfo.PVisit.TempDoctor.ID);//ʵϰҽ����ţ���Ӧtdoctor �е�ftygh
                    s[64] = b.PatientInfo.PVisit.TempDoctor.Name;//ʵϰҽʦ����
                    s[65] = this.ConverDoc(b.CodingOper.ID);//����Ա���
                    s[66] = b.CodingOper.Name;//����Ա����
                    s[67] = this.ConverDoc(b.OperInfo.ID);//���������߱��
                    s[68] = b.OperInfo.Name;//����Ա���ƣ����������ߣ�
                    if (b.MrQuality == null || b.MrQuality == "")
                    {
                        s[69] = "1";
                    }
                    else
                    {
                        s[69] = b.MrQuality;//�������� 
                    }
                    s[70] = this.constMana.GetConstant("CASEQUALITY", b.MrQuality).Name;
                    s[71] = this.ConverDoc(b.QcDoc.ID);//�ʿ�ҽʦ����
                    s[72] = b.QcDoc.Name;//�ʿ�ҽʦ
                    s[73] = this.ConverDoc(b.QcNurse.ID);
                    s[74] = b.QcNurse.Name;//�ʿػ�ʿ����
                    //�ʿ�����
                    if (b.CheckDate < new DateTime(1900, 1, 1))
                    {
                        s[75] = b.PatientInfo.PVisit.OutTime.ToShortDateString().Replace('-', '/');
                    }
                    else if (b.CheckDate <= b.PatientInfo.PVisit.OutTime)//�ʿ����ڲ�����С�ڳ�Ժ����
                    {
                        s[75] = b.PatientInfo.PVisit.OutTime.ToShortDateString().Replace('-', '/');
                    }
                    else
                    {
                        s[75] = b.CheckDate.ToShortDateString().Replace('-', '/');//�ʿ�����
                    }
                    #region ���� �ܷ���s[76] ��ҩ��s[77] ��ҩ��s[78] �г�ҩ��s[79] �в�ҩ��s[80] ������s[81]
                    if (Feeds == null || Feeds.Tables.Count == 0 || Feeds.Tables[0].Rows.Count == 0)
                    {
                        s[76] = "0.00";//�ܷ���
                        s[77] = "0.00";//��ҩ��
                        s[78] = "0.00";//��ҩ��
                        s[79] = "0.00";//�г�ҩ��
                        s[80] = "0.00";//�в�ҩ��
                        s[81] = "0.00";//������
                    }
                    else
                    {
                        s[76] = Feeds.Tables[0].Rows[0][0].ToString();//�ܷ���
                        s[77] = Feeds.Tables[0].Rows[0][17].ToString();//��ҩ��
                        s[78] = "0.00";//��ҩ��
                        s[79] = Feeds.Tables[0].Rows[0][19].ToString();//�г�ҩ��
                        s[80] = Feeds.Tables[0].Rows[0][20].ToString();//�в�ҩ��
                        s[81] = Feeds.Tables[0].Rows[0][29].ToString();//������
                    }
                    #endregion
                    //�Ƿ�ʬ����1���� 2����
                    if (b.CadaverCheck == "1")
                    {
                        s[82] = "1";
                        s[83] = "��";
                    }
                    else if (b.CadaverCheck == "2")
                    {
                        s[82] = "2";
                        s[83] = "��";
                    }
                    else
                    {
                        s[82] = "2";
                        s[83] = "��";
                    }
                    //s[83] = this.constMana.GetConstant("CASEYSEORNO", b.CadaverCheck).Name;
                    #region s[84]Ѫ�ͱ�� s[85]Ѫ��
                    if (b.PatientInfo.BloodType.ID.ToString() == "A")
                    {
                        s[84] = "1";
                        s[85] = b.PatientInfo.BloodType.ID.ToString();
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString() == "B")
                    {
                        s[84] = "2";
                        s[85] = b.PatientInfo.BloodType.ID.ToString();
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString() == "AB")
                    {
                        s[84] = "4";
                        s[85] = b.PatientInfo.BloodType.ID.ToString();
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString() == "O")
                    {
                        s[84] = "3";
                        s[85] = b.PatientInfo.BloodType.ID.ToString();
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString() == "9")
                    {
                        s[84] = "6";
                        s[85] = "δ��";
                    }
                    else if (b.PatientInfo.BloodType.ID.ToString() == "6")
                    {
                        s[84] = "6";
                        s[85] = "δ��";
                    }
                    else
                    {
                        s[84] = "6";
                        s[85] = "δ��";
                    }
                    #endregion
                    //s[86] = b.RhBlood;//RH���
                    //RH
                    //if (b.RhBlood == "1")
                    //{
                    //    s[86] = "1";
                    //    s[87] = "��";
                    //}
                    //else if (b.RhBlood == "2")
                    //{
                    //    s[86] = "2";
                    //    s[87] = "��";
                    //}
                    //else if (b.RhBlood == "3")
                    //{
                    //    s[86] = "3";
                    //    s[87] = "����";
                    //}
                    //else
                    //{
                    //    //s[86] = "4";
                    //    //s[87] = "δ��";
                        s[86] = "2";
                        s[87] = "��";
                    //}
                    //Ӥ����
                    int babyNum = 0;
                    try
                    {
                        babyNum = FS.FrameWork.Function.NConvert.ToInt32(b.PatientInfo.User03);
                        s[88] = babyNum.ToString();
                    }
                    catch
                    {
                        s[88] = "0";
                    }
                    s[89] = "0";//�Ƿ񲿷ֲ��֣�1�� 0��
                    #region  s[90]�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ� s[91]�״�ת�ƿƱ�  s[92]�״�ת������  s[93]�״�ת��ʱ��
                    if (alChangeDepe != null && alChangeDepe.Count > 0)
                    {
                        FS.HISFC.Models.RADT.Location changeDept = alChangeDepe[0] as FS.HISFC.Models.RADT.Location;
                        if (changeDept.Dept.ID != null && changeDept.Dept.ID != "")
                        {
                            try
                            {
                                s[90] = this.ConverDept(changeDept.Dept.ID);//�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                                s[91] = this.ConverDeptName(changeDept.Dept.ID, changeDept.Dept.Name);//�״�ת�ƿƱ�
                                s[92] = FS.FrameWork.Function.NConvert.ToDateTime(changeDept.Dept.Memo).ToShortDateString();//�״�ת������
                                s[93] = FS.FrameWork.Function.NConvert.ToDateTime(changeDept.Dept.Memo).Hour.ToString().PadLeft(2, '0');//�״�ת��ʱ��
                            }
                            catch
                            {
                                s[90] = "";//�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                                s[91] = "";//�״�ת�ƿƱ�
                                s[92] = "";//�״�ת������
                                s[93] = "";//�״�ת��ʱ��
                            }
                        }
                        else
                        {
                            s[90] = "";//�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                            s[91] = "";//�״�ת�ƿƱ�
                            s[92] = "";//�״�ת������
                            s[93] = "";//�״�ת��ʱ��
                        }
                    }
                    else
                    {
                        s[90] = "";//�״�ת��ͳһ�ƺţ�HIS����ʱ�洢HIS�ƺ�
                        s[91] = "";//�״�ת�ƿƱ�
                        s[92] = "";//�״�ת������
                        s[93] = "";//�״�ת��ʱ��
                    }
                    #endregion
                    s[94] = this.ConverDoc(b.OperInfo.ID);//����Ա���
                    s[95] = b.OperInfo.Name;//����Ա
                    s[96] = System.DateTime.Now.ToShortDateString();//��������FS.FrameWork.Function.NConvert.ToDateTime(((FS.FrameWork.Models.RADT.Location)alChangeDepe[0]).User01).ToShortDateString().Replace('-', '/');
                    //FS.FrameWork.Models.NeuObject CaseExaplTypeObj = this.constMana.GetConstant("CASEEXAMPLETYPE", b.ExampleType);
                    //if (CaseExaplTypeObj != null && CaseExaplTypeObj.ID != "")
                    //{
                    //    if (CaseExaplTypeObj.Memo != "" && CaseExaplTypeObj.Memo.ToUpper() != "TRUE")
                    //    {
                    //        s[97] = CaseExaplTypeObj.Memo;//�������ͱ��
                    //        s[98] = b.ExampleType;//��������
                    //    }
                    //}
                    //else
                    //{
                    //    switch (b.ExampleType)//�������ͱ��
                    //    {
                    //        case "A":
                    //            s[97] = "1";
                    //            break;
                    //        case "B":
                    //            s[97] = "2";
                    //            break;
                    //        case "C":
                    //            s[97] = "3";
                    //            break;
                    //        case "D":
                    //            s[97] = "4";
                    //            break;
                    //    }
                    //    s[98] = b.ExampleType;//��������
                    //}
                    s[97] = "1";
                    s[98] = "һ��";//��������

                    s[99] = "";//���Ϲ鵵���
                    s[100] = "";//���Ϲ鵵
                    s[101] = b.PatientInfo.PVisit.InSource.ID;//������Դ���
                    s[102] = this.constMana.GetConstant("INAVENUE", b.PatientInfo.PVisit.InSource.ID).Name;//������Դ
                    s[103] = b.PatientInfo.User02;//�Ƿ����� chengym
                    if (b.PatientInfo.User03 == "0")
                    {
                        s[104] = "0";//�Ƿ����븾Ӥ��
                    }
                    else
                    {
                        s[104] = "1";//�Ƿ����븾Ӥ��
                    }
                    //Ժ�д�������ԴԺ�б��� 12-8-28
                    try
                    {
                        int infNum = this.baseDml.QueryInfCount(b.PatientInfo.ID);
                        if (infNum == -1)
                        {
                            s[105] = "0";//ҽԺ��Ⱦ������������Ϊ�գ�����Ӱ�챨��ͳ�ƽ�� chengym
                        }
                        else
                        {
                            s[105] = infNum.ToString();
                        }
                    }
                    catch
                    {
                        s[105] = "0";
                    }
                    s[106] = "";//��չ1 
                    s[107] = "";
                    s[108] = "";
                    s[109] = "";
                    s[110] = "";
                    s[111] = "";
                    s[112] = "";
                    s[113] = "";//��չ8������
                    s[114] = "";//��չ9��������
                    s[115] = "";
                    s[116] = "";
                    s[117] = "";
                    s[118] = "";
                    s[119] = "";
                    s[120] = "";//��չ15
                    s[121] = b.PatientInfo.DIST;//����
                    s[122] = b.CurrentAddr;//��סַ
                    s[123] = b.CurrentPhone;//�ֵ绰
                    s[124] = b.CurrentZip;//���ʱ�
                    s[125] = b.PatientInfo.Profession.ID;//ְҵ���
                    //���ܴ���ҽԺ��д����������
                    try
                    {
                        int bweight = FS.FrameWork.Function.NConvert.ToInt32(b.BabyBirthWeight);
                        b.BabyBirthWeight = bweight.ToString();
                    }
                    catch
                    {
                        b.BabyBirthWeight = "0";
                    }
                    s[126] = b.BabyBirthWeight;//��������������
                    //���ܴ���ҽԺ��д����������
                    try
                    {
                        int biweight = FS.FrameWork.Function.NConvert.ToInt32(b.BabyInWeight);
                        b.BabyInWeight = biweight.ToString();
                    }
                    catch
                    {
                        b.BabyInWeight = "0";
                    }
                    s[127] = b.BabyInWeight;//��������Ժ����
                    s[128] = b.InPath;//��Ժ;�����
                    s[129] = this.constMana.GetConstant("CASEINAVENUE", b.InPath).Name;//��Ժ;��
                    s[130] = b.ClinicPath;//�ٴ�·���������
                    if (b.ClinicPath == "1")
                    {
                        s[131] = "��";//�ٴ�·������
                    }
                    else
                    {
                        s[131] = "��";//�ٴ�·������
                    }
                    s[132] = "";//����������
                    s[133] = b.PathNum;//�����
                    //s[134] = b.AnaphyFlag;//�Ƿ�ҩ��������
                    //if (b.AnaphyFlag == "1")
                    //{
                    //    s[135] = "��";//�Ƿ�ҩ�����
                    //}
                    //else
                    //{
                    //    s[135] = "��";//�Ƿ�ҩ�����
                    //}
                    s[134] = "1";
                    s[135] = "��";
                    s[136] = this.ConverDoc(b.DutyNurse.ID);//���λ�ʿ���
                    s[137] = b.DutyNurse.Name;//���λ�ʿ
                    //s[138] = b.Out_Type;//��Ժ��ʽ���
                    //if (b.Out_Type == "1")//��Ժ��ʽ
                    //{
                    //    s[138] = "1";
                    //    s[139] = "ҽ����Ժ";
                    //}
                    //else if (b.Out_Type == "2")//��Ժ��ʽ
                    //{
                    //    s[138] = "2";
                    //    s[139] = "ҽ��תԺ";
                    //}
                    //else if (b.Out_Type == "3")//��Ժ��ʽ
                    //{
                    //    s[138] = "3";
                    //    s[139] = "ҽ��ת������������Ժ";
                    //}
                    //else if (b.Out_Type == "4")//��Ժ��ʽ
                    //{
                    //    s[138] = "4";
                    //    s[139] = "��ҽ����Ժ";
                    //}
                    //else if (b.Out_Type == "5")//��Ժ��ʽ
                    //{
                    //    s[138] = "5";
                    //    s[139] = "����";
                    //}
                    //else
                    //{
                    //    s[138] = "1";
                    //    s[139] = "ҽ����Ժ";
                    //}
                    s[138] = "1";
                    s[139] = "ҽ����Ժ";
                    s[140] = b.HighReceiveHopital;//��Ժ��ʽΪҽ��תԺ�������ҽ�ƻ�������
                    s[141] = b.LowerReceiveHopital;//��Ժ��ʽΪת������������������/��������Ժ�������ҽ�ƻ�������
                    //s[142] = b.ComeBackInMonth;//�Ƿ��г�Ժ31������סԺ�ƻ����
                    //s[143] = "";//�Ƿ��г�Ժ31������סԺ�ƻ�
                    s[142] = "1";//�Ƿ��г�Ժ31������סԺ�ƻ����
                    s[143] = "��";//�Ƿ��г�Ժ31������סԺ�ƻ�
                    s[144] = b.ComeBackPurpose;//��סԺĿ��
                    s[145] = b.OutComeDay.ToString();//­�����˻��߻���ʱ�䣺��Ժǰ ��
                    s[146] = b.OutComeHour.ToString();//­�����˻��߻���ʱ�䣺��Ժǰ Сʱ
                    s[147] = b.OutComeMin.ToString();//­�����˻��߻���ʱ�䣺��Ժǰ ����
                    s[148] = (b.OutComeDay * 24 * 60 + b.OutComeHour * 60 + b.OutComeMin).ToString();//��Ժǰ�����ܷ���(�졢Сʱ����ɷ���)
                    s[149] = b.InComeDay.ToString();//­�����˻��߻���ʱ�䣺��Ժ�� ��
                    s[150] = b.InComeHour.ToString();//­�����˻��߻���ʱ�䣺��Ժ�� Сʱ
                    s[151] = b.InComeMin.ToString();//­�����˻��߻���ʱ�䣺��Ժ�� ����
                    s[152] = (b.InComeDay * 24 * 60 + b.InComeHour * 60 + b.InComeMin).ToString();//��Ժ������ܷ���
                    FS.FrameWork.Models.NeuObject pactObj = this.constMana.GetConstant("CASEPACTCHANGE", b.PatientInfo.Pact.ID);
                    if (pactObj != null)
                    {
                        if (pactObj.Memo != "" && pactObj.Memo.ToUpper() != "TRUE")
                        {
                            if (pactObj.Memo == "9")
                            {
                                s[153] = "99";//���ʽ���
                            }
                            else
                            {
                                s[153] = pactObj.Memo.PadLeft(2, '0');//���ʽ���
                            }
                            s[154] = this.constMana.GetConstant("CASEPACT", pactObj.Memo).Name;//���ʽ
                        }
                        else
                        {
                            s[153] = b.PatientInfo.Pact.ID;
                            s[154] = this.constMana.GetConstant("CASEPACT", b.PatientInfo.Pact.ID).Name;//���ʽ
                        }
                    }
                    else
                    {
                        s[153] = b.PatientInfo.Pact.ID;
                        s[154] = this.constMana.GetConstant("CASEPACT", b.PatientInfo.Pact.ID).Name;//���ʽ
                    }

                    if (Feeds == null || Feeds.Tables.Count == 0 || Feeds.Tables[0].Rows.Count == 0)
                    {
                        s[155] = "0.00";//סԺ�ܷ��ã��Էѽ��
                        s[156] = "0.00";//�ۺ�ҽ�Ʒ����ࣺ��1��һ��ҽ�Ʒ����
                        s[157] = "0.00";//�ۺ�ҽ�Ʒ����ࣺ��2��һ�����Ʋ�����
                        s[158] = "0.00";//�ۺ�ҽ�Ʒ����ࣺ��3�������
                        s[159] = "0.00";//�ۺ�ҽ�Ʒ����ࣺ��4����������
                        s[160] = "0.00";//����ࣺ(5) ������Ϸ�
                        s[161] = "0.00";//����ࣺ(6) ʵ������Ϸ�
                        s[162] = "0.00";//����ࣺ(7) Ӱ��ѧ��Ϸ�
                        s[163] = "0.00";//����ࣺ(8) �ٴ������Ŀ��
                        s[164] = "0.00";//�����ࣺ(9) ������������Ŀ��
                        s[165] = "0.00";//�����ࣺ������������Ŀ�� �����ٴ��������Ʒ�
                        s[166] = "0.00";//�����ࣺ(10) �������Ʒ�
                        s[167] = "0.00";//�����ࣺ�������Ʒ� ���������
                        s[168] = "0.00";//�����ࣺ�������Ʒ� ����������
                        s[169] = "0.00";//�����ࣺ(11) ������
                        s[170] = "0.00";//��ҽ�ࣺ��ҽ������
                        s[171] = "0.00";//��ҩ�ࣺ ��ҩ�� ���п���ҩ�����
                        s[172] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ Ѫ��
                        s[173] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ �׵�������Ʒ��
                        s[174] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ �򵰰���Ʒ��
                        s[175] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ��Ѫ��������Ʒ��
                        s[176] = "0.00";//ѪҺ��ѪҺ��Ʒ�ࣺ ϸ���������
                        s[177] = "0.00";//�Ĳ��ࣺ�����һ����ҽ�ò��Ϸ�
                        s[178] = "0.00";//�Ĳ��ࣺ������һ����ҽ�ò��Ϸ�
                        s[179] = "0.00";//�Ĳ��ࣺ������һ����ҽ�ò��Ϸ�
                        s[180] = "0.00";//�ۺ�ҽ�Ʒ����ࣺһ��ҽ�Ʒ���� ������ҽ��֤���ηѣ���ҽ��
                        s[181] = "0.00";//�ۺ�ҽ�Ʒ����ࣺһ��ҽ�Ʒ���� ������ҽ��֤���λ���ѣ���ҽ��
                        s[182] = "0.00";//��ҽ�ࣺ��ϣ���ҽ��
                        s[183] = "0.00";//��ҽ�ࣺ���ƣ���ҽ��
                        s[184] = "0.00";//��ҽ�ࣺ���� �������Σ���ҽ��
                        s[185] = "0.00";//��ҽ�ࣺ���� ���й��ˣ���ҽ��
                        s[186] = "0.00";//��ҽ�ࣺ���� ���������ķ�����ҽ��
                        s[187] = "0.00";//��ҽ�ࣺ�����������ƣ���ҽ��
                        s[188] = "0.00";//��ҽ�ࣺ���� ���иس����ƣ���ҽ��
                        s[189] = "0.00";//��ҽ�ࣺ���� �����������ƣ���ҽ��
                        s[190] = "0.00";//��ҽ�ࣺ��������ҽ��
                        s[191] = "0.00";//��ҽ�ࣺ���� ������ҩ�������ӹ�����ҽ��
                        s[192] = "0.00";//��ҽ�ࣺ���� ���б�֤ʩ�ţ���ҽ��
                        s[193] = "0.00";//��ҩ�ࣺ�г�ҩ�� ����ҽ�ƻ�����ҩ�Ƽ��ѣ���ҽ��
                        //s[194] = "0.00";//��ҽ�ࣺ���� ���й��ˣ���ҽ��
                        //s[195] = "0.00";//��ҽ�ࣺ���� ���������ķ�����ҽ��
                        //s[196] = "0.00";//��ҽ�ࣺ�����������ƣ���ҽ��
                        //s[197] = "0.00";//��ҽ�ࣺ���� ���иس����ƣ���ҽ��
                        //s[198] = "0.00";//��ҽ�ࣺ���� �����������ƣ���ҽ��
                        //s[199] = "0.00";//��ҽ�ࣺ��������ҽ��
                        //s[200] = "0.00";//��ҽ�ࣺ���� ������ҩ�������ӹ�����ҽ��
                        //s[201] = "0.00";//��ҽ�ࣺ���� ���б�֤ʩ�ţ���ҽ��
                        //s[202] = "0.00";//��ҩ�ࣺ�г�ҩ�� ����ҽ�ƻ�����ҩ�Ƽ��ѣ���ҽ��
                    }
                    else
                    {
                        s[155] = Feeds.Tables[0].Rows[0][1].ToString();//�Ը����
                        s[156] = Feeds.Tables[0].Rows[0][2].ToString();//�ۺ�ҽ�Ʒ����ࣺ��1��һ��ҽ�Ʒ����
                        s[157] = Feeds.Tables[0].Rows[0][3].ToString();//�ۺ�ҽ�Ʒ����ࣺ��2��һ�����Ʋ�����
                        s[158] = Feeds.Tables[0].Rows[0][4].ToString();//�ۺ�ҽ�Ʒ����ࣺ��3�������
                        s[159] = Feeds.Tables[0].Rows[0][5].ToString();//�ۺ�ҽ�Ʒ����ࣺ��4����������
                        s[160] = Feeds.Tables[0].Rows[0][6].ToString();//����ࣺ(5) ������Ϸ�
                        s[161] = Feeds.Tables[0].Rows[0][7].ToString();//����ࣺ(6) ʵ������Ϸ�
                        s[162] = Feeds.Tables[0].Rows[0][8].ToString();//����ࣺ(7) Ӱ��ѧ��Ϸ�
                        s[163] = Feeds.Tables[0].Rows[0][9].ToString();//����ࣺ(8) �ٴ������Ŀ��
                        s[164] = Feeds.Tables[0].Rows[0][10].ToString();//�����ࣺ(9) ������������Ŀ��
                        s[165] = Feeds.Tables[0].Rows[0][11].ToString();//�����ࣺ������������Ŀ�� �����ٴ��������Ʒ�
                        s[166] = Feeds.Tables[0].Rows[0][12].ToString();//�����ࣺ(10) �������Ʒ�
                        s[167] = Feeds.Tables[0].Rows[0][13].ToString();//�����ࣺ�������Ʒ� ���������
                        s[168] = Feeds.Tables[0].Rows[0][14].ToString();//�����ࣺ�������Ʒ� ����������
                        s[169] = Feeds.Tables[0].Rows[0][15].ToString();//�����ࣺ(11) ������
                        s[170] = Feeds.Tables[0].Rows[0][16].ToString();//��ҽ�ࣺ��ҽ������
                        s[171] = Feeds.Tables[0].Rows[0][18].ToString();//��ҩ�ࣺ ��ҩ�� ���п���ҩ�����
                        s[172] = Feeds.Tables[0].Rows[0][21].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ Ѫ��
                        s[173] = Feeds.Tables[0].Rows[0][22].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ �׵�������Ʒ��
                        s[174] = Feeds.Tables[0].Rows[0][23].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ �򵰰���Ʒ��
                        s[175] = Feeds.Tables[0].Rows[0][24].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ��Ѫ��������Ʒ��
                        s[176] = Feeds.Tables[0].Rows[0][25].ToString();//ѪҺ��ѪҺ��Ʒ�ࣺ ϸ���������
                        s[177] = Feeds.Tables[0].Rows[0][26].ToString();//�Ĳ��ࣺ�����һ����ҽ�ò��Ϸ�
                        s[178] = Feeds.Tables[0].Rows[0][27].ToString();//�Ĳ��ࣺ������һ����ҽ�ò��Ϸ�
                        s[179] = Feeds.Tables[0].Rows[0][28].ToString();//�Ĳ��ࣺ������һ����ҽ�ò��Ϸ�
                        s[180] = "0.00";//�ۺ�ҽ�Ʒ����ࣺһ��ҽ�Ʒ���� ������ҽ��֤���ηѣ���ҽ��
                        s[181] = "0.00";//�ۺ�ҽ�Ʒ����ࣺһ��ҽ�Ʒ���� ������ҽ��֤���λ���ѣ���ҽ��
                        s[182] = "0.00";//��ҽ�ࣺ��ϣ���ҽ��
                        s[183] = "0.00";//��ҽ�ࣺ���ƣ���ҽ��
                        s[184] = "0.00";//��ҽ�ࣺ���� �������Σ���ҽ��
                        s[185] = "0.00";//��ҽ�ࣺ���� ���й��ˣ���ҽ��
                        s[186] = "0.00";//��ҽ�ࣺ���� ���������ķ�����ҽ��
                        s[187] = "0.00";//��ҽ�ࣺ�����������ƣ���ҽ��
                        s[188] = "0.00";//��ҽ�ࣺ���� ���иس����ƣ���ҽ��
                        s[189] = "0.00";//��ҽ�ࣺ���� �����������ƣ���ҽ��
                        s[190] = "0.00";//��ҽ�ࣺ��������ҽ��
                        s[191] = "0.00";//��ҽ�ࣺ���� ������ҩ�������ӹ�����ҽ��
                        s[192] = "0.00";//��ҽ�ࣺ���� ���б�֤ʩ�ţ���ҽ��
                        s[193] = "0.00";//��ҩ�ࣺ�г�ҩ�� ����ҽ�ƻ�����ҩ�Ƽ��ѣ���ҽ��
                        //s[194] = "0.00";//��ҽ�ࣺ���� ���й��ˣ���ҽ��
                        //s[195] = "0.00";//��ҽ�ࣺ���� ���������ķ�����ҽ��
                        //s[196] = "0.00";//��ҽ�ࣺ�����������ƣ���ҽ��
                        //s[197] = "0.00";//��ҽ�ࣺ���� ���иس����ƣ���ҽ��
                        //s[198] = "0.00";//��ҽ�ࣺ���� �����������ƣ���ҽ��
                        //s[199] = "0.00";//��ҽ�ࣺ��������ҽ��
                        //s[200] = "0.00";//��ҽ�ࣺ���� ������ҩ�������ӹ�����ҽ��
                        //s[201] = "0.00";//��ҽ�ࣺ���� ���б�֤ʩ�ţ���ҽ��
                        //s[202] = "0.00";//��ҩ�ࣺ�г�ҩ�� ����ҽ�ƻ�����ҩ�Ƽ��ѣ���ҽ��
                    }
                    return s;
                }
                catch (Exception ex)
                {
                    this.Err = ex.ToString();
                    return null;
                }
                #endregion
            }
        }

        /// <summary>
        /// ��ýӿ�HIS_BA1 INSERT SQL
        /// </summary>
        /// <param name="b">������ҳ��ʵ����</param>
        /// <param name="Feeds">������Ϣ����</param>
        /// <param name="alChangeDepe">ת����Ϣ����</param>
        /// <param name="alDose">�����Ϣ����</param>
        /// <param name="isMetCasBase">true������ҳ��Ϣ false סԺ������Ϣ</param>
        /// <returns></returns>
        private string GetInsertHISBA1SQLDrgs(FS.HISFC.Models.HealthRecord.Base b, DataSet Feeds,
            System.Collections.ArrayList alChangeDepe, System.Collections.ArrayList alDose, bool isMetCasBase)
        {
            if (b == null)
            {
                this.Err = "�����ʵ�岻��Ϊnull";

                return null;
            }

            string strReturn = string.Empty;

            strReturn = @"INSERT INTO HIS_BA1
  (
   Fifinput,
   FPRN,
   FTIMES,
   FICDVersion,
   FZYID,
   FAGE,
   FNAME,
   FSEXBH,
   FSEX,
   FBIRTHDAY,
   FBIRTHPLACE, --10
   FIDCard,
   fcountrybh,
   fcountry,
   fnationalitybh,
   fnationality,
   FJOB,
   FSTATUSBH,
   FSTATUS,
   FDWNAME,
   FDWADDR,--20
   FDWTELE,
   FDWPOST,
   FHKADDR,
   FHKPOST,
   FLXNAME,
   FRELATE,
   FLXADDR,
   FLXTELE,
   FASCARD1,
   FRYDATE,--30
   FRYTIME,
   FRYTYKH,
   FRYDEPT,
   FRYBS,
   FCYDATE,
   FCYTIME,
   FCYTYKH,
   FCYDEPT,
   FCYBS,
   FDAYS,--40
   FMZZDBH,
   FMZZD,
   FMZDOCTBH,
   FMZDOCT,
   FPHZD,
   FGMYW,
   FmzCYACCOBH,
   FmzCYACCO,
   FLCBLACCOBH,
   FLCBLACCO,--50
   FQJTIMES,
   FQJSUCTIMES,
   FKZRBH,
   FKZR,
   FZRDOCTBH,
   FZRDOCTOR,
   FZZDOCTBH,
   FZZDOCT,
   FZYDOCTBH,
   FZYDOCT,--60
   FJXDOCTBH,
   FJXDOCT,
   FSXDOCTBH,
   FSXDOCT,
   FBMYBH,
   FBMY,
   FZLRBH,
   FZLR,
   FQUALITYBH,
   FQUALITY,--70
   FZKDOCTBH,
   FZKDOCT,
   FZKNURSEBH,
   FZKNURSE,
   FZKRQ,
FSUM1,
FXYF,
FZYF,
FZCHYF,
FZCYF,--80
FQTF,
   FBODYBH,
   FBODY,
   FBLOODBH,
   FBLOOD,
   FRHBH,
   FRH,
   FBABYNUM,
   FTWILL,
   FZKTYKH,--90
   FZKDEPT,
   FZKDATE,
   FZKTIME,
   FSRYBH,
   FSRY,
   FWORKRQ,
   FJBFXBH,
   FJBFX,
   FFHGDBH,
   FFHGD,--100
   FSOURCEBH,
   FSOURCE,
   FIFSS,
   FIFFYK,
   FYNGR,
   FEXTEND1,
   FEXTEND2,
   FEXTEND3,
   FEXTEND4,
   FEXTEND5,--110
   FEXTEND6,
   FEXTEND7,
   FEXTEND8,
   FEXTEND9,
   FEXTEND10,
   FEXTEND11,
   FEXTEND12,
   FEXTEND13,
   FEXTEND14,
   FEXTEND15,--120
    FNATIVE,
    FCURRADDR,
    FCURRTELE,
    FCURRPOST,
    FJOBBH,
    FCSTZ,
    FRYTZ,
    FRYTJBH,
    FRYTJ,
    FYCLJBH,--130
    FYCLJ,
    FPHZDBH,
    FPHZDNUM,
    FIFGMYWBH,
    FIFGMYW,
    FNURSEBH,
    FNURSE,
    FLYFSBH,
    FLYFS,
    FYZOUTHOSTITAL,--140
    FSQOUTHOSTITAL,
    FISAGAINRYBH,
    FISAGAINRY,
    FISAGAINRYMD,
    FRYQHMDAYS,
    FRYQHMHOURS,
    FRYQHMMINS,
    FRYQHMCOUNTS,
    FRYHMDAYS,
    FRYHMHOURS,--150
    FRYHMMINS,
    FRYHMCOUNTS,
    FFBBHNEW,
    FFBNEW,
FZFJE,
FZHFWLYLF,
FZHFWLCZF,
FZHFWLHLF,
FZHFWLQTF,
FZDLBLF,--160
FZDLSSSF,
FZDLYXF,
FZDLLCF,
FZLLFFSSF,
FZLLFWLZWLF,
FZLLFSSF,
FZLLFMZF,
FZLLFSSZLF,
FKFLKFF,
FZYLZF,--170
FXYLGJF,
FXYLXF,
FXYLBQBF,
FXYLQDBF,
FXYLYXYZF,
FXYLXBYZF,
FHCLCJF,
FHCLZLF,
FHCLSSF,
FZHFWLYLF01,--180
FZHFWLYLF02,
FZYLZDF,
FZYLZLF,
FZYLZLF01,
FZYLZLF02,
FZYLZLF03,
FZYLZLF04,
FZYLZLF05,
FZYLZLF06,
FZYLQTF,--190
FZYLQTF01,
FZYLQTF02,
FZCLJGZJF --193
)
  VALUES
  (
'{0}',
'{1}',
 {2},
'{3}',
'{4}',
'{5}',
'{6}',
'{7}',
'{8}',
'{9}',
'{10}',
'{11}',
'{12}',
'{13}',
'{14}',
'{15}',
'{16}',
'{17}',
'{18}',
'{19}',
'{20}',
'{21}',
'{22}',
'{23}',
'{24}',
'{25}',
'{26}',
'{27}',
'{28}',
'{29}',
'{30}',
'{31}',
'{32}',
'{33}',
'{34}',
'{35}',
'{36}',
'{37}',
'{38}',
'{39}',
 {40},
'{41}',
'{42}',
'{43}',
'{44}',
'{45}',
'{46}',
'{47}',
'{48}',
'{49}',
'{50}',
 {51},
 {52},
'{53}',
'{54}',
'{55}',
'{56}',
'{57}',
'{58}',
'{59}',
'{60}',
'{61}',
'{62}',
'{63}',
'{64}',
'{65}',
'{66}',
'{67}',
'{68}',
'{69}',
'{70}',
'{71}',
'{72}',
'{73}',
'{74}',
'{75}',
 {76},
 {77},
 {78},
 {79},
 {80},
 {81},
'{82}',
'{83}',
'{84}',
'{85}',
'{86}',
'{87}',
 {88},
'{89}',
'{90}',
'{91}',
'{92}',
'{93}',
'{94}',
'{95}',
'{96}',
'{97}',
'{98}',
'{99}',
'{100}',
'{101}',
'{102}',
'{103}',
'{104}',
 {105},
'{106}',
'{107}',
'{108}',
'{109}',
'{110}',
'{111}',
'{112}',
'{113}',
'{114}',
'{115}',
'{116}',
'{117}',
'{118}',
'{119}',
'{120}',
'{121}',
'{122}',
'{123}',
'{124}',
'{125}',
 {126},
 {127},
'{128}',
'{129}',
'{130}',
'{131}',
'{132}',
'{133}',
'{134}',
'{135}',
'{136}',
'{137}',
'{138}',
'{139}',
'{140}',
'{141}',
'{142}',
'{143}',
'{144}',
{145},
{146},
{147},
{148},
{149},
{150},
{151},
{152},
'{153}',
'{154}',
{155},
{156},
{157},
{158},
{159},
{160},
{161},
{162},
{163},
{164},
{165},
{166},
{167},
{168},
{169},
{170},
{171},
{172},
{173},
{174},
{175},
{176},
{177},
{178},
{179},
{180},
{181},
{182},
{183},
{184},
{185},
{186},
{187},
{188},
{189},
{190},
{191},
{192},
{193}
)";

            try
            {

                strReturn = string.Format(strReturn, this.GetBaseInfoBA1Drgs(b, Feeds, alChangeDepe, alDose,isMetCasBase));
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;

                return null;
            }

            return strReturn;
        }
        #endregion
        #region HIS_BA3
        /// <summary>
        /// HIS_BA3  --���������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertHISBA3Drgs(FS.HISFC.Models.HealthRecord.Base patientInfo, FS.HISFC.Models.HealthRecord.Diagnose obj)
        {
            if (obj.DiagInfo.DiagType.ID == "16")
            {
                obj.DiagInfo.DiagType.ID = "f";
            }
            string sql = @"insert into HIS_BA3 (fprn,ftimes,FZDLX, FICDVersion, FICDM, FJBNAME,FRYBQBH,FRYBQ) values ('{0}',{1},'{2}',{3},'{4}','{5}','{6}','{7}')";
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                sql = string.Format(sql,
                                                 this.PatientNoChang(patientNO.Substring(this.fun.PatientNoSubstr())),
                                                 patientInfo.PatientInfo.InTimes.ToString().TrimStart('0'),
                                                 //patientInfo.PatientInfo.InTimes.ToString().PadLeft(2, '0'),
                                                 obj.DiagInfo.DiagType.ID,//����
                                                 "11",//ICD�汾��
                                                 obj.DiagInfo.ICD10.ID,
                                                 //obj.DiagInfo.Name,
                                                 obj.DiagInfo.ICD10.Name,
                                                 obj.DiagOutState,
                                                 "");
            }
            catch
            {
            }

            ReadSQL(sql);

            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }
            return 1;
        }
        #endregion
        #region HIS_BA4
        /// <summary>
        /// insert HIS_BA4  --������Ϣ
        /// </summary>
        /// <param name="patientInfo">������ҳʵ��</param>
        /// <param name="obj">������Ϣʵ��</param>
        /// <returns></returns>
        public int insertHisBa4Drgs(FS.HISFC.Models.HealthRecord.Base patientInfo, FS.HISFC.Models.HealthRecord.OperationDetail obj)
        {

            #region  sql
            string sql = @"INSERT INTO HIS_BA4
(
FPRN ,--������ 0
FTIMES ,--	����
FNAME	,--��������
FOPTIMES   ,--��������
FOPCODE	,--	������
FOP	,--	�������Ӧ����
FOPDATE	,--	��������
FQIEKOUBH	,--	�пڱ��
FQIEKOU	,--�п�
FYUHEBH	,--���ϱ��
FYUHE	,--	����--10
FDOCBH	,--	����ҽ�����
FDOCNAME	,--	����ҽ��
FMAZUIBH   ,--	����ʽ���
FMAZUI	,--����ʽ
FIFFSOP	,--	�Ƿ񸽼�����
FOPDOCT1BH	,--I�����
FOPDOCT1	,--I������
FOPDOCT2BH	,--	II�����
FOPDOCT2	,--II������
FMZDOCTBH	,--	����ҽ�����--20
FMZDOCT,	--����ҽ��
FZQSSBH,--�����������1�ǣ�0��
FZQSS,--��������
FSSJBBH,--����������
FSSJB,--��������
FOPKSNAME,--����ҽ�����ڿ�������
FOPTYKH --����ҽ�����ڿ��ұ��   ����Ϊ��
)
VALUES
(
'{0}' ,
{1},
'{2}' ,
{3},
'{4}' ,
'{5}',
'{6}' ,
'{7}',
'{8}' ,
'{9}',
'{10}' ,
'{11}',
'{12}' ,
'{13}',
'{14}' ,
{15},
'{16}' ,
'{17}',
'{18}' ,
'{19}',
'{20}' ,
'{21}',
'{22}' ,
'{23}',
'{24}' ,
'{25}',
'{26}' ,
'{27}'
)";
            #endregion
            string MarcKind_Code = string.Empty;
            string MarcKind_Name = string.Empty;
            FS.FrameWork.Models.NeuObject info = this.constMana.GetConstant("CASEANESTYPE", obj.MarcKind);
            if (info != null && info.ID != "")
            {
                if (info.Memo != "" && info.Memo.ToUpper() != "TRUE")
                {
                    MarcKind_Code = info.Memo;
                    MarcKind_Name = info.Name;
                }
                else
                {
                    MarcKind_Code = obj.MarcKind;
                    MarcKind_Name = info.Name;
                }
            }
            else
            {
                MarcKind_Code = obj.MarcKind;
                MarcKind_Name = info.Name;
            }

            //�����пڣ�0���Ϊ01��02,03
            string NickKind_Code = string.Empty; 
            string NickKind_Name = string.Empty;
            //FS.FrameWork.Models.NeuObject info = this.constMana.GetConstant("INCITYPE", obj.NickKind);
            //if(info!=null&&info.ID!="")
            //{
            //    if(info.Memo!=""&&info.Memo!="true")
            //    {
            //        NickKind_Code=info.Memo;
            //        NickKind_Name = info.Name;
            //    }
            //    else
            //    {
            //        NickKind_Code=obj.NickKind;
            //        NickKind_Name=this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.INCITYPE, obj.NickKind).Name;
            //    }
            if (obj.NickKind.ToString() == "01" || obj.NickKind.ToString() == "02" || obj.NickKind.ToString() == "03")
            {
                NickKind_Code = "0";
                NickKind_Name = "0��";
            }
            else
            {
                NickKind_Code = obj.NickKind;
                NickKind_Name = this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.INCITYPE, obj.NickKind).Name;
            }

            string checkDateTypeCode = string.Empty;
            string checkDateTypeName = string.Empty;

            if (obj.OperationKind == "1")
            {
                checkDateTypeCode = "1";
                checkDateTypeName = "��";
            }
            else
            {
                checkDateTypeCode = "0";
                checkDateTypeName = "��";
            }
            //�������߱����ȡ���߿��� ����ȡ���� �ٰ�һ����ȡһ������
            FS.HISFC.BizLogic.Manager.Person perMana = new FS.HISFC.BizLogic.Manager.Person();
            FS.HISFC.Models.Base.Employee empl = perMana.GetPersonByID(obj.FirDoctInfo.ID.PadLeft(6, '0'));
            if (empl == null || empl.Dept.ID == "")
            {
                ArrayList peral = new ArrayList();
                peral = perMana.GetPersonByName(obj.FirDoctInfo.Name);
                if (peral != null && peral.Count > 0)
                {
                    empl = peral[0] as FS.HISFC.Models.Base.Employee;
                }
                else
                {
                    empl = new FS.HISFC.Models.Base.Employee();
                    empl = perMana.GetPersonByID(obj.SecDoctInfo.ID.PadLeft(6, '0'));
                    if (empl == null || empl.Dept.ID == "")
                    {
                        ArrayList peral1 = new ArrayList();
                        peral1 = perMana.GetPersonByName(obj.SecDoctInfo.Name);
                        if (peral != null && peral1.Count > 0)
                        {
                            empl = peral1[0] as FS.HISFC.Models.Base.Employee;
                        }
                        else
                        {
                            empl = new FS.HISFC.Models.Base.Employee();
                        }
                    }
                }
            }
            else
            {
                if (obj.FirDoctInfo.Name == null || obj.FirDoctInfo.Name == "")
                {
                    obj.FirDoctInfo.Name = empl.Name;
                }
            }
            //�������ֶ�Ϊ�� ��ʡ������������ʱ��ʾ��null�����±���ʧ�ܣ���������Ϊ����������Ĵ���
            if (obj.FirDoctInfo.Name == null || obj.FirDoctInfo.Name.Trim() == "")//
            {
                obj.FirDoctInfo.Name = "1";
            }
            if (obj.FirDoctInfo.ID == null || obj.FirDoctInfo.ID.Trim() == "")//
            {
                obj.FirDoctInfo.ID = "1";
            }
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                sql = string.Format(sql, this.PatientNoChang(patientNO.Substring(this.PatientNoSubstr())),
                                                 patientInfo.PatientInfo.InTimes.ToString().PadLeft(2, '0'),
                                                 patientInfo.PatientInfo.Name.ToString(),//��������
                                                 obj.HappenNO.ToString(),//��������
                                                 obj.OperationInfo.ID.ToString(),//������
                                                 this.ChangeCharacter(obj.OperationInfo.Name.ToString()),//�������Ӧ����
                                                 this.ChangeDateTime(obj.OperationDate.ToShortDateString()),//��������
                                                 NickKind_Code,//obj.NickKind.ToString(), //�пڱ��
                                                 NickKind_Name,// this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.INCITYPE, obj.NickKind).Name.ToString(),  //�п�
                                                 obj.CicaKind.ToString(),//���ϱ��
                                                 this.constMana.GetConstant("CICATYPE", obj.CicaKind).Name.ToString(),  //����
                                                 this.ConverDoc(obj.FirDoctInfo.ID),//����ҽ�����
                                                 obj.FirDoctInfo.Name,//����ҽ��
                                                 MarcKind_Code, // obj.MarcKind.ToString(),//����ʽ���
                                                 MarcKind_Name, //this.constMana.GetConstant("CASEANESTYPE",obj.MarcKind).Name.ToString(),//����ʽ
                                                 "0",//�Ƿ񸽼�����
                                                 this.ConverDoc(obj.SecDoctInfo.ID),//I�����
                                                 obj.SecDoctInfo.Name.ToString(), // I������
                                                 this.ConverDoc(obj.ThrDoctInfo.ID),//II�����
                                                 obj.ThrDoctInfo.Name.ToString(),//II������
                                                 this.ConverDoc(obj.NarcDoctInfo.ID),//����ҽ�����
                                                 obj.NarcDoctInfo.Name.ToString(), //����ҽ��
                                                 checkDateTypeCode,//�����������1�ǣ�0��
                                                 checkDateTypeName,//������������1�ǣ�0��
                                                 obj.FourDoctInfo.Name,//����������
                                                 this.constMana.GetConstant("CASELEVEL", obj.FourDoctInfo.Name).Name.ToString(),//��������
                                                 empl.Dept.Name,//����ҽ�����ڿ�������
                                                 empl.Dept.ID //����ҽ�����ڿ��ұ���
                                                 );
            }
            catch
            {
            }
            ReadSQL(sql);
            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }
            return 1;
        }
        #endregion
        #region HIS_BA5
        /// <summary>
        /// insert HIS_BA5 --��Ӥ��Ϣ
        /// </summary>
        /// <param name="patientInfo">������ҳʵ��</param>
        /// <param name="obj">��Ӥ����Ϣ</param>
        /// <returns></returns>
        public int insertHisBa5Drgs(FS.HISFC.Models.HealthRecord.Base patientInfo, FS.HISFC.Models.HealthRecord.Baby obj)
        {


            string sql = @"insert into HIS_BA5 (FPRN,FTIMES ,FBABYNUM ,FNAME ,FBABYSEXBH ,FBABYSEX ,FTZ ,FRESULTBH ,FRESULT ,
                  FZGBH ,FZG ,FBABYQJ,FBABYSUC ,FHXBH ,FHX) 
                                         values ('{0}',{1},{2},'{3}','{4}','{5}',{6},'{7}','{8}',
                  '{9}','{10}','{11}','{12}','{13}','{14}')";



            if (obj.SexCode.ToString() == "M" || obj.SexCode.ToString() == "1")
            {
                obj.SexCode = "1";
                obj.Infect.Memo = "��";
            }
            else if (obj.SexCode.ToString() == "F" || obj.SexCode.ToString() == "2")
            {
                obj.SexCode = "2";
                obj.Infect.Memo = "Ů";
            }
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                sql = string.Format(sql, this.PatientNoChang(patientNO.Substring(this.PatientNoSubstr())),
                                                 patientInfo.PatientInfo.InTimes.ToString().PadLeft(2, '0'),
                                                 obj.HappenNum.ToString(),//Ӥ�����
                                                 patientInfo.PatientInfo.Name.ToString(),//��������
                                                 obj.SexCode.ToString(),//Ӥ���Ա���
                                                 obj.Infect.Memo,//Ӥ���Ա�
                                                 obj.Weight.ToString(),//Ӥ������
                                                 obj.BirthEnd.ToString(), //���������
                                                 this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.CHILDBEARINGRESULT, obj.BirthEnd).Name.ToString(),  //������
                                                 obj.BabyState,//ת����
                                                 this.constMana.GetConstant("babyZG", obj.BabyState).Name.ToString(),  //ת��
                                                 obj.SalvNum.ToString(),//���ȴ���
                                                 obj.SuccNum.ToString(),//Ӥ�����ȳɹ�����
                                                 obj.Breath,//�������
                                                 this.constMana.GetConstant(FS.HISFC.Models.Base.EnumConstant.BREATHSTATE, obj.BirthEnd).Name.ToString() //����
                                                 );
            }
            catch
            {
            }
            ReadSQL(sql);
            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }


            return 1;
        }
        #endregion
        /// <summary>
        /// HIS_BA6  --����������Ϣ
        /// </summary>
        /// <param name="patientInfo">������ҳʵ��</param>
        /// <param name="obj">��������Ϣ</param>
        /// <returns></returns>
        public int InsertHISBA6Drgs(FS.HISFC.Models.HealthRecord.Base patientInfo, FS.HISFC.Models.HealthRecord.Tumour obj)
        {
            #region sql
            string sql = @"INSERT INTO HIS_BA6
(
FPRN,--0
FTIMES,
FFLFSBH,
FFLFS,
FFLCXBH,
FFLCX,--5
FFLZZBH,
FFLZZ,
FYJY,
FYCS,
FYTS,--10
FYRQ1,
FYRQ2,
FQJY,
FQCS,
FQTS,--15
FQRQ1,
FQRQ2,
FZNAME,
FZJY,
FZCS,--20
FZTS,
FZRQ1,
FZRQ2,
FHLFSBH,
FHLFS,--25
FHLFFBH,
FHLFF,
FQTYPE,
FQT,
FQN,--30
FQM,
FQALL,
FQALLBH--33
)
VALUES
(
'{0}',
'{1}',
'{2}',
'{3}',
'{4}',
'{5}',
'{6}',
'{7}',
'{8}',
'{9}',
'{10}',
'{11}',
'{12}',
'{13}',
'{14}',
'{15}',
'{16}',
'{17}',
'{18}',
'{19}',
'{20}',
'{21}',
'{22}',
'{23}',
'{24}',
'{25}',
'{26}',
'{27}',
'{28}',
'{29}',
'{30}',
'{31}',
'{32}',
'{33}'
)";
            #endregion
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                sql = string.Format(sql,
                                                 this.PatientNoChang(patientNO.Substring(this.PatientNoSubstr())),//������
                                                 patientInfo.PatientInfo.InTimes.ToString().TrimStart('0'),//����
                                                 obj.Rmodeid,//���Ʒ�ʽ���
                                                 this.constMana.GetConstant("RADIATETYPE", obj.Rmodeid).Name,//���Ʒ�ʽ
                                                 obj.Rprocessid,//���Ƴ�����
                                                 this.constMana.GetConstant("RADIATEPERIOD", obj.Rprocessid).Name,//���Ƴ���
                                                 obj.Rdeviceid,//����װ�ñ��
                                                 this.constMana.GetConstant("RADIATEDEVICE", obj.Rdeviceid).Name,//����װ��
                                                 obj.Gy1,//1.ԭ�������
                                                 obj.Time1,//ԭ�������
                                                 obj.Day1,//ԭ��������
                                                 this.ChangeDateTime(obj.BeginDate1.ToString()),//ԭ���ʼ����
                                                 this.ChangeDateTime(obj.EndDate1.ToString()),//ԭ�������ʱ��
                                                 obj.Gy2,//2.�����ܰͽ����
                                                 obj.Time2,//�����ܰͽ����
                                                 obj.Day2,//�����ܰͽ�����
                                                 this.ChangeDateTime(obj.BeginDate2.ToString()),//�����ܰͽῪʼʱ��
                                                 this.ChangeDateTime(obj.EndDate2.ToString()),//�����ܰͽ����ʱ��
                                                 obj.Position,//3.ת��������
                                                 obj.Gy3,//3.ת�������
                                                 obj.Time3,//ת�������
                                                 obj.Day3,//ת��������
                                                 this.ChangeDateTime(obj.BeginDate3.ToString()),//ת���ʼʱ��
                                                 this.ChangeDateTime(obj.EndDate3.ToString()),//ת�������ʱ��
                                                 obj.Cmodeid,//���Ʒ�ʽ���
                                                 this.constMana.GetConstant("CHEMOTHERAPY", obj.Cmodeid).Name,//���Ʒ�ʽ
                                                 obj.Cmethod,//���Ʒ������
                                                 this.constMana.GetConstant("CHEMOTHERAPYWAY", obj.Cmethod).Name,//���Ʒ���
                                                 obj.Tumour_Type,//������������
                                                 obj.Tumour_T,//ԭ������T
                                                 obj.Tumour_N,//�ܰ�ת��N
                                                 obj.Tumour_M,//Զ��ת��M
                                                 this.constMana.GetConstant("CASETUMOURSTAGE",obj.Tumour_Stage).Name,//����
                                                 obj.Tumour_Stage//���ڱ���
                                                 );
            }
            catch
            {
            }

            sql = sql.Replace("'NULL'", "NULL");
            ReadSQL(sql);

            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ����HIS_BA6  FYRQ1 FYRQ2Ϊ��
        /// </summary>
        /// <returns></returns>
        public int UpdateHISBA6FYRQ(string FPRN)
        {
            // t.FQRQ1 t.FQRQ2  t.FZRQ1 t.FZRQ2  
            string strSQL = @"update HIS_BA6 set FYJY=null, FYCS=null, FYTS=null ,FYRQ1=null ,FYRQ2=null where FPRN='{0}'";
            try
            {
                strSQL = string.Format(strSQL, this.PatientNoChang(FPRN.PadLeft(10,'0').Substring(this.PatientNoSubstr())));
            }
            catch
            {
                return -1;
            }
            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// ����HIS_BA6  t.FQRQ1 t.FQRQ2  Ϊ��
        /// </summary>
        /// <returns></returns>
        public int UpdateHISBA6FQRQ(string FPRN)
        {
            //t.FZRQ1 t.FZRQ2  
            string strSQL = @"update HIS_BA6 set FQJY=null,FQCS=null ,FQTS=null,FQRQ1=null ,FQRQ2=null where FPRN='{0}'";
            try
            {
               // strSQL = string.Format(strSQL, this.PatientNoChang(FPRN.Substring(this.PatientNoSubstr())));
           strSQL = string.Format(strSQL, this.PatientNoChang(FPRN.PadLeft(10, '0').Substring(this.PatientNoSubstr())));



            }
            catch
            {
                return -1;
            }
            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// ����HIS_BA6  t.FZRQ1 t.FZRQ2Ϊ��
        /// </summary>
        /// <returns></returns>
        public int UpdateHISBA6FZRQ(string FPRN)
        {
            string strSQL = @"update HIS_BA6 set FZJY=null, FZCS=null, FZTS=null ,FZRQ1=null ,FZRQ2=null where FPRN='{0}'";
            try
            {
                strSQL = string.Format(strSQL, this.PatientNoChang(FPRN.PadLeft(10, '0').Substring(this.PatientNoSubstr())));
            }
            catch
            {
                return -1;
            }
            ReadSQL(strSQL);

            int intReturn = this.cmd.ExecuteNonQuery();

            if (intReturn < 0)
            {
                return -1;
            }

            return intReturn;
        }
        /// <summary>
        /// HIS_BA7  --�������Ƽ�¼
        /// </summary>
        /// <param name="patientInfo">������ҳʵ��</param>
        /// <param name="obj">����������������Ϣ</param>
        /// <returns></returns>
        public int InsertHISBA7Drgs(FS.HISFC.Models.HealthRecord.Base patientInfo, FS.HISFC.Models.HealthRecord.TumourDetail obj)
        {
            #region sql
            string sql = @"INSERT INTO HIS_BA7
(
FPRN,
FTIMES,
FHLRQ1,
FHLRQ2,
FHLDRUG,
FHLPROC,
FHLLXBH,
FHLLX
)
VALUES
(
'{0}',
'{1}',
'{2}',
'{3}',
'{4}',
'{5}',
'{6}',
'{7}'
)";
            #endregion
            string patientNO = patientInfo.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            try
            {
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList UnitList = con.GetList(FS.HISFC.Models.Base.EnumConstant.DOSEUNIT);
                UnitListHelper.ArrayObject = UnitList;
                sql = string.Format(sql,
                                                 this.PatientNoChang(patientNO.Substring(this.PatientNoSubstr())),//������
                                                 patientInfo.PatientInfo.InTimes.ToString().TrimStart('0'),//����
                                                 this.ChangeDateTime(obj.CureDate.ToString()),//������ʼ����
                                                 this.ChangeDateTime(obj.OperInfo.OperTime.ToString()),//������ֹ����
                                               //  obj.DrugInfo.Name + "(" + obj.Qty.ToString() + obj.Unit.ToString() + ")",//����ҩ�����Ƽ�����
                                                 obj.DrugInfo.Name + "(" + obj.Qty.ToString() + UnitListHelper.GetName(obj.Unit) + ")",
                                                 obj.Period.ToString(),//�����Ƴ�
                                                 obj.Result.ToString(),//��Ч���
                                                 "" //��Ч
                                                 );
            }
            catch
            {
            }
            sql = sql.Replace("'NULL'", "NULL");
            ReadSQL(sql);

            if (this.cmd.ExecuteNonQuery() < 0)
            {
                return -1;
            }
            return 1;
        }
        #region ת��������
        /// <summary>
        /// ���ݳ�����DEPTUPLOAD1��mark�ֶ�ת����������
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="deptName">��������</param>
        private string ConverDeptName(string deptCode, string deptName)
        {
            FS.FrameWork.Models.NeuObject obj = this.constMana.GetConstant("DEPTUPLOAD1", deptCode);
            if (obj == null)
            {
                return deptName;
            }
            string strReturn = obj.Memo;

            if (strReturn == "")
            {
                return deptName;
            }
            else
            {
                return strReturn;
            }
        }

        /// <summary>
        /// �������ַ� ת�� 
        /// ���� '�� ת���� ������
        /// </summary>
        /// <param name="Character">�ַ���</param>
        /// <returns></returns>
        private string ChangeCharacter(string Character)
        {
            Character = Character.Replace("'", "��");
            return Character;
        }


        /// <summary>
        /// ���ݳ�����DOCTORUPLOAD��mark�ֶ�ת��ҽ������
        /// </summary>
        /// <param name="DocCode">ҽ������</param>
        private string ConverDoc(string DocCode)
        {
            FS.FrameWork.Models.NeuObject obj = this.constMana.GetConstant("DOCTORUPLOAD", DocCode);
            if (obj == null)
            {
                return DocCode;
            }
            string strReturn = obj.Memo;

            if (strReturn == "")
            {
                return DocCode;
            }
            else
            {
                return strReturn;
            }
        }
        /// <summary>
        /// ʱ��ת��
        /// sqlserver ����0001-01-01
        /// </summary>
        /// <param name="dtStr">����ת��</param>
        /// <returns></returns>
        private string ChangeDateTime(string dtStr)
        {
            string retStr = string.Empty;
            DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(dtStr);

            if (dt.Date.Year < 1990)
            {
                retStr = "NULL";
            }
            else
            {
                retStr = dtStr;
            }
            return retStr;
        }

        /// <summary>
        ///  �ϴ�������λ��
        ///  û�����ó���������8λ ������ʵ�ʷ���
        /// </summary>
        /// <returns></returns>
        private int PatientNoSubstr()
        {
            int ret = 2;//8λ 
            FS.FrameWork.Models.NeuObject obj = this.constMana.GetConstant("CASEPNOSUBSTR", "1");
            //��ά������ϴ�8λ
            if (obj == null)
            {
                ret = 2;
                return ret;
            }
            if (obj.Memo == "")
            {
                ret = 2;
                return ret;
            }
            else if (obj.Memo.ToUpper() == "TRUE")
            {
                ret = 2;
                return ret;
            }
            else
            {
                int uplaodNum = 0;
                try
                {
                    uplaodNum = FS.FrameWork.Function.NConvert.ToInt32(obj.Memo);
                }
                catch
                {
                    uplaodNum = 0;
                }
                if (uplaodNum == 0)
                {
                    ret = 2;
                    return ret;
                }
                else
                {
                    ret = 10 - uplaodNum;
                }
            }
            return ret;
        }
        #endregion
        #endregion 
        #region סԺ��־ 2012-6-26 ����
        /// <summary>
        /// HIS_ZYLOG -- ������̬��־
        /// ���������HIS��ʱ����Ϊ������ʽ��TZyWardWorkDayReport
        /// </summary>
        /// <param name="alPatientMove">��̬�ձ�����</param>
        /// <param name="beginDate">��ʼ����</param>
        /// <param name="endDate">��������</param>
        /// <param name="isExist">�Ƿ������ݱ�����</param>
        /// <returns></returns>
        public int InsertTZyWardWorklog(System.Collections.ArrayList alPatientMove, string beginDate, string endDate, ref int intDelete)
        {
            this.IsOpen();
            string strSQL = "delete TZyWardWorklog where FRQ = '{0}' and FTykh= '{1}'";

            string strsql = @"INSERT INTO TZyWardWorklog
  (FRQ,
FTykh,
FKsName,
FBEDSUM,
FStatFlag,
FYRS,
FRYRS,
FTKZR,
FTQZR,
FCYRS,
FDEADRS,
FZRTK,
FZRTQ,
FXYRS,
FPRS,
FBCZYL,
FWorkrq
)
VALUES
  (
   '{0}',
   '{1}',
   '{2}',
   {3},
   '{4}',
   {5},
   {6},
   {7},
   {8},
   {9},
   {10},
   {11},
   {12},
   {13},
   {14},
   {15},
   '{16}'
)";
            string temp = string.Empty;
            int intReturn = 0;
            foreach (FS.HISFC.Models.HealthRecord.Case.PatientMove patientMove in alPatientMove)
            {
                int j = 0;
                this.cmd.CommandText = string.Empty;
                this.cmd.CommandText = string.Format(strSQL, patientMove.OperDate.ToShortDateString().Replace('-', '/'), patientMove.DeptCode);
                try
                {
                    j = this.cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                }
                if (j > 0) intDelete += j;


                this.cmd.CommandText = string.Empty;
                temp = strsql;
                this.cmd.CommandText = string.Format(temp,
                                                 patientMove.OperDate.ToShortDateString().Replace('-', '/'),
                                                 patientMove.DeptCode,
                                                 patientMove.OperDept,
                                                 patientMove.BedNum.ToString(),
                                                 "***",
                                                 patientMove.OriginalNum.ToString(),
                                                 patientMove.InNum.ToString(),
                                                 patientMove.OtherDeptIn.ToString(),
                                                 patientMove.OtherRegionIn.ToString(),
                                                 patientMove.OutNum.ToString(),
                                                 patientMove.DeadNum.ToString(),
                                                 patientMove.ToOtherDept.ToString(),
                                                 patientMove.ToOtherRegion.ToString(),
                                                 patientMove.PatientNum.ToString(),
                                                 patientMove.AccompanyNum.ToString(),
                                                 patientMove.BeduseNum.ToString(),//**��λռ����,ûд�㷨��Ҫ�ڱ��ֲ㸳ֵ**
                                                 patientMove.Memo.Replace('-', '/')
                                                 );

                if (this.cmd.ExecuteNonQuery() <= 0)
                {
                    return -1;
                }
                intReturn++;
            }
            //if (intDelete > 0) isExist = true;
            return intReturn;
        }

        #endregion
        private string PatientNoChang(string patientNo)
        {
            string ret = string.Empty;
            //�Ƿ���Ҫת��
            ArrayList al = this.constMana.GetList("CasePatientNoChang");
            if (al != null && al.Count>0)//��Ҫת��
            {
                ret = patientNo.Replace("v", "V");
              
                if (ret.IndexOf("V") >= 0)
                {
                    ret = ret.Replace("V", "0");
                    ret = "V" + ret.Substring(1);
                }
                else
                {
                    ret = patientNo;
                }
            }
            else 
            {
                ret= patientNo;
            }
            return ret;
        }

        public int GetBATimes(string inpatientno,string patientno,string date)
        {
            string sql = @"SELECT t.FTIMES  from tpatientvisit t where  t.fzyid='{0}'";
            sql = string.Format(sql, inpatientno);

            ReadSQL(sql);
            this.reader = this.cmd.ExecuteReader();
            try
            {
                while (this.reader.Read())
                {
                    return int.Parse(this.reader[0].ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                this.Err1 = ex.Message;
                return 0;
            }
            finally
            {
                this.reader.Close();
            }
        }
    }
}