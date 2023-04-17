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
        #region 通过XML创建和保存DataTable列信息

        /// <summary>
        /// 根据保存的XML信息,生成列信息
        /// </summary>
        /// <param name="pathName">XML文件存储位置</param>
        /// <param name="table">要初始化的DataTable</param>
        /// <param name="dv">DataTable的DataView</param>
        /// <param name="sv">绑定DataView的FarPointSheet</param>
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
        /// 打印病案首页
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0正常  ，-1 出错</returns>
        public static int PrintCaseFirstPage(FS.HISFC.Models.RADT.PatientInfo info)
        {
            HealthRecord.ucCasePrint casePrint = new HealthRecord.ucCasePrint();
            casePrint.LoadInfo();
            FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
            FS.HISFC.BizProcess.Integrate.RADT RadtInpatient = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.HealthRecord.Base caseBase = new FS.HISFC.Models.HealthRecord.Base();
            //判断是否有该患者
            if (info.ID == null || info.ID == "")
            {
                MessageBox.Show("住院流水号不能为空");
                return -1;
            }
            //获取住院号赋值给实体
            FS.HISFC.Models.RADT.PatientInfo patientInfo = RadtInpatient.GetPatientInfoByPatientNO(info.ID);
            if (patientInfo == null)
            {
                MessageBox.Show("获取人员信息出错");
                return -1;
            }
            caseBase.PatientInfo = patientInfo;
            //casePrint.contro = caseBase;
            //获取默认打印机
            string errStr = "";
            ArrayList alSetting = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("BAPrinter", out errStr);
            if (alSetting == null)
            {
                MessageBox.Show(errStr);
                return -1;
            }
            if (alSetting.Count == 0)
            {
                MessageBox.Show("请填写打印机名称配置文件");
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
        /// 打印病案首页
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
        /// 打印病案首页
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
        /// 转换字符串
        /// null及"" 返回赋值字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="isPrint">打印界面用</param>
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


        #region 消息发送模式 by cube
        /// <summary>
        /// 信息发送
        /// </summary>
        /// <param name="alInfo">所有信息</param>
        /// <param name="operType">操作类别</param>
        /// <param name="infoType">数据类别</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>-1发送失败</returns>
        public static int SendBizMessage(ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType infoType, ref string errInfo)
        {
            object MessageSender = InterfaceManager.GetBizInfoSenderImplement();
            if (MessageSender is FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender)
            {
                return ((FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender)MessageSender).Send(alInfo, operType, infoType, ref errInfo);
            }
            else if (MessageSender == null)
            {
                errInfo = "没维护接口：FS.SOC.HISFC.BizProcess.MessagePaternInterface.ISender的实现";
                return 0;
            }

            //测试一下
            //FS.SOC.HISFC.BizLogic.MessagePattern.MessageSenderInterfaceImplement MessageSenderInterfaceImplement = new FS.SOC.HISFC.BizLogic.MessagePattern.MessageSenderInterfaceImplement();
            //return MessageSenderInterfaceImplement.Send(alInfo, operType, infoType, ref errInfo);

            errInfo = "接口实现不是指定类型：FS.SOC.HISFC.BizProcess.MessagePaternInterface.ISender";
            return -1;
        }
        /// <summary>
        /// 显示消息，MessageBox的统一风格
        /// </summary>
        /// <param name="text">提示内容</param>
        /// <param name="messageBoxIcon">图标</param>
        public static void ShowMessage(string text, System.Windows.Forms.MessageBoxIcon messageBoxIcon)
        {

            string caption = "";
            switch (messageBoxIcon)
            {
                case System.Windows.Forms.MessageBoxIcon.Warning:
                    caption = "警告>>";
                    break;
                case System.Windows.Forms.MessageBoxIcon.Error:
                    caption = "错误>>";
                    break;
                default:
                    caption = "提示>>";
                    break;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, messageBoxIcon);
        }
        #endregion


        #region 获取全部的住院记录

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
