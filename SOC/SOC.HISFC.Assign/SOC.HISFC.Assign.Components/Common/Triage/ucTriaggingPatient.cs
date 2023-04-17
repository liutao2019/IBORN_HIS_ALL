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
    /// [功能描述: 门诊待分诊或已分诊患者信息显示]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    internal partial class ucTriaggingPatient : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.Assign.Interface.Components.ITriaggingPatient
    {
        public ucTriaggingPatient()
        {
            InitializeComponent();
        }

        #region 域变量

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> selectedReigsterChange;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> dragDrop;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> dragDropRegister;

        private FS.HISFC.Models.Nurse.EnuTriageStatus triageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.None;
        #endregion

        #region 初始化

        private int initEvents()
        {
            this.tvTriaggingPatient.AfterSelect -= new TreeViewEventHandler(tvTriaggingPatient_AfterSelect);
            this.tvTriaggingPatient.AfterSelect += new TreeViewEventHandler(tvTriaggingPatient_AfterSelect);

            this.tvTriaggingPatient.LostFocus -= new EventHandler(tvTriaggingPatient_LostFocus);
            this.tvTriaggingPatient.LostFocus += new EventHandler(tvTriaggingPatient_LostFocus);

            this.tvTriaggingPatient.DragEnter -= new DragEventHandler(tvTriaggingPatient_DragEnter);
            this.tvTriaggingPatient.DragEnter += new DragEventHandler(tvTriaggingPatient_DragEnter);

            this.tvTriaggingPatient.DragDrop -= new DragEventHandler(tvTriaggingPatient_DragDrop);
            this.tvTriaggingPatient.DragDrop += new DragEventHandler(tvTriaggingPatient_DragDrop);

            this.tvTriaggingPatient.ItemDrag -= new ItemDragEventHandler(tvTriaggingPatient_ItemDrag);
            this.tvTriaggingPatient.ItemDrag += new ItemDragEventHandler(tvTriaggingPatient_ItemDrag);

            return 1;
        }

        #endregion

        #region 方法

        public int addNode(FS.HISFC.Models.Registration.Register register)
        {
            TreeNode node = null;
            //加载未包含的
            if (!this.tvTriaggingPatient.Nodes.ContainsKey(register.ID))
            {
                node = new TreeNode();
                this.tvTriaggingPatient.Nodes.Add(node);
            }
            else
            {
                node = this.tvTriaggingPatient.Nodes[register.ID];
            }

            node.Name = register.ID;
            if (register.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)//预约挂号
            {
                node.Text = "[" + register.DoctorInfo.SeeNO.ToString() + "号" + "]" + register.Name
                    + "(" + register.DoctorInfo.Templet.Begin.ToString("HH:mm") + "-" + register.DoctorInfo.Templet.End.ToString("HH:mm") + ")";
            }
            else
            {
                node.Text = "[" + register.DoctorInfo.SeeNO.ToString() + "号" + "]" + register.Name;
            }
            node.Tag = register;

            return 1;
        }

        #endregion

        #region 触发事件

        void tvTriaggingPatient_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }
            FS.HISFC.Models.Registration.Register register = null;
            if (e.Node.Tag is FS.HISFC.Models.Registration.Register)
            {
                register = e.Node.Tag as FS.HISFC.Models.Registration.Register;

            }

            if (this.selectedReigsterChange != null)
            {
                this.selectedReigsterChange(register);
            }
        }

        void tvTriaggingPatient_LostFocus(object sender, EventArgs e)
        {
            this.tvTriaggingPatient.SelectedNode = null;
        }

        void tvTriaggingPatient_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item == null) return;
            if (e.Button == MouseButtons.Left && e.Item is TreeNode)
            {
                if ((e.Item as TreeNode).Tag == null) return;

                DragDropEffects dropEffect = this.tvTriaggingPatient.DoDragDrop((e.Item as TreeNode).Tag, DragDropEffects.All | DragDropEffects.Move);
            }
        }

        void tvTriaggingPatient_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.SOC.HISFC.Assign.Models.Assign)))
            {
                if (this.dragDrop != null)
                {
                    this.dragDrop(e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign)) as FS.SOC.HISFC.Assign.Models.Assign);
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

            if (e.Data.GetDataPresent(typeof(FS.SOC.HISFC.Assign.Models.Assign)) )
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        #endregion

        #region IInitialisable 成员

        public int Init()
        {
            this.initEvents();

            this.tvTriaggingPatient.ImageList = this.tvTriaggingPatient.groupImageList;
            this.tvTriaggingPatient.ImageIndex = 2;
            this.tvTriaggingPatient.SelectedImageIndex = 3;

            this.tvTriaggingPatient.AllowDrop = true;
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
            return 1;
        }

        #endregion

        #region ITriaggingPatient 成员

        public int AddRange(System.Collections.ArrayList alPatient)
        {
            if (alPatient != null)
            {
                foreach (FS.HISFC.Models.Registration.Register register in alPatient)
                {
                    this.addNode(register);
                }

                ArrayList temp = new ArrayList();
                foreach (TreeNode item in this.tvTriaggingPatient.Nodes)
                {
                    if (!alPatient.Contains(item.Tag))
                    {
                        temp.Add(item);
                    }
                }
                int i = 0;
                int num = temp.Count; 
                while (temp.Count > 0 && i < num * 2)
                {
                    this.tvTriaggingPatient.Nodes.Remove(temp[0] as TreeNode);
                    temp.Remove(temp[0]);
                    i++;
                }

            }
            this.neuGroupBox1.Text = "待分诊患者：" + this.tvTriaggingPatient.Nodes.Count.ToString();
            this.tvTriaggingPatient.Sort();
            Application.DoEvents();
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> SelectedItemChange
        {
            get
            {
                return selectedReigsterChange;
            }
            set
            {
                selectedReigsterChange = value;
            }
        }

        public FS.HISFC.Models.Registration.Register SelectItem
        {
            get
            {
                if (this.tvTriaggingPatient.SelectedNode != null)
                {
                    return this.tvTriaggingPatient.SelectedNode.Tag as FS.HISFC.Models.Registration.Register;
                }
                return null;
            }
        }

        public FS.HISFC.Models.Registration.Register FirstItem
        {
            get
            {
                if (this.tvTriaggingPatient.Nodes.Count > 0)
                {
                    return this.tvTriaggingPatient.Nodes[0].Tag as FS.HISFC.Models.Registration.Register;
                }
                return null;
            }
        }

        public int Remove(FS.HISFC.Models.Registration.Register register)
        {
            if (this.tvTriaggingPatient.Nodes.ContainsKey(register.ID))
            {
                this.tvTriaggingPatient.Nodes.RemoveByKey(register.ID);
            }

            return 1;
        }

        public int Add(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return -1;
            }

            this.addNode(register);

            this.tvTriaggingPatient.Sort();

            if (this.tvTriaggingPatient.Nodes.ContainsKey(register.ID))
            {
                this.tvTriaggingPatient.SelectedNode = this.tvTriaggingPatient.Nodes[register.ID];
            }
            Application.DoEvents();

            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> DragDrop
        {
            get
            {
                return dragDrop;
            }
            set
            {
                dragDrop = value;
            }
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

        #endregion

        #region ITriaggingPatient 成员


        public FS.HISFC.Models.Nurse.EnuTriageStatus TriageStatus
        {
            get
            {
                return triageStatus;
            }
            set
            {
                triageStatus = value;
            }
        }

        #endregion
    }
}
