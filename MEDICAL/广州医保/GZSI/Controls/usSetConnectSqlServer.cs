using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using FS.FrameWork.Function;

namespace GZSI.Controls
{
    /// <summary>
    /// usSetConnectSqlServer 的摘要说明。

    /// </summary>
    public partial class usSetConnectSqlServer : System.Windows.Forms.UserControl
    {

        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>

        public usSetConnectSqlServer()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。

            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化

        }
        private IContainer components;
        private Label label4;
        private Button btnSave;
        private Label lb;
        private Button btnCancel;
        private TabPage tabPage1;
        private Label label3;
        private TextBox tbInstance;
        private TextBox tbPassword;
        private Label label1;
        private TextBox tbUserName;
        private TextBox tbServer;
        private Label label2;
        private Button btnConnectTest;
        private Panel panel1;
        private TabControl tabControl1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPactType;

        private string profileName = @".\profile\SiDataBasePTMZ.xml";//医保数据库连接设置;

        //		string connString = "";

        private void usSetConnectSqlServer_Load(object sender, System.EventArgs e)
        {
            if (!creatXmlDoc(profileName))
            { return; }
            initControl();
            this.FindForm().Text = "连接设置";
        }

        private void btnConnectTest_Click(object sender, System.EventArgs e)
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

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            XmlDocument xmlDoc = getXmlDoc(profileName);

            XmlNode rootNode = xmlDoc.SelectSingleNode("//设置");
            ArrayList alPact = getArrayListFromXmlNodes(rootNode.ChildNodes);
            //将节点的["当前使用"]属性都设为0
            foreach (FS.FrameWork.Models.NeuObject obj in alPact)
            {
                int index = FS.FrameWork.Function.NConvert.ToInt32(obj.ID);
                XmlNode node = rootNode.ChildNodes.Item(index);
                node.Attributes["当前使用"].Value = "0";
            }

            //修改当前操作的节点属性 
            int selectindex = FS.FrameWork.Function.NConvert.ToInt32(this.cmbPactType.SelectedValue.ToString());
            XmlNode selectnode = rootNode.ChildNodes.Item(selectindex);

            if (selectnode == null)
            {
                MessageBox.Show("获得节点错误 " + selectnode.Name);
            }
            try
            {

                selectnode.Attributes["数据库实例名"].Value = this.tbServer.Text;
                selectnode.Attributes["数据库名"].Value = this.tbInstance.Text;
                selectnode.Attributes["用户名"].Value = this.tbUserName.Text;
                selectnode.Attributes["密码"].Value = this.tbPassword.Text;
                selectnode.Attributes["当前使用"].Value = "1";
            }
            catch { return; }

            xmlDoc.Save(profileName);

