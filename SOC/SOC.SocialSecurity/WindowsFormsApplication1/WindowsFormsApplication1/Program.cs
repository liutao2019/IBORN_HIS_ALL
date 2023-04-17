using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Save();
        }

        public static void Save()
        {
            Sqlserver sqlserver = new Sqlserver();

            Oracle oracle = new Oracle();

            sqlserver.Connect();

            oracle.Connect();

            DataSet ds = new DataSet();

            string sql = @"select   a.patientid,  a.patientname,  sexflag = case when a.patientsexflag = 1 then '男' when a.patientsexflag = 2 then '女' else '其它' end ,  a.age+'0M0D0H0m' age,  
                        a.patientbirthday,  a.RequestDoctorEmployeeNo,  a.RequestDoctorEmployeeName,  
                        a.requestdepartmentno,  a.requestdepartmentname,  a.DiseaseName,  
                        RIGHT(a.ExamineRequestNo,len(a.ExamineRequestNo) -1) as ExamineRequestNo,  
                        a.MedicareNo,  a.PatientCardNo,  a.IPSeqNoText,  a.SickBedNo,  a.SeqNo,  
                        b.itemsetno,  b.itemsetdesc, b.amount,  a.registerdate,  
                        blood_sign = (case when b.ExamineExemplarDesc like '%血%' and RTRIM(b.ExamineExemplarDesc) <> '末梢血' then 'true'  else 'false' end ), 
                        'MZ' + CONVERT(varchar,b.ExamineItemSetForLISID) as ExamineItemSetForLISID,  
                        a.RequestExecutiveDateTime,  CONVERT(varchar(6),registerdate,12) + convert(varchar(8),seqno) as pat_in_no,
                        a.Phone,CONVERT(varchar(6),registerdate,12) + convert(varchar(8),seqno)
                        from dbo.tExamineRequest a,dbo.tExamineItemSetList b  where a.ExamineRequestID = b.ExamineRequestID and   b.payflag=1 and IPSeqNoText='' and a.registerdate > '{0}'";

            DateTime dtNow = DateTime.Now;

            //sql = string.Format(sql, dtNow.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss"));
            sql = string.Format(sql, dtNow.AddDays(-10).ToString("yyyy-MM-dd HH:mm:ss"));

            sqlserver.ExecQuery(sql, ref ds);

            if (ds != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    try
                    {
                        string sqlinsert = @"insert into JIANXUN_LISFEEDETAIL
                                        (
                                          PAITENTID    ,
                                          PATIENTNAME  ,
                                          SEXFLAG      ,
                                          AGE          ,
                                          PATIENTBIRTHDAY,
                                          EMPLNO       ,
                                          EMPLNAME     ,
                                          DEPTNO       ,
                                          DEPTNAME     ,
                                          DISEASENAME  ,
                                          EXAMINO      ,
                                          MEDCINENO    ,
                                          PATIENTCARDNO,
                                          IPSEQNO      ,
                                          SICKBEDNO    ,
                                          SEQNO        ,
                                          ITEMSETNO    ,
                                          ITEMSETDESC  ,
                                          AMOUNT       ,
                                          REGISTERDATE ,
                                          BLOODSIGN    ,
                                          EXAMIITEMSETNO ,
                                          EXECDATETIME ,
                                          PATIENTINNO  ,
                                          PHONE        ,
                                          OLDINDEX     
                                        )
                                        values
                                        (
                                        '{0}',
                                        '{1}',
                                        '{2}',
                                        '{3}',
                                        to_date('{4}','yyyy-mm-dd hh24:mi:ss'),
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
                                        to_date('{19}','yyyy-mm-dd hh24:mi:ss'),
                                        '{20}',
                                        '{21}',
                                        to_date('{22}','yyyy-mm-dd hh24:mi:ss'),
                                        '{23}',
                                        '{24}',
                                        '{25}'
                                        )";

                        sqlinsert = string.Format(sqlinsert,
                            row[0].ToString(),
                            row[1].ToString(),
                            row[2].ToString(),
                            row[3].ToString(),
                            DateTime.Parse(row[4].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            row[5].ToString(),
                            row[6].ToString(),
                            row[7].ToString(),
                            row[8].ToString(),
                            row[9].ToString(),
                            row[10].ToString(),
                            row[11].ToString(),
                            row[12].ToString(),
                            row[13].ToString(),
                            row[14].ToString(),
                            row[15].ToString(),
                            row[16].ToString(),
                            row[17].ToString(),
                            row[18].ToString(),
                            DateTime.Parse(row[19].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            row[20].ToString(),
                            row[21].ToString(),
                            DateTime.Parse(row[22].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            row[23].ToString(),
                            row[24].ToString(),
                            row[25].ToString());

                        int i = oracle.ExecNoQuery(sqlinsert);

                        string a = i.ToString();
                    }
                    catch { }
                }
            }

            oracle = null;
            sqlserver = null;
        }
    }
}
