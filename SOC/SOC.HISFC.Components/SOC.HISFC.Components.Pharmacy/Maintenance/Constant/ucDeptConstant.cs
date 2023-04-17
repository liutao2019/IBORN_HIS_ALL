using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    /// <summary>
    /// [功能描述: 科室库存常数维护]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007-03]<br></br>
    /// </summary>
    public partial class ucDeptConstant : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucDeptConstant()
        {
            InitializeComponent();
        }

        #region 说明

        /*
         *  1、新增科室(不一定是药房或药库)需要维护部门常数时 需先在科室结构内对03类别进行添加
         * 
         *  2、Sql
         *      SELECT  
				PHA_COM_DEPT.DEPT_CODE,                              --部门编码
				COM_DEPTSTAT.DEPT_NAME,				     --部门名称
				PHA_COM_DEPT.STORE_MAX_DAYS,                         --库房最高库存量(天)
				PHA_COM_DEPT.STORE_MIN_DAYS,                         --库房最低库存量(天)
				PHA_COM_DEPT.REFERENCE_DAYS,                         --参考天数
				PHA_COM_DEPT.BATCH_FLAG,                             --是否按批号管理药品
				PHA_COM_DEPT.STORE_FLAG,                             --是否管理药品库存
				PHA_COM_DEPT.UNIT_FLAG,                              --库存管理时默认的单位，1包装单位，0最小单位
				PHA_COM_DEPT.OPER_CODE,                              --操作员代码
				PHA_COM_DEPT.OPER_DATE                               --操作时间
			FROM 	PHA_COM_DEPT,
				COM_DEPTSTAT  
			WHERE 	PHA_COM_DEPT.PARENT_CODE  = COM_DEPTSTAT.PARENT_CODE 
			AND  	PHA_COM_DEPT.CURRENT_CODE = COM_DEPTSTAT.CURRENT_CODE 
			AND   	COM_DEPTSTAT.STAT_CODE = '03' 
			AND   	COM_DEPTSTAT.DEPT_CODE = PHA_COM_DEPT.DEPT_CODE 
			AND   	PHA_COM_DEPT.PARENT_CODE  =  fun_get_parentcode  
			AND  	PHA_COM_DEPT.CURRENT_CODE =  fun_get_currentcode  
         * 
         */

        #endregion

        #region 域变量

        /// <summary>
        /// 业务管理类
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// 是否可以设置库存管理单位
        /// </summary>
        private bool isManagerUnitFlag = false;

        /// <summary>
        /// 是否可以设置批号管理
        /// </summary>
        private bool isManagerBatch = true;

        /// <summary>
        /// 是否可以设置库存管理
        /// </summary>
        private bool isManagerStore = true;

        /// <summary>
        /// 是否可以设置库存警戒线参数
        /// </summary>
        private bool isManagerParam = true;

        /// <summary>
        /// 是否可以设置药柜管理标志
        /// </summary>
        private bool isManagerArk = false;

        /// <summary>
        /// 已维护的科室库存列表
        /// </summary>
        private System.Collections.Hashtable hsPhaDept = new Hashtable();

        private System.Collections.Hashtable hsDeptControlDrugType = new Hashtable();

        /// <summary>
        /// 是否允许设置入库单号
        /// </summary>
        private bool isManagerInListNO = false;

        /// <summary>
        /// 是否允许设置出库单号
        /// </summary>
        private bool isManagerOutListNO = false;
        #endregion

        #region 属性

        /// <summary>
        /// 是否可以设置库存管理单位
        /// </summary>
        [Description("是否可以设置库存管理单位"),Category("设置"),DefaultValue(true)]
        public bool IsManagerUnitFlag
        {
            get
            {
                return isManagerUnitFlag;
            }
            set
            {
                isManagerUnitFlag = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUnitFlag].Visible = value;                
            }
        }

        /// <summary>
        /// 是否可以设置批号管理
        /// </summary>
        [Description("是否可以设置批号管理"), Category("设置"), DefaultValue(true)]
        public bool IsManagerBatch
        {
            get
            {
                return isManagerBatch;
            }
            set
            {
                isManagerBatch = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsBatch].Visible = value;
            }
        }

        /// <summary>
        /// 是否可以设置库存管理
        /// </summary>
        [Description("是否可以设置库存管理"), Category("设置"), DefaultValue(true)]
        public bool IsManagerStore
        {
            get
            {
                return isManagerStore;
            }
            set
            {
                isManagerStore = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsStore].Visible = value;
            }
        }

        /// <summary>
        /// 是否可以设置库存警戒线参数
        /// </summary>
        [Description("是否可以设置库存警戒线参数"), Category("设置"), DefaultValue(true)]
        public bool IsManagerParam
        {
            get
            {
                return isManagerParam;
            }
            set
            {
                isManagerParam = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMaxDays].Visible = value;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMinDays].Visible = value;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColReferenceDays].Visible = value;
            }
        }

        /// <summary>
        /// 是否可以设置药柜管理
        /// </summary>
        [Description("是否可以设置药柜管理"), Category("设置"), DefaultValue(true)]
        public bool IsManagerArk
        {
            get
            {
                return this.isManagerArk;
            }
            set
            {
                this.isManagerArk = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsArk].Visible = value;
            }
        }

        /// <summary>
        /// 是否允许设置入库单号
        /// </summary>
        [Description("是否允许设置入库单号"), Category("设置"), DefaultValue(true)]
        public bool IsManagerInListNO
        {
            get
            {
                return this.isManagerInListNO;
            }
            set
            {
                this.isManagerInListNO = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColInListNO].Visible = value;
            }
        }

        /// <summary>
        /// 是否允许设置出库单号
        /// </summary>
        [Description("是否允许设置出库单号"), Category("设置"), DefaultValue(true)]
        public bool IsManagerOutListNO
        {
            get
            {
                return this.isManagerOutListNO;
            }
            set
            {
                this.isManagerOutListNO = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOutListNO].Visible = value;
            }
        }

        private string privePowerString = "0300";

        [Description("使用窗口需要的权限,如：0300"), Category("设置"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }

        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {          
            return toolBarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.SaveDeptCons();
        }

        #endregion

        /// <summary>
        /// 显示科室列表
        /// </summary>
        private int ShowDeptList()
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            //取科室常数信息
            //这加载维护在药库权限管理里的科室，药库管理权限一级分类03
            ArrayList alPharmacyStatDept = this.phaConsManager.QueryPharmacyStatList();
            if (alPharmacyStatDept == null)
            {
                MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                return -1;
            }
            if (alPharmacyStatDept.Count == 0)
            {
                Function.ShowMessage("系统中没有维护药库管理权限，请与系统管理员联系！", MessageBoxIcon.Information);
                return 0;
            }
            ArrayList alMaintenanced = this.phaConsManager.QueryDeptConstantList();
            if (alMaintenanced == null)
            {
                MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                return -1;
            }
            if (alMaintenanced.Count == 0)
            {
                Function.ShowMessage("系统中没有维护药库管理权限，请与系统管理员联系！", MessageBoxIcon.Information);
                return 0;
            }
            try
            {
                this.hsPhaDept.Clear();

                FS.HISFC.Models.Pharmacy.DeptConstant deptConstant = null;
                this.neuSpread1_Sheet1.RowCount = 0;

                int iCount = 0;

                for (int i = 0; i < alMaintenanced.Count; i++)
                {                  
                    deptConstant = alMaintenanced[i] as FS.HISFC.Models.Pharmacy.DeptConstant;

                    this.hsPhaDept.Add(deptConstant.ID,null);

                    if (deptConstant.ID.Substring(0, 1) == "S")
                    {
                        continue;
                    }
                   
                    this.neuSpread1_Sheet1.Rows.Add(iCount, 1);

                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptID].Value = deptConstant.ID;			//部门编码
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptName].Value = deptConstant.Name;			//部门名称
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsStore].Value = deptConstant.IsStore;		//是否管理库存
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsBatch].Value = deptConstant.IsBatch;		//是否按批号管理
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColUnitFlag].Value = deptConstant.UnitFlag == "1" ? "包装单位" : "最小单位";//库存管理默认单位:0最小单位,1包装单位
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColMaxDays].Value = deptConstant.StoreMaxDays;	//库存上限天数
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColMinDays].Value = deptConstant.StoreMinDays;	//库存下限天数
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColReferenceDays].Value = deptConstant.ReferenceDays;//参考天数
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsArk].Value = deptConstant.IsArk;            //药柜管理标志 是否药柜管理
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColInListNO].Value = deptConstant.InListNO;
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColOutListNO].Value = deptConstant.OutListNO;

                    iCount++;
                }

                for (int i = 0; i < alPharmacyStatDept.Count; i++)
                {
                    deptConstant = alPharmacyStatDept[i] as FS.HISFC.Models.Pharmacy.DeptConstant;
                    if (hsPhaDept.Contains(deptConstant.ID))
                    {
                        continue;
                    }

                    if (deptConstant.ID.Substring(0, 1) == "S")
                    {
                        continue;
                    }

                    this.hsPhaDept.Add(deptConstant.ID, null);

                    this.neuSpread1_Sheet1.Rows.Add(iCount, 1);

                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptID].Value = deptConstant.ID;			//部门编码
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptName].Value = deptConstant.Name;			//部门名称
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsStore].Value = deptConstant.IsStore;		//是否管理库存
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsBatch].Value = deptConstant.IsBatch;		//是否按批号管理
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColUnitFlag].Value = deptConstant.UnitFlag == "1" ? "包装单位" : "最小单位";//库存管理默认单位:0最小单位,1包装单位
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColMaxDays].Value = deptConstant.StoreMaxDays;	//库存上限天数
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColMinDays].Value = deptConstant.StoreMinDays;	//库存下限天数
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColReferenceDays].Value = deptConstant.ReferenceDays;//参考天数
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsArk].Value = deptConstant.IsArk;            //药柜管理标志 是否药柜管理
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColInListNO].Value = deptConstant.InListNO;
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColOutListNO].Value = deptConstant.OutListNO;

                    iCount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取科室结构信息
        /// </summary>
        /// <returns></returns>
        private int ShowDeptStat()
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alDeptStat = deptStatManager.LoadDepartmentStat("03");
            if (alDeptStat == null)
            {
                MessageBox.Show(Language.Msg("获取科室节点信息失败"));
                return -1;
            }

            foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in alDeptStat)
            {
                if (this.hsPhaDept.ContainsKey(deptStat.DeptCode))
                {
                    continue;
                }

                if (deptStat.DeptCode.Substring(0, 1) == "S")
                {
                    continue;
                }

                FS.HISFC.Models.Pharmacy.DeptConstant deptConstant = new FS.HISFC.Models.Pharmacy.DeptConstant();

                deptConstant.ID = deptStat.DeptCode;
                deptConstant.Name = deptStat.DeptName;

                int iCount = this.neuSpread1_Sheet1.Rows.Count;

                this.neuSpread1_Sheet1.Rows.Add(iCount, 1);

                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptID].Value = deptConstant.ID;			//部门编码
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptName].Value = deptConstant.Name;			//部门名称        

                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsStore].Value = false;
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsBatch].Value = false ;		//是否按批号管理
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColUnitFlag].Value = "最小单位";//库存管理默认单位:0最小单位,1包装单位
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsArk].Value = false;            //药柜管理标志 是否药柜管理

                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColInListNO].Value = "";            //药柜管理标志 是否药柜管理
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColOutListNO].Value = "";            //药柜管理标志 是否药柜管理

            }
            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        private int SaveDeptCons()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                MessageBox.Show(Language.Msg("没有可以保存的数据"));
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            phaConsManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Pharmacy.DeptConstant deptConstant = null;

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                deptConstant = new FS.HISFC.Models.Pharmacy.DeptConstant();

                deptConstant.ID = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDeptID].Text;			                                //部门编码
                deptConstant.Name = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDeptName].Text;			                            //部门名称
                deptConstant.IsStore = NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColIsStore].Value.ToString());		//是否管理库存
                deptConstant.IsBatch = NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColIsBatch].Value.ToString());		//是否按批号管理
                deptConstant.UnitFlag = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColUnitFlag].Text.ToString() == "包装单位" ? "1" : "0";//库存管理默认单位:0最小单位,1包装单位
                deptConstant.StoreMaxDays = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColMaxDays].Text);              //库存上限天数
                deptConstant.StoreMinDays = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColMinDays].Text);              //库存下限天数
                deptConstant.ReferenceDays = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColReferenceDays].Text);       //参考天数
                deptConstant.IsArk = NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColIsArk].Value.ToString());         //是否药柜管理
                //{849BBA57-0A27-4e6b-BC8C-C92A9B9B325F}
                //deptConstant.InListNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColInListNO].Value.ToString();
                //deptConstant.OutListNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColOutListNO].Value.ToString();
                try
                {
                    deptConstant.InListNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColInListNO].Value.ToString();
                }
                catch (Exception)
                {

                    deptConstant.InListNO = "";
                }

                try
                {
                    deptConstant.OutListNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColOutListNO].Value.ToString();
                }
                catch (Exception)
                {

                    deptConstant.OutListNO = "";
                }

                int parm = this.phaConsManager.UpdateDeptConstant(deptConstant);
                if (parm == 0)
                {                    
                        if (this.phaConsManager.InsertDeptConstant(deptConstant) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                            return -1;
                        }
                }
                else if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                    return -1;
                }
            }

            if (this.phaConsManager.DeleteControlDrugAttribute("D", this.sheetView1.Cells[0, 3].Text, "T") == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                return -1;
            }
            for (int i = 0; i < this.sheetView1.RowCount; i++)
            {
                FS.SOC.HISFC.Models.Pharmacy.Constant.ControlAttribute ca = new FS.SOC.HISFC.Models.Pharmacy.Constant.ControlAttribute();
                ca.ObjectCode = this.sheetView1.Cells[i, 3].Text;
                ca.ObjectType = "D";
                ca.AttributeCode = this.sheetView1.Cells[i, 1].Text;
                ca.AttributeType = "T";
                ca.ValidState = (FS.FrameWork.Function.NConvert.ToBoolean(this.sheetView1.Cells[i, 0].Value) ? "1" : "0");
                ca.OperID = this.phaConsManager.Operator.ID;
                ca.OperTime = this.phaConsManager.GetDateTimeFromSysDateTime();
                if (this.phaConsManager.InsetControlDrugAttribute(ca) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("保存成功"));

            return 1;
        }

        private int ShowControlDrugAttribute(string deptNO)
        {
            hsDeptControlDrugType.Clear();
            SOC.HISFC.BizProcess.Cache.Common.InitDrugType();
            this.sheetView1.RowCount = 0;
            this.sheetView1.RowCount = SOC.HISFC.BizProcess.Cache.Common.drugTypeHelper.ArrayObject.Count;
            int rowIndex = 0;

            ArrayList alAttribute = this.phaConsManager.QueryControlDrugAttribute("D", "T");
            if (alAttribute == null)
            {
                MessageBox.Show("获取控药属性发生错误：" + this.phaConsManager.Err);
                return -1;
            }

            foreach (FS.SOC.HISFC.Models.Pharmacy.Constant.ControlAttribute controlAttribute in alAttribute)
            {
                if (!hsDeptControlDrugType.Contains(controlAttribute.ObjectCode + controlAttribute.AttributeCode))
                {
                    FS.FrameWork.Models.NeuObject deptControlDrugType = new FS.FrameWork.Models.NeuObject();
                    deptControlDrugType.ID = controlAttribute.AttributeCode;
                    deptControlDrugType.Name = SOC.HISFC.BizProcess.Cache.Common.GetDrugTypeName(controlAttribute.AttributeCode);
                    deptControlDrugType.Memo = (controlAttribute.ValidState == "1" ? "1" : "0");
                    hsDeptControlDrugType.Add(controlAttribute.ObjectCode + controlAttribute.AttributeCode, deptControlDrugType);
                }

            }

            foreach (FS.FrameWork.Models.NeuObject drugType in SOC.HISFC.BizProcess.Cache.Common.drugTypeHelper.ArrayObject)
            {
                FS.FrameWork.Models.NeuObject deptControlDrugType = new FS.FrameWork.Models.NeuObject();
                if (hsDeptControlDrugType.Contains(deptNO + drugType.ID))
                {
                    deptControlDrugType = hsDeptControlDrugType[deptNO + drugType.ID] as FS.FrameWork.Models.NeuObject;
                }
                else
                {
                    deptControlDrugType.ID = drugType.ID;
                    deptControlDrugType.Name = drugType.Name;
                    deptControlDrugType.Memo = "0";
                    hsDeptControlDrugType.Add(deptNO + drugType.ID, deptControlDrugType);
                }

                this.sheetView1.Cells[rowIndex, 0].Value = FS.FrameWork.Function.NConvert.ToBoolean(deptControlDrugType.Memo);
                this.sheetView1.Cells[rowIndex, 1].Text = drugType.ID;
                this.sheetView1.Cells[rowIndex, 2].Text = drugType.Name;
                this.sheetView1.Cells[rowIndex, 3].Text = deptNO;
                this.sheetView1.Cells[rowIndex, 4].Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptNO);
                rowIndex++;
            }
            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {


                this.ShowDeptList();

                //this.ShowDeptStat();

                this.neuSpread1.CellClick+=new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellClick);
                this.sheetView1.RowCount = 0;
                this.sheetView1.Columns.Get(0).CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.sheetView1.Columns.Get(1).CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.sheetView1.Columns.Get(1).Width = 0;
                this.sheetView1.Columns.Get(2).CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.sheetView1.Columns.Get(3).CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.sheetView1.Columns.Get(3).Width = 0;
                this.sheetView1.Columns.Get(4).CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1_Sheet1.ActiveRowIndex = -1;
            }

            base.OnLoad(e);
        }

        void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string deptNO = this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColDeptID].Text;			                                //部门编码
            this.ShowControlDrugAttribute(deptNO);
        }


        /// <summary>
        /// 列设置
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// 科室名称
            /// </summary>
            ColDeptName,
            /// <summary>
            /// 是否管理库存
            /// </summary>
            ColIsStore,
            /// <summary>
            /// 是否管理批号
            /// </summary>
            ColIsBatch,
            /// <summary>
            /// 库存管理单位
            /// </summary>
            ColUnitFlag,
            /// <summary>
            /// 库存管理上限
            /// </summary>
            ColMaxDays,
            /// <summary>
            /// 库存管理下限
            /// </summary>
            ColMinDays,
            /// <summary>
            /// 参考天数
            /// </summary>
            ColReferenceDays,
            /// <summary>
            /// 是否药柜管理
            /// </summary>
            ColIsArk,
            ColInListNO,
            ColOutListNO,
            /// <summary>
            /// 科室编码
            /// </summary>
            ColDeptID
        }

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (!SOC.HISFC.Components.PharmacyCommon.Function.JugePrive(this.PrivePowerString))
            {
                Function.ShowMessage("您没有权限操作此窗口，如有疑问请与系统管理员联系！", MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }

        #endregion
    }
}