            MessageBox.Show("保存成功!");
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.FindForm().Close();
        }

        private void initControl()
        {
            string dbInstance = "";
            string DataBaseName = "";
            string userName = "";
            string password = "";
            //从xml文件中读取数据库配置属性

            XmlDocument xmlDoc = getXmlDoc(profileName);
            XmlNode rootNode = xmlDoc.SelectSingleNode("//设置");
            //加载医保类型列表
            ArrayList alPactType = getArrayListFromXmlNodes(rootNode.ChildNodes);
            this.cmbPactType.DataSource = alPactType;
            this.cmbPactType.ValueMember = "ID";
            this.cmbPactType.DisplayMember = "Name";

            int selectIndex;
            XmlNode node = getUsedProfileNode(out selectIndex);
            this.cmbPactType.SelectedIndex = selectIndex;
            try
            {
                dbInstance = node.Attributes["数据库实例名"].Value.ToString();
                DataBaseName = node.Attributes["数据库名"].Value.ToString();
                userName = node.Attributes["用户名"].Value.ToString();
                password = node.Attributes["密码"].Value.ToString();
            }
            catch { return; }

            //将数据库配置属性显示在控件中

            this.tbServer.Text = dbInstance;
            this.tbInstance.Text = DataBaseName;
            this.tbUserName.Text = userName;
            this.tbPassword.Text = password;

        }

        #region 对xml文件的操作

        /// <summary>
        /// 为数据库配置创建xml文件 成功访问true
        /// </summary>
        /// <returns></returns>
        private bool creatXmlDoc(string profileName)
        {
            if (!System.IO.File.Exists(profileName))
            {
                FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
                XmlDocument doc = new XmlDocument();
                XmlElement root;
                root = myXml.CreateRootElement(doc, "SqlServerConnectForHis4.0", "1.0");
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
                    return false;
                }
                return true;
            }
            return true;
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="profileName"></param>
        /// <returns></returns>
        private XmlDocument getXmlDoc(string profileName)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                StreamReader sr = new StreamReader(profileName, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
                return doc;
            }
            catch { return null; }
        }

        /// <summary>
        /// 将xml节点转换成ArrayList
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        private ArrayList getArrayListFromXmlNodes(XmlNodeList nodeList)
        {
            try
            {
                ArrayList al = new ArrayList();
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                for (int i = 0; i < nodeList.Count; i++)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.Name = nodeList.Item(i).Name;
                    obj.Memo = nodeList.Item(i).Attributes["当前使用"].Value.ToString();
                    obj.ID = i.ToString();              //节点的下标


                    al.Add(obj);
                }
                return al;
            }
            catch
            {
                return new ArrayList();
            }
        }

        /// <summary>
        /// 数据库当前设置的结点
        /// </summary>
        /// <returns></returns>
        private XmlNode getUsedProfileNode(out int usedNodeIndex)
        {
            XmlDocument xmlDoc = getXmlDoc(profileName);
            XmlNode rootNode = xmlDoc.SelectSingleNode("//设置");
            ArrayList al = this.getArrayListFromXmlNodes(rootNode.ChildNodes);
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.Memo == "1")         //节点的["当前使用"]属性
                {
                    usedNodeIndex = FS.FrameWork.Function.NConvert.ToInt32(obj.ID);
                    return rootNode.ChildNodes.Item(usedNodeIndex);
                }
            }
            usedNodeIndex = 0;
            return null;
        }
        #endregion

        private void cmbPactType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dbInstance = "";
            string DataBaseName = "";
            string userName = "";
            string password = "";

            XmlDocument xmlDoc = getXmlDoc(profileName);
            XmlNode rootNode = xmlDoc.SelectSingleNode("//设置");
            int Index = NConvert.ToInt32(this.cmbPactType.SelectedValue.ToString());
            XmlNode selectNode = rootNode.ChildNodes.Item(Index);
            try
            {
                dbInstance = selectNode.Attributes["数据库实例名"].Value.ToString();
                DataBaseName = selectNode.Attributes["数据库名"].Value.ToString();
                userName = selectNode.Attributes["用户名"].Value.ToString();
                password = selectNode.Attributes["密码"].Value.ToString();
            }
            catch { return; }

            //将数据库配置属性显示在控件中

            this.tbServer.Text = dbInstance;
            this.tbInstance.Text = DataBaseName;
            this.tbUserName.Text = userName;
            this.tbPassword.Text = password;

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lb = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmbPactType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.tbInstance = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnectTest = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 23);
            this.label4.TabIndex = 12;
            this.label4.Text = "医保类型:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(193, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lb
            // 
            this.lb.Location = new System.Drawing.Point(8, 41);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(72, 23);
            this.lb.TabIndex = 1;
            this.lb.Text = "服务器地址:";
            this.lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(281, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmbPactType);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lb);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tbInstance);
            this.tabPage1.Controls.Add(this.tbPassword);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbUserName);
            this.tabPage1.Controls.Add(this.tbServer);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnConnectTest);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(369, 220);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "连接医保服务器配置";
            // 
            // cmbPactType
            // 
            this.cmbPactType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPactType.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbPactType.IsEnter2Tab = false;
            this.cmbPactType.IsFlat = false;
            this.cmbPactType.IsLike = true;
            this.cmbPactType.IsListOnly = false;
            this.cmbPactType.IsPopForm = true;
            this.cmbPactType.IsShowCustomerList = false;
            this.cmbPactType.IsShowID = false;
            this.cmbPactType.Location = new System.Drawing.Point(86, 11);
            this.cmbPactType.Name = "cmbPactType";
            this.cmbPactType.PopForm = null;
            this.cmbPactType.ShowCustomerList = false;
            this.cmbPactType.ShowID = false;
            this.cmbPactType.Size = new System.Drawing.Size(194, 24);
            this.cmbPactType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPactType.TabIndex = 27;
            this.cmbPactType.Tag = "";
            this.cmbPactType.ToolBarUse = false;
            this.cmbPactType.SelectedIndexChanged += new System.EventHandler(this.cmbPactType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "密码:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbInstance
            // 
            this.tbInstance.Location = new System.Drawing.Point(88, 73);
            this.tbInstance.Name = "tbInstance";
            this.tbInstance.Size = new System.Drawing.Size(192, 21);
            this.tbInstance.TabIndex = 2;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(88, 137);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(152, 21);
            this.tbPassword.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据库名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(88, 105);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(192, 21);
            this.tbUserName.TabIndex = 4;
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(88, 41);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(192, 21);
            this.tbServer.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "用户名:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConnectTest
            // 
            this.btnConnectTest.Location = new System.Drawing.Point(256, 137);
            this.btnConnectTest.Name = "btnConnectTest";
            this.btnConnectTest.Size = new System.Drawing.Size(96, 23);
            this.btnConnectTest.TabIndex = 10;
            this.btnConnectTest.Text = "测试连接(&T)";
            this.btnConnectTest.Click += new System.EventHandler(this.btnConnectTest_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 197);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 48);
            this.panel1.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(377, 245);
            this.tabControl1.TabIndex = 13;
            // 
            // usSetConnectSqlServer
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Name = "usSetConnectSqlServer";
            this.Size = new System.Drawing.Size(377, 245);
            this.Load += new System.EventHandler(this.usSetConnectSqlServer_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
