using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Nurse.Controls.SDFY
{
    public partial class frmDisplay1 : Form
    {
        #region ����

        private FS.HISFC.BizLogic.Nurse.Queue queMgr = new FS.HISFC.BizLogic.Nurse.Queue();
        private FS.HISFC.BizProcess.Integrate.Manager psMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Nurse.Assign assMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        private FS.HISFC.Models.Base.Employee ps = new FS.HISFC.Models.Base.Employee();
        //private FS.HISFC.Models.RADT.Person ps = new FS.HISFC.Models.RADT.Person();
        private FS.HISFC.BizProcess.Integrate.Registration.Registration doctSchemaMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        private FS.HISFC.BizLogic.Nurse.Room roomMgr = new FS.HISFC.BizLogic.Nurse.Room();

        private ArrayList alQueue = new ArrayList();
        private ArrayList alBook = new ArrayList();

        /// <summary>
        /// �ܶ�����
        /// </summary>
        private int queueNum = 0;

        /// <summary>
        /// ��ǰҳ��
        /// </summary>
        private int pageNum = 0;

        /// <summary>
        /// �趨��ҳʱÿҳˢ�µ��ٶ� ���뵥λ Ĭ��30��
        /// </summary>
        public int PageRefreshTime 
        {
            set
            {
                //�������Ĳ������� ��Ĭ��Ϊ30sˢ��
                if (value <= 0)
                {
                    this.timer4.Interval = 10000;
                }
                else
                {
                    this.timer4.Interval = value;
                }
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��������
        /// </summary>
        private bool isDiaplayName = true;

        /// <summary>
        /// ҽ��������ʾ����
        /// </summary>
        private float doctFontSize = 30;

        /// <summary>
        /// ����������ʾ����
        /// </summary>
        private float patientFontSize = 30;

        /// <summary>
        /// ����������ʾ����
        /// </summary>
        private float countFontSize = 30;

        #endregion

        public frmDisplay1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ����
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            //��ȡ��ǰ�ؼ�����

            DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Function.GetNoon(currenttime);//���
            this.alQueue = queMgr.Query(ps.Nurse.ID, current, noonID);            
            int intTmp = this.alQueue.Count;
            if (intTmp <= 0)
            {
                this.pnlctrl1.Controls.Clear();
                this.pnlctrl2.Controls.Clear();
                this.pnlctrl3.Controls.Clear();
                this.pnlctrl4.Controls.Clear();
                //���ó�����������Ĵ���(û��ά������)-------------------------------------??????????
            }
            //�ؼ�������ԭ����Ƚ�
            if (intTmp != queueNum)
            {   //��ֵһ���µĿؼ�/��������
                this.queueNum = intTmp;
            }
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizProcess.Integrate.Manager controlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            string screenSize = controlMgr.QueryControlerInfo("900004");
            this.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) + 1, 1);

            string screenSizeX = controlMgr.QueryControlerInfo("900008");
            string screenSizeY = controlMgr.QueryControlerInfo("900009");

            this.Size = new Size(FS.FrameWork.Function.NConvert.ToInt32(screenSizeX), FS.FrameWork.Function.NConvert.ToInt32(screenSizeY));

            ps = (FS.HISFC.Models.Base.Employee)this.queMgr.Operator;
            DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Function.GetNoon(currenttime);//���
            this.alQueue = queMgr.Query(ps.Nurse.ID, current, noonID);

            this.queueNum = this.alQueue.Count;

            this.pnltop.Height = this.Height / 2;
            this.pnlTleft.Width = this.Width / 2;
            this.pnlBleft.Width = this.Width / 2;

            this.timer2.Enabled = true;//������ûˢ�£��˴���������
            this.timer4.Enabled = true;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            string pathName = Application.StartupPath + "/Setting/NurseSetting.xml";
            if (!System.IO.File.Exists(pathName)) return;
            doc.Load(pathName);
            System.Xml.XmlNode xn = doc.SelectSingleNode("//�Ƿ���������");
            if (xn != null)
            {
                isDiaplayName = FS.FrameWork.Function.NConvert.ToBoolean(xn.Attributes[0].Value);
            }

            System.Xml.XmlNode xnDoct = doc.SelectSingleNode("//ҽ����������");
            if (xnDoct != null)
            {
                try
                {
                    doctFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnDoct.Attributes[0].Value);
                }
                catch (Exception)
                {
                    doctFontSize = 30;           
                }
                
            }
            System.Xml.XmlNode xnPatient = doc.SelectSingleNode("//������������");
            if (xnPatient != null)
            {
                try
                {
                    patientFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnPatient.Attributes[0].Value);
                }
                catch (Exception)
                {
                    patientFontSize = 30;
                }
                
            }
            System.Xml.XmlNode xnCount = doc.SelectSingleNode("//������������");
            if (xnCount != null)
            {
                try
                {
                    countFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnCount.Attributes[0].Value);
                }
                catch (Exception)
                {
                    countFontSize = 30;
                }
            }

            
            this.timer4_Tick(null, null);

        }

        private void frmDisplay_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string PadName(string name)
        {
            //�����ֲ���(ԭ��6.5.4)
            int n = name.Length;
            string strname = "";
            if (n == 2)
            {
                strname = name.PadRight(6, ' ');
            }
            else if (n == 3)
            {
                strname = name.PadRight(5, ' ');
            }
            else if (n == 4)
            {
                strname = name.PadRight(4, ' ');
            }
            return strname;
        }

        /// <summary>
        /// �����һ������վ���ж���4���������ʱ��ʾ����ҳ����� 
        /// ����ȫ�ֱ�����page��ǰ��ʾҳ��  currentPageQueue��ǰҳ�ķ�������б� 
        /// ÿ��ˢ�°�page��alQueue��ȡ����ǰҳ��Ӧ����ʾ���ĸ�������У���ֵ��currentPageQueue�б��У�Ȼ����ʾ����Ļ��
        /// ÿ��ˢ�� page+1 
        /// ygch {E7AD911A-5EFD-4999-8849-52BA9D61FAD7}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer4_Tick(object sender, EventArgs e)
        {
            //���������Ϊ0���ٴ���
            if (this.queueNum == 0)
            {
                return;
            }
            //�����ǰҳҳ��������һҳ����ת����һҳ
            if ((decimal)this.pageNum >= (decimal)this.queueNum / 4)
            {
                this.pageNum = 0;
            }
            int index = this.pageNum * 4;
            //�ѵ�ǰҳ���������ʾ����ʾ��Ļ
            FS.HISFC.Models.Nurse.Queue queue = null;
            ucQueForDisplay uc = null;
            #region ��һ����ʾ
            if (index < this.alQueue.Count)
            {
                queue = this.alQueue[index] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }

            try
            {
                uc = this.pnlctrl1.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl1.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl1.Controls.Add(uc);
            }
            #endregion

            #region �ڶ�����ʾ
            if (index + 1 < this.alQueue.Count)
            {
                queue = this.alQueue[index + 1] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }
            uc = null;
            try
            {
                uc = this.pnlctrl2.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl2.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl2.Controls.Add(uc);
            }
            #endregion

            #region ��������ʾ
            if (index + 2 < this.alQueue.Count)
            {
                queue = this.alQueue[index + 2] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }

            uc = null;
            try
            {
                uc = this.pnlctrl3.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl3.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl3.Controls.Add(uc);
            }
            #endregion

            #region ���ĸ���ʾ
            if (index + 3 < this.alQueue.Count)
            {
                queue = this.alQueue[index + 3] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }

            uc = null;
            try
            {
                uc = this.pnlctrl4.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl4.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl4.Controls.Add(uc);
            }
            #endregion

            
            this.pageNum++;
          

        }
    }
}