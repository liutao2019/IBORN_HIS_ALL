using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;
using FS.HISFC.Models.RADT;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY
{
    public class Function
    {
        public Function()
        {
        }

        /// <summary>
        /// 获取午别
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static string GetNoon(DateTime current)
        {
            FS.HISFC.BizProcess.Integrate.Registration.Registration schemaMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            ArrayList alNoon = schemaMgr.Query();
            if (alNoon == null) return "";
            /*
             * 实际午别为医生出诊时间段,上午可能为08~11:30，下午为14~17:30
             * 所以挂号员如果不在这个时间段挂号,就有可能提示午别未维护
             * 所以改为根据传人时间所在的午别例如：9：30在06~12之间，那么就判断是否有午别在
             * 06~12之间，全包含就说明9:30是那个午别代码
             */

            int[,] zones = new int[,] { { 0, 120000 }, { 120000, 180000 }, { 180000, 235959 } };
            int time = int.Parse(current.ToString("HHmmss"));
            int begin = 0, end = 0;

            for (int i = 0; i < 3; i++)
            {
                if (zones[i, 0] <= time && zones[i, 1] > time)
                {
                    begin = zones[i, 0];
                    end = zones[i, 1];
                    break;
                }
            }

            foreach (FS.HISFC.Models.Registration.Noon obj in alNoon)
            {
                if (int.Parse(obj.BeginTime.ToString("HHmmss")) >= begin &&
                    int.Parse(obj.EndTime.ToString("HHmmss")) <= end)
                {
                    return obj.ID;
                }
            }

            return "";
        }

        /// <summary>
        /// 分诊
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="assign"></param>
        /// <param name="TrigeWhereFlag">分诊标志 1.分到队列  2.分到诊台</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static int Triage(FS.HISFC.Models.Nurse.Assign assign,
            string TrigeWhereFlag, ref string error)
        {

            FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();
            FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
            FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            try
            {
                //assignMgr.SetTrans(trans.Trans);
                //regMgr.SetTrans(trans.Trans);

                //1、获取队列最大看诊序号
                //assign.SeeNO = assignMgr.Query((assign.Queue as FS.FrameWork.Models.NeuObject));
                //if (assign.SeeNO == -1)
                //{
                //    error = assignMgr.Err;
                //    return -1;
                //}

                //assign.SeeNO = assign.SeeNO + 1;

                if (!string.IsNullOrEmpty(assign.Queue.Memo))
                {
                    string[] bookNum = assign.Queue.Memo.Split('|');

                    for (int i = 0; i < bookNum.Length; i++)
                    {
                        if (assign.SeeNO.ToString().Contains(bookNum[i]))
                        {
                            assign.SeeNO = assign.SeeNO + 1;
                        }
                    }

                }
                //1、获取队列最大看诊序号
                string nurseID = ((FS.HISFC.Models.Base.Employee)assignMgr.Operator).Dept.ID;
                ArrayList alNurse = conMgr.QueryDepartmentForArray(nurseID);
                if (alNurse == null || alNurse.Count <= 0)
                {
                    error = "更新看诊序号失败！";
                    return -1;
                }

                string Type = "", Subject = "";
                int seeNo = 0;

                #region 患者排号方式

                //if (this.strGetSeeNoType == "1" &&
                //    doctID != null && doctID != "")
                //{
                //    Type = "1";//医生
                //    Subject = doctID;
                //}
                //else if (this.strGetSeeNoType == "2")
                //{
                //    Type = "2";//科室
                //    Subject = deptID;
                //}
                //else if (this.strGetSeeNoType == "3")
                //{
                //    Type = "3";//诊区
                //    Subject = deptID;
                //}

                #endregion

                Type = "3";//诊区
                Subject = nurseID;
                string noonID = GetNoon(assignMgr.GetDateTimeFromSysDateTime());
                //更新看诊序号
                if (regMgr.UpdateSeeNo(Type, assign.Register.DoctorInfo.SeeDate, Subject, noonID) == -1)
                {
                    error = regMgr.Err;
                    return -1;
                }

                //获取看诊序号		
                if (regMgr.GetSeeNo(Type, assign.Register.DoctorInfo.SeeDate, Subject, noonID, ref seeNo) == -1)
                {
                    error = regMgr.Err;
                    return -1;
                }
                assign.SeeNO = seeNo;
                //专家的直接取 时间段内的看诊序号
                //				if(FS.neFS.HISFC.Components.Function.NConvert.ToInt32(assign.Register.IsPre) == 1)
                //if (assign.Register.DoctorInfo.Templet.Doct.ID != null && assign.Register.DoctorInfo.Templet.Doct.ID != "")
                //{
                //    assign.SeeNO = assign.Register.DoctorInfo.SeeNO;
                //}

                //2、插入分诊信息表
                if (assignMgr.Insert(assign) == -1)
                {
                    error = assignMgr.Err;
                    return -1;
                }

                //3、更新挂号信息表，置分诊标志
                FS.HISFC.BizLogic.Nurse.Assign a = new FS.HISFC.BizLogic.Nurse.Assign();
                //a.SetTrans(trans.Trans);
                if (regMgr.Update(assign.Register.ID, FS.FrameWork.Management.Connection.Operator.ID,
                    a.GetDateTimeFromSysDateTime()/*regMgr.GetDateTimeFromSysDateTime()*/) == -1)
                {
                    error = regMgr.Err;
                    return -1;
                }
                //4.队列数量增加1
                if (assignMgr.UpdateQueue(assign.Queue.ID, "1") == -1)
                {
                    error = assignMgr.Err;
                    return -1;
                }

            }
            catch (Exception e)
            {
                error = e.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="assign"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static int CancelTriage(FS.HISFC.Models.Nurse.Assign assign, ref string error)
        {
            FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();

            FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            try
            {
                //assignMgr.SetTrans(trans.Trans);
                //regMgr.SetTrans(trans.Trans);

                //删除分诊信息
                int rtn = assignMgr.Delete(assign);
                if (rtn == -1)//出错
                {
                    error = assignMgr.Err;
                    return -1;
                }

                if (rtn == 0)
                {
                    error = "该分诊信息状态已经发生改变,请刷新屏幕!";
                    return -1;
                }
                //恢复挂号信息的分诊状态
                rtn = regMgr.CancelTriage(assign.Register.ID);
                if (rtn == -1)
                {
                    error = regMgr.Err;
                    return -1;
                }
                //4.队列数量-1
                if (assignMgr.UpdateQueue(assign.Queue.ID, "-1") == -1)
                {
                    error = assignMgr.Err;
                    return -1;
                }
            }
            catch (Exception e)
            {
                error = e.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 新建XML
        /// </summary>
        /// <returns></returns>
        public static int CreateXML(string fileName, string extendTime, string opertime)
        {
            string path;
            try
            {
                path = fileName.Substring(0, fileName.LastIndexOf(@"\"));
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch { }

            FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            root = myXml.CreateRootElement(doc, "Setting", "1.0");

            XmlElement e = myXml.AddXmlNode(doc, root, "延长队列", "");
            myXml.AddNodeAttibute(e, "ExtendTime", extendTime);

            e = myXml.AddXmlNode(doc, root, "保存时间", "");
            myXml.AddNodeAttibute(e, "LastOperTime", opertime);

            try
            {
                StreamWriter sr = new StreamWriter(fileName, false, System.Text.Encoding.Default);
                string cleandown = doc.OuterXml;
                sr.Write(cleandown);
                sr.Close();
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show("无法保存！" + ex.Message); }

            return 1;
        }
    }

    /// <summary>
    /// 婴儿登记业务处理
    /// </summary>
    public class BabyManager : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 构造
        /// </summary>
        public BabyManager()
        {

        }

        #region 婴儿登记表
        /// <summary>
        /// 插入婴儿扩展表
        /// </summary>
        /// <param name="IsMatherHBAP">母亲是否乙肝抗原阳性 1是 0不是</param>
        /// <param name="ISImmu">婴儿是否已经注射高效免疫球d蛋白 1是 0不是</param>
        /// <param name="babyInfo">婴儿实体</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int InsertBabyInfoExtend(PatientInfo babyInfo, string IsMatherHBAP, string ISImmu)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("SDLocal.RADT.Inpatient.InsertBabyInfoExtend", ref strSql) == -1)
            {
                this.Err = "没有找到SDLocal.RADT.Inpatient.InsertBabyInfoExtend字段!";
                return -1;
            }

            #region sql语句
            //          INSERT INTO FIN_IPR_BABYINFO_EXTEND 
            //(INPATIENT_NO, 
            //HAPPEN_NO, 
            //ISMOMHBAP, 
            //ISIMMU, 
            //OPER_CODE, 
            //OPER_DATE, 
            //EXTEND1, 
            //EXTEND2, 
            //EXTEND3)
            //VALUES 
            //('{0}', 
            //{1}, 
            //'{2}', 
            //'{3}', 
            //'{4}', 
            //SYSDATE, 
            //NULL, 
            //NULL, 
            //NULL);
            #endregion

            try
            {
                strSql = string.Format(strSql, babyInfo.ID, babyInfo.User01, IsMatherHBAP, ISImmu, Operator.ID);
            }
            catch (Exception ex)
            {
                Err = "参数赋值时候出错！" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新婴儿登记表 更新两个字段
        /// </summary>
        /// <param name="IsMatherHBAP">母亲是否乙肝抗原阳性 1是 0不是</param>
        /// <param name="ISImmu">婴儿是否已经注射高效免疫球d蛋白 1是 0不是</param>
        /// <param name="babyInfo">婴儿实体</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateBabyInfo(string IsMatherHBAP, string ISImmu, PatientInfo babyInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("SDLocal.RADT.Inpatient.UpdateBabyInfo", ref strSql) == -1)
            {
                this.Err = "没有找到SDLocal.RADT.Inpatient.UpdateBabyInfo字段!";
                return -1;
            }

            #region sql语句
            //  update fin_ipr_babyinfo
            // set ISMATHERHBAP = '{0}',  --母亲是否乙肝抗原阳性 1是 0不是
            //     ISINJECTEDIMMU = '{1}' --婴儿是否已经注射高效免疫球蛋白 1是 0不是
            //where INPATIENT_NO = '{2}'  --住院流水号
            // and  HAPPEN_NO = '{3}'     --发生序号
            #endregion

            try
            {
                strSql = string.Format(strSql, IsMatherHBAP, ISImmu, babyInfo.ID, babyInfo.User01);
            }
            catch (Exception ex)
            {
                Err = "参数赋值时候出错！" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// 由住院号获取婴儿的两个扩展字段: 母亲是否乙肝抗原阳性 和 婴儿是否已经注射高效免疫球蛋白
        /// </summary>
        /// <param name="patiengNO">婴儿住院号</param>
        /// <returns>成功返回NeuObject 失败返回null</returns>
        public FS.FrameWork.Models.NeuObject GetBabyInfoExtend(string patiengNO)
        {
            string strSql = string.Empty;

            if (Sql.GetSql("SDLocal.RADT.Inpatient.GetBabyInfoExtend", ref strSql) == -1)
            {
                this.Err = "没有找到SDLocal.RADT.Inpatient.GetBabyInfoExtend字段!";
                return null;
            }

            #region sql语句
            //select b.inpatient_no, b.ismatherhbap, b.isinjectedimmu
            // from fin_ipr_babyinfo b
            //where b.inpatient_no = '{0}'
            #endregion

            strSql = string.Format(strSql, patiengNO);

            return this.GetBabyExtend(strSql);
        }

        /// <summary>
        /// 由住院号获取婴儿的两个扩展字段: 母亲是否乙肝抗原阳性 和 婴儿是否已经注射高效免疫球蛋白
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>成功返回NeuObject 失败返回null</returns>
        private FS.FrameWork.Models.NeuObject GetBabyExtend(string sql)
        {
            FS.FrameWork.Models.NeuObject baby = new FS.FrameWork.Models.NeuObject();

            if (ExecQuery(sql) == -1)
            {
                return null;
            }

            try
            {
                while (Reader.Read())
                {
                    try
                    {
                        baby = new FS.FrameWork.Models.NeuObject();
                        if (!Reader.IsDBNull(0)) baby.ID = Reader[0].ToString();
                        if (!Reader.IsDBNull(1)) baby.User01 = Reader[1].ToString();
                        if (!Reader.IsDBNull(2)) baby.User02 = Reader[2].ToString();
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
            }
            catch (Exception e)
            {
                Err = "获取婴儿信息失败！" + e.Message;
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return baby;
            }

            Reader.Close();
            return baby;
        }
        #endregion
    }

    [System.Xml.Serialization.XmlRoot()]
    public struct RefreshFrequence
    {
        /// <summary>
        /// 如果为:10则代表十秒
        /// 
        /// 默认为:"no"不刷新
        /// </summary>
        public string RefreshTime;

        /// <summary>
        /// 是否允许自动分诊
        /// </summary>
        public bool IsAutoTriage;
    }
}