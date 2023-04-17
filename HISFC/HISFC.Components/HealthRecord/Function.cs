using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.IO;
namespace FS.HISFC.Components.HealthRecord
{
    class Function
    {
        #region ͨ��XML�����ͱ���DataTable����Ϣ

        /// <summary>
        /// ���ݱ����XML��Ϣ,��������Ϣ
        /// </summary>
        /// <param name="pathName">XML�ļ��洢λ��</param>
        /// <param name="table">Ҫ��ʼ����DataTable</param>
        /// <param name="dv">DataTable��DataView</param>
        /// <param name="sv">��DataView��FarPointSheet</param>
        public static void CreatColumnByXML(string pathName, DataTable table, ref DataView dv, FarPoint.Win.Spread.SheetView sv)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                StreamReader sr = new StreamReader(pathName, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
            }
            catch
            {
                return;
            }

            XmlNodeList nodes = doc.SelectNodes("//Column");

            string tempString = "";

            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["type"].Value == "TextCellType")
                {
                    tempString = "System.String";
                }
                else if (node.Attributes["type"].Value == "CheckBoxCellType")
                {
                    tempString = "System.Boolean";
                }
                else if (node.Attributes["type"].Value == "NumberCellType")
                {
                    tempString = "System.Decimal";
                }
                else if (node.Attributes["type"].Value == "DateTimeCellType")
                {
                    tempString = "System.DateTime";
                }
                else
                {
                    tempString = "System.String";
                }

