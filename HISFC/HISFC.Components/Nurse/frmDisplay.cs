using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace FS.HISFC.Components.Nurse
{
    public partial class frmDisplay : Form
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
        private int queueNum = 0;
        private int nowNum = 0;

        #endregion

        public frmDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ֵ
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //ר�Ҷ��б�־
			this.rtbcontent.Tag = "0";

			ArrayList al = new ArrayList();
			FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();
			DateTime current = this.assMgr.GetDateTimeFromSysDateTime().Date;
			for(int i = nowNum ; i < this.queueNum ; i++)
			{
				nowNum++;
				if(nowNum >= queueNum) nowNum = 0;

				queue = (FS.HISFC.Models.Nurse.Queue)this.alQueue[nowNum];
				if(queue.ExpertFlag == "1")
				{
					this.rtbcontent.Tag = "1";
				}
				al = this.assMgr.QueryPatient(current,current.AddDays(1),queue.Console.ID,"1",queue.Doctor.ID);
				if(al.Count > 0) break;
			}
            FS.HISFC.BizProcess.Integrate.Manager psMgrt = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Base.Employee pst = new FS.HISFC.Models.Base.Employee();
			pst = psMgrt.GetEmployeeInfo(queue.Doctor.ID);
			this.rtbcontent.Text = queue.SRoom.Name + "--" + queue.Console.Name + "[" + pst.Name +"]" + "\n";
           
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Nurse.Assign info = (FS.HISFC.Models.Nurse.Assign)al[i];

                this.rtbcontent.Text = this.rtbcontent.Text + "[" + (i + 1).ToString().PadLeft(2, '0') + "]"
                    + this.PadName(info.Register.Name);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            //��ȡ��ǰ�ؼ�����
            ps = psMgr.GetEmployeeInfo(this.queMgr.Operator.ID);
            DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Nurse.Function.GetNoon(currenttime);//���
            this.alQueue = queMgr.Query(ps.Nurse.ID, current, noonID);
            int intTmp = this.alQueue.Count;
            if (intTmp <= 0)
            {
                this.Controls.Clear();
                //���ó�����������Ĵ���(û��ά������)-------------------------------------??????????
            }
            //�ؼ�������ԭ����Ƚ�
            if (intTmp != queueNum && intTmp > 0)
            {
                if (queueNum > 0)
                {
                    this.Controls.Clear();
                }
                //��ֵһ���µĿؼ�/��������
                this.queueNum = intTmp;
            }
        }

        /// <summary>
        /// ʱ��
        /// </summary>
        private void timer3_Tick(object sender, EventArgs e)
        {
            this.label2.Text = "ף�����տ���   "+ DateTime.Now.ToString();
        }

        Hashtable htDepts = new Hashtable();

        private void frmDisplay_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizProcess.Integrate.Manager controlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            string screenSize = controlMgr.QueryControlerInfo("900004");
            this.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) + 1, 0);

            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            string screenSizeX = controlMgr.QueryControlerInfo("900008");
            string screenSizeY = controlMgr.QueryControlerInfo("900009"); 
            #endregion

            this.Size = new Size(FS.FrameWork.Function.NConvert.ToInt32(screenSizeX), FS.FrameWork.Function.NConvert.ToInt32(screenSizeY));

            ps = psMgr.GetEmployeeInfo(this.queMgr.Operator.ID);
            DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Nurse.Function.GetNoon(currenttime);//���
            this.alQueue = queMgr.Query(ps.Nurse.ID, current, noonID);
            this.queueNum = this.alQueue.Count;

            this.label1.Text = "��ӭ��" + (new FS.HISFC.BizProcess.Integrate.Manager()).GetHospitalName() + "����";

            FS.HISFC.BizLogic.Manager.DepartmentStatManager stat = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alDepts = stat.LoadByParent("14", ps.Nurse.ID);

            foreach (FS.HISFC.Models.Base.DepartmentStat dep in alDepts)
            {
                if (!htDepts.Contains(dep.DeptCode))
                    htDepts.Add(dep.DeptCode, dep);
            }

            this.timer3.Start();
            this.timer2.Start();
            this.timer1.Start();
            this.timer4.Start();
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

        private void timer4_Tick(object sender, EventArgs e)
        {
            #region �кŴ���
            //Ŀǰ�����ƽ���Lily And Wang �����⣬����ֱ�ӷ���
            if (System.IO.File.Exists("TTS.exe"))
            {
                this.timer4.Stop();

                DataSet dsPat = new DataSet();

                this.roomMgr.ExecQuery("Nurse.QueuyCallQueue.1", ref dsPat,"");

                if (dsPat != null && dsPat.Tables.Count > 0 && dsPat.Tables[0].Rows.Count > 0)
                {
                    this.timer4.Enabled = false;

                    foreach (DataRow row in dsPat.Tables[0].Rows)
                    {
                        FS.FrameWork.Models.NeuObject obj = this.htDepts[row[3].ToString()] as FS.FrameWork.Models.NeuObject;

                        if (obj == null || obj.ID == "")
                        {
                            continue;
                        }

                        this.queMgr.DelCallQueue(row[0].ToString());

                        //����LED������

                        System.Diagnostics.Process.Start("TTS.exe", "��" + row[1].ToString() + "��" + row[2].ToString());

                        System.Threading.Thread.Sleep(6000);
                    }
                }

                this.timer4.Start();

            }

            #endregion

            #region LED��Ļ��ʾ����
            #endregion
        }
    }
}