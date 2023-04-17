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
    public partial class frmUserOrder : Form
    {
        public frmUserOrder()
        {
            InitializeComponent();

        }

        #region 属性设置
        /// <summary>
        /// 字段排列顺序
        /// </summary>
        private string orderSql = string.Empty;
        public string OrderSql
        {
            set
            {
                this.orderSql = value;
            }
            get
            {
                return this.orderSql;
            }

        }
        
        /// <summary>
        /// 记录查询字段
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
        #endregion

        #region 事件
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserOrder_Load(object sender, EventArgs e)
        {

            this.ucUserOrderControl.Arrlist = this.Arrlist;
            this.ucUserOrderControl.GetOrderList();
            this.ucUserOrderControl.GetOrderBy();
            AddControllerUserOrder();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            bool flag = true;
            StringBuilder sb = new StringBuilder();
            foreach (Control c in flowLayoutPanel.Controls)
            {
                ucUserOrder ucUserOrderOne = c as ucUserOrder;                
                //list.Add(ucUserOrderOne.UserOrderOjectTrans);
                if (ucUserOrderOne.UserOrderOjectTrans.dropUserOrder != "" && ucUserOrderOne.UserOrderOjectTrans.dropUserOrderBy != "")
                {
                    sb = sb.Append(ucUserOrderOne.UserOrderOjectTrans.dropUserOrder);
                    sb = sb.Append(@"  ");
                    sb = sb.Append(ucUserOrderOne.UserOrderOjectTrans.dropUserOrderBy);
                    sb = sb.Append(",");
                }   
                else
                {
                    //throw new Exception("请输入需要排序的字段");
                    MessageBox.Show("请输入需要排序的字段及顺序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flag = false;
                }
            }
            if (sb.Length > 0 && flag == true)
            {
                sb.Remove(sb.Length - 1, 1);
                OrderSql = sb.ToString();
                this.Close();
            }

            
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

         //<summary>
         //排序控件中添加按钮加载行为
         //</summary>
         //<param name="sender"></param>
         //<param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            
            
            ucUserOrderControl = new ucUserOrder();
            foreach (Control c in ucUserOrderControl.Controls)
            {
                if (c.Name == "btnAdd")
                {
                    Button btn = c as Button;
                    btn.Enabled = false;
                }
            }
            //flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.Controls.Add(ucUserOrderControl);
            ucUserOrderControl.Arrlist = this.Arrlist;
            ucUserOrderControl.GetOrderList();
            ucUserOrderControl.GetOrderBy();
        }

        /// <summary>
        /// 添加排序控件
        /// </summary>
        private void AddControllerUserOrder()
        {
            //ucUserOrderControl = new ucUserOrder();
            foreach (Control c in ucUserOrderControl.Controls)
            {
                if (c.Name == "btnDelete")
                {
                    Button btn = c as Button;
                    btn.Enabled = false;
                    //btn.Click += btnAdd;
                }
                if (c.Name == "btnAdd")
                {
                    Button btn = c as Button;
                    btn.Click += BtnAdd_Click;

                }
            }
        }

        #endregion

    }
}