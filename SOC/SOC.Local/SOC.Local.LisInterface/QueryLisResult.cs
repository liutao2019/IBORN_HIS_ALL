using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace SOC.Local.LisInterface
{
    /// <summary>
    /// [��������: ҽ��վ�鿴Lis���]<br></br>
    /// [�� �� ��: zhaoj]<br></br>
    /// [����ʱ��: 2009-07-23]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class QueryLisResult:FS.HISFC.BizProcess.Interface.Common.ILis
    {
        #region �����

        /// <summary>
        /// �������
        /// </summary>
        private FS.HISFC.Models.RADT.EnumPatientType patientType = FS.HISFC.Models.RADT.EnumPatientType.C;

        private string errMsg = "";

        #endregion

        #region ILis ��Ա

        public bool CheckOrder(FS.HISFC.Models.Order.Order order)
        {
            return false;
        }

        public int Commit()
        {
            return 1;
        }

        public int Connect()
        {
            return 1;
        }

        public int Disconnect()
        {
            return 1;
        }

        public string ErrCode
        {
            get
            {
                return "";
            }
        }
        
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }
        }

        string resultType = "";
        public string ResultType
        {
            get
            {
                return resultType;
            }
            set
            {
                resultType = value;
            }
        }

        public bool IsReportValid(string id)
        {
            return false;
        }

        public int PlaceOrder(ICollection<FS.HISFC.Models.Order.Order> orders)
        {
            return 1;
        }

        public int PlaceOrder(FS.HISFC.Models.Order.Order order)
        {
            return 1;
        }

        public string[] QueryResult()
        {
            return new string[] { };
        }

        public int Rollback()
        {
            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            return;
        }

        public int ShowResult(string id)
        {
            return 1;
        }

        /// <summary>
        /// ��ʾLis���
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public int ShowResultByPatient()
        {
            string inpatientNo = myPatientInfo.PID.CardNO;

            string arguments = "";

            if (this.PatientType == FS.HISFC.Models.RADT.EnumPatientType.C)
            {
                if (string.IsNullOrEmpty(inpatientNo))
                {
                    this.errMsg = "���￨�Ų���Ϊ�գ�";
                    return -1;
                }
                arguments = inpatientNo + " " + "����";
            }
            else if (this.PatientType == FS.HISFC.Models.RADT.EnumPatientType.I)
            {
                if (string.IsNullOrEmpty(inpatientNo))
                {
                    this.errMsg = "סԺ�Ų���Ϊ�գ�";
                    return -1;
                }
                arguments = myPatientInfo.PID .PatientNO+ " " + "סԺ";
            }

            string loadPath = "";
            string process = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\LisInterface.xml"))
            {
                XmlDocument file = new XmlDocument();
                file.Load(Application.StartupPath + "\\LisInterface.xml");
                XmlNode node = file.SelectSingleNode("Config/StartPath");
                if (node != null)
                {
                    loadPath = node.InnerText;
                }
                node = file.SelectSingleNode("Config/Process");
                if (node != null)
                {
                    process = node.InnerText;
                }
            }

            if (string.IsNullOrEmpty(loadPath + process))
            {
                this.errMsg = "��ά��LIS���������Ŀ¼�������ļ�Ϊ��Ŀ¼��LisInteface.xml��";
                return -1;
            }

            //��ȡ·��
            if (System.IO.File.Exists(Application.StartupPath + loadPath + process))
            {
                try
                {
                    //System.Diagnostics.Process.Start(Application.StartupPath + loadPath, arguments);

                    System.Diagnostics.Process pro = new System.Diagnostics.Process();
                    pro.StartInfo.WorkingDirectory = Application.StartupPath + loadPath;
                    System.Diagnostics.Process.Start(Application.StartupPath + loadPath, arguments);
                }
                catch (Exception e)
                {
                    this.errMsg = "����Lis�������" + e.Message;
                    return -1;
                }
            }
            else
            {
                this.errMsg = "������Lis���������밴��������·�����ã���Ŀ¼\"" + loadPath + "\"";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �������
        /// </summary>
        public FS.HISFC.Models.RADT.EnumPatientType PatientType
        {
            get
            {
                return this.patientType;
            }
            set
            {
                this.patientType = value;
            }
        }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        FS.HISFC.Models.RADT.Patient myPatientInfo = null;

        public int SetPatient(FS.HISFC.Models.RADT.Patient patient)
        {
            myPatientInfo = patient;
            return 1;
        }

        #endregion
    }
}
