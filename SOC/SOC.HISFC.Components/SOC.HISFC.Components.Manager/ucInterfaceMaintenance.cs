using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Manager
{
    public partial class ucInterfaceMaintenance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInterfaceMaintenance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据明细显示控件（接口）实例
        /// </summary>
        protected SOC.HISFC.BizProcess.CommonInterface.IDataDetail curIDataDetail = null;

        protected System.Data.DataTable dtDetail = null;

        protected string settingFileName = "";

        private FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

        System.Collections.Hashtable hsDelete = new System.Collections.Hashtable();

        #region 变量及属性

        private string interfaceDefineDll = "SOC.HISFC.BizProcess.MessagePatternInterface.dll";

        /// <summary>
        /// 接口定义dll
        /// </summary>
        [Description("接口定义dll"), Category("设置"), Browsable(true)]
        public string InterfaceDefineDll
        {
            get { return interfaceDefineDll; }
            set { interfaceDefineDll = value; }
        }

        private string interfaceImplementDll = "SOC.HISFC.BizProcess.MessagePattern.dll";

        /// <summary>
        /// 接口实现dll
        /// </summary>
        [Description("接口实现dll"), Category("设置"), Browsable(true)]
        public string InterfaceImplementDll
        {
            get { return interfaceImplementDll; }
            set { interfaceImplementDll = value; }
        }
        #endregion

        #region 初始化

        protected virtual int Init()
        {
            this.settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPInterfaceMaintenaceSetting.xml";

            int param = this.InitDataTable();

            if (param == -1)
            {
                return param;
            }

            param = this.InitDataDetail();

            if (param == -1)
            {
                return param;
            }

            this.curIDataDetail.Info = "(接口类、实现接口的类)                主键： 医院编号 + 调用接口的dll + 调用接口的类 + 索引";

            param = this.AddMaintenanceToDataTable();


            return param;
        }

        /// <summary>
        /// 初始化入库明细数据控件(接口)
        /// 必须在InitDataTable之后调用
        /// </summary>
        /// <returns></returns>
        protected virtual int InitDataDetail()
        {
            if (this.curIDataDetail == null)
            {
                this.curIDataDetail = new FS.SOC.HISFC.Components.Common.Base.ucDataDetail();

                int param = this.curIDataDetail.Init();

                if (param == -1)
                {
                    Function.ShowMessage("系统设置错误：数据录入控件初始化失败！请与系统管理员联系！", MessageBoxIcon.Error);
                    return -1;
                }

                this.Controls.Add(this.curIDataDetail as System.Windows.Forms.Control);
            }

            this.curIDataDetail.Filter = "接口类 like '%{0}%' or 实现接口的类 like '%{0}%' or 索引 like '%{0}%' or 实现接口的dll like '%{0}%' or 接口名称 like '%{0}%'";
            if (this.curIDataDetail.FpSpread != null && this.curIDataDetail.FpSpread.Sheets.Count > 0 && this.dtDetail != null)
            {
                this.curIDataDetail.FpSpread.Sheets[0].DataSource = this.dtDetail.DefaultView;
                this.curIDataDetail.SettingFileName = this.settingFileName;
                this.InitFarPoint();
            }
            return 1;
        }

        /// <summary>
        /// 初始化设置明细数据的FarPoint
        /// </summary>
        /// <returns></returns>
        protected virtual int InitFarPoint()
        {
            //配置文件在过滤后恢复FarPoint格式用
            if (!System.IO.File.Exists(this.settingFileName))
            {
                this.curIDataDetail.FpSpread.Sheets[0].RowHeader.Visible = true;

                FarPoint.Win.Spread.CellType.CheckBoxCellType c = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                FarPoint.Win.Spread.CellType.ComboBoxCellType cb = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

                this.curIDataDetail.FpSpread.SetColumnWith(0, "PK_HOS_CODE", 0);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "PK_CONTAINERDLLNAME", 0);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "PK_CONTAINERCONTROL", 0);
                this.curIDataDetail.FpSpread.SetColumnWith(0, "PK_PRINTERINDEX", 0);

                //this.curIDataDetail.FpSpread.SaveSchema(this.settingFileName);
            }
            else
            {
                this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);
            }

            this.curIDataDetail.FpSpread.EditModePermanent = true;
            this.curIDataDetail.FpSpread.EditMode = true;
            this.curIDataDetail.FpSpread.EditModeReplace = true;

            return 1;
        }

        /// <summary>
        /// 初始化明细数据的DataTable
        /// 决定FarPoint的列名称
        /// 修改列时注意明细数据显示的FarPoint初始化函数InitDetailDataFarPoint保持一致
        /// 修改列时注意设置过滤字符串的函数InitDataDetailUC保持一致
        /// 修改列时注意向明细数据显示的DataTable添加数据时的函数AddInputObjectToDataTable保持一致
        /// 请保证主键在最后一列
        /// </summary>
        /// <returns></returns>
        protected virtual int InitDataTable()
        {
            if (this.dtDetail == null)
            {
                this.dtDetail = new System.Data.DataTable();
            }

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBol = System.Type.GetType("System.Boolean");
            this.dtDetail.Columns.AddRange
                (
                new System.Data.DataColumn[]
                {
                    new DataColumn("医院编号",      dtStr),
                    new DataColumn("业务编码",      dtStr),
                    new DataColumn("接口名称",      dtStr),
                    new DataColumn("定义接口的dll", dtStr),
                    new DataColumn("接口类",        dtStr),
                    new DataColumn("调用接口的dll", dtStr),
                    new DataColumn("调用接口的类",  dtStr),
                    new DataColumn("实现接口的dll", dtStr),
                    new DataColumn("实现接口的类",  dtStr),
                    new DataColumn("强制实现",      dtBol),
                    new DataColumn("索引",        dtStr),
                    new DataColumn("备注",          dtStr),
                    new DataColumn("PK_HOS_CODE",          dtStr),
                    new DataColumn("PK_CONTAINERDLLNAME",          dtStr),
                    new DataColumn("PK_CONTAINERCONTROL",          dtStr),
                    new DataColumn("PK_PRINTERINDEX",          dtStr),
                }
                );

            DataColumn[] keys = new DataColumn[4];

            keys[0] = this.dtDetail.Columns["PK_HOS_CODE"];
            keys[1] = this.dtDetail.Columns["PK_CONTAINERDLLNAME"];
            keys[2] = this.dtDetail.Columns["PK_CONTAINERCONTROL"];
            keys[3] = this.dtDetail.Columns["PK_PRINTERINDEX"];

            this.dtDetail.PrimaryKey = keys;

            this.dtDetail.Columns["PK_HOS_CODE"].ReadOnly = true;
            this.dtDetail.Columns["PK_CONTAINERDLLNAME"].ReadOnly = true;
            this.dtDetail.Columns["PK_CONTAINERCONTROL"].ReadOnly = true;
            this.dtDetail.Columns["PK_PRINTERINDEX"].ReadOnly = true;


            return 1;
        }

        #endregion

        #region 保存
        private int SaveData()
        {
            this.curIDataDetail.SetFocusToFilter();

            DataTable modifyTable = this.dtDetail.GetChanges(DataRowState.Modified);
            DataTable addTable = this.dtDetail.GetChanges(DataRowState.Added);
            if (modifyTable == null && addTable == null)
            {
                this.dtDetail.AcceptChanges();
                Function.ShowMessage("数据没有改变！", MessageBoxIcon.Information);
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (addTable != null)
            {
                #region 新加的数据
                foreach (DataRow row in addTable.Rows)
                {
                    if (row["调用接口的dll"].ToString() == ""
                          || row["调用接口的类"].ToString() == ""
                           || row["医院编号"].ToString() == ""
                           || row["索引"].ToString() == "")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("保存失败：主键为空", MessageBoxIcon.Information);
                        return -1;
                    }

                    bool isNeedImpleted = FS.FrameWork.Function.NConvert.ToBoolean(row["强制实现"]);
                    string state = "0";
                    if (isNeedImpleted)
                    {
                        state = "1";
                    }

                    string SQL = @"insert into  com_maintenance_report_print(
                                           containerdllname,
                                           containercontrol,
                                           printerdllname,
                                           printercontrol,
                                           printerindex,
                                           memo,
                                           interface,
                                           containertype,
                                           name,
                                           state,
                                           hos_code)
                                    values
                                        (
                                           '{0}',
                                           '{1}',
                                           '{2}',
                                           '{3}',
                                           {4},
                                           '{5}',
                                           '{6}',
                                           '{7}',
                                           '{8}',
                                           '{9}',
                                           '{10}'
                                        )

                                    ";


                    SQL = string.Format(SQL,
                            row["调用接口的dll"].ToString(),
                            row["调用接口的类"].ToString(),
                            row["实现接口的dll"].ToString(),
                            row["实现接口的类"].ToString(),
                            row["索引"].ToString(),
                            row["备注"].ToString(),
                            row["接口类"].ToString(),
                            row["业务编码"].ToString(),
                            row["接口名称"].ToString(),
                            state,
                            row["医院编号"].ToString()
                        );

                    if (this.dbMgr.ExecNoQuery(SQL) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败：" + this.dbMgr.Err);
                        return -1;
                    }
                }
                #endregion
            }
            if (modifyTable != null)
            {
                #region 修改的数据

                foreach (DataRow row in modifyTable.Rows)
                {

                    bool isNeedImpleted = FS.FrameWork.Function.NConvert.ToBoolean(row["强制实现"]);
                    string state = "0";
                    if (isNeedImpleted)
                    {
                        state = "1";
                    }

                    string SQL = @"update  com_maintenance_report_print set
                                           containerdllname = '{0}',
                                           containercontrol = '{1}',
                                           printerdllname = '{2}',
                                           printercontrol = '{3}',
                                           printerindex = {4},
                                           memo = '{5}',
                                           interface = '{6}',
                                           containertype = '{7}',
                                           name = '{8}',
                                           state = '{9}',
                                           hos_code = '{10}'
                                  where    hos_code = '{11}'
                                  and      containerdllname = '{12}'
                                  and      containercontrol = '{13}'
                                  and      printerindex = {14}

                                    ";


                    SQL = string.Format(SQL,
                            row["调用接口的dll"].ToString(),
                            row["调用接口的类"].ToString(),
                            row["实现接口的dll"].ToString(),
                            row["实现接口的类"].ToString(),
                            row["索引"].ToString(),
                            row["备注"].ToString(),
                            row["接口类"].ToString(),
                            row["业务编码"].ToString(),
                            row["接口名称"].ToString(),
                            state,
                            row["医院编号"].ToString(),
                            row["PK_HOS_CODE"].ToString(),
                            row["PK_CONTAINERDLLNAME"].ToString(),
                            row["PK_CONTAINERCONTROL"].ToString(),
                            row["PK_PRINTERINDEX"].ToString()
                        );

                    if (this.dbMgr.ExecNoQuery(SQL) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败：" + this.dbMgr.Err);
                        return -1;
                    }
                }
                #endregion

            }

            FS.FrameWork.Management.PublicTrans.Commit();
            Function.ShowMessage("保存成功！", MessageBoxIcon.Information);

            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            return 1;
        }
        #endregion

        #region DataTable数据添加

        /// <summary>
        /// 向DataTable添加出库实体，显示出库明细信息
        /// </summary>
        /// <returns></returns>
        protected virtual int AddMaintenanceToDataTable()
        {

            //CONTAINERCONTROL, PRINTERINDEX, CONTAINERDLLNAME, HOS_CODE

            #region 反射定义的接口
            System.Reflection.Assembly assembly = null;
            try
            {
                assembly = System.Reflection.Assembly.LoadFrom(this.InterfaceDefineDll);


                Type[] types = assembly.GetExportedTypes();
                foreach (Type t in types)
                {
                    if (!t.IsInterface)
                    {
                        continue;
                    }

                    DataSet ds = new DataSet();

                    string SQL = @"select * from   com_maintenance_report_print t,com_hospitalinfo h where  t.hos_code = h.hos_code and t.interface = '{0}'";
                    SQL = string.Format(SQL, t.ToString());

                    if (dbMgr.ExecQuery(SQL, ref ds) == -1)
                    {
                        MessageBox.Show("查询接口实现维护出错！");
                        return -1;
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            DataRow row = this.dtDetail.NewRow();

                            row["医院编号"] = r["HOS_CODE"];
                            row["接口名称"] = r["NAME"];
                            row["业务编码"] = r["CONTAINERTYPE"];
                            row["调用接口的dll"] = r["CONTAINERDLLNAME"];
                            row["调用接口的类"] = r["CONTAINERCONTROL"];
                            row["实现接口的dll"] = r["PRINTERDLLNAME"];
                            row["实现接口的类"] = r["PRINTERCONTROL"];
                            row["定义接口的dll"] = this.InterfaceDefineDll;
                            row["接口类"] = t.ToString();
                            row["强制实现"] = FS.FrameWork.Function.NConvert.ToBoolean(r["STATE"]);
                            row["索引"] = r["PRINTERINDEX"];
                            row["备注"] = r["MEMO"];

                            row["PK_HOS_CODE"] = r["HOS_CODE"];
                            row["PK_CONTAINERDLLNAME"] = r["CONTAINERDLLNAME"];
                            row["PK_CONTAINERCONTROL"] = r["CONTAINERCONTROL"];
                            row["PK_PRINTERINDEX"] = r["PRINTERINDEX"];

                            this.dtDetail.Rows.Add(row);
                        }
                    }

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            #endregion


            this.dtDetail.AcceptChanges();
            this.curIDataDetail.FpSpread.ReadSchema(this.settingFileName);

            return 1;
        }

        protected virtual int AddNewMaintenance()
        {
            DataRow row = this.dtDetail.NewRow();

            row["PK_HOS_CODE"] = (new Random()).Next().ToString();
            row["PK_CONTAINERDLLNAME"] = (new Random()).Next().ToString();
            row["PK_CONTAINERCONTROL"] = "New_PK_CONTAINERCONTROL";
            row["PK_PRINTERINDEX"] = "New_PK_PRINTERINDEX";
            
            row["定义接口的dll"] = this.interfaceDefineDll;
            row["实现接口的dll"] = this.interfaceImplementDll;

            this.dtDetail.Rows.Add(row);
            return 1;
        }

        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            this.Init();
            this.curIDataDetail.FpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(FpSpread_CellDoubleClick);
            base.OnLoad(e);
        }

        void FpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader)
            {
                object[] keys = new object[4];

                keys[0] = this.curIDataDetail.FpSpread.GetCellText(0, e.Row, "PK_HOS_CODE");//PK_HOS_CODE
                keys[1] = this.curIDataDetail.FpSpread.GetCellText(0, e.Row, "PK_CONTAINERDLLNAME");//this.dtDetail.Columns["PK_CONTAINERDLLNAME"];
                keys[2] = this.curIDataDetail.FpSpread.GetCellText(0, e.Row, "PK_CONTAINERCONTROL");//this.dtDetail.Columns["PK_CONTAINERCONTROL"];
                keys[3] = this.curIDataDetail.FpSpread.GetCellText(0, e.Row, "PK_PRINTERINDEX");//this.dtDetail.Columns["PK_PRINTERINDEX"];

                DataRow row = this.dtDetail.Rows.Find(keys);
                if (row != null)
                {
                    //新加的数据不在数据库不用删除
                    if (row["PK_CONTAINERCONTROL"].ToString() != "New_PK_CONTAINERCONTROL" && row["PK_PRINTERINDEX"].ToString() != "New_PK_PRINTERINDEX")
                    {

                        #region 删除的数据
                        DialogResult dr = MessageBox.Show("确定删除 吗？", "提示>>", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.Cancel)
                        {
                            return;
                        }

                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        string SQL = @"delete  from com_maintenance_report_print
                                  where    hos_code = '{0}'
                                  and      containerdllname = '{1}'
                                  and      containercontrol = '{2}'
                                  and      printerindex = {3}

                                    ";


                        SQL = string.Format(SQL,
                                row["PK_HOS_CODE"].ToString(),
                                row["PK_CONTAINERDLLNAME"].ToString(),
                                row["PK_CONTAINERCONTROL"].ToString(),
                                row["PK_PRINTERINDEX"].ToString()
                            );

                        if (this.dbMgr.ExecNoQuery(SQL) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存失败：" + this.dbMgr.Err);
                        }
                        #endregion

                    }

                    row.Delete();
                }
            }
        }

        public override int Export(object sender, object neuObject)
        {
            curIDataDetail.FpSpread.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            return base.Export(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return base.OnSave(sender, neuObject);
        }

        public override int SetPrint(object sender, object neuObject)
        {
            this.AddNewMaintenance();
            return base.SetPrint(sender, neuObject);
        }
        #endregion


    }
}