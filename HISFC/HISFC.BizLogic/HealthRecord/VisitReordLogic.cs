using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.HealthRecord;
namespace FS.HISFC.BizLogic.HealthRecord
{

    /// <summary>
    /// {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
    /// </summary>
    public class VisitReordLogic : FS.FrameWork.Management.Database
    {

        private List<VisitReord> QueryObcrecordBySql(string sql)
        {
            List<VisitReord> items = new List<VisitReord>();
            //执行当前Sql语句
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    VisitReord visitReordBase = new VisitReord();
                    visitReordBase.SERIALNO = this.Reader[0].ToString();
                    visitReordBase.USERMCNO = this.Reader[1].ToString();
                    visitReordBase.DOCTORID = this.Reader[2].ToString(); ;
                    visitReordBase.CHIEFCOMPLAINT = this.Reader[3].ToString();
                    visitReordBase.CONSULTDATE = this.Reader[4].ToString();
                    visitReordBase.PASTHISTORY = this.Reader[5].ToString();
                    visitReordBase.HPI = this.Reader[6].ToString();
                    visitReordBase.SIGN = this.Reader[7].ToString();
                    visitReordBase.ALLERGIES = this.Reader[8].ToString();
                    visitReordBase.PRESCRIPTION = this.Reader[9].ToString();
                    visitReordBase.HEIGHT = this.Reader[10].ToString();
                    visitReordBase.WEIGHT = this.Reader[11].ToString();
                    visitReordBase.SYSTOLIC = this.Reader[12].ToString();
                    visitReordBase.DIASTOLIC = this.Reader[13].ToString();
                    visitReordBase.TEMPERATURE = this.Reader[14].ToString();
                    visitReordBase.BLOOD = this.Reader[15].ToString();
                    string diagnose1 = this.Reader[16].ToString();
                    visitReordBase.DIAGNOSE1 = diagnose1;
                    string diagnosecode1 = this.Reader[17].ToString();
                    visitReordBase.DIAGNOSECODE1 = diagnosecode1;
                    string diagnose2 = this.Reader[18].ToString();
                    visitReordBase.DIAGNOSE2 = diagnose2;
                    string diagnosecode2 = this.Reader[19].ToString();
                    visitReordBase.DIAGNOSECODE2 = diagnosecode2;
                    string diagnose3 = this.Reader[20].ToString();
                    visitReordBase.DIAGNOSE3 = diagnose3;
                    string diagnosecode3 = this.Reader[21].ToString();
                    visitReordBase.DIAGNOSECODE3 = diagnosecode3;
                    string diagnose4 = this.Reader[22].ToString();
                    visitReordBase.DIAGNOSE4 = diagnose4;
                    string diagnosecode4 = this.Reader[23].ToString();
                    visitReordBase.DIAGNOSECODE4 = diagnosecode4;
                    string diagnose5 = this.Reader[24].ToString();
                    visitReordBase.DIAGNOSE5 = diagnose5;
                    string diagnosecode5 = this.Reader[25].ToString();
                    visitReordBase.DIAGNOSECODE5 = diagnosecode5;
                    string diagnose6 = this.Reader[26].ToString();
                    visitReordBase.DIAGNOSE6 = diagnose6;
                    string diagnosecode6 = this.Reader[27].ToString();
                    visitReordBase.DIAGNOSECODE6 = diagnosecode6;
                    string diagnose7 = this.Reader[28].ToString();
                    visitReordBase.DIAGNOSE7 = diagnose7;
                    string diagnosecode7 = this.Reader[29].ToString();
                    visitReordBase.DIAGNOSECODE7 = diagnosecode7;
                    string diagnose8 = this.Reader[30].ToString();
                    visitReordBase.DIAGNOSE8 = diagnose8;
                    string diagnosecode8 = this.Reader[31].ToString();
                    visitReordBase.DIAGNOSECODE8 = diagnosecode8;
                    string expectedtime = this.Reader[32].ToString();
                    visitReordBase.EXPECTEDTIME = expectedtime;
                    visitReordBase.Diaglist = new List<Diatemple>() { new Diatemple { DiagCode = diagnosecode1, DiagName = diagnose1 }, new Diatemple { DiagCode = diagnosecode2, DiagName = diagnose2 }, new Diatemple { DiagCode = diagnosecode3, DiagName = diagnose3 }, new Diatemple { DiagCode = diagnosecode4, DiagName = diagnose4 }, new Diatemple { DiagCode = diagnosecode5, DiagName = diagnose5 }, new Diatemple { DiagCode = diagnosecode6, DiagName = diagnose6 }, new Diatemple { DiagCode = diagnosecode7, DiagName = diagnose7 }, new Diatemple { DiagCode = diagnosecode8, DiagName = diagnose8 } };
                  

                    items.Add(visitReordBase);
                }

                return items;
            }
            catch (Exception e)
            {
                this.Err = "获得预结算基本信息出错！" + e.Message;
                this.WriteErr();

                return null;
            }
            finally
            {
                if (Reader != null && !Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }



        public ArrayList QueryObcrecordBySerialno(string serialno)
        {

            string strSql = "";
            if (this.Sql.GetSql("Order.OutPatient.GetObisRecord", ref strSql) == -1) return null;
            strSql = strSql = string.Format(strSql, serialno); 
            strSql = string.Format(strSql, serialno);
            List<VisitReord> list = this.QueryObcrecordBySql(strSql);
            if (list != null)
            {
                return new ArrayList(list);
            }
            return null;
        }
    }
}
