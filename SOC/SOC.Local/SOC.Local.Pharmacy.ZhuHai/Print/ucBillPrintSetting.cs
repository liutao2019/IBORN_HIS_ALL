using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FS.SOC.Local.Pharmacy.ZhuHai.Print
{
    public partial class ucBillPrintSetting : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBillPrintSetting()
        {
            InitializeComponent();
            this.neuFpEnter1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuFpEnter1_ColumnWidthChanged);
        }

        void neuFpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuFpEnter1_Sheet1, Application.StartupPath + "\\Profile\\PharmacyBillPrintSetting.xml");
        }
        FS.HISFC.Components.Manager.Controls.ucPageSize ucPageSize;
        private FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        private FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// ��ȡֽ�����õ�SQLID
        /// </summary>
        private string mySQLID = "SOC.Pharmacy.BillPrintSetting.Query";

        /// <summary>
        /// sqlid
        /// </summary>
        [Description("��ȡֽ�����õ�SQL"),Browsable(true),Category("����")]
        public string SQLID
        {
            get { return mySQLID; }
            set { mySQLID = value; }
        }

        /// <summary>
        /// ��ȡֽ�����õı����ļ�
        /// </summary>
        private string settingFile = "";

        /// <summary>
        /// ��ȡֽ�����õı����ļ�
        /// </summary>
        [Description("��ȡֽ�����õı����ļ�"), Browsable(true), Category("����")]
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
        /// ����Ȩ��
        /// </summary>
        private string class2Code = "";

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        [Browsable(false)]
        public string Class2Code
        {
            get { return class2Code; }
            set { class2Code = value; }
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        private string class3Code = "";

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        [Browsable(false)]
        public string Class3Code
        {
            get { return class3Code; }
            set { class3Code = value; }
        }

        /// <summary>
        /// ��ӡ���õ�DLL
        /// </summary>
        private string myDLLName = "SOC.Local.Pharmacy.ZhuHai.dll";

        /// <summary>
        /// ��ӡ���õ�DLL
        /// </summary>
        [Description("��ӡ���õ�DLL"), Browsable(true), Category("����")]
        public string DLLName
        {
            get { return myDLLName; }
            set { myDLLName = value; }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
                System.Data.DataSet dsDefine = new DataSet();//���ڽ��涨�����ʾ
                System.Data.DataTable dt = new DataTable();
                dsDefine.Tables.Add(dt);
                for (int index = 0; index < (int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.End; index++)
                {
                    dt.Columns.Add(((FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet)index).ToString());
                }
                //this.neuFpEnter1_Sheet1.DataSource = dsDefine;
                //return;

                #region ���ݿ�ϵͳ����
                System.Data.DataSet dsSystem = new DataSet();//�������ݿⶨ��Ĳ�ѯ���
                if (this.pageSizeMgr.ExecQuery(this.SQLID, ref dsSystem, this.Class2Code, this.Class3Code) == -1)
                {
                    MessageBox.Show("��ѯϵͳ���ݳ���!");
                    return;
                }

                //�����ݿ��ѯ����ϵͳ���ø�ֵ��������ʾ����
                foreach (DataRow row in dsSystem.Tables[0].Rows)
                {
                    DataRow r = dt.NewRow();
                    for (int index = 0; index < (int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.End; index++)
                    {
                        try
                        {
                            //���ݵ�һ�δ�ӡʱ��ѯ���Ƿ��ӡ��Ȼ�󱾵ر���ͻ���ѡ���Ƿ��ӡ
                            //��ʱ������
                            r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�Ѿ�ѯ��.ToString()] = true;
                            r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����.ToString()] = this.DLLName;
                            r[((FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet)index).ToString()] = row[((FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet)index).ToString()];
                        }
                        catch { }
                    }
                    dt.Rows.Add(r);
                }
                #endregion

                #region ��ȡ���������ļ�
                if (System.IO.File.Exists(this.SettingFile))
                {
                    System.Data.DataSet dsTmp = new DataSet();
                    dsTmp.ReadXml(this.SettingFile);
                    if (dsTmp == null)
                    {
                        MessageBox.Show("���������ļ������Ѿ���!");
                        return;
                    }
                    for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
                    {
                        DataRow row = dt.Rows[rowIndex];

                        foreach (DataRow r in dsTmp.Tables[0].Rows)
                        {
                            #region
                            if (row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����Ȩ�޴���.ToString()].ToString() == r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����Ȩ�޴���.ToString()].ToString())
                            {
                                if (row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����Ȩ�޴���.ToString()].ToString() == r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����Ȩ�޴���.ToString()].ToString() && row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����.ToString()].ToString() == r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����.ToString()].ToString())
                                {
                                    try
                                    {
                                        row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.��ӡ.ToString()] = r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.��ӡ.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.��ʾ.ToString()] = r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.��ʾ.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.Ԥ��.ToString()] = r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.Ԥ��.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�Ѿ�ѯ��.ToString()] = r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�Ѿ�ѯ��.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�ؼ�.ToString()] = r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�ؼ�.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.��������.ToString()] = r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.��������.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.ÿҳ����.ToString()] = r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.ÿҳ����.ToString()];
                                    }
                                    catch { }

                                    try
                                    {
                                        row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����ʽ.ToString()] = r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����ʽ.ToString()];
                                    }
                                    catch { }
                                    try
                                    {
                                        row[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����.ToString()] = r[FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����.ToString()];
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

                #region ��ʽ��FarPoint
                FarPoint.Win.Spread.CellType.CheckBoxCellType c = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�Ѿ�ѯ��].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.��ӡ].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.��ʾ].CellType = c;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.Ԥ��].CellType = c;

                #region ����
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
                            Type ta = t.GetInterface("FS.SOC.Local.Pharmacy.ZhuHai.Base.IPharmacyBillPrint");
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
                            Type ta = t.GetInterface("FS.SOC.Local.Pharmacy.ZhuHai.Base.IPharmacyBillPrint");
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


                ArrayList allDeptPI = deptMgr.GetDeptmentByType("PI");
                ArrayList allDeptP = deptMgr.GetDeptmentByType("P");
                ArrayList allDept = new ArrayList();
                allDept.AddRange(allDeptP);
                allDept.AddRange(allDeptPI);
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�ؼ�].CellType = cb;

                FarPoint.Win.Spread.CellType.ComboBoxCellType cb2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                cb2.Items = new string[] { FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.����˳��.ToString(), FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.��λ��.ToString() };
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����ʽ].CellType = cb2;
                FarPoint.Win.Spread.CellType.ComboBoxCellType cb3 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����].CellType = cb3;
                string [] param = new string[allDeptP.Count + 1 + allDeptPI.Count];
                param[0] = "ȫ��";
                int indexCount = 1;
                foreach(FS.FrameWork.Models.NeuObject deptInfo in allDept)
                {
                    param[indexCount] = deptInfo.Name;
                    indexCount++;
                }
                cb3.Items = param;
                
                #endregion

                this.neuFpEnter1_Sheet1.ColumnHeader.Rows[0].Height = 36F;

                if (System.IO.File.Exists(Application.StartupPath + "\\Profile\\PharmacyBillPrintSetting.xml"))
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuFpEnter1_Sheet1, Application.StartupPath + "\\Profile\\PharmacyBillPrintSetting.xml");
                }

                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����Ȩ�޴���].Visible = false;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����Ȩ�޴���].Visible = false;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�Ѿ�ѯ��].Visible = false;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�ؼ�].Visible = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����].Visible = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager;

                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����Ȩ�޴���].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����Ȩ�޴���].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�������].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�Ѿ�ѯ��].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.ֽ������].Locked = true;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.����].Locked = !((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.�ؼ�].Locked = !((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager;
                this.neuFpEnter1_Sheet1.Columns[(int)FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.ColSet.ÿҳ����].Locked = !((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager;

                #endregion

                ucPageSize = new FS.HISFC.Components.Manager.Controls.ucPageSize();
                ucPageSize.Dock = DockStyle.Fill;

                this.neuTabControl1.TabPages[1].Controls.Add(ucPageSize);
                this.neuFpEnter1.EditMode = true;
        }

        /// <summary>
        ///  ����
        /// </summary>
        private void Save()
        {
            if (this.neuTabControl1.SelectedTab == tabPage2)
            {
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    
                    this.ucPageSize.Save();
                    this.ucPageSize.Retrieve();
                }
                else
                {
                    MessageBox.Show("ϵͳ���ã��޸���Ч!");
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
                MessageBox.Show("����ɹ�!");
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
