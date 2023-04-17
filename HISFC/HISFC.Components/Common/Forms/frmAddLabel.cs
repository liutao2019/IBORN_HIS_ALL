using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmAddLabel : Form
    {
        //{9A0B93B1-AB81-47e9-BFE1-61A6DCE2C4A8}
        string type = "";
        string crmid = "";
        string hisurl = "";
        DataTable dt = new DataTable();

        //当前标签list
        List<FS.HISFC.Models.Label.PatientLabel> currentLabelList = new List<FS.HISFC.Models.Label.PatientLabel>();

            

        public frmAddLabel(string type, string crmid, string hisurl,List<FS.HISFC.Models.Label.PatientLabel> list)
        {
            InitializeComponent();
            this.type = type;
            this.crmid = crmid;
            this.hisurl = hisurl;
            this.currentLabelList = list;

            getLabelTypes(type);

            initFarpoint();
            setFarpointContent();
            
        }

        public void getLabelTypes(string type)
        {
            string req = "<relay><req><patientId>f</patientId></req><method>listLabelTypeList</method></relay>";

            //string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(hisurl, "crmRelay", new string[] { req }) as string;

            //{4E763617-7D00-4650-92F0-A19BF599978E}
            string resultXml = @"<res><resultCode>1</resultCode><resultDesc>查询成功</resultDesc><labelList><dictionary_type>LABELTYPE1</dictionary_type><dictionary_code>LABELTYPE11</dictionary_code><dictionary_name>一般情况</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE1</dictionary_type><dictionary_code>LABELTYPE12</dictionary_code><dictionary_name>高危妊娠因素</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE1</dictionary_type><dictionary_code>LABELTYPE13</dictionary_code><dictionary_name>孕期异常表现</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE1</dictionary_type><dictionary_code>LABELTYPE14</dictionary_code><dictionary_name>个体状况</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE3</dictionary_type><dictionary_code>LABELTYPE31</dictionary_code><dictionary_name>投诉倾向</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE3</dictionary_type><dictionary_code>LABELTYPE32</dictionary_code><dictionary_name>投诉过</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE3</dictionary_type><dictionary_code>LABELTYPE33</dictionary_code><dictionary_name>性格</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE3</dictionary_type><dictionary_code>LABELTYPE34</dictionary_code><dictionary_name>餐饮部分</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE3</dictionary_type><dictionary_code>LABELTYPE35</dictionary_code><dictionary_name>服务细节</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE4</dictionary_type><dictionary_code>LABELTYPE41</dictionary_code><dictionary_name>信仰</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE4</dictionary_type><dictionary_code>LABELTYPE42</dictionary_code><dictionary_name>消费习惯</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE4</dictionary_type><dictionary_code>LABELTYPE43</dictionary_code><dictionary_name>素质</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE4</dictionary_type><dictionary_code>LABELTYPE44</dictionary_code><dictionary_name>排斥项目</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE4</dictionary_type><dictionary_code>LABELTYPE45</dictionary_code><dictionary_name>购买意向</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE2</dictionary_type><dictionary_code>LABELTYPE21</dictionary_code><dictionary_name>孕妇特殊背景</dictionary_name></labelList><labelList><dictionary_type>LABELTYPE2</dictionary_type><dictionary_code>LABELTYPE22</dictionary_code><dictionary_name>丈夫特殊背景</dictionary_name></labelList></res>";

            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(resultXml);
            System.Xml.XmlNodeList nodeList = document.SelectNodes("/res/labelList");

            //初始化datatable的列
            dt.Columns.Add("Text", Type.GetType("System.String"));
            dt.Columns.Add("Value", Type.GetType("System.String"));

            foreach (System.Xml.XmlNode node in nodeList)  
            {
                System.Xml.XmlNode dictionary_type = node.SelectSingleNode("dictionary_type");

                if (dictionary_type.InnerText == type)
                {
                    dt.Rows.Add(node.SelectSingleNode("dictionary_name").InnerText,node.SelectSingleNode("dictionary_code").InnerText);
                }
            }  

            labelCombobox.DataSource = dt;
            labelCombobox.DisplayMember = "Text";   // Text，即显式的文本
            labelCombobox.ValueMember = "Value";    // Value，即实际的值
            labelCombobox.SelectedIndex = 0;        //  设置为默认选中第一个

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (crmid == null || crmid == "" || crmid=="-1")
            {
                MessageBox.Show("该患者未绑定crm系统，无法绑卡!", "ERROR");
                return;
            }


            //点击时获取当前操作编号
            HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;

            string employeeId = employee.ID;
            string deptId = dept.ID;
            string employeeName = employee.Name;


            string req = @"<req>
                            <patientLabelId>{0}</patientLabelId>
                            <patientId>{1}</patientId>
                            <labelType>{2}</labelType>
                            <labelId>{3}</labelId>
                            <labelSpecies>{4}</labelSpecies>
                            <labelContent>{5}</labelContent>
                            <memo>{6}</memo>
                            <extFiled1>{7}</extFiled1>
                            <extFiled2>{8}</extFiled2>
                            <extFiled3>{9}</extFiled3>
                            <labelState>{10}</labelState>
                            <operDept>{11}</operDept>
                            <operCode>{12}</operCode>
                            <operName>{13}</operName>
                            <operTime>{14}</operTime>
                        </req>";
            string patientLabelId = "";
            string patientId = crmid;
            string labelType = type;
            string labelId = "999";
            //{929E868E-3D45-4a06-AB38-722E309BD3C2}
            string labelSpecies = labelCombobox.SelectedValue.ToString();
            string labelContent = labelTextBox.Text;
            string memo = "";
            string extFiled1 = "0";
            string extFiled2 = "0";
            string extFiled3 = "";
            string labelState = "1";
            string operDept = deptId;
            string operCode = employeeId;
            string operName = employeeName;
            string operTime = "";

            string request = string.Format(req, patientLabelId, patientId, labelType, labelId, labelSpecies, labelContent,
                memo, extFiled1, extFiled2, extFiled3, labelState, operDept, operCode, operName, operTime);

            string url = hisurl;
            string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "postCrmLabel", new string[] { request }) as string;

            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(resultXml);
            System.Xml.XmlNode node = document.SelectSingleNode("/res/resultCode");
            if (node.InnerText == "0")
            {
                MessageBox.Show("添加标签成功!", "SUCCESS");
            }
            else
            {
                MessageBox.Show("添加标签失败!", "ERROR");
            }
            this.Close();
        }

        #region farpoint相关

        private void initFarpoint()
        {
            this.neuSpread1_Sheet1.RowCount = 1; //默认只有一行

            this.neuSpread1_Sheet1.Cells[0, 0].Text = "0";
            this.neuSpread1_Sheet1.Cells[0, 1].Text = "0";
            this.neuSpread1_Sheet1.Cells[0, 2].Text = "0";

            this.neuSpread1_Sheet1.Rows[0].Tag = "00";
            this.neuSpread1_Sheet1.Rows[0].Height = 30F;
            this.neuSpread1_Sheet1.Rows[0].Font = new System.Drawing.Font("宋体", 12F);

        }

        private void setFarpointContent()
        {
            this.neuSpread1_Sheet1.RowCount = currentLabelList.Count;

            int index = 0;
            foreach(FS.HISFC.Models.Label.PatientLabel patientlabel in currentLabelList)
            {
                this.neuSpread1_Sheet1.Cells[index, 0].Text = patientlabel.LabelContent;
                this.neuSpread1_Sheet1.Cells[index, 1].Text = patientlabel.CreaterName;
                this.neuSpread1_Sheet1.Cells[index, 2].Text = patientlabel.LabelCreatetime.Substring(0,10);

                this.neuSpread1_Sheet1.Rows[index].Tag = patientlabel;
                this.neuSpread1_Sheet1.Rows[index].Height = 30F;
                this.neuSpread1_Sheet1.Rows[index].Font = new System.Drawing.Font("宋体", 11F);

                this.neuSpread1_Sheet1.Cells[index, 0].Font = new System.Drawing.Font("黑体", 14F);

                index++;
            }
        }
        #endregion

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (neuSpread1_Sheet1.IsBlockSelected == false)
            {
                MessageBox.Show("请选择需要删除的标签");
                return;
            }

            int row = neuSpread1_Sheet1.ActiveRowIndex;
            FS.HISFC.Models.Label.PatientLabel label = (FS.HISFC.Models.Label.PatientLabel)this.neuSpread1_Sheet1.Rows[row].Tag;
            
            //点击时获取当前操作编号
            HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;

            string employeeId = employee.ID;
            string deptId = dept.ID;
            string employeeName = employee.Name;

            if (label.CreaterCode == employeeId || employeeName == label.CreaterName)
            {
                //自己创建的标签，能够删除
                string deleteXmlTemplate = @"<req>
	                                    <patientLabelId>{0}</patientLabelId>
                                        <operCode>{1}</operCode>
                                    </req>";
                string relayTemplate=@"<relay>{0}<method>{1}</method></relay>";

                string deleteXml = string.Format(deleteXmlTemplate, label.PatientLabelId, employeeId);

                string relay = string.Format(relayTemplate, deleteXml, "deletePatientLabel");

                FS.HISFC.BizProcess.Integrate.IbornMobileService service = new FS.HISFC.BizProcess.Integrate.IbornMobileService();
                service.Url = hisurl;
                string deleteResult = service.crmRelay(relay);

                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(deleteResult);
                System.Xml.XmlNode node = document.SelectSingleNode("/res/resultCode");
                if (node.InnerText == "0")
                {
                    MessageBox.Show("删除标签成功!", "SUCCESS");
                }
                else
                {
                    MessageBox.Show("删除标签失败!", "ERROR");
                }
                this.Close();
                
            }
            else
            {
                MessageBox.Show("不能删除他人创建的标签");
                return;
            }

        }

    }
}
