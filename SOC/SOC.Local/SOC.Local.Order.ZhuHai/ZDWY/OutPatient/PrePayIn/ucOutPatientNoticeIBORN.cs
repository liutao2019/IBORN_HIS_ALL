using System;
using System.Collections.Generic;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PrePayIn
{
    public partial class ucOutPatientNoticeIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintInHosNotice
    {
        public ucOutPatientNoticeIBORN()
        {
            InitializeComponent();
        }

        #region 变量


        #endregion


        #region IPrintInHosNotice 成员

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("A5");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("A5", 550, 850);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsLandScape = true;
            print.IsDataAutoExtend = false;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
            return 1;
        }
        
        public int PrintView()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("A5");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("A5", 550, 850);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            print.IsLandScape = true;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
            return 1;
        }
        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// Manager业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private ArrayList marryList = FS.HISFC.Models.RADT.MaritalStatusEnumService.List();

        public int SetValue(FS.HISFC.Models.RADT.PatientInfo prePatientInfo)
        {
            this.Clear();

            if (null == prePatientInfo) return 0;
            this.lblTitleName.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
            this.lblAge.Text = dbMgr.GetAge(prePatientInfo.Birthday);
            this.lblDiagnosis.Text = prePatientInfo.ClinicDiagnose + ";" + prePatientInfo.MSDiagnoses; //{0935cb7a-b021-4c17-94bf-eaf68b523472}
            this.lblGender.Text = prePatientInfo.Sex.Name;
            this.lblInDate.Text = prePatientInfo.PVisit.InTime.ToString();
            this.lblInDept.Text = prePatientInfo.PVisit.PatientLocation.Dept.Name;
            this.lblName.Text = prePatientInfo.Name;
            this.lblCardNO.Text = prePatientInfo.PID.CardNO;
            this.lblIDNO.Text = prePatientInfo.IDCard;
            this.lblPhone.Text = string.IsNullOrEmpty(prePatientInfo.PhoneHome) ? prePatientInfo.Mobile : prePatientInfo.PhoneHome;
            this.lblAdressHome.Text = prePatientInfo.AddressHome;
            this.lblCardNO.Text = prePatientInfo.PID.CardNO;
            this.lblInWard.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(prePatientInfo.PVisit.PatientLocation.NurseCell.ID);
            //this.lblInDoct.Text = prePatientInfo.Name;
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            accountCard = accountManager.GetAccountCardForOne(prePatientInfo.PID.CardNO);

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            foreach (FS.FrameWork.Models.NeuObject o in marryList)
            {
                if (o.ID == prePatientInfo.MaritalStatus.ID.ToString())
                {

                    this.lblMarry.Text = o.Name;
                    break;
                }
                else
                {
                    this.lblMarry.Text = "";
                }
            }

            if (accountCard != null)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = managerIntegrate.GetConstansObj("MemCardType", accountCard.AccountLevel.ID);

                this.lblVIP.Text = obj.Name;
            }
            else
            {
                this.lblVIP.Text = "";
            }
            lblPatientType.Text = prePatientInfo.PatientType.Name;//{84C3BB2F-BC2D-4dfa-99D0-F4C545EC528B}添加患者类型
            this.lblPrintDate.Text = accountManager.GetDateTimeFromSysDateTime().ToString();
            //if (string.IsNullOrEmpty(prePatientInfo.User03))// {D59C1D74-CE65-424a-9CB3-7F9174664504}
            //{
            //    prePatientInfo.User03 = FS.FrameWork.Management.Connection.Operator.ID;
            //}
            this.lblInDoct.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(FS.FrameWork.Management.Connection.Operator.ID);

                //FS.FrameWork.Management.Connection.Operator.Name;
            return 1;
        }

        private void Clear()
        {
            this.lblAge.Text = string.Empty;
            this.lblDiagnosis.Text = string.Empty;
            this.lblCardNO.Text = string.Empty;
            this.lblGender.Text = string.Empty;
            this.lblInDate.Text = string.Empty;
            this.lblInDept.Text = string.Empty;
            this.lblVIP.Text = string.Empty;
            this.lblName.Text = string.Empty;
            this.lblMarry.Text = string.Empty;
            this.lblInWard.Text = string.Empty;
            this.lblInDoct.Text = string.Empty;
            this.lblIDNO.Text = string.Empty;
            this.lblAdressHome.Text = string.Empty;
            this.lblPhone.Text = string.Empty;
            this.lblPrintDate.Text = string.Empty;
        }

        #endregion
    }
}
