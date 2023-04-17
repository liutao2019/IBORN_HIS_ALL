using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Components.Project
{
    public class MissionManager : FrameWork.Management.DataBaseManger
    {
        public MissionManager()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml"))
            {
                SOC.Public.XML.File.CreateXmlFile(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "1.0", "设置");
            }
        }
        public int QueryMission(ref System.Data.DataSet ds)
        {
            string SQL = "";
            if (this.Sql.GetCommonSql("SOC.System.Project.QueryMission.All", ref SQL) == -1)
            {
                SQL = @"select * from view_soc_project_mission";
                this.Sql.CacheSql("SOC.System.Project.QueryMission.All", SQL);
            }

            return this.ExecQuery(SQL, ref ds);
        }

        public int QueryMission(string modelName, string state, string missionAccepter, DateTime dt, ref System.Data.DataSet ds)
        {
            string SQL = "";

            if (this.Sql.GetCommonSql("SOC.System.Project.QueryMission.ByMSSD", ref SQL) == -1)
            {
                SQL = @"select * from view_soc_project_mission 
                where (模块名称 like '{0}%' or 'All'='{0}') 
                and (状态 like '{1}%' or 'All' = '{1}') 
                and (开发人 like '{2}%' or 'All' = '{2}')
                and 录入时间>=to_date('{3}','yyyy-mm-dd hh24:mi:ss')";
                this.Sql.CacheSql("SOC.System.Project.QueryMission.ByMSSD", SQL);
            }
            string SQLFormat = string.Format(SQL, modelName, state, missionAccepter, dt.ToString());
            return this.ExecQuery(SQLFormat, ref ds);
        }

        public int CheckOutMission(string ID, string checkOutOperName, DateTime checkOutTime)
        {
            string SQL = "";

            if (this.Sql.GetCommonSql("SOC.System.Project.UpdateMission.ByCheckOut", ref SQL) == -1)
            {
                SQL = @"
                        update soc_project_mission
                           set lock_state = '是',
                               lock_oper  = '{1}',
                               lock_time  = to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
                         where id = '{0}'
                           and (lock_state = '否' or lock_state is null or lock_state = '')
                        ";
                this.Sql.CacheSql("SOC.System.Project.UpdateMission.ByCheckOut", SQL);
            }
            string SQLFormat = string.Format(SQL, ID, checkOutOperName, checkOutTime.ToString());
            return this.ExecNoQuery(SQLFormat);
        }

        public int CancelCheckOutMission(string ID, string checkOutOperName)
        {
            string SQL = "";

            if (this.Sql.GetCommonSql("SOC.System.Project.UpdateMission.ByCancelCheckOut", ref SQL) == -1)
            {
                SQL = @"
                        update soc_project_mission
                           set lock_state = '否',
                               lock_oper  = ''
                         where id = '{0}'
                           and lock_state = '是'
                           and lock_oper  = '{1}'
                        ";
                this.Sql.CacheSql("SOC.System.Project.UpdateMission.ByCancelCheckOut", SQL);
            }
            string SQLFormat = string.Format(SQL, ID, checkOutOperName);
            return this.ExecNoQuery(SQLFormat);
        }

        public string GetMissionPrimaryKey()
        {
            string SQL = "";

            if (this.Sql.GetCommonSql("SOC.System.Project.Mission.GetSequence", ref SQL) == -1)
            {
                SQL = @"select soc_project_mission_id.nextval from dual";
                this.Sql.CacheSql("SOC.System.Project.Mission.GetSequence", SQL);
            }
            return this.ExecSqlReturnOne(SQL);
        }

        public ArrayList QueryAllModels()
        {
            string val = @"门诊挂号,门诊收费,门诊药房,门诊护士,门诊医生,门诊报表,住院登记,住院报表,住院收费,住院护士,住院医生,住院药房,住院收费,住院报表,药库,电子病历,物资,设备,供应室,DCP,电子申请单,LIS接口,PACS接口,应急系统,其他";
            val = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "Param", "Models", val);
            string[] items = val.Split(',');
            ArrayList al = new ArrayList();
            int id = 1;
            foreach (string item in items)
            {
                FS.HISFC.Models.Base.Spell o = new FS.HISFC.Models.Base.Spell();
                o.ID = id.ToString();
                o.Name = item;
                o.SpellCode = this.GetSpellCode(o.Name);
                al.Add(o);
                id++;
            }

            return al;

        }

        public ArrayList QueryAllStates()
        {
            string val = @"未完成,完成,需再了解,待确定,延迟处理,不需要处理";
            val = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "Param", "States", val);
            string[] items = val.Split(',');
            ArrayList al = new ArrayList();
            int id = 1;
            foreach (string item in items)
            {
                FS.HISFC.Models.Base.Spell o = new FS.HISFC.Models.Base.Spell();
                o.ID = id.ToString();
                o.Name = item;
                o.SpellCode = this.GetSpellCode(o.Name);
                al.Add(o);
                id++;
            }

            return al;

        }

        public ArrayList QueryAllMissionAccepters()
        {
            string val = "";
            val = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "Param", "Accepters", val);
            if (val == "")
            {
                SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "Param", "Accepters", "PM,院方");
            }
            string[] items = val.Split(',');
            ArrayList al = new ArrayList();
            int id = 1;
            foreach (string item in items)
            {
                FS.HISFC.Models.Base.Spell o = new FS.HISFC.Models.Base.Spell();
                o.ID = id.ToString();
                o.Name = item;
                o.SpellCode = this.GetSpellCode(o.Name);
                al.Add(o);
                id++;
            }

            return al;

        }

        public ArrayList QueryAllFuncionTypes()
        {
            string val = @"业务流程,业务功能,系统管理,系统优化,数据维护,数据库变更,sql变更,查询,报表,单据打印,UI,A类系统bug,B类流程bug,C类功能bug,D类一般性bug,其它";
            val = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "Param", "FuncionTypes", val);
            string[] items = val.Split(',');
            ArrayList al = new ArrayList();
            int id = 1;
            foreach (string item in items)
            {
                FS.HISFC.Models.Base.Spell o = new FS.HISFC.Models.Base.Spell();
                o.ID = id.ToString();
                o.Name = item;
                o.SpellCode = this.GetSpellCode(o.Name);
                al.Add(o);
                id++;
            }

            return al;

        }

        public ArrayList QueryAllPriorityLevels()
        {
            string val = @"紧急A,重要1,不紧急B,不重要2,A1,A2,B1,B2";
            val = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "Param", "FuncionTypes", val);
            string[] items = val.Split(',');
            ArrayList al = new ArrayList();
            int id = 1;
            foreach (string item in items)
            {
                FS.HISFC.Models.Base.Spell o = new FS.HISFC.Models.Base.Spell();
                o.ID = id.ToString();
                o.Name = item;
                o.SpellCode = this.GetSpellCode(o.Name);
                al.Add(o);
                id++;
            }

            return al;

        }

        /// <summary>
        /// 取单个字符的拼音首字母
        /// </summary>
        /// <param name="str">要转换的单个字符</param>
        /// <returns>拼音声母(I、U、V不能返回)</returns>
        public string GetSpellChar(string str)
        {
            if (str.CompareTo("吖") < 0) return "";
            if (str.CompareTo("八") < 0) return "A";
            if (str.CompareTo("嚓") < 0) return "B";
            if (str.CompareTo("咑") < 0) return "C";
            if (str.CompareTo("妸") < 0) return "D";
            if (str.CompareTo("发") < 0) return "E";
            if (str.CompareTo("旮") < 0) return "F";
            if (str.CompareTo("铪") < 0) return "G";
            if (str.CompareTo("讥") < 0) return "H";
            if (str.CompareTo("咔") < 0) return "J";
            if (str.CompareTo("垃") < 0) return "K";
            if (str.CompareTo("嘸") < 0) return "L";
            if (str.CompareTo("拏") < 0) return "M";
            if (str.CompareTo("噢") < 0) return "N";
            if (str.CompareTo("妑") < 0) return "O";
            if (str.CompareTo("七") < 0) return "P";
            if (str.CompareTo("亽") < 0) return "Q";
            if (str.CompareTo("仨") < 0) return "R";
            if (str.CompareTo("他") < 0) return "S";
            if (str.CompareTo("哇") < 0) return "T";
            if (str.CompareTo("夕") < 0) return "W";
            if (str.CompareTo("丫") < 0) return "X";
            if (str.CompareTo("帀") < 0) return "Y";
            if (str.CompareTo("咗") < 0) return "Z";
            return "";
        }

        public string GetSpellCode(string str)
        {


            string[] xnzm = new string[10];
            xnzm[0] = "Y";
            xnzm[1] = "E";
            xnzm[2] = "S";
            xnzm[3] = "S";
            xnzm[4] = "W";
            xnzm[5] = "L";
            xnzm[6] = "Q";
            xnzm[7] = "B";
            xnzm[8] = "J";
            xnzm[9] = "S";

            //str = "辶廴亠";
            string tempStr = "";
            foreach (char c in str)
            {
                if ((int)c >= 48 && (int)c <= 57)//0-9
                {
                    tempStr += c.ToString();
                }
                else if ((int)c >= 65 && (int)c <= 90)//A-Z
                {
                    tempStr += c.ToString();
                }
                else if ((int)c >= 97 && (int)c <= 122)//a-z
                {
                    tempStr += c.ToString();
                }
                else if ((int)c >= 8544 && (int)c <= 8555)//ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩⅪⅫ,完美吧
                {
                    tempStr += xnzm[(int)c - 8544];
                }
                else
                {
                    tempStr += GetSpellChar(c.ToString()); //累加拼音首字母
                }
            }
            return tempStr;
        }
    }
}