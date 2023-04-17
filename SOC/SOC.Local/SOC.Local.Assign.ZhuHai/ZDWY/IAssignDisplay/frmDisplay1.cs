using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.IO;


namespace FS.SOC.Local.Assign.ZhuHai.ZDWY.IAssignDisplay
{
    /// <summary>
    ///  FS.FrameWork.WinForms.Controls.ucBaseControl 
    /// </summary>
    public partial class frmDisplay1 : Form, FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay
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


        /// <summary>
        /// ��ʾ�Ļ�����
        /// </summary>
        private int showCount = 3;


        /// <summary>
        /// �����ӳ�ʱ��
        /// </summary>
        private double extendTime = -1;

        /// <summary>
        /// ���Ź���panel�߶�
        /// </summary>
        private int flashHeight = 70;


        /// <summary>
        /// �ײ��Ĺ��
        /// </summary>
        private string flashTitle = "ף��������";


        /// <summary>
        /// �������
        /// </summary>
        private float flashFontSize = 40;


        /// <summary>
        /// �к�ˢ��ʱ��
        /// </summary>
        int callRefreshTime = 1;

        /// <summary>
        /// ��ʿ�кŽӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Nurse.INurseAssign INurseAssign = null;

        #endregion

        public frmDisplay1()
        {
            InitializeComponent();
        }

        #region �¼�


        /// <summary>
        /// ��ѯ����
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            //��ȡ��ǰ�ؼ�����

            DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = GetNoon(currenttime);//���
            this.alQueue = queMgr.Query(ps.Nurse.ID, current, noonID);
            int intTmp = this.alQueue.Count;
            if (intTmp <= 0)
            {
                this.pnlctrl1.Controls.Clear();
                this.pnlctrl2.Controls.Clear();
                this.pnlctrl3.Controls.Clear();
                this.pnlctrl4.Controls.Clear();
                this.pnlctrl5.Controls.Clear();


                //���ó�����������Ĵ���(û��ά������)-------------------------------------??????????
            }
            //�ؼ�������ԭ����Ƚ�
            if (intTmp != queueNum)
            {   //��ֵһ���µĿؼ�/��������
                this.queueNum = intTmp;
            }
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
            if ((decimal)this.pageNum >= (decimal)this.queueNum / 5)
            {
                this.pageNum = 0;
            }
            int index = this.pageNum * 5;
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
                uc.ShowCount = this.showCount;
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
                uc.ShowCount = this.showCount;
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
                uc.ShowCount = this.showCount;
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
                uc.ShowCount = this.showCount;
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
                uc.ShowCount = this.showCount;
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
                uc.ShowCount = this.showCount;
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
                uc.ShowCount = this.showCount;
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
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl4.Controls.Add(uc);
            }
            #endregion

            #region �������ʾ
            if (index + 4 < this.alQueue.Count)
            {
                queue = this.alQueue[index + 4] as FS.HISFC.Models.Nurse.Queue;
            }
            else
            {
                queue = new FS.HISFC.Models.Nurse.Queue();
            }

            uc = null;
            try
            {
                uc = this.pnlctrl5.Controls[0] as ucQueForDisplay;
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
            }
            catch
            {
            }
            if (uc == null)
            {
                this.pnlctrl5.Controls.Clear();
                uc = new ucQueForDisplay(isDiaplayName);
                uc.SetFontSize(doctFontSize, patientFontSize, countFontSize);
                uc.ShowCount = this.showCount;
                uc.Queue = queue;
                uc.Dock = DockStyle.Fill;
                this.pnlctrl5.Controls.Add(uc);
            }
            #endregion






            this.pageNum++;


        }


        /// <summary>
        /// �кŶ�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCall_Tick(object sender, EventArgs e)
        {
            this.CalledPatient();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.FindForm().WindowState = FormWindowState.Maximized;
            base.OnLoad(e);
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {

            this.LoadMenuSet();


            FS.HISFC.BizProcess.Integrate.Manager controlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            string screenSize = controlMgr.QueryControlerInfo("900004");
            this.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) + 1, 1);

            string screenSizeX = controlMgr.QueryControlerInfo("900008");
            string screenSizeY = controlMgr.QueryControlerInfo("900009");

            this.Size = new Size(FS.FrameWork.Function.NConvert.ToInt32(screenSizeX), FS.FrameWork.Function.NConvert.ToInt32(screenSizeY));

            ps = (FS.HISFC.Models.Base.Employee)this.queMgr.Operator;
            DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = GetNoon(currenttime);//���
            this.alQueue = queMgr.Query(ps.Nurse.ID, current, noonID);

            this.queueNum = this.alQueue.Count;

            this.pnltop.Height = (this.Height - flashHeight);

            this.pnlTleft.Width = this.Width / 5;
            this.pnlTMiddle.Width = this.Width / 5;
            this.pnlTRight.Width = this.Width / 5;
            this.pnlTMiddle3.Width = this.Width / 5;
            this.pnlTMiddle2.Width = this.Width / 5;

            this.lblTitle.Text = this.flashTitle;
            this.pnlFlash.Height = this.flashHeight;
            this.lblTitle.Font = new System.Drawing.Font("����", flashFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            this.lblDelay.Font = new System.Drawing.Font("����", flashFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWait.Font = new System.Drawing.Font("����", flashFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.lblWait.Location = new Point(this.lblDelay.Location.X, this.lblDelay.Location.Y + this.lblDelay.Height + 3);
            //this.lblTitle.Location = new Point(this.lblDelay.Location.X, this.lblWait.Location.Y + this.lblWait.Height + 3);

            this.timer2.Enabled = true;//������ûˢ�£��˴���������
            this.timer4.Enabled = true;
            this.timerCall.Enabled = true;
            this.timerCall.Interval = callRefreshTime * 1000;


            this.timer4_Tick(null, null);

        }

        private void frmDisplay_DoubleClick(object sender, EventArgs e)
        {

            this.FindForm().WindowState = FormWindowState.Normal;
            this.FindForm().Close();
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == (Keys.RButton | Keys.ShiftKey | Keys.Alt))
            {
                this.timer2.Enabled = false;//������ûˢ�£��˴���������
                this.timer4.Enabled = false;
                this.timerCall.Enabled = false;
                this.FindForm().Dispose();
                this.FindForm().Close();

            }
            return base.ProcessDialogKey(keyData);
        }


        #endregion

        #region ����



        /// <summary>
        /// �½�XML
        /// </summary>
        /// <returns></returns>
        public static int CreateXML(string fileName, string extendTime, string opertime)
        {
            string path;
            try
            {
                path = fileName.Substring(0, fileName.LastIndexOf(@"\"));
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch { }

            FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            root = myXml.CreateRootElement(doc, "Setting", "1.0");

            XmlElement e = myXml.AddXmlNode(doc, root, "�ӳ�����", "");
            myXml.AddNodeAttibute(e, "ExtendTime", extendTime);

            e = myXml.AddXmlNode(doc, root, "����ʱ��", "");
            myXml.AddNodeAttibute(e, "LastOperTime", opertime);

            try
            {
                StreamWriter sr = new StreamWriter(fileName, false, System.Text.Encoding.Default);
                string cleandown = doc.OuterXml;
                sr.Write(cleandown);
                sr.Close();
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show("�޷����棡" + ex.Message); }

            return 1;
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static string GetNoon(DateTime current)
        {
            FS.HISFC.BizProcess.Integrate.Registration.Registration schemaMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

            ArrayList alNoon = schemaMgr.Query();
            if (alNoon == null) return "";
            /*
             * ʵ�����Ϊҽ������ʱ���,�������Ϊ08~11:30������Ϊ14~17:30
             * ���ԹҺ�Ա����������ʱ��ιҺ�,���п�����ʾ���δά��
             * ���Ը�Ϊ���ݴ���ʱ�����ڵ�������磺9��30��06~12֮�䣬��ô���ж��Ƿ��������
             * 06~12֮�䣬ȫ������˵��9:30���Ǹ�������
             */

            int[,] zones = new int[,] { { 0, 120000 }, { 120000, 180000 }, { 180000, 235959 } };
            int time = int.Parse(current.ToString("HHmmss"));
            int begin = 0, end = 0;

            for (int i = 0; i < 3; i++)
            {
                if (zones[i, 0] <= time && zones[i, 1] > time)
                {
                    begin = zones[i, 0];
                    end = zones[i, 1];
                    break;
                }
            }

            foreach (FS.HISFC.Models.Registration.Noon obj in alNoon)
            {
                if (int.Parse(obj.BeginTime.ToString("HHmmss")) >= begin &&
                    int.Parse(obj.EndTime.ToString("HHmmss")) <= end)
                {
                    return obj.ID;
                }
            }

            return "";
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
        /// ����ˢ�½к�
        /// </summary>
        private void CalledPatient()
        {


            DateTime current = this.assMgr.GetDateTimeFromSysDateTime();


            #region �ӳٻ���
            ArrayList delayList = new ArrayList();
            ArrayList waitList = new ArrayList();
            //delayList = assMgr.Query(ps.Dept.ID, current.Date, "ALL", FS.HISFC.Models.Nurse.EnuTriageStatus.Delay);

            waitList = assMgr.Query(ps.Dept.ID, current.Date, "ALL", FS.HISFC.Models.Nurse.EnuTriageStatus.In);

            string delay = "���Ż��ߣ�";
            for (int i = 0; i < delayList.Count; i++)
            {
                delay += (delayList[i] as FS.HISFC.Models.Nurse.Assign).Register.Name + " ";
            }
            lblDelay.Text = delay;

            string wait = string.Empty;

            if (waitList.Count > 0)
            {
                waitList.Sort(new AssignInDateCompare());
                wait = "";
            }
            else
            {
                wait = flashTitle;
            }

            for (int i = 0; i < 6; i++)
            {
                if (i < waitList.Count)
                {
                    if (i % 2 == 0 && i != 0)
                    {
                        wait += "\n";
                    }
                    string temps = "��" + (waitList[i] as FS.HISFC.Models.Nurse.Assign).Register.Name + "��" + (waitList[i] as FS.HISFC.Models.Nurse.Assign).Queue.SRoom.Name + "����";

                    wait += temps.PadRight(15, ' ');
                }
            }

            lblWait.Text = wait;

            #endregion

            /*
            if (lblWait.Location.X < 0 && (Math.Abs(lblWait.Location.X) - lblWait.Width / 2) > 0)//��label1�ұ�Ե���������Ĺ��������Ե֮��ľ���С��0ʱ
            {
                lblWait.Location = new Point(lblWait.Width / 2, lblWait.Location.Y);//����label1���Ե���������Ĺ��������Ե֮��ľ���Ϊ�ô���Ŀ��
            }
            else
            {
                lblWait.Left -= 10;//����label1���Ե���������Ĺ��������Ե֮��ľ���
            }


            if (lblTitle.Location.X < 0 && (Math.Abs(lblTitle.Location.X) - lblTitle.Width / 2) > 0)//��label1�ұ�Ե���������Ĺ��������Ե֮��ľ���С��0ʱ
            {
                lblTitle.Location = new Point(lblTitle.Width / 2, lblTitle.Location.Y);//����label1���Ե���������Ĺ��������Ե֮��ľ���Ϊ�ô���Ŀ��
            }
            else
            {
                lblTitle.Left -= 10;//����label1���Ե���������Ĺ��������Ե֮��ľ���
            }
             */



            try
            {
                string noonID = GetNoon(current);
                if (this.extendTime > 0)
                {
                    noonID = GetNoon(current.AddMinutes(-this.extendTime));
                }
                string nurseID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                FS.HISFC.BizProcess.Interface.Nurse.INurseAssign INurseAssign = null;
                INurseAssign = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.INurseAssign))
                    as FS.HISFC.BizProcess.Interface.Nurse.INurseAssign;
                if (INurseAssign != null)
                {
                    INurseAssign.Call(nurseID, noonID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// ��ȡ����Ļ����
        /// </summary>
        private void LoadMenuSet()
        {
            try
            {

                #region �к�����

                if (!System.IO.File.Exists(Application.StartupPath + "/Setting/ExtendQueue.xml"))
                {
                    CreateXML(Application.StartupPath + "/Setting/ExtendQueue.xml", "60", this.assMgr.Operator.ID);
                }
                //�Ƿ��ӳ�����ʱ�� �кŵı�������
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + "/Setting/ExtendQueue.xml");
                XmlNode node = doc.SelectSingleNode("//�ӳ�����");
                if (node != null)
                {
                    this.extendTime = double.Parse(node.Attributes["ExtendTime"].Value);
                }
                node = doc.SelectSingleNode("//�к�ˢ��ʱ��");
                if (node != null)
                {
                    this.callRefreshTime = int.Parse(node.Attributes["CallRefreshTime"].Value);
                }

                #endregion

                #region ��ʾ����

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

                System.Xml.XmlNode xnFlash = doc.SelectSingleNode("//�������");
                if (xnFlash != null)
                {
                    try
                    {
                        if (xnFlash.Attributes["Height"] != null)
                        {
                            flashHeight = int.Parse(xnFlash.Attributes["Height"].Value);
                        }

                        if (xnFlash.Attributes["Title"] != null)
                        {

                            flashTitle = xnFlash.Attributes["Title"].Value;
                        }

                        if (xnFlash.Attributes["Font"] != null)
                        {
                            flashFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnFlash.Attributes["Font"].Value);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);

                    }
                }

                System.Xml.XmlNode xnShow = doc.SelectSingleNode("//������ʾ����");
                if (xnShow != null)
                {
                    try
                    {
                        if (xnShow.Attributes["Count"] != null)
                        {
                            showCount = int.Parse(xnShow.Attributes["Count"].Value);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);

                    }
                }


                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ʾ");
            }
        }


        public class AssignInDateCompare : IComparer
        {
            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                try
                {
                    FS.HISFC.Models.Nurse.Assign assignX = x as FS.HISFC.Models.Nurse.Assign;
                    FS.HISFC.Models.Nurse.Assign assignY = y as FS.HISFC.Models.Nurse.Assign;


                    if (assignX == null)
                    {
                        return (assignY != null) ? 1 : 0;
                    }
                    else if (assignY == null)
                    {
                        return -1;
                    }


                    if (assignX.InTime < assignY.InTime)
                    {
                        return 1;
                    }
                    else if (assignX.InTime > assignY.InTime)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }

                }

                catch
                {
                    return 0;
                }

                return 0;
            }

            #endregion
        }

        #endregion

        #region IAssignDisplay ��Ա

        /// <summary>
        /// �ر���Ļ
        /// </summary>
        void FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay.Close()
        {
            if (this != null && !this.IsDisposed)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// ����Ļ
        /// </summary>
        void FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay.Show()
        {
            if (Screen.AllScreens.Length > 1)
            {
                this.Show();
                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            else
            {
                this.Show();
                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
        }

        #endregion

        #region IDisposable ��Ա

        void IDisposable.Dispose()
        {
            if (this != null && !this.IsDisposed)
            {
                this.Close();
                this.Dispose();
            }
        }

        #endregion
    }
}