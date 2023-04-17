using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucUserSelect : UserControl
    {
        /// <summary>
        /// 私有成员
        /// </summary>
        private selectObject selectObjectTemp =new selectObject();

        /// <summary>
        /// 管理业务层 用来获取过滤条件公式
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 构造函数
        /// </summary>
        public ucUserSelect()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 属性
        /// </summary>
        public selectObject SelectObjectTemp
        {
            get
            {
                GetSelectObject();
                return selectObjectTemp;
            }
            set
            {
                selectObjectTemp = value;
            }
        }

        /// <summary>
        /// 查询条件的选择项目
        /// </summary>
        private ArrayList arrListItem = null;
        public ArrayList ArrListItem
        {
            get
            {
                return this.arrListItem;
            }
            set
            {
                arrListItem = value;
            }
        }

        /// <summary>
        /// 待筛选信息
        /// </summary>
        private ArrayList tableInfo = null;
        public ArrayList TableInfo
        {
            get
            {
                return this.tableInfo;
            }
            set
            {
                tableInfo = value;
            }
        }

        private ArrayList arrAlLj = null;
        public ArrayList ArrAlLj
        {
            get
            {
                return this.arrAlLj;
            }
            set
            {
                arrAlLj = value;
            }
        }

        /// <summary>
        /// 获取查询项的值，并绑定到下拉列表处
        /// </summary>
        public void GetSelectItem()
        {
            ArrayList arrSelectField = new ArrayList();
            if((arrListItem!=null)&&(arrListItem.Count>0))
            {
                foreach (string str in arrListItem)
                {
                    NeuObject obj = new NeuObject();
                    obj.ID = str.ToString();
                    obj.Name = str.ToString();
                    arrSelectField.Add(obj);
                }
                dropSelectItem.AddItems(arrSelectField);
            }
        }

        public void GetSelectFormula()
        {
            if ((this.ArrAlLj != null) && (this.ArrAlLj.Count > 0))
            {
                this.cmbLj1.AddItems(this.ArrAlLj);
            }
        }

        public void GetAelectAndOr()
        {
            ArrayList arrAndOr = new ArrayList();
            NeuObject AndOr1 = new NeuObject();
            AndOr1.ID = "AND";
            AndOr1.Name = "与";
            arrAndOr.Add(AndOr1);

            NeuObject AndOr2 = new NeuObject();
            AndOr2.ID = "OR";
            AndOr2.Name = "或";
            arrAndOr.Add(AndOr2);

            comAndOr.AddItems(arrAndOr);

        }

        /// <summary>
        /// 当项目的下拉列表发生改变时，联动的将后面的下拉列表的绑定信息进行改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dropSelectItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            dropSelectBy.ClearItems();
            ArrayList arrSelectBy = new ArrayList();
            ArrayList arrSelectByTemp = new ArrayList();
            string selectItemSingle = this.dropSelectItem.Tag.ToString();
            if ((arrListItem != null) && (arrListItem.Count > 0))
            {
                foreach (string sTemp in arrListItem)
                {
                    if (selectItemSingle == sTemp)
                    {
                        foreach (cTable tableTemp in tableInfo)
                        {
                            if (sTemp == tableTemp.Item)
                            {
                                arrSelectByTemp = tableTemp.ArrSelectBy;
                                break;
                            }
                        }
                    }
                }
            }
            if ((arrSelectByTemp != null) && (arrSelectByTemp.Count > 0))
            {
                foreach (string singleTemp in arrSelectByTemp)
                {
                    NeuObject obj = new NeuObject();
                    obj.ID = singleTemp.ToString();
                    obj.Name = singleTemp.ToString();
                    arrSelectBy.Add(obj);
                }

                dropSelectBy.AddItems(arrSelectBy);
            }
        }

        /// <summary>
        /// 内部类，将查询条件打包
        /// </summary>
        public class selectObject
        {
            public string selectItem = string.Empty;
            public string selectBy = string.Empty;
            public string selectAndOr = string.Empty;
            public string selectWhere = string.Empty;
            public bool visibleAble = false;
            public string selectCondition = string.Empty;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        public void GetSelectObject()
        {
            selectObjectTemp.selectItem = string.IsNullOrEmpty(this.dropSelectItem.Text.ToString()) == true ? "" : this.dropSelectItem.Text.ToString();
            selectObjectTemp.selectBy = string.IsNullOrEmpty(this.dropSelectBy.Text.ToString()) == true ? "" : this.dropSelectBy.Text.ToString();
            selectObjectTemp.visibleAble=this.comAndOr.Visible;
            if (this.comAndOr.Visible)
            {
                selectObjectTemp.selectAndOr = string.IsNullOrEmpty(this.comAndOr.Tag.ToString()) == true ? "" : this.comAndOr.Tag.ToString();
            }
            else
            {
                selectObjectTemp.selectAndOr = null;
            }
            selectObjectTemp.selectWhere = string.IsNullOrEmpty(this.cmbLj1.Tag.ToString()) == true ? "" : this.cmbLj1.Tag.ToString();
            selectObjectTemp.selectCondition = string.Format(this.dropSelectItem.Text.ToString() + " " + this.cmbLj1.Tag.ToString(), this.dropSelectBy.Text.ToString());
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        ///// <summary>
        ///// AND单选框changed事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void rbtnAnd_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.rbtnOr.Checked = false;
        //}

        ///// <summary>
        ///// OR单选框changed事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void rbtnOr_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.rbtnAnd.Checked = false;
        //}

        private void ucUserSelect_Load(object sender, EventArgs e)
        {
            this.GetSelectItem();
            this.GetSelectFormula();
        }

        
    }
}
