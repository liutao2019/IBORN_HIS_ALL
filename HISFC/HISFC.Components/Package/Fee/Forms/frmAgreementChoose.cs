using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace HISFC.Components.Package.Fee.Forms
{
    public delegate int DelegateArrayListSet(ArrayList agreementArray);

    public partial class frmAgreementChoose : Form
    {
        #region 属性
        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        #endregion

        #region 业务类
        /// <summary>
        /// 合同查询类
        /// </summary>
        private GZ.BizLogic.Delivery.AgreementManager.AgreementBLL agreementMgr = new GZ.BizLogic.Delivery.AgreementManager.AgreementBLL();

        /// <summary>
        /// 套餐管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Package();

        /// <summary>
        /// 套餐维护业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        #region 变量

        /// <summary>
        /// 设置支付方式
        /// </summary>
        public event DelegateArrayListSet SetArrayListRes;


        /// <summary>
        /// 套餐类别
        /// </summary>
        private ArrayList categoryList = new ArrayList();

        #endregion

        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetAgreementInfo();
            }

        }

        /// <summary>
        /// 选择已签订合同的项目
        /// </summary>
        public frmAgreementChoose()
        {
            InitializeComponent();

            categoryList = null;
            this.categoryList = constantMgr.GetList("PACKAGETYPE");

            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.fpAgreement.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpAgreement_CellClick);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ArrayList agreementList = this.getAgreementList();

            if (agreementList.Count == 0)
            {
                MessageBox.Show("请选择条目！");
                return;
            }

            if(this.SetArrayListRes(agreementList) < 0)
            {
                return;
            }

            this.Close();
        }

        /// <summary>
        /// 列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpAgreement_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 0)
                return;

            this.fpAgreement_Sheet1.SetActiveCell(e.Row, 0);
            bool value = (bool)this.fpAgreement_Sheet1.Cells[e.Row, (int)AgreementCols.Check].Value;
            this.fpAgreement_Sheet1.Cells[e.Row, (int)AgreementCols.Check].Value = !value;
        }

        /// <summary>
        /// 查询和设置合同显示
        /// </summary>
        private void SetAgreementInfo()
        {
            this.fpAgreement_Sheet1.RowCount = 0;
            //查询签订了合同但未收费的合同条目
            List<GZ.Model.Agreement> agreementList =  this.agreementMgr.GetAgreementByPatientID(this.patientInfo.PID.CardNO, "2");

            if (agreementList == null || agreementList.Count == 0)
            {
                return;
            }
            //{CCA8788A-A686-4309-81AA-3EB4D74EBCE4} 更改合同为显示状态
            foreach (GZ.Model.Agreement model in agreementList)
            {

                //FS.HISFC.Models.MedicalPackage.Package package = packageMgr.QueryPackageByID(model.PackageContext);
                //package.User01 = this.packageProcess.QueryTotFeeByPackge(package.ID).ToString();
                //package.User02 = getKind(package.PackageType);
                //package.User03 = getRange(package.UserType);

                this.fpAgreement_Sheet1.RowCount += 1;
                this.fpAgreement_Sheet1.Rows[this.fpAgreement_Sheet1.RowCount-1].Tag = model;
                this.fpAgreement_Sheet1.Cells[this.fpAgreement_Sheet1.RowCount-1,(int)AgreementCols.Check].Value = false;
                this.fpAgreement_Sheet1.Cells[this.fpAgreement_Sheet1.RowCount - 1, (int)AgreementCols.Name].Text = model .PackageName;
                this.fpAgreement_Sheet1.Cells[this.fpAgreement_Sheet1.RowCount - 1, (int)AgreementCols.Name].Locked = true;
                this.fpAgreement_Sheet1.Cells[this.fpAgreement_Sheet1.RowCount - 1, (int)AgreementCols.OrgPrice].Value = model.PackageMoney;
                this.fpAgreement_Sheet1.Cells[this.fpAgreement_Sheet1.RowCount - 1, (int)AgreementCols.OrgPrice].Locked = true;
                this.fpAgreement_Sheet1.Cells[this.fpAgreement_Sheet1.RowCount - 1, (int)AgreementCols.RealPrice].Value = model.AgreementMoney;
                this.fpAgreement_Sheet1.Cells[this.fpAgreement_Sheet1.RowCount - 1, (int)AgreementCols.RealPrice].Locked = true;
                this.fpAgreement_Sheet1.Cells[this.fpAgreement_Sheet1.RowCount - 1, (int)AgreementCols.AgreementDate].Text = model.OperDate.ToString();
                this.fpAgreement_Sheet1.Cells[this.fpAgreement_Sheet1.RowCount - 1, (int)AgreementCols.AgreementDate].Locked = true;
            }
        }

        private ArrayList getAgreementList()
        {
            ArrayList agreementList = new ArrayList();
            foreach (Row row in this.fpAgreement_Sheet1.Rows)
            {
                if ((bool)this.fpAgreement_Sheet1.Cells[row.Index, (int)AgreementCols.Check].Value == true)
                {
                    agreementList.Add(row.Tag);
                }
            }

            return agreementList;
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        public void Clear()
        {
            this.fpAgreement_Sheet1.RowCount = 0;
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


        /// <summary>
        /// 列枚举
        /// </summary>
        private enum AgreementCols
        {
            /// <summary>
            /// 勾选框
            /// </summary>
            Check = 0,

            /// <summary>
            /// 套餐名称
            /// </summary>
            Name = 1,

            /// <summary>
            /// 原价
            /// </summary>
            OrgPrice = 2,

            /// <summary>
            /// 成交金额
            /// </summary>
            RealPrice = 3,

            /// <summary>
            /// 签订日期
            /// </summary>
            AgreementDate =4
        }
    }
}
