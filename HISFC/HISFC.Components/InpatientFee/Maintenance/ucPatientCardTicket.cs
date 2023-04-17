using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using GZ.Components.Bespeak;
using System.Collections;
using System.Text.RegularExpressions;
using FS.HISFC.Models.Fee;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    public partial class ucPatientCardTicket : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientCardTicket()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 卡类型
        /// </summary>
        private FS.FrameWork.Models.NeuObject cardKind = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 人员业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 卡卷业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.DiscountCardLogic discountCardLogic = new FS.HISFC.BizLogic.Fee.DiscountCardLogic();

        /// <summary>
        /// 客户卡卷业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PatientDiscountCardLogic patientCardLogic = new FS.HISFC.BizLogic.Fee.PatientDiscountCardLogic();

        /// <summary>
        /// 人员财务组业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup employeeFinanceGroupManager = new FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup();

        /// <summary>
        /// 账户管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 人员数组
        /// </summary>
        ArrayList personList = new ArrayList();

        /// <summary>
        /// 人员财务组
        /// </summary>
        ArrayList finaceGroupList = new ArrayList();

        /// <summary>
        /// 性别
        /// </summary>
        private ArrayList sexList = null;

        /// <summary>
        /// 证件类型
        /// </summary>
        private ArrayList IDTypeList = null;

        /// <summary>
        /// toolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private string currentStartCode = string.Empty;
        private long currentCodeNums = 0;
        private bool isMouseCheck = true;

        /// <summary>
        /// 当前登录操作员
        /// </summary>
        private FS.HISFC.Models.Base.Employee curOper;

        /// <summary>
        /// 判断是否最后一个卡号
        /// </summary>
        private bool Used = false;

        #endregion

        #region 属性

        private FS.FrameWork.Models.NeuObject CurrCardKind
        {
            get
            {
                if (this.cmbCardKind.Tag.ToString() == string.Empty)
                {
                    this.cmbCardKind.SelectedIndex = 0;
                }
                cardKind.ID = this.cmbCardKind.Tag.ToString();
                cardKind.Name = this.cmbCardKind.Text.Trim();

                return cardKind;
            }
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

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

            }
        }

        /// <summary>
        /// 取得当前人员姓名
        /// </summary>
        /// <param name="personCode"></param>
        /// <returns></returns>
        protected string GetPersonName(string personCode)
        {
            string PersonName = string.Empty;
            if (personList != null)
            {
                foreach (FS.HISFC.Models.Base.Employee p in personList)
                {
                    if (p.ID == personCode)
                    {
                        PersonName = p.Name;
                        break;
                    }
                }
            }
            return PersonName;
        }

        /// <summary>
        /// 取得当前人员编码
        /// </summary>
        /// <param name="personName"></param>
        /// <returns></returns>
        private string GetPersonCode(string personName)
        {
            string PersonCode = string.Empty;
            if (personList != null)
            {
                foreach (FS.HISFC.Models.Base.Employee p in personList)
                {
                    if (p.Name == personName)
                    {
                        PersonCode = p.ID;
                        break;
                    }
                }
            }
            return PersonCode;
        }

        /// <summary>
        /// 取得当前人员类型，用于判断操作的人员类型是否在后台有变化
        /// </summary>
        /// <param name="personCode"></param>
        /// <returns></returns>
        protected string GetPersonType(string personCode)
        {
            FS.HISFC.Models.Base.Employee employeeInfo = managerIntegrate.GetEmployeeInfo(personCode);

            return employeeInfo.EmployeeType.ID.ToString();
        }

        /// <summary>
        /// 获取证件类型
        /// </summary>
        /// <param name="al"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string QueryNameByIDFromDictionnary(ArrayList al, string ID)
        {
            try
            {
                foreach (FS.FrameWork.Models.NeuObject obj1 in al)
                {
                    if (obj1.ID == ID)
                    {
                        return obj1.Name;
                    }
                }
            }
            catch
            { }
            return string.Empty;
        }

        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            //当前操作员
            this.curOper = (FS.HISFC.Models.Base.Employee)this.discountCardLogic.Operator;

            this.personList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);

            this.cmbCardKind.AddItems((new FS.HISFC.BizLogic.Manager.Constant().GetList("GetCardKind")));

            if (this.cmbCardKind.alItems == null || this.cmbCardKind.alItems.Count == 0)
            {
                MessageBox.Show("请在常数维护中维护收据的类别");
                return;
            }

            this.cmbOper.AddItems(this.personList);
            //默认选择当前用户
            int i = 0;
            foreach (FS.HISFC.Models.Base.Employee person in this.personList)
            {
                if (person.ID == this.curOper.ID)
                {
                    this.cmbOper.SelectedIndex = i;
                }
                i++;
            }

            //默认选择当前第一种卡
            this.cmbCardKind.SelectedIndex = 0;
        }

        /// <summary>
        ///  根据条件查询人员信息
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
        ///根据选择的人员信息设置综合查询信息
        /// </summary>
        /// <param name="patient"></param>
        private void patientInfoSet(PatientInfo patient)
        {
            this.PatientInfo = patient;
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            clearPatientInfo();
            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                this.tbMedicalNO.Text = string.Empty;
                this.tbName.Text = string.Empty;
                this.tbCardType.Text = string.Empty;
                this.tbIDNO.Text = string.Empty;
                this.tbSex.Text = string.Empty;
                this.tbAge.Text = string.Empty;
                this.tbPhone.Text = string.Empty;
                return;
            }

            this.txtCardNO.Text = string.Empty;
            this.tbMedicalNO.Text = this.PatientInfo.PID.CardNO;
            this.tbName.Text = PatientInfo.Name;
            this.tbCardType.Text = this.QueryNameByIDFromDictionnary(this.IDTypeList, this.patientInfo.IDCardType.ID);
            this.tbIDNO.Text = this.patientInfo.IDCard;
            this.tbSex.Text = this.QueryNameByIDFromDictionnary(this.sexList, patientInfo.Sex.ID.ToString());
            this.tbAge.Text = this.accountMgr.GetAge(patientInfo.Birthday);

            this.tbPhone.Text = this.patientInfo.PhoneHome;

        }

        /// <summary>
        /// 清空信息
        /// </summary>
        private void clearPatientInfo()
        {
            foreach (Control control in this.pnlTop.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                }
            }
        }



        /// <summary>
        /// 获取最新可用卡号
        /// </summary>
        /// <returns></returns>
        private void GetNewCardNo()
        {
            ArrayList DiscountCards = new ArrayList();
            if (this.cmbOper.SelectedItem == null || this.cmbCardKind.SelectedItem == null)
            {
                MessageBox.Show("请先选择收费员及卡类型！");
                return;
            }
            string personcode = this.cmbOper.SelectedItem.ID;
            string cardKind = this.cmbCardKind.SelectedItem.ID;
            DiscountCards = this.discountCardLogic.QueryUsedCardByPersonAndCardKind(personcode, cardKind);
            if (DiscountCards != null && DiscountCards.Count > 0)
            {
                FS.HISFC.Models.Fee.DiscountCard discountCard = new FS.HISFC.Models.Fee.DiscountCard();
                discountCard = DiscountCards[0] as FS.HISFC.Models.Fee.DiscountCard;
                int cardno = Convert.ToInt32(discountCard.UsedNo) + 1;
                this.lblTip.Visible = false;
                this.txtNewCardNo.Text = cardno.ToString();

                if (this.txtNewCardNo.Text == discountCard.EndNo)
                {
                    this.Used = true;
                }
            }
            else
            {
                this.lblTip.Visible = true;
            }
            
        }

        /// <summary>
        /// 保存购物卡领取信息
        /// </summary>
        private void SavePatientCard()
        {
            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                MessageBox.Show("客户信息不可为空！");
                this.txtCardNO.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtNewCardNo.Text.Trim()))
            {
                MessageBox.Show("无可用卡号！");
                this.txtNewCardNo.Focus();
                return;
            }

            string newcardNo = this.txtNewCardNo.Text.Trim();
            for (int i = newcardNo.Length - 1; i > 0; i--)
            {
                if (!Regex.IsMatch(newcardNo[i].ToString(), "^[0-9]*$"))
                {
                    MessageBox.Show("非有效卡号！");
                    this.txtNewCardNo.Focus();
                    return;
                }
            }

            if (this.cmbCardKind.SelectedItem == null || this.cmbOper.SelectedItem == null)
            {
                MessageBox.Show("卡类型与收费员不可为空！");
                return;
            }

            PatientDiscountCard patientCard = new PatientDiscountCard();
            patientCard.CardNo = this.txtNewCardNo.Text.Trim();
            patientCard.CardKind = this.cmbCardKind.SelectedItem.ID;
            patientCard.CardName = this.cmbCardKind.SelectedItem.Name;
            patientCard.GetName = this.PatientInfo.Name;
            patientCard.GetCardNo = this.PatientInfo.PID.CardNO;
            patientCard.GetPhone = this.PatientInfo.PhoneHome;
            patientCard.GetTime = System.DateTime.Now;
            patientCard.GetOper = this.cmbOper.SelectedItem.ID;
            patientCard.UsedState = "0";

            if (!this.patientCardLogic.NewCardNoIsValid(this.txtNewCardNo.Text.Trim(), this.cmbCardKind.SelectedItem.ID))
            {
                MessageBox.Show("该卡存在已领取记录！");
                return;
            }

            string usedState = "1";
            if (this.Used == true)
            {
                usedState = "-1";
            }

            if (this.patientCardLogic.InsertPatientCard(patientCard) < 0)
            {
               
                MessageBox.Show("保存领取信息失败！");
                return;
            }
            else{

                if (this.discountCardLogic.UpdateDiscountCardUsedNo(patientCard.GetOper, patientCard.CardKind, patientCard.CardNo, usedState, this.curOper.ID, patientCard.GetTime.ToString()) < 0)
                {
                    MessageBox.Show("更新购物卡信息失败！");
                    return;
                }
            }

            this.QueryAllPatientCards();

            MessageBox.Show("保存领取信息成功！");
        }

        /// <summary>
        /// 查询所有领卡记录
        /// </summary>
        private void QueryAllPatientCards()
        {
            this.fpSpread2_Sheet1.Rows.Count = 0;
            ArrayList PatientCards = new ArrayList();
            PatientCards = this.patientCardLogic.QueryAllPatientCard();
            if (PatientCards != null && PatientCards.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.PatientDiscountCard info in PatientCards)
                {
                    fpSpread2_Sheet1.Rows.Add(0, 1);
                    fpSpread2_Sheet1.Cells[0, 0].Text = info.CardNo;
                    fpSpread2_Sheet1.Cells[0, 1].Text = info.CardKind;
                    fpSpread2_Sheet1.Cells[0, 2].Text = info.CardName;
                    fpSpread2_Sheet1.Cells[0, 3].Text = info.GetName;
                    fpSpread2_Sheet1.Cells[0, 4].Text = info.GetCardNo;
                    fpSpread2_Sheet1.Cells[0, 5].Text = info.GetTime.ToString();
                    fpSpread2_Sheet1.Cells[0, 6].Text = info.GetPhone;
                    fpSpread2_Sheet1.Cells[0, 7].Text = this.GetPersonName(info.GetOper);
                    if (info.UsedState == "0")
                        fpSpread2_Sheet1.Cells[0, 8].Text = "未用";
                    else
                        fpSpread2_Sheet1.Cells[0, 8].Text = "已用";
                    fpSpread2_Sheet1.Cells[0, 9].Text = info.UsedName;
                    fpSpread2_Sheet1.Cells[0, 10].Text = info.UsedCardNo;
                    fpSpread2_Sheet1.Cells[0, 11].Text = info.UsedPhone;
                    fpSpread2_Sheet1.Cells[0, 12].Text = info.UsedTime.ToString();
                    fpSpread2_Sheet1.Cells[0, 13].Text = this.GetPersonName(info.UsedOper);
                }
            }
        }

        /// <summary>
        /// 退回卡卷信息
        /// </summary>
        private void DeletePatientCards()
        {
            if (string.IsNullOrEmpty(this.txtNewCardNo.Text.Trim()))
            {
                MessageBox.Show("无可用卡号！");
                this.txtNewCardNo.Focus();
                return;
            }

            string cardNo = this.txtNewCardNo.Text.Trim();
            for (int i = cardNo.Length - 1; i > 0; i--)
            {
                if (!Regex.IsMatch(cardNo[i].ToString(), "^[0-9]*$"))
                {
                    MessageBox.Show("非有效卡号！");
                    this.txtNewCardNo.Focus();
                    return;
                }
            }

            if (this.cmbCardKind.SelectedItem == null)
            {
                MessageBox.Show("卡类型不可为空！");
                return;
            }

            if (this.patientCardLogic.DeletePatientCardByCardKindAndNo(cardNo, this.cmbCardKind.SelectedItem.ID) < 0)
            {

                MessageBox.Show("退回领取信息失败！");
                return;
            }

            this.QueryAllPatientCards();
            MessageBox.Show("退回领取信息成功！");
        }

        #endregion

        #region 事件
        /// <summary>
        /// 个人信息检索，回车
        /// </summary>
        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                queryPatientList(this.txtCardNO.Text);
            }
        }


        private void btnGetCardNo_Click(object sender, EventArgs e)
        {
            this.GetNewCardNo();
        }

        /// <summary>
        /// 添加toolbar按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("保存领取信息", "保存领取信息", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("查询领取信息", "查询领取信息", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolBarService.AddToolButton("退回领取信息", "退回领取信息", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.ToString())
            {
                case "保存领取信息":
                    this.SavePatientCard();
                    break;
                case "查询领取信息":
                    this.QueryAllPatientCards();
                    break;
                case "退回领取信息":
                    this.DeletePatientCards();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private void ucPatientCardTicket_Load(object sender, EventArgs e)
        {
            Init();
        }

        #endregion 

        
    }
}
