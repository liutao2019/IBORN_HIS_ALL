using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucUserOrder : UserControl
    {
        private UserOrderObject UserOrderOjectTemp = new UserOrderObject();

        public ucUserOrder()
        {
            InitializeComponent();
        }

        #region 属性
        /// <summary>
        /// 打包控件2个下拉框 成为一个对象
        /// </summary>
        public UserOrderObject UserOrderOjectTrans
        {
            get
            {
                GetUserOrderObject();
                return UserOrderOjectTemp;
            }
            set
            {
                UserOrderOjectTemp = value;
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

        #region 获取下拉框值,绑定下拉框的值
        public void GetOrderList()
        {

            //this.dropOrderField.AddItems(arrlist);
            //this.dropOrderField.DataSource = arrlist;
            ArrayList arrOrderField = new ArrayList(); 
            foreach (string str in arrlist)
            {
                NeuObject obj = new NeuObject();
                obj.ID = str.ToString();
                obj.Name = str.ToString();
                arrOrderField.Add(obj);
            }
            dropOrderField.AddItems(arrOrderField);

        }

        public void GetOrderBy()
        {
            ArrayList arrOrderBy = new ArrayList();
            NeuObject OrderBy1 = new NeuObject();
            OrderBy1.ID = "ASC";
            OrderBy1.Name = "升序";
            arrOrderBy.Add(OrderBy1);

            NeuObject OrderBy2 = new NeuObject();
            OrderBy2.ID = "DESC";
            OrderBy2.Name = "降序";
            arrOrderBy.Add(OrderBy2);

            dropOrderBy.AddItems(arrOrderBy);

        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        #region 把控件中字段、和排序顺序看作对象
        public class UserOrderObject
        {
            public string dropUserOrder = string.Empty;
            public string dropUserOrderBy = string.Empty;
        }

        private void GetUserOrderObject()
        {

            UserOrderOjectTemp.dropUserOrder = string.IsNullOrEmpty(dropOrderField.Tag.ToString()) == true ? "" : dropOrderField.Tag.ToString();
            UserOrderOjectTemp.dropUserOrderBy = string.IsNullOrEmpty(dropOrderBy.Tag.ToString()) == true ? "" : dropOrderBy.Tag.ToString();

        }
        #endregion
    }
}
