using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBorn.SI.GuangZhou.Controls
{
    /// <summary>
    /// 查询广州医保住院登记信息
    /// </summary>
    public partial class ucGetSiRegInfoInPatient : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucGetSiRegInfoInPatient()
        {
            InitializeComponent();
        }

        #region 变量和属性

        bool isOK = false;
        public bool IsOK { get { return isOK; } }

        private FS.HISFC.Models.RADT.PatientInfo currentReg = null;
        /// <summary>
        /// 当前操作的患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo CurrentReg
        {
            get
            {
                return currentReg;
            }
            set
            {
                currentReg = value;
            }
        }

        /// <summary>
        /// 设置开始时间
        /// </summary>
        public DateTime BeginTime
        {
            set
            {
                this.dtBeginTime.Value = value;
            }
        }

        /// <summary>
        /// 就诊类别：1住院 2门诊特定项目 3普通门诊
        /// </summary>
        private string medicalTypeKind = "1";

        /// <summary>
        /// 就诊类别：1住院 2门诊特定项目 3普通门诊
        /// </summary>
        public string MedicalTypeKind
        {
            get { return medicalTypeKind; }
            set { medicalTypeKind = value; }
        }

        /// <summary>
        /// 当前患者的医保登记信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo selectedInsReg = null;

        /// <summary>
        /// 当前患者的医保登记信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo SelectedInsReg
        {
            get { return selectedInsReg; }
            set { selectedInsReg = value; }
        }

        /// <summary>
        /// 广州医保的住院登记列表
        /// </summary>
        private List<FS.HISFC.Models.RADT.PatientInfo> insRegList = null;

        /// <summary>
        /// 广州医保的住院登记列表
        /// </summary>
        private DataTable dtInsReg = null;

        /// <summary>
        /// 查询类型
        /// </summary>
        private List<FS.FrameWork.Models.NeuObject> queryTypeHelper = null;

        #endregion

        #region 方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            isOK = false;
            this.selectedInsReg = null;
            this.insRegList = new List<FS.HISFC.Models.RADT.PatientInfo>();
            this.dtInsReg.Clear();
            this.fpSpread_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// 初始化FP格式
        /// </summary>
        private void InitFpFormat()
        {
            try
            {
                // FarPoint的column类型 与 DataTable的Column类型必须要一致(基础数据类型，或者 自定义类类型)
                //表结构
                this.dtInsReg = new DataTable();
                this.dtInsReg.DefaultView.AllowEdit = true;
                this.dtInsReg.Columns.AddRange(
                    new DataColumn[]
                    {
                        new DataColumn("就医登记号",typeof(string)),
                        new DataColumn("医院编号",typeof(string )),
                        new DataColumn("证件号",typeof(string)),
                        new DataColumn("患者姓名",typeof(string)),
                        new DataColumn("单位",typeof(string)),
                        new DataColumn("性别",typeof(string)),
                        new DataColumn("出生日期",typeof(string)),
                        new DataColumn("人员类别",typeof(string)),
                        new DataColumn("门诊号",typeof(string)),
                        new DataColumn("就诊类别",typeof(string)),
                        new DataColumn("登记时间",typeof(string)),
                        new DataColumn("登记诊断",typeof(string)),
                        new DataColumn("ICD码",typeof(string)),
                        new DataColumn("看诊科室",typeof(string)),
                    }
                    );

                this.fpSpread.EditModePermanent = false;
                this.fpSpread.EditMode = false;
                this.fpSpread.EditModeReplace = false;

                this.fpSpread.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
                this.fpSpread_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
                this.fpSpread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
                this.fpSpread_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;

                //格式属性设置
                this.fpSpread_Sheet1.DataAutoSizeColumns = false;
                this.fpSpread_Sheet1.DataAutoCellTypes = false;

                //绑定数据源
                this.fpSpread_Sheet1.DataSource = this.dtInsReg.DefaultView;

                //宽度设置
                //this.fpSpread.SetColumnWith(0, "就医登记号", 120F);
                //this.fpSpread.SetColumnWith(0, "医院编号", 40F);
                //this.fpSpread.SetColumnWith(0, "证件号", 120F);
                //this.fpSpread.SetColumnWith(0, "患者姓名", 90F);
                //this.fpSpread.SetColumnWith(0, "单位", 120F);
                //this.fpSpread.SetColumnWith(0, "性别", 30F);
                //this.fpSpread.SetColumnWith(0, "出生日期", 80F);
                //this.fpSpread.SetColumnWith(0, "人员类别", 60F);
                //this.fpSpread.SetColumnWith(0, "住院号", 60F);
                //this.fpSpread.SetColumnWith(0, "就诊类别", 60F);
                //this.fpSpread.SetColumnWith(0, "入院时间", 130F);
                //this.fpSpread.SetColumnWith(0, "入院诊断", 60F);
                //this.fpSpread.SetColumnWith(0, "ICD码", 60F);
                //this.fpSpread.SetColumnWith(0, "入院科室", 90F);
                //this.fpSpread.SetColumnWith(0, "入院床位", 90F);
                //this.fpSpread.SetColumnWith(0, "审批号", 60F);               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 初始化基础数据
        /// </summary>
        private void InitBaseData()
        {

            this.queryTypeHelper = new List<FS.FrameWork.Models.NeuObject>();
            ////就医登记号
            //FS.FrameWork.Models.NeuObject item = new FS.FrameWork.Models.NeuObject();
            //item.ID = "0";
            //item.Name = "就医登记号";
            //this.queryTypeHelper.Add(item);
            //证件号
            FS.FrameWork.Models.NeuObject item = new FS.FrameWork.Models.NeuObject();
            item.ID = "1";
            item.Name = "证件号";
            this.queryTypeHelper.Add(item);
            //姓名
            item = new FS.FrameWork.Models.NeuObject();
            item.ID = "2";
            item.Name = "姓名";
            this.queryTypeHelper.Add(item);
            this.cmbQueryType.AddItems(this.queryTypeHelper);

            //默认选中项
            this.cmbQueryType.Tag = "2";
            if (this.CurrentReg != null)
            {
                this.txtInput.Text = this.CurrentReg.Name;
                this.QueryInsRegInfo(this.CurrentReg.Name);
            }

        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="listInsReg"></param>
        public void DisplayInsReg(List<FS.HISFC.Models.RADT.PatientInfo> listInsReg)
        {
            if (listInsReg == null || listInsReg.Count <= 0)
            {
                return;
            }
            listInsReg = listInsReg.OrderBy(m => (m.SIMainInfo.OperDate)).ToList<FS.HISFC.Models.RADT.PatientInfo>();
            foreach (FS.HISFC.Models.RADT.PatientInfo item in listInsReg)
            {
                DataRow dr = this.dtInsReg.NewRow();
                dr["就医登记号"] = item.SIMainInfo.RegNo;
                dr["医院编号"] = item.SIMainInfo.HosNo;
                dr["证件号"] = item.IDCard;
                dr["患者姓名"] = item.Name;
                dr["单位"] = item.CompanyName;
                dr["性别"] = item.Sex.ID;
                dr["出生日期"] = item.Birthday;
                dr["人员类别"] = this.GetPersonTypeName(item.SIMainInfo.EmplType);
                dr["门诊号"] = item.IDCard;
                dr["就诊类别"] = GetJZLBName(item.SIMainInfo.MedicalType.ID);
                dr["登记时间"] = item.SIMainInfo.OperDate;
                dr["登记诊断"] = item.SIMainInfo.InDiagnose.Name;
                dr["ICD码"] = item.SIMainInfo.InDiagnose.ID;
                //dr["看诊科室"] = ir.Register.PatientAdmit.Dept.ID;              

                this.dtInsReg.Rows.Add(dr);
            }
        }

        /// <summary>
        /// 获得就诊类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetJZLBName(string code)
        {
            if (code == "1")
            {
                return "住院";
            }
            else if (code == "2")
            {
                return "门诊特定项目";
            }
            else if (code == "3")
            {
                return "门诊";
            }
            return string.Empty;
        }

        /// <summary>
        /// 获得人员类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetPersonTypeName(string code)
        {
            if (code == "1")
            {
                return "在职";
            }
            else if (code == "2")
            {
                return "退休";
            }
            else if (code == "3")
            {
                return "离休";
            }
            else if (code == "4")
            {
                return "1-4级工残";
            }
            else if (code == "5")
            {
                return "无业";
            }
            else if (code == "6")
            {
                return "已趸缴";
            }
            else if (code == "7")
            {
                return "退职";
            }
            else if (code == "70")
            {
                return "学龄前儿童";
            }
            else if (code == "71")
            {
                return "中小学生";
            }
            else if (code == "72")
            {
                return "大中专学生";
            }
            else if (code == "73")
            {
                return "其他未成年人";
            }
            else if (code == "74")
            {
                return "非从业人员";
            }
            else if (code == "75")
            {
                return "老年人";
            }
            else if (code == "66")
            {
                return "企业离休";
            }
            else if (code == "62")
            {
                return "公医退休";
            }
            else if (code == "61")
            {
                return "公医在职";
            }
            else
            {
                return "居民医保";
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void QueryInsRegInfo(string inputCode)
        {
            //先清屏
            this.Clear();

            IBorn.SI.GuangZhou.InPatient.GetRegisterList getRegisterManager = new IBorn.SI.GuangZhou.InPatient.GetRegisterList();
            string jzlb = this.CurrentReg.SIMainInfo.MedicalType.ID;
            string queryType = this.cmbQueryType.Text; //查询类型
            if (string.IsNullOrEmpty(inputCode))
            {
                if (getRegisterManager.CallService(CurrentReg, ref this.insRegList, "", this.dtBeginTime.Value, jzlb, "") < 0)
                {
                    MessageBox.Show("查询医保住院登记失败!");
                    return;
                }
            }
            else
            {
                switch (queryType)
                {
                    case "证件号":
                        {
                            if (getRegisterManager.CallService(CurrentReg, ref this.insRegList, inputCode, this.dtBeginTime.Value, jzlb, "") < 0)
                            {
                                MessageBox.Show("查询医保住院登记失败!");
                                return;
                            }
                        }
                        break;
                    case "姓名":
                        if (getRegisterManager.CallService(CurrentReg, ref this.insRegList, "", this.dtBeginTime.Value, jzlb, inputCode) < 0)
                        {
                            MessageBox.Show("查询医保住院登记失败!");
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }

            this.DisplayInsReg(this.insRegList);
        }

        /// <summary>
        /// 获取选中的医保登记信息
        /// </summary>
        /// <returns></returns>
        private int GetInsRegInfo()
        {
            if (this.fpSpread_Sheet1.ActiveRow == null)
            {
                return -1;
            }
            int rowIndex = this.fpSpread_Sheet1.ActiveRowIndex;
            string regNO = this.fpSpread_Sheet1.Cells[rowIndex, 0].Text;
            this.SelectedInsReg = this.insRegList.Find(m => m.SIMainInfo.RegNo == regNO);
            if (this.SelectedInsReg == null)
            {
                return -1;
            }
            if (string.IsNullOrEmpty(this.SelectedInsReg.IDCard))
            {
                MessageBox.Show("该医保登记号的证件号为空，请联系医保局!");
                return -1;
            }
            if (this.SelectedInsReg.IDCard != this.CurrentReg.IDCard)
            {
                if (MessageBox.Show("医保登记的证件号【" + this.SelectedInsReg.IDCard + "】与本地登记的登记号【" + this.CurrentReg.IDCard + "】不一致，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return -1;
                }
            }
            if (this.SelectedInsReg.Name != this.CurrentReg.Name)
            {
                if (MessageBox.Show("医保登记的姓名【" + this.SelectedInsReg.Name + "】与本地登记的姓名【" + this.CurrentReg.Name + "】不一致，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return -1;
                }
            }
            //if (this.SelectedInsReg.RegisterTime.Date != this.CurrentReg.Register.PatientAdmit.Time.Date)
            //{
            //    if (this.MessageBoxYesNo("医保登记的入院日期【" + this.SelectedInsReg.RegisterTime.Date.ToShortDateString() + "】与本地登记的入院日期【" + this.CurrentReg.Register.PatientAdmit.Time.Date.ToShortDateString() + "】不一致，是否继续？") == DialogResult.No)
            //    {
            //        return -1;
            //    }
            //}            

            return 1;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucGetSiRegInfo_Load(object sender, EventArgs e)
        {
            this.InitFpFormat();
            this.InitBaseData();
            this.FindForm().Text = "选择医保登记患者信息";

            this.txtInput.Focus();
            this.txtInput.Select();

        }

        /// <summary>
        /// 查询医保登记信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btQuery_Click(object sender, EventArgs e)
        {
            string inputCode = this.txtInput.Text.Trim();
            this.QueryInsRegInfo(inputCode);
        }

        /// <summary>
        /// 查询医保登记信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string inputCode = this.txtInput.Text.Trim();
                this.QueryInsRegInfo(inputCode);
            }
            else if (e.KeyData == Keys.Up)
            {
                if (this.fpSpread_Sheet1.ActiveRow != null && this.fpSpread_Sheet1.ActiveRowIndex > 0)
                {
                    this.fpSpread_Sheet1.ActiveRowIndex--;
                    this.fpSpread.ShowRow(0, this.fpSpread_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
                }
            }
            else if (e.KeyData == Keys.Down)
            {
                if (this.fpSpread_Sheet1.ActiveRow != null && this.fpSpread_Sheet1.ActiveRowIndex < (this.fpSpread_Sheet1.Rows.Count - 1))
                {
                    this.fpSpread_Sheet1.ActiveRowIndex++;
                    this.fpSpread.ShowRow(0, this.fpSpread_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
                }
            }
        }

        /// <summary>
        /// 选择医保登记信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread_Sheet1.ActiveRow == null)
            {
                return;
            }
            if (this.GetInsRegInfo() == 1)
            {
                isOK = true;
                this.FindForm().Close();
            }
        }

        /// <summary>
        /// 选择医保登记信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.fpSpread_Sheet1.ActiveRow == null)
            {
                return;
            }
            if (this.GetInsRegInfo() == 1)
            {
                isOK = true;
                this.FindForm().Close();
            }
        }

        /// <summary>
        /// 选择医保登记信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.fpSpread_Sheet1.ActiveRow == null)
            {
                return;
            }
            if (this.GetInsRegInfo() == 1)
            {
                isOK = true;
                this.FindForm().Close();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCanceL_Click(object sender, EventArgs e)
        {
            this.Clear();
            isOK = false;
            this.FindForm().Close();
        }

        #endregion

    }
}
