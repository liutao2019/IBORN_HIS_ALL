using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDLY.Array
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
            string reglevlCode = "";
            string regDiagItemCode = "";
            FS.HISFC.Models.Base.Employee doct = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(queueObj.Doctor.ID);


            FS.HISFC.Models.Registration.RegLevel regl = regMgr.QueryRegLevelByCode(regObj.DoctorInfo.Templet.RegLevel.ID);

            //普通号进了专家～
            if (!regl.IsExpert&& queueObj.ExpertFlag == "1")
            {
                if (MessageBox.Show("普通挂号进诊专家队列" + "是否继续？", "提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) 
                    return -1;
            }


            if (regMgr.GetSupplyRegInfo(doct.ID, doct.Level.ID, queueObj.Dept.ID, ref reglevlCode, ref regDiagItemCode) == -1)
            {
                MessageBox.Show(regMgr.Err);
                return -1;
            }
            FS.HISFC.Models.Registration.RegLevel doctRegLevl = FS.SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(reglevlCode);
            if (regObj.DoctorInfo.Templet.RegLevel.ID != doctRegLevl.ID)
            {
                if (MessageBox.Show("您看诊的挂号级别为:【" + doctRegLevl.Name + "】，\r\n而患者的挂号级别为：【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】\r\n\r\n是否继续看诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    //errInfo = "您已经取消操作！";
                    return -1;
                }
            }
            return 1;
        }

        private static ArrayList alNoon = null;

        /// <summary>
        /// 获取午别
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static string GetNoon(DateTime current)
        {
            if (alNoon == null)
            {
                FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

                alNoon = noonMgr.Query();
            }
            if (alNoon == null)
            {
                return "";
            }
            int time = int.Parse(current.ToString("HHmmss"));

            foreach (FS.HISFC.Models.Base.Noon obj in alNoon)
            {
                if (time >= int.Parse(obj.StartTime.ToString("HHmmss")) &&
                   time <= int.Parse(obj.EndTime.ToString("HHmmss")))
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