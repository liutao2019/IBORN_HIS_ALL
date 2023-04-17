using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Nurse.GuangZhou.Gyzl.IPrintBar
{
    /// <summary>
    /// 门诊打印LIS条码界面
    /// </summary>
    public partial class ucPrintLisCode : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPrintLisCode()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.Load += new EventHandler(ucPrintLisCode_Load);
            }
        }

        #region 变量

        #region 业务层变量

        /// <summary>
        /// 门诊费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 院注管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject InjMgr = new FS.HISFC.BizLogic.Nurse.Inject();

        /// <summary>
        /// 药品管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 挂号管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 参数控制业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 医嘱函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        #endregion

        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 存放科室信息
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 配置文件
        /// </summary>
        private string injectRegisterXml = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\\Profile\\printLisCode.xml";

        FS.HISFC.Models.Pharmacy.Item drug = null;

        FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();

        FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();

        Common com = new Common();

        #endregion

        #region 属性

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucPrintLisCode_Load(object sender, EventArgs e)
        {
            this.dtpStart.Value = DateTime.Now.Date.AddDays(0);
            this.dtpEnd.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            this.InitData();
            this.SetFP();
            this.Clear();
        }

        /// <summary>
        /// 初始化医生
        /// </summary>
        private void InitData()
        {
        }

        /// <summary>
        /// 设置格式
        /// </summary>
        private void SetFP()
        {
            txtType.ReadOnly = true;
            numType.DecimalPlaces = 0;

            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.处方号].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.项目].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.条码号].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.是否接收].CellType = txtType;

            if (System.IO.File.Exists(injectRegisterXml))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }

            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        #region 工具栏

        /// <summary>
        /// 定义工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("打印LIS条码", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "打印LIS条码":
                    this.PrintLisBarCode();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #endregion

        #region 公共

        /// <summary>
        /// 设置颜色(高亮度显示最后一条clinic医嘱)
        /// </summary>
        /// <returns></returns>
        private int ShowColor()
        {
            //取得最大clinic_code
            int maxClinic = 0;
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return -1;
            }
            FS.HISFC.Models.Fee.Outpatient.FeeItemList item = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (FS.FrameWork.Function.NConvert.ToInt32(item.ID) > maxClinic)
                {
                    maxClinic = FS.FrameWork.Function.NConvert.ToInt32(item.ID);
                }
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (item.ID == maxClinic.ToString())
                {
                    this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(i, 0, false);
                }
            }
            return 0;
        }

        /// <summary>
        /// 设置病人信息
        /// </summary>
        /// <param name="patient"></param>
        private void SetPatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (patient == null || patient.ID == "")
            {
                return;
            }
            else
            {
                this.patientInfo = patient;
                this.txtName.Text = patient.Name;
                this.txtSex.Text = patient.Sex.Name;
                this.txtBirthday.Text = patient.Birthday.ToString("yyyy-MM-dd");
                this.txtAge.Text = this.GetAgeByBirthday(patient.Birthday);
                this.txtDiagnoses.Text = com.GetDiagnose(patient.ID);
                this.txtClinicNo.Text = patientInfo.PID.CardNO;
                this.Query();
            }
        }

        private string GetAgeByBirthday(DateTime birthday)
        {
            TimeSpan ts = DateTime.Now - birthday;
            int year = ts.Days / 365;
            return year + "岁";
        }

        #endregion

        #region  打印

        /// <summary>
        /// 打印Lis条码
        /// </summary>
        private void PrintLisBarCode()
        {
            FS.HISFC.BizProcess.Interface.Registration.IPrintBar lisBarPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar)) as FS.HISFC.BizProcess.Interface.Registration.IPrintBar;
            if (lisBarPrint == null)
            {
                return;
            }
            string err = "";
            if (this.patientInfo == null)
            {
                return;
            }
            this.patientInfo.ExtendFlag1 = this.dtpStart.Value.ToString();
            this.patientInfo.ExtendFlag2 = this.dtpEnd.Value.ToString();
            lisBarPrint.printBar(this.patientInfo, ref err);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            neuSpread1_Sheet1.RowCount = 0;

            this.txtPatientName.Text = "";
            this.txtPatientName.Focus();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.checkPatient();
            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            //查询全部LIS项目
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return;
            }
            else
            {
                ArrayList al = com.GetULItemListByClinicNo(this.patientInfo.ID);
                this.AddDetail(al);
            }
        }

        /// <summary>
        /// 添加项目明细
        /// </summary>
        /// <param name="details"></param>
        private void AddDetail(ArrayList details)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            int curRow = 0;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in details)
            {
                curRow = this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Rows[curRow].Locked = true;
                this.neuSpread1_Sheet1.Rows[curRow].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Rows[curRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[curRow, 0].Text = feeItemList.RecipeNO;
                this.neuSpread1_Sheet1.Cells[curRow, 1].Text = feeItemList.Item.Name;
                this.neuSpread1_Sheet1.Cells[curRow, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[curRow, 2].Text = feeItemList.Order.Sample.ID;
                this.neuSpread1_Sheet1.Cells[curRow, 3].Text = feeItemList.ConfirmedInjectCount > 0 ? "是" : "否";
                this.neuSpread1_Sheet1.Cells[curRow, 3].Font = new Font("宋体", 9, FontStyle.Bold);
                if (feeItemList.ConfirmedInjectCount > 0)
                {
                    this.neuSpread1_Sheet1.Rows[curRow].BackColor = Color.LightSkyBlue;
                }
            }
        }

        private int QueryPatientWithULItemByNameAndDate(string name, DateTime start, DateTime end)
        {
            int result = 0;
            frmQueryPatientByName f = new frmQueryPatientByName();
            f.SelectedPatient += new frmQueryPatientByName.GetPatient(SetPatient);

            if (f.QueryByNameAndDate(name, start, end, "UL") > 0)
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    result = 1;
                }
                f.Dispose();//释放资源
            }
            return result;
        }

        private int QueryPatientWithULItemByCardNoAndDate(string cardNo, DateTime start, DateTime end)
        {
            int result = 0;
            frmQueryPatientByName f = new frmQueryPatientByName();
            f.SelectedPatient += new frmQueryPatientByName.GetPatient(SetPatient);
            if (f.QueryByCardNoAndDate(cardNo, start, end, "UL") > 0)
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    result = 1;
                }
                f.Dispose();//释放资源
            }
            return result;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 按卡号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.checkPatient();
            }
        }

        private void checkPatient()
        {
            string input = this.txtPatientName.Text.Trim();
            Regex regex = new Regex("^\\d+$");
            if (!regex.IsMatch(input))
            {
                if (string.IsNullOrEmpty(input))
                {
                    input = "%";
                }

                //根据患者姓名检索需要打印LIS条码的患者信息

                int result = this.QueryPatientWithULItemByNameAndDate(input, this.dtpStart.Value, this.dtpEnd.Value);
                if (result != 1 || this.patientInfo == null)
                {
                    MessageBox.Show("该患者没有检验项目", "提示");
                    this.txtPatientName.Focus();
                    return;
                }
            }
            else
            {
                int result = this.QueryPatientWithULItemByCardNoAndDate(input, this.dtpStart.Value, this.dtpEnd.Value);
                if (result != 1 || this.patientInfo == null)
                {
                    MessageBox.Show("该患者没有检验项目", "提示");
                    this.txtPatientName.Focus();
                    return;
                }
            }

            this.txtPatientName.Focus();
        }

        /// <summary>
        /// 保存显示设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] types = new Type[1];
                types[0] = typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint);
                return types;
            }
        }

        #endregion

        /// <summary>
        /// 列设置枚举
        /// </summary>
        enum EnumColSet
        {
            处方号,
            项目,
            条码号,
            是否接收
        }
    }

}
