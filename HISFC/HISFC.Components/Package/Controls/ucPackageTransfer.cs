using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GZ.Components.Bespeak;
using System.Collections;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Components.OutpatientFee.Controls;
using FS.HISFC.Models.MedicalPackage.Fee;
using FS.SOC.HISFC.BizProcess.CommonInterface;


namespace HISFC.Components.Package.Controls
{
    public partial class ucPackageTransfer : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// {727B4075-258A-4F11-A757-D8F0AEF08380} 套餐核销
        /// </summary>
        public ucPackageTransfer()
        {
           
            InitializeComponent();
            detailVisible = true;
            //if (!CommonController.CreateInstance().JugePrive("0820", "28"))
            //{
            //    MessageBox.Show("您没有套餐核销权限，如需开通请走企业微信申请！");
            //    // CommonController.CreateInstance().MessageBox("您没有退其他操作员收费记录的权限，操作已取消，该费用的操作员是：" + CommonController.CreateInstance().GetEmployeeName(invoiceTemp.BalanceOper.ID), MessageBoxIcon.Warning);
            //    this.FindForm().Close();
            //    return;
            //}
        }


        #region 属性

        /// <summary>
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetPatientInfo();
                this.SetPackageInfo();
            }
        }

        /// <summary>
        /// 当前会员卡信息
        /// </summary>
        private FS.HISFC.Models.Account.AccountCard accountCardInfo = null;

       
        /// <summary>
        /// 合同列表
        /// </summary>
        private ArrayList pactList = null;

        /// <summary>
        /// 性别
        /// </summary>
        private ArrayList sexList = null;

        /// <summary>
        /// 证件类型
        /// </summary>
        private ArrayList IDTypeList = null;

        /// <summary>
        /// 会员等级
        /// </summary>
        private ArrayList memberLevel = null;

        public bool detailVisible = false;

        //{0082764E-FE0E-467a-86D6-AC0762396803}
        public bool DetailVisible
        {
            get
            {
                return this.detailVisible;
            }
            set
            {
                detailVisible = value;
                //this.pnlPackageDetail.Visible = true;
                //this.detailVisible = true;
            }
        }

        public string PackageIDS { set; get; }

        public string ClinicCodes { set; get; }

        ucInvoicePreview cp = null;

        /// <summary>
        /// 核销 开单医生
        /// </summary>
        public string DoctCode { set; get; }

        /// <summary>
        /// 核销 开单科室
        /// </summary>
        public string DoctDeptCode { set; get; }

        #endregion

        #region 业务类

        /// <summary>
        /// 账户管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 合同单位业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 套餐购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.Package pckMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
        /// <summary>
        /// 套餐购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail pckdMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();
        /// <summary>
        /// 套餐理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Package();
        /// <summary>
        /// 非药品
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 药品
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        protected FS.HISFC.BizProcess.Integrate.Registration.Registration regManagement = new FS.HISFC.BizProcess.Integrate.Registration.Registration();


        protected FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
        #endregion 

        ArrayList packageList = null;


        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox queryControl = sender as TextBox;
                string QueryStr = queryControl.Text.Trim();

                //bool result = Regex.IsMatch(txtCardNO.Text, "^([0-9]{1,})$");
                //if (result)
                //{
                //    result =Regex .IsMatch (txtCardNO .Text ,@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$ ");
                //}
                ////病历号默认进行补全
                if ((sender as TextBox).Name == "txtCardNO")
                {
                    queryPatientList(this.txtCardNO.Text);
                    // GetPatient();
                }
                SetPackageInfo();
            }
        }

        /// <summary>
        ///  根据条件查询人员信息 //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} 修改综合查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private bool queryPatientList(string condition)
        {
           
            Form patientForm = new Form();
            ucPatientList patientList = new ucPatientList();
            patientForm.Size = patientList.Size;
            patientForm.Controls.Add(patientList);
            patientList.QueryCondition = condition;
            patientForm.StartPosition = FormStartPosition.Manual;
            patientForm.Location = new Point(PointToScreen(this.txtCardNO.Location).X, PointToScreen(this.txtCardNO.Location).Y + this.txtCardNO.Height * 2);
            patientForm.FormBorderStyle = FormBorderStyle.None;
            patientForm.ShowInTaskbar = false;
            patientList.patientInfo = patientInfoSet;

            if (patientList.patientList != null && patientList.patientList.Count > 0)
            {
                patientForm.ShowDialog();
                return true;
            }
            else
            {
                MessageBox.Show("没有该患者信息！");
                // tbName.Text = patient.Name;
            }

            return false;
        }

        /// <summary>
        ///根据选择的人员信息设置综合查询信息  //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} 修改综合查询
        /// </summary>
        /// <param name="patient"></param>
        private void patientInfoSet(PatientInfo patient)
        {
            this.PatientInfo = patient;
           // SetPatientInfo();
        }

     

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                this.tbMedicalNO.Text = string.Empty;
                this.tbName.Text = string.Empty;
                this.tbCardType.Text = string.Empty;
                this.tbIDNO.Text = string.Empty;
                this.tbSex.Text = string.Empty;
                this.tbAge.Text = string.Empty;
                this.tbLevel.Tag = string.Empty;
                this.tbPhone.Text = string.Empty;
                this.tbPact.Tag = string.Empty;

                return;
            }


            System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountMgr.GetMarkList(this.PatientInfo.PID.CardNO, "ALL", "1");
            if (cardList == null || cardList.Count < 1)
            {
                MessageBox.Show("病历号不存在！");
                return;
            }
            this.accountCardInfo = cardList[cardList.Count - 1];

            this.tbMedicalNO.Text = this.accountCardInfo.Patient.PID.CardNO;
            this.tbName.Text = patientInfo.Name;
            this.tbCardType.Text = this.QueryNameByIDFromDictionnary(this.IDTypeList, this.patientInfo.IDCardType.ID);
            this.tbIDNO.Text = this.patientInfo.IDCard;
            this.tbSex.Text = this.QueryNameByIDFromDictionnary(this.sexList, patientInfo.Sex.ID.ToString());
            this.tbAge.Text = this.accountMgr.GetAge(patientInfo.Birthday);
            this.tbLevel.Text = this.QueryNameByIDFromDictionnary(this.memberLevel, this.accountCardInfo.AccountLevel.ID.ToString());
            this.tbPhone.Text = this.patientInfo.PhoneHome;
            this.tbPact.Text = this.QueryNameByIDFromDictionnary(this.pactList, this.patientInfo.Pact.ID);
        }

        private string QueryNameByIDFromDictionnary(ArrayList al, string ID)
        {
            try
            {
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    if (obj.ID == ID)
                    {
                        return obj.Name;
                    }
                }
            }
            catch
            { }
            return string.Empty;
        }

        private void cmbdept_SelectedValueChanged(object sender, EventArgs e)
        {
            #region
            //    // MessageBox.Show(cmbdept.SelectedText + "}}" + cmbdept.Text);
        //    if (packageList != null)
        //    {
        //        //{809C32E9-110F-4de6-BE09-2BB85ABAEFDE}
        //        //FS.FrameWork.Models.NeuObject obj = constantMgr.GetConstant("DEPTPACKAGE", cmbdept.Tag.ToString());//cmbdept.Tag as FS.FrameWork.Models.NeuObject;
        //        //if (string.IsNullOrEmpty(obj.Memo))
        //        //{
        //        //    MessageBox.Show("科室套餐配置出错！请联系信息科");
        //        //    return;
        //        //}
        //        // string[] arr = obj.Memo.Split('|');
        //        ArrayList pList = new ArrayList();
        //        foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
        //        {
        //            //foreach (string packagetype in arr)
        //            // {
        //            //if (package.PackageInfo.PackageType == packagetype)
        //            //{
        //            // pList.Add(package);
        //            // break;
        //            //}
        //            //  }
        //            string dept = cmbdept.Tag.ToString();
        //            if (package.Package_Dept.ToString() == dept)
        //            {
        //                pList.Add(package);
        //            }

        //        }
        //        if (pList.Count > 0)
        //        {
        //            ShowPackage(pList);
        //            fpPackageDetail_Sheet1.RowCount = 0;
        //        }
        //        else
        //        {
        //            fpPackageDetail_Sheet1.RowCount = 0;
        //            fpPackage_Sheet1.RowCount = 0;
        //        }

        //    }
        //    else
        //    {
        //        SetPackageInfo();
        //    }
#endregion

        }


        /// <summary>
        /// 设置套餐信息
        /// </summary>
        private void SetPackageInfo()
        {
            fpPackage_Sheet1.RowCount = 0;
            if (this.PatientInfo == null) return;
            packageList = this.pckMgr.QueryByCardNO1(this.PatientInfo.PID.CardNO, "1", "0");
            if (packageList == null)
            {
                return;
            }

            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

            //{809C32E9-110F-4de6-BE09-2BB85ABAEFDE}
            //FS.FrameWork.Models.NeuObject obj = constantMgr.GetConstant("DEPTPACKAGE", cmbdept.Tag.ToString());//cmbdept.Tag as FS.FrameWork.Models.NeuObject;
            //if (string.IsNullOrEmpty(obj.Memo))
            //{
            //    MessageBox.Show("科室套餐配置出错！请联系信息科");
            //    return;
            //}
            //string[] arr = obj.Memo.Split('|');
            ArrayList pList = new ArrayList();
            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
            {
                //foreach (string packagetype in arr)
                //{
                //    if (package.PackageInfo.PackageType == packagetype)
                //    {
                //        pList.Add(package);
                //        break;
                //    }
                //}
                string dept = cmbdept2.Tag.ToString();
                if (dept == "ALL")
                {
                    pList.Add(package);
                }
                else
                {
                    if (package.Package_Dept.ToString() == dept)
                    {
                        pList.Add(package);
                    }
                }
            }
            if (pList.Count > 0)
            {
                ShowPackage(pList);
                fpPackageDetail_Sheet1.RowCount = 0;
            }
            else
            {
                fpPackage_Sheet1.RowCount = 0;
                fpPackageDetail_Sheet1.RowCount = 0;
            }

           // this.fpPackage_SelectionChanged(null, null);
        }

        private void ShowPackage(ArrayList pList)
        {
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpPackageDetail_Sheet1.RowCount = 0;

            if (this.patientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                return;
            }
            //{84D474E6-6590-472b-9D6C-8DC90000A16C}  更改挂号查询只能查询未使用套餐

            string packageIDs = string.Empty;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in pList)
            {
               // packageIDs += "'" + package.ID + "',";
               // MessageBox.Show(package.PackageInfo.PackageType + "||" + package.PackageInfo.ParentCode);
                //package.PackageInfo = this.packageMgr.QueryPackageByID(package.PackageInfo.ID);
                this.fpPackage_Sheet1.AddRows(this.fpPackage_Sheet1.RowCount, 1);
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.option].Text = "false";
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.Name].Text = package.PackageInfo.Name;
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.TotCost].Text = package.Package_Cost.ToString("F2");
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.RealCost].Text = package.Real_Cost.ToString("F2");
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.GiftCost].Text = package.Gift_cost.ToString("F2");
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.EtcCost].Text = package.Etc_cost.ToString("F2");
                //this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.operTime].Text = package.OperTime.ToString();
                //{E03501D7-CCC8-4d11-AA15-663C6B6FA393}添加购买日期
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.operTime].Text = package.OperTime.ToString();
                //ClinicCodes += package.ID + ",";
                this.fpPackage_Sheet1.Rows[this.fpPackage_Sheet1.RowCount - 1].Tag = package;
            }
            if (packageIDs.Length > 0)
            {
                //PackageIDS = packageIDs = packageIDs.Substring(0, packageIDs.Length - 1);
                //ClinicCodes = ClinicCodes.Substring(0, ClinicCodes.Length - 1);
                //SetPackageDetail(packageIDs);
            }
           // FilterPackage();
        }

        

        /// <summary>
        /// 套餐选择触发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackage_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.fpPackage_Sheet1.ActiveRow == null)
            {
                this.fpPackageDetail_Sheet1.RowCount = 0;
                return;
            }
            ClinicCodes = PackageIDS = string.Empty;
            if (this.fpPackage_Sheet1.ActiveCell.Column.Index == 0)
            {
                string packageIDs = string.Empty;
                for (int i = 0; i < fpPackage_Sheet1.Rows.Count; i++)
                {
                    if (this.fpPackage_Sheet1.Cells[i, (int)PackageCols.option].Text.ToLower() == "true")
                    {
                        ClinicCodes += (this.fpPackage_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package).ID + ",";
                        packageIDs += "'" + (this.fpPackage_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package).ID + "',";
                    }
                }
                if (packageIDs.Length > 0)
                {
                    ClinicCodes = ClinicCodes.Substring(0, ClinicCodes.Length - 1);
                    PackageIDS = packageIDs = packageIDs.Substring(0, packageIDs.Length - 1);
                    this.SetPackageDetail(packageIDs);
                }
                else
                {
                    fpPackageDetail_Sheet1.RowCount = 0;
                }
            }
           
            //FS.HISFC.Models.MedicalPackage.Fee.Package selectPackage = this.fpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
            
        }

       


        /// <summary>
        /// 设置套餐明细
        /// </summary>
        private void SetPackageDetail(string packageIds)
        {
            this.fpPackageDetail_Sheet1.RowCount = 0;
            ArrayList details = this.pckdMgr.QueryDetailByClinicNOs(packageIds, "0");
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
            {
                //改动detail的获取方式把。7B4075-258A-4F11-A757-D8F0AEF08380} 
                if (detail.ConfirmQTY== detail.Item.Qty)
                {
                    break;
                }
                if (string.IsNullOrEmpty(detail.Item.Name))
                {

                    decimal tmpQTY = detail.Item.Qty;
                    if (detail.Item.ID.Substring(0, 1) == "Y")
                    {
                        detail.Item = itemIntegrate.GetItem(detail.Item.ID);
                    }
                    else
                    {
                        detail.Item = itemMgr.GetUndrugByCode(detail.Item.ID);
                    }

                    detail.Item.Qty = tmpQTY;
                }

                this.fpPackageDetail_Sheet1.AddRows(this.fpPackageDetail_Sheet1.RowCount, 1);
                this.fpPackageDetail_Sheet1.Rows[this.fpPackageDetail_Sheet1.RowCount - 1].Tag = detail;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.option].Text = "true";
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.ItemName].Text = detail.Item.Name + "[" + detail.Item.Specs + "]" + "*" + detail.Item.Qty.ToString() + detail.Unit;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.TotQty].Text = detail.Item.Qty.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Unit].Text = detail.Unit.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.ValiableQty].Text = detail.RtnQTY.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.TotCost].Text = detail.Detail_Cost.ToString("F2");
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.RealCost].Text = detail.Real_Cost.ToString("F2");
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.GiftCost].Text = detail.Gift_cost.ToString("F2");
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.EtcCost].Text = detail.Etc_cost.ToString("F2");
                PackageIDS = packageIds;
            }
        }

      


       

        private void fpPackage_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == (int)PackageCols.option)
            {
                //添加全选{B90963E3-E1DA-45da-AD6B-C2E1B19C4CB0}
                if (e.ColumnHeader)
                {
                    if (fpPackage_Sheet1.ColumnHeader.Columns[(int)PackageCols.option].Label == "全选")
                    {
                        for (int i = 0; i < fpPackage_Sheet1.Rows.Count; i++)
                        {
                            this.fpPackage_Sheet1.Cells[i, (int)PackageCols.option].Text = "true";
                        }
                        fpPackage_Sheet1.ColumnHeader.Columns[(int)PackageCols.option].Label = "取消";
                    }
                    else
                        if (fpPackage_Sheet1.ColumnHeader.Columns[(int)PackageCols.option].Label == "取消")
                    {
                        for (int i = 0; i < fpPackage_Sheet1.Rows.Count; i++)
                        {
                            this.fpPackage_Sheet1.Cells[i, (int)PackageCols.option].Text = "false";
                        }
                        fpPackage_Sheet1.ColumnHeader.Columns[(int)PackageCols.option].Label = "全选";
                    }
                }
                else
                {

                    if (this.fpPackage_Sheet1.Cells[e.Row, (int)PackageCols.option].Text.ToLower() == "true")
                    {
                        this.fpPackage_Sheet1.Cells[e.Row, (int)PackageCols.option].Text = "false";
                    }
                    else
                        fpPackage_Sheet1.Cells[e.Row, (int)PackageCols.option].Text = "true";
                }
            }
        }

        /// <summary>
        /// 套餐核销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {

            Save();
        }

        private void Save()
        {
            if (!CommonController.CreateInstance().JugePrive("0820", "29"))
            {
                MessageBox.Show("您没有套餐核销权限，如需开通请走企业微信申请！");
                // CommonController.CreateInstance().MessageBox("您没有退其他操作员收费记录的权限，操作已取消，该费用的操作员是：" + CommonController.CreateInstance().GetEmployeeName(invoiceTemp.BalanceOper.ID), MessageBoxIcon.Warning);
                Form fm = this.FindForm();
                if (fm != null)
                {
                    // fm.Show();
                    fm.Close();
                }
                return;
            }
            if (PatientInfo == null) {
                MessageBox.Show("请选择患者信息！");
                return; 
            }
            if (fpPackageDetail_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("请选择要核销的套餐信息！");
                return; 
            }

            //{a6f13f8f-b30b-408b-99aa-8009e6367533}
            if (MessageBox.Show("是否确认核销？", "确认核销",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }


            //事务启动
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //{C016782D-59EC-4F60-8767-876BAA7E3E08}
            FS.HISFC.Models.Registration.Register reg = ClinicReg();
            if (reg != null && !string.IsNullOrEmpty(reg.ID))
            {

            }
            else
            {
                this.feeIntegrate.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            this.PatientInfo.ID = reg.ID;
            List<FS.HISFC.Models.MedicalPackage.Fee.PackageDetail> listPakcageDetial = new List<FS.HISFC.Models.MedicalPackage.Fee.PackageDetail>();
            ArrayList listFee = GetItemList(reg, listPakcageDetial);

            ArrayList list = GetNoFeeItemList(reg, listPakcageDetial);

            string errText = string.Empty;
            if (!IsItemValid(list))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }

            ///划价信息
            if (!feeIntegrate.SetChargeInfo(reg, list, DateTime.Now, ref errText))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText);
                return;
            }

            list = feeIntegrate.QueryChargedFeeItemListsByClinicNO(reg.ID);
            //获取发票信息
            ArrayList balancesAndBalanceLists = GetInvionInfo(reg, list);

            ArrayList alInvoice = (ArrayList)balancesAndBalanceLists[0];
            ArrayList alInvoiceDetial = (ArrayList)balancesAndBalanceLists[1];
            ArrayList alInvoiceFeeDetial = (ArrayList)balancesAndBalanceLists[2];

            Balance balance = alInvoice[0] as Balance;
            ArrayList balancePays = new ArrayList();
            int i = 1;

            decimal ecoSum = 0;
            decimal donateCost = 0;
            decimal owncost = 0;
            for (int j = 0; j < listFee.Count; j++)
            {
                FeeItemList f = listFee[j] as FeeItemList;
                ecoSum += f.FT.RebateCost;
                donateCost += f.FT.DonateCost;
                owncost += f.FT.OwnCost;
            }
            //套餐实收
            BalancePay balancePay = new BalancePay();
            balancePay.Squence = i.ToString();
            balancePay.Invoice = balance.Invoice;
            balancePay.PayType.ID = "PR"; //new FS.FrameWork.Models.NeuObject();
            balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            balancePay.Memo = "财务套餐核销";
            balancePay.FT.TotCost = balancePay.FT.RealCost = balance.FT.RealCost = owncost - ecoSum - donateCost;
            balancePay.UsualObject = listPakcageDetial;
            balancePays.Add(balancePay);


            if (ecoSum > 0)
            {
                i++;
                //套餐优惠
                BalancePay balancePay1 = new BalancePay();
                balancePay1.Squence = i.ToString();
                balancePay1.Invoice = balance.Invoice;
                balancePay1.PayType.ID = "PY"; //new FS.FrameWork.Models.NeuObject();
                balancePay1.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePay1.Memo = "财务套餐核销";
                balancePay1.FT.TotCost = balancePay1.FT.RealCost = ecoSum; //balance.FT.RebateCost;
                balancePays.Add(balancePay1);

            }

            if (donateCost > 0)
            {
                i++;
                //套餐赠送
                BalancePay balancePay2 = new BalancePay();
                balancePay2.Squence = i.ToString();
                balancePay2.Invoice = balance.Invoice;
                balancePay2.PayType.ID = "PD"; //new FS.FrameWork.Models.NeuObject();
                balancePay2.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePay2.Memo = "财务套餐核销";
                balancePay2.FT.TotCost = balancePay2.FT.RealCost = donateCost; //balance.FT.DonateCost;
                balancePays.Add(balancePay2);
            }
            this.feeIntegrate.IsNeedUpdateInvoiceNO = true;

            bool returnValue = this.feeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, false, reg,
               alInvoice, alInvoiceDetial, list, alInvoiceFeeDetial, balancePays, ref errText);

            if (!returnValue)
            {
                this.feeIntegrate.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (errText != "")
                {
                    MessageBox.Show(errText);
                }

                return;
            }
            ////核销套餐消耗状态
            //FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageBll = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
            //int ret = pckMgr.UpdatePackageCostFlag(PackageIDS);
            //if (ret < 0)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show("更新套餐核销状态错误！" + pckMgr.Err);
            //    return;
            //}
            //添加核销记录7B4075-258A-4F11-A757-D8F0AEF08380} 
            if (!string.IsNullOrEmpty(ClinicCodes))
            {
                string[] clinicArr = ClinicCodes.Split(',');
                HISFC.BizLogic.MedicalPackage.Fee.PackageWriteOffBLL bll = new HISFC.BizLogic.MedicalPackage.Fee.PackageWriteOffBLL();
                foreach (string clinic in clinicArr)
                {
                    PackageWriteOff model = new PackageWriteOff();
                    model.RealCost = listPakcageDetial.Where(t => t.ID == clinic).Sum(t => t.Real_Cost);
                    model.Clinic_Code = clinic;
                    model.Card_NO = patientInfo.PID.CardNO;
                    model.InvoiceNO = (balancePays[0] as BalancePay).Invoice.ID;
                    if (bll.Insert(model) < 0)
                    {
                        this.feeIntegrate.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新套餐核销记录错误！" + bll.Err);
                        return;
                    }

                }
            }

            this.feeIntegrate.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();

            if (returnValue)
            {
                MessageBox.Show("套餐核销成功！");
                //核销套餐消耗状态7B4075-258A-4F11-A757-D8F0AEF08380} 
                FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageBll = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
                string[] packageidArr = ClinicCodes.Split(',');
                foreach (string pid in packageidArr)
                {
                    ArrayList detailsNocost = this.pckdMgr.QueryDetailByClinicNOsNoCost(pid, "0");
                    if (detailsNocost.Count == 0)
                    {
                        int ret = pckMgr.UpdatePackageCostFlag(pid);
                        if (ret < 0)
                        {
                           
                            MessageBox.Show("更新套餐核销状态错误！" + pckMgr.Err);
                            return;
                        }
                    }
                }
                fpPackageDetail_Sheet1.RowCount = 0;
                SetPackageInfo();
                cp.Clear();
            }
            else
            {
                MessageBox.Show(errText);
            }
        }


        protected override int OnSave(object sender, object neuObject)
        {
            Save();  
            return base.OnSave(sender, neuObject);
        }
         

        /// <summary>
        /// 挂号
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register ClinicReg()
        {
            
            //string regclinic = string.Empty;
            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
            if (PatientInfo != null)
            {
                register.PID.CardNO = PatientInfo.PID.CardNO;
                register.Name = PatientInfo.Name;

                //{72502FD5-FB9B-4d2a-9309-DC85BC5AA575}
                //部分外国客户没有身份证信息，idcard为空导致报错
                if (PatientInfo.IDCard == null)
                {
                    register.IDCard = "";
                }
                else if (PatientInfo.IDCard != null)
                {
                    register.IDCard = PatientInfo.IDCard.Trim(); 
                }
                register.IDCardType = PatientInfo.IDCardType;
                register.Sex = PatientInfo.Sex;
                register.Pact = PatientInfo.Pact;
                if (patientInfo.Pact.PayKind.ID == "")
                {
                    register.Pact.PayKind.ID = "01";
                }
                register.PhoneHome = PatientInfo.PhoneHome;
                register.AddressHome = PatientInfo.AddressHome;
                register.Birthday = PatientInfo.Birthday;
                register.DoctorInfo.SeeDate = DateTime .Now;    //挂号时间为当前时间
                // register.DoctorInfo = new Schema();
                register.DoctorInfo.Templet.Doct = new FS.FrameWork.Models.NeuObject();
                DoctCode = "00T002";
                register.DoctorInfo.Templet.Doct.ID = DoctCode;
                register.DoctorInfo.Templet.Doct.Name = "未指定";
                register.DoctorInfo.Templet.Dept = new FS.FrameWork.Models.NeuObject();
                register.DoctorInfo.Templet.Dept.ID = cmbdept.Tag.ToString ();
                register.DoctorInfo.Templet.Dept.Name = cmbdept.Text;
                register.InputOper.ID = "CWHX";
                register.InputOper.Name = "财务核销";
                register.Memo = "财务核销";
                FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
                register.ID = regMgr.GetSequence("Registration.Register.ClinicID");
                
               
                int iReturn = regManagement.Insert(register);
                //return register;
                if (iReturn <= 0)
                {
                    MessageBox.Show("挂号失败！"+regManagement.Err);
                    return null;
                }
                else
                {
                    PatientInfo.ID = register.ID;
                    register = regManagement.GetByClinic(register.ID);
                }

            }
            return register;
        }

        /// <summary>
        /// 获取费用明细7B4075-258A-4F11-A757-D8F0AEF08380} 
        /// </summary>
        private ArrayList GetItemList(FS.HISFC.Models.Registration.Register reg, List<FS.HISFC.Models.MedicalPackage.Fee.PackageDetail> list)
        {
            ArrayList feeItemLists = new ArrayList();
            for (int i = 0; i < fpPackageDetail_Sheet1.Rows.Count; i++)
            {
                if (this.fpPackageDetail_Sheet1.Rows[i].Tag == null)
                {
                    continue;
                }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                if (fpPackageDetail_Sheet1.Cells[i,(int)DetailCols.option].Text.ToLower()!= "true")
                {
                    continue; 
                }
                FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail = this.fpPackageDetail_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                if (detail != null)
                {
                    FeeItemList f = new FeeItemList();
                    FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                    //物质信息
                    FS.HISFC.Models.Fee.Item.Undrug undrugItem = itemMgr.GetItemByUndrugCode(detail.Item.ID);
                    f.Item = undrugItem;
                    f.Item.Qty = detail.RtnQTY;

                    //费用信息
                    //精度调整，出现decimal28位为1的状态{406FB40F-758E-4377-920D-6243D4706E47}
                    f.FT.TotCost = Math.Round((100 * detail.RtnQTY / (detail.ConfirmQTY + detail.RtnQTY)) * detail.Detail_Cost/100,5);

                    f.FT.RealCost = Math.Round((100 * detail.RtnQTY / (detail.ConfirmQTY + detail.RtnQTY)) * detail.Real_Cost / 100, 5);

                    f.FT.RebateCost = Math.Round((100 * detail.RtnQTY / (detail.ConfirmQTY + detail.RtnQTY)) * detail.Etc_cost/100,5);
                    f.FT.DonateCost = Math.Round((100 * detail.RtnQTY / (detail.ConfirmQTY + detail.RtnQTY)) * detail.Gift_cost / 100, 5);
                    f.FT.OwnCost = f.FT.TotCost;
                    f.FT.PubCost = 0;
                    //f.FT.
                    //开立信息
                    f.DoctDeptInfo.ID = cmbdept.Tag.ToString ();
                    f.DoctDeptInfo.Name = cmbdept.Text;
                    f.RecipeOper.ID = DoctCode;
                    f.RecipeOper.Dept.ID = cmbdept.Tag.ToString ();
                    f.RecipeOper.Dept.Name = cmbdept.Text;
                  
                    //划价信息
                    f.ChargeOper.ID = "CWHX";
                    f.ChargeOper.OperTime = DateTime.Now;
                    //string execDept = this.GetExecDeptByFeeWindow(f);
                    //if (execDept != null)
                    //    f.ExecOper.Dept.ID = execDept;
                    //else 
                        f.ExecOper.Dept.ID = cmbdept.Tag.ToString ();
                    f.ExecOper.Dept.Name = cmbdept.Text;
                    f.Patient = reg;
                    f.IsPackage = "1";
                    f.Days = 1;
                    f.FTSource = "4";
                    f.Memo = "财务核销";
                    detail.Item.Qty = detail.RtnQTY;
                    detail.Memo = "财务核销";
                    list.Add(detail);
                    // f.ExecOper.Dept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(execDept);
                    feeItemLists.Add(f);


                }
            }
            return feeItemLists;
        }

        private ArrayList GetNoFeeItemList(FS.HISFC.Models.Registration.Register reg, List<FS.HISFC.Models.MedicalPackage.Fee.PackageDetail> list)
        {
            ArrayList feeItemLists = new ArrayList();
            for (int i = 0; i < fpPackageDetail_Sheet1.Rows.Count; i++)
            {
                if (this.fpPackageDetail_Sheet1.Rows[i].Tag == null)
                {
                    continue;
                }//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}7B4075-258A-4F11-A757-D8F0AEF08380} 
                if (fpPackageDetail_Sheet1.Cells[i, (int)DetailCols.option].Text.ToLower() != "true")
                {
                    continue;
                }
                FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail = this.fpPackageDetail_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                if (detail != null)
                {
                    FeeItemList f = new FeeItemList();
                    FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                    //物质信息
                    FS.HISFC.Models.Fee.Item.Undrug undrugItem = itemMgr.GetItemByUndrugCode(detail.Item.ID);
                    f.Item = undrugItem;
                    f.Item.Qty = detail.RtnQTY;

                    //费用信息
                    f.FT.TotCost =(100*detail.RtnQTY/(detail.ConfirmQTY+detail.RtnQTY)) *detail.Detail_Cost/100;
                    f.FT.RealCost = (100 * detail.RtnQTY / (detail.ConfirmQTY + detail.RtnQTY)) * detail.Real_Cost/100;
                    //f.FT.RebateCost = detail.Etc_cost;
                    //f.FT.DonateCost = detail.Gift_cost;
                    f.FT.OwnCost =  f.FT.TotCost;
                    //f.FT.PubCost = 0;
                    //f.FT.
                    //开立信息
                    f.DoctDeptInfo.ID = cmbdept.Tag.ToString();
                    f.DoctDeptInfo.Name = cmbdept.Text;
                    f.RecipeOper.ID = DoctCode;
                    f.RecipeOper.Dept.ID = cmbdept.Tag.ToString();
                    f.RecipeOper.Dept.Name = cmbdept.Text;

                    //划价信息
                    f.ChargeOper.ID = "CWHX";
                    f.ChargeOper.OperTime = DateTime.Now;
                    //string execDept = this.GetExecDeptByFeeWindow(f);
                    //if (execDept != null)
                    //    f.ExecOper.Dept.ID = execDept;
                    //else 
                    f.ExecOper.Dept.ID = cmbdept.Tag.ToString();
                    f.ExecOper.Dept.Name = cmbdept.Text;
                    f.Patient = reg;
                    f.IsPackage = "1";
                    f.Days = 1;
                    f.FTSource = "4";
                    f.Memo = "财务核销";
                    detail.Item.Qty = detail.RtnQTY;
                    detail.Memo = "财务核销";
                   // list.Add(detail);
                    // f.ExecOper.Dept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(execDept);
                    feeItemLists.Add(f);


                }
            }
            return feeItemLists;
        }

        public ArrayList GetInvionInfo(FS.HISFC.Models.Registration.Register reg, ArrayList list)
        {
            // string realInvoiceNO =cp.InvoiceNO;//当前显示发票号
            feeIntegrate.IsNeedUpdateInvoiceNO = true;
            string invoiceNO = "";//当前收费发票号
            string realInvoiceNO = cp.InvoiceNO;//当前显示发票号

            FS.HISFC.Models.Base.Employee employee = this.managerIntegrate.GetEmployeeInfo(this.undrugManager.Operator.ID);
            string errText = string.Empty;
            //获得本次收费起始发票号
            int iReturnValue = this.feeIntegrate.GetInvoiceNO(employee, "C", ref invoiceNO, ref realInvoiceNO, ref errText);
            if (iReturnValue == -1)
            {
                MessageBox.Show(errText);

                return null;
            }
            ArrayList balancesAndBalanceLists = FS.HISFC.Components.OutpatientFee.Class.Function.MakeInvoice(this.feeIntegrate, reg, list, invoiceNO, realInvoiceNO, ref errText);

            //********************
            if (balancesAndBalanceLists == null)
            {
                MessageBox.Show(errText);

                return null;
            }

            ArrayList alInvoice = (ArrayList)balancesAndBalanceLists[0];
            if (alInvoice.Count <= 0)
            {
                MessageBox.Show("发票数量为0！");

                return null;
            }
            return balancesAndBalanceLists;
        }

        /// <summary>
        /// 判断最后收费项目是否停用等
        /// </summary>
        /// <param name="feeItemLists">要判断的费用明细</param>
        /// <returns>成功 true 失败 false</returns>
        protected  bool IsItemValid(ArrayList feeItemLists)
        {
            string tmpValue = "0";
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
           
            bool isJudgeValid =controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.STOP_ITEM_WARNNING, false, false);

            if (!isJudgeValid) //如果不需要判断，默认都没有停用
            {
                return true;
            }

            foreach (FeeItemList f in feeItemLists)
            {
                f.Patient = this.PatientInfo;
                if (f.Item.ID == "999")
                {
                    continue;
                }

                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item drugItem = pharmacyIntegrate.GetItem(f.Item.ID);
                    if (drugItem == null)
                    {
                        MessageBox.Show("查询药品项目出错!" + pharmacyIntegrate.Err);

                        return false;
                    }
                    if (drugItem.IsStop)
                    {
                        MessageBox.Show("[" + drugItem.Name + "]已经停用!请验证再收费!");

                        return false;
                    }
                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrugItem = undrugManager.GetUndrugByCode(f.Item.ID);
                    if (undrugItem == null)
                    {
                        MessageBox.Show("查询非药品项目出错!" + undrugManager.Err);

                        return false;
                    }
                    if (undrugItem.ValidState != "1")//停用
                    {
                        MessageBox.Show("[" + undrugItem.Name +"]已经停用或废弃，请验证再收费!");

                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 根据登陆的收费窗口获取取药科室
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private string GetExecDeptByFeeWindow(FeeItemList f)
        {
            string strSql = @"SELECT t.EXE_DEPT
     FROM view_cli_itemlist t
     where (dept_code = '{1}'
     OR dept_code = 'undrug')
     and item_code = '{0}'
     and rownum = 1
     ORDER BY SORT_ID ";
            FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

            strSql = string.Format(strSql, f.Item.ID, ((FS.HISFC.Models.Base.Employee)(accountManager.Operator)).Dept.ID);
            return accountManager.ExecSqlReturnOne(strSql, string.Empty);
        }

        private void ucPackageTransfer_Load(object sender, EventArgs e)
        {
            if (!CommonController.CreateInstance().JugePrive("0820", "29"))
            {
                MessageBox.Show("您没有套餐核销权限，如需开通请走企业微信申请！");
                // CommonController.CreateInstance().MessageBox("您没有退其他操作员收费记录的权限，操作已取消，该费用的操作员是：" + CommonController.CreateInstance().GetEmployeeName(invoiceTemp.BalanceOper.ID), MessageBoxIcon.Warning);
                Form fm = this.FindForm();
                if (fm != null)
                {
                   // fm.Show();
                  //  fm.Close();
                }
                return;
            }
            cp = new ucInvoicePreview();
            pnlBottom.Controls.Add(cp);
            FS.HISFC.Models.Base.Employee emplObj = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;
            cp.IsValidFee = true;

            FS.HISFC.Models.Base.Employee oper = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            if (emplObj.IsManager || emplObj.EmployeeType.ID.ToString() == "F")
            {
                cp.InitInvoice();
            }
            cp.InvoiceUpdated += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(leftControl_InvoiceUpdated);

            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //{809C32E9-110F-4de6-BE09-2BB85ABAEFDE}
           // 初始化证件类型列表
            //ArrayList deptPackageList = constantMgr.GetList("DEPTPACKAGE");
            //if (deptPackageList == null)
            //{
            //    MessageBox.Show("初始化科室套餐列表出错!" + constantMgr.Err);

            //    return ;
            //}
            //初始化套餐科室
            FS.HISFC.Models.Base.Spell spell = null;
            spell = new FS.HISFC.Models.Base.Spell();
            spell.ID = "ALL";
            spell.Name = "全部";
            
            ArrayList dept2list = new ArrayList();
            dept2list.Add(spell);
            ArrayList deptList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
          
            ArrayList alDeptInPatient = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            dept2list.AddRange(deptList);
            dept2list.AddRange(alDeptInPatient);



            cmbdept2.AddItems(dept2list);
            //{26D62184-51A0-40ac-9123-7EDE93A27433}
            //门诊科室
            ArrayList deptPackageList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);

            //护士站{BF6B5B23-F4F5-4a1e-B4DA-DD883F08F549}
            ArrayList deptPackagenurseList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            deptPackageList.AddRange(deptPackagenurseList);

            cmbdept.AddItems(deptPackageList);

            //cmbdept.SelectedIndex = 0;
            


            //MessageBox.Show(cmbdept.SelectedValue.ToString ());
            cmbdept.Tag = DoctDeptCode;
            cmbdept2.Tag = "ALL";
            //MessageBox.Show(cmbdept.SelectedText+"||"+cmbdept.Text);

            //this.cmbCardType.AddItems(IDTypeList);

        }

        /// <summary>
        /// 左侧控件的发票或者其他信息更新事件
        /// </summary>
        protected virtual void leftControl_InvoiceUpdated()
        {
            //if (!((Control)this.registerControl).Focus())
            //{
            //    ((Control)this.registerControl).Focus();
            //}
            //if (this.itemInputControl.IsFocus)
            //{
            //    ((Control)this.registerControl).Focus();
            //}
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum PackageCols
        {
            /// <summary>
            /// 选择
            /// </summary>
            option = 0,
            /// <summary>
            /// 名称
            /// </summary>
            Name = 1,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 2,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 3,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 6,

            /// <summary>
            /// 优惠金额
            /// </summary>
            EtcCost = 5,

            /// <summary>
            /// 收费日期
            /// </summary>
            operTime = 4,
        }

        /// <summary>
        /// 列枚举7B4075-258A-4F11-A757-D8F0AEF08380} 
        /// </summary>
        private enum DetailCols
        {
            /// <summary>
            /// 选择
            /// </summary>
            option = 0,

            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 1,

            /// <summary>
            /// 总数量
            /// </summary>
            TotQty = 2,

            /// <summary>
            /// 单位
            /// </summary>
            Unit = 3,

            /// <summary>
            /// 可开数量
            /// </summary>
            ValiableQty = 4,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 5,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 6,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 7,

            /// <summary>
            /// 优惠金额
            /// </summary>
            EtcCost = 8
        }

        private void cmbdept2_SelectedValueChanged(object sender, EventArgs e)
        {
           
            if (packageList != null)
            {
                ArrayList pList = new ArrayList();
                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
                {
                    string dept = cmbdept2.Tag.ToString();
                    if (dept == "ALL")
                    {
                        pList.Add(package);
                    }
                    else
                    {
                        if (package.Package_Dept.ToString() == dept)
                        {
                            pList.Add(package);
                        }
                    }
                }
                if (pList.Count > 0)
                {
                    ShowPackage(pList);
                    fpPackageDetail_Sheet1.RowCount = 0;
                }
                else
                {
                    fpPackageDetail_Sheet1.RowCount = 0;
                    fpPackage_Sheet1.RowCount = 0;
                }
            }
            else
            {
                SetPackageInfo();
            }
        }

        private void txtCardNO_TextChanged(object sender, EventArgs e)
        {

        }

        private void fpPackageDetail_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == (int)PackageCols.option)
            {
                //添加全选{B90963E3-E1DA-45da-AD6B-C2E1B19C4CB0}7B4075-258A-4F11-A757-D8F0AEF08380} 
                if (e.ColumnHeader)
                {
                    if (fpPackageDetail_Sheet1.ColumnHeader.Columns[(int)DetailCols.option].Label == "全选")
                    {
                        for (int i = 0; i < fpPackage_Sheet1.Rows.Count; i++)
                        {
                            this.fpPackageDetail_Sheet1.Cells[i, (int)DetailCols.option].Text = "true";
                        }
                        fpPackageDetail_Sheet1.ColumnHeader.Columns[(int)DetailCols.option].Label = "取消";
                    }
                    else
                        if (fpPackageDetail_Sheet1.ColumnHeader.Columns[(int)DetailCols.option].Label == "取消")
                        {
                            for (int i = 0; i < fpPackage_Sheet1.Rows.Count; i++)
                            {
                                this.fpPackageDetail_Sheet1.Cells[i, (int)DetailCols.option].Text = "false";
                            }
                            fpPackageDetail_Sheet1.ColumnHeader.Columns[(int)DetailCols.option].Label = "全选";
                        }
                }
                else
                {

                    if (this.fpPackageDetail_Sheet1.Cells[e.Row, (int)DetailCols.option].Text.ToLower() == "true")
                    {
                        this.fpPackageDetail_Sheet1.Cells[e.Row, (int)DetailCols.option].Text = "false";
                    }
                    else
                        fpPackageDetail_Sheet1.Cells[e.Row, (int)DetailCols.option].Text = "true";
                }
            }
        }

        
    }
}
