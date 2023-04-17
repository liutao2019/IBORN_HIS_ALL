using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace SOC.HISFC.Components.Nurse.Controls.GYSY
{
    public class Function
    {
        public Function()
        {
        }

        private static FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 分诊前的判断
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="queueObj"></param>
        /// <returns></returns>
        public static int CheckArray(FS.HISFC.Models.Registration.Register regObj, FS.HISFC.Models.Nurse.Queue queueObj)
        {
            string reglevlCode = "", regDiagItemCode = "";
            
            FS.HISFC.Models.Base.Employee doct = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(queueObj.Doctor.ID);
            FS.HISFC.Models.Registration.RegLevel regl = regMgr.QueryRegLevelByCode(regObj.DoctorInfo.Templet.RegLevel.ID);

            if (regMgr.GetSupplyRegInfo(doct.ID, doct.Level.ID, queueObj.Dept.ID, ref reglevlCode, ref regDiagItemCode) == -1)
            {
                MessageBox.Show(regMgr.Err);
                return -1;
            }
            FS.HISFC.Models.Registration.RegLevel doctRegLevl = FS.SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(reglevlCode);
            if (regObj.DoctorInfo.Templet.RegLevel.ID != doctRegLevl.ID)
            {
                string tip = "该医生队列的挂号级别为:【" + doctRegLevl.Name + "】\n\n"
                      + "患者的挂号级别为：【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】\n\n\n是否要继续分诊？";
                if (MessageBox.Show(tip, "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 获取午别
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static string GetNoon(DateTime current)
        {
            ArrayList alNoon = regMgr.Query();
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

            FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            try
            {
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
                
                //2、修改分诊信息表----先update，无数据再insert
                if (assignMgr.Update(assign) <= 0)
                {
                    if (assignMgr.Insert(assign) == -1)
                    {
                        error = assignMgr.Err;
                        return -1;
                    }
                }
                

                //3、更新挂号信息表，置为已分诊标志
                if (regMgr.Update(assign.Register.ID, FS.FrameWork.Management.Connection.Operator.ID,
                    regMgr.GetDateTimeFromSysDateTime()) == -1)
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

            try
            {
                //要更新的数据
                assign.Queue.ID = ""; //队列代码
                assign.Queue.Name = ""; //队列名
                assign.Queue.Doctor.ID = ""; //看诊医生code
                assign.Queue.SRoom.ID = "";  //诊室号
                assign.Queue.SRoom.Name = ""; //诊室名称
                assign.Queue.Console.ID = ""; //诊台号
                assign.Queue.Console.Name = ""; //诊台名称
                
                //int rtn = assignMgr.Delete(assign);
                int rtn = assignMgr.Update(assign);//改为更新，防止取消分诊后在科室患者中也找不到
                
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


       // public static int GetTriagePatient()
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