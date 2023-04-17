using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace HISFC.Components.Package.Fee.Controls
{
    public delegate void DelegateRtnSelectedItem(FS.HISFC.Models.MedicalPackage.Package package);

    public partial class ucPackageSelector : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        /// <summary>
        /// 套餐维护业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();
        
        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
        
        /// <summary>
        /// 套餐类别
        /// </summary>
        private ArrayList categoryList = new ArrayList();

        /// <summary>
        /// 双击返回当前选中套餐
        /// </summary>
        public event DelegateRtnSelectedItem RtnSelectedItem;

        /// <summary>
        /// 数据集
        /// </summary>
        private System.Data.DataView packageDataView;

        public ucPackageSelector()
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
        /// 双击选中套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackage_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            Row activeRow = this.fpPackage_Sheet1.ActiveRow;
            if (activeRow != null)
            {
                string packageID = this.fpPackage_Sheet1.Cells[activeRow.Index, 0].Value.ToString();
                FS.HISFC.Models.MedicalPackage.Package package = this.packageProcess.GetPackage(packageID);
                package.User01 = this.packageProcess.QueryTotFeeByPackge(package.ID).ToString();
                package.User02 = getKind(package.PackageType);
                package.User03 = getRange(package.UserType);
                this.RtnSelectedItem(package);
            }
        }

        public void GetCurrentPackage()
        {
            Row activeRow = this.fpPackage_Sheet1.ActiveRow;
            if (activeRow != null)
            {
                string packageID = this.fpPackage_Sheet1.Cells[activeRow.Index, 0].Value.ToString();
                FS.HISFC.Models.MedicalPackage.Package package = this.packageProcess.GetPackage(packageID);
                package.User01 = this.packageProcess.QueryTotFeeByPackge(package.ID).ToString();
                package.User02 = getKind(package.PackageType);
                package.User03 = getRange(package.UserType);
                this.RtnSelectedItem(package);
            }
        }

        public void NextRow()
        {
            if (this.fpPackage_Sheet1.ActiveRowIndex < this.fpPackage_Sheet1.RowCount -1)
            {
                this.fpPackage_Sheet1.SetActiveCell(this.fpPackage_Sheet1.ActiveRowIndex + 1, 0);
            }
        }

        public void PreRow()
        {
            if (this.fpPackage_Sheet1.ActiveRowIndex > 0)
            {
                this.fpPackage_Sheet1.SetActiveCell(this.fpPackage_Sheet1.ActiveRowIndex - 1, 0);
            }
        }

        /// <summary>
        /// 初始化项目列表
        /// </summary>
        public void Init()
        {
            categoryList = null;
            this.categoryList = constantMgr.GetList("PACKAGETYPE");

            ArrayList packageList = new ArrayList();
            packageList = this.packageProcess.GetPackageListByType("ALL");
            this.packageDataView = new DataView(this.CreateDataSet(packageList).Tables[0]);
            this.fpPackage_Sheet1.DataSource = this.packageDataView;
        }

        protected virtual DataSet CreateDataSet(ArrayList al)
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
                new DataColumn("类型",dtStr),//3
                new DataColumn("范围", dtStr),//4
                new DataColumn("拼音码",dtStr),//5
                new DataColumn("输入码",dtStr), //6
                new DataColumn("套餐级别",dtStr), //7
                new DataColumn("父套餐编码",dtStr) //8
            });

            if (al == null)
            {
                return dataSet;
            }

            for (int i = 0; i < al.Count; i++)
            {
                if (al[i].GetType() == typeof(FS.HISFC.Models.MedicalPackage.Package))
                {
                    FS.HISFC.Models.MedicalPackage.Package obj;
                    obj = (FS.HISFC.Models.MedicalPackage.Package)al[i];
                    //{42429E90-70AB-4a1d-B94F-6CCE1A4D83CE}
                    //obj.User01 = this.packageProcess.QueryTotFeeByPackge(obj.ID).ToString();
                    //obj.User02 = getKind(obj.PackageType);
                    obj.User03 = getRange(obj.UserType);
                    if (obj.IsValid)
                    {
                        dtMain.Rows.Add(new object[]{
                        obj.ID,
                        obj.Name,
                        obj.User01,
                        obj.User02,
                        obj.User03,
                        obj.SpellCode,
                        obj.UserCode,
                        obj.PackageClass,
                        obj.ParentCode
                        });
                    }
                }
            }

            this.fpPackage_Sheet1.Columns[7].Visible = false;
            this.fpPackage_Sheet1.Columns[8].Visible = false;

            return dataSet;
        }

        private string getKind(string kindID)
        {
            foreach (FS.HISFC.Models.Base.Const cst in categoryList)
            {
                if (kindID == cst.ID)
                {
                    return cst.Name;
                }
            }

            return string.Empty;
        }

        private string getRange(FS.HISFC.Models.Base.ServiceTypes type)
        {
            string rtn = string.Empty;
            switch (type)
            {
                case FS.HISFC.Models.Base.ServiceTypes.C:
                    rtn = "门诊";
                    break;
                case FS.HISFC.Models.Base.ServiceTypes.I:
                    rtn = "住院";
                    break;
                case FS.HISFC.Models.Base.ServiceTypes.T:
                    rtn = "体检";
                    break;
                case FS.HISFC.Models.Base.ServiceTypes.A:
                    rtn = "全部";
                    break;
                default:
                    break;
            }

            return rtn;
        }

        public void Filter(string str,string strclass,string parent)
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

            sInput = "((编码 like '%{0}%' or 名称 like '%{0}%' or 输入码 like '%{0}%' or 拼音码 like '%{0}%') and ( 套餐级别 = '{1}') and ( 父套餐编码 = '{2}' or 'ALL' = '{2}'))";
            sInput = string.Format(sInput, queryCode, queryCodeclass, queryCodeparent);

            packageDataView.RowFilter = sInput;
        }

    }
}
