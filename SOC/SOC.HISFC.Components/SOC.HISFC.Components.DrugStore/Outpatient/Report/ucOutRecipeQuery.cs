using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Management;

namespace Neusoft.SOC.HISFC.Components.DrugStore.Outpatient.Common
{
    /// <summary>
    /// [功能描述: 门诊处方查询]<br></br>
    /// [创 建 者: cao-lin]<br></br>
    /// [创建时间: 2012-08]<br></br>
    /// <修改记录 
    ///		 待实现 门诊标签的补打 由此处完成
    ///  />
    /// </summary>
    public partial class ucOutRecipeQuery : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucOutRecipeQuery()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 药品管理类
        /// </summary>
        SOC.HISFC.BizLogic.Pharmacy.Item itemManager = new Neusoft.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 药品管理类
        /// </summary>
        SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new SOC.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// 操作科室
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject operDept = null;

        /// <summary>
        /// 药品数组
        /// </summary>
        private ArrayList drugCollectioon = null;

        /// <summary>
        /// 人员类别
        /// </summary>
        private ArrayList personList = null;

        /// <summary>
        /// 是否全部传输
        /// </summary>
        private bool isSendAllData = false;

        /// <summary>
        /// 打印控件接口类
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory factory = null;

        #endregion

        #region 帮助类变量

        /// <summary>
        /// 人员帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper personHelper = null;

        /// <summary>
        /// 配药终端帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper drugTerminalHelper = null;

        /// <summary>
        /// 发药终端帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper sendTerminalHelper = null;



        #region 工具栏信息

