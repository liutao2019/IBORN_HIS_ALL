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
        /// ��ȡ���
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static string GetNoon(DateTime current)
        {
            FS.HISFC.BizProcess.Integrate.Registration.Registration schemaMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            ArrayList alNoon = schemaMgr.Query();
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
            FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
            FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            try
            {
                //assignMgr.SetTrans(trans.Trans);
                //regMgr.SetTrans(trans.Trans);

                //1����ȡ������������
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

                #region �����źŷ�ʽ

                //if (this.strGetSeeNoType == "1" &&
                //    doctID != null && doctID != "")
                //{
                //    Type = "1";//ҽ��
                //    Subject = doctID;
                //}
                //else if (this.strGetSeeNoType == "2")
                //{
                //    Type = "2";//����
                //    Subject = deptID;
                //}
                //else if (this.strGetSeeNoType == "3")
                //{
                //    Type = "3";//����
                //    Subject = deptID;
                //}

                #endregion

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
                //ר�ҵ�ֱ��ȡ ʱ����ڵĿ������
                //				if(FS.neFS.HISFC.Components.Function.NConvert.ToInt32(assign.Register.IsPre) == 1)
                //if (assign.Register.DoctorInfo.Templet.Doct.ID != null && assign.Register.DoctorInfo.Templet.Doct.ID != "")
                //{
                //    assign.SeeNO = assign.Register.DoctorInfo.SeeNO;
                //}

                //2�����������Ϣ��
                if (assignMgr.Insert(assign) == -1)
                {
                    error = assignMgr.Err;
                    return -1;
                }

                //3�����¹Һ���Ϣ���÷����־
                FS.HISFC.BizLogic.Nurse.Assign a = new FS.HISFC.BizLogic.Nurse.Assign();
                //a.SetTrans(trans.Trans);
                if (regMgr.Update(assign.Register.ID, FS.FrameWork.Management.Connection.Operator.ID,
                    a.GetDateTimeFromSysDateTime()/*regMgr.GetDateTimeFromSysDateTime()*/) == -1)
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

            FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            try
            {
                //assignMgr.SetTrans(trans.Trans);
                //regMgr.SetTrans(trans.Trans);

                //ɾ��������Ϣ
                int rtn = assignMgr.Delete(assign);
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

    /// <summary>
    /// Ӥ���Ǽ�ҵ����
    /// </summary>
    public class BabyManager : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ����
        /// </summary>
        public BabyManager()
        {

        }

        #region Ӥ���ǼǱ�
        /// <summary>
        /// ����Ӥ����չ��
        /// </summary>
        /// <param name="IsMatherHBAP">ĸ���Ƿ��Ҹο�ԭ���� 1�� 0����</param>
        /// <param name="ISImmu">Ӥ���Ƿ��Ѿ�ע���Ч������d���� 1�� 0����</param>
        /// <param name="babyInfo">Ӥ��ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int InsertBabyInfoExtend(PatientInfo babyInfo, string IsMatherHBAP, string ISImmu)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("SDLocal.RADT.Inpatient.InsertBabyInfoExtend", ref strSql) == -1)
            {
                this.Err = "û���ҵ�SDLocal.RADT.Inpatient.InsertBabyInfoExtend�ֶ�!";
                return -1;
            }

            #region sql���
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
                Err = "������ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����Ӥ���ǼǱ� ���������ֶ�
        /// </summary>
        /// <param name="IsMatherHBAP">ĸ���Ƿ��Ҹο�ԭ���� 1�� 0����</param>
        /// <param name="ISImmu">Ӥ���Ƿ��Ѿ�ע���Ч������d���� 1�� 0����</param>
        /// <param name="babyInfo">Ӥ��ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int UpdateBabyInfo(string IsMatherHBAP, string ISImmu, PatientInfo babyInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("SDLocal.RADT.Inpatient.UpdateBabyInfo", ref strSql) == -1)
            {
                this.Err = "û���ҵ�SDLocal.RADT.Inpatient.UpdateBabyInfo�ֶ�!";
                return -1;
            }

            #region sql���
            //  update fin_ipr_babyinfo
            // set ISMATHERHBAP = '{0}',  --ĸ���Ƿ��Ҹο�ԭ���� 1�� 0����
            //     ISINJECTEDIMMU = '{1}' --Ӥ���Ƿ��Ѿ�ע���Ч�����򵰰� 1�� 0����
            //where INPATIENT_NO = '{2}'  --סԺ��ˮ��
            // and  HAPPEN_NO = '{3}'     --�������
            #endregion

            try
            {
                strSql = string.Format(strSql, IsMatherHBAP, ISImmu, babyInfo.ID, babyInfo.User01);
            }
            catch (Exception ex)
            {
                Err = "������ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��סԺ�Ż�ȡӤ����������չ�ֶ�: ĸ���Ƿ��Ҹο�ԭ���� �� Ӥ���Ƿ��Ѿ�ע���Ч�����򵰰�
        /// </summary>
        /// <param name="patiengNO">Ӥ��סԺ��</param>
        /// <returns>�ɹ�����NeuObject ʧ�ܷ���null</returns>
        public FS.FrameWork.Models.NeuObject GetBabyInfoExtend(string patiengNO)
        {
            string strSql = string.Empty;

            if (Sql.GetSql("SDLocal.RADT.Inpatient.GetBabyInfoExtend", ref strSql) == -1)
            {
                this.Err = "û���ҵ�SDLocal.RADT.Inpatient.GetBabyInfoExtend�ֶ�!";
                return null;
            }

            #region sql���
            //select b.inpatient_no, b.ismatherhbap, b.isinjectedimmu
            // from fin_ipr_babyinfo b
            //where b.inpatient_no = '{0}'
            #endregion

            strSql = string.Format(strSql, patiengNO);

            return this.GetBabyExtend(strSql);
        }

        /// <summary>
        /// ��סԺ�Ż�ȡӤ����������չ�ֶ�: ĸ���Ƿ��Ҹο�ԭ���� �� Ӥ���Ƿ��Ѿ�ע���Ч�����򵰰�
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>�ɹ�����NeuObject ʧ�ܷ���null</returns>
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
                Err = "��ȡӤ����Ϣʧ�ܣ�" + e.Message;
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