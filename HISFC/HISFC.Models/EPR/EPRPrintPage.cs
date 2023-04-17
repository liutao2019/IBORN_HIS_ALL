using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Text;

namespace FS.HISFC.Models.EPR
{
    /// <summary>
    /// ��������
    /// </summary>
    /// 
    [System.Serializable]
    public class EPRPrintPage:FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// ��ӡͼƬ
        /// </summary>
        private Image img;

        public Image Img
        {
            get { return img; }
            set { img = value; }
        }
        private FS.HISFC.Models.Base.PageSize myPageSize;

        public FS.HISFC.Models.Base.PageSize pageSize
        {
            get { return myPageSize; }
            set { myPageSize = value; }
        }
        /// <summary>
        /// ��ӡҳ
        /// </summary>
        private int myPage;

        public int Page
        {
            get { return myPage; }
            set { myPage = value; }
        }
        //private string myCheckPrints;
        /// <summary>
        /// �ļ�����
        /// </summary>
        private string myFileName;

        public string FileName
        {
            get { return myFileName; }
            set { myFileName = value; }
        }
        /// <summary>
        /// ��ӡ�ؼ�������Row��CheckPrint����
        /// </summary>
        private ArrayList mySortedControls;

        /// <summary>
        /// ��ӡ�ؼ�������Row��CheckPrint���ʹ�ӡ�ؼ�
        /// </summary>
        public ArrayList SortedControls
        {
            get { return mySortedControls; }
            set { mySortedControls = value; }
        }
        public string SortedControlsXml
        {
            get
            {
                FS.FrameWork.Xml.XML xml = new FS.FrameWork.Xml.XML();
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlElement root = xml.CreateRootElement(doc, "Controls");
                string strInnerXml = "";
                foreach (SortedControl ctr in this.SortedControls)
                {
                    strInnerXml += ctr.xmlNode.OuterXml;
                }
                root.InnerXml = strInnerXml;
                return doc.OuterXml;
            }
            set
            {
                this.SortedControls = new ArrayList();
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(value);
                System.Xml.XmlElement root = doc.DocumentElement;
                foreach (System.Xml.XmlNode node in root.ChildNodes)
                {
                    SortedControl ctr = new SortedControl((System.Xml.XmlElement)node);
                    this.SortedControls.Add(ctr);
                }
            }
        }

        /// <summary>
        /// ��ʼ����
        /// Ϊдҳüת�ơ�ת��׼��
        /// </summary>
        private DateTime myBeginDate;

        /// <summary>
        /// ��ʼ����
        /// Ϊдҳüת�ơ�ת��׼��
        /// </summary>
        public DateTime BeginDate
        {
            get { return myBeginDate; }
            set { myBeginDate = value; }
        }

        /// <summary>
        /// ��������
        /// Ϊдҳüת�ơ�ת����Ϣ׼��
        /// </summary>
        private DateTime myEndDate;

        /// <summary>
        /// ��������
        /// Ϊдҳüת�ơ�ת����Ϣ׼��
        /// </summary>
        public DateTime EndDate
        {
            get { return myEndDate; }
            set { myEndDate = value; }
        }

        /// <summary>
        /// ����סԺ��
        /// </summary>
        private string MyPatientNo;

        /// <summary>
        /// ����סԺ��
        /// </summary>
        public string PatientNo
        {
            get { return MyPatientNo; }
            set { MyPatientNo = value; }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private int myStartRow;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public int StartRow
        {
            get { return myStartRow; }
            set { myStartRow = value; }
        }


    }
}