        /// <summary>
        /// 定义工具栏服务
        /// </summary>
        protected Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            this.SetFpDetail();
            //增加工具栏
            this.toolBarService.AddToolButton("合并", "合并处方显示", 0, true, false, null);
            return this.toolBarService;
        }


        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();

            return base.Export(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region 数据初始化

        /// <summary>
        /// 数据初始化
        /// </summary>
        protected void DataInit()
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在加载基础查询数据,请稍候...."));
            Application.DoEvents();

            this.dtEnd.Value = this.itemManager.GetDateTimeFromSysDateTime();
            this.dtBegin.Value = this.dtEnd.Value.AddDays(-1);

            #region 加载查询类别

            ArrayList al = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject info1 = new Neusoft.FrameWork.Models.NeuObject();
            info1.ID = "A";
            info1.Name = "全部";
            al.Add(info1);
            Neusoft.FrameWork.Models.NeuObject info2 = new Neusoft.FrameWork.Models.NeuObject();
            info2.ID = "0";
            info2.Name = "病历卡号";
            al.Add(info2);
            Neusoft.FrameWork.Models.NeuObject info3 = new Neusoft.FrameWork.Models.NeuObject();
            info3.ID = "1";
            info3.Name = "发票号";
            al.Add(info3);
            Neusoft.FrameWork.Models.NeuObject info4 = new Neusoft.FrameWork.Models.NeuObject();
            info4.ID = "2";
            info4.Name = "姓名";
            al.Add(info4);
            Neusoft.FrameWork.Models.NeuObject info5 = new Neusoft.FrameWork.Models.NeuObject();
            info5.ID = "3";
            info5.Name = "处方号";
            al.Add(info5);
            Neusoft.FrameWork.Models.NeuObject info6 = new Neusoft.FrameWork.Models.NeuObject();
            info6.ID = "D";
            info6.Name = "药品";
            al.Add(info6);
            Neusoft.FrameWork.Models.NeuObject info7 = new Neusoft.FrameWork.Models.NeuObject();
            info7.ID = "4";
            info7.Name = "医生姓名";
            al.Add(info7);
            this.cmbQueryType.DataSource = al;
            this.cmbQueryType.DisplayMember = "Name";
            this.cmbQueryType.ValueMember = "ID";

            #endregion

            #region 加载人员

            Neusoft.HISFC.BizLogic.Manager.Person personManager = new Neusoft.HISFC.BizLogic.Manager.Person();
            ArrayList personAl = personManager.GetEmployeeAll();
            if (personAl == null)
            {
                MessageBox.Show("获取人员列表失败" + personManager.Err,"提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            this.personList = personAl;
            if (this.personHelper == null)
            {
                this.personHelper = new Neusoft.FrameWork.Public.ObjectHelper(personAl);
            }
            #endregion

            #region 加载药品
            List<Neusoft.HISFC.Models.Pharmacy.Item> itemList = this.itemManager.QueryItemList(true);
            if (itemList == null)
            {
                MessageBox.Show("获取药品列表失败" + this.itemManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (Neusoft.HISFC.Models.Pharmacy.Item item in itemList)
            {
                item.Memo = item.Specs;
            }

            this.drugCollectioon = new ArrayList(itemList.ToArray());

            #endregion

            #region 加载门诊终端列表
            ArrayList alDrugTerminal = this.drugStoreManager.QueryDrugTerminalByDeptCode(this.operDept.ID, "1");
            if (alDrugTerminal != null)
            {
                this.drugTerminalHelper = new Neusoft.FrameWork.Public.ObjectHelper(alDrugTerminal);
            }
            ArrayList alSendTerminal = this.drugStoreManager.QueryDrugTerminalByDeptCode(this.operDept.ID, "0");
            if (alSendTerminal != null)
            {
                this.sendTerminalHelper = new Neusoft.FrameWork.Public.ObjectHelper(alSendTerminal);
            }
            #endregion

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #endregion

        private void SetFpDetail()
        {
            if (this.cmbQueryType.SelectedValue != null && this.cmbQueryType.SelectedValue.ToString() == "D")
            {
                FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                numCellType.DecimalPlaces = 4;

                this.neuSpread2_Sheet1.Columns[0].Visible = false;			//药品编码
                this.neuSpread2_Sheet1.Columns[1].Width = 184F;			    //名称[规格]
                this.neuSpread2_Sheet1.Columns[2].Width = 90F;				//总量 单位

                this.neuSpread2_Sheet1.Columns[3].Visible = false;
                this.neuSpread2_Sheet1.Columns[4].Visible = false;
                this.neuSpread2_Sheet1.Columns[5].Visible = false;
            }
            else
            {
                this.neuSpread2_Sheet1.Columns[0].Visible = true;
                this.neuSpread2_Sheet1.Columns[0].Width = 40F;		//就诊号
                this.neuSpread2_Sheet1.Columns[1].Width = 40F;		//姓名
                this.neuSpread2_Sheet1.Columns[2].Width = 35F;		//性别
                this.neuSpread2_Sheet1.Columns[3].Width = 0F;		//年龄
                this.neuSpread2_Sheet1.Columns[3].Visible = true;
                this.neuSpread2_Sheet1.Columns[4].Width = 50F;		//挂号科室
                this.neuSpread2_Sheet1.Columns[4].Visible = true;
                this.neuSpread2_Sheet1.Columns[5].Width = 60F;		//挂号医生
                this.neuSpread2_Sheet1.Columns[5].Visible = true;
                this.neuSpread2_Sheet1.Columns[6].Width = 60F;		//合同单位
                this.neuSpread2_Sheet1.Columns[7].Width = 60F;		//病历号
                this.neuSpread2_Sheet1.Columns[8].Width = 90F;		//看诊日期
                this.neuSpread2_Sheet1.Columns[9].Width = 900F;
            }
        }

        /// <summary>
        /// 设置FarPoint
        /// </summary>
        private void SetFP()
        {
            if (this.cmbQueryType.SelectedValue != null && this.cmbQueryType.SelectedValue.ToString() == "D")
            {
                FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                numCellType.DecimalPlaces = 4;

                this.neuSpread1_Sheet1.Columns[0].Visible = false;			//药品编码
                this.neuSpread1_Sheet1.Columns[1].Width = 184F;			    //名称[规格]
                this.neuSpread1_Sheet1.Columns[2].Width = 90F;				//总量 单位

                this.neuSpread1_Sheet1.Columns[3].Visible = false;
                this.neuSpread1_Sheet1.Columns[4].Visible = false;
                this.neuSpread1_Sheet1.Columns[5].Visible = false;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[0].Visible = true;
                this.neuSpread1_Sheet1.Columns[0].Width = 60F;		//就诊号
                this.neuSpread1_Sheet1.Columns[1].Width = 60F;		//姓名
                this.neuSpread1_Sheet1.Columns[2].Width = 35F;		//性别
                this.neuSpread1_Sheet1.Columns[3].Width = 35F;		//年龄
                this.neuSpread1_Sheet1.Columns[3].Visible = true;
                this.neuSpread1_Sheet1.Columns[4].Width = 70F;		//挂号科室
                this.neuSpread1_Sheet1.Columns[4].Visible = true;
                this.neuSpread1_Sheet1.Columns[5].Width = 60F;		//挂号医生
                this.neuSpread1_Sheet1.Columns[5].Visible = true;
                this.neuSpread1_Sheet1.Columns[6].Width = 70F;		//合同单位
                this.neuSpread1_Sheet1.Columns[7].Width = 70F;		//病历号
                this.neuSpread1_Sheet1.Columns[8].Width = 120F;		//看诊日期
                this.neuSpread1_Sheet1.Columns[9].Width = 450F;
            }
        }

        public override int Print(object sender, object neuObject)
        {
            return base.Print(sender, neuObject);
        }

        /// <summary>
        /// 执行打印
        /// </summary>
        /// <param name="al">打印数据</param>
        /// <returns>成功返回1 失败返回-1</returns>
        internal int Print(Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, ArrayList al, string sendWindow)
        {
            return 1;
        }

        protected virtual bool IsValid()
        {
            DateTime dt1 = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            DateTime dt2 = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);
            if (dt1 >= dt2)
            {
                MessageBox.Show(Language.Msg("查询 开始时间应大于终止时间"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        protected void QueryData()
        {
            if (!this.IsValid())
            {
                return;
            }

            this.ClearDetail();

            try
            {
                this.neuSpread1_Sheet1.DataSource = null;
                this.neuSpread1_Sheet1.Rows.Count = 0;
                this.neuSpread2_Sheet1.DataSource = null;
                this.neuSpread2_Sheet1.DataSource = null;

                DataSet dsData = new DataSet();
                DataSet dsDataDetail = new DataSet();

                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询 请稍候...");
                Application.DoEvents();

                if (this.cmbQueryType.SelectedValue.ToString() == "D")
                {
                    if (this.txtQueryData.Text == "")
                        this.txtQueryData.Tag = "";
                    //if (this.GetDrugDataSet(ref dsData, this.txtQueryData.Tag) == 1)
                    //{
                    //    this.neuSpread1_Sheet1.DataSource = dsData;

                    //    this.SetFP();
                    //}
                }

                else
                {
                    if (this.GetDataSet(ref dsData) == 1)
                    {
                        this.neuSpread1_Sheet1.DataSource = dsData;

                        this.SetFP();
                    }
               
                }

                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取汇总Sql索引
        /// </summary>
        /// <param name="isHead">是否头信息查询</param>
        /// <param name="strIndex">Sql语句索引</param>
        private void GetSqlIndex(bool isHead, ref string strIndex)
        {
            if (this.ckBlurry.Checked)
            {
                strIndex = "Pharmacy.DrugStore.RecipeQuery.Head.4";
            }
            else
            {
                strIndex = "Pharmacy.DrugStore.RecipeQuery.Head.3";
            }
        }

        private void GetSqlIndexDetail(ref string strIndex)
        {
                strIndex = "Pharmacy.DrugStore.RecipeQuery.Detail1";
        }

        /// <summary>
        /// 获取待查询数据
        /// </summary>
        /// <param name="queryData"></param>
        private void GetQueryData(ref string queryData)
        {
            switch (this.cmbQueryType.SelectedValue.ToString())
            {
                case "A":		//全部
                    queryData = "A";
                    break;
                case "0":		//病历卡号
                    queryData = this.txtQueryData.Text.PadLeft(10, '0');
                    break;
                case "1":		//发票号
                    queryData = this.txtQueryData.Text.PadLeft(12, '0');
                    break;
                case "2":		//姓名
                case "3":		//处方号
                    queryData = this.txtQueryData.Text;
                    break;
                case "4":
                    queryData = this.txtQueryData.Tag.ToString();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryData"></param>
        private void GetQueryDataDetail(ref string queryData)
        {
            switch (this.cmbQueryType.SelectedValue.ToString())
            {
                case "A":		//全部
                    queryData = "A";
                    break;
                case "0":		//病历卡号
                    queryData = this.txtQueryData.Text.PadLeft(10, '0');
                    break;
                case "1":		//发票号
                    queryData = this.txtQueryData.Text.PadLeft(12, '0');
                    break;
                case "2":		//姓名
                case "3":		//处方号
                    queryData = this.txtQueryData.Text;
                    break;
                case "4":
                    queryData = this.txtQueryData.Tag.ToString();
                    break;
            }
        }

        /// <summary>
        /// 执行Sql语句，获取处方明细信息
        /// </summary>
        /// <param name="dsDataDetail"></param>
        /// <returns></returns>
        private int GetDataSetDetail(string clinicCode,ref DataSet dsDataDetail)
        {
            dsDataDetail = new DataSet();
            DataTable dtDetail = new DataTable("dtDetail");
            DataSet dsDetail = new DataSet();
            DateTime dt1 = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            DateTime dt2 = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);

            string strIndex = "";
            string strQueryData = "";

            this.GetSqlIndexDetail(ref strIndex);
            this.GetQueryDataDetail(ref strQueryData);


            this.drugStoreManager.ExecQuery(strIndex, ref dsDataDetail, clinicCode);
            return 1;
        }

        /// <summary>
        /// 执行Sql语句 获取就诊信息
        /// </summary>
        /// <param name="dsData">查询后的DataSet</param>
        /// <returns></returns>
        private int GetDataSet(ref DataSet dsData)
        {
            dsData = new DataSet();
            DataTable dtHead = new DataTable("Head");
            DataSet dsHead = new DataSet();

            DateTime dt1 = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            DateTime dt2 = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);

            string strIndex = "";
            string strQueryData = "";

            this.GetSqlIndex(true, ref strIndex);
            this.GetQueryData(ref strQueryData);
            if (this.ckBlurry.Checked)
            {
                strQueryData = "%" + strQueryData + "%";
            }
            //获取头信息
            this.drugStoreManager.ExecQuery(strIndex, ref dsHead, dt1.ToString(), dt2.ToString(), strQueryData, this.cmbQueryType.SelectedValue.ToString(), this.operDept.ID);
            if (dsHead == null)
            {
                MessageBox.Show("数据加载错误" + this.drugStoreManager.Err);
                return -1;
            }
            if (dsHead != null && dsHead.Tables.Count > 0 && dsHead.Tables[0].Rows.Count > 0)
            {
                dtHead = dsHead.Tables[0];
                dtHead.TableName = "Head";
                dsHead.Tables.Remove(dsHead.Tables[0]);
                string str = "";
                for (int i = 0; i < dtHead.Rows.Count; i++)
                {
                    DataSet dsDetail = new DataSet();

                    #region 设置显示 由编码转换为名称
                    dtHead.Rows[i]["年龄"] = this.drugStoreManager.GetAge(Neusoft.FrameWork.Function.NConvert.ToDateTime(dtHead.Rows[i]["年龄"].ToString()));

                    if (dtHead.Rows[i]["性别"] != null)
                    {
                        switch (dtHead.Rows[i]["性别"].ToString())
                        {
                            case "M":
                                dtHead.Rows[i]["性别"] = "男";
                                break;
                            case "F":
                                dtHead.Rows[i]["性别"] = "女";
                                break;
                            case "U":
                                dtHead.Rows[i]["性别"] = "未知";
                                break;

                        }
                    }
                    #endregion
                }

                dsData.Tables.Add(dtHead);

            }
            else
            {
                return 0;
            }

            return 1;
        }

     

        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("导出成功"));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.operDept = ((Neusoft.HISFC.Models.Base.Employee)this.drugStoreManager.Operator).Dept;

                this.DataInit();

                //{9FA792B0-A60F-48d8-A3F5-1C52450C44A5} 获取打印类型 取消原代码 需在报表接口维护内对实现进行配置
                object factoryInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory)) as Neusoft.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory;
                if (factoryInstance != null)
                {
                    this.factory = factoryInstance as Neusoft.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory;
                }

                this.cmbQueryType.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbQueryType.SelectedIndexChanged += new EventHandler(cmbQueryType_SelectedIndexChanged);
            }
            catch
            { }

            base.OnLoad(e);
        }

        void cmbQueryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbQueryType.Text == "发票号" || this.cmbQueryType.Text == "病历卡号")
            {
                this.ckBlurry.Enabled = false;
            }
            else
            {
                this.ckBlurry.Enabled = true;
            }
        }

      

        private void txtQueryData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.QueryData();
            }
            if (e.KeyCode == Keys.Space && this.cmbQueryType.SelectedValue != null && this.cmbQueryType.SelectedValue.ToString() == "D" && this.drugCollectioon != null)
            {
                Neusoft.FrameWork.Models.NeuObject drugInfo = new Neusoft.FrameWork.Models.NeuObject();
                if (Neusoft.FrameWork.WinForms.Classes.Function.ChooseItem(this.drugCollectioon, new string[] { "编码", "商品名称", "规格" }, new bool[] { false, true, true, false, false, false, false, false, false }, new int[] { 100, 160, 80 }, ref drugInfo) == 0)
                {
                    return;
                }
                else
                {
                    this.txtQueryData.Text = drugInfo.Name;
                    this.txtQueryData.Tag = drugInfo.ID;

                    this.QueryData();
                }
            }
            //南庄增加
            else if (e.KeyCode == Keys.Space && this.cmbQueryType.SelectedValue != null && this.cmbQueryType.SelectedValue.ToString() == "4" && this.personHelper != null)
            {
                Neusoft.FrameWork.Models.NeuObject personInfo = new Neusoft.FrameWork.Models.NeuObject();
                if (Neusoft.FrameWork.WinForms.Classes.Function.ChooseItem(this.personList, new string[] { "编码", "人员姓名", "" }, new bool[] { false, true, true, false, false, false, false, false, false }, new int[] { 100, 160, 80 }, ref personInfo) == 0)
                {
                    return;
                }
                else
                {
                    this.txtQueryData.Text = personInfo.Name;
                    this.txtQueryData.Tag = personInfo.ID;

                    this.QueryData();
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Space && this.cmbQueryType.Focused)
            {
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(Neusoft.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory);

                return printType;
            }
        }

        #endregion

        private void ClearDetail()
        {
            this.neuSpread2_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 画组合号方法
        /// </summary>
        /// <param name="dsDataDetail"></param>
        private void DrawComboNO(DataSet dsDataDetail)
        {
            DataTable dt = new DataTable();
            if (dsDataDetail != null && dsDataDetail.Tables.Count > 0)
            {
                dt = dsDataDetail.Tables[0];
            }
            string comBoTemp = "";
            int index = 1;
            for (int i = 0; i < dt.Rows.Count;i++)
            {
                if (dt.Rows[i]["组合号"].ToString() == comBoTemp)
                {
                    dt.Rows[i]["组合号"] = "";
                }
                else
                {
                    dt.Rows[i]["组合号"] = "组" + index;
                    index++;
                }
            }
        }
        #region 事件
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string clinicCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text.ToString();
            DataSet dsDataDetail = new DataSet();
            this.neuSpread2_Sheet1.RowCount = 0;
            int param = this.GetDataSetDetail(clinicCode,ref  dsDataDetail);
            if(param  == 1)
            {
                DrawComboNO(dsDataDetail);
                this.neuSpread2_Sheet1.DataSource = dsDataDetail;

            }
        }
        #endregion
    }
}
        #endregion