                table.Columns.Add(new DataColumn(node.Attributes["displayname"].Value, Type.GetType(tempString)));
            }

            dv = new DataView(table);

            sv.DataSource = dv;
        }

        #endregion

        /// <summary>
        /// ��ӡ������ҳ
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0����  ��-1 ����</returns>
        public static int PrintCaseFirstPage(FS.HISFC.Models.RADT.PatientInfo info)
        {
            HealthRecord.ucCasePrint casePrint = new HealthRecord.ucCasePrint();
            casePrint.LoadInfo();
            FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
            FS.HISFC.BizProcess.Integrate.RADT RadtInpatient = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.HealthRecord.Base caseBase = new FS.HISFC.Models.HealthRecord.Base();
            //�ж��Ƿ��иû���
            if (info.ID == null || info.ID == "")
            {
                MessageBox.Show("סԺ��ˮ�Ų���Ϊ��");
                return -1;
            }
            //��ȡסԺ�Ÿ�ֵ��ʵ��
            FS.HISFC.Models.RADT.PatientInfo patientInfo = RadtInpatient.GetPatientInfoByPatientNO(info.ID);
            if (patientInfo == null)
            {
                MessageBox.Show("��ȡ��Ա��Ϣ����");
                return -1;
            }
            caseBase.PatientInfo = patientInfo;
            //casePrint.contro = caseBase;
            //��ȡĬ�ϴ�ӡ��
            string errStr = "";
            ArrayList alSetting = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("BAPrinter", out errStr);
            if (alSetting == null)
            {
                MessageBox.Show(errStr);
                return -1;
            }
            if (alSetting.Count == 0)
            {
                MessageBox.Show("����д��ӡ�����������ļ�");
                FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("BAPrinter");
                return -1;
            }
            string printerSetting = alSetting[0] as string;
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                if (System.Drawing.Printing.PrinterSettings.InstalledPrinters[i].IndexOf(printerSetting) != -1)
                    p.PrintDocument.PrinterSettings.PrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
            }

            p.IsPrintInputBox = true;
            Common.Classes.Function.GetPageSize("case1", ref p);
            p.PrintPage(20, 80, casePrint);
            return 0;
        }
        /// <summary>
        /// ��ӡ������ҳ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int PrintCaseFPByFrm(FS.HISFC.Models.RADT.PatientInfo info, object frmTag)
        {

            //			System.Windows.Forms.Form frmPrint = new Form();
            //			frmPrint.Size = new System.Drawing.Size(825,1070);
            //			casePrint.Dock= System.Windows.Forms.DockStyle.Fill;
            //			frmPrint.AutoScale = false;
            //			frmPrint.Controls.Add(casePrint);
            //			frmPrint.ShowDialog();
            //try
            //{
            //    HealthRecord.frmPrintCasePage frm = new frmPrintCasePage();
            //    frm.Tag = frmTag;
            //    frm.Show();
            //    //frm.Visible=false;
            //    return frm.Print(info);
            //}
            //catch
            //{
            return 0;
            //}


        }
        /// <summary>
        /// ��ӡ������ҳ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int PrintCaseFPByFrm(FS.HISFC.Models.RADT.PatientInfo info)
        {

            //			System.Windows.Forms.Form frmPrint = new Form();
            //			frmPrint.Size = new System.Drawing.Size(825,1070);
            //			casePrint.Dock= System.Windows.Forms.DockStyle.Fill;
            //			frmPrint.AutoScale = false;
            //			frmPrint.Controls.Add(casePrint);
            //			frmPrint.ShowDialog();
            //try
            //{
            //    Case.frmPrintCasePage frm = new frmPrintCasePage();
            //    frm.Show();
            //    //frm.Visible=false;
            //    return frm.Print(info);
            //}
            //catch
            //{
            return 0;
            //}


        }

        /// <summary>
        /// ת���ַ���
        /// null��"" ���ظ�ֵ�ַ���
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="isPrint">��ӡ������</param>
        /// <returns></returns>
        public string ReturnStringValue(string str ,bool isPrint)
        {
            string ret = string.Empty;
            if (str != null && str != "")
            {
                ret = str;
            }
            else 
            {
                if (isPrint)
                {
                    ret = "-";
                }
                else
                {
                    ret = string.Empty;
                }
            }
            return ret;
        }


        #region ��Ϣ����ģʽ by cube
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="alInfo">������Ϣ</param>
        /// <param name="operType">�������</param>
        /// <param name="infoType">�������</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <returns>-1����ʧ��</returns>
        public static int SendBizMessage(ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType infoType, ref string errInfo)
        {
            object MessageSender = InterfaceManager.GetBizInfoSenderImplement();
            if (MessageSender is FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender)
            {
                return ((FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender)MessageSender).Send(alInfo, operType, infoType, ref errInfo);
            }
            else if (MessageSender == null)
            {
                errInfo = "ûά���ӿڣ�FS.SOC.HISFC.BizProcess.MessagePaternInterface.ISender��ʵ��";
                return 0;
            }

            //����һ��
            //FS.SOC.HISFC.BizLogic.MessagePattern.MessageSenderInterfaceImplement MessageSenderInterfaceImplement = new FS.SOC.HISFC.BizLogic.MessagePattern.MessageSenderInterfaceImplement();
            //return MessageSenderInterfaceImplement.Send(alInfo, operType, infoType, ref errInfo);

            errInfo = "�ӿ�ʵ�ֲ���ָ�����ͣ�FS.SOC.HISFC.BizProcess.MessagePaternInterface.ISender";
            return -1;
        }
        /// <summary>
        /// ��ʾ��Ϣ��MessageBox��ͳһ���
        /// </summary>
        /// <param name="text">��ʾ����</param>
        /// <param name="messageBoxIcon">ͼ��</param>
        public static void ShowMessage(string text, System.Windows.Forms.MessageBoxIcon messageBoxIcon)
        {

            string caption = "";
            switch (messageBoxIcon)
            {
                case System.Windows.Forms.MessageBoxIcon.Warning:
                    caption = "����>>";
                    break;
                case System.Windows.Forms.MessageBoxIcon.Error:
                    caption = "����>>";
                    break;
                default:
                    caption = "��ʾ>>";
                    break;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, messageBoxIcon);
        }
        #endregion


        #region ��ȡȫ����סԺ��¼

        public static DataTable Getdate()
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            
            string strSql = @"
               select  i.inpatient_no,i.patient_no,i.in_date ,i.in_times from fin_ipr_inmaininfo i
                    ";

          
           DataSet ds =new DataSet();
          dbMgr.ExecQuery(strSql,ref ds);
           return ds.Tables[0];
        }

        public static int UpdateTimes(string inpatientno, string times)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

            string strSql = @"
                update fin_ipr_inmaininfo i 
                    set i.in_times='{1}'
                    where i.inpatient_no='{0}' ";

            strSql = String.Format(strSql, inpatientno,times);

            return dbMgr.ExecQuery(strSql);
        }

        public static DataTable Getdate2()
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

            string strSql = @"
            select  i.inpatient_no,i.patient_no,to_char(i.in_date,'yyyy-mm-dd HH24:mi:ss') ,i.in_times,i.in_state from fin_ipr_inmaininfo i 
                    where  
                    i.in_times='0' or i.in_times is null or i.in_times =''
                    order by i.in_date asc
        ";


            DataSet ds = new DataSet();
            dbMgr.ExecQuery(strSql, ref ds);
            return ds.Tables[0];
        }

        public static string gettimes2( string patientno,string datetime)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

            string strSql = @"    
                    select   nvl(max(i.in_times),0)+1 from fin_ipr_inmaininfo i 
                    where   i.in_date<to_date('{1}','yyyy-mm-dd HH24:mi:ss')
                    and i.patient_no='{0}'
        ";


            string sql = string.Format(strSql , patientno, datetime);
            return dbMgr.ExecSqlReturnOne(sql, "1");
        }

        #endregion
    }
}
