using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Account.Controls.IBorn
{

    public delegate void DelegateRtnSelectedItem(ItemMedical itemMediacl);

    public partial class ucItemMedicalSelector : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        /// <summary>
        /// 双击返回当前选中套餐
        /// </summary>
        public event DelegateRtnSelectedItem RtnSelectedItem;

        /// <summary>
        /// 数据集
        /// </summary>
        private System.Data.DataView packageDataView;


        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();


        public ucItemMedicalSelector()
        {
            InitializeComponent();
            InitFP();
        }

        /// <summary>
        /// 初始化FP
        /// </summary>
        void InitFP()
        {
            this.fpPackage.CellDoubleClick += new CellClickEventHandler(fpPackage_CellDoubleClick);
            this.lblClose.Click += new EventHandler(lblClose_Click);
        }


        void lblClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>
        /// 初始化项目列表
        /// </summary>
        public void Init()
        {
            List<ItemMedical> packageList = new List<ItemMedical>();
            packageList = this.accountManager.QueryAllItemMedical("1");
            this.packageDataView = new DataView(this.CreateDataSet(packageList).Tables[0]);
            this.fpPackage_Sheet1.DataSource = this.packageDataView;
        }


        protected virtual DataSet CreateDataSet(List<ItemMedical> al)
        {
            DataSet dataSet = new DataSet();
            dataSet.EnforceConstraints = false;//是否遵循约束规则

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");

            DataTable dtMain;
            dtMain = dataSet.Tables.Add("Table");
            dtMain.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("编码",dtStr),//0
                new DataColumn("名称", dtStr),//1
                new DataColumn("金额", dtStr),//2
                new DataColumn("拼音码",dtStr),//3
                new DataColumn("输入码",dtStr) //4
             
            });

            if (al == null)
            {
                return dataSet;
            }

            for (int i = 0; i < al.Count; i++)
            {
                if (al[i].GetType() == typeof(ItemMedical))
                {
                    ItemMedical obj;
                    obj = (ItemMedical)al[i];
                    if (obj.ValidState == "1")
                    {
                        dtMain.Rows.Add(new object[]{
                        obj.PackageId,
                        obj.PackageName,
                        obj.PackageCost,
                        obj.SpellCode,
                        obj.InputCode
                       
                        });
                    }
                }
            }

            return dataSet;
        }


        /// <summary>
        /// 双击选中项目包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackage_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            Row activeRow = this.fpPackage_Sheet1.ActiveRow;
            if (activeRow != null)
            {
                string packageID = this.fpPackage_Sheet1.Cells[activeRow.Index, 0].Value.ToString();

                ItemMedical itemmedical = this.accountManager.QueryAllItemMedicalById(packageID);
                this.RtnSelectedItem(itemmedical);
            }
        }


        public void Filter(string str, string strclass, string parent)
        {
            string sInput = string.Empty;
            //取输入码
            string[] spChar = new string[] { "@", "#", "$", "%", "^", "&", "[", "]", "|" };
            string queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(str.Trim(), spChar);
            string queryCodeclass = FS.FrameWork.Public.String.TakeOffSpecialChar(strclass.Trim(), spChar);
            string queryCodeparent = FS.FrameWork.Public.String.TakeOffSpecialChar(parent.Trim(), spChar);
            queryCode = queryCode.Replace("*", "[*]");
            queryCodeclass = queryCodeclass.Replace("*", "[*]");
            queryCodeparent = queryCodeparent.Replace("*", "[*]");
            sInput = "((编码 like '%{0}%' or 名称 like '%{0}%' or 输入码 like '%{0}%' or 拼音码 like '%{0}%'))";
            sInput = string.Format(sInput, queryCode, queryCodeclass, queryCodeparent);
            packageDataView.RowFilter = sInput;
        }
    }
}
