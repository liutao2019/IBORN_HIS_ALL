using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class frmUserSelectMuil : Form
    {
        public frmUserSelectMuil()
        {
            InitializeComponent();
        }
        #region 设置属性

        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 筛选SQL
        /// </summary>
        private string selectSql = string.Empty;
        public string SelectSql
        {
            set
            {
                this.selectSql = value;
            }
            get
            {
                return this.selectSql;
            }
        }

        /// <summary>
        /// 记录筛选字段
        /// </summary>
        private ArrayList arrlist = null;
        public ArrayList Arrlist
        {
            get
            {
                return this.arrlist;
            }
            set
            {
                arrlist = value;
            }
        }

        /// <summary>
        /// 下拉列表信息
        /// </summary>
        private ArrayList arrcTableInfo = null;
        public ArrayList ArrcTableInfo
        {
            get
            {
                return arrcTableInfo;
            }
            set
            {
                arrcTableInfo = value;
            }
        }

        private ArrayList arrAlLj = null;
        public ArrayList ArrAlLj
        {
            get
            {
                return arrAlLj;
            }
            set
            {
                arrAlLj = value;
            }
        }
        #endregion

        /// <summary>
        /// load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserSelectMuil_Load(object sender, EventArgs e)
        {
            this.ArrAlLj = this.managerIntegrate.GetConstantList("FilterType");
            this.ucUserSelectControl.ArrListItem = this.Arrlist;
            this.ucUserSelectControl.TableInfo = this.arrcTableInfo;
            this.ucUserSelectControl.ArrAlLj = this.ArrAlLj;
            this.ucUserSelectControl.GetSelectItem();
            this.ucUserSelectControl.GetSelectFormula();
            this.ucUserSelectControl.GetAelectAndOr();
            AddControllerUserSelectMuil();
        }

        /// <summary>
        /// 查询控件中添加按钮加载行为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {

            ucUserSelectControl = new ucUserSelect();
            foreach (Control c in ucUserSelectControl.Controls)
            {
                if (c.Name == "btnAdd")
                {
                    Button btn = c as Button;
                    btn.Visible = false;
                }
                if (c.Name == "comAndOr")
                {
                    FS.FrameWork.WinForms.Controls.NeuComboBox comTemp = c as FS.FrameWork.WinForms.Controls.NeuComboBox;
                    comTemp.Visible = true;
                }
                if (c.Name == "btnDelete")
                {
                    Button btn = c as Button;
                    btn.Visible = true;
                }
            }
            //flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(ucUserSelectControl);
            ucUserSelectControl.ArrListItem = this.Arrlist;
            ucUserSelectControl.TableInfo = this.ArrcTableInfo;
            this.ucUserSelectControl.ArrAlLj = this.ArrAlLj;
            ucUserSelectControl.GetSelectItem();
            ucUserSelectControl.GetSelectFormula();
            ucUserSelectControl.GetAelectAndOr();
            //ucUserSelectControl.GetSelectBy();
            //ucUserOrderControl.GetOrderBy();
        }



        /// <summary>
        /// 增加选择条件控件
        /// </summary>
        private void AddControllerUserSelectMuil()
        {
            //ucUserOrderControl = new ucUserOrder();
            foreach (Control c in ucUserSelectControl.Controls)
            {
                if (c.Name == "btnDelete")
                {
                    Button btn = c as Button;
                    //btn.Enabled = false;
                    btn.Visible = false;
                    //btn.Click += btnAdd;
                }
                if (c.Name == "comAndOr")
                {
                    FS.FrameWork.WinForms.Controls.NeuComboBox comTemp = c as FS.FrameWork.WinForms.Controls.NeuComboBox;
                    comTemp.Visible = false;
                }
                if (c.Name == "btnAdd")
                {
                    Button btn = c as Button;
                    btn.Click += BtnAdd_Click;
                }
            }
        }

        private void bntSelect_Click(object sender, EventArgs e)
        {
            bool flag = true;
            StringBuilder sb = new StringBuilder();
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                    ucUserSelect ucUserSelectOne = c as ucUserSelect;
                    //list.Add(ucUserOrderOne.UserOrderOjectTrans);
                    if (ucUserSelectOne.SelectObjectTemp.selectItem != "" && ucUserSelectOne.SelectObjectTemp.selectBy != ""&&ucUserSelectOne.SelectObjectTemp.selectWhere!="")
                    {
                        if (!ucUserSelectOne.SelectObjectTemp.visibleAble)
                        {
                            sb.Append(" ( " + ucUserSelectOne.SelectObjectTemp.selectCondition);
                        }
                        else
                        {
                        }
                        //if (sign == 1)
                        //{
                        //    sb = sb.Append(" " + ucUserSelectOne.SelectObjectTemp.selectItem + "='");
                        //    //sb = sb.Append(@"  ");
                        //    sb = sb.Append(ucUserSelectOne.SelectObjectTemp.selectBy);
                        //    sb = sb.Append(@"' ");
                        //    sign++;
                        //}
                        //else
                        //{
                        //    sb = sb.Append(" AND " + ucUserSelectOne.SelectObjectTemp.selectItem + "='");
                        //    //sb = sb.Append(@"  ");
                        //    sb = sb.Append(ucUserSelectOne.SelectObjectTemp.selectBy);
                        //    sb = sb.Append(@"' ");
                        //}
                    }
                    else
                    {
                        //throw new Exception("请输入需要排序的字段");
                        MessageBox.Show("请输入需要筛选的字段及筛选条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        flag = false;
                        break;
                    }
            }
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                ucUserSelect ucUserSelectOne = c as ucUserSelect;
                if (ucUserSelectOne.SelectObjectTemp.selectItem != "" && ucUserSelectOne.SelectObjectTemp.selectBy != "" && ucUserSelectOne.SelectObjectTemp.selectWhere != "")
                {
                    if (ucUserSelectOne.SelectObjectTemp.visibleAble)
                    {
                        sb.Append(" "+ucUserSelectOne.SelectObjectTemp.selectAndOr+" "+ucUserSelectOne.SelectObjectTemp.selectCondition+" ");
                    }
                    else
                    {
                    }
                }
                else
                {
                    MessageBox.Show("请输入需要筛选的字段及筛选条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flag = false;
                    break;
                }
            }
            sb.Append(@" )");
            if (sb.Length > 0 && flag == true)
            {
                //sb.Remove(sb.Length - 1, 1);
                SelectSql = sb.ToString();
                this.Close();
            }
        }

        private void bntReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region 事件
        #endregion
    }
}