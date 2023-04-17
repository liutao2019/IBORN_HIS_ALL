using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace FS.HISFC.Components.HealthRecord.UploadGuangDong
{
    /// <summary>
    /// 测试病案接口连接
    /// </summary>
    public partial class ucSetConnectSqlServer : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucSetConnectSqlServer()
        {
            InitializeComponent();
        }

        private string profileName = @".\Profile\CaseDataBase.xml";//病案数据库连接设置;

        private void ucSetConnectSqlServer_Load(object sender, EventArgs e)
        {
            string dbInstance = "";
            string DataBaseName = "";
            string userName = "";
            string password = "";

            if (!System.IO.File.Exists(profileName))
            {

                FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
                XmlDocument doc = new XmlDocument();
                XmlElement root;
                root = myXml.CreateRootElement(doc, "SqlServerConnectForHis5.0", "1.0");

                XmlElement dbName = myXml.AddXmlNode(doc, root, "设置", "");

                myXml.AddNodeAttibute(dbName, "数据库实例名", "");
                myXml.AddNodeAttibute(dbName, "数据库名", "");
                myXml.AddNodeAttibute(dbName, "用户名", "");
                myXml.AddNodeAttibute(dbName, "密码", "");

                try
                {
                    StreamWriter sr = new StreamWriter(profileName, false, System.Text.Encoding.Default);
                    string cleandown = doc.OuterXml;
                    sr.Write(cleandown);
                    sr.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("无法保存！" + ex.Message);
                    return;
                }
            }
            else
            {
                XmlDocument doc = new XmlDocument();

                try
                {
                    StreamReader sr = new StreamReader(profileName, System.Text.Encoding.Default);
                    string cleandown = sr.ReadToEnd();
                    doc.LoadXml(cleandown);
                    sr.Close();
                }
                catch { return; }

                XmlNode node = doc.SelectSingleNode("//设置");

                try
                {

                    dbInstance = node.Attributes["数据库实例名"].Value.ToString();
                    DataBaseName = node.Attributes["数据库名"].Value.ToString();
                    userName = node.Attributes["用户名"].Value.ToString();
                    password = node.Attributes["密码"].Value.ToString();
                }
                catch { return; }

                this.tbServer.Text = dbInstance;
                this.tbInstance.Text = DataBaseName;
                this.tbUserName.Text = userName;
                this.tbPassword.Text = password;
            }
            this.FindForm().Text = "连接设置";
        }

        private void btnConnectTest_Click(object sender, EventArgs e)
        {
            string dbInstance = this.tbInstance.Text.Trim();
            string DataBaseName = this.tbServer.Text.Trim();
            string userName = this.tbUserName.Text.Trim();
            string password = this.tbPassword.Text.Trim();

            string connString = "packet size=4096;user id=" + userName + ";data source=" + DataBaseName + ";pers" +
                "ist security info=True;initial catalog=" + dbInstance + ";password=" + password;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connString;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                conn.Open();
            }
            catch (SqlException ex)
            {
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("测试连接失败:" + ex.Message);
                return;
            }
            Cursor.Current = Cursors.Arrow;

            MessageBox.Show("连接成功!");
            conn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                StreamReader sr = new StreamReader(profileName, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
            }
            catch { return; }

            XmlNode node = doc.SelectSingleNode("//设置");

            try
            {

                node.Attributes["数据库实例名"].Value = this.tbServer.Text;
                node.Attributes["数据库名"].Value = this.tbInstance.Text;
                node.Attributes["用户名"].Value = this.tbUserName.Text;
                node.Attributes["密码"].Value = this.tbPassword.Text;
            }
            catch
            {
                return;
            }

            doc.Save(profileName);

            MessageBox.Show("保存成功!");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
