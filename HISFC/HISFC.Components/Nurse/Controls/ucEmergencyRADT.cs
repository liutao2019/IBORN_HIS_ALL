using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Nurse.Controls
{
    /// <summary>
    /// [��������: �������ۻ�ʿվ���������л��ؼ�]<br></br>
    /// [�� �� ��: ��ѩ��]<br></br>
    /// [����ʱ��: 2006-10-25]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucEmergencyRADT : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucEmergencyRADT()
        {
            InitializeComponent();

        }

        #region ����
        protected TreeNode node = null;
        protected FS.HISFC.Models.RADT.PatientInfo patient = null;
        FS.FrameWork.WinForms.Forms.ToolBarService tooBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //try
            //{
            //    tv = sender as TreeView;
            //}
            //catch { }
            this.tooBarService.AddToolButton("ˢ��", "ˢ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
           
            this.neuTabControl1.TabPages.Clear();
            this.neuTabControl1.TabPages.Add(this.tbBedView);//Ĭ����ʾ����
            ucBedListView uc = new ucBedListView();
            uc.ListViewItemChanged += new ListViewItemSelectionChangedEventHandler(uc_ListViewItemChanged);
            uc.Dock = DockStyle.Fill;
            uc.Visible = true;
            FS.FrameWork.WinForms.Forms.IControlable ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
            if (ic != null)
            {
                ic.Init(this.tv, null, null);
                ic.SetValue(patient, this.tv.Nodes[0]);
                ic.RefreshTree += new EventHandler(ic_RefreshTree);
                ic.SendParamToControl += new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo += new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);
            
            }
            this.tbBedView.Controls.Add(uc);
            base.OnInit(sender, neuObject, param);
            return tooBarService;
           
        }
        /// <summary>
        /// ��û���
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            string txtNode = "";

            //����ѡ�еĽڵ��κ����Ͳ�ͬ,��ʾ��ͬ������
            if (e.Parent == null)
            {
                //һ���ڵ�
                txtNode = e.Tag.ToString();
                //��ʾ��������һ����
                type = EnumPatientType.Dept;
            }
            else
            {
                //����(����)�ڵ�
                txtNode = e.Parent.Tag.ToString();

                //���ݽڵ����͵Ĳ�ͬ,��ʾ��ͬ������
                if (txtNode == EnumPatientType.In.ToString())
                {
                    type = EnumPatientType.In;
                }
                else if (txtNode == EnumPatientType.Arrive.ToString())
                {
                    type = EnumPatientType.Arrive;
                }else if (txtNode == EnumPatientType.ShiftIn.ToString())
                {
                    type = EnumPatientType.ShiftIn;
                }else if (txtNode == EnumPatientType.ShiftOut.ToString())
                {
                    type = EnumPatientType.ShiftOut;
                }
                else if (txtNode == EnumPatientType.Out.ToString())
                {
                    type = EnumPatientType.Out;
                }
                //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
                else if (txtNode == EnumPatientType.PreOut.ToString())
                {
                    type = EnumPatientType.PreOut;
                }
                else if (txtNode == EnumPatientType.PreIn.ToString())
                {
                    type = EnumPatientType.PreIn;
                }
                else
                {
                    type = EnumPatientType.In;
                }
                node = e;
                patient = e.Tag as FS.HISFC.Models.RADT.PatientInfo;
            }

            this.neuTabControl1_SelectedIndexChanged(null, null);
            return base.OnSetValue(neuObject, e);
        }

        private EnumPatientType mytype = EnumPatientType.Dept;
        /// <summary>
        /// ����
        /// </summary>
        protected EnumPatientType type
        {
            get
            {
                return mytype;
            }
            set
            {
                if (mytype == value) return;
                mytype = value;
                try
                {
                    this.neuTabControl1.TabPages.Clear();
                }
                catch { };
                if (mytype == EnumPatientType.Dept)
                {
                    
                    this.neuTabControl1.TabPages.Add(this.tbBedView);
                }
                else if (mytype == EnumPatientType.In)
                {
                    
                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    //this.neuTabControl1.TabPages.Add(this.tpDept);
                   // this.neuTabControl1.TabPages.Add(this.tpChangeDoc);
                   
                    
                }
                else if (mytype == EnumPatientType.Out)
                {
                    
                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpCallBack);
                    
                }
                else if (mytype == EnumPatientType.Arrive)
                {
                    
                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpArrive);
                    
                }
                else if (mytype == EnumPatientType.ShiftIn)
                {

                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpArrive);
                    
                }
                else if (mytype == EnumPatientType.ShiftOut)
                {
                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpCancelDept);
                }
                //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
                else if (mytype == EnumPatientType.PreOut)
                {
                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.tpOut.Text = "���۳�Ժ";
                    this.neuTabControl1.TabPages.Add(this.tpOut);
                }
                else if (mytype == EnumPatientType.PreIn)
                {
                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.tpIn.Text = "����תסԺ";
                    this.neuTabControl1.TabPages.Add(this.tpIn);
                }
            }
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tabControl Selected Changed
            FS.FrameWork.WinForms.Forms.IControlable ic = null;
            if (this.neuTabControl1.SelectedTab == this.tbBedView)//��λһ��
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBedListView uc = new ucBedListView();
                    uc.ListViewItemChanged += new ListViewItemSelectionChangedEventHandler(uc_ListViewItemChanged);
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if(ic!=null)
                    {
                        ic.Init(this.tv,null,null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }
            else if (this.neuTabControl1.SelectedTab == this.tpArrive)//����
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBasePatientArrive uc = new ucBasePatientArrive();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    if (this.node.Parent != null && this.node.Parent.Tag.ToString() == "ShiftIn")
                    {
                        uc.arrivetype = ArriveType.ShiftIn;
                        
                    }
                    else
                    {
                        uc.arrivetype = ArriveType.Regedit;
                        
                    }
                   
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if(ic!=null)
                    {
                        ic.Init(this.tv,null,null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                    
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                    ucBasePatientArrive uc = ic as ucBasePatientArrive;
                    if (this.node.Parent != null && this.node.Parent.Tag.ToString() == "ShiftIn")
                    {
                        uc.arrivetype = ArriveType.ShiftIn;

                    }
                    else
                    {
                        uc.arrivetype = ArriveType.Regedit;

                    }
                    
                }
                
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCallBack)//�һ�
            {
                 if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBasePatientArrive uc = new ucBasePatientArrive();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.arrivetype = ArriveType.CallBack;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if(ic!=null)
                    {
                        ic.Init(this.tv,null,null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCancelDept)//ȡ��ת��
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftOut uc = new ucPatientShiftOut();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.IsCancel = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }
            else if (this.neuTabControl1.SelectedTab == this.tpChangeDoc)//��ҽ��
            {
                 if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBasePatientArrive uc = new ucBasePatientArrive();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.arrivetype = ArriveType.ChangeDoc;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if(ic!=null)
                    {
                        ic.Init(this.tv,null,null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }
            else if (this.neuTabControl1.SelectedTab == this.tpDept)//������
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftOut uc = new ucPatientShiftOut();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.IsCancel = false;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }
           
                 
            else if (this.neuTabControl1.SelectedTab == this.tpOut)//��Ժ�Ǽ�
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientOut uc = new ucPatientOut();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }
            //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
            else if (this.neuTabControl1.SelectedTab == this.tpIn) //תסԺ
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientIn uc = new ucPatientIn();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }
            else if (this.neuTabControl1.SelectedTab == this.tpPatient)//���߻�����Ϣ
            {

                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientInfo uc = new ucPatientInfo();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }
            else
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    return;
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }

            if (ic != null)
            {
                ic.SetValue(patient, node);
                ic.RefreshTree -= new EventHandler(ic_RefreshTree);
                ic.SendParamToControl -= new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo -= new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

                ic.RefreshTree += new EventHandler(ic_RefreshTree);
                ic.SendParamToControl += new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo += new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);
            
            }
        }

        void uc_ListViewItemChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            myPatientInfo = e.Item.Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (myPatientInfo != null)
            {
                string strBedInfo = myPatientInfo.PVisit.PatientLocation.Bed.ID;
                strBedInfo = strBedInfo.Length > 4 ? strBedInfo.Substring(4) : strBedInfo;
                e.Item.ToolTipText = myPatientInfo.Name + "-��" + strBedInfo + "����-" + ((EnumBedState)e.Item.ImageIndex).ToString();
                base.OnStatusBarInfo(sender, myPatientInfo.Name + "-��" + strBedInfo + "����-" + ((EnumBedState)e.Item.ImageIndex).ToString());
            }
            else
            {
                base.OnStatusBarInfo(sender,((EnumBedState)e.Item.ImageIndex).ToString());
            }
        }

        void ic_StatusBarInfo(object sender, string msg)
        {
            this.OnStatusBarInfo(sender, msg);
        }

        void ic_SendParamToControl(object sender, string dllName, string controlName, object objParams)
        {
            this.OnSendParamToControl(sender, dllName, controlName, objParams);
        }

        void ic_SendMessage(object sender, string msg)
        {
            this.OnSendMessage(sender, msg);
        }

        void ic_RefreshTree(object sender, EventArgs e)
        {
            this.OnRefreshTree();
            try
            {
                ucBedListView uc = this.tbBedView.Controls[0] as ucBedListView;
                uc.RefreshView();
            }
            catch { }
        }

        protected override void OnRefreshTree()
        {
           
            ((tvEmergencyPatientList)this.tv).Refresh();
            base.OnRefreshTree();
        }
        #endregion

        #region ���к���
        /// <summary>
        /// ���ŵ�Tabpage
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        /// <param name="tag"></param>
        public void AddTabpage(FS.FrameWork.WinForms.Controls.ucBaseControl control, string title, object tag)
        {
          
            foreach (TabPage tb in this.neuTabControl1.TabPages)
            {
                if (tb.Text == title)
                {
                    this.neuTabControl1.SelectedTab = tb;
                    return;
                }
            }
            TabPage tp = new TabPage(title);
            this.neuTabControl1.TabPages.Add(tp);
           
            control.Dock = DockStyle.Fill;
            control.Visible = true;
          
            FS.FrameWork.WinForms.Forms.IControlable ic = control as FS.FrameWork.WinForms.Forms.IControlable;
            if (ic != null)
            {
                ic.Init(this.tv, null, null);
            }
            tp.Controls.Add(control);
            if (ic != null)
                ic.SetValue(patient, node);
            this.neuTabControl1.SelectedTab = tp;
        }
        #region �˵���
       
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ˢ��":
                    {
                        ((tvEmergencyPatientList)this.tv).Refresh();

                        break;
                    }
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion
        #endregion
        public enum EnumBedState
        {
            �մ� = 0,
            �� = 1,
            Ů = 2,
            �ر� = 3,
            �������� = 4,
            �������� = 5,
            һ������ = 6,
            ��Σ = 7,
            ��֢ = 8,
            ���� = 9,
            �ż� = 10,
            �Ҵ� = 11,
            �� = 12,
            û�� = 13
        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        public enum EnumPatientType
        {
            In = 0,//��Ժ����
            Arrive = 1,//�����ﻼ��
            Out = 2,//��Ժ�Ǽǻ���
            ShiftIn = 3,//ת�뻼��
            ShiftOut = 4,//ת������
            Dept = 5, //�����б�
            PreOut = 6,//���صǼ�
            PreIn = 7,//����תסԺ
        }
    }
}
