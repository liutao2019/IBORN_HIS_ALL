using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmUnExecutedItem : FS.FrameWork.WinForms.Forms.BaseForm
    {

        /// <summary>
        /// 患者基本信息   {048e65b9-0b30-4049-9d66-e74fbe28c2fa}
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
                this.SetItemInfo();
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

        FS.HISFC.BizLogic.MedicalPackage.Fee.Package pckMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();

        public frmUnExecutedItem()
        {
            InitializeComponent();
            InitControls();
        }


        /// <summary>
        /// 初始化控件下拉列表
        /// </summary>
        private void InitControls()
        {
            //初始化合同单位
            pactList = this.pactManager.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("初始化合同单位出错!" + this.pactManager.Err);
                return;
            }
            //初始化性别
            ArrayList sexListTemp = new ArrayList();
            sexList = new ArrayList();
            sexListTemp = FS.HISFC.Models.Base.SexEnumService.List();
            FS.HISFC.Models.Base.Spell spell = null;
            foreach (FS.FrameWork.Models.NeuObject neuObj in sexListTemp)
            {
                spell = new FS.HISFC.Models.Base.Spell();
                if (neuObj.ID == "M")
                {
                    spell.ID = neuObj.ID;
                    spell.Name = neuObj.Name;
                    spell.Memo = neuObj.Memo;
                    spell.UserCode = "1";
                }
                else if (neuObj.ID == "F")
                {
                    spell.ID = neuObj.ID;
                    spell.Name = neuObj.Name;
                    spell.Memo = neuObj.Memo;
                    spell.UserCode = "2";
                }
                else if (neuObj.ID == "O")
                {
                    spell.ID = neuObj.ID;
                    spell.Name = neuObj.Name;
                    spell.Memo = neuObj.Memo;
                    spell.UserCode = "3";
                }
                else
                {
                    spell.ID = neuObj.ID;
                    spell.Name = neuObj.Name;
                    spell.Memo = neuObj.Memo;
                }
                sexList.Add(spell);
            }
           
            //初始化证件类型列表
            IDTypeList = constantMgr.GetList("IDCard");
            if (IDTypeList == null)
            {
                MessageBox.Show("初始化证件类型列表出错!" + this.constantMgr.Err);

                return;
            }

            //初始化会员级别
            memberLevel = constantMgr.GetList("MemCardType");
            if (memberLevel == null)
            {
                MessageBox.Show("初始化会员类型列表出错!" + this.constantMgr.Err);

                return;
            }

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



        /// <summary>
        /// 设置套餐信息
        /// </summary>
        private void SetItemInfo()
        {
          
            this.fpPackageDetail_Sheet1.RowCount = 0;

            if (this.patientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                return;
            }

            

            ArrayList packageList = this.pckMgr.QueryUnExeccutedItem(this.PatientInfo.PID.CardNO);
            if (packageList == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail package in packageList)
            {
                this.fpPackageDetail_Sheet1.AddRows(this.fpPackageDetail_Sheet1.RowCount, 1);
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, 0].Value = package.CardNO;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, 1].Text = package.Name ;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, 2].Text = package.Cancel_Flag;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, 3].Text = package.CancelOper;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, 4].Text = package.Memo;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, 5].Text = package.Oper;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, 6].Text = package.PackageName;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, 7].Text = package.PayFlag;
                this.fpPackageDetail_Sheet1.Rows[this.fpPackageDetail_Sheet1.RowCount - 1].Tag = package;
            }
           
        }
    }
}
