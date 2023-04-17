using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// [功能描述： 传染病常数备注选择项]
    /// [创 建 者 ： 赵景]
    /// [创建时间： 2008-09]
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public partial class ucMyReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMyReport()
        {
            InitializeComponent();
        }

        public ucMyReport(Hashtable hs)
        {
            InitializeComponent();
            if (hs != null)
            {
                this.hsMemo = hs;
            }
        }

        /// <summary>
        ///  数据保存委托
        /// </summary>
        /// <param name="memo"></param>
        public delegate void DataBinding(Hashtable hs);

        /// <summary>
        /// 数据保存事件
        /// </summary>
        public event DataBinding enterDataBinding;

        /// <summary>
        /// 传入常数列表
        /// </summary>
        private ArrayList constList = null;

        /// <summary>
        /// 备注集合
        /// </summary>
        private Hashtable hsMemo=new Hashtable();

        /// <summary>
        /// 传入常数列表
        /// </summary>
        public ArrayList ConstList
        {
            get
            {
                return this.constList;
            }
            set
            {
                this.constList = value;

                InitListView();
            }
        }

        /// <summary>
        /// 设置选项
        /// </summary>
        private void InitListView()
        {
            if (this.ConstList == null || this.ConstList.Count == 0)
            {
                return;
            }
            this.lsvMemo.Items.Clear();

            foreach (FS.HISFC.Models.Base.Const con in this.ConstList)
            {
                System.Windows.Forms.ListViewItem listviewItem = new ListViewItem();
                listviewItem.Text = con.Name;
                listviewItem.Name = con.ID;
                listviewItem.Tag = con;

                if (this.hsMemo.ContainsKey(con.ID))
                {
                    listviewItem.Checked = true;
                }
                
                this.lsvMemo.Items.Add(listviewItem);
            }
        }

        /// <summary>
        /// OK确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            this.hsMemo.Clear();
            foreach (System.Windows.Forms.ListViewItem listviewItem in this.lsvMemo.Items)
            {
                if (listviewItem.Checked)
                {
                    hsMemo.Add(listviewItem.Name, listviewItem.Text);
                }
            }

            this.enterDataBinding(this.hsMemo);

            this.ParentForm.Close();
        }

        /// <summary>
        /// cancel取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void lsvMemo_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                this.lsvMemo.Items[e.Index].ForeColor = Color.Red;
            }
            else
            {
                this.lsvMemo.Items[e.Index].ForeColor = Color.Black;
            }
        }
    }
}
