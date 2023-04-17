using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// 佛山妇幼检验数据上传功能，涉及hiswebsesrver和检验服务器两台
    /// {540F3738-625F-476b-BEC7-F7A14D258E26}
    /// </summary>
    public partial class ucLisFuyouReport : FS.FrameWork.WinForms.Controls.ucBaseControl//, FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucLisFuyouReport()
        {
            InitializeComponent();
        }

        #region 变量

        private FarPoint.Win.Spread.CellType.ComboBoxCellType cmbDept = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
        private FarPoint.Win.Spread.CellType.ComboBoxCellType cmbDoc = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
        private FS.HISFC.BizLogic.Order.Permission manager = new FS.HISFC.BizLogic.Order.Permission();

        private string inpatientno = "";

        /// <summary>
        /// 住院流水号
        /// </summary>
        /// <returns></returns>
        public string InpatientNo
        {
            set
            {
                if (value == null || value == "")
                {
                    inpatientno = "";
                    return;
                }
                inpatientno = value;
                //this.ucPatient1.PatientInfo = CacheManager.RadtIntegrate.GetPatientInfomation(value);
                this.Retrieve();
            }
        }
        #endregion

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            /*
            try
            {
                ucPermissionInput u = new ucPermissionInput();
                u.Permission = this.neuSpread1.Sheets[0].ActiveRow.Tag as FS.HISFC.Models.Order.Consultation;

                //this.ucPatient1.PatientInfo = CacheManager.RadtIntegrate.GetPatientInfomation(u.Permission.InpatientNo);
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "授权";
                FS.FrameWork.WinForms.Classes.Function.PopForm.MaximizeBox = false;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(u);

                this.Retrieve();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             * */
        }

        public int Retrieve()
        {
            ArrayList al = new ArrayList();


            if (al == null)
            {
                MessageBox.Show(manager.Err);
                return -1;
            }

            this.neuSpread1_Sheet1.RowCount = al.Count;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Order.Consultation permission = al[i] as FS.HISFC.Models.Order.Consultation;
                if (permission == null)
                {
                    MessageBox.Show("错误！");
                    return -1;
                }

                this.neuSpread1.Sheets[0].Cells[i, 0].Value = CacheManager.RadtIntegrate.GetPatientInfomation(permission.InpatientNo).Name;
                this.neuSpread1.Sheets[0].Cells[i, 1].Value = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(permission.DeptConsultation.ID);
                this.neuSpread1.Sheets[0].Cells[i, 2].Value = permission.DoctorConsultation.Name;
                this.neuSpread1.Sheets[0].Cells[i, 3].Value = permission.BeginTime;
                this.neuSpread1.Sheets[0].Cells[i, 4].Value = permission.EndTime;
                this.neuSpread1.Sheets[0].Cells[i, 5].Value = permission.Name;
                this.neuSpread1.Sheets[0].Cells[i, 6].Value = "";
                this.neuSpread1.Sheets[0].Cells[i, 7].Value = "";
                this.neuSpread1.Sheets[0].Rows[i].Tag = permission;
            }
            return 0;
        }

        private void ucPermissionManager_Load(object sender, EventArgs e)
        {

        }

        void ucQueryInpatientNo1_myEvent()
        {

        }

        #region 工具
        public DataTable getSendDetailTest(string rawstring)
        {
            DataTable dtb = new DataTable();

            //得到序列化结果aaa
            string aaa = rawstring;

            if (aaa.Substring(0, 1) == "0")
            {
                try
                {
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    // var obj = serializer.DeserializeObject(aaa);//反序列化

                    aaa = aaa.Substring(2, aaa.Length - 2);

                    ArrayList dic = serializer.Deserialize<ArrayList>(aaa);//反序列化ArrayList类型

                    if (dic.Count > 0)
                    {
                        foreach (Dictionary<string, object> drow in dic)
                        {
                            if (dtb.Columns.Count == 0)
                            {
                                foreach (string key in drow.Keys)
                                {
                                    dtb.Columns.Add(key, drow[key].GetType());//添加dt的列名
                                }
                            }
                            DataRow row = dtb.NewRow();
                            foreach (string key in drow.Keys)
                            {

                                row[key] = drow[key];//添加列值
                            }
                            dtb.Rows.Add(row);//添加一行
                        }
                    }
                }
                catch (Exception e)
                {
                    //
                }
            }
            else
            {
                //
            }

            return dtb;
        }

        /// <summary>
        /// string 到 DataTable
        /// </summary>
        /// <param name="strdata"></param>
        /// <returns></returns>
        public static DataTable StringToDataTable(string strdata)
        {
            if (string.IsNullOrEmpty(strdata))
            {
                return null;
            }
            DataTable dt = new DataTable();
            string[] strSplit = { "@&@" };
            string[] strRow = { "#$%" };    //分解行的字符串
            string[] strColumn = { "^&*" }; //分解字段的字符串

            string[] strArr = strdata.Split(strSplit, StringSplitOptions.None);
            System.IO.StringReader sr = new System.IO.StringReader(strArr[0]);
            dt.ReadXmlSchema(sr);
            sr.Close();


            string strTable = strArr[1]; //取表的数据
            if (!string.IsNullOrEmpty(strTable))
            {
                string[] strRows = strTable.Split(strRow, StringSplitOptions.None); //解析成行的字符串数组
                for (int rowIndex = 0; rowIndex < strRows.Length; rowIndex++)       //行的字符串数组遍历
                {
                    string vsRow = strRows[rowIndex]; //取行的字符串
                    string[] vsColumns = vsRow.Split(strColumn, StringSplitOptions.None); //解析成字段数组
                    dt.Rows.Add(vsColumns);
                }
            }
            return dt;
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {

            this.neuSpread1_Sheet1.RowCount = 0;
            queryAndSetValue();
        }

        public void queryAndSetValue()
        {

            //{b2cf268b-8824-4c34-a95d-f6decfd0c185}
            string req = @"<req>
	                            <beginTime>{0}</beginTime>
                                <endTime>{1}</endTime>
                                <name>{2}</name>
                            </req>";
            string stringDtBegin = this.dtBegin.Text + " 00:00:00";
            string stringDtEnd = this.dtEnd.Text + " 23:59:59";
            string stringName = this.txtNames.Text;

            req = string.Format(req, stringDtBegin, stringDtEnd, stringName);
            FS.HISFC.BizProcess.Integrate.IbornMobileService server = new FS.HISFC.BizProcess.Integrate.IbornMobileService();
            server.Url = "http://10.20.10.220:8020/IbornMobileService.asmx";
            //server.Url = "http://localhost:8080/IbornMobileService.asmx";
            string res = server.GetLisFuYouReportPatientList(req);
            //string res = WebServiceHelper.InvokeWebService("http://localhost:8080/IbornMobileService.asmx", "GetLisFuYouReportPatientList",req);  

            System.Xml.XmlDocument docbind = new System.Xml.XmlDocument();
            docbind.LoadXml(res);

            string msg = docbind.SelectSingleNode("/res/msg").InnerText;
            string content = docbind.SelectSingleNode("/res/content").InnerText;

            int i = 0;
            if (!string.IsNullOrEmpty(content))
            {
                System.Xml.XmlNodeList atoms = docbind.SelectNodes("/res/content/atom");
                foreach (System.Xml.XmlNode item in atoms)
                {
                    string name = item.SelectSingleNode("name").InnerText;
                    string cardno = item.SelectSingleNode("cardno").InnerText;
                    string itemname = item.SelectSingleNode("itemname").InnerText;

                    this.neuSpread1.Sheets[0].AddRows(i, 1);
                    this.neuSpread1.Sheets[0].Cells[i, 0].Value = name;
                    this.neuSpread1.Sheets[0].Cells[i, 1].Value = cardno;
                    this.neuSpread1.Sheets[0].Cells[i, 2].Value = itemname;
                    //this.neuSpread1.Sheets[0].Rows[i].Tag = permission;

                    i++;
                }
            }

            //DataTable dt = StringToDataTable(content);
        }

        /*
        #region IMaintenanceControlable 成员

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Add()
        {

            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Copy()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Cut()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Delete()
        {

            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Export()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Import()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Init()
        {
            return 0;
        }

        bool FS.FrameWork.WinForms.Forms.IMaintenanceControlable.IsDirty
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Modify()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.NextRow()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Paste()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.PreRow()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Print()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.PrintConfig()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.PrintPreview()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Query()
        {
            queryAndSetValue();
            return 0;
        }

        FS.FrameWork.WinForms.Forms.IMaintenanceForm queryForm;
        FS.FrameWork.WinForms.Forms.IMaintenanceForm FS.FrameWork.WinForms.Forms.IMaintenanceControlable.QueryForm
        {
            get
            {
                return this.queryForm;
            }
            set
            {
                this.queryForm = value;
            }
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Save()
        {
            return 0;
        }

        #endregion

        */


        #region IToolBar 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Query(object sender, object neuObject)
        {
            queryAndSetValue();

            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int currnetrowindex = this.neuSpread1.Sheets[0].ActiveRowIndex;

            string activeName = this.neuSpread1.Sheets[0].Cells[currnetrowindex, 0].Text.ToString();
            string activeCardNo = this.neuSpread1.Sheets[0].Cells[currnetrowindex, 1].Text.ToString();
            string activeItemName = this.neuSpread1.Sheets[0].Cells[currnetrowindex, 2].Text.ToString();

            if (MessageBox.Show("是否确认上传" + activeName + dtBegin.Text + "至" + dtEnd.Text + "的检验信息？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //选择就诊时间
                FS.HISFC.Components.Order.Forms.frmChooseLisDate frm = new FS.HISFC.Components.Order.Forms.frmChooseLisDate();
                frm.ShowDialog();

                DateTime chooseDate = frm.dt;
                //frm.Close();
                //frm.DialogResult = DialogResult.Cancel;

                //{F5D2CE04-8B2F-4099-A1B9-78E83BF393E7}
                string reqformat = @"<req>
	                                    <cardNo>{0}</cardNo>
	                                    <dtBegin>{1}</dtBegin>
	                                    <dtEnd>{2}</dtEnd>
	                                    <name>{3}</name>
	                                    <lisDate>{4}</lisDate>
                                    </req>";
                string stringDtBegin = this.dtBegin.Text + " 00:00:00";
                string stringDtEnd = this.dtEnd.Text + " 23:59:59";

                string req = string.Format(reqformat, activeCardNo, stringDtBegin, stringDtEnd, activeName, chooseDate.ToString("yyyy-MM-dd HH:mm:ss"));
                FS.HISFC.BizProcess.Integrate.IbornMobileService server = new FS.HISFC.BizProcess.Integrate.IbornMobileService();
                server.Url = "http://10.20.10.220:8020/IbornMobileService.asmx";

                string res = server.GetLisFuYouReportDetail(req);

                System.Xml.XmlDocument docbind = new System.Xml.XmlDocument();

                res = res.Replace("&rt", "");  //{92f6cefd-f438-4ca0-827a-7e42748532a7}

                docbind.LoadXml(res);

                string fyResult = docbind.SelectSingleNode("/res/fyResult").InnerText;
                string lisReuslt = docbind.SelectSingleNode("/res/lisReuslt").InnerText;

                MessageBox.Show("妇幼提示：" + fyResult);
            }
            else
            {
                return;
            }
        }

        #endregion
    }
}
