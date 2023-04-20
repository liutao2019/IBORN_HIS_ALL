using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY
{
    public partial class ucRegister : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        #region 定义域
        /// <summary>
        /// 门诊费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee patientMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 院注管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject InjMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy drugMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        /// <summary>
        /// 科室函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager DeptMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 人员函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager PsMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 挂号实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register reg = null;
        /// <summary>
        /// 常数业务层函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 药房函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy storeMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 医嘱函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        private ArrayList al = new ArrayList();
      

        /// <summary>
        /// 医生集合
        /// </summary>
        private Hashtable htDoctors = new Hashtable();
        /// <summary>
        /// 病人信息集合
        /// </summary>
        Hashtable hsInfos = new Hashtable();
        /// <summary>
        /// 是否第一次登记
        /// </summary>
        private bool IsFirstTime = false;
     

        /// <summary>
        /// 最大注射顺序号
        /// </summary>
        private int maxInjectOrder = 0;
      
        /// <summary>
        /// 打印在巡视卡上的用法
        /// </summary>
        private string printUsage = "iv.drip(门）;";
      
        /// <summary>
        /// "静脉输液用法（打印瓶签的用法） （维护ID,根据符号;分隔）"
        /// </summary>
        private string injectUsage = "";

        #region 注射顺序号
        /// <summary>
        /// 是否自动生成注射顺序号
        /// </summary>
        private bool IsAutoOrder = true;
        /// <summary>
        /// 当前注射顺序号
        /// </summary>
        private int currentOrder = 0;
        #endregion

        /// <summary>
        /// 是否显示患者当天可登记的全部处方（按频次显示全部）
        /// {24A47206-F111-4817-A7B4-353C21FC7724}
        /// </summary>
        private bool isShowAllInject = false;

        /// <summary>
        /// 用来存储频次实体的字典
        /// {24A47206-F111-4817-A7B4-353C21FC7724}
        /// </summary>
        private Dictionary<string, FS.HISFC.Models.Order.Frequency> dicFrequency = new Dictionary<string, FS.HISFC.Models.Order.Frequency>();

        /// <summary>
        /// 用来存储打印的非药品项目
        /// </summary>
        private Dictionary<string, FS.FrameWork.Models.NeuObject> dicUndrugInject = new Dictionary<string, FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo IGetOrderNo = null;

        #endregion

        #region 属性

        /// <summary>
        /// 配置文件
        /// </summary>
        private string injectRegisterXml = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\\Profile\\injectRegister.xml";

        /// <summary>
        /// 是否显示患者当天可登记的全部处方
        /// {24A47206-F111-4817-A7B4-353C21FC7724}
        /// </summary>
        [Description("是否显示患者当天可登记的全部处方（按频次显示全部）"), Category("设置"), DefaultValue("false")]
        public bool IsShowAllInject
        {
            get
            {
                return isShowAllInject;
            }
            set
            {
                isShowAllInject = value;
            }
        }

      

        [Description("用法是否打印在巡视卡上（维护ID,根据符号;分隔）"), Category("设置")]
        public string Usage
        {
            get
            {
                return this.printUsage;
            }
            set
            {
                this.printUsage = value;
            }
        }


        [Description("静脉输液用法（打印瓶签的用法） （维护ID,根据符号;分隔）"), Category("设置")]
        public string InjectUsage
        {
            get
            {
                return this.injectUsage;
            }
            set
            {
                this.injectUsage = value;
            }
        }

        /// <summary>
        /// 是否一次打印所有院注次数
        /// </summary>
        private bool isSaveAllInjectCount = true;

        /// <summary>
        /// 是否一次打印所有院注次数
        /// </summary>
        [Description("是否一次打印所有院注次数,否则按照每次打印一条"), Category("设置")]
        public bool IsSaveAllInjectCount
        {
            get
            {
                return isSaveAllInjectCount;
            }
            set
            {
                isSaveAllInjectCount = value;
            }
        }

        /// <summary>
        /// 是否自动打印瓶签
        /// </summary>
        private bool isAutoPrint = true;
        [Description("是否自动打印巡视卡"), Category("设置")]
        public bool IsAutoPrint
        {
            get
            {
                return isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }






        /// <summary>
        /// 是否自动保存数据
        /// </summary>
        private bool isAutoSave = true;
        [Description("是否自动保存数据"), Category("设置")]
        public bool IsAutoSave
        {
            get
            {
                return isAutoSave;
            }
            set
            {
                this.isAutoSave = value;
            }
        }

        /// <summary>
        /// 是否自动保存数据
        /// </summary>
        private bool isUserOrderNumber = false;
        [Description("是否使用注射顺序号"), Category("设置")]
        public bool IsUserOrderNumber
        {
            get
            {
                return isUserOrderNumber;
            }
            set
            {
                this.isUserOrderNumber = value;
            }
        }

        /// <summary>
        /// 自动打印时保存是否提示
        /// </summary>
        private bool isMessageInSave = true;
        [Description("自动打印时保存是否提示"), Category("设置")]
        public bool IsMessageInSave
        {
            get
            {
                return isMessageInSave;
            }
            set
            {
                this.isMessageInSave = value;
            }
        }

        /// <summary>
        /// 是否显示有特定用法的非药品
        /// </summary>
        private bool isShowUndrug = true;
        [Description("是否显示有特定用法的非药品"), Category("设置")]
        public bool IsShowUndrug
        {
            get
            {
                return isShowUndrug;
            }
            set
            {
                this.isShowUndrug = value;
            }
        }

       

        #endregion

        #region 初始化
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.dtpStart.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(0 - this.beginDateIntervalDays);
            this.dtpEnd.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(1).AddSeconds(-1);
            this.lbCue.Text = "";
            this.initDoctor();
            this.txtCardNo.Focus();
            this.InitOrder();
        }

        /// <summary>
        /// 初始化医生
        /// </summary>
        private void initDoctor()
        {
            FS.HISFC.BizProcess.Integrate.Manager doctMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            al = doctMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al != null)
            {
                foreach (FS.HISFC.Models.Base.Employee p in al)
                {
                    this.htDoctors.Add(p.ID, p.Name);
                }
            }
        }
        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("全选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("取消", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("打印瓶签", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton( "打印签名卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null );
            this.toolBarService.AddToolButton("打印注射单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印巡视单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton( "打印患者卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null );
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} 增加打印号码条
            this.toolBarService.AddToolButton("打印号码条", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            //{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
            this.toolBarService.AddToolButton("修改皮试", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "全选":
                    this.SelectedAll(true);
                    break;
                case "取消":
                    this.SelectedAll(false);
                    break;
                case "打印瓶签":
                    this.PrintCure();
                    break;
                case "打印签名卡":
                    this.PrintItinerate();
                    break;
                case "打印注射单":
                    this.PrintInject();
                    break;
                case "打印巡视单":
                    this.PrintInjectScoutCard();
                    break;
                case "打印患者卡":
                    this.PrintPatient();
                    break;
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} 增加打印号码条
                case "打印号码条":
                    this.PrintNumber();
                    break;
                //{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
                case "修改皮试":
                    this.ModifyHytest();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 公共
        /// <summary>
        /// 获取医生名称根据代码
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetDoctByID(string ID)
        {
            IDictionaryEnumerator dict = htDoctors.GetEnumerator();
            while (dict.MoveNext())
            {
                if (dict.Key.ToString() == ID)
                    return dict.Value.ToString();
            }

            return "";
        }
        /// <summary>
        /// 设置格式
        /// </summary>
        private void SetFP()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtOnly = new FarPoint.Win.Spread.CellType.TextCellType();
            txtOnly.ReadOnly = true;
            this.neuSpread1_Sheet1.Columns[2].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[3].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[4].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[5].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[6].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[7].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[8].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[9].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[10].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[11].CellType = txtOnly;
            //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
            this.neuSpread1_Sheet1.Columns[13].CellType = txtOnly;

            if (System.IO.File.Exists(injectRegisterXml))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }

            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
        }

        /// <summary>
        /// 获取午别
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetNoon(DateTime dt)
        {
            string strNoon = "上午";
            if (FS.FrameWork.Function.NConvert.ToInt32(dt.ToString("HH")) >= 12)
            {
                strNoon = "下午";
            }
            return strNoon;
        }
        /// <summary>
        /// 压缩显示
        /// </summary>
        private void LessShow()
        {

        }
        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }
      
       
        

        /// <summary>
        /// 只显示最后一次的
        /// </summary>
        /// <returns></returns>
        private int ShowLastOnly()
        {
            //取得最大clinic_code
            int maxClinic = 0;
            if (this.neuSpread1_Sheet1.RowCount <= 0) return -1;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item =
                    (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (FS.FrameWork.Function.NConvert.ToInt32(item.ID) > maxClinic)
                {
                    maxClinic = FS.FrameWork.Function.NConvert.ToInt32(item.ID);
                }
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item =
                    (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (item.ID != maxClinic.ToString())
                {
                    //this.fpSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.LightYellow;
                    this.neuSpread1_Sheet1.SetValue(i, 0, false);
                    this.neuSpread1_Sheet1.Rows[i].Remove();
                }
            }
            return 0;
        }
        /// <summary>
        /// 获得最大注射顺序
        /// </summary>
        /// <returns></returns>
        private int GetMaxInjectOrder()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0) return 0;
            this.neuSpread1.StopCellEditing();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                    this.neuSpread1_Sheet1.GetText(i, 0) == "") continue;
                if (FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 1].Text) > maxInjectOrder)
                {
                    maxInjectOrder = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 1].Text);
                }
            }
            return maxInjectOrder;
        }

        /// <summary>
        /// 设置病人信息
        /// </summary>
        /// <param name="reg"></param>
        private void SetPatient(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg == null || reg.ID == "")
            {
                return;
            }
            else
            {
                int iAge = FS.FrameWork.Function.NConvert.ToInt32(System.DateTime.Now.ToString("yyyy"))
                    - FS.FrameWork.Function.NConvert.ToInt32(reg.Birthday.ToString("yyyy"));
                this.txtName.Text = reg.Name;
                this.txtSex.Text = reg.Sex.Name;
                this.txtBirthday.Text = reg.Birthday.ToString("yyyy-MM-dd");
                this.txtAge.Text = this.InjMgr.GetAge(reg.Birthday);//iAge.ToString();
                this.txtCardNo.Text = reg.PID.CardNO;

                #region 诊断
                ArrayList alDiagnoses = new ArrayList();
                alDiagnoses = (new FS.HISFC.BizLogic.HealthRecord.Diagnose()).QueryDiagnoseNoOps(reg.ID);
                this.txtDiagnoses.Text = "";
                if (alDiagnoses != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose dg in alDiagnoses)
                    {
                        if (dg.Memo == reg.ID || dg.Is30Disease == "0")
                        {//把非本次挂号的诊断过滤掉，如果以后诊断很多，通过这种方式很慢的话，要重写一个业务层
                            continue;
                        }
                        else
                        {//把本次挂号的诊断连接起来放到lblDiagnose里
                            this.txtDiagnoses.Text += dg.DiagInfo.ICD10.Name + " ";
                        }
                    }
                }
                else
                {
                    this.txtDiagnoses.Text = "";
                }
                #endregion
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void SelectAll(bool isSelected)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //{FAC1693A-3EBA-44b3-A1E3-6D6750A98D80}
                //this.neuSpread1_Sheet1.SetValue(i, 0, isSelected, false);
                this.neuSpread1_Sheet1.Cells[i, 0].Value = isSelected;
            }
        }
		

        #endregion

        #region  打印
        /// <summary>
        /// 打印患者卡
        /// </summary>
        private void PrintPatient()
        {
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint patientPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            if (patientPrint == null)
            {
                patientPrint = new FS.HISFC.Components.Nurse.Print.ucPrintPatient() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
                //Nurse.Print.ucPrintPatient uc = new Nurse.Print.ucPrintPatient();
            }
            patientPrint.Init(al);
        }
        /// <summary>
        /// 打印瓶签
        /// </summary>
        private void PrintCure()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint curePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            if (curePrint == null)
            {
                curePrint = new FS.HISFC.Components.Nurse.Print.ucPrintCure() as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            }
            curePrint.Init(al);
        }
        /// <summary>
        /// 打印注射单.
        /// </summary>
        private void PrintInject()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint injectPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            if (injectPrint == null)
            {
                injectPrint = new FS.HISFC.Components.Nurse.Print.ucPrintInject() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            }
            injectPrint.Init(al);
        }
        /// <summary>
        /// 打印签名卡
        /// </summary>
        private void PrintItinerate()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new FS.HISFC.Components.Nurse.Print.ucPrintItinerate() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = false;
            itineratePrint.Init(al);
        }
        /// <summary>
        /// 连续纸张的签名卡
        /// </summary>
        private void PrintItinerateLarge()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new FS.HISFC.Components.Nurse.Print.ucPrintItinerateLarge() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = false;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// {E73C3DF3-9DCF-4a46-ADE0-3D44663F6F7A}
        /// 打印静脉输液巡视卡
        /// </summary>
        private void PrintInjectScoutCard()
        { 
            int intReturn = this.GetAllPrintInjectList();
            if (intReturn==-1)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            int intCount = 0;
            foreach (ArrayList al in hsInfos.Values)
            {
                FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
                if (itineratePrint == null)
                {
                    return;
                }

                itineratePrint.IsReprint = false;
                itineratePrint.Init(al);
            }
           
        }

        /// <summary>
        /// {30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} 增加号码条
        /// </summary>
        private void PrintNumber()
        {
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
            ArrayList al = this.GetPrintInjectList();
            //			this.maxInjectOrder = 0;
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint numberPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            if (numberPrint == null)
            {
                numberPrint = new FS.HISFC.Components.Nurse.Print.ucPrintNumber() as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
                //Nurse.Print.ucPrintNumber uc = new Nurse.Print.ucPrintNumber();
            }
            numberPrint.Init(al);
        }

        /// <summary>
        /// 获取要打印的数据
        /// (打印瓶签)
        /// </summary>
        /// <returns></returns>
        private ArrayList GetPrintInjectList()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            this.neuSpread1.StopCellEditing();

            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                {
                    continue;
                }

                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, 11].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();
                info.Patient = reg;

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 2].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString();

                if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (!injectUsage.Contains(detail.Order.Usage.ID+";"))
                    {
                        continue;
                    }
                }


                //医生名称
                if (hsEmpl.Contains(detail.RecipeOper.ID))
                {
                    empl = hsEmpl[detail.RecipeOper.ID] as FS.HISFC.Models.Base.Employee;
                }
                else
                {
                    empl = this.PsMgr.GetEmployeeInfo(detail.RecipeOper.ID);

                    if (empl == null)
                    {
                        MessageBox.Show("获取人员信息失败:" + PsMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                        return new ArrayList();

                    }
                    hsEmpl.Add(empl.ID, empl);
                }

                info.Item.Order.ReciptDoctor.Name = empl.Name;
                info.Item.Order.ReciptDoctor.ID = empl.ID;
                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                }
                info.InjectOrder = strOrder;
                al.Add(info);
                //判断接瓶,如果是则添加到alJiePing中
                if (orderinfo.ExtendFlag1 == null || orderinfo.ExtendFlag1.Length < 1)
                    orderinfo.ExtendFlag1 = "1|";
                //				string[] str = orderinfo.Mark1.Split('|');
                int inum = FS.FrameWork.Function.NConvert.ToInt32(orderinfo.ExtendFlag1.Substring(0, 1));
                info.Memo = inum.ToString();
                //FS.neFS.HISFC.Components.Function.NConvert.ToInt32(str[0]);
                //				if(inum > 1)
                //				{
                //						FS.HISFC.Models.Nurse.Inject inj = new FS.HISFC.Models.Nurse.Inject();
                //						inj = info.Clone();
                //						inj.InjectOrder = (this.GetMaxInjectOrder() + 1).ToString();
                //						maxInjectOrder++;
                //						alJiePing.Add(inj);
                //					}
                //				}

                //{EB016FFE-0980-479c-879E-225462ECA6D0}
                info.PrintNo = detail.User02;
            }
            return al;
        }

        /// <summary>
        /// 是否需要打印的用法
        /// </summary>
        /// <param name="usageCode"></param>
        /// <returns></returns>
        private bool IsPrintUsage(string usageCode)
        {
            //如果界面没有维护打印用法时，根据院注用法打印
            if (string.IsNullOrEmpty(printUsage))
            {
                return SOC.HISFC.BizProcess.Cache.Common.IsInnerInjectUsage(usageCode);
            }
            else
            {
                return printUsage.Contains(usageCode + ";");
            }
        }
       
        /// <summary>
        /// 获取要打印的数据（可维护用法）
        ///{0D976883-8A45-4a97-AFEF-7D8ED425C89A}
        /// </summary>
        /// <returns></returns>
        private int GetAllPrintInjectList()
        {
            this.SelectAll(true);
            this.neuSpread1.StopCellEditing();
            hsInfos.Clear();
            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                    continue;
                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, 11].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();
                if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (!IsPrintUsage(detail.Order.Usage.ID))
                    {
                        continue;
                    }
                    //if (!printUsage.Contains(detail.Order.Usage.ID + ";"))
                    //{
                    //    continue;
                    //}
                }

                info.Patient.ID = detail.Patient.ID;
                info.Patient.Name = reg.Name;
                info.Patient.Sex.ID = reg.Sex.ID;
                info.Patient.Birthday = reg.Birthday;
                info.Patient.Card.ID = this.txtCardNo.Text.Trim().PadLeft(10, '0');

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 2].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString();

                //医生名称
                if (hsEmpl.Contains(detail.RecipeOper.ID))
                {
                    empl = hsEmpl[detail.RecipeOper.ID] as FS.HISFC.Models.Base.Employee;
                }
                else
                {
                    empl = this.PsMgr.GetEmployeeInfo(detail.RecipeOper.ID);

                    if (empl == null)
                    {
                        MessageBox.Show("获取人员信息失败:" + PsMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                        return -1;
                    }
                    hsEmpl.Add(empl.ID, empl);
                }
                if (empl == null)
                {
                    MessageBox.Show("找不到相关医生信息");
                    return -1;
                }
                info.Item.Order.ReciptDoctor.Name = empl.Name;
                info.Item.Order.ReciptDoctor.ID = empl.ID;
                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                }
                info.Item.Days = detail.Days;
                //患者当次注射处方时间
                info.InjectOrder = strOrder;
                
                info.User03 = this.neuSpread1_Sheet1.Cells[i, 13].Text;

                string hypoTest = string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 11].Text) ? "" : "(" + this.neuSpread1_Sheet1.Cells[i, 11].Text + ")";

                if (orderinfo != null)
                {
                    //备注应该是Memo+皮试
                    //info.Memo = orderinfo.ExtendFlag1;
                    info.Memo = orderinfo.Memo;
                    info.Hypotest = orderinfo.HypoTest;
                }

                if (!hsInfos.ContainsKey(info.Item.Order.ReciptDoctor.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(info);
                    hsInfos.Add(info.Item.Order.ReciptDoctor.ID, al);
                }
                else
                {
                    ((ArrayList)hsInfos[info.Item.Order.ReciptDoctor.ID]).Add(info);
                }
            }
            return 1;
        }

        /// <summary>
        /// 获取最优先的使用方法
        /// </summary>
        /// <param name="IsInit">是否初始化</param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Const GetFirstUsage()
        {
            FS.HISFC.Models.Fee.Outpatient.FeeItemList info = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            if (this.neuSpread1_Sheet1.RowCount <= 0) return new FS.HISFC.Models.Base.Const();

            int FirstCodeNum = 10000;
            FS.HISFC.Models.Base.Const retobj = new FS.HISFC.Models.Base.Const();
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    info = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                    FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstant("SPECIAL", info.Order.Usage.ID);
                    FS.HISFC.Models.Base.Const conobj = (FS.HISFC.Models.Base.Const)obj;

                    if (conobj.SortID < FirstCodeNum)
                    {
                        FirstCodeNum = conobj.SortID;
                        retobj = conobj;
                    }
                }
            }
            catch
            {
                return retobj;
            }

            return retobj;
        }
        #endregion	

        #region 注射顺序号的处理
        /// <summary>
        /// 设置默认注射顺序
        /// </summary>
        private void SetInject()
        {

            #region  没有数据就不管了,直接返回
            if (this.neuSpread1_Sheet1.RowCount <= 0) return;
            #endregion

            #region 设置患者今天的注射顺序号
            if (this.chkIsOrder.Checked)
            {
                this.SetOrder();
            }
            else
            {
                this.txtOrder.Text = "0";
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 14].Text = this.txtOrder.Text;
                }
            }
            #endregion

            #region 设置每组项目的注射顺序
            int InjectOrder = 1;
            this.neuSpread1_Sheet1.SetValue(0, 1, 1, false);
            for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
            {

                if (this.neuSpread1_Sheet1.Cells[i, 7].Text == null || this.neuSpread1_Sheet1.Cells[i, 7].Text.Trim() == "")
                {
                    InjectOrder++;
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else if (this.neuSpread1_Sheet1.Cells[i, 7].Text != null && this.neuSpread1_Sheet1.Cells[i, 7].Text.Trim() != ""
                    //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                    && this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, 13].Text == this.neuSpread1_Sheet1.Cells[i - 1, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i - 1, 13].Text)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else
                {
                    InjectOrder++;
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
            }
            #endregion

        }

        /// <summary>
        /// 初始化注射顺序号
        /// </summary>
        private void InitOrder()
        {
            //读取是否自动生成注射顺序
            try
            {
                bool isAutoInjectOrder = false;
                isAutoInjectOrder = FS.FrameWork.Function.NConvert.ToBoolean(this.conMgr.QueryControlerInfo("900005"));
                if (isAutoInjectOrder)
                {
                    this.chkIsOrder.Checked = true;
                    this.SetOrder();
                    this.lbLastOrder.Text = "今天最后一次注射号:" +
                        (FS.FrameWork.Function.NConvert.ToInt32(this.txtOrder.Text.Trim()) - 1).ToString();
                }
                else
                {
                    this.chkIsOrder.Checked = false;
                    this.lbLastOrder.Text = "本机不自动生成注射顺序号!";
                    this.txtOrder.Text = "0";
                }
                //XmlDocument doc = new XmlDocument();
                //doc.Load(Application.StartupPath + "/Setting/NurseSetting.xml");
                //XmlNode node = doc.SelectSingleNode("//是否生成注射顺序");

                //if (node != null && node.Attributes["isAutoInjectOrder"].Value.ToString() == "false")
                //{
                //    this.chkIsOrder.Checked = false;
                //    this.lbLastOrder.Text = "本机不自动生成注射顺序号!";
                //    this.txtOrder.Text = "0";
                //}
                //else
                //{
                //    this.chkIsOrder.Checked = true;
                //    this.SetOrder();
                //    this.lbLastOrder.Text = "今天最后一次注射号:" +
                //        (FS.FrameWork.Function.NConvert.ToInt32(this.txtOrder.Text.Trim()) - 1).ToString();
                //}
            }
            catch //无配置文件
            {
                this.chkIsOrder.Checked = false;
                this.lbLastOrder.Text = "本机不自动生成注射顺序号!";
                this.txtOrder.Text = "0";
            }


        }
        /// <summary>
        /// 设置注射号
        /// </summary>
        private void SetOrder()
        {
            if (!this.chkIsOrder.Checked)
            {
                this.txtOrder.Text = "0";
                this.lbLastOrder.Text = "现在本机没有设置自动生成序号!";
                return;
            }
            //如果自动生成,设置第一个序号,并赋值this.currentOrder
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} 改为通过接口实现，如果没有配置则按原代码生程序号
            this.CreateInterface();
            if (IGetOrderNo != null)
            {
                string orderNo = IGetOrderNo.GetOrderNo(this.reg);
                this.txtOrder.Text = orderNo;
                if (this.neuSpread1_Sheet1.Rows.Count == 0)
                {
                    return;
                }
                string comboAndInjectTime = this.neuSpread1_Sheet1.Cells[0, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[0, 13].Text;
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    string rowComboAndInjectTime = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, 13].Text;
                    if (comboAndInjectTime != rowComboAndInjectTime)
                    {
                        comboAndInjectTime = rowComboAndInjectTime;
                        orderNo = IGetOrderNo.GetSamePatientNextOrderNo(orderNo);
                    }
                    this.neuSpread1_Sheet1.Cells[i, 14].Text = orderNo;
                }
                return;
            }
            else
            {
                FS.HISFC.Models.Nurse.Inject info = this.InjMgr.QueryLast();
                if (info != null && info.Booker.OperTime != System.DateTime.MinValue)
                {
                    if (info.Booker.OperTime.ToString("yyyy-MM-dd")
                        == this.InjMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd"))
                    {
                        this.txtOrder.Text = (FS.FrameWork.Function.NConvert.ToInt32(info.OrderNO) + 1).ToString();
                    }
                    else
                    {
                        this.txtOrder.Text = "1";
                    }
                }
                else
                {
                    this.txtOrder.Text = "1";
                }
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 14].Text = this.txtOrder.Text;
                }
            }
        }

        /// <summary>
        /// 创建接口
        /// {30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
        /// </summary>
        private void CreateInterface()
        {
            if (this.IGetOrderNo == null)
            {
                this.IGetOrderNo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo)) as FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 确认保存( 1.met_nuo_inject插入记录  2.fin_ipb_feeitemlist更新已确认院注数量，确认标志)
        /// </summary>
        private int Save()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("没有要保存的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            
            this.neuSpread1.StopCellEditing();
            int selectNum = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE" || this.neuSpread1_Sheet1.GetValue(i, 0).ToString() == "")
                {
                    selectNum++;
                }
            }
            if (selectNum >= this.neuSpread1_Sheet1.RowCount)
            {
                MessageBox.Show("请选择要保存的数据", "提示");
                return -1;
            }

            if (this.isUserOrderNumber)
            {
                #region 判断输入队列号的有效性
                if (this.txtOrder.Text == null || this.txtOrder.Text.Trim().ToString() == "")
                {
                    MessageBox.Show("没有输入队列顺序号!");
                    this.txtOrder.Focus();
                    return -1;
                }
           
                else if (this.InjMgr.QueryInjectOrder(this.txtOrder.Text.Trim().ToString()).Count > 0)
                {
                    if (MessageBox.Show("该队列号已经使用,是否继续!", "提示", System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        this.txtOrder.Focus();
                        return -1;
                    }
                }
             
                #endregion


                #region 检查注射顺序号的有效性（组号相同的，注射顺序号也必须相同）
                for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 7].Tag != null && this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() != "" &&
                        //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                        this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, 13].Text == this.neuSpread1_Sheet1.Cells[i - 1, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i - 1, 13].Text
                        && this.neuSpread1_Sheet1.GetValue(i, 1).ToString() != this.neuSpread1_Sheet1.GetValue(i - 1, 1).ToString()
                        )
                    {
                        MessageBox.Show("相同组号的注射顺序号必须相同!", "第" + (i + 1).ToString() + "行");
                        return -1;
                    }
                }
                #endregion
            }

            #region 检查院注次数的有效性（组号相同的，注射顺序号也必须相同）
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                string strnum = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                if (strnum == null || strnum == "")
                {
                    MessageBox.Show("院注次数不能为空!", "第" + (i + 1).ToString() + "行");
                    return -1;
                }
                if (!this.IsNum(strnum))
                {
                    MessageBox.Show("院注次数必须为数字!", "第" + (i + 1).ToString() + "行");
                    return -1;
                }
                string completenum = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "TRUE")
                    if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) == 0)
                    {
                        continue;
                    }

                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) <= FS.FrameWork.Function.NConvert.ToInt32(completenum))
                    {
                        MessageBox.Show("院注次数已满!", "第" + (i + 1).ToString() + "行");
                        return -1;
                    }
                }
             
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            try
            {
                this.InjMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.drugMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.DeptMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.PsMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                DateTime confirmDate = this.InjMgr.GetDateTimeFromSysDateTime();

                FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
                FS.HISFC.Models.Nurse.Inject info = null;

                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                        this.neuSpread1_Sheet1.GetText(i, 0) == "")
                    {
                        continue;
                    }
                    detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;

                 

                    info = new FS.HISFC.Models.Nurse.Inject();

                    #region 实体转化（门诊项目收费明细实体FeeItemList－->注射实体Inject）

                    info.Patient = reg;
                    info.Patient.ID = detail.Patient.ID;
                    info.Patient.Name = reg.Name;
                    info.Patient.Sex.ID = reg.Sex.ID;
                    info.Patient.Birthday = reg.Birthday;
                    info.Patient.PID.CardNO= reg.PID.CardNO;

                    //info.Item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)detail.Item;
                    info.Item = detail;
                    info.Item.ID = detail.Item.ID;
                    info.Item.Name = detail.Item.Name;

                    //info.Item.Item.MinFee.ID = detail.Item.MinFee.ID;
                    //info.Item.Item.Price = detail.Item.Price;
                    
               

                    

                    info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 2].Text);
                    //开单科室名称
                    if (hsDept.Contains(detail.RecipeOper.Dept.ID))
                    {
                        dept = hsDept[detail.RecipeOper.Dept.ID] as FS.HISFC.Models.Base.Department;
                    }
                    else
                    {
                        dept = this.DeptMgr.GetDepartment(detail.RecipeOper.Dept.ID); 
                        if (dept == null)
                        {
                            MessageBox.Show("获取开立科室错误:" + DeptMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                            return -1;

                        }
                        hsDept.Add(dept.ID, dept);
                    }

                    info.Item.Order.DoctorDept.Name = dept.Name;
                    info.Item.Order.DoctorDept.ID = dept.ID;
                    //医生名称
                    if (hsEmpl.Contains(detail.RecipeOper.ID))
                    {
                        empl = hsEmpl[detail.RecipeOper.ID] as FS.HISFC.Models.Base.Employee;
                    }
                    else
                    {
                        empl = this.PsMgr.GetEmployeeInfo(detail.RecipeOper.ID);
                        if (empl == null)
                        {
                            MessageBox.Show("获取人员信息失败:" + PsMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                            return -1;

                        }
                        hsEmpl.Add(empl.ID, empl);
                    }

                    info.Item.Order.ReciptDoctor.Name = empl.Name;
                    info.Item.Order.ReciptDoctor.ID = empl.ID;
                    //是否皮试
                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && this.neuSpread1_Sheet1.Cells[i, 11].Tag.ToString().ToUpper() == "TRUE")
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                    }
                    else
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                    }
                    #endregion

                    info.ID = this.InjMgr.GetSequence("Nurse.Inject.GetSeq");
                    //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                    //info.OrderNO = this.txtOrder.Text.ToString();
                    info.OrderNO = this.neuSpread1_Sheet1.Cells[i, 14].Text;
                    //{24A47206-F111-4817-A7B4-353C21FC7724}
                    info.PrintNo = detail.User02;
                    info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString();
                    info.Booker.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    info.Booker.OperTime = confirmDate;
                    info.Item.ExecOper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    string strOrder = "";
                    if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                    {
                        strOrder = "";
                    }
                    else
                    {
                        strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                    }
                    info.InjectOrder = strOrder;
                    info.Item.Days = detail.Days;
                    string hypoTest = this.neuSpread1_Sheet1.Cells[i, 11].Text;

                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //备注--(取医嘱备注)
                        FS.HISFC.Models.Order.OutPatient.Order orderinfo =
                            (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, 11].Tag;
                        if (orderinfo != null)
                        {
                            //备注应该是Memo+皮试
                            //info.Memo = orderinfo.ExtendFlag1;
                            info.Memo = orderinfo.Memo;
                            info.Hypotest = orderinfo.HypoTest;
                        }
                    }

                    #region 向met_nuo_inject中，插入记录
                    if (this.InjMgr.Insert(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.InjMgr.Err, "提示");
                        return -1;
                    }
                    #endregion

                    #region 向fin_ipb_feeitemlist中，插入数量

                    //更新的院注次数
                    int updateInjectCount = 1;
                    //默认保存一天的注射次数
                    //int updateInjectCount = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(detail.Order.Frequency.ID);
                    if (isSaveAllInjectCount)
                    {
                        if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            updateInjectCount = detail.InjectCount;
                        }
                        else
                        {
                            updateInjectCount = FS.FrameWork.Function.NConvert.ToInt32(detail.Item.Qty);
                        }
                    }
                    if (this.patientMgr.UpdateConfirmInject(detail.Order.ID, detail.RecipeNO, detail.SequenceNO.ToString(), updateInjectCount) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.patientMgr.Err, "提示");
                        return -1;
                    }
                    #endregion
                    info.Item.InjectCount = info.Item.InjectCount;
                   
                    this.lbLastOrder.Text = "今天最后一次注射号:" + info.OrderNO;

                }
                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            if (this.isMessageInSave)
            {
                MessageBox.Show("保存成功!", "提示");
            }
            this.Clear();
            this.lbCue.Text = "";

            this.txtCardNo.SelectAll();
            this.txtRecipe.Text = "";
            this.txtCardNo.Text = "";
            this.txtCardNo.Focus();
            return 0;
        }
        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            this.txtOrder.Text = "";
            this.txtRecipe.Text = "";
            
            this.txtName.Text = string.Empty;
            this.txtDiagnoses.Text = string.Empty;
            this.txtBirthday.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.txtSex.Text = string.Empty;


            reg = null;
            this.txtCardNo.Focus();
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }
            string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');

            //获取医生开立的处方信息（没有全部执行完的）
            DateTime dtFrom = this.dtpStart.Value.Date;
            DateTime dtTo = this.dtpEnd.Value.Date.AddDays(1);

            if (this.txtRecipe.Text.Trim() == "")
            {
                al = this.patientMgr.QueryOutpatientFeeItemListsZs(cardNo, dtFrom, dtTo);
            }
            else
            {
                al = this.patientMgr.QueryFeeDetailFromRecipeNO(this.txtRecipe.Text.Trim());

                if (al == null)
                {
                    return;
                }

                if (al.Count > 0)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)al[0];
                    reg = this.regMgr.GetByClinic(item.Patient.ID);
                    if (reg == null || reg.ID == "")
                    {
                        MessageBox.Show("没有病历号为:" + item.Patient.ID + "的患者!", "提示");
                        this.txtCardNo.Focus();
                        this.Clear();
                        return;
                    }

                    this.txtCardNo.Text = item.Patient.PID.CardNO;
                    this.SetPatient(reg);
                    this.txtRecipe.Text = "";
                    this.Query();

                    return;
                }

            }

            this.Query(al);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private void Query(ArrayList al)
        {
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("该患者没有需要确认的医嘱信息!", "提示");
                this.txtCardNo.Focus();
                return;
            }

            this.AddDetail(al);

            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("该时间段内没有该患者信息!", "提示");
                this.txtCardNo.Focus();
                return;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                int confirmNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 3].Text);
                if (confirmNum == 0)
                {
                    this.lbCue.Text = "首次院注,请核对院注次数!";
                    this.lbCue.ForeColor = System.Drawing.Color.Magenta;
                }
            }

            this.SelectAll(true);
            this.SetComb();

            this.SetFP();

            this.txtOrder.Focus();
            if (this.isUserOrderNumber)
            {
                this.SetInject();
            }

            //打印巡视卡
            if (this.isAutoPrint)
            {
                if (IsFirstTime)
                {
                    this.PrintInjectScoutCard();
                }
                this.PrintCure();
            }

            if (this.isAutoSave)
            {
                this.SelectAll(true);
                this.Save();
            }
        }
        /// <summary>
        /// 添加项目明细
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(ArrayList details)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }

            //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
            List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> tmpFeeList = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
            if (details != null)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in al)
                {
                    if (detail.CancelType == FS.HISFC.Models.Base.CancelTypes.Canceled)
                    {
                        continue;
                    }
                    if (detail.TransType == FS.HISFC.Models.Base.TransTypes.Negative)
                    {
                        continue;
                    }


                    #region 判断是否需要打印注射单
                    if (detail.ConfirmedInjectCount == 0)
                    {
                        IsFirstTime = true;
                    }
                    #endregion

                    #region  院注次数 <= 已确认院注次数 的不显示

                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //院注次数 <= 已确认院注次数 的不显示
                        if (detail.InjectCount != 0 && detail.InjectCount <= detail.ConfirmedInjectCount && !this.cbFinish.Checked)
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region  判断非药品是否显示
                    if (isShowUndrug)
                    {
                        if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                        {
                            //增加院注次数等于数量的不显示
                            if (detail.ConfirmedInjectCount == detail.Item.Qty)
                            {
                                continue;
                            }

                            if (!dicUndrugInject.ContainsKey(detail.Item.ID))
                            {
                                continue;
                            }

                        }
                    }
                    else
                    {
                        if (detail.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            continue;
                        }
                    }
                    #endregion

                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        #region 维护那些用法显示在界面上
                        if (!IsPrintUsage(detail.Order.Usage.ID ))
                        {
                            continue;
                        }
                        //if (!printUsage.Contains(detail.Order.Usage.ID))
                        //{
                        //    continue;
                        //}
                        #endregion
                    }

                    #region 是否显示0次数的
                    if (!chkNullNum.Checked && detail.InjectCount == 0)
                    {
                        continue;
                    }
                    #endregion

                    #region 今天已经登记的QD不显示，注射两次的BID不显示，当前午别注射一次的BID不再显示。(根据今天的登记时间)
                    DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(
                        this.InjMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd 00:00:00"));
                    ArrayList alTodayInject = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO, detail.SequenceNO.ToString(), dt);
                    if (!this.dicFrequency.ContainsKey(detail.Order.Frequency.ID))
                    {
                        continue;
                    }

                    FS.HISFC.Models.Order.Frequency frequence = this.dicFrequency[detail.Order.Frequency.ID];
                    string[] injectTime = frequence.Time.Split('-');
                    
                    //当天的已经全部注射完毕后跳过
                    if (alTodayInject.Count >= injectTime.Length)
                    {
                        continue;
                    }
                    if (this.isShowAllInject)
                    {
                        for (int i = alTodayInject.Count; i < injectTime.Length; i++)
                        {
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList newDetail = detail.Clone();
                            newDetail.User03 = injectTime[i];
                            tmpFeeList.Add(newDetail);
                        }
                    }
                    else
                    {
                        //未过上次注射时间的话不允许再次登记
                        if (alTodayInject.Count > 0)
                        {
                            DateTime lastInjectTime = FrameWork.Function.NConvert.ToDateTime(dt.ToString("yyyy-MM-dd ") + injectTime[alTodayInject.Count - 1] + ":00");
                            if (this.InjMgr.GetDateTimeFromSysDateTime() < lastInjectTime)
                            {
                                continue;
                            }
                        }
                        detail.User03 = injectTime[alTodayInject.Count];
                        tmpFeeList.Add(detail);
                    }
                    #endregion
                }

                //排序
                tmpFeeList.Sort(new FeeItemListSort());
                //获取打印序号
                this.CreateInterface();
                if (this.IGetOrderNo != null)
                {
                    this.IGetOrderNo.SetPrintNo(new ArrayList(tmpFeeList.ToArray()));
                }
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in tmpFeeList)
                {
                    this.AddDetail(feeItem);
                }

                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.LessShow();
                }
            }
        }

        FS.HISFC.Models.Pharmacy.Item drug = null;

        /// <summary>
        /// 开立科室
        /// </summary>
        FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();

        /// <summary>
        /// 员工信息
        /// </summary>
        FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();

        Hashtable hsEmpl = new Hashtable();

        FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;

        /// <summary>
        /// 存放科室信息
        /// </summary>
        Hashtable hsDept = new Hashtable();

        FS.HISFC.Models.Pharmacy.ApplyOut applyOutObj = null;

        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList info)
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            int row = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Rows[row].Tag = info;

            #region "窗口赋值"
            #region 获取皮试信息
            string strTest = "否";
            //获取皮试信息
            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                drug = this.drugMgr.GetItem(info.Item.ID);
                if (drug == null)
                {
                    MessageBox.Show("获取药品皮试信息失败!");
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                    return;
                }
                strTest = "否";
                if (drug.IsAllergy)
                {
                    strTest = "是";
                }
            }
            //
            #endregion

            if (hsDept.Contains(info.RecipeOper.Dept.ID))
            {
                dept = hsDept[info.RecipeOper.Dept.ID] as FS.HISFC.Models.Base.Department;
            }
            else
            {
                dept = this.DeptMgr.GetDepartment(info.RecipeOper.Dept.ID);
                if (dept == null)
                {
                    MessageBox.Show("获取开立科室错误:" + DeptMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                    return;

                }
                hsDept.Add(dept.ID, dept);
            }

            //{765D340F-879B-4761-9FFA-E1A629886025}判断医生科室有没有维护
            if (dept!= null)
            {
                info.Order.DoctorDept.Name = dept.Name;
            }
            else
            {
                MessageBox.Show("医生科室没有维护");
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                return;
            }

            this.neuSpread1_Sheet1.SetValue(row, 1, "", false);//注射顺序号
            this.neuSpread1_Sheet1.SetValue(row, 2, info.InjectCount.ToString(), false);//院注次数
            this.neuSpread1_Sheet1.SetValue(row, 3, info.ConfirmedInjectCount.ToString(), false);//已经确认的院注次数
            this.neuSpread1_Sheet1.SetValue(row, 4, this.GetDoctByID(info.RecipeOper.ID), false);//开单医生
            this.neuSpread1_Sheet1.Cells[row, 4].Tag = info.Order.ReciptDoctor.ID;
            this.neuSpread1_Sheet1.SetValue(row, 5, dept.Name, false);//科别
            this.neuSpread1_Sheet1.Cells[row, 5].Tag = info.Order.DoctorDept.ID;
            this.neuSpread1_Sheet1.SetValue(row, 6, info.Item.Name, false);//药品名称
            this.neuSpread1_Sheet1.Cells[row, 7].Tag = info.Order.Combo.ID;//组合号
            this.neuSpread1_Sheet1.SetValue(row, 8, info.Order.DoseOnce.ToString() + info.Order.DoseUnit, false);//每次量
            this.neuSpread1_Sheet1.SetValue(row, 9, info.Order.Frequency.ID, false);//频次
            this.neuSpread1_Sheet1.Cells[row, 9].Tag = info.Order.Frequency.ID.ToString();
            this.neuSpread1_Sheet1.SetValue(row, 10, info.Order.Usage.Name, false);//用法
            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                this.neuSpread1_Sheet1.Cells[row, 11].Tag = drug.IsAllergy.ToString().ToUpper();
            }
            orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();

            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                orderinfo = orderMgr.GetOneOrder(info.Patient.ID, info.Order.ID);
                if (orderinfo != null)
                {
                    this.neuSpread1_Sheet1.SetText(row, 12, string.IsNullOrEmpty(orderinfo.Memo) ? " " : orderinfo.Memo);

                    if (orderinfo.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest)
                    {
                        if (!drug.IsAllergy)
                        {
                            orderinfo.HypoTest = 0;//免试的值为零
                        }
                    }
                    this.neuSpread1_Sheet1.Cells[row, 11].Text = this.GetHyTestInfo(orderinfo.HypoTest.ToString());
                    this.neuSpread1_Sheet1.Cells[row, 11].Tag = orderinfo;
                }
                else
                {
                    orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();
                    if (drug.IsAllergy)
                    {
                        orderinfo.Item = drug;
                        orderinfo.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest;
                    }
                    else
                    {
                        orderinfo.HypoTest = 0;
                    }

                    this.neuSpread1_Sheet1.Cells[row, 11].Text = this.GetHyTestInfo(orderinfo.HypoTest.ToString());
                    this.neuSpread1_Sheet1.Cells[row, 11].Tag = orderinfo;

                }
            }
            //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
            this.neuSpread1_Sheet1.Cells[row, 13].Text = info.User03;
            #endregion
        }

        /// <summary>
        /// 获取皮试信息
        /// </summary>
        /// <param name="hytestID"></param>
        /// <returns></returns>
        private string GetHyTestInfo(string hytestID)
        {
            switch (hytestID)
            {
                case "1":
                    return "免试";
                case "2":
                    return "需皮试";
                case "3":
                    return "阳性";
                case "4":
                    return "阴性";
                case "0":
                default:
                    return "否";
            }
        }

        /// <summary>
        /// 设置组合号
        /// </summary>
        private void SetComb()
        {
            int myCount = this.neuSpread1_Sheet1.RowCount;
            int i;
            //第一行
            this.neuSpread1_Sheet1.SetValue(0, 7, "┓");
            //最后行
            this.neuSpread1_Sheet1.SetValue(myCount - 1, 7, "┛");
            //中间行
            for (i = 1; i < myCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString();
                string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, 7].Tag.ToString();
                string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, 7].Tag.ToString();

                //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                string currentRowInjectTime = this.neuSpread1_Sheet1.Cells[i, 13].Text.ToString();
                string priorRowInjectTime = this.neuSpread1_Sheet1.Cells[prior, 13].Text.ToString();
                string nextRowInjectTime = this.neuSpread1_Sheet1.Cells[next, 13].Text.ToString();

                #region """""
                bool bl1 = true;
                bool bl2 = true;
                //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                if (currentRowCombNo + currentRowInjectTime != priorRowCombNo + priorRowInjectTime)
                    bl1 = false;
                if (currentRowCombNo + currentRowInjectTime != nextRowCombNo + nextRowInjectTime)
                    bl2 = false;
                //  ┃
                if (bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "┃");
                }
                //  ┛
                if (bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "┛");
                }
                //  ┓
                if (!bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "┓");
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "");
                }
                #endregion
            }
            //把没有组号的去掉
            for (i = 0; i < myCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() == "")
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "");
                }
            }
            //判断首末行 有组号，且只有自己一组数据的情况
            if (myCount == 1)
            {
                this.neuSpread1_Sheet1.SetValue(0, 7, "");
            }
            //只有首末两行，那么还要判断组号啊
            if (myCount == 2)
            {
                if (this.neuSpread1_Sheet1.Cells[0, 7].Tag.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, 7].Tag.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                    this.neuSpread1_Sheet1.SetValue(1, 7, "");
                }
                //防止一个药bid组合在一起
                if (this.neuSpread1_Sheet1.Cells[0, 13].Text.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, 13].Text.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                    this.neuSpread1_Sheet1.SetValue(1, 7, "");
                }
            }
            if (myCount > 2)
            {
                if (this.neuSpread1_Sheet1.GetValue(1, 7).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(1, 7).ToString() != "┛")
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                }
                if (this.neuSpread1_Sheet1.GetValue(myCount - 2, 7).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(myCount - 2, 7).ToString() != "┓")
                {
                    this.neuSpread1_Sheet1.SetValue(myCount - 1, 7, "");
                }
            }

        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {

        }

        /// <summary>
        /// 全选
        /// </summary>
        private void SelectedAll(bool isSelected)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //{FAC1693A-3EBA-44b3-A1E3-6D6750A98D80}
                //this.neuSpread1_Sheet1.SetValue(i, 0, isSelected, false);
                this.neuSpread1_Sheet1.Cells[i, 0].Value = isSelected;
            }
        }

        private void SelectedComb(bool isSelect)
        {
            
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            string combID = this.neuSpread1_Sheet1.Cells[row, 7].Tag.ToString();
            //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
            string injectTime = this.neuSpread1_Sheet1.Cells[row, 13].Text;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                if (this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() == combID && this.neuSpread1_Sheet1.Cells[i, 13].Text == injectTime) 
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Value = isSelect;
                }
            }

        }

        /// <summary>
        /// 修改皮试信息//{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
        /// </summary>
        private void ModifyHytest()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value);
                if (isSelected)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.neuSpread1_Sheet1.Cells[i, 11].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (orderinfo.HypoTest <= FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest) continue;
                    al.Add(orderinfo);
                }

            }

            if (al.Count == 0)
            {
                return;
            }
            FS.HISFC.Components.Nurse.Forms.frmHypoTest frmHypoTest = new FS.HISFC.Components.Nurse.Forms.frmHypoTest();
            frmHypoTest.AlOrderList = al;
            DialogResult d = frmHypoTest.ShowDialog();
            if (d == DialogResult.OK)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value);
                    if (!isSelected)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.neuSpread1_Sheet1.Cells[i, 11].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    //if (orderinfo.HypoTest == 1)
                    //{
                    //    strHypoTest = "否";
                    //}
                    //else if (orderinfo.HypoTest == 2)
                    //{
                    //    strHypoTest = "是";
                    //}
                    //else if (orderinfo.HypoTest == 3)
                    //{
                    //    strHypoTest = "阳性";
                    //}
                    //else if (orderinfo.HypoTest == 4)
                    //{
                    //    strHypoTest = "阴性";
                    //}
                    this.neuSpread1_Sheet1.Cells[i, 11].Text = this.GetHyTestInfo(orderinfo.HypoTest.ToString());

                }
            }


        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ucRegister()
        {
            InitializeComponent();
        }

        private void ucDept_Load(object sender, EventArgs e)
        {
            this.Init();
            this.SetFP();
            //{24A47206-F111-4817-A7B4-353C21FC7724} 初始化帮助类
            this.InitHelper();
            //{EB016FFE-0980-479c-879E-225462ECA6D0} 瓶签补打
            this.ucCureReprint1.Init();
            this.Clear();
        }

        /// <summary>
        /// 初始化帮助类
        /// {24A47206-F111-4817-A7B4-353C21FC7724}
        /// </summary>
        private void InitHelper()
        {
            //频次
            ArrayList alFrequency = this.conMgr.QuereyFrequencyList();
            foreach (FS.HISFC.Models.Order.Frequency frequency in alFrequency)
            {
                if (!this.dicFrequency.ContainsKey(frequency.ID))
                {
                    this.dicFrequency.Add(frequency.ID, frequency);
                }
            }
            ArrayList alUndrugInject = this.conMgr.QueryConstantList("UndrugInject");
            foreach (FS.FrameWork.Models.NeuObject neuObject in alUndrugInject)
            {
                if (!this.dicUndrugInject.ContainsKey(neuObject.ID))
                {
                    this.dicUndrugInject.Add(neuObject.ID,neuObject);
                }
            }
        }

        FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
        FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 查询的挂号的日期间隔
        /// </summary>
        private int queryRegDays = 2;

        /// <summary>
        /// 查询的挂号的日期间隔
        /// </summary>
        [Category("查询设置"), Description("查询挂号信息的日期间隔（从界面上的开始时间往前查询几天）")]
        public int QueryRegDays
        {
            get
            {
                return queryRegDays;
            }
            set
            {
                queryRegDays = value;
            }
        }

        /// <summary>
        /// 开始时间的日期间隔(距今天的间隔日期)
        /// </summary>
        private int beginDateIntervalDays = 0;

        /// <summary>
        /// 开始时间的日期间隔(距今天的间隔日期)
        /// </summary>
        [Category("查询设置"), Description("开始时间的日期间隔(距今天的间隔日期)")]
        public int BeginDateIntervalDays
        {
            get
            {
                return beginDateIntervalDays;
            }
            set
            {
                beginDateIntervalDays = value;
            }
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtRecipe.Text = string.Empty;
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("请输入病历号!", "提示");
                    this.txtCardNo.Focus();
                    this.Clear();
                    return;
                }
                string strCardNO = this.txtCardNo.Text.Trim();//.PadLeft(10, '0');
                int iTemp = feeManage.ValidMarkNO(strCardNO, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("无效卡号，请联系管理员！");
                    this.txtCardNo.Focus();
                    this.Clear();
                    return ;
                }
                this.Clear();

                string cardNo = objCard.Patient.PID.CardNO;
                ArrayList alRegs = this.regMgr.Query(cardNo, this.dtpStart.Value.AddDays(0 - queryRegDays));
                if (alRegs == null || alRegs.Count == 0)
                {
                    MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                    this.txtCardNo.Focus();
                    this.Clear();
                    return;
                }
                reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
                if (reg == null || reg.ID == "")
                {
                    MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                    this.txtCardNo.Focus();
                    this.Clear();
                    return;
                }

                this.txtCardNo.Text = cardNo;
                this.SetPatient(reg);

                //分解注射项目
                if (al != null)
                {                   
                    this.Query();
                }
                this.txtCardNo.Focus();
                this.txtRecipe.SelectAll();
            }
        }

        private void txtRecipe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCardNo.Text = string.Empty;
                if (this.txtRecipe.Text.Trim() == "")
                {
                    this.txtRecipe.Focus();
                    return;
                }
                this.Query();
                if (this.isUserOrderNumber)
                {
                    this.SetInject();
                }
                this.txtRecipe.Focus();
                this.txtRecipe.SelectAll();
            }
        }

        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            int altKey = Keys.Alt.GetHashCode();

            if (keyData == Keys.F1)
            {
                this.SelectAll(true);
                return true;
            }
            if (keyData == Keys.F2)
            {
                this.SelectAll(false);
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.S.GetHashCode())
            {
                if (this.Save() == 0)
                {
                    this.Print();
                }
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.Q.GetHashCode())
            {
                this.Query();
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.P.GetHashCode())
            {
                //
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            bool isSelect = Convert.ToBoolean(this.neuSpread1_Sheet1.Cells[row, 0].Value);
            this.SelectedComb(isSelect);
        }

        /// <summary>
        /// 排序
        /// {24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
        /// </summary>
        public class FeeItemListSort : IComparer<FS.HISFC.Models.Fee.Outpatient.FeeItemList>
        {
            public int Compare(FS.HISFC.Models.Fee.Outpatient.FeeItemList x, FS.HISFC.Models.Fee.Outpatient.FeeItemList y)
            {
                //先按照处方排序
                if (x.RecipeNO != y.RecipeNO)
                {
                    return -y.RecipeNO.CompareTo(x.RecipeNO);
                }
                //按注射时间排序
                if (x.User03 != y.User03)
                {
                    string a = x.User03;
                    string b = y.User03;
                    if (a.Length < "12:00".Length)
                    {
                        a = a.PadLeft("12:00".Length, '0');
                    }
                    if (b.Length < "12:00".Length)
                    {
                        b = b.PadLeft("12:00".Length, '0');
                    }
                    return -b.CompareTo(a);
                }

                //按组合号排序
                if (x.Order.Combo.ID != y.Order.Combo.ID)
                {
                    return -y.Order.Combo.ID.CompareTo(x.Order.Combo.ID);
                }
                //处方内序号
                if (x.SequenceNO != y.SequenceNO)
                {
                    return -y.SequenceNO.CompareTo(x.SequenceNO);
                }
                //药品编码
                if (x.Item.ID != y.Item.ID)
                {
                    return -y.Item.ID.CompareTo(x.Item.ID);
                }
                return 0;
            }
        }

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
    }
}
