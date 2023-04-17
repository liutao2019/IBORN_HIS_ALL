using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace Neusoft.SOC.Local.Pharmacy.Print
{
    public partial class ucBillPrintSetting : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBillPrintSetting()
        {
            InitializeComponent();
            this.neuFpEnter1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuFpEnter1_ColumnWidthChanged);
        }

        void neuFpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            Neusoft.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuFpEnter1_Sheet1, Application.StartupPath + "\\Profile\\PharmacyBillPrintSetting.xml");
        }
        Neusoft.HISFC.Components.Manager.Controls.ucPageSize ucPageSize;
        private Neusoft.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new Neusoft.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// 获取纸张设置的SQLID
        /// </summary>
        private string mySQLID = "SOC.Pharmacy.BillPrintSetting.Query";

        /// <summary>
        /// sqlid
        /// </summary>
        [Description("获取纸张设置的SQL"),Browsable(true),Category("设置")]
        public string SQLID
        {
            get { return mySQLID; }
            set { mySQLID = value; }
        }

        /// <summary>
        /// 获取纸张设置的本地文件
        /// </summary>
        private string settingFile = "";

        /// <summary>
        /// 获取纸张设置的本地文件
        /// </summary>
        [Description("获取纸张设置的本地文件"), Browsable(true), Category("设置")]
        public string SettingFile
        {
            get 
            {
                if (string.IsNullOrEmpty(this.settingFile))
                {
                    return Application.StartupPath + "\\Profile\\PharmacyBillPrintDefine.xml";
                }
                return this.settingFile;
            }

        }

        /// <summary>
        /// 二级权限
        /// </summary>
        private string class2Code = "";

        /// <summary>
        /// 二级权限
        /// </summary>
        [Browsable(false)]
        public string Class2Code
        {
            get { return class2Code; }
            set { class2Code = value; }
        }

        /// <summary>
        /// 三级权限
        /// </summary>
        private string class3Code = "";

        /// <summary>
        /// 三级权限
        /// </summary>
        [Browsable(false)]
        public string Class3Code
        {
            get { return class3Code; }
            set { class3Code = value; }
        }

        /// <summary>
        /// 打印调用的DLL
        /// </summary>
        private string myDLLName = "SOC.Local.Pharmacy.dll";

        /// <summary>
        /// 打印调用的DLL
        /// </summary>
        [Description("打印调用的DLL"), Browsable(true), Category("设置")]
        public string DLLName
        {
            get { return myDLLName; }
            set { myDLLName = value; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
                System.Data.DataSet dsDefine = new DataSet();//用于界面定义的显示
                System.Data.DataTable dt = new DataTable();
                dsDefine.Tables.Add(dt);
                for (int index = 0; index < (int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.End; index++)
                {
                    dt.Columns.Add(((Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet)index).ToString());
                }
                //this.neuFpEnter1_Sheet1.DataSource = dsDefine;
                //return;

                #region 数据库系统设置
                System.Data.DataSet dsSystem = new DataSet();//用于数据库定义的查询结果
                if (this.pageSizeMgr.ExecQuery(this.SQLID, ref dsSystem, this.Class2Code, this.Class3Code) == -1)
                {
                    MessageBox.Show("查询系统单据出错!");
                    return;
                }

                //从数据库查询到的系统设置赋值到界面显示出来
                foreach (DataRow row in dsSystem.Tables[0].Rows)
                {
                    DataRow r = dt.NewRow();
                    for (int index = 0; index < (int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.End; index++)
                    {
                        try
                        {
                            //单据第一次打印时会询问是否打印，然后本地保存客户的选择：是否打印
                            //暂时不处理
                            r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.已经询问.ToString()] = true;
                            r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.程序集.ToString()] = this.DLLName;
                            r[((Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet)index).ToString()] = row[((Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet)index).ToString()];
                        }
                        catch { }
                    }
                    dt.Rows.Add(r);
                }
                #endregion

                #region 读取本地设置文件
                if (System.IO.File.Exists(this.SettingFile))
                {
                    System.Data.DataSet dsTmp = new DataSet();
                    dsTmp.ReadXml(this.SettingFile);
                    if (dsTmp == null)
                    {
                        MessageBox.Show("本地设置文件可能已经损坏!");
                        return;
                    }
                    for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
                    {
                        DataRow row = dt.Rows[rowIndex];

                        foreach (DataRow r in dsTmp.Tables[0].Rows)
                        {
                            #region
                            if (row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.二级权限代码.ToString()].ToString() == r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.二级权限代码.ToString()].ToString())
                            {
                                if (row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.三级权限代码.ToString()].ToString() == r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.三级权限代码.ToString()].ToString())
                                {
                                    try
                                    {
                                        row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.打印.ToString()] = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.打印.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.提示.ToString()] = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.提示.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.预览.ToString()] = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.预览.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.已经询问.ToString()] = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.已经询问.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.控件.ToString()] = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.控件.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.单据名称.ToString()] = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.单据名称.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.每页行数.ToString()] = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.每页行数.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.排序方式.ToString()] = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.排序方式.ToString()];
                                    }
                                    catch { }
                                }
                            }
                            #endregion
                        }



                    }
                }
                #endregion

                this.neuFpEnter1_Sheet1.DataSource = dsDefine;

                #region 格式化FarPoint
                FarPoint.Win.Spread.CellType.CheckBoxCellType c = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.已经询问].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.打印].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.提示].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.预览].CellType = c;

                #region 反射
                FarPoint.Win.Spread.CellType.ComboBoxCellType cb = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                System.Reflection.Assembly assembly = null;
                try
                {
                    assembly = System.Reflection.Assembly.LoadFrom(this.DLLName);

                    Type[] types = assembly.GetExportedTypes();
                    int count = 0;
                    foreach (Type t in types)
                    {
                        try
                        {
                            //System.Reflection.InterfaceMapping inter = t.GetInterfaceMap(typeof(IPharmacyBill));
                            Type ta = t.GetInterface("Neusoft.SOC.Local.Pharmacy.Base.IPharmacyBillPrint");
                            if (ta == null)
                            {
                                continue;
                            }
                            count++;
                        }
                        catch { }
                    }
                    string[] items = new string[count];
                    count = 0;
                    foreach (Type t in types)
                    {
                        try
                        {
                            //System.Reflection.InterfaceMapping inter = t.GetInterfaceMap(typeof(IPharmacyBill));
                            Type ta = t.GetInterface("Neusoft.SOC.Local.Pharmacy.Base.IPharmacyBillPrint");
                            if (ta == null)
                            {
                                continue;
                            }
                            items[count++] = t.ToString();
                        }
                        catch { }
                    }
                    cb.Items = items;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
                
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.控件].CellType = cb;

                FarPoint.Win.Spread.CellType.ComboBoxCellType cb2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                cb2.Items = new string[] { Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.物理顺序.ToString(), Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.货位号.ToString() };
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.排序方式].CellType = cb2;
                #endregion

                this.neuFpEnter1_Sheet1.ColumnHeader.Rows[0].Height = 36F;

                if (System.IO.File.Exists(Application.StartupPath + "\\Profile\\PharmacyBillPrintSetting.xml"))
                {
                    Neusoft.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuFpEnter1_Sheet1, Application.StartupPath + "\\Profile\\PharmacyBillPrintSetting.xml");
                }

                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.二级权限代码].Visible = false;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.三级权限代码].Visible = false;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.已经询问].Visible = false;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.控件].Visible = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.程序集].Visible = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager;

                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.二级权限代码].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.三级权限代码].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.操作类别].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.已经询问].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.纸张名称].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.程序集].Locked = !((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.控件].Locked = !((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager;
                this.neuFpEnter1_Sheet1.Columns[(int)Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.每页行数].Locked = !((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager;

                #endregion

                ucPageSize = new Neusoft.HISFC.Components.Manager.Controls.ucPageSize();
                ucPageSize.Dock = DockStyle.Fill;

                this.neuTabControl1.TabPages[1].Controls.Add(ucPageSize);
                this.neuFpEnter1.EditMode = true;
        }

        /// <summary>
        ///  保存
        /// </summary>
        private void Save()
        {
            if (this.neuTabControl1.SelectedTab == tabPage2)
            {
                if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                {
                    
                    this.ucPageSize.Save();
                    this.ucPageSize.Retrieve();
                }
                else
                {
                    MessageBox.Show("系统设置，修改无效!");
                    return;
                }
            }
            else if (this.neuTabControl1.SelectedTab == tabPage1)
            {
                if (this.neuFpEnter1_Sheet1.DataSource == null)
                {
                    return;
                }

                System.Data.DataSet ds = this.neuFpEnter1_Sheet1.DataSource as System.Data.DataSet;
                if (ds == null)
                {
                    return;
                }
                ds.AcceptChanges();
                ds.WriteXml(this.SettingFile, XmlWriteMode.WriteSchema);
                this.Init();
                MessageBox.Show("保存成功!");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }


        
    }
}
