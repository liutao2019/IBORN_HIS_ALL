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
        /// ����ǰ���ж�
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
                string tip = "��ҽ�����еĹҺż���Ϊ:��" + doctRegLevl.Name + "��\n\n"
                      + "���ߵĹҺż���Ϊ����" + regObj.DoctorInfo.Templet.RegLevel.Name + "��\n\n\n�Ƿ�Ҫ�������";
                if (MessageBox.Show(tip, "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static string GetNoon(DateTime current)
        {
            ArrayList alNoon = regMgr.Query();
            if (alNoon == null) return "";
            /*
             * ʵ�����Ϊҽ������ʱ���,�������Ϊ08~11:30������Ϊ14~17:30
             * ���ԹҺ�Ա����������ʱ��ιҺ�,���п�����ʾ���δά��
             * ���Ը�Ϊ���ݴ���ʱ�����ڵ�������磺9��30��06~12֮�䣬��ô���ж��Ƿ��������
             * 06~12֮�䣬ȫ������˵��9:30���Ǹ�������
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
        /// ����
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="assign"></param>
        /// <param name="TrigeWhereFlag">�����־ 1.�ֵ�����  2.�ֵ���̨</param>
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
                //1����ȡ������������
                string nurseID = ((FS.HISFC.Models.Base.Employee)assignMgr.Operator).Dept.ID;
                ArrayList alNurse = conMgr.QueryDepartmentForArray(nurseID);
                if (alNurse == null || alNurse.Count <= 0)
                {
                    error = "���¿������ʧ�ܣ�";
                    return -1;
                }

                string Type = "", Subject = "";
                int seeNo = 0;

                Type = "3";//����
                Subject = nurseID;
                string noonID = GetNoon(assignMgr.GetDateTimeFromSysDateTime());
                //���¿������
                if (regMgr.UpdateSeeNo(Type, assign.Register.DoctorInfo.SeeDate, Subject, noonID) == -1)
                {
                    error = regMgr.Err;
                    return -1;
                }

                //��ȡ�������		
                if (regMgr.GetSeeNo(Type, assign.Register.DoctorInfo.SeeDate, Subject, noonID, ref seeNo) == -1)
                {
                    error = regMgr.Err;
                    return -1;
                }
                assign.SeeNO = seeNo;
                
                //2���޸ķ�����Ϣ��----��update����������insert
                if (assignMgr.Update(assign) <= 0)
                {
                    if (assignMgr.Insert(assign) == -1)
                    {
                        error = assignMgr.Err;
                        return -1;
                    }
                }
                

                //3�����¹Һ���Ϣ����Ϊ�ѷ����־
                if (regMgr.Update(assign.Register.ID, FS.FrameWork.Management.Connection.Operator.ID,
                    regMgr.GetDateTimeFromSysDateTime()) == -1)
                {
                    error = regMgr.Err;
                    return -1;
                }
                //4.������������1
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
        /// ȡ������
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
                //Ҫ���µ�����
                assign.Queue.ID = ""; //���д���
                assign.Queue.Name = ""; //������
                assign.Queue.Doctor.ID = ""; //����ҽ��code
                assign.Queue.SRoom.ID = "";  //���Һ�
                assign.Queue.SRoom.Name = ""; //��������
                assign.Queue.Console.ID = ""; //��̨��
                assign.Queue.Console.Name = ""; //��̨����
                
                //int rtn = assignMgr.Delete(assign);
                int rtn = assignMgr.Update(assign);//��Ϊ���£���ֹȡ��������ڿ��һ�����Ҳ�Ҳ���
                
                if (rtn == -1)//����
                {
                    error = assignMgr.Err;
                    return -1;
                }

                if (rtn == 0)
                {
                    error = "�÷�����Ϣ״̬�Ѿ������ı�,��ˢ����Ļ!";
                    return -1;
                }
                //�ָ��Һ���Ϣ�ķ���״̬
                rtn = regMgr.CancelTriage(assign.Register.ID);
                if (rtn == -1)
                {
                    error = regMgr.Err;
                    return -1;
                }
                //4.��������-1
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
        /// �½�XML
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

            XmlElement e = myXml.AddXmlNode(doc, root, "�ӳ�����", "");
            myXml.AddNodeAttibute(e, "ExtendTime", extendTime);

            e = myXml.AddXmlNode(doc, root, "����ʱ��", "");
            myXml.AddNodeAttibute(e, "LastOperTime", opertime);

            try
            {
                StreamWriter sr = new StreamWriter(fileName, false, System.Text.Encoding.Default);
                string cleandown = doc.OuterXml;
                sr.Write(cleandown);
                sr.Close();
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show("�޷����棡" + ex.Message); }

            return 1;
        }


    }

    [System.Xml.Serialization.XmlRoot()]
    public struct RefreshFrequence
    {
        /// <summary>
        /// ���Ϊ:10�����ʮ��
        /// 
        /// Ĭ��Ϊ:"no"��ˢ��
        /// </summary>
        public string RefreshTime;

        /// <summary>
        /// �Ƿ������Զ�����
        /// </summary>
        public bool IsAutoTriage;
    }
}