using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.RADT.BizLogic
{
    public class ComOldPatient : FS.FrameWork.Management.Database
    {
        #region 私有函数

        /// <summary>
        /// 查询患者信息主SQL(ZL)
        /// </summary>
        /// <returns>FIN_IPR_INMAININFO SQL 语句</returns>
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
        /// 根据SQL语句查询旧系统患者基本信息
        /// </summary>
        /// <param name="SQLPatient"></param>
        /// <returns></returns>
        private ArrayList myPatientQuery(string SQLPatient)
        {

            ArrayList al = new ArrayList();
            FS.HISFC.Models.RADT.PatientInfo PatientInfo;
            ProgressBarText = "正在查询患者...";
            ProgressBarValue = 0;
            //取系统时间,用来得到年龄字符串
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
                        if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); // 住院号
                        if (!Reader.IsDBNull(0)) PatientInfo.PID.PatientNO = Reader[0].ToString(); // 住院号
                        if (!Reader.IsDBNull(1)) PatientInfo.Card.ICCard.ID = Reader[1].ToString(); //IC卡号
                        if (!Reader.IsDBNull(2)) PatientInfo.Name = Reader[2].ToString(); //  姓名						
                        if (!Reader.IsDBNull(3)) PatientInfo.SpellCode = Reader[3].ToString(); //拼音码
                        if (!Reader.IsDBNull(4)) PatientInfo.WBCode = Reader[4].ToString(); // 五笔码
                        if (!Reader.IsDBNull(5)) PatientInfo.Birthday = NConvert.ToDateTime(Reader[5]); // 生日
                        if (!Reader.IsDBNull(6)) PatientInfo.Sex.ID = Reader[6].ToString(); //   性别
                        if (!Reader.IsDBNull(7)) PatientInfo.IDCard = Reader[7].ToString(); //  身份证号
                        if (!Reader.IsDBNull(8)) PatientInfo.BloodType.ID = Reader[8].ToString(); //  血型
                        if (!Reader.IsDBNull(9)) PatientInfo.Profession.ID = Reader[9].ToString(); //  职业
                        if (!Reader.IsDBNull(10)) PatientInfo.CompanyName = Reader[10].ToString(); // 单位名称
                        if (!Reader.IsDBNull(11)) PatientInfo.PhoneBusiness = Reader[11].ToString(); //  单位电话
                        if (!Reader.IsDBNull(12)) PatientInfo.User01 = Reader[12].ToString(); //单位邮编
                        if (!Reader.IsDBNull(13)) PatientInfo.AddressHome = Reader[13].ToString(); //  家庭地址
                        if (!Reader.IsDBNull(14)) PatientInfo.PhoneHome = Reader[14].ToString(); //  家庭电话
                        if (!Reader.IsDBNull(15)) PatientInfo.User02 = Reader[15].ToString(); //  家庭邮编
                        if (!Reader.IsDBNull(16)) PatientInfo.DIST = Reader[16].ToString(); //  籍贯
                        if (!Reader.IsDBNull(17)) PatientInfo.Nationality.ID = Reader[17].ToString(); //  民族
                        if (!Reader.IsDBNull(18)) PatientInfo.Kin.Name = Reader[18].ToString(); //  联系人姓名
                        if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationPhone = Reader[19].ToString(); // 联系人电话
                        if (!Reader.IsDBNull(20)) PatientInfo.Kin.RelationAddress = Reader[20].ToString(); //  联系人地址
                        if (!Reader.IsDBNull(21)) PatientInfo.Kin.Relation.ID = Reader[21].ToString(); //  与患者关系
                        if (!Reader.IsDBNull(22)) PatientInfo.MaritalStatus.ID = Reader[22].ToString(); // 婚姻状况
                        if (!Reader.IsDBNull(23)) PatientInfo.Country.ID = Reader[23].ToString(); //  国籍             
                        if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.ID = Reader[24].ToString(); //  合同类别
                        if (!Reader.IsDBNull(25)) PatientInfo.Pact.PayKind.Name = Reader[25].ToString(); //  合同类别名称
                        if (!Reader.IsDBNull(26)) PatientInfo.Pact.ID = Reader[26].ToString(); //  合同单位
                        if (!Reader.IsDBNull(27)) PatientInfo.Pact.Name = Reader[27].ToString(); //  合同单位名称

                        if (!Reader.IsDBNull(28)) PatientInfo.SSN = Reader[28].ToString(); //  医疗证号
                        if (!Reader.IsDBNull(29)) PatientInfo.AreaCode = Reader[29].ToString(); //  出生地
                        if (!Reader.IsDBNull(30)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[30]); //  医疗费用
                        if (!Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31]); //  药物过敏

                        if (!Reader.IsDBNull(41)) PatientInfo.InTimes = NConvert.ToInt32(Reader[41]); //入院次数

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
            } //抛出错误
            catch (Exception ex)
            {
                Err = "获得患者基本信息出错！" + ex.Message;
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

        /// <summary>
        /// 查询旧系统患者
        /// </summary>
        /// <param name="patientName">姓名</param>
        /// <param name="sexcode">性别：1 男；2 女</param>
        /// <returns></returns>
        public ArrayList GetOldSysPatientInfo(string sqlWhere)
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

        #endregion

    }
}
