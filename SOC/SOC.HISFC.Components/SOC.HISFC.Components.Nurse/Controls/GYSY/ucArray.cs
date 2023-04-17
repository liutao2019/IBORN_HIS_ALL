using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace SOC.HISFC.Components.Nurse.Controls.GYSY
{
    /// <summary>
    /// [��������: ����������]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: ����]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucArray : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ����
        /// </summary>
        public ucArray()
        {
            InitializeComponent();
        }

        #region ������
        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager controlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        
        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        
        /// <summary>
        /// ���������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Assign assginMgr = new FS.HISFC.BizLogic.Nurse.Assign();
       
        /// <summary>
        /// ��̨������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Seat seatMgr = new FS.HISFC.BizLogic.Nurse.Seat();
       
        /// <summary>
        /// ���й�����
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();
       
        /// <summary>
        /// �Һż���ʵ��
        /// </summary>
        FS.HISFC.Models.Registration.RegLevel regLevel = new FS.HISFC.Models.Registration.RegLevel();

        /// <summary>
        /// treeview2����̨��Ϣ
        /// </summary>
        private ArrayList alSeats = new ArrayList();
        private ArrayList alCollections = null;
        private TreeNode SelectedRegPatient = null;

        /// <summary>
        /// �Զ��������
        /// </summary>
        string TrigeWhereFlag = "0";

        /// <summary>
        /// ��ʾ���Ƿ�����ʾ
        /// </summary>
        bool IsShowScreen = false;

        /// <summary>
        /// �Ƿ��տ���
        /// </summary>
        bool IsByDept = true;

        #region �˻�����
        /// <summary>
        /// �˻��ҺŽӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister iNurseRegiser = null;

        /// <summary>
        /// �����ۺ�ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// �˻��Ƿ��ն˿۷�
        /// </summary>
        bool isAccountTermianFee = false;

        /// <summary>
        /// ��������ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        #endregion

        /// <summary>
        /// ���ﻼ��ˢ��Ƶ�ʣ�Ĭ�ϲ�ˢ��
        /// </summary>
        private string frequence = "None";

        /// <summary>
        /// ��ʾ��
        /// </summary>
        SOC.HISFC.Components.Nurse.Controls.GYSY.frmDisplay f = null;

        /// <summary>
        /// �Ƿ�㡰�桱�ر�
        /// </summary>
        bool IsCancel = true;

        #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
        /// <summary>
        /// ҽ���б�
        /// </summary>
        FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();

        private QueDisplayType queType = QueDisplayType.Large;

        /// <summary>
        /// �����б���ʾģʽ
        /// </summary>
        [Category("�ؼ�����"), Description("�����б���ʾģʽ")]
        public QueDisplayType QueDisplayType
        {
            get
            {
                return this.queType;
            }
            set
            {
                this.queType = value;
            }
        }

        private FormDisplayMode frmDspType = FormDisplayMode.Horizontal;

        /// <summary>
        /// �����б���ʾģʽ
        /// </summary>
        [Category("�ؼ�����"), Description("������ʾģʽ")]
        public FormDisplayMode FormDisplayMode
        {
            get
            {
                return this.frmDspType;
            }
            set
            {
                this.frmDspType = value;
            }
        }

        #endregion

        #region add by sunm 0925
        FS.FrameWork.Public.ObjectHelper queueHelper = new FS.FrameWork.Public.ObjectHelper();

        bool isTrigeExpert = false;

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        FS.FrameWork.Public.ObjectHelper noonHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �����ӳ�ʱ��
        /// </summary>
        private double extendTime = -1;

        /// <summary>
        /// �ϴ��޸Ķ����ӳ��Ĳ���ʱ��
        /// </summary>
        private DateTime dtLastOper = DateTime.MinValue;

        /// <summary>
        /// ��ǰ����ID
        /// </summary>
        private string deptID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;//Nurse.ID

        #endregion

        #region ����
        /// <summary>
        /// �Ƿ������Զ�����
        /// </summary>
        private bool isAutoTriage;

        /// <summary>
        /// �Ƿ������Զ�����
        /// </summary>
        public bool IsAutoTriage
        {
            get
            {
                return this.isAutoTriage;
            }
            set
            {
                this.isAutoTriage = value;
                this.neuGroupBox1.Visible = value;
            }
        }



        /// <summary>
        /// �Ƿ������Զ�ˢ�·��ﻼ��
        /// </summary>
        bool isFresh = true;

        /// <summary>
        /// �Ƿ������Զ�ˢ�·��ﻼ��
        /// </summary>
        [Description("�Ƿ������Զ�ˢ�·��ﻼ��,����ֻ��ʾ�����Ĳ���ˢ��"), Category("��������")]
        public bool IsFresh
        {
            get
            {
                return isFresh;
            }
            set
            {
                isFresh = value;
            }
        }

        /// <summary>
        /// ��ǰ������ʾ�����Ҹ��� ���Ϊ4��
        /// �����趨Ϊ1��2��4
        /// </summary>
        private int displayCount = 4;

        /// <summary>
        /// ��ǰ������ʾ�����Ҹ���
        /// �����趨Ϊ1��2��4
        /// </summary>
        [Category("������ʾ"), Description("��ǰ������ʾ�����Ҹ����������趨Ϊ1��2��4")]
        public int DisplayCount
        {
            get
            {
                return displayCount;
            }
            set
            {
                this.displayCount = value;
            }
        }

        /// <summary>
        /// ��ʾģʽ 0 ���շ�������ʾ��1 ����ҽ����ʾ
        /// </summary>
        private int displayMode = 0;

        /// <summary>
        /// ��ʾģʽ 0 ���շ�������ʾ��1 ����ҽ����ʾ
        /// </summary>
        [Category("������ʾ"), Description("��ʾģʽ 0 ���շ�������ʾ��1 ����ҽ����ʾ")]
        public int DisplayMode
        {
            get
            {
                return displayMode;
            }
            set
            {
                displayMode = value;
            }
        }

        /// <summary>
        /// �к�ˢ��ʱ��
        /// </summary>
        int callRefreshTime = 10;

        /// <summary>
        /// �к�ˢ��ʱ��
        /// </summary>
        [Category("�ؼ�����"), Description("�к�ˢ��ʱ�䣬���10��")]
        public int CallRefreshTime
        {
            set
            {
                if (value < 10)
                {
                    this.callRefreshTime = 10;
                }
                else
                {
                    this.callRefreshTime = value;
                }
            }
            get
            {
                return this.callRefreshTime;
            }
        }
        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zִ��, true, false, null);
            this.toolBarService.AddToolButton("ȡ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("ˢ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            this.toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X��һ��, true, false, null);
            this.toolBarService.AddToolButton("ȡ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S��һ��, true, false, null);
            this.toolBarService.AddToolButton("��ͣ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);
            this.toolBarService.AddToolButton("���ԣ�����", "", 0, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// �˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "����":
                    this.Triage();
                    break;
                case "ȡ������":
                    this.CancelTriage();
                    break;
                case "ˢ��":
                    //this.RefreshForm();
                    this.RefreshRegPatient();

                    this.RefreshQueue();

                    this.RefreshRoom();
                    this.lvPatient.Items.Clear();
                    break;
                case "����":
                    this.In();
                    break;
                case "ȡ������":
                    this.CancelIn();
                    break;
                case "��ͣ":
                    break;
                case "���ԣ�����":
                    this.SetScreen(false);
                    break;
                case "���ԣ��أ�":
                    this.SetScreen(true);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ��ʼ��

        /// <summary>
        /// ����Load�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucArray_Load(object sender, EventArgs e)
        {
            #region {4B7F932C-1C2A-4b65-B6F4-C2C1EFA19B12}
            #region ����
            // [2007/03/13] �������ļ�,�������(���߾�����Ϣ��ѯ)
            //if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
            //{
            //    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
            //    System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "/Setting/arrangeSetting.xml", System.IO.FileMode.Open);

            //    RefreshFrequence refres = (RefreshFrequence)xs.Deserialize(fs);
            //    this.neuGroupBox1.Visible = refres.IsAutoTriage;

            //    switch (refres.RefreshTime.Trim())
            //    {
            //        case "auto":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rbAuto.Checked = true;
            //            break;
            //        case "no":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rbNo.Checked = true;
            //            break;
            //        case "10":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rb10.Checked = true;
            //            break;
            //        case "30":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rb30.Checked = true;
            //            break;
            //        case "60":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rb60.Checked = true;
            //            break;
            //        case "300":
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rbOther.Checked = true;
            //            break;
            //        default:
            //            this.SetRadioFalse();
            //            fs.Close();
            //            this.rbNo.Checked = true;
            //            break;
            //    }
            //}
            //else
            //{
            //    this.SetRadioFalse();
            //    this.rbNo.Checked = true;
            //} 
            #endregion
            #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
            this.doctHelper.ArrayObject = controlMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            this.Init();
            #endregion
            this.LoadMenuSet();
            #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
            this.CreateHeaders();


            if (this.queType == QueDisplayType.Detail)
            {
                this.lvQueue.View = View.Details;
            }
            else
            {
                this.lvQueue.View = View.LargeIcon;
            }

            if (this.frmDspType == FormDisplayMode.Vertical)
            {
                this.pnlQueue.Dock = DockStyle.Left;

                this.pnlQueue.Width = 400;

                this.neuSplitter3.Dock = DockStyle.Left;
            }
            else
            {
                this.pnlQueue.Dock = DockStyle.Top;

                this.pnlQueue.Height = 200;

                this.neuSplitter3.Dock = DockStyle.Top;
            }

            #endregion
            #endregion
            InitNurseRegister();
            
            if (this.ParentForm != null)
            {
                this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
            }
            // [2007/03/13] ����
        }

        /// <summary>
        /// �رմ���ǰ�ر�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SetScreen(true);
        }

        /// <summary>
        /// ��ʼ���б�
        /// </summary>
        private void Init()
        {
            this.noonHelper.ArrayObject = this.regMgr.QueryNoon();

            this.RefreshQueue();

            this.RefreshRoom();

            #region {4B7F932C-1C2A-4b65-B6F4-C2C1EFA19B12}
            //this.LoadMenuSet();
            #endregion
            this.neuTreeView1.ImageList = this.neuTreeView1.deptImageList;
            this.neuTreeView2.ImageList = this.neuTreeView2.groupImageList;

            if (this.frequence != "Auto") this.RefreshRegPatient();
            if (this.frequence == "Auto")
            {
                this.Auto();
            }
            isAccountTermianFee = controlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, true, false);
            //��ȡ�������
            TrigeWhereFlag = this.controlMgr.QueryControlerInfo("900001");
            if (TrigeWhereFlag == null || TrigeWhereFlag == "")
            {
                TrigeWhereFlag = "0";
            }
            isTrigeExpert = controlIntegrate.GetControlParam<bool>("900003", true, false);
            this.timer2.Interval = this.callRefreshTime * 1000;
        }

        /// <summary>
        /// ���ɹҺŻ����б�
        /// </summary>
        /// <returns></returns>
        private int RefreshRegPatient()
        {
            DateTime current = this.assginMgr.GetDateTimeFromSysDateTime().Date;
            string myNurseDept = this.deptID;

            //���˿���
            this.neuTreeView1.Nodes.Clear();
            TreeNode root = new TreeNode("�����ﻼ��");
            this.neuTreeView1.Nodes.Add(root);

            this.alCollections = this.regMgr.QueryNoTriagebyDept(current, myNurseDept);
            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Registration.Register obj in this.alCollections)
                {
                    //����Ѿ����������
                    //bool blTriage = this.JudgeTrige(obj.ID);
                    //if (blTriage == true)
                    //{
                    //    continue;
                    //}
                    TreeNode node = new TreeNode();
                    //�������----------------------------------------------------------------------
                    int length = obj.Name.Trim().Length;
                    string strname = "";
                    if (length <= 7)
                    {
                        strname = obj.Name.PadRight(7 - length, ' ');
                    }
                    else
                    {
                        strname = obj.Name;
                    }
                    //ensd---------------------------------------------------------------------------
                    if (obj.DoctorInfo.Templet.Doct.ID != null && obj.DoctorInfo.Templet.Doct.ID != "")
                    {
                        node.Text = strname + " [" + obj.DoctorInfo.Templet.RegLevel.Name + "]" + " [" + obj.DoctorInfo.Templet.Doct.Name + "]" + obj.DoctorInfo.Templet.Doct.Name + "-" + obj.DoctorInfo.SeeDate.ToString();
                    }
                    else
                    {
                        node.Text = strname + " [" + obj.DoctorInfo.Templet.RegLevel.Name + "]" + obj.DoctorInfo.Templet.Dept.Name + "-" + obj.DoctorInfo.SeeDate.ToString();
                    }
                    //node.ImageIndex = 2;
                    //node.SelectedImageIndex = 2;
                    node.Tag = obj;

                    root.Nodes.Add(node);
                }
            }

            root.Expand();

            return 0;
        }

        /// <summary>
        /// ������̨�б����̨�´��ﻼ��
        /// </summary>
        /// <returns></returns>
        private int RefreshRoom()
        {
            this.neuTreeView2.Nodes.Clear();
            this.alSeats = new ArrayList();

            TreeNode root = new TreeNode("�����б�");
            this.neuTreeView2.Nodes.Add(root);

            FS.HISFC.BizLogic.Nurse.Room roomMgr = new FS.HISFC.BizLogic.Nurse.Room();

            this.alCollections = roomMgr.GetRoomInfoByNurseNo(this.deptID);
            ArrayList alPatient = null;

            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Nurse.Room obj in this.alCollections)
                {
                    if (obj.IsValid == "0") continue;

                    TreeNode node = new TreeNode();

                    node.Text = obj.Name;
                    #region {E1FBDE66-8059-496f-B631-F5815788EB47}
                    //node.ImageIndex = 41;
                    //node.SelectedImageIndex = 40;
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 3;
                    #endregion
                    node.Tag = obj;

                    root.Nodes.Add(node);

                    ArrayList alConsole = this.seatMgr.Query(obj.ID);
                    if (alConsole != null)
                    {
                        foreach (FS.HISFC.Models.Nurse.Seat seat in alConsole)
                        {
                            if (seat.PRoom.IsValid == "0") continue;

                            TreeNode node2 = new TreeNode();
                            TreeNode node3 = new TreeNode();

                            node2.Text = seat.Name + "[" + this.GetDoct(seat.ID) + "]";
                            
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 3;
                            
                            node2.Tag = seat;
                            node.Nodes.Add(node2);

                            #region ���ɴ��ﻼ����---�޸�Ϊ����

                            string queueID = "";
                            ArrayList alQueues = queueMgr.Query(
                                this.deptID,
                                this.queueMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd"));
                            foreach (FS.HISFC.Models.Nurse.Queue queue in alQueues)
                            {
                                if (queue.Console.ID == seat.ID)
                                {
                                    queueID = queue.ID;
                                    break;
                                }
                            }
                            
                            //��ʾ"���ڿ���"�Ĳ���  ==  ��Ϊҽ�����"����"��ť���������ݣ�������ʱ��ʾ�����е�һ������
                            alPatient = this.assginMgr.Query(
                                this.deptID,
                                this.assginMgr.GetDateTimeFromSysDateTime().Date,
                                queueID,
                                FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);//����״̬

                            if (alPatient != null && alPatient.Count > 0 )
                            {
                                //��ʾҽ�������µĵ�һλ����
                                node3.Text = (alPatient[0] as FS.HISFC.Models.Nurse.Assign).Register.Name;
                                node2.Nodes.Add(node3);
                            }
                            #endregion
                                                                            
                            //��ӵ���̨���飬�����Զ�����ʱ���á�
                            this.alSeats.Add(seat);

                        }//end foreach
                    }//end if
                }
                this.AddPatientByConsole();

                #region ���ø����ҵ�����(��������)
                //foreach (TreeNode node1 in this.treeView2.Nodes[0].Nodes)
                //{
                //    foreach (TreeNode node2 in node1.Nodes)
                //    {
                //        int count = node2.Nodes.Count;
                //        node2.Text = node2.Text + "(" + count.ToString() + ")";
                //    }
                //}
                #endregion
            }

            root.ExpandAll();
            return 0;
        }

        /// <summary>
        /// ������̨�ڽ��������Ѱ��ҽ������
        /// </summary>
        /// <param name="seatID"></param>
        /// <returns></returns>
        private string GetDoct(string seatID)//1128
        {
            string strDoct = "";
            for (int i = 0; i < this.lvQueue.Items.Count; i++)
            {
                FS.HISFC.Models.Nurse.Queue info =
                    this.lvQueue.Items[i].Tag as FS.HISFC.Models.Nurse.Queue;
                //�ҵ�����������
                if (info.Console.ID == seatID)
                {
                    //��������ά����ҽ��
                    if (info.Doctor.ID == null || info.Doctor.ID == "")
                    {
                        continue;
                    }
                    FS.HISFC.BizProcess.Integrate.Manager psMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    FS.HISFC.Models.Base.Employee ps = psMgr.GetEmployeeInfo(info.Doctor.ID);
                    if (ps == null || ps.Name != null || ps.Name != "")
                    {
                        strDoct = ps.Name;
                        break;
                    }
                }
            }
            return strDoct;
        }

        /// <summary>
        /// ���ɴ��ﻼ��
        /// </summary>
        private void AddPatientByConsole()
        {
            this.alCollections = this.assginMgr.QueryUnionRegister(
                this.deptID,
                this.assginMgr.GetDateTimeFromSysDateTime().Date,
                FS.HISFC.Models.Nurse.EnuTriageStatus.In
                );

            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Nurse.Assign obj in this.alCollections)
                {
                    //����clinic_code ��û����Ƿ��˺�
                    this.AddPatient(obj);
                }
            }
        }

        /// <summary>
        /// ��ӵ�����
        /// </summary>
        /// <param name="assign"></param>
        private void AddPatient(FS.HISFC.Models.Nurse.Assign assign)
        {
            foreach (TreeNode node2 in this.neuTreeView2.Nodes[0].Nodes)
            {
                foreach (TreeNode node in node2.Nodes)
                {
                    if (assign.Queue.Console.ID == (node.Tag as FS.HISFC.Models.Nurse.Seat).ID)
                    {
                        TreeNode child = new TreeNode();
                        if (assign.Register.DoctorInfo.Templet.Doct.ID != null && assign.Register.DoctorInfo.Templet.Doct.ID != "")
                        {
                            child.Text = assign.SeeNO.ToString() + "[" + assign.Register.Name + "]" + "[" + assign.Register.DoctorInfo.Templet.RegLevel.Name + "]" + "[" + assign.Register.DoctorInfo.Templet.Doct.Name + "]";
                        }
                        else
                        {
                            child.Text = assign.SeeNO.ToString() + "[" + assign.Register.Name + "]" + "[" + assign.Register.DoctorInfo.Templet.RegLevel.Name + "]";
                        }
                        #region  {E1FBDE66-8059-496f-B631-F5815788EB47}
                        //child.ImageIndex = 36;
                        //child.SelectedImageIndex = 36;
                        child.ImageIndex = 4;
                        child.SelectedImageIndex = 3;
                        #endregion
                        child.Tag = assign;

                        node.Nodes.Add(child);
                        break;
                    }

                }
            }
        }

        /// <summary>
        /// ��ѯ����̨���������Ϣ
        /// </summary>
        private void RefreshQueue()
        {
            this.lvQueue.Items.Clear();
            DateTime current = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            string noonID = SOC.HISFC.Components.Nurse.Controls.GYSY.Function.GetNoon(current);//���
            if (noonID == "")
            {
                MessageBox.Show("��ǰʱ����û�ж�Ӧ�����!", "��ʾ");
                return;
            }
            //noonID = "1";
            this.RefreshQueue(this.deptID, current, noonID);
        }

        /// <summary>
        /// ˢ�±���̨�ķ������
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <param name="noonID"></param>
        /// <returns></returns>
        private int RefreshQueue(string nurseID, DateTime queueDate, string noonID)
        {
            FS.HISFC.BizLogic.Nurse.Queue queueMgr =
                new FS.HISFC.BizLogic.Nurse.Queue();

            
            //�ҳ�ȫ���ҽ������ ygch {EEA49694-3632-427f-BF2F-6870576B9AA6}
            this.alCollections = queueMgr.Query(nurseID, queueDate.Date.ToShortDateString());

            this.queueHelper.ArrayObject = this.alCollections;

            //����
            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Nurse.Queue obj in this.alCollections)
                {
                    int count = 0;

                    //if (!obj.IsValid) continue;

                    ListViewItem item = new ListViewItem();

                    #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
                    item.SubItems.Clear();

                    item.SubItems.Add(obj.AssignDept.Name);
                    item.SubItems.Add(this.doctHelper.GetName(obj.Doctor.ID));
                    item.SubItems.Add(obj.Doctor.ID);
                    item.SubItems.Add(obj.SRoom.Name);
                    item.SubItems.Add(obj.WaitingCount.ToString());
                    item.SubItems.Add(noonHelper.GetObjectFromID(obj.Noon.ID).Name);
                    #endregion
                     
                    ArrayList al = assginMgr.Query(nurseID, queueDate.Date, obj.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
                    count += al.Count;

                    al = assginMgr.Query(nurseID, queueDate.Date, obj.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Delay);
                    count += al.Count;




                    item.Text = obj.Name;
                    if (obj.Memo != null && obj.Memo != "")
                        item.Text = item.Text + "\n" + "[" + obj.AssignDept.Name + "]";
                    
                    //item.Text = item.Text + "(" + obj.WaitingCount.ToString() + "��)";
                    if (!obj.IsValid)
                    {
                        item.Text = item.Text + "(" + count + "��)\r\n" + "[ͣ]" + FS.SOC.HISFC.BizProcess.Cache.Common.GetNoonInfo(obj.Noon.ID).Name;
                    }
                    else
                    {
                        item.Text = item.Text + "(" + count + "��)\r\n" + FS.SOC.HISFC.BizProcess.Cache.Common.GetNoonInfo(obj.Noon.ID).Name;
                    }

                    //ר�Һͷ�ר�ҵ�ͼ������
                    if (obj.ExpertFlag == "1")
                    {
                        item.ImageIndex = 1;
                    }
                    else
                    {
                        item.ImageIndex = 0;
                    }

                    item.Tag = obj;
                    //�����ȡ�Ķ�������뵱ǰ���в�һ�£��򱳾�ɫ�ú� ygch
                    if (obj.Noon.ID != noonID
                        || !obj.IsValid)
                    {
                        item.BackColor = System.Drawing.Color.Red;
                    }
                    this.lvQueue.Items.Add(item);
                }
            }
            return 0; 
        }
        
        /// <summary>
        /// �˵�����
        /// </summary>
        private void LoadMenuSet()
        {
            try
            {
                //start  �Ƿ��ϸ��տ����Զ�����ı�������,Ĭ���� -----
                XmlDocument doc2 = new XmlDocument();
                doc2.Load(Application.StartupPath + "/Setting/NurseSetting.xml");
                XmlNode node2 = doc2.SelectSingleNode("//�Ƿ񰴿��ҷ���");

                if (node2 != null && node2.Attributes["isByDept"].Value.ToString() == "false")
                {
                    this.IsByDept = false;
                }
                else
                {
                    this.IsByDept = true;
                }

                #region {4B7F932C-1C2A-4b65-B6F4-C2C1EFA19B12}
                //if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                //{
                //    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                //    System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "/Setting/arrangeSetting.xml", System.IO.FileMode.Open);

                //    RefreshFrequence refres = (RefreshFrequence)xs.Deserialize(fs);
                //this.neuGroupBox1.Visible = refres.IsAutoTriage;
                node2 = doc2.SelectSingleNode("//�Ƿ��Զ�����");
                if (node2 != null && node2.Attributes["isAutoTriage"].Value.ToString() == "false")
                {
                    this.neuGroupBox1.Visible = false;
                }
                else
                {
                    this.neuGroupBox1.Visible = true;
                }

                node2 = doc2.SelectSingleNode("//�Զ�����ˢ��");
                string refreshTime = "";
                if (node2 != null)
                {
                    refreshTime = node2.Attributes["RefreshTime"].Value.ToString();
                }

                if (this.neuGroupBox1.Visible)
                {
                    switch (refreshTime.Trim())
                    {
                        case "auto":
                            this.SetRadioFalse();
                            this.rbAuto.Checked = true;
                            break;
                        case "no":
                            this.SetRadioFalse();
                            this.rbNo.Checked = true;
                            break;
                        case "5":
                            this.SetRadioFalse();
                            this.rb5.Checked = true;
                            break;
                        case "10":
                            this.SetRadioFalse();
                            this.rb10.Checked = true;
                            break;
                        case "30":
                            this.SetRadioFalse();
                            this.rb30.Checked = true;
                            break;
                        case "60":
                            this.SetRadioFalse();
                            this.rb60.Checked = true;
                            break;
                        case "300":
                            this.SetRadioFalse();
                            this.rbOther.Checked = true;
                            break;
                        default:
                            this.SetRadioFalse();
                            this.rbNo.Checked = true;
                            break;
                    }
                }

                if (!isFresh)
                {
                    this.SetRadioFalse();
                    this.rbNo.Checked = true;
                }

                #endregion

                //this.SetFreq(frequence);
                if (!System.IO.File.Exists(Application.StartupPath + "/Setting/ExtendQueue.xml"))
                {
                    Function.CreateXML(Application.StartupPath + "/Setting/ExtendQueue.xml", "60", this.dtLastOper.ToString());
                }
                //�Ƿ��ӳ�����ʱ�� �кŵı�������
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + "/Setting/ExtendQueue.xml");
                XmlNode node = doc.SelectSingleNode("//�ӳ�����");

                if (node != null)
                {
                    this.extendTime = double.Parse(node.Attributes["ExtendTime"].Value);
                }

                node = doc.SelectSingleNode("//����ʱ��");
                if (node != null)
                {
                    this.dtLastOper = FS.FrameWork.Function.NConvert.ToDateTime(node.Attributes["LastOperTime"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ʾ");
            }
        }

        #endregion

        #region �Ϸ�

        private void neuTreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //�϶���
            if (e.Item == null) return;
            if (e.Button == MouseButtons.Left)
            {
                if ((e.Item as TreeNode).Tag == null || (e.Item as TreeNode).Tag.ToString() == "") return;

                this.SelectedRegPatient = (TreeNode)e.Item;
                DragDropEffects dropEffect = this.neuTreeView1.DoDragDrop((e.Item as TreeNode).Tag, DragDropEffects.All | DragDropEffects.Move);
            }
        }

        private void neuTreeView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
            {
                this.CancelTriage();
            }
        }

        private void neuTreeView1_DragEnter(object sender, DragEventArgs e)
        {
            this.neuTreeView1.Focus();

            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lvQueue_DragDrop(object sender, DragEventArgs e)
        {
            //����
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
            {
                //����
                ListViewItem item;

                Point p = this.lvQueue.PointToClient(Cursor.Position);

                item = this.lvQueue.GetItemAt(p.X, p.Y);
                if (item == null) return;

                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

                #region ʵ�帳ֵ
                assign.Register = (FS.HISFC.Models.Registration.Register)
                    e.Data.GetData(typeof(FS.HISFC.Models.Registration.Register));

                FS.HISFC.Models.Nurse.Queue info = (FS.HISFC.Models.Nurse.Queue)item.Tag;

                if (Function.CheckArray(assign.Register, info) == -1)
                {
                    return;
                }

                assign.Queue = info.Clone();
                #region ���������ʾ
                //ygch ��ǰ�����Ч {EEA49694-3632-427f-BF2F-6870576B9AA6}
                DateTime current = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
                string noonID = SOC.HISFC.Components.Nurse.Controls.GYSY.Function.GetNoon(current);
                if (info.Noon.ID != noonID)
                {
                    MessageBox.Show("ѡ���ҽ�����������Ч��");
                    return;
                }
                //�û����Ѿ��˺�
                if (this.Judgeback(assign.Register.ID))
                {
                    MessageBox.Show("�û����Ѿ��˺�!��ˢ����Ļ!");
                    return;
                }
                //�û����Ѿ�����
                if (this.JudgeTrige(assign.Register.ID))
                {
                    MessageBox.Show("�û����Ѿ�����!��ˢ����Ļ!", "��ʾ");
                    return;
                }

                #endregion

                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                assign.TriageDept = this.deptID;
                assign.TirageTime = this.assginMgr.GetDateTimeFromSysDateTime(); 
                assign.Oper.OperTime = assign.TirageTime;
                assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                assign.Queue.Dept = info.AssignDept;

                #endregion

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                string error = "";

                if (Function.Triage(assign, "1", ref error) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(error, "��ʾ");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                if (this.SelectedRegPatient != null)
                    this.SelectedRegPatient.Remove();

                this.RefreshWaitingCount(info.ID);

                this.RetrieveWait(this.deptID,
                                 this.assginMgr.GetDateTimeFromSysDateTime().Date,
                                 (item.Tag as FS.HISFC.Models.Nurse.Queue).ID);

            }
        }

        private void lvQueue_DragEnter(object sender, DragEventArgs e)
        {
            this.lvQueue.Focus();

            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lvQueue_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
            {
                ListViewItem item;

                Point p = this.lvQueue.PointToClient(Cursor.Position);

                item = this.lvQueue.GetItemAt(p.X, p.Y);

                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }

        private void lvPatient_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //�϶���
            if (e.Item == null) return;
            if (e.Button == MouseButtons.Left)
            {
                DragDropEffects dropEffect = lvPatient.DoDragDrop((e.Item as ListViewItem).Tag, DragDropEffects.All | DragDropEffects.Move);
            }
        }

        private void neuTreeView2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
            {
                Point p = this.neuTreeView2.PointToClient(Cursor.Position);

                TreeNode node = this.neuTreeView2.GetNodeAt(p.X, p.Y);

                if (node == null) return;

                this.neuTreeView2.SelectedNode = node;
            }
        }

        private void neuTreeView2_DragEnter(object sender, DragEventArgs e)
        {
            this.neuTreeView2.Focus();

            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void neuTreeView2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Nurse.Assign)))
            {
                //����̨
                Point p = this.neuTreeView2.PointToClient(Cursor.Position);

                TreeNode node = this.neuTreeView2.GetNodeAt(p.X, p.Y);

                if (node == null || node.Tag == null) return;
                //û�з�����̨(���߻���)���棬���ء�
                if (node.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Seat)
                    && node.Parent.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Seat)) return;


                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
                assign = (FS.HISFC.Models.Nurse.Assign)e.Data.GetData(typeof(FS.HISFC.Models.Nurse.Assign));

                if (this.Judgeback(assign.Register.ID))
                {
                    MessageBox.Show("�û����Ѿ��˺�!��ȡ���������Ϣ!");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                try
                {
                    this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    int rtn = 1;
                    if (node.Tag.GetType() == typeof(FS.HISFC.Models.Nurse.Seat))
                    {
                        rtn = assginMgr.Update(assign.Register.ID, (FS.FrameWork.Models.NeuObject)node.Parent.Tag,
                            (FS.FrameWork.Models.NeuObject)node.Tag, assginMgr.GetDateTimeFromSysDateTime());
                    }
                    else
                    {
                        rtn = assginMgr.Update(assign.Register.ID, (FS.FrameWork.Models.NeuObject)node.Parent.Parent.Tag,
                            (FS.FrameWork.Models.NeuObject)node.Parent.Tag, assginMgr.GetDateTimeFromSysDateTime());
                    }

                    if (rtn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(assginMgr.Err, "��ʾ");
                        return;
                    }
                    if (rtn == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�û��߷���״̬�Ѿ��ı�,������ˢ����Ļ!", "��ʾ");
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch (Exception err)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(err.Message, "��ʾ");
                    return;
                }
                //ˢ����Ļ
                //�ڷ����б���ɾ���÷�����Ϣ
                foreach (ListViewItem item in this.lvPatient.Items)
                {
                    if ((item.Tag as FS.HISFC.Models.Nurse.Assign).Register.ID == assign.Register.ID)
                    {
                        this.lvPatient.Items.Remove(item);
                        break;
                    }
                }
                //�ڽ����б�����ӻ���
                if (node.Tag.GetType() == typeof(FS.HISFC.Models.Nurse.Seat))
                {
                    assign.Queue.Console = (FS.FrameWork.Models.NeuObject)node.Tag;
                }
                else
                {
                    assign.Queue.Console = (FS.FrameWork.Models.NeuObject)node.Parent.Tag;
                }
                this.AddPatient(assign);
                this.RefreshWaitingCount(assign.Queue.ID);
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// �ж��Ƿ��Ѿ�����
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        private bool JudgeTrige(string ClinicCode)
        {
            //��FIN_OPB_REGISTER��,����ClinicCodeȡ������,���assign_flag��Triage,�򷵻�true,���򷵻�false
            return this.regMgr.QueryIsTriage(ClinicCode);
        }

        /// <summary>
        /// �ж��Ƿ��˺�
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        private bool Judgeback(string ClinicCode)
        {
            //��FIN_OPB_REGISTER��,����ClinicCodeȡ������,���valid_flag = 1 ����false,���򷵻�true
            return this.regMgr.QueryIsCancel(ClinicCode);
        }

        /// <summary>
        /// ���ݻ���վ��ʱ�䣬���� ��ȡ����WaitingCount�Ķ���
        /// </summary>
        /// <param name="al"></param>
        /// <param name="regDept"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Nurse.Queue GetMinQueue(ArrayList al, string regDept)
        {
            int minCount = int.MaxValue;
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime().Date; //this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;

            FS.HISFC.Models.Nurse.Queue minInfo = new FS.HISFC.Models.Nurse.Queue();

            #region ����С�����Ķ���

            ArrayList alMin = new ArrayList();
            foreach (FS.HISFC.Models.Nurse.Queue info in al)
            {
                if (info.ExpertFlag == "1") continue;

                #region �Ҷ���ҿ���
                //if (regDept != "ALL")
                //{
                //    //�����ҽ������Ķ����������Ҳ�Ҳ���...continue
                //    ArrayList alDept = new ArrayList();
                //    FS.HISFC.BizLogic.Manager.DepartmentStatManager deptMgr =
                //        new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                //    alDept = deptMgr.GetMultiDept(info.Doctor.ID);
                //    bool IsHas = false;

                //    foreach (FS.HISFC.Models.Base.DepartmentStat deptinfo in alDept)
                //    {
                //        if (deptinfo.ID == regDept)
                //        {
                //            IsHas = true;
                //            break;
                //        }
                //    }
                //    if (!IsHas) continue;
                //}
                #endregion

                if (this.IsByDept)
                {
                    if (regDept != info.AssignDept.ID) continue;
                }
                if (info.WaitingCount < minCount)
                {
                    minCount = info.WaitingCount;
                    minInfo = info;
                }

            }
            //��������С�����Ķ�����
            #endregion

            //û���ҵ�����
            if (minCount == int.MaxValue)
            {
                return null;
            }

            //��С�������� +1 
            foreach (FS.HISFC.Models.Nurse.Queue info in al)
            {
                if (info == minInfo)
                {
                    //�˶������� +1 
                    info.WaitingCount = info.WaitingCount + 1;
                }
            }

            return minInfo;
        }

        /// <summary>
        /// ��ѯĳ�����´��ﻼ��
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="queueID"></param>
        private void RetrieveWait(string nurseID, DateTime today, string queueID)
        {
            this.lvPatient.Items.Clear();

            this.alCollections = this.assginMgr.Query(nurseID, today.Date, queueID,
                FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);

            //��ʾ�ӳٽкŻ���
            this.alCollections.AddRange(this.assginMgr.Query(nurseID, today.Date, queueID, FS.HISFC.Models.Nurse.EnuTriageStatus.Delay));

            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Nurse.Assign assgin in this.alCollections)
                {
                    this.AddAssign(assgin);
                }
            }
        }

        /// <summary>
        /// ��ӷ�����Ϣ
        /// </summary>
        /// <param name="assign"></param>
        private void AddAssign(FS.HISFC.Models.Nurse.Assign assign)
        {
            ListViewItem item = new ListViewItem();
            item.Text = assign.Register.PID.CardNO;
            item.Tag = assign;

            FS.HISFC.Models.Registration.Register retTemp = regMgr.GetByClinic(assign.Register.ID);

            string ttt = "";
            if (retTemp != null)
            {
                if (retTemp.InputOper.OperTime.Date < retTemp.DoctorInfo.SeeDate.Date || retTemp.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                {
                    ttt = "[Ԥ]";
                }
            }

            if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay)
            {
                ttt+="[��]";
            }
           
            item.SubItems.Add(assign.Register.Name + ttt);

            item.SubItems.Add(assign.Register.Sex.Name);

            string age = string.Empty;
            if (string.IsNullOrEmpty(assign.Register.Age))
            {
                FS.HISFC.Models.Registration.Register regObj = this.regMgr.GetByClinic(assign.Register.ID);
                age = this.assginMgr.GetAge(regObj.Birthday, true);
            }
            else
            {
                age = assign.Register.Age;
            }

            age = age.Replace("_", "");


            if (age.IndexOf("��") > 0 || age.IndexOf("��") > 0 || age.IndexOf("��") > 0)
            {
                int iyear = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, age.IndexOf("��")));
                int iMonth = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(age.IndexOf("��") + 1, age.IndexOf("��") - age.IndexOf("��") - 1));
                int iDay = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(age.IndexOf("��") + 1, age.IndexOf("��") - age.IndexOf("��") - 1));
                if (iyear == 0)
                {
                    age = iMonth.ToString() + "��" + iDay.ToString() + "��";
                }
                else if (iyear >= 1 && iyear < 14)
                {
                    age = iyear.ToString() + "��" + iMonth.ToString() + "��";
                }
                else
                {
                    age = iyear.ToString() + "��";
                }
            }


            item.SubItems.Add(age);

            FS.HISFC.Models.Nurse.Queue queobj = new FS.HISFC.Models.Nurse.Queue();
            queobj = this.queueHelper.GetObjectFromID(assign.Queue.ID) as FS.HISFC.Models.Nurse.Queue;

            if (retTemp != null)
            {
                item.SubItems.Add(retTemp.OrderNO.ToString()+"("+retTemp.DoctorInfo.SeeNO+")");
            }
            else
            {
                item.SubItems.Add(queobj.Order.ToString() + "-" + assign.SeeNO.ToString());
            }

            item.SubItems.Add(assign.TirageTime.ToString());

            if (retTemp != null)
            {
                item.SubItems.Add(retTemp.DoctorInfo.SeeDate.ToString());
                item.SubItems.Add(retTemp.InputOper.OperTime.ToString());
            }

            this.lvPatient.Items.Add(item);
        }

        /// <summary>
        /// �жϹҺ���Ϣ����Ч��
        /// </summary>
        /// <param name="reginfo"></param>
        /// <returns></returns>
        private int ValidRegInfo(FS.HISFC.Models.Registration.Register reginfo)
        {
            //�Һű�û���ҵ��ò����ŵĻ���
            if (reginfo == null || reginfo.PID.CardNO == null || reginfo.PID.CardNO == "")
            {
                MessageBox.Show("û�иû�����Ϣ!", "��ʾ");
                this.neuTextBox1.Focus();
                this.neuTextBox1.SelectAll();
                return -1;
            }
            
            //��ֹ����
            if (this.JudgeTrige(reginfo.ID))
            {
                MessageBox.Show("�û����Ѿ�����!", "��ʾ");
                return -1;
            }
            //�û��߲����ڵ�ǰ����վ�������� �������ƴ���
            if (this.assginMgr.QueryNurseByDept(reginfo.DoctorInfo.Templet.Dept.ID) != this.deptID)
            {
                if (MessageBox.Show("���߹Һſ���[" + reginfo.DoctorInfo.Templet.Dept.Name + "]���������Ļ���վ!" + "\n" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return -1;
            }
            return 0;
        }
        
        /// <summary>
        /// ���ݲ������ڷ�����в��ҷ�����Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns>�ҵ����� 0  δ�ҵ����� -1</returns>
        private int QueryAssignRecord(string cardNo)
        {
            DateTime dt = this.assginMgr.GetDateTimeFromSysDateTime().Date;
            FS.HISFC.Models.Nurse.Assign assign = this.assginMgr.Query(dt, cardNo);
            //add by Niuxy  �ж��Ƿ�Ϊ��    
            if (assign == null)
            {
                return -1;
            }
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = assign.Register.DoctorInfo.Templet.Dept.ID;

            ArrayList altemp = this.controlMgr.QueryNurseStationByDept(obj, "14");

            if (altemp.Count != 0)
            {
                if ((altemp[0] as FS.FrameWork.Models.NeuObject).ID == this.deptID)
                {
                    MessageBox.Show("û�иû�����Ϣ��\n1 û�йҺŻ��Ǳ�����վ����\n2 �û����Ѿ�������\n3 �û����Ѿ�����");
                    return -2;
                }
            }
            
            if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == this.deptID)
            {
                //���Ѿ��������Ϣ,������
                if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                {
                    return 0;
                }
                return -1;
            }
            return -1;
        }

        /// <summary>
        /// �Һű��в���
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        private int QueryReg(string cardNo)
        {
            //��ȡ��Ч���ڵ�������Ϣ
            int validDays = FS.FrameWork.Function.NConvert.ToInt32(this.controlMgr.QueryControlerInfo("MZ0014"));
            DateTime dtvalid = this.assginMgr.GetDateTimeFromSysDateTime().Date.AddDays(1 - validDays);// this.regMgr.GetDateTimeFromSysDateTime().Date.AddDays(1 - validDays);
            
            ArrayList al = new ArrayList();
            
            al = this.regMgr.QueryUnionNurseTriage(cardNo, dtvalid);
            FS.HISFC.Models.Registration.Register reginfo = new FS.HISFC.Models.Registration.Register();

            if (al == null)
            {
                return -1;
            }
            else if (al.Count > 1)//����һ���Һ���Ϣ
            {
                
            }
            else if (al.Count == 1)//ֻ��һ���Һ���Ϣ
            {
                reginfo = (FS.HISFC.Models.Registration.Register)al[0];
            }
            else if (al.Count <= 0)//û���ҵ�
            {
                return -1;
            }

            if (reginfo == null && reginfo.PID.CardNO == null)
            {
                return -1;
            }
            
            //��ֹ�Զ������ʱ�������ˢ��û����
            if (frequence == "Auto" && this.neuTreeView1.Nodes.Count <= 0)
            {
                this.neuTreeView1.Nodes.Clear();
                TreeNode root = new TreeNode("�����ﻼ��", 40, 40);
                this.neuTreeView1.Nodes.Add(root);
            }
            //����������

            bool IsFound = false;
            foreach (TreeNode nodeCompare in this.neuTreeView1.Nodes[0].Nodes)
            {
                FS.HISFC.Models.Registration.Register obj =
                    (FS.HISFC.Models.Registration.Register)nodeCompare.Tag;
                if (obj.ID == reginfo.ID && obj.PID.CardNO == reginfo.PID.CardNO && obj != null && obj.PID.CardNO != null)
                {
                    IsFound = true;
                    this.neuTreeView1.SelectedNode = nodeCompare;
                    this.Triage();
                    break;
                }
            }

            //δ�ҵ�
            if (!IsFound)
            {
                TreeNode node = new TreeNode();

                if (reginfo.DoctorInfo.Templet.Doct.ID != null && reginfo.DoctorInfo.Templet.Doct.ID != "")
                {
                    node.Text = reginfo.Name + " [" + reginfo.DoctorInfo.Templet.RegLevel.Name + "]" + " [" + reginfo.DoctorInfo.Templet.Doct.Name + "]" + reginfo.DoctorInfo.Templet.Doct.Name + "-" + reginfo.DoctorInfo.SeeDate.ToString();
                }
                else
                {
                    node.Text = reginfo.Name + " [" + reginfo.DoctorInfo.Templet.RegLevel.Name + "]" + reginfo.DoctorInfo.Templet.Dept.Name + "-" + reginfo.DoctorInfo.SeeDate.ToString();
                }

                //node.Text = reginfo.Name + "  [" + reginfo.PID.CardNO + "] " + reginfo.DoctorInfo.Templet.Dept.Name;
                node.ImageIndex = 36;
                node.SelectedImageIndex = 35;
                node.Tag = reginfo;

                this.neuTreeView1.Nodes[0].Nodes.Add(node);
                this.neuTreeView1.SelectedNode = node;
                this.Triage();
            }
            return 0;
        }

        /// <summary>
        /// ˢ��
        /// </summary>
        /// <param name="select"></param>
        private void RefreshAssign(FS.HISFC.Models.Nurse.Assign select)
        {
            if (this.lvQueue.SelectedItems == null ||
                this.lvQueue.SelectedItems.Count == 0) return;

            ListViewItem selected = this.lvQueue.SelectedItems[0];

            if (selected.Tag.GetType() ==
                typeof(FS.HISFC.Models.Nurse.Queue))
            {
                if ((selected.Tag as FS.HISFC.Models.Nurse.Queue).ID == select.Queue.ID)
                    this.AddAssign(select);
            }
            else
            {
                if ((selected.Tag as FS.HISFC.Models.Registration.DoctSchema).ID == select.Queue.ID)
                    this.AddAssign(select);
            }
        }

        /// <summary>
        /// ˢ�º�������
        /// </summary>
        /// <param name="queueID"></param>
        private void RefreshWaitingCount(string queueID)
        {
            ArrayList queueList = queueMgr.QueryByQueueID(queueID);
            if (queueList != null || queueList.Count > 0)
            {
                FS.HISFC.Models.Nurse.Queue que = queueList[0] as FS.HISFC.Models.Nurse.Queue;


                for (int i = 0; i < lvQueue.Items.Count; i++)
                {

                    FS.HISFC.Models.Nurse.Queue queItem = lvQueue.Items[i].Tag as FS.HISFC.Models.Nurse.Queue;
                    if (queItem.ID == queueID)
                    {
                        int count = 0;
                        ArrayList al = assginMgr.Query(queItem.Dept.ID, queItem.QueueDate.Date, queItem.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
                        count += al.Count;

                        al = assginMgr.Query(queItem.Dept.ID, queItem.QueueDate.Date, queItem.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Delay);
                        count += al.Count;


                        lvQueue.Items[i].Text = que.Name;
                        if (que.Memo != null && que.Memo != "")
                            lvQueue.Items[i].Text = lvQueue.Items[i].Text + "\n" + "[" + que.AssignDept.Name + "]";
                        lvQueue.Items[i].Text = lvQueue.Items[i].Text + "(" + count.ToString() + "��)";
                    }
                }
            }
        }

        #endregion

        #region ������ר���Զ�����
        /// <summary>
        /// ר���Զ�����(ʹ��֮ǰ��Ҫˢ��һ�»��ߺͶ���)------��Ϊ������ģ��ʹ�ã����ܸ������Զ������غ�
        /// </summary>
        private void AutoExpert()
        {
            #region	ר���Ƿ��Զ�����

            if (!isTrigeExpert)
            {
                return;
            }
            #endregion

            #region ��ȡ������Ϣ
            //��ȡʱ��
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime().Date; //this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = SOC.HISFC.Components.Nurse.Controls.GYSY.Function.GetNoon(currenttime);

            //��Ҫ����Ļ�����Ϣ
            this.alCollections = this.regMgr.QueryNoTriagebyDept(current, this.deptID);
            if (this.alCollections == null) return;

            //����Ķ���
            ArrayList al = new ArrayList();
            al = queueMgr.Query(this.deptID, current, noonID);
            if (al == null || al.Count <= 0) return;
            #endregion

            #region �Զ�����
            foreach (FS.HISFC.Models.Registration.Register reg in this.alCollections)
            {
                if (reg.DoctorInfo.Templet.Doct.ID != null || reg.DoctorInfo.Templet.Doct.ID != "")
                {
                    //ʵ�帳ֵ
                    bool IsFound = false;
                    FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
                    for (int i = 0; i < this.lvQueue.Items.Count; i++)
                    {
                        assign.Queue = (FS.HISFC.Models.Nurse.Queue)this.lvQueue.Items[i].Tag;
                        if (assign.Queue.Doctor.ID == reg.DoctorInfo.Templet.Doct.ID
                            && assign.Queue.SRoom.ID != null && assign.Queue.SRoom.ID != ""
                            && assign.Queue.Console.ID != null && assign.Queue.Console.ID != "")
                        {
                            assign.Register = reg;
                            if (TrigeWhereFlag == "0")
                            {
                                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                            }
                            else
                            {
                                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                            }
                            //assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;
                            assign.TriageDept = this.deptID;
                            assign.TirageTime = this.assginMgr.GetDateTimeFromSysDateTime().Date;// this.regMgr.GetDateTimeFromSysDateTime();
                            assign.Oper.OperTime = assign.TirageTime;
                            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                            assign.Queue.Dept = assign.Register.DoctorInfo.Templet.Dept;
                            assign.InTime = this.assginMgr.GetDateTimeFromSysDateTime().Date;// this.regMgr.GetDateTimeFromSysDateTime();

                            IsFound = true;
                            break;
                        }
                    }

                    //�������ݿ�-------�и�bug�����޷�����in_date
                    if (IsFound)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        string error = "";

                        if (Function.Triage(assign, "2", ref error) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(error, "��ʾ");
                            return;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();
                    }//end if

                }
            }//----------end foreach

            #endregion

        }

        #endregion

        #region �Զ����� �ȶ���,����̨------------�����Բ���,��ʱ����
        #region ˵��
        /*--------------------------------------------------------------------
		 * 1.Ҫʹ�ô��Զ�����,������ÿһ������ά����������̨,����,��Щ���ܷ���,���û�в���
		 * 2.��ר�ҵĴ���ֱ�ӷ��ﵽ����,������
		 * 3.�Զ����ﵽ����:�����ߺ�,�ҳ���ר�Ҷ����к����������ٵĶ��в���.
		 * 4.�����Զ����ﵽ��̨:��̨����С�����賣��,����뻼��.
		 * 5.���ʱ,ֹͣ�Զ�����;��ӽ�����,�ָ��Զ�����
		 *	 ���벡���Żس�ʱ,ֹͣ�Զ�����,��������رջ���MessageBoX��ȷ����ָ�
		 * 6.ע����Ӧ����,��̨���������ı仯
		 * -------------------------------------------------------------------*/
        #endregion

        /// <summary>
        /// ʵ��ת��( �Һ� + ���� --> ���� )
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="queue"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Nurse.Assign TransEntity(FS.HISFC.Models.Registration.Register reg,
            FS.HISFC.Models.Nurse.Queue queue)
        {
            FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

            assign.Register = reg;
            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
            assign.TriageDept = this.deptID;
            assign.TirageTime = this.assginMgr.GetDateTimeFromSysDateTime();
            assign.Oper.OperTime = assign.TirageTime;
            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            assign.Queue.Dept = assign.Register.DoctorInfo.Templet.Doct;
            assign.InTime = this.assginMgr.GetDateTimeFromSysDateTime();

            assign.Queue = queue;

            return assign;
        }

        #endregion

        #region ֱ�ӷ��ﵽ��̨

        /*�Զ����ﵽ��̨-----------------------------------------------------------------
		 * 1.����׼��  
		 *		(1)�г�Ҫ����Ļ���. (2)�г�ÿ����̨����Ϣ
		 * 2.����ѡ��
		 *		(1)����Ҫ��ƥ���(���ҵĿ���Ҫ�Ǹö���ҽ�������½�Ŀ���)
		 *		(2)�������������,�ҳ���ǰ�������ٵĶ���.
		 * 3.���з���
		 *		(1)ͬʱ�����仯,�����´��ٴβ�ѯ���ݿ�
		 * 
		 * ----------------------------------------------------------------------------*/

        /// <summary>
        /// �Զ����ﵽ��̨�ĺ���
        /// </summary>
        private void Auto()
        {
            #region �Ȱѽ����ϵĻ���׼��׼����!

            //��ȡʱ��
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = SOC.HISFC.Components.Nurse.Controls.GYSY.Function.GetNoon(currenttime);

            //����׼��,��ֹ�س��Ҳ���
            this.neuTreeView1.Nodes.Clear();
            TreeNode root = new TreeNode("�����ﻼ��");
            this.neuTreeView1.Nodes.Add(root);
            this.RefreshRoom();
            this.RefreshQueue();
            if (this.lvQueue.Items.Count <= 0)
            {
                this.RefreshRegPatient();
                return;
            }

            //��ȡ������Ϣ
            this.alCollections = this.regMgr.QueryNoTriagebyDept(current, this.deptID);
            if (this.alCollections == null || this.alCollections.Count <= 0) return;

            //��ȡ�������
            ArrayList alQueue = new ArrayList();
            
            alQueue = queueMgr.Query(this.deptID, current, noonID);

            if (alQueue == null || alQueue.Count <= 0) return;
            foreach (FS.HISFC.Models.Nurse.Queue queue in alQueue)
            {
                //��������ֶ�
                queue.WaitingCount = this.assginMgr.QueryConsoleNum(queue.Console.ID, current,
                            current.AddDays(1), FS.HISFC.Models.Nurse.EnuTriageStatus.In);
            }

            #endregion

            #region ר��
            foreach (FS.HISFC.Models.Registration.Register reginfo in this.alCollections)
            {
                FS.HISFC.Models.Registration.RegLevel regl = new FS.HISFC.Models.Registration.RegLevel();
                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
                //�ų���ר��
                regl = this.regMgr.QueryRegLevelByCode(reginfo.DoctorInfo.Templet.RegLevel.ID);
                //if (reginfo.DoctorInfo.Templet.Doct.ID == null || reginfo.DoctorInfo.Templet.Doct.ID == "")
                //{
                //    continue;
                //}
                if (regl.IsExpert == false)
                {
                    continue;
                }

                //���Ҷ�Ӧ�Ķ���
                bool IsFound = false;

                for (int i = 0; i < this.lvQueue.Items.Count; i++)
                {
                    assign.Queue = (FS.HISFC.Models.Nurse.Queue)this.lvQueue.Items[i].Tag;

                    if (assign.Queue.Doctor.ID == reginfo.DoctorInfo.Templet.Doct.ID
                         && assign.Queue.AssignDept.ID == reginfo.DoctorInfo.Templet.Dept.ID)
                    //&& assign.Queue.Dept.ID == reginfo.DoctorInfo.Templet.Dept.ID)//assign.Queue.ExpertFlag == "1" && 
                    {
                        assign = this.TransEntity(reginfo, assign.Queue);
                        //���ݷ����������
                        if (this.TrigeWhereFlag == "0")
                        {
                            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                        }
                        else
                        {
                            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                        }
                        IsFound = true;
                        break;
                    }
                }

                //�ҵ�����
                if (IsFound)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    string error = "";

                    if (Function.Triage(assign, "1", ref error) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(error, "��ʾ");
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                //û���ҵ��Ĳ�������
            }
            #endregion

            #region ��ר��
            foreach (FS.HISFC.Models.Registration.Register reginfo in this.alCollections)
            {
                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
                FS.HISFC.Models.Registration.RegLevel regl = new FS.HISFC.Models.Registration.RegLevel();
                //ȡ�Һż�����Ϣ
                regl = this.regMgr.QueryRegLevelByCode(reginfo.DoctorInfo.Templet.RegLevel.ID);

                //�ų�ר��
                //if (reginfo.DoctorInfo.Templet.Doct.ID != null && reginfo.DoctorInfo.Templet.Doct.ID != "")
                //{
                //    continue;
                //}
                if (regl.IsExpert == true)
                {
                    continue;
                }

                //�ùҺſ��ҵ���С��������
                FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();
                queue = this.GetMinQueue(alQueue, reginfo.DoctorInfo.Templet.Dept.ID);
                if (queue == null)
                {
                    continue;
                }

                //ʵ��ת��
                assign = this.TransEntity(reginfo, queue);
                if (this.TrigeWhereFlag == "0")
                {
                    assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                }
                else
                {
                    assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                }

                //���ݿ����
                string error = "";
                
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                if (Function.Triage(assign, "1", ref error) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(error, "��ʾ");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion

            #region ˢ�½���
            this.RefreshRegPatient();
            this.RefreshRoom();
            #endregion

        }

        #endregion

        #region ������ش���


        /// <summary>
        /// ����
        /// </summary>
        private void Triage()
        {
            if (this.lvQueue.Items.Count <= 0)
            {
                MessageBox.Show("û��ά������!", "��ʾ");
                return;
            }

            TreeNode node = this.neuTreeView1.SelectedNode;

            if (node == null || node.Parent == null) 
                return;

            FS.HISFC.Models.Registration.Register reginfo =
                (FS.HISFC.Models.Registration.Register)node.Tag;
            FS.HISFC.Models.Registration.RegLevel myRegLevel = new FS.HISFC.Models.Registration.RegLevel();
            myRegLevel = this.regMgr.QueryRegLevelByCode(reginfo.DoctorInfo.Templet.RegLevel.ID);
            reginfo.DoctorInfo.Templet.RegLevel.IsExpert = myRegLevel.IsExpert;

            
            //��ֹ�Ѿ��˺�
            if (this.Judgeback(reginfo.ID))
            {
                this.neuTreeView1.SelectedNode.Remove();
                MessageBox.Show("�û����Ѿ��˺�!");
                return;
            }

            ucTriage f = new ucTriage(this.deptID);
            f.Register = (FS.HISFC.Models.Registration.Register)node.Tag;

            f.OK += new ucTriage.MyDelegate(f_OK);
            f.Cancel += new EventHandler(f_Cancel);

            FS.FrameWork.WinForms.Classes.Function.ShowControl(f);
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        private void CancelTriage()
        {
            if (this.lvPatient.SelectedItems == null ||
                this.lvPatient.SelectedItems.Count == 0) return;

            ListViewItem selected = this.lvPatient.SelectedItems[0];

            string error = "";

            if (MessageBox.Show("�Ƿ�Ҫȡ���÷�����Ϣ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (Function.CancelTriage((selected.Tag as FS.HISFC.Models.Nurse.Assign), ref error)
                == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(error, "��ʾ");
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.RefreshWaitingCount((selected.Tag as FS.HISFC.Models.Nurse.Assign).Queue.ID);
            this.lvPatient.Items.Remove(selected);
            this.RefreshRegPatient();
        }

        /// <summary>
        /// ȡ���Ѿ����ﵫ�������ķ����¼
        /// </summary>
        private void CancelTriageUnsee()
        {
            DateTime endDate = this.assginMgr.GetDateTimeFromSysDateTime();
            DateTime beginDate = endDate.Date;
            string noonID = SOC.HISFC.Components.Nurse.Controls.GYSY.Function.GetNoon(endDate);
            ArrayList al = new ArrayList();
            al = this.assginMgr.QueryUnInSee(beginDate.ToString(), endDate.ToString(), this.deptID, noonID);
            if (al != null && al.Count > 0)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                string error = "";
                foreach (FS.HISFC.Models.Nurse.Assign assign in al)
                {
                    if (Function.CancelTriage(assign, ref error) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(error, "��ʾ");
                        return;
                    }
                    //this.RefreshRegPatient();
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
        }

        #endregion

        #region ������ش���

        /// <summary>
        /// ����
        /// </summary>
        private void In()
        {
            if (this.lvPatient.SelectedItems == null || this.lvPatient.SelectedItems.Count == 0)
                return;

            ListViewItem selected = this.lvPatient.SelectedItems[0];

            FS.HISFC.Models.Nurse.Assign assigninfo = (FS.HISFC.Models.Nurse.Assign)selected.Tag;
            //��ֹ�Ѿ��˺�
            if (this.Judgeback(assigninfo.Register.ID))
            {
                MessageBox.Show("�û����Ѿ��˺�!��ȡ���������Ϣ");
                return;
            }

            ////ucInRoom f = new ucInRoom(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            //ucInRoom f = new ucInRoom(this.deptID);
            //f.Assign = assigninfo;
            //f.OK += new ucInRoom.MyDelegate(In_OK);
            //FS.FrameWork.WinForms.Classes.Function.ShowControl(f);

            //this.RefreshWaitingCount((selected.Tag as FS.HISFC.Models.Nurse.Assign).Queue.ID);
            ////			f.OK +=new Nurse.frmInRoom.MyDelegate(In_OK);
            ////if (f.DialogResult == DialogResult.OK)
            ////{
            ////    this.In_OK(assigninfo);
            ////}

            ////f.Dispose();
        }
        /// <summary>
        /// ��������ĳ���
        /// </summary>
        /// <param name="assign"></param>
        private void In_OK(FS.HISFC.Models.Nurse.Assign assign)
        {
            //�ڷ����б���ɾ���÷�����Ϣ
            foreach (ListViewItem item in this.lvPatient.Items)
            {
                if ((item.Tag as FS.HISFC.Models.Nurse.Assign).Register.ID == assign.Register.ID)
                {
                    this.lvPatient.Items.Remove(item);
                    break;
                }
            }
            //�ڽ����б�����ӻ���
            this.AddPatient(assign);
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        private void CancelIn()
        {
            TreeNode node = this.neuTreeView2.SelectedNode;

            if (node == null || node.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Assign))
                return;

            if (MessageBox.Show("�Ƿ�Ҫȡ���ý�����Ϣ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return;


            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            try
            {
                this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                info = node.Tag as FS.HISFC.Models.Nurse.Assign;
                int rtn = this.assginMgr.CancelIn(info.Register.ID, info.Queue.Console.ID);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.assginMgr.Err, "��ʾ");
                    return;
                }
                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�÷�����Ϣ״̬�Ѿ��ı�,��ˢ����Ļ!", "��ʾ");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();


            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
            try
            {
                this.RefreshWaitingCount(info.Queue.ID);
            }
            catch (Exception)
            { }


            //ɾ�����ﻼ��
            this.neuTreeView2.Nodes.Remove(node);
            //�ָ�Ϊ���ﻼ��
            if (this.lvQueue.SelectedItems == null ||
                this.lvQueue.SelectedItems.Count == 0) return;

            ListViewItem selected = this.lvQueue.SelectedItems[0];

            if (selected.Tag.GetType() ==
                typeof(FS.HISFC.Models.Nurse.Queue))
            {
                if ((selected.Tag as FS.HISFC.Models.Nurse.Queue).ID ==
                    (node.Tag as FS.HISFC.Models.Nurse.Assign).Queue.ID)
                    this.AddAssign((FS.HISFC.Models.Nurse.Assign)node.Tag);
            }
            else
            {
                if ((selected.Tag as FS.HISFC.Models.Registration.DoctSchema).ID ==
                    (node.Tag as FS.HISFC.Models.Nurse.Assign).Queue.ID)
                    this.AddAssign((FS.HISFC.Models.Nurse.Assign)node.Tag);
            }
            //ygch �����ĳ��Զ�������Զ�ȡ������֮��lvPatient�ؼ�������Զ������ֵ�ˣ�ֱ��Clear.... {2E027CE2-330C-4eb9-87A0-4B59B733FC4C}
            //this.lvPatient.Items.Clear();
        }
        #endregion

        #region ������
        
        /// <summary>
        /// ��������ʾ
        /// </summary>
        /// <param name="isClose">�Ƿ�رյĶ���</param>
        private void SetScreen(bool isClose)
        {
            string screenSize = this.controlMgr.QueryControlerInfo("900004");
            ToolStripButton tsb = this.toolBarService.GetToolButton("���ԣ�����");
            if (tsb == null)
            {
                tsb = this.toolBarService.GetToolButton("���ԣ��أ�");
            }

            if (tsb == null)
            {
                return;
            }

            if (isClose)
            {
                tsb.Text = "���ԣ�����";

                if (f != null)
                {
                    f.Close();
                }
            }
            else
            {
                FS.SOC.HISFC.Assign.BizLogic.Assign ass = new FS.SOC.HISFC.Assign.BizLogic.Assign();
                if (Screen.AllScreens.Length <= 1)
                {
                    if (MessageBox.Show("���ĵ���ֻ������һ����Ļ���Ƿ�ȷ����ʾ����������\r\n\r\n��ʾ�����������ܻ�Ӱ����������������", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                f = new  frmDisplay();
                //f.DisplayCount = this.displayCount;
                //f.DisplayMode = this.displayMode;
                //f.NurseCellID = ass.GetNurseCellCode(this.deptID);

                f.NurseCellID = this.deptID;
                f.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize), -30);
                f.BringToFront();
                f.Show();

                tsb.Text = "���ԣ��أ�";
            }

          
        }
        #endregion

        #region  ˢ������
        /// <summary>
        /// ����RadioButton��
        /// </summary>
        private void SetRadioFalse()
        {
            // ��ˢ��
            this.rbNo.Checked = false;
            // 5��
            this.rb5.Checked = false;
            // 10��
            this.rb10.Checked = false;
            // 30��
            this.rb30.Checked = false;
            // һ����
            this.rb60.Checked = false;
            // �����
            this.rbOther.Checked = false;
            // �Զ�
            this.rbAuto.Checked = false;
        }

        private void rbAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbAuto.Checked)
            {
                this.frequence = "Auto";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "auto";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 20000;
                this.timer1.Enabled = true;
            }
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbNo.Checked)
            {
                this.frequence = "None";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "no";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Enabled = false;
            }
        }

        private void rb5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb5.Checked)
            {
                this.frequence = "5";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "5";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 5000;
                this.timer1.Enabled = true;
            }
        }

        private void rb10_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb10.Checked)
            {
                this.frequence = "10";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "10";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 10000;
                this.timer1.Enabled = true;
            }
        }

        private void rb30_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb30.Checked)
            {
                this.frequence = "30";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "30";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 30000;
                this.timer1.Enabled = true;
            }
        }

        private void rb60_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb60.Checked)
            {
                this.frequence = "60";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "60";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 60000;
                this.timer1.Enabled = true;
            }
        }

        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbOther.Checked)
            {
                this.frequence = "300";
                RefreshFrequence rf = new RefreshFrequence();
                if (System.IO.File.Exists(Application.StartupPath + "/Setting/arrangeSetting.xml"))
                {
                    System.IO.File.Delete(Application.StartupPath + "/Setting/arrangeSetting.xml");
                }

                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/Setting/arrangeSetting.xml"), System.IO.FileMode.CreateNew, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "300";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 300000;
                this.timer1.Enabled = true;
            }
        }
        #endregion

        #region �¼��������

        /// <summary>
        /// �Զ�ˢ�¶�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.frequence != "Auto")
            {
                this.RefreshRegPatient();
                
                this.RefreshQueue();
                
                this.RefreshRoom();
                this.AutoExpert();
                
                //this.CalledPatient();//�����к�
                this.lvPatient.Items.Clear();
            }
            else
            {
                this.Auto();
            }
        }

        /// <summary>
        /// ���ƽкŶ�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.CalledPatient();
        }

        /// <summary>
        /// �����Żس�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            IsCancel = true;

            string txtStr = this.neuTextBox1.Text.Trim();

            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            string cardNo = string.Empty;
            if (isAccountTermianFee && feeMgr.ValidMarkNO(txtStr, ref accountCard) > 0)
            {
                GetAccountRegister(accountCard);
            }
            else
            {

                cardNo = txtStr.PadLeft(10, '0');
                //2.���ҹҺű�FIN_OPB_REGISTER
                int returnValue = this.QueryReg(cardNo);
                if (returnValue == 2) return;
                if (returnValue == -1)
                {
                    MessageBox.Show("û�иû��ߵĹҺ���Ϣ");
                    return;
                }
            }
            /*---------------------------------------------------------------------------------*
             * �Ե���,ͬһ���߹���ĳһ���������ŵĴ���-->����һ���б�����Թ�ѡ��.             *
             *		1.ȡ���߽������Ч������Ϣ. ��tag = 1									   *
             *		  ȡ���߽������Ч�Һ���Ϣ(�����־Ϊ0��). ��tag = 2                       *
             *		2.������� = 1 ,ֱ�ӵ�����Ӧ����										   *
             *		3.˫��,�ж�. tag=1 �򵯳�������� 2 �򵯳��������                         *
             *---------------------------------------------------------------------------------*/
        }

        /// <summary>
        /// �ѷ��ﻼ�߲�ѯ�س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCard2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;

            DateTime dt = this.assginMgr.GetDateTimeFromSysDateTime().Date;
            string cardNo = this.txtCard2.Text.ToString().Trim().PadLeft(10, '0');
            FS.HISFC.Models.Nurse.Assign assign = this.assginMgr.Query(dt, cardNo);
            
            if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == this.deptID)
            {
                string tip = "��������:" + assign.Register.Name
                    + "\n ������ :" + assign.Register.PID.CardNO
                    + "\n---------------------------------"
                    + "\n����:" + assign.Queue.SRoom.Name
                    + "\n��̨:" + assign.Queue.Console.Name
                    + "\n����ҽ��:" + this.GetDoct(assign.Queue.Console.ID)
                    + "\n�Һ�ʱ��:" + assign.Register.DoctorInfo.SeeDate.ToString()
                    + "\n\n �Ƿ���Ҫ�Ի���ȡ�����������·��";
                if (MessageBox.Show(tip, "�Ƿ���Ҫȡ������", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Function.CancelTriage(assign, ref tip);
                }
            }
            else if (assign == null)
            {
                MessageBox.Show("û����Ч������Ϣ!");
            }
        }

        private void lvQueue_Click(object sender, EventArgs e)
        {
            if (this.lvQueue.SelectedItems == null || this.lvQueue.SelectedItems.Count == 0) return;
            
            ListViewItem selected = this.lvQueue.SelectedItems[0];

            string selectedID = null;
            if (selected.Tag.GetType() == typeof(FS.HISFC.Models.Nurse.Queue))
            {
                selectedID = (selected.Tag as FS.HISFC.Models.Nurse.Queue).ID;
            }
            else
            {
                selectedID = (selected.Tag as FS.HISFC.Models.Registration.DoctSchema).ID;
                
            }
            this.RetrieveWait(this.deptID, this.assginMgr.GetDateTimeFromSysDateTime().Date, selectedID);
        }

        private void neuTreeView2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //��ȡ������Ϣ
                FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
                try
                {
                    info = (FS.HISFC.Models.Nurse.Assign)this.neuTreeView2.GetNodeAt(e.X, e.Y).Tag;
                    
                    if (info != null)
                    {
                        this.TIMEToolStripMenuItem.Text =
                            "������:" + info.Register.PID.CardNO + "    " + "\n" +
                            "�Һſ���:" + info.Register.DoctorInfo.Templet.Dept.Name + "    " + "\n" +
                            "�Ա�:" + info.Register.Sex.Name + "    " + "\n" +
                            //"����:" + age.ToString() + "---" + "\n" +
                            "�Һ�ʱ��:" + info.Register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm");
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// ���߶���ǰ��һλ
        /// </summary>
        private void UpMove()
        {
            if (this.lvPatient.SelectedItems == null ||
                this.lvPatient.SelectedItems.Count == 0) return;

            //��ȡѡ�������һ�����Ϣ
            ListViewItem selected = this.lvPatient.SelectedItems[0];
            int currentIndex = this.lvPatient.SelectedIndices[0];
            FS.HISFC.Models.Nurse.Assign info = (FS.HISFC.Models.Nurse.Assign)selected.Tag;


            //�������ݿ⣬����ʵ�ʿ������
            if (currentIndex > 0)
            {
                int priorIndex = currentIndex - 1;
                ListViewItem priorItem = this.lvPatient.Items[priorIndex];
                FS.HISFC.Models.Nurse.Assign priorinfo = (FS.HISFC.Models.Nurse.Assign)priorItem.Tag;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    if (this.assginMgr.Update(info.Register.ID, info.SeeNO.ToString(), priorinfo.SeeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������˳��ʧ��", "��ʾ");
                        return;
                    }

                    if (this.assginMgr.Update(priorinfo.Register.ID, priorinfo.SeeNO.ToString(), info.SeeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������˳��ʧ��", "��ʾ");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��������˳��ʧ��" + ex.Message, "��ʾ");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            this.RetrieveWait(this.deptID, this.assginMgr.GetDateTimeFromSysDateTime().Date, info.Queue.ID);
            
            this.lvPatient.Focus();

            for (int i = 0; i < this.lvPatient.Items.Count; i++)
            {
                FS.HISFC.Models.Nurse.Assign tempinfo = (FS.HISFC.Models.Nurse.Assign)this.lvPatient.Items[i].Tag;
                if (tempinfo.Register.ID == info.Register.ID)
                {
                    this.lvPatient.Items[i].Selected = true;
                    break;
                }
            }
        }

        /// <summary>
        /// ���߶��к���һλ
        /// </summary>
        private void DownMove()
        {
            if (this.lvPatient.SelectedItems == null || this.lvPatient.SelectedItems.Count == 0) return;
            //��ȡѡ�������һ�����Ϣ
            ListViewItem selected = this.lvPatient.SelectedItems[0];
            int currentIndex = this.lvPatient.SelectedIndices[0];
            FS.HISFC.Models.Nurse.Assign info = (FS.HISFC.Models.Nurse.Assign)selected.Tag;

            //�������ݿ⣬����ʵ�ʿ������
            if (currentIndex < this.lvPatient.Items.Count - 1)
            {
                int nextIndex = currentIndex + 1;
                ListViewItem priorItem = this.lvPatient.Items[nextIndex];
                FS.HISFC.Models.Nurse.Assign nextinfo = (FS.HISFC.Models.Nurse.Assign)priorItem.Tag;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    if (this.assginMgr.Update(info.Register.ID, info.SeeNO.ToString(), nextinfo.SeeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������˳��ʧ��", "��ʾ");
                        return;
                    }
                    if (this.assginMgr.Update(nextinfo.Register.ID, nextinfo.SeeNO.ToString(), info.SeeNO) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������˳��ʧ��", "��ʾ");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��������˳��ʧ��" + ex.Message, "��ʾ");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            this.RetrieveWait(this.deptID, this.assginMgr.GetDateTimeFromSysDateTime().Date, info.Queue.ID);
            this.lvPatient.Focus();
            for (int i = 0; i < this.lvPatient.Items.Count; i++)
            {
                FS.HISFC.Models.Nurse.Assign tempinfo = (FS.HISFC.Models.Nurse.Assign)this.lvPatient.Items[i].Tag;
                if (tempinfo.Register.ID == info.Register.ID)
                {
                    this.lvPatient.Items[i].Selected = true;
                    break;
                }
            }
        }

        private void UPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpMove();
        }

        private void DOWNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DownMove();
        }

        private void TIMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //��ȡ������Ϣ
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            try
            {
                info = (FS.HISFC.Models.Nurse.Assign)this.neuTreeView2.SelectedNode.Tag;
                if (info != null)
                {
                    this.TIMEToolStripMenuItem.Text =
                        "������:" + info.Register.PID.CardNO + "\n" +
                        "�Һſ���:" + info.Register.DoctorInfo.Templet.Dept.Name + "\n" +
                        "�Ա�:" + info.Register.Sex.Name + "\n" +
                        "����:" + info.Register.Age.ToString() + "\n" +
                        "�Һ�ʱ��:" + info.Register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm");
                }
            }
            catch
            {
                //MessageBox.Show("��ѡ��һ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void uc_OnSelect(FS.HISFC.Models.Registration.Register res)
        {
            this.IsCancel = false;
        }
        
        /// <summary>
        /// ������������ȷ����ˢ�½���
        /// </summary>
        /// <param name="assign"></param>
        private void f_OK(FS.HISFC.Models.Nurse.Assign assign)
        {
            TreeNode node = this.neuTreeView1.SelectedNode;

            //�����ж�node��Ϊ�գ����򱨴�
            if (node != null)
            {
                this.neuTreeView1.Nodes[0].Nodes.Remove(node);
            }

            this.RefreshAssign(assign);
            this.RefreshRegPatient();
            this.RefreshQueue();
            this.RefreshRoom();
        }

        /// <summary>
        /// ������������ȡ����ˢ�½���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void f_Cancel(Object sender, EventArgs e)
        {
            this.RefreshRegPatient();
            this.RefreshQueue();
            this.RefreshRoom();
        }

        #endregion

        #region �к�

        /// <summary>
        /// �����к�
        ///��ǰ��ʿվ��Ӧ���Ұ����к�{63F980B1-1F3E-4a55-A7E3-34B1640772DD}
        /// </summary>
        private void CalledPatient()
        {
            try
            {
                DateTime current = this.assginMgr.GetDateTimeFromSysDateTime();
                string noonID = SOC.HISFC.Components.Nurse.Controls.GYSY.Function.GetNoon(current);

                if (this.extendTime > 0)
                {
                    noonID = SOC.HISFC.Components.Nurse.Controls.GYSY.Function.GetNoon(current.AddMinutes(-this.extendTime));
                }
                string nurseID = this.deptID;
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

        #endregion

        #region ��ݼ�
        /// <summary>
        /// ��ݼ�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            #region {284437CB-1E5B-4fec-93AB-81924370D443}
            //if (keyData == Keys.F1)
            //{
            //    this.Triage();
            //}
            //else if (keyData == Keys.F2)
            //{
            //    this.In();
            //}
            //else if (keyData == Keys.F3)
            //{
            //    //this.CancelIn();
            //}
            //else if (keyData == Keys.F5)
            //{
            //    this.RefreshRegPatient();
            //    this.RefreshRoom();
            //}
            if (keyData == Keys.F11)
            {
                this.DownMove();
            }
            else if (keyData == Keys.F12)
            {
                this.UpMove();
            }
            else if (keyData.GetHashCode() == Keys.Add.GetHashCode())
            {
                this.DownMove();
            }
            else if (keyData.GetHashCode() == Keys.Subtract.GetHashCode())
            {
                this.UpMove();
            }
            #endregion

            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region {51AB60B7-8610-40c4-B5BC-CD978E788892}

        /// <summary>
        /// ����ListView����
        /// </summary>
        private void CreateHeaders()
        {
            //
            ColumnHeader colHead;
            colHead = new ColumnHeader();
            colHead.Text = "��������";
            colHead.Width = 150;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "����";
            colHead.Width = 100;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "ҽ��";
            colHead.Width = 100;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "ҽ�����";
            colHead.Width = 80;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "����";
            colHead.Width = 100;
            this.lvQueue.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "��������";
            colHead.Width = 100;
            this.lvQueue.Columns.Add(colHead);
            //��� ygch {EEA49694-3632-427f-BF2F-6870576B9AA6}
            colHead = new ColumnHeader();
            colHead.Text = "���";
            colHead.Width = 50;
            this.lvQueue.Columns.Add(colHead);

        }

        private void lvQueue_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lvQueue.SelectedItems == null || this.lvQueue.SelectedItems.Count == 0)
                {
                    this.lvQueue.ContextMenuStrip = this.mnuSet;
                    this.MenuSetControl(true);
                }
                else
                {
                    this.lvQueue.ContextMenuStrip = null;
                }
            }
        }

        /// <summary>
        /// �������ò˵�List����ʾ��ʽ
        /// </summary>
        /// <param name="flag"></param>
        private void MenuSetControl(bool flag)
        {
            this.mnuI_Large.Checked = false;
            this.mnuI_Detail.Checked = false;

            if (this.lvQueue.Visible)
            {
                switch (this.lvQueue.View)
                {
                    case View.LargeIcon:
                        this.mnuI_Large.Checked = flag;
                        break;
                    case View.Details:
                        this.mnuI_Detail.Checked = flag;
                        break;
                    default:
                        this.mnuI_Large.Checked = flag;
                        break;
                }
            }
        }

        private void mnuI_Large_Click(object sender, EventArgs e)
        {
            this.lvQueue.Visible = true;

            this.lvQueue.View = View.LargeIcon;
            this.MenuSetControl(true);
        }

        private void mnuI_Detail_Click(object sender, EventArgs e)
        {
            this.lvQueue.Visible = true;

            this.lvQueue.View = View.Details;
            this.MenuSetControl(true);
        }


        #endregion

        #region �˻�����

        private int InitNurseRegister()
        {
            object obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister));
            if (obj != null)
            {
                this.iNurseRegiser = obj as FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister;
                iNurseRegiser.OnGetRegister += new FS.HISFC.BizProcess.Interface.Registration.GetRegisterHander(iNurseRegiser_OnGetRegister);
            }

            return 1;
        }

        void iNurseRegiser_OnGetRegister(ref FS.HISFC.Models.Registration.Register reg)
        {
            TreeNode node = new TreeNode();
            node.ImageIndex = 36;
            node.SelectedImageIndex = 35;
            node.Text = reg.Name;
            node.Tag = reg;
            this.neuTreeView1.Nodes[0].Nodes.Add(node);
            this.neuTreeView1.SelectedNode = node;
        }

        private int GetAccountRegister(FS.HISFC.Models.Account.AccountCard accountCard)
        {

            decimal vacancy = 0m;
            //ͨ���������ж��˻�״̬�Ƿ���
            int returnValue = this.feeMgr.GetAccountVacancy(accountCard.Patient.PID.CardNO, ref vacancy);

            if (returnValue <= 0)
            {
                MessageBox.Show(feeMgr.Err);
                return -1;
            }

            if (!feeMgr.CheckAccountPassWord(accountCard.Patient))
            {
                return -1;
            }
            iNurseRegiser.Patient = accountCard.Patient;
            FS.FrameWork.WinForms.Classes.Function.ShowControl(iNurseRegiser as Control);
            return 1;
        }
        #endregion

        #region IInterfaceContainer ��Ա

        /// <summary>
        /// �ӿ�����
        /// </summary>
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] types = new Type[1];
                types[0] = typeof(FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister);
                return types;
            }
        }

        #endregion

        #region ����
        //private void RefreshForm()
        //{
        //    this.AutoExpert();
        //    this.RefreshRegPatient();
        //    this.RefreshQueue();
        //    this.RefreshRoom();
        //}

        //private void neuLabelTextBox1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyData != Keys.Enter) return;

        //    string cardNo = this.neuTextBox1.Text.ToString().Trim().PadLeft(10, '0');

        //    //1.���ұ�MET_NUO_ASSIGNRECORD
        //    if (this.QueryAssignRecord(cardNo) == 0) return;

        //    //2.���ҹҺű�FIN_OPB_REGISTER
        //    this.QueryReg(cardNo);
        //    /*---------------------------------------------------------------------------------*
        //     * �Ե���,ͬһ���߹���ĳһ���������ŵĴ���-->����һ���б�����Թ�ѡ��.             *
        //     *		1.ȡ���߽������Ч������Ϣ. ��tag = 1									   *
        //     *		  ȡ���߽������Ч�Һ���Ϣ(�����־Ϊ0��). ��tag = 2                       *
        //     *		2.������� = 1 ,ֱ�ӵ�����Ӧ����										   *
        //     *		3.˫��,�ж�. tag=1 �򵯳�������� 2 �򵯳��������                         *
        //     *---------------------------------------------------------------------------------*/
        //}
        #endregion
    }

    #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
    /// <summary>
    /// ������ʾģʽ
    /// </summary>
    public enum QueDisplayType
    {
        /// <summary>
        /// ��ͼ��
        /// </summary>
        Large,
        /// <summary>
        /// ��ϸ�б�
        /// </summary>
        Detail
    }

    /// <summary>
    /// ���кͷ����б����ʾģʽ
    /// </summary>
    public enum FormDisplayMode
    {
        /// <summary>
        /// ����
        /// </summary>
        Horizontal,
        /// <summary>
        /// ����
        /// </summary>
        Vertical
    }
    #endregion

    class SortQueue : IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            return (x as FS.HISFC.Models.Nurse.Queue).WaitingCount.CompareTo(
                    (y as FS.HISFC.Models.Nurse.Queue).WaitingCount);
        }

        #endregion
    }
}