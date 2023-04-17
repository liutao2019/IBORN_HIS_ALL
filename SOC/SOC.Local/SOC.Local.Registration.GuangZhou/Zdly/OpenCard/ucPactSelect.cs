using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Registration.GuangZhou.Zdly.OpenCard
{
    /// <summary>
    /// 合同单位选择
    /// </summary>
    public partial class ucPactSelect : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public delegate void CheckedNode(Neusoft.HISFC.Models.Base.PactInfo obj);

        public event CheckedNode CheckedNodeEvent;

        public delegate void EnterCloseDropDown();

        public event EnterCloseDropDown EnterCloseDropDownEvent;

        public ucPactSelect()
        {
            InitializeComponent();
            this.tvSelected.BeforeCheck += new TreeViewCancelEventHandler(tvSelected_BeforeCheck);
            this.tvSelected.MouseDoubleClick += new MouseEventHandler(tvSelected_MouseDoubleClick);
            this.tvUnselected.MouseDoubleClick += new MouseEventHandler(tvUnselected_MouseDoubleClick);
            this.tvSelected.AfterCheck += new TreeViewEventHandler(tvSelected_AfterCheck);
            this.tvSelected.KeyDown += new KeyEventHandler(tvSelected_KeyDown);
            this.tvUnselected.KeyDown += new KeyEventHandler(tvSelected_KeyDown);
            this.tvSelected.DragDrop+=new DragEventHandler(tv_DragDrop);
            this.tvUnselected.DragDrop += new DragEventHandler(tv_DragDrop);
            this.tvUnselected.DragEnter += new DragEventHandler(tv_DragEnter);
            this.tvSelected.DragEnter += new DragEventHandler(tv_DragEnter);
            this.tvSelected.ItemDrag += new ItemDragEventHandler(tv_ItemDrag);
            this.tvUnselected.ItemDrag += new ItemDragEventHandler(tv_ItemDrag);
            this.tvSelected.ImageList = this.tvSelected.deptImageList;
            this.tvUnselected.ImageList = this.tvUnselected.groupImageList;
        }

        void tv_DragEnter(object sender, DragEventArgs e)
        {
                e.Effect = DragDropEffects.Move;
        }

        void tvSelected_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                this.gbUnSelect.Select();
                this.tvUnselected.Focus();
            }
            else if (e.KeyCode == Keys.Right)
            {
                this.gbSelected.Select();
                this.tvSelected.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (tvUnselected.Focused)
                {
                    if (this.tvUnselected.SelectedNode != null)
                    {
                        this.AddSelected(this.tvUnselected.SelectedNode.Tag as Neusoft.HISFC.Models.Base.PactInfo,true);
                    }
                }
                else if (tvSelected.Focused)
                {
                    if (EnterCloseDropDownEvent != null)
                    {
                        EnterCloseDropDownEvent();
                    }
                }
            }

        }

        void tvSelected_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                if (CheckedNodeEvent != null)
                {
                    CheckedNodeEvent(e.Node.Tag as Neusoft.HISFC.Models.Base.PactInfo);
                }
            }
        }

        void tvUnselected_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = this.tvUnselected.GetNodeAt(e.X,e.Y);
            if (node != null)
            {
                this.tvUnselected.Nodes.RemoveByKey(node.Name);
                this.AddSelected(node.Tag as Neusoft.HISFC.Models.Base.PactInfo,true);
            }
        }

        void tvSelected_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = this.tvSelected.GetNodeAt(e.X,e.Y);
            if (node != null)
            {
                this.tvSelected.Nodes.RemoveByKey(node.Name);
                if (node.Checked)
                {
                    if (this.tvSelected.Nodes.Count > 0)
                    {
                        this.tvSelected.Nodes[0].Checked = true;
                    }
                }
                this.AddNoSelect(node.Tag as Neusoft.HISFC.Models.Base.PactInfo);
            }
        }

        private void tvSelected_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            this.tvSelected.CheckBoxes = false;
            this.tvSelected.CheckBoxes = true;

            this.tvSelected.SelectedNode = e.Node;
        }

        public void Clear()
        {
            for (int i = 0; i < this.tvSelected.Nodes.Count; i++)
            {
                this.AddNoSelect(this.tvSelected.Nodes[i].Tag as Neusoft.HISFC.Models.Base.PactInfo);
            }
            this.selectedList = new ArrayList();
        }

        public void ClearSelected()
        {
            for (int i = 0; i < this.tvSelected.Nodes.Count; i++)
            {
                if (selectedList != null && selectedList.Contains(this.tvSelected.Nodes[i].Tag as Neusoft.HISFC.Models.Base.PactInfo))
                {
                    continue;
                }
                this.AddNoSelect(this.tvSelected.Nodes[i].Tag as Neusoft.HISFC.Models.Base.PactInfo);
            }
        }

        public void AddNoSelect(Neusoft.HISFC.Models.Base.PactInfo obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ID))
            {
                return;
            }

            if (this.tvSelected.Nodes.ContainsKey(obj.ID))
            {
                this.tvSelected.Nodes.RemoveByKey(obj.ID);

                if (this.tvSelected.Nodes.Count > 0)
                {
                    this.tvSelected.Nodes[0].Checked = true;
                }
            }
            
            if (this.tvUnselected.Nodes.ContainsKey(obj.ID))
            {
                return;
            }

            TreeNode node = new TreeNode();
            node.Name = obj.ID;
            node.Text = obj.Name;
            node.Tag = obj;
            node.ImageIndex = 0;
            node.SelectedImageIndex = 1;
            this.tvUnselected.Nodes.Add(node);
        }

        public void AddSelected(Neusoft.HISFC.Models.Base.PactInfo obj,bool isCheck)
        {
            if (obj == null||string.IsNullOrEmpty(obj.ID))
            {
                return;
            }

            if (this.tvUnselected.Nodes.ContainsKey(obj.ID))
            {
                this.tvUnselected.Nodes.RemoveByKey(obj.ID);
            }

            if (this.tvSelected.Nodes.ContainsKey(obj.ID))
            {
                return;
            }

            
            if (isCheck)
            {
                foreach (TreeNode temp in this.tvSelected.Nodes)
                {
                    temp.Checked = false;
                }
            }
            TreeNode node = new TreeNode();
            node.Checked = isCheck;
            node.Name = obj.ID;
            node.Text = obj.Name;
            node.Tag = obj;
            node.ImageIndex = 0;
            node.SelectedImageIndex = 1;
            this.tvSelected.Nodes.Add(node);

            if (isCheck && this.CheckedNodeEvent!=null)
            {
                this.CheckedNodeEvent(obj);
            }

            this.tvSelected.Select();
            this.tvSelected.SelectedNode = node;
        }

        public Neusoft.HISFC.Models.Base.PactInfo SelectedDefault
        {
            get
            {
                for (int i = 0; i < this.tvSelected.Nodes.Count; i++)
                {
                    if (this.tvSelected.Nodes[i].Checked)
                    {
                        return tvSelected.Nodes[i].Tag as Neusoft.HISFC.Models.Base.PactInfo;
                    }
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.AddSelected(value,true);
                    if (this.tvSelected.Nodes.ContainsKey(value.ID))
                    {
                        this.tvSelected.SelectedNode = this.tvSelected.Nodes[value.ID];
                        this.tvSelected.SelectedNode.Checked = true;
                    }
                }
            }
        }

        private ArrayList selectedList = new ArrayList();

        public ArrayList SelectedList
        {
            get
            {
                return selectedList;
            }
            set
            {
                if (selectedList != null)
                {
                    selectedList = value;
                    foreach (Neusoft.HISFC.Models.Base.PactInfo obj in selectedList)
                    {
                        this.AddSelected(obj,false);
                    }
                }
            }
        }

        public ArrayList GetSelectedList()
        {
            ArrayList list = new ArrayList();
            Neusoft.HISFC.Models.Base.PactInfo pact = null;
            for (int i = 0; i < this.tvSelected.Nodes.Count; i++)
            {
                pact = tvSelected.Nodes[i].Tag as Neusoft.HISFC.Models.Base.PactInfo;
                if (pact != null)
                {
                    list.Add(pact);
                }
            }
            return list;
        }

        private void tv_DragDrop(object sender, DragEventArgs e)
        {
            if (sender is TreeView)
            {
                TreeNode node = e.Data.GetData("System.Windows.Forms.TreeNode") as TreeNode;
                if (node != null && node is TreeNode)
                {
                    if (((TreeView)sender).Name == this.tvSelected.Name)
                    {
                        this.AddSelected(node.Tag as Neusoft.HISFC.Models.Base.PactInfo,true);
                    }
                    else if (((TreeView)sender).Name == this.tvUnselected.Name)
                    {
                        this.AddNoSelect(node.Tag as Neusoft.HISFC.Models.Base.PactInfo);
                    }
                }
            }
           
        }

        private void tv_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.tvUnselected.Select();
            base.OnGotFocus(e);
        }

    }

    public class ComboBoxPactSelect : Neusoft.FrameWork.WinForms.Controls.NeuComboBox
    {
        #region 成员变量
        private const int WM_LBUTTONDOWN = 0x201, WM_LBUTTONDBLCLK = 0x203;
        ToolStripControlHost dataGridViewHost;
        ToolStripDropDown dropDown;
        private string m_sDefaultColumn;
        private bool m_blPopupAutoSize = false;

        #endregion

        public ucPactSelect DropDownControl
        {
            get
            {
                return dataGridViewHost.Control as ucPactSelect;
            }
        }

        #region 构造函数
        public ComboBoxPactSelect()
        {
            DrawComboBox();
        }
        #endregion

        #region 方法
        private void DrawComboBox()
        {
            ucPactSelect pactSelect = new ucPactSelect();
            pactSelect.CheckedNodeEvent += new ucPactSelect.CheckedNode(pactSelect_CheckedNodeEvent);
            pactSelect.EnterCloseDropDownEvent += new ucPactSelect.EnterCloseDropDown(pactSelect_EnterCloseDropDownEvent);
            //设置DataGridView的数据源
            Form frmDataSource = new Form();
            pactSelect.Dock = DockStyle.Fill;
            frmDataSource.Size = pactSelect.Size;
            frmDataSource.Controls.Add(pactSelect);
            frmDataSource.SuspendLayout();

            dataGridViewHost = new ToolStripControlHost(pactSelect);
            dataGridViewHost.AutoSize = m_blPopupAutoSize;

            dropDown = new ToolStripDropDown();
            dropDown.Width = this.Width;
            dropDown.Items.Add(dataGridViewHost);
            dropDown.Closed += new ToolStripDropDownClosedEventHandler(dropDown_Closed);
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void pactSelect_EnterCloseDropDownEvent()
        {
            isShow = false;
            this.HideDropDown();

        }

        void pactSelect_CheckedNodeEvent(Neusoft.HISFC.Models.Base.PactInfo obj)
        {
            if (obj != null)
            {
                string name = obj.Name;
                //ArrayList al = this.DropDownControl.GetSelectedList();
                //if (al != null)
                //{
                //    foreach (Neusoft.HISFC.Models.Base.PactInfo neuObj in al)
                //    {
                //        if (neuObj.ID.Equals(obj.ID) == false)
                //        {
                //            name += "|" + neuObj.Name;
                //        }
                //    }
                //}
                base.Tag = obj.ID;
                base.Text = name;
                
            }
        }

        private void dropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            //获取鼠标位置
            Point p = this.Parent.PointToScreen(this.Location);
            Point mousePosition = Control.MousePosition;
            if (!(mousePosition.X <= p.X + this.Size.Width && mousePosition.X >= p.X
                && mousePosition.Y >= p.Y && mousePosition.Y <= p.Y + this.Size.Height))
            {
                isShow = false;
            }

            if (e.CloseReason == ToolStripDropDownCloseReason.Keyboard)
            {
                isShow = false;
            }

            this.Select();
        }

        bool isShow = false;

        private void ShowDropDown()
        {
            isShow = !isShow;
            if (isShow)
            {
                if (dropDown != null)
                {
                    if (dropDown.Visible == false)
                    {
                        dataGridViewHost.Size = this.DropDownControl.Size;
                        if (this.DropDownControl.SelectedList == null || this.DropDownControl.SelectedList.Count == 0)
                        {
                            this.DropDownControl.AddSelected(this.SelectedItem as Neusoft.HISFC.Models.Base.PactInfo, true);
                        }
                        this.DropDownControl.Select();
                        dropDown.Show(this, 0, this.Height);
                    }
                }
            }
        }

        private void HideDropDown()
        {
            if (dropDown != null)
            {
                if (dropDown.Visible)
                {
                    dropDown.Hide();
                }
            }
        }

        #region 重写方法
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONDBLCLK)
            {

                ShowDropDown();

                return;
            }
            base.WndProc(ref m);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dropDown != null)
                {
                    dropDown.Dispose();
                    dropDown = null;
                }
            }
            base.Dispose(disposing);
        }

        public override int AddItems(ArrayList alItems)
        {
            if (alItems != null)
            {
                foreach (Neusoft.HISFC.Models.Base.PactInfo obj in alItems)
                {
                    this.DropDownControl.AddNoSelect(obj);
                }
            }

            return base.AddItems(alItems);

        }

        public void AddValues(ArrayList alItems)
        {
            if (alItems != null)
            {
                this.DropDownControl.SelectedList = alItems;
            }
        }

        public ArrayList GetValues()
        {
            ArrayList al= this.DropDownControl.GetSelectedList();
            if (al == null || al.Count == 0)
            {
                Neusoft.HISFC.Models.Base.PactInfo pact = this.SelectedItem as Neusoft.HISFC.Models.Base.PactInfo;
                if (pact != null)
                {
                    al.Add(pact);
                }
            }
            return al;
        }

        public override Object Tag
        {
            get
            {
                return base.Tag;
            }
            set
            {
                if (value != null)
                {
                    base.Tag = value.ToString();
                    this.DropDownControl.ClearSelected();
                    this.DropDownControl.SelectedDefault = base.SelectedItem as Neusoft.HISFC.Models.Base.PactInfo;
                }
            }
        }

        public void Clear()
        {
            base.Tag = "";
            base.Text = "";
            this.DropDownControl.Clear();
        }

        protected override void OnSelectedValueChanged(EventArgs e)
        {
            if (preSelectItem != null && preSelectItem.ID != this.SelectedItem.ID)
            {
                if (!DropDownControl.SelectedList.Contains(preSelectItem))
                {
                    this.DropDownControl.AddNoSelect(preSelectItem);
                }
            }

            this.DropDownControl.SelectedDefault = this.SelectedItem as Neusoft.HISFC.Models.Base.PactInfo;
        }

        private Neusoft.HISFC.Models.Base.PactInfo preSelectItem = null;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                ShowDropDown();
                return;
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left||e.KeyCode== Keys.Down||e.KeyCode== Keys.Right)
            {
                preSelectItem = this.SelectedItem as Neusoft.HISFC.Models.Base.PactInfo;
            }

            base.OnKeyDown(e);
        }

        #endregion


        #endregion
    }
}
