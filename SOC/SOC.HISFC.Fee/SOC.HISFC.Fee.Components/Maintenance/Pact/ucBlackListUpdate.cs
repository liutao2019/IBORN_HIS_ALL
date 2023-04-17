using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    public partial class ucBlackListUpdate : Form
    {
        public ucBlackListUpdate()
        {
            InitializeComponent();
        }

        #region 变量属性
        //当前操作员
        FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
        //上一项、下一项事件代理
        public delegate void GetNextItemHandler(int span);
        public event GetNextItemHandler GetNextItem;
        //保存完成事件代理
        public delegate void SaveItemHandler(FS.SOC.HISFC.Fee.Models.BlackList blackList);
        public event SaveItemHandler EndSave;
        // 合同单位业务处理实例
        private FS.SOC.HISFC.Fee.BizLogic.PactInfo pactInfo = new FS.SOC.HISFC.Fee.BizLogic.PactInfo();
        // 控件内操作的黑名单实体
        FS.SOC.HISFC.Fee.Models.BlackList blackList = null;
        // 控件内操作的黑名业务实例
        FS.SOC.HISFC.Fee.BizLogic.BlackList blackListBiz = new FS.SOC.HISFC.Fee.BizLogic.BlackList();
        #endregion
      
        #region 初始化
        //页面加载
        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            this.neuCancelBtn.Click -= new EventHandler(btnCancel_Click);
            this.neuCancelBtn.Click += new EventHandler(btnCancel_Click);
            this.neuSaveBtn.Click -= new EventHandler(btnSave_Click);
            this.neuSaveBtn.Click += new EventHandler(btnSave_Click);

            this.nbtBack.Click -= new EventHandler(nbtBack_Click);
            this.nbtBack.Click += new EventHandler(nbtBack_Click);
            this.nbtNext.Click -= new EventHandler(nbtNext_Click);
            this.nbtNext.Click += new EventHandler(nbtNext_Click);
        }
        //页面加载初始化
        public int Init()
        {
            try
            {
                this.InitBaseData();
            }
            catch (System.Exception ex)
            {
                FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }
            return 1;
        }
        // 初始化页面基本数据
        private void InitBaseData()
        {
            //合同单位
            this.cmbPactName.AddItems(pactInfo.QueryPactUnitAll());
            //种类 0 单位 1 个人
            this.cmbKind.AddItems(FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().QueryConstant("BlackListKind"));
            //性别
            this.cmbSex.AddItems(FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().QueryConstant("sex"));
        }

        #endregion

        #region 事件

        // 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        // 保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }
        // 保存
        private void Save()
        {
            if (this.CheckValid())
            {
                FS.SOC.HISFC.Fee.Models.BlackList blackList = this.GetBlackList();

                if (blackList != null)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    FS.SOC.HISFC.Fee.BizLogic.BlackList blackListBiz = new FS.SOC.HISFC.Fee.BizLogic.BlackList();
                    blackList.OperDate = blackListBiz.GetDateTimeFromSysDateTime();
                    ArrayList alItem = new ArrayList();
                    //新增
                    if (string.IsNullOrEmpty(blackList.BlackId))
                    {
                        if (blackListBiz.InsertItem(blackList) == -1)
                        {
                            blackList.BlackId = "";
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + blackListBiz.Err, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    //更新
                    else
                    {
                        if (blackListBiz.UpdateItem(blackList) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + blackListBiz.Err, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //父窗口更新该数据
                    if(this.EndSave != null)
                    {
                        this.EndSave(blackList);
                    }
                    FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("保存成功！", MessageBoxIcon.Information);
                    this.Hide();
                }
            }
        }
        // 下一项
        void nbtNext_Click(object sender, EventArgs e)
        {
            if (this.GetNextItem != null)
            {
                this.GetNextItem(1);
            }
        }
        //上一项
        void nbtBack_Click(object sender, EventArgs e)
        {
            if (this.GetNextItem != null)
            {
                this.GetNextItem(-1);
            }
        }

        #endregion

        #region 方法

        // 有效性检测
        private bool CheckValid()
        {
            //患者名称不能大于20个汉字
            if (!string.IsNullOrEmpty(this.txtName.Text) && FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 40) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者名称最多可录入20个汉字!"), "提示");
                this.txtName.Focus();
                return false;
            }
            //校验合同单位
            if (!string.IsNullOrEmpty(this.cmbPactName.Text) && this.ValidCombox(this.cmbPactName,FS.FrameWork.Management.Language.Msg("您选择的合同单位有误或不在合同单位的下拉列表中,请重新选择!")) < 0)
            {
                this.cmbPactName.Focus();
                return false;
            }
            //医疗证号不能为空
            string mcardNo = this.txtMcardNo.Text;
            if(string.IsNullOrEmpty(mcardNo))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请录入医疗证号!"), "提示");
                this.txtMcardNo.Focus();
                return false;
            }
            //医疗证号不能大于20位
            if (!FS.FrameWork.Public.String.ValidMaxLengh(mcardNo.Trim(), 20))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("医疗证号最多可录入20位数字!"), "提示");
                this.txtMcardNo.Focus();
                return false;
            }
            //校验种类
            if (!string.IsNullOrEmpty(this.cmbKind.Text) && this.ValidCombox(this.cmbKind, FS.FrameWork.Management.Language.Msg("您选择的种类有误或不在种类的下拉列表中,请重新选择!")) < 0)
            {
                this.cmbKind.Focus();
                return false;
            }
            //身份证号不能大于18位
            if (!string.IsNullOrEmpty(this.txtIdDno.Text) && !FS.FrameWork.Public.String.ValidMaxLengh(this.txtIdDno.Text, 18))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("身份证号最多可录入18位数字,请重新输入!"), "提示");
                this.txtIdDno.Select();
                return false;
            }
            //校验身份证号
            if (!string.IsNullOrEmpty(this.txtIdDno.Text))
            {
                int reurnValue = this.ProcessIDENNO(this.txtIdDno.Text);

                if (reurnValue < 0)
                {
                    return false;
                }
            }
            //校验性别
            if (!string.IsNullOrEmpty(this.cmbSex.Text) && this.ValidCombox(this.cmbSex, FS.FrameWork.Management.Language.Msg("您选择的性别有误或不在性别的下拉列表中,请重新选择!")) < 0)
            {
                this.cmbSex.Focus();
                return false;
            }
            //出生日期不能大于当前时间
            DateTime current = this.blackListBiz.GetDateTimeFromSysDateTime().Date;
            if (this.dtpBirthday.Value.Date > current)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间!"), "提示");
                this.dtpBirthday.Focus();
                return false;
            }
            //起始日期不能大于结束日期
            if (this.dtpBeginDate.Value.Date > this.dtpEndDate.Value.Date)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("起始日期不能大于结束日期!"), "提示");
                this.dtpEndDate.Focus();
                return false;
            }
            return true;
        }
        // 校验下拉列表选择项是否合法
        private int ValidCombox(FS.FrameWork.WinForms.Controls.NeuComboBox comBoBox,string ErrMsg)
        {
            int j = 0;
            for (int i = 0; i < comBoBox.Items.Count; i++)
            {
                if (comBoBox.Text.Trim() == comBoBox.Items[i].ToString())
                {
                    comBoBox.SelectedIndex = i;
                    j++;
                    break;
                }
            }
            //例："您选择的[合同单位]有误或不在[合同单位]的下拉列表中,请重新选择"
            if (j == 0)
            {
                MessageBox.Show(ErrMsg);
                return -1;
            }
            return 1;
        }
        // 校验身份证
        private int ProcessIDENNO(string idNO)
        {
            try
            {
                string errText = string.Empty;
                //校验身份证号
                string idNOTmp = string.Empty;
                if (idNO.Length == 15)
                {
                    idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
                }
                else
                {
                    idNOTmp = idNO;
                }
                //校验身份证号
                int returnValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref errText);

                if (returnValue < 0)
                {
                    MessageBox.Show(errText);
                    this.txtIdDno.Focus();
                    return -1;
                }
                string[] reurnString = errText.Split(',');
                if (!string.IsNullOrEmpty(this.dtpBirthday.Text) && this.dtpBirthday.Text != reurnString[1])
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的生日日期与身份证号码中的生日不符"));
                    this.dtpBirthday.Focus();
                    return -1;
                }
                if (!string.IsNullOrEmpty(this.cmbSex.Text) && this.cmbSex.Text != reurnString[2])
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的性别与身份证中的性别不符"));
                    this.cmbSex.Focus();
                    return -1;
                }
            }
            catch
            {
            }
            return 1;
        }
        // 获取基本信息
        private FS.SOC.HISFC.Fee.Models.BlackList GetBlackList()
        {
            if (this.blackList == null)
            {
                this.blackList = new FS.SOC.HISFC.Fee.Models.BlackList();
            }
            FS.SOC.HISFC.Fee.Models.BlackList blackList = this.blackList.Clone();
            blackList.Name = this.txtName.Text;
            if (!string.IsNullOrEmpty(this.cmbPactName.Text))
            {
                blackList.PactCode = this.cmbPactName.Tag.ToString();
                blackList.PactName = this.cmbPactName.Text;
            }
            blackList.McordNo = this.txtMcardNo.Text;
            if (!string.IsNullOrEmpty(this.cmbKind.Text))
            {
                blackList.Kind = this.cmbKind.Tag.ToString();
                blackList.KindName = this.cmbKind.Text;
            }
            blackList.IdDno = this.txtIdDno.Text;
            if (!string.IsNullOrEmpty(this.cmbSex.Text))
            {
                blackList.SexCode = this.cmbSex.Tag.ToString();
                blackList.SexName = this.cmbSex.Text;
            }
            string birthday = this.dtpBirthday.Text;
            if (!string.IsNullOrEmpty(birthday) && !birthday.Equals("0001-01-01 00:00:00"))
            {
                blackList.Birthday = Convert.ToDateTime(birthday);
            }
            blackList.Ddyy1 = this.cmbDdyy1.Text;
            blackList.Ddyy2 = this.cmbDdyy2.Text;
            blackList.Ddyy3 = this.cmbDdyy3.Text;
            blackList.ClinicRate = Convert.ToDecimal(string.IsNullOrEmpty(this.nudClinicRate.Text) ? "0" : this.nudClinicRate.Text);
            blackList.InpatientRate = Convert.ToDecimal(string.IsNullOrEmpty(this.nudInpatientRate.Text) ? "0" : this.nudInpatientRate.Text);
            string beginDate = this.dtpBeginDate.Text;
            if (!string.IsNullOrEmpty(beginDate) && !beginDate.Equals("0001-01-01 00:00:00"))
            {
                blackList.BeginDate = Convert.ToDateTime(beginDate);
            }
            string endDate = this.dtpEndDate.Text;
            if (!string.IsNullOrEmpty(endDate) && !endDate.Equals("0001-01-01 00:00:00"))
            {
                blackList.EndDate = Convert.ToDateTime(endDate);
            }
            blackList.DayLimit = Convert.ToDecimal(string.IsNullOrEmpty(this.nudDayLimit.Text)? "0" : this.nudDayLimit.Text);
            blackList.MonthLimit = Convert.ToDecimal(string.IsNullOrEmpty(this.nudMonthLimit.Text)? "0" : this.nudMonthLimit.Text);
            blackList.YearLimit = Convert.ToDecimal(string.IsNullOrEmpty(this.nudYearLimit.Text)? "0" : this.nudYearLimit.Text);
            blackList.OnceLimit = Convert.ToDecimal(string.IsNullOrEmpty(this.nudOnceLimit.Text)? "0" : this.nudOnceLimit.Text);
            blackList.BedLimit = Convert.ToDecimal(string.IsNullOrEmpty(this.nudBedLimit.Text)? "0" : this.nudBedLimit.Text);
            blackList.AirLimit = Convert.ToDecimal(string.IsNullOrEmpty(this.nudAirLimit.Text) ? "0" : this.nudAirLimit.Text);
            blackList.UnitCode = this.cmbUnitCode.Text;
            //当前操作员
            blackList.OperCode = employee.ID;
            //当前操作时间
            blackList.OperDate = this.blackListBiz.GetDateTimeFromSysDateTime();
            //门诊启用标记 0 不启用 1 启用
            bool clinicFlag = this.cbxClinicFlag.Checked;
            if (clinicFlag)
            {
                blackList.ClinicFlag = "1";
            }
            else
            {
                blackList.ClinicFlag = "0";
            }
            //住院启用标记 0 不启用 1 启用
            bool inpatientFlag = this.cbxInpatientFlag.Checked;
            if (inpatientFlag)
            {
                blackList.InpatientFlag = "1";
            }
            else
            {
                blackList.InpatientFlag = "0";
            }
            return blackList;
        }
        // 根据传入的blackList实体信息 设置控件显示
        public void SetBlackList(FS.SOC.HISFC.Fee.Models.BlackList blackList)
        {
            this.blackList = blackList;
            this.txtName.Text = blackList.Name;
            this.cmbPactName.Text = blackList.PactName;
            this.cmbPactName.Tag = blackList.PactCode;
            this.txtMcardNo.Text = blackList.McordNo;
            this.cmbKind.Text = blackList.KindName;
            this.cmbKind.Tag = blackList.Kind;
            this.txtIdDno.Text = blackList.IdDno;
            this.cmbSex.Text = blackList.SexName;
            this.cmbSex.Tag = blackList.SexCode;
            string birthDay = blackList.Birthday.ToString();
            if (!string.IsNullOrEmpty(birthDay) && !birthDay.Equals("0001-01-01 00:00:00"))
            {
                this.dtpBirthday.Text = blackList.Birthday.ToShortDateString();
            }
            this.cmbDdyy1.Text = blackList.Ddyy1;
            this.cmbDdyy2.Text = blackList.Ddyy2;
            this.cmbDdyy3.Text = blackList.Ddyy3;
            this.nudClinicRate.Text = blackList.ClinicRate.ToString();
            this.nudInpatientRate.Text = blackList.InpatientRate.ToString();
            string beginDate = blackList.BeginDate.ToString();
            if (!string.IsNullOrEmpty(beginDate) && !beginDate.Equals("0001-01-01 00:00:00"))
            {
                this.dtpBeginDate.Text = blackList.BeginDate.ToShortDateString();
            }
            string endDate = blackList.EndDate.ToString();
            if (!string.IsNullOrEmpty(endDate) && !endDate.Equals("0001-01-01 00:00:00"))
            {
                this.dtpEndDate.Text = blackList.EndDate.ToShortDateString();
            }
            this.cmbUnitCode.Text = blackList.UnitCode;
            this.nudDayLimit.Text = blackList.DayLimit.ToString();
            this.nudMonthLimit.Text = blackList.MonthLimit.ToString();
            this.nudYearLimit.Text = blackList.YearLimit.ToString();
            this.nudOnceLimit.Text = blackList.OnceLimit.ToString();
            this.nudBedLimit.Text = blackList.BedLimit.ToString();
            this.nudAirLimit.Text = blackList.AirLimit.ToString();
 
            //门诊启用标记 0 不启用 1 启用
            string clinicFlag = blackList.ClinicFlag;
            if (!string.IsNullOrEmpty(clinicFlag) && clinicFlag.Equals("1"))
            {
                this.cbxClinicFlag.Checked = true;
            }
            else
            {
                this.cbxInpatientFlag.Checked = false;
            }
            //住院启用标记 0 不启用 1 启用
            string inpatientFlag = blackList.InpatientFlag;
            if (!string.IsNullOrEmpty(inpatientFlag) && inpatientFlag.Equals("1"))
            {
                this.cbxInpatientFlag.Checked = true;
            }
            else
            {
                this.cbxInpatientFlag.Checked = false;
            }
        }

        #endregion
    }
}
