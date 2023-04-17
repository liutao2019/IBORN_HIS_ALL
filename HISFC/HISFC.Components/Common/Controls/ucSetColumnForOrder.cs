using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FS.FrameWork;
using System.IO;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// [��������: Fp��������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <˵��
    ///		 ͨ�������Fp ��ʾFp������Ϣ ����ά���Ƿ���ʾ/�������Ϣ
    ///  />
    /// </summary>
    public partial class ucSetColumnForOrder : UserControl
    {
        public ucSetColumnForOrder()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.InitDataTable();
                this.InitXmlDoc();
                this.ReadXmlSetData();

                //���������ƶ����� 
                this.btnUp.Visible = false;
                this.btnDown.Visible = false;
            }
        }

        public event System.EventHandler DisplayEvent;

        #region �����

        /// <summary>
        /// �������ļ��洢·��
        /// </summary>
        public string FilePath = "PharmacyCol.xml";

        /// <summary>
        /// Xml�ĵ�
        /// </summary>
        private System.Xml.XmlDocument doc = null;

        /// <summary>
        /// DataTable��ʼ��
        /// </summary>
        private System.Data.DataTable dt = null;

        #endregion

        /// <summary>
        /// ��ʼ��DataSet
        /// </summary>
        private void InitDataTable()
        {
            dt = new DataTable("Setup");

            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBool = System.Type.GetType("System.Boolean");


            dt.Columns.AddRange(new DataColumn[]{ new DataColumn("����",dtStr), 
													 new DataColumn("��ʾ",dtBool),                                                   
													 new DataColumn("����",dtBool),
													 new DataColumn("��ӡ",dtBool),
                                                     new DataColumn("ά��",dtBool),
                                                     new DataColumn("���",dtStr),
                                                    });

            this.neuSpread1_Sheet1.DataSource = dt;

            this.neuSpread1_Sheet1.Columns[0].Locked = true;
        }

        /// <summary>
        /// ��ʼ��Xml�ĵ�
        /// </summary>
        private void InitXmlDoc()
        {
            try
            {
                this.doc = new XmlDocument();
                if (System.IO.File.Exists(this.FilePath))
                {
                    //this.doc.LoadXml(this.FilePath,System.Text.Encoding.Default);
                    StreamReader sr = new StreamReader(this.FilePath, System.Text.Encoding.Default);
                    string cleandown = sr.ReadToEnd();
                    doc.LoadXml(cleandown);
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("����Xml�ĵ���������" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// ���ݴ������Ϣ����DataTable ��������ļ����� ���������ļ��ڻ�ȡ��Ϣ ���� �ɴ����SheetView�Զ�����
        /// </summary>
        /// <param name="savePath">�����ļ�����·��</param>
        /// <param name="sv">�����õ�SheetView</param>
        public void SetDataTable(string savePath, FarPoint.Win.Spread.SheetView sv)
        {
            this.FilePath = savePath;

            if (System.IO.File.Exists(savePath))
            {
                if (this.ReadXmlSetData() == -1)
                    this.SetDataTable(sv);
            }
            else
            {
                this.SetDataTable(sv);
            }
        }

        /// <summary>
        /// ���ݴ������Ϣ����DataTable �ɴ����SheetView�Զ�����
        /// </summary>
        /// <param name="sv">�����õ�SheetView</param>
        public void SetDataTable(FarPoint.Win.Spread.SheetView sv)
        {
            this.dt.Rows.Clear();
            for (int i = 0; i < sv.Columns.Count; i++)
            {
                this.dt.Rows.Add(new Object[]{sv.Columns[i].Label,
											  sv.Columns[i].Visible,
											 sv.Columns[i].AllowAutoSort,
											 true,
                                             false,
                                             sv.Columns[i].Width});
            }
        }


        /// <summary>
        /// ���ݴ������Ϣ����DataTable �ɴ����SheetView��ָ�����Զ�����
        /// </summary>
        /// <param name="sv">�����õ�SheetView</param>
        public void SetDataTable(FarPoint.Win.Spread.SheetView sv, params int[] iIndexCollection)
        {
            this.dt.Rows.Clear();
            for (int i = 0; i < iIndexCollection.Length; i++)
            {
                this.dt.Rows.Add(new Object[]{sv.Columns[iIndexCollection[i]].Label,
											  sv.Columns[iIndexCollection[i]].Visible,
											 sv.Columns[iIndexCollection[i]].AllowAutoSort,
											 true,
                                             false,
                                            sv.Columns[iIndexCollection[i]].Width});

            }
        }

        /// <summary>
        /// ��������ʾ
        /// </summary>
        /// <param name="displayVisible">'��ʾ' �������Ƿ���Ч</param>
        /// <param name="sortVisible">'����' �������Ƿ���Ч</param>
        /// <param name="printVisible">'��ӡ'�������Ƿ���Ч</param>
        /// <param name="setVisible">'ά��'�������Ƿ���Ч</param>
        public void SetColVisible(bool displayVisible, bool sortVisible, bool printVisible, bool setVisible)
        {
            this.neuSpread1_Sheet1.Columns[1].Visible = displayVisible;
            this.neuSpread1_Sheet1.Columns[2].Visible = sortVisible;
            this.neuSpread1_Sheet1.Columns[3].Visible = printVisible;
            this.neuSpread1_Sheet1.Columns[4].Visible = setVisible;
        }

        /// <summary>
        /// ��ȡ��ǰָ������ѡ�е���
        /// </summary>
        /// <param name="checkCol">�������ѡ����</param>
        /// <returns>����ʾ��������</returns>
        public List<string> GetCheckCol(CheckCol checkCol)
        {
            List<string> strDisplay = new List<string>();
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)checkCol].Text == "True")
                {
                    strDisplay.Add(this.neuSpread1_Sheet1.Cells[i, 0].Text);
                }
            }

            return strDisplay;
        }
        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetView"></param>
        public static void ReadXml(string fileName, FarPoint.Win.Spread.SheetView sheetView)
        {
            if (System.IO.File.Exists(fileName))
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                try
                {
                    doc.Load(fileName);
                }
                catch { return; }
                XmlNodeList nodes = doc.SelectNodes("//Column");
                if (nodes == null)
                {
                    MessageBox.Show("Xml�ĵ���ʽ�����Ϲ淶 �����½���");
                    return;
                }
                for (int i = 0; i < sheetView.Columns.Count; i++)
                {
                    foreach (XmlNode node in nodes)
                    {
                        if (sheetView.Columns[i].Label == node.Attributes["displayname"].Value)
                        {
                            try
                            {
                                sheetView.Columns[i].Visible = bool.Parse(node.Attributes["visible"].Value);
                                sheetView.Columns[i].AllowAutoSort = bool.Parse(node.Attributes["sort"].Value);
                                sheetView.Columns[i].Width = float.Parse(node.Attributes["width"].Value);
                            }
                            catch { }
                        }
                    }

                }
            }

        }
        /// <summary>
        /// ����Xml����DataSet��ʾ
        /// </summary>
        private int ReadXmlSetData()
        {
            if (System.IO.File.Exists(this.FilePath))
            {
                if (!this.doc.HasChildNodes)
                    this.InitXmlDoc();
                XmlNodeList nodes = doc.SelectNodes("//Column");
                if (nodes == null)
                {
                    MessageBox.Show("Xml�ĵ���ʽ�����Ϲ淶 �����½���");
                    return -1;
                }
                this.dt.Rows.Clear();
                try
                {
                    foreach (XmlNode node in nodes)
                    {
                        this.dt.Rows.Add(new Object[]{ node.Attributes["displayname"].Value,
											 bool.Parse( node.Attributes["visible"].Value ),
											 bool.Parse( node.Attributes["sort"].Value ),
											 bool.Parse(node.Attributes["enable"].Value),
                                             false,
                                             node.Attributes["width"].Value});
                    }
                }
                catch { return -1; }
            }
            return 0;
        }

        /// <summary>
        /// ����Xml�ĵ������¶�ȡ
        /// </summary>
        private void SaveAndReLoad()
        {

            {
                string xml = "";
                //xml += "<?xml version=\"1.0\" encoding=\"GB2312\"?>";
                xml = xml + @"<Setting>";
                for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                {
                    xml = xml + string.Format(@"<Column displayname=""{0}"" visible=""{1}"" sort=""{2}"" enable=""{3}""  width=""{4}""/>",
                        this.neuSpread1.ActiveSheet.Cells[i, 0].Text,
                        this.neuSpread1.ActiveSheet.Cells[i, 1].Text,
                        this.neuSpread1.ActiveSheet.Cells[i, 2].Text,
                        this.neuSpread1.ActiveSheet.Cells[i, 3].Text,
                        this.neuSpread1.ActiveSheet.Cells[i, 5].Text);
                }
                xml = xml + @"</Setting>";
                try
                {
                    doc.LoadXml(xml);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            }
            //this.doc.Save(this.FilePath);
            #region add by liuww 
            StreamWriter sr = new StreamWriter(this.FilePath, false, System.Text.Encoding.Default);
            string cleandown = doc.OuterXml;
            sr.Write(cleandown);
            sr.Close();
            #endregion
            this.dt.Rows.Clear();
            this.ReadXmlSetData();
        }

        /// <summary>
        /// ��ȡ��ǰ�ж�Ӧ��Xml�ڵ�
        /// </summary>
        /// <returns></returns>
        private XmlNode GetNowXmlNode()
        {
            int i = this.neuSpread1_Sheet1.ActiveRowIndex;
            if (i < 0)
                return null;

            string colName = this.neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
            string xPath = "Setting/Column[@displayname='" + colName + "']";

            XmlNode nowNode = this.doc.SelectSingleNode(xPath);

            return nowNode;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int i = this.neuSpread1_Sheet1.ActiveRowIndex;

            if (i == 0)
            {
                return;
            }
            else
            {
                XmlNode nowNode = this.GetNowXmlNode();
                if (nowNode == null)
                    return;

                XmlNode preNode = nowNode.PreviousSibling;
                nowNode.ParentNode.InsertBefore(nowNode, preNode);



                i--;
                this.neuSpread1_Sheet1.ActiveRowIndex = i;
                this.neuSpread1_Sheet1.AddSelection(i, 0, 1, 3);
                this.neuSpread1.SetViewportTopRow(0, i - 15);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int i = this.neuSpread1_Sheet1.ActiveRowIndex;

            if (i == this.neuSpread1_Sheet1.RowCount)
            {
                return;
            }
            else
            {
                XmlNode nowNode = this.GetNowXmlNode();
                if (nowNode == null)
                    return;
                XmlNode nextNode = nowNode.NextSibling;

                nowNode.ParentNode.InsertAfter(nowNode, nextNode);


                i++;
                this.neuSpread1_Sheet1.ActiveRowIndex = i;
                this.neuSpread1_Sheet1.AddSelection(i, 0, 1, 3);
                this.neuSpread1.SetViewportTopRow(0, i - 15);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveAndReLoad();
            if (this.DisplayEvent != null)
                this.DisplayEvent(sender, e);
            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string xmlField = "";
            switch (e.Column)
            {
                case 1:
                    xmlField = "visible";
                    break;
                case 2:
                    xmlField = "sort";
                    break;
                case 3:
                    xmlField = "enable";
                    break;
            }
            if (xmlField != "")
            {
                XmlNode myNode = this.GetNowXmlNode();
                if (myNode != null && this.doc != null)
                {
                    myNode.Attributes[xmlField].Value = this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text;
                    //this.doc.Save(this.FilePath);
                    #region add by liuww
                    StreamWriter sr = new StreamWriter(FilePath, false, System.Text.Encoding.Default);
                    string cleandown = doc.OuterXml;
                    sr.Write(cleandown);
                    sr.Close();
                    #endregion
                }
            }
        }
    }

   
}
