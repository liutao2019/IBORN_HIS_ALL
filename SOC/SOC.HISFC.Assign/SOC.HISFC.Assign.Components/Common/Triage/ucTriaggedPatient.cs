using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Components.Common.Triage
{
    /// <summary>
    /// [功能描述: 门诊已分诊患者信息显示]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    internal partial class ucTriaggedPatient : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient
    {
        public ucTriaggedPatient()
        {
            InitializeComponent();
        }

        #region 域变量

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> selectedAssignChange;

        private FS.HISFC.Models.Nurse.EnuTriageStatus triageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.None;

        private FS.SOC.HISFC.Assign.Models.Queue queue = null;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> dragDropAssign;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> dragDropRegister;

        #endregion

        #region 初始化

        private int initEvents()
        {
            this.tvTriageWaiting.AfterSelect -= new TreeViewEventHandler(tv_AfterSelect);
            this.tvTriageWaiting.AfterSelect += new TreeViewEventHandler(tv_AfterSelect);

            this.tvTriageWaiting.LostFocus -= new EventHandler(tvTriaggingPatient_LostFocus);
            this.tvTriageWaiting.LostFocus += new EventHandler(tvTriaggingPatient_LostFocus);

            this.tvTriageWaiting.DragEnter -= new DragEventHandler(tvTriaggingPatient_DragEnter);
            this.tvTriageWaiting.DragEnter += new DragEventHandler(tvTriaggingPatient_DragEnter);

            this.tvTriageWaiting.DragDrop -= new DragEventHandler(tvTriaggingPatient_DragDrop);
            this.tvTriageWaiting.DragDrop += new DragEventHandler(tvTriaggingPatient_DragDrop);

            if (this.triageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In
                || this.triageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage
                || this.triageStatus== FS.HISFC.Models.Nurse.EnuTriageStatus.Delay)
            {
                this.tvTriageWaiting.ItemDrag -= new ItemDragEventHandler(tvTriaggingPatient_ItemDrag);
                this.tvTriageWaiting.ItemDrag += new ItemDragEventHandler(tvTriaggingPatient_ItemDrag);
                this.tvTriageWaiting.AllowDrop = true;
            }

            return 1;
        }

        #endregion

        #region 方法

        private void setName(int count)
        {
            switch (this.triageStatus)
            {
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Triage:
                    this.gbTriageWaiting.Text = "候诊：" + count.ToString();
                    break;
                case FS.HISFC.Models.Nurse.EnuTriageStatus.In:
                    this.gbTriageWaiting.Text = "进诊：" + count.ToString();
                    break;
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive:
                    this.gbTriageWaiting.Text = "到诊：" + count.ToString();
                    break;
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Out:
                    this.gbTriageWaiting.Text = "已诊：" + count.ToString();
                    break;
                case FS.HISFC.Models.Nurse.EnuTriageStatus.Delay:
                    this.gbTriageWaiting.Text = "暂不看诊或未看诊：" + count.ToString();
                    break;
                default:
                    this.gbTriageWaiting.Text = "其他：" + count.ToString();
                    break;
            }

            if (queue != null)
            {
                switch (this.triageStatus)
                {
                    case FS.HISFC.Models.Nurse.EnuTriageStatus.Triage:
                        this.gbTriageWaiting.Text = queue.Name + "-" + this.gbTriageWaiting.Text;
                        break;
                    case FS.HISFC.Models.Nurse.EnuTriageStatus.In:
                        this.gbTriageWaiting.Text = queue.SRoom.Name + "-" + this.gbTriageWaiting.Text;
                        break;
                    case FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive:
                        this.gbTriageWaiting.Text = queue.Console.Name + "-" + this.gbTriageWaiting.Text;
                        break;
                    case FS.HISFC.Models.Nurse.EnuTriageStatus.Out:
                        this.gbTriageWaiting.Text = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployeeName(queue.Doctor.ID) + "-" + this.gbTriageWaiting.Text;
                        break;
                    default:
                        this.gbTriageWaiting.Text = queue.Name + "-" + this.gbTriageWaiting.Text;
                        break;
                }

            }

        }

        private void addNode(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            TreeNode node = null;
            //加载未包含的
            if (!this.tvTriageWaiting.Nodes.ContainsKey(assign.Register.ID))
            {
                node = new TreeNode();
                this.tvTriageWaiting.Nodes.Add(node);
            }
            else
            {
                node = this.tvTriageWaiting.Nodes[assign.Register.ID];
            }

            node.Name = assign.Register.ID;
            FS.HISFC.BizProcess.Integrate.Registration.Registration reg = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
            if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay)
            {
                if (assign.Register.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                {                  
                    ArrayList regList = reg.QueryPatient(assign.Register.ID);
                    if (regList.Count > 0)
                    {
                        FS.HISFC.Models.Registration.Register register = regList[0] as FS.HISFC.Models.Registration.Register;
                        node.Text = "[" + assign.SeeNO.ToString() + "号]" + assign.Register.Name
                            + "(" + register.DoctorInfo.Templet.Begin.ToString("HH:mm") + "-" + register.DoctorInfo.Templet.End.ToString("HH:mm") + ")" 
                            + "--队列：" + assign.Queue.Name;
                    }
                    else
                    {
                        node.Text = "[" + assign.SeeNO.ToString() + "号]" + assign.Register.Name + "--队列：" + assign.Queue.Name;
                    }
                }
                else
                {
                    node.Text = "[" + assign.SeeNO.ToString() + "号]" + assign.Register.Name + "--队列：" + assign.Queue.Name;
                }
            }
            else
            {
                if (assign.Register.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                { 
                    ArrayList regList = reg.QueryPatient(assign.Register.ID);
                    if (regList.Count > 0)
                    {
                        FS.HISFC.Models.Registration.Register register = regList[0] as FS.HISFC.Models.Registration.Register;
                        node.Text = "[" + assign.SeeNO.ToString() + "号]" + assign.Register.Name + "(" + register.DoctorInfo.Templet.Begin.ToString("HH:mm")
                            + "-" + register.DoctorInfo.Templet.End.ToString("HH:mm") + ")";
                    }
                    else
                    {
                        node.Text = "[" + assign.SeeNO.ToString() + "号]" + assign.Register.Name;
                    }
                }
                else
                {
                    node.Text = "[" + assign.SeeNO.ToString() + "号]" + assign.Register.Name;
                }
            }
            node.Tag = assign;
            this.setName(tvTriageWaiting.Nodes.Count);
            
        }

        #endregion

        #region 触发事件

        void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }
            FS.SOC.HISFC.Assign.Models.Assign assign = null;
            if (e.Node.Tag is FS.SOC.HISFC.Assign.Models.Assign)
            {
                assign = e.Node.Tag as FS.SOC.HISFC.Assign.Models.Assign;

            }

            if (this.selectedAssignChange != null)
            {
                this.selectedAssignChange(assign);
            }
        }

        void tvTriaggingPatient_LostFocus(object sender, EventArgs e)
        {
            this.tvTriageWaiting.SelectedNode = null;
        }

        void tvTriaggingPatient_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item == null) return;
            if (e.Button == MouseButtons.Left && e.Item is TreeNode)
            {
                if ((e.Item as TreeNode).Tag == null) return;

                DragDropEffects dropEffect = this.tvTriageWaiting.DoDragDrop((e.Item as TreeNode).Tag, DragDropEffects.All | DragDropEffects.Move);
            }
        }

        void tvTriaggingPatient_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.SOC.HISFC.Assign.Models.Assign)))
            {
                if (this.dragDropAssign != null)
                {
                    this.dragDropAssign(e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign)) as FS.SOC.HISFC.Assign.Models.Assign);
                }
            }
            else if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
            {
                if (this.dragDropRegister != null)
                {
                    this.dragDropRegister(e.Data.GetData(typeof(FS.HISFC.Models.Registration.Register)) as FS.HISFC.Models.Registration.Register);
                }
            }
        }

        void tvTriaggingPatient_DragEnter(object sender, DragEventArgs e)
        {
           if (this.triageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
            {
                if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else if (e.Data.GetDataPresent(typeof(FS.SOC.HISFC.Assign.Models.Assign))
                && (
                    ((FS.SOC.HISFC.Assign.Models.Assign)e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign))).TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In 
                      || ((FS.SOC.HISFC.Assign.Models.Assign)e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign))).TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay
                        )
                    )
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else if (this.triageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
            {
                if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else if (e.Data.GetDataPresent(typeof(FS.SOC.HISFC.Assign.Models.Assign))
                  && (
                    ((FS.SOC.HISFC.Assign.Models.Assign)e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign))).TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage
                    || ((FS.SOC.HISFC.Assign.Models.Assign)e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign))).TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay
                    )
                    )
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
           else if (this.triageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay)
           {
               if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
               {
                   e.Effect = DragDropEffects.Move;
               }
               else if (e.Data.GetDataPresent(typeof(FS.SOC.HISFC.Assign.Models.Assign))
                   &&
                   (((FS.SOC.HISFC.Assign.Models.Assign)e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign))).TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage
                   || ((FS.SOC.HISFC.Assign.Models.Assign)e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign))).TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
               )
               {
                   e.Effect = DragDropEffects.Move;
               }
               else
               {
                   e.Effect = DragDropEffects.None;
               }
           }
        }

        #endregion
        
        #region ITriaggedPatient 成员

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> SelectedItemChange
        {
            get
            {
                return selectedAssignChange;
            }
            set
            {
                selectedAssignChange = value;
            }
        }

        public int AddRange(System.Collections.ArrayList alPatient)
        {
            if (alPatient != null)
            {
                foreach (FS.SOC.HISFC.Assign.Models.Assign obj in alPatient)
                {
                    this.addNode(obj);
                }

                ArrayList temp = new ArrayList();
                foreach (TreeNode item in this.tvTriageWaiting.Nodes)
                {
                    if (!alPatient.Contains(item.Tag))
                    {
                        temp.Add(item);
                    }
                }
                int i = 0;
                int num = temp.Count; ;
                while (temp.Count > 0 && i < num * 2)
                {
                    this.tvTriageWaiting.Nodes.Remove(temp[0] as TreeNode);
                    temp.Remove(temp[0]);
                    this.setName(tvTriageWaiting.Nodes.Count);
                    i++;
                }
            }
            this.tvTriageWaiting.Sort();
            Application.DoEvents();
            return 1;
        }

        public FS.HISFC.Models.Nurse.EnuTriageStatus TriageStatus
        {
            get {
                return this.triageStatus;
            }
            set
            {
                triageStatus = value;
                this.setName(this.tvTriageWaiting.Nodes.Count);
            }
        }

        public FS.SOC.HISFC.Assign.Models.Assign SelectItem
        {
            get
            {
                if (this.tvTriageWaiting.SelectedNode != null)
                {
                    return this.tvTriageWaiting.SelectedNode.Tag as FS.SOC.HISFC.Assign.Models.Assign;
                }
                return null;
            }
        }

        public FS.SOC.HISFC.Assign.Models.Assign FirstItem
        {
            get
            {
                if (this.tvTriageWaiting.Nodes.Count > 0)
                {
                    return this.tvTriageWaiting.Nodes[0].Tag as FS.SOC.HISFC.Assign.Models.Assign;
                }
                return null;
            }
        }

        public FS.SOC.HISFC.Assign.Models.Queue Queue
        {
            get
            {
                return this.queue;
            }
            set
            {
                this.queue = value;
                this.setName(this.tvTriageWaiting.Nodes.Count);
            }
        }

        public int Remove(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (this.tvTriageWaiting.Nodes.ContainsKey(assign.Register.ID))
            {
                this.tvTriageWaiting.Nodes.RemoveByKey(assign.Register.ID);
            }
            this.setName(this.tvTriageWaiting.Nodes.Count);
            Application.DoEvents();
            return 1;
        }

        public int Add(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return -1;
            }

            this.addNode(assign);

            this.tvTriageWaiting.Sort();

            if (this.tvTriageWaiting.Nodes.ContainsKey(assign.Register.ID))
            {
                this.tvTriageWaiting.SelectedNode = this.tvTriageWaiting.Nodes[assign.Register.ID];
            }
            Application.DoEvents();
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> DragDropRegister
        {
            get
            {
                return this.dragDropRegister;
            }
            set
            {
                dragDropRegister = value;
            }
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> DragDropAssign
        {
            get
            {
                return this.dragDropAssign;
            }
            set
            {
                dragDropAssign = value;
            }
        }
        #endregion

        #region IInitialisable 成员

        public int Init()
        {
            this.initEvents();

            this.tvTriageWaiting.ImageList = this.tvTriageWaiting.groupImageList;
            this.tvTriageWaiting.ImageIndex = 2;
            this.tvTriageWaiting.SelectedImageIndex = 3;
            return 1;
        }

        #endregion

        #region ILoadable 成员

        public new int Load()
        {
            return 1;
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.tvTriageWaiting.Nodes.Clear();
            this.queue = null;
            this.setName(this.tvTriageWaiting.Nodes.Count);

            return 1;
        }

        #endregion
    }
}
