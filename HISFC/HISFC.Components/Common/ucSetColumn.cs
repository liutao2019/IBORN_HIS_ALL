using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Neusoft.NFC.Function;

namespace UFC.Pharmacy.Base
{
    /// <summary>
    /// [��������: Fp��������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <˵��
    ///		 ͨ�������Fp ��ʾFp������Ϣ ����ά���Ƿ���ʾ/�������Ϣ
    ///  />
    /// </summary>
    public partial class ucSetColumn : UserControl
    {
        public ucSetColumn()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.InitDataTable();
                this.InitXmlDoc();
                this.ReadXmlSetData();
            }
        }

        public event System.EventHandler DisplayEvent;

        #region �����

        /// <summary>
        /// �������ļ��洢·��
        /// </summary>
        private string filePath = "PharmacyCol.xml";

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
                                                     new DataColumn("ά��",dtBool)});

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
                if (System.IO.File.Exists(this.filePath))
                    this.doc.Load(this.filePath);
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
        public void SetDataTable(string savePath,FarPoint.Win.Spread.SheetView sv)
        {
            this.filePath = savePath;

            if (System.IO.File.Exists(savePath))
            {
                this.ReadXmlSetData();
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
                                             false});
            }
        }

        /// <summary>
        /// ���ݴ������Ϣ����DataTable �ɴ����SheetView��ָ�����Զ�����
        /// </summary>
        /// <param name="sv">�����õ�SheetView</param>
        public void SetDataTable(FarPoint.Win.Spread.SheetView sv,params int[] iIndexCollection)
        {
            this.dt.Rows.Clear();
            for (int i = 0; i < iIndexCollection.Length; i++)
            {
                this.dt.Rows.Add(new Object[]{sv.Columns[iIndexCollection[i]].Label,
											  sv.Columns[iIndexCollection[i]].Visible,
											 sv.Columns[iIndexCollection[i]].AllowAutoSort,
											 true,
                                             false});
            }
        }

        /// <summary>
        /// ��������ʾ
        /// </summary>
        /// <param name="displayVisible">'��ʾ' �������Ƿ���Ч</param>
        /// <param name="sortVisible">'����' �������Ƿ���Ч</param>
        /// <param name="printVisible">'��ӡ'�������Ƿ���Ч</param>
        /// <param name="setVisible">'ά��'�������Ƿ���Ч</param>
        public void SetColVisible(bool displayVisible,bool sortVisible,bool printVisible,bool setVisible)
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
        /// ����Xml����DataSet��ʾ
        /// </summary>
        private void ReadXmlSetData()
        {
            if (System.IO.File.Exists(this.filePath))
            {
                if (!this.doc.HasChildNodes)
                    this.InitXmlDoc();
                XmlNodeList nodes = doc.SelectNodes("//Column");
                if (nodes == null)
                {
                    MessageBox.Show("Xml�ĵ���ʽ�����Ϲ淶 �����½���");
                    return;
                }
                this.dt.Rows.Clear();
                foreach (XmlNode node in nodes)
                {
                    this.dt.Rows.Add(new Object[]{ node.Attributes["displayname"].Value,
											 bool.Parse( node.Attributes["visible"].Value ),
											 bool.Parse( node.Attributes["sort"].Value ),
											 bool.Parse(node.Attributes["enable"].Value),
                                             false});
                }
            }
        }

        /// <summary>
        /// ����Xml�ĵ������¶�ȡ
        /// </summary>
        private void SaveAndReLoad()
        {
            this.doc.Save(this.filePath);
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

                this.SaveAndReLoad();

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

                this.SaveAndReLoad();

                i++;
                this.neuSpread1_Sheet1.ActiveRowIndex = i;
                this.neuSpread1_Sheet1.AddSelection(i, 0, 1, 3);
                this.neuSpread1.SetViewportTopRow(0, i - 15);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.DisplayEvent != null)
                this.DisplayEvent(sender, e);

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
                    this.doc.Save(this.filePath);
                }
            }
        }
    }

    /// <summary>
    /// ��ָ������
    /// </summary>
    public enum CheckCol
    {
        /// <summary>
        /// ����
        /// </summary>
        Display,
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        Visible,
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        Sort,
        /// <summary>
        /// �Ƿ��ӡ
        /// </summary>
        Print,
        /// <summary>
        /// �Ƿ�ά��
        /// </summary>
        Set
    }
}
