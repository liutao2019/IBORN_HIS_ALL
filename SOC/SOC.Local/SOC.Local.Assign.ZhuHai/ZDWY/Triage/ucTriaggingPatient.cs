using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Function;

namespace SOC.Local.Assign.ShenZhen.BinHai.Triage
{
    /// <summary>
    /// [功能描述: 门诊待分诊或已分诊患者信息显示]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    internal partial class ucTriaggingPatient : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.SOC.HISFC.Assign.Interface.Components.ITriaggingPatient
    {
        public ucTriaggingPatient()
        {
            InitializeComponent();
        }

        #region 域变量

        private Neusoft.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Neusoft.HISFC.Models.Registration.Register> selectedReigsterChange;

        private Neusoft.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Neusoft.SOC.HISFC.Assign.Models.Assign> dragDrop;

        /// <summary>
        /// 不进行自动分诊的科室
        /// </summary>
        string noTriageDeptCode = string.Empty;
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

        public int addNode(Neusoft.HISFC.Models.Registration.Register register)
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
            string str = string.Empty;
            if (register.DoctorInfo.Templet.RegLevel.IsEmergency == true)
            { str = "(急)"; }
            else
            { str = ""; }
            string strHot = string.Empty;
            if (NConvert.ToInt32(register.Temperature) > 37.8)//NConvert.ToDecimal()
            { strHot = "(热)"; }
            else
            { strHot = ""; }
            if (register.RegType == Neusoft.HISFC.Models.Base.EnumRegType.Pre)//预约挂号
            {
                node.Text = str + "[" + register.DoctorInfo.SeeNO.ToString() + "号" + "]" + register.Name + strHot
                    + "(" + register.DoctorInfo.Templet.Begin.ToString("HH:mm") + "-" + register.DoctorInfo.Templet.End.ToString("HH:mm") + ")";
            }
            else
            {
                node.Text = str + "[" + register.DoctorInfo.SeeNO.ToString() + "号" + "]" + register.Name + strHot;
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
            Neusoft.HISFC.Models.Registration.Register register = null;
            if (e.Node.Tag is Neusoft.HISFC.Models.Registration.Register)
            {
                register = e.Node.Tag as Neusoft.HISFC.Models.Registration.Register;

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
            if (e.Data.GetDataPresent(typeof(Neusoft.SOC.HISFC.Assign.Models.Assign)))
            {
                if (this.dragDrop != null)
                {
                    this.dragDrop(e.Data.GetData(typeof(Neusoft.SOC.HISFC.Assign.Models.Assign)) as Neusoft.SOC.HISFC.Assign.Models.Assign);
                }
            }
        }

        void tvTriaggingPatient_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(typeof(Neusoft.SOC.HISFC.Assign.Models.Assign)) )
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
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

            noTriageDeptCode = (new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam()).GetControlParam<string>("200061", true, "");
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
                foreach (Neusoft.HISFC.Models.Registration.Register register in alPatient)
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

        public Neusoft.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Neusoft.HISFC.Models.Registration.Register> SelectedItemChange
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

        public Neusoft.HISFC.Models.Registration.Register SelectItem
        {
            get
            {
                if (this.tvTriaggingPatient.SelectedNode != null)
                {
                    return this.tvTriaggingPatient.SelectedNode.Tag as Neusoft.HISFC.Models.Registration.Register;
                }
                return null;
            }
        }

        public Neusoft.HISFC.Models.Registration.Register FirstItem
        {
            get
            {
                if (this.tvTriaggingPatient.Nodes.Count > 0)
                {
                    //return this.tvTriaggingPatient.Nodes[0].Tag as Neusoft.HISFC.Models.Registration.Register;
                    for (int a = 0; a < this.tvTriaggingPatient.Nodes.Count; a++)
                    {
                        Neusoft.HISFC.Models.Registration.Register reg = this.tvTriaggingPatient.Nodes[a].Tag as Neusoft.HISFC.Models.Registration.Register;
                        if (reg.RegType == Neusoft.HISFC.Models.Base.EnumRegType.Pre)
                        {
                            DateTime dtNow = (new Neusoft.SOC.HISFC.Assign.BizLogic.Assign()).GetDateTimeFromSysDateTime();
                            string NowTime = dtNow.ToString("HH") + dtNow.ToString("mm");
                            string preTime = reg.DoctorInfo.Templet.Begin.ToString("HH") + reg.DoctorInfo.Templet.Begin.ToString("mm");
                            string endTime = reg.DoctorInfo.Templet.End.ToString("HH") + reg.DoctorInfo.Templet.End.ToString("mm");
                            if (NConvert.ToDecimal(NowTime) >= NConvert.ToDecimal(preTime) && NConvert.ToDecimal(NowTime) <= NConvert.ToDecimal(endTime))//现在时间要开始大于等于预约时间，才开始这个操作
                            {
                                return reg;
                            }
                        }
                        else
                        {
                            if (noTriageDeptCode != "")//设置了，才往下走，如果没设置，现场的就不自动分诊
                            {
                                string dept = ((Neusoft.HISFC.Models.Base.Employee)(new Neusoft.FrameWork.Management.DataBaseManger()).Operator).Dept.ID;
                                if (dept == noTriageDeptCode)//全科的
                                {

                                }
                                else//其他科室的要不要自动分诊
                                {
                                    return reg;
                                }
                            }
                        }
                    }
                }
                return null;
            }
        }

        public int Remove(Neusoft.HISFC.Models.Registration.Register register)
        {
            if (this.tvTriaggingPatient.Nodes.ContainsKey(register.ID))
            {
                this.tvTriaggingPatient.Nodes.RemoveByKey(register.ID);
            }

            return 1;
        }

        public int Add(Neusoft.HISFC.Models.Registration.Register register)
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

        public Neusoft.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Neusoft.SOC.HISFC.Assign.Models.Assign> DragDrop
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
        #endregion
    }
}
