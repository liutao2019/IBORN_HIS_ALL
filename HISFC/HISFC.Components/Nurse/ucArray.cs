using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FS.HISFC.Components.Nurse
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
        //FS.HISFC.BizLogic.Registration.RegLevel regLevelManager = new FS.HISFC.BizLogic.Registration.RegLevel();
        FS.HISFC.Models.Registration.RegLevel regLevel = new FS.HISFC.Models.Registration.RegLevel();
        
        /// <summary>
        /// treeview2����̨��Ϣ
        /// </summary>
        private ArrayList alSeats = new ArrayList();
        private ArrayList alCollections = null;
        private TreeNode SelectedRegPatient = null;

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
        /// �Զ�������ȱ�־
        /// </summary>
        string TrigeWhereFlag = "0";
        /// <summary>
        /// ��ʾ���Ƿ�����ʾ
        /// </summary>
        bool IsShowScreen = false;
        /// <summary>
        /// �Ƿ��п��Ҳ�����Ӧ��ϵ{F044FCF3-6736-4aaa-AA04-4088BB194C20}
        /// </summary>
        bool isNurseDept = true;       
        /// <summary>
        /// ��ʾ��
        /// </summary>
        //Nurse.frmDisplay f = null;

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
        Nurse.frmDisplay f = null;
        /// <summary>
        /// �Ƿ�㡰�桱�ر�
        /// </summary>
        bool IsCancel = true;

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
        /// �Ƿ��п��Ҳ�����Ӧ��ϵ{F044FCF3-6736-4aaa-AA04-4088BB194C20}
        /// </summary>
        public bool IsNurseDept
        {
            get { return isNurseDept; }
            set { isNurseDept = value; }
        }
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

        #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}

        /// <summary>
        /// ר���Ƿ��Զ�����
        /// </summary>
        bool isTrigeExpert = false;

        #endregion

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zִ��, true, false, null);
            this.toolBarService.AddToolButton("ȡ������", "",(int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
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
                    #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
                    this.RefreshQueue();
                    #endregion
                    this.RefreshRoom();
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
                    this.Screen();
                    break;
                #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
                case "���ԣ��أ�":
                    this.Screen();
                    break;
                #endregion
                default:
                    break;
            }
        }

        private void RefreshForm()
        {
            this.AutoExpert();
            this.RefreshRegPatient();
            this.RefreshQueue();
            this.RefreshRoom();
        }

        private void Triage()
        {
            if (this.lvQueue.Items.Count <= 0)
            {
                MessageBox.Show("û��ά������!", "��ʾ");
                return;
            }

            TreeNode node = this.neuTreeView1.SelectedNode;

            if (node == null || node.Parent == null) return;

            FS.HISFC.Models.Registration.Register reginfo =
                (FS.HISFC.Models.Registration.Register)node.Tag;
            FS.HISFC.Models.Registration.RegLevel myRegLevel = new FS.HISFC.Models.Registration.RegLevel();
            myRegLevel = this.regMgr.QueryRegLevelByCode(reginfo.DoctorInfo.Templet.RegLevel.ID);
            reginfo.DoctorInfo.Templet.RegLevel.IsExpert = myRegLevel.IsExpert;
            //��ֹ����
            if (this.JudgeTrige(reginfo.ID))
            {
                this.neuTreeView1.SelectedNode.Remove();
                MessageBox.Show("�û����Ѿ�����!", "��ʾ");
                return;
            }
            //��ֹ�Ѿ��˺�
            if (this.Judgeback(reginfo.ID))
            {
                this.neuTreeView1.SelectedNode.Remove();
                MessageBox.Show("�û����Ѿ��˺�!");
                return;
            }

            //ucTriage f = new ucTriage(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            
            ucTriage f = new ucTriage(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            f.Register = (FS.HISFC.Models.Registration.Register)node.Tag;

            f.OK += new ucTriage.MyDelegate(f_OK);
            f.Cancel+=new EventHandler(f_Cancel);
           
            FS.FrameWork.WinForms.Classes.Function.ShowControl(f);
        }

        /// <summary>
        /// ����ȷ����ˢ�½���
        /// </summary>
        /// <param name="assign"></param>
        private void f_OK(FS.HISFC.Models.Nurse.Assign assign)
        {
            TreeNode node = this.neuTreeView1.SelectedNode;

            //{AAA35EEB-7FCD-4fa4-8CAA-9321E2AE3050}
            //�����ж�node��Ϊ�գ����򱨴�
            if (node != null)
            {
                this.neuTreeView1.Nodes[0].Nodes.Remove(node);
            }
            //{AAA35EEB-7FCD-4fa4-8CAA-9321E2AE3050}
            this.RefreshAssign(assign);
            this.RefreshRegPatient();
            this.RefreshQueue();
            this.RefreshRoom();

        }

        private void f_Cancel(Object sender, EventArgs e)
        {
            this.RefreshRegPatient();
            this.RefreshQueue();
            this.RefreshRoom();
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

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            if (Function.CancelTriage( (selected.Tag as FS.HISFC.Models.Nurse.Assign), ref error)
                == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(error, "��ʾ");
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.lvPatient.Items.Remove(selected);
            this.RefreshRegPatient();
        }
        private void CancelTriageUnsee()
        { 
            DateTime endDate = this.assginMgr.GetDateTimeFromSysDateTime();
            DateTime beginDate =endDate.Date;
            string noonID = Nurse.Function.GetNoon(endDate);
            ArrayList al = new ArrayList();
           al =  this.assginMgr.QueryUnInSee( beginDate.ToString(),endDate.ToString(),((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,noonID);
           if (al != null && al.Count > 0)
           {
               string error = "";
               foreach (FS.HISFC.Models.Nurse.Assign assign in al)
               {
                   FS.FrameWork.Management.PublicTrans.BeginTransaction();

                   //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                   //t.BeginTransaction();

                   if (Function.CancelTriage(assign, ref error) == -1)
                   {
                       FS.FrameWork.Management.PublicTrans.RollBack();
                       MessageBox.Show(error, "��ʾ");
                       return;
                   }

                   FS.FrameWork.Management.PublicTrans.Commit();

                   this.RefreshRegPatient();
 
               }
           }
        }

        /// <summary>
        /// �˵�����
        /// </summary>
        private void LoadMenuSet()
        {
            try
            {
                //XmlDocument doc = new XmlDocument();
                //doc.Load(Application.StartupPath + "/Setting/feeSetting.xml");
                //XmlNode node = doc.SelectSingleNode("//����ˢ��Ƶ��");

                //if (node != null)
                //    frequence = node.Attributes["currentSet"].Value;

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
                #endregion

                if (!isFresh)
                {
                    this.SetRadioFalse();
                    this.rbNo.Checked = true;
                }

                //this.SetFreq(frequence);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ʾ");
            }
        }

        private void Screen()
        {
            string screenSize = this.controlMgr.QueryControlerInfo("900004");
            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            ToolStripButton tsb = this.toolBarService.GetToolButton("���ԣ�����");
            if (tsb == null)
            {
                ToolStripButton tsbtmp = this.toolBarService.GetToolButton("���ԣ��أ�");

                if (tsbtmp == null)
                {
                    return;
                }
                else
                {
                    tsbtmp.Text = "���ԣ�����";
                }
            }
            else
            {
                tsb.Text = "���ԣ��أ�";
            }
            
            //�����������
            if (!IsShowScreen)
            {
                f = new frmDisplay();
                f.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) + 1, 0);
                f.Show();
                IsShowScreen = true;
            }
            else
            {
                if (f != null)
                {
                    f.Close();
                }
                IsShowScreen = false;
            }
            #endregion
        }

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

            //ucInRoom f = new ucInRoom(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            ucInRoom f = new ucInRoom(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            f.Assign = assigninfo;
            f.OK += new ucInRoom.MyDelegate(In_OK);
            FS.FrameWork.WinForms.Classes.Function.ShowControl(f);
            //			f.OK +=new Nurse.frmInRoom.MyDelegate(In_OK);
            //if (f.DialogResult == DialogResult.OK)
            //{
            //    this.In_OK(assigninfo);
            //}

            //f.Dispose();
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

            //�ָ�����״̬

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();

            try
            {
                this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.Nurse.Assign info = node.Tag as FS.HISFC.Models.Nurse.Assign;
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
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ���б�
        /// </summary>
        private void Init()
        {
            this.RefreshQueue();

            this.RefreshRoom();

            #region {4B7F932C-1C2A-4b65-B6F4-C2C1EFA19B12}
            //this.LoadMenuSet();
            #endregion
            this.neuTreeView1.ImageList = this.neuTreeView1.deptImageList;
            this.neuTreeView2.ImageList = this.neuTreeView2.groupImageList;
            this.isNurseDept = controlIntegrate.GetControlParam<bool>("900010", true, false);//�Ƿ���ڿ��Ҳ�����Ӧ��ϵ{F044FCF3-6736-4aaa-AA04-4088BB194C20}
            if (this.frequence != "Auto") this.RefreshRegPatient();
            if (this.frequence == "Auto")
            {
                this.Auto();
            }
            isAccountTermianFee = controlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, true, false);
            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            //��ȡ�������
            TrigeWhereFlag = controlIntegrate.GetControlParam<string>("900001", true, "0");
            
            isTrigeExpert = controlIntegrate.GetControlParam<bool>("900003", true, false);

            #endregion
        }
        /// <summary>
        /// ���ɹҺŻ����б�
        /// </summary>
        /// <returns></returns>
        private int RefreshRegPatient()
        {
            this.CancelTriageUnsee();
            DateTime current = this.assginMgr.GetDateTimeFromSysDateTime().Date;// this.regMgr.GetDateTimeFromSysDateTime().Date;
            string myNurseDept = "";

            //myNurseDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;  by niuxinyuan

            myNurseDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            //���˿���
            this.neuTreeView1.Nodes.Clear();
            TreeNode root = new TreeNode("�����ﻼ��");
            this.neuTreeView1.Nodes.Add(root);
            if (this.isNurseDept)//{F044FCF3-6736-4aaa-AA04-4088BB194C20}�Ƿ���ڿ��Ҳ�����Ӧ��ϵ
            {
                this.alCollections = this.regMgr.QueryNoTriagebyDept(current, myNurseDept);
            }
            else
            {
                this.alCollections = this.regMgr.QueryNoTriagebyNurse(current, myNurseDept);
            }
            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Registration.Register obj in this.alCollections)
                {
                    //����Ѿ����������
                    bool blTriage = this.JudgeTrige(obj.ID);
                    if (blTriage == true)
                    {
                        continue;
                    }
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

            FS.HISFC.BizLogic.Nurse.Room roomMgr =
                new FS.HISFC.BizLogic.Nurse.Room();

            //this.alCollections = roomMgr.GetRoomInfoByNurseNo(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            this.alCollections = roomMgr.GetRoomInfoByNurseNo(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);

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

                            node2.Text = seat.Name + "[" + this.GetDoct(seat.ID) + "]";//------------------------------
                            #region  {E1FBDE66-8059-496f-B631-F5815788EB47}
                            //node2.ImageIndex = 18;
                            //node2.SelectedImageIndex = 18;
                            node.ImageIndex = 2;
                            node.SelectedImageIndex = 3; 
                            #endregion
                            node2.Tag = seat;
                            node.Nodes.Add(node2);
                            //��ӵ���̨���飬�����Զ�����ʱ���á�
                            this.alSeats.Add(seat);
                        }
                    }//end foreach
                }//end if
                this.AddPatientByConsole();

                #region ���ø����ҵ�����(��������)
                //				foreach(TreeNode node1 in this.treeView2.Nodes[0].Nodes)
                //				{
                //					foreach(TreeNode node2 in node1.Nodes)
                //					{
                //						int count = node2.Nodes.Count;
                //						node2.Text = node2.Text + "(" + count.ToString() + ")";
                //					}
                //				}
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
        private string GetDoct(string seatID)
        {
            string strDoct = "";
            for (int i = 0; i < this.lvQueue.Items.Count; i++)
            {
                FS.HISFC.Models.Nurse.Queue info = this.lvQueue.Items[i].Tag
                                                            as FS.HISFC.Models.Nurse.Queue;
                //�ҵ�����������
                if (info.Console.ID == seatID)
                {
                    //��������ά����ҽ��
                    if (info.Doctor.ID == null || info.Doctor.ID == "")
                    {
                        continue;
                    }
                    //FS.HISFC.BizLogic.Manager.Person psMgr = new FS.HISFC.BizLogic.Manager.Person();
                    //FS.HISFC.Models.RADT.Person ps = new FS.HISFC.Models.RADT.Person();
                    FS.HISFC.BizProcess.Integrate.Manager psMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    FS.HISFC.Models.Base.Employee ps = new FS.HISFC.Models.Base.Employee();
                    ps = psMgr.GetEmployeeInfo(info.Doctor.ID);
                    if (ps.Name != null || ps.Name != "")
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
            //this.alCollections = this.assginMgr.Query(
            //    ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,
            //    this.assginMgr.GetDateTimeFromSysDateTime().Date,
            //    FS.HISFC.Models.Nurse.EnuTriageStatus.In
            //    );
            this.alCollections = this.assginMgr.QueryUnionRegister(
                //((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,
                ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,
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
                            child.Text = assign.SeeNO.ToString() + "[" + assign.Register.Name + "]"+"[" + assign.Register.DoctorInfo.Templet.RegLevel.Name + "]" + "[" + assign.Register.DoctorInfo.Templet.Doct.Name + "]" ;
                        }
                        else
                        {
                            child.Text =  assign.SeeNO.ToString() + "[" + assign.Register.Name + "]"+"[" + assign.Register.DoctorInfo.Templet.RegLevel.Name + "]" ;
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
            string noonID = Nurse.Function.GetNoon(current);//���
            if (noonID == "")
            {
                MessageBox.Show("��ǰʱ����û�ж�Ӧ�����!", "��ʾ");
                return;
            }
            this.RefreshQueue(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, current, noonID);

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

            //this.alCollections = queueMgr.Query(nurseID, queueDate, noonID);
            this.alCollections = queueMgr.Query(nurseID, queueDate.Date/*.ToShortDateString()*/,noonID);

            if (this.alCollections != null)
            {
                foreach (FS.HISFC.Models.Nurse.Queue obj in this.alCollections)
                {
                    if (!obj.IsValid) continue;

                    ListViewItem item = new ListViewItem();

                    #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
                    item.SubItems.Clear();

                    item.SubItems.Add(obj.AssignDept.Name);
                    item.SubItems.Add(this.doctHelper.GetName(obj.Doctor.ID));
                    item.SubItems.Add(obj.Doctor.ID);
                    item.SubItems.Add(obj.SRoom.Name);
                    item.SubItems.Add(obj.WaitingCount.ToString());
                    #endregion

                    item.Text = obj.Name;
                    if (obj.Memo != null && obj.Memo != "")
                        item.Text = item.Text + "\n" + "[" + obj.AssignDept.Name + "]";
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

                    this.lvQueue.Items.Add(item);
                }
            }
            return 0;
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
                assign.Queue = info;
                #region ���������ʾ
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

                //�Һſ�������п��Ҳ�����
                if (assign.Queue.AssignDept.ID != assign.Register.DoctorInfo.Templet.Dept.ID
                    && assign.Queue.AssignDept.ID != "" && assign.Queue.AssignDept.ID != null)
                {
                    if (MessageBox.Show("���߹Һſ���[" + assign.Register.DoctorInfo.Templet.Dept.Name + "]����еĿ������["
                        + assign.Queue.AssignDept.Name + "]����!" + "\n" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                //��ͨ�Ž���ר�ҡ�
                if ((assign.Register.DoctorInfo.Templet.Doct.ID == "" || assign.Register.DoctorInfo.Templet.Doct.ID == null)
                    && assign.Queue.ExpertFlag == "1")
                {
                    if (MessageBox.Show("��ͨ�ҺŽ���ר�Ҷ���" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                //ר�ҺŽ�����ͨ��
                if (assign.Register.DoctorInfo.Templet.Doct.ID != "" && assign.Register.DoctorInfo.Templet.Doct.ID != null
                    && assign.Queue.ExpertFlag == "0")
                {
                    if (MessageBox.Show("ר�ҹҺŽ�����ͨ����" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                //ר�ҺŽ�����ͨ��
                if (assign.Register.DoctorInfo.Templet.Doct.ID != "" && assign.Register.DoctorInfo.Templet.Doct.ID != null
                    && assign.Queue.Doctor.ID != assign.Register.DoctorInfo.Templet.Doct.ID)
                {
                    if (MessageBox.Show("ѡ��ҽ����Һ�ҽ����һ��" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                #endregion
                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                //assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;// var.User.Nurse.ID;
                assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                assign.TirageTime = this.assginMgr.GetDateTimeFromSysDateTime(); //this.assginMgr.GetDateTimeFromSysDateTime().Date;// this.regMgr.GetDateTimeFromSysDateTime();
                assign.Oper.OperTime = assign.TirageTime;
                assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID; //var.User.ID;
                assign.Queue.Dept = info.AssignDept;

                #endregion

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

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

                //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID/*var.User.Nurse.ID*/, 
                //                  this.assginMgr.GetDateTimeFromSysDateTime().Date /*this.regMgr.GetDateTimeFromSysDateTime().Date*/,
                //                  (item.Tag as FS.HISFC.Models.Nurse.Queue).ID);
                this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID/*var.User.Nurse.ID*/,
                                 this.assginMgr.GetDateTimeFromSysDateTime().Date /*this.regMgr.GetDateTimeFromSysDateTime().Date*/,
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

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

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
            bool bl = false;
            bl = this.regMgr.QueryIsTriage(ClinicCode);
            return bl;
        }

        /// <summary>
        /// �ж��Ƿ��˺�
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        private bool Judgeback(string ClinicCode)
        {
            //��FIN_OPB_REGISTER��,����ClinicCodeȡ������,���valid_flag = 1 ����false,���򷵻�true
            bool bl = false;
            bl = this.regMgr.QueryIsCancel(ClinicCode);
            return bl;
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
            //�ҳ�������С�����Ķ���for your 

            ArrayList alMin = new ArrayList();
            foreach (FS.HISFC.Models.Nurse.Queue info in al)
            {
                if (info.ExpertFlag == "1") continue;

                #region �Ҷ���ҿ���
                //				if(regDept != "ALL")
                //				{
                //					//�����ҽ������Ķ����������Ҳ�Ҳ���...continue
                //					ArrayList alDept = new ArrayList();
                //					FS.HISFC.BizLogic.Manager.DepartmentStatManager deptMgr = 
                //						new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                //					alDept = deptMgr.GetMultiDept(info.Doctor.ID);
                //					bool IsHas = false;
                //
                //					foreach(FS.HISFC.Models.Base.DepartmentStat deptinfo in alDept)
                //					{
                //						if(deptinfo.ID == regDept )
                //						{
                //							IsHas = true;
                //							break;
                //						}
                //					}
                //					if(!IsHas) continue;
                //				}
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

        #endregion

        #region ������ר���Զ�����
        /// <summary>
        /// ר���Զ�����(ʹ��֮ǰ��Ҫˢ��һ�»��ߺͶ���)------��Ϊ������ģ��ʹ�ã����ܸ������Զ������غ�
        /// </summary>
        private void AutoExpert()
        {
            #region	ר���Ƿ��Զ�����{DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            //string TrigeExpertFlag = this.controlMgr.QueryControlerInfo("900003");
            //if (TrigeExpertFlag == null || TrigeExpertFlag == "")
            //{
            //    TrigeExpertFlag = "0";
            //}
            if (!isTrigeExpert)
            {
                return;
            }
            #endregion

            #region ��ȡ������Ϣ
            //��ȡʱ��
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime().Date; //this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Nurse.Function.GetNoon(currenttime);

            //��Ҫ����Ļ�����Ϣ
            //this.alCollections = this.regMgr.QueryNoTriagebyDept(current, FS.FrameWork.Management.Connection.Operator.ID);
            this.alCollections = this.regMgr.QueryNoTriagebyDept(current, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            if (this.alCollections == null) return;

            //����Ķ���
            ArrayList al = new ArrayList();
            al = queueMgr.Query(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, current, noonID);
            if (al == null || al.Count <= 0) return;
            #endregion

            #region �Զ����ﵽ
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
                            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
                            if (TrigeWhereFlag == "0")
                            {
                                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                            }
                            else
                            {
                                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                            }
                            #endregion
                            //assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;
                            assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
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

                        //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                        //SQLCA.BeginTransaction();
                        string error = "";

                        if (Function.Triage( assign, "2", ref error) == -1)
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
        /// �Զ�����������
        /// </summary>
        private void AutoRefresh()
        {
            #region �Ȱѽ����ϵĻ���׼��׼����!
            //��ֹ�س��Ҳ���
            this.neuTreeView1.Nodes.Clear();
            TreeNode root = new TreeNode("�����ﻼ��", 40, 40);
            this.neuTreeView1.Nodes.Add(root);

            this.RefreshRoom();
            this.RefreshQueue();

            if (this.lvQueue.Items.Count <= 0)
            {
                this.RefreshRegPatient();
                return;
            }

            #region ��ȡ��Ϣ
            //��ȡ�������
            TrigeWhereFlag = this.controlMgr.QueryControlerInfo("900001");
            if (TrigeWhereFlag == null || TrigeWhereFlag == "")
            {
                TrigeWhereFlag = "0";
            }

            //�Ƿ��Ѿ�ȫ������
            bool IsAll = true;

            //��ȡʱ��
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Nurse.Function.GetNoon(currenttime);

            //��Ҫ����Ļ�����Ϣ
            this.alCollections = this.regMgr.QueryNoTriagebyDept(current, FS.FrameWork.Management.Connection.Operator.ID);
            if (this.alCollections == null) return;

            //����Ķ���
            ArrayList al = new ArrayList();
            al = queueMgr.Query(FS.FrameWork.Management.Connection.Operator.ID, current, noonID);
            if (al == null || al.Count <= 0) return;
            #endregion
            #endregion

            #region �Զ����ﵽ����
            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);

            foreach (FS.HISFC.Models.Registration.Register reg in this.alCollections)
            {
                //��ֹ����
                if (this.JudgeTrige(reg.ID)) continue;

                FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();

                #region 1.���ڷ�ר�ҹҺ�,�ҳ���ǰ�����������ٵĶ��в���
                if (reg.DoctorInfo.Templet.Doct.ID == null || reg.DoctorInfo.Templet.Doct.ID == "")
                {
                    //��ȡ���������Ķ���
                    FS.HISFC.Models.Nurse.Queue info = new FS.HISFC.Models.Nurse.Queue();
                    info = this.GetMinQueue(al, reg.DoctorInfo.Templet.Doct.ID);
                    if (info == null)
                    {
                        IsAll = false;
                        continue;
                    }
                    //ʵ��ת��
                    assign = this.TransEntity(reg, info);

                    //���ݿ����
                    string error = "";
                    //SQLCA.BeginTransaction();

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    if (Function.Triage(assign, "1", ref error) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(error, "��ʾ");
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    //ˢ�´˶�����Ϣ
                    this.RefreshAssign(assign);
                }
                #endregion

                #region 2.����ר�ҹҺ�,�ҵ�ר�Ҷ��з���,���򲻽��в���
                else
                {
                    //ʵ�帳ֵ
                    bool IsFound = false;
                    for (int i = 0; i < this.lvQueue.Items.Count; i++)
                    {
                        assign.Queue = (FS.HISFC.Models.Nurse.Queue)this.lvQueue.Items[i].Tag;
                        if (assign.Queue.ExpertFlag == "1" && assign.Queue.Doctor.ID == reg.DoctorInfo.Templet.Doct.ID)
                        {
                            assign = this.TransEntity(reg, assign.Queue);
                            IsFound = true;
                            break;
                        }
                    }

                    //�������ݿ�
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
                    else
                    {
                        IsAll = false;
                    }

                    this.RefreshAssign(assign);
                }
                #endregion
            }
            #endregion

            #region �Զ����ﵽ��̨
            if (TrigeWhereFlag == "1")
            {
                this.AutoConsole();
            }
            #endregion

            #region ����ˢ��һ�½���
            //û��ȫ�����з���
            if (!IsAll)
            {
                this.RefreshRegPatient();
            }
            this.neuTextBox1.Focus();
            this.RefreshRoom();
            #endregion
        }
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
            //assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;
            assign.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            assign.TirageTime = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            assign.Oper.OperTime = assign.TirageTime;
            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            assign.Queue.Dept = assign.Register.DoctorInfo.Templet.Doct;
            assign.InTime = this.assginMgr.GetDateTimeFromSysDateTime();

            assign.Queue = queue;

            return assign;
        }

        /// <summary>
        /// �Զ����ﵽ��̨
        /// </summary>
        private void AutoConsole()
        {
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);

            //�ҳ���������еĴ�����Ϣ
            ArrayList alAssign = new ArrayList();
            alAssign = this.assginMgr.Query(FS.FrameWork.Management.Connection.Operator.ID, System.DateTime.Now.Date,
                FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
            if (alAssign == null || alAssign.Count <= 0) return;

            //�Զ����еĴ�����Ϣ     1.���ά������̨�����Զ����
            //						 2.û��ά����̨�ģ����ٽ��в�����
            foreach (FS.HISFC.Models.Nurse.Assign info in alAssign)
            {
                if (info.Queue.Console.ID == null || info.Queue.Console.ID == "") continue;

                //����������1.��̨��treeview2���ҵ�   2.��̨�е�countΪ0   
                //			3.������ݹҺ�˳�򣬲�û�и������кš�����:�ֶ��ı���˳��Ų������ã�
                bool IsFoundConsole = false;
                foreach (FS.HISFC.Models.Nurse.Seat console in this.alSeats)
                {
                    //������̨�ҵ���Ӧ����,Ȼ��ȡ�������ڵ�����
                    if (console.ID == info.Queue.Console.ID)
                    {
                        int n = this.assginMgr.QueryConsoleNum(console.ID, current,
                            current.AddDays(1), FS.HISFC.Models.Nurse.EnuTriageStatus.In);
                        if (n >= 0 && n <
                            FS.FrameWork.Function.NConvert.ToInt32(this.TrigeWhereFlag))
                        {
                            this.alSeats.Remove(console);
                            IsFoundConsole = true;
                            break;
                        }
                    }
                }

                if (!IsFoundConsole) continue;

                //SQLCA.BeginTransaction();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.assginMgr.Update(info.Register.ID, info.Queue.SRoom, info.Queue.Console,
                    this.assginMgr.GetDateTimeFromSysDateTime()) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�Զ�����Ӷ��е���̨����!", "��ʾ");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
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

            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            ////����һ�·������
            //TrigeWhereFlag = this.controlMgr.QueryControlerInfo("900001");
            //if (TrigeWhereFlag == null || TrigeWhereFlag == "")
            //{
            //    TrigeWhereFlag = "0";
            //} 
            #endregion

            //��ȡʱ��
            DateTime currenttime = this.assginMgr.GetDateTimeFromSysDateTime();// this.regMgr.GetDateTimeFromSysDateTime();
            DateTime current = currenttime.Date;
            string noonID = Nurse.Function.GetNoon(currenttime);

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
            //this.alCollections = this.regMgr.QueryNoTriagebyDept(current, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            this.alCollections = this.regMgr.QueryNoTriagebyDept(current, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            if (this.alCollections == null || this.alCollections.Count <= 0) return;

            //��ȡ�������
            ArrayList alQueue = new ArrayList();
            //alQueue = queueMgr.Query(
            //    ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,
            //    current, 
            //    noonID);
            alQueue = queueMgr.Query(
                ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,
                current,
                noonID);

            if (alQueue == null || alQueue.Count <= 0) return;
            foreach (FS.HISFC.Models.Nurse.Queue queue in alQueue)
            {
                //��������ֶ�
                queue.WaitingCount = this.assginMgr.QueryConsoleNum(queue.Console.ID, current,
                            current.AddDays(1), FS.HISFC.Models.Nurse.EnuTriageStatus.In);
            }

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance/*var.con*/);
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
                        #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
                        //���ݷ����������
                        if (this.TrigeWhereFlag == "0")
                        {
                            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                        }
                        else
                        {
                            assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                        }
                        #endregion
                        IsFound = true;
                        break;
                    }
                }

                //�ҵ�����
                if (IsFound)
                {
                    //SQLCA.BeginTransaction();

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    string error = "";

                    if (Function.Triage( assign, "1", ref error) == -1)
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
                #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
                if (this.TrigeWhereFlag == "0")
                {
                    assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                }
                else
                {
                    assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                }
                #endregion

                //���ݿ����
                string error = "";
                //SQLCA.BeginTransaction();

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

        #region �к�

        /// <summary>
        /// �����к�
        ///��ǰ��ʿվ��Ӧ���Ұ����к�{63F980B1-1F3E-4a55-A7E3-34B1640772DD}
        /// </summary>
        private void CalledPatient()
        {
            foreach (ListViewItem viewItem in lvQueue.Items)
            {
                if (viewItem.Tag.GetType() == typeof(FS.HISFC.Models.Nurse.Queue))
                {
                    string queueID = (viewItem.Tag as FS.HISFC.Models.Nurse.Queue).ID;
                    //��ѯ�������Ļ���
                    FS.HISFC.Models.Nurse.Assign assignObj = this.assginMgr.QueryLastAssignPatient(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, this.assginMgr.GetDateTimeFromSysDateTime().Date, queueID, "2");
                    if (assignObj != null)
                    {
                        //this.controlMgr.Speak("��" + assignObj.Register.Name + "��" + assignObj.Queue.Console.Name + "����");
                        //this.controlMgr.Speak("��" + assignObj.Register.Name + "��" + assignObj.Queue.Console.Name + "����");
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        if (this.assginMgr.UpdateByCalled(queueID, assignObj.Register.ID, this.assginMgr.GetDateTimeFromSysDateTime(), "") == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���·���״̬ʧ�ܣ�" + this.assginMgr.Err);
                            return;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
            }
        }

        #endregion

        #region �¼��������

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.frequence != "Auto")
            {
                this.RefreshRegPatient();
                this.RefreshRoom();
                this.AutoExpert();
                //{63F980B1-1F3E-4a55-A7E3-34B1640772DD}
                this.CalledPatient();
            }
            else
            {
                this.Auto();
            }
        }

        private void lvQueue_Click(object sender, EventArgs e)
        {
            if (this.lvQueue.SelectedItems == null ||
                this.lvQueue.SelectedItems.Count == 0) return;

            ListViewItem selected = this.lvQueue.SelectedItems[0];

            if (selected.Tag.GetType() ==
                typeof(FS.HISFC.Models.Nurse.Queue))
            {
                //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID/*var.User.Nurse.ID*/, 
                //                  this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                //                  (selected.Tag as FS.HISFC.Models.Nurse.Queue).ID);
                this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID/*var.User.Nurse.ID*/,
                                 this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                                 (selected.Tag as FS.HISFC.Models.Nurse.Queue).ID);
            }
            else
            {
                //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,/*var.User.Nurse.ID*/
                //                  this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                //                  (selected.Tag as FS.HISFC.Models.Registration.DoctSchema).ID);
                this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,/*var.User.Nurse.ID*/
                                 this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                                 (selected.Tag as FS.HISFC.Models.Registration.DoctSchema).ID);
            }
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
                            //							"����:" + age.ToString() + "---" + "\n" +
                            "�Һ�ʱ��:" + info.Register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm");
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private void neuLabelTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;

            string cardNo = this.neuTextBox1.Text.ToString().Trim().PadLeft(10, '0');

            //1.���ұ�MET_NUO_ASSIGNRECORD
            if (this.QueryAssignRecord(cardNo) == 0) return;

            //2.���ҹҺű�FIN_OPB_REGISTER
            this.QueryReg(cardNo);
            /*---------------------------------------------------------------------------------*
             * �Ե���,ͬһ���߹���ĳһ���������ŵĴ���-->����һ���б�����Թ�ѡ��.             *
             *		1.ȡ���߽������Ч������Ϣ. ��tag = 1									   *
             *		  ȡ���߽������Ч�Һ���Ϣ(�����־Ϊ0��). ��tag = 2                       *
             *		2.������� = 1 ,ֱ�ӵ�����Ӧ����										   *
             *		3.˫��,�ж�. tag=1 �򵯳�������� 2 �򵯳��������                         *
             *---------------------------------------------------------------------------------*/
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
            item.SubItems.Add(assign.Register.Name);
            item.SubItems.Add(assign.Register.Sex.Name);
            #region {51AB60B7-8610-40c4-B5BC-CD978E788892}
            if (string.IsNullOrEmpty(assign.Register.Age))
            {
                FS.HISFC.Models.Registration.Register regObj = this.regMgr.GetByClinic(assign.Register.ID);
                item.SubItems.Add(this.assginMgr.GetAge(regObj.Birthday));
            }
            else
            {
                item.SubItems.Add(assign.Register.Age);
            }
            #endregion
            item.SubItems.Add(assign.SeeNO.ToString());
            item.SubItems.Add(assign.TirageTime.TimeOfDay.ToString());

            this.lvPatient.Items.Add(item);
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

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

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

            //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,/*var.User.Nurse.ID, */
            //                  this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date*/ 
            //                  info.Queue.ID);
            this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,/*var.User.Nurse.ID, */
                            this.assginMgr.GetDateTimeFromSysDateTime().Date, /*this.regMgr.GetDateTimeFromSysDateTime().Date*/
                            info.Queue.ID);
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
            if (this.lvPatient.SelectedItems == null ||
                this.lvPatient.SelectedItems.Count == 0) return;
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

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

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

            //this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID,/*var.User.Nurse.ID, */
            //                  this.assginMgr.GetDateTimeFromSysDateTime().Date,/* this.regMgr.GetDateTimeFromSysDateTime().Date,*/
            //                  info.Queue.ID);
            this.RetrieveWait(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID,/*var.User.Nurse.ID, */
                              this.assginMgr.GetDateTimeFromSysDateTime().Date,/* this.regMgr.GetDateTimeFromSysDateTime().Date,*/
                              info.Queue.ID);
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
            //			//�û��߽���û�йҺ���Ϣ
            //			if(reginfo.RegDate.ToString("yyyy-MM-dd") != this.assginMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd"))
            //			{
            //				MessageBox.Show("�û��߽���û�йҺ�!","��ʾ");
            //				this.textBox1.Focus();
            //				this.textBox1.SelectAll();
            //				return -1 ;
            //			}
            //�û��߽����Ѿ��˺�
            //			if(this.Judgeback(reginfo.ID))
            //			{
            //				MessageBox.Show("�û������һ�ιҺ���Ϣ�Ѿ��˺�!\n���ȷ�����߻�����Ч�Һ���Ϣ,��ˢ����Ļ!������Ļ�н��з���!","��ʾ");
            //				return -1 ;
            //			}
            //��ֹ����
            if (this.JudgeTrige(reginfo.ID))
            {
                MessageBox.Show("�û����Ѿ�����!", "��ʾ");//\n���ȷ�����߻�����Ч�Һ���Ϣ,��ˢ����Ļ!������Ļ�н��з���!
                return -1;
            }
            //�û��߲����ڵ�ǰ����վ�������� �������ƴ���
            if (this.assginMgr.QueryNurseByDept(reginfo.DoctorInfo.Templet.Dept.ID) != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID/* var.User.Nurse.ID*/)
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

            ArrayList altemp = this.controlMgr.QueryNurseStationByDept(obj,"14");

            if (altemp.Count != 0)
            {   
                //by niuxinyan
                //if ((altemp[0] as FS.FrameWork.Models.NeuObject).ID == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
                //{
                //    MessageBox.Show("û�иĻ�����Ϣ��\n1 û�йҺŻ��Ǳ�����վ����\n2 �û����Ѿ�������\n3 �û����Ѿ�����");
                //    return -2;
                //}
                if ((altemp[0] as FS.FrameWork.Models.NeuObject).ID == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
                {
                    MessageBox.Show("û�иû�����Ϣ��\n1 û�йҺŻ��Ǳ�����վ����\n2 �û����Ѿ�������\n3 �û����Ѿ�����");
                    return -2;
                }
            }
            

            //if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
            {
                //���Ѿ��������Ϣ,������
                if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                {
                    ucInRoom f = new ucInRoom(FS.FrameWork.Management.Connection.Operator.ID);
                    f.Assign = assign;
                    f.OK +=new ucInRoom.MyDelegate(In_OK);
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(f);

                    /*
                    frmInRoom f = new frmInRoom(var.User.Nurse.ID, var);
                    f.Assign = assign;

                    f.OK += new Nurse.frmInRoom.MyDelegate(In_OK);
                    f.ShowDialog();

                    f.Dispose();
                    */
                    return 0;
                }
                return -1;
            }

            return -1;

        }
        private void uc_OnSelect(FS.HISFC.Models.Registration.Register res)
        {
            this.IsCancel = false;
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
            //ArrayList al = this.regMgr.Query(cardNo, dtvalid);
            ArrayList al = new ArrayList();
            al = this.regMgr.QueryUnionNurse(cardNo, dtvalid);
            FS.HISFC.Models.Registration.Register reginfo = new FS.HISFC.Models.Registration.Register();


            //����һ���Һ���Ϣ
            if (al.Count > 1)
            {
                Nurse.ucPopPatient uc = new ucPopPatient();
                uc.OnSelect+=new ucPopPatient.SelectHander(uc_OnSelect);
                uc.alAll = al;
                uc.Init();
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                if (IsCancel == true)
                {
                    IsCancel = false;
                    return 2;
                }

                reginfo = uc.retInfo;
            }
            else if (al.Count == 1)//ֻ��һ���Һ���Ϣ
            {
                reginfo = (FS.HISFC.Models.Registration.Register)al[0];
            }
            //û���ҵ�
            else if (al.Count <= 0)
            {
                return -1;
            }

            if (reginfo == null && reginfo.PID.CardNO == null)
            {
                return -1;
            }
            //			//�ж�����
            //			if(this.ValidRegInfo(reginfo) == -1 ) return -1;

            //��ֹ�Զ������ʱ�������ˢ��û����.....
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
                if (obj.ID ==reginfo.ID && obj.PID.CardNO == reginfo.PID.CardNO && obj != null && obj.PID.CardNO != null)
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

                this.pnlQueue.Width = 300;

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

            // [2007/03/13] ����
        }

        /// <summary>
        /// ����RadioButton��
        /// </summary>
        private void SetRadioFalse()
        {
            // ��ˢ��
            this.rbNo.Checked = false;
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

        #endregion

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

        private void txtCard2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;


            DateTime dt = this.assginMgr.GetDateTimeFromSysDateTime().Date;
            string cardNo = this.txtCard2.Text.ToString().Trim().PadLeft(10, '0');
            FS.HISFC.Models.Nurse.Assign assign = this.assginMgr.Query(dt, cardNo);
            bool bl = false;

            //if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID /*var.User.Nurse.ID*/)
              if (assign != null && !this.Judgeback(assign.Register.ID) && assign.TriageDept == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID /*var.User.Nurse.ID*/)
            {
                //��ѯ�Ѿ��������Ϣ
                if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                {
                    bl = true;

                    string temp = "��������:" + assign.Register.Name + "\n"
                        + "-----------------------------" + "\n"
                        + "����:" + assign.Queue.SRoom.Name + "\n"
                        + "��̨:" + assign.Queue.Console.Name + "\n"
                        + "����ҽ��:" + this.GetDoct(assign.Queue.Console.ID) + "\n"
                        + "�Һ�ʱ��:" + assign.Register.DoctorInfo.SeeDate.ToString();
                    MessageBox.Show(temp, assign.Register.PID.CardNO);
                }
            }
            if (!bl)
            {
                MessageBox.Show("û����Ч������Ϣ!");
            }
        }

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
            #region {DB856C47-4C49-4257-AA81-1DCBFF658B1E}
            //if (obj == null)
            //{
            //    MessageBox.Show("��ά���ӿ�FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister��ʵ������!");
            //    return -1;
            //}
            if (obj != null)
            {
                this.iNurseRegiser = obj as FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister;
                iNurseRegiser.OnGetRegister += new FS.HISFC.BizProcess.Interface.Registration.GetRegisterHander(iNurseRegiser_OnGetRegister);
            }
            #endregion
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

        #region addby xuewj 2010-11-4 {FCEC42B4-DF78-45c2-8D1A-EDAB94AA56DD} ����ʱ�޸Ļ��߻�����Ϣ
        private void neuTreeView1_MouseDown(object sender, MouseEventArgs e)
        {
            // �����Ҽ��˵�  by zlw 2006-5-1
            System.Windows.Forms.ContextMenu cmPatientPro = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem miPatientPro = new System.Windows.Forms.MenuItem();

            cmPatientPro.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { miPatientPro });

            miPatientPro.Text = "�޸ĹҺ���Ϣ";
            this.neuTreeView1.ContextMenu = cmPatientPro;

            miPatientPro.Click += new System.EventHandler(this.miPatientPro_Click);
        }

        private void miPatientPro_Click(object sender, System.EventArgs e)
        {
            FS.HISFC.Models.Registration.Register findPatient = null;
            System.Windows.Forms.TreeNode node = this.neuTreeView1.SelectedNode;
            if (node == null) return;
            findPatient = node.Tag as FS.HISFC.Models.Registration.Register;
            if (findPatient == null)
            {
                return;
            }
            else
            {
                if (this.regMgr.QueryIsCancel(findPatient.ID) == true)
                {
                    MessageBox.Show("�Һ���Ϣ�����ϣ���ˢ�½���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.regMgr.QueryIsTriage(findPatient.ID) == true)
                {
                    MessageBox.Show("�û����ѷ����ˢ�½���!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ucUpdateRegInfo ucUpdate = new ucUpdateRegInfo();
                ucUpdate.PatientInfo = findPatient.Clone();
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucUpdate);
                this.RefreshRegPatient();
            }
        } 
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
}