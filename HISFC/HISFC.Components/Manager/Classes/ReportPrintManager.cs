using System;
using System.Collections.Generic;
using System.Text;

namespace UFC.Manager.Classes
{
    /// <summary>
    /// [��������: �����ӡά��ҵ���]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-11-27]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class ReportPrintManager : Neusoft.NFC.Management.DataBaseManger
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="containerDllName"></param>
        /// <param name="containerDllControl"></param>
        /// <param name="printDllName"></param>
        /// <param name="printDllControl"></param>
        /// <param name="index"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public int InsertData(string containerDllName,string containerDllControl, string printDllName,string printDllControl,int index,string memo, string interfaceName)
        {
            string sql = string.Format("insert into COM_MAINTENANCE_REPORT_PRINT (CONTAINERDLLNAME,CONTAINERCONTROL,PRINTERDLLNAME,PRINTERCONTROL,PRINTERINDEX,MEMO,INTERFACE) values('{0}','{1}','{2}','{3}',{4},'{5}','{6}')",
                containerDllName, containerDllControl, printDllName, printDllControl, index.ToString(), memo);
            return this.ExecNoQuery(sql);
            
            
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="containerDllName"></param>
        /// <param name="containerDllControl"></param>
        /// <param name="printDllName"></param>
        /// <param name="printDllControl"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int DeleteData(string containerDllName,string containerDllControl, string printDllName,string printDllControl,int index)
        {
            string sql = string.Format("delete COM_MAINTENANCE_REPORT_PRINT where CONTAINERDLLNAME='{0}' and CONTAINERCONTROL='{1}' and PRINTERDLLNAME='{2}' and PRINTERCONTROL='{3}' and PRINTERINDEX={4}",
                containerDllName, containerDllControl, printDllName, printDllControl, index.ToString());
            return this.ExecNoQuery(sql);
        }
        /// <summary>
        /// װ������
        /// </summary>
        /// <returns></returns>
        public List<ReportPrint> LoadData()
        {
            List<ReportPrint> ret = new List<ReportPrint>();
            string containerDllName = string.Empty;
            string containerControlName = string.Empty;
            string sql = "select * from COM_MAINTENANCE_REPORT_PRINT order by CONTAINERCONTROL,PRINTERCONTROL,PRINTERINDEX";
            this.ExecQuery(sql);

            if (this.Reader.Read())
            {
                containerDllName = this.Reader[0].ToString();
                containerControlName = this.Reader[1].ToString();

                //��ȡ��һ��
                ReportPrint reportPrint = new ReportPrint();
                reportPrint.ContainerDllName = containerDllName;
                reportPrint.ContainerContorl = containerControlName;
                reportPrint.Add(this.Reader[2].ToString(), this.Reader[3].ToString(), short.Parse(this.Reader[4].ToString()), this.Reader[5].ToString());
                ret.Add(reportPrint);

                //��ȡ���������
                while (this.Reader.Read())
                {
                    if (containerControlName == this.Reader[1].ToString())
                    {
                        reportPrint.Add(this.Reader[2].ToString(), this.Reader[3].ToString(), short.Parse(this.Reader[4].ToString()), this.Reader[5].ToString());
                    }
                    else
                    {
                        containerDllName = this.Reader[0].ToString();
                        containerControlName = this.Reader[1].ToString();

                        reportPrint = new ReportPrint();
                        reportPrint.ContainerDllName = containerDllName;
                        reportPrint.ContainerContorl = containerControlName;
                        reportPrint.Add(this.Reader[2].ToString(), this.Reader[3].ToString(), short.Parse(this.Reader[4].ToString()), this.Reader[5].ToString());
                        ret.Add(reportPrint);
                    }

                }
            }
            this.Reader.Dispose();

            return ret;
        }

        /// <summary>
        /// ��ñ����ӡ�ؼ�
        /// </summary>
        /// <param name="controlName">�ؼ���������</param>
        /// <returns></returns>
        public ReportPrint GetReportPrint(string controlName)
        {            
            string sql = string.Format("select * from COM_MAINTENANCE_REPORT_PRINT where PRINTERCONTROL='{0}' order by CONTAINERCONTROL,PRINTERCONTROL,PRINTERINDEX ", controlName);
            
            return this.GetReportPrintObject(sql);
        }

        /// <summary>
        /// �õ�ʵ�ֽӿڿؼ���Ϣ
        /// </summary>
        /// <param name="controlName">�ؼ���������</param>
        /// <param name="interfaceName">�ӿ���������</param>
        /// <param name="index">�ӿ�������</param>
        /// <returns></returns>
        public ReportPrint GetReportPrint(string controlName,string interfaceName,int index)
        {
            string sql = string.Format("select * from COM_MAINTENANCE_REPORT_PRINT where PRINTERCONTROL='{0}' and INTERFACE='{1}' and PRINTERINDEX={2} order by CONTAINERCONTROL,PRINTERCONTROL,PRINTERINDEX ", 
                controlName,interfaceName,index.ToString());
            
            return this.GetReportPrintObject(sql);

        }

        /// <summary>
        /// �õ�ʵ�ֽӿڿؼ���Ϣ
        /// </summary>
        /// <param name="controlName">�ؼ���������</param>
        /// <param name="interfaceName">�ӿ���������</param>
        /// <returns></returns>
        public ReportPrint GetReportPrint(string controlName, string interfaceName)
        {
            string sql = string.Format("select * from COM_MAINTENANCE_REPORT_PRINT where PRINTERCONTROL='{0}' and INTERFACE='{1}' order by CONTAINERCONTROL,PRINTERCONTROL,PRINTERINDEX ",
                controlName, interfaceName);

            return this.GetReportPrintObject(sql);
        }

        /// <summary>
        /// �õ�ʵ�ֽӿڿؼ���Ϣ
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns></returns>
        private ReportPrint GetReportPrintObject(string sql)
        {
            ReportPrint reportPrint = null;
            string containerDllName = string.Empty;
            string containerControlName = string.Empty;
            
            this.ExecQuery(sql);

            if (this.Reader.Read())
            {
                //��ȡ��һ��
                reportPrint = new ReportPrint();
                reportPrint.ContainerDllName = this.Reader[0].ToString();
                reportPrint.ContainerContorl = this.Reader[1].ToString();
                reportPrint.Add(this.Reader[2].ToString(), this.Reader[3].ToString(), short.Parse(this.Reader[4].ToString()), this.Reader[5].ToString());

                while (this.Reader.Read())
                {
                    reportPrint.Add(this.Reader[2].ToString(), this.Reader[3].ToString(), short.Parse(this.Reader[4].ToString()), this.Reader[5].ToString());
                }
            }

            this.Reader.Dispose();
            return reportPrint;
        }
    }
}